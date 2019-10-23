Partial Public Class PopupSeikyuuSiireSakiHenkou
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Private MLogic As New MessageLogic
    Private CLogic As New CommonLogic
    Private logic As New SeikyuuSiireSakiHenkouLogic
    Private dataRec As SeikyuuSiireSakiHenkouRecord
    Private earthEnum As EarthEnum
    Private strKeijouZumi As String
    Private strViewMode As String

    Private Const CHK_TRUE As String = "1"
    Private Const CHK_False As String = "0"

    ''' <summary>
    ''' ページ初期化処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupSeikyuuSiireSakiHenkou_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        '画面項目の動きをセッティング
        setDispAction()

        '調査会社コード・事業所コードの取得
        Dim tysRec As TysKaisyaRecord
        Dim tysLogic As New TyousakaisyaSearchLogic
        Dim cBizLogic As New CommonBizLogic
        Dim dicSeikyuuSaki As Dictionary(Of String, String)

        tysRec = tysLogic.getTysKaisyaInfo(CLogic.GetDisplayString(Request("KaisyaCd")))

        'パラメータの取得
        Me.SelectSeikyuuSaki.SelectedValue = CLogic.GetDisplayString(Request("SeikyuuSakiKbn"))
        Me.TextSeikyuuSakiCd.Text = CLogic.GetDisplayString(Request("SeikyuuSakiCd"))
        Me.TextSeikyuuSakiBrc.Text = CLogic.GetDisplayString(Request("SeikyuuSakiBrc"))
        Me.TextSiireSakiCd.Text = CLogic.GetDisplayString(Request("SiireSakiCd"))
        Me.TextSiireSakiBrc.Text = CLogic.GetDisplayString(Request("SiireSakiBrc"))
        Me.HiddenKaisyaCd.Value = CLogic.GetDisplayString(tysRec.TysKaisyaCd)
        Me.HiddenJigyousyoCd.Value = CLogic.GetDisplayString(tysRec.JigyousyoCd)
        Me.HiddenKameitenCd.Value = CLogic.GetDisplayString(Request("KameitenCd"))
        Me.HiddenSyouhinCd.Value = CLogic.GetDisplayString(Request("SyouhinCd"))
        Me.HiddenKojKaisyaSeikyuu.Value = CLogic.GetDisplayString(Request("KojKaisyaSeikyuu"))
        Me.HiddenKojKaisyaCd.Value = CLogic.GetDisplayString(Request("KojKaisyaCd"))
        strKeijouZumi = CLogic.GetDisplayString(Request("KeijouFlg"))
        strViewMode = CLogic.GetDisplayString(Request("ViewMode"))

        '基本請求先情報の取得
        If Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE Then
            dicSeikyuuSaki = cBizLogic.getDefaultSeikyuuSaki(Me.HiddenKojKaisyaCd.Value)
        Else
            dicSeikyuuSaki = cBizLogic.getDefaultSeikyuuSaki(Me.HiddenKameitenCd.Value, Me.HiddenSyouhinCd.Value)
        End If

        '基本請求先情報をセット
        Me.HiddenSeikyuuSakiKbn.Value = CLogic.GetDisplayString(dicSeikyuuSaki(cBizLogic.dicKeySeikyuuSakiKbn))
        Me.HiddenSeikyuuSakiCd.Value = CLogic.GetDisplayString(dicSeikyuuSaki(cBizLogic.dicKeySeikyuuSakiCd))
        Me.HiddenSeikyuuSakiBrc.Value = CLogic.GetDisplayString(dicSeikyuuSaki(cBizLogic.dicKeySeikyuuSakiBrc))
        Me.TextDefaultSeikyuuSakiCd.Text = CLogic.GetDispSeikyuuSakiCd(Me.HiddenSeikyuuSakiKbn.Value _
                                                                        , Me.HiddenSeikyuuSakiCd.Value _
                                                                        , Me.HiddenSeikyuuSakiBrc.Value _
                                                                        , True)

        Me.TextDefaultSeikyuuSakiMei.Text = CLogic.GetDisplayString(dicSeikyuuSaki(cBizLogic.dicKeySeikyuuSakiMei))

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.TextDefaultSeikyuuSakiCd, Me.TextDefaultSeikyuuSakiMei, Me.TextTorikesiRiyuuKihon}
        '基本請求先 加盟店取消情報を表示
        CLogic.GetKameitenTorikesiRiyuuMain(Me.HiddenSeikyuuSakiKbn.Value, Me.HiddenSeikyuuSakiCd.Value, TextTorikesiRiyuuKihon, True, False, objChgColor)

        '基本仕入先のセット
        Me.TextDefaultSiireSakiCd.Text = Me.HiddenKaisyaCd.Value & " - " & Me.HiddenJigyousyoCd.Value
        Me.TextDefaultSiireSakiMei.Text = CLogic.GetDisplayString(tysRec.TysKaisyaMei)

        '請求先・仕入先デフォルト相違チェック用コントロールの初期化
        Me.HiddenChkSeikyuuSaki.Value = String.Empty
        Me.HiddenChkSiireSaki.Value = String.Empty

        '登録請求先名のセット
        Me.HiddenSeikyuuSakiCall.Value = String.Empty
        ButtonSeikyuuSaki_Click(sender, e)

        '登録仕入先名のセット
        Me.HiddenSiireSakiCall.Value = String.Empty
        ButtonSiireSaki_Click(sender, e)
        Me.HiddenSiireSakiCdNew.Value = String.Empty


        '検索ポップアップのオープン許可
        Me.HiddenSeikyuuSakiCall.Value = "1"
        Me.HiddenSiireSakiCall.Value = "1"

    End Sub

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban    '地盤画面共通クラス
        Dim jSM As New JibanSessionManager

        'マスターページ情報を取得(ScriptManager用)
        masterAjaxSM = AjaxScriptManager

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            CLogic.CloseWindow(Me)
            Exit Sub
        End If

        ''権限のチェック
        'If userinfo.IraiGyoumuKengen = -1 Or _
        '       userinfo.HoukokusyoGyoumuKengen = -1 Or _
        '       userinfo.KoujiGyoumuKengen = -1 Or _
        '       userinfo.HosyouGyoumuKengen = -1 Or _
        '       userinfo.KekkaGyoumuKengen = -1 Or _
        '       userinfo.KeiriGyoumuKengen = -1 Then
        '    '経理権限以外、４業務権限（依頼・報告・工事・保証・結果）の何れかがある場合、ボタンを有効化
        '    Me.ButtonSubmit.Disabled = False
        'Else
        '    veiwMode(sender, jSM)
        'End If

        '暫定で経理のみ使用可とする(後々、運用に慣れたら4業務権限にも開放)
        If userinfo.KeiriGyoumuKengen = -1
            Me.ButtonSubmit.Disabled = False
        Else
            veiwMode(sender, jSM)
        End If

        Dim ht As New Hashtable
        '売上計上済の場合、経理権限を持っていないユーザーは画面参照のみ
        If strKeijouZumi = "1" Then
            If userinfo.KeiriGyoumuKengen <> -1 Then
                veiwMode(sender, jSM)
            End If
        End If

        '表示モードのチェック
        If strViewMode = EarthConst.MODE_VIEW Or strViewMode = EarthConst.MODE_KAKUNIN Then
            veiwMode(sender, jSM)
        End If

    End Sub

#Region "プライベートメソッド"
    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        '****************************************************************************
        ' ボタン押下イベントの設定
        '****************************************************************************
        Me.ButtonSubmit.Attributes("onclick") = "funcReturn();"
        Me.ButtonCancel.Attributes("onclick") = "funcCancel();"

        '****************************************************************************
        ' ドロップダウンリスト設定
        '****************************************************************************
        Dim helper As New DropDownHelper
        ' 請求先区分コンボにデータをバインドする
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSaki, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

        '*****************************
        '* コードおよびポップアップボタン
        '*****************************
        Dim scriptSb As New StringBuilder()
        '請求先
        scriptSb.Append("if(checkTempValueForOnBlur(this)){")
        scriptSb.Append("   if(objEBI('" & Me.SelectSeikyuuSaki.ClientID & "').value == '' && objEBI('" & Me.TextSeikyuuSakiCd.ClientID & "').value == '' && objEBI('" & Me.TextSeikyuuSakiBrc.ClientID & "').value == ''){")
        scriptSb.Append("       objEBI('" & Me.HiddenSeikyuuSakiCdOld.ClientID & "').value = '';}")
        scriptSb.Append("   clrSeikyuuInfo();}")

        Me.SelectSeikyuuSaki.Attributes("onblur") = scriptSb.ToString
        Me.SelectSeikyuuSaki.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        Me.TextSeikyuuSakiCd.Attributes("onblur") = scriptSb.ToString
        Me.TextSeikyuuSakiCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        Me.TextSeikyuuSakiBrc.Attributes("onblur") = scriptSb.ToString
        Me.TextSeikyuuSakiBrc.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        '調査会社
        scriptSb = New StringBuilder
        scriptSb.Append("if(checkTempValueForOnBlur(this)){")
        scriptSb.Append("   if(objEBI('" & Me.TextSiireSakiCd.ClientID & "').value == '' && objEBI('" & Me.TextSiireSakiBrc.ClientID & "').value == ''){")
        scriptSb.Append("       objEBI('" & Me.HiddenSiireSakiCdOld.ClientID & "').value = '';}")
        scriptSb.Append("   objEBI('" & Me.TextSiireSakiMei.ClientID & "').value = '';}")

        Me.TextSiireSakiCd.Attributes("onblur") = scriptSb.ToString
        Me.TextSiireSakiCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        Me.TextSiireSakiBrc.Attributes("onblur") = scriptSb.ToString
        Me.TextSiireSakiBrc.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        '調査会社検索画面呼び出し用の検索タイプをセット
        Me.HiddenTysKensakuType.Value = CStr(earthEnum.EnumTyousakaisyaKensakuType.SIIRESAKI)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        CLogic.chgVeiwMode(Me.TextTorikesiRiyuuKihon, Nothing, True)
        CLogic.chgVeiwMode(Me.TextTorikesiRiyuuTouroku, Nothing, True)

    End Sub

    ''' <summary>
    ''' 呼出元から取得したパラメータをDictionaryにする
    ''' </summary>
    ''' <returns>Dictionary</returns>
    ''' <remarks></remarks>
    Private Function getItem4Params() As Dictionary(Of String, String)
        Dim dicParams As New Dictionary(Of String, String)

        dicParams.Add("SeikyuuSakiKbn", Me.SelectSeikyuuSaki.SelectedValue)
        dicParams.Add("SeikyuuSakiCd", Me.TextSeikyuuSakiCd.Text)
        dicParams.Add("SeikyuuSakiBrc", Me.TextSeikyuuSakiBrc.Text)
        dicParams.Add("SiireSakiCd", Me.TextSiireSakiCd.Text)
        dicParams.Add("SiireSakiBrc", Me.TextSiireSakiBrc.Text)
        dicParams.Add("KaisyaCd", Me.TextDefaultSiireSakiCd.Text)
        dicParams.Add("JigyousyoCd", Me.TextDefaultSiireSakiCd.Text)
        dicParams.Add("KameitenCd", Me.HiddenKameitenCd.Value)
        dicParams.Add("SyouhinCd", Me.HiddenSyouhinCd.Value)

        Return dicParams

    End Function

    ''' <summary>
    ''' 請求先情報を変更前請求先格納Hiddenにセットする
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setOldSeikyuuSaki()
        If Me.SelectSeikyuuSaki.SelectedValue = String.Empty _
                And Me.TextSeikyuuSakiCd.Text = String.Empty _
                And Me.TextSeikyuuSakiBrc.Text = String.Empty Then
            Me.HiddenSeikyuuSakiCdOld.Value = String.Empty
        Else
            Me.HiddenSeikyuuSakiCdOld.Value = Me.SelectSeikyuuSaki.SelectedValue & EarthConst.SEP_STRING _
                                            & Me.TextSeikyuuSakiCd.Text & EarthConst.SEP_STRING _
                                            & Me.TextSeikyuuSakiBrc.Text
        End If
    End Sub

    ''' <summary>
    ''' 仕入先情報を変更前仕入先格納Hiddenにセットする
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setOldSiireSaki()
        If Me.TextSiireSakiCd.Text = String.Empty _
                And Me.TextSiireSakiBrc.Text = String.Empty Then
            Me.HiddenSiireSakiCdOld.Value = String.Empty
        Else
            Me.HiddenSiireSakiCdOld.Value = Me.TextSiireSakiCd.Text & EarthConst.SEP_STRING & Me.TextSiireSakiBrc.Text
        End If
    End Sub

    ''' <summary>
    ''' 請求先・仕入先デフォルト相違チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkSeikyuuSiireSaki()
        Dim tmpScript As String = ""

        If (Me.SelectSeikyuuSaki.SelectedValue = Me.HiddenSeikyuuSakiKbn.Value _
                And Me.TextSeikyuuSakiCd.Text = Me.HiddenSeikyuuSakiCd.Value _
                And Me.TextSeikyuuSakiBrc.Text = Me.HiddenSeikyuuSakiBrc.Value) _
                Or (Me.SelectSeikyuuSaki.SelectedValue = String.Empty _
                And Me.TextSeikyuuSakiCd.Text = String.Empty _
                And Me.TextSeikyuuSakiBrc.Text = String.Empty) Then
            Me.HiddenChkSeikyuuSaki.Value = String.Empty

        Else
            Me.HiddenChkSeikyuuSaki.Value = "1"
        End If

        If (Me.TextSiireSakiCd.Text = Me.HiddenKaisyaCd.Value _
                And Me.TextSiireSakiBrc.Text = Me.HiddenJigyousyoCd.Value) _
                Or (Me.TextSiireSakiCd.Text = String.Empty _
                And Me.TextSiireSakiBrc.Text = String.Empty) Then
            Me.HiddenChkSiireSaki.Value = String.Empty
        Else
            Me.HiddenChkSiireSaki.Value = "1"
        End If
    End Sub

    ''' <summary>
    ''' 画面を参照モードにする
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub veiwMode(ByVal sender As Object, ByVal jSM As JibanSessionManager)
        Dim ht As New Hashtable
        Dim tmpScript As String

        Me.ButtonSubmit.Disabled = True
        jSM.Hash2Ctrl(Me.UpdatePanelSeikyuuSaki, EarthConst.MODE_VIEW, ht)
        jSM.Hash2Ctrl(Me.UpdatePanelSiireSaki, EarthConst.MODE_VIEW, ht)
        Me.ButtonSeikyuuSaki.Style("display") = "none"
        Me.ButtonSiireSaki.Style("display") = "none"

        tmpScript = "viewMode = '1'"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "funcViewMode", tmpScript, True)
    End Sub
#End Region

#Region "ボタンイベント"
    ''' <summary>
    ''' 請求先検索ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuuSaki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim hdnOldObj() As HiddenField = {Me.HiddenSeikyuuSakiCdOld}
        Dim blnResult As Boolean

        '請求先検索画面呼出
        blnResult = CLogic.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                        , Me.SelectSeikyuuSaki, Me.TextSeikyuuSakiCd _
                                                        , Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei _
                                                        , Me.ButtonSeikyuuSaki _
                                                        , hdnOldObj, Me.HiddenSeikyuuSakiCall)

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuSaki, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuuTouroku}
        '取消理由取得設定と色替処理
        CLogic.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuSaki.SelectedValue, Me.TextSeikyuuSakiCd.Text, TextTorikesiRiyuuTouroku, True, False, objChgColor)

        If Me.HiddenSeikyuuSakiCall.Value <> String.Empty Then
            If blnResult Then
                'フォーカスセット
                setFocusAJ(Me.ButtonSeikyuuSaki)
            End If
        End If

        '請求先・仕入先デフォルト相違チェック
        chkSeikyuuSiireSaki()

    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSiireSaki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSiireSakiCd As String      '仕入先コード
        Dim strSiireSakiBrc As String     '仕入先枝番
        Dim list As New List(Of TyousakaisyaSearchRecord)
        Dim tysKaiSrchLogic As New TyousakaisyaSearchLogic
        Dim tmpScript As String = String.Empty
        Dim intTysCnt As Integer

        strSiireSakiCd = IIf(Me.TextSiireSakiCd.Text <> String.Empty, Me.TextSiireSakiCd.Text, String.Empty)
        strSiireSakiBrc = IIf(Me.TextSiireSakiBrc.Text <> String.Empty, Me.TextSiireSakiBrc.Text, String.Empty)

        '画面から仕入先情報を取得
        If Me.HiddenSiireSakiCdNew.Value = String.Empty Then
            Me.HiddenSiireSakiCdNew.Value = strSiireSakiCd & strSiireSakiBrc
        End If

        intTysCnt = tysKaiSrchLogic.GetTyousakaisyaSearchResultCnt(Me.HiddenSiireSakiCdNew.Value, _
                                                                        "", _
                                                                        "", _
                                                                        "", _
                                                                        True, _
                                                                        Me.HiddenKameitenCd.Value, _
                                                                        CInt(Me.HiddenTysKensakuType.Value))
        If intTysCnt = 1 Then
            list = tysKaiSrchLogic.GetTyousakaisyaSearchResult(Me.HiddenSiireSakiCdNew.Value, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            True, _
                                                                            Me.HiddenKameitenCd.Value, _
                                                                            CInt(Me.HiddenTysKensakuType.Value))
        End If

        If list.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = list(0)
            Me.TextSiireSakiCd.Text = recData.TysKaisyaCd
            Me.TextSiireSakiBrc.Text = recData.JigyousyoCd
            Me.TextSiireSakiMei.Text = recData.TysKaisyaMei

            Me.HiddenSiireSakiCdNew.Value = String.Empty
            If Me.HiddenSiireSakiCall.Value <> String.Empty Then
                'フォーカスセット
                setFocusAJ(Me.ButtonSiireSaki)
            End If

            setOldSiireSaki()
        Else
            '調査会社名をクリア
            Me.TextSiireSakiMei.Text = String.Empty

            If Me.HiddenSiireSakiCall.Value <> String.Empty Then
                '検索画面表示用JavaScript『callSearch』を実行
                Dim tmpFocusScript = "objEBI('" & Me.ButtonSiireSaki.ClientID & "').focus();"
                Dim tmpHdnCdClearScript = "objEBI('" & Me.HiddenSiireSakiCdNew.ClientID & "').value = ''"
                tmpScript = "callSearch('" & Me.HiddenSiireSakiCdNew.ClientID & EarthConst.SEP_STRING & _
                                             Me.HiddenKameitenCd.ClientID & EarthConst.SEP_STRING & _
                                             Me.HiddenTysKensakuType.ClientID & "','" _
                                           & UrlConst.SEARCH_TYOUSAKAISYA & "','" _
                                           & Me.HiddenSiireSakiCdNew.ClientID & EarthConst.SEP_STRING & _
                                             Me.TextSiireSakiMei.ClientID & EarthConst.SEP_STRING & _
                                             Me.HiddenKakushuNG.ClientID & "','" _
                                           & Me.ButtonSiireSaki.ClientID & "');"
                tmpScript = tmpFocusScript & tmpScript & tmpHdnCdClearScript

                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            End If

        End If

        '請求先・仕入先デフォルト相違チェック
        chkSeikyuuSiireSaki()

    End Sub
#End Region

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        'フォーカスセット
        masterAjaxSM.SetFocus(ctrl)
    End Sub

End Class