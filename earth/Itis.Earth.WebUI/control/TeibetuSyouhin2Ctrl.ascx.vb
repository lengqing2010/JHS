
Partial Public Class TeibetuSyouhin2Ctrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim cbLogic As New CommonBizLogic

#Region "プロパティ"

#Region "商品コード２の邸別請求レコードDictionary"
    ''' <summary>
    ''' 商品コード２の邸別請求レコードDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecordのDictionaryである事</remarks>
    Private htbSyouhin2Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' 商品コード２の邸別請求Dictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード２の邸別請求レコードDictionary</returns>
    ''' <remarks>画面表示NOをKeyとしたTeibetuSeikyuuRecordのリストである事</remarks>
    Public Property Syouhin2Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            ' 更新用にDictionaryを再生成する
            htbSyouhin2Records = New Dictionary(Of Integer, TeibetuSeikyuuRecord)

            ' 商品２－１
            If Not Syouhin2Record01.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin2Records.Add(1, Syouhin2Record01.TeibetuSeikyuuRec)
            End If

            ' 商品２－２
            If Not Syouhin2Record02.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin2Records.Add(2, Syouhin2Record02.TeibetuSeikyuuRec)
            End If

            ' 商品２－３
            If Not Syouhin2Record03.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin2Records.Add(3, Syouhin2Record03.TeibetuSeikyuuRec)
            End If

            ' 商品２－４
            If Not Syouhin2Record04.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin2Records.Add(4, Syouhin2Record04.TeibetuSeikyuuRec)
            End If

            Return htbSyouhin2Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin2Records = value
            ' データをコントロールに設定
            SetCtrlData(htbSyouhin2Records)
        End Set
    End Property

#End Region

#Region "邸別データ共通設定情報"
    ''' <summary>
    ''' 邸別データ共通設定情報
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingInfo() As TeibetuSettingInfoRecord
        Get
            Dim info As New TeibetuSettingInfoRecord
            info.Kubun = HiddenKubun.Value                                                  ' 区分
            info.Bangou = HiddenBangou.Value                                                ' 番号（保証書NO）
            info.UpdLoginUserId = HiddenLoginUserId.Value                                   ' ログインユーザーID
            info.KeiriGyoumuKengen = Integer.Parse(HiddenKeiriGyoumuKengen.Value)           ' 経理業務権限
            info.HattyuusyoKanriKengen = Integer.Parse(HiddenHattyuusyoKanriKengen.Value)   ' 発注書管理権限
            Return info
        End Get
        Set(ByVal value As TeibetuSettingInfoRecord)
            HiddenKubun.Value = value.Kubun                                 ' 区分
            HiddenBangou.Value = value.Bangou                               ' 番号（保証書NO）
            HiddenLoginUserId.Value = value.UpdLoginUserId                  ' ログインユーザーID
            ' 経理業務権限
            HiddenKeiriGyoumuKengen.Value = value.KeiriGyoumuKengen.ToString()
            ' 発注書管理権限
            HiddenHattyuusyoKanriKengen.Value = value.HattyuusyoKanriKengen.ToString()
        End Set
    End Property
#End Region

#Region "請求タイプ"
    ''' <summary>
    ''' 請求タイプ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SeikyuuType() As String
        Get
            Return HiddenSeikyuuType.Value
        End Get
        Set(ByVal value As String)
            HiddenSeikyuuType.Value = value
        End Set
    End Property
#End Region

#Region "系列コード"
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return HiddenKeiretuCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKeiretuCd.Value = value
        End Set
    End Property
#End Region

#Region "請求先情報UpdatePanel"
    ''' <summary>
    ''' 請求先情報UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateSeikyuusakiPanel() As UpdatePanel
        Get
            Return UpdatePanelIraiInfo
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelIraiInfo = value
        End Set
    End Property
#End Region

#Region "税込金額"
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiKingaku() As Integer
        Get
            Dim zeikomi As Integer = 0

            If Not Syouhin2Record01 Is Nothing Then
                zeikomi = Syouhin2Record01.ZeikomiKingaku
            End If
            If Not Syouhin2Record02 Is Nothing Then
                zeikomi = zeikomi + Syouhin2Record02.ZeikomiKingaku
            End If
            If Not Syouhin2Record03 Is Nothing Then
                zeikomi = zeikomi + Syouhin2Record03.ZeikomiKingaku
            End If
            If Not Syouhin2Record04 Is Nothing Then
                zeikomi = zeikomi + Syouhin2Record04.ZeikomiKingaku
            End If

            Return zeikomi
        End Get
    End Property
#End Region

#End Region

#Region "イベント"
    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（請求先・仕入先情報設定用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)
    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（請求タイプ設定）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">カスタムイベント発生元ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">請求先タイプ（直接請求/他請求）</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuTypeAction(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByVal strId As String _
                                        , ByVal strSeikyuuSakiTypeStr As String _
                                        , ByVal strKeiretuCd As String _
                                        , ByVal strKameitenCd As String)
    ''' <summary>
    ''' ページロード時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "請求先情報設定"
    ''' <summary>
    ''' 商品2-1レコードに請求先情報を設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei21(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin2Record01.GetSeikyuuInfo
        ' 請求先情報を商品２コントロールへ設定する
        SetSeikyuuInfo("1")
    End Sub

    ''' <summary>
    ''' 商品2-2レコードに請求先情報を設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei22(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin2Record02.GetSeikyuuInfo
        ' 請求先情報を商品２コントロールへ設定する
        SetSeikyuuInfo("2")
    End Sub

    ''' <summary>
    ''' 商品2-3レコードに請求先情報を設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei23(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin2Record03.GetSeikyuuInfo
        ' 請求先情報を商品２コントロールへ設定する
        SetSeikyuuInfo("3")
    End Sub

    ''' <summary>
    ''' 商品2-4レコードに請求先情報を設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei24(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin2Record04.GetSeikyuuInfo
        ' 請求先情報を商品２コントロールへ設定する
        SetSeikyuuInfo("4")
    End Sub
#End Region

    ''' <summary>
    ''' 税込金額の変更を親コントロールに通知する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeZeikomiGaku(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs, _
                                   ByVal zeikomigaku As Integer)

#Region "税込金額変更時のイベント群"
    ''' <summary>
    ''' 商品２－１税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin21(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin2Record01.ChangeZeikomiGaku

        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' 商品２－２税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin22(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin2Record02.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' 商品２－３税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin23(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin2Record03.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' 商品２－４税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin24(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin2Record04.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

#End Region

#End Region

    ''' <summary>
    ''' 売上年月日の変更を親コントロールに通知する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeUriageNengappi(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal uriagenengappi As String)



#Region "プライベートメソッド"

    ''' <summary>
    ''' 請求先情報を設定する
    ''' </summary>
    ''' <param name="rowNo"></param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuInfo(ByVal rowNo As String)
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl

        teibetuRecCtrl = FindControl("Syouhin2Record0" & rowNo)

        ' 請求先情報を商品コントロールへ設定する
        teibetuRecCtrl.SeikyuuType = SeikyuuType
        teibetuRecCtrl.KeiretuCd = KeiretuCd
        teibetuRecCtrl.UpdateSyouhinPanel.Update()

    End Sub

    ''' <summary>
    ''' 取得データをコントロールに設定します
    ''' </summary>
    ''' <param name="dicTeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlData(ByVal dicTeibetuRec As Dictionary(Of Integer, TeibetuSeikyuuRecord))

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetCtrlData", _
                                            dicTeibetuRec)

        Dim i As Integer
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl

        ' コントロールの数分処理を行う
        For i = 1 To 4
            teibetuRecCtrl = FindControl("Syouhin2Record0" & i.ToString())

            ' KEYは設定する
            teibetuRecCtrl.SettingInfo = Me.SettingInfo

            ' 画面表示NOを設定
            teibetuRecCtrl.GamenHyoujiNo = i.ToString()

            ' 商品２設定
            If dicTeibetuRec IsNot Nothing AndAlso dicTeibetuRec.ContainsKey(i) Then
                ' データを明細コントロールにセット
                teibetuRecCtrl.TeibetuSeikyuuRec = dicTeibetuRec.Item(i)
            Else
                ' データの存在しないレコードは非活性にする
                teibetuRecCtrl.Enabled = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' ユーザーコントロールで設定した請求先・仕入先情報を画面の隠し項目に反映する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSiireInfo(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String) Handles Syouhin2Record01.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin2Record02.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin2Record03.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin2Record04.SetSeikyuuSiireSakiAction
        '請求仕入用カスタムイベント
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, strId)
    End Sub

    ''' <summary>
    ''' 請求タイプの設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">カスタムイベント発生元ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">請求先タイプ(直接請求/他請求)</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuType(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String _
                                    , ByVal strSeikyuuSakiTypeStr As String _
                                    , ByVal strKeiretuCd As String _
                                    , ByVal strKameitenCd As String) Handles Syouhin2Record01.SetSeikyuuTypeAction _
                                                                            , Syouhin2Record02.SetSeikyuuTypeAction _
                                                                            , Syouhin2Record03.SetSeikyuuTypeAction _
                                                                            , Syouhin2Record04.SetSeikyuuTypeAction _

        '請求タイプの設定
        RaiseEvent SetSeikyuuTypeAction(sender _
                                        , e _
                                        , strId _
                                        , strSeikyuuSakiTypeStr _
                                        , strKeiretuCd _
                                        , strKameitenCd)
    End Sub

#End Region

#Region "パブリックメソッド"
    ''' <summary>
    ''' エラーチェック
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="denpyouNgList"></param>
    ''' <param name="denpyouErrMess"></param>
    ''' <param name="seikyuuUmuErrMess"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByVal denpyouNgList As String, _
                          ByRef denpyouErrMess As String, _
                          ByRef seikyuuUmuErrMess As String, _
                          ByRef strChgPartMess As String _
                          )

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckKinsoku", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    denpyouNgList, _
                                                    denpyouErrMess, _
                                                    seikyuuUmuErrMess, _
                                                    strChgPartMess _
                                                    )
        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "_"
        End If

        ' 商品２－１のエラーチェック
        Syouhin2Record01.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "１", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' 商品２－２のエラーチェック
        Syouhin2Record02.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "２", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' 商品２－３のエラーチェック
        Syouhin2Record03.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "３", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' 商品２－４のエラーチェック
        Syouhin2Record04.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "４", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

    End Sub

    Private Const SYOUHIN2_RECORDS_ID = "Syouhin2Record0"
    ''' <summary>
    ''' 基本請求先・仕入先情報の設定
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strCtrlId">指定ユーザーコントロールID</param>
    ''' <remarks></remarks>
    Public Sub SetDefaultSeikyuuSiireSakiInfo(ByVal strKameitenCd As String _
                                            , ByVal strTysKaisyaCd As String _
                                            , ByVal strKeiretuCd As String _
                                            , Optional ByVal strCtrlId As String = "")
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin2Record01, Syouhin2Record02, Syouhin2Record03, Syouhin2Record04}

        If strCtrlId <> String.Empty Then
            Dim intIndex As Integer

            intIndex = Integer.Parse(strCtrlId.Replace(Me.ClientID & Me.ClientIDSeparator & SYOUHIN2_RECORDS_ID, String.Empty)) - 1
            syouhinCtrl(intIndex).SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
        Else
            For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
                syouhinCtrl(intCnt).SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
            Next
        End If
    End Sub

    ''' <summary>
    ''' 請求タイプの設定
    ''' </summary>
    ''' <param name="enSeikyuuType">請求タイプ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuType(ByVal enSeikyuuType As EarthEnum.EnumSeikyuuType _
                            , ByVal strKeiretuCd As String _
                            , ByVal strKameitenCd As String _
                            , Optional ByVal strCtrlId As String = "")
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin2Record01, Syouhin2Record02, Syouhin2Record03, Syouhin2Record04}

        If strCtrlId <> String.Empty Then
            Dim intIndex As Integer
            intIndex = Integer.Parse(strCtrlId.Replace(Me.ClientID & Me.ClientIDSeparator & SYOUHIN2_RECORDS_ID, String.Empty)) - 1
            syouhinCtrl(intIndex).SetSeikyuuType(enSeikyuuType, strKeiretuCd, strKameitenCd)
        Else
            For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
                syouhinCtrl(intCnt).SetSeikyuuType(enSeikyuuType, strKeiretuCd, strKameitenCd)
            Next
        End If
    End Sub

    ''' <summary>
    '''商品毎の請求・仕入先が変更されていないかをチェックし、
    '''変更されている場合情報の再取得
    ''' </summary>
    ''' <remarks></remarks>
    Public Function setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin2Record01, Syouhin2Record02, Syouhin2Record03, Syouhin2Record04}
        For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
            If syouhinCtrl(intCnt).AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
                '商品２，３検索ボタン押下時の処理を実行
                syouhinCtrl(intCnt).SetSyouhin23(sender, e)
                syouhinCtrl(intCnt).AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
                '変更された商品が有った場合、ループ終了(原則として、1商品毎しか変更されないため)
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' 特別対応ツールチップに特別対応コードを設定する
    ''' </summary>
    ''' <param name="strTokubetuTaiouCd">特別対応コード</param>
    ''' <param name="rowNo">画面表示NO</param>
    ''' <param name="ttRec">特別対応レコードクラス</param>
    ''' <remarks></remarks>
    Public Sub SetTokubetuTaiouToolTip(ByVal strTokubetuTaiouCd As String, ByVal rowNo As String, ByVal ttRec As TokubetuTaiouRecordBase)
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim emType As EarthEnum.emToolTipType           'ツールチップ表示タイプ

        '対象の商品2の行を取得
        teibetuRecCtrl = FindControl("Syouhin2Record0" & rowNo)

        'ツールチップ設定対象かチェック
        emType = cbLogic.checkToolTipSetValue(Me, ttRec, teibetuRecCtrl.BunruiCd, teibetuRecCtrl.GamenHyoujiNo, teibetuRecCtrl.AccUriageSyori.SelectedValue)
        If emType <> EarthEnum.emToolTipType.NASI Then
            '表示用
            teibetuRecCtrl.AccTokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)

            '特別対応コード対象Hdnに格納(登録用)
            If teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty Then
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = strTokubetuTaiouCd
            Else
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value &= EarthConst.SEP_STRING & strTokubetuTaiouCd
            End If

            If emType = EarthEnum.emToolTipType.SYUSEI Then
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AcclblTokubetuTaiou.Text = EarthConst.SYUU_TOOL_TIP
            End If
        End If

    End Sub

    ''' <summary>
    ''' 特別対応データ更新フラグを初期化する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearTokubetuTaiouInfo()
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim i As Integer

        ' コントロールの数分処理を行う
        For i = 1 To EarthConst.SYOUHIN2_COUNT
            teibetuRecCtrl = FindControl("Syouhin2Record0" & i.ToString())
            teibetuRecCtrl.AccTokubetuTaiouUpdFlg.Value = EarthConst.NASI_VAL
            teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty
            teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty
        Next
    End Sub

    ''' <summary>
    ''' ツールチップから特別対応コードを取得する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCdFromToolTip() As String
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim i As Integer
        Dim strTmp As String
        Dim strTokubetuTaiouCd As String = String.Empty

        ' コントロールの数分処理を行う
        For i = 1 To EarthConst.SYOUHIN2_COUNT
            teibetuRecCtrl = FindControl("Syouhin2Record0" & i.ToString())

            '特別対応データ更新フラグ=1の場合
            If teibetuRecCtrl.AccTokubetuTaiouUpdFlg.Value = EarthConst.ARI_VAL Then
                strTmp = teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value
                If strTmp <> String.Empty Then
                    If strTokubetuTaiouCd = String.Empty Then
                        strTokubetuTaiouCd = strTmp
                    Else
                        strTokubetuTaiouCd &= EarthConst.SEP_STRING & strTmp
                    End If
                End If
            End If
        Next

        Return strTokubetuTaiouCd
    End Function

#End Region
End Class