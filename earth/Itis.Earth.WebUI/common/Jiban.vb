''' <summary>
''' 地盤画面共通クラス
''' </summary>
''' <remarks>画面表示に関連する共通処理を管理</remarks>
Public Class Jiban

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのStringLogicクラス
    Dim sLogic As New StringLogic
    'CommonLogicクラス
    Dim ComLogic As New CommonLogic

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        '必要に応じて実装
        'MsgBox("コンストラクタ")

    End Sub

    ''' <summary>
    ''' 依頼（受注）画面データ保存用ハッシュテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private _iraiData As New Hashtable

    ''' <summary>
    ''' 依頼（受注）画面データ保存用ハッシュテーブル
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IraiData() As Hashtable
        Get
            Return _iraiData
        End Get
        Set(ByVal value As Hashtable)
            _iraiData = value
        End Set
    End Property

    ''' <summary>
    ''' 画面ドロップダウンリスト(商品1)保存用ハッシュテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private _ddlDataSyouhin1 As New Hashtable
    ''' <summary>
    ''' 画面ドロップダウンリスト(商品1)保存用ハッシュテーブル
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DdlDataSyouhin1() As Hashtable
        Get
            Return _ddlDataSyouhin1
        End Get
        Set(ByVal value As Hashtable)
            _ddlDataSyouhin1 = value
        End Set
    End Property

    ''' <summary>
    ''' 画面ドロップリスト(調査方法)保存用ハッシュテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private _ddlDataTysHouhou As New Hashtable
    ''' <summary>
    ''' 画面ドロップダウンリスト(調査方法)保存用ハッシュテーブル
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DdlDataTysHouhou() As Hashtable
        Get
            Return _ddlDataTysHouhou
        End Get
        Set(ByVal value As Hashtable)
            _ddlDataTysHouhou = value
        End Set
    End Property

    ''' <summary>
    ''' ユーザー認証
    ''' </summary>
    ''' <param name="userInfo">ユーザー情報</param>
    ''' <remarks></remarks>
    Public Sub UserAuth(ByRef userInfo As LoginUserInfo)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UserAuth", _
                                                    userInfo)

        ' ユーザー認証
        Dim ninsyou As New Ninsyou()
        If (Not ninsyou.IsUserLogon()) Then
            '認証失敗
            ninsyou.EndResponseWithAccessDeny()
        End If

        'ユーザー権限によりリンク状態を切り替え
        Dim loginLogic As New LoginUserLogic

        If loginLogic.MakeUserInfo(ninsyou.GetUserID(), userInfo) Then

        Else
            ' ユーザーアカウント情報取得不可の場合Nothingをセット
            userInfo = Nothing
            Debug.WriteLine("ログインユーザー情報の取得(地盤認証マスタ)に失敗しました")
        End If

    End Sub

    ''' <summary>
    ''' プルダウン連動コード入力項目スクリプト設定
    ''' </summary>
    ''' <param name="objCd">コード入力項目オブジェクト</param>
    ''' <param name="objPull">プルダウン項目オブジェクト</param>
    ''' <remarks>当処理は、各画面のLoad処理で最初に実行する。
    ''' 画面側で更にイベントハンドラを埋め込む際には、かき消さないように追記する</remarks>
    Public Sub SetPullCdScriptSrc(ByVal objCd As System.Object, _
                                  ByVal objPull As System.Object)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetPullCdScriptSrc", _
                                                    objCd, _
                                                    objPull)

        'コード入力項目の値を、プルダウンとあわせる
        If objPull.GetType().ToString() = "System.Web.UI.WebControls.DropDownList" Then
            If objCd.GetType().ToString() = "System.Web.UI.WebControls.TextBox" Then
                objCd.Text = objPull.Text
            Else
                objCd.Value = objPull.Text
            End If

        Else
            objCd.Value = objPull.Value
        End If
        'コード入力項目、プルダウン項目のイベントハンドラにスクリプトを設定
        objCd.Attributes("onblur") = "setCode2Pull(this,'" & objPull.ClientID & "'," & objCd.MaxLength & ");"
        objCd.Attributes("onblur") += "if(objEBI('" & objPull.ClientID & "').value!=this.value)this.value='';"
        objPull.Attributes("onchange") = "objEBI('" & objCd.ClientID & "').value=this.value;"
    End Sub

    ''' <summary>
    ''' コントロールにJavaScriptのイベントハンドラスクリプトをセットする
    ''' </summary>
    ''' <param name="obj">セット先のコントロールオブジェクト</param>
    ''' <param name="eventType">イベントハンドラの種類(onclick、onchange等)</param>
    ''' <param name="script">セットするスクリプト</param>
    ''' <remarks></remarks>
    Public Sub SetEventScript(ByVal obj As Object, _
                              ByVal eventType As String, _
                              ByVal script As String)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetEventScript", _
                                                    obj, _
                                                    eventType, _
                                                    script)

        Dim tmpScr As String = obj.Attributes(eventType)
        If tmpScr <> String.Empty Then
            '既に同じスクリプトが設定されていないかをチェック
            If tmpScr.IndexOf(script) = -1 Then
                obj.Attributes(eventType) += script
            End If
        Else
            obj.Attributes(eventType) = script
        End If
    End Sub

    ''' <summary>
    ''' 全てのWebUIコントロールの「Style("visibility")」を切替える
    ''' </summary>
    ''' <param name="target">対象親コントロール</param>
    ''' <param name="setType">セットするvisibilityの値(visible/hidden/...)</param>
    ''' <remarks></remarks>
    Public Sub SetVisibilityAll(ByVal target As Control, _
                                ByVal setType As String)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetVisibilityAll", _
                                                    target, _
                                                    setType)

        If target.HasControls() Then
            Dim child As Control
            For Each child In target.Controls()
                Select Case child.GetType.Name
                    Case "HtmlInputText", "HtmlSelect", "HtmlTextArea", "HtmlInputRadioButton" _
                         , "HtmlInputCheckBox", "DropDownList", "HtmlInputButton", "HtmlButton" _
                         ', "HtmlInputHidden"
                        If child.ID <> "" Then
                            Dim tmpObj As Object = child
                            tmpObj.Style("visibility") = setType
                        End If

                    Case Else
                        '現在のコントロール階層に対象が存在しない場合、子コントロールを基点に処理を実行する
                        SetVisibilityAll(child, setType)

                End Select
            Next
        End If
    End Sub

    ''' <summary>
    ''' 全てのWebUIコントロールのDisabledを変更する（活性、非活性化）
    ''' </summary>
    ''' <param name="target">対象親コントロール</param>
    ''' <param name="setType">制御（true:set disabled / false:remove disabled）</param>
    ''' <param name="noTraget">対象外にするコントロールID郡</param>
    ''' <remarks></remarks>
    Public Sub ChangeDesabledAll(ByVal target As Control, _
                                 ByVal setType As Boolean, _
                                 ByVal noTraget As Hashtable)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChangeDesabledAll", _
                                                    target, _
                                                    setType, _
                                                    noTraget)

        If target.HasControls() Then
            Dim child As Control
            For Each child In target.Controls()
                Dim tmpObj As Object = child

                Select Case tmpObj.GetType.Name
                    Case "HtmlInputText", "HtmlTextArea", "HtmlInputRadioButton" _
                         , "HtmlInputCheckBox", "HtmlInputButton", "HtmlButton" _
                         , "HtmlSelect"
                        If tmpObj.ID <> "" And noTraget.ContainsKey(tmpObj.ID) = False Then
                            tmpObj.Disabled = setType

                        End If

                    Case "DropDownList", "TextBox", "CheckBox"
                        If tmpObj.ID <> "" And noTraget.ContainsKey(tmpObj.ID) = False Then
                            tmpObj.Enabled = (setType = False)

                        End If

                    Case Else
                        '現在のコントロール階層に対象が存在しない場合、子コントロールを基点に処理を実行する
                        ChangeDesabledAll(child, setType, noTraget)

                End Select
            Next
        End If
    End Sub

    ''' <summary>
    ''' 全てのWebUIコントロールの値をクリアする
    ''' </summary>
    ''' <param name="target">対象親コントロール</param>
    ''' <remarks></remarks>
    Public Sub ClearCtrlValue(ByVal target As Control)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ClearCtrlValue", _
                                                    target)

        If target.HasControls() Then
            Dim child As Control
            For Each child In target.Controls()
                Dim tmpObj As Object = child

                Select Case tmpObj.GetType.Name
                    Case "HtmlInputText", "HtmlTextArea", "HtmlInputHidden"
                        tmpObj.Value = String.Empty
                    Case "DropDownList", "HtmlSelect"
                        tmpObj.SelectedIndex = 0
                    Case "HtmlGenericControl"
                        tmpObj.InnerText = String.Empty
                    Case "HtmlInputRadioButton", "HtmlInputCheckBox"
                        tmpObj.Checked = False

                    Case Else
                        '現在のコントロール階層に対象が存在しない場合、子コントロールを基点に処理を実行する
                        ClearCtrlValue(child)

                End Select
            Next
        End If
    End Sub


    ''' <summary>
    ''' バイト数チェック(SJIS)
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <param name="max">最大OKバイト数</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks></remarks>
    Public Function ByteCheckSJIS(ByVal target As String, _
                                  ByVal max As Integer) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ByteCheckSJIS", _
                                                    target, _
                                                    max)

        Return sLogic.ByteCheckSJIS(target, max)

    End Function

    ''' <summary>
    ''' 禁則文字チェック
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks></remarks>
    Public Function KinsiStrCheck(ByVal target As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".KinsiStrCheck", _
                                                    target)

        Return sLogic.KinsiStrCheck(target)

    End Function

    ''' <summary>
    ''' 数値文字列桁数チェック
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <param name="seisu">整数部桁数</param>
    ''' <param name="syousu">小数部桁数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SuutiStrCheck(ByVal target As String, _
                                  ByVal seisu As Integer, _
                                  ByVal syousu As Integer) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SuutiStrCheck", _
                                                    target, _
                                                    seisu, _
                                                    syousu)

        target = target.Replace(",", String.Empty)

        If target <> String.Empty Then
            Dim arr As Array = target.Split(".")
            If ByteCheckSJIS(arr(0).ToString, seisu) = False Then
                Return False
            End If

            If arr.Length > 1 Then
                If ByteCheckSJIS(arr(1).ToString, syousu) = False Then
                    Return False
                End If
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' コードバイト数チェック(SJIS)
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <param name="max">最大OKバイト数</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks>Char型の文字列桁数チェック</remarks>
    Public Function CodeByteCheckSJIS(ByVal target As String, _
                                  ByVal max As Integer) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CodeByteCheckSJIS", _
                                                    target, _
                                                    max)

        Return sLogic.GetStrByteSJIS(target) = max

    End Function

    ''' <summary>
    ''' 金額プラス値不許可チェック
    ''' </summary>
    ''' <param name="target"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function KingakuPlusCheck(ByVal target As String) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".KingakuPlusCheck", _
                                                    target)
        Dim kingaku As Integer = 0
        Const intChk As Integer = 0

        '空白の場合はチェックしない
        If target = String.Empty Then
            Return True
        Else
            ComLogic.SetDisplayString(target, kingaku)

            If kingaku > intChk Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

End Class
