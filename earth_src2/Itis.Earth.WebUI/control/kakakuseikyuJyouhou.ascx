<%@ Control Language="vb" AutoEventWireup="false" Codebehind="kakakuseikyuJyouhou.ascx.vb"
    Inherits="Itis.Earth.WebUI.kakakuseikyuJyouhou" %>
<link rel="stylesheet" href="../css/jhsearth.css" type="text/css" />

<script type="text/javascript">

 function clearCheck(){
 
 if (emptyCheck() == true)
    return true;
 else
    {    
    var goon = confirm("<%= Itis.Earth.BizLogic.Messages.Instance.MSG2000C %>");
    return goon;
    }
 }

 function emptyCheck(){
        if (isEmpty(objEBI("<%= tbxKaiyaku.clientID %>").value) == false)
        return false;
        if (isEmpty(objEBI("<%= tbxSyouhinCd1.clientID %>").value) == false)
        return false;
        if (isEmpty(objEBI("<%= tbxSyouhinCd2.clientID %>").value) == false)
        return false;
        if (isEmpty(objEBI("<%= tbxSyouhinCd3.clientID %>").value) == false)
        return false;

   return true;
 }
 
  function KameitenEmptyCheck()
  {
        if (isEmpty(objEBI("<%= tbxKameitenCd.clientID %>").value) == false)
        return false;

        return true;
  }
 
 
   function CopyItemCheck()
  {
  
         if ( KameitenEmptyCheck() )
         {
         var errorMsg = "<%= Itis.Earth.BizLogic.Messages.Instance.MSG013E %>"; 
         alert(errorMsg.replace("@PARAM1", "加盟店コード"));
         objEBI("<%= tbxKameitenCd.clientID %>").focus();
         return false;
         }
         else
         {
         return clearCheck();
         }
  
  }

 function isEmpty(itemValue)
 { 
 var temp = itemValue.replace(/ /g,"");
 temp = temp.replace(/　/g,"");
 if(temp == "") 
 return true;
 else return false;
 }
 
 function fncShowConfirm(){
    if(confirm("請求先欄をクリアしますか？")){
        //調査
        objEBI("<%= ddlSeikyusakiKbn_Tys.clientID %>").selectedIndex = 0;
        objEBI("<%= tbxSeikyusakiCode_Tys.clientID %>").value = "";
        objEBI("<%= tbxSeikyusakiEdaban_Tys.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_name_Tys.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_simebi_Tys.clientID %>").value = "";
        //工事
        objEBI("<%= ddlSeikyusakiKbn_Kouj.clientID %>").selectedIndex = 0;
        objEBI("<%= tbxSeikyusakiCode_Kouj.clientID %>").value = "";
        objEBI("<%= tbxSeikyusakiEdaban_Kouj.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_name_Kouj.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_simebi_Kouj.clientID %>").value = "";
        //販促品
        objEBI("<%= ddlSeikyusakiKbn_Hansoku.clientID %>").selectedIndex = 0;
        objEBI("<%= tbxSeikyusakiCode_Hansoku.clientID %>").value = "";
        objEBI("<%= tbxSeikyusakiEdaban_Hansoku.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_name_Hansoku.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_simebi_Hansoku.clientID %>").value = "";
        //設計確認
        objEBI("<%= ddlSeikyusakiKbn_Tatemono.clientID %>").selectedIndex = 0;
        objEBI("<%= tbxSeikyusakiCode_Tatemono.clientID %>").value = "";
        objEBI("<%= tbxSeikyusakiEdaban_Tatemono.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_name_Tatemono.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_simebi_Tatemono.clientID %>").value = "";
        
        objEBI("<%= ddlSeikyusakiKbn_Sk5.clientID %>").selectedIndex = 0;
        objEBI("<%= tbxSeikyusakiCode_Sk5.clientID %>").value = "";
        objEBI("<%= tbxSeikyusakiEdaban_Sk5.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_name_Sk5.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_simebi_Sk5.clientID %>").value = "";
        
        objEBI("<%= ddlSeikyusakiKbn_Sk6.clientID %>").selectedIndex = 0;
        objEBI("<%= tbxSeikyusakiCode_Sk6.clientID %>").value = "";
        objEBI("<%= tbxSeikyusakiEdaban_Sk6.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_name_Sk6.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_simebi_Sk6.clientID %>").value = "";
        
        objEBI("<%= ddlSeikyusakiKbn_Sk7.clientID %>").selectedIndex = 0;
        objEBI("<%= tbxSeikyusakiCode_Sk7.clientID %>").value = "";
        objEBI("<%= tbxSeikyusakiEdaban_Sk7.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_name_Sk7.clientID %>").value = "";
        objEBI("<%= lblSeikyusaki_simebi_Sk7.clientID %>").value = "";

        objEBI("<%= btnKihonSet.clientID %>").disabled = false;
    }else{
        
    }
 }
 
</script>

<table id="TABLE1" cellpadding="1" class="mainTable" style="margin-top: 10px; width: 968px;
    text-align: left">
    <tr>
        <th class="tableTitle" colspan="12" rowspan="1" style="height: 27px">
            <a id="titleText_kakaku" runat="server">価格・請求情報</a> &nbsp; <span id="titleInfobarKakaku"
                runat="server" style="display: none">
                <asp:Button ID="btnTouroku" runat="server" Text="登録" />
                &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnCopy" runat="server" Text="情報コピー" OnClientClick="return CopyItemCheck()" />
                &nbsp; &nbsp;
                <asp:Label ID="lblkameiten" runat="server" Style="font-size: 13px; height: 20px;
                    font-weight: normal; text-align: center;">加盟店コード</asp:Label>
                <asp:TextBox ID="tbxKameitenCd" MaxLength="5" Width="80px" runat="server" Style="ime-mode: disabled;"></asp:TextBox>
            </span>
        </th>
    </tr>
    <!--基本情報明細-->
    <tr id="meisaiTbody_kakaku" runat="server" style="display: none">
        <td>
            <table id="Table2" cellpadding="1" style="width: 940px; border: none; margin-top: -5px;
                margin-left: -5px;">
                <tr style="border: none;">
                    <td style="width: 929px; border: none;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline "
                            ChildrenAsTriggers="False">
                            <ContentTemplate>
                                <table class="titleTable" border="0" style="margin-bottom: -10px; width: 880px; vertical-align: top;"
                                    cellpadding="0" cellspacing="0">
                                    <tr style="border: none;">
                                        <td style="border: none; vertical-align: top; height: 58px;">
                                            <strong>価格情報</strong>
                                            <table style="border-collapse: collapse; border: 0px solid gray;">
                                                <tr>
                                                    <td class="koumokuMei" style="width: 96px">
                                                        解約払戻金</td>
                                                    <td style="width: 110px;" >
                                                        <asp:TextBox ID="tbxKaiyaku" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"></asp:TextBox></td>
                                                    <td class="koumokuMei" style="width: 116px">
                                                        SDS以外 後付
                                                        <br />解析品質保証料</td>
                                                    <td style="width: 110px;" >
                                                        <asp:TextBox ID="tbxKaisekiHosyouKkk" runat="server"  MaxLength="11" Width="96px" style="text-align:right;" CssClass="kingaku"></asp:TextBox></td>
                                                    <td class="koumokuMei" style="width: 116px">
                                                        SDS 後付
                                                        <br />解析品質保証料</td>
                                                    <td style="width: 110px;" >
                                                        <asp:TextBox ID="tbxSsgrKkk" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"  style="text-align:right;"></asp:TextBox></td>
                                                    <td style="text-align: center; width: 97px; border: none;">
                                                        <asp:Button ID="btnKakaku" runat="server" Text="価格設定" OnClientClick="fncOpenHanbai();return false;" /></td>
                                                    <td style="border: none;">
                                                        <asp:Button ID="btnTokubetuTaiou" runat="server" Text="特別対応設定" OnClientClick="fncOpenTokubetu();return false;" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="koumokuMei" style="width: 96px">液状化特約費
                        
                                                    </td>
                                                    <td style="width: 110px">
                                                     <asp:TextBox ID="tbx_ekijyouka_tokuyaku_kakaku" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"  style="text-align:right;"></asp:TextBox>
                                                    </td>
                                                    <td class="koumokuMei" style="width: 116px">
                                                    液状化商品
                                                    <br />SDS価格＋
                                                    </td>
                                                    <td style="width: 110px">
                                                     <asp:TextBox ID="tbx_ekijyouka_kanihantei_kakaku" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"  style="text-align:right;"></asp:TextBox>
                                                    </td>
                                                    <td class="koumokuMei" style="width: 116px">
                                                    オンライン割引
                                                    </td>
                                                    <td style="width: 110px">
                                                        <asp:DropDownList ID="ddlOnline" runat="server">
                                                            <asp:ListItem Value="1">有り</asp:ListItem>
                                                            <asp:ListItem Value="">無し</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    </td>
                                                    <td style="border-right: medium none; border-top: medium none; border-left: medium none;
                                                        width: 97px; border-bottom: medium none; text-align: center">
                                                    </td>
                                                    <td style="border-right: medium none; border-top: medium none; border-left: medium none;
                                                        border-bottom: medium none">
                                                    </td>
                                                </tr>
                                                <%--                                    <tr>
                                        <td class="koumokuMei" style="width: 86px">
                                            SS</td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="tbxSSkk" runat="server" CssClass="kingaku" MaxLength="11" Width="96px" ></asp:TextBox>
                                        </td>
                                        <td  class="koumokuMei"  style="width: 86px">
                                            再調査</td>
                                        <td colspan="1" style="width: 120px">
                                        <asp:TextBox ID="tbxSaitys" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"></asp:TextBox></td>
                                   
                                    </tr>
                                    <tr>
                                        <td  class="koumokuMei" style="width: 86px">
                                            SSGR</td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="tbxSSGRkk" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"></asp:TextBox></td>
                                    <td  class="koumokuMei"  style="width: 86px">
                                            保証無</td>
                                        <td colspan="1" style="width: 120px">
                                            <asp:TextBox ID="tbxHousyomu" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei"  style="width: 86px">
                                            解析保証</td>
                                        <td  style="width: 120px">
                                            <asp:TextBox ID="tbxKaiseki" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"></asp:TextBox></td>
                                        <td  class="koumokuMei"  style="width: 86px; height: 20px;">
                                            事前調査</td>
                                        <td colspan="1" style="width: 120px;">
                                            <asp:TextBox ID="tbxJizentys" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td  class="koumokuMei"  style="width: 86px">
                                            解約払戻金</td>
                                        <td  style="width: 120px;" colspan = "3">
                                            <asp:TextBox ID="tbxKaiyaku" runat="server" CssClass="kingaku" MaxLength="11" Width="96px"></asp:TextBox></td>
                                    </tr>
                            </table>

          </td>
          <td style="border :none;vertical-align :top; margin-left :15px; height: 158px;"> 
         
          <strong>請求情報</strong> 
           <table class="tinyTable" style ="border-collapse: collapse; border: 0px solid gray;">
                                <tr>
                                        <td  class="koumokuMei"  style="width: 138px">
                                            調査</td>
                                        <td  class="koumokuMei"  style="width: 120px; border-right-style: dotted;">
                                            請求先</td>
                                        <td style="width: 84px ;border-left-style: none;">
                                            <asp:TextBox ID="tbxTysSekyuSaki" runat="server" MaxLength="5" Width="72px" style="ime-mode:disabled;" ></asp:TextBox></td>
                                        <td  class="koumokuMei"  style="width: 120px; border-right-style: dotted;">
                                            締め日</td>
                                        <td style="width: 84px ;border-left-style: none;">
                                            <asp:TextBox ID="tbxTysSekyuDate" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                        <td  class="koumokuMei"  style="width: 138px">
                                           工事</td>
                                        <td  class="koumokuMei"  style="width: 120px; border-right-style: dotted;">
                                            請求先</td>
                                        <td style="width: 84px ;border-left-style: none;">
                                            <asp:TextBox ID="tbxKoujSekyuSaki" runat="server" MaxLength="5" Width="72px" style="ime-mode:disabled;" ></asp:TextBox></td>
                                        <td  class="koumokuMei"  style="width: 120px; border-right-style: dotted;">
                                            締め日</td>
                                        <td style="width: 84px ;border-left-style: none;">
                                            <asp:TextBox ID="tbxKoujSekyuDate" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                        <td  class="koumokuMei"  style="width: 138px;"> 
                                           販促品</td>
                                        <td class="koumokuMei"  style="width: 120px; border-right-style: dotted;">
                                            請求先</td>
                                        <td style="width: 84px ;border-left-style: none;">
                                            <asp:TextBox ID="tbxHansokuSekyuSaki" runat="server" MaxLength="5" Width="72px" style="ime-mode:disabled;" ></asp:TextBox></td>
                                        <td class="koumokuMei"  style="width: 120px; border-right-style: dotted;">
                                            締め日</td>
                                        <td style="width: 84px ;border-left-style: none;">
                                            <asp:TextBox ID="tbxHansokuSekyuDate" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                </tr>

                            </table>
   
          </td>
          </tr>
--%>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                &nbsp<strong>多棟割引情報</strong>
                                <table style="margin-left: 4px; width: 884px; border-collapse: collapse; border: 0px solid gray;">
                                    <thead>
                                        <tr class="shouhinTableTitle">
                                            <td style="height: 21px">
                                                棟区分</td>
                                            <td style="width: 105px; height: 21px;">
                                                棟数</td>
                                            <td style="width: 136px; height: 21px;">
                                                商品コード</td>
                                            <td style="width: 395px; height: 21px;">
                                                商品名</td>
                                            <td style="width: 175px; height: 21px;">
                                                1棟あたり割引金額</td>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td class="koumokuMei" style="width: 48px;">
                                            1</td>
                                        <td class="koumokuMei" style="width: 105px;">
                                            4棟～9棟</td>
                                        <td style="width: 136px;">
                                            <asp:TextBox ID="tbxSyouhinCd1" runat="server" MaxLength="8" Style="display: inline;
                                                ime-mode: disabled;" Width="72px"></asp:TextBox>
                                            <asp:Button ID="btnKensaku1" runat="server" Text="検索" CssClass="gyoumuSearchBtn" /></td>
                                        <td style="width: 395px;">
                                            <asp:TextBox ID="tbxSyouhinNm1" runat="server" Width="384px" BorderStyle="none" BackColor="#E6E6E6"
                                                TabIndex="-1"></asp:TextBox>
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:TextBox ID="tbxMoney1" runat="server" Width="165px" BorderStyle="none" BackColor="#E6E6E6"
                                                TabIndex="-1" Style="text-align: center;" Text="money1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei" style="width: 48px;">
                                            2</td>
                                        <td class="koumokuMei" style="width: 105px;">
                                            10棟～19棟</td>
                                        <td style="width: 136px;">
                                            <asp:TextBox ID="tbxSyouhinCd2" runat="server" MaxLength="8" Width="72px" Style="display: inline;
                                                ime-mode: disabled;"></asp:TextBox>
                                            <asp:Button ID="btnKensaku2" runat="server" Text="検索" CssClass="gyoumuSearchBtn" /></td>
                                        <td style="width: 395px;">
                                            <asp:TextBox ID="tbxSyouhinNm2" runat="server" Width="384px" BorderStyle="none" BackColor="#E6E6E6"
                                                TabIndex="-1"></asp:TextBox>
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:TextBox ID="tbxMoney2" runat="server" Width="165px" BorderStyle="none" BackColor="#E6E6E6"
                                                TabIndex="-1" Style="text-align: center;" Text="money2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei" style="width: 48px;">
                                            3</td>
                                        <td class="koumokuMei" style="width: 105px;">
                                            20棟以上</td>
                                        <td style="width: 136px;">
                                            <asp:TextBox ID="tbxSyouhinCd3" runat="server" MaxLength="8" Width="72px" Style="display: inline;
                                                ime-mode: disabled;"></asp:TextBox>
                                            <asp:Button ID="btnKensaku3" runat="server" Text="検索" CssClass="gyoumuSearchBtn" /></td>
                                        <td style="width: 395px;">
                                            <asp:TextBox ID="tbxSyouhinNm3" runat="server" Width="384px" BorderStyle="none" BackColor="#E6E6E6"
                                                TabIndex="-1"></asp:TextBox>
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:TextBox ID="tbxMoney3" runat="server" Width="165px" BorderStyle="none" BackColor="#E6E6E6"
                                                TabIndex="-1" Style="text-align: center;" Text="money3"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                &nbsp<strong>請求先&nbsp; </strong>&nbsp;
                                <asp:Button ID="btnKihonSet" runat="server" Text="基本セット" CssClass="gyoumuSearchBtn"
                                    Style="margin-bottom: 2px;" Width="80px" OnClick="btnKihonSet_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="クリア" CssClass="gyoumuSearchBtn" Style="margin-bottom: 2px;"
                                    Width="80px" OnClientClick="fncShowConfirm();return false;" />
                                <table style="margin-left: 4px; border-collapse: collapse; border: 0px solid gray;
                                    width: 816px;">
                                    <thead>
                                    </thead>
                                    <tr>
                                        <td class="koumokuMei" style="width: 60px;">
                                            <asp:TextBox ID="tbxSeikyuusakiNaiyou1" runat="server" Text="" TabIndex="-1" Style="width: 55px;
                                                border: none; background-color: transparent; font-weight: bold;"></asp:TextBox>
                                        </td>
                                        <td style="" colspan="2">
                                            <asp:DropDownList ID="ddlSeikyusakiKbn_Tys" runat="server" Style="width: 80px;">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="tbxSeikyusakiCode_Tys" runat="server" MaxLength="5" Style="display: inline;
                                                ime-mode: disabled;" Width="40px"></asp:TextBox>
                                            -
                                            <asp:TextBox ID="tbxSeikyusakiEdaban_Tys" runat="server" MaxLength="2" Style="display: inline;
                                                ime-mode: disabled" Width="24px"></asp:TextBox>
                                            <asp:Button ID="btnSeikyusaki_Sel_Tys" runat="server" Text="検索" CssClass="gyoumuSearchBtn" />
                                            <asp:Button ID="btnSeikyusaki_Syousai_Tys" runat="server" Text="詳細" CssClass="gyoumuSearchBtn" />
                                            &nbsp;
                                            <asp:TextBox ID="lblSeikyusaki_name_Tys" runat="server" BackColor="#E6E6E6" BorderStyle="none"
                                                TabIndex="-1" Width="312px" Font-Bold="True"></asp:TextBox>
                                            <asp:TextBox ID="lblSeikyusaki_simebi_Tys" runat="server" BackColor="#E6E6E6" BorderStyle="none"
                                                TabIndex="-1" Width="96px" Font-Bold="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei" style="width: 60px;">
                                            <asp:TextBox ID="tbxSeikyuusakiNaiyou2" runat="server" Text="" TabIndex="-1" Style="width: 55px;
                                                border: none; background-color: transparent; font-weight: bold;"></asp:TextBox>
                                        </td>
                                        <td style="" colspan="2">
                                            <asp:DropDownList ID="ddlSeikyusakiKbn_Kouj" runat="server" Style="width: 80px;">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="tbxSeikyusakiCode_Kouj" runat="server" MaxLength="5" Style="display: inline;
                                                ime-mode: disabled" Width="40px"></asp:TextBox>
                                            -
                                            <asp:TextBox ID="tbxSeikyusakiEdaban_Kouj" runat="server" MaxLength="2" Style="display: inline;
                                                ime-mode: disabled" Width="24px"></asp:TextBox>
                                            <asp:Button ID="btnSeikyusaki_Sel_Kouj" runat="server" Text="検索" CssClass="gyoumuSearchBtn" />
                                            <asp:Button ID="btnSeikyusaki_Syousai_Kouj" runat="server" Text="詳細" CssClass="gyoumuSearchBtn" />
                                            &nbsp;
                                            <asp:TextBox ID="lblSeikyusaki_name_Kouj" runat="server" BackColor="#E6E6E6" BorderStyle="none"
                                                TabIndex="-1" Width="312px" Font-Bold="True"></asp:TextBox>
                                            <asp:TextBox ID="lblSeikyusaki_simebi_Kouj" runat="server" BackColor="#E6E6E6" BorderStyle="none"
                                                TabIndex="-1" Width="96px" Font-Bold="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei" style="width: 60px;">
                                            <asp:TextBox ID="tbxSeikyuusakiNaiyou3" runat="server" Text="" TabIndex="-1" Style="width: 55px;
                                                border: none; background-color: transparent; font-weight: bold;"></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlSeikyusakiKbn_Hansoku" runat="server" Style="width: 80px;">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="tbxSeikyusakiCode_Hansoku" runat="server" MaxLength="5" Style="display: inline;
                                                ime-mode: disabled" Width="40px"></asp:TextBox>
                                            -
                                            <asp:TextBox ID="tbxSeikyusakiEdaban_Hansoku" runat="server" MaxLength="2" Style="display: inline;
                                                ime-mode: disabled" Width="24px"></asp:TextBox>
                                            <asp:Button ID="btnSeikyusaki_Sel_Hansoku" runat="server" Text="検索" CssClass="gyoumuSearchBtn" />
                                            <asp:Button ID="btnSeikyusaki_Syousai_Hansoku" runat="server" Text="詳細" CssClass="gyoumuSearchBtn" />
                                            &nbsp;
                                            <asp:TextBox ID="lblSeikyusaki_name_Hansoku" runat="server" BackColor="#E6E6E6" BorderStyle="none"
                                                TabIndex="-1" Width="312px" Font-Bold="True"></asp:TextBox>
                                            <asp:TextBox ID="lblSeikyusaki_simebi_Hansoku" runat="server" BackColor="#E6E6E6"
                                                BorderStyle="none" TabIndex="-1" Width="96px" Font-Bold="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei" style="width: 60px; height: 22px;">
                                            <asp:TextBox ID="tbxSeikyuusakiNaiyou4" runat="server" Text="" TabIndex="-1" Style="width: 55px;
                                                border: none; background-color: transparent; font-weight: bold;"></asp:TextBox>
                                        </td>
                                        <td style="height: 22px;" colspan="2">
                                            <asp:DropDownList ID="ddlSeikyusakiKbn_Tatemono" runat="server" Style="width: 80px;">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="tbxSeikyusakiCode_Tatemono" runat="server" MaxLength="5" Style="display: inline;
                                                ime-mode: disabled" Width="40px"></asp:TextBox>
                                            -
                                            <asp:TextBox ID="tbxSeikyusakiEdaban_Tatemono" runat="server" MaxLength="2" Style="display: inline;
                                                ime-mode: disabled" Width="24px"></asp:TextBox>
                                            <asp:Button ID="btnSeikyusaki_Sel_Tatemono" runat="server" Text="検索" CssClass="gyoumuSearchBtn" />
                                            <asp:Button ID="btnSeikyusaki_Syousai_Tatemono" runat="server" Text="詳細" CssClass="gyoumuSearchBtn" />&nbsp;
                                            &nbsp;<asp:TextBox ID="lblSeikyusaki_name_Tatemono" runat="server" BackColor="#E6E6E6"
                                                BorderStyle="none" TabIndex="-1" Width="312px" Font-Bold="True"></asp:TextBox>
                                            <asp:TextBox ID="lblSeikyusaki_simebi_Tatemono" runat="server" BackColor="#E6E6E6"
                                                BorderStyle="none" TabIndex="-1" Width="96px" Font-Bold="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei" style="width: 60px; height: 22px;">
                                            <asp:TextBox ID="tbxSeikyuusakiNaiyou5" runat="server" Text="" TabIndex="-1" Style="width: 55px;
                                                border: none; background-color: transparent; font-weight: bold;"></asp:TextBox>
                                        </td>
                                        <td style="height: 22px;" colspan="2">
                                            <asp:DropDownList ID="ddlSeikyusakiKbn_Sk5" runat="server" Style="width: 80px;">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="tbxSeikyusakiCode_Sk5" runat="server" MaxLength="5" Style="display: inline;
                                                ime-mode: disabled" Width="40px"></asp:TextBox>
                                            -
                                            <asp:TextBox ID="tbxSeikyusakiEdaban_Sk5" runat="server" MaxLength="2" Style="display: inline;
                                                ime-mode: disabled" Width="24px"></asp:TextBox>
                                            <asp:Button ID="btnSeikyusaki_Sel_Sk5" runat="server" Text="検索" CssClass="gyoumuSearchBtn" />
                                            <asp:Button ID="btnSeikyusaki_Syousai_Sk5" runat="server" Text="詳細" CssClass="gyoumuSearchBtn" />&nbsp;
                                            &nbsp;<asp:TextBox ID="lblSeikyusaki_name_Sk5" runat="server" BackColor="#E6E6E6"
                                                BorderStyle="none" TabIndex="-1" Width="312px" Font-Bold="True"></asp:TextBox>
                                            <asp:TextBox ID="lblSeikyusaki_simebi_Sk5" runat="server" BackColor="#E6E6E6" BorderStyle="none"
                                                TabIndex="-1" Width="96px" Font-Bold="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei" style="width: 60px; height: 22px;">
                                            <asp:TextBox ID="tbxSeikyuusakiNaiyou6" runat="server" Text="" TabIndex="-1" Style="width: 55px;
                                                border: none; background-color: transparent; font-weight: bold;"></asp:TextBox>
                                        </td>
                                        <td style="height: 22px;" colspan="2">
                                            <asp:DropDownList ID="ddlSeikyusakiKbn_Sk6" runat="server" Style="width: 80px;">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="tbxSeikyusakiCode_Sk6" runat="server" MaxLength="5" Style="display: inline;
                                                ime-mode: disabled" Width="40px"></asp:TextBox>
                                            -
                                            <asp:TextBox ID="tbxSeikyusakiEdaban_Sk6" runat="server" MaxLength="2" Style="display: inline;
                                                ime-mode: disabled" Width="24px"></asp:TextBox>
                                            <asp:Button ID="btnSeikyusaki_Sel_Sk6" runat="server" Text="検索" CssClass="gyoumuSearchBtn" />
                                            <asp:Button ID="btnSeikyusaki_Syousai_Sk6" runat="server" Text="詳細" CssClass="gyoumuSearchBtn" />&nbsp;
                                            &nbsp;<asp:TextBox ID="lblSeikyusaki_name_Sk6" runat="server" BackColor="#E6E6E6"
                                                BorderStyle="none" TabIndex="-1" Width="312px" Font-Bold="True"></asp:TextBox>
                                            <asp:TextBox ID="lblSeikyusaki_simebi_Sk6" runat="server" BackColor="#E6E6E6" BorderStyle="none"
                                                TabIndex="-1" Width="96px" Font-Bold="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="koumokuMei" style="width: 60px; height: 22px;">
                                            <asp:TextBox ID="tbxSeikyuusakiNaiyou7" runat="server" Text="" TabIndex="-1" Style="width: 55px;
                                                border: none; background-color: transparent; font-weight: bold;"></asp:TextBox>
                                        </td>
                                        <td style="height: 22px;" colspan="2">
                                            <asp:DropDownList ID="ddlSeikyusakiKbn_Sk7" runat="server" Style="width: 80px;">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="tbxSeikyusakiCode_Sk7" runat="server" MaxLength="5" Style="display: inline;
                                                ime-mode: disabled" Width="40px"></asp:TextBox>
                                            -
                                            <asp:TextBox ID="tbxSeikyusakiEdaban_Sk7" runat="server" MaxLength="2" Style="display: inline;
                                                ime-mode: disabled" Width="24px"></asp:TextBox>
                                            <asp:Button ID="btnSeikyusaki_Sel_Sk7" runat="server" Text="検索" CssClass="gyoumuSearchBtn" />
                                            <asp:Button ID="btnSeikyusaki_Syousai_Sk7" runat="server" Text="詳細" CssClass="gyoumuSearchBtn" />&nbsp;
                                            &nbsp;<asp:TextBox ID="lblSeikyusaki_name_Sk7" runat="server" BackColor="#E6E6E6"
                                                BorderStyle="none" TabIndex="-1" Width="312px" Font-Bold="True"></asp:TextBox>
                                            <asp:TextBox ID="lblSeikyusaki_simebi_Sk7" runat="server" BackColor="#E6E6E6" BorderStyle="none"
                                                TabIndex="-1" Width="96px" Font-Bold="True"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnConfirm" runat="server" Text="Button" Style="display: none;" />
                                <asp:HiddenField ID="hidConfrim" runat="server" />
                                <asp:HiddenField ID="hid_kbn_HAN" runat="server" />
                                <asp:HiddenField ID="hid_cd_HAN" runat="server" />
                                <asp:HiddenField ID="hid_brc_HAN" runat="server" />
                                <asp:HiddenField ID="hid_kakunin_HAN" runat="server" />
                                <asp:HiddenField ID="hid_kbn_KOU" runat="server" />
                                <asp:HiddenField ID="hid_cd_KOU" runat="server" />
                                <asp:HiddenField ID="hid_brc_KOU" runat="server" />
                                <asp:HiddenField ID="hid_kakunin_KOU" runat="server" />
                                <asp:HiddenField ID="hid_kbn_TAT" runat="server" />
                                <asp:HiddenField ID="hid_cd_TAT" runat="server" />
                                <asp:HiddenField ID="hid_brc_TAT" runat="server" />
                                <asp:HiddenField ID="hid_kakunin_TAT" runat="server" />
                                <asp:HiddenField ID="hid_kbn_TYS" runat="server" />
                                <asp:HiddenField ID="hid_cd_TYS" runat="server" />
                                <asp:HiddenField ID="hid_brc_TYS" runat="server" />
                                <asp:HiddenField ID="hid_kakunin_TYS" runat="server" />
                                <asp:HiddenField ID="hid_kbn_SK5" runat="server" />
                                <asp:HiddenField ID="hid_cd_SK5" runat="server" />
                                <asp:HiddenField ID="hid_brc_SK5" runat="server" />
                                <asp:HiddenField ID="hid_kakunin_SK5" runat="server" />
                                <asp:HiddenField ID="hid_kbn_SK6" runat="server" />
                                <asp:HiddenField ID="hid_cd_SK6" runat="server" />
                                <asp:HiddenField ID="hid_brc_SK6" runat="server" />
                                <asp:HiddenField ID="hid_kakunin_SK6" runat="server" />
                                <asp:HiddenField ID="hid_kbn_SK7" runat="server" />
                                <asp:HiddenField ID="hid_cd_SK7" runat="server" />
                                <asp:HiddenField ID="hid_brc_SK7" runat="server" />
                                <asp:HiddenField ID="hid_kakunin_SK7" runat="server" />
                                <asp:HiddenField ID="hidSeisikiMei" runat="server" />
                                <asp:HiddenField ID="hidKyoutuKameitenMei1" runat="server" />
                                <asp:HiddenField ID="hidKyoutuEigyousyoCode" runat="server" />
                                <asp:HiddenField ID="hidKyoutuKubun" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnTouroku" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCopy" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnKihonSet" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Sel_Tys" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Sel_Kouj" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Sel_Hansoku" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Sel_Tatemono" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Syousai_Tys" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Syousai_Kouj" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Syousai_Hansoku" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Syousai_Tatemono" EventName="Click" />
                                
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Sel_Sk5" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Sel_Sk6" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Sel_Sk7" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Syousai_Sk5" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Syousai_Sk6" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSeikyusaki_Syousai_Sk7" EventName="Click" />
                                
                                <asp:AsyncPostBackTrigger ControlID="btnConfirm" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
