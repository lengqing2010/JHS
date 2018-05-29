<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="HanbaiKakakuMasterKobetuSettei.aspx.vb" Inherits="Itis.Earth.WebUI.HanbaiKakakuMasterKobetuSettei"
    Title="販売価格マスタ個別設定" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>

    <table border="0" cellpadding="1" cellspacing="2" style="width: 650px; text-align: left;"
        class="titleTable">
        <tr>
            <th style="width: 200px;">
                販売価格マスタ個別設定
            </th>
            <th style="width: 100px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="width: 90px; height: 25px;
                    padding-top: 2px;" />
            </th>
            <th style="width: 220px;">
            </th>
            <th style="width: 120px;">
                <asp:Button ID="btnTouroku" runat="server" Text="登録" Style="width: 110px; height: 30px;
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
            <th style="width: 50px;">
            </th>
            <th style="width: 150px;">
            </th>
            <th style="width: 220px;">
            </th>
            <th style="width: 30px;">
            </th>
            <th style="width: 70px;">
            </th>
            <th style="width: 80px;">
            </th>
        </tr>
        <tr style="height: 30px;">
            <td colspan="1" style="border-top: none; background-color: #ccffff; padding-left: 5px;">
                相手先
            </td>
            <td colspan="6" style="border-top: none; height: 25px; border-right: 0px; padding-left: 10px;">
                <asp:DropDownList ID="ddlAiteSakiSyubetu" runat="server" Style="background-color: #FFE4C4;
                    width: 90px;">
                </asp:DropDownList>
                <asp:TextBox ID="tbxAiteSakiCd" runat="server" Text="" Style="background-color: #FFE4C4;
                    width: 50px;" MaxLength="5" CssClass="codeNumber"></asp:TextBox>
                <asp:Button ID="btnAiteSakiCd" runat="server" Text="検索" Style="width: 40px; padding-top: 2px;" />
                <asp:TextBox ID="tbxAiteSakiMei" runat="server" CssClass="readOnlyStyle" ReadOnly="true"
                    TabIndex="-1" Style="width: 260px;"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td colspan="1" style="background-color: #ccffff; padding-left: 5px;">
                商品
            </td>
            <td colspan="3" style="padding-left: 10px;">
                <asp:DropDownList ID="ddlSyouhinCd" runat="server" Style="background-color: #FFE4C4;
                    width: 350px;">
                </asp:DropDownList>
            </td>
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                取消
            </td>
            <td colspan="1" style="padding-left: 10px;">
                <asp:CheckBox ID="chkTorikesi" runat="server" Text="" Checked="true" />
            </td>
        </tr>
        <tr style="height: 30px;">
            <td colspan="1" style="background-color: #ccffff; padding-left: 5px;">
                調査方法
            </td>
            <td colspan="3" style="padding-left: 10px;">
                <asp:DropDownList ID="ddlTyousaHouhou" runat="server" Style="background-color: #FFE4C4;
                    width: 310px;">
                </asp:DropDownList>
            </td>
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                公開
            </td>
            <td colspan="1" style="padding-left: 10px;">
                <asp:CheckBox ID="chkKoukai" runat="server" Text="" Checked="true" />
            </td>
        </tr>
        <tr style="height: 30px;">
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                工務店請求金額
            </td>
            <td colspan="1" style="padding-left: 10px;">
                <asp:TextBox ID="tbxKoumutenSeikyuuKingaku" runat="server" Text="" MaxLength="13"
                    Style="width: 100px; text-align: right; ime-mode: disabled;" CssClass="codeNumber"></asp:TextBox>
            </td>
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                工務店請求金額変更フラグ
            </td>
            <td colspan="2" style="padding-left: 10px;">
                <asp:DropDownList ID="ddlKoumutenSeikyuuKingakuFlg" runat="server" Style="width: 120px;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                実請求金額
            </td>
            <td colspan="1" style="padding-left: 10px;">
                <asp:TextBox ID="tbxJituSeikyuuKingaku" runat="server" Text="" MaxLength="13" Style="width: 100px;
                    text-align: right; ime-mode: disabled;" CssClass="codeNumber"></asp:TextBox>
            </td>
            <td colspan="2" style="background-color: #ccffff; padding-left: 5px;">
                実請求金額変更フラグ
            </td>
            <td colspan="2" style="padding-left: 10px;">
                <asp:DropDownList ID="ddlJituSeikyuuKingakuFlg" runat="server" Style="width: 120px;">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnKensakuFlg" runat="server" />
    <asp:HiddenField ID="hdnAiteSakiSyubetu" runat="server" />
    <asp:HiddenField ID="hdnAiteSakiCd" runat="server" />
    <asp:HiddenField ID="hdnDbFlg" runat="server" />
</asp:Content>
