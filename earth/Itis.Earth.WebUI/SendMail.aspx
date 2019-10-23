<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SendMail.aspx.vb" Inherits="Itis.Earth.WebUI.SendMail" Title="メール送信" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script language="javascript" type="text/javascript">
// <!CDATA[

// クリアボタン押下時
function ButtonClear_onclick() {
    document.getElementById("ctl00$CPH1$TextTo").value = "";
    document.getElementById("ctl00$CPH1$TextSubject").value = "";
    document.getElementById("ctl00$CPH1$TextBody").value = "";
}

// ]]>
    </script>

    <table style="border: 1px solid gray; background-color: #EEEEEE; width: 840px;">
        <tr>
            <td>
                <asp:Label ID="LabelMessage" runat="server" Font-Bold="True" ForeColor="Red" Width="595px"></asp:Label></td>
        </tr>
    </table>
    <br />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    EARTH メール送信</th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <table style="width: 600px; border: 1px solid gray;">
        <tr>
            <td style="width: 84px; height: 20px" class="koumokuMei" id="TdFrom" runat="server">
                送信者</td>
            <td style="width: 97px; height: 20px;" colspan="2">
                <asp:TextBox ID="TextFrom" runat="server" Font-Names="ＭＳ ゴシック" Width="300px" BorderStyle="None"
                    ReadOnly="True">k</asp:TextBox><nobr>※WebConfig.xmlに設定</nobr>
            </td>
        </tr>
        <tr>
            <td style="width: 84px; height: 20px;" class="koumokuMei" id="TdTo" runat="server">
                送信先</td>
            <td style="width: 97px; height: 20px;">
                <asp:TextBox ID="TextTo" runat="server" Width="400px" Font-Names="ＭＳ ゴシック" CssClass="hissu"></asp:TextBox>
            </td>
            <td rowspan="2">
                <input id="ButtonClear" type="button" value="クリア" onclick="return ButtonClear_onclick()"
                    style="width: 58px; height: 40px" /></td>
        </tr>
        <tr>
            <td style="width: 84px; height: 16px" class="koumokuMei" id="TdSubject" runat="server">
                件名</td>
            <td style="width: 97px; height: 16px">
                <asp:TextBox ID="TextSubject" runat="server" Width="400px" Font-Names="ＭＳ ゴシック" CssClass="hissu">[EARTH]：</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 84px; height: 296px;" class="koumokuMei" id="TdBody" runat="server">
                本文</td>
            <td style="width: 97px; height: 296px;" colspan="2">
                <asp:TextBox ID="TextBody" runat="server" Rows="22" TextMode="MultiLine" Width="486px"
                    Font-Names="ＭＳ ゴシック" CssClass="hissu">★ EARTHシステム管理者よりお知らせ ★</asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 84px" class="koumokuMei" id="TD2" runat="server">
                添付</td>
            <td style="width: 97px" colspan="2">
                <asp:FileUpload ID="FileUpload" runat="server" Width="500px" /></td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Button ID="ButtonSend" runat="server" Height="39px" Text="送　信" Width="138px" /></td>
        </tr>
    </table>
</asp:Content>
