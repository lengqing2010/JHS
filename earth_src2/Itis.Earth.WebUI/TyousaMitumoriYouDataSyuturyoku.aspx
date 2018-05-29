<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="TyousaMitumoriYouDataSyuturyoku.aspx.vb" Inherits="Itis.Earth.WebUI.TyousaMitumoriYouDataSyuturyoku"
    Title="見積書作成用データ出力" %>

<%@ Register Src="control/common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
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
        width: 98%; height: 9%; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCsvData" />
            <asp:PostBackTrigger ControlID="btnNO" />
        </Triggers>
        <ContentTemplate>
            <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                class="titleTable">
                <tr>
                    <th>
                        見積書作成用データ出力
                    </th>
                    <th style="text-align: right;">
                    </th>
                </tr>
                <tr>
                    <td colspan="2" rowspan="1">
                    </td>
                </tr>
            </table>
            <table id="tblKensaku" style="width: 970px; text-align: left;" class="mainTable paddinNarrow"
                cellpadding="1">
                <tr>
                    <th class="tableTitle" colspan="1" rowspan="1" style="height: 20px;">
                        検索条件
                    </th>
                    <th class="tableTitle" colspan="7" rowspan="1" style="height: 20px;">
                        <asp:Button runat="server" ID="btnClearWin" Text="クリア" OnClientClick="fncClearWin();return false;" />
                    </th>
                </tr>
                <tr>
                    <td class="koumokuMei" style="width: 60px;">
                        区分</td>
                    <td colspan="3" style="width: 260px;">
                        <uc1:common_drop ID="ddlKbn" runat="server" GetWidth="232" GetStyle="kubun" />
                    </td>
                    <td class="koumokuMei" style="width: 80px;">
                        加盟店</td>
                    <td style="border-right-color: #E6E6E6; border-right: solid 1px gray;">
                        <asp:TextBox runat="server" ID="tbxKameiTenCd" MaxLength="5" AutoCompleteType="Disabled"
                            Style="width: 55px;" AutoPostBack="true" CssClass="codeNumber"></asp:TextBox>
                        <asp:Button runat="server" ID="btnKameiTenSearch" Text="検索" OnClientClick="return fncKameitenSearch('1');" />
                        <asp:TextBox ID="tbxKameiTenMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"  Width="260px"></asp:TextBox>
                    </td>
                    <td class="koumokuMei" style="width: 38px;">
                        取消
                    </td>
                    <td style="width: 150px;">
                        <asp:TextBox ID="tbxTorikesi" runat="server" Width="80px" CssClass="readOnlyStyle"
                            TabIndex="-1" Style="border-bottom: none;"></asp:TextBox>
                        <asp:HiddenField ID="hidTorikesi" runat="server" />
                        <input type="button" id ="btnChangeColor" style="display:none;" onclick="fncChangeColor();" />
                    </td>
                </tr>
                
                <tr>
                    <td class="koumokuMei" style="font-weight: bold;">
                        施主名</td>
                    <td colspan="3" class="" style="border-right-color: #E6E6E6;">
                        <asp:TextBox ID="tbxSesyuMei" runat="server" width="259px"></asp:TextBox>
                    </td>
                    <td class="koumokuMei" style=" ">
                        系列コード
                    </td>
                    <td class="" style="border-right-color: #E6E6E6; border-right: solid 1px gray;">
                        <asp:TextBox runat="server" ID="tbxKeiretuCd" MaxLength="5" Style="width: 55px;" CssClass="codeNumber" OnTextChanged="tbxKeiretuCd_TextChanged"></asp:TextBox>
                        <asp:Button runat="server" ID="btnKeiretuSearch" Text="検索" OnClientClick="return fncKeiretuSearch();" />
                        <asp:TextBox runat="Server" ID="tbxKeiretuMei" CssClass="readOnlyStyle" Width="260px" TabIndex="-1" BackColor="#E6E6E6"></asp:TextBox>
                    </td>
                    <td class="koumokuMei">東西</td>
                    <td >
                        <asp:RadioButton ID="rbnTyou" runat="server" Text="東日本" GroupName="EW" />
                        <asp:RadioButton ID="rbnSei" runat="server" Text="西日本" GroupName="EW" />
                    </td>
                </tr>
                
                <tr>
                    <td class="hissu" style="font-weight: bold;">
                        番号</td>
                    <td class="hissu" style="border-right-color: #E6E6E6; ">
                        <asp:TextBox runat="server" ID="tbxBangou1" MaxLength="10" AutoCompleteType="Disabled" Width="118"
                            AutoPostBack="true" CssClass="codeNumber"></asp:TextBox>
                    </td>
                    <td class="hissu" style="border-left-color: #E6E6E6; border-right-color: #E6E6E6;">
                        ～
                    </td>
                    <td class="hissu" style="border-left-color: #E6E6E6; ">
                        <asp:TextBox runat="server" ID="tbxBangou2" MaxLength="10" AutoCompleteType="Disabled" Width="118"
                            AutoPostBack="true" CssClass="codeNumber"></asp:TextBox>
                    </td>
                    <td class="koumokuMei">
                        見積書作成</td>
                    <td colspan="3">
                        <asp:RadioButton ID="rbnMitumori1" runat="server" Text="未" GroupName="Mitumori" Checked="true" />
                        &nbsp;／&nbsp;<asp:RadioButton ID="rbnMitumori2" runat="server" Text="済" GroupName="Mitumori" />
                    </td>
                </tr>
                <tr class="tableSpacer">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right" style="border-right-color: #E6E6E6;">
                        検索上限件数</td>
                    <td colspan="1" rowspan="1" align="left" style="border-left-color: #E6E6E6; border-right-color: #E6E6E6;">
                        <asp:DropDownList runat="server" ID="ddlSearchCount" Width="80px">
                            <asp:ListItem Value="10" Text="10件" Selected="true"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50件"></asp:ListItem>
                            <asp:ListItem Value="max" Text="無制限"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="3" style="border-left-color: #E6E6E6;">
                        <asp:Button runat="server" ID="btnKensaku" Text="検索実行" /></td>
                </tr>
            </table>
            <table style="text-align: left; width: 100px;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="height: 15px">
                        検索結果：
                    </td>
                    <td>
                        <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        件
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="height: 15px;">
                        <asp:RadioButton ID="rbnFlg1" ForeColor="blue" Font-Bold="true" runat="server" Text="個別"
                            GroupName="Flg" Checked="true" />
                        &nbsp;／&nbsp;<asp:RadioButton ID="rbnFlg2" runat="server" ForeColor="blue" Font-Bold="true"
                            Text="連棟" GroupName="Flg" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnCsvData" Text="CSV出力" />
                        <asp:Button ID="btnExcelDownLoad" runat="server" Width="150px" Text="EXCEL原紙ダウンロード" />
                        <asp:Button ID="btnToukenDownLoad" runat="server" Width="170px" Text="東建様用_原紙ダウンロード"  />
                    </td>
                </tr>
            </table>
            <table class="gridviewTableHeader" cellpadding="0" cellspacing="0" style="width:913px">
                <tr style="vertical-align: middle;">
                    <td style="width: 54px; border-left: 1px solid black;">
                        対象<asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" /></td>
                    <td style="width: 39px;">
                        区分
                    </td>
                    <td style="width: 80px;">
                        番号
                    </td>
                    <td style="width: 266px;">
                        物件名
                    </td>
                    <td style="width: 92px;">
                        見積書作成日
                    </td>
                    <td style="width: 88px;">
                        加盟店コード
                    </td>
                    <td style="border-right: 1px solid black;">
                        加盟店名
                    </td>
                </tr>
            </table>
            <div runat="server" id="divMeisai">
                <asp:GridView ID="grdItiran" runat="server" AutoGenerateColumns="False" BackColor="White"
                    CssClass="tableMeiSai" BorderWidth="1px" ShowHeader="False" CellPadding="0" Width="913">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkTaisyou" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="57px" Height="21px" HorizontalAlign="Center" BorderColor="#999999" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="kbn">
                            <ItemStyle  Width="42px" HorizontalAlign="Center" BorderColor="#999999" />
                        </asp:BoundField>
                        <asp:BoundField DataField="hosyousyo_no">
                            <ItemStyle Width="82px" HorizontalAlign="Left" BorderColor="#999999" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sesyu_mei">
                            <ItemStyle Width="275px" HorizontalAlign="Left" BorderColor="#999999" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tys_mitsyo_sakusei_date">
                            <ItemStyle Width="97px" HorizontalAlign="Left" BorderColor="#999999" />
                        </asp:BoundField>
                        <asp:BoundField DataField="kameiten_cd">
                            <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                        </asp:BoundField>
                        <asp:BoundField DataField="kameiten_mei1">
                            <ItemStyle  HorizontalAlign="Left" BorderColor="#999999" />
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle ForeColor="White" />
                    <AlternatingRowStyle BackColor="LightCyan" />
                </asp:GridView>
            </div>
            <asp:HiddenField ID="hidCsvFlg" runat="server" />
            <asp:HiddenField ID="hidKbn_HosyousyoNo" runat="server" />
            <asp:HiddenField ID="hidKameitenCd" runat="server" />
            <asp:HiddenField ID="hidMitumori" runat="server" />
            <asp:Button ID="btnNO" runat="server" Text="Button" Style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
