<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="torihikiJyouhou.ascx.vb" Inherits="Itis.Earth.WebUI.torihikiJyouhou" %>
  
  <link rel="stylesheet" href="../css/jhsearth.css" type="text/css" />
  
    <script type="text/javascript">
    
    function addCommn(obj)
    {
    
    var sel = obj.parentNode.previousSibling.childNodes[0];
    var index = sel.selectedIndex;
      if(index != -1)
        {
        var naiyou = obj.parentNode.parentNode.nextSibling.childNodes[0].childNodes[0];
        var txtAdd = sel.options[index].text;
        txtAdd = txtAdd.split(":")[1];
            if (sel.options[index].text != "")
                {
                 naiyou.value += txtAdd;
                } 
        }

    }
    
    function HtsChange(){
    
    if ( objEBI("<%= ddlHattyusyo.clientID %>").selectedIndex == 0 )
    {
    objEBI("<%= ddlHtsTys.clientID %>").disabled = true;
    objEBI("<%= ddlHtsKs.clientID %>").disabled = true;
    objEBI("<%= ddlHtsKj.clientID %>").disabled = true;
    }
    else
    {
    objEBI("<%= ddlHtsTys.clientID %>").disabled = false;
    objEBI("<%= ddlHtsKs.clientID %>").disabled = false;
    objEBI("<%= ddlHtsKj.clientID %>").disabled = false;
    }
    }
  
    </script>



    
    
    <table style="text-align: left; width:968px;margin-top:10px;" class="mainTable" cellpadding="1" id="Table2" >
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1" style="text-align: left">
                    <a id="titleText_torihiki" runat="server">取引情報</a>
                    <span id="titleInfobarTorihiki" style="display: none;" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode ="Conditional" RenderMode="Inline">
                   <ContentTemplate>
                        <asp:Button ID="btnTouroku" runat="server" Text="登録" />
                   </ContentTemplate>
                </asp:UpdatePanel>
                    </span>      
                </th>
                <th class="tableTitle" colspan="1" rowspan="1" style="text-align: left">
                </th>
            </tr>
        </thead>
        <!--基本情報明細-->
        <tbody id="meisaiTbody_torihiki" runat="server" style="display: none;"> 
                            <tr>
                                <td class="koumokuMei" style="width: 75px;">
                                    保証期間</td>
                                <td  style=" width: 60px;">
                                    <asp:TextBox ID="tbxHosyouKigen" runat="server" CssClass="kingaku" MaxLength="2" Width="32px"></asp:TextBox>
                                    年</td>
                                <td class="koumokuMei" style=" width: 80px;">
                                    保証書発行<br/>タイミング</td>
                                <td  style="width: 60px;">
                                    <asp:DropDownList ID="ddlHosyousyoHakkou" runat="server"  >
                                        <asp:ListItem Value="0">依頼書</asp:ListItem>
                                        <asp:ListItem Value="1">自動</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="koumokuMei" style="width: 105px;">入金確認条件</td>
                                <td  style=" width: 190px;">
                                    <asp:DropDownList ID="ddlNyukinKakunin" runat="server">
                                    </asp:DropDownList></td>
                                <td  class="koumokuMei"  style=" width: 100px;">
                                    入金確認覚書</td>
                                <td colspan="2">
                                    <asp:TextBox ID="tbxNyukinKakuninKakusyo" runat="server" MaxLength="10" Width="80px" style="ime-mode:disabled;"></asp:TextBox></td>
                            </tr>



                            <tr>
                                <td class="koumokuMei" style="width: 75px;">
                                    液状化特約管理</td>                                   
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlEkijoukaTokuyakuKannri" runat="server">
                                    </asp:DropDownList>
                                </td>
                                
                                <td class="koumokuMei" style="width: 105px;">新特約切替日 
</td>
                                <td>
                                    <asp:TextBox ID="tbxSinToyoKaisiBi" runat="server" MaxLength="10" Width="65px" CssClass = "codeNumber"></asp:TextBox>
                                </td>
                                <td  class="koumokuMei"  >
                                    WEB申込<br />
                                    採番判別FLG
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlWebMoushikomiSaibanHanbetuFlg" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">1:仮採番</asp:ListItem>
                                    </asp:DropDownList></td>
                                
                            </tr>



                            <tr>
                                <td  class="koumokuMei" >
                                    工事会社<br/>
                                    請求有無</td>
                                <td style="" >
                                <asp:DropDownList ID="ddlKoujiKaisyaSeikyu" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                </asp:DropDownList></td>
                                <td  class="koumokuMei"  >
                                    工事担当<br/>FLG</td>
                                <td style="" ><asp:DropDownList ID="ddlKoujiTantouFlg" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                </asp:DropDownList></td>
                                <td  class="koumokuMei"  >
                                    調査見積書FLG</td>
                                <td style=""  >
                                    <asp:DropDownList ID="ddlTysMitumorisyoFlg" runat="server">
                                    </asp:DropDownList></td>
                                <td  class="koumokuMei"  >
                                    発注書FLG</td>
                                <td colspan="2">
                                <asp:DropDownList ID="ddlHattyusyoFlg" runat="server">
                                </asp:DropDownList></td>
                            </tr>
                            
                            <tr>
                                <td  class="koumokuMei"  colspan = "2">
                                   見積書ファイル名 </td>
                                <td colspan = "4">
                                    <asp:TextBox ID="tbxMitumorisyoFileNm" runat="server" MaxLength="24" Width="280px"></asp:TextBox></td>
                                    
                                <td  class="koumokuMei"  >
                                    発注書未着<br />
                                    連携対象外FLG
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlHattyuusyoMichakuRenkeiTaisyougaiFlg" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">1:連携対象外</asp:ListItem>
                                    </asp:DropDownList></td>
                                    
                            </tr>      
            <tr>
                <td class="koumokuMei" colspan="2">
                    指定請求書有無
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddl_shitei_seikyuusyo_umu" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="koumokuMei">
                    シロアリ検査表示
                </td>
                <td colspan="4">
                    <asp:DropDownList ID="ddl_shiroari_kensa_hyouji" runat="server">
                        <asp:ListItem Value="0">0：非表示</asp:ListItem>
                        <asp:ListItem Value="1">1：表示（請求支払なし）</asp:ListItem>
                        <asp:ListItem Value="2">2：表示</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr style="height:1px; size:1px;">
                <td class="" colspan="7" style="font-size:0px; height:0px; border-style:none;"></td>
                <td style="width: 34px;font-size:1px; height:0px;border-style:none;">&nbsp;</td>
                <td  style="font-size:0px; height:0px;border-style:none;"></td>
            </tr>
            
            
            
        </tbody>
    </table>

    <table style="text-align: left; width:968px;margin-top:10px;" class="mainTable" cellpadding="1" id="Table4" >
        <thead>
            <tr>
                <th class="tableTitle" colspan="8" rowspan="1" style="text-align: left">

                   
                    <a id="titleText_torihiki2" runat="server">取引情報２</a>
                    <span id="titleInfobarTorihiki2" style="display: none;" runat="server">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode ="Conditional" RenderMode="Inline">
                   <ContentTemplate>
                        <asp:Button ID="btnTouroku2" runat="server" Text="登録" OnClick="btnTouroku2_Click" />
                   </ContentTemplate>
                </asp:UpdatePanel>
                    </span>      
                </th>
                <th class="tableTitle" colspan="1" rowspan="1" style="text-align: left">
                </th>
            </tr>
        </thead>
        
        <!--基本情報明細-->
        <tbody id="meisaiTbody_torihiki2" runat="server" style="display: none;"> 
            
            <tr>
                <td class="koumokuMei" colspan="2">
                    自動発行_先方確認者
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbx_hosyousyo_hak_kakuninsya" runat="server" MaxLength="25" Width="140px"></asp:TextBox>
                </td>
                <td class="koumokuMei">
                    自動発行_確認日
                </td>
                <td class="">
                    <asp:TextBox ID="tbx_hosyousyo_hak_kakunin_date" runat="server" MaxLength="10" Width="75px" CssClass = "codeNumber" style="ime-mode:disabled"></asp:TextBox>
                </td>
                <td  class="koumokuMei" colspan="2">
                    保証書引渡日印字有無 &nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddl_hikiwatasi_inji_umu" runat="server">
                        <asp:ListItem Value="">無</asp:ListItem>
                        <asp:ListItem Value="1">1：有</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            
            
            
            <tr>
                <td class="koumokuMei" colspan="2">
                    保証期間_先方確認者
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbx_hosyou_kikan_kakuninsya" runat="server" MaxLength="25" Width="140px"></asp:TextBox>
                </td>
                <td class="koumokuMei">
                    保証期間<br />適用開始日
                </td>
                <td class="" colspan="4">
                    <asp:TextBox ID="tbx_hosyou_kikan_start_date" runat="server" MaxLength="10" Width="75px" CssClass = "codeNumber" style="ime-mode:disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    保証書発送方法</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddl_hosyousyo_hassou_umu" runat="server">
                        <asp:ListItem Value="">郵送</asp:ListItem>
                        <asp:ListItem Value="1">1:PDF納品</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="koumokuMei">
                    保証書発送方法<br />間適用開始日
                </td>
                <td class="" colspan="4">
                    <asp:TextBox ID="tbx_hosyousyo_hassou_umu_start_date" runat="server" MaxLength="10" Width="75px" CssClass = "codeNumber" style="ime-mode:disabled"></asp:TextBox>
                </td>
            </tr>
            
            
            <tr>
                <td class="koumokuMei" colspan="2">
                    サポート調査<br />保証付保FAX先方確認者
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbx_fuho_fax_kakuninsya" runat="server" MaxLength="25" Width="140px"></asp:TextBox>
                </td>
                <td class="koumokuMei">
                    サポート調査<br />保証付保FAX確認日
                </td>
                <td class="">
                    <asp:TextBox ID="tbx_fuho_fax_kakunin_date" runat="server" MaxLength="10" Width="75px" CssClass = "codeNumber" style="ime-mode:disabled"></asp:TextBox>
                </td>
                <td class="koumokuMei" colspan="2">
                    サポート調査<br />保証付保FAX送付有無 &nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddl_fuho_fax_umu" runat="server">
                        <asp:ListItem Value="">無</asp:ListItem>
                        <asp:ListItem Value="1">1：有(工事有り)</asp:ListItem>
                        <asp:ListItem Value="2">2：有（工事無し）</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr style="height:1px; size:1px;">
                <td class="" colspan="7" style="font-size:0px; height:0px; border-style:none;"></td>
                <td style="width: 34px;font-size:1px; height:0px;border-style:none;">&nbsp;</td>
                <td  style="font-size:0px; height:0px;border-style:none;"></td>
            </tr>
            
        </tbody>
    </table>
    
    <table style="text-align: left; margin-top:10px; width: 968px;" class="mainTable" cellpadding="1" id="Table1" >
        <thead>
            <tr>
                <th class="tableTitle" colspan="10" rowspan="1" style="text-align: left; height: 24px;">

                      <a id="titleText_gyoumu" runat="server">取引情報(業務)</a>
                      
                     <span id="titleInfobarTHgyoumu" style="display: none;" runat="server">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode ="Conditional " RenderMode ="Inline">
                         <ContentTemplate>
                            <asp:Button ID="btnTouroku_gyoumu" runat="server" Text="登録" />
                         </ContentTemplate>
                       </asp:UpdatePanel>
                     </span>

                   
                </th>
            </tr>
        </thead>
        <!--基本情報明細-->
        <tbody id="meisaiTbody_gyoumu" runat="server" style="display: none;">

                            <tr>
                                <td style="WIDTH: 60px; height: 55px;" class="koumokuMei">
                                    調査<br />
                                    見積書
                                </td>
                                <td  style="width: 28px; height: 55px;">
                                    <asp:DropDownList ID="ddlTysMitumori" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="WIDTH: 52px; height: 55px;" class="koumokuMei">
                                    基礎<br />
                                    断面図
                                </td>
                                <td colspan = "" style="width: 8px; height: 55px;" >
                                    <asp:DropDownList ID="ddlKisoDanmenzu" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan = "" style="WIDTH: 258px; height: 55px;" class="koumokuMei">
                                    多棟割引<br/>区分</td>
                                <td colspan = ""  style="width: 48px; height: 55px;">
                                    <asp:DropDownList ID="ddlTatouwaribikiKbn" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="0">無</asp:ListItem>
                                        <asp:ListItem Value="1">通常</asp:ListItem>
                                        <asp:ListItem Value="2">特殊</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="height: 55px;" class="koumokuMei">
                                    多棟<br />
                                    割引<br />
                                    備考
                                </td>
                                <td colspan = "" style="width: 303px; height: 55px" >
                                    <asp:TextBox ID="tbxTatouwaibikiBikou" runat="server" MaxLength="40" Width="256px" ></asp:TextBox>
                                </td>
                                <td style="WIDTH: 59px; height: 55px;" class="koumokuMei">
                                    特価<br />
                                    申請
                                </td>
                                <td colspan="" style="width: 100px; height: 55px" >
                                    <asp:DropDownList ID="ddlTokkaSinsei" runat="server" Width="88px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="1">特価+1棟</asp:ListItem>
                                        <asp:ListItem Value="2">特価+連棟</asp:ListItem>
                                        <asp:ListItem Value="3">超特価</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                              
                            <tr>
                                <td colspan="4" >
                                    <table style="border-width : 0px;">
                                 <tr>
                                    <td style="border-width:0px; width: 108px;">
                                    &nbsp;<strong>工事見積</strong><br />
                                    <table style="border-collapse: collapse; border: 1px solid gray; width: 104px;">
                                         <thead >                
                                        <tr class="shouhinTableTitle">
                                            <td style="" >
                                                残土処分費</td>
                                            <td style="" >
                                                給水車代
                                                </td>
                                        </tr>
                                         </thead> 
                                        <tr>
                                            <td style="width: 68px; ">
                                                <asp:DropDownList ID="ddlZandoSyobunhi" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 91px;">
                                                <asp:DropDownList ID="ddlKyusuisyadai" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                            </td>
                                            <td style="border-width:0px; width: 126px;">
                                    &nbsp;<strong>弊社対応</strong><br />
                                    <table style="border-collapse: collapse; border: 1px solid gray;">
                                         <thead class = "gridviewTableHeader">
                                         <tr class="shouhinTableTitle">
                                            <td>
                                                地縄はり</td>
                                            <td >
                                                杭芯出し</td>
                                        </tr>
                                         </thead> 
                                        
                                        <tr>
                                            <td ><asp:DropDownList ID="ddlJinawahari" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                            </asp:DropDownList></td>
                                            <td ><asp:DropDownList ID="ddlKuisindasi" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                            </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="1" valign ="middle" style="WIDTH: 258px" class="koumokuMei">
                                    工事までの<br />
                                    平均日数</td>
                                <td colspan="1" valign ="middle" style=" width: 48px;">
                                    <asp:TextBox ID="tbxHeikinhisuu" runat="server" CssClass="kingaku" MaxLength="3" Width="40px" ></asp:TextBox>
                                </td>
                                <td colspan="1" class="koumokuMei" >
                                    標準<br />
                                    基礎
                                </td>
                                <td style="width: 303px;"  >
                                    <asp:TextBox ID="tbxHyoujunKiso" runat="server"  Width="256px" MaxLength="40"></asp:TextBox>
                                </td> 
                                <td colspan="1" style="WIDTH: 59px; " class="koumokuMei">
                                    JHS以外工事
                                </td>
                                <td colspan="1" style="width: 100px;">
                                    <asp:DropDownList ID="ddlJHSigaiKouji" runat="server">
                                            <asp:ListItem Value="0">なし</asp:ListItem>
                                            <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4" >
                                    <table style="border-width : 0px ;">
                                        <tr>
                                            <td style="border-width:0px; width: 423px;">
                                                &nbsp;<strong>報告書発行部数</strong><br />
                                        <table style="width: 96px;display:inline ;margin-bottom: 4px;border-collapse: collapse; border: 0px solid gray;" >
                                         <thead class = "gridviewTableHeader">
                                         <tr style=" background-color: #FFE4C4;">
                                            <td class="shouhinTableTitle" >
                                                調査</td>
                                            <td class="shouhinTableTitle" >
                                                工事</td>
                                            <td class="shouhinTableTitle" >
                                                検査</td>
                                        </tr>
                                         </thead> 
                                        
                                        <tr>
                                            <td >
                                                <asp:TextBox ID="tbxTysHks" runat="server" CssClass="kingaku" MaxLength="2" Width="32px" style="text-align:right;"></asp:TextBox></td>
                                            <td >
                                                <asp:TextBox ID="tbxKjHks" runat="server" CssClass="kingaku" MaxLength="2" Width="32px" style="text-align:right;"></asp:TextBox></td>
                                            <td >
                                                <asp:TextBox ID="tbxKsHks" runat="server" CssClass="kingaku" MaxLength="2" Width="32px" style="text-align:right;"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                            </td>
                                            <td style="border-width:0px; width: 462px;">
                                                &nbsp;<strong>報告書同封</strong><br />
                                    <table style="width: 88px;display:inline;margin-bottom: 4px;border-collapse: collapse; border: 0px solid gray;">
                                         <thead class = "gridviewTableHeader">
                                         
                                         <tr style=" background-color: #FFE4C4;">
                                            <td style="" class="shouhinTableTitle">
                                                調査</td>
                                            <td style="" class="shouhinTableTitle">
                                                工事</td>
                                            <td style="" class="shouhinTableTitle">
                                                検査</td>
                                        </tr>
                                         </thead>
                                        
                                        <tr>
                                            <td >
                                                <asp:DropDownList ID="ddlTysDoufu" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td >
                                                <asp:DropDownList ID="ddlKjDoufu" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td >
                                                <asp:DropDownList ID="ddlKsDoufu" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td colspan="1" style="WIDTH: 258px" class="koumokuMei">
                                    引渡ﾌｧｲﾙ</td>
                                <td colspan="1" style="width: 48px;">
                                    <asp:DropDownList ID="ddlHikiwataFile" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td colspan="1" class="koumokuMei">
                                    入金前<br />
                                    保証書<br />
                                    発行
                                    </td>
                                <td colspan="3"  style="" >
                                    <asp:TextBox ID="tbxNkHssHakou" runat="server" MaxLength="40" Width="296px"></asp:TextBox>
                                </td>
                            </tr>

        </tbody>
    </table>

    <table id="Table3" cellpadding="1" class="mainTable" style="margin-top: 10px; width: 968px;
        text-align: left;display:none;">
        <thead>
            <tr>
                <th class="tableTitle" colspan="11" rowspan="1" style="text-align: left; height: 24px;">
                
                    <a id="titleText_keiri" runat="server" style ="display :inline " >取引情報(経理)</a>
                    
                       <span id="titleInfobarTHkeiri" style="display: none;" runat="server"> 
                               <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode ="Conditional" RenderMode="Inline" >
                                <ContentTemplate>            
                        <asp:Button ID="btnTouroku_keiri" runat="server" Text="登録" style ="display :inline " />
                                </ContentTemplate> 
                               </asp:UpdatePanel>              
                        </span>
                </th>
            </tr>
        </thead>
        <!--基本情報明細-->
        <tbody id="meisaiTbody_keiri" runat="server" style="display: none;">

                            <tr>
                                <td style="WIDTH: 120px" class="koumokuMei" >
                                    回収締め日</td>
                                <td style="WIDTH: 130px">
                                    <asp:TextBox ID="tbxKaisyuSimeibi" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                <td style="width: 100px" class="koumokuMei" >
                                    請求書必着日</td>
                                <td style="width: 70px" >
                                    <asp:TextBox ID="tbxSeikyuhityakubi" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                <td style="width: 100px" class="koumokuMei" >
                                    支払予定日</td>
                                <td style="width: 70px" >
                                    <asp:TextBox ID="tbxSiharayibi" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                <td style="WIDTH: 71px" class="koumokuMei" >
                                    ヶ月後</td>
                                <td  width = "" style="width: 200px">
                                    <asp:TextBox ID="tbxGetugou" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox>日</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 110px">
                                    &nbsp;<strong>支払方法</strong>
                                    <table style="margin-left: 4px;margin-bottom: 4px; width: 216px;border-collapse: collapse; border: 1px solid gray;">
                                        <tr>
                                            <td class="koumokuMei">
                                                現金
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:TextBox ID="tbxGenkin" runat="server" CssClass="kingaku" MaxLength="3" Width="40px"></asp:TextBox>％&nbsp;</td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlSiharaiHouhou" runat="server">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">振込</asp:ListItem>
                                                    <asp:ListItem Value="2">引落し</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td class="koumokuMei">手形</td>
                                            <td style="">
                                                <asp:TextBox ID="tbxTegata" runat="server" CssClass="kingaku" MaxLength="3" Width="40px"></asp:TextBox>％</td>
                                            <td style="" class="koumokuMei" >
                                                支払ｻｲﾄ
                                            </td>
                                            <td style="">
                                                <asp:TextBox ID="tbxSiharaiSaito" runat="server" CssClass="kingaku" MaxLength="2" Width="40px"></asp:TextBox>日</td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="4">
                                    &nbsp<strong>手形比率</strong>
                                    <table style="margin-left:4px;border-collapse: collapse; border: 1px solid gray;">
                                        <tr>
                                            <td style="height: 21px;width :220px">
                                    <asp:DropDownList ID="ddlTegataHiritu" runat="server"> 
                                    </asp:DropDownList></td>
                                            <td style="height: 21px; width :120px">
                                    <asp:Button ID="btnTyoufuTegata" runat="server" Text="貼付" OnClientClick ="addCommn(this);return false;" Height="24px" Width="48px" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 30px;" >
                                    <asp:TextBox ID="tbxTyoufuTegata" runat="server" Rows="2" TextMode="MultiLine"  MaxLength = "128" Width="320px" Font-Names="Arial" ></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="2"  >
                                    <table style="margin-left:4px; border: gray 1px solid; border-collapse: collapse;">
                                        <tr>
                                            <td class="koumokuMei" style="width: 52px" >
                                                発注書</td>
                                            <td>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                    <asp:DropDownList ID="ddlHattyusyo" runat="server" AutoPostBack ="true"  >
                                       <asp:ListItem Value="1">有り</asp:ListItem>
                                       <asp:ListItem Value="0">なし</asp:ListItem>
                                    </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="">
                                                <table style="border-collapse: collapse; border: 1px solid gray;">
                                                    <thead class = "gridviewTableHeader">
                                                        <tr style=" background-color: #FFE4C4;">
                                                            <td style="width: 50px;" class="shouhinTableTitle">
                                                                調査</td>
                                                            <td style="width: 50px;" class="shouhinTableTitle">
                                                                工事</td>
                                                            <td style="width: 50px;" class="shouhinTableTitle">
                                                                検査</td>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td style="height: 20px; width: 45px;">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                        <ContentTemplate>
                                    <asp:DropDownList ID="ddlHtsTys" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlHattyusyo" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 45px; height: 20px">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                        <ContentTemplate>
                                    <asp:DropDownList ID="ddlHtsKj" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlHattyusyo" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                                        </td>
                                                        <td style="width: 45px; height: 20px;">
                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                        <ContentTemplate>
                                    <asp:DropDownList ID="ddlHtsKs" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlHattyusyo" EventName="SelectedIndexChanged" />
                                        </Triggers>   
                                    </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="koumokuMei"  >
                                    先方指定請求書</td>
                                <td colspan="">
                                    <asp:DropDownList ID="ddlSenpoSiteiSks" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList>
                                   </td> 
                                    <td colspan="4">
                                   <font color = "red" >※請求方法が通常と異なる場合は、フローを提出</font>
                                    </td>
                                <td class="koumokuMei" style="width: 71px" >
                                    ﾌﾛｰ確認日</td>
                                <td >
                                    <asp:TextBox ID="tbxProKakaninbi" runat="server" MaxLength="10" Width="96px" style="ime-mode:disabled;" ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="koumokuMei" >
                                    協力会費
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlKyouryokuKaihi" runat="server">
                                        <asp:ListItem Value="0">なし</asp:ListItem>
                                        <asp:ListItem Value="1">有り</asp:ListItem>
                                    </asp:DropDownList>&nbsp;
                                 </td>
                                <td colspan="6">
                                   &nbsp<strong>協力会費比率</strong>
                                <table style="margin-left:4px; margin-bottom :4px;border: gray 1px solid; border-collapse: collapse;">
                                    <tr>
                                     <td style="height: 21px; width : 220px">
                                <asp:DropDownList ID="ddlKyouryokuKaihiHiritu" runat="server"> 
                                </asp:DropDownList></td>
                                     <td style="width :40px;width : 120px">
                                    <asp:Button ID="btnTyoufuKyouryoukuKh" runat="server" Text="貼付" OnClientClick ="addCommn(this);return false;" Height="24px" Width="48px" />
                                    </td>
                                    </tr>
                                    <tr>
                                     <td colspan="2" style="">
                                    <asp:TextBox ID="tbxTyoufuKyouryoukuKh" runat="server" Rows="2" TextMode="MultiLine"   MaxLength = "128"  Width="320px" Font-Names="Arial"></asp:TextBox></td>
                                    </tr>
                                </table>
                                </td>
                            </tr>

        </tbody>
    </table>
