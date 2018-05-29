<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="SeikyuuSakiHenkouMaster.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuuSakiHenkouMaster"
    Title="請求先変更マスタメンテナンス" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定  
    </script>

    <script language="javascript" for="document" event="onkeydown"> 
  if(event.keyCode==13 && event.srcElement.type!="button" && event.srcElement.type!="submit" && event.srcElement.type!="reset" && event.srcElement.type!="textarea" && event.srcElement.type!="")
     event.keyCode=9; 
    </script>

    <div id="buySelName" runat="server" class="modalDiv" style="position: absolute; left: 300px;
        top: 140px; z-index: 2; display: none;">
    </div>
    <div id="disableDiv" runat="server" style="position: absolute; left: 0px; top: 0px;
        width: 1002px; height: 596px; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <table border="0" cellpadding="0" cellspacing="2" class="titleTable" style="width: 960px;
        text-align: left">
        <tbody>
            <tr>
                <th>
                    請求先変更マスタメンテナンス &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;<asp:Button ID="btnBack" runat="server"
                        CssClass="kyoutuubutton" Text="戻る" />
                </th>
                <th style="text-align: right">
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 13px">
                </td>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="UpdatePanelA" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="TbHead" runat="server" cellpadding="2" class="mainTable" style="width: 680px;
                text-align: left">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 90px">
                            加盟店
                        </td>
                        <td>
                            <asp:TextBox ID="tbxKameiten_cd" CssClass="hissu" runat="server" MaxLength="5" Style="ime-mode: disabled;"
                                Width="64px"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnSearchKameiten" runat="server" Text="検索" />
                            <asp:TextBox ID="tbxKameiten_mei" runat="server" CssClass="readOnlyStyle" MaxLength="40"
                                Width="288px" TabIndex="-1"></asp:TextBox>
                        </td>
                        <td class="koumokuMei" style="width: 35px;">
                            取消
                        </td>
                        <td style="width: 110px;">
                            <asp:TextBox ID="tbxTorikesi" runat="server" Width="100px" CssClass="readOnlyStyle"
                                TabIndex="-1" Style="border-bottom: none;"></asp:TextBox>
                            <asp:HiddenField ID="hidTorikesi" runat="server" />
                            <asp:Button ID="btnChangeColor" runat="server" Style="display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 90px; height: 29px;">
                            商品区分</td>
                        <td style="height: 29px" colspan="3">
                            <asp:DropDownList ID="ddlSyouhinKBN" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr align="left">
                        <td colspan="4" rowspan="1">
                            <asp:Button ID="btnSearch" runat="server" Text="絞込 & 編集" Width="112px" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="クリア" /></td>
                    </tr>
                </tbody>
            </table>
            <br />
            <asp:GridView ID="grdHead" runat="server" CellPadding="0" CellSpacing="0" CssClass="masterHeader"
                ShowHeader="False" Style="border-top: black 1px solid; border-left: black 1px solid">
            </asp:GridView>
            <div id="divMeisai" runat="server">
                <asp:GridView ID="grdBody" runat="server" BackColor="#E6E6E6" BorderColor="GrayText"
                    BorderWidth="1px" CellPadding="0" ShowHeader="False" Style="border-right: #999999 1px solid;
                    border-top: black 1px solid">
                    <RowStyle BorderColor="#999999" BorderWidth="1px" />
                    <SelectedRowStyle ForeColor="White" />
                    <AlternatingRowStyle BackColor="#C0FFC0" />
                </asp:GridView>
            </div>
            <asp:Button Style="display: none" ID="btn" runat="server" Text="Button" OnClick="btn_Click">
            </asp:Button>
            <asp:HiddenField ID="hidTop" runat="server" />
            <asp:HiddenField ID="hidBtn" runat="server" />
            <asp:HiddenField ID="hidRowIndex" runat="server" />
            <asp:HiddenField ID="hidBool" runat="server" />
            <asp:Button ID="btnOpen" runat="server" Style="display: none;" Text="Button" />
            <asp:HiddenField ID="hidKameiten" runat="server" />
            <asp:HiddenField ID="hidSyouhinKBN" runat="server" />
            <asp:HiddenField ID="hidSeikyuuKBN" runat="server" />
            <asp:HiddenField ID="hidstrHenkou" runat="server" />
            <asp:HiddenField ID="hidBrc" runat="server" />
            <asp:HiddenField ID="hidUPDTime" runat="server" />
            <asp:HiddenField ID="hidSyouhinmae" runat="server" />
            <asp:HiddenField ID="hidTorikesiMeisai" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn" />
            <asp:AsyncPostBackTrigger ControlID="btnOpen" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
            <asp:AsyncPostBackTrigger ControlID="btnSearchKameiten" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
