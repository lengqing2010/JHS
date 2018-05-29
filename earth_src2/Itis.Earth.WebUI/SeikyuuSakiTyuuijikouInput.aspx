<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="SeikyuuSakiTyuuijikouInput.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuuSakiTyuuijikouInput"
    Title="請求先注意情報登録" %>

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
                請求先注意情報登録
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
            <th style="width: 80px; background-color: #ccffff;">
            </th>
            <th style="width: 100px;">
            </th>
            <th style="width: 80px;">
            </th>
            <th style="width: 160px;">
            </th>
            <th style="width: 80px;">
            </th>
            <th style="width: 160px;">
            </th>
        </tr>
        <tr style="height: 30px;">
            <td colspan="1" style="border-top: none; background-color: #ccffff; padding-left: 5px;">
                請求先
            </td>
            <td colspan="5" style="border-top: none;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:DropDownList ID="ddlSeikyuusakiKbn" runat="server" Style="background-color: #FFE4C4;
                                width: 100px; margin-left: 5px;">
                            </asp:DropDownList>
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:TextBox ID="tbxSeikyuusakiCd" runat="server" Text="" MaxLength="5" Style="background-color: #FFE4C4;
                                width: 50px; ime-mode: disabled; margin-left: 5px;"></asp:TextBox>
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:Label ID="lblCdBrc" runat="server" Text="－"></asp:Label>
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:TextBox ID="tbxSeikyuusakiBrc" runat="server" Text="" MaxLength="2" Style="background-color: #FFE4C4;
                                width: 20px; ime-mode: disabled;"></asp:TextBox>
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:Button ID="btnSeikyuusakiSearch" runat="server" Text="検索" Style="padding-top: 2px;
                                margin-left: 5px;" />
                        </td>
                        <td style="width: 85px; border-top: none; border-bottom: none; border-left: none;
                            border-right: none; padding-left: 20px;">
                            請求締め日：
                        </td>
                        <td style="width: 55px; border-top: none; border-bottom: none; border-left: none;
                            border-right: none;">
                            <asp:Label ID="lblSeikyuuSimeDate" runat="server" Style="width: auto;" Text="1"></asp:Label>
                        </td>
                        <td style="width: 95px; border-top: none; border-bottom: none; border-left: none;
                            border-right: none;">
                            請求書必着日：
                        </td>
                        <td style="width: 55px; border-top: none; border-bottom: none; border-left: none;
                            border-right: none;">
                            <asp:Label ID="lblSeikyuusyoHittykDate" runat="server" Style="width: auto;" Text="2"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9" style="border-top: none; border-bottom: none; border-left: none;
                            border-right: none;">
                            <asp:TextBox ID="tbxSeikyuusakiMei" runat="server" CssClass="readOnlyStyle" ReadOnly="true"
                                TabIndex="-1" Style="width: 530px; margin-left: 5px;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td style="background-color: #ccffff; padding-left: 5px;">
                入力№
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblNo" runat="server" Style="width: auto;" Text="999"></asp:Label>
            </td>
            <td style="background-color: #ccffff; padding-left: 5px;">
                重要度
            </td>
            <td style="padding-left: 5px;">
                <asp:DropDownList ID="ddlJyuuyoudo" runat="server" Style="width: 80px;">
                    <asp:ListItem Value="2" Text="高"></asp:ListItem>
                    <asp:ListItem Value="1" Text="中"></asp:ListItem>
                    <asp:ListItem Value="0" Text="低" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="background-color: #ccffff; padding-left: 5px;">
                取消
            </td>
            <td style="padding-left: 10px;">
                <asp:CheckBox ID="chkTorikesi" runat="server" Text="" />
            </td>
        </tr>
        <tr style="height: 30px;">
            <td style="background-color: #ccffff; padding-left: 5px;">
                種別コード
            </td>
            <td colspan="5" style="padding-left: 5px;">
                <asp:DropDownList ID="ddlSyubetuCd" runat="server" Style="background-color: #FFE4C4;
                    width: 350px;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="height: 55px;">
            <td style="background-color: #ccffff; padding-left: 5px;">
                詳細
            </td>
            <td colspan="5" style="padding-left: 5px;">
                <asp:TextBox ID="tbxSyousai" runat="server" TextMode="MultiLine" Text="" Style="width: 520px;
                    height: 35px; font-family: ＭＳ Ｐゴシック; overflow-y: hidden; overflow-x: hidden;"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnDisplay" runat="server" Text="" Style="display: none;" />
    <asp:Button ID="btnUpdate" runat="server" Text="" Style="display: none;" />
    <asp:HiddenField ID="hdnSeikyuusakiKbn" runat="server" />
    <asp:HiddenField ID="hdnSeikyuusakiCd" runat="server" />
    <asp:HiddenField ID="hdnSeikyuusakiBrc" runat="server" />
    <asp:HiddenField ID="hdnSearchFlg" runat="server" />
</asp:Content>
