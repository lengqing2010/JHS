
Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic

''' <summary>
''' �����X��{���_���̑�
''' </summary>
''' <remarks></remarks>
Partial Public Class kameiten_sonota
    Inherits System.Web.UI.UserControl

#Region "���ʕϐ�"
    '���ʂ�MESSAGE�@OBJECT
    Private CommonMsgAndFocusBL As New kihonjyouhou.MessageAndFocus()
    '���ʂ̃`�F�b�N
    Private CommonCheckFuc As New CommonCheck()

#End Region

#Region "�v���p�e�B"

    Public _kameiten_cd As String
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property kameiten_cd() As String
        Get
            Return _kameiten_cd
        End Get
        Set(ByVal value As String)
            _kameiten_cd = value
        End Set
    End Property

    '����
    Private _kenngenn As Boolean
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get
        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set
    End Property

    '�X�V��
    Private _upd_login_user_id As String
    Public Property Upd_login_user_id() As String
        Get
            Return _upd_login_user_id
        End Get

        Set(ByVal value As String)
            _upd_login_user_id = value
        End Set

    End Property

    '�����X���
    Private _kameitenTableDataTable As DataAccess.KameitenDataSet.m_kameitenTableDataTable
    Public Property KameitenTableDataTable() As DataAccess.KameitenDataSet.m_kameitenTableDataTable
        Get
            Return _kameitenTableDataTable
        End Get

        Set(ByVal value As DataAccess.KameitenDataSet.m_kameitenTableDataTable)
            _kameitenTableDataTable = value
        End Set
    End Property

#End Region

#Region "���"

    ''' <summary>
    ''' PAGE LOAD
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '��ʐݒ�
            GamenInit()
        End If

    End Sub

    ''' <summary>
    ''' �o�^�{�^������
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '��ʂ�INPUT�`�F�b�N�@OK��
        If GamenInputCheck() Then

            '�r�������X�`�F�b�N
            Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
            If Not otherPageFunction.DoFunction(Parent.Page, "Haitakameiten") Then
                Exit Sub
            End If

            '�o�^���� 
            Dim KihonJyouhouInquiryLogic As New BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
            Dim insdata As New DataAccess.KameitenDataSet.m_kameitenTableDataTable
            Dim dr As DataAccess.KameitenDataSet.m_kameitenTableRow
            dr = insdata.NewRow

            If Me.tbxDanmenzu1.Text <> "" Then
                dr.danmenzu1 = Me.tbxDanmenzu1.Text
                Me.tbxDanmenzu1.Text = CInt(Me.tbxDanmenzu1.Text)
            End If
            If Me.tbxDanmenzu2.Text <> "" Then
                dr.danmenzu2 = Me.tbxDanmenzu2.Text
                Me.tbxDanmenzu2.Text = CInt(Me.tbxDanmenzu2.Text)
            End If

            If Me.tbxDanmenzu3.Text <> "" Then
                dr.danmenzu3 = Me.tbxDanmenzu3.Text
                Me.tbxDanmenzu3.Text = CInt(Me.tbxDanmenzu3.Text)
            End If

            If Me.tbxDanmenzu4.Text <> "" Then
                dr.danmenzu4 = Me.tbxDanmenzu4.Text
                Me.tbxDanmenzu4.Text = CInt(Me.tbxDanmenzu4.Text)
            End If

            If Me.tbxDanmenzu5.Text <> "" Then
                dr.danmenzu5 = Me.tbxDanmenzu5.Text
                Me.tbxDanmenzu5.Text = CInt(Me.tbxDanmenzu5.Text)
            End If

            If Me.tbxDanmenzu6.Text <> "" Then
                dr.danmenzu6 = Me.tbxDanmenzu6.Text
                Me.tbxDanmenzu6.Text = CInt(Me.tbxDanmenzu6.Text)
            End If

            If Me.tbxDanmenzu7.Text <> "" Then
                dr.danmenzu7 = Me.tbxDanmenzu7.Text
                Me.tbxDanmenzu7.Text = CInt(Me.tbxDanmenzu7.Text)
            End If

            dr.upd_login_user_id = _upd_login_user_id
            insdata.Rows.Add(dr)

            '���̑�
            If KihonJyouhouInquiryLogic.SetkameitenInfo(_kameiten_cd, insdata) Then
                ShowMsg(Messages.Instance.MSG018S, Me.btnTouroku, "���̑�")
                '�ŐV�X�V����---�X�V
                otherPageFunction.DoFunction(Parent.Page, "SetKyoutuuKousin")
            Else
                ShowMsg(Messages.Instance.MSG019E, Me.tbxDanmenzu1, "���̑�")
            End If

        End If

    End Sub

    ''' <summary>
    ''' ���̑�LINK�{�^������
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub lbtnSonota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '���ׂ�\��
        If meisaiTbody.Style.Item("display") = "none" Then
            meisaiTbody.Style.Item("display") = "inline"
            btnTouroku.Style.Item("display") = "inline"
        Else
            meisaiTbody.Style.Item("display") = "none"
            btnTouroku.Style.Item("display") = "none"
        End If
    End Sub

#End Region

#Region "����"

    ''' <summary>
    ''' ��ʂ̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GamenInit()

        '�{�^���̌����ݒ�
        Me.btnTouroku.Enabled = _kenngenn
        meisaiTbody.Style.Item("display") = "none"

        '�f�ʐ}�̐ݒ�
        If _kameitenTableDataTable.Rows.Count > 0 Then
            Me.tbxDanmenzu1.Text = _kameitenTableDataTable(0).danmenzu1
            Me.tbxDanmenzu2.Text = _kameitenTableDataTable(0).danmenzu2
            Me.tbxDanmenzu3.Text = _kameitenTableDataTable(0).danmenzu3
            Me.tbxDanmenzu4.Text = _kameitenTableDataTable(0).danmenzu4
            Me.tbxDanmenzu5.Text = _kameitenTableDataTable(0).danmenzu5
            Me.tbxDanmenzu6.Text = _kameitenTableDataTable(0).danmenzu6
            Me.tbxDanmenzu7.Text = _kameitenTableDataTable(0).danmenzu7
        End If

    End Sub


    ''' <summary>
    ''' �`�F�b�N
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GamenInputCheck() As Boolean
        Dim csScript As New StringBuilder
        Dim MsgTotal As String
        Dim CltID As String

        TbxItemInputCheck(Me.tbxDanmenzu1, "�f�ʐ}1", "���p����", , 1)
        TbxItemInputCheck(Me.tbxDanmenzu1, "�f�ʐ}1", "����", 4)

        TbxItemInputCheck(Me.tbxDanmenzu2, "�f�ʐ}2", "���p����", , 1)
        TbxItemInputCheck(Me.tbxDanmenzu2, "�f�ʐ}2", "����", 4)

        TbxItemInputCheck(Me.tbxDanmenzu3, "�f�ʐ}3", "���p����", , 1)
        TbxItemInputCheck(Me.tbxDanmenzu3, "�f�ʐ}3", "����", 4)

        TbxItemInputCheck(Me.tbxDanmenzu4, "�f�ʐ}4", "���p����", , 1)
        TbxItemInputCheck(Me.tbxDanmenzu4, "�f�ʐ}4", "����", 4)

        TbxItemInputCheck(Me.tbxDanmenzu5, "�f�ʐ}5", "���p����", , 1)
        TbxItemInputCheck(Me.tbxDanmenzu5, "�f�ʐ}5", "����", 4)

        TbxItemInputCheck(Me.tbxDanmenzu6, "�f�ʐ}6", "���p����", , 1)
        TbxItemInputCheck(Me.tbxDanmenzu6, "�f�ʐ}6", "����", 4)

        TbxItemInputCheck(Me.tbxDanmenzu7, "�f�ʐ}7", "���p����", , 1)
        TbxItemInputCheck(Me.tbxDanmenzu7, "�f�ʐ}7", "����", 4)


        MsgTotal = CommonMsgAndFocusBL.Message

        If MsgTotal <> "" Then
            CltID = CommonMsgAndFocusBL.focusCtrl.ClientID
            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err", csScript.Append("alert('" & MsgTotal & "');objEBI('" & CltID & "').select();").ToString, True)
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' �����`�F�b�N
    ''' </summary>
    ''' <param name="control"></param>
    ''' <param name="MsgParam"></param>
    ''' <param name="kbn"></param>
    ''' <param name="len"></param>
    ''' <param name="Flg"></param>
    ''' <param name="kakaFlg"></param>
    ''' <remarks></remarks>
    Protected Sub TbxItemInputCheck(ByVal control As System.Web.UI.Control, _
                                ByVal MsgParam As String, _
                                ByVal kbn As String, _
                                Optional ByVal len As Int64 = 0, _
                                Optional ByVal Flg As Int16 = 0, _
                                Optional ByVal kakaFlg As WebUI.kbn = WebUI.kbn.HANKAKU)

        Dim checkKekka As String = ""

        If CType(control, TextBox).Text.ToString.Trim <> "" Then
            Select Case kbn
                Case "���p����"
                    checkKekka = CommonCheckFuc.CheckHankaku(CType(control, TextBox).Text.ToString, MsgParam, "1")
                Case "����"
                    checkKekka = CommonCheckFuc.CheckByte(CType(control, TextBox).Text.ToString, len, MsgParam, kakaFlg)
            End Select
        End If

        If checkKekka <> "" Then
            CommonMsgAndFocusBL.Append(checkKekka)
            CommonMsgAndFocusBL.AppendFocusCtrl(control)
        End If

    End Sub

    ''' <summary>
    ''' MsgBox�\��
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="param1"></param>
    ''' <param name="param2"></param>
    ''' <param name="param3"></param>
    ''' <param name="param4"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg(ByVal msg As String, _
                                ByVal control As System.Web.UI.Control, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "")

        Dim csScript As New StringBuilder


        Dim pPage As Page = Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("ShowMsg")

        msg = msg.Replace("@PARAM1", param1) _
                          .Replace("@PARAM2", param2) _
                          .Replace("@PARAM3", param3) _
                          .Replace("@PARAM4", param4)

        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {control.ClientID, csScript.AppendFormat( _
                                                                "" & msg & "", _
                                                                param1, param2, param3, param4).ToString _
                                                                })
        End If


    End Sub

#End Region

End Class