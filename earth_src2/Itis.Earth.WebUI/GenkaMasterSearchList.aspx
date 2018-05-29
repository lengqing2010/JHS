<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="GenkaMasterSearchList.aspx.vb" Inherits="Itis.Earth.WebUI.GenkaMasterSearchList"
    Title="原価マスタ照会" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js"></script>

    <script type="text/javascript">
    //window名付与
    var objWin = window;
    objWin.name = "earthMainWindow"
    initPage(); //画面初期設定
    
    function fncClose()
    {
        window.close();
    }
        
//    function window.onload(){

//    }

    function fncOpenInputPopup()
    {
        window.open("GenkaMasterInput.aspx","popup","menubar=yes,toolbar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes");
    
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
            <th style="width: 180px;">
                原価マスタ照会
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="height: 25px; padding-top: 2px;" />
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnCsvInput" runat="server" Text="CSV取込" Style="height: 25px; padding-top: 2px;" />
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
            <th class="tableTitle" style="width: 60px; height: 20px;">
                検索条件
            </th>
            <th class="tableTitle" style="width: 50px;">
                <asp:Button ID="btnClear" runat="server" Text="クリア" Style="padding-top: 2px;" />
            </th>
            <th class="tableTitle" style="width: 400px;">
            </th>
            <th class="tableTitle" style="width: 100px;">
            </th>
            <th class="tableTitle">
            </th>
        </tr>
        <tr>
            <td colspan="2" style="height: 28px; background-color: #ccffff;">
                調査会社コード
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbxTyousaKaisyaCd" runat="server" Text="" MaxLength="6" Style="width: 50px;
                    ime-mode: disabled;"></asp:TextBox>
                <asp:Button ID="btnKensakuTyousakaisyaCd" runat="server" Text="検索" Style="padding-top: 2px;" />
                <asp:TextBox ID="tbxTyousaKaisyaMei" runat="server" CssClass="readOnlyStyle" ReadOnly="true"
                    TabIndex="-1" Width="280px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 28px; background-color: #ccffff;">
                相手先コード
            </td>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" style="border-top: none; border-bottom: none;
                    border-left: none; border-right: none;">
                    <tr>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <asp:DropDownList ID="ddlAiteSakiSyubetu" runat="server" Style="width: 90px;">
                            </asp:DropDownList>
                        </td>
                        <td style="border-top: none; border-bottom: none; border-left: none; border-right: none;">
                            <div id="divAitesaki" runat="server">
                                <asp:TextBox ID="tbxAiteSakiCdFrom" runat="server" Text="" MaxLength="5" Style="width: 40px;
                                    ime-mode: disabled;"></asp:TextBox>
                                <asp:Button ID="btnAiteSakiCdFrom" runat="server" Text="検索" Style="padding-top: 2px;" />
                                <asp:TextBox ID="tbxAiteSakiMeiFrom" runat="server" CssClass="readOnlyStyle" ReadOnly="true"
                                    TabIndex="-1" Style="width: 242px;"></asp:TextBox>
                                <asp:Label ID="lblFromTo" runat="server" Text="～" style=" margin-left:5px;" ></asp:Label>
                                <asp:TextBox ID="tbxAiteSakiCdTo" runat="server" Text="" MaxLength="5" Style="width: 40px;
                                    margin-left:5px; ime-mode: disabled;"></asp:TextBox>
                                <asp:Button ID="btnAiteSakiCdTo" runat="server" Text="検索" Style="padding-top: 2px;" />
                                <asp:TextBox ID="tbxAiteSakiMeiTo" runat="server" CssClass="readOnlyStyle" ReadOnly="true"
                                    TabIndex="-1" Style="width: 242px;"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 28px; background-color: #ccffff;">
                商品コード
            </td>
            <td>
                <asp:DropDownList ID="ddlSyouhinCd" runat="server" Style="width: 350px;">
                </asp:DropDownList>
            </td>
            <td style="height: 28px; background-color: #ccffff;">
                調査方法</td>
            <td>
                <asp:DropDownList ID="ddlTyousaHouhou" runat="server" Style="width: 300px;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 28px; text-align: center;">
                <asp:Label ID="lblJyouken" runat="server" Text="検索上限件数" Style=""></asp:Label>
                <asp:DropDownList ID="ddlKensakuJyouken" runat="server" Style="width: 70px;">
                    <asp:ListItem Value="10" Text="10件"></asp:ListItem>
                    <asp:ListItem Value="100" Text="100件" Selected="true"></asp:ListItem>
                    <asp:ListItem Value="max" Text="無制限"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnKensakujiltukou" runat="server" Text="検索実行" Style="height: 25px;
                    padding-top: 2px;" />
                <asp:CheckBox ID="chkKensakuTaisyouGai" runat="server" Text="取消は検索対象外" />
                <asp:CheckBox ID="chkAitesakiTaisyouGai" runat="server" Text="取消相手先は対象外" />
                <asp:Button ID="btnCsvOutput" runat="server" Text="CSV出力" Style="height: 25px; padding-top: 2px;" />
                <asp:CheckBox ID="chkMiseltutei" runat="server" Text="未設定も含む" />
            </td>
        </tr>
    </table>
    <table id="tableKensuu" border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="width:65px; text-align:left;">
                検索結果：
            </td>
            <td>
                <asp:Label runat="server" ID="lblCount" style="width:auto;"></asp:Label>
            </td>
            <td style="width:20px; text-align:right;">
                件
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divHeadLeft" style="width: 729px; border-top: solid 1px black; border-left: solid 1px black;
                    border-bottom: solid 1px black; overflow-y: hidden; overflow-x: hidden;">
                    <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffd9;
                        font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 75px; border-top: none; border-bottom: none; border-left: none;
                                border-right: solid 1px gray; text-align: center;">
                                調コード
                                <asp:LinkButton runat="server" ID="btnTyousakaisyaCdUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTyousakaisyaCdDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 120px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                調査会社名
                                <asp:LinkButton runat="server" ID="btnTyousakaisyaMeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTyousakaisyaMeiDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 70px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                先種別
                                <asp:LinkButton runat="server" ID="btnAiteSakiSyubetuUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAiteSakiSyubetuDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 72px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                先コード
                                <asp:LinkButton runat="server" ID="btnAiteSakiCdUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAiteSakiCdDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 84px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                相手先名
                                <asp:LinkButton runat="server" ID="btnAiteSakiMeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAiteSakiMeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 60px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                商品
                                <asp:LinkButton runat="server" ID="btnSyouhinCdUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSyouhinCdDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 120px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                商品名
                                <asp:LinkButton runat="server" ID="btnSyouhinMeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSyouhinMeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 120px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                調査方法
                                <asp:LinkButton runat="server" ID="btnTyousaHouhouUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTyousaHouhouDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 230px; border-top: solid 1px black;
                    border-bottom: solid 1px black; border-right: solid 1px black; overflow-y: hidden;
                    overflow-x: hidden;">
                    <table id="tableHeadRigth" border="0" cellpadding="0" cellspacing="0" style="width: 2860px;
                        background-color: #ffffd9; font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 55px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                取消
                                <asp:LinkButton runat="server" ID="btnTorikesiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTorikesiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格1
                                <asp:LinkButton runat="server" ID="btnKakaku1Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku1Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg1Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg1Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格2
                                <asp:LinkButton runat="server" ID="btnKakaku2Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku2Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg2Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg2Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格3
                                <asp:LinkButton runat="server" ID="btnKakaku3Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku3Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg3Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg3Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格4
                                <asp:LinkButton runat="server" ID="btnKakaku4Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku4Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg4Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg4Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格5
                                <asp:LinkButton runat="server" ID="btnKakaku5Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku5Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg5Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg5Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格6
                                <asp:LinkButton runat="server" ID="btnKakaku6Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku6Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg6Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg6Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格7
                                <asp:LinkButton runat="server" ID="btnKakaku7Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku7Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg7Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg7Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格8
                                <asp:LinkButton runat="server" ID="btnKakaku8Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku8Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg8Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg8Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格9
                                <asp:LinkButton runat="server" ID="btnKakaku9Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku9Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg9Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg9Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格10
                                <asp:LinkButton runat="server" ID="btnKakaku10Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku10Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg10Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg10Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格11～19
                                <asp:LinkButton runat="server" ID="btnKakaku11to19Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku11to19Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg11to19Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg11to19Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格20～29
                                <asp:LinkButton runat="server" ID="btnKakaku21to29Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku21to29Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg21to29Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg21to29Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格30～39
                                <asp:LinkButton runat="server" ID="btnKakaku31to39Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku31to39Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg31to39Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg31to39Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格40～49
                                <asp:LinkButton runat="server" ID="btnKakaku41to49Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku41to49Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg41to49Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg41to49Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center;">
                                棟価格50～
                                <asp:LinkButton runat="server" ID="btnKakaku50Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakaku50Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="border-top: none; border-bottom: none; border-right: solid 1px gray; text-align: center;">
                                価格変更
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg50Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakakuHenkouFlg50Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetCsvOut();fncSetScroll();"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 729px;
                    height: 265px; border-left: solid 1px black; border-bottom: solid 1px black;
                    overflow-y: hidden; overflow-x: hidden; margin-top: -1px;">
                    <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="width: 729px;">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="70px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="66px" Text='<%#Eval("tys_kaisya_cd")%>'
                                        ToolTip='<%#Eval("tys_kaisya_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="116px" Height="21px" HorizontalAlign="Left" BorderColor="#999999"
                                    Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="111px" Text='<%#Eval("tys_kaisya_mei")%>'
                                        ToolTip='<%#Eval("tys_kaisya_mei")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="66px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="61px" Text='<%#Eval("aitesaki_syubetu")%>'
                                        ToolTip='<%#Eval("aitesaki_syubetu")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="68px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="63px" Text='<%#Eval("aitesaki_cd")%>'
                                        ToolTip='<%#Eval("aitesaki_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="80px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="75px" Text='<%#Eval("aitesaki_mei")%>'
                                        ToolTip='<%#Eval("aitesaki_mei")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="56px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="51px" Text='<%#Eval("syouhin_cd")%>'
                                        ToolTip='<%#Eval("syouhin_cd")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="116px" Height="21px" HorizontalAlign="Left" BorderColor="#999999"
                                    Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="111px" Text='<%#Eval("syouhin_mei")%>'
                                        ToolTip='<%#Eval("syouhin_mei")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Height="21px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTysKaisyaCd" runat="server" Width="111px" Text='<%#Eval("tys_houhou")%>'
                                        ToolTip='<%#Eval("tys_houhou")%>' Style="white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td>
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 230px;
                    height: 265px; border-bottom: solid 1px black; border-right: solid 1px black;
                    overflow: scroll; margin-top: -1px; overflow-x: hidden; overflow-y: hidden;">
                    <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="width: 2861px;
                        border-right: solid 1px gray; padding-right: 4px; margin-left: -1px;">
                        <Columns>
                            <asp:BoundField DataField="torikesi">
                                <ItemStyle Width="47px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk1">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg1">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk2">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg2">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk3">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg3">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk4">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg4">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk5">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg5">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk6">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg6">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk7">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg7">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk8">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg8">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk9">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg9">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk10">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg10">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk11t19">
                                <ItemStyle Width="107px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg11t19">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk20t29">
                                <ItemStyle Width="107px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg20t29">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk30t39">
                                <ItemStyle Width="107px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg30t39">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk40t49">
                                <ItemStyle Width="107px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg40t49">
                                <ItemStyle Width="72px" Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk50t">
                                <ItemStyle Width="92px" Height="21px" HorizontalAlign="Right" BorderColor="#999999" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tou_kkk_henkou_flg50t">
                                <ItemStyle Height="21px" HorizontalAlign="Left" BorderColor="#999999" />
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 17px; height: 265px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 265px; width: 30px;
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
            <td style="width: 231px;">
                <div style="overflow-x: hidden; height: 18px; width: 231px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 248px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 2860px;">
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
