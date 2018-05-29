<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kameiten_tourokuryou.ascx.vb" Inherits="Itis.Earth.WebUI.kameiten_tourokuryou" %>


        <table cellpadding="1" class="mainTable" style="margin-top: 10px; width: 968px; text-align: left">
            <thead>
                <tr>
                    <th class="tableTitle" colspan="12" rowspan="1" style="text-align: left">
                        <asp:LinkButton ID="lbtnTouroku" runat="server" OnClick="lbtnTouroku_Click">登録料情報</asp:LinkButton><a id="titleText6" runat="server"></a>
                        <asp:Button ID="btnTouroku" runat="server" OnClick="btnTouroku_Click" style="display:none ;" Text="登録" />
                             <asp:Button ID="Button1" runat="server" Text="Button" style="display:none ; visibility:hidden;" OnClick="Button1_Click" />&nbsp;

                        <span id="titleInfobar6" runat="server" style="display:inline "></span>&nbsp;
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Text="請求先"></asp:Label>
                        &nbsp;<asp:Label ID="lblSeikyusaki" runat="server" Font-Bold="False" ForeColor="Red" Text=""></asp:Label>
                        <asp:Label ID="lblUriageKeijou" runat="server"></asp:Label></th>
                </tr>
            </thead>
            <tbody id="meisaiTbody" runat="server" style="display:none;">
                <tr style="border-right: #999999 1px solid; border-top: black 1px solid; font-weight: bold;
                   background-color: #ffffd9; text-align: center">
                    <td   style="width: 70px;  ">
                        登録日</td>
                    <td   style="width: 80px;  ">
                        請求</td>
                    <td   style="width: 123px;  ">
                        商品ｺｰﾄﾞ</td>
                    <td   style="width: 159px">
                        商品名</td>
                    <td   style="width: 80px">
                        実請求<br />
                        税抜価格</td>
                    <td   style="width: 60px">
                        消費税</td>
                    <td   style="width: 70px">
                        税込金額</td>
                    <td   style="width: 75px">
                        工務店請求<br />
                        税抜金額</td>
                    <td   style="width: 92px">
                        請求書発行日</td>
                    <td   style="width: 80px">
                        売上年月日</td>
                </tr>
                <tr>
                    <td style="width: 70px;  ">
                        <asp:TextBox ID="tbxAddDate" runat="server" MaxLength="10" Width="65px" CssClass = "codeNumber" ></asp:TextBox>
                    </td>
                    <td style="width: 80px;  ">
                        <asp:DropDownList ID="ddlSeikyuuUmu" runat="server" AutoPostBack="True" Width="88px">
                            <asp:ListItem Selected="True" Text="0：請求無し" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1：請求有り" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 123px;  ">
                        &nbsp;<asp:TextBox ID="tbxSyouhinCd" runat="server" CssClass="codeNumber" Text=""
                            Width="56px"></asp:TextBox>
                        <asp:Button ID="btnKansaku" runat="server" Text="検索" /></td>
                    <td style="width: 159px">
                        <%--<asp:TextBox ID="tbxSyouhinMei" runat="server" MaxLength="24" Width="110px"></asp:TextBox>--%>
                        <asp:Label ID="lblSyouhinMei" runat="server" Width="144px" Style="word-wrap: break-word; word-break: break-all;" ></asp:Label>
                    </td>
                    <td style="width: 80px">
                        <asp:TextBox ID="tbxZeinuki" runat="server" CssClass="kingaku" Width="64px" TabIndex="-1" ></asp:TextBox>
                    </td>
                    <td style="width: 60px; text-align: right">
                        <%--<asp:TextBox ID="tbxSyouhizei" runat="server" MaxLength="24" Width="120px"></asp:TextBox>--%>
                        <asp:Label ID="lblSyouhizei" runat="server" Font-Bold="False"></asp:Label></td>
                    <td  style="width: 70px; text-align: right; height: 38px;" >
                        <asp:Label ID="lblZeikomi" runat="server" Font-Bold="False" ></asp:Label>
                    </td>
                    <td style="width: 75px">
                        <asp:TextBox ID="tbxKoumutenSeikyuuGaku" runat="server" CssClass="kingaku" 
                            Width="64px" OnTextChanged="tbxKoumutenSeikyuuGaku_TextChanged" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td style="width: 92px">
                        <asp:TextBox ID="tbxSeikyuDate" runat="server" MaxLength="24" Width="65px" CssClass="codeNumber" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td style="width: 80px">
                        <asp:TextBox ID="tbxUriDate" runat="server" MaxLength="24" Width="65px"  CssClass="codeNumber" BorderWidth="0px" ReadOnly="True" BackColor="Transparent" TabIndex="-1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70px">
                        備考</td>
                    <td colspan="9">
                        <asp:TextBox ID="tbxBikou" runat="server" MaxLength="30" Width="332px"></asp:TextBox></td>
                </tr>

                <tr>
                    <th class="tableTitle" colspan="12" rowspan="1" style="border-bottom: silver 1px solid;
                        text-align: left">
                        <a id="titleText7" runat="server">販促品初期ツール料</a> &nbsp;&nbsp;<span id="titleInfobar7" runat="server"
                            style="display: none"></span>&nbsp;
                        <asp:Label ID="lblUriageKeijou1" runat="server"></asp:Label></th>
                </tr>

                <tr style="border-right: #999999 1px solid; border-top: black 1px solid; font-weight: bold;
                    background-color: #ffffd9; text-align: center">
                    <td   style="width: 70px; height: 29px;  ">
                        配送日</td>
                    <td   style="width: 80px; height: 29px;  ">
                        請求</td>
                    <td   style="width: 123px; height: 29px;  ">
                        商品ｺｰﾄﾞ</td>
                    <td   style="width: 159px; height: 29px">
                        商品名</td>
                    <td   style="width: 80px; height: 29px">
                        実請求<br />
                        税抜価格</td>
                    <td   style="width: 60px; height: 29px">
                        消費税</td>
                    <td   style="width: 70px; height: 29px">
                        税込金額</td>
                    <td rowspan="2" style="border-right: medium none; border-top: medium none; border-left: medium none;
                        width: 75px; border-bottom: 0px dotted; background-color: white">
                        <hr style="width: 64px; border-top-style: none; border-right-style: none; border-left-style: none;
                            background-color: transparent; border-bottom-style: none" />
                    </td>
                    <td   style="width: 92px; height: 29px">
                        請求書発行日</td>
                    <td   style="width: 80px; height: 29px">
                        売上年月日</td>
                </tr>
                <tr>
                    <td style="width: 70px;  height: 38px;">
                        <asp:TextBox ID="tbxAddDate1" runat="server" MaxLength="10" Width="65px"  CssClass="codeNumber"></asp:TextBox>
                    </td>
                    <td style="width: 80px;  height: 38px;">
                        <asp:DropDownList ID="ddlSeikyuuUmu1" runat="server" AutoPostBack="True" Width="88px">
                            <asp:ListItem Selected="True" Text="0：請求無し" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1：請求有り" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 123px;  height: 38px;">
                        &nbsp;<asp:TextBox ID="tbxSyouhinCd1" runat="server" CssClass="codeNumber" Text=""
                            Width="56px" OnTextChanged="tbxSyouhinCd1_TextChanged"></asp:TextBox>
                        <asp:Button ID="btnKansaku1" runat="server" Text="検索" /></td>
                    <td style="width: 159px; height: 38px;">
                        <%--<asp:TextBox ID="tbxSyouhinMei" runat="server" MaxLength="24" Width="110px"></asp:TextBox>--%>
                        <asp:Label ID="lblSyouhinMei1" runat="server" Style="word-break: break-all; word-wrap: break-word" Width="144px"></asp:Label></td>
                    <td style="width: 80px; height: 38px;">
                        <asp:TextBox ID="tbxZeinuki1" runat="server" CssClass="kingaku"  Width="64px" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td style="width: 60px; text-align: right; height: 38px;">
                        <%--<asp:TextBox ID="tbxSyouhizei" runat="server" MaxLength="24" Width="120px"></asp:TextBox>--%>
                        <asp:Label ID="lblSyouhizei1" runat="server" Font-Bold="False"></asp:Label></td>
                    <td style="width: 70px; text-align: right; height: 38px;">
                        <asp:Label ID="lblZeikomi1" runat="server" Font-Bold="False" ></asp:Label>
                    </td>
                    <td style="width: 92px; height: 38px;">
                        <asp:TextBox ID="tbxSeikyuDate1" runat="server" MaxLength="24" Width="65px"  CssClass="codeNumber" TabIndex="-1"></asp:TextBox>
                    </td>
                    <td style="width: 80px; height: 38px;">
                        <asp:TextBox ID="tbxUriDate1" runat="server" MaxLength="24" Width="65px"  CssClass="codeNumber" BorderWidth="0px" ReadOnly="True" BackColor="Transparent" TabIndex="-1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 70px; height: 7px;">
                        備考</td>
                    <td colspan="9" style="border-top-width: 0px; height: 7px;">
                        <asp:TextBox ID="tbxBikou1" runat="server" MaxLength="30" Width="332px"></asp:TextBox>
                        
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:HiddenField ID="hidHyoujunkakaku" runat="server" Value="0" />
        <asp:HiddenField ID="hidZeikbn" runat="server" />
        <asp:HiddenField ID="hidZeikbnTxt" runat="server" />
        <asp:HiddenField ID="hidHyoujunkakaku1" runat="server" Value="0" />
         <asp:HiddenField ID="hidZeikbn1" runat="server" />
        <asp:HiddenField ID="hidZeikbnTxt1" runat="server" />
        <asp:HiddenField ID="hidUpdTime" runat="server" />
        <asp:HiddenField ID="hidUpdTime1" runat="server" />  
        <asp:HiddenField ID="hidLastFocus" runat="server" />
        <asp:HiddenField ID="hidSeikyusaki" runat="server" />
        
            <asp:HiddenField ID="hidAutoKoumuFlg" runat="server" />
                   <asp:HiddenField ID="hidAutoJituFlg" runat="server" />     
