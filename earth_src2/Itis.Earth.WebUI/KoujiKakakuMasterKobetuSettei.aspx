<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="KoujiKakakuMasterKobetuSettei.aspx.vb" Inherits="Itis.Earth.WebUI.KoujiKakakuMasterKobetuSettei"
    Title="çHéñâøäiÉ}ÉXÉ^å¬ï ê›íË" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //windowñºïtó^
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //âÊñ èâä˙ê›íË
    </script>

    <table border="0" cellpadding="1" cellspacing="2" style="width: 650px; text-align: left;"
        class="titleTable">
        <tr>
            <th style="width: 200px;">
                çHéñâøäiÉ}ÉXÉ^å¬ï ê›íË
            </th>
            <th style="width: 100px;">
                <asp:Button ID="btnClose" runat="server" Text="ï¬Ç∂ÇÈ" Style="width: 90px; height: 25px;
                    padding-top: 2px;" />
            </th>
            <th style="width: 220px;">
            </th>
            <th style="width: 120px;">
                <asp:Button ID="btnTouroku" runat="server" Text="ìoò^" Style="width: 110px; height: 30px;
                    background-color: #ff66ff; font-size: 14px; font-weight: bold; padding-top: 2px;" />
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td colspan="5" rowspan="1">
            </td>
        </tr>
    </table>
    <table style="text-align: left;" class="mainTable paddinNarrow" cellpadding="1">
        <tr style="height: 0px;">
            <th style="width: 100px; background-color: #ccffff;">
            </th>
            <th style="width: 19px;">
            </th>
            <th style="width: 379px;">
            </th>
            <th style="width: 64px;">
            </th>
            <th style="width: 56px;">
            </th>
            <th style="width: 20px;">
            </th>
            <th style="width: 106px;">
            </th>
        </tr>
        <tr style="height: 30px;">
            <td colspan="1" style="border-top: none; background-color: #ccffff; padding-left: 5px;">
                ëäéËêÊ
            </td>
            <td colspan="6" style="border-top: none; height: 25px; border-right: 0px; padding-left: 10px;">
                <asp:DropDownList ID="ddlAiteSakiSyubetu" runat="server" Style="background-color: #FFE4C4;
                    width: 90px;">
                </asp:DropDownList>
                <asp:TextBox ID="tbxAiteSakiCd" runat="server" Text="" Style="background-color: #FFE4C4;
                    width: 50px;" MaxLength="5" CssClass="codeNumber"></asp:TextBox>
                <asp:Button ID="btnAiteSakiCd" runat="server" Text="åüçı" Style="width: 40px; padding-top: 2px;" />
                <asp:TextBox ID="tbxAiteSakiMei" runat="server" CssClass="readOnlyStyle" ReadOnly="true"
                    TabIndex="-1" Width="288px" ></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td colspan="1" style="background-color: #ccffff; padding-left: 5px;">
                è§ïi
            </td>
            <td colspan="3" style="padding-left: 10px;">
                <asp:DropDownList ID="ddlSyouhinCd" runat="server" Style="background-color: #FFE4C4;
                    width: 350px;">
                </asp:DropDownList>
            </td>
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                éÊè¡
            </td>
            <td colspan="1" style="padding-left: 10px; width: 106px;">
                <asp:CheckBox ID="chkTorikesi" runat="server" Text="" Checked="true" />
            </td>
        </tr>
        <tr style="height: 30px;">
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                çHéñâÔé–
            </td>
            <td colspan="1" style="padding-left: 10px; width: 379px;">
                <asp:TextBox ID="tbxKojKaisyaCd" runat="server" Text="" Style="background-color: #FFE4C4;width: 60px;" MaxLength="6"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="åüçı" Style="width: 32px; padding-top: 2px;"
                    OnClientClick="return fncKojKaisyaSearch();" />
                <asp:TextBox ID="tbxKojKaisyaMei" runat="server" CssClass="readOnlyStyle" ReadOnly="true" TabIndex="-1"
                   Width="264px"></asp:TextBox>
            </td>
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                çHéñâÔé–êøãÅóLñ≥
            </td>
            <td colspan="2" style="padding-left: 10px;">
                <asp:DropDownList ID="ddlKojGaisyaSeikyuuUmu" runat="server" Style="width: 120px;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                îÑè„ã‡äz(ê≈î≤)
            </td>
            <td colspan="1" style="padding-left: 10px; width: 379px;">
                <asp:TextBox ID="tbxUriGaku" runat="server" Text="" MaxLength="13" Style="width: 100px;
                    text-align: right; ime-mode: disabled;" CssClass="codeNumber"></asp:TextBox>
            </td>
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                êøãÅóLñ≥
            </td>
            <td colspan="2" style="padding-left: 10px;">
                <asp:DropDownList ID="ddlSeikyuuUmu" runat="server" Style="width: 120px;">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnKensakuFlg" runat="server" />
    <asp:HiddenField ID="hdnAiteSakiSyubetu" runat="server" />
    <asp:HiddenField ID="hdnAiteSakiCd" runat="server" />
    <asp:HiddenField ID="hdnKensakuFlg2" runat="server" />
    <asp:HiddenField ID="hdnKojKaisyaCd" runat="server" />
    <asp:HiddenField ID="hdnDbFlg" runat="server" />
</asp:Content>
