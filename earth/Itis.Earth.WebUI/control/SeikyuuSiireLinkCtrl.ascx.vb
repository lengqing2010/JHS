Public Class SeikyuuSiireLinkCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private cl As New CommonLogic
    Private cbl As New CommonBizLogic
    Private Const CHK_TRUE As String = "1"
    Private Const CHK_False As String = "0"

#Region "プロパティ"
    ''' <summary>
    ''' 加盟店コードHiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKameitenCd() As HtmlInputHidden
        Get
            Return HiddenKameitenCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' 請求先コードHiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiCd() As HtmlInputHidden
        Get
            Return HiddenSeikyuuSakiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSeikyuuSakiCd = value
        End Set
    End Property

    ''' <summary>
    ''' 請求先枝番Hiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiBrc() As HtmlInputHidden
        Get
            Return HiddenSeikyuuSakiBrc
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSeikyuuSakiBrc = value
        End Set
    End Property

    ''' <summary>
    ''' 請求先区分Hiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiKbn() As HtmlInputHidden
        Get
            Return HiddenSeikyuuSakiKbn
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSeikyuuSakiKbn = value
        End Set
    End Property

    ''' <summary>
    ''' 調査会社コードHiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTysKaisyaCd() As HtmlInputHidden
        Get
            Return HiddenTysKaisyaCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTysKaisyaCd = value
        End Set
    End Property

    ''' <summary>
    ''' 調査会社事業所コードHiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTysKaisyaJigyousyoCd() As HtmlInputHidden
        Get
            Return HiddenTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTysKaisyaJigyousyoCd = value
        End Set
    End Property

    ''' <summary>
    ''' 請求締め日Hiddenへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSimeDate() As HtmlInputHidden
        Get
            Return HiddenSimeDate
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSimeDate = value
        End Set
    End Property

    ''' <summary>
    ''' 請求先/仕入先リンクへの外部アクセス用プロパティ
    ''' </summary>
    ''' <value>LinkSeikyuuSiireHenkou</value>
    ''' <returns>LinkSeikyuuSiireHenkou</returns>
    ''' <remarks></remarks>
    Public Property AccLinkSeikyuuSiireHenkou() As HtmlAnchor
        Get
            Return LinkSeikyuuSiireHenkou
        End Get
        Set(ByVal value As HtmlAnchor)
            LinkSeikyuuSiireHenkou = value
        End Set
    End Property

    ''' <summary>
    ''' 請求先変更時チェックフラグ
    ''' </summary>
    ''' <value>HiddenChkSeikyuuSakiChg</value>
    ''' <returns>HiddenChkSeikyuuSakiChg</returns>
    ''' <remarks></remarks>
    Public Property AccHiddenChkSeikyuuSakiChg() As HtmlInputHidden
        Get
            Return HiddenChkSeikyuuSakiChg
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenChkSeikyuuSakiChg = value
        End Set
    End Property

    ''' <summary>
    ''' 請求先タイプ文字列（直接請求/他請求）
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiTypeStr As String = string.Empty
    ''' <summary>
    ''' 請求先タイプ文字列（直接請求/他請求）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SeikyuuSakiTypeStr() As String
        Get
            Return strSeikyuuSakiTypeStr
        End Get
    End Property

#End Region

    ''' <summary>
    ''' ページ描画前処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        '請求先・仕入先のリンク設定
        SetLinkStyle()

        'リンクの表示On/Off設定
        If Me.HiddenSyouhinCd.Value <> String.Empty Then
            Me.LinkSeikyuuSiireHenkou.Style("display") = "inline"
        Else
            Me.LinkSeikyuuSiireHenkou.Style("display") = "none"
            ClearSeikyuuSiireInfo()
        End If

        'ツールチップの設定
        Dim strToolTip As String = GetLinkTip()
        'Htmlコントロールにツールチップを設定する
        cl.SetToolTipForCtrl(Me.LinkSeikyuuSiireHenkou, strToolTip)

        'イベントハンドラの付与
        Me.LinkSeikyuuSiireHenkou.Attributes("onclick") = "callSeikyuuSiireSakiModal('" _
                                                                & UrlConst.SEIKYUU_SIIRE_HENKOU & "','" _
                                                                & Me.HiddenSeikyuuSakiCd.ClientID & "','" _
                                                                & Me.HiddenSeikyuuSakiBrc.ClientID & "','" _
                                                                & Me.HiddenSeikyuuSakiKbn.ClientID & "','" _
                                                                & Me.HiddenTysKaisyaCd.ClientID & "','" _
                                                                & Me.HiddenTysKaisyaJigyousyoCd.ClientID & "','" _
                                                                & Me.HiddenKameitenCd.ClientID & "','" _
                                                                & Me.HiddenDefaultSiireSaki.ClientID & "','" _
                                                                & Me.HiddenSyouhinCd.ClientID & "','" _
                                                                & Me.HiddenKojKaisyaSeikyuu.ClientID & "','" _
                                                                & Me.HiddenKojKaisyaCd.ClientID & "','" _
                                                                & Me.HiddenUriageSyorizumi.ClientID & "','" _
                                                                & Me.HiddenViewMode.ClientID & "','" _
                                                                & Me.UpdatePanelSeikyuuSiireLink.ClientID & "','" _
                                                                & Me.HiddenChkSeikyuuSakiChg.ClientID & "')"
        'アップデートパネル(リンク等)の更新
        Me.UpdatePanelSeikyuuSiireLink.Update()

    End Sub

    ''' <summary>
    ''' 邸別請求レコードから請求先/仕入先リンクに値をセット
    ''' </summary>
    ''' <param name="recData">邸別請求レコード</param>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuSiireLinkFromTeibetuRec(ByVal recData As TeibetuSeikyuuRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSeikyuuSiireLinkFromTeibetuRec", _
                                                    recData)
        With recData
            ' 請求先コード
            Me.HiddenSeikyuuSakiCd.Value = cl.GetDispStr(.SeikyuuSakiCd)
            ' 請求先枝番
            Me.HiddenSeikyuuSakiBrc.Value = cl.GetDispStr(.SeikyuuSakiBrc)
            ' 請求先区分
            Me.HiddenSeikyuuSakiKbn.Value = cl.GetDispStr(.SeikyuuSakiKbn)
            ' 調査会社コード
            Me.HiddenTysKaisyaCd.Value = cl.GetDispStr(.TysKaisyaCd)
            ' 調査会社事業所コード
            Me.HiddenTysKaisyaJigyousyoCd.Value = cl.GetDispStr(.TysKaisyaJigyousyoCd)
        End With
    End Sub

    ''' <summary>
    ''' 請求先/仕入先リンクから邸別請求レコードに値をセット
    ''' </summary>
    ''' <param name="redData">邸別請求レコード(参照渡し)</param>
    ''' <remarks></remarks>
    Public Sub SetTeibetuRecFromSeikyuuSiireLink(ByRef redData As TeibetuSeikyuuRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetTeibetuRecFromSeikyuuSiireLink", _
                                                    redData)
        With redData
            '請求先コード
            If Me.HiddenSeikyuuSakiCd.Value <> String.Empty Then
                .SeikyuuSakiCd = Me.HiddenSeikyuuSakiCd.Value
            Else
                redData.SeikyuuSakiCd = Nothing
            End If
            '請求先枝番
            If Me.HiddenSeikyuuSakiBrc.Value <> String.Empty Then
                redData.SeikyuuSakiBrc = Me.HiddenSeikyuuSakiBrc.Value
            Else
                redData.SeikyuuSakiBrc = Nothing
            End If
            '請求先区分
            If Me.HiddenSeikyuuSakiKbn.Value <> String.Empty Then
                redData.SeikyuuSakiKbn = Me.HiddenSeikyuuSakiKbn.Value
            Else
                redData.SeikyuuSakiKbn = Nothing
            End If
            '調査会社コード
            If Me.HiddenTysKaisyaCd.Value <> String.Empty Then
                redData.TysKaisyaCd = Me.HiddenTysKaisyaCd.Value
            Else
                redData.TysKaisyaCd = Nothing
            End If
            '調査会社事業所コード
            If Me.HiddenTysKaisyaJigyousyoCd.Value <> String.Empty Then
                redData.TysKaisyaJigyousyoCd = Me.HiddenTysKaisyaJigyousyoCd.Value
            Else
                redData.TysKaisyaJigyousyoCd = Nothing
            End If
        End With
    End Sub

    ''' <summary>
    ''' 画面によって値が変更され得るコントロールから請求先/仕入先リンクに値をセット
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSiireSakiCd">仕入先コード(地盤の調査会社コード)</param>
    ''' <param name="strKeijyouZumi">計上済み判断(1：計上済み／1以外：未計上)</param>
    ''' <param name="blnKojKaisyaSeikyuu">工事会社請求判断</param>
    ''' <param name="strKojKaisyaCd">工事会社コード</param>
    ''' <param name="strDenUriDate">伝票売上年月日</param>
    ''' <param name="strViewMode">表示モード</param>
    ''' <returns>請求先情報を格納したDictionary</returns>
    ''' <remarks>請求先・仕入先の基本情報をセットする為の情報を親画面から取得する</remarks>
    Public Function SetVariableValueCtrlFromParent(ByVal strKameitenCd As String _
                                            , ByVal strSyouhinCd As String _
                                            , ByVal strSiireSakiCd As String _
                                            , ByVal strKeijyouZumi As String _
                                            , Optional ByVal blnKojKaisyaSeikyuu As Boolean = False _
                                            , Optional ByVal strKojKaisyaCd As String = "" _
                                            , Optional ByVal strDenUriDate As String = "" _
                                            , Optional ByVal strViewMode As String = EarthConst.MODE_EDIT) As Dictionary(Of String, String)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetVariableValueCtrlFromParent" _
                                                    , strKameitenCd _
                                                    , strSyouhinCd _
                                                    , strSiireSakiCd _
                                                    , strKojKaisyaCd _
                                                    , strDenUriDate _
                                                    , strViewMode)
        '基本請求先格納Dictionary
        Dim dicSeikyuuSakiInfo As New Dictionary(Of String, String)

        '商品コード
        Me.HiddenSyouhinCd.Value = strSyouhinCd

        '加盟店コード
        Me.HiddenKameitenCd.Value = strKameitenCd
        '工事会社請求
        If blnKojKaisyaSeikyuu Then
            Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE
        Else
            Me.HiddenKojKaisyaSeikyuu.Value = CHK_False
        End If
        '工事会社コード
        Me.HiddenKojKaisyaCd.Value = strKojKaisyaCd

        '基本仕入先コードの設定
        Me.HiddenDefaultSiireSaki.Value = strSiireSakiCd

        '売上計上済のセット
        Me.HiddenUriageSyorizumi.Value = strKeijyouZumi

        '伝票売上年月日のセット
        Me.HiddenDenUriDate.Value = strDenUriDate

        '表示モードのセット
        Me.HiddenViewMode.Value = strViewMode

        '商品コードがない場合はリンクを非表示なので、情報を取得する為のDBアクセスはしない
        If Me.HiddenSyouhinCd.Value = String.Empty Then

            'アップデートパネル(基本情報等のHidden)の更新
            Me.UpdatePanelSeikyuuSiireInfo.Update()

            Return Nothing
            Exit Function
        End If

        '基本請求先の取得
        If Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE Then
            '工事会社請求
            dicSeikyuuSakiInfo = cbl.getDefaultSeikyuuSaki(strKojKaisyaCd)
        Else
            '加盟店請求
            dicSeikyuuSakiInfo = cbl.getDefaultSeikyuuSaki(strKameitenCd, strSyouhinCd)
        End If

        '請求先タイプ文字列のセット
        SetSeikyuuSakiTypeStr(dicSeikyuuSakiInfo)

        '基本請求先コードの設定
        Me.HiddenDefaultSeikyuuSaki.Value = dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiCd) & _
                                            dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiBrc) & _
                                            dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiKbn)

        '基本請求先名の設定
        HiddenDefaultSeikyuuSakiMei.Value = dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiMei)

        'アップデートパネル(基本情報等のHidden)の更新
        Me.UpdatePanelSeikyuuSiireInfo.Update()

        Return dicSeikyuuSakiInfo

    End Function

    ''' <summary>
    ''' 請求締め日の設定
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="blnKojKaisyaSeikyuu">工事会社請求</param>
    ''' <param name="strKojKaisyaCd">工事会社コード</param>
    ''' <remarks>自動設定用</remarks>
    Public Sub SetSeikyuuSimeDate(ByVal strSyouhinCd As String, Optional ByVal blnKojKaisyaSeikyuu As Boolean = False, Optional ByVal strKojKaisyaCd As String = "")
        '商品コード
        Me.HiddenSyouhinCd.Value = strSyouhinCd

        '工事会社請求
        If blnKojKaisyaSeikyuu Then
            Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE
        Else
            Me.HiddenKojKaisyaSeikyuu.Value = CHK_False
        End If

        '工事会社コード
        Me.HiddenKojKaisyaCd.Value = strKojKaisyaCd

        '請求締め日のセット
        SetSeikyuuSimeDate()

    End Sub

    ''' <summary>
    ''' 請求締め日の設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuSimeDate()
        '請求締め日の設定
        If Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE Then
            '工事会社請求
            Me.HiddenSimeDate.Value = cl.GetDisplayString(cbl.GetSeikyuuSimeDateFromTyousa(Me.HiddenSeikyuuSakiCd.Value _
                                                                                    , Me.HiddenSeikyuuSakiBrc.Value _
                                                                                    , Me.HiddenSeikyuuSakiKbn.Value _
                                                                                    , Me.HiddenKojKaisyaCd.Value))
        Else
            '加盟店請求
            Me.HiddenSimeDate.Value = cl.GetDisplayString(cbl.GetSeikyuuSimeDateFromKameiten(Me.HiddenSeikyuuSakiCd.Value _
                                                                                    , Me.HiddenSeikyuuSakiBrc.Value _
                                                                                    , Me.HiddenSeikyuuSakiKbn.Value _
                                                                                    , Me.HiddenKameitenCd.Value _
                                                                                    , Me.HiddenSyouhinCd.Value))
        End If

    End Sub

    ''' <summary>
    ''' 請求書発行日の取得
    ''' </summary>
    ''' <returns>請求書発行日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoHakkouDate(Optional ByVal strDenUriDate As String = "") As String

        If strDenUriDate = String.Empty Then
            strDenUriDate = Me.HiddenDenUriDate.Value
        End If
        Dim SeikyuusyoHakkouDate As String = cl.GetDisplayString(cbl.CalcSeikyuusyoHakkouDate(Me.HiddenSimeDate.Value, strDenUriDate))

        Return SeikyuusyoHakkouDate
    End Function

#Region "プライベートメソッド"
    ''' <summary>
    ''' 請求先/仕入先変更リンクスタイルの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLinkStyle()
        Const LINK_STYLE As String = "text-decoration:underline;cursor:pointer;"
        Const RED_BOLD_STYLE As String = "color:Red;font-weight:bold;"

        '基本請求先コード
        Dim strDefSeikyuuSaki As String = Me.HiddenDefaultSeikyuuSaki.Value
        '基本仕入先コード
        Dim strDefSiireSaki As String = Me.HiddenDefaultSiireSaki.Value
        '登録請求先コード
        Dim strChgSeikyuuSaki As String = Me.HiddenSeikyuuSakiCd.Value _
                                        & Me.HiddenSeikyuuSakiBrc.Value _
                                        & Me.HiddenSeikyuuSakiKbn.Value
        '登録仕入先コード
        Dim strChgSiireSaki As String = Me.HiddenTysKaisyaCd.Value _
                                        & Me.HiddenTysKaisyaJigyousyoCd.Value

        'リンクにスタイルの適用(ポインタの変更とアンダーバーの付与)
        Me.LinkSeikyuuSiireHenkou.Attributes("style") = LINK_STYLE
        Me.lblLinkSeikyuuStr.Attributes("style") = LINK_STYLE
        Me.lblLinkSiireStr.Attributes("style") = LINK_STYLE

        'リンクの色を設定
        If strChgSeikyuuSaki <> String.Empty And strChgSeikyuuSaki <> strDefSeikyuuSaki Then
            lblLinkSeikyuuStr.Attributes("style") = RED_BOLD_STYLE
        End If

        If strChgSiireSaki <> String.Empty And strChgSiireSaki <> strDefSiireSaki Then
            lblLinkSiireStr.Attributes("style") = RED_BOLD_STYLE
        End If

    End Sub

    ''' <summary>
    ''' 請求先・仕入先のツールチップ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetLinkTip() As String
        Dim strSeikyuuSakiCd As String = Me.HiddenSeikyuuSakiCd.Value
        Dim strSeikyuuSakiBrc As String = Me.HiddenSeikyuuSakiBrc.Value
        Dim strSeikyuuSakiKbn As String = Me.HiddenSeikyuuSakiKbn.Value
        Dim strKameitenCd As String = Me.HiddenKameitenCd.Value
        Dim strSyouhinCd As String = Me.HiddenSyouhinCd.Value
        Dim strDefaultSiireSaki As String = Me.HiddenDefaultSiireSaki.Value
        Dim strChangeSeikyuuSaki As String = strSeikyuuSakiCd & strSeikyuuSakiBrc & strSeikyuuSakiKbn
        Dim strChangeSiireSaki As String = Me.HiddenTysKaisyaCd.Value & Me.HiddenTysKaisyaJigyousyoCd.Value

        Dim strTipSeikyuuSaki As String
        Dim strTipSiireSaki As String
        Dim uriageLogic As New UriageDataSearchLogic
        Dim seikyuuSakiList As List(Of SeikyuuSakiInfoRecord)
        Dim tysKaisyaLogic As New TyousakaisyaSearchLogic
        Dim strTysKaisyaCd As String

        '請求先名の取得
        If strChangeSeikyuuSaki = String.Empty Then
            '変更が無い場合はデフォルトの請求先名
            strTipSeikyuuSaki = Me.HiddenDefaultSeikyuuSakiMei.Value
        Else
            '変更がある場合には新しい請求先名
            seikyuuSakiList = uriageLogic.GetSeikyuuSakiInfo(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn)
            If seikyuuSakiList.Count > 0 Then
                strTipSeikyuuSaki = seikyuuSakiList(0).SeikyuuSakiMei
            Else
                strTipSeikyuuSaki = String.Empty
            End If
        End If

        '仕入先コードの設定
        If strChangeSiireSaki = String.Empty Then
            '変更が無い場合にはデフォルトの仕入先名
            strTysKaisyaCd = strDefaultSiireSaki
        Else
            '変更がある場合には新しい仕入先名
            strTysKaisyaCd = strChangeSiireSaki
        End If
        '仕入先名の取得
        strTipSiireSaki = tysKaisyaLogic.GetTyousaKaisyaMei(strTysKaisyaCd, String.Empty, False)

        'リンクのツールチップ設定
        Dim cLogic As New CommonLogic
        Dim strLinkTip As String = String.Format(EarthConst.LINK_TIP_STRING, strTipSeikyuuSaki, strTipSiireSaki)

        Return strLinkTip
    End Function

    ''' <summary>
    ''' 請求先・仕入先情報のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearSeikyuuSiireInfo()
        Me.HiddenSeikyuuSakiCd.Value = String.Empty
        Me.HiddenSeikyuuSakiBrc.Value = String.Empty
        Me.HiddenSeikyuuSakiKbn.Value = String.Empty
        Me.HiddenTysKaisyaCd.Value = String.Empty
        Me.HiddenTysKaisyaJigyousyoCd.Value = String.Empty
    End Sub

    ''' <summary>
    ''' 請求先タイプ文字列のセット（直接請求/他請求）
    ''' </summary>
    ''' <param name="dicSeikyuuSakiInfo">請求先情報Dictionary</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSakiTypeStr(ByVal dicSeikyuuSakiInfo As Dictionary(Of String, String))
        Dim strSeikyuuSakiCd As String = Me.HiddenSeikyuuSakiCd.Value
        Dim strSeikyuuSakiBrc As String = Me.HiddenSeikyuuSakiBrc.Value
        Dim strSeikyuuSakikbn As String = Me.HiddenSeikyuuSakiKbn.Value
        Dim strKameitenCd As String = Me.HiddenKameitenCd.Value
        Dim strKihonSeikyuuSakiCd As String = dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiCd)

        '請求先タイプの判断
        If strSeikyuuSakiCd = String.Empty _
        And strSeikyuuSakiBrc = String.Empty _
        And strSeikyuuSakikbn = String.Empty Then
            If strKameitenCd = strKihonSeikyuuSakiCd Then
                '直接請求
                strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TYOKUSETU
            Else
                '他請求
                strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TASETU
            End If
        Else
            If strKameitenCd = strSeikyuuSakiCd Then
                '直接請求
                strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TYOKUSETU
            Else
                '他請求
                strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TASETU
            End If
        End If

    End Sub
#End Region

End Class