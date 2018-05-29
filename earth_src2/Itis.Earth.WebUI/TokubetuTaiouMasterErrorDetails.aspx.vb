Imports Itis.Earth.BizLogic

Partial Public Class TokubetuTaiouMasterErrorDetails
    Inherits System.Web.UI.Page

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�G���[�m�F</summary>
    ''' <remarks>�����X���i�������@���ʑΉ��}�X�^�G���[�m�F�@�\��񋟂���</remarks>
    ''' <history>2011�N3��11���@�W���o�t�i��A���V�X�e�����j�V�K�쐬</history>
    Private tokubetuTaiouMasterLogic As New TokubetuTaiouMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

    ''' <summary>�y�[�W���b�h</summary> 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen,kaiseki_master_kanri_kengen")
        If Not blnEigyouKengen Then
            '�G���[��ʂ֑J�ڂ��āA�G���[���b�Z�[�W��\������
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, strUserID)

            '������
            Call SetInitData()
        End If
        'javascript�쐬
        MakeJavascript()
        '����飃{�^������
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")
        '�CSV�o�ͣ�{�^������
        Me.btnCsvOutput.Attributes.Add("onClick", "if(!fncCsvOut()){return false;}")
    End Sub

    ''' <summary>CSV�o�̓{�^���̏���</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        '���ʑΉ��G���[CSV�f�[�^���擾����
        Dim dtTokubetuTaiouErrorCSV As New Data.DataTable
        dtTokubetuTaiouErrorCSV = tokubetuTaiouMasterLogic.GetTokubetuTaiouErrorCSV(CStr(ViewState("EdiJouhouDate")), CStr(ViewState("SyoriDate")))
        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("TokubetuTaiouMasterErrCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conTokubetuTaiouErrCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For Each row As Data.DataRow In dtTokubetuTaiouErrorCSV.Rows
            writer.WriteLine(row.Item("edi_jouhou_sakusei_date"), row.Item("gyou_no"), Me.SetTimeType(row.Item("syori_datetime").ToString), row.Item("aitesaki_syubetu"), _
                                        row.Item("aitesaki_cd"), row.Item("aitesaki_mei"), row.Item("syouhin_cd"), row.Item("syouhin_mei"), row.Item("tys_houhou_no"), _
                                        row.Item("tys_houhou"), row.Item("tokubetu_taiou_cd"), row.Item("tokubetu_taiou_meisyou"), row.Item("torikesi"), _
                                        row.Item("kasan_syouhin_cd"), row.Item("kasan_syouhin_mei"), row.Item("syokiti"), row.Item("uri_kasan_gaku"), row.Item("koumuten_kasan_gaku"), _
                                        row.Item("add_login_user_id"), Me.SetTimeType(row.Item("add_datetime").ToString), row.Item("upd_login_user_id"), Me.SetTimeType(row.Item("upd_datetime").ToString))
        Next

        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    ''' <summary>
    ''' ���t�`���ݒ�
    ''' </summary>
    Private Function SetTimeType(ByVal strTime As String) As String
        If Not strTime.Trim.Equals(String.Empty) Then
            Return CDate(strTime).ToString("yyyy/MM/dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' ��ʏ�����
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetInitData()

        Dim strSyoriDate As String = String.Empty   '��������
        Dim strEdiDate As String    'EDI���쐬��

        If Not Request("sendSearchTerms") Is Nothing Then
            strSyoriDate = Split(Request("sendSearchTerms").ToString, "$$$")(0)
            Me.lblSyoriDate.Text = Left(Split(strSyoriDate, "$$$")(0), 4)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(4, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(6, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & " " & strSyoriDate.Substring(8, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(10, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(12, 2) '��������
            Me.lblFileMei.Text = Split(Request("sendSearchTerms").ToString, "$$$")(1)   '���̓t�@�C����
            Me.lblFileMei.ToolTip = Split(Request("sendSearchTerms").ToString, "$$$")(1)
            strEdiDate = Split(Request("sendSearchTerms").ToString, "$$$")(2)           'EDI���쐬��
        Else
            Me.lblSyoriDate.Text = String.Empty     '��������
            Me.lblFileMei.Text = String.Empty       '���̓t�@�C����
            strEdiDate = String.Empty               'EDI���쐬��
        End If

        '�����f�[�^���擾����
        Dim dtTokubetuTaiouError As New Data.DataTable
        dtTokubetuTaiouError = tokubetuTaiouMasterLogic.GetTokubetuTaiouError(strEdiDate, strSyoriDate)
        '�������ʂ�ݒ肷��
        Me.grdMeisaiLeft.DataSource = dtTokubetuTaiouError
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = dtTokubetuTaiouError
        Me.grdMeisaiRight.DataBind()
        '�������ʌ������擾����
        Dim intCount As Integer = tokubetuTaiouMasterLogic.GetTokubetuTaiouErrorCount(strEdiDate, strSyoriDate)

        '�������ʌ�����ݒ肷��
        SetKensakuCount(intCount)
        'CSV�f�[�^������ݒ肷��
        Me.hidCSVCount.Value = intCount
        ViewState("EdiJouhouDate") = strEdiDate
        ViewState("SyoriDate") = strSyoriDate
        '�f�[�^��0���̏ꍇ�A�G���[���b�Z�[�W��\������
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' �������ʂ�ݒ�
    ''' </summary>
    Private Sub SetKensakuCount(ByVal intCount As Integer)
        If intCount > 100 Then
            Me.lblCount.Text = "100 / " & CStr(intCount)
            Me.lblCount.ForeColor = Drawing.Color.Red
            scrollHeight = 100 * 22 + 1
        Else
            Me.lblCount.Text = CStr(intCount)
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
            '�߂�{�^���̏���
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")
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
            '.AppendLine("function fncScrollV(){")
            '.AppendLine("   var divbody=" & Me.divMeisai.ClientID & ";")
            '.AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            '.AppendLine("   divbody.scrollTop = divVscroll.scrollTop;")
            '.AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divMeisaiLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divMeisaiRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divMeisaiRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("}")
            'CSV�o�̓{�^������������ꍇ�A�m�F���b�Z�[�W��\������B
            .AppendLine("function fncCsvOut(){")
            .AppendLine("   document.all." & Me.hidCsvOut.ClientID & ".value='';")
            .AppendLine("   if(document.getElementById('" & Me.hidCSVCount.ClientID & "').value > " & CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "){")
            .AppendLine("       if(confirm('" & Messages.Instance.MSG013C.Replace("@PARAM1", System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "')){")
            .AppendLine("           return ture;")
            .AppendLine("       }else{")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("   else ")
            .AppendLine("   { ")
            .AppendLine("       return true; ")
            .AppendLine("   } ")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisaiRight.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim str1 As String = CType(e.Row.Cells(5).FindControl("uri_kasan_gaku"), Label).Text
            Dim str2 As String = CType(e.Row.Cells(6).FindControl("koumuten_kasan_gaku"), Label).Text

            If String.IsNullOrEmpty(str1) Then
                CType(e.Row.Cells(5).FindControl("uri_kasan_gaku"), Label).Text = String.Empty
            Else
                CType(e.Row.Cells(5).FindControl("uri_kasan_gaku"), Label).Text = FormatNumber(str1.Trim, 0)
            End If

            If String.IsNullOrEmpty(str2) Then
                CType(e.Row.Cells(6).FindControl("koumuten_kasan_gaku"), Label).Text = String.Empty
            Else
                CType(e.Row.Cells(6).FindControl("koumuten_kasan_gaku"), Label).Text = FormatNumber(str2.Trim, 0)
            End If
        End If
    End Sub
End Class