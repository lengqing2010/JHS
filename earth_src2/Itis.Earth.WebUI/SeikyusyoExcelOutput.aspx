<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SeikyusyoExcelOutput.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyusyoExcelOutput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>無題のページ</title>
</head>

<script language="vbscript" type="text/vbscript">
<!--

    Sub LoadExcel()
        Call lo_c_Open_Excel("<%= xltFolder %>","<%= xltFile %>","<%= csvFolder %>","<%= csvDataFile %>","<%= csvData %>","<%= strErr %>")
    End Sub

    ' CSV ファイルを作成し、Excel を起動
Sub lo_c_Open_Excel(XltFolder,XltFile,CsvFolder,CsvDataFile,csvData,strErr) 

    Dim objFso 
    Dim objTxtD
    Dim objXls 
    Dim objExcelApp 
    if strErr="" then

        if  csvData <> "" Then 
            ' CSV ファイル作成 
            Set objFso = CreateObject("Scripting.FileSystemObject") 
            Set objTxtD = objFso.CreateTextFile(CsvFolder & CsvDataFile, True, False) 

            Dim strRepAfterD    ' 置換後の文字列 
            ' 「@@@」を改行コードに置き換える 

            strRepAfterD = Replace(csvData, "@@@", vbnewline) 
            ' テキストファイルに書き出し 
      
            objTxtD.WriteLine(strRepAfterD) 
 
            objTxtD.Close 
            
            ' Excel 起動 
            Set objXls = CreateObject("Excel.Application") 
            objXls.Visible = false 
            objXls.UserControl = True 
            On Error resume next 
            Set objExcelApp = objXls.Workbooks.Open(XltFolder & XltFile) 
            If err.number = 0 Then 
                objXls.Visible = false 
               
                objXls.Run(objXls.ActiveWorkbook.name & "!Auto_open") 
                objXls.Visible = True 
                Set objXls = Nothing 
            Else 
                Err.Clear 
                Call Msgbox("ファイルが存在しません。" & vbCrLf & "フォルダ名：" & XltFolder & vbCrLf & "ファイル名：" & XltFile, vbOkOnly + vbExclamation) 
            End If

            Set objTxtD = Nothing 
            Set objFso = Nothing 
        else 
            call funOutError(strErr) 
        end if 
       
    else
       call funOutError(strErr) 
    end if
    
End Sub

//-->
</script>


<body id="CreateCSV" onload="">

<script language='javascript' type='text/javascript'>
    function funBtnEnable1(){
        setTimeout(function(){
            var buyDivID=document.getElementById('hidSelName').value;        
            window.parent.opener.document.getElementById(buyDivID).style.display='none';
            var DisableDivID=document.getElementById('hidDisableDiv').value;        
            window.parent.opener.document.getElementById(DisableDivID).style.display='none';
            //window.parent.close();        
        },0);
    }

</script> 
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hidDenpyouTorihikisakis" runat="server" />
            <asp:HiddenField ID="hidKeijyoubi" runat="server" />
            <asp:HiddenField ID="hidSelName" runat="server" />
            <asp:HiddenField ID="hidDisableDiv" runat="server" />
        </div>
    </form>
    
<script language='javascript' type='text/javascript'>
        function funBtnEnable(){
            var buyDivID=document.getElementById('hidSelName').value;        
            window.parent.opener.document.getElementById(buyDivID).style.display='none';
            var DisableDivID=document.getElementById('hidDisableDiv').value;        
            window.parent.opener.document.getElementById(DisableDivID).style.display='none';
            window.parent.close();        
    }
//    function Minimize() 
//    {
//    window.innerWidth = 100;
//    window.innerHeight = 100;
//    window.screenX = screen.width;
//    window.screenY = screen.height;
//    alwaysLowered = true;
//    }


//    Minimize();
    setTimeout(function(){LoadExcel()},0);
    setTimeout(function(){funBtnEnable()},1);
    
    


    
</script> 
</body>
</html>
