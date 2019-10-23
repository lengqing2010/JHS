
Partial Public Class SearchUriageData
    Inherits System.Web.UI.Page

    '��ʕ\���̕�����ϊ��p
    Private CLogic As CommonLogic = CommonLogic.Instance
    '���b�Z�[�W�N���X
    Private MLogic As New MessageLogic
    '���O�C�����[�U���R�[�h
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    '����f�[�^�e�[�u�����R�[�h�N���X
    Private rec As New UriageDataKeyRecord

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
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic

            ' �敪�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(selectKbn, DropDownHelper.DropDownType.Kubun)
            ' ������敪�R���{�Ƀf�[�^���o�C���h����
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)


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
    ''' �������s�{�^���������̏���
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

        If strSyouhinCd <> String.Empty Then
            '���i���������s
            dataArray = lgcSyouhinSearch.GetSyouhinInfo(strSyouhinCd, String.Empty, total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As SyouhinMeisaiRecord = dataArray(0)
            Me.TextSyouhinCd.Value = recData.SyouhinCd
            Me.TextHinmei.Value = recData.SyouhinMei
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
    ''' �����挟���{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiSearch.ServerClick
        Dim blnResult As Boolean

        '�����挟����ʌďo
        blnResult = CLogic.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                        , Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                        , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)

        '�ԐF�����ύX�Ή���z��Ɋi�[
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextSeikyuuKameiTorikesiRiyuu}
        '������R�擾�ݒ�ƐF�֏���
        CLogic.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, TextSeikyuuKameiTorikesiRiyuu, True, False, objChgColor)

        If blnResult Then
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.btnSeikyuuSakiSearch)
        End If

    End Sub

    ''' <summary>
    ''' �����X�����{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim blnResult As Boolean

        '�����X������ʌďo
        blnResult = CLogic.CallKameitenSearchWindow(sender _
                                                , e _
                                                , Me _
                                                , Me.selectKbn.ClientID _
                                                , Me.selectKbn.SelectedValue _
                                                , Me.TextKameitenCd _
                                                , Me.TextKameitenMei _
                                                , Me.btnKameitenSearch _
                                                , False _
                                                , Me.TextKameiTorikesiRiyuu _
                                                )

        If blnResult Then
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.btnKameitenSearch)
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
        Dim myLogic As New UriageDataSearchLogic

        '���������̐ݒ�
        Me.SetSearchKeyFromCtrl(rec)

        '����
        Dim total_count As Integer = 0

        '�������s
        dtCsv = myLogic.GetUriageDataCsv(sender, rec, total_count)

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
        If CLogic.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_URIAGE_DATA, dtCsv) = False Then
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
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.SelectSeikyuuKbn.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextKameitenCd.Attributes("onblur") = "clrKameitenInfo(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������R�e�L�X�g�{�b�N�X�̃X�^�C����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �����X������R
        CLogic.chgVeiwMode(Me.TextKameiTorikesiRiyuu, Nothing, True)
        ' �����������R
        CLogic.chgVeiwMode(Me.TextSeikyuuKameiTorikesiRiyuu, Nothing, True)

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ��猟���L�[���R�[�h�ւ̒l�Z�b�g���s���B
    ''' </summary>
    ''' <param name="recKey">���o���R�[�h�N���X�̃L�[</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As UriageDataKeyRecord)
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
        '�`�[�쐬�� From
        recKey.AddDatetimeFrom = IIf(Me.TextAddDateFrom.Value <> "", Me.TextAddDateFrom.Value, DateTime.MinValue)
        '�`�[�쐬�� To
        recKey.AddDatetimeTo = IIf(Me.TextAddDateTo.Value <> "", Me.TextAddDateTo.Value, DateTime.MinValue)
        '���i�R�[�h
        recKey.SyouhinCd = IIf(Me.TextSyouhinCd.Value <> "", Me.TextSyouhinCd.Value, String.Empty)
        '������R�[�h
        recKey.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> "", Me.TextSeikyuuSakiCd.Value, String.Empty)
        '�����X�R�[�h
        recKey.KameitenCd = IIf(Me.TextKameitenCd.Value <> "", Me.TextKameitenCd.Value, String.Empty)
        '������}��
        recKey.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> "", Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '������敪
        recKey.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuKbn.SelectedValue <> "", Me.SelectSeikyuuKbn.SelectedValue, String.Empty)
        '������J�i��
        recKey.SeikyuuSakiMeiKana = IIf(Me.TextSeikyuuSakiMeiKana.Value <> "", Me.TextSeikyuuSakiMeiKana.Value, String.Empty)
        '�����N���� From
        recKey.SeikyuuDateFrom = IIf(Me.TextSeikyuuDateFrom.Value <> "", Me.TextSeikyuuDateFrom.Value, DateTime.MinValue)
        '�����N���� To
        recKey.SeikyuuDateTo = IIf(Me.TextSeikyuuDateTo.Value <> "", Me.TextSeikyuuDateTo.Value, DateTime.MinValue)
        '����N���� From
        recKey.UriDateFrom = IIf(Me.TextUriageDateFrom.Value <> "", Me.TextUriageDateFrom.Value, DateTime.MinValue)
        '����N���� To
        recKey.UriDateTo = IIf(Me.TextUriageDateTo.Value <> "", Me.TextUriageDateTo.Value, DateTime.MinValue)
        '�`�[����N���� From
        recKey.DenUriDateFrom = IIf(Me.TextDenpyouUriageDateFrom.Value <> "", Me.TextDenpyouUriageDateFrom.Value, DateTime.MinValue)
        '�`�[����N���� To
        recKey.DenUriDateTo = IIf(Me.TextDenpyouUriageDateTo.Value <> "", Me.TextDenpyouUriageDateTo.Value, DateTime.MinValue)
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
        Const CELL_COLOR As String = "red"
        Const CELL_BOLD As String = "bold"
        Const CSS_TEXT_CENTER = "textCenter"
        Const CSS_KINGAKU = "kingaku"
        Const CSS_NUMBER = "number"
        Const CSS_DATE = "date"

        '�\���ő匏��
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        '���W�b�N�N���X�̐���
        Dim logic As New UriageDataSearchLogic

        '�������s
        Dim resultArray As List(Of UriageSearchResultRecord) = logic.GetUriageDataInfo(sender, rec, 1, end_count, total_count)

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
            resultCount.Style("color") = CELL_COLOR
            displayCount = maxSearchCount.Value & " / " & CLogic.GetDisplayString(total_count)
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
        For Each data As UriageSearchResultRecord In resultArray

            rowCnt += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdDenUnqNo As New HtmlTableCell              '�`�[���j�[�NNO
            Dim objTdDenpyouType As New HtmlTableCell           '�`�[���
            Dim objTdDenpyouNo As New HtmlTableCell             '�`�[�ԍ�
            Dim objTdSeikyuuSakiCd As New HtmlTableCell         '������R�[�h
            Dim objTdSeikyuuSakiMei As New HtmlTableCell        '�����於
            Dim objTdKbn As New HtmlTableCell                   '�敪
            Dim objTdBangou As New HtmlTableCell                '�ԍ�
            Dim objTdSesyuMei As New HtmlTableCell              '�{�喼
            Dim objTdSyouhinCd As New HtmlTableCell             '���i�R�[�h
            Dim objTdHinmei As New HtmlTableCell                '�i��
            Dim objTdUriGaku As New HtmlTableCell               '������z
            Dim objTdSuuRyou As New HtmlTableCell               '����
            Dim objTdUriDate As New HtmlTableCell               '����N����
            Dim objTdDenUriDate As New HtmlTableCell            '�`�[����N����
            Dim objTdDenUriDateLink As New HyperLink            '�`�[����N���������N
            Dim objTdUriKeijyouFlg As New HtmlTableCell         '���㏈��(����v��t���O)
            Dim objTdSeikyuuDate As New HtmlTableCell           '�����N����
            Dim objTdSeikyuuDateLink As New HyperLink           '�����N���������N
            Dim objTdKameitenCd As New HtmlTableCell            '�����X�R�[�h
            Dim objTdKameitenMei As New HtmlTableCell           '�����X��
            Dim strDenpyouType As String                        '�`�[���

            '�������ʔz�񂩂�Z���Ɋi�[
            objTdDenUnqNo.InnerHtml = CLogic.GetDisplayString(data.DenUnqNo, EarthConst.HANKAKU_SPACE)
            strDenpyouType = CLogic.GetDisplayString(data.DenSyubetu, EarthConst.HANKAKU_SPACE)
            objTdDenpyouType.InnerHtml = strDenpyouType
            objTdDenpyouNo.InnerHtml = CLogic.GetDisplayString(data.DenNo, EarthConst.HANKAKU_SPACE)
            objTdSeikyuuSakiCd.InnerHtml = CLogic.GetDispSeikyuuSakiCd(data.SeikyuuSakiKbn, data.SeikyuuSakiCd, data.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = CLogic.GetDisplayString(data.SeikyuuSakiMei, EarthConst.HANKAKU_SPACE)
            objTdKbn.InnerHtml = CLogic.GetDisplayString(data.Kbn, EarthConst.HANKAKU_SPACE)
            objTdBangou.InnerHtml = CLogic.GetDisplayString(data.Bangou, EarthConst.HANKAKU_SPACE)
            objTdSesyuMei.InnerHtml = CLogic.GetDisplayString(data.SesyuMei, EarthConst.HANKAKU_SPACE)
            objTdSyouhinCd.InnerHtml = CLogic.GetDisplayString(data.SyouhinCd, EarthConst.HANKAKU_SPACE)
            objTdHinmei.InnerHtml = CLogic.GetDisplayString(data.Hinmei, EarthConst.HANKAKU_SPACE)
            objTdUriGaku.InnerHtml = CLogic.GetDisplayString(Format(data.UriGaku, EarthConst.FORMAT_KINGAKU_1), EarthConst.HANKAKU_SPACE)
            objTdSuuRyou.InnerHtml = CLogic.GetDisplayString(data.Suu, EarthConst.HANKAKU_SPACE)
            objTdUriDate.InnerHtml = CLogic.GetDisplayString(data.UriDate, EarthConst.HANKAKU_SPACE)

            objTdUriKeijyouFlg.InnerHtml = IIf(data.UriKeijyouFlg = 1, EarthConst.URIAGE_KEI_ZUMI, EarthConst.HANKAKU_SPACE)

            '�}�C�i�X�`�[�݂̂̐ݒ�
            If strDenpyouType = EarthConst.UR Then

                Dim strSeikyuuJs As String = String.Empty
                Dim strDenUriJs As String = String.Empty

                '�`�[����N����
                objTdDenUriDateLink.Text = CLogic.GetDisplayString(data.DenUriDate, EarthConst.HANKAKU_SPACE)
                objTdDenUriDateLink.NavigateUrl = "javascript:void(0);"
                strDenUriJs = "openModalDenUriDate('" & UrlConst.POPUP_DENPYOU_URIAGE_DATE_HENKOU & "','" & CLogic.GetDisplayString(data.DenUnqNo) & "','" & CLogic.GetDisplayString(data.DenUriDate) & "')"
                objTdDenUriDateLink.Attributes.Add("onclick", strDenUriJs)
                objTdDenUriDate.Controls.Add(objTdDenUriDateLink)

                '�����N�����̒l�ݒ�ƃ����N�̐ݒ�
                objTdSeikyuuDateLink.Text = CLogic.GetDisplayString(data.SeikyuuDate, EarthConst.HANKAKU_SPACE)
                objTdSeikyuuDateLink.NavigateUrl = "javascript:void(0);"
                strSeikyuuJs &= "openModalSeikyuuDate('" & UrlConst.POPUP_SEIKYUU_DATE_HENKOU & "','" & CLogic.GetDisplayString(data.DenUnqNo) & "','" & CLogic.GetDisplayString(data.SeikyuuDate) & "')"
                objTdSeikyuuDateLink.Attributes.Add("onclick", strSeikyuuJs)
                objTdSeikyuuDate.Controls.Add(objTdSeikyuuDateLink)
            Else
                '�`�[����N����(�����N�Ȃ�)
                objTdDenUriDate.InnerHtml = CLogic.GetDisplayString(data.DenUriDate, EarthConst.HANKAKU_SPACE)
                '�����N����(�����N�Ȃ�)
                objTdSeikyuuDate.InnerHtml = CLogic.GetDisplayString(data.SeikyuuDate, EarthConst.HANKAKU_SPACE)
            End If

            objTdKameitenCd.InnerHtml = CLogic.GetDisplayString(data.KameitenCd, EarthConst.HANKAKU_SPACE)
            objTdKameitenMei.InnerHtml = CLogic.GetDisplayString(data.KameitenMei, EarthConst.HANKAKU_SPACE)

            '�e�Z���̕��ݒ�
            If rowCnt = 1 Then
                objTdDenUnqNo.Style("width") = widthList1(0)
                objTdDenpyouType.Style("width") = widthList1(1)
                objTdDenpyouNo.Style("width") = widthList1(2)
                objTdSeikyuuSakiCd.Style("width") = widthList1(3)
                objTdSeikyuuSakiMei.Style("width") = widthList1(4)
                objTdKbn.Style("width") = widthList1(5)
                objTdBangou.Style("width") = widthList1(6)
                objTdSesyuMei.Style("width") = widthList1(7)

                objTdSyouhinCd.Style("width") = widthList2(0)
                objTdHinmei.Style("width") = widthList2(1)
                objTdUriGaku.Style("width") = widthList2(2)
                objTdSuuRyou.Style("width") = widthList2(3)
                objTdUriDate.Style("width") = widthList2(4)
                objTdDenUriDate.Style("width") = widthList2(5)
                objTdUriKeijyouFlg.Style("width") = widthList2(6)
                objTdSeikyuuDate.Style("width") = widthList2(7)
                objTdKameitenCd.Style("width") = widthList2(8)
                objTdKameitenMei.Style("width") = widthList2(9)
            End If

            '�X�^�C���A�N���X�ݒ�
            objTdDenUnqNo.Attributes("class") = CSS_TEXT_CENTER
            objTdDenpyouType.Attributes("class") = CSS_TEXT_CENTER
            If strDenpyouType = EarthConst.UR Then
                objTdDenpyouType.Style("color") = CELL_COLOR
            End If
            objTdDenpyouNo.Attributes("class") = CSS_TEXT_CENTER
            objTdKbn.Attributes("class") = CSS_TEXT_CENTER
            objTdBangou.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinCd.Attributes("class") = CSS_TEXT_CENTER
            objTdUriGaku.Attributes("class") = CSS_KINGAKU
            objTdSuuRyou.Attributes("class") = CSS_NUMBER
            objTdUriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            '�`�[����N�����͐ԓ`�E���`�Ŕ�r�𕪂���
            If strDenpyouType = EarthConst.UR Then
                '�ԓ`�̏ꍇ�̓����N�Ɣ�r
                If objTdUriDate.InnerText <> objTdDenUriDateLink.Text Then
                    objTdDenUriDateLink.Style("color") = CELL_COLOR
                    objTdDenUriDate.Style("font-weight") = CELL_BOLD
                End If
            Else
                '���`�̏ꍇ�̓Z�����̕����Ɣ�r
                If objTdUriDate.InnerText <> objTdDenUriDate.InnerText Then
                    objTdDenUriDate.Style("color") = CELL_COLOR
                    objTdDenUriDate.Style("font-weight") = CELL_BOLD
                End If
            End If
            objTdDenUriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdUriKeijyouFlg.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSakiCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdKameitenCd.Attributes("class") = CSS_TEXT_CENTER

            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            '�s�ɃZ�����Z�b�g1
            With objTr1.Controls
                .Add(objTdDenUnqNo)
                .Add(objTdDenpyouType)
                .Add(objTdDenpyouNo)
                .Add(objTdSeikyuuSakiCd)
                .Add(objTdSeikyuuSakiMei)
                .Add(objTdKbn)
                .Add(objTdBangou)
                .Add(objTdSesyuMei)
            End With

            objTr2.ID = "DataTable_resultTr2_" & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(CheckSaisinDenpyou.Attributes("tabindex")) + 10
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2�s�ڈȍ~�̓^�u�ړ��Ȃ�
                objTr2.Attributes("tabindex") = -1
            End If

            '�s�ɃZ���ƃZ�b�g2
            With objTr2.Controls
                .Add(objTdSyouhinCd)
                .Add(objTdHinmei)
                .Add(objTdUriGaku)
                .Add(objTdSuuRyou)
                .Add(objTdUriDate)
                .Add(objTdDenUriDate)
                .Add(objTdUriKeijyouFlg)
                .Add(objTdSeikyuuDate)
                .Add(objTdKameitenCd)
                .Add(objTdKameitenMei)
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