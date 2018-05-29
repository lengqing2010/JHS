Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data

Partial Public Class SeikyusyoFcwOutputTestPage
    Inherits System.Web.UI.Page
#Region "パース"
    Public Const sDirName As String = "C:\JHS\earth"
#End Region
#Region "サービスパース"
    Public sv_templist As String = getfiledatetimelist("data")
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call check()
        If Not IsPostBack Then


            '「Excel出力」ボタンを押下する
            Me.btnExcel.Attributes.Add("onclick", "body_onLoad(""1"");return false;")

        Else

            Select Case hidSeni.Value
                Case "1" 'Excel出力
                    Dim strSeikyusyoNo As String = ""
                    ViewState("strNo") = tbx_seikyusyo_no.Text.Trim
                    MakeJavaScript()
                    ClientScript.RegisterStartupScript(Me.GetType(), "ERR", "PopPrint();", True)
            End Select
        End If

    End Sub

    ''' <summary>
    ''' 請求先元帳帳票出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnInsatu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsatu.Click

        Dim strSeikyusyoNo As String
        strSeikyusyoNo = tbx_seikyusyo_no.Text
        Response.Redirect("/jhs_earth/SeikyusyoFcwOutput.aspx?Kbn=1&seino=" & strSeikyusyoNo)


    End Sub

    ''' <summary>
    ''' Excel出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2010/09/21 車龍(大連情報システム部)　新規作成</history>
    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

       
    End Sub
    ''' <summary>
    ''' EXCELテンプレートファイルチェク
    ''' </summary>
    ''' <history>
    ''' [大連情報システム部]	2007/09/10	P-36729　新規作成 高 
    ''' </history>
    Private Sub check()

    

        Dim csType As Type = Page.GetType
        Dim csName As String = "check"
        Dim csScript As New StringBuilder

        csScript.Append("<script language='vbscript' type='text/vbscript'>" & vbCrLf)
        csScript.Append("function body_onLoad(obj)" & vbCrLf)
        csScript.Append("   Dim sv_templist,cr_templist,strHour, strMinute,strYEAR, strMONTH, strDAY" & vbCrLf)
        csScript.Append("   Dim TimeStamp,dwn_flg,c_obj_Fso,File_Path,c_obj_Folder,c_obj_File,fl,i" & vbCrLf)
        csScript.Append("   On Error Resume Next" & vbCrLf)
        csScript.Append("       dwn_flg = false" & vbCrLf)
        csScript.Append("       sv_templist=""" & sv_templist & """" & vbCrLf)
        csScript.Append("       cr_templist =""""" & vbCrLf)
        csScript.Append("       File_Path =""" & sDirName & """" & vbCrLf)
        csScript.Append("       If sv_templist <> """" Then" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = c_obj_Fso.GetFolder(File_Path)" & vbCrLf)
        csScript.Append("           Set c_obj_File = c_obj_Folder.Files" & vbCrLf)
        csScript.Append("           For Each fl In c_obj_File" & vbCrLf)

        csScript.Append("               If (Ucase(right(fl.Name,3)) = ""XLT"" OR  Ucase(right(fl.Name,3)) = ""XLS"") Then" & vbCrLf)
        csScript.Append("                   If cr_templist <> """" Then" & vbCrLf)
        csScript.Append("                       cr_templist = cr_templist & "",""" & vbCrLf)
        csScript.Append("                   End If" & vbCrLf)
        csScript.Append("                   strYEAR   = Right(""0000"" & CStr(YEAR(fl.DateLastModified)), 4)" & vbCrLf)
        csScript.Append("                   strMONTH  = Right(""00"" & CStr(MONTH(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strDAY    = Right(""00"" & Cstr(DAY(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strHour   = Right(""00"" & CStr(Hour(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strMinute = Right(""00"" & CStr(Minute(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   timeStamp = strYEAR & strMONTH & strDAY & strHour & strMinute" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"" "","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,""/"","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"":"","""")" & vbCrLf)

        csScript.Append("                   cr_templist = cr_templist & trim(fl.Name) & "":"" & trim(timestamp)" & vbCrLf)

        csScript.Append("               End If" & vbCrLf)
        csScript.Append("           Next" & vbCrLf)

        csScript.Append("           Set c_obj_File = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = Nothing" & vbCrLf)
        csScript.Append("           If Err > 0 or cr_templist = """" Then" & vbCrLf)
        csScript.Append("               dwn_flg = true" & vbCrLf)
        csScript.Append("           Else" & vbCrLf)
        csScript.Append("               ar_cr = split(Ucase(cr_templist), "","")" & vbCrLf)
        csScript.Append("               ar_sv = split(Ucase(sv_templist), "","")" & vbCrLf)
        csScript.Append("               For i = 0 to Ubound(ar_sv)" & vbCrLf)
        csScript.Append("                   rc = Filter(ar_cr, ar_sv(i))" & vbCrLf)
        csScript.Append("                   If Ubound(rc) = 0 then" & vbCrLf)
        csScript.Append("                       If rc(0) = """" then" & vbCrLf)
        csScript.Append("                           dwn_flg = true" & vbCrLf)
        csScript.Append("                           Exit For" & vbCrLf)
        csScript.Append("                       End if" & vbCrLf)
        csScript.Append("                   Else" & vbCrLf)
        csScript.Append("                       dwn_flg = true" & vbCrLf)
        csScript.Append("                       Exit For" & vbCrLf)
        csScript.Append("                   End if" & vbCrLf)
        csScript.Append("               Next" & vbCrLf)
        csScript.Append("           End if" & vbCrLf)
        csScript.Append("           If dwn_flg = true Then" & vbCrLf)
        csScript.Append("               call download(obj)" & vbCrLf)
        csScript.Append("           End If" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("       If dwn_flg = false Then" & vbCrLf)
        csScript.Append("           fncSubmit(obj)" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("End function" & vbCrLf)

        csScript.Append("function body_onLoad2(obj)" & vbCrLf)
        csScript.Append("   Dim sv_templist,cr_templist,strHour, strMinute,strYEAR, strMONTH, strDAY" & vbCrLf)
        csScript.Append("   Dim TimeStamp,dwn_flg,c_obj_Fso,File_Path,c_obj_Folder,c_obj_File,fl,i" & vbCrLf)
        csScript.Append("   On Error Resume Next" & vbCrLf)
        csScript.Append("       dwn_flg = false" & vbCrLf)
        csScript.Append("       sv_templist=""" & sv_templist & """" & vbCrLf)
        csScript.Append("       cr_templist =""""" & vbCrLf)
        csScript.Append("       File_Path =""" & sDirName & """" & vbCrLf)
        csScript.Append("       If sv_templist <> """" Then" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = c_obj_Fso.GetFolder(File_Path)" & vbCrLf)
        csScript.Append("           Set c_obj_File = c_obj_Folder.Files" & vbCrLf)
        csScript.Append("           For Each fl In c_obj_File" & vbCrLf)
        csScript.Append("               If (Ucase(right(fl.Name,3)) = ""XLT"" OR  Ucase(right(fl.Name,3)) = ""XLS"") Then" & vbCrLf)
        csScript.Append("                   If cr_templist <> """" Then" & vbCrLf)
        csScript.Append("                       cr_templist = cr_templist & "",""" & vbCrLf)
        csScript.Append("                   End If" & vbCrLf)
        csScript.Append("                   strYEAR   = Right(""0000"" & CStr(YEAR(fl.DateLastModified)), 4)" & vbCrLf)
        csScript.Append("                   strMONTH  = Right(""00"" & CStr(MONTH(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strDAY    = Right(""00"" & Cstr(DAY(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strHour   = Right(""00"" & CStr(Hour(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strMinute = Right(""00"" & CStr(Minute(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   timeStamp = strYEAR & strMONTH & strDAY & strHour & strMinute" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"" "","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,""/"","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"":"","""")" & vbCrLf)
        csScript.Append("                   cr_templist = cr_templist & trim(fl.Name) & "":"" & trim(timestamp)" & vbCrLf)
        csScript.Append("               End If" & vbCrLf)
        csScript.Append("           Next" & vbCrLf)

        csScript.Append("           Set c_obj_File = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = Nothing" & vbCrLf)
        csScript.Append("           If Err > 0 or cr_templist = """" Then" & vbCrLf)
        csScript.Append("               dwn_flg = true" & vbCrLf)
        csScript.Append("           Else" & vbCrLf)
        csScript.Append("               ar_cr = split(Ucase(cr_templist), "","")" & vbCrLf)
        csScript.Append("               ar_sv = split(Ucase(sv_templist), "","")" & vbCrLf)
        csScript.Append("               For i = 0 to Ubound(ar_sv)" & vbCrLf)
        csScript.Append("                   rc = Filter(ar_cr, ar_sv(i))" & vbCrLf)
        csScript.Append("                   If Ubound(rc) = 0 then" & vbCrLf)
        csScript.Append("                       If rc(0) = """" then" & vbCrLf)
        csScript.Append("                           dwn_flg = true" & vbCrLf)
        csScript.Append("                           Exit For" & vbCrLf)
        csScript.Append("                       End if" & vbCrLf)
        csScript.Append("                   Else" & vbCrLf)
        csScript.Append("                       dwn_flg = true" & vbCrLf)
        csScript.Append("                       Exit For" & vbCrLf)
        csScript.Append("                   End if" & vbCrLf)
        csScript.Append("               Next" & vbCrLf)
        csScript.Append("           End if" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("       If dwn_flg = false Then" & vbCrLf)
        csScript.Append("           call fncSubmit(obj)" & vbCrLf)
        csScript.Append("       else" & vbCrLf)
        csScript.Append("           call body_load3(obj)" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("End function" & vbCrLf)
        csScript.Append("</script>" & vbCrLf)

        csScript.Append("<script language='javascript' type='text/javascript'>" & vbCrLf)
        csScript.Append("function download(obj){" & vbCrLf)
        csScript.Append("   window.location.href='data/earth.lha';" & vbCrLf)
        csScript.Append("   body_load3(obj);" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.Append("function body_load3(obj){" & vbCrLf)
        csScript.Append("   setTimeout('body_onLoad2(' + obj + ')',1000);" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.Append("function fncSubmit(obj) {" & vbCrLf)

        csScript.Append(Form.Name & "." & hidSeni.ClientID & ".value = obj;" & vbCrLf)
        csScript.Append(Form.Name & ".submit();" & vbCrLf)
        csScript.Append("}" & vbCrLf)
        csScript.Append("</script>" & vbCrLf)

        ClientScript.RegisterStartupScript(csType, csName, csScript.ToString)
    End Sub
    Private Function getfiledatetimelist(ByVal path As String) As String
        Dim fo As New Scripting.FileSystemObject
        Dim fp As String
        Dim fr As Scripting.Folder
        Dim fc As Scripting.Files
        Dim fl As Scripting.File
        Dim fname As String, s As String, timestamp As String
        Dim strHour As String, strMinute As String
        Dim strYEAR As String, strMONTH As String, strDAY As String

        fp = Server.MapPath(path)
        fr = fo.GetFolder(fp)
        fc = fr.Files
        s = ""
        For Each fl In fc
            fname = fl.Name
            If (UCase(Right(fname, 3)) = "XLT" Or UCase(Right(fname, 3)) = "XLS") Then
                If s <> "" Then
                    s = s & ","
                End If
                timestamp = CStr(fl.DateLastModified)
                ''日付時間軸の整形
                strYEAR = Right("0000" & CStr(Year(fl.DateLastModified)), 4)
                strMONTH = Right("00" & CStr(Month(fl.DateLastModified)), 2)
                strDAY = Right("00" & CStr(Day(fl.DateLastModified)), 2)
                strHour = Right("00" & CStr(Hour(fl.DateLastModified)), 2)
                strMinute = Right("00" & CStr(Minute(fl.DateLastModified)), 2)
                timestamp = strYEAR & strMONTH & strDAY & strHour & strMinute
                timestamp = Replace(timestamp, " ", "")
                timestamp = Replace(timestamp, "/", "")
                timestamp = Replace(timestamp, ":", "")
                s = s & Trim(fname) & ":" & Trim(timestamp)
            End If
        Next
        getfiledatetimelist = s
        fo = Nothing
        fp = Nothing
        fr = Nothing
        fc = Nothing
        fl = Nothing
    End Function
    ''' <summary>
    ''' JavaScript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/09/24 車龍(大連情報システム部)　新規作成</history>
    Protected Sub MakeJavaScript()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("    function ShowModal(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       if(buyDiv.style.display=='none')")
            .AppendLine("       {")
            .AppendLine("        buyDiv.style.display='';")
            .AppendLine("        disable.style.display='';")
            .AppendLine("        disable.style.width=995;")
            .AppendLine("        disable.style.height=600;")
            .AppendLine("        disable.focus();")
            .AppendLine("       }else{")
            .AppendLine("        buyDiv.style.display='none';")
            .AppendLine("        disable.style.display='none';")
            .AppendLine("       }")
            .AppendLine("    }")
            .AppendLine("    function PopPrint(){")
            .AppendLine("       ShowModal();")
            .AppendLine("       var objwindow=window.open(encodeURI('WaitMsg.aspx?url=SeikyusyoExcelOutput.aspx?strNoAddId=" & CStr(ViewState("strNo")) & "," & Me.buySelName.ClientID & "," & Me.disableDiv.ClientID & "'),'proxy_operation','width=450,height=150,status=no,resizable=no,directories=no,scrollbars=no,left=0,top=0');" & vbCrLf)
            .AppendLine("       objwindow.focus();")
            .AppendLine("    }" & vbCrLf)
            .AppendLine("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

End Class