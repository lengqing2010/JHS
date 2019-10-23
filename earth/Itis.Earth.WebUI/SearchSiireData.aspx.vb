
Partial Public Class SearchSiireData
    Inherits System.Web.UI.Page

    '���ʃ��W�b�N
    Private CLogic As CommonLogic = CommonLogic.Instance
    '���b�Z�[�W�N���X
    Private MLogic As New MessageLogic
    '���O�C�����[�U���R�[�h
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '����f�[�^�e�[�u�����R�[�h�N���X
    Private rec As New SiireDataKeyRecord

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

            '�������̃`�F�b�N
            '�o���Ɩ�����
            If userinfo.KeiriGyoumuKengen = 0 Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            ' �敪�R���{�Ƀf�[�^���o�C���h����
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            helper.SetDropDownList(selectKbn, DropDownHelper.DropDownType.Kubun)


            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()

            '�t�H�[�J�X�ݒ�
            kubun_all.Focus()

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
        '�����{�^���Ƀt�H�[�J�X
        Me.btnSearch.Focus()

        '����������ݒ�
        SetSearchKeyFromCtrl(rec)

        '�������ʂ���ʂɕ\��
        SetSearchResult(sender, e)

    End Sub

    ''' <summary>
    ''' ���i�����{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSyouhinSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyouhinSearch.ServerClick

        Dim lgcSyouhinSearch As New UriageDataSearchLogic
        Dim dataArray As New List(Of SyouhinMeisaiRecord)
        Dim total_count As Integer = 0
        Dim tmpScript As String = String.Empty
        Dim strSyouhinCd As String = String.Empty

        '��ʂ���R�[�h���擾
        strSyouhinCd = IIf(Me.TextSyouhinCd.Value <> "", Me.TextSyouhinCd.Value, String.Empty)

        '���i���������s
        If Not String.IsNullOrEmpty(strSyouhinCd) Then
            dataArray = lgcSyouhinSearch.GetSyouhinInfo(strSyouhinCd, String.Empty, total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As SyouhinMeisaiRecord = dataArray(0)
            Me.TextSyouhinCd.Value = recData.SyouhinCd
            Me.TextHinmei.Value = recData.SyouhinMei
            '�t�H�[�J�X�Z�b�g
            masterAjaxSM.SetFocus(Me.btnSyouhinSearch)
        Else
            Dim tmpFocusScript = "objEBI('" & btnSyouhinSearch.ClientID & "').focus();"
            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.TextSyouhinCd.ClientID & EarthConst.SEP_STRING & Me.hdnSyouhinType.ClientID & "','" _
                                       & UrlConst.SEARCH_SYOUHIN & "','" _
                                       & Me.TextSyouhinCd.ClientID & "','" _
                                       & Me.btnSyouhinSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �d���挟���{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSiireSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSiireSakiSearch.ServerClick
        Dim lgcSiireSearch As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)
        Dim total_count As Integer = 0
        Dim tmpScript As String = String.Empty
        Dim strSiireCd As String = String.Empty
        Dim strSiireBrc As String = String.Empty

        '��ʂ���R�[�h���擾(�|�b�v�A�b�v�߂�l�p)
        If Me.HiddenSiireSakiCdNew.Value = String.Empty Then
            strSiireCd = IIf(Me.TextSiireSakiCd.Value <> "", Me.TextSiireSakiCd.Value, String.Empty)
            strSiireBrc = IIf(Me.TextSiireSakiBrc.Value <> "", Me.TextSiireSakiBrc.Value, String.Empty)
            Me.HiddenSiireSakiCdNew.Value = strSiireCd & strSiireBrc
        End If

        '�d���挟�������s
        If Me.HiddenSiireSakiCdNew.Value <> String.Empty Then
            dataArray = lgcSiireSearch.GetTyousakaisyaSearchResult(Me.HiddenSiireSakiCdNew.Value, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        True, _
                                                                        Me.HiddenKameitenCd.Value, _
                                                                        CInt(Me.HiddenTysKensakuType.Value))
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            Me.TextSiireSakiCd.Value = recData.TysKaisyaCd
            Me.TextSiireSakiBrc.Value = recData.JigyousyoCd
            Me.TextSiireSakiMei.Value = recData.TysKaisyaMei

            '�t�H�[�J�X�Z�b�g
            masterAjaxSM.SetFocus(Me.btnSiireSakiSearch)

            Me.HiddenSiireSakiCdNew.Value = String.Empty
        Else
            '������Ж����N���A
            Me.TextSiireSakiMei.Value = String.Empty

            Dim tmpFocusScript = "objEBI('" & btnSiireSakiSearch.ClientID & "').focus();"
            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.HiddenSiireSakiCdNew.ClientID & EarthConst.SEP_STRING & _
                                         Me.HiddenKameitenCd.ClientID & EarthConst.SEP_STRING & _
                                         Me.HiddenTysKensakuType.ClientID & "','" _
                                       & UrlConst.SEARCH_TYOUSAKAISYA & "','" _
                                       & Me.HiddenSiireSakiCdNew.ClientID & EarthConst.SEP_STRING & _
                                         Me.HiddenKakushuNG.ClientID & "','" _
                                       & Me.btnSiireSakiSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

        '��ʂ���R�[�h���擾(�������ʑޔ�p)
        If Me.HiddenSiireSakiCdNew.Value <> String.Empty Then
            strSiireCd = IIf(Me.TextSiireSakiCd.Value <> "", Me.TextSiireSakiCd.Value, String.Empty)
            strSiireBrc = IIf(Me.TextSiireSakiBrc.Value <> "", Me.TextSiireSakiBrc.Value, String.Empty)
            Me.HiddenSiireSakiCdNew.Value = strSiireCd & strSiireBrc
        End If

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

        Dim strFileNm As String = String.Empty  '�o�̓t�@�C����
        Dim dtCsv As DataTable
        Dim myLogic As New SiireDataSearchLogic

        '���������̐ݒ�
        SetSearchKeyFromCtrl(rec)

        '����
        Dim total_count As Integer = 0

        '�������s
        dtCsv = myLogic.GetSiireDataCsv(sender, rec, total_count)

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            Exit Sub
        ElseIf total_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV�o��"), 0, "kensakuErr")
            Exit Sub
        End If

        '�o�͗p�f�[�^�e�[�u������ɁACSV�o�͂��s�Ȃ�
        If CLogic.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_SIIRE_DATA, dtCsv) = False Then
            ' �o�͗p�����񂪂Ȃ��̂ŁA�����I��
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV�o��"), 0, "OutputErr")
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' �����N�����ύX�㏈���N���{�^��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonModalRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonModalRefresh.Click
        Dim tmpFocusScript = "exeSearch();"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript, True)
    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �������s�{�^���֘A
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�������s�{�^���̃C�x���g�n���h����ݒ�
        btnSearch.Attributes("onclick") = "checkJikkou('0');"
        'CSV�o�̓{�^���̃C�x���g�n���h����ݒ�
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou('1');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �敪�A�S�敪�֘A
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�敪�v���_�E���A�S�敪�`�F�b�N�{�b�N�X�̃C�x���g�n���h����ݒ�
        selectKbn.Attributes("onchange") = "setKubunVal();"
        kubun_all.Attributes("onclick") = "setKubunVal();"
        '��ʋN�����f�t�H���g�́u�S�敪�v�Ƀ`�F�b�N
        If IsPostBack = False Then
            kubun_all.Checked = True
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ���i�}�X�^�|�b�v�A�b�v��ʂ̕��ގw��
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.hdnSyouhinType.Value = EarthEnum.EnumSyouhinKubun.AllSyouhin

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�`�[�ԍ��C�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextDenpyouBangouFrom.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"
        Me.TextDenpyouBangouTo.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�}�X�^�����n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSyouhinCd.Attributes("onblur") = "clrName(this,'" & Me.TextHinmei.ClientID & "');"
        Me.TextSiireSakiCd.Attributes("onblur") = "clrName(this,'" & Me.TextSiireSakiMei.ClientID & "');"
        Me.TextSiireSakiBrc.Attributes("onblur") = "clrName(this,'" & Me.TextSiireSakiMei.ClientID & "');"

        '������Ќ�����ʌĂяo���p�̌����^�C�v���Z�b�g
        Me.HiddenTysKensakuType.Value = CStr(EarthEnum.EnumTyousakaisyaKensakuType.SIIRESAKI)

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ��猟���L�[���R�[�h�ւ̒l�Z�b�g���s���B
    ''' </summary>
    ''' <param name="recKey">���o���R�[�h�N���X�̃L�[</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As SiireDataKeyRecord)
        '�敪�������ɓ���̂�"�S�ĈȊO"�̂Ƃ�
        If kubun_all.Checked = False Then
            ' �敪
            recKey.Kbn = IIf(selectKbn.SelectedValue <> "", selectKbn.SelectedValue, String.Empty)
        End If

        '�ԍ� From
        recKey.BangouFrom = IIf(Me.TextBangouFrom.Value <> "", Me.TextBangouFrom.Value, String.Empty)
        '�ԍ� To
        recKey.BangouTo = IIf(Me.TextBangouTo.Value <> "", Me.TextBangouTo.Value, String.Empty)
        '�`�[�ԍ� From
        recKey.DenNoFrom = IIf(Me.TextDenpyouBangouFrom.Value <> "", Me.TextDenpyouBangouFrom.Value, String.Empty)
        '�`�[�ԍ� To 
        recKey.DenNoTo = IIf(Me.TextDenpyouBangouTo.Value <> "", Me.TextDenpyouBangouTo.Value, String.Empty)
        '�`�[�쐬��(�o�^�N����) From
        recKey.AddDatetimeFrom = IIf(Me.TextAddDateFrom.Value <> "", Me.TextAddDateFrom.Value, DateTime.MinValue)
        '�`�[�쐬��(�o�^�N����) To
        recKey.AddDatetimeTo = IIf(Me.TextAddDateTo.Value <> "", Me.TextAddDateTo.Value, DateTime.MinValue)
        '���i�R�[�h
        recKey.SyouhinCd = IIf(Me.TextSyouhinCd.Value <> "", Me.TextSyouhinCd.Value, String.Empty)
        '�d����R�[�h
        recKey.SiireSakiCd = IIf(Me.TextSiireSakiCd.Value <> "", Me.TextSiireSakiCd.Value, String.Empty)
        '�d����}��
        recKey.SiireSakiBrc = IIf(Me.TextSiireSakiBrc.Value <> "", Me.TextSiireSakiBrc.Value, String.Empty)
        '�d���於�J�i
        recKey.SiireSakiMeiKana = IIf(Me.TextSiireSakiMeiKana.Value <> "", Me.TextSiireSakiMeiKana.Value, String.Empty)
        '�d���N���� From
        recKey.SiireDateFrom = IIf(Me.TextSiireDateFrom.Value <> "", Me.TextSiireDateFrom.Value, DateTime.MinValue)
        '�d���N���� To
        recKey.SiireDateTo = IIf(Me.TextSiireDateTo.Value <> "", Me.TextSiireDateTo.Value, DateTime.MinValue)
        '�ŐV�`�[�̂ݕ\��
        recKey.NewDenpyouDisp = IIf(CheckSaisinDenpyou.Checked, CheckSaisinDenpyou.Value, Integer.MinValue)
        '�}�C�i�X�`�[�̂ݕ\��
        recKey.MinusDenpyouDisp = IIf(CheckMinusDenpyou.Checked, CheckMinusDenpyou.Value, Integer.MinValue)
        '�v��ςݓ`�[�̂ݕ\��
        recKey.KeijyouZumiDisp = IIf(CheckKeijyouFlg.Checked, CheckKeijyouFlg.Value, Integer.MinValue)

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
        Dim total_count As Integer = 0
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        '���W�b�N�N���X�̐���
        Dim logic As New SiireDataSearchLogic

        '�������s
        Dim resultArray As List(Of SiireSearchResultRecord) = logic.GetSiireDataInfo(sender, rec, 1, end_count, total_count)

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            Me.HiddenCsvOutPut.Value = ""
        ElseIf total_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        ' CSV�o�͏���ȏ�̏ꍇ�A�m�F���b�Z�[�W��t�^
        If total_count > intCsvMaxCnt Then
            Me.HiddenCsvMaxCnt.Value = "1"
        Else
            Me.HiddenCsvMaxCnt.Value = String.Empty
        End If

        '�\��������������
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")
        If Val(maxSearchCount.Value) < total_count Then
            resultCount.Style("color") = "red"
            displayCount = maxSearchCount.Value & " / " & CommonLogic.Instance.GetDisplayString(total_count)
        End If

        '�������ʌ�����ݒ�
        resultCount.InnerHtml = displayCount

        '�s�J�E���^
        Dim rowCnt As Integer = 0

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


        '�擾��������f�[�^����ʂɕ\��
        For Each data As SiireSearchResultRecord In resultArray

            rowCnt += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdDenUnqNo As New HtmlTableCell              '�`�[���j�[�NNO
            Dim objTdDenpyouNo As New HtmlTableCell             '�`�[�ԍ�
            Dim objTdSiireSakiCd As New HtmlTableCell           '�d����R�[�h
            Dim objTdSiireSakiMei As New HtmlTableCell          '�d���於
            Dim objTdKbn As New HtmlTableCell                   '�敪
            Dim objTdBangou As New HtmlTableCell                '�ԍ�
            Dim objTdSesyuMei As New HtmlTableCell              '�{�喼
            Dim objTdSyouhinCd As New HtmlTableCell             '���i�R�[�h
            Dim objTdHinmei As New HtmlTableCell                '�i��
            Dim objTdSuu As New HtmlTableCell                   '����
            Dim objTdSiireGaku As New HtmlTableCell             '������z
            Dim objTdSiireDate As New HtmlTableCell             '�d���N����
            Dim objTdDenpyouType As New HtmlTableCell           '�`�[���
            Dim objTdDenSiireDate As New HtmlTableCell          '�`�[�d���N����
            Dim objTdDenSiireDateLink As New HyperLink          '�`�[�d���N���������N
            Dim objTdSiireKeijyouFlg As New HtmlTableCell       '�d������(�d���v��t���O)

            Dim strDenpyouType As String

            '�������ʔz�񂩂�Z���Ɋi�[
            objTdDenUnqNo.InnerHtml = CLogic.GetDisplayString(data.DenpyouUniqueNo, EarthConst.HANKAKU_SPACE)
            objTdDenpyouNo.InnerHtml = CLogic.GetDisplayString(data.DenpyouNo, EarthConst.HANKAKU_SPACE)
            objTdSiireSakiCd.InnerHtml = CLogic.GetDispSeikyuuSakiCd(String.Empty, data.TysKaisyaCd, data.TysKaisyaJigyousyoCd, False)
            objTdSiireSakiMei.InnerHtml = CLogic.GetDisplayString(data.TysKaisyaMei, EarthConst.HANKAKU_SPACE)
            objTdKbn.InnerHtml = CLogic.GetDisplayString(data.Kbn, EarthConst.HANKAKU_SPACE)
            objTdBangou.InnerHtml = CLogic.GetDisplayString(data.Bangou, EarthConst.HANKAKU_SPACE)
            objTdSesyuMei.InnerHtml = CLogic.GetDisplayString(data.SesyuMei, EarthConst.HANKAKU_SPACE)
            objTdSyouhinCd.InnerHtml = CLogic.GetDisplayString(data.SyouhinCd, EarthConst.HANKAKU_SPACE)
            objTdHinmei.InnerHtml = CLogic.GetDisplayString(data.Hinmei, EarthConst.HANKAKU_SPACE)
            objTdSuu.InnerHtml = CLogic.GetDisplayString(Format(data.Suu, "#,0"), EarthConst.HANKAKU_SPACE)
            objTdSiireGaku.InnerHtml = CLogic.GetDisplayString(Format(data.SiireGaku, "#,0"), EarthConst.HANKAKU_SPACE)
            objTdSiireDate.InnerHtml = CLogic.GetDisplayString(data.SiireDate, EarthConst.HANKAKU_SPACE)
            strDenpyouType = CLogic.GetDisplayString(data.DenpyouSyubetu, EarthConst.HANKAKU_SPACE)
            objTdDenpyouType.InnerHtml = strDenpyouType
            objTdSiireKeijyouFlg.InnerHtml = IIf(data.SiireKeijyouFlg = 1, EarthConst.SIIRE_KEI_ZUMI, EarthConst.HANKAKU_SPACE)

            '�}�C�i�X�`�[�݂̂̐ݒ�
            If strDenpyouType = EarthConst.SR Then
                Dim strDenSiireJs As String = String.Empty
                objTdDenSiireDateLink.Text = CLogic.GetDisplayString(data.DenpyouSiireDate, EarthConst.HANKAKU_SPACE)
                objTdDenSiireDateLink.NavigateUrl = "javascript:void(0);"
                strDenSiireJs = "openModalDenSiireDate('" & UrlConst.POPUP_DENPYOU_SIIRE_DATE_HENKOU & "','" & CLogic.GetDisplayString(data.DenpyouUniqueNo) & "','" & CLogic.GetDisplayString(data.DenpyouSiireDate) & "')"
                objTdDenSiireDateLink.Attributes.Add("onclick", strDenSiireJs)
                objTdDenSiireDate.Controls.Add(objTdDenSiireDateLink)
            Else
                objTdDenSiireDate.InnerHtml = CLogic.GetDisplayString(data.DenpyouSiireDate, EarthConst.HANKAKU_SPACE)
            End If


            '�e�Z���̕��ݒ�
            If rowCnt = 1 Then
                objTdDenUnqNo.Style("width") = widthList1(0)
                objTdDenpyouType.Style("width") = widthList1(1)
                objTdDenpyouNo.Style("width") = widthList1(2)
                objTdSiireSakiCd.Style("width") = widthList1(3)
                objTdSiireSakiMei.Style("width") = widthList1(4)
                objTdKbn.Style("width") = widthList1(5)
                objTdBangou.Style("width") = widthList1(6)
                objTdSesyuMei.Style("width") = widthList1(7)

                objTdSyouhinCd.Style("width") = widthList2(0)
                objTdHinmei.Style("width") = widthList2(1)
                objTdSuu.Style("width") = widthList2(2)
                objTdSiireGaku.Style("width") = widthList2(3)
                objTdSiireDate.Style("width") = widthList2(4)
                objTdDenSiireDate.Style("width") = widthList2(5)
                objTdSiireKeijyouFlg.Style("width") = widthList2(6)
            End If

            '�X�^�C���A�N���X�ݒ�
            objTdDenUnqNo.Attributes("class") = "textCenter"
            objTdDenpyouNo.Attributes("class") = "textCenter"
            objTdKbn.Attributes("class") = "textCenter"
            objTdBangou.Attributes("class") = "textCenter"
            objTdSyouhinCd.Attributes("class") = "textCenter"
            objTdSyouhinCd.Attributes("class") = "textCenter"
            objTdSiireGaku.Attributes("class") = "kingaku"
            objTdSuu.Attributes("class") = "kingaku"
            objTdSiireDate.Attributes("class") = "date textCenter"
            objTdDenSiireDate.Attributes("class") = "date textCenter"
            objTdSiireSakiCd.Attributes("class") = "textCenter"
            objTdDenpyouType.Attributes("class") = "textCenter"
            If strDenpyouType = EarthConst.SR Then
                objTdDenpyouType.Style("color") = "red"
            End If
            objTdSiireKeijyouFlg.Attributes("class") = "textCenter"

            '�ꗗ�����s��ID�t�^�Ɗi�[
            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            With objTr1.Controls
                .Add(objTdDenUnqNo)
                .Add(objTdDenpyouType)
                .Add(objTdDenpyouNo)
                .Add(objTdSiireSakiCd)
                .Add(objTdSiireSakiMei)
                .Add(objTdKbn)
                .Add(objTdBangou)
                .Add(objTdSesyuMei)
            End With

            '�ꗗ�E���s��ID�t�^�Ɗi�[
            objTr2.ID = "DataTable_resultTr2_" & rowCnt

            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(CheckSaisinDenpyou.Attributes("tabindex")) + 10
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                '2�s�ڈȍ~�̓^�u�ړ��Ȃ�
                objTr2.Attributes("tabindex") = -1
            End If

            With objTr2.Controls
                .Add(objTdSyouhinCd)
                .Add(objTdHinmei)
                .Add(objTdSuu)
                .Add(objTdSiireGaku)
                .Add(objTdSiireDate)
                .Add(objTdDenSiireDate)
                .Add(objTdSiireKeijyouFlg)
            End With

            '�e�[�u���ɍs���Z�b�g
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next
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

#End Region

End Class