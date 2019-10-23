Partial Public Class PopupTokubetuTaiou
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' ���ʑΉ��f�[�^���/�s���[�U�[�R���g���[���i�[���X�g
    ''' </summary>
    ''' <remarks></remarks>
    Private pListCtrl As New List(Of TokubetuTaiouRecordCtrl)

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    Dim MLogic As New MessageLogic
    Dim mttLogic As New TokubetuTaiouMstLogic   '���ʑΉ��}�X�^
    Dim tttLogic As New TokubetuTaiouLogic      '���ʑΉ��g����
    Dim jLogic As New JibanLogic                '�n�Ճ��W�b�N
    Dim kLogic As New KameitenSearchLogic       '�����X�������W�b�N

#Region "�C�x���g"
    ''' <summary>
    ''' �q�R���g���[���ŋN�������`�F�b�N�{�b�N�X�`�F�b�N�C�x���g��e��ʂŔ��f����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Sub SetCheckTokubetuTaiouChangeAction(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String)

        Me.SetCheckTokubetuTaiouChangeOyaAction(strId)
    End Sub
#End Region

#Region "�v���p�e�B"

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
    ''' �������@No
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _TysHouhouNo As String
    ''' <summary>
    ''' �������@No
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrTysHouhouNo() As String
        Get
            Return _TysHouhouNo
        End Get
        Set(ByVal value As String)
            _TysHouhouNo = value
        End Set
    End Property

    ''' <summary>
    ''' ���i�R�[�h(�q�ɃR�[�h="100")
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _SyouhinCd As String
    ''' <summary>
    ''' ���i�R�[�h(�q�ɃR�[�h="100")
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrSyouhinCd() As String
        Get
            Return _SyouhinCd
        End Get
        Set(ByVal value As String)
            _SyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' ���i�R�[�h(���i1,2,3���)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ArrSyouhinCd As String
    ''' <summary>
    ''' ���i�R�[�h(���i1,2,3���)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrArrSyouhinCd() As String
        Get
            Return _ArrSyouhinCd
        End Get
        Set(ByVal value As String)
            _ArrSyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' �v��FLG(���i1,2,3���)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ArrKeijouFlg As String
    ''' <summary>
    ''' �v��FLG(���i1,2,3���)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrArrKeijouFlg() As String
        Get
            Return _ArrKeijouFlg
        End Get
        Set(ByVal value As String)
            _ArrKeijouFlg = value
        End Set
    End Property

    ''' <summary>
    ''' ���������z(���i1,2,3���)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ArrHattyuuKingaku As String
    ''' <summary>
    ''' ���������z(���i1,2,3���)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrArrHattyuuKingaku() As String
        Get
            Return _ArrHattyuuKingaku
        End Get
        Set(ByVal value As String)
            _ArrHattyuuKingaku = value
        End Set
    End Property

    ''' <summary>
    ''' ���ʑΉ��c�[���`�b�vDisplay�R�[�h(���i1,2,3���)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ArrDisplayCd As String
    ''' <summary>
    ''' ���ʑΉ��c�[���`�b�vDisplay�R�[�h(���i1,2,3���)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrArrDisplayCd() As String
        Get
            Return _ArrDisplayCd
        End Get
        Set(ByVal value As String)
            _ArrDisplayCd = value
        End Set
    End Property

    ''' <summary>
    ''' ���ʑΉ��X�V�ΏۃR�[�h(���i1,2,3���)(�s�v)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChgTokuCd As String
    ''' <summary>
    ''' ���ʑΉ��X�V�ΏۃR�[�h(���i1,2,3���)(�s�v)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrChgTokuCd() As String
        Get
            Return _ChgTokuCd
        End Get
        Set(ByVal value As String)
            _ChgTokuCd = value
        End Set
    End Property

    ''' <summary>
    ''' ��ʃ��[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private _GamenMode As EarthEnum.emTokubetuTaiouSearchType
    ''' <summary>
    ''' ��ʃ��[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pEmGamenMode() As EarthEnum.emTokubetuTaiouSearchType
        Get
            Return _GamenMode
        End Get
        Set(ByVal value As EarthEnum.emTokubetuTaiouSearchType)
            _GamenMode = value
        End Set
    End Property

    ''' <summary>
    ''' ���ʑΉ����i���f�p�t���O
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _TokutaiKkkHaneiFlg As String
    ''' <summary>
    ''' ���ʑΉ����i���f�p�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrTokutaiKkkHaneiFlg() As String
        Get
            Return _TokutaiKkkHaneiFlg
        End Get
        Set(ByVal value As String)
            _TokutaiKkkHaneiFlg = value
        End Set
    End Property

    ''' <summary>
    ''' �A��������
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _RentouBukkenSuu As String
    ''' <summary>
    ''' ���ʑΉ����i���f�p�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrRentouBukkenSuu() As String
        Get
            Return _RentouBukkenSuu
        End Get
        Set(ByVal value As String)
            _RentouBukkenSuu = value
        End Set
    End Property

#End Region

#Region "�R���g���[��ID�ړ���"
    Private Const CTBL_NAME_TOKUBETU_TAIOU As String = "CtrlTokubetu_"
#End Region

#Region "�p�����[�^(�z��)"
    Private pStrArrSyouhin1Cd() As String
    Private pStrArrSyouhin2Cd() As String
    Private pStrArrSyouhin3Cd() As String
    Private pStrArrUriKeijyouFlg1() As String
    Private pStrArrUriKeijyouFlg2() As String
    Private pStrArrUriKeijyouFlg3() As String
    Private pStrArrHattyuuKingaku1() As String
    Private pStrArrHattyuuKingaku2() As String
    Private pStrArrHattyuuKingaku3() As String
    Private pStrArrDisplayCd1() As String
    Private pStrArrDisplayCd2() As String
    Private pStrArrDisplayCd3() As String
    Private pStrArrUpdDatetime As String
    Private pStrArrUpdDatetime1() As String
    Private pStrArrUpdDatetime2() As String
    Private pStrArrUpdDatetime3() As String
#End Region

#Region "��ʌŗL�R���g���[���l"
    Private Const SETTEI_SAKI As String = "���i"
    Private Const KEIJYOU_ZUMI As String = "��"

    ''' <summary>
    ''' �z��^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum emArrType
        ''' <summary>
        ''' ���i�R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_CD = 1
        ''' <summary>
        ''' ����v��FLG
        ''' </summary>
        ''' <remarks></remarks>
        KEIJYOU_FLG = 2
        ''' <summary>
        ''' ���������z
        ''' </summary>
        ''' <remarks></remarks>
        HATTYUU_KINGAKU = 3
        ''' <summary>
        ''' �X�V����
        ''' </summary>
        ''' <remarks></remarks>
        UPD_DATETIME = 4
        ''' <summary>
        ''' ���ʑΉ��c�[���`�b�vDipslay�R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        DISPLAY_CD = 5
    End Enum
#End Region

#End Region

#Region "�����Ǎ��������n"
    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban    '�n�Չ�ʋ��ʃN���X
        Dim jSM As New JibanSessionManager

        '�}�X�^�[�y�[�W�����擾(ScriptManager�p)
        masterAjaxSM = AjaxScriptManager

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ
            cl.CloseWindow(Me)
            Me.ButtonTouroku1.Visible = False
            Me.ButtonGetMaster.Visible = False
            Exit Sub
        End If

        If IsPostBack = False Then '�����N����
            '�p�����[�^�擾
            pStrKbn = Request("kbn")
            pStrBangou = Request("no")
            pStrKameitenCd = Request("kameitencd")
            pStrTysHouhouNo = Request("TysHouhouNo")
            pStrSyouhinCd = Request("SyouhinCd")
            pStrArrSyouhinCd = Request("ArrSyouhinCd")
            pStrArrKeijouFlg = Request("ArrKeijouFlg")
            pStrArrHattyuuKingaku = Request("ArrHattyuuKingaku")
            pStrArrDisplayCd = Request("ArrDisplayCd")
            pStrChgTokuCd = Request("ChgTokuCd")
            pEmGamenMode = Request("GamenMode")
            pStrTokutaiKkkHaneiFlg = Request("TokutaiKkkHaneiFlg")
            pStrRentouBukkenSuu = Request("RentouBukkenSuu")

            ' �p�����[�^�s�����͉�ʂ����
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                cl.CloseWindow(Me)
                Me.ButtonTouroku1.Visible = False
                Me.ButtonGetMaster.Visible = False
                Exit Sub
            Else
                '���ʏ��̐ݒ�
                Me.HiddenKubun.Value = pStrKbn
                Me.HiddenNo.Value = pStrBangou
            End If

            '�������̃`�F�b�N
            '�ȉ��̂����ꂩ�̌������Ȃ��ꍇ�A����ʂ����

            '�˗��Ɩ�����
            '�񍐏��Ɩ�����
            '�H���Ɩ�����
            '�ۏ؋Ɩ�����
            '���ʋƖ�����
            If userinfo.IraiGyoumuKengen = 0 _
                        And userinfo.HoukokusyoGyoumuKengen = 0 _
                        And userinfo.KoujiGyoumuKengen = 0 _
                        And userinfo.HosyouGyoumuKengen = 0 _
                        And userinfo.KekkaGyoumuKengen = 0 Then
                Me.ButtonTouroku1.Visible = False
                Me.ButtonGetMaster.Visible = False
            End If

            '****************************************************************************
            ' ���ʑΉ��f�[�^�擾
            '****************************************************************************
            Dim jr As New JibanRecordBase
            jr = jLogic.GetJibanData(pStrKbn, pStrBangou)

            '�n�Ճf�[�^�����݂���ꍇ�A��ʂɕ\��������
            If jLogic.ExistsJibanData(pStrKbn, pStrBangou) AndAlso jr IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jr)
            Else
                cl.CloseWindow(Me)
                Me.ButtonTouroku1.Visible = False
                Me.ButtonGetMaster.Visible = False
                Exit Sub
            End If

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            '�{�^�������C�x���g�̐ݒ�
            Me.setBtnEvent()

            '�@�ʉ�ʂ���͎Q�Ƃ̂�
            If pEmGamenMode = EarthEnum.emTokubetuTaiouSearchType.TeibetuSyuusei Then
                Me.ButtonTouroku1.Visible = False
            End If

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
    Private Sub PopupTokubetuTaiou_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '���׍s���擾
        Me.HiddenLineCnt.Value = pListCtrl.Count.ToString

        ' �ꗗ�̔w�i�F�Đݒ�
        Dim tmpScript As String
        tmpScript = "settingTable();"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "makeRowStripes", tmpScript, True)

    End Sub

    ''' <summary>
    ''' �n�Ճ��R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s���i�Q�Ə����p�j
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)
        Dim total_row As Integer = 0
        Dim dataArray As New List(Of KameitenSearchRecord)

        '****************************************************************************
        ' �n�Ճf�[�^�擾&�Z�b�g&�p�����[�^�擾
        '****************************************************************************
        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList
        Dim strTmpVal As String = String.Empty

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
        Me.TextSesyuMei.Text = cl.GetDisplayString(jr.SesyuMei)

        '�X�V���� �Ȃ���� �o�^����
        Me.HiddenRegUpdDate.Value = IIf(jr.UpdDatetime <> Date.MinValue, _
                                        jr.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2), _
                                        jr.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2))


        '�����X�R�[�h�E���i�R�[�h�E�������@NO�������ꂩ�s���̏ꍇ��DB���擾����
        If pStrKameitenCd Is Nothing OrElse pStrKameitenCd = String.Empty _
            Or pStrTysHouhouNo Is Nothing OrElse pStrTysHouhouNo = String.Empty _
                Or pStrSyouhinCd Is Nothing OrElse pStrSyouhinCd = String.Empty Then

            Me.HiddenKameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)
            Me.HiddenTysHouhouNo.Value = cl.GetDisplayString(jr.TysHouhou)
            If Not jr.Syouhin1Record Is Nothing Then
                Me.HiddenSyouhinCd.Value = jr.Syouhin1Record.SyouhinCd
            End If
        Else
            Me.HiddenKameitenCd.Value = pStrKameitenCd
            Me.HiddenTysHouhouNo.Value = pStrTysHouhouNo
            Me.HiddenSyouhinCd.Value = pStrSyouhinCd
        End If

        'KEY��񂪎擾�ł��Ȃ��ꍇ�A��ʂ����
        If Me.HiddenKameitenCd.Value = String.Empty _
            OrElse Me.HiddenTysHouhouNo.Value = String.Empty _
                OrElse Me.HiddenSyouhinCd.Value = String.Empty Then
            cl.CloseWindow(Me)
            Exit Sub
        End If

        '�����X�R�[�h
        Me.TextKameitenCd.Text = Me.HiddenKameitenCd.Value

        '�����X��
        dataArray = kLogic.GetKameitenSearchResult(jr.Kbn _
                                         , Me.HiddenKameitenCd.Value _
                                         , False _
                                         , total_row)
        If total_row = 1 Then
            Dim recData As KameitenSearchRecord = dataArray(0)
            Me.TextKameitenMei.Text = cl.GetDisplayString(recData.KameitenMei1)
        Else
            Me.TextKameitenMei.Text = String.Empty
        End If

        '�h���b�v�_�E�����X�g�̐ݒ�i���i1�j
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Syouhin1)
        strTmpVal = cl.GetDispNum(Me.HiddenSyouhinCd.Value, "")
        If cl.ChkDropDownList(objDrpTmp, strTmpVal) Then
            'DDL�ɂ���΁A�I������
            objDrpTmp.SelectedValue = strTmpVal
        Else
            Dim recSyouhin As New SyouhinMeisaiRecord
            recSyouhin = jLogic.GetSyouhinRecord(strTmpVal)
            If Not recSyouhin Is Nothing Then
                objDrpTmp.Items.Add(New ListItem(recSyouhin.SyouhinCd & ":" & recSyouhin.SyouhinMei, recSyouhin.SyouhinCd))
                objDrpTmp.SelectedValue = recSyouhin.SyouhinCd  '�I�����
            End If
        End If
        '���i1
        Me.TextSyouhin1.Text = objDrpTmp.SelectedItem.Text

        '�h���b�v�_�E�����X�g�̐ݒ�i�������@�j
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.TyousaHouhou)
        '�������@��DDL�\������
        cl.ps_SetSelectTextBoxTysHouhou(CInt(Me.HiddenTysHouhouNo.Value), objDrpTmp, True, Me.TextTysHouhou)


        '****************************************************************************
        ' �p�����[�^��Hidden�ɐݒ�
        '****************************************************************************
        Me.SetPrmJibanRec(sender, e, jr)

        '***************************************
        ' �@�ʐ����f�[�^
        '***************************************
        '�����i���(��ʒl��萶��)
        Me.ps_SetSyouhin123ToTeibetuRec(jr)

        '****************************************************************************
        ' ���ʑΉ��}�X�^�擾/���ʑΉ��f�[�^�ݒ�
        '****************************************************************************
        Me.SetCtrlFromDataRec(sender, e, jr)

    End Sub

    ''' <summary>
    ''' ���ʑΉ��}�X�^�擾/���ʑΉ��f�[�^�ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)
        Dim ctrlTokubetuInfoRec As New TokubetuTaiouRecordCtrl
        Dim listRes As List(Of TokubetuTaiouRecordBase)
        Dim dtRec As TokubetuTaiouRecordBase
        Dim intTotalCnt As Integer = 0

        Dim strSetteiSaki As String      '�ݒ��
        Dim search_shouhin() As String   '���i����p
        Dim intSearchSyouhin As Integer  '���i����p
        Dim strSyouhinBunrui As String
        Dim strSyouhinGamenHyoujiNo As String
        Dim intCnt As Integer            '�J�E���^
        Dim intGamenHyoujiNo As Integer

        Dim htTtSetteiSaki As New Dictionary(Of Integer, String)
        Dim SyouhinRec As SyouhinMeisaiRecord = jLogic.GetSyouhinRecord(Me.HiddenSyouhinCd.Value) '���i��
        Dim strSyouhin1Mei As String = String.Empty '���i��
        If Not SyouhinRec Is Nothing Then
            strSyouhin1Mei = SyouhinRec.SyouhinMei
        End If

        '���ʑΉ��}�X�^
        listRes = mttLogic.GetTokubetuTaiouInfo(sender, Me.HiddenKbn.Value, Me.TextBangou.Text, Me.HiddenKameitenCd.Value, Me.HiddenSyouhinCd.Value, Me.HiddenTysHouhouNo.Value, intTotalCnt)
        '�������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
        If intTotalCnt = -1 Then
            Exit Sub
        End If

        '�v��FLG�Q
        SplitPrm(Me.HiddenArrKeijyouFlg.Value, pStrArrUriKeijyouFlg1, pStrArrUriKeijyouFlg2, pStrArrUriKeijyouFlg3)
        '���������z
        SplitPrm(Me.HiddenArrHattyuuKingaku.Value, pStrArrHattyuuKingaku1, pStrArrHattyuuKingaku2, pStrArrHattyuuKingaku3)

        '���ʑΉ��c�[���`�b�v���
        If Me.HiddenArrDisplayCd.Value <> String.Empty Then
            SplitPrm(Me.HiddenArrDisplayCd.Value, pStrArrDisplayCd1, pStrArrDisplayCd2, pStrArrDisplayCd3)

            cbLogic.SetTokubetuTaiouCdTeibetuKey(htTtSetteiSaki, EarthEnum.emTeibetuBunrui.SYOUHIN1, pStrArrDisplayCd1)
            cbLogic.SetTokubetuTaiouCdTeibetuKey(htTtSetteiSaki, EarthEnum.emTeibetuBunrui.SYOUHIN2, pStrArrDisplayCd2)
            cbLogic.SetTokubetuTaiouCdTeibetuKey(htTtSetteiSaki, EarthEnum.emTeibetuBunrui.SYOUHIN3, pStrArrDisplayCd3)
        End If

        '���ʑΉ����R�[�h���X�g���x�[�X�Ƀ��[�v
        For intCnt = 0 To listRes.Count - 1
            '���R�[�h�����݂���ꍇ�̂݉�ʕ\��
            dtRec = listRes(intCnt)
            If Not dtRec Is Nothing AndAlso dtRec.mTokubetuTaiouCd <> Integer.MinValue Then
                '���[�U�R���g���[���̓Ǎ���ID�̕t�^
                ctrlTokubetuInfoRec = Me.LoadControl("control/TokubetuTaiouRecordCtrl.ascx")
                ctrlTokubetuInfoRec.ID = CTBL_NAME_TOKUBETU_TAIOU & (intCnt + 1).ToString

                '�`�F�b�N�{�b�N�X�C�x���g�̎���
                AddHandler ctrlTokubetuInfoRec.SetCheckTokubetuTaiouChangeAction, AddressOf Me.SetCheckTokubetuTaiouChangeAction

                '�e�[�u���ɖ��׍s����s�ǉ�
                Me.tblMeisai.Controls.Add(ctrlTokubetuInfoRec)

                '�f�[�^���R���g���[���ɃZ�b�g
                ctrlTokubetuInfoRec.SetCtrlFromDataRec(sender, e, dtRec)

                '�ݒ�悪�Ԏ��ł��󒍉�ʏ���艿�i���f�����ς̏ꍇ�A���Ƃ��Đݒ����㏑������
                If Me.HiddenArrDisplayCd.Value <> String.Empty Then
                    Dim intTmpTtCd As Integer = cl.GetDisplayString(ctrlTokubetuInfoRec.AccHdnTokubetuTaiouCd.Value)
                    '�ݒ�悪���ɃZ�b�g����Ă���ꍇ�ADB�l�ɂȂ��Ă��ݒ����Z�b�g����
                    If htTtSetteiSaki.ContainsKey(intTmpTtCd) Then
                        strSetteiSaki = htTtSetteiSaki(intTmpTtCd)
                        If strSetteiSaki <> String.Empty Then
                            search_shouhin = strSetteiSaki.Split(EarthConst.UNDER_SCORE)
                            strSyouhinBunrui = search_shouhin(0) '���i1,2,3
                            strSyouhinGamenHyoujiNo = search_shouhin(1) '��ʕ\��NO

                            Select Case strSyouhinBunrui
                                Case "1" '���i1
                                    ctrlTokubetuInfoRec.AccHdnBunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_1
                                    ctrlTokubetuInfoRec.AccHdnGamenHyoujiNo.Value = strSyouhinGamenHyoujiNo

                                Case "2" '���i2
                                    ctrlTokubetuInfoRec.AccHdnBunruiCd.Value = cbLogic.pf_getBunruiCd(ctrlTokubetuInfoRec.AccTextKasanSyouhinCd.Value)
                                    ctrlTokubetuInfoRec.AccHdnGamenHyoujiNo.Value = strSyouhinGamenHyoujiNo

                                Case "3" '���i3
                                    ctrlTokubetuInfoRec.AccHdnBunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_3
                                    ctrlTokubetuInfoRec.AccHdnGamenHyoujiNo.Value = strSyouhinGamenHyoujiNo

                            End Select
                            '�Ԏ������ɐݒ�
                            ctrlTokubetuInfoRec.AccHdnHiddenSetteiSakiStyle.Value = EarthConst.STYLE_COLOR_BLUE

                        End If
                    End If
                End If

                '���i�R�[�h1���قȂ�ꍇ�A���z���Z���i�R�[�h�E���z���Z���i�����㏑������
                If ctrlTokubetuInfoRec.AccTextKasanSyouhinCd.Value <> String.Empty Then
                    If ctrlTokubetuInfoRec.AccTextKasanSyouhinCd.Value <> Me.HiddenSyouhinCd.Value Then
                        If ctrlTokubetuInfoRec.AccHdnBunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_1 Then
                            ctrlTokubetuInfoRec.AccTextKasanSyouhinCd.Value = Me.HiddenSyouhinCd.Value
                            ctrlTokubetuInfoRec.AccTextKasanSyouhinMei.Value = strSyouhin1Mei
                        End If
                    End If
                End If

                '�����ڂ�ΏۂɁA���i��񂩂甄��v��FLG�A���������z��ݒ�
                If ctrlTokubetuInfoRec.AccHdnHiddenSetteiSakiStyle.Value = EarthConst.STYLE_COLOR_BLUE Then
                    '�ݒ����擾
                    cl.SetDisplayString(ctrlTokubetuInfoRec.AccHdnGamenHyoujiNo.Value, intGamenHyoujiNo)
                    strSetteiSaki = cbLogic.DevideTokubetuCd(sender, ctrlTokubetuInfoRec.AccHdnBunruiCd.Value, intGamenHyoujiNo)
                    If strSetteiSaki <> String.Empty Then
                        '����v��A���������z
                        search_shouhin = strSetteiSaki.Split(EarthConst.UNDER_SCORE)
                        strSyouhinBunrui = search_shouhin(0) '���i1,2,3
                        strSyouhinGamenHyoujiNo = search_shouhin(1) '��ʕ\��NO

                        intSearchSyouhin = CInt(strSyouhinGamenHyoujiNo)
                        intSearchSyouhin -= 1

                        Select Case strSyouhinBunrui
                            Case "1" '���i1
                                Me.SetUriKeijyouHattyuuKingaku(ctrlTokubetuInfoRec, pStrArrUriKeijyouFlg1(intSearchSyouhin), pStrArrHattyuuKingaku1(intSearchSyouhin))
                            Case "2" '���i2
                                Me.SetUriKeijyouHattyuuKingaku(ctrlTokubetuInfoRec, pStrArrUriKeijyouFlg2(intSearchSyouhin), pStrArrHattyuuKingaku2(intSearchSyouhin))
                            Case "3" '���i3
                                Me.SetUriKeijyouHattyuuKingaku(ctrlTokubetuInfoRec, pStrArrUriKeijyouFlg3(intSearchSyouhin), pStrArrHattyuuKingaku3(intSearchSyouhin))
                        End Select
                    End If
                End If

                '��ʕ\���������ڂ����X�g�ɒǉ�
                pListCtrl.Add(ctrlTokubetuInfoRec)
            End If
        Next

        '��ʕ\�����_�̒l���AHidden�ɕێ�(�ύX�`�F�b�N�p)
        If Me.HiddenOpenValues.Value = String.Empty Then
            Me.HiddenOpenValues.Value = Me.getCtrlValuesString()
        End If

        '�ݒ��Đݒ菈��
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", "objEBI('" & Me.ButtonSetSetteiSaki.ClientID & "').click();", True)

    End Sub

    ''' <summary>
    ''' �o�^/�C���{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V���A��ʂ̃O���C�A�E�g���s�Ȃ��B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        Dim tmpCheckTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this,null,1);}else{return false;}"
        Dim tmpScript As String = "if(CheckTouroku()){" & tmpCheckTouroku & "}else{return false;}"
        Dim tmpScript2 As String = "if(confirm('" & Messages.MSG181C & "')){}else{return false;}"

        '�o�^����MSG�m�F��AOK�̏ꍇ�X�V�������s��
        Me.ButtonTouroku1.Attributes("onclick") = "setAction('" & EarthEnum.emTokubetuTaiouActBtn.BtnOther & "');" & tmpScript

        '�}�X�^�Ď擾�{�^��
        Dim tmpGetMaster As String = "setAction('" & EarthEnum.emTokubetuTaiouActBtn.BtnMaster & "');" & tmpScript2
        Me.ButtonGetMaster.Attributes("onclick") = "if(CheckGetMaster()){" & tmpGetMaster & "}else{return false;}"

    End Sub

    ''' <summary>
    ''' ���ʑΉ��}�X�^�擾/�}�X�^�l��ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRecMst(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctrlTokubetuInfoRec As New TokubetuTaiouRecordCtrl
        Dim dtRec As New TokubetuTaiouRecordBase
        Dim intTotalCnt As Integer = 0
        Dim intCnt As Integer = 0 '�J�E���^

        With Me.tblMeisai.Controls
            For intCnt = 1 To .Count - 1
                ctrlTokubetuInfoRec = Me.LoadControl("control/TokubetuTaiouRecordCtrl.ascx")
                ctrlTokubetuInfoRec.ID = CTBL_NAME_TOKUBETU_TAIOU & intCnt.ToString

                ctrlTokubetuInfoRec = pListCtrl(intCnt - 1)

                dtRec = New TokubetuTaiouRecordBase

                '��ʏ������R�[�h�ɃZ�b�g
                ctrlTokubetuInfoRec.GetRowCtrlToDataRec(dtRec)

                '�f�[�^���R���g���[���ɃZ�b�g
                ctrlTokubetuInfoRec.SetCtrlFromHiddenMst(sender, e, dtRec)

                .Add(ctrlTokubetuInfoRec)

            Next

        End With

        '�ݒ����Z�b�g
        Me.SetCheckTokubetuTaiouChangeOyaAction(Me.ButtonGetMaster.ClientID)

        '�}�X�^�Ď擾�{�^���������̓��b�Z�[�W��\��
        '�������b�Z�[�W�̕\���ƃe�[�u�����C�A�E�g��ݒ�
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", "alert('" & Messages.MSG184E & "');", True)

    End Sub

    ''' <summary>
    ''' ��ʍ��ڐݒ菈��(�|�X�g�o�b�N�p)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDisplayPostBack()
        Dim intMakeCnt As Integer = 0

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
    ''' <param name="intRowCnt">�쐬����s��</param>
    ''' <remarks></remarks>
    Private Sub createRow(ByVal intRowCnt As Integer)
        Dim ctrlTokubetuInfoRec As New TokubetuTaiouRecordCtrl

        With Me.tblMeisai.Controls
            ctrlTokubetuInfoRec = Me.LoadControl("control/TokubetuTaiouRecordCtrl.ascx")
            ctrlTokubetuInfoRec.ID = CTBL_NAME_TOKUBETU_TAIOU & (intRowCnt + 1).ToString

            '�`�F�b�N�{�b�N�X�`�F�b�N�C�x���g�̎���
            AddHandler ctrlTokubetuInfoRec.SetCheckTokubetuTaiouChangeAction, AddressOf Me.SetCheckTokubetuTaiouChangeAction

            .Add(ctrlTokubetuInfoRec)
        End With

        pListCtrl.Add(ctrlTokubetuInfoRec)

    End Sub
#End Region

#Region "DB�X�V�����n"
    ''' <summary>
    ''' ���͍��ڂ̃`�F�b�N
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkInput() As Boolean
        '�`�F�b�N�Ȃ�
        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim listRec As List(Of TokubetuTaiouRecordBase)
        Dim jr As New JibanRecordBase
        Dim intTmp As Integer
        Dim blnAuto As Boolean

        '�n�Ճf�[�^�̎擾
        jr = Me.GetCtrlFromJibanRec()

        '�e�s���Ƃɉ�ʂ��烌�R�[�h�N���X�ɓ��ꍞ��
        listRec = Me.GetRowCtrlToList()

        '���ʑΉ���������
        cl.SetDisplayString(Me.HiddenGamenMode.Value, intTmp)
        If intTmp = EarthEnum.emTokubetuTaiouSearchType.IraiKakunin Then
            blnAuto = False
        Else
            blnAuto = True
        End If

        '�f�[�^�̍X�V���s��
        If tttLogic.saveData(Me, jr, listRec, blnAuto) = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' ��ʕ\�����ڂ���n�Ճ��R�[�h�ւ̒l�Z�b�g���s���i�r���`�F�b�N�p�j
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlFromJibanRec() As JibanRecordBase
        Dim jr As New JibanRecordBase

        ' ���݂̒n�Ճf�[�^��DB����擾����
        jr = jLogic.GetJibanData(Me.HiddenKbn.Value, Me.TextBangou.Text)

        '���ʑΉ���������
        Dim intTmp As Integer = 0
        cl.SetDisplayString(Me.HiddenGamenMode.Value, intTmp)

        '***************************************
        ' �n�Ճf�[�^
        '***************************************
        With jr
            '��KEY��񁨉�ʏ����㏑��
            '�����X�R�[�h
            .KameitenCd = Me.HiddenKameitenCd.Value
            ''���i�R�[�h���㑱�ŃZ�b�g
            '.Syouhin1Record.SyouhinCd = Me.HiddenSyouhinCd.Value
            '�������@NO
            .TysHouhou = Me.HiddenTysHouhouNo.Value

            '***************************************
            ' �@�ʐ����f�[�^
            '***************************************
            If intTmp = EarthEnum.emTokubetuTaiouSearchType.IraiKakunin Then
                '�����i���(��ʒl��萶��)
                Me.ps_SetSyouhin123ToTeibetuRec(jr)
            End If

            '�X�V�҃��[�UID
            .UpdLoginUserId = userinfo.LoginUserId
            '�X�V����
            .UpdDatetime = Date.Parse(Me.HiddenRegUpdDate.Value)

            '�A��������
            .RentouBukkenSuu = cl.GetDispNum(Me.HiddenRentouBukkenSuu.Value, "1")

        End With

        Return jr
    End Function

    ''' <summary>
    ''' �����n���ꂽHidden�����ƂɁA���i123(�@�ʐ���)���𐶐����A�擾����
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h�N���X</param>
    ''' <remarks></remarks>
    Private Sub ps_SetSyouhin123ToTeibetuRec(ByRef jibanRec As JibanRecordBase)

        With jibanRec
            '���i�����N���A
            .Syouhin1Record = Nothing
            .Syouhin2Records = Nothing
            .Syouhin3Records = Nothing
        End With

        '���i1���R�[�h�I�u�W�F�N�g�̐���
        jLogic.CreateSyouhin1Rec(Me, jibanRec)
        '���i2���R�[�h�I�u�W�F�N�g�̐���
        jLogic.CreateSyouhin23Rec(Me, jibanRec)
        '���i3���R�[�h�I�u�W�F�N�g�̐���
        jLogic.CreateSyouhin23Rec(Me, jibanRec, True)

        '�����i��񁨉�ʏ����㏑��
        '���i�R�[�h�Q
        SplitPrm(Me.HiddenArrSyouhinCd.Value, pStrArrSyouhin1Cd, pStrArrSyouhin2Cd, pStrArrSyouhin3Cd)
        '�v��FLG�Q
        SplitPrm(Me.HiddenArrKeijyouFlg.Value, pStrArrUriKeijyouFlg1, pStrArrUriKeijyouFlg2, pStrArrUriKeijyouFlg3)
        '���������z
        SplitPrm(Me.HiddenArrHattyuuKingaku.Value, pStrArrHattyuuKingaku1, pStrArrHattyuuKingaku2, pStrArrHattyuuKingaku3)
        '�X�V����
        SplitPrm(Me.HiddenArrUpdDatetime.Value, pStrArrUpdDatetime1, pStrArrUpdDatetime2, pStrArrUpdDatetime3)

        '���i1
        If Not jibanRec.Syouhin1Record Is Nothing Then

            With jibanRec.Syouhin1Record
                '���i�R�[�h
                cl.SetDisplayString(pStrArrSyouhin1Cd(0), .SyouhinCd)
                '���ރR�[�h
                .BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1
                '��ʕ\��No
                .GamenHyoujiNo = 1
                '����v��FLG
                cl.SetDisplayString(pStrArrUriKeijyouFlg1(0), .UriKeijyouFlg)
                '���������z
                cl.SetDisplayString(pStrArrHattyuuKingaku1(0), .HattyuusyoGaku)
                '�X�V����
                If Not pStrArrUpdDatetime1 Is Nothing AndAlso pStrArrUpdDatetime1(0) <> EarthConst.BRANK_STRING Then
                    .UpdDatetime = DateTime.ParseExact(pStrArrUpdDatetime1(0), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                End If
            End With

        End If

        '���i2
        If Not jibanRec.Syouhin2Records Is Nothing Then
            For intCnt As Integer = 1 To EarthConst.SYOUHIN2_COUNT
                If jibanRec.Syouhin2Records.ContainsKey(intCnt) Then

                    With jibanRec.Syouhin2Records(intCnt)
                        '���i�R�[�h
                        cl.SetDisplayString(pStrArrSyouhin2Cd(intCnt - 1), .SyouhinCd)
                        '���ރR�[�h
                        .BunruiCd = cbLogic.pf_getBunruiCd(.SyouhinCd)
                        '��ʕ\��No
                        .GamenHyoujiNo = intCnt
                        '����v��FLG
                        cl.SetDisplayString(pStrArrUriKeijyouFlg2(intCnt - 1), .UriKeijyouFlg)
                        '���������z
                        cl.SetDisplayString(pStrArrHattyuuKingaku2(intCnt - 1), .HattyuusyoGaku)
                        '�X�V����
                        If Not pStrArrUpdDatetime2 Is Nothing AndAlso pStrArrUpdDatetime2(intCnt - 1) <> EarthConst.BRANK_STRING Then
                            .UpdDatetime = DateTime.ParseExact(pStrArrUpdDatetime2(intCnt - 1), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                        End If
                    End With

                End If
            Next
        End If
        '���i�R�[�h���ݒ�͍폜
        jLogic.DeleteSyouhin23Rec(Me, jibanRec)

        '���i3
        If Not jibanRec.Syouhin3Records Is Nothing Then
            For intCnt As Integer = 1 To EarthConst.SYOUHIN3_COUNT

                If jibanRec.Syouhin3Records.ContainsKey(intCnt) Then

                    With jibanRec.Syouhin3Records(intCnt)
                        '���i�R�[�h
                        cl.SetDisplayString(pStrArrSyouhin3Cd(intCnt - 1), .SyouhinCd)
                        '���ރR�[�h
                        .BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3
                        '��ʕ\��No
                        .GamenHyoujiNo = intCnt
                        '����v��FLG
                        cl.SetDisplayString(pStrArrUriKeijyouFlg3(intCnt - 1), .UriKeijyouFlg)
                        '���������z
                        cl.SetDisplayString(pStrArrHattyuuKingaku3(intCnt - 1), .HattyuusyoGaku)
                        '�X�V����
                        If Not pStrArrUpdDatetime3 Is Nothing AndAlso pStrArrUpdDatetime3(intCnt - 1) <> EarthConst.BRANK_STRING Then
                            .UpdDatetime = DateTime.ParseExact(pStrArrUpdDatetime3(intCnt - 1), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                        End If
                    End With

                End If
            Next
        End If
        '���i�R�[�h���ݒ�͍폜
        jLogic.DeleteSyouhin23Rec(Me, jibanRec, True)


    End Sub

    ''' <summary>
    ''' ��ʂ̊e���׍s�������R�[�h�N���X�Ɏ擾���A�n�Ճ��R�[�h�N���X�̃��X�g��ԋp����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToList() As List(Of TokubetuTaiouRecordBase)
        Dim listRec As New List(Of TokubetuTaiouRecordBase)
        Dim intCntCtrl As Integer = 0
        Dim dtRec As New TokubetuTaiouRecordBase
        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim intMaxCnt As Integer = 0

        intMaxCnt = pListCtrl.Count

        '***************************************
        ' ���ʑΉ��f�[�^
        '***************************************
        For intCntCtrl = 1 To intMaxCnt
            ctrlTokubetu = pListCtrl(intCntCtrl - 1)

            dtRec = New TokubetuTaiouRecordBase

            '�敪
            dtRec.Kbn = Me.HiddenKbn.Value
            '�ԍ��i�ۏ؏�NO�j
            dtRec.HosyousyoNo = Me.TextBangou.Text
            '�X�V�҃��[�UID
            dtRec.UpdLoginUserId = userinfo.LoginUserId

            '��ʏ������R�[�h�ɃZ�b�g
            ctrlTokubetu.GetRowCtrlToDataRec(dtRec)

            listRec.Add(dtRec)
        Next

        Return listRec
    End Function

#End Region

#Region "���ʑΉ����i�Ή�"

#Region "��ʕ\���֘A"
    ''' <summary>
    ''' �p�����[�^��Hidden�Ɋi�[
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetPrmJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByRef jr As JibanRecordBase)
        '��ʃ��[�h
        Me.HiddenGamenMode.Value = pEmGamenMode

        '���ʑΉ����i���f�p�t���O(���i1�ύX�t���O)
        Me.HiddenTokutaiKkkHaneiFlg.Value = IIf(pStrTokutaiKkkHaneiFlg = "1", pStrTokutaiKkkHaneiFlg, "0")

        '�A��������
        Me.HiddenRentouBukkenSuu.Value = IIf(pStrRentouBukkenSuu = Nothing OrElse pStrRentouBukkenSuu = String.Empty, "1", pStrRentouBukkenSuu)

        '���i�R�[�h�Q
        If pStrArrSyouhinCd Is Nothing OrElse pStrArrSyouhinCd = String.Empty Then
            pStrArrSyouhinCd = SetArrFromTeibetuSeikyuu(jr, emArrType.SYOUHIN_CD)           '�s���̏ꍇ��DB���擾����
        End If
        Me.HiddenArrSyouhinCd.Value = pStrArrSyouhinCd                                      'Hidden�ɑޔ�

        '�v��FLG�Q
        If pStrArrKeijouFlg Is Nothing OrElse pStrArrKeijouFlg = String.Empty Then
            pStrArrKeijouFlg = SetArrFromTeibetuSeikyuu(jr, emArrType.KEIJYOU_FLG)          '�s���̏ꍇ��DB���擾����
        End If
        Me.HiddenArrKeijyouFlg.Value = pStrArrKeijouFlg                                     'Hidden�ɑޔ�

        '���������z�Q
        If pStrArrHattyuuKingaku Is Nothing OrElse pStrArrHattyuuKingaku = String.Empty Then
            pStrArrHattyuuKingaku = SetArrFromTeibetuSeikyuu(jr, emArrType.HATTYUU_KINGAKU) '�s���̏ꍇ��DB���擾����
        End If
        Me.HiddenArrHattyuuKingaku.Value = pStrArrHattyuuKingaku                            'Hidden�ɑޔ�

        '�X�V����
        If pEmGamenMode <> EarthEnum.emTokubetuTaiouSearchType.IraiKakunin Then             '�󒍉�ʈȊO����Ă΂ꂽ�ꍇ�ADB���擾����
            pStrArrUpdDatetime = SetArrFromTeibetuSeikyuu(jr, emArrType.UPD_DATETIME)
            Me.HiddenArrUpdDatetime.Value = pStrArrUpdDatetime                              'Hidden�ɑޔ�
        End If

        '���ʑΉ��c�[���`�b�vDisplay�R�[�h
        Me.HiddenArrDisplayCd.Value = pStrArrDisplayCd                                      'Hidden�ɑޔ�
        '���ʑΉ��X�V�ΏۃR�[�h(�s�v)
        Me.HiddenChgTokuCd.Value = pStrChgTokuCd                                            'Hidden�ɑޔ�

    End Sub

    ''' <summary>
    ''' ��؂蕶��($$$)���g���ADB�l�𕶎���ɂ���
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <param name="emType">�z��^�C�v</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetArrFromTeibetuSeikyuu(ByVal jr As JibanRecordBase, ByVal emType As emArrType)

        Dim intCnt As Integer
        Dim strArr As String
        Dim strSepString2 As String = EarthConst.SEP_STRING & EarthConst.SEP_STRING                         '���i�e�s��؂蕶��

        '���i1
        strArr = GetArrFromTeibetuSeikyuu(jr.Syouhin1Record, emType)
        strArr &= EarthConst.SEP_STRING
        '���i2
        For intCnt = 1 To EarthConst.SYOUHIN2_COUNT
            If Not jr.Syouhin2Records Is Nothing AndAlso jr.Syouhin2Records.ContainsKey(intCnt) Then

                strArr &= strSepString2 & GetArrFromTeibetuSeikyuu(jr.Syouhin2Records(intCnt), emType)
            Else
                If emType = emArrType.HATTYUU_KINGAKU Then
                    strArr &= strSepString2 & "0"
                Else
                    strArr &= strSepString2 & EarthConst.BRANK_STRING
                End If
            End If
        Next
        strArr &= EarthConst.SEP_STRING
        '���i3
        For intCnt = 1 To EarthConst.SYOUHIN3_COUNT
            If Not jr.Syouhin3Records Is Nothing AndAlso jr.Syouhin3Records.ContainsKey(intCnt) Then

                strArr &= strSepString2 & GetArrFromTeibetuSeikyuu(jr.Syouhin3Records(intCnt), emType)
            Else
                If emType = emArrType.HATTYUU_KINGAKU Then
                    strArr &= strSepString2 & "0"
                Else
                    strArr &= strSepString2 & EarthConst.BRANK_STRING
                End If
            End If
        Next

        Return strArr
    End Function

    ''' <summary>
    ''' ������܂��͔��p�X�y�[�X��Ԃ�
    ''' </summary>
    ''' <param name="dtRec">�@�ʐ������R�[�h</param>
    ''' <param name="emType">�z��^�C�v</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetArrFromTeibetuSeikyuu(ByVal dtRec As TeibetuSeikyuuRecord, ByVal emType As emArrType) As String
        Dim strTmp As String = String.Empty

        If emType = emArrType.SYOUHIN_CD Then                                       '���i�R�[�h
            strTmp = dtRec.SyouhinCd

        ElseIf emType = emArrType.KEIJYOU_FLG Then                                  '����v��FLG
            strTmp = cl.GetDisplayString(dtRec.UriKeijyouFlg, String.Empty)

        ElseIf emType = emArrType.HATTYUU_KINGAKU Then                              '���������z
            strTmp = cl.GetDisplayString(dtRec.HattyuusyoGaku, "0")

        ElseIf emType = emArrType.UPD_DATETIME Then                                 '�X�V����
            '�󒍉�ʂ���Ă΂ꂽ�ꍇ�͐ݒ肵�Ȃ�
            If pEmGamenMode = EarthEnum.emTokubetuTaiouSearchType.IraiKakunin Then
                strTmp = String.Empty
            Else
                strTmp = IIf(dtRec.UpdDatetime = DateTime.MinValue, Format(dtRec.AddDatetime, EarthConst.FORMAT_DATE_TIME_1), Format(dtRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
            End If

        End If

        '�l�������Ă��Ȃ��ꍇ���p�X�y�[�X��Ԃ�
        If strTmp Is Nothing OrElse strTmp Is String.Empty Then
            Return EarthConst.BRANK_STRING
        Else
            Return strTmp
        End If

    End Function

    ''' <summary>
    ''' �p�����[�^�𕪊����Ĕz��Ɋi�[����
    ''' </summary>
    ''' <param name="strPrm">�p�����[�^</param>
    ''' <param name="strArr1">���i1�z��</param>
    ''' <param name="strArr2">���i2�z��</param>
    ''' <param name="strArr3">���i3�z��</param>
    ''' <remarks></remarks>
    Private Sub SplitPrm(ByVal strPrm As String, ByRef strArr1() As String, ByRef strArr2() As String, ByRef strArr3() As String)

        Dim strSepString3 As String = EarthConst.SEP_STRING & EarthConst.SEP_STRING & EarthConst.SEP_STRING '���i1�`3��؂蕶��
        Dim strSepString2 As String = EarthConst.SEP_STRING & EarthConst.SEP_STRING                         '���i�e�s��؂蕶��

        Dim arrTmp() As String
        '�z��̏�����
        arrTmp = Nothing
        strArr1 = Nothing
        strArr2 = Nothing
        strArr3 = Nothing

        If strPrm <> String.Empty Then
            '��؂蕶���ŕ���
            arrTmp = Split(strPrm, strSepString3)

            '��؂蕶���ŕ������A�e���i�z���
            strArr1 = Split(arrTmp(0), strSepString2)
            strArr2 = Split(arrTmp(1), strSepString2)
            strArr3 = Split(arrTmp(2), strSepString2)
        End If

    End Sub

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesString() As String

        Dim listRec As New List(Of TokubetuTaiouRecordBase)
        Dim dtRec As New TokubetuTaiouRecordBase
        Dim sb As New StringBuilder

        '�e�s���Ƃɉ�ʂ��烌�R�[�h�N���X�ɓ��ꍞ��
        listRec = GetRowCtrlToList()

        For Each dtRec In listRec

            If dtRec.CheckJyky = True Then
                '���ʑΉ��R�[�h
                sb.Append(dtRec.TokubetuTaiouCd & EarthConst.SEP_STRING)
            End If
        Next

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' ���ʑΉ����/����v��E���������z���Z�b�g
    ''' </summary>
    ''' <param name="ctrlRec">���ʑΉ����R�[�hCtrl</param>
    ''' <param name="strKeijyouFlg">�v��FLG</param>
    ''' <param name="strHattyuuKingaku">���������z</param>
    ''' <remarks></remarks>
    Private Sub SetUriKeijyouHattyuuKingaku(ByRef ctrlRec As TokubetuTaiouRecordCtrl, ByVal strKeijyouFlg As String, ByVal strHattyuuKingaku As String)
        '����v��FLG
        If strKeijyouFlg = EarthConst.ARI_VAL Then
            ctrlRec.AccHdnUriKeijyou.Value = EarthConst.ARI_VAL
            ctrlRec.AccTextUriKeijyou.Value = KEIJYOU_ZUMI
        Else
            ctrlRec.AccHdnUriKeijyou.Value = EarthConst.NASI_VAL
            ctrlRec.AccTextUriKeijyou.Value = String.Empty
        End If

        '���������z
        ctrlRec.AccHdnHattyuuKingaku.Value = strHattyuuKingaku

    End Sub

#End Region

#Region "�`�F�b�N�{�b�N�X�֘A"
    ''' <summary>
    ''' ���ʑΉ��`�F�b�N�{�b�N�X/�`�F�b�N��ԕύX���A�ݒ����Ď擾����
    ''' </summary>
    ''' <param name="ttList">���ʑΉ����R�[�h�̃��X�g</param>
    ''' <remarks></remarks>
    Private Sub ps_SetSetteiSaki(ByRef ttList As List(Of TokubetuTaiouRecordBase))
        Dim ttRec As New TokubetuTaiouRecordBase
        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim strResult As String

        Dim intCntCtrl As Integer = 0
        Dim intMaxCnt As Integer = 0
        intMaxCnt = pListCtrl.Count

        For intCntCtrl = 1 To intMaxCnt

            ttRec = ttList(intCntCtrl - 1)
            ctrlTokubetu = pListCtrl(intCntCtrl - 1)

            '�ݒ����Ď擾
            If Not ttRec Is Nothing Then
                '�擾���ʂ��w��̓��ʑΉ����R�[�h�R���g���[���̐ݒ��ɑ΂��Đݒ肷��
                strResult = cbLogic.DevideTokubetuCd(Me, ttRec.BunruiCd, ttRec.GamenHyoujiNo)

                '�ݒ��̕\���ؑ֏������s�Ȃ�
                If strResult <> String.Empty Then
                    '�ݒ���\��
                    ctrlTokubetu.AccTextSetteiSaki.Value = SETTEI_SAKI & strResult
                    '���ރR�[�h
                    ctrlTokubetu.AccHdnBunruiCd.Value = cl.GetDisplayString(ttRec.BunruiCd)
                    '��ʕ\��NO
                    ctrlTokubetu.AccHdnGamenHyoujiNo.Value = cl.GetDisplayString(ttRec.GamenHyoujiNo)
                Else
                    '�ݒ���\��
                    ctrlTokubetu.AccTextSetteiSaki.Value = String.Empty
                    '���ރR�[�h
                    ctrlTokubetu.AccHdnBunruiCd.Value = String.Empty
                    '��ʕ\��NO
                    ctrlTokubetu.AccHdnGamenHyoujiNo.Value = String.Empty
                End If

            End If
        Next
    End Sub

    ''' <summary>
    ''' ���ʑΉ����R�[�h(DB�l)�ɉ�ʏ����Z�b�g���ĕԋp
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <returns>���ʑΉ����R�[�h�̃��X�g</returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuInfo(ByVal jibanRec As JibanRecordBase) As List(Of TokubetuTaiouRecordBase)

        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim ttDispRec As New TokubetuTaiouRecordBase
        Dim ttList As New List(Of TokubetuTaiouRecordBase)
        Dim intCnt As Integer
        Dim intListCnt As Integer
        Dim intCtrlCnt As Integer = 0

        With jibanRec
            If Not jibanRec Is Nothing AndAlso Not .Syouhin1Record Is Nothing Then
                '���ʑΉ��}�X�^�x�[�X�̓��ʑΉ��f�[�^���擾
                ttList = mttLogic.GetTokubetuTaiouInfo(Me, _
                                                       .Kbn, _
                                                       .HosyousyoNo, _
                                                       .KameitenCd, _
                                                       .Syouhin1Record.SyouhinCd, _
                                                       .TysHouhou, _
                                                       intCnt)
            End If
        End With

        '���������`�F�b�N
        If intCnt <= 0 Then
            Return Nothing
        End If
        '***************************************
        ' ���ʑΉ��f�[�^
        '***************************************
        For intCtrlCnt = 0 To pListCtrl.Count - 1

            ctrlTokubetu = pListCtrl(intCtrlCnt)
            ttDispRec = New TokubetuTaiouRecordBase

            '��ʏ������R�[�h�ɃZ�b�g
            ttDispRec = ctrlTokubetu.GetChkRowCtrlToDataRec

            '���ʑΉ��}�X�^�x�[�X�̓��ʑΉ��f�[�^
            For intListCnt = 0 To ttList.Count - 1

                'DB�l����ʏ��ŏ㏑��
                If ttList(intListCnt).mTokubetuTaiouCd = ttDispRec.TokubetuTaiouCd Then
                    With ttList(intListCnt)
                        .TokubetuTaiouCd = ttDispRec.TokubetuTaiouCd
                        .Torikesi = IIf(ttDispRec.CheckJyky = True, 0, 1)
                        .BunruiCd = ttDispRec.BunruiCd
                        .GamenHyoujiNo = ttDispRec.GamenHyoujiNo
                        .KasanSyouhinCd = ttDispRec.KasanSyouhinCd
                        .KoumutenKasanGaku = ttDispRec.KoumutenKasanGaku
                        .UriKasanGaku = ttDispRec.UriKasanGaku
                        .KkkSyoriFlg = ttDispRec.KkkSyoriFlg
                        .UpdFlg = IIf(ttDispRec.HenkouCheck = True, 1, 0)
                        .SetteiSakiStyle = ttDispRec.SetteiSakiStyle
                    End With

                    Exit For
                End If
            Next
        Next

        Return ttList
    End Function

    ''' <summary>
    ''' �`�F�b�N�{�b�N�X��ClientID�����ƂɊY����Ctrl���擾
    ''' </summary>
    ''' <param name="strId">�`�F�b�N�{�b�N�X��ClientID</param>
    ''' <returns>���ʑΉ����R�[�hCtrl</returns>
    ''' <remarks></remarks>
    Public Function GetChkRowCtrl(ByVal strId As String) As TokubetuTaiouRecordCtrl
        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim intCntCtrl As Integer = 0
        Dim intMaxCnt As Integer = 0

        intMaxCnt = pListCtrl.Count

        '***************************************
        ' ���ʑΉ��f�[�^
        '***************************************
        For intCntCtrl = 1 To intMaxCnt

            ctrlTokubetu = pListCtrl(intCntCtrl - 1)

            If ctrlTokubetu.AccCheckBoxTokubetuTaiou.ClientID = strId Then
                Return ctrlTokubetu
            End If
        Next

        Return Nothing
    End Function

#End Region

#End Region

#Region "�{�^���C�x���g"
    ''' <summary>
    ''' �C�����s�{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTouroku1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTouroku1.ServerClick

        '���̓`�F�b�N
        If checkInput() = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If SaveData() Then '�o�^����

            '���ʑΉ��{�^���F�߂�FLG�𗧂Ă�(�e��ʗp)
            Me.HiddenPressMasterFlg.Value = EarthEnum.emTokubetuTaiouActBtn.PressBtnMstTouroku

            '��ʂ����
            Dim tmpScript1 As String = "window.returnValue = " & Me.HiddenPressMasterFlg.Value & ";" & "window.close();" '��ʂ����
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", tmpScript1, True)

        Else
            '�o�^���s
            MLogic.AlertMessage(sender, Messages.MSG019E.Replace("@PARAM1", "�o�^/�C��"), 0, "ButtonTouroku1_ServerClick")
        End If

    End Sub

    ''' <summary>
    ''' �}�X�^�[�Ď擾�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonGetMaster_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetMaster.ServerClick
        '****************************************************************************
        ' �����X���i�������@���ʑΉ��}�X�^�ݒ�
        '****************************************************************************
        Me.SetCtrlFromDataRecMst(sender, e)

    End Sub

    ''' <summary>
    ''' ���ʑΉ��`�F�b�N�{�b�N�X/�`�F�b�N��ԕύX���A�Y���̓��ʑΉ��̐ݒ��𔻒肵�ĉ�ʂɃZ�b�g����
    ''' </summary>
    ''' <param name="strClientId">�`�F�b�N�{�b�N�X��ClientID</param>
    ''' <remarks></remarks>
    Private Sub SetCheckTokubetuTaiouChangeOyaAction(ByVal strClientId As String)

        Dim jr As New JibanRecordBase
        Dim ttRec As New TokubetuTaiouRecordBase            '��ʏ��
        Dim ttList As New List(Of TokubetuTaiouRecordBase)  '��ʏ��
        Dim intTmpKingakuAction As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION
        Dim intCnt As Integer = 0

        '�n�Ճf�[�^�̎擾
        jr = Me.GetCtrlFromJibanRec()

        '���ʑΉ��f�[�^�̎擾
        ttList = GetRowCtrlToList()

        '�擾���ʂ����Ƃɐݒ����𔻒肷��
        Dim blnSyouhin1Henkou As Boolean = IIf(Me.HiddenTokutaiKkkHaneiFlg.Value = "1", True, False)
        intTmpKingakuAction = cbLogic.pf_ChkTokubetuTaiouKkk(Me, ttList, jr, blnSyouhin1Henkou)
        '���`�F�b�N���C�x���g�ł̓G���[MSG�͕\�����Ȃ�
        'If intTmpKingakuAction <= EarthEnum.emKingakuAction.KINGAKU_ALERT Then
        '    MLogic.AlertMessage(Me, Messages.MSG200W.Replace("@PARAM1", cbLogic.AccTokubetuTaiouKkkMsg), 0, "KkkException")
        'End If

        '�`�F�b�N����Ă���S�s�̐ݒ����Ď擾
        Me.ps_SetSetteiSaki(ttList)

        '�t�H�[�J�X�̍Đݒ�
        Dim tmpScript As String
        If strClientId <> String.Empty Then
            tmpScript = "objEBI('" & strClientId & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "focusSet", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �ݒ��Đݒ菈��(��\��)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSetSetteiSaki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSetSetteiSaki.ServerClick
        Me.SetCheckTokubetuTaiouChangeOyaAction(String.Empty)
    End Sub

#End Region

End Class