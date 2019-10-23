<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="MasterHiduke.aspx.vb" Inherits="Itis.Earth.WebUI.MasterHiduke" Title="EARTH 日付マスタ更新" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
    	<!-- 
		//変更確認MSG表示
		function Js_ChkReg(strCTLID){
            var varRes;
            varRes = confirm('日付を変更します。よろしいですか？');
            if (varRes == true){

            }else{

            }
		}
	-->
    </script>

    <!-- 画面上部・ヘッダ[Table1] -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    日付マスタ編集
                </th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <table style="width: 300px;" class="mainTable" cellpadding="3">
        <!-- 1行目 -->
        <tr>
            <th colspan="2" style="font-size: 16px;" class="">
                自動設定用データ
            </th>
        </tr>
        <tr>
            <td style="width: 60px" class="koumokuMei">
                区分
            </td>
            <td>
                <asp:DropDownList ID="SelectKubun" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <!-- 2行目 -->
        <tr>
            <td colspan="2">
                <table id="Table2" class="miniTable" cellpadding="3">
                    <!-- 1行目 -->
                    <tr>
                        <td style="width: 100px" class="koumokuMei">
                            保証書発行日
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelHosyousyoHakkou" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextHosyousyoHakkouDate" CssClass="date readOnlyStyle" MaxLength="10"
                                        Style="color: Red;" Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="TextHosyousyoHakkouHenkou" MaxLength="10" Style="" CssClass="date"
                                        Text="" runat="server" />&nbsp;&nbsp;<input type="button" id="ButtonHosyousyoHakkouHenkou"
                                            style="width: 40px" class="" value="変更" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="SelectKubun" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- 2行目 -->
                    <tr>
                        <td class="koumokuMei">
                            報告書発送日
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelHoukokusyoHassou" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextHoukokusyoHassouDate" MaxLength="10" Style="color: Red" CssClass="date readOnlyStyle"
                                        Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="TextHoukokusyoHassouHenkou" MaxLength="10" Style="" CssClass="date"
                                        Text="" runat="server" />&nbsp;&nbsp;<input type="button" id="ButtonHoukokusyoHassouHenkou"
                                            style="width: 40px" class="" value="変更" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="SelectKubun" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- 3行目 -->
                    <tr>
                        <td class="koumokuMei">
                            保証書NO年月
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelHosyousyoNo" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextHosyousyoNo" MaxLength="10" Style="color: Red;" CssClass="date readOnlyStyle"
                                        Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="TextHosyousyoNoHenkou" MaxLength="7" Style="" CssClass="date" Text=""
                                        runat="server" />&nbsp;&nbsp;<input type="button" id="ButtonHosyousyoNoHenkou" style="width: 40px"
                                            class="" value="変更" runat="server" />
                                    <asp:HiddenField ID="HiddenUpdDateTime" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="SelectKubun" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
