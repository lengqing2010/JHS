
Partial Public Class IraiCtrl2
    Inherits System.Web.UI.UserControl

#Region "メンバ変数"
    Dim iraiSession As New IraiSession
    Dim sk As String = String.Empty
    Dim user_info As New LoginUserInfo      'ユーザー情報格納用
    Dim masterAjaxSM As New ScriptManager   'マスターページScriptManager格納用
    Dim ComLog As New CommonLogic           '画面共通処理ロジッククラス
    Dim jSM As New JibanSessionManager      'セッション管理クラス
    Dim jBn As New Jiban                    '地盤画面共通クラス
    Dim MyLogic As New JibanLogic
    Dim intSetSts As Integer = 0            '商品1取得ステータス
    Dim pStrUCtrl1Id = "ctl00_CPH1_IraiCtrl1_1_"
    Dim cbLogic As New CommonBizLogic
    Dim tLogic As New TokubetuTaiouLogic    '特別対応ロジッククラス

    ''' <summary>
    '''画面表示時や参照時の加盟店、調査会社マスタ検索時に、商品情報再取得を行わないためのフラグ 
    ''' </summary>
    ''' <remarks></remarks>
    Dim flgNotGetSyouhin As Boolean = False

    ''' <summary>
    ''' 依頼業務権限があるか否かを保持
    ''' </summary>
    ''' <remarks></remarks>
    Private flgIraiGyoumuKengen As Boolean

    ''' <summary>
    '''商品１をクリアできない場合、Falseとなるフラグ（依頼内容確定ボタン押下時用）
    ''' </summary>
    ''' <remarks></remarks>
    Dim flgShouhin1ClrOK As Boolean = True

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaGamenAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal irai1Mode As String, ByVal actMode As String)

    ''' <summary>
    ''' 登録完了時の親画面の処理実行用カスタムイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaActAtAfterExe(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs, _
                                    ByVal irai1Mode As String, _
                                    ByVal actMode As String, _
                                    ByVal exeMode As String, _
                                    ByVal result As Boolean, _
                                    ByVal jibanRecAfterUpdate As JibanRecord)

    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

    ''' <summary>
    '''調査方法SDS自動設定チェック時、取得した調査会社格納用
    ''' </summary>
    ''' <remarks></remarks>
    Dim strTysGaisyaCd As String = String.Empty

    ''' <summary>
    '''調査方法SDS自動設定チェック時の結果格納用
    ''' </summary>
    ''' <remarks></remarks>
    Dim blnRet As Boolean = False

#End Region

#Region "ユーザーコントロールへの外部からのアクセス用Getter/Setter"

    ''' <summary>
    ''' 外部からのアクセス用 for btnIrainaiyouKaijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBtnirainaiyoukaijo() As HtmlInputButton
        Get
            Return btnIrainaiyouKaijo
        End Get
        Set(ByVal value As HtmlInputButton)
            btnIrainaiyouKaijo = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for btnIrainaiyouKakutei
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBtnirainaiyoukakutei() As HtmlInputButton
        Get
            Return btnIrainaiyouKakutei
        End Get
        Set(ByVal value As HtmlInputButton)
            btnIrainaiyouKakutei = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for btn_irai2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBtn_Irai2() As HtmlInputButton
        Get
            Return btn_irai2
        End Get
        Set(ByVal value As HtmlInputButton)
            btn_irai2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for itemKb_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccItemkb_3() As HtmlInputRadioButton
        Get
            Return itemKb_3
        End Get
        Set(ByVal value As HtmlInputRadioButton)
            itemKb_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for btnSetSyouhin1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBtnSetSyouhin1() As HtmlInputButton
        Get
            Return btnSetSyouhin1
        End Get
        Set(ByVal value As HtmlInputButton)
            btnSetSyouhin1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for kameitenCd
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtKameitenCd() As HtmlInputText
        Get
            Return kameitenCd
        End Get
        Set(ByVal value As HtmlInputText)
            kameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for choSyouhin1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccCboSyouhin1() As DropDownList
        Get
            Return choSyouhin1
        End Get
        Set(ByVal value As DropDownList)
            choSyouhin1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for cboTyousaHouhou
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AcCboTysHouhou() As DropDownList
        Get
            Return cboTyousaHouhou
        End Get
        Set(ByVal value As DropDownList)
            cboTyousaHouhou = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenKakuteiValuesTokubetu
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenKakuteiValuesTokubetu() As HtmlInputHidden
        Get
            Return HiddenKakuteiValuesTokubetu
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKakuteiValuesTokubetu = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 商品UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccUpdPanelSyouhin() As UpdatePanel
        Get
            Return updPanelSyouhin
        End Get
        Set(ByVal value As UpdatePanel)
            updPanelSyouhin = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenChgTokuUpdDatetime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenChgTokuUpdDatetime() As HtmlInputHidden
        Get
            Return HiddenChgTokuUpdDatetime
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenChgTokuUpdDatetime = value
        End Set
    End Property

#Region "商品123情報(商品コード)"
    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_1_1
    ''' </summary>
    ''' <value></value>


    Public Property AccTxtSyouhinCd_1_1() As HtmlInputText
        Get
            Return shouhinCd_1_1
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_1_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_2_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_2_1() As HtmlInputText
        Get
            Return shouhinCd_2_1
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_2_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_2_2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_2_2() As HtmlInputText
        Get
            Return shouhinCd_2_2
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_2_2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_2_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_2_3() As HtmlInputText
        Get
            Return shouhinCd_2_3
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_2_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_2_4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_2_4() As HtmlInputText
        Get
            Return shouhinCd_2_4
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_2_4 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_1() As HtmlInputText
        Get
            Return shouhinCd_3_1
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_2() As HtmlInputText
        Get
            Return shouhinCd_3_2
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_3() As HtmlInputText
        Get
            Return shouhinCd_3_3
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_4() As HtmlInputText
        Get
            Return shouhinCd_3_4
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_4 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_5
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_5() As HtmlInputText
        Get
            Return shouhinCd_3_5
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_5 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_6
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_6() As HtmlInputText
        Get
            Return shouhinCd_3_6
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_6 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_7
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_7() As HtmlInputText
        Get
            Return shouhinCd_3_7
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_7 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_8
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_8() As HtmlInputText
        Get
            Return shouhinCd_3_8
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_8 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shouhinCd_3_9
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtSyouhinCd_3_9() As HtmlInputText
        Get
            Return shouhinCd_3_9
        End Get
        Set(ByVal value As HtmlInputText)
            shouhinCd_3_9 = value
        End Set
    End Property
#End Region

#Region "商品123情報(計上FLG)"
    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_1_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_1_1() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_1_1
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_1_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_2_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_2_1() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_2_1
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_2_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_2_2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_2_2() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_2_2
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_2_2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_2_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_2_3() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_2_3
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_2_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_2_4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_2_4() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_2_4
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_2_4 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_1() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_1
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_2() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_2
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_3() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_3
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_4() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_4
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_4 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_5
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_5() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_5
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_5 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_6
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_6() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_6
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_6 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_7
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_7() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_7
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_7 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_8
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_8() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_8
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_8 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for uriageKeijyouFlg_3_9
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtUriKeijyouFlg_3_9() As HtmlInputHidden
        Get
            Return uriageKeijyouFlg_3_9
        End Get
        Set(ByVal value As HtmlInputHidden)
            uriageKeijyouFlg_3_9 = value
        End Set
    End Property
#End Region

#Region "商品123情報(発注書金額)"
    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_1_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_1_1() As HtmlInputText
        Get
            Return hattyuuKingaku_1_1
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_1_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_2_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_2_1() As HtmlInputText
        Get
            Return hattyuuKingaku_2_1
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_2_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_2_2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_2_2() As HtmlInputText
        Get
            Return hattyuuKingaku_2_2
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_2_2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_2_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_2_3() As HtmlInputText
        Get
            Return hattyuuKingaku_2_3
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_2_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_2_4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_2_4() As HtmlInputText
        Get
            Return hattyuuKingaku_2_4
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_2_4 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_1() As HtmlInputText
        Get
            Return hattyuuKingaku_3_1
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_2() As HtmlInputText
        Get
            Return hattyuuKingaku_3_2
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_3() As HtmlInputText
        Get
            Return hattyuuKingaku_3_3
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_4() As HtmlInputText
        Get
            Return hattyuuKingaku_3_4
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_4 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_5
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_5() As HtmlInputText
        Get
            Return hattyuuKingaku_3_5
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_5 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_6
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_6() As HtmlInputText
        Get
            Return hattyuuKingaku_3_6
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_6 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_7
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_7() As HtmlInputText
        Get
            Return hattyuuKingaku_3_7
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_7 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_8
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_8() As HtmlInputText
        Get
            Return hattyuuKingaku_3_8
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_8 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hattyuuKingaku_3_9
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTxtHattyuuKingaku_3_9() As HtmlInputText
        Get
            Return hattyuuKingaku_3_9
        End Get
        Set(ByVal value As HtmlInputText)
            hattyuuKingaku_3_9 = value
        End Set
    End Property
#End Region

#Region "特別対応価格反映フラグ"
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTokutaiKkkHaneiFlg
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTokutaiKkkHaneiFlg() As HtmlInputHidden
        Get
            Return HiddenTokutaiKkkHaneiFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTokutaiKkkHaneiFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTokutaiKkkHaneiFlgJT(特別対応画面用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTokutaiKkkHaneiFlgPu() As HtmlInputHidden
        Get
            Return HiddenTokutaiKkkHaneiFlgPu
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTokutaiKkkHaneiFlgPu = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTokutaiPreMode
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTokutaiPreMode() As HtmlInputHidden
        Get
            Return HiddenTokutaiPreMode
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTokutaiPreMode = value
        End Set
    End Property
#End Region

#Region "商品123情報(特別対応ツールチップ)"
    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_1_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_1_1() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_1_1
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_1_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_2_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_2_1() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_2_1
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_2_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_2_2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_2_2() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_2_2
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_2_2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_2_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_2_3() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_2_3
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_2_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_2_4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_2_4() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_2_4
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_2_4 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_1() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_1
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_2() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_2
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_3() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_3
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_4() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_4
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_4 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_5
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_5() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_5
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_5 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_6
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_6() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_6
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_6 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_7
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_7() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_7
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_7 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_8
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_8() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_8
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_8 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl_3_9
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip_3_9() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl_3_9
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl_3_9 = value
        End Set
    End Property
#End Region

#Region "特別対応更新対象コード"
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenChgTokuCd
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenChgTokuCd() As HtmlInputHidden
        Get
            Return HiddenChgTokuCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenChgTokuCd = value
        End Set
    End Property
#End Region

#Region "連棟物件数"
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenRentouBukkenSuu
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenRentouBukkenSuu() As HtmlInputHidden
        Get
            Return HiddenRentouBukkenSuu
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenRentouBukkenSuu = value
        End Set
    End Property
#End Region

#Region "画面遷移時情報保持_確認モード (非表示)"
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenModeKakunin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenModeKakunin() As HtmlInputHidden
        Get
            Return HiddenModeKakunin
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenModeKakunin = value
        End Set
    End Property
#End Region

#Region "工務店請求税抜金額"
    ''' <summary>
    ''' 外部からのアクセス用 for koumutenSeikyuZeinukiGaku_1_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AcckoumutenSeikyuZeinukiGaku_1_1() As HtmlInputText
        Get
            Return koumutenSeikyuZeinukiGaku_1_1
        End Get
        Set(ByVal value As HtmlInputText)
            koumutenSeikyuZeinukiGaku_1_1 = value
        End Set
    End Property
#End Region

#Region "実請求税抜金額"
    ''' <summary>
    ''' 外部からのアクセス用 for jituSeikyuZeinukiGaku_1_1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccjituSeikyuZeinukiGaku_1_1() As HtmlInputText
        Get
            Return jituSeikyuZeinukiGaku_1_1
        End Get
        Set(ByVal value As HtmlInputText)
            jituSeikyuZeinukiGaku_1_1 = value
        End Set
    End Property
#End Region

#End Region

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        ' ユーザー基本認証
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If
        '依頼業務権限チェック
        If user_info.IraiGyoumuKengen = -1 Then
            flgIraiGyoumuKengen = True
        Else
            flgIraiGyoumuKengen = False
        End If

        If IsPostBack = False Then

            ' 工事担当者は加盟店の設定により表示されるので初期は非表示
            kojTantoSpan.Visible = False
            koujiTantouNm.Value = String.Empty

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' ドロップダウンリストのデータ設定ヘルパー
            Dim helper As New DropDownHelper
            ' 調査概要コンボにデータをバインドする
            helper.SetDropDownList(cboTyousaGaiyou, DropDownHelper.DropDownType.TyousaGaiyou, True)
            ' 建物用途コンボにデータをバインドする
            helper.SetDropDownList(cboTatemonoYouto, DropDownHelper.DropDownType.TatemonoYouto, True, False)

            'セッション値がある場合はセッションからDDLを生成する
            If iraiSession.DdlDataStr IsNot Nothing Then
                '●画面間遷移時(復元)
                jSM.Text2Array(iraiSession.DdlDataStr, Me.choSyouhin1, DropDownHelper.DropDownType.Syouhin1, True)
                jSM.Text2Array(iraiSession.DdlDataStr, Me.cboTyousaHouhou, DropDownHelper.DropDownType.TyousaHouhou, True)
            Else
                '●初期表示時(通常)
                ' 商品1コンボにデータをバインドする
                helper.SetDropDownList(choSyouhin1, DropDownHelper.DropDownType.Syouhin1, True, True)
                ' 調査方法コンボにデータをバインドする
                helper.SetDropDownList(cboTyousaHouhou, DropDownHelper.DropDownType.TyousaHouhou, True, False)
            End If

            '地盤読み込みデータがセッションに存在する場合、画面に表示させる
            If iraiSession.JibanData IsNot Nothing And _
               Request("st") = EarthConst.MODE_VIEW And _
               iraiSession.ExeMode <> EarthConst.MODE_EXE_DIRECT_TOUROKU Then
                'actModeに「参照」をセット
                actMode.Value = EarthConst.MODE_VIEW
                Dim jr As JibanRecord = iraiSession.JibanData
                setIrai2FromJibanRec(sender, e, jr)
                '地盤読み込みデータを消去
                iraiSession.JibanData = Nothing
            ElseIf actMode.Value = "" Then
                'actMode未設定時に「編集」をセット
                actMode.Value = EarthConst.MODE_EDIT
            End If

            '****************************************************************************
            ' セッションから画面表示値を取得
            '****************************************************************************
            'iraiSession.Irai2Dataにデータが無く、iraiSession.Irai2DataStrに文字データで存在する場合、変換して取得
            If iraiSession.Irai2Data Is Nothing And iraiSession.Irai2DataStr IsNot Nothing Then
                If iraiSession.Irai2DataStr <> String.Empty Then
                    jSM.HashStr2Ctrl(Me, iraiSession.Irai2Mode, jSM.Text2hash(iraiSession.Irai2DataStr))
                    jSM.Ctrl2Hash(Me, jBn.IraiData)
                    iraiSession.Irai2Data = jBn.IraiData
                End If
            End If

            '認証結果によって画面表示を切替える
            If flgIraiGyoumuKengen Then
                '依頼業務権限が有る場合、編集可能
                jSM.Hash2Ctrl(Me, iraiSession.Irai2Mode, iraiSession.Irai2Data)
                If iraiSession.IraiST = EarthConst.MODE_NEW Then
                    If flgKakutei.Value = "0" Then
                        jSM.Hash2Ctrl(irai2TBody2, EarthConst.MODE_VIEW, iraiSession.Irai2Data)
                        btnIrainaiyouKaijo.Visible = False
                    ElseIf flgKakutei.Value = "1" Then
                        jSM.Hash2Ctrl(irai2TBody, EarthConst.MODE_VIEW, iraiSession.Irai2Data)
                        '各種検索ボタンを非表示
                        kameitenSearch.Visible = False
                        tyousakaisyaSearch.Visible = False
                        btnIrainaiyouKakutei.Visible = False
                    End If
                End If
            Else
                '依頼業務権限が無い場合、編集不可
                jSM.Hash2Ctrl(Me, EarthConst.MODE_VIEW, iraiSession.Irai2Data)
                '各種検索ボタンを非表示
                kameitenSearch.Visible = False
                tyousakaisyaSearch.Visible = False
            End If

            '検索用にirai1の区分値をirai2内のコントロールに値コピーする
            If iraiSession.Irai1Data IsNot Nothing Then
                kubunVal.Value = iraiSession.Irai1Data(pStrUCtrl1Id & "cboKubun").Text
                hosyousyoNoVal.Value = iraiSession.Irai1Data(pStrUCtrl1Id & "hoshouNo").Value
                hakiSyubetuVal.Value = iraiSession.Irai1Data(pStrUCtrl1Id & "cboDataHaki").Text
                kubunId.Value = iraiSession.Irai1Data.Item(pStrUCtrl1Id & "cboKubun").ClientID
                hosyousyoNoId.Value = iraiSession.Irai1Data.Item(pStrUCtrl1Id & "hoshouNo").ClientID
                If Me.HiddenInsTokubetuTaiouFlg.Value <> "@" Then
                    Me.HiddenInsTokubetuTaiouFlg.Value = iraiSession.Irai1Data.Item(pStrUCtrl1Id & "HiddenInsTokubetuTaiouFlg").Value
                End If
                Me.HiddenRentouBukkenSuu.Value = iraiSession.Irai1Data.Item(pStrUCtrl1Id & "HiddenRentouBukkenSuu").Value

            End If

            '確認へ画面遷移時の【特別対応価格反映処理】
            If iraiSession.Irai2Mode = EarthConst.MODE_KAKUNIN Then

                '新規引継で確認画面まで来た場合には、全ての特別対応コードDisplasyを更新対象とする(初回のみ)
                If Me.AccHiddenTokutaiPreMode.Value = EarthConst.MODE_EXE_HIKITUGI Then
                    '画面ツールチップより全ての特別対応コードを集める
                    SetDefaultTokutaiTooltipReg(sender, e)
                    'フラグを初期化
                    Me.AccHiddenTokutaiPreMode.Value = String.Empty
                End If

                '○Step12フラグによる処理
                If Me.HiddenTokutaiKkkHaneiFlg.Value = "0" Then
                    '新規登録時は、価格算出のみ行う
                    calcTeibetuTokubetuKkk(sender, e, False)
                    Me.HiddenTokutaiKkkHaneiFlg.Value = String.Empty '価格反映後はフラグを下げる
                ElseIf Me.HiddenTokutaiKkkHaneiFlg.Value = "1" Then
                    '商品1変更時は、変更された商品に加算する
                    calcTeibetuTokubetuKkk(sender, e, True)
                    Me.HiddenTokutaiKkkHaneiFlg.Value = String.Empty '価格反映後はフラグを下げる
                End If

            End If

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            '確認モードの場合、SDS自動設定処理を行わない
            If iraiSession.Irai2Mode = EarthConst.MODE_KAKUNIN Then
                Me.HiddenModeKakunin.Value = "1"
            End If
            setDispAction(sender, e)

            '****************************************************************************
            ' 画面モードによって、表示状態の切り替えを行う
            '****************************************************************************
            If iraiSession.Irai2Mode = EarthConst.MODE_EDIT Then   '依頼内容画面が編集モードの場合

                '表示切替リンクのリンク先をクリアor設定
                irai2DispLink.HRef = ""
                irai2DispLink2.HRef = ""
                irai2DispLink3.HRef = "javascript:changeDisplay('" & irai2TBody3.ClientID & "','" & syouhin3Display.ClientID & "');"

                '依頼内容編集ボタンの非表示
                btn_irai2.Visible = False

                '新規ではない場合、確定ボタン郡を非表示
                If iraiSession.IraiST <> EarthConst.MODE_NEW Then
                    btnIrainaiyouKakutei.Visible = False
                    btnIrainaiyouKaijo.Visible = False
                    iraiKakuteiTable.Visible = False
                End If

                'イベントハンドラを設定
                '特別対応価格が付随している場合は、変更時に確認メッセージを表示
                Dim ChkTokubetuTaiouScript As String = "ChkTokubetuTaiou('" & Me.ucTokubetuTaiouToolTipCtrl_1_1.AccDisplayCd.ClientID & "')"
                Dim setKameiScript As String = "CopyItemValue('" & Me.kameitenCdOld.ClientID & "', '" & Me.kameitenCd.ClientID & "');"
                Dim setTysGaisyaScript As String = "CopyItemValue('" & Me.tyousakaisyaCdOld.ClientID & "', '" & Me.tyousakaisyaCd.ClientID & "');"

                kameitenCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this))if(" & ChkTokubetuTaiouScript _
                                                    & "){{if(checkNumber(this))callKameitenSearch(this)}}else{" & setKameiScript & "};"
                kameitenCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
                tyousakaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this))if(" & ChkTokubetuTaiouScript _
                                                    & "){{if(checkNumber(this))callTyousakaisyaSearch(this)}}else{" & setTysGaisyaScript & "};"
                tyousakaisyaCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

                '請求有無整合性チェックフラグを初期化
                HiddenSeikyuuUmuCheck.Value = String.Empty

                'フォーカスセット
                kameitenCd.Focus()

            Else   '依頼内容画面が編集モード以外の場合

                '表示切替リンクのリンク先を設定
                irai2DispLink.HRef = "javascript:changeDisplay('" & irai2TBody.ClientID & "');changeDisplay('" & irai2TitleInfobar.ClientID & "');"
                irai2DispLink2.HRef = "javascript:changeDisplay('" & irai2TBody2.ClientID & "');"
                irai2DispLink3.HRef = "javascript:changeDisplay('" & irai2TBody3.ClientID & "');"

                '各種検索ボタンを非表示
                kameitenSearch.Visible = False
                tyousakaisyaSearch.Visible = False
                btnIrainaiyouKakutei.Visible = False
                btnIrainaiyouKaijo.Visible = False
                iraiKakuteiTable.Visible = False

                '依頼内容画面が参照モードの場合、情報バー、表示切替リンクの設定を変更
                If iraiSession.Irai2Mode = EarthConst.MODE_VIEW Then
                    irai2TitleInfobar.Style("display") = "inline"
                    irai2TBody.Style("display") = "none"
                    irai2TBody2.Style("display") = "none"
                    irai2TBody3.Style("display") = "none"
                End If

            End If

            'タイトルバーに加盟店コード、加盟店名、調査会社コード、調査会社名を表示
            irai2TitleInfobar.InnerHtml = "【" & kameitenCd.Value & "】 【" & kameitenNm.Value & "】 【" & tyousakaisyaCd.Value & "】 【" & tyousakaisyaNm.Value & "】"

            '============================================================
            'ダイレクト登録モードが指定されている場合、実行処理を行う
            '============================================================
            If iraiSession.ExeMode IsNot Nothing Then
                If iraiSession.ExeMode = EarthConst.MODE_EXE_DIRECT_TOUROKU Then
                    If checkInput(True) Then
                        exeAtKakunin(sender, e, EarthConst.MODE_EXE_TOUROKU)
                        '処理終了後、セッションから確認画面処理モード、地盤データを削除
                        iraiSession.ExeMode = Nothing
                        iraiSession.JibanData = Nothing
                    End If
                End If
            End If

        Else

            '商品毎の請求・仕入先が変更されていないかをチェックし、
            '変更されている場合情報の再取得
            setSeikyuuSiireHenkou(sender, e)

            'セッションに画面表示値を格納
            Dim test As String = Me.ucSeikyuuSiireLink_1_1.GetType.Name
            jSM.Ctrl2Hash(Me, jBn.IraiData)
            iraiSession.Irai2Data = jBn.IraiData

            'セッションにDDL画面表示情報を格納
            jSM.Ddl2Hash(Me.choSyouhin1, jBn.DdlDataSyouhin1)
            iraiSession.DdlDataSyouhin1 = jBn.DdlDataSyouhin1
            jSM.Ddl2Hash(Me.cboTyousaHouhou, jBn.DdlDataTysHouhou)
            iraiSession.DdlDataTysHouhou = jBn.DdlDataTysHouhou

            '商品３エリアの表示を切り替え
            irai2TBody3.Style("display") = syouhin3Display.Value

            '============================================================
            '受注【確認】での各種処理実行
            '============================================================
            If iraiSession.ExeMode IsNot Nothing Then

                exeAtKakunin(sender, e, iraiSession.ExeMode)
                '処理終了後、セッションから確認画面処理モード、地盤データを削除
                iraiSession.ExeMode = Nothing
                iraiSession.JibanData = Nothing
            End If

        End If

        '画面表示時点の値を、Hiddenに保持(原価 変更チェック用)
        If Me.HiddenOpenValuesGenka.Value = String.Empty Then
            Me.HiddenOpenValuesGenka.Value = getCtrlValuesStringGenka()
        End If

        '画面表示時点の値を、Hiddenに保持(販売価格 変更チェック用)
        If Me.HiddenOpenValuesHanbai.Value = String.Empty Then
            Me.HiddenOpenValuesHanbai.Value = getCtrlValuesStringHanbai()
        End If

        '画面表示時点の値を、Hiddenに保持(商品価格設定M 変更チェック用)
        If Me.HiddenOpenValuesSyouhinKkk.Value = String.Empty Then
            Me.HiddenOpenValuesSyouhinKkk.Value = getCtrlValuesStringSyouhinKkk()
        End If

        '表示モードを格納
        actModeIrai2.Value = iraiSession.Irai2Mode

        'コンテキストに値を格納
        Context.Items("irai") = iraiSession

    End Sub

    ''' <summary>
    ''' ページ描画前処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            '++++++++++++++++++++++++++++++++++++++++++++++++++++
            '商品郡の配列から、請求、仕入先変更リンクの生成を行う
            '++++++++++++++++++++++++++++++++++++++++++++++++++++
            '各行のコントロールを変数にセット
            ' 設定するコントロールのインスタンスを取得
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

            '計上済フラグ
            Dim strUriKeijouFlg As String = ctrlRec.UriageKeijyouFlg.Value
            Dim strViewMode As String

            strViewMode = ComLog.GetViewMode(strUriKeijouFlg, user_info.KeiriGyoumuKengen)

            If ln <> "1_1" Then
                If ctrlRec.SyouhinCd.Attributes("readonly") IsNot Nothing AndAlso ctrlRec.SyouhinCd.Attributes("readonly") = True Then
                    strViewMode = EarthConst.MODE_VIEW
                End If
            End If

            If actModeIrai2.Value = EarthConst.MODE_VIEW Or actModeIrai2.Value = EarthConst.MODE_KAKUNIN Then
                strViewMode = actModeIrai2.Value
            End If

            Dim strSetSyouhinCd As String

            If ctrlRec.SyouhinCd.Value = ctrlRec.SyouhinCdOld.Value Then
                strSetSyouhinCd = ctrlRec.SyouhinCd.Value
            Else
                strSetSyouhinCd = String.Empty
            End If

            ctrlRec.SeikyuuSiireLink.SetVariableValueCtrlFromParent(kameitenCd.Value _
                                                                    , strSetSyouhinCd _
                                                                    , tyousakaisyaCd.Value _
                                                                    , strUriKeijouFlg _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)

            '特別対応ツールチップによる商品情報の活性化制御
            If ln.Split("_")(0) = 1 Then
                subEnableSeikyuu1()
            ElseIf ln.Split("_")(0) = 2 Then
                subEnableSeikyuu23(ln)
            End If

        Next

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '表示切替
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '保証商品の表示切替
        ComLog.ChgDispHosyouSyouhin(Me.ChkChgSyouhinHosyouUmu(), Me.TextHosyouSyouhinUmu)
        Me.UpdatePanelHosyouSyouhinUmu.Update()

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction(ByVal sender As Object, ByVal e As System.EventArgs)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 他システムへのリンクボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '加盟店注意事項
        ComLog.getKameitenTyuuijouhouPath(kameitenCd.ClientID, ButtonKameitenTyuuijouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'ビルダー情報表示ボタン
        builderCheck.Attributes("onclick") = "callSearch('" & kameitenCd.ClientID & "','" & UrlConst.BUILDER_INFO & "','','');"
        '商品4
        ComLog.getSyouhin4MasterPath(ButtonSyouhin4, _
                                     user_info, _
                                     kubunVal.ClientID, _
                                     hosyousyoNoVal.ClientID, _
                                     kameitenCd.ClientID, _
                                     tyousakaisyaCd.ClientID)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'プルダウン連動コード入力項目スクリプト設定
        setPullCdScript()

        'ビルダー情報ボタンは非活性
        builderCheck.Disabled = True

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 建物用途、戸数関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 建物用途はデフォルト１
        If cboTatemonoYouto.SelectedValue = String.Empty Then
            tatemonoYoutoCode.Value = "1"
            cboTatemonoYouto.SelectedValue = "1"
        End If
        ' 戸数はデフォルト１
        If kosuu.Value = String.Empty Then
            kosuu.Value = "1"
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 進捗ステータスによって、調査会社の編集可否を切り替え
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        If HiddenStatusIf.Value > EarthConst.IF_STATUS_TOUROKU_ZUMI Then
            Dim ht As New Hashtable
            jSM.Hash2Ctrl(UpdatePanel2, EarthConst.MODE_VIEW, ht)
            tyousakaisyaSearch.Disabled = True
            tyousakaisyaSearch.Visible = False
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 商品関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '商品区分の値によって、表示を切り替える
        checkSyouhinkubun()

        '商品１設定関連ののonchangeイベントハンドラを設定
        setSyouhin1Script()

        '商品郡の表示設定
        shouhinCtrlSetting()

        ' 商品1,2,3の活性制御
        If iraiSession.IraiST <> EarthConst.MODE_NEW Or flgKakutei.Value = "1" Then
            For Each shn As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
                If shn.Split("_")(0) = 1 Then
                    subEnableSeikyuu1()
                Else
                    subEnableSeikyuu23(shn)
                End If
            Next
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ取得
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        flgNotGetSyouhin = True '商品取得を行わないためのフラグを立てる
        If kameitenCd.Value <> "" Then
            actCtrlId.Value = kameitenSearch.ClientID
            kameitenSearchSub(kameitenSearch, e, False)
        End If
        If tyousakaisyaCd.Value <> "" Then
            actCtrlId.Value = tyousakaisyaSearch.ClientID
            tyousakaisyaSearchSub(tyousakaisyaSearch, e, False)
        End If
        flgNotGetSyouhin = False '商品取得を行わないためのフラグを切る

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '破棄種別によって、画面項目を無効化
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        If hakiSyubetuVal.Value >= "1" Then
            '有効化、無効化の対象外にするコントロール郡
            Dim noTarget As New Hashtable
            noTarget.Add(ButtonKameitenTyuuijouhou.ID, True)
            noTarget.Add(builderCheck.ID, True)
            '全てのコントロールを無効化()
            jBn.ChangeDesabledAll(Me, True, noTarget)
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ComLog.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

        '依頼内容確定時、Ajax処理中の場合はボタン押下させない
        Me.btnIrainaiyouKakutei.Attributes("onclick") = "if(flgAjaxRunning != undefined && flgAjaxRunning){alert('" & Messages.MSG104E & "');return false;}"

    End Sub

    ''' <summary>
    ''' プルダウン連動コード入力項目スクリプト設定呼び出し
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setPullCdScript()
        '建物用途
        jBn.SetPullCdScriptSrc(tatemonoYoutoCode, cboTatemonoYouto)
        '調査方法
        jBn.SetPullCdScriptSrc(chousaHouhouCode, cboTyousaHouhou)
    End Sub

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(原価)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringGenka() As String
        Dim sb As New StringBuilder

        sb.Append(Me.tyousakaisyaCd.Value & EarthConst.SEP_STRING)          '調査会社コード
        sb.Append(Me.kameitenCd.Value & EarthConst.SEP_STRING)              '加盟店コード
        sb.Append(Me.keiretuCd.Value & EarthConst.SEP_STRING)               '系列コード
        sb.Append(Me.choSyouhin1.SelectedValue & EarthConst.SEP_STRING)     '商品1プルダウン
        sb.Append(Me.cboTyousaHouhou.SelectedValue & EarthConst.SEP_STRING) '調査方法
        sb.Append(Me.iraiTousuu.Value & EarthConst.SEP_STRING)              '同時依頼棟数

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(販売価格)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringHanbai() As String
        Dim sb As New StringBuilder

        sb.Append(Me.tyousakaisyaCd.Value & EarthConst.SEP_STRING)      '調査会社コード
        sb.Append(Me.kameitenCd.Value & EarthConst.SEP_STRING)          '加盟店コード
        sb.Append(Me.keiretuCd.Value & EarthConst.SEP_STRING)           '系列コード
        sb.Append(Me.EigyousyoCd.Value & EarthConst.SEP_STRING)         '営業所コード
        sb.Append(Me.choSyouhin1.SelectedValue & EarthConst.SEP_STRING) '商品1プルダウン
        sb.Append(Me.cboTyousaHouhou.SelectedValue & EarthConst.SEP_STRING)     '調査方法NO

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(商品価格設定)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringSyouhinKkk() As String
        Dim sb As New StringBuilder

        ' 商品区分
        If itemKb_1.Checked = True Then
            ' 60年保証(EARTHでは非表示)
            sb.Append("1" & EarthConst.SEP_STRING)
        ElseIf itemKb_2.Checked = True Then
            ' 土地販売
            sb.Append("2" & EarthConst.SEP_STRING)
        ElseIf itemKb_3.Checked = True Then
            ' リフォーム
            sb.Append("3" & EarthConst.SEP_STRING)
        Else
            ' 未設定は商品区分 9 
            sb.Append("9" & EarthConst.SEP_STRING)
        End If

        '商品1プルダウン
        sb.Append(Me.choSyouhin1.SelectedValue & EarthConst.SEP_STRING)
        '調査方法NO
        sb.Append(Me.cboTyousaHouhou.SelectedValue & EarthConst.SEP_STRING)

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocus(ByVal ctrl As Control)
        'フォーカスセット
        If iraiSession.Irai2Mode = EarthConst.MODE_EDIT Then
            masterAjaxSM.SetFocus(ctrl)
        End If
    End Sub

    ''' <summary>
    ''' 商品区分の値によって、表示を切り替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkSyouhinkubun()

        'ラジオボタンは非表示で商品区分は全てラベル化
        If itemKb_1.Checked Then
            ' 60年保証
            itemKbSpan_1.Style("display") = "inline"
            itemKbSpan_2.Style("display") = "none"
            itemKbSpan_3.Style("display") = "none"
            itemKbSpan_9.Style("display") = "none"
        ElseIf itemKb_2.Checked Then
            ' 土地販売
            itemKbSpan_1.Style("display") = "none"
            itemKbSpan_2.Style("display") = "inline"
            itemKbSpan_3.Style("display") = "none"
            itemKbSpan_9.Style("display") = "none"
        ElseIf itemKb_3.Checked Then
            ' リフォーム
            itemKbSpan_1.Style("display") = "none"
            itemKbSpan_2.Style("display") = "none"
            itemKbSpan_3.Style("display") = "inline"
            itemKbSpan_9.Style("display") = "none"
        Else
            ' 未設定は商品区分 9 
            itemKbSpan_1.Style("display") = "none"
            itemKbSpan_2.Style("display") = "none"
            itemKbSpan_3.Style("display") = "none"
            itemKbSpan_9.Style("display") = "inline"
        End If

    End Sub

    ''' <summary>
    ''' 商品１設定関連ののonchangeイベントハンドラを設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSyouhin1Script()

        '商品１設定関連ののonchangeイベントハンドラを設定
        Dim tmpScript As String = "callSetSyouhin1(this);"
        Dim ChkTysGaiyouScript As String = "callChkTysGaiyou(this)"
        Dim ChkBuilderScript As String = "callChkBuilder(this)"
        Dim setTysGaiyouScript As String = "callSetTysGaiyou(this);"
        Dim setSyouhinScript As String = "CopyItemValue('" & Me.HiddenSyouhin1Pre.ClientID & "', '" & Me.choSyouhin1.ClientID & "');"
        Dim setTysHouhouScript As String = "CopyItemValue('" & Me.HiddenTysHouhouPre.ClientID & "', '" & Me.cboTyousaHouhou.ClientID & "');CopyItemValue('" & Me.HiddenTysHouhouPre.ClientID & "', '" & Me.chousaHouhouCode.ClientID & "');"
        Dim setTateYoutoScript As String = "CopyItemValue('" & Me.HiddenTatemonoYoutoPre.ClientID & "', '" & Me.cboTatemonoYouto.ClientID & "');CopyItemValue('" & Me.HiddenTatemonoYoutoPre.ClientID & "', '" & Me.tatemonoYoutoCode.ClientID & "');"
        Dim ChkTokubetuTaiouScript As String = "ChkTokubetuTaiou('" & Me.ucTokubetuTaiouToolTipCtrl_1_1.AccDisplayCd.ClientID & "')"

        If iraiSession.IraiST <> EarthConst.MODE_NEW Then
            cboTyousaHouhou.Attributes("onchange") += "if(" & ChkTokubetuTaiouScript & "){" & tmpScript & "}else{" & setTysHouhouScript & "}"
            cboTatemonoYouto.Attributes("onchange") += "if(" & ChkTokubetuTaiouScript & "){" & tmpScript & "}else{" & setTateYoutoScript & "}"
            choSyouhin1.Attributes("onchange") = "if(" & ChkTokubetuTaiouScript & "){" & tmpScript & "}else{" & setSyouhinScript & "}"
            cboTyousaGaiyou.Attributes("onchange") = "if(" & ChkBuilderScript & ")" & "if(" & ChkTysGaiyouScript & ");" '& tmpScript
            iraiTousuu.Attributes("onblur") = "if(checkTempValueForOnBlur_DoujiIrai(this)){if(checkNumberAddFig(this)) if(" & ChkTysGaiyouScript & ")" & tmpScript & "}else{checkNumberAddFig(this);}"
            iraiTousuu.Attributes("onfocus") = "removeFig(this);setTempValueForOnBlur_DoujiIrai(this);"
            afterChouKaishaSet.Attributes("onclick") = tmpScript

        Else '新規受注時の場合
            '調査概要設定処理
            choSyouhin1.Attributes("onchange") = setTysGaiyouScript
            cboTyousaHouhou.Attributes("onchange") += setTysGaiyouScript
        End If

        '商品１請求有無プルダウン
        Dim tmpSeikyuUmuCtrl As HtmlSelect = FindControl("seikyuUmu_" & EarthConst.Instance.ARRAY_SHOUHIN_LINES(0))
        tmpSeikyuUmuCtrl.Attributes("onchange") = tmpScript

    End Sub

    ''' <summary>
    ''' 商品情報関連コントロールの表示設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub shouhinCtrlSetting()

        '商品３の表示を非表示化（デフォルト）
        irai2TBody3.Style("display") = "none"
        syouhin3Display.Value = "none"

        '商品郡の配列から、各商品行の設定を行う
        For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            '各行のコントロールを変数にセット
            ' 設定するコントロールのインスタンスを取得
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

            Dim search_shouhin23 As String = ln.Split("_")(0)   '商品2or3判定用

            '商品検索ボタンコントロールが存在する場合（商品２，３）
            If ctrlRec.ShouhinSearchBtn IsNot Nothing Then
                If iraiSession.Irai2Mode = EarthConst.MODE_EDIT And flgIraiGyoumuKengen = True Then
                    If iraiSession.IraiST <> EarthConst.MODE_NEW Or flgKakutei.Value = "1" Or search_shouhin23 = 3 Then
                        '商品検索ボタンのonclickイベントをセット（ターゲット商品行番号をテンポラリにセット -> callSearchSyouhinを実行）
                        Dim tmpStrTargetIds As String = ctrlRec.SyouhinCd.ClientID & EarthConst.SEP_STRING & shouhinSearchTargetLineNo.ClientID
                        Dim tmpReturnTargetIds As String = ctrlRec.SyouhinCd.ClientID & EarthConst.SEP_STRING & ctrlRec.BunruiCd.ClientID & EarthConst.SEP_STRING & ctrlRec.SyouhinNm.ClientID & _
                                                            EarthConst.SEP_STRING & ctrlRec.DispSyouhinNm.ClientID
                        Dim tmpAfterEventBtnId As String = afterSearchSyouhin23.ClientID
                        ctrlRec.ShouhinSearchBtn.Attributes("onclick") = "objEBI('" & shouhinSearchTargetLineNo.ClientID & "').value='" & ln & "';" & _
                                                                      "callSearchSyouhin('" & ln & "','" & tmpStrTargetIds & "','" & _
                                                                                        tmpReturnTargetIds & "','" & tmpAfterEventBtnId & "');"
                        ctrlRec.ShouhinSearchBtn.Attributes("onmouseup") = "JSsearchSyouhin23Type=9;"
                        ctrlRec.ShouhinSearchBtn.Attributes("onkeydown") = "if(event.keyCode==13||event.keyCode==32)JSsearchSyouhin23Type=9;"

                        '商品コードのonchangeイベントをセット
                        ctrlRec.SyouhinCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this))callSyouhinSearchOnChange(this,'" & ln & "');"
                        ctrlRec.SyouhinCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

                        ' 請求有無変更時のアクション
                        ctrlRec.SeikyuuUmu.Attributes("onchange") = "callSyouhin23SeikyuuUmuHenkou(this,'" & ln & "')"
                    Else
                        'modeEditの場合でも、新規登録で、依頼内容未確定の場合
                        If search_shouhin23 = 2 Then
                            '商品２の場合、商品検索ボタンの非表示化
                            ctrlRec.ShouhinSearchBtn.Visible = False
                        End If
                    End If
                Else
                    '商品検索ボタンの非表示化、商品コードの無い行を非表示化
                    ctrlRec.ShouhinSearchBtn.Visible = False
                    If ctrlRec.SyouhinCd.Value = "" Then
                        ctrlRec.SyouhinLine.Style("display") = "none"
                    Else
                        ctrlRec.SyouhinLine.Style("display") = "inline"
                    End If
                End If

                '商品３の表示切替チェック
                If search_shouhin23 = 3 Then
                    If ctrlRec.SyouhinCd.Value <> "" Then
                        '商品３に商品コードがセットされている場合、表示
                        irai2TBody3.Style("display") = "inline"
                        syouhin3Display.Value = "inline"
                    End If

                End If

            End If

            ' 発注書確定判断時の売上状況コントロールID
            Dim uriage_jyoukyou_id As String = ""

            Select Case search_shouhin23
                Case 1
                    uriage_jyoukyou_id = uriageJyoukyou1.ClientID
                Case 2
                    ' 商品２は商品１の状況
                    uriage_jyoukyou_id = uriageJyoukyou1.ClientID
                Case 3
                    ' 商品３は個別
                    uriage_jyoukyou_id = ctrlRec.UriageJyoukyou.ClientID
            End Select

            '**************************
            '金額項目のonblurイベント
            '**************************
            Dim onBlurScript As String
            '金額変更時のアクション
            Dim koumutenSeikyuuGakuOnblurScript As String = "callKingakuHenkouKoumuten(this,'" & ln & "');"
            Dim jituSeikyuuGakuOnblurScript As String = "callKingakuHenkouJituseikyu(this,'" & ln & "');"
            Dim syoudakusyoKingakuOnblurScript As String = "callKingakuHenkouSyoudakusyo(this,'" & ln & "');"
            Dim hattyuusyoKingakuOnblurScript As String = "callHattyuusyoKingakuHenkou(this,'" & ln & "'" & _
                                                               ",'" & ctrlRec.JituSeikyuuGaku.ClientID & _
                                                               "," & ctrlRec.HattyuusyoKakutei.ClientID & _
                                                               "," & ctrlRec.HidHattyuusyoKingakuOld.ClientID & _
                                                               "," & uriage_jyoukyou_id & _
                                                               "');"

            '商品2
            If search_shouhin23 = "2" Then
                onBlurScript = "if(checkTempValueForOnBlur(this)){if(checkKingaku(this)){if(checkPlusKingaku(" & ctrlRec.BunruiCd.ClientID & ",this)){@KINGAKU_SCRIPT@}}}else{checkKingaku(this);}"
            Else '商品1,3
                onBlurScript = "if(checkTempValueForOnBlur(this)){if(checkKingaku(this)){@KINGAKU_SCRIPT@}}else{checkKingaku(this);}"
            End If

            '金額変更時のScriptを付与
            koumutenSeikyuuGakuOnblurScript = onBlurScript.Replace("@KINGAKU_SCRIPT@", koumutenSeikyuuGakuOnblurScript)
            jituSeikyuuGakuOnblurScript = onBlurScript.Replace("@KINGAKU_SCRIPT@", jituSeikyuuGakuOnblurScript)
            syoudakusyoKingakuOnblurScript = onBlurScript.Replace("@KINGAKU_SCRIPT@", syoudakusyoKingakuOnblurScript)
            hattyuusyoKingakuOnblurScript = onBlurScript.Replace("@KINGAKU_SCRIPT@", hattyuusyoKingakuOnblurScript)

            'onblurイベントに付与
            ctrlRec.KoumutenSeikyuuGaku.Attributes("onblur") = koumutenSeikyuuGakuOnblurScript
            ctrlRec.JituSeikyuuGaku.Attributes("onblur") = jituSeikyuuGakuOnblurScript
            ctrlRec.SyoudakusyoKingaku.Attributes("onblur") = syoudakusyoKingakuOnblurScript
            ctrlRec.HattyuusyoKingaku.Attributes("onblur") = hattyuusyoKingakuOnblurScript


            '発注書確定コンボ変更時のアクション
            ctrlRec.HattyuusyoKakutei.Attributes("onchange") = "callHattyuusyoKakuteiHenkou(this,'" & ln & "'" & _
                                                               ",'" & ctrlRec.JituSeikyuuGaku.ClientID & _
                                                               "," & ctrlRec.HattyuusyoKingaku.ClientID & _
                                                               "')"

            If search_shouhin23 = "3" Then
                '商品３確定コンボ変更時のアクション
                ctrlRec.KakuteiKbn.Attributes("onchange") = "callSyouhin3KakuteiHenkou(this,'" & ln & "')"
            End If

            '桁区切り除去処理を付与
            Dim onfocusScript As String = "removeFig(this);setTempValueForOnBlur(this);"
            ctrlRec.KoumutenSeikyuuGaku.Attributes("onfocus") = onfocusScript
            ctrlRec.JituSeikyuuGaku.Attributes("onfocus") = onfocusScript
            ctrlRec.SyoudakusyoKingaku.Attributes("onfocus") = onfocusScript
            ctrlRec.HattyuusyoKingaku.Attributes("onfocus") = onfocusScript
            'MaxLength設定
            ctrlRec.KoumutenSeikyuuGaku.MaxLength = 7
            ctrlRec.JituSeikyuuGaku.MaxLength = 7
            ctrlRec.SyoudakusyoKingaku.MaxLength = 7
            ctrlRec.HattyuusyoKingaku.MaxLength = 7

            '商品名表示ラベルに値をセット
            ctrlRec.DispSyouhinNm.InnerText = ctrlRec.SyouhinNm.Value

        Next
    End Sub

    ''' <summary>
    ''' 【依頼内容】編集ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_irai2_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_irai2.ServerClick

        '処理ステータスをコンテキストで渡す
        Dim stVal As String = iraiSession.IraiST
        If iraiSession.IraiST = EarthConst.MODE_NEW Or iraiSession.IraiST = EarthConst.MODE_NEW_EDIT Then
            stVal = EarthConst.MODE_NEW_EDIT
        End If

        iraiSession.IraiST = IIf(stVal Is Nothing, String.Empty, stVal)
        Context.Items("irai") = iraiSession
        Server.Transfer(UrlConst.IRAI_STEP_2)

    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '確認モードオフ
        Me.HiddenModeKakunin.Value = "0"

        If kameitenSearchType.Value <> "1" Then
            kameitenSearchSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            kameitenSearchSub(sender, e, False)
            kameitenSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="callWindow">検索ポップアップを起動するか否かの指定</param>
    ''' <remarks></remarks>
    Private Sub kameitenSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        '加盟店検索フラグ初期化
        kameitenSearchFlg.Value = "0"

        ' 入力されているコードをキーに、マスタを検索し、データを1件のみ取得できた場合は
        ' 画面情報を更新する。データが無い場合は、検索画面を表示する
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim blnTorikesi As Boolean = (iraiSession.IraiST = EarthConst.MODE_NEW Or flgNotGetSyouhin = False)
        Dim total_count As Integer

        ' 取得件数を絞り込む場合、引数を追加してください
        If kameitenCd.Value <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(kubunVal.Value, _
                                                                    kameitenCd.Value, _
                                                                    blnTorikesi, _
                                                                    total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As KameitenSearchRecord = dataArray(0)
            kameitenCd.Value = recData.KameitenCd

            '●ビルダー注意事項チェック
            If kameitenSearchLogic.ChkBuilderData13(kameitenCd.Value) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If

            '●調査方法SDS自動設定チェック
            '画面.商品1が入力済み以外の場合、以降の処理は行わない。
            If (Me.HiddenModeKakunin.Value <> "1") AndAlso (Me.kameitenCd.Value <> "") AndAlso (Me.choSyouhin1.SelectedValue <> "") Then
                blnRet = cbLogic.ChkTysJidouSet(Me.choSyouhin1.SelectedValue, Me.kameitenCd.Value, strTysGaisyaCd)
            End If

            '発注停止チェック
            If flgNotGetSyouhin = False Then
                ComLog.chkOrderStopFlg(sender, dataArray(0).OrderStopFLG, Me.kameitenCd.Value, Me.saveCdOrderStop.Value)
            End If

            'フォーカスセット
            setFocus(kameitenSearch)
        Else
            '●ビルダー注意事項フラグ初期化
            Me.HiddenKameitenTyuuiJikou.Value = String.Empty

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & kameitenSearch.ClientID & "').focus();"
            Dim tmpScript As String = "callSearch('" & kubunVal.ClientID & EarthConst.SEP_STRING & kameitenCd.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            kameitenCd.ClientID & EarthConst.SEP_STRING & kameitenNm.ClientID & "','" & kameitenSearch.ClientID & "');"
            If callWindow Then
                tmpScript = tmpFocusScript & tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            ElseIf kameitenSearchType.Value = "1" Then
                tmpScript = tmpFocusScript & "if(JSkameitenSearchType==9)" & tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            End If
        End If

        Dim tmpScript2 = "JSkameitenSearchType=0;"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "resetSearchType", tmpScript2, True)

        ' 加盟店検索実行後処理実行
        kameitenSearchAfter_ServerClick(sender, e)

    End Sub

    ''' <summary>
    ''' 加盟店検索実行後処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchAfter_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '加盟店検索実行後処理(加盟店詳細情報、ビルダー情報取得)
        Dim logic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = (iraiSession.IraiST = EarthConst.MODE_NEW Or flgNotGetSyouhin = False)
        Dim record As KameitenSearchRecord = logic.GetKameitenSearchResult(kubunVal.Value, kameitenCd.Value, tyousakaisyaCd.Value, blnTorikesi)
        Dim strErrMsg As String = String.Empty
        Dim kameitenSearchLogic As New KameitenSearchLogic

        '商品情報再取得の「対象外以外」の場合、商品１情報取得チェック
        If flgNotGetSyouhin = False Then
            ' データ取得用パラメータクラス
            Dim hin1InfoRec As New Syouhin1InfoRecord
            ' 取得レコード格納クラス
            Dim hin1SetRec As New Syouhin1AutoSetRecord

            ' 商品１情報を取得し、該当のデータが有るかをチェック
            If record.KameitenCd <> Nothing AndAlso Syouhin1Get(sender, hin1InfoRec, hin1SetRec, record) Then
                '商品１が取得できるのでOK
            Else
                '商品１が取得できない場合
                If hattyuuKingaku_1_1.Value <> "0" And hattyuuKingaku_1_1.Value <> String.Empty Then
                    '発注書金額が設定済みの場合、クリアしないメッセージを表示
                    MLogic.AlertMessage(sender, Messages.MSG010E, 0, "hin1SetError")
                    '商品１が取得できなかった場合、加盟店コードを戻して、処理終了(関連情報を更新する前に終了する)
                    kameitenCd.Value = kameitenCdOld.Value
                    Exit Sub
                End If

            End If

            '●ビルダー注意事項判定
            strErrMsg = String.Empty
            If cbLogic.ChkErrBuilderData(Me.cboTyousaGaiyou.SelectedValue, Me.kameitenCd.Value, Me.HiddenKameitenTyuuiJikou.Value, strErrMsg) = False Then
                '商品１が取得できていても、ビルダー注意事項チェックでNGの場合、処理終了(関連情報を更新する前に終了する)
                MLogic.AlertMessage(sender, strErrMsg, 0, "hin1SetError2")
                kameitenCd.Value = kameitenCdOld.Value
                '●ビルダー注意事項チェック
                If kameitenSearchLogic.ChkBuilderData13(kameitenCd.Value) Then
                    Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
                Else
                    Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
                End If
                'フォーカスセット
                setFocus(Me.kameitenCd)
                Exit Sub
            End If
        End If

        '画面に値をセット
        kameitenNm.Value = ComLog.GetDisplayString(record.KameitenMei1)
        kameitenTel.Value = ComLog.GetDisplayString(record.TelNo)
        kameitenFax.Value = ComLog.GetDisplayString(record.FaxNo)
        kameitenMail.Value = ComLog.GetDisplayString(record.MailAddress)
        builderNo.Value = ComLog.GetDisplayString(record.BuilderNo)
        builderCheck.Disabled = IIf(record.BuilderCount > 0, False, True)
        keiretuNm.Value = ComLog.GetDisplayString(record.KeiretuMei)
        kameitenJuusyo.Value = ComLog.GetDisplayString(record.Jyuusyo)
        EigyousyoCd.Value = ComLog.GetDisplayString(record.EigyousyoCd)
        EigyousyoMei.Value = ComLog.GetDisplayString(record.EigyousyoMei)
        keiretuCd.Value = ComLog.GetDisplayString(record.KeiretuCd)
        TysSeikyuuSaki.Value = ComLog.GetDisplayString(record.TysSeikyuuSaki)
        mitsumoriHitsuyou.Value = ComLog.GetDisplayString(record.TysMitsyoMsg)
        hacchuushoHituyou.Value = ComLog.GetDisplayString(record.HattyuusyoMsg)
        kakushuNG.Value = IIf(record.KahiKbn = 9, EarthConst.TYOUSA_KAISYA_NG, "")
        TextJioSakiFlg.Value = ComLog.GetDisplayString(IIf(record.JioSakiFLG = 1, EarthConst.JIO_SAKI, String.Empty))

        '調査方法SDS自動設定
        If blnRet = True AndAlso strTysGaisyaCd <> String.Empty Then
            '調査方法
            chousaHouhouCode.Value = ComLog.GetDisplayString(EarthConst.TYOUSA_HOUHOU_CD_15)
            cboTyousaHouhou.SelectedValue = ComLog.GetDisplayString(EarthConst.TYOUSA_HOUHOU_CD_15)
            '調査会社
            tyousakaisyaCd.Value = ComLog.GetDisplayString(strTysGaisyaCd)
            flgNotGetSyouhin = True '商品取得を行わないためのフラグを立てる
            tyousakaisyaSearchSub(sender, e, False)
            flgNotGetSyouhin = False '商品取得を行わないためのフラグを切る
            If tyousakaisyaCd.Value <> tyousakaisyaCdOld.Value Then   '調査会社Oldに調査会社をセットする
                tyousakaisyaCdOld.Value = tyousakaisyaCd.Value
                tyousakaisyaNmOld.Value = tyousakaisyaNm.Value
            End If
            '調査概要
            btnSetTysGaiyou_ServerClick(sender, e)
        End If

        '加盟店取消理由表示・色変処理
        Dim objChgColor As Object() = New Object() {Me.kameitenCd, Me.kameitenNm, Me.TextTorikesiRiyuu}
        ComLog.GetKameitenTorikesiRiyuu(kubunVal.Value, kameitenCd.Value, TextTorikesiRiyuu, True, blnTorikesi, objChgColor)

        '加盟店コードを退避(発注停止処理の場合は再検索)
        Me.saveCdOrderStop.Value = ComLog.GetDisplayString(record.KameitenCd)

        If record.KojTantouFlg Then
            kojTantoSpan.Visible = True
        Else
            kojTantoSpan.Visible = False
            koujiTantouNm.Value = String.Empty
        End If

        If flgNotGetSyouhin = False Then
            ' 保証書発行有無は０の場合無し、以外１（地盤仕様）
            cboHosyouUmu.Value = IIf(ComLog.GetDisplayString(record.HosyousyoHakUmu) = "1", "1", "")
            HosyouKikan.Value = ComLog.GetDisplayString(record.HosyouKikan)
            ' 工事会社請求有無設定
            KojGaisyaSeikyuuUmu.Value = IIf(ComLog.GetDisplayString(record.KojGaisyaSeikyuuUmu) = "1", "1", "")

            '商品１設定処理実行
            actCtrlId.Value = kameitenSearch.ClientID
            btnSetSyouhin1_ServerClick(sender, e)
            If kameitenNm.Value <> String.Empty Then
                kameitenCdOld.Value = kameitenCd.Value
            End If
        ElseIf iraiSession.IraiST = EarthConst.MODE_NEW And flgKakutei.Value = "0" Then
            kameitenCdOld.Value = kameitenCd.Value
        End If

    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tyousakaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If tyousakaisyaSearchType.Value <> "1" Then
            tyousakaisyaSearchSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            tyousakaisyaSearchSub(sender, e, False)
            tyousakaisyaSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tyousakaisyaSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = (iraiSession.IraiST = EarthConst.MODE_NEW Or flgNotGetSyouhin = False)
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        If tyousakaisyaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(tyousakaisyaCd.Value, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            kameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            tyousakaisyaCd.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            tyousakaisyaNm.Value = recData.TysKaisyaMei
            ' 調査会社NG設定
            If recData.KahiKbn = 9 Then
                kakushuNG.Value = EarthConst.TYOUSA_KAISYA_NG
                tyousakaisyaCd.Style("color") = "red"
                tyousakaisyaNm.Style("color") = "red"
            Else
                kakushuNG.Value = String.Empty
                tyousakaisyaCd.Style("color") = "blue"
                tyousakaisyaNm.Style("color") = "blue"
            End If

            'フォーカスセット
            setFocus(tyousakaisyaSearch)
        Else
            '表示色を初期化
            tyousakaisyaCd.Style.Remove("color")
            tyousakaisyaNm.Style.Remove("color")

            '調査会社名をクリア
            tyousakaisyaNm.Value = String.Empty

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & tyousakaisyaSearch.ClientID & "').focus();"
            Dim tmpScript = "callSearch('" & tyousakaisyaCd.ClientID & EarthConst.SEP_STRING & kameitenCd.ClientID & "','" & UrlConst.SEARCH_TYOUSAKAISYA & "','" & _
                            tyousakaisyaCd.ClientID & EarthConst.SEP_STRING & _
                            tyousakaisyaNm.ClientID & EarthConst.SEP_STRING & _
                            kakushuNG.ClientID & "','" & tyousakaisyaSearch.ClientID & "');"
            If callWindow Then
                tmpScript = tmpFocusScript & tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            ElseIf tyousakaisyaSearchType.Value = "1" Then
                tmpScript = tmpFocusScript & "if(JStyousakaisyaSearchType==9)" & tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            End If
        End If

        Dim tmpScript2 = "JStyousakaisyaSearchType=0;"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "resetSearchType", tmpScript2, True)

        If flgNotGetSyouhin = False Then
            ' 調査会社検索実行後処理実行（商品１設定処理）
            actCtrlId.Value = tyousakaisyaSearch.ClientID
            btnSetSyouhin1_ServerClick(sender, e)
            If tyousakaisyaNm.Value <> String.Empty Then
                tyousakaisyaCdOld.Value = tyousakaisyaCd.Value
                tyousakaisyaNmOld.Value = tyousakaisyaNm.Value
            End If
        ElseIf iraiSession.IraiST = EarthConst.MODE_NEW And flgKakutei.Value = "0" Then
            tyousakaisyaCdOld.Value = tyousakaisyaCd.Value
            tyousakaisyaNmOld.Value = tyousakaisyaNm.Value
        End If

    End Sub

    ''' <summary>
    ''' 商品検索ボタン押下時の処理（商品２，３）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearchSyouhin23_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If searchSyouhin23Type.Value <> "1" Then
            searchSyouhin23Sub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            searchSyouhin23Sub(sender, e, False)
            searchSyouhin23Type.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 商品検索ボタン押下時の処理（商品２，３）(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub searchSyouhin23Sub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        Dim ln As String = targetLine_SearchSyouhin23.Value '商品行番号
        Dim search_shouhin23 As String = ln.Split("_")(0)   '商品2or3

        ' 設定するコントロールのインスタンスを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim SyouhinSearchLogic As New SyouhinSearchLogic
        Dim total_row As Integer

        Dim TyousaHouhouNo As Integer = Integer.MinValue
        ComLog.SetDisplayString(cboTyousaHouhou.SelectedValue, TyousaHouhouNo)          ' 調査方法

        '商品２，３検索を実行
        Dim dataArray As List(Of Syouhin23Record) = SyouhinSearchLogic.GetSyouhinInfo(ctrlRec.SyouhinCd.Value, _
                                                                    "", _
                                                                    (search_shouhin23 = "2"), _
                                                                     total_row, _
                                                                     TyousaHouhouNo)

        If dataArray.Count = 1 Then
            '商品情報を画面にセット
            Dim recData As Syouhin23Record = dataArray(0)

            'フォーカスセット
            setFocus(ctrlRec.ShouhinSearchBtn)
        Else
            '検索ポップアップを起動

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & ctrlRec.ShouhinSearchBtn.ClientID & "').focus();"
            Dim tmpScript = "callSearch('" & strTargetIds_SearchSyouhin23.Value & "','" & UrlConst.SEARCH_SYOUHIN & "','" & _
                                            returnTargetIds_SearchSyouhin23.Value & "','" & _
                                            afterEventBtnId_SearchSyouhin23.Value & "');"
            If callWindow Then
                tmpScript = tmpFocusScript & tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
                Exit Sub
            ElseIf searchSyouhin23Type.Value = "1" Then
                tmpScript = tmpFocusScript & "if(JSsearchSyouhin23Type==9)" & tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            End If
        End If

        Dim tmpScript2 = "JSsearchSyouhin23Type=0;"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "resetSearchType", tmpScript2, True)

        '商品２，３設定後処理
        afterSearchSyouhin23_ServerClick(sender, e)

    End Sub

    ''' <summary>
    ''' 商品検索後処理（商品２，３）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub afterSearchSyouhin23_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim ln As String = targetLine_SearchSyouhin23.Value '商品行番号

        ' 設定するコントロールのインスタンスを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)
        Dim syouhin_type As String = targetLine_SearchSyouhin23.Value

        ' 情報取得用のパラメータクラス
        Dim syouhin23_info As New Syouhin23InfoRecord
        ' 商品の基本情報を取得
        Dim syouhin23_rec As Syouhin23Record = getSyouhinInfo(syouhin_type.Split("_")(0), ctrlRec.SyouhinCd.Value)

        ' 取得できない場合、エラー
        If syouhin23_rec Is Nothing OrElse ctrlRec.SyouhinCd.Value = String.Empty Then
            If ctrlRec.HattyuusyoKingaku.Value <> "0" And ctrlRec.HattyuusyoKingaku.Value <> String.Empty Then
                '発注書金額が設定済みの場合、クリアしない
                MLogic.AlertMessage(sender, Messages.MSG010E, 0, "hinSetError")
                ctrlRec.SyouhinCd.Value = ctrlRec.SyouhinCdOld.Value
                Exit Sub
            End If
        End If

        '商品コード入力コントロールの背景色を初期化
        ctrlRec.SyouhinCd.Style("background-color") = ""
        '商品名表示コントロールに値をセット
        ctrlRec.DispSyouhinNm.InnerHtml = ctrlRec.SyouhinNm.Value

        '商品２，３設定処理実行
        setSyouhinCd23(sender, syouhin_type, ctrlRec.SyouhinCd.Value, syouhin23_rec)

        'old値更新
        ctrlRec.SyouhinCdOld.Value = ctrlRec.SyouhinCd.Value

        '商品行の活性制御
        subEnableSeikyuu23(ln)

        '残額を設定
        setZangaku()

    End Sub

    ''' <summary>
    ''' 商品１設定ボタン(非表示)押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSetSyouhin1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSyouhin1.ServerClick

        'エラーMSG
        Dim strErrMsg As String = String.Empty

        '編集モード以外の場合、処理を抜ける
        If iraiSession.Irai2Mode <> EarthConst.MODE_EDIT Or _
           (iraiSession.IraiST = EarthConst.MODE_NEW And flgKakutei.Value = "0") Or _
           uriageKeijyouFlg_1_1.Value = "1" Then
            Exit Sub
        End If

        '実行元のクライアントコントロール
        Dim strActCtrlId As String = actCtrlId.Value.Replace(ClientID & ClientIDSeparator.ToString, "")
        Dim actCtrl As Control = FindControl(strActCtrlId)

        Select Case actCtrl.ID

            Case itemKb_1.ID, itemKb_2.ID, itemKb_3.ID, itemKb_9.ID
                '商品区分から呼ばれた場合
                If shouhinCd_1_1.Value = "" Then
                    '商品コード１の設定実行
                    Syouhin1Set(sender, actCtrl)
                    '商品１承諾書金額の設定
                    Syouhin1SyoudakuGakuSet(sender, actCtrl)
                Else
                    '商品コード１の設定実行
                    Syouhin1Set(sender, actCtrl)
                End If
                'フォーカスセット
                If actCtrl.ID = itemKb_2.ID Then
                    setFocus(itemKb_2)
                ElseIf actCtrl.ID = itemKb_3.ID Then
                    setFocus(itemKb_3)
                Else
                    setFocus(iraiTousuu)
                End If

            Case cboTyousaHouhou.ID
                '調査方法から呼ばれた場合
                '商品コード１の設定実行
                Syouhin1Set(sender, actCtrl)
                '商品１承諾書金額の設定
                Syouhin1SyoudakuGakuSet(sender, actCtrl)
                'フォーカスセット
                setFocus(actCtrl)

                'Case cboTyousaGaiyou.ID
                '    '調査概要から呼ばれた場合
                '    '商品コード１の設定実行
                '    Syouhin1Set(sender, actCtrl)
                '    '商品１承諾書金額の設定
                '    Syouhin1SyoudakuGakuSet(sender, actCtrl)
                '    'フォーカスセット
                '    setFocus(actCtrl)

            Case cboTatemonoYouto.ID
                '建物用途から呼ばれた場合
                '商品コード１の設定実行
                Syouhin1Set(sender, actCtrl)
                '商品１承諾書金額の設定
                Syouhin1SyoudakuGakuSet(sender, actCtrl)
                'フォーカスセット
                setFocus(actCtrl)

            Case afterChouKaishaSet.ID, tyousakaisyaSearch.ID
                '調査会社から呼ばれた場合
                '商品１承諾書金額の設定
                Syouhin1SyoudakuGakuSet(sender, actCtrl)
                '商品１販売価格の設定
                Syouhin1HanbaiKakakuSet(sender, actCtrl)
                '請求書発行日、売上年月日の設定実行
                Syouhin1UriageSeikyuDateSet(sender, actCtrl, False)

            Case iraiTousuu.ID

                '同時依頼棟数から呼ばれた場合
                '商品１承諾書金額の設定
                Syouhin1SyoudakuGakuSet(sender, actCtrl)
                '商品ｺｰﾄﾞ2[多棟数割引]の自動設定
                TatouwariSet(sender)
                'フォーカスセット
                setFocus(actCtrl)

            Case seikyuUmu_1_1.ID
                '商品１請求有無から呼ばれた場合

                If seikyuUmu_1_1.Value = "1" Then
                    '請求有りの場合
                    '商品コード１の設定実行
                    Syouhin1Set(sender, actCtrl)
                Else
                    '請求無しの場合
                    ' 工務店請求額
                    koumutenSeikyuZeinukiGaku_1_1.Value = "0"
                    Me.HiddenKoumutenGakuHenkouKahi_1_1.Value = EarthConst.HENKOU_FUKA
                    ' 実請求額
                    jituSeikyuZeinukiGaku_1_1.Value = "0"
                    Me.HiddenJituGakuHenkouKahi_1_1.Value = EarthConst.HENKOU_FUKA
                    shouhiZei_1_1.Value = "0"                   ' 消費税額
                    zeikomiGaku_1_1.Value = "0"                 ' 税込金額
                    seikyuuHakkouDate_1_1.Value = ""            ' 請求書発行日
                End If

                '請求書発行日、売上年月日の設定実行
                Syouhin1UriageSeikyuDateSet(sender, actCtrl, True)
                'フォーカスセット
                setFocus(actCtrl)

            Case kameitenSearch.ID, kameitenSearchAfter.ID
                '商品コード１の設定実行
                Syouhin1Set(sender, actCtrl)
                '特定加盟店の商品ｺｰﾄﾞ2自動設定
                TokuteitenSyouhin2Set(sender)
                '商品ｺｰﾄﾞ2[多棟数割引]の自動設定
                TatouwariSet(sender)
                '商品１承諾書金額の設定
                Syouhin1SyoudakuGakuSet(sender, actCtrl)

            Case btnIrainaiyouKakutei.ID
                '依頼内容確定ボタンから呼ばれた場合

                Syouhin1Set(sender, actCtrl)
                Syouhin1SyoudakuGakuSet(sender, actCtrl)
                Syouhin1UriageSeikyuDateSet(sender, actCtrl, False)
                '特定加盟店の商品ｺｰﾄﾞ2自動設定
                TokuteitenSyouhin2Set(sender)
                '商品ｺｰﾄﾞ2[多棟数割引]の自動設定
                TatouwariSet(sender)

            Case choSyouhin1.ID
                '商品1プルダウンから呼ばれた場合

                'SDS自動設定用
                If kameitenSearchType.Value <> "1" Then
                    kameitenSearchSub(sender, e)
                Else
                    'コードonchangeで呼ばれた場合
                    kameitenSearchSub(sender, e, False)
                    kameitenSearchType.Value = String.Empty
                End If

                '商品コード１の設定実行
                Syouhin1Set(sender, actCtrl)
                '商品１承諾書金額の設定
                Syouhin1SyoudakuGakuSet(sender, actCtrl)
                'フォーカスセット
                setFocus(actCtrl)

            Case Else

        End Select

        ' 商品1,2の活性制御
        For Each shn As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            If shn.Split("_")(0) = 1 Then
                subEnableSeikyuu1()
            ElseIf shn.Split("_")(0) = 2 Then
                subEnableSeikyuu23(shn)
            End If
        Next

        '残額を設定
        setZangaku()

        '呼び出し元ID保持Hiddenをクリア
        actCtrlId.Value = String.Empty

    End Sub

    ''' <summary>
    ''' 調査概要設定ボタン(非表示)押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSetTysGaiyou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTysGaiyou.ServerClick

        Dim lgcJiban As New JibanLogic
        '設定・取得用 商品価格設定レコード
        Dim recKakakuSettei As New KakakuSetteiRecord

        '実行元のクライアントコントロール
        Dim strActCtrlId As String = actCtrlIdTg.Value.Replace(ClientID & ClientIDSeparator.ToString, "")
        Dim actCtrl As Control = FindControl(strActCtrlId)

        'SDS自動設定用処理
        '商品1プルダウンから呼ばれた場合、加盟店検索処理を呼び出す
        If kameitenSearchFlg.Value = "1" Then
            If kameitenSearchType.Value <> "1" Then
                kameitenSearchSub(sender, e)
            Else
                'コードonchangeで呼ばれた場合
                kameitenSearchSub(sender, e, False)
                kameitenSearchType.Value = String.Empty
            End If
        End If

        ' 商品区分
        If itemKb_1.Checked = True Then
            ' 60年保証(EARTHでは非表示)
            recKakakuSettei.SyouhinKbn = 1
        ElseIf itemKb_2.Checked = True Then
            ' 土地販売
            recKakakuSettei.SyouhinKbn = 2
        ElseIf itemKb_3.Checked = True Then
            ' リフォーム
            recKakakuSettei.SyouhinKbn = 3
        Else
            ' 未設定は商品区分 9 
            recKakakuSettei.SyouhinKbn = 9
        End If
        ' 商品コード
        ComLog.SetDisplayString(Me.choSyouhin1.SelectedValue, recKakakuSettei.SyouhinCd)
        ' 調査方法
        ComLog.SetDisplayString(Me.cboTyousaHouhou.SelectedValue, recKakakuSettei.TyousaHouhouNo)

        '商品価格設定マスタから値の取得
        lgcJiban.GetTysGaiyou(recKakakuSettei)

        '調査概要の設定
        Me.cboTyousaGaiyou.SelectedValue = ComLog.GetDispNum(recKakakuSettei.TyousaGaiyou)

        'フォーカスセット
        If actCtrlIdTg.Value <> String.Empty Then
            setFocus(actCtrl)
        End If

    End Sub

    ''' <summary>
    '''商品毎の請求・仕入先が変更されていないかをチェックし、
    '''変更されている場合情報の再取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)

        '請求先、仕入先が変更された行をチェックし、存在した場合は
        '各行の請求有無変更時の処理を実行する
        For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)
            '各行の請求仕入変更リンクUC内のチェックフラグHiddenを参照し、フラグが立っている場合は処理実施
            If ctrlRec.SeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
                If ln.Split("_")(0) = 1 Then
                    '商品１用
                    actCtrlId.Value = seikyuUmu_1_1.ClientID
                    btnSetSyouhin1_ServerClick(sender, e)
                Else
                    '商品１以外用
                    syouhin23SeikyuuUmuHenkouLineNo.Value = ln
                    syouhin23SeikyuuUmuHenkou_ServerClick(sender, e)
                End If
                'フラグ初期化
                ctrlRec.SeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
                'フォーカスは請求仕入変更リンク
                setFocus(ctrlRec.SeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)
                '変更された商品が有った場合、UpdatePanelをUpdateし、ループ終了(原則として、1商品毎しか変更されないため)
                updPanelSyouhin.Update()
                Exit For
            End If
        Next

    End Sub

    ''' <summary>
    ''' 残額を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setZangaku()

        '*************************************************************
        ' 商品１．残額設定
        '*************************************************************
        Dim zangaku As Integer = 0
        ' 加算対象
        Dim taisyou() As String = {"1_1", "2_1", "2_2", "2_3", "2_4"}

        For Each ln As String In taisyou

            Dim ctrl As HtmlInputText = FindControl("zeikomiGaku_" & ln)
            Dim add_zeikomi As Integer = 0

            ComLog.SetDisplayString(ctrl.Value, add_zeikomi)

            ' 数値の場合、税込金額に加算する
            If Not add_zeikomi = Integer.MinValue Then
                zangaku = zangaku + add_zeikomi
            End If

        Next

        ' 入金額を減算
        Dim add_nyuukingaku As Integer = 0
        ComLog.SetDisplayString(nyuukinGaku_2.Value, add_nyuukingaku)
        ' 数値の場合入金額に加算する
        If Not add_nyuukingaku = Integer.MinValue Then
            zangaku = zangaku - add_nyuukingaku
        End If

        ' 解約払戻金額を加算
        Dim add_kaiyaku As Integer = 0
        ComLog.SetDisplayString(kaiyakuHaraimodosi.Value, add_kaiyaku)
        ' 数値の場合解約払戻金額を減算する
        If Not add_kaiyaku = Integer.MinValue Then
            zangaku = zangaku + add_kaiyaku
        End If

        ' 残額にセットする
        If Not zangaku = Integer.MinValue Then
            nyuukinZanGaku_1.Value = zangaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            nyuukinZanGaku_2.Value = zangaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            nyuukinZanGaku_1.Value = ""
            nyuukinZanGaku_2.Value = ""
        End If

        '*************************************************************
        ' 商品３．残額設定
        '*************************************************************
        Dim zangaku_3 As Integer = 0
        ' 加算対象
        Dim taisyou_3() As String = {"3_1", "3_2", "3_3", "3_4", "3_5", "3_6", "3_7", "3_8", "3_9"}

        For Each ln As String In taisyou_3

            Dim ctrl As HtmlInputText = FindControl("zeikomiGaku_" & ln)
            Dim add_zeikomi As Integer = 0

            ComLog.SetDisplayString(ctrl.Value, add_zeikomi)

            ' 数値の場合、税込金額に加算する
            If Not add_zeikomi = Integer.MinValue Then
                zangaku_3 = zangaku_3 + add_zeikomi
            End If

        Next

        ' 入金額を減算
        Dim add_nyuukingaku3 As Integer = 0

        ComLog.SetDisplayString(nyuukinGaku_3.Value, add_nyuukingaku3)
        ' 数値の場合入金額を減算する
        If Not add_nyuukingaku3 = Integer.MinValue Then
            zangaku_3 = zangaku_3 - add_nyuukingaku3
        End If

        ' 残額にセットする
        If Not zangaku_3 = Integer.MinValue Then
            nyuukinZanGaku_3.Value = zangaku_3.ToString(EarthConst.FORMAT_KINGAKU_1)
            nyuukinZanGaku_4.Value = zangaku_3.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            nyuukinZanGaku_3.Value = ""
            nyuukinZanGaku_4.Value = ""
        End If
    End Sub

    ''' <summary>
    ''' 商品コード１の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="actCtrl">処理実行元コントロール</param>
    ''' <remarks></remarks>
    Private Sub Syouhin1Set(ByVal sender As System.Object, ByVal actCtrl As Control)
        ' データ取得用ロジッククラス
        Dim logic As New JibanLogic
        ' データ取得用パラメータクラス
        Dim param_rec As New Syouhin1InfoRecord
        ' 取得レコード格納クラス
        Dim syouhin_rec As New Syouhin1AutoSetRecord
        'エラーメッセージ
        Dim strErrMsg As String = String.Empty
        '商品1取得ステータス
        Dim blnSyouhin1Get As Boolean

        blnSyouhin1Get = Syouhin1Get(sender, param_rec, syouhin_rec, Nothing, intSetSts, actCtrl)

        '依頼内容確定時はマスタ取得不可＝確定不可
        If actCtrl.ClientID = Me.btnIrainaiyouKakutei.ClientID And _
           intSetSts > 0 Then
            '商品1クリア
            Dim tmpUpdDatetime As String = HiddenUpdDatetime_1_1.Value
            clrSyouhinLine(EarthConst.Instance.ARRAY_SHOUHIN_LINES(0))
            Exit Sub
        End If

        ' 商品１情報を取得し画面にセット
        If blnSyouhin1Get Then

            '原価マスタのみに無い場合 MSG表示
            If intSetSts = EarthEnum.emSyouhin1Error.GetGenka Then
                strErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)
            End If
            '販売価格マスタのみに無い場合 MSG表示
            If intSetSts = EarthEnum.emSyouhin1Error.GetHanbai Then
                strErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)
            End If
            '原価と販売価格マスタにない場合 両MSGを表示
            If intSetSts = EarthEnum.emSyouhin1Error.GetGenkaHanbai Then
                strErrMsg += cbLogic.GetSyouhin1ErrMsg(EarthEnum.emSyouhin1Error.GetGenka)
                strErrMsg += cbLogic.GetSyouhin1ErrMsg(EarthEnum.emSyouhin1Error.GetHanbai)
            End If
            If strErrMsg <> "" Then
                MLogic.AlertMessage(sender, strErrMsg, 0, "GetSyouhinKakakuError")
            End If

            ' 画面にセットする
            bunruiCd_1_1.Value = "100"                          ' 分類コード（100固定）
            shouhinCd_1_1.Value = syouhin_rec.SyouhinCd         ' 商品コード
            shouhinCdOld_1_1.Value = syouhin_rec.SyouhinCd      ' 商品コード
            shouhinNm_1_1.InnerText = syouhin_rec.SyouhinNm     ' 商品名表示ラベル
            shouhinNmText_1_1.Value = syouhin_rec.SyouhinNm     ' 商品名テキストボックス（非表示）

            ' 請求有無が未選択の場合、"1"をセット
            If seikyuUmu_1_1.Value = "" Then
                seikyuUmu_1_1.Value = "1"
            End If

            ' 発注書確定FLG
            If hattyuuKakutei_1_1.Value = "" Then
                hattyuuKakutei_1_1.Value = "0"
            End If

            ' 発注書確定FLG Old
            If hattyuuKakuteiOld_1_1.Value <> "1" Then
                hattyuuKakuteiOld_1_1.Value = "0"
            End If

            ' 税区分
            zeikubun_1_1.Value = syouhin_rec.ZeiKbn

            '***************************************************************************
            '* 販売価格(工務店請求金額・実請求金額)の設定
            '***************************************************************************
            If intSetSts = EarthEnum.emSyouhin1Error.GetHanbai Or intSetSts = EarthEnum.emSyouhin1Error.GetGenkaHanbai Then
                '●販売価格マスタ取得不可は、金額設定なし・変更可
                If seikyuUmu_1_1.Value = "1" Then
                    '請求ありは変更可
                    Me.HiddenJituGakuHenkouKahi_1_1.Value = String.Empty
                    Me.HiddenKoumutenGakuHenkouKahi_1_1.Value = String.Empty
                Else
                    '請求なしは変更不可
                    Me.HiddenJituGakuHenkouKahi_1_1.Value = EarthConst.HENKOU_FUKA
                    Me.HiddenKoumutenGakuHenkouKahi_1_1.Value = EarthConst.HENKOU_FUKA
                End If
            Else
                '●販売価格マスタからの取得可能の場合は設定
                ' 請求ありの場合、金額をセット
                If seikyuUmu_1_1.Value = "1" Then
                    '工務店請求額
                    koumutenSeikyuZeinukiGaku_1_1.Value = syouhin_rec.KoumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    '工務店請求変更可否フラグ
                    If syouhin_rec.KoumutenGakuHenkouFlg Then
                        Me.HiddenKoumutenGakuHenkouKahi_1_1.Value = String.Empty
                    Else
                        Me.HiddenKoumutenGakuHenkouKahi_1_1.Value = EarthConst.HENKOU_FUKA
                    End If
                    ' 実請求額
                    jituSeikyuZeinukiGaku_1_1.Value = syouhin_rec.JituGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    ' 実請求税抜金額変更可否フラグ
                    If syouhin_rec.JituGakuHenkouFlg Then
                        Me.HiddenJituGakuHenkouKahi_1_1.Value = String.Empty
                    Else
                        Me.HiddenJituGakuHenkouKahi_1_1.Value = EarthConst.HENKOU_FUKA
                    End If
                    ' 消費税額
                    Me.shouhiZei_1_1.Value = syouhin_rec.TaxUri.ToString(EarthConst.FORMAT_KINGAKU_1)
                    ' 税込金額（実請求税抜金額＋消費税）
                    Me.zeikomiGaku_1_1.Value = (syouhin_rec.JituGaku + syouhin_rec.TaxUri).ToString(EarthConst.FORMAT_KINGAKU_1)
                Else
                    ' 工務店請求額
                    Me.koumutenSeikyuZeinukiGaku_1_1.Value = "0"
                    Me.HiddenKoumutenGakuHenkouKahi_1_1.Value = EarthConst.HENKOU_FUKA  '変更不可
                    ' 実請求額
                    Me.jituSeikyuZeinukiGaku_1_1.Value = "0"
                    Me.HiddenJituGakuHenkouKahi_1_1.Value = EarthConst.HENKOU_FUKA      '変更不可
                    Me.shouhiZei_1_1.Value = "0"                   ' 消費税額
                    Me.zeikomiGaku_1_1.Value = "0"                 ' 税込金額
                End If

                '●販売価格セット時は、常に特別対応価格を再計算予定とする
                '商品1の特別対応ツールチップを初期化
                ClearToolTipValue("1_1")
                '確認へ画面用フラグセット(商品1特別価格のみ更新)
                Me.HiddenTokutaiKkkHaneiFlg.Value = "1"     '商品1のみ例外価格設定
                Me.HiddenTokutaiKkkHaneiFlgPu.Value = "1"     '商品1のみ例外価格設定(特別対応画面用)

            End If

            ' 非表示項目
            kakakuSetteiBasyo.Value = syouhin_rec.KakakuSettei
            zeiritu_1_1.Value = syouhin_rec.Zeiritu
            kingakuFlg_1_1.Value = ""

        Else
            If hattyuuKingaku_1_1.Value <> "0" And hattyuuKingaku_1_1.Value <> String.Empty Then
                '発注書金額が設定済みの場合、クリアしない
                MLogic.AlertMessage(sender, Messages.MSG010E, 0, "hin1SetError")
                '依頼内容確定ボタン押下時用のチェックフラグにFalseをセット
                flgShouhin1ClrOK = False

                '商品1をOLD値に戻す
                setSyouhin1Old(sender, actCtrl)

            ElseIf intSetSts = EarthEnum.emSyouhin1Error.GetSyouhin1 Then
                '商品1情報取得エラーは商品1クリア
                Dim tmpUpdDatetime As String = HiddenUpdDatetime_1_1.Value
                clrSyouhinLine(EarthConst.Instance.ARRAY_SHOUHIN_LINES(0))
                '更新日時は退避して保持（排他チェック用）
                HiddenUpdDatetime_1_1.Value = tmpUpdDatetime
            Else
                strErrMsg = cbLogic.GetSyouhin1ErrMsg(intSetSts)
                MLogic.AlertMessage(sender, strErrMsg, 0, "GetSyouhin1Error")
                '依頼内容確定ボタン押下時用のチェックフラグにFalseをセット
                flgShouhin1ClrOK = False
            End If
        End If

        '変更前値保持Hiddenの内容を更新
        If itemKb_1.Checked Then
            itemKbPre.Value = itemKb_1.Value
        ElseIf itemKb_2.Checked Then
            itemKbPre.Value = itemKb_2.Value
        ElseIf itemKb_3.Checked Then
            itemKbPre.Value = itemKb_3.Value
        ElseIf itemKb_9.Checked Then
            itemKbPre.Value = itemKb_9.Value
        End If
        kameitenCdOld.Value = kameitenCd.Value
        HiddenTysHouhouPre.Value = cboTyousaHouhou.SelectedValue
        tyousaGaiyouPre.Value = cboTyousaGaiyou.SelectedValue
        Me.HiddenSyouhin1Pre.Value = Me.choSyouhin1.SelectedValue
        Me.HiddenTatemonoYoutoPre.Value = Me.cboTatemonoYouto.SelectedValue

    End Sub

    ''' <summary>
    ''' 商品1をOLD値に戻します
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSyouhin1Old(ByVal sender As System.Object, ByVal actCtrl As Control)

        'OLD値に値を戻す
        Dim itemKbn As String = itemKbPre.Value '商品区分OLD値テンポラリ
        Select Case actCtrl.ID
            Case itemKb_1.ID, itemKb_2.ID, itemKb_3.ID, itemKb_9.ID
                itemKb_1.Checked = False
                itemKb_2.Checked = False
                itemKb_3.Checked = False
                itemKb_9.Checked = False
                Select Case itemKbn
                    Case itemKb_1.Value
                        itemKb_1.Checked = True
                    Case itemKb_2.Value
                        itemKb_2.Checked = True
                    Case itemKb_3.Value
                        itemKb_3.Checked = True
                    Case itemKb_9.Value
                        itemKb_9.Checked = True
                End Select
            Case kameitenSearch.ID
                kameitenCd.Value = kameitenCdOld.Value
            Case cboTyousaHouhou.ID
                cboTyousaHouhou.SelectedValue = HiddenTysHouhouPre.Value
                chousaHouhouCode.Value = HiddenTysHouhouPre.Value
            Case cboTyousaGaiyou.ID
                cboTyousaGaiyou.SelectedValue = tyousaGaiyouPre.Value
            Case btnIrainaiyouKakutei.ID
                '依頼内容確定ボタン押下時は、すべて戻す
                itemKb_1.Checked = False
                itemKb_2.Checked = False
                itemKb_3.Checked = False
                itemKb_9.Checked = False
                Select Case itemKbn
                    Case itemKb_1.Value
                        itemKb_1.Checked = True
                    Case itemKb_2.Value
                        itemKb_2.Checked = True
                    Case itemKb_3.Value
                        itemKb_3.Checked = True
                    Case itemKb_9.Value
                        itemKb_9.Checked = True
                End Select
                kameitenCd.Value = kameitenCdOld.Value
                cboTyousaHouhou.SelectedValue = HiddenTysHouhouPre.Value
                chousaHouhouCode.Value = HiddenTysHouhouPre.Value
                cboTyousaGaiyou.SelectedValue = tyousaGaiyouPre.Value
        End Select

    End Sub

    ''' <summary>
    ''' 商品コード１情報の取得
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="hin1InfoRec">Syouhin1InfoRecord</param>
    ''' <param name="hin1AutoSetRec">Syouhin1AutoSetRecord</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function Syouhin1Get(ByVal sender As System.Object, _
                                 ByRef hin1InfoRec As Syouhin1InfoRecord, _
                                 ByRef hin1AutoSetRec As Syouhin1AutoSetRecord, _
                                 Optional ByVal kameitenSerchRec As KameitenSearchRecord = Nothing, _
                                 Optional ByRef intSetSts As Integer = 0, _
                                 Optional ByVal actCtrl As Control = Nothing _
                                 ) As Boolean
        ' データ取得用ロジッククラス
        Dim logic As New JibanLogic

        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo("1_1")

        ' データ取得用のパラメータセット
        hin1InfoRec.Kubun = kubunVal.Value    ' 区分

        If itemKb_1.Checked = True Then
            ' 60年保証(EARTHでは非表示)
            hin1InfoRec.SyouhinKbn = 1        ' 商品区分
        ElseIf itemKb_2.Checked = True Then
            ' 土地販売
            hin1InfoRec.SyouhinKbn = 2        ' 商品区分
        ElseIf itemKb_3.Checked = True Then
            ' リフォーム
            hin1InfoRec.SyouhinKbn = 3        ' 商品区分
        Else
            ' 未設定は商品区分 9 
            hin1InfoRec.SyouhinKbn = 9        ' 商品区分
        End If

        ComLog.SetDisplayString(tatemonoYoutoCode.Value, hin1InfoRec.TatemonoYouto)                 ' 建物用途
        ComLog.SetDisplayString(choSyouhin1.SelectedValue, hin1InfoRec.SyouhinCd)                   ' 商品コード
        ComLog.SetDisplayString(Me.EigyousyoCd.Value, hin1InfoRec.EigyousyoCd)                      ' 営業所コード
        ComLog.SetDisplayString(cboTyousaHouhou.SelectedValue, hin1InfoRec.TyousaHouhouNo)          ' 調査方法
        ComLog.SetDisplayString(ctrlRec.JituSeikyuuGaku.Value, hin1InfoRec.ZeinukiKingaku1)         ' 税抜金額
        ComLog.SetDisplayString(ctrlRec.KoumutenSeikyuuGaku.Value, hin1InfoRec.KoumutenKingaku1)    ' 工務店請求額
        ComLog.SetDisplayString(Me.tyousakaisyaCd.Value, hin1InfoRec.TysKaisyaCd)                   ' 調査会社コード＋事業所コード
        ComLog.SetDisplayString(Me.iraiTousuu.Value, hin1InfoRec.DoujiIraiTousuu)                   ' 同時依頼棟数

        '加盟店関連情報
        If kameitenSerchRec Is Nothing Then
            hin1InfoRec.KameitenCd = kameitenCd.Value                               ' 加盟店コード
            hin1InfoRec.KeiretuCd = keiretuCd.Value                                 ' 系列コード
            hin1InfoRec.KeiretuFlg = logic.GetKeiretuFlg(hin1InfoRec.KeiretuCd)     ' 系列フラグ
        Else
            hin1InfoRec.KameitenCd = kameitenSerchRec.KameitenCd                    ' 加盟店コード
            hin1InfoRec.KeiretuCd = kameitenSerchRec.KeiretuCd                      ' 系列コード
            hin1InfoRec.KeiretuFlg = logic.GetKeiretuFlg(hin1InfoRec.KeiretuCd)     ' 系列フラグ
        End If

        '指定されている請求先をレコードにセット
        hin1AutoSetRec.SeikyuuSakiCd = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiCd.Value
        hin1AutoSetRec.SeikyuuSakiBrc = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiBrc.Value
        hin1AutoSetRec.SeikyuuSakiKbn = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiKbn.Value

        ' 商品１情報を取得
        Dim retVal As Boolean = logic.GetSyouhin1Info(sender, hin1InfoRec, hin1AutoSetRec, intSetSts)

        ' 商品情報が取得できた場合、請求先、請求先タイプ(直接or他請求)をセット
        If retVal Then
            '商品の請求先をセット
            ctrlRec.SyouhinSeikyuuSakiCd.Value = hin1AutoSetRec.SeikyuuSakiCd

            If Not actCtrl Is Nothing Then
                Select Case actCtrl.ID
                    '商品区分
                    '調査方法
                    '商品1
                    Case itemKb_1.ID, itemKb_2.ID, itemKb_3.ID, itemKb_9.ID, cboTyousaHouhou.ID, choSyouhin1.ID
                        '調査概要の設定
                        cboTyousaGaiyou.SelectedValue = ComLog.GetDispNum(hin1AutoSetRec.TyousaGaiyou, "")
                End Select
            End If
        End If

        Return retVal
    End Function

    ''' <summary>
    ''' 商品１承諾書金額の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="actCtrl">処理実行元コントロール</param>
    ''' <remarks></remarks>
    Private Sub Syouhin1SyoudakuGakuSet(ByVal sender As System.Object, ByVal actCtrl As Control)

        Dim logic As New JibanLogic
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo("1_1")

        ' 取得する承諾書価格
        Dim syoudaku_kakaku As Integer = 0

        Dim chousa_jouhou As Integer
        Dim chousa_gaiyou As Integer
        Dim irai_tousuu As Integer
        Dim decZeiRitu As Decimal
        Dim strAlertMes As String = String.Empty
        Dim blnSyoudakuHenkouFlg As Boolean '承諾書金額変更可能FLG

        ' 数値項目の変換
        ComLog.SetDisplayString(chousaHouhouCode.Value, chousa_jouhou)
        ComLog.SetDisplayString(cboTyousaGaiyou.SelectedValue, chousa_gaiyou)
        ComLog.SetDisplayString(iraiTousuu.Value, irai_tousuu)

        If logic.GetSyoudakusyoKingaku1(ctrlRec.SyouhinCd.Value, _
                                        kubunVal.Value, _
                                        chousa_jouhou, _
                                        chousa_gaiyou, _
                                        irai_tousuu, _
                                        tyousakaisyaCd.Value, _
                                        kameitenCd.Value, _
                                        syoudaku_kakaku, _
                                        Me.keiretuCd.Value, _
                                        blnSyoudakuHenkouFlg) = True Then

            ' 承諾書金額を画面にセット
            ctrlRec.SyoudakusyoKingaku.Value = syoudaku_kakaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            ' 承諾書金額 変更可否設定
            If blnSyoudakuHenkouFlg = False Then
                ctrlRec.SyoudakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
            Else
                ctrlRec.SyoudakuHenkouKahi.Value = String.Empty
            End If
            ' 税率を取得
            setDispStr(ctrlRec.ZeiRitu.Value, decZeiRitu)
            ' 仕入消費税額を画面にセット
            ctrlRec.SiireSyouhizei.Value = Fix(syoudaku_kakaku * decZeiRitu)
        Else
            '●原価マスタに該当の組み合わせが無いかつ商品設定無しは、金額無し変更不可
            If ctrlRec.SyouhinCd.Value = "" Then
                ctrlRec.SyoudakusyoKingaku.Value = ""
                ctrlRec.SiireSyouhizei.Value = ""
                ctrlRec.SyoudakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
            Else
                ctrlRec.SyoudakuHenkouKahi.Value = ""
            End If

            '商品1設定の場合のみ、
            If ctrlRec.SyouhinCd.Value <> String.Empty Then
                '調査会社アクションの場合は、値を元に戻す
                If actCtrl.ID = Me.tyousakaisyaSearch.ID AndAlso Me.tyousakaisyaCd.Value <> String.Empty Then
                    MLogic.AlertMessage(sender, Messages.MSG180E, 0, "GetGenkaError")
                    Me.tyousakaisyaCd.Value = Me.tyousakaisyaCdOld.Value
                    Me.tyousakaisyaNm.Value = Me.tyousakaisyaNmOld.Value
                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' 商品１請求書発行日、売上年月日の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="actCtrl">処理実行元コントロール</param>
    ''' <param name="uri_set">売上年月日無条件設定時:true</param>
    ''' <remarks></remarks>
    Private Sub Syouhin1UriageSeikyuDateSet(ByVal sender As System.Object, ByVal actCtrl As Control, ByVal uri_set As Boolean)

        Dim logic As New JibanLogic
        Dim tyousa_jissi_date As DateTime
        Dim seikyuu_hakkou_date As DateTime
        Dim uriage_date As DateTime

        ComLog.SetDisplayString(seikyuuHakkouDate_1_1.Value, seikyuu_hakkou_date)
        ComLog.SetDisplayString(uriageDate_1_1.Value, uriage_date)

        ' 邸別請求レコードに必要情報をセットし、請求書発行日、売上年月日を取得する
        Dim teibetu_rec As New TeibetuSeikyuuRecord

        ' 画面の情報をセット
        teibetu_rec.Kbn = kubunVal.Value
        teibetu_rec.SeikyuuUmu = seikyuUmu_1_1.Value
        teibetu_rec.SeikyuusyoHakDate = seikyuu_hakkou_date
        teibetu_rec.UriDate = uriage_date
        teibetu_rec.SyouhinCd = Me.shouhinCd_1_1.Value
        teibetu_rec.SeikyuuSakiCd = Me.ucSeikyuuSiireLink_1_1.AccSeikyuuSakiCd.Value
        teibetu_rec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink_1_1.AccSeikyuuSakiBrc.Value
        teibetu_rec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink_1_1.AccSeikyuuSakiKbn.Value

        ' 調査実施日を日付型にする
        ComLog.SetDisplayString(tyousaJissibi.Value, tyousa_jissi_date)

        ' 請求書発行日、売上年月日を取得する
        logic.SubSeikyuuUriageDateSet(kameitenCd.Value, tyousa_jissi_date, teibetu_rec, uri_set)

        ' 結果をセットする
        seikyuuHakkouDate_1_1.Value = ComLog.GetDisplayString(teibetu_rec.SeikyuusyoHakDate)
        uriageDate_1_1.Value = ComLog.GetDisplayString(teibetu_rec.UriDate)

        ' 商品１，２の請求書発行日、売上年月日の連動
        If seikyuUmu_1_1.Value = "1" Then
            seikyuuHakkouDate_2_1.Value = IIf(seikyuuHakkouDate_2_1.Value <> "", seikyuuHakkouDate_1_1.Value, seikyuuHakkouDate_2_1.Value)
            seikyuuHakkouDate_2_2.Value = IIf(seikyuuHakkouDate_2_2.Value <> "", seikyuuHakkouDate_1_1.Value, seikyuuHakkouDate_2_2.Value)
            seikyuuHakkouDate_2_3.Value = IIf(seikyuuHakkouDate_2_3.Value <> "", seikyuuHakkouDate_1_1.Value, seikyuuHakkouDate_2_3.Value)
            seikyuuHakkouDate_2_4.Value = IIf(seikyuuHakkouDate_2_4.Value <> "", seikyuuHakkouDate_1_1.Value, seikyuuHakkouDate_2_4.Value)
            uriageDate_2_1.Value = IIf(uriageDate_2_1.Value <> "", uriageDate_1_1.Value, uriageDate_2_1.Value)
            uriageDate_2_2.Value = IIf(uriageDate_2_2.Value <> "", uriageDate_1_1.Value, uriageDate_2_2.Value)
            uriageDate_2_3.Value = IIf(uriageDate_2_3.Value <> "", uriageDate_1_1.Value, uriageDate_2_3.Value)
            uriageDate_2_4.Value = IIf(uriageDate_2_4.Value <> "", uriageDate_1_1.Value, uriageDate_2_4.Value)
        End If
    End Sub

    ''' <summary>
    ''' 商品１販売価格（工務店請求額・実請求額）の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="actCtrl"></param>
    ''' <remarks></remarks>
    Private Sub Syouhin1HanbaiKakakuSet(ByVal sender As System.Object, ByVal actCtrl As Control)

        Dim lgcJiban As New JibanLogic
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo("1_1")
        Dim hin1InfoRec As New Syouhin1InfoRecord                     '商品１検索用レコード
        Dim hin1AutoSetRec As New Syouhin1AutoSetRecord               '商品１自動設定用レコード

        ' 検索KEYの設定
        ComLog.SetDisplayString(Me.chousaHouhouCode.Value, hin1InfoRec.TyousaHouhouNo)
        ComLog.SetDisplayString(Me.choSyouhin1.SelectedValue, hin1InfoRec.SyouhinCd)
        hin1InfoRec.TysKaisyaCd = Me.tyousakaisyaCd.Value
        hin1InfoRec.KameitenCd = Me.kameitenCd.Value
        hin1InfoRec.EigyousyoCd = Me.EigyousyoCd.Value
        hin1InfoRec.KeiretuCd = Me.keiretuCd.Value

        '販売価格マスタへの取得
        If lgcJiban.GetHanbaiKingaku1(hin1InfoRec, hin1AutoSetRec) Then
            '取得出来た場合
            If ctrlRec.SeikyuuUmu.Value = "1" Then
                '請求ありの場合、金額をセット
                '工務店請求額
                ctrlRec.KoumutenSeikyuuGaku.Value = hin1AutoSetRec.KoumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                '工務店請求変更可否フラグ
                If hin1AutoSetRec.KoumutenGakuHenkouFlg Then
                    ctrlRec.KoumutenHenkouKahi.Value = String.Empty
                Else
                    ctrlRec.KoumutenHenkouKahi.Value = EarthConst.HENKOU_FUKA
                End If
                '実請求額
                ctrlRec.JituSeikyuuGaku.Value = hin1AutoSetRec.JituGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                '実請求税抜金額変更可否フラグ
                If hin1AutoSetRec.JituGakuHenkouFlg Then
                    ctrlRec.JituGakuHenkouKahi.Value = String.Empty
                Else
                    ctrlRec.JituGakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
                End If
                '消費税額
                hin1AutoSetRec.TaxUri = Fix(hin1AutoSetRec.JituGaku * hin1AutoSetRec.Zeiritu)
                ctrlRec.ZeiGaku.Value = hin1AutoSetRec.TaxUri.ToString(EarthConst.FORMAT_KINGAKU_1)
                '税込金額(実請求額＋消費税)
                ctrlRec.ZeiKomiGaku.Value = (hin1AutoSetRec.JituGaku + hin1AutoSetRec.TaxUri).ToString(EarthConst.FORMAT_KINGAKU_1)
            Else
                '請求なしの場合、0円変更不可
                ' 工務店請求額
                ctrlRec.KoumutenSeikyuuGaku.Value = "0"
                ctrlRec.KoumutenHenkouKahi.Value = EarthConst.HENKOU_FUKA   '変更不可
                ' 実請求額
                ctrlRec.JituSeikyuuGaku.Value = "0"
                ctrlRec.JituGakuHenkouKahi.Value = EarthConst.HENKOU_FUKA   '変更不可
                ctrlRec.ZeiGaku.Value = "0"                   ' 消費税額
                ctrlRec.ZeiKomiGaku.Value = "0"               ' 税込金額
            End If

            '●販売価格セット時は、常に特別対応価格を再計算予定とする
            '商品1の特別対応ツールチップを初期化
            ClearToolTipValue("1_1")
            '確認へ画面用フラグセット(商品1特別価格のみ更新)
            Me.HiddenTokutaiKkkHaneiFlg.Value = "1"     '商品1のみ例外価格設定
            Me.HiddenTokutaiKkkHaneiFlgPu.Value = "1"     '商品1のみ例外価格設定(特別対応画面用)

        Else
            '取得出来ない場合かつ商品1設定無しは金額無し変更不可
            If ctrlRec.SyouhinCd.Value = "" Then
                ctrlRec.KoumutenSeikyuuGaku.Value = ""
                ctrlRec.KoumutenHenkouKahi.Value = EarthConst.HENKOU_FUKA
                ctrlRec.JituSeikyuuGaku.Value = ""
                ctrlRec.JituGakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
                ctrlRec.ZeiGaku.Value = ""
                ctrlRec.ZeiKomiGaku.Value = ""
            Else
                ctrlRec.KoumutenHenkouKahi.Value = ""
                ctrlRec.JituGakuHenkouKahi.Value = ""
            End If

            '調査会社アクションの場合は、値を元に戻す
            '商品1設定の場合のみ、
            If ctrlRec.SyouhinCd.Value <> String.Empty Then
                If actCtrl.ID = Me.tyousakaisyaSearch.ID AndAlso Me.tyousakaisyaCd.Value <> String.Empty Then
                    MLogic.AlertMessage(sender, Messages.MSG182E, 0, "GetHanbaiError")
                    Me.tyousakaisyaCd.Value = Me.tyousakaisyaCdOld.Value
                    Me.tyousakaisyaNm.Value = Me.tyousakaisyaNmOld.Value
                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' 特定店商品２設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TokuteitenSyouhin2Set(ByVal sender As Object)

        Dim logic As New JibanLogic
        Dim kameiten_cd As String = String.Empty

        ' 加盟店取得済みチェック（取消加盟店対策）
        If kameitenNm.Value = String.Empty Then
            ' 加盟店名が空(＝加盟店未取)の場合、処理を抜ける
            Exit Sub
        End If

        ' 商品２設定済みチェック
        If shouhinCd_2_1.Value <> String.Empty OrElse _
           shouhinCd_2_2.Value <> String.Empty OrElse _
           shouhinCd_2_3.Value <> String.Empty OrElse _
           shouhinCd_2_4.Value <> String.Empty Then
            ' 一つでも商品２が設定されている場合、処理を抜ける
            Exit Sub
        End If

        ' 加盟店コード取得
        ComLog.SetDisplayString(kameitenCd.Value, kameiten_cd)

        ' 特定店商品２取得処理
        Dim syouhinCd2List As New List(Of String)
        If logic.GetTokuteitenSyouhin2(kameiten_cd, syouhinCd2List) = True Then
            Dim lineCount As Integer = 1
            For Each syouhinCd2 As String In syouhinCd2List
                setSyouhinCd23(sender, "2_" & lineCount.ToString, syouhinCd2)
                lineCount += 1
                If lineCount > 4 Then
                    Exit For
                End If
            Next
        End If

    End Sub

    ''' <summary>
    ''' 多棟割り商品２設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TatouwariSet(ByVal sender As Object)

        Dim logic As New JibanLogic
        Dim syouhin_cd2 As String = ""
        Dim irai_tousuu As Integer

        ComLog.SetDisplayString(iraiTousuu.Value, irai_tousuu)

        ' 多棟割り設定時の商品コード２取得処理
        If logic.GetTatouwariSyouhinCd2(kubunVal.Value, _
                                        kameitenCd.Value, _
                                        irai_tousuu, _
                                        syouhin_cd2) = True Then

            ' 商品コードが取得できた場合、既に同一商品コードが存在しない且つ
            ' 商品コード２に空きがある場合、邸別請求より明細を取得して画面にセットする

            ' 同一コードのチェック
            If shouhinCd_2_1.Value = syouhin_cd2 Or _
               shouhinCd_2_2.Value = syouhin_cd2 Or _
               shouhinCd_2_3.Value = syouhin_cd2 Or _
               shouhinCd_2_4.Value = syouhin_cd2 Then
                ' 同一コードありの場合は設定しない
                Exit Sub
            End If

            ' 空いている欄があれば設定する
            If shouhinCd_2_1.Value = "" Then
                setSyouhinCd23(sender, "2_1", syouhin_cd2)
            ElseIf shouhinCd_2_2.Value = "" Then
                setSyouhinCd23(sender, "2_2", syouhin_cd2)
            ElseIf shouhinCd_2_3.Value = "" Then
                setSyouhinCd23(sender, "2_3", syouhin_cd2)
            ElseIf shouhinCd_2_4.Value = "" Then
                setSyouhinCd23(sender, "2_4", syouhin_cd2)
            Else
                ' 空きが無い場合は設定しない
                Exit Sub
            End If

        End If

    End Sub

#Region "商品コード2 画面設定"

#Region "商品コード2,3 画面設定"
    ''' <summary>
    ''' 商品コード2,3 画面設定
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="syouhin_type">設定したい商品行タイプ</param>
    ''' <param name="syouhin_cd">商品コード</param>
    ''' <remarks></remarks>
    Private Sub setSyouhinCd23(ByVal sender As Object, _
                               ByVal syouhin_type As String, _
                               ByVal syouhin_cd As String, _
                               Optional ByVal syouhin23_rec As Syouhin23Record = Nothing)

        ' 設定するコントロールのインスタンスを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(syouhin_type)
        Dim tmpUpdDatetime = ctrlRec.UpdDatetime.Value '更新日付退避

        If syouhin_cd = String.Empty Then
            '商品コードが空の場合、行をクリアして処理終了
            clrSyouhinLine(syouhin_type)
            '更新日時は退避していた値を戻す（排他チェック用）
            ctrlRec.UpdDatetime.Value = tmpUpdDatetime
            Exit Sub
        End If

        ' 情報取得用のパラメータクラス
        Dim syouhin23InfoRec As New Syouhin23InfoRecord
        ' 商品の基本情報を取得
        Dim syouhin23Rec As Syouhin23Record

        If syouhin23_rec Is Nothing Then
            '引数で渡されていない場合、新たに取得
            syouhin23Rec = getSyouhinInfo(syouhin_type.Split("_")(0), syouhin_cd)
        Else
            '引数で渡されている場合、それを使用
            syouhin23Rec = syouhin23_rec
        End If

        ' 取得できない場合、エラー
        If syouhin23Rec Is Nothing OrElse syouhin23Rec.SyouhinCd = String.Empty Then
            setFocus(ctrlRec.SyouhinCd)
            ' 行をクリアして処理終了
            clrSyouhinLine(syouhin_type)
            '更新日時は退避していた値を戻す（排他チェック用）
            ctrlRec.UpdDatetime.Value = tmpUpdDatetime
            Exit Sub
        End If

        '画面上で請求先が指定されている場合、レコードの請求先を上書き
        If ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiCd.Value <> String.Empty Then
            '請求先をレコードにセット
            syouhin23Rec.SeikyuuSakiCd = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiCd.Value
            syouhin23Rec.SeikyuuSakiBrc = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiBrc.Value
            syouhin23Rec.SeikyuuSakiKbn = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiKbn.Value
        End If
        '請求先タイプを画面にセット
        ctrlRec.SyouhinSeikyuuSakiCd.Value = syouhin23Rec.SeikyuuSakiCd

        ' 邸別請求情報取得用のロジッククラス
        Dim logic As New JibanLogic

        ' 商品コード及び画面の情報をセット
        syouhin23InfoRec.Syouhin2Rec = syouhin23Rec                             ' 商品の基本情報
        syouhin23InfoRec.SeikyuuUmu = ctrlRec.SeikyuuUmu.Value                  ' 請求有無
        syouhin23InfoRec.HattyuusyoKakuteiFlg = ctrlRec.HattyuusyoKakutei.Value ' 発注書確定フラグ
        syouhin23InfoRec.Seikyuusaki = syouhin23Rec.SeikyuuSakiType             ' 請求先タイプ(直接or他請求)
        syouhin23InfoRec.KeiretuCd = keiretuCd.Value                            ' 系列コード
        syouhin23InfoRec.KeiretuFlg = logic.GetKeiretuFlg(keiretuCd.Value)      ' 系列フラグ

        ' 請求レコードの取得(確実に結果が有る)
        Dim teibetu_seikyuu_rec As TeibetuSeikyuuRecord = getSyouhin23SeikyuuInfo(sender, syouhin23InfoRec)

        ' コード、名称をセット（多棟割りの場合、設定必須の為）
        ctrlRec.BunruiCd.Value = syouhin23Rec.SoukoCd
        ctrlRec.SyouhinCd.Value = syouhin23Rec.SyouhinCd
        ctrlRec.SyouhinCdOld.Value = syouhin23Rec.SyouhinCd
        ctrlRec.DispSyouhinNm.InnerHtml = syouhin23Rec.SyouhinMei
        ctrlRec.SyouhinNm.Value = syouhin23Rec.SyouhinMei

        ' 商品３は確定区分をセット
        If syouhin_type.Split("_")(0) = "3" Then
            ctrlRec.KakuteiKbn.Value = IIf(teibetu_seikyuu_rec.KakuteiKbn = Integer.MinValue, "0", teibetu_seikyuu_rec.KakuteiKbn.ToString())
            ctrlRec.KakuteiKbnOld.Value = ctrlRec.KakuteiKbn.Value
            ' 確定状況により、請求書発行日・売上年月日設定
            logic.SubChangeKakutei(kameitenCd.Value, teibetu_seikyuu_rec)
            ctrlRec.SeikyuusyoHakkouDate.Value = ComLog.GetDisplayString(teibetu_seikyuu_rec.SeikyuusyoHakDate)
            ctrlRec.UriageDate.Value = ComLog.GetDisplayString(teibetu_seikyuu_rec.UriDate)
            ctrlRec.UriageJyoukyou.Value = IIf(teibetu_seikyuu_rec.UriKeijyouFlg = 1, EarthConst.URIAGE_ZUMI, "")
        End If

        ' 価格情報をセット
        ctrlRec.KoumutenSeikyuuGaku.Value = teibetu_seikyuu_rec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        ctrlRec.JituSeikyuuGaku.Value = teibetu_seikyuu_rec.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        ctrlRec.ZeiGaku.Value = (Fix(teibetu_seikyuu_rec.UriGaku * syouhin23InfoRec.Syouhin2Rec.Zeiritu)).ToString(EarthConst.FORMAT_KINGAKU_1)
        ctrlRec.ZeiKomiGaku.Value = teibetu_seikyuu_rec.UriGaku + Fix(teibetu_seikyuu_rec.UriGaku * syouhin23InfoRec.Syouhin2Rec.Zeiritu).ToString(EarthConst.FORMAT_KINGAKU_1)
        ctrlRec.SyoudakusyoKingaku.Value = teibetu_seikyuu_rec.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        ctrlRec.SiireSyouhizei.Value = Fix(teibetu_seikyuu_rec.SiireGaku * syouhin23InfoRec.Syouhin2Rec.Zeiritu)
        ctrlRec.ZeiKubun.Value = teibetu_seikyuu_rec.ZeiKbn
        ctrlRec.ZeiRitu.Value = syouhin23Rec.Zeiritu
        ctrlRec.KingakuFlg.Value = ""
        If ctrlRec.HattyuusyoKakuteiOld.Value <> "1" Then
            ctrlRec.HattyuusyoKakuteiOld.Value = ""
        End If

        ' 商品２の場合、商品１と日付連動
        If syouhin_type.Split("_")(0) = "2" Then
            ' 売上は無条件
            ctrlRec.UriageDate.Value = uriageDate_1_1.Value

            If seikyuUmu_1_1.Value = "1" Then
                ' 商品１，２の請求書発行日、売上年月日の連動
                ctrlRec.SeikyuusyoHakkouDate.Value = seikyuuHakkouDate_1_1.Value
            Else
                Dim cBizLogic As New CommonBizLogic
                ' 商品１の請求有無が無の場合のみ算出
                ctrlRec.SeikyuuSiireLink.SetSeikyuuSimeDate(syouhin23Rec.SyouhinCd)
                ctrlRec.SeikyuusyoHakkouDate.Value = ctrlRec.SeikyuuSiireLink.GetSeikyuusyoHakkouDate()
            End If
        End If

        ' 金額再設定
        setKingaku(syouhin_type)

        ' コントロールの活性制御
        subEnableSeikyuu23(syouhin_type)

    End Sub
#End Region

#Region "商品 コントロール行"
    ''' <summary>
    ''' 商品関連コントロールのインスタンスを列単位に取得する
    ''' </summary>
    ''' <param name="syouhin_type">インスタンスを取得したいレコードタイプ</param>
    ''' <returns>インスタンスを格納したレコードクラス</returns>
    ''' <remarks></remarks>
    Private Function getSyouhinRowInfo(ByVal syouhin_type As String) As SyouhinCtrlRecord

        Dim ctrl_rec As New SyouhinCtrlRecord

        ctrl_rec.SyouhinLine = FindControl("shouhinLine_" & syouhin_type)
        ctrl_rec.BunruiCd = FindControl("bunruiCd_" & syouhin_type)
        ctrl_rec.SyouhinCd = FindControl("shouhinCd_" & syouhin_type)
        ctrl_rec.SyouhinCdOld = FindControl("shouhinCdOld_" & syouhin_type)
        ctrl_rec.SeikyuuSiireLink = FindControl("ucSeikyuuSiireLink_" & syouhin_type)
        ctrl_rec.ShouhinSearchBtn = FindControl("shouhinSearch_" & syouhin_type)
        ctrl_rec.SyouhinNm = FindControl("shouhinNmText_" & syouhin_type)
        ctrl_rec.DispSyouhinNm = FindControl("shouhinNm_" & syouhin_type)
        If syouhin_type.Split("_")(0) = "3" Then
            ctrl_rec.KakuteiKbn = FindControl("kakutei_" & syouhin_type)
            ctrl_rec.KakuteiKbnSpan = FindControl("kakuteiSpan_" & syouhin_type)
            ctrl_rec.UriageJyoukyou = FindControl("uriageJyoukyou_" & syouhin_type)
            ctrl_rec.KakuteiKbnOld = FindControl("HiddenKakuteiOld_" & syouhin_type)
        End If
        ctrl_rec.KoumutenSeikyuuGaku = FindControl("koumutenSeikyuZeinukiGaku_" & syouhin_type)
        ctrl_rec.KoumutenHenkouKahi = FindControl("HiddenKoumutenGakuHenkouKahi_" & syouhin_type)
        ctrl_rec.JituSeikyuuGaku = FindControl("jituSeikyuZeinukiGaku_" & syouhin_type)
        ctrl_rec.JituGakuHenkouKahi = FindControl("HiddenJituGakuHenkouKahi_" & syouhin_type)
        ctrl_rec.ZeiGaku = FindControl("shouhiZei_" & syouhin_type)
        ctrl_rec.ZeiKubun = FindControl("zeikubun_" & syouhin_type)
        ctrl_rec.ZeiRitu = FindControl("zeiritu_" & syouhin_type)
        ctrl_rec.ZeiKomiGaku = FindControl("zeikomiGaku_" & syouhin_type)
        ctrl_rec.SyoudakusyoKingaku = FindControl("shoudakuKingaku_" & syouhin_type)
        ctrl_rec.SyoudakuHenkouKahi = FindControl("HiddenSyoudakuHenkouKahi_" & syouhin_type)
        ctrl_rec.SiireSyouhizei = FindControl("HidSiireSyouhizei_" & syouhin_type)
        ctrl_rec.SeikyuusyoHakkouDate = FindControl("seikyuuHakkouDate_" & syouhin_type)
        ctrl_rec.UriageDate = FindControl("uriageDate_" & syouhin_type)
        ctrl_rec.SeikyuuUmu = FindControl("seikyuUmu_" & syouhin_type)
        ctrl_rec.SeikyuuUmuSpan = FindControl("seikyuUmuSpan_" & syouhin_type)
        ctrl_rec.MitumoriDate = FindControl("mitumoriSakuseiDate_" & syouhin_type)
        ctrl_rec.HattyuusyoKakutei = FindControl("hattyuuKakutei_" & syouhin_type)
        ctrl_rec.HattyuusyoKakuteiSpan = FindControl("hattyuuKakuteiSpan_" & syouhin_type)
        ctrl_rec.HattyuusyoKakuteiOld = FindControl("hattyuuKakuteiOld_" & syouhin_type)
        ctrl_rec.HattyuusyoKingaku = FindControl("hattyuuKingaku_" & syouhin_type)
        ctrl_rec.HidHattyuusyoKingakuOld = FindControl("HiddenHattyuuKingakuOld_" & syouhin_type)
        ctrl_rec.HattyuusyoKakuninbi = FindControl("hattyuuKakuninDate_" & syouhin_type)
        ctrl_rec.KingakuFlg = FindControl("kingakuFlg_" & syouhin_type)
        ctrl_rec.UriageKeijyouFlg = FindControl("uriageKeijyouFlg_" & syouhin_type)
        ctrl_rec.UriageKeijyouBi = FindControl("uriageKeijyouBi_" & syouhin_type)
        ctrl_rec.Bikou = FindControl("bikou_" & syouhin_type)
        ctrl_rec.IkkatuNyuukinFlg = FindControl("ikkatuNyuukinFlg_" & syouhin_type)
        ctrl_rec.SeikyuuSiireLink = FindControl("ucSeikyuuSiireLink_" & syouhin_type)
        ctrl_rec.TokubetuTaiouToolTip = FindControl("ucTokubetuTaiouToolTipCtrl_" & syouhin_type)
        ctrl_rec.UpdDatetime = FindControl("HiddenUpdDatetime_" & syouhin_type)
        ctrl_rec.SyouhinSeikyuuSakiCd = FindControl("HidSyouhinSeikyuuSakiCd_" & syouhin_type)
        If syouhin_type.Split("_")(0) = "2" Then
            ctrl_rec.KoumutenSeikyuuGakuOld = FindControl("koumutenSeikyuZeinukiGakuOld_" & syouhin_type)
            ctrl_rec.JituSeikyuuGakuOld = FindControl("jituSeikyuZeinukiGakuOld_" & syouhin_type)
            ctrl_rec.SyoudakusyoKingakuOld = FindControl("shoudakuKingakuOld_" & syouhin_type)
            ctrl_rec.HattyuusyoKingakuOld = FindControl("hattyuuKingakuOld_" & syouhin_type)
        End If
        Return ctrl_rec

    End Function

    ''' <summary>
    ''' 指定商品行の値をクリアする
    ''' </summary>
    ''' <param name="ln"></param>
    ''' <remarks></remarks>
    Private Sub clrSyouhinLine(ByVal ln As String)

        '対象行のコントロールをクリア
        jBn.ClearCtrlValue(FindControl("shouhinLine_" & ln))

    End Sub

#End Region

#End Region

    ''' <summary>
    ''' 商品２、３画面表示用の商品情報を取得します
    ''' </summary>
    ''' <param name="syouhin_type">商品２or３</param>
    ''' <param name="syouhin_cd">商品コード</param>
    ''' <param name="blnPostBack">True:PostBack時,False:初期読込処理時</param>
    ''' <returns>商品2,3画面設定用邸別請求レコード</returns>
    ''' <remarks></remarks>
    Private Function getSyouhinInfo(ByVal syouhin_type As String, _
                                    ByVal syouhin_cd As String, _
                                    Optional ByVal blnPostBack As Boolean = True) As Syouhin23Record

        Dim syouhin23_rec As Syouhin23Record = Nothing

        ' 情報取得用のロジッククラス
        Dim logic As New JibanLogic
        Dim count As Integer = 0

        ' 商品情報を取得する
        '（コード指定なので１件のみ取得される）(加盟店コード指定でデフォルトの請求先情報も合わせて取得)
        Dim intSyouhinKbn As EarthEnum.EnumSyouhinKubun
        If blnPostBack Then 'PostBack時
            Select Case syouhin_type
                Case "1"
                    intSyouhinKbn = EarthEnum.EnumSyouhinKubun.Syouhin1
                Case "2"
                    intSyouhinKbn = EarthEnum.EnumSyouhinKubun.Syouhin2_110
                Case "3"
                    intSyouhinKbn = EarthEnum.EnumSyouhinKubun.Syouhin3
                Case Else
                    intSyouhinKbn = EarthEnum.EnumSyouhinKubun.AllSyouhin
            End Select

        Else '初期読込処理時
            intSyouhinKbn = EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi
        End If

        Dim TyousaHouhouNo As Integer = Integer.MinValue
        ComLog.SetDisplayString(cboTyousaHouhou.SelectedValue, TyousaHouhouNo)          ' 調査方法

        Dim list As List(Of Syouhin23Record) = logic.GetSyouhin23(syouhin_cd, _
                                                                  "", _
                                                                  intSyouhinKbn, _
                                                                  count, _
                                                                  TyousaHouhouNo, _
                                                                  kameitenCd.Value)

        ' 取得できない場合
        If list.Count < 1 Then
            Return syouhin23_rec
        End If

        ' 取得できた場合のみセット
        syouhin23_rec = list(0)

        Return syouhin23_rec

    End Function

    ''' <summary>
    ''' 商品２、３画面表示用の邸別請求データを取得します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="syouhin23_info">商品２，３情報取得用のパラメータクラス</param>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Private Function getSyouhin23SeikyuuInfo(ByVal sender As Object, _
                                             ByVal syouhin23_info As Syouhin23InfoRecord _
                                             ) As TeibetuSeikyuuRecord

        Dim teibetu_rec As TeibetuSeikyuuRecord = Nothing

        ' 情報取得用のロジッククラス
        Dim logic As New JibanLogic

        ' 請求データの取得
        teibetu_rec = logic.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1)

        Return teibetu_rec

    End Function

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks></remarks>
    Public Function checkInput(Optional ByVal flgNextGamen As Boolean = False, Optional ByVal flgSyouhinKkkChk As Boolean = True) As Boolean
        Dim e As New System.EventArgs

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList
        Dim seikyuuUmuErrMess As String = String.Empty

        Dim strErrMsg As String = String.Empty '作業用
        Dim blnKamentenFlg As Boolean = False
        Dim kameitenSearchLogic As New KameitenSearchLogic

        If hakiSyubetuVal.Value >= "1" Then
            '破棄種別が選択されている場合、スルー

        Else
            'コード入力値変更チェック
            If kameitenCd.Value <> kameitenCdOld.Value Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "加盟店コード")
                arrFocusTargetCtrl.Add(kameitenSearch)
                blnKamentenFlg = True 'フラグを立てる
            End If
            If tyousakaisyaCd.Value <> tyousakaisyaCdOld.Value Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "調査会社コード")
                arrFocusTargetCtrl.Add(tyousakaisyaSearch)
            End If
            For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
                Dim tmpSyouhinRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)
                If ln <> EarthConst.Instance.ARRAY_SHOUHIN_LINES(0) And _
                   tmpSyouhinRec.SyouhinCd.Value <> tmpSyouhinRec.SyouhinCdOld.Value Then
                    errMess += Messages.MSG030E.Replace("@PARAM1", "商品コード" & ln & "行目")
                    arrFocusTargetCtrl.Add(tmpSyouhinRec.ShouhinSearchBtn)
                End If
            Next

            '依頼内容確定チェック
            If flgNextGamen = True And (iraiSession.IraiST = EarthConst.MODE_NEW And flgKakutei.Value = "0") Then
                errMess += Messages.MSG028E
                arrFocusTargetCtrl.Add(btnIrainaiyouKakutei)
            End If

            '必須チェック
            If kameitenCd.Value = "" Or kameitenNm.Value = "" Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "加盟店")
                arrFocusTargetCtrl.Add(kameitenCd)
                blnKamentenFlg = True 'フラグを立てる
            End If
            If cboTatemonoYouto.SelectedValue = "" Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "建物用途")
                arrFocusTargetCtrl.Add(cboTatemonoYouto)
            End If
            If tyousakaisyaCd.Value = "" Or tyousakaisyaNm.Value = "" Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "調査会社")
                arrFocusTargetCtrl.Add(tyousakaisyaCd)
            End If
            If cboTyousaHouhou.SelectedValue = "" Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "調査方法")
                arrFocusTargetCtrl.Add(cboTyousaHouhou)
            End If
            If shouhinCd_1_1.Value = "" And (flgNextGamen = True Or flgKakutei.Value = "1") Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "商品コード１")
                If btnIrainaiyouKaijo.Visible Then
                    arrFocusTargetCtrl.Add(btnIrainaiyouKaijo)
                End If
            End If
            If choSyouhin1.SelectedValue = "" Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "商品1")
                arrFocusTargetCtrl.Add(choSyouhin1)
            End If

        End If

        '禁則文字数チェック
        If jBn.KinsiStrCheck(koujiTantouNm.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "工事担当者")
            arrFocusTargetCtrl.Add(koujiTantouNm)
        End If

        'バイト数チェック
        If jBn.ByteCheckSJIS(koujiTantouNm.Value, koujiTantouNm.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "工事担当者")
            arrFocusTargetCtrl.Add(koujiTantouNm)
        End If

        '桁数チェック
        If jBn.SuutiStrCheck(iraiTousuu.Value, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "同時依頼棟数").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(iraiTousuu)
        End If
        If jBn.SuutiStrCheck(kosuu.Value, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "戸数").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(kosuu)
        End If

        'プラス値不許可チェック
        For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)
            Dim search_shouhin2 As String = ln.Split("_")(0)   '商品2判定用
            Dim search_shouhin2_2 As Integer                   '商品2判定用

            ComLog.SetDisplayString(ln.Split("_")(1), search_shouhin2_2)

            If search_shouhin2 = "2" Then
                If ctrlRec.SyouhinCd.Value <> String.Empty Then
                    If ctrlRec.BunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_2_115 Then

                        '工務店請求金額
                        If ctrlRec.KoumutenSeikyuuGaku.Value <> ctrlRec.KoumutenSeikyuuGakuOld.Value Then
                            '変更有の場合チェックを行なう
                            If jBn.KingakuPlusCheck(ctrlRec.KoumutenSeikyuuGaku.Value) = False Then
                                errMess += Messages.MSG198E.Replace("@PARAM1", ln).Replace("@PARAM2", "工務店請求税抜金額")
                                arrFocusTargetCtrl.Add(ctrlRec.SeikyuuSiireLink)
                            End If
                        End If
                        '実請求金額
                        If ctrlRec.JituSeikyuuGaku.Value <> ctrlRec.JituSeikyuuGakuOld.Value Then
                            '変更有の場合チェックを行なう
                            If jBn.KingakuPlusCheck(ctrlRec.JituSeikyuuGaku.Value) = False Then
                                errMess += Messages.MSG198E.Replace("@PARAM1", ln).Replace("@PARAM2", "実請求税抜金額")
                                arrFocusTargetCtrl.Add(ctrlRec.SeikyuuSiireLink)
                            End If
                        End If
                        '承諾書金額
                        If ctrlRec.SyoudakusyoKingaku.Value <> ctrlRec.SyoudakusyoKingakuOld.Value Then
                            '変更有の場合チェックを行なう
                            If jBn.KingakuPlusCheck(ctrlRec.SyoudakusyoKingaku.Value) = False Then
                                errMess += Messages.MSG198E.Replace("@PARAM1", ln).Replace("@PARAM2", "承諾書金額")
                                arrFocusTargetCtrl.Add(ctrlRec.SeikyuuSiireLink)
                            End If
                        End If
                        '発注書金額
                        If ctrlRec.HattyuusyoKingaku.Value <> ctrlRec.HattyuusyoKingakuOld.Value Then
                            '変更有の場合チェックを行なう
                            If jBn.KingakuPlusCheck(ctrlRec.HattyuusyoKingaku.Value) = False Then
                                errMess += Messages.MSG198E.Replace("@PARAM1", ln).Replace("@PARAM2", "発注書金額")
                                arrFocusTargetCtrl.Add(ctrlRec.SeikyuuSiireLink)
                            End If
                        End If
                    End If
                End If
            End If
        Next

        'その他チェック
        '●調査概要/同時依頼棟数チェック
        strErrMsg = String.Empty
        If cbLogic.ChkErrTysGaiyou(Me.cboTyousaGaiyou.SelectedValue, Me.iraiTousuu.Value, strErrMsg) = False Then
            errMess += strErrMsg
            arrFocusTargetCtrl.Add(Me.iraiTousuu)
        End If
        '●ビルダー注意事項チェック(加盟店関連のエラーがない場合チェックする)
        strErrMsg = String.Empty
        If blnKamentenFlg = False Then
            If kameitenSearchLogic.ChkBuilderData13(kameitenCd.Value) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If
            If cbLogic.ChkErrBuilderData(Me.cboTyousaGaiyou.SelectedValue, Me.kameitenCd.Value, Me.HiddenKameitenTyuuiJikou.Value, strErrMsg) = False Then
                errMess += strErrMsg
                arrFocusTargetCtrl.Add(Me.kameitenCd)
            End If
        End If

        '商品の請求有無と売上金額との関連チェック(請求無し・0円以外：NG / 請求あり・0円: 警告)
        If HiddenSeikyuuUmuCheck.Value <> "1" And iraiSession.Irai2Mode = EarthConst.MODE_EDIT Then
            For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
                Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)
                If ctrlRec.SyouhinCd.Value <> String.Empty Then
                    If ctrlRec.SeikyuuUmu.Value = 0 And ctrlRec.JituSeikyuuGaku.Value <> "0" Then
                        '請求無し・0円以外：NG
                        errMess += String.Format(Messages.MSG157E, ln)
                        arrFocusTargetCtrl.Add(ctrlRec.SeikyuuUmu)
                    ElseIf ctrlRec.SeikyuuUmu.Value = 1 And ctrlRec.JituSeikyuuGaku.Value = "0" Then
                        '請求あり・0円: 警告
                        seikyuuUmuErrMess += "商品" & ln & "、"
                        arrFocusTargetCtrl.Add(ctrlRec.JituSeikyuuGaku)
                    End If
                End If
            Next
        End If

        '商品価格設定・原価・販売価格マスタへの組み合わせ存在チェック(商品1のみ)
        If flgSyouhinKkkChk Then
            Dim logic As New JibanLogic
            Dim intTysHouhou As Integer
            Dim intTysGaiyou As Integer
            Dim intDoujiIraiTousuu As Integer

            Dim recSyouhin1 As SyouhinCtrlRecord = getSyouhinRowInfo("1_1") '商品１レコード

            '初期読込時と登録時に商品価格設定関係の値が変更時のみ『商品価格設定マスタ』値をチェックする
            If Me.HiddenOpenValuesSyouhinKkk.Value <> getCtrlValuesStringSyouhinKkk() Then

                '設定・取得用 商品価格設定レコード
                Dim recKakakuSettei As New KakakuSetteiRecord

                ' 商品区分
                If itemKb_1.Checked = True Then
                    ' 60年保証(EARTHでは非表示)
                    recKakakuSettei.SyouhinKbn = 1
                ElseIf itemKb_2.Checked = True Then
                    ' 土地販売
                    recKakakuSettei.SyouhinKbn = 2
                ElseIf itemKb_3.Checked = True Then
                    ' リフォーム
                    recKakakuSettei.SyouhinKbn = 3
                Else
                    ' 未設定は商品区分 9 
                    recKakakuSettei.SyouhinKbn = 9
                End If
                ' 商品コード
                ComLog.SetDisplayString(Me.choSyouhin1.SelectedValue, recKakakuSettei.SyouhinCd)
                ' 調査方法
                ComLog.SetDisplayString(Me.cboTyousaHouhou.SelectedValue, recKakakuSettei.TyousaHouhouNo)

                '商品価格設定マスタから値の取得
                If logic.GetTysGaiyou(recKakakuSettei) = False Then
                    errMess += String.Format(Messages.MSG183E)
                    arrFocusTargetCtrl.Add(Me)
                End If
            End If

            '○初期読込時と登録時に原価関係の値が変更時のみ『原価マスタ』値チェックをする
            If Me.HiddenOpenValuesGenka.Value <> getCtrlValuesStringGenka() Then

                '数値項目の変換
                ComLog.SetDisplayString(Me.chousaHouhouCode.Value, intTysHouhou)
                ComLog.SetDisplayString(Me.cboTyousaGaiyou.SelectedValue, intTysGaiyou)
                ComLog.SetDisplayString(Me.iraiTousuu.Value, intDoujiIraiTousuu)

                '原価チェック
                If logic.GetSyoudakusyoKingaku1(recSyouhin1.SyouhinCd.Value, _
                                                Me.kubunVal.Value, _
                                                intTysHouhou, _
                                                intTysGaiyou, _
                                                intDoujiIraiTousuu, _
                                                Me.tyousakaisyaCd.Value, _
                                                Me.kameitenCd.Value, _
                                                0, _
                                                Me.keiretuCd.Value, _
                                                False) = False Then

                    errMess += String.Format(Messages.MSG180E)
                    arrFocusTargetCtrl.Add(Me)
                End If
            End If

            '○初期読込時と登録時に販売価格関係の値が変更時のみ『販売価格マスタ』値チェックをする
            If Me.HiddenOpenValuesHanbai.Value <> getCtrlValuesStringHanbai() Then
                '販売価格チェック
                Dim hin1InfoRec As New Syouhin1InfoRecord
                Dim hin1AutoSetRecord As New Syouhin1AutoSetRecord
                hin1InfoRec.SyouhinCd = recSyouhin1.SyouhinCd.Value
                hin1InfoRec.TysKaisyaCd = Me.tyousakaisyaCd.Value
                hin1InfoRec.TyousaHouhouNo = intTysHouhou
                hin1InfoRec.KameitenCd = Me.kameitenCd.Value
                hin1InfoRec.EigyousyoCd = Me.EigyousyoCd.Value
                hin1InfoRec.KeiretuCd = Me.keiretuCd.Value

                If logic.GetHanbaiKingaku1(hin1InfoRec, _
                                           hin1AutoSetRecord) = False Then
                    errMess += String.Format(Messages.MSG182E)
                    arrFocusTargetCtrl.Add(Me)
                End If
            End If
        End If

        '商品1(依頼内容の選択項目と商品1/商品2の表示項目)の差異チェック
        If flgSyouhinKkkChk Then
            If shouhinCd_1_1.Value <> choSyouhin1.SelectedValue Then
                'エラーメッセージ表示
                errMess += Messages.MSG041E.Replace("範囲指定", "商品1の選択内容と表示ラベル")
                arrFocusTargetCtrl.Add(choSyouhin1)
            End If
        End If

        '入力項目チェック(調査手配センター)
        Dim strTysTehai As String = String.Empty
        strTysTehai = cbLogic.ChkExistKameitenTysTehaiCenter(Me.kameitenCd.Value, Me.tyousakaisyaCd.Value)
        If strTysTehai <> String.Empty Then
            errMess += Messages.MSG206E.Replace("@PARAM1", strTysTehai)
            arrFocusTargetCtrl.Add(Me.tyousakaisyaCd)
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        '請求有無と売上金額との関連チェックの結果、確認メッセージを表示する
        If seikyuuUmuErrMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "if(confirm('" & String.Format(Messages.MSG156C, seikyuuUmuErrMess) & "')){document.getElementById('" & HiddenSeikyuuUmuCheck.ClientID & "').value='1'; autoExeButtonId = '" & Page.Request.Params.Get("__EVENTTARGET") & "';};"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        '画面項目再設定
        setDispAction(Me, e)

        'エラー無しの場合、Trueを返す
        Return True

    End Function

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行う（参照処理用）
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub setIrai2FromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecord)

        'セッションのirai2を削除
        iraiSession.Irai2Data = Nothing
        'セッションのddlを削除
        iraiSession.DdlDataSyouhin1 = Nothing
        iraiSession.DdlDataTysHouhou = Nothing

        '加盟店、調査会社マスタ取得時に商品取得を行わないためのフラグを立てる
        flgNotGetSyouhin = True

        '基本情報
        kubunVal.Value = jr.Kbn
        hosyousyoNoVal.Value = jr.HosyousyoNo

        '加盟店 -> 関連情報取得
        kameitenCd.Value = jr.KameitenCd
        If kameitenCd.Value <> "" Then
            actCtrlId.Value = kameitenSearch.ClientID
            kameitenSearchSub(kameitenSearchAfter, e, False)
        End If
        kameitenCdOld.Value = kameitenCd.Value

        '依頼内容
        koujiTantouNm.Value = jr.KojTantousyaMei
        itemKb_1.Checked = (itemKb_1.Value = jr.SyouhinKbn)
        itemKb_2.Checked = (itemKb_2.Value = jr.SyouhinKbn)
        itemKb_3.Checked = (itemKb_3.Value = jr.SyouhinKbn)
        itemKb_9.Checked = (itemKb_9.Value = jr.SyouhinKbn)
        iraiTousuu.Value = Format(jr.DoujiIraiTousuu, "#,0")
        cboTatemonoYouto.SelectedValue = IIf(jr.TatemonoYoutoNo = 0, "", jr.TatemonoYoutoNo)
        kosuu.Value = Format(jr.Kosuu, "#,0")

        '調査会社情報取得
        tyousakaisyaCd.Value = jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd
        If tyousakaisyaCd.Value <> "" Then
            actCtrlId.Value = tyousakaisyaSearch.ClientID
            tyousakaisyaSearchSub(tyousakaisyaSearch, e, False)
        End If
        tyousakaisyaCdOld.Value = tyousakaisyaCd.Value
        tyousakaisyaNmOld.Value = tyousakaisyaNm.Value

        'ReportIF進捗ステータス
        HiddenStatusIf.Value = jr.StatusIf
        If EarthConst.Instance.IF_STATUS.ContainsKey(HiddenStatusIf.Value) Then
            TextStatusIf.Value = EarthConst.Instance.IF_STATUS(HiddenStatusIf.Value)
        Else
            TextStatusIf.Value = HiddenStatusIf.Value
        End If

        '加盟店、調査会社マスタ取得時に商品取得を行わないためのフラグを下げる
        flgNotGetSyouhin = False

        '調査方法のDDL表示処理
        ComLog.ps_SetSelectTextBoxTysHouhou(jr.TysHouhou, Me.cboTyousaHouhou, False)

        '調査概要
        cboTyousaGaiyou.SelectedValue = IIf(jr.TysGaiyou = 0, 9, jr.TysGaiyou)

        '保証書発行状況自動セット関連
        HiddenHosyousyoHakkouJyoukyouMoto.Value = IIf(jr.HosyousyoHakJyky = 0, "", jr.HosyousyoHakJyky)
        HiddenHosyousyoHakkouJyoukyou.Value = HiddenHosyousyoHakkouJyoukyouMoto.Value
        HiddenTyousaGaiyouMoto.Value = cboTyousaGaiyou.SelectedValue
        HiddenHosyousyoHakkouJyoukyouSetteiDateMoto.Value = ComLog.GetDisplayString(jr.HosyousyoHakJykySetteiDate)

        '●保証商品有無
        ' 商品設定状況
        Me.HiddenHosyouSyouhinUmuOld.Value = ComLog.GetDisplayString(jr.HosyouSyouhinUmu)

        '●保証書発行日
        Me.HiddenHosyousyoHakDateOld.Value = ComLog.GetDisplayString(jr.HosyousyoHakDate)

        '●計画書作成日
        Me.HiddenKeikakusyoSakuseiDateOld.Value = ComLog.GetDisplayString(jr.KeikakusyoSakuseiDate)

        cboHosyouUmu.Value = IIf(jr.HosyouUmu = Integer.MinValue, "", jr.HosyouUmu)
        HosyouKikan.Value = IIf(jr.HosyouKikan = Integer.MinValue, "", jr.HosyouKikan)
        KojGaisyaSeikyuuUmu.Value = IIf(jr.KojGaisyaSeikyuuUmu = Integer.MinValue, "", jr.KojGaisyaSeikyuuUmu)

        '更新日付
        updateDateTime.Value = IIf(jr.UpdDatetime = Date.MinValue, "", Format(jr.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))

        ' 進捗データの項目セット(調査実施日)
        tyousaJissibi.Value = getDispStr(jr.TysJissiDate)

        ' データ取得用ロジッククラス
        Dim logic As New JibanLogic

        ' 変更前値をセット
        itemKbPre.Value = jr.SyouhinKbn
        kameitenCdOld.Value = jr.KameitenCd
        HiddenTysHouhouPre.Value = jr.TysHouhou
        tyousaGaiyouPre.Value = jr.TysGaiyou
        Me.HiddenTatemonoYoutoPre.Value = jr.TatemonoYoutoNo

        '●原価マスタより承諾書金額変更FLGを取得
        Dim blnSyoudakuHenkouFlg As Boolean
        Dim strSyouhinCd As String = String.Empty

        '商品1コードの取得（取得出来た場合のみ設定）
        If jr.Syouhin1Record IsNot Nothing Then

            '商品1コード
            strSyouhinCd = jr.Syouhin1Record.SyouhinCd

            '商品1前値を保持
            Me.HiddenSyouhin1Pre.Value = jr.Syouhin1Record.SyouhinCd

            ' 価格設定場所
            kakakuSetteiBasyo.Value = logic.GetKakakuSetteiBasyo(jr.SyouhinKbn, jr.TysHouhou, strSyouhinCd)

            '商品1プルダウンの設定と存在チェック
            If ComLog.ChkDropDownList(choSyouhin1, ComLog.GetDispNum(strSyouhinCd)) Then
                'DDLにあれば、選択する
                choSyouhin1.SelectedValue = ComLog.GetDispNum(strSyouhinCd)
            Else
                'DDLになければ、アイテムを追加
                choSyouhin1.Items.Add(New ListItem(strSyouhinCd & ":" & jr.Syouhin1Record.SyouhinMei, strSyouhinCd))
                choSyouhin1.SelectedValue = ComLog.GetDispNum(strSyouhinCd)     '選択状態
            End If

            '原価マスタへのチェック（承諾書金額）
            If logic.GetSyoudakusyoKingaku1(strSyouhinCd, _
                                            jr.Kbn, _
                                            jr.TysHouhou, _
                                            jr.TysGaiyou, _
                                            jr.DoujiIraiTousuu, _
                                            jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd, _
                                            jr.KameitenCd, _
                                            0, _
                                            Me.keiretuCd.Value, _
                                            blnSyoudakuHenkouFlg) Then
                jr.Syouhin1Record.SyoudakuHenkouKahi = blnSyoudakuHenkouFlg
            Else
                '取得出来ない場合承諾書金額変更不可
                jr.Syouhin1Record.SyoudakuHenkouKahi = False
            End If

            '販売価格マスタへのチェック（工務店請求金額・実請求税抜金額）
            Dim hin1InfoRec As New Syouhin1InfoRecord
            Dim hin1AutoSetRec As New Syouhin1AutoSetRecord

            '検索KEYの設定
            hin1InfoRec.TysKaisyaCd = jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd      '調査会社コード＋事業所コード
            hin1InfoRec.KameitenCd = jr.KameitenCd                                  '加盟店コード
            setDispStr(Me.EigyousyoCd.Value, hin1InfoRec.EigyousyoCd)               '営業所コード
            setDispStr(Me.keiretuCd.Value, hin1InfoRec.KeiretuCd)                   '系列コード
            hin1InfoRec.SyouhinCd = strSyouhinCd                                    '商品コード
            hin1InfoRec.TyousaHouhouNo = jr.TysHouhou                               '調査方法NO

            If logic.GetHanbaiKingaku1(hin1InfoRec, hin1AutoSetRec) Then
                '工務店請求・実請求の変更可否設定
                jr.Syouhin1Record.KoumutenHenkouKahi = hin1AutoSetRec.KoumutenGakuHenkouFlg
                jr.Syouhin1Record.JituseikyuuHenkouKahi = hin1AutoSetRec.JituGakuHenkouFlg
            Else
                '取得出来ない場合、工務店請求・実請求税抜金額変更不可
                jr.Syouhin1Record.KoumutenHenkouKahi = False
                jr.Syouhin1Record.JituseikyuuHenkouKahi = False
            End If

            '商品１
            Dim ln As String = "1_1"    '商品行番号
            '邸別請求データから画面商品コントロールに値をセットする
            setTeibetuToSyouhin(ln, jr.Syouhin1Record)

            '○特別対応ボタン用にデフォルトの加盟店・商品コード・調査方法をセット
            If String.IsNullOrEmpty(Me.HiddenKakuteiValuesTokubetu.Value) Then
                Me.HiddenKakuteiValuesTokubetu.Value = getCtrlValuesStringTokubetu()
            End If

        End If

        '商品２
        If jr.Syouhin2Records IsNot Nothing Then
            For Each de As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In jr.Syouhin2Records
                Dim ln As String = "2_" & de.Key    '商品行番号
                '邸別請求データから画面商品コントロールに値をセットする
                setTeibetuToSyouhin(ln, de.Value)
            Next
        End If

        '商品３
        If jr.Syouhin3Records IsNot Nothing Then
            For Each de As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In jr.Syouhin3Records
                Dim ln As String = "3_" & de.Key    '商品行番号
                '邸別請求データから画面商品コントロールに値をセットする
                setTeibetuToSyouhin(ln, de.Value)
            Next
        End If

        kaiyakuHaraimodosi.Value = "0"
        ' 解約払戻金額をHiddenのkaiyakuHaraimodosiに設定する（分類:180 残額計算で使用）
        If jr.KaiyakuHaraimodosiRecord IsNot Nothing Then
            ' 解約払い戻し有りの場合、Hiddenにセット
            kaiyakuHaraimodosi.Value = Format(jr.KaiyakuHaraimodosiRecord.UriGaku, EarthConst.FORMAT_KINGAKU_1)
        End If

        ' 入金額の初期化
        nyuukinGaku_1.Value = "0"
        nyuukinGaku_2.Value = "0"
        nyuukinGaku_3.Value = "0"
        nyuukinGaku_4.Value = "0"

        ' 入金額を邸別入金の情報より設定する（商品１：110 商品３:120）
        If jr.TeibetuNyuukinRecords IsNot Nothing Then
            For Each de As KeyValuePair(Of String, TeibetuNyuukinRecord) In jr.TeibetuNyuukinRecords

                Dim rec As TeibetuNyuukinRecord = de.Value

                If rec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1 Then
                    ' 商品１の入金額をセット
                    nyuukinGaku_1.Value = Format(rec.NyuukinGaku, EarthConst.FORMAT_KINGAKU_1)
                    nyuukinGaku_2.Value = Format(rec.NyuukinGaku, EarthConst.FORMAT_KINGAKU_1)
                ElseIf rec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3 Then
                    ' 商品２の入金額をセット
                    nyuukinGaku_3.Value = Format(rec.NyuukinGaku, EarthConst.FORMAT_KINGAKU_1)
                    nyuukinGaku_4.Value = Format(rec.NyuukinGaku, EarthConst.FORMAT_KINGAKU_1)
                End If
            Next
        End If

        ' 残額計算する
        setZangaku()

        '区分・番号に紐付く特別対応データを取得する
        GetTokubetuTaiouCd(sender)

        'セッションに画面情報を格納
        jSM.Ddl2Hash(Me.choSyouhin1, jBn.DdlDataSyouhin1)
        iraiSession.DdlDataSyouhin1 = jBn.DdlDataSyouhin1
        jSM.Ddl2Hash(Me.cboTyousaHouhou, jBn.DdlDataTysHouhou)
        iraiSession.DdlDataTysHouhou = jBn.DdlDataTysHouhou
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai2Data = jBn.IraiData

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面の商品情報へ値セットを行う(特別対応価格用)
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Private Sub setSyouhinFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecord)

        ' データ取得用ロジッククラス
        Dim logic As New JibanLogic
        Dim strSyouhinCd As String = String.Empty

        '商品1コードの取得（取得出来た場合のみ設定）
        If jr.Syouhin1Record IsNot Nothing Then

            '商品１
            Dim ln As String = "1_1"    '商品行番号
            '画面コントロールを取得
            Dim ctrlSyouhinCdText As HtmlInputText = FindControl("shouhinCd_" & ln)         '画面.商品コード
            Dim ctrlBunruiCdHidden As HtmlInputHidden = FindControl("bunruiCd_" & ln)       '画面.分類コード

            '計算ロジック結果のステータスにより処理分岐
            If jr.Syouhin1Record.KkkHenkouCheck = EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE Then
                '【更新】
                If ctrlSyouhinCdText.Value = jr.Syouhin1Record.SyouhinCd And _
                    ctrlBunruiCdHidden.Value = jr.Syouhin1Record.BunruiCd Then       '分類コードと商品コードが一致時のみ更新
                    '邸別請求データから商品コントロールにセット(特別対応金額系項目のみ)
                    setTeibetuToSyouhinForTokubetu(ln, jr.Syouhin1Record)
                End If
            End If
        End If

        '商品２
        If jr.Syouhin2Records IsNot Nothing Then
            For Each de As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In jr.Syouhin2Records
                Dim ln As String = "2_" & de.Key    '商品行番号
                '画面コントロールを取得
                Dim ctrlSyouhinCdText As HtmlInputText = FindControl("shouhinCd_" & ln)         '画面.商品コード
                Dim ctrlBunruiCdHidden As HtmlInputHidden = FindControl("bunruiCd_" & ln)       '画面.分類コード

                '計算ロジック結果のステータスにより処理分岐
                Select Case de.Value.KkkHenkouCheck

                    Case EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT
                        '【追加】
                        If String.IsNullOrEmpty(ctrlSyouhinCdText.Value) Then       '画面の商品コードが無い場合のみセット
                            'レコードの商品コードを画面にセット
                            ctrlSyouhinCdText.Value = de.Value.SyouhinCd
                            '商品検索ボタン押下時処理・設定処理
                            Me.targetLine_SearchSyouhin23.Value = ln
                            afterSearchSyouhin23_ServerClick(sender, e)

                            '邸別請求データから商品コントロールにセット(特別対応金額系項目のみ)
                            setTeibetuToSyouhinForTokubetu(ln, de.Value)
                        End If

                    Case EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE
                        '【更新】
                        If ctrlSyouhinCdText.Value = de.Value.SyouhinCd And _
                            ctrlBunruiCdHidden.Value = de.Value.BunruiCd Then       '分類コードと商品コードが一致時のみ更新
                            '邸別請求データから商品コントロールにセット(特別対応金額系項目のみ)
                            setTeibetuToSyouhinForTokubetu(ln, de.Value)
                        End If

                    Case Else
                        Continue For

                End Select
            Next
        End If

        '商品３
        If jr.Syouhin3Records IsNot Nothing Then
            For Each de As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In jr.Syouhin3Records
                Dim ln As String = "3_" & de.Key    '商品行番号

                '画面コントロールを取得
                Dim ctrlSyouhinCdText As HtmlInputText = FindControl("shouhinCd_" & ln)         '画面.商品コード
                Dim ctrlBunruiCdHidden As HtmlInputHidden = FindControl("bunruiCd_" & ln)       '画面.分類コード

                '計算ロジック結果のステータスにより処理分岐
                Select Case de.Value.KkkHenkouCheck
                    Case EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT
                        '【追加】
                        If String.IsNullOrEmpty(ctrlSyouhinCdText.Value) Then       '画面の商品コードが無い場合のみセット
                            'レコードの商品コードを画面にセット
                            ctrlSyouhinCdText.Value = de.Value.SyouhinCd

                            '商品検索ボタン押下時処理・設定処理
                            Me.targetLine_SearchSyouhin23.Value = ln
                            afterSearchSyouhin23_ServerClick(sender, e)

                            '邸別請求データから商品コントロールにセット(特別対応金額系項目のみ)
                            setTeibetuToSyouhinForTokubetu(ln, de.Value)
                        End If

                    Case EarthEnum.emTeibetuSeikyuuKkkJyky.DELETE
                        '【削除】
                        If Not String.IsNullOrEmpty(ctrlSyouhinCdText.Value) Then
                            '商品コードを初期化
                            ctrlSyouhinCdText.Value = String.Empty
                            '商品なし検索をして、初期化する
                            Me.targetLine_SearchSyouhin23.Value = ln
                            afterSearchSyouhin23_ServerClick(sender, e)
                        End If

                    Case EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE
                        '【更新】
                        If ctrlSyouhinCdText.Value = de.Value.SyouhinCd And _
                            ctrlBunruiCdHidden.Value = de.Value.BunruiCd Then       '分類コードと商品コードが一致時のみ更新
                            '邸別請求データから商品コントロールにセット(特別対応金額系項目のみ)
                            setTeibetuToSyouhinForTokubetu(ln, de.Value)
                        End If

                    Case Else
                        Continue For
                End Select
            Next
        End If

        ' 残額計算する
        setZangaku()

    End Sub

    ''' <summary>
    ''' 邸別請求データから画面商品コントロールに値をセットする（参照処理用）
    ''' </summary>
    ''' <param name="ln">商品行番号</param>
    ''' <param name="hinRec">邸別請求データ</param>
    ''' <remarks></remarks>
    Private Sub setTeibetuToSyouhin(ByVal ln As String, ByVal hinRec As TeibetuSeikyuuRecord)

        If hinRec Is Nothing Then
            Exit Sub
        End If

        '指定行の商品コントロールを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

        '分類コード
        ctrlRec.BunruiCd.Value = hinRec.BunruiCd
        '商品コード
        ctrlRec.SyouhinCd.Value = hinRec.SyouhinCd
        ctrlRec.SyouhinCdOld.Value = hinRec.SyouhinCd
        '商品名
        ctrlRec.SyouhinNm.Value = hinRec.SyouhinMei
        ctrlRec.DispSyouhinNm.InnerHtml = hinRec.SyouhinMei
        '工務店請求税抜金額
        ctrlRec.KoumutenSeikyuuGaku.Value = Format(hinRec.KoumutenSeikyuuGaku, "#,0")
        '●税抜売上金額(実請求税抜金額)
        ctrlRec.JituSeikyuuGaku.Value = Format(hinRec.UriGaku, "#,0")
        '税率
        ctrlRec.ZeiRitu.Value = hinRec.Zeiritu
        '税区分
        ctrlRec.ZeiKubun.Value = hinRec.ZeiKbn
        '●売上消費税額(消費税)
        ctrlRec.ZeiGaku.Value = Format(hinRec.UriageSyouhiZeiGaku, "#,0")
        '●税込売上金額(税込額)
        ctrlRec.ZeiKomiGaku.Value = Format(hinRec.ZeikomiUriGaku, "#,0")
        '承諾書金額
        ctrlRec.SyoudakusyoKingaku.Value = IIf(hinRec.SiireGaku = Integer.MinValue, "", Format(hinRec.SiireGaku, "#,0"))
        '仕入消費税額
        ctrlRec.SiireSyouhizei.Value = IIf(hinRec.SiireSyouhiZeiGaku = Integer.MinValue, "", hinRec.SiireSyouhiZeiGaku)
        '請求書発行非
        ctrlRec.SeikyuusyoHakkouDate.Value = getDispStr(hinRec.SeikyuusyoHakDate)
        '売上年月日
        ctrlRec.UriageDate.Value = getDispStr(hinRec.UriDate)
        '請求有無
        ctrlRec.SeikyuuUmu.Value = hinRec.SeikyuuUmu
        '見積書作成日
        ctrlRec.MitumoriDate.Value = getDispStr(hinRec.TysMitsyoSakuseiDate)
        '発注書確定
        ctrlRec.HattyuusyoKakutei.Value = hinRec.HattyuusyoKakuteiFlg
        ctrlRec.HattyuusyoKakuteiOld.Value = hinRec.HattyuusyoKakuteiFlg
        '発注書金額
        ctrlRec.HattyuusyoKingaku.Value = IIf(hinRec.HattyuusyoGaku = Integer.MinValue, "", Format(hinRec.HattyuusyoGaku, "#,0"))
        '商品1のみの設定
        If ln.Split("_")(0) = "1" Then
            ' 承諾書金額変更可否を設定
            If hinRec.SyoudakuHenkouKahi = False Then
                ctrlRec.SyoudakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
            Else
                ctrlRec.SyoudakuHenkouKahi.Value = String.Empty
            End If
            ' 工務店請求金額変更可否を設定
            If hinRec.KoumutenHenkouKahi = False Then
                ctrlRec.KoumutenHenkouKahi.Value = EarthConst.HENKOU_FUKA
            Else
                ctrlRec.KoumutenHenkouKahi.Value = String.Empty
            End If
            ' 実請求税抜金額変更可否を設定
            If hinRec.JituseikyuuHenkouKahi = False Then
                ctrlRec.JituGakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
            Else
                ctrlRec.JituGakuHenkouKahi.Value = String.Empty
            End If
        End If
        '商品2のみの設定
        If ln.Split("_")(0) = "2" Then
            '工務店請求税抜金額Old
            ctrlRec.KoumutenSeikyuuGakuOld.Value = ctrlRec.KoumutenSeikyuuGaku.Value
            '実請求税抜金額Old
            ctrlRec.JituSeikyuuGakuOld.Value = ctrlRec.JituSeikyuuGaku.Value
            '承諾書金額Old
            ctrlRec.SyoudakusyoKingakuOld.Value = ctrlRec.SyoudakusyoKingaku.Value
            '発注書金額Old
            ctrlRec.HattyuusyoKingakuOld.Value = ctrlRec.HattyuusyoKingaku.Value
        End If
        '発注書確認日
        ctrlRec.HattyuusyoKakuninbi.Value = getDispStr(hinRec.HattyuusyoKakuninDate)
        '売上計上FLG
        ctrlRec.UriageKeijyouFlg.Value = getDispStr(hinRec.UriKeijyouFlg)
        '売上計上日
        ctrlRec.UriageKeijyouBi.Value = getDispStr(hinRec.UriKeijyouDate)
        '備考
        ctrlRec.Bikou.Value = getDispStr(hinRec.Bikou)
        '一括入金FLG
        ctrlRec.IkkatuNyuukinFlg.Value = getDispStr(hinRec.IkkatuNyuukinFlg)
        If ln.Split("_")(0) = "1" Then
            ' 商品１は売上状況１を設定()
            uriageJyoukyou1.Value = IIf(hinRec.UriKeijyouFlg = 1, EarthConst.URIAGE_ZUMI, "")
        End If
        If ln.Split("_")(0) = "3" Then
            ' 商品３は各々に売上状況を設定
            ctrlRec.UriageJyoukyou.Value = IIf(hinRec.UriKeijyouFlg = 1, EarthConst.URIAGE_ZUMI, "")
            ' 確定区分をセット
            ctrlRec.KakuteiKbn.Value = hinRec.KakuteiKbn
            ' 確定区分Oldをセット
            ctrlRec.KakuteiKbnOld.Value = ctrlRec.KakuteiKbn.Value
        End If

        '請求先/仕入先情報の取得
        ctrlRec.SeikyuuSiireLink.SetSeikyuuSiireLinkFromTeibetuRec(hinRec)

        '直接請求、他請求の情報を取得
        Dim syouhin23Rec As Syouhin23Record = getSyouhinInfo(ln.Split("_")(0), ctrlRec.SyouhinCd.Value, False)
        '画面上で請求先が指定されている場合、レコードの請求先を上書き
        If ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiCd.Value <> String.Empty Then
            '請求先をレコードにセット
            syouhin23Rec.SeikyuuSakiCd = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiCd.Value
            syouhin23Rec.SeikyuuSakiBrc = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiBrc.Value
            syouhin23Rec.SeikyuuSakiKbn = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiKbn.Value
        End If
        '請求先タイプを画面にセット
        ctrlRec.SyouhinSeikyuuSakiCd.Value = syouhin23Rec.SeikyuuSakiCd

        '読み込みデータの更新日時（排他チェック用）
        If hinRec.UpdDatetime = Date.MinValue Then
            '更新日時がNullの場合、登録日時を保持
            ctrlRec.UpdDatetime.Value = IIf(hinRec.AddDatetime = Date.MinValue, "", Format(hinRec.AddDatetime, EarthConst.FORMAT_DATE_TIME_1))
        Else
            '更新日時がNullの場合、登録日時を保持
            ctrlRec.UpdDatetime.Value = IIf(hinRec.UpdDatetime = Date.MinValue, "", Format(hinRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        End If

    End Sub

    ''' <summary>
    ''' 邸別請求データから商品コントロールにセット（特別対応価格用）
    ''' </summary>
    ''' <param name="ln"></param>
    ''' <param name="hinRec"></param>
    ''' <remarks></remarks>
    Private Sub setTeibetuToSyouhinForTokubetu(ByVal ln As String, ByVal hinRec As TeibetuSeikyuuRecord)

        If hinRec Is Nothing Then
            Exit Sub
        End If

        '指定行の商品コントロールを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

        '工務店請求税抜金額
        ctrlRec.KoumutenSeikyuuGaku.Value = Format(hinRec.KoumutenSeikyuuGaku, "#,0")
        '●税抜売上金額(実請求税抜金額)
        ctrlRec.JituSeikyuuGaku.Value = Format(hinRec.UriGaku, "#,0")
        '税率
        ctrlRec.ZeiRitu.Value = hinRec.Zeiritu
        '税区分
        ctrlRec.ZeiKubun.Value = hinRec.ZeiKbn
        '売上消費税額(消費税)　
        ctrlRec.ZeiGaku.Value = Format(hinRec.UriageSyouhiZeiGaku, "#,0")
        '税込売上金額(税込額) 
        ctrlRec.ZeiKomiGaku.Value = Format(hinRec.ZeikomiUriGaku, "#,0")


    End Sub

    ''' <summary>
    ''' 発注書確定フラグ変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub hattyuusyoKakuteiHenkou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funCheckHKakutei(1, sender, hattyuusyoHenkouLineNo.Value)

        If hattyuusyoHenkouLineNo.Value.Split("_")(0) = "1" Then
            '入力制御
            subEnableSeikyuu1()
        Else
            '入力制御
            subEnableSeikyuu23(hattyuusyoHenkouLineNo.Value)
        End If

        'フォーカスセット
        setFocus(FindControl("hattyuuKakutei_" & hattyuusyoHenkouLineNo.Value))

    End Sub

    ''' <summary>
    ''' 発注書金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub hattyuusyoKingakuHenkou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        hattyuuAfterUpdate(sender, hattyuusyoHenkouLineNo.Value)
    End Sub

#Region "発注書金額関連の処理"
    ''' <summary>
    ''' 発注金額変更時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ln"></param>
    ''' <remarks></remarks>
    Private Sub hattyuuAfterUpdate(ByVal sender As Object, _
                                   ByVal ln As String)

        ' 発注書確定コンボ
        Dim hattyuu_kakutei_ctrl As HtmlSelect = FindControl("hattyuuKakutei_" & ln)
        ' 発注書金額
        Dim hattyuu_kin_ctrl As HtmlInputText = FindControl("hattyuuKingaku_" & ln)
        ' 発注書金額old
        Dim hattyuu_kin_old_ctrl As HtmlInputHidden = FindControl("HiddenHattyuuKingakuOld_" & ln)
        ' 発注書確認日
        Dim hattyuu_kakunin_ctrl As HtmlInputText = FindControl("hattyuuKakuninDate_" & ln)

        Dim hattyuu_kingaku As Integer = Integer.MinValue

        ComLog.SetDisplayString(hattyuu_kin_ctrl.Value, hattyuu_kingaku)

        hattyuu_kakunin_ctrl.Value = pfunZ010SetHatyuuYMD(hattyuu_kingaku)

        '発注書確定チェック
        Select Case funCheckHKakutei(2, sender, ln)
            Case 1
                setFocus(hattyuu_kakutei_ctrl)
            Case 2
                hattyuu_kakutei_ctrl.Value = "1"
        End Select

        '更新後発注書金額をoldに設定
        hattyuu_kin_old_ctrl.Value = hattyuu_kin_ctrl.Value
        hattyuu_kin_ctrl.Value = IIf(hattyuu_kingaku = Integer.MinValue, "", hattyuu_kingaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        '入力制御
        subEnableSeikyuu23(ln)

        'フォーカスセット
        setFocus(hattyuu_kin_ctrl)

    End Sub

    ''' <summary>
    ''' 発注書確認日設定処理
    ''' </summary>
    ''' <param name="rvntKingaku">発注書金額</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pfunZ010SetHatyuuYMD(ByVal rvntKingaku As Integer) As String
        If rvntKingaku = 0 Or rvntKingaku = Integer.MinValue Then
            Return ""
        Else
            Return Date.Now.ToString("yyyy/MM/dd")
        End If
    End Function

    ''' <summary>
    ''' 発注書確定チェック
    ''' </summary>
    ''' <param name="rlngMode">1,発注書確定変更時.2,発注書金額変更時</param>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="ln">商品の位置</param>
    ''' <returns>0:処理なし、1:発注書確定へフォーカス移行、2:自動確定</returns>
    ''' <remarks></remarks>
    Public Function funCheckHKakutei(ByVal rlngMode As Long, _
                                     ByVal sender As Object, _
                                     ByVal ln As String) As Long
        funCheckHKakutei = 0

        ' 売上状況
        Dim uriage_ctrl As HtmlInputHidden = IIf(ln.Split("_")(0) <> "3", _
                                                 FindControl("uriageJyoukyou1"), _
                                                 FindControl("uriageJyoukyou_" & ln))
        ' 発注書確定
        Dim hattyuu_kakutei_ctrl As HtmlSelect = FindControl("hattyuuKakutei_" & ln)
        ' 発注書金額
        Dim hattyuu_kin_ctrl As HtmlInputText = FindControl("hattyuuKingaku_" & ln)
        ' 発注書金額old
        Dim hattyuu_kin_old_ctrl As HtmlInputHidden = FindControl("HiddenHattyuuKingakuOld_" & ln)
        ' 税抜金額
        Dim zeinuki_ctrl As HtmlInputText = FindControl("jituSeikyuZeinukiGaku_" & ln)

        If hattyuu_kakutei_ctrl.Value = 1 And _
           hattyuu_kin_ctrl.Value IsNot zeinuki_ctrl.Value Then
            ' JSで処理
        End If

        If rlngMode = 2 Then
            If uriage_ctrl.Value = EarthConst.URIAGE_ZUMI Then

                ' 比較する金額を数値変換する
                Dim chk_val1 As Integer = 0
                Dim chk_val2 As Integer = 0
                ComLog.SetDisplayString(hattyuu_kin_ctrl.Value, chk_val1)
                ComLog.SetDisplayString(zeinuki_ctrl.Value, chk_val2)

                ' 比較して同じ場合は無条件に確定する（確定不要の場合はJS側でキャンセルされる為）
                If chk_val1 = chk_val2 Then
                    funCheckHKakutei = 2
                End If
            End If
        End If
    End Function
#End Region

    ''' <summary>
    ''' 工務店請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kingakuHenkouKoumuten_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        subSetJituGaku(sender, kingakuHenkouLineNo.Value)
    End Sub

    ''' <summary>
    ''' 実請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kingakuHenkouJituseikyu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        subSetKoumutenGaku(sender, kingakuHenkouLineNo.Value)
    End Sub

    ''' <summary>
    ''' 商品コード2,3の実請求金額設定処理（工務店金額変更時に呼ぶ）
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="ln">商品の位置</param>
    ''' <remarks></remarks>
    Private Sub subSetJituGaku(ByVal sender As Object, ByVal ln As String)

        ' 設定するコントロールのインスタンスを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)
        Dim lgcJiban As New JibanLogic

        ctrlRec.JituSeikyuuGaku.Value = IIf(ctrlRec.JituSeikyuuGaku.Value = String.Empty, 0, ctrlRec.JituSeikyuuGaku.Value)
        ctrlRec.KoumutenSeikyuuGaku.Value = IIf(ctrlRec.KoumutenSeikyuuGaku.Value = String.Empty, 0, ctrlRec.KoumutenSeikyuuGaku.Value)

        ' 調査請求先が直接請求の場合
        If ctrlRec.SyouhinSeikyuuSakiCd.Value = kameitenCd.Value Then

            ' 工務店請求額を税抜金額（実請求金額）へセット
            ctrlRec.JituSeikyuuGaku.Value = ctrlRec.KoumutenSeikyuuGaku.Value

        ElseIf lgcJiban.GetKeiretuFlg(keiretuCd.Value) = 1 Then

            If ctrlRec.KingakuFlg.Value <> "2" Then

                Dim logic As New JibanLogic

                Dim koumuten_gaku As Integer = 0
                Dim zeinuki_gaku As Integer = 0

                ComLog.SetDisplayString(ctrlRec.KoumutenSeikyuuGaku.Value, koumuten_gaku)

                If logic.GetSeikyuuGaku(sender, _
                                        5, _
                                        keiretuCd.Value, _
                                        ctrlRec.SyouhinCd.Value, _
                                        koumuten_gaku, _
                                        zeinuki_gaku) Then

                    ' 税抜金額（実請求金額）へセット
                    ctrlRec.JituSeikyuuGaku.Value = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                    ctrlRec.KingakuFlg.Value = "1"

                End If

            End If
        End If

        ctrlRec.KoumutenSeikyuuGaku.Value = IIf(ctrlRec.KoumutenSeikyuuGaku.Value = String.Empty, 0, CInt(ctrlRec.KoumutenSeikyuuGaku.Value).ToString(EarthConst.FORMAT_KINGAKU_1))
        ctrlRec.JituSeikyuuGaku.Value = IIf(ctrlRec.JituSeikyuuGaku.Value = String.Empty, 0, CInt(ctrlRec.JituSeikyuuGaku.Value).ToString(EarthConst.FORMAT_KINGAKU_1))

        ' 金額再設定
        setKingaku(ln)

        '実請求税抜金額にフォーカスセット
        setFocus(ctrlRec.JituSeikyuuGaku)

    End Sub

    ''' <summary>
    ''' 商品コード2,3の工務店金額設定処理（実請求金額変更時に呼ぶ）
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="ln">商品の位置</param>
    ''' <remarks></remarks>
    Private Sub subSetKoumutenGaku(ByVal sender As Object, ByVal ln As String)

        Dim lgcJiban As New JibanLogic

        ' 設定するコントロールのインスタンスを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

        ctrlRec.JituSeikyuuGaku.Value = IIf(ctrlRec.JituSeikyuuGaku.Value = String.Empty, 0, ctrlRec.JituSeikyuuGaku.Value)
        ctrlRec.KoumutenSeikyuuGaku.Value = IIf(ctrlRec.KoumutenSeikyuuGaku.Value = String.Empty, 0, ctrlRec.KoumutenSeikyuuGaku.Value)

        ' 調査請求先が直接請求の場合
        If ctrlRec.SyouhinSeikyuuSakiCd.Value = kameitenCd.Value Then

            ' 税抜金額（実請求金額）を工務店請求額へをセット
            ctrlRec.KoumutenSeikyuuGaku.Value = ctrlRec.JituSeikyuuGaku.Value

        ElseIf lgcJiban.GetKeiretuFlg(keiretuCd.Value) = 1 Then

            If ctrlRec.KingakuFlg.Value <> "1" Then

                Dim logic As New JibanLogic

                Dim koumuten_gaku As Integer = 0
                Dim zeinuki_gaku As Integer = 0

                ComLog.SetDisplayString(ctrlRec.JituSeikyuuGaku.Value, zeinuki_gaku)

                ' 請求額算出メソッドへの引数設定（商品１の場合のみ6,他は4）
                Dim param As Integer = IIf(ln.Split("_")(0) = "1", 6, 4)

                ' 請求額を算出する
                If logic.GetSeikyuuGaku(sender, _
                                        param, _
                                        keiretuCd.Value, _
                                        ctrlRec.SyouhinCd.Value, _
                                        zeinuki_gaku, _
                                        koumuten_gaku) Then

                    ' 工務店請求額へセット
                    ctrlRec.KoumutenSeikyuuGaku.Value = koumuten_gaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                    ctrlRec.KingakuFlg.Value = "2"

                End If

            End If
        End If

        ctrlRec.KoumutenSeikyuuGaku.Value = IIf(ctrlRec.KoumutenSeikyuuGaku.Value = String.Empty, 0, CInt(ctrlRec.KoumutenSeikyuuGaku.Value).ToString(EarthConst.FORMAT_KINGAKU_1))
        ctrlRec.JituSeikyuuGaku.Value = IIf(ctrlRec.JituSeikyuuGaku.Value = String.Empty, 0, CInt(ctrlRec.JituSeikyuuGaku.Value).ToString(EarthConst.FORMAT_KINGAKU_1))

        ' 金額再設定
        setKingaku(ln)

        '承諾書金額にフォーカスセット
        setFocus(ctrlRec.SyoudakusyoKingaku)

    End Sub

    ''' <summary>
    ''' 承諾書金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kingakuHenkouSyoudakusyo_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim logic As New JibanLogic
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(kingakuHenkouLineNo.Value)

        Dim intSyoudakusyoKingaku As Integer = 0
        Dim decZeiRitu As Decimal

        ' 仕入消費税額算出
        If ctrlRec.SyoudakusyoKingaku.Value <> String.Empty Then
            ' 承諾書金額を画面から取得
            setDispStr(ctrlRec.SyoudakusyoKingaku.Value, intSyoudakusyoKingaku)
            ' 税率を画面から取得
            setDispStr(ctrlRec.ZeiRitu.Value, decZeiRitu)
            ' 仕入消費税額を画面にセット
            ctrlRec.SiireSyouhizei.Value = Fix(intSyoudakusyoKingaku * decZeiRitu)
        Else
            ' 承諾書金額が空の場合、仕入消費税額をクリア
            ctrlRec.SiireSyouhizei.Value = ""
        End If

        '請求有無にフォーカスセット
        setFocus(ctrlRec.SeikyuuUmu)

    End Sub

    ''' <summary>
    ''' 金額設定
    ''' </summary>
    ''' <param name="ln">商品の位置</param>
    ''' <remarks></remarks>
    Private Sub setKingaku(ByVal ln As String)

        ' 設定するコントロールのインスタンスを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        ComLog.SetDisplayString(ctrlRec.JituSeikyuuGaku.Value, zeinuki)
        ComLog.SetDisplayString(ctrlRec.ZeiRitu.Value, zeiritu)

        zeigaku = Fix(zeinuki * zeiritu)
        zeikomi_gaku = zeinuki + zeigaku

        ctrlRec.ZeiGaku.Value = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1)
        ctrlRec.ZeiKomiGaku.Value = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1)

        ' 残額再設定
        setZangaku()

    End Sub

    ''' <summary>
    ''' 受注【確認】での各種処理実行
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="exeMode">確認画面からの処理モード</param>
    ''' <remarks></remarks>
    Private Sub exeAtKakunin(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal exeMode As String)
        Dim jiban_logic As New JibanLogic
        Dim jR As New JibanRecord
        Dim jibanRecAfterExe As New JibanRecord
        Dim listRec As New List(Of TokubetuTaiouRecordBase)

        '登録/変更実行
        If exeMode = EarthConst.MODE_EXE_TOUROKU Or exeMode = EarthConst.MODE_EXE_PDF_RENRAKU Or _
           exeMode = EarthConst.MODE_EXE_TYOUSAMITSUMORISYO_SAKUSEI Then

            jR = iraiSession.JibanData

            jR.KameitenCd = kameitenCd.Value
            jR.KojTantousyaMei = koujiTantouNm.Value

            If itemKb_1.Checked Then
                jR.SyouhinKbn = itemKb_1.Value
            End If
            If itemKb_2.Checked Then
                jR.SyouhinKbn = itemKb_2.Value
            End If
            If itemKb_3.Checked Then
                jR.SyouhinKbn = itemKb_3.Value
            End If
            If itemKb_9.Checked Then
                jR.SyouhinKbn = itemKb_9.Value
            End If

            setDispStr(iraiTousuu.Value, jR.DoujiIraiTousuu)
            setDispStr(cboTatemonoYouto.SelectedValue, jR.TatemonoYoutoNo)
            setDispStr(kosuu.Value, jR.Kosuu)

            '調査会社等
            Dim tmpTys = tyousakaisyaCd.Value
            If tmpTys.Length >= 6 Then   '長さ6以上必須
                jR.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2)
                jR.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2)
            End If
            setDispStr(chousaHouhouCode.Value, jR.TysHouhou)
            setDispStr(cboTyousaGaiyou.SelectedValue, jR.TysGaiyou)
            setDispStr(tyousaJissibi.Value, jR.TysJissiDate)

            ' ReportIf連携用
            setDispStr(cboTyousaHouhou.SelectedItem.Text, jR.TysHouhouMeiIf) ' 調査方法名
            setDispStr(kameitenNm.Value, jR.KameitenMeiIf)      ' 加盟店名
            setDispStr(kameitenTel.Value, jR.KameitenTelIf)     ' 加盟店電話番号
            setDispStr(kameitenFax.Value, jR.KameitenFaxIf)     ' 加盟店FAX番号
            setDispStr(kameitenMail.Value, jR.KameitenMailIf)   ' 加盟店メールアドレス
            setDispStr(tyousakaisyaNm.Value, jR.TysKaisyaMeiIf) ' 調査会社名

            ' 地盤連携用
            setDispStr(cboHosyouUmu.Value, jR.HosyouUmu)
            setDispStr(HosyouKikan.Value, jR.HosyouKikan)
            Dim intTmp As Integer = IIf(KojGaisyaSeikyuuUmu.Value = "1", KojGaisyaSeikyuuUmu.Value, Integer.MinValue)
            setDispStr(intTmp, jR.KojGaisyaSeikyuuUmu) '工事会社請求

            '商品セット
            If shouhinCd_1_1.Value <> String.Empty Then
                jR.Syouhin1Record = setSyouhinToTeibetu(1)(1)
            End If
            jR.Syouhin2Records = setSyouhinToTeibetu(2)
            jR.Syouhin3Records = setSyouhinToTeibetu(3)

            '更新ユーザID
            jR.UpdLoginUserId = user_info.LoginUserId

            '更新日付
            If jR.UpdDatetime = Nothing Then
                If updateDateTime.Value = "" Then
                    jR.UpdDatetime = DateTime.MinValue
                Else
                    jR.UpdDatetime = DateTime.ParseExact(updateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
                End If
            End If

            '更新者
            jR.Kousinsya = cbLogic.GetKousinsya(user_info.LoginUserId, DateTime.Now)

            If jR.SyoriKensuu <= 0 Then
                jR.SyoriKensuu = 0
            End If
            jibanRecAfterExe.SyoriKensuu = jR.SyoriKensuu '処理件数のコピー

            '*********************************************************
            '●保証関連の自動設定
            cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.Jutyuu, jR)

            '物件履歴データの自動セット
            Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jR, Me.HiddenHosyouSyouhinUmuOld.Value, Me.HiddenKeikakusyoSakuseiDateOld.Value)

            If Not brRec Is Nothing Then
                '物件履歴レコードのチェック
                Dim strErrMsg As String = String.Empty
                If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                    MLogic.AlertMessage(sender, strErrMsg, 0, "ErrBukkenRireki")
                    Exit Sub
                End If
            End If
            '*********************************************************

            '特別対応レコードリストの取得と設定
            listRec = GetRowCtrlToList(sender)

            ' 地盤テーブル更新を実施
            Dim tourokuResult As Boolean = False
            tourokuResult = jiban_logic.UpdateJibanData(sender, jR, jibanRecAfterExe, brRec, listRec)

            ' 親画面処理へ移行
            RaiseEvent OyaActAtAfterExe(Me, e, iraiSession.Irai2Mode, actMode.Value, exeMode, tourokuResult, jibanRecAfterExe)

        End If

        '削除実行時
        If exeMode = EarthConst.MODE_EXE_SAKUJO Then

            ' 地盤テーブル削除を実施
            Dim deleteResult As Boolean = False
            deleteResult = jiban_logic.DeleteJibanData(sender, kubunVal.Value, hosyousyoNoVal.Value, True, user_info.LoginUserId)
            ' 親画面処理へ移行
            RaiseEvent OyaActAtAfterExe(Me, e, iraiSession.Irai2Mode, actMode.Value, exeMode, deleteResult, jibanRecAfterExe)

        End If


    End Sub

    ''' <summary>
    ''' 画面商品コントロールから邸別請求データに値をセットする（登録/更新処理用）
    ''' </summary>
    ''' <returns>邸別請求データ格納済みHashtable</returns>
    ''' <remarks></remarks>
    Private Function setSyouhinToTeibetu(ByVal targetLN As Integer) As Dictionary(Of Integer, TeibetuSeikyuuRecord)

        Dim tsH As New Dictionary(Of Integer, TeibetuSeikyuuRecord)

        '商品郡の配列から、各商品行の設定を行う
        For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            '各行のコントロールを変数にセット
            ' 設定するコントロールのインスタンスを取得
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

            Dim arrLn As Array = ln.Split("_")   '商品2or3判定用
            Dim shouhinNo As Integer = arrLn(0)
            Dim gamenHyoujiNo As Integer = arrLn(1)

            If targetLN = shouhinNo And ctrlRec.SyouhinCd.Value <> "" Then
                Dim tsR As New TeibetuSeikyuuRecord
                '区分
                tsR.Kbn = kubunVal.Value
                '番号
                tsR.HosyousyoNo = hosyousyoNoVal.Value
                '分類コード
                tsR.BunruiCd = ctrlRec.BunruiCd.Value
                '画面表示NO
                tsR.GamenHyoujiNo = gamenHyoujiNo
                '商品コード
                tsR.SyouhinCd = ctrlRec.SyouhinCd.Value
                '商品名
                tsR.SyouhinMei = ctrlRec.SyouhinNm.Value
                '税区分
                tsR.ZeiKbn = ctrlRec.ZeiKubun.Value
                '税率
                setDispStr(ctrlRec.ZeiRitu.Value, tsR.Zeiritu)
                '工務店請求税抜金額
                setDispStr(ctrlRec.KoumutenSeikyuuGaku.Value, tsR.KoumutenSeikyuuGaku)
                '実請求税抜金額
                setDispStr(ctrlRec.JituSeikyuuGaku.Value, tsR.UriGaku)
                '消費税額
                setDispStr(ctrlRec.ZeiGaku.Value, tsR.UriageSyouhiZeiGaku)
                '承諾書金額
                setDispStr(ctrlRec.SyoudakusyoKingaku.Value, tsR.SiireGaku)
                '仕入消費税額
                setDispStr(ctrlRec.SiireSyouhizei.Value, tsR.SiireSyouhiZeiGaku)
                '請求書発行日
                setDispStr(ctrlRec.SeikyuusyoHakkouDate.Value, tsR.SeikyuusyoHakDate)
                '売上年月日
                setDispStr(ctrlRec.UriageDate.Value, tsR.UriDate)
                ' 伝票売上年月日(ロジッククラスで自動セット)
                tsR.DenpyouUriDate = DateTime.MinValue
                '請求有無
                setDispStr(ctrlRec.SeikyuuUmu.Value, tsR.SeikyuuUmu)
                '見積書作成日
                setDispStr(ctrlRec.MitumoriDate.Value, tsR.TysMitsyoSakuseiDate)
                '発注書確定
                setDispStr(ctrlRec.HattyuusyoKakutei.Value, tsR.HattyuusyoKakuteiFlg)
                '発注書金額
                setDispStr(ctrlRec.HattyuusyoKingaku.Value, tsR.HattyuusyoGaku)
                '発注書確認日
                setDispStr(ctrlRec.HattyuusyoKakuninbi.Value, tsR.HattyuusyoKakuninDate)
                If shouhinNo = 3 Then '商品３
                    '確定区分
                    setDispStr(ctrlRec.KakuteiKbn.Value, tsR.KakuteiKbn)
                Else
                    '確定区分
                    tsR.KakuteiKbn = Integer.MinValue
                End If
                ' EARTH編集外項目
                '売上計上FLG
                setDispStr(ctrlRec.UriageKeijyouFlg.Value, tsR.UriKeijyouFlg)
                '売上計上日
                setDispStr(ctrlRec.UriageKeijyouBi.Value, tsR.UriKeijyouDate)
                '備考
                setDispStr(ctrlRec.Bikou.Value, tsR.Bikou)
                '一括入金FLG
                setDispStr(ctrlRec.IkkatuNyuukinFlg.Value, tsR.IkkatuNyuukinFlg)

                '請求先/仕入先のセット
                ctrlRec.SeikyuuSiireLink.SetTeibetuRecFromSeikyuuSiireLink(tsR)

                'データ取得時点での邸別請求テーブル.更新日時の保持値（排他チェック用）
                If ctrlRec.UpdDatetime.Value = "" Then
                    tsR.UpdDatetime = DateTime.MinValue
                Else
                    tsR.UpdDatetime = DateTime.ParseExact(ctrlRec.UpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
                End If

                '邸別請求レコードをハッシュにセット
                tsH.Add(gamenHyoujiNo, tsR)
            End If
        Next

        Return tsH

    End Function

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（オーバーライド）
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getDispStr(ByVal str As Object) As String

        Return ComLog.GetDisplayString(str)

    End Function

    ''' <summary>
    ''' 画面表示用文字列を特定の型に変換するファンクション（オーバーライド）
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setDispStr(ByVal str As Object, ByRef retObj As Object) As Boolean

        If ComLog.SetDisplayString(str, retObj) Then
            Return True
        Else
            Return False
        End If

    End Function

#Region "商品１，２，３の活性制御"
    ''' <summary>
    ''' 依頼タブの請求データ（商品コード1）の入力制御処理
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub subEnableSeikyuu1()

        '編集モード以外の場合、処理を抜ける
        If iraiSession.Irai2Mode <> EarthConst.MODE_EDIT Or flgIraiGyoumuKengen = False Then
            Exit Sub
        End If

        '指定行の商品コントロールを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo("1_1")

        'JibanSessionManagerのgetSessionsを用いて、商品行を参照設定にする。
        Dim ht As New Hashtable
        jSM.Hash2Ctrl(ctrlRec.SyouhinLine, EarthConst.MODE_VIEW, ht)

        ' 売上状況を判定
        If uriageJyoukyou1.Value <> "" Then
            '売上計上済みの場合、依頼内容 を参照モードに変更
            jSM.Hash2Ctrl(irai2TBody, EarthConst.MODE_VIEW, iraiSession.Irai2Data)
            '各種検索ボタンを非表示
            kameitenSearch.Visible = False
            tyousakaisyaSearch.Visible = False
            btnIrainaiyouKakutei.Visible = False
        ElseIf ctrlRec.SyouhinCd.Value = "" Then
        Else
            ' 特別対応価格対応
            If ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty Then
                If ctrlRec.SyoudakuHenkouKahi.Value = "" Then
                    ComLog.chgDispSyouhinText(ctrlRec.SyoudakusyoKingaku)               ' 承諾書金額
                End If

                ComLog.chgDispSyouhinPull(ctrlRec.SeikyuuUmu, ctrlRec.SeikyuuUmuSpan)   ' 請求有無

                '請求有無=有りの場合のみ活性化
                If ctrlRec.SeikyuuUmu.Value = "1" Then

                    '工務店請求金額の変更可否
                    If ctrlRec.KoumutenHenkouKahi.Value = "" Then
                        ComLog.chgDispSyouhinText(ctrlRec.KoumutenSeikyuuGaku)
                    End If

                    '実請求税抜金額の変更可否
                    If ctrlRec.JituGakuHenkouKahi.Value = "" Then
                        ComLog.chgDispSyouhinText(ctrlRec.JituSeikyuuGaku)
                    End If

                End If
            End If
        End If

        '商品コード判定
        If ctrlRec.SyouhinCd.Value = "" Then
            ' ドロップダウンを空白にする
            ctrlRec.SeikyuuUmu.Style.Add("display", "none")      ' 請求有無
            ctrlRec.SeikyuuUmuSpan.InnerHtml = ""                ' 請求有無(表示SPAN)
            ctrlRec.HattyuusyoKakutei.Style.Add("display", "none") ' 発注書確定
            ctrlRec.HattyuusyoKakuteiSpan.InnerHtml = ""           ' 発注書確定(表示SPAN)

        ElseIf ctrlRec.SeikyuuUmu.Value = "0" Then
        ElseIf ctrlRec.HattyuusyoKakutei.Value = "1" Then
        Else
            ComLog.chgDispSyouhinText(ctrlRec.HattyuusyoKingaku)   ' 発注書金額
        End If

        '発注書確定判定
        If ctrlRec.HattyuusyoKakuteiOld.Value = "1" Then
        ElseIf ctrlRec.SyouhinCd.Value = "" Then
        Else
            ' 発注書確定済の場合、変更不可（EARTH仕様）
            If ctrlRec.HattyuusyoKakuteiOld.Value <> "1" Then
                ComLog.chgDispSyouhinPull(ctrlRec.HattyuusyoKakutei, ctrlRec.HattyuusyoKakuteiSpan)   ' 発注書確定
            End If

        End If

    End Sub

    ''' <summary>
    ''' 依頼タブの請求データ（商品コード2,3）の入力制御処理
    ''' </summary>
    ''' <param name="ln">商品の位置</param>
    ''' <remarks></remarks>
    Public Sub subEnableSeikyuu23(ByVal ln As String)

        Dim lgcJiban As New JibanLogic

        '編集モード以外の場合、処理を抜ける
        If iraiSession.Irai2Mode <> EarthConst.MODE_EDIT Or flgIraiGyoumuKengen = False Then
            Exit Sub
        End If

        If ln.Split("_")(0) <> "2" And ln.Split("_")(0) <> "3" Then
            ' 商品2,3以外は処理しない
            Exit Sub
        End If

        Dim syouhin_type As Integer = IIf(ln.Split("_")(0) = "2", 2, 3)

        '指定行の商品コントロールを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

        Dim uriage_2 As String = ""
        Dim uriage_3 As String = ""

        If ln.Split("_")(0) = "2" Then
            uriage_2 = IIf(uriageJyoukyou1.Value Is Nothing, "", uriageJyoukyou1.Value)
        Else
            uriage_3 = IIf(ctrlRec.UriageJyoukyou.Value Is Nothing, "", ctrlRec.UriageJyoukyou.Value)
        End If

        ' 売上状況を取得
        Dim uriage_jyoukyou As String = IIf(syouhin_type = 2, uriage_2, uriage_3)

        'JibanSessionManagerのgetSessionsを用いて、商品行を参照設定にする。
        Dim ht As New Hashtable
        jSM.Hash2Ctrl(ctrlRec.SyouhinLine, EarthConst.MODE_VIEW, ht)

        ctrlRec.ShouhinSearchBtn.Disabled = True    ' 商品検索ボタン

        ' 売上状況を判定
        If uriage_jyoukyou <> "" Then
        ElseIf ctrlRec.SyouhinCd.Value = "" Then
            ComLog.chgDispSyouhinText(ctrlRec.SyouhinCd)                            ' 商品コード
            ctrlRec.ShouhinSearchBtn.Disabled = False                               ' 商品検索ボタン
            If syouhin_type = 3 Then
                ctrlRec.KakuteiKbn.Disabled = False
            End If
        Else
            ' 商品３のみ確定フラグを判定
            If syouhin_type = 3 Then
                If ctrlRec.KakuteiKbnOld.Value <> "1" Then
                    ComLog.chgDispSyouhinPull(ctrlRec.KakuteiKbn, ctrlRec.KakuteiKbnSpan) ' 確定区分
                End If
            End If

            ' 特別対応価格対応
            If ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value = "" Then
                ComLog.chgDispSyouhinText(ctrlRec.SyouhinCd)                            ' 商品コード
                ctrlRec.ShouhinSearchBtn.Disabled = False                               ' 商品検索ボタン
                ComLog.chgDispSyouhinText(ctrlRec.SyoudakusyoKingaku)                   ' 承諾書金額
                ComLog.chgDispSyouhinPull(ctrlRec.SeikyuuUmu, ctrlRec.SeikyuuUmuSpan)   ' 請求有無

                ' 請求有無の判定
                If ctrlRec.SeikyuuUmu.Value = "1" Then
                    ComLog.chgDispSyouhinText(ctrlRec.JituSeikyuuGaku) ' 実請求金額

                    If ctrlRec.SyouhinSeikyuuSakiCd.Value <> kameitenCd.Value And _
                       lgcJiban.GetKeiretuFlg(keiretuCd.Value) = 0 Then

                    Else
                        ComLog.chgDispSyouhinText(ctrlRec.KoumutenSeikyuuGaku) ' 工務店請求額
                    End If
                End If
            End If

        End If

        '商品コード判定
        If ctrlRec.SyouhinCd.Value = "" Then
            ' 商品コード空白時はドロップダウン部を空白にする
            ctrlRec.SeikyuuUmu.Style.Add("display", "none")         ' 請求有無
            ctrlRec.SeikyuuUmuSpan.InnerHtml = ""                   ' 請求有無(SPAN)
            ctrlRec.HattyuusyoKakutei.Style.Add("display", "none")  ' 発注書確定
            ctrlRec.HattyuusyoKakuteiSpan.InnerHtml = ""            ' 発注書確定(SPAN)
            ' 商品３のみ確定区分
            If syouhin_type = 3 Then
                ctrlRec.KakuteiKbn.Style.Add("display", "none")     ' 確定区分
                ctrlRec.KakuteiKbnSpan.InnerHtml = ""               ' 確定区分(SPAN)
            End If
        ElseIf ctrlRec.SeikyuuUmu.Value = "0" Then
        ElseIf ctrlRec.HattyuusyoKakutei.Value = "1" Then
        Else
            ComLog.chgDispSyouhinText(ctrlRec.HattyuusyoKingaku)           ' 発注書金額
        End If

        '発注書確定判定
        If ctrlRec.HattyuusyoKakuteiOld.Value = "1" Then
        ElseIf ctrlRec.SyouhinCd.Value = "" Then
        Else
            ' 発注書確定済の場合、変更不可（EARTH仕様）
            If ctrlRec.HattyuusyoKakuteiOld.Value <> "1" Then
                ComLog.chgDispSyouhinPull(ctrlRec.HattyuusyoKakutei, ctrlRec.HattyuusyoKakuteiSpan) ' 確定区分
            End If
        End If

    End Sub

#End Region

#Region "商品3確定"
    ''' <summary>
    ''' 商品3確定変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub syouhin3KakuteiHenkou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '画面の情報を設定する
        Dim line_no As String = syouhin3KakuteiHenkouLineNo.Value

        ' 商品3の場合のみ処理を行う
        If line_no.Split("_")(0) <> "3" Then
            Exit Sub
        End If

        '指定行の商品コントロールを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(line_no)

        ' 請求書発行日、売上年月日を設定します
        setHakkoubi(ctrlRec)

    End Sub
#End Region

    ''' <summary>
    ''' 商品３の請求年月日，売上年月日を設定します
    ''' </summary>
    ''' <param name="ctrlRec">商品のコントロールレコード</param>
    ''' <remarks></remarks>
    Private Sub setHakkoubi(ByRef ctrlRec As SyouhinCtrlRecord)
        ' 売上・請求年月日取得用ロジッククラス
        Dim logic As New JibanLogic
        ' 値を取得する為の邸別請求レコード
        Dim teibetu_rec As New TeibetuSeikyuuRecord

        ' 確定区分
        ComLog.SetDisplayString(ctrlRec.KakuteiKbn.Value, _
                                                teibetu_rec.KakuteiKbn)
        ' 請求書発行日
        ComLog.SetDisplayString(ctrlRec.SeikyuusyoHakkouDate.Value, _
                                                teibetu_rec.SeikyuusyoHakDate)
        ' 売上年月日
        ComLog.SetDisplayString(ctrlRec.UriageDate.Value, _
                                                teibetu_rec.UriDate)
        ' 請求有無
        ComLog.SetDisplayString(ctrlRec.SeikyuuUmu.Value, _
                                                teibetu_rec.SeikyuuUmu)
        ' 区分
        teibetu_rec.Kbn = kubunVal.Value
        ' 商品コード
        ComLog.SetDisplayString(ctrlRec.SyouhinCd.Value, _
                                                teibetu_rec.SyouhinCd)

        '請求先情報のセット
        teibetu_rec.SeikyuuSakiCd = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiCd.Value
        teibetu_rec.SeikyuuSakiBrc = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiBrc.Value
        teibetu_rec.SeikyuuSakiKbn = ctrlRec.SeikyuuSiireLink.AccSeikyuuSakiKbn.Value

        ' 売上・請求年月日取得
        logic.SubChangeKakutei(kameitenCd.Value, teibetu_rec)

        ' 請求書発行日
        ctrlRec.SeikyuusyoHakkouDate.Value = _
        ComLog.GetDisplayString(teibetu_rec.SeikyuusyoHakDate)

        ' 売上年月日
        ctrlRec.UriageDate.Value = _
        ComLog.GetDisplayString(teibetu_rec.UriDate)

    End Sub

#Region "商品2,3請求有無変更"
    ''' <summary>
    ''' 商品2,3請求有無変更処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub syouhin23SeikyuuUmuHenkou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '画面の情報を設定する
        Dim line_no As String = syouhin23SeikyuuUmuHenkouLineNo.Value

        '指定行の商品コントロールを取得
        Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(line_no)

        '有無しか無いが、空白が登場してもいいように既存と同じく空白は無にする
        If ctrlRec.SeikyuuUmu.Value = "" Then
            ctrlRec.SeikyuuUmu.Value = "0"
        End If

        If ctrlRec.SeikyuuUmu.Value = "1" Then
            ' 商品２，３の設定処理
            setSyouhinCd23(sender, line_no, ctrlRec.SyouhinCd.Value)
        Else
            ctrlRec.KoumutenSeikyuuGaku.Value = "0"
            ctrlRec.JituSeikyuuGaku.Value = "0"
            ctrlRec.ZeiGaku.Value = "0"
            ctrlRec.ZeiKomiGaku.Value = "0"
            ctrlRec.SeikyuusyoHakkouDate.Value = ""
            If line_no.Split("_")(0) = "2" Then
                ctrlRec.UriageDate.Value = uriageDate_1_1.Value ' 売上年月日は商品１の売上日をセット
            End If

            ' 金額再設定
            setKingaku(line_no)

            If line_no.Split("_")(0) = "2" Then

                ' 商品２の場合は１の制御も実行
                subEnableSeikyuu1()
                For Each shn As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
                    If shn.Split("_")(0) = 2 Then
                        subEnableSeikyuu23(shn)
                    End If
                Next
            Else
                For Each shn As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
                    If shn.Split("_")(0) = 3 Then
                        subEnableSeikyuu23(shn)
                    End If
                Next
            End If
        End If

        ' 商品３の場合のみ日付設定
        If line_no.Split("_")(0) = "3" Then
            ' 請求書発行日、売上年月日を設定します
            setHakkoubi(ctrlRec)
        End If

        '請求有無にフォーカスセット
        setFocus(ctrlRec.SeikyuuUmu)

    End Sub
#End Region

    ''' <summary>
    ''' 依頼内容確定 -> 商品１、２設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnIrainaiyouKakutei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIrainaiyouKakutei.ServerClick

        Dim strErrMsg As String = String.Empty

        '確認モードオン(SDS自動設定オフ)
        Me.HiddenModeKakunin.Value = "1"

        If checkInput(False, False) Then
            '入力チェックがOKであれば、画面を切替える
            '確定フラグをセット
            flgKakutei.Value = "1"
            '商品セット処理実行
            actCtrlId.Value = btnIrainaiyouKakutei.ClientID
            btnSetSyouhin1_ServerClick(sender, e)

            If shouhinCd_1_1.Value = String.Empty OrElse flgShouhin1ClrOK = False Then
                '商品１が取得できなかった場合、エラーメッセージを表示する。
                '発注書金額設定済みでクリアできなかった場合も、エラーとする。
                '確定フラグをセット（未確定に戻す）
                flgKakutei.Value = "0"
                strErrMsg = cbLogic.GetSyouhin1ErrMsg(intSetSts)
                MLogic.AlertMessage(sender, strErrMsg, 0, "hin1SetErrorAtKakutei")
                jSM.Hash2Ctrl(irai2TBody2, EarthConst.MODE_VIEW, iraiSession.Irai2Data)
            Else
                Dim notTargets As New Hashtable
                'コンテキストに現在の画面情報を格納
                jSM.Ctrl2Hash(Me, iraiSession.Irai2Data)

                '現在のページを再読込
                Dim stVal As String = iraiSession.IraiST

                '新規受注モード
                If iraiSession.IraiST = EarthConst.MODE_NEW Then
                    '特別対応データデフォルト登録
                    If Me.InsTokubetuTaiouDefault Then
                    Else
                        '特別対応データが登録できなかった場合、エラーメッセージを表示する。
                        '確定フラグをセット（未確定に戻す）
                        flgKakutei.Value = "0"
                        MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "特別対応データの登録"), 0, "hin1SetErrorAtKakutei")
                        jSM.Hash2Ctrl(irai2TBody2, EarthConst.MODE_VIEW, iraiSession.Irai2Data)
                        Exit Sub
                    End If

                End If

                If iraiSession.IraiST = EarthConst.MODE_NEW Or iraiSession.IraiST = EarthConst.MODE_NEW_EDIT Then
                    stVal = EarthConst.MODE_NEW_EDIT
                End If
                iraiSession.IraiST = IIf(stVal Is Nothing, String.Empty, stVal)
                Context.Items("irai") = iraiSession
                Server.Transfer(UrlConst.IRAI_STEP_2)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 依頼内容確定解除 -> 依頼内容再設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnIrainaiyouKaijo_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIrainaiyouKaijo.ServerClick

        If checkInput(False, False) Then
            '入力チェックがOKであれば、画面を切替える
            '確定フラグをセット
            flgKakutei.Value = "0"
            'コンテキストに現在の画面情報を格納
            jSM.Ctrl2Hash(Me, iraiSession.Irai2Data)

            '現在のページを再読込
            Dim stVal As String = iraiSession.IraiST
            If iraiSession.IraiST = EarthConst.MODE_NEW Or iraiSession.IraiST = EarthConst.MODE_NEW_EDIT Then
                stVal = EarthConst.MODE_NEW_EDIT
            End If
            iraiSession.IraiST = IIf(stVal Is Nothing, String.Empty, stVal)
            Context.Items("irai") = iraiSession
            Server.Transfer(UrlConst.IRAI_STEP_2)
        End If

    End Sub

#Region "保証商品有無対応"

    ''' <summary>
    ''' 画面で変更された商品を元に、保証商品有無を判定する(商品１～３)
    ''' </summary>
    ''' <returns>保証有無(1:保証あり,0:保証なし)</returns>
    ''' <remarks></remarks>
    Private Function ChkChgSyouhinHosyouUmu() As String
        Dim jR As New JibanRecordBase
        Dim strRet As String = String.Empty
        Dim strSyouhinCds As String = String.Empty 'セパレータ区切り

        '商品1
        If Me.shouhinCd_1_1.Value <> String.Empty Then
            strSyouhinCds &= Me.shouhinCd_1_1.Value & EarthConst.SEP_STRING
        End If

        '商品2,3の取得
        jR.Syouhin2Records = setSyouhinToTeibetu(2)
        jR.Syouhin3Records = setSyouhinToTeibetu(3)

        '商品2
        If Not jR.Syouhin2Records Is Nothing Then
            For intCnt As Integer = 1 To EarthConst.SYOUHIN2_COUNT
                If jR.Syouhin2Records.ContainsKey(intCnt) Then
                    strSyouhinCds &= jR.Syouhin2Records.Item(intCnt).SyouhinCd() & EarthConst.SEP_STRING
                End If
            Next
        End If

        '商品3
        If Not jR.Syouhin3Records Is Nothing Then
            For intCnt As Integer = 1 To EarthConst.SYOUHIN3_COUNT
                If jR.Syouhin3Records.ContainsKey(intCnt) Then
                    strSyouhinCds &= jR.Syouhin3Records.Item(intCnt).SyouhinCd() & EarthConst.SEP_STRING
                End If
            Next
        End If

        'いずれかの商品がセットされている場合、保証有無を判定
        If strSyouhinCds <> String.Empty Then
            strRet = cbLogic.ChkChgSyouhinHosyouSyouhinUmu(strSyouhinCds)
        End If

        Return strRet
    End Function

    ''' <summary>
    ''' 商品1～3の保証ありなし状況と、保証書発行状況の保証有無を判定する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckInputHosyouSyouhinUmu() As Boolean

        '●保証書発行状況
        Dim strHosyouSyouhinUmu As String = Me.ChkChgSyouhinHosyouUmu()
        Dim strHosyousyoHakJykyHosyouUmu As String = cbLogic.GetHosyousyoHakJykyHosyouUmu(Me.HiddenHosyousyoHakkouJyoukyouMoto.Value)

        If strHosyouSyouhinUmu <> String.Empty And strHosyousyoHakJykyHosyouUmu <> String.Empty Then
            '商品情報の保証商品有無と、保証書発行状況の保証商品有無が異なる場合、エラー
            If strHosyouSyouhinUmu <> strHosyousyoHakJykyHosyouUmu Then
                Return False
            End If
        End If

        Return True
    End Function

#End Region

#Region "特別対応"

    ''' <summary>
    ''' 特別対応のKEY情報をセットし、レコードクラスにセットする
    ''' ※区分/番号/加盟店コード/商品コード１/調査方法NO
    ''' </summary>
    ''' <returns>特別対応レコードクラス</returns>
    ''' <remarks></remarks>
    Private Function SetTokubetuTaiouKeyInfo() As TokubetuTaiouRecordBase
        Dim tRec As New TokubetuTaiouRecordBase

        With tRec
            ComLog.SetDisplayString(Me.kubunVal.Value, .Kbn)
            ComLog.SetDisplayString(Me.hosyousyoNoVal.Value, .HosyousyoNo)
            ComLog.SetDisplayString(Me.kameitenCd.Value, .AitesakiCd)
            ComLog.SetDisplayString(Me.choSyouhin1.SelectedValue, .SyouhinCd)
            ComLog.SetDisplayString(Me.chousaHouhouCode.Value, .TysHouhouNo)
        End With

        Return tRec
    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(特別対応)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCtrlValuesStringTokubetu() As String
        Dim sb As New StringBuilder

        sb.Append(Me.kameitenCd.Value & EarthConst.SEP_STRING)                  '加盟店コード
        sb.Append(Me.choSyouhin1.SelectedValue & EarthConst.SEP_STRING)         '商品1コード
        sb.Append(Me.cboTyousaHouhou.SelectedValue & EarthConst.SEP_STRING)     '調査方法NO

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 特別対応データ/デフォルトINSERT処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>アクション：
    ''' ・依頼内容確定ボタン押下時
    ''' ・Step2初期読込時(各ボタン押下後に呼出時)
    ''' </remarks>
    Private Function InsTokubetuTaiouDefault() As Boolean
        Dim blnRet As Boolean = True

        '特別対応データデフォルト登録(保証書NO採番処理でのみフラグを立てている)
        If HiddenInsTokubetuTaiouFlg.Value = "1" Then

            If MyLogic.insertTokubetuTaiouUI(Me, _
                                           Me.kubunVal.Value, _
                                           Me.hosyousyoNoVal.Value, _
                                           Me.kameitenCd.Value, _
                                           Me.shouhinCd_1_1.Value, _
                                           Me.chousaHouhouCode.Value, _
                                           user_info.LoginUserId, _
                                           DateTime.Now, _
                                           Me.HiddenRentouBukkenSuu.Value) Then

                HiddenInsTokubetuTaiouFlg.Value = "@"

                '確認画面・特別対応価格反映用フラグ
                Me.HiddenTokutaiKkkHaneiFlg.Value = "0"     '商品1は既存通りに全商品特別価格反映
                Me.HiddenTokutaiKkkHaneiFlgPu.Value = "0"     '商品1は既存通りに全商品特別価格反映

                '●依頼内容確定時に登録成功したら、それを正として扱う(DB値ではなく)
                Me.HiddenKakuteiValuesTokubetu.Value = getCtrlValuesStringTokubetu()
            Else
                blnRet = False
            End If
        End If

        Return blnRet
    End Function

    ''' <summary>
    ''' 特別対応データを取得する(ツールチップ表示用)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub GetTokubetuTaiouCd(ByVal sender As Object)

        Dim intCnt As Integer = 0
        Dim ttList As New List(Of TokubetuTaiouRecordBase)

        '区分、保証書NOをキーに特別対応データを取得
        ttList = tLogic.GetTokubetuTaiouDataInfo(sender, kubunVal.Value, hosyousyoNoVal.Value, String.Empty, intCnt)

        '振分け処理
        Me.DevideTokubetuTaiouCd(sender, ttList, False)

    End Sub

    ''' <summary>
    ''' 特別対応データのリストを画面の各ツールチップに振り分ける
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ttList">特別対応レコードのリスト</param>
    ''' <remarks></remarks>
    Private Sub DevideTokubetuTaiouCd(ByVal sender As Object, ByVal ttList As List(Of TokubetuTaiouRecordBase), _
                                      ByVal chkTorikesiFlg As Boolean)

        Dim recTmp As New TokubetuTaiouRecordBase       '作業用
        Dim strTokubetuTaiouCd As String = String.Empty '特別対応コード
        Dim strResult As String                         '振分先
        Dim emType As EarthEnum.emToolTipType           'ツールチップ表示タイプ

        '初期化
        Me.ClearToolTipValue()

        If Not ttList Is Nothing Then
            For Each recTmp In ttList

                '特別対応コードを作業用に取得
                strTokubetuTaiouCd = ComLog.GetDisplayString(recTmp.TokubetuTaiouCd)

                '振分け先を取得
                strResult = cbLogic.checkDevideTaisyou(sender, recTmp, chkTorikesiFlg)

                '振分け先に特別対応コードを追加
                If strResult <> String.Empty Then
                    Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(strResult)

                    If ctrlRec.SyouhinCd.Value <> String.Empty Then
                        'ツールチップ設定対象かチェック
                        emType = cbLogic.checkToolTipSetValue(sender, recTmp, ctrlRec.BunruiCd.Value, strResult.Split("_")(1), ctrlRec.UriageKeijyouFlg.Value)

                        'ツールチップHiddenに特別対応コードを格納
                        If emType <> EarthEnum.emToolTipType.NASI Then
                            ctrlRec.TokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)

                            '修正ありの場合、ラベル表示を変更
                            If emType = EarthEnum.emToolTipType.SYUSEI Then
                                ctrlRec.TokubetuTaiouToolTip.AcclblTokubetuTaiou.Text = EarthConst.SYUU_TOOL_TIP
                            End If
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 特別対応データのリスト（変更情報）を各ツールチップのHiddenに振り分ける
    ''' </summary>
    ''' <param name="ttList"></param>
    ''' <remarks></remarks>
    Private Sub DevideTokubetuTaiouHidden(ByVal sender As System.Object, ByVal ttList As List(Of TokubetuTaiouRecordBase))

        Dim recTmp As New TokubetuTaiouRecordBase
        Dim strResult As String = String.Empty

        '初期化
        ClearToolTipHiddenValue()

        If Not ttList Is Nothing Then
            '特別対応リスト分処理を繰り返す
            For Each recTmp In ttList
                If recTmp.UpdFlg <> "1" Then
                    '変更対象ではない場合は、処理を飛ばす
                    Continue For
                End If

                '振り分け先を取得
                strResult = cbLogic.DevideTokubetuCd(sender, recTmp.BunruiCd, recTmp.GamenHyoujiNo)

                '振分け先のHiddenに追加
                If strResult <> String.Empty Then
                    Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(strResult)

                    '更新日時
                    If ctrlRec.TokubetuTaiouToolTip.AccUpdDatetime.Value = String.Empty Then
                        ctrlRec.TokubetuTaiouToolTip.AccUpdDatetime.Value = IIf(recTmp.UpdDatetime = Date.MinValue, "", Format(recTmp.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
                    Else
                        ctrlRec.TokubetuTaiouToolTip.AccUpdDatetime.Value &= EarthConst.SEP_STRING & IIf(recTmp.UpdDatetime = Date.MinValue, "", Format(recTmp.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
                    End If

                    '変更対象
                    If ctrlRec.TokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty Then
                        ctrlRec.TokubetuTaiouToolTip.AccTaisyouCd.Value = ComLog.GetDisplayString(recTmp.TokubetuTaiouCd)
                    Else
                        ctrlRec.TokubetuTaiouToolTip.AccTaisyouCd.Value &= EarthConst.SEP_STRING & ComLog.GetDisplayString(recTmp.TokubetuTaiouCd)
                    End If

                End If

            Next
        End If

    End Sub

    ''' <summary>
    ''' 更新用の特別対応情報リストを画面から取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToList(ByVal sender As System.Object) As List(Of TokubetuTaiouRecordBase)

        Dim listRec As New List(Of TokubetuTaiouRecordBase)
        Dim jibanRec As New JibanRecord
        Dim intCntCtrl As Integer = 0
        Dim lgcTokuTaiMst As New TokubetuTaiouMstLogic
        Dim lgcTokuTai As New TokubetuTaiouLogic
        Dim dtRec As New TokubetuTaiouRecordBase
        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim intTokuTaiCnt As Integer = 0          '特別対応マスタ件数用カウンタ
        Dim intMaxCnt As Integer = 0
        Dim arrTokuCd() As String = Nothing
        Dim arrTokuUpdDatetime() As String = Nothing
        Dim lgcIkkatu As New IkkatuHenkouTysSyouhinLogic
        Dim strTaisyouCd As String = String.Empty
        Dim blnFindFlg As Boolean = False
        Dim dicTokuCd As New Dictionary(Of String, String)

        '地盤データの取得
        jibanRec = GetCtrlFromJibanRec(Me)

        '画面Hidden値から更新対象となるコードを取得
        strTaisyouCd = Me.HiddenChgTokuCd.Value         '特別対応コード群($$$区切り)
        arrTokuCd = lgcIkkatu.getArrayFromDollarSep(strTaisyouCd, True)
        arrTokuUpdDatetime = lgcIkkatu.getArrayFromDollarSep(Me.HiddenChgTokuUpdDatetime.Value, True)

        '更新対象が無い場合には、処理を抜ける
        If String.IsNullOrEmpty(strTaisyouCd) OrElse arrTokuCd.Length = 0 Then
            Return listRec
            Exit Function
        End If

        '更新対象群をDictionaryに格納
        If arrTokuCd.Length <> 0 AndAlso arrTokuUpdDatetime.Length <> 0 _
            AndAlso arrTokuCd.Length = arrTokuUpdDatetime.Length Then
            '【特別対応コード(KEY)：更新日時(VALUE)】で辞書格納
            For intDicCnt As Integer = 0 To arrTokuCd.Length - 1
                dicTokuCd.Add(arrTokuCd(intDicCnt), arrTokuUpdDatetime(intDicCnt))
            Next
        End If

        '特別対応マスタベースの特別対応データをDBから取得(対象コードのみ)
        listRec = lgcTokuTai.GetTokubetuTaiouDataInfo(sender, _
                                                     jibanRec.Kbn, _
                                                     jibanRec.HosyousyoNo, _
                                                     strTaisyouCd, _
                                                     intTokuTaiCnt)
        '処理件数チェック
        If intTokuTaiCnt <= 0 Then
            Return listRec
            Exit Function
        End If

        '更新対象群を元に画面から有る・無し情報を設定
        For intMainCnt As Integer = 0 To listRec.Count - 1

            '画面より発見FLGを初期化（無ければ親Hidddenから設定）
            blnFindFlg = False

            '画面情報すべてより特別対応コード郡を取得する
            For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
                '商品行１つを取得
                Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

                '特別対応ツールチップから情報を取得(特別対応コード)
                If ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value <> String.Empty Then

                    '複数の特別対応コードの場合があるので、$$$区切りで切り分ける
                    Dim arrDisplayCd() As String = Split(ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value, EarthConst.SEP_STRING)

                    '1商品・1ツールチップの中から該当のコードがあるか走査
                    For i As Integer = 0 To arrDisplayCd.Length - 1

                        '更新リストと画面がヒットしたら画面情報を上書き
                        If listRec(intMainCnt).TokubetuTaiouCd = arrDisplayCd(i) Then
                            Dim arrLn As Array = ln.Split("_")
                            '分類コード
                            listRec(intMainCnt).BunruiCd = ctrlRec.BunruiCd.Value
                            '画面表示No
                            listRec(intMainCnt).GamenHyoujiNo = arrLn(1)
                            '更新フラグ(ここのタイミングではすべて更新対象となるため)
                            listRec(intMainCnt).UpdFlg = "1"
                            '金額加算商品コード(=画面.邸別の商品コード)
                            listRec(intMainCnt).KasanSyouhinCd = ctrlRec.SyouhinCd.Value
                            '更新ログインユーザ
                            listRec(intMainCnt).UpdLoginUserId = user_info.LoginUserId
                            '更新日時
                            listRec(intMainCnt).UpdDatetime = DateTime.ParseExact(dicTokuCd(listRec(intMainCnt).TokubetuTaiouCd), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                            '削除フラグ
                            listRec(intMainCnt).DeleteFlg = False

                            '発見FLGをON
                            blnFindFlg = True
                        End If
                    Next
                End If
            Next

            '画面に無い場合は、親Hdnより紐付け解除扱いにする
            If blnFindFlg = False Then
                '分類コード（DBで削除するので変更しない）
                '画面表示No（DBで削除するので変更しない）
                '更新フラグ(ここのタイミングではすべて更新対象となるため)
                listRec(intMainCnt).UpdFlg = "1"
                '更新ログインユーザ
                listRec(intMainCnt).UpdLoginUserId = user_info.LoginUserId
                '更新日時
                listRec(intMainCnt).UpdDatetime = DateTime.ParseExact(dicTokuCd(listRec(intMainCnt).TokubetuTaiouCd), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                '削除フラグ
                listRec(intMainCnt).DeleteFlg = True
            End If

        Next

        Return listRec

    End Function

    ''' <summary>
    ''' 邸別商品に対して特別対応価格対応処理を行う
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Function calcTeibetuTokubetuKkk(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnSyouhin1Henkou As Boolean) As Boolean

        Dim jibanRec As New JibanRecord
        Dim dtRec As New TokubetuTaiouRecordBase
        Dim listRes As New List(Of TokubetuTaiouRecordBase)
        Dim lgcTokuTaiMM As New TokubetuTaiouMstLogic
        Dim intTTMstCnt As Integer = 0          '特別対応マスタ件数用カウンタ
        Dim intListCnt As Integer = 0           '特別対応価格反映処理件数カウンタ
        Dim intTmpKingakuAction As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION

        '地盤データの取得
        jibanRec = GetCtrlFromJibanRec(Me)

        With jibanRec
            If Not jibanRec Is Nothing AndAlso Not .Syouhin1Record Is Nothing Then
                '特別対応マスタベースの特別対応データを取得(DB値)
                listRes = lgcTokuTaiMM.GetTokubetuTaiouInfo(sender, _
                                                             .Kbn, _
                                                             .HosyousyoNo, _
                                                             .KameitenCd, _
                                                             .Syouhin1Record.SyouhinCd, _
                                                             .TysHouhou, _
                                                             intTTMstCnt)
            End If
        End With

        '処理件数チェック
        If intTTMstCnt <= 0 Then
            Exit Function
        End If

        '特別対応リスト内を走査し、画面情報を追記する（画面表示No,分類コード）
        For intListCnt = 0 To listRes.Count - 1

            '編集画面で商品1変更があった場合は、更新FLGを再設定
            If blnSyouhin1Henkou AndAlso listRes(intListCnt).SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_1 Then
                listRes(intListCnt).UpdFlg = 1
            End If

            '特別対応リストに画面情報を最新で上書き
            If listRes(intListCnt).UpdFlg = 1 AndAlso listRes(intListCnt).KkkSyoriFlg <> 1 Then
                setGamenNoBunruiCd(listRes(intListCnt))
            End If
        Next

        '最新の特別対応データを基に、(地盤レコード内)邸別請求データへの反映を行なう
        '●特別対応価格反映処理：TODO
        intTmpKingakuAction = cbLogic.pf_ChkTokubetuTaiouKkk(sender, listRes, jibanRec, blnSyouhin1Henkou)
        If intTmpKingakuAction <= EarthEnum.emKingakuAction.KINGAKU_ALERT Then
            MLogic.AlertMessage(sender, Messages.MSG200W.Replace("@PARAM1", cbLogic.AccTokubetuTaiouKkkMsg), 0, "KkkException")
        End If

        'Hidden値へ、変更があった特別対応コードをセットする(登録時確認用）
        calcTeibetuTokubetuKkkAfter(sender, listRes)

        '地盤レコードを画面にセット→特別対応価格で変更があったものだけ画面反映する:TODO
        setSyouhinFromJibanRec(sender, e, jibanRec)
        'レコードによる表示設定処理
        setDispAction(sender, e)

        '特別対応ツールチップ再表示
        DevideTokubetuTaiouCd(sender, listRes, True)
        '特別対応ツールチップのHidden項目を更新（登録時用）
        DevideTokubetuTaiouHidden(sender, listRes)

        Return True
    End Function

    ''' <summary>
    ''' 特別対応コードの更新対象をHiddenに格納
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="listRes"></param>
    ''' <remarks></remarks>
    Private Sub calcTeibetuTokubetuKkkAfter(ByVal sender As System.Object, ByVal listRes As List(Of TokubetuTaiouRecordBase))

        Dim dicTokuCd As New Dictionary(Of String, String)
        Dim arrTokuCd() As String = Nothing
        Dim arrTokuUpdDatetime() As String = Nothing
        Dim strTokuJoinString As String = String.Empty
        Dim lgcIkkatu As New IkkatuHenkouTysSyouhinLogic

        '画面Hidden値から取得
        arrTokuCd = lgcIkkatu.getArrayFromDollarSep(Me.HiddenChgTokuCd.Value, True)
        arrTokuUpdDatetime = lgcIkkatu.getArrayFromDollarSep(Me.HiddenChgTokuUpdDatetime.Value, True)

        'Dictionaryに格納
        If arrTokuCd.Length <> 0 AndAlso arrTokuUpdDatetime.Length <> 0 _
            AndAlso arrTokuCd.Length = arrTokuUpdDatetime.Length Then
            '特別対応コード：更新日時で辞書格納
            For i As Integer = 0 To arrTokuCd.Length - 1
                dicTokuCd.Add(arrTokuCd(i), arrTokuUpdDatetime(i))
            Next
        End If

        'DBの特別対応コードを引き当てる
        If listRes.Count > 0 Then
            '特別対応コードリスト内を走査
            For j As Integer = 0 To listRes.Count - 1
                '更新対象のフラグか判断
                If listRes(j).ChgVal = False Then
                    Continue For
                End If

                '更新日時(特別対応コード1件)
                Dim strDatetime As String = ComLog.GetDisplayString(listRes(j).UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1))

                If Not dicTokuCd.ContainsKey(listRes(j).TokubetuTaiouCd) Then
                    '辞書にキーが無い場合は追加
                    dicTokuCd.Add(listRes(j).TokubetuTaiouCd, strDatetime)
                ElseIf dicTokuCd.ContainsKey(listRes(j).TokubetuTaiouCd) AndAlso _
                    dicTokuCd(listRes(j).TokubetuTaiouCd) <> strDatetime Then
                    'キーがある場合は、更新日時だけ更新
                    dicTokuCd(listRes(j).TokubetuTaiouCd) = strDatetime
                End If
            Next

            '設定した辞書をHdnに退避
            Me.HiddenChgTokuCd.Value = lgcIkkatu.getJoinString(dicTokuCd.Keys.GetEnumerator)
            Me.HiddenChgTokuUpdDatetime.Value = lgcIkkatu.getJoinString(dicTokuCd.Values.GetEnumerator)

        Else
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 特別対応レコードから画面を走査し、画面表示Noと分類コードをセット
    ''' </summary>
    ''' <param name="dtRec"></param>
    ''' <remarks></remarks>
    Private Sub setGamenNoBunruiCd(ByRef dtRec As TokubetuTaiouRecordBase)

        Dim blnFindCd As Boolean = False

        '画面の商品行から、特別対応
        For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            '商品行を1行取得
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

            If Not ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty Then
                'ツールチップに値がある場合、全て取得
                Dim arrTokubetuTaiouCd() As String = Split(ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value, EarthConst.SEP_STRING)

                'ツールチップから特別対応コードを検索
                For i As Integer = 0 To arrTokubetuTaiouCd.Length - 1
                    If arrTokubetuTaiouCd(i).ToString = dtRec.TokubetuTaiouCd.ToString Then
                        'コードがあった行の場合、画面表示Noと分類コードをセット
                        Dim arrLn As Array = ln.Split("_")
                        dtRec.GamenHyoujiNo = arrLn(1)
                        dtRec.BunruiCd = ctrlRec.BunruiCd.Value
                        '発見フラグをON
                        blnFindCd = True
                    End If
                Next
            End If
        Next

        '特別対応コードが画面にいなかった場合は、設定先を初期化
        If blnFindCd = False Then
            dtRec.GamenHyoujiNo = Nothing
            dtRec.BunruiCd = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 画面表示項目から地盤レコードへの値セットを行う
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCtrlFromJibanRec(ByVal sender As Object) As JibanRecord

        Dim jiban_logic As New JibanLogic
        Dim jR As New JibanRecord

        ' 現在の地盤データをDBから取得する
        jR = jiban_logic.GetJibanData(Me.kubunVal.Value, Me.hosyousyoNoVal.Value)

        '***************************************
        ' 地盤データ
        '***************************************
        '加盟店コード
        jR.KameitenCd = Me.kameitenCd.Value

        '調査方法NO
        setDispStr(chousaHouhouCode.Value, jR.TysHouhou)

        '商品セット（1～3）
        If shouhinCd_1_1.Value <> String.Empty Then
            jR.Syouhin1Record = setSyouhinToTeibetu(1)(1)
        End If
        jR.Syouhin2Records = setSyouhinToTeibetu(2)
        jR.Syouhin3Records = setSyouhinToTeibetu(3)

        '更新ユーザID
        jR.UpdLoginUserId = user_info.LoginUserId

        '更新日付
        If jR.UpdDatetime = Nothing Then
            If updateDateTime.Value = "" Then
                jR.UpdDatetime = DateTime.MinValue
            Else
                jR.UpdDatetime = DateTime.ParseExact(updateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If
        End If

        '更新者
        jR.Kousinsya = cbLogic.GetKousinsya(user_info.LoginUserId, DateTime.Now)

        Return jR

    End Function

    ''' <summary>
    ''' 特別対応ツールチップのHiddenを初期化
    ''' </summary>
    ''' <param name="syouhin_type">指定行のみ初期化する場合に指定する</param>
    ''' <remarks></remarks>
    Private Sub ClearToolTipValue(Optional ByVal syouhin_type As String = "")

        If syouhin_type = "" Then
            '全行を初期化
            For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
                '++++++++++++++++++++++++++++++++++++++++++++++++++++
                '商品郡の配列から、特別対応ツールチップの初期化を行なう
                '++++++++++++++++++++++++++++++++++++++++++++++++++++
                '各行のコントロールを変数にセット
                '設定するコントロールのインスタンスを取得
                Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

                ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty
            Next

        Else
            '指定行を初期化
            '設定するコントロールのインスタンスを取得
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(syouhin_type)

            ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 特別対応ツールチップのHiddenを初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearToolTipHiddenValue()

        'すべての商品行の初期化
        For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            '商品行を１行取得
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

            '更新日時
            ctrlRec.TokubetuTaiouToolTip.AccUpdDatetime.Value = String.Empty
            '変更対象
            ctrlRec.TokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty
        Next

    End Sub

    ''' <summary>
    ''' 画面上すべての特別対応ツールチップを更新対象とする
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultTokutaiTooltipReg(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim jibanRec As New JibanRecord
        Dim dicTokuCd As New Dictionary(Of String, String)
        Dim lgcIkkatu As New IkkatuHenkouTysSyouhinLogic
        Dim lgcTokuTaiMM As New TokubetuTaiouMstLogic
        Dim intTTMstCnt As Integer = 0          '特別対応マスタ件数用カウンタ
        Dim listRes As New List(Of TokubetuTaiouRecordBase)

        '地盤データの取得
        jibanRec = GetCtrlFromJibanRec(Me)


        '特別対応マスタベースの特別対応データを取得(DB値)
        listRes = lgcTokuTaiMM.GetTokubetuTaiouInfo(sender, _
                                                     Me.kubunVal.Value, _
                                                     Me.hosyousyoNoVal.Value, _
                                                     Me.kameitenCd.Value, _
                                                     Me.shouhinCd_1_1.Value, _
                                                     Me.chousaHouhouCode.Value, _
                                                     intTTMstCnt)

        '処理件数チェック
        If intTTMstCnt <= 0 Then
            Exit Sub
        End If

        '画面情報すべてより特別対応コード群を取得する
        For Each ln As String In EarthConst.Instance.ARRAY_SHOUHIN_LINES
            '商品行1つを取得
            Dim ctrlRec As SyouhinCtrlRecord = getSyouhinRowInfo(ln)

            '特別対応ツールチップから情報を取得(特別対応コード)
            If ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value <> String.Empty Then

                '複数の特別対応コードの場合があるので、$$$区切りで切り分ける
                Dim arrDisplayCd() As String = Split(ctrlRec.TokubetuTaiouToolTip.AccDisplayCd.Value, EarthConst.SEP_STRING)
                Dim arrUpdDatetime() As String = Split(ctrlRec.TokubetuTaiouToolTip.AccUpdDatetime.Value, EarthConst.SEP_STRING)

                '特別対応コードがある場合に追加
                If arrDisplayCd.Length > 0 Then
                    'ツールチップの特別対応コード分のループ
                    For i As Integer = 0 To arrDisplayCd.Length - 1
                        'DB値のコードリストから走査
                        For j As Integer = 0 To listRes.Count - 1
                            'DB値にあれば、更新日時を画面にセット
                            If arrDisplayCd(i) = listRes(j).TokubetuTaiouCd Then
                                dicTokuCd.Add(arrDisplayCd(i), ComLog.GetDisplayString(listRes(j).UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1)))
                            End If
                        Next
                    Next
                End If
            End If
        Next

        '設定した辞書をHdnに退避(初回のみ動作なので上書きする)
        Me.HiddenChgTokuCd.Value = lgcIkkatu.getJoinString(dicTokuCd.Keys.GetEnumerator)
        Me.HiddenChgTokuUpdDatetime.Value = lgcIkkatu.getJoinString(dicTokuCd.Values.GetEnumerator)

    End Sub

#End Region

End Class