<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master" CodeBehind="YosinJyouhouDirectInquiry.aspx.vb" Inherits="Itis.Earth.WebUI.YosinJyouhouDirectInquiry" 
    title="�^�M��� �_�C���N�g����" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="js/jhsearth.js"></script>
<script type="text/javascript">
    //window���t�^
    var objWin = window;
    objWin.name = "earthMainWindow1"
    initPage(); //��ʏ����ݒ�  
</script>

<div style="width: 900px; height: 500px;text-align:center;">
<table style="text-align:center; width:300px;" border="0" cellpadding="0" cellspacing="2" class="titleTable">
    <tbody>
        <tr>
            <th style="height: 18px;" colspan="2">
                <span style="color: #3300ff; font-size: 16px;">
                    <br />
                �^�M��� �_�C���N�g����
                    <br />
                    <br />
                    <br />
                </span>
            </th>
            
        </tr>
        <tr>
            <td style="width: 127px; height: 29px;text-align:right;">
                �����X�R�[�h</td>
            <td style="text-align:left; height: 29px; width: 163px;">
                &nbsp; &nbsp;
                <asp:TextBox ID="tbxKameitenCd" runat="server" CssClass="codeNumber" MaxLength="5"
                    Style="width: 85px" Width="181px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 65px; text-align: center">
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                &nbsp;<asp:Button ID="btnTyuiJikou" runat="server" CssClass="bottombutton" Text="����"
                    Width="83px" />
                <asp:Button ID="btnClear" runat="server" CssClass="bottombutton" Text="�N���A"
                    Width="83px"  />
                </td>

        </tr>
    </tbody>
</table>
    </div>

</asp:Content>
