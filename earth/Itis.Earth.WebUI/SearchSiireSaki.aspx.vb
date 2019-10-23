
Partial Public Class SearchSiireSaki
    Inherits System.Web.UI.Page

    Dim sdsLogic As New SiireDataSearchLogic
    Dim mesLogic As New MessageLogic
    Dim cl As New CommonLogic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            ' �d����R�[�h����̏ꍇ�͖��̂Ƀt�H�[�J�X
            TextKana.Focus()

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                TextCd.Value = arrSearchTerm(0)                     '�e��ʂ���POST���ꂽ���1 �F�d����R�[�h
                TextBrc.Value = arrSearchTerm(1)                    '�e��ʂ���POST���ꂽ���1 �F�d����R�[�h
                returnTargetIds.Value = Request("returnTargetIds")  '�e��ʂ���POST���ꂽ�߂�l�Z�b�g��ID�S
                afterEventBtnId.Value = Request("afterEventBtnId")  '�l�Z�b�g��ɉ�������A�e��ʂ̃{�^��ID
            End If

            If TextCd.Value.Trim() <> "" Then
                ' �d����R�[�h�Ƀt�H�[�J�X
                TextCd.Focus()
            End If

        End If

    End Sub

    ''' <summary>
    ''' �������s���̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick

        '�f�[�^�擾�ő匏�����Z�b�g
        Dim maxCount As Integer
        Try
            maxCount = Integer.Parse(SelectMaxSearchCount.Value)
        Catch ex As Exception
            maxCount = 100
        End Try

        '���������ɉ������d����̃��R�[�h�����ׂĎ擾���܂�
        Dim list As List(Of siireSakiInfoRecord)
        '�����������i�[�p
        Dim resultRowCount As Integer
        list = sdsLogic.GetSiireSakiInfo(TextCd.Value, _
                                           TextBrc.Value, _
                                           String.Empty, _
                                           TextKana.Value, _
                                           resultRowCount, _
                                           1, _
                                           maxCount)

        ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
        If resultRowCount = 0 Then
            mesLogic.AlertMessage(sender, Messages.MSG020E)
        End If

        '�\��������������
        Dim displayCount As String = resultRowCount
        TdResultCount.Style.Remove("color")
        If maxCount <> Integer.MaxValue Then
            If maxCount < resultRowCount Then
                TdResultCount.Style("color") = "red"
                displayCount = maxCount & " / " & CommonLogic.Instance.GetDisplayString(resultRowCount)
            End If
        End If
        TdResultCount.InnerHtml = displayCount

        ' �s�J�E���^
        Dim rowCount As Integer = 0

        ' �擾�����d���������ʂɕ\��
        For Each data As siireSakiInfoRecord In list

            rowCount += 1
            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdCd As New HtmlTableCell
            Dim objTdName As New HtmlTableCell

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            With objIptRtnHiddn
                .Value = String.Empty
                .Value &= data.siireSakiCd & EarthConst.SEP_STRING
                .Value &= data.siireSakiBrc & EarthConst.SEP_STRING
                .Value &= data.SiireSakiMei & EarthConst.SEP_STRING
            End With
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdCd.InnerHtml = cl.GetDisplayString(data.SiireSakiCd & "-" & data.SiireSakiBrc, EarthConst.HANKAKU_SPACE)
            objTdName.InnerHtml = cl.GetDisplayString(data.SiireSakiMei, EarthConst.HANKAKU_SPACE)

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If
            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdCd)
            objTr.Controls.Add(objTdName)

            searchGrid.Controls.Add(objTr)

            If resultRowCount = 1 Then
                '��������1���݂̂̏ꍇ�̗�ID�i�[�phidden�ɒl���Z�b�g
                firstSend.Value = objTr.ClientID
            End If

        Next

        TextKana.Focus()

    End Sub

End Class