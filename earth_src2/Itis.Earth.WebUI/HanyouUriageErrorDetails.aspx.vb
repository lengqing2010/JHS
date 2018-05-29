Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class HanyouUriageErrorDetails
    Inherits System.Web.UI.Page

    Protected scrollHeight As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        If Not blnEigyouKengen Then
            '�G���[��ʂ֑J�ڂ��āA�G���[���b�Z�[�W��\������
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonChk.SetURL(Me, strUserID)
            '������
            Call SetInitData()
        End If
        'javascript�쐬
        MakeJavascript()
        '����飃{�^������
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")
        '�CSV�o�ͣ�{�^������
        Me.btnCsvOutput.Attributes.Add("onClick", "return fncCsvOut();")
    End Sub

    ''' <summary>�����f�[�^���Z�b�g����</summary>
    Protected Sub SetInitData()
        Dim hanyouUriageBC As New HanyouUriageLogic
        Dim strSyoriDate As String = String.Empty   '��������
        Dim strEdiDate As String = String.Empty     'EDI���쐬��
        If Not Request("sendSearchTerms") Is Nothing Then
            strEdiDate = Split(Request("sendSearchTerms").ToString, "$$$")(2)                   'EDI���쐬��
            strSyoriDate = Split(Request("sendSearchTerms").ToString, "$$$")(0)                 '��������
            Me.lblSyoriDate.Text = Left(Split(strSyoriDate, "$$$")(0), 4)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(4, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(6, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & " " & strSyoriDate.Substring(8, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(10, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(12, 2)   '��������
            Me.lblFileMei.Text = Split(Request("sendSearchTerms").ToString, "$$$")(1)           '���̓t�@�C����
            Me.lblFileMei.ToolTip = Split(Request("sendSearchTerms").ToString, "$$$")(1)        '���̓t�@�C�����^�C�g��
        Else
            Me.lblSyoriDate.Text = String.Empty     '��������
            Me.lblFileMei.Text = String.Empty       '���̓t�@�C����
        End If

        '�����f�[�^���擾����
        Dim dtKojKakakuInfo As DataTable
        dtKojKakakuInfo = hanyouUriageBC.GetHanyouUriageErr(strEdiDate, strSyoriDate, 100)
        '�������ʂ�ݒ肷��
        Me.grdBodyLeft.DataSource = dtKojKakakuInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtKojKakakuInfo
        Me.grdBodyRight.DataBind()
        '�������ʌ������擾����
        Dim intCount As Integer = hanyouUriageBC.GetHanyouUriageErrCount(strEdiDate, strSyoriDate)
        '�������ʌ�����ݒ肷��
        SetKensakuCount(intCount)
        'CSV�f�[�^������ݒ肷��
        Me.hidCsvCount.Value = intCount
        ViewState("EdiJouhouDate") = strEdiDate
        ViewState("SyoriDate") = strSyoriDate
        '�f�[�^��0���̏ꍇ�A�G���[���b�Z�[�W��\������
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        Else
            '������ݒ�
            SetKingaku()
        End If
    End Sub

    ''' <summary>�������ʌ�����ݒ肷��</summary>
    ''' <param name="intCount">�������ʌ���</param>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If intCount > 100 Then
            Me.lblCount.Text = "100 / " & intCount
            Me.lblCount.ForeColor = Drawing.Color.Red
            scrollHeight = 100 * 22 + 1
        Else
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 22 + 1
        End If

    End Sub

    ''' <summary>������ݒ�</summary>
    Public Sub SetKingaku()

        Dim rowCount As Integer
        Dim commonChk As New CommonCheck
        For rowCount = 0 To Me.grdBodyLeft.Rows.Count - 1
            If grdBodyLeft.Rows(rowCount).Cells(1).Text.ToString.Replace("&nbsp;", "").Trim.Equals(String.Empty) OrElse grdBodyLeft.Rows(rowCount).Cells(1).Text.ToString.Replace("&nbsp;", "").Trim = "0" Then
                grdBodyLeft.Rows(rowCount).Cells(1).Text = ""
            Else
                grdBodyLeft.Rows(rowCount).Cells(1).Text = "���"
            End If
        Next
        For rowCount = 0 To Me.grdBodyRight.Rows.Count - 1
            If CType(Me.grdBodyRight.Rows(rowCount).Cells(1).Controls(1), Label).Text.ToString.Trim.Equals(String.Empty) Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(1).Controls(1), Label).Text = ""
            ElseIf commonChk.CheckHankaku(CType(Me.grdBodyRight.Rows(rowCount).Cells(1).Controls(1), Label).Text, "", "") = String.Empty Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(1).Controls(1), Label).Text = FormatNumber(CType(Me.grdBodyRight.Rows(rowCount).Cells(1).Controls(1), Label).Text, 0)
            End If
            If CType(Me.grdBodyRight.Rows(rowCount).Cells(2).Controls(1), Label).Text.ToString.Trim.Equals(String.Empty) Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(2).Controls(1), Label).Text = ""
            ElseIf commonChk.CheckHankaku(CType(Me.grdBodyRight.Rows(rowCount).Cells(2).Controls(1), Label).Text, "", "") = String.Empty Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(2).Controls(1), Label).Text = FormatNumber(CType(Me.grdBodyRight.Rows(rowCount).Cells(2).Controls(1), Label).Text, 0)
            End If
            If CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text.ToString.Trim.Equals(String.Empty) Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text = ""
            ElseIf commonChk.CheckHankaku(CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text, "", "") = String.Empty Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text = FormatNumber(CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text, 0)
            End If
            If CType(Me.grdBodyRight.Rows(rowCount).Cells(5).Controls(1), Label).Text.ToString.Trim.Equals(String.Empty) Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(5).Controls(1), Label).Text = ""
            ElseIf commonChk.CheckHankaku(CType(Me.grdBodyRight.Rows(rowCount).Cells(5).Controls(1), Label).Text, "", "") = String.Empty Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(5).Controls(1), Label).Text = FormatNumber(CType(Me.grdBodyRight.Rows(rowCount).Cells(5).Controls(1), Label).Text, 0)
            End If
        Next

    End Sub
    Private Function SetTimeType(ByVal strTime As Object, Optional ByVal yyyy As Boolean = False) As String
        If IsDBNull(strTime) Then
            Return String.Empty
        End If

        If Not strTime.ToString.Trim.Equals(String.Empty) Then
            If IsDate(strTime.ToString) Then
                If yyyy Then
                    Return CDate(strTime).ToString("yyyy/MM/dd")

                Else
                    Return CDate(strTime).ToString("yyyy/MM/dd HH:mm:ss")
                End If

            Else
                Return strTime.ToString
            End If

        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>�G���[���b�Z�[�W�\��</summary>
    ''' <param name="strMessage">�G���[���b�Z�[�W</param>
    ''' <param name="strObjId">�N���C�A���gID</param>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("   document.getElementById('" & strObjId & "').select();")
            End If
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub

    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '�X�N���[����ݒ肷��
            .AppendLine("function wheel(event){")
            .AppendLine("   var delta = 0;")
            .AppendLine("   if(!event)")
            .AppendLine("       event = window.event;")
            .AppendLine("   if(event.wheelDelta){")
            .AppendLine("       delta = event.wheelDelta/120;")
            .AppendLine("       if(window.opera)")
            .AppendLine("           delta = -delta;")
            .AppendLine("       }else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("   if(delta)")
            .AppendLine("       handle(delta);")
            .AppendLine("}")
            .AppendLine("function handle(delta){")
            .AppendLine("   var divVscroll=" & divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   if (delta < 0){")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("   }else{")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("}")
            '�N���A�{�^������
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")
            'CSV�o�̓{�^������������ꍇ�A�m�F���b�Z�[�W��\������B
            .AppendLine("function fncCsvOut(){")
            .AppendLine("   if(document.getElementById('" & Me.hidCsvCount.ClientID & "').value > " & CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "){")
            .AppendLine("       if(confirm('" & Messages.Instance.MSG013C.Replace("@PARAM1", System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "')){")
            .AppendLine("           document.forms[0].submit();")
            .AppendLine("       }else{")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>CSV�o��</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click
        Dim hanyouUriageBC As New HanyouUriageLogic
        '�H�����i�G���[�f�[�^���擾����
        Dim dtHanyouSiireCsvInfo As DataTable
        '�H�����i�G���[CSV�f�[�^���擾����
        dtHanyouSiireCsvInfo = hanyouUriageBC.GetHanyouUriageErr(ViewState("EdiJouhouDate"), ViewState("SyoriDate"), System.Configuration.ConfigurationManager.AppSettings("CsvDownMax"))
        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("HanyouUriageErrCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conHanyouUriageErrCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For Each row As Data.DataRow In dtHanyouSiireCsvInfo.Rows
            writer.WriteLine(row.Item("edi_jouhou_sakusei_date"), _
                                row.Item("gyou_no"), _
                                SetTimeType(row.Item("syori_datetime")), _
                                row.Item("torikesi"), _
                                row.Item("uriage_ten_kbn"), _
                                row.Item("uriage_ten_cd"), _
                                row.Item("seikyuu_saki_kbn"), _
                                row.Item("seikyuu_saki_cd"), _
                                row.Item("seikyuu_saki_brc"), _
                                row.Item("syouhin_cd"), _
                                row.Item("hin_mei"), _
                                row.Item("suu"), _
                                row.Item("tanka"), _
                                row.Item("syanai_genka"), _
                                row.Item("zei_kbn"), _
                                row.Item("syouhizei_gaku"), _
                                SetTimeType(row.Item("uri_date"), True), _
                                SetTimeType(row.Item("seikyuu_date"), True), _
                                SetTimeType(row.Item("denpyou_uri_date"), True), _
                                row.Item("uri_keijyou_flg"), _
                                SetTimeType(row.Item("uri_keijyou_date"), True), _
                                row.Item("kbn"), _
                                row.Item("bangou"), _
                                row.Item("sesyu_mei"), _
                                row.Item("tekiyou"), _
                                row.Item("add_login_user_id"), _
                                row.Item("add_login_user_name"), _
                                SetTimeType(row.Item("add_datetime"), True), _
                                row.Item("upd_login_user_id"), _
                                row.Item("upd_login_user_name"), _
                                SetTimeType(row.Item("upd_datetime")))
        Next

        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()
    End Sub

End Class