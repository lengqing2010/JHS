Imports Itis.Earth.BizLogic
Imports System.Data
''' <summary>
''' ������Ѓ}�X�^
''' </summary>
''' <history>
''' <para>2010/05/15�@�n���R(��A)�@�V�K�쐬</para>
''' </history>
Partial Public Class TyousaKaisyaMaster
    Inherits System.Web.UI.Page
    '�{�^��
    Private blnBtn As Boolean
    Private blnBtn1 As Boolean
    Private blnBtn2 As Boolean
    '�C���X�^���X����
    Private CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
    '���ʃ`�F�b�N
    Private commoncheck As New CommonCheck
    '�C���X�^���X����
    Private TyousaKaisyaMasterBL As New Itis.Earth.BizLogic.TyousaKaisyaMasterLogic

    Private Const SEP_STRING As String = "$$$"
    '
    Private Const SEARCH_SEIKYUU_SAKI As String = "SeikyuuSakiMaster.aspx"

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '�����`�F�b�N��
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        '�����`�F�b�N����ѐݒ�
        blnBtn = commonChk.CommonNinnsyou(strUserID, "irai_gyoumu_kengen,keiri_gyoumu_kengen")
        blnBtn1 = commonChk.CommonNinnsyou(strUserID, "irai_gyoumu_kengen")
        blnBtn2 = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        ViewState("UserId") = strUserID
        '�����`�F�b�N��
        If Not IsPostBack Then
            'DDL�̏����ݒ�
            SetDdlListInf()
            '�V��v���Ə��R�[�h
            Me.tbxSkkJigyousyoCd.Text = "YMP8"
            Me.hidSkkJigyousyoCd.Value = "YMP8"

            '�C���{�^��
            btnSyuusei.Enabled = False
            '�o�^�{�^��
            btnTouroku.Enabled = True
            '������R
            tbxTorikesiRiyuu.Attributes.Add("readonly", "true;")
            '������V�K�o�^
            labSeikyuuSaki.Style.Add("display", "none")
            labHyouji.Style.Add("display", "none")

        Else
            '����Ǝ�����R�̏��
            If Me.hidTorikesi.Value = "true" Then
                Me.chkTorikesi.Checked = True
                tbxTorikesiRiyuu.Attributes.Remove("readonly")
            Else
                Me.chkTorikesi.Checked = False
                tbxTorikesiRiyuu.Attributes.Add("readonly", "true;")
            End If

            '�����挟���{�^��������
            If Me.hidConfirm.Value = "Hyouji" Then
                labSeikyuuSaki.Style.Add("display", "none")
                labHyouji.Style.Add("display", "none")

                Me.hidConfirm1.Value = "NO"

            End If
        End If

        '����`�F�b�N�{�b�N�X
        Me.chkTorikesi.Attributes.Add("onClick", "fncSetTorikesiVal();")
        '�X�֔ԍ�
        Me.tbxYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")
        'SS����i
        Me.tbxSSKijyunKkk.Attributes.Add("onblur", "checkNumberAddFig(this);")
        Me.tbxSSKijyunKkk.Attributes.Add("onfocus", "removeFig(this);")
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        ''�e�b ����N��
        'Me.tbxFCNyuukaiDate.Attributes.Add("onBlur", "fncCheckNengetu(this,'�e�b ����N��');")
        ''�e�b �މ�N��
        'Me.tbxFCTaikaiDate.Attributes.Add("onBlur", "fncCheckNengetu(this,'�e�b �މ�N��');")
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        '�i�`�o�`�m�� ����N��
        Me.tbxJapanKaiNyuukaiDate.Attributes.Add("onBlur", "fncCheckNengetu(this,'�i�`�o�`�m�� ����N��');")
        '�i�`�o�`�m�� �މ�N��
        Me.tbxJapanKaiTaikaiDate.Attributes.Add("onBlur", "fncCheckNengetu(this,'�i�`�o�`�m�� �މ�N��');")
        '���������t��X�֔ԍ�
        Me.tbxSkysySoufuYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")
        '�t�@�N�^�����O�J�n�N��
        Me.tbxFctringKaisiNengetu.Attributes.Add("onBlur", "fncCheckNengetu(this,'�t�@�N�^�����O�J�n�N��');")

        '������ЃR�[�h
        Me.tbxTyousaKaisyaCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxTyousaKaisyaCd.ClientID & "')")
        Me.tbxTyousaKaisyaCd.Attributes.Add("onblur", "fncblur('1','" & Me.tbxTyousaKaisyaCd.ClientID & "')")
        '���Ə��R�[�h
        Me.tbxJigyousyoCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxJigyousyoCd.ClientID & "')")
        Me.tbxJigyousyoCd.Attributes.Add("onblur", "fncblur('','" & Me.tbxJigyousyoCd.ClientID & "')")
        '������敪
        Me.ddlSeikyuuSaki.Attributes.Add("onfocus", "fncFocus('" & Me.ddlSeikyuuSaki.ClientID & "')")
        Me.ddlSeikyuuSaki.Attributes.Add("onblur", "fncblur('','" & Me.ddlSeikyuuSaki.ClientID & "')")
        '������R�[�h
        Me.tbxSeikyuuSakiCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
        Me.tbxSeikyuuSakiCd.Attributes.Add("onblur", "fncblur('','" & Me.tbxSeikyuuSakiCd.ClientID & "')")
        '������}��
        Me.tbxSeikyuuSakiBrc.Attributes.Add("onfocus", "fncFocus('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")
        Me.tbxSeikyuuSakiBrc.Attributes.Add("onblur", "fncblur('','" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

        '������Ж�
        Me.tbxTyousaKaisya_Mei.Attributes.Add("readonly", "true;")
        'FC�X
        Me.tbxFCTenMei.Attributes.Add("readonly", "true;")
        '�H���񍐏������ύX���O�C�����[�U
        Me.tbxKojHkksTyokusouUpdLoginUserId.Attributes.Add("readonly", "true;")
        '�H���񍐏������ύX����
        Me.tbxKojHkksTyokusouUpdDatetime.Attributes.Add("readonly", "true;")
        '���������Z���^�[
        'Me.tbxKensakuKensaCenterMei.Attributes.Add("readonly", "true;")
        '������
        Me.tbxSeikyuuSakiMei.Attributes.Add("readonly", "true;")
        '�V��v�x����
        Me.tbxSkkShriSakiMei.Attributes.Add("readonly", "true;")
        'SAP�p�d���於
        Me.tbxSiireSakiMei.Attributes.Add("readonly", "true;")
        '�x���W�v�掖�Ə�
        Me.tbxTysKaisyaCd.Attributes.Add("readonly", "true;")
        '�x���W�v�掖�Ə���
        Me.tbxTysKaisyaMei.Attributes.Add("readonly", "true;")
        '�x�����׏W�v�掖�Ə�
        Me.tbxTysMeisaiKaisyaCd.Attributes.Add("readonly", "true;")
        '�x�����׏W�v�掖�Ə���
        Me.tbxTysMeisaiKaisyaMei.Attributes.Add("readonly", "true;")

        '�o�^�{�^��
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncCheck01()){return false;}else{if(!fncCheck02()){return false;}else{if(!fncCheck03()){return false;}}};disableButton1();")

        '�C���{�^��
        Me.btnSyuusei.Attributes.Add("onClick", "if(!fncCheck01()){return false;}else{if(!fncCheck02()){return false;}else{if(!fncCheck03()){return false;}}};disableButton1();")

        '���׃N���A
        btnClearMeisai.Attributes.Add("onclick", "if(!confirm('�N���A���s�Ȃ��܂��B\n��낵���ł����H')){return false;};disableButton1();")

        '�����於�E���t�Z���ɃR�s�[
        btnKensakuSeikyuuSoufuCopy.Attributes.Add("onclick", "if(!confirm('�����於�E���t�Z���ɏ㏑���R�s�[���܂��B\n��낵���ł����H')){return false;};disableButton1();")

        btnSearchTyousaKaisya.Attributes.Add("onclick", "disableButton1();")
        btnSearch.Attributes.Add("onclick", "disableButton1();")
        btnClear.Attributes.Add("onclick", "disableButton1();")
        btnKensakuYuubinNo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuFCTen.Attributes.Add("onclick", "disableButton1();")
        'btnKensakuKensaCenter.Attributes.Add("onclick", "disableButton1();")

        '======================2011/06/27 �ԗ� �C�� �J�n��=================================
        'btnKensakuSeikyuuSaki.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSaki.Attributes.Add("onclick", "if(!fncSeikyuusakiChangeCheck()){return false;}else{disableButton1();}")
        '======================2011/06/27 �ԗ� �C�� �I����=================================

        btnKensakuSeikyuuSyousai.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSyo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSkkShriSaki.Attributes.Add("onclick", "disableButton1();")
        btnKensakuShriJigyousyo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuShriMeisaiJigyousyo.Attributes.Add("onclick", "disableButton1();")

        btnOK.Attributes.Add("onclick", "disableButton1();")
        btnNO.Attributes.Add("onclick", "disableButton1();")

        tbxSeikyuuSakiShriSakiKana.Attributes.Add("onblur", "fncTokomozi(this)")
        tbxTyousaKaisyaMeiKana.Attributes.Add("onblur", "fncTokomozi(this)")

        'JavaScript
        MakeScript()

        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        End If

        '�V��v���Ə��R�[�h�E�V��v�x����R�[�h�̌����ݒ�
        If blnBtn1 And Not blnBtn2 Then
            tbxSkkJigyousyoCd.Enabled = False
            tbxSkkShriSakiCd.Enabled = False
            btnKensakuSkkShriSaki.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' ������Ќ����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearchTyousaKaisya_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchTyousaKaisya.Click
        Dim strScript As String = ""
        '�f�[�^�擾
        Dim dtTyousaKaisyaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.tyousakaisyaTableDataTable
        dtTyousaKaisyaTable = TyousaKaisyaMasterBL.SelTyousaKaisya(tbxTyousaKaisya_Cd.Text, "", False)

        '�������ʂ�1���������ꍇ
        If dtTyousaKaisyaTable.Rows.Count = 1 Then
            tbxTyousaKaisya_Cd.Text = dtTyousaKaisyaTable.Item(0).tys_kaisya_cd.ToString & _
                                 dtTyousaKaisyaTable.Item(0).jigyousyo_cd.ToString
            tbxTyousaKaisya_Mei.Text = dtTyousaKaisyaTable.Item(0).tys_kaisya_mei
        Else
            tbxTyousaKaisya_Mei.Text = ""
            strScript = "objSrchWin = window.open('search_tyousa.aspx?Kbn='+escape('����')+'&soukoCd='+escape('#')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxTyousaKaisya_Cd.ClientID & _
                    "&objMei=" & tbxTyousaKaisya_Mei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxTyousaKaisya_Cd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxTyousaKaisya_Mei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' �i���ҏW�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strErr As String = ""

        If strErr = "" Then
            '���͕K�{
            strErr = commoncheck.CheckHissuNyuuryoku(tbxTyousaKaisya_Cd.Text, "������ЃR�[�h")
        End If
        If strErr = "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxTyousaKaisya_Cd.Text, "������ЃR�[�h")
        End If
        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & tbxTyousaKaisya_Cd.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        Else
            GetMeisaiData(tbxTyousaKaisya_Cd.Text, tbxTyousaKaisyaCd.Text, tbxJigyousyoCd.Text, "btnSearch")
        End If
    End Sub

    ''' <summary>
    ''' �o�^�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Dim strErr As String = ""
        Dim strID As String = ""
        Dim strHenkou As String = ""
        Dim strDisplayName As String = ""
        strID = InputCheck(strErr)

        '20101112 �n���R �u��ʂ̏Z���v�̐������`�F�b�N�͊O���Ă��܂��Ă��������B �폜�@��
        ''�X�֔ԍ����݃`�F�b�N
        'If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = TyousaKaisyaMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
        '        strID = tbxYuubinNo.ClientID
        '    End If
        'End If

        ''���������t��X�֔ԍ����݃`�F�b�N
        'If strErr = "" And Trim(tbxSkysySoufuYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = TyousaKaisyaMasterBL.SelYuubinInfo(Trim(tbxSkysySoufuYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        '20101112 �n���R �u��ʂ̏Z���v�̐������`�F�b�N�͊O���Ă��܂��Ă��������B �폜�@��

        '�c�Ə����݃`�F�b�N
        If strErr = "" And Trim(tbxFCTen.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = TyousaKaisyaMasterBL.SelEigyousyo(Trim(tbxFCTen.Text))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "�c�Ə��}�X�^").ToString
                strID = tbxFCTen.ClientID
                tbxFCTenMei.Text = ""
            End If
        End If

        If strErr = "" Then
            '�y������V�K�o�^�z���\������Ă���ꍇ�A������Ж��̃`�F�b�N���s��
            If Me.hidConfirm1.Value = "OK" Then
                If TrimNull(Me.tbxTyousaKaisyaMei.Text) = "" Then
                    strID = Me.tbxTyousaKaisyaMei.ClientID
                    strErr = Messages.Instance.MSG033E
                End If
            End If
        End If
        '�G���[�����鎞
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            Dim dtTyousaKaisyaTable As New Itis.Earth.DataAccess.TyousaKaisyaDataSet.m_tyousakaisyaDataTable
            dtTyousaKaisyaTable = TyousaKaisyaMasterBL.SelMTyousaKaisyaInfo("", tbxTyousaKaisyaCd.Text, tbxJigyousyoCd.Text, "btnTouroku")
            '�d���`�F�b�N
            If dtTyousaKaisyaTable.Rows.Count <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�}�X�^�[�ɏd���f�[�^�����݂��܂��B');document.getElementById('" & tbxTyousaKaisyaCd.ClientID & "').focus();", True)
                Return
            End If

            '�H���񍐏�������ύX�����ꍇ
            If TrimNull(ddlKojHkksTyokusouFlg.SelectedValue) = "" Then
                strHenkou = "NO"
            Else
                strHenkou = "YES"
                strDisplayName = TyousaKaisyaMasterBL.SelKoujiInfo(ViewState("UserId")).Rows(0).Item("DisplayName").ToString
                If strDisplayName = "" Then
                    strDisplayName = ViewState("UserId")
                End If
            End If

            Dim strTrue As String = ""
            '�y������V�K�o�^�z���\������Ă���ꍇ
            If Me.hidConfirm1.Value = "OK" Then
                strTrue = "OK"
            End If

            '�f�[�^�o�^
            If TyousaKaisyaMasterBL.InsTyousaKaisya(SetMeisaiData, strHenkou, strDisplayName, strTrue) Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "������Ѓ}�X�^") & "');"
                '�Ď擾
                GetMeisaiData("", tbxTyousaKaisyaCd.Text, tbxJigyousyoCd.Text, "btnTouroku")
            Else
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "������Ѓ}�X�^") & "');"
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If
    End Sub

    ''' <summary>
    ''' �C���{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click

        Dim strReturn As String = ""
        Dim strErr As String = ""
        Dim strID As String = ""
        Dim strHenkou As String = ""
        Dim strDisplayName As String = ""
        '�`�F�b�N
        strID = InputCheck(strErr)

        '20101112 �n���R �u��ʂ̏Z���v�̐������`�F�b�N�͊O���Ă��܂��Ă��������B �폜�@��
        ''�X�֔ԍ����݃`�F�b�N
        'If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = TyousaKaisyaMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count = 0 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
        '        strID = tbxYuubinNo.ClientID
        '    End If
        'End If

        ''���������t��X�֔ԍ����݃`�F�b�N
        'If strErr = "" And Trim(tbxSkysySoufuYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = TyousaKaisyaMasterBL.SelYuubinInfo(Trim(tbxSkysySoufuYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        '20101112 �n���R �u��ʂ̏Z���v�̐������`�F�b�N�͊O���Ă��܂��Ă��������B �폜�@��

        '�c�Ə����݃`�F�b�N
        If strErr = "" And Trim(tbxFCTen.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = TyousaKaisyaMasterBL.SelEigyousyo(Trim(tbxFCTen.Text))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "�c�Ə��}�X�^").ToString
                strID = tbxFCTen.ClientID
                tbxFCTenMei.Text = ""
            End If
        End If

        If strErr = "" Then
            '�y������V�K�o�^�z���\������Ă���ꍇ�A������Ж��̃`�F�b�N���s��
            If Me.hidConfirm1.Value = "OK" Then
                If TrimNull(Me.tbxTyousaKaisyaMei.Text) = "" Then
                    strID = Me.tbxTyousaKaisyaMei.ClientID
                    strErr = Messages.Instance.MSG033E
                End If
            End If
        End If
        '�G���[�����鎞
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            '�H���񍐏�������ύX�����ꍇ
            If hidKojHkksTyokusouFlg.Value = ddlKojHkksTyokusouFlg.SelectedValue Then
                strHenkou = "NO"
            Else
                strHenkou = "YES"
                strDisplayName = TyousaKaisyaMasterBL.SelKoujiInfo(ViewState("UserId")).Rows(0).Item("DisplayName").ToString
                If strDisplayName = "" Then
                    strDisplayName = ViewState("UserId")
                End If
            End If

            Dim strTrue As String = ""
            '�y������V�K�o�^�z���\������Ă���ꍇ
            If Me.hidConfirm1.Value = "OK" Then
                strTrue = "OK"
            End If

            '�X�V����
            strReturn = TyousaKaisyaMasterBL.UpdTyousaKaisya(SetMeisaiData, strHenkou, strDisplayName, strTrue)
            If strReturn = "0" Then
                '�X�V����
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "������Ѓ}�X�^") & "');"
                '��ʍĕ`�揈��
                GetMeisaiData("", tbxTyousaKaisyaCd.Text, tbxJigyousyoCd.Text, "btnSyuusei")
            ElseIf strReturn = "1" Then
                '�X�V���s
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "������Ѓ}�X�^") & "');"
            ElseIf strReturn = "2" Then
                '���݃`�F�b�N
                strErr = "alert('�Y���f�[�^�����݂��܂���B���ɍ폜����Ă���\��������܂��B');"
            Else
                '���̑�
                strErr = "alert('" & strReturn & "');"
            End If
            '���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If

    End Sub

    ''' <summary>
    ''' ������.�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuSeikyuuSaki_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki.Click

        '���������䋤�ʏ��� ������.�����{�^��������
        SetKasseika()

        Dim strScript As String = ""

        '��������擾
        Dim dtSeikyuuSakiTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTable1DataTable
        'dtSeikyuuSakiTable = CommonSearchLogic.GetSeikyuuSakiInfo("2", ddlSeikyuuSaki.SelectedValue, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, "", False)
        dtSeikyuuSakiTable = TyousaKaisyaMasterBL.SelVSeikyuuSakiInfo(ddlSeikyuuSaki.SelectedValue, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, False)

        '�������ʂ�1���������ꍇ
        If dtSeikyuuSakiTable.Rows.Count = 1 Then
            If dtSeikyuuSakiTable.Item(0).torikesi = "0" Then
                '������R�[�h
                tbxSeikyuuSakiCd.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_cd)
                hidSeikyuuSakiCd.Value = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_cd)
                '������}��
                tbxSeikyuuSakiBrc.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_brc)
                hidSeikyuuSakiBrc.Value = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_brc)
                '������敪
                SetDropSelect(ddlSeikyuuSaki, TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_kbn))
                hidSeikyuuSakiKbn.Value = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_kbn)
                '�����於
                tbxSeikyuuSakiMei.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_mei)

                hidConfirm2.Value = ""
            Else
                '�����於
                tbxSeikyuuSakiMei.Text = ""
                '���b�Z�[�W�\��
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�w�肵��������͎������Ă��܂��B');", True)
            End If
        ElseIf dtSeikyuuSakiTable.Rows.Count = 0 Then
            '�������ʂ�0���������ꍇ
            '���.������敪��"1�F�������" ���� ���.������ЃR�[�h�����.������R�[�h ���� ���.���Ə��R�[�h�����.������}�Ԃ̏ꍇ
            If (ddlSeikyuuSaki.SelectedValue = "1") And (tbxSeikyuuSakiCd.Text <> "") And (tbxSeikyuuSakiBrc.Text <> "") And (tbxTyousaKaisyaCd.Text.ToUpper = tbxSeikyuuSakiCd.Text.ToUpper) And (tbxJigyousyoCd.Text.ToUpper = tbxSeikyuuSakiBrc.Text.ToUpper) Then
                '���b�Z�[�W�\��
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "fncConfirm();", True)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "if(confirm('���̓��e�̓o�^���ɐ�����}�X�^�ɓo�^���܂����H')){ window.setTimeout('objEBI(\'" & Me.btnOK.ClientID & "\').click()',10);}else{ window.setTimeout('objEBI(\'" & Me.btnNO.ClientID & "\').click()',10);}; ", True)
            Else
                '�����於
                tbxSeikyuuSakiMei.Text = ""
                strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&&Kbn='+escape('������')+'&FormName=" & _
                                             Me.Page.Form.Name & "&objKbn=" & _
                                             ddlSeikyuuSaki.ClientID & _
                                             "&objHidKbn=" & hidSeikyuuSakiKbn.ClientID & _
                                             "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
                                             "&objHidCd=" & hidSeikyuuSakiCd.ClientID & _
                                             "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
                                             "&hidConfirm2=" & hidConfirm2.ClientID & _
                                             "&objHidBrc=" & hidSeikyuuSakiBrc.ClientID & _
                                             "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
                                             "&strKbn='+escape(eval('document.all.'+'" & _
                                             ddlSeikyuuSaki.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
            End If
        Else
            '�����於
            tbxSeikyuuSakiMei.Text = ""
            strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&Kbn='+escape('������')+'&FormName=" & _
                                             Me.Page.Form.Name & "&objKbn=" & _
                                             ddlSeikyuuSaki.ClientID & _
                                             "&objHidKbn=" & hidSeikyuuSakiKbn.ClientID & _
                                             "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
                                             "&objHidCd=" & hidSeikyuuSakiCd.ClientID & _
                                             "&hidConfirm2=" & hidConfirm2.ClientID & _
                                             "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
                                             "&objHidBrc=" & hidSeikyuuSakiBrc.ClientID & _
                                             "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
                                             "&strKbn='+escape(eval('document.all.'+'" & _
                                             ddlSeikyuuSaki.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �X�֔ԍ�.�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuYuubinNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuYuubinNo.Click
        '�Z��
        Dim data As DataSet
        Dim jyuusyo As String
        Dim jyuusyoMei As String
        Dim jyuusyoNo As String

        '�Z���擾
        Dim csScript As New StringBuilder

        data = (TyousaKaisyaMasterBL.GetMailAddress(Me.tbxYuubinNo.Text.Replace("-", String.Empty).Trim))
        If data.Tables(0).Rows.Count = 1 Then

            jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
            jyuusyoNo = jyuusyo.Split(",")(0)
            jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))
            If jyuusyoNo.Length > 3 Then
                jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
            End If

            csScript.AppendLine("if(document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
            csScript.AppendLine("if (!confirm('�����f�[�^������܂����㏑�����Ă�낵���ł����B')){}else{ " & vbCrLf)

            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)

            csScript.AppendLine("}else{" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)
        Else
            csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo.ClientID & "','" & Me.tbxJyuusyo1.ClientID & "','" & Me.tbxJyuusyo2.ClientID & "');")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openWindowYuubin", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' ���������t��X�֔ԍ�.�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuSeikyuuSyo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSyo.Click
        '�Z��
        Dim data As DataSet
        Dim jyuusyo As String
        Dim jyuusyoMei As String
        Dim jyuusyoNo As String

        '�Z���擾
        Dim csScript As New StringBuilder

        data = (TyousaKaisyaMasterBL.GetMailAddress(Me.tbxSkysySoufuYuubinNo.Text.Replace("-", String.Empty).Trim))
        If data.Tables(0).Rows.Count = 1 Then

            jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
            jyuusyoNo = jyuusyo.Split(",")(0)
            jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))
            If jyuusyoNo.Length > 3 Then
                jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
            End If

            csScript.AppendLine("if(document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
            csScript.AppendLine("if (!confirm('�����f�[�^������܂����㏑�����Ă�낵���ł����B')){}else{ " & vbCrLf)

            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)

            csScript.AppendLine("}else{" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)
        Else
            csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxSkysySoufuYuubinNo.ClientID & "','" & Me.tbxSkysySoufuJyuusyo1.ClientID & "','" & Me.tbxSkysySoufuJyuusyo2.ClientID & "');")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openWindowYuubin", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' FC�X�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuFCTen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuFCTen.Click
        Dim strScript As String = ""
        '�f�[�^�擾
        Dim dtKeiretuTable As New Itis.Earth.DataAccess.CommonSearchDataSet.EigyousyoTableDataTable
        dtKeiretuTable = TyousaKaisyaMasterBL.SelEigyousyo(tbxFCTen.Text)

        '�������ʂ�1���������ꍇ
        If dtKeiretuTable.Rows.Count = 1 Then
            tbxFCTen.Text = TrimNull(dtKeiretuTable.Item(0).eigyousyo_cd)
            tbxFCTenMei.Text = TrimNull(dtKeiretuTable.Item(0).eigyousyo_mei)
        Else
            tbxFCTenMei.Text = ""
            strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('�c�Ə�')+'&FormName=" & Form.Name & "&objCd=" & Me.tbxFCTen.ClientID & "&objMei=" & Me.tbxFCTenMei.ClientID & "&strCd='+escape(eval('document.all.'+'" & Me.tbxFCTen.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & Me.tbxFCTenMei.ClientID & "').value)+'&KensakuKubun=A&blnDelete=True', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    '''' <summary>
    '''' ���������Z���^�[�����{�^������������
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Protected Sub btnKensakuKensaCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuKensaCenter.Click
    '    Dim strScript As String = ""
    '    '�f�[�^�擾
    '    Dim dtFcTable As New DataTable
    '    dtFcTable = TyousaKaisyaMasterBL.SelMfcInfo(tbxKensakuKensaCenter.Text)

    '    '�������ʂ�1���������ꍇ
    '    If dtFcTable.Rows.Count = 1 Then
    '        tbxKensakuKensaCenter.Text = TrimNull(dtFcTable.Rows(0).Item("fc_cd"))
    '        hidKensakuKensaCenter.Value = TrimNull(dtFcTable.Rows(0).Item("fc_cd"))
    '        tbxKensakuKensaCenterMei.Text = TrimNull(dtFcTable.Rows(0).Item("fc_nm"))
    '    Else
    '        tbxKensakuKensaCenterMei.Text = ""
    '        strScript = "alert('�Y���̌����Z���^�[�͂���܂���B');"
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
    '    End If
    'End Sub

    ''' <summary>
    ''' �V��v�x����.�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuSkkShriSaki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuSkkShriSaki.Click
        Dim strScript As String = ""
        '�f�[�^�擾
        Dim dtSkkShriSakiTable As New Data.DataTable
        dtSkkShriSakiTable = TyousaKaisyaMasterBL.SelSKK(tbxSkkJigyousyoCd.Text, tbxSkkShriSakiCd.Text)

        '�������ʂ�1���������ꍇ
        If dtSkkShriSakiTable.Rows.Count = 1 Then
            '�V��v���Ə��R�[�h
            tbxSkkJigyousyoCd.Text = TrimNull(dtSkkShriSakiTable.Rows(0).Item("skk_jigyou_cd"))
            hidSkkJigyousyoCd.Value = TrimNull(dtSkkShriSakiTable.Rows(0).Item("skk_jigyou_cd"))
            '�V��v�x����R�[�h
            tbxSkkShriSakiCd.Text = TrimNull(dtSkkShriSakiTable.Rows(0).Item("skk_shri_saki_cd"))
            hidSkkShriSakiCd.Value = TrimNull(dtSkkShriSakiTable.Rows(0).Item("skk_shri_saki_cd"))
            '�V��v�x���於 
            tbxSkkShriSakiMei.Text = TrimNull(dtSkkShriSakiTable.Rows(0).Item("shri_saki_mei_kanji"))
        Else
            tbxSkkShriSakiMei.Text = ""
            strScript = "objSrchWin = window.open('search_SinkaikeiSiharaiSaki.aspx?Kbn='+escape('�V��v�x����')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxSkkJigyousyoCd.ClientID & _
           "&objCd2=" & tbxSkkShriSakiCd.ClientID & _
           "&objHidCd2=" & hidSkkShriSakiCd.ClientID & _
                    "&objMei=" & tbxSkkShriSakiMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxSkkJigyousyoCd.ClientID & "').value)+'&strCd2='+escape(eval('document.all.'+'" & _
                    tbxSkkShriSakiCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxSkkShriSakiMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub




    Protected Sub btnSiireSakiKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ds As DataSet = CommonSearchLogic.SelSAPSiireSaki(0, "", Me.tbxSiireSaki.Text, "", "a1_ktokk asc")

        If ds.Tables(1).Rows.Count = 1 Then

            Me.tbxSiireSaki.Text = ds.Tables(1).Rows(0).Item(1).ToString
            Me.tbxSiireSakiMei.Text = ds.Tables(1).Rows(0).Item(2).ToString




        Else
            Dim strScript As String = "window.open('search_SAPSiireSaki.aspx', 'searchWindow2', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)

        End If


    End Sub




    ''' <summary>
    ''' �x���W�v�掖�Ə������{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuShriJigyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuShriJigyousyo.Click
        Dim strScript As String = ""
        '�f�[�^�擾
        Dim dtTyousaKaisyaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.tyousakaisyaTableDataTable
        'dtTyousaKaisyaTable = CommonSearchLogic.GetTyousaInfo("2", tbxTysKaisyaCd.Text & tbxShriJigyousyoCd.Text, "", "")
        dtTyousaKaisyaTable = TyousaKaisyaMasterBL.SelTyousaKaisya(tbxTysKaisyaCd.Text, tbxShriJigyousyoCd.Text, True)

        '�������ʂ�1���������ꍇ
        If dtTyousaKaisyaTable.Rows.Count = 1 Then
            tbxShriJigyousyoCd.Text = TrimNull(dtTyousaKaisyaTable.Item(0).jigyousyo_cd)
            hidShriJigyousyoCd.Value = TrimNull(dtTyousaKaisyaTable.Item(0).jigyousyo_cd)
            tbxTysKaisyaMei.Text = TrimNull(dtTyousaKaisyaTable.Item(0).tys_kaisya_mei)
        Else
            tbxTysKaisyaMei.Text = ""
            strScript = "objSrchWin = window.open('search_SiharaiTyousa.aspx?Kbn='+escape('�x���W�v�掖�Ə�')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxTysKaisyaCd.ClientID & _
           "&objCd2=" & tbxShriJigyousyoCd.ClientID & _
           "&objHidCd2=" & hidShriJigyousyoCd.ClientID & _
                    "&objMei=" & tbxTysKaisyaMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxTysKaisyaCd.ClientID & "').value)+'&strCd2='+escape(eval('document.all.'+'" & _
                    tbxShriJigyousyoCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxTysKaisyaMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' �x�����׏W�v�掖�Ə������{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuShriMeisaiJigyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuShriMeisaiJigyousyo.Click
        Dim strScript As String = ""
        '�f�[�^�擾
        Dim dtTyousaKaisyaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.tyousakaisyaTableDataTable
        'dtTyousaKaisyaTable = CommonSearchLogic.GetTyousaInfo("2", tbxTysMeisaiKaisyaCd.Text & tbxShriMeisaiJigyousyoCd.Text, "", "")
        dtTyousaKaisyaTable = TyousaKaisyaMasterBL.SelTyousaKaisya(tbxTysMeisaiKaisyaCd.Text, tbxShriMeisaiJigyousyoCd.Text, True)

        '�������ʂ�1���������ꍇ
        If dtTyousaKaisyaTable.Rows.Count = 1 Then
            tbxShriMeisaiJigyousyoCd.Text = TrimNull(dtTyousaKaisyaTable.Item(0).jigyousyo_cd)
            hidShriMeisaiJigyousyoCd.Value = TrimNull(dtTyousaKaisyaTable.Item(0).jigyousyo_cd)
            tbxTysMeisaiKaisyaMei.Text = TrimNull(dtTyousaKaisyaTable.Item(0).tys_kaisya_mei)
        Else
            tbxTysMeisaiKaisyaMei.Text = ""
            strScript = "objSrchWin = window.open('search_SiharaiTyousa.aspx?Kbn='+escape('�x�����׏W�v�掖�Ə�')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxTysMeisaiKaisyaCd.ClientID & _
           "&objCd2=" & tbxShriMeisaiJigyousyoCd.ClientID & _
           "&objHidCd2=" & hidShriMeisaiJigyousyoCd.ClientID & _
                    "&objMei=" & tbxTysMeisaiKaisyaMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxTysMeisaiKaisyaCd.ClientID & "').value)+'&strCd2='+escape(eval('document.all.'+'" & _
                    tbxShriMeisaiJigyousyoCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxTysMeisaiKaisyaMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '���
            .AppendLine("function fncSetTorikesiVal()")
            .AppendLine("{")
            .AppendLine("   var objTorikesi = document.getElementById('" & Me.chkTorikesi.ClientID & "')")
            .AppendLine("   var objTorikesiRiyuu = document.getElementById('" & Me.tbxTorikesiRiyuu.ClientID & "')")
            .AppendLine("   var hidTorikesi = document.getElementById('" & Me.hidTorikesi.ClientID & "')")
            .AppendLine("   if(objTorikesi.checked == true){")
            .AppendLine("       objTorikesiRiyuu.readOnly = false;")
            .AppendLine("       hidTorikesi.value = 'true';")
            .AppendLine("   }else{")
            .AppendLine("       objTorikesiRiyuu.value='';")
            .AppendLine("       objTorikesiRiyuu.readOnly = true;")
            .AppendLine("       hidTorikesi.value = 'false';")
            .AppendLine("   }")
            .AppendLine("}")
            '�X�֔ԍ�
            .AppendLine("function SetYuubinNo(e)")
            .AppendLine("{")
            .AppendLine("   var val;")
            .AppendLine("   var val2;")
            .AppendLine("   val = e.value;")
            .AppendLine("   arr = val.split('-');")
            .AppendLine("   val = arr.join('');")
            .AppendLine("   if (val.length>=3){")
            .AppendLine("       val2 = val.substring(0,3) + '-' + val.substring(3,val.length);")
            .AppendLine("   }else{")
            .AppendLine("       val2 =val;")
            .AppendLine("   }")
            .AppendLine("   e.value = val2.replace(/(^\s*)|(\s*$)/g,'');")
            .AppendLine("}")
            '���t�`�F�b�N(yyyy/mm)
            .AppendLine("   function fncCheckNengetu(obj,objId){")
            .AppendLine("   	if (obj.value==''){return true;}")
            .AppendLine("   	var checkFlg = true;")
            .AppendLine("   	obj.value = obj.value.Trim();")
            .AppendLine("   	var val = obj.value;")
            .AppendLine("   	val = SetDateNoSign(val,'/');")
            .AppendLine("   	val = SetDateNoSign(val,'-');")
            .AppendLine("   	val = val+'01';")
            .AppendLine("   	if(val == '')return;")
            .AppendLine("   	val = removeSlash(val);")
            .AppendLine("   	val = val.replace(/\-/g, '');")
            .AppendLine("   	if(val.length == 6){")
            .AppendLine("   		if(val.substring(0, 2) > 70){")
            .AppendLine("   			val = '19' + val;")
            .AppendLine("   		}else{")
            .AppendLine("   			val = '20' + val;")
            .AppendLine("   		}")
            .AppendLine("   	}else if(val.length == 4){")
            .AppendLine("   		dd = new Date();")
            .AppendLine("   		year = dd.getFullYear();")
            .AppendLine("   		val = year + val;")
            .AppendLine("   	}")
            .AppendLine("   	if(val.length != 8){")
            .AppendLine("   		checkFlg = false;")
            .AppendLine("   	}else{  //8���̏ꍇ")
            .AppendLine("   		val = addSlash(val);")
            .AppendLine("   		var arrD = val.split('/');")
            .AppendLine("   		if(checkDateVali(arrD[0],arrD[1],arrD[2]) == false){")
            .AppendLine("   			checkFlg = false; ")
            .AppendLine("   		}")
            .AppendLine("   	}")
            .AppendLine("   	if(!checkFlg){")
            .AppendLine("   		event.returnValue = false;")
            .AppendLine("           if (objId == '�e�b ����N��'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "�e�b ����N��").ToString & "');")
            .AppendLine("           }else if(objId == '�e�b �މ�N��'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "�e�b �މ�N��").ToString & "');")
            .AppendLine("           }else if(objId == '�i�`�o�`�m�� ����N��'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "�i�`�o�`�m�� ����N��").ToString & "');")
            .AppendLine("           }else if(objId == '�i�`�o�`�m�� �މ�N��'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "�i�`�o�`�m�� �މ�N��").ToString & "');")
            .AppendLine("           }else if(objId == '�t�@�N�^�����O�J�n�N��'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "�t�@�N�^�����O�J�n�N��").ToString & "');")
            .AppendLine("           }")
            .AppendLine("           obj.focus();")
            .AppendLine("   		obj.select();")
            .AppendLine("   		return false;")
            .AppendLine("   	}else{")
            .AppendLine("   		obj.value = val.substring(0,7);")
            .AppendLine("   	}")
            .AppendLine("   }")
            .AppendLine("   function SetDateNoSign(value,sign){")
            .AppendLine("   	var arr;")
            .AppendLine("   	arr = value.split(sign);")
            .AppendLine("   	var i;")
            .AppendLine("   	for(i=0;i<=arr.length-1;i++){")
            .AppendLine("   		if(arr[i].length==1){")
            .AppendLine("   			arr[i] = '0' + arr[i];        ")
            .AppendLine("   		}")
            .AppendLine("   	}")
            .AppendLine("   	return arr.join('');")
            .AppendLine("   } ")
            '�l�ύX���ɁA�u�x���W�v���ЃR�[�h�v�Ɓu�x�����׏W�v���ЃR�[�h�v�ɓ����l���R�s�[����
            .AppendLine("function fncSetCopy()")
            .AppendLine("{")
            .AppendLine("   var objTyousaKaisyaCd = document.getElementById('" & Me.tbxTyousaKaisyaCd.ClientID & "')")
            .AppendLine("   var objTysKaisyaCd = document.getElementById('" & Me.tbxTysKaisyaCd.ClientID & "')")
            .AppendLine("   var objTysMeisaiKaisyaCd = document.getElementById('" & Me.tbxTysMeisaiKaisyaCd.ClientID & "')")
            .AppendLine("   objTysKaisyaCd.value = objTyousaKaisyaCd.value;")
            .AppendLine("   objTysMeisaiKaisyaCd.value = objTyousaKaisyaCd.value;")
            .AppendLine("}")

            '���̑��`�F�b�NCHK03
            .AppendLine("function fncCheck03()")
            .AppendLine("{")
            '�����挟���{�^���֘A
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var hidSeikyuuSakiCd = document.getElementById('" & Me.hidSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var hidSeikyuuSakiBrc = document.getElementById('" & Me.hidSeikyuuSakiBrc.ClientID & "')")
            .AppendLine("   var hidSeikyuuSakiKbn = document.getElementById('" & Me.hidSeikyuuSakiKbn.ClientID & "')")
            '���������Z���^�[�{�^���֘A
            '.AppendLine("   var tbxKensakuKensaCenter = document.getElementById('" & Me.tbxKensakuKensaCenter.ClientID & "')")
            .AppendLine("   var hidKensakuKensaCenter = document.getElementById('" & Me.hidKensakuKensaCenter.ClientID & "')")
            .AppendLine("   var tbxSkkShriSakiCd = document.getElementById('" & Me.tbxSkkShriSakiCd.ClientID & "')")
            .AppendLine("   var hidSkkShriSakiCd = document.getElementById('" & Me.hidSkkShriSakiCd.ClientID & "')")
            '�x���W�v�掖�Ə������֘A
            .AppendLine("   var tbxShriJigyousyoCd = document.getElementById('" & Me.tbxShriJigyousyoCd.ClientID & "')")
            .AppendLine("   var hidShriJigyousyoCd = document.getElementById('" & Me.hidShriJigyousyoCd.ClientID & "')")
            '�x�����׏W�v�掖�Ə������֘A
            .AppendLine("   var tbxShriMeisaiJigyousyoCd = document.getElementById('" & Me.tbxShriMeisaiJigyousyoCd.ClientID & "')")
            .AppendLine("   var hidShriMeisaiJigyousyoCd = document.getElementById('" & Me.hidShriMeisaiJigyousyoCd.ClientID & "')")

            .AppendLine("   var tbxSkkShriSakiMei = document.getElementById('" & Me.tbxSkkShriSakiMei.ClientID & "')")
            .AppendLine("   var tbxTysKaisyaMei = document.getElementById('" & Me.tbxTysKaisyaMei.ClientID & "')")
            .AppendLine("   var tbxTysMeisaiKaisyaMei = document.getElementById('" & Me.tbxTysMeisaiKaisyaMei.ClientID & "')")
            '.AppendLine("   var tbxKensakuKensaCenterMei = document.getElementById('" & Me.tbxKensakuKensaCenterMei.ClientID & "')")

            '������
            .AppendLine("   var hidConfirm2 = document.getElementById('" & Me.hidConfirm2.ClientID & "')")
            .AppendLine("   if((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!='')){")
            .AppendLine("   if(hidConfirm2.value=='����'){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "������") & "');")
            .AppendLine("       ddlSeikyuuSaki.focus();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   }")

            .AppendLine("   if((ddlSeikyuuSaki.value != hidSeikyuuSakiKbn.value)&&((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!=''))){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "������敪") & "');")
            .AppendLine("       ddlSeikyuuSaki.focus();")
            .AppendLine("       return false;")



            .AppendLine("   }else if((tbxSeikyuuSakiCd.value != hidSeikyuuSakiCd.value)&&((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!=''))){")

            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "������R�[�h") & "');")
            .AppendLine("       tbxSeikyuuSakiCd.focus();")
            .AppendLine("       return false;")
            .AppendLine("   }else if((tbxSeikyuuSakiBrc.value != hidSeikyuuSakiBrc.value)&&((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!=''))){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "������}��") & "');")
            .AppendLine("       tbxSeikyuuSakiBrc.focus();")
            .AppendLine("       return false;")

            .AppendLine("   }else if((tbxSkkShriSakiCd.value != hidSkkShriSakiCd.value)&&(tbxSkkShriSakiCd.value !='')){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "�V��v�x����R�[�h") & "');")
            .AppendLine("       tbxSkkShriSakiMei.value='';")
            .AppendLine("       tbxSkkShriSakiCd.focus();")
            .AppendLine("       return false;")
            '�x���W�v�掖�Ə�
            .AppendLine("   }else if((tbxShriJigyousyoCd.value != hidShriJigyousyoCd.value)&&(tbxShriJigyousyoCd.value != '')){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "�x���W�v�掖�Ə��R�[�h") & "');")
            .AppendLine("       tbxTysKaisyaMei.value='';")
            .AppendLine("       tbxShriJigyousyoCd.focus();")
            .AppendLine("       return false;")
            '�x�����׏W�v�掖�Ə�
            .AppendLine("   }else if((tbxShriMeisaiJigyousyoCd.value != hidShriMeisaiJigyousyoCd.value)&&(tbxShriMeisaiJigyousyoCd.value != '')){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "�x�����׏W�v�掖�Ə��R�[�h") & "');")
            .AppendLine("       tbxTysMeisaiKaisyaMei.value='';")
            .AppendLine("       tbxShriMeisaiJigyousyoCd.focus();")
            .AppendLine("       return false;")
            '���������Z���^�[
            '.AppendLine("   }else if((tbxKensakuKensaCenter.value != hidKensakuKensaCenter.value)&&(tbxKensakuKensaCenter.value != '')){")
            '.AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "���������Z���^�[�R�[�h") & "');")
            '.AppendLine("       tbxKensakuKensaCenterMei.value='';")
            '.AppendLine("       tbxKensakuKensaCenter.focus();")
            '.AppendLine("       return false;")
            .AppendLine("   }else{")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("}")

            '���̑��`�F�b�NCHK01
            .AppendLine("function fncCheck01()")
            .AppendLine("{")
            .AppendLine("   var ddlTktJbnTysSyuninSkkFlg = document.getElementById('" & Me.ddlTktJbnTysSyuninSkkFlg.ClientID & "')")
            .AppendLine("   var ddlTyousaGyoumu = document.getElementById('" & Me.ddlTyousaGyoumu.ClientID & "')")
            .AppendLine("   if((ddlTktJbnTysSyuninSkkFlg.value == '0')&&(ddlTyousaGyoumu.value == '1')){")
            .AppendLine("       alert('" & Messages.Instance.MSG031E & "');")
            .AppendLine("       ddlTyousaGyoumu.focus();")
            .AppendLine("       return false;")
            .AppendLine("   }else{")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("}")

            '���̑��`�F�b�NCHK02
            .AppendLine("function fncCheck02()")
            .AppendLine("{")
            .AppendLine("   var chkTorikesi = document.getElementById('" & Me.chkTorikesi.ClientID & "')")
            .AppendLine("   var tbxTorikesiRiyuu = document.getElementById('" & Me.tbxTorikesiRiyuu.ClientID & "')")
            .AppendLine("   if((chkTorikesi.checked == true)&&(tbxTorikesiRiyuu.value == '')){")
            .AppendLine("       alert('" & Messages.Instance.MSG032E & "');")
            .AppendLine("       tbxTorikesiRiyuu.focus();")
            .AppendLine("       return false;")
            .AppendLine("   }else{")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("}")



            '�X�ւ̎擾
            .AppendLine("function fncOpenwindowYuubin(id1,mei1,mei2)" & vbCrLf)
            .AppendLine("{" & vbCrLf)
            .AppendLine("var strkbn='�X��';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & _
            Me.Page.Form.Name & _
            "&objCd=" & _
            "'+escape(id1)+'" & _
            "&objMei=" & _
            "'+mei1+'" & _
            "&objMei2=" & _
            "'+mei2+'" & _
            "&strCd='+escape(eval('document.all.'+" & _
            " id1 +'" & "').value)" & _
            "+'&strMei='+escape(eval('document.all.'+" & _
            " mei1 " & ").innerText)" & _
            "+'&strMei2='+escape(eval('document.all.'+" & _
            " mei2 " & ").innerText)" & _
            ", 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)

            '�����挟���{�^��������
            .AppendLine("function fncConfirm()")
            .AppendLine("{")
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   if(confirm('���̒�����Гo�^���ɐ�����}�X�^�ɓo�^���܂����H')){")
            .AppendLine("       hidConfirm.value = 'OK';")
            .AppendLine("   }else{")
            .AppendLine("       hidConfirm.value = 'NO';")
            .AppendLine("   }")
            .AppendLine("   document.getElementById('" & Me.Form.Name & "').submit();")
            .AppendLine("}")

            '�u������V�K�o�^�v�����\�����ʏ���
            .AppendLine("function fncHyouji()")
            .AppendLine("{")
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   var labSeikyuuSaki = document.getElementById('" & Me.labSeikyuuSaki.ClientID & "')")
            .AppendLine("   var labHyouji = document.getElementById('" & Me.labHyouji.ClientID & "')")
            .AppendLine("   var hidConfirm1 = document.getElementById('" & Me.hidConfirm1.ClientID & "')")
            .AppendLine("   labSeikyuuSaki.style.visibility = 'hidden';")
            .AppendLine("   labHyouji.style.visibility = 'hidden';")
            .AppendLine("   hidConfirm.value = 'Hyouji';")
            .AppendLine("   hidConfirm1.value = 'NO';")
            .AppendLine("}")

            ''�R�s�[
            '.AppendLine("function fncFocus(obj)")
            '.AppendLine("{")
            '.AppendLine("   var tbxTyousaKaisyaCd = document.getElementById(obj)")
            '.AppendLine("   var hidChange = document.getElementById('" & Me.hidChange.ClientID & "')")
            '.AppendLine("   hidChange.value = tbxTyousaKaisyaCd.value;")
            '.AppendLine("}")
            ''�R�s�[
            '.AppendLine("function fncblur(kbn,obj)")
            '.AppendLine("{")
            '.AppendLine("   var tbxTyousaKaisyaCd = document.getElementById(obj)")
            '.AppendLine("   var hidChange = document.getElementById('" & Me.hidChange.ClientID & "')")
            '.AppendLine("   if(tbxTyousaKaisyaCd.value != hidChange.value){")
            '.AppendLine("       if(kbn=='1'){")
            '.AppendLine("           fncSetCopy();")
            '.AppendLine("           fncHyouji();")
            '.AppendLine("       }else{")
            '.AppendLine("           fncHyouji();")
            '.AppendLine("       }")
            '.AppendLine("   }")
            '.AppendLine("}")

            '�R�s�[
            .AppendLine("function fncFocus()")
            .AppendLine("{")
            .AppendLine("   var tbxTyousaKaisyaCd = document.getElementById('" & Me.tbxTyousaKaisyaCd.ClientID & "')")
            .AppendLine("   var tbxJigyousyoCd = document.getElementById('" & Me.tbxJigyousyoCd.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

            .AppendLine("   var hidChange1 = document.getElementById('" & Me.hidChange1.ClientID & "')")
            .AppendLine("   var hidChange2 = document.getElementById('" & Me.hidChange2.ClientID & "')")
            .AppendLine("   var hidChange3 = document.getElementById('" & Me.hidChange3.ClientID & "')")
            .AppendLine("   var hidChange4 = document.getElementById('" & Me.hidChange4.ClientID & "')")
            .AppendLine("   var hidChange5 = document.getElementById('" & Me.hidChange5.ClientID & "')")
            .AppendLine("   hidChange1.value = tbxTyousaKaisyaCd.value;")
            .AppendLine("   hidChange2.value = tbxJigyousyoCd.value;")
            .AppendLine("   hidChange3.value = ddlSeikyuuSaki.value;")
            .AppendLine("   hidChange4.value = tbxSeikyuuSakiCd.value;")
            .AppendLine("   hidChange5.value = tbxSeikyuuSakiBrc.value;")
            .AppendLine("}")
            '�R�s�[
            .AppendLine("function fncblur(kbn)")
            .AppendLine("{")
            .AppendLine("   var tbxTyousaKaisyaCd = document.getElementById('" & Me.tbxTyousaKaisyaCd.ClientID & "')")
            .AppendLine("   var tbxJigyousyoCd = document.getElementById('" & Me.tbxJigyousyoCd.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

            .AppendLine("   var hidChange1 = document.getElementById('" & Me.hidChange1.ClientID & "')")
            .AppendLine("   var hidChange2 = document.getElementById('" & Me.hidChange2.ClientID & "')")
            .AppendLine("   var hidChange3 = document.getElementById('" & Me.hidChange3.ClientID & "')")
            .AppendLine("   var hidChange4 = document.getElementById('" & Me.hidChange4.ClientID & "')")
            .AppendLine("   var hidChange5 = document.getElementById('" & Me.hidChange5.ClientID & "')")

            .AppendLine("   var hidConfirm1 = document.getElementById('" & Me.hidConfirm1.ClientID & "')")

            .AppendLine("   var hidConfirm2 = document.getElementById('" & Me.hidConfirm2.ClientID & "')")

            .AppendLine("   if(hidConfirm1.value=='OK'){")
            .AppendLine("   if((tbxTyousaKaisyaCd.value != hidChange1.value)||(tbxJigyousyoCd.value != hidChange2.value)||(ddlSeikyuuSaki.value != hidChange3.value)||(tbxSeikyuuSakiCd.value != hidChange4.value)||(tbxSeikyuuSakiBrc.value != hidChange5.value)){")
            .AppendLine("       if(kbn=='1'){")
            .AppendLine("           fncSetCopy();")
            .AppendLine("           fncHyouji();")
            .AppendLine("       }else{")
            .AppendLine("           fncHyouji();")
            .AppendLine("       }")
            .AppendLine("   hidConfirm2.value='����';")
            .AppendLine("   }")
            .AppendLine("  }else{")
            .AppendLine("       if(kbn=='1'){")
            .AppendLine("           fncSetCopy();")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("}")

            '��{���Z�b�g
            .AppendLine("function fncDisable()")
            .AppendLine("{")
            .AppendLine("   var btnSearchTyousaKaisya = document.getElementById('" & Me.btnSearchTyousaKaisya.ClientID & "')")
            .AppendLine("   var btnSearch = document.getElementById('" & Me.btnSearch.ClientID & "')")
            .AppendLine("   var btnClear = document.getElementById('" & Me.btnClear.ClientID & "')")
            .AppendLine("   var btnSyuusei = document.getElementById('" & Me.btnSyuusei.ClientID & "')")
            .AppendLine("   var btnTouroku = document.getElementById('" & Me.btnTouroku.ClientID & "')")
            .AppendLine("   var btnClearMeisai = document.getElementById('" & Me.btnClearMeisai.ClientID & "')")
            .AppendLine("   var btnKensakuYuubinNo = document.getElementById('" & Me.btnKensakuYuubinNo.ClientID & "')")
            .AppendLine("   var btnKensakuFCTen = document.getElementById('" & Me.btnKensakuFCTen.ClientID & "')")
            ' .AppendLine("   var btnKensakuKensaCenter = document.getElementById('" & Me.btnKensakuKensaCenter.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSaki = document.getElementById('" & Me.btnKensakuSeikyuuSaki.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSyousai = document.getElementById('" & Me.btnKensakuSeikyuuSyousai.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSoufuCopy = document.getElementById('" & Me.btnKensakuSeikyuuSoufuCopy.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSyo = document.getElementById('" & Me.btnKensakuSeikyuuSyo.ClientID & "')")
            .AppendLine("   var btnKensakuSkkShriSaki = document.getElementById('" & Me.btnKensakuSkkShriSaki.ClientID & "')")
            .AppendLine("   var btnKensakuShriJigyousyo = document.getElementById('" & Me.btnKensakuShriJigyousyo.ClientID & "')")
            .AppendLine("   var btnKensakuShriMeisaiJigyousyo = document.getElementById('" & Me.btnKensakuShriMeisaiJigyousyo.ClientID & "')")

            .AppendLine("   var btnOK = document.getElementById('" & Me.btnOK.ClientID & "')")
            .AppendLine("   var btnNO = document.getElementById('" & Me.btnNO.ClientID & "')")

            .AppendLine("   var my_array = new Array(17);")
            .AppendLine("   my_array[0] = btnSearchTyousaKaisya;")
            .AppendLine("   my_array[1] = btnSearch;")
            .AppendLine("   my_array[2] = btnClear;")
            .AppendLine("   my_array[3] = btnSyuusei;")
            .AppendLine("   my_array[4] = btnTouroku;")
            .AppendLine("   my_array[5] = btnClearMeisai;")
            .AppendLine("   my_array[6] = btnKensakuYuubinNo;")
            .AppendLine("   my_array[7] = btnKensakuFCTen;")
            '.AppendLine("   my_array[8] = btnKensakuKensaCenter;")
            .AppendLine("   my_array[9] = btnKensakuSeikyuuSaki;")
            .AppendLine("   my_array[10] = btnKensakuSeikyuuSyousai;")
            .AppendLine("   my_array[11] = btnKensakuSeikyuuSoufuCopy;")
            .AppendLine("   my_array[12] = btnKensakuSeikyuuSyo;")
            .AppendLine("   my_array[13] = btnKensakuSkkShriSaki;")
            .AppendLine("   my_array[14] = btnKensakuShriJigyousyo;")
            .AppendLine("   my_array[15] = btnKensakuShriMeisaiJigyousyo;")

            .AppendLine("   my_array[16] = btnOK;")
            .AppendLine("   my_array[17] = btnNO;")

            .AppendLine("   for (i = 0; i < 18; i++){")
            .AppendLine("       if(i != 8){my_array[i].disabled = true;}")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("function disableButton1()")
            .AppendLine("{")
            .AppendLine("   window.setTimeout('fncDisable()',0);")
            .AppendLine("   return true;")
            .AppendLine("}")

            '======================2011/06/27 �ԗ� �ǉ� �J�n��=================================
            .AppendLine("function fncSeikyuusakiChangeCheck() ")
            .AppendLine("{ ")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "'); ")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "'); ")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "'); ")
            .AppendLine("   var hidSeikyuuSakiCd = document.getElementById('" & Me.hidSeikyuuSakiCd.ClientID & "'); ")
            .AppendLine("   var hidSeikyuuSakiBrc = document.getElementById('" & Me.hidSeikyuuSakiBrc.ClientID & "'); ")
            .AppendLine("   var hidSeikyuuSakiKbn = document.getElementById('" & Me.hidSeikyuuSakiKbn.ClientID & "'); ")
            .AppendLine("   if((tbxSeikyuuSakiCd.value!=hidSeikyuuSakiCd.value)||(tbxSeikyuuSakiBrc.value!=hidSeikyuuSakiBrc.value)||(ddlSeikyuuSaki.value!=hidSeikyuuSakiKbn.value)) ")
            .AppendLine("   { ")
            .AppendLine("       if(confirm('�����悪�ύX����Ă��܂��B\r\n����������N���A���܂�����낵���ł����H')) ")
            .AppendLine("       { ")
            .AppendLine("           document.getElementById('" & Me.tbxSeikyuuSakiShriSakiMei.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSeikyuuSakiShriSakiKana.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSkysySoufuTelNo.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxShriYouFaxNo.ClientID & "').value = ''; ")
            .AppendLine("           return true; ")
            .AppendLine("       } ")
            .AppendLine("       else ")
            .AppendLine("       { ")
            .AppendLine("           tbxSeikyuuSakiCd.value = hidSeikyuuSakiCd.value; ")
            .AppendLine("           tbxSeikyuuSakiBrc.value = hidSeikyuuSakiBrc.value ")
            .AppendLine("           ddlSeikyuuSaki.value = hidSeikyuuSakiKbn.value ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            .AppendLine("   } ")
            .AppendLine("   else ")
            .AppendLine("   { ")
            .AppendLine("       return true; ")
            .AppendLine("   } ")
            .AppendLine("} ")
            '======================2011/06/27 �ԗ� �ǉ� �I����=================================
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    ''' <summary>
    ''' ���׃f�[�^���擾
    ''' </summary>
    ''' <param name="TyousaKaisya_Cd"></param>
    ''' <param name="btn"></param>
    ''' <remarks></remarks>
    Sub GetMeisaiData(ByVal TyousaKaisya_Cd As String, _
                      ByVal TyousaKaisyaCd As String, _
                      ByVal JigyousyoCd As String, _
                      Optional ByVal btn As String = "")

        Dim strErr As String = ""
        Dim dtTyousaKaisyaDataSet As New Itis.Earth.DataAccess.TyousaKaisyaDataSet.m_tyousakaisyaDataTable
        dtTyousaKaisyaDataSet = TyousaKaisyaMasterBL.SelMTyousaKaisyaInfo(TyousaKaisya_Cd, TyousaKaisyaCd, JigyousyoCd, btn)

        If dtTyousaKaisyaDataSet.Rows.Count = 1 Then
            With dtTyousaKaisyaDataSet.Item(0)
                '���
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '������ЃR�[�h
                tbxTyousaKaisyaCd.Text = TrimNull(.tys_kaisya_cd)
                '�������R�[�h
                tbxJigyousyoCd.Text = TrimNull(.jigyousyo_cd)

                '������R
                tbxTorikesiRiyuu.Text = TrimNull(.torikesi_riyuu)
                If chkTorikesi.Checked = True Then
                    Me.tbxTorikesiRiyuu.Attributes.Remove("readonly")
                    Me.hidTorikesi.Value = "true"
                Else
                    Me.tbxTorikesiRiyuu.Attributes.Add("readonly", "true;")
                    Me.hidTorikesi.Value = "false"
                End If

                '������Ж�
                tbxTyousaKaisyaMei.Text = TrimNull(.tys_kaisya_mei)
                If btn = "btnSearch" Then
                    tbxTyousaKaisya_Mei.Text = TrimNull(.tys_kaisya_mei)
                    tbxTyousaKaisya_Cd.Text = TrimNull(.tys_kaisya_cd).ToUpper & TrimNull(.jigyousyo_cd)
                End If

                '������Ж��J�i
                tbxTyousaKaisyaMeiKana.Text = TrimNull(.tys_kaisya_mei_kana)
                '�X�֔ԍ�
                tbxYuubinNo.Text = TrimNull(.yuubin_no)
                '�Z���P
                tbxJyuusyo1.Text = TrimNull(.jyuusyo1)
                '�d�b�ԍ�
                tbxTelNo.Text = TrimNull(.tel_no)
                '�Z���Q
                tbxJyuusyo2.Text = TrimNull(.jyuusyo2)
                'FAX�ԍ�
                tbxFaxNo.Text = TrimNull(.fax_no)
                'SS����i
                tbxSSKijyunKkk.Text = AddComa(.ss_kijyun_kkk)
                '�����Ɩ�
                SetDropSelect(ddlTyousaGyoumu, TrimNull(.tys_kaisya_flg))
                '�H���Ɩ�
                SetDropSelect(ddlKoujiGyoumu, TrimNull(.koj_kaisya_flg))
                'FC�X
                tbxFCTen.Text = TrimNull(.fc_ten_cd)
                tbxFCTenMei.Text = TrimNull(.keiretu_mei)
                '==============2012/04/12 �ԗ� 405738 �폜��====================
                ''�e�b�X�敪
                'SetDropSelect(ddlFCTenKbn, TrimNull(.fc_ten_kbn))
                ''�e�b ����N��
                'tbxFCNyuukaiDate.Text = toYYYYMM(.fc_nyuukai_date)
                ''�e�b �މ�N��
                'tbxFCTaikaiDate.Text = toYYYYMM(.fc_taikai_date)
                '==============2012/04/12 �ԗ� 405738 �폜��====================
                '�i�`�o�`�m��敪
                SetDropSelect(ddlJapanKbn, TrimNull(.japan_kai_kbn))
                '�i�`�o�`�m�� ����N��
                tbxJapanKaiNyuukaiDate.Text = toYYYYMM(.japan_kai_nyuukai_date)
                '�i�`�o�`�m�� �މ�N��
                tbxJapanKaiTaikaiDate.Text = toYYYYMM(.japan_kai_taikai_date)

                btxZenjyuhinHosoku.Text = TrimNull(.zenjyuhin_hosoku)
                '��n�n�Ւ�����C���i
                SetDropSelect(ddlTktJbnTysSyuninSkkFlg, TrimNull(.tkt_jbn_tys_syunin_skk_flg))
                '�q�|�i�g�r�g�[�N��
                SetDropSelect(ddlRJhsTokenFlg, TrimNull(.report_jhs_token_flg))
                '�H���񍐏�����
                SetDropSelect(ddlKojHkksTyokusouFlg, TrimNull(.koj_hkks_tyokusou_flg))
                hidKojHkksTyokusouFlg.Value = TrimNull(.koj_hkks_tyokusou_flg)

                '�H���񍐏������ύX���O�C�����[�U
                tbxKojHkksTyokusouUpdLoginUserId.Text = TrimNull(.koj_hkks_tyokusou_upd_login_user_id)
                '�H���񍐏������ύX����
                tbxKojHkksTyokusouUpdDatetime.Text = toYYYYMMDDHH(.koj_hkks_tyokusou_upd_datetime)

                '���������Z���^�[
                'tbxKensakuKensaCenter.Text = TrimNull(.kensa_center_cd)
                hidKensakuKensaCenter.Value = TrimNull(.kensa_center_cd)

                '���������Z���^�[��
                'tbxKensakuKensaCenterMei.Text = TrimNull(.fc_nm)

                '������敪
                SetDropSelect(ddlSeikyuuSaki, TrimNull(.seikyuu_saki_kbn))
                '������R�[�h
                tbxSeikyuuSakiCd.Text = TrimNull(.seikyuu_saki_cd).ToUpper
                '������}��
                tbxSeikyuuSakiBrc.Text = TrimNull(.seikyuu_saki_brc).ToUpper
                '�����於
                tbxSeikyuuSakiMei.Text = TrimNull(.seikyuu_saki_mei)
                '������R�[�h
                hidSeikyuuSakiCd.Value = TrimNull(.seikyuu_saki_cd).ToUpper
                '������}��
                hidSeikyuuSakiBrc.Value = TrimNull(.seikyuu_saki_brc).ToUpper
                '������敪
                hidSeikyuuSakiKbn.Value = TrimNull(.seikyuu_saki_kbn)

                '������x���於
                tbxSeikyuuSakiShriSakiMei.Text = TrimNull(.seikyuu_saki_shri_saki_mei)
                '������x���於�J�i
                tbxSeikyuuSakiShriSakiKana.Text = TrimNull(.seikyuu_saki_shri_saki_kana)
                '���������t��X�֔ԍ�
                tbxSkysySoufuYuubinNo.Text = TrimNull(.skysy_soufu_yuubin_no)
                '���������t��Z���P
                tbxSkysySoufuJyuusyo1.Text = TrimNull(.skysy_soufu_jyuusyo1)
                '���������t��d�b�ԍ�
                tbxSkysySoufuTelNo.Text = TrimNull(.skysy_soufu_tel_no)
                '���������t��Z���Q
                tbxSkysySoufuJyuusyo2.Text = TrimNull(.skysy_soufu_jyuusyo2)
                '�x���pFAX�ԍ�
                tbxShriYouFaxNo.Text = TrimNull(.shri_you_fax_no)

                '�V��v�x���掖�Ə��R�[�h
                tbxSkkJigyousyoCd.Text = TrimNull(.skk_jigyousyo_cd)
                hidSkkJigyousyoCd.Value = TrimNull(.skk_jigyousyo_cd)

                'SAP�p�d��
                Me.tbxSiireSaki.Text = TrimNull(.a1_lifnr)
                Me.tbxSiireSakiMei.Text = TrimNull(.a1_a_zz_sort)


                '�V��v�x����R�[�h
                tbxSkkShriSakiCd.Text = TrimNull(.skk_shri_saki_cd)
                hidSkkShriSakiCd.Value = TrimNull(.skk_shri_saki_cd)

                '�V��v�x���於
                tbxSkkShriSakiMei.Text = TrimNull(.shri_saki_mei_kanji)
                '�x�����ߓ�
                tbxShriSimeDate.Text = TrimNull(.shri_sime_date)
                '�x���\�茎��
                tbxShriYoteiGessuu.Text = TrimNull(.shri_yotei_gessuu)
                '�t�@�N�^�����O�J�n�N��
                tbxFctringKaisiNengetu.Text = toYYYYMM(.fctring_kaisi_nengetu)
                '�x���W�v�掖�Ə�
                tbxTysKaisyaCd.Text = TrimNull(.tys_kaisya_cd)
                '�x���W�v�掖�Ə�
                tbxShriJigyousyoCd.Text = TrimNull(.shri_jigyousyo_cd)
                hidShriJigyousyoCd.Value = TrimNull(.shri_jigyousyo_cd)

                '�x���W�v�掖�Ə���
                tbxTysKaisyaMei.Text = TrimNull(.shri_kaisya_mei)
                '�x�����׏W�v�掖�Ə�
                tbxTysMeisaiKaisyaCd.Text = TrimNull(.tys_kaisya_cd)
                '�x�����׏W�v�掖�Ə��R�[�h
                tbxShriMeisaiJigyousyoCd.Text = TrimNull(.shri_meisai_jigyousyo_cd)
                hidShriMeisaiJigyousyoCd.Value = TrimNull(.shri_meisai_jigyousyo_cd)

                '�x�����׏W�v�掖�Ə���
                tbxTysMeisaiKaisyaMei.Text = TrimNull(.shri_meisai_kaisya_mei)

                '============2012/04/12 �ԗ� 405721 �ǉ���==========================
                '��\�Җ�
                Me.tbxDaihyousyaMei.Text = TrimNull(.daihyousya_mei)
                '��E��
                Me.tbxYasyokuMei.Text = TrimNull(.yakusyoku_mei)
                '============2012/04/12 �ԗ� 405721 �ǉ���==========================

                '2013/11/04 ���F�ǉ� ��
                'SDS�ێ����
                SetDropSelect(Me.ddlSdsJyouhou, TrimNull(.sds_hoji_info))
                'SDS�䐔���
                Me.tbxSdsKiki.Text = TrimNull(.sds_daisuu_info)
                '2013/11/04 ���F�ǉ� ��

                If TrimNull(.jituzai_flg) = "1" Then
                    ddlJituzaiFlg.SelectedIndex = 1
                Else
                    ddlJituzaiFlg.SelectedIndex = 0
                End If

                hidUPDTime.Value = TrimNull(.upd_datetime)
                UpdatePanelA.Update()
            End With
            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
            Else
                btnSyuusei.Enabled = True
                btnTouroku.Enabled = False
            End If
            '�V��v���Ə��R�[�h�E�V��v�x����R�[�h�̌����ݒ�
            If blnBtn1 And Not blnBtn2 Then
                tbxSkkJigyousyoCd.Enabled = False
                tbxSkkShriSakiCd.Enabled = False
                btnKensakuSkkShriSaki.Enabled = False
            Else
                tbxSkkJigyousyoCd.Enabled = True
                tbxSkkShriSakiCd.Enabled = True
                btnKensakuSkkShriSaki.Enabled = True
            End If
            tbxTyousaKaisyaCd.Attributes.Add("readonly", "true;")
            tbxJigyousyoCd.Attributes.Add("readonly", "true;")
        Else
            MeisaiClear()
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            If btn <> "btnSearch" Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = True
                tbxTyousaKaisyaCd.Attributes.Remove("readonly")
                tbxJigyousyoCd.Attributes.Remove("readonly")
            End If
            tbxTyousaKaisya_Mei.Text = ""
        End If

        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")
        hidConfirm2.Value = ""

        If btn = "btnSearch" Or btn = "btnSyuusei" Or btn = "btnTouroku" Then
            tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
            tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
            tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
            tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
            tbxSkysySoufuTelNo.Attributes.Remove("readonly")
            tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
            tbxShriYouFaxNo.Attributes.Remove("readonly")
            btnKensakuSeikyuuSoufuCopy.Enabled = True
            btnKensakuSeikyuuSyo.Enabled = True
        End If

        hidSyousai.Value = ""
    End Sub

    ''' <summary>
    ''' �o�^�ƏC���l������
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetMeisaiData() As Itis.Earth.DataAccess.TyousaKaisyaDataSet.m_tyousakaisyaDataTable
        Dim dtTyousaKaisyaDataSet As New Itis.Earth.DataAccess.TyousaKaisyaDataSet.m_tyousakaisyaDataTable

        dtTyousaKaisyaDataSet.Rows.Add(dtTyousaKaisyaDataSet.NewRow)
        '���
        dtTyousaKaisyaDataSet.Item(0).torikesi = IIf(chkTorikesi.Checked, "1", "0")
        '������ЃR�[�h
        dtTyousaKaisyaDataSet.Item(0).tys_kaisya_cd = tbxTyousaKaisyaCd.Text.ToUpper
        '�������R�[�h
        dtTyousaKaisyaDataSet.Item(0).jigyousyo_cd = tbxJigyousyoCd.Text.ToUpper
        '������R
        dtTyousaKaisyaDataSet.Item(0).torikesi_riyuu = tbxTorikesiRiyuu.Text
        '������Ж�
        dtTyousaKaisyaDataSet.Item(0).tys_kaisya_mei = tbxTyousaKaisyaMei.Text
        '������Ж��J�i
        dtTyousaKaisyaDataSet.Item(0).tys_kaisya_mei_kana = tbxTyousaKaisyaMeiKana.Text
        '�X�֔ԍ�
        dtTyousaKaisyaDataSet.Item(0).yuubin_no = tbxYuubinNo.Text
        '�Z���P
        dtTyousaKaisyaDataSet.Item(0).jyuusyo1 = tbxJyuusyo1.Text
        '�d�b�ԍ�
        dtTyousaKaisyaDataSet.Item(0).tel_no = tbxTelNo.Text
        '�Z���Q
        dtTyousaKaisyaDataSet.Item(0).jyuusyo2 = tbxJyuusyo2.Text
        'FAX�ԍ�
        dtTyousaKaisyaDataSet.Item(0).fax_no = tbxFaxNo.Text
        'SS����i
        dtTyousaKaisyaDataSet.Item(0).ss_kijyun_kkk = Replace(tbxSSKijyunKkk.Text, ",", "")
        '�����Ɩ�
        dtTyousaKaisyaDataSet.Item(0).tys_kaisya_flg = ddlTyousaGyoumu.SelectedValue
        '�H���Ɩ�
        dtTyousaKaisyaDataSet.Item(0).koj_kaisya_flg = ddlKoujiGyoumu.SelectedValue
        'FC�X
        dtTyousaKaisyaDataSet.Item(0).fc_ten_cd = tbxFCTen.Text
        dtTyousaKaisyaDataSet.Item(0).keiretu_mei = tbxFCTenMei.Text
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        ''�e�b�X�敪
        'dtTyousaKaisyaDataSet.Item(0).fc_ten_kbn = ddlFCTenKbn.SelectedValue
        ''�e�b ����N��
        'dtTyousaKaisyaDataSet.Item(0).fc_nyuukai_date = tbxFCNyuukaiDate.Text
        ''�e�b �މ�N��
        'dtTyousaKaisyaDataSet.Item(0).fc_taikai_date = tbxFCTaikaiDate.Text
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        '�i�`�o�`�m��敪
        dtTyousaKaisyaDataSet.Item(0).japan_kai_kbn = ddlJapanKbn.SelectedValue
        '�i�`�o�`�m�� ����N��
        dtTyousaKaisyaDataSet.Item(0).japan_kai_nyuukai_date = tbxJapanKaiNyuukaiDate.Text
        '�i�`�o�`�m�� �މ�N��
        dtTyousaKaisyaDataSet.Item(0).japan_kai_taikai_date = tbxJapanKaiTaikaiDate.Text

        dtTyousaKaisyaDataSet.Item(0).zenjyuhin_hosoku = btxZenjyuhinHosoku.Text

        '��n�n�Ւ�����C���i
        dtTyousaKaisyaDataSet.Item(0).tkt_jbn_tys_syunin_skk_flg = ddlTktJbnTysSyuninSkkFlg.SelectedValue
        '�q�|�i�g�r�g�[�N��
        dtTyousaKaisyaDataSet.Item(0).report_jhs_token_flg = ddlRJhsTokenFlg.SelectedValue

        '�H���񍐏�����
        dtTyousaKaisyaDataSet.Item(0).koj_hkks_tyokusou_flg = ddlKojHkksTyokusouFlg.SelectedValue
        '�H���񍐏������ύX���O�C�����[�U
        dtTyousaKaisyaDataSet.Item(0).koj_hkks_tyokusou_upd_login_user_id = tbxKojHkksTyokusouUpdLoginUserId.Text
        '�H���񍐏������ύX����
        dtTyousaKaisyaDataSet.Item(0).koj_hkks_tyokusou_upd_datetime = tbxKojHkksTyokusouUpdDatetime.Text

        '���������Z���^�[
        'dtTyousaKaisyaDataSet.Item(0).kensa_center_cd = tbxKensakuKensaCenter.Text
        ''���������Z���^�[��
        'dtTyousaKaisyaDataSet.Item(0).fc_nm = tbxKensakuKensaCenterMei.Text
        '������敪
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_kbn = ddlSeikyuuSaki.SelectedValue
        '������R�[�h
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_cd = tbxSeikyuuSakiCd.Text.ToUpper
        '������}��
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_brc = tbxSeikyuuSakiBrc.Text.ToUpper
        '�����於
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_mei = tbxSeikyuuSakiMei.Text
        '������x���於
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_shri_saki_mei = tbxSeikyuuSakiShriSakiMei.Text
        '������x���於�J�i
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_shri_saki_kana = tbxSeikyuuSakiShriSakiKana.Text
        '���������t��X�֔ԍ�
        dtTyousaKaisyaDataSet.Item(0).skysy_soufu_yuubin_no = tbxSkysySoufuYuubinNo.Text
        '���������t��Z���P
        dtTyousaKaisyaDataSet.Item(0).skysy_soufu_jyuusyo1 = tbxSkysySoufuJyuusyo1.Text
        '���������t��d�b�ԍ�
        dtTyousaKaisyaDataSet.Item(0).skysy_soufu_tel_no = tbxSkysySoufuTelNo.Text
        '���������t��Z���Q
        dtTyousaKaisyaDataSet.Item(0).skysy_soufu_jyuusyo2 = tbxSkysySoufuJyuusyo2.Text
        '�x���pFAX�ԍ�
        dtTyousaKaisyaDataSet.Item(0).shri_you_fax_no = tbxShriYouFaxNo.Text
        '���V��v�x����
        dtTyousaKaisyaDataSet.Item(0).skk_jigyousyo_cd = tbxSkkJigyousyoCd.Text
        '�V��v�x����R�[�h
        dtTyousaKaisyaDataSet.Item(0).skk_shri_saki_cd = tbxSkkShriSakiCd.Text
        '�V��v�x���於
        dtTyousaKaisyaDataSet.Item(0).shri_saki_mei_kanji = tbxSkkShriSakiMei.Text
        '�x�����ߓ�
        dtTyousaKaisyaDataSet.Item(0).shri_sime_date = tbxShriSimeDate.Text
        '�x���\�茎��
        dtTyousaKaisyaDataSet.Item(0).shri_yotei_gessuu = tbxShriYoteiGessuu.Text
        '�t�@�N�^�����O�J�n�N��
        dtTyousaKaisyaDataSet.Item(0).fctring_kaisi_nengetu = tbxFctringKaisiNengetu.Text
        '���x���W�v�掖�Ə��R�[�h
        dtTyousaKaisyaDataSet.Item(0).shri_jigyousyo_cd = tbxShriJigyousyoCd.Text
        '�x�����׏W�v�掖�Ə��R�[�h
        dtTyousaKaisyaDataSet.Item(0).shri_meisai_jigyousyo_cd = tbxShriMeisaiJigyousyoCd.Text

        'SAP�p�d����
        dtTyousaKaisyaDataSet.Item(0).a1_lifnr = tbxSiireSaki.Text
        dtTyousaKaisyaDataSet.Item(0).a1_a_zz_sort = tbxSiireSakiMei.Text


        ''�x�����׏W�v�掖�Ə���
        'dtTyousaKaisyaDataSet.Item(0).tys_kaisya_mei = tbxTysMeisaiKaisyaMei.Text
        '============2012/04/12 �ԗ� 405721 �ǉ���==========================
        With dtTyousaKaisyaDataSet.Item(0)
            '��\�Җ�
            .daihyousya_mei = Me.tbxDaihyousyaMei.Text.Trim
            '��E��
            .yakusyoku_mei = Me.tbxYasyokuMei.Text.Trim
        End With
        '============2012/04/12 �ԗ� 405721 �ǉ���==========================

        '2013/11/04 ���F�ǉ� ��
        With dtTyousaKaisyaDataSet.Item(0)
            'SDS�ێ����
            .sds_hoji_info = Me.ddlSdsJyouhou.SelectedValue
            'SDS�䐔���
            .sds_daisuu_info = Me.tbxSdsKiki.Text.Trim
        End With
        '2013/11/04 ���F�ǉ� ��
        If ddlJituzaiFlg.SelectedIndex = 1 Then
            dtTyousaKaisyaDataSet.Rows(0).Item("jituzai_flg") = "1"
        Else
            dtTyousaKaisyaDataSet.Rows(0).Item("jituzai_flg") = DBNull.Value
        End If


        dtTyousaKaisyaDataSet.Item(0).upd_login_user_id = ViewState("UserId")
        dtTyousaKaisyaDataSet.Item(0).add_login_user_id = ViewState("UserId")
        dtTyousaKaisyaDataSet.Item(0).upd_datetime = hidUPDTime.Value

        Return dtTyousaKaisyaDataSet
    End Function

    ''' <summary>
    ''' ���׃N���A
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnClearMeisai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearMeisai.Click
        MeisaiClear()
    End Sub

    ''' <summary>
    ''' �N���A
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        '�������
        tbxTyousaKaisya_Cd.Text = ""
        '������Ж�
        tbxTyousaKaisya_Mei.Text = ""
        'MeisaiClear()
    End Sub

    ''' <summary>
    ''' �����於�E���t�Z���ɃR�s�[
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuSeikyuuSoufuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSoufuCopy.Click
        tbxSeikyuuSakiShriSakiMei.Text = tbxTyousaKaisyaMei.Text
        tbxSeikyuuSakiShriSakiKana.Text = tbxTyousaKaisyaMeiKana.Text
        tbxSkysySoufuYuubinNo.Text = tbxYuubinNo.Text
        tbxSkysySoufuJyuusyo1.Text = tbxJyuusyo1.Text
        tbxSkysySoufuTelNo.Text = tbxTelNo.Text
        tbxSkysySoufuJyuusyo2.Text = tbxJyuusyo2.Text
        tbxShriYouFaxNo.Text = tbxFaxNo.Text
    End Sub

    ''' <summary>
    ''' ���׍��ڃN���A
    ''' </summary>
    ''' <remarks></remarks>
    Sub MeisaiClear()
        '���
        chkTorikesi.Checked = False
        '������ЃR�[�h
        tbxTyousaKaisyaCd.Text = ""
        '�������R�[�h
        tbxJigyousyoCd.Text = ""
        '������R
        tbxTorikesiRiyuu.Text = ""
        tbxTorikesiRiyuu.Attributes.Add("readonly", "true;")
        '������Ж�
        tbxTyousaKaisyaMei.Text = ""
        '������Ж��J�i
        tbxTyousaKaisyaMeiKana.Text = ""
        '�X�֔ԍ�
        tbxYuubinNo.Text = ""
        '�Z���P
        tbxJyuusyo1.Text = ""
        '�d�b�ԍ�
        tbxTelNo.Text = ""
        '�Z���Q
        tbxJyuusyo2.Text = ""
        'FAX�ԍ�
        tbxFaxNo.Text = ""
        'SS����i
        tbxSSKijyunKkk.Text = ""
        'FC�X
        tbxFCTen.Text = ""
        'FC�X��
        tbxFCTenMei.Text = ""
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        ''�e�b ����N��
        'tbxFCNyuukaiDate.Text = ""
        ''�e�b �މ�N��
        'tbxFCTaikaiDate.Text = ""
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        '�i�`�o�`�m�� ����N��
        tbxJapanKaiNyuukaiDate.Text = ""
        '�i�`�o�`�m�� �މ�N��
        tbxJapanKaiTaikaiDate.Text = ""

        btxZenjyuhinHosoku.Text = ""
        '�H���񍐏������ύX���O�C�����[�U
        tbxKojHkksTyokusouUpdLoginUserId.Text = ""
        '�H���񍐏������ύX����
        tbxKojHkksTyokusouUpdDatetime.Text = ""
        '���������Z���^�[
        'tbxKensakuKensaCenter.Text = ""
        ''���������Z���^�[��
        'tbxKensakuKensaCenterMei.Text = ""
        '������
        tbxSeikyuuSakiCd.Text = ""
        '������}��
        tbxSeikyuuSakiBrc.Text = ""
        '�����於
        tbxSeikyuuSakiMei.Text = ""
        '������x���於
        tbxSeikyuuSakiShriSakiMei.Text = ""
        '������x���於�J�i
        tbxSeikyuuSakiShriSakiKana.Text = ""
        '���������t��X�֔ԍ�
        tbxSkysySoufuYuubinNo.Text = ""
        '���������t��Z���P
        tbxSkysySoufuJyuusyo1.Text = ""
        '���������t��d�b�ԍ�
        tbxSkysySoufuTelNo.Text = ""
        '���������t��Z���Q
        tbxSkysySoufuJyuusyo2.Text = ""
        '�x���pFAX�ԍ�
        tbxShriYouFaxNo.Text = ""
        '�V��v�x����
        tbxSkkJigyousyoCd.Text = "YMP8"
        '�V��v�x����
        tbxSkkShriSakiCd.Text = ""
        '�V��v�x����
        tbxSkkShriSakiMei.Text = ""
        '�x�����ߓ�
        tbxShriSimeDate.Text = ""
        '�x���\�茎��
        tbxShriYoteiGessuu.Text = ""
        '�t�@�N�^�����O�J�n�N��
        tbxFctringKaisiNengetu.Text = ""
        '�x���W�v�掖�Ə�
        tbxTysKaisyaCd.Text = ""
        '�x���W�v�掖�Ə�
        tbxShriJigyousyoCd.Text = ""
        '�x���W�v�掖�Ə�
        tbxTysKaisyaMei.Text = ""
        '�x�����׏W�v�掖�Ə�
        tbxTysMeisaiKaisyaCd.Text = ""
        '�x�����׏W�v�掖�Ə�
        tbxShriMeisaiJigyousyoCd.Text = ""
        '�x�����׏W�v�掖�Ə�
        tbxTysMeisaiKaisyaMei.Text = ""

        '�����Ɩ�
        SetDropSelect(ddlTyousaGyoumu, "")
        '�H���Ɩ�
        SetDropSelect(ddlKoujiGyoumu, "")
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        ''�e�b�X�敪
        'SetDropSelect(ddlFCTenKbn, "")
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        '�i�`�o�`�m��敪
        SetDropSelect(ddlJapanKbn, "")
        '��n�n�Ւ�����C���i
        SetDropSelect(ddlTktJbnTysSyuninSkkFlg, "")
        '�q�|�i�g�r�g�[�N��
        '=================2011/07/14 �ԗ� �C�� �J�n��=========================
        'SetDropSelect(ddlRJhsTokenFlg, "")
        SetDropSelect(ddlRJhsTokenFlg, "1")
        '=================2011/07/14 �ԗ� �C�� �J�n��=========================
        '�H���񍐏�����
        SetDropSelect(ddlKojHkksTyokusouFlg, "")
        '������
        SetDropSelect(ddlSeikyuuSaki, "")
        '============2012/04/12 �ԗ� 405721 �ǉ���==========================
        '��\�Җ�
        Me.tbxDaihyousyaMei.Text = String.Empty
        '��E��
        Me.tbxYasyokuMei.Text = String.Empty
        '============2012/04/12 �ԗ� 405721 �ǉ���==========================

        Me.tbxSiireSaki.Text = ""
        Me.tbxSiireSakiMei.Text = ""


        '2013/11/04 ���F�ǉ� ��
        'SDS�ێ����
        SetDropSelect(Me.ddlSdsJyouhou, "")
        'SDS�@��䐔
        Me.tbxSdsKiki.Text = String.Empty
        '2013/11/04 ���F�ǉ���

        ddlJituzaiFlg.SelectedIndex = 1

        'HIDDEN�ݒ�
        Me.hidSeikyuuSakiCd.Value = ""
        Me.hidSeikyuuSakiBrc.Value = ""
        Me.hidSeikyuuSakiKbn.Value = ""
        Me.hidKensakuKensaCenter.Value = ""
        Me.hidSkkJigyousyoCd.Value = ""
        Me.hidSkkShriSakiCd.Value = ""
        Me.hidShriJigyousyoCd.Value = ""
        Me.hidShriMeisaiJigyousyoCd.Value = ""
        Me.hidConfirm.Value = ""
        Me.hidTorikesi.Value = ""

        '������V�K�o�^
        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")

        tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
        tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
        tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
        tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
        tbxSkysySoufuTelNo.Attributes.Remove("readonly")
        tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
        tbxShriYouFaxNo.Attributes.Remove("readonly")
        btnKensakuSeikyuuSoufuCopy.Enabled = True
        btnKensakuSeikyuuSyo.Enabled = True

        hidUPDTime.Value = ""
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        Else
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
        End If
        '�V��v���Ə��R�[�h�E�V��v�x����R�[�h�̌����ݒ�
        If blnBtn1 And Not blnBtn2 Then
            tbxSkkJigyousyoCd.Enabled = False
            tbxSkkShriSakiCd.Enabled = False
            btnKensakuSkkShriSaki.Enabled = False
        Else
            tbxSkkJigyousyoCd.Enabled = True
            tbxSkkShriSakiCd.Enabled = True
            btnKensakuSkkShriSaki.Enabled = True
        End If


        tbxTyousaKaisyaCd.Attributes.Remove("readonly")
        tbxJigyousyoCd.Attributes.Remove("readonly")
        UpdatePanelA.Update()
    End Sub

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <param name="strErr">�G���[���b�Z�[�W</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '������ЃR�[�h
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxTyousaKaisyaCd.Text, "������ЃR�[�h")
            If strErr <> "" Then
                strID = tbxTyousaKaisyaCd.ClientID
            End If
        End If
        If strErr = "" And tbxTyousaKaisyaCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxTyousaKaisyaCd.Text, "������ЃR�[�h")
            If strErr <> "" Then
                strID = tbxTyousaKaisyaCd.ClientID
            End If
        End If
        '�������R�[�h
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxJigyousyoCd.Text, "�������R�[�h")
            If strErr <> "" Then
                strID = tbxJigyousyoCd.ClientID
            End If
        End If
        If strErr = "" And tbxJigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxJigyousyoCd.Text, "�������R�[�h")
            If strErr <> "" Then
                strID = tbxJigyousyoCd.ClientID
            End If
        End If
        '������R
        If strErr = "" And tbxTorikesiRiyuu.Text <> "" Then
            '�֑�����
            strErr = commoncheck.CheckKinsoku(tbxTorikesiRiyuu.Text, "������R")
            If strErr <> "" Then
                strID = tbxTorikesiRiyuu.ClientID
            End If
        End If
        '������Ж�
        If strErr = "" And tbxTyousaKaisyaMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxTyousaKaisyaMei.Text, 40, "������Ж�", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxTyousaKaisyaMei.ClientID
            End If
        End If
        If strErr = "" And tbxTyousaKaisyaMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxTyousaKaisyaMei.Text, "������Ж�")
            If strErr <> "" Then
                strID = tbxTyousaKaisyaMei.ClientID
            End If
        End If
        '������Ж��J�i
        If strErr = "" And tbxTyousaKaisyaMeiKana.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxTyousaKaisyaMeiKana.Text, 20, "������Ж��J�i", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxTyousaKaisyaMeiKana.ClientID
            End If
        End If
        If strErr = "" And tbxTyousaKaisyaMeiKana.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxTyousaKaisyaMeiKana.Text, "������Ж��J�i")
            If strErr <> "" Then
                strID = tbxTyousaKaisyaMeiKana.ClientID
            End If
        End If
        '============2012/04/12 �ԗ� 405721 �ǉ���==========================
        '��\�Җ�
        If strErr = "" And Me.tbxDaihyousyaMei.Text.Trim <> "" Then
            strErr = commoncheck.CheckByte(Me.tbxDaihyousyaMei.Text.Trim, 20, "��\�Җ�", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = Me.tbxDaihyousyaMei.ClientID
            End If
        End If
        If strErr = "" And Me.tbxDaihyousyaMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(Me.tbxDaihyousyaMei.Text.Trim, "��\�Җ�")
            If strErr <> "" Then
                strID = Me.tbxDaihyousyaMei.ClientID
            End If
        End If

        '��E��
        If strErr = "" And Me.tbxYasyokuMei.Text.Trim <> "" Then
            strErr = commoncheck.CheckByte(Me.tbxYasyokuMei.Text.Trim, 20, "��E��", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = Me.tbxYasyokuMei.ClientID
            End If
        End If
        If strErr = "" And Me.tbxYasyokuMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(Me.tbxYasyokuMei.Text.Trim, "��E��")
            If strErr <> "" Then
                strID = Me.tbxYasyokuMei.ClientID
            End If
        End If
        '============2012/04/12 �ԗ� 405721 �ǉ���==========================
        '�X�֔ԍ�
        If strErr = "" And tbxYuubinNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxYuubinNo.Text, "�X�֔ԍ�", "1")
            If strErr <> "" Then
                strID = tbxYuubinNo.ClientID
            End If
        End If

        '2013/11/05 ���F�ǉ� ��
        'SDS�@��䐔
        If strErr = "" And Me.tbxSdsKiki.Text <> "" Then
            strErr = commoncheck.CheckHankaku(Me.tbxSdsKiki.Text, "SDS�@��䐔", "1")
            If strErr <> "" Then
                strErr = String.Format(Messages.Instance.MSG2006E, "SDS�@��䐔")
                strID = Me.tbxSdsKiki.ClientID
            End If
        End If
        '2013/11/05 ���F�ǉ� ��

        '�Z���P
        If strErr = "" And tbxJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxJyuusyo1.Text, 40, "�Z���P", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxJyuusyo1.ClientID
            End If
        End If
        If strErr = "" And tbxJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxJyuusyo1.Text, "�Z���P")
            If strErr <> "" Then
                strID = tbxJyuusyo1.ClientID
            End If
        End If
        '�d�b�ԍ�
        If strErr = "" And tbxTelNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxTelNo.Text, "�d�b�ԍ�", "1")
            If strErr <> "" Then
                strID = tbxTelNo.ClientID
            End If
        End If
        '�Z���Q
        If strErr = "" And tbxJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxJyuusyo2.Text, 30, "�Z���Q", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxJyuusyo2.ClientID
            End If
        End If
        If strErr = "" And tbxJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxJyuusyo2.Text, "�Z���Q")
            If strErr <> "" Then
                strID = tbxJyuusyo2.ClientID
            End If
        End If
        'FAX�ԍ�
        If strErr = "" And tbxFaxNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxFaxNo.Text, "FAX�ԍ�", "1")
            If strErr <> "" Then
                strID = tbxFaxNo.ClientID
            End If
        End If
        'SS����i
        If strErr = "" And tbxSSKijyunKkk.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxSSKijyunKkk.Text, "SS����i", "1")
            If strErr <> "" Then
                strID = tbxSSKijyunKkk.ClientID
            End If
        End If
        'FC�X�R�[�h
        If strErr = "" And tbxFCTen.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxFCTen.Text, "FC�X�R�[�h")
            If strErr <> "" Then
                strID = tbxFCTen.ClientID
            End If
        End If
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        ''�e�b ����N��
        'If strErr = "" And tbxFCNyuukaiDate.Text <> "" Then
        '    strErr = commoncheck.CheckYuukouHiduke(tbxFCNyuukaiDate.Text, "�e�b ����N��")
        '    If strErr <> "" Then
        '        strID = tbxFCNyuukaiDate.ClientID
        '    End If
        'End If
        ''�e�b �މ�N��
        'If strErr = "" And tbxFCTaikaiDate.Text <> "" Then
        '    strErr = commoncheck.CheckYuukouHiduke(tbxFCTaikaiDate.Text, "�e�b �މ�N��")
        '    If strErr <> "" Then
        '        strID = tbxFCTaikaiDate.ClientID
        '    End If
        'End If
        '==============2012/04/12 �ԗ� 405738 �폜��====================
        '�i�`�o�`�m�� ����N��
        If strErr = "" And tbxJapanKaiNyuukaiDate.Text <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxJapanKaiNyuukaiDate.Text, "�i�`�o�`�m�� ����N��")
            If strErr <> "" Then
                strID = tbxJapanKaiNyuukaiDate.ClientID
            End If
        End If
        '�i�`�o�`�m�� �މ�N��
        If strErr = "" And tbxJapanKaiTaikaiDate.Text <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxJapanKaiTaikaiDate.Text, "�i�`�o�`�m�� �މ�N��")
            If strErr <> "" Then
                strID = tbxJapanKaiTaikaiDate.ClientID
            End If
        End If

        If strErr = "" And btxZenjyuhinHosoku.Text <> "" Then
            strErr = commoncheck.CheckByte(btxZenjyuhinHosoku.Text, 80, "�S�Z�i�敪�⑫", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = btxZenjyuhinHosoku.ClientID
            End If
        End If

        '���������Z���^�[�R�[�h
        'If strErr = "" And tbxKensakuKensaCenter.Text <> "" Then
        '    strErr = commoncheck.ChkHankakuEisuuji(tbxKensakuKensaCenter.Text, "���������Z���^�[�R�[�h")
        '    If strErr <> "" Then
        '        strID = tbxKensakuKensaCenter.ClientID
        '    End If
        'End If
        '������R�[�h
        If strErr = "" And tbxSeikyuuSakiCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSakiCd.Text, "������R�[�h")
            If strErr <> "" Then
                strID = tbxSeikyuuSakiCd.ClientID
            End If
        End If
        '������}��
        If strErr = "" And tbxSeikyuuSakiBrc.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSakiBrc.Text, "������}��")
            If strErr <> "" Then
                strID = tbxSeikyuuSakiBrc.ClientID
            End If
        End If
        '������x���於
        If strErr = "" And tbxSeikyuuSakiShriSakiMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSeikyuuSakiShriSakiMei.Text, 80, "������x���於", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSeikyuuSakiShriSakiMei.ClientID
            End If
        End If
        If strErr = "" And tbxSeikyuuSakiShriSakiMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSeikyuuSakiShriSakiMei.Text, "������x���於")
            If strErr <> "" Then
                strID = tbxSeikyuuSakiShriSakiMei.ClientID
            End If
        End If
        '������x���於�J�i
        If strErr = "" And tbxSeikyuuSakiShriSakiKana.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSeikyuuSakiShriSakiKana.Text, 40, "������x���於�J�i", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSeikyuuSakiShriSakiKana.ClientID
            End If
        End If
        If strErr = "" And tbxSeikyuuSakiShriSakiKana.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSeikyuuSakiShriSakiKana.Text, "������x���於�J�i")
            If strErr <> "" Then
                strID = tbxSeikyuuSakiShriSakiKana.ClientID
            End If
        End If
        '���������t��X�֔ԍ�
        If strErr = "" And tbxSkysySoufuYuubinNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxSkysySoufuYuubinNo.Text, "���������t��X�֔ԍ�", "1")
            If strErr <> "" Then
                strID = tbxSkysySoufuYuubinNo.ClientID
            End If
        End If
        '���������t��Z���P
        If strErr = "" And tbxSkysySoufuJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSkysySoufuJyuusyo1.Text, 40, "���������t��Z���P", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSkysySoufuJyuusyo1.ClientID
            End If
        End If
        If strErr = "" And tbxSkysySoufuJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSkysySoufuJyuusyo1.Text, "���������t��Z���P")
            If strErr <> "" Then
                strID = tbxSkysySoufuJyuusyo1.ClientID
            End If
        End If
        '���������t��d�b�ԍ�
        If strErr = "" And tbxSkysySoufuTelNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxSkysySoufuTelNo.Text, "���������t��d�b�ԍ�", "1")
            If strErr <> "" Then
                strID = tbxSkysySoufuTelNo.ClientID
            End If
        End If
        '���������t��Z���Q
        If strErr = "" And tbxSkysySoufuJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSkysySoufuJyuusyo2.Text, 40, "���������t��Z���Q", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSkysySoufuJyuusyo2.ClientID
            End If
        End If
        If strErr = "" And tbxSkysySoufuJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSkysySoufuJyuusyo2.Text, "���������t��Z���Q")
            If strErr <> "" Then
                strID = tbxSkysySoufuJyuusyo2.ClientID
            End If
        End If
        '�x���pFAX�ԍ�
        If strErr = "" And tbxShriYouFaxNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxShriYouFaxNo.Text, "�x���pFAX�ԍ�", "1")
            If strErr <> "" Then
                strID = tbxShriYouFaxNo.ClientID
            End If
        End If
        '�V��v���Ə��R�[�h
        If strErr = "" And tbxSkkJigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSkkJigyousyoCd.Text, "�V��v���Ə��R�[�h")
            If strErr <> "" Then
                strID = tbxSkkJigyousyoCd.ClientID
            End If
        End If
        '�V��v�x����R�[�h
        If strErr = "" And tbxSkkShriSakiCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSkkShriSakiCd.Text, "�V��v�x����R�[�h")
            If strErr <> "" Then
                strID = tbxSkkShriSakiCd.ClientID
            End If
        End If
        '�x�����ߓ�
        If strErr = "" And tbxShriSimeDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxShriSimeDate.Text, "�x�����ߓ�", "31")
            If strErr <> "" Then
                strID = tbxShriSimeDate.ClientID
            End If
        End If
        '�x���\�茎��
        If strErr = "" And tbxShriYoteiGessuu.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxShriYoteiGessuu.Text, "�x���\�茎��", "12")
            If strErr <> "" Then
                strID = tbxShriYoteiGessuu.ClientID
            End If
        End If
        '�t�@�N�^�����O�J�n�N��
        If strErr = "" And tbxFctringKaisiNengetu.Text <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxFctringKaisiNengetu.Text, "�t�@�N�^�����O�J�n�N��")
            If strErr <> "" Then
                strID = tbxFctringKaisiNengetu.ClientID
            End If
        End If
        '�x���W�v�掖�Ə�
        If strErr = "" And tbxShriJigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxShriJigyousyoCd.Text, "�x���W�v�掖�Ə��R�[�h")
            If strErr <> "" Then
                strID = tbxShriJigyousyoCd.ClientID
            End If
        End If
        '�x�����׏W�v�掖�Ə�
        If strErr = "" And tbxShriMeisaiJigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxShriMeisaiJigyousyoCd.Text, "�x�����׏W�v�掖�Ə��R�[�h")
            If strErr <> "" Then
                strID = tbxShriMeisaiJigyousyoCd.ClientID
            End If
        End If
        Return strID

    End Function

    ''' <summary>
    ''' DDL�̏����ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetDdlListInf()
        '���h���b�v�_�E�����X�g�ݒ聫
        Dim ddlist As ListItem

        '�����Ɩ�ddlTyousaGyoumu
        ddlist = New ListItem
        ddlist.Text = "�s��Ȃ�"
        ddlist.Value = "0"
        ddlTyousaGyoumu.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�s��"
        ddlist.Value = "1"
        ddlTyousaGyoumu.Items.Add(ddlist)

        '�H���Ɩ�ddlKoujiGyoumu
        ddlist = New ListItem
        ddlist.Text = "�s��Ȃ�"
        ddlist.Value = "0"
        ddlKoujiGyoumu.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�s��"
        ddlist.Value = "1"
        ddlKoujiGyoumu.Items.Add(ddlist)

        '==============2012/04/12 �ԗ� 405738 �폜��====================
        ''FC�X�敪ddlFCTenKbn
        'ddlist = New ListItem
        'ddlist.Text = "������"
        'ddlist.Value = "0"
        'ddlFCTenKbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "����"
        'ddlist.Value = "1"
        'ddlFCTenKbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "�މ�"
        'ddlist.Value = "3"
        'ddlFCTenKbn.Items.Add(ddlist)
        '==============2012/04/12 �ԗ� 405738 �폜��====================

        'JAPAN��敪ddlJapanKbn
        ddlist = New ListItem
        ddlist.Text = "������"
        ddlist.Value = "0"
        ddlJapanKbn.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "�����iJHS����L�j"
        ddlist.Value = "1"
        ddlJapanKbn.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "�����i�S�Z�i�̂݁j"
        ddlist.Value = "2"
        ddlJapanKbn.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "�މ�"
        ddlist.Value = "3"
        ddlJapanKbn.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "�ΏۊO"
        ddlist.Value = "4"
        ddlJapanKbn.Items.Add(ddlist)

        '��n�n�Ւ�����C���iddlTktJbnTysSyuninSkkFlg
        ddlist = New ListItem
        ddlist.Text = "����"
        ddlist.Value = "0"
        ddlTktJbnTysSyuninSkkFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�L��"
        ddlist.Value = "1"
        ddlTktJbnTysSyuninSkkFlg.Items.Add(ddlist)

        '�q�|�i�g�r�g�[�N��ddlRJhsTokenFlg
        ddlist = New ListItem
        ddlist.Text = "����"
        ddlist.Value = "0"
        ddlRJhsTokenFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�L��"
        ddlist.Value = "1"
        ddlRJhsTokenFlg.Items.Add(ddlist)
        '=================2011/07/14 �ԗ� �ǉ� �J�n��=========================
        ddlRJhsTokenFlg.SelectedIndex = 1
        '=================2011/07/14 �ԗ� �ǉ� �J�n��=========================

        '�H���񍐏�����ddlKojHkksTyokusouFlg
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlKojHkksTyokusouFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "��"
        ddlist.Value = "1"
        ddlKojHkksTyokusouFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�s��"
        ddlist.Value = "0"
        ddlKojHkksTyokusouFlg.Items.Add(ddlist)

        '������敪ddlSeikyuuSaki
        SetKakutyou(ddlSeikyuuSaki, "1")

    End Sub

    ''' <summary>
    ''' ������敪�h���b�v�_�E�����X�g�ݒ�
    ''' </summary>
    ''' <param name="ddl">�h���b�v�_�E�����X�g</param>
    ''' <param name="strSyubetu">���̎��</param>
    ''' <remarks></remarks>
    Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = TyousaKaisyaMasterBL.SelKakutyouInfo(strSyubetu)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & "�F" & TrimNull(dtTable.Rows(intCount).Item("meisyou"))
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
            ddl.Items.Add(ddlist)
        Next
    End Sub

    ''' <summary>
    ''' ���������䋤�ʏ��� ������.�����{�^��������
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetKasseika()
        If ddlSeikyuuSaki.SelectedValue = "1" And tbxTyousaKaisyaCd.Text.ToUpper = tbxSeikyuuSakiCd.Text.ToUpper And tbxJigyousyoCd.Text.ToUpper = tbxSeikyuuSakiBrc.Text.ToUpper Then
            tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
            tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
            tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
            tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
            tbxSkysySoufuTelNo.Attributes.Remove("readonly")
            tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
            tbxShriYouFaxNo.Attributes.Remove("readonly")
            btnKensakuSeikyuuSoufuCopy.Enabled = True
            btnKensakuSeikyuuSyo.Enabled = True
        Else
            tbxSeikyuuSakiShriSakiMei.Text = ""
            tbxSeikyuuSakiShriSakiKana.Text = ""
            tbxSkysySoufuYuubinNo.Text = ""
            tbxSkysySoufuJyuusyo1.Text = ""
            tbxSkysySoufuTelNo.Text = ""
            tbxSkysySoufuJyuusyo2.Text = ""
            tbxShriYouFaxNo.Text = ""

            tbxSeikyuuSakiShriSakiMei.Attributes.Add("readonly", "true;")
            tbxSeikyuuSakiShriSakiKana.Attributes.Add("readonly", "true;")
            tbxSkysySoufuYuubinNo.Attributes.Add("readonly", "true;")
            tbxSkysySoufuJyuusyo1.Attributes.Add("readonly", "true;")
            tbxSkysySoufuTelNo.Attributes.Add("readonly", "true;")
            tbxSkysySoufuJyuusyo2.Attributes.Add("readonly", "true;")
            tbxShriYouFaxNo.Attributes.Add("readonly", "true;")
            btnKensakuSeikyuuSoufuCopy.Enabled = False
            btnKensakuSeikyuuSyo.Enabled = False
        End If
    End Sub

    ''' <summary>�󔒂��폜</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    ''' <summary>DDL�ݒ�</summary>
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)

        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub

    ''' <summary>
    ''' ���ڃf�[�^�ɃR�}��ǉ�
    ''' </summary>
    ''' <param name="kekka">���z</param>
    Protected Function AddComa(ByVal kekka As String) As String
        If TrimNull(kekka) = "" Then
            Return ""
        Else
            Return CInt(kekka).ToString("###,###,##0")
        End If

    End Function

    ''' <summary>
    ''' ���t�^�ύX����
    ''' </summary>
    ''' <param name="ymd">�N��</param>
    ''' <returns></returns>
    ''' <remarks>2010/01/12 �n���R�i��A�j �V�K�쐬</remarks>
    Public Function toYYYYMM(ByVal ymd As Object) As Object

        If ymd.Equals(String.Empty) Or ymd.Equals(DBNull.Value) Then
            Return ymd
        Else
            Return CDate(ymd).ToString("yyyy/MM")
        End If

    End Function

    ''' <summary>
    ''' ���t�^�ύX����
    ''' </summary>
    ''' <param name="ymd">�N��</param>
    ''' <returns></returns>
    ''' <remarks>2010/01/12 �n���R�i��A�j �V�K�쐬</remarks>
    Public Function toYYYYMMDDHH(ByVal ymd As Object) As Object

        If ymd.Equals(String.Empty) Or ymd.Equals(DBNull.Value) Then
            Return ymd
        Else
            Return CDate(ymd).ToString("yyyy/MM/dd hh:mm:ss")
        End If

    End Function

    ''' <summary>
    ''' �Z���P�A�Q�擾
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetJyusho(ByVal value As String) As String
        Dim i As Integer
        If value.Length > 20 Then
            For i = 20 To value.Length
                If System.Text.Encoding.Default.GetBytes(Left(value, i)).Length >= 39 Then
                    Return value.Substring(0, i) & "," & value.Substring(i, value.Length - i)
                End If
            Next
        End If
        Return value & ","
    End Function

    ''' <summary>
    ''' �ڍ׃{�^��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuSeikyuuSyousai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSyousai.Click
        Dim strScript As String = String.Empty
        Dim tmpScript As String = String.Empty
        '������敪<>""(��) and ������R�[�h<>""(��) and ������}�� <> ""(��) �̏ꍇ
        If Me.ddlSeikyuuSaki.SelectedValue <> "" And Me.tbxSeikyuuSakiCd.Text <> "" And Me.tbxSeikyuuSakiBrc.Text <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = TyousaKaisyaMasterBL.SelSeikyuuSaki(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSaki.SelectedValue, True)
            If dtTable.Rows.Count > 0 Then
                tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki.ClientID & "','" & _
                                            SEARCH_SEIKYUU_SAKI & "','" _
                                       & Me.btnKensakuSeikyuuSyousai.ClientID & "');"

                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            Else
                '���b�Z�[�W�\��
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�Y������f�[�^�����݂��܂���B');", True)
            End If
        Else
            '���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�������񂪐ݒ肳��Ă��܂���B\r\n���������͂��ĉ������B');", True)
        End If
    End Sub

    ''' <summary>
    ''' �����挟���{�^���������AOK��I����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.hidConfirm.Value = ""
        '�Y������f�[�^�����݂���ꍇ
        Dim dtSeikyuuSakiTouroku As New DataTable
        dtSeikyuuSakiTouroku = TyousaKaisyaMasterBL.SelSeikyuuSakiTouroku(tbxSeikyuuSakiBrc.Text)
        If dtSeikyuuSakiTouroku.Rows.Count > 0 Then
            labSeikyuuSaki.Style.Add("display", "block")
            labHyouji.Style.Add("display", "block")
            labHyouji.Text = dtSeikyuuSakiTouroku.Rows(0).Item("hyouji_naiyou").ToString
        Else
            labSeikyuuSaki.Style.Add("display", "block")
        End If

        Me.hidConfirm1.Value = "OK"
        Me.hidConfirm2.Value = ""

        Me.tbxSeikyuuSakiMei.Text = ""

        hidSeikyuuSakiCd.Value = tbxSeikyuuSakiCd.Text
        hidSeikyuuSakiBrc.Value = tbxSeikyuuSakiBrc.Text
        hidSeikyuuSakiKbn.Value = ddlSeikyuuSaki.SelectedValue
    End Sub

    ''' <summary>
    ''' �����挟���{�^���������ANO��I����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click
        Dim strScript As String = ""

        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")
        Me.hidConfirm1.Value = "ON"
        Me.hidConfirm2.Value = "����"

        '�����於
        tbxSeikyuuSakiMei.Text = ""
        Me.hidConfirm.Value = ""
        strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&Kbn='+escape('������')+'&FormName=" & _
                                     Me.Page.Form.Name & "&objKbn=" & _
                                     ddlSeikyuuSaki.ClientID & _
                                     "&objHidKbn=" & hidSeikyuuSakiKbn.ClientID & _
                                     "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
                                     "&objHidCd=" & hidSeikyuuSakiCd.ClientID & _
                                     "&hidConfirm2=" & hidConfirm2.ClientID & _
                                     "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
                                     "&objHidBrc=" & hidSeikyuuSakiBrc.ClientID & _
                                     "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
                                     "&strKbn='+escape(eval('document.all.'+'" & _
                                     ddlSeikyuuSaki.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                     tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                     tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
    End Sub

    ''' <summary>
    ''' �߂�{�^���̏���
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub


End Class