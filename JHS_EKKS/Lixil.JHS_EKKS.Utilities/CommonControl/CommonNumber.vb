Option Explicit On
Option Strict On

Imports System.Web
Imports System.Drawing
Imports System.Text

''' <summary>
''' 数値コントロール
''' </summary>
''' <remarks>数値コントロール</remarks>
''' <history>
''' <para>2012/11/14 P-44979 王新 新規作成 </para>
''' </history>
Public Class CommonNumber
    Inherits System.Web.UI.WebControls.TextBox

#Region "定数"
    Private Const CON_READONLY_BGCOLOR As String = "#D0D0D0"    '使用不可の背景色
    Private Const CON_DEFAULT_BGCOLOR As String = "white"       '平常の背景色
    Private Const CON_FONTCOLOR As String = "red"               'マイナスの場合、赤文字にする
#End Region

#Region "変数"
    Private _ObjName As String = String.Empty
    Private _PageReadOnly As Boolean = False
    Private _LeftFormat As String = String.Empty
    Private _RightFormat As String = String.Empty
    Private _DefaultBackColor As String = CON_DEFAULT_BGCOLOR
    Private _ReadOnlyBackColor As String = CON_READONLY_BGCOLOR
#End Region

#Region "プロパティ"
    ''' <summary>
    ''' テキストの取得
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value() As String
        Get
            Dim strValue As String
            strValue = MyBase.Text.Replace(",", "")

            If Not LeftFormat.Equals(String.Empty) Then
                strValue = strValue.Replace(LeftFormat, "")
            End If

            If Not RightFormat.Equals(String.Empty) Then
                strValue = strValue.Replace(RightFormat, "")
            End If

            Return strValue
        End Get
        Set(ByVal value As String)
            If value = "" Then
                MyBase.Text = ""
            Else
                Dim strValue As String
                If IsNumeric(value) Then
                    strValue = Format(Convert.ToInt64(value.Replace(",", "")), "###,###,###,###,###,##0")
                    MyBase.Text = Me.LeftFormat & strValue & Me.RightFormat

                    'マイナスの場合、数値を赤文字にする
                    If Convert.ToDouble(Me.Value) < 0 Then
                        Me.ForeColor = Color.FromName(CON_FONTCOLOR)
                    End If
                Else
                    MyBase.Text = ""
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' 使用不可の設定
    ''' </summary>
    ''' <value>True:使用不可、False:使用可</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PageReadOnly() As Boolean
        Get
            PageReadOnly = _PageReadOnly
        End Get
        Set(ByVal value As Boolean)
            _PageReadOnly = value
        End Set
    End Property

    ''' <summary>
    ''' 項目名称(メッセージ表示用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObjName() As String
        Get
            ObjName = _ObjName
        End Get
        Set(ByVal value As String)
            _ObjName = value
        End Set
    End Property

    ''' <summary>
    ''' 左に文字を追加する
    ''' </summary>
    ''' <value>左に文字(例:$, \)</value>
    ''' <returns></returns>
    ''' <remarks>左に文字を追加する</remarks>
    Public Property LeftFormat() As String
        Get
            LeftFormat = _LeftFormat
        End Get
        Set(ByVal value As String)
            _LeftFormat = value
        End Set
    End Property

    ''' <summary>
    ''' 右に文字を追加する
    ''' </summary>
    ''' <value>右に文字(例:%)</value>
    ''' <returns></returns>
    ''' <remarks>右に文字を追加する</remarks>
    Public Property RightFormat() As String
        Get
            RightFormat = _RightFormat
        End Get
        Set(ByVal value As String)
            _RightFormat = value
        End Set
    End Property

    ''' <summary>
    ''' 平常の背景色
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefaultBackColor() As String
        Get
            DefaultBackColor = _DefaultBackColor
        End Get
        Set(ByVal value As String)
            _DefaultBackColor = value
            MyBase.BackColor = Color.FromName(value)
        End Set
    End Property

    ''' <summary>
    ''' 使用不可の背景色
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReadOnlyBackColor() As String
        Get
            ReadOnlyBackColor = _ReadOnlyBackColor
        End Get
        Set(ByVal value As String)
            _ReadOnlyBackColor = value
            If PageReadOnly Then
                MyBase.BackColor = Color.FromName(value)
            End If
        End Set
    End Property
#End Region

#Region "事件"
    ''' <summary>
    ''' 初期化時、コントロールを設定する
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            '各項目を設定する
            Call SetAttributes()
        End If

        If Not MyBase.Text = "" Then
            Dim strValue As String
            If IsNumeric(Me.Value) Then
                strValue = Format(Convert.ToInt64(Me.Value), "###,###,###,###,###,##0")
                MyBase.Text = Me.LeftFormat & strValue & Me.RightFormat

                'マイナスの場合、数値を赤文字にする
                If Convert.ToDouble(Me.Value) < 0 Then
                    Me.ForeColor = Color.FromName(CON_FONTCOLOR)
                End If
            Else
                MyBase.Text = ""
            End If
        End If

        MyBase.OnLoad(e)
    End Sub

    ''' <summary>
    ''' グリッド表示する時、コントロールを再設定する
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub DataBind()
        If Page.IsPostBack Then
            '各項目を設定する
            Call SetAttributes()
        End If

        MyBase.DataBind()
    End Sub

#End Region

#Region "メンッド"
    ''' <summary>
    ''' javascriptで各項目と事件を設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetAttributes()
        '入力モード
        Me.Attributes("style") = Me.Attributes("style") & _
                                 "ime-mode:disabled;text-align:right;"

        'フォーカス
        Me.Attributes("onfocus") = Me.Attributes("onfocus") & _
                                  "if(readOnly==true){return true;}SetNumberFocusEnter(this,'" & LeftFormat & "','" & RightFormat & "');this.select();"

        'フォーカスアウト
        Me.Attributes("onfocusout") = "if(readOnly==true){return true;}if(!focusoutFlg){return true;}if(!SetNumberFocusOut(this,'" & ObjName & "','" & LeftFormat & "','" & RightFormat & "')){return false;}" & _
                                      Me.Attributes("onfocusout")

        'Enter
        Me.Attributes("onkeypress") = "if (event.keyCode>=48 && event.keyCode<=57){}else{return false;}" & _
                                      Me.Attributes("onkeypress")

        'Keydown
        Me.Attributes("onkeydown") = "if (event.keyCode==13){event.keyCode=9;}" & _
                                     Me.Attributes("onkeydown")

        If PageReadOnly Then
            Me.Attributes("readOnly") = "true"
            Me.Attributes("tabIndex") = "-1"
            Me.Attributes("style") = Me.Attributes("style") & _
                                 "background-color:" & ReadOnlyBackColor & ";"
        End If
    End Sub

    ''' <summary>
    ''' 使用不可を設定する
    ''' </summary>
    ''' <param name="blnReadOnlyFlg">True:使用不可、False:使用可</param>
    ''' <remarks></remarks>
    Public Sub SetReadOnly(ByVal blnReadOnlyFlg As Boolean)
        If blnReadOnlyFlg Then
            PageReadOnly = True
            Me.Attributes("readOnly") = "true"
            Me.Attributes("tabIndex") = "-1"
            Me.Attributes("style") = Me.Attributes("style") & _
                                 "background-color:" & ReadOnlyBackColor & ";"
        Else
            PageReadOnly = False
            Me.Attributes("readOnly") = "false"
            Me.Attributes("tabIndex") = "0"
            Me.Attributes("style") = Me.Attributes("style") & _
                                 "background-color:" & DefaultBackColor & ";"
        End If
    End Sub
#End Region
End Class
