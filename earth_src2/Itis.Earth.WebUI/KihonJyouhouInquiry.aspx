<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="KihonJyouhouInquiry.aspx.vb" Inherits="Itis.Earth.WebUI.KihonJyouhouInquiry"  MasterPageFile="~/earthMaster.Master" 
 Title ="加盟店基本情報照会"%>

<%@ Register Src="control/SinnseiKbn_jyouhou.ascx" TagName="SinnseiKbn_jyouhou" TagPrefix="uc10" %>


<%@ Register Src="control/torihikiJyouhou.ascx" TagName="torihikiJyouhou" TagPrefix="uc11" %>


<%@ Register Src="control/kameiten_bikou.ascx" TagName="kameiten_bikou" TagPrefix="uc8" %>
<%@ Register Src="control/kameiten_sonota.ascx" TagName="kameiten_sonota" TagPrefix="uc9" %>

<%@ Register Src="control/kameiten_tourokuryou.ascx" TagName="kameiten_tourokuryou"
    TagPrefix="uc7" %>

<%@ Register Src="control/kihon_jyouhou.ascx" TagName="kihon_jyouhou" TagPrefix="uc6" %>

<%@ Register Src="control/kakakuseikyuJyouhou.ascx" TagName="kakakuseikyuJyouhou"
    TagPrefix="uc5" %>

<%@ Register Src="control/kameitenkihon_jyushoNoPage.ascx" TagName="kameitenkihon_jyushoNoPage"
    TagPrefix="uc4" %>

<%@ Register Src="control/kameitenkihon_jyusho.ascx" TagName="kameitenkihon_jyusho"
    TagPrefix="uc3" %>

<%@ Register Src="control/common_drop.ascx" TagName="common_drop" TagPrefix="uc2" %>

<%@ Register Src="control/kyoutuu_jyouhou.ascx" TagName="kyoutuu_jyouhou" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    
    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        objWin.focus();
        initPage(); //画面初期設定
        
        var arrdrp1 = new Array();
        var arrdrp2 = new Array();
        var arrdrp3 = new Array();
        var arrdrp4 = new Array();
        var arrdrp5 = new Array();
        var arrdrp6 = new Array();
        var arrdrp7 = new Array();
        
        
        function chksyo(e,index){
            
            var obj;
            var drp;
            
            eval("obj = arrdrp"+ index +";");     
 
           
          for (i=0;i<=3;i++){

              if (document.getElementById(obj[i])!=e){
                  if (document.getElementById(obj[i]).selectedIndex ==1){
                    e.selectedIndex=0;
                    alert("送付先住所" + (i + 1) + "で選択済みです");
                    return false;
                  }
              
              }  
          
          }  
           
         return true; 
          
               
        
        }


    
    
var comObj1='';
var comObj2='';
var oldObjFocus=null;
/**
 * 日付チェック
 * @param obj:処理対象オブジェクト
 * @return
 */
function SetDateNoSign(value,sign){

var arr;
arr = value.split(sign);

var i;

for(i=0;i<=arr.length-1;i++){
    if(arr[i].length==1){
        arr[i] = "0" + arr[i];        
    }
}

return arr.join("");

} 

function checkDate(obj){
  var checkFlg = true;
  obj.value = obj.value.Trim();
  var val = obj.value;
 

 val = SetDateNoSign(val,"/");
 val = SetDateNoSign(val,"-");



  

  if(val == "")return;  //空の場合、終了
  
  val = removeSlash(val); //スラッシュ除去
  val = val.replace(/\-/g, "");  //ハイフンを削除

  if(val.length == 6){  //6桁の場合
    if(val.substring(0, 2) > 70){
      val = "19" + val;
    }else{
      val = "20" + val;
    }
  }else if(val.length == 4){  //4桁の場合
    dd = new Date();
    year = dd.getFullYear();
    val = year + val;
  }
  
  if(val.length != 8){
    checkFlg = false; //この時点で8桁で無ければNG
  }else{  //8桁の場合
    val = addSlash(val);  //スラッシュ付与
    var arrD = val.split("/");
    if(checkDateVali(arrD[0],arrD[1],arrD[2]) == false){
      checkFlg = false; //日付妥当性チェックNG
    }
  }
  
  if(!checkFlg){
    event.returnValue = false;
    obj.select();
    alert("日付以外が入力されています。");
    return false;
  }else{
    obj.value = val;
    return true;
  }
}

document.forms[0].onsubmit = function(){
try{
ShowModal();
}catch(e){
//alert('aaaaaa');
}

}

function SetYoubin(e){
    var val;
    var val2;
    val = e.value;
    arr = val.split("-");
    val = arr.join("");
    
    if (val.length>=3){        
        val2 = val.substring(0,3) + "-" + val.substring(3,val.length);
    }else{
        val2 =val;    
    }
    
    e.value = val2;    
}


 function copyCheck(){
    if (!chkJyuushoGamenChange()){
         return confirm("<%= Itis.Earth.BizLogic.Messages.Instance.MSG2031C %>");
    } 
 }

function gfIsNumeric(e,msg) {
var i ;
var value;
var vl;
vl = e.value;
value = e.value;

for (i=0;i<=vl.length-1;i++){
    value = value.replace(",","");
}

 if (!isNaN(value)){

 } else {
alert(msg + "は半角数字で入力して下さい。") ; 
  return false;

  
 }
 return true;
}
 



    </script>
    
<script type="text/javascript" language="javascript" for="document" event="onkeydown"> 
  if(event.keyCode==13 && event.srcElement.type!="button" && event.srcElement.type!="submit" && event.srcElement.type!="reset" && event.srcElement.type!="textarea" && event.srcElement.type!="")
     event.keyCode=9; 
</script> 

<!--##############
    TITLE
    ##############-->
<uc1:kyoutuu_jyouhou ID="Kyoutuu_jyouhou1" runat="server" GetStyle="KihonJyouhou" />
        
  
<div style="overflow:auto ; height:294px; width:985px; margin-top:10px; border-width:0px; border-style:solid;">

  
    <uc10:SinnseiKbn_jyouhou ID="SinnseiKbn_jyouhou1" runat="server" />
    <br />
<!--##############
    共通情報
    ##############-->
    <uc6:kihon_jyouhou id="Kihon_jyouhou1" runat="server">
    </uc6:kihon_jyouhou>

    <uc5:kakakuseikyuJyouhou ID="KakakuseikyuJyouhou1" runat="server" />
    <uc11:torihikiJyouhou id="TorihikiJyouhou1" runat="server">
    </uc11:torihikiJyouhou>
    
    <table id="Table4" cellpadding="1" class="mainTable" 
                        style="margin-top: 10px; width: 968px; text-align: left">
                        <thead>
                            <tr>
                                <th class="tableTitle" colspan="10" rowspan="1" style="text-align: left">
                                    <a id="titleText5" runat="server">住所情報</a>&nbsp; &nbsp;<span id="titleInfobar5" runat="server" ></span>
                                </th>
                            </tr>
                        </thead>
                        <tbody id="meisaiTbody5" runat="server" style="display: none">
                            <tr>
                                <td colspan="8" rowspan="3" >
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  RenderMode="Inline">
                                                <Triggers>
                     <%--                               <asp:AsyncPostBackTrigger ControlID="Kyoutuu_jyouhou1"   />--%>
                                                </Triggers>
                                                <ContentTemplate>
                                                
                                       
                                    <uc3:kameitenkihon_jyusho ID="Kameitenkihon_jyusho1" runat="server" />

                                    <uc4:kameitenkihon_jyushoNoPage id="Kameitenkihon_jyushoNoPage1" runat="server">
                                    </uc4:kameitenkihon_jyushoNoPage>
                                    
                                    <uc4:kameitenkihon_jyushoNoPage id="Kameitenkihon_jyushoNoPage2" runat="server">
                                    </uc4:kameitenkihon_jyushoNoPage>                      

                                    <uc4:kameitenkihon_jyushoNoPage id="Kameitenkihon_jyushoNoPage3" runat="server">
                                    </uc4:kameitenkihon_jyushoNoPage>
                                    
                                    <uc4:kameitenkihon_jyushoNoPage id="Kameitenkihon_jyushoNoPage4" runat="server">
                                    </uc4:kameitenkihon_jyushoNoPage>
                                   
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional"  RenderMode="Inline">

<Triggers>

</Triggers>

<ContentTemplate>
    <uc7:kameiten_tourokuryou id="Kameiten_tourokuryou1" runat="server">
    </uc7:kameiten_tourokuryou>        
</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional"  RenderMode="Inline">
<Triggers>
</Triggers>
<ContentTemplate>
    <uc8:kameiten_bikou ID="Kameiten_bikou1" runat="server" >
    </uc8:kameiten_bikou>
</ContentTemplate>
</asp:UpdatePanel>


<uc9:kameiten_sonota id="Kameiten_sonota1" runat="server">
</uc9:kameiten_sonota>

</div> 

 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"  RenderMode="Inline">
     <ContentTemplate>
        <div id="buySelName" runat="server" class="modalDiv" style="position:absolute;left:300px; top:140px;z-index:2;display:none;">
        </div>
        <div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:100%; height:100%; z-index:100;  FILTER:alpha(opacity=70);background-color:#000000; display:none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
        </div>  
     
     </ContentTemplate>
</asp:UpdatePanel>
     
<asp:HiddenField ID="hidKameitenCd" runat="server" />

<asp:HiddenField ID="hidKousinHi" runat="server" />
<asp:HiddenField ID="hidKousinFlg" runat="server" />

<asp:HiddenField ID="hidKbn" runat="server" />
<asp:HiddenField ID="hidUpdLoginUserId" runat="server" />
<asp:HiddenField ID="hidJyusho" runat="server" />
<asp:HiddenField ID="hidKameitenCopyFlg" runat="server" />

    <asp:UpdatePanel ID="updHiddenpanel" runat="server" UpdateMode="Conditional"  RenderMode="Inline">
         <ContentTemplate>
 <asp:HiddenField ID="hidKeiretuCd" runat="server" />        
         
         </ContentTemplate>
    </asp:UpdatePanel>
   

</asp:Content>