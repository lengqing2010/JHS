
''' <summary>
''' 地盤画面セッション管理クラス
''' </summary>
''' <remarks>画面情報のセッション格納＆セッションからの画面表示を行う</remarks>
Public Class JibanSessionManager
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'セパレータ文字列
    Const sepStr2 As String = vbTab & EarthConst.SEP_STRING & vbTab
    'セパレータ文字列
    Const sepStrShp As String = EarthConst.SEP_STRING_REPORTIF
    'セパレータ文字列(2重)
    Const sepStrDbl = EarthConst.SEP_STRING & EarthConst.SEP_STRING
    'セパレータ文字列(3重)
    Const sepStrTpl = EarthConst.SEP_STRING & EarthConst.SEP_STRING & EarthConst.SEP_STRING

    'CommonLogic
    Dim cl As New CommonLogic

    ''' <summary>
    ''' 画面項目の値をセッション格納用ハッシュテーブルに格納
    ''' セッションキー＝各コントロールのID / セッション値＝各コントロールの値
    ''' </summary>
    ''' <param name="target">指定されたコントロール配下の子コントロールに対して、処理を実行する</param>
    ''' <param name="iraiData">依頼画面データ格納ハッシュテーブル</param>
    ''' <remarks></remarks>
    Public Sub Ctrl2Hash(ByVal target As Control, _
                         ByRef iraiData As Hashtable)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".Ctrl2Hash", _
                                                    target, _
                                                    iraiData)

        If target.HasControls() Then
            Dim child As Control
            For Each child In target.Controls()
                Select Case child.GetType.Name
                    Case "HtmlInputText", "HtmlInputHidden", "HtmlSelect", "HtmlTextArea", "HtmlInputRadioButton", "HtmlInputCheckBox", "DropDownList", "Label"
                        If child.ClientID <> "" Then
                            iraiData(child.ClientID) = child
                        End If
                    Case Else
                        '現在のコントロール階層に対象が存在しない場合、子コントロールを基点に処理を実行する
                        Ctrl2Hash(child, iraiData)

                End Select
            Next
        End If
    End Sub

    ''' <summary>
    ''' 画面項目の値をセッション格納用ハッシュテーブルに格納
    ''' ドロップダウンリスト専用
    ''' </summary>
    ''' <param name="ddlTgt"></param>
    ''' <param name="ddlData"></param>
    ''' <remarks></remarks>
    Public Sub Ddl2Hash(ByVal ddlTgt As DropDownList, ByRef ddlData As Hashtable)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".Ctrl2Hash", _
                                                    ddlTgt, _
                                                    ddlData)

        If ddlTgt.Items.Count > 0 Then
            For i As Integer = 0 To ddlTgt.Items.Count - 1
                If ddlTgt.Items(i).Value <> String.Empty Then
                    ddlData(i) = ddlTgt.Items(i).Value & EarthConst.SEP_STRING & ddlTgt.Items(i).Text
                End If
            Next
        End If

    End Sub

    ''' <summary>
    ''' セッション格納用ハッシュテーブルに格納されている値を、画面コントロールにセットする
    ''' キー＝各コントロールのID / 値＝各コントロールの値
    ''' また、画面モードが「edit」以外の場合、対象のコントロールを表示専用に設定する
    ''' </summary>
    ''' <param name="target">指定されたコントロール配下の子コントロールに対して、処理を実行する</param>
    ''' <param name="viewMode">画面表示モード</param>
    ''' <param name="iraiData">依頼画面データ格納ハッシュテーブル</param>
    ''' <remarks></remarks>
    Public Sub Hash2Ctrl(ByVal target As Control, _
                         ByVal viewMode As String, _
                         ByRef iraiData As Hashtable, _
                         Optional ByVal notTargets As Hashtable = Nothing)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".Hash2Ctrl", _
                                                    target, _
                                                    viewMode, _
                                                    iraiData, _
                                                    notTargets)

        '共通ロジックの生成
        Dim cl As New CommonLogic

        '対象外一覧の生成
        Dim nTh As New Hashtable
        If notTargets Is Nothing Then
            nTh.Add("aa", "aa")
        Else
            nTh = notTargets
        End If

        'メイン処理
        If target.HasControls() Then

            For i As Integer = 0 To target.Controls().Count - 1
                Dim tmpCtrl As Object = target.Controls(i)

                Select Case tmpCtrl.GetType.Name

                    Case "HtmlInputText", "HtmlInputHidden", "HtmlTextArea"
                        If CheckCtrlHash2(tmpCtrl, iraiData) Then
                            tmpCtrl.Value = iraiData(tmpCtrl.ClientID).Value
                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            cl.chgVeiwMode(tmpCtrl)
                        End If

                    Case "TextBox"
                        If CheckCtrlHash2(tmpCtrl, iraiData) Then
                            tmpCtrl.Text = iraiData(tmpCtrl.ClientID).Text
                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            cl.chgVeiwMode(tmpCtrl)
                        End If

                    Case "HtmlInputRadioButton", "HtmlInputCheckBox"
                        If CheckCtrlHash2(tmpCtrl, iraiData) Then
                            tmpCtrl.Checked = iraiData(tmpCtrl.ClientID).Checked
                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            cl.chgVeiwMode(tmpCtrl, searchTempText(target, i))
                        End If

                    Case "HtmlSelect"
                        If CheckCtrlHash2(tmpCtrl, iraiData) Then
                            tmpCtrl.Value = iraiData(tmpCtrl.ClientID).Value
                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            cl.chgVeiwMode(tmpCtrl, searchTempText(target, i))
                        End If

                    Case "DropDownList"
                        If CheckCtrlHash2(tmpCtrl, iraiData) Then
                            If cl.ChkDropDownList(tmpCtrl, cl.GetDispStr(iraiData(tmpCtrl.ClientID).Text)) Or _
                                iraiData(tmpCtrl.ClientID).Text = "" Then
                                'DDLに存在する場合はそのまま設定
                                tmpCtrl.Text = iraiData(tmpCtrl.ClientID).Text
                            Else
                                'DDLに存在しない場合は、リストに追加して設定
                                tmpCtrl.Items.Add(New ListItem(iraiData(tmpCtrl.ClientID).SelectedItem.text, iraiData(tmpCtrl.ClientID).Text))
                                tmpCtrl.Text = iraiData(tmpCtrl.ClientID).Text
                            End If
                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            cl.chgVeiwMode(tmpCtrl, searchTempText(target, i))
                        End If

                    Case "CheckBox"
                            If CheckCtrlHash2(tmpCtrl, iraiData) Then
                                tmpCtrl.checked = iraiData(tmpCtrl.ClientID).checked
                            End If
                            If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                                cl.chgVeiwMode(tmpCtrl, searchTempText(target, i))
                            End If

                    Case "HtmlInputButton"
                            If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                                cl.chgVeiwMode(tmpCtrl.Style)
                            End If

                    Case "Label"
                        If CheckCtrlHash2(tmpCtrl, iraiData) Then
                            tmpCtrl.Text = iraiData(tmpCtrl.ClientID).Text
                        End If

                    Case Else
                            '現在のコントロール階層に対象が存在しない場合、子コントロールを基点に処理を実行する
                            Hash2Ctrl(tmpCtrl, viewMode, iraiData, nTh)

                End Select

            Next

        End If

    End Sub

    ''' <summary>
    ''' 対象コントロールの値表示用コントロールを検索（セレクトボックス、ラジオボタン等表示化用）
    ''' </summary>
    ''' <param name="target"></param>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function searchTempText(ByRef target As Control, ByRef i As Integer) As HtmlGenericControl
        Dim ii As New Integer
        For ii = 1 To target.Controls.Count - i
            If target.Controls(i + ii).GetType.Name = "HtmlGenericControl" Then
                Return target.Controls(i + ii)
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' 画面コントロールがセッション格納用ハッシュテーブルに格納されているか否かをチェック
    ''' </summary>
    ''' <param name="tmpCtrl">チェック対象コントロール</param>
    ''' <param name="iraiData">セッション格納用ハッシュテーブル</param>
    ''' <returns>チェック結果（true/false）</returns>
    ''' <remarks></remarks>
    Private Function CheckCtrlHash2(ByVal tmpCtrl As Object, _
                                    ByVal iraiData As Hashtable) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckCtrlHash2", _
                                                    tmpCtrl, _
                                                    iraiData)

        If iraiData IsNot Nothing Then
            If iraiData.ContainsKey(tmpCtrl.ClientID) Then
                If iraiData(tmpCtrl.ClientID).clientID = tmpCtrl.clientID Then
                    Return True
                End If
            End If
        End If

        Return False

    End Function

    ''' <summary>
    ''' 画面コントロールの情報を文字列に変換する
    ''' </summary>
    ''' <param name="targetCtrl">指定されたコントロール配下の子コントロールに対して、処理を実行する</param>
    ''' <param name="sb">データ格納StringBuilder（キー=ID、値=value等）</param>
    ''' <param name="flgId">IDを含めるか否かの設定値 </param>
    ''' <param name="notTargets">対象外一覧Hashtable </param>
    ''' <remarks></remarks>
    Public Sub Ctrl2Str(ByVal targetCtrl As Control, _
                        ByRef sb As StringBuilder, _
                        ByVal flgId As Boolean, _
                        Optional ByVal notTargets As Hashtable = Nothing)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".Ctrl2Str", _
                                                    targetCtrl, _
                                                    sb, _
                                                    flgId, _
                                                    notTargets)

        Dim sepStr As String = EarthConst.SEP_STRING
        '対象外一覧の生成
        Dim nTh As New Hashtable
        If notTargets Is Nothing Then
            nTh.Add("aa", "aa")
        Else
            nTh = notTargets
        End If

        If targetCtrl.HasControls() Then
            Dim ctrl As Object
            For Each ctrl In targetCtrl.Controls()
                Select Case ctrl.GetType.Name
                    Case "HtmlInputText", "HtmlInputHidden", "HtmlSelect", "HtmlTextArea"
                        If ctrl.ID <> "" AndAlso nTh.ContainsKey(ctrl.ID) = False Then
                            If flgId Then
                                sb.Append(ctrl.ID)
                                sb.Append(vbTab)
                            End If
                            sb.Append(ctrl.Value)
                            sb.Append(sepStr)
                        End If
                    Case "TextBox", "Label"
                        If ctrl.ID <> "" AndAlso nTh.ContainsKey(ctrl.ID) = False Then
                            If flgId Then
                                sb.Append(ctrl.ID)
                                sb.Append(vbTab)
                            End If
                            sb.Append(ctrl.Text)
                            sb.Append(sepStr)
                        End If
                    Case "HtmlInputRadioButton", "HtmlInputCheckBox", "CheckBox"
                        If ctrl.ID <> "" AndAlso nTh.ContainsKey(ctrl.ID) = False Then
                            If flgId Then
                                sb.Append(ctrl.ID)
                                sb.Append(vbTab)
                            End If
                            sb.Append(ctrl.Checked.ToString)
                            sb.Append(sepStr)
                        End If
                    Case "DropDownList"
                        If ctrl.ID <> "" AndAlso nTh.ContainsKey(ctrl.ID) = False Then
                            If flgId Then
                                sb.Append(ctrl.ID)
                                sb.Append(vbTab)
                            End If
                            sb.Append(ctrl.SelectedValue)
                            sb.Append(sepStr)
                        End If
                    Case Else
                        '現在のコントロール階層に対象が存在しない場合、子コントロールを基点に処理を実行する
                        Ctrl2Str(ctrl, sb, flgId, nTh)
                End Select
            Next

        End If

    End Sub

    ''' <summary>
    ''' 画面コントロールの情報を持ったハッシュテーブルを、文字列に変換する
    ''' </summary>
    ''' <param name="ht">指定されたコントロール配下の子コントロールに対して、処理を実行する</param>
    ''' <param name="strVal">データ格納文字列（キー=ID、値=value等）</param>
    ''' <remarks></remarks>
    Public Sub CtrlHash2Str(ByVal ht As Hashtable, _
                            ByRef strVal As String)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CtrlHash2Str", _
                                                    ht, _
                                                    strVal)

        Dim sb As New StringBuilder()
        For Each key As String In ht.Keys
            Dim ctrl As Object = ht(key)
            Select Case ctrl.GetType.Name
                Case "HtmlInputText", "HtmlInputHidden", "HtmlSelect", "HtmlTextArea"
                    If ctrl.ClientID <> "" Then
                        sb.Append(ctrl.ClientID)
                        sb.Append(vbTab)
                        sb.Append(ctrl.Value)
                        sb.Append(sepStr2)
                    End If
                Case "HtmlInputRadioButton", "HtmlInputCheckBox"
                    If ctrl.ClientID <> "" Then
                        sb.Append(ctrl.ClientID)
                        sb.Append(vbTab)
                        sb.Append(ctrl.Checked.ToString)
                        sb.Append(sepStr2)
                    End If
                Case "DropDownList"
                    If ctrl.ClientID <> "" Then
                        sb.Append(ctrl.ClientID)
                        sb.Append(vbTab)
                        sb.Append(ctrl.SelectedValue & sepStrShp & ctrl.selecteditem.text)
                        sb.Append(sepStr2)
                    End If
                Case "Label"
                    If ctrl.ClientID <> "" Then
                        sb.Append(ctrl.ClientID)
                        sb.Append(vbTab)
                        sb.Append(ctrl.Text)
                        sb.Append(sepStr2)
                    End If
                Case Else

            End Select

        Next
        strVal = sb.ToString()
    End Sub

    ''' <summary>
    ''' ドロップダウンリストのハッシュテーブルから文字列に変換する
    ''' </summary>
    ''' <param name="ht1"></param>
    ''' <param name="ht2"></param>
    ''' <param name="strval"></param>
    ''' <remarks></remarks>
    Public Sub DdlHash2Str(ByRef strval As String, ByVal ht1 As Hashtable, ByVal ht2 As Hashtable)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CtrlHash2Str", _
                                                    ht1, _
                                                    ht2, _
                                                    strval)

        Dim sb As New StringBuilder()

        '商品1
        If ht1 IsNot Nothing Then
            Dim Item As New DictionaryEntry
            For Each Item In ht1
                sb.Append(Item.Value)
                sb.Append(sepStrDbl)
            Next
        End If

        sb.Append(sepStrTpl)

        '調査方法
        If ht2 IsNot Nothing Then
            Dim item2 As New DictionaryEntry
            For Each item2 In ht2
                sb.Append(item2.Value)
                sb.Append(sepStrDbl)
            Next
        End If

        strval = sb.ToString()

    End Sub

    ''' <summary>
    ''' 文字列に格納されている値を、画面コントロールにセットする
    ''' キー＝各コントロールのID / 値＝各コントロールの値
    ''' また、画面モードが「edit」以外の場合、対象のコントロールを表示専用に設定する
    ''' </summary>
    ''' <param name="target">指定されたコントロール配下の子コントロールに対して、処理を実行する</param>
    ''' <param name="viewMode">画面表示モード</param>
    ''' <param name="ht">データ格納文字列（キー=ID、値=value等）</param>
    ''' <remarks></remarks>
    Public Sub HashStr2Ctrl(ByVal target As Control, _
                            ByVal viewMode As String, _
                            ByVal ht As Hashtable, _
                            Optional ByVal notTargets As Hashtable = Nothing)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".HashStr2Ctrl", _
                                                    target, _
                                                    viewMode, _
                                                    ht, _
                                                    notTargets)

        '対象外一覧の生成
        Dim nTh As New Hashtable
        If notTargets Is Nothing Then
            nTh.Add("aa", "aa")
        Else
            nTh = notTargets
        End If

        'メイン処理
        If target.HasControls() Then

            For i As Integer = 0 To target.Controls().Count - 1
                Dim tmpCtrl As Object = target.Controls(i)

                Select Case tmpCtrl.GetType.Name

                    Case "HtmlInputText", "HtmlInputHidden", "HtmlTextArea"
                        If CheckCtrlHash(tmpCtrl, ht) Then
                            tmpCtrl.Value = ht(tmpCtrl.ClientID)
                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            tmpCtrl.Attributes("readonly") = True
                            Dim classString As String = String.Empty
                            If tmpCtrl.Attributes("class") IsNot Nothing Then
                                classString = Replace(tmpCtrl.Attributes("class").ToString, "readOnlyStyle", "")
                                classString = Replace(classString, " readOnlyStyle", String.Empty)
                            End If
                            tmpCtrl.Attributes("class") = classString & " readOnlyStyle"
                            tmpCtrl.Style("border-style") = "none"
                            tmpCtrl.Attributes("tabindex") = -1
                        End If

                    Case "HtmlInputRadioButton", "HtmlInputCheckBox"
                        If CheckCtrlHash(tmpCtrl, ht) Then
                            tmpCtrl.Checked = ht(tmpCtrl.ClientID)
                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            tmpCtrl.Style("display") = "none"
                            Dim tmpText As HtmlGenericControl = target.Controls(i + 1)
                            If tmpCtrl.Checked = True Then
                                Dim tmpInnerHtml As String = tmpText.InnerHtml
                                tmpInnerHtml = Replace(tmpInnerHtml, "&nbsp;【&nbsp;", String.Empty)
                                tmpInnerHtml = Replace(tmpInnerHtml, "&nbsp;】&nbsp;", String.Empty)
                                tmpInnerHtml = "&nbsp;【&nbsp;" & tmpInnerHtml & "&nbsp;】&nbsp;"
                                tmpText.InnerHtml = tmpInnerHtml
                                tmpText.Style("display") = "inline"
                            Else
                                tmpText.Style("display") = "none"
                            End If
                        End If

                    Case "HtmlSelect"
                        If CheckCtrlHash(tmpCtrl, ht) Then
                            tmpCtrl.Value = ht(tmpCtrl.ClientID)
                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            tmpCtrl.Style("display") = "none"
                            For ii As Integer = 1 To target.Controls.Count - i
                                If target.Controls(i + ii).GetType.Name = "HtmlGenericControl" Then
                                    Dim tmpText As HtmlGenericControl = target.Controls(i + ii)
                                    tmpText.InnerHtml = "&nbsp;【&nbsp;" & tmpCtrl.Items(tmpCtrl.SelectedIndex).Text & "&nbsp;】&nbsp;"
                                    Exit For
                                End If
                            Next
                        End If

                    Case "DropDownList"
                        If CheckCtrlHash(tmpCtrl, ht) Then
                            'tmpCtrl.SelectedValue = ht(tmpCtrl.ClientID)

                            'DDLの連結文字列
                            Dim tmpString As String = ht(tmpCtrl.ClientID).ToString
                            Dim tmpValue As String = String.Empty
                            Dim tmpText As String = String.Empty

                            'セパレータでコードと名称に分解
                            If tmpString <> String.Empty Then
                                Dim arr1 As Array = Split(ht(tmpCtrl.ClientID).ToString, sepStrShp)
                                If arr1.Length = 2 Then
                                    tmpValue = arr1(0)
                                    tmpText = arr1(1)
                                End If
                            End If
                            'ドロップダウンリストの存在チェック
                            If cl.ChkDropDownList(tmpCtrl, tmpValue) Or tmpValue = "" Then
                                tmpCtrl.selectedvalue = tmpValue
                            Else
                                '存在しない場合は追加
                                tmpCtrl.items.add(New ListItem(tmpText, tmpValue))
                                tmpCtrl.selectedvalue = tmpValue
                            End If

                        End If
                        If viewMode <> EarthConst.MODE_EDIT And nTh.ContainsKey(tmpCtrl.ID) = False Then
                            tmpCtrl.Style("display") = "none"
                            Dim tmpText As HtmlGenericControl = target.Controls(i + 1)
                            tmpText.InnerHtml = "&nbsp;【&nbsp;" & tmpCtrl.SelectedItem.Text & "&nbsp;】&nbsp;"
                        End If

                    Case "Label"
                        If CheckCtrlHash(tmpCtrl, ht) Then
                            tmpCtrl.Text = ht(tmpCtrl.ClientID)
                        End If

                    Case Else
                        '現在のコントロール階層に対象が存在しない場合、子コントロールを基点に処理を実行する
                        HashStr2Ctrl(tmpCtrl, viewMode, ht, nTh)

                End Select

            Next

        End If

    End Sub

    ''' <summary>
    ''' 画面コントロールがコントロール情報格納用ハッシュテーブルに格納されているか否かをチェック
    ''' </summary>
    ''' <param name="tmpCtrl">チェック対象コントロール</param>
    ''' <param name="ht">データ格納用ハッシュテーブル</param>
    ''' <returns>チェック結果（true/false）</returns>
    ''' <remarks></remarks>
    Private Function CheckCtrlHash(ByVal tmpCtrl As Object, _
                                   ByVal ht As Hashtable) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckCtrlHash", _
                                                    tmpCtrl, _
                                                    ht)

        If ht IsNot Nothing Then
            If ht.ContainsKey(tmpCtrl.ClientID) Then
                Return True
            End If
        End If

        Return False

    End Function

    ''' <summary>
    ''' ハッシュテーブルの中身を、テキストに変換する
    ''' </summary>
    ''' <param name="ht">データ格納用ハッシュテーブル</param>
    ''' <returns>チェック結果（true/false）</returns>
    ''' <remarks></remarks>
    Public Function Hash2text(ByVal ht As Hashtable) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".Hash2text", _
                                                    ht)

        Dim rtnStr As String = String.Empty

        If ht IsNot Nothing Then
            For Each key As String In ht.Keys
                rtnStr += key & vbTab & ht(key) & sepStr2
            Next
        End If

        Return rtnStr

    End Function

    ''' <summary>
    ''' テキストをハッシュテーブルに変換する
    ''' </summary>
    ''' <param name="str">データ格納用ハッシュテーブル</param>
    ''' <returns>チェック結果（true/false）</returns>
    ''' <remarks></remarks>
    Public Function Text2hash(ByVal str As String) As Hashtable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".Text2hash", _
                                                    str)

        Dim ht As New Hashtable

        If str <> String.Empty Then
            Dim arr1 As Array = Split(str, sepStr2)   '区切り文字でスプリット
            For Each tmp1 As String In arr1
                Dim arr2 As Array = Split(tmp1, vbTab)  'タブでスプリット
                If arr2(0) <> String.Empty Then
                    'ハッシュテーブルに格納
                    ht(arr2(0)) = arr2(1)
                End If
            Next
        End If

        Return ht

    End Function

    ''' <summary>
    ''' テキストを配列に変換する
    ''' </summary>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Public Sub Text2Array(ByVal str As String, ByRef ddlTgt As DropDownList, ByVal intDdlType As Integer, Optional ByVal withSpaceRow As Boolean = True)

        If str = String.Empty Then
            Exit Sub
        End If

        '文字列をプルダウンアイテム毎に切り分ける
        Dim arrItem As Array = Split(str, sepStrTpl)
        '各ドロップダウンリスト用配列
        Dim arrDdlType As Array = Nothing

        'アイテムを行毎に切り分ける
        If arrItem.Length > 1 Then
            '商品1(1番目に格納したもの)
            Select Case intDdlType
                '商品１ドロップダウンリスト
                Case DropDownHelper.DropDownType.Syouhin1
                    arrDdlType = Split(arrItem(0), sepStrDbl)
                    '調査方法ドロップダウンリスト
                Case DropDownHelper.DropDownType.TyousaHouhou
                    arrDdlType = Split(arrItem(1), sepStrDbl)
                Case Else
                    arrDdlType = Nothing
            End Select
        End If

        '空白行の挿入
        If withSpaceRow Then
            ddlTgt.Items.Add(New ListItem(String.Empty, String.Empty))
        End If

        'ドロップダウンリストの内容設定
        If arrDdlType IsNot Nothing AndAlso arrDdlType.Length > 0 Then
            '配列の要素数をカウント(ループ用)
            Dim intArrCnt As Integer = arrDdlType.Length - 1

            For i As Integer = intArrCnt To 0 Step -1
                'TextとValueを切り分ける
                Dim arrSep As Array = Split(arrDdlType(i).ToString, EarthConst.SEP_STRING)
                If arrSep.Length = 2 Then
                    'プルダウンにカウントダウンして追加
                    ddlTgt.Items.Add(New ListItem(arrSep(1), arrSep(0)))
                End If
            Next
        End If

    End Sub

End Class
