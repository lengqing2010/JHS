Imports Itis.Earth.BizLogic
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data
''' <summary>
''' 請求先元帳帳票出力処理
''' </summary>
''' <history>
''' <para>2010/07/14　王霽陽(大連情報システム部)　新規作成</para>
''' </history> 
Partial Public Class SiharaisakiMototyouOutput
    Inherits System.Web.UI.Page
#Region "プライベート変数"

    Private siharaisakiMototyouOutputLogic As New SiharaisakiMototyouOutputLogic
    'ログインユーザーを取得する。
    Private ninsyou As New Ninsyou()

    'FCWのクラスを作成
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "siharaisaki"

    Public Const COMMA As Char = ","c

    Enum PDFStatus As Integer
        OK = 0                              '正常
        IOException = 1                     'エラー(他のユーザがファイルを開いている)
        UnauthorizedAccessException = 2     'エラー(ファイルを作成するパスが不正)
        NoData = 3                          '対象のデータが取得できません。

    End Enum

#End Region
    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")
        If Not IsPostBack Then
            '基本認証
            If ninsyou.GetUserID() = "" Then
                Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
                Server.Transfer("CommonErr.aspx")
            Else
                ViewState("strTysKaisyaCd") = Context.Request("shrCd")
                ViewState("strJigyousyoCd") = Context.Request("jigCd")
                ViewState("strFromDate") = Context.Request("fromDate")
                ViewState("strToDate") = Context.Request("toDate")
                ViewState("syainCd") = ninsyou.GetUserID()
            End If
        End If

        '支払先元帳出力
        Call CreateDataSource()

    End Sub

    ''' <summary>
    ''' データファイルを作ります
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateDataSource()
        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        'インスタンスの生成
        Dim dtSiireSiharaiDataTable As Data.DataTable   '仕入支払データを取得する
        Dim dtSiireSiharaiNmDataTable As Data.DataTable '支払名dt
        Dim strTysKaisyaCd As String = String.Empty     '調査会社コード	
        Dim strJigyousyoCd As String = String.Empty     '支払集計先事業所コード
        Dim strSiharaiNm As String = String.Empty       '支払先名

        Dim strFromDate As String = String.Empty        '抽出期間FROM(YYYY / MM / DD)
        Dim strToDate As String = String.Empty          '抽出期間TO(YYYY / MM / DD)

        Dim lngTyokuzengyouzandaka As Long = 0          '直前行の残高
        Dim lngGoukeikin As Long = 0                    '最終行_金額(仕入データテーブル.仕入金額＋外税額 の合計値)
        Dim errMsg As String = String.Empty
        Dim syainCd As String                           '社員コード
        Dim KakusyuDataSyuturyokuMenuBL As New KakusyuDataSyuturyokuMenuLogic

        strTysKaisyaCd = ViewState("strTysKaisyaCd")
        strJigyousyoCd = ViewState("strJigyousyoCd")
        strFromDate = ViewState("strFromDate")
        strToDate = ViewState("strToDate")
        syainCd = ViewState("syainCd")

        dtSiireSiharaiNmDataTable = KakusyuDataSyuturyokuMenuBL.GetTyousakaisyaSearchResult(strTysKaisyaCd & strJigyousyoCd, _
                                                       String.Empty, _
                                                       String.Empty, _
                                                       String.Empty, _
                                                       True, _
                                                       strTysKaisyaCd & strJigyousyoCd, _
                                                       "2")

        If dtSiireSiharaiNmDataTable.Rows.Count <> 0 Then
            strSiharaiNm = dtSiireSiharaiNmDataTable.Rows(0).Item("seikyuu_saki_shri_saki_mei").ToString.Trim
        Else
            strSiharaiNm = String.Empty
        End If

        fcw = New FcwUtility(Page, syainCd, kinouId)

        '繰越残高を取得
        Dim kurikosiZan As Long = siharaisakiMototyouOutputLogic.GetSiharaiSakiMototyouKurikosiZan(strTysKaisyaCd & strJigyousyoCd, strFromDate)

        '売上入金データ取得
        dtSiireSiharaiDataTable = siharaisakiMototyouOutputLogic.GetSiharaiSakiMototyouData(strTysKaisyaCd, strJigyousyoCd, strFromDate, strToDate)

        If dtSiireSiharaiDataTable.Rows.Count > 0 Then
            '---検索したデータを編集する。---st
            Dim editDt As New DataTable
            Dim editDR As Data.DataRow = editDt.NewRow
            Dim sb As New StringBuilder
            Dim intCount As Integer

            editDt.Columns.Add("seiCdBrc", GetType(String))
            editDt.Columns.Add("seiNm", GetType(String))
            editDt.Columns.Add("fromDate", GetType(String))
            editDt.Columns.Add("toDate", GetType(String))
            editDt.Columns.Add("pageHide", GetType(String))
            editDt.Columns.Add("pagesTotal", GetType(String))
            editDt.Columns.Add("denpyou_uri_date", GetType(String))
            editDt.Columns.Add("kamoku", GetType(String))
            editDt.Columns.Add("syouhin_cd", GetType(String))
            editDt.Columns.Add("hinmei", GetType(String))
            editDt.Columns.Add("kbnbangou", GetType(String))
            editDt.Columns.Add("jutyuu_bukken_mei", GetType(String))
            editDt.Columns.Add("suu", GetType(String))
            editDt.Columns.Add("tanka", GetType(String))
            editDt.Columns.Add("uri_gaku", GetType(String))
            editDt.Columns.Add("sotozei_gaku", GetType(String))
            editDt.Columns.Add("gaku", GetType(String))
            editDt.Columns.Add("zendaka", GetType(String))
            editDt.Columns.Add("denpyou_no", GetType(String))

            '調査会社コード ＋ "-" ＋支払集計先事業所コード
            editDR("seiCdBrc") = strTysKaisyaCd & "-" & strJigyousyoCd
            '請求先名
            editDR("seiNm") = strSiharaiNm
            '抽出期間FROM(YYYY/MM/DD)
            editDR("fromDate") = NengoChange(CDate(strFromDate), False)
            '抽出期間TO(YYYY/MM/DD)
            editDR("toDate") = NengoChange(CDate(strToDate), False)

            If dtSiireSiharaiDataTable.Rows.Count + 2 > 21 Then
                editDR("pageHide") = String.Empty
            Else
                editDR("pageHide") = " "
            End If

            intCount = dtSiireSiharaiDataTable.Rows.Count

            editDR("pagesTotal") = Math.Ceiling((intCount + 2) / 21)

            '先頭行項目(繰越残高の取得する)

            editDR("kamoku") = "繰越残高"
            editDR("zendaka") = kurikosiZan
            lngTyokuzengyouzandaka = kurikosiZan
            editDt.Rows.Add(editDR)
            editDR = editDt.NewRow

            'データ行項目の取得する
            For j As Integer = 0 To dtSiireSiharaiDataTable.Rows.Count - 1
                '年月日
                If TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("nengappi")) = String.Empty Then
                    editDR("denpyou_uri_date") = String.Empty
                Else
                    editDR("denpyou_uri_date") = NengoChange(TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("nengappi")), True)
                End If
                '科目
                editDR("kamoku") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kamoku"))
                '商品コード
                editDR("syouhin_cd") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("syouhin_cd"))
                '商品名/入金種別など
                editDR("hinmei") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("hinmei"))
                '顧客番号
                editDR("kbnbangou") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kyakuno"))
                '物件名/摘要など
                editDR("jutyuu_bukken_mei") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("jutyuu_bukken_mei"))
                '数量
                editDR("suu") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("suu"))
                '単価
                editDR("tanka") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("tanka"))
                '税抜金額
                editDR("uri_gaku") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("siire_gaku"))
                '消費税
                editDR("sotozei_gaku") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("sotozei_gaku"))
                '金額
                editDR("gaku") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kingaku"))
                '残高
                If TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kamoku")) = "仕入" Then
                    editDR("zendaka") = lngTyokuzengyouzandaka + TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kingaku"))
                    lngTyokuzengyouzandaka = lngTyokuzengyouzandaka + TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kingaku"))
                Else
                    editDR("zendaka") = lngTyokuzengyouzandaka - TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kingaku"))
                    lngTyokuzengyouzandaka = lngTyokuzengyouzandaka - TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kingaku"))
                End If

                '伝票番号
                editDR("denpyou_no") = TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("denpyou_no"))
                '最終行_金額
                If TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kamoku")) = "仕入" Then
                    lngGoukeikin = lngGoukeikin + TrimNull(dtSiireSiharaiDataTable.Rows(j).Item("kingaku"))
                End If

                editDt.Rows.Add(editDR)
                editDR = editDt.NewRow
            Next
            '科目
            editDR("kamoku") = "合計"
            editDR("hinmei") = "期間合計"
            editDR("gaku") = lngGoukeikin
            editDR("zendaka") = lngTyokuzengyouzandaka
            editDt.Rows.Add(editDR)
            editDR = editDt.NewRow

            '---検索したデータを編集する。---ed
            'DATデータを作成
            ' ヘッダー部作成
            sb.Append(fcw.CreateDatHeader())
            '   [FixedDataSection] 部作成
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection(editDt)))
            '   [TableDataSection] 部作成
            sb.Append(fcw.CreateTableDataSection(GetTableDataSection(editDt)))

            errMsg = fcw.GetErrMsg(fcw.WriteData(sb.ToString))
        Else
            errMsg = fcw.GetErrMsg(PDFStatus.NoData)
        End If

        ' 請求先元帳帳票データPDF　出力
        If Not errMsg.Equals(String.Empty) Then
            'メッセージ
            Context.Items("strFailureMsg") = errMsg
            Server.Transfer("CommonErr.aspx")
        Else
            fcw.OpenPdf()
        End If
    End Sub

    ''' <summary>
    ''' GetFixedDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection(ByVal data As DataTable) As String

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        'データを取得
        Return fcw.GetFixedDataSection( _
                                                     "seiCdBrc" & _
                                                     ",seiNm" & _
                                                     ",fromDate" & _
                                                     ",toDate" & _
                                                     ",pageHide" & _
                                                     ",pagesTotal", data)
    End Function

    ''' <summary>
    ''' GetTableDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTableDataSection(ByVal data As DataTable) As String

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        '共通CLASS
        Dim earthAction As New EarthAction

        'データを取得
        Return earthAction.JoinDataTable( _
                                                    data, _
                                                    COMMA, _
                                                    "denpyou_uri_date" & _
                                                    ",kamoku" & _
                                                    ",syouhin_cd" & _
                                                    ",hinmei" & _
                                                    ",kbnbangou" & _
                                                    ",jutyuu_bukken_mei" & _
                                                    ",suu" & _
                                                    ",tanka" & _
                                                    ",uri_gaku" & _
                                                    ",sotozei_gaku" & _
                                                    ",gaku" & _
                                                    ",zendaka" & _
                                                    ",denpyou_no")

    End Function

    ''' <summary>空白文字の削除処理</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    ''' <summary>
    ''' ->YYYY年MM月DD日と->YY年MM月DD日
    ''' </summary>
    ''' <param name="nowDate">日付</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NengoChange(ByVal nowDate As Date, Optional ByVal strFlg As Boolean = False) As String
        If strFlg = False Then
            Return nowDate.Year() & "年" & Right("0" & nowDate.Month, 2) & "月" & Right("0" & nowDate.Day, 2) & "日"
        Else
            Return Right(nowDate.Year(), 2) & "/" & Right("0" & nowDate.Month, 2) & "/" & Right("0" & nowDate.Day, 2)
        End If
    End Function
End Class