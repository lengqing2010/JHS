Public Partial Class PopupSeikyuusyoMiinsatu
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '�s�R���g���[��ID�ړ���
    Private Const CTRL_NAME_TR As String = "resultTr_"

    '���ʃ��W�b�N
    Private cl As New CommonLogic
    '���b�Z�[�W�N���X
    Private MLogic As New MessageLogic
    '���W�b�N�N���X
    Dim MyLogic As New SeikyuuMiinsatuDataSearchLogic

    '�����f�[�^���R�[�h�N���X
    Private dtRec As New SeikyuuDataRecord


    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        Dim jSM As New JibanSessionManager

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master

        '�X�N���v�g�}�l�[�W���[���擾�iScriptManager�p�j
        masterAjaxSM = Me.SM1

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ
            cl.CloseWindow(Me)
            Exit Sub
        End If

        If IsPostBack = False Then

            '�������̃`�F�b�N
            '�o���Ɩ�����
            If userinfo.KeiriGyoumuKengen = 0 Then
                cl.CloseWindow(Me)
            End If

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            '�Ȃ�

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            '�t�H�[�J�X�ݒ�
            BtnClose.Focus()

            '****************************************************************************
            ' ������������ꗗ�f�[�^�擾
            '****************************************************************************
            SetSearchResult(sender, e)

        Else

        End If
    End Sub

    ''' <summary>
    ''' �����������s�f�[�^���e�[�u���ɕ\������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Const CSS_TEXT_CENTER = "textCenter"
        Const CSS_DATE = "date"

        Dim end_count As Integer = 100 '�\���ő匏��
        Dim total_count As Integer = 0 '�擾����

        '�������s
        Dim resultArray As New List(Of SeikyuuDataRecord)
        resultArray = MyLogic.GetSeikyuuMiinsatuData(sender, dtRec, 1, end_count, total_count)

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
        ElseIf total_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        '�\��������������
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")

        If end_count < total_count Then
            resultCount.Style("color") = "red"
            displayCount = end_count & " / " & CommonLogic.Instance.GetDisplayString(total_count)
        End If

        '�������ʌ�����ݒ�
        resultCount.InnerHtml = displayCount

        '************************
        '* ��ʃe�[�u���֏o��
        '************************

        Dim objTr As HtmlTableRow
        Dim objTdSeikyuuSakiCd As HtmlTableCell         '������R�[�h
        Dim objTdSeikyuuSakiMei As HtmlTableCell        '�����於
        Dim objTdSeikyuuSimeDate As HtmlTableCell       '�������ߓ�
        Dim objTdSeikyuusyoHakDate As HtmlTableCell     '���������s��
        Dim objTdSeikyuuSyoYousi As HtmlTableCell       '��������

        Dim rowCnt As Integer = 0 '�J�E���^

        '�������ʂ���Z���Ɋi�[
        For Each dtRec In resultArray

            rowCnt += 1

            '�C���X�^���X��
            objTr = New HtmlTableRow
            objTdSeikyuuSakiCd = New HtmlTableCell
            objTdSeikyuuSakiMei = New HtmlTableCell
            objTdSeikyuuSimeDate = New HtmlTableCell
            objTdSeikyuusyoHakDate = New HtmlTableCell
            objTdSeikyuuSyoYousi = New HtmlTableCell

            '�l�̐ݒ�
            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(dtRec.SeikyuuSakiKbn, dtRec.SeikyuuSakiCd, dtRec.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(dtRec.SeikyuuSakiMei, EarthConst.HANKAKU_SPACE)
            objTdSeikyuuSimeDate.InnerHtml = cl.GetDisplayString(dtRec.SeikyuuSimeDate, EarthConst.HANKAKU_SPACE)
            objTdSeikyuusyoHakDate.InnerHtml = cl.GetDisplayString(dtRec.SeikyuusyoHakDate, EarthConst.HANKAKU_SPACE)
            objTdSeikyuuSyoYousi.InnerHtml = cl.GetDisplayString(dtRec.KaisyuuSeikyuusyoYousiMei, EarthConst.HANKAKU_SPACE)

            '�X�^�C���A�N���X�ݒ�
            objTdSeikyuuSakiCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSimeDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdSeikyuusyoHakDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER

            '�sID��JS�C�x���g�̕t�^
            objTr.ID = CTRL_NAME_TR & rowCnt
            If rowCnt = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If

            '�Z�����s�Ɋi�[
            objTr.Controls.Add(objTdSeikyuuSakiCd)
            objTr.Controls.Add(objTdSeikyuuSakiMei)
            objTr.Controls.Add(objTdSeikyuuSimeDate)
            objTr.Controls.Add(objTdSeikyuusyoHakDate)
            objTr.Controls.Add(objTdSeikyuuSyoYousi)

            '1�s��ǉ�
            Me.searchGrid.Controls.Add(objTr)
        Next

    End Sub

End Class