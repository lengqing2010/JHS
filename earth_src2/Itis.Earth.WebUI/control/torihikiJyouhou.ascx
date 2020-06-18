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
                    <a id="titleText_torihiki" runat="server">������</a>
                    <span id="titleInfobarTorihiki" style="display: none;" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode ="Conditional" RenderMode="Inline">
                   <ContentTemplate>
                        <asp:Button ID="btnTouroku" runat="server" Text="�o�^" />
                   </ContentTemplate>
                </asp:UpdatePanel>
                    </span>      
                </th>
                <th class="tableTitle" colspan="1" rowspan="1" style="text-align: left">
                </th>
            </tr>
        </thead>
        <!--��{��񖾍�-->
        <tbody id="meisaiTbody_torihiki" runat="server" style="display: none;"> 
                            <tr>
                                <td class="koumokuMei" style="width: 75px;">
                                    �ۏ؊���</td>
                                <td  style=" width: 60px;">
                                    <asp:TextBox ID="tbxHosyouKigen" runat="server" CssClass="kingaku" MaxLength="2" Width="32px"></asp:TextBox>
                                    �N</td>
                                <td class="koumokuMei" style=" width: 80px;">
                                    �ۏ؏����s<br/>�^�C�~���O</td>
                                <td  style="width: 60px;">
                                    <asp:DropDownList ID="ddlHosyousyoHakkou" runat="server"  >
                                        <asp:ListItem Value="0">�˗���</asp:ListItem>
                                        <asp:ListItem Value="1">����</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="koumokuMei" style="width: 105px;">�����m�F����</td>
                                <td  style=" width: 190px;">
                                    <asp:DropDownList ID="ddlNyukinKakunin" runat="server">
                                    </asp:DropDownList></td>
                                <td  class="koumokuMei"  style=" width: 100px;">
                                    �����m�F�o��</td>
                                <td colspan="2">
                                    <asp:TextBox ID="tbxNyukinKakuninKakusyo" runat="server" MaxLength="10" Width="80px" style="ime-mode:disabled;"></asp:TextBox></td>
                            </tr>



                            <tr>
                                <td class="koumokuMei" style="width: 75px;">
                                    �t�󉻓���Ǘ�</td>                                   
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlEkijoukaTokuyakuKannri" runat="server">
                                    </asp:DropDownList>
                                </td>
                                
                                <td class="koumokuMei" style="width: 105px;">�V����ؑ֓� 
</td>
                                <td>
                                    <asp:TextBox ID="tbxSinToyoKaisiBi" runat="server" MaxLength="10" Width="65px" CssClass = "codeNumber"></asp:TextBox>
                                </td>
                                <td  class="koumokuMei"  >
                                    WEB�\��<br />
                                    �̔Ԕ���FLG
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlWebMoushikomiSaibanHanbetuFlg" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">1:���̔�</asp:ListItem>
                                    </asp:DropDownList></td>
                                
                            </tr>



                            <tr>
                                <td  class="koumokuMei" >
                                    �H�����<br/>
                                    �����L��</td>
                                <td style="" >
                                <asp:DropDownList ID="ddlKoujiKaisyaSeikyu" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                </asp:DropDownList></td>
                                <td  class="koumokuMei"  >
                                    �H���S��<br/>FLG</td>
                                <td style="" ><asp:DropDownList ID="ddlKoujiTantouFlg" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                </asp:DropDownList></td>
                                <td  class="koumokuMei"  >
                                    �������Ϗ�FLG</td>
                                <td style=""  >
                                    <asp:DropDownList ID="ddlTysMitumorisyoFlg" runat="server">
                                    </asp:DropDownList></td>
                                <td  class="koumokuMei"  >
                                    ������FLG</td>
                                <td colspan="2">
                                <asp:DropDownList ID="ddlHattyusyoFlg" runat="server">
                                </asp:DropDownList></td>
                            </tr>
                            
                            <tr>
                                <td  class="koumokuMei"  colspan = "2">
                                   ���Ϗ��t�@�C���� </td>
                                <td colspan = "4">
                                    <asp:TextBox ID="tbxMitumorisyoFileNm" runat="server" MaxLength="24" Width="280px"></asp:TextBox></td>
                                    
                                <td  class="koumokuMei"  >
                                    ����������<br />
                                    �A�g�ΏۊOFLG
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlHattyuusyoMichakuRenkeiTaisyougaiFlg" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">1:�A�g�ΏۊO</asp:ListItem>
                                    </asp:DropDownList></td>
                                    
                            </tr>      
            <tr>
                <td class="koumokuMei" colspan="2">
                    �w�萿�����L��
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddl_shitei_seikyuusyo_umu" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="koumokuMei">
                    �V���A�������\��
                </td>
                <td colspan="4">
                    <asp:DropDownList ID="ddl_shiroari_kensa_hyouji" runat="server">
                        <asp:ListItem Value="0">0�F��\��</asp:ListItem>
                        <asp:ListItem Value="1">1�F�\���i�����x���Ȃ��j</asp:ListItem>
                        <asp:ListItem Value="2">2�F�\��</asp:ListItem>
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

                   
                    <a id="titleText_torihiki2" runat="server">������Q</a>
                    <span id="titleInfobarTorihiki2" style="display: none;" runat="server">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode ="Conditional" RenderMode="Inline">
                   <ContentTemplate>
                        <asp:Button ID="btnTouroku2" runat="server" Text="�o�^" OnClick="btnTouroku2_Click" />
                   </ContentTemplate>
                </asp:UpdatePanel>
                    </span>      
                </th>
                <th class="tableTitle" colspan="1" rowspan="1" style="text-align: left">
                </th>
            </tr>
        </thead>
        
        <!--��{��񖾍�-->
        <tbody id="meisaiTbody_torihiki2" runat="server" style="display: none;"> 
            
            <tr>
                <td class="koumokuMei" colspan="2">
                    �������s_����m�F��
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbx_hosyousyo_hak_kakuninsya" runat="server" MaxLength="25" Width="140px"></asp:TextBox>
                </td>
                <td class="koumokuMei">
                    �������s_�m�F��
                </td>
                <td class="">
                    <asp:TextBox ID="tbx_hosyousyo_hak_kakunin_date" runat="server" MaxLength="10" Width="75px" CssClass = "codeNumber" style="ime-mode:disabled"></asp:TextBox>
                </td>
                <td  class="koumokuMei" colspan="2">
                    �ۏ؏����n���󎚗L�� &nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddl_hikiwatasi_inji_umu" runat="server">
                        <asp:ListItem Value="">��</asp:ListItem>
                        <asp:ListItem Value="1">1�F�L</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            
            
            
            <tr>
                <td class="koumokuMei" colspan="2">
                    �ۏ؊���_����m�F��
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbx_hosyou_kikan_kakuninsya" runat="server" MaxLength="25" Width="140px"></asp:TextBox>
                </td>
                <td class="koumokuMei">
                    �ۏ؊���<br />�K�p�J�n��
                </td>
                <td class="" colspan="4">
                    <asp:TextBox ID="tbx_hosyou_kikan_start_date" runat="server" MaxLength="10" Width="75px" CssClass = "codeNumber" style="ime-mode:disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    �ۏ؏��������@</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddl_hosyousyo_hassou_umu" runat="server">
                        <asp:ListItem Value="">�X��</asp:ListItem>
                        <asp:ListItem Value="1">1:PDF�[�i</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="koumokuMei">
                    �ۏ؏��������@<br />�ԓK�p�J�n��
                </td>
                <td class="" colspan="4">
                    <asp:TextBox ID="tbx_hosyousyo_hassou_umu_start_date" runat="server" MaxLength="10" Width="75px" CssClass = "codeNumber" style="ime-mode:disabled"></asp:TextBox>
                </td>
            </tr>
            
            
            <tr>
                <td class="koumokuMei" colspan="2">
                    �T�|�[�g����<br />�ۏؕt��FAX����m�F��
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbx_fuho_fax_kakuninsya" runat="server" MaxLength="25" Width="140px"></asp:TextBox>
                </td>
                <td class="koumokuMei">
                    �T�|�[�g����<br />�ۏؕt��FAX�m�F��
                </td>
                <td class="">
                    <asp:TextBox ID="tbx_fuho_fax_kakunin_date" runat="server" MaxLength="10" Width="75px" CssClass = "codeNumber" style="ime-mode:disabled"></asp:TextBox>
                </td>
                <td class="koumokuMei" colspan="2">
                    �T�|�[�g����<br />�ۏؕt��FAX���t�L�� &nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddl_fuho_fax_umu" runat="server">
                        <asp:ListItem Value="">��</asp:ListItem>
                        <asp:ListItem Value="1">1�F�L(�H���L��)</asp:ListItem>
                        <asp:ListItem Value="2">2�F�L�i�H�������j</asp:ListItem>
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

                      <a id="titleText_gyoumu" runat="server">������(�Ɩ�)</a>
                      
                     <span id="titleInfobarTHgyoumu" style="display: none;" runat="server">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode ="Conditional " RenderMode ="Inline">
                         <ContentTemplate>
                            <asp:Button ID="btnTouroku_gyoumu" runat="server" Text="�o�^" />
                         </ContentTemplate>
                       </asp:UpdatePanel>
                     </span>

                   
                </th>
            </tr>
        </thead>
        <!--��{��񖾍�-->
        <tbody id="meisaiTbody_gyoumu" runat="server" style="display: none;">

                            <tr>
                                <td style="WIDTH: 60px; height: 55px;" class="koumokuMei">
                                    ����<br />
                                    ���Ϗ�
                                </td>
                                <td  style="width: 28px; height: 55px;">
                                    <asp:DropDownList ID="ddlTysMitumori" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="WIDTH: 52px; height: 55px;" class="koumokuMei">
                                    ��b<br />
                                    �f�ʐ}
                                </td>
                                <td colspan = "" style="width: 8px; height: 55px;" >
                                    <asp:DropDownList ID="ddlKisoDanmenzu" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan = "" style="WIDTH: 258px; height: 55px;" class="koumokuMei">
                                    ��������<br/>�敪</td>
                                <td colspan = ""  style="width: 48px; height: 55px;">
                                    <asp:DropDownList ID="ddlTatouwaribikiKbn" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="0">��</asp:ListItem>
                                        <asp:ListItem Value="1">�ʏ�</asp:ListItem>
                                        <asp:ListItem Value="2">����</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="height: 55px;" class="koumokuMei">
                                    ����<br />
                                    ����<br />
                                    ���l
                                </td>
                                <td colspan = "" style="width: 303px; height: 55px" >
                                    <asp:TextBox ID="tbxTatouwaibikiBikou" runat="server" MaxLength="40" Width="256px" ></asp:TextBox>
                                </td>
                                <td style="WIDTH: 59px; height: 55px;" class="koumokuMei">
                                    ����<br />
                                    �\��
                                </td>
                                <td colspan="" style="width: 100px; height: 55px" >
                                    <asp:DropDownList ID="ddlTokkaSinsei" runat="server" Width="88px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="1">����+1��</asp:ListItem>
                                        <asp:ListItem Value="2">����+�A��</asp:ListItem>
                                        <asp:ListItem Value="3">������</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                              
                            <tr>
                                <td colspan="4" >
                                    <table style="border-width : 0px;">
                                 <tr>
                                    <td style="border-width:0px; width: 108px;">
                                    &nbsp;<strong>�H������</strong><br />
                                    <table style="border-collapse: collapse; border: 1px solid gray; width: 104px;">
                                         <thead >                
                                        <tr class="shouhinTableTitle">
                                            <td style="" >
                                                �c�y������</td>
                                            <td style="" >
                                                �����ԑ�
                                                </td>
                                        </tr>
                                         </thead> 
                                        <tr>
                                            <td style="width: 68px; ">
                                                <asp:DropDownList ID="ddlZandoSyobunhi" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 91px;">
                                                <asp:DropDownList ID="ddlKyusuisyadai" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                            </td>
                                            <td style="border-width:0px; width: 126px;">
                                    &nbsp;<strong>���БΉ�</strong><br />
                                    <table style="border-collapse: collapse; border: 1px solid gray;">
                                         <thead class = "gridviewTableHeader">
                                         <tr class="shouhinTableTitle">
                                            <td>
                                                �n��͂�</td>
                                            <td >
                                                �Y�c�o��</td>
                                        </tr>
                                         </thead> 
                                        
                                        <tr>
                                            <td ><asp:DropDownList ID="ddlJinawahari" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                            </asp:DropDownList></td>
                                            <td ><asp:DropDownList ID="ddlKuisindasi" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                            </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="1" valign ="middle" style="WIDTH: 258px" class="koumokuMei">
                                    �H���܂ł�<br />
                                    ���ϓ���</td>
                                <td colspan="1" valign ="middle" style=" width: 48px;">
                                    <asp:TextBox ID="tbxHeikinhisuu" runat="server" CssClass="kingaku" MaxLength="3" Width="40px" ></asp:TextBox>
                                </td>
                                <td colspan="1" class="koumokuMei" >
                                    �W��<br />
                                    ��b
                                </td>
                                <td style="width: 303px;"  >
                                    <asp:TextBox ID="tbxHyoujunKiso" runat="server"  Width="256px" MaxLength="40"></asp:TextBox>
                                </td> 
                                <td colspan="1" style="WIDTH: 59px; " class="koumokuMei">
                                    JHS�ȊO�H��
                                </td>
                                <td colspan="1" style="width: 100px;">
                                    <asp:DropDownList ID="ddlJHSigaiKouji" runat="server">
                                            <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                            <asp:ListItem Value="1">�L��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4" >
                                    <table style="border-width : 0px ;">
                                        <tr>
                                            <td style="border-width:0px; width: 423px;">
                                                &nbsp;<strong>�񍐏����s����</strong><br />
                                        <table style="width: 96px;display:inline ;margin-bottom: 4px;border-collapse: collapse; border: 0px solid gray;" >
                                         <thead class = "gridviewTableHeader">
                                         <tr style=" background-color: #FFE4C4;">
                                            <td class="shouhinTableTitle" >
                                                ����</td>
                                            <td class="shouhinTableTitle" >
                                                �H��</td>
                                            <td class="shouhinTableTitle" >
                                                ����</td>
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
                                                &nbsp;<strong>�񍐏�����</strong><br />
                                    <table style="width: 88px;display:inline;margin-bottom: 4px;border-collapse: collapse; border: 0px solid gray;">
                                         <thead class = "gridviewTableHeader">
                                         
                                         <tr style=" background-color: #FFE4C4;">
                                            <td style="" class="shouhinTableTitle">
                                                ����</td>
                                            <td style="" class="shouhinTableTitle">
                                                �H��</td>
                                            <td style="" class="shouhinTableTitle">
                                                ����</td>
                                        </tr>
                                         </thead>
                                        
                                        <tr>
                                            <td >
                                                <asp:DropDownList ID="ddlTysDoufu" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td >
                                                <asp:DropDownList ID="ddlKjDoufu" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td >
                                                <asp:DropDownList ID="ddlKsDoufu" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td colspan="1" style="WIDTH: 258px" class="koumokuMei">
                                    ���ņ��</td>
                                <td colspan="1" style="width: 48px;">
                                    <asp:DropDownList ID="ddlHikiwataFile" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td colspan="1" class="koumokuMei">
                                    �����O<br />
                                    �ۏ؏�<br />
                                    ���s
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
                
                    <a id="titleText_keiri" runat="server" style ="display :inline " >������(�o��)</a>
                    
                       <span id="titleInfobarTHkeiri" style="display: none;" runat="server"> 
                               <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode ="Conditional" RenderMode="Inline" >
                                <ContentTemplate>            
                        <asp:Button ID="btnTouroku_keiri" runat="server" Text="�o�^" style ="display :inline " />
                                </ContentTemplate> 
                               </asp:UpdatePanel>              
                        </span>
                </th>
            </tr>
        </thead>
        <!--��{��񖾍�-->
        <tbody id="meisaiTbody_keiri" runat="server" style="display: none;">

                            <tr>
                                <td style="WIDTH: 120px" class="koumokuMei" >
                                    ������ߓ�</td>
                                <td style="WIDTH: 130px">
                                    <asp:TextBox ID="tbxKaisyuSimeibi" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                <td style="width: 100px" class="koumokuMei" >
                                    �������K����</td>
                                <td style="width: 70px" >
                                    <asp:TextBox ID="tbxSeikyuhityakubi" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                <td style="width: 100px" class="koumokuMei" >
                                    �x���\���</td>
                                <td style="width: 70px" >
                                    <asp:TextBox ID="tbxSiharayibi" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox></td>
                                <td style="WIDTH: 71px" class="koumokuMei" >
                                    ������</td>
                                <td  width = "" style="width: 200px">
                                    <asp:TextBox ID="tbxGetugou" runat="server" CssClass="kingaku" MaxLength="2" Width="48px"></asp:TextBox>��</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 110px">
                                    &nbsp;<strong>�x�����@</strong>
                                    <table style="margin-left: 4px;margin-bottom: 4px; width: 216px;border-collapse: collapse; border: 1px solid gray;">
                                        <tr>
                                            <td class="koumokuMei">
                                                ����
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:TextBox ID="tbxGenkin" runat="server" CssClass="kingaku" MaxLength="3" Width="40px"></asp:TextBox>��&nbsp;</td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlSiharaiHouhou" runat="server">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">�U��</asp:ListItem>
                                                    <asp:ListItem Value="2">������</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td class="koumokuMei">��`</td>
                                            <td style="">
                                                <asp:TextBox ID="tbxTegata" runat="server" CssClass="kingaku" MaxLength="3" Width="40px"></asp:TextBox>��</td>
                                            <td style="" class="koumokuMei" >
                                                �x�����
                                            </td>
                                            <td style="">
                                                <asp:TextBox ID="tbxSiharaiSaito" runat="server" CssClass="kingaku" MaxLength="2" Width="40px"></asp:TextBox>��</td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="4">
                                    &nbsp<strong>��`�䗦</strong>
                                    <table style="margin-left:4px;border-collapse: collapse; border: 1px solid gray;">
                                        <tr>
                                            <td style="height: 21px;width :220px">
                                    <asp:DropDownList ID="ddlTegataHiritu" runat="server"> 
                                    </asp:DropDownList></td>
                                            <td style="height: 21px; width :120px">
                                    <asp:Button ID="btnTyoufuTegata" runat="server" Text="�\�t" OnClientClick ="addCommn(this);return false;" Height="24px" Width="48px" /></td>
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
                                                ������</td>
                                            <td>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                    <asp:DropDownList ID="ddlHattyusyo" runat="server" AutoPostBack ="true"  >
                                       <asp:ListItem Value="1">�L��</asp:ListItem>
                                       <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
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
                                                                ����</td>
                                                            <td style="width: 50px;" class="shouhinTableTitle">
                                                                �H��</td>
                                                            <td style="width: 50px;" class="shouhinTableTitle">
                                                                ����</td>
                                                        </tr>
                                                    </thead>
                                                    <tr>
                                                        <td style="height: 20px; width: 45px;">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                        <ContentTemplate>
                                    <asp:DropDownList ID="ddlHtsTys" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
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
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
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
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
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
                                    ����w�萿����</td>
                                <td colspan="">
                                    <asp:DropDownList ID="ddlSenpoSiteiSks" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                    </asp:DropDownList>
                                   </td> 
                                    <td colspan="4">
                                   <font color = "red" >���������@���ʏ�ƈقȂ�ꍇ�́A�t���[���o</font>
                                    </td>
                                <td class="koumokuMei" style="width: 71px" >
                                    �۰�m�F��</td>
                                <td >
                                    <asp:TextBox ID="tbxProKakaninbi" runat="server" MaxLength="10" Width="96px" style="ime-mode:disabled;" ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="koumokuMei" >
                                    ���͉��
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlKyouryokuKaihi" runat="server">
                                        <asp:ListItem Value="0">�Ȃ�</asp:ListItem>
                                        <asp:ListItem Value="1">�L��</asp:ListItem>
                                    </asp:DropDownList>&nbsp;
                                 </td>
                                <td colspan="6">
                                   &nbsp<strong>���͉��䗦</strong>
                                <table style="margin-left:4px; margin-bottom :4px;border: gray 1px solid; border-collapse: collapse;">
                                    <tr>
                                     <td style="height: 21px; width : 220px">
                                <asp:DropDownList ID="ddlKyouryokuKaihiHiritu" runat="server"> 
                                </asp:DropDownList></td>
                                     <td style="width :40px;width : 120px">
                                    <asp:Button ID="btnTyoufuKyouryoukuKh" runat="server" Text="�\�t" OnClientClick ="addCommn(this);return false;" Height="24px" Width="48px" />
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
