Option Explicit On
Option Strict On

Imports System.Web
Imports System.Drawing
Imports System.Text

''' <summary>
''' ���l�R���g���[��
''' </summary>
''' <remarks>���l�R���g���[��</remarks>
''' <history>
''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
''' </history>
Public Class CommonNumber
    Inherits System.Web.UI.WebControls.TextBox

#Region "�萔"
    Private Const CON_READONLY_BGCOLOR As String = "#D0D0D0"    '�g�p�s�̔w�i�F
    Private Const CON_DEFAULT_BGCOLOR As String = "white"       '����̔w�i�F
    Private Const CON_FONTCOLOR As String = "red"               '�}�C�i�X�̏ꍇ�A�ԕ����ɂ���
#End Region

#Region "�ϐ�"
    Private _ObjName As String = String.Empty
    Private _PageReadOnly As Boolean = False
    Private _LeftFormat As String = String.Empty
    Private _RightFormat As String = String.Empty
    Private _DefaultBackColor As String = CON_DEFAULT_BGCOLOR
    Private _ReadOnlyBackColor As String = CON_READONLY_BGCOLOR
#End Region

#Region "�v���p�e�B"
    ''' <summary>
    ''' �e�L�X�g�̎擾
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

                    '�}�C�i�X�̏ꍇ�A���l��ԕ����ɂ���
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
    ''' �g�p�s�̐ݒ�
    ''' </summary>
    ''' <value>True:�g�p�s�AFalse:�g�p��</value>
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
    ''' ���ږ���(���b�Z�[�W�\���p)
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
    ''' ���ɕ�����ǉ�����
    ''' </summary>
    ''' <value>���ɕ���(��:$, \)</value>
    ''' <returns></returns>
    ''' <remarks>���ɕ�����ǉ�����</remarks>
    Public Property LeftFormat() As String
        Get
            LeftFormat = _LeftFormat
        End Get
        Set(ByVal value As String)
            _LeftFormat = value
        End Set
    End Property

    ''' <summary>
    ''' �E�ɕ�����ǉ�����
    ''' </summary>
    ''' <value>�E�ɕ���(��:%)</value>
    ''' <returns></returns>
    ''' <remarks>�E�ɕ�����ǉ�����</remarks>
    Public Property RightFormat() As String
        Get
            RightFormat = _RightFormat
        End Get
        Set(ByVal value As String)
            _RightFormat = value
        End Set
    End Property

    ''' <summary>
    ''' ����̔w�i�F
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
    ''' �g�p�s�̔w�i�F
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

#Region "����"
    ''' <summary>
    ''' ���������A�R���g���[����ݒ肷��
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            '�e���ڂ�ݒ肷��
            Call SetAttributes()
        End If

        If Not MyBase.Text = "" Then
            Dim strValue As String
            If IsNumeric(Me.Value) Then
                strValue = Format(Convert.ToInt64(Me.Value), "###,###,###,###,###,##0")
                MyBase.Text = Me.LeftFormat & strValue & Me.RightFormat

                '�}�C�i�X�̏ꍇ�A���l��ԕ����ɂ���
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
    ''' �O���b�h�\�����鎞�A�R���g���[�����Đݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub DataBind()
        If Page.IsPostBack Then
            '�e���ڂ�ݒ肷��
            Call SetAttributes()
        End If

        MyBase.DataBind()
    End Sub

#End Region

#Region "�����b�h"
    ''' <summary>
    ''' javascript�Ŋe���ڂƎ�����ݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetAttributes()
        '���̓��[�h
        Me.Attributes("style") = Me.Attributes("style") & _
                                 "ime-mode:disabled;text-align:right;"

        '�t�H�[�J�X
        Me.Attributes("onfocus") = Me.Attributes("onfocus") & _
                                  "if(readOnly==true){return true;}SetNumberFocusEnter(this,'" & LeftFormat & "','" & RightFormat & "');this.select();"

        '�t�H�[�J�X�A�E�g
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
    ''' �g�p�s��ݒ肷��
    ''' </summary>
    ''' <param name="blnReadOnlyFlg">True:�g�p�s�AFalse:�g�p��</param>
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
