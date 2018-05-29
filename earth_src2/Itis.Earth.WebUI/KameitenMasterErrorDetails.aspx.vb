Imports Itis.Earth.BizLogic

Partial Public Class KameitenMasterErrorDetails
    Inherits System.Web.UI.Page

    ''' <summary>�����X���ꊇ�捞�G���[�m�F</summary>
    ''' <remarks>�����X���ꊇ�捞�G���[�m�F�@�\��񋟂���</remarks>
    ''' <history>2012�N2��13���@�ԁi��A���V�X�e�����j�V�K�쐬</history>
    Private kameitenMasterBC As New KameitenMasterLogic
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
        Me.btnCsvOutput.Attributes.Add("onClick", "if(!fncCsvOut()){return false;}")
    End Sub

    ''' <summary>CSV�o�̓{�^���̏���</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        '���ʑΉ��G���[CSV�f�[�^���擾����
        Dim dtTokubetuTaiouErrorCSV As New Data.DataTable
        dtTokubetuTaiouErrorCSV = kameitenMasterBC.GetKameitenInfoIttukatuErrorCsv(CStr(ViewState("EdiJouhouDate")), CStr(ViewState("SyoriDate")))
        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("KameitenInfoIttukatuErrCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conKameitenInfoIttukatuErrCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For Each row As Data.DataRow In dtTokubetuTaiouErrorCSV.Rows
            writer.WriteLine(row.Item("edi_jouhou_sakusei_date"), row.Item("gyou_no"), row.Item("syori_datetime"), row.Item("kbn"), row.Item("kameiten_cd"), row.Item("torikesi"), row.Item("hattyuu_teisi_flg"), _
                        row.Item("kameiten_mei1"), row.Item("tenmei_kana1"), row.Item("kameiten_mei2"), row.Item("tenmei_kana2"), _
                        row.Item("builder_no"), row.Item("builder_mei"), row.Item("keiretu_cd"), row.Item("keiretu_mei"), _
                        row.Item("eigyousyo_cd"), row.Item("eigyousyo_mei"), row.Item("kameiten_seisiki_mei"), row.Item("kameiten_seisiki_mei_kana"), _
                        row.Item("todouhuken_cd"), row.Item("todouhuken_mei"), row.Item("nenkan_tousuu"), row.Item("fuho_syoumeisyo_flg"), row.Item("fuho_syoumeisyo_kaisi_nengetu"), _
                        row.Item("eigyou_tantousya_mei"), row.Item("eigyou_tantousya_simei"), row.Item("kyuu_eigyou_tantousya_mei"), _
                        row.Item("kyuu_eigyou_tantousya_simei"), row.Item("koj_uri_syubetsu"), row.Item("koj_uri_syubetsu_mei"), row.Item("jiosaki_flg"), row.Item("kaiyaku_haraimodosi_kkk"), row.Item("tou1_syouhin_cd"), _
                        row.Item("tou1_syouhin_mei"), row.Item("tou2_syouhin_cd"), row.Item("tou2_syouhin_mei"), row.Item("tou3_syouhin_cd"), _
                        row.Item("tou3_syouhin_mei"), row.Item("tys_seikyuu_saki_kbn"), row.Item("tys_seikyuu_saki_cd"), _
                        row.Item("tys_seikyuu_saki_brc"), row.Item("tys_seikyuu_saki_mei"), row.Item("koj_seikyuu_saki_kbn"), _
                        row.Item("koj_seikyuu_saki_cd"), row.Item("koj_seikyuu_saki_brc"), row.Item("koj_seikyuu_saki_mei"), _
                        row.Item("hansokuhin_seikyuu_saki_kbn"), row.Item("hansokuhin_seikyuu_saki_cd"), row.Item("hansokuhin_seikyuu_saki_brc"), _
                        row.Item("hansokuhin_seikyuu_saki_mei"), row.Item("tatemono_seikyuu_saki_kbn"), row.Item("tatemono_seikyuu_saki_cd"), _
                        row.Item("tatemono_seikyuu_saki_brc"), row.Item("tatemono_seikyuu_saki_mei"), row.Item("seikyuu_saki_kbn5"), _
                        row.Item("seikyuu_saki_cd5"), row.Item("seikyuu_saki_brc5"), row.Item("seikyuu_saki_mei5"), row.Item("seikyuu_saki_kbn6"), _
                        row.Item("seikyuu_saki_cd6"), row.Item("seikyuu_saki_brc6"), row.Item("seikyuu_saki_mei6"), row.Item("seikyuu_saki_kbn7"), _
                        row.Item("seikyuu_saki_cd7"), row.Item("seikyuu_saki_brc7"), row.Item("seikyuu_saki_mei7"), row.Item("hosyou_kikan"), row.Item("hosyousyo_hak_umu"), row.Item("nyuukin_kakunin_jyouken"), _
                        row.Item("nyuukin_kakunin_oboegaki"), row.Item("tys_mitsyo_flg"), row.Item("hattyuusyo_flg"), row.Item("yuubin_no"), _
                        row.Item("jyuusyo1"), row.Item("jyuusyo2"), row.Item("syozaichi_cd"), row.Item("syozaichi_mei"), row.Item("busyo_mei"), _
                        row.Item("daihyousya_mei"), row.Item("tel_no"), row.Item("fax_no"), row.Item("mail_address"), _
                        row.Item("bikou1"), row.Item("bikou2"), _
                        row.Item("add_date"), row.Item("seikyuu_umu"), row.Item("syouhin_cd"), row.Item("syouhin_mei"), row.Item("uri_gaku"), row.Item("koumuten_seikyuu_gaku"), row.Item("seikyuusyo_hak_date"), row.Item("uri_date"), row.Item("bikou"), _
                        SetTimeType(row.Item("kameiten_upd_datetime")), SetTimeType(row.Item("tatouwari_upd_datetime1")), SetTimeType(row.Item("tatouwari_upd_datetime2")), SetTimeType(row.Item("tatouwari_upd_datetime3")), SetTimeType(row.Item("kameiten_jyuusyo_upd_datetime")), _
                        row.Item("bikou_syubetu1"), row.Item("bikou_syubetu1_mei"), row.Item("bikou_syubetu2"), _
                        row.Item("bikou_syubetu2_mei"), row.Item("bikou_syubetu3"), row.Item("bikou_syubetu3_mei"), row.Item("bikou_syubetu4"), _
                        row.Item("bikou_syubetu4_mei"), row.Item("bikou_syubetu5"), row.Item("bikou_syubetu5_mei"), row.Item("naiyou1"), row.Item("naiyou2"), _
                        row.Item("naiyou3"), row.Item("naiyou4"), row.Item("naiyou5"), row.Item("add_login_user_id"), SetTimeType(row.Item("add_datetime")), row.Item("upd_login_user_id"), SetTimeType(row.Item("upd_datetime")))
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
        dtTokubetuTaiouError = kameitenMasterBC.GetKameitenInfoIttukatuError(strEdiDate, strSyoriDate)
        '�������ʂ�ݒ肷��
        Me.grdMeisaiLeft.DataSource = dtTokubetuTaiouError
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = dtTokubetuTaiouError
        Me.grdMeisaiRight.DataBind()
        '�������ʌ������擾����
        Dim intCount As Integer = kameitenMasterBC.GetKameitenInfoIttukatuErrorCount(strEdiDate, strSyoriDate)

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
    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisaiRight.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim str1 As String = CType(e.Row.Cells(16).FindControl("kaiyaku_haraimodosi_kkk"), Label).Text

            If String.IsNullOrEmpty(str1) Then
                CType(e.Row.Cells(16).FindControl("kaiyaku_haraimodosi_kkk"), Label).Text = String.Empty
            Else
                Try
                    CType(e.Row.Cells(16).FindControl("kaiyaku_haraimodosi_kkk"), Label).Text = FormatNumber(str1.Trim, 0)
                Catch ex As Exception

                End Try
            End If

            '================��2013/03/13 �ԗ� 407584 �ǉ���========================
            Dim strKingaku As String

            '������z
            strKingaku = CType(e.Row.Cells(16).FindControl("lblUriGaku"), Label).Text
            If String.IsNullOrEmpty(strKingaku) Then
                CType(e.Row.FindControl("lblUriGaku"), Label).Text = String.Empty
            Else
                Try
                    CType(e.Row.FindControl("lblUriGaku"), Label).Text = FormatNumber(strKingaku.Trim, 0)
                Catch ex As Exception

                End Try
            End If

            '�H���X�������z
            strKingaku = CType(e.Row.Cells(16).FindControl("lblKoumutenSeikyuuGaku"), Label).Text
            If String.IsNullOrEmpty(strKingaku) Then
                CType(e.Row.FindControl("lblKoumutenSeikyuuGaku"), Label).Text = String.Empty
            Else
                Try
                    CType(e.Row.FindControl("lblKoumutenSeikyuuGaku"), Label).Text = FormatNumber(strKingaku.Trim, 0)
                Catch ex As Exception

                End Try
            End If
            '================��2013/03/13 �ԗ� 407584 �ǉ���========================

            ''============2012/05/21 �ԗ� 407553�̑Ή� �ǉ���============================
            'Dim lblKousinDate As Label

            ''�����X�}�X�^_�X�V����
            'lblKousinDate = CType(e.Row.FindControl("lblKameitenUpdDatetime"), Label)
            'lblKousinDate.Text = Me.SetKousinDate(lblKousinDate.Text.Trim)

            ''���������}�X�^_�X�V����1
            'lblKousinDate = CType(e.Row.FindControl("lblTatouwariUpdDatetime1"), Label)
            'lblKousinDate.Text = Me.SetKousinDate(lblKousinDate.Text.Trim)

            ''���������}�X�^_�X�V����2
            'lblKousinDate = CType(e.Row.FindControl("lblTatouwariUpdDatetime2"), Label)
            'lblKousinDate.Text = Me.SetKousinDate(lblKousinDate.Text.Trim)

            ''���������}�X�^_�X�V����3
            'lblKousinDate = CType(e.Row.FindControl("lblTatouwariUpdDatetime3"), Label)
            'lblKousinDate.Text = Me.SetKousinDate(lblKousinDate.Text.Trim)

            ''�����X�Z���}�X�^_�X�V����
            'lblKousinDate = CType(e.Row.FindControl("lblKameitenJyuusyoUpdDatetime"), Label)
            'lblKousinDate.Text = Me.SetKousinDate(lblKousinDate.Text.Trim)

            ''============2012/05/21 �ԗ� 407553�̑Ή� �ǉ���============================

        End If
    End Sub

    '''' <summary>
    '''' �X�V�������Z�b�g����
    '''' </summary>
    '''' <param name="strDate"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    '''' <history>2012/05/21 �ԗ� 407553�̑Ή� �ǉ�</history>
    'Private Function SetKousinDate(ByVal strDate As String) As String

    '    Dim strTemp As String = strDate.Trim

    '    Try
    '        If Not strTemp.Equals(String.Empty) Then
    '            strTemp = strTemp.Replace("_", String.Empty).Replace("/", String.Empty).Replace(" ", String.Empty).Replace(":", String.Empty)
    '            If strTemp.Length < 14 Then
    '                strTemp = strTemp & StrDup(14 - strTemp.Length, "0")
    '            Else
    '                strTemp = Left(strTemp, 14)
    '            End If
    '            strTemp = Left(strTemp, 4) & "/" & Mid(strTemp, 5, 2) & "/" & Mid(strTemp, 7, 2) & " " & Mid(strTemp, 9, 2) & ":" & Mid(strTemp, 11, 2) & ":" & Mid(strTemp, 13, 2)
    '        End If

    '        Return strTemp

    '    Catch ex As Exception

    '        Return strDate.Trim
    '    End Try

    'End Function

End Class