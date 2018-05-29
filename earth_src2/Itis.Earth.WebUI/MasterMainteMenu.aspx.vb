Imports Itis.Earth.BizLogic

Partial Public Class MasterMainteMenu
    Inherits System.Web.UI.Page

    ''' <summary>�}�X�^�����e�i���X</summary>
    ''' <remarks>�}�X�^�����e�i���X��񋟂���</remarks>
    ''' <history>
    ''' <para>2009/07/15�@�t��(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private commonCheck As New CommonCheck

    ''' <summary>�y�[�W���b�h</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim user_info As New LoginUserInfo
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        ' ���[�U�[��{�F��
        jBn.userAuth(user_info)
        If ninsyou.GetUserID() = String.Empty Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        If user_info Is Nothing Then
            Me.btnWaribikiMaster.Enabled = False
            Me.btnKeiretuMaster.Enabled = False
            btnEigyousyoMaster.Enabled = False
            btnTyousaKaisyaMaster.Enabled = False
            btnSiireMaster.Enabled = False
            btnSeikyuuSakiMaster.Enabled = False
            btnSyouhinMaster.Enabled = False
            btnSyouhinKakakusetteiMaster.Enabled = False
            btnWaribikiMaster.Enabled = False
            btnTokuteitenSyouhin2SetteiMaster.Enabled = False
            btnSeikyuuSakiHenkouMaster.Enabled = False

            btnKairyKojSyubetuMaster.Enabled = False
            btnHanteiKojiSyubetuMaster.Enabled = False
            btnTantousyaMaster.Enabled = False
            btnUserKanri.Enabled = False

            btnKakakuMaster.Enabled = False
            '=========================2011/04/25 �ԗ� �ǉ� �J�n��===============================
            '������X���i�������@���ʑΉ��}�X�^��{�^��
            Me.btnTokubetuTaiou.Enabled = False
            '=========================2011/04/25 �ԗ� �ǉ� �I����===============================
            btnKoujiKakakuMaster.Enabled = False
        Else
            If user_info.Account Is Nothing Then
                Me.btnWaribikiMaster.Enabled = False
                Me.btnKeiretuMaster.Enabled = False
                btnEigyousyoMaster.Enabled = False
                btnTyousaKaisyaMaster.Enabled = False
                btnSiireMaster.Enabled = False
                btnSeikyuuSakiMaster.Enabled = False
                btnSyouhinMaster.Enabled = False
                btnSyouhinKakakusetteiMaster.Enabled = False
                btnWaribikiMaster.Enabled = False
                btnTokuteitenSyouhin2SetteiMaster.Enabled = False
                btnSeikyuuSakiHenkouMaster.Enabled = False

                btnKairyKojSyubetuMaster.Enabled = False
                btnHanteiKojiSyubetuMaster.Enabled = False
                btnTantousyaMaster.Enabled = False
                btnUserKanri.Enabled = False

                btnKakakuMaster.Enabled = False
                '=========================2011/04/25 �ԗ� �ǉ� �J�n��===============================
                '������X���i�������@���ʑΉ��}�X�^��{�^��
                Me.btnTokubetuTaiou.Enabled = False
                '=========================2011/04/25 �ԗ� �ǉ� �I����===============================
                btnKoujiKakakuMaster.Enabled = False
            Else
                Me.btnWaribikiMaster.Enabled = True
                Me.btnKeiretuMaster.Enabled = True
                btnEigyousyoMaster.Enabled = True
                btnTyousaKaisyaMaster.Enabled = True
                btnSiireMaster.Enabled = True
                btnSeikyuuSakiMaster.Enabled = True
                btnSyouhinMaster.Enabled = True
                btnSyouhinKakakusetteiMaster.Enabled = True
                btnWaribikiMaster.Enabled = True
                btnTokuteitenSyouhin2SetteiMaster.Enabled = True
                btnSeikyuuSakiHenkouMaster.Enabled = True

                btnKairyKojSyubetuMaster.Enabled = True
                btnHanteiKojiSyubetuMaster.Enabled = True
                btnTantousyaMaster.Enabled = True
                btnUserKanri.Enabled = True

                btnKakakuMaster.Enabled = True
                '=========================2011/04/25 �ԗ� �ǉ� �J�n��===============================
                '������X���i�������@���ʑΉ��}�X�^��{�^��
                Me.btnTokubetuTaiou.Enabled = True
                '=========================2011/04/25 �ԗ� �ǉ� �I����===============================
                btnKoujiKakakuMaster.Enabled = True
            End If
        End If
        

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, ninsyou.GetUserID())
        End If
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")

        '=========================2011/04/25 �ԗ� �ǉ� �J�n��===============================
        '��̔����i�}�X�^��{�^��
        Me.btnKakakuMaster.Attributes.Add("onClick", "ShowPopup('HanbaiKakakuMasterSearchList.aspx');return false;")
        '������}�X�^��{�^��
        Me.btnSiireMaster.Attributes.Add("onClick", "ShowPopup('GenkaMasterSearchList.aspx');return false;")
        '������X���i�������@���ʑΉ��}�X�^��{�^��
        Me.btnTokubetuTaiou.Attributes.Add("onClick", "ShowPopup('TokubetuTaiouMasterSearchList.aspx');return false;")
        '=========================2011/04/25 �ԗ� �ǉ� �I����===============================

        Me.btnKoujiKakakuMaster.Attributes.Add("onClick", "ShowPopup('KoujiKakakuMasterSearchList.aspx');return false;")

    End Sub

    ''' <summary>���[�U�[�Ǘ��{�^�����N���b�N��</summary>
    Private Sub btnUserKanri_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserKanri.Click
        Server.Transfer("KanrisyaMenuInquiryInput.aspx")
    End Sub

    ''' <summary>���������}�X�^�{�^�����N���b�N��</summary>
    Private Sub btnWaribikiMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWaribikiMaster.Click
        Server.Transfer("WaribikiMasterSearch.aspx")
    End Sub

    ''' <summary>�߂�{�^�����N���b�N��</summary>
    Private Sub btnModoru_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Response.Redirect(UrlConst.MAIN)
    End Sub

    Protected Sub btnKeiretuMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeiretuMaster.Click
        Server.Transfer("KeiretuMaster.aspx")
    End Sub

    Protected Sub btnSyouhinMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyouhinMaster.Click
        Server.Transfer("SyouhinMaster.aspx")
    End Sub



    Protected Sub btnEigyousyoMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEigyousyoMaster.Click
        Server.Transfer("EigyousyoMaster.aspx")
    End Sub

    Protected Sub btnTyousaKaisyaMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTyousaKaisyaMaster.Click
        Server.Transfer("TyousaKaisyaMaster.aspx")
    End Sub

    '=========================2011/04/25 �ԗ� �ǉ� �폜 �J�n��===============================
    'Protected Sub btnSiireMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSiireMaster.Click
    '    'Server.Transfer("SiireMaster.aspx")
    '    Server.Transfer("GenkaMasterSearchList.aspx")
    'End Sub
    '=========================2011/04/25 �ԗ� �ǉ� �폜 �I����===============================

    Protected Sub btnSeikyuuSakiMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiMaster.Click
        Server.Transfer("SeikyuuSakiMaster.aspx")
    End Sub

    Protected Sub btnSyouhinKakakusetteiMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyouhinKakakusetteiMaster.Click
        Server.Transfer("SyouhinKakakusetteiMaster.aspx")
    End Sub

    Protected Sub btnTokuteitenSyouhin2SetteiMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTokuteitenSyouhin2SetteiMaster.Click
        Server.Transfer("Syouhin2Master.aspx")
    End Sub

    Protected Sub btnSeikyuuSakiHenkouMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiHenkouMaster.Click
        Server.Transfer("SeikyuuSakiHenkouMaster.aspx")
    End Sub

    Protected Sub btnKairyKojSyubetuMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKairyKojSyubetuMaster.Click
        Server.Transfer("KairyKojSyubetuMaster.aspx")
    End Sub

    Protected Sub btnHanteiKojiSyubetuMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanteiKojiSyubetuMaster.Click
        Server.Transfer("HanteiKojiSyubetuMaster.aspx")
    End Sub

    Protected Sub btnTantousyaMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTantousyaMaster.Click
        Server.Transfer("TantouSyaMaster.aspx")
    End Sub


    '=========================2011/04/25 �ԗ� �ǉ� �폜 �J�n��===============================
    '''' <summary>
    '''' ���i�}�X�^�{�^�������������
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub btnKakakuMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKakakuMaster.Click
    '    Server.Transfer("HanbaiKakakuMasterSearchList.aspx")
    'End Sub
    '=========================2011/04/25 �ԗ� �폜 �I����===============================

End Class