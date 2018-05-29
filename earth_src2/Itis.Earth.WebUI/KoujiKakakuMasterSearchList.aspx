<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="KoujiKakakuMasterSearchList.aspx.vb" Inherits="Itis.Earth.WebUI.KoujiKakakuMaster"
    Title="工事価格マスタ照会" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

  <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
        
        function fncPopupKobetuSettei(strSyobetu,strCd,strSyouhin,strKojKaisya)
        {
            if((strSyobetu == '')&&(strCd == '')&&(strSyouhin == '')&&(strKojKaisya == ''))
            {
                window.open('KoujiKakakuMasterKobetuSettei.aspx','KobetuSettei','top=0,left=0,width=1000,height=400,menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes');
            }
            else
            {
                var sendSearchTerms = strSyobetu + '$$$' + strCd + '$$$' + strSyouhin + '$$$' + strKojKaisya;
                window.open('KoujiKakakuMasterKobetuSettei.aspx?sendSearchTerms='+sendSearchTerms,'KobetuSettei','top=0,left=0,width=1000,height=400,menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes');
            }
        }
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
            <th style="width: 160px;">
                工事価格マスタ照会
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th style="width: 100px;">
                <asp:Button ID="btnCsvUpload" runat="server" Text="CSV取込" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th style="width: 100px;">
                <asp:Button ID="btnTouroku" runat="server" Text="新規登録" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td colspan="5" rowspan="1">
            </td>
        </tr>
    </table>
    <table style="width: 975px; text-align: left;" class="mainTable paddinNarrow" cellpadding="1">
        <tr >
            <th class="tableTitle" style="width: 110px; height: 20px;">
                <table border="0" cellpadding="0" cellspacing="0" style="border: none; width: 110px;">
                    <tr>
                        <td style="border: none; width: 60px;">
                            検索条件
                        </td>
                        <td style="border: none;">
                            <asp:Button ID="btnClear" runat="server" Text="クリア" Style="padding-top: 2px;" />
                        </td>
                    </tr>
                </table>
            </th>
            <th class="tableTitle" style="width: 90px;">
            </th>
            <th class="tableTitle" style="width: 260px;">
            </th>
            <th class="tableTitle" style="width: 110px;">
            </th>
            <th class="tableTitle">
            </th>
        </tr>
        <tr style="height: 30px;">
            <td style="height: 25px; background-color: #ccffff;">
                相手先コード
            </td>
            <td style="height: 25px; border-right: 0px;">
                <asp:DropDownList ID="ddlAiteSakiSyubetu" runat="server" Style="width: 85px;">
                </asp:DropDownList>
            </td>
            <td colspan="3" style="height: 25px; border-left: 0px;">
                <div style="width: 600px; height: 25px;">
                    <div runat="server" id="divAitesaki" style="">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="border: none;">
                                    <asp:TextBox ID="tbxAiteSakiCdFrom" runat="server" Text="" Style="width: 40px;" MaxLength="5"
                                        CssClass="codeNumber"></asp:TextBox>
                                    <asp:Button ID="btnAiteSakiCdFrom" runat="server" Text="検索" Style="width: 32px; padding-top: 2px;"
                                        OnClientClick="return fncAiteSakiSearch('1');" />
                                    <asp:TextBox ID="tbxAiteSakiMeiFrom" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                        Style="width: 260px;"></asp:TextBox>
                                </td>
                                <td style="border: none;">
                                    <div id="divAitesakiTo" runat="server">
                                        <asp:Label ID="lblFromTo" runat="server" Text="～" Style="margin-left: 5px;"></asp:Label>
                                        <asp:TextBox ID="tbxAiteSakiCdTo" runat="server" Text="" Style="width: 40px;" MaxLength="5"
                                            CssClass="codeNumber"></asp:TextBox>
                                        <asp:Button ID="btnAiteSakiCdTo" runat="server" Text="検索" Style="width: 32px; padding-top: 2px;"
                                            OnClientClick="return fncAiteSakiSearch('2');" />
                                        <asp:TextBox ID="tbxAiteSakiMeiTo" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                                            Style="width: 260px;"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td style="background-color: #ccffff;">
                商品コード
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlSyouhinCd" runat="server" Style="width: 350px;">
                </asp:DropDownList>
            </td>
            <td style="background-color: #ccffff;">
                工事会社</td>
            <td>
                <asp:TextBox ID="tbxKojKaisyaCd" runat="server" Text="" Style="width: 60px;" MaxLength="6"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="検索" Style="width: 32px; padding-top: 2px;"
                    OnClientClick="return fncKojKaisyaSearch();" />
                <asp:TextBox ID="tbxKojKaisyaMei" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
                    Style="width: 242px;"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td colspan="5" style="height: 20px; text-align: center;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="border: none;">
                            <asp:Label ID="lblJyouken" runat="server" Text="検索上限件数" Style=""></asp:Label>
                            <asp:DropDownList ID="ddlKensakuCount" runat="server" Style="width: 70px;">
                                <asp:ListItem Value="10" Text="10件"></asp:ListItem>
                                <asp:ListItem Value="100" Text="100件" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="max" Text="無制限"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnKensaku" runat="server" Text="検索実行" Style="width: 60px; height: 25px;
                                padding-top: 2px;" />
                            <asp:CheckBox ID="chkKensakuTaisyouGai" runat="server" Text="取消は対象外" Checked="true" />
                            <asp:CheckBox ID="chkAitesakiTaisyouGai" runat="server" Text="取消相手先は対象外" Checked="true" />&nbsp;
                        </td>
                        <td style="border: none; vertical-align: bottom;">
                            <div id="divSitei" runat="server" style="display: none;">
                                &nbsp;</div>
                        </td>
                        <td style="border: none;">
                            <div id="divCsvOutput" runat="server">
                                <asp:Button ID="btnCsvOutput" runat="server" Text="CSV出力" Style="width: 65px; height: 25px;
                                    padding-top: 2px;" />
                            </div>
                        </td>
                        <td style="border: none; vertical-align: bottom;">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="width: 65px;">
                検索結果：&nbsp;
            </td>
            <td>
                <asp:Label runat="server" ID="lblCount"></asp:Label>
            </td>
            <td>
                &nbsp;件
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" style="width: 522px; overflow-y: hidden; overflow-x: hidden;">
                    <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffd9;
                        font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 75px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-left: solid 1px black; border-right: solid 1px gray; text-align: center;">
                                先種別
                                <asp:LinkButton runat="server" ID="btnAitesakiSyubetuUp" Text="▲" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAitesakiSyubetuDown" Text="▼" Height="14px"
                                    TabIndex="-1" Visible="false" Font-Underline="false" Style="margin-left: -8px;"
                                    OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                先コード
                                <asp:LinkButton runat="server" ID="btnAitesakiCdUp" Text="▲" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAitesakiCdDown" Text="▼" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" Style="margin-left: -8px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 151px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                相手先名
                                <asp:LinkButton runat="server" ID="btnAitesakiMeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAitesakiMeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" Style="margin-left: -8px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 70px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                商品
                                <asp:LinkButton runat="server" ID="btnSyouhinCdUp" Text="▲" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSyouhinCdDown" Text="▼" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" Style="margin-left: -8px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 141px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                商品名
                                <asp:LinkButton runat="server" ID="btntSyouhinMeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btntSyouhinMeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" Style="margin-left: -8px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 445px; overflow-y: hidden; overflow-x: hidden;
                    border-right: solid 1px black;">
                    <table id="tableHeadRigth" border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffd9;
                        font-weight: bold; width: 772px;">
                        <tr style="height: 24px;">
                         <td style="width: 96px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                工コード
                                <asp:LinkButton runat="server" ID="btnKojCdUp" Text="▲" Height="14px"
                                    TabIndex="-1" Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKojCdDown" Text="▼" Height="14px"
                                    TabIndex="-1" Visible="false" Font-Underline="false" Style="margin-left: -8px;"
                                    OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 135px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                工事会社名
                                <asp:LinkButton runat="server" ID="btnKojKaisyaUp" Text="▲" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKojKaisyaDown" Text="▼" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" Style="margin-left: -8px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 65px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                取消
                                <asp:LinkButton runat="server" ID="btnTorikesiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTorikesiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" Style="margin-left: -8px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 166px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                売上金額(税抜)
                                <asp:LinkButton runat="server" ID="btnUriGakuUp" Text="▲" Height="14px"
                                    TabIndex="-1" Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnUriGakuDown" Text="▼" Height="14px"
                                    TabIndex="-1" Visible="false" Font-Underline="false" Style="margin-left: -8px;"
                                    OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 150px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                工事会社請求有無
                                <asp:LinkButton runat="server" ID="btnKojUmuUp" Text="▲" Height="14px"
                                    TabIndex="-1" Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKojUmuDown" Text="▼"
                                    Height="14px" TabIndex="-1" Visible="false" Font-Underline="false" Style="margin-left: -8px;"
                                    OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 136px; border-top: solid 1px black; border-bottom: solid 1px black;
                                border-right: solid 1px gray; text-align: center;">
                                請求有無
                                <asp:LinkButton runat="server" ID="btnSeikyuUmuUp" Text="▲" Height="14px" TabIndex="-1"
                                    Visible="false" Font-Underline="false" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSeikyuUmuDown" Text="▼" Height="14px"
                                    TabIndex="-1" Visible="false" Font-Underline="false" Style="margin-left: -8px;"
                                    OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                           
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 521px;
                    height: 288px; overflow-y: hidden; overflow-x: hidden; margin-top: -1px; border-left: 1px solid black;
                    border-bottom: 1px solid black;">
                    <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" ShowHeader="False" CellPadding="0">
                        <Columns>
                            <asp:BoundField DataField="aitesaki">
                                <ItemStyle Width="70px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="aitesaki_cd">
                                <ItemStyle Width="76px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAitesakiMei" Text='<%#Eval("aitesaki_mei").ToString%>'
                                        ToolTip='<%#Eval("aitesaki_mei").ToString%>' Width="145px" Style="white-space: normal;
                                        overflow: hidden;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="147px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="syouhin_cd">
                                <ItemStyle Width="66px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSyouhinMei" Text='<%#Eval("syouhin_mei").ToString%>'
                                        ToolTip='<%#Eval("syouhin_mei").ToString%>' Width="135px" Style="white-space: normal;
                                        overflow: hidden;"></asp:Label>
                                    <asp:HiddenField ID="hidCd" Value ='<%#Eval("koj_cd").ToString%>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="137px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td>
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 445px;
                    height: 288px; overflow-x: hidden; overflow-y: hidden; margin-top: -1px; border-right: 1px solid black;
                    border-bottom: 1px solid black;">
                    <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CssClass="tableMeiSai" ShowHeader="False" CellPadding="0" Width="772px">
                        <Columns>
                            <asp:BoundField DataField="koj_cd">
                                <ItemStyle Width="95px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbltyskaisyaMei" Text='<%#Eval("tys_kaisya_mei").ToString%>'
                                        ToolTip='<%#Eval("tys_kaisya_mei").ToString%>' Width="130px" Style="white-space: normal;
                                        overflow: hidden;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="134px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="torikesi">
                                <ItemStyle Width="65px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblgaku"  Text='<%#Eval("uri_gaku").ToString%>'
                                        ToolTip='<%#Eval("uri_gaku").ToString%>' Width="164px" Style="white-space: normal;
                                        overflow: hidden;"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="166px" Height="31px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:TemplateField>
                            
  
                            <asp:BoundField DataField="kojumu">
                                <ItemStyle Width="154px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="seikyuumu">
                                <ItemStyle Width="139px" Height="31px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle ForeColor="White" />
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 17px; height: 288px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 288px; width: 30px;
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
        <tr>
            <td style="height: 16px;">
            </td>
            <td style="width: 445px">
                <div style="overflow-x: hidden; height: 18px; width: 445px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 461px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 771px;">
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
        </tr>
    </table>
    <asp:HiddenField ID="hidScroll" runat="server" />
    <asp:HiddenField ID="hidCsvOut" runat="server" Value="" />
</asp:Content>
