Partial Public Class SearchSiharaiData
    Inherits System.Web.UI.Page

    '��ʕ\���̕�����ϊ��p
    Private CLogic As CommonLogic = CommonLogic.Instance
    '���b�Z�[�W�N���X
    Private MLogic As New MessageLogic
    '���O�C�����[�U���R�[�h
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    '����f�[�^�e�[�u�����R�[�h�N���X
    Private rec As New SiharaiDataKeyRecord

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
            '�Ȃ�

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()

            '�t�H�[�J�X�ݒ�
            Me.TextShriDateFrom.Focus()

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
        Me.BtnSearch.Focus()

        '����������ݒ�
        SetSearchKeyFromCtrl(rec)

        '�������ʂ���ʂɕ\��
        SetSearchResult(sender, e)

    End Sub

    ''' <summary>
    ''' ������Ќ����{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnTysKaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTysKaisyaSearch.ServerClick
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)
        Dim blnTorikesi As Boolean = True

        If Me.TextTysKaisyaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(Me.TextTysKaisyaCd.Value, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            "")
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            Me.TextTysKaisyaCd.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            Me.TextTysKaisyaMei.Value = recData.TysKaisyaMei

            '�t�H�[�J�X�Z�b�g
            masterAjaxSM.SetFocus(Me.BtnTysKaisyaSearch)
        Else
            '������Ж����N���A
            Me.TextTysKaisyaMei.Value = String.Empty
            Me.HiddenTyskaisyaCd.Value = String.Empty
            Dim tmpFocusScript = "objEBI('" & BtnTysKaisyaSearch.ClientID & "').focus();"
            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpScript = "callSearch('" & Me.TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & Me.HiddenTyskaisyaCd.ClientID & _
                                             "','" & UrlConst.SEARCH_TYOUSAKAISYA & _
                                             "','" & Me.TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & Me.TextTysKaisyaMei.ClientID & _
                                             "','" & Me.BtnTysKaisyaSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' �V��v�x���挟���{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnSkkShriSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSkkShriSakiSearch.ServerClick
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim lgcTysKaisyaSearch As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of SinkaikeiSiharaiSakiRecord)

        If Me.TextSkkJigyousyoCd.Value <> String.Empty Or Me.TextSkkShriSakiCd.Value <> String.Empty Then
            dataArray = lgcTysKaisyaSearch.GetSkkSiharaisakiSearchResult(Me.TextSkkJigyousyoCd.Value, _
                                                                         Me.TextSkkShriSakiCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As SinkaikeiSiharaiSakiRecord = dataArray(0)
            Me.TextSkkJigyousyoCd.Value = recData.SkkJigyouCd
            Me.TextSkkShriSakiCd.Value = recData.SkkShriSakiCd
            Me.TextShriSakiMei.Value = recData.ShriSakiMeiKanji
        Else
            '�x���於���N���A
            Me.TextShriSakiMei.Value = String.Empty
            '�V��v�x����}�X�^��\��
            Dim strScript = "objSrchWin = window.open('" & UrlConst.EARTH2_SEARCH_SINKAIKEI_SIHARAI_SAKI & "?Kbn='+escape('�V��v�x����')+'&SiharaiKbn='+escape   ('Siharai')+'&FormName=" & _
                                                      Me.Page.Form.Name & "&objCd=" & _
                                                      Me.TextSkkJigyousyoCd.ClientID & _
                                                      "&objCd2=" & Me.TextSkkShriSakiCd.ClientID & _
                                                      "&objHidCd2=" & Me.HiddenSkkShriSakiCd.ClientID & _
                                                      "&objMei=" & Me.TextShriSakiMei.ClientID & _
                                                      "&strCd='+escape(eval('document.all.'+'" & _
                                                      Me.TextSkkJigyousyoCd.ClientID & "').value)+'&strCd2='+escape(eval('document.all.'+'" & _
                                                      Me.TextSkkShriSakiCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                                      Me.TextShriSakiMei.ClientID & "').value), 'searchWindow',       'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
            Exit Sub
        End If

    End Sub


    ''' <summary>
    ''' CSV�o�̓{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnHiddenCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHiddenCsv.ServerClick
        setFocusAJ(Me.BtnCsv) '�t�H�[�J�X

        Me.HiddenCsvOutPut.Value = String.Empty '�t���O���N���A

        Dim strFileNm As String = String.Empty  '�o�̓t�@�C����
        Dim dtCsv As DataTable
        Dim myLogic As New SiharaiDataSearchLogic

        '���������̐ݒ�
        Me.SetSearchKeyFromCtrl(rec)

        '����
        Dim total_count As Integer = 0

        '�������s
        dtCsv = myLogic.GetSiharaiDataCsv(sender, rec, total_count)

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
        If CLogic.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_SIHARAI_DATA, dtCsv) = False Then
            ' �o�͗p�����񂪂Ȃ��̂ŁA�����I��
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV�o��"), 0, "OutputErr")
            Exit Sub
        End If

    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        Dim checkDate As String = "checkDate(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���t�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�x���N����From
        Me.TextShriDateFrom.Attributes("onblur") = checkDate
        '�x���N����To
        Me.TextShriDateTo.Attributes("onblur") = checkDate

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �������s�{�^���֘A
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�������s�{�^���̃C�x���g�n���h����ݒ�
        BtnSearch.Attributes("onclick") = "checkJikkou('0');"
        'CSV�o�̓{�^���̃C�x���g�n���h����ݒ�
        Me.BtnCsv.Attributes("onclick") = "checkJikkou('1');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�`�[�ԍ��C�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextDenNoFrom.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"
        Me.TextDenNoTo.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"


        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�}�X�^�����n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextTysKaisyaCd.Attributes("onblur") = "clrName(this,'" & Me.TextTysKaisyaMei.ClientID & "');"
        Me.TextSkkJigyousyoCd.Attributes("onblur") = "clrName(this,'" & Me.TextShriSakiMei.ClientID & "');"
        Me.TextSkkShriSakiCd.Attributes("onblur") = "clrName(this,'" & Me.TextShriSakiMei.ClientID & "');"

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ��猟���L�[���R�[�h�ւ̒l�Z�b�g���s���B
    ''' </summary>
    ''' <param name="recKey">���o���R�[�h�N���X�̃L�[</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As SiharaiDataKeyRecord)

        '�x���N���� From
        recKey.ShriDateFrom = IIf(Me.TextShriDateFrom.Value <> "", Me.TextShriDateFrom.Value, DateTime.MinValue)
        '�x���N������ To
        recKey.ShriDateTo = IIf(Me.TextShriDateTo.Value <> "", Me.TextShriDateTo.Value, DateTime.MinValue)
        '�`�[�ԍ� From
        recKey.DenNoFrom = IIf(Me.TextDenNoFrom.Value <> "", Me.TextDenNoFrom.Value, String.Empty)
        '�`�[�ԍ� To 
        recKey.DenNoTo = IIf(Me.TextDenNoTo.Value <> "", Me.TextDenNoTo.Value, String.Empty)
        '������ЃR�[�h�{������Ў��Ə��R�[�h
        recKey.TysKaisyaCd = IIf(Me.TextTysKaisyaCd.Value <> "", Me.TextTysKaisyaCd.Value, String.Empty)
        '�V��v���Ə��R�[�h
        recKey.SkkJigyouCd = IIf(Me.TextSkkJigyousyoCd.Value <> "", Me.TextSkkJigyousyoCd.Value, String.Empty)
        '�V��v�x����R�[�h
        recKey.SkkShriSakiCd = IIf(Me.TextSkkShriSakiCd.Value <> "", Me.TextSkkShriSakiCd.Value, String.Empty)
        '�ŐV�`�[�̂ݕ\��
        recKey.NewDenDisp = IIf(CheckSaisinDenpyou.Checked, CheckSaisinDenpyou.Value, Integer.MinValue)

    End Sub

    ''' <summary>
    ''' ���������Ō����������e���������ʃe�[�u���ɕ\������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Const CELL_COLOR As String = "red"
        Const CSS_TEXT_CENTER = "textCenter"
        Const CSS_KINGAKU = "kingaku"
        Const CSS_DATE = "date"

        '�\���ő匏��
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        '���W�b�N�N���X�̐���
        Dim logic As New SiharaiDataSearchLogic

        '�������s
        Dim resultArray As List(Of SiharaiSearchResultRecord) = logic.GetSiharaiDataInfo(sender, rec, 1, end_count, total_count)

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
        '�x�����v
        Dim lngTotalGaku As Long = 0

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

        '�擾�����x���f�[�^����ʂɕ\��
        For Each data As SiharaiSearchResultRecord In resultArray

            rowCnt += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdDenUnqNo As New HtmlTableCell              '�`�[���j�[�NNO
            Dim objTdDenNo As New HtmlTableCell                 '�`�[�ԍ�
            Dim objTdTysKaisyaCd As New HtmlTableCell           '������ЃR�[�h
            Dim objTdSkkCd As New HtmlTableCell                 '�V��v�R�[�h
            Dim objTdShriSaki As New HtmlTableCell              '�x����
            Dim objTdFurikomigaku As New HtmlTableCell          '�U���z
            Dim objTdSousaigaku As New HtmlTableCell            '���E�z
            Dim objTdShriDate As New HtmlTableCell              '�x���N����
            Dim objTdTekiyou As New HtmlTableCell               '�E�v

            '�������ʔz�񂩂�Z���Ɋi�[
            objTdDenUnqNo.InnerHtml = CLogic.GetDisplayString(data.DenUnqNo, EarthConst.HANKAKU_SPACE)
            objTdDenNo.InnerHtml = CLogic.GetDisplayString(data.DenNo, EarthConst.HANKAKU_SPACE)
            objTdTysKaisyaCd.InnerHtml = CLogic.GetDisplayString(data.TysKaisyaCd, EarthConst.HANKAKU_SPACE)
            objTdSkkCd.InnerHtml = CLogic.GetDisplayString(data.SkkJigyouCd + data.SkkShriSakiCd, EarthConst.HANKAKU_SPACE)
            objTdShriSaki.InnerHtml = CLogic.GetDisplayString(data.ShriSakiMei, EarthConst.HANKAKU_SPACE)
            objTdFurikomigaku.InnerHtml = CLogic.GetDisplayString(Format(data.Furikomi, EarthConst.FORMAT_KINGAKU_1), EarthConst.HANKAKU_SPACE)
            objTdSousaigaku.InnerHtml = CLogic.GetDisplayString(Format(data.Sousai, EarthConst.FORMAT_KINGAKU_1), EarthConst.HANKAKU_SPACE)
            objTdShriDate.InnerHtml = CLogic.GetDisplayString(data.ShriDate, EarthConst.HANKAKU_SPACE)
            objTdTekiyou.InnerHtml = CLogic.GetDisplayString(data.TekiyouMei, EarthConst.HANKAKU_SPACE)

            '�����v���z�����Z
            lngTotalGaku += (data.Furikomi + data.Sousai)

            '�e�Z���̕��ݒ�()
            If rowCnt = 1 Then
                objTdDenUnqNo.Style("width") = widthList1(0)
                objTdDenNo.Style("width") = widthList1(1)
                objTdTysKaisyaCd.Style("width") = widthList1(2)
                objTdSkkCd.Style("width") = widthList1(3)
                objTdShriSaki.Style("width") = widthList1(4)

                objTdFurikomigaku.Style("width") = widthList2(0)
                objTdSousaigaku.Style("width") = widthList2(1)
                objTdShriDate.Style("width") = widthList2(2)
                objTdTekiyou.Style("width") = widthList2(3)
            End If

            '�X�^�C���A�N���X�ݒ�
            objTdDenUnqNo.Attributes("class") = CSS_TEXT_CENTER
            objTdDenNo.Attributes("class") = CSS_TEXT_CENTER
            objTdTysKaisyaCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSkkCd.Attributes("class") = CSS_TEXT_CENTER
            objTdFurikomigaku.Attributes("class") = CSS_KINGAKU
            objTdSousaigaku.Attributes("class") = CSS_KINGAKU
            objTdShriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER

            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            '�s�ɃZ�����Z�b�g1
            With objTr1.Controls
                .Add(objTdDenUnqNo)
                .Add(objTdDenNo)
                .Add(objTdTysKaisyaCd)
                .Add(objTdSkkCd)
                .Add(objTdShriSaki)
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
                .Add(objTdFurikomigaku)
                .Add(objTdSousaigaku)
                .Add(objTdShriDate)
                .Add(objTdTekiyou)
            End With

            '�e�[�u���ɍs���Z�b�g
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)
        Next

        '�x�����v��ݒ�
        Me.TdTotalKingaku.InnerHtml = Format(lngTotalGaku, EarthConst.FORMAT_KINGAKU_2)

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