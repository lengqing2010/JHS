<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="YosinJyouhouDirectList.aspx.vb" Inherits="Itis.Earth.WebUI.YosinJyouhouDirectList" 
    title="�^�M��� �_�C���N�g����" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window���t�^
    var objWin = window;
    objWin.name = "earthMainWindow1"
    initPage(); //��ʏ����ݒ�  


</script>

<div style="width: 900px; height: 500px;text-align:left;">
                    <br />
   <table style="text-align: left;width: 646px;" class="mainTable" cellpadding="0" id="Table5">
                        <!--������񖾍�-->
                        <tbody id="Tbody4" runat="server"  class="tableMeiSai" style="margin-top:0px;width: 950px; ">
                            <tr>
                                <td class="koumokuMei" style="width: 153px">
                                    �����X�R�[�h</td>
                                <td style="width: 150px" >
                                    <asp:TextBox runat="server" ID="tbxKameitenCd" style="width: 70px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/></td>
                                <td class="koumokuMei" style="width: 126px">
                                �����X��
                                </td>
                                <td >
                                    <asp:TextBox runat="server" ID="tbxKameitenMei" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                                </td>
                            </tr>
                        </tbody>
                    </table>
    <br />
    <br />
 <table style="text-align: left;width: 646px;"class="gridviewTableHeader"�@border ="0" cellpadding="0" cellspacing ="0">
  <tr>
                 <td style="border-left: black 1px solid;border-bottom: black 1px solid;width: 110px; height: 14px;">
                     ���񂹐���</td>
                 <td  style="border-bottom: black 1px solid;width: 128px; height: 14px;">
                     ���񂹐�R�[�h</td>
                 <td style="border-bottom: black 1px solid;height: 14px" >
                     ���񂹐於</td>
             </tr>
 
 </table>
    <br />
 <table style="text-align: left;width: 646px;" class="mainTable" cellpadding="0" id="TABLE1" >
        <!--������񖾍�-->
         <tbody id="kyoutuTBody" runat="server"  class="tableMeiSai" style="margin-top:0px;width: 950px; ">
            
            <tr>
                <td class="koumokuMei" style="width: 109px">
                    ����</td>
                <td style="width: 128px" >
                    <asp:TextBox runat="server" ID="tbxTyousaSeikyuuCd" style="width: 70px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/></td>
                <td >
                    <asp:TextBox runat="server" ID="tbxTyousaSeikyuuMei" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
           
         </tbody>       
    </table> 
    <br/>
  <table style="text-align: left;width: 646px;" class="mainTable" cellpadding="0" id="TABLE2" >
        <!--������񖾍�-->
         <tbody id="Tbody1" runat="server"  class="tableMeiSai" style="margin-top:0px;width: 950px; ">
            
            <tr>
                <td class="koumokuMei" style="width: 109px">
                    �H��</td>
                <td style="width: 128px" >
                    <asp:TextBox runat="server" ID="tbxKojSeikyuuCd" style="width: 70px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/></td>
                <td >
                    <asp:TextBox runat="server" ID="tbxKojSeikyuuMei" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
           
         </tbody>       
    </table>    
    <br/>
  <table style="text-align: left;width: 646px;" class="mainTable" cellpadding="0" id="TABLE3" >
        <!--������񖾍�-->
         <tbody id="Tbody2" runat="server"  class="tableMeiSai" style="margin-top:0px;width: 950px; ">
            
            <tr>
                <td class="koumokuMei" style="width: 109px">
                    �̑��i</td>
                <td style="width: 128px" >
                    <asp:TextBox ID="tbxHansokuhinSeikyuuCd" runat="server" CssClass="readOnly" ReadOnly="true"
                        Style="width: 70px;" TabIndex="-1">
                    </asp:TextBox></td>
                <td >
                    <asp:TextBox runat="server" ID="tbxHansokuhinSeikyuuMei" style="width: 280px;" CssClass ="readOnly"  readonly="true" TabIndex="-1"/>
                </td>
            </tr>
           
         </tbody>       
    </table>    
    <asp:HiddenField ID="hidNayose" runat="server" />
    <asp:Button ID="Button1" runat="server" Text="Button" style="display:none;"/>
    </div>

</asp:Content>
