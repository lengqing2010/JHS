<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kameiten_bikou.ascx.vb" Inherits="Itis.Earth.WebUI.kameiten_bikou" %>


<table cellpadding="1" class="mainTable" style="margin-top: 10px; width: 968px; text-align: left">
    <thead>
        <tr>
            <th class="tableTitle" colspan="12" rowspan="1" style="text-align: left">
              <asp:LinkButton id="lbtnBikou" runat="server">îıçl</asp:LinkButton> &nbsp; <span id="titleInfobar" runat="server"    ></span>
            </th>
        </tr>
    </thead>
    <tbody id="meisaiTbody" runat="server" style="display:inline  ">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="gridviewTableHeader" style="margin-top: 10px;
                    margin-left: 10px; ">
                    <tr>
                        <td style="border-left: black 1px solid; width: 120px;">
                            éÌï 
                        </td>
                        <td style="width: 259px">
                            éÌï ñº</td>
                        <td style="width: 397px">
                            ì‡óe</td>
                    </tr>
                </table>
                
                <div id="Div1" runat="server" style=" overflow:scroll  ;border-right: #999999 1px inset; border-top: #999999 1px inset;
                    left: 0px; margin-bottom: 10px; margin-left: 10px; overflow: auto; border-left: #999999 1px inset;
                    width: 816px; border-bottom: #999999 1px inset; top: 0px; height: 248px">
                    <asp:GridView ID="grdBeikou" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderWidth="0px" CellPadding="0" CssClass="tableMeiSai" ShowHeader="False" Width="" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxBikousyubetu" runat="server" CssClass = "codeNumber" Text='<%# eval("bikou_syubetu") %>' Width="46px" AutoPostBack ="true" OnTextChanged="tbxBikousyubetu_TextChanged"></asp:TextBox>

                                        <asp:Button ID="btnKensaku1" runat="server" Text="åüçı"    />
                                    <asp:HiddenField ID="hidNyuuryokuNo" runat="server" Value = '<%# eval("nyuuryoku_no") %>' /> 
                                </ItemTemplate>
                                <ItemStyle  BorderColor="#999999" HorizontalAlign="Left" Width="120px"   CssClass="hissu"/>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                
                                <ItemTemplate>
                                    <asp:Label ID="lblSyubetumei" runat="server"  Text='<%# eval("meisyou") %>' Width="240"></asp:Label> 
                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="260px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxNaiyou" runat="server" Text='<%# eval("naiyou") %>' Width="250px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="270px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnTouroku" runat="server" Text="ìoò^" OnClick="Touroku_Click" CommandArgument="" Width="50" /> 
                                    <asp:Button ID="btnSakujyo" runat="server" Text="çÌèú" OnClientClick ="if (!confirm('çÌèúÇµÇ‹Ç∑ÅB')){return false;}" OnClick="Sakujyo_Click" CommandArgument="" Width="50"/> 

                   
                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    
                     <asp:GridView ID="grdAddBeikou" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderWidth="0px" CellPadding="0" CssClass="tableMeiSai" ShowHeader="False" Width="" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxAddBikousyubetu" runat="server" CssClass = "codeNumber" Text='' Width="46px" AutoPostBack ="true" OnTextChanged="tbxBikousyubetu_TextChanged2"></asp:TextBox>

                                        <asp:Button ID="btnKensaku2" runat="server" Text="åüçı"   />
                                    <asp:HiddenField ID="hidNyuuryokuNo" runat="server" Value = '' /> 
                                </ItemTemplate>
                                <ItemStyle  BorderColor="#999999" HorizontalAlign="Left" Width="120px" CssClass="hissu"/>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                
                                <ItemTemplate>
                                    <asp:Label ID="lblAddSyubetumei" runat="server"  Text='' Width="240"></asp:Label> 
                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="260px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxAddNaiyou" runat="server" Text='' Width="250px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="270px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnSinki" runat="server" Text="êVãK" OnClick="Sinki_Click" CommandArgument="" Width="50" /> 
                                     <asp:Button ID="btn" runat="server" Text="Å@Å@" Width="50" Visible="false"  /> 

                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    
                    <%--
                    <asp:GridView ID="grdAddBeikou" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderWidth="0px" CellPadding="0" CssClass="tableMeiSai" ShowHeader="False" Width="820">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxAddBikousyubetu" runat="server"  CssClass = "codeNumber" Width="46px" AutoPostBack ="true" OnTextChanged="tbxBikousyubetu_TextChanged2"></asp:TextBox>
                                    <asp:Button ID="btnKensaku2" runat="server" Text="åüçı"   />

                                                   
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFE4E1 " BorderColor="#999999" HorizontalAlign="Left" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                          <asp:Label ID="lblAddSyubetumei" runat="server" Text="&nbsp;"  Width="240"></asp:Label>   
                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="260px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                              <asp:TextBox ID="tbxAddNaiyou" runat="server"  Width="190px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="270px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                                        <asp:Button ID="btnSinki" runat="server" Text="êVãK" OnClick="Sinki_Click" CommandArgument="" /> 
                                </ItemTemplate>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" Width="125px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                </div>
                <span id="ctl00_ContentPlaceHolder1_UpdatePanelA">
                    <div>
                        <asp:HiddenField ID="hidMaxDate" runat="server" />
                        &nbsp;
                        <asp:TextBox ID="TextBoxDisplayNone" runat="server" style="display:none;"></asp:TextBox></div>
                </span>
            </td>
        </tr>
    </tbody>
</table>

