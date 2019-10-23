Partial Public Class SearchNyuukinData
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim sl As New StringLogic
    ''' <summary>
    ''' ���b�Z�[�W�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

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
            Me.setDispAction()

            '�{�^�������C�x���g�̐ݒ�
            Me.setBtnEvent()

            '�t�H�[�J�X�ݒ�
            Me.TextDenpyouBangouFrom.Focus()

        End If
    End Sub

#Region "�{�^���C�x���g"

    ''' <summary>
    ''' �������s���̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        Dim MyLogic As New NyuukinDataSearchLogic
        Dim listResult As List(Of NyuukinDataRecord)

        '�����f�[�^�e�[�u�����R�[�h�N���X
        Dim recKey As New NyuukinDataKeyRecord

        '�����������̎擾
        Me.SetSearchKeyFromCtrl(recKey)

        '�\���ő匏��
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        '�������s
        listResult = MyLogic.GetNyuukinDataInfo(sender, recKey, 1, end_count, total_count)

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
            displayCount = maxSearchCount.Value & " / " & cl.GetDisplayString(total_count)
        End If

        '�������ʌ�����ݒ�
        resultCount.InnerHtml = displayCount

        '����ʂɃZ�b�g
        Me.SetCtrlFromDataRec(listResult)

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

        '�ԐF�����ύX�Ή���z��Ɋi�[
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
        '������R�擾�ݒ�ƐF�֏���
        cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuSakiKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, TextTorikesiRiyuu, True, False, objChgColor)

        If blnResult Then
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.btnSeikyuuSakiSearch)
        End If

    End Sub

    ''' <summary>
    ''' CSV�o�̓{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenCsv.ServerClick
        'Dim strFileNm As String = String.Empty  '�o�̓t�@�C����

        '�����f�[�^�e�[�u�����R�[�h�N���X
        Dim recKey As New NyuukinDataKeyRecord
        Dim dtCsv As DataTable
        Dim MyLogic As New NyuukinDataSearchLogic

        '�����������̎擾
        Me.SetSearchKeyFromCtrl(recKey)

        '����
        Dim total_count As Integer = 0

        '�������s
        dtCsv = MyLogic.GetNyuukinDataCsv(sender, recKey, total_count)

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
        If cl.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_NYUUKIN_DATA, dtCsv) = False Then
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
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�`�[�ԍ��C�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextDenpyouBangouFrom.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"
        Me.TextDenpyouBangouTo.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�}�X�^�����n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.SelectSeikyuuSakiKbn.Attributes("onblur") = "clrSeikyuuInfo(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������R�e�L�X�g�{�b�N�X�̃X�^�C����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

    End Sub

    ''' <summary>
    ''' �o�^/�C�����s�{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V�̊m�F���s�Ȃ��B<br/>
    ''' OK���FDB�X�V���s�Ȃ��B<br/>
    ''' �L�����Z�����FDB�X�V�𒆒f����B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ���s�{�^���֘A
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�������s�{�^���̃C�x���g�n���h����ݒ�
        Me.btnSearch.Attributes("onclick") = "checkJikkou(0);"

        'CSV�o�̓{�^���̃C�x���g�n���h����ݒ�
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou(1);"

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ��猟���L�[���R�[�h�ւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="recKey">���o���R�[�h�N���X�̃L�[</param>
    ''' <remarks></remarks>
    Public Sub SetSearchKeyFromCtrl(ByRef recKey As NyuukinDataKeyRecord)

        '�`�[�ԍ� From
        recKey.DenNoFrom = IIf(Me.TextDenpyouBangouFrom.Value <> "", Me.TextDenpyouBangouFrom.Value, String.Empty)
        '�`�[�ԍ� To 
        recKey.DenNoTo = IIf(Me.TextDenpyouBangouTo.Value <> "", Me.TextDenpyouBangouTo.Value, String.Empty)
        '�`�[�쐬��(�o�^�N����) From
        recKey.AddDatetimeFrom = IIf(Me.TextAddDateFrom.Value <> "", Me.TextAddDateFrom.Value, DateTime.MinValue)
        '�`�[�쐬��(�o�^�N����) To
        recKey.AddDatetimeTo = IIf(Me.TextAddDateTo.Value <> "", Me.TextAddDateTo.Value, DateTime.MinValue)
        recKey.NyuukinDateFrom = IIf(Me.TextNyuukinDateFrom.Value <> "", Me.TextNyuukinDateFrom.Value, DateTime.MinValue)
        '�����N���� To
        recKey.NyuukinDateTo = IIf(Me.TextNyuukinDateTo.Value <> "", Me.TextNyuukinDateTo.Value, DateTime.MinValue)
        '������R�[�h
        recKey.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> "", Me.TextSeikyuuSakiCd.Value, String.Empty)
        '������}��
        recKey.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> "", Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '������敪
        recKey.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuSakiKbn.SelectedValue <> "", Me.SelectSeikyuuSakiKbn.SelectedValue, String.Empty)
        '������J�i��
        recKey.SeikyuuSakiMeiKana = IIf(Me.TextSeikyuuSakiMeiKana.Value <> "", Me.TextSeikyuuSakiMeiKana.Value, String.Empty)
        '�ŐV�`�[�̂ݕ\��
        recKey.NewDenpyouDisp = Integer.MinValue

    End Sub

    ''' <summary>
    ''' ���o���R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="listResult">���o���R�[�h�N���X�̃��X�g</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal listResult As List(Of NyuukinDataRecord))

        Dim objTr1 As New HtmlTableRow
        Dim objTr2 As New HtmlTableRow

        Dim objTdDenUnqNo As New HtmlTableCell          '�`�[���j�[�NNO
        Dim objTdDenpyouSyubetu As HtmlTableCell        '�`�[���
        Dim objTdSeikyuuSakiCd As HtmlTableCell         '������R�[�h
        Dim objTdSeikyuuSakiMei As HtmlTableCell        '�����於
        Dim objTdNyuukinDate As HtmlTableCell           '�����N����

        Dim objTdGoukeiGaku As HtmlTableCell            '�`�[���v���z
        Dim objTdGenkin As HtmlTableCell                '����
        Dim objTdKogitte As HtmlTableCell               '���؎�
        Dim objTdKouzaFurikae As HtmlTableCell          '�����U��
        Dim objTdFurikomi As HtmlTableCell              '�U��
        Dim objTdTegata As HtmlTableCell                '��`
        Dim objTdKyouryokuKaihi As HtmlTableCell        '���͉��
        Dim objTdFurikomiTesuuRyou As HtmlTableCell     '�U���萔��
        Dim objTdSousai As HtmlTableCell                '���E
        Dim objTdNebiki As HtmlTableCell                '�l��
        Dim objTdSonota As HtmlTableCell                '���̑�

        Dim objTdTegataKijitu As HtmlTableCell          '��`����
        Dim objTdTekiyouMei As HtmlTableCell            '�E�v��
        Dim objTdNkTorikomiNo As HtmlTableCell          '�����捞NO

        '�擾��������f�[�^����ʂɕ\��
        Dim dtRec As New NyuukinDataRecord
        Dim rowCnt As Integer = 0 '�J�E���^

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
        For Each dtRec In listResult

            rowCnt += 1

            '�C���X�^���X��
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            objTdDenUnqNo = New HtmlTableCell              '�`�[���j�[�NNO
            objTdDenpyouSyubetu = New HtmlTableCell        '�`�[���
            objTdSeikyuuSakiCd = New HtmlTableCell         '������R�[�h
            objTdSeikyuuSakiMei = New HtmlTableCell        '�����於
            objTdNyuukinDate = New HtmlTableCell           '�����N����

            objTdGoukeiGaku = New HtmlTableCell            '�`�[���v���z
            objTdGenkin = New HtmlTableCell                '����
            objTdKogitte = New HtmlTableCell               '���؎�
            objTdKouzaFurikae = New HtmlTableCell          '�����U��
            objTdFurikomi = New HtmlTableCell              '�U��
            objTdTegata = New HtmlTableCell                '��`
            objTdKyouryokuKaihi = New HtmlTableCell        '���͉��
            objTdFurikomiTesuuRyou = New HtmlTableCell     '�U���萔��
            objTdSousai = New HtmlTableCell                '���E
            objTdNebiki = New HtmlTableCell                '�l��
            objTdSonota = New HtmlTableCell                '���̑�

            objTdTegataKijitu = New HtmlTableCell          '��`����
            objTdTekiyouMei = New HtmlTableCell            '�E�v��
            objTdNkTorikomiNo = New HtmlTableCell          '�����捞NO

            '�l�̐ݒ�
            objTdDenUnqNo.InnerHtml = cl.GetDisplayString(dtRec.DenpyouUniqueNo, EarthConst.HANKAKU_SPACE)
            objTdDenpyouSyubetu.InnerHtml = cl.GetDisplayString(dtRec.DenpyouSyubetu, EarthConst.HANKAKU_SPACE)
            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(dtRec.SeikyuuSakiKbn, dtRec.SeikyuuSakiCd, dtRec.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(dtRec.SeikyuuSakiMei, EarthConst.HANKAKU_SPACE)
            objTdNyuukinDate.InnerHtml = cl.GetDisplayString(dtRec.NyuukinDate, EarthConst.HANKAKU_SPACE)

            objTdGoukeiGaku.InnerHtml = Format(dtRec.DenpyouGoukeiGaku, EarthConst.FORMAT_KINGAKU_2)
            objTdGenkin.InnerHtml = IIf(dtRec.Genkin = Long.MinValue, "0", Format(dtRec.Genkin, EarthConst.FORMAT_KINGAKU_2))
            objTdKogitte.InnerHtml = IIf(dtRec.Kogitte = Long.MinValue, "0", Format(dtRec.Kogitte, EarthConst.FORMAT_KINGAKU_2))
            objTdKouzaFurikae.InnerHtml = IIf(dtRec.KouzaFurikae = Long.MinValue, "0", Format(dtRec.KouzaFurikae, EarthConst.FORMAT_KINGAKU_2))
            objTdFurikomi.InnerHtml = IIf(dtRec.Furikomi = Long.MinValue, "0", Format(dtRec.Furikomi, EarthConst.FORMAT_KINGAKU_2))
            objTdTegata.InnerHtml = IIf(dtRec.Tegata = Long.MinValue, "0", Format(dtRec.Tegata, EarthConst.FORMAT_KINGAKU_2))
            objTdKyouryokuKaihi.InnerHtml = IIf(dtRec.KyouryokuKaihi = Long.MinValue, "0", Format(dtRec.KyouryokuKaihi, EarthConst.FORMAT_KINGAKU_2))
            objTdFurikomiTesuuRyou.InnerHtml = IIf(dtRec.FurikomiTesuuryou = Long.MinValue, "0", Format(dtRec.FurikomiTesuuryou, EarthConst.FORMAT_KINGAKU_2))
            objTdSousai.InnerHtml = IIf(dtRec.Sousai = Long.MinValue, "0", Format(dtRec.Sousai, EarthConst.FORMAT_KINGAKU_2))
            objTdNebiki.InnerHtml = IIf(dtRec.Nebiki = Long.MinValue, "0", Format(dtRec.Nebiki, EarthConst.FORMAT_KINGAKU_2))
            objTdSonota.InnerHtml = IIf(dtRec.Sonota = Long.MinValue, "0", Format(dtRec.Sonota, EarthConst.FORMAT_KINGAKU_2))

            objTdTegataKijitu.InnerHtml = cl.GetDisplayString(dtRec.TegataKijitu, EarthConst.HANKAKU_SPACE)
            If dtRec.TekiyouMei Is Nothing Then
                objTdTekiyouMei.InnerHtml = cl.GetDisplayString(dtRec.TekiyouMei, EarthConst.HANKAKU_SPACE)
            Else
                objTdTekiyouMei.InnerHtml = cl.GetDisplayString(dtRec.TekiyouMei.Trim, EarthConst.HANKAKU_SPACE)
            End If
            objTdNkTorikomiNo.InnerHtml = cl.GetDisplayString(dtRec.NyuukinTorikomiUniqueNo, EarthConst.HANKAKU_SPACE)

            '�e�Z���̕��ݒ�
            If rowCnt = 1 Then
                objTdDenUnqNo.Style("width") = widthList1(0)
                objTdDenpyouSyubetu.Style("width") = widthList1(1)
                objTdSeikyuuSakiCd.Style("width") = widthList1(2)
                objTdSeikyuuSakiMei.Style("width") = widthList1(3)
                objTdNyuukinDate.Style("width") = widthList1(4)

                objTdGoukeiGaku.Style("width") = widthList2(0)
                objTdGenkin.Style("width") = widthList2(1)
                objTdKogitte.Style("width") = widthList2(2)
                objTdKouzaFurikae.Style("width") = widthList2(3)
                objTdFurikomi.Style("width") = widthList2(4)
                objTdTegata.Style("width") = widthList2(5)
                objTdKyouryokuKaihi.Style("width") = widthList2(6)
                objTdFurikomiTesuuRyou.Style("width") = widthList2(7)
                objTdSousai.Style("width") = widthList2(8)
                objTdNebiki.Style("width") = widthList2(9)
                objTdSonota.Style("width") = widthList2(10)

                objTdTegataKijitu.Style("width") = widthList2(11)
                objTdTekiyouMei.Style("width") = widthList2(12)
                objTdNkTorikomiNo.Style("width") = widthList2(13)
            End If

            '�X�^�C���A�N���X�ݒ�
            objTdDenUnqNo.Attributes("class") = "textCenter"
            objTdDenpyouSyubetu.Attributes("class") = "textCenter"
            If objTdDenpyouSyubetu.InnerHtml = EarthConst.FR Then
                objTdDenpyouSyubetu.Style("color") = "red"
            End If
            objTdSeikyuuSakiCd.Attributes("class") = "textCenter"
            objTdSeikyuuSakiMei.Attributes("class") = ""
            objTdNyuukinDate.Attributes("class") = "date textCenter"

            objTdGoukeiGaku.Attributes("class") = "kingaku"
            objTdGenkin.Attributes("class") = "kingaku"
            objTdKogitte.Attributes("class") = "kingaku"
            objTdKouzaFurikae.Attributes("class") = "kingaku"
            objTdFurikomi.Attributes("class") = "kingaku"
            objTdTegata.Attributes("class") = "kingaku"
            objTdKyouryokuKaihi.Attributes("class") = "kingaku"
            objTdFurikomiTesuuRyou.Attributes("class") = "kingaku"
            objTdSousai.Attributes("class") = "kingaku"
            objTdNebiki.Attributes("class") = "kingaku"
            objTdSonota.Attributes("class") = "kingaku"

            objTdTegataKijitu.Attributes("class") = "date textCenter"
            objTdTekiyouMei.Attributes("class") = ""
            objTdNkTorikomiNo.Attributes("class") = "number"

            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            '�s�ɃZ�����Z�b�g1
            With objTr1.Controls
                .Add(objTdDenUnqNo)
                .Add(objTdDenpyouSyubetu)
                .Add(objTdSeikyuuSakiCd)
                .Add(objTdSeikyuuSakiMei)
                .Add(objTdNyuukinDate)
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
                .Add(objTdGoukeiGaku)
                .Add(objTdGenkin)
                .Add(objTdKogitte)
                .Add(objTdKouzaFurikae)
                .Add(objTdFurikomi)
                .Add(objTdTegata)
                .Add(objTdKyouryokuKaihi)
                .Add(objTdFurikomiTesuuRyou)
                .Add(objTdSousai)
                .Add(objTdNebiki)
                .Add(objTdSonota)
                .Add(objTdTegataKijitu)
                .Add(objTdTekiyouMei)
                .Add(objTdNkTorikomiNo)
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