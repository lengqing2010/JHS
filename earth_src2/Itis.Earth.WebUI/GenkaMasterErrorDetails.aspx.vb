Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class GenkaMasterErrorDetails
    Inherits System.Web.UI.Page

    ''' <summary>�����}�X�^�Ɖ�</summary>
    ''' <remarks>�����}�X�^�Ɖ�@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2011/03/01�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private genkaMasterLogic As New GenkaMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

    ''' <summary>�y�[�W���b�h</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "kaiseki_master_kanri_kengen")
        If Not blnEigyouKengen Then
            '�G���[��ʂ֑J�ڂ��āA�G���[���b�Z�[�W��\������
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If

        'javascript�쐬
        MakeJavascript()

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, strUserID)
            '������
            Call SetInitData()

        Else

            ''CSV�o�̓{�^������������ꍇ
            'If Me.hidCsvOut.Value = "1" Then

            '    'CSV�o��
            '    Call CsvOutPut()
            'End If
            ''DIV��\��
            'CloseCover()
        End If

        '����飃{�^������
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")
        '�CSV�o�ͣ�{�^������
        Me.btnCsvOutput.Attributes.Add("onClick", "if(!fncCsvOut()){return false;}")

    End Sub
    ''' <summary>CSV�o��</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click
        'Me.hidCsvOut.Value = "1"
        'ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.forms[0].submit();</script>")

        'CSV�o��
        Call CsvOutPut()
    End Sub

    '''' <summary>CSV�o��</summary>
    Private Sub CsvOutPut()
        '�̔����i�G���[�f�[�^���擾����
        Dim dtGenkaErrCsvInfo As New Data.DataTable
        '�̔����i�G���[CSV�f�[�^���擾����
        dtGenkaErrCsvInfo = genkaMasterLogic.SelGenkaErrCsv(CStr(ViewState("EdiJouhouDate")), CStr(ViewState("SyoriDate")))

        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("GenkaMasterErrCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conGenkaErrCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For i As Integer = 0 To dtGenkaErrCsvInfo.Rows.Count - 1
            With dtGenkaErrCsvInfo.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), .Item(7), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), .Item(12), .Item(13), .Item(14), .Item(15), .Item(16), .Item(17), .Item(18), .Item(19), _
                                 .Item(20), .Item(21), .Item(22), .Item(23), .Item(24), .Item(25), .Item(26), .Item(27), .Item(28), .Item(29), _
                                 .Item(30), .Item(31), .Item(32), .Item(33), .Item(34), .Item(35), .Item(36), .Item(37), .Item(38), .Item(39), _
                                 .Item(40), .Item(41), .Item(42), .Item(43), .Item(44), Me.SetTimeType(.Item(45).ToString), .Item(46), Me.SetTimeType(.Item(47).ToString))
            End With
        Next

        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()
    End Sub

    Private Function SetTimeType(ByVal strTime As String) As String
        If Not strTime.Trim.Equals(String.Empty) Then
            Return CDate(strTime).ToString("yyyy/MM/dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>�����f�[�^���Z�b�g����</summary>
    Protected Sub SetInitData()

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
        Dim dtGenkaInfo As New Data.DataTable
        dtGenkaInfo = genkaMasterLogic.GetGenkaErr(strEdiDate, strSyoriDate)
        '�������ʂ�ݒ肷��
        Me.grdBodyLeft.DataSource = dtGenkaInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtGenkaInfo
        Me.grdBodyRight.DataBind()
        '�������ʌ������擾����
        Dim intCount As Integer = genkaMasterLogic.GetGenkaErrCount(strEdiDate, strSyoriDate)
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
            Call Me.SetKingaku()
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
            .AppendLine("   document.all." & Me.hidCsvOut.ClientID & ".value='';")
            .AppendLine("   if(document.getElementById('" & Me.hidCsvCount.ClientID & "').value > " & CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "){")
            .AppendLine("       if(confirm('" & Messages.Instance.MSG013C.Replace("@PARAM1", System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "')){")
            .AppendLine("           return true;")
            .AppendLine("       }else{")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("   else ")
            .AppendLine("   { ")
            .AppendLine("       return true; ")
            .AppendLine("   } ")
            .AppendLine("}")

            'DIV�\��
            .AppendLine("   function fncShowModal(){")
            .AppendLine("      var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("      var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("      if(buyDiv.style.display=='none')")
            .AppendLine("      {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("      }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("      }")
            .AppendLine("   }")
            'DIV��\��
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            '.AppendLine("alert('fncClosecover');")
            .AppendLine("   }")
            .AppendLine("</script>")

        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>DIV��\��</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub


    ''' <summary>������ݒ�</summary>
    Public Sub SetKingaku()

        Dim numIndex() As Integer = {1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29}

        Dim rowCount As Integer

        For rowCount = 0 To Me.grdBodyRight.Rows.Count - 1
            For Each i As Integer In numIndex
                If Me.grdBodyRight.Rows(rowCount).Cells(i).Text.ToString.Trim.Equals("&nbsp;") Then
                    Me.grdBodyRight.Rows(rowCount).Cells(i).Text = ""
                ElseIf commonCheck.CheckHankaku(Me.grdBodyRight.Rows(rowCount).Cells(i).Text, "", "") = String.Empty Then
                    Me.grdBodyRight.Rows(rowCount).Cells(i).Text = FormatNumber(Me.grdBodyRight.Rows(rowCount).Cells(i).Text, 0)
                End If
            Next
        Next

    End Sub

End Class