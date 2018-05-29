Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports System.Data

''' <summary>
''' �N�x�v��l�ݒ�
''' </summary>
''' <remarks>�N�x�v��l�ݒ�</remarks>
''' <history>
''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
''' </history>
Partial Class NendoKeikakuInput
    Inherits System.Web.UI.Page

#Region "�v���C�x�[�g�ϐ�"
    '�C���X�^���X����
    Private objNendoKeikakuInputBC As New Lixil.JHS_EKKS.BizLogic.NendoKeikakuInputBC
    Private objCommonBC As New Lixil.JHS_EKKS.BizLogic.CommonBC
    Private objCommon As New Common
    Private objCommonCheck As New CommonCheck
#End Region

#Region "�萔"
    '���j���[�ԍ�
    Private Const conMenuno As String = "K002"
#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim strYear As String
        Dim strUserID As String = ""

        If Not IsPostBack Then
            objCommonCheck.CommonNinsyou(strUserID, Me.Master.loginUserInfo, kegen.UserIdOnly)

            '���O�C�����[�U�[��ۑ�����
            ViewState("LoginUserID") = strUserID

            '�V�X�e���������擾����
            strYear = objCommon.GetSystemYear()

            '�����N�x���X�g�ɐݒ肷��
            Me.drpNendo.DataSource = objCommonBC.GetKeikakuNendoData()
            Me.drpNendo.DataBind()

            '�����N�x��ݒ肷��
            Me.drpNendo.SelectedIndex = -1

            '�V�X�e���N�x��ݒ肷��
            For i As Integer = 0 To Me.drpNendo.Items.Count - 1
                If Me.drpNendo.Items(i).Value.Equals(Convert.ToString(strYear)) Then
                    Me.drpNendo.Items(i).Selected = True
                    Exit For
                End If
            Next

            '�N�x�𔻒f����
            If Me.drpNendo.SelectedValue.Equals(String.Empty) Then
                '�N�x���󔒂̏ꍇ
                Call SetPageClear()
            Else
                '�x�X�ʂ̌v��N�x��ݒ肷��
                Me.lblNendo.Text = Me.drpNendo.SelectedValue & "�N�x"

                '�N�x�ɂ��A�S�ЂƊe�x�X�̃f�[�^��ݒ肷��
                Call SetNendoData(strYear)
            End If
        End If

        '��ʂ�JS EVENT�ݒ�
        Call SetJsEvent()
    End Sub

    ''' <summary>
    ''' �N�x��I�����鎞
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Protected Sub drpNendo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpNendo.SelectedIndexChanged
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim strNendo As String              '�N�x

        '�N�x���擾����
        strNendo = Me.drpNendo.SelectedValue

        If strNendo = "" Then
            Call SetPageClear()
        Else
            Me.tbSum.Visible = True

            Me.btnKensuuCopy.Enabled = True
            Me.btnUriKingakuCopy.Enabled = True
            Me.btnArariCopy.Enabled = True

            '�x�X�ʂ̔N�x��ݒ肷��
            Me.lblNendo.Text = strNendo & "�N�x"

            '�N�x�ɂ��A�S�ЂƊe�x�X�̃f�[�^��ݒ肷��
            Call SetNendoData(strNendo)

        End If

    End Sub

    ''' <summary>
    ''' ���אݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Protected Sub grdMeisai_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles grdMeisai.ItemDataBound
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim intRowIndex As Integer
        intRowIndex = e.Item.ItemIndex

        '���׃��R�[�h��ݒ肷��
        If Not Me.grdMeisai.DataSource Is Nothing Then
            CType(e.Item.FindControl("numKeikaku1_1"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku1_1"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku1_2"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku1_2"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku1_3"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku1_3"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku2_1"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku2_1"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku2_2"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku2_2"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku2_3"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku2_3"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku3_1"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku3_1"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku3_2"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku3_2"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku3_3"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku3_3"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"
        End If
    End Sub

    ''' <summary>
    ''' �S�Ђ̌v��l�ۑ��{�^�����N���b�N���鎞
    ''' </summary>
    ''' <returns>���ʏ����t���O</returns>
    ''' <remarks>�S�Ђ̌v��l�ۑ��{�^�����N���b�N���鎞</remarks>
    ''' <history>
    ''' <para>2012/11/22 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function BtnAllSave_Click() As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim hsData As Hashtable
        Dim strYear As String               '�v��N�x

        '�v��N�x���擾����
        strYear = Me.drpNendo.SelectedValue

        '�v��N�x�̕K���̓`�F�b�N
        If strYear.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG009E, "�v��N�x")
            Me.drpNendo.Focus()
            Return False
        End If

        If Not Me.txtKome.Text.Trim().Equals(String.Empty) Then
            '�֑������`�F�b�N
            If Not objCommonCheck.kinsiStrCheck(Me.txtKome.Text.Trim()) Then
                objCommon.SetShowMessage(Me, CommonMessage.MSG068E, "�R�����g")
                Me.txtKome.Focus()
                Return False
            End If
        End If

        '�S�Ђ̌v��f�[�^���擾����
        hsData = GetZensyaData(0)

        '�S�Ђ̌v��f�[�^��DB�ɓo�^����
        objNendoKeikakuInputBC.SetZensyaKeikakuKanriData(hsData)

        '�N�x�ɂ��A�S�ЂƊe�x�X�̃f�[�^��ݒ肷��
        Call SetNendoData(strYear)

        '�����v���Čv�Z����
        Call SetSumData()

        '�ۑ�����������A�m�F���b�Z�[�W��\������
        objCommon.SetShowMessage(Me, CommonMessage.MSG012E, Me.drpNendo.SelectedValue)

        Return True
    End Function

    ''' <summary>
    ''' �S�Ђ̊m��{�^�����N���b�N���鎞
    ''' </summary>
    ''' <returns>���ʏ����t���O</returns>
    ''' <remarks>�S�Ђ̊m��{�^�����N���b�N���鎞</remarks>
    ''' <history>
    ''' <para>2012/11/22 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function BtnAllConfirm_Click() As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim hsData As Hashtable
        Dim strYear As String               '�v��N�x
        Dim strMaxAddDateTime As String     '�ő�o�^����(�֘A�`�F�b�N�p)
        Dim blnKakuteiFlg As Boolean        '�m��t���O

        '�m��t���O�̐ݒ�
        If Me.btnAllSave.Enabled Then
            blnKakuteiFlg = True
        Else
            blnKakuteiFlg = False
        End If

        '�v��N�x���擾����
        strYear = Me.drpNendo.SelectedValue

        '�v��N�x�̕K���̓`�F�b�N
        If strYear.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG009E, "�v��N�x")
            Me.drpNendo.Focus()
            Return False
        End If

        '�v�撲�������̕K���̓`�F�b�N
        If Me.numKeikakuKensuu.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "�v�撲������")
            Me.numKeikakuKensuu.Focus()
            Return False
        End If

        '�v�攄����z�̕K���̓`�F�b�N
        If Me.numKeikakuUriKingaku.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "�v�攄����z")
            Me.numKeikakuUriKingaku.Focus()
            Return False
        End If

        '�v��e�����z�̕K���̓`�F�b�N
        If Me.numKeikakuArari.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "�v��e�����z")
            Me.numKeikakuArari.Focus()
            Return False
        End If

        '���N�x�Ŋ��Ɋm��f�[�^������ꍇ
        If Not blnKakuteiFlg Then
            '�R�����g�̕K���̓`�F�b�N
            If Me.txtKome.Text.Trim().Equals(String.Empty) Then
                objCommon.SetShowMessage(Me, CommonMessage.MSG014E)
                Me.txtKome.Focus()
                Return False
            End If

            '�֑������`�F�b�N
            If Not objCommonCheck.kinsiStrCheck(Me.txtKome.Text.Trim()) Then
                objCommon.SetShowMessage(Me, CommonMessage.MSG068E, "�R�����g")
                Me.txtKome.Focus()
                Return False
            End If
        End If

        '�֘A�`�F�b�N
        If Not ViewState("MaxAddDateTime") Is Nothing Then
            '�Y���N�x�̍ő�o�^�������擾����(�x�X����)
            strMaxAddDateTime = objNendoKeikakuInputBC.GetMaxSitenbetuKeikakuKanriData(strYear)

            '�Y���N�x�̍ő�o�^�����Ɩ��ׂ̔N�x���r����
            If Not Convert.ToString(ViewState("MaxAddDateTime")).Equals(strMaxAddDateTime) Then
                '�G���[���b�Z�[�W��\������
                objCommon.SetShowMessage(Me, CommonMessage.MSG042E)
                Return False
            End If
        End If

        '�S�Ђ̌v��f�[�^���擾����
        hsData = GetZensyaData(1)

        '�S�Ђ̌v��f�[�^��DB�ɓo�^����
        objNendoKeikakuInputBC.SetZensyaKeikakuKanriData(hsData)

        '�N�x�ɂ��A�S�ЂƊe�x�X�̃f�[�^��ݒ肷��
        Call SetNendoData(strYear)

        '�����v���Čv�Z����
        Call SetSumData()

        '���N�x�Ŋ��Ɋm��f�[�^������ꍇ
        If Not blnKakuteiFlg Then
            '�m�芮��������A�m�F���b�Z�[�W��\������
            objCommon.SetShowMessage(Me, CommonMessage.MSG015E)
        Else
            '�m�芮��������A�m�F���b�Z�[�W��\������
            objCommon.SetShowMessage(Me, CommonMessage.MSG013E, Me.drpNendo.SelectedValue)
        End If

        Return True
    End Function

    ''' <summary>
    ''' �x�X�ʂ̌v��l�ۑ��{�^�����N���b�N���鎞
    ''' </summary>
    ''' <returns>���ʏ����t���O</returns>
    ''' <remarks>�x�X�ʂ̌v��l�ۑ��{�^�����N���b�N���鎞</remarks>
    ''' <history>
    ''' <para>2012/11/22 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function BtnSitenbetuSave_Click() As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtValue As DataTable
        Dim strYear As String               '�v��N�x

        '�v��N�x���擾����
        strYear = Me.drpNendo.SelectedValue

        '�v��N�x�̕K���̓`�F�b�N
        If strYear.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG009E, "�v��N�x")
            Me.drpNendo.Focus()
            Return False
        End If

        '�x�X�̌v��f�[�^���擾����
        dtValue = GetSitenbetuData(0)

        '�S�Ђ̌v��f�[�^��DB�ɓo�^����
        objNendoKeikakuInputBC.SetSitenbetuKeikakuKanriData(dtValue)

        '�N�x�ɂ��A�S�ЂƊe�x�X�̃f�[�^��ݒ肷��
        Call SetNendoData(strYear)

        '�����v���Čv�Z����
        Call SetSumData()

        '�ۑ�����������A�m�F���b�Z�[�W��\������
        objCommon.SetShowMessage(Me, CommonMessage.MSG016E)

        Return True
    End Function

    ''' <summary>
    ''' �x�X�ʂ̊m��{�^�����N���b�N���鎞
    ''' </summary>
    ''' <returns>���ʏ����t���O</returns>
    ''' <remarks>�x�X�ʂ̊m��{�^�����N���b�N���鎞</remarks>
    ''' <history>
    ''' <para>2012/11/22 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function BtnSitenbetuConfirm_Click() As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtValue As DataTable
        Dim strYear As String               '�v��N�x
        Dim strMaxAddDateTime As String     '�ő�o�^����(�֘A�`�F�b�N�p)
        Dim strMsg As String                '�G���[���b�Z�[�W���e
        Dim blnKakuteiFlg As Boolean        '�m��t���O

        '�m��t���O�̐ݒ�
        If Me.btnSitenbetuSave.Enabled Then
            blnKakuteiFlg = True
        Else
            blnKakuteiFlg = False
        End If

        '�v��N�x���擾����
        strYear = Me.drpNendo.SelectedValue

        '�v��N�x�̕K���̓`�F�b�N
        If strYear.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG009E, "�v��N�x")
            Me.drpNendo.Focus()
            Return False
        End If

        '�v�撲�������̕K���̓`�F�b�N
        If Me.numKeikakuKensuu.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "�v�撲������")
            Me.numKeikakuKensuu.Focus()
            Return False
        End If

        '�v�攄����z�̕K���̓`�F�b�N
        If Me.numKeikakuUriKingaku.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "�v�攄����z")
            Me.numKeikakuUriKingaku.Focus()
            Return False
        End If

        '�v��e�����z�̕K���̓`�F�b�N
        If Me.numKeikakuArari.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "�v��e�����z")
            Me.numKeikakuArari.Focus()
            Return False
        End If

        '��ʊ֘A�`�F�b�N
        If Not Me.numKeikaku1_4_Sum.Value.Equals(Me.numKeikakuKensuu.Value) OrElse _
           Not Me.numKeikaku2_4_Sum.Value.Equals(Me.numKeikakuUriKingaku.Value) OrElse _
           Not Me.numKeikaku3_4_Sum.Value.Equals(Me.numKeikakuArari.Value) Then
            '�m�F���b�Z�[�W��\������
            objCommon.SetShowMessage(Me, CommonMessage.MSG017E)

            strMsg = ""

            '�x�X�ʌv��e���ځ@<�@�S�̌v��e���ځ@�̏ꍇ
            If objCommon.GetLongFromObj(Me.numKeikaku1_4_Sum.Value) < _
                objCommon.GetLongFromObj(Me.numKeikakuKensuu.Value) Then
                strMsg = CommonMessage.MSG018E.Replace("{0}", "��������") & "\r\n"
            End If

            If objCommon.GetLongFromObj(Me.numKeikaku2_4_Sum.Value) < _
                objCommon.GetLongFromObj(Me.numKeikakuUriKingaku.Value) Then
                strMsg = strMsg & CommonMessage.MSG018E.Replace("{0}", "������z") & "\r\n"
            End If

            If objCommon.GetLongFromObj(Me.numKeikaku3_4_Sum.Value) < _
                objCommon.GetLongFromObj(Me.numKeikakuArari.Value) Then
                strMsg = strMsg & CommonMessage.MSG018E.Replace("{0}", "�e�����z") & "\r\n"
            End If

            If strMsg <> "" Then
                strMsg = strMsg.Remove(strMsg.LastIndexOf("\r\n"))
                '�G���[���b�Z�[�W��\������
                objCommon.SetShowMessage(Me, strMsg)
                Return False
            End If
        End If

        'DB�֘A�`�F�b�N
        '�Y���N�x�̍ő�o�^�������擾����(�S�Ђ���)
        strMaxAddDateTime = objNendoKeikakuInputBC.GetMaxZensyaKeikakuKanriData(strYear)

        '�ő�o�^�����̗L���𔻒f����
        If Not strMaxAddDateTime.Equals(String.Empty) Then
            '�S�Ђ̃f�[�^�̗L���𔻒f����
            If Not ViewState("ZensyaData") Is Nothing Then
                '�Y���N�x�̍ő�o�^�����Ɩ��ׂ̔N�x���r����
                If Not Convert.ToString(CType(ViewState("ZensyaData"), DataTable).Rows(0)("add_datetime")).Equals(strMaxAddDateTime) Then
                    '�G���[���b�Z�[�W��\������
                    objCommon.SetShowMessage(Me, CommonMessage.MSG041E)
                    Return False
                End If
            End If
        End If

        '�x�X�̌v��f�[�^���擾����
        dtValue = GetSitenbetuData(1)

        '�S�Ђ̌v��f�[�^��DB�ɓo�^����
        objNendoKeikakuInputBC.SetSitenbetuKeikakuKanriData(dtValue)

        '�N�x�ɂ��A�S�ЂƊe�x�X�̃f�[�^��ݒ肷��
        Call SetNendoData(strYear)

        '�����v���Čv�Z����
        Call SetSumData()

        '���N�x�Ŋ��Ɋm��f�[�^������ꍇ
        If Not blnKakuteiFlg Then
            '�m�芮��������A�m�F���b�Z�[�W��\������
            objCommon.SetShowMessage(Me, CommonMessage.MSG021E)
        Else
            '�m�芮��������A�m�F���b�Z�[�W��\������
            objCommon.SetShowMessage(Me, CommonMessage.MSG019E)
        End If

        Return True
    End Function

    ''' <summary>
    ''' ���ʌv��l�ݒ�{�^�����N���b�N���鎞
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSitenbetu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSitenbetu.Click
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        '���ʌv��l�ݒ��ʂ֑J�ڂ���
        Server.Transfer("SitenTukibetuKeikakuchiSearchList.aspx")
    End Sub
#End Region

#Region "�����b�h"
    ''' <summary>
    ''' ��ʂ�JS EVENT�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/05/28 P-42186 ���V �V�K�쐬 </para>																															
    ''' </history>	
    Private Sub SetJsEvent()
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '�S�Ђ̌v��l�ۑ��{�^��������
        Me.btnAllSave.OnClick = "BtnAllSave_Click()"

        '�S�Ђ̊m��{�^��������
        Me.btnAllConfirm.OnClick = "BtnAllConfirm_Click()"

        '�x�X�ʂ̌v��l�ۑ��{�^��������
        Me.btnSitenbetuSave.OnClick = "BtnSitenbetuSave_Click()"

        '�x�X�ʂ̊m��{�^��������
        Me.btnSitenbetuConfirm.OnClick = "BtnSitenbetuConfirm_Click()"

    End Sub

    ''' <summary>
    ''' �N�x�ɂ��A�S�ЂƊe�x�X�̃f�[�^��ݒ肷��
    ''' </summary>
    ''' <param name="strNendo">�N�x</param>
    ''' <remarks>�N�x�ɂ��A�S�ЂƊe�x�X�̃f�[�^��ݒ肷��</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Private Sub SetNendoData(ByVal strNendo As String)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strNendo)

        Dim dtZensyaData As DataTable           '�S�Ќv��f�[�^
        Dim dtSitenbetuData As DataTable        '�x�X�ʃf�[�^
        Dim strKakuteiFlg As String             '�m��FLG
        Dim intSysYearFlg As Integer            '�V�X�e���N�x�t���O(0:�V�X�e���N�x���Ȃ��A1:�V�X�e���N�x)

        '�V�X�e���N�x�𔻒f����
        If objCommon.GetSystemYear().Equals(strNendo) Then
            '�V�X�e���N�x
            intSysYearFlg = 1

            '�ۑ��{�^���Ɗm��{�^�����g�p�s�ɂȂ�
            Me.btnAllSave.Enabled = True
            Me.btnAllConfirm.Enabled = True
            Me.btnSitenbetuSave.Enabled = True
            Me.btnSitenbetuConfirm.Enabled = True
        Else
            '�V�X�e���N�x���Ȃ�
            intSysYearFlg = 0
        End If

        '�S�Е�
        dtZensyaData = objNendoKeikakuInputBC.GetZensyaKeikakuKanriData(strNendo)

        If Not dtZensyaData Is Nothing AndAlso dtZensyaData.Rows.Count > 0 Then
            '�S�Ќv��f�[�^�ɂ��A��ʍ��ڂ�ݒ肷��
            Me.numKeikakuKensuu.Value = Convert.ToString(dtZensyaData.Rows(0)("keikaku_kensuu"))
            Me.numKeikakuUriKingaku.Value = Convert.ToString(dtZensyaData.Rows(0)("keikaku_uri_kingaku"))
            Me.numKeikakuArari.Value = Convert.ToString(dtZensyaData.Rows(0)("keikaku_arari"))
            Me.txtKome.Text = Convert.ToString(dtZensyaData.Rows(0)("keikaku_settutei_kome"))
            strKakuteiFlg = Convert.ToString(dtZensyaData.Rows(0)("kakutei_flg"))

            '�S�Ђ̃f�[�^��ۑ�����
            ViewState("ZensyaData") = dtZensyaData
        Else
            '�w�b�_���̗l����ݒ肷��
            Me.tdMeisaiHead.Attributes.Add("style", "")
            Me.numKeikakuKensuu.Value = ""
            Me.numKeikakuUriKingaku.Value = ""
            Me.numKeikakuArari.Value = ""
            Me.txtKome.Text = ""
            strKakuteiFlg = ""

            '�S�Ђ̃f�[�^���N���A����
            ViewState("ZensyaData") = Nothing

            '�S�Ќv��f�[�^���������
            dtZensyaData.Dispose()
            dtZensyaData = Nothing
        End If

        '���N�x�Ŋ��Ɋm��f�[�^������ꍇ
        If strKakuteiFlg = "1" Then
            Me.btnAllSave.Enabled = False
        Else
            Me.btnAllSave.Enabled = True
        End If

        '�V�X�e���N�x�ɂ��A�x�X�ʂ̃f�[�^��߂�
        If intSysYearFlg = 1 Then
            '�V�X�e���N�x�̏ꍇ
            dtSitenbetuData = objNendoKeikakuInputBC.GetSitenbetuKeikakuKanriData(strNendo, 0)
        Else
            '�V�X�e���N�x���Ȃ��ꍇ
            dtSitenbetuData = objNendoKeikakuInputBC.GetSitenbetuKeikakuKanriData(strNendo, 0)
        End If

        If Not dtSitenbetuData Is Nothing AndAlso dtSitenbetuData.Rows.Count > 0 Then
            '�w�b�_���̗l����ݒ肷��
            Me.tdMeisaiHead.Attributes.Add("style", "border-bottom: black 2px solid;")
            Me.tbSum.Visible = True
            Me.grdMeisai.DataSource = dtSitenbetuData
            Me.grdMeisai.DataBind()

            Me.btnKensuuCopy.Enabled = True
            Me.btnUriKingakuCopy.Enabled = True
            Me.btnArariCopy.Enabled = True

            '���N�x�Ŋ��Ɋm��f�[�^������ꍇ
            If Convert.ToString(dtSitenbetuData.Rows(0)("kakutei_flg")).Equals("1") Then
                Me.btnSitenbetuSave.Enabled = False
            Else
                Me.btnSitenbetuSave.Enabled = True
            End If

            '�x�X�ʂ̃f�[�^��ۑ�����
            ViewState("SitenbetuData") = dtSitenbetuData

            '�Y���N�x�̍ő�o�^������ۑ�����
            ViewState("MaxAddDateTime") = objNendoKeikakuInputBC.GetMaxSitenbetuKeikakuKanriData(strNendo)
        Else
            '�w�b�_���̗l�����N���A����
            Me.tdMeisaiHead.Attributes.Add("style", "")
            Me.tbSum.Visible = False
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()
            Me.btnSitenbetuSave.Enabled = False
            Me.btnSitenbetuConfirm.Enabled = False
            Me.btnKensuuCopy.Enabled = False
            Me.btnUriKingakuCopy.Enabled = False
            Me.btnArariCopy.Enabled = False

            '�ۑ�����x�X�ʂ̃f�[�^���N���A����
            ViewState("SitenbetuData") = Nothing

            '�Y���N�x�̍ő�o�^������ۑ�����
            ViewState("MaxAddDateTime") = Nothing
        End If

        '�����v���Čv�Z����
        Call SetSumData()
    End Sub

    ''' <summary>
    ''' ���׃f�[�^�����v����
    ''' </summary>
    ''' <remarks>���׃f�[�^�����v����</remarks>
    Private Sub SetSumData()
        Dim intJittuseki1_1 As Long
        Dim intJittuseki1_2 As Long
        Dim intJittuseki1_3 As Long
        Dim intJittuseki1_4 As Long

        Dim intJittuseki2_1 As Long
        Dim intJittuseki2_2 As Long
        Dim intJittuseki2_3 As Long
        Dim intJittuseki2_4 As Long

        Dim intJittuseki3_1 As Long
        Dim intJittuseki3_2 As Long
        Dim intJittuseki3_3 As Long
        Dim intJittuseki3_4 As Long

        Dim intKeikaku1_1 As Long
        Dim intKeikaku1_2 As Long
        Dim intKeikaku1_3 As Long
        Dim intKeikaku1_4 As Long

        Dim intKeikaku2_1 As Long
        Dim intKeikaku2_2 As Long
        Dim intKeikaku2_3 As Long
        Dim intKeikaku2_4 As Long

        Dim intKeikaku3_1 As Long
        Dim intKeikaku3_2 As Long
        Dim intKeikaku3_3 As Long
        Dim intKeikaku3_4 As Long

        Dim i As Integer

        If Me.grdMeisai.DataSource Is Nothing Then
            Me.numJittuseki1_1_Sum.Value = "0"
            Me.numJittuseki1_2_Sum.Value = "0"
            Me.numJittuseki1_3_Sum.Value = "0"
            Me.numJittuseki1_4_Sum.Value = "0"

            Me.numJittuseki2_1_Sum.Value = "0"
            Me.numJittuseki2_2_Sum.Value = "0"
            Me.numJittuseki2_3_Sum.Value = "0"
            Me.numJittuseki2_4_Sum.Value = "0"

            Me.numJittuseki3_1_Sum.Value = "0"
            Me.numJittuseki3_2_Sum.Value = "0"
            Me.numJittuseki3_3_Sum.Value = "0"
            Me.numJittuseki3_4_Sum.Value = "0"

            Me.numKeikaku1_1_Sum.Value = "0"
            Me.numKeikaku1_2_Sum.Value = "0"
            Me.numKeikaku1_3_Sum.Value = "0"
            Me.numKeikaku1_4_Sum.Value = "0"

            Me.numKeikaku2_1_Sum.Value = "0"
            Me.numKeikaku2_2_Sum.Value = "0"
            Me.numKeikaku2_3_Sum.Value = "0"
            Me.numKeikaku2_4_Sum.Value = "0"

            Me.numKeikaku3_1_Sum.Value = "0"
            Me.numKeikaku3_2_Sum.Value = "0"
            Me.numKeikaku3_3_Sum.Value = "0"
            Me.numKeikaku3_4_Sum.Value = "0"
        Else

            For i = 0 To Me.grdMeisai.Items.Count - 1
                intJittuseki1_1 = intJittuseki1_1 + GetGridValue(i, "numJittuseki1_1")
                intJittuseki1_2 = intJittuseki1_2 + GetGridValue(i, "numJittuseki1_2")
                intJittuseki1_3 = intJittuseki1_3 + GetGridValue(i, "numJittuseki1_3")
                intJittuseki1_4 = intJittuseki1_4 + GetGridValue(i, "numJittuseki1_4")

                intJittuseki2_1 = intJittuseki2_1 + GetGridValue(i, "numJittuseki2_1")
                intJittuseki2_2 = intJittuseki2_2 + GetGridValue(i, "numJittuseki2_2")
                intJittuseki2_3 = intJittuseki2_3 + GetGridValue(i, "numJittuseki2_3")
                intJittuseki2_4 = intJittuseki2_4 + GetGridValue(i, "numJittuseki2_4")

                intJittuseki3_1 = intJittuseki3_1 + GetGridValue(i, "numJittuseki3_1")
                intJittuseki3_2 = intJittuseki3_2 + GetGridValue(i, "numJittuseki3_2")
                intJittuseki3_3 = intJittuseki3_3 + GetGridValue(i, "numJittuseki3_3")
                intJittuseki3_4 = intJittuseki3_4 + GetGridValue(i, "numJittuseki3_4")

                intKeikaku1_1 = intKeikaku1_1 + GetGridValue(i, "numKeikaku1_1")
                intKeikaku1_2 = intKeikaku1_2 + GetGridValue(i, "numKeikaku1_2")
                intKeikaku1_3 = intKeikaku1_3 + GetGridValue(i, "numKeikaku1_3")
                intKeikaku1_4 = intKeikaku1_4 + GetGridValue(i, "numKeikaku1_4")

                intKeikaku2_1 = intKeikaku2_1 + GetGridValue(i, "numKeikaku2_1")
                intKeikaku2_2 = intKeikaku2_2 + GetGridValue(i, "numKeikaku2_2")
                intKeikaku2_3 = intKeikaku2_3 + GetGridValue(i, "numKeikaku2_3")
                intKeikaku2_4 = intKeikaku2_4 + GetGridValue(i, "numKeikaku2_4")

                intKeikaku3_1 = intKeikaku3_1 + GetGridValue(i, "numKeikaku3_1")
                intKeikaku3_2 = intKeikaku3_2 + GetGridValue(i, "numKeikaku3_2")
                intKeikaku3_3 = intKeikaku3_3 + GetGridValue(i, "numKeikaku3_3")
                intKeikaku3_4 = intKeikaku3_4 + GetGridValue(i, "numKeikaku3_4")

            Next

            Me.numJittuseki1_1_Sum.Value = Convert.ToString(intJittuseki1_1)
            Me.numJittuseki1_2_Sum.Value = Convert.ToString(intJittuseki1_2)
            Me.numJittuseki1_3_Sum.Value = Convert.ToString(intJittuseki1_3)
            Me.numJittuseki1_4_Sum.Value = Convert.ToString(intJittuseki1_4)

            Me.numJittuseki2_1_Sum.Value = Convert.ToString(intJittuseki2_1)
            Me.numJittuseki2_2_Sum.Value = Convert.ToString(intJittuseki2_2)
            Me.numJittuseki2_3_Sum.Value = Convert.ToString(intJittuseki2_3)
            Me.numJittuseki2_4_Sum.Value = Convert.ToString(intJittuseki2_4)

            Me.numJittuseki3_1_Sum.Value = Convert.ToString(intJittuseki3_1)
            Me.numJittuseki3_2_Sum.Value = Convert.ToString(intJittuseki3_2)
            Me.numJittuseki3_3_Sum.Value = Convert.ToString(intJittuseki3_3)
            Me.numJittuseki3_4_Sum.Value = Convert.ToString(intJittuseki3_4)

            Me.numKeikaku1_1_Sum.Value = Convert.ToString(intKeikaku1_1)
            Me.numKeikaku1_2_Sum.Value = Convert.ToString(intKeikaku1_2)
            Me.numKeikaku1_3_Sum.Value = Convert.ToString(intKeikaku1_3)
            Me.numKeikaku1_4_Sum.Value = Convert.ToString(intKeikaku1_4)

            Me.numKeikaku2_1_Sum.Value = Convert.ToString(intKeikaku2_1)
            Me.numKeikaku2_2_Sum.Value = Convert.ToString(intKeikaku2_2)
            Me.numKeikaku2_3_Sum.Value = Convert.ToString(intKeikaku2_3)
            Me.numKeikaku2_4_Sum.Value = Convert.ToString(intKeikaku2_4)

            Me.numKeikaku3_1_Sum.Value = Convert.ToString(intKeikaku3_1)
            Me.numKeikaku3_2_Sum.Value = Convert.ToString(intKeikaku3_2)
            Me.numKeikaku3_3_Sum.Value = Convert.ToString(intKeikaku3_3)
            Me.numKeikaku3_4_Sum.Value = Convert.ToString(intKeikaku3_4)
        End If
    End Sub

    ''' <summary>
    ''' ���׍��ڂ̃f�[�^���擾����
    ''' </summary>
    ''' <param name="intRowIndex">�s�ԍ�</param>
    ''' <param name="strControlId">�R���g���[����</param>
    ''' <returns>���׍��ڂ̃f�[�^</returns>
    ''' <remarks>���׍��ڂ̃f�[�^���擾����</remarks>
    Private Function GetGridValue(ByVal intRowIndex As Integer, _
                                  ByVal strControlId As String) As Long
        Dim strValue As String

        strValue = CType(Me.grdMeisai.Items(intRowIndex).FindControl(strControlId), CommonNumber).Value

        If strValue = "" Then
            Return 0
        Else
            Return Convert.ToInt64(strValue)
        End If
    End Function

    ''' <summary>
    ''' ���׍��ڂ̃f�[�^���擾����
    ''' </summary>
    ''' <param name="intRowIndex">�s�ԍ�</param>
    ''' <param name="strControlId">�R���g���[����</param>
    ''' <param name="intReturnFlg">0:�󔒂̏ꍇ�A�u0�v��߂�A1:�󔒂̏ꍇ�A�uNULL�v��߂�</param>
    ''' <returns>���׍��ڂ̃f�[�^</returns>
    ''' <remarks>���׍��ڂ̃f�[�^���擾����</remarks>
    Private Function GetGridValue(ByVal intRowIndex As Integer, _
                                  ByVal strControlId As String, _
                                  ByVal intReturnFlg As Integer) As Object
        Dim strValue As String

        strValue = CType(Me.grdMeisai.Items(intRowIndex).FindControl(strControlId), CommonNumber).Value

        If strValue = "" Then
            If intReturnFlg = 0 Then
                Return 0
            Else
                Return DBNull.Value
            End If
        Else
            Return Convert.ToInt64(strValue)
        End If
    End Function

    ''' <summary>
    ''' �S�Ќv��f�[�^���擾����
    ''' </summary>
    ''' <param name="intButtonFlg">�ۑ��{�^���A�m��{�^���̋敪</param>
    ''' <returns>�S�Ќv��f�[�^</returns>
    ''' <remarks>�S�Ќv��f�[�^���擾����</remarks>
    Private Function GetZensyaData(ByVal intButtonFlg As Integer) As Hashtable
        Dim hsData As Hashtable

        hsData = New Hashtable

        hsData("keikaku_nendo") = Me.drpNendo.SelectedValue             '�N�x
        hsData("keikaku_kensuu") = Me.numKeikakuKensuu.Value            '�v�撲������
        hsData("keikaku_uri_kingaku") = Me.numKeikakuUriKingaku.Value   '�v�攄����z
        hsData("keikaku_arari") = Me.numKeikakuArari.Value              '�v��e��
        hsData("keikaku_settutei_kome") = Me.txtKome.Text               '�v��ݒ莞����
        hsData("keikaku_henkou_flg") = DBNull.Value                     '�v��ύXFLG

        '�ۑ��{�^�����͊m��{�^���𔻒f����
        If intButtonFlg = 0 Then
            '�ۑ��{�^��
            hsData("kakutei_flg") = 0                                   '�m��FLG
            hsData("keikaku_huhen_flg") = DBNull.Value                  '�v��l�s��FLG
        Else
            '�m��{�^��
            hsData("kakutei_flg") = 1                                   '�m��FLG

            '���N�x�Ŋ��Ɋm��f�[�^������ꍇ
            If Me.btnAllSave.Enabled = False Then
                hsData("keikaku_huhen_flg") = 1                         '�v��l�s��FLG
            Else
                hsData("keikaku_huhen_flg") = DBNull.Value              '�v��l�s��FLG
            End If
        End If

        hsData("add_login_user_id") = ViewState("LoginUserID")          '�o�^��ID

        Return hsData
    End Function

    ''' <summary>
    ''' �x�X�ʂ̃f�[�^���擾����
    ''' </summary>
    ''' <param name="intButtonFlg">�ۑ��{�^���A�m��{�^���̋敪</param>
    ''' <returns>�x�X�ʂ̃f�[�^</returns>
    ''' <remarks>�x�X�ʂ̃f�[�^���擾����</remarks>
    Private Function GetSitenbetuData(ByVal intButtonFlg As Integer) As DataTable
        Dim dtViewStateData As DataTable
        Dim dtValue As DataTable
        Dim drValue As DataRow
        Dim strSystemDate As String
        Dim i As Integer

        If Me.grdMeisai.DataSource Is Nothing Then
            dtValue = Nothing
        End If

        '�x�X�ʂ̃f�[�^���擾����
        dtViewStateData = CType(ViewState("SitenbetuData"), DataTable)

        dtValue = New DataTable
        dtValue.Columns.Add("keikaku_nendo")                '�v��N�x
        dtValue.Columns.Add("siten_mei")                    '�x�X��
        dtValue.Columns.Add("add_datetime")                 '�o�^����
        dtValue.Columns.Add("busyo_cd")                     '�����R�[�h
        dtValue.Columns.Add("eigyou_keikaku_kensuu")        '�c��_�v�撲������
        dtValue.Columns.Add("tokuhan_keikaku_kensuu")       '����_�v�撲������
        dtValue.Columns.Add("FC_keikaku_kensuu")            'FC_�v�撲������
        dtValue.Columns.Add("eigyou_keikaku_uri_kingaku")   '�c��_�v�攄����z
        dtValue.Columns.Add("tokuhan_keikaku_uri_kingaku")  '����_�v�攄����z
        dtValue.Columns.Add("FC_keikaku_uri_kingaku")       'FC_�v�攄����z
        dtValue.Columns.Add("eigyou_keikaku_arari")         '�c��_�v��e��
        dtValue.Columns.Add("tokuhan_keikaku_arari")        '����_�v��e��
        dtValue.Columns.Add("FC_keikaku_arari")             'FC_�v��e��

        dtValue.Columns.Add("keikaku_henkou_flg")           '�v��ύXFLG
        dtValue.Columns.Add("keikaku_settutei_kome")        '�v��ݒ莞����
        dtValue.Columns.Add("kakutei_flg")                  '�m��FLG
        dtValue.Columns.Add("keikaku_huhen_flg")            '�v��l�s��FLG
        dtValue.Columns.Add("add_login_user_id")            '�o�^��ID

        '�V�X�e�����Ԃ��擾����
        strSystemDate = objCommon.GetSystemDate().ToString()

        '���׃f�[�^��ۑ�����
        For i = 0 To Me.grdMeisai.Items.Count - 1
            drValue = dtValue.NewRow()
            drValue("keikaku_nendo") = Me.drpNendo.SelectedValue                        '�v��N�x 
            drValue("siten_mei") = dtViewStateData.Rows(i)("busyo_mei")                 '�x�X�� 
            drValue("add_datetime") = strSystemDate                                     '�o�^����
            drValue("busyo_cd") = dtViewStateData.Rows(i)("busyo_cd")                   '�����R�[�h 

            drValue("eigyou_keikaku_kensuu") = GetGridValue(i, "numKeikaku1_1", 1)      '�c��_�v�撲������ 
            drValue("tokuhan_keikaku_kensuu") = GetGridValue(i, "numKeikaku1_2", 1)     '����_�v�撲������ 
            drValue("FC_keikaku_kensuu") = GetGridValue(i, "numKeikaku1_3", 1)          'FC_�v�撲������ 
            drValue("eigyou_keikaku_uri_kingaku") = GetGridValue(i, "numKeikaku2_1", 1) '�c��_�v�攄����z 
            drValue("tokuhan_keikaku_uri_kingaku") = GetGridValue(i, "numKeikaku2_2", 1) '����_�v�攄����z 
            drValue("FC_keikaku_uri_kingaku") = GetGridValue(i, "numKeikaku2_3", 1)     'FC_�v�攄����z 
            drValue("eigyou_keikaku_arari") = GetGridValue(i, "numKeikaku3_1", 1)       '�c��_�v��e�� 
            drValue("tokuhan_keikaku_arari") = GetGridValue(i, "numKeikaku3_2", 1)      '����_�v��e�� 
            drValue("FC_keikaku_arari") = GetGridValue(i, "numKeikaku3_3", 1)           'FC_�v��e�� 

            drValue("keikaku_henkou_flg") = DBNull.Value                                '�v��ύXFLG
            drValue("keikaku_settutei_kome") = DBNull.Value                             '�v��ݒ莞����

            '�ۑ��{�^�����͊m��{�^���𔻒f����
            If intButtonFlg = 0 Then
                '�ۑ��{�^��
                drValue("kakutei_flg") = 0                                   '�m��FLG
                drValue("keikaku_huhen_flg") = DBNull.Value                  '�v��l�s��FLG
            Else
                '�m��{�^��
                drValue("kakutei_flg") = 1                                   '�m��FLG

                '���N�x�Ŋ��Ɋm��f�[�^������ꍇ
                If Me.btnSitenbetuSave.Enabled = False Then
                    drValue("keikaku_huhen_flg") = 1                         '�v��l�s��FLG
                Else
                    drValue("keikaku_huhen_flg") = DBNull.Value              '�v��l�s��FLG
                End If
            End If

            drValue("add_login_user_id") = ViewState("LoginUserID")
            dtValue.Rows.Add(drValue)
        Next

        dtViewStateData.Dispose()
        Return dtValue
    End Function

    ''' <summary>
    ''' ��ʃf�[�^���N���A����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPageClear()
        '�S�Ђ̃f�[�^���N���A����
        Me.numKeikakuKensuu.Value = ""
        Me.numKeikakuUriKingaku.Value = ""
        Me.numKeikakuArari.Value = ""
        Me.txtKome.Text = ""

        '�x�X�ʂ̔N�x���N���A����
        Me.lblNendo.Text = ""

        '�w�b�_���̗l�����N���A����
        Me.tdMeisaiHead.Attributes.Add("style", "")

        Me.grdMeisai.DataSource = Nothing
        Me.grdMeisai.DataBind()

        Me.btnKensuuCopy.Enabled = False
        Me.btnUriKingakuCopy.Enabled = False
        Me.btnArariCopy.Enabled = False

        Me.btnAllSave.Enabled = True
        Me.btnAllConfirm.Enabled = True
        Me.btnSitenbetuSave.Enabled = True
        Me.btnSitenbetuConfirm.Enabled = True

        Call SetSumData()

        Me.tbSum.Visible = False
    End Sub
#End Region

    
End Class
