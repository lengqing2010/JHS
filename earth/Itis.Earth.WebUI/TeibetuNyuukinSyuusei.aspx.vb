
Partial Public Class TeibetuNyuukinSyuusei
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����o�ϐ�"
    ''' <summary>
    ''' ���ʏ����N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim ComLog As New CommonLogic

    Dim user_info As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim sysdate As DateTime
    '�}�X�^�[�y�[�W��MasterContentPlaceHolder���p�̂���
    Dim MasterCPH1 As System.Web.UI.WebControls.ContentPlaceHolder
    Dim cbLogic As New CommonBizLogic

#End Region

#Region "�����o�萔"
    Private Const PATH_USR_CTRL_TOP As String = "control/TeibetuNyuukinRecordCtrlTop.ascx"
    Private Const PATH_USR_CTRL_ROW As String = "control/TeibetuNyuukinRecordCtrl.ascx"
    Private Const SW_ON As String = "1"
    Private Const SW_OFF As String = "0"
    
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

        '�}�X�^�[�y�[�W��MasterContentPlaceHolder���擾
        Dim myMaster As EarthMasterPage = Page.Master
        MasterCPH1 = myMaster.MasterContentPlaceHolder

        ' ���[�U�[��{�F��
        jBn.UserAuth(user_info)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If user_info IsNot Nothing Then
            ' �o���Ɩ�������ݒ�
            HiddenKeiriGyoumuKengen.Value = user_info.KeiriGyoumuKengen.ToString()
        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        '���񖾍׃e�[�u�����N���A
        Me.tblMeisai.Controls.Clear()

        If IsPostBack = False Then

            ' �p�����[�^�G���[�L��
            Dim isError As Boolean = False

            ' Key����ێ�
            _kbn = Request("kbn")
            _no = Request("no")

            TextKubun.Text = _kbn
            TextBangou.Text = _no

            ' �p�����[�^�s�����͉�ʂ�\�����Ȃ�
            If _kbn Is Nothing Or _kbn = String.Empty Or _
               _no Is Nothing Or _no = String.Empty Then
                ' �G���[�L��
                isError = True
            Else
                Dim logic As New JibanLogic
                '�n�Ճf�[�^�����݂��Ȃ��ꍇ
                If logic.ExistsJibanData(_kbn, _no) = False Then
                    ' ���݂��Ȃ�
                    isError = True
                End If
            End If

            ' �p�����[�^�s�����̓��j���[��ʂ֑J�ڂ���
            If isError Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            setDispAction()

            ' �n�Ճf�[�^����ʂɐݒ肷��
            SetJibanData()
        Else
            '��ʍ��ڐݒ菈��(�|�X�g�o�b�N�p)
            setDisplayPostBack()

        End If
    End Sub

    ''' <summary>
    ''' ��񕥖ߕԋ��`�F�b�N�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CheckboxKaiyakuHaraimodosikin_CheckedChanged(ByVal sender As System.Object, _
                                                               ByVal e As System.EventArgs) _
                                                               Handles CheckboxKaiyakuHaraimodosikin.CheckedChanged
        ' ��񕥖ߐݒ�
        If CheckboxKaiyakuHaraimodosikin.Checked = True Then
            TextHenkinSyoriDate.Enabled = True
            TextHenkinSyoriDate.Text = ComLog.GetDisplayString(DateTime.Now)
        Else
            TextHenkinSyoriDate.Enabled = False
            TextHenkinSyoriDate.Text = String.Empty
        End If
        Me.HiddenKaiyakuSyori.Value = SW_ON
    End Sub

    ''' <summary>
    ''' �o�^�^�C�����s�{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTourokuExe_ServerClick(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles ButtonTourokuExe.ServerClick
        ' ��ʂ̓��e��DB�ɔ��f����
        SaveData(sender)
    End Sub

    ''' <summary>
    ''' �y�[�W���[�h�R���v���[�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim recCtrlTop As TeibetuNyuukinRecordCtrlTop

        If Me.HiddenKaiyakuSyori.Value <> SW_ON Then
            '��񕥖߂̓����z����ѕԋ��z���ύX���ꂽ���̏���
            recCtrlTop = Me.tblMeisai.Controls(Me.tblMeisai.Controls.Count - 1)
            If Me.TrKaiyakuMeisai.Visible = True And recCtrlTop.AccHiddenZangaku.Value = "0" Then
                recCtrlTop.AccHiddenZangaku.Value = String.Empty
                CheckboxKaiyakuHaraimodosikin.Checked = True
                Me.UpdatePanelKaiyakuCheck.Update()
                Me.TextHenkinSyoriDate.Enabled = True
                If Me.TextHenkinSyoriDate.Text = String.Empty Then
                    Me.TextHenkinSyoriDate.Text = DateTime.Now.ToString("yyyy/MM/dd")
                    Me.UpdatePanelKaiyaku.Update()
                End If
            End If
        End If
        Me.HiddenKaiyakuSyori.Value = String.Empty
    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"
#Region "�y�[�W���[�h"
#Region "��������"
    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '�o�^/�C�����s�{�^���������̃C�x���g�n���h��
        Dim tmpScript = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);objEBI('" & ButtonTourokuExe.ClientID & "').click();}else{return false;}"
        ButtonTourokuSyuuseiJikkou1.Attributes("onclick") = tmpScript
        ButtonTourokuSyuuseiJikkou2.Attributes("onclick") = tmpScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �C�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '���ʑΉ�
        ComLog.getTokubetuTaiouLinkPath(Me.ButtonTokubetuTaiou, _
                                     user_info, _
                                     Me.TextKubun.ClientID, _
                                     Me.TextBangou.ClientID, _
                                     "", _
                                     "", _
                                     "")

        '��������\���{�^��
        ButtonBukkenRireki.Attributes("onclick") = "callSearch('" & TextKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.POPUP_BUKKEN_RIREKI & "','','');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������R�e�L�X�g�{�b�N�X�̃X�^�C����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ComLog.chgVeiwMode(Me.TextTorikesiRiyuu)

    End Sub

#End Region

#Region "��ʕ`��"
    ''' <summary>
    ''' �n�Ճf�[�^����ʂɐݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJibanData()

        Dim jLogic As New JibanLogic
        Dim nLogic As New TeibetuNyuukinLogic
        Dim record As JibanRecord
        Dim listNyuukinRec As List(Of TeibetuNyuukinRecord)

        ' �ēǂݍ��ݗp
        If _kbn = String.Empty Or _kbn Is Nothing Then
            _kbn = TextKubun.Text
        End If
        If _no = String.Empty Or _no Is Nothing Then
            _no = TextBangou.Text
        End If

        ' �n�Ճf�[�^���擾����
        record = jLogic.GetJibanData(_kbn, _no)

        ' �n�Ճf�[�^�Ȃ��͉�ʂ�\�����Ȃ�
        If record Is Nothing Then
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '�@�ʓ����f�[�^���擾����
        listNyuukinRec = nLogic.GetTeibetuSeikyuuNyuukinData(_kbn, _no)

        ' �n�Ճf�[�^�Ȃ��͉�ʂ�\�����Ȃ�
        If listNyuukinRec Is Nothing OrElse listNyuukinRec.Count = 0 Then
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '�n�Ճe�[�u��.�X�V�҂��烍�O�C�����[�UID�A�X�V�������擾
        CommonLogic.Instance.SetKousinsya(record.Kousinsya, TextSaisyuuKousinnsya.Text, TextSaisyuuKousinNitiji.Text)

        ' �R���g���[���փf�[�^��ݒ�
        TextKubun.Text = record.Kbn
        TextBangou.Text = record.HosyousyoNo
        TextSetunusiMei.Text = record.SesyuMei
        TextBukkenJyuusyo1.Text = record.BukkenJyuusyo1
        TextBukkenJyuusyo2.Text = record.BukkenJyuusyo2
        TextBukkenJyuusyo3.Text = record.BukkenJyuusyo3

        HiddenUpdDatetime.Value = record.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)

        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim kameitenData As New KameitenSearchRecord

        ' �����X�����擾
        kameitenData = kameitenSearchLogic.GetKameitenSearchResult(record.Kbn, _
                                                                   record.KameitenCd, _
                                                                   String.Empty, _
                                                                   False)

        If Not kameitenData Is Nothing Then
            TextKameitenCd.Text = record.KameitenCd
            TextKameitenMei.Text = kameitenData.KameitenMei1
            TextKeiretuCd.Text = kameitenData.KeiretuCd
            TextKeiretuMei.Text = kameitenData.KeiretuMei
            TextEigyousyoCd.Text = kameitenData.EigyousyoCd
            TextEigyousyoMei.Text = kameitenData.EigyousyoMei
        End If

        '�����X������R�ݒ�
        setTorikesiRiyuu(TextKubun.Text, TextKameitenCd.Text)

        ' ���t�`�F�b�N
        Dim checkDate As String = "checkDate(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ���V�X�e���ւ̃����N�{�^���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�ۏ؏�DB
        ComLog.getHosyousyoDbFilePath(TextKubun.Text, TextBangou.Text, ButtonHosyousyoDB)

        '�����X���ӎ���
        ComLog.getKameitenTyuuijouhouPath(TextKameitenCd.ClientID, ButtonKameitenTyuuijouhou)

        'onblur�ݒ�
        TextHenkinSyoriDate.Attributes("onblur") = checkDate

        '��ʂɓ@�ʖ���(�����^����)�f�[�^��ݒ�
        SetTeibetuData(record, listNyuukinRec)

        ' ��񕥖ߐݒ�
        TextHenkinSyoriDate.Text = IIf(record.HenkinSyoriDate = DateTime.MinValue, String.Empty, record.HenkinSyoriDate.ToString("yyyy/MM/dd"))
        If record.HenkinSyoriFlg = 1 Then
            CheckboxKaiyakuHaraimodosikin.Checked = True
            TextHenkinSyoriDate.Enabled = True
        Else
            CheckboxKaiyakuHaraimodosikin.Checked = False
            TextHenkinSyoriDate.Enabled = False
        End If

        ' ��񃌃R�[�h�����̏ꍇ�A��񍀖ڂ��\���ɂ���
        If Me.HiddenKaiyakuNashiFlg.Value = "1" Then
            TrKaiyakuHeader.Visible = False
            TrKaiyakuMeisai.Visible = False
        End If

    End Sub

#End Region

#Region "DB�����ʂɃf�[�^���Z�b�g"
    ''' <summary>
    ''' �n�Ճ��R�[�h���e��@�ʃ��R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="rec">�n�Ճ��R�[�h</param>
    ''' <param name="listNyuukinRec">�@�ʓ������R�[�h�N���X�̃��X�g</param>
    ''' <remarks></remarks>
    Private Sub SetTeibetuData(ByVal rec As JibanRecord, ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord))
        Dim logic As New TeibetuNyuukinLogic
        Dim dicBunruiCnt As Dictionary(Of String, Integer)
        Dim intBunruiCd As Integer
        Dim intBunruiCnt As Integer

        dicBunruiCnt = GetBunruiCnt(listNyuukinRec)

        '���i�P
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Syouhin1
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '���i�Q
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Syouhin2_110
        intBunruiCnt = dicBunruiCnt(intBunruiCd)
        Me.HiddenSyouhin2Cnt.Value = SetTableRecords(rec, listNyuukinRec, intBunruiCd, intBunruiCnt, rec.Syouhin2Records)

        '���i�R
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Syouhin3
        intBunruiCnt = dicBunruiCnt(intBunruiCd)
        Me.HiddenSyouhin3Cnt.Value = SetTableRecords(rec, listNyuukinRec, intBunruiCd, intBunruiCnt, rec.Syouhin3Records)

        '���i�S
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Syouhin4
        intBunruiCnt = dicBunruiCnt(intBunruiCd)
        Me.HiddenSyouhin4Cnt.Value = SetTableRecords(rec, listNyuukinRec, intBunruiCd, intBunruiCnt, rec.Syouhin4Records)

        '���ǍH��
        intBunruiCd = EarthEnum.EnumSyouhinKubun.KairyouKouji
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '�ǉ��H��
        intBunruiCd = EarthEnum.EnumSyouhinKubun.TuikaKouji
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '�����񍐏�
        intBunruiCd = EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '�H���񍐏�
        intBunruiCd = EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '�ۏ؏�
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Hosyousyo
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

        '��񕥖�
        intBunruiCd = EarthEnum.EnumSyouhinKubun.Kaiyaku
        SetTableRecord(rec, listNyuukinRec, intBunruiCd)

    End Sub

    ''' <summary>
    ''' ���ނ��Ƃ̌������擾
    ''' </summary>
    ''' <param name="listNyuukinRec"></param>
    ''' <returns>���ނ��Ƃ̌������i�[����Dictionary</returns>
    ''' <remarks></remarks>
    Private Function GetBunruiCnt(ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord)) As Dictionary(Of String, Integer)
        Dim intRecCnt As Integer = 0
        Dim dicBunruiCnt As New Dictionary(Of String, Integer)

        dicBunruiCnt.Add(EarthConst.SOUKO_CD_SYOUHIN_2_110, 0)
        dicBunruiCnt.Add(EarthConst.SOUKO_CD_SYOUHIN_3, 0)
        dicBunruiCnt.Add(EarthConst.SOUKO_CD_SYOUHIN_4, 0)

        For Each rec As TeibetuNyuukinRecord In listNyuukinRec
            If rec.BunruiCd IsNot Nothing AndAlso rec.BunruiCd <> String.Empty Then
                Select Case rec.BunruiCd.Substring(0, 2)
                    Case EarthConst.SOUKO_CD_SYOUHIN_2_110.Substring(0, 2)
                        dicBunruiCnt(EarthConst.SOUKO_CD_SYOUHIN_2_110) += 1
                    Case EarthConst.SOUKO_CD_SYOUHIN_3.Substring(0, 2)
                        dicBunruiCnt(EarthConst.SOUKO_CD_SYOUHIN_3) += 1
                    Case EarthConst.SOUKO_CD_SYOUHIN_4.Substring(0, 2)
                        dicBunruiCnt(EarthConst.SOUKO_CD_SYOUHIN_4) += 1
                End Select
            End If
        Next

        Return dicBunruiCnt

    End Function

    ''' <summary>
    ''' ��ʂ̃e�[�u���ɐ������Ɠ��������Z�b�g����
    ''' </summary>
    ''' <param name="rec">�n�Ճ��R�[�h</param>
    ''' <param name="listNyuukinRec">�@�ʓ������R�[�h�N���X�̃��X�g</param>
    ''' <param name="enSyouhinType">���i�敪���</param>
    ''' <remarks></remarks>
    Private Sub SetTableRecord(ByVal rec As JibanRecord _
                                , ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord) _
                                , ByVal enSyouhinType As EarthEnum.EnumSyouhinKubun)
        Dim strSyouhinId As String
        Dim strBunruiName As String
        Dim strBunruiCd As String
        Dim recSyouhin As New TeibetuSeikyuuRecord
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        Dim recNyuukin As TeibetuNyuukinRecord
        Dim intCntNyuukinRec As Integer = 0

        Select Case enSyouhinType
            Case EarthEnum.EnumSyouhinKubun.Syouhin1
                '���i1
                strSyouhinId = EarthConst.USR_CTRL_ID_ITEM1
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_ITEM1
                strBunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1
                recSyouhin = rec.Syouhin1Record
            Case EarthEnum.EnumSyouhinKubun.KairyouKouji
                '���ǍH��
                strSyouhinId = EarthConst.USR_CTRL_ID_K_KOUJI
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_K_KOUJI
                strBunruiCd = EarthConst.SOUKO_CD_KAIRYOU_KOUJI
                recSyouhin = rec.KairyouKoujiRecord
            Case EarthEnum.EnumSyouhinKubun.TuikaKouji
                '�ǉ��H��
                strSyouhinId = EarthConst.USR_CTRL_ID_T_KOUJI
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_T_KOUJI
                strBunruiCd = EarthConst.SOUKO_CD_TUIKA_KOUJI
                recSyouhin = rec.TuikaKoujiRecord
            Case EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo
                '�����񍐏�
                strSyouhinId = EarthConst.USR_CTRL_ID_T_HOUKOKU
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_T_HOUKOKU
                strBunruiCd = EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO
                recSyouhin = rec.TyousaHoukokusyoRecord
            Case EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo
                '�H���񍐏�
                strSyouhinId = EarthConst.USR_CTRL_ID_K_HOUKOKU
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_K_HOUKOKU
                strBunruiCd = EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO
                recSyouhin = rec.KoujiHoukokusyoRecord
            Case EarthEnum.EnumSyouhinKubun.Hosyousyo
                '�ۏ؏�
                strSyouhinId = EarthConst.USR_CTRL_ID_HOSYOUSYO
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_HOSYOUSYO
                strBunruiCd = EarthConst.SOUKO_CD_HOSYOUSYO
                recSyouhin = rec.HosyousyoRecord
            Case EarthEnum.EnumSyouhinKubun.Kaiyaku
                '��񕥖�
                strSyouhinId = EarthConst.USR_CTRL_ID_KAIYAKU
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_KAIYAKU
                strBunruiCd = EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI
                recSyouhin = rec.KaiyakuHaraimodosiRecord
            Case Else
                ctrlRecTop = New TeibetuNyuukinRecordCtrlTop
                '�^�C�g����ݒ�
                ctrlRecTop.AccLblTitle.Text = String.Empty
                '�e�[�u���ɖ��׍s����s�ǉ�
                Me.tblMeisai.Controls.Add(ctrlRecTop)
                Exit Sub
        End Select

        intCntNyuukinRec = GetBunruiStartIndex(listNyuukinRec, strBunruiCd)

        '���[�U�R���g���[���̓Ǎ�
        ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
        If intCntNyuukinRec < listNyuukinRec.Count Then
            '���i1
            With ctrlRecTop
                'ID�t�^
                .ID = strSyouhinId
                '�^�C�g����ݒ�
                .AccLblTitle.Text = strBunruiName

                If listNyuukinRec(intCntNyuukinRec) IsNot Nothing Then
                    recNyuukin = listNyuukinRec(intCntNyuukinRec)
                    intCntNyuukinRec += 1
                Else
                    recNyuukin = Nothing
                End If

                SetSyouhinRec(ctrlRecTop, recSyouhin, recNyuukin)

                '�e�[�u���ɖ��׍s����s�ǉ�
                Me.tblMeisai.Controls.Add(ctrlRecTop)
            End With
        Else
            If enSyouhinType = EarthEnum.EnumSyouhinKubun.Kaiyaku Then
                '��񕥖߂̓��R�[�h���Ȃ��ꍇ�t���O�Z�b�g
                Me.HiddenKaiyakuNashiFlg.Value = SW_ON
            End If
            '���[�U�R���g���[���̓Ǎ�
            ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
            'ID�t�^
            ctrlRecTop.ID = strSyouhinId
            '�^�C�g����ݒ�
            ctrlRecTop.AccLblTitle.Text = strBunruiName
            'DB�Ƀ��R�[�h���Ȃ��ꍇ�͔�\��
            If ctrlRecTop.AccTextSyouhinCd.Text = String.Empty Then
                ctrlRecTop.AccTextNyuukinKinGaku.Text = String.Empty
                ctrlRecTop.AccTextNyuukinKinGaku.Style("display") = "none"
                ctrlRecTop.AccTextHenkinGaku.Text = String.Empty
                ctrlRecTop.AccTextHenkinGaku.Style("display") = "none"
            End If
            '�e�[�u���ɖ��׍s����s�ǉ�
            Me.tblMeisai.Controls.Add(ctrlRecTop)
        End If
    End Sub

    ''' <summary>
    ''' ��ʂ̃e�[�u���ɐ������Ɠ��������Z�b�g����
    ''' </summary>
    ''' <param name="rec">�n�Ճ��R�[�h</param>
    ''' <param name="listNyuukinRec">�@�ʓ������R�[�h�N���X�̃��X�g</param>
    ''' <param name="enSyouhinType">���i�敪���</param>
    ''' <param name="intBunruiCnt">���ނ��Ƃ̌���</param>
    ''' <param name="dicSyouhinRecs">�@�ʐ������R�[�hDictionary</param>
    ''' <returns>���[�v����</returns>
    ''' <remarks></remarks>
    Private Function SetTableRecords(ByVal rec As JibanRecord _
                                    , ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord) _
                                    , ByVal enSyouhinType As EarthEnum.EnumSyouhinKubun _
                                    , ByVal intBunruiCnt As Integer _
                                    , ByVal dicSyouhinRecs As Dictionary(Of Integer, TeibetuSeikyuuRecord)) As Integer
        Dim strSyouhinId As String
        Dim strBunruiName As String
        Dim strBunruiCd As String
        Dim intCntNyuukinRec As Integer = 0
        Dim intLoopCnt As Integer = 0
        Dim recSyouhin As TeibetuSeikyuuRecord
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        Dim ctrlRec As TeibetuNyuukinRecordCtrl
        Dim recNyuukin As TeibetuNyuukinRecord

        Select Case enSyouhinType
            Case EarthEnum.EnumSyouhinKubun.Syouhin2_110, EarthEnum.EnumSyouhinKubun.Syouhin2_115
                '���i2
                strSyouhinId = EarthConst.USR_CTRL_ID_ITEM2
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_ITEM2
                strBunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_110
            Case EarthEnum.EnumSyouhinKubun.Syouhin3
                '���i3
                strSyouhinId = EarthConst.USR_CTRL_ID_ITEM3
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_ITEM3
                strBunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3
            Case EarthEnum.EnumSyouhinKubun.Syouhin4
                '���i4
                strSyouhinId = EarthConst.USR_CTRL_ID_ITEM4
                strBunruiName = EarthConst.ITEM_BUNRUI_NAME_ITEM4
                strBunruiCd = EarthConst.SOUKO_CD_SYOUHIN_4
            Case Else
                ctrlRecTop = New TeibetuNyuukinRecordCtrlTop
                '�^�C�g����ݒ�
                ctrlRecTop.AccLblTitle.Text = String.Empty
                '�e�[�u���ɖ��׍s����s�ǉ�
                Me.tblMeisai.Controls.Add(ctrlRecTop)
                Exit Function
        End Select

        If listNyuukinRec IsNot Nothing AndAlso listNyuukinRec.Count > 0 Then

            intCntNyuukinRec = GetBunruiStartIndex(listNyuukinRec, strBunruiCd)

            While intCntNyuukinRec < listNyuukinRec.Count AndAlso listNyuukinRec(intCntNyuukinRec).BunruiCd.Substring(0, 2) = strBunruiCd.Substring(0, 2)
                '���[�v�J�E���^�̃J�E���g�A�b�v
                intLoopCnt += 1

                '�@�ʓ������R�[�h�̎擾
                recNyuukin = listNyuukinRec(intCntNyuukinRec)

                '�@�ʐ������R�[�h�̎擾
                If dicSyouhinRecs.ContainsKey(recNyuukin.GamenHyoujiNo) Then
                    recSyouhin = dicSyouhinRecs(recNyuukin.GamenHyoujiNo)
                Else
                    recSyouhin = Nothing
                End If

                If intLoopCnt = 1 Then
                    '���[�U�R���g���[���̓Ǎ�
                    ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
                    'ID�t�^
                    ctrlRecTop.ID = strSyouhinId & intLoopCnt

                    '�^�C�g����ݒ�
                    ctrlRecTop.AccLblTitle.Text = strBunruiName
                    ctrlRecTop.AccTdTitle.Attributes("rowspan") = intBunruiCnt

                    '���i�s���[�U�[�R���g���[����DB�̒l���Z�b�g
                    SetSyouhinRec(ctrlRecTop, recSyouhin, recNyuukin)

                    '�e�[�u���ɖ��׍s����s�ǉ�
                    Me.tblMeisai.Controls.Add(ctrlRecTop)
                Else
                    '���[�U�R���g���[���̓Ǎ�
                    ctrlRec = Me.LoadControl(PATH_USR_CTRL_ROW)
                    'ID�t�^
                    ctrlRec.ID = strSyouhinId & intLoopCnt

                    '���i�s���[�U�[�R���g���[����DB�̒l���Z�b�g
                    SetSyouhinRec(ctrlRec, recSyouhin, recNyuukin)

                    ''DB�Ƀ��R�[�h���Ȃ��ꍇ�͔�\��
                    If ctrlRec.AccTextSyouhinCd.Text = String.Empty _
                    And ctrlRec.AccTextNyuukinKinGaku.Text = String.Empty _
                    And ctrlRec.AccTextHenkinGaku.Text = String.Empty Then
                        ctrlRec.AccTextNyuukinKinGaku.Text = String.Empty
                        ctrlRec.AccTextNyuukinKinGaku.Style("display") = "none"
                        ctrlRec.AccTextHenkinGaku.Text = String.Empty
                        ctrlRec.AccTextHenkinGaku.Style("display") = "none"
                    End If

                    '�s�̐F�ݒ�
                    If intLoopCnt Mod 2 = 0 Then
                        ctrlRec.AccTrRecord.Attributes.Add("class", "even")
                    End If

                    '�e�[�u���ɖ��׍s����s�ǉ�
                    Me.tblMeisai.Controls.Add(ctrlRec)
                End If

                intCntNyuukinRec += 1

            End While
        End If

        If listNyuukinRec Is Nothing OrElse listNyuukinRec.Count = 0 OrElse intLoopCnt = 0 Then
            '���[�U�R���g���[���̓Ǎ�
            ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
            'ID�t�^
            ctrlRecTop.ID = strSyouhinId & 1
            '�^�C�g����ݒ�
            ctrlRecTop.AccLblTitle.Text = strBunruiName
            'DB�Ƀ��R�[�h���Ȃ��ꍇ�͔�\��
            If ctrlRecTop.AccTextSyouhinCd.Text = String.Empty Then
                ctrlRecTop.AccTextNyuukinKinGaku.Text = String.Empty
                ctrlRecTop.AccTextNyuukinKinGaku.Style("display") = "none"
                ctrlRecTop.AccTextHenkinGaku.Text = String.Empty
                ctrlRecTop.AccTextHenkinGaku.Style("display") = "none"
            End If
            '�e�[�u���ɖ��׍s����s�ǉ�
            Me.tblMeisai.Controls.Add(ctrlRecTop)
        End If

        Return intLoopCnt
    End Function

    ''' <summary>
    ''' �@�ʓ������R�[�h�N���X�̃��X�g���ŕ��ނ��Ƃ̊J�nIndex���擾���܂�
    ''' </summary>
    ''' <param name="listNyuukinRec">�@�ʓ������R�[�h�N���X�̃��X�g</param>
    ''' <param name="strBunruiCd">���ރR�[�h</param>
    ''' <returns>���ރR�[�h���Ƃ̊J�nIndex</returns>
    ''' <remarks></remarks>
    Private Function GetBunruiStartIndex(ByVal listNyuukinRec As List(Of TeibetuNyuukinRecord), ByVal strBunruiCd As String)
        Dim intIndex As Integer
        For intIndex = 0 To listNyuukinRec.Count - 1
            If listNyuukinRec(intIndex).BunruiCd IsNot Nothing AndAlso listNyuukinRec(intIndex).BunruiCd <> String.Empty Then
                If listNyuukinRec(intIndex).BunruiCd.Substring(0, 2) = strBunruiCd.Substring(0, 2) Then
                    Exit For
                End If
            End If
        Next

        Return intIndex

    End Function

    ''' <summary>
    ''' ������R�̐ݒ�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Private Sub setTorikesiRiyuu(ByVal strKbn As String, ByVal strKameitenCd As String)

        '�F�ւ������Ώۂ̃R���g���[����z��Ɋi�[(��������R�e�L�X�g�{�b�N�X�ȊO)
        Dim objArray() As Object = New Object() {Me.TextKameitenCd, Me.TextKameitenMei}

        '������R�Ɖ����X���̕����F�ݒ�
        ComLog.GetKameitenTorikesiRiyuu(strKbn _
                                        , strKameitenCd _
                                        , Me.TextTorikesiRiyuu _
                                        , True _
                                        , False _
                                        , objArray)

    End Sub

#End Region

#Region "���i�s���[�U�[�R���g���[���̒l�Z�b�g"
    ''' <summary>
    ''' ���i�s���[�U�[�R���g���[���̒l�Z�b�g
    ''' </summary>
    ''' <param name="ctrlRecTop">���i�s���[�U�[�R���g���[��</param>
    ''' <param name="recSeikyuu">�@�ʐ������R�[�h</param>
    ''' <param name="recNyuukin">�@�ʓ������R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetSyouhinRec(ByVal ctrlRecTop As TeibetuNyuukinRecordCtrlTop _
                            , ByVal recSeikyuu As TeibetuSeikyuuRecord _
                            , ByVal recNyuukin As TeibetuNyuukinRecord)
        With ctrlRecTop
            '�������
            SetSeikyuuRecord(.AccTextSyouhinCd, .AccTextSyouhinMei, .AccTextSeikyuuKingGaku, recSeikyuu)
            '�������
            SetNyuukinRecord(.AccTextNyuukinKinGaku, .AccTextHenkinGaku, .AccHiddenBunruiCd, .AccHiddenGamenHyoujiNo, .AccHiddenUpdDatetime, recNyuukin)
        End With
    End Sub

    ''' <summary>
    ''' ���i�s���[�U�[�R���g���[���̒l�Z�b�g
    ''' </summary>
    ''' <param name="ctrlRec">���i�s���[�U�[�R���g���[��</param>
    ''' <param name="recSeikyuu">�@�ʐ������R�[�h</param>
    ''' <param name="recNyuukin">�@�ʓ������R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetSyouhinRec(ByVal ctrlRec As TeibetuNyuukinRecordCtrl _
                            , ByVal recSeikyuu As TeibetuSeikyuuRecord _
                            , ByVal recNyuukin As TeibetuNyuukinRecord)
        With ctrlRec
            '�������
            SetSeikyuuRecord(.AccTextSyouhinCd, .AccTextSyouhinMei, .AccTextSeikyuuKingGaku, recSeikyuu)
            '�������
            SetNyuukinRecord(.AccTextNyuukinKinGaku, .AccTextHenkinGaku, .AccHiddenBunruiCd, .AccHiddenGamenHyoujiNo, .AccHiddenUpdDatetime, recNyuukin)
        End With
    End Sub

    ''' <summary>
    ''' �@�ʐ����f�[�^���R���g���[���ɃZ�b�g���܂�
    ''' </summary>
    ''' <param name="ctrlSyouhinCd">���i�R�[�h�e�L�X�g�{�b�N�X</param>
    ''' <param name="ctrlSyouhinMei">���i���e�L�X�g�{�b�N�X</param>
    ''' <param name="ctrlSeikyuuKingaku">�������z�e�L�X�g�{�b�N�X</param>
    ''' <param name="rec">�@�ʐ������R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuRecord(ByVal ctrlSyouhinCd As TextBox _
                                , ByVal ctrlSyouhinMei As TextBox _
                                , ByVal ctrlSeikyuuKingaku As TextBox _
                                , ByVal rec As TeibetuSeikyuuRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSeikyuuRecord" _
                                                    , ctrlSyouhinCd _
                                                    , ctrlSyouhinMei _
                                                    , ctrlSeikyuuKingaku _
                                                    , rec)
        If rec IsNot Nothing Then
            ctrlSyouhinCd.Text = rec.SyouhinCd
            ctrlSyouhinMei.Text = rec.SyouhinMei
            If ctrlSeikyuuKingaku IsNot Nothing Then
                ctrlSeikyuuKingaku.Text = rec.ZeikomiUriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            End If
        End If

    End Sub

    ''' <summary>
    ''' �@�ʓ����f�[�^���R���g���[���ɃZ�b�g���܂�
    ''' </summary>
    ''' <param name="ctrlNyuukinGaku">�������z�e�L�X�g�{�b�N�X</param>
    ''' <param name="ctrlHenkinGaku">�ԋ��z�e�L�X�g�{�b�N�X</param>
    ''' <param name="rec">�@�ʓ������R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetNyuukinRecord(ByVal ctrlNyuukinGaku As TextBox _
                                , ByVal ctrlHenkinGaku As TextBox _
                                , ByVal ctrlBunruiCd As HiddenField _
                                , ByVal ctrlGamenHyoujiNo As HiddenField _
                                , ByVal ctrlUpdDatetime As HiddenField _
                                , ByVal rec As TeibetuNyuukinRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetNyuukinRecord" _
                                                    , ctrlNyuukinGaku _
                                                    , ctrlHenkinGaku _
                                                    , ctrlBunruiCd _
                                                    , ctrlGamenHyoujiNo _
                                                    , rec)

        If rec IsNot Nothing Then
            ' �ō������z
            ctrlNyuukinGaku.Text = _
                IIf(rec.ZeikomiNyuukinGaku = Integer.MinValue, _
                    String.Empty, _
                    rec.ZeikomiNyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
            ' �ō��ԋ��z
            ctrlHenkinGaku.Text = _
                IIf(rec.ZeikomiHenkinGaku = Integer.MinValue, _
                    String.Empty, _
                    rec.ZeikomiHenkinGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
            ' ���ރR�[�h
            ctrlBunruiCd.Value = rec.BunruiCd
            ' ��ʕ\��NO
            ctrlGamenHyoujiNo.Value = rec.GamenHyoujiNo
            ' �X�V����
            ctrlUpdDatetime.Value = rec.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)

        End If

    End Sub

#End Region

#Region "��ʍĕ`��"
    ''' <summary>
    ''' ��ʍ��ڐݒ菈��(�|�X�g�o�b�N�p)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDisplayPostBack()
        Dim intCntSyouhin2 As Integer
        Dim intCntSyouhin3 As Integer
        Dim intCntSyouhin4 As Integer

        If Me.HiddenSyouhin2Cnt.Value <> String.Empty Then
            intCntSyouhin2 = Integer.Parse(Me.HiddenSyouhin2Cnt.Value)
        Else
            intCntSyouhin2 = 0
        End If
        If Me.HiddenSyouhin3Cnt.Value <> String.Empty Then
            intCntSyouhin3 = Integer.Parse(Me.HiddenSyouhin3Cnt.Value)
        Else
            intCntSyouhin3 = 0
        End If
        If Me.HiddenSyouhin4Cnt.Value <> String.Empty Then
            intCntSyouhin4 = Integer.Parse(Me.HiddenSyouhin4Cnt.Value)
        Else
            intCntSyouhin4 = 0
        End If

        '*************
        '* ���i�P
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_ITEM1)
        '*************
        '* ���i�Q
        '*************
        RemakeSyouinRecords(EarthConst.USR_CTRL_ID_ITEM2, intCntSyouhin2)
        '*************
        '* ���i�R
        '*************
        RemakeSyouinRecords(EarthConst.USR_CTRL_ID_ITEM3, intCntSyouhin3)
        '*************
        '* ���i�S
        '*************
        RemakeSyouinRecords(EarthConst.USR_CTRL_ID_ITEM4, intCntSyouhin4)
        '*************
        '* ���ǍH��
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_K_KOUJI)
        '*************
        '* �ǉ��H��
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_T_KOUJI)
        '*************
        '* �����񍐏�
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_T_HOUKOKU)
        '*************
        '* �H���񍐏�
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_K_HOUKOKU)
        '*************
        '* �ۏ؏�
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_HOSYOUSYO)
        '*************
        '* ��񕥖�
        '*************
        RemakeSyouhinRecord(EarthConst.USR_CTRL_ID_KAIYAKU)
        ' ��񃌃R�[�h�����̏ꍇ�A��񍀖ڂ��\���ɂ���
        If Me.HiddenKaiyakuNashiFlg.Value = "1" Then
            TrKaiyakuHeader.Visible = False
            TrKaiyakuMeisai.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' ���i�s���[�U�[�R���g���[���̍č쐬
    ''' </summary>
    ''' <param name="strRecId">���i�s���[�U�[�R���g���[��ID</param>
    ''' <remarks></remarks>
    Private Sub RemakeSyouhinRecord(ByVal strRecId As String)
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        '���[�U�R���g���[���̓Ǎ���ID�̕t�^
        ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
        ctrlRecTop.ID = strRecId
        '�e�[�u���ɖ��׍s����s�ǉ�
        Me.tblMeisai.Controls.Add(ctrlRecTop)
    End Sub

    ''' <summary>
    ''' ���i�s���[�U�[�R���g���[���̍č쐬
    ''' </summary>
    ''' <param name="strRecId">���i�s���[�U�[�R���g���[��ID</param>
    ''' <param name="intCntSyouhin">���i�s����</param>
    ''' <remarks></remarks>
    Private Sub RemakeSyouinRecords(ByVal strRecId As String, ByVal intCntSyouhin As Integer)
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        Dim ctrlRec As TeibetuNyuukinRecordCtrl
        Dim intLoopCnt As Integer

        '���[�U�R���g���[���̓Ǎ���ID�̕t�^
        ctrlRecTop = Me.LoadControl(PATH_USR_CTRL_TOP)
        ctrlRecTop.ID = strRecId & "1"
        '�e�[�u���ɖ��׍s����s�ǉ�
        Me.tblMeisai.Controls.Add(ctrlRecTop)

        If intCntSyouhin > 1 Then
            For intLoopCnt = 1 To intCntSyouhin - 1
                '���[�U�R���g���[���̓Ǎ���ID�̕t�^
                ctrlRec = Me.LoadControl(PATH_USR_CTRL_ROW)
                ctrlRec.ID = strRecId & intLoopCnt + 1
                '
                If intLoopCnt Mod 2 = 0 Then
                    ctrlRec.AccTrRecord.Attributes.Add("class", "even")
                End If
                '�e�[�u���ɖ��׍s����s�ǉ�
                Me.tblMeisai.Controls.Add(ctrlRec)
            Next
        End If
    End Sub

#End Region

#End Region

#Region "DB�X�V"

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveData(ByVal sender As System.Object)

        '�������b�Z�[�W���A�������w��|�b�v�A�b�v�\���̂��߂̃t���O���N���A
        callModalFlg.Value = String.Empty

        '�G���[���b�Z�[�W������
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New List(Of Control)

        ' �G���[�`�F�b�N
        CheckInput(errMess, arrFocusTargetCtrl)

        If errMess <> String.Empty Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If


        ' �@�ʃf�[�^�C���p�̃��W�b�N�N���X
        Dim logic As New TeibetuNyuukinLogic
        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        Dim jibanRec As New JibanRecordTeibetuNyuukin


        '***************************************
        ' �n�Ճf�[�^
        '***************************************
        ' �敪
        jibanRec.Kbn = TextKubun.Text
        ' �ԍ��i�ۏ؏�NO�j
        jibanRec.HosyousyoNo = TextBangou.Text
        ' �X�V�҃��[�U�[ID
        jibanRec.UpdLoginUserId = user_info.LoginUserId

        ' �Ǎ����̍X�V�ғ���
        If HiddenUpdDatetime.Value = String.Empty Then
            jibanRec.UpdDatetime = DateTime.MinValue
        Else
            jibanRec.UpdDatetime = DateTime.Parse(HiddenUpdDatetime.Value)
        End If

        ' �ԋ������t���O
        jibanRec.HenkinSyoriFlg = IIf(CheckboxKaiyakuHaraimodosikin.Checked, 1, 0)
        ' �ԋ�������
        If TextHenkinSyoriDate.Text = String.Empty Then
            jibanRec.HenkinSyoriDate = DateTime.MinValue
        Else
            jibanRec.HenkinSyoriDate = DateTime.Parse(TextHenkinSyoriDate.Text)
        End If

        '***************************************
        ' �@�ʓ����f�[�^
        '***************************************

        ' �f�[�^�ݒ�p��Dictionary�ł�
        Dim nyuukinRecords As New Dictionary(Of String, TeibetuNyuukinRecord)
        Dim listNyuukinRecs As New List(Of TeibetuNyuukinUpdateRecord)
        ' �f�[�^�ݒ�p�̓@�ʓ������R�[�h�ł�
        Dim record As TeibetuNyuukinRecord
        Dim updRec As TeibetuNyuukinUpdateRecord
        '��ʂ̃��[�U�[�R���g���[��
        Dim ctrlRecTop As TeibetuNyuukinRecordCtrlTop
        Dim ctrlRec As TeibetuNyuukinRecordCtrl
        Dim strBunruiCd As String = String.Empty
        Dim strGamenHyoujiNo As String = String.Empty
        Dim blnSetRec As Boolean = False

        sysdate = Now

        For intCnt As Integer = 0 To Me.tblMeisai.Controls.Count - 1
            Select Case Me.tblMeisai.Controls(intCnt).GetType.ToString
                Case "ASP.control_teibetunyuukinrecordctrltop_ascx"
                    ctrlRecTop = Me.tblMeisai.Controls(intCnt)
                    If ctrlRecTop.AccTextNyuukinKinGaku.Text <> String.Empty Or ctrlRecTop.AccTextHenkinGaku.Text <> String.Empty Then
                        '��ʂ̏���@�ʓ������R�[�h�ɃZ�b�g
                        record = SetUpdateNyuukinData(ctrlRecTop.AccTextNyuukinKinGaku.Text _
                                                    , ctrlRecTop.AccTextHenkinGaku.Text _
                                                    , ctrlRecTop.AccHiddenBunruiCd.Value _
                                                    , ctrlRecTop.AccHiddenGamenHyoujiNo.Value _
                                                    , ctrlRecTop.AccHiddenUpdDatetime.Value)
                    Else
                        record = Nothing
                    End If
                    '���ރR�[�h�̎擾
                    strBunruiCd = ctrlRecTop.AccHiddenBunruiCd.Value
                    '��ʕ\��NO�̎擾
                    strGamenHyoujiNo = ctrlRecTop.AccHiddenGamenHyoujiNo.Value
                    blnSetRec = True
                Case "ASP.control_teibetunyuukinrecordctrl_ascx"
                    ctrlRec = Me.tblMeisai.Controls(intCnt)
                    If ctrlRec.AccTextNyuukinKinGaku.Text <> String.Empty Or ctrlRec.AccTextHenkinGaku.Text <> String.Empty Then
                        '��ʂ̏���@�ʓ������R�[�h�ɃZ�b�g
                        record = SetUpdateNyuukinData(ctrlRec.AccTextNyuukinKinGaku.Text _
                                , ctrlRec.AccTextHenkinGaku.Text _
                                , ctrlRec.AccHiddenBunruiCd.Value _
                                , ctrlRec.AccHiddenGamenHyoujiNo.Value _
                                , ctrlRec.AccHiddenUpdDatetime.Value)
                    Else
                        record = Nothing
                    End If
                    '���ރR�[�h�̎擾
                    strBunruiCd = ctrlRec.AccHiddenBunruiCd.Value
                    '��ʕ\��NO�̎擾
                    strGamenHyoujiNo = ctrlRec.AccHiddenGamenHyoujiNo.Value
                    blnSetRec = True
                Case Else
                    record = Nothing
                    blnSetRec = False
            End Select

            If blnSetRec Then
                updRec = New TeibetuNyuukinUpdateRecord
                updRec.TeibetuNyuukinrecord = record
                updRec.BunruiCd = strBunruiCd
                If IsNumeric(strGamenHyoujiNo) Then
                    updRec.GamenHyoujiNo = Integer.Parse(strGamenHyoujiNo)
                Else
                    updRec.GamenHyoujiNo = 0
                End If

                'List�ɐݒ肷��
                listNyuukinRecs.Add(updRec)
            End If
        Next

        '�@�ʓ������R�[�h���X�V�p�n�Ճf�[�^�ɐݒ�
        jibanRec.TeibetuNyuukinRecords = nyuukinRecords
        jibanRec.TeibetuNyuukinLists = listNyuukinRecs

        '�X�V��
        jibanRec.Kousinsya = cbLogic.GetKousinsya(user_info.LoginUserId, DateTime.Now)

        ' DB�֔��f
        If logic.SaveJibanData(sender, jibanRec) Then
            Me.tblMeisai.Controls.Clear()

            SetJibanData()

            '�������b�Z�[�W���A�������w��|�b�v�A�b�v�\���̂��߂Ƀt���O���Z�b�g
            callModalFlg.Value = Boolean.TrueString
        End If

    End Sub

    ''' <summary>
    ''' �G���[�`�F�b�N
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <remarks></remarks>
    Private Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control))

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckKinsoku", _
                                                    errMess, _
                                                    arrFocusTargetCtrl)

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '���͒l�`�F�b�N
        If TextHenkinSyoriDate.Text <> String.Empty Then
            If DateTime.Parse(TextHenkinSyoriDate.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHenkinSyoriDate.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�ԋ�������")
                arrFocusTargetCtrl.Add(TextHenkinSyoriDate)
            End If
        End If

    End Sub

    ''' <summary>
    ''' DB�ݒ�p�̓@�ʓ����f�[�^��ҏW���܂�
    ''' </summary>
    ''' <param name="nyuukinGaku">��ʂ̓����z</param>
    ''' <param name="henkinGaku">��ʂ̕ԋ��z</param>
    ''' <param name="bunruiCd">���ރR�[�h</param>
    ''' <param name="gamenHyoujiNo">��ʕ\��NO</param>
    ''' <param name="updateDateTime">�Ǎ����̍X�V����</param>
    ''' <returns>�@�ʓ������R�[�h</returns>
    ''' <remarks></remarks>
    Private Function SetUpdateNyuukinData(ByVal nyuukinGaku As String _
                                        , ByVal henkinGaku As String _
                                        , ByVal bunruiCd As String _
                                        , ByVal gamenHyoujiNo As String _
                                        , ByVal updateDateTime As String) As TeibetuNyuukinRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetUpdateNyuukinData", _
                                                    nyuukinGaku, _
                                                    henkinGaku, _
                                                    bunruiCd, _
                                                    gamenHyoujiNo, _
                                                    updateDateTime)

        Dim record As New TeibetuNyuukinRecord
        '��ʕ\��NO�̐��l�`�F�b�N
        If IsNumeric(gamenHyoujiNo) Then
            Dim intGamenHyoujiNo As Integer = Integer.Parse(gamenHyoujiNo)
        Else
            Return Nothing
            Exit Function
        End If

        nyuukinGaku = nyuukinGaku.Replace(",", String.Empty)
        henkinGaku = henkinGaku.Replace(",", String.Empty)

        ' �L�[���
        record.Kbn = TextKubun.Text
        record.HosyousyoNo = TextBangou.Text
        record.BunruiCd = bunruiCd
        record.GamenHyoujiNo = gamenHyoujiNo

        If (nyuukinGaku.Trim() = String.Empty Or nyuukinGaku.Trim() = "0") And _
           (henkinGaku.Trim() = String.Empty Or henkinGaku.Trim() = "0") Then

            ' �����E�ԋ��z�����ɋ󔒂܂��͂O�̏ꍇ�A�����폜����
            Return Nothing
        End If

        ' �����z
        If nyuukinGaku.Trim() = String.Empty Then
            record.ZeikomiNyuukinGaku = 0
        Else
            record.ZeikomiNyuukinGaku = Integer.Parse(nyuukinGaku)
        End If
        ' �ԋ��z
        If henkinGaku.Trim() = String.Empty Then
            record.ZeikomiHenkinGaku = 0
        Else
            record.ZeikomiHenkinGaku = Integer.Parse(henkinGaku)
        End If
        ' �ŏI������
        record.SaisyuuNyuukinDate = sysdate.Date

        ' �X�V�҃��[�U�[ID
        record.UpdLoginUserId = user_info.LoginUserId
        ' �Ǎ����̍X�V�ғ���
        If updateDateTime = String.Empty Then
            record.UpdDatetime = DateTime.MinValue
        Else
            record.UpdDatetime = DateTime.Parse(updateDateTime)
        End If

        Return record

    End Function

#End Region

#End Region

End Class