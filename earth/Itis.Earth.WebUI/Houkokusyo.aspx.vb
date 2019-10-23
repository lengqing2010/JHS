Partial Public Class Houkokusyo
    Inherits System.Web.UI.Page

    Dim iraiSession As New IraiSession
    Dim userInfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    ''' <summary>
    ''' ���b�Z�[�W�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    Dim JibanLogic As New JibanLogic
    Dim kameitenlogic As New KameitenSearchLogic
    Dim MyLogic As New HoukokusyoLogic
    Dim kisoSiyouLogic As New KisoSiyouLogic
    Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
    Dim cbLogic As New CommonBizLogic
    Dim strLogic As New StringLogic

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
    Public Property pStrKbn() As String
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
    Public Property pStrBangou() As String
        Get
            Return _no
        End Get
        Set(ByVal value As String)
            _no = value
        End Set
    End Property
#End Region

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
        jBn.UserAuth(userInfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userInfo Is Nothing Then
            '���O�C����񂪖����ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        '�e�e�[�u���̕\����Ԃ�؂�ւ���
        Me.TBodyHoukokushoInfo.Style("display") = Me.HiddenHoukokusyoInfoStyle.Value

        If IsPostBack = False Then '�����N����

            ' Key����ێ�
            _kbn = Request("sendPage_kubun")
            _no = Request("sendPage_hosyoushoNo")

            ' �p�����[�^�s�����͉�ʂ�\�����Ȃ�
            If _kbn Is Nothing Or _
               _no Is Nothing Then
                Response.Redirect(UrlConst.MAIN)
            End If

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�(�����񍐏�����)
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' �󗝃R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectJuri, DropDownHelper.DropDownType.HkksJuri)

            ' �ʐ^�󗝃R���{�Ƀf�[�^���o�C���h����
            helper.SetMeisyouDropDownList(Me.SelectSyasinJuri, EarthConst.emMeisyouType.SYASIN_JURI)

            ' ��͒S���҃R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKaisekiTantousya, DropDownHelper.DropDownType.Tantousya)

            ' ��b�d�l�ڑ����R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectHanteiSetuzokuMoji, DropDownHelper.DropDownType.KsSiyouSetuzokusi)

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            SetDispAction()

            '�{�^�������C�x���g�̐ݒ�
            setBtnEvent()

            '****************************************************************************
            ' �n�Ճf�[�^�擾
            '****************************************************************************
            Dim jibanRec As New JibanRecordBase
            jibanRec = JibanLogic.GetJibanData(pStrKbn, pStrBangou)
            iraiSession.JibanData = jibanRec

            '�n�Փǂݍ��݃f�[�^���Z�b�V�����ɑ��݂���ꍇ�A��ʂɕ\��������
            If iraiSession.JibanData IsNot Nothing Then
                Dim jr As JibanRecordBase = iraiSession.JibanData
                SetCtrlFromJibanRec(sender, e, jr) '�n�Ճf�[�^���R���g���[���ɃZ�b�g

            End If

            If ButtonTouroku1.Disabled = False Then
                ButtonTouroku1.Focus() '�o�^/�C���{�^���Ƀt�H�[�J�X
            End If

        Else
            '���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
            '�ύX����Ă���ꍇ���̍Ď擾
            setSeikyuuSiireHenkou(sender, e)

        End If

        '�R���e�L�X�g�ɒl���i�[
        Context.Items("irai") = iraiSession

    End Sub

    ''' <summary>
    ''' �y�[�W���[�h�R���v���[�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim strUriageZumi As String = String.Empty  '���㏈���ςݔ��f�t���O�p
        Dim strViewMode As String = String.Empty

        '���㏈���ϔ��f�t���O�̎擾
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanUriageSyorizumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userInfo.KeiriGyoumuKengen)

        If Me.SelectSeikyuuUmu.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        End If

        '������/�d������̃Z�b�g
        Me.ucSeikyuuSiireLink.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.TextItemCd.Text _
                                                                    , Me.HiddenDefaultSiireSakiCdForLink.Value _
                                                                    , strUriageZumi _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)


    End Sub

    ''' <summary>
    ''' Ajax���쎞�̃t�H�[�J�X�Z�b�g
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        '�t�H�[�J�X�Z�b�g
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' �o�^/�C�����s�{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V�̊m�F���s�Ȃ��B<br/>
    ''' OK���FDB�X�V���s�Ȃ��B<br/>
    ''' �L�����Z�����FDB�X�V�𒆒f����B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()
        '�C�x���g�n���h���o�^
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(CheckTouroku()){" & tmpTouroku & "}else{return false;}"

        ButtonTouroku1.Attributes("onclick") = tmpScript
        ButtonTouroku2.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' �����{�^���̕\���ؑ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetButtonSearchDisp()

        ButtonHantei1.Style("display") = "none"
        ButtonHantei2.Style("display") = "none"

    End Sub

    ''' <summary>
    ''' �Ɩ�����[���[�U�[�R���g���[��]���[�h������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ucGyoumuKyoutuu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucGyoumuKyoutuu.Load

        '�ŏI�X�V�ҁA�ŏI�X�V�������Z�b�g
        TextSaisyuuKousinSya.Text = ucGyoumuKyoutuu.AccLastupdateusernm.Value
        TextSaisyuuKousinDate.Text = ucGyoumuKyoutuu.AccLastupdatedatetime.Value

        '****************************
        '* ����������
        '****************************

        '�����N�����̂�
        If IsPostBack = False Then
            '��ʐ���
            SetEnableControl()
        End If

        '�񍐏��Ɩ��������邢�͌��ʋƖ�����
        If userInfo.HoukokusyoGyoumuKengen = 0 And userInfo.KekkaGyoumuKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiSyubetu.ID, ucGyoumuKyoutuu.AccDataHakiSyubetu) '�f�[�^�j�����
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiDate.ID, ucGyoumuKyoutuu.AccDataHakiDate) '�f�[�^�j����
            jSM.Hash2Ctrl(UpdatePanelHoll, EarthConst.MODE_VIEW, ht, htNotTarget)

            SetButtonSearchDisp()

            '�o�^�{�^��
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True
        End If

        '�j������
        If userInfo.DataHakiKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiSyubetu, EarthConst.MODE_VIEW, ht) '�f�[�^�j�����
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiData, EarthConst.MODE_VIEW, ht) '�f�[�^�j����

        End If

        '���ʏ��̓��̓`�F�b�N
        If ucGyoumuKyoutuu.AccKameitenCd.Value = "" Then '�����X�R�[�h
            Dim tmpScript As String = ""

            '�o�^�{�^���̔񊈐���
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True

            tmpScript = "alert('" & Messages.MSG065W & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucGyoumuKyoutuu_Load", tmpScript, True)

            '�n�Չ�ʋ��ʃN���X
            Dim noTarget As New Hashtable
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable

            '�S�ẴR���g���[���𖳌���
            jSM.Hash2Ctrl(UpdatePanelHoll, EarthConst.MODE_VIEW, ht, htNotTarget)
        End If

        '�j����ʃ`�F�b�N
        If ucGyoumuKyoutuu.AccDataHakiSyubetu.SelectedValue <> "0" Then
            '�j����ʂ��ݒ肳��Ă���ꍇ�A���ׂẴR���g���[���𖳌���
            CheckHakiDisable(True)
        End If

        '�f�t�H���g�t�H�[�J�X
        If ButtonTouroku1.Disabled <> True Then
            SetFocus(ButtonTouroku1)
        End If

    End Sub

    ''' <summary>
    ''' �n�Ճ��R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByRef jr As JibanRecordBase)
        Dim jSM As New JibanSessionManager '�Z�b�V�����Ǘ��N���X
        Dim jBn As New Jiban '�n�Չ�ʃN���X
        Dim blnTorikesi As Boolean = False '����t���O(=False)

        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '******************************************
        '* ��ʃR���g���[���ɐݒ�y�����񍐏����z
        '******************************************
        '�����X�R�[�h
        Me.HiddenKameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)

        TextIraiTantousya.Text = cl.GetDispStr(jr.IraiTantousyaMei) '�˗��S����
        If jr.TysKaisyaCd <> "" And jr.TysKaisyaJigyousyoCd <> "" Then
            TextTyousaKaisyaMei.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.TysKaisyaCd, jr.TysKaisyaJigyousyoCd, False) '�������
            HiddenDefaultSiireSakiCdForLink.Value = (jr.TysKaisyaCd + jr.TysKaisyaJigyousyoCd)
        End If

        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.TyousaHouhou, True, False)
        '�������@��DDL�\������
        cl.ps_SetSelectTextBoxTysHouhou(jr.TysHouhou, objDrpTmp, True, Me.TextTyousaHouhou)

        TextTyousaJissiDate.Text = cl.GetDispStr(jr.TysJissiDate) '�������{��
        TextKeikakusyoSakuseiDate.Text = cl.GetDispStr(jr.KeikakusyoSakuseiDate) '�v�揑�쐬��
        SelectJuri.SelectedValue = cl.GetDispNum(jr.TysHkksUmu, "") '��
        TextJuriSyousai.Text = cl.GetDispStr(jr.TysHkksJyuriSyousai) '�󗝏ڍ�
        TextJuriDate.Text = cl.GetDispStr(jr.TysHkksJyuriDate) '�󗝓�
        TextHassouDate.Text = cl.GetDispStr(jr.TysHkksHakDate) '������
        SelectSyasinJuri.SelectedValue = cl.ChgStrToInt(jr.TikanKoujiSyasinJuri) '�u���H���ʐ^��
        TextSyasinComment.Text = cl.GetDispStr(jr.TikanKoujiSyasinComment) '�u���H���ʐ^�R�����g
        TextSaihakkouDate.Text = cl.GetDispStr(jr.TysHkksSaihakDate) '�Ĕ��s��

        '�����񍐏���񂪂���ꍇ
        If Not jr.TyousaHoukokusyoRecord Is Nothing Then

            '�@�ʐ��������R���g���[���ɃZ�b�g
            SetCtrlTeibetuSeikyuuData(jr.TyousaHoukokusyoRecord)

            '�@�ʓ��������R���g���[���ɃZ�b�g
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' �����z/�c�z���Z�b�g
                CalcZangaku(jr.getZeikomiGaku(New String() {"150"}), jr.getNyuukinGaku("150"))
            Else
                ' �����z/�c�z���Z�b�g
                SetKingaku(True)
            End If

        End If

        '�y���茋�ʁz
        '���݃`�F�b�N
        If ChkTantousya(SelectKaisekiTantousya, cl.GetDispNum(jr.TantousyaCd)) Then
            TextKaisekiTantousyaCd.Text = cl.GetDispNum(jr.TantousyaCd, "") '��͒S���҃R�[�h
            SelectKaisekiTantousya.SelectedValue = TextKaisekiTantousyaCd.Text '��͒S����
        ElseIf jr.TantousyaCd > 0 Then
            TextKaisekiTantousyaCd.Text = cl.GetDispNum(jr.TantousyaCd, "") '��͒S���҃R�[�h
            SelectKaisekiTantousya.Items.Add(New ListItem(TextKaisekiTantousyaCd.Text & ":" & jr.TantousyaMei, TextKaisekiTantousyaCd.Text)) '��͒S����
            SelectKaisekiTantousya.SelectedValue = TextKaisekiTantousyaCd.Text  '�I�����
        End If
        TextKaisekiSyouninsya.Text = cl.GetDispStr(jr.TyousaHoukokusyoSyouninsyaIf) '��͏��F��
        TextKoujiTantousyaCd.Text = cl.GetDispNum(jr.SyouninsyaCd, "") '�H���S���҃R�[�h
        TextKoujiTantousya.Text = cl.GetDispStr(jr.SyouninsyaMei) '�H���S����
        TextHantei1Cd.Text = cl.GetDispNum(jr.HanteiCd1, "") '����P�R�[�h
        SpanHantei1.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd1)) '����P��
        SelectHanteiSetuzokuMoji.SelectedValue = jr.HanteiSetuzokuMoji '����ڑ�����
        TextHantei2Cd.Text = cl.GetDispNum(jr.HanteiCd2, "") '����Q�R�[�h
        SpanHantei2.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd2)) '����Q��

        '****************************
        '* Hidden����
        '****************************
        HiddenHosyousyoHakJykyOld.Value = cl.GetDispNum(jr.HosyousyoHakJyky, "") '�ۏ؏����s��Old

        HiddenHosyousyoHakJyky.Value = cl.GetDispNum(jr.HosyousyoHakJyky, "") '�ۏ؏����s��(DB�X�V�p)
        HiddenHosyousyoHakJykySetteiDate.Value = cl.GetDispStr(jr.HosyousyoHakJykySetteiDate) '�ۏ؏����s�󋵐ݒ��(DB�X�V�p)

        HiddenHosyousyoHakkouDate.Value = cl.GetDispStr(jr.HosyousyoHakDate) '�ۏ؏����s��(���̓`�F�b�N�p)

        HiddenHantei1CdOld.Value = cl.GetDispNum(jr.HanteiCd1, "") '����1Old
        HiddenHantei2CdOld.Value = cl.GetDispNum(jr.HanteiCd2, "") '����2Old
        HiddenHanteiSetuzokuMojiOld.Value = cl.GetDispNum(jr.HanteiSetuzokuMoji, "") '����ڑ�����Old
        HiddenHantei1Old.Value = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd1)) '����P��
        HiddenHantei1CdMae.Value = cl.GetDispNum(jr.HanteiCd1, "") '����1�O
        HiddenHantei2CdMae.Value = cl.GetDispNum(jr.HanteiCd2, "") '����2�O

        HiddenJituseikyuu1Flg.Value = "" '�������Ŕ����z�E�����ݒ�p�t���O

        HiddenTyousaKekkaTourokuDate.Value = IIf(jr.TysKekkaAddDatetime = DateTime.MinValue, "", Format(jr.TysKekkaAddDatetime, EarthConst.FORMAT_DATE_TIME_1)) '�������ʓo�^����
        HiddenTyousaKekkaUpdateDate.Value = IIf(jr.TysKekkaUpdDatetime = DateTime.MinValue, "", Format(jr.TysKekkaUpdDatetime, EarthConst.FORMAT_DATE_TIME_1)) '�������ʍX�V����

        Me.HiddenKojHanteiKekkaFlgOld.Value = cl.GetDispNum(jr.KojHanteiKekkaFlg, "0") '�H�����茋��FLGOLD

        '****************************
        '* �Z�b�V�����ɉ�ʏ����i�[
        '****************************
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

    End Sub

#Region "�����z/�c�z"

    ''' <summary>
    ''' �����z�E�c�z��\�����܂�
    ''' </summary>
    ''' <param name="uriageGoukeiGaku">�ō�������z���v</param>
    ''' <param name="nyuukinGaku">�����z</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal uriageGoukeiGaku As Integer, _
                           ByVal nyuukinGaku As Integer)

        ' NULL�͂O�ɂ���
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)
        nyuukinGaku = IIf(nyuukinGaku = Integer.MinValue, 0, nyuukinGaku)

        ' �����z
        TextNyuukingaku.Text = nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' �c�z
        TextZangaku.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

    ''' <summary>
    ''' �����z�E�c�z��\�����܂�<br/>
    ''' �ō�������z���v�̂ݕύX���A�Čv�Z���܂�
    ''' </summary>
    ''' <param name="strUriageGoukeiGaku">�ō�������z���v</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal strUriageGoukeiGaku As String)

        If strUriageGoukeiGaku = "" Then
            ' �c�z
            TextZangaku.Text = "0"

        Else
            Dim uriageGoukeiGaku As Integer = Integer.MinValue

            uriageGoukeiGaku = CInt(strUriageGoukeiGaku)

            Dim strNyuukinGaku As String = IIf(TextNyuukingaku.Text.Replace(",", "").Trim() = "", _
                                            "0", _
                                            TextNyuukingaku.Text.Replace(",", "").Trim())

            Dim nyuukinGaku As Integer = Integer.Parse(strNyuukinGaku)

            ' NULL�͂O�ɂ���
            uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)

            ' �c�z
            TextZangaku.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

        End If

    End Sub

#End Region

#Region "�@�ʐ������R�[�h�ҏW"
    ''' <summary>
    ''' �@�ʐ������R�[�h�f�[�^���R���g���[���ɃZ�b�g���܂�
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuData(ByVal TeibetuRec As TeibetuSeikyuuRecord)

        ' ���i�R�[�h�iHidden�j
        TextItemCd.Text = TeibetuRec.SyouhinCd
        '���i��
        SpanItemMei.InnerHtml = TeibetuRec.SyouhinMei
        '�H���X�����Ŕ����z
        TextKoumutenSeikyuuKingaku.Text = IIf(TeibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, _
                                            0, _
                                            TeibetuRec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' �ŗ��iHidden�j
        HiddenZeiritu.Value = TeibetuRec.Zeiritu
        ' �ŋ敪�iHidden�j
        HiddenZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* �y������z�z
        '*****************
        '���������Ŋz(�����)
        TextSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���Ŕ�������z(�������Ŕ����z)
        TextJituseikyuuKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '���ō�������z(�ō��z)
        TextZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* �y�d�����z�z
        '*****************
        '�d�����z
        Me.HiddenSiireGaku.Value = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '���d������Ŋz
        Me.HiddenSiireSyouhiZei.Value = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))

        ' ���������s��
        TextSeikyuusyoHakkouDate.Text = IIf(TeibetuRec.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          TeibetuRec.SeikyuusyoHakDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' ����N����
        TextUriageNengappi.Text = IIf(TeibetuRec.UriDate = Date.MinValue, _
                                      "", _
                                      TeibetuRec.UriDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' �����L���h���b�v�_�E��
        SelectSeikyuuUmu.SelectedValue = IIf(TeibetuRec.SeikyuuUmu = 1, "1", "0")
        ' ���㏈����
        SpanUriageSyorizumi.InnerHtml = IIf(TeibetuRec.UriKeijyouDate = Date.MinValue, "", EarthConst.URIAGE_ZUMI)
        ' ����v���
        Me.HiddenUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)
        ' ���������z
        TextHattyuusyoKingaku.Text = IIf(TeibetuRec.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         TeibetuRec.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' �������m��
        TextHattyuusyoKakutei.Text = IIf(TeibetuRec.HattyuusyoKakuteiFlg = 1, EarthConst.KAKUTEI, EarthConst.MIKAKUTEI)
        ' �������m�F��
        TextHattyuusyoKakuninDate.Text = IIf(TeibetuRec.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           TeibetuRec.HattyuusyoKakuninDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        '�Ĕ��s���R
        TextSaihakkouRiyuu.Text = TeibetuRec.Bikou

        '������/�d���惊���N�փZ�b�g
        Me.ucSeikyuuSiireLink.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        '������^�C�v�̎擾�ݒ�
        Me.TextSeikyuusaki.Text = cl.GetSeikyuuSakiTypeStr( _
                                                        TeibetuRec.SyouhinCd _
                                                        , EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo _
                                                        , Me.HiddenKameitenCd.Value _
                                                        , TeibetuRec.SeikyuuSakiCd _
                                                        , TeibetuRec.SeikyuuSakiBrc _
                                                        , TeibetuRec.SeikyuuSakiKbn)

        '�X�V����(�r������p)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenUpdateTeibetuSeikyuuDatetime)

    End Sub

    ''' <summary>
    ''' �@�ʐ������R�[�h�f�[�^�ɃR���g���[���̓��e���Z�b�g���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlData() As TeibetuSeikyuuRecord

        ' ���i�R�[�h���ݒ莞�A�����Z�b�g���Ȃ�
        If TextItemCd.Text = "" Then
            Return Nothing
        End If

        ' �@�ʐ������R�[�h
        Dim record As New TeibetuSeikyuuRecord

        ' �敪
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ۏ؏�NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        '���ރR�[�h(150)
        record.BunruiCd = EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO
        '��ʕ\��NO
        record.GamenHyoujiNo = 1
        ' ���i�R�[�h
        record.SyouhinCd = TextItemCd.Text
        '������z
        cl.SetDisplayString(TextJituseikyuuKingaku.Text, record.UriGaku)
        '�d�����z
        cl.SetDisplayString(Me.HiddenSiireGaku.Value, record.SiireGaku)
        ' �ŋ敪
        record.ZeiKbn = HiddenZeiKbn.Value
        ' �ŗ�
        cl.SetDisplayString(HiddenZeiritu.Value, record.Zeiritu)
        ' ����Ŋz
        cl.SetDisplayString(TextSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' �d������Ŋz
        cl.SetDisplayString(Me.HiddenSiireSyouhiZei.Value, record.SiireSyouhiZeiGaku)
        ' ���������s��
        cl.SetDisplayString(TextSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' ����N����
        cl.SetDisplayString(TextUriageNengappi.Text, record.UriDate)
        ' �`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
        record.DenpyouUriDate = DateTime.MinValue
        ' �����L��
        record.SeikyuuUmu = IIf(SelectSeikyuuUmu.SelectedValue = "1", 1, 0)
        '����v��FLG��
        record.UriKeijyouFlg = 0
        '����v�����
        record.UriKeijyouDate = DateTime.MinValue
        '�m��敪
        record.KakuteiKbn = Integer.MinValue
        '���l
        record.Bikou = TextSaihakkouRiyuu.Text
        '�H���X�����Ŕ����z
        cl.SetDisplayString(TextKoumutenSeikyuuKingaku.Text, record.KoumutenSeikyuuGaku)
        ' ���������z
        cl.SetDisplayString(TextHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' �������m�F��
        cl.SetDisplayString(TextHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        '�ꊇ����FLG��
        record.IkkatuNyuukinFlg = Integer.MinValue
        '�������Ϗ��쐬��
        record.TysMitsyoSakuseiDate = DateTime.MinValue
        ' �������m��FLG
        record.HattyuusyoKakuteiFlg = IIf(TextHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '������/�d���惊���N����擾
        Me.ucSeikyuuSiireLink.SetTeibetuRecFromSeikyuuSiireLink(record)

        '�X�V���O�C�����[�UID
        record.UpdLoginUserId = userInfo.LoginUserId
        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetime)

        Return record
    End Function

#End Region

    ''' <summary>
    ''' �o�^/�C�� ���s�{�^���P,�Q�������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTouroku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTouroku1.ServerClick _
                                                                                                                , ButtonTouroku2.ServerClick
        Dim tmpScript As String = ""

        '����ύX���R�`�F�b�N
        If Me.checkInputBukkenRireki(Me.HiddenHanteiHenkouRiyuu.Value, False) = False Then Exit Sub

        '���ʏ��
        If ucGyoumuKyoutuu.checkInput() = False Then Exit Sub

        '�����񍐏����
        If checkInput() = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If SaveData() Then '�o�^����
            tmpScript = "window.close();" '��ʂ����
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick1", tmpScript, True)

        Else '�o�^���s
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "�o�^/�C��") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick2", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ����ύX���R�`�F�b�N
    ''' </summary>
    ''' <param name="strHenkouRiyuu">����ύX���R</param>
    ''' <param name="blnNaiyou">True:�u���e�v�`�F�b�N,False:�u����ύX���R�v�`�F�b�N</param>
    ''' <returns></returns>
    ''' <remarks>����֘A���ύX����Ă���ꍇ�A�`�F�b�N���s�Ȃ�</remarks>
    Public Function checkInputBukkenRireki(ByRef strHenkouRiyuu As String, ByVal blnNaiyou As Boolean) As Boolean
        Dim jBn As New Jiban '�n�Չ�ʃN���X
        Dim strHanteiHenkouRiyuu As String = String.Empty
        Dim errMess As String = String.Empty
        Dim strCtrlName As String = "����ύX���R"
        Dim blnHanteiFlg As Boolean = True

        '����1=���͍ςł�����֘A���ύX����Ă���ꍇ�A�ȉ��̃`�F�b�N���s�Ȃ�
        '����1
        If Me.HiddenHantei1CdOld.Value <> String.Empty Then '���͍�
            '����1
            If Me.HiddenHantei1CdOld.Value <> Me.TextHantei1Cd.Text Then
                blnHanteiFlg = False
            End If
            '����2
            If Me.HiddenHantei2CdOld.Value <> Me.TextHantei2Cd.Text Then
                blnHanteiFlg = False
            End If
            '����ڑ���
            If Me.HiddenHanteiSetuzokuMojiOld.Value <> Me.SelectHanteiSetuzokuMoji.SelectedValue Then
                blnHanteiFlg = False
            End If
        End If

        '����֘A���ύX����Ă���ꍇ�A�ȉ��̃`�F�b�N���s�Ȃ��B
        If blnHanteiFlg = False Then
            ' �o�^�O�`�F�b�N
            strHenkouRiyuu = strHenkouRiyuu.Trim '�󔒏���

            '���K�{�`�F�b�N
            If strHenkouRiyuu = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", strCtrlName)
            End If
            If blnNaiyou Then '���e�`�F�b�N��
                '�֑�������u��
                strLogic.KinsiStrClear(strHenkouRiyuu)
            End If
            '���֑������`�F�b�N(��������̓t�B�[���h���Ώ�)
            If jBn.KinsiStrCheck(strHenkouRiyuu) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", strCtrlName)
            End If
            '���s�ϊ�(��������T.���e)
            If strHenkouRiyuu <> "" Then
                strHenkouRiyuu = strHenkouRiyuu.Replace(vbCrLf, " ")
            End If
            '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
            If blnNaiyou Then '���e�`�F�b�N��
                If jBn.ByteCheckSJIS(strHenkouRiyuu, 512) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", strCtrlName)
                End If
            Else '����ύX���R�`�F�b�N��
                If jBn.ByteCheckSJIS(strHenkouRiyuu, 256) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", strCtrlName)
                End If
            End If

            '�G���[����������
            If errMess <> "" Then
                'Me.HiddenHanteiHenkouRiyuu.Value = "" '������
                strHenkouRiyuu = String.Empty

                '�G���[���b�Z�[�W�\��
                MLogic.AlertMessage(Me, errMess)
                '�t�H�[�J�X�Z�b�g
                setFocusAJ(Me.ButtonTouroku1)
                Return False
                Exit Function
                'Else
                '    Me.HiddenHanteiHenkouRiyuu.Value = strHenkouRiyuu
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N
    ''' </summary>
    ''' <remarks>
    ''' �R�[�h���͒l�ύX�`�F�b�N<br/>
    ''' �K�{�`�F�b�N<br/>
    ''' �֑������`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �����`�F�b�N<br/>
    ''' ���t�`�F�b�N<br/>
    ''' ���̑��`�F�b�N<br/>
    ''' </remarks>
    Public Function checkInput() As Boolean
        Dim e As New System.EventArgs

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�S�ẴR���g���[����L����(�j����ʃ`�F�b�N�p)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        If ucGyoumuKyoutuu.AccDataHakiSyubetu.SelectedValue >= "1" Then
            '�j����ʂ��I������Ă���ꍇ�A�X���[

        Else

            '���R�[�h���͒l�ύX�`�F�b�N
            '����1�R�[�h
            If TextHantei1Cd.Text <> HiddenHantei1CdMae.Value Or (TextHantei1Cd.Text <> "" And SpanHantei1.InnerHtml = "") Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "����1�R�[�h")
                arrFocusTargetCtrl.Add(ButtonHantei1)
            End If
            '����2�R�[�h
            If TextHantei2Cd.Text <> HiddenHantei2CdMae.Value Or (TextHantei2Cd.Text <> "" And SpanHantei2.InnerHtml = "") Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "����2�R�[�h")
                arrFocusTargetCtrl.Add(ButtonHantei2)
            End If

            '���K�{�`�F�b�N
            '<�����񍐏��Ĕ��s>

            '(Chk20:<�񍐏����>�����񍐏��Ĕ��s�������́A���A<�񍐏����><�H���񍐏��Ĕ��s>�����L�����L��̏ꍇ�A�`�F�b�N���s���B)
            '�Ĕ��s��
            If TextSaihakkouDate.Text.Length <> 0 Then
                '�����L��
                If SelectSeikyuuUmu.SelectedValue = "1" Then
                    '�������Ŕ����z
                    If TextJituseikyuuKingaku.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "�������Ŕ����z")
                        arrFocusTargetCtrl.Add(TextJituseikyuuKingaku)
                    End If
                    '���������s��
                    If TextSeikyuusyoHakkouDate.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "���������s��")
                        arrFocusTargetCtrl.Add(TextSeikyuusyoHakkouDate)
                    End If
                End If

                '(Chk19:<�񍐏����>�����񍐏��Ĕ��s�������͂̏ꍇ�A�`�F�b�N���s���B)
                '����
                If SelectSeikyuuUmu.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "����")
                    arrFocusTargetCtrl.Add(SelectSeikyuuUmu)
                End If
                '�Ĕ��s���R
                If TextSaihakkouRiyuu.Text.Length = 0 Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "�Ĕ��s���R")
                    arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
                End If

            End If

        End If

        '�����t�`�F�b�N
        '�f�[�^�j����
        If ucGyoumuKyoutuu.AccDataHakiDate.Value <> "" Then
            If cl.checkDateHanni(ucGyoumuKyoutuu.AccDataHakiDate.Value) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�f�[�^�j����")
                arrFocusTargetCtrl.Add(ucGyoumuKyoutuu.AccDataHakiDate)
            End If
        End If
        '�󗝓�
        If TextJuriDate.Text <> "" Then
            If cl.checkDateHanni(TextJuriDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�󗝓�")
                arrFocusTargetCtrl.Add(TextJuriDate)
            End If
        End If
        '������
        If TextHassouDate.Text <> "" Then
            If cl.checkDateHanni(TextHassouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "������")
                arrFocusTargetCtrl.Add(TextHassouDate)
            End If
        End If
        '�Ĕ��s��
        If TextSaihakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextSaihakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�Ĕ��s��")
                arrFocusTargetCtrl.Add(TextSaihakkouDate)
            End If
        End If
        '****************
        '* �����񍐏��Ĕ��s
        '****************
        '���������s��
        If TextSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���������s��")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkouDate)
            End If
        End If


        '���֑������`�F�b�N(��������̓t�B�[���h���Ώ�)
        '�󗝏ڍ�
        If jBn.KinsiStrCheck(TextJuriSyousai.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�󗝏ڍ�")
            arrFocusTargetCtrl.Add(TextJuriSyousai)
        End If
        '�ʐ^�R�����g
        If jBn.KinsiStrCheck(TextSyasinComment.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�ʐ^�R�����g")
            arrFocusTargetCtrl.Add(TextSyasinComment)
        End If
        '�Ĕ��s���R
        If jBn.KinsiStrCheck(TextSaihakkouRiyuu.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�Ĕ��s���R")
            arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
        End If

        '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
        '�󗝏ڍ�
        If jBn.ByteCheckSJIS(TextJuriSyousai.Text, TextJuriSyousai.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�󗝏ڍ�")
            arrFocusTargetCtrl.Add(TextJuriSyousai)
        End If
        '�ʐ^�R�����g
        If jBn.ByteCheckSJIS(TextSyasinComment.Text, TextSyasinComment.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�ʐ^�R�����g")
            arrFocusTargetCtrl.Add(TextSyasinComment)
        End If
        '�Ĕ��s���R
        If jBn.ByteCheckSJIS(TextSaihakkouRiyuu.Text, TextSaihakkouRiyuu.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�Ĕ��s���R")
            arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
        End If

        '�����̑��`�F�b�N
        '(Chk01:[�n�Ճe�[�u��]�������{�������́A����1�������͂̏ꍇ�A�G���[�Ƃ���B)
        If TextTyousaJissiDate.Text <> "" And TextHantei1Cd.Text = "" Then
            errMess += Messages.MSG062E.Replace("@PARAM1", "����R�[�h1")
            arrFocusTargetCtrl.Add(TextHantei1Cd)
        End If

        '(Chk02:�������{���������͂̎��A����1�����͂̓G���[)
        If TextTyousaJissiDate.Text = "" And TextHantei1Cd.Text <> "" Then
            errMess += Messages.MSG100E.Replace("@PARAM1", "�������{��").Replace("@PARAM2", "����1�R�[�h")
            arrFocusTargetCtrl.Add(TextHantei1Cd)
        End If

        '(Chk03:����1�������͂��i����2������ or �ڑ��������́j�̓G���[)
        If TextHantei1Cd.Text = "" Then
            If SelectHanteiSetuzokuMoji.SelectedValue <> "" Or TextHantei2Cd.Text <> "" Then
                errMess += Messages.MSG061E.Replace("@PARAM1", "����R�[�h1")
                arrFocusTargetCtrl.Add(TextHantei1Cd)
            End If
        End If

        '(Chk04:����1�����͂���͒S���ҁ������͂̓G���[)
        If TextHantei1Cd.Text <> "" And SelectKaisekiTantousya.SelectedValue = "" Then
            errMess += Messages.MSG061E.Replace("@PARAM1", "��͒S����")
            arrFocusTargetCtrl.Add(TextKaisekiTantousyaCd)
        End If

        '(Chk05:����1�����͂��v�揑�쐬���������͂̏ꍇ�A�G���[�ɂ���B)
        If TextHantei1Cd.Text <> "" And TextKeikakusyoSakuseiDate.Text = "" Then
            errMess += Messages.MSG061E.Replace("@PARAM1", "�v�揑�쐬��")
            arrFocusTargetCtrl.Add(ButtonHantei1)
        End If

        '(Chk12:<�ۏ؉��>�ۏ؏����s�������́A���A����1���n�Ճe�[�u���i�X�V�O�j.����1�̏ꍇ�A�m�F���b�Z�[�W��\������B
        '�m�FOK�̏ꍇ�A�o�^������B)
        '=>����R�[�h1�ύX�������ɂĊm�F�ς�

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If errMess <> "" Then
            Dim tmpScript As String = ""

            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False

        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True

    End Function

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        '*************************
        '�n�Ճf�[�^�͍X�V�Ώۂ̂�
        '�@�ʐ����f�[�^�͑S�čX�V
        '*************************

        Dim jrOld As New JibanRecord
        ' ���݂̒n�Ճf�[�^��DB����擾����
        jrOld = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        Dim jibanRec As New JibanRecordHoukokusyo
        Dim brRecHantei As New BukkenRirekiRecord

        '�i��T�X�V�p�ɁADB��̒l���Z�b�g����
        JibanLogic.SetSintyokuJibanData(jrOld, jibanRec)

        '���i1�`3�̃R�s�[
        JibanLogic.ps_CopyTeibetuSyouhinData(jrOld, jibanRec)

        '***************************************
        ' �n�Ճf�[�^(���ʏ��)
        '***************************************
        ' �敪
        jibanRec.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ԍ��i�ۏ؏�NO�j
        jibanRec.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' �f�[�^�j�����
        cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiSyubetu, jibanRec.DataHakiSyubetu)
        ' �f�[�^�j����
        If ucGyoumuKyoutuu.DataHakiSyubetu = "0" Then
            jibanRec.DataHakiDate = DateTime.MinValue
        Else
            cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiDate, jibanRec.DataHakiDate)
        End If
        ' �{�喼
        jibanRec.SesyuMei = ucGyoumuKyoutuu.SesyuMei
        ' �����Z��1
        jibanRec.BukkenJyuusyo1 = ucGyoumuKyoutuu.Jyuusyo1
        ' �����Z��2
        jibanRec.BukkenJyuusyo2 = ucGyoumuKyoutuu.Jyuusyo2
        ' �����Z��3
        jibanRec.BukkenJyuusyo3 = ucGyoumuKyoutuu.Jyuusyo3
        ' ���l
        jibanRec.Bikou = ucGyoumuKyoutuu.Bikou
        ' ���l2
        jibanRec.Bikou2 = ucGyoumuKyoutuu.Bikou2
        ' �X�V�҃��[�U�[ID
        jibanRec.UpdLoginUserId = userInfo.LoginUserId
        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
        If ucGyoumuKyoutuu.AccupdateDateTime.Value = "" Then
            jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
        Else
            jibanRec.UpdDatetime = DateTime.ParseExact(ucGyoumuKyoutuu.AccupdateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If
        '�������@�R�[�h
        jibanRec.TysHouhou = ucGyoumuKyoutuu.TyousaHouhouCd
        '�������@��
        jibanRec.TysHouhouMeiIf = ucGyoumuKyoutuu.TyousaHouhouMei
        '������ЃR�[�h
        jibanRec.TysKaisyaCd = ucGyoumuKyoutuu.TyousaKaishaCd
        '������Ў��Ə��R�[�h
        jibanRec.TysKaisyaJigyousyoCd = ucGyoumuKyoutuu.TyousaKaishaJigyousyoCd
        '������Ж�
        jibanRec.TysKaisyaMeiIf = ucGyoumuKyoutuu.TyousaKaishaMei
        '�����X�R�[�h
        jibanRec.KameitenCd = ucGyoumuKyoutuu.AccKameitenCd.Value
        '�����X��
        jibanRec.KameitenMeiIf = ucGyoumuKyoutuu.KameitenMei
        '�����XTel
        jibanRec.KameitenTelIf = ucGyoumuKyoutuu.KameitenTel
        '�����XFax
        jibanRec.KameitenFaxIf = ucGyoumuKyoutuu.KameitenFax
        '�����XMail
        jibanRec.KameitenMailIf = ucGyoumuKyoutuu.KameitenMail
        '�\����
        jibanRec.KouzouMeiIf = ucGyoumuKyoutuu.KouzouMei

        '***************************************
        ' �@�ʐ����f�[�^
        '***************************************
        ' �u�����񍐏����v�̓@�ʐ����f�[�^���Z�b�g���܂�
        jibanRec.TyousaHoukokusyoRecord = GetSeikyuuCtrlData()

        '***************************************
        ' �n�Ճf�[�^(�����񍐏����)
        '***************************************
        '��
        cl.SetDisplayString(SelectJuri.SelectedValue, jibanRec.TysHkksUmu)
        '�󗝏ڍ�
        jibanRec.TysHkksJyuriSyousai = TextJuriSyousai.Text
        '�󗝓�
        cl.SetDisplayString(TextJuriDate.Text, jibanRec.TysHkksJyuriDate)
        '������
        cl.SetDisplayString(TextHassouDate.Text, jibanRec.TysHkksHakDate)
        '�u���H���ʐ^��
        jibanRec.TikanKoujiSyasinJuri = SelectSyasinJuri.SelectedValue
        '�u���H���ʐ^�R�����g
        jibanRec.TikanKoujiSyasinComment = TextSyasinComment.Text
        '�Ĕ��s��
        cl.SetDisplayString(TextSaihakkouDate.Text, jibanRec.TysHkksSaihakDate)
        '��͒S���҃R�[�h
        cl.SetDisplayString(TextKaisekiTantousyaCd.Text, jibanRec.TantousyaCd)
        '����R�[�h1
        cl.SetDisplayString(TextHantei1Cd.Text, jibanRec.HanteiCd1)
        '����R�[�h2
        cl.SetDisplayString(TextHantei2Cd.Text, jibanRec.HanteiCd2)
        '����ڑ�����
        cl.SetDisplayString(SelectHanteiSetuzokuMoji.SelectedValue, jibanRec.HanteiSetuzokuMoji)

        '***************************************
        ' ��ʓ��͍��ڈȊO
        '***************************************
        cl.SetDisplayString(TextKeikakusyoSakuseiDate.Text, jibanRec.KeikakusyoSakuseiDate) '�v�揑�쐬��

        cl.SetDisplayString(HiddenHosyousyoHakJyky.Value, jibanRec.HosyousyoHakJyky) '�ۏ؏����s��
        cl.SetDisplayString(HiddenHosyousyoHakJykySetteiDate.Value, jibanRec.HosyousyoHakJykySetteiDate) '�ۏ؏����s�󋵐ݒ��

        '�������ʓo�^����
        If HiddenTyousaKekkaTourokuDate.Value = "" Then '������
            '����1�R�[�h
            If TextHantei1Cd.Text <> "" Then '����
                jibanRec.TysKekkaAddDatetime = DateTime.Now
            End If
        Else '���́�DB�l�����̂܂܍X�V
            jibanRec.TysKekkaAddDatetime = DateTime.ParseExact(HiddenTyousaKekkaTourokuDate.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        '�������ʍX�V����
        If HiddenTyousaKekkaTourokuDate.Value <> "" Then '����

            '����1�R�[�h�A����2�R�[�h�A����ڑ����������ꂩ�ɕύX���������ꍇ
            If HiddenHantei1CdOld.Value <> TextHantei1Cd.Text _
                Or HiddenHantei2CdOld.Value <> TextHantei2Cd.Text _
                    Or HiddenHanteiSetuzokuMojiOld.Value <> SelectHanteiSetuzokuMoji.SelectedValue Then

                jibanRec.TysKekkaUpdDatetime = DateTime.Now '�V�X�e�����t���Z�b�g

            Else '�X�V�����Ȃ�

                If HiddenTyousaKekkaUpdateDate.Value <> "" Then 'DB�̒l���̂܂�
                    jibanRec.TysKekkaUpdDatetime = DateTime.ParseExact(HiddenTyousaKekkaUpdateDate.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
                Else
                    jibanRec.TysKekkaUpdDatetime = DateTime.MinValue
                End If
            End If
        End If

        '�X�V��
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userInfo.LoginUserId, DateTime.Now)

        '�H�����茋��FLG
        Dim intKojHanteiKekkaFlg As Integer = Integer.MinValue
        intKojHanteiKekkaFlg = MyLogic.GetKojHanteiKekkaFlg(Me.TextHantei1Cd.Text, Me.TextHantei2Cd.Text, Me.SelectHanteiSetuzokuMoji.SelectedValue)
        cl.SetDisplayString(intKojHanteiKekkaFlg, jibanRec.KojHanteiKekkaFlg)

        '���������f�[�^�̃Z�b�g
        brRecHantei = Me.GetBukkenCtrlData()

        If Not brRecHantei Is Nothing Then
            '����ύX���R�`�F�b�N
            If Me.checkInputBukkenRireki(brRecHantei.Naiyou, True) = False Then Return False

            '����NO���ȉ��łȂ��ꍇ�A�G���[
            If brRecHantei.RirekiNo = 1 Or brRecHantei.RirekiNo = 2 Or brRecHantei.RirekiNo = 3 Or brRecHantei.RirekiNo = 4 Then
            Else
                Dim strMsg As String = Messages.MSG147E.Replace("@PARAM1", "���������f�[�^�̎����ݒ�")
                MLogic.AlertMessage(Me, strMsg, 0, "Err_BukkenRireki1")
                Return False
            End If
        End If

        '************************************************
        ' �ۏ؏����s�󋵁A�ۏ؏����s�󋵐ݒ���A�ۏ؏��i�L���̎����ݒ�
        '************************************************
        '���i�̎����ݒ��ɍs�Ȃ�
        cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.Houkokusyo, jibanRec)

        '���������f�[�^�̎����Z�b�g
        Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cl.GetDisplayString(jrOld.HosyouSyouhinUmu), cl.GetDisplayString(jrOld.KeikakusyoSakuseiDate))

        If Not brRec Is Nothing Then
            '�����������R�[�h�̃`�F�b�N
            Dim strErrMsg As String = String.Empty
            If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                MLogic.AlertMessage(Me, strErrMsg, 0, "ErrBukkenRireki")
                Exit Function
            End If
        End If
        '*********************************************************
        '===========================================================

        ' �f�[�^�̍X�V���s���܂�
        If MyLogic.SaveJibanData(Me, jibanRec, brRec, brRecHantei) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        Dim jBn As New Jiban '�n�Չ�ʃN���X

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �v���_�E��/�R�[�h���͘A�g�ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        jBn.SetPullCdScriptSrc(TextKaisekiTantousyaCd, SelectKaisekiTantousya) '��͒S����

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript�֘A�Z�b�g
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim checkNumber As String = "checkNumber(this);"

        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '���t���ڗp
        Dim onFocusPostBackScriptDate As String = "setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this)){__doPostBack(this.id,'');}}else{checkDate(this);}"

        '*****************************
        '* ����R�[�h�P�A�Q����у{�^��
        '*****************************
        TextHantei1Cd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callHantei1Search(this);}else{checkNumber(this);}"
        TextHantei1Cd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        TextHantei2Cd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callHantei2Search(this);}else{checkNumber(this);}"
        TextHantei2Cd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        ButtonHantei1.Attributes("onclick") = "SetChangeMaeValue('" & HiddenHantei1CdMae.ClientID & "','" & TextHantei1Cd.ClientID & "');"
        ButtonHantei2.Attributes("onclick") = "SetChangeMaeValue('" & HiddenHantei2CdMae.ClientID & "','" & TextHantei2Cd.ClientID & "');"

        '*****************************
        '* ���z�n
        '*****************************
        '�H���X�����Ŕ����z
        TextKoumutenSeikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript & ";SetChangeMaeValue('" & HiddenKoumutenSeikyuuKingakuMae.ClientID & "','" & TextKoumutenSeikyuuKingaku.ClientID & "');"
        TextKoumutenSeikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextKoumutenSeikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�������Ŕ����z
        TextJituseikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript & ";SetChangeMaeValue('" & HiddenJituseikyuuKingakuMae.ClientID & "','" & TextJituseikyuuKingaku.ClientID & "');"
        TextJituseikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextJituseikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown

        '*****************************
        '* ���t�n
        '*****************************
        '�󗝓�
        TextJuriDate.Attributes("onblur") = checkDate
        TextJuriDate.Attributes("onkeydown") = disabledOnkeydown
        '������
        TextHassouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextHassouDate.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenHassouDateMae.ClientID & "','" & TextHassouDate.ClientID & "');" & onFocusPostBackScriptDate
        TextHassouDate.Attributes("onkeydown") = disabledOnkeydown
        '�Ĕ��s��
        TextSaihakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextSaihakkouDate.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenSaihakkouDateMae.ClientID & "','" & TextSaihakkouDate.ClientID & "');" & onFocusPostBackScriptDate
        TextSaihakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '���������s��
        TextSeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        TextSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '*****************************
        '* �h���b�v�_�E�����X�g
        '*****************************
        '�����L��
        SelectSeikyuuUmu.Attributes("onfocus") = "checkTempValueForOnBlur(this);" & "SetChangeMaeValue('" & HiddenSeikyuuUmuMae.ClientID & "','" & SelectSeikyuuUmu.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���l�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '��͒S����
        TextKaisekiTantousyaCd.Attributes("onblur") += checkNumber
        TextKaisekiTantousyaCd.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �@�\�ʃe�[�u���̕\���ؑ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����񍐏�
        Me.AncTysHoukokusyo.HRef = "JavaScript:changeDisplay('" & Me.TBodyHoukokushoInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenHoukokusyoInfoStyle.ClientID & "','" & Me.TBodyHoukokushoInfo.ClientID & "');"

    End Sub

    ''' <summary>
    ''' �Ɩ�����[���[�U�[�R���g���[��]��ucGyoumuKyoutuu_OyaGamenAction_hensyu�ŌĂ΂�鏈��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ucGyoumuKyoutuu_OyaGamenAction_hensyu(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnDisabled As Boolean) Handles ucGyoumuKyoutuu.OyaGamenAction_hensyu
        '�R���g���[���̗L��/����
        CheckHakiDisable(blnDisabled)
    End Sub

    ''' <summary>
    ''' �j����ʂɂ���āA�R���g���[���̗L��/������ؑւ���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckHakiDisable(Optional ByVal blnFlg As Boolean = False)
        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�L�����A�������̑ΏۊO�ɂ���R���g���[���S
        Dim noTarget As New Hashtable
        noTarget.Add(divTyousaHoukokusyo, True)
        noTarget.Add(ButtonTouroku1.ID, True) '�o�^�{�^��1
        noTarget.Add(ButtonTouroku2.ID, True) '�o�^�{�^��2

        If blnFlg Then
            '�S�ẴR���g���[���𖳌���
            jBn.ChangeDesabledAll(divTyousaHoukokusyo, True, noTarget)
        Else
            '�S�ẴR���g���[����L����
            jBn.ChangeDesabledAll(divTyousaHoukokusyo, False, noTarget)
        End If

    End Sub


#Region "���z�v�Z�֘A"

    ''' <summary>
    ''' ���z�ݒ�(�����/�ō����z)
    ''' </summary>
    ''' <param name="objJituSeikyuu">�������Ŕ����z</param>
    ''' <param name="objZeiritu">����Ń}�X�^.�ŗ�</param>
    ''' <remarks>����ł���ѐō����z�v�Z�����A�o�͂���</remarks>
    Private Sub SetZeiKingaku(ByVal objJituSeikyuu As Object, ByVal objZeiritu As Object)
        Dim lngSyouhizei As Long = 0 '�����
        Dim lngZeikomiKingaku As Long = 0 '�ō����z

        '�y����Łz=�y�������Ŕ����z�z������Ń}�X�^.�ŗ��iKEY�F�X�ʏ����e�[�u��.�ŋ敪�j
        lngSyouhizei = CLng(objJituSeikyuu * objZeiritu)

        '�y�ō����z�z=�y�������Ŕ����z�z�{�y����Łz
        lngZeikomiKingaku = CLng(objJituSeikyuu) + lngSyouhizei

        TextSyouhizei.Text = lngSyouhizei.ToString(EarthConst.FORMAT_KINGAKU_1) '�����
        TextZeikomiKingaku.Text = lngZeikomiKingaku.ToString(EarthConst.FORMAT_KINGAKU_1) '�ō����z

    End Sub

    ''' <summary>
    ''' ���z�ݒ�(�����z/�c�z)
    ''' </summary>
    ''' <param name="strZeikomiNyuukingaku">�@�ʓ����e�[�u��.�ō��������z</param>
    ''' <param name="intZeikomiHenkingaku">�ō��ԋ����z�iKEY:�@�ʐ����e�[�u��.�敪�A�ۏ؏�NO�A���ރR�[�h="150"�j</param>
    ''' <param name="strZeikomiKingaku">�����z(�ō�)</param>
    ''' <remarks>�����z�i�ō��j����юc�z�v�Z�����A�o�͂���</remarks>
    Private Sub SetNyuukingaku( _
                                    ByVal strZeikomiNyuukingaku As String _
                                    , ByVal intZeikomiHenkingaku As Integer _
                                    , ByVal strZeikomiKingaku As String _
                                )
        Dim intZeikomiNyuukingaku As Integer = Integer.Parse(strZeikomiNyuukingaku)
        Dim intNyuukingaku As Integer = 0
        Dim intZangaku As Integer = 0

        '�y�����z�i�ō��j�z=�@�ʓ����e�[�u��.�ō��������z�|�ō��ԋ����z�iKEY:�@�ʐ����e�[�u��.�敪�A�ۏ؏�NO�A���ރR�[�h="150"�j
        intNyuukingaku = intZeikomiNyuukingaku - intZeikomiHenkingaku

        '�y�c�z�z=�y�ō����z�z�|�y�����z(�ō�)�z
        intZangaku = Integer.Parse(strZeikomiKingaku) - intNyuukingaku

        TextNyuukingaku.Text = intNyuukingaku.ToString(EarthConst.FORMAT_KINGAKU_1) '�����z�i�ō��j
        TextZangaku.Text = intZangaku.ToString(EarthConst.FORMAT_KINGAKU_1) '�c�z

        '�yhidden���ځz
        HiddenZeikomiNyuukingaku.Value = intZeikomiNyuukingaku  '�ō��������z
        HiddenZeikomiHenkinkingaku.Value = intZeikomiNyuukingaku  '�ō��ԋ����z

    End Sub

    ''' <summary>
    ''' ���z�ݒ�(�c�z)
    ''' </summary>
    ''' <param name="strZeikomiKingaku">�ō����z</param>
    ''' <param name="strNyuukingaku">�����z(�ō�)</param>
    ''' <remarks>�c�z���v�Z���A�o�͂���</remarks>
    Protected Function SetZangaku(ByVal strZeikomiKingaku As String, ByVal strNyuukingaku As String) As String
        Dim strReturn As String = ""
        Dim lngZeikomi As Long = 0
        Dim lngNyuukin As Long = 0

        If strZeikomiKingaku.Length = 0 Or strNyuukingaku.Length = 0 Then
            Return strReturn
        End If

        lngZeikomi = CLng(strZeikomiKingaku)
        lngNyuukin = CLng(strNyuukingaku)
        strReturn = (lngZeikomi - lngNyuukin).ToString(EarthConst.FORMAT_KINGAKU_1)
        Return strReturn
    End Function

    ''' <summary>
    ''' �H���X�����Ŕ����z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKoumutenSeikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim syouhinRec As New Syouhin23Record

        setFocusAJ(TextJituseikyuuKingaku)

        '����
        If SelectSeikyuuUmu.SelectedValue = "1" Then '�L
            '���i�R�[�h
            If TextItemCd.Text.Length <> 0 Then '�ݒ��

                '�ŏ��ݒ�
                SetZeiInfo(TextItemCd.Text)

                '������
                If TextSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���

                    '�������Ŕ����z�����.�H���X�����Ŕ����z
                    TextJituseikyuuKingaku.Text = TextKoumutenSeikyuuKingaku.Text

                    SetKingaku() '���z�Čv�Z

                ElseIf TextSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '������

                    Dim kameitenlogic As New KameitenSearchLogic
                    Dim blnTorikesi As Boolean = False
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If cl.getKeiretuFlg(record.KeiretuCd) Then '3�n��

                        '<�\2>�������Ŕ����z�i�|���j�̐ݒ聣

                        Dim logic As New JibanLogic
                        Dim koumuten_gaku As Integer = 0
                        Dim zeinuki_gaku As Integer = 0

                        cl.SetDisplayString(TextKoumutenSeikyuuKingaku.Text, koumuten_gaku)
                        koumuten_gaku = IIf(koumuten_gaku = Integer.MinValue, 0, koumuten_gaku)

                        If logic.GetSeikyuuGaku(sender, _
                                                5, _
                                                record.KeiretuCd, _
                                                TextItemCd.Text, _
                                                koumuten_gaku, _
                                                zeinuki_gaku) Then


                            '���i�����擾(�L�[:���i�R�[�h)
                            syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                            If Not syouhinRec Is Nothing Then
                                '(*3)�����L���ύX���ɁA�����ݒ肳�ꂽ�H���X�������z��0�i���i�}�X�^.�W�����i��0�j�̏ꍇ�A1��̂ݎ��������z�̎����ݒ���s���B
                                If syouhinRec.HyoujunKkk = 0 Then
                                    If HiddenJituseikyuu1Flg.Value = "" Then
                                        HiddenJituseikyuu1Flg.Value = "1" '�t���O�����Ă�

                                        ' �Ŕ����z�i���������z�j�փZ�b�g
                                        TextJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                                    End If

                                    '*****************
                                    '* �n�ՃV�X�e���̏����ɍ��킹�邽�߁A�R�����g�A�E�g
                                    '*****************
                                    'Else
                                    '    ' �Ŕ����z�i���������z�j�փZ�b�g
                                    '    TextJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU1)

                                    SetKingaku() '���z�Čv�Z

                                End If

                            End If

                        End If

                    End If

                End If
            End If

        End If

        '��ʐ���
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' �������Ŕ����z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextJituSeikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextSeikyuusyoHakkouDate)

        '�������Ŕ����z
        If TextJituseikyuuKingaku.Text.Length = 0 Then '���͂Ȃ�
            TextSyouhizei.Text = ""
            TextZeikomiKingaku.Text = ""
            'TextZangaku.Text = "0"

            SetKingaku() '���z�Čv�Z

        Else '���͂���

            '�����L��
            If SelectSeikyuuUmu.SelectedValue = "1" Then '�L
                '���i�R�[�h
                If TextItemCd.Text.Length <> 0 Then '�ݒ��

                    '�ŏ��ݒ�
                    SetZeiInfo(TextItemCd.Text)

                    '������
                    If TextSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���

                        '�H���X�����Ŕ����z�����.�������Ŕ����z
                        TextKoumutenSeikyuuKingaku.Text = TextJituseikyuuKingaku.Text

                        SetKingaku() '���z�Čv�Z

                    Else '���ڐ����ȊO
                        SetKingaku() '���z�Čv�Z
                    End If

                End If

            End If

        End If

        '��ʐ���
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' �ŗ�/�ŋ敪��Hidden�ɃZ�b�g����
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetZeiInfo(ByVal strItemCd As String)
        Dim houkokusyoLogic As New HoukokusyoLogic
        Dim syouhinRec As New Syouhin23Record

        '���i�����擾(�L�[:���i�R�[�h)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪
        End If
    End Sub

    ''' <summary>
    ''' ���z�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku(Optional ByVal blnZeigaku As Boolean = False)

        ' �Ŕ����i�i���������z�j
        Dim zeinuki_ctrl As TextBox = TextJituseikyuuKingaku
        ' ����ŗ�
        Dim zeiritu_ctrl As HtmlInputHidden = HiddenZeiritu
        ' ����Ŋz
        Dim zeigaku_ctrl As TextBox = TextSyouhizei
        ' �ō����z
        Dim zeikomi_gaku_ctrl As TextBox = TextZeikomiKingaku

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '������
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '���͂���

            cl.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cl.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            If blnZeigaku Then '����Ŋz�̒l�Ōv�Z
                cl.SetDisplayString(CInt(zeigaku_ctrl.Text), zeigaku)
            Else
                zeigaku = Fix(zeinuki * zeiritu)
            End If
            zeikomi_gaku = zeinuki + zeigaku

            TextSyouhizei.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '�����
            TextZeikomiKingaku.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '�ō����z

        End If

        ' �����z/�c�z���Z�b�g
        CalcZangaku(TextZeikomiKingaku.Text)

    End Sub

    ''' <summary>
    ''' �c�z��ݒ肵�܂�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZangaku()
        Dim lngNyuukingaku As Long = 0
        Dim lngZangaku As Long = 0

        If HiddenNyuukingakuOld.Value <> "" Then
            lngNyuukingaku = CLng(HiddenNyuukingakuOld.Value.Replace(",", ""))
        End If

        '�c�z���ō����z-�����z(�ō�)
        lngZangaku = CLng(TextZeikomiKingaku.Text.Replace(",", "")) - CLng(TextNyuukingaku.Text.Replace(",", ""))
        TextZangaku.Text = lngZangaku.ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

#End Region

    ''' <summary>
    ''' (2) �����񍐏��󗝕ύX���̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectJuri_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strJuri As String = SelectJuri.SelectedValue '��
        Dim strJuriDate As String = TextJuriDate.Text '�󗝓�
        Dim strHassouDate As String = TextHassouDate.Text '������
        Dim strHizukeHassouDate As String = "" '���t�}�X�^.�񍐏�������

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(SelectJuri)

        If strJuri = "1" Then '�����񍐏��󗝁�1�i�L��j�̏ꍇ

            If strJuriDate = String.Empty Then '�󗝓��������͂̏ꍇ
                ' �V�X�e�����t���Z�b�g
                TextJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                If strHassouDate = String.Empty Then '�������������͂̏ꍇ

                    Dim houkokusyoLogic As New HoukokusyoLogic
                    Dim strRet As String = String.Empty

                    '���������s���̎����ݒ�
                    '�������񍐏��󗝓�����L�̓��t�}�X�^.�񍐏��������̏ꍇ�́A���t�}�X�^.�񍐏��������{1������ҏW����
                    strRet = houkokusyoLogic.GetHoukokusyoHassoudate( _
                                                             ucGyoumuKyoutuu.Kubun _
                                                            , TextJuriDate.Text)
                    If strRet <> String.Empty Then
                        TextHassouDate.Text = strRet
                    End If
                End If

            End If

        ElseIf strJuri = "2" Or strJuri = "3" Then '�����񍐏��󗝁�2,3�i�ۗ��A���́A���t�s�v�j�̏ꍇ

            If strJuriDate = String.Empty Then '�󗝓��������͂̏ꍇ
                ' �V�X�e�����t���Z�b�g
                TextJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        ElseIf strJuri = "4" Then '�����񍐏��󗝁�4�i�Ĕ����j�̏ꍇ
            If strJuriDate = String.Empty Then '�󗝓��������͂̏ꍇ
                ' �V�X�e�����t���Z�b�g
                TextJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        End If

        '����������
        SetEnableControl()

    End Sub

#Region "�����ʊ֘A"

    ''' <summary>
    ''' ����R�[�h1�{�^��������/�����ʌ�����ʎQ��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHantei1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If HiddenHanteiSearchType1.Value <> "1" Then
            TextHantei1_ChangeSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            TextHantei1_ChangeSub(sender, e, False)
            HiddenHanteiSearchType1.Value = String.Empty
        End If

        ucGyoumuKyoutuu.SetHanteiNG(TextHantei1Cd.Text, TextHantei2Cd.Text)

    End Sub

    ''' <summary>
    ''' ����R�[�h2�{�^��������/�����ʌ�����ʎQ��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHantei2_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If HiddenHanteiSearchType2.Value <> "1" Then
            TextHantei2_ChangeSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            TextHantei2_ChangeSub(sender, e, False)
            HiddenHanteiSearchType2.Value = String.Empty
        End If

        ucGyoumuKyoutuu.SetHanteiNG(TextHantei1Cd.Text, TextHantei2Cd.Text)

    End Sub

    ''' <summary>
    ''' (12) ����1�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHantei1_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim kisosiyouSearchLogic As New KisoSiyouLogic
        Dim blnTorikesi As Boolean = False
        Dim dataArray As New List(Of KisoSiyouRecord)
        Dim intCount As Integer = 0

        '1.��ʂ̐ݒ�
        '����1���̕\���F��ݒ肷��
        If TextHantei1Cd.Text <> "" Then
            dataArray = kisosiyouSearchLogic.GetKisoSiyouSearchResult( _
                                    TextHantei1Cd.Text _
                                    , "" _
                                    , ucGyoumuKyoutuu.AccKameitenCd.Value _
                                    , True _
                                    , blnTorikesi _
                                    , intCount _
                                    )
        End If

        If intCount = 1 Then
            Dim recData As KisoSiyouRecord = dataArray(0)
            TextHantei1Cd.Text = recData.KsSiyouNo
            SpanHantei1.InnerHtml = recData.KsSiyou
            Me.HiddenHantei1CdMae.Value = TextHantei1Cd.Text

            ' �������NG�ݒ�
            If recData.KahiKbn = 9 Then
                SpanHantei1.Style("color") = "red"
            Else
                SpanHantei1.Style("color") = "blue"
            End If

            '����R�[�h1�����{�^�����s�㏈��
            ButtonHantei1SearchAfter(sender, e)

            '�Z�b�g�t�H�[�J�X
            setFocusAJ(ButtonHantei1)
        Else

            SpanHantei1.Style("color") = "black"

            '����R�[�h���ݒ�̂��߁A�N���A
            SpanHantei1.InnerHtml = ""

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpFocusScript = "objEBI('" & ButtonHantei1.ClientID & "').focus();"
            Dim tmpScript = "callSearch('" & TextHantei1Cd.ClientID & EarthConst.SEP_STRING & ucGyoumuKyoutuu.AccKameitenCd.ClientID & _
                                        "','" & UrlConst.SEARCH_HANTEI & "','" & _
                                        TextHantei1Cd.ClientID & EarthConst.SEP_STRING & SpanHantei1.ClientID & _
                                        "','" & ButtonHantei1.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf HiddenHanteiSearchType1.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript, True)
            End If
        End If

    End Sub

    ''' <summary>
    ''' ����R�[�h1�����{�^�����s�㏈��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHantei1SearchAfter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        '(1)��͒S���ҁ������͂̏ꍇ
        If SelectKaisekiTantousya.SelectedValue = "" Then
            '�Y���҂����邩�`�F�b�N
            If ChkTantousya(SelectKaisekiTantousya, cl.GetDispNum(userInfo.AccountNo)) Then
                TextKaisekiTantousyaCd.Text = cl.GetDispNum(userInfo.AccountNo)
                SelectKaisekiTantousya.SelectedValue = cl.GetDispNum(userInfo.AccountNo)
            End If
        End If

        '(2)����1�����͂̏ꍇ
        If TextHantei1Cd.Text.Length <> 0 Then
            '�v�揑�쐬��
            If TextKeikakusyoSakuseiDate.Text.Length = 0 Then
                TextKeikakusyoSakuseiDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If
        End If

        '2.����R�[�h1�ύX�m�F
        '(1)�ۏ؏����s��=���͍ρA������1���n�Ճe�[�u���i�X�V�O�j.����1�̏ꍇ
        If HiddenHosyousyoHakkouDate.Value <> "" And TextHantei1Cd.Text <> HiddenHantei1CdOld.Value Then
            '����R�[�h1�̊m�F���b�Z�[�W��\������B
            Dim strMsg As String = Messages.MSG063C.Replace("@PARAM1", HiddenHantei1CdOld.Value)

            tmpScript = "callHantei1Cancel('" & strMsg & "','" & ButtonHantei1.ClientID & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHantei1SearchAfter1", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' (13) ����2�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHantei2_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim kisosiyouSearchLogic As New KisoSiyouLogic
        Dim blnTorikesi As Boolean = False
        Dim dataArray As New List(Of KisoSiyouRecord)
        Dim intCount As Integer = 0

        '1.��ʂ̐ݒ�
        '����1���̕\���F��ݒ肷��
        If TextHantei2Cd.Text <> "" Then
            dataArray = kisosiyouSearchLogic.GetKisoSiyouSearchResult( _
                                    TextHantei2Cd.Text _
                                    , "" _
                                    , ucGyoumuKyoutuu.AccKameitenCd.Value _
                                    , True _
                                    , blnTorikesi _
                                    , intCount _
                                    )
        End If

        If intCount = 1 Then
            Dim recData As KisoSiyouRecord = dataArray(0)
            TextHantei2Cd.Text = recData.KsSiyouNo
            SpanHantei2.InnerHtml = recData.KsSiyou
            Me.HiddenHantei2CdMae.Value = TextHantei2Cd.Text

            ' �������NG�ݒ�
            If recData.KahiKbn = 9 Then
                SpanHantei2.Style("color") = "red"
            Else
                SpanHantei2.Style("color") = "blue"
            End If

            '�Z�b�g�t�H�[�J�X
            setFocusAJ(ButtonHantei2)
        Else

            SpanHantei2.Style("color") = "black"

            '����R�[�h���ݒ�̂��߁A�N���A
            SpanHantei2.InnerHtml = ""

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpFocusScript = "objEBI('" & ButtonHantei2.ClientID & "').focus();"
            Dim tmpScript = "callSearch('" & TextHantei2Cd.ClientID & EarthConst.SEP_STRING & ucGyoumuKyoutuu.AccKameitenCd.ClientID & _
                                        "','" & UrlConst.SEARCH_HANTEI & "','" & _
                                        TextHantei2Cd.ClientID & EarthConst.SEP_STRING & SpanHantei2.ClientID & _
                                        "','" & ButtonHantei2.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf HiddenHanteiSearchType2.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript, True)
            End If
        End If

    End Sub

    ''' <summary>
    ''' ��͒S���҂̑��݃`�F�b�N
    ''' </summary>
    ''' <param name="drpArg">�`�F�b�N�Ώۃh���b�v�_�E�����X�g</param>
    ''' <param name="strSearchCd">[String]�R�[�h�l</param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks>��͒S���҂����݂��邩�ǂ����𔻒f����</remarks>
    Protected Function ChkTantousya(ByVal drpArg As DropDownList, ByVal strSearchCd As String) As Boolean
        Dim intItemCnt As Integer = drpArg.Items.Count '�A�C�e����
        Dim intCnt As Integer '�J�E���^

        For intCnt = 0 To intItemCnt - 1
            If strSearchCd = drpArg.Items(intCnt).Value Then
                Return True
            End If
        Next

        Return False
    End Function

#Region "���������f�[�^�̓���"
    ''' <summary>
    ''' ��������o�^�p���R�[�h�ɃR���g���[���̓��e���Z�b�g���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBukkenCtrlData() As BukkenRirekiRecord

        ' ����ύX���R=���ݒ莞�A���������ւ̏����͍s��Ȃ�
        If Me.HiddenHanteiHenkouRiyuu.Value = String.Empty Then
            Return Nothing
        End If

        '��������o�^�p���R�[�h
        Dim record As New BukkenRirekiRecord
        Dim logic As New HoukokusyoLogic

        '�H�����茋��FLG
        Dim intKojHanteiKekkaFlgOld As Integer = 0 'OLD
        Dim intKojHanteiKekkaFlg As Integer = 0

        If CStr(Me.HiddenKojHanteiKekkaFlgOld.Value) <> String.Empty Then
            intKojHanteiKekkaFlgOld = CInt(Me.HiddenKojHanteiKekkaFlgOld.Value)
        End If
        intKojHanteiKekkaFlg = logic.GetKojHanteiKekkaFlg(Me.TextHantei1Cd.Text, Me.TextHantei2Cd.Text, Me.SelectHanteiSetuzokuMoji.SelectedValue)

        ' �敪
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ۏ؏�NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        '�������
        record.RirekiSyubetu = EarthConst.BUKKEN_RIREKI_RIREKI_SYUBETU_HANTEI
        '����NO
        record.RirekiNo = Me.SetRirekiNo(intKojHanteiKekkaFlgOld, intKojHanteiKekkaFlg)
        '����NO
        record.NyuuryokuNo = Integer.MinValue
        '���e
        record.Naiyou = Me.SetNaiyou()
        '�ėp���t
        cl.SetDisplayString("", record.HanyouDate)
        '�ėp�R�[�h
        cl.SetDisplayString("", record.HanyouCd)
        '�Ǘ����t
        cl.SetDisplayString("", record.KanriDate)
        '�Ǘ��R�[�h
        record.KanriCd = Me.SetKanriCd(intKojHanteiKekkaFlgOld, intKojHanteiKekkaFlg)
        '�ύX�ۃt���O
        record.HenkouKahiFlg = 1
        '���
        record.Torikesi = 0
        '�o�^(�X�V)���O�C�����[�UID
        record.UpdLoginUserId = userInfo.LoginUserId
        '�o�^(�X�V)����
        record.UpdDatetime = DateTime.Now

        Return record
    End Function

    ''' <summary>
    ''' �������ɗ���NO���Z�b�g����
    ''' ���ύX�O�ƕύX��.�H��FLG�̒l�Ŕ���
    ''' </summary>
    ''' <param name="intKojHanteiKekkaFlgOld">�ύX�O.�H������FLG</param>
    ''' <param name="intKojHanteiKekkaFlg">�ύX��.�H������FLG</param>
    ''' <returns>
    ''' �ύX�H���������H�������i�ύX�O�̒n��T�̍H��FLG=0�A�ύX��̍H��FLG=0�j�F1
    ''' �ύX�H�����聨�H������i�ύX�O�̒n��T�̍H��FLG=1�A�ύX��̍H��FLG=1�j�F2
    ''' �ύX�H���������H������i�ύX�O�̒n��T�̍H��FLG=0�A�ύX��̍H��FLG=1�j�F3
    ''' �ύX�H�����聨�H�������i�ύX�O�̒n��T�̍H��FLG=1�A�ύX��̍H��FLG=0�j�F4
    ''' </returns>
    ''' <remarks></remarks>
    Private Function SetRirekiNo(ByVal intKojHanteiKekkaFlgOld As Integer, ByVal intKojHanteiKekkaFlg As Integer) As Integer
        Dim intRet As Integer = Integer.MinValue  '�߂�l

        If intKojHanteiKekkaFlgOld = 0 Then

            If intKojHanteiKekkaFlg = 0 Then
                intRet = 1
            ElseIf intKojHanteiKekkaFlg = 1 Then
                intRet = 3
            End If

        ElseIf intKojHanteiKekkaFlgOld = 1 Then

            If intKojHanteiKekkaFlg = 0 Then
                intRet = 4
            ElseIf intKojHanteiKekkaFlg = 1 Then
                intRet = 2
            End If

        End If

        Return intRet
    End Function

    ''' <summary>
    ''' �������ɓ��e���Z�b�g����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetNaiyou() As String
        Dim strRetVal As String = String.Empty '�߂�l

        Dim kisoSiyouLogic As New KisoSiyouLogic

        '����1
        Dim intHantei1Cd As Integer
        '����2
        Dim intHantei2Cd As Integer
        '����ڑ���
        Dim intHanteiSetuzokusi As Integer
        '����1��
        Dim strHantei1Mei As String = String.Empty
        '����2��
        Dim strHantei2Mei As String = String.Empty
        '����ڑ�����
        Dim strHanteiSetuzokusiMei As String = String.Empty

        '����1
        If CStr(Me.HiddenHantei1CdOld.Value) <> String.Empty Then
            intHantei1Cd = CInt(Me.HiddenHantei1CdOld.Value)
        Else
            intHantei1Cd = Integer.MinValue
        End If
        '����2
        If CStr(Me.HiddenHantei2CdOld.Value) <> String.Empty Then
            intHantei2Cd = CInt(Me.HiddenHantei2CdOld.Value)
        Else
            intHantei2Cd = Integer.MinValue
        End If
        '����ڑ���
        If CStr(Me.HiddenHanteiSetuzokuMojiOld.Value) <> String.Empty Then
            intHanteiSetuzokusi = CInt(Me.HiddenHanteiSetuzokuMojiOld.Value)
        Else
            intHanteiSetuzokusi = Integer.MinValue
        End If

        strHantei1Mei = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(intHantei1Cd)) '����P��
        strHantei2Mei = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(intHantei2Cd)) '����Q��

        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList
        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.KsSiyouSetuzokusi, True, False)

        '**************************
        '�u���e�v�̎����ݒ�
        '**************************
        '**************
        '*�ύX�O
        '**************      
        objDrpTmp.SelectedValue = intHanteiSetuzokusi '����ڑ���

        strRetVal = EarthConst.BUKKEN_RIREKI_HENKOU_MAE
        If Me.HiddenHantei1CdOld.Value <> String.Empty Then
            '����1
            strRetVal &= "[" & Me.HiddenHantei1CdOld.Value & "]" & strHantei1Mei
        End If
        If Me.HiddenHanteiSetuzokuMojiOld.Value <> String.Empty Then
            '����ڑ���
            strRetVal &= "[" & Me.HiddenHanteiSetuzokuMojiOld.Value & "]" & objDrpTmp.SelectedItem.Text
        End If
        If Me.HiddenHantei2CdOld.Value <> String.Empty Then
            '����2
            strRetVal &= "[" & Me.HiddenHantei2CdOld.Value & "]" & strHantei2Mei
        End If

        '**************
        '*�ύX��
        '**************
        objDrpTmp.SelectedValue = Me.SelectHanteiSetuzokuMoji.SelectedValue

        strRetVal &= EarthConst.BUKKEN_RIREKI_HENKOU_ATO
        If Me.TextHantei1Cd.Text <> String.Empty Then
            '����1
            strRetVal &= "[" & Me.TextHantei1Cd.Text & "]" & Me.SpanHantei1.InnerHtml
        End If
        If Me.SelectHanteiSetuzokuMoji.SelectedValue <> String.Empty Then
            '����ڑ���
            strRetVal &= "[" & Me.SelectHanteiSetuzokuMoji.SelectedValue & "]" & objDrpTmp.SelectedItem.Text
        End If
        If Me.TextHantei2Cd.Text <> String.Empty Then
            '����2
            strRetVal &= "[" & Me.TextHantei2Cd.Text & "]" & Me.SpanHantei2.InnerHtml
        End If

        '**************
        '* ����ύX���R
        '**************
        strRetVal &= EarthConst.BUKKEN_RIREKI_HENKOU_RIYUU & Me.HiddenHanteiHenkouRiyuu.Value

        Return strRetVal
    End Function

    ''' <summary>
    ''' �������ɊǗ��R�[�h���Z�b�g����
    ''' </summary>
    ''' <param name="intKojHanteiKekkaFlgOld">�ύX�O.�H������FLG</param>
    ''' <param name="intKojHanteiKekkaFlg">�ύX��.�H������FLG</param>
    ''' <returns>�������Ƒ������𕶎���A�����ĕԂ�</returns>
    ''' <remarks></remarks>
    Private Function SetKanriCd(ByVal intKojHanteiKekkaFlgOld As Integer, ByVal intKojHanteiKekkaFlg As Integer) As String
        Dim strRet As String = String.Empty

        strRet = CStr(intKojHanteiKekkaFlgOld) & CStr(intKojHanteiKekkaFlg)

        Return strRet
    End Function

#End Region

#End Region

#Region "�����ݒ�"

    ''' <summary>
    ''' �����񍐏��������ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHassouDate_ServerChange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(SelectSyasinJuri)

        '������
        If TextHassouDate.Text.Length <> 0 Then '���͂���
            '�����ݒ�͂Ȃ��A��ʐ���̂݁������ɏ����Ȃ�

        ElseIf TextHassouDate.Text.Length = 0 Then '���͂Ȃ�

            '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextHattyuusyoKingaku.Text <> "0" And TextHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextHassouDate.Text = HiddenHassouDateMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextHassouDate_ServerChange1", tmpScript, True)
                Exit Sub
            End If

            '�������ݒ�
            Me.TextSaihakkouDate.Text = String.Empty '�Ĕ��s��
            Me.TextSaihakkouRiyuu.Text = String.Empty '�Ĕ��s���R

            ClearControl()

        End If

        '��ʐ���
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' �Ĕ��s���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSaihakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strDtTmp As String
        Dim tmpScript As String = ""

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(TextSaihakkouDate)

        '�Ĕ��s��
        If TextSaihakkouDate.Text.Length <> 0 Then '���͂���

            setFocusAJ(SelectSeikyuuUmu)

            '�����L��
            If SelectSeikyuuUmu.SelectedValue = "1" Then '�L
                '���i�R�[�h
                If TextItemCd.Text.Length <> 0 Then '�ݒ��
                    '���������s��
                    If TextSeikyuusyoHakkouDate.Text.Length = 0 Then '������
                        '�������ߓ��̃Z�b�g
                        Me.ucSeikyuuSiireLink.SetSeikyuuSimeDate(Me.TextItemCd.Text)
                        '���������s���̎����ݒ�
                        strDtTmp = Me.ucSeikyuuSiireLink.GetSeikyuusyoHakkouDate()
                        TextSeikyuusyoHakkouDate.Text = strDtTmp
                    End If

                    '����N����
                    If TextUriageNengappi.Text.Length = 0 Then '������
                        '����N�����̎����ݒ�
                        TextUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                End If

            ElseIf SelectSeikyuuUmu.SelectedValue = "0" Then '��
                '���i�R�[�h
                If TextItemCd.Text.Length <> 0 Then '�ݒ��
                    '����N����
                    If TextUriageNengappi.Text.Length = 0 Then '������
                        '����N�����̎����ݒ�
                        TextUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If
                End If
            End If

        Else '���͂Ȃ�

            '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextHattyuusyoKingaku.Text <> "0" And TextHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextSaihakkouDate.Text = HiddenSaihakkouDateMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextSaihakkouDate_TextChanged1", tmpScript, True)
                Exit Sub
            End If

            '�������ݒ�
            Me.TextSaihakkouRiyuu.Text = String.Empty '�Ĕ��s���R

            ClearControl()

        End If

        '��ʐ���
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' �����L���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""
        Dim blnTorikesi As Boolean = False

        Dim strDtTmp As String

        Dim syouhinRec As New Syouhin23Record

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(SelectSeikyuuUmu)

        '�d���z�͏��0�~
        Me.HiddenSiireGaku.Value = "0"
        Me.HiddenSiireSyouhiZei.Value = "0"

        '�����L��
        If SelectSeikyuuUmu.SelectedValue = "" Then '��

            '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextHattyuusyoKingaku.Text <> "0" And TextHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectSeikyuuUmu.SelectedValue = HiddenSeikyuuUmuMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextSaihakkouDate_TextChanged1", tmpScript, True)
                Exit Sub
            End If

            '�������ݒ�
            ClearControl() '�󔒃N���A

        ElseIf SelectSeikyuuUmu.SelectedValue = "0" Then '��

            '���z��0�N���A
            Clear0SyouhinTable()

            '���i�R�[�h/���i���̎����ݒ聣
            SetSyouhinInfo()

            SetKingaku() '���z�̍Čv�Z

        ElseIf SelectSeikyuuUmu.SelectedValue = "1" Then '�L

            '���i�R�[�h/���i���̎����ݒ聣
            SetSyouhinInfo()

            '���i�R�[�h
            If TextItemCd.Text = String.Empty Then '�ݒ�Ȃ�
                '�����L��
                SelectSeikyuuUmu.SelectedValue = "" '��

                SetEnableControl() '��ʐ���
                Exit Sub
            Else
                '���������s��
                If TextSeikyuusyoHakkouDate.Text = String.Empty Then '������
                    '�������ߓ��̃Z�b�g
                    Me.ucSeikyuuSiireLink.SetSeikyuuSimeDate(Me.TextItemCd.Text)
                    '���������s���̎����ݒ�
                    strDtTmp = Me.ucSeikyuuSiireLink.GetSeikyuusyoHakkouDate()
                    TextSeikyuusyoHakkouDate.Text = strDtTmp
                End If

                TextUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) '����N����

                '�������m��
                If TextHattyuusyoKakutei.Text.Length = 0 Then '(*5)�������m�肪�󔒂̏ꍇ�́A�u0�F���m��v��ݒ肷��
                    TextHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
                End If

            End If

            '���i�R�[�h
            If TextItemCd.Text <> String.Empty Then '�ݒ��

                '*************************
                '* �ȉ��A�����ݒ菈��
                '*************************
                If TextSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���

                    '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    If record Is Nothing Then '�f�[�^�擾�ł��Ȃ������ꍇ
                        SelectSeikyuuUmu.SelectedValue = "" '�擾NG(�����L���̃N���A)

                        ClearControl() '�󔒃N���A
                    End If

                    If record.Torikesi <> 0 Then '����t���O�������Ă���ꍇ
                        '**************************************************
                        ' �������i�n��ȊO�j
                        '**************************************************
                        ' �H���X�����z�͂O
                        TextKoumutenSeikyuuKingaku.Text = "0"

                        '�������Ŕ����z�̎����ݒ�
                        syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                        If Not syouhinRec Is Nothing Then
                            HiddenZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                            '�������Ŕ����z�����i�}�X�^.�W�����i
                            TextJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                        End If

                    Else
                        '**************************************************
                        ' ���ڐ���
                        '**************************************************
                        '�H���X(A)
                        '������(A)

                        syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                        If Not syouhinRec Is Nothing Then
                            '�H���X�����Ŕ����z�����i�}�X�^.�W�����i
                            TextKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                            '�������Ŕ����z�����.�H���X�����Ŕ����z
                            TextJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                            HiddenZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪
                        End If

                    End If

                ElseIf TextSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '������

                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    If record Is Nothing Then '�f�[�^�擾�ł��Ȃ������ꍇ
                        SelectSeikyuuUmu.SelectedValue = "" '�擾NG(�����L���̃N���A)

                        ClearControl() '�󔒃N���A
                    End If

                    '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If cl.getKeiretuFlg(record.KeiretuCd) Then '3�n��
                        '**************************************************
                        ' �������i3�n��j
                        '**************************************************
                        '�H���X(A)
                        '������(B)

                        syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                        If Not syouhinRec Is Nothing Then
                            HiddenZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                            '�H���X�����Ŕ����z�����i�}�X�^.�W�����i
                            TextKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                            '�����.�H���X�����Ŕ����z��0 �̏ꍇ�� 0 �Œ�
                            If TextKoumutenSeikyuuKingaku.Text = "0" Then
                                TextJituseikyuuKingaku.Text = "0" '�������Ŕ����z

                            Else
                                '**************************************************
                                ' �������i3�n��j
                                '**************************************************
                                Dim zeinukiGaku As Integer = 0

                                If JibanLogic.GetSeikyuuGaku(sender, _
                                                              3, _
                                                              record.KeiretuCd, _
                                                              TextItemCd.Text, _
                                                              syouhinRec.HyoujunKkk, _
                                                              zeinukiGaku) Then
                                    ' ���������z�փZ�b�g
                                    TextJituseikyuuKingaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                                End If

                            End If

                        End If

                    Else '3�n��ȊO

                        '�H���X(B)
                        '������(C)

                        '**************************************************
                        ' �������i3�n��ȊO�j
                        '**************************************************
                        ' �H���X�����z�͂O
                        TextKoumutenSeikyuuKingaku.Text = "0"

                        '�������Ŕ����z�̎����ݒ�
                        syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                        If Not syouhinRec Is Nothing Then
                            HiddenZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                            '�������Ŕ����z�����i�}�X�^.�W�����i
                            TextJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        End If

                    End If

                End If

                SetKingaku() '���z�̍Čv�Z

            End If

        End If

        '��ʐ���
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' �����񍐏������E���i�e�[�u�����z�G���A��0�N���A
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Clear0SyouhinTable()
        '���z��0�N���A
        '�������ݒ�
        TextKoumutenSeikyuuKingaku.Text = "0" '�H���X�������z
        TextJituseikyuuKingaku.Text = "0" '���������z
        TextSyouhizei.Text = "0" '�����
        TextZeikomiKingaku.Text = "0" '�ō����z
        TextSeikyuusyoHakkouDate.Text = "" '���������s��
        TextUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) '����N����
        '�������m��
        If TextHattyuusyoKakutei.Text.Length = 0 Then '(*5)�������m�肪�󔒂̏ꍇ�́A�u0�F���m��v��ݒ肷��
            TextHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If

    End Sub

    ''' <summary>
    ''' �����񍐏������E���i�e�[�u�����z�G���A�̋󔒃N���A
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearControl()
        '�������ݒ�(�󔒃N���A)
        SelectSeikyuuUmu.SelectedValue = "" '�����L��
        SpanSeikyuUmu.Style.Add("display", "none") 'SPAN�����L��
        SpanSeikyuUmu.InnerHtml = "" 'SPAN�����L��

        TextItemCd.Text = "" '���i�R�[�h
        SpanItemMei.InnerHtml = "" '���i��
        TextKoumutenSeikyuuKingaku.Text = "" '�H���X�������z
        TextJituseikyuuKingaku.Text = "" '���������z
        HiddenSiireGaku.Value = ""        '�d����z
        TextSyouhizei.Text = "" '�����
        HiddenSiireSyouhiZei.Value = ""   '�d�������
        TextZeikomiKingaku.Text = "" '�ō����z
        TextSeikyuusyoHakkouDate.Text = "" '���������s��
        TextUriageNengappi.Text = "" '����N����
        TextHattyuusyoKakutei.Text = "" '�������m��

        SetKingaku() '���z�̍Čv�Z

    End Sub

    ''' <summary>
    '''���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
    '''�ύX����Ă���ꍇ���̍Ď擾
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)
        '������A�d���悪�ύX���ꂽ�s���`�F�b�N���A���݂����ꍇ��
        '�e�s�̐����L���ύX���̏��������s����

        '**************************
        ' �����񍐏�
        '**************************
        '�����d���ύX�����NUC���̃`�F�b�N�t���OHidden���Q�Ƃ��A�t���O�������Ă���ꍇ�͏������{
        If Me.ucSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '�����L���ύX������
            Me.SelectSeikyuuUmu_SelectedIndexChanged(sender, e)

            '�t���O������
            Me.ucSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            '�t�H�[�J�X�͐����d���ύX�����N
            setFocusAJ(Me.ucSeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)

            '�ύX���ꂽ���i���L�����ꍇ�AUpdatePanel��Update
            Me.UpdatePanelSyouhinInfo.Update()

        End If

    End Sub

    ''' <summary>
    ''' ���i�̊�{�����Z�b�g����(�����񍐏�)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfo()
        Dim syouhinRec As Syouhin23Record

        '���i�R�[�h/���i���̎����ݒ聣
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If syouhinRec Is Nothing Then
            TextItemCd.Text = "" '���i�R�[�h
            SpanItemMei.InnerHtml = "" '���i��
        Else
            TextItemCd.Text = cl.GetDispStr(syouhinRec.SyouhinCd) '���i�R�[�h
            SpanItemMei.InnerHtml = cl.GetDispStr(syouhinRec.SyouhinMei) '���i��
            HiddenZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

            '��ʏ�Ő����悪�w�肳��Ă���ꍇ�A���R�[�h�̐�������㏑��
            If Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value <> String.Empty Then
                '����������R�[�h�ɃZ�b�g
                syouhinRec.SeikyuuSakiCd = Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value
                syouhinRec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
                syouhinRec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value
            End If
            Me.TextSeikyuusaki.Text = syouhinRec.SeikyuuSakiType '��������

        End If

    End Sub

#End Region

#Region "��ʐ���"

    ''' <summary>
    ''' �R���g���[���̊���������
    ''' </summary>
    ''' <remarks>�R���g���[���̊�����������d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControl()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        htNotTarget.Add(TextSeikyuusaki.ID, TextSeikyuusaki) '������
        htNotTarget.Add(TextHattyuusyoKingaku.ID, TextHattyuusyoKingaku) '���������z
        htNotTarget.Add(TextHattyuusyoKakutei.ID, TextHattyuusyoKakutei) '�������m��
        jSM.Hash2Ctrl(TdHassouDate, EarthConst.MODE_VIEW, ht) '������
        jSM.Hash2Ctrl(TdSaihakkouDate, EarthConst.MODE_VIEW, ht) '�Ĕ��s��
        jSM.Hash2Ctrl(TrSyouhin, EarthConst.MODE_VIEW, ht, htNotTarget) '���i�e�[�u��
        jSM.Hash2Ctrl(TdSaihakkouRiyuu, EarthConst.MODE_VIEW, ht) '�Ĕ��s���R

        '���D�揇1
        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)
        If SpanUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '�ύX�ӏ��Ȃ��̂��߁A��ʂ���擾
            cl.chgDispSyouhinText(TextSaihakkouRiyuu) '�Ĕ��s���R

            '���D�揇2
        ElseIf TextHassouDate.Text.Length = 0 Then '�����񍐏���������������
            cl.chgDispSyouhinText(TextHassouDate) '������
        Else
            cl.chgDispSyouhinText(TextHassouDate) '������

            '���D�揇3
            If TextSaihakkouDate.Text.Length = 0 Then '�Ĕ��s����������
                cl.chgDispSyouhinText(TextSaihakkouDate) '�Ĕ��s��

                '���D�揇4,5
            ElseIf SelectSeikyuuUmu.SelectedValue = "0" Or SelectSeikyuuUmu.SelectedValue = "" Then '���� ��or��
                cl.chgDispSyouhinText(TextSaihakkouDate) '�Ĕ��s��
                cl.chgDispSyouhinPull(SelectSeikyuuUmu, SpanSeikyuUmu) '�����L��
                cl.chgDispSyouhinText(TextSaihakkouRiyuu) '�Ĕ��s���R

                '���D�揇6
            Else
                cl.chgDispSyouhinText(TextSaihakkouDate) '�Ĕ��s��
                cl.chgDispSyouhinPull(SelectSeikyuuUmu, SpanSeikyuUmu) '�����L��
                cl.chgDispSyouhinText(TextSaihakkouRiyuu) '�Ĕ��s���R

                Dim kameitenlogic As New KameitenSearchLogic
                Dim blnTorikesi As Boolean = False
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                If record.Torikesi <> 0 Then
                    record.KeiretuCd = ""
                End If

                If TextSeikyuusaki.Text = EarthConst.SEIKYU_TASETU And cl.getKeiretuFlg(record.KeiretuCd) = False Then

                    cl.chgDispSyouhinText(TextJituseikyuuKingaku) '�������Ŕ����z
                    cl.chgDispSyouhinText(TextSeikyuusyoHakkouDate) '���������s��

                    '���D�揇7
                Else
                    cl.chgDispSyouhinText(TextJituseikyuuKingaku) '�������Ŕ����z
                    cl.chgDispSyouhinText(TextSeikyuusyoHakkouDate) '���������s��
                    cl.chgDispSyouhinText(TextKoumutenSeikyuuKingaku) '�H���X�����Ŕ����z
                End If

            End If

        End If

        '�󗝕ύX������
        If SelectJuri.SelectedValue = "2" Or SelectJuri.SelectedValue = "3" Then
            jSM.Hash2Ctrl(TdHassouDate, EarthConst.MODE_VIEW, ht) '������
            jSM.Hash2Ctrl(TdSaihakkouDate, EarthConst.MODE_VIEW, ht) '�Ĕ��s��
        End If

        '�������{�������͎�����
        If TextTyousaJissiDate.Text = "" Then
            jSM.Hash2Ctrl(TdHantei, EarthConst.MODE_VIEW, ht) '����֘A
            SetButtonSearchDisp()

        End If


    End Sub

#End Region

End Class