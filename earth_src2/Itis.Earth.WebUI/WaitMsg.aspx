<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WaitMsg.aspx.vb" Inherits="Itis.Earth.WebUI.WaitMsg" %>


<html xmlns="http://www.w3.org/1999/xhtml">

<head><title>お待ちください</title>      
</head>

<body bgcolor="LightBlue">

<div id="demo" style="overflow:hidden;height:220px; line-height:44px;width:440px; border:0px solid #666; white-space:nowrap;">

	<div id="demo1" style="font-color:SteelBlue;" >  
	        <font color="SteelBlue" size="6">
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            ただいまダウンロード中です。しばらくお待ちください。
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	        </font>
	</div>

	<div id="demo2"></div>
    <iframe height="0" width="0" src="<%= htmlQuery %>">
    </iframe>
</div>

<script type="text/javascript"> 

var speed=10;

var demo2=document.getElementById("demo2");

var demo1=document.getElementById("demo1");

var demo=document.getElementById("demo");

function MarqueeLeft()

{ 
	if(demo.scrollLeft>1000) {
		demo.scrollLeft=0;
	} else{
 		demo.scrollLeft++ ;
 	} 
}
var MyMar=setInterval(MarqueeLeft,speed); 
//demo.onmouseover=function() {clearInterval(MyMar);} 
//demo.onmouseout=function() {MyMar=setInterval(MarqueeLeft,speed);} 

</script>  

  </body> </html>