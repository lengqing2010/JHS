Partial Public Class SearchSeikyuusyoSimeDateRireki
    Inherits System.Web.UI.Page

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '���ʃ��W�b�N
    Private cl As New CommonLogic
    '�����������������W�b�N�N���X
    Dim MyLogic As New SeikyuuSimeDateRerekiSearchLogic

#Region "�R���g���[���l"
    Private Const CTRL_NAME_TR1 As String = "DataTable_resultTr1_"
    Private Const CTRL_NAME_TR2 As String = "DataTable_resultTr2_"
    Private Const CTRL_NAME_TD_SENTOU As String = "DataTable_Sentou_Td_"
    Private Const CTRL_NAME_CHECK_TAISYOU As String = "ChkTaisyou_"
    Private Const CTRL_NAME_HIDDEN_UPDDATETIME As String = "HdnUpdDatetime_"
    Private Const CTRL_NAME_HIDDEN_KAGAMI_UPDDATETIME As String = "HdnKagamiUpdDatetime_"
#End Region

#Region "CSS�N���X��"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_KINGAKU = "kingaku"
    Private Const CSS_DATE = "date"
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

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()

            '�{�^�������C�x���g�̐ݒ�
            setBtnEvent()

            '�t�H�[�J�X�ݒ�
            Me.SelectSeikyuuSakiKbn.Focus()

        End If

    End Sub

#Region "�{�^���C�x���g"

    ''' <summary>
    ''' �����挟���{�^���������̏���
    ''' </summary>
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
    ''' �߂�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonReturn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReturn.ServerClick

        '�������f�[�^�쐬��ʂ֖߂�
        Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)

    End Sub

    ''' <summary>
    ''' ��������{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonRirekiTorikesi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRirekiTorikesi.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonRirekiTorikesi

        ' ���̓`�F�b�N
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If Me.SaveData() Then '�o�^����

            tmpScript = "gNoDataMsgFlg = '1';"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonRirekiTorikesi_ServerClick", tmpScript, True)

            Me.SetCmnSearchResult(sender, e)

        Else '�o�^���s

            Me.SetCmnSearchResult(sender, e)

            setFocusAJ(Me.btnSearch) '�t�H�[�J�X

            tmpScript = "alert('" & Messages.MSG147E.Replace("@PARAM1", "�������") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonRirekiTorikesi_ServerClick", tmpScript, True)
            Exit Sub
        End If
    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"

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
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �{�^���̃C�x���g�n���h����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�������s�{�^��
        Me.btnSearch.Attributes("onclick") = "checkJikkou(0);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������NO�̃C�x���g�n���h����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuusyoNoFrom.Attributes("onblur") = "if(checkNumber(this))setFromTo(this);"
        Me.TextSeikyuusyoNoTo.Attributes("onblur") = "if(checkNumber(this))this.value = paddingStr(this.value,15,'0');"
       
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�}�X�^�����n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrName(this,'" & TextSeikyuuSakiMei.ClientID & "');"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrName(this,'" & TextSeikyuuSakiMei.ClientID & "');"
        Me.SelectSeikyuuSakiKbn.Attributes("onblur") = "clrName(this,'" & TextSeikyuuSakiMei.ClientID & "');"

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
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG197C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(" & strChkTaisyou & " && " & strChkJikkou & "){" & tmpTouroku & "}else{return false;}"

        '�m�FMSG��AOK�̏ꍇ�㑱�������s�Ȃ�
        Me.ButtonRirekiTorikesi.Attributes("onclick") = tmpScript.Replace("@PARAM1", EarthEnum.emSearchSeikyuuSimeDateRirekiBtnType.Torikesi)

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ��猟���L�[���R�[�h�ւ̒l�Z�b�g
    ''' </summary>
    ''' <param name="keyRec">���o���R�[�h�N���X�̃L�[</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef keyRec As SeikyuuSimeDateRirekiKeyRecord)
        Dim SeikyuusyoHakkouNengetuFrom As String = ""
        Dim SeikyuusyoHakkouNengetuTo As String = ""

        '������敪
        keyRec.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuSakiKbn.SelectedValue <> String.Empty, Me.SelectSeikyuuSakiKbn.SelectedValue, String.Empty)
        '������R�[�h
        keyRec.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> String.Empty, Me.TextSeikyuuSakiCd.Value, String.Empty)
        '������}��
        keyRec.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> String.Empty, Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '������J�i��
        keyRec.SeikyuuSakiMeiKana = IIf(Me.TextSeikyuuSakiMeiKana.Value <> String.Empty, Me.TextSeikyuuSakiMeiKana.Value, String.Empty)
        '�����N����_FROM
        keyRec.SeikyuusyoHakDateFrom = IIf(Me.TextSeikyuusyoHakkouDateFrom.Value <> "", Me.TextSeikyuusyoHakkouDateFrom.Value, DateTime.MinValue)
        '�����N����_TO
        keyRec.SeikyuusyoHakDateTo = IIf(Me.TextSeikyuusyoHakkouDateTo.Value <> "", Me.TextSeikyuusyoHakkouDateTo.Value, DateTime.MinValue)
        '������No_FROM
        keyRec.SeikyuusyoNoFrom = IIf(Me.TextSeikyuusyoNoFrom.Value <> String.Empty, Me.TextSeikyuusyoNoFrom.Value, String.Empty)
        '������No_TO
        keyRec.SeikyuusyoNoTo = IIf(Me.TextSeikyuusyoNoTo.Value <> String.Empty, Me.TextSeikyuusyoNoTo.Value, String.Empty)
        '���
        keyRec.Torikesi = IIf(Me.CheckTorikesiTaisyou.Checked, 0, Integer.MinValue)
        '�ŐV�������ߓ��̂ݕ\��
        keyRec.NewRirekiDisp = IIf(Me.CheckSaisinSeikyuuSimeDate.Checked, CheckSaisinSeikyuuSimeDate.Value, Integer.MinValue)

    End Sub

    ''' <summary>
    ''' �������s������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCmnSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim keyRec As New SeikyuuSimeDateRirekiKeyRecord

        '����������ݒ�
        Me.SetSearchKeyFromCtrl(keyRec)

        '�������ʂ���ʂɕ\��
        Me.SetSearchResult(sender, e, keyRec)

    End Sub

    ''' <summary>
    ''' ���������Ō����������e���������ʃe�[�u���ɕ\��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal keyRec As SeikyuuSimeDateRirekiKeyRecord)

        '�\���ő匏��
        Dim end_count As Integer = Integer.Parse(maxSearchCount.Value)
        Dim total_count As Integer = 0 '�擾����

        '�������s
        Dim resultArray As New List(Of SeikyuuSimeDateRirekiRecord)

        '�Ώۃ`�F�b�N�{�b�N�X�̏�����
        Me.CheckAll.Checked = False

        resultArray = MyLogic.GetSeikyuuSimeDateRirekiDataInfo(sender, keyRec, 1, end_count, total_count)

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
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
        End If

        '�������ʌ�����ݒ�
        resultCount.InnerHtml = displayCount

        '************************
        '* ��ʃe�[�u���֏o��
        '************************

        '�s�J�E���^
        Dim rowCnt As Integer = 0
        Dim strSpace As String = EarthConst.HANKAKU_SPACE

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

        Dim objTdReturn As New HtmlTableCell

        'Hidden
        Dim objHdnSimeDateRirekiPk As HtmlInputHidden   'PK5���i�[
        Dim objHdnUpdDatetime As HtmlInputHidden        '�X�V����
        Dim objHdnKagamiUpdDatetime As HtmlInputHidden  '�����ӍX�V����

        'CheckBox 
        Dim objChkTaisyou As HtmlInputCheckBox          '�Ώ�

        'Table Cell
        Dim objTdTaisyou As HtmlTableCell               '�Ώ�
        Dim objTdRirekiNo As HtmlTableCell              '����NO
        Dim objTdTorikesi As HtmlTableCell              '���
        Dim objTdSeikyuuSakiCd As HtmlTableCell         '������R�[�h
        Dim objTdSeikyuuSakiMei As HtmlTableCell        '�����於
        Dim objTdSeikyuuSakiMei2 As HtmlTableCell       '�����於2
        Dim objTdSeikyuusyoHakDate As HtmlTableCell     '���������s��
        Dim objTdKonkaiGoseikyuuGaku As HtmlTableCell   '�������z
        Dim objTdSeikyuusyoNo As HtmlTableCell          '������NO
        Dim objTdZenTaisyouFlg As HtmlTableCell         '�S�Ώۃt���O

        Dim SimeDateRirekiPK As String = ""             '���������Ώۃ`�F�b�N�{�b�N�X��PK���i�[

        '�擾�����f�[�^����ʂɕ\��
        For Each data As SeikyuuSimeDateRirekiRecord In resultArray
            rowCnt += 1

            'Tr
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            '***********
            '* Table1
            '********
            'Hidden
            objHdnSimeDateRirekiPk = New HtmlInputHidden
            objHdnUpdDatetime = New HtmlInputHidden         'Hidden�X�V����
            objHdnKagamiUpdDatetime = New HtmlInputHidden   'Hidden�����ӍX�V����

            'CheckBox 
            objChkTaisyou = New HtmlInputCheckBox           '�Ώۃ`�F�b�N�{�b�N�X

            'Table Cell
            objTdTaisyou = New HtmlTableCell               '�Ώ�
            objTdRirekiNo = New HtmlTableCell              '����NO
            objTdTorikesi = New HtmlTableCell              '���
            objTdSeikyuuSakiCd = New HtmlTableCell         '������R�[�h

            '***********
            '* Table2
            '********
            objTdSeikyuuSakiMei = New HtmlTableCell        '�����於
            objTdSeikyuuSakiMei2 = New HtmlTableCell       '�����於2
            objTdSeikyuusyoHakDate = New HtmlTableCell     '���������s��
            objTdKonkaiGoseikyuuGaku = New HtmlTableCell   '�������z
            objTdSeikyuusyoNo = New HtmlTableCell          '������NO
            objTdZenTaisyouFlg = New HtmlTableCell         '�S�Ώۃt���O

            '�������ʔz�񂩂�Z���Ɋi�[
            objTdTaisyou.ID = CTRL_NAME_TD_SENTOU & rowCnt

            '******************
            '* �擪��
            '*************
            If data.MaxRirekiNo = 1 Then
                'Hidden���������ߓ�����PK
                objHdnSimeDateRirekiPk.ID = "returnHidden" & rowCnt
                With objHdnSimeDateRirekiPk
                    .Value &= data.SeikyuuSakiCd & EarthConst.SEP_STRING
                    .Value &= data.SeikyuuSakiBrc & EarthConst.SEP_STRING
                    .Value &= data.SeikyuuSakiKbn & EarthConst.SEP_STRING
                    .Value &= data.SeikyuusyoHakNengetu & EarthConst.SEP_STRING
                    .Value &= data.SeikyuuSimeDate
                End With
                objTdTaisyou.Controls.Add(objHdnSimeDateRirekiPk)
                objTdTaisyou.Attributes("class") = "searchReturnValues"
                SimeDateRirekiPK = objHdnSimeDateRirekiPk.Value

                'Hidden�X�V����
                objHdnUpdDatetime.ID = CTRL_NAME_HIDDEN_UPDDATETIME & rowCnt
                objHdnUpdDatetime.Value = IIf(data.UpdDatetime = DateTime.MinValue, Format(data.AddDatetime, EarthConst.FORMAT_DATE_TIME_1), Format(data.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
                objTdTaisyou.Controls.Add(objHdnUpdDatetime)

                'Hidden�����ӍX�V����
                objHdnKagamiUpdDatetime.ID = CTRL_NAME_HIDDEN_KAGAMI_UPDDATETIME & rowCnt
                objHdnKagamiUpdDatetime.Value = Format(data.SkUpdDatetime, EarthConst.FORMAT_DATE_TIME_1)
                objTdTaisyou.Controls.Add(objHdnKagamiUpdDatetime)

                '�Ώۃ`�F�b�N�{�b�N�X(�߂�v�f(hidden)�́A�K���擪��̐擪��Input�^�O�ɃZ�b�g���邱��)
                objChkTaisyou.ID = CTRL_NAME_CHECK_TAISYOU & SimeDateRirekiPK
                objChkTaisyou.Attributes("tabindex") = Integer.Parse(Me.CheckTorikesiTaisyou.Attributes("tabindex")) + intTrTabIndex + rowCnt
                objTdTaisyou.Controls.Add(objChkTaisyou)
            Else
                objTdTaisyou.InnerHtml = strSpace
            End If
            '******************

            objTdRirekiNo.InnerHtml = cl.GetDisplayString(data.RirekiNo, strSpace)

            '�����0�ȊO�̏ꍇ�́A�������ʗ��Ɂu����v�ƕ\��
            If data.Torikesi = 0 Then
                objTdTorikesi.InnerHtml = strSpace
            Else
                objTdTorikesi.InnerHtml = EarthConst.TORIKESI
            End If

            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(data.SeikyuuSakiKbn, data.SeikyuuSakiCd, data.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei, strSpace)
            objTdSeikyuuSakiMei2.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei2, strSpace)
            objTdSeikyuusyoHakDate.InnerHtml = cl.GetDisplayString(data.SeikyuusyoHakDate, strSpace)
            objTdKonkaiGoseikyuuGaku.InnerHtml = cl.GetDisplayString(Format(data.KonkaiGoseikyuuGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdSeikyuusyoNo.InnerHtml = cl.GetDisplayString(data.SeikyuusyoNo, strSpace)

            '�S�Ώۃt���O��1�̏ꍇ�́A�������ʗ��Ɂu����v�ƕ\���B�S�Ώۃt���O��0�̏ꍇ�́A�������ʗ��Ɂu�Ȃ��v�ƕ\���B
            If data.ZenTaisyouFlg = 1 Then
                objTdZenTaisyouFlg.InnerHtml = EarthConst.ARI_HIRAGANA
            ElseIf data.ZenTaisyouFlg = 0 Then
                objTdZenTaisyouFlg.InnerHtml = EarthConst.NASI_HIRAGANA
            Else
                objTdZenTaisyouFlg.InnerHtml = strSpace
            End If

            '�e�Z���̕��ݒ�
            If rowCnt = 1 Then
                objTdTaisyou.Style("width") = widthList1(0)
                objTdRirekiNo.Style("width") = widthList1(1)
                objTdTorikesi.Style("width") = widthList1(2)
                objTdSeikyuuSakiCd.Style("width") = widthList1(3)

                objTdSeikyuuSakiMei.Style("width") = widthList2(0)
                objTdSeikyuuSakiMei2.Style("width") = widthList2(1)
                objTdSeikyuusyoHakDate.Style("width") = widthList2(2)
                objTdKonkaiGoseikyuuGaku.Style("width") = widthList2(3)
                objTdSeikyuusyoNo.Style("width") = widthList2(4)
                objTdZenTaisyouFlg.Style("width") = widthList2(5)
            End If

            '�X�^�C���A�N���X�ݒ�
            objTdTaisyou.Attributes("class") = CSS_TEXT_CENTER
            objTdRirekiNo.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuusyoNo.Attributes("class") = CSS_TEXT_CENTER
            objTdTorikesi.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSakiCd.Attributes("class") = CSS_TEXT_CENTER

            objTdSeikyuuSakiMei.Attributes("class") = ""
            objTdSeikyuuSakiMei2.Attributes("class") = ""
            objTdKonkaiGoseikyuuGaku.Attributes("class") = CSS_KINGAKU
            objTdSeikyuusyoHakDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdZenTaisyouFlg.Attributes("class") = CSS_TEXT_CENTER

            '�sID��JS�C�x���g�̕t�^
            objTr1.ID = CTRL_NAME_TR1 & rowCnt
            objTr1.Attributes("tabindex") = -1

            '�s�ɃZ�����Z�b�g1
            With objTr1.Controls
                .Add(objTdTaisyou) '�߂�v�f(hidden)�́A�K���擪��̐擪��Input�^�O�ɃZ�b�g���邱��
                .Add(objTdRirekiNo)
                .Add(objTdTorikesi)
                .Add(objTdSeikyuuSakiCd)
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

            '�s�ɃZ�����Z�b�g2()
            With objTr2.Controls
                .Add(objTdSeikyuuSakiMei)
                .Add(objTdSeikyuuSakiMei2)
                .Add(objTdSeikyuusyoHakDate)
                .Add(objTdKonkaiGoseikyuuGaku)
                .Add(objTdSeikyuusyoNo)
                .Add(objTdZenTaisyouFlg)
            End With

            '�e�[�u���ɍs���Z�b�g
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next

    End Sub
#End Region

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
        If prmActBtn.ID = Me.ButtonRirekiTorikesi.ID Then
            tmpCtrl = Me.ButtonRirekiTorikesi
        Else
            Return False
        End If

        '�Ώۃ`�F�b�N�{�b�N�X�ɂăf�[�^���Z�b�g����Ă��邩���`�F�b�N
        If Me.HiddenSimeDateRirekiPk.Value = String.Empty _
            Or Me.HiddenUpdDatetime.Value = String.Empty Then
            errMess += Messages.MSG140E
            arrFocusTargetCtrl.Add(tmpCtrl)
        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim listRec As New List(Of SeikyuuSimeDateRirekiRecord)

        '��ʂ��烌�R�[�h�N���X�ɃZ�b�g
        listRec = Me.GetCtrlToDataRec()

        ' �f�[�^�̍X�V���s���܂�
        If MyLogic.saveData(Me, listRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʂ̊e�������R�[�h�N���X�Ɏ擾���ADB�X�V�p���R�[�h�N���X��ԋp����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataRec() As List(Of SeikyuuSimeDateRirekiRecord)

        Dim listRec As New List(Of SeikyuuSimeDateRirekiRecord)
        Dim dtRec As New SeikyuuSimeDateRirekiRecord
        Dim intCnt As Integer = 0
        Dim arrKeySeikyuusyoSimeDateRireki() As String = Nothing
        Dim arrKeyInfo() As String = Nothing
        Dim arrKeyUpdDatetime() As String = Nothing
        Dim arrKagamiUpdDatetime() As String = Nothing

        If Me.HiddenSimeDateRirekiPk.Value <> String.Empty Then
            arrKeySeikyuusyoSimeDateRireki = Split(Me.HiddenSimeDateRirekiPk.Value, EarthConst.SEP_STRING & EarthConst.SEP_STRING)
        End If

        If Me.HiddenUpdDatetime.Value <> String.Empty Then
            arrKeyUpdDatetime = Split(Me.HiddenUpdDatetime.Value, EarthConst.SEP_STRING & EarthConst.SEP_STRING)
        End If

        If Me.HiddenKagamiUpdDatetime.Value <> String.Empty Then
            arrKagamiUpdDatetime = Split(Me.HiddenKagamiUpdDatetime.Value, EarthConst.SEP_STRING & EarthConst.SEP_STRING)
        End If

        '����NO�ƍX�V�����Ɛ����ӍX�V�����̌�������̏ꍇ()
        If arrKeySeikyuusyoSimeDateRireki.Length = arrKeyUpdDatetime.Length _
            AndAlso arrKeySeikyuusyoSimeDateRireki.Length = arrKagamiUpdDatetime.Length Then

            If Not arrKeySeikyuusyoSimeDateRireki Is Nothing _
                AndAlso Not arrKeyUpdDatetime Is Nothing _
                AndAlso Not arrKagamiUpdDatetime Is Nothing Then

                For intCnt = 0 To arrKeySeikyuusyoSimeDateRireki.Length - 1
                    '����NO���邢�͍X�V�������󔒂̏ꍇ�A���̏�����
                    If arrKeySeikyuusyoSimeDateRireki(intCnt) = String.Empty _
                        OrElse arrKeyUpdDatetime(intCnt) = String.Empty _
                        OrElse arrKagamiUpdDatetime(intCnt) = String.Empty Then
                        Continue For
                    End If

                    arrKeyInfo = Split(arrKeySeikyuusyoSimeDateRireki(intCnt), EarthConst.SEP_STRING)

                    '***************************************
                    ' ��������f�[�^
                    '***************************************
                    dtRec = New SeikyuuSimeDateRirekiRecord

                    '���
                    dtRec.Torikesi = 1

                    '������R�[�h
                    cl.SetDisplayString(arrKeyInfo(0), dtRec.SeikyuuSakiCd)

                    '������}��
                    cl.SetDisplayString(arrKeyInfo(1), dtRec.SeikyuuSakiBrc)

                    '������敪
                    cl.SetDisplayString(arrKeyInfo(2), dtRec.SeikyuuSakiKbn)

                    '���������s�N��
                    cl.SetDisplayString(arrKeyInfo(3), dtRec.SeikyuusyoHakNengetu)

                    '�������ߓ�
                    cl.SetDisplayString(arrKeyInfo(4), dtRec.SeikyuuSimeDate)

                    '�X�V�҃��[�U�[ID
                    dtRec.UpdLoginUserId = userinfo.LoginUserId

                    '�X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
                    If arrKeyUpdDatetime(intCnt) = "" Then
                        dtRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                    Else
                        dtRec.UpdDatetime = DateTime.ParseExact(arrKeyUpdDatetime(intCnt), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                    End If

                    '�����ӍX�V���� �ǂݍ��ݎ��̃^�C���X�^���v
                    If arrKagamiUpdDatetime(intCnt) = "" Then
                        dtRec.SkUpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                    Else
                        dtRec.SkUpdDatetime = DateTime.ParseExact(arrKagamiUpdDatetime(intCnt), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                    End If

                    listRec.Add(dtRec)
                Next

            End If
        End If

        Return listRec
    End Function

#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks>�r��������s���ׁA�������̍X�V���t��ݒ肵�Ă�������<br/>
    '''          �X�V���̓��t�̓V�X�e�����t���ݒ肳��܂�</remarks>
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#End Region

End Class