
Partial Public Class TeibetuSyuusei
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' �˗����e���[�U�[�R���g���[���p�w�b�_
    ''' </summary>
    ''' <remarks></remarks>
    Protected USR_CTRL_IRAI_NAIYOU As String

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim user_info As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic
    Dim jLogic As New JibanLogic
    Dim cbLogic As New CommonBizLogic
    Dim tLogic As New TokubetuTaiouLogic

#Region "�R���g���[���^�C�v"
    Enum CtrlTypes
        ''' <summary>
        ''' ���i���׃R���g���[��
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinCtrl = 1
        ''' <summary>
        ''' ���i�Q�R���g���[��
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2Ctrl = 2
        ''' <summary>
        ''' ���i�R�R���g���[��
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin3Ctrl = 3
        ''' <summary>
        ''' �H���R���g���[��
        ''' </summary>
        ''' <remarks></remarks>
        KoujiCtrl = 4
    End Enum
#End Region

#Region "�v���p�e�B"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _kbn As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property Kbn() As String
        Get
            Return _kbn
        End Get
        Set(ByVal value As String)
            _kbn = value
        End Set
    End Property

    ''' <summary>
    ''' �ԍ�(�ۏ؏�No)
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _no As String
    ''' <summary>
    ''' �ԍ�(�ۏ؏�No)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property No() As String
        Get
            Return _no
        End Get
        Set(ByVal value As String)
            _no = value
        End Set
    End Property
#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X

        '�˗����e���[�U�[�R���g���[���p�w�b�_�̎擾
        USR_CTRL_IRAI_NAIYOU = Me.IraiNaiyou.ClientID & Me.ClientIDSeparator.ToString

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

            ' Key����ێ�
            _kbn = Request("kbn")
            _no = Request("no")

            ' �p�����[�^�s�����͉�ʂ�\�����Ȃ�
            If _kbn Is Nothing Or _
               _no Is Nothing Then
                Me.ButtonTouroku1.Style("display") = "none"
                Me.ButtonTouroku2.Style("display") = "none"
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            Else
                If jLogic.ExistsJibanData(_kbn, _no) = False Then
                    cl.CloseWindow(Me)
                    Me.ButtonTouroku1.Style("display") = "none"
                    Me.ButtonTouroku2.Style("display") = "none"
                    Response.Redirect(UrlConst.MAIN)
                    Exit Sub
                End If

            End If

            ' ��\�����ڂɐݒ�
            HiddenKubun.Value = _kbn
            HiddenBangou.Value = _no

            '�����L���Ɣ�����z�̐������`�F�b�N�t���O
            HiddenSeikyuuUmuCheck.Value = String.Empty
            '�ύX�ӏ��`�F�b�N�t���O
            HiddenChgValChk.Value = String.Empty

            ' �n�Ճf�[�^����ʂɐݒ肷��
            SetJibanData()

            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            setDispAction()

        Else
            '���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
            '�ύX����Ă���ꍇ���̍Ď擾
            setSeikyuuSiireHenkou(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' �o�^/�C�� ���s�{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTourokuExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTourokuExe.ServerClick

        ' ���̓`�F�b�N
        If Not CheckInput() Then
            Exit Sub
        End If

        ' ��ʂ̓��e��DB�ɔ��f����
        SaveData()

    End Sub

#Region "�ō����z�ύX���̃C�x���g�Q"
    ''' <summary>
    ''' ���i�P�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin1(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin1Record01.ChangeZeikomiGaku

        ' ���i�Q�̐ō����z���擾
        Dim syouhin2Zeikomi As Integer = CtrlTeibetuSyouhin2.ZeikomiKingaku

        ' ��񕥖߂��̐ō����z���擾
        Dim kaiyakuZeikomi As Integer = HosyouKaiyaku.ZeikomiKingaku

        ' ���i�P�̎c�z�Đݒ�
        NyuukinZangakuCtrlSyouhin1.CalcZangaku(zeikomigaku + syouhin2Zeikomi + kaiyakuZeikomi)
        NyuukinZangakuCtrlSyouhin1.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' ���i�Q�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin2(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles CtrlTeibetuSyouhin2.ChangeZeikomiGaku

        ' ���i�P�̐ō����z���擾
        Dim syouhin1Zeikomi As Integer = Syouhin1Record01.ZeikomiKingaku

        ' ��񕥖߂��̐ō����z���擾
        Dim kaiyakuZeikomi As Integer = HosyouKaiyaku.ZeikomiKingaku

        ' ���i�P�̎c�z�Đݒ�
        NyuukinZangakuCtrlSyouhin1.CalcZangaku(zeikomigaku + syouhin1Zeikomi + kaiyakuZeikomi)
        NyuukinZangakuCtrlSyouhin1.UpdateZangakuPanel.Update()

    End Sub

    ''' <summary>
    ''' ��񕥖ߐō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiKaiyaku(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles HosyouKaiyaku.ChangeZeikomiGaku

        ' ���i�P�̐ō����z���擾
        Dim syouhin1Zeikomi As Integer = Syouhin1Record01.ZeikomiKingaku

        ' ���i�Q�̐ō����z���擾
        Dim syouhin2Zeikomi As Integer = CtrlTeibetuSyouhin2.ZeikomiKingaku

        ' ���i�P�̎c�z�Đݒ�
        NyuukinZangakuCtrlSyouhin1.CalcZangaku(zeikomigaku + syouhin1Zeikomi + syouhin2Zeikomi)
        NyuukinZangakuCtrlSyouhin1.UpdateZangakuPanel.Update()

    End Sub

    ''' <summary>
    ''' ���i�R�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin3(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles CtrlTeibetuSyouhin3.ChangeZeikomiGaku

        ' ���i�R�̎c�z�Đݒ�
        NyuukinZangakuCtrlSyouhin3.CalcZangaku(zeikomigaku)
        NyuukinZangakuCtrlSyouhin3.UpdateZangakuPanel.Update()

    End Sub

    ''' <summary>
    ''' ���ǍH���ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiKairyouKouji(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Kairyoukouji.ChangeZeikomiGaku

        ' ���ǍH���̎c�z�Đݒ�
        Kairyoukouji.ZanGaku.CalcZangaku(zeikomigaku)
        Kairyoukouji.ZanGaku.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' �ǉ��H���ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiTuikaKouji(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Tuikakouji.ChangeZeikomiGaku

        ' �ǉ��H���̎c�z�Đݒ�
        Tuikakouji.ZanGaku.CalcZangaku(zeikomigaku)
        Tuikakouji.ZanGaku.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' �����񍐏��ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiTyousaHoukokusyo(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles HoukokusyoTyousa.ChangeZeikomiGaku

        ' �����񍐏��̎c�z�Đݒ�
        NyuukinZangakuCtrlHoukokusyoTyousa.CalcZangaku(zeikomigaku)
        NyuukinZangakuCtrlHoukokusyoTyousa.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' �H���񍐏��ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiKoujiHoukokusyo(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles HoukokusyoKouji.ChangeZeikomiGaku

        ' �H���񍐏��̎c�z�Đݒ�
        NyuukinZangakuCtrlHoukokusyoKouji.CalcZangaku(zeikomigaku)
        NyuukinZangakuCtrlHoukokusyoKouji.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' �ۏ؏��ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiKoujiHosyousyo(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles HosyouHosyousyo.ChangeZeikomiGaku

        ' �ۏ؏��̎c�z�Đݒ�
        NyuukinZangakuCtrlHosyou.CalcZangaku(zeikomigaku)
        NyuukinZangakuCtrlHosyou.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' �˗��R���g���[����蓊�����锭�����z�v���C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GetSyouhin1HattyuuKingaku(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles IraiNaiyou.GetHattyuuKingaku

        ' ���i�P�̔��������z���˗��R���g���[���Ƀg�X����
        IraiNaiyou.Syouhin1HattyuuKingaku = Syouhin1Record01.HattyuusyoKingaku
    End Sub

#End Region

    ''' <summary>
    ''' �y�[�W���[�h�R���v���[�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '�H�����i�ɂ��ẮA�˗����e���[�U�[�R���g���[���̃��[�h���ł͏��i���擾�ł��Ȃ��ׁA
        '���̃^�C�~���O�ŃZ�b�g����
        SetSeikyuuSiireInfo(Me.Kairyoukouji.ClientID)
        SetSeikyuuSiireInfo(Me.Tuikakouji.ClientID)

        '���ʑΉ��{�^��
        ChgDispTokubetuTaiou()
        Me.UpdatePanelTokubetuTaiou.Update()

    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' �n�Ճf�[�^����ʂɐݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJibanData()

        Dim logic As New JibanLogic
        Dim teibetuLogic As New TeibetuSyuuseiLogic
        Dim record As New JibanRecord
        Dim tmpSyouhin1Cd As String = String.Empty

        ' �ēǂݍ��ݗp
        If _kbn = "" Or _kbn Is Nothing Then
            _kbn = HiddenKubun.Value
        End If
        If _no = "" Or _no Is Nothing Then
            _no = HiddenBangou.Value
        End If

        ' �n�Ճf�[�^���擾����
        record = logic.GetJibanData(_kbn, _no)

        ' ��\�����ڂ̐ݒ�
        '�n�Ճe�[�u��.�X�V�҂��烍�O�C�����[�UID�A�X�V�������擾
        cl.SetKousinsya(record.Kousinsya, TextSaisyuuKousinsya.Value, TextSaisyuuKousinDateTime.Value)
        '�X�V����
        HiddenUpdDatetime.Value = IIf(record.UpdDatetime = Date.MinValue, _
                                      record.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2), _
                                      record.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2))

        ' ���ʏ��R���g���[���փf�[�^��ݒ�
        Kyoutuu.Kubun = record.Kbn
        Kyoutuu.Bangou = record.HosyousyoNo
        Kyoutuu.Bikou1 = record.Bikou
        Kyoutuu.Bikou2 = record.Bikou2
        Kyoutuu.Sesyumei = record.SesyuMei
        Kyoutuu.Jyuusyo1 = record.BukkenJyuusyo1
        Kyoutuu.Jyuusyo2 = record.BukkenJyuusyo2
        Kyoutuu.Jyuusyo3 = record.BukkenJyuusyo3
        Kyoutuu.TyousaJissibi = record.TysJissiDate.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Kyoutuu.KairyouKoujiJissibi = record.KairyKojDate
        Kyoutuu.KairyouKoujiKankou = record.KairyKojSokuhouTykDate
        Kyoutuu.TuikaKoujiJissibi = record.TKojDate
        Kyoutuu.TuikaKoujiKankou = record.TKojSokuhouTykDate
        Kyoutuu.KaisekiTantouCd = record.TantousyaCd
        Kyoutuu.KaisekiTantouMei = record.TantousyaMei
        Kyoutuu.KoujiTantouCd = record.SyouninsyaCd
        Kyoutuu.KoujiTantouMei = record.SyouninsyaMei
        Kyoutuu.HanteiCd1 = record.HanteiCd1
        Kyoutuu.HanteiCd2 = record.HanteiCd2
        Kyoutuu.HanteiSetuzokuMoji = record.HanteiSetuzokuMoji
        ' �o���Ɩ�������ݒ�
        Kyoutuu.KeiriGyoumuKengen = user_info.KeiriGyoumuKengen

        ' �˗����e�R���g���[���փf�[�^��ݒ�
        IraiNaiyou.Kbn = record.Kbn
        IraiNaiyou.KameitenCd = record.KameitenCd
        IraiNaiyou.TysKaisyaCd = record.TysKaisyaCd
        IraiNaiyou.TysKaisyaJigyousyoCd = record.TysKaisyaJigyousyoCd
        IraiNaiyou.DoujiIraiTousuu = record.DoujiIraiTousuu
        IraiNaiyou.TatemonoYoutoNo = record.TatemonoYoutoNo
        IraiNaiyou.TysHouhou = cl.GetDisplayString(record.TysHouhou)
        IraiNaiyou.TysGaiyou = IIf(record.TysGaiyou = 0, 9, record.TysGaiyou)
        IraiNaiyou.SyouhinKbn = record.SyouhinKbn
        ' �o���Ɩ�������ݒ�
        IraiNaiyou.KeiriGyoumuKengen = user_info.KeiriGyoumuKengen
        'ReportIF�i���X�e�[�^�X
        IraiNaiyou.StatusIfCd = record.StatusIf
        If EarthConst.Instance.IF_STATUS.ContainsKey(IraiNaiyou.StatusIfCd) Then
            IraiNaiyou.StatusIfName = EarthConst.Instance.IF_STATUS(IraiNaiyou.StatusIfCd)
        Else
            IraiNaiyou.StatusIfName = IraiNaiyou.StatusIfCd
        End If

        '���i1���R�[�h������ꍇ�̂ݐݒ�
        If record.Syouhin1Record IsNot Nothing Then

            '�˗����e�E���i1(DDL��ݒ�)
            IraiNaiyou.Syouhin1 = record.Syouhin1Record.SyouhinCd
            IraiNaiyou.Syouhin1Mei = record.Syouhin1Record.SyouhinMei

            '���ʑΉ��{�^���p���i1
            tmpSyouhin1Cd = cl.GetDisplayString(record.Syouhin1Record.SyouhinCd)

        End If

        '�����ʑΉ��{�^���p�Ƀf�t�H���g�̉����X�E���i�R�[�h�E�������@���Z�b�g
        If String.IsNullOrEmpty(Me.HiddenKakuteiValuesTokubetu.Value) Then
            Me.HiddenKakuteiValuesTokubetu.Value = cl.GetDisplayString(record.KameitenCd) & EarthConst.SEP_STRING _
                                                 & tmpSyouhin1Cd & EarthConst.SEP_STRING _
                                                 & cl.GetDisplayString(record.TysHouhou) & EarthConst.SEP_STRING
        End If

        Dim settingInfoRec As New TeibetuSettingInfoRecord
        settingInfoRec.Kubun = record.Kbn
        settingInfoRec.Bangou = record.HosyousyoNo
        settingInfoRec.UpdLoginUserId = user_info.LoginUserId
        settingInfoRec.KeiriGyoumuKengen = _
            IIf(user_info.KeiriGyoumuKengen = Integer.MinValue, 0, user_info.KeiriGyoumuKengen)
        settingInfoRec.HattyuusyoKanriKengen = _
            IIf(user_info.HattyuusyoKanriKengen = Integer.MinValue, 0, user_info.HattyuusyoKanriKengen)

        ' ��񕥖߂��ԋ��t���O
        If record.HenkinSyoriFlg = 1 Then
            LabelKaiyakuMessage.Text = EarthConst.HENKIN_ZUMI
        End If

        '*******************************************************************
        ' ���i�P
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      Syouhin1Record01, _
                      record.Syouhin1Record, _
                      settingInfoRec, _
                      True, _
                      False, _
                      "100", _
                      NyuukinZangakuCtrlSyouhin1, _
                      record.getZeikomiGaku(New String() {"100", "110", "180"}), _
                      record.getNyuukinGaku("100"))

        '*******************************************************************
        ' ���i�Q
        '*******************************************************************
        ' ���ʐݒ���
        CtrlTeibetuSyouhin2.SettingInfo = settingInfoRec

        ' ���i�Q���R�[�h�փf�[�^��ݒ�
        CtrlTeibetuSyouhin2.Syouhin2Records = record.Syouhin2Records

        '*******************************************************************
        ' ���i�R
        '*******************************************************************
        ' ���ʐݒ���
        CtrlTeibetuSyouhin3.SettingInfo = settingInfoRec

        ' ���i�R���R�[�h�փf�[�^��ݒ�
        CtrlTeibetuSyouhin3.Syouhin3Records = record.Syouhin3Records

        ' �c�z���Z�b�g
        NyuukinZangakuCtrlSyouhin3.CalcZangaku(record.getZeikomiGaku(New String() {"120"}), _
                                               record.getNyuukinGaku("120"))

        '*******************************************************************
        ' ���i�P�`�R ���ʑΉ��c�[���`�b�v
        '*******************************************************************
        Me.GetTokubetuTaiouCd()

        '*******************************************************************
        ' ���ǍH��
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.KoujiCtrl, _
                      Kairyoukouji, _
                      record.KairyouKoujiRecord, _
                      settingInfoRec, _
                      False, _
                      False, _
                      "130", _
                      Kairyoukouji.ZanGaku, _
                      record.getZeikomiGaku(New String() {"130"}), _
                      record.getNyuukinGaku("130"))

        ' �����X�R�[�h
        Kairyoukouji.KameitenCd = record.KameitenCd
        ' �H����А����L��
        Kairyoukouji.KoujiKaisyaSeikyuuUmu = record.KojGaisyaSeikyuuUmu

        ' �H����ЃR�[�h
        If record.KojGaisyaCd Is Nothing Then
            Kairyoukouji.KoujiKaisyaCd = ""
        Else
            Kairyoukouji.KoujiKaisyaCd = record.KojGaisyaCd
        End If

        ' �H����Ў��Ə��R�[�h
        If record.KojGaisyaJigyousyoCd Is Nothing Then
            Kairyoukouji.KoujiKaisyaJigyousyoCd = ""
        Else
            Kairyoukouji.KoujiKaisyaJigyousyoCd = record.KojGaisyaJigyousyoCd
        End If

        '*******************************************************************
        ' �ǉ��H��
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.KoujiCtrl, _
                      Tuikakouji, _
                      record.TuikaKoujiRecord, _
                      settingInfoRec, _
                      False, _
                      False, _
                      "140", _
                      Tuikakouji.ZanGaku, _
                      record.getZeikomiGaku(New String() {"140"}), _
                      record.getNyuukinGaku("140"))

        ' �����X�R�[�h
        Tuikakouji.KameitenCd = record.KameitenCd
        ' �ǉ��H����А����L��
        Tuikakouji.KoujiKaisyaSeikyuuUmu = record.TKojKaisyaSeikyuuUmu

        ' �ǉ��H����ЃR�[�h
        If record.TKojKaisyaCd Is Nothing Then
            Tuikakouji.KoujiKaisyaCd = ""
        Else
            Tuikakouji.KoujiKaisyaCd = record.TKojKaisyaCd
        End If

        ' �ǉ��H����Ў��Ə��R�[�h
        If record.TKojKaisyaJigyousyoCd Is Nothing Then
            Tuikakouji.KoujiKaisyaJigyousyoCd = ""
        Else
            Tuikakouji.KoujiKaisyaJigyousyoCd = record.TKojKaisyaJigyousyoCd
        End If

        '*******************************************************************
        ' �����񍐏�
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      HoukokusyoTyousa, _
                      record.TyousaHoukokusyoRecord, _
                      settingInfoRec, _
                      True, _
                      True, _
                      "150", _
                      NyuukinZangakuCtrlHoukokusyoTyousa, _
                      record.getZeikomiGaku(New String() {"150"}), _
                      record.getNyuukinGaku("150"))

        '*******************************************************************
        ' �H���񍐏�
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      HoukokusyoKouji, _
                      record.KoujiHoukokusyoRecord, _
                      settingInfoRec, _
                      True, _
                      True, _
                      "160", _
                      NyuukinZangakuCtrlHoukokusyoKouji, _
                      record.getZeikomiGaku(New String() {"160"}), _
                      record.getNyuukinGaku("160"))

        '*******************************************************************
        ' �ۏ؏�
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      HosyouHosyousyo, _
                      record.HosyousyoRecord, _
                      settingInfoRec, _
                      True, _
                      True, _
                      "170", _
                      NyuukinZangakuCtrlHosyou, _
                      record.getZeikomiGaku(New String() {"170"}), _
                      record.getNyuukinGaku("170"))

        '*******************************************************************
        ' ��񕥖�
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      HosyouKaiyaku, _
                      record.KaiyakuHaraimodosiRecord, _
                      settingInfoRec, _
                      True, _
                      True, _
                      "180")

        '********************************************************************************
        ' ����f�[�^���݃`�F�b�N�p ����f�[�^�e�[�u���ɗL���ȓ`�[�f�[�^�������Ȃ����X�g
        '********************************************************************************
        Dim denpyouNGList As String
        denpyouNGList = teibetuLogic.GetTeibetuSeikyuuDenpyouHakkouZumiUriageData(record.Kbn, record.HosyousyoNo)
        HiddenDenpyouNGList.Value = denpyouNGList

    End Sub

    ''' <summary>
    ''' �@�ʃf�[�^���e���׃R���g���[���ɐݒ肵�܂�
    ''' </summary>
    ''' <param name="ctrlType">�R���g���[�����</param>
    ''' <param name="ctrl">�ݒ�Ώۂ̊e�햾�׃R���g���[��</param>
    ''' <param name="record">�n�Ճ��R�[�h</param>
    ''' <param name="settingInfoRec">���ʐݒ���</param>
    ''' <param name="recEnabled">���R�[�h�����̏ꍇ�ɃR���g���[����񊈐�������ꍇtrue</param>
    ''' <param name="seikyuuUmu">�����L����񊈐������菜�O</param>
    ''' <param name="bunruiCd">���ރR�[�h�i�񍐏��E�ۏ؏��j</param>
    ''' <param name="zangakuCtrl">�c�z�ݒ�p�R���g���[���i�ݒ莞�̂ݎw��j</param>
    ''' <param name="zeikomigaku">�c�z�v�Z�p�̐ō����z</param>
    ''' <param name="nyuukingaku">�c�z�v�Z�p�̓����z</param>
    ''' <remarks></remarks>
    Private Sub SetTeibetuRec(ByVal ctrlType As CtrlTypes, _
                              ByVal ctrl As Object, _
                              ByVal record As TeibetuSeikyuuRecord, _
                              ByVal settingInfoRec As TeibetuSettingInfoRecord, _
                              Optional ByVal recEnabled As Boolean = False, _
                              Optional ByVal seikyuuUmu As Boolean = False, _
                              Optional ByVal bunruiCd As String = "", _
                              Optional ByVal zangakuCtrl As NyuukinZangakuCtrl = Nothing, _
                              Optional ByVal zeikomigaku As Integer = Integer.MinValue, _
                              Optional ByVal nyuukingaku As Integer = Integer.MinValue)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetTeibetuRec", _
                                                    ctrlType, _
                                                    ctrl, _
                                                    record, _
                                                    settingInfoRec, _
                                                    recEnabled, _
                                                    seikyuuUmu, _
                                                    bunruiCd, _
                                                    zangakuCtrl, _
                                                    zeikomigaku, _
                                                    nyuukingaku)

        ' �R���g���[���̎�ޕʂɐݒ���s��
        Select Case ctrlType
            Case CtrlTypes.SyouhinCtrl

                ' ���i���R�[�h�C���X�^���X�ɐݒ�
                Dim syouhinCtrl As TeibetuSyouhinRecordCtrl = ctrl

                ' ���ʐݒ���
                syouhinCtrl.SettingInfo = settingInfoRec

                ' �f�[�^�����̏ꍇ�ɃR���g���[����񊈐�������ꍇ
                If recEnabled = True Then
                    If record Is Nothing Then
                        syouhinCtrl.Enabled = False

                        If seikyuuUmu = True Then
                            ' �����L����������
                            syouhinCtrl.EnableSeikyuuUmu = True
                            ' �񍐏��E�ۏ؏��p�ɕ��ރR�[�h��ݒ�
                            syouhinCtrl.BunruiCd = bunruiCd
                        End If
                    End If
                End If

                ' ���R�[�h�փf�[�^��ݒ�
                syouhinCtrl.TeibetuSeikyuuRec = record

            Case CtrlTypes.KoujiCtrl

                ' �H���R���g���[���C���X�^���X�ɐݒ�
                Dim koujiCtrl As TeibetuKoujiRecordCtrl = ctrl

                ' ���ʐݒ���
                koujiCtrl.SettingInfo = settingInfoRec

                ' �H�����R�[�h�փf�[�^��ݒ�
                koujiCtrl.TeibetuSeikyuuRec = record

        End Select

        If Not zangakuCtrl Is Nothing Then
            ' �c�z���Z�b�g
            zangakuCtrl.CalcZangaku(zeikomigaku, nyuukingaku)
        End If

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '�o�^/�C�����s�{�^���������̃C�x���g�n���h��
        Dim tmpScript = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);objEBI('" & ButtonTourokuExe.ClientID & "').click();}else{return false;}"
        ButtonTouroku1.Attributes("onclick") = tmpScript
        ButtonTouroku2.Attributes("onclick") = tmpScript

        '���i4
        cl.getSyouhin4MasterPath(ButtonSyouhin4, _
                                 user_info, _
                                 Me.Kyoutuu.AccKubun.ClientID, _
                                 Me.Kyoutuu.AccBangou.ClientID, _
                                 Me.IraiNaiyou.KameitenCdBox.ClientID, _
                                 Me.IraiNaiyou.TyousaKaishaCdBox.ClientID)

        '���ʑΉ�
        cl.getTokubetuTaiouLinkPathJT(Me.ButtonTokubetuTaiou, _
                                    user_info, _
                                    Me.Kyoutuu.AccKubun.ClientID, _
                                    Me.Kyoutuu.AccBangou.ClientID, _
                                    Me.IraiNaiyou.KameitenCdBox.ClientID, _
                                    Me.IraiNaiyou.AccTysHouhou.ClientID, _
                                    Me.IraiNaiyou.AccSelectSyouhin1.ClientID, _
                                    Me.HiddenKakuteiValuesTokubetu.ClientID, _
                                    Me.ButtonTokubetuTaiou.ClientID, _
                                    EarthEnum.emTokubetuTaiouSearchType.TeibetuSyuusei)

    End Sub

    ''' <summary>
    ''' �˗��R���g���[���Ŏ擾�������i�P�����ݒ�������i�P�R���g���[���ɔ��f����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="syouhinRec"></param>
    ''' <remarks></remarks>
    Private Sub SetSyouhin1Data(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                ByVal syouhinRec As Syouhin1AutoSetRecord) Handles IraiNaiyou.Syouhin1SetAction

        Syouhin1Record01.AutoSetSyouhinRecord = syouhinRec ' ���i���R�[�h
        Syouhin1Record01.UpdateSyouhinPanel.Update()

    End Sub

    ''' <summary>
    ''' ���[�U�[�R���g���[���Őݒ肵��������E�d���������ʂ̉B�����ڂɔ��f����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSiireInfo(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String) Handles IraiNaiyou.SetSeikyuuSiireSakiAction _
                                                                    , Syouhin1Record01.SetSeikyuuSiireSakiAction _
                                                                    , CtrlTeibetuSyouhin2.SetSeikyuuSiireSakiAction _
                                                                    , CtrlTeibetuSyouhin3.SetSeikyuuSiireSakiAction _
                                                                    , Kairyoukouji.SetSeikyuuSiireSakiAction _
                                                                    , Tuikakouji.SetSeikyuuSiireSakiAction _
                                                                    , HoukokusyoTyousa.SetSeikyuuSiireSakiAction _
                                                                    , HoukokusyoKouji.SetSeikyuuSiireSakiAction _
                                                                    , HosyouHosyousyo.SetSeikyuuSiireSakiAction _
                                                                    , HosyouKaiyaku.SetSeikyuuSiireSakiAction
        SetSeikyuuSiireInfo(strId)
    End Sub

    ''' <summary>
    ''' ���[�U�[�R���g���[���Őݒ肵���H�����i�}�X�^������ʂ̉B�����ڂɔ��f����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Private Sub GetKojMInfoAction(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String) Handles IraiNaiyou.GetKojMInfoAction _
                                                                    , Kairyoukouji.GetKojMInfoAction _
                                                                    , Tuikakouji.GetKojMInfoAction

        Me.SetKojMInfo(strId)
    End Sub

    ''' <summary>
    ''' ��ʂ̉B�����ڂ��g���A�H�����i���擾����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Private Sub SetKojMInfoAction(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String) Handles IraiNaiyou.SetKojMInfoAction

        Me.SetKojKakaku(strId)
    End Sub

    ''' <summary>
    ''' �˗��E���i�R���g���[�����猴���E�̔����i�}�X�^�ւ̃`�F�b�N���s���i�q�p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId"></param>
    ''' <remarks></remarks>
    Public Sub CheckGenkaHanbaiKkkMaster(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs, _
                                 ByVal strId As String) Handles IraiNaiyou.CheckGenkaHanbaiKkkMasterAction

        If IsPostBack = True Then
            Dim lgcJiban As New JibanLogic
            Dim strAlertMes As String = String.Empty
            Dim blnGetGenkaFlg As Boolean = False
            Dim blnGetHanbaiFlg As Boolean = False
            Dim hin1InfoRec As New Syouhin1InfoRecord
            Dim hin1AutoSetRecord As New Syouhin1AutoSetRecord

            '���i1�������ꍇ�̓`�F�b�N���Ȃ�
            If Syouhin1Record01.TeibetuSeikyuuRec Is Nothing Then
                Exit Sub
            End If

            '�����}�X�^�ւ̎擾
            blnGetGenkaFlg = lgcJiban.GetSyoudakusyoKingaku1(Syouhin1Record01.TeibetuSeikyuuRec.SyouhinCd, _
                                                             Kyoutuu.Kubun, _
                                                             IraiNaiyou.TysHouhou, _
                                                             IraiNaiyou.TysGaiyou, _
                                                             IraiNaiyou.DoujiIraiTousuu, _
                                                             IraiNaiyou.TyousaKaishaCdBox.Value, _
                                                             IraiNaiyou.KameitenCd, _
                                                             0, _
                                                             IraiNaiyou.KeiretuCd, _
                                                             False)

            '��ʍ��ڂ̐ݒ�
            hin1InfoRec.SyouhinCd = Syouhin1Record01.TeibetuSeikyuuRec.SyouhinCd
            hin1InfoRec.TysKaisyaCd = IraiNaiyou.TyousaKaishaCdBox.Value
            hin1InfoRec.TyousaHouhouNo = IraiNaiyou.TysHouhou
            hin1InfoRec.KameitenCd = IraiNaiyou.KameitenCd
            hin1InfoRec.EigyousyoCd = IraiNaiyou.EigyousyoCd
            hin1InfoRec.KeiretuCd = IraiNaiyou.KeiretuCd

            '�̔����i�}�X�^�ւ̎擾
            blnGetHanbaiFlg = lgcJiban.GetHanbaiKingaku1(hin1InfoRec, hin1AutoSetRecord)

            '�}�X�^�l�擾�s�̃p�^�[���ɂ���ĕ\�����e��؂�ւ���
            If blnGetGenkaFlg = False And blnGetHanbaiFlg = False Then
                strAlertMes += Messages.MSG180E
                strAlertMes += Messages.MSG182E
            ElseIf blnGetGenkaFlg = False Then
                strAlertMes += Messages.MSG180E
            ElseIf blnGetHanbaiFlg = False Then
                strAlertMes += Messages.MSG182E
            End If
            '������ЃR�[�h���ݒ�ς݂̏ꍇ�̂�
            If IraiNaiyou.TyousaKaishaCdBox.Value <> String.Empty AndAlso strAlertMes <> String.Empty Then
                '���b�Z�[�W�\��
                MLogic.AlertMessage(sender, strAlertMes, 0, "GetKakakuError")
                '������Ђ����ɖ߂�
                IraiNaiyou.ReturnTyousakaisyaCdNm(sender, e)
            End If

        End If

    End Sub

    ''' <summary>
    ''' �˗��R���g���[���Őݒ肵���H�����i�}�X�^������ʂ̉B�����ڂɔ��f����
    ''' </summary>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Private Sub SetKojMInfo(Optional ByVal strId As String = "")
        '�H�����i�}�X�^�擾�p�̉�ʏ��
        Dim keyRec As KoujiKakakuKeyRecord = New KoujiKakakuKeyRecord
        With keyRec
            .KameitenCd = Me.IraiNaiyou.KameitenCd
            .KeiretuCd = Me.IraiNaiyou.KeiretuCd
            .EigyousyoCd = Me.IraiNaiyou.EigyousyoCd
        End With

        Select Case True
            Case strId.IndexOf(Me.IraiNaiyou.ClientID) >= 0 '�˗����eCTRL�ύX��
                '���ǍH��
                Me.Kairyoukouji.SetKojKkkMstInfo(keyRec)

                '�ǉ��H��
                Me.Tuikakouji.SetKojKkkMstInfo(keyRec)

            Case strId.IndexOf(Me.Kairyoukouji.ClientID) >= 0 '���ǍH��CTRL�ύX��
                '���ǍH��
                Me.Kairyoukouji.SetKojKkkMstInfo(keyRec)

            Case strId.IndexOf(Me.Tuikakouji.ClientID) >= 0 '�ǉ��H��CTRL�ύX��
                '�ǉ��H��
                Me.Tuikakouji.SetKojKkkMstInfo(keyRec)

        End Select

    End Sub

    ''' <summary>
    ''' �@�ʍH�����R�[�h�R���g���[���̍H�����i�擾�������Ăяo��
    ''' </summary>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Private Sub SetKojKakaku(Optional ByVal strId As String = "")

        If strId.IndexOf(Me.IraiNaiyou.ClientID) >= 0 Then '�˗����eCTRL�ύX��
            '���ǍH��
            If Kairyoukouji.SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.KameitenCd) = False Then
                '���H���ȊO�̏ꍇ�͉��i�擾���s�Ȃ�Ȃ�
            End If

            '�ǉ��H��
            If Tuikakouji.SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.KameitenCd) = False Then
                '���H���ȊO�̏ꍇ�͉��i�擾���s�Ȃ�Ȃ�
            End If

        End If

    End Sub

    ''' <summary>
    ''' �˗��R���g���[���Őݒ肵��������E�d���������ʂ̉B�����ڂɔ��f����
    ''' </summary>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSiireInfo(Optional ByVal strId As String = "")
        '������E�d����ύX��ʗp���
        Dim strKameitenCd As String = Me.IraiNaiyou.KameitenCd
        Dim strKeiretuCd As String = Me.IraiNaiyou.KeiretuCd
        Dim strTysKaisyaCd As String = Me.IraiNaiyou.TyousaKaishaCdBox.Value

        Select Case True
            Case strId.IndexOf(Me.IraiNaiyou.ClientID) >= 0, strId = String.Empty
                '�˗����e�ύX��
                '***** �S���i�̃��R�[�h�։����XCD�ƒ������CD�̏����Z�b�g *****
                '���i�P
                Me.Syouhin1Record01.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '���i�Q
                Me.CtrlTeibetuSyouhin2.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '���i�R
                Me.CtrlTeibetuSyouhin3.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '���ǍH��
                Me.Kairyoukouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd)
                '�ǉ��H��
                Me.Tuikakouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd)
                '�񍐏�(����)
                Me.HoukokusyoTyousa.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '�񍐏�(�H��)
                Me.HoukokusyoKouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '�ۏ�
                Me.HosyouHosyousyo.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '��񕥖�
                Me.HosyouKaiyaku.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.Syouhin1Record01.ClientID) >= 0
                '���i1�ύX��
                Me.Syouhin1Record01.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.CtrlTeibetuSyouhin2.ClientID) >= 0
                '���i2�ύX��
                Me.CtrlTeibetuSyouhin2.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd, strId)

            Case strId.IndexOf(Me.CtrlTeibetuSyouhin3.ClientID) >= 0
                '���i3�ύX��
                Me.CtrlTeibetuSyouhin3.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd, strId)

            Case strId.IndexOf(Me.Kairyoukouji.ClientID) >= 0
                '���ǍH���ύX��
                Me.Kairyoukouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd)

            Case strId.IndexOf(Me.Tuikakouji.ClientID) >= 0
                '�ǉ��H���ύX��
                Me.Tuikakouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd)

            Case strId.IndexOf(Me.HoukokusyoTyousa.ClientID) >= 0
                '�񍐏�(����)�ύX��
                Me.HoukokusyoTyousa.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.HoukokusyoKouji.ClientID) >= 0
                '�񍐏�(�H��)�ύX��
                Me.HoukokusyoKouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.HosyouHosyousyo.ClientID) >= 0
                '�ۏؕύX��
                Me.HosyouHosyousyo.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.HosyouKaiyaku.ClientID) >= 0
                '��񕥖ߕύX��
                Me.HosyouKaiyaku.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
        End Select


    End Sub

    ''' <summary>
    ''' ���i1�̐�������擾����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GetSeikyuuSakiInfoSyouhin1(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles IraiNaiyou.GetSyouhin1SeikyuuSakiInfo
        Dim strSeikyuuSakiCd As String
        Dim strSeikyuuSakibrc As String
        Dim strSeikyuuSakikbn As String

        strSeikyuuSakiCd = Syouhin1Record01.AccSeikyuuSiireLink.AccSeikyuuSakiCd.Value
        strSeikyuuSakibrc = Syouhin1Record01.AccSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
        strSeikyuuSakikbn = Syouhin1Record01.AccSeikyuuSiireLink.AccSeikyuuSakiKbn.Value

        IraiNaiyou.Syouhin1SeikyuuSakiCd = strSeikyuuSakiCd
        IraiNaiyou.Syouhin1SeikyuuSakiBrc = strSeikyuuSakibrc
        IraiNaiyou.Syouhin1SeikyuuSakiKbn = strSeikyuuSakikbn

    End Sub

    ''' <summary>
    ''' �����^�C�v�̐ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">�J�X�^���C�x���g������ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">������^�C�v(���ڐ���/������)</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuType(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByVal strId As String _
                                        , ByVal strSeikyuuSakiTypeStr As String _
                                        , ByVal strKeiretuCd As String _
                                        , ByVal strKameitenCd As String) Handles IraiNaiyou.SetSeikyuuTypeAction _
                                                                                , Syouhin1Record01.SetSeikyuuTypeAction _
                                                                                , CtrlTeibetuSyouhin2.SetSeikyuuTypeAction _
                                                                                , CtrlTeibetuSyouhin3.SetSeikyuuTypeAction _
                                                                                , HoukokusyoTyousa.SetSeikyuuTypeAction _
                                                                                , HoukokusyoKouji.SetSeikyuuTypeAction _
                                                                                , HosyouHosyousyo.SetSeikyuuTypeAction _
                                                                                , HosyouKaiyaku.SetSeikyuuTypeAction

        '�����^�C�v
        Dim enSeikyuuType As EarthEnum.EnumSeikyuuType
        Dim lgcJiban As New JibanLogic

        If strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TYOKUSETU Then
            ' ���ڐ���
            If lgcJiban.GetKeiretuFlg(strKeiretuCd) = 1 Then
                ' �n��
                enSeikyuuType = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu
            Else
                ' �n��ȊO
                enSeikyuuType = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
            End If
        Else
            ' ������
            If lgcJiban.GetKeiretuFlg(strKeiretuCd) = 1 Then
                ' �n��
                enSeikyuuType = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu
            Else
                ' �n��ȊO
                enSeikyuuType = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu
            End If
        End If

        '�J�X�^���C�x���g������ID�ɂ�镪��
        Select Case True
            Case strId.IndexOf(Me.IraiNaiyou.ClientID) >= 0
                '���i1���R�[�h�֐����^�C�v�̐ݒ�
                Me.Syouhin1Record01.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , EarthConst.Instance.ARRAY_SHOUHIN_LINES(0))
                '���i2���R�[�h�֐����^�C�v�̐ݒ�
                Me.CtrlTeibetuSyouhin2.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)
                '���i3���R�[�h�֐����^�C�v�̐ݒ�
                Me.CtrlTeibetuSyouhin3.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

                '�񍐏��������R�[�h�֐����^�C�v�̐ݒ�
                Me.HoukokusyoTyousa.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

                '�񍐏��H�����R�[�h�֐����^�C�v�̐ݒ�
                Me.HoukokusyoKouji.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

                '�ۏ؃��R�[�h�֐����^�C�v�̐ݒ�
                Me.HosyouHosyousyo.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

                '��񕥖߃��R�[�h�֐����^�C�v�̐ݒ�
                Me.HosyouKaiyaku.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

            Case strId.IndexOf(Me.Syouhin1Record01.ClientID) >= 0
                '���i1���R�[�h�֐����^�C�v�̐ݒ�
                Me.Syouhin1Record01.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , EarthConst.Instance.ARRAY_SHOUHIN_LINES(0))
            Case strId.IndexOf(Me.CtrlTeibetuSyouhin2.ClientID) >= 0
                '���i2���R�[�h�֐����^�C�v�̐ݒ�
                Me.CtrlTeibetuSyouhin2.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.CtrlTeibetuSyouhin3.ClientID) >= 0
                '���i3���R�[�h�֐����^�C�v�̐ݒ�
                Me.CtrlTeibetuSyouhin3.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.HoukokusyoTyousa.ClientID) >= 0
                '�񍐏��������R�[�h�֐����^�C�v�̐ݒ�
                Me.HoukokusyoTyousa.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.HoukokusyoKouji.ClientID) >= 0
                '�񍐏��H�����R�[�h�֐����^�C�v�̐ݒ�
                Me.HoukokusyoKouji.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.HosyouHosyousyo.ClientID) >= 0
                '�ۏ؃��R�[�h�֐����^�C�v�̐ݒ�
                Me.HosyouHosyousyo.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.HosyouKaiyaku.ClientID) >= 0
                '��񕥖߃��R�[�h�֐����^�C�v�̐ݒ�
                Me.HosyouKaiyaku.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)
        End Select
    End Sub

    ''' <summary>
    ''' �������@�̐ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="TyousaHouhouNo">�������@No</param>
    ''' <remarks></remarks>
    Private Sub SetTysHouhou(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByRef TyousaHouhouNo As Integer) Handles CtrlTeibetuSyouhin3.SetTysHouhouAction
        TyousaHouhouNo = IraiNaiyou.TysHouhou
    End Sub

    ''' <summary>
    '''���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
    '''�ύX����Ă���ꍇ���̍Ď擾
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)
        '*************************************************************
        '* ������A�d���悪�ύX���ꂽ�s���`�F�b�N���A���݂����ꍇ��
        '* �e�s�̐����L���ύX���̏��������s����
        '*************************************************************
        '���i1
        If Me.Syouhin1Record01.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            ' �˗��R���g���[���̏��i�P�ݒ胍�W�b�N�����s���A���ʂ����i�P�ɔ��f�����
            IraiNaiyou.Syouhin1Set(sender, e)
            Me.Syouhin1Record01.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '�ύX���ꂽ���i���L�����ꍇ�A�����I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
            Exit Sub
        End If
        '���i2
        If CtrlTeibetuSyouhin2.setSeikyuuSiireHenkou(sender, e) Then
            '�ύX���ꂽ���i���L�����ꍇ�A�����I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
            Exit Sub
        End If
        '���i3
        If CtrlTeibetuSyouhin3.setSeikyuuSiireHenkou(sender, e) Then
            '�ύX���ꂽ���i���L�����ꍇ�A�����I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
            Exit Sub
        End If
        '�񍐏�(����)
        If Me.HoukokusyoTyousa.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            Me.HoukokusyoKouji.DispMode = TeibetuSyouhinRecordCtrl.DisplayMode.HOUKOKUSYO
            Me.HoukokusyoTyousa.SetSyouhinEtc(sender, e, True)
            Me.HoukokusyoTyousa.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '�ύX���ꂽ���i���L�����ꍇ�A�����I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
            Exit Sub
        End If
        '�񍐏�(�H��)
        If Me.HoukokusyoKouji.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            Me.HoukokusyoKouji.DispMode = TeibetuSyouhinRecordCtrl.DisplayMode.HOUKOKUSYO
            Me.HoukokusyoKouji.SetSyouhinEtc(sender, e, True)
            Me.HoukokusyoKouji.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '�ύX���ꂽ���i���L�����ꍇ�A�����I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
            Exit Sub
        End If
        '�ۏ�
        If Me.HosyouHosyousyo.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            Me.HosyouHosyousyo.DispMode = TeibetuSyouhinRecordCtrl.DisplayMode.HOSYOU
            Me.HosyouHosyousyo.SetSyouhinEtc(sender, e, True)
            Me.HosyouHosyousyo.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '�ύX���ꂽ���i���L�����ꍇ�A�����I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
            Exit Sub
        End If
        '��񕥖�
        If Me.HosyouKaiyaku.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            Me.HosyouKaiyaku.DispMode = TeibetuSyouhinRecordCtrl.DisplayMode.KAIYAKU
            Me.HosyouKaiyaku.SetSyouhinEtc(sender, e, True)
            Me.HosyouKaiyaku.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '�ύX���ꂽ���i���L�����ꍇ�A�����I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' ���i�P�̎������z�A�H���X�����z���˗���ʂփZ�b�g����<br/>
    ''' ���i�P�R���g���[���ŕύX�����^�C�~���O�Ŗ{���\�b�h���Ă΂�܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="jituSeikyuuGaku">���������z</param>
    ''' <param name="koumutenSeikyuuGaku">�H���X�����z</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuKingaku(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs, _
                                  ByVal jituSeikyuuGaku As String, _
                                  ByVal koumutenSeikyuuGaku As String) Handles Syouhin1Record01.KingakuSetAction

        IraiNaiyou.Syouhin1JituSeikyuuGaku = jituSeikyuuGaku           ' �������z
        IraiNaiyou.Syouhin1KoumutenSeikyuuGaku = koumutenSeikyuuGaku   ' �H���X�����z
        IraiNaiyou.UpdateSyouhin1Seikyuu.Update()

    End Sub

    ''' <summary>
    ''' �˗��R���g���[���̏��i�P�ݒ胍�W�b�N�����s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecKakakuSettei(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin1Record01.ExecKakakuSettei

        ' �˗��R���g���[���̏��i�P�ݒ胍�W�b�N�����s���A���ʂ����i�P�ɔ��f�����
        IraiNaiyou.Syouhin1Set(Me, e)

    End Sub

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveData()

        '�������b�Z�[�W���A�������w��|�b�v�A�b�v�\���̂��߂̃t���O���N���A
        callModalFlg.Value = String.Empty

        ' ���݂̒n�Ճf�[�^��DB����擾����
        Dim jibanLogic As New JibanLogic
        Dim jibanRecOld As New JibanRecord
        jibanRecOld = jibanLogic.GetJibanData(HiddenKubun.Value, HiddenBangou.Value)

        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        Dim jibanRec As New JibanRecordTeibetuSyuusei

        '�i��T�X�V�p�ɁADB��̒l���Z�b�g����
        jibanLogic.SetSintyokuJibanData(jibanRecOld, jibanRec)

        ' �@�ʃf�[�^�C���p�̃��W�b�N�N���X
        Dim logic As New TeibetuSyuuseiLogic

        '***************************************
        ' �n�Ճf�[�^
        '***************************************
        ' �敪
        jibanRec.Kbn = HiddenKubun.Value
        ' �ԍ��i�ۏ؏�NO�j
        jibanRec.HosyousyoNo = HiddenBangou.Value
        ' �X�V�҃��[�U�[ID
        jibanRec.UpdLoginUserId = user_info.LoginUserId
        ' �����X�R�[�h
        jibanRec.KameitenCd = IraiNaiyou.KameitenCd
        ' ���i�敪
        jibanRec.SyouhinKbn = IraiNaiyou.SyouhinKbn
        ' ���l
        jibanRec.Bikou = Kyoutuu.Bikou1
        ' ���l2
        jibanRec.Bikou2 = Kyoutuu.Bikou2
        ' ������к���
        jibanRec.TysKaisyaCd = IraiNaiyou.TysKaisyaCd
        ' ������Ў��Ə�����
        jibanRec.TysKaisyaJigyousyoCd = IraiNaiyou.TysKaisyaJigyousyoCd
        ' �����p�r
        jibanRec.TatemonoYoutoNo = IraiNaiyou.TatemonoYoutoNo
        ' �������@
        jibanRec.TysHouhou = IraiNaiyou.TysHouhou
        ' �����T�v
        jibanRec.TysGaiyou = IraiNaiyou.TysGaiyou
        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
        jibanRec.UpdDatetime = Date.Parse(HiddenUpdDatetime.Value)
        ' �����˗�����
        jibanRec.DoujiIraiTousuu = IraiNaiyou.DoujiIraiTousuu
        '************************
        ' �H�����
        '************************
        ' �H����к���
        jibanRec.KojGaisyaCd = Kairyoukouji.KoujiKaisyaCd
        ' �H����Ў��Ə�����
        jibanRec.KojGaisyaJigyousyoCd = Kairyoukouji.KoujiKaisyaJigyousyoCd
        ' �H����А����L��
        jibanRec.KojGaisyaSeikyuuUmu = Kairyoukouji.KoujiKaisyaSeikyuuUmu
        '************************
        ' �ǉ��H�����
        '************************
        ' �ǉ��H����к���
        jibanRec.TKojKaisyaCd = Tuikakouji.KoujiKaisyaCd
        ' �ǉ��H����Ў��Ə�����
        jibanRec.TKojKaisyaJigyousyoCd = Tuikakouji.KoujiKaisyaJigyousyoCd
        ' �ǉ��H����А����L��
        jibanRec.TKojKaisyaSeikyuuUmu = Tuikakouji.KoujiKaisyaSeikyuuUmu

        '***************************************
        ' �@�ʐ����f�[�^
        '***************************************

        ' ���i�P�̓@�ʐ����f�[�^���Z�b�g���܂�
        jibanRec.Syouhin1Record = Syouhin1Record01.TeibetuSeikyuuRec

        ' ���i�Q�̓@�ʐ����f�[�^���Z�b�g���܂�
        If Not CtrlTeibetuSyouhin2.Syouhin2Records Is Nothing Then
            jibanRec.Syouhin2Records = CtrlTeibetuSyouhin2.Syouhin2Records
        End If

        ' ���i�R�̓@�ʐ����f�[�^���Z�b�g���܂�
        If Not CtrlTeibetuSyouhin3.Syouhin3Records Is Nothing Then
            jibanRec.Syouhin3Records = CtrlTeibetuSyouhin3.Syouhin3Records
        End If

        ' ���ǍH���̓@�ʐ����f�[�^���Z�b�g���܂�
        jibanRec.KairyouKoujiRecord = Kairyoukouji.TeibetuSeikyuuRec

        ' �ǉ��H���̓@�ʐ����f�[�^���Z�b�g���܂�
        jibanRec.TuikaKoujiRecord = Tuikakouji.TeibetuSeikyuuRec

        ' �����񍐏��̓@�ʐ����f�[�^���Z�b�g���܂�
        jibanRec.TyousaHoukokusyoRecord = HoukokusyoTyousa.TeibetuSeikyuuRec

        ' �H���񍐏��̓@�ʐ����f�[�^���Z�b�g���܂�
        jibanRec.KoujiHoukokusyoRecord = HoukokusyoKouji.TeibetuSeikyuuRec

        ' �ۏ؏��̓@�ʐ����f�[�^���Z�b�g���܂�
        jibanRec.HosyousyoRecord = HosyouHosyousyo.TeibetuSeikyuuRec

        ' ��񕥖߂̓@�ʐ����f�[�^���Z�b�g���܂�
        jibanRec.KaiyakuHaraimodosiRecord = HosyouKaiyaku.TeibetuSeikyuuRec

        '�X�V��
        jibanRec.Kousinsya = cbLogic.GetKousinsya(user_info.LoginUserId, DateTime.Now)

        '*********************************************************
        '���ۏ؊֘A�̎����ݒ�
        cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.TeibetuSyuusei, jibanRec)

        '���������f�[�^�̎����Z�b�g
        Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cl.GetDisplayString(jibanRecOld.HosyouSyouhinUmu), cl.GetDisplayString(jibanRecOld.KeikakusyoSakuseiDate))

        If Not brRec Is Nothing Then
            '�����������R�[�h�̃`�F�b�N
            Dim strErrMsg As String = String.Empty
            If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                MLogic.AlertMessage(Me, strErrMsg, 0, "ErrBukkenRireki")
                Exit Sub
            End If
        End If
        '*********************************************************

        '���ʑΉ����i�Ή�
        Dim strTokubetuTaiouCd As String = String.Empty
        Dim strTmp As String

        '���i�P
        If Me.Syouhin1Record01.AccTokubetuTaiouUpdFlg.Value = EarthConst.ARI_VAL Then
            strTokubetuTaiouCd = Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value
        End If

        '���i�Q
        strTmp = Me.CtrlTeibetuSyouhin2.GetCdFromToolTip
        If strTmp <> String.Empty Then
            If strTokubetuTaiouCd = String.Empty Then
                strTokubetuTaiouCd = strTmp
            Else
                strTokubetuTaiouCd &= EarthConst.SEP_STRING & strTmp
            End If
        End If

        '���i�R
        strTmp = Me.CtrlTeibetuSyouhin3.GetCdFromToolTip
        If strTmp <> String.Empty Then
            If strTokubetuTaiouCd = String.Empty Then
                strTokubetuTaiouCd = strTmp
            Else
                strTokubetuTaiouCd &= EarthConst.SEP_STRING & strTmp
            End If
        End If

        Dim sender As New Object
        Dim ttLogic As New TokubetuTaiouLogic
        Dim intTokubetuCnt As Integer = 0
        Dim listRec As New List(Of TokubetuTaiouRecordBase)

        If strTokubetuTaiouCd <> String.Empty Then
            listRec = ttLogic.GetTokubetuTaiouDataInfo(sender, _
                                                         jibanRec.Kbn, _
                                                         jibanRec.HosyousyoNo, _
                                                         strTokubetuTaiouCd, _
                                                         intTokubetuCnt)
        End If

        If intTokubetuCnt <= 0 Then
            listRec = Nothing
        End If

        ' �f�[�^�̍X�V���s���܂�
        If logic.SaveJibanData(Me, jibanRec, brRec, listRec) Then

            ' �ēǂݍ��݂���
            SetJibanData()

            '�������b�Z�[�W���A�������w��|�b�v�A�b�v�\���̂��߂Ƀt���O���Z�b�g
            callModalFlg.Value = Boolean.TrueString

            '�����L���Ɣ�����z�̐������`�F�b�N�t���O
            HiddenSeikyuuUmuCheck.Value = String.Empty
            '�ύX�ӏ��`�F�b�N�t���O
            HiddenChgValChk.Value = String.Empty

        End If

    End Sub

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <returns>�`�F�b�N���� True:OK False:NG</returns>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean

        '�G���[���b�Z�[�W������
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New List(Of Control)
        Dim denpyouErrMess As String = String.Empty
        Dim tmpDenpyouNgList As String = HiddenDenpyouNGList.Value
        Dim seikyuuUmuErrMess As String = String.Empty '�����L���`�F�b�N�p
        Dim strChgPartMess As String = String.Empty '�ύX�ӏ��p

        ' �G���[���b�Z�[�W��ۑ�
        Dim saveErrMess As String = errMess
        Dim saveErrMess2 As String = strChgPartMess '�ύX�ӏ��p

        ' ���ʏ��̃G���[�`�F�b�N
        Kyoutuu.CheckInput(errMess, arrFocusTargetCtrl, "���ʏ��", strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ň˗�����W�J����
            Kyoutuu.KyoutuuInfo.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' �˗����̃G���[�`�F�b�N
        IraiNaiyou.CheckInput(errMess, arrFocusTargetCtrl, "�˗����", strChgPartMess, Syouhin1Record01.SyouhinCd)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ň˗�����W�J����
            IraiNaiyou.IraiTBody.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' ���i�P�̃G���[�`�F�b�N
        Syouhin1Record01.CheckInput(errMess, arrFocusTargetCtrl, "���i�P", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�Q�̃G���[�`�F�b�N
        CtrlTeibetuSyouhin2.CheckInput(errMess, arrFocusTargetCtrl, "���i�Q", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ŏ��i1/2����W�J����
            TBodySyouhin12.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' ���i�R�̃G���[�`�F�b�N
        CtrlTeibetuSyouhin3.CheckInput(errMess, arrFocusTargetCtrl, "���i�R", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ŏ��i3����W�J����
            TBodySyouhin3.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' ���ǍH���̃G���[�`�F�b�N
        Kairyoukouji.CheckInput(errMess, arrFocusTargetCtrl, "���ǍH��", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' �ǉ��H���̃G���[�`�F�b�N
        Tuikakouji.CheckInput(errMess, arrFocusTargetCtrl, "�ǉ��H��", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ŉ��ǍH������W�J����
            TBodyKairyou.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' �����񍐏��̃G���[�`�F�b�N
        HoukokusyoTyousa.CheckInput(errMess, arrFocusTargetCtrl, "�����񍐏�", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' �H���񍐏��̃G���[�`�F�b�N
        HoukokusyoKouji.CheckInput(errMess, arrFocusTargetCtrl, "�H���񍐏�", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ŕ񍐏�����W�J����
            TBodyHoukokusyo.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' �ۏ؏��̃G���[�`�F�b�N
        HosyouHosyousyo.CheckInput(errMess, arrFocusTargetCtrl, "�ۏ؏�", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ��񕥖߂̃G���[�`�F�b�N
        HosyouKaiyaku.CheckInput(errMess, arrFocusTargetCtrl, "��񕥖�", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' �G���[���b�Z�[�W���ǉ����ꂽ�̂ŕۏ؏�����W�J����
            TBodyHosyou.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        '**********************
        '* �e��`�F�b�N����
        '**********************
        '���ύX�ӏ�
        Dim strTmpChgChk As String = strChgPartMess.Replace("\r\n", "") 'JS�ɂĉ��s�R�[�h���ϊ�����邽�ߒu��
        If strChgPartMess <> "" And Me.HiddenChgValChk.Value <> strTmpChgChk Then
            '�t�H�[�J�X�Z�b�g
            ButtonTouroku1.Focus()
            '�G���[���b�Z�[�W�\��
            Dim strMsg As String = Messages.MSG178C.Replace("@PARAM1", strChgPartMess)
            Dim tmpScript = "if(confirm('" & strMsg & "')){document.getElementById('" & HiddenChgValChk.ClientID & "').value='" & strTmpChgChk & "'; autoExeButtonId = '" & ButtonTouroku1.ClientID & "';};"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        '�����̓`�F�b�N�S��
        If errMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        '���`�[�֘A
        If denpyouErrMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            ButtonTouroku1.Focus()
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "if(confirm('" & Messages.MSG152C.Replace("@PARAM1", denpyouErrMess) & "')){document.getElementById('" & HiddenDenpyouNGList.ClientID & "').value=''; autoExeButtonId = '" & ButtonTouroku1.ClientID & "';};"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        '�������L���֘A
        If seikyuuUmuErrMess <> "" And HiddenSeikyuuUmuCheck.Value <> "1" Then
            '�t�H�[�J�X�Z�b�g
            ButtonTouroku1.Focus()
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "if(confirm('" & String.Format(Messages.MSG156C, seikyuuUmuErrMess) & "')){document.getElementById('" & HiddenSeikyuuUmuCheck.ClientID & "').value='1'; autoExeButtonId = '" & ButtonTouroku1.ClientID & "';};"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        Return True
    End Function
#Region "���ʑΉ�"

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���(���ʑΉ�)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringTokubetu() As String
        Dim sb As New StringBuilder

        sb.Append(Me.IraiNaiyou.KameitenCd & EarthConst.SEP_STRING)                         '�����X�R�[�h
        sb.Append(Me.IraiNaiyou.AccSelectSyouhin1.SelectedValue & EarthConst.SEP_STRING)    '���i1�R�[�h
        sb.Append(Me.IraiNaiyou.AccTysHouhou.SelectedValue & EarthConst.SEP_STRING)         '�������@No

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' ���ʑΉ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgDispTokubetuTaiou()

        If Me.HiddenKakuteiValuesTokubetu.Value <> getCtrlValuesStringTokubetu() Then
            '�Ԕw�i
            Me.ButtonTokubetuTaiou.Style(EarthConst.STYLE_BACK_GROUND_COLOR) = EarthConst.STYLE_COLOR_RED
            '����
            Me.ButtonTokubetuTaiou.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
        Else
            '�w�i�F��������
            Me.ButtonTokubetuTaiou.Style(EarthConst.STYLE_BACK_GROUND_COLOR) = ""
            '�m�[�}��
            Me.ButtonTokubetuTaiou.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_NORMAL
        End If

    End Sub

    ''' <summary>
    ''' ���ʑΉ��f�[�^���擾����(�c�[���`�b�v�\���p)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetTokubetuTaiouCd()

        Dim intCnt As Integer = 0
        Dim ttList As New List(Of TokubetuTaiouRecordBase)

        '�敪�A�ۏ؏�NO���L�[�ɓ��ʑΉ��f�[�^���擾
        ttList = tLogic.GetTokubetuTaiouDataInfo(Me, Kbn, No, String.Empty, intCnt)

        '�U��������
        Me.DevideTokubetuTaiouCd(ttList)

    End Sub

    ''' <summary>
    ''' ���ʑΉ��f�[�^�̃��X�g����ʂ̊e�c�[���`�b�v�ɐU�蕪����
    ''' </summary>
    ''' <param name="ttList">���ʑΉ����R�[�h�̃��X�g</param>
    ''' <remarks></remarks>
    Private Sub DevideTokubetuTaiouCd(ByVal ttList As List(Of TokubetuTaiouRecordBase))

        Dim recTmp As New TokubetuTaiouRecordBase       '��Ɨp
        Dim strTokubetuTaiouCd As String = String.Empty '���ʑΉ��R�[�h
        Dim strResult As String                         '�U����
        Dim emType As EarthEnum.emToolTipType           '�c�[���`�b�v�\���^�C�v

        '���ʑΉ��f�[�^�X�V�t���O��������
        Me.Syouhin1Record01.AccTokubetuTaiouUpdFlg.Value = EarthConst.NASI_VAL
        Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty
        Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty

        Me.CtrlTeibetuSyouhin2.ClearTokubetuTaiouInfo()
        Me.CtrlTeibetuSyouhin3.ClearTokubetuTaiouUpdFlg()

        If Not ttList Is Nothing Then
            For Each recTmp In ttList
                '���ʑΉ��R�[�h����Ɨp�Ɏ擾
                strTokubetuTaiouCd = cl.GetDisplayString(recTmp.TokubetuTaiouCd)

                '�U��������擾
                strResult = cbLogic.checkDevideTaisyou(Me, recTmp)

                '�U�������Hidden�ɒǉ�
                If strResult <> String.Empty Then
                    Dim search_shouhin As String = strResult.Split(EarthConst.UNDER_SCORE)(0)   '���i����p
                    Dim strRowNo As String = strResult.Split(EarthConst.UNDER_SCORE)(1)         '

                    '�c�[���`�b�vHidden�ɓ��ʑΉ��R�[�h���i�[
                    If search_shouhin = "1" Then
                        '�c�[���`�b�v�ݒ�Ώۂ��`�F�b�N
                        emType = cbLogic.checkToolTipSetValue(Me, recTmp, Me.Syouhin1Record01.BunruiCd, Me.Syouhin1Record01.GamenHyoujiNo, Me.Syouhin1Record01.AccUriageSyori.SelectedValue)
                        If emType <> EarthEnum.emToolTipType.NASI Then
                            '�\���p
                            Me.Syouhin1Record01.AccTokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)
                            '�o�^�p
                            If Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty Then
                                Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = strTokubetuTaiouCd
                            Else
                                Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value &= EarthConst.SEP_STRING & strTokubetuTaiouCd
                            End If

                            If emType = EarthEnum.emToolTipType.SYUSEI Then
                                Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AcclblTokubetuTaiou.Text = EarthConst.SYUU_TOOL_TIP
                            End If
                        End If

                    ElseIf search_shouhin = "2" Then
                        Me.CtrlTeibetuSyouhin2.SetTokubetuTaiouToolTip(strTokubetuTaiouCd, strRowNo, recTmp)

                    ElseIf search_shouhin = "3" Then
                        Me.CtrlTeibetuSyouhin3.SetTokubetuTaiouToolTip(strTokubetuTaiouCd, strRowNo, recTmp)

                    End If

                End If
            Next
        End If
    End Sub

#End Region

#End Region

End Class