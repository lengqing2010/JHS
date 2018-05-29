Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports System.Data

''' <summary>
''' �x�X ���ʌv��l�Ɖ�
''' </summary>
''' <remarks>�x�X ���ʌv��l�Ɖ�</remarks>
''' <history>
''' <para>2012/11/24 P-44979 ���g �V�K�쐬 </para>
''' </history>
Partial Class SitenTukibetuKeikakuchiSearchList
    Inherits System.Web.UI.Page

#Region "�v���C�x�[�g�ϐ�"

    Private objCommon As New Common
    Private objSitenTukibetuKeikakuchiSearchListBC As New Lixil.JHS_EKKS.BizLogic.SitenTukibetuKeikakuchiSearchListBC
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC

#End Region

#Region "�萔"

    Private Const CON_TITLE As String = "�x�X ���ʌv��l�ݒ�"
    '�p�[�X
    Public Const sDirName As String = "C:\jhs_ekks\"
    '�T�[�r�X�p�[�X
    Public sv_templist As String = getfiledatetimelist("data")

#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '���[�U�[�����̃`�F�b�N
        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)

        Call check()

        If Not IsPostBack Then

            Call PageInit()

            '�uExcel�o�́v�{�^������������
            Me.btnSyuturyoku1.OnClientClick = "body_onLoad(""1"");return false;"
        Else
            Select Case hidSeni.Value
                Case "1" 'Excel�o��

                    MakePopJavaScript()
                    ClientScript.RegisterStartupScript(Me.GetType(), "ERR", "setTimeout('PopPrint()',10);", True)
            End Select
            hidSeni.Value = ""

        End If

        'JavaScript���쐬����
        Call MakeJavaScript()

        '��ʂ�JS EVENT�ݒ�
        Call SetJsEvent()

    End Sub

    ''' <summary>
    ''' �x�X�������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Protected Sub btnShitenKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShitenKensaku.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Call ShowPop(True)

    End Sub

    ''' <summary>
    ''' ���ʌv��\���{�^�����N���b�N����ꍇ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Protected Sub btnKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '�`�F�b�N���s��
        If CheckInput() = False Then
            '��ʏ����̃Z�b�g
            Me.divMeisai.Visible = False
            Me.btnTorikomi.Enabled = False
            Me.btnKeikakuMinaosi.Enabled = False
            Me.btnSitenbetuTukiConfirm.Enabled = False
            Me.lblSumi.Text = ""
            Exit Sub
        End If

        Dim strNendo As String
        Dim strBusyoCd As String
        Dim addDate As New ArrayList
        Dim addEigyouKbn As New ArrayList
        strNendo = Me.ddlNendo.SelectedValue.Trim.ToString
        strBusyoCd = Me.hidCitenCd.Value.Trim.ToString

        '�v��f�[�^
        Dim dtKeikaku As New Data.DataTable
        dtKeikaku = objSitenTukibetuKeikakuchiSearchListBC.GetSitenbetuTukiKeikakuKanri(strNendo, strBusyoCd)
        '���N�f�[�^
        Dim dtLastYear As New Data.DataTable
        dtLastYear = objSitenTukibetuKeikakuchiSearchListBC.GetJissekiKanri(strNendo, strBusyoCd)

        '�Y���f�[�^�����݂��Ȃ��ꍇ�A
        If dtKeikaku.Rows.Count = 0 AndAlso dtLastYear.Rows.Count = 0 Then
            '���b�Z�[�W��\������
            objCommon.SetShowMessage(Me, CommonMessage.MSG011E)
            '��ʏ����̃Z�b�g
            Me.divMeisai.Visible = False
            Me.btnTorikomi.Enabled = True
            Me.btnKeikakuMinaosi.Enabled = False
            Me.btnSitenbetuTukiConfirm.Enabled = False
            lblSumi.Text = ""

            Exit Sub
        Else
            Me.divMeisai.Visible = True
        End If

        If dtKeikaku.Rows.Count = 0 Then
            '�{�^���̐���
            Me.btnTorikomi.Enabled = True
            Me.btnSitenbetuTukiConfirm.Enabled = False
            Me.btnKeikakuMinaosi.Enabled = False
            lblSumi.Text = ""

        Else

            '�o�^�����̊i�[
            For i As Integer = 0 To dtKeikaku.Rows.Count - 1
                addDate.Add(dtKeikaku.Rows(i).Item("add_datetime"))
                addEigyouKbn.Add(dtKeikaku.Rows(i).Item("eigyou_kbn"))
            Next

            '�v��l�s��FLG
            If dtKeikaku.Rows(0).Item("kakutei_flg").ToString = "1" Then
                Me.btnTorikomi.Enabled = False
                Me.btnSitenbetuTukiConfirm.Enabled = False
                lblSumi.Text = "�u�v��ρv"
                lblSumi.Style.Add("color", "blue")

                '�v��l�s��FLG
                If dtKeikaku.Rows(0).Item("keikaku_huhen_flg").ToString = "1" Then
                    Me.btnKeikakuMinaosi.Enabled = False
                Else
                    Me.btnKeikakuMinaosi.Enabled = True
                End If

            Else
                Me.btnSitenbetuTukiConfirm.Enabled = True
                Me.btnKeikakuMinaosi.Enabled = False
                lblSumi.Text = ""

                '�v��l�s��FLG
                If dtKeikaku.Rows(0).Item("keikaku_huhen_flg").ToString = "1" Then
                    Me.btnTorikomi.Enabled = False
                Else
                    Me.btnTorikomi.Enabled = True
                End If

            End If

            If dtKeikaku.Rows.Count < 3 Then
                '���ƕʂɃf�[�^�̍쐬
                dtKeikaku = MeisaiDataSakusei(dtKeikaku)
            End If


        End If

        If dtLastYear.Rows.Count < 3 AndAlso dtLastYear.Rows.Count <> 0 Then
            '���ƕʂɃf�[�^�̍쐬
            dtLastYear = MeisaiDataSakusei(dtLastYear)
        End If

        '���ׂ̃f�[�^�Z�b�g
        SetMeisaiData(dtKeikaku, dtLastYear)

        ViewState("nendo") = strNendo
        ViewState("busyoCd") = strBusyoCd
        ViewState("busyoMei") = Me.tbxSiten.Text.ToString
        ViewState("addDate") = addDate
        ViewState("addEigyouKbn") = addEigyouKbn
    End Sub

#End Region

#Region "�����b�h"

    ''' <summary>
    ''' ��ʏ�����
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private Sub PageInit()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim CommonBC As New CommonBC
        '�N�x�@�I���̃Z�b�g
        Dim dtKeikakuNendo As Data.DataTable = CommonBC.GetKeikakuNendoData()
        ddlNendo.DataSource = dtKeikakuNendo
        ddlNendo.DataBind()
        Me.ddlNendo.SelectedIndex = -1

        Dim strSysNen As String
        '�V�X�e���N�x���擾����
        strSysNen = objCommon.GetSystemYear()

        '�V�X�e���N�x��ݒ肷��
        For i As Integer = 0 To Me.ddlNendo.Items.Count - 1
            If Me.ddlNendo.Items(i).Value.Equals(strSysNen) Then
                Me.ddlNendo.Items(i).Selected = True
                Exit For
            End If
        Next

        '��ʏ����̃Z�b�g
        Me.divMeisai.Visible = False
        Me.btnTorikomi.Enabled = False
        Me.btnKeikakuMinaosi.Enabled = False
        Me.btnSitenbetuTukiConfirm.Enabled = False
        Me.lblSumi.Text = ""

    End Sub

    ''' <summary>
    ''' ����POPUP
    ''' </summary>
    ''' <param name="blnPop"></param>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private Function ShowPop(ByVal blnPop As Boolean) As Integer

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, blnPop)

        Dim csScript As New StringBuilder

        Dim SitenSearchBC As New SitenSearchBC
        Dim dtSiten As Data.DataTable
        If blnPop Then
            dtSiten = SitenSearchBC.GetBusyoKanri("0", tbxSiten.Text, False)
        Else
            dtSiten = SitenSearchBC.GetBusyoKanri("0", tbxSiten.Text, False, False)
        End If

        ShowPop = dtSiten.Rows.Count
        If dtSiten.Rows.Count = 1 Then
            Me.tbxSiten.Text = dtSiten.Rows(0).Item(0).ToString
            Me.hidCitenCd.Value = dtSiten.Rows(0).Item(1).ToString
        Else
            If blnPop Then
                'popUp�����̏ꍇ
                csScript.AppendLine("window.open('./PopupSearch/SitenSearch.aspx?formName=" & Me.Form.ClientID & "&strSitenMei='+ escape($ID('" & Me.tbxSiten.ClientID & "').value)+'&field=" & Me.tbxSiten.ClientID & "'+'&fieldCd=" & Me.hidCitenCd.ClientID & "', 'SitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
                '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
                ClientScript.RegisterStartupScript(Me.GetType(), "1", csScript.ToString, True)
            Else
                '�����̏ꍇ
                If ShowPop > 1 Then
                    If Me.hidCitenCd.Value.ToString.Trim <> String.Empty Then
                        For i As Integer = 0 To dtSiten.Rows.Count - 1
                            If Me.hidCitenCd.Value.ToString.Trim = dtSiten.Rows(i).Item(1).ToString() Then
                                ShowPop = 1
                                Exit For
                            End If
                        Next

                    End If
                End If

            End If
        End If

    End Function

    ''' <summary>
    ''' JavaScript���쐬����
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private Sub MakeJavaScript()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")

            .AppendLine("</script>  ")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' ��ʂ�JS EVENT�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private Sub SetJsEvent()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        'CSV�o�̓{�^��������
        Me.btnSyuturyoku.OnClick = "BtnSyuturyoku_Click()"

        'CSV��荞�݃{�^��������
        Me.btnTorikomi.Button.Attributes.Add("onClick", "window.open('SitenTukibetuKeikakuchiInput.aspx', 'PopupCSVInput');return false;")

        '�v�挩�����{�^��������
        Me.btnKeikakuMinaosi.OnClick = "BtnKeikakuMinaosi_Click()"

        '�x�X�� ���ʌv��l�m��{�^��������
        Me.btnSitenbetuTukiConfirm.OnClick = "BtnSitenbetuTukiConfirm_Click()"

        Me.tbxSiten.Attributes("onkeydown") = "if (event.keyCode==13){event.keyCode=9;}"

    End Sub

    ''' <summary>
    ''' CSV�o�̓{�^�����N���b�N���鎞
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Sub BtnSyuturyoku_Click()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '�`�F�b�N���s��
        If CheckInput() = False Then
            Exit Sub
        End If

        '��ʑJ��
        Dim csScript As New StringBuilder

        With csScript
            .AppendLine("function window.onload()")
            .AppendLine("{")
            .AppendLine("  $ID('" & Me.btnSyuturyoku1.ClientID & "').click();")
            .AppendLine("}")
        End With

        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "PopupWaitMsg", csScript.ToString, True)

    End Sub

    ''' <summary>
    ''' �v�挩�����{�^�����N���b�N���鎞
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Sub BtnKeikakuMinaosi_Click()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        If objSitenTukibetuKeikakuchiSearchListBC.SetKakuteiFlg(ViewState("nendo").ToString, _
                                                                ViewState("busyoCd").ToString, _
                                                                ViewState("addDate"), _
                                                                ViewState("addEigyouKbn"), _
                                                                Master.loginUserInfo.Items(0).Account.ToString, _
                                                                "0") Then
            Me.btnTorikomi.Enabled = True
            Me.btnSitenbetuTukiConfirm.Enabled = True
            Me.btnKeikakuMinaosi.Enabled = False
            Me.lblSumi.Text = ""

        End If

        '��ʂ̍ĕ\��
        Dim strNendo As String
        Dim strBusyoCd As String
        strNendo = ViewState("nendo").ToString
        strBusyoCd = ViewState("busyoCd").ToString
        '��ʂ̃Z�b�g
        Me.tbxSiten.Text = ViewState("busyoMei").ToString
        Me.ddlNendo.SelectedValue = strNendo
        Me.hidCitenCd.Value = strBusyoCd

        '�v��f�[�^
        Dim dtKeikaku As New Data.DataTable
        dtKeikaku = objSitenTukibetuKeikakuchiSearchListBC.GetSitenbetuTukiKeikakuKanri(strNendo, strBusyoCd)
        '���N�f�[�^
        Dim dtLastYear As New Data.DataTable
        dtLastYear = objSitenTukibetuKeikakuchiSearchListBC.GetJissekiKanri(strNendo, strBusyoCd)

        If dtKeikaku.Rows.Count < 3 Then
            '���ƕʂɃf�[�^�̍쐬
            dtKeikaku = MeisaiDataSakusei(dtKeikaku)
        End If

        '�v��l�s��FLG
        If dtKeikaku.Rows(0).Item("keikaku_huhen_flg").ToString = "1" Then
            Me.btnTorikomi.Enabled = False
        End If

        If dtLastYear.Rows.Count < 3 AndAlso dtLastYear.Rows.Count <> 0 Then
            '���ƕʂɃf�[�^�̍쐬
            dtLastYear = MeisaiDataSakusei(dtLastYear)
        End If

        '���ׂ̃f�[�^�Z�b�g
        SetMeisaiData(dtKeikaku, dtLastYear)

    End Sub

    ''' <summary>
    ''' �x�X�� ���ʌv��l�m��{�^�����N���b�N���鎞
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Sub BtnSitenbetuTukiConfirm_Click()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)


        If objSitenTukibetuKeikakuchiSearchListBC.SetKakuteiFlg(ViewState("nendo").ToString, _
                                                                    ViewState("busyoCd").ToString, _
                                                                    ViewState("addDate"), _
                                                                    ViewState("addEigyouKbn"), _
                                                                    Master.loginUserInfo.Items(0).Account.ToString, _
                                                                    "1") Then
            Me.btnTorikomi.Enabled = False
            Me.btnSitenbetuTukiConfirm.Enabled = False
            Me.btnKeikakuMinaosi.Enabled = True

            Me.lblSumi.Text = "�u�v��ρv"
            Me.lblSumi.Style.Add("color", "blue")

        End If

        '��ʂ̍ĕ\��
        Dim strNendo As String
        Dim strBusyoCd As String
        strNendo = ViewState("nendo").ToString
        strBusyoCd = ViewState("busyoCd").ToString

        '�v��f�[�^
        Dim dtKeikaku As New Data.DataTable
        dtKeikaku = objSitenTukibetuKeikakuchiSearchListBC.GetSitenbetuTukiKeikakuKanri(strNendo, strBusyoCd)
        '���N�f�[�^
        Dim dtLastYear As New Data.DataTable
        dtLastYear = objSitenTukibetuKeikakuchiSearchListBC.GetJissekiKanri(strNendo, strBusyoCd)

        If dtKeikaku.Rows.Count < 3 Then
            '���ƕʂɃf�[�^�̍쐬
            dtKeikaku = MeisaiDataSakusei(dtKeikaku)
        End If

        If dtLastYear.Rows.Count < 3 AndAlso dtLastYear.Rows.Count <> 0 Then
            '���ƕʂɃf�[�^�̍쐬
            dtLastYear = MeisaiDataSakusei(dtLastYear)
        End If

        '�v��l�s��FLG
        If dtKeikaku.Rows(0).Item("keikaku_huhen_flg").ToString = "1" Then
            Me.btnKeikakuMinaosi.Enabled = False
        End If

        '���ׂ̃f�[�^�Z�b�g
        SetMeisaiData(dtKeikaku, dtLastYear)

    End Sub

    ''' <summary>
    ''' ���׃f�[�^���쐬����
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private Function MeisaiDataSakusei(ByVal dtTableBefore As Data.DataTable) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtTable As New Data.DataTable
        Dim drEigyouKbn As Data.DataRow()

        dtTable = dtTableBefore.Copy
        dtTable.Clear()

        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '1' ")
        '�c��
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(0).Item("eigyou_kbn") = "1"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If
        '����
        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '3' ")
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(1).Item("eigyou_kbn") = "3"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If
        '�e�b
        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '4' ")
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(2).Item("eigyou_kbn") = "4"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If

        Return dtTable

    End Function

    ''' <summary>
    ''' ���̓`�F�b�N�`�F�b�N
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckInput() As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        If Me.ddlNendo.SelectedValue = "" Then
            '���b�Z�[�W��\������
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "�N�x �I��")
            Me.ddlNendo.Focus()
            Return False
        End If

        If Me.tbxSiten.Text.Trim = "" Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "�x�X��")
            Me.tbxSiten.Focus()
            Return False
        End If
        Dim intSitenDataCount As Integer
        intSitenDataCount = ShowPop(False)
        If intSitenDataCount = 0 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG022E, "�x�X")
            Me.tbxSiten.Focus()
            Return False

        ElseIf intSitenDataCount > 1 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG062E, "�x�X")
            Me.tbxSiten.Focus()
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' ���׃f�[�^�̃Z�b�g
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Sub SetMeisaiData(ByVal dtKeikaku As Data.DataTable, ByVal dtLastYear As Data.DataTable)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, dtKeikaku, dtLastYear)

        '����
        SitenTukibetuKeikakuchiList4.ShowMeisaiData(dtKeikaku, dtLastYear, "4")
        SitenTukibetuKeikakuchiList5.ShowMeisaiData(dtKeikaku, dtLastYear, "5")
        SitenTukibetuKeikakuchiList6.ShowMeisaiData(dtKeikaku, dtLastYear, "6")
        SitenTukibetuKeikakuchiList7.ShowMeisaiData(dtKeikaku, dtLastYear, "7")
        SitenTukibetuKeikakuchiList8.ShowMeisaiData(dtKeikaku, dtLastYear, "8")
        SitenTukibetuKeikakuchiList9.ShowMeisaiData(dtKeikaku, dtLastYear, "9")
        SitenTukibetuKeikakuchiList10.ShowMeisaiData(dtKeikaku, dtLastYear, "10")
        SitenTukibetuKeikakuchiList11.ShowMeisaiData(dtKeikaku, dtLastYear, "11")
        SitenTukibetuKeikakuchiList12.ShowMeisaiData(dtKeikaku, dtLastYear, "12")
        SitenTukibetuKeikakuchiList1.ShowMeisaiData(dtKeikaku, dtLastYear, "1")
        SitenTukibetuKeikakuchiList2.ShowMeisaiData(dtKeikaku, dtLastYear, "2")
        SitenTukibetuKeikakuchiList3.ShowMeisaiData(dtKeikaku, dtLastYear, "3")
        '�l����
        SitenTukibetuKeikakuchiList456.ShowMeisaiData(dtKeikaku, dtLastYear, "456")
        SitenTukibetuKeikakuchiList789.ShowMeisaiData(dtKeikaku, dtLastYear, "789")
        SitenTukibetuKeikakuchiList101112.ShowMeisaiData(dtKeikaku, dtLastYear, "101112")
        SitenTukibetuKeikakuchiList123.ShowMeisaiData(dtKeikaku, dtLastYear, "123")
        '���
        SitenTukibetuKeikakuchiListKamiki.ShowMeisaiData(dtKeikaku, dtLastYear, "kamiki")
        '����
        SitenTukibetuKeikakuchiListSimoki.ShowMeisaiData(dtKeikaku, dtLastYear, "simoki")
        '�N�x�W�v
        SitenTukibetuKeikakuchiListNendo.ShowMeisaiData(dtKeikaku, dtLastYear, "nendo")

    End Sub

    ''' <summary>
    ''' EXCEL�e���v���[�g�t�@�C���`�F�N
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private Sub check()
        Dim csType As Type = Page.GetType
        Dim csName As String = "check"
        Dim csScript As New StringBuilder

        csScript.Append("<script language='vbscript' type='text/vbscript'>" & vbCrLf)
        csScript.Append("function body_onLoad(obj)" & vbCrLf)
        csScript.Append("   Dim sv_templist,cr_templist,strHour, strMinute,strYEAR, strMONTH, strDAY" & vbCrLf)
        csScript.Append("   Dim TimeStamp,dwn_flg,c_obj_Fso,File_Path,c_obj_Folder,c_obj_File,fl,i" & vbCrLf)
        csScript.Append("   On Error Resume Next" & vbCrLf)

        csScript.Append("       dwn_flg = false" & vbCrLf)
        csScript.Append("       sv_templist=""" & sv_templist & """" & vbCrLf)
        csScript.Append("       cr_templist =""""" & vbCrLf)
        csScript.Append("       File_Path =""" & sDirName & """" & vbCrLf)
        csScript.Append("       If sv_templist <> """" Then" & vbCrLf)

        csScript.Append("           Set c_obj_Fso = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = c_obj_Fso.GetFolder(File_Path)" & vbCrLf)
        csScript.Append("           Set c_obj_File = c_obj_Folder.Files" & vbCrLf)
        csScript.Append("           For Each fl In c_obj_File" & vbCrLf)

        csScript.Append("           If (Ucase(right(fl.Name,3)) = ""XLT"" OR  Ucase(right(fl.Name,3)) = ""XLS"") Then" & vbCrLf)
        csScript.Append("                   If cr_templist <> """" Then" & vbCrLf)
        csScript.Append("                       cr_templist = cr_templist & "",""" & vbCrLf)
        csScript.Append("                   End If" & vbCrLf)
        csScript.Append("                   strYEAR   = Right(""0000"" & CStr(YEAR(fl.DateLastModified)), 4)" & vbCrLf)
        csScript.Append("                   strMONTH  = Right(""00"" & CStr(MONTH(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strDAY    = Right(""00"" & Cstr(DAY(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strHour   = Right(""00"" & CStr(Hour(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strMinute = Right(""00"" & CStr(Minute(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   timeStamp = strYEAR & strMONTH & strDAY & strHour & strMinute" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"" "","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,""/"","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"":"","""")" & vbCrLf)

        csScript.Append("                   cr_templist = cr_templist & trim(fl.Name) & "":"" & trim(timestamp)" & vbCrLf)

        csScript.Append("               End If" & vbCrLf)
        csScript.Append("           Next" & vbCrLf)

        csScript.Append("           Set c_obj_File = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = Nothing" & vbCrLf)
        csScript.Append("           If Err > 0 or cr_templist = """" Then" & vbCrLf)
        csScript.Append("               dwn_flg = true" & vbCrLf)
        csScript.Append("           Else" & vbCrLf)
        csScript.Append("               ar_cr = split(Ucase(cr_templist), "","")" & vbCrLf)
        csScript.Append("               ar_sv = split(Ucase(sv_templist), "","")" & vbCrLf)
        csScript.Append("               For i = 0 to Ubound(ar_sv)" & vbCrLf)
        csScript.Append("                   rc = Filter(ar_cr, ar_sv(i))" & vbCrLf)
        csScript.Append("                   If Ubound(rc) = 0 then" & vbCrLf)
        csScript.Append("                       If rc(0) = """" then" & vbCrLf)
        csScript.Append("                           dwn_flg = true" & vbCrLf)
        csScript.Append("                           Exit For" & vbCrLf)
        csScript.Append("                       End if" & vbCrLf)
        csScript.Append("                   Else" & vbCrLf)
        csScript.Append("                       dwn_flg = true" & vbCrLf)
        csScript.Append("                       Exit For" & vbCrLf)
        csScript.Append("                   End if" & vbCrLf)
        csScript.Append("               Next" & vbCrLf)
        csScript.Append("           End if" & vbCrLf)
        csScript.Append("           If dwn_flg = true Then" & vbCrLf)
        csScript.Append("               call download(obj)" & vbCrLf)
        csScript.Append("           End If" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("       If dwn_flg = false Then" & vbCrLf)
        csScript.Append("           fncSubmit(obj)" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("End function" & vbCrLf)

        csScript.Append("function body_onLoad2(obj)" & vbCrLf)
        csScript.Append("   Dim sv_templist,cr_templist,strHour, strMinute,strYEAR, strMONTH, strDAY" & vbCrLf)
        csScript.Append("   Dim TimeStamp,dwn_flg,c_obj_Fso,File_Path,c_obj_Folder,c_obj_File,fl,i" & vbCrLf)
        csScript.Append("   On Error Resume Next" & vbCrLf)
        csScript.Append("       dwn_flg = false" & vbCrLf)
        csScript.Append("       sv_templist=""" & sv_templist & """" & vbCrLf)
        csScript.Append("       cr_templist =""""" & vbCrLf)
        csScript.Append("       File_Path =""" & sDirName & """" & vbCrLf)
        csScript.Append("       If sv_templist <> """" Then" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = c_obj_Fso.GetFolder(File_Path)" & vbCrLf)
        csScript.Append("           Set c_obj_File = c_obj_Folder.Files" & vbCrLf)
        csScript.Append("           For Each fl In c_obj_File" & vbCrLf)
        csScript.Append("               If (Ucase(right(fl.Name,3)) = ""XLT"" OR  Ucase(right(fl.Name,3)) = ""XLS"") Then" & vbCrLf)
        csScript.Append("                   If cr_templist <> """" Then" & vbCrLf)
        csScript.Append("                       cr_templist = cr_templist & "",""" & vbCrLf)
        csScript.Append("                   End If" & vbCrLf)
        csScript.Append("                   strYEAR   = Right(""0000"" & CStr(YEAR(fl.DateLastModified)), 4)" & vbCrLf)
        csScript.Append("                   strMONTH  = Right(""00"" & CStr(MONTH(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strDAY    = Right(""00"" & Cstr(DAY(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strHour   = Right(""00"" & CStr(Hour(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strMinute = Right(""00"" & CStr(Minute(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   timeStamp = strYEAR & strMONTH & strDAY & strHour & strMinute" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"" "","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,""/"","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"":"","""")" & vbCrLf)
        csScript.Append("                   cr_templist = cr_templist & trim(fl.Name) & "":"" & trim(timestamp)" & vbCrLf)
        csScript.Append("               End If" & vbCrLf)
        csScript.Append("           Next" & vbCrLf)

        csScript.Append("           Set c_obj_File = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = Nothing" & vbCrLf)
        csScript.Append("           If Err > 0 or cr_templist = """" Then" & vbCrLf)
        csScript.Append("               dwn_flg = true" & vbCrLf)
        csScript.Append("           Else" & vbCrLf)
        csScript.Append("               ar_cr = split(Ucase(cr_templist), "","")" & vbCrLf)
        csScript.Append("               ar_sv = split(Ucase(sv_templist), "","")" & vbCrLf)
        csScript.Append("               For i = 0 to Ubound(ar_sv)" & vbCrLf)
        csScript.Append("                   rc = Filter(ar_cr, ar_sv(i))" & vbCrLf)
        csScript.Append("                   If Ubound(rc) = 0 then" & vbCrLf)
        csScript.Append("                       If rc(0) = """" then" & vbCrLf)
        csScript.Append("                           dwn_flg = true" & vbCrLf)
        csScript.Append("                           Exit For" & vbCrLf)
        csScript.Append("                       End if" & vbCrLf)
        csScript.Append("                   Else" & vbCrLf)
        csScript.Append("                       dwn_flg = true" & vbCrLf)
        csScript.Append("                       Exit For" & vbCrLf)
        csScript.Append("                   End if" & vbCrLf)
        csScript.Append("               Next" & vbCrLf)
        csScript.Append("           End if" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("       If dwn_flg = false Then" & vbCrLf)
        csScript.Append("           call fncSubmit(obj)" & vbCrLf)
        csScript.Append("       else" & vbCrLf)
        csScript.Append("           call body_load3(obj)" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("End function" & vbCrLf)
        csScript.Append("</script>" & vbCrLf)

        csScript.Append("<script language='javascript' type='text/javascript'>" & vbCrLf)
        csScript.Append("function download(obj){" & vbCrLf)
        csScript.Append("   window.location.href='data/JHS_EKKS.lha';" & vbCrLf)
        csScript.Append("   body_load3(obj);" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.Append("function body_load3(obj){" & vbCrLf)
        csScript.Append("   setTimeout('body_onLoad2(' + obj + ')',1000);" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.Append("function fncSubmit(obj) {" & vbCrLf)

        csScript.Append(Form.Name & "." & hidSeni.ClientID & ".value = obj;" & vbCrLf)
        csScript.Append(Form.Name & ".submit();" & vbCrLf)
        csScript.Append("}" & vbCrLf)
        csScript.Append("</script>" & vbCrLf)

        ClientScript.RegisterStartupScript(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' JavaScript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Protected Sub MakePopJavaScript()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "MakePopJavaScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("    function PopPrint(){")
            .AppendLine("       ShowModal();")
            .AppendLine("       var objwindow=window.open(encodeURI('WaitMsg.aspx?url=SitenTukibetuKeikakuchiExcelOutput.aspx?strNo='+escape('" & Me.ddlNendo.SelectedValue.Trim.ToString & "," & Me.hidCitenCd.Value.Trim.ToString & "," & Me.tbxSiten.Text.Trim.ToString & "')+'|divID=" & Me.Master.DivBuySelName.ClientID & "," & Me.Master.DivDisableId.ClientID & "'),'proxy_operation','width=450,height=150,status=no,resizable=no,directories=no,scrollbars=no,left=0,top=0');" & vbCrLf)
            .AppendLine("       objwindow.focus();")
            .AppendLine("    }" & vbCrLf)
            .AppendLine("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    Private Function getfiledatetimelist(ByVal path As String) As String
        Dim fo As New Scripting.FileSystemObject
        Dim fp As String
        Dim fr As Scripting.Folder
        Dim fc As Scripting.Files
        Dim fl As Scripting.File
        Dim fname As String, s As String, timestamp As String
        Dim strHour As String, strMinute As String
        Dim strYEAR As String, strMONTH As String, strDAY As String

        fp = Server.MapPath(path)
        fr = fo.GetFolder(fp)
        fc = fr.Files
        s = ""
        For Each fl In fc
            fname = fl.Name
            If (UCase(Right(fname, 3)) = "XLT" Or UCase(Right(fname, 3)) = "XLS") Then
                If s <> "" Then
                    s = s & ","
                End If
                timestamp = CStr(fl.DateLastModified)
                ''���t���Ԏ��̐��`
                strYEAR = Right("0000" & CStr(Year(fl.DateLastModified)), 4)
                strMONTH = Right("00" & CStr(Month(fl.DateLastModified)), 2)
                strDAY = Right("00" & CStr(Day(fl.DateLastModified)), 2)
                strHour = Right("00" & CStr(Hour(fl.DateLastModified)), 2)
                strMinute = Right("00" & CStr(Minute(fl.DateLastModified)), 2)
                timestamp = strYEAR & strMONTH & strDAY & strHour & strMinute
                timestamp = Replace(timestamp, " ", "")
                timestamp = Replace(timestamp, "/", "")
                timestamp = Replace(timestamp, ":", "")
                s = s & Trim(fname) & ":" & Trim(timestamp)
            End If
        Next
        getfiledatetimelist = s
        fo = Nothing
        fp = Nothing
        fr = Nothing
        fc = Nothing
        fl = Nothing
    End Function

#End Region

End Class
