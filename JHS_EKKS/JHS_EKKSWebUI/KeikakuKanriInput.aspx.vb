Option Explicit On
Option Strict On

Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

''' <summary>
''' �v��Ǘ� CSV�捞
''' </summary>
''' <remarks></remarks>
Partial Class KeikakuKanriInput
    Inherits System.Web.UI.Page

#Region "�v���C�x�[�g�ϐ�"
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC
    Private objKeikakuKanriInputBC As New KeikakuKanriInputBC
    Private objCommon As New Common
    Private csvKbn As String
#End Region

#Region "�萔"
    Private Const csvKeikaku As String = "B1"           '�v��b�r�u�捞
    Private Const csvMikomi As String = "B2"            '�����b�r�u�捞
    Private Const csvFcKeikaku As String = "B3"         '�e�b�p�v��b�r�u�捞
    Private Const csvFcMikomi As String = "B4"          '�e�b�p�����b�r�u�捞
#End Region

#Region "�C�x���g"

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        '�����`�F�b�N
        Dim strUserID As String = ""

        Dim CommonCheck As New CommonCheck
        If CommonCheck.CommonNinsyou(strUserID, Master.loginUserInfo, kegen.KeikakuTorikomiKengen) Then
            Me.btnCsvInput.Button.Enabled = True
        Else
            Me.btnCsvInput.Button.Enabled = False
        End If

        If Not IsPostBack Then
            If Not Request.QueryString("csvKbn") Is Nothing AndAlso String.IsNullOrEmpty(hidCsvKbn.Text) Then
                hidCsvKbn.Text = Request.QueryString("csvKbn").ToString
            End If

            '��ʂ�ݒ�
            Call Me.SetGamen()
        End If

        'Javascript�쐬
        Call Me.MakeJavascript()

        ''DIV��\��
        'CloseCover()

        'CSV�捞�{�^��
        Me.btnCsvInput.Button.Attributes.Add("onClick", "if(!fncCheckCsvPath()){return false;}")
        '����飃{�^��
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")

        'CSV�捞�{�^����Click����
        Me.btnCsvInput.OnClick = "btnCsvInput_Click()"

    End Sub

    ''' <summary>
    ''' �uCSV�捞�v�{�^���N���b�N����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function btnCsvInput_Click() As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim strUmuFlg As String = String.Empty
        Dim strMessage As String = objKeikakuKanriInputBC.ChkCsvFile(Me.fupCsvInput, hidCsvKbn.Text, strUmuFlg)


        '��ʂ�ݒ�
        Call Me.SetGamen()

        If strMessage = String.Empty Then

            '�������b�Z�[�W��\������
            If strUmuFlg = "1" Then
                SetShowMessage(CommonMessage.MSG040E, String.Empty)
            Else
                SetShowMessage(CommonMessage.MSG039E, String.Empty)
            End If
        Else
            '�G���[���b�Z�[�W��\������
            SetShowMessage(strMessage, String.Empty)
        End If

        Return True
    End Function

    ''' <summary>
    ''' �f�[�^�s���f�[�^�Ƀo�C���h���ꂽ�Ƃ�����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Private Sub grdInputKanri_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInputKanri.RowDataBound
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = CDate(e.Row.Cells(0).Text).ToString("yyyy/MM/dd HH:mm:ss")
            Dim linkButton As Web.UI.WebControls.LinkButton = CType(e.Row.Cells(2).Controls(1), LinkButton)
            If linkButton.Text = "����" Then
                linkButton.Visible = False
                e.Row.Cells(2).Text = "����"
            Else
                Dim sendSearchTerms As String = CType(e.Row.Cells(2).Controls(5), HiddenField).Value & "$$$" & CType(e.Row.Cells(1).Controls(1), Label).Text _
                                                & "$$$" & CType(e.Row.Cells(2).Controls(3), HiddenField).Value
                linkButton.Attributes.Add("style", "text-decoration:underline; color:Blue; cursor:hand;")
                linkButton.Attributes.Add("onClick", "fncErrorDetails('" & sendSearchTerms & "');return false;")
            End If
        End If
    End Sub

#End Region

#Region "�����b�h"

    ''' <summary>
    ''' �N���C�A���g �X�N���v�g�� Page �I�u�W�F�N�g�ɓo�^����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Private Sub MakeJavascript()
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        Dim sbScript As New StringBuilder

        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '�G���[�m�F��ʂ��|�b�v�A�b�v����
            .AppendLine("   function fncErrorDetails(strValue){")
            .AppendLine("       var sendSearchTerms;")
            .AppendLine("       window.open('KeikakuKanriErrorDetails.aspx?sendSearchTerms='+encodeURIComponent(strValue),'ErrorDetails','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            'CLOSE�{�^������
            .AppendLine("   function fncClose(){")
            .AppendLine("       window.close();")
            .AppendLine("   }")
            '�uCSV�捞�v�{�^������������ꍇ�A���̓`�F�b�N
            .AppendLine("   function fncCheckCsvPath(){")
            .AppendLine("       var strId = '" & Me.fupCsvInput.ClientID & "';")
            .AppendLine("       var strPath = $ID('" & Me.fupCsvInput.ClientID & "').value;")
            .AppendLine("       if(strPath == ''){")
            .AppendLine("           alert('" & CommonMessage.MSG036E & "');")
            .AppendLine("           $ID(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�g���q�`�F�b�N
            .AppendLine("       if(strPath.toLowerCase().substr(strPath.length-4,strPath.length) != '.csv'){")
            .AppendLine("           alert('" & CommonMessage.MSG037E & "');")
            .AppendLine("           $ID(strId).focus();")
            .AppendLine("           $ID(strId).select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�t�@�C�����݃`�F�b�N
            .AppendLine("       if(!FileExist(strPath)){")
            .AppendLine("           alert('" & CommonMessage.MSG011E & "');")
            .AppendLine("           $ID(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�T�C�Y�`�F�b�N
            .AppendLine("       if(GetSize(strPath) == 0){")
            .AppendLine("           alert('" & CommonMessage.MSG060E & "');")
            .AppendLine("           $ID(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�m�F���b�Z�[�W
            Select Case hidCsvKbn.Text
                Case csvKeikaku
                    .AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, "@path", "�v��Ǘ�") & "'")
                Case csvMikomi
                    .AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, "@path", "�\�茩���Ǘ�") & "'")
                Case csvFcKeikaku
                    .AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, "@path", "FC�p�v��Ǘ�") & "'")
                Case csvFcMikomi
                    .AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, "@path", "FC�p�\�茩���Ǘ�") & "'")
            End Select
            .AppendLine("       strMsg = strMsg.replace('@path',strPath);")
            .AppendLine("       if(confirm(strMsg)){")
            .AppendLine("           return true;")
            .AppendLine("       }else{ ")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("   }")
            '�t�@�C�����݃`�F�b�N�֐�
            .AppendLine("   function FileExist(fPath){")
            .AppendLine("        if( (fPath.indexOf(':') < 0) && (fPath.substr(0,2) != '\\\\') ) ")
            .AppendLine("        { ")
            .AppendLine("           return false; ")
            .AppendLine("        } ")
            .AppendLine("       var sfso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("       if(sfso.FileExists(fPath)){")
            .AppendLine("           sfso=null;return true; ")
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
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>
    ''' ��ʂ�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Private Sub SetGamen()
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '�A�b�v�Ǘ��f�[�^���擾
        Dim dtInputKanri As New Data.DataTable
        dtInputKanri = objKeikakuKanriInputBC.GetInputKanri()

        If dtInputKanri.Rows.Count > 0 Then
            Me.grdInputKanri.DataSource = dtInputKanri
            Me.grdInputKanri.DataBind()

            '�������ʂ�ݒ�
            Dim strCount As String = objKeikakuKanriInputBC.GetInputKanriCount()

            If Convert.ToInt64(strCount) > 100 Then
                Me.lblCount.Text = "100/" & strCount
                '�ԐF
                Me.lblCount.ForeColor = Drawing.Color.Red
            Else
                Me.lblCount.Text = strCount
                '���F
                Me.lblCount.ForeColor = Drawing.Color.Black
            End If

        Else
            '�������ʂ�ݒ�
            Me.lblCount.Text = "0"
            '���F
            Me.lblCount.ForeColor = Drawing.Color.Black
        End If

        'CSV�捞�{�^���̖��̕\��
        Select Case hidCsvKbn.Text
            Case csvKeikaku
                btnCsvInput.Button.Text = "�v��CSV�捞"
            Case csvMikomi
                btnCsvInput.Button.Text = "����CSV�捞"
            Case csvFcKeikaku
                btnCsvInput.Button.Text = "FC�p�v��CSV�捞"
            Case csvFcMikomi
                btnCsvInput.Button.Text = "FC�p����CSV�捞"
            Case Else
                btnCsvInput.Button.Text = String.Empty.PadLeft(20, " "c)
                Me.btnCsvInput.Button.Enabled = False
        End Select
    End Sub

    '''' <summary>
    '''' DIV��\��
    '''' </summary>
    '''' <remarks></remarks>
    '''' <history>																
    '''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    '''' </history>
    'Public Sub CloseCover()
    '    'EMAB��Q�Ή����̊i�[����
    '    EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
    '                           MyMethod.GetCurrentMethod.Name)
    '    Dim csScript As New StringBuilder

    '    csScript.AppendLine("<script language='javascript' type='text/javascript'>")
    '    csScript.AppendLine("fncClosecover();")
    '    csScript.AppendLine("</script>")

    '    Page.ClientScript.RegisterStartupScript(Page.GetType, "CloseCover", csScript.ToString)
    'End Sub

    ''' <summary>���b�Z�[�W�\��</summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
    ''' <param name="strObjId">�N���C�A���gID</param>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Private Sub SetShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	window.name = 'EigyouKeikakuKanriMenu.aspx';setMenuBgColor();")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("  try{ document.getElementById('" & strObjId & "').select();}catch(e){}")
            End If
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub
#End Region
    
End Class
