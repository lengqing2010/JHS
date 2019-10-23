Partial Public Class SearchSeikyuusyo
    Inherits System.Web.UI.Page

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '���ʃ��W�b�N
    Private cl As New CommonLogic
    '���b�Z�[�W�N���X
    Private MLogic As New MessageLogic
    '�����f�[�^���W�b�N�N���X
    Dim MyLogic As New SeikyuuDataSearchLogic
    '����������f�[�^���W�b�N�N���X
    Dim SubLogic As New SeikyuuMiinsatuDataSearchLogic

    '�����f�[�^����KEY���R�[�h�N���X
    Private keyRec As New SeikyuuDataKeyRecord
    '�����f�[�^���R�[�h�N���X
    Private dtRec As New SeikyuuDataRecord

#Region "�v���p�e�B"

#Region "�p�����[�^/�������f�[�^�쐬���"

    ''' <summary>
    ''' �N�����
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _GamenMode As String = String.Empty
    ''' <summary>
    ''' �N�����
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrGamenMode() As String
        Get
            Return _GamenMode
        End Get
        Set(ByVal value As String)
            _GamenMode = value
        End Set
    End Property

#End Region

#Region "�R���g���[���l"
    '�^�C�g��
    Private Const CTRL_VALUE_TITLE As String = "�������ꗗ"
    Private Const CTRL_VALUE_TITLE_KAKO As String = "�ߋ�" & CTRL_VALUE_TITLE
    '���������h���b�v�_�E�����X�g
    Private Const CTRL_VALUE_SEIKYUUSYOSIKI_DDL_ISNULL As String = "�����������ݒ�"

    '�������s���A��\���f�[�^�����݂���ꍇ���b�Z�[�W�G���A�Ɉȉ���\������
    Private Const ALERT_MSG_DISP_NONE As String = "���\��������Ă��Ȃ��f�[�^������܂�"

    Private Const CTRL_NAME_TR1 As String = "DataTable_resultTr1_"
    Private Const CTRL_NAME_TR2 As String = "DataTable_resultTr2_"
    Private Const CTRL_NAME_TD_SENTOU As String = "DataTable_Sentou_Td_"
    Private Const CTRL_NAME_HIDDEN_SEIKYUUSYO_NO As String = "HdnSeikyuusyoNo_"
    Private Const CTRL_NAME_HIDDEN_UPDDATETIME As String = "HdnUpdDatetime_"
    Private Const CTRL_NAME_HIDDEN_PRINT_TAISYOUGAI_FLG As String = "HdnPrintFlg_"
    Private Const CTRL_NAME_HIDDEN_TORIKESI_FLG As String = "HdnTorikesiFlg_"
    Private Const CTRL_NAME_HIDDEN_SYOSIKI_FLG As String = "HdnSyosikiFlg_"
    Private Const CTRL_NAME_HIDDEN_MEISAI_CNT As String = "HdnMeisaiCnt_"
    Private Const CTRL_NAME_CHECK_TAISYOU As String = "ChkTaisyou_"
#End Region

#Region "CSS�N���X��"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_KINGAKU = "kingaku"
    Private Const CSS_NUMBER = "number"
    Private Const CSS_DATE = "date"
#End Region

#End Region

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
            Exit Sub
        End If

        If IsPostBack = False Then
            Try
                ' ���n�p�����[�^��ێ�
                '���p�����[�^�̃`�F�b�N
                pStrGamenMode = Request("st")

                ' �p�����[�^�s�����͉�ʂ�\�����Ȃ�(��ʃ��[�h)
                If pStrGamenMode Is Nothing Then
                    Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
                    Exit Sub
                Else
                    Me.TextSekyuusyoHakDateTo.Value = Date.Now.ToString("yyyy/MM/dd")
                    'Hidden�Ɋi�[
                    Me.HiddenPrmSeikyuusyoHakDateTo.Value = Date.Now.ToString("yyyy/MM/dd")
                    Me.HiddenGamenMode.Value = pStrGamenMode
                End If
            Catch ex As Exception
                Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
                Exit Sub
            End Try

            '�������̃`�F�b�N
            '�o���Ɩ�����
            If userinfo.KeiriGyoumuKengen = 0 Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' ������敪�R���{�Ƀf�[�^���o�C���h����
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            ' ���������R���{�Ƀf�[�^���o�C���h����
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSyousiki, EarthConst.emKtMeisyouType.KAISYUU_SEIKYUUSYO_YOUSI, True, False)
            'IS NULL�����p��Add
            Me.SelectSeikyuuSyousiki.Items.Add(New ListItem(CTRL_VALUE_SEIKYUUSYOSIKI_DDL_ISNULL, EarthConst.ISNULL))

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()

            '��ʐݒ�
            Me.SetDispControl(sender, e)

            '�{�^�������C�x���g�̐ݒ�
            setBtnEvent()

            '�t�H�[�J�X�ݒ�
            Me.TextSekyuusyoHakDateFrom.Focus()

        Else
            '��ʐݒ�
            Me.SetDispControl(sender, e)

        End If

    End Sub

#Region "�{�^���C�x���g"
    ''' <summary>
    ''' �������s�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        setFocusAJ(Me.btnSearch) '�t�H�[�J�X

        Me.SetCmnSearchResult(sender, e)

    End Sub

    ''' <summary>
    ''' CSV�o�̓{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenCsv.ServerClick
        setFocusAJ(Me.ButtonCsv) '�t�H�[�J�X

        Me.HiddenCsvOutPut.Value = String.Empty '�t���O���N���A
        Me.HiddenCsvMaxCnt.Value = String.Empty

        '���R�[�h�N���X
        Dim dtCsv As New DataTable
        Dim strFileNm As String = String.Empty

        '�����������̎擾
        Me.SetSearchKeyFromCtrl(keyRec)

        '����
        Dim total_count As Integer = 0

        '��ʃ��[�h�ʏ���
        If Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '�������ꗗ���
            '�������s
            dtCsv = MyLogic.GetSeikyuusyoDataCsv(sender, keyRec, total_count, EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo)
            strFileNm = EarthConst.FILE_NAME_CSV_SEIKYUUSYO_DATA

        ElseIf Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then '�ߋ��������ꗗ���
            '�������s
            dtCsv = MyLogic.GetSeikyuusyoDataCsv(sender, keyRec, total_count, EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo)
            strFileNm = EarthConst.FILE_NAME_CSV_KAKO_SEIKYUUSYO_DATA
        End If

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
            Exit Sub
        ElseIf total_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV�o��"), 0, "kensakuErr")
            Exit Sub
        End If

        '�o�͗p�f�[�^�e�[�u������ɁACSV�o�͂��s�Ȃ�
        If cl.OutPutFileFromDtTable(strFileNm, dtCsv) = False Then
            ' �o�͗p�����񂪂Ȃ��̂ŁA�����I��
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV�o��"), 0, "OutputErr")
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' �߂�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonReturn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReturn.ServerClick
        Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
    End Sub

    ''' <summary>
    ''' ����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuusyoPrint_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuusyoPrint.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonSeikyuusyoPrint

        '����{�^���������ADB�X�V���s�Ȃ�
        If objActBtn.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT Then

            ' ���̓`�F�b�N
            If Me.checkInput(objActBtn) = False Then Exit Sub

            tmpScript = "gNoDataMsgFlg = '1';"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenPrint_FlgSet", tmpScript, True)

            ' ��ʂ̓��e��DB�ɔ��f����
            If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoPrint) Then '�o�^����

                Me.SetCmnSearchResult(sender, e)

            Else '�o�^���s
                Me.SetCmnSearchResult(sender, e)

                setFocusAJ(Me.btnSearch) '�t�H�[�J�X

                tmpScript = "alert('" & Messages.MSG147E.Replace("@PARAM1", objActBtn.Value) & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenPrint_ServerClick", tmpScript, True)
                Exit Sub
            End If

        ElseIf objActBtn.Value = EarthConst.BUTTON_SEIKYUUSYO_RE_PRINT Then '�Ĉ���{�^��������

            Me.SetCmnSearchResult(sender, e)

        End If

        'PDF�o�͏���
        tmpScript = "gVarPdfFlg = '1';"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenPrint_ServerClick2", tmpScript, True)

    End Sub

    ''' <summary>
    ''' ����������{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuusyoTorikesi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuusyoTorikesi.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonSeikyuusyoTorikesi

        ' ���̓`�F�b�N
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoTorikesi) Then '�o�^����

            tmpScript = "gNoDataMsgFlg = '1';"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenTorikesi_ServerClick", tmpScript, True)

            Me.SetCmnSearchResult(sender, e)
        Else '�o�^���s
            Me.SetCmnSearchResult(sender, e)

            setFocusAJ(Me.btnSearch) '�t�H�[�J�X

            tmpScript = "alert('" & Messages.MSG147E.Replace("@PARAM1", "���������") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenTorikesi_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' �����挟���{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiSearch.ServerClick
        Dim blnResult As Boolean

        '�����挟����ʌďo
        blnResult = cl.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                        , Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                        , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)
        If blnResult Then
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.btnSeikyuuSakiSearch)
        End If

    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        Dim strJs As String

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �{�^���̃C�x���g�n���h����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�������s�{�^��
        Me.btnSearch.Attributes("onclick") = "checkJikkou('0');"
        'CSV�o�̓{�^��
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou('1');"

        '������ꗗ�{�^��
        strJs = "window.open('" & UrlConst.POPUP_SEIKYUUSYO_MIINSATU & _
        "','this','menubar=no,toolbar=no,location=no,status=no,resizable=yes,scrollbars=yes')"
        Me.ButtonMiinsatu.Attributes.Add("onclick", strJs)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�}�X�^�����n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrName(this,'" & Me.TextSeikyuuSakiMei.ClientID & "');"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrName(this,'" & Me.TextSeikyuuSakiMei.ClientID & "');"
        Me.SelectSeikyuuSakiKbn.Attributes("onblur") = "clrName(this,'" & Me.TextSeikyuuSakiMei.ClientID & "');"

    End Sub

    ''' <summary>
    ''' �e��{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Sub setBtnEvent()
        Dim strChkTaisyou As String = "ChkTaisyou(@PARAM1)"
        Dim strChkJikkou As String = "checkJikkou(@PARAM1)"

        '�C�x���g�n���h���o�^
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(" & strChkTaisyou & " && " & strChkJikkou & "){" & tmpTouroku & "}else{return false;}"

        '�m�FMSG��AOK�̏ꍇ�㑱�������s�Ȃ�
        Me.ButtonSeikyuusyoPrint.Attributes("onclick") = tmpScript.Replace("@PARAM1", EarthEnum.emSearchSeikyuusyoBtnType.Print)
        Me.ButtonSeikyuusyoTorikesi.Attributes("onclick") = tmpScript.Replace("@PARAM1", EarthEnum.emSearchSeikyuusyoBtnType.Torikesi)

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
    ''' ��ʍ��ڂ��猟���L�[���R�[�h�ւ̒l�Z�b�g���s���B
    ''' </summary>
    ''' <param name="recKey">���o���R�[�h�N���X�̃L�[</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As SeikyuuDataKeyRecord)

        '������NO�Q
        recKey.ArrSeikyuuSakiNo = IIf(Me.HiddenSendValSeikyuusyoNo.Value <> String.Empty, Me.HiddenSendValSeikyuusyoNo.Value, String.Empty)

        '���������s�� From
        recKey.SeikyuusyoHakDateFrom = IIf(Me.TextSekyuusyoHakDateFrom.Value <> String.Empty, Me.TextSekyuusyoHakDateFrom.Value, DateTime.MinValue)
        '���������s�� To
        recKey.SeikyuusyoHakDateTo = IIf(Me.TextSekyuusyoHakDateTo.Value <> String.Empty, Me.TextSekyuusyoHakDateTo.Value, DateTime.MinValue)
        '������敪
        recKey.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuSakiKbn.SelectedValue <> String.Empty, Me.SelectSeikyuuSakiKbn.SelectedValue, String.Empty)
        '������R�[�h
        recKey.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> String.Empty, Me.TextSeikyuuSakiCd.Value, String.Empty)
        '������}��
        recKey.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> String.Empty, Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '������J�i��
        recKey.SeikyuuSakiMeiKana = IIf(Me.TextSeikyuuSakiMeiKana.Value <> String.Empty, Me.TextSeikyuuSakiMeiKana.Value, String.Empty)
        '���
        recKey.Torikesi = IIf(Me.CheckTorikesiTaisyou.Checked, 0, Integer.MinValue)

        '��ʕʏ���
        If Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '�������ꗗ���
            '��������
            recKey.SeikyuuSimeDate = IIf(Me.TextSeikyuuSimeDate.Value <> String.Empty, Me.TextSeikyuuSimeDate.Value, String.Empty)
            '��������
            recKey.SeikyuuSyosiki = IIf(Me.SelectSeikyuuSyousiki.SelectedValue <> String.Empty, Me.SelectSeikyuuSyousiki.SelectedValue, String.Empty)
            '���׌��� FROM
            recKey.MeisaiKensuuFrom = IIf(Me.TextMeisaiKensuuFrom.Value <> String.Empty, Me.TextMeisaiKensuuFrom.Value, Integer.MinValue)
            '���׌��� TO
            recKey.MeisaiKensuuTo = IIf(Me.TextMeisaiKensuuTo.Value <> String.Empty, Me.TextMeisaiKensuuTo.Value, Integer.MinValue)
            '�󎚗p��
            recKey.InjiYousi = Integer.MinValue

        ElseIf Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then '�ߋ��������ꗗ���
            '��������
            recKey.SeikyuuSimeDate = IIf(Me.TextSeikyuuSimeDate.Value <> String.Empty, Me.TextSeikyuuSimeDate.Value, String.Empty)
            '��������
            recKey.SeikyuuSyosiki = IIf(Me.SelectSeikyuuSyousiki.SelectedValue <> String.Empty, Me.SelectSeikyuuSyousiki.SelectedValue, String.Empty)
            '���׌��� FROM
            recKey.MeisaiKensuuFrom = Integer.MinValue
            '���׌��� TO
            recKey.MeisaiKensuuTo = Integer.MinValue
            '�󎚗p��
            recKey.InjiYousi = IIf(Me.CheckInjiTaisyou.Checked, 0, Integer.MinValue)
        End If

    End Sub

    ''' <summary>
    ''' ���������Ō����������e���������ʃe�[�u���ɕ\������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '�\���ő匏��
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0 '�擾����
        Dim intMihakkou As Integer = 0 '�����s����
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        '�������s
        Dim resultArray As New List(Of SeikyuuDataRecord)

        '�Ώۃ`�F�b�N�{�b�N�X�̏�����(CSV�o�͎��ȊO)
        If Me.HiddenCsvOutPut.Value <> "1" Then
            Me.CheckAll.Checked = False
        End If

        '��ʃ��[�h�ʏ���
        If Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '�������ꗗ���
            resultArray = MyLogic.GetSeikyuuDataInfo(sender, keyRec, 1, end_count, total_count, EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo)

            '�����s������ݒ�
            MyLogic.GetMihakkouCnt(sender, Me.TdMihakkou.InnerHtml)

            '�X�^�C���ݒ�
            If Me.TrMihakkou.Style("display") = "inline" Then
                '�X�^�C���ݒ�
                If Me.TdMihakkou.InnerHtml = String.Empty OrElse Me.TdMihakkou.InnerHtml = "0" Then
                    Me.TdMihakkou.Style("color") = ""
                    Me.TdMihakkou.Style("font-weight") = ""
                Else
                    Me.TdMihakkou.Style("color") = "red"
                    Me.TdMihakkou.Style("font-weight") = "bold"
                End If
            End If

        ElseIf Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then '�ߋ��������ꗗ���
            resultArray = MyLogic.GetSeikyuuDataInfo(sender, keyRec, 1, end_count, total_count, EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo)
        End If

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
            Me.HiddenCsvOutPut.Value = ""
        ElseIf total_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        '�\��������������
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")

        If end_count < total_count Then
            resultCount.Style("color") = "red"
            displayCount = CommonLogic.Instance.GetDisplayString(end_count) & " / " & CommonLogic.Instance.GetDisplayString(total_count)
            'MSG�G���A�̕\���ؑ�
            Me.TdMsgArea.InnerHtml = ALERT_MSG_DISP_NONE
        Else
            'MSG�G���A�̕\���ؑ�
            Me.TdMsgArea.InnerHtml = String.Empty
        End If

        '�������ʌ�����ݒ�
        resultCount.InnerHtml = displayCount

        '************************
        '* ��ʃe�[�u���֏o��
        '************************
        '�s�J�E���^
        Dim rowCnt As Integer = 0
        Dim strSpace As String = EarthConst.HANKAKU_SPACE
        Dim intTorikesi As Integer = 0

        '************
        '* �Z�����擾
        '************
        '�e�Z���̕��ݒ�p�̃��X�g�쐬�i�^�C�g���s�̕����x�[�X�ɂ���j
        Dim widthList1 As New List(Of String)
        Dim tableWidth1 As Integer = 0

        For Each cell As HtmlTableCell In TableTitleTable1.Rows(0).Cells
            Dim tmpWidth = cell.Style("width")
            widthList1.Add(tmpWidth)
            tableWidth1 += Integer.Parse(tmpWidth.Replace("px", "")) + 3
        Next
        TableTitleTable1.Style("width") = tableWidth1 & "px"
        TableDataTable1.Style("width") = tableWidth1 & "px"

        Dim widthList2 As New List(Of String)
        Dim tableWidth2 As Integer = 0

        For Each cell As HtmlTableCell In TableTitleTable2.Rows(0).Cells
            Dim tmpWidth = cell.Style("width")
            widthList2.Add(tmpWidth)
            tableWidth2 += Integer.Parse(tmpWidth.Replace("px", "")) + 3
        Next
        TableTitleTable2.Style("width") = tableWidth2 & "px"
        TableDataTable2.Style("width") = tableWidth2 & "px"

        '************
        '* �ϐ��錾
        '************
        'TabIndex
        Dim intTrTabIndex As Integer = 5
        'Tr
        Dim objTr1 As HtmlTableRow
        Dim objTr2 As HtmlTableRow

        Dim strSeikyuusyoNo As String                   '������NO

        'Hidden 
        Dim objHdnSeikyuusyoNo As HtmlInputHidden       '������NO
        Dim objHdnUpdDatetime As HtmlInputHidden        '�X�V����
        Dim objHdnPrintTaisyougai As HtmlInputHidden    '����o�͑ΏۊO�t���O
        Dim objHdnTorikesi As HtmlInputHidden           '����t���O
        Dim objHdnSyosiki As HtmlInputHidden            '���������t���O
        Dim objHdnMeisaiCnt As HtmlInputHidden          '�������׌���
        'CheckBox 
        Dim objChkTaisyou As HtmlInputCheckBox          '�Ώ�
        'Table Cell
        Dim objTdTaisyou As HtmlTableCell               '�Ώ�
        Dim objTdSeikyuusyoNo As HtmlTableCell          '������NO
        Dim objTdTorikesi As HtmlTableCell              '���
        Dim objTdSeikyuuSakiCd As HtmlTableCell         '������R�[�h
        Dim objTdSeikyuuSakiMei As HtmlTableCell        '�����於
        Dim objTdSeikyuuSakiMei2 As HtmlTableCell       '�����於2

        Dim objTdSeikyuusyoHakDate As HtmlTableCell     '���������s��
        Dim objTdSeikyuuSimeDate As HtmlTableCell       '�������ߓ�
        Dim objTdKaisyuuYoteiDate As HtmlTableCell      '����\���

        Dim objTdZenkaiGoseikyuuGaku As HtmlTableCell   '�O��䐿�����z
        Dim objTdGonyuukinGaku As HtmlTableCell         '������z
        Dim objTdZenkaiKurikosiZandaka As HtmlTableCell '�O��J�z�c��
        Dim objTdKonkaiGoseikyuuGaku As HtmlTableCell   '����䐿���z
        Dim objTdKurikosiZandaka As HtmlTableCell       '�J�z�c��

        Dim objTdYuubinNo As HtmlTableCell              '�X�֔ԍ�
        Dim objTdTelNo As HtmlTableCell                 '�d�b�ԍ�
        Dim objTdJuusyo1 As HtmlTableCell               '�Z��1
        Dim objTdJuusyo2 As HtmlTableCell               '�Z��2

        '�擾�����f�[�^����ʂɕ\��
        For Each data As SeikyuuDataRecord In resultArray
            rowCnt += 1

            'Tr
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            strSeikyuusyoNo = String.Empty

            '***********
            '* Table1
            '********
            'Hidden 
            objHdnSeikyuusyoNo = New HtmlInputHidden       'Hidden������NO
            objHdnUpdDatetime = New HtmlInputHidden        'Hidden�X�V����
            objHdnPrintTaisyougai = New HtmlInputHidden    'Hidden����o�͑ΏۊO�t���O
            objHdnTorikesi = New HtmlInputHidden           'Hidden����t���O
            objHdnSyosiki = New HtmlInputHidden
            objHdnMeisaiCnt = New HtmlInputHidden
            'CheckBox 
            objChkTaisyou = New HtmlInputCheckBox          '�Ώۃ`�F�b�N�{�b�N�X
            'Table Cell
            objTdTaisyou = New HtmlTableCell               '�Ώ�
            objTdSeikyuusyoNo = New HtmlTableCell          '������NO
            objTdTorikesi = New HtmlTableCell              '���
            objTdSeikyuuSakiCd = New HtmlTableCell         '������R�[�h
            objTdSeikyuuSakiMei = New HtmlTableCell        '�����於
            objTdSeikyuuSakiMei2 = New HtmlTableCell       '�����於2

            '***********
            '* Table2
            '********
            objTdSeikyuusyoHakDate = New HtmlTableCell     '���������s��
            objTdSeikyuuSimeDate = New HtmlTableCell       '�������ߓ�
            objTdKaisyuuYoteiDate = New HtmlTableCell      '����\���

            objTdZenkaiGoseikyuuGaku = New HtmlTableCell   '�O��䐿���z
            objTdGonyuukinGaku = New HtmlTableCell         '������z
            objTdZenkaiKurikosiZandaka = New HtmlTableCell '�O��J�z�c��
            objTdKonkaiGoseikyuuGaku = New HtmlTableCell   '����䐿���z
            objTdKurikosiZandaka = New HtmlTableCell       '�J�z�c��

            objTdYuubinNo = New HtmlTableCell              '�X�֔ԍ�
            objTdTelNo = New HtmlTableCell                 '�d�b�ԍ�
            objTdJuusyo1 = New HtmlTableCell               '�Z��1
            objTdJuusyo2 = New HtmlTableCell               '�Z��2

            '�������ʔz�񂩂�Z���Ɋi�[
            objTdTaisyou.ID = CTRL_NAME_TD_SENTOU & rowCnt

            '������NO���擾
            strSeikyuusyoNo = cl.GetDisplayString(data.SeikyuusyoNo)
            '������擾
            intTorikesi = IIf(data.Torikesi = 0, data.Torikesi, 1)

            '******************
            '* �擪��
            '*************
            'Hidden������NO
            objHdnSeikyuusyoNo.ID = CTRL_NAME_HIDDEN_SEIKYUUSYO_NO & rowCnt
            objHdnSeikyuusyoNo.Value = strSeikyuusyoNo
            objTdTaisyou.Controls.Add(objHdnSeikyuusyoNo)

            'Hidden�X�V����
            objHdnUpdDatetime.ID = CTRL_NAME_HIDDEN_UPDDATETIME & rowCnt
            objHdnUpdDatetime.Value = IIf(data.UpdDatetime = DateTime.MinValue, Format(data.AddDatetime, EarthConst.FORMAT_DATE_TIME_1), Format(data.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
            objTdTaisyou.Controls.Add(objHdnUpdDatetime)

            'HiddenCSV�o�͑ΏۊO�t���O
            objHdnPrintTaisyougai.ID = CTRL_NAME_HIDDEN_PRINT_TAISYOUGAI_FLG & rowCnt
            objHdnPrintTaisyougai.Value = IIf(data.PrintTaisyougaiFlg = 1, data.PrintTaisyougaiFlg, 0)
            objTdTaisyou.Controls.Add(objHdnPrintTaisyougai)

            'Hidden����t���O
            objHdnTorikesi.ID = CTRL_NAME_HIDDEN_TORIKESI_FLG & rowCnt
            objHdnTorikesi.Value = intTorikesi.ToString
            objTdTaisyou.Controls.Add(objHdnTorikesi)

            'Hidden�����t���O
            objHdnSyosiki.ID = CTRL_NAME_HIDDEN_SYOSIKI_FLG & rowCnt
            objHdnSyosiki.Value = cl.GetDisplayString(data.KaisyuuSeikyuusyoYousi, EarthConst.ISNULL)
            objTdTaisyou.Controls.Add(objHdnSyosiki)

            'Hidden���׌���
            objHdnMeisaiCnt.ID = CTRL_NAME_HIDDEN_MEISAI_CNT & rowCnt
            objHdnMeisaiCnt.Value = cl.GetDisplayString(data.MeisaiKensuu, 0)
            objTdTaisyou.Controls.Add(objHdnMeisaiCnt)

            '�Ώۃ`�F�b�N�{�b�N�X(�߂�v�f(hidden)�́A�K���擪��̐擪��Input�^�O�ɃZ�b�g���邱��)
            objChkTaisyou.ID = CTRL_NAME_CHECK_TAISYOU & strSeikyuusyoNo
            objChkTaisyou.Attributes("tabindex") = Integer.Parse(Me.CheckTorikesiTaisyou.Attributes("tabindex")) + intTrTabIndex + rowCnt
            objTdTaisyou.Controls.Add(objChkTaisyou)
            '******************

            objTdSeikyuusyoNo.InnerHtml = cl.GetDisplayString(strSeikyuusyoNo, strSpace)
            If intTorikesi = 0 Then
                objTdTorikesi.InnerHtml = strSpace
            ElseIf intTorikesi = 1 Then
                objTdTorikesi.InnerHtml = EarthConst.TORIKESI
            End If
            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(data.SeikyuuSakiKbn, data.SeikyuuSakiCd, data.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei, strSpace)
            objTdSeikyuuSakiMei2.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei2, strSpace)

            objTdSeikyuusyoHakDate.InnerHtml = cl.GetDisplayString(data.SeikyuusyoHakDate, strSpace)
            '��ToolTip�ݒ�
            cl.setSeikyuusyoToolTip(data, objTdSeikyuusyoHakDate, CommonLogic.emToolTipSetType.SeikyuusyoHakDate)
            objTdSeikyuuSimeDate.InnerHtml = cl.GetDisplayString(data.SeikyuuSimeDateMst, strSpace)
            '��ToolTip�ݒ�
            cl.setSeikyuusyoToolTip(data, objTdSeikyuuSimeDate, CommonLogic.emToolTipSetType.SeikyuuSimeDate)
            objTdKaisyuuYoteiDate.InnerHtml = cl.GetDisplayString(data.KonkaiKaisyuuYoteiDate, strSpace)

            objTdZenkaiGoseikyuuGaku.InnerHtml = cl.GetDisplayString(Format(data.ZenkaiGoseikyuuGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdGonyuukinGaku.InnerHtml = cl.GetDisplayString(Format(data.GonyuukinGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdZenkaiKurikosiZandaka.InnerHtml = cl.GetDisplayString(Format(data.KurikosiGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdKonkaiGoseikyuuGaku.InnerHtml = cl.GetDisplayString(Format(data.KonkaiGoseikyuuGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdKurikosiZandaka.InnerHtml = cl.GetDisplayString(Format(data.KonkaiKurikosiGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)

            objTdYuubinNo.InnerHtml = cl.GetDisplayString(data.YuubinNo, strSpace)
            objTdTelNo.InnerHtml = cl.GetDisplayString(data.TelNo, strSpace)
            objTdJuusyo1.InnerHtml = cl.GetDisplayString(data.Jyuusyo1, strSpace)
            objTdJuusyo2.InnerHtml = cl.GetDisplayString(data.Jyuusyo2, strSpace)

            '�e�Z���̕��ݒ�
            If rowCnt = 1 Then
                objTdTaisyou.Style("width") = widthList1(0)
                objTdSeikyuusyoNo.Style("width") = widthList1(1)
                objTdTorikesi.Style("width") = widthList1(2)
                objTdSeikyuuSakiCd.Style("width") = widthList1(3)
                objTdSeikyuuSakiMei.Style("width") = widthList1(4)
                objTdSeikyuuSakiMei2.Style("width") = widthList1(5)

                objTdSeikyuusyoHakDate.Style("width") = widthList2(0)
                objTdSeikyuuSimeDate.Style("width") = widthList2(1)
                objTdKaisyuuYoteiDate.Style("width") = widthList2(2)

                objTdZenkaiGoseikyuuGaku.Style("width") = widthList2(3)
                objTdGonyuukinGaku.Style("width") = widthList2(4)
                objTdZenkaiKurikosiZandaka.Style("width") = widthList2(5)
                objTdKonkaiGoseikyuuGaku.Style("width") = widthList2(6)
                objTdKurikosiZandaka.Style("width") = widthList2(7)

                objTdYuubinNo.Style("width") = widthList2(8)
                objTdTelNo.Style("width") = widthList2(9)
                objTdJuusyo1.Style("width") = widthList2(10)
                objTdJuusyo2.Style("width") = widthList2(11)
            End If

            '�X�^�C���A�N���X�ݒ�
            objTdTaisyou.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuusyoNo.Attributes("class") = CSS_TEXT_CENTER
            objTdTorikesi.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSakiCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSakiMei.Attributes("class") = ""
            objTdSeikyuuSakiMei2.Attributes("class") = ""

            objTdSeikyuusyoHakDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdSeikyuuSimeDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdKaisyuuYoteiDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER

            objTdZenkaiGoseikyuuGaku.Attributes("class") = CSS_KINGAKU
            objTdGonyuukinGaku.Attributes("class") = CSS_KINGAKU
            objTdZenkaiKurikosiZandaka.Attributes("class") = CSS_KINGAKU
            objTdKonkaiGoseikyuuGaku.Attributes("class") = CSS_KINGAKU
            objTdKurikosiZandaka.Attributes("class") = CSS_KINGAKU

            objTdYuubinNo.Attributes("class") = CSS_TEXT_CENTER
            objTdTelNo.Attributes("class") = CSS_TEXT_CENTER
            objTdJuusyo1.Attributes("class") = ""
            objTdJuusyo2.Attributes("class") = ""

            '�sID��JS�C�x���g�̕t�^
            objTr1.ID = CTRL_NAME_TR1 & rowCnt
            objTr1.Attributes("tabindex") = -1

            '�s�ɃZ�����Z�b�g1
            With objTr1.Controls
                .Add(objTdTaisyou) '�߂�v�f(hidden)�́A�K���擪��̐擪��Input�^�O�ɃZ�b�g���邱��
                .Add(objTdSeikyuusyoNo)
                .Add(objTdTorikesi)
                .Add(objTdSeikyuuSakiCd)
                .Add(objTdSeikyuuSakiMei)
                .Add(objTdSeikyuuSakiMei2)
            End With

            '�sID��JS�C�x���g�̕t�^
            objTr2.ID = CTRL_NAME_TR2 & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(Me.CheckTorikesiTaisyou.Attributes("tabindex")) + intTrTabIndex
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2�s�ڈȍ~�̓^�u�ړ��Ȃ�
                objTr2.Attributes("tabindex") = -1
            End If

            '�s�ɃZ���ƃZ�b�g2
            With objTr2.Controls
                .Add(objTdSeikyuusyoHakDate)
                .Add(objTdSeikyuuSimeDate)
                .Add(objTdKaisyuuYoteiDate)

                .Add(objTdZenkaiGoseikyuuGaku)
                .Add(objTdGonyuukinGaku)
                .Add(objTdZenkaiKurikosiZandaka)
                .Add(objTdKonkaiGoseikyuuGaku)
                .Add(objTdKurikosiZandaka)

                .Add(objTdYuubinNo)
                .Add(objTdTelNo)
                .Add(objTdJuusyo1)
                .Add(objTdJuusyo2)
            End With

            '�e�[�u���ɍs���Z�b�g
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next



    End Sub

    ''' <summary>
    ''' ��ʃ��[�h�ʂ̉�ʐݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispControl(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Hidden���Đݒ�
        pStrGamenMode = Me.HiddenGamenMode.Value

        Dim end_count As Integer = 10 '�\���ő匏��
        Dim total_count As Integer = 0 '�擾����

        '������f�[�^������
        Dim resultArray As New List(Of SeikyuuDataRecord)
        resultArray = SubLogic.GetSeikyuuMiinsatuData(sender, dtRec, 1, end_count, total_count)

        If pStrGamenMode = CStr(EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo) Then '�������ꗗ���
            '�^�C�g��
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE
            '�E�B���h�E�^�C�g���o�[
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE
            '���
            Me.ButtonSeikyuusyoPrint.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT
            '��ʕʕ\���ؑ֍s
            Me.TrMihakkou.Style("display") = "inline"
            Me.TrSearchArea.Style("display") = "inline"
            Me.SpanInjiYousi.Style("display") = "none"

            '������ꗗ(�Y���f�[�^�����݂���ꍇ�̂݃{�^���\��)
            If total_count = 0 Then
                Me.ButtonMiinsatu.Style("display") = "none"
            ElseIf total_count >= 1 Then
                Me.ButtonMiinsatu.Style("display") = "inline"
            ElseIf total_count = -1 Then
                ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
                Exit Sub
            End If

        ElseIf pStrGamenMode = CStr(EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo) Then '�ߋ��������ꗗ���
            '�^�C�g��
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE_KAKO
            '�E�B���h�E�^�C�g���o�[
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE_KAKO
            '�Ĉ��
            Me.ButtonSeikyuusyoPrint.Value = EarthConst.BUTTON_SEIKYUUSYO_RE_PRINT
            '������ꗗ
            Me.ButtonMiinsatu.Style("display") = "none"
            '��ʕʕ\���ؑ֍s
            Me.TrMihakkou.Style("display") = "none"
            Me.TdSelectSeikyuuSyousiki.ColSpan = "3"
            Me.KoumokumeiSearchMeisaiKensuu.Style("display") = "none"
            Me.TdTextMeisaiKensuu.Style("display") = "none"
            Me.SpanInjiYousi.Style("display") = "inline"
        Else
            Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
            Exit Sub
        End If

        Me.TextSeikyuuSakiCd.Attributes("class") = "codeNumber"

    End Sub

    ''' <summary>
    ''' �������s������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCmnSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '����������ݒ�
        Me.SetSearchKeyFromCtrl(keyRec)

        '�������ʂ���ʂɕ\��
        Me.SetSearchResult(sender, e)

    End Sub

#Region "DB�X�V�����n"

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
    Public Function checkInput(ByVal prmActBtn As HtmlInputButton) As Boolean
        '�G���[���b�Z�[�W������
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        Dim tmpCtrl As HtmlControl
        If prmActBtn.ID = Me.ButtonSeikyuusyoTorikesi.ID Then
            tmpCtrl = Me.ButtonSeikyuusyoTorikesi
        ElseIf prmActBtn.ID = Me.ButtonSeikyuusyoPrint.ID Then
            tmpCtrl = Me.ButtonSeikyuusyoPrint
        Else
            Return False
        End If

        '�Ώۃ`�F�b�N�{�b�N�X�ɂăf�[�^���Z�b�g����Ă��邩���`�F�b�N
        If Me.HiddenSendValSeikyuusyoNo.Value = String.Empty _
            Or Me.HiddenSendValUpdDatetime.Value = String.Empty Then
            errMess += Messages.MSG140E
            arrFocusTargetCtrl.Add(tmpCtrl)
        End If

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If errMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            MLogic.AlertMessage(Me, errMess)
            Return False
        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <param name="emType">�����f�[�^�̍X�V�^�C�v</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function SaveData(ByVal emType As EarthEnum.emSeikyuusyoUpdType) As Boolean
        Dim listRec As New List(Of SeikyuuDataRecord)

        '��ʂ��烌�R�[�h�N���X�ɃZ�b�g
        listRec = Me.GetCtrlToDataRec()

        ' �f�[�^�̍X�V���s���܂�
        If MyLogic.saveData(Me, listRec, emType) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̊e�������R�[�h�N���X�Ɏ擾���ADB�X�V�p���R�[�h�N���X��ԋp����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataRec() As List(Of SeikyuuDataRecord)
        Dim listRec As New List(Of SeikyuuDataRecord)
        Dim dtRec As New SeikyuuDataRecord
        Dim intCnt As Integer = 0
        Dim arrKeySeikyuusyoNo() As String = Nothing
        Dim arrKeyUpdDatetime() As String = Nothing

        If Me.HiddenSendValSeikyuusyoNo.Value <> String.Empty Then
            arrKeySeikyuusyoNo = Split(Me.HiddenSendValSeikyuusyoNo.Value, EarthConst.SEP_STRING)
        End If

        If Me.HiddenSendValUpdDatetime.Value <> String.Empty Then
            arrKeyUpdDatetime = Split(Me.HiddenSendValUpdDatetime.Value, EarthConst.SEP_STRING)
        End If

        '������NO�ƍX�V�����̌�������̏ꍇ
        If arrKeySeikyuusyoNo.Length = arrKeyUpdDatetime.Length Then

            If Not arrKeySeikyuusyoNo Is Nothing And Not arrKeyUpdDatetime Is Nothing Then

                For intCnt = 0 To arrKeySeikyuusyoNo.Length - 1
                    '������NO���邢�͍X�V�������󔒂̏ꍇ�A���̏�����
                    If arrKeySeikyuusyoNo(intCnt) = String.Empty _
                        OrElse arrKeyUpdDatetime(intCnt) = String.Empty Then
                        Continue For
                    End If

                    '***************************************
                    ' �����f�[�^
                    '***************************************
                    dtRec = New SeikyuuDataRecord

                    ' ������NO
                    cl.SetDisplayString(arrKeySeikyuusyoNo(intCnt), dtRec.SeikyuusyoNo)
                    ' ���
                    dtRec.Torikesi = 1
                    ' �����������
                    dtRec.SeikyuusyoInsatuDate = DateTime.Now
                    ' �X�V�҃��[�U�[ID
                    dtRec.UpdLoginUserId = userinfo.LoginUserId
                    ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
                    If arrKeyUpdDatetime(intCnt) = "" Then
                        dtRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                    Else
                        dtRec.UpdDatetime = DateTime.ParseExact(arrKeyUpdDatetime(intCnt), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                    End If

                    listRec.Add(dtRec)
                Next
            End If
        End If

        Return listRec
    End Function

#End Region

#End Region

End Class