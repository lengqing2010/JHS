
Partial Public Class TeibetuNyuukinSyuusei
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    ''' <summary>
    ''' 共通処理クラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim ComLog As New CommonLogic

    Dim user_info As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim sysdate As DateTime
    'マスターページのMasterContentPlaceHolder利用のため
    Dim MasterCPH1 As System.Web.UI.WebControls.ContentPlaceHolder
    Dim cbLogic As New CommonBizLogic

#End Region

#Region "メンバ定数"
    Private Const PATH_USR_CTRL_TOP As String = "control/TeibetuNyuukinRecordCtrlTop.ascx"
    Private Const PATH_USR_CTRL_ROW As String = "control/TeibetuNyuukinRecordCtrl.ascx"
    Private Const SW_ON As String = "1"
    Private Const SW_OFF As String = "0"
    
#End Region

#Region "プロパティ"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _kbn As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property Kbn() As String
        Get
            Return _kbn
        End Get
        Set(ByVal value As String)
            _kbn = value
        End Set
    End Property

    ''' <summary>
    ''' 番号(保証書No)
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _no As String
    ''' <summary>
    ''' 番号(保証書No)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property No() As String
        Get
            Return _no
        End Get
        Set(ByVal value As String)
            _no = value
        End Set
    End Property

#End Region

#Region "イベント"
    ''' <summary>
    ''' ページロード時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        'マスターページのMasterContentPlaceHolderを取得
        Dim myMaster As EarthMasterPage = Page.Master
        MasterCPH1 = myMaster.MasterContentPlaceHolder

        ' ユーザー基本認証
        jBn.UserAuth(user_info)

        '認証結果によって画面表示を切替える
        If user_info IsNot Nothing Then
            ' 経理業務権限を設定
            HiddenKeiriGyoumuKengen.Value = user_info.KeiriGyoumuKengen.ToString()
        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        '毎回明細テーブルをクリア
        Me.tblMeisai.Controls.Clear()

        If IsPostBack = False Then

            ' パラメータエラー有無
            Dim isError As Boolean = False

            ' Key情報を保持
            _kbn = Request("kbn")
            _no = Request("no")

            TextKubun.Text = _kbn
            TextBangou.Text = _no

            ' パラメータ不足時は画面を表示しない
            If _kbn Is Nothing Or _kbn = String.Empty Or _
               _no Is Nothing Or _no = String.Empty Then
                ' エラー有り
                isError = True
            Else
                Dim logic As New JibanLogic
                '地盤データが存在しない場合
                If logic.ExistsJibanData(_kbn, _no) = False Then
                    ' 存在しない
                    isError = True
                End If
            End If

            ' パラメータ不正時はメニュー画面へ遷移する
            If isError Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            setDispAction()

            ' 地盤データを画面に設定する
            SetJibanData()
        Else
            '画面項目設定処理(ポストバック用)
            setDisplayPostBack()

        End If
    End Sub

    ''' <summary>
    ''' 解約払戻返金チェック変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CheckboxKaiyakuHaraimodosikin_CheckedChanged(ByVal sender As System.Object, _
                                                               ByVal e As System.EventArgs) _
                                                               Handles CheckboxKaiyakuHaraimodosikin.CheckedChanged
        ' 解約払戻設定
        If CheckboxKaiyakuHaraimodosikin.Checked = True Then
            TextHenkinSyoriDate.Enabled = True
            TextHenkinSyoriDate.Text = ComLog.GetDisplayString(DateTime.Now)
        Else
            TextHenkinSyoriDate.Enabled = False
            TextHenkinSyoriDate.Text = String.Empty
        End If
        Me.HiddenKaiyakuSyori.Value = SW_ON
    End Sub

    ''' <summary>
    ''' 登録／修正実行ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTourokuExe_ServerClick(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles ButtonTourokuExe.ServerClick
        ' 画面の内容をDBに反映する
        SaveData(sender)
    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim recCtrlTop As TeibetuNyuukinRecordCtrlTop

        If Me.HiddenKaiyakuSyori.Value <> SW_ON Then
            '解約払戻の入金額および返金額が変更された時の処理
            recCtrlTop = Me.tblMeisai.Controls(Me.tblMeisai.Controls.Count - 1)
            If Me.TrKaiyakuMeisai.Visible = True And recCtrlTop.AccHiddenZangaku.Value = "0" Then
                recCtrlTop.AccHiddenZangaku.Value = String.Empty
                CheckboxKaiyakuHaraimodosikin.Checked = True
                Me.UpdatePanelKaiyakuCheck.Update()
                Me.TextHenkinSyoriDate.Enabled = True
                If Me.TextHenkinSyoriDate.Text = String.Empty Then
                    Me.TextHenkinSyoriDate.Text = DateTime.Now.ToString("yyyy/MM/dd")
                    Me.UpdatePanelKaiyaku.Update()
                End If
            End If
        End If
        Me.HiddenKaiyakuSyori.Value = String.Empty
    End Sub

#End Region

#Region "プライベートメソッド"
#Region "ページロード"
#Region "初期処理"
    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '登録/修正実行ボタン押下時のイベントハンドラ
        Dim tmpScript = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);objEBI('" & ButtonTourokuExe.ClientID & "').click();}else{return false;}"
        ButtonTourokuSyuuseiJikkou1.Attributes("onclick") = tmpScript
        ButtonTourokuSyuuseiJikkou2.Attributes("onclick") = tmpScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '特別対応
        ComLog.getTokubetuTaiouLinkPath(Me.ButtonTokubetuTaiou, _
                                     user_info, _
                                     Me.TextKubun.ClientID, _
                                     Me.TextBangou.ClientID, _
                                     "", _
                                     "", _
                                     "")

        '物件履歴表示ボタン
        ButtonBukkenRireki.Attributes("onclick") = "callSearch('" & TextKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.POPUP_BUKKEN_RIREKI & "','','');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ComLog.chgVeiwMode(Me.TextTorikesiRiyuu)

    End Sub

#End Region

#Region "画面描画"
    ''' <summary>
    ''' 地盤データを画面に設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJibanData()

        Dim jLogic As New JibanLogic
        Dim nLogic As New TeibetuNyuukinLogic
        Dim record As JibanRecord
        Dim listNyuukinRec As List(Of TeibetuNyuukinRecord)

        ' 再読み込み用
        If _kbn = String.Empty Or _kbn Is Nothing Then
            _kbn = TextKubun.Text
        End If
        If _no = String.Empty Or _no Is Nothing Then
            _no = TextBangou.Text
        End If

        ' 地盤データを取得する
        record = jLogic.GetJibanData(_kbn, _no)

        ' 地盤データなしは画面を表示しない
        If record Is Nothing Then
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '邸別入金データを取得する
        listNyuukinRec = nLogic.GetTeibetuSeikyuuNyuukinData(_kbn, _no)

        ' 地盤データなしは画面を表示しない
        If listNyuukinRec Is Nothing OrElse listNyuukinRec.Count = 0 Then
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '地盤テーブル.更新者からログインユーザID、更新日時を取得
        CommonLogic.Instance.SetKousinsya(record.Kousinsya, TextSaisyuuKousinnsya.Text, TextSaisyuuKousinNitiji.Text)

        ' コントロールへデータを設定
        TextKubun.Text = record.Kbn
        TextBangou.Text = record.HosyousyoNo
        TextSetunusiMei.Text = record.SesyuMei
        TextBukkenJyuusyo1.Text = record.BukkenJyuusyo1
        TextBukkenJyuusyo2.Text = record.BukkenJyuusyo2
        TextBukkenJyuusyo3.Text = record.BukkenJyuusyo3

        HiddenUpdDatetime.Value = record.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)

        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim kameitenData As New KameitenSearchRecord

        ' 加盟店情報を取得
        kameitenData = kameitenSearchLogic.GetKameitenSearchResult(record.Kbn, _
                                                                   record.KameitenCd, _
                                                                   String.Empty, _
                                                                   False)

        If Not kameitenData Is Nothing Then
            TextKameitenCd.Text = record.KameitenCd
            TextKameitenMei.Text = kameitenData.KameitenMei1
            TextKeiretuCd.Text = kameitenData.KeiretuCd
            TextKeiretuMei.Text = kameitenData.KeiretuMei
            TextEigyousyoCd.Text = kameitenData.EigyousyoCd
            TextEigyousyoMei.Text = kameitenData.EigyousyoMei
        End If

        '加盟店取消理由設定
        setTorikesiRiyuu(TextKubun.Text, TextKameitenCd.Text)

        ' 日付チェック
        Dim checkDate As String = "checkDate(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 他システムへのリンクボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '保証書DB
        ComLog.getHosyousyoDbFilePath(TextKubun.Text, TextBangou.Text, ButtonHosyousyoDB)

        '加盟店注意事項
        ComLog.getKameitenTyuuijouhouPath(TextKameitenCd.ClientID, ButtonKameitenTyuuijouhou)

        'onblur設定
        TextHenkinSyoriDate.Attributes("onblur") = checkDate

        '画面に邸別明細(請求／入金)データを設定
        SetTeibetuData(record, listNyuukinRec)

        ' 解約払戻設定
        TextHenkinSyoriDate.Text = IIf(record.HenkinSyoriDate = DateTime.MinValue, String.Empty, record.HenkinSyoriDate.ToString("yyyy/MM/dd"))
        If record.HenkinSyoriFlg = 1 Then
            CheckboxKaiyakuHaraimodosikin.Checked = True
            TextHenkinSyoriDate.Enabled = True
        Else
            CheckboxKaiyakuHaraimodosikin.Checked = False
            TextHenkinSyoriDate.Enabled = False
        End If

        ' 解約レコード無しの場合、解約項目を非表示にする
        If Me.HiddenKaiyakuNashiFlg.Value = "1" Then
            TrKaiyakuHeader.Visible = False
            TrKaiyakuMeisai.Visible = False
        End If

    End Sub

#End Region

#Region "DBから画面にデータをセット"
    ''' <summary>
    ''' 地盤レコードより各種邸別レコードを取得します
    ''' </summary>
    ''' <param name="rec">地盤レコード</param>
    ''' <param name="listNyuukinRec">邸別入金レコードクラスのリスト</param>
    ''' <remarks></remarks>
    Private Sub SetTeibetuData(ByVal rec As JibanRecord, ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord))
        Dim logic As New TeibetuNyuukinLogic
        Dim dicBunruiCnt As Dictionary(Of String, Integer)
        Dim intBunruiCd As Integer
        Dim intBunruiCnt As Integer

        dicBunruiCnt = GetBunruiCnt(listNyuukinRec)

        '商品１
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Syouhin1
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '商品２
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Syouhin2_110
        intBunruiCnt = dicBunruiCnt(intBunruiCd)
        Me.HiddenSyouhin2Cnt.Value = SetTableRecords(rec, listNyuukinRec, intBunruiCd, intBunruiCnt, rec.Syouhin2Records)

        '商品３
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Syouhin3
        intBunruiCnt = dicBunruiCnt(intBunruiCd)
        Me.HiddenSyouhin3Cnt.Value = SetTableRecords(rec, listNyuukinRec, intBunruiCd, intBunruiCnt, rec.Syouhin3Records)

        '商品４
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Syouhin4
        intBunruiCnt = dicBunruiCnt(intBunruiCd)
        Me.HiddenSyouhin4Cnt.Value = SetTableRecords(rec, listNyuukinRec, intBunruiCd, intBunruiCnt, rec.Syouhin4Records)

        '改良工事
        intBunruiCd = EarthEnum.EnumSyouhinKubun.KairyouKouji
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '追加工事
        intBunruiCd = EarthEnum.EnumSyouhinKubun.TuikaKouji
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '調査報告書
        intBunruiCd = EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '工事報告書
        intBunruiCd = EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '保証書
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Hosyousyo
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '解約払戻
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Kaiyaku
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

    End Sub

    ''' <summary>
    ''' 分類ごとの件数を取得
    ''' </summary>
    ''' <param name="listNyuukinRec"></param>
    ''' <returns>分類ごとの件数を格納したDictionary</returns>
    ''' <remarks></remarks>
    Private Function GetBunruiCnt(ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord)) As Dictionary(Of String, Integer)
        Dim intRecCnt As Integer = 0
        Dim dicBunruiCnt As New Dictionary(Of String, Integer)

        dicBunruiCnt.Add(EarthConst.SOUKO_CD_SYOUHIN_2_110, 0)
        dicBunruiCnt.Add(EarthConst.SOUKO_CD_SYOUHIN_3, 0)
        dicBunruiCnt.Add(EarthConst.SOUKO_CD_SYOUHIN_4, 0)

        For Each rec As TeibetuNyuukinRecord In listNyuukinRec
            If rec.BunruiCd IsNot Nothing AndAlso rec.BunruiCd <> String.Empty Then
                Select Case rec.BunruiCd.Substring(0, 2)
                    Case EarthConst.SOUKO_CD_SYOUHIN_2_110.Substring(0, 2)
                        dicBunruiCnt(EarthConst.SOUKO_CD_SYOUHIN_2_110) += 1
                    Case EarthConst.SOUKO_CD_SYOUHIN_3.Substring(0, 2)
                        dicBunruiCnt(EarthConst.SOUKO_CD_SYOUHIN_3) += 1
                    Case EarthConst.SOUKO_CD_SYOUHIN_4.Substring(0, 2)
                        dicBunruiCnt(EarthConst.SOUKO_CD_SYOUHIN_4) += 1
                End Select
            End If
        Next

        Return dicBunruiCnt

    End Function

    ''' <summary>
    ''' 画面のテーブルに請求情報と入金情報をセットする
    ''' </summary>
    ''' <param name="rec">地盤レコード</param>
    ''' <param name="listNyuukinRec">邸別入金レコードクラスのリスト</param>
    ''' <param name="enSyouhinType">商品区分種類</param>
    ''' <remarks></remarks>
    Private Sub SetTableRecord(ByVal rec As JibanRecord _
                                , ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord) _
                                , ByVal enSyouhinType As EarthEnum.EnumSyouhinKubun)
        Dim strSyouhinId As String
        Dim strBunruiName As String
        Dim strBunruiCd As String
        Dim recSyouhin As New TeibetuSeikyuuRecord
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        Dim recNyuukin As TeibetuNyuukinRecord
        Dim intCntNyuukinRec As Integer = 0

        Select Case enSyouhinType
            Case EarthEnum.EnumSyouhinKubun.Syouhin1
                '商品1
                strSyouhinId = EarthConst.USR_CTRL_ID_ITEM1
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_ITEM1
                strBunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1
                recSyouhin = rec.Syouhin1Record
            Case EarthEnum.EnumSyouhinKubun.KairyouKouji
                '改良工事
                strSyouhinId = EarthConst.USR_CTRL_ID_K_KOUJI
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_K_KOUJI
                strBunruiCd = EarthConst.SOUKO_CD_KAIRYOU_KOUJI
                recSyouhin = rec.KairyouKoujiRecord
            Case EarthEnum.EnumSyouhinKubun.TuikaKouji
                '追加工事
                strSyouhinId = EarthConst.USR_CTRL_ID_T_KOUJI
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_T_KOUJI
                strBunruiCd = EarthConst.SOUKO_CD_TUIKA_KOUJI
                recSyouhin = rec.TuikaKoujiRecord
            Case EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo
                '調査報告書
                strSyouhinId = EarthConst.USR_CTRL_ID_T_HOUKOKU
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_T_HOUKOKU
                strBunruiCd = EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO
                recSyouhin = rec.TyousaHoukokusyoRecord
            Case EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo
                '工事報告書
                strSyouhinId = EarthConst.USR_CTRL_ID_K_HOUKOKU
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_K_HOUKOKU
                strBunruiCd = EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO
                recSyouhin = rec.KoujiHoukokusyoRecord
            Case EarthEnum.EnumSyouhinKubun.Hosyousyo
                '保証書
                strSyouhinId = EarthConst.USR_CTRL_ID_HOSYOUSYO
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_HOSYOUSYO
                strBunruiCd = EarthConst.SOUKO_CD_HOSYOUSYO
                recSyouhin = rec.HosyousyoRecord
            Case EarthEnum.EnumSyouhinKubun.Kaiyaku
                '解約払戻
                strSyouhinId = EarthConst.USR_CTRL_ID_KAIYAKU
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_KAIYAKU
                strBunruiCd = EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI
                recSyouhin = rec.KaiyakuHaraimodosiRecord
            Case Else
                ctrlRecTop = New TeibetuNyuukinRecordCtrlTop
                'タイトルを設定
                ctrlRecTop.AccLblTitle.Text = String.Empty
                'テーブルに明細行を一行追加
                Me.tblMeisai.Controls.Add(ctrlRecTop)
                Exit Sub
        End Select

        intCntNyuukinRec = GetBunruiStartIndex(listNyuukinRec, strBunruiCd)

        'ユーザコントロールの読込
        ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
        If intCntNyuukinRec < listNyuukinRec.Count Then
            '商品1
            With ctrlRecTop
                'ID付与
                .ID = strSyouhinId
                'タイトルを設定
                .AccLblTitle.Text = strBunruiName

                If listNyuukinRec(intCntNyuukinRec) IsNot Nothing Then
                    recNyuukin = listNyuukinRec(intCntNyuukinRec)
                    intCntNyuukinRec += 1
                Else
                    recNyuukin = Nothing
                End If

                SetSyouhinRec(ctrlRecTop, recSyouhin, recNyuukin)

                'テーブルに明細行を一行追加
                Me.tblMeisai.Controls.Add(ctrlRecTop)
            End With
        Else
            If enSyouhinType = EarthEnum.EnumSyouhinKubun.Kaiyaku Then
                '解約払戻はレコードがない場合フラグセット
                Me.HiddenKaiyakuNashiFlg.Value = SW_ON
            End If
            'ユーザコントロールの読込
            ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
            'ID付与
            ctrlRecTop.ID = strSyouhinId
            'タイトルを設定
            ctrlRecTop.AccLblTitle.Text = strBunruiName
            'DBにレコードがない場合は非表示
            If ctrlRecTop.AccTextSyouhinCd.Text = String.Empty Then
                ctrlRecTop.AccTextNyuukinKinGaku.Text = String.Empty
                ctrlRecTop.AccTextNyuukinKinGaku.Style("display") = "none"
                ctrlRecTop.AccTextHenkinGaku.Text = String.Empty
                ctrlRecTop.AccTextHenkinGaku.Style("display") = "none"
            End If
            'テーブルに明細行を一行追加
            Me.tblMeisai.Controls.Add(ctrlRecTop)
        End If
    End Sub

    ''' <summary>
    ''' 画面のテーブルに請求情報と入金情報をセットする
    ''' </summary>
    ''' <param name="rec">地盤レコード</param>
    ''' <param name="listNyuukinRec">邸別入金レコードクラスのリスト</param>
    ''' <param name="enSyouhinType">商品区分種類</param>
    ''' <param name="intBunruiCnt">分類ごとの件数</param>
    ''' <param name="dicSyouhinRecs">邸別請求レコードDictionary</param>
    ''' <returns>ループ件数</returns>
    ''' <remarks></remarks>
    Private Function SetTableRecords(ByVal rec As JibanRecord _
                                    , ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord) _
                                    , ByVal enSyouhinType As EarthEnum.EnumSyouhinKubun _
                                    , ByVal intBunruiCnt As Integer _
                                    , ByVal dicSyouhinRecs As Dictionary(Of Integer, TeibetuSeikyuuRecord)) As Integer
        Dim strSyouhinId As String
        Dim strBunruiName As String
        Dim strBunruiCd As String
        Dim intCntNyuukinRec As Integer = 0
        Dim intLoopCnt As Integer = 0
        Dim recSyouhin As TeibetuSeikyuuRecord
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        Dim ctrlRec As TeibetuNyuukinRecordCtrl
        Dim recNyuukin As TeibetuNyuukinRecord

        Select Case enSyouhinType
            Case EarthEnum.EnumSyouhinKubun.Syouhin2_110, EarthEnum.EnumSyouhinKubun.Syouhin2_115
                '商品2
                strSyouhinId = EarthConst.USR_CTRL_ID_ITEM2
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_ITEM2
                strBunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_110
            Case EarthEnum.EnumSyouhinKubun.Syouhin3
                '商品3
                strSyouhinId = EarthConst.USR_CTRL_ID_ITEM3
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_ITEM3
                strBunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3
            Case EarthEnum.EnumSyouhinKubun.Syouhin4
                '商品4
                strSyouhinId = EarthConst.USR_CTRL_ID_ITEM4
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_ITEM4
                strBunruiCd = EarthConst.SOUKO_CD_SYOUHIN_4
            Case Else
                ctrlRecTop = New TeibetuNyuukinRecordCtrlTop
                'タイトルを設定
                ctrlRecTop.AccLblTitle.Text = String.Empty
                'テーブルに明細行を一行追加
                Me.tblMeisai.Controls.Add(ctrlRecTop)
                Exit Function
        End Select

        If listNyuukinRec IsNot Nothing AndAlso listNyuukinRec.Count > 0 Then

            intCntNyuukinRec = GetBunruiStartIndex(listNyuukinRec, strBunruiCd)

            While intCntNyuukinRec < listNyuukinRec.Count AndAlso listNyuukinRec(intCntNyuukinRec).BunruiCd.Substring(0, 2) = strBunruiCd.Substring(0, 2)
                'ループカウンタのカウントアップ
                intLoopCnt += 1

                '邸別入金レコードの取得
                recNyuukin = listNyuukinRec(intCntNyuukinRec)

                '邸別請求レコードの取得
                If dicSyouhinRecs.ContainsKey(recNyuukin.GamenHyoujiNo) Then
                    recSyouhin = dicSyouhinRecs(recNyuukin.GamenHyoujiNo)
                Else
                    recSyouhin = Nothing
                End If

                If intLoopCnt = 1 Then
                    'ユーザコントロールの読込
                    ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
                    'ID付与
                    ctrlRecTop.ID = strSyouhinId & intLoopCnt

                    'タイトルを設定
                    ctrlRecTop.AccLblTitle.Text = strBunruiName
                    ctrlRecTop.AccTdTitle.Attributes("rowspan") = intBunruiCnt

                    '商品行ユーザーコントロールにDBの値をセット
                    SetSyouhinRec(ctrlRecTop, recSyouhin, recNyuukin)

                    'テーブルに明細行を一行追加
                    Me.tblMeisai.Controls.Add(ctrlRecTop)
                Else
                    'ユーザコントロールの読込
                    ctrlRec = Me.LoadControl(PATH_USR_CTRL_ROW)
                    'ID付与
                    ctrlRec.ID = strSyouhinId & intLoopCnt

                    '商品行ユーザーコントロールにDBの値をセット
                    SetSyouhinRec(ctrlRec, recSyouhin, recNyuukin)

                    ''DBにレコードがない場合は非表示
                    If ctrlRec.AccTextSyouhinCd.Text = String.Empty _
                    And ctrlRec.AccTextNyuukinKinGaku.Text = String.Empty _
                    And ctrlRec.AccTextHenkinGaku.Text = String.Empty Then
                        ctrlRec.AccTextNyuukinKinGaku.Text = String.Empty
                        ctrlRec.AccTextNyuukinKinGaku.Style("display") = "none"
                        ctrlRec.AccTextHenkinGaku.Text = String.Empty
                        ctrlRec.AccTextHenkinGaku.Style("display") = "none"
                    End If

                    '行の色設定
                    If intLoopCnt Mod 2 = 0 Then
                        ctrlRec.AccTrRecord.Attributes.Add("class", "even")
                    End If

                    'テーブルに明細行を一行追加
                    Me.tblMeisai.Controls.Add(ctrlRec)
                End If

                intCntNyuukinRec += 1

            End While
        End If

        If listNyuukinRec Is Nothing OrElse listNyuukinRec.Count = 0 OrElse intLoopCnt = 0 Then
            'ユーザコントロールの読込
            ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
            'ID付与
            ctrlRecTop.ID = strSyouhinId & 1
            'タイトルを設定
            ctrlRecTop.AccLblTitle.Text = strBunruiName
            'DBにレコードがない場合は非表示
            If ctrlRecTop.AccTextSyouhinCd.Text = String.Empty Then
                ctrlRecTop.AccTextNyuukinKinGaku.Text = String.Empty
                ctrlRecTop.AccTextNyuukinKinGaku.Style("display") = "none"
                ctrlRecTop.AccTextHenkinGaku.Text = String.Empty
                ctrlRecTop.AccTextHenkinGaku.Style("display") = "none"
            End If
            'テーブルに明細行を一行追加
            Me.tblMeisai.Controls.Add(ctrlRecTop)
        End If

        Return intLoopCnt
    End Function

    ''' <summary>
    ''' 邸別入金レコードクラスのリスト内で分類ごとの開始Indexを取得します
    ''' </summary>
    ''' <param name="listNyuukinRec">邸別入金レコードクラスのリスト</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <returns>分類コードごとの開始Index</returns>
    ''' <remarks></remarks>
    Private Function GetBunruiStartIndex(ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord), ByVal strBunruiCd As String)
        Dim intIndex As Integer
        For intIndex = 0 To listNyuukinRec.Count - 1
            If listNyuukinRec(intIndex).BunruiCd IsNot Nothing AndAlso listNyuukinRec(intIndex).BunruiCd <> String.Empty Then
                If listNyuukinRec(intIndex).BunruiCd.Substring(0, 2) = strBunruiCd.Substring(0, 2) Then
                    Exit For
                End If
            End If
        Next

        Return intIndex

    End Function

    ''' <summary>
    ''' 取消理由の設定
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Private Sub setTorikesiRiyuu(ByVal strKbn As String, ByVal strKameitenCd As String)

        '色替え処理対象のコントロールを配列に格納(※取消理由テキストボックス以外)
        Dim objArray() As Object = New Object() {Me.TextKameitenCd, Me.TextKameitenMei}

        '取消理由と加盟店情報の文字色設定
        ComLog.GetKameitenTorikesiRiyuu(strKbn _
                                        , strKameitenCd _
                                        , Me.TextTorikesiRiyuu _
                                        , True _
                                        , False _
                                        , objArray)

    End Sub

#End Region

#Region "商品行ユーザーコントロールの値セット"
    ''' <summary>
    ''' 商品行ユーザーコントロールの値セット
    ''' </summary>
    ''' <param name="ctrlRecTop">商品行ユーザーコントロール</param>
    ''' <param name="recSeikyuu">邸別請求レコード</param>
    ''' <param name="recNyuukin">邸別入金レコード</param>
    ''' <remarks></remarks>
    Private Sub SetSyouhinRec(ByVal ctrlRecTop As TeibetuNyuukinRecordCtrlTop _
                            , ByVal recSeikyuu As TeibetuSeikyuuRecord _
                            , ByVal recNyuukin As TeibetuNyuukinRecord)
        With ctrlRecTop
            '請求情報
            SetSeikyuuRecord(.AccTextSyouhinCd, .AccTextSyouhinMei, .AccTextSeikyuuKingGaku, recSeikyuu)
            '入金情報
            SetNyuukinRecord(.AccTextNyuukinKinGaku, .AccTextHenkinGaku, .AccHiddenBunruiCd, .AccHiddenGamenHyoujiNo, .AccHiddenUpdDatetime, recNyuukin)
        End With
    End Sub

    ''' <summary>
    ''' 商品行ユーザーコントロールの値セット
    ''' </summary>
    ''' <param name="ctrlRec">商品行ユーザーコントロール</param>
    ''' <param name="recSeikyuu">邸別請求レコード</param>
    ''' <param name="recNyuukin">邸別入金レコード</param>
    ''' <remarks></remarks>
    Private Sub SetSyouhinRec(ByVal ctrlRec As TeibetuNyuukinRecordCtrl _
                            , ByVal recSeikyuu As TeibetuSeikyuuRecord _
                            , ByVal recNyuukin As TeibetuNyuukinRecord)
        With ctrlRec
            '請求情報
            SetSeikyuuRecord(.AccTextSyouhinCd, .AccTextSyouhinMei, .AccTextSeikyuuKingGaku, recSeikyuu)
            '入金情報
            SetNyuukinRecord(.AccTextNyuukinKinGaku, .AccTextHenkinGaku, .AccHiddenBunruiCd, .AccHiddenGamenHyoujiNo, .AccHiddenUpdDatetime, recNyuukin)
        End With
    End Sub

    ''' <summary>
    ''' 邸別請求データをコントロールにセットします
    ''' </summary>
    ''' <param name="ctrlSyouhinCd">商品コードテキストボックス</param>
    ''' <param name="ctrlSyouhinMei">商品名テキストボックス</param>
    ''' <param name="ctrlSeikyuuKingaku">請求金額テキストボックス</param>
    ''' <param name="rec">邸別請求レコード</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuRecord(ByVal ctrlSyouhinCd As TextBox _
                                , ByVal ctrlSyouhinMei As TextBox _
                                , ByVal ctrlSeikyuuKingaku As TextBox _
                                , ByVal rec As TeibetuSeikyuuRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSeikyuuRecord" _
                                                    , ctrlSyouhinCd _
                                                    , ctrlSyouhinMei _
                                                    , ctrlSeikyuuKingaku _
                                                    , rec)
        If rec IsNot Nothing Then
            ctrlSyouhinCd.Text = rec.SyouhinCd
            ctrlSyouhinMei.Text = rec.SyouhinMei
            If ctrlSeikyuuKingaku IsNot Nothing Then
                ctrlSeikyuuKingaku.Text = rec.ZeikomiUriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 邸別入金データをコントロールにセットします
    ''' </summary>
    ''' <param name="ctrlNyuukinGaku">入金金額テキストボックス</param>
    ''' <param name="ctrlHenkinGaku">返金額テキストボックス</param>
    ''' <param name="rec">邸別入金レコード</param>
    ''' <remarks></remarks>
    Private Sub SetNyuukinRecord(ByVal ctrlNyuukinGaku As TextBox _
                                , ByVal ctrlHenkinGaku As TextBox _
                                , ByVal ctrlBunruiCd As HiddenField _
                                , ByVal ctrlGamenHyoujiNo As HiddenField _
                                , ByVal ctrlUpdDatetime As HiddenField _
                                , ByVal rec As TeibetuNyuukinRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetNyuukinRecord" _
                                                    , ctrlNyuukinGaku _
                                                    , ctrlHenkinGaku _
                                                    , ctrlBunruiCd _
                                                    , ctrlGamenHyoujiNo _
                                                    , rec)

        If rec IsNot Nothing Then
            ' 税込入金額
            ctrlNyuukinGaku.Text = _
                IIf(rec.ZeikomiNyuukinGaku = Integer.MinValue, _
                    String.Empty, _
                    rec.ZeikomiNyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
            ' 税込返金額
            ctrlHenkinGaku.Text = _
                IIf(rec.ZeikomiHenkinGaku = Integer.MinValue, _
                    String.Empty, _
                    rec.ZeikomiHenkinGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
            ' 分類コード
            ctrlBunruiCd.Value = rec.BunruiCd
            ' 画面表示NO
            ctrlGamenHyoujiNo.Value = rec.GamenHyoujiNo
            ' 更新日時
            ctrlUpdDatetime.Value = rec.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)

        End If

    End Sub

#End Region

#Region "画面再描画"
    ''' <summary>
    ''' 画面項目設定処理(ポストバック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDisplayPostBack()
        Dim intCntSyouhin2 As Integer
        Dim intCntSyouhin3 As Integer
        Dim intCntSyouhin4 As Integer

        If Me.HiddenSyouhin2Cnt.Value <> String.Empty Then
            intCntSyouhin2 = Integer.Parse(Me.HiddenSyouhin2Cnt.Value)
        Else
            intCntSyouhin2 = 0
        End If
        If Me.HiddenSyouhin3Cnt.Value <> String.Empty Then
            intCntSyouhin3 = Integer.Parse(Me.HiddenSyouhin3Cnt.Value)
        Else
            intCntSyouhin3 = 0
        End If
        If Me.HiddenSyouhin4Cnt.Value <> String.Empty Then
            intCntSyouhin4 = Integer.Parse(Me.HiddenSyouhin4Cnt.Value)
        Else
            intCntSyouhin4 = 0
        End If

        '*************
        '* 商品１
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_ITEM1)
        '*************
        '* 商品２
        '*************
        RemakeSyouinRecords(EarthConst.USR_CTRL_ID_ITEM2, intCntSyouhin2)
        '*************
        '* 商品３
        '*************
        RemakeSyouinRecords(EarthConst.USR_CTRL_ID_ITEM3, intCntSyouhin3)
        '*************
        '* 商品４
        '*************
        RemakeSyouinRecords(EarthConst.USR_CTRL_ID_ITEM4, intCntSyouhin4)
        '*************
        '* 改良工事
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_K_KOUJI)
        '*************
        '* 追加工事
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_T_KOUJI)
        '*************
        '* 調査報告書
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_T_HOUKOKU)
        '*************
        '* 工事報告書
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_K_HOUKOKU)
        '*************
        '* 保証書
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_HOSYOUSYO)
        '*************
        '* 解約払戻
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_KAIYAKU)
        ' 解約レコード無しの場合、解約項目を非表示にする
        If Me.HiddenKaiyakuNashiFlg.Value = "1" Then
            TrKaiyakuHeader.Visible = False
            TrKaiyakuMeisai.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' 商品行ユーザーコントロールの再作成
    ''' </summary>
    ''' <param name="strRecId">商品行ユーザーコントロールID</param>
    ''' <remarks></remarks>
    Private Sub RemakeSyouhinRecord(ByVal strRecId As String)
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        'ユーザコントロールの読込とIDの付与
        ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
        ctrlRecTop.ID = strRecId
        'テーブルに明細行を一行追加
        Me.tblMeisai.Controls.Add(ctrlRecTop)
    End Sub

    ''' <summary>
    ''' 商品行ユーザーコントロールの再作成
    ''' </summary>
    ''' <param name="strRecId">商品行ユーザーコントロールID</param>
    ''' <param name="intCntSyouhin">商品行件数</param>
    ''' <remarks></remarks>
    Private Sub RemakeSyouinRecords(ByVal strRecId As String, ByVal intCntSyouhin As Integer)
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        Dim ctrlRec As TeibetuNyuukinRecordCtrl
        Dim intLoopCnt As Integer

        'ユーザコントロールの読込とIDの付与
        ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
        ctrlRecTop.ID = strRecId & "1"
        'テーブルに明細行を一行追加
        Me.tblMeisai.Controls.Add(ctrlRecTop)

        If intCntSyouhin > 1 Then
            For intLoopCnt = 1 To intCntSyouhin - 1
                'ユーザコントロールの読込とIDの付与
                ctrlRec = Me.LoadControl(PATH_USR_CTRL_ROW)
                ctrlRec.ID = strRecId & intLoopCnt + 1
                '
                If intLoopCnt Mod 2 = 0 Then
                    ctrlRec.AccTrRecord.Attributes.Add("class", "even")
                End If
                'テーブルに明細行を一行追加
                Me.tblMeisai.Controls.Add(ctrlRec)
            Next
        End If
    End Sub

#End Region

#End Region

#Region "DB更新"

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveData(ByVal sender As System.Object)

        '完了メッセージ兼、次物件指定ポップアップ表示のためのフラグをクリア
        callModalFlg.Value = String.Empty

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New List(Of Control)

        ' エラーチェック
        CheckInput(errMess, arrFocusTargetCtrl)

        If errMess <> String.Empty Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If


        ' 邸別データ修正用のロジッククラス
        Dim logic As New TeibetuNyuukinLogic
        ' 画面内容より地盤レコードを生成する
        Dim jibanRec As New JibanRecordTeibetuNyuukin


        '***************************************
        ' 地盤データ
        '***************************************
        ' 区分
        jibanRec.Kbn = TextKubun.Text
        ' 番号（保証書NO）
        jibanRec.HosyousyoNo = TextBangou.Text
        ' 更新者ユーザーID
        jibanRec.UpdLoginUserId = user_info.LoginUserId

        ' 読込時の更新者日時
        If HiddenUpdDatetime.Value = String.Empty Then
            jibanRec.UpdDatetime = DateTime.MinValue
        Else
            jibanRec.UpdDatetime = DateTime.Parse(HiddenUpdDatetime.Value)
        End If

        ' 返金処理フラグ
        jibanRec.HenkinSyoriFlg = IIf(CheckboxKaiyakuHaraimodosikin.Checked, 1, 0)
        ' 返金処理日
        If TextHenkinSyoriDate.Text = String.Empty Then
            jibanRec.HenkinSyoriDate = DateTime.MinValue
        Else
            jibanRec.HenkinSyoriDate = DateTime.Parse(TextHenkinSyoriDate.Text)
        End If

        '***************************************
        ' 邸別入金データ
        '***************************************

        ' データ設定用のDictionaryです
        Dim nyuukinRecords As New Dictionary(Of String, TeibetuNyuukinRecord)
        Dim listNyuukinRecs As New List(Of TeibetuNyuukinUpdateRecord)
        ' データ設定用の邸別入金レコードです
        Dim record As TeibetuNyuukinRecord
        Dim updRec As TeibetuNyuukinUpdateRecord
        '画面のユーザーコントロール
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        Dim ctrlRec As TeibetuNyuukinRecordCtrl
        Dim strBunruiCd As String = String.Empty
        Dim strGamenHyoujiNo As String = String.Empty
        Dim blnSetRec As Boolean = False

        sysdate = Now

        For intCnt As Integer = 0 To Me.tblMeisai.Controls.Count - 1
            Select Case Me.tblMeisai.Controls(intCnt).GetType.ToString
                Case "ASP.control_teibetunyuukinrecordctrltop_ascx"
                    ctrlRecTop = Me.tblMeisai.Controls(intCnt)
                    If ctrlRecTop.AccTextNyuukinKinGaku.Text <> String.Empty Or ctrlRecTop.AccTextHenkinGaku.Text <> String.Empty Then
                        '画面の情報を邸別入金レコードにセット
                        record = SetUpdateNyuukinData(ctrlRecTop.AccTextNyuukinKinGaku.Text _
                                                    , ctrlRecTop.AccTextHenkinGaku.Text _
                                                    , ctrlRecTop.AccHiddenBunruiCd.Value _
                                                    , ctrlRecTop.AccHiddenGamenHyoujiNo.Value _
                                                    , ctrlRecTop.AccHiddenUpdDatetime.Value)
                    Else
                        record = Nothing
                    End If
                    '分類コードの取得
                    strBunruiCd = ctrlRecTop.AccHiddenBunruiCd.Value
                    '画面表示NOの取得
                    strGamenHyoujiNo = ctrlRecTop.AccHiddenGamenHyoujiNo.Value
                    blnSetRec = True
                Case "ASP.control_teibetunyuukinrecordctrl_ascx"
                    ctrlRec = Me.tblMeisai.Controls(intCnt)
                    If ctrlRec.AccTextNyuukinKinGaku.Text <> String.Empty Or ctrlRec.AccTextHenkinGaku.Text <> String.Empty Then
                        '画面の情報を邸別入金レコードにセット
                        record = SetUpdateNyuukinData(ctrlRec.AccTextNyuukinKinGaku.Text _
                                , ctrlRec.AccTextHenkinGaku.Text _
                                , ctrlRec.AccHiddenBunruiCd.Value _
                                , ctrlRec.AccHiddenGamenHyoujiNo.Value _
                                , ctrlRec.AccHiddenUpdDatetime.Value)
                    Else
                        record = Nothing
                    End If
                    '分類コードの取得
                    strBunruiCd = ctrlRec.AccHiddenBunruiCd.Value
                    '画面表示NOの取得
                    strGamenHyoujiNo = ctrlRec.AccHiddenGamenHyoujiNo.Value
                    blnSetRec = True
                Case Else
                    record = Nothing
                    blnSetRec = False
            End Select

            If blnSetRec Then
                updRec = New TeibetuNyuukinUpdateRecord
                updRec.TeibetuNyuukinrecord = record
                updRec.BunruiCd = strBunruiCd
                If IsNumeric(strGamenHyoujiNo) Then
                    updRec.GamenHyoujiNo = Integer.Parse(strGamenHyoujiNo)
                Else
                    updRec.GamenHyoujiNo = 0
                End If

                'Listに設定する
                listNyuukinRecs.Add(updRec)
            End If
        Next

        '邸別入金レコードを更新用地盤データに設定
        jibanRec.TeibetuNyuukinRecords = nyuukinRecords
        jibanRec.TeibetuNyuukinLists = listNyuukinRecs

        '更新者
        jibanRec.Kousinsya = cbLogic.GetKousinsya(user_info.LoginUserId, DateTime.Now)

        ' DBへ反映
        If logic.SaveJibanData(sender, jibanRec) Then
            Me.tblMeisai.Controls.Clear()

            SetJibanData()

            '完了メッセージ兼、次物件指定ポップアップ表示のためにフラグをセット
            callModalFlg.Value = Boolean.TrueString
        End If

    End Sub

    ''' <summary>
    ''' エラーチェック
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <remarks></remarks>
    Private Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control))

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckKinsoku", _
                                                    errMess, _
                                                    arrFocusTargetCtrl)

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '入力値チェック
        If TextHenkinSyoriDate.Text <> String.Empty Then
            If DateTime.Parse(TextHenkinSyoriDate.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHenkinSyoriDate.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "返金処理日")
                arrFocusTargetCtrl.Add(TextHenkinSyoriDate)
            End If
        End If

    End Sub

    ''' <summary>
    ''' DB設定用の邸別入金データを編集します
    ''' </summary>
    ''' <param name="nyuukinGaku">画面の入金額</param>
    ''' <param name="henkinGaku">画面の返金額</param>
    ''' <param name="bunruiCd">分類コード</param>
    ''' <param name="gamenHyoujiNo">画面表示NO</param>
    ''' <param name="updateDateTime">読込時の更新日時</param>
    ''' <returns>邸別入金レコード</returns>
    ''' <remarks></remarks>
    Private Function SetUpdateNyuukinData(ByVal nyuukinGaku As String _
                                        , ByVal henkinGaku As String _
                                        , ByVal bunruiCd As String _
                                        , ByVal gamenHyoujiNo As String _
                                        , ByVal updateDateTime As String) As TeibetuNyuukinRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetUpdateNyuukinData", _
                                                    nyuukinGaku, _
                                                    henkinGaku, _
                                                    bunruiCd, _
                                                    gamenHyoujiNo, _
                                                    updateDateTime)

        Dim record As New TeibetuNyuukinRecord
        '画面表示NOの数値チェック
        If IsNumeric(gamenHyoujiNo) Then
            Dim intGamenHyoujiNo As Integer = Integer.Parse(gamenHyoujiNo)
        Else
            Return Nothing
            Exit Function
        End If

        nyuukinGaku = nyuukinGaku.Replace(",", String.Empty)
        henkinGaku = henkinGaku.Replace(",", String.Empty)

        ' キー情報
        record.Kbn = TextKubun.Text
        record.HosyousyoNo = TextBangou.Text
        record.BunruiCd = bunruiCd
        record.GamenHyoujiNo = gamenHyoujiNo

        If (nyuukinGaku.Trim() = String.Empty Or nyuukinGaku.Trim() = "0") And _
           (henkinGaku.Trim() = String.Empty Or henkinGaku.Trim() = "0") Then

            ' 入金・返金額が共に空白または０の場合、物理削除する
            Return Nothing
        End If

        ' 入金額
        If nyuukinGaku.Trim() = String.Empty Then
            record.ZeikomiNyuukinGaku = 0
        Else
            record.ZeikomiNyuukinGaku = Integer.Parse(nyuukinGaku)
        End If
        ' 返金額
        If henkinGaku.Trim() = String.Empty Then
            record.ZeikomiHenkinGaku = 0
        Else
            record.ZeikomiHenkinGaku = Integer.Parse(henkinGaku)
        End If
        ' 最終入金日
        record.SaisyuuNyuukinDate = sysdate.Date

        ' 更新者ユーザーID
        record.UpdLoginUserId = user_info.LoginUserId
        ' 読込時の更新者日時
        If updateDateTime = String.Empty Then
            record.UpdDatetime = DateTime.MinValue
        Else
            record.UpdDatetime = DateTime.Parse(updateDateTime)
        End If

        Return record

    End Function

#End Region

#End Region

End Class