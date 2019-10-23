Public Partial Class SeikyuuSiireSyouhinRecordCtrl
    Inherits System.Web.UI.UserControl
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
#Region "プロパティ"
    ''' <summary>
    ''' 行色スタイル
    ''' </summary>
    ''' <remarks></remarks>
    Private strRowColorStyle As String
    ''' <summary>
    ''' 行色スタイル
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property RowColorStyle() As String
        Set(ByVal value As String)
            strRowColorStyle = value
        End Set
    End Property

    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AccBunruiCd() As String
        Set(ByVal value As String)
            Me.TextBunruiCd.Text = value
        End Set
        Get
            Return Me.TextBunruiCd.Text
        End Get
    End Property
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AccGamenHyoujiNo() As String
        Set(ByVal value As String)
            Me.TextHyoujiNo.Text = value
        End Set
        Get
            Return Me.TextHyoujiNo.Text
        End Get
    End Property
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiCd() As String
        Set(ByVal value As String)
            Me.TextSeikyuuSakiCd.Text = value
        End Set
        Get
            Return Me.TextSeikyuuSakiCd.Text
        End Get
    End Property
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiBrc() As String
        Set(ByVal value As String)
            Me.TextSeikyuuSakiBrc.Text = value
        End Set
        Get
            Return Me.TextSeikyuuSakiBrc.Text
        End Get
    End Property
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiKbn() As String
        Set(ByVal value As String)
            Me.SelectSeikyuuSaki.SelectedValue = value
        End Set
        Get
            Return Me.SelectSeikyuuSaki.SelectedValue
        End Get
    End Property
    ''' <summary>
    ''' 仕入先コード
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AccSiireSakiCd() As String
        Set(ByVal value As String)
            Me.TextSiireSakiCd.Text = value
        End Set
        Get
            Return Me.TextSiireSakiCd.Text
        End Get
    End Property
    ''' <summary>
    ''' 仕入先枝番
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AccSiireSakiBrc() As String
        Set(ByVal value As String)
            Me.TextSiireSakiBrc.Text = value
        End Set
        Get
            Return Me.TextSiireSakiBrc.Text
        End Get
    End Property
    ''' <summary>
    ''' 更新日時Hidden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnUpdDateTime() As HiddenField
        Get
            Return Me.HiddenUpdDateTime
        End Get
        Set(ByVal value As HiddenField)
            Me.HiddenUpdDateTime = value
        End Set
    End Property

#Region "WriteOnlyコントロール"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>

    Public WriteOnly Property AccSyouhinCd() As String
        Set(ByVal value As String)
            Me.TextSyouhinCd.Text = value
        End Set
    End Property
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccSyouhinMei() As String
        Set(ByVal value As String)
            Me.TextSyouhinMei.Text = value
        End Set
    End Property
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccKameitenCd() As String
        Set(ByVal value As String)
            Me.HiddenKameitenCd.Value = value
        End Set
    End Property
    ''' <summary>
    ''' 工事会社請求フラグ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccKojKaisyaSeikyuuFlg() As String
        Set(ByVal value As String)
            Me.HiddenKojKaisyaSeikyuuFlg.Value = value
        End Set
    End Property
    ''' <summary>
    ''' 工事会社コード
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccKojKaisyaCd() As String
        Set(ByVal value As String)
            Me.HiddenKojKaisyaCd.Value = value
        End Set
    End Property
    ''' <summary>
    ''' 基本仕入先コード
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccDefaultSiireSakiCd() As String
        Set(ByVal value As String)
            Me.HiddenDefaultSiireSakiCd.Value = value
        End Set
    End Property
    ''' <summary>
    ''' 基本仕入先枝番
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccDefaultSiireSakiBrc() As String
        Set(ByVal value As String)
            Me.HiddenDefaultSiireSakiBrc.Value = value
        End Set
    End Property
#End Region

#End Region

#Region "フィールド"
    Private masterAjaxSM As New ScriptManager
    Private cLogic As New CommonLogic
    Private jbn As New Jiban
    Private userInfo As New LoginUserInfo
#End Region

#Region "イベント"
#Region "ぺージ系"
    ''' <summary>
    ''' 画面初期処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jbn.UserAuth(userInfo)
        '認証結果によって画面表示を切替える
        If userInfo Is Nothing Then
            Response.Redirect(UrlConst.MAIN)
        End If

        '****************************************************************************
        ' ドロップダウンリスト設定
        '****************************************************************************
        Dim helper As New DropDownHelper
        ' 請求先区分コンボにデータをバインドする
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSaki, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

        '*****************************
        '*　JavaScriptの設定
        '*****************************
        Me.ButtonSetDefSeikyuu.Attributes("onclick") = "setDefaultSeikyuuSaki('" & Me.TextSeikyuuSakiCd.ClientID & "'" & _
                                                                             ", '" & Me.TextSeikyuuSakiBrc.ClientID & "'" & _
                                                                             ", '" & Me.SelectSeikyuuSaki.ClientID & "'" & _
                                                                             ", '" & Me.TextSeikyuuSakiMei.ClientID & "'" & _
                                                                             ", '" & Me.HiddenDefaultSeikyuuSakiCd.ClientID & "'" & _
                                                                             ", '" & Me.HiddenDefaultSeikyuuSakiBrc.ClientID & "'" & _
                                                                             ", '" & Me.HiddenDefaultSeikyuuSakiKbn.ClientID & "'" & _
                                                                             ", '" & Me.HiddenDefaultSeikyuuSakiMei.ClientID & "'" & _
                                                                             ", '" & Me.HiddenSeikyuuSakiCdOld.ClientID & "'" & _
                                                                            ");"
        Me.ButtonSetDefSiire.Attributes("onclick") = "setDefaultSiireSaki('" & Me.TextSiireSakiCd.ClientID & "'" & _
                                                                         ", '" & Me.TextSiireSakiBrc.ClientID & "'" & _
                                                                         ", '" & Me.TextSiireSakiMei.ClientID & "'" & _
                                                                         ", '" & Me.HiddenDefaultSiireSakiCd.ClientID & "'" & _
                                                                         ", '" & Me.HiddenDefaultSiireSakiBrc.ClientID & "'" & _
                                                                         ", '" & Me.HiddenDefaultSiireSakiMei.ClientID & "'" & _
                                                                         ", '" & Me.HiddenSiireSakiCdOld.ClientID & "'" & _
                                                                        ");"
        '*****************************
        '* コードおよびポップアップボタン
        '*****************************
        Dim scriptSb As New StringBuilder()
        '請求先
        scriptSb.Append("if(checkTempValueForOnBlur(this)){")
        scriptSb.Append("   if(objEBI('" & Me.SelectSeikyuuSaki.ClientID & "').value == '' && objEBI('" & Me.TextSeikyuuSakiCd.ClientID & "').value == '' && objEBI('" & Me.TextSeikyuuSakiBrc.ClientID & "').value == ''){")
        scriptSb.Append("       objEBI('" & Me.HiddenSeikyuuSakiCdOld.ClientID & "').value = '';}")
        scriptSb.Append("   objEBI('" & Me.TextSeikyuuSakiMei.ClientID & "').value = '';}")

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


        '読取り専用のセット
        Me.TextSyouhinCd.Attributes("readonly") = True
        Me.TextBunruiCd.Attributes("readonly") = True
        Me.TextHyoujiNo.Attributes("readonly") = True
        Me.TextSyouhinMei.Attributes("readonly") = True
        Me.TextSeikyuuSakiMei.Attributes("readonly") = True
        Me.TextSiireSakiMei.Attributes("readonly") = True

    End Sub

    ''' <summary>
    ''' ページ描画前処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        '請求先・仕入先デフォルト相違チェック()
        chkSeikyuuSiireSaki()
        Me.UpdatePanelSeikyuuSaki.Update()
        Me.UpdatePanelSiireSaki.Update()
    End Sub
#End Region

#Region "ボタン系"
    ''' <summary>
    ''' 請求先検索
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuuSaki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuuSaki.ServerClick
        Dim hdnOldObj() As HiddenField = {Me.HiddenSeikyuuSakiCdOld}
        Dim blnResult As Boolean

        '請求先検索画面呼出
        blnResult = cLogic.CallSeikyuuSakiSearchWindow(sender, e, Me.Parent.Page _
                                                        , Me.SelectSeikyuuSaki, Me.TextSeikyuuSakiCd _
                                                        , Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei _
                                                        , Me.ButtonSeikyuuSaki _
                                                        , hdnOldObj)
        'フォーカスセット
        setFocusAJ(Me.ButtonSeikyuuSaki)

    End Sub

    ''' <summary>
    ''' 仕入先検索
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSiireSaki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSiireSaki.ServerClick
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
                                                                        Me.HiddenKameitenCd.Value)
        If intTysCnt = 1 Then
            list = tysKaiSrchLogic.GetTyousakaisyaSearchResult(Me.HiddenSiireSakiCdNew.Value, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            True, _
                                                                            Me.HiddenKameitenCd.Value)
        End If

        If list.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = list(0)
            Me.TextSiireSakiCd.Text = recData.TysKaisyaCd
            Me.TextSiireSakiBrc.Text = recData.JigyousyoCd
            Me.TextSiireSakiMei.Text = recData.TysKaisyaMei

            Me.HiddenSiireSakiCdNew.Value = String.Empty
            'フォーカスセット
            setFocusAJ(Me.ButtonSiireSaki)

            setOldSiireSaki()
        Else
            '調査会社名をクリア
            Me.TextSiireSakiMei.Text = String.Empty

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
    End Sub
#End Region
#End Region

#Region "パブリックメソッド"
#Region "エラーチェック"
    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <param name="strErrMsg">エラーメッセージ</param>
    ''' <param name="listErrCtrl">エラーコントロール群</param>
    ''' <param name="strRowIdentify">行識別文字列</param>
    ''' <remarks></remarks>
    Public Sub checkInput(ByRef strErrMsg As String, ByRef listErrCtrl As List(Of Control), ByVal strRowIdentify As String)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".checkInput" _
                                                    , strErrMsg _
                                                    , listErrCtrl _
                                                    , strRowIdentify)
        Dim strNowSeikyuuSakiCd As String
        Dim strOldSeikyuuSakiCd As String
        Dim strNowSiireSakiCd As String
        Dim strOldSiireSakiCd As String

        '請求先の検索状況を判定
        strNowSeikyuuSakiCd = Me.SelectSeikyuuSaki.SelectedValue & EarthConst.SEP_STRING _
                                & Me.TextSeikyuuSakiCd.Text & EarthConst.SEP_STRING _
                                & Me.TextSeikyuuSakiBrc.Text
        strOldSeikyuuSakiCd = Me.HiddenSeikyuuSakiCdOld.Value

        'コード・枝番・区分が全てブランクのときはエラーにしない
        If Me.SelectSeikyuuSaki.SelectedValue = String.Empty _
        And Me.TextSeikyuuSakiCd.Text = String.Empty _
        And Me.TextSeikyuuSakiBrc.Text = String.Empty Then
            strNowSeikyuuSakiCd = String.Empty
            strOldSeikyuuSakiCd = String.Empty
        End If

        If strNowSeikyuuSakiCd <> strOldSeikyuuSakiCd Then
            '請求先の検索状況NG
            strErrMsg &= strRowIdentify & EarthConst.BRANK_STRING & Messages.MSG030E.Replace("@PARAM1", "請求先")
            listErrCtrl.Add(Me.ButtonSeikyuuSaki)
        End If

        '仕入先の検索状況を判定
        strNowSiireSakiCd = Me.TextSiireSakiCd.Text & EarthConst.SEP_STRING _
                                & Me.TextSiireSakiBrc.Text
        strOldSiireSakiCd = Me.HiddenSiireSakiCdOld.Value

        'コード・枝番がブランクのときはエラーにしない
        If Me.TextSiireSakiCd.Text = String.Empty _
        And Me.TextSiireSakiBrc.Text = String.Empty Then
            strNowSiireSakiCd = String.Empty
            strOldSiireSakiCd = String.Empty
        End If

        If strNowSiireSakiCd <> strOldSiireSakiCd Then
            '仕入先の検索状況NG
            strErrMsg &= strRowIdentify & EarthConst.BRANK_STRING & Messages.MSG030E.Replace("@PARAM1", "仕入先")
            listErrCtrl.Add(Me.ButtonSiireSaki)
        End If

    End Sub
#End Region

#Region "DBからデータの取得＆セット"
    ''' <summary>
    ''' 画面にデータをセットする
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setDispData()
        Dim cBizLogic As New CommonBizLogic
        Dim dicSeikyuuSaki As Dictionary(Of String, String)
        Dim tysLogic As New TyousakaisyaSearchLogic
        Dim uriLogic As New UriageDataSearchLogic
        Dim listSeikyuuInfo As List(Of SeikyuuSakiInfoRecord)

        '行色の設定
        Me.trSeikyuu.Attributes.Add("class", strRowColorStyle)
        Me.trSiire.Attributes.Add("class", strRowColorStyle)

        '権限チェック
        If userInfo.KeiriGyoumuKengen <> -1 Then
            setViewMode()
        End If

        '基本請求先の取得
        If Me.HiddenKojKaisyaSeikyuuFlg.Value = "1" Then
            dicSeikyuuSaki = cBizLogic.getDefaultSeikyuuSaki(Me.HiddenKojKaisyaCd.Value)
        Else
            dicSeikyuuSaki = cBizLogic.getDefaultSeikyuuSaki(Me.HiddenKameitenCd.Value, Me.TextSyouhinCd.Text)
        End If

        '基本請求先の設定
        Me.HiddenDefaultSeikyuuSakiCd.Value = dicSeikyuuSaki(cBizLogic.dicKeySeikyuuSakiCd)
        Me.HiddenDefaultSeikyuuSakiBrc.Value = dicSeikyuuSaki(cBizLogic.dicKeySeikyuuSakiBrc)
        Me.HiddenDefaultSeikyuuSakiKbn.Value = dicSeikyuuSaki(cBizLogic.dicKeySeikyuuSakiKbn)
        Me.HiddenDefaultSeikyuuSakiMei.Value = dicSeikyuuSaki(cBizLogic.dicKeySeikyuuSakiMei)

        '基本仕入先名の取得
        Me.HiddenDefaultSiireSakiMei.Value = tysLogic.GetTyousaKaisyaMei(Me.HiddenDefaultSiireSakiCd.Value _
                                                                        , Me.HiddenDefaultSiireSakiBrc.Value _
                                                                        , False)

        '邸別に登録されている請求先の名称を取得＆設定
        listSeikyuuInfo = uriLogic.GetSeikyuuSakiInfo(Me.TextSeikyuuSakiCd.Text, Me.TextSeikyuuSakiBrc.Text, Me.SelectSeikyuuSaki.SelectedValue)
        If listSeikyuuInfo IsNot Nothing AndAlso listSeikyuuInfo.Count = 1 Then
            Me.TextSeikyuuSakiMei.Text = listSeikyuuInfo(0).SeikyuuSakiMei
        End If

        '検索確認用の前回請求先コードをセット
        Me.HiddenSeikyuuSakiCdOld.Value = Me.SelectSeikyuuSaki.SelectedValue & EarthConst.SEP_STRING _
                                        & Me.TextSeikyuuSakiCd.Text & EarthConst.SEP_STRING _
                                        & Me.TextSeikyuuSakiBrc.Text

        '邸別に登録されている仕入先の名称を取得＆設定
        Me.TextSiireSakiMei.Text = tysLogic.GetTyousaKaisyaMei(Me.TextSiireSakiCd.Text _
                                                            , Me.TextSiireSakiBrc.Text _
                                                            , False)

        '検索確認用の前回仕入先コードをセット
        Me.HiddenSiireSakiCdOld.Value = Me.TextSiireSakiCd.Text & EarthConst.SEP_STRING _
                                        & Me.TextSiireSakiBrc.Text
    End Sub
#End Region
#End Region

#Region "プライベートメソッド"
#Region "画面表示設定"
    ''' <summary>
    ''' 画面を参照モードに変更する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setViewMode()
        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable

        Me.ButtonSeikyuuSaki.Disabled = True
        Me.ButtonSiireSaki.Disabled = True
        Me.ButtonSetDefSeikyuu.Disabled = True
        Me.ButtonSetDefSiire.Disabled = True

        jSM.Hash2Ctrl(Me.mainTable, EarthConst.MODE_VIEW, ht)

    End Sub

    ''' <summary>
    ''' 請求先・仕入先デフォルト相違チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkSeikyuuSiireSaki()
        Dim tmpScript As String = ""

        If (Me.SelectSeikyuuSaki.SelectedValue = Me.HiddenDefaultSeikyuuSakiKbn.Value _
                And Me.TextSeikyuuSakiCd.Text = Me.HiddenDefaultSeikyuuSakiCd.Value _
                And Me.TextSeikyuuSakiBrc.Text = Me.HiddenDefaultSeikyuuSakiBrc.Value) _
                Or (Me.SelectSeikyuuSaki.SelectedValue = String.Empty _
                And Me.TextSeikyuuSakiCd.Text = String.Empty _
                And Me.TextSeikyuuSakiBrc.Text = String.Empty) Then

            cLogic.setStyleRedBold(Me.SelectSeikyuuSaki.Style, False)
            cLogic.setStyleRedBold(Me.SpanSeikyuuSakiKbn.Style, False)
            cLogic.setStyleRedBold(Me.TextSeikyuuSakiCd.Style, False)
            cLogic.setStyleRedBold(Me.TextSeikyuuSakiBrc.Style, False)
            cLogic.setStyleRedBold(Me.TextSeikyuuSakiMei.Style, False)
        Else
            cLogic.setStyleRedBold(Me.SelectSeikyuuSaki.Style, True)
            cLogic.setStyleRedBold(Me.SpanSeikyuuSakiKbn.Style, True)
            cLogic.setStyleRedBold(Me.TextSeikyuuSakiCd.Style, True)
            cLogic.setStyleRedBold(Me.TextSeikyuuSakiBrc.Style, True)
            cLogic.setStyleRedBold(Me.TextSeikyuuSakiMei.Style, True)

        End If

        If (Me.TextSiireSakiCd.Text = Me.HiddenDefaultSiireSakiCd.Value _
                And Me.TextSiireSakiBrc.Text = Me.HiddenDefaultSiireSakiBrc.Value) _
                Or (Me.TextSiireSakiCd.Text = String.Empty _
                And Me.TextSiireSakiBrc.Text = String.Empty) Then

            cLogic.setStyleRedBold(Me.TextSiireSakiCd.Style, False)
            cLogic.setStyleRedBold(Me.TextSiireSakiBrc.Style, False)
            cLogic.setStyleRedBold(Me.TextSiireSakiMei.Style, False)
        Else
            cLogic.setStyleRedBold(Me.TextSiireSakiCd.Style, True)
            cLogic.setStyleRedBold(Me.TextSiireSakiBrc.Style, True)
            cLogic.setStyleRedBold(Me.TextSiireSakiMei.Style, True)
        End If

    End Sub
#End Region

#Region "ユーティリティ"
    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setFocusAJ" _
                                                    , ctrl)
        'フォーカスセット
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' 仕入先情報を変更前仕入先格納Hiddenにセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub setOldSiireSaki()
        If Me.TextSiireSakiCd.Text = String.Empty _
                And Me.TextSiireSakiBrc.Text = String.Empty Then
            Me.HiddenSiireSakiCdOld.Value = String.Empty
        Else
            Me.HiddenSiireSakiCdOld.Value = Me.TextSiireSakiCd.Text & EarthConst.SEP_STRING & Me.TextSiireSakiBrc.Text
        End If
    End Sub

#End Region
#End Region

End Class