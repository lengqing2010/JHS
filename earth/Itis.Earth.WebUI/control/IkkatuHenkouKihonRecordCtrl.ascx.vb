
Partial Public Class IkkatuHenkouKihonRecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    Dim JLogic As New JibanLogic

#Region "ユーザーコントロールへの外部からのアクセス用Getter/Setter"

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenKbn（区分）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AccHiddenKbn() As HtmlInputHidden
        Get
            Return Me.HiddenKbn
        End Get
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenNo（番号）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AccHiddenNo() As HtmlInputHidden
        Get
            Return Me.HiddenNo
        End Get
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextKokyakuBangou（顧客番号）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AccTextKokyakuBangou() As TextBox
        Get
            Return Me.TextKokyakuBangou
        End Get
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextSesyuMei（施主名）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSesyuMei() As TextBox
        Get
            Return Me.TextSesyuMei
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextSesyuMei
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBukkenMei（受注物件名）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextBukkenMei() As TextBox
        Get
            Return Me.TextBukkenMei
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextBukkenMei
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextTyousaKibouDate（調査希望日）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextTyousaKibouDate() As TextBox
        Get
            Return Me.TextTyousaKibouDate
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextTyousaKibouDate
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextTyousaKibouJikan（調査希望時間）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextTyousaKibouJikan() As TextBox
        Get
            Return Me.TextTyousaKibouJikan
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextTyousaKibouJikan
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBukkenJuusyo1（物件住所1）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextBukkenJyuusyo1() As TextBox
        Get
            Return Me.TextBukkenJyuusyo1
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextBukkenJyuusyo1
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBukkenJuusyo2（物件住所2）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextBukkenJyuusyo2() As TextBox
        Get
            Return Me.TextBukkenJyuusyo2
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextBukkenJyuusyo2
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBukkenJuusyo3（物件住所3）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextBukkenJyuusyo3() As TextBox
        Get
            Return Me.TextBukkenJyuusyo3
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextBukkenJyuusyo3
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBikou（備考）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextBikou() As TextBox
        Get
            Return Me.TextBikou
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextBikou
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBunjouCode（分譲コード）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextBunjouCode() As TextBox
        Get
            Return Me.TextBunjouCode
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextBunjouCode
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextNayoseCode（物件名寄コード）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextNayoseCode() As TextBox
        Get
            Return Me.TextNayoseCode
        End Get
        Set(ByVal value As TextBox)
            value = Me.TextNayoseCode
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for SelectKeiyu（経由）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSelectKeiyu() As DropDownList
        Get
            Return Me.SelectKeiyu
        End Get
        Set(ByVal value As DropDownList)
            value = Me.SelectKeiyu
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for updateDateTime（更新日時）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccupdateDateTime() As HtmlInputHidden
        Get
            Return Me.updateDateTime
        End Get
        Set(ByVal value As HtmlInputHidden)
            value = Me.updateDateTime
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TD調査希望日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>TD調査希望日</remarks>
    Public Property AccTdTyousaKibouDate() As HtmlTableCell
        Get
            Return TdTyousaKibouDate
        End Get
        Set(ByVal value As HtmlTableCell)
            TdTyousaKibouDate = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TD調査希望時間
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>TD調査希望時間</remarks>
    Public Property AccTdTyousaKibouJikan() As HtmlTableCell
        Get
            Return TdTyousaKibouJikan
        End Get
        Set(ByVal value As HtmlTableCell)
            TdTyousaKibouJikan = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TR一行目
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>TR一行目</remarks>
    Public Property AccTrIkkatuHenkouRec1() As HtmlTableRow
        Get
            Return TrIkkatuHenkouRec1
        End Get
        Set(ByVal value As HtmlTableRow)
            TrIkkatuHenkouRec1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TR二行目
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>TR二行目</remarks>
    Public Property AccTrIkkatuHenkouRec2() As HtmlTableRow
        Get
            Return TrIkkatuHenkouRec2
        End Get
        Set(ByVal value As HtmlTableRow)
            TrIkkatuHenkouRec2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TR三行目
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>TR三行目</remarks>
    Public Property AccTrIkkatuHenkouRec3() As HtmlTableRow
        Get
            Return TrIkkatuHenkouRec3
        End Get
        Set(ByVal value As HtmlTableRow)
            TrIkkatuHenkouRec3 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TR四行目
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>TR四行目</remarks>
    Public Property AccTrIkkatuHenkouRec4() As HtmlTableRow
        Get
            Return TrIkkatuHenkouRec4
        End Get
        Set(ByVal value As HtmlTableRow)
            TrIkkatuHenkouRec4 = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            Me.SetDispAction()

        End If

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim checkDate As String = "checkDate(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ***コードおよび検索ポップアップボタン
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '分譲コード
        Me.TextBunjouCode.Attributes("onblur") = "if(checkNumber(this)) checkMinus(this);"
        '施主名（受注物件名へのコピー）
        Me.TextSesyuMei.Attributes("onblur") = "setJyutyuuBukkenMei(objEBI('" & Me.TextBukkenMei.ClientID & "'),this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        ''+++++++++++++++++++++++++++++++++++++++++++++++++++
        ''+ 日付系
        ''+++++++++++++++++++++++++++++++++++++++++++++++++++
        ''***********************
        ''* 改良工事
        ''***********************
        '調査希望日
        Me.TextTyousaKibouDate.Attributes("onblur") = checkDate
        Me.TextTyousaKibouDate.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ドロップダウンリスト
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

    End Sub

#Region "プライベートメソッド"

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

        Dim strErrGyouInfo As String = "顧客番号:" & Me.TextKokyakuBangou.Text & " "

        '●コード入力値変更チェック
        'なし

        '●必須チェック
        '施主名
        If Me.TextSesyuMei.Text = String.Empty Then
            errMess += strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "施主名")
            arrFocusTargetCtrl.Add(Me.TextSesyuMei)
        End If
        '受注物件名
        If Me.TextBukkenMei.Text = String.Empty Then
            errMess += strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "受注物件名")
            arrFocusTargetCtrl.Add(Me.TextBukkenMei)
        End If
        '調査希望日
        If Me.TextTyousaKibouDate.Text = String.Empty Then
            errMess += strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "調査希望日")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouDate)
        End If
        '物件住所1
        If Me.TextBukkenJyuusyo1.Text = String.Empty Then
            errMess += strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If

        '●日付チェック
        '調査希望日
        If Me.TextTyousaKibouDate.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextTyousaKibouDate.Text) = False Then
                errMess += strErrGyouInfo & Messages.MSG014E.Replace("@PARAM1", "調査希望日")
                arrFocusTargetCtrl.Add(Me.TextTyousaKibouDate)
            End If
        End If

        '●桁数チェック
        'なし

        '●禁則文字チェック(文字列入力フィールドが対象)
        '施主名
        If jBn.KinsiStrCheck(Me.TextSesyuMei.Text) = False Then
            errMess += strErrGyouInfo & Messages.MSG015E.Replace("@PARAM1", "施主名")
            arrFocusTargetCtrl.Add(Me.TextSesyuMei)
        End If
        '受注物件名
        If jBn.KinsiStrCheck(Me.TextBukkenMei.Text) = False Then
            errMess += strErrGyouInfo & Messages.MSG015E.Replace("@PARAM1", "受注物件名")
            arrFocusTargetCtrl.Add(Me.TextBukkenMei)
        End If
        '調査希望時間
        If jBn.KinsiStrCheck(Me.TextTyousaKibouJikan.Text) = False Then
            errMess += strErrGyouInfo & Messages.MSG015E.Replace("@PARAM1", "調査希望時間")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouJikan)
        End If
        '物件住所1
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo1.Text) = False Then
            errMess += strErrGyouInfo & Messages.MSG015E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '物件住所2
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo2.Text) = False Then
            errMess += strErrGyouInfo & Messages.MSG015E.Replace("@PARAM1", "物件住所2")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo2)
        End If
        '物件住所3
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo3.Text) = False Then
            errMess += strErrGyouInfo & Messages.MSG015E.Replace("@PARAM1", "物件住所3")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo3)
        End If
        '備考
        If jBn.KinsiStrCheck(Me.TextBikou.Text) = False Then
            errMess += strErrGyouInfo & Messages.MSG015E.Replace("@PARAM1", "備考")
            arrFocusTargetCtrl.Add(Me.TextBikou)
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '施主名
        If jBn.ByteCheckSJIS(Me.TextSesyuMei.Text, Me.TextSesyuMei.MaxLength) = False Then
            errMess += strErrGyouInfo & Messages.MSG016E.Replace("@PARAM1", "施主名")
            arrFocusTargetCtrl.Add(Me.TextSesyuMei)
        End If
        '受注物件名
        If jBn.ByteCheckSJIS(Me.TextBukkenMei.Text, Me.TextSesyuMei.MaxLength) = False Then
            errMess += strErrGyouInfo & Messages.MSG016E.Replace("@PARAM1", "受注物件名")
            arrFocusTargetCtrl.Add(Me.TextBukkenMei)
        End If
        '調査希望時間
        If jBn.ByteCheckSJIS(Me.TextTyousaKibouJikan.Text, Me.TextTyousaKibouJikan.MaxLength) = False Then
            errMess += strErrGyouInfo & Messages.MSG016E.Replace("@PARAM1", "調査希望時間")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouJikan)
        End If
        '物件住所1
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo1.Text, Me.TextBukkenJyuusyo1.MaxLength) = False Then
            errMess += strErrGyouInfo & Messages.MSG016E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '物件住所2
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo2.Text, Me.TextBukkenJyuusyo2.MaxLength) = False Then
            errMess += strErrGyouInfo & Messages.MSG016E.Replace("@PARAM1", "物件住所2")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo2)
        End If
        '物件住所3
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo3.Text, Me.TextBukkenJyuusyo3.MaxLength) = False Then
            errMess += strErrGyouInfo & Messages.MSG016E.Replace("@PARAM1", "物件住所3")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo3)
        End If
        '備考
        If jBn.ByteCheckSJIS(Me.TextBikou.Text, Me.TextBikou.MaxLength) = False Then
            errMess += strErrGyouInfo & Messages.MSG016E.Replace("@PARAM1", "備考")
            arrFocusTargetCtrl.Add(Me.TextBikou)
        End If

        '分譲コード(存在チェック)
        If Me.TextBunjouCode.Text <> String.Empty Then
            If JLogic.ChkJibanBunjouCd(Me.TextBunjouCode.Text) = False Then
                errMess += strErrGyouInfo & Messages.MSG165E.Replace("@PARAM1", "分譲コード").Replace("@PARAM2", "地盤データ").Replace("@PARAM3", "分譲コード")
                arrFocusTargetCtrl.Add(Me.TextBunjouCode)
            End If
        End If

        '物件NOを取得
        Dim strBukkenNo As String = Me.TextKokyakuBangou.Text
        Dim strBukkenNayoseCd As String = Me.TextNayoseCode.Text
        Dim blnBukkenNoFlg As Boolean = True

        '物件名寄コード
        If strBukkenNayoseCd = String.Empty Then '未入力
            strBukkenNayoseCd = strBukkenNo '未入力の場合、当物件NOをセット
        End If

        '物件名寄コード(入力不正チェック)
        If Me.TextNayoseCode.Text <> String.Empty Then
            If Me.TextNayoseCode.Text.Length = 11 Then
            Else
                blnBukkenNoFlg = False

                errMess += strErrGyouInfo & Messages.MSG040E.Replace("@PARAM1", "物件名寄コード") & "【区分+保証書NO(番号)】\r\n"
                arrFocusTargetCtrl.Add(Me.TextNayoseCode)
            End If
        End If

        If blnBukkenNoFlg Then
            '名寄先が親物件かのチェック
            If JLogic.ChkBukkenNayoseOyaBukken(strBukkenNo, strBukkenNayoseCd) = False Then
                errMess += strErrGyouInfo & Messages.MSG167E.Replace("@PARAM1", "名寄先の物件").Replace("@PARAM2", "子物件").Replace("@PARAM3", "物件名寄コード")
                arrFocusTargetCtrl.Add(Me.TextNayoseCode)
            End If

            '自物件の名寄状況チェック
            If JLogic.ChkBukkenNayoseJyky(strBukkenNo, strBukkenNayoseCd) = False Then
                errMess += strErrGyouInfo & Messages.MSG167E.Replace("@PARAM1", "当物件NO").Replace("@PARAM2", "他物件の名寄先").Replace("@PARAM3", "物件名寄コード")
                arrFocusTargetCtrl.Add(Me.TextNayoseCode)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        ' 経由コンボにデータをバインドする
        helper.SetDropDownList(SelectKeiyu, DropDownHelper.DropDownType.Keiyu, False, True)

        '******************************************
        '* 画面コントロールに設定
        '******************************************
        '顧客番号
        Me.TextKokyakuBangou.Text = cl.GetDisplayString(jr.Kbn) & cl.GetDisplayString(jr.HosyousyoNo)
        '施主名
        Me.TextSesyuMei.Text = cl.GetDisplayString(jr.SesyuMei)
        '受注物件名
        Me.TextBukkenMei.Text = cl.GetDisplayString(jr.JyutyuuBukkenMei)
        '物件住所1
        Me.TextBukkenJyuusyo1.Text = cl.GetDisplayString(jr.BukkenJyuusyo1)
        '物件住所2
        Me.TextBukkenJyuusyo2.Text = cl.GetDisplayString(jr.BukkenJyuusyo2)
        '物件住所3
        Me.TextBukkenJyuusyo3.Text = cl.GetDisplayString(jr.BukkenJyuusyo3)
        '経由
        Me.SelectKeiyu.SelectedValue = jr.Keiyu
        '備考
        Me.TextBikou.Text = cl.GetDisplayString(jr.Bikou)
        '分譲コード
        Me.TextBunjouCode.Text = cl.GetDisplayString(jr.BunjouCd)
        '物件名寄コード
        Me.TextNayoseCode.Text = cl.GetDisplayString(jr.BukkenNayoseCd)
        '調査希望日
        Me.TextTyousaKibouDate.Text = cl.GetDisplayString(jr.TysKibouDate)
        '調査希望時間
        Me.TextTyousaKibouJikan.Text = cl.GetDisplayString(jr.TysKibouJikan)
        '更新日時
        Me.updateDateTime.Value = IIf(jr.UpdDatetime = Date.MinValue, "", Format(jr.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        '更新者
        '更新ログインユーザID

        '****************************
        '* Hidden項目
        '****************************
        '区分
        Me.HiddenKbn.Value = cl.GetDisplayString(jr.Kbn)
        '番号
        Me.HiddenNo.Value = cl.GetDisplayString(jr.HosyousyoNo)


    End Sub

#End Region

End Class