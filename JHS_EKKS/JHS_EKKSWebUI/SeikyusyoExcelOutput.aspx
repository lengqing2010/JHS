<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SeikyusyoExcelOutput.aspx.vb" Inherits="SeikyusyoExcelOutput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>無題のページ</title>
</head>


<script language='javascript' type='text/javascript'>
    function funBtnEnable(CsvDataFile){
        var buyDivID=document.getElementById('hidSelName').value;        
        window.parent.opener.document.getElementById(buyDivID).style.display='none';
        var DisableDivID=document.getElementById('hidDisableDiv').value;        
        window.parent.opener.document.getElementById(DisableDivID).style.display='none';
        window.parent.close();

       //window.open("DownLoad.aspx?df="+ encodeURI(CsvDataFile),"window","height=100,width=400,scroll=no,help=0,status=0,state=0,resizable=no,maximize=no,minimize=no;")
    }
</script> 

<body id="CreateCSV" >
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hidDenpyouTorihikisakis" runat="server" />
            <asp:HiddenField ID="hidKeijyoubi" runat="server" />
            <asp:HiddenField ID="hidSelName" runat="server" />
            <asp:HiddenField ID="hidDisableDiv" runat="server" />
        </div>
    </form>
</body>
</html>
