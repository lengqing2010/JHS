<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="KameitenMasterErrorDetails.aspx.vb" Inherits="Itis.Earth.WebUI.KameitenMasterErrorDetails"
    Title="加盟店情報一括取込エラー確認" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
        //window名付与
        var objWin = window;
        objWin.name = "earthMainWindow"
        initPage(); //画面初期設定
    </script>

    <table style="text-align: left; width: 960px;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tr>
        </tr>
        <tr>
            <th style="width: 400px;">
                加盟店情報一括取込エラー確認
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px;" />
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnCsvOutput" runat="server" Text="CSV出力" Style="height: 25px;" />
            </th>
            <th>
            </th>
        </tr>
        <tr>
            <td colspan="5" rowspan="1">
            </td>
        </tr>
    </table>
    <table style="width: 400px; text-align: left;" class="mainTable paddinNarrow" cellpadding="1">
        <tr>
            <td style="width: 120px; height: 25px; background-color: #ccffff;">
                取込日時
            </td>
            <td style="height: 25px">
                <asp:Label ID="lblSyoriDate" runat="server" Text="" Width="280px" Style="padding-left: 4px;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px; height: 25px; background-color: #ccffff;">
                取込ファイル名
            </td>
            <td style="height: 25px">
                <asp:Label ID="lblFileMei" runat="server" Text="" Width="280px" Style="padding-left: 4px;
                    white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="width: 65px; height: 14px;">
                検索結果：&nbsp;
            </td>
            <td style="height: 14px">
                <asp:Label runat="server" ID="lblCount"></asp:Label>&nbsp;件
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" runat="server" style="width: 410px; overflow-y: hidden; overflow-x: hidden;
                    border-left: solid 1px black;">
                    <table style="width: 410px; border-bottom: solid 1px black; text-align: left;" class="gridviewTableHeader"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50px;">
                                <asp:Label ID="Label1" runat="server" Width="50px" Text="行番号" ToolTip="行番号" Style="white-space: nowrap;
                                    overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label2" runat="server" Width="40px" Text="区分" ToolTip="区分" Style="white-space: nowrap;
                                    overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="Label3" runat="server" Width="80px" Text="加盟店コード" ToolTip="加盟店コード"
                                    Style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label4" runat="server" Width="40px" Text="取消" ToolTip="取消" Style="white-space: nowrap;
                                    overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="Label5" runat="server" Width="80px" Text="発注停止フラグ" ToolTip="発注停止フラグ"
                                    Style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                            </td>
                            <td style="border-right: solid 1px gray;">
                                <asp:Label ID="Label6" runat="server" Text="加盟店名1" ToolTip="加盟店名1" Style="white-space: nowrap;
                                    overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 548px; overflow-y: hidden; overflow-x: hidden;
                    border-right: solid 1px black;">
                    <table style="width: 12400px; border-bottom: solid 1px black;" class="gridviewTableHeader"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 78px;">
                                店名カナ1</td>
                            <td style="width: 78px">
                                加盟店名2</td>
                            <td style="width: 80px">
                                店名カナ2</td>
                            <td style="width: 93px">
                                ビルダーNO</td>
                            <td style="width: 75px">
                                ビルダー名</td>
                            <td style="width: 75px">
                                系列コード</td>
                            <td style="width: 75px">
                                系列名</td>
                            <td style="width: 93px">
                                営業所コード</td>
                            <td style="width: 93px">
                                営業所名</td>
                            <td style="width: 93px">
                                正式名称</td>
                            <td style="width: 93px">
                                正式名称2</td>
                            <td style="width: 111px">
                                都道府県コード</td>
                            <td style="width: 93px">
                                都道府県名</td>
                            <td style="width: 60px">
                                年間棟数</td>
                            <td style="width: 94px">
                                付保証明FLG</td>
                            <td style="width: 139px">
                                付保証明書開始年月</td>
                            <td style="width: 93px">
                                営業担当者</td>
                            <td style="width: 111px">
                                営業担当者名</td>
                            <td style="width: 111px">
                                旧営業担当者</td>
                            <td style="width: 111px">
                                旧営業担当者名</td>
                            <td style="width: 90px">
                                工事売上種別</td>
                            <td style="width: 100px">
                                工事売上種別名</td>
                            <td style="width: 90px">
                                JIO先フラグ</td>
                            <td style="width: 111px">
                                解約払戻価格</td>
                            <td style="width: 139px">
                                棟区分1商品コード</td>
                            <td style="width: 111px">
                                棟区分1商品名</td>
                            <td style="width: 139px">
                                棟区分2商品コード</td>
                            <td style="width: 111px">
                                棟区分2商品名</td>
                            <td style="width: 139px">
                                棟区分3商品コード</td>
                            <td style="width: 111px">
                                棟区分3商品名</td>
                            <td style="width: 111px">
                                調査請求先区分</td>
                            <td style="width: 111px">
                                調査請求先コード</td>
                            <td style="width: 111px">
                                調査請求先枝番</td>
                            <td style="width: 111px">
                                調査請求先名</td>
                            <td style="width: 111px">
                                工事請求先区分</td>
                            <td style="width: 111px">
                                工事請求先コード</td>
                            <td style="width: 111px">
                                工事請求先枝番</td>
                            <td style="width: 111px">
                                工事請求先名</td>
                            <td style="width: 139px">
                                販促品請求先区分</td>
                            <td style="width: 139px">
                                販促品請求先コード</td>
                            <td style="width: 139px">
                                販促品請求先枝番</td>
                            <td style="width: 139px">
                                販促品請求先名</td>
                            <td style="width: 139px">
                                建物検査請求先区分</td>
                            <td style="width: 139px">
                                建物検査請求先コード</td>
                            <td style="width: 139px">
                                建物検査請求先枝番</td>
                            <td style="width: 139px">
                                建物検査請求先名</td>
                            <td style="width: 111px">
                                請求先区分5</td>
                            <td style="width: 111px">
                                請求先コード5</td>
                            <td style="width: 111px">
                                請求先枝番5</td>
                            <td style="width: 93px">
                                請求先5名</td>
                            <td style="width: 111px">
                                請求先区分6</td>
                            <td style="width: 111px">
                                請求先コード6</td>
                            <td style="width: 111px">
                                請求先枝番6</td>
                            <td style="width: 93px">
                                請求先6名</td>
                            <td style="width: 111px">
                                請求先区分7</td>
                            <td style="width: 111px">
                                請求先コード7</td>
                            <td style="width: 111px">
                                請求先枝番7</td>
                            <td style="width: 93px">
                                請求先7名</td>
                            <td style="width: 60px">
                                保証期間</td>
                            <td style="width: 100px">
                                保証書発行有無</td>
                            <td style="width: 111px">
                                入金確認条件</td>
                            <td style="width: 111px">
                                入金確認覚書</td>
                            <td style="width: 112px">
                                調査見積書FLG</td>
                            <td style="width: 94px">
                                発注書FLG</td>
                            <td style="width: 75px">
                                郵便番号</td>
                            <td style="width: 93px">
                                住所1</td>
                            <td style="width: 93px">
                                住所2</td>
                            <td style="width: 93px">
                                所在地コード</td>
                            <td style="width: 93px">
                                所在地名</td>
                            <td style="width: 75px">
                                部署名</td>
                            <td style="width: 75px">
                                代表者名</td>
                            <td style="width: 93px">
                                電話番号</td>
                            <td style="width: 94px">
                                FAX番号</td>
                            <td style="width: 93px">
                                申込担当者</td>
                            <td style="width: 75px">
                                備考1</td>
                            <td style="width: 75px">
                                備考2</td>
                            <td style="width: 135px">
                                登録日</td>
                            <td style="width: 60px">
                                請求有無</td>
                            <td style="width: 70px">
                                商品コード</td>
                            <td style="width: 135px">
                                商品名</td>
                            <td style="width: 80px">
                                売上金額</td>
                            <td style="width: 100px">
                                工務店請求金額</td>
                            <td style="width: 135px">
                                請求書発行日</td>
                            <td style="width: 135px">
                                売上年月日</td>
                            <td style="width: 140px">
                                備考</td>
                            <td style="width: 135px">
                                加盟店_更新日時</td>
                            <td style="width: 135px">
                                多棟割引_更新日時1</td>
                            <td style="width: 135px">
                                多棟割引_更新日時2</td>
                            <td style="width: 135px">
                                多棟割引_更新日時3</td>
                            <td style="width: 135px">
                                加盟店住所_更新日時</td>
                            <td style="width: 111px">
                                追加_備考種別①</td>
                            <td style="width: 120px">
                                追加_備考種別①名</td>
                            <td style="width: 111px">
                                追加_備考種別②</td>
                            <td style="width: 120px">
                                追加_備考種別②名</td>
                            <td style="width: 111px">
                                追加_備考種別③</td>
                            <td style="width: 120px">
                                追加_備考種別③名</td>
                            <td style="width: 111px">
                                追加_備考種別④</td>
                            <td style="width: 120px">
                                追加_備考種別④名</td>
                            <td style="width: 111px">
                                追加_備考種別⑤</td>
                            <td style="width: 120px">
                                追加_備考種別⑤名</td>
                            <td style="width: 93px">
                                追加_内容①</td>
                            <td style="width: 93px">
                                追加_内容②</td>
                            <td style="width: 93px">
                                追加_内容③</td>
                            <td style="width: 93px">
                                追加_内容④</td>
                            <td style="width: 93px">
                                追加_内容⑤</td>
                            <td style="width: 139px">
                                登録ログインユーザID</td>
                            <td style="width: 93px">
                                登録日時</td>
                            <td style="width: 139px">
                                更新ログインユーザID</td>
                            <td style="">
                                更新日時</td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 410px;">
                <div id="divMeisaiLeft" runat="server" onmousewheel="wheel();" style="width: 410px;
                    height: 221px; overflow-y: hidden; overflow-x: hidden; border-left: solid 1px black;
                    margin-top: -1px; border-bottom: solid 1px black;">
                    <asp:GridView ID="grdMeisaiLeft" runat="server" ShowHeader="False" CssClass="tableMeiSai"
                        CellPadding="0" AutoGenerateColumns="False" Width="410px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="51px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblGyou_no" runat="server" Width="51px" Text='<%#Eval("gyou_no").ToString%>'
                                        ToolTip='<%#Eval("gyou_no").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="42px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblKbn" runat="server" Width="42px" Text='<%#Eval("kbn").ToString%>'
                                        ToolTip='<%#Eval("kbn").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblKameiten_cd" runat="server" Width="82px" Text='<%#Eval("kameiten_cd").ToString%>'
                                        ToolTip='<%#Eval("kameiten_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="42px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="42px" Text='<%#Eval("torikesi").ToString%>'
                                        ToolTip='<%#Eval("torikesi").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHattyuu_teisi_flg" runat="server" Width="82px" Text='<%#Eval("hattyuu_teisi_flg").ToString%>'
                                        ToolTip='<%#Eval("hattyuu_teisi_flg").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="80px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblKameiten_mei1" runat="server" Width="80px" Text='<%#Eval("kameiten_mei1").ToString%>'
                                        ToolTip='<%#Eval("kameiten_mei1").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#CCFFFF" />
                        <RowStyle Height="22px" BorderColor="#999999" />
                    </asp:GridView>
                </div>
            </td>
            <td style="vertical-align: top; width: 548px;">
                <div id="divMeisaiRight" runat="server" onmousewheel="wheel();" style="width: 548px;
                    height: 221px; overflow-y: hidden; overflow-x: hidden; border-right: solid 1px black;
                    margin-top: -1px; border-bottom: solid 1px black;">
                    <asp:GridView ID="grdMeisaiRight" runat="server" ShowHeader="False" CssClass="tableMeiSai"
                        CellPadding="0" AutoGenerateColumns="False" Width="12400px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="79px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTenmei_kana1" runat="server" Width="79px" Text='<%#Eval("tenmei_kana1").ToString%>'
                                        ToolTip='<%#Eval("tenmei_kana1").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="80px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblKameiten_mei2" runat="server" Width="80px" Text='<%#Eval("kameiten_mei2").ToString%>'
                                        ToolTip='<%#Eval("kameiten_mei2").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="tenmei_kana2" runat="server" Width="82px" Text='<%#Eval("tenmei_kana2").ToString%>'
                                        ToolTip='<%#Eval("tenmei_kana2").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="builder_no" runat="server" Width="95px" Text='<%#Eval("builder_no").ToString%>'
                                        ToolTip='<%#Eval("builder_no").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="77px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="builder_mei" runat="server" Width="77px" Text='<%#Eval("builder_mei").ToString%>'
                                        ToolTip='<%#Eval("builder_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="77px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="keiretu_cd" runat="server" Width="77px" Text='<%#Eval("keiretu_cd").ToString%>'
                                        ToolTip='<%#Eval("keiretu_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="77px" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="keiretu_mei" runat="server" Width="77px" Text='<%#Eval("keiretu_mei").ToString%>'
                                        ToolTip='<%#Eval("keiretu_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="eigyousyo_cd" runat="server" Width="95px" Text='<%#Eval("eigyousyo_cd").ToString%>'
                                        ToolTip='<%#Eval("eigyousyo_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="eigyousyo_mei" runat="server" Width="95px" Text='<%#Eval("eigyousyo_mei").ToString%>'
                                        ToolTip='<%#Eval("eigyousyo_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="kameiten_seisiki_mei" runat="server" Width="95px" Text='<%#Eval("kameiten_seisiki_mei").ToString%>'
                                        ToolTip='<%#Eval("kameiten_seisiki_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="kameiten_seisiki_mei_kana" runat="server" Width="95px" Text='<%#Eval("kameiten_seisiki_mei_kana").ToString%>'
                                        ToolTip='<%#Eval("kameiten_seisiki_mei_kana").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="todouhuken_cd" runat="server" Width="113px" Text='<%#Eval("todouhuken_cd").ToString%>'
                                        ToolTip='<%#Eval("todouhuken_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="todouhuken_mei" runat="server" Width="95px" Text='<%#Eval("todouhuken_mei").ToString%>'
                                        ToolTip='<%#Eval("todouhuken_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="62" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="nenkan_tousuu" runat="server" Width="60px" Text='<%#Eval("nenkan_tousuu").ToString%>'
                                        ToolTip='<%#Eval("nenkan_tousuu").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="96px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="fuho_syoumeisyo_flg" runat="server" Width="96px" Text='<%#Eval("fuho_syoumeisyo_flg").ToString%>'
                                        ToolTip='<%#Eval("fuho_syoumeisyo_flg").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="fuho_syoumeisyo_kaisi_nengetu" runat="server" Width="141px" Text='<%#Eval("fuho_syoumeisyo_kaisi_nengetu").ToString%>'
                                        ToolTip='<%#Eval("fuho_syoumeisyo_kaisi_nengetu").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="eigyou_tantousya_mei" runat="server" Width="95px" Text='<%#Eval("eigyou_tantousya_mei").ToString%>'
                                        ToolTip='<%#Eval("eigyou_tantousya_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="eigyou_tantousya_simei" runat="server" Width="113px" Text='<%#Eval("eigyou_tantousya_simei").ToString%>'
                                        ToolTip='<%#Eval("eigyou_tantousya_simei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="kyuu_eigyou_tantousya_mei" runat="server" Width="113px" Text='<%#Eval("kyuu_eigyou_tantousya_mei").ToString%>'
                                        ToolTip='<%#Eval("kyuu_eigyou_tantousya_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="kyuu_eigyou_tantousya_simei" runat="server" Width="113px" Text='<%#Eval("kyuu_eigyou_tantousya_simei").ToString%>'
                                        ToolTip='<%#Eval("kyuu_eigyou_tantousya_simei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="koj_uri_syubetsu" runat="server" Width="90px" Text='<%#Eval("koj_uri_syubetsu").ToString%>'
                                        ToolTip='<%#Eval("koj_uri_syubetsu").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="koj_uri_syubetsu_mei" runat="server" Width="100px" Text='<%#Eval("koj_uri_syubetsu_mei").ToString%>'
                                        ToolTip='<%#Eval("koj_uri_syubetsu_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="jiosaki_flg" runat="server" Width="90px" Text='<%#Eval("jiosaki_flg").ToString%>'
                                        ToolTip='<%#Eval("jiosaki_flg").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="kaiyaku_haraimodosi_kkk" runat="server" Width="113px" Text='<%#Eval("kaiyaku_haraimodosi_kkk").ToString%>'
                                        ToolTip='<%#Eval("kaiyaku_haraimodosi_kkk").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tou1_syouhin_cd" runat="server" Width="141px" Text='<%#Eval("tou1_syouhin_cd").ToString%>'
                                        ToolTip='<%#Eval("tou1_syouhin_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tou1_syouhin_mei" runat="server" Width="113px" Text='<%#Eval("tou1_syouhin_mei").ToString%>'
                                        ToolTip='<%#Eval("tou1_syouhin_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tou2_syouhin_cd" runat="server" Width="141px" Text='<%#Eval("tou2_syouhin_cd").ToString%>'
                                        ToolTip='<%#Eval("tou2_syouhin_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tou2_syouhin_mei" runat="server" Width="113px" Text='<%#Eval("tou2_syouhin_mei").ToString%>'
                                        ToolTip='<%#Eval("tou2_syouhin_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tou3_syouhin_cd" runat="server" Width="141px" Text='<%#Eval("tou3_syouhin_cd").ToString%>'
                                        ToolTip='<%#Eval("tou3_syouhin_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tou3_syouhin_mei" runat="server" Width="113px" Text='<%#Eval("tou3_syouhin_mei").ToString%>'
                                        ToolTip='<%#Eval("tou3_syouhin_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tys_seikyuu_saki_kbn" runat="server" Width="113px" Text='<%#Eval("tys_seikyuu_saki_kbn").ToString%>'
                                        ToolTip='<%#Eval("tys_seikyuu_saki_kbn").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tys_seikyuu_saki_cd" runat="server" Width="113px" Text='<%#Eval("tys_seikyuu_saki_cd").ToString%>'
                                        ToolTip='<%#Eval("tys_seikyuu_saki_cd").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tys_seikyuu_saki_brc" runat="server" Width="113px" Text='<%#Eval("tys_seikyuu_saki_brc").ToString%>'
                                        ToolTip='<%#Eval("tys_seikyuu_saki_brc").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tys_seikyuu_saki_mei" runat="server" Width="113px" Text='<%#Eval("tys_seikyuu_saki_mei").ToString%>'
                                        ToolTip='<%#Eval("tys_seikyuu_saki_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="koj_seikyuu_saki_kbn" runat="server" Width="113px" Text='<%#Eval("koj_seikyuu_saki_kbn").ToString%>'
                                        ToolTip='<%#Eval("koj_seikyuu_saki_kbn").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="koj_seikyuu_saki_cd" runat="server" Width="113px" Text='<%#Eval("koj_seikyuu_saki_cd").ToString%>'
                                        ToolTip='<%#Eval("koj_seikyuu_saki_cd").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="koj_seikyuu_saki_brc" runat="server" Width="113px" Text='<%#Eval("koj_seikyuu_saki_brc").ToString%>'
                                        ToolTip='<%#Eval("koj_seikyuu_saki_brc").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="koj_seikyuu_saki_mei" runat="server" Width="113px" Text='<%#Eval("koj_seikyuu_saki_mei").ToString%>'
                                        ToolTip='<%#Eval("koj_seikyuu_saki_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="hansokuhin_seikyuu_saki_kbn" runat="server" Width="141px" Text='<%#Eval("hansokuhin_seikyuu_saki_kbn").ToString%>'
                                        ToolTip='<%#Eval("hansokuhin_seikyuu_saki_kbn").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="hansokuhin_seikyuu_saki_cd" runat="server" Width="141px" Text='<%#Eval("hansokuhin_seikyuu_saki_cd").ToString%>'
                                        ToolTip='<%#Eval("hansokuhin_seikyuu_saki_cd").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="hansokuhin_seikyuu_saki_brc" runat="server" Width="141px" Text='<%#Eval("hansokuhin_seikyuu_saki_brc").ToString%>'
                                        ToolTip='<%#Eval("hansokuhin_seikyuu_saki_brc").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="hansokuhin_seikyuu_saki_mei" runat="server" Width="141px" Text='<%#Eval("hansokuhin_seikyuu_saki_mei").ToString%>'
                                        ToolTip='<%#Eval("hansokuhin_seikyuu_saki_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tatemono_seikyuu_saki_kbn" runat="server" Width="141px" Text='<%#Eval("tatemono_seikyuu_saki_kbn").ToString%>'
                                        ToolTip='<%#Eval("tatemono_seikyuu_saki_kbn").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tatemono_seikyuu_saki_cd" runat="server" Width="141px" Text='<%#Eval("tatemono_seikyuu_saki_cd").ToString%>'
                                        ToolTip='<%#Eval("tatemono_seikyuu_saki_cd").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tatemono_seikyuu_saki_brc" runat="server" Width="141px" Text='<%#Eval("tatemono_seikyuu_saki_brc").ToString%>'
                                        ToolTip='<%#Eval("tatemono_seikyuu_saki_brc").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tatemono_seikyuu_saki_mei" runat="server" Width="141px" Text='<%#Eval("tatemono_seikyuu_saki_mei").ToString%>'
                                        ToolTip='<%#Eval("tatemono_seikyuu_saki_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_kbn5" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_kbn5").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_kbn5").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_cd5" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_cd5").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_cd5").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_brc5" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_brc5").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_brc5").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_mei5" runat="server" Width="95px" Text='<%#Eval("seikyuu_saki_mei5").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_mei5").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_kbn6" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_kbn6").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_kbn6").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_cd6" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_cd6").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_cd6").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_brc6" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_brc6").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_brc6").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_mei6" runat="server" Width="95px" Text='<%#Eval("seikyuu_saki_mei6").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_mei6").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_kbn7" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_kbn7").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_kbn7").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_cd7" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_cd7").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_cd7").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_brc7" runat="server" Width="113px" Text='<%#Eval("seikyuu_saki_brc7").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_brc7").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="seikyuu_saki_mei7" runat="server" Width="95px" Text='<%#Eval("seikyuu_saki_mei7").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_saki_mei7").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="62px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="hosyou_kikan" runat="server" Width="60px" Text='<%#Eval("hosyou_kikan").ToString%>'
                                        ToolTip='<%#Eval("hosyou_kikan").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="hosyousyo_hak_umu" runat="server" Width="100px" Text='<%#Eval("hosyousyo_hak_umu").ToString%>'
                                        ToolTip='<%#Eval("hosyousyo_hak_umu").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="nyuukin_kakunin_jyouken" runat="server" Width="113px" Text='<%#Eval("nyuukin_kakunin_jyouken").ToString%>'
                                        ToolTip='<%#Eval("nyuukin_kakunin_jyouken").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="nyuukin_kakunin_oboegaki" runat="server" Width="113px" Text='<%#Eval("nyuukin_kakunin_oboegaki").ToString%>'
                                        ToolTip='<%#Eval("nyuukin_kakunin_oboegaki").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="114px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tys_mitsyo_flg" runat="server" Width="114px" Text='<%#Eval("tys_mitsyo_flg").ToString%>'
                                        ToolTip='<%#Eval("tys_mitsyo_flg").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="96px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="hattyuusyo_flg" runat="server" Width="96px" Text='<%#Eval("hattyuusyo_flg").ToString%>'
                                        ToolTip='<%#Eval("hattyuusyo_flg").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="77px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="yuubin_no" runat="server" Width="77px" Text='<%#Eval("yuubin_no").ToString%>'
                                        ToolTip='<%#Eval("yuubin_no").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="jyuusyo1" runat="server" Width="95px" Text='<%#Eval("jyuusyo1").ToString%>'
                                        ToolTip='<%#Eval("jyuusyo1").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="jyuusyo2" runat="server" Width="95px" Text='<%#Eval("jyuusyo2").ToString%>'
                                        ToolTip='<%#Eval("jyuusyo2").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="syozaichi_cd" runat="server" Width="95px" Text='<%#Eval("syozaichi_cd").ToString%>'
                                        ToolTip='<%#Eval("syozaichi_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="syozaichi_mei" runat="server" Width="95px" Text='<%#Eval("syozaichi_mei").ToString%>'
                                        ToolTip='<%#Eval("syozaichi_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="77px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="busyo_mei" runat="server" Width="77px" Text='<%#Eval("busyo_mei").ToString%>'
                                        ToolTip='<%#Eval("busyo_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="77px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="daihyousya_mei" runat="server" Width="77px" Text='<%#Eval("daihyousya_mei").ToString%>'
                                        ToolTip='<%#Eval("daihyousya_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="tel_no" runat="server" Width="95px" Text='<%#Eval("tel_no").ToString%>'
                                        ToolTip='<%#Eval("tel_no").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="96px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="fax_no" runat="server" Width="96px" Text='<%#Eval("fax_no").ToString%>'
                                        ToolTip='<%#Eval("fax_no").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="mail_address" runat="server" Width="95px" Text='<%#Eval("mail_address").ToString%>'
                                        ToolTip='<%#Eval("mail_address").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="77px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou1" runat="server" Width="77px" Text='<%#Eval("bikou1").ToString%>'
                                        ToolTip='<%#Eval("bikou1").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="77px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou2" runat="server" Width="77px" Text='<%#Eval("bikou2").ToString%>'
                                        ToolTip='<%#Eval("bikou2").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAddDate" runat="server" Width="135px" Text='<%#Eval("add_date").ToString%>'
                                        ToolTip='<%#Eval("add_date").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="62px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSeikyuuUmu" runat="server" Width="60px" Text='<%#Eval("seikyuu_umu").ToString%>'
                                        ToolTip='<%#Eval("seikyuu_umu").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="72px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSyouhinCd" runat="server" Width="70px" Text='<%#Eval("syouhin_cd").ToString%>'
                                        ToolTip='<%#Eval("syouhin_cd").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSyouhinMei" runat="server" Width="135px" Text='<%#Eval("syouhin_mei").ToString%>'
                                        ToolTip='<%#Eval("syouhin_mei").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" BorderColor="#999999" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUriGaku" runat="server" Width="80px" Text='<%#Eval("uri_gaku").ToString%>'
                                        ToolTip='<%#Eval("uri_gaku").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="100px" BorderColor="#999999" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblKoumutenSeikyuuGaku" runat="server" Width="102px" Text='<%#Eval("koumuten_seikyuu_gaku").ToString%>'
                                        ToolTip='<%#Eval("koumuten_seikyuu_gaku").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSeikyuusyoHakDate" runat="server" Width="135px" Text='<%#Eval("seikyuusyo_hak_date").ToString%>'
                                        ToolTip='<%#Eval("seikyuusyo_hak_date").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUriDate" runat="server" Width="135px" Text='<%#Eval("uri_date").ToString%>'
                                        ToolTip='<%#Eval("uri_date").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="142px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBikou" runat="server" Width="140px" Text='<%#Eval("bikou").ToString%>'
                                        ToolTip='<%#Eval("bikou").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblKameitenUpdDatetime" runat="server" Width="135px" Text='<%#Eval("kameiten_upd_datetime").ToString%>'
                                        ToolTip='<%#Eval("kameiten_upd_datetime").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTatouwariUpdDatetime1" runat="server" Width="135px" Text='<%#Eval("tatouwari_upd_datetime1").ToString%>'
                                        ToolTip='<%#Eval("tatouwari_upd_datetime1").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTatouwariUpdDatetime2" runat="server" Width="135px" Text='<%#Eval("tatouwari_upd_datetime2").ToString%>'
                                        ToolTip='<%#Eval("tatouwari_upd_datetime2").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTatouwariUpdDatetime3" runat="server" Width="135px" Text='<%#Eval("tatouwari_upd_datetime3").ToString%>'
                                        ToolTip='<%#Eval("tatouwari_upd_datetime3").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblKameitenJyuusyoUpdDatetime" runat="server" Width="135px" Text='<%#Eval("kameiten_jyuusyo_upd_datetime").ToString%>'
                                        ToolTip='<%#Eval("kameiten_jyuusyo_upd_datetime").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu1" runat="server" Width="113px" Text='<%#Eval("bikou_syubetu1").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu1").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="122px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu1_mei" runat="server" Width="122px" Text='<%#Eval("bikou_syubetu1_mei").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu1_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu2" runat="server" Width="113px" Text='<%#Eval("bikou_syubetu2").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu2").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="122px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu2_mei" runat="server" Width="122px" Text='<%#Eval("bikou_syubetu2_mei").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu2_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu3" runat="server" Width="113px" Text='<%#Eval("bikou_syubetu3").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu3").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="122px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu3_mei" runat="server" Width="122px" Text='<%#Eval("bikou_syubetu3_mei").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu3_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu4" runat="server" Width="113px" Text='<%#Eval("bikou_syubetu4").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu4").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="122px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu4_mei" runat="server" Width="122px" Text='<%#Eval("bikou_syubetu4_mei").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu4_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="113px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu5" runat="server" Width="113px" Text='<%#Eval("bikou_syubetu5").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu5").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="122px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="bikou_syubetu5_mei" runat="server" Width="122px" Text='<%#Eval("bikou_syubetu5_mei").ToString%>'
                                        ToolTip='<%#Eval("bikou_syubetu5_mei").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="naiyou1" runat="server" Width="95px" Text='<%#Eval("naiyou1").ToString%>'
                                        ToolTip='<%#Eval("naiyou1").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="naiyou2" runat="server" Width="95px" Text='<%#Eval("naiyou2").ToString%>'
                                        ToolTip='<%#Eval("naiyou2").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="naiyou3" runat="server" Width="95px" Text='<%#Eval("naiyou3").ToString%>'
                                        ToolTip='<%#Eval("naiyou3").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="naiyou4" runat="server" Width="95px" Text='<%#Eval("naiyou4").ToString%>'
                                        ToolTip='<%#Eval("naiyou4").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="naiyou5" runat="server" Width="95px" Text='<%#Eval("naiyou5").ToString%>'
                                        ToolTip='<%#Eval("naiyou5").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="add_login_user_id" runat="server" Width="141px" Text='<%#Eval("add_login_user_id").ToString%>'
                                        ToolTip='<%#Eval("add_login_user_id").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="95px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="add_datetime" runat="server" Width="95px" Text='<%#Eval("add_datetime").ToString%>'
                                        ToolTip='<%#Eval("add_datetime").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="141px" BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="upd_login_user_id" runat="server" Width="141px" Text='<%#Eval("upd_login_user_id").ToString%>'
                                        ToolTip='<%#Eval("upd_login_user_id").ToString%>' Style="white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="upd_datetime" runat="server" Text='<%#Eval("upd_datetime").ToString%>'
                                        ToolTip='<%#Eval("upd_datetime").ToString%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#CCFFFF" />
                        <RowStyle Height="22px" BorderColor="#999999" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 16px; height: 221px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 221px; width: 30px;
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
            <td>
                <div style="overflow-x: hidden; height: 18px; width: 548px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 565px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 12400px;">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style="height: 16px;">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidCSVCount" runat="server" />
    <asp:HiddenField ID="hidCsvOut" runat="server" />
</asp:Content>
