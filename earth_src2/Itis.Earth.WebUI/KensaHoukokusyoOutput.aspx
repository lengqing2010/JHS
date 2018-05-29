<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="KensaHoukokusyoOutput.aspx.vb" Inherits="Itis.Earth.WebUI.KensaHoukokusyoOutput"
    Title="検査報告書_各帳票出力画面" %>

<%@ Register Src="control/common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定
    
    //onload
    document.body.onload = function(){
        if ("<%=gridviewId%>"!=''){
            objEBI("<%=tblRightId%>").style.height=objEBI("<%=gridviewId%>").offsetHeight;
//            objEBI("<%=tblLeftId%>").style.width=objEBI("<%=gridviewId%>").offsetWidth;
        }
    }
    
    function fncClose()
    {
        window.close();
    }
   
    </script>

    <div id="buySelName" runat="server" class="modalDiv" style="position: absolute; left: 300px;
        top: 140px; z-index: 2; display: none;">
    </div>
    <div id="disableDiv" runat="server" style="position: absolute; left: 0px; top: 0px;
        width: 100%; height: 100%; z-index: 100; filter: alpha(opacity=70); background-color: #000000;
        display: none;">
        <iframe src="about:blank" id="hiddenIframe" width="100%" height="100%"></iframe>
    </div>
    <table style="text-align: left; width: 960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
            <th style="width: 250px;">
                検査報告書_各帳票出力画面
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnDefault" runat="server" Text="" TabIndex="-1" OnClientClick=";return false;"
                    Style="width: 1px; background-color: Transparent; border: none 0px;" />
                <asp:Button ID="btnReturn" runat="server" Text="戻る" Style="width: 50px; height: 25px;
                    padding-top: 2px;" />
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
    </table>
    <table id="tblKensaku" style="width: 960px; text-align: left;" class="mainTable paddinNarrow"
        cellpadding="1">
        <tr>
            <th class="tableTitle" style="width: 65px; height: 20px;">
                検索条件
            </th>
            <th class="tableTitle" style="width: 350px;">
                <asp:Button ID="btnClear" runat="server" Text="クリア" Style="padding-top: 2px;" />
            </th>
            <th class="tableTitle" style="width: 65px;">
            </th>
            <th class="tableTitle">
            </th>
        </tr>
        <tr>
            <td style="height: 28px;background-color: #ccffff; font-weight: bold;">
                発送日
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbxSendDateFrom" runat="server" Width="100px" MaxLength="10" CssClass="codeNumber"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="～" Style="margin-left: 5px;"></asp:Label>
                <asp:TextBox ID="tbxSendDateTo" runat="server" Width="100px" MaxLength="10" CssClass="codeNumber"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 28px; background-color: #ccffff; font-weight: bold;">
                区分
            </td>
            <td>
                <uc1:common_drop ID="ddlKbn" runat="server" GetWidth="238" GetStyle="kubun" />
            </td>
            <td style="background-color: #ccffff; font-weight: bold;">
                番号
            </td>
            <td>
                <asp:TextBox ID="tbxNoFrom" runat="server" Width="120px" MaxLength="10" CssClass="codeNumber"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="～" Style="margin-left: 5px;"></asp:Label>
                <asp:TextBox ID="tbxNoTo" runat="server" Width="120px" MaxLength="10" CssClass="codeNumber"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 28px; background-color: #ccffff; font-weight: bold;">
                加盟店
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbxKameitenCd" runat="server" MaxLength="5" CssClass="codeNumber"
                    Style="width: 40px;"></asp:TextBox>
                <asp:Button ID="btnKameitenCd" runat="server" Text="検索" OnClientClick="fncKameitenSearch('1');return false;"
                    Style="padding-top: 2px;" />
                <asp:TextBox ID="tbxKameitenMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Style="width: 250px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 28px; text-align: center;">
                <asp:Label ID="lblJyouken" runat="server" Text="検索上限件数" Style=""></asp:Label>
                <asp:DropDownList ID="ddlKensakuJyouken" runat="server" Style="width: 70px;">
                    <asp:ListItem Value="10" Text="10件"></asp:ListItem>
                    <asp:ListItem Value="100" Text="100件" Selected="true"></asp:ListItem>
                    <asp:ListItem Value="max" Text="無制限"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnKensakujiltukou" runat="server" Text="検索実行" Style="height: 25px;
                    padding-top: 2px;" />
                <asp:Button ID="btnCsvOutput" runat="server" Text="CSV出力" Style="height: 25px; padding-top: 2px;" />
                <asp:CheckBox ID="chkKensakuTaisyouGai" runat="server" Text="取消は検索対象外" Checked="true"
                    Enabled="false" />
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
                <div id="divHeadLeft" style="width: 960px; border-top: solid 1px black; border-left: solid 1px black;
                    border-right: solid 1px black; border-bottom: solid 1px black; overflow: hidden;">
                    <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="width: 961px;
                        background-color: #ffffd9; font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 61px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                管理
                                <asp:LinkButton runat="server" ID="btnKanriNoUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKanriNoDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 32px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                区分
                                <asp:LinkButton runat="server" ID="btnKbnUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKbnDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                保証書No
                                <asp:LinkButton runat="server" ID="btnHosyousyoNoUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnHosyousyoNoDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 282px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                施主名
                                <asp:LinkButton runat="server" ID="btnSesyuMeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSesyuMeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                加盟店CD
                                <asp:LinkButton runat="server" ID="btnKameitenCdUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKameitenCdDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="border-top: none; border-bottom: none; border-right: solid 1px gray; text-align: center;
                                font-size: 12px;">
                                加盟店名
                                <asp:LinkButton runat="server" ID="btnKameitenMeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKameitenMeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 52px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                部数
                                <asp:LinkButton runat="server" ID="btnBusuuUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnBusuuDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 68px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                格納日
                                <asp:LinkButton runat="server" ID="btnKakunouDateUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakunouDateDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 68px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                発送日
                                <asp:LinkButton runat="server" ID="btnHassouDateUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnHassouDateDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <%--<td style="width: 70px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                管理表
                                <asp:LinkButton runat="server" ID="btnKanrihyouUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKanrihyouDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 70px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                送付状
                                <asp:LinkButton runat="server" ID="btnSoufujyouUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSoufujyouDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 70px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                報告表
                                <asp:LinkButton runat="server" ID="btnHoukokuhyouUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnHoukokuhyouDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>--%>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 960px;
                    height: 275px; border-left: solid 1px black; border-right: solid 1px black; border-bottom: solid 1px black;
                    overflow: hidden; margin-top: -1px;">
                    <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="width: 961px;">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblKanriNo" runat="server" Width="52px" Text='<%#Eval("kanri_no")%>'
                                        ToolTip='<%#Eval("kanri_no")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="56px" Height="24px" HorizontalAlign="Left" BorderColor="#999999"
                                    Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="kbn">
                                <ItemStyle Width="28px"  HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="hosyousyo_no">
                                <ItemStyle Width="76px"  HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblSesyuMei" runat="server" Width="276px" Text='<%#Eval("sesyu_mei")%>'
                                        ToolTip='<%#Eval("sesyu_mei")%>' Style="font-size: 11px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="278px"  HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="kameiten_cd">
                                <ItemStyle Width="76px" HorizontalAlign="Center" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblKameitenMei2" runat="server" Width="222px" Text='<%#Eval("kameiten_mei")%>'
                                        ToolTip='<%#Eval("kameiten_mei")%>' Style="font-size: 11px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;" ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxBusuu" runat="server" Width="30px" Style="font-size: 12px; text-align: center;"
                                        CssClass="codeNumber" MaxLength="1" Text='<%#Eval("kensa_hkks_busuu")%>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="48px" HorizontalAlign="Center" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="kakunou_date">
                                <ItemStyle Width="64px" HorizontalAlign="Left" BorderColor="#999999"  Font-Size="12px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="hassou_date">
                                <ItemStyle Width="64px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="kanrihyou_out_flg">
                                <ItemStyle Width="66px" HorizontalAlign="Center" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="souhujyou_out_flg">
                                <ItemStyle Width="66px" HorizontalAlign="Center" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="kensa_hkks_out_flg">
                                <ItemStyle Width="66px" HorizontalAlign="Center" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="souhu_tantousya" />
                        </Columns>
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <%--            <td>
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 230px;
                    height: 265px; border-bottom: solid 1px black; border-right: solid 1px black;
                    overflow: scroll; margin-top: -1px; overflow: hidden;">
                    <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="width: 2861px;
                        border-right: solid 1px gray; padding-right: 4px; margin-left: -1px;">
                        <Columns>
                            <asp:BoundField DataField="Columns3">
                                <ItemStyle Width="47px"  HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Columns4">
                                <ItemStyle  HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>--%>
            <td valign="top" style="width: 17px; height: 275px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 276px; width: 30px;
                    margin-left: -14px;" onscroll="fncScrollV();">
                    <table id="tblLeft" runat="server">
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <%--        <tr>
            <td style="height: 16px;">
            </td>
            <td style="width: 231px;">
                <div style="overflow: hidden; height: 18px; width: 231px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 248px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table id="tblRight" runat="server" style="width: 2860px;">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style="height: 16px; width: 17px;">
            </td>
        </tr>--%>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="text-align: center; width: 960px;
        margin-top: 20px;">
        <tr>
            <td>
                <asp:Button ID="btnKanriHyouExcelOutput" runat="server" Text="管理表EXCEL出力" Style="width: 180px;
                    padding-top: 2px; background-color: Green; font-weight:bold;" />
            </td>
            <td>
                <asp:Button ID="btnSoufuJyouPdfOutput" runat="server" Text="送付状PDF出力" Style="width: 180px;
                    padding-top: 2px; background-color: #FFC0CB; font-weight:bold;" />
            </td>
            <td>
                <asp:Button ID="btnHoukokusyoPdfOutput" runat="server" Text="報告書PDF出力" Style="width: 180px;
                    padding-top: 2px; background-color: #FFA500; font-weight:bold;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidScroll" runat="server" />
    <asp:HiddenField ID="hidCsvFlg" runat="server" />
    <asp:HiddenField ID="hidCsvOut" runat="server" Value="" />
    <asp:Button ID="btnCsv" runat="server" Style="display: none;" />
    <input type="button" id="btnShowModal" onclick="ShowModal();" Style="display: none;" />
</asp:Content>
