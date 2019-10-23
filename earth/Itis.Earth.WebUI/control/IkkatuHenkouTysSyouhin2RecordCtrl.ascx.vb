Imports System.Collections

Partial Public Class IkkatuHenkouTysSyouhin2RecordCtrl
    Inherits System.Web.UI.UserControl

    Private cl As New CommonLogic
    Private dicKey As New IkkatuHenkouTysSyouhinLogic
    Private cbLogic As New CommonBizLogic

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '****************************************************************************
        ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
        '****************************************************************************
        setDispAction()
    End Sub

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' 商品の活性制御
            For intCnt As Integer = 1 To 4
                '指定行の商品コントロールを取得
                Dim ctrlRow As IkkatuSyouhin2CtrlReord = getItem2RowInfo("2_" & intCnt)
                enableItem2(ctrlRow)
            Next
        End If
    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusScript As String = "removeFig(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        Dim onChgScript(4) As String
        Dim onChangeItem(4) As String
        Dim onChangeKingaku(4) As String
        For i As Integer = LBound(onChgScript) To UBound(onChgScript)
            onChgScript(i) = "rowChgItem2('" & Me.ClientID & "', " & (i + 1) & ");"
            onChangeItem(i) = "clearMeisaiKingakuText('" & Me.ClientID & "', " & (i + 1) & ");"
            onChangeKingaku(i) = "setKingakuItem2(this, '" & Me.ClientID & "', " & (i + 1) & ");"
        Next

        Dim onClickAddBtn(4) As String
        For i As Integer = LBound(onChgScript) To UBound(onChgScript)
            onClickAddBtn(i) = "rowAdd(this);"
        Next

        Dim onClickDelBtn(4) As String
        For i As Integer = LBound(onChgScript) To UBound(onChgScript)
            onClickDelBtn(i) = "rowDelete(this);"
        Next

        For intCnt As Integer = 1 To 4
            '指定行の商品コントロールを取得
            Dim ctrlRec As IkkatuSyouhin2CtrlReord = getItem2RowInfo("2_" & intCnt)
            With ctrlRec
                '+++++++++++++++++++++++++++++++++++++++++++++++++++
                '+ 金額系
                '+++++++++++++++++++++++++++++++++++++++++++++++++++
                '************
                '* 画面下部
                '************
                '商品コンボ
                .Syouhin.Attributes("onchange") = onChgScript(intCnt - 1) & onChangeItem(intCnt - 1)

                '工務店請求金額
                .KoumutenKingaku.Attributes("onfocus") = onFocusScript
                .KoumutenKingaku.Attributes("onblur") = checkKingaku
                .KoumutenKingaku.Attributes("onkeydown") = disabledOnkeydown
                .KoumutenKingaku.Attributes("onchange") = onChangeKingaku(intCnt - 1)

                '実請求金額
                .JituSeikyuuKingaku.Attributes("onfocus") = onFocusScript
                .JituSeikyuuKingaku.Attributes("onblur") = checkKingaku
                .JituSeikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown
                .JituSeikyuuKingaku.Attributes("onchange") = onChangeKingaku(intCnt - 1)

                '承諾書金額
                .SyoudakusyoKingaku.Attributes("onfocus") = onFocusScript
                .SyoudakusyoKingaku.Attributes("onblur") = checkKingaku
                .SyoudakusyoKingaku.Attributes("onkeydown") = disabledOnkeydown

                '請求有無
                .SeikyuuUmu.Attributes("onchange") = onChgScript(intCnt - 1)

                '追加ボタン
                .AddBtn.Attributes("onclick") = onClickAddBtn(intCnt - 1)

                '削除ボタン
                .DelBtn.Attributes("onclick") = onClickDelBtn(intCnt - 1)
            End With
        Next

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="item2Rec">邸別請求レコード（商品2）</param>
    ''' <remarks></remarks>
    Public Sub setCtrlFromJibanRec(ByVal jr As JibanRecordBase, ByVal Item1Rec As TeibetuSeikyuuRecord, ByVal item2Rec As Dictionary(Of Integer, TeibetuSeikyuuRecord), ByVal trHead As HtmlTableRow)
        Dim sortedList As New SortedList()
        Dim sortedListKingaku As New SortedList()
        Dim dicBlank As New Dictionary(Of String, String)
        Dim item2List As New List(Of Dictionary(Of String, String))
        Dim item2ListKingaku As New List(Of Dictionary(Of String, String))
        Dim IEnumRec As IEnumerator(Of TeibetuSeikyuuRecord)
        Dim wkTsRec As TeibetuSeikyuuRecord
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim kameitenRec As New KameitenSearchRecord
        Dim blnTorikesi As Boolean = False
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strRecString As String
        Dim strKingakuString As String
        Dim jibanLogic As New JibanLogic
        Dim itemRec As Syouhin23Record
        Dim wkDtTime As DateTime

        '****************************************************************************
        ' ドロップダウンリスト設定
        '****************************************************************************
        Dim helper As New DropDownHelper
        ' 商品コード2コンボにデータをバインドする
        helper.SetDropDownList(Me.SelectSyouhin_2_1, DropDownHelper.DropDownType.Syouhin2Group)
        helper.SetDropDownList(Me.SelectSyouhin_2_2, DropDownHelper.DropDownType.Syouhin2Group)
        helper.SetDropDownList(Me.SelectSyouhin_2_3, DropDownHelper.DropDownType.Syouhin2Group)
        helper.SetDropDownList(Me.SelectSyouhin_2_4, DropDownHelper.DropDownType.Syouhin2Group)

        If item2Rec IsNot Nothing Then
            IEnumRec = item2Rec.Values.GetEnumerator
            While IEnumRec.MoveNext
                wkTsRec = IEnumRec.Current
                Dim dic As New Dictionary(Of String, String)
                Dim dicKingaku As New Dictionary(Of String, String)
                'IkkatuHenkouTysSyouhinLogic.getDicItem2メソッドと同じ順番で登録すること
                With dic
                    '地盤テーブルから取得
                    .Add(dicKey.dkKameitenCd, cl.GetDisplayString(jr.KameitenCd))                               '加盟店コード --------------- (0)
                    '邸別請求テーブル(商品１)から取得
                    .Add(dicKey.dkUriKeijyouFlgItem1, cl.GetDisplayString(Item1Rec.UriKeijyouFlg))              '売上計上フラグ(商品１) ----- (1)
                    .Add(dicKey.dkUriDateItem1, cl.GetDisplayString(Item1Rec.UriDate))                          '商品1の売上年月日 -----------(2)
                    '邸別請求テーブル(商品２)から取得
                    .Add(dicKey.dkKbn, cl.GetDisplayString(wkTsRec.Kbn))                                        '区分 ----------------------- (3)
                    .Add(dicKey.dkHosyousyoNo, cl.GetDisplayString(wkTsRec.HosyousyoNo))                        '保証書NO ------------------- (4)
                    .Add(dicKey.dkBunruiCd, cl.GetDisplayString(wkTsRec.BunruiCd))                              '分類コード ----------------- (5)
                    .Add(dicKey.dkGamenHyoujiNo, cl.GetDisplayString(wkTsRec.GamenHyoujiNo))                    '画面表示NO ----------------- (6)
                    .Add(dicKey.dkSyouhinCd, cl.GetDisplayString(wkTsRec.SyouhinCd))                            '商品コード ----------------- (7)
                    .Add(dicKey.dkZeiKbn, cl.GetDisplayString(wkTsRec.ZeiKbn))                                  '税区分 --------------------- (8)
                    .Add(dicKey.dkSeikyuusyoHakDate, cl.GetDisplayString(wkTsRec.SeikyuusyoHakDate))            '請求書発行日 ----------------(9)
                    .Add(dicKey.dkUriDate, cl.GetDisplayString(wkTsRec.UriDate))                                '売上年月日 ----------------- (10)
                    .Add(dicKey.dkSeikyuuUmu, cl.GetDisplayString(wkTsRec.SeikyuuUmu))                          '請求有無 ------------------- (11)
                    .Add(dicKey.dkKakuteiKbn, cl.GetDisplayString(wkTsRec.KakuteiKbn))                          '確定区分 ------------------- (12)
                    .Add(dicKey.dkUriKeijyouFlg, cl.GetDisplayString(wkTsRec.UriKeijyouFlg))                    '売上計上フラグ ------------- (13)
                    .Add(dicKey.dkUriKeijouDate, cl.GetDisplayString(wkTsRec.UriKeijyouDate))                   '売上計上日 ----------------- (14)
                    .Add(dicKey.dkBikou, cl.GetDisplayString(wkTsRec.Bikou))                                    '備考 ----------------------- (15)
                    .Add(dicKey.dkHattyuusyoGaku, cl.GetDisplayString(wkTsRec.HattyuusyoGaku))                                      '発注書金額 --------- (16)
                    .Add(dicKey.dkHattyuusyoKakuninDate, cl.GetDisplayString(wkTsRec.HattyuusyoKakuninDate))                        '発注書確認日 ------- (17)
                    .Add(dicKey.dkIkkatuNyuukinFlg, cl.GetDisplayString(wkTsRec.IkkatuNyuukinFlg))                                  '一括入金フラグ ----- (18)
                    .Add(dicKey.dkTysMitsyoSakuseiDate, cl.GetDisplayString(wkTsRec.TysMitsyoSakuseiDate))                          '調査見積書作成日 --- (19)
                    .Add(dicKey.dkHattyuusyoKakuteiFlg, cl.GetDisplayString(wkTsRec.HattyuusyoKakuteiFlg))                          '発注書確定フラグ --- (20)
                    .Add(dicKey.dkAddLoginUserId, cl.GetDisplayString(wkTsRec.AddLoginUserId))                                      '登録ログインユーザーID   --- (21)

                    wkDtTime = wkTsRec.AddDatetime
                    If wkDtTime = DateTime.MinValue Then
                        .Add(dicKey.dkAddDatetime, cl.GetDisplayString(wkTsRec.AddDatetime))                                            '登録日時 ------------------- (22)
                    Else
                        .Add(dicKey.dkAddDatetime, cl.GetDisplayString(wkTsRec.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1)))    '登録日時 ------------------- (22)
                    End If
                    .Add(dicKey.dkUpdLoginUserId, cl.GetDisplayString(wkTsRec.UpdLoginUserId))                                          '更新ログインユーザーID   --- (23)
                    wkDtTime = wkTsRec.UpdDatetime
                    If wkDtTime = DateTime.MinValue Then
                        .Add(dicKey.dkUpdDatetime, cl.GetDisplayString(wkTsRec.UpdDatetime))                                            '更新日時 ------------------- (24)
                    Else
                        .Add(dicKey.dkUpdDatetime, cl.GetDisplayString(wkTsRec.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1)))    '更新日時 ------------------- (24)
                    End If
                    .Add(dicKey.dkSeikyuuSaki, cl.GetDisplayString(wkTsRec.SeikyuuSakiCd))                                              '請求先コード --------------- (25)
                End With

                'IkkatuHenkouTysSyouhinLogic.getDicItem2Kingakuメソッドと同じ順番で登録すること
                With dicKingaku
                    .Add(dicKey.dkGamenHyoujiNo, cl.GetDisplayString(wkTsRec.GamenHyoujiNo))
                    .Add(dicKey.dkKoumutenSeikyuuGaku, cl.GetDisplayString(wkTsRec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)))
                    .Add(dicKey.dkUriGaku, cl.GetDisplayString(wkTsRec.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)))
                    .Add(dicKey.dkSiireGaku, cl.GetDisplayString(wkTsRec.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1)))
                End With

                '画面表示Noでソートしながらリストへ追加
                sortedList.Add(dic(dicKey.dkGamenHyoujiNo), dic)
                sortedListKingaku.Add(dic(dicKey.dkGamenHyoujiNo), dicKingaku)

            End While
        End If

        'ソート済みリストで画面表示Noが歯抜けの箇所に空のDictionaryを追加
        For intCnt As Integer = 1 To 4
            If intCnt <= sortedList.Count Then
                If intCnt.ToString <> sortedList.GetByIndex(intCnt - 1)("GamenHyoujiNo") Then
                    sortedList.Add(intCnt.ToString, dicBlank)
                    sortedListKingaku.Add(intCnt.ToString, dicBlank)
                End If
            Else
                sortedList.Add(intCnt.ToString, dicBlank)
                sortedListKingaku.Add(intCnt.ToString, dicBlank)
            End If
        Next
        'ソート済みリストから通常のリストへ移送
        For intCnt As Integer = 0 To 3
            item2List.Add(sortedList.GetByIndex(intCnt))
            item2ListKingaku.Add(sortedListKingaku.GetByIndex(intCnt))
        Next

        '取得したデータを画面へ設定
        For intCnt As Integer = 0 To 3
            Dim ctrlRec As IkkatuSyouhin2CtrlReord = getItem2RowInfo("2_" & (intCnt + 1))
            With ctrlRec
                'セルの幅を設定
                For cellsCnt As Integer = 0 To ctrlRec.SyouhinLine.Controls.Count - 1
                    Dim meisaiCell As HtmlTableCell = ctrlRec.SyouhinLine.Controls(cellsCnt)
                    Dim headCell As HtmlTableCell = trHead.Controls(cellsCnt)
                    meisaiCell.Style("width") = headCell.Style("width")
                Next

                If item2List(intCnt).Count > 0 Then
                    If intCnt = 0 Then
                        '商品１の売上年月日
                        .UriDateItem1.Value = item2List(intCnt)(dicKey.dkUriDateItem1)
                    End If

                    '区分
                    .Kbn.Value = item2List(intCnt)(dicKey.dkKbn)
                    '保証書NO
                    .HosyousyoNo.Value = item2List(intCnt)(dicKey.dkHosyousyoNo)
                    '分類コード
                    .BunruiCd.Value = item2List(intCnt)(dicKey.dkBunruiCd)
                    '画面表示NO
                    .GamenHyoujiNo.Value = item2List(intCnt)(dicKey.dkGamenHyoujiNo)
                    '画面表示NO
                    .No.Text = item2List(intCnt)(dicKey.dkGamenHyoujiNo)

                    '加盟店コード
                    .KameitenCd.Value = item2List(intCnt)(dicKey.dkKameitenCd)
                    '加盟店マスタからデータを取得
                    kameitenRec = kameitenSearchLogic.GetKameitenSearchResult(item2List(intCnt)(dicKey.dkKbn), _
                                                                              item2List(intCnt)(dicKey.dkKameitenCd), _
                                                                              "", _
                                                                              blnTorikesi)
                    '系列コード
                    .KeiretuCd.Value = cl.GetDisplayString(kameitenRec.KeiretuCd)

                    '直接請求/他請求の取得
                    itemRec = jibanLogic.GetSyouhinInfo(item2List(intCnt)(dicKey.dkSyouhinCd), EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi, item2List(intCnt)(dicKey.dkKameitenCd))
                    If item2List(intCnt)(dicKey.dkSeikyuuSaki) <> String.Empty Then
                        itemRec.SeikyuuSakiCd = item2List(intCnt)(dicKey.dkSeikyuuSaki)
                        '請求先コード
                        .SeikyuuSakiCd.Value = item2List(intCnt)(dicKey.dkSeikyuuSaki)
                    End If

                    '調査請求先
                    .TysSeikyuuSaki.Value = itemRec.SeikyuuSakiType
                    '加盟店取消
                    .Torikeshi.Value = cl.GetDisplayString(kameitenRec.Torikesi)

                    '顧客番号
                    .KokyakuBangou.Text = item2List(intCnt)(dicKey.dkKbn) & item2List(intCnt)(dicKey.dkHosyousyoNo)

                    '商品コード
                    Dim strSyouhinCd As String = item2List(intCnt)(dicKey.dkSyouhinCd)
                    '商品コードの存在チェック
                    If cl.ChkDropDownList(.Syouhin, strSyouhinCd) Then
                        .Syouhin.SelectedValue = strSyouhinCd
                    Else '未存在の場合、項目追加
                        '商品名の取得
                        Dim syouhinLogic As New SyouhinSearchLogic
                        Dim intDataCnt As Integer
                        Dim listSyouhin As List(Of SyouhinMeisaiRecord) = _
                                                        syouhinLogic.GetSyouhinInfo( _
                                                                                    strSyouhinCd _
                                                                                    , String.Empty _
                                                                                    , EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi _
                                                                                    , intDataCnt _
                                                                                    , 1, 1)
                        '商品名のセット
                        Dim strSyouhinMei As String = String.Empty
                        If listSyouhin.Count = 1 Then
                            strSyouhinMei = listSyouhin(0).SyouhinMei
                        End If

                        .Syouhin.Items.Add(New ListItem(strSyouhinCd & ":" & strSyouhinMei, strSyouhinCd)) '商品コード
                        .Syouhin.SelectedValue = strSyouhinCd '選択状態
                    End If

                    '税区分
                    .ZeiKbn.Value = item2List(intCnt)(dicKey.dkZeiKbn)
                    '売上年月日
                    .UriDate.Value = item2List(intCnt)(dicKey.dkUriDate)
                    '発注書確定フラグ
                    .HattyuusyoKakuteiFlg.Value = item2List(intCnt)(dicKey.dkHattyuusyoKakuteiFlg)
                    '発注書金額
                    .HattyuusyoGaku.Value = item2List(intCnt)(dicKey.dkHattyuusyoGaku)

                    '売上処理
                    If item2List(intCnt)(dicKey.dkUriKeijyouFlgItem1) = "0" Then
                        .UriageSyori.Text = ""
                    Else
                        .UriageSyori.Text = EarthConst.KEIJOU_ZUMI
                    End If

                    '工務店請求額
                    .KoumutenKingaku.Text = item2ListKingaku(intCnt)(dicKey.dkKoumutenSeikyuuGaku)
                    '実請求金額
                    .JituSeikyuuKingaku.Text = item2ListKingaku(intCnt)(dicKey.dkUriGaku)
                    '承諾書金額
                    .SyoudakusyoKingaku.Text = item2ListKingaku(intCnt)(dicKey.dkSiireGaku)

                    '請求有無
                    If item2List(intCnt)(dicKey.dkSeikyuuUmu) = "0" Then
                        .SeikyuuUmu.SelectedValue = "0"
                    Else
                        .SeikyuuUmu.SelectedValue = "1"
                    End If

                    '商品退避用Hiddenに現在の商品情報を設定
                    .SyouhinBK.Value = item2List(intCnt)(dicKey.dkSyouhinCd)
                    .SyouhinKingakuBK.Value = .KoumutenKingaku.Text & EarthConst.SEP_STRING & _
                                                .JituSeikyuuKingaku.Text & EarthConst.SEP_STRING & _
                                                .SyoudakusyoKingaku.Text & EarthConst.SEP_STRING & _
                                                .SeikyuuUmu.SelectedValue

                    '画面表示時のDB値の連結値を取得
                    strRecString = clsLogic.getJoinString(item2List(intCnt).Values.GetEnumerator)
                    .DbValue.Value = strRecString
                    .ChgValue.Value = strRecString

                    '画面表示金額のDB値の連結値を取得
                    strKingakuString = clsLogic.getJoinString(item2ListKingaku(intCnt).Values.GetEnumerator)
                    .DbKingaku.Value = strKingakuString
                    .ChgKingaku.Value = strKingakuString
                Else
                    '行を非表示
                    .SyouhinLine.Style("display") = "none"
                    .RowDisplay.Value = "none"

                    If intCnt = 0 Then
                        '商品１の売上年月日
                        .UriDateItem1.Value = cl.GetDisplayString(Item1Rec.UriDate)
                    End If

                    '区分
                    .Kbn.Value = cl.GetDisplayString(jr.Kbn)
                    '保証書NO
                    .HosyousyoNo.Value = cl.GetDisplayString(jr.HosyousyoNo)
                    '顧客番号
                    .KokyakuBangou.Text = cl.GetDisplayString(jr.Kbn) & cl.GetDisplayString(jr.HosyousyoNo)

                    '加盟店コード
                    .KameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)

                    '加盟店マスタからデータを取得
                    kameitenRec = kameitenSearchLogic.GetKameitenSearchResult(.Kbn.Value, _
                                                                              .KameitenCd.Value, _
                                                                              "", _
                                                                              blnTorikesi)
                    '系列コード
                    .KeiretuCd.Value = cl.GetDisplayString(kameitenRec.KeiretuCd)
                    '調査請求先
                    .TysSeikyuuSaki.Value = String.Empty
                    '加盟店取消
                    .Torikeshi.Value = cl.GetDisplayString(kameitenRec.Torikesi)
                End If
            End With
        Next

        '非表示行の画面表示NOの設定
        For intCntI As Integer = 1 To 4
            For intCntJ As Integer = 1 To 4
                Dim ctrlRec As IkkatuSyouhin2CtrlReord = getItem2RowInfo("2_" & (intCntJ))
                With ctrlRec
                    If .SyouhinLine.Style("display") <> "none" And .GamenHyoujiNo.Value = cl.GetDisplayString(intCntI) Then
                        Exit For
                    End If

                    If .SyouhinLine.Style("display") = "none" And .GamenHyoujiNo.Value = "" Then
                        .GamenHyoujiNo.Value = intCntI
                        .No.Text = intCntI
                        Exit For
                    End If
                End With
            Next
        Next

    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks>
    ''' コード入力値変更チェック<br/>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Public Sub checkInput( _
                            ByRef errMess As String, _
                            ByRef arrFocusTargetCtrl As List(Of Control) _
                          )

        '地盤画面共通クラス
        Dim jBn As New Jiban

        Dim strErrGyouInfo As String
        Dim strBlnk As String = EarthConst.BRANK_STRING
        Dim ctrlRow As IkkatuSyouhin2CtrlReord

        ' 商品の活性制御
        For intCnt As Integer = 1 To 4
            '指定行の商品コントロールを取得
            ctrlRow = getItem2RowInfo("2_" & intCnt)
            enableItem2(ctrlRow)
        Next

        '●コード入力値変更チェック
        'なし

        '●必須チェック
        '商品2_1
        If Me.TrTysSyouhin_2_1.Style("display") <> "none" Then '行表示時
            If Me.SelectSyouhin_2_1.SelectedValue = String.Empty Then '商品コード=空白
                strErrGyouInfo = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou_2_1.Text & strBlnk
                If Me.HiddenHattyuusyoGaku_2_1.Value <> "0" And Me.HiddenHattyuusyoGaku_2_1.Value <> "" Then
                    '発注書金額≠0かつ≠NULLの場合
                    errMess &= strErrGyouInfo & "商品2_1は" & Messages.MSG010E & "\r\n"
                    Me.SelectSyouhin_2_1.SelectedValue = Me.HiddenSelectSyouhin_2_1.Value
                    ctrlRow = getItem2RowInfo("2_1")
                    '商品情報の復元
                    Dim strItem2Info() As String
                    strItem2Info = Split(Me.HiddenSyouhinKingaku_2_1.Value, EarthConst.SEP_STRING)
                    Me.TextKoumutenKingaku_2_1.Text = strItem2Info(0)
                    Me.TextJituSeikyuuKingaku_2_1.Text = strItem2Info(1)
                    Me.TextSyoudakusyoKingaku_2_1.Text = strItem2Info(2)
                    Me.SelectSeikyuuUmu_2_1.SelectedValue = strItem2Info(3)
                    enableItem2(ctrlRow)
                Else
                    errMess &= strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "商品2_1")
                End If
                arrFocusTargetCtrl.Add(Me.SelectSyouhin_2_1)
            End If
        End If
        '商品2_2
        If Me.TrTysSyouhin_2_2.Style("display") <> "none" Then '行表示時
            If Me.SelectSyouhin_2_2.SelectedValue = String.Empty Then '商品コード=空白
                strErrGyouInfo = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou_2_2.Text & strBlnk
                If Me.HiddenHattyuusyoGaku_2_2.Value <> "0" And Me.HiddenHattyuusyoGaku_2_2.Value <> "" Then
                    '発注書金額≠0かつ≠NULLの場合
                    errMess &= strErrGyouInfo & "商品2_2は" & Messages.MSG010E & "\r\n"
                    Me.SelectSyouhin_2_2.SelectedValue = Me.HiddenSelectSyouhin_2_2.Value
                    ctrlRow = getItem2RowInfo("2_2")
                    '商品情報の復元
                    Dim strItem2Info() As String
                    strItem2Info = Split(Me.HiddenSyouhinKingaku_2_2.Value, EarthConst.SEP_STRING)
                    Me.TextKoumutenKingaku_2_2.Text = strItem2Info(0)
                    Me.TextJituSeikyuuKingaku_2_2.Text = strItem2Info(1)
                    Me.TextSyoudakusyoKingaku_2_2.Text = strItem2Info(2)
                    Me.SelectSeikyuuUmu_2_2.SelectedValue = strItem2Info(3)
                    enableItem2(ctrlRow)
                Else
                    errMess &= strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "商品2_2")
                End If
                arrFocusTargetCtrl.Add(Me.SelectSyouhin_2_2)
            End If
        End If
        '商品2_3
        If Me.TrTysSyouhin_2_3.Style("display") <> "none" Then '行表示時
            If Me.SelectSyouhin_2_3.SelectedValue = String.Empty Then '商品コード=空白
                strErrGyouInfo = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou_2_3.Text & strBlnk
                If Me.HiddenHattyuusyoGaku_2_3.Value <> "0" And Me.HiddenHattyuusyoGaku_2_3.Value <> "" Then
                    '発注書金額≠0かつ≠NULLの場合
                    errMess &= strErrGyouInfo & "商品2_3は" & Messages.MSG010E & "\r\n"
                    Me.SelectSyouhin_2_3.SelectedValue = Me.HiddenSelectSyouhin_2_3.Value
                    ctrlRow = getItem2RowInfo("2_3")
                    '商品情報の復元
                    Dim strItem2Info() As String
                    strItem2Info = Split(Me.HiddenSyouhinKingaku_2_3.Value, EarthConst.SEP_STRING)
                    Me.TextKoumutenKingaku_2_3.Text = strItem2Info(0)
                    Me.TextJituSeikyuuKingaku_2_3.Text = strItem2Info(1)
                    Me.TextSyoudakusyoKingaku_2_3.Text = strItem2Info(2)
                    Me.SelectSeikyuuUmu_2_3.SelectedValue = strItem2Info(3)
                    enableItem2(ctrlRow)
                Else
                    errMess &= strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "商品2_3")
                End If
                arrFocusTargetCtrl.Add(Me.SelectSyouhin_2_3)
            End If
        End If
        '商品2_4
        If Me.TrTysSyouhin_2_4.Style("display") <> "none" Then '行表示時
            If Me.SelectSyouhin_2_4.SelectedValue = String.Empty Then '商品コード=空白
                strErrGyouInfo = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou_2_4.Text & strBlnk
                If Me.HiddenHattyuusyoGaku_2_4.Value <> "0" And Me.HiddenHattyuusyoGaku_2_4.Value <> "" Then
                    '発注書金額≠0かつ≠NULLの場合
                    errMess &= strErrGyouInfo & "商品2_4は" & Messages.MSG010E & "\r\n"
                    Me.SelectSyouhin_2_4.SelectedValue = Me.HiddenSelectSyouhin_2_4.Value
                    ctrlRow = getItem2RowInfo("2_4")
                    '商品情報の復元
                    Dim strItem2Info() As String
                    strItem2Info = Split(Me.HiddenSyouhinKingaku_2_4.Value, EarthConst.SEP_STRING)
                    Me.TextKoumutenKingaku_2_4.Text = strItem2Info(0)
                    Me.TextJituSeikyuuKingaku_2_4.Text = strItem2Info(1)
                    Me.TextSyoudakusyoKingaku_2_4.Text = strItem2Info(2)
                    Me.SelectSeikyuuUmu_2_4.SelectedValue = strItem2Info(3)
                    enableItem2(ctrlRow)
                Else
                    errMess &= strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "商品2_4")
                End If
                arrFocusTargetCtrl.Add(Me.SelectSyouhin_2_4)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 金額の整合性チェック
    ''' </summary>
    ''' <param name="arrFocusTargetCtrl">フォーカス対象リスト</param>
    ''' <returns>エラーメッセージ</returns>
    ''' <remarks>
    ''' 直接請求の場合、工務店請求額と実請求額は等しくなければいけない。
    ''' 請求有・3系列の場合、工務店請求額・実請求額のどちらかのみ変更された際、最低一回は計算処理必須。
    ''' </remarks>
    Public Function checkKingaku(ByRef arrFocusTargetCtrl As List(Of Control)) As String
        Dim errMess As String = ""
        Dim strErrGyouInfo(4) As String
        Dim strBlnk As String = EarthConst.BRANK_STRING
        Dim jbLogic As New JibanLogic
        Dim intKeiretuFlg As Integer = 0

        strErrGyouInfo(0) = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou_2_1.Text & strBlnk & "商品2_1" & strBlnk
        strErrGyouInfo(1) = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou_2_2.Text & strBlnk & "商品2_2" & strBlnk
        strErrGyouInfo(2) = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou_2_3.Text & strBlnk & "商品2_3" & strBlnk
        strErrGyouInfo(3) = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou_2_4.Text & strBlnk & "商品2_4" & strBlnk

        '系列フラグ
        intKeiretuFlg = jbLogic.GetKeiretuFlg(Me.HiddenKeiretuCd.Value)

        '直接請求の場合
        '商品2_1
        '○特別対応価格が反映されていない場合のみ、チェックを行う
        If String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl_2_1.AccDisplayCd.Value) Then
            If Me.HiddenTysSeikyuuSaki_2_1.Value = EarthConst.SEIKYU_TYOKUSETU Then
                If Me.TextKoumutenKingaku_2_1.Text <> Me.TextJituSeikyuuKingaku_2_1.Text Then
                    errMess &= strErrGyouInfo(0) & Messages.MSG132E
                    arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku_2_1)
                End If
            End If
        End If
        '商品2_2
        If String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl_2_2.AccDisplayCd.Value) Then
            If Me.HiddenTysSeikyuuSaki_2_2.Value = EarthConst.SEIKYU_TYOKUSETU Then
                If Me.TextKoumutenKingaku_2_2.Text <> Me.TextJituSeikyuuKingaku_2_2.Text Then
                    errMess &= strErrGyouInfo(1) & Messages.MSG132E
                    arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku_2_2)
                End If
            End If
        End If
        '商品2_3
        If String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl_2_3.AccDisplayCd.Value) Then
            If Me.HiddenTysSeikyuuSaki_2_3.Value = EarthConst.SEIKYU_TYOKUSETU Then
                If Me.TextKoumutenKingaku_2_3.Text <> Me.TextJituSeikyuuKingaku_2_3.Text Then
                    errMess &= strErrGyouInfo(2) & Messages.MSG132E
                    arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku_2_3)
                End If
            End If
        End If
        '商品2_4
        If String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl_2_4.AccDisplayCd.Value) Then
            If Me.HiddenTysSeikyuuSaki_2_4.Value = EarthConst.SEIKYU_TYOKUSETU Then
                If Me.TextKoumutenKingaku_2_4.Text <> Me.TextJituSeikyuuKingaku_2_4.Text Then
                    errMess &= strErrGyouInfo(3) & Messages.MSG132E
                    arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku_2_4)
                End If
            End If
        End If

        '請求有・3系列の場合
        '商品2_1
        If String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl_2_1.AccDisplayCd.Value) Then
            If Me.HiddenAutoKingakuFlg_2_1.Value = Me.HiddenManualKingakuFlg_2_1.Value Then
                If Me.SelectSeikyuuUmu_2_1.SelectedValue = "1" _
                        And intKeiretuFlg = 1 _
                        And Me.HiddenBothKingakuChgFlg_2_1.Value <> String.Empty _
                        And Me.HiddenBothKingakuChgFlg_2_1.Value <> "1" Then
                    errMess &= strErrGyouInfo(0) & Messages.MSG142E
                    arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku_2_1)
                End If
            End If
        End If
        '商品2_2
        If String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl_2_2.AccDisplayCd.Value) Then
            If Me.HiddenAutoKingakuFlg_2_1.Value = Me.HiddenManualKingakuFlg_2_1.Value Then
                If Me.SelectSeikyuuUmu_2_2.SelectedValue = "1" _
                        And intKeiretuFlg = 1 _
                        And Me.HiddenBothKingakuChgFlg_2_2.Value <> String.Empty _
                        And Me.HiddenBothKingakuChgFlg_2_2.Value <> "1" Then
                    errMess &= strErrGyouInfo(1) & Messages.MSG142E
                    arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku_2_2)
                End If
            End If
        End If
        '商品2_3
        If String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl_2_3.AccDisplayCd.Value) Then
            If Me.HiddenAutoKingakuFlg_2_1.Value = Me.HiddenManualKingakuFlg_2_1.Value Then
                If Me.SelectSeikyuuUmu_2_3.SelectedValue = "1" _
                                    And intKeiretuFlg = 1 _
                                    And Me.HiddenBothKingakuChgFlg_2_3.Value <> String.Empty _
                                    And Me.HiddenBothKingakuChgFlg_2_3.Value <> "1" Then
                    errMess &= strErrGyouInfo(2) & Messages.MSG142E
                    arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku_2_3)
                End If
            End If
        End If
        '商品2_4
        If String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl_2_4.AccDisplayCd.Value) Then
            If Me.HiddenAutoKingakuFlg_2_1.Value = Me.HiddenManualKingakuFlg_2_1.Value Then
                If Me.SelectSeikyuuUmu_2_4.SelectedValue = "1" _
                                        And intKeiretuFlg = 1 _
                                        And Me.HiddenBothKingakuChgFlg_2_4.Value <> String.Empty _
                                        And Me.HiddenBothKingakuChgFlg_2_4.Value <> "1" Then
                    errMess &= strErrGyouInfo(3) & Messages.MSG142E
                    arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku_2_4)
                End If
            End If
        End If

        Return errMess
    End Function

    ''' <summary>
    ''' 商品データの入力制御処理
    ''' </summary>
    ''' <param name="ctrlRow">処理対象行</param>
    ''' <remarks></remarks>
    Public Sub enableItem2(ByVal ctrlRow As IkkatuSyouhin2CtrlReord)
        '商品2の売上状況を取得
        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim jibanLogic As New JibanLogic

        With ctrlRow
            jSM.Hash2Ctrl(.SyouhinLine, EarthConst.MODE_VIEW, ht)

            .AddBtn.Disabled = True
            .DelBtn.Disabled = True

            '特別対応ツールチップ(非表示設定)
            .TokubetuTaiouToolTip.AccVisibleFlg.Value = "1"

            '売上状況を判定()
            If .UriageSyori.Text <> "" Or .Torikeshi.Value = "1" Then

            ElseIf .Syouhin.SelectedValue = "" And .SyouhinLine.Style("display") <> "none" Then

                cl.chgDispCheckBox(.AutoCalc, .AutoCalcSpan)    'チェックボックス
                cl.chgDispSyouhinPull(.Syouhin, .SyouhinSpan)   '商品コンボ
                .SeikyuuUmu.Style("display") = "none"
                .SeikyuuUmuSpan.InnerHtml = ""
                .AddBtn.Disabled = False    '追加ボタン
                .DelBtn.Disabled = False    '削除ボタン
                '発注書金額＝0または＝NULLの場合、自動設定(クリア処理)を行う
                If .HattyuusyoGaku.Value = "0" Or .HattyuusyoGaku.Value = "" Then
                    .KoumutenKingaku.Text = ""
                    .JituSeikyuuKingaku.Text = ""
                    .SyoudakusyoKingaku.Text = ""
                    .SeikyuuUmu.SelectedValue = "1"
                End If

            Else
                cl.chgDispCheckBox(.AutoCalc, .AutoCalcSpan)    'チェックボックス
                '価格反映されている場合
                If .TokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty Then
                    '商品コンボ
                    cl.chgDispSyouhinPull(.Syouhin, .SyouhinSpan)
                End If

                '追加ボタン
                .AddBtn.Disabled = False
                '発注書金額＝0または＝NULLの場合、削除ボタン使用可
                If .HattyuusyoGaku.Value = "0" Or .HattyuusyoGaku.Value = "" Then
                    '削除ボタン
                    .DelBtn.Disabled = False
                End If

                '承諾書金額
                cl.chgDispSyouhinText(.SyoudakusyoKingaku)

                '売上年月日が未入力の場合
                If .UriDateItem1.Value = "" Then
                    '価格反映されている場合
                    If .TokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty Then
                        '請求有無
                        cl.chgDispSyouhinPull(.SeikyuuUmu, .SeikyuuUmuSpan)
                    End If
                End If

                '価格反映されている場合
                If .TokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty Then
                    '請求有無の判定
                    If .SeikyuuUmu.SelectedValue = "1" Then
                        '実請求金額
                        cl.chgDispSyouhinText(.JituSeikyuuKingaku)
                        '直接請求/他請求の判定
                        If .TysSeikyuuSaki.Value = EarthConst.SEIKYU_TASETU And _
                            jibanLogic.GetKeiretuFlg(.KeiretuCd.Value) = 0 Then
                        Else
                            '工務店請求額
                            cl.chgDispSyouhinText(.KoumutenKingaku)
                        End If
                    End If
                End If

            End If
        End With


    End Sub

    ''' <summary>
    ''' 画面商品コントロールから邸別請求データに値をセットする（登録/更新処理用）
    ''' </summary>
    ''' <param name="blnUpdate">更新判断フラグ</param>
    ''' <returns>邸別請求データ格納済みHashtable</returns>
    ''' <remarks></remarks>
    Public Function setSyouhinToTeibetu(ByRef blnUpdate As Boolean, ByVal jibanRec As JibanRecordIkkatuHenkouTysSyouhin) As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim tsH As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        'Dim JibanLogic As New JibanLogic

        '商品郡の配列から、各商品行の設定を行う
        For intRowCnt As Integer = 1 To 4
            '指定行の商品コントロールを取得
            Dim ctrlRow As IkkatuSyouhin2CtrlReord = getItem2RowInfo("2_" & intRowCnt)
            Dim tsR As New TeibetuSeikyuuRecord
            Dim strChgValues() As String
            Dim dicItem2 As Dictionary(Of String, String)
            Dim blnMakeBlank As Boolean

            '更新判断フラグの設定(金額以外)
            If ctrlRow.ChgValue.Value <> ctrlRow.DbValue.Value Then
                blnUpdate = True
            End If
            '更新判断フラグの設定(金額)
            If ctrlRow.ChgKingaku.Value <> ctrlRow.DbKingaku.Value Then
                blnUpdate = True
            End If

            If ctrlRow.SyouhinLine.Style("display") = "none" Then
                Continue For
            End If

            If String.IsNullOrEmpty(ctrlRow.ChgValue.Value) Then
                blnMakeBlank = True
            End If

            strChgValues = clsLogic.getArrayFromDollarSep(ctrlRow.ChgValue.Value)
            dicItem2 = clsLogic.getDicItem2(strChgValues, blnMakeBlank)

            '区分
            tsR.Kbn = ctrlRow.Kbn.Value
            '番号
            tsR.HosyousyoNo = ctrlRow.HosyousyoNo.Value
            '分類コード
            tsR.BunruiCd = ctrlRow.BunruiCd.Value
            '画面表示NO
            tsR.GamenHyoujiNo = CInt(ctrlRow.GamenHyoujiNo.Value)
            '商品コード
            tsR.SyouhinCd = ctrlRow.Syouhin.SelectedValue
            '売上金額
            If ctrlRow.JituSeikyuuKingaku.Text = "" Then
                ctrlRow.JituSeikyuuKingaku.Text = "0"
            End If
            cl.SetDisplayString(ctrlRow.JituSeikyuuKingaku.Text, tsR.UriGaku)
            '仕入金額
            If ctrlRow.SyoudakusyoKingaku.Text = "" Then
                ctrlRow.SyoudakusyoKingaku.Text = "0"
            End If
            cl.SetDisplayString(ctrlRow.SyoudakusyoKingaku.Text, tsR.SiireGaku)
            '税区分
            tsR.ZeiKbn = dicItem2(dicKey.dkZeiKbn)
            '請求書発行日
            cl.SetDisplayString(dicItem2(dicKey.dkSeikyuusyoHakDate), tsR.SeikyuusyoHakDate)
            '売上年月日
            cl.SetDisplayString(dicItem2(dicKey.dkUriDate), tsR.UriDate)
            '請求有無
            cl.SetDisplayString(ctrlRow.SeikyuuUmu.SelectedValue, tsR.SeikyuuUmu)
            '売上計上FLG
            cl.SetDisplayString(dicItem2(dicKey.dkUriKeijyouFlg), tsR.UriKeijyouFlg)
            '売上計上日
            cl.SetDisplayString(dicItem2(dicKey.dkUriKeijouDate), tsR.UriKeijyouDate)
            '確定区分
            tsR.KakuteiKbn = Integer.MinValue
            '備考
            cl.SetDisplayString(dicItem2(dicKey.dkBikou), tsR.Bikou)
            '工務店請求金額
            If ctrlRow.KoumutenKingaku.Text = "" Then
                ctrlRow.KoumutenKingaku.Text = "0"
            End If
            cl.SetDisplayString(ctrlRow.KoumutenKingaku.Text, tsR.KoumutenSeikyuuGaku)
            '発注書金額
            cl.SetDisplayString(dicItem2(dicKey.dkHattyuusyoGaku), tsR.HattyuusyoGaku)
            '発注書確認日
            cl.SetDisplayString(dicItem2(dicKey.dkHattyuusyoKakuninDate), tsR.HattyuusyoKakuninDate)
            '一括入金FLG
            cl.SetDisplayString(dicItem2(dicKey.dkIkkatuNyuukinFlg), tsR.IkkatuNyuukinFlg)
            '調査見積書作成日
            cl.SetDisplayString(dicItem2(dicKey.dkTysMitsyoSakuseiDate), tsR.TysMitsyoSakuseiDate)
            '発注書確定FLG
            cl.SetDisplayString(dicItem2(dicKey.dkHattyuusyoKakuteiFlg), tsR.HattyuusyoKakuteiFlg)

            '************************************************************
            '*　商品追加の場合、かつ、商品１が未計上・売上済の場合、
            '*　商品1の売上年月日と請求書発行日を商品2にコピーする()
            '************************************************************
            If ctrlRow.DbValue.Value = String.Empty AndAlso jibanRec.Syouhin1Record.UriDate <> DateTime.MinValue Then
                tsR.UriDate = jibanRec.Syouhin1Record.UriDate
                If tsR.SeikyuuUmu = 1 Then
                    '商品2の請求有無が有の場合のみ、請求書発行日を設定
                    If jibanRec.Syouhin1Record.SeikyuuUmu = 1 Then
                        '商品１の請求有無が有の場合、商品1の請求書発行日を設定
                        tsR.SeikyuusyoHakDate = jibanRec.Syouhin1Record.SeikyuusyoHakDate
                    Else
                        Dim cBizLogic As New CommonBizLogic
                        Dim strSimeDate As String
                        '請求締日の取得
                        strSimeDate = cBizLogic.GetSeikyuuSimeDateFromKameiten(tsR.SeikyuuSakiCd _
                                                                                , tsR.SeikyuuSakiBrc _
                                                                                , tsR.SeikyuuSakiKbn _
                                                                                , jibanRec.KameitenCd _
                                                                                , tsR.SyouhinCd)
                        '商品１の請求有無が無の場合、算出して請求書発行日を設定
                        tsR.SeikyuusyoHakDate = cBizLogic.CalcSeikyuusyoHakkouDate(strSimeDate)
                    End If
                End If
            End If

            'データ取得時点での邸別請求テーブル.更新日時の保持値（排他チェック用）
            If dicItem2(dicKey.dkUpdDatetime) = "" Then
                If dicItem2(dicKey.dkAddDatetime) = "" Then
                    tsR.UpdDatetime = DateTime.MinValue
                Else
                    tsR.UpdDatetime = DateTime.ParseExact(dicItem2(dicKey.dkAddDatetime), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                End If
            Else
                tsR.UpdDatetime = DateTime.ParseExact(dicItem2(dicKey.dkUpdDatetime), EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If

            '邸別請求レコードをハッシュにセット
            tsH.Add(tsR.GamenHyoujiNo, tsR)

        Next

        Return tsH

    End Function

#Region "商品２コントロール行"
    ''' <summary>
    ''' 商品２レコードコントロールのインスタンスを行単位で取得する
    ''' </summary>
    ''' <param name="syouhinType">インスタンスを取得したいレコードタイプ</param>
    ''' <returns>インスタンスを格納したレコードクラス</returns>
    ''' <remarks></remarks>
    Public Function getItem2RowInfo(ByVal syouhinType As String) As IkkatuSyouhin2CtrlReord
        Dim ctrlRec As New IkkatuSyouhin2CtrlReord

        With ctrlRec
            .KameitenCd = FindControl("HiddenKameitenCode")
            .KeiretuCd = FindControl("HiddenKeiretuCd")
            .TysSeikyuuSaki = FindControl("HiddenTysSeikyuuSaki_" & syouhinType)
            .Torikeshi = FindControl("HiddenTorikeshi")
            .UriDateItem1 = FindControl("HiddenUriDateItem1")
            .SyouhinLine = FindControl("TrTysSyouhin_" & syouhinType)
            .DbValue = FindControl("HiddenDbValue_" & syouhinType)
            .ChgValue = FindControl("HiddenChgValue_" & syouhinType)
            .DbKingaku = FindControl("HiddenDbKingaku_" & syouhinType)
            .ChgKingaku = FindControl("HiddenChgKingaku_" & syouhinType)
            .Kbn = FindControl("HiddenKbn_" & syouhinType)
            .HosyousyoNo = FindControl("HiddenHosyousyoNo_" & syouhinType)
            .BunruiCd = FindControl("HiddenBunruiCd_" & syouhinType)
            .GamenHyoujiNo = FindControl("HiddenGamenHyoujiNo_" & syouhinType)
            .ZeiKbn = FindControl("HiddenZeiKbn_" & syouhinType)
            .UriDate = FindControl("HiddenUriDate_" & syouhinType)
            .HattyuusyoKakuteiFlg = FindControl("HiddenHattyuusyoKakuteiFlg_" & syouhinType)
            .HattyuusyoGaku = FindControl("HiddenHattyuusyoGaku_" & syouhinType)
            .AutoKingakuFlg = FindControl("HiddenAutoKingakuFlg_" & syouhinType)
            .ManualKingakuFlg = FindControl("HiddenManualKingakuFlg_" & syouhinType)
            .BothKingakuChgFlg = FindControl("HiddenBothKingakuChgFlg_" & syouhinType)
            .RowDisplay = FindControl("HiddenRowDisplay_" & syouhinType)
            .AddDateTime = FindControl("HiddenAddDatetimeItem_" & syouhinType)
            .UpdDateTime = FindControl("HiddenUpdDatetimeItem_" & syouhinType)
            .AutoCalc = FindControl("CheckAutoCalc_" & syouhinType)
            .AutoCalcSpan = FindControl("SPAN_Check_" & syouhinType)
            .No = FindControl("TextNo_" & syouhinType)
            .KokyakuBangou = FindControl("TextKokyakuBangou_" & syouhinType)
            .Syouhin = FindControl("SelectSyouhin_" & syouhinType)
            .SyouhinBK = FindControl("HiddenSelectSyouhin_" & syouhinType)
            .SyouhinKingakuBK = FindControl("HiddenSyouhinKingaku_" & syouhinType)
            .SyouhinSpan = FindControl("SPAN_Syouhin_" & syouhinType)
            .UriageSyori = FindControl("TextUriageSyori_" & syouhinType)
            .KoumutenKingaku = FindControl("TextKoumutenKingaku_" & syouhinType)
            .JituSeikyuuKingaku = FindControl("TextJituSeikyuuKingaku_" & syouhinType)
            .SyoudakusyoKingaku = FindControl("TextSyoudakusyoKingaku_" & syouhinType)
            .SeikyuuUmu = FindControl("SelectSeikyuuUmu_" & syouhinType)
            .SeikyuuUmuSpan = FindControl("SPAN_Seikyuu_" & syouhinType)
            .AddBtn = FindControl("ButtonAdd_" & syouhinType)
            .DelBtn = FindControl("ButtonDelete_" & syouhinType)
            .SeikyuuSakiCd = FindControl("HiddenSeikyuuSakiCd_" & syouhinType)
            .TokubetuTaiouToolTip = FindControl("ucTokubetuTaiouToolTipCtrl_" & syouhinType)
        End With

        Return ctrlRec
    End Function
#End Region

#Region "特別対応ツールチップ"
    ''' <summary>
    ''' 特別対応ツールチップに特別対応コードを設定する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strTokubetuTaiouCd">特別対応コード</param>
    ''' <param name="SyouhinType">振分先</param>
    ''' <param name="recTmp">特別対応レコードクラス</param>
    ''' <remarks></remarks>
    Public Sub SetTokubetuTaiouToolTip(ByVal sender As Object, _
                                        ByVal strTokubetuTaiouCd As String, _
                                        ByVal SyouhinType As String, _
                                        ByVal recTmp As TokubetuTaiouRecordBase)

        Dim ctrlRec As IkkatuSyouhin2CtrlReord

        ctrlRec = Me.getItem2RowInfo(SyouhinType)

        'ツールチップ設定対象かチェック
        If cbLogic.checkToolTipSetValue(sender, recTmp, ctrlRec.BunruiCd.Value, ctrlRec.GamenHyoujiNo.Value) <> EarthEnum.emToolTipType.NASI Then
            ctrlRec.TokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)
        End If

    End Sub
#End Region

End Class

