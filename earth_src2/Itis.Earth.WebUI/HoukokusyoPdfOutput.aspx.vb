Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data

Partial Public Class HoukokusyoPdfOutput
    Inherits System.Web.UI.Page

#Region "�v���C�x�[�g�ϐ�"
    '���O�C�����[�U�[���擾����B
    Private ninsyou As New Ninsyou()

    Private kensaHoukokusyoOutputLogic As New KensaHoukokusyoOutputLogic
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "Houkokusyo"

    Private Const APOST As Char = ","c
    Private Const SEP_STRING As String = "$$$"

    Enum PDFStatus As Integer

        OK = 0                              '����
        IOException = 1                     '�G���[(���̃��[�U���t�@�C�����J���Ă���)
        UnauthorizedAccessException = 2     '�G���[(�t�@�C�����쐬����p�X���s��)
        NoData = 3                          '�Ώۂ̃f�[�^���擾�ł��܂���B

    End Enum
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")


        If Not IsPostBack Then

            '��{�F��
            If ninsyou.GetUserID() = "" Then
                Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
                Server.Transfer("CommonErr.aspx")
            Else
                ViewState("loginID") = ninsyou.GetUserID()
            End If

            ViewState("KanriNo") = Request("KanriNo")
            ViewState("index") = 0

            'Call Me.WaitOutput()
            'Dim outputIndex As Integer = CInt(Me.hidIndex.Value)
            'ClientScript.RegisterClientScriptBlock(Page.GetType(), "setCopy", "<script type =""text/javascript"" > window.onload=function(){ setTimeout(""document.getElementById('" & Me.btnStart.ClientID & "').click();""," & outputIndex * 2001 & ");}</script>")
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "setCopy", "<script type =""text/javascript"" > window.onload=function(){ setTimeout(""document.getElementById('" & Me.btnStart.ClientID & "').click();"",0);}</script>")

        End If
    End Sub

    Private Sub btnStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStart.Click

        Me.hiddenIframe.Attributes.Remove("src")

        Dim arrKanriNo() As String = ViewState("KanriNo").ToString.Replace(SEP_STRING, ",").Split(",")

        Me.divProgressBar.Style.Add("width", Math.Floor((Convert.ToInt32(ViewState("index")) / arrKanriNo.Length) * 100).ToString & "%")
        Me.lblProgress.Text = ViewState("index").ToString & " / " & arrKanriNo.Length.ToString

        If Convert.ToInt32(ViewState("index")) < arrKanriNo.Length Then
            Call Me.CreateFcwTyouhyouData(arrKanriNo(Convert.ToInt32(ViewState("index"))))
            ViewState("index") = Convert.ToInt32(ViewState("index")) + 1
        Else
            '�I��
            Call Me.CloseWindow()
        End If



    End Sub

    Private Sub CreateFcwTyouhyouData(ByVal kanriNo As String)

        Dim sb As New Text.StringBuilder
        Dim editDt_Fixed As New DataTable
        Dim editDt_Table As New DataTable
        Dim dr As Data.DataRow

        Dim errMsg As String = String.Empty
        Dim syainCd As String '�Ј��R�[�h

        '�f�[�^�擾
        Dim houkokusyoDt As Data.DataTable
        houkokusyoDt = kensaHoukokusyoOutputLogic.GetHoukokusyoPdfInfo(kanriNo)

        Dim kbn As String = String.Empty
        Dim hosyousyoNo As String = String.Empty
        Dim kameitenCd As String = String.Empty


        syainCd = ViewState("loginID")
        fcw = New FcwUtility(Page, syainCd, kinouId, ".fcx")

        If Not houkokusyoDt.Rows.Count > 0 Then
            '�f�[�^���Ȃ��ꍇ
            errMsg = fcw.GetErrMsg(PDFStatus.NoData)

        Else
            '[Head] ���쐬
            sb.Append(fcw.CreateDatHeader(APOST.ToString))

            editDt_Fixed.Columns.Add(New Data.DataColumn("�{�喼", GetType(String)))
            editDt_Fixed.Columns.Add(New Data.DataColumn("�����X��", GetType(String)))
            editDt_Fixed.Columns.Add(New Data.DataColumn("�����Z��1", GetType(String)))
            editDt_Fixed.Columns.Add(New Data.DataColumn("�����Z��2_3", GetType(String)))
            editDt_Fixed.Columns.Add(New Data.DataColumn("�����\���K��", GetType(String)))
            editDt_Fixed.Columns.Add(New Data.DataColumn("�����H����1_10", GetType(String)))
            editDt_Fixed.Columns.Add(New Data.DataColumn("FC��", GetType(String)))
            editDt_Fixed.Columns.Add(New Data.DataColumn("��������", GetType(String)))
            editDt_Fixed.Columns.Add(New Data.DataColumn("��", GetType(String)))

            editDt_Table.Columns.Add(New Data.DataColumn("�����H����", GetType(String)))
            editDt_Table.Columns.Add(New Data.DataColumn("�������{��", GetType(String)))
            editDt_Table.Columns.Add(New Data.DataColumn("��������", GetType(String)))

            With houkokusyoDt.Rows(0)
                kbn = .Item("kbn").ToString
                hosyousyoNo = .Item("hosyousyo_no").ToString
                kameitenCd = .Item("kameiten_cd").ToString

                For i As Integer = 1 To 10
                    dr = editDt_Table.NewRow
                    dr.Item("�����H����") = .Item("kensa_koutei_nm" & i.ToString).ToString
                    If .Item("kensa_start_jissibi" & i.ToString).ToString.Equals(String.Empty) Then
                        dr.Item("�������{��") = String.Empty
                    Else
                        dr.Item("�������{��") = Convert.ToDateTime(.Item("kensa_start_jissibi" & i.ToString)).ToString("yyyy�NMM��dd��")
                    End If
                    dr.Item("��������") = .Item("kensa_in_nm" & i.ToString).ToString
                    editDt_Table.Rows.Add(dr)
                Next

                dr = editDt_Fixed.NewRow
                dr.Item("�{�喼") = .Item("sesyu_mei").ToString
                dr.Item("�����X��") = .Item("kameiten_mei").ToString
                dr.Item("�����Z��1") = .Item("bukken_jyuusyo1").ToString
                dr.Item("�����Z��2_3") = .Item("bukken_jyuusyo2").ToString & Space(1) & .Item("bukken_jyuusyo3").ToString
                dr.Item("�����\���K��") = .Item("gaiyou_you").ToString & .Item("tatemono_kaisu").ToString
                dr.Item("FC��") = .Item("fc_nm").ToString
                dr.Item("�����H����1_10") = Me.GetAllKensaKouteiName(editDt_Table)
                dr.Item("��������") = IIf(editDt_Table.Rows(0).Item(0).ToString.Trim.Equals(String.Empty), String.Empty, "�� �i").ToString
                dr.Item("��") = String.Empty
                editDt_Fixed.Rows.Add(dr)
            End With

            '�\��
            sb.Append(fcw.CreateFormSection("PAGE=Houkokusyo_Hyousi"))
            '[FixedDataSection] ���쐬
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_Hyousi(editDt_Fixed)))

            '����
            sb.Append(fcw.CreateFormSection("PAGE=Houkokusyo_Hakusi"))
            '[FixedDataSection] ���쐬
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_Hakuusi(editDt_Fixed)))

            '�����T�v
            sb.Append(fcw.CreateFormSection("PAGE=Houkokusyo_KensaGaiyou"))
            '[FixedDataSection] ���쐬
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_KensaGaiyou(editDt_Fixed)))

            '����
            sb.Append(fcw.CreateFormSection("PAGE=Houkokusyo_Hakusi"))
            '[FixedDataSection] ���쐬
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_Hakuusi(editDt_Fixed)))

            '�������茋��
            sb.Append(fcw.CreateFormSection("PAGE=Houkokusyo_KensaHanteiKekka"))
            '[FixedDataSection] ���쐬
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_KensaHanteiKekka(editDt_Fixed)))
            '[TableDataSection] ���쐬
            sb.Append(fcw.CreateTableDataSection(GetTableDataSection_KensaHanteiKekka(editDt_Table)))
            sb.Append(vbNewLine)

            '����
            sb.Append(fcw.CreateFormSection("PAGE=Houkokusyo_Hakusi"))
            '[FixedDataSection] ���쐬
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_Hakuusi(editDt_Fixed)))

            '����(�R�� )
            sb.Append(fcw.CreateFormSection("PAGE=Houkokusyo_Memo"))
            '[TableDataSection] ���쐬
            sb.Append(fcw.CreateTableDataSection(GetTableDataSection_Memo(editDt_Fixed)))
            sb.Append(vbNewLine)
            '[TableDataSection] ���쐬
            sb.Append(fcw.CreateTableDataSection(GetTableDataSection_Memo(editDt_Fixed)))
            sb.Append(vbNewLine)
            '[TableDataSection] ���쐬
            sb.Append(fcw.CreateTableDataSection(GetTableDataSection_Memo(editDt_Fixed)))
            sb.Append(vbNewLine)

            '���\��
            sb.Append(fcw.CreateFormSection("PAGE=Houkokusyo_End"))
            '[FixedDataSection] ���쐬
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_End(editDt_Fixed)))



            'DAT�t�@�C���쐬
            errMsg = fcw.GetErrMsg(fcw.WriteData(sb.ToString))
        End If

        'PDF�@�o��
        If Not errMsg.Equals(String.Empty) Then
            '���b�Z�[�W
            'Context.Items("strFailureMsg") = errMsg
            'Server.Transfer("CommonErr.aspx")

            Call Me.ShowResult("�Ǘ�No(" & kanriNo & ")�F" & errMsg, True)
            Return
        Else
            ViewState("FileName") = kbn & kameitenCd & "_" & kbn & hosyousyoNo & "_�\���T�v"
            Me.Page.Title = ViewState("FileName").ToString

            Dim pdfOpenFlg As Boolean = False
            Dim TyouhyouPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouPath").ToString
            TyouhyouPath = TyouhyouPath & ViewState("FileName").ToString & ".pdf"
            If System.IO.File.Exists(TyouhyouPath) Then
                Dim fileInfo As New System.IO.FileInfo(TyouhyouPath)
                Try
                    fileInfo.MoveTo(TyouhyouPath)
                Catch ex As Exception
                    pdfOpenFlg = True
                Finally
                    fileInfo = Nothing
                End Try
            End If

            If pdfOpenFlg Then
                '���b�Z�[�W
                'Context.Items("strFailureMsg") = "�t�@�C���u" & ViewState("FileName").ToString & ".pdf" & "�v�͊J���Ă��܂��B�쐬�ł��܂���B"
                'Server.Transfer("CommonErr.aspx")

                Call Me.ShowResult("�t�@�C���u" & ViewState("FileName").ToString & ".pdf" & "�v�͊J���Ă��܂��B�쐬�ł��܂���B", True)
                Return
            Else
                Dim strUrl = fcw.GetUrlKensaHoukokusyo(ViewState("FileName").ToString)

                'Call Me.ShowPdf(strUrl)
                Call Me.CopyPdf(strUrl)
            End If

        End If

    End Sub

#Region "PDF���R�s�[����"
    Private Sub CopyPdf(ByVal url As String)

        Me.hiddenIframe.Attributes.Add("src", url)

        ClientScript.RegisterClientScriptBlock(Page.GetType(), "CopyPdf", "<script type =""text/javascript"" > window.onload=function(){ setTimeout(""document.getElementById('" & Me.btnCopy.ClientID & "').click();"",1000);}</script>")
    End Sub

    Private Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click

        '�i�[�p�X
        Dim strHoukokusyoPdfPath As String = System.Configuration.ConfigurationManager.AppSettings("HoukokusyoPdfPath").ToString
        strHoukokusyoPdfPath = strHoukokusyoPdfPath & IIf(Right(strHoukokusyoPdfPath, 1).Equals("\"), String.Empty, "\").ToString

        '�쐬�����t�@�C��
        Dim TyouhyouPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouPath").ToString
        TyouhyouPath = TyouhyouPath & ViewState("FileName") & ".pdf"

        Dim tryAgainFlg As Boolean = True
        Dim againCnt As Integer = 0
        While tryAgainFlg
            If Not System.IO.File.Exists(TyouhyouPath) Then
                If againCnt < 3 Then
                    againCnt = againCnt + 1
                    System.Threading.Thread.Sleep(1000)
                    Continue While
                Else
                    tryAgainFlg = False
                End If

                'PDF�쐬�����s�̏ꍇ
                'Context.Items("strFailureMsg") = "�񍐏�PDF�̍쐬�͎��s���܂����B"
                'Server.Transfer("CommonErr.aspx")

                Call Me.ShowResult("�񍐏��u" & ViewState("FileName").ToString & ".pdf" & "�v�̍쐬�͎��s���܂����B", True)
                Return

            Else
                Try
                    '���[���R�s�[����
                    Dim pdfFile As New System.IO.FileInfo(TyouhyouPath)
                    pdfFile.CopyTo(strHoukokusyoPdfPath & ViewState("FileName") & ".pdf", True)

                    'Call Me.CloseWindow()
                    'ClientScript.RegisterClientScriptBlock(Page.GetType(), "setCopy", "<script type =""text/javascript"" > window.onload=function(){ setTimeout(""document.getElementById('" & Me.btnStart.ClientID & "').click();"",0);}</script>")

                    Call Me.ShowResult("�񍐏��u" & ViewState("FileName").ToString & ".pdf" & "�v���쐬���܂����B", False)
                    Return

                Catch ex As Exception
                    'PDF�i�[�����s�̏ꍇ
                    'Context.Items("strFailureMsg") = "�t�@�C���u" & ViewState("FileName").ToString & ".pdf" & "�v�͊J���Ă��܂��B�i�[�ł��܂���B"
                    'Server.Transfer("CommonErr.aspx")

                    Call Me.ShowResult("�t�@�C���u" & ViewState("FileName").ToString & ".pdf" & "�v�͊J���Ă��܂��B�i�[�ł��܂���B", True)
                    Return
                End Try

                Return

            End If

        End While

    End Sub

    Private Sub CloseWindow()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "CloseWindow"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function CloseWindow(){")
            .AppendLine("   alert('�t�@�C���o�͂��������܂����B');")
            .AppendLine("   window.opener.document.getElementById('btnShowModal').click();")
            .AppendLine("   window.close();")
            .AppendLine("}")
            .AppendLine("setTimeout('CloseWindow();',1000);")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    Private Sub ShowResult(ByVal strMsg As String, ByVal errFlg As Boolean)

        Dim dt As Data.DataTable
        Dim dr As Data.DataRow

        If ViewState("Msg") Is Nothing Then
            dt = New Data.DataTable
            dt.Columns.Add(New Data.DataColumn("no"))
            dt.Columns.Add(New Data.DataColumn("msg"))
            dt.Columns.Add(New Data.DataColumn("err_flg"))
        Else
            dt = CType(ViewState("Msg"), Data.DataTable)
        End If

        dr = dt.NewRow
        dr.Item("no") = dt.Rows.Count + 1
        dr.Item("msg") = strMsg
        dr.Item("err_flg") = IIf(errFlg, "1", "0").ToString
        dt.Rows.InsertAt(dr, 0)

        ViewState("Msg") = dt

        Me.grdMsg.DataSource = dt
        Me.grdMsg.DataBind()

        ClientScript.RegisterClientScriptBlock(Page.GetType(), "setCopy", "<script type =""text/javascript"" > window.onload=function(){ setTimeout(""document.getElementById('" & Me.btnStart.ClientID & "').click();"",0);}</script>")
    End Sub

    Private Sub grdMsg_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMsg.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(e.Row.Cells.Count - 1).Text.Equals("1") Then
                e.Row.ForeColor = Drawing.Color.Red
            End If

            e.Row.Cells(e.Row.Cells.Count - 1).Visible = False
        End If
    End Sub

#End Region


#Region "PDF���J��"
    Private Sub ShowPdf(ByVal url As String)

        Me.hiddenIframe.Attributes.Add("src", url)
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "setCopy", "<script type =""text/javascript"" > window.onload=function(){ setTimeout(""document.getElementById('" & Me.btnOpenFile.ClientID & "').click();"",1000);}</script>")
    End Sub

    Private Sub btnOpenFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click

        Me.hiddenIframe.Attributes.Remove("src")
        Me.Page.Title = ViewState("FileName").ToString

        Dim TyouhyouPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouPath").ToString
        TyouhyouPath = TyouhyouPath & ViewState("FileName").ToString & ".pdf"

        Dim tryAgainFlg As Boolean = True
        Dim againCnt As Integer = 0
        While tryAgainFlg
            If Not System.IO.File.Exists(TyouhyouPath) Then
                If againCnt < 3 Then
                    againCnt = againCnt + 1
                    System.Threading.Thread.Sleep(1000)
                    Continue While
                Else
                    tryAgainFlg = False
                End If

                'PDF�쐬�����s�̏ꍇ
                Context.Items("strFailureMsg") = "�񍐏��u" & ViewState("FileName") & ".pdf" & "�v�̍쐬�͎��s���܂����B"
                Server.Transfer("CommonErr.aspx")

            Else
                '���[��Open����
                Call Me.OpenPdfFile(TyouhyouPath)


                'Dim pdfSaveFolder As String = "pdf"
                'Dim tempSavePath As String = Server.MapPath(pdfSaveFolder)
                'Dim copyFromDir As New System.IO.DirectoryInfo(tempSavePath)
                'For Each pdfFile As System.IO.FileInfo In copyFromDir.GetFiles("*.pdf")
                '    pdfFile.Delete()
                'Next
                'tempSavePath = tempSavePath & "/" & ViewState("FileName") & ".pdf"
                'System.IO.File.Copy(TyouhyouPath, tempSavePath, True)
                ''���[��Open����
                'Call Me.OpenPdfFile(pdfSaveFolder & "/" & ViewState("FileName") & ".pdf")

                Return

            End If

        End While

    End Sub

    Private Sub OpenPdfFile(ByVal strFileName As String)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function OpenPdfFile(){")
            .AppendLine("   document.getElementById('divWord').style.display='none';")
            .AppendLine("   window.location.href = '" & strFileName.Replace("\", "\\") & "';")
            '.AppendLine("   window.open('" & strFileName.Replace("\", "\\") & "','HoukokusyoPdf' + (new Date()).getTime(),'width=1024,height=768,top=0,left=0,status=yes,resizable=yes,directories=yes,scrollbars=yes');")
            '.AppendLine("   window.close();")
            .AppendLine("}")
            .AppendLine("setTimeout('OpenPdfFile();',10);")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub
#End Region





    Private Function GetAllKensaKouteiName(ByVal dt As Data.DataTable) As String

        Dim lst As New List(Of String)
        For Each row As Data.DataRow In dt.Rows
            If Not row.Item("�����H����").ToString.Trim.Equals(String.Empty) Then
                lst.Add(row.Item("�����H����").ToString.Trim)
            End If
        Next

        If lst.Count > 0 Then
            Return String.Join("�E", lst.ToArray)
        Else
            Return String.Empty
        End If

    End Function


    ''' <summary>
    ''' �u�\���v������FixedDataSection
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection_Hyousi(ByVal data As DataTable) As String

        '�f�[�^���擾
        Return fcw.GetFixedDataSection( _
                                        "�{�喼" & _
                                        ",�����X��", data)

    End Function

    ''' <summary>
    ''' �u�����v������FixedDataSection
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection_Hakuusi(ByVal data As DataTable) As String

        '�f�[�^���擾
        Return fcw.GetFixedDataSection("��", data)

    End Function

    ''' <summary>
    ''' �u�����T�v�v������FixedDataSection
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection_KensaGaiyou(ByVal data As DataTable) As String

        '�f�[�^���擾
        Return fcw.GetFixedDataSection( _
                                        "�{�喼" & _
                                        ",�����Z��1" & _
                                        ",�����Z��2_3" & _
                                        ",�����\���K��" & _
                                        ",�����H����1_10" & _
                                        ",FC��", data)

    End Function

    ''' <summary>
    ''' �u�������茋�ʁv������FixedDataSection
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection_KensaHanteiKekka(ByVal data As DataTable) As String

        '�f�[�^���擾
        Return fcw.GetFixedDataSection("��������", data)

    End Function
    ''' <summary>
    ''' �u�������茋�ʁv������TableDataSection
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTableDataSection_KensaHanteiKekka(ByVal data As DataTable) As String

        '����CLASS
        Dim earthAction As New EarthAction

        '�f�[�^���擾
        Return earthAction.JoinDataTable( _
                                        data, _
                                        APOST, _
                                        "�����H����" & _
                                        ",�������{��" & _
                                        ",��������")

    End Function

    ''' <summary>
    ''' �u�����v������TableDataSection
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTableDataSection_Memo(ByVal data As DataTable) As String

        '����CLASS
        Dim earthAction As New EarthAction

        '�f�[�^���擾
        Return earthAction.JoinDataTable( _
                                        data, _
                                        APOST, _
                                        "��")

    End Function

    ''' <summary>
    ''' �u���\���v������FixedDataSection
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection_End(ByVal data As DataTable) As String

        '�f�[�^���擾
        Return fcw.GetFixedDataSection("��", data)

    End Function




    Private Sub WaitOutput()
        Dim outputIndex As Integer = CInt(Me.hidIndex.Value)
        System.Threading.Thread.Sleep(outputIndex * 1001)
        'Response.Write(outputIndex * 1001)
        'Response.Write("<br>")
        'Response.Write(Now.ToString("HH:mm:ss.fff"))

    End Sub

End Class
