Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Partial Public Class HanbaiKakakuMasterErrorDetails
    Inherits System.Web.UI.Page

    ''' <summary>�̔����i�}�X�^�Ɖ�</summary>
    ''' <remarks>�̔����i�}�X�^�Ɖ�@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2011/03/01�@���c(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private hanbaiKakakuErrLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

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
            commonCheck.SetURL(Me, strUserID)
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
    ''' <summary>CSV�o��</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        '�̔����i�G���[�f�[�^���擾����
        Dim dtHanbaiKakakuErrCsvInfo As HanbaiKakakuMasterDataSet.HanbaiKakakuErrCSVTableDataTable
        '�̔����i�G���[CSV�f�[�^���擾����
        dtHanbaiKakakuErrCsvInfo = hanbaiKakakuErrLogic.SelHanbaiKakakuErrCsv(ViewState("EdiJouhouDate"), ViewState("SyoriDate"))

        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("HanbaiKakakuMasterErrCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conHanbaiKakakuErrCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For Each row As HanbaiKakakuMasterDataSet.HanbaiKakakuErrCSVTableRow In dtHanbaiKakakuErrCsvInfo
            writer.WriteLine(row.edi_jouhou_sakusei_date, row.gyou_no, SetTimeType(row.syori_datetime), _
                row.aitesaki_syubetu, row.aitesaki_cd, row.aitesaki_mei, row.syouhin_cd, _
                row.syouhin_mei, row.tys_houhou_no, row.tys_houhou, row.torikesi, _
                row.koumuten_seikyuu_gaku, row.koumuten_seikyuu_gaku_henkou_flg, _
                row.jitu_seikyuu_gaku, row.jitu_seikyuu_gaku_henkou_flg, row.koukai_flg, _
                row.add_login_user_id, SetTimeType(row.add_datetime), row.upd_login_user_id, _
                SetTimeType(row.upd_datetime))
        Next

        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()
    End Sub
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
        Dim dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.HanbaiKakakuErrTableDataTable
        dtHanbaiKakakuInfo = hanbaiKakakuErrLogic.GetHanbaiKakakuErr(strEdiDate, strSyoriDate)
        '�������ʂ�ݒ肷��
        Me.grdBodyLeft.DataSource = dtHanbaiKakakuInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtHanbaiKakakuInfo
        Me.grdBodyRight.DataBind()
        '�������ʌ������擾����
        Dim intCount As Integer = hanbaiKakakuErrLogic.GetHanbaiKakakuErrCount(strEdiDate, strSyoriDate)
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

        For rowCount = 0 To Me.grdBodyRight.Rows.Count - 1
            '�H���X�������z��ݒ肷��
            If Me.grdBodyRight.Rows(rowCount).Cells(1).Text.ToString.Trim.Equals("&nbsp;") Then
                Me.grdBodyRight.Rows(rowCount).Cells(1).Text = ""
            ElseIf commonCheck.CheckHankaku(Me.grdBodyRight.Rows(rowCount).Cells(1).Text, "", "") = String.Empty Then
                Me.grdBodyRight.Rows(rowCount).Cells(1).Text = FormatNumber(Me.grdBodyRight.Rows(rowCount).Cells(1).Text, 0)
            End If
            '���������z��ݒ肷��
            If Me.grdBodyRight.Rows(rowCount).Cells(3).Text.ToString.Trim.Equals("&nbsp;") Then
                Me.grdBodyRight.Rows(rowCount).Cells(3).Text = ""
            ElseIf commonCheck.CheckHankaku(Me.grdBodyRight.Rows(rowCount).Cells(3).Text, "", "") = String.Empty Then
                Me.grdBodyRight.Rows(rowCount).Cells(3).Text = FormatNumber(Me.grdBodyRight.Rows(rowCount).Cells(3).Text, 0)
            End If
        Next

    End Sub
    ''' <summary>���t�ύX</summary>
    ''' <param name="strTime">���t</param>
    ''' <returns>���t(yyyy/MM/dd HH:mm:ss)</returns>
    Private Function SetTimeType(ByVal strTime As String) As String
        If Not strTime.Trim.Equals(String.Empty) Then
            Return CDate(strTime).ToString("yyyy/MM/dd HH:mm:ss")
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

End Class