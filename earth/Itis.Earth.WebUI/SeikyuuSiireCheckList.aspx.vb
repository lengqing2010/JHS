
Partial Public Class SeikyuuSiireCheckList
    Inherits System.Web.UI.Page

    Dim user_info As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ���[�U�[��{�F��
        jBn.UserAuth(user_info)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If user_info IsNot Nothing Then

        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        If IsPostBack = False Then

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            ' �敪�R���{�Ƀf�[�^���o�C���h����
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            'helper.SetDropDownList(cboKubun_1, DropDownHelper.DropDownType.Kubun)
            'helper.SetDropDownList(cboKubun_2, DropDownHelper.DropDownType.Kubun)
            'helper.SetDropDownList(cboKubun_3, DropDownHelper.DropDownType.Kubun)

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            'setDispAction()

            '�t�H�[�J�X�ݒ�
            'Me.cmbHosyousho_hak_jyky.Focus()

        Else


        End If

    End Sub

End Class