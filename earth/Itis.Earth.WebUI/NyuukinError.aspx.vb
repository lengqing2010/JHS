Partial Public Class NyuukinError
    Inherits System.Web.UI.Page

#Region "�ϐ�"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X

        ' ���[�U�[��{�F��
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            '���O�C����񂪖����ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If
        '��������
        If user_info.KeiriGyoumuKengen <> -1 Then
            '�����������ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        setTable()

    End Sub

    ''' <summary>
    ''' �������s���A���ʂ���ʂɕ\������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setTable()
        Dim clsLogic As New NyuukinErrorLogic
        Dim nyuukinErrTable As DataTable
        Dim objTr As HtmlTableRow
        Dim objTdGyouNo As HtmlTableCell
        Dim objTdGroupCd As HtmlTableCell
        Dim objTdKokyakuCd As HtmlTableCell
        Dim objTdTekiyou As HtmlTableCell
        Dim objTdNyuukinGaku As HtmlTableCell
        Dim objTdSyouhinCd As HtmlTableCell

        '1.�G���[���̎擾
        nyuukinErrTable = clsLogic.GetNyuukinErrData(Request("nkn_kbn"), Request("edi"))
        'If Request("nkn_kbn") = 1 Then
        '    Me.lblTitle.Text = "�����d���m�F"
        '    Me.Title = "EARTH �����d���m�F"
        'End If
        '�\�������\������
        Me.resultCount.InnerText = nyuukinErrTable.Rows.Count
        Me.textTorikomiDate.Value = nyuukinErrTable.Rows(0).Item("syori_datetime")
        Me.textTorikomiFileName.Value = Request("file")

        '�O���b�h�̃N���A
        Me.errorGrid.Controls.Clear()
        '�G���[���̏o��
        For intCnt As Integer = 0 To nyuukinErrTable.Rows.Count - 1
            '�R���g���[���̃C���X�^���X����
            objTr = New HtmlTableRow
            objTdGyouNo = New HtmlTableCell
            objTdGroupCd = New HtmlTableCell
            objTdKokyakuCd = New HtmlTableCell
            objTdTekiyou = New HtmlTableCell
            objTdNyuukinGaku = New HtmlTableCell
            objTdSyouhinCd = New HtmlTableCell
            objTdNyuukinGaku.Align = "right"
            '�Z���֎擾�����l�̃Z�b�g
            With nyuukinErrTable.Rows(intCnt)
                objTdGyouNo.InnerText = .Item("gyou_no")
                objTdGroupCd.InnerText = .Item("group_cd")
                objTdKokyakuCd.InnerText = .Item("kokyaku_cd")
                If IsDBNull(.Item("tekiyou")) OrElse .Item("tekiyou").ToString.Trim = String.Empty Then
                    objTdTekiyou.InnerText = "�@"
                Else
                    objTdTekiyou.InnerText = .Item("tekiyou")
                End If
                objTdNyuukinGaku.InnerText = Format(.Item("nyuukin_gaku"), EarthConst.FORMAT_KINGAKU_3)
                If IsDBNull(.Item("syouhin_cd")) OrElse .Item("syouhin_cd").ToString.Trim = String.Empty Then
                    objTdSyouhinCd.InnerText = "�@"
                Else
                    objTdSyouhinCd.InnerText = .Item("syouhin_cd")
                End If
            End With
            objTr.ID = "resultTr_" & intCnt + 1
            '�s�ɃZ���̒ǉ�
            With objTr.Controls
                .Add(objTdGyouNo)
                .Add(objTdGroupCd)
                .Add(objTdKokyakuCd)
                .Add(objTdTekiyou)
                .Add(objTdNyuukinGaku)
                .Add(objTdSyouhinCd)
            End With
            objTr.BorderColor = "black"

            '�O���b�h�ɍs�̒ǉ�
            errorGrid.Controls.Add(objTr)
        Next
    End Sub
End Class