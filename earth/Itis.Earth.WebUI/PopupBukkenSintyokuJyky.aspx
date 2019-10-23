<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    CodeBehind="PopupBukkenSintyokuJyky.aspx.vb" Inherits="Itis.Earth.WebUI.PopupBukkenSintyokuJyky"
    Title="EARTH 物件進捗状況" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript">

         //ウィンドウサイズ変更
        try{
            window.resizeTo(750,390);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        } 
         
    </script>

    <%-- 画面タイトル --%>
    <div id="searchGrid" runat="server">
        <input type="hidden" id="HiddenGamenMode" runat="server" />
        <table>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 120px;">
                                物件進捗状況</th>
                            <th style="text-align: left;">
                                <input type="button" id="ButtonClose" runat="server" value="閉じる" onclick="window.close();" />
                            </th>
                            <th style="text-align: right; font-size: 11.5px;" width="400px">
                                <span id="SpanSyoriDateType" runat="server"> </span>
                            </th>
                            <th style="font-size: 12.5px; font-weight:normal;">
                                <asp:TextBox ID="TextSyoriDate" CssClass="readOnlyStyle" Style="width: 100px"
                                    ReadOnly="true" Text="" runat="server" TabIndex="-1" />
                            </th>
                        </tr>
                    </table>
                </td>
                
            </tr>
        </table>
        <br />
        <%-- 画面表示パラメータ --%>
        <table class="mainTable" cellpadding="0" cellspacing="0">
            <tr>
                <td class="koumokuMei">
                    区分</td>
                <th style="text-align: left; position:relative;left:5px">
                    <asp:Label runat="server" Width="160px">
                        <span runat="server" id="SpanKbn" style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
                <td class="koumokuMei">
                    番号</td>
                <th style="text-align: left; position:relative;left:5px">
                    <asp:Label runat="server" Width="100px">
                        <span runat="server" id="SpanBangou" style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
            </tr>
        </table>
        <br />
        <%-- 保証書依頼不可状況(前日までの状況) --%>
        <table class="mainTable" cellpadding="0" cellspacing="0" rules="all" bordercolor="gray">
            <tr class="shouhinTableTitle">
                <td colspan="5">
                    品質保証書依頼不可状況
                </td>
            </tr>
            <tr class="textCenter">
                <td class="koumokuMei">
                    解析完了</td>
                <td class="koumokuMei">
                    工事有無</td>
                <td class="koumokuMei">
                    工事完了</td>
                <td class="koumokuMei">
                    入金確認条件</td>
                <td class="koumokuMei">
                    入金状況</td>
                <!--<td class="koumokuMei">
                    建物検査</td>-->
            </tr>
            <tr class="textCenter" style="height:22px">
                <th id="CellKaisekiKanry" runat="server">
                    <asp:Label runat="server" Width="90px">
                        <span runat="server" id="SpanKaisekiKanry" style="font-size:13px; font-weight:normal;"></span>
                        <input type="hidden" id="HiddenKaisekiKanry" runat="server" />
                    </asp:Label>
                </th>
                <th>
                    <asp:Label runat="server" Width="90px">
                        <span runat="server" id="SpanKojUmu" style="font-size:13px; font-weight:normal;"></span>
                        <input type="hidden" id="HiddenKojUmu" runat="server" />
                    </asp:Label>
                </th>
                <th id="CellKojKanry" runat="server">
                    <asp:Label runat="server" Width="135px">
                        <span runat="server" id="SpanKojKanry" style="font-size:13px; font-weight:normal;"></span>
                        <input type="hidden" id="HiddenKojKanry" runat="server" />
                    </asp:Label>
                </th>
                <th>
                    <asp:Label runat="server" Width="155px">
                        <span runat="server" id="SpanNyuukinKakuninJyouken" style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
                <th id="CellNyuukinJyky" runat="server">
                    <asp:Label runat="server" Width="105px">
                        <span runat="server" id="SpanNyuukinJyky" style="font-size:13px; font-weight:normal;"></span>
                        <input type="hidden" id="HiddenNyuukinJyky" runat="server" />
                    </asp:Label>
                </th>
                <!--<th id="CellKasi" runat="server">
                    <asp:Label runat="server" Width="105px">
                        <span runat="server" id="SpanKasi" style="font-size:13px; font-weight:normal;"></span>
                        <input type="hidden" id="HiddenKasi" runat="server" />
                    </asp:Label>
                </th>-->
            </tr>
        </table>
        <br />
        <%-- 地盤モール公開状況 --%>
        <table class="mainTable" cellpadding="0" cellspacing="0" rules="all" bordercolor="gray">
            <tr class="shouhinTableTitle">
                <td colspan="4">
                    地盤モール公開状況
                </td>
            </tr>
            <tr class="textCenter">
                <td class="koumokuMei">
                    解析</td>
                <td class="koumokuMei">
                    改良工事</td>
                <td class="koumokuMei">
                    ご入金</td>
                <td class="koumokuMei">
                    発行依頼</td>
            </tr>
            <tr class="textCenter" style="height:22px">
                <th id="CellKaiseki" runat="server">
                    <asp:Label runat="server" Width="100px">
                        <span runat="server" id="SpanKaiseki" style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
                <th>
                    <asp:Label runat="server" Width="100px">
                        <span runat="server" id="SpanKairyoKoji" style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
                <th id="CellNyuukin" runat="server">
                    <asp:Label runat="server" Width="100px">
                        <span runat="server" id="SpanNyuukin" style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
                <th>
                    <asp:Label runat="server" Width="100px">
                        <span runat="server" id="SpanHakkouIrai" style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
            </tr>
        </table>        
        <br />
        <asp:Label runat="server" Width="100px">
                        <span runat="server" id="SpanKojKanryNaiyou" style="font-size:13px; font-weight:normal;"></span>
        </asp:Label>
        <br /><br />
        <asp:Label runat="server" Width="100px">
                        <span runat="server" id="SpanNyuukinJykyNaiyou" style="font-size:13px; font-weight:normal;"></span>
        </asp:Label>
        
        <%-- 保険申請状況 --%>
        <!--<table class="mainTable" cellpadding="0" cellspacing="0">
            <tr class="shouhinTableTitle">
                <td colspan="4">
                    付保証明状況</td>
            </tr>
            <tr class="textCenter">
                <td class="koumokuMei">
                    付保証明書FLG</td>
                <th>
                    <asp:Label runat="server" Width="90px">
                        <span runat="server" id="SpanFuhoSyomeisyoFlg" Style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
                <td class="koumokuMei">
                    付保証明書発送日</td>
                <th>
                    <asp:Label runat="server" Width="100px">
                        <span runat="server" id="SpanFuhoSyomeisyoHassoDate" Style="font-size:13px; font-weight:normal;"></span>
                    </asp:Label>
                </th>
            </tr>
        </table>-->
    </div>
</asp:Content>



