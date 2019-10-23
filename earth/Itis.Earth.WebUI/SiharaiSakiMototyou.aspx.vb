Partial Public Class SiharaiSakiMototyou
    Inherits System.Web.UI.Page

    '��ʕ\���̕�����ϊ��p
    Private CLogic As CommonLogic = CommonLogic.Instance
    '���b�Z�[�W�N���X
    Private MLogic As New MessageLogic
    '���O�C�����[�U���R�[�h
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    'CSV�p�f���~�^�w��
    Dim strCsvDelimiter As String = EarthConst.CSV_DELIMITER
    'CSV�p���蕶���w��
    Dim strCsvQuote As String = String.Empty

    Dim cl As New CommonLogic

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

        '�\�����ڂ̏�����
        TdSaisinKurikosiDate.InnerHtml = String.Empty
        TdTourokuZandaka.InnerHtml = String.Empty

        HiddenTysKensakuType.Value = EarthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI

        If IsPostBack = False Then

            '�������̃`�F�b�N
            '�o���Ɩ�����
            If userinfo.KeiriGyoumuKengen = 0 Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()

            '�t�H�[�J�X�ݒ�
            TextSiharaisakiCd.Focus()

            '****************************************************************************
            'Request�f�[�^�擾
            '****************************************************************************
            TextSiharaisakiCd.Value = Request("shrCd")
            TextNengappiFrom.Value = Request("fromDate")
            TextNengappiTo.Value = Request("toDate")

            If TextSiharaisakiCd.Value <> String.Empty And _
               TextNengappiFrom.Value <> String.Empty And _
               TextNengappiTo.Value <> String.Empty Then
                '�S�Ă̏��������N�G�X�g�f�[�^����擾�o�����ꍇ�A�f�[�^�擾�������������s
                ButtonHiddenDisplay_ServerClick(sender, e)
            Else
                Dim dteNow As DateTime = DateTime.Now
                Dim dteTermFirst As DateTime

                '�N�x�n�ߓ��t
                dteTermFirst = cl.GetTermFirstDate(dteNow)
                TextNengappiFrom.Value = dteTermFirst.ToString(EarthConst.FORMAT_DATE_TIME_9)
                '�V�X�e�����t
                TextNengappiTo.Value = dteNow.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        End If

    End Sub

#Region "�{�^���C�x���g"
    ''' <summary>
    ''' �x���挟���{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnShriSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim lgcSiireSearch As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)
        Dim total_count As Integer = 0
        Dim tmpScript As String = String.Empty
        Dim strSiharaiCd As String = String.Empty
        Dim strSiharaiBrc As String = String.Empty

        '�x���挟�������s
        If Me.TextSiharaisakiCd.Value <> String.Empty Then
            dataArray = lgcSiireSearch.GetTyousakaisyaSearchResult(TextSiharaisakiCd.Value, _
                                                                   String.Empty, _
                                                                   String.Empty, _
                                                                   String.Empty, _
                                                                   False, _
                                                                   HiddenKameitenCd.Value, _
                                                                   HiddenTysKensakuType.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            Me.TextSiharaisakiCd.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            Me.TextShriSakiMei.Value = recData.SeikyuuSakiSiharaiMei
            '�t�@�N�^�����O�J�n�N�����擾
            Dim DLogic As New DataLogic
            TdFactaringStNengetu.InnerHtml = DLogic.dtTime2Str(recData.FctringKaisiNengetu, EarthConst.FORMAT_DATE_TIME_8)

            '�t�H�[�J�X�Z�b�g
            masterAjaxSM.SetFocus(btnShriSakiSearch)

        Else
            '�x���於���N���A
            Me.TextShriSakiMei.Value = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & TextSiharaisakiCd.ClientID & EarthConst.SEP_STRING & _
                                         HiddenKameitenCd.ClientID & EarthConst.SEP_STRING & _
                                         HiddenTysKensakuType.ClientID & "','" _
                                       & UrlConst.SEARCH_TYOUSAKAISYA & "','" _
                                       & TextSiharaisakiCd.ClientID & EarthConst.SEP_STRING & _
                                         HiddenKakusyuNG.ClientID & "','" _
                                       & btnShriSakiSearch.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �������s�{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenDisplay_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenDisplay.ServerClick
        Try
            '�{�^���Ƀt�H�[�J�X
            If HiddenCsvOutPut.Value = String.Empty Then
                Me.ButtonDisplay.Focus()
            Else
                Me.ButtonCsv.Focus()
            End If

            '���͒l�`�F�b�N
            If Not checkInput(sender) Then
                Exit Sub
            End If

            '�������ʂ���ʂɕ\��
            SetSearchResult(sender, e)

        Catch ex As Exception
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "�\��") & "\r\n" & ex.Message, 0, "kensakuErr")

        End Try

    End Sub

    ''' <summary>
    ''' CSV�o�̓{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenCsv.ServerClick

        Try
            '�{�^���Ƀt�H�[�J�X
            Me.ButtonCsv.Focus()

            '���͒l�`�F�b�N
            If Not checkInput(sender) Then
                Exit Sub
            End If

            '�������ʂ���ʂɕ\��
            Dim dtList As List(Of SiharaiSakiMototyouRecord) = SetSearchResult(sender, e)

            If dtList.Count = 0 Then
                ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
                MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
                Exit Sub
            ElseIf dtList.Count = -1 Then
                ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
                MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV�o��"), 0, "kensakuErr")
                Exit Sub
            End If

            '�f�[�^���R�[�h��List�̓��e���ACSV�o�͗p�ɕ�����
            Dim csvString As String = recListToString(dtList)

            '�o�̓t�@�C����
            Dim strFileNm As String = "�x���挳���f�[�^.csv"

            'HTTP���X�|���X�I�u�W�F�N�g(�J�����g)
            Dim httpRes As HttpResponse = HttpContext.Current.Response

            '�t�@�C���̏o�͂��s��
            With httpRes
                .Clear()
                .AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileNm))
                .ContentType = "text/plain"
                .BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(csvString))
                .End()
            End With

        Catch ex As Exception
            HiddenCsvOutPut.Value = String.Empty
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV�o��") & "\r\n" & ex.Message, 0, "kensakuErr")

        End Try


    End Sub

    ''' <summary>
    ''' ����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenPrint_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenPrint.ServerClick
        Try
            '���͒l�`�F�b�N
            If Not checkInput(sender) Then
                Me.ButtonPrint.Focus()
                Exit Sub
            End If

            '�������ʂ���ʂɕ\��
            Dim dtList As List(Of SiharaiSakiMototyouRecord) = SetSearchResult(sender, e)

            If dtList.Count = 0 Then
                ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
                MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
                Me.ButtonPrint.Focus()
                Exit Sub
            ElseIf dtList.Count = -1 Then
                ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
                MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "���"), 0, "kensakuErr")
                Me.ButtonPrint.Focus()
                Exit Sub
            End If

            'PDF�v���r���[�Ăяo���pGET�p�����[�^�ݒ�
            Dim shrCd As String = TextSiharaisakiCd.Value.Substring(0, TextSiharaisakiCd.Value.Length - 2)
            Dim jigCd As String = TextSiharaisakiCd.Value.Substring(TextSiharaisakiCd.Value.Length - 2, 2)

            Dim tmpParam As String = "{0}&{1}&{2}&{3}&{4}"
            tmpParam = String.Format(tmpParam, "shrCd=" & HttpUtility.UrlEncode(shrCd), _
                                               "jigCd=" & HttpUtility.UrlEncode(jigCd), _
                                               "shrNm=" & HttpUtility.UrlEncode(TextShriSakiMei.Value), _
                                               "fromDate=" & HttpUtility.UrlEncode(TextNengappiFrom.Value), _
                                               "toDate=" & HttpUtility.UrlEncode(TextNengappiTo.Value))
            'PDF�v���r���[�Ăяo���p�X�N���v�g
            Dim tmpScript As String = "document.getElementById('" & ButtonPrint.ClientID & "').focus();window.open('" & UrlConst.EARTH2_SIHARAISAKI_MOTOTYOU_OUTPUT & "?" & tmpParam & "');"
            ScriptManager.RegisterStartupScript(sender, sender.GetType(), "callprint", tmpScript, True)

        Catch ex As Exception
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "���") & "\r\n" & ex.Message, 0, "kensakuErr")
            Me.ButtonPrint.Focus()

        End Try
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
        ButtonDisplay.Attributes("onclick") = "checkJikkou('0');"
        'CSV�o�̓{�^���̃C�x���g�n���h����ݒ�
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou('1');"
        'CSV�o�̓{�^���̃C�x���g�n���h����ݒ�
        Me.ButtonPrint.Attributes("onclick") = "checkJikkou('2');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�}�X�^�����n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSiharaisakiCd.Attributes("onchange") = "objEBI('" & Me.TextShriSakiMei.ClientID & "').value='';"

    End Sub

    ''' <summary>
    ''' ���͒l�`�F�b�N
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function checkInput(ByVal sender As System.Object)

        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        '�x���於���擾
        Dim lgcSiireSearch As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)
        dataArray = lgcSiireSearch.GetTyousakaisyaSearchResult(TextSiharaisakiCd.Value, String.Empty, String.Empty, String.Empty, False, HiddenKameitenCd.Value, HiddenTysKensakuType.Value)
        If dataArray.Count = 1 Then
            '���݂̉�ʏ�ԂŁA�����悪�P���̂ݎ擾�ł���ꍇ�A�����Ŗ��̂���ʂɃZ�b�g
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            TextSiharaisakiCd.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            TextShriSakiMei.Value = recData.SeikyuuSakiSiharaiMei
            '�t�@�N�^�����O�J�n�N�����擾
            Dim DLogic As New DataLogic
            TdFactaringStNengetu.InnerHtml = DLogic.dtTime2Str(recData.FctringKaisiNengetu, EarthConst.FORMAT_DATE_TIME_8)
        End If

        '�K�{�`�F�b�N
        If String.IsNullOrEmpty(TextSiharaisakiCd.Value) AndAlso TextSiharaisakiCd.Value.Length >= 6 Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "�x����")
            arrFocusTargetCtrl.Add(TextSiharaisakiCd)
        End If
        If String.IsNullOrEmpty(TextNengappiFrom.Value) OrElse String.IsNullOrEmpty(TextNengappiTo.Value) Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "�N����")
            arrFocusTargetCtrl.Add(TextNengappiFrom)
        End If

        '���t�`�F�b�N
        '���tFrom
        If TextNengappiFrom.Value <> String.Empty Then
            If Not CLogic.checkDateHanni(TextNengappiFrom.Value) Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�N����(FROM)")
                arrFocusTargetCtrl.Add(TextNengappiFrom)
            End If
        End If
        '���tTo
        If TextNengappiTo.Value <> String.Empty Then
            If Not CLogic.checkDateHanni(TextNengappiTo.Value) Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�N����(TO)")
                arrFocusTargetCtrl.Add(TextNengappiTo)
            End If
        End If
        '���tFrom�E�ߋ��f�[�^�`�F�b�N
        If TextNengappiFrom.Value <> String.Empty Then
            If TextNengappiFrom.Value < EarthConst.KEIRI_DATA_MIN_DATE Then
                errMess += Messages.MSG179W
                arrFocusTargetCtrl.Add(TextNengappiFrom)
            End If
        End If
        '���tFrom-To�`�F�b�N
        If TextNengappiFrom.Value <> String.Empty And TextNengappiTo.Value <> String.Empty Then
            If TextNengappiFrom.Value > TextNengappiTo.Value Then
                errMess += Messages.MSG022E.Replace("@PARAM1", "�N����")
                arrFocusTargetCtrl.Add(TextNengappiFrom)
            End If
        End If

        '�G���[�������̓��b�Z�[�W�\��
        If errMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            MLogic.AlertMessage(sender, errMess, 0, "inputerror")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' ���������Ō����������e���������ʃe�[�u���ɕ\������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Function SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs) As List(Of SiharaiSakiMototyouRecord)
        Const CSS_TEXT_CENTER = "textCenter"
        Const CSS_KINGAKU = "kingaku"

        Dim listCount As Integer = 0

        '���W�b�N�N���X�̐���
        Dim logic As New MototyouLogic

        '���|���f�[�^�e�[�u���̎擾
        Dim kaikakeData As KaikakeDataRecord = logic.GetKaikakeDataNewest(TextSiharaisakiCd.Value)

        '��ʍ��ڂɒl���Z�b�g
        setTdStr(TdSaisinKurikosiDate, kaikakeData.TaisyouNengetu)
        setTdStr(TdTourokuZandaka, kaikakeData.TougetuKurikosiZan, EarthConst.FORMAT_KINGAKU_1)

        '�J�z�c�����擾
        Dim kurikosiZan As Long = logic.GetSiharaiSakiMototyouKurikosiZan(TextSiharaisakiCd.Value, _
                                                                          TextNengappiFrom.Value)
        Dim kurikosiRec As New SiharaiSakiMototyouRecord
        kurikosiRec.Kamoku = "�J�z�c��"
        kurikosiRec.Zandaka = kurikosiZan

        '�f�[�^�s�擾���s
        Dim dataList As List(Of SiharaiSakiMototyouRecord) = _
                                    logic.GetSiharaiSakiMototyouDenpyouData(TextSiharaisakiCd.Value, _
                                                                            TextNengappiFrom.Value, _
                                                                            TextNengappiTo.Value)

        '���ʌ����Z�b�g
        listCount = dataList.Count

        '�擾���ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If listCount = 0 Then
            '�擾���ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            Me.HiddenCsvOutPut.Value = ""
            Return dataList
        ElseIf listCount = -1 Then
            '�擾���ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Return dataList
        End If

        '���v�s���쐬
        Dim goukeiRec As New SiharaiSakiMototyouRecord
        goukeiRec.Kamoku = "���v"
        goukeiRec.Hinmei = "���ԍ��v"

        '�擪�s�A�ŏI�s�ɂ��ꂼ��J�z�A���v�s���R�[�h���Z�b�g
        dataList.Insert(0, kurikosiRec)
        dataList.Insert(dataList.Count, goukeiRec)

        '�������ʌ�����ݒ�
        TdResultCount.InnerHtml = listCount

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


        '�擾�����`�[�f�[�^����ʂɕ\��
        Dim tmpGoukei As Long = 0               '���v�s�p�̍��v���z��������
        Dim tmpZandaka As Long = kurikosiZan    '�c�����ڂ�������(�J�z�c�����Z�b�g)
        Dim tmpDenpyouNo As String = String.Empty
        For Each rec As SiharaiSakiMototyouRecord In dataList

            rowCnt += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden

            Dim objTdNengappi As New HtmlTableCell              '�N����
            Dim objTdKamoku As New HtmlTableCell                '�Ȗ�
            Dim objTdSyouhinCd As New HtmlTableCell             '���i�R�[�h
            Dim objTdHinmei As New HtmlTableCell                '���i��/�x����ʂȂ�
            Dim objTdKokyakuNo As New HtmlTableCell             '�ڋq�ԍ�
            Dim objTdBukkenMei As New HtmlTableCell             '������/�E�v�Ȃ�
            Dim objTdSuu As New HtmlTableCell                   '����
            Dim objTdTanka As New HtmlTableCell                 '�P��
            Dim objTdZeinukiGaku As New HtmlTableCell           '�Ŕ����z
            Dim objTdSotozeiGaku As New HtmlTableCell           '�����
            Dim objTdKingaku As New HtmlTableCell               '���z
            Dim objTdZandaka As New HtmlTableCell               '�c��
            Dim objTdDenpyouNo As New HtmlTableCell             '�`�[�ԍ�

            '�c���A���v�z�v�Z�̒~�ςƁA�x���`�[�ԍ��̈ꎞ�ێ�
            If rec.Kamoku = "�d��" Then
                '�d�����A�c�������Z
                tmpZandaka += IIf(rec.Kingaku = Long.MinValue, 0, rec.Kingaku)
                '�d�����A���v�z�����Z
                tmpGoukei += IIf(rec.Kingaku = Long.MinValue, 0, rec.Kingaku)
                '�ꎞ�ێ��`�[�ԍ��̍X�V
                tmpDenpyouNo = String.Empty
            ElseIf rec.Kamoku = "�x��" Then
                '�d���ȊO(�x��)���A�c�������Z
                tmpZandaka -= IIf(rec.Kingaku = Long.MinValue, 0, rec.Kingaku)
                '�ꎞ�ێ��`�[�ԍ��̍X�V
                If tmpDenpyouNo = rec.DenpyouNo Then
                    '���O�s�Ɠ��l�̏ꍇ�A��ʕ\���ɂ͓`�[�ԍ���\�����Ȃ�
                    rec.DenpyouNo = String.Empty
                Else
                    '�`�[�ԍ����ς���Ă����ꍇ�A�ꎞ�ێ��l���ύX
                    tmpDenpyouNo = rec.DenpyouNo
                End If
            ElseIf rec.Kamoku = "���v" Then
                '���v�s�̏ꍇ�A����܂ł̎c���A���v�z���Z�b�g
                rec.Kingaku = tmpGoukei
            End If
            '�c�������R�[�h�ɃZ�b�g
            rec.Zandaka = tmpZandaka

            '�������ʔz�񂩂�Z���Ɋi�[
            setTdStr(objTdNengappi, rec.Nengappi)
            setTdStr(objTdKamoku, rec.Kamoku)
            setTdStr(objTdSyouhinCd, rec.SyouhinCd)
            setTdStr(objTdHinmei, rec.Hinmei)
            setTdStr(objTdKokyakuNo, rec.KokyakuNo)
            setTdStr(objTdBukkenMei, rec.BukkenMei)
            setTdStr(objTdSuu, rec.Suu, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdTanka, rec.Tanka, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdZeinukiGaku, rec.ZeinukiGaku, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdSotozeiGaku, rec.SotozeiGaku, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdKingaku, rec.Kingaku, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdZandaka, rec.Zandaka, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdDenpyouNo, rec.DenpyouNo)

            '�e�Z���̕��ݒ�()
            If rowCnt = 1 Then
                objTdNengappi.Style("width") = widthList1(0)
                objTdKamoku.Style("width") = widthList1(1)
                objTdSyouhinCd.Style("width") = widthList1(2)
                objTdHinmei.Style("width") = widthList1(3)
                objTdKokyakuNo.Style("width") = widthList1(4)
                objTdBukkenMei.Style("width") = widthList1(5)
                objTdSuu.Style("width") = widthList1(6)

                objTdTanka.Style("width") = widthList2(0)
                objTdZeinukiGaku.Style("width") = widthList2(1)
                objTdSotozeiGaku.Style("width") = widthList2(2)
                objTdKingaku.Style("width") = widthList2(3)
                objTdZandaka.Style("width") = widthList2(4)
                objTdDenpyouNo.Style("width") = widthList2(5)
            End If

            '�X�^�C���A�N���X�ݒ�
            objTdNengappi.Attributes("class") = CSS_TEXT_CENTER
            objTdKamoku.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinCd.Attributes("class") = CSS_TEXT_CENTER
            objTdKokyakuNo.Attributes("class") = CSS_TEXT_CENTER
            objTdSuu.Attributes("class") = CSS_KINGAKU
            objTdTanka.Attributes("class") = CSS_KINGAKU
            objTdZeinukiGaku.Attributes("class") = CSS_KINGAKU
            objTdSotozeiGaku.Attributes("class") = CSS_KINGAKU
            objTdKingaku.Attributes("class") = CSS_KINGAKU
            objTdZandaka.Attributes("class") = CSS_KINGAKU
            objTdDenpyouNo.Attributes("class") = CSS_TEXT_CENTER

            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            '�s�ɃZ�����Z�b�g1
            With objTr1.Controls
                .Add(objTdNengappi)
                .Add(objTdKamoku)
                .Add(objTdSyouhinCd)
                .Add(objTdHinmei)
                .Add(objTdKokyakuNo)
                .Add(objTdBukkenMei)
                .Add(objTdSuu)
            End With

            objTr2.ID = "DataTable_resultTr2_" & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2�s�ڈȍ~�̓^�u�ړ��Ȃ�
                objTr2.Attributes("tabindex") = -1
            End If

            '�s�ɃZ���ƃZ�b�g2
            With objTr2.Controls
                .Add(objTdTanka)
                .Add(objTdZeinukiGaku)
                .Add(objTdSotozeiGaku)
                .Add(objTdKingaku)
                .Add(objTdZandaka)
                .Add(objTdDenpyouNo)
            End With

            '�e�[�u���ɍs���Z�b�g
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next

        '���X�g��߂�
        Return dataList

    End Function

#Region "���R�[�h�̓��e���o�͗p�Ƀe�L�X�g��"
    Private Function recListToString(ByVal DataList As List(Of SiharaiSakiMototyouRecord)) As String
        Dim sb As New StringBuilder
        Dim siharaiSakiCd As String = TextSiharaisakiCd.Value
        Dim siharaiSakiMei As String = TextShriSakiMei.Value

        '�^�C�g���s���Z�b�g
        setCsvString(sb, "�x����敪")
        setCsvString(sb, "�x���於")
        setCsvString(sb, "�N����")
        setCsvString(sb, "�Ȗ�")
        setCsvString(sb, "���i�R�[�h")
        setCsvString(sb, "���i��/�x����ʂȂ�")
        setCsvString(sb, "�ڋq�ԍ�")
        setCsvString(sb, "������/�E�v�Ȃ�")
        setCsvString(sb, "����")
        setCsvString(sb, "�P��")
        setCsvString(sb, "�Ŕ����z")
        setCsvString(sb, "�����")
        setCsvString(sb, "���z")
        setCsvString(sb, "�c��")
        setCsvString(sb, "�`�[�ԍ�", True)

        'List�����[�v
        For Each rec As SiharaiSakiMototyouRecord In DataList

            'CSV�o�͗p��StringBuilder�ɒl���Z�b�g����
            setCsvString(sb, siharaiSakiCd)
            setCsvString(sb, siharaiSakiMei)
            setCsvString(sb, rec.Nengappi)
            setCsvString(sb, rec.Kamoku)
            setCsvString(sb, rec.SyouhinCd)
            setCsvString(sb, rec.Hinmei)
            setCsvString(sb, rec.KokyakuNo)
            setCsvString(sb, rec.BukkenMei)
            setCsvString(sb, rec.Suu)
            setCsvString(sb, rec.Tanka)
            setCsvString(sb, rec.ZeinukiGaku)
            setCsvString(sb, rec.SotozeiGaku)
            setCsvString(sb, rec.Kingaku)
            setCsvString(sb, rec.Zandaka)
            setCsvString(sb, rec.DenpyouNo, True)

        Next

        Return sb.ToString
    End Function


#End Region

#End Region


#Region "���[�J���֐�"

    ''' <summary>
    ''' TD�ɓK����������l���Z�b�g
    ''' </summary>
    ''' <param name="td"></param>
    ''' <param name="val"></param>
    ''' <param name="formatStr"></param>
    ''' <param name="defStr"></param>
    ''' <remarks></remarks>
    Private Sub setTdStr(ByRef td As HtmlTableCell, _
                         ByVal val As Object, _
                         Optional ByVal formatStr As String = "", _
                         Optional ByVal defStr As String = EarthConst.HANKAKU_SPACE)

        Dim retStr As String = String.Empty

        'GetDisplayString�ŕ\��������
        retStr = CLogic.GetDisplayString(val, defStr)

        '�t�H�[�}�b�g�X�^�C�����w�肳��Ă���AGetDisplayString�̌��ʂ���l�łȂ��ꍇ�A
        '���̒l���當����t�H�[�}�b�g���s��
        If Not String.IsNullOrEmpty(formatStr) AndAlso retStr <> defStr Then
            retStr = Format(val, formatStr)
        End If

        'TD�ɃZ�b�g
        td.InnerHtml = retStr

    End Sub

    ''' <summary>
    ''' CSV�o�͗p��StringBuilder�ɒl���Z�b�g����
    ''' </summary>
    ''' <param name="sb"></param>
    ''' <param name="valObj"></param>
    ''' <param name="flgEndCol"></param>
    ''' <remarks></remarks>
    Private Sub setCsvString(ByRef sb As StringBuilder, ByVal valObj As Object, Optional ByVal flgEndCol As Boolean = False)

        sb.AppendFormat(strCsvQuote & "{0}" & strCsvQuote, CLogic.GetDisplayString(valObj, String.Empty))

        '�ŏI�J�����̏ꍇ�A�f���~�^�ł͂Ȃ����s���Z�b�g����
        If flgEndCol Then
            sb.Append(vbCrLf)
        Else
            sb.Append(strCsvDelimiter)
        End If

    End Sub


#End Region

End Class