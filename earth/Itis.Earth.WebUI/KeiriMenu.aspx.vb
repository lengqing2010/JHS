Partial Public Class KeiriMenu
    Inherits System.Web.UI.Page

    '���O�C�����[�U���R�[�h
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

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
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ
            Response.Redirect(UrlConst.MAIN)
            Me.BtnStyle()
            Exit Sub
        End If

        If IsPostBack = False Then

            '�������̃`�F�b�N
            '�o���Ɩ�����
            If userinfo.KeiriGyoumuKengen = 0 Then
                Response.Redirect(UrlConst.MAIN)
                Me.BtnStyle()
                Exit Sub
            End If
            '�t�H�[�J�X�ݒ�
            Me.BtnTeibetuSyuusei.Focus()
        End If

        '�@�ʃf�[�^�C���{�^��
        BtnTeibetuSyuusei.Attributes("onclick") = _
            "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_SYUUSEI & "','teibetu',event.shiftKey==false,'','');return false;"
        '�X�ʃf�[�^�C���{�^��
        BtnTenbetuSyuusei.Attributes("onclick") = _
            "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','tenbetu',event.shiftKey==false,'','1','');return false;"
        '�@�ʓ����C���{�^��
        BtnTeibetuNyuukinSyuusei.Attributes("onclick") = _
            "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_NYUUKIN_SYUUSEI & "','teinyuu',event.shiftKey==false,'','');return false;"

    End Sub

#Region "�{�^������������"

    ''' <summary>����v��^���z�����쐬������</summary>
    Private Sub BtnUriageSiireSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUriageSiireSakusei.Click
        Response.Redirect(UrlConst.URIAGE_SIIRE_SAKUSEI)
    End Sub

    ''' <summary>����`�[�Ɖ�^�������ύX�{�^��������</summary>
    Private Sub BtnSearchUriageData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchUriageData.Click
        Response.Redirect(UrlConst.SEARCH_URIAGE_DATA)
    End Sub

    ''' <summary>�����f�[�^�ꊇ�C���{�^��������</summary>
    Private Sub BtnGetujiIkkatuSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGetujiIkkatuSyuusei.Click
        Response.Redirect(UrlConst.GETUJI_IKKATU_SYUUSEI)
    End Sub

    ''' <summary>�d���`�[�Ɖ�{�^��������</summary>
    Private Sub BtnSearchSiireData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchSiireData.Click
        Response.Redirect(UrlConst.SEARCH_SIIRE_DATA)
    End Sub

    ''' <summary>�����`�[�Ɖ�{�^��������</summary>
    Private Sub BtnSearchNyuukinData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchNyuukinData.Click
        Response.Redirect(UrlConst.SEARCH_NYUUKIN_DATA)
    End Sub

    ''' <summary>�����捞�����{�^��������</summary>
    Private Sub BtnNyuukinSyori_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNyuukinSyori.Click
        Response.Redirect(UrlConst.NYUUKIN_SYORI)
    End Sub

    ''' <summary>�������f�[�^�쐬�^�o�̓{�^��������</summary>
    Private Sub BtnSeikyuusyoDataSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeikyuusyoDataSakusei.Click
        Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
    End Sub

    ''' <summary>�x���`�[�Ɖ�{�^��������</summary>
    Private Sub BtnSearchSiharaiData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchSiharaiData.Click
        Response.Redirect(UrlConst.SEARCH_SIHARAI_DATA)
    End Sub

    ''' <summary>�����捞�f�[�^�Ɖ�^�o�^�^�C���{�^��������</summary>
    Private Sub BtnSearchNyuukinTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchNyuukinTorikomi.Click
        Response.Redirect(UrlConst.SEARCH_NYUUKIN_TORIKOMI)
    End Sub

    ''' <summary>�����挳���{�^��������</summary>
    Private Sub BtnSeikyuuSakiMototyou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeikyuuSakiMototyou.Click
        Response.Redirect(UrlConst.SEIKYUU_SAKI_MOTOTYOU)
    End Sub

    ''' <summary>�ėp����f�[�^�Ɖ�^�o�^�^�C���{�^��������</summary>
    Private Sub BtnSearchHannyouUriage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchHannyouUriage.Click
        Response.Redirect(UrlConst.SEARCH_HANNYOU_URIAGE)
    End Sub

    ''' <summary>�����N�����ꊇ�ύX�{�^��������</summary>
    Private Sub BtnSeikyuuDateIkkatuHenkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeikyuuDateIkkatuHenkou.Click
        Response.Redirect(UrlConst.SEIKYUU_DATE_IKKATU_HENKOU)
    End Sub

    ''' <summary>�x���挳���{�^��������</summary>
    Private Sub BtnSiharaiSakiMototyou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSiharaiSakiMototyou.Click
        Response.Redirect(UrlConst.SIHARAI_SAKI_MOTOTYOU)
    End Sub

    ''' <summary>�ėp�d���f�[�^�Ɖ�^�o�^�^�C���{�^��������</summary>
    Private Sub BtnSearchHannyouSiire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchHannyouSiire.Click
        Response.Redirect(UrlConst.SEARCH_HANNYOU_SIIRE)
    End Sub

    ''' <summary>�@�ʐ�����E�d����ꊇ�ύX�{�^��������</summary>
    Private Sub BtnTeibetuSiireIkkatuHenkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTeibetuSiireIkkatuHenkou.Click
        Response.Redirect(UrlConst.SEIKYUU_SIIRE_SAKI_HENKOU)
    End Sub

    ''' <summary>�e��}�X�^�^�f�[�^�o�̓{�^��������</summary>
    Private Sub BtnKakusyuDataSyuturyoku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKakusyuDataSyuturyoku.Click
        Response.Redirect(UrlConst.EARTH2_KAKUSYU_DATA_SYUTURYOKU_MENU)
    End Sub

    ''' <summary>�߂�{�^��������</summary>
    Private Sub BtnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModoru.Click
        Response.Redirect(UrlConst.MAIN)
    End Sub

#End Region


    ''' <summary>
    ''' �{�^����\������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnStyle()

        Me.BtnTeibetuSyuusei.Attributes("style") = "display:none"
        Me.BtnTeibetuSyuusei.Attributes.Remove("onclick")
        Me.BtnUriageSiireSakusei.Attributes("style") = "display:none"
        Me.BtnSearchUriageData.Attributes("style") = "display:none"
        Me.BtnTenbetuSyuusei.Attributes("style") = "display:none"
        Me.BtnTenbetuSyuusei.Attributes.Remove("onclick")
        Me.BtnGetujiIkkatuSyuusei.Attributes("style") = "display:none"
        Me.BtnSearchSiireData.Attributes("style") = "display:none"
        Me.BtnTeibetuNyuukinSyuusei.Attributes("style") = "display:none"
        Me.BtnTeibetuNyuukinSyuusei.Attributes.Remove("onclick")
        Me.BtnSearchNyuukinData.Attributes("style") = "display:none"
        Me.BtnNyuukinSyori.Attributes("style") = "display:none"
        Me.BtnSeikyuusyoDataSakusei.Attributes("style") = "display:none"
        Me.BtnSearchSiharaiData.Attributes("style") = "display:none"
        Me.BtnSearchNyuukinTorikomi.Attributes("style") = "display:none"
        Me.BtnSeikyuuSakiMototyou.Attributes("style") = "display:none"
        Me.BtnSearchHannyouUriage.Attributes("style") = "display:none"
        Me.BtnSeikyuuDateIkkatuHenkou.Attributes("style") = "display:none"
        Me.BtnSiharaiSakiMototyou.Attributes("style") = "display:none"
        Me.BtnSearchHannyouSiire.Attributes("style") = "display:none"
        Me.BtnTeibetuSiireIkkatuHenkou.Attributes("style") = "display:none"
        Me.BtnKakusyuDataSyuturyoku.Attributes("style") = "display:none"

    End Sub
End Class