Imports Itis.Earth.BizLogic

Partial Public Class TyousaMitumoriYouDataSyuturyoku
    Inherits System.Web.UI.Page

    '�C���X�^���X����
    Private TyousaMitumoriYouDataSyuturyokuBL As New TyousaMitumoriYouDataSyuturyokuLogic
    '���ʃ`�F�b�N
    Private commoncheck As New CommonCheck
    '���O�C�����[�U�[���擾����B
    Private Ninsyou As New Ninsyou()

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '��{�F��
        If Ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        ViewState("UserId") = Ninsyou.GetUserID()

        'JavaScript
        MakeJavascript()

        If Not IsPostBack Then
            Me.divMeisai.Attributes.Add("style", "height: 220px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:933px;")

            hidCsvFlg.Value = ""

            If Not IsNothing(Request("sendSearchTerms")) Then
                Me.ddlKbn.SelectedValue = Split(Request("sendSearchTerms"), "$$$")(0)
                Me.tbxBangou1.Text = Split(Request("sendSearchTerms"), "$$$")(1)
                Me.tbxBangou2.Text = Split(Request("sendSearchTerms"), "$$$")(1)
                If Not Me.tbxBangou1.Text.Equals(String.Empty) Then
                    '����
                    Call Me.btnKensaku_Click(sender, e)
                End If
            End If

        Else
            CloseCover()
        End If

        '�����X
        tbxKameiTenMei.Attributes.Add("readonly", "true;")

        '==================2017/12/26 �� ������ǉ����� �ǉ���====================================
        '�n��R�[�h
        tbxKeiretuMei.Attributes.Add("readonly", "true;")
        '==================2017/12/26 �� ������ǉ����� �ǉ���====================================

        '==================2012/03/28 �ԗ� 405821�Č��̑Ή� �ǉ���====================================
        '���
        Me.tbxTorikesi.Attributes.Add("readonly", "true;")
        '�����X�̐F���Z�b�g����
        Call Me.SetColor()
        '==================2012/03/28 �ԗ� 405821�Č��̑Ή� �ǉ���====================================

        ''�ԍ��R�s�[
        'tbxBangou1.Attributes.Add("onblur", "SetOnaji(this);")
        '�������s�{�^��
        btnKensaku.Attributes.Add("onclick", "if(document.getElementById('" & Me.ddlSearchCount.ClientID & "').value=='max'){if(!confirm('������������Ɂu�������v���I������Ă��܂��B\n��ʕ\���Ɏ��Ԃ��|����\��������܂����A���s���Ă�낵���ł����H')){return false;}else{fncShowModal();}}else{fncShowModal();};")
        'CSV�o�̓{�^��
        btnCsvData.Attributes.Add("onclick", "if(document.getElementById('" & Me.hidCsvFlg.ClientID & "').value==''||document.getElementById('" & Me.hidCsvFlg.ClientID & "').value==0){alert('�o�͑Ώۃf�[�^��I�����ĉ������B');return false;}else{if(document.getElementById('" & Me.rbnFlg1.ClientID & "').checked==true){if(document.getElementById('" & Me.hidCsvFlg.ClientID & "').value>100){alert('�Ώی�����100���𒴂��Ă��܂��B\n�Ώۂ��i���āA�ēx���s���Ă��������B');return false;}}else{if(document.getElementById('" & Me.hidCsvFlg.ClientID & "').value>3000){alert('�Ώی�����3�猏�𒴂��Ă��܂��B\n�Ώۂ��i���āA�ēx���s���Ă��������B');return false;}}};")
        '���ׂČ��I��
        chkAll.Attributes.Add("onclick", "fncShowModal();")
        'EXCEL�t�@�C��DownLoad
        btnExcelDownLoad.Attributes.Add("onclick", "javascript:window.location.href='./Files/�������Ϗ��쐬�p.lha';return false;")

        '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
        '�����l�p��EXCEL�t�@�C��DownLoad
        btnToukenDownLoad.Attributes.Add("onclick", "javascript:window.location.href='./Files/�����l�p_�������Ϗ��쐬�p.lha';return false;")
        '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
        
    End Sub

    '�����i�������W�I�{�^���ɓ��͗L��̏ꍇ�j
    Private Function GetTouZaiKbn() As String
        If Me.rbnTyou.Checked Then
            Return "0"
        ElseIf Me.rbnSei.Checked Then
            Return "1"
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' CSV�f�[�^�o��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCsvData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvData.Click

        '���Ϗ��쐬
        Dim strMitumoriFlg As String = String.Empty
        If Me.hidMitumori.Value = "1" Then
            strMitumoriFlg = "1"
        End If

        'CSV�o��
        Dim dtCsvTable As New Data.DataTable
        If Not TyousaMitumoriYouDataSyuturyokuBL.SetCsvData(Response, Me.grdItiran, strMitumoriFlg, Me.rbnFlg1.Checked, Me.hidCsvFlg.Value, ViewState("UserId"), dtCsvTable, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn()) Then
            Response.Clear()
            '�G���[���L��ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�V�X�e���G���[���������܂����A�Ǘ��҂ɘA�����Ă��������B');", True)
        Else
            If dtCsvTable.Rows.Count > 0 Then

                'CSV�f�[�^
                ViewState.Item("dtCsvTable") = dtCsvTable
                '�`�F�b�N�Ώۂ�����
                Me.hidCsvFlg.Value = String.Empty

                'GridView�̒l���Č���
                Dim dtTyousaMitumori As New Data.DataTable
                dtTyousaMitumori = TyousaMitumoriYouDataSyuturyokuBL.GetTyousaMitumoriInfo(ddlKbn.SelectedValue, tbxBangou1.Text.Trim, tbxBangou2.Text.Trim, tbxKameiTenCd.Text.Trim, strMitumoriFlg, ddlSearchCount.SelectedValue, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn())

                '�������̎擾
                Dim intCount As Int64
                intCount = TyousaMitumoriYouDataSyuturyokuBL.GetTyousaMitumoriCount(ddlKbn.SelectedValue, tbxBangou1.Text.Trim, tbxBangou2.Text.Trim, tbxKameiTenCd.Text.Trim, strMitumoriFlg, ddlSearchCount.SelectedValue, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn())
                If Me.ddlSearchCount.SelectedValue = "max" Then
                    Me.lblCount.Text = intCount
                    Me.lblCount.ForeColor = Drawing.Color.Black
                Else
                    If intCount > Me.ddlSearchCount.SelectedValue Then
                        Me.lblCount.Text = Me.ddlSearchCount.SelectedValue & " / " & intCount
                        Me.lblCount.ForeColor = Drawing.Color.Red
                    Else
                        Me.lblCount.Text = dtTyousaMitumori.Rows.Count
                        Me.lblCount.ForeColor = Drawing.Color.Black
                    End If
                End If

                If dtTyousaMitumori.Rows.Count > 0 Then
                    Me.chkAll.Checked = False
                    '�f�[�^���O���ȊO�̏ꍇ
                    grdItiran.Visible = True
                    Me.grdItiran.DataSource = dtTyousaMitumori
                    Me.grdItiran.DataBind()
                    If dtTyousaMitumori.Rows.Count <= 10 Then
                        Me.divMeisai.Attributes.Add("style", "height: 221px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:933px;")
                        Me.grdItiran.Columns(0).ItemStyle.Width = 56
                        Me.grdItiran.Columns(3).ItemStyle.Width = 276
                        Me.grdItiran.Columns(6).ItemStyle.Width = 254
                    Else
                        Me.divMeisai.Attributes.Add("style", "height: 220px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:950px;")
                        Me.grdItiran.Columns(0).ItemStyle.Width = 57
                        Me.grdItiran.Columns(3).ItemStyle.Width = 275
                        Me.grdItiran.Columns(6).ItemStyle.Width = 254
                    End If
                Else
                    grdItiran.Visible = False
                End If

                '�Č���
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "window.setTimeout('objEBI(\'" & Me.btnNO.ClientID & "\').click()',10); ", True)
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "alert('" & Messages.Instance.MSG039E & "');", True)
            End If
        End If
    End Sub


    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click

        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = "TyousaMitumorisyo.csv"

        Response.Buffer = True
        Dim writer As New CsvWriter(Response.OutputStream, System.Text.Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conTyousaMitumoriCsvHeader)


        '��ʃf�[�^�̃\�[�g����ݒ肷��
        Dim dtCsvTable As Data.DataView = CType(ViewState.Item("dtCsvTable"), Data.DataTable).DefaultView
        dtCsvTable.Sort = "kbn,hosyousyo_no,bunrui_cd,syouhin_cd" & " " & "ASC"
        Dim dtCsvTable1 As Data.DataTable = dtCsvTable.ToTable()

        If dtCsvTable1.Rows.Count > 0 Then
            For intRow1 As Integer = 0 To dtCsvTable1.Rows.Count - 1
                With dtCsvTable1.Rows(intRow1)
                    If String.IsNullOrEmpty(dtCsvTable1.Rows(intRow1).Item(5).ToString.Trim) Then
                        writer.WriteLine(.Item(0).ToString, _
                                         .Item(1).ToString, _
                                         .Item(2).ToString, _
                                         .Item(3).ToString, _
                                         .Item(4).ToString, _
                                         "���S����", _
                                         .Item(6).ToString, _
                                         .Item(7).ToString, _
                                         .Item(8).ToString, _
                                         .Item(9).ToString, _
                                         .Item(10).ToString, _
                                         .Item(11).ToString, _
                                         .Item(12).ToString, _
                                         .Item(13).ToString, _
                                         Fix(.Item(14)).ToString, _
                                         Fix(.Item(15)).ToString, _
                                         .Item(16).ToString)
                    Else
                        writer.WriteLine(.Item(0).ToString, _
                                         .Item(1).ToString, _
                                         .Item(2).ToString, _
                                         .Item(3).ToString, _
                                         .Item(4).ToString, _
                                         .Item(5).ToString, _
                                         .Item(6).ToString, _
                                         .Item(7).ToString, _
                                         .Item(8).ToString, _
                                         .Item(9).ToString, _
                                         .Item(10).ToString, _
                                         .Item(11).ToString, _
                                         .Item(12).ToString, _
                                         .Item(13).ToString, _
                                         Fix(.Item(14)).ToString, _
                                         Fix(.Item(15)).ToString, _
                                         .Item(16).ToString)
                    End If
                End With
            Next
        End If

        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "shift-jis"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))

        Response.End()
    End Sub

    ''' <summary>
    ''' CHECKBOX
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItiran_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItiran.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("chkTaisyou"), CheckBox).Attributes.Add("onclick", "ChkCsvFlg(this," & e.Row.RowIndex & ");")
        End If
    End Sub

    ''' <summary>
    ''' �������s�{�^��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        '���̓`�F�b�N
        Dim strErr As String = ""
        Dim strID As String = ""
        strID = InputCheck(strErr)

        '�G���[�����鎞
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();document.getElementById('" & strID & "').select();", True)
        Else
            '���Ϗ��쐬
            Dim strMitumoriFlg As String = String.Empty
            If rbnMitumori2.Checked = True Then
                strMitumoriFlg = "1"
                Me.hidMitumori.Value = "1"
            Else
                Me.hidMitumori.Value = String.Empty
            End If

            '��ʏ��̎擾
            Dim dtTyousaMitumori As New Data.DataTable
            dtTyousaMitumori = TyousaMitumoriYouDataSyuturyokuBL.GetTyousaMitumoriInfo(ddlKbn.SelectedValue, tbxBangou1.Text.Trim, tbxBangou2.Text.Trim, tbxKameiTenCd.Text.Trim, strMitumoriFlg, ddlSearchCount.SelectedValue, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn())

            '�������̎擾
            Dim intCount As Int64
            intCount = TyousaMitumoriYouDataSyuturyokuBL.GetTyousaMitumoriCount(ddlKbn.SelectedValue, tbxBangou1.Text.Trim, tbxBangou2.Text.Trim, tbxKameiTenCd.Text.Trim, strMitumoriFlg, ddlSearchCount.SelectedValue, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn())
            If Me.ddlSearchCount.SelectedValue = "max" Then
                Me.lblCount.Text = intCount
                Me.lblCount.ForeColor = Drawing.Color.Black
            Else
                If intCount > Me.ddlSearchCount.SelectedValue Then
                    Me.lblCount.Text = Me.ddlSearchCount.SelectedValue & " / " & intCount
                    Me.lblCount.ForeColor = Drawing.Color.Red
                Else
                    Me.lblCount.Text = dtTyousaMitumori.Rows.Count
                    Me.lblCount.ForeColor = Drawing.Color.Black
                End If
            End If

            If dtTyousaMitumori.Rows.Count > 0 Then

                Me.chkAll.Checked = False

                Me.grdItiran.Visible = True
                '�f�[�^���O���ȊO�̏ꍇ
                Me.grdItiran.DataSource = dtTyousaMitumori
                Me.grdItiran.DataBind()
                If dtTyousaMitumori.Rows.Count <= 10 Then
                    Me.divMeisai.Attributes.Add("style", "height: 221px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:933px;")
                    Me.grdItiran.Columns(0).ItemStyle.Width = 56
                    Me.grdItiran.Columns(3).ItemStyle.Width = 276
                    Me.grdItiran.Columns(6).ItemStyle.Width = 254
                Else
                    Me.divMeisai.Attributes.Add("style", "height: 220px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:950px;")
                    Me.grdItiran.Columns(0).ItemStyle.Width = 57
                    Me.grdItiran.Columns(3).ItemStyle.Width = 275
                    Me.grdItiran.Columns(6).ItemStyle.Width = 254
                End If
            Else
                '�f�[�^���O���̏ꍇ
                Me.grdItiran.Visible = False
                strErr = "alert('" & Messages.Instance.MSG039E & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            End If

        End If
    End Sub

    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")

            '�n��|�b�v�A�b�v
            .AppendLine("   function fncKeiretuSearch(){")
            '.AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            '.AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            '.AppendLine("           objKbn1.focus();")
            '.AppendLine("           return false;")
            '.AppendLine("       }")
            .AppendLine("       var strkbn='�n��';")
            .AppendLine("       var blnTaikai ;")
            .AppendLine("       var arrKubun = '';")
            '.AppendLine("       if(objKbn1.selectedIndex!=0){")
            '.AppendLine("           arrKubun = arrKubun + objKbn1.value + ',';")
            '.AppendLine("       }")
            '.AppendLine("       if(objKbn2.selectedIndex!=0){")
            '.AppendLine("           arrKubun = arrKubun + objKbn2.value + ',';")
            '.AppendLine("       }")
            '.AppendLine("       if(objKbn3.selectedIndex!=0){")
            '.AppendLine("           arrKubun = arrKubun + objKbn3.value + ',';")
            '.AppendLine("       }")
            .AppendLine("       arrKubun = arrKubun.substring(0,arrKubun.length-1);")
            .AppendLine("       if(document.getElementById('" & Me.tbxKameiTenCd.ClientID & "').value != ''){")
            .AppendLine("           blnTaikai = 'False' ")
            .AppendLine("       }else{")
            .AppendLine("           blnTaikai = 'True' ")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxKeiretuCd.ClientID & "&objMei=" & Me.tbxKeiretuMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxKeiretuCd.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun='+arrKubun+'&blnDelete='+blnTaikai, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")

            '�����X�|�b�v�A�b�v
            .AppendLine("   function fncKameitenSearch(strKbn){")
            .AppendLine("       var strkbn='�����X'")
            .AppendLine("       var strClientID ")
            .AppendLine("       strClientID = '" & Me.tbxKameiTenCd.ClientID & "'")
            .AppendLine("       var strClientID ")
            .AppendLine("       strClientMei = '" & Me.tbxKameiTenMei.ClientID & "'")
            .AppendLine("       var blnTaikai ")
            '============2012/03/29 �ԗ� 405721�Č��̑Ή� �C����=========================
            .AppendLine("       var strHidTorikesiCd; ")
            .AppendLine("       var strTxtTorikesiCd; ")
            '.AppendLine("       var blnTaikai='True' ")
            .AppendLine("       var blnTaikai='False'; ")
            .AppendLine("       strHidTorikesiCd = '" & Me.hidTorikesi.ClientID & "'")
            .AppendLine("       strTxtTorikesiCd = '" & Me.tbxTorikesi.ClientID & "'")
            '============2012/03/29 �ԗ� 405721�Č��̑Ή� �C����=========================
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientID+'&objMei='+strClientMei+'&strCd='+escape(eval('document.all.'+strClientID).value)+'&strMei='+escape(eval('document.all.'+strClientMei).value)+")
            .AppendLine("       '&blnDelete='+blnTaikai+'&HidTorikesiCd='+strHidTorikesiCd+'&TxtdTorikesiCd='+strTxtTorikesiCd+'&btnChangeColorCd=btnChangeColor', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")

            '�ԍ�(To)�ɓ����l���R�s�[
            .AppendLine("function SetOnaji(e)")
            .AppendLine("{")
            .AppendLine("   var objtbxBangou2 ")
            .AppendLine("   objtbxBangou2=document.getElementById('" & Me.tbxBangou2.ClientID & "');")
            .AppendLine("   if(e.value!=''){")
            .AppendLine("       objtbxBangou2.value = e.value;")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("   function fncClearWin(){")
            .AppendLine("       document.getElementById('" & Me.ddlKbn.DdlClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & Me.tbxKameiTenCd.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxKameiTenMei.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxBangou1.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxBangou2.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.rbnMitumori1.ClientID & "').checked = true;")
            .AppendLine("       document.getElementById('" & Me.ddlSearchCount.ClientID & "').selectedIndex = 0;")
            '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
            .AppendLine("       document.getElementById('" & Me.hidTorikesi.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxTorikesi.ClientID & "').value = '';")
            .AppendLine("       fncChangeColor();")
            '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================

            .AppendLine("       document.getElementById('" & Me.tbxKeiretuCd.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxKeiretuMei.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxSesyuMei.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.rbnTyou.ClientID & "').checked = false;")
            .AppendLine("       document.getElementById('" & Me.rbnSei.ClientID & "').checked = false;")



            .AppendLine("   }")

            '�Ώ�CHECK
            .AppendLine("function ChkCsvFlg(e,rowNo)")
            .AppendLine("{")
            .AppendLine("   var hidCsvFlg ")
            .AppendLine("   hidCsvFlg=document.getElementById('" & Me.hidCsvFlg.ClientID & "');")
            .AppendLine("   var intCsvFlg=hidCsvFlg.value;")
            .AppendLine("   if(e.checked==true){")
            .AppendLine("       intCsvFlg++;")
            .AppendLine("   }else{")
            .AppendLine("       intCsvFlg--;")
            .AppendLine("   }")
            .AppendLine("   hidCsvFlg.value=intCsvFlg;")
            .AppendLine("}")

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
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
            '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
            .AppendLine("function fncChangeColor()")
            .AppendLine("{")
            .AppendLine("   var strColor;")
            .AppendLine("   if((document.getElementById('" & Me.hidTorikesi.ClientID & "').value == '0')||(document.getElementById('" & Me.hidTorikesi.ClientID & "').value == ''))")
            .AppendLine("   {")
            .AppendLine("       strColor = 'black';")
            .AppendLine("   }")
            .AppendLine("   else")
            .AppendLine("   {")
            .AppendLine("       strColor = 'red';")
            .AppendLine("   }")
            .AppendLine("	document.getElementById('" & Me.tbxKameiTenCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKameiTenMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxTorikesi.ClientID & "').style.color = strColor; ")
            .AppendLine("}")
            '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub

    ''' <summary>DIV�\��</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <param name="strErr">�G���[���b�Z�[�W</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '�����X
        If strErr = "" And tbxKameiTenCd.Text <> "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxKameiTenCd.Text, "�����X�R�[�h")
            strID = tbxKameiTenCd.ClientID
        End If
        '�ԍ�
        If strErr = "" Then
            '���͕K�{
            If tbxBangou1.Text.Trim = "" And tbxBangou2.Text.Trim = "" Then
                strErr = Messages.Instance.MSG037E.Replace("@PARAM1", "�����ԍ�")
            End If
            strID = tbxBangou1.ClientID
        End If
        '�ԍ�FROM
        If strErr = "" And tbxBangou1.Text <> "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxBangou1.Text, "�����ԍ�")
            strID = tbxBangou1.ClientID
        End If
        '�ԍ�TO
        If strErr = "" And tbxBangou2.Text <> "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxBangou2.Text, "�����ԍ�")
            strID = tbxBangou2.ClientID
        End If
        '�ԍ�
        If strErr = "" Then
            '���͕K�{
            If rbnMitumori1.Checked = False And rbnMitumori2.Checked = False Then
                strErr = Messages.Instance.MSG038E.Replace("@PARAM1", "���Ϗ��쐬")
            End If
            strID = rbnMitumori1.ClientID
        End If

        Return strID

    End Function

    ''' <summary>
    ''' �J�[�\���ړ����A�����X���擾
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbxKameiTenCd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxKameiTenCd.TextChanged
        Dim dtTable As Data.DataTable
        If Me.tbxKameiTenCd.Text.Trim <> String.Empty Then
            dtTable = TyousaMitumoriYouDataSyuturyokuBL.GetKameitenMei(Me.tbxKameiTenCd.Text.Trim)
            If dtTable.Rows.Count > 0 Then
                '�����X��
                Me.tbxKameiTenMei.Text = dtTable.Rows(0).Item("kameiten_mei1").ToString.Trim
                '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                '���
                Me.hidTorikesi.Value = dtTable.Rows(0).Item("torikesi").ToString.Trim
                If dtTable.Rows(0).Item("torikesi").ToString.Trim.Equals("0") Then
                    Me.tbxTorikesi.Text = String.Empty
                Else
                    Me.tbxTorikesi.Text = dtTable.Rows(0).Item("torikesi").ToString.Trim & ":" & dtTable.Rows(0).Item("torikesi_txt").ToString.Trim
                End If
                '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
            Else
                Me.tbxKameiTenCd.Text = String.Empty
                Me.tbxKameiTenMei.Text = String.Empty
                '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                '���
                Me.tbxTorikesi.Text = String.Empty
                Me.hidTorikesi.Value = String.Empty
                '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG2034E & "');document.getElementById('" & Me.tbxKameiTenCd.ClientID & "').focus();", True)
            End If
        Else
            Me.tbxKameiTenMei.Text = String.Empty
            '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
            '���
            Me.tbxTorikesi.Text = String.Empty
            Me.hidTorikesi.Value = String.Empty
            '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
        End If
        '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
        '�����X�̐F���Z�b�g����
        Call Me.SetColor()
        '============2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���=========================

    End Sub

    ''' <summary>
    ''' �ԍ��J�[�\���ړ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbxBangou1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxBangou1.TextChanged


        If commoncheck.ChkHankakuEisuuji(Me.tbxBangou1.Text.Trim, "") <> "" And Me.tbxBangou1.Text.Trim <> String.Empty Then
            Me.tbxBangou1.Text = String.Empty
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�p�����ȊO�����͂���Ă��܂�');document.getElementById('" & Me.tbxBangou1.ClientID & "').focus();", True)
        Else
            If Me.tbxBangou2.Text.Trim = String.Empty And Me.tbxBangou1.Text.Trim <> String.Empty Then
                Me.tbxBangou2.Text = Me.tbxBangou1.Text.Trim
            End If
        End If

    End Sub

    ''' <summary>
    ''' �ԍ��J�[�\���ړ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbxBangou2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxBangou2.TextChanged
        If commoncheck.ChkHankakuEisuuji(Me.tbxBangou2.Text.Trim, "") <> "" And Me.tbxBangou2.Text.Trim <> String.Empty Then
            Me.tbxBangou2.Text = String.Empty
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�p�����ȊO�����͂���Ă��܂�');document.getElementById('" & Me.tbxBangou2.ClientID & "').focus();", True)
        End If
    End Sub

    ''' <summary>
    ''' ���ׂđI����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Dim intCount As Int64 = 0
        intCount = Me.grdItiran.Rows.Count
        If intCount > 0 Then
            If Me.chkAll.Checked = True Then
                Me.hidCsvFlg.Value = intCount.ToString
                For i As Int64 = 0 To intCount - 1
                    CType(Me.grdItiran.Rows(i).Cells(0).FindControl("chkTaisyou"), CheckBox).Checked = True
                Next
            Else
                Me.hidCsvFlg.Value = 0
                For i As Int64 = 0 To intCount - 1
                    CType(Me.grdItiran.Rows(i).Cells(0).FindControl("chkTaisyou"), CheckBox).Checked = False
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' �F��ύX����
    ''' </summary>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Private Sub SetColor()

        Dim strTorikesi As String
        strTorikesi = Me.hidTorikesi.Value.Trim

        If strTorikesi.Equals("0") OrElse strTorikesi.Equals(String.Empty) Then
            Me.tbxKameiTenCd.ForeColor = Drawing.Color.Black
            Me.tbxKameitenMei.ForeColor = Drawing.Color.Black
            Me.tbxTorikesi.ForeColor = Drawing.Color.Black
        Else
            Me.tbxKameiTenCd.ForeColor = Drawing.Color.Red
            Me.tbxKameitenMei.ForeColor = Drawing.Color.Red
            Me.tbxTorikesi.ForeColor = Drawing.Color.Red
        End If

    End Sub

    Protected Sub tbxKeiretuCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class