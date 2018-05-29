<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="SeikyuuSakiTyuuijikouSearchList.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuuSakiTyuuijikouSearchList"
    Title="請求先注意事項照会" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定     
    </script>

    <div id="buySelName" runat="server" class="modalDiv" style="position: absolute; left: 300px;
        top: 140px; z-index: 2; display: none;">
    </div>
    <div id="disableDiv" runat="server" style="position: absolute; left: 0px; top: 0px;
        width: 1002px; height: 620px; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <table style="text-align: left; width: 960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
            <th style="width: 180px;">
                請求先注意事項照会
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnTouroku" runat="server" Text="新規登録" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th>
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td colspan="4" rowspan="1">
            </td>
        </tr>
    </table>
    <table id="tblKensaku" style="width: 960px; text-align: left;" class="mainTable paddinNarrow"
        cellpadding="1">
        <tr>
            <th class="tableTitle" style="width: 90px; height: 20px;">
                検索条件
            </th>
            <th class="tableTitle" style="width: 390px;">
                <asp:Button ID="btnClear" runat="server" Text="クリア" Style="padding-top: 2px;" />
            </th>
            <th class="tableTitle" style="width: 70px;">
            </th>
            <th class="tableTitle" style="width: 90px;">
            </th>
            <th class="tableTitle" style="width: 90px;">
            </th>
            <th class="tableTitle" style="width: 70px;">
            </th>
            <th class="tableTitle" style="width: 90px;">
            </th>
            <th class="tableTitle" style="width: 70px;">
            </th>
        </tr>
        <tr>
            <td style="height: 28px; background-color: #ccffff;">
                請求先</td>
            <td colspan="7">
                <table border="0" cellpadding="0" cellspacing="0" style="border-top: none; border-bottom: none;
                    border-left: none; border-right: none;">
                    <tr>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:DropDownList ID="ddlSeikyuusakiKbn" runat="server" Style="width: 100px; margin-left: 5px;">
                            </asp:DropDownList>
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:TextBox ID="tbxSeikyuusakiCd" runat="server" Text="" MaxLength="5" Style="width: 50px;
                                ime-mode: disabled; margin-left: 5px;"></asp:TextBox>
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:Label ID="lblCdBrc" runat="server" Text="－"></asp:Label></td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:TextBox ID="tbxSeikyuusakiBrc" runat="server" Text="" MaxLength="2" Style="width: 20px;
                                ime-mode: disabled;"></asp:TextBox>
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:Button ID="btnSeikyuusakiSearch" runat="server" Text="検索" Style="padding-top: 2px;
                                margin-left: 5px;" />
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:TextBox ID="tbxSeikyuusakiMei" runat="server" CssClass="readOnlyStyle" ReadOnly="true"
                                TabIndex="-1" Style="width: 530px; margin-left: 5px;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 28px; background-color: #ccffff;">
                種別コード</td>
            <td>
                <asp:DropDownList ID="ddlSyubetuCd" runat="server" Style="width: 350px; margin-left: 5px;">
                </asp:DropDownList>
            </td>
            <td style="height: 28px; background-color: #ccffff;">
                重要度</td>
            <td>
                <asp:DropDownList ID="ddlJyuuyoudo" runat="server" Style="width: 60px; margin-left: 5px;">
                </asp:DropDownList>
            </td>
            <td style="height: 28px; background-color: #ccffff;">
                請求締め日</td>
            <td>
                <asp:TextBox ID="tbxSeikyuuSimeDate" runat="server" Text="" MaxLength="2" Style="width: 25px;
                    text-align: right; ime-mode: disabled; margin-left: 5px;"></asp:TextBox>
            </td>
            <td style="height: 28px; background-color: #ccffff;">
                請求書必着日</td>
            <td>
                <asp:TextBox ID="tbxSeikyuusyoHittykDate" runat="server" Text="" MaxLength="2" Style="width: 25px;
                    text-align: right; ime-mode: disabled; margin-left: 5px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="8" style="height: 28px;">
                <asp:Label ID="lblKensuu" runat="server" Text="検索上限件数" Style="margin-left: 100px;"></asp:Label>
                <asp:DropDownList ID="ddlKensaKensuu" runat="server" Style="width: 70px; margin-left: 10px;">
                    <asp:ListItem Value="10" Text="10件"></asp:ListItem>
                    <asp:ListItem Value="100" Text="100件" Selected="true"></asp:ListItem>
                    <asp:ListItem Value="max" Text="無制限"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnKensakujiltukou" runat="server" Text="検索実行" Style="height: 25px;
                    padding-top: 2px; margin-left: 10px;" />
                <asp:CheckBox ID="chkKensakuTaisyouGai" runat="server" Text="取消は検索対象外" Style="margin-left: 10px;" />
                <asp:Button ID="btnCsvOutput" runat="server" Text="CSV出力" Style="height: 25px; padding-top: 2px;
                    margin-left: 5px;" />
            </td>
        </tr>
    </table>
    <table id="tableKensuu" border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="width: 65px; text-align: left;">
                検索結果：
            </td>
            <td>
                <asp:Label runat="server" ID="lblCount" Style="width: auto;"></asp:Label>
            </td>
            <td style="width: 20px; text-align: right;">
                件
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHead" style="width: 958px; overflow-y: hidden; overflow-x: hidden; border-top: solid 1px black;
                    border-left: solid 1px black; border-bottom: solid 1px black; border-right: solid 1px black;">
                    <table cellpadding="0" cellspacing="0" style="width: 959px; background-color: #ffffd9;
                        font-weight: bold; text-align: center;">
                        <tr style="height: 32px;">
                            <td style="width: 73px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray;">
                                請CD<asp:LinkButton ID="btnSeikyuusakiCdUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSeikyuusakiCdDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 218px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray;">
                                請求先名<asp:LinkButton ID="btnSeikyuusakiMeiUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSeikyuusakiMeiDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 26px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray;">
                                №</td>
                            <td style="width: 38px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray;">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            取<br />
                                            消
                                        </td>
                                        <td>
                                            <div id="divTorikesi" runat="server" style="width: 23px; border: none; overflow: hidden;">
                                                <asp:LinkButton ID="btnTorikesiUp" runat="server" TabIndex="-1" Font-Underline="false"
                                                    Height="14px" Text="▲"></asp:LinkButton>
                                                <asp:LinkButton ID="btnTorikesiDown" runat="server" TabIndex="-1" Font-Underline="false"
                                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 130px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray;">
                                種別コード<asp:LinkButton ID="btnSyubetuCdUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSyubetuCdDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="border-top: none; border-bottom: none; border-left: none; border-right: solid 1px gray;">
                                詳細<asp:LinkButton ID="btnSyousaiUp" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Text="▲"></asp:LinkButton>
                                <asp:LinkButton ID="btnSyousaiDown" runat="server" TabIndex="-1" Font-Underline="false"
                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton></td>
                            <td style="width: 38px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray;">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            重<br />
                                            要
                                        </td>
                                        <td>
                                            <div id="divJyuuyoudo" runat="server" style="width: 23px; border: none; overflow: hidden;">
                                                <asp:LinkButton ID="btnJyuuyoudoUp" runat="server" TabIndex="-1" Font-Underline="false"
                                                    Height="14px" Text="▲"></asp:LinkButton>
                                                <asp:LinkButton ID="btnJyuuyoudoDown" runat="server" TabIndex="-1" Font-Underline="false"
                                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 38px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray;">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            締<br />
                                            日
                                        </td>
                                        <td>
                                            <div id="divSeikyuuSimeDate" runat="server" style="width: 23px; border: none; overflow: hidden;">
                                                <asp:LinkButton ID="btnSeikyuuSimeDateUp" runat="server" TabIndex="-1" Font-Underline="false"
                                                    Height="14px" Text="▲"></asp:LinkButton>
                                                <asp:LinkButton ID="btnSeikyuuSimeDateDown" runat="server" TabIndex="-1" Font-Underline="false"
                                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 38px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray;">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            必<br />
                                            着
                                        </td>
                                        <td>
                                            <div id="divSeikyuusyoHittykDate" runat="server" style="width: 23px; border: none;
                                                overflow: hidden;">
                                                <asp:LinkButton ID="btnSeikyuusyoHittykDateUp" runat="server" TabIndex="-1" Font-Underline="false"
                                                    Height="14px" Text="▲"></asp:LinkButton>
                                                <asp:LinkButton ID="btnSeikyuusyoHittykDateDown" runat="server" TabIndex="-1" Font-Underline="false"
                                                    Height="14px" Style="margin-left: -10px" Text="▼"></asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 960px;">
                <div id="divMeisai" runat="server" onmousewheel="wheel();" style="width: 958px; height: 289px;
                    overflow-y: hidden; overflow-x: hidden; border-left: solid 1px black; border-right: solid 1px black;
                    margin-top: -1px; border-bottom: solid 1px black;">
                    <asp:GridView ID="grdMeisai" runat="server" AutoGenerateColumns="False" CellPadding="0"
                        Width="959px" ShowHeader="False" Style="padding-left: 2px;">
                        <Columns>
                            <asp:BoundField DataField="_seikyuu_saki_cd">
                                <ItemStyle Width="70px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="216px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSeikyuuSakiMei" runat="server" Width="214px" Text='<%#Eval("seikyuu_saki_mei")%>'
                                        ToolTip='<%#Eval("seikyuu_saki_mei")%>' Style="font-size: 12px; white-space: normal;
                                        overflow: hidden;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="24px" HorizontalAlign="Right" BorderColor="#999999" Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNyuuryokuNo" runat="server" Width="22px" Text='<%#Eval("nyuuryoku_no")%>'
                                        ToolTip='<%#Eval("nyuuryoku_no")%>' Style="font-size: 12px; white-space: normal;
                                        overflow: hidden;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="_torikesi">
                                <ItemStyle Width="36px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="128px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSyubetuCd" runat="server" Width="126px" Text='<%#Eval("_syubetu_cd")%>'
                                        ToolTip='<%#Eval("_syubetu_cd")%>' Style="font-size: 12px; white-space: normal;
                                        overflow: hidden;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSyousai" runat="server" Width="344px" Text='<%#Eval("syousai")%>'
                                        ToolTip='<%#Eval("syousai")%>' Style="font-size: 12px; white-space: normal; overflow: hidden;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="_jyuyodo">
                                <ItemStyle Width="36px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="_seikyuu_sime_date">
                                <ItemStyle Width="36px" HorizontalAlign="Right" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="_seikyuusyo_hittyk_date">
                                <ItemStyle Width="36px" HorizontalAlign="Right" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="seikyuu_saki_kbn">
                                <ItemStyle Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="seikyuu_saki_cd">
                                <ItemStyle Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="seikyuu_saki_brc">
                                <ItemStyle Width="1px" />
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#CCFFFF" />
                        <RowStyle Height="31px" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 16px; height: 289px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 289px; width: 30px;
                    margin-left: -14px;" onscroll="fncScrollV();">
                    <table height="<%=scrollHeight%>">
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnCsvSyori" runat="server" Text="" Style="display: none;" />
    <asp:HiddenField ID="hidCSVFlg" runat="server" />
</asp:Content>
