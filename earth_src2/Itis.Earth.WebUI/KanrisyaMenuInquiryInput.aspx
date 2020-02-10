<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="KanrisyaMenuInquiryInput.aspx.vb" Inherits="Itis.Earth.WebUI.KanrisyaMenuInquiryInput" 
    title="ユーザー管理 照会・登録" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定   

</script>
<script type="text/javascript">

 

 function clearCheck(){

 

 if (emptyCheck() == true){
    fncUserSearch();
    return false;
}
 else

    {    

        var goon = confirm("<%= Itis.Earth.BizLogic.Messages.Instance.MSG2000C %>");

            if (goon==true){
            
            fncUserSearch();
            }
        return false;
        
        
        }

 }

 

 function emptyCheck(){

        if (isEmpty(objEBI("<%= lblLogonId.clientID %>").innerText) == false)

        return false;

   return true;

 }

 

 function isEmpty(itemValue)

 { 

 var temp = itemValue.replace(/ /g,"");

 temp = temp.replace(/　/g,"");

 if(temp == "") return true;

 else return false;

 }

 

</script>

<div id="buySelName" runat="server" class="modalDiv" style="position:absolute;left:300px; top:140px;z-index:2;display:none;">
</div>
<div id="disableDiv" runat="server" style="position:absolute; left:0px; top:0px; width:1002px; height:616px; z-index:100;  FILTER:alpha(opacity=70);background-color:#000000; display:none;">
<iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
</div> 

<table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
    <tbody>
        <tr>
            <th>
                ユーザー管理 照会・登録
            </th>
            <th style="text-align: right;">
            </th>
        </tr>
        <tr>
            <td colspan="2" rowspan="1">
            </td>
        </tr>
    </tbody>
</table>
<br />
<table style ="border-collapse :collapse ; font-size :16px;"  >
<tr>
    <td style ="width :100px;">ユーザーＩＤ</td>
    <td style ="width :90px;">
        <asp:TextBox ID="tbxUserCd" runat="server" MaxLength ="30" Width ="80px" Text="" CssClass="codeNumber"></asp:TextBox></td>
    <td style ="width :100px;">
        <asp:Button ID="btnSearch" runat="server" Text="検索" /></td>
</tr>
</table>

<table border="1" cellpadding="1" cellspacing="0" class="mainTable paddinNarrow" style="margin-top: 10px; width: 700px; text-align: left;  border-style:solid; border-color:Gray ;">
    <tr style ="height :23px">
        <td style="width: 130px;" class="koumokuMei">Logon_ID</td>
        <td style="width: 190px">
            <asp:Label ID="lblLogonId" runat="server" Text="&nbsp;"></asp:Label></td>
        <td style="width: 130px;" class="koumokuMei">新人区分</td>
        <td style="width: 190px">
            <asp:DropDownList ID="ddlSinjinKubun" runat="server" Width ="140px">
                <asp:ListItem Text="" Value ="" ></asp:ListItem>
                <asp:ListItem Text="通常" Value ="0" ></asp:ListItem>
                <asp:ListItem Text="新人" Value ="1" ></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr style ="height :23px">
        <td class="koumokuMei">氏名</td>
        <td ><asp:TextBox ID="tbxSiMei" runat="server" BorderStyle="None" BackColor="#E6E6E6" ReadOnly="True" TabIndex="-1"></asp:TextBox></td>
        <td class="koumokuMei">業務コード</td>
        <td>
            <asp:DropDownList ID="ddlGyoumuCode" runat="server" Width ="140px" >
                <asp:ListItem Text="&#160;" Value ="0" Selected="True" ></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr style ="height :23px">
        <td class="koumokuMei">所属部署</td>
        <td >
            <asp:Label ID="lblSyozokuBusyo" runat="server" Text="&nbsp;"></asp:Label>
            <asp:HiddenField ID="hidBusyoCd" runat="server" />
        </td>
        <td colspan ="2" >
            <asp:Label ID="Label1" runat="server" Text="営業照会権限追加" Font-Bold="true" ></asp:Label>
        </td>
    </tr>
    <tr style ="height :23px">
        <td class="koumokuMei">組織レベル</td>
        <td ><asp:Label ID="lblSosikiLevel" runat="server" Text="&nbsp;"></asp:Label>
            <asp:HiddenField ID="hidLevel" runat="server" />
        </td>
        <td style="width: 100px;" class="koumokuMei">レベル</td>
        <td>
            <asp:DropDownList ID="ddlLevel" runat="server" Width ="140px" AutoPostBack="true" >
                <asp:ListItem  Value ="0" Text ="&nbsp;" ></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr style ="height :23px">
        <td class="koumokuMei">役職</td>
        <td ><asp:Label ID="lblYakusyoku" runat="server" Text="&nbsp;"></asp:Label></td>
        <td class="koumokuMei">部署コード</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:DropDownList ID="ddlBusyo" runat="server" Width ="140px" >
                <asp:ListItem  Value ="0" Text ="&nbsp;" ></asp:ListItem>
            </asp:DropDownList>
                </ContentTemplate>
             <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlLevel" EventName="SelectedIndexChanged" />
                    </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr style ="height :23px">
        <td class="koumokuMei">参照権限管理</td>
        <td colspan ="3"><asp:Label ID="lblSansyouKengen" runat="server" Text="&nbsp;"></asp:Label></td>
    </tr>
    
</table>
<br />
<div style="font-weight: bolder;">地盤業務権限</div>
<table style="width: 700px; text-align :center; border-width:1px; border-color:Gray;" cellpadding="0" cellspacing="0" border="1" >
    <thead class = "gridviewTableHeader">
      <tr style ="height :23px;background-color: #ffffcc;">
       <td>依頼業務</td>
        <td>新規入力</td>
        <td>ﾃﾞｰﾀ破棄</td>
        <td>結果業務</td>
        <td>保証業務</td>
        <td>報告書業務</td>
        <td>工事業務</td>
        <td>経理業務</td>
        <td>販促売上</td>
        <td>発注書管理</td>
        </tr>
    </thead>
    <tbody>
    <tr style ="height :23px;">
        <td style="background-color:#e6e6e6;">
            <asp:DropDownList ID="ddlIraiGyoumu" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblIraiGyoumu" runat="server" Text="&nbsp;"></asp:Label>--%>
        </td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlSinkiNyuryoku" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblSinkiNyuryoku" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlDataHaki" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblDataHaki" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlKekkagyoumu" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblKekkagyoumu" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlHosyouGyoumu" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblHosyouGyoumu" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlHokosyoGyoumu" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblHokosyoGyoumu" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlKouji" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblKouji" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlKeiriGyoumu" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblKeiriGyoumu" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlHansokuUriage" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblHansokuUriage" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlHattyusyoKanri" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblHattyusyoKanri" runat="server" Text="&nbsp;">--%></asp:Label></td>
    </tr>
    </tbody>
</table>
<br />
<div style="font-weight: bolder;">マスタ管理権限</div>
<table style="width: 340px; text-align :center ;border-width:1px; border-color:Gray;" cellpadding="0" cellspacing="0" border="1" >
    <thead class = "gridviewTableHeader">
      <tr style ="height :23px;background-color: #ffffcc;">
       <td>解析ﾏｽﾀ</td>
        <td>営業ﾏｽﾀ</td>
        <td>価格ﾏｽﾀ</td>
        <td>調査管理</td>
        <td>検査業務</td>
        <td>汎用①</td>
        <td>汎用②</td>
        <td>汎用③</td>
        <td style ="color:Red ;">ｼｽﾃﾑ管理者</td>
      </tr>
    </thead>
    <tbody>
    <tr style ="height :23px;">
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlKaisekiMaster" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblKaisekiMaster" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlEigyouMaster" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblEigyouMaster" runat="server" Text="&nbsp;"></asp:Label>--%></td>
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlKakakuMaster" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblKakakuMaster" runat="server" Text="&nbsp;"></asp:Label>--%></td>
            
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddltyousaka_kanrisya_kengen" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
        </td>            
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlkensa_gyoumu_kengen" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
        </td>    
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlhanyou1_gyoumu_kengen" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
        </td>    
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlhanyou2_gyoumu_kengen" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
        </td>    
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlhanyou3_gyoumu_kengen" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
        </td>          
            
        <td style="background-color:#e6e6e6;">
                    <asp:DropDownList ID="ddlSystemKanrisya" runat="server" Font-Size="16px" Width="40" Font-Names="ＭＳ Ｐゴシック">
                <asp:ListItem Value="0" Text=""></asp:ListItem>
                <asp:ListItem Value="-1">○</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:Label ID="lblSystemKanrisya" runat="server" Text="&nbsp;"></asp:Label>--%></td>
    </tr>
    </tbody>
</table>

<table style="width: 260px; margin-top :30px;" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td style="height: 25px;border-left:0px white;border-right:0px white;width: 85px;">
            <asp:Button runat="server" ID="btnBack" Text="戻る" Width="85px" />
        </td>
        <td style="height: 25px;border-right:0px white;width: 85px; ">
           <asp:Button runat="server" ID="btnTouroku" Text="登録" Width="85px" />
       </td>
    </tr>
</table>

<asp:HiddenField ID="hidHaita" runat="server"  Value ="" />  
<asp:HiddenField ID="hidbtnKbn" runat="server"  Value ="" />

</asp:Content>
