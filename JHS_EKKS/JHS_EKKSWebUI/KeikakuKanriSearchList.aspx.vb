Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Data
Imports System.Collections.Generic
Imports Lixil.JHS_EKKS.BizLogic
Imports Messages = Lixil.JHS_EKKS.Utilities.CommonMessage
Imports Lixil.JHS_EKKS.Utilities
Partial Class KeikakuKanriSearchList
    Inherits System.Web.UI.Page
    Protected Const CsvFolder As String = "C:\jhs_ekks\download\"
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC
    Public gridviewRightId As String = ""
    Public tblRightId As String = ""
    Public tblLeftId As String = ""
    Private KeikakuKanriSearchListBC As New KeikakuKanriSearchListBC
    Private keikakuKanriKameitenKensakuSyoukaiInquiryBC As New KeikakuKanriKameitenKensakuSyoukaiInquiryBC
    Private strSysNen As Integer = 0
    Private strSysTuki As Integer
    Private intBuildRowIndex As Integer
#Region "パース"
    Public Const sDirName As String = "C:\jhs_ekks"
#End Region


    ''' <summary>
    ''' ページロード場合
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        Dim CommonCheck As New CommonCheck
        Dim CommonBC As New CommonBC
        Dim strUserId As String = ""
        Dim userInfo As New LoginUserInfoList
        Dim blnKegen As Boolean = CommonCheck.CommonNinsyou(strUserId, userInfo, kegen.UserIdOnly)
        ViewState("strUserId") = strUserId

        If blnKegen = False Or userInfo.Items(0).KeikakuMinaosiKengen <> -1 Then
            ViewState("btnMinaosi") = ""
        Else
            ViewState("btnMinaosi") = "1"
        End If
        If blnKegen = False Or userInfo.Items(0).KeikakuKakuteiKengen <> -1 Then

            ViewState("btnKakunin") = ""
        Else
            ViewState("btnKakunin") = "1"
        End If
        If blnKegen = False Or userInfo.Items(0).KeikakuTorikomiKengen <> -1 Then

            ViewState("btnTorikomi") = ""
        Else
            ViewState("btnTorikomi") = "1"
            btnTorikomi1.Button.Enabled = True
            'btnTorikomi2.Button.Enabled = True
            btnTorikomi3.Button.Enabled = True
            'btnTorikomi4.Button.Enabled = True
        End If
        Master.loginUserInfo = userInfo
        strSysNen = Convert.ToDateTime(CommonBC.SelSystemDate.Rows(0).Item(0).ToString).Year
        strSysTuki = Convert.ToDateTime(CommonBC.SelSystemDate.Rows(0).Item(0).ToString).Month

        If strSysTuki <= 3 Then
            strSysNen = strSysNen - 1
        End If

        If Not IsPostBack Then
            btnKeikakuHyouji.Button.Width = CType("120", Unit)
            btnKeikakuHyouji.Button.Height = CType("30", Unit)
            btnMinaosi.Button.BackColor = Drawing.Color.Gold
            btnMinaosi.Button.Font.Bold = True
            btnMinaosi.Button.Width = CType("120", Unit)
            btnKakunin.Button.BackColor = Drawing.Color.Fuchsia
            btnKakunin.Button.Font.Bold = True
            btnKakunin.Button.Width = CType("120", Unit)
            btnKakunin.Button.Style.Add("padding-top", "2px")
            btnMinaosi.Button.Style.Add("padding-top", "2px")
            PageInit()
            ''「Excel出力」ボタンを押下する
            'btnSyuturyoku.OnClientClick = "body_onLoad(""1"");return false;"

        Else
            'Select Case hidSeni.Value
            '    Case "1" 'Excel出力
            '        MakePopJavaScript()
            '        ClientScript.RegisterStartupScript(Me.GetType(), "ERR", "setTimeout('PopPrint()',10);", True)
            'End Select
            'hidSeni.Value = ""
        End If

        Master.ShowMode = ""
        btnHouRenSou.Button.Attributes("onclick") = "window.open('http://jhs-hrs-direct.intra.j-shield.co.jp/ASPSFAES2/root/SfClientMain.php');return false;"
        btnKeikakuHyouji.Button.Attributes.Add("onclick", "ShowModal();")
        btnMinaosi.Button.Attributes.Add("onclick", "ShowModal();")
        btnKakunin.Button.Attributes.Add("onclick", "ShowModal();")
        linMae.Attributes.Add("onclick", "ShowModal();")
        lnkAto.Attributes.Add("onclick", "ShowModal();")
        btnKeikakuHyouji.OnClick = "KeikakuHyouji_Click()"
        ''「Excel出力」ボタンを押下する
        ' btnSyuturyoku.OnClientClick = "PopPrint();return false;"
        btnSyuturyoku.OnClick = "Syuturyoku_Click()"
        btnMinaosi.OnClick = "Minaosi_Click()"
        btnKakunin.OnClick = "Kakunin_Click()"
        If grdItiranRight.Rows.Count > 0 AndAlso grdItiranRight.Visible Then
            gridviewRightId = grdItiranRight.ClientID
            tblRightId = scrollV.ClientID
            tblLeftId = scrollH.ClientID
        End If
        'JavaScriptを作成する
        MakeJavaScript()
        MakePopJavaScript()
        '画面のJS EVENT設定
        Call SetJsEvent()
        btnSyuturyoku.Button.Style.Add("width", "65px;")
        btnTorikomi1.Button.Style.Add("width", "60px;")
        btnTorikomi2.Button.Style.Add("width", "90px;")
        btnTorikomi3.Button.Style.Add("width", "90px;")
        btnTorikomi4.Button.Style.Add("width", "90px;")
        HyperLink2.NavigateUrl = "excel/" & System.Configuration.ConfigurationManager.AppSettings("TemplateFileName")
        HyperLink2.Text = System.Configuration.ConfigurationManager.AppSettings("TemplateFileName")
    End Sub
    Public Sub Syuturyoku_Click()

        Dim CsvDataFile As String = ""

        If CheckInput() = False Then

            Exit Sub
        End If
        '検索条件
        Dim objCommonBC As New CommonBC
        Dim CommonCheck As New CommonCheck
        Dim LoginUserInfoList As New LoginUserInfoList
        '権限がある場合
        CommonCheck.CommonNinsyou("", LoginUserInfoList, kegen.UserIdOnly)
        Dim strSysTuki As DateTime = Convert.ToDateTime(objCommonBC.SelSystemDate.Rows(0).Item(0).ToString)

        CsvDataFile = "Keikaku_Kanri_data_" & strSysTuki.ToString("yyyyMMddHHmmss") & ".csv"
        'Dim XltFile As String = "計画管理フォーマット.xlt"

        '検索条件
        Dim KeikakuKanriSearchListBC As New KeikakuKanriSearchListBC
        Dim dtData As New Data.DataTable
        Dim dtTmp As New Data.DataTable

        Dim KeikakuKanriRecord As KeikakuKanriRecord = SetKensaku(True)
        'dtTmp = KeikakuKanriSearchListBC.GetSitenbetuTukiData(KeikakuKanriRecord)

        Dim data As New StringBuilder()
        Dim data2 As New StringBuilder()
        Dim data3 As New StringBuilder()
        Dim data4 As New StringBuilder()
        Dim blnHead As Boolean = False
        With data
            .Append("KeikakuKanriCSV@@@")
            'For Each dr As DataRow In dtTmp.Rows
            '    For i As Integer = 0 To dr.ItemArray.Length - 1

            '        .Append(dr.Item(i).ToString)
            '        ' 行の最後の項目かどうか
            '        If i.Equals(dr.ItemArray.Length - 1) Then
            '            .Append("@@@")
            '        Else
            '            ' 最後の項目でなければ、「,」で区切る
            '            .Append(",")
            '        End If

            '    Next i
            'Next
            .Append(KeikakuKanriRecord.Nendo)
            .Append(",")
            .Append(KeikakuKanriRecord.Shiten)
            .Append(",")
            .Append(KeikakuKanriRecord.ShitenMei)
            .Append(",")
            .Append(LoginUserInfoList.Items(0).Simei)
            .Append(",HEAD_END")
        End With

        dtData = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.meisai, True)

        If dtData.Rows.Count > 0 Then
            dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.syoukei, True)
            DataTableAdd(dtData, dtTmp)

            If KeikakuKanriRecord.EigyouKBN.FC Then
                dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.FC, True)
                DataTableAdd(dtData, dtTmp)


            End If
            dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.goukeiFC, True)
            DataTableAdd(dtData, dtTmp)
            dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.goukei, True)
            DataTableAdd(dtData, dtTmp)

            With data2


                For Each dr As DataRow In dtData.Rows
                    Dim firstItemFlg As Boolean = True

                    For i As Integer = 0 To dr.ItemArray.Length - 1
                        If (i <= KeikakuKanriRecord.selectIndex.zennen_heikin_tanka) OrElse _
                            (i >= KeikakuKanriRecord.selectIndex.gatu_jisseki_kensuu4 AndAlso _
                            i <= KeikakuKanriRecord.selectIndex.gatu_keisanyou_tyoku_koj_ritu3) OrElse _
                            (i >= KeikakuKanriRecord.selectIndex.gatu_keisanyou_uri_heikin_tanka4 AndAlso _
                            i <= KeikakuKanriRecord.selectIndex.gatu_keisanyou_siire_heikin_tanka3) OrElse _
                            i = KeikakuKanriRecord.selectIndex.keikakuyou_nenkan_tousuu OrElse _
                            i = KeikakuKanriRecord.selectIndex.sds_kaisi_nengetu OrElse _
                            i = KeikakuKanriRecord.selectIndex.data_type OrElse _
                            i = KeikakuKanriRecord.selectIndex.syouhin_cd OrElse _
                            i = KeikakuKanriRecord.selectIndex.kensuu_count_umu OrElse _
                            i = KeikakuKanriRecord.selectIndex.zennen_siire_heikin_tanka Then

                            If Not blnHead Then
                                If firstItemFlg Then
                                    data3.Append("@@@")
                                    data4.Append("@@@")
                                Else
                                    data3.Append(",")
                                    data4.Append(",")
                                End If

                                data3.Append(KeikakuKanriRecord.GetHeadString(0, i))
                                data4.Append(KeikakuKanriRecord.GetHeadString(1, i))
                            End If

                            If firstItemFlg Then
                                .Append("@@@")
                            Else
                                .Append(",")
                            End If
                            .Append(dr.Item(i).ToString)
                        Else
                            If Not blnHead Then
                                data3.Append("")
                                data4.Append("")
                            End If

                            .Append("")

                        End If

                        '' 行の最後の項目かどうか
                        'If i.Equals(dr.ItemArray.Length - 1) Then
                        '    ' 最後の項目ならば、改行コードの変わりに「@@@」を埋め込む
                        '    If Not blnHead Then
                        '        data3.Append("@@@")
                        '        data4.Append("@@@")
                        '    End If
                        '    .Append("@@@")
                        'Else
                        '    ' 最後の項目でなければ、「,」で区切る
                        '    If Not blnHead Then
                        '        data3.Append(",")
                        '        data4.Append(",")
                        '    End If
                        '    .Append(",")
                        'End If
                        firstItemFlg = False
                    Next i
                    blnHead = True
                Next

            End With
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)



            Dim strRepAfterD As String = Replace(data.ToString & data3.ToString & data4.ToString & data2.ToString, "@@@", vbNewLine)    ' 置換後の文字列 
            ' 「@@@」を改行コードに置き換える 


            ' テキストファイルに書き出し 

            writer.WriteLine(strRepAfterD)

            'CSVファイルダウンロード
            Response.Charset = "utf-8"
            Response.ContentType = "text/csv"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(CsvDataFile))
            Response.End()

        Else
            ShowMessage(String.Format(CommonMessage.MSG072E), ddlKeikaku.ClientID)
        End If


    End Sub
    Public Sub Kakunin_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        KeikakuKanriSearchListBC.SetKeikakuKanri(hidDataTime.Value, ViewState("strUserId").ToString, True)
        lblSumi.Text = "「計画済」"
        lblSumi.Style.Add("color", "blue")
        ViewState("keikaku_kakutei_flg") = "1"
        SetButtonEnable()
        If ViewState("btnTorikomi").ToString = "" Then
            btnTorikomi1.Button.Enabled = False
            btnTorikomi2.Button.Enabled = False
            btnTorikomi3.Button.Enabled = False
            btnTorikomi4.Button.Enabled = False
        End If

    End Sub
    Public Sub Minaosi_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        KeikakuKanriSearchListBC.SetKeikakuKanri(hidDataTime.Value, ViewState("strUserId").ToString, False)
        lblSumi.Text = ""

        ViewState("keikaku_kakutei_flg") = "0"
        SetButtonEnable()
    End Sub
    ''' <summary>
    ''' JavaScript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/09/24 車龍(大連情報システム部)　新規作成</history>
    Protected Sub MakePopJavaScript()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "MakePopJavaScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("    function PopPrint(){")
            .AppendLine("       ShowModal();")
            .AppendLine("       var objwindow=window.open(encodeURI('WaitMsg.aspx?url=SeikyusyoExcelOutput.aspx?strNo=" & SetKensaku(True).GetKensakuString & "|divID=" & Me.Master.DivBuySelName.ClientID & "," & Me.Master.DivDisableId.ClientID & "'),'proxy_operation','width=450,height=150,status=no,resizable=no,directories=no,scrollbars=no,left=0,top=0');" & vbCrLf)
            .AppendLine("       objwindow.focus();")
            .AppendLine("    }" & vbCrLf)
            .AppendLine("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub
    Private Function AddRitu(ByVal strRitu As String) As String
        If strRitu = "" Then
            Return ""
        Else
            Return strRitu
        End If
    End Function
    Sub DataTableAdd(ByRef dtMain As DataTable, ByVal dtAdd As DataTable)
        Dim selectIndex As New KeikakuKanriRecord.selectIndex
        For i As Integer = 0 To dtAdd.Rows.Count - 1
            dtMain.Rows.Add(dtAdd.Rows(i).ItemArray)
        Next
    End Sub




    Public Sub KeikakuHyouji_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)


        SetPage(1, True)

    End Sub
    Sub SetPage(ByVal intPage As Integer, ByVal blnKensaku As Boolean)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, intPage)
        '今年データ
        Dim selectIndex As New KeikakuKanriRecord.selectIndex
        Dim dtData As New Data.DataTable
        Dim dtTmp As New Data.DataTable
        Dim intBuildLeft As Integer = 0
        Dim intBuildRight As Integer = 0
        Dim intLeftRowCnt As Integer = 0
        Dim intKameitenDatatableIndex As Integer = 0
        If CheckInput() = False Then
            gridviewLeft.Visible = False
            gridviewRight.Visible = False
            grdItiranLeft.Visible = False
            grdItiranRight.Visible = False
            btnMinaosi.Button.Enabled = False
            btnKakunin.Button.Enabled = False
            btnSyuturyoku.Button.Enabled = False
            If ViewState("btnTorikomi").ToString = "" Then
                btnTorikomi1.Button.Enabled = False
                btnTorikomi2.Button.Enabled = False
                btnTorikomi3.Button.Enabled = False
                btnTorikomi4.Button.Enabled = False
            End If

            gridviewRightId = ""
            tblRightId = ""
            tblLeftId = ""
            lblSumi.Text = ""
            lblKensuu.Text = ""
            Exit Sub
        End If

        '検索条件
        Dim KeikakuKanriRecord As KeikakuKanriRecord
        If blnKensaku Then
            KeikakuKanriRecord = SetKensaku(False)
            ViewState("SetKensaku") = KeikakuKanriRecord
            ViewState("lenPage") = Me.ddlKensuu.SelectedValue
        Else
            KeikakuKanriRecord = CType(ViewState("SetKensaku"), KeikakuKanriRecord)
        End If

        dtData = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.meisai)
        If dtData.Rows.Count > 0 Then
            gridviewLeft.Visible = True
            gridviewRight.Visible = True
            grdItiranLeft.Visible = True
            grdItiranRight.Visible = True
            'If ViewState("btnMinaosi").ToString = "1" Then
            '    btnMinaosi.Button.Enabled = True
            'End If
            'If ViewState("btnKakunin").ToString = "1" Then
            '    btnKakunin.Button.Enabled = True
            'End If
            btnSyuturyoku.Button.Enabled = True
            'btnTorikomi1.Button.Enabled = True
            'btnTorikomi2.Button.Enabled = True
            'btnTorikomi3.Button.Enabled = True
            'btnTorikomi4.Button.Enabled = True
            gridviewRightId = grdItiranRight.ClientID

            tblRightId = scrollV.ClientID
            tblLeftId = scrollH.ClientID
            dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.syoukei)
            DataTableAdd(dtData, dtTmp)
            If chkFC.Checked Then
                dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.FC)
                DataTableAdd(dtData, dtTmp)
                dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.goukeiFC)
                DataTableAdd(dtData, dtTmp)
            End If
            dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.goukei)
            DataTableAdd(dtData, dtTmp)

            '左データテーブル
            Dim dtLeft As New DataTable
            dtLeft.Columns.Add(New DataColumn("Build", GetType(String)))
            dtLeft.Columns.Add(New DataColumn("Kbn", GetType(String)))
            dtLeft.Columns.Add(New DataColumn("KoujiHiritu", GetType(String)))
            dtLeft.Columns.Add(New DataColumn("EigyouKbn", GetType(String)))
            dtLeft.Columns.Add(New DataColumn("Syouhin", GetType(String)))
            dtLeft.Columns.Add(New DataColumn("HeikinTanka", GetType(String)))
            dtLeft.Columns.Add(New DataColumn("DataType", GetType(String)))
            '右データテーブル
            Dim dtRight As New DataTable

            For intCol As Integer = 0 To 12
                If intCol <= 11 Then
                    dtRight.Columns.Add(New DataColumn("KoujiHiritu" & intCol, GetType(String)))

                End If

                '前年同月
                dtRight.Columns.Add(New DataColumn("ZKensuu" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("ZKinkaku" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("ZArari" & intCol, GetType(String)))
                '計画
                dtRight.Columns.Add(New DataColumn("KKensuu" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("KKinkaku" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("KArari" & intCol, GetType(String)))
                '見込
                dtRight.Columns.Add(New DataColumn("MKensuu" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("MKinkaku" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("MArari" & intCol, GetType(String)))
                '実績
                dtRight.Columns.Add(New DataColumn("JKensuu" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("JKinkaku" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("JArari" & intCol, GetType(String)))
                '計画達成率
                dtRight.Columns.Add(New DataColumn("JKKensuu" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("JKKinkaku" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("JKArari" & intCol, GetType(String)))
                '見込達成率
                dtRight.Columns.Add(New DataColumn("MKKensuu" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("MKKinkaku" & intCol, GetType(String)))
                dtRight.Columns.Add(New DataColumn("MKArari" & intCol, GetType(String)))
            Next
            'ヘーダーデータテーブル
            Dim dtHead As New DataTable
            For intCol As Integer = 0 To 12
                If intCol <= 11 Then
                    dtHead.Columns.Add(New DataColumn("KoujiHiritu" & intCol, GetType(String)))
                End If

                '前年同月
                dtHead.Columns.Add(New DataColumn("ZKensuu" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("ZKinkaku" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("ZArari" & intCol, GetType(String)))
                '計画
                dtHead.Columns.Add(New DataColumn("KKensuu" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("KKinkaku" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("KArari" & intCol, GetType(String)))
                '見込
                dtHead.Columns.Add(New DataColumn("MKensuu" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("MKinkaku" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("MArari" & intCol, GetType(String)))
                '実績
                dtHead.Columns.Add(New DataColumn("JKensuu" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("JKinkaku" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("JArari" & intCol, GetType(String)))
                '計画達成率
                dtHead.Columns.Add(New DataColumn("JKKensuu" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("JKKinkaku" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("JKArari" & intCol, GetType(String)))
                '見込達成率
                dtHead.Columns.Add(New DataColumn("MKKensuu" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("MKKinkaku" & intCol, GetType(String)))
                dtHead.Columns.Add(New DataColumn("MKArari" & intCol, GetType(String)))
            Next


            HeadRowData(dtHead)
            Dim strBuild As String = ""
            Dim arrRowGoukei(11) As Integer
            '12*13-1
            Dim arrColSyoukei(155) As Long

            'Dim selectIndex As New KeikakuKanriRecord.selectIndex

            Dim strDataType As String = ""
            Dim blnTyokuKoj As Integer = 0

            'Dim blnAddRow As Boolean = False
            lblSumi.Text = ""
            ViewState("keikaku_huhen_flg") = "0"
            ViewState("keikaku_kakutei_flg") = "0"
            For intCount As Integer = 0 To dtData.Rows.Count - 1

                Dim drKotosi As DataRow = dtData.Rows(intCount)

                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    If intCount = 0 Then
                        strBuild = drKotosi.Item(selectIndex.kameiten_cd).ToString
                        intBuildLeft = 0
                        blnTyokuKoj = 0

                        hidDataTime.Value = KeikakuKanriRecord.Nendo & "|" & drKotosi.Item(selectIndex.kameiten_cd).ToString & "|" & drKotosi.Item(selectIndex.add_datetime).ToString & "|" & drKotosi.Item(selectIndex.syouhin_cd).ToString
                        intKameitenDatatableIndex = 0
                    Else
                        hidDataTime.Value = hidDataTime.Value & "," & KeikakuKanriRecord.Nendo & "|" & drKotosi.Item(selectIndex.kameiten_cd).ToString & "|" & drKotosi.Item(selectIndex.add_datetime).ToString & "|" & drKotosi.Item(selectIndex.syouhin_cd).ToString

                    End If
                    If strBuild <> drKotosi.Item(selectIndex.kameiten_cd).ToString Then
                        LeftRowAdd(strBuild, intCount - 1, dtData, dtLeft, dtRight, intBuildLeft, arrColSyoukei, blnTyokuKoj)
                        strBuild = drKotosi.Item(selectIndex.kameiten_cd).ToString

                        For intCol As Integer = 0 To 11
                            For I As Integer = intCount - intBuildLeft To intCount - 1
                                intLeftRowCnt = intKameitenDatatableIndex + 1
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = AddRitu(dtData.Rows(I).Item(selectIndex.gatu_keisanyou_koj_hantei_ritu4 + intCol * 3).ToString) & "%"
                                End If
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "%" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = ""
                                End If
                                intLeftRowCnt = intKameitenDatatableIndex + 4
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = AddRitu(dtData.Rows(I).Item(selectIndex.gatu_keisanyou_koj_jyuchuu_ritu4 + intCol * 3).ToString) & "%"
                                End If
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "%" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = ""
                                End If
                                intLeftRowCnt = intKameitenDatatableIndex + 7
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = AddRitu(dtData.Rows(I).Item(selectIndex.gatu_keisanyou_tyoku_koj_ritu4 + intCol * 3).ToString) & "%"
                                End If
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "%" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = ""
                                End If
                            Next
                        Next
                        For I As Integer = intCount - intBuildLeft To intCount - 1
                            intLeftRowCnt = intKameitenDatatableIndex + 1
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = AddRitu(dtData.Rows(I).Item(selectIndex.koj_hantei_ritu).ToString) & "%"
                            End If
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "%" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = ""
                            End If
                            intLeftRowCnt = intKameitenDatatableIndex + 3
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = AddRitu(dtData.Rows(I).Item(selectIndex.koj_jyuchuu_ritu).ToString) & "%"
                            End If
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "%" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = ""
                            End If
                            intLeftRowCnt = intKameitenDatatableIndex + 5
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = AddRitu(dtData.Rows(I).Item(selectIndex.tyoku_koj_ritu).ToString) & "%"
                            End If
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "%" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = ""
                            End If
                        Next

                        intKameitenDatatableIndex = dtLeft.Rows.Count
                        intBuildLeft = 0
                        For intArr As Integer = 0 To 155
                            arrColSyoukei(intArr) = 0
                        Next
                        blnTyokuKoj = 0

                    End If


                    LeftRowSet(intCount, dtLeft, dtData, False, intBuildLeft, blnTyokuKoj)
                    RightRowSet(intCount, dtRight, dtLeft, dtData, arrColSyoukei, False, intBuildLeft, blnTyokuKoj)
                    intBuildLeft = intBuildLeft + 1
                Else
                    If strBuild <> "" AndAlso strBuild <> drKotosi.Item(selectIndex.kameiten_cd).ToString Then
                        LeftRowAdd(strBuild, intCount - 1, dtData, dtLeft, dtRight, intBuildLeft, arrColSyoukei, blnTyokuKoj)
                        strBuild = drKotosi.Item(selectIndex.kameiten_cd).ToString
                        For intCol As Integer = 0 To 11
                            For I As Integer = intCount - intBuildLeft To intCount - 1
                                intLeftRowCnt = intKameitenDatatableIndex + 1
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = AddRitu(dtData.Rows(I).Item(selectIndex.gatu_keisanyou_koj_hantei_ritu4 + intCol * 3).ToString) & "%"
                                End If
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "%" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = ""
                                End If
                                intLeftRowCnt = intKameitenDatatableIndex + 4
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = AddRitu(dtData.Rows(I).Item(selectIndex.gatu_keisanyou_koj_jyuchuu_ritu4 + intCol * 3).ToString) & "%"
                                End If
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "%" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = ""
                                End If
                                intLeftRowCnt = intKameitenDatatableIndex + 7
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = AddRitu(dtData.Rows(I).Item(selectIndex.gatu_keisanyou_tyoku_koj_ritu4 + intCol * 3).ToString) & "%"
                                End If
                                If dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol).ToString = "%" Then
                                    dtRight.Rows(intLeftRowCnt).Item("KoujiHiritu" & intCol) = ""
                                End If
                            Next
                        Next
                        For I As Integer = intCount - intBuildLeft To intCount - 1
                            intLeftRowCnt = intKameitenDatatableIndex + 1
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = AddRitu(dtData.Rows(I).Item(selectIndex.koj_hantei_ritu).ToString) & "%"
                            End If
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "%" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = ""
                            End If
                            intLeftRowCnt = intKameitenDatatableIndex + 3
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = AddRitu(dtData.Rows(I).Item(selectIndex.koj_jyuchuu_ritu).ToString) & "%"
                            End If
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "%" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = ""
                            End If
                            intLeftRowCnt = intKameitenDatatableIndex + 5
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = AddRitu(dtData.Rows(I).Item(selectIndex.tyoku_koj_ritu).ToString) & "%"
                            End If
                            If dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu").ToString = "%" Then
                                dtLeft.Rows(intLeftRowCnt).Item("KoujiHiritu") = ""
                            End If
                        Next
                        intBuildLeft = 0
                        blnTyokuKoj = 0
                        strDataType = drKotosi.Item(selectIndex.data_type).ToString & "," & drKotosi.Item(selectIndex.meisyou).ToString
                        For intArr As Integer = 0 To 155
                            arrColSyoukei(intArr) = 0
                        Next
                    End If

                    If strDataType <> drKotosi.Item(selectIndex.data_type).ToString & "," & drKotosi.Item(selectIndex.meisyou).ToString Then
                        Select Case Split(strDataType, ",")(0)
                            Case CInt(KeikakuKanriRecord.selectKbn.FC).ToString, CInt(KeikakuKanriRecord.selectKbn.syoukei).ToString
                                GoukeiRowAdd(dtLeft, dtRight, arrColSyoukei, "小計")
                            Case CInt(KeikakuKanriRecord.selectKbn.goukeiFC).ToString, CInt(KeikakuKanriRecord.selectKbn.goukei).ToString
                                GoukeiRowAdd(dtLeft, dtRight, arrColSyoukei, "合計")
                        End Select

                        For intArr As Integer = 0 To 155
                            arrColSyoukei(intArr) = 0
                        Next
                        intBuildLeft = 0
                        strDataType = drKotosi.Item(selectIndex.data_type).ToString & "," & drKotosi.Item(selectIndex.meisyou).ToString
                        blnTyokuKoj = 0
                    End If
                    'If blnAddRow = False And CInt(drKotosi.Item(selectIndex.data_type).ToString) = KeikakuKanriRecord.selectKbn.syoukei Then
                    '    dtLeft.Rows.Add(dtLeft.NewRow)
                    '    dtRight.Rows.Add(dtRight.NewRow)
                    '    blnAddRow = True
                    'End If
                    'If blnAddRow = True And CInt(drKotosi.Item(selectIndex.data_type).ToString) = KeikakuKanriRecord.selectKbn.goukeiFC Then
                    '    dtLeft.Rows.Add(dtLeft.NewRow)
                    '    dtRight.Rows.Add(dtRight.NewRow)
                    '    blnAddRow = False    
                    'End If
                    LeftRowSet(intCount, dtLeft, dtData, False, intBuildLeft, blnTyokuKoj)
                    RightRowSet(intCount, dtRight, dtLeft, dtData, arrColSyoukei, False, intBuildLeft, blnTyokuKoj)
                    intBuildLeft = intBuildLeft + 1
                End If
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    If lblSumi.Text = "" Then
                        If drKotosi.Item(KeikakuKanriRecord.selectIndex.keikaku_kakutei_flg).ToString = "1" Then
                            lblSumi.Text = "「計画済」"
                            lblSumi.Style.Add("color", "blue")
                            If ViewState("btnTorikomi").ToString = "" Then
                                btnTorikomi1.Button.Enabled = False
                                btnTorikomi2.Button.Enabled = False
                                btnTorikomi3.Button.Enabled = False
                                btnTorikomi4.Button.Enabled = False
                            End If

                            ViewState("keikaku_kakutei_flg") = "1"
                        End If
                    End If


                    If drKotosi.Item(KeikakuKanriRecord.selectIndex.keikaku_huhen_flg).ToString = "1" Then
                        ViewState("keikaku_huhen_flg") = "1"
                    End If

                End If

            Next
            SetButtonEnable()
            GoukeiRowAdd(dtLeft, dtRight, arrColSyoukei, "合計")

            'LeftRowAdd(strBuild, dtData.Rows.Count - 1, dtData, dtLeft, intBuildLeft - 1)
            'RightRowAdd(strBuild, "", intBuildRight - 1, dtData, dtRight, arrColSyoukei)

            ViewState("strKeiKbn") = ""
            ViewState("intKei") = 0
            ViewState("intBuild") = 0
            ViewState("intDataType") = KeikakuKanriRecord.selectKbn.meisai

            ViewState("intKaisi") = 0
            ViewState("strBunbetu") = ""
            ViewState("intBunbetu") = 0
            Dim strTmp As String = GridColIndexSet(dtHead.Columns.Count - 1)

            ViewState("intKaisiCol") = CInt(Split(strTmp, ",")(0))
            ViewState("intSyuuryouCol") = CInt(Split(strTmp, ",")(1))
            ViewState("intMaxCol") = dtHead.Columns.Count - 1
            ViewState("intMaxWidth") = "1573"
            ViewState("blnSinki") = False

            Dim intKensuu As Integer
            Dim bof As Boolean = False
            Dim eof As Boolean = False
            lnkAto.Visible = True
            linMae.Visible = True
            Dim strPage As String = ""
            If blnKensaku Then
                ViewState("Page") = intPage
                intKensuu = KeikakuKanriSearchListBC.GetCount
                ViewState("PageSum") = intKensuu
            Else

                intKensuu = CType(ViewState("PageSum"), Integer)
            End If
            Dim dtRightTable As New DataTable
            grdItiranLeft.DataSource = GetPageData(CType(ViewState("Page"), Integer), dtLeft, dtRight, ViewState("lenPage").ToString, intKensuu, bof, eof, strPage, dtRightTable)
            grdItiranLeft.DataBind()
            gridviewRight.DataSource = dtHead
            gridviewRight.DataBind()
            grdItiranRight.DataSource = dtRightTable
            grdItiranRight.DataBind()

            '======================↓車龍(chel1)↓===============================
            Call Me.SetMeisaiDataStyle()
            '======================↑車龍(chel1)↑===============================

            'If eof Then
            '    grdItiranLeft.Rows(CInt(ViewState("intKei"))).Cells(0).Style.Add("text-align", "center;")
            '    grdItiranLeft.Rows(CInt(ViewState("intKei"))).Cells(0).RowSpan = grdItiranLeft.Rows.Count - CInt(ViewState("intKei"))
            '    For intRow As Integer = CInt(ViewState("intKei")) + 1 To grdItiranLeft.Rows.Count - 1

            '        grdItiranLeft.Rows(intRow).Cells(0).Visible = False
            '    Next
            '    '  全体(FC除く)
            '    grdItiranLeft.Rows(grdItiranLeft.Rows.Count - CInt(ViewState("intKaisi")) - 1).Cells(1).RowSpan = CInt(ViewState("intKaisi")) + 1
            '    For intRow As Integer = grdItiranLeft.Rows.Count - CInt(ViewState("intKaisi")) To grdItiranLeft.Rows.Count - 1

            '        grdItiranLeft.Rows(intRow).Cells(1).Visible = False
            '    Next


            '    CType(grdItiranLeft.Rows(grdItiranLeft.Rows.Count - CInt(ViewState("intKaisi")) - 1).Cells(1).Controls(1), Label).Style.Add("text-align", "center")
            '    CType(grdItiranLeft.Rows(grdItiranLeft.Rows.Count - CInt(ViewState("intKaisi")) - 1).Cells(1).Controls(1), Label).Text = "全体<br>（FC<br>除外）"
            '    lnkAto.Visible = False
            'Else
            '    grdItiranLeft.Rows(CInt(ViewState("intBuild"))).Cells(1).Style.Add("text-align", "center;")
            '    grdItiranLeft.Rows(CInt(ViewState("intBuild"))).Cells(1).RowSpan = grdItiranLeft.Rows.Count - CInt(ViewState("intBuild"))
            '    For intRow As Integer = CInt(ViewState("intBuild")) + 1 To grdItiranLeft.Rows.Count - 1
            '        grdItiranLeft.Rows(intRow).Cells(1).Visible = False
            '    Next
            '    ' e.Row.Cells(0).Style("border-top") = "1px"
            'End If
            If bof Then
                linMae.Visible = False
            End If
            If eof Then
                lnkAto.Visible = False
            End If
            Me.lblKensuu.Text = "&nbsp" & strPage & "/" & intKensuu & "&nbsp件表示" & "&nbsp"
            Me.lblKensuu.Style.Add("color", "blue")

        Else
            gridviewLeft.Visible = False
            gridviewRight.Visible = False
            grdItiranLeft.Visible = False
            grdItiranRight.Visible = False
            btnMinaosi.Button.Enabled = False
            btnKakunin.Button.Enabled = False
            btnSyuturyoku.Button.Enabled = False
            lnkAto.Visible = False
            linMae.Visible = False
            If ViewState("btnTorikomi").ToString = "" Then
                btnTorikomi1.Button.Enabled = False
                btnTorikomi2.Button.Enabled = False
                btnTorikomi3.Button.Enabled = False
                btnTorikomi4.Button.Enabled = False
            End If

            gridviewRightId = ""
            tblRightId = ""
            tblLeftId = ""
            lblSumi.Text = ""
            lblKensuu.Text = ""
            ShowMessage(String.Format(CommonMessage.MSG072E), ddlKeikaku.ClientID)
        End If
    End Sub
    Function GetPageData(ByVal intIndex As Integer, ByVal dtLeft As DataTable, ByVal dtRight As DataTable, ByVal lenPage As String, ByVal intKensuu As Integer, ByRef bof As Boolean, ByRef eof As Boolean, ByRef strPage As String, ByRef dtRightTable As DataTable) As Data.DataTable
        Dim dtTmp As New Data.DataTable
        'Dim nPage As Integer = 0
        Dim len As Integer
        Dim selectIndex As New KeikakuKanriRecord.selectIndex
        'bof = False
        'eof = False
        'If lenPage = "" Then
        '    bof = True
        '    eof = True
        '    strPage = "1～" & intKensuu
        '    Return dt
        'End If
        If lenPage.Equals(String.Empty) Then
            len = intKensuu
        Else
            len = CType(lenPage, Integer)
        End If
        'If len >= intKensuu Then
        '    intIndex = 1
        'End If
        'If intIndex = 1 Then
        '    bof = True
        'End If
        'If (intIndex) * (len * 14) >= intKensuu * 14 Then
        '    nPage = dt.Rows.Count - 1
        '    strPage = (intIndex - 1) * len + 1 & "～" & intKensuu
        '    eof = True
        'Else
        '    nPage = (intIndex) * (len * 14) - 1
        '    strPage = (intIndex - 1) * len + 1 & "～" & intIndex * len
        'End If

        'dtTmp = dt.Clone
        'For i As Integer = (intIndex - 1) * (len * 14) To nPage
        '    dtTmp.Rows.Add(dt.Rows(i).ItemArray)
        'Next
        If lenPage = "" Then
            bof = True
            eof = True
            strPage = "1～" & intKensuu
            'Return dtLeft
        End If

        If len >= intKensuu Then
            intIndex = 1
        End If
        If intIndex = 1 Then
            bof = True
        End If
        If intIndex * len >= intKensuu Then
            strPage = (intIndex - 1) * len + 1 & "～" & intKensuu
            eof = True
        Else
            strPage = (intIndex - 1) * len + 1 & "～" & intIndex * len
        End If
        'len = CType(lenPage, Integer)
        'If intIndex * len > intKensuu Then

        'End If
        'If intIndex * len = intKensuu Then

        'End If
        'If intIndex * len < intKensuu Then

        'End If
        Dim strBuild As String = ""
        Dim intBuild As Integer = 0
        dtTmp = dtLeft.Clone
        dtRightTable = dtRight.Clone
        For i As Integer = 0 To dtLeft.Rows.Count - 1
            If i = 0 Then
                strBuild = dtLeft.Rows(i).Item(selectIndex.kameiten_cd).ToString
                intBuild = 1
            Else
                If dtLeft.Rows(i).Item(0).ToString() = "ビルダー名" Then
                    intBuild = intBuild + 1
                End If
            End If

            If intIndex * len >= intKensuu Then
                If intBuild >= ((intIndex - 1) * len) + 1 Then
                    dtTmp.Rows.Add(dtLeft.Rows(i).ItemArray)
                    dtRightTable.Rows.Add(dtRight.Rows(i).ItemArray)
                End If
            Else
                If intBuild >= ((intIndex - 1) * len) + 1 And intBuild <= intIndex * len Then
                    dtTmp.Rows.Add(dtLeft.Rows(i).ItemArray)
                    dtRightTable.Rows.Add(dtRight.Rows(i).ItemArray)
                End If
            End If


        Next
        Return dtTmp
    End Function
    Private Sub SetButtonEnable()
        btnMinaosi.Button.Enabled = False
        btnKakunin.Button.Enabled = False
        If ViewState("btnTorikomi").ToString = "" Then
            btnTorikomi1.Button.Enabled = False
            btnTorikomi2.Button.Enabled = False
            btnTorikomi3.Button.Enabled = False
            btnTorikomi4.Button.Enabled = False
        End If

        If ViewState("keikaku_kakutei_flg").ToString = "1" Then
            If btnKakunin.Button.Enabled = True Then
                btnKakunin.Button.Enabled = False
            End If
            If ViewState("keikaku_huhen_flg").ToString = "0" Then

                If ViewState("btnMinaosi").ToString = "1" Then
                    btnMinaosi.Button.Enabled = True
                End If
            End If
        Else

            If ViewState("keikaku_huhen_flg").ToString = "0" Then
                btnTorikomi1.Button.Enabled = True
                'btnTorikomi2.Button.Enabled = True
                btnTorikomi3.Button.Enabled = True
                'btnTorikomi4.Button.Enabled = True
            End If
            If ViewState("btnKakunin").ToString = "1" Then
                btnKakunin.Button.Enabled = True
            End If
        End If

    End Sub
    ''' <summary>
    '''  ヘーダーデータテーブル
    ''' </summary>
    ''' <param name="dtHead">データテーブル</param>
    ''' <remarks></remarks>
    Private Sub HeadRowData(ByRef dtHead As DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, dtHead)
        Dim drRow As DataRow = dtHead.NewRow
        For intCol As Integer = 0 To 12
            If intCol <= 11 Then
                drRow.Item("KoujiHiritu" & intCol) = GetNenTuki(intCol)
            End If

            '前年同月
            drRow.Item("ZKensuu" & intCol) = GetNenTuki(intCol)
            drRow.Item("ZKinkaku" & intCol) = GetNenTuki(intCol)
            drRow.Item("ZArari" & intCol) = GetNenTuki(intCol)
            '計画
            drRow.Item("KKensuu" & intCol) = GetNenTuki(intCol)
            drRow.Item("KKinkaku" & intCol) = GetNenTuki(intCol)
            drRow.Item("KArari" & intCol) = GetNenTuki(intCol)
            '見込
            drRow.Item("MKensuu" & intCol) = GetNenTuki(intCol)
            drRow.Item("MKinkaku" & intCol) = GetNenTuki(intCol)
            drRow.Item("MArari" & intCol) = GetNenTuki(intCol)
            '実績
            drRow.Item("JKensuu" & intCol) = GetNenTuki(intCol)
            drRow.Item("JKinkaku" & intCol) = GetNenTuki(intCol)
            drRow.Item("JArari" & intCol) = GetNenTuki(intCol)
            '計画達成率
            drRow.Item("JKKensuu" & intCol) = GetNenTuki(intCol)
            drRow.Item("JKKinkaku" & intCol) = GetNenTuki(intCol)
            drRow.Item("JKArari" & intCol) = GetNenTuki(intCol)
            '見込達成率
            drRow.Item("MKKensuu" & intCol) = GetNenTuki(intCol)
            drRow.Item("MKKinkaku" & intCol) = GetNenTuki(intCol)
            drRow.Item("MKArari" & intCol) = GetNenTuki(intCol)
        Next
        dtHead.Rows.Add(drRow)
        drRow = dtHead.NewRow
        For intCol As Integer = 0 To 12
            If intCol <= 11 Then
                drRow.Item("KoujiHiritu" & intCol) = "売上"
            End If

            '前年同月
            drRow.Item("ZKensuu" & intCol) = "売上"
            drRow.Item("ZKinkaku" & intCol) = "売上"
            drRow.Item("ZArari" & intCol) = "売上"
            '計画
            drRow.Item("KKensuu" & intCol) = "売上"
            drRow.Item("KKinkaku" & intCol) = "売上"
            drRow.Item("KArari" & intCol) = "売上"
            '見込
            drRow.Item("MKensuu" & intCol) = "売上"
            drRow.Item("MKinkaku" & intCol) = "売上"
            drRow.Item("MArari" & intCol) = "売上"
            '実績
            drRow.Item("JKensuu" & intCol) = "売上"
            drRow.Item("JKinkaku" & intCol) = "売上"
            drRow.Item("JArari" & intCol) = "売上"
            '計画達成率
            drRow.Item("JKKensuu" & intCol) = "売上"
            drRow.Item("JKKinkaku" & intCol) = "売上"
            drRow.Item("JKArari" & intCol) = "売上"
            '見込達成率
            drRow.Item("MKKensuu" & intCol) = "売上"
            drRow.Item("MKKinkaku" & intCol) = "売上"
            drRow.Item("MKArari" & intCol) = "売上"
        Next
        dtHead.Rows.Add(drRow)

        drRow = dtHead.NewRow
        For intCol As Integer = 0 To 12
            If intCol <= 11 Then
                drRow.Item("KoujiHiritu" & intCol) = "工事比率（計算用）"
                '前年同月
                drRow.Item("ZKensuu" & intCol) = "前年同月"
                drRow.Item("ZKinkaku" & intCol) = "前年同月"
                drRow.Item("ZArari" & intCol) = "前年同月"

            Else
                '前年同月
                drRow.Item("ZKensuu" & intCol) = "前年"
                drRow.Item("ZKinkaku" & intCol) = "前年"
                drRow.Item("ZArari" & intCol) = "前年"
            End If

            '計画
            drRow.Item("KKensuu" & intCol) = "計画"
            drRow.Item("KKinkaku" & intCol) = "計画"
            drRow.Item("KArari" & intCol) = "計画"
            '見込
            drRow.Item("MKensuu" & intCol) = "見込"
            drRow.Item("MKinkaku" & intCol) = "見込"
            drRow.Item("MArari" & intCol) = "見込"
            '実績
            drRow.Item("JKensuu" & intCol) = "実績"
            drRow.Item("JKinkaku" & intCol) = "実績"
            drRow.Item("JArari" & intCol) = "実績"
            '計画達成率
            drRow.Item("JKKensuu" & intCol) = "計画達成率"
            drRow.Item("JKKinkaku" & intCol) = "計画達成率"
            drRow.Item("JKArari" & intCol) = "計画達成率"
            '見込達成率
            drRow.Item("MKKensuu" & intCol) = "見込進捗率"
            drRow.Item("MKKinkaku" & intCol) = "見込進捗率"
            drRow.Item("MKArari" & intCol) = "見込進捗率"

        Next
        dtHead.Rows.Add(drRow)

        drRow = dtHead.NewRow
        For intCol As Integer = 0 To 12
            If intCol <= 11 Then
                drRow.Item("KoujiHiritu" & intCol) = "工事比率（計算用）"
            End If

            '前年同月
            drRow.Item("ZKensuu" & intCol) = "件数"
            drRow.Item("ZKinkaku" & intCol) = "金額"
            drRow.Item("ZArari" & intCol) = "粗利額"
            '計画
            drRow.Item("KKensuu" & intCol) = "件数"
            drRow.Item("KKinkaku" & intCol) = "金額"
            drRow.Item("KArari" & intCol) = "粗利額"
            '見込
            drRow.Item("MKensuu" & intCol) = "件数"
            drRow.Item("MKinkaku" & intCol) = "金額"
            drRow.Item("MArari" & intCol) = "粗利額"
            '実績
            drRow.Item("JKensuu" & intCol) = "件数"
            drRow.Item("JKinkaku" & intCol) = "金額"
            drRow.Item("JArari" & intCol) = "粗利額"
            '計画達成率
            drRow.Item("JKKensuu" & intCol) = "件数"
            drRow.Item("JKKinkaku" & intCol) = "金額"
            drRow.Item("JKArari" & intCol) = "粗利額"
            '見込達成率
            drRow.Item("MKKensuu" & intCol) = "件数"
            drRow.Item("MKKinkaku" & intCol) = "金額"
            drRow.Item("MKArari" & intCol) = "粗利額"
        Next
        dtHead.Rows.Add(drRow)
    End Sub

    Private Sub GoukeiRowAdd(ByRef dtLeft As DataTable, ByRef dtRight As DataTable, ByRef arrColSyoukei() As Long, Optional ByVal strAdd As String = "")
        '合計行
        Dim drLeft As DataRow = dtLeft.NewRow

        drLeft.Item("Build") = strAdd
        drLeft.Item("KoujiHiritu") = ""
        drLeft.Item("EigyouKbn") = ""
        drLeft.Item("Syouhin") = ""
        drLeft.Item("HeikinTanka") = ""
        dtLeft.Rows.Add(drLeft)
        '合計行
        Dim drRight As DataRow = dtRight.NewRow
        For intCol As Integer = 0 To 12
            If intCol <= 11 Then
                drRight.Item("KoujiHiritu" & intCol) = "合計"
            End If

            '前年同月
            drRight.Item("ZKensuu" & intCol) = ""
            drRight.Item("ZKinkaku" & intCol) = FormatValue(arrColSyoukei(intCol * 12).ToString)
            drRight.Item("ZArari" & intCol) = FormatValue(arrColSyoukei(intCol * 12 + 1).ToString)
            '計画
            drRight.Item("KKensuu" & intCol) = ""
            drRight.Item("KKinkaku" & intCol) = FormatValue(arrColSyoukei(intCol * 12 + 2).ToString)
            drRight.Item("KArari" & intCol) = FormatValue(arrColSyoukei(intCol * 12 + 3).ToString)
            '見込
            drRight.Item("MKensuu" & intCol) = ""
            drRight.Item("MKinkaku" & intCol) = FormatValue(arrColSyoukei(intCol * 12 + 4).ToString)
            drRight.Item("MArari" & intCol) = FormatValue(arrColSyoukei(intCol * 12 + 5).ToString)
            '実績
            drRight.Item("JKensuu" & intCol) = ""
            drRight.Item("JKinkaku" & intCol) = FormatValue(arrColSyoukei(intCol * 12 + 6).ToString)
            drRight.Item("JArari" & intCol) = FormatValue(arrColSyoukei(intCol * 12 + 7).ToString)
            '計画達成率
            drRight.Item("JKKensuu" & intCol) = ""
            drRight.Item("JKKinkaku" & intCol) = FormatValue(Tasseiritu(arrColSyoukei(intCol * 12 + 6), arrColSyoukei(intCol * 12 + 2)).ToString, 1)
            drRight.Item("JKArari" & intCol) = FormatValue(Tasseiritu(arrColSyoukei(intCol * 12 + 7), arrColSyoukei(intCol * 12 + 3)).ToString, 1)
            '見込達成率
            drRight.Item("MKKensuu" & intCol) = ""
            drRight.Item("MKKinkaku" & intCol) = FormatValue(Tasseiritu(arrColSyoukei(intCol * 12 + 4), arrColSyoukei(intCol * 12 + 2)).ToString, 1)
            drRight.Item("MKArari" & intCol) = FormatValue(Tasseiritu(arrColSyoukei(intCol * 12 + 5), arrColSyoukei(intCol * 12 + 3)).ToString, 1)
        Next
        dtRight.Rows.Add(drRight)
    End Sub
    Private Sub LeftRowAdd(ByVal strBuild As String, ByVal intBuild As Integer, ByVal dtKotosi As Data.DataTable, ByRef dtLeft As Data.DataTable, ByRef dtRight As Data.DataTable, ByVal intAddRow As Integer, ByRef arrColSyoukei() As Long, ByRef blnTyokuKoj As Integer)
        If intAddRow > 12 Then
            intBuildRowIndex = intAddRow - 1
        Else
            intBuildRowIndex = 12
        End If
        For intAdd As Integer = intAddRow To intBuildRowIndex

            LeftRowSet(intBuild, dtLeft, dtKotosi, True, intAdd, blnTyokuKoj)
            RightRowSet(intBuild, dtRight, dtLeft, dtKotosi, arrColSyoukei, True, intAdd, blnTyokuKoj)
        Next

        GoukeiRowAdd(dtLeft, dtRight, arrColSyoukei)

    End Sub

    Private Sub RightRowSet(ByVal intCount As Integer, ByRef dtRight As Data.DataTable, ByRef dtleft As Data.DataTable, ByVal dtKotosi As DataTable, ByRef arrColSyoukei() As Long, ByVal blnAdd As Boolean, ByVal intAddCount As Integer, ByVal blnTyokuKoj As Integer)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, intCount, dtKotosi, dtRight, arrColSyoukei)
        Dim selectIndex As New KeikakuKanriRecord.selectIndex
        Dim drRight As DataRow = dtRight.NewRow
        Dim drKotosi As DataRow = dtKotosi.Rows(intCount)
        Dim arrRowGoukei(11) As Long
        'If intAddCount <= 6 Then
        Select Case intAddCount
            Case 0
                For intCol As Integer = 0 To 11
                    drRight.Item("KoujiHiritu" & intCol) = "工事判定率"

                Next
            Case 1
                For intCol As Integer = 0 To 11
                    If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString _
                    Or drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.FC).ToString Then
                        'drRight.Item("KoujiHiritu" & intCol) = AddRitu(drKotosi.Item(selectIndex.gatu_keisanyou_koj_hantei_ritu4 + intCol * 3).ToString)
                        'For i As Integer = 0 To dtKotosi.Rows.Count - 1
                        '    If drRight.Item("KoujiHiritu" & intCol).ToString = "%" OrElse drRight.Item("KoujiHiritu" & intCol).ToString = "" Then

                        '        drRight.Item("KoujiHiritu" & intCol) = AddRitu(dtKotosi.Rows(i).Item(selectIndex.gatu_keisanyou_koj_hantei_ritu4 + intCol * 3).ToString) & "%"
                        '    Else
                        '        Exit For
                        '    End If
                        'Next
                        If drRight.Item("KoujiHiritu" & intCol).ToString = "%" Then
                            drRight.Item("KoujiHiritu" & intCol) = ""
                        End If
                    End If
                Next
            Case 2
                For intCol As Integer = 0 To 11
                    drRight.Item("KoujiHiritu" & intCol) = ""
                Next
            Case 3
                For intCol As Integer = 0 To 11
                    drRight.Item("KoujiHiritu" & intCol) = "工事受注率"
                Next
            Case 4
                For intCol As Integer = 0 To 11
                    If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString _
                    Or drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.FC).ToString Then

                        'For i As Integer = 0 To dtKotosi.Rows.Count - 1
                        '    If drRight.Item("KoujiHiritu" & intCol).ToString = "%" OrElse drRight.Item("KoujiHiritu" & intCol).ToString = "" Then

                        '        drRight.Item("KoujiHiritu" & intCol) = AddRitu(dtKotosi.Rows(i).Item(selectIndex.gatu_keisanyou_koj_jyuchuu_ritu4 + intCol * 3).ToString) & "%"
                        '    Else
                        '        Exit For
                        '    End If
                        'Next
                        'If drRight.Item("KoujiHiritu" & intCol).ToString = "%" Then
                        '    drRight.Item("KoujiHiritu" & intCol) = ""
                        'End If
                    End If
                Next
            Case 5
                For intCol As Integer = 0 To 11
                    drRight.Item("KoujiHiritu" & intCol) = ""
                Next
            Case 6
                For intCol As Integer = 0 To 11
                    drRight.Item("KoujiHiritu" & intCol) = "直工事率"
                Next
            Case 7
                For intCol As Integer = 0 To 11
                    If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString _
                    Or drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.FC).ToString Then
                        ' drRight.Item("KoujiHiritu" & intCol) = AddRitu(drKotosi.Item(selectIndex.gatu_keisanyou_tyoku_koj_ritu4 + intCol * 3).ToString)
                        'For i As Integer = 0 To dtKotosi.Rows.Count - 1
                        '    If drRight.Item("KoujiHiritu" & intCol).ToString = "%" OrElse drRight.Item("KoujiHiritu" & intCol).ToString = "" Then

                        '        drRight.Item("KoujiHiritu" & intCol) = AddRitu(dtKotosi.Rows(i).Item(selectIndex.gatu_keisanyou_tyoku_koj_ritu4 + intCol * 3).ToString) & "%"
                        '    Else
                        '        Exit For
                        '    End If
                        '    If drRight.Item("KoujiHiritu" & intCol).ToString = "%" Then
                        '        drRight.Item("KoujiHiritu" & intCol) = ""
                        '    End If
                        'Next
                    End If
                Next
            Case Else
                For intCol As Integer = 0 To 11
                    drRight.Item("KoujiHiritu" & intCol) = ""
                Next
        End Select

        If blnAdd Then
            For intCol As Integer = 0 To 12
                '前年同月
                drRight.Item("ZKensuu" & intCol) = ""
                drRight.Item("ZKinkaku" & intCol) = ""
                drRight.Item("ZArari" & intCol) = ""
                '計画
                drRight.Item("KKensuu" & intCol) = ""
                drRight.Item("KKinkaku" & intCol) = ""
                drRight.Item("KArari" & intCol) = ""
                '見込
                drRight.Item("MKensuu" & intCol) = ""
                drRight.Item("MKinkaku" & intCol) = ""
                drRight.Item("MArari" & intCol) = ""
                '実績
                drRight.Item("JKensuu" & intCol) = ""
                drRight.Item("JKinkaku" & intCol) = ""
                drRight.Item("JArari" & intCol) = ""
                '計画達成率
                drRight.Item("JKKensuu" & intCol) = ""
                drRight.Item("JKKinkaku" & intCol) = ""
                drRight.Item("JKArari" & intCol) = ""
                '見込達成率
                drRight.Item("MKKensuu" & intCol) = ""
                drRight.Item("MKKinkaku" & intCol) = ""
                drRight.Item("MKArari" & intCol) = ""
            Next
        Else
            For intCol As Integer = 0 To 11

                '前年同月
                drRight.Item("ZKensuu" & intCol) = FormatValue(drKotosi.Item(selectIndex.z_gatu_jisseki_kensuu4 + intCol * 3).ToString)
                arrRowGoukei(8) = arrRowGoukei(8) + SetValue(drKotosi, selectIndex.z_gatu_jisseki_kensuu4 + intCol * 3)
                drRight.Item("ZKinkaku" & intCol) = FormatValue(drKotosi.Item(selectIndex.z_gatu_jisseki_kingaku4 + intCol * 3).ToString)
                arrRowGoukei(0) = arrRowGoukei(0) + SetValue(drKotosi, selectIndex.z_gatu_jisseki_kingaku4 + intCol * 3)
                arrColSyoukei(intCol * 12) = arrColSyoukei(intCol * 12) + SetValue(drKotosi, selectIndex.z_gatu_jisseki_kingaku4 + intCol * 3)
                drRight.Item("ZArari" & intCol) = FormatValue(drKotosi.Item(selectIndex.z_gatu_jisseki_arari4 + intCol * 3).ToString)
                arrRowGoukei(1) = arrRowGoukei(1) + SetValue(drKotosi, selectIndex.z_gatu_jisseki_arari4 + intCol * 3)
                arrColSyoukei(intCol * 12 + 1) = arrColSyoukei(intCol * 12 + 1) + SetValue(drKotosi, selectIndex.z_gatu_jisseki_arari4 + intCol * 3)

                '計画
                drRight.Item("KKensuu" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_keikaku_kensuu4 + intCol * 3).ToString)
                arrRowGoukei(9) = arrRowGoukei(9) + SetValue(drKotosi, selectIndex.gatu_keikaku_kensuu4 + intCol * 3)
                drRight.Item("KKinkaku" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_keikaku_kingaku4 + intCol * 3).ToString)
                arrRowGoukei(2) = arrRowGoukei(2) + SetValue(drKotosi, selectIndex.gatu_keikaku_kingaku4 + intCol * 3)
                arrColSyoukei(intCol * 12 + 2) = arrColSyoukei(intCol * 12 + 2) + SetValue(drKotosi, selectIndex.gatu_keikaku_kingaku4 + intCol * 3)

                drRight.Item("KArari" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_keikaku_arari4 + intCol * 3).ToString)
                arrRowGoukei(3) = arrRowGoukei(3) + SetValue(drKotosi, selectIndex.gatu_keikaku_arari4 + intCol * 3)
                arrColSyoukei(intCol * 12 + 3) = arrColSyoukei(intCol * 12 + 3) + SetValue(drKotosi, selectIndex.gatu_keikaku_arari4 + intCol * 3)
                '見込
                drRight.Item("MKensuu" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_mikomi_kensuu4 + intCol * 3).ToString)
                arrRowGoukei(10) = arrRowGoukei(10) + SetValue(drKotosi, selectIndex.gatu_mikomi_kensuu4 + intCol * 3)
                drRight.Item("MKinkaku" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_mikomi_kingaku4 + intCol * 3).ToString)
                arrRowGoukei(4) = arrRowGoukei(4) + SetValue(drKotosi, selectIndex.gatu_mikomi_kingaku4 + intCol * 3)
                arrColSyoukei(intCol * 12 + 4) = arrColSyoukei(intCol * 12 + 4) + SetValue(drKotosi, selectIndex.gatu_mikomi_kingaku4 + intCol * 3)
                drRight.Item("MArari" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_mikomi_arari4 + intCol * 3).ToString)
                arrRowGoukei(5) = arrRowGoukei(5) + SetValue(drKotosi, selectIndex.gatu_mikomi_arari4 + intCol * 3)
                arrColSyoukei(intCol * 12 + 5) = arrColSyoukei(intCol * 12 + 5) + SetValue(drKotosi, selectIndex.gatu_mikomi_arari4 + intCol * 3)
                '実績
                drRight.Item("JKensuu" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_jisseki_kensuu4 + intCol * 3).ToString)
                arrRowGoukei(11) = arrRowGoukei(11) + SetValue(drKotosi, selectIndex.gatu_jisseki_kensuu4 + intCol * 3)
                drRight.Item("JKinkaku" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_jisseki_kingaku4 + intCol * 3).ToString)
                arrRowGoukei(6) = arrRowGoukei(6) + SetValue(drKotosi, selectIndex.gatu_jisseki_kingaku4 + intCol * 3)
                arrColSyoukei(intCol * 12 + 6) = arrColSyoukei(intCol * 12 + 6) + SetValue(drKotosi, selectIndex.gatu_jisseki_kingaku4 + intCol * 3)
                drRight.Item("JArari" & intCol) = FormatValue(drKotosi.Item(selectIndex.gatu_jisseki_arari4 + intCol * 3).ToString)
                arrRowGoukei(7) = arrRowGoukei(7) + SetValue(drKotosi, selectIndex.gatu_jisseki_arari4 + intCol * 3)
                arrColSyoukei(intCol * 12 + 7) = arrColSyoukei(intCol * 12 + 7) + SetValue(drKotosi, selectIndex.gatu_jisseki_arari4 + intCol * 3)
                '計画達成率
                drRight.Item("JKKensuu" & intCol) = FormatValue(Tasseiritu(drKotosi, selectIndex.gatu_jisseki_kensuu4 + intCol * 3, selectIndex.gatu_keikaku_kensuu4 + intCol * 3).ToString, 1)
                drRight.Item("JKKinkaku" & intCol) = FormatValue(Tasseiritu(drKotosi, selectIndex.gatu_jisseki_kingaku4 + intCol * 3, selectIndex.gatu_keikaku_kingaku4 + intCol * 3).ToString, 1)
                'arrColSyoukei(intCol * 12 + 8) = arrColSyoukei(intCol * 12 + 9) + SetValue(drRight.Item("JKKinkaku" & intCol).ToString)
                drRight.Item("JKArari" & intCol) = FormatValue(Tasseiritu(drKotosi, selectIndex.gatu_jisseki_arari4 + intCol * 3, selectIndex.gatu_keikaku_arari4 + intCol * 3).ToString, 1)
                'arrColSyoukei(intCol * 12 + 9) = arrColSyoukei(intCol * 12 + 10) + SetValue(drRight.Item("JKArari" & intCol).ToString)
                '見込達成率
                drRight.Item("MKKensuu" & intCol) = FormatValue(Tasseiritu(drKotosi, selectIndex.gatu_mikomi_kensuu4 + intCol * 3, selectIndex.gatu_keikaku_kensuu4 + intCol * 3).ToString, 1)
                drRight.Item("MKKinkaku" & intCol) = FormatValue(Tasseiritu(drKotosi, selectIndex.gatu_mikomi_kingaku4 + intCol * 3, selectIndex.gatu_keikaku_kensuu4 + intCol * 3).ToString, 1)
                'arrColSyoukei(intCol * 12 + 10) = arrColSyoukei(intCol * 12 + 11) + SetValue(drRight.Item("MKKinkaku" & intCol).ToString)
                drRight.Item("MKArari" & intCol) = FormatValue(Tasseiritu(drKotosi, selectIndex.gatu_mikomi_arari4 + intCol * 3, selectIndex.gatu_keikaku_kensuu4 + intCol * 3).ToString, 1)
                'arrColSyoukei(intCol * 12 + 11) = arrColSyoukei(intCol * 12 + 12) + SetValue(drRight.Item("MKArari" & intCol).ToString)
            Next
            Dim intCol2 As Integer = 12
            '前年同月
            drRight.Item("ZKensuu" & intCol2) = FormatValue(arrRowGoukei(8).ToString)
            drRight.Item("ZKinkaku" & intCol2) = FormatValue(arrRowGoukei(0).ToString)
            arrColSyoukei(144) = arrColSyoukei(144) + arrRowGoukei(0)
            drRight.Item("ZArari" & intCol2) = FormatValue(arrRowGoukei(1).ToString)
            arrColSyoukei(145) = arrColSyoukei(145) + arrRowGoukei(1)

            '計画
            drRight.Item("KKensuu" & intCol2) = FormatValue(arrRowGoukei(9).ToString)
            drRight.Item("KKinkaku" & intCol2) = FormatValue(arrRowGoukei(2).ToString)
            arrColSyoukei(146) = arrColSyoukei(146) + arrRowGoukei(2)
            drRight.Item("KArari" & intCol2) = FormatValue(arrRowGoukei(3).ToString)
            arrColSyoukei(147) = arrColSyoukei(147) + arrRowGoukei(3)
            '見込
            drRight.Item("MKensuu" & intCol2) = FormatValue(arrRowGoukei(10).ToString)
            drRight.Item("MKinkaku" & intCol2) = FormatValue(arrRowGoukei(4).ToString)
            arrColSyoukei(148) = arrColSyoukei(148) + arrRowGoukei(4)
            drRight.Item("MArari" & intCol2) = FormatValue(arrRowGoukei(5).ToString)
            arrColSyoukei(149) = arrColSyoukei(149) + arrRowGoukei(5)

            '実績
            drRight.Item("JKensuu" & intCol2) = FormatValue(arrRowGoukei(11).ToString)
            drRight.Item("JKinkaku" & intCol2) = FormatValue(arrRowGoukei(6).ToString)
            arrColSyoukei(150) = arrColSyoukei(150) + arrRowGoukei(6)
            drRight.Item("JArari" & intCol2) = FormatValue(arrRowGoukei(7).ToString)
            arrColSyoukei(151) = arrColSyoukei(151) + arrRowGoukei(7)

            '計画達成率
            drRight.Item("JKKensuu" & intCol2) = FormatValue(Tasseiritu(arrRowGoukei(11), arrRowGoukei(9)).ToString, 1)
            drRight.Item("JKKinkaku" & intCol2) = FormatValue(Tasseiritu(arrRowGoukei(6), arrRowGoukei(2)).ToString, 1)

            drRight.Item("JKArari" & intCol2) = FormatValue(Tasseiritu(arrRowGoukei(7), arrRowGoukei(3)).ToString, 1)
            '見込達成率
            drRight.Item("MKKensuu" & intCol2) = FormatValue(Tasseiritu(arrRowGoukei(10), arrRowGoukei(9)).ToString, 1)
            drRight.Item("MKKinkaku" & intCol2) = FormatValue(Tasseiritu(arrRowGoukei(4), arrRowGoukei(2)).ToString, 1)
            drRight.Item("MKArari" & intCol2) = FormatValue(Tasseiritu(arrRowGoukei(5), arrRowGoukei(3)).ToString, 1)

        End If
        dtRight.Rows.Add(drRight)
    End Sub
    ''' <summary>
    ''' 列indexより、年月を作成する
    ''' </summary>
    ''' <param name="intCol"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNenTuki(ByVal intCol As Integer) As String
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, intCol)
        If intCol <= 8 Then
            Return ddlKeikaku.SelectedValue & "年" & CStr(intCol + 4) & "月"
        Else
            If intCol = 12 Then
                Return ddlKeikaku.SelectedValue & "年度年間"
            Else
                Return CType(ddlKeikaku.SelectedValue, Integer) + 1 & "年" & CStr(intCol - 8) & "月"
            End If

        End If
    End Function
    ''' <summary>
    ''' 文字列より、INT型を変換
    ''' </summary>
    ''' <param name="strVlaue">文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetValue(ByVal strVlaue As String) As Integer
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, strVlaue)
        If strVlaue = "" Then
            Return 0
        Else
            Return CInt(strVlaue)
        End If

    End Function
    ''' <summary>
    ''' DBより、INT型を変換
    ''' </summary>
    ''' <param name="drKotosi">今年datarow</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetValue(ByVal drKotosi As DataRow, ByVal intIndex As Integer) As Long
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, drKotosi, intIndex)
        If IsDBNull(drKotosi.Item(intIndex)) Then
            Return 0
        Else
            Return CLng(drKotosi.Item(intIndex))
        End If

    End Function
    Function FormatValue(ByVal strVlaue As String, Optional ByVal Num As Integer = 0) As String
        If strVlaue = "" Then
            Return ""
        Else
            If Num = 1 Then
                Return FormatNumber(strVlaue, Num) & "%"
            Else
                Return FormatNumber(strVlaue, Num)
            End If

        End If
    End Function
    ''' <summary>
    ''' 達成率計算
    ''' </summary>
    ''' <param name="drKotosi">今年datarow</param>
    ''' <param name="intBunshi">分子</param>
    ''' <param name="intBunbo">分母</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Tasseiritu(ByVal drKotosi As DataRow, ByVal intBunshi As Integer, ByVal intBunbo As Integer) As String
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, drKotosi, intBunshi, intBunbo)
        If IsDBNull(drKotosi.Item(intBunshi)) Then
            Return ""
        End If
        If IsDBNull(drKotosi.Item(intBunbo)) Then
            Return ""
        End If
        If drKotosi.Item(intBunbo).ToString = "0" Then
            Return ""
        End If
        Return CStr(Math.Round(CLng(drKotosi.Item(intBunshi)) * 100 / CLng(drKotosi.Item(intBunbo)), 1))
    End Function
    ''' <summary>
    ''' 達成率計算
    ''' </summary>
    ''' <param name="intBunshi">分子</param>
    ''' <param name="intBunbo">分母</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Tasseiritu(ByVal intBunshi As Long, ByVal intBunbo As Long) As String
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, intBunshi, intBunbo)
        If intBunbo = 0 Then
            Return ""
        End If
        Return CStr(Math.Round(intBunshi * 100 / intBunbo, 1))
    End Function
    'Private Function SetTyokuKoj(ByVal drKotosi As DataRow, ByRef blnTyokuKoj As Integer) As String
    '    Dim selectIndex As New KeikakuKanriRecord.selectIndex


    '    If drKotosi.Item(selectIndex.meisyou2).ToString = "工事" Then
    '        If blnTyokuKoj = 0 Then
    '            blnTyokuKoj = 1
    '            Return "直工事率"
    '        ElseIf blnTyokuKoj = 1 Then
    '            blnTyokuKoj = 2
    '            Return AddRitu(drKotosi.Item(selectIndex.tyoku_koj_ritu).ToString)
    '        ElseIf blnTyokuKoj = 2 Then
    '            blnTyokuKoj = 3
    '            Return ""
    '        Else
    '            Return ""
    '        End If
    '    Else
    '        Return ""
    '    End If
    'End Function
    Private Sub LeftRowSet(ByVal intRow As Integer, ByRef dtLeft As Data.DataTable, ByVal dtKotosi As DataTable, ByVal blnAdd As Boolean, ByVal intAddCount As Integer, ByRef blnTyokuKoj As Integer)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, intRow, dtLeft, dtKotosi, blnAdd)
        Dim selectIndex As New KeikakuKanriRecord.selectIndex
        Dim drLeft As DataRow = dtLeft.NewRow
        Dim drKotosi As DataRow = dtKotosi.Rows(intRow)

        Select Case intAddCount
            Case 0
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = "ビルダー名"
                Else
                    drLeft.Item("Build") = ""
                End If

                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If
                drLeft.Item("KoujiHiritu") = "工事判定率"

                'Case 1
                '    drLeft.Item("Build") = ""
                '    If blnAdd Then
                '        drLeft.Item("Kbn") = ""
                '    Else
                '        drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                '    End If


            Case 1

                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = drKotosi.Item(selectIndex.kameiten_mei)
                Else
                    drLeft.Item("Build") = ""
                End If
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If
                drLeft.Item("KoujiHiritu") = ""

            Case 2
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = drKotosi.Item(selectIndex.kameiten_mei)
                Else
                    drLeft.Item("Build") = ""
                End If
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If
                drLeft.Item("KoujiHiritu") = "工事受注率"
            Case 3

                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = "加盟店コード"


                Else
                    drLeft.Item("Build") = ""
                    drLeft.Item("KoujiHiritu") = ""
                End If
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

                'Case 5
                '    drLeft.Item("Build") = ""
                '    If blnAdd Then
                '        drLeft.Item("Kbn") = ""
                '    Else
                '        drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                '    End If
                drLeft.Item("KoujiHiritu") = ""
            Case 4
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = drKotosi.Item(selectIndex.kameiten_cd)
                Else
                    drLeft.Item("Build") = ""
                End If
                drLeft.Item("KoujiHiritu") = "直工事率"
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If


                'Case 7
                '    drLeft.Item("Build") = ""
                '    If blnAdd Then
                '        drLeft.Item("Kbn") = ""
                '    Else
                '        drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                '    End If
                '    If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then


                '    Else
                '        drLeft.Item("KoujiHiritu") = ""
                '    End If

            Case 5
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = "担当名"
                Else
                    drLeft.Item("Build") = ""
                End If
                drLeft.Item("KoujiHiritu") = ""
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

                'Case 9
                '    drLeft.Item("Build") = ""
                '    If blnAdd Then
                '        drLeft.Item("Kbn") = ""
                '    Else
                '        drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                '    End If
                '    drLeft.Item("KoujiHiritu") = ""
            Case 6
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = drKotosi.Item(selectIndex.eigyou_tantousya_mei)

                Else
                    drLeft.Item("Build") = ""

                End If
                drLeft.Item("KoujiHiritu") = ""
                ' drLeft.Item("Build") = drKotosi.Item(selectIndex.eigyou_tantousya_mei)
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

                'Case 11
                '    drLeft.Item("Build") = ""
                '    If blnAdd Then
                '        drLeft.Item("Kbn") = ""
                '    Else
                '        drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                '    End If
                '    drLeft.Item("KoujiHiritu") = ""
            Case 7
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = "年間棟数"

                Else
                    drLeft.Item("Build") = ""

                End If
                drLeft.Item("KoujiHiritu") = ""
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

                'Case 13
                '    drLeft.Item("Build") = ""
                '    If blnAdd Then
                '        drLeft.Item("Kbn") = ""
                '    Else
                '        drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                '    End If
                '    drLeft.Item("KoujiHiritu") = ""
            Case 8
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = (drKotosi.Item(selectIndex.keikaku_nenkan_tousuu).ToString)
                Else
                    drLeft.Item("Build") = ""
                End If
                drLeft.Item("KoujiHiritu") = ""
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

                'Case 15
                '    drLeft.Item("Build") = ""
                '    If blnAdd Then
                '        drLeft.Item("Kbn") = ""
                '    Else
                '        drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                '    End If
                '    drLeft.Item("KoujiHiritu") = ""
            Case 9
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = "業態"
                Else
                    drLeft.Item("Build") = ""
                End If
                drLeft.Item("KoujiHiritu") = ""
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

            Case 10
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = drKotosi.Item(selectIndex.gyoutai)
                Else
                    drLeft.Item("Build") = ""
                End If
                drLeft.Item("KoujiHiritu") = ""
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

                'Case 18
                '    drLeft.Item("Build") = ""
                '    If blnAdd Then
                '        drLeft.Item("Kbn") = ""
                '    Else
                '        drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                '    End If
                '    drLeft.Item("KoujiHiritu") = ""
            Case 11
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = "売上比率"
                Else
                    drLeft.Item("Build") = ""
                End If
                drLeft.Item("KoujiHiritu") = ""
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

            Case 12
                If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then
                    drLeft.Item("Build") = AddRitu(drKotosi.Item(selectIndex.uri_hiritu).ToString) & "%"
                Else
                    drLeft.Item("Build") = ""
                End If
                drLeft.Item("KoujiHiritu") = ""
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If

            Case Else
                drLeft.Item("Build") = ""
                If blnAdd Then
                    drLeft.Item("Kbn") = ""
                Else
                    drLeft.Item("Kbn") = drKotosi.Item(selectIndex.meisyou)
                End If
                drLeft.Item("KoujiHiritu") = ""
        End Select
        If drKotosi.Item(selectIndex.data_type).ToString = CInt(KeikakuKanriRecord.selectKbn.meisai).ToString Then

        End If
        Select Case drKotosi.Item(selectIndex.data_type).ToString
            Case CInt(KeikakuKanriRecord.selectKbn.FC).ToString, CInt(KeikakuKanriRecord.selectKbn.syoukei).ToString
                drLeft.Item("Build") = "小計"
            Case CInt(KeikakuKanriRecord.selectKbn.goukeiFC).ToString, CInt(KeikakuKanriRecord.selectKbn.goukei).ToString
                drLeft.Item("Build") = "合計"
        End Select
        If blnAdd Then
            drLeft.Item("EigyouKbn") = ""
            drLeft.Item("Syouhin") = ""
            drLeft.Item("HeikinTanka") = ""
        Else
            drLeft.Item("EigyouKbn") = drKotosi.Item(selectIndex.meisyou2)
            drLeft.Item("Syouhin") = drKotosi.Item(selectIndex.syouhin_mei)
            drLeft.Item("HeikinTanka") = FormatValue(drKotosi.Item(selectIndex.zennen_heikin_tanka).ToString)
        End If
        drLeft.Item("DataType") = drKotosi.Item(selectIndex.data_type).ToString & "," & drKotosi.Item(selectIndex.kensuu_count_umu).ToString
        dtLeft.Rows.Add(drLeft)
    End Sub


    ''' <summary>
    ''' 「計画表示」ボタンクリック時の入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckInput() As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        If ddlKeikaku.SelectedValue = "" Then
            ShowMessage(String.Format(Messages.MSG001E, "計画年度"), ddlKeikaku.ClientID)
            Return False
        ElseIf (chkSangetu.Checked OrElse chkKongetu.Checked OrElse chkYogetu.Checked) AndAlso (ddlKeikaku.SelectedValue <> strSysNen.ToString) Then
            ShowMessage(Messages.MSG047E, ddlKeikaku.ClientID)
            Return False
        End If
        If chkFC.Checked AndAlso tbxSiten.Text.Trim = "" Then
            ShowMessage(String.Format(Messages.MSG001E, "支店名"), tbxSiten.ClientID)
            Return False
        End If
        Dim intDataCount As Integer
        If tbxSiten.Text <> "" Then
            intDataCount = ShowPop(KeikakuKanriRecord.popKbn.Shiten, False)
            If intDataCount = 0 Then
                ShowMessage(String.Format(CommonMessage.MSG022E, "支店名"), tbxSiten.ClientID)
                Return False

            ElseIf intDataCount > 1 Then
                ShowMessage(String.Format(CommonMessage.MSG062E, "支店名"), tbxSiten.ClientID)
                Return False
            End If
        End If

        If tbxUser.Text <> "" Then
            intDataCount = ShowPop(KeikakuKanriRecord.popKbn.User, False)
            If intDataCount = 0 Then
                ShowMessage(String.Format(CommonMessage.MSG022E, "営業マン"), tbxUser.ClientID)
                Return False

            ElseIf intDataCount > 1 Then
                ShowMessage(String.Format(CommonMessage.MSG062E, "営業マン"), tbxUser.ClientID)
                Return False
            End If
        End If

        If tbxKameiten.Text <> "" Then
            intDataCount = ShowPop(KeikakuKanriRecord.popKbn.Kameiten, False)
            If intDataCount = 0 Then
                ShowMessage(String.Format(CommonMessage.MSG022E, "登録事業者"), tbxKameiten.ClientID)
                Return False

            ElseIf intDataCount > 1 Then
                ShowMessage(String.Format(CommonMessage.MSG062E, "登録事業者"), tbxKameiten.ClientID)
                Return False
            End If
        End If

        If tbxEigyou.Text <> "" Then
            intDataCount = ShowPop(KeikakuKanriRecord.popKbn.Eigyou, False)
            If intDataCount = 0 Then
                ShowMessage(String.Format(CommonMessage.MSG022E, "営業所"), tbxEigyou.ClientID)
                Return False

            ElseIf intDataCount > 1 Then
                ShowMessage(String.Format(CommonMessage.MSG062E, "営業所"), tbxEigyou.ClientID)
                Return False
            End If
        End If

        '統一法人
        If Me.tbxTouituHoujin.Text <> "" Then
            intDataCount = ShowPop(KeikakuKanriRecord.popKbn.TouituHoujin, False)
            If intDataCount = 0 Then
                ShowMessage(String.Format(CommonMessage.MSG022E, "統一法人"), tbxTouituHoujin.ClientID)
                Return False

            ElseIf intDataCount > 1 Then
                ShowMessage(String.Format(CommonMessage.MSG062E, "統一法人"), tbxTouituHoujin.ClientID)
                Return False
            End If
        End If

        '法人
        If Me.tbxHoujin.Text <> "" Then
            intDataCount = ShowPop(KeikakuKanriRecord.popKbn.Houjin, False)
            If intDataCount = 0 Then
                ShowMessage(String.Format(CommonMessage.MSG022E, "法人"), tbxHoujin.ClientID)
                Return False

            ElseIf intDataCount > 1 Then
                ShowMessage(String.Format(CommonMessage.MSG062E, "法人"), tbxHoujin.ClientID)
                Return False
            End If
        End If



        If Me.tbxSiten.Text.Trim.Equals(String.Empty) _
           AndAlso Me.tbxUser.Text.Trim.Equals(String.Empty) _
           AndAlso Me.tbxKameiten.Text.Trim.Equals(String.Empty) _
           AndAlso Me.tbxEigyou.Text.Trim.Equals(String.Empty) _
           AndAlso Me.ddlSyozoku.SelectedValue.Trim.Equals(String.Empty) _
           AndAlso Me.ddlTodoufuken.SelectedValue.Trim.Equals(String.Empty) _
           AndAlso Me.tbxTouituHoujin.Text.Trim.Equals(String.Empty) _
           AndAlso Me.tbxHoujin.Text.Trim.Equals(String.Empty) _
           AndAlso Me.ddlZokusei1.SelectedValue.Trim.Equals(String.Empty) _
           AndAlso Me.ddlZokusei2.SelectedValue.Trim.Equals(String.Empty) _
           AndAlso Me.ddlZokusei3.SelectedValue.Trim.Equals(String.Empty) _
           AndAlso Me.ddlZokusei4.SelectedValue.Trim.Equals(String.Empty) _
           AndAlso Me.ddlZokusei5.SelectedValue.Trim.Equals(String.Empty) _
           AndAlso Me.tbxZokusei6.Text.Trim.Equals(String.Empty) Then

            ShowMessage(String.Format(Messages.MSG055E, "条件"), tbxSiten.ClientID)
            Return False
        End If

        '「営業区分」
        If (Not Me.chkEigyou.Checked) AndAlso (Not Me.chkEigyouNew.Checked) AndAlso (Not Me.chkTokuhan.Checked) AndAlso (Not Me.chkTokuhanNew.Checked) Then
            ShowMessage(String.Format(Messages.MSG055E, "営業・営業(新規)・特販・特販(新規)"), Me.chkEigyou.ClientID)
            Return False
        End If

        '「表示欄選択」
        If (Not Me.chkZennenDougetu.Checked) AndAlso (Not Me.chkKeikaku.Checked) AndAlso (Not Me.chkMikomi.Checked) AndAlso (Not Me.chkJisseki.Checked) AndAlso (Not Me.chkTassei.Checked) AndAlso (Not Me.chkSintyoku.Checked) Then
            ShowMessage(Messages.MSG085E, Me.chkZennenDougetu.ClientID)
            Return False
        End If

        Dim commonCheck As New CommonCheck
        Dim strErrorMessage As String

        '「属性6」
        If Not Me.tbxZokusei6.Text.Trim.Equals(String.Empty) Then

            '   禁止文字チェック
            If Not commonCheck.kinsiStrCheck(Me.tbxZokusei6.Text.Trim) Then
                Call Me.ShowMessage(String.Format(Messages.MSG033E, "属性6"), Me.tbxZokusei6.ClientID)
                Return False
            End If

            '   バイト数チェック
            strErrorMessage = commonCheck.CheckByte(Me.tbxZokusei6.Text.Trim, 40, "属性6", kbn.ZENKAKU)
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.ShowMessage(String.Format(Messages.MSG073E, "属性6"), Me.tbxZokusei6.ClientID)
                Return False
            End If
        End If

        '   「年間棟数(FROM)」
        If Not Me.tbxNenkanTousuuFrom.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.CheckHankaku(Me.tbxNenkanTousuuFrom.Text.Trim, "年間棟数", "1")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.ShowMessage(strErrorMessage, Me.tbxNenkanTousuuFrom.ClientID)
                Return False
            End If
        End If

        '   「年間棟数(TO)」
        If Not Me.tbxNenkanTousuuTo.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.CheckHankaku(Me.tbxNenkanTousuuTo.Text.Trim, "年間棟数", "1")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.ShowMessage(strErrorMessage, Me.tbxNenkanTousuuTo.ClientID)
                Return False
            End If

            '   範囲チェック
            If Me.tbxNenkanTousuuFrom.Text.Trim.Equals(String.Empty) Then
                '   加盟店(FROM)が未入力の場合
                Call Me.ShowMessage(String.Format(Messages.MSG074E, "年間棟数(From)"), Me.tbxNenkanTousuuFrom.ClientID)
                Return False
            Else
                If Convert.ToInt32(Me.tbxNenkanTousuuFrom.Text.Trim) > Convert.ToInt32(Me.tbxNenkanTousuuTo.Text.Trim) Then
                    '   年間棟数(FROM) > 年間棟数(TO)の場合
                    Call Me.ShowMessage(String.Format(Messages.MSG024E, "年間棟数", "年間棟数"), Me.tbxNenkanTousuuTo.ClientID)
                    Return False
                End If
            End If
        End If


        Return True
    End Function
    ''' <summary>メッセージ表示</summary>
    ''' <param name="strMessage">メッセージ</param>
    ''' <param name="strObjId">クライアントID</param>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	window.name = 'EigyouKeikakuKanriMenu.aspx';setMenuBgColor();")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("  try{ document.getElementById('" & strObjId & "').select();}catch(e){}")
            End If
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub
    Private Function SetKensaku(ByVal blnCsv As Boolean) As KeikakuKanriRecord
        Dim KeikakuKanriRecord As New KeikakuKanriRecord

        Dim TaisyouKikan As New KeikakuKanriRecord.taisyouKikan
        '営業区分
        Dim EigyouKBNjyouken As New EigyouJyouken
        '絞込み選択
        Dim SiborikomiJyouken As New SiborikomiJyouken
        '表示欄選択
        Dim HyoujiSentakuJyouken As New HyoujiSentakuJyouken

        '年間棟数範囲
        Dim NenkanTousuuHaniJyouken As New NenkanTousuuHaniJyouken


        With KeikakuKanriRecord
            .Nendo = ddlKeikaku.SelectedValue '年度
            .Shiten = hidSitenCd.Value '支店
            .User = hidUser.Value '営業マン
            .Kameiten = hidKameiten.Value '登録事業所
            .ShitenMei = tbxSiten.Text '支店名
            .Eigyou = hidEigyou.Value '営業所

            .Syozoku = Me.ddlSyozoku.SelectedValue '所属
            .Todoufuken = Me.ddlTodoufuken.SelectedValue '都道府県
            .TouituHoujin = Me.hidTouituHoujin.Value '統一法人
            .Houjin = Me.hidHoujin.Value '法人
            .Zokusei1 = Me.ddlZokusei1.SelectedValue '属性1
            .Zokusei2 = Me.ddlZokusei2.SelectedValue '属性2
            .Zokusei3 = Me.ddlZokusei3.SelectedValue '属性3
            .Zokusei4 = Me.ddlZokusei4.SelectedValue '属性4
            .Zokusei5 = Me.ddlZokusei5.SelectedValue '属性5
            .Zokusei6 = Me.tbxZokusei6.Text.Trim  '属性6

            '表示対象期間
            .Taisyou = KeikakuKanriRecord.taisyouKikan.Nenkan
            If chkKongetu.Checked Then
                .Taisyou = KeikakuKanriRecord.taisyouKikan.Kongetu
            End If
            If chkSangetu.Checked Then
                .Taisyou = KeikakuKanriRecord.taisyouKikan.Sangetu
            End If
            If chkYogetu.Checked Then
                .Taisyou = KeikakuKanriRecord.taisyouKikan.Yogetu
            End If

            '営業区分
            If chkEigyou.Checked Then
                EigyouKBNjyouken.Eigyou = True '営業
            End If
            If chkTokuhan.Checked Then
                EigyouKBNjyouken.Tokuhan = True '特販
            End If
            If chkSinki.Checked Then
                EigyouKBNjyouken.Sinki = True '新規
            End If
            If chkFC.Checked Then
                EigyouKBNjyouken.FC = True 'FC
            End If

            If Me.chkEigyouNew.Checked Then
                EigyouKBNjyouken.EigyouNew = True '営業(新規)
            End If
            If Me.chkTokuhanNew.Checked Then
                EigyouKBNjyouken.TokuhanNew = True '特販(新規)
            End If
            If Me.chkFCNew.Checked Then
                EigyouKBNjyouken.FCNew = True 'FC(新規)
            End If

            .EigyouKBN = EigyouKBNjyouken

            '絞込み選択
            If chkKeikakuTi.Checked Then
                SiborikomiJyouken.KeikakuTi = True
            End If
            If chkSinkiTouroku.Checked Then
                SiborikomiJyouken.SinkiTouroku = True
            End If
            If chkBunjyou.Checked Then
                SiborikomiJyouken.Bunjyou = True
            End If
            If chkTyumon.Checked Then
                SiborikomiJyouken.Tyumon = True
            End If
            If chkNenkanTouSuu.Checked Then
                SiborikomiJyouken.NenkanTouSuu = True
            End If
            .Siborikomi = SiborikomiJyouken

            '年間棟数範囲
            NenkanTousuuHaniJyouken.Keikakuyou = Me.chkKeikakuyou.Checked '計画用
            NenkanTousuuHaniJyouken.NenkanTouSuuFrom = Me.tbxNenkanTousuuFrom.Text.Trim  '年間棟数From
            NenkanTousuuHaniJyouken.NenkanTouSuuTo = Me.tbxNenkanTousuuTo.Text.Trim  '年間棟数To
            .NenkanTousuuHani = NenkanTousuuHaniJyouken

            '表示欄選択
            If Me.chkZennenDougetu.Checked Then
                HyoujiSentakuJyouken.ZennenDougetu = True '前年同月
            End If
            If chkKeikaku.Checked Then
                HyoujiSentakuJyouken.Keikaku = True
            End If
            If chkMikomi.Checked Then
                HyoujiSentakuJyouken.Mikomi = True
            End If
            If chkJisseki.Checked Then
                HyoujiSentakuJyouken.Jisseki = True
            End If
            If chkTassei.Checked Then
                HyoujiSentakuJyouken.Tassei = True
            End If
            If chkSintyoku.Checked Then
                HyoujiSentakuJyouken.Sintyoku = True
            End If

            .HyoujiSentaku = HyoujiSentakuJyouken


            'If Not blnCsv Then
            '    .Kensuu = ddlKensuu.SelectedValue
            'Else
            '    .Kensuu = ""
            'End If
            .Kensuu = ""
        End With

        Return KeikakuKanriRecord
    End Function
    ''' <summary>
    ''' JavaScriptを作成する
    ''' </summary>
    ''' <remarks>2012/11/28 高</remarks>
    Private Sub MakeJavaScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("function fncHyoujiCheck(objID) ")
            .AppendLine("{ ")
            .AppendLine("   if (objID.id=='" & chkKongetu.ClientID & "') ")
            .AppendLine("   {")
            .AppendLine("       document.all." & chkSangetu.ClientID & ".checked=false; ")
            .AppendLine("       document.all." & chkYogetu.ClientID & ".checked=false; ")
            '.AppendLine("       document.all." & chkMikomi.ClientID & ".checked=false; ")
            .AppendLine("   } ")
            .AppendLine("   if (objID.id=='" & chkSangetu.ClientID & "') ")
            .AppendLine("   {")
            .AppendLine("       document.all." & chkKongetu.ClientID & ".checked=false; ")
            .AppendLine("       document.all." & chkYogetu.ClientID & ".checked=false;")
            '.AppendLine("       document.all." & chkMikomi.ClientID & ".checked=false;")
            .AppendLine("   } ")
            .AppendLine("   if (objID.id=='" & chkYogetu.ClientID & "') ")
            .AppendLine("   {")
            .AppendLine("       document.all." & chkKongetu.ClientID & ".checked=false; ")
            .AppendLine("       document.all." & chkSangetu.ClientID & ".checked=false; ")
            '.AppendLine("       document.all." & chkMikomi.ClientID & ".checked=document.all." & chkYogetu.ClientID & ".checked; ")
            .AppendLine("   } ")
            .AppendLine("  if (document.all." & ddlKeikaku.ClientID & ".value!='" & strSysNen & "' && (document.all." & chkSangetu.ClientID & ".checked==true || document.all." & chkKongetu.ClientID & ".checked==true || document.all." & chkYogetu.ClientID & ".checked==true)) {")
            .AppendLine("   alert('" & Messages.MSG047E & "');document.all." & ddlKeikaku.ClientID & ".focus();} ")
            .AppendLine("} ")
            .AppendLine("function fncClear() ")
            .AppendLine("{ ")
            .AppendLine("   document.all." & ddlKeikaku.ClientID & ".value='" & strSysNen.ToString & "'; ")
            .AppendLine("   document.all." & tbxSiten.ClientID & ".value=''; ")
            .AppendLine("   document.all." & Me.hidSitenCd.ClientID & ".value=''; ") '支店(hidden)
            .AppendLine("   document.all." & tbxUser.ClientID & ".value=''; ")
            .AppendLine("   document.all." & Me.hidUser.ClientID & ".value=''; ") '営業マン(hidden)
            .AppendLine("   document.all." & tbxKameiten.ClientID & ".value=''; ")
            .AppendLine("   document.all." & Me.hidKameiten.ClientID & ".value=''; ") '登録事業者(hidden)
            '  .AppendLine("   document.all." & tbxKeiretu.ClientID & ".value=''; ")
            .AppendLine("   document.all." & tbxEigyou.ClientID & ".value=''; ")
            .AppendLine("   document.all." & Me.hidEigyou.ClientID & ".value=''; ") '営業所(hidden)
            .AppendLine("   document.all." & chkKongetu.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkSangetu.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkYogetu.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkEigyou.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & Me.chkEigyouNew.ClientID & ".checked=false; ") '営業(新規)
            .AppendLine("   document.all." & chkTokuhan.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & Me.chkTokuhanNew.ClientID & ".checked=false; ") '特販(新規)
            '.AppendLine("   document.all." & chkSinki.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkFC.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & Me.chkFCNew.ClientID & ".checked=false; ") 'FC(新規)
            .AppendLine("   document.all." & chkKeikakuTi.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkSinkiTouroku.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkBunjyou.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkNenkanTouSuu.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkKeikaku.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkMikomi.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkJisseki.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkTassei.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkSintyoku.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & chkTyumon.ClientID & ".checked=false; ")
            .AppendLine("   document.all." & ddlKensuu.ClientID & ".value='" & ViewState("Kensuu").ToString & "'; ")

            .AppendLine("   document.all." & Me.ddlSyozoku.ClientID & ".value=''; ") '所属
            .AppendLine("   document.all." & Me.ddlTodoufuken.ClientID & ".value=''; ") '都道府県
            .AppendLine("   document.all." & Me.tbxTouituHoujin.ClientID & ".value=''; ") '統一法人
            .AppendLine("   document.all." & Me.hidTouituHoujin.ClientID & ".value=''; ") '統一法人(hidden)
            .AppendLine("   document.all." & Me.tbxHoujin.ClientID & ".value=''; ") '法人
            .AppendLine("   document.all." & Me.hidHoujin.ClientID & ".value=''; ") '法人(hidden)
            .AppendLine("   document.all." & Me.ddlZokusei1.ClientID & ".value=''; ") '属性1
            .AppendLine("   document.all." & Me.ddlZokusei2.ClientID & ".value=''; ") '属性2
            .AppendLine("   document.all." & Me.ddlZokusei3.ClientID & ".value=''; ") '属性3
            .AppendLine("   document.all." & Me.ddlZokusei4.ClientID & ".value=''; ") '属性4
            .AppendLine("   document.all." & Me.ddlZokusei5.ClientID & ".value=''; ") '属性5
            .AppendLine("   document.all." & Me.tbxZokusei6.ClientID & ".value=''; ") '属性6

            '年間棟数範囲
            .AppendLine("   document.all." & Me.chkKeikakuyou.ClientID & ".checked=false; ") '計画用
            .AppendLine("   document.all." & Me.tbxNenkanTousuuFrom.ClientID & ".value=''; ") '年間棟数From
            .AppendLine("   document.all." & Me.tbxNenkanTousuuTo.ClientID & ".value=''; ") '年間棟数To

            .AppendLine("   document.all." & Me.chkZennenDougetu.ClientID & ".checked=false; ") '前年同月

            .AppendLine("   } ")
            .AppendLine("   function fncScrollV(){")
            .AppendLine("       var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("       divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("       divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   }")
            .AppendLine("   function fncScrollH(){")
            .AppendLine("       var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("       divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("       divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function wheel(event){")
            .AppendLine("       var delta = 0;")
            .AppendLine("       if(!event)")
            .AppendLine("           event = window.event;")
            .AppendLine("       if (event.wheelDelta){")
            .AppendLine("           delta = event.wheelDelta/120;")
            .AppendLine("           if (window.opera)")
            .AppendLine("               delta = -delta;")
            .AppendLine("       } else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("       if (delta)")
            .AppendLine("           handle(delta);")
            .AppendLine("   }")
            .AppendLine("   function handle(delta){")
            .AppendLine("      var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("      if (delta < 0){")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("      }else{")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("      }")
            .AppendLine("   }")

            .AppendLine("</script>  ")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub
    Private Sub SetJsEvent()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        chkKongetu.Attributes("onclick") = "fncHyoujiCheck(this);"
        chkSangetu.Attributes("onclick") = "fncHyoujiCheck(this);"
        chkYogetu.Attributes("onclick") = "fncHyoujiCheck(this);"
        btnClear.Button.Attributes("onclick") = "fncClear();return false;"
        btnTorikomi1.Button.Attributes("onclick") = "window.open('KeikakuKanriInput.aspx?csvKbn=B1', 'PopupCSVInput');return false;"
        btnTorikomi2.Button.Attributes("onclick") = "window.open('KeikakuKanriInput.aspx?csvKbn=B2', 'PopupCSVInput');return false;"
        btnTorikomi3.Button.Attributes("onclick") = "window.open('KeikakuKanriInput.aspx?csvKbn=B3', 'PopupCSVInput');return false;"
        btnTorikomi4.Button.Attributes("onclick") = "window.open('KeikakuKanriInput.aspx?csvKbn=B4', 'PopupCSVInput');return false;"

        Me.tbxSiten.Attributes("onfocusout") = "if (this.value ==''){$ID('" & Me.hidSitenCd.ClientID & "').value='';}"
        Me.tbxUser.Attributes("onfocusout") = "if (this.value ==''){$ID('" & Me.hidUser.ClientID & "').value='';}"
        Me.tbxKameiten.Attributes("onfocusout") = "if (this.value ==''){$ID('" & Me.hidKameiten.ClientID & "').value='';}"
        Me.tbxEigyou.Attributes("onfocusout") = "if (this.value ==''){$ID('" & Me.hidEigyou.ClientID & "').value='';}"

        Me.tbxTouituHoujin.Attributes("onfocusout") = "if (this.value ==''){$ID('" & Me.hidTouituHoujin.ClientID & "').value='';}" '統一法人
        Me.tbxHoujin.Attributes("onfocusout") = "if (this.value ==''){$ID('" & Me.hidHoujin.ClientID & "').value='';}" '法人

    End Sub
    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <remarks>2012/11/28 高</remarks>
    Private Sub PageInit()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        Dim CommonBC As New CommonBC
        Dim dtKeikaku As Data.DataTable = CommonBC.GetKeikakuNendoData
        ddlKeikaku.DataSource = dtKeikaku
        ddlKeikaku.DataValueField = "code"
        ddlKeikaku.DataTextField = "meisyou"
        ddlKeikaku.DataBind()
        ddlKeikaku.Items.Insert(0, "")


        '「営業区分」
        chkEigyou.Checked = True '営業
        chkTokuhan.Checked = True '特販
        'chkSinki.Checked = True '新規
        Me.chkEigyouNew.Checked = True '営業(新規)
        Me.chkTokuhanNew.Checked = True '特販(新規)

        '「表示欄選択」
        Me.chkZennenDougetu.Checked = True '前年同月
        Me.chkKeikaku.Checked = True '計画
        Me.chkJisseki.Checked = True '実績



        'data of dropdownlist
        Dim dtDdlData As Data.DataTable

        '所属
        Me.ddlSyozoku.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKakutyouMeisyouInfo("30")

        Me.ddlSyozoku.DataValueField = "code"
        Me.ddlSyozoku.DataTextField = "meisyou"
        Me.ddlSyozoku.DataSource = dtDdlData
        Me.ddlSyozoku.DataBind()
        Me.ddlSyozoku.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '都道府県 
        Me.ddlTodoufuken.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetTodoufukenInfo()

        Me.ddlTodoufuken.DataValueField = "cd"
        Me.ddlTodoufuken.DataTextField = "mei"
        Me.ddlTodoufuken.DataSource = dtDdlData
        Me.ddlTodoufuken.DataBind()
        Me.ddlTodoufuken.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '属性1  
        Me.ddlZokusei1.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("21")
        Me.ddlZokusei1.DataValueField = "code"
        Me.ddlZokusei1.DataTextField = "meisyou"
        Me.ddlZokusei1.DataSource = dtDdlData
        Me.ddlZokusei1.DataBind()
        Me.ddlZokusei1.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '属性2 
        Me.ddlZokusei2.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("22")
        Me.ddlZokusei2.DataValueField = "code"
        Me.ddlZokusei2.DataTextField = "meisyou"
        Me.ddlZokusei2.DataSource = dtDdlData
        Me.ddlZokusei2.DataBind()
        Me.ddlZokusei2.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '属性3 
        Me.ddlZokusei3.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("23")
        Me.ddlZokusei3.DataValueField = "code"
        Me.ddlZokusei3.DataTextField = "meisyou"
        Me.ddlZokusei3.DataSource = dtDdlData
        Me.ddlZokusei3.DataBind()
        Me.ddlZokusei3.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '属性4
        Me.ddlZokusei4.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKakutyouMeisyouInfo("21")
        Me.ddlZokusei4.DataValueField = "code"
        Me.ddlZokusei4.DataTextField = "meisyou"
        Me.ddlZokusei4.DataSource = dtDdlData
        Me.ddlZokusei4.DataBind()
        Me.ddlZokusei4.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '属性5
        Me.ddlZokusei5.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKakutyouMeisyouInfo("22")
        Me.ddlZokusei5.DataValueField = "code"
        Me.ddlZokusei5.DataTextField = "meisyou"
        Me.ddlZokusei5.DataSource = dtDdlData
        Me.ddlZokusei5.DataBind()
        Me.ddlZokusei5.Items.Insert(0, New ListItem(String.Empty, String.Empty))


        Dim arrKensuu() As String = Split(System.Configuration.ConfigurationManager.AppSettings("Kensuu"), ",")
        Dim item As New ListItem
        For intCount As Integer = 0 To arrKensuu.Length - 1
            item = New ListItem
            item.Text = arrKensuu(intCount)
            item.Value = arrKensuu(intCount)
            ddlKensuu.Items.Add(item)
        Next

        ddlKensuu.Items.Insert(0, "")
        If arrKensuu.Length - 1 >= 0 Then
            ddlKensuu.SelectedIndex = 1
        Else
            ddlKensuu.SelectedIndex = 0
        End If
        ViewState("Kensuu") = ddlKensuu.SelectedIndex
        gridviewLeft.Visible = False
        btnMinaosi.Button.Enabled = False
        btnKakunin.Button.Enabled = False
        If ViewState("btnTorikomi").ToString = "" Then
            btnTorikomi1.Button.Enabled = False
            btnTorikomi2.Button.Enabled = False
            btnTorikomi3.Button.Enabled = False
            btnTorikomi4.Button.Enabled = False
        End If

        btnSyuturyoku.Button.Enabled = False
        ddlKeikaku.SelectedValue = strSysNen.ToString
        lnkAto.Visible = False
        linMae.Visible = False
    End Sub
    ''' <summary>
    ''' 共通POPUP
    ''' </summary>
    ''' <param name="popKbn"></param>
    ''' <remarks>2012/11/28 高</remarks>
    Private Function ShowPop(ByVal popKbn As KeikakuKanriRecord.popKbn, ByVal blnPop As Boolean) As Integer
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, popKbn)
        Dim csScript As New StringBuilder

        Select Case popKbn
            Case KeikakuKanriRecord.popKbn.Shiten '支店
                Dim SitenSearchBC As New SitenSearchBC
                Dim dtSiten As New Data.DataTable
                If blnPop Then
                    dtSiten = SitenSearchBC.GetBusyoKanri("0", tbxSiten.Text, False)
                Else
                    dtSiten = SitenSearchBC.GetBusyoKanri("0", tbxSiten.Text, False, False)
                End If
                ShowPop = dtSiten.Rows.Count

                If dtSiten.Rows.Count = 1 Then
                    Me.tbxSiten.Text = dtSiten.Rows(0).Item(0).ToString  '支店名
                    Me.hidSitenCd.Value = dtSiten.Rows(0).Item(1).ToString     '支店コード
                Else
                    If blnPop Then
                        'popUp検索の場合
                        csScript.AppendLine("window.open('./PopupSearch/SitenSearch.aspx?formName=" & Me.Form.ClientID & "&strSitenMei='+ escape($ID('" & Me.tbxSiten.ClientID & "').value)+'&field=" & Me.tbxSiten.ClientID & "'+'&fieldCd=" & Me.hidSitenCd.ClientID & "', 'SitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
                        'ページ応答で、クライアント側のスクリプト ブロックを出力します
                        ClientScript.RegisterStartupScript(Me.GetType(), popKbn.ToString, csScript.ToString, True)
                    Else
                        '複数の場合
                        If ShowPop > 1 Then
                            If Me.hidSitenCd.Value.ToString.Trim <> String.Empty Then
                                For i As Integer = 0 To dtSiten.Rows.Count - 1
                                    If Me.hidSitenCd.Value.ToString.Trim = dtSiten.Rows(i).Item(1).ToString() Then
                                        ShowPop = 1
                                        Exit For
                                    End If
                                Next

                            End If
                        End If

                    End If
                End If
            Case KeikakuKanriRecord.popKbn.User '営業マン

                Dim SitenSearchBC As New EigyouManSearchBC
                Dim dtUserInfo As New Data.DataTable

                If blnPop Then
                    dtUserInfo = SitenSearchBC.GetUserInfo("0", "", tbxUser.Text, False)
                Else
                    dtUserInfo = SitenSearchBC.GetUserInfo("0", "", tbxUser.Text, False, False)
                End If
                ShowPop = dtUserInfo.Rows.Count

                If dtUserInfo.Rows.Count = 1 Then
                    Me.tbxUser.Text = dtUserInfo.Rows(0).Item(1).ToString
                    Me.hidUser.Value = dtUserInfo.Rows(0).Item(0).ToString
                Else
                    If blnPop Then
                        'popUp検索の場合
                        csScript.AppendLine("window.open('./PopupSearch/EigyouManSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyouManMei='+ escape($ID('" & Me.tbxUser.ClientID & "').value)+'&field=" & Me.tbxUser.ClientID & "'+'&fieldCd=" & Me.hidUser.ClientID & "', 'EigyouManSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
                        'ページ応答で、クライアント側のスクリプト ブロックを出力します
                        ClientScript.RegisterStartupScript(Me.GetType(), popKbn.ToString, csScript.ToString, True)
                    Else
                        '複数の場合
                        If ShowPop > 1 Then
                            If Me.hidUser.Value.ToString.Trim <> String.Empty Then
                                For i As Integer = 0 To dtUserInfo.Rows.Count - 1
                                    If Me.hidUser.Value.ToString.Trim = dtUserInfo.Rows(i).Item(0).ToString() Then
                                        ShowPop = 1
                                        Exit For
                                    End If
                                Next

                            End If
                        End If

                    End If
                End If

            Case KeikakuKanriRecord.popKbn.Keiretu '系列
                'Dim KeiretuSearchBC As New KeiretuSearchBC
                'Dim dtKeiretu As Data.DataTable = KeiretuSearchBC.GetKiretuJyouhou("0", tbxKeiretu.Text, "", False)
                'If dtKeiretu.Rows.Count = 1 Then
                '    tbxKeiretu.Text = dtKeiretu.Rows(0).Item(0).ToString
                '    ShowPop = True
                'Else
                '    If blnPop Or (blnPop = False And tbxKeiretu.Text <> "") Then
                '        csScript.AppendLine("window.open('./PopupSearch/KeiretuSearch.aspx?formName=" & Me.Form.ClientID & "&strKeiretuCd='+ escape($ID('" & Me.tbxKeiretu.ClientID & "').value) +'&field=" & Me.tbxKeiretu.ClientID & "', 'KeiretuSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
                '    Else
                '        ShowPop = True
                '    End If
                'End If
            Case KeikakuKanriRecord.popKbn.Kameiten '登録事業者
                Dim TourokuJigyousyaSearchBC As New TourokuJigyousyaSearchBC
                Dim dtTourokuJigyousya As New Data.DataTable

                If blnPop Then
                    dtTourokuJigyousya = TourokuJigyousyaSearchBC.GetTourokuJigyousya("0", "", tbxKameiten.Text, False)
                Else
                    dtTourokuJigyousya = TourokuJigyousyaSearchBC.GetTourokuJigyousya("0", "", tbxKameiten.Text, False, False)
                End If
                ShowPop = dtTourokuJigyousya.Rows.Count
                If dtTourokuJigyousya.Rows.Count = 1 Then
                    Me.tbxKameiten.Text = dtTourokuJigyousya.Rows(0).Item(1).ToString  '加盟店名
                    Me.hidKameiten.Value = dtTourokuJigyousya.Rows(0).Item(0).ToString     '加盟店コード
                Else
                    If blnPop Then
                        'popUp検索の場合
                        csScript.AppendLine("window.open('./PopupSearch/TourokuJigyousyaSearch.aspx?formName=" & Me.Form.ClientID & "&strTourokuJigyousya='+ escape($ID('" & Me.tbxKameiten.ClientID & "').value)+'&field=" & Me.tbxKameiten.ClientID & "'+'&fieldCd=" & Me.hidKameiten.ClientID & "', 'TourokuJigyousya', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=900,height=500,top=30,left=0');")
                        'ページ応答で、クライアント側のスクリプト ブロックを出力します
                        ClientScript.RegisterStartupScript(Me.GetType(), popKbn.ToString, csScript.ToString, True)
                    Else
                        '複数の場合
                        If ShowPop > 1 Then
                            If Me.hidKameiten.Value.ToString.Trim <> String.Empty Then
                                For i As Integer = 0 To dtTourokuJigyousya.Rows.Count - 1
                                    If Me.hidKameiten.Value.ToString.Trim = dtTourokuJigyousya.Rows(i).Item(0).ToString() Then
                                        ShowPop = 1
                                        Exit For
                                    End If
                                Next

                            End If
                        End If

                    End If
                End If
            Case KeikakuKanriRecord.popKbn.Eigyou '営業所
                Dim EigyousyoSearchBC As New EigyousyoSearchBC
                Dim dtEigyousyo As New Data.DataTable
                If blnPop Then
                    dtEigyousyo = EigyousyoSearchBC.GetEigyousyoMei("0", tbxEigyou.Text, False)
                Else
                    dtEigyousyo = EigyousyoSearchBC.GetEigyousyoMei("0", tbxEigyou.Text, False, False)
                End If
                ShowPop = dtEigyousyo.Rows.Count
                If dtEigyousyo.Rows.Count = 1 Then
                    Me.tbxEigyou.Text = dtEigyousyo.Rows(0).Item(0).ToString  '支店名
                    Me.hidEigyou.Value = dtEigyousyo.Rows(0).Item(1).ToString     '支店コード
                Else
                    If blnPop Then
                        'popUp検索の場合
                        csScript.AppendLine("window.open('./PopupSearch/EigyousyoSearch.aspx?formName=" & Me.Form.ClientID & "&strSitenMei='+ escape($ID('" & Me.tbxEigyou.ClientID & "').value)+'&field=" & Me.tbxEigyou.ClientID & "'+'&fieldCd=" & Me.hidEigyou.ClientID & "', 'EigyousyoSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
                        'ページ応答で、クライアント側のスクリプト ブロックを出力します
                        ClientScript.RegisterStartupScript(Me.GetType(), popKbn.ToString, csScript.ToString, True)
                    Else
                        '複数の場合
                        If ShowPop > 1 Then
                            If Me.hidEigyou.Value.ToString.Trim <> String.Empty Then
                                For i As Integer = 0 To dtEigyousyo.Rows.Count - 1
                                    If Me.hidEigyou.Value.ToString.Trim = dtEigyousyo.Rows(i).Item(1).ToString() Then
                                        ShowPop = 1
                                        Exit For
                                    End If
                                Next

                            End If
                        End If

                    End If
                End If
            Case KeikakuKanriRecord.popKbn.TouituHoujin '統一法人
                Dim keikakuKanriKameitenSearchBC As New KeikakuKanriKameitenSearchBC
                Dim dtTouituHoujin As New Data.DataTable
                If blnPop Then
                    dtTouituHoujin = keikakuKanriKameitenSearchBC.GetKeikakuKanriKameiten("", Me.ddlKeikaku.SelectedValue, String.Empty, Me.tbxTouituHoujin.Text.Trim, False)
                Else
                    dtTouituHoujin = keikakuKanriKameitenSearchBC.GetKeikakuKanriKameiten("", Me.ddlKeikaku.SelectedValue, String.Empty, Me.tbxTouituHoujin.Text.Trim, False, False)
                End If
                ShowPop = dtTouituHoujin.Rows.Count
                If dtTouituHoujin.Rows.Count = 1 Then
                    Me.tbxTouituHoujin.Text = dtTouituHoujin.Rows(0).Item("kameiten_mei").ToString() '統一法人
                    Me.hidTouituHoujin.Value = dtTouituHoujin.Rows(0).Item("kameiten_cd").ToString() '統一法人hidden
                Else
                    If blnPop Then
                        'popUp検索の場合
                        csScript.AppendLine("window.open('./PopupSearch/KeikakuKanriKameitenSearch.aspx?formName=aspnetForm&strKameitenCdValue='+ escape('')+'&strKameitenCdId='+ '" & Me.hidTouituHoujin.ClientID & "' +'&strKameitenMeiValue='+ escape($ID('" & Me.tbxTouituHoujin.ClientID & "').value) +'&strKameitenMeiId=' + '" & Me.tbxTouituHoujin.ClientID & "' +'&strYear='+ '" & Me.ddlKeikaku.SelectedValue & "', 'TouituHoujinSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
                        'ページ応答で、クライアント側のスクリプト ブロックを出力します
                        ClientScript.RegisterStartupScript(Me.GetType(), popKbn.ToString, csScript.ToString, True)
                    Else
                        '複数の場合
                        If ShowPop > 1 Then
                            If Me.hidTouituHoujin.Value.ToString.Trim <> String.Empty Then
                                For i As Integer = 0 To dtTouituHoujin.Rows.Count - 1
                                    If Me.hidTouituHoujin.Value.ToString.Trim = dtTouituHoujin.Rows(i).Item("kameiten_cd").ToString() Then
                                        ShowPop = 1
                                        Exit For
                                    End If
                                Next

                            End If
                        End If
                    End If
                End If

            Case KeikakuKanriRecord.popKbn.Houjin '法人
                Dim keikakuKanriKameitenSearchBC As New KeikakuKanriKameitenSearchBC
                Dim dtHoujin As New Data.DataTable
                If blnPop Then
                    dtHoujin = keikakuKanriKameitenSearchBC.GetKeikakuKanriKameiten("", Me.ddlKeikaku.SelectedValue, String.Empty, Me.tbxHoujin.Text.Trim, False)
                Else
                    dtHoujin = keikakuKanriKameitenSearchBC.GetKeikakuKanriKameiten("", Me.ddlKeikaku.SelectedValue, String.Empty, Me.tbxHoujin.Text.Trim, False, False)
                End If
                ShowPop = dtHoujin.Rows.Count
                If dtHoujin.Rows.Count = 1 Then
                    Me.tbxHoujin.Text = dtHoujin.Rows(0).Item("kameiten_mei").ToString() '法人
                    Me.hidHoujin.Value = dtHoujin.Rows(0).Item("kameiten_cd").ToString() '法人hidden
                Else
                    If blnPop Then
                        'popUp検索の場合
                        csScript.AppendLine("window.open('./PopupSearch/KeikakuKanriKameitenSearch.aspx?formName=aspnetForm&strKameitenCdValue='+ escape('')+'&strKameitenCdId='+ '" & Me.hidHoujin.ClientID & "' +'&strKameitenMeiValue='+ escape($ID('" & Me.tbxHoujin.ClientID & "').value) +'&strKameitenMeiId=' + '" & Me.tbxHoujin.ClientID & "' +'&strYear='+ '" & Me.ddlKeikaku.SelectedValue & "', 'HoujinSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
                        'ページ応答で、クライアント側のスクリプト ブロックを出力します
                        ClientScript.RegisterStartupScript(Me.GetType(), popKbn.ToString, csScript.ToString, True)
                    Else
                        '複数の場合
                        If ShowPop > 1 Then
                            If Me.hidHoujin.Value.ToString.Trim <> String.Empty Then
                                For i As Integer = 0 To dtHoujin.Rows.Count - 1
                                    If Me.hidHoujin.Value.ToString.Trim = dtHoujin.Rows(i).Item("kameiten_cd").ToString() Then
                                        ShowPop = 1
                                        Exit For
                                    End If
                                Next

                            End If
                        End If
                    End If
                End If


        End Select
        If grdItiranRight.Rows.Count > 0 AndAlso grdItiranRight.Visible Then
            gridviewRightId = grdItiranRight.ClientID
            tblRightId = scrollV.ClientID
            tblLeftId = scrollH.ClientID
        Else
            gridviewRightId = ""
            tblRightId = ""
            tblLeftId = ""
        End If

    End Function
    ''' <summary>
    ''' 支店押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>2012/11/28 高</remarks>
    Protected Sub btnShiten_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShiten.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        'If grdItiranRight.Rows.Count > 0 Then
        '    gridviewRightId = grdItiranRight.ClientID
        '    tblRightId = scrollV.ClientID
        '    tblLeftId = scrollH.ClientID
        'End If

        Call ShowPop(KeikakuKanriRecord.popKbn.Shiten, True)
    End Sub
    ''' <summary>
    ''' 営業マン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>2012/11/28 高</remarks>
    Protected Sub btnUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        'gridviewRightId = grdItiranRight.ClientID
        'tblRightId = scrollV.ClientID
        'tblLeftId = scrollH.ClientID
        Call ShowPop(KeikakuKanriRecord.popKbn.User, True)
    End Sub
    ''' <summary>
    ''' 加盟店押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>2012/11/28 高</remarks>
    Protected Sub btnKameiten_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKameiten.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        'gridviewRightId = grdItiranRight.ClientID
        'tblRightId = scrollV.ClientID
        'tblLeftId = scrollH.ClientID
        Call ShowPop(KeikakuKanriRecord.popKbn.Kameiten, True)
    End Sub
    '''' <summary>
    '''' 系列コード押下時の処理
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks>2012/11/28 高</remarks>
    'Protected Sub btnKeiretu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeiretu.Click
    '    'EMAB障害対応情報の格納処理
    '    EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
    '                           MyMethod.GetCurrentMethod.Name, sender, e)
    '    Call ShowPop(KeikakuKanriRecord.popKbn.Keiretu, True)
    'End Sub
    ''' <summary>
    ''' 営業所押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>2012/11/28 高</remarks>
    Protected Sub btnEigyou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEigyou.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        'gridviewRightId = grdItiranRight.ClientID
        'tblRightId = scrollV.ClientID
        'tblLeftId = scrollH.ClientID
        Call ShowPop(KeikakuKanriRecord.popKbn.Eigyou, True)
    End Sub

    ''' <summary>
    ''' 「統一法人」押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>2012/10/12 車</remarks>
    Protected Sub btnTouituHoujin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouituHoujin.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        'gridviewRightId = grdItiranRight.ClientID
        'tblRightId = scrollV.ClientID
        'tblLeftId = scrollH.ClientID

        If Me.ddlKeikaku.SelectedValue.Equals(String.Empty) Then
            Call Me.ShowMessage(String.Format(Messages.MSG001E, "計画年度"), Me.ddlKeikaku.ClientID)
            Return
        End If

        Call ShowPop(KeikakuKanriRecord.popKbn.TouituHoujin, True)
    End Sub

    ''' <summary>
    ''' 「法人」押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>2012/10/12 車</remarks>
    Protected Sub btnHoujin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHoujin.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        'gridviewRightId = grdItiranRight.ClientID
        'tblRightId = scrollV.ClientID
        'tblLeftId = scrollH.ClientID

        If Me.ddlKeikaku.SelectedValue.Equals(String.Empty) Then
            Call Me.ShowMessage(String.Format(Messages.MSG001E, "計画年度"), Me.ddlKeikaku.ClientID)
            Return
        End If

        Call ShowPop(KeikakuKanriRecord.popKbn.Houjin, True)
    End Sub

    Protected Sub grdItiranLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItiranLeft.RowDataBound
        'Dim strDataType As String = ""
        'If e.Row.RowIndex <> -1 Then
        '    strDataType = Split(CType(e.Row.Cells(4).Controls(2), HiddenField).Value, ",")(0).ToString
        '    If strDataType <> "" Then
        '        If CInt(strDataType) = CInt(KeikakuKanriRecord.selectKbn.meisai) Then
        '            e.Row.Cells(0).Style("border-top") = "none"
        '            e.Row.Cells(0).Style("border-bottom") = "none"
        '        End If

        '        Select Case CType(e.Row.Cells(2).Controls(1), Label).Text
        '            Case "工事受注率", "直工事率", "工事判定率"
        '                e.Row.Cells(2).Style("border-bottom") = "none"

        '            Case Else
        '                e.Row.Cells(2).Style("border-top") = "none"
        '                e.Row.Cells(2).Style("border-bottom") = "none"
        '        End Select
        '    Else
        '        e.Row.Cells(0).Style("border-top") = "none"
        '    End If

        '    ''明細行
        '    Select Case CType(e.Row.Cells(0).Controls(1), Label).Text
        '        Case "加盟店コード"
        '            'ビルダー名
        '            grdItiranLeft.Rows(e.Row.RowIndex - 2).Cells(0).RowSpan = 2
        '            CType(grdItiranLeft.Rows(e.Row.RowIndex - 2).Cells(0).Controls(1), Label).CssClass = ""
        '            CType(grdItiranLeft.Rows(e.Row.RowIndex - 2).Cells(0).Controls(1), Label).Style.Add("word-break", "break-all;")
        '            CType(grdItiranLeft.Rows(e.Row.RowIndex - 2).Cells(0).Controls(1), Label).Style.Add("height", "26px")
        '            CType(grdItiranLeft.Rows(e.Row.RowIndex - 2).Cells(0).Controls(1), Label).Style.Add("font-size", "11px")

        '            e.Row.Cells(0).Style("border-top") = "1px"

        '            grdItiranLeft.Rows(e.Row.RowIndex - 1).Cells(0).Visible = False
        '        Case "担当名", "年間棟数", "業態", "売上比率"
        '            '加盟店コード～業態の内容の text-align
        '            CType(grdItiranLeft.Rows(e.Row.RowIndex - 1).Cells(0).Controls(1), Label).Style.Add("text-align", "right;")
        '            e.Row.Cells(0).Style("border-top") = "1px"
        '        Case "ビルダー名"
        '            If e.Row.RowIndex <> 0 Then
        '                ' CType(grdItiranLeft.Rows(e.Row.RowIndex - 3).Cells(0).Controls(1), Label).Style.Add("text-align", "right;")
        '                grdItiranLeft.Rows(CInt(ViewState("intBuild"))).Cells(1).Style.Add("text-align", "center;")
        '                grdItiranLeft.Rows(CInt(ViewState("intBuild"))).Cells(1).RowSpan = e.Row.RowIndex - CInt(ViewState("intBuild"))
        '                For intRow As Integer = CInt(ViewState("intBuild")) + 1 To e.Row.RowIndex - 1
        '                    grdItiranLeft.Rows(intRow).Cells(1).Visible = False
        '                Next
        '                e.Row.Cells(0).Style("border-top") = "1px"
        '            End If
        '            ViewState("intBuild") = e.Row.RowIndex
        '        Case "小計"
        '            'If CType(grdItiranLeft.Rows(e.Row.RowIndex - 8).Cells(0).Controls(1), Label).Text = "売上比率" Then
        '            '    ViewState("intKei") = e.Row.RowIndex
        '            'End If

        '        Case "合計"
        '            If chkFC.Checked = False Then
        '                If CType(grdItiranLeft.Rows(e.Row.RowIndex - 1).Cells(0).Controls(1), Label).Text = "小計" Then
        '                    'grdItiranLeft.Rows(CInt(ViewState("intKei"))).Cells(0).Style.Add("text-align", "center;")
        '                    'grdItiranLeft.Rows(CInt(ViewState("intKei"))).Cells(0).RowSpan = e.Row.RowIndex - CInt(ViewState("intKei"))
        '                    'For intRow As Integer = CInt(ViewState("intKei")) + 1 To e.Row.RowIndex - 1
        '                    '    grdItiranLeft.Rows(intRow).Cells(0).Visible = False
        '                    'Next
        '                    ViewState("intKei") = e.Row.RowIndex
        '                End If
        '            Else
        '                If CType(grdItiranLeft.Rows(e.Row.RowIndex - 2).Cells(1).Controls(1), Label).Text = "全体" Then
        '                    ' grdItiranLeft.Rows(CInt(ViewState("intKei"))).Cells(0).Style.Add("text-align", "center;")

        '                    ViewState("intKei") = e.Row.RowIndex
        '                End If
        '            End If


        '    End Select



        '    If e.Row.RowIndex > 0 Then
        '        Select Case CType(grdItiranLeft.Rows(e.Row.RowIndex - 1).Cells(2).Controls(1), Label).Text
        '            Case "工事受注率", "直工事率", "工事判定率"
        '                CType(e.Row.Cells(2).Controls(1), Label).Style.Add("text-align", "right;")

        '        End Select
        '        If CType(grdItiranLeft.Rows(e.Row.RowIndex - 1).Cells(0).Controls(1), Label).Text = "売上比率" Then
        '            CType(e.Row.Cells(0).Controls(1), Label).Style.Add("text-align", "right;")
        '        End If

        '    End If

        '    e.Row.Cells(5).Style("text-align") = "right"


        '    '1つ加盟店のみ場合
        '    If strDataType = CInt(KeikakuKanriRecord.selectKbn.syoukei).ToString And CInt(ViewState("intBuild")) >= 0 Then
        '        grdItiranLeft.Rows(CInt(ViewState("intBuild"))).Cells(1).RowSpan = e.Row.RowIndex - CInt(ViewState("intBuild"))
        '        CType(grdItiranLeft.Rows(CInt(ViewState("intBuild"))).Cells(1).Controls(1), Label).Style.Add("text-align", "center")
        '        For intRow As Integer = CInt(ViewState("intBuild")) + 1 To e.Row.RowIndex - 1
        '            grdItiranLeft.Rows(intRow).Cells(1).Visible = False
        '        Next
        '        ViewState("intBuild") = -1
        '    End If


        '    ' 明細行以外の区分列のSpan()

        '    If strDataType <> "" Then
        '        If CInt(strDataType) <> CInt(KeikakuKanriRecord.selectKbn.meisai) Then
        '            If strDataType & CType(e.Row.Cells(1).Controls(1), Label).Text <> ViewState("intDataType").ToString And CInt(ViewState("intKaisi")) > 0 Then
        '                grdItiranLeft.Rows(e.Row.RowIndex - CInt(ViewState("intKaisi")) - 1).Cells(0).RowSpan = CInt(ViewState("intKaisi")) + 1

        '                grdItiranLeft.Rows(e.Row.RowIndex - CInt(ViewState("intKaisi")) - 1).Cells(0).Style.Add("text-align", "center;")
        '                grdItiranLeft.Rows(e.Row.RowIndex - CInt(ViewState("intKaisi")) - 1).Cells(1).RowSpan = CInt(ViewState("intKaisi")) + 1
        '                grdItiranLeft.Rows(e.Row.RowIndex - CInt(ViewState("intKaisi")) - 1).Cells(1).Style.Add("text-align", "center;")
        '                CType(grdItiranLeft.Rows(e.Row.RowIndex - CInt(ViewState("intKaisi")) - 1).Cells(1).Controls(1), Label).Style.Add("text-align", "center")
        '                For intRow As Integer = e.Row.RowIndex - CInt(ViewState("intKaisi")) To e.Row.RowIndex - 1
        '                    grdItiranLeft.Rows(intRow).Cells(0).Visible = False
        '                    grdItiranLeft.Rows(intRow).Cells(1).Visible = False
        '                Next
        '                ViewState("intKaisi") = 0
        '            End If
        '            ViewState("intDataType") = strDataType & CType(e.Row.Cells(1).Controls(1), Label).Text

        '            ViewState("intKaisi") = CInt(ViewState("intKaisi")) + 1

        '        End If
        '    Else
        '        If e.Row.RowIndex <> 0 Then
        '            grdItiranLeft.Rows(e.Row.RowIndex - 1).Cells(2).Style("border-bottom") = "1px"
        '        End If

        '        ' 合計行()
        '        If e.Row.RowIndex - 1 > 0 Then
        '            e.Row.Cells(0).Style("border-bottom") = "1px"
        '        End If
        '        e.Row.Cells(2).ColumnSpan = 4
        '        For intCol As Integer = 3 To 5
        '            e.Row.Cells(intCol).Visible = False
        '        Next
        '    End If
        '    '区分
        '    If ViewState("strBunbetu").ToString = "" Then
        '        ViewState("strBunbetu") = CType(e.Row.Cells(3).Controls(1), Label).Text
        '        ViewState("intBunbetu") = e.Row.RowIndex
        '    End If
        '    If ViewState("strBunbetu").ToString <> CType(e.Row.Cells(3).Controls(1), Label).Text Then
        '        If ViewState("strBunbetu").ToString <> "" Then
        '            CType(grdItiranLeft.Rows(CInt(ViewState("intBunbetu"))).Cells(3).Controls(1), Label).Style.Add("text-align", "center")
        '            grdItiranLeft.Rows(CInt(ViewState("intBunbetu"))).Cells(3).RowSpan = e.Row.RowIndex - CInt(ViewState("intBunbetu"))
        '            For intRow As Integer = CInt(ViewState("intBunbetu")) + 1 To e.Row.RowIndex - 1
        '                grdItiranLeft.Rows(intRow).Cells(3).Visible = False

        '            Next
        '        End If

        '        ViewState("strBunbetu") = CType(e.Row.Cells(3).Controls(1), Label).Text
        '        ViewState("intBunbetu") = e.Row.RowIndex
        '    End If


        'End If
    End Sub

    Protected Sub grdItiranRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItiranRight.RowDataBound
        'Dim intKoj As Integer = 0
        'Dim strDataType As String = ""
        'If e.Row.RowIndex <> -1 Then
        '    strDataType = Split(CType(grdItiranLeft.Rows(e.Row.RowIndex).Cells(4).Controls(2), HiddenField).Value, ",")(0).ToString
        '    For intCol As Integer = 0 To CInt(ViewState("intMaxCol"))
        '        If CType(gridviewRight.Rows(3).Cells(intCol).Controls(1), Label).Text = "件数" Then

        '            If CType(e.Row.Cells(intCol).Controls(1), Label).Text = "直工事率" Then
        '                intKoj = e.Row.RowIndex
        '            End If
        '            If (CType(gridviewRight.Rows(2).Cells(intCol).Controls(1), Label).Text = "見込" _
        '                OrElse CType(gridviewRight.Rows(2).Cells(intCol).Controls(1), Label).Text = "計画") AndAlso intCol < 228 Then
        '                If CType(grdItiranLeft.Rows(e.Row.RowIndex).Cells(4).Controls(1), Label).Text <> "" AndAlso (CInt(strDataType) = KeikakuKanriRecord.selectKbn.meisai OrElse CInt(strDataType) = KeikakuKanriRecord.selectKbn.FC) Then
        '                    '濃い黄色
        '                    If Split(CType(grdItiranLeft.Rows(e.Row.RowIndex).Cells(4).Controls(2), HiddenField).Value, ",")(1).ToString = "1" Then
        '                        e.Row.Cells(intCol).CssClass = ""
        '                        e.Row.Cells(intCol).CssClass = "meisaiYellow"
        '                    End If
        '                    '薄い黄色　
        '                    If Split(CType(grdItiranLeft.Rows(e.Row.RowIndex).Cells(4).Controls(2), HiddenField).Value, ",")(1).ToString = "0" Then
        '                        e.Row.Cells(intCol).CssClass = ""
        '                        e.Row.Cells(intCol).CssClass = "meisaiUsuiYellow"
        '                    End If
        '                End If
        '            End If


        '            '合計行
        '            If strDataType = "" Then
        '                e.Row.Cells(intCol).CssClass = ""
        '                e.Row.Cells(intCol).CssClass = "U009_td_backcolor6"
        '            End If
        '        Else
        '            '合計行
        '            If strDataType = "" Then
        '                e.Row.Cells(intCol).CssClass = ""
        '                e.Row.Cells(intCol).CssClass = "U009_td_backcolor5"
        '            End If
        '        End If
        '        If CType(gridviewRight.Rows(2).Cells(intCol).Controls(1), Label).Text = "前年同月" OrElse CType(gridviewRight.Rows(2).Cells(intCol).Controls(1), Label).Text = "前年" Then
        '            If CType(grdItiranLeft.Rows(e.Row.RowIndex).Cells(1).Controls(1), Label).Text = "新規" OrElse CType(grdItiranLeft.Rows(e.Row.RowIndex).Cells(1).Controls(1), Label).Text = "FC" Then
        '                ViewState("blnSinki") = True
        '            End If
        '            If strDataType = "" Then
        '                ViewState("blnSinki") = False
        '            End If
        '            If CBool(ViewState("blnSinki")) = True Then
        '                e.Row.Cells(intCol).CssClass = ""
        '                e.Row.Cells(intCol).CssClass = "U009_td_backcolor4"
        '            End If
        '        End If



        '        If intCol / 19 = intCol \ 19 AndAlso intCol <> 228 Then
        '            Select Case CType(e.Row.Cells(intCol).Controls(1), Label).Text
        '                Case "工事受注率", "直工事率", "工事判定率"
        '                    e.Row.Cells(intCol).Style("border-bottom") = "none"
        '                Case "合計"

        '                Case Else
        '                    e.Row.Cells(intCol).Style("border-top") = "none"
        '                    e.Row.Cells(intCol).Style("border-bottom") = "none"
        '            End Select
        '            If e.Row.RowIndex > 0 Then
        '                Select Case CType(grdItiranRight.Rows(e.Row.RowIndex - 1).Cells(0).Controls(1), Label).Text
        '                    Case "工事受注率", "直工事率", "工事判定率"

        '                        CType(e.Row.Cells(intCol).Controls(1), Label).Style.Add("text-align", "right;")

        '                End Select
        '            End If
        '        Else
        '            CType(e.Row.Cells(intCol).Controls(1), Label).Style.Add("text-align", "right;")
        '        End If

        '    Next
        '    GridStyle(grdItiranRight, e.Row)
        'End If




        '======================↓車龍(chel1)↓===============================
        'If e.Row.RowType = DataControlRowType.DataRow Then

        '    '比率の項目数
        '    Dim hirituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.HirituItem).GetType).Length
        '    '前年同月の項目数
        '    Dim zennenItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.ZennenItem).GetType).Length
        '    '計画の項目数
        '    Dim keikakuItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.KeikakuItem).GetType).Length
        '    '見込の項目数
        '    Dim mikomiItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.MikomiItem).GetType).Length
        '    '実績の項目数
        '    Dim jissekiItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.JissekiItem).GetType).Length
        '    '達成率の項目数
        '    Dim tasseirituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.TasseirituItem).GetType).Length
        '    '進捗率の項目数
        '    Dim sintyokurituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.SintyokurituItem).GetType).Length

        '    '各月の項目数
        '    Dim itemCountOfMonth As Integer = hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount + sintyokurituItemCount
        '    '年間の項目数
        '    Dim itemCountOfYear As Integer = zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount + sintyokurituItemCount

        '    '各月の列結合数(ColumnSpan)
        '    Dim columnSpanOfMonth As Integer = 0
        '    '年間の列結合数(ColumnSpan)
        '    Dim columnSpanOfYear As Integer = 0

        '    columnSpanOfMonth = columnSpanOfMonth + hirituItemCount
        '    columnSpanOfYear = columnSpanOfYear + 0

        '    '「前年同月」checkbox
        '    If Not Me.chkZennenDougetu.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth
        '            End If
        '            For j As Integer = 0 To zennenItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + zennenItemCount
        '        columnSpanOfYear = columnSpanOfYear + zennenItemCount
        '    End If


        '    '「計画」checkbox
        '    If Not Me.chkKeikaku.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount
        '            End If
        '            For j As Integer = 0 To keikakuItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + keikakuItemCount
        '        columnSpanOfYear = columnSpanOfYear + keikakuItemCount
        '    End If


        '    '「見込」checkbox
        '    If Not Me.chkMikomi.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount + keikakuItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount + keikakuItemCount
        '            End If
        '            For j As Integer = 0 To mikomiItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + mikomiItemCount
        '        columnSpanOfYear = columnSpanOfYear + mikomiItemCount
        '    End If


        '    '「実績」checkbox
        '    If Not Me.chkJisseki.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount + keikakuItemCount + mikomiItemCount
        '            End If
        '            For j As Integer = 0 To jissekiItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + jissekiItemCount
        '        columnSpanOfYear = columnSpanOfYear + jissekiItemCount
        '    End If


        '    '「達成率」checkbox
        '    If Not Me.chkTassei.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount
        '            End If
        '            For j As Integer = 0 To tasseirituItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + tasseirituItemCount
        '        columnSpanOfYear = columnSpanOfYear + tasseirituItemCount
        '    End If


        '    '「進捗率」checkbox
        '    If Not Me.chkSintyoku.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount
        '            End If
        '            For j As Integer = 0 To sintyokurituItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + sintyokurituItemCount
        '        columnSpanOfYear = columnSpanOfYear + sintyokurituItemCount
        '    End If

        'End If

        '======================↑車龍(chel1)↑===============================

    End Sub

    Protected Sub gridviewRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridviewRight.RowDataBound
        'If e.Row.RowIndex <> -1 Then
        '    GridStyle(gridviewRight, e.Row)
        'End If








        '======================↓車龍(chel1)↓===============================

        'If e.Row.RowType = DataControlRowType.DataRow Then

        '    '比率の項目数
        '    Dim hirituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.HirituItem).GetType).Length
        '    '前年同月の項目数
        '    Dim zennenItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.ZennenItem).GetType).Length
        '    '計画の項目数
        '    Dim keikakuItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.KeikakuItem).GetType).Length
        '    '見込の項目数
        '    Dim mikomiItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.MikomiItem).GetType).Length
        '    '実績の項目数
        '    Dim jissekiItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.JissekiItem).GetType).Length
        '    '達成率の項目数
        '    Dim tasseirituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.TasseirituItem).GetType).Length
        '    '進捗率の項目数
        '    Dim sintyokurituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.SintyokurituItem).GetType).Length

        '    '各月の項目数
        '    Dim itemCountOfMonth As Integer = hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount + sintyokurituItemCount
        '    '年間の項目数
        '    Dim itemCountOfYear As Integer = zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount + sintyokurituItemCount

        '    '各月の列結合数(ColumnSpan)
        '    Dim columnSpanOfMonth As Integer = 0
        '    '年間の列結合数(ColumnSpan)
        '    Dim columnSpanOfYear As Integer = 0

        '    columnSpanOfMonth = columnSpanOfMonth + hirituItemCount
        '    columnSpanOfYear = columnSpanOfYear + 0

        '    '「前年同月」checkbox
        '    If Not Me.chkZennenDougetu.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth
        '            End If
        '            For j As Integer = 0 To zennenItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + zennenItemCount
        '        columnSpanOfYear = columnSpanOfYear + zennenItemCount
        '    End If


        '    '「計画」checkbox
        '    If Not Me.chkKeikaku.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount
        '            End If
        '            For j As Integer = 0 To keikakuItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + keikakuItemCount
        '        columnSpanOfYear = columnSpanOfYear + keikakuItemCount
        '    End If


        '    '「見込」checkbox
        '    If Not Me.chkMikomi.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount + keikakuItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount + keikakuItemCount
        '            End If
        '            For j As Integer = 0 To mikomiItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + mikomiItemCount
        '        columnSpanOfYear = columnSpanOfYear + mikomiItemCount
        '    End If


        '    '「実績」checkbox
        '    If Not Me.chkJisseki.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount + keikakuItemCount + mikomiItemCount
        '            End If
        '            For j As Integer = 0 To jissekiItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + jissekiItemCount
        '        columnSpanOfYear = columnSpanOfYear + jissekiItemCount
        '    End If


        '    '「達成率」checkbox
        '    If Not Me.chkTassei.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount
        '            End If
        '            For j As Integer = 0 To tasseirituItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + tasseirituItemCount
        '        columnSpanOfYear = columnSpanOfYear + tasseirituItemCount
        '    End If


        '    '「進捗率」checkbox
        '    If Not Me.chkSintyoku.Checked Then
        '        For i As Integer = 0 To 12
        '            Dim intBeginCellIndex As Integer = 0
        '            If i < 12 Then
        '                '各月
        '                intBeginCellIndex = i * itemCountOfMonth + hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount
        '            Else
        '                '年間
        '                intBeginCellIndex = i * itemCountOfMonth + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount
        '            End If
        '            For j As Integer = 0 To sintyokurituItemCount - 1
        '                e.Row.Cells(intBeginCellIndex + j).Visible = False
        '            Next
        '        Next
        '    Else
        '        columnSpanOfMonth = columnSpanOfMonth + sintyokurituItemCount
        '        columnSpanOfYear = columnSpanOfYear + sintyokurituItemCount
        '    End If


        '    '列結合の幅
        '    Dim widthSum As Double

        '    Select Case e.Row.RowIndex
        '        Case 0, 1 ' 年月,"売上"

        '            For i As Integer = 0 To 12

        '                If i < 12 Then
        '                    '各月
        '                    widthSum = CType(e.Row.Cells(i * itemCountOfMonth).Controls(1), Label).Width.Value
        '                    For j As Integer = 1 To itemCountOfMonth - 1
        '                        If e.Row.Cells(i * itemCountOfMonth + j).Visible Then
        '                            widthSum = widthSum + CType(e.Row.Cells(i * itemCountOfMonth + j).Controls(1), Label).Width.Value
        '                            e.Row.Cells(i * itemCountOfMonth + j).Visible = False
        '                        End If
        '                    Next
        '                    e.Row.Cells(i * itemCountOfMonth).ColumnSpan = columnSpanOfMonth
        '                    CType(e.Row.Cells(i * itemCountOfMonth).Controls(1), Label).Width = CType(widthSum, Unit)
        '                    e.Row.Cells(i * itemCountOfMonth).HorizontalAlign = HorizontalAlign.Center
        '                Else
        '                    '年間
        '                    widthSum = 0
        '                    If columnSpanOfYear > 0 Then
        '                        For j As Integer = 0 To itemCountOfYear - 1
        '                            If e.Row.Cells(i * itemCountOfMonth + j).Visible Then
        '                                widthSum = widthSum + CType(e.Row.Cells(i * itemCountOfMonth + j).Controls(1), Label).Width.Value
        '                                e.Row.Cells(i * itemCountOfMonth + j).Visible = False
        '                            End If
        '                        Next
        '                        e.Row.Cells(i * itemCountOfMonth + itemCountOfYear - columnSpanOfYear).Visible = True
        '                        e.Row.Cells(i * itemCountOfMonth + itemCountOfYear - columnSpanOfYear).ColumnSpan = columnSpanOfYear
        '                        CType(e.Row.Cells(i * itemCountOfMonth + itemCountOfYear - columnSpanOfYear).Controls(1), Label).Width = CType(widthSum, Unit)
        '                        e.Row.Cells(i * itemCountOfMonth + itemCountOfYear - columnSpanOfYear).HorizontalAlign = HorizontalAlign.Center
        '                    End If
        '                End If

        '            Next

        '        Case 2 ' 比率,"前年同月","計画","見込","実績","達成率","進捗率"

        '            For i As Integer = 0 To 12

        '                widthSum = CType(e.Row.Cells(i * itemCountOfMonth).Controls(1), Label).Width.Value

        '                Dim intBeginCellIndex As Integer = 0
        '                intBeginCellIndex = i * itemCountOfMonth

        '                '比率
        '                If i < 12 Then
        '                    e.Row.Cells(intBeginCellIndex).RowSpan = 2
        '                    e.Row.Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center
        '                    e.Row.Cells(intBeginCellIndex).Height = CType(37, Unit)
        '                    CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Text = "工事比率<br>（計算用）"
        '                End If

        '                '前年同月
        '                If i < 12 Then
        '                    '各月
        '                    intBeginCellIndex = intBeginCellIndex + hirituItemCount
        '                Else
        '                    '年間
        '                    intBeginCellIndex = intBeginCellIndex + 0
        '                End If
        '                If Me.chkZennenDougetu.Checked Then
        '                    e.Row.Cells(intBeginCellIndex).ColumnSpan = zennenItemCount
        '                    widthSum = CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width.Value
        '                    For j As Integer = 1 To zennenItemCount - 1
        '                        If e.Row.Cells(intBeginCellIndex + j).Visible Then
        '                            widthSum = widthSum + CType(e.Row.Cells(intBeginCellIndex + j).Controls(1), Label).Width.Value
        '                            e.Row.Cells(intBeginCellIndex + j).Visible = False
        '                        End If
        '                    Next
        '                    CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
        '                    e.Row.Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center
        '                End If

        '                '計画
        '                intBeginCellIndex = intBeginCellIndex + zennenItemCount
        '                e.Row.Cells(intBeginCellIndex).ColumnSpan = keikakuItemCount
        '                widthSum = CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width.Value
        '                For j As Integer = 1 To keikakuItemCount - 1
        '                    If e.Row.Cells(intBeginCellIndex + j).Visible Then
        '                        widthSum = widthSum + CType(e.Row.Cells(intBeginCellIndex + j).Controls(1), Label).Width.Value
        '                        e.Row.Cells(intBeginCellIndex + j).Visible = False
        '                    End If
        '                Next
        '                CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
        '                e.Row.Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

        '                '見込
        '                intBeginCellIndex = intBeginCellIndex + keikakuItemCount
        '                e.Row.Cells(intBeginCellIndex).ColumnSpan = mikomiItemCount
        '                widthSum = CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width.Value
        '                For j As Integer = 1 To mikomiItemCount - 1
        '                    If e.Row.Cells(intBeginCellIndex + j).Visible Then
        '                        widthSum = widthSum + CType(e.Row.Cells(intBeginCellIndex + j).Controls(1), Label).Width.Value
        '                        e.Row.Cells(intBeginCellIndex + j).Visible = False
        '                    End If
        '                Next
        '                CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
        '                e.Row.Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

        '                '実績
        '                intBeginCellIndex = intBeginCellIndex + mikomiItemCount
        '                e.Row.Cells(intBeginCellIndex).ColumnSpan = jissekiItemCount
        '                widthSum = CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width.Value
        '                For j As Integer = 1 To jissekiItemCount - 1
        '                    If e.Row.Cells(intBeginCellIndex + j).Visible Then
        '                        widthSum = widthSum + CType(e.Row.Cells(intBeginCellIndex + j).Controls(1), Label).Width.Value
        '                        e.Row.Cells(intBeginCellIndex + j).Visible = False
        '                    End If
        '                Next
        '                CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
        '                e.Row.Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

        '                '達成率
        '                intBeginCellIndex = intBeginCellIndex + jissekiItemCount
        '                e.Row.Cells(intBeginCellIndex).ColumnSpan = tasseirituItemCount
        '                widthSum = CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width.Value
        '                For j As Integer = 1 To jissekiItemCount - 1
        '                    If e.Row.Cells(intBeginCellIndex + j).Visible Then
        '                        widthSum = widthSum + CType(e.Row.Cells(intBeginCellIndex + j).Controls(1), Label).Width.Value
        '                        e.Row.Cells(intBeginCellIndex + j).Visible = False
        '                    End If
        '                Next
        '                CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
        '                e.Row.Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

        '                '進捗率
        '                intBeginCellIndex = intBeginCellIndex + tasseirituItemCount
        '                e.Row.Cells(intBeginCellIndex).ColumnSpan = sintyokurituItemCount
        '                widthSum = CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width.Value
        '                For j As Integer = 1 To jissekiItemCount - 1
        '                    If e.Row.Cells(intBeginCellIndex + j).Visible Then
        '                        widthSum = widthSum + CType(e.Row.Cells(intBeginCellIndex + j).Controls(1), Label).Width.Value
        '                        e.Row.Cells(intBeginCellIndex + j).Visible = False
        '                    End If
        '                Next
        '                CType(e.Row.Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
        '                e.Row.Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center
        '            Next

        '        Case 3
        '            For i As Integer = 0 To e.Row.Cells.Count - 1
        '                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center

        '                If (i < itemCountOfMonth * 12) AndAlso (i Mod itemCountOfMonth = 0) Then
        '                    e.Row.Cells(i).Visible = False
        '                Else

        '                End If
        '            Next

        '    End Select

        'End If
        '======================↑車龍(chel1)↑===============================

    End Sub




    ''' <summary>
    ''' 明細データStyleを設定する
    ''' </summary>
    ''' <remarks>車龍(chel1)</remarks>
    Private Sub SetMeisaiDataStyle()

        '比率の項目数
        Dim hirituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.HirituItem).GetType).Length
        '前年同月の項目数
        Dim zennenItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.ZennenItem).GetType).Length
        '計画の項目数
        Dim keikakuItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.KeikakuItem).GetType).Length
        '見込の項目数
        Dim mikomiItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.MikomiItem).GetType).Length
        '実績の項目数
        Dim jissekiItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.JissekiItem).GetType).Length
        '達成率の項目数
        Dim tasseirituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.TasseirituItem).GetType).Length
        '進捗率の項目数
        Dim sintyokurituItemCount As Integer = System.Enum.GetValues((New KeikakuKanriRecord.SintyokurituItem).GetType).Length

        '==============================================================================================================

        '各月の項目数
        Dim itemCountOfMonth As Integer = hirituItemCount + zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount + sintyokurituItemCount
        '年間の項目数
        Dim itemCountOfYear As Integer = zennenItemCount + keikakuItemCount + mikomiItemCount + jissekiItemCount + tasseirituItemCount + sintyokurituItemCount

        '==============================================================================================================

        '各月の列結合数(ColumnSpan)
        Dim columnSpanOfMonth As Integer = 0
        '年間の列結合数(ColumnSpan)
        Dim columnSpanOfYear As Integer = 0

        columnSpanOfMonth = columnSpanOfMonth + hirituItemCount
        columnSpanOfYear = columnSpanOfYear + 0

        '「前年同月」のデータを表示する場合
        If Me.chkZennenDougetu.Checked Then
            columnSpanOfMonth = columnSpanOfMonth + zennenItemCount
            columnSpanOfYear = columnSpanOfMonth + zennenItemCount
        End If
        '「計画」のデータを表示する場合
        If Me.chkKeikaku.Checked Then
            columnSpanOfMonth = columnSpanOfMonth + keikakuItemCount
            columnSpanOfYear = columnSpanOfMonth + keikakuItemCount
        End If
        '「見込」のデータを表示する場合
        If Me.chkMikomi.Checked Then
            columnSpanOfMonth = columnSpanOfMonth + mikomiItemCount
            columnSpanOfYear = columnSpanOfMonth + mikomiItemCount
        End If
        '「実績」のデータを表示する場合
        If Me.chkJisseki.Checked Then
            columnSpanOfMonth = columnSpanOfMonth + jissekiItemCount
            columnSpanOfYear = columnSpanOfMonth + jissekiItemCount
        End If
        '「達成率」のデータを表示する場合
        If Me.chkTassei.Checked Then
            columnSpanOfMonth = columnSpanOfMonth + tasseirituItemCount
            columnSpanOfYear = columnSpanOfMonth + tasseirituItemCount
        End If
        '「進捗率」のデータを表示する場合
        If Me.chkSintyoku.Checked Then
            columnSpanOfMonth = columnSpanOfMonth + sintyokurituItemCount
            columnSpanOfYear = columnSpanOfMonth + sintyokurituItemCount
        End If

        '==============================================================================================================

        '各月、年間の表示Flag
        Dim monthShowFlg(12) As Boolean
        monthShowFlg(0) = False '四月
        monthShowFlg(1) = False '五月
        monthShowFlg(2) = False '六月
        monthShowFlg(3) = False '七月
        monthShowFlg(4) = False '八月
        monthShowFlg(5) = False '九月
        monthShowFlg(6) = False '十月
        monthShowFlg(7) = False '十一月
        monthShowFlg(8) = False '十二月
        monthShowFlg(9) = False '一月
        monthShowFlg(10) = False '二月
        monthShowFlg(11) = False '三月
        monthShowFlg(12) = False '年間

        '各月、年間の表示Flagをクリアする
        For i As Integer = 0 To monthShowFlg.Length - 1
            monthShowFlg(i) = False
        Next

        '当前月
        Dim CommonBC As New CommonBC
        Dim thisMonth As Integer = Convert.ToDateTime(CommonBC.SelSystemDate.Rows(0).Item(0).ToString).Month
        Dim thisMonthIndex As Integer
        If thisMonth >= 4 Then
            thisMonthIndex = thisMonth - 4
        Else
            thisMonthIndex = thisMonth + 8
        End If

        '「今月」checkbox
        If Me.chkKongetu.Checked Then
            '当月のみを表示する
            monthShowFlg(thisMonthIndex) = True
        End If

        '「直近3ヶ月」checkbox
        If Me.chkSangetu.Checked Then
            '当月、前月、前前月のみを表示する
            '例えば、
            '　当月 = 4月の場合：4月
            '　当月 = 5月の場合：4月、5月
            '　当月 = 6月の場合：4月、5月、6月
            '　......
            '　当月 = 3月の場合：1月、2月、3月
            For i As Integer = thisMonthIndex To thisMonthIndex - 2 Step -1
                If i >= 0 Then
                    monthShowFlg(i) = True
                End If
            Next
        End If

        '「先行4ヶ月」checkbox
        If Me.chkYogetu.Checked Then
            '当月、後月、後後月、後後後月のみを表示する
            '例えば、
            '　当月 = 4月の場合：4月、5月、6月、7月
            '　......
            '　当月 = 12月の場合：12月、1月、2月、3月
            '　当月 = 1月の場合：1月、2月、3月
            '　当月 = 2月の場合：2月、3月
            '　当月 = 3月の場合：3月
            For i As Integer = thisMonthIndex To thisMonthIndex + 3
                If i <= 11 Then
                    monthShowFlg(i) = True
                End If
            Next
        End If

        '「今月」、「直近3ヶ月」、「先行4ヶ月」　選択しない
        If (Not Me.chkKongetu.Checked) AndAlso (Not Me.chkSangetu.Checked) AndAlso (Not Me.chkYogetu.Checked) Then
            '全部月と年間を表示する
            For i As Integer = 0 To 12
                monthShowFlg(i) = True
            Next
        End If

        '==============================================================================================================

        '「営業区分」列の結合開始行
        Dim intKbnBeginRowIndex As Integer = 0

        '「分類」列の結合開始行
        Dim intBuruiBeginRowIndex As Integer = 0
        Dim strBuruiOld As String = CType(Me.grdItiranLeft.Rows(0).Cells(3).Controls(1), Label).Text
        Dim strBuruiNow As String = String.Empty

        For rowIndex As Integer = 0 To Me.grdItiranRight.Rows.Count - 1

            Dim strDataType() As String
            strDataType = Split(CType(Me.grdItiranLeft.Rows(rowIndex).Cells(4).Controls(2), HiddenField).Value, ",")

            '合計行Flag
            Dim lbnSumRowFlg As Boolean
            lbnSumRowFlg = CType(Me.grdItiranRight.Rows(rowIndex).Cells(0).Controls(1), Label).Text.Equals("合計")
            If lbnSumRowFlg Then
                Dim asdf As Integer = 0
            End If

            '営業区分
            Dim strEigyouKbn As String
            strEigyouKbn = CType(Me.grdItiranLeft.Rows(rowIndex).Cells(1).Controls(1), Label).Text

            '分類
            strBuruiNow = CType(Me.grdItiranLeft.Rows(rowIndex).Cells(3).Controls(1), Label).Text

            '><><><><><><><><><><><><><><><><><↓左側↓><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><

            '「ビルダー情報」列、「区分」列
            Select Case CType(Me.grdItiranLeft.Rows(rowIndex).Cells(0).Controls(1), Label).Text
                Case "小計", "合計"
                    '小計、合計部分

                    If rowIndex > 0 Then
                        Me.grdItiranLeft.Rows(rowIndex - 1).Cells(0).Style.Remove("border-bottom")
                    End If

                    '「ビルダー情報」、「区分」列を結合する
                    If lbnSumRowFlg Then '合計行
                        '「区分」列を結合する
                        Me.grdItiranLeft.Rows(intKbnBeginRowIndex).Cells(0).RowSpan = rowIndex - intKbnBeginRowIndex + 1
                        Me.grdItiranLeft.Rows(intKbnBeginRowIndex).Cells(1).RowSpan = rowIndex - intKbnBeginRowIndex + 1
                        For i As Integer = intKbnBeginRowIndex + 1 To rowIndex
                            Me.grdItiranLeft.Rows(i).Cells(0).Visible = False
                            Me.grdItiranLeft.Rows(i).Cells(1).Visible = False
                        Next

                        '居中
                        Me.grdItiranLeft.Rows(intKbnBeginRowIndex).Cells(0).HorizontalAlign = HorizontalAlign.Center
                        Me.grdItiranLeft.Rows(intKbnBeginRowIndex).Cells(1).HorizontalAlign = HorizontalAlign.Center

                        '全体（FC除外）
                        If CType(Me.grdItiranLeft.Rows(intKbnBeginRowIndex).Cells(1).Controls(1), Label).Text.Equals("全体（FC除外）") Then
                            CType(Me.grdItiranLeft.Rows(intKbnBeginRowIndex).Cells(1).Controls(1), Label).Text = "全体<br>（FC<br>除外）"
                        End If

                        intKbnBeginRowIndex = rowIndex + 1
                    End If

                Case Else
                    '明細部分
                    Me.grdItiranLeft.Rows(rowIndex).Cells(0).Style.Add("border-bottom", "#FFCB96")
                    Select Case CType(Me.grdItiranLeft.Rows(rowIndex).Cells(0).Controls(1), Label).Text

                        Case "ビルダー名"
                            Me.grdItiranLeft.Rows(rowIndex + 1).Cells(0).RowSpan = 2
                            CType(Me.grdItiranLeft.Rows(rowIndex + 1).Cells(0).Controls(1), Label).CssClass = ""
                            CType(Me.grdItiranLeft.Rows(rowIndex + 1).Cells(0).Controls(1), Label).Style.Add("word-break", "break-all;")
                            Me.grdItiranLeft.Rows(rowIndex + 2).Cells(0).Visible = False

                            CType(Me.grdItiranLeft.Rows(rowIndex + 1).Cells(0).Controls(1), Label).Style.Add("font-size", "11px")

                            If rowIndex > 0 Then
                                Me.grdItiranLeft.Rows(rowIndex - 1).Cells(0).Style.Remove("border-bottom")
                            End If

                        Case "加盟店コード"
                            Me.grdItiranLeft.Rows(rowIndex - 2).Cells(0).Style.Remove("border-bottom")

                        Case "担当名", "年間棟数", "業態", "売上比率"
                            Me.grdItiranLeft.Rows(rowIndex - 1).Cells(0).Style.Remove("border-bottom")

                        Case Else
                            If rowIndex > 0 Then
                                If Not CType(Me.grdItiranLeft.Rows(rowIndex - 1).Cells(0).Controls(1), Label).Text.Equals("ビルダー名") Then
                                    Me.grdItiranLeft.Rows(rowIndex).Cells(0).HorizontalAlign = HorizontalAlign.Right

                                End If
                            End If
                    End Select

                    '「区分」列を結合する
                    If lbnSumRowFlg Then '合計行
                        '「区分」列を結合する
                        Me.grdItiranLeft.Rows(intKbnBeginRowIndex).Cells(1).RowSpan = rowIndex - intKbnBeginRowIndex + 1
                        For i As Integer = intKbnBeginRowIndex + 1 To rowIndex
                            Me.grdItiranLeft.Rows(i).Cells(1).Visible = False
                        Next
                        '居中
                        Me.grdItiranLeft.Rows(intKbnBeginRowIndex).Cells(1).HorizontalAlign = HorizontalAlign.Center

                        intKbnBeginRowIndex = rowIndex + 1
                    End If

            End Select

            '「工事比率」列
            Select Case CType(Me.grdItiranLeft.Rows(rowIndex).Cells(2).Controls(1), Label).Text

                Case "工事判定率", "工事受注率", "直工事率"

                Case Else
                    If rowIndex > 0 Then
                        If Not lbnSumRowFlg Then
                            Me.grdItiranLeft.Rows(rowIndex - 1).Cells(2).Style.Add("border-bottom", "#99CDFF")
                        End If
                    End If
                    Me.grdItiranLeft.Rows(rowIndex).Cells(2).HorizontalAlign = HorizontalAlign.Right

            End Select

            '「分類」列
            If Not strBuruiOld.Equals(strBuruiNow) Then
                '結合の判断
                If Not strBuruiOld.Equals(String.Empty) Then
                    '列結合
                    If rowIndex - intBuruiBeginRowIndex > 1 Then
                        Me.grdItiranLeft.Rows(intBuruiBeginRowIndex).Cells(3).RowSpan = rowIndex - intBuruiBeginRowIndex
                        For i As Integer = intBuruiBeginRowIndex + 1 To rowIndex - 1
                            Me.grdItiranLeft.Rows(i).Cells(3).Visible = False
                        Next
                    End If

                    '居中
                    Me.grdItiranLeft.Rows(intBuruiBeginRowIndex).Cells(3).HorizontalAlign = HorizontalAlign.Center
                End If

                intBuruiBeginRowIndex = rowIndex
                strBuruiOld = strBuruiNow
            End If

            '「平均単価」列
            Me.grdItiranLeft.Rows(rowIndex).Cells(5).HorizontalAlign = HorizontalAlign.Right

            '合計行の結合
            If lbnSumRowFlg Then
                Me.grdItiranLeft.Rows(rowIndex).Cells(2).ColumnSpan = 4
                Me.grdItiranLeft.Rows(rowIndex).Cells(3).Visible = False
                Me.grdItiranLeft.Rows(rowIndex).Cells(4).Visible = False
                Me.grdItiranLeft.Rows(rowIndex).Cells(5).Visible = False
            End If

            '><><><><><><><><><><><><><><><><><↑左側↑><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><

            '><><><><><><><><><><><><><><><><><↓右側↓><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><
            For monthIndex As Integer = 0 To 12

                '各月、年間の表示
                If Not monthShowFlg(monthIndex) Then
                    '表示しない

                    If monthIndex < 12 Then
                        '各月
                        For i As Integer = 0 To itemCountOfMonth - 1
                            'ヘッダー
                            If rowIndex < 4 Then
                                Me.gridviewRight.Rows(rowIndex).Cells(monthIndex * itemCountOfMonth + i).Visible = False
                            End If
                            '明細
                            Me.grdItiranRight.Rows(rowIndex).Cells(monthIndex * itemCountOfMonth + i).Visible = False
                        Next
                    Else
                        '年間
                        For i As Integer = 0 To itemCountOfYear - 1
                            'ヘッダー
                            If rowIndex < 4 Then
                                Me.gridviewRight.Rows(rowIndex).Cells(monthIndex * itemCountOfMonth + i).Visible = False
                            End If
                            '明細
                            Me.grdItiranRight.Rows(rowIndex).Cells(monthIndex * itemCountOfMonth + i).Visible = False
                        Next
                    End If

                Else 'If Not monthShowFlg(monthIndex) Then

                    '表示する

                    '操作開始列の索引
                    Dim intBeginCellIndex As Integer = 0
                    '結合した列の幅
                    Dim widthSum As Double = 0

                    '操作開始列をクリアする
                    intBeginCellIndex = 0
                    '結合した列の幅をクリアする
                    widthSum = 0

                    '=========================================================================================================
                    '「比率」を設定する
                    If monthIndex < 12 Then
                        '各月
                        intBeginCellIndex = monthIndex * itemCountOfMonth

                        Dim strTemp As String = CType(Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Text

                        Select Case strTemp
                            Case "工事受注率", "直工事率", "工事判定率", "合計"
                                Me.grdItiranRight.Rows(rowIndex).Cells(monthIndex * itemCountOfMonth).HorizontalAlign = HorizontalAlign.Left

                            Case Else
                                Me.grdItiranRight.Rows(rowIndex).Cells(monthIndex * itemCountOfMonth).HorizontalAlign = HorizontalAlign.Right

                                If rowIndex > 0 Then
                                    Me.grdItiranRight.Rows(rowIndex - 1).Cells(intBeginCellIndex).Style.Add("border-bottom", "#ccffff")
                                End If

                        End Select

                        '合計行
                        If lbnSumRowFlg Then
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex).CssClass = "U009_td_backcolor5"
                        End If

                    End If

                    '=========================================================================================================

                    '「前年同月」、「計画」、「見込」、「実績」、「達成率」、「進捗率」を表示するかどうか

                    '「前年同月」checkbox
                    If monthIndex < 12 Then
                        '各月
                        intBeginCellIndex = monthIndex * itemCountOfMonth + hirituItemCount
                    Else
                        '年間
                        intBeginCellIndex = monthIndex * itemCountOfMonth + 0
                    End If
                    If Not Me.chkZennenDougetu.Checked Then
                        '「前年同月」を表示しない
                        For j As Integer = 0 To zennenItemCount - 1
                            'ヘッダー
                            If rowIndex < 4 Then
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                            End If
                            '明細
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                        Next
                    Else
                        '「前年同月」を表示する

                        'ヘッダー列の結合(「"前年同月"」、「"計画"」、「"見込"」、「"実績"」、「"達成率"」、「"進捗率"」行)
                        If rowIndex = 2 Then
                            widthSum = 0
                            For i As Integer = 0 To zennenItemCount - 1
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible = False
                                widthSum = widthSum + CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Controls(1), Label).Width.Value
                            Next

                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Visible = True
                            '結合の項目数
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).ColumnSpan = zennenItemCount
                            '幅
                            CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
                            '居中
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

                        End If

                        '明細データ　居右
                        For i As Integer = 0 To zennenItemCount - 1
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).HorizontalAlign = HorizontalAlign.Right

                            '合計行
                            If lbnSumRowFlg Then
                                If i = 0 Then
                                    '「件数」
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor6"
                                Else
                                    '「件数」以外
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor5"
                                End If
                            End If

                            'FC行の前年データを表示しない
                            If strEigyouKbn.Equals("FC") Then
                                Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor4"
                            End If
                        Next

                    End If

                    '「計画」checkbox
                    intBeginCellIndex = intBeginCellIndex + zennenItemCount
                    If Not Me.chkKeikaku.Checked Then
                        '「計画」を表示しない
                        For j As Integer = 0 To keikakuItemCount - 1
                            'ヘッダー
                            If rowIndex < 4 Then
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                            End If
                            '明細
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                        Next
                    Else
                        '「計画」を表示する

                        'ヘッダー列の結合(「"前年同月"」、「"計画"」、「"見込"」、「"実績"」、「"達成率"」、「"進捗率"」行)
                        If rowIndex = 2 Then
                            widthSum = 0
                            For i As Integer = 0 To keikakuItemCount - 1
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible = False
                                widthSum = widthSum + CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Controls(1), Label).Width.Value
                            Next

                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Visible = True
                            '結合の項目数
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).ColumnSpan = keikakuItemCount
                            '幅
                            CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
                            '居中
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

                        End If

                        '明細データ　居右
                        For i As Integer = 0 To keikakuItemCount - 1
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).HorizontalAlign = HorizontalAlign.Right

                            '合計行
                            If lbnSumRowFlg Then
                                If i = 0 Then
                                    '「件数」
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor6"
                                Else
                                    '「件数」以外
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor5"
                                End If
                            End If
                        Next

                        '「件数」の背景色
                        If monthIndex < 12 Then
                            If (CType(Me.grdItiranLeft.Rows(rowIndex).Cells(4).Controls(1), Label).Text <> "") _
                                AndAlso _
                                (CInt(strDataType(0)) = KeikakuKanriRecord.selectKbn.meisai OrElse CInt(strDataType(0)) = KeikakuKanriRecord.selectKbn.FC) Then
                                '濃い黄色
                                If strDataType(1) = "1" Then
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex).CssClass = "meisaiYellow"
                                End If
                                '薄い黄色　
                                If strDataType(1) = "0" Then
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex).CssClass = "meisaiUsuiYellow"
                                End If
                            End If
                        End If
                    End If

                    '「見込」checkbox
                    intBeginCellIndex = intBeginCellIndex + keikakuItemCount
                    If Not Me.chkMikomi.Checked Then
                        '「見込」を表示しない
                        For j As Integer = 0 To mikomiItemCount - 1
                            'ヘッダー
                            If rowIndex < 4 Then
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                            End If
                            '明細
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                        Next
                    Else
                        '「見込」を表示する

                        'ヘッダー列の結合(「"前年同月"」、「"計画"」、「"見込"」、「"実績"」、「"達成率"」、「"進捗率"」行)
                        If rowIndex = 2 Then
                            widthSum = 0
                            For i As Integer = 0 To mikomiItemCount - 1
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible = False
                                widthSum = widthSum + CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Controls(1), Label).Width.Value
                            Next

                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Visible = True
                            '結合の項目数
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).ColumnSpan = mikomiItemCount
                            '幅
                            CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
                            '居中
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

                        End If

                        '明細データ　居右
                        For i As Integer = 0 To mikomiItemCount - 1
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).HorizontalAlign = HorizontalAlign.Right

                            '合計行
                            If lbnSumRowFlg Then
                                If i = 0 Then
                                    '「件数」
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor6"
                                Else
                                    '「件数」以外
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor5"
                                End If
                            End If
                        Next

                        '「件数」の背景色
                        If monthIndex < 12 Then
                            If (CType(Me.grdItiranLeft.Rows(rowIndex).Cells(4).Controls(1), Label).Text <> "") _
                                AndAlso _
                                (CInt(strDataType(0)) = KeikakuKanriRecord.selectKbn.meisai OrElse CInt(strDataType(0)) = KeikakuKanriRecord.selectKbn.FC) Then
                                '濃い黄色
                                If strDataType(1) = "1" Then
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex).CssClass = "meisaiYellow"
                                End If
                                '薄い黄色　
                                If strDataType(1) = "0" Then
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex).CssClass = "meisaiUsuiYellow"
                                End If
                            End If
                        End If

                    End If

                    '「実績」checkbox
                    intBeginCellIndex = intBeginCellIndex + mikomiItemCount
                    If Not Me.chkJisseki.Checked Then
                        '「実績」を表示しない
                        For j As Integer = 0 To jissekiItemCount - 1
                            'ヘッダー
                            If rowIndex < 4 Then
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                            End If
                            '明細
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                        Next
                    Else
                        '「実績」を表示する

                        'ヘッダー列の結合(「"前年同月"」、「"計画"」、「"見込"」、「"実績"」、「"達成率"」、「"進捗率"」行)
                        If rowIndex = 2 Then
                            widthSum = 0
                            For i As Integer = 0 To jissekiItemCount - 1
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible = False
                                widthSum = widthSum + CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Controls(1), Label).Width.Value
                            Next

                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Visible = True
                            '結合の項目数
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).ColumnSpan = jissekiItemCount
                            '幅
                            CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
                            '居中
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

                        End If

                        '明細データ　居右
                        For i As Integer = 0 To jissekiItemCount - 1
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).HorizontalAlign = HorizontalAlign.Right

                            '合計行
                            If lbnSumRowFlg Then
                                If i = 0 Then
                                    '「件数」
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor6"
                                Else
                                    '「件数」以外
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor5"
                                End If
                            End If
                        Next

                    End If

                    '「達成率」checkbox
                    intBeginCellIndex = intBeginCellIndex + jissekiItemCount
                    If Not Me.chkTassei.Checked Then
                        '「達成率」を表示しない
                        For j As Integer = 0 To tasseirituItemCount - 1
                            'ヘッダー
                            If rowIndex < 4 Then
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                            End If
                            '明細
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                        Next
                    Else
                        '「達成率」を表示する

                        'ヘッダー列の結合(「"前年同月"」、「"計画"」、「"見込"」、「"実績"」、「"達成率"」、「"進捗率"」行)
                        If rowIndex = 2 Then
                            widthSum = 0
                            For i As Integer = 0 To tasseirituItemCount - 1
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible = False
                                widthSum = widthSum + CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Controls(1), Label).Width.Value
                            Next

                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Visible = True
                            '結合の項目数
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).ColumnSpan = tasseirituItemCount
                            '幅
                            CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
                            '居中
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

                        End If

                        '明細データ　居右
                        For i As Integer = 0 To tasseirituItemCount - 1
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).HorizontalAlign = HorizontalAlign.Right

                            '合計行
                            If lbnSumRowFlg Then
                                If i = 0 Then
                                    '「件数」
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor6"
                                Else
                                    '「件数」以外
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor5"
                                End If
                            End If
                        Next

                    End If

                    '「進捗率」checkbox
                    intBeginCellIndex = intBeginCellIndex + tasseirituItemCount
                    If Not Me.chkSintyoku.Checked Then
                        '「進捗率」を表示しない
                        For j As Integer = 0 To sintyokurituItemCount - 1
                            'ヘッダー
                            If rowIndex < 4 Then
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                            End If
                            '明細
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + j).Visible = False
                        Next
                    Else
                        '「進捗率」を表示する

                        'ヘッダー列の結合(「"前年同月"」、「"計画"」、「"見込"」、「"実績"」、「"達成率"」、「"進捗率"」行)
                        If rowIndex = 2 Then
                            widthSum = 0
                            For i As Integer = 0 To sintyokurituItemCount - 1
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible = False
                                widthSum = widthSum + CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Controls(1), Label).Width.Value
                            Next

                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Visible = True
                            '結合の項目数
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).ColumnSpan = sintyokurituItemCount
                            '幅
                            CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
                            '居中
                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center

                        End If

                        '明細データ　居右
                        For i As Integer = 0 To sintyokurituItemCount - 1
                            Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).HorizontalAlign = HorizontalAlign.Right

                            '合計行
                            If lbnSumRowFlg Then
                                If i = 0 Then
                                    '「件数」
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor6"
                                Else
                                    '「件数」以外
                                    Me.grdItiranRight.Rows(rowIndex).Cells(intBeginCellIndex + i).CssClass = "U009_td_backcolor5"
                                End If
                            End If
                        Next

                    End If

                    '=========================================================================================================

                    '操作開始列をクリアする
                    intBeginCellIndex = 0
                    '結合した列の幅をクリアする
                    widthSum = 0

                    Select Case rowIndex
                        Case 0, 1
                            'ヘッダー列の結合(「年月」行、「"売上"」行)
                            intBeginCellIndex = monthIndex * itemCountOfMonth

                            If monthIndex < 12 Then
                                '各月

                                widthSum = CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width.Value

                                For i As Integer = 1 To itemCountOfMonth - 1
                                    If Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible Then
                                        widthSum = widthSum + CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Controls(1), Label).Width.Value
                                        Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible = False
                                    End If
                                Next

                                '結合の項目数
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).ColumnSpan = columnSpanOfMonth
                                '幅
                                CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
                                '居中
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center
                            Else
                                '年間

                                widthSum = 0

                                If columnSpanOfYear > 0 Then
                                    For i As Integer = 0 To itemCountOfYear - 1
                                        If Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible Then
                                            widthSum = widthSum + CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Controls(1), Label).Width.Value
                                            Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).Visible = False
                                        End If
                                    Next

                                    Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Visible = True
                                    '結合の項目数
                                    Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).ColumnSpan = columnSpanOfYear
                                    '幅
                                    CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Width = CType(widthSum, Unit)
                                    '居中
                                    Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center
                                End If
                            End If

                        Case 2
                            '工事比率の行結合
                            If monthIndex < 12 Then
                                '各月

                                intBeginCellIndex = monthIndex * itemCountOfMonth

                                '結合の項目数
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).RowSpan = 2
                                '高
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Height = CType(37, Unit)
                                '文字
                                CType(Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Controls(1), Label).Text = "工事比率<br>（計算用）"
                                '居中
                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).HorizontalAlign = HorizontalAlign.Center
                            End If

                        Case 3
                            intBeginCellIndex = monthIndex * itemCountOfMonth

                            '工事比率の行結合
                            If monthIndex < 12 Then

                                Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex).Visible = False
                            End If

                            '「件数」、「金額」、「粗利」居中
                            If monthIndex < 12 Then
                                '各月
                                For i As Integer = 1 To itemCountOfMonth - 1
                                    Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).HorizontalAlign = HorizontalAlign.Center
                                Next
                            Else
                                '年間
                                If columnSpanOfYear > 0 Then
                                    For i As Integer = 0 To itemCountOfYear - 1
                                        Me.gridviewRight.Rows(rowIndex).Cells(intBeginCellIndex + i).HorizontalAlign = HorizontalAlign.Center
                                    Next
                                End If
                            End If
                    End Select

                End If 'If Not monthShowFlg(monthIndex) Then

            Next 'For monthIndex As Integer = 0 To 12

            '><><><><><><><><><><><><><><><><><↑右側↑><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><

        Next 'For rowIndex As Integer = 0 To Me.grdItiranRight.Rows.Count - 1

    End Sub


    Private Function SetCellWidth(ByVal intMaxWith As Integer) As Integer
        If Not chkKeikaku.Checked Then

            intMaxWith = intMaxWith - 250
        End If
        If Not chkMikomi.Checked Then

            intMaxWith = intMaxWith - 250
        End If
        If Not chkJisseki.Checked Then

            intMaxWith = intMaxWith - 250
        End If
        If Not chkTassei.Checked Then

            intMaxWith = intMaxWith - 250
        End If
        If Not chkSintyoku.Checked Then

            intMaxWith = intMaxWith - 250
        End If
        Return intMaxWith
    End Function
    Private Sub GridStyle(ByVal grdGrid As GridView, ByVal grdRow As GridViewRow)
        For intCol As Integer = 0 To CInt(ViewState("intKaisiCol")) - 1
            grdRow.Cells(intCol).Visible = False
        Next
        For intCol As Integer = CInt(ViewState("intSyuuryouCol")) + 1 To grdGrid.Columns.Count - 1
            grdRow.Cells(intCol).Visible = False
        Next
        Dim strTmp As String = ""
        Dim strKaisiNen As String = ""
        Dim intSpan As Integer = 0
        Dim intST As Integer = 0
        Dim intTmp As Integer = 0
        Dim intMaxWith As Integer = SetCellWidth(CInt(ViewState("intMaxWidth")))
        Dim intDan As Integer = 0

        For intCol As Integer = CInt(ViewState("intKaisiCol")) To CInt(ViewState("intSyuuryouCol"))
            If intCol >= 228 Then
                intDan = 227
            Else
                If intCol / 19 = intCol \ 19 Then
                    intDan = intCol
                End If
            End If


            If Not chkKeikaku.Checked Then

                If ((intCol - intDan) = 4 OrElse (intCol - intDan) = 5 OrElse (intCol - intDan) = 6) Then
                    grdRow.Cells(intCol).Visible = False
                End If

            End If

            If Not chkMikomi.Checked Then
                If ((intCol - intDan) = 7 OrElse (intCol - intDan) = 8 OrElse (intCol - intDan) = 9) Then
                    grdRow.Cells(intCol).Visible = False
                End If

            End If
            If Not chkJisseki.Checked Then
                If ((intCol - intDan) = 10 OrElse (intCol - intDan) = 11 OrElse (intCol - intDan) = 12) Then
                    grdRow.Cells(intCol).Visible = False
                End If

            End If
            If Not chkTassei.Checked Then
                If ((intCol - intDan) = 13 OrElse (intCol - intDan) = 14 OrElse (intCol - intDan) = 15) Then
                    grdRow.Cells(intCol).Visible = False
                End If

            End If
            If Not chkSintyoku.Checked Then
                If ((intCol - intDan) = 16 OrElse (intCol - intDan) = 17 OrElse (intCol - intDan) = 18) Then
                    grdRow.Cells(intCol).Visible = False
                End If

            End If
        Next
        For intCol As Integer = CInt(ViewState("intKaisiCol")) To CInt(ViewState("intSyuuryouCol"))
            If grdGrid.ID = "gridviewRight" Then

                Select Case grdRow.RowIndex
                    Case 0, 1
                        If intCol = CInt(ViewState("intKaisiCol")) Then
                            strKaisiNen = CType(grdRow.Cells(intCol).Controls(1), Label).Text
                            If grdRow.RowIndex = 1 Then
                                strKaisiNen = strKaisiNen & CType(grdGrid.Rows(grdRow.RowIndex - 1).Cells(intCol).Controls(1), Label).Text
                            End If

                            intSpan = 1
                            intST = intCol
                        End If
                        If grdRow.RowIndex = 1 Then
                            strTmp = CType(grdRow.Cells(intCol).Controls(1), Label).Text & CType(grdGrid.Rows(grdRow.RowIndex - 1).Cells(intCol).Controls(1), Label).Text
                        Else
                            strTmp = CType(grdRow.Cells(intCol).Controls(1), Label).Text
                        End If
                        If strKaisiNen <> strTmp Then
                            grdRow.Cells(intST).ColumnSpan = intSpan - 1
                            If intST <> 228 Then
                                If intST / 19 = intST \ 19 Then
                                    CType(grdRow.Cells(intST).Controls(1), Label).Width = CType(intMaxWith, Unit)
                                End If
                            Else
                                CType(grdRow.Cells(intST).Controls(1), Label).Width = CType(intMaxWith - 73, Unit)
                            End If
                            CType(grdRow.Cells(intST).Controls(1), Label).Style.Add("text-align", "center")
                            For intTmp = intST + 1 To intCol - 1
                                grdRow.Cells(intTmp).Visible = False
                            Next
                            strKaisiNen = CType(grdRow.Cells(intCol).Controls(1), Label).Text
                            If grdRow.RowIndex = 1 Then
                                strKaisiNen = strKaisiNen & CType(grdGrid.Rows(grdRow.RowIndex - 1).Cells(intCol).Controls(1), Label).Text
                            End If
                            intSpan = 1
                            intST = intCol
                        End If
                        If grdRow.Cells(intCol).Visible Then
                            intSpan = intSpan + 1
                        End If

                    Case 2
                        If intCol = CInt(ViewState("intKaisiCol")) Then
                            strKaisiNen = CType(grdRow.Cells(intCol).Controls(1), Label).Text

                            intSpan = 1
                            intST = intCol
                        End If

                        If strKaisiNen <> CType(grdRow.Cells(intCol).Controls(1), Label).Text Then
                            If (intST / 19 = intST \ 19 And intST <> 228) OrElse (intCol = CInt(ViewState("intKaisiCol"))) Then
                                grdRow.Cells(intST).RowSpan = 2
                                CType(grdRow.Cells(intST).Controls(1), Label).Text = "工事比率<br>（計算用）"
                            Else
                                CType(grdRow.Cells(intST).Controls(1), Label).Width = CType("250", Unit)

                            End If
                            CType(grdRow.Cells(intST).Controls(1), Label).Style.Add("text-align", "center")
                            grdRow.Cells(intST).ColumnSpan = intSpan - 1


                            For intTmp = intST + 1 To intCol - 1
                                grdRow.Cells(intTmp).Visible = False
                            Next

                            strKaisiNen = CType(grdRow.Cells(intCol).Controls(1), Label).Text
                            intSpan = 1
                            intST = intCol
                        End If

                        intSpan = intSpan + 1

                    Case 3
                        If intCol / 19 = intCol \ 19 And intCol <> 228 Then
                            grdRow.Cells(intCol).Visible = False
                        End If
                        CType(grdRow.Cells(intCol).Controls(1), Label).Style.Add("text-align", "center")
                End Select
            End If
        Next
        If grdGrid.ID = "gridviewRight" Then
            Select Case grdRow.RowIndex
                Case 0, 1
                    grdRow.Cells(intST).ColumnSpan = intSpan - 1
                    If intST <> 228 Then
                        CType(grdRow.Cells(intST).Controls(1), Label).Width = CType(intMaxWith, Unit)
                    Else
                        CType(grdRow.Cells(intST).Controls(1), Label).Width = CType(intMaxWith - 73, Unit)
                    End If
                    CType(grdRow.Cells(intST).Controls(1), Label).Style.Add("text-align", "center")
                    For intTmp = intST + 1 To CInt(ViewState("intSyuuryouCol"))
                        grdRow.Cells(intTmp).Visible = False
                    Next
                Case 2
                    grdRow.Cells(intST).ColumnSpan = intSpan - 1
                    CType(grdRow.Cells(intST).Controls(1), Label).Width = CType("250", Unit)
                    CType(grdRow.Cells(intST).Controls(1), Label).Style.Add("text-align", "center")
                    For intTmp = intST + 1 To CInt(ViewState("intSyuuryouCol"))
                        grdRow.Cells(intTmp).Visible = False
                    Next


            End Select

        End If
    End Sub

    Private Function GridColIndexSet(ByVal intColCount As Integer) As String
        Dim intColIndex As Integer = 0
        Dim intTmp As Integer = 0
        If chkKongetu.Checked Then
            If strSysTuki - 4 < 0 Then
                intColIndex = strSysTuki + 8
            Else
                intColIndex = strSysTuki - 4
            End If

            Return (intColIndex) * 19 & "," & ((intColIndex + 1) * 19 - 1)
        End If

        If chkSangetu.Checked Then

            If strSysTuki = 5 Then
                intColIndex = 1
            ElseIf strSysTuki = 4 Then
                intColIndex = 0
            Else
                If strSysTuki - 6 < 0 Then
                    intColIndex = strSysTuki + 6
                Else
                    intColIndex = strSysTuki - 6
                End If
            End If

            intTmp = (intColIndex) * 19
            If strSysTuki - 4 < 0 Then
                intColIndex = strSysTuki + 8
            Else
                intColIndex = strSysTuki - 4
            End If

            Return intTmp & "," & ((intColIndex + 1) * 19 - 1)
        End If
        If chkYogetu.Checked Then
            If strSysTuki - 4 < 0 Then
                intColIndex = strSysTuki + 8
            Else
                intColIndex = strSysTuki - 4
            End If

            intTmp = (intColIndex) * 19
            If strSysTuki <= 3 OrElse strSysTuki = 12 Then
                Return intTmp & "," & intColCount - 18
            Else

                Return intTmp & "," & (strSysTuki - 1) * 19 - 1
            End If
        End If
        Return "0," & intColCount
    End Function

    Protected Sub linMae_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linMae.Click
        ViewState("Page") = CType(ViewState("Page"), Integer) - 1


        SetPage(CType(ViewState("Page"), Integer), False)
    End Sub

    Protected Sub lnkAto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAto.Click
        ViewState("Page") = CType(ViewState("Page"), Integer) + 1


        SetPage(CType(ViewState("Page"), Integer), False)
    End Sub
End Class
