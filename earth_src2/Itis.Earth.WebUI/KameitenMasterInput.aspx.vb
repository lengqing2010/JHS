Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class KameitenMasterInput
    Inherits System.Web.UI.Page

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�捞</summary>
    ''' <remarks>�����X���i�������@���ʑΉ��}�X�^�捞�p�@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2011/03/08�@�W���o�t(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private kameitenMasterBC As New KameitenMasterLogic
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

        'Javascript�쐬
        Call Me.MakeJavascript()

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            CommonCheck.SetURL(Me, strUserID)
            '��ʂ�ݒ�
            Call Me.SetGamen()
        Else
            'DIV��\��
            CloseCover()
        End If

        Me.btnCsvInput.Attributes.Add("onClick", "if(!fncCheckCsvPath()){return false;}else{fncShowModal();}")
        '����飃{�^��
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")

    End Sub

    ''' <summary>��ʂ�ݒ�</summary>
    Private Sub SetGamen()

        '�A�b�v�Ǘ��f�[�^���擾
        Dim dtInputKanri As New Data.DataTable
        dtInputKanri = kameitenMasterBC.GetInputKanri()

        If dtInputKanri.Rows.Count > 0 Then
            Me.grdInputKanri.DataSource = dtInputKanri
            Me.grdInputKanri.DataBind()


            '�������ʂ�ݒ�
            Dim intCount As Integer = kameitenMasterBC.GetInputKanriCount()

            If intCount > 100 Then
                Me.lblCount.Text = "100/" & CStr(intCount)
                '�ԐF
                Me.lblCount.ForeColor = Drawing.Color.Red
            Else
                Me.lblCount.Text = CStr(intCount)
                '���F
                Me.lblCount.ForeColor = Drawing.Color.Black
            End If

        Else
            '�������ʂ�ݒ�
            Me.lblCount.Text = "0"
            '���F
            Me.lblCount.ForeColor = Drawing.Color.Black
        End If

    End Sub


    Private Sub btnCsvInput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvInput.Click
        Dim intLineNo As Integer = 1
        Dim exitsFlg As Boolean = False
        Dim strMsg As String
        Dim kbnAndKameitenCd As String = ""
        Dim arrCsvLine() As String
        Dim csScript As New StringBuilder
        Call Me.SetGamen()
        Dim insCd As String, updCd As String
        Dim hidInsLineNo As String = ""

       
        '�����X���݃`�F�b�N
        'strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, intLineNo, exitsFlg, kbnAndKameitenCd, hidInsLineNo.Value, arrCsvLine)
        strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, arrCsvLine, insCd, updCd, hidInsLineNo)

        '�捞�t�@�C���ۑ�
        ViewState("arrCsvLine") = arrCsvLine
        ViewState("FileName") = Me.fupCsvinput.FileName
        ViewState("hidInsLineNo") = hidInsLineNo


        If strMsg = "Success" Then
            If insCd <> "" AndAlso updCd <> "" Then
                With csScript
                    '�m�F���b�Z�[�W
                    '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
                    .AppendLine("   var strMsg = '�V�K�o�^�̎��ێҏ�񂪂���܂��B�V�K�o�^���s���܂����H\n���Ώۂ̉����X�R�[�h:" & insCd & "\n�����̉����X�R�[�h�����݂��܂����㏑���Ă�낵���ł��傤���H\n���Ώۂ̉����X�R�[�h:" & updCd & "'")
                    .AppendLine("   if(confirm(strMsg)){")
                    .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
                    .AppendLine("   }else{")
                    .AppendLine("   }")
                End With
            ElseIf insCd <> "" Then
                With csScript
                    '�m�F���b�Z�[�W
                    '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
                    .AppendLine("   var strMsg = '�V�K�o�^�̎��ێҏ�񂪂���܂��B�V�K�o�^���s���܂����H\n���Ώۂ̉����X�R�[�h:" & insCd & "'")
                    .AppendLine("   if(confirm(strMsg)){")
                    .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
                    .AppendLine("   }else{")
                    .AppendLine("   }")
                End With
            ElseIf updCd <> "" Then
                With csScript
                    '�m�F���b�Z�[�W
                    '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
                    .AppendLine("   var strMsg = '�����̉����X�R�[�h�����݂��܂����㏑���Ă�낵���ł��傤���H\n���Ώۂ̉����X�R�[�h:" & updCd & "'")
                    .AppendLine("   if(confirm(strMsg)){")
                    .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
                    .AppendLine("   }else{")
                    .AppendLine("   }")
                End With

            End If
        Else
            With csScript
                .AppendLine(" alert('" & strMsg & "');")
            End With

        End If

        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)









        'If "UnExits".Equals(strMsg) OrElse "Err".Equals(strMsg) OrElse "Success".Equals(strMsg) Then
        'Else
        '    ShowMessage(strMsg)
        '    hidLineNo.Value = String.Empty
        '    'hidExitsFlg.Value = String.Empty
        '    hidInsLineNo.Value = String.Empty
        '    ViewState("arrCsvLine") = Nothing
        'End If
        'If "UnExits".Equals(strMsg) Then
        '    '�敪:����۰��̧��.�敪(�����ۂ̒l) -�����X����:����۰��̧��.�����X����(�����ۂ̒l)
        '    Dim kbn As String = kbnAndKameitenCd.Split(",")(0).Trim()
        '    Dim kameitenCd As String = kbnAndKameitenCd.Split(",")(1).Trim()

        '    With csScript
        '        '�m�F���b�Z�[�W
        '        '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
        '        .AppendLine("   var strMsg = '" & String.Format(Messages.Instance.MSG2065E, kbn, kameitenCd) & "'")
        '        .AppendLine("   if(confirm(strMsg)){")
        '        If "Err".Equals(strMsg) Then
        '            .AppendLine(" alert('" & String.Format(Messages.Instance.MSG2066E) & "');")
        '        Else
        '            .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        '        End If
        '        .AppendLine("   }else{")
        '        .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")
        '        '.AppendLine("   document.getElementById ('" & hidExitsFlg.ClientID & "').value = ''")
        '        .AppendLine("   document.getElementById ('" & hidInsLineNo.ClientID & "').value = ''")
        '        .AppendLine("   }")
        '    End With
        '    '�G���[�̍s����ۑ�
        '    hidLineNo.Value = intLineNo
        '    '�g�����݂��Ȃ��̏ꍇ�����X�R�[�h���݂��ǂ���ۑ�
        '    'hidExitsFlg.Value = exitsFlg
        '    '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        '    ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)

        'Else

        '    With csScript
        '        '�m�F���b�Z�[�W
        '        '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
        '        .AppendLine("   var strMsg = '�����̉����X�R�[�h�����݂��܂����㏑���Ă�낵���ł��傤���H '")
        '        .AppendLine("   if(confirm(strMsg)){")
        '        If "Err".Equals(strMsg) Then
        '            .AppendLine(" alert('" & String.Format(Messages.Instance.MSG2066E) & "');")
        '        Else
        '            .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        '        End If
        '        .AppendLine("   }else{")
        '        .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")
        '        '.AppendLine("   document.getElementById ('" & hidExitsFlg.ClientID & "').value = ''")
        '        .AppendLine("   document.getElementById ('" & hidInsLineNo.ClientID & "').value = ''")
        '        .AppendLine("   }")
        '    End With
        '    '�G���[�̍s����ۑ�
        '    hidLineNo.Value = intLineNo
        '    '�g�����݂��Ȃ��̏ꍇ�����X�R�[�h���݂��ǂ���ۑ�
        '    'hidExitsFlg.Value = exitsFlg
        '    '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        '    ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)

        'End If
        ''Else
        ''    arrCsvLine = ViewState("arrCsvLine")
        ''    intLineNo = hidLineNo.Value + 1
        ''    '�����X���݃`�F�b�N
        ''    strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, intLineNo, exitsFlg, kbnAndKameitenCd, hidInsLineNo.Value, arrCsvLine)
        ''    '�捞�t�@�C���ۑ�
        ''    ViewState("arrCsvLine") = arrCsvLine
        ''    If "UnExits".Equals(strMsg) Then
        ''        '�敪:����۰��̧��.�敪(�����ۂ̒l) -�����X����:����۰��̧��.�����X����(�����ۂ̒l)
        ''        Dim kbn As String = kbnAndKameitenCd.Split(",")(0).Trim()
        ''        Dim kameitenCd As String = kbnAndKameitenCd.Split(",")(1).Trim()
        ''        With csScript
        ''            '�m�F���b�Z�[�W
        ''            '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
        ''            .AppendLine("   var strMsg = '�敪-�����X���ނ̑g�������Ȃ����̂����݂��܂��B�V�K�o�^���s���܂����B" & kbn & "-" & kameitenCd & "'")
        ''            .AppendLine("   if(confirm(strMsg)){")
        ''            .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        ''            .AppendLine("   }else{")
        ''            .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")
        ''            .AppendLine("   document.getElementById ('" & hidExitsFlg.ClientID & "').value = ''")
        ''            .AppendLine("   }")
        ''        End With
        ''        '�G���[�̍s����ۑ�
        ''        hidLineNo.Value = intLineNo
        ''        '�g�����݂��Ȃ��̏ꍇ�����X�R�[�h���݂��ǂ���ۑ�
        ''        hidExitsFlg.Value = exitsFlg
        ''        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ''        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
        ''    End If
        ''End If
        ''End If

        ''If "Success".Equals(strMsg) Then
        ''    Dim strUmuFlg As String = String.Empty
        ''    Dim strMessage As String = kameitenMasterBC.ChkCsvFile(strUmuFlg, hidInsLineNo.Value, arrCsvLine, ViewState("FileName").ToString)

        ''    '��ʂ�ݒ�

        ''    Call Me.SetGamen()
        ''    If strMessage = String.Empty Then

        ''        '�������b�Z�[�W��\������
        ''        If strUmuFlg = "1" Then
        ''            ShowMessage(Messages.Instance.MSG047C)
        ''        Else
        ''            ShowMessage(Messages.Instance.MSG046C)
        ''        End If
        ''    Else
        ''        ShowMessage(strMessage)
        ''    End If
        ''    hidLineNo.Value = String.Empty
        ''    'hidExitsFlg.Value = String.Empty
        ''    hidInsLineNo.Value = String.Empty
        ''    ViewState("arrCsvLine") = Nothing
        ''End If


    End Sub
    Private Sub btnCsvCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvCheck.Click
        Dim intLineNo As Integer = 0
        Dim exitsFlg As Boolean = False
        Dim strMsg As String
        Dim kbnAndKameitenCd As String = ""
        Dim arrCsvLine() As String
        Dim csScript As New StringBuilder
        Call Me.SetGamen()
        'If Not String.IsNullOrEmpty(hidExitsFlg.Value) AndAlso "True".Equals(hidExitsFlg.Value) Then
        '    ShowMessage("�V�K�o�^�̉����X���ނ��d�����Ă܂�")
        '    hidLineNo.Value = String.Empty
        '    hidExitsFlg.Value = String.Empty
        '    hidInsLineNo.Value = String.Empty
        '    ViewState("arrCsvLine") = Nothing
        'Else
        ' If String.IsNullOrEmpty(hidLineNo.Value) Then
        'arrCsvLine = ViewState("arrCsvLine")
        'intLineNo = hidLineNo.Value + 1
        ''�����X���݃`�F�b�N
        'strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, intLineNo, exitsFlg, kbnAndKameitenCd, hidInsLineNo.Value, arrCsvLine)
        ''�捞�t�@�C���ۑ�
        ''ViewState("arrCsvLine") = arrCsvLine
        'If "UnExits".Equals(strMsg) OrElse "Err".Equals(strMsg) Then
        '    '�敪:����۰��̧��.�敪(�����ۂ̒l) -�����X����:����۰��̧��.�����X����(�����ۂ̒l)
        '    Dim kbn As String = kbnAndKameitenCd.Split(",")(0).Trim()
        '    Dim kameitenCd As String = kbnAndKameitenCd.Split(",")(1).Trim()

        '    With csScript
        '        '�m�F���b�Z�[�W
        '        .AppendLine("   var strMsg = '" & String.Format(Messages.Instance.MSG2065E, kbn, kameitenCd) & "'")
        '        .AppendLine("   if(confirm(strMsg)){")
        '        If "Err".Equals(strMsg) Then
        '            .AppendLine(" alert('" & String.Format(Messages.Instance.MSG2066E) & "');")
        '        Else
        '            .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        '        End If
        '        .AppendLine("   }else{")
        '        .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")
        '        '.AppendLine("   document.getElementById ('" & hidExitsFlg.ClientID & "').value = ''")
        '        .AppendLine("   document.getElementById ('" & hidInsLineNo.ClientID & "').value = ''")
        '        .AppendLine("   }")
        '    End With
        '    '�G���[�̍s����ۑ�
        '    hidLineNo.Value = intLineNo

        '    '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        '    ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
        'End If
        'Else
        'arrCsvLine = ViewState("arrCsvLine")
        'intLineNo = hidLineNo.Value + 1
        ''�����X���݃`�F�b�N
        'strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, intLineNo, exitsFlg, kbnAndKameitenCd, hidInsLineNo.Value, arrCsvLine)
        ''�捞�t�@�C���ۑ�
        'ViewState("arrCsvLine") = arrCsvLine
        'If "UnExits".Equals(strMsg) Then
        '    '�敪:����۰��̧��.�敪(�����ۂ̒l) -�����X����:����۰��̧��.�����X����(�����ۂ̒l)
        '    Dim kbn As String = kbnAndKameitenCd.Split(",")(0).Trim()
        '    Dim kameitenCd As String = kbnAndKameitenCd.Split(",")(1).Trim()
        '    With csScript
        '        '�m�F���b�Z�[�W
        '        '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
        '        .AppendLine("   var strMsg = '�敪-�����X���ނ̑g�������Ȃ����̂����݂��܂��B�V�K�o�^���s���܂����B" & kbn & "-" & kameitenCd & "'")
        '        .AppendLine("   if(confirm(strMsg)){")
        '        .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        '        .AppendLine("   }else{")
        '        .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")

        '        .AppendLine("   }")
        '    End With
        '    '�G���[�̍s����ۑ�
        '    hidLineNo.Value = intLineNo

        '    '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        '    ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
        'End If
        'End If
        'End If

        'If "Success".Equals(strMsg) Then
        Dim strUmuFlg As String = String.Empty
        Dim strMessage As String = kameitenMasterBC.ChkCsvFile(strUmuFlg, ViewState("hidInsLineNo").ToString, ViewState("arrCsvLine"), ViewState("FileName").ToString)

        '��ʂ�ݒ�
        Call Me.SetGamen()

        If strMessage = String.Empty Then

            '�������b�Z�[�W��\������
            If strUmuFlg = "1" Then
                ShowMessage(Messages.Instance.MSG047C)
            Else
                ShowMessage(Messages.Instance.MSG046C)
            End If
        Else
            ShowMessage(strMessage)
        End If

        ViewState("hidInsLineNo") = ""

        'hidLineNo.Value = String.Empty
        'hidExitsFlg.Value = String.Empty
        'hidInsLineNo.Value = String.Empty

        ViewState("arrCsvLine") = Nothing
        ' End If
    End Sub

    Private Sub grdInputKanri_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInputKanri.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = CDate(e.Row.Cells(0).Text).ToString("yyyy/MM/dd HH:mm:ss")
            Dim linkButton As Web.UI.WebControls.LinkButton = CType(e.Row.Cells(2).Controls(1), LinkButton)
            If linkButton.Text = "�Ȃ�" Then
                linkButton.Visible = False
                e.Row.Cells(2).Text = "�Ȃ�"
            Else
                Dim sendSearchTerms As String = CType(e.Row.Cells(2).Controls(5), HiddenField).Value & "$$$" & CType(e.Row.Cells(1).Controls(1), Label).Text _
                                                & "$$$" & CType(e.Row.Cells(2).Controls(3), HiddenField).Value
                linkButton.Attributes.Add("style", "text-decoration:underline; color:Blue; cursor:hand;")
                linkButton.Attributes.Add("onClick", "fncErrorDetails('" & sendSearchTerms & "');return false;")
            End If
        End If
    End Sub

    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '�G���[�m�F��ʂ��|�b�v�A�b�v����
            .AppendLine("   function fncErrorDetails(strValue){")
            .AppendLine("       var sendSearchTerms;")
            .AppendLine("       window.open('KameitenMasterErrorDetails.aspx?sendSearchTerms='+encodeURIComponent(strValue),'ErrorDetails','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            'CLOSE�{�^������
            .AppendLine("   function fncClose(){")
            .AppendLine("       window.close();")
            .AppendLine("   }")
            '�uCSV�捞�v�{�^������������ꍇ�A���̓`�F�b�N
            .AppendLine("   function fncCheckCsvPath(){")
            .AppendLine("       var strId = '" & Me.fupCsvinput.ClientID & "';")
            .AppendLine("       var strPath = document.getElementById('" & Me.fupCsvinput.ClientID & "').value;")
            .AppendLine("       if(strPath == ''){")
            .AppendLine("           alert('" & Messages.Instance.MSG042E & "');")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�g���q�`�F�b�N
            .AppendLine("       if(right(strPath.toLowerCase(),4) != "".csv""){")
            .AppendLine("           alert('" & Messages.Instance.MSG043E & "');")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           document.getElementById(strId).select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�t�@�C�����݃`�F�b�N
            .AppendLine("       if(!FileExist(strPath)){")
            .AppendLine("           alert('" & Messages.Instance.MSG039E & "');")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�T�C�Y�`�F�b�N
            .AppendLine("       if(GetSize(strPath) == 0){")
            .AppendLine("           alert('" & Messages.Instance.MSG044E & "');")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�m�F���b�Z�[�W
            .AppendLine("       if(confirm('" & Messages.Instance.MSG054C.Replace("@PARAM1", "�����X���ꊇ�捞") & "'.replace('@PARAM2',strPath))){")
            .AppendLine("           return true;}")
            .AppendLine("       else{")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           return false;}")
            .AppendLine("   }")
            '�t�@�C�����݃`�F�b�N�֐�
            .AppendLine("   function FileExist(fPath){")
            .AppendLine("        if( (fPath.indexOf(':') < 0) && (left(fPath,2) != '\\\\') ) ")
            .AppendLine("        { ")
            .AppendLine("           return false; ")
            .AppendLine("        } ")
            .AppendLine("       var sfso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("       if(sfso.FileExists(fPath)){")
            .AppendLine("            sfso=null;return true; ")
            .AppendLine("       }")
            .AppendLine("       else{")
            .AppendLine("           sfso=null;return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            '�t�@�C���T�C�Y���擾
            .AppendLine("   function GetSize(files){")
            .AppendLine("       var fso,f;")
            .AppendLine("	    var fSize;   ")
            .AppendLine("       fso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("       f=fso.GetFile(files);")
            .AppendLine("       fSize = f.size; ")
            .AppendLine("       sfso=null;f=null; ")
            .AppendLine("	  return fSize; ")
            .AppendLine("   }")
            .AppendLine("   function LTrim(str){")
            .AppendLine("       var i;")
            .AppendLine("       for(i=0;i<str.length;i++){")
            .AppendLine("            if(str.charAt(i)!=' '&&str.charAt(i)!='�@')break;")
            .AppendLine("       }")
            .AppendLine("       str=str.substring(i,str.length);")
            .AppendLine("       return str;")
            .AppendLine("   }")
            .AppendLine("   function RTrim(str){")
            .AppendLine("       var i;")
            .AppendLine("       for(i=str.length-1;i>=0;i--)")
            .AppendLine("           {if(str.charAt(i)!=' '&&str.charAt(i)!='�@')break;}")
            .AppendLine("       str=str.substring(0,i+1);")
            .AppendLine("       return str;")
            .AppendLine("   }")
            '�X�y�[�X���폜
            .AppendLine("   function Trim(str){")
            .AppendLine("       return LTrim(RTrim(str));")
            .AppendLine("   }")
            .AppendLine("   function left(mainStr,lngLen){")
            .AppendLine("       if (lngLen>0) {return mainStr.substring(0,lngLen);}")
            .AppendLine("       else{return null;}")
            .AppendLine("   }")
            .AppendLine("   function right(mainStr,lngLen){")
            .AppendLine("       if (mainStr.length-lngLen>=0 && mainStr.length>=0 && mainStr.length-lngLen<=mainStr.length){")
            .AppendLine("           return mainStr.substring(mainStr.length-lngLen,mainStr.length);}")
            .AppendLine("       else{return null;}")
            .AppendLine("   }")
            .AppendLine("   function mid(mainStr,starnum,endnum){")
            .AppendLine("       if (mainStr.length>=0){")
            .AppendLine("           return mainStr.substr(starnum,endnum);")
            .AppendLine("       }")
            .AppendLine("       else{return null;}")
            .AppendLine("   }")
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
            .AppendLine("   }")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
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

    ''' <summary>DIV��\��</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

End Class