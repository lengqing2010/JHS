Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data

Partial Public Class SoufujyouPdfOutput
    Inherits System.Web.UI.Page

#Region "�v���C�x�[�g�ϐ�"
    '���O�C�����[�U�[���擾����B
    Private ninsyou As New Ninsyou()

    Private kensaHoukokusyoOutputLogic As New KensaHoukokusyoOutputLogic
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "Soufujyou"
    Private loginID As String
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

        '��{�F��
        If Ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        Else
            loginID = Ninsyou.GetUserID()
        End If

        If Not IsPostBack Then

            ViewState("KanriNo") = Request("KanriNo")

            Call Me.CreateFcwTyouhyouData()

            Me.Page.Title = ViewState("FileName").ToString

        End If

    End Sub


    Private Sub CreateFcwTyouhyouData()

        Dim sb As New Text.StringBuilder
        Dim editDt As New DataTable

        Dim errMsg As String = String.Empty
        Dim syainCd As String                           '�Ј��R�[�h

        Dim kanriNo As String = "'" & ViewState("KanriNo").ToString.Replace(SEP_STRING, "','") & "'"

        '�f�[�^�擾
        Dim soufujyouDt As Data.DataTable
        soufujyouDt = kensaHoukokusyoOutputLogic.GetSyoufujyouPdfInfo(kanriNo)

        '�ő�̔�����
        Dim maxHassouDate As String = StrDup(6, "_")
        If soufujyouDt.Rows.Count > 0 Then
            maxHassouDate = Convert.ToDateTime(soufujyouDt.Compute("MAX(hassou_date)", "")).ToString("yyyyMMdd")
        End If

        ViewState("FileName") = "�R��_�����񍐏�_" & maxHassouDate & Now.ToString("HHmm") & "_���t��"

        syainCd = loginID
        fcw = New FcwUtility(Page, syainCd, kinouId, ".fcp")

        If Not soufujyouDt.Rows.Count > 0 Then
            '�f�[�^���Ȃ��ꍇ
            errMsg = fcw.GetErrMsg(PDFStatus.NoData)

        Else
            '[Head] ���쐬
            sb.Append(fcw.CreateDatHeader(APOST.ToString))

            editDt.Columns.Add(New Data.DataColumn("�����X�R�[�h", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("�ʂ�No", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("�����X��", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("�Z��1", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("�Z��2", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("������", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("�X�֔ԍ�", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("�d�b�ԍ�", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("������", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("�S��", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("�ԍ�_����", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("����", GetType(String)))


            'Dim kameitenCdOld As String = soufujyouDt.Rows(0).Item("kameiten_cd").ToString '�����X�R�[�h(��r�p)
            Dim tooshiNoOld As String = soufujyouDt.Rows(0).Item("tooshi_no").ToString '�ʂ�No(��r�p)

            For i As Integer = 0 To soufujyouDt.Rows.Count - 1
                With soufujyouDt.Rows(i)
                    Dim kameitenCd As String = .Item("kameiten_cd").ToString  '�����X�R�[�h
                    Dim tooshiNo As String = .Item("tooshi_no").ToString '�ʂ�No

                    'If (Not kameitenCd.Equals(kameitenCdOld)) OrElse (Not tooshiNo.Equals(tooshiNoOld)) Then
                    '    '�����X�R�[�h or �ʂ�No ��ς���ꍇ

                    If Not tooshiNo.Equals(tooshiNoOld) Then
                        '�ʂ�No��ς���ꍇ

                        sb.Append(vbCrLf)
                        '[FixedDataSection] ���쐬
                        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection(editDt)))
                        '[TableDataSection] ���쐬
                        sb.Append(fcw.CreateTableDataSection(GetTableDataSection(editDt)))

                        '�N���A
                        For rowIndex As Integer = editDt.Rows.Count - 1 To 0 Step -1
                            editDt.Rows.RemoveAt(rowIndex)
                        Next

                        'kameitenCdOld = kameitenCd
                        tooshiNoOld = tooshiNo
                    End If

                    Dim dr As Data.DataRow
                    dr = editDt.NewRow
                    dr.Item("�����X�R�[�h") = kameitenCd
                    dr.Item("�ʂ�No") = tooshiNo
                    dr.Item("�����X��") = .Item("kameiten_mei").ToString
                    dr.Item("�Z��1") = .Item("jyuusyo1").ToString
                    dr.Item("�Z��2") = .Item("jyuusyo2").ToString
                    dr.Item("������") = .Item("busyo_mei").ToString
                    dr.Item("�X�֔ԍ�") = .Item("yuubin_no").ToString
                    dr.Item("�d�b�ԍ�") = .Item("tel_no").ToString
                    dr.Item("������") = Convert.ToDateTime(.Item("hassou_date")).ToString("yyyy�NMM��dd��")
                    dr.Item("�S��") = .Item("souhu_tantousya").ToString
                    dr.Item("�ԍ�_����") = .Item("kbn").ToString & .Item("hosyousyo_no").ToString & "�F" & .Item("sesyu_mei").ToString
                    dr.Item("����") = .Item("kensa_hkks_busuu").ToString
                    editDt.Rows.Add(dr)
                End With
            Next

            If editDt.Rows.Count > 0 Then
                sb.Append(vbCrLf)
                '[FixedDataSection] ���쐬
                sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection(editDt)))
                '[TableDataSection] ���쐬
                sb.Append(fcw.CreateTableDataSection(GetTableDataSection(editDt)))
            End If

            'DAT�t�@�C���쐬
            errMsg = fcw.GetErrMsg(fcw.WriteData(sb.ToString))
        End If

        'PDF�@�o��
        If Not errMsg.Equals(String.Empty) Then
            '���b�Z�[�W
            Context.Items("strFailureMsg") = errMsg
            Server.Transfer("CommonErr.aspx")

        Else
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
                Context.Items("strFailureMsg") = "�t�@�C���u" & ViewState("FileName").ToString & ".pdf" & "�v�͊J���Ă��܂��B�쐬�ł��܂���B"
                Server.Transfer("CommonErr.aspx")
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
        Dim strSoufujyouPdfPath As String = System.Configuration.ConfigurationManager.AppSettings("SoufujyouPdfPath").ToString
        strSoufujyouPdfPath = strSoufujyouPdfPath & IIf(Right(strSoufujyouPdfPath, 1).Equals("\"), String.Empty, "\").ToString

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
                Context.Items("strFailureMsg") = "���t��PDF�̍쐬�͎��s���܂����B"
                Server.Transfer("CommonErr.aspx")

            Else
                Try
                    '���[���R�s�[����
                    Dim pdfFile As New System.IO.FileInfo(TyouhyouPath)
                    pdfFile.CopyTo(strSoufujyouPdfPath & ViewState("FileName") & ".pdf", True)

                    Call Me.CloseWindow()

                Catch ex As Exception
                    'PDF�i�[�����s�̏ꍇ
                    Context.Items("strFailureMsg") = "�t�@�C���u" & ViewState("FileName").ToString & ".pdf" & "�v�͊J���Ă��܂��B�i�[�ł��܂���B"
                    Server.Transfer("CommonErr.aspx")
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
            .AppendLine("   window.close();")
            .AppendLine("}")
            .AppendLine("CloseWindow();")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

#End Region



#Region "PDF���J��"
    Private Sub ShowPdf(ByVal url As String)

        Me.hiddenIframe.Attributes.Add("src", url)

        ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowPdf", "<script type =""text/javascript"" > window.onload=function(){ setTimeout(""document.getElementById('" & Me.btnOpenFile.ClientID & "').click();"",1000);}</script>")
    End Sub

    Private Sub btnOpenFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click

        Me.hiddenIframe.Attributes.Remove("src")

        Me.Page.Title = ViewState("FileName").ToString

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
                Context.Items("strFailureMsg") = "���t��PDF�̍쐬�͎��s���܂����B"
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
        Dim csName As String = "OpenPdfFile"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function OpenPdfFile(){")
            .AppendLine("   document.getElementById('divWord').style.display='none';")
            .AppendLine("   window.location.href = '" & strFileName.Replace("\", "\\") & "';")
            '.AppendLine("   window.open('" & strFileName.Replace("\", "\\") & "','SoufujyouPdf' + (new Date()).getTime(),'width=1024,height=768,top=0,left=0,status=yes,resizable=yes,directories=yes,scrollbars=yes');")
            '.AppendLine("   window.close();")
            .AppendLine("}")
            .AppendLine("setTimeout('OpenPdfFile();',10);")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub
#End Region

    Private Function GetFixedDataSection(ByVal data As DataTable) As String

        '�f�[�^���擾
        Return fcw.GetFixedDataSection( _
                                        "�����X�R�[�h" & _
                                        ",�ʂ�No" & _
                                        ",�����X��" & _
                                        ",�Z��1" & _
                                        ",�Z��2" & _
                                        ",������" & _
                                        ",�X�֔ԍ�" & _
                                        ",�d�b�ԍ�" & _
                                        ",������" & _
                                        ",�S��", data)
    End Function

    Private Function GetTableDataSection(ByVal data As DataTable) As String

        '����CLASS
        Dim earthAction As New EarthAction

        '�f�[�^���擾
        Return earthAction.JoinDataTable( _
                                        data, _
                                        APOST, _
                                        "�ԍ�_����" & _
                                        ",����")

    End Function
End Class
