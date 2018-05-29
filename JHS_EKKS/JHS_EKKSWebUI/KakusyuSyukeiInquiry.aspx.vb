Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.Utilities.CommonMessage
Imports System.Data

''' <summary>
''' �e��W�v
''' </summary>
''' <remarks>�e��W�v</remarks>
Partial Class KakusyuSyukeiInquiry
    Inherits System.Web.UI.Page

#Region "�v���C�x�[�g�ϐ�"

    Private kakusyuSyukeiInquiryBC As New KakusyuSyukeiInquiryBC
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC
    Private common As New Common


#End Region

#Region "�萔"

    ''' <summary>
    ''' POP��ʂ̋敪
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum popKbn As Integer

        Shiten = 1    '�x�X
        Kameiten = 2  '�s���{��
        Eigyou = 3    '�c�Ə�
        Keiretu = 4   '�n��
        User = 5      '�c�ƃ}��
        TourokuJigyousya = 6 '�o�^���Ǝ�

    End Enum

#End Region

#Region "�C�x���g"

    ''' <summary>
    ''' ��ʂ̏����\��
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)

        'JavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then

            '�����\�����A��ʂ̃Z�b�g����
            Call SetSyokiSeltuto()
            '���ו���\�����Ȃ�
            Me.divHead.Visible = False
        End If

        '�x�X �@'Siten'
        Me.tbxSiten.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxSiten.ClientID & "'));fncTextBoxTrue();return false;")
        '�s���{���@'Todouhuken'
        Me.tbxKameiten.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxKameiten.ClientID & "'));fncTextBoxTrue();return false;")
        '�c�Ə��@'Eigyousyo'
        Me.tbxEigyou.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxEigyou.ClientID & "'));fncTextBoxTrue();return false;")
        '�n�񖼁@'Keiretu'
        Me.tbxKeiretu.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxKeiretu.ClientID & "'));fncTextBoxTrue();return false;")
        '�c�ƃ}���@'EigyouMan'
        Me.tbxUser.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxUser.ClientID & "'));fncTextBoxTrue();return false;")
        '�o�^���Ǝҁ@'Tourokusya'
        Me.tbxTourokuJgousya.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxTourokuJgousya.ClientID & "'));fncTextBoxTrue();return false;")

        Me.btnPopup.Attributes.Add("onClick", "fncTextBoxTrue();return false;")

        '��ʂ�JS EVENT�ݒ�
        Call SetJsEvent()

    End Sub

    ''' <summary>
    ''' �N�x�{�^�����������鎞�̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/05�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnNendo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNendo.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '�N�x�I�����X�g�{�b�N�X������
        Me.ddlNendo.Enabled = True

        '���Ԃ̑I����񊈐�
        Me.ddlKikanFrom.Enabled = False
        Me.ddlKikanTo.Enabled = False

        '�����̑I����񊈐�
        Me.ddlTukinamiFrom.Enabled = False
        Me.ddlTukinamiTo.Enabled = False
        Me.ddlTukinamiTo2.Enabled = False

    End Sub

    ''' <summary>
    ''' ���ԃ{�^�����������鎞�̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/05�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnKikan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKikan.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '�N�x�I�����X�g�{�b�N�X�񊈐�
        Me.ddlNendo.Enabled = False

        '���Ԃ̑I����������
        Me.ddlKikanFrom.Enabled = True
        Me.ddlKikanTo.Enabled = True

        '�����̑I����񊈐�
        Me.ddlTukinamiFrom.Enabled = False
        Me.ddlTukinamiTo.Enabled = False
        Me.ddlTukinamiTo2.Enabled = False

    End Sub

    ''' <summary>
    ''' �����{�^�����������鎞�̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/05�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnTukinami_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTukinami.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '�N�x�I�����X�g�{�b�N�X�񊈐�
        Me.ddlNendo.Enabled = False

        '���Ԃ̑I����񊈐�
        Me.ddlKikanFrom.Enabled = False
        Me.ddlKikanTo.Enabled = False

        '�����̑I����������
        Me.ddlTukinamiFrom.Enabled = True
        Me.ddlTukinamiTo.Enabled = True
        Me.ddlTukinamiTo2.Enabled = True

    End Sub

    ''' <summary>
    ''' �x�X�̌����{�^���������̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnSiten_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiten.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxSiten.Text.Equals("ALL") Then
            Call SetClear(popKbn.Shiten)
            Me.hidModouru.Value = "Siten"
        Else
            If Me.tbxSiten.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.Shiten)
            Else
                Dim SitenSearchBC As New SitenSearchBC
                Dim dtSiten As Data.DataTable = SitenSearchBC.GetBusyoKanri("0", Me.tbxSiten.Text, False)
                If dtSiten.Rows.Count = 1 Then
                    Me.tbxSiten.Text = dtSiten.Rows(0).Item(0).ToString
                    Me.hidSitenCd.Value = dtSiten.Rows(0).Item(1).ToString
                    Me.hidModouru.Value = "Siten"
                    Call SetClear(popKbn.Shiten)
                Else
                    Call ShoPop(popKbn.Shiten)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' �c�ƃ}���̌����{�^���������̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxUser.Text.Equals("ALL") Then
            Call SetClear(popKbn.User)
            Me.hidModouru.Value = "EigyouMan"
        Else
            If Me.tbxUser.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.User)
            Else
                Dim UserSearchBC As New EigyouManSearchBC
                Dim dtUserInfo As Data.DataTable = UserSearchBC.GetUserInfo("0", "", Me.tbxUser.Text, False)
                If dtUserInfo.Rows.Count = 1 Then
                    Me.tbxUser.Text = dtUserInfo.Rows(0).Item(1).ToString
                    Me.hidUserCd.Value = dtUserInfo.Rows(0).Item(0).ToString
                    Me.hidModouru.Value = "EigyouMan"
                    Call SetClear(popKbn.User)
                Else
                    Call ShoPop(popKbn.User)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' �s���{���̌����{�^���������̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnKameiten_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKameiten.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxKameiten.Text.Equals("ALL") Then
            Call SetClear(popKbn.Kameiten)
            Me.hidModouru.Value = "Todouhuken"
        Else
            If Me.tbxKameiten.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.Kameiten)
            Else
                Dim TodouhukenBC As New TodoufukenSearchBC
                Dim Todouhuken As Data.DataTable = TodouhukenBC.GetTodoufukenMei("0", Me.tbxKameiten.Text)
                If Todouhuken.Rows.Count = 1 Then
                    Me.tbxKameiten.Text = Todouhuken.Rows(0).Item(1).ToString
                    Me.hidKameitenCd.Value = Todouhuken.Rows(0).Item(0).ToString
                    Me.hidModouru.Value = "Todouhuken"
                    Call SetClear(popKbn.Kameiten)
                Else
                    Call ShoPop(popKbn.Kameiten)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' �n�񖼂̌����{�^���������̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnKeiretu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeiretu.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        If Me.tbxKeiretu.Text.Equals("ALL") Then
            Call SetClear(popKbn.Keiretu)
            Me.hidModouru.Value = "Keiretu"
        Else
            If Me.tbxKeiretu.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.Keiretu)
            Else
                Dim KeiretuSearchBC As New KeiretuSearchBC
                Dim dtKeiretu As Data.DataTable = KeiretuSearchBC.GetKiretuJyouhou("0", "", Me.tbxKeiretu.Text, False)
                If dtKeiretu.Rows.Count = 1 Then
                    Me.tbxKeiretu.Text = dtKeiretu.Rows(0).Item(1).ToString
                    Me.hidKeiretuMei.Value = dtKeiretu.Rows(0).Item(0).ToString
                    Me.hidModouru.Value = "Keiretu"
                    Call SetClear(popKbn.Keiretu)
                Else
                    Call ShoPop(popKbn.Keiretu)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' �c�Ə��̌����{�^���������̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnEigyou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEigyou.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxEigyou.Text.Equals("ALL") Then
            Call SetClear(popKbn.Eigyou)
            Me.hidModouru.Value = "Eigyousyo"
        Else
            If Me.tbxEigyou.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.Eigyou)
            Else
                Dim EigyousyoSearchBC As New EigyousyoSearchBC
                Dim dtEigyousyo As Data.DataTable = EigyousyoSearchBC.GetEigyousyoMei("0", Me.tbxEigyou.Text, False)
                If dtEigyousyo.Rows.Count = 1 Then
                    Me.tbxEigyou.Text = dtEigyousyo.Rows(0).Item(0).ToString
                    Me.hidEigyouCd.Value = dtEigyousyo.Rows(0).Item(1).ToString
                    Me.hidModouru.Value = "Eigyousyo"
                    Call SetClear(popKbn.Eigyou)
                Else
                    Call ShoPop(popKbn.Eigyou)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' �o�^���Ǝ҂̌����{�^���������̏���
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/07�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub btnTourokuJgousya_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTourokuJgousya.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxTourokuJgousya.Text.Equals("ALL") Then
            Call SetClear(popKbn.TourokuJigyousya)
            Me.hidModouru.Value = "Tourokusya"
        Else
            If Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.TourokuJigyousya)
            Else
                Dim TourokuJgousyaBC As New TourokuJigyousyaSearchBC
                Dim dtTourokuJgousya As Data.DataTable = TourokuJgousyaBC.GetTourokuJigyousya("0", "", Me.tbxTourokuJgousya.Text, False)
                If dtTourokuJgousya.Rows.Count = 1 Then
                    Me.tbxEigyou.Text = dtTourokuJgousya.Rows(0).Item(1).ToString
                    Me.hidTourokuJgousya.Value = dtTourokuJgousya.Rows(0).Item(0).ToString
                    Me.hidModouru.Value = "Tourokusya"
                    Call SetClear(popKbn.TourokuJigyousya)
                Else
                    Call ShoPop(popKbn.TourokuJigyousya)
                End If
            End If
        End If
    End Sub

    '''' <summary>
    '''' ���ʕ\���{�^���������̏���
    '''' </summary>
    '''' <param name="sender">Object</param>
    '''' <param name="e">System.EventArgs</param>
    '''' <history>
    '''' <para>2013/01/10�@���F(��A���V�X�e����)�@�V�K�쐬</para>
    '''' </history>
    'Protected Sub btnKeltukaHyouji_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeltukaHyouji.Click

    '    'EMAB��Q�Ή����̊i�[����
    '    EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
    '                           MyMethod.GetCurrentMethod.Name, sender, e)

    '    '�N�xLIST��I�����Ȃ���
    '    If ((Me.ddlNendo.Enabled = True) AndAlso (Me.ddlNendo.SelectedValue = 0)) OrElse _
    '       ((Me.ddlKikanFrom.Enabled = True) AndAlso (Me.ddlKikanFrom.SelectedValue = 0)) OrElse _
    '       ((Me.ddlTukinamiFrom.Enabled = True) AndAlso (Me.ddlTukinamiFrom.SelectedValue = 0)) Then
    '        '���b�Z�[�W��\������
    '        common.SetShowMessage(Me, MSG001E, "�N�x")
    '        '���ו���\�����Ȃ�
    '        Me.divHead.Visible = False

    '        If Me.ddlNendo.Enabled = True Then
    '            Me.ddlNendo.Focus()
    '        ElseIf Me.ddlKikanFrom.Enabled = True Then
    '            Me.ddlKikanFrom.Focus()
    '        ElseIf Me.ddlTukinamiFrom.Enabled = True Then
    '            Me.ddlTukinamiFrom.Focus()
    '        End If

    '        '���ʕ\���Q�����I�ׂ�悤��
    '    ElseIf (Me.ddlTukinamiFrom.Enabled = True) AndAlso (Me.ddlTukinamiTo.SelectedItem.Value = "0") Then
    '        common.SetShowMessage(Me, MSG048E)
    '        Me.ddlTukinamiTo.Focus()

    '        'FC�`�F�b�N�{�b�N�X�Ǝx�X���̊֌W
    '    ElseIf (Me.chkFC.Checked = True) AndAlso (Me.tbxSiten.Text = String.Empty) Then

    '        Me.tbxSiten.Focus()

    '        '���b�Z�[�W��\������
    '        common.SetShowMessage(Me, MSG001E, "�x�X��")
    '        '���ו���\�����Ȃ�
    '        Me.divHead.Visible = False

    '        '�W�v ���e�I������I�����Ȃ���
    '    ElseIf Me.tbxSiten.Text = String.Empty AndAlso _
    '           Me.tbxKameiten.Text = String.Empty AndAlso _
    '           Me.tbxEigyou.Text = String.Empty AndAlso _
    '           Me.tbxKeiretu.Text = String.Empty AndAlso _
    '           Me.tbxUser.Text = String.Empty AndAlso _
    '           Me.tbxTourokuJgousya.Text = String.Empty Then

    '        '���b�Z�[�W��\������
    '        common.SetShowMessage(Me, MSG058E)
    '        '���ו���\�����Ȃ�
    '        Me.divHead.Visible = False

    '        '�c�Ƌ敪����I�����Ȃ���
    '    ElseIf Me.chkEigyou.Checked = False AndAlso _
    '            Me.chkFC.Checked = False AndAlso _
    '            Me.chkSinki.Checked = False AndAlso _
    '            Me.chkTokuhan.Checked = False Then
    '        '���b�Z�[�W��\������
    '        common.SetShowMessage(Me, MSG071E)

    '        '�����{�^���̉������f����
    '    ElseIf Not Me.tbxSiten.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxKameiten.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxEigyou.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxKeiretu.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxUser.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then

    '        If CheckInput() = True OrElse _
    '           Me.tbxSiten.Text.Equals("ALL") OrElse _
    '           Me.tbxKameiten.Text.Equals("ALL") OrElse _
    '           Me.tbxEigyou.Text.Equals("ALL") OrElse _
    '           Me.tbxKeiretu.Text.Equals("ALL") OrElse _
    '           Me.tbxUser.Text.Equals("ALL") OrElse _
    '           Me.tbxTourokuJgousya.Text.Equals("ALL") Then

    '            '��ʂőI�������̍���
    '            If Not Me.tbxSiten.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "�x�X"
    '            ElseIf Not Me.tbxKameiten.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "�s���{��"
    '            ElseIf Not Me.tbxEigyou.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "�c�Ə�"
    '            ElseIf Not Me.tbxKeiretu.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "�n��"
    '            ElseIf Not Me.tbxUser.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "�c�ƃ}��"
    '            ElseIf Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "�o�^���Ǝ�"
    '            End If

    '            Dim dtMeisaiDate As Data.DataTable
    '            '�x�X
    '            Dim strSiten As String
    '            If Me.tbxSiten.Text.Equals("ALL") Then
    '                strSiten = Me.tbxSiten.Text
    '            Else
    '                strSiten = Me.hidSitenCd.Value
    '            End If
    '            '�s���{��
    '            Dim strKameitenCd As String
    '            If Me.tbxKameiten.Text.Equals("ALL") Then
    '                strKameitenCd = Me.tbxKameiten.Text
    '            Else
    '                strKameitenCd = Me.hidKameitenCd.Value
    '            End If
    '            '�c�Ə�
    '            Dim strEigyouCd As String
    '            If Me.tbxEigyou.Text.Equals("ALL") Then
    '                strEigyouCd = Me.tbxEigyou.Text
    '            Else
    '                strEigyouCd = Me.hidEigyouCd.Value
    '            End If
    '            '�n��
    '            Dim strKeiretuMei As String
    '            If Me.tbxKeiretu.Text.Equals("ALL") Then
    '                strKeiretuMei = Me.tbxKeiretu.Text
    '            Else
    '                strKeiretuMei = Me.hidKeiretuMei.Value
    '            End If
    '            '�c�ƃ}��
    '            Dim strUserCd As String
    '            If Me.tbxUser.Text.Equals("ALL") Then
    '                strUserCd = Me.tbxUser.Text
    '            Else
    '                strUserCd = Me.hidUserCd.Value
    '            End If
    '            '�o�^���Ǝ�
    '            Dim strTourokuJgousya As String
    '            If Me.tbxTourokuJgousya.Text.Equals("ALL") Then
    '                strTourokuJgousya = Me.tbxTourokuJgousya.Text
    '            Else
    '                strTourokuJgousya = Me.hidTourokuJgousya.Value
    '            End If

    '            '�c�Ƌ敪
    '            Dim strEigyouKbn As String = String.Empty '�c��
    '            If Me.chkEigyou.Checked = True Then
    '                strEigyouKbn = strEigyouKbn & ",1"
    '            End If
    '            If Me.chkTokuhan.Checked = True Then '����
    '                strEigyouKbn = strEigyouKbn & ",3"
    '            End If
    '            If Me.chkSinki.Checked = True Then '�V�K
    '                strEigyouKbn = strEigyouKbn & ",2"
    '            End If
    '            If Me.chkFC.Checked = True Then 'FC
    '                strEigyouKbn = strEigyouKbn & ",4"
    '            End If

    '            '�f�[�^���擾����
    '            If (Me.ddlNendo.Enabled = True) Then

    '                'FC�̃`�F�b�N�{�b�N�X���`�F�b�N���Ȃ�
    '                If Me.chkFC.Checked = False Then

    '                    '�N��:��ʂ̃f�[�^���擾����
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
    '                                   strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
    '                                   strTourokuJgousya, Me.ddlNendo.SelectedValue, 1, 12, strEigyouKbn)

    '                ElseIf (Me.chkFC.Checked = True) AndAlso _
    '                       (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

    '                    'FC�̃`�F�b�N�{�b�N�X���`�F�b�N����
    '                    '�N��:��ʂ̃f�[�^���擾����
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
    '                                                          Me.ddlNendo.SelectedValue, 1, 12)
    '                Else
    '                    '�W�v
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
    '                                   Me.ddlNendo.SelectedValue, 1, 12, strEigyouKbn)

    '                End If

    '            ElseIf (Me.ddlKikanTo.Enabled = True AndAlso Me.ddlKikanFrom.Enabled = True) Then

    '                Dim intBegin As Integer
    '                Dim intEnd As Integer
    '                If Me.ddlKikanTo.SelectedItem.Text = "���" Then
    '                    intBegin = 4
    '                    intEnd = 9
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "�l����(4,5,6��)" Then
    '                    intBegin = 4
    '                    intEnd = 6
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "�l����(7,8,9��)" Then
    '                    intBegin = 7
    '                    intEnd = 9
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "�l����(10,11,12��)" Then
    '                    intBegin = 10
    '                    intEnd = 12
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "�l����(1,2,3��)" Then
    '                    intBegin = 1
    '                    intEnd = 3
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "����" Then
    '                    intBegin = 10
    '                    intEnd = 15
    '                End If

    '                'FC�̃`�F�b�N�{�b�N�X���`�F�b�N���Ȃ�
    '                If Me.chkFC.Checked = False Then

    '                    '����:��ʂ̃f�[�^���擾����
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
    '                                   strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
    '                                   strTourokuJgousya, Me.ddlKikanFrom.SelectedValue, intBegin, intEnd, strEigyouKbn)

    '                ElseIf (Me.chkFC.Checked = True) AndAlso _
    '                       (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

    '                    'FC�̃`�F�b�N�{�b�N�X���`�F�b�N����
    '                    '����:��ʂ̃f�[�^���擾����
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
    '                                    Me.ddlKikanFrom.SelectedValue, intBegin, intEnd)

    '                Else
    '                    '�W�v
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
    '                                   Me.ddlKikanFrom.SelectedValue, intBegin, intEnd, strEigyouKbn)

    '                End If

    '            ElseIf (Me.ddlTukinamiFrom.Enabled = True AndAlso Me.ddlTukinamiTo.Enabled = True AndAlso Me.ddlTukinamiTo2.Enabled = True) Then

    '                Dim strTukinamiTo As String
    '                If Me.ddlTukinamiTo.SelectedValue < 4 Then
    '                    strTukinamiTo = Me.ddlTukinamiTo.SelectedValue + 12
    '                Else
    '                    strTukinamiTo = Me.ddlTukinamiTo.SelectedValue
    '                End If

    '                Dim strTukinamiTo2 As String
    '                If Me.ddlTukinamiTo2.SelectedItem IsNot Nothing Then
    '                    If Me.ddlTukinamiTo2.SelectedItem.Text.Equals("2��") Then
    '                        strTukinamiTo2 = 14
    '                    ElseIf Me.ddlTukinamiTo2.SelectedItem.Text.Equals("3��") Then
    '                        strTukinamiTo2 = 15
    '                    Else
    '                        strTukinamiTo2 = Me.ddlTukinamiTo2.SelectedValue
    '                    End If
    '                Else
    '                    strTukinamiTo2 = 15
    '                End If


    '                'FC�̃`�F�b�N�{�b�N�X���`�F�b�N���Ȃ�
    '                If Me.chkFC.Checked = False Then

    '                    '����:��ʂ̃f�[�^���擾����
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
    '                                   strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
    '                                   strTourokuJgousya, Me.ddlTukinamiFrom.SelectedValue, _
    '                                   strTukinamiTo, strTukinamiTo2, strEigyouKbn)

    '                ElseIf (Me.chkFC.Checked = True) AndAlso _
    '                       (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

    '                    'FC�̃`�F�b�N�{�b�N�X���`�F�b�N����
    '                    '����:��ʂ̃f�[�^���擾����
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
    '                                   Me.ddlTukinamiFrom.SelectedValue, strTukinamiTo, strTukinamiTo2)

    '                Else
    '                    '�W�v
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
    '                                   Me.ddlTukinamiFrom.SelectedValue, strTukinamiTo, strTukinamiTo2, strEigyouKbn)
    '                End If

    '            Else
    '                dtMeisaiDate = Nothing
    '            End If

    '            If dtMeisaiDate.Rows.Count = 0 Then
    '                '���b�Z�[�W��\������
    '                common.SetShowMessage(Me, MSG067E)
    '                '���ו���\�����Ȃ�
    '                Me.divHead.Visible = False
    '            Else

    '                '���ו���\������
    '                Me.divHead.Visible = True
    '                '��ʂ�
    '                Call BoundGridView(dtMeisaiDate)
    '            End If

    '        End If

    '    End If

    '    If Me.tbxSiten.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Siten"
    '    ElseIf Me.tbxKameiten.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Todouhuken"
    '    ElseIf Me.tbxEigyou.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Eigyousyo"
    '    ElseIf Me.tbxKeiretu.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Keiretu"
    '    ElseIf Me.tbxUser.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "EigyouMan"
    '    ElseIf Me.tbxTourokuJgousya.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Tourokusya"
    '    End If

    'End Sub

    ''' <summary>
    ''' ���ו���LEFT������RowDataBound
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/01/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim title As String = CType(e.Row.FindControl("hidTitle"), HiddenField).Value

            Dim dtTemp As Data.DataTable
            Dim drValue As Data.DataRow
            Dim drTemp() As Data.DataRow
            drTemp = CType(ViewState("dtSource"), Data.DataTable).Select("busyo_cd='" & title & "'")

            dtTemp = CType(ViewState("dtSource"), Data.DataTable).Clone
            For Each drValue In drTemp
                dtTemp.ImportRow(drValue)
            Next

            CType(e.Row.FindControl("SyouhinInfo1"), CommonControl_KakusyuSyukeiInquiryInfo).DataSource = dtTemp
        End If

    End Sub

    ''' <summary>
    ''' ���ו���Right������RowDataBound
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/01/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyRight.RowDataBound

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim title As String = CType(e.Row.FindControl("hidTitle"), HiddenField).Value

            Dim dtTemp As Data.DataTable
            Dim drValue As Data.DataRow
            Dim drTemp() As Data.DataRow
            drTemp = CType(ViewState("dtSource"), Data.DataTable).Select("busyo_cd='" & title & "'")

            dtTemp = CType(ViewState("dtSource"), Data.DataTable).Clone
            For Each drValue In drTemp
                dtTemp.ImportRow(drValue)
            Next

            CType(e.Row.FindControl("SyouhinData1"), CommonControl_KakusyuSyukeiInquiryData).DataSource = dtTemp
        End If

    End Sub

    ''' <summary>
    ''' ���ʕ\��FROM��change����
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2013/01/11�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub ddlTukinamiTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTukinamiTo.SelectedIndexChanged

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        '���ʕ\���P�̒l
        Dim selValue As String
        selValue = Me.ddlTukinamiTo.SelectedValue
        If Me.ddlTukinamiTo.SelectedItem.Value = "0" Then
            '����ToDropdownList���擾����
            Dim dtTukinamiTo As Data.DataTable = kakusyuSyukeiInquiryBC.GetTukinamiListData()
            If dtTukinamiTo.Rows.Count > 0 Then
                Me.ddlTukinamiTo2.DataSource = dtTukinamiTo
                Me.ddlTukinamiTo2.DataValueField = "code"
                Me.ddlTukinamiTo2.DataTextField = "meisyou"
                Me.ddlTukinamiTo2.DataBind()
            End If

            Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
            Me.ddlTukinamiTo2.SelectedIndex = 0

        ElseIf Me.ddlTukinamiTo.SelectedItem.Value = "1" Then
            Me.ddlTukinamiTo2.Items.Clear()
            Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
            Me.ddlTukinamiTo2.Items.Insert(1, New ListItem("2��", "1"))
            Me.ddlTukinamiTo2.Items.Insert(2, New ListItem("3��", "2"))
            Me.ddlTukinamiTo2.SelectedValue = 1

        ElseIf Me.ddlTukinamiTo.SelectedItem.Value = "2" Then
            Me.ddlTukinamiTo2.Items.Clear()
            Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
            Me.ddlTukinamiTo2.Items.Insert(1, New ListItem("3��", "1"))
            Me.ddlTukinamiTo2.SelectedValue = 1

        ElseIf Me.ddlTukinamiTo.SelectedItem.Value = "3" Then
            Me.ddlTukinamiTo2.Items.Clear()

        Else
            '���ʕ\���Q���N���A
            Me.ddlTukinamiTo2.Items.Clear()
            Dim index As Integer = 1
            Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
            For i As Integer = CType(selValue, Integer) + 1 To 15
                Dim j As Integer
                If i > 12 Then
                    j = i - 12
                Else
                    j = i
                End If

                Me.ddlTukinamiTo2.Items.Insert(index, New ListItem(j.ToString & "��", i))
                index = index + 1
                Me.ddlTukinamiTo2.SelectedValue = CType(selValue, Integer) + 1
            Next

        End If

    End Sub

    ''' <summary>
    ''' ���ʕ\��TO��change����
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2013/01/11�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub ddlTukinamiTo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTukinamiTo2.SelectedIndexChanged

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '���ʕ\���Q�����I�ׂ�悤��
        If Me.ddlTukinamiTo.SelectedItem.Value = "0" AndAlso Me.ddlTukinamiTo2.SelectedItem.Value <> "0" Then
            common.SetShowMessage(Me, MSG048E)
            Me.ddlTukinamiTo.Focus()
        End If

    End Sub

#End Region

#Region "���\�b�h"

    ''' <summary>
    ''' ��ʂ̑����ڂ��N���A����
    ''' </summary>
    ''' <param name="popKbn">��ʍ��ڂ̋敪</param>
    ''' <remarks></remarks>
    ''' <history>2013/01/17�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub SetClear(ByVal popKbn As KakusyuSyukeiInquiry.popKbn)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Select Case popKbn

            '�x�X
            Case KakusyuSyukeiInquiry.popKbn.Shiten
                Me.tbxKameiten.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxUser.Text = String.Empty

                '�c�ƃ}��
            Case KakusyuSyukeiInquiry.popKbn.User
                Me.tbxKameiten.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

                '�o�^���Ǝ�
            Case KakusyuSyukeiInquiry.popKbn.TourokuJigyousya
                Me.tbxKameiten.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxUser.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

                '�c�Ə�
            Case KakusyuSyukeiInquiry.popKbn.Eigyou
                Me.tbxKameiten.Text = String.Empty
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxUser.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

                '�n��
            Case KakusyuSyukeiInquiry.popKbn.Keiretu
                Me.tbxKameiten.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxUser.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

                '�s���{��
            Case KakusyuSyukeiInquiry.popKbn.Kameiten
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxUser.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

        End Select

    End Sub

    ''' <summary>
    ''' ���ו�����Bound
    ''' </summary>
    ''' <param name="dtSource">Data.DataTable</param>
    ''' <remarks></remarks>
    ''' <history>2013/01/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub BoundGridView(ByVal dtSource As Data.DataTable)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dt As New Data.DataTable
        Dim dc As Data.DataColumn
        Dim dr As Data.DataRow

        dc = New Data.DataColumn("title")
        dt.Columns.Add(dc)

        Dim strTitle As String = String.Empty

        For i As Integer = 0 To dtSource.Rows.Count - 1
            If Not strTitle.Equals(dtSource.Rows(i).Item("busyo_cd").ToString) Then
                strTitle = dtSource.Rows(i).Item("busyo_cd").ToString
                dr = dt.NewRow
                dr.Item("title") = strTitle
                dt.Rows.Add(dr)
            End If
        Next

        ViewState("dtSource") = dtSource

        Me.grdBodyLeft.DataSource = dt
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dt
        Me.grdBodyRight.DataBind()

    End Sub


    ''' <summary>
    ''' ��ʂ̏W�v ���e�I�𕔕��A�����{�^�����������邩�ǂ����ɂ��āA���f����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/10�@���F(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Function CheckInput() As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '�x�X
        If Not Me.tbxSiten.Text.Equals(String.Empty) Then
            Dim intSitenDataCount As Integer
            intSitenDataCount = ShowPop(False, popKbn.Shiten)
            If intSitenDataCount = 0 Then
                If Not Me.tbxSiten.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "�x�X")
                End If
                Return False

            ElseIf intSitenDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "�x�X")
                Return False

            Else
                Return True
            End If

            '�s���{��
        ElseIf Not Me.tbxKameiten.Text.Equals(String.Empty) Then
            Dim intTodouhukenDataCount As Integer
            intTodouhukenDataCount = ShowPop(False, popKbn.Kameiten)
            If intTodouhukenDataCount = 0 Then
                If Not Me.tbxKameiten.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "�s���{��")
                End If
                Return False

            ElseIf intTodouhukenDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "�s���{��")
                Return False

            Else
                Return True
            End If

            '�c�Ə�
        ElseIf Not Me.tbxEigyou.Text.Equals(String.Empty) Then
            Dim intEigyouDataCount As Integer
            intEigyouDataCount = ShowPop(False, popKbn.Eigyou)
            If intEigyouDataCount = 0 Then
                If Not Me.tbxEigyou.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "�c�Ə�")
                End If
                Return False

            ElseIf intEigyouDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "�c�Ə�")
                Return False

            Else
                Return True
            End If

            '�n��
        ElseIf Not Me.tbxKeiretu.Text.Equals(String.Empty) Then
            Dim intKeiretuDataCount As Integer
            intKeiretuDataCount = ShowPop(False, popKbn.Keiretu)
            If intKeiretuDataCount = 0 Then
                If Not Me.tbxKeiretu.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "�n��")
                End If
                Return False

            ElseIf intKeiretuDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "�n��")
                Return False

            Else
                Return True
            End If

            '�c�ƃ}��
        ElseIf Not Me.tbxUser.Text.Equals(String.Empty) Then
            Dim intUserDataCount As Integer
            intUserDataCount = ShowPop(False, popKbn.User)
            If intUserDataCount = 0 Then
                If Not Me.tbxUser.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "�c�ƃ}��")
                End If
                Return False

            ElseIf intUserDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "�c�ƃ}��")
                Return False

            Else
                Return True
            End If

            '�o�^���Ǝ�
        ElseIf Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then
            Dim intTourokuJgousyaDataCount As Integer
            intTourokuJgousyaDataCount = ShowPop(False, popKbn.TourokuJigyousya)
            If intTourokuJgousyaDataCount = 0 Then
                If Not Me.tbxTourokuJgousya.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "�o�^���Ǝ�")
                End If
                Return False

            ElseIf intTourokuJgousyaDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "�o�^���Ǝ�")
                Return False

            Else
                Return True
            End If
        End If

    End Function

    ''' <summary>
    ''' �����\������ʂ̃Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/12/05�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub SetSyokiSeltuto()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '�N�x,����From,����FromDropdownList���擾����
        Call GetNendoListDate()

        '����ToDropdownList���擾����
        Call GetKikanToListDate()

        '����ToDropdownList���擾����
        Call GetTukinamiToListDate()

        '�N�x�I�����X�g�{�b�N�X������
        Me.ddlNendo.Enabled = True

        '���Ԃ̑I����������
        Me.ddlKikanFrom.Enabled = False
        Me.ddlKikanTo.Enabled = False

        '�����̑I����������
        Me.ddlTukinamiFrom.Enabled = False
        Me.ddlTukinamiTo.Enabled = False
        Me.ddlTukinamiTo2.Enabled = False

        '�c��,����,�V�KcheckBox��I�����Ă��܂�
        Me.chkEigyou.Checked = True
        Me.chkTokuhan.Checked = True
        Me.chkSinki.Checked = True
        'focus�ɃZ�b�g����
        Me.tbxSiten.Focus()

    End Sub

    ''' <summary>
    ''' ���ʃZ�b�g����
    ''' </summary>
    ''' <history>2012/11/30�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Sub GetTukinamiToListDate()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '����FromDropdownList���N���A����
        Me.ddlTukinamiTo.Items.Clear()
        Me.ddlTukinamiTo2.Items.Clear()

        '����ToDropdownList���擾����
        Dim dtTukinamiTo As Data.DataTable = kakusyuSyukeiInquiryBC.GetTukinamiListData()
        If dtTukinamiTo.Rows.Count > 0 Then
            Me.ddlTukinamiTo.DataSource = dtTukinamiTo
            Me.ddlTukinamiTo.DataValueField = "code"
            Me.ddlTukinamiTo.DataTextField = "meisyou"
            Me.ddlTukinamiTo.DataBind()

            Me.ddlTukinamiTo2.DataSource = dtTukinamiTo
            Me.ddlTukinamiTo2.DataValueField = "code"
            Me.ddlTukinamiTo2.DataTextField = "meisyou"
            Me.ddlTukinamiTo2.DataBind()
        End If

        Dim objCommonBC As New CommonBC
        Dim strSysTuki As Integer = Convert.ToDateTime(objCommonBC.SelSystemDate.Rows(0).Item(0).ToString).Month

        '���ʕ\���͏����𖢑I����ݒ�
        Me.ddlTukinamiTo.Items.Insert(0, New ListItem("", "0"))
        Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
        Me.ddlTukinamiTo.SelectedIndex = 1

        'ddlBeginTuki�����ݒ�4���Ȃ̂ŁA�V�X�e�����@5���@�̏ꍇ�̂�
        If (strSysTuki = 4) OrElse (strSysTuki = 5) Then
            'ddlEndTuki�����ݒ����
            Me.ddlTukinamiTo2.SelectedIndex = 0
        Else
            Me.ddlTukinamiTo2.SelectedValue = IIf(strSysTuki < 4, (strSysTuki + 12 - 1).ToString, (strSysTuki - 1).ToString)
        End If

    End Sub

    ''' <summary>
    ''' ����POPUP
    ''' </summary>
    ''' <param name="popKbn">�W�v ���e�I���̋敪</param>
    ''' <history>
    ''' <para>2013/01/10�@���F(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private Function ShowPop(ByVal blnPop As Boolean, ByVal popKbn As KakusyuSyukeiInquiry.popKbn) As Integer

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, blnPop)

        Dim csScript As New StringBuilder
        Select Case popKbn
            '�x�X
            Case KakusyuSyukeiInquiry.popKbn.Shiten

                Dim SitenSearchBC As New SitenSearchBC
                Dim dtSiten As Data.DataTable
                dtSiten = SitenSearchBC.GetBusyoKanri("0", Me.tbxSiten.Text, False, False)

                ShowPop = dtSiten.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxSiten.Text = dtSiten.Rows(0).Item("busyo_mei").ToString
                    Me.hidSitenCd.Value = dtSiten.Rows(0).Item("busyo_cd").ToString
                    Me.hidModouru.Value = "Siten"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '�����̏ꍇ
                    If ShowPop > 1 Then
                        If Me.hidSitenCd.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtSiten.Rows.Count - 1
                                If Me.hidSitenCd.Value.ToString.Trim = dtSiten.Rows(i).Item("busyo_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '�c�ƃ}��
            Case KakusyuSyukeiInquiry.popKbn.User

                Dim EigyouManSearchBC As New EigyouManSearchBC
                Dim dtUserInfo As New Data.DataTable
                dtUserInfo = EigyouManSearchBC.GetUserInfo("0", "", Me.tbxUser.Text, False, False)

                ShowPop = dtUserInfo.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxUser.Text = dtUserInfo.Rows(0).Item("DisplayName").ToString
                    Me.hidUserCd.Value = dtUserInfo.Rows(0).Item("login_user_id").ToString
                    Me.hidModouru.Value = "EigyouMan"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '�����̏ꍇ
                    If ShowPop > 1 Then
                        If Me.hidUserCd.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtUserInfo.Rows.Count - 1
                                If Me.hidUserCd.Value.ToString.Trim = dtUserInfo.Rows(i).Item("login_user_id").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '�o�^���Ǝ�
            Case KakusyuSyukeiInquiry.popKbn.TourokuJigyousya
                Dim TourokuJigyousyaSearchBC As New TourokuJigyousyaSearchBC
                Dim dtTourokuJigyousya As New Data.DataTable
                dtTourokuJigyousya = TourokuJigyousyaSearchBC.GetTourokuJigyousya("0", "", Me.tbxTourokuJgousya.Text, False, False)

                ShowPop = dtTourokuJigyousya.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxTourokuJgousya.Text = dtTourokuJigyousya.Rows(0).Item("kameiten_mei1").ToString
                    Me.hidTourokuJgousya.Value = dtTourokuJigyousya.Rows(0).Item("kameiten_cd").ToString
                    Me.hidModouru.Value = "Tourokusya"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '�����̏ꍇ
                    If ShowPop > 1 Then
                        If Me.hidTourokuJgousya.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtTourokuJigyousya.Rows.Count - 1
                                If Me.hidTourokuJgousya.Value.ToString.Trim = dtTourokuJigyousya.Rows(i).Item("kameiten_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '�c�Ə�
            Case KakusyuSyukeiInquiry.popKbn.Eigyou
                Dim EigyousyoSearchBC As New EigyousyoSearchBC
                Dim dtEigyousyo As New Data.DataTable
                dtEigyousyo = EigyousyoSearchBC.GetEigyousyoMei("0", Me.tbxEigyou.Text, False, False)

                ShowPop = dtEigyousyo.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxEigyou.Text = dtEigyousyo.Rows(0).Item("busyo_mei").ToString
                    Me.hidEigyouCd.Value = dtEigyousyo.Rows(0).Item("busyo_cd").ToString
                    Me.hidModouru.Value = "Eigyousyo"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '�����̏ꍇ
                    If ShowPop > 1 Then
                        If Me.hidEigyouCd.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtEigyousyo.Rows.Count - 1
                                If Me.hidEigyouCd.Value.ToString.Trim = dtEigyousyo.Rows(i).Item("busyo_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '�n��
            Case KakusyuSyukeiInquiry.popKbn.Keiretu
                Dim KeiretuSearchBC As New KeiretuSearchBC
                Dim dtKeiretu As New Data.DataTable
                dtKeiretu = KeiretuSearchBC.GetKiretuJyouhou("0", "", Me.tbxKeiretu.Text, False, False)

                ShowPop = dtKeiretu.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxKeiretu.Text = dtKeiretu.Rows(0).Item("keiretu_mei").ToString
                    Me.hidKeiretuMei.Value = dtKeiretu.Rows(0).Item("keiretu_cd").ToString
                    Me.hidModouru.Value = "Keiretu"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '�����̏ꍇ
                    If ShowPop > 1 Then
                        If Me.hidKeiretuMei.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtKeiretu.Rows.Count - 1
                                If Me.hidKeiretuMei.Value.ToString.Trim = dtKeiretu.Rows(i).Item("keiretu_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '�s���{��
            Case KakusyuSyukeiInquiry.popKbn.Kameiten
                Dim TodoufukenSearchBC As New TodoufukenSearchBC
                Dim dtTodoufuken As New Data.DataTable
                dtTodoufuken = TodoufukenSearchBC.GetTodoufukenMei("0", Me.tbxKameiten.Text, False)

                ShowPop = dtTodoufuken.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxKameiten.Text = dtTodoufuken.Rows(0).Item("todouhuken_mei").ToString
                    Me.hidKameitenCd.Value = dtTodoufuken.Rows(0).Item("todouhuken_cd").ToString
                    Me.hidModouru.Value = "Todouhuken"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '�����̏ꍇ
                    If ShowPop > 1 Then
                        If Me.hidKameitenCd.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtTodoufuken.Rows.Count - 1
                                If Me.hidKameitenCd.Value.ToString.Trim = dtTodoufuken.Rows(i).Item("todouhuken_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If
        End Select

    End Function

    ''' <summary>
    ''' ����POPUP
    ''' </summary>
    ''' <param name="popKbn">POPUP�̋敪</param>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub ShoPop(ByVal popKbn As popKbn)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, popKbn)

        Dim csScript As New StringBuilder
        Select Case popKbn
            Case KakusyuSyukeiInquiry.popKbn.Shiten
                csScript.AppendLine("window.open('./PopupSearch/SitenSearch.aspx?formName=" & Me.Form.ClientID & "&strSitenMei='+ escape($ID('" & Me.tbxSiten.ClientID & "').value)+'&field=" & Me.tbxSiten.ClientID & "'+'&fieldCd=" & Me.hidSitenCd.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'SitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.Kameiten
                csScript.AppendLine("window.open('./PopupSearch/TodoufukenSearch.aspx?formName=" & Me.Form.ClientID & "&strTodouhukenMei='+ escape($ID('" & Me.tbxKameiten.ClientID & "').value)+'&field=" & Me.tbxKameiten.ClientID & "'+'&fieldCd=" & Me.hidKameitenCd.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'TodouhukenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.Eigyou
                csScript.AppendLine("window.open('./PopupSearch/EigyousyoSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyousyoMei='+ escape($ID('" & Me.tbxEigyou.ClientID & "').value)+'&field=" & Me.tbxEigyou.ClientID & "'+'&fieldCd=" & Me.hidEigyouCd.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'EigyousyoSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.Keiretu
                csScript.AppendLine("window.open('./PopupSearch/KeiretuSearch.aspx?formName=" & Me.Form.ClientID & "&strKeiretuCd='+ escape($ID('" & Me.tbxKeiretu.ClientID & "').value) +'&field=" & Me.tbxKeiretu.ClientID & "'+'&fieldMei=" & Me.hidKeiretuMei.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'KeiretuSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.User
                csScript.AppendLine("window.open('./PopupSearch/EigyouManSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyouManMei='+ escape($ID('" & Me.tbxUser.ClientID & "').value)+'&field=" & Me.tbxUser.ClientID & "'+'&fieldCd=" & Me.hidUserCd.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'EigyouManSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.TourokuJigyousya
                csScript.AppendLine("window.open('./PopupSearch/TourokuJigyousyaSearch.aspx?formName=" & Me.Form.ClientID & "&strTourokuJigyousya='+ escape($ID('" & Me.tbxTourokuJgousya.ClientID & "').value)+'&field=" & Me.tbxTourokuJgousya.ClientID & "'+'&fieldCd=" & Me.hidTourokuJgousya.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'TourokuJigyousya', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=850,height=500,top=30,left=0');")
        End Select

        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), popKbn.ToString, csScript.ToString, True)

    End Sub

    ''' <summary>
    ''' �N�xDropdownList���擾����,����FromDropdownList���擾����,����FromDropdownList���擾����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/12/05�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub GetNendoListDate()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '�N�xDropdownList���N���A����
        Me.ddlNendo.Items.Clear()
        '����FromDropdownList���N���A����
        Me.ddlKikanFrom.Items.Clear()
        '����FromDropdownList���N���A����
        Me.ddlTukinamiFrom.Items.Clear()

        Dim CommonBC As New CommonBC
        Dim objCommon As New Common
        Dim dtNendo As Data.DataTable = CommonBC.GetKeikakuNendoData()

        '�V�X�e���N�x���擾����
        Dim strSysNen As String = objCommon.GetSystemYear()

        If dtNendo.Rows.Count > 0 Then

            '�N�xLIST�擾
            Me.ddlNendo.DataSource = dtNendo
            Me.ddlNendo.DataValueField = "code"
            Me.ddlNendo.DataTextField = "meisyou"
            Me.ddlNendo.DataBind()
            Me.ddlNendo.Items.Insert(0, New ListItem("", "0"))
            '�����\���̓V�X�e���N
            Me.ddlNendo.SelectedValue = strSysNen

            '���Ԃ̔N�xLIST�擾
            Me.ddlKikanFrom.DataSource = dtNendo
            Me.ddlKikanFrom.DataValueField = "code"
            Me.ddlKikanFrom.DataTextField = "meisyou"
            Me.ddlKikanFrom.DataBind()
            Me.ddlKikanFrom.Items.Insert(0, New ListItem("", "0"))
            '�����\���̓V�X�e���N
            Me.ddlKikanFrom.SelectedValue = strSysNen

            '�����̂̔N�xLIST�擾
            Me.ddlTukinamiFrom.DataSource = dtNendo
            Me.ddlTukinamiFrom.DataValueField = "code"
            Me.ddlTukinamiFrom.DataTextField = "meisyou"
            Me.ddlTukinamiFrom.DataBind()
            Me.ddlTukinamiFrom.Items.Insert(0, New ListItem("", "0"))
            '�����\���̓V�X�e���N
            Me.ddlTukinamiFrom.SelectedValue = strSysNen

        End If

    End Sub

    ''' <summary>
    ''' ����ToDropdownList���擾����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/12/05�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub GetKikanToListDate()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '����ToDropdownList���N���A����
        Me.ddlKikanTo.Items.Clear()

        Me.ddlKikanTo.Items.Insert(0, New ListItem("���", "0"))
        Me.ddlKikanTo.Items.Insert(1, New ListItem("����", "1"))
        Me.ddlKikanTo.Items.Insert(2, New ListItem("�l����(4,5,6��)", "2"))
        Me.ddlKikanTo.Items.Insert(3, New ListItem("�l����(7,8,9��)", "3"))
        Me.ddlKikanTo.Items.Insert(4, New ListItem("�l����(10,11,12��)", "4"))
        Me.ddlKikanTo.Items.Insert(5, New ListItem("�l����(1,2,3��)", "5"))

    End Sub

    ''' <summary>
    '''  JS�쐬
    ''' </summary>
    ''' <history>2012/12/07�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub MakeJavaScript()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                                MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("function window.onload()")
            .AppendLine("{")
            .AppendLine("   //alert('1');")
            .AppendLine("   window.name ='" & CommonConstBC.uriageYojituKanri & "';")
            .AppendLine("   setMenuBgColor();")
            .AppendLine("   fncTextBoxTrue();")
            .AppendLine("}")
            .AppendLine("function fncTextBoxTrue()")
            .AppendLine("{")
            .AppendLine("//alert($ID('" & Me.hidModouru.ClientID & "').value);")
            .AppendLine("   switch($ID('" & Me.hidModouru.ClientID & "').value) ")
            .AppendLine("   {")
            '�x�XTextbox��Clear
            .AppendLine("   case 'Siten':")
            .AppendLine("      if($ID('" & Me.tbxSiten.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            '�x�X��Hidden���N���A����
            .AppendLine("         $ID('" & Me.hidSitenCd.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            .AppendLine("         $ID('" & Me.tbxSiten.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnSiten.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '�s���{��Textbox��Clear
            .AppendLine("   case 'Todouhuken':")
            .AppendLine("      if($ID('" & Me.tbxKameiten.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            '�x�X��Hidden���N���A����
            .AppendLine("         $ID('" & Me.hidKameitenCd.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '�s���{��
            .AppendLine("         $ID('" & Me.tbxKameiten.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnKameiten.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '�c�Ə�Textbox��Clear
            .AppendLine("   case 'Eigyousyo':")
            .AppendLine("      if($ID('" & Me.tbxEigyou.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            '�c�Ə���Hidden���N���A����
            .AppendLine("         $ID('" & Me.hidEigyouCd.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '�c�Ə�
            .AppendLine("         $ID('" & Me.tbxEigyou.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnEigyou.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '�n��Textbox��Clear
            .AppendLine("   case 'Keiretu':")
            .AppendLine("      if($ID('" & Me.tbxKeiretu.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            .AppendLine("         $ID('" & Me.hidKeiretuMei.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '�n��
            .AppendLine("         $ID('" & Me.tbxKeiretu.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnKeiretu.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '�c�ƃ}��Textbox��Clear
            .AppendLine("   case 'EigyouMan':")
            .AppendLine("      if($ID('" & Me.tbxUser.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            .AppendLine("         $ID('" & Me.hidUserCd.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '�c�ƃ}��
            .AppendLine("         $ID('" & Me.tbxUser.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnUser.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '�o�^���Ǝ�Textbox��Clear
            .AppendLine("   case 'Tourokusya':")
            .AppendLine("      if($ID('" & Me.tbxTourokuJgousya.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            .AppendLine("         $ID('" & Me.hidTourokuJgousya.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '�o�^���Ǝ�
            .AppendLine("         $ID('" & Me.tbxTourokuJgousya.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnTourokuJgousya.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")
            .AppendLine("        default:")
            .AppendLine("           fncSetDisabled(false);")
            .AppendLine("   }")
            .AppendLine("  return false;")
            .AppendLine("}")

            '�������͑傫���ɕύX����
            .AppendLine("function fncToUpper(strTextBoxValue)")
            .AppendLine("   {")
            .AppendLine("      var str;")
            .AppendLine("      str = strTextBoxValue.value.toUpperCase();")
            .AppendLine("      strTextBoxValue.value = str")
            .AppendLine("   }")

            .AppendLine("function fncSetDisabled(flg)")
            .AppendLine("   {")
            '�x�X��
            .AppendLine("         $ID('" & Me.tbxSiten.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnSiten.ClientID & "').disabled = flg;")
            '�s���{��
            .AppendLine("         $ID('" & Me.tbxKameiten.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnKameiten.ClientID & "').disabled = flg;")
            '�c�Ə�
            .AppendLine("         $ID('" & Me.tbxEigyou.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnEigyou.ClientID & "').disabled = flg;")
            '�n��
            .AppendLine("         $ID('" & Me.tbxKeiretu.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnKeiretu.ClientID & "').disabled = flg;")
            '�c�ƃ}��
            .AppendLine("         $ID('" & Me.tbxUser.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnUser.ClientID & "').disabled = flg;")
            '�o�^���Ǝ�
            .AppendLine("         $ID('" & Me.tbxTourokuJgousya.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnTourokuJgousya.ClientID & "').disabled = flg;")
            .AppendLine("   }")

            .AppendLine("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' ��ʂ�JS EVENT�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/03/26 ���F �V�K�쐬 </para>	
    ''' </history>	
    Private Sub SetJsEvent()
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '�S�Ђ̌v��l�ۑ��{�^��������
        Me.btnAllSave.OnClick = "BtnAllSave_Click()"

    End Sub

    ''' <summary>
    ''' ���ʕ\���{�^���������̏���
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/03/26 ���F �V�K�쐬 </para>	
    ''' </history>	
    Public Function BtnAllSave_Click() As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '�N�xLIST��I�����Ȃ���
        If ((Me.ddlNendo.Enabled = True) AndAlso (Me.ddlNendo.SelectedValue = 0)) OrElse _
           ((Me.ddlKikanFrom.Enabled = True) AndAlso (Me.ddlKikanFrom.SelectedValue = 0)) OrElse _
           ((Me.ddlTukinamiFrom.Enabled = True) AndAlso (Me.ddlTukinamiFrom.SelectedValue = 0)) Then
            '���b�Z�[�W��\������
            common.SetShowMessage(Me, MSG001E, "�N�x")
            '���ו���\�����Ȃ�
            Me.divHead.Visible = False

            If Me.ddlNendo.Enabled = True Then
                Me.ddlNendo.Focus()
            ElseIf Me.ddlKikanFrom.Enabled = True Then
                Me.ddlKikanFrom.Focus()
            ElseIf Me.ddlTukinamiFrom.Enabled = True Then
                Me.ddlTukinamiFrom.Focus()
            End If

            '���ʕ\���Q�����I�ׂ�悤��
        ElseIf (Me.ddlTukinamiFrom.Enabled = True) AndAlso (Me.ddlTukinamiTo.SelectedItem.Value = "0") Then
            common.SetShowMessage(Me, MSG048E)
            Me.ddlTukinamiTo.Focus()

            'FC�`�F�b�N�{�b�N�X�Ǝx�X���̊֌W
        ElseIf (Me.chkFC.Checked = True) AndAlso (Me.tbxSiten.Text = String.Empty) Then

            Me.tbxSiten.Focus()

            '���b�Z�[�W��\������
            common.SetShowMessage(Me, MSG001E, "�x�X��")
            '���ו���\�����Ȃ�
            Me.divHead.Visible = False

            '�W�v ���e�I������I�����Ȃ���
        ElseIf Me.tbxSiten.Text = String.Empty AndAlso _
               Me.tbxKameiten.Text = String.Empty AndAlso _
               Me.tbxEigyou.Text = String.Empty AndAlso _
               Me.tbxKeiretu.Text = String.Empty AndAlso _
               Me.tbxUser.Text = String.Empty AndAlso _
               Me.tbxTourokuJgousya.Text = String.Empty Then

            '���b�Z�[�W��\������
            common.SetShowMessage(Me, MSG058E)
            '���ו���\�����Ȃ�
            Me.divHead.Visible = False

            '�c�Ƌ敪����I�����Ȃ���
        ElseIf Me.chkEigyou.Checked = False AndAlso _
                Me.chkFC.Checked = False AndAlso _
                Me.chkSinki.Checked = False AndAlso _
                Me.chkTokuhan.Checked = False Then
            '���b�Z�[�W��\������
            common.SetShowMessage(Me, MSG071E)

            '�����{�^���̉������f����
        ElseIf Not Me.tbxSiten.Text.Equals(String.Empty) OrElse _
               Not Me.tbxKameiten.Text.Equals(String.Empty) OrElse _
               Not Me.tbxEigyou.Text.Equals(String.Empty) OrElse _
               Not Me.tbxKeiretu.Text.Equals(String.Empty) OrElse _
               Not Me.tbxUser.Text.Equals(String.Empty) OrElse _
               Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then

            If CheckInput() = True OrElse _
               Me.tbxSiten.Text.Equals("ALL") OrElse _
               Me.tbxKameiten.Text.Equals("ALL") OrElse _
               Me.tbxEigyou.Text.Equals("ALL") OrElse _
               Me.tbxKeiretu.Text.Equals("ALL") OrElse _
               Me.tbxUser.Text.Equals("ALL") OrElse _
               Me.tbxTourokuJgousya.Text.Equals("ALL") Then

                '��ʂőI�������̍���
                If Not Me.tbxSiten.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "�x�X"
                ElseIf Not Me.tbxKameiten.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "�s���{��"
                ElseIf Not Me.tbxEigyou.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "�c�Ə�"
                ElseIf Not Me.tbxKeiretu.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "�n��"
                ElseIf Not Me.tbxUser.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "�c�ƃ}��"
                ElseIf Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "�o�^���Ǝ�"
                End If

                Dim dtMeisaiDate As Data.DataTable
                '�x�X
                Dim strSiten As String
                If Me.tbxSiten.Text.Equals("ALL") Then
                    strSiten = Me.tbxSiten.Text
                Else
                    strSiten = Me.hidSitenCd.Value
                End If
                '�s���{��
                Dim strKameitenCd As String
                If Me.tbxKameiten.Text.Equals("ALL") Then
                    strKameitenCd = Me.tbxKameiten.Text
                Else
                    strKameitenCd = Me.hidKameitenCd.Value
                End If
                '�c�Ə�
                Dim strEigyouCd As String
                If Me.tbxEigyou.Text.Equals("ALL") Then
                    strEigyouCd = Me.tbxEigyou.Text
                Else
                    strEigyouCd = Me.hidEigyouCd.Value
                End If
                '�n��
                Dim strKeiretuMei As String
                If Me.tbxKeiretu.Text.Equals("ALL") Then
                    strKeiretuMei = Me.tbxKeiretu.Text
                Else
                    strKeiretuMei = Me.hidKeiretuMei.Value
                End If
                '�c�ƃ}��
                Dim strUserCd As String
                If Me.tbxUser.Text.Equals("ALL") Then
                    strUserCd = Me.tbxUser.Text
                Else
                    strUserCd = Me.hidUserCd.Value
                End If
                '�o�^���Ǝ�
                Dim strTourokuJgousya As String
                If Me.tbxTourokuJgousya.Text.Equals("ALL") Then
                    strTourokuJgousya = Me.tbxTourokuJgousya.Text
                Else
                    strTourokuJgousya = Me.hidTourokuJgousya.Value
                End If

                '�c�Ƌ敪
                Dim strEigyouKbn As String = String.Empty '�c��
                If Me.chkEigyou.Checked = True Then
                    strEigyouKbn = strEigyouKbn & ",1"
                End If
                If Me.chkTokuhan.Checked = True Then '����
                    strEigyouKbn = strEigyouKbn & ",3"
                End If
                If Me.chkSinki.Checked = True Then '�V�K
                    strEigyouKbn = strEigyouKbn & ",2"
                End If
                If Me.chkFC.Checked = True Then 'FC
                    strEigyouKbn = strEigyouKbn & ",4"
                End If

                '�f�[�^���擾����
                If (Me.ddlNendo.Enabled = True) Then

                    'FC�̃`�F�b�N�{�b�N�X���`�F�b�N���Ȃ�
                    If Me.chkFC.Checked = False Then

                        '�N��:��ʂ̃f�[�^���擾����
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
                                       strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
                                       strTourokuJgousya, Me.ddlNendo.SelectedValue, 1, 12, strEigyouKbn)

                    ElseIf (Me.chkFC.Checked = True) AndAlso _
                           (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

                        'FC�̃`�F�b�N�{�b�N�X���`�F�b�N����
                        '�N��:��ʂ̃f�[�^���擾����
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
                                                              Me.ddlNendo.SelectedValue, 1, 12)
                    Else
                        '�W�v
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
                                       Me.ddlNendo.SelectedValue, 1, 12, strEigyouKbn)

                    End If

                ElseIf (Me.ddlKikanTo.Enabled = True AndAlso Me.ddlKikanFrom.Enabled = True) Then

                    Dim intBegin As Integer
                    Dim intEnd As Integer
                    If Me.ddlKikanTo.SelectedItem.Text = "���" Then
                        intBegin = 4
                        intEnd = 9
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "�l����(4,5,6��)" Then
                        intBegin = 4
                        intEnd = 6
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "�l����(7,8,9��)" Then
                        intBegin = 7
                        intEnd = 9
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "�l����(10,11,12��)" Then
                        intBegin = 10
                        intEnd = 12
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "�l����(1,2,3��)" Then
                        intBegin = 1
                        intEnd = 3
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "����" Then
                        intBegin = 10
                        intEnd = 15
                    End If

                    'FC�̃`�F�b�N�{�b�N�X���`�F�b�N���Ȃ�
                    If Me.chkFC.Checked = False Then

                        '����:��ʂ̃f�[�^���擾����
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
                                       strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
                                       strTourokuJgousya, Me.ddlKikanFrom.SelectedValue, intBegin, intEnd, strEigyouKbn)

                    ElseIf (Me.chkFC.Checked = True) AndAlso _
                           (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

                        'FC�̃`�F�b�N�{�b�N�X���`�F�b�N����
                        '����:��ʂ̃f�[�^���擾����
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
                                        Me.ddlKikanFrom.SelectedValue, intBegin, intEnd)

                    Else
                        '�W�v
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
                                       Me.ddlKikanFrom.SelectedValue, intBegin, intEnd, strEigyouKbn)

                    End If

                ElseIf (Me.ddlTukinamiFrom.Enabled = True AndAlso Me.ddlTukinamiTo.Enabled = True AndAlso Me.ddlTukinamiTo2.Enabled = True) Then

                    Dim strTukinamiTo As String
                    If Me.ddlTukinamiTo.SelectedValue < 4 Then
                        strTukinamiTo = Me.ddlTukinamiTo.SelectedValue + 12
                    Else
                        strTukinamiTo = Me.ddlTukinamiTo.SelectedValue
                    End If

                    Dim strTukinamiTo2 As String
                    If Me.ddlTukinamiTo2.SelectedItem IsNot Nothing Then
                        If Me.ddlTukinamiTo2.SelectedItem.Text.Equals("2��") Then
                            strTukinamiTo2 = 14
                        ElseIf Me.ddlTukinamiTo2.SelectedItem.Text.Equals("3��") Then
                            strTukinamiTo2 = 15
                        Else
                            strTukinamiTo2 = Me.ddlTukinamiTo2.SelectedValue
                        End If
                    Else
                        strTukinamiTo2 = 15
                    End If


                    'FC�̃`�F�b�N�{�b�N�X���`�F�b�N���Ȃ�
                    If Me.chkFC.Checked = False Then

                        '����:��ʂ̃f�[�^���擾����
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
                                       strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
                                       strTourokuJgousya, Me.ddlTukinamiFrom.SelectedValue, _
                                       strTukinamiTo, strTukinamiTo2, strEigyouKbn)

                    ElseIf (Me.chkFC.Checked = True) AndAlso _
                           (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

                        'FC�̃`�F�b�N�{�b�N�X���`�F�b�N����
                        '����:��ʂ̃f�[�^���擾����
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
                                       Me.ddlTukinamiFrom.SelectedValue, strTukinamiTo, strTukinamiTo2)

                    Else
                        '�W�v
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
                                       Me.ddlTukinamiFrom.SelectedValue, strTukinamiTo, strTukinamiTo2, strEigyouKbn)
                    End If

                Else
                    dtMeisaiDate = Nothing
                End If

                If dtMeisaiDate.Rows.Count = 0 Then
                    '���b�Z�[�W��\������
                    common.SetShowMessage(Me, MSG067E)
                    '���ו���\�����Ȃ�
                    Me.divHead.Visible = False
                Else

                    '���ו���\������
                    Me.divHead.Visible = True
                    '��ʂ�
                    Call BoundGridView(dtMeisaiDate)
                End If

            End If

        End If

        If Me.tbxSiten.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Siten"
        ElseIf Me.tbxKameiten.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Todouhuken"
        ElseIf Me.tbxEigyou.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Eigyousyo"
        ElseIf Me.tbxKeiretu.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Keiretu"
        ElseIf Me.tbxUser.Text.Equals("ALL") Then
            Me.hidModouru.Value = "EigyouMan"
        ElseIf Me.tbxTourokuJgousya.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Tourokusya"
        End If

    End Function

#End Region

End Class
