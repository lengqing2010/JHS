Partial Public Class PopupSyouhin4
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' ���i4���׍s���/�s���[�U�[�R���g���[���i�[���X�g
    ''' </summary>
    ''' <remarks></remarks>
    Private pListCtrl As New List(Of Syouhin4RecordCtrl)

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic
    Dim cbLogic As New CommonBizLogic

#Region "�p�����[�^/�e�Ɩ����"
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

    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _kameitenCd As String
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrKameitenCd() As String
        Get
            Return _kameitenCd
        End Get
        Set(ByVal value As String)
            _kameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _TysKaisyaCd As String
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrTysKaisyaCd() As String
        Get
            Return _TysKaisyaCd
        End Get
        Set(ByVal value As String)
            _TysKaisyaCd = value
        End Set
    End Property

#End Region

#Region "���i4�E�s�R���g���[��ID�ړ���"
    Private Const SYOUHIN4_CTRL_NAME As String = EarthConst.USR_CTRL_ID_ITEM4
#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban    '�n�Չ�ʋ��ʃN���X
        Dim jSM As New JibanSessionManager
        Dim intKeiriKengen As Integer = 0
        Dim intIraiKengen As Integer = 0
        Dim intHattyuusyoKengen As Integer = 0

        '�}�X�^�[�y�[�W�����擾(ScriptManager�p)
        masterAjaxSM = AjaxScriptManager

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ
            cl.CloseWindow(Me)
            Me.ButtonTouroku1.Visible = False
            Me.ButtonAddNewRow.Visible = False
            Exit Sub
        End If

        If IsPostBack = False Then '�����N����

            pStrKbn = Request("kbn")
            pStrBangou = Request("no")
            pStrKameitenCd = Request("kameitencd")
            pStrTysKaisyaCd = Request("TysKaisyaCd")

            ' �p�����[�^�s�����͉�ʂ����
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                cl.CloseWindow(Me)
                Me.ButtonTouroku1.Visible = False
                Me.ButtonAddNewRow.Visible = False
                Exit Sub
            End If

            '�������̐ݒ�

            '�o���Ɩ������A�˗��Ɩ������A�������Ǘ�����
            intKeiriKengen = userinfo.KeiriGyoumuKengen
            intIraiKengen = userinfo.IraiGyoumuKengen
            intHattyuusyoKengen = userinfo.HattyuusyoKanriKengen

            '�o�������������ꍇ�͓o�^�A�ǉ��{�^���𖳌���
            If intKeiriKengen <> "-1" Then
                Me.ButtonTouroku1.Visible = False
                Me.ButtonAddNewRow.Visible = False
            End If

            '���[�U�R���g���[���p��Hidden�ɃZ�b�g
            Me.HiddenKeiriGyoumuKengen.Value = intKeiriKengen
            Me.HiddenIraiGyoumuKengen.Value = intIraiKengen
            Me.HiddenHattyuusyoKanriKengen.Value = intHattyuusyoKengen

            '���ʏ��̐ݒ�
            Me.HiddenKubun.Value = pStrKbn
            Me.HiddenNo.Value = pStrBangou
            Me.HiddenKameitenCd.Value = pStrKameitenCd
            Me.HiddenJibanTysKaisyaCd.Value = pStrTysKaisyaCd

            '****************************************************************************
            ' �n�Ճf�[�^�擾
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            '�n�Ճf�[�^�����݂���ꍇ�A��ʂɕ\��������
            If jibanRec IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jibanRec)
            Else
                cl.CloseWindow(Me)
                Exit Sub
            End If

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            '�{�^�������C�x���g�̐ݒ�
            Me.setBtnEvent()

            Me.ButtonClose.Focus()
        Else
            '��ʍ��ڐݒ菈��(�|�X�g�o�b�N�p)
            Me.setDisplayPostBack()

        End If

    End Sub

    ''' <summary>
    ''' �y�[�W���[�h�R���v���[�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '���׍s���擾
        Me.HiddenLineCnt.Value = pListCtrl.Count.ToString

        ' �ꗗ�̔w�i�F�Đݒ�
        Dim tmpScript As String
        tmpScript = "settingTable();"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "makeRowStripes", tmpScript, True)

        If IsPostBack = True Then
            '���������̍Đݒ�
            setSeikyuuSiireHenkou(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' ���͍��ڂ̃`�F�b�N
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkInput() As Boolean
        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim errTarget As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)
        Dim tmpScript As String = ""

        Dim intCnt As Integer = 0
        Dim intRowCnt As Integer = Me.tblMeisai.Controls.Count - 1
        Dim ctrlSyouhin4Rec As Syouhin4RecordCtrl

        '���i4���[�U�R���g���[���̃G���[�`�F�b�N
        For intCnt = 0 To intRowCnt - 1
            ctrlSyouhin4Rec = Me.tblMeisai.Controls(intCnt + 1)
            ctrlSyouhin4Rec.CheckInput(errMess, arrFocusTargetCtrl, intCnt + 1)
        Next

        If errMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim lgcSyouhin As New Syouhin4Logic
        Dim jibanRec As New JibanRecord

        '�e�s���Ƃɉ�ʂ��烌�R�[�h�N���X�ɓ��ꍞ��
        jibanRec = GetRowCtrlToSyouhinRec()

        '�f�[�^�̍X�V���s��
        If lgcSyouhin.SaveJibanData(Me, jibanRec, pListCtrl.Count) = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̊e���׍s�������R�[�h�N���X�Ɏ擾���A�n�Ճ��R�[�h�N���X�̃��X�g��ԋp����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToSyouhinRec() As JibanRecord
        Dim dicSyouhin4Records As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim jibanRec As New JibanRecord
        Dim intCntCtrl As Integer = 0

        '***************************************
        ' �n�Ճf�[�^
        '***************************************
        '�敪
        jibanRec.Kbn = Me.HiddenKbn.Value
        '�ԍ��i�ۏ؏�NO�j
        jibanRec.HosyousyoNo = Me.TextBangou.Text
        '�X�V�҃��[�UID
        jibanRec.UpdLoginUserId = userinfo.LoginUserId
        '�X�V����
        jibanRec.UpdDatetime = Date.Parse(Me.HiddenRegUpdDate.Value)
        '�X�V��
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, DateTime.Now)

        '�X�V�p��Dictionary���Đ�������
        jibanRec.Syouhin4Records = New Dictionary(Of Integer, TeibetuSeikyuuRecord)

        '���i4���̎擾
        For intCntCtrl = 1 To pListCtrl.Count
            Dim ctrlSyouhin As Syouhin4RecordCtrl = pListCtrl(intCntCtrl - 1)

            '��ʏ������R�[�h�ɃZ�b�g
            jibanRec.Syouhin4Records.Add(intCntCtrl, ctrlSyouhin.setTeibetuToSyouhin)

            '�e��ʂ��狤�ʏ����Z�b�g
            jibanRec.Syouhin4Records.Item(intCntCtrl).Kbn = Me.HiddenKbn.Value '�敪
            jibanRec.Syouhin4Records.Item(intCntCtrl).HosyousyoNo = Me.TextBangou.Text '�ԍ�
            jibanRec.Syouhin4Records.Item(intCntCtrl).BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_4 '���ރR�[�h

        Next

        Return jibanRec
    End Function

    ''' <summary>
    ''' �n�Ճ��R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s���i�Q�Ə����p�j
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        '****************************************************************************
        ' �n�Ճf�[�^�擾&�Z�b�g
        '****************************************************************************
        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '�h���b�v�_�E�����X�g�̐ݒ�i�敪�j
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kubun, False, True)
        If jr.Kbn IsNot Nothing Then
            objDrpTmp.SelectedValue = jr.Kbn
        Else
            objDrpTmp.SelectedItem.Text = String.Empty
        End If

        '�敪
        Me.TextKbn.Text = objDrpTmp.SelectedItem.Text
        '�敪�i�B�����ځj
        Me.HiddenKbn.Value = jr.Kbn
        '�ԍ�
        Me.TextBangou.Text = cl.GetDispStr(jr.HosyousyoNo)
        '�{�喼
        Me.TextSesyuMei.Text = cl.GetDispStr(jr.SesyuMei)
        '�X�V���� �Ȃ���� �o�^����
        Me.HiddenRegUpdDate.Value = IIf(jr.UpdDatetime <> Date.MinValue, _
                                        jr.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2), _
                                        jr.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2))

        '****************************************************************************
        ' �f�[�^�擾&�Z�b�g
        '****************************************************************************
        Me.SetCtrlFromDataRec(sender, e, jr)

    End Sub

    ''' <summary>
    ''' �e���R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)
        Dim jSM As New JibanSessionManager '�Z�b�V�����Ǘ��N���X
        Dim jBn As New Jiban '�n�Չ�ʃN���X
        Dim IEnumRec As IEnumerator(Of TeibetuSeikyuuRecord)    '���i4���R�[�h�񋓗p
        Dim logic As New JibanLogic
        Dim intCnt As Integer = 0 '�J�E���^
        Dim ctrlSyouhin4InfoRec As New Syouhin4RecordCtrl
        Dim wkTsRec As New TeibetuSeikyuuRecord

        '���i4���R�[�h�̑��݃`�F�b�N
        If jr.Syouhin4Records Is Nothing Then
            Exit Sub
        End If

        '���i4���R�[�h�Q����񋓃I�u�W�F�N�g�Ɋi�[����
        IEnumRec = jr.Syouhin4Records.Values.GetEnumerator
        While IEnumRec.MoveNext
            wkTsRec = IEnumRec.Current  '1�������[�N�Ɋi�[
            '���i�R�[�h�����݂���ꍇ�̂݉�ʕ\��
            If wkTsRec.SyouhinCd IsNot Nothing Then
                '���[�U�R���g���[���̓Ǎ���ID�̕t�^
                ctrlSyouhin4InfoRec = Me.LoadControl("control/Syouhin4RecordCtrl.ascx")
                ctrlSyouhin4InfoRec.ID = SYOUHIN4_CTRL_NAME & (intCnt + 1).ToString

                '���ʏ��̐ݒ�
                ctrlSyouhin4InfoRec.AccHdnKbn.Value = Me.HiddenKubun.Value                           '�敪
                ctrlSyouhin4InfoRec.AccHdnKameitenCd.Value = Me.HiddenKameitenCd.Value               '�����X�R�[�h
                ctrlSyouhin4InfoRec.AccHdnJibanTysKaisyaCd.Value = Me.HiddenJibanTysKaisyaCd.Value   '������ЃR�[�h�i�n�Ձj
                ctrlSyouhin4InfoRec.KeiriGyoumuKengen = Me.HiddenKeiriGyoumuKengen.Value             '�o���Ɩ�����
                ctrlSyouhin4InfoRec.IraiGyoumuKengen = Me.HiddenIraiGyoumuKengen.Value               '�˗��Ɩ�����
                ctrlSyouhin4InfoRec.HattyuusyoKanriKengen = Me.HiddenHattyuusyoKanriKengen.Value     '�������Ǘ�����

                '�e�[�u���ɖ��׍s����s�ǉ�
                Me.tblMeisai.Controls.Add(ctrlSyouhin4InfoRec)

                '���R�[�h�����ʍ��ڂփZ�b�g
                ctrlSyouhin4InfoRec.SetCtrlFromDataRec(sender, e, jr, wkTsRec)

                '��ʕ\���������ڂ����X�g�ɒǉ�
                pListCtrl.Add(ctrlSyouhin4InfoRec)

                intCnt += 1
            End If
        End While
    End Sub

    ''' <summary>
    ''' ���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
    ''' �ύX����Ă���ꍇ�̍Ď擾
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Function setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
        Dim ctrlSyouhin As Syouhin4RecordCtrl

        '�ꗗ�̃��R�[�h��������ύX���̃`�F�b�N
        For intCntCtrl As Integer = 1 To Me.tblMeisai.Controls.Count - 1
            ctrlSyouhin = FindControl(SYOUHIN4_CTRL_NAME & intCntCtrl)
            '�������񂪕ύX����Ă����ꍇ
            If ctrlSyouhin.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
                '���i�����{�^���������̏��������s
                ctrlSyouhin.SetSyouhin(sender, e)
                Me.UpdatePanelSyouhin4.Update()
                '������ύX�t���O��������
                ctrlSyouhin.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
                '�ύX���ꂽ���i���L�����ꍇ�A���[�v�I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
                Return True
                Exit Function
            End If
        Next

        Return False
    End Function

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' �o�^/�C���{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V���A��ʂ̃O���C�A�E�g���s�Ȃ��B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()
        '�C�x���g�n���h���o�^
        Dim tmpScript As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this,null,1);}else{return false;}"

        '�o�^����MSG�m�F��AOK�̏ꍇ�X�V�������s��
        Me.ButtonTouroku1.Attributes("onclick") = tmpScript
    End Sub

    ''' <summary>
    ''' ��ʍ��ڐݒ菈��(�|�X�g�o�b�N�p)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDisplayPostBack()
        Dim intMakeCnt As Integer

        '�s���̎擾
        intMakeCnt = Integer.Parse(Me.HiddenLineCnt.Value)

        '�s�쐬
        For intCnt As Integer = 0 To intMakeCnt - 1
            Me.createRow(intCnt)
        Next

    End Sub

    ''' <summary>
    ''' �s���쐬���܂�
    ''' </summary>
    ''' <param name="intRowCnt">���݂̍s��</param>
    ''' <remarks></remarks>
    Private Sub createRow(ByVal intRowCnt As Integer, Optional ByVal enabled As Boolean = False)
        Dim ctrlSyouhin4InfoRec As New Syouhin4RecordCtrl

        With Me.tblMeisai.Controls
            ctrlSyouhin4InfoRec = Me.LoadControl("control/Syouhin4RecordCtrl.ascx")
            ctrlSyouhin4InfoRec.ID = SYOUHIN4_CTRL_NAME & (intRowCnt + 1).ToString

            '�n�Տ��̐ݒ�
            ctrlSyouhin4InfoRec.AccHdnKbn.Value = Me.HiddenKubun.Value                          '�敪
            ctrlSyouhin4InfoRec.AccHdnKameitenCd.Value = Me.HiddenKameitenCd.Value              '�����X�R�[�h
            ctrlSyouhin4InfoRec.AccHdnJibanTysKaisyaCd.Value = Me.HiddenJibanTysKaisyaCd.Value  '������ЃR�[�h�i�n�Ձj
            ctrlSyouhin4InfoRec.KeiriGyoumuKengen = Me.HiddenKeiriGyoumuKengen.Value            '�o���Ɩ�����
            ctrlSyouhin4InfoRec.IraiGyoumuKengen = Me.HiddenIraiGyoumuKengen.Value              '�˗��Ɩ�����
            ctrlSyouhin4InfoRec.HattyuusyoKanriKengen = Me.HiddenHattyuusyoKanriKengen.Value    '�������Ǘ�����

            .Add(ctrlSyouhin4InfoRec)
        End With

        pListCtrl.Add(ctrlSyouhin4InfoRec)

        '�V�K�s�̏ꍇ�͔񊈐���
        If enabled Then
            ctrlSyouhin4InfoRec.initCtrl(False)
        End If
    End Sub

#End Region

#Region "�{�^���C�x���g"

    ''' <summary>
    ''' �C�����s�{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTouroku1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTouroku1.ServerClick

        '���i���`�F�b�N
        If checkInput() = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If SaveData() Then '�o�^����
            '��ʂ����
            cl.CloseWindow(Me)
        Else
            '�o�^���s
            MLogic.AlertMessage(sender, Messages.MSG019E.Replace("@PARAM1", "�o�^/�C��"), 0, "ButtonTouroku1_ServerClick")
        End If

    End Sub

    ''' <summary>
    ''' �V�K�s�ǉ��{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonAddNewRow_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddNewRow.ServerClick
        Dim intRowCnt As Integer

        '���ݍs�̎擾
        intRowCnt = IIf(Me.HiddenLineCnt.Value = String.Empty, 0, Me.HiddenLineCnt.Value)

        createRow(intRowCnt, True)

    End Sub

#End Region

End Class