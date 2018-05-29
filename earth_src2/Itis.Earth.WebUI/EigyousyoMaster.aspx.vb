Imports Itis.Earth.BizLogic
Imports System.Data

Partial Public Class EigyousyoMaster
    Inherits System.Web.UI.Page

    '�{�^��
    Private blnBtn As Boolean
    '�C���X�^���X����
    Private CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
    '���ʃ`�F�b�N
    Private commoncheck As New CommonCheck
    '�C���X�^���X����
    Private EigyousyoMasterBL As New Itis.Earth.BizLogic.EigyousyoMasterLogic

    '�ڍ׃{�^��
    Private Const SEP_STRING As String = "$$$"
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
        blnBtn = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen")
        ViewState("UserId") = strUserID
        If Not IsPostBack Then
            'DDL�̏����ݒ�
            SetDdlListInf()

            '�C���{�^��
            btnSyuusei.Enabled = False
            '�o�^�{�^��
            btnTouroku.Enabled = True
            'FC�X�Z�������X�V
            btnFcTenKousin.Enabled = False
            '������V�K�o�^
            labSeikyuuSaki.Style.Add("display", "none")
            labHyouji.Style.Add("display", "none")

            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            '�Œ�`���[�W
            Me.btnKoteiTyaaji.Enabled = False
            Me.lblKoteiTyaaji.Text = String.Empty
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
        Else
            '�����挟���{�^��������
            If Me.hidConfirm.Value = "Hyouji" Then
                labSeikyuuSaki.Style.Add("display", "none")
                labHyouji.Style.Add("display", "none")

                Me.hidConfirm1.Value = "NO"
            End If
        End If

        '�c�Ə���
        Me.tbxEigyousyo_Mei.Attributes.Add("readonly", "true;")
        '������
        Me.tbxSeikyuuSakiMei.Attributes.Add("readonly", "true;")
        '�X�֔ԍ�
        Me.tbxYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        ''���������t��X�֔ԍ�
        'Me.tbxSkysySoufuYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        'DISABLE
        btnSearchEigyousyo.Attributes.Add("onclick", "disableButton1();")
        btnSearch.Attributes.Add("onclick", "disableButton1();")
        btnClear.Attributes.Add("onclick", "disableButton1();")
        btnFcTenKousin.Attributes.Add("onclick", "disableButton1();")
        btnKensakuYuubinNo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSaki.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSyousai.Attributes.Add("onclick", "disableButton1();")
        '20100712�@�d�l�ύX�@�폜�@�n���R������
        'btnKensakuSeikyuuSyo.Attributes.Add("onclick", "disableButton1();")
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        btnFC.Attributes.Add("onclick", "disableButton1();")
        btnOK.Attributes.Add("onclick", "disableButton1();")
        btnNO.Attributes.Add("onclick", "disableButton1();")

        '���׃N���A
        btnClearMeisai.Attributes.Add("onclick", "if(!confirm('�N���A���s�Ȃ��܂��B\n��낵���ł����H')){return false;};disableButton1();")

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        ''�����於�E���t�Z���ɃR�s�[
        'btnKensakuSeikyuuSoufuCopy.Attributes.Add("onclick", "if(!confirm('���t�Z���ɏ㏑���R�s�[���܂��B\n��낵���ł����H')){return false;};disableButton1();")
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        'JavaScript
        MakeScript()

        '���p�J�i�ϊ�
        '20100712�@�d�l�ύX�@�폜�@�n���R������
        'tbxSeikyuuSakiShriSakiKana.Attributes.Add("onblur", "fncTokomozi(this)")
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        tbxEigyousyoMeiKana.Attributes.Add("onblur", "fncTokomozi(this)")

        '�o�^�{�^��
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncCheck03()){return false;};disableButton1();")

        '�C���{�^��
        Me.btnSyuusei.Attributes.Add("onClick", "if(!fncCheck03()){return false;};disableButton1();")

        ''������ЃR�[�h
        'Me.tbxEigyousyoCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxEigyousyoCd.ClientID & "')")
        'Me.tbxEigyousyoCd.Attributes.Add("onblur", "fncblur('" & Me.tbxEigyousyoCd.ClientID & "')")
        ''������敪
        'Me.ddlSeikyuuSaki.Attributes.Add("onfocus", "fncFocus('" & Me.ddlSeikyuuSaki.ClientID & "')")
        'Me.ddlSeikyuuSaki.Attributes.Add("onblur", "fncblur('" & Me.ddlSeikyuuSaki.ClientID & "')")
        ''������R�[�h
        'Me.tbxSeikyuuSakiCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
        'Me.tbxSeikyuuSakiCd.Attributes.Add("onblur", "fncblur('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
        ''������}��
        'Me.tbxSeikyuuSakiBrc.Attributes.Add("onfocus", "fncFocus('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")
        'Me.tbxSeikyuuSakiBrc.Attributes.Add("onblur", "fncblur('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

        '����
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
            btnFcTenKousin.Enabled = False
        End If

        '=========2012/04/10 �ԗ� 405738 �ǉ���================================
        'CSV�o��
        Me.btnCsv.Attributes.Add("onClick", "document.getElementById('" & Me.btnCsvOutput.ClientID & "').click();return false;")
        '������Ќ���
        Me.btnTyousaKaisyaCd.Attributes.Add("onclick", "disableButton1();")
        Me.btnTyousakaisya.Attributes.Add("onclick", "disableButton1();")

        '������� �Z�����R�s�[
        Me.btnTyousakaisyaCopy.Attributes.Add("onClick", "fncCopyTyousaKaisya();return false;")

        '������Ж�
        Me.tbxTyousaKaisyaMei.Attributes.Add("readonly", "true;")
        '������Ж��J�i
        Me.tbxTyousaKaisyaMeiKana.Attributes.Add("readonly", "true;")
        '(�������)��\�Җ�
        Me.tbxDaihyousyaMei.Attributes.Add("readonly", "true;")
        '(�������)��E��
        Me.tbxYasyokuMei.Attributes.Add("readonly", "true;")
        '(�������)�X�֔ԍ�
        Me.tbxTyousakaisyaYuubinNo.Attributes.Add("readonly", "true;")
        '(�������)�Z��1
        Me.tbxTyousakaisyaJyuusyo1.Attributes.Add("readonly", "true;")
        '(�������)�d�b�ԍ�
        Me.tbxTyousakaisyaTelNo.Attributes.Add("readonly", "true;")
        '(�������)�Z��2
        Me.tbxTyousakaisyaJyuusyo2.Attributes.Add("readonly", "true;")
        '(�������)FAX�ԍ�
        Me.tbxTyousakaisyaFaxNo.Attributes.Add("readonly", "true;")
        '(�������)JAPAN��敪
        Me.tbxJapanKaiKbn.Attributes.Add("readonly", "true;")
        '(�������)JAPAN�����N��
        Me.tbxJapanKaiNyuukaiYM.Attributes.Add("readonly", "true;")
        '(�������)JAPAN��މ�N��
        Me.tbxJapanKaiTaikaiYM.Attributes.Add("readonly", "true;")
        '(�������)��n�n�Ւ�����C���i�L���t���O
        Me.tbxTyousaSyuninSikaku.Attributes.Add("readonly", "true;")
        '(�������)ReportJHS�g�[�N���L���t���O
        Me.tbxReportJHS.Attributes.Add("readonly", "true;")

        '�e�b ����N��
        Me.tbxFcNyuukaiYM.Attributes.Add("onBlur", "fncCheckNengetu(this,'�e�b ����N��');")
        '�e�b �މ�N��
        Me.tbxFcTaikaiYM.Attributes.Add("onBlur", "fncCheckNengetu(this,'�e�b �މ�N��');")
        '=========2012/04/10 �ԗ� 405738 �ǉ���================================

    End Sub

    ''' <summary>
    ''' �I��ҏW�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strErr As String = ""

        If strErr = "" Then
            '���͕K�{
            strErr = commoncheck.CheckHissuNyuuryoku(Me.tbxEigyousyo_Cd.Text, "�c�Ə��R�[�h")
        End If
        If strErr = "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxEigyousyo_Cd.Text, "�c�Ə��R�[�h")
        End If
        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & tbxEigyousyo_Cd.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        Else
            GetMeisaiData(tbxEigyousyo_Cd.Text, tbxEigyousyoCd.Text, "btnSearch")
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
        Dim strDisplayName As String = ""
        strID = InputCheck(strErr)

        '�X�֔ԍ����݃`�F�b�N
        If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = EigyousyoMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
                strID = tbxYuubinNo.ClientID
            End If
        End If

        '������Б��݃`�F�b�N
        If strErr = "" And Trim(Me.tbxTyousaKaisyaCd.Text.Trim()) <> "" Then
            If EigyousyoMasterBL.GetTyousaKaisyaCount(Me.tbxTyousaKaisyaCd.Text.Trim()) <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "������Ѓ}�X�^").ToString
                strID = tbxTyousaKaisyaCd.ClientID
            End If
        End If

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        ''���������t��X�֔ԍ����݃`�F�b�N
        'If strErr = "" And Trim(tbxSkysySoufuYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = EigyousyoMasterBL.SelYuubinInfo(Trim(tbxSkysySoufuYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        '�G���[�����鎞
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            Dim dtEigyousyoTable As New Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable
            dtEigyousyoTable = EigyousyoMasterBL.SelEigyousyoInfo("", Me.tbxEigyousyoCd.Text, "btnTouroku")
            '�d���`�F�b�N
            If dtEigyousyoTable.Rows.Count <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�}�X�^�[�ɏd���f�[�^�����݂��܂��B');document.getElementById('" & tbxEigyousyoCd.ClientID & "').focus();", True)
                Return
            End If

            Dim strTrue As String = ""
            '�y������V�K�o�^�z���\������Ă���ꍇ
            If Me.hidConfirm1.Value = "OK" Then
                strTrue = "OK"
            End If

            '�f�[�^�o�^
            If EigyousyoMasterBL.InsEigyousyo(SetMeisaiData, strTrue) Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "�c�Ə��}�X�^") & "');"
                '�Ď擾
                GetMeisaiData("", tbxEigyousyoCd.Text, "btnTouroku")
            Else
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "�c�Ə��}�X�^") & "');"
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
        Dim strDisplayName As String = ""
        '�`�F�b�N
        strID = InputCheck(strErr)

        '�X�֔ԍ����݃`�F�b�N
        If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = EigyousyoMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
                strID = tbxYuubinNo.ClientID
            End If
        End If

        '������Б��݃`�F�b�N
        If strErr = "" And Trim(Me.tbxTyousaKaisyaCd.Text.Trim()) <> "" Then
            If EigyousyoMasterBL.GetTyousaKaisyaCount(Me.tbxTyousaKaisyaCd.Text.Trim()) <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "������Ѓ}�X�^").ToString
                strID = tbxTyousaKaisyaCd.ClientID
            End If
        End If

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        ''���������t��X�֔ԍ����݃`�F�b�N
        'If strErr = "" And Trim(tbxSkysySoufuYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = EigyousyoMasterBL.SelYuubinInfo(Trim(tbxSkysySoufuYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        '�G���[�����鎞
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            Dim strTrue As String = ""
            '�y������V�K�o�^�z���\������Ă���ꍇ
            If Me.hidConfirm1.Value = "OK" Then
                strTrue = "OK"
            End If

            '�X�V����
            strReturn = EigyousyoMasterBL.UpdEigyousyo(SetMeisaiData, strTrue)
            If strReturn = "0" Then
                '�X�V����
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "�c�Ə��}�X�^") & "');"
                '��ʍĕ`�揈��
                GetMeisaiData("", tbxEigyousyoCd.Text, "btnSyuusei")
            ElseIf strReturn = "1" Then
                '�X�V���s
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "�c�Ə��}�X�^") & "');"
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

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        ''���������䋤�ʏ��� ������.�����{�^��������
        'SetKasseika()
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        Dim strScript As String = ""

        '��������擾
        Dim dtSeikyuuSakiTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTable1DataTable
        dtSeikyuuSakiTable = EigyousyoMasterBL.SelVSeikyuuSakiInfo(ddlSeikyuuSaki.SelectedValue, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, False)

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
            'ElseIf dtSeikyuuSakiTable.Rows.Count = 0 Then
            '    '�������ʂ�0���������ꍇ
            '    '���.������敪��"2�F�c�Ə�" ���� ���.������ЃR�[�h�����.������R�[�h ���� ���.���Ə��R�[�h�����.������}�Ԃ̏ꍇ
            '    If (ddlSeikyuuSaki.SelectedValue = "2") And (tbxSeikyuuSakiCd.Text <> "") And (tbxSeikyuuSakiBrc.Text <> "") And (tbxEigyousyoCd.Text.ToUpper = tbxSeikyuuSakiCd.Text.ToUpper) Then
            '        '���b�Z�[�W�\��
            '        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "fncConfirm();", True)
            '        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "if(confirm('���̓��e�̓o�^���ɐ�����}�X�^�ɓo�^���܂����H')){ window.setTimeout('objEBI(\'" & Me.btnOK.ClientID & "\').click()',10);}else{ window.setTimeout('objEBI(\'" & Me.btnNO.ClientID & "\').click()',10);}; ", True)
            '    Else
            '        '�����於
            '        tbxSeikyuuSakiMei.Text = ""
            '        strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&&Kbn='+escape('������')+'&FormName=" & _
            '                                     Me.Page.Form.Name & "&objKbn=" & _
            '                                     ddlSeikyuuSaki.ClientID & _
            '                                     "&objHidKbn=" & hidSeikyuuSakiKbn.ClientID & _
            '                                     "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
            '                                     "&objHidCd=" & hidSeikyuuSakiCd.ClientID & _
            '                                     "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
            '                                     "&hidConfirm2=" & hidConfirm2.ClientID & _
            '                                     "&objHidBrc=" & hidSeikyuuSakiBrc.ClientID & _
            '                                     "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
            '                                     "&strKbn='+escape(eval('document.all.'+'" & _
            '                                     ddlSeikyuuSaki.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
            '                                     tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
            '                                     tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
            '    End If
        Else
            '�����於
            tbxSeikyuuSakiMei.Text = ""
            strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?strFlg=1&blnDelete=True&Kbn='+escape('������')+'&FormName=" & _
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

    '20100712�@�d�l�ύX�@�폜�@�n���R������
    '''' <summary>
    '''' ���������䋤�ʏ��� ������.�����{�^��������
    '''' </summary>
    '''' <remarks></remarks>
    'Sub SetKasseika()
    '    If ddlSeikyuuSaki.SelectedValue = "2" And tbxEigyousyoCd.Text.ToUpper = tbxSeikyuuSakiCd.Text.ToUpper Then
    '        tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
    '        tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
    '        tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
    '        tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
    '        tbxSkysySoufuTelNo.Attributes.Remove("readonly")
    '        tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
    '        tbxShriYouFaxNo.Attributes.Remove("readonly")
    '        btnKensakuSeikyuuSoufuCopy.Enabled = True
    '        btnKensakuSeikyuuSyo.Enabled = True
    '    Else
    '        tbxSeikyuuSakiShriSakiMei.Text = ""
    '        tbxSeikyuuSakiShriSakiKana.Text = ""
    '        tbxSkysySoufuYuubinNo.Text = ""
    '        tbxSkysySoufuJyuusyo1.Text = ""
    '        tbxSkysySoufuTelNo.Text = ""
    '        tbxSkysySoufuJyuusyo2.Text = ""
    '        tbxShriYouFaxNo.Text = ""

    '        tbxSeikyuuSakiShriSakiMei.Attributes.Add("readonly", "true;")
    '        tbxSeikyuuSakiShriSakiKana.Attributes.Add("readonly", "true;")
    '        tbxSkysySoufuYuubinNo.Attributes.Add("readonly", "true;")
    '        tbxSkysySoufuJyuusyo1.Attributes.Add("readonly", "true;")
    '        tbxSkysySoufuTelNo.Attributes.Add("readonly", "true;")
    '        tbxSkysySoufuJyuusyo2.Attributes.Add("readonly", "true;")
    '        tbxShriYouFaxNo.Attributes.Add("readonly", "true;")
    '        btnKensakuSeikyuuSoufuCopy.Enabled = False
    '        btnKensakuSeikyuuSyo.Enabled = False
    '    End If
    'End Sub
    '20100712�@�d�l�ύX�@�폜�@�n���R������

    ''' <summary>
    ''' �c�Ə������{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearchEigyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEigyousyo.Click
        Dim strScript As String = ""
        '�f�[�^�擾
        Dim dtEigyousyoTable As New Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable
        dtEigyousyoTable = EigyousyoMasterBL.SelEigyousyoInfo(tbxEigyousyo_Cd.Text, "", "btnSearch")

        '�������ʂ�1���������ꍇ
        If dtEigyousyoTable.Rows.Count = 1 Then
            tbxEigyousyo_Cd.Text = dtEigyousyoTable.Item(0).eigyousyo_cd.ToString 
            tbxEigyousyo_Mei.Text = dtEigyousyoTable.Item(0).eigyousyo_mei
        Else
            tbxEigyousyo_Mei.Text = ""
            strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('�c�Ə�')+'&soukoCd='+escape('#')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxEigyousyo_Cd.ClientID & _
                    "&objMei=" & tbxEigyousyo_Mei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxEigyousyo_Cd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxEigyousyo_Mei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
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

        data = (EigyousyoMasterBL.GetMailAddress(Me.tbxYuubinNo.Text.Replace("-", String.Empty).Trim))
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

    '20100712�@�d�l�ύX�@�폜�@�n���R������
    '''' <summary>
    '''' ���������t��X�֔ԍ�.�����{�^������������
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub btnKensakuSeikyuuSyo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSyo.Click
    '    '�Z��
    '    Dim data As DataSet
    '    Dim jyuusyo As String
    '    Dim jyuusyoMei As String
    '    Dim jyuusyoNo As String

    '    '�Z���擾
    '    Dim csScript As New StringBuilder

    '    data = (EigyousyoMasterBL.GetMailAddress(Me.tbxSkysySoufuYuubinNo.Text.Replace("-", String.Empty).Trim))
    '    If data.Tables(0).Rows.Count = 1 Then

    '        jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
    '        jyuusyoNo = jyuusyo.Split(",")(0)
    '        jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))
    '        If jyuusyoNo.Length > 3 Then
    '            jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
    '        End If

    '        csScript.AppendLine("if(document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
    '        csScript.AppendLine("if (!confirm('�����f�[�^������܂����㏑�����Ă�낵���ł����B')){}else{ " & vbCrLf)

    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

    '        csScript.AppendLine("}" & vbCrLf)

    '        csScript.AppendLine("}else{" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

    '        csScript.AppendLine("}" & vbCrLf)
    '    Else
    '        csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxSkysySoufuYuubinNo.ClientID & "','" & Me.tbxSkysySoufuJyuusyo1.ClientID & "','" & Me.tbxSkysySoufuJyuusyo2.ClientID & "');")
    '    End If
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openWindowYuubin", csScript.ToString, True)
    'End Sub
    '20100712�@�d�l�ύX�@�폜�@�n���R������

    '20100712�@�d�l�ύX�@�폜�@�n���R������
    '''' <summary>
    '''' �����於�E���t�Z���ɃR�s�[
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Protected Sub btnKensakuSeikyuuSoufuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSoufuCopy.Click
    '    'tbxSeikyuuSakiShriSakiMei.Text = tbxEigyousyoMei.Text
    '    'tbxSeikyuuSakiShriSakiKana.Text = tbxEigyousyoMeiKana.Text
    '    tbxSkysySoufuYuubinNo.Text = tbxYuubinNo.Text
    '    tbxSkysySoufuJyuusyo1.Text = tbxJyuusyo1.Text
    '    tbxSkysySoufuTelNo.Text = tbxTelNo.Text
    '    tbxSkysySoufuJyuusyo2.Text = tbxJyuusyo2.Text
    '    tbxShriYouFaxNo.Text = tbxFaxNo.Text
    'End Sub
    '20100712�@�d�l�ύX�@�폜�@�n���R������

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
        '�c�Ə�
        tbxEigyousyo_Cd.Text = ""
        '�c�Ə���
        tbxEigyousyo_Mei.Text = ""
    End Sub

    ''' <summary>
    ''' �o�^�ƏC���l������
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetMeisaiData() As Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable
        Dim dtEigyousyoDataSet As New Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable

        dtEigyousyoDataSet.Rows.Add(dtEigyousyoDataSet.NewRow)
        '���
        dtEigyousyoDataSet.Item(0).torikesi = IIf(chkTorikesi.Checked, "1", "0")
        '�c�Ə��R�[�h
        dtEigyousyoDataSet.Item(0).eigyousyo_cd = tbxEigyousyoCd.Text.ToUpper
        '�c�Ə���
        dtEigyousyoDataSet.Item(0).eigyousyo_mei = tbxEigyousyoMei.Text
        '�c�Ə��J�i
        dtEigyousyoDataSet.Item(0).eigyousyo_kana = tbxEigyousyoMeiKana.Text
        '�c�Ə����󎚗L��
        dtEigyousyoDataSet.Item(0).eigyousyo_mei_inji_umu = Me.ddlEigyousyoMeiInjiUmu.SelectedValue
        '������
        dtEigyousyoDataSet.Item(0).busyo_mei = tbxBusyoMei.Text
        '�X�֔ԍ�
        dtEigyousyoDataSet.Item(0).yuubin_no = tbxYuubinNo.Text
        '�Z���P
        dtEigyousyoDataSet.Item(0).jyuusyo1 = tbxJyuusyo1.Text
        '�d�b�ԍ�
        dtEigyousyoDataSet.Item(0).tel_no = tbxTelNo.Text
        '�Z���Q
        dtEigyousyoDataSet.Item(0).jyuusyo2 = tbxJyuusyo2.Text
        'FAX�ԍ�
        dtEigyousyoDataSet.Item(0).fax_no = tbxFaxNo.Text
        
        '������敪
        dtEigyousyoDataSet.Item(0).seikyuu_saki_kbn = ddlSeikyuuSaki.SelectedValue
        '������R�[�h
        dtEigyousyoDataSet.Item(0).seikyuu_saki_cd = tbxSeikyuuSakiCd.Text.ToUpper
        '������}��
        dtEigyousyoDataSet.Item(0).seikyuu_saki_brc = tbxSeikyuuSakiBrc.Text.ToUpper

        '=========2012/04/10 �ԗ� 405738 �ǉ���================================
        With dtEigyousyoDataSet.Item(0)
            '�W�vFC�p����
            .syuukei_fc_ten_cd = Me.tbxSyuukeiFcCd.Text.Trim
            '�G���A�R�[�h
            .eria_cd = Me.ddlArea.SelectedValue.Trim
            '�u���b�N�R�[�h
            .block_cd = Me.ddlBlock.SelectedValue.Trim
            'FC�X�敪
            .fc_ten_kbn = Me.ddlFcTenKbn.SelectedValue.Trim
            'FC����N��
            .fc_nyuukai_date = Me.tbxFcNyuukaiYM.Text.Trim
            'FC�މ�N��
            .fc_taikai_date = Me.tbxFcTaikaiYM.Text.Trim
            '(FC)������ЃR�[�h
            .fc_tys_kaisya_cd = Left(Me.tbxTyousaKaisyaCd.Text, 4)
            '(FC)���Ə��R�[�h
            .fc_jigyousyo_cd = Mid(Me.tbxTyousaKaisyaCd.Text.Trim, 5, 2)
        End With
        '=========2012/04/10 �ԗ� 405738 �ǉ���================================

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        ''�����於
        'dtEigyousyoDataSet.Item(0).seikyuu_saki_mei = tbxSeikyuuSakiShriSakiMei.Text
        ''�����於�J�i
        'dtEigyousyoDataSet.Item(0).seikyuu_saki_kana = tbxSeikyuuSakiShriSakiKana.Text
        ''���������t��X�֔ԍ�
        'dtEigyousyoDataSet.Item(0).skysy_soufu_yuubin_no = tbxSkysySoufuYuubinNo.Text
        ''���������t��Z���P
        'dtEigyousyoDataSet.Item(0).skysy_soufu_jyuusyo1 = tbxSkysySoufuJyuusyo1.Text
        ''���������t��d�b�ԍ�
        'dtEigyousyoDataSet.Item(0).skysy_soufu_tel_no = tbxSkysySoufuTelNo.Text
        ''���������t��Z���Q
        'dtEigyousyoDataSet.Item(0).skysy_soufu_jyuusyo2 = tbxSkysySoufuJyuusyo2.Text
        ''���������t��FAX�ԍ�
        'dtEigyousyoDataSet.Item(0).skysy_soufu_fax_no = tbxShriYouFaxNo.Text
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        '�o�^�A�X�V���O�C�����[�U�[ID
        dtEigyousyoDataSet.Item(0).upd_login_user_id = ViewState("UserId")
        dtEigyousyoDataSet.Item(0).add_login_user_id = ViewState("UserId")
        dtEigyousyoDataSet.Item(0).upd_datetime = hidUPDTime.Value

        Return dtEigyousyoDataSet
    End Function

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <param name="strErr">�G���[���b�Z�[�W</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '�c�Ə��R�[�h
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxEigyousyoCd.Text, "�c�Ə��R�[�h")
            If strErr <> "" Then
                strID = tbxEigyousyoCd.ClientID
            End If
        End If
        If strErr = "" And tbxEigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxEigyousyoCd.Text, "�c�Ə��R�[�h")
            If strErr <> "" Then
                strID = tbxEigyousyoCd.ClientID
            End If
        End If
        '�c�Ə����󎚗L��
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(ddlEigyousyoMeiInjiUmu.Text, "�c�Ə����󎚗L���R�[�h")
            If strErr <> "" Then
                strID = ddlEigyousyoMeiInjiUmu.ClientID
            End If
        End If
        '�c�Ə���
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxEigyousyoMei.Text, "�c�Ə���")
            If strErr <> "" Then
                strID = tbxEigyousyoMei.ClientID
            End If
        End If
        If strErr = "" And tbxEigyousyoMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxEigyousyoMei.Text, 40, "�c�Ə���", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxEigyousyoMei.ClientID
            End If
        End If
        If strErr = "" And tbxEigyousyoMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxEigyousyoMei.Text, "�c�Ə���")
            If strErr <> "" Then
                strID = tbxEigyousyoMei.ClientID
            End If
        End If
        '�c�Ə����J�i
        If strErr = "" And tbxEigyousyoMeiKana.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxEigyousyoMeiKana.Text, 20, "�c�Ə��J�i", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxEigyousyoMeiKana.ClientID
            End If
        End If
        If strErr = "" And tbxEigyousyoMeiKana.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxEigyousyoMeiKana.Text, "�c�Ə��J�i")
            If strErr <> "" Then
                strID = tbxEigyousyoMeiKana.ClientID
            End If
        End If
        '�X�֔ԍ�
        If strErr = "" And tbxYuubinNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxYuubinNo.Text, "�X�֔ԍ�", "1")
            If strErr <> "" Then
                strID = tbxYuubinNo.ClientID
            End If
        End If
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
        '������
        If strErr = "" And tbxBusyoMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxBusyoMei.Text, 50, "������", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxBusyoMei.ClientID
            End If
        End If
        If strErr = "" And tbxBusyoMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxBusyoMei.Text, "������")
            If strErr <> "" Then
                strID = tbxBusyoMei.ClientID
            End If
        End If

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

        '=========2012/04/10 �ԗ� 405738 �ǉ���================================
        '�W�vFC�p����(���p�p����)
        If strErr = "" And Me.tbxSyuukeiFcCd.Text.Trim <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(Me.tbxSyuukeiFcCd.Text.Trim, "�W�vFC�p�R�[�h")
            If strErr <> "" Then
                strID = Me.tbxSyuukeiFcCd.ClientID
            End If
        End If

        '�e�b ����N��(�L�����t�`�F�b�N)
        If strErr = "" And tbxFcNyuukaiYM.Text.Trim <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxFcNyuukaiYM.Text.Trim, "�e�b ����N��")
            If strErr <> "" Then
                strID = tbxFcNyuukaiYM.ClientID
            End If
        End If
        '�e�b �މ�N��(�L�����t�`�F�b�N)
        If strErr = "" And tbxFcTaikaiYM.Text.Trim <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxFcTaikaiYM.Text.Trim, "�e�b �މ�N��")
            If strErr <> "" Then
                strID = tbxFcTaikaiYM.ClientID
            End If
        End If

        '(FC)�������(FC�X�敪�������̏ꍇ�A���̓`�F�b�N���K�v)
        If (strErr = "") AndAlso (Me.ddlFcTenKbn.SelectedValue.Trim.Equals("1")) Then
            strErr = commoncheck.CheckHissuNyuuryoku(Me.tbxTyousaKaisyaCd.Text, "������ЃR�[�h")
            If strErr <> "" Then
                strID = Me.tbxTyousaKaisyaCd.ClientID
            End If
        End If

        '(FC)�������(���p�p����)
        If (strErr = "") AndAlso (Not Me.tbxTyousaKaisyaCd.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.ChkHankakuEisuuji(Me.tbxTyousaKaisyaCd.Text.Trim, "������ЃR�[�h")
            If strErr <> "" Then
                strID = Me.tbxTyousaKaisyaCd.ClientID
            End If
        End If

        '=========2012/04/10 �ԗ� 405738 �ǉ���================================

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        ''�����於
        'If strErr = "" And tbxSeikyuuSakiShriSakiMei.Text <> "" Then
        '    strErr = commoncheck.CheckByte(tbxSeikyuuSakiShriSakiMei.Text, 80, "�����於", kbn.ZENKAKU)
        '    If strErr <> "" Then
        '        strID = tbxSeikyuuSakiShriSakiMei.ClientID
        '    End If
        'End If
        'If strErr = "" And tbxSeikyuuSakiShriSakiMei.Text <> "" Then
        '    strErr = commoncheck.CheckKinsoku(tbxSeikyuuSakiShriSakiMei.Text, "�����於")
        '    If strErr <> "" Then
        '        strID = tbxSeikyuuSakiShriSakiMei.ClientID
        '    End If
        'End If
        ''�����於�J�i
        'If strErr = "" And tbxSeikyuuSakiShriSakiKana.Text <> "" Then
        '    strErr = commoncheck.CheckByte(tbxSeikyuuSakiShriSakiKana.Text, 40, "������J�i", kbn.ZENKAKU)
        '    If strErr <> "" Then
        '        strID = tbxSeikyuuSakiShriSakiKana.ClientID
        '    End If
        'End If
        'If strErr = "" And tbxSeikyuuSakiShriSakiKana.Text <> "" Then
        '    strErr = commoncheck.CheckKinsoku(tbxSeikyuuSakiShriSakiKana.Text, "������J�i")
        '    If strErr <> "" Then
        '        strID = tbxSeikyuuSakiShriSakiKana.ClientID
        '    End If
        'End If
        ''���������t��X�֔ԍ�
        'If strErr = "" And tbxSkysySoufuYuubinNo.Text <> "" Then
        '    strErr = commoncheck.CheckHankakuHaifun(tbxSkysySoufuYuubinNo.Text, "���������t��X�֔ԍ�", "1")
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        ''���������t��Z���P
        'If strErr = "" And tbxSkysySoufuJyuusyo1.Text <> "" Then
        '    strErr = commoncheck.CheckByte(tbxSkysySoufuJyuusyo1.Text, 40, "���������t��Z���P", kbn.ZENKAKU)
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuJyuusyo1.ClientID
        '    End If
        'End If
        'If strErr = "" And tbxSkysySoufuJyuusyo1.Text <> "" Then
        '    strErr = commoncheck.CheckKinsoku(tbxSkysySoufuJyuusyo1.Text, "���������t��Z���P")
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuJyuusyo1.ClientID
        '    End If
        'End If
        ''���������t��d�b�ԍ�
        'If strErr = "" And tbxSkysySoufuTelNo.Text <> "" Then
        '    strErr = commoncheck.CheckHankakuHaifun(tbxSkysySoufuTelNo.Text, "���������t��d�b�ԍ�", "1")
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuTelNo.ClientID
        '    End If
        'End If
        ''���������t��Z���Q
        'If strErr = "" And tbxSkysySoufuJyuusyo2.Text <> "" Then
        '    strErr = commoncheck.CheckByte(tbxSkysySoufuJyuusyo2.Text, 40, "���������t��Z���Q", kbn.ZENKAKU)
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuJyuusyo2.ClientID
        '    End If
        'End If
        'If strErr = "" And tbxSkysySoufuJyuusyo2.Text <> "" Then
        '    strErr = commoncheck.CheckKinsoku(tbxSkysySoufuJyuusyo2.Text, "���������t��Z���Q")
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuJyuusyo2.ClientID
        '    End If
        'End If
        ''���������t��FAX�ԍ�
        'If strErr = "" And tbxShriYouFaxNo.Text <> "" Then
        '    strErr = commoncheck.CheckHankakuHaifun(tbxShriYouFaxNo.Text, "���������t��FAX�ԍ�", "1")
        '    If strErr <> "" Then
        '        strID = tbxShriYouFaxNo.ClientID
        '    End If
        'End If
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        Return strID

    End Function

    ''' <summary>
    ''' ���׃f�[�^���擾
    ''' </summary>
    ''' <param name="Eigyousyo_Cd"></param>
    ''' <param name="btn"></param>
    ''' <remarks></remarks>
    Sub GetMeisaiData(ByVal Eigyousyo_Cd As String, _
                      ByVal EigyousyoCd As String, _
                      Optional ByVal btn As String = "")

        Dim strErr As String = ""
        Dim dtEigyousyoDataSet As New Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable
        dtEigyousyoDataSet = EigyousyoMasterBL.SelEigyousyoInfo(Eigyousyo_Cd, EigyousyoCd, btn)

        If dtEigyousyoDataSet.Rows.Count = 1 Then
            With dtEigyousyoDataSet.Item(0)
                '���
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '�c�Ə��R�[�h
                tbxEigyousyoCd.Text = TrimNull(.eigyousyo_cd)
                '�c�Ə���
                tbxEigyousyoMei.Text = TrimNull(.eigyousyo_mei)
                If btn = "btnSearch" Then
                    tbxEigyousyo_Mei.Text = TrimNull(.eigyousyo_mei)
                    tbxEigyousyo_Cd.Text = TrimNull(.eigyousyo_cd).ToUpper
                End If
                '������Ж��J�i
                tbxEigyousyoMeiKana.Text = TrimNull(.eigyousyo_kana)
                '�c�Ə����󎚗L��
                SetDropSelect(ddlEigyousyoMeiInjiUmu, TrimNull(.eigyousyo_mei_inji_umu))

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
                '������
                tbxBusyoMei.Text = TrimNull(.busyo_mei)

                '������敪
                SetDropSelect(ddlSeikyuuSaki, TrimNull(.seikyuu_saki_kbn))
                '������R�[�h
                tbxSeikyuuSakiCd.Text = TrimNull(.seikyuu_saki_cd).ToUpper
                '������}��
                tbxSeikyuuSakiBrc.Text = TrimNull(.seikyuu_saki_brc).ToUpper
                '�����於
                tbxSeikyuuSakiMei.Text = TrimNull(.seikyuu_saki_mei1)
                '������R�[�h
                hidSeikyuuSakiCd.Value = TrimNull(.seikyuu_saki_cd).ToUpper
                '������}��
                hidSeikyuuSakiBrc.Value = TrimNull(.seikyuu_saki_brc).ToUpper
                '������敪
                hidSeikyuuSakiKbn.Value = TrimNull(.seikyuu_saki_kbn)
                '=========2012/04/10 �ԗ� 405738 �ǉ���================================
                '�W�vFC�p����
                Me.tbxSyuukeiFcCd.Text = TrimNull(.syuukei_fc_ten_cd)
                '�G���A�R�[�h
                Call Me.SetDropSelect(Me.ddlArea, TrimNull(.eria_cd))
                '�u���b�N�R�[�h
                Call Me.SetDropSelect(Me.ddlBlock, TrimNull(.block_cd))
                'FC�X�敪
                Call Me.SetDropSelect(Me.ddlFcTenKbn, TrimNull(.fc_ten_kbn))
                'FC����N��
                Me.tbxFcNyuukaiYM.Text = TrimNull(.fc_nyuukai_date)
                'FC�މ�N��
                Me.tbxFcTaikaiYM.Text = TrimNull(.fc_taikai_date)
                '(FC)������ЃR�[�h
                Me.tbxTyousaKaisyaCd.Text = TrimNull(.fc_tys_kaisya_cd) & TrimNull(.fc_jigyousyo_cd)

                '������Џ����Z�b�g����
                Call Me.SetTyousaKaisyaInfo()

                '�Œ�`���[�W���Z�b�g����
                Call Me.SetKoteiTyaaji()
                '=========2012/04/10 �ԗ� 405738 �ǉ���================================

                '20100712�@�d�l�ύX�@�폜�@�n���R������
                ''�����於
                'tbxSeikyuuSakiShriSakiMei.Text = TrimNull(.seikyuu_saki_mei)
                ''������J�i
                'tbxSeikyuuSakiShriSakiKana.Text = TrimNull(.seikyuu_saki_kana)
                ''���������t��X�֔ԍ�
                'tbxSkysySoufuYuubinNo.Text = TrimNull(.skysy_soufu_yuubin_no)
                ''���������t��Z���P
                'tbxSkysySoufuJyuusyo1.Text = TrimNull(.skysy_soufu_jyuusyo1)
                ''���������t��d�b�ԍ�
                'tbxSkysySoufuTelNo.Text = TrimNull(.skysy_soufu_tel_no)
                ''���������t��Z���Q
                'tbxSkysySoufuJyuusyo2.Text = TrimNull(.skysy_soufu_jyuusyo2)
                ''���������t��FAX�ԍ�
                'tbxShriYouFaxNo.Text = TrimNull(.skysy_soufu_fax_no)
                '20100712�@�d�l�ύX�@�폜�@�n���R������

                hidUPDTime.Value = TrimNull(.upd_datetime)
                UpdatePanelA.Update()
            End With
            '������
            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
                btnFcTenKousin.Enabled = False
            Else
                btnSyuusei.Enabled = True
                btnTouroku.Enabled = False
                btnFcTenKousin.Enabled = True
            End If
            tbxEigyousyoCd.Attributes.Add("readonly", "true;")
        Else
            MeisaiClear()
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            If btn <> "btnSearch" Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = True
                btnFcTenKousin.Enabled = False
                tbxEigyousyoCd.Attributes.Remove("readonly")
            End If
            tbxEigyousyo_Mei.Text = ""
        End If

        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")
        hidConfirm2.Value = ""

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        'If btn = "btnSearch" Or btn = "btnSyuusei" Or btn = "btnTouroku" Then
        '    tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
        '    tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
        '    tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
        '    tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
        '    tbxSkysySoufuTelNo.Attributes.Remove("readonly")
        '    tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
        '    tbxShriYouFaxNo.Attributes.Remove("readonly")
        '    btnKensakuSeikyuuSoufuCopy.Enabled = True
        '    btnKensakuSeikyuuSyo.Enabled = True
        'End If
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        hidSyousai.Value = ""
    End Sub

    ''' <summary>
    ''' ���׍��ڃN���A
    ''' </summary>
    ''' <remarks></remarks>
    Sub MeisaiClear()
        '���
        chkTorikesi.Checked = False
        '�c�Ə��R�[�h
        tbxEigyousyoCd.Text = ""
        '�c�Ə���
        tbxEigyousyoMei.Text = ""
        '�c�Ə����J�i
        tbxEigyousyoMeiKana.Text = ""
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
        '������
        tbxBusyoMei.Text = ""

        '������
        tbxSeikyuuSakiCd.Text = ""
        '������}��
        tbxSeikyuuSakiBrc.Text = ""
        '�����於
        tbxSeikyuuSakiMei.Text = ""

        '=========2012/04/10 �ԗ� 405738 �ǉ���================================
        '�W�vFC�p����
        Me.tbxSyuukeiFcCd.Text = String.Empty
        '�G���A�R�[�h
        Me.ddlArea.SelectedIndex = 0
        '�u���b�N�R�[�h
        Me.ddlBlock.SelectedIndex = 0
        'FC�X�敪
        Me.ddlFcTenKbn.SelectedIndex = 0
        'FC����N��
        Me.tbxFcNyuukaiYM.Text = String.Empty
        'FC�މ�N��
        Me.tbxFcTaikaiYM.Text = String.Empty
        '(FC)������ЃR�[�h
        Me.tbxTyousaKaisyaCd.Text = String.Empty

        '������Џ����N���A����
        Call Me.ClearTyousakaisya()

        '�Œ�`���[�W
        Me.btnKoteiTyaaji.Enabled = False
        Me.lblKoteiTyaaji.Text = String.Empty
        '=========2012/04/10 �ԗ� 405738 �ǉ���================================

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        ''������x���於
        'tbxSeikyuuSakiShriSakiMei.Text = ""
        ''������x���於�J�i
        'tbxSeikyuuSakiShriSakiKana.Text = ""
        ''���������t��X�֔ԍ�
        'tbxSkysySoufuYuubinNo.Text = ""
        ''���������t��Z���P
        'tbxSkysySoufuJyuusyo1.Text = ""
        ''���������t��d�b�ԍ�
        'tbxSkysySoufuTelNo.Text = ""
        ''���������t��Z���Q
        'tbxSkysySoufuJyuusyo2.Text = ""
        ''�x���pFAX�ԍ�
        'tbxShriYouFaxNo.Text = ""
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        '�c�Ə����󎚗L��
        SetDropSelect(ddlEigyousyoMeiInjiUmu, "")
        '������
        SetDropSelect(ddlSeikyuuSaki, "")

        'HIDDEN�ݒ�
        Me.hidSeikyuuSakiCd.Value = ""
        Me.hidSeikyuuSakiBrc.Value = ""
        Me.hidSeikyuuSakiKbn.Value = ""

        Me.hidConfirm.Value = ""

        '������V�K�o�^
        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")

        '20100712�@�d�l�ύX�@�폜�@�n���R������
        'tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
        'tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
        'tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
        'tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
        'tbxSkysySoufuTelNo.Attributes.Remove("readonly")
        'tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
        'tbxShriYouFaxNo.Attributes.Remove("readonly")
        'btnKensakuSeikyuuSoufuCopy.Enabled = True
        'btnKensakuSeikyuuSyo.Enabled = True
        '20100712�@�d�l�ύX�@�폜�@�n���R������

        hidUPDTime.Value = ""
        '����
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
            btnFcTenKousin.Enabled = False
        Else
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
            btnFcTenKousin.Enabled = False
        End If

        tbxEigyousyoCd.Attributes.Remove("readonly")
        UpdatePanelA.Update()
    End Sub


    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")

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

            'Disable
            .AppendLine("function fncDisable()")
            .AppendLine("{")
            .AppendLine("   var btnSearchEigyousyo = document.getElementById('" & Me.btnSearchEigyousyo.ClientID & "')")
            .AppendLine("   var btnSearch = document.getElementById('" & Me.btnSearch.ClientID & "')")
            .AppendLine("   var btnClear = document.getElementById('" & Me.btnClear.ClientID & "')")
            .AppendLine("   var btnSyuusei = document.getElementById('" & Me.btnSyuusei.ClientID & "')")
            .AppendLine("   var btnTouroku = document.getElementById('" & Me.btnTouroku.ClientID & "')")
            .AppendLine("   var btnClearMeisai = document.getElementById('" & Me.btnClearMeisai.ClientID & "')")
            .AppendLine("   var btnFcTenKousin = document.getElementById('" & Me.btnFcTenKousin.ClientID & "')")
            .AppendLine("   var btnKensakuYuubinNo = document.getElementById('" & Me.btnKensakuYuubinNo.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSaki = document.getElementById('" & Me.btnKensakuSeikyuuSaki.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSyousai = document.getElementById('" & Me.btnKensakuSeikyuuSyousai.ClientID & "')")
            '20100712�@�d�l�ύX�@�폜�@�n���R������
            '.AppendLine("   var btnKensakuSeikyuuSoufuCopy = document.getElementById('" & Me.btnKensakuSeikyuuSoufuCopy.ClientID & "')")
            '.AppendLine("   var btnKensakuSeikyuuSyo = document.getElementById('" & Me.btnKensakuSeikyuuSyo.ClientID & "')")
            '20100712�@�d�l�ύX�@�폜�@�n���R������
            .AppendLine("   var btnFC = document.getElementById('" & Me.btnFC.ClientID & "')")
            .AppendLine("   var btnOK = document.getElementById('" & Me.btnOK.ClientID & "')")
            .AppendLine("   var btnNO = document.getElementById('" & Me.btnNO.ClientID & "')")
            .AppendLine("   var my_array = new Array(12);")
            .AppendLine("   my_array[0] = btnSearchEigyousyo;")
            .AppendLine("   my_array[1] = btnSearch;")
            .AppendLine("   my_array[2] = btnClear;")
            .AppendLine("   my_array[3] = btnSyuusei;")
            .AppendLine("   my_array[4] = btnTouroku;")
            .AppendLine("   my_array[5] = btnClearMeisai;")
            .AppendLine("   my_array[6] = btnFcTenKousin;")
            .AppendLine("   my_array[7] = btnKensakuYuubinNo;")
            .AppendLine("   my_array[8] = btnKensakuSeikyuuSaki;")
            .AppendLine("   my_array[9] = btnKensakuSeikyuuSyousai;")
            '20100712�@�d�l�ύX�@�폜�@�n���R������
            '.AppendLine("   my_array[10] = btnKensakuSeikyuuSoufuCopy;")
            '.AppendLine("   my_array[11] = btnKensakuSeikyuuSyo;")
            '20100712�@�d�l�ύX�@�폜�@�n���R������
            .AppendLine("   my_array[10] = btnFC;")
            .AppendLine("   my_array[11] = btnOK;")
            .AppendLine("   my_array[12] = btnNO;")
            .AppendLine("   for (i = 0; i < 13; i++){")
            .AppendLine("       my_array[i].disabled = true;")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("function disableButton1()")
            .AppendLine("{")
            .AppendLine("   window.setTimeout('fncDisable()',0);")
            .AppendLine("   return true;")
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

            '�����挟���{�^��������
            .AppendLine("function fncConfirm()")
            .AppendLine("{")
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   if(confirm('���̉c�Ə��o�^���ɐ�����}�X�^�ɓo�^���܂����H')){")
            .AppendLine("       hidConfirm.value = 'OK';")
            .AppendLine("   }else{")
            .AppendLine("       hidConfirm.value = 'NO';")
            .AppendLine("   }")
            .AppendLine("   document.getElementById('" & Me.Form.Name & "').submit();")
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

            '������
            '.AppendLine("   var hidConfirm2 = document.getElementById('" & Me.hidConfirm2.ClientID & "')")
            '.AppendLine("   if((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!='')){")
            '.AppendLine("       if(hidConfirm2.value=='����'){")
            '.AppendLine("           alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "������") & "');")
            '.AppendLine("           ddlSeikyuuSaki.focus();")
            '.AppendLine("           return false;")
            '.AppendLine("       }")
            '.AppendLine("   }")

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

            .AppendLine("   }else{")
            .AppendLine("       return true;")
            .AppendLine("   }")
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

            '�u������V�K�o�^�v�����\�����ʏ���
            '�t�H�[�J�X
            .AppendLine("function fncFocus()")
            .AppendLine("{")
            .AppendLine("   var tbxEigyousyoCd = document.getElementById('" & Me.tbxEigyousyoCd.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

            .AppendLine("   var hidChange1 = document.getElementById('" & Me.hidChange1.ClientID & "')")
            .AppendLine("   var hidChange3 = document.getElementById('" & Me.hidChange3.ClientID & "')")
            .AppendLine("   var hidChange4 = document.getElementById('" & Me.hidChange4.ClientID & "')")
            .AppendLine("   var hidChange5 = document.getElementById('" & Me.hidChange5.ClientID & "')")

            .AppendLine("   hidChange1.value = tbxEigyousyoCd.value;")
            .AppendLine("   hidChange3.value = ddlSeikyuuSaki.value;")
            .AppendLine("   hidChange4.value = tbxSeikyuuSakiCd.value;")
            .AppendLine("   hidChange5.value = tbxSeikyuuSakiBrc.value;")
            .AppendLine("}")
            '���X�g�t�H�[�J�X
            .AppendLine("function fncblur()")
            .AppendLine("{")
            .AppendLine("   var tbxEigyousyoCd = document.getElementById('" & Me.tbxEigyousyoCd.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

            .AppendLine("   var hidChange1 = document.getElementById('" & Me.hidChange1.ClientID & "')")
            .AppendLine("   var hidChange3 = document.getElementById('" & Me.hidChange3.ClientID & "')")
            .AppendLine("   var hidChange4 = document.getElementById('" & Me.hidChange4.ClientID & "')")
            .AppendLine("   var hidChange5 = document.getElementById('" & Me.hidChange5.ClientID & "')")

            .AppendLine("   var hidConfirm1 = document.getElementById('" & Me.hidConfirm1.ClientID & "')")
            .AppendLine("   var hidConfirm2 = document.getElementById('" & Me.hidConfirm2.ClientID & "')")

            .AppendLine("   if(hidConfirm1.value=='OK'){")
            .AppendLine("       if((tbxEigyousyoCd.value != hidChange1.value)||(ddlSeikyuuSaki.value != hidChange3.value)||(tbxSeikyuuSakiCd.value != hidChange4.value)||(tbxSeikyuuSakiBrc.value != hidChange5.value)){")
            .AppendLine("           fncHyouji();")
            .AppendLine("           hidConfirm2.value='����';")
            .AppendLine("       }")
            .AppendLine("   }else{")
            .AppendLine("   }")
            .AppendLine("}")

            '�����挟���{�^��������
            .AppendLine("function fncKousin(obj)")
            .AppendLine("{")
            .AppendLine("   var hidKousin = document.getElementById('" & Me.hidKousin.ClientID & "')")
            .AppendLine("   if(confirm(obj + '�̏Z�������X�V�������s���܂��B�X�����ł����H')){")
            .AppendLine("       hidKousin.value = 'OK';")
            .AppendLine("   }else{")
            .AppendLine("       hidKousin.value = 'NO';")
            .AppendLine("   }")
            .AppendLine("   document.getElementById('" & Me.Form.Name & "').submit();")
            .AppendLine("}")
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            '������� �Z�����R�s�[�{�^��������
            .AppendLine("function fncCopyTyousaKaisya()")
            .AppendLine("{")
            .AppendLine("   document.getElementById('" & Me.tbxYuubinNo.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaYuubinNo.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaJyuusyo1.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaJyuusyo2.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.tbxTelNo.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaTelNo.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.tbxFaxNo.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaFaxNo.ClientID & "').value;")
            .AppendLine("}")
            '���t�`�F�b�N(yyyy/mm)
            .AppendLine("function fncCheckNengetu(obj,objId)")
            .AppendLine("{ ")
            .AppendLine("	if (obj.value==''){return true;}")
            .AppendLine("	var checkFlg = true;")
            .AppendLine("	obj.value = obj.value.Trim();")
            .AppendLine("	var val = obj.value;")
            .AppendLine("	val = SetDateNoSign(val,'/');")
            .AppendLine("	val = SetDateNoSign(val,'-');")
            .AppendLine("	val = val+'01';")
            .AppendLine("	if(val == '')return;")
            .AppendLine("	val = removeSlash(val);")
            .AppendLine("	val = val.replace(/\-/g, '');")
            .AppendLine("	if(val.length == 6){")
            .AppendLine("		if(val.substring(0, 2) > 70){")
            .AppendLine("			val = '19' + val;")
            .AppendLine("		}else{")
            .AppendLine("			val = '20' + val;")
            .AppendLine("		}")
            .AppendLine("	}else if(val.length == 4){")
            .AppendLine("		dd = new Date();")
            .AppendLine("		year = dd.getFullYear();")
            .AppendLine("		val = year + val;")
            .AppendLine("	}")
            .AppendLine("	if(val.length != 8){")
            .AppendLine("		checkFlg = false;")
            .AppendLine("	}else{  //8���̏ꍇ")
            .AppendLine("		val = addSlash(val);")
            .AppendLine("		var arrD = val.split('/');")
            .AppendLine("		if(checkDateVali(arrD[0],arrD[1],arrD[2]) == false){")
            .AppendLine("			checkFlg = false; ")
            .AppendLine("		}")
            .AppendLine("	}")
            .AppendLine("	if(!checkFlg){")
            .AppendLine("		event.returnValue = false;")
            .AppendLine("        if (objId == '�e�b ����N��'){")
            .AppendLine("            alert('" & Replace(Messages.Instance.MSG014E, "@PARAM1", "�e�b ����N��").ToString & "');")
            .AppendLine("        }else if(objId == '�e�b �މ�N��'){")
            .AppendLine("            alert('" & Replace(Messages.Instance.MSG014E, "@PARAM1", "�e�b �މ�N��").ToString & "');")
            .AppendLine("        }")
            .AppendLine("        obj.focus();")
            .AppendLine("		obj.select();")
            .AppendLine("		return false;")
            .AppendLine("	}else{")
            .AppendLine("		obj.value = val.substring(0,7);")
            .AppendLine("	}")
            .AppendLine("}")
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    ''' <summary>
    ''' DDL�̏����ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetDdlListInf()
        '���h���b�v�_�E�����X�g�ݒ聫
        Dim ddlist As ListItem

        '�c�Ə����󎚗L��ddlEigyousyoMeiInjiUmu
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlEigyousyoMeiInjiUmu.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "����"
        ddlist.Value = "0"
        ddlEigyousyoMeiInjiUmu.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�L��"
        ddlist.Value = "1"
        ddlEigyousyoMeiInjiUmu.Items.Add(ddlist)

        '������敪ddlSeikyuuSaki
        'SetKakutyou(ddlSeikyuuSaki, "1")
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlSeikyuuSaki.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�������"
        ddlist.Value = "1"
        ddlSeikyuuSaki.Items.Add(ddlist)

        '=========2012/04/10 �ԗ� 405738 �ǉ���================================
        Dim dtDDL As New Data.DataTable

        '�G���A
        Me.ddlArea.Items.Clear()
        dtDDL = EigyousyoMasterBL.GetDdlList(60)
        Me.ddlArea.DataValueField = "code"
        Me.ddlArea.DataTextField = "meisyou"
        Me.ddlArea.DataSource = dtDDL
        Me.ddlArea.DataBind()
        '�擪�s
        Me.ddlArea.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '�u���b�N
        Me.ddlBlock.Items.Clear()
        dtDDL = EigyousyoMasterBL.GetDdlList(61)
        Me.ddlBlock.DataValueField = "code"
        Me.ddlBlock.DataTextField = "meisyou"
        Me.ddlBlock.DataSource = dtDDL
        Me.ddlBlock.DataBind()
        '�擪�s
        Me.ddlBlock.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'FC�X�敪
        Me.ddlFcTenKbn.Items.Clear()
        Me.ddlFcTenKbn.Items.Insert(0, New ListItem("������", "0"))
        Me.ddlFcTenKbn.Items.Insert(1, New ListItem("����", "1"))
        Me.ddlFcTenKbn.Items.Insert(2, New ListItem("�މ�", "3"))

        '=========2012/04/10 �ԗ� 405738 �ǉ���================================

    End Sub

    '20100712�@�d�l�ύX�@�폜�@�n���R������
    '''' <summary>
    '''' ������敪�h���b�v�_�E�����X�g�ݒ�
    '''' </summary>
    '''' <param name="ddl">�h���b�v�_�E�����X�g</param>
    '''' <param name="strSyubetu">���̎��</param>
    '''' <remarks></remarks>
    'Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
    '    Dim dtTable As New DataTable
    '    Dim intCount As Integer = 0
    '    dtTable = EigyousyoMasterBL.SelKakutyouInfo(strSyubetu)

    '    Dim ddlist As New ListItem
    '    ddlist.Text = ""
    '    ddlist.Value = ""
    '    ddl.Items.Add(ddlist)
    '    For intCount = 0 To dtTable.Rows.Count - 1
    '        ddlist = New ListItem
    '        ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & "�F" & TrimNull(dtTable.Rows(intCount).Item("meisyou"))
    '        ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
    '        ddl.Items.Add(ddlist)
    '    Next
    'End Sub
    '20100712�@�d�l�ύX�@�폜�@�n���R������

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
            dtTable = EigyousyoMasterBL.SelSeikyuuSaki(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSaki.SelectedValue, False)
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
    ''' FC�X�X�V����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFcTenKousin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFcTenKousin.Click

        ''���b�Z�[�W�\��
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "fncKousin('" & Me.tbxEigyousyoMei.Text & "');", True)
        '���b�Z�[�W�\��
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "FC_Kousin", "if(confirm('" & Me.tbxEigyousyoMei.Text & "�̏Z�������X�V�������s���܂��B�X�����ł����H')){window.setTimeout('objEBI(\'" & Me.btnFC.ClientID & "\').click()',10);}; ", True)

    End Sub

    ''' <summary>
    ''' FC�X�X�V����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFC.Click

        Me.hidKousin.Value = ""

        Dim dtKameiten As New Data.DataTable
        '�����X�}�X�^���擾����
        dtKameiten = EigyousyoMasterBL.SelKameiten(Me.tbxEigyousyoCd.Text)
        If dtKameiten.Rows.Count > 0 Then
            '�X�V�ǉ�����
            If EigyousyoMasterBL.SetKousinTuika(Me.tbxEigyousyoCd.Text, ViewState("UserId")) Then
                '���b�Z�[�W�\��
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('FC�X�Z�������X�V�������������܂����B');", True)
            Else
                '���b�Z�[�W�\��
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('FC�X�Z�������X�V���������s���܂����B');", True)
            End If
        Else
            '���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Me.tbxEigyousyoMei.Text & "��A�敪�̉����X�͂���܂���B');", True)
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
        dtSeikyuuSakiTouroku = EigyousyoMasterBL.SelSeikyuuSakiTouroku(tbxSeikyuuSakiBrc.Text)
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
        Me.hidConfirm1.Value = "NO"
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

    ''' <summary>
    ''' CSV�o��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        'CSV�f�[�^���擾����
        Dim dtCsv As New Data.DataTable
        dtCsv = EigyousyoMasterBL.GetEigyousyoCsv()

        '�V�X�e�����t���擾����
        Dim strSystemDate As String
        strSystemDate = EigyousyoMasterBL.GetSystemDateYMD().Rows(0).Item("system_date")

        'CSV�t�@�C�����ݒ�
        Dim strFileName As String
        strFileName = strSystemDate & "_Fc_Kameiten_Ichiran.csv"

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conEigyousyoCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For i As Integer = 0 To dtCsv.Rows.Count - 1
            With dtCsv.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), .Item(7), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), .Item(12), .Item(13), .Item(14), .Item(15), .Item(16), .Item(17), .Item(18), .Item(19), _
                                 .Item(20), .Item(21), .Item(22), .Item(23))
            End With
        Next


        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    ''' <summary>
    ''' ������� �I���{�^��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Private Sub btnTyousaKaisyaCd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousaKaisyaCd.Click

        '������ЃR�[�h
        Dim strTyousaKaisyaCd As String
        strTyousaKaisyaCd = Me.tbxTyousaKaisyaCd.Text.Trim

        '������Џ����N���A����
        Call Me.ClearTyousakaisya()

        If Not strTyousaKaisyaCd.Equals(String.Empty) Then
            '������ЃR�[�h�����͂̏ꍇ

            If EigyousyoMasterBL.GetTyousaKaisyaCount(strTyousaKaisyaCd) = 1 Then
                '������Џ����Z�b�g����
                Call Me.SetTyousaKaisyaInfo()
            Else
                '������Ќ�����ʂ�\������
                Call Me.ShowTyousakaisyaKensaku()
            End If

        Else
            '������ЃR�[�h�������͂̏ꍇ

            '������Ќ�����ʂ�\������
            Call Me.ShowTyousakaisyaKensaku()
        End If

    End Sub

    ''' <summary>
    ''' ������Ќ�����ʂ�\������
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Private Sub ShowTyousakaisyaKensaku()
        Dim strScript As String = ""

        strScript = "objSrchWin = window.open('search_tyousa.aspx?Kbn='+escape('����')+'&soukoCd='+escape('#')+'&FormName=" & Me.Page.Form.Name & _
                "&objCd=" & Me.tbxTyousaKaisyaCd.ClientID & _
                "&objMei=" & Me.tbxTyousaKaisyaMei.ClientID & _
                "&strCd='+escape(eval('document.all.'+'" & Me.tbxTyousaKaisyaCd.ClientID & "').value)+" & _
                "'&strMei='+escape(eval('document.all.'+'" & Me.tbxTyousaKaisyaMei.ClientID & "').value)+" & _
                "'&btnSelectId=" & Me.btnTyousakaisya.ClientID & "', " & _
                "'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowTyousakaisyaKensaku", strScript, True)

    End Sub

    ''' <summary>
    ''' ������Џ����N���A����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Private Sub ClearTyousakaisya()

        '������Ж�
        Me.tbxTyousaKaisyaMei.Text = String.Empty
        '������Ж��J�i
        Me.tbxTyousaKaisyaMeiKana.Text = String.Empty
        '(�������)��\�Җ�
        Me.tbxDaihyousyaMei.Text = String.Empty
        '(�������)��E��
        Me.tbxYasyokuMei.Text = String.Empty
        '(�������)�X�֔ԍ�
        Me.tbxTyousakaisyaYuubinNo.Text = String.Empty
        '(�������)�Z��1
        Me.tbxTyousakaisyaJyuusyo1.Text = String.Empty
        '(�������)�d�b�ԍ�
        Me.tbxTyousakaisyaTelNo.Text = String.Empty
        '(�������)�Z��2
        Me.tbxTyousakaisyaJyuusyo2.Text = String.Empty
        '(�������)FAX�ԍ�
        Me.tbxTyousakaisyaFaxNo.Text = String.Empty
        '(�������)JAPAN��敪
        Me.tbxJapanKaiKbn.Text = String.Empty
        '(�������)JAPAN�����N��
        Me.tbxJapanKaiNyuukaiYM.Text = String.Empty
        '(�������)JAPAN��މ�N��
        Me.tbxJapanKaiTaikaiYM.Text = String.Empty
        '(�������)��n�n�Ւ�����C���i�L���t���O
        Me.tbxTyousaSyuninSikaku.Text = String.Empty
        '(�������)ReportJHS�g�[�N���L���t���O
        Me.tbxReportJHS.Text = String.Empty

    End Sub

    ''' <summary>
    ''' ������Џ����擾����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Private Sub btnTyousakaisya_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousakaisya.Click

        '������Џ����Z�b�g����
        Call Me.SetTyousaKaisyaInfo()

    End Sub

    ''' <summary>
    ''' ������Џ����Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Private Sub SetTyousaKaisyaInfo()

        '������ЃR�[�h
        Dim strTyousaKaisyaCd As String = Me.tbxTyousaKaisyaCd.Text.Trim()

        If Not strTyousaKaisyaCd.Equals(String.Empty) Then

            '������Џ����擾����
            Dim dtTyousaKaisyaInfo As New Data.DataTable
            dtTyousaKaisyaInfo = EigyousyoMasterBL.GetTyousaKaisyaInfo(strTyousaKaisyaCd)

            If dtTyousaKaisyaInfo.Rows.Count > 0 Then
                With dtTyousaKaisyaInfo.Rows(0)
                    '������ЃR�[�h
                    Me.tbxTyousaKaisyaCd.Text = .Item("tys_kaisya_cd").ToString.Trim() & .Item("jigyousyo_cd").ToString.Trim()
                    '������Ж�
                    Me.tbxTyousaKaisyaMei.Text = .Item("tys_kaisya_mei").ToString.Trim()
                    '������Ж��J�i
                    Me.tbxTyousaKaisyaMeiKana.Text = .Item("tys_kaisya_mei_kana").ToString.Trim()
                    '(�������)��\�Җ�
                    Me.tbxDaihyousyaMei.Text = .Item("daihyousya_mei").ToString.Trim()
                    '(�������)��E��
                    Me.tbxYasyokuMei.Text = .Item("yakusyoku_mei").ToString.Trim()
                    '(�������)�X�֔ԍ�
                    Me.tbxTyousakaisyaYuubinNo.Text = .Item("yuubin_no").ToString.Trim()
                    '(�������)�Z��1
                    Me.tbxTyousakaisyaJyuusyo1.Text = .Item("jyuusyo1").ToString.Trim()
                    '(�������)�d�b�ԍ�
                    Me.tbxTyousakaisyaTelNo.Text = .Item("tel_no").ToString.Trim()
                    '(�������)�Z��2
                    Me.tbxTyousakaisyaJyuusyo2.Text = .Item("jyuusyo2").ToString.Trim()
                    '(�������)FAX�ԍ�
                    Me.tbxTyousakaisyaFaxNo.Text = .Item("fax_no").ToString.Trim()
                    '(�������)JAPAN��敪
                    Select Case .Item("japan_kai_kbn").ToString.Trim()
                        Case "0"
                            Me.tbxJapanKaiKbn.Text = "������"
                        Case "1"
                            Me.tbxJapanKaiKbn.Text = "����"
                        Case "3"
                            Me.tbxJapanKaiKbn.Text = "�މ�"
                        Case Else
                            Me.tbxJapanKaiKbn.Text = "������"
                    End Select

                    '(�������)JAPAN�����N��
                    Me.tbxJapanKaiNyuukaiYM.Text = .Item("japan_kai_nyuukai_date").ToString.Trim()
                    '(�������)JAPAN��މ�N��
                    Me.tbxJapanKaiTaikaiYM.Text = .Item("japan_kai_taikai_date").ToString.Trim()
                    '(�������)��n�n�Ւ�����C���i�L���t���O
                    Select Case .Item("tkt_jbn_tys_syunin_skk_flg").ToString.Trim()
                        Case "0"
                            Me.tbxTyousaSyuninSikaku.Text = "����"
                        Case "1"
                            Me.tbxTyousaSyuninSikaku.Text = "�L��"
                        Case Else
                            Me.tbxTyousaSyuninSikaku.Text = "����"
                    End Select

                    '(�������)ReportJHS�g�[�N���L���t���O
                    Select Case .Item("report_jhs_token_flg").ToString.Trim()
                        Case "0"
                            Me.tbxReportJHS.Text = "����"
                        Case "1"
                            Me.tbxReportJHS.Text = "�L��"
                        Case Else
                            Me.tbxReportJHS.Text = "����"
                    End Select
                End With
            Else
                '������Џ����N���A����
                Call Me.ClearTyousakaisya()
            End If
        Else
            '������Џ����N���A����
            Call Me.ClearTyousakaisya()
        End If

    End Sub

    ''' <summary>
    ''' �Œ�`���[�W���Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Private Sub SetKoteiTyaaji()

        '�c�Ə��R�[�h
        Dim strEigyousyoCd As String = Me.tbxEigyousyoCd.Text.Trim

        If Left(strEigyousyoCd, 2).Equals("AF") AndAlso Me.ddlFcTenKbn.SelectedValue.Trim.Equals("1") Then
            '���͓����擾����
            Dim strNyuuryokuDate As String
            strNyuuryokuDate = EigyousyoMasterBL.GetKoteiTyaaji(strEigyousyoCd, True).Rows(0).Item("nyuuryoku_date").ToString.Trim

            If Not strNyuuryokuDate.Equals(String.Empty) Then
                '�{���Ő����ς�
                Me.lblKoteiTyaaji.Text = Left(strNyuuryokuDate, 4) & "�N" & Right(strNyuuryokuDate, 2) & "���@�����ς�"
                Me.lblKoteiTyaaji.ForeColor = Drawing.Color.Red

                '�u�Œ�`���[�W�v
                Me.btnKoteiTyaaji.Enabled = False

            Else
                strNyuuryokuDate = EigyousyoMasterBL.GetKoteiTyaaji(strEigyousyoCd, False).Rows(0).Item("nyuuryoku_date").ToString.Trim
                If Not strNyuuryokuDate.Equals(String.Empty) Then
                    '�{���Ŗ�����
                    Me.lblKoteiTyaaji.Text = Left(strNyuuryokuDate, 4) & "�N" & Right(strNyuuryokuDate, 2) & "���@�����ς�"
                    Me.lblKoteiTyaaji.ForeColor = Drawing.Color.Blue
                Else
                    '�ߋ��ɓo�^���Ȃ��ꍇ
                    Me.lblKoteiTyaaji.Text = "�Œ�`���[�W�@���������Ȃ�"
                    Me.lblKoteiTyaaji.ForeColor = Drawing.Color.Black
                End If

                '�����摶�݃`�F�b�N
                If EigyousyoMasterBL.SelSeikyuusakiCheck(strEigyousyoCd) Then
                    '�u�Œ�`���[�W�v
                    Me.btnKoteiTyaaji.Enabled = True
                Else
                    '�u�Œ�`���[�W�v
                    Me.btnKoteiTyaaji.Enabled = False
                End If

            End If
        Else
            '�Œ�`���[�W
            Me.btnKoteiTyaaji.Enabled = False
            Me.lblKoteiTyaaji.Text = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' �Œ�`���[�W�{�^��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Private Sub btnKoteiTyaaji_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKoteiTyaaji.Click

        '�c�Ə��R�[�h
        Dim strEigyousyoCd As String
        strEigyousyoCd = Me.tbxEigyousyoCd.Text.Trim

        '���b�Z�[�W
        Dim strMessage As String
        strMessage = EigyousyoMasterBL.SetKoteiTyaaji(strEigyousyoCd, CStr(ViewState("UserId")))

        '�Œ�`���[�W�����Z�b�g����
        If strMessage.Trim.Equals(String.Empty) Then
            '�����̏ꍇ

            '���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & String.Format(Messages.Instance.MSG2069E, "�Œ�`���[�W") & "');", True)

            '�Œ�`���[�W���Z�b�g����
            Call Me.SetKoteiTyaaji()
        Else
            '���s�̏ꍇ

            '���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strMessage & "');", True)
        End If

    End Sub

End Class