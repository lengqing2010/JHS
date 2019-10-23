Public Partial Class SeikyuuDateIkkatuHenkou
    Inherits System.Web.UI.Page

    '��ʕ\���̕�����ϊ��p
    Private CLogic As CommonLogic = CommonLogic.Instance
    '���b�Z�[�W�N���X
    Private MLogic As New MessageLogic
    '���O�C�����[�U���R�[�h
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    '�����N�����ꊇ�ύX���W�b�N
    Dim logic As New SeikyuuDateIkkatuHenkouLogic

#Region "�v���p�e�B"

#Region "�R���g���[���l"
    '�ꗗ�w�b�_�[�s
    Private Const HEADER_TEIBETU As String = "�@�ʐ����e�[�u��"
    Private Const HEADER_TENBETU As String = "�X�ʐ����e�[�u��"
    Private Const HEADER_TENBETU_SYOKI As String = "�X�ʏ��������e�[�u��"
    Private Const HEADER_HANNYOU_URIAGE As String = "�ėp����e�[�u��"
    Private Const HEADER_URIAGE_DATA As String = "����f�[�^�e�[�u��(���f�[�^�폜��)"

    '��������
    Private Const RESULT_STR_KEN As String = "��"

    Private Const CTRL_NAME_TR1 As String = "DataTable_resultTr1_"
    Private Const CTRL_NAME_TR2 As String = "DataTable_resultTr2_"

    '�����{�^�������L�����f�p
    Public Const BTN_SEARCH_FLG_ARI As String = "1"
    Public Const BTN_SEARCH_FLG_NASI As String = "0"

#End Region

#Region "CSS�N���X��"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_KINGAKU = "kingaku"
    Private Const CSS_NUMBER = "number"
    Private Const CSS_DATE = "date"
#End Region

#End Region

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

            ' ������敪�R���{�Ƀf�[�^���o�C���h����
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()

            '�t�H�[�J�X�ݒ�
            SelectSeikyuuKbn.Focus()

        End If

    End Sub

#Region "�{�^���C�x���g"

    ''' <summary>
    ''' �����挟���{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiSearch.ServerClick
        Dim blnResult As Boolean
        Dim comBizLogic As New CommonBizLogic

        '�����挟����ʌďo
        blnResult = CLogic.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                        , Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                        , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)

        '�ԐF�����ύX�Ή���z��Ɋi�[
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
        '������R�擾�ݒ�ƐF�֏���
        CLogic.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, TextTorikesiRiyuu, True, False, objChgColor)

        '���������Z�b�g
        If Me.TextSeikyuuSakiCd.Value <> String.Empty Then
            TextSeikyuuSimebi.Value = comBizLogic.GetSeikyuuSimeDate(Me.TextSeikyuuSakiCd.Value, _
                                                                     Me.TextSeikyuuSakiBrc.Value, _
                                                                     Me.SelectSeikyuuKbn.SelectedValue)
            If TextSeikyuuSimebi.Value <> String.Empty Then
                TextSeikyuuSimebi.Value &= "��"
            End If
        End If

        If blnResult Then
            'hidden�ɑޔ�
            Me.HiddenSeikyuuSakiKbnOld.Value = Me.SelectSeikyuuKbn.SelectedValue
            Me.HiddenSeikyuuSakiCdOld.Value = Me.TextSeikyuuSakiCd.Value
            Me.HiddenSeikyuuSakiBrcOld.Value = Me.TextSeikyuuSakiBrc.Value
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.btnSearch)
        End If

        '�������ύX�����ꍇ�͍ēx�����{�^���������K�v
        Me.HiddenSearch.Value = BTN_SEARCH_FLG_NASI

    End Sub

    ''' <summary>
    ''' �����N�����ꊇ�ύX�������s�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuuDateIkkatuHenkouExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuuDateIkkatuHenkouExe.ServerClick

        '���̓`�F�b�N
        If Not CheckInput() Then
            Exit Sub
        Else
        End If

        Try
            Dim listResult As New List(Of Integer)

            '�������s
            listResult = logic.SeikyuuDateIkkatuHenkou(sender, _
                                                       TextSeikyuuSakiCd.Value, _
                                                       TextSeikyuuSakiBrc.Value, _
                                                       SelectSeikyuuKbn.SelectedValue, _
                                                       TextSeikyuuDate.Value, _
                                                       DateTime.Now, _
                                                       userinfo.LoginUserId)

            If listResult(0) < 0 Then
                TdResult1.InnerHtml = "�G���["
                MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "�����N�����ꊇ�ύX") & "\r\n" & listResult(0), 0, "err")
            Else
                '���ʕ\��
                Me.SetTableHenkouKekka(listResult)
            End If

        Catch ex As Exception
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "�����N�����ꊇ�ύX") & "\r\n" & ex.Message, 0, "err")

        End Try

        'DB�l�̍ŐV����ʕ`��
        Me.SetCmnSearchResult(sender, e, True)

    End Sub

    ''' <summary>
    ''' �������s�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        setFocusAJ(Me.btnSearch) '�t�H�[�J�X

        '���̓`�F�b�N
        If Not CheckInput() Then
            Exit Sub
        Else
        End If

        Me.SetCmnSearchResult(sender, e)

    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"

    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �{�^���̃C�x���g�n���h����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�������s�{�^��
        Me.btnSearch.Attributes("onclick") = "checkJikkou();"

        '�����{�^�������L�����f(��)
        Me.HiddenSearch.Value = BTN_SEARCH_FLG_NASI

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�}�X�^�����n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.SelectSeikyuuKbn.Attributes("onblur") = "clrSeikyuuInfo(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������R�e�L�X�g�{�b�N�X�̃X�^�C����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        CLogic.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

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
    ''' �������s������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCmnSearchResult(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs, _
                                    Optional ByVal UpdateFlg As Boolean = False)

        Dim total_count As Integer = 0  '�擾����
        Dim row_count As Integer = 0    '�e�e�[�u���̎擾����

        Dim listResult As New List(Of Integer)
        Dim intResult As Integer = 0
        listResult.Add(intResult)

        '************
        '* �������s
        '************
        '1.�������s���@�ʐ����e�[�u��
        Dim TeibetuArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        TeibetuArray = Me.GetCmnSearchResult(sender, _
                                                row_count, _
                                                total_count, _
                                                listResult, _
                                                EarthEnum.emIkkatuHenkouDataSearchType.TeibetuSeikyuu)

        If row_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        '2.�������s���X�ʐ����e�[�u��
        Dim TenbetuArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        TenbetuArray = Me.GetCmnSearchResult(sender, _
                                                row_count, _
                                                total_count, _
                                                listResult, _
                                                EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu)
        If row_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        '3.�������s���X�ʏ��������e�[�u��
        Dim TenbetuSyokiArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        TenbetuSyokiArray = Me.GetCmnSearchResult(sender, _
                                                     row_count, _
                                                     total_count, _
                                                     listResult, _
                                                     EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSyoki)
        If row_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        '4.�������s���ėp����e�[�u��
        Dim HannyouUriageArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        HannyouUriageArray = Me.GetCmnSearchResult(sender, _
                                                      row_count, _
                                                      total_count, _
                                                      listResult, _
                                                      EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage)
        If row_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        '5.�������s������f�[�^�e�[�u��
        Dim UriageDataArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        UriageDataArray = Me.GetCmnSearchResult(sender, _
                                                   row_count, _
                                                   total_count, _
                                                   listResult, _
                                                   EarthEnum.emIkkatuHenkouDataSearchType.UriageData)
        If row_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
        End If

        '�������ʌ�����ݒ�
        resultCount.InnerHtml = total_count

        '************************
        '* ��ʃe�[�u���֏o��
        '************************
        '�s�J�E���^
        Dim rowCnt As Integer = 0

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
        '* ���ʕ\��
        '************
        '1.���ʕ\�����@�ʐ����e�[�u��
        Me.SetSearchResult(sender, _
                            e, _
                            TeibetuArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.TeibetuSeikyuu, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        '2.���ʕ\�����X�ʐ����e�[�u��
        Me.SetSearchResult(sender, _
                            e, _
                            TenbetuArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        '3.���ʕ\�����X�ʏ��������e�[�u��
        Me.SetSearchResult(sender, _
                            e, _
                            TenbetuSyokiArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSyoki, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        '4.���ʕ\�����ėp����e�[�u��
        Me.SetSearchResult(sender, _
                            e, _
                            HannyouUriageArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        '5.���ʕ\��������f�[�^�e�[�u��
        Me.SetSearchResult(sender, _
                            e, _
                            UriageDataArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.UriageData, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        ''�ꊇ�ύX�{�^���������́A�X�V���ʌ�����\������
        'If UpdateFlg = False Then
        '    '���ʕ\��
        '    Me.SetTableHenkouKekka(listResult)
        'End If

        '�����{�^�������L�����f(�L)
        Me.HiddenSearch.Value = BTN_SEARCH_FLG_ARI

    End Sub

    ''' <summary>
    ''' �Ώۃe�[�u������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="row_count">�ŏI�s</param>
    ''' <param name="total_count">�S����</param>
    ''' <param name="listResult">�������ʌ����\���p���X�g</param>
    ''' <param name="emType">�����N�����ꊇ�ύX�����^�C�v</param>
    ''' <returns>�����N�����ꊇ�ύX�����p���R�[�h��List(Of SeikyuuDateIkkatuHenkouRecord)</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSearchResult(ByVal sender As System.Object, _
                                        ByRef row_count As Integer, _
                                        ByRef total_count As Integer, _
                                        ByRef listResult As List(Of Integer), _
                                        ByVal emType As EarthEnum.emIkkatuHenkouDataSearchType _
                                        ) As List(Of SeikyuuDateIkkatuHenkouRecord)

        '�\���ő匏��
        Dim end_count As Integer = EarthConst.MAX_RESULT_COUNT

        '�w�b�_�[�s���쐬
        Dim teibetuRec As New SeikyuuDateIkkatuHenkouRecord
        Dim tenbetuRec As New SeikyuuDateIkkatuHenkouRecord
        Dim tenbetuSyokiRec As New SeikyuuDateIkkatuHenkouRecord
        Dim HannyouUriageRec As New SeikyuuDateIkkatuHenkouRecord
        Dim UriageDataRec As New SeikyuuDateIkkatuHenkouRecord
        Dim UriageDataSeikyuuDateRec As New SeikyuuDateIkkatuHenkouRecord

        '�w�b�_�[�s�ɒl���Z�b�g
        teibetuRec.SesyuMei = HEADER_TEIBETU
        tenbetuRec.SesyuMei = HEADER_TENBETU
        tenbetuSyokiRec.SesyuMei = HEADER_TENBETU_SYOKI
        HannyouUriageRec.SesyuMei = HEADER_HANNYOU_URIAGE
        UriageDataRec.SesyuMei = HEADER_URIAGE_DATA

        Dim resultArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        resultArray = logic.GetSeikyuuDateIkkatuHenkou(sender, _
                                                       TextSeikyuuSakiCd.Value, _
                                                       TextSeikyuuSakiBrc.Value, _
                                                       SelectSeikyuuKbn.SelectedValue, _
                                                       1, _
                                                       end_count, _
                                                       row_count, _
                                                       emType)
        If row_count >= 0 Then
            '�������ʌ���
            listResult(0) = row_count
            listResult.Add(row_count)

            If row_count <> 0 Then

                '�擪�s�w�b�_�[���Z�b�g
                Select Case emType
                    Case EarthEnum.emIkkatuHenkouDataSearchType.TeibetuSeikyuu
                        resultArray.Insert(0, teibetuRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu
                        resultArray.Insert(0, tenbetuRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSyoki
                        resultArray.Insert(0, tenbetuSyokiRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage
                        resultArray.Insert(0, HannyouUriageRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.UriageData
                        resultArray.Insert(0, UriageDataRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.UriageDataTorikesiSeikyuuDate
                        resultArray.Insert(0, UriageDataSeikyuuDateRec)
                End Select

                '�������ɃZ�b�g
                total_count += row_count
            End If
        End If

        Return resultArray

    End Function

    ''' <summary>
    ''' ���������Ō����������e���������ʃe�[�u���ɕ\������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="resultArray">�����N�����ꊇ�ύX�����p���R�[�h��List(Of SeikyuuDateIkkatuHenkouRecord)</param>
    ''' <param name="emType">�����N�����ꊇ�ύX�����^�C�v</param>
    ''' <param name="widthList1">�e�Z���̕��ݒ�p�̃��X�g1</param>
    ''' <param name="widthlist2">�e�Z���̕��ݒ�p�̃��X�g2</param>
    ''' <param name="rowCnt">�s�J�E���^</param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                ByVal resultArray As List(Of SeikyuuDateIkkatuHenkouRecord), _
                                ByVal emType As EarthEnum.emIkkatuHenkouDataSearchType, _
                                ByVal widthList1 As List(Of String), _
                                ByVal widthlist2 As List(Of String), _
                                ByRef rowCnt As Integer)

        '************
        '* �ϐ��錾
        '************
        Dim blnFirstRow As Boolean = True
        Dim strSpace As String = EarthConst.HANKAKU_SPACE

        'TabIndex
        Dim intTrTabIndex As Integer = 5
        'Tr
        Dim objTr1 As HtmlTableRow
        Dim objTr2 As HtmlTableRow

        'Table Cell
        Dim objTdKbn As HtmlTableCell               '�敪
        Dim objTdBangou As HtmlTableCell            '�ԍ�
        Dim objTdSesyuMei As HtmlTableCell          '�{�喼
        Dim objTdKameitenCd As HtmlTableCell        '�����X�R�[�h
        Dim objTdSyouhinCd As HtmlTableCell         '���i�R�[�h
        Dim objTdSyouhinMei As HtmlTableCell        '���i��

        Dim objTdSuu As HtmlTableCell               '����
        Dim objTdUriGaku As HtmlTableCell           '������z
        Dim objTdSeikyuusyoHakDate As HtmlTableCell '���������s��
        Dim objTdUriDate As HtmlTableCell           '����N����
        Dim objTdDennpyouUriDate As HtmlTableCell   '�`�[����N����

        '�擾�����f�[�^����ʂɕ\��
        For Each data As SeikyuuDateIkkatuHenkouRecord In resultArray
            rowCnt += 1

            'Tr
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            'Table Cell
            objTdKbn = New HtmlTableCell                '�敪
            objTdBangou = New HtmlTableCell             '�ԍ�
            objTdSesyuMei = New HtmlTableCell           '�{�喼
            objTdKameitenCd = New HtmlTableCell         '�����X�R�[�h
            objTdSyouhinCd = New HtmlTableCell          '���i�R�[�h
            objTdSyouhinMei = New HtmlTableCell         '���i��

            objTdSuu = New HtmlTableCell                '����
            objTdUriGaku = New HtmlTableCell            '������z
            objTdSeikyuusyoHakDate = New HtmlTableCell  '���������s��
            objTdUriDate = New HtmlTableCell            '����N����
            objTdDennpyouUriDate = New HtmlTableCell    '�`�[����N����

            '�������ʔz�񂩂�Z���Ɋi�[
            objTdKbn.InnerHtml = CLogic.GetDisplayString(data.Kbn, strSpace)
            objTdBangou.InnerHtml = CLogic.GetDisplayString(data.Bangou, strSpace)
            objTdSesyuMei.InnerHtml = CLogic.GetDisplayString(data.SesyuMei, strSpace)
            objTdKameitenCd.InnerHtml = CLogic.GetDisplayString(data.KameitenCd, strSpace)
            objTdSyouhinCd.InnerHtml = CLogic.GetDisplayString(data.SyouhinCd, strSpace)
            objTdSyouhinMei.InnerHtml = CLogic.GetDisplayString(data.SyouhinMei, strSpace)

            objTdSuu.InnerHtml = CLogic.GetDisplayString(data.Suu, strSpace)

            If blnFirstRow Then
                objTdUriGaku.InnerHtml = CLogic.GetDisplayString(data.UriGaku, strSpace)
                blnFirstRow = False
            Else
                If emType = EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu _
                    OrElse emType = EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage Then

                    If data.Tanka = Integer.MinValue OrElse data.Suu = Integer.MinValue Then
                        objTdUriGaku.InnerHtml = CLogic.GetDisplayString(data.UriGaku, strSpace)
                    Else
                        objTdUriGaku.InnerHtml = CLogic.GetDisplayString(data.Tanka * data.Suu, strSpace)
                    End If
                Else
                    objTdUriGaku.InnerHtml = CLogic.GetDisplayString(data.UriGaku, strSpace)
                End If
            End If

            objTdSeikyuusyoHakDate.InnerHtml = CLogic.GetDisplayString(data.SeikyuusyoHakDate, strSpace)
            objTdUriDate.InnerHtml = CLogic.GetDisplayString(data.UriDate, strSpace)
            objTdDennpyouUriDate.InnerHtml = CLogic.GetDisplayString(data.DenpyouUriDate, strSpace)


            '�e�Z���̕��ݒ�
            If rowCnt = 1 Then
                objTdKbn.Style("width") = widthList1(0)
                objTdBangou.Style("width") = widthList1(1)
                objTdSesyuMei.Style("width") = widthList1(2)
                objTdKameitenCd.Style("width") = widthList1(3)
                objTdSyouhinCd.Style("width") = widthList1(4)
                objTdSyouhinMei.Style("width") = widthList1(5)

                objTdSuu.Style("width") = widthlist2(0)
                objTdUriGaku.Style("width") = widthlist2(1)
                objTdSeikyuusyoHakDate.Style("width") = widthlist2(2)
                objTdUriDate.Style("width") = widthlist2(3)
                objTdDennpyouUriDate.Style("width") = widthlist2(4)
            End If

            '�X�^�C���A�N���X�ݒ�
            objTdKbn.Attributes("class") = CSS_TEXT_CENTER
            objTdBangou.Attributes("class") = CSS_TEXT_CENTER
            objTdSesyuMei.Attributes("class") = ""
            objTdKameitenCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinMei.Attributes("class") = ""

            objTdSuu.Attributes("class") = CSS_NUMBER
            objTdUriGaku.Attributes("class") = CSS_KINGAKU
            objTdSeikyuusyoHakDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdUriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdDennpyouUriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER


            '�sID��JS�C�x���g�̕t�^
            objTr1.ID = CTRL_NAME_TR1 & rowCnt
            objTr1.Attributes("tabindex") = -1

            '�s�ɃZ�����Z�b�g1
            With objTr1.Controls
                .Add(objTdKbn)
                .Add(objTdBangou)
                .Add(objTdSesyuMei)
                .Add(objTdKameitenCd)
                .Add(objTdSyouhinCd)
                .Add(objTdSyouhinMei)
            End With

            '�sID��JS�C�x���g�̕t�^
            objTr2.ID = CTRL_NAME_TR2 & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(Me.btnSearch.Attributes("tabindex")) + intTrTabIndex
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2�s�ڈȍ~�̓^�u�ړ��Ȃ�
                objTr2.Attributes("tabindex") = -1
            End If

            '�s�ɃZ���ƃZ�b�g2
            With objTr2.Controls
                .Add(objTdSuu)
                .Add(objTdUriGaku)
                .Add(objTdSeikyuusyoHakDate)
                .Add(objTdUriDate)
                .Add(objTdDennpyouUriDate)
            End With

            '�e�[�u���ɍs���Z�b�g
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)
        Next
    End Sub

    ''' <summary>
    ''' �������ʕ\��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTableHenkouKekka(ByVal listResult As List(Of Integer))

        TdResult1.InnerHtml = listResult(1) & RESULT_STR_KEN
        TdResult2.InnerHtml = listResult(2) & RESULT_STR_KEN
        TdResult3.InnerHtml = listResult(3) & RESULT_STR_KEN
        TdResult4.InnerHtml = listResult(4) & RESULT_STR_KEN
        TdResult5.InnerHtml = listResult(5) & RESULT_STR_KEN

    End Sub

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean
        '�G���[���b�Z�[�W������
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        '���R�[�h���͒l�ύX�`�F�b�N
        CheckSeikyuuChg(strErrMsg)

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If strErrMsg <> "" Then
            '�t�H�[�J�X�Z�b�g
            btnSeikyuuSakiSearch.Focus()
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True

    End Function

    ''' <summary>
    ''' ��������͒l�ύX�`�F�b�N
    ''' </summary>
    ''' <param name="strErrMsg"></param>
    ''' <remarks></remarks>
    Private Sub CheckSeikyuuChg(ByRef strErrMsg As String)

        '������R�[�h�E�}�ԁE�敪�̂ǂꂩ�ɓ��͂�����ꍇ�̂݃`�F�b�N
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd.Value) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc.Value) Or _
           Me.SelectSeikyuuKbn.SelectedIndex <> 0 Then

            '�����{�^���������ƌ��݂ō��ق�����̂��`�F�b�N
            If Me.TextSeikyuuSakiCd.Value <> Me.HiddenSeikyuuSakiCdOld.Value _
                Or Me.TextSeikyuuSakiBrc.Value <> Me.HiddenSeikyuuSakiBrcOld.Value _
                Or Me.SelectSeikyuuKbn.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "������")
            End If
        End If
    End Sub

#End Region
End Class