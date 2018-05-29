<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TyousaSijisyo.aspx.vb" Inherits="Itis.Earth.WebUI.TyousaSijisyo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>調査指示書</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-size:60px">
        <strong>
            <br />
            <br />
            <br />
            <br />
        <strong>実行中です</strong><BR><BR>
        <strong>しばらくお待ち下さい</strong><BR>
        

    </div>
    
            <iframe id="hiddenIframe1" runat="server" title="modalDiv" width="100%" height="50%" style="display: none;">
        </iframe>
        <asp:Button ID="btnRun" runat="server" Text="Button" style="display: none;" />
        <a id="file" href="" runat="server" style="display: none;">プレビュー</a>
    </form>
    
    
    <script language= "javascript">
    
        window.onload = function(){
             var iframe = document.getElementById("hiddenIframe1");
              if (iframe.src!=''){
                setTimeout(function(){ChkIframe()},200);
             }
        }
        
        
        
        function ChkIframe(){
        
            var iframe = document.getElementById("hiddenIframe1");
            
            if (iframe.src!=''){
                if(iframe.readyState === "complete" || iframe.readyState == "loaded"){
                    iframe.src = '';
                    document.getElementById("btnRun").click();
                }else{
                    setTimeout(function(){ChkIframe()},200);
                }
            }
            /*
            if (iframe.attachEvent){ 
                iframe.attachEvent("onload", function(){ 
                    alert("Local iframe is now loaded."); 
                }); 
            } else { 
                iframe.onload = function(){ 
                    alert("Local iframe is now loaded."); 
                }; 
            }*/
        }
    
    
    </script>
</body>
</html>
