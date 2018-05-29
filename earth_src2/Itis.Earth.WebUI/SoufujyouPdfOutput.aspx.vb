Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data

Partial Public Class SoufujyouPdfOutput
    Inherits System.Web.UI.Page

#Region "プライベート変数"
    'ログインユーザーを取得する。
    Private ninsyou As New Ninsyou()

    Private kensaHoukokusyoOutputLogic As New KensaHoukokusyoOutputLogic
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "Soufujyou"
    Private loginID As String
    Private Const APOST As Char = ","c
    Private Const SEP_STRING As String = "$$$"

    Enum PDFStatus As Integer

        OK = 0                              '正常
        IOException = 1                     'エラー(他のユーザがファイルを開いている)
        UnauthorizedAccessException = 2     'エラー(ファイルを作成するパスが不正)
        NoData = 3                          '対象のデータが取得できません。

    End Enum
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")

        '基本認証
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
        Dim syainCd As String                           '社員コード

        Dim kanriNo As String = "'" & ViewState("KanriNo").ToString.Replace(SEP_STRING, "','") & "'"

        'データ取得
        Dim soufujyouDt As Data.DataTable
        soufujyouDt = kensaHoukokusyoOutputLogic.GetSyoufujyouPdfInfo(kanriNo)

        '最大の発送日
        Dim maxHassouDate As String = StrDup(6, "_")
        If soufujyouDt.Rows.Count > 0 Then
            maxHassouDate = Convert.ToDateTime(soufujyouDt.Compute("MAX(hassou_date)", "")).ToString("yyyyMMdd")
        End If

        ViewState("FileName") = "山梨_検査報告書_" & maxHassouDate & Now.ToString("HHmm") & "_送付状"

        syainCd = loginID
        fcw = New FcwUtility(Page, syainCd, kinouId, ".fcp")

        If Not soufujyouDt.Rows.Count > 0 Then
            'データがない場合
            errMsg = fcw.GetErrMsg(PDFStatus.NoData)

        Else
            '[Head] 部作成
            sb.Append(fcw.CreateDatHeader(APOST.ToString))

            editDt.Columns.Add(New Data.DataColumn("加盟店コード", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("通しNo", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("加盟店名", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("住所1", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("住所2", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("部署名", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("郵便番号", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("電話番号", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("発送日", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("担当", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("番号_名称", GetType(String)))
            editDt.Columns.Add(New Data.DataColumn("部数", GetType(String)))


            'Dim kameitenCdOld As String = soufujyouDt.Rows(0).Item("kameiten_cd").ToString '加盟店コード(比較用)
            Dim tooshiNoOld As String = soufujyouDt.Rows(0).Item("tooshi_no").ToString '通しNo(比較用)

            For i As Integer = 0 To soufujyouDt.Rows.Count - 1
                With soufujyouDt.Rows(i)
                    Dim kameitenCd As String = .Item("kameiten_cd").ToString  '加盟店コード
                    Dim tooshiNo As String = .Item("tooshi_no").ToString '通しNo

                    'If (Not kameitenCd.Equals(kameitenCdOld)) OrElse (Not tooshiNo.Equals(tooshiNoOld)) Then
                    '    '加盟店コード or 通しNo を変える場合

                    If Not tooshiNo.Equals(tooshiNoOld) Then
                        '通しNoを変える場合

                        sb.Append(vbCrLf)
                        '[FixedDataSection] 部作成
                        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection(editDt)))
                        '[TableDataSection] 部作成
                        sb.Append(fcw.CreateTableDataSection(GetTableDataSection(editDt)))

                        'クリア
                        For rowIndex As Integer = editDt.Rows.Count - 1 To 0 Step -1
                            editDt.Rows.RemoveAt(rowIndex)
                        Next

                        'kameitenCdOld = kameitenCd
                        tooshiNoOld = tooshiNo
                    End If

                    Dim dr As Data.DataRow
                    dr = editDt.NewRow
                    dr.Item("加盟店コード") = kameitenCd
                    dr.Item("通しNo") = tooshiNo
                    dr.Item("加盟店名") = .Item("kameiten_mei").ToString
                    dr.Item("住所1") = .Item("jyuusyo1").ToString
                    dr.Item("住所2") = .Item("jyuusyo2").ToString
                    dr.Item("部署名") = .Item("busyo_mei").ToString
                    dr.Item("郵便番号") = .Item("yuubin_no").ToString
                    dr.Item("電話番号") = .Item("tel_no").ToString
                    dr.Item("発送日") = Convert.ToDateTime(.Item("hassou_date")).ToString("yyyy年MM月dd日")
                    dr.Item("担当") = .Item("souhu_tantousya").ToString
                    dr.Item("番号_名称") = .Item("kbn").ToString & .Item("hosyousyo_no").ToString & "：" & .Item("sesyu_mei").ToString
                    dr.Item("部数") = .Item("kensa_hkks_busuu").ToString
                    editDt.Rows.Add(dr)
                End With
            Next

            If editDt.Rows.Count > 0 Then
                sb.Append(vbCrLf)
                '[FixedDataSection] 部作成
                sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection(editDt)))
                '[TableDataSection] 部作成
                sb.Append(fcw.CreateTableDataSection(GetTableDataSection(editDt)))
            End If

            'DATファイル作成
            errMsg = fcw.GetErrMsg(fcw.WriteData(sb.ToString))
        End If

        'PDF　出力
        If Not errMsg.Equals(String.Empty) Then
            'メッセージ
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
                'メッセージ
                Context.Items("strFailureMsg") = "ファイル「" & ViewState("FileName").ToString & ".pdf" & "」は開いています。作成できません。"
                Server.Transfer("CommonErr.aspx")
            Else
                Dim strUrl = fcw.GetUrlKensaHoukokusyo(ViewState("FileName").ToString)

                'Call Me.ShowPdf(strUrl)
                Call Me.CopyPdf(strUrl)
            End If

        End If

    End Sub

#Region "PDFをコピーする"
    Private Sub CopyPdf(ByVal url As String)

        Me.hiddenIframe.Attributes.Add("src", url)

        ClientScript.RegisterClientScriptBlock(Page.GetType(), "CopyPdf", "<script type =""text/javascript"" > window.onload=function(){ setTimeout(""document.getElementById('" & Me.btnCopy.ClientID & "').click();"",1000);}</script>")
    End Sub

    Private Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click

        '格納パス
        Dim strSoufujyouPdfPath As String = System.Configuration.ConfigurationManager.AppSettings("SoufujyouPdfPath").ToString
        strSoufujyouPdfPath = strSoufujyouPdfPath & IIf(Right(strSoufujyouPdfPath, 1).Equals("\"), String.Empty, "\").ToString

        '作成したファイル
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

                'PDF作成が失敗の場合
                Context.Items("strFailureMsg") = "送付状PDFの作成は失敗しました。"
                Server.Transfer("CommonErr.aspx")

            Else
                Try
                    '帳票をコピーする
                    Dim pdfFile As New System.IO.FileInfo(TyouhyouPath)
                    pdfFile.CopyTo(strSoufujyouPdfPath & ViewState("FileName") & ".pdf", True)

                    Call Me.CloseWindow()

                Catch ex As Exception
                    'PDF格納が失敗の場合
                    Context.Items("strFailureMsg") = "ファイル「" & ViewState("FileName").ToString & ".pdf" & "」は開いています。格納できません。"
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
            .AppendLine("   alert('ファイル出力が完了しました。');")
            .AppendLine("   window.close();")
            .AppendLine("}")
            .AppendLine("CloseWindow();")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

#End Region



#Region "PDFを開く"
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

                'PDF作成が失敗の場合
                Context.Items("strFailureMsg") = "送付状PDFの作成は失敗しました。"
                Server.Transfer("CommonErr.aspx")

            Else
                '帳票をOpenする
                Call Me.OpenPdfFile(TyouhyouPath)


                'Dim pdfSaveFolder As String = "pdf"
                'Dim tempSavePath As String = Server.MapPath(pdfSaveFolder)
                'Dim copyFromDir As New System.IO.DirectoryInfo(tempSavePath)
                'For Each pdfFile As System.IO.FileInfo In copyFromDir.GetFiles("*.pdf")
                '    pdfFile.Delete()
                'Next
                'tempSavePath = tempSavePath & "/" & ViewState("FileName") & ".pdf"
                'System.IO.File.Copy(TyouhyouPath, tempSavePath, True)
                ''帳票をOpenする
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

        'データを取得
        Return fcw.GetFixedDataSection( _
                                        "加盟店コード" & _
                                        ",通しNo" & _
                                        ",加盟店名" & _
                                        ",住所1" & _
                                        ",住所2" & _
                                        ",部署名" & _
                                        ",郵便番号" & _
                                        ",電話番号" & _
                                        ",発送日" & _
                                        ",担当", data)
    End Function

    Private Function GetTableDataSection(ByVal data As DataTable) As String

        '共通CLASS
        Dim earthAction As New EarthAction

        'データを取得
        Return earthAction.JoinDataTable( _
                                        data, _
                                        APOST, _
                                        "番号_名称" & _
                                        ",部数")

    End Function
End Class
