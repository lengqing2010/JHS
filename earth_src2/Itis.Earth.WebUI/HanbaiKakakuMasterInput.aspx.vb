Imports Itis.Earth.BizLogic
Partial Public Class HanbaiKakakuMasterInput
    Inherits System.Web.UI.Page

    ''' <summary>�̔��}�X�^�捞</summary>
    ''' <remarks>�����}�X�^�捞�p�@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2011/03/07�@���c(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private hanbaiKakakuInputLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck

    ''' <summary>�y�[�W���b�h</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen")
        If Not blnEigyouKengen Then
            '�G���[��ʂ֑J�ڂ��āA�G���[���b�Z�[�W��\������
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            CommonCheck.SetURL(Me, strUserID)
            '������
            Call SetInitData()
        End If
        'javascript�쐬
        MakeJavascript()
        '�uCSV�捞�v�{�^������������ꍇ�A���̓`�F�b�N
        Me.btnCsvUpload.Attributes.Add("onClick", "if(fncCheckCsvPath()){fncShowModal();}else{return false;}")
        '�u����v�{�^������
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")

    End Sub
    ''' <summary>�����f�[�^���Z�b�g����</summary>
    Protected Sub SetInitData()

        Dim dtUploadKanri As New Data.DataTable
        Dim intCount As Integer

        '�A�b�v�Ǘ��f�[�^���擾
        dtUploadKanri = hanbaiKakakuInputLogic.GetUploadKanri()
        If dtUploadKanri.Rows.Count > 0 Then
            Me.grdUploadKanri.DataSource = dtUploadKanri
            Me.grdUploadKanri.DataBind()
        End If
        '�A�b�v���[�h�Ǘ��f�[�^�������擾����
        intCount = hanbaiKakakuInputLogic.GetUploadKanriCount()
        '�������ʌ�����ݒ肷��
        Call SetKensakuCount(intCount)

    End Sub
    ''' <summary>�������ʌ�����ݒ肷��</summary>
    ''' <param name="intCount">�������ʌ���</param>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If intCount > 100 Then
            Me.lblCount.Text = "100 / " & intCount
            Me.lblCount.ForeColor = Drawing.Color.Red
        Else
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
        End If

    End Sub
    ''' <summary>�捞�G���[�L����ݒ肷��</summary>
    Private Sub grdUploadKanri_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUploadKanri.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblUmu As Web.UI.WebControls.Label = CType(e.Row.Cells(2).Controls(1), Label)
            If lblUmu.Text = "����" Then
                lblUmu.Attributes.Add("style", "text-decoration:underline; color:Blue; cursor:hand;")
                lblUmu.Attributes.Add("onClick", "fncErrorDetails('" & e.Row.Cells(3).Text & "','" & CType(e.Row.Cells(1).Controls(1), Label).Text & "','" & e.Row.Cells(4).Text & "');return false;")
            End If
            e.Row.Cells(3).Attributes.Add("style", "display:none;")
            e.Row.Cells(4).Attributes.Add("style", "display:none;")
        End If
    End Sub
    ''' <summary>CSV�捞�{�^������</summary>
    Private Sub btnCsvUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvUpload.Click

        Dim strErrMessage As String
        Dim strUmuFlg As String = ""
        strErrMessage = hanbaiKakakuInputLogic.ChkCsvFile(Me.fupCsvUpload, strUmuFlg)
        If strErrMessage = String.Empty Then
            '�捞�f�[�^��\������
            Call SetInitData()
            '�������b�Z�[�W��\������
            If strUmuFlg = "1" Then
                ShowMessage(Messages.Instance.MSG047C)
            Else
                ShowMessage(Messages.Instance.MSG046C)
            End If
        Else
            ShowMessage(strErrMessage)
        End If
    End Sub
    ''' <summary>���b�Z�[�W�\��</summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
    Private Sub ShowMessage(ByVal strMessage As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
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
            '�N���A�{�^������
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")
            '�u����v�����N����������ꍇ�A�̔����i�}�X�^�G���[�m�F�|�b�v�A�b�v��ʂ��N������
            .AppendLine("function fncErrorDetails(strSyoriDatetime,strNyuuryokuFileMei,strEdiJouhouDate){")
            .AppendLine("   window.open('HanbaiKakakuMasterErrorDetails.aspx?sendSearchTerms='+strSyoriDatetime+'$$$'+encodeURIComponent(strNyuuryokuFileMei)+'$$$'+strEdiJouhouDate, 'hanbaiKakakuWindow')")
            .AppendLine("   return false;")
            .AppendLine("}")
            '�uCSV�捞�v�{�^������������ꍇ�A���̓`�F�b�N
            .AppendLine("function fncCheckCsvPath(){")
            .AppendLine("   var strPath = document.getElementById('" & Me.fupCsvUpload.ClientID & "').value;")
            '�K�{���̓`�F�b�N
            .AppendLine("   if(strPath==''){")
            .AppendLine("       alert('" & Messages.Instance.MSG042E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '�g���q�`�F�b�N
            .AppendLine("   if(strPath.length < 4){")
            .AppendLine("       alert('" & Messages.Instance.MSG043E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false;")
            .AppendLine("   }else{")
            .AppendLine("       if(strPath.substring(strPath.length-4,strPath.length).toLowerCase() != "".csv""){")
            .AppendLine("           alert('" & Messages.Instance.MSG043E & "');")
            .AppendLine("           document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("   }")
            '�t�@�C�����݃`�F�b�N
            .AppendLine("   var fso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("   if( (strPath.indexOf(':') < 0) && (strPath.substring(0,2) != '\\\\') ){ ")
            .AppendLine("       alert('" & Messages.Instance.MSG039E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }else if(!fso.FileExists(strPath)){")
            .AppendLine("       alert('" & Messages.Instance.MSG039E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '�e�ʃ`�F�b�N
            .AppendLine("   if(fso.GetFile(strPath).size == 0){")
            .AppendLine("       alert('" & Messages.Instance.MSG044E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '�m�F���b�Z�[�W
            .AppendLine("   var strMsg = '" & Messages.Instance.MSG045C.Replace("@PARAM1", "�̔����i") & "'")
            .AppendLine("   strMsg = strMsg.replace('@PARAM2',strPath);")
            .AppendLine("   if(confirm(strMsg)){")
            .AppendLine("       document.forms[0].submit();")
            .AppendLine("   }else{")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   return true;")
            .AppendLine("}")
            'DIV�\��
            .AppendLine("function fncShowModal(){")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("   if(buyDiv.style.display=='none')")
            .AppendLine("   {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("   }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub
End Class