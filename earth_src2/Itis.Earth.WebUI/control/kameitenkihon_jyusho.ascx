<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kameitenkihon_jyusho.ascx.vb" Inherits="Itis.Earth.WebUI.kameitenkihon_jyusho_user" %>
<%@ Register Src="common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript">
// <!CDATA[


function chkNengetu(obj){
if (obj.value==""){return true;}


    var checkFlg = true;
    
    

 
    obj.value = obj.value.Trim();
    var val = obj.value;
    
    val = SetDateNoSign(val,"/");
    val = SetDateNoSign(val,"-");
        
    val = val+'01';

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
    obj.value = val.substring(0,7);
  }

}


function chkN(value){
  //数値以外の場合、アラートを表示
  if(isNaN(value)){
    return false;
  }
  return true;
}


// ]]>
</script>


    

<table id="Table4" cellpadding="1" class="mainTable" style="margin-top: 10px; width: 916px;
    text-align: left" readonly="readonly">
    <thead>
        <tr>
            <th class="tableTitle" colspan="10" rowspan="1" style="text-align: left">
                <a id="titleText5" runat="server">住所情報</a>&nbsp; &nbsp;<span id="titleInfobar5" runat="server"
                    style="display: none"></span>
                <asp:Button ID="btnTouroku" runat="server" Text="登録"   />
            </th>
        </tr>
    </thead>

    <tbody id="meisaiTbody5" runat="server" >

                                        <tr>
                                            <td class="koumokuMei" style="width: 120px">
                                            登録年月</td>
                                            <td colspan="3" style="width: 340px">
                                                <asp:TextBox ID="tbxAddNengetu" runat="server" Style="width: 72px" CssClass = "codeNumber" MaxLength="7"></asp:TextBox></td>
                                            <td colspan="4" style="width: 486px">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="koumokuMei" style="width: 120px" >
                                                郵便番号</td>
                                            <td colspan="3" style="width: 340px">
                                                <asp:TextBox ID="tbxYuubinNo1" runat="server" Style="width: 72px" CssClass = "codeNumber"></asp:TextBox>
                                                <asp:Button ID="btnKensaku" runat="server" Text="検索" OnClientClick = "" /></td>
                                            <td   class="hissu" style="width: 120px; font-weight:bold;">
                                                住所１</td>
                                            <td class="hissu" colspan="3" style="width: 605px;">
                                                <asp:TextBox ID="tbxJyuusyo1" runat="server" Style="width: 296px" CssClass = "input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="koumokuMei" style="width: 120px"  >
                                                所在地ｺｰﾄﾞ</td>
                                            <td colspan="3" style="width: 340px;">
                                                <uc1:common_drop ID="ddlTodoufuken" runat="server" GetStyle="todoufuken" />
                                            </td>
                                            <td style="width: 120px" class="koumokuMei" >
                                                住所２</td>
                                            <td colspan="3" >
                                                <asp:TextBox ID="tbxJyuusyo2" runat="server" Style="width: 296px"  CssClass = "input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="koumokuMei" style="width: 120px"  >
                                                部署名</td>
                                            <td colspan="3" style="width: 340px;">
                                                <asp:TextBox ID="tbxBusyoMei1" runat="server" Style="width: 296px"  CssClass = "input"></asp:TextBox></td>
                                            <td  class="koumokuMei" style="width: 120px" >
                                                代表者名</td>
                                            <td colspan="3" style="width: 360px; ">
                                                <asp:TextBox ID="tbxDaihyousyaMei1" runat="server" Style="width: 144px"  CssClass = "input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="WIDTH: 120px" class="koumokuMei" >
                                                電話番号</td>
                                            <td colspan="3" style="width: 340px">
                                                <asp:TextBox ID="tbxTelNo1" runat="server" Style="width: 145px" CssClass = "codeNumber"></asp:TextBox></td>
                                            <td style="width: 120px" class="koumokuMei" >
                                                ＦＡⅩ番号</td>
                                            <td >
                                                <asp:TextBox ID="tbxFaxNo1" runat="server" Style="width: 100px" CssClass = "codeNumber"></asp:TextBox></td>
                                            <td style="width: 120px" class="koumokuMei" >
                                                申込担当者</td>
                                            <td>
                                                <asp:TextBox ID="tbxMailAddress" runat="server" Style="width: 144px" CssClass = "input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="WIDTH: 120px" class="koumokuMei" >
                                                備考１</td>
                                            <td colspan="3" style="width: 340px">
                                                <asp:TextBox ID="tbxBikou11" runat="server" Style="width: 200px" CssClass = "input"></asp:TextBox></td>
                                            <td style="WIDTH: 120px" class="koumokuMei" >
                                                備考２</td>
                                            <td  colspan="3" style="width: 355px">
                                                <asp:TextBox ID="tbxBikou21" runat="server" Style="width: 280px" CssClass = "input"></asp:TextBox></td>
                                        </tr>
           
    </tbody>

</table>
    <asp:HiddenField ID="hidUpdTime" runat="server" />

