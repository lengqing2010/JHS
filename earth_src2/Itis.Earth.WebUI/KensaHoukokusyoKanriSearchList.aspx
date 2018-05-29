<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/earthMaster.Master"
    Codebehind="KensaHoukokusyoKanriSearchList.aspx.vb" Inherits="Itis.Earth.WebUI.KensaHoukokusyoKanriSearchList"
    Title="検査報告書管理" %>

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
        if ("<%=gridviewRightId%>"!=''){
            objEBI("<%=tblRightId%>").style.height=objEBI("<%=gridviewRightId%>").offsetHeight;
            objEBI("<%=tblLeftId%>").style.width=objEBI("<%=gridviewRightId%>").offsetWidth;             
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
            <th style="width: 180px;">
                検査報告書管理
            </th>
            <th style="width: 70px;">
                <asp:Button ID="btnDefault" runat="server" Text="" TabIndex="-1" OnClientClick=";return false;"
                    Style="width: 1px; background-color: Transparent; border: none 0px;" />
                <asp:Button ID="btnClose" runat="server" Text="閉じる" Style="width: 50px; height: 25px;
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
            <td style="height: 28px; background-color: #ccffff; font-weight: bold;">
                格納日
            </td>
            <td>
                <asp:TextBox ID="tbxKakunouDateFrom" runat="server" Width="100px" MaxLength="10"
                    CssClass="codeNumber"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="～" Style="margin-left: 5px;"></asp:Label>
                <asp:TextBox ID="tbxKakunouDateTo" runat="server" Width="100px" MaxLength="10" CssClass="codeNumber"></asp:TextBox>
            </td>
            <td style="background-color: #ccffff; font-weight: bold;">
                発送日
            </td>
            <td>
                <asp:TextBox ID="tbxSendDateFrom" runat="server" Width="120px" MaxLength="10" CssClass="codeNumber"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="～" Style="margin-left: 5px;"></asp:Label>
                <asp:TextBox ID="tbxSendDateTo" runat="server" Width="120px" MaxLength="10" CssClass="codeNumber"></asp:TextBox>
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
                <asp:TextBox ID="tbxNoFrom" runat="server" Width="120px" CssClass="codeNumber" MaxLength="10"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="～" Style="margin-left: 5px;"></asp:Label>
                <asp:TextBox ID="tbxNoTo" runat="server" Width="120px" CssClass="codeNumber" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 28px; background-color: #ccffff; font-weight: bold;">
                加盟店
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbxKameitenCdFrom" runat="server" MaxLength="5" CssClass="codeNumber"
                    Style="width: 40px;"></asp:TextBox>
                <asp:Button ID="btnKameitenCdFrom" runat="server" Text="検索" OnClientClick="fncKameitenSearch('1');return false;"
                    Style="padding-top: 2px;" />
                <asp:TextBox ID="tbxKameitenMeiFrom" runat="server" CssClass="readOnlyStyle" TabIndex="-1"
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
                <asp:CheckBox ID="chkKensakuTaisyouGai" runat="server" Checked="true" Text="取消は検索対象外" />
                <asp:CheckBox ID="chkSendDateTaisyouGai" runat="server" Checked="true" Text="発送日セット済みは対象外" />
            </td>
        </tr>
    </table>
    <table id="tableKensuu" border="0" cellpadding="0" cellspacing="0" style="margin-top: 5px;">
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
    <table id="table1" border="0" cellpadding="0" cellspacing="0" style="width: 960px;
        margin-top: 5px;">
        <tr>
            <td style="width: 100px;">
            </td>
            <td style="width: 110px;">
                一括セット発送日
            </td>
            <td style="width: 150px;">
                <asp:TextBox ID="tbxSetSendDate" runat="server" Width="100px" MaxLength="10" CssClass="codeNumber"></asp:TextBox>
            </td>
            <td style="width: 80px;">
                送付担当者
            </td>
            <td style="width: 120px;">
                <asp:TextBox ID="tbxTantousya" runat="server" Width="100px" MaxLength="64"></asp:TextBox>
            </td>
            <td style="width: 250px;">
                <asp:Button ID="btnSetSend" runat="server" Width="140px" Text="選択物件一括セット" Style="padding-top: 2px;" />
            </td>
            <td style="text-align: right;">
                <asp:Button ID="btnTorikesi" runat="server" Width="80px" Text="取消セット" Style="padding-top: 2px;" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 5px;">
        <tr>
            <td>
                <div id="divHeadLeft" style="width: 830px; border-top: solid 1px black; border-left: solid 1px black;
                    border-bottom: solid 1px black; overflow: hidden;">
                    <table id="tableHeadLeft" border="0" cellpadding="0" cellspacing="0" style="width: 830px;
                        background-color: #ffffd9; font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 45px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                対象<asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Style="margin-left: -3px;" />
                            </td>
                            <td style="width: 32px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                取消
                                <asp:LinkButton runat="server" ID="btnTorikesiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px; display: none;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTorikesiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px; display: none;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 60px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                管理
                                <asp:LinkButton runat="server" ID="btnKanrinoUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKanrinoDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 32px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                区分
                                <asp:LinkButton runat="server" ID="btnKbnUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px; display: none;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKbnDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px; display: none;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                保証書NO
                                <asp:LinkButton runat="server" ID="btnHosyousyonoUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnHosyousyonoDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 282px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                施主名
                                <asp:LinkButton runat="server" ID="btnSesyumeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSesyumeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 62px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                加盟店CD
                                <asp:LinkButton runat="server" ID="btnKameitencdUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px; display: none;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKameitencdDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px; display: none;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="border-top: none; border-bottom: none; border-right: solid 1px gray; text-align: center;
                                font-size: 12px;">
                                加盟店名
                                <asp:LinkButton runat="server" ID="btnKameitenmeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKameitenmeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <div id="divHeadRight" runat="server" style="width: 128px; border-top: solid 1px black;
                    border-bottom: solid 1px black; border-right: solid 1px black; overflow: hidden;">
                    <table id="tableHeadRigth" border="0" cellpadding="0" cellspacing="0" style="width: 6670px;
                        background-color: #ffffd9; font-weight: bold;">
                        <tr style="height: 24px;">
                            <td style="width: 83px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                格納日
                                <asp:LinkButton runat="server" ID="btnKakunoudateUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKakunoudateDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 83px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                発送日
                                <asp:LinkButton runat="server" ID="btnHassoudateUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnHassoudateDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 60px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                部数
                                <asp:LinkButton runat="server" ID="btnKensahkksbusuuUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensahkksbusuuDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 180px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査報告書送付先住所1
                                <asp:LinkButton runat="server" ID="btnKensahkksjyuusyo1Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensahkksjyuusyo1Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 180px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査報告書送付先住所2
                                <asp:LinkButton runat="server" ID="btnKensahkksjyuusyo2Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensahkksjyuusyo2Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                郵便番号
                                <asp:LinkButton runat="server" ID="btnYuubinnoUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnYuubinnoDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                電話番号
                                <asp:LinkButton runat="server" ID="btnTelnoUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTelnoDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                部署名
                                <asp:LinkButton runat="server" ID="btnBusyomeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnBusyomeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 115px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                都道府県コード
                                <asp:LinkButton runat="server" ID="btnTodouhukencdUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTodouhukencdDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 95px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                都道府県名
                                <asp:LinkButton runat="server" ID="btnTodouhukenmeiUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTodouhukenmeiDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 130px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                発送日入力フラグ
                                <asp:LinkButton runat="server" ID="btnHassoudateinflgUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnHassoudateinflgDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 95px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                送付担当者
                                <asp:LinkButton runat="server" ID="btnSouhutantousyaUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSouhutantousyaDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 90px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                物件住所1
                                <asp:LinkButton runat="server" ID="btnBukkenjyuusyo1Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnBukkenjyuusyo1Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 90px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                物件住所2
                                <asp:LinkButton runat="server" ID="btnBukkenjyuusyo2Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnBukkenjyuusyo2Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 90px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                物件住所3
                                <asp:LinkButton runat="server" ID="btnBukkenjyuusyo3Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnBukkenjyuusyo3Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                建物構造
                                <asp:LinkButton runat="server" ID="btnTatemonokouzouUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTatemonokouzouDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                建物階数
                                <asp:LinkButton runat="server" ID="btnTatemonokaisuUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTatemonokaisuDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 60px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                FC名
                                <asp:LinkButton runat="server" ID="btnFcnmUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnFcnmDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                依頼担当者名
                                <asp:LinkButton runat="server" ID="btnKameitentantoUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKameitentantoDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 130px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                建物加盟店コード
                                <asp:LinkButton runat="server" ID="btnTatemonokameitencdUp" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTatemonokameitencdDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 130px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                管理表出力フラグ
                                <asp:LinkButton runat="server" ID="btnKanrihyououtflgUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKanrihyououtflgDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                管理表出力日
                                <asp:LinkButton runat="server" ID="btnKanrihyououtdateUp" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKanrihyououtdateDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 130px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                送付状出力フラグ
                                <asp:LinkButton runat="server" ID="btnSouhujyououtflgUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSouhujyououtflgDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                送付状出力日
                                <asp:LinkButton runat="server" ID="btnSouhujyououtdateUp" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnSouhujyououtdateDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 160px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査報告書出力フラグ
                                <asp:LinkButton runat="server" ID="btnKensahkksoutflgUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensahkksoutflgDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 145px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査報告書出力日
                                <asp:LinkButton runat="server" ID="btnKensahkksoutdateUp" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensahkksoutdateDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 80px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                通しNo
                                <asp:LinkButton runat="server" ID="btnTooshinoUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnTooshinoDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名1
                                <asp:LinkButton runat="server" ID="btnKensakouteinm1Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm1Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名2
                                <asp:LinkButton runat="server" ID="btnKensakouteinm2Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm2Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名3
                                <asp:LinkButton runat="server" ID="btnKensakouteinm3Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm3Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名4
                                <asp:LinkButton runat="server" ID="btnKensakouteinm4Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm4Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名5
                                <asp:LinkButton runat="server" ID="btnKensakouteinm5Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm5Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名6
                                <asp:LinkButton runat="server" ID="btnKensakouteinm6Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm6Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名7
                                <asp:LinkButton runat="server" ID="btnKensakouteinm7Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm7Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名8
                                <asp:LinkButton runat="server" ID="btnKensakouteinm8Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm8Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名9
                                <asp:LinkButton runat="server" ID="btnKensakouteinm9Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm9Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査工程名10
                                <asp:LinkButton runat="server" ID="btnKensakouteinm10Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensakouteinm10Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日1
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi1Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi1Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日2
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi2Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi2Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日3
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi3Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi3Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日4
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi4Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi4Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日5
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi5Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi5Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日6
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi6Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi6Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日7
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi7Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi7Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日8
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi8Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi8Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日9
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi9Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi9Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 110px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査実施日10
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi10Up" Text="▲" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensastartjissibi10Down" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名1
                                <asp:LinkButton runat="server" ID="btnKensainnm1Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm1Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名2
                                <asp:LinkButton runat="server" ID="btnKensainnm2Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm2Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名3
                                <asp:LinkButton runat="server" ID="btnKensainnm3Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm3Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名4
                                <asp:LinkButton runat="server" ID="btnKensainnm4Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm4Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名5
                                <asp:LinkButton runat="server" ID="btnKensainnm5Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm5Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名6
                                <asp:LinkButton runat="server" ID="btnKensainnm6Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm6Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名7
                                <asp:LinkButton runat="server" ID="btnKensainnm7Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm7Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名8
                                <asp:LinkButton runat="server" ID="btnKensainnm8Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm8Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名9
                                <asp:LinkButton runat="server" ID="btnKensainnm9Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm9Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                検査員名10
                                <asp:LinkButton runat="server" ID="btnKensainnm10Up" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnKensainnm10Down" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 160px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                登録ログインユーザID
                                <asp:LinkButton runat="server" ID="btnAddloginuseridUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAddloginuseridDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 100px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                登録日時
                                <asp:LinkButton runat="server" ID="btnAdddatetimeUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnAdddatetimeDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="width: 160px; border-top: none; border-bottom: none; border-right: solid 1px gray;
                                text-align: center; font-size: 12px;">
                                更新ログインユーザID
                                <asp:LinkButton runat="server" ID="btnUpdloginuseridUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnUpdloginuseridDown" Text="▼" Height="14px"
                                    TabIndex="-1" Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                            <td style="border-top: none; border-bottom: none; border-right: solid 1px gray; text-align: center;
                                font-size: 12px;">
                                更新日時
                                <asp:LinkButton runat="server" ID="btnUpddatetimeUp" Text="▲" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -5px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnUpddatetimeDown" Text="▼" Height="14px" TabIndex="-1"
                                    Font-Underline="false" Style="margin-left: -10px;" OnClientClick="fncSetScroll();"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div id="divBodyLeft" runat="server" onmousewheel="wheel();" style="width: 830px;
                    height: 275px; border-left: solid 1px black; border-bottom: solid 1px black;
                    overflow: hidden; margin-top: -1px;">
                    <asp:GridView ID="grdBodyLeft" runat="server" AutoGenerateColumns="False" BackColor="White"
                        ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="width: 830px;">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="40px" Height="24px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkKensakuTaisyouGai" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="28px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTorikesi" runat="server" Width="24px" Text='<%#Eval("torikesi")%>'
                                        ToolTip='<%#Eval("torikesi")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="56px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblKanrino" runat="server" Width="52px" Text='<%#Eval("kanri_no")%>'
                                        ToolTip='<%#Eval("kanri_no")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="28px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblkbn" runat="server" Width="24px" Text='<%#Eval("kbn")%>' ToolTip='<%#Eval("kbn")%>'
                                        Style="font-size: 12px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="76px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHosyousyono" runat="server" Width="70px" Text='<%#Eval("hosyousyo_no")%>'
                                        ToolTip='<%#Eval("hosyousyo_no")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="278px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns3" runat="server" Width="276px" Text='<%#Eval("sesyu_mei")%>'
                                        ToolTip='<%#Eval("sesyu_mei")%>' Style="font-size: 11px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="58px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblkameitencd" runat="server" Width="54px" Text='<%#Eval("kameiten_cd")%>'
                                        ToolTip='<%#Eval("kameiten_cd")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns24" runat="server" Width="222px" Text='<%#Eval("kameiten_mei")%>'
                                        ToolTip='<%#Eval("kameiten_mei")%>' Style="font-size: 11px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td style="vertical-align:top;">
                <div id="divBodyRight" runat="server" onmousewheel="wheel();" style="width: 128px;
                    height: 275px; border-bottom: solid 1px black; border-right: solid 1px black;
                    overflow: scroll; margin-top: -1px; overflow: hidden;">
                    <asp:GridView ID="grdBodyRight" runat="server" AutoGenerateColumns="False" BackColor="White"
                        ShowHeader="False" CellPadding="0" CssClass="tableMeiSai" Style="width: 6670px;
                        border-right: solid 1px gray; padding-right: 4px; margin-left: -1px;">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="75px" Height="24px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns4" runat="server" Width="75px" Text='<%#GetstrDate(Eval("kakunou_date").ToString())%>'
                                        ToolTip='<%#Eval("kakunou_date").Tostring()%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="75px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns25" runat="server" Text='<%#GetstrDate(Eval("hassou_date").ToString())%>'
                                        ToolTip='<%#Eval("hassou_date").Tostring()%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kensa_hkks_busuu">
                                <ItemStyle Width="52px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="172px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="170px" Text='<%#Eval("kensa_hkks_jyuusyo1")%>'
                                        ToolTip='<%#Eval("kensa_hkks_jyuusyo1")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="172px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="170px" Text='<%#Eval("kensa_hkks_jyuusyo2")%>'
                                        ToolTip='<%#Eval("kensa_hkks_jyuusyo2")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="yuubin_no">
                                <ItemStyle Width="72px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tel_no">
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="72px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="70px" Text='<%#Eval("busyo_mei")%>'
                                        ToolTip='<%#Eval("busyo_mei")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="todouhuken_cd">
                                <ItemStyle Width="107px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="87px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="85px" Text='<%#Eval("todouhuken_mei")%>'
                                        ToolTip='<%#Eval("todouhuken_mei")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="hassou_date_in_flg">
                                <ItemStyle Width="122px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="87px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="85px" Text='<%#Eval("souhu_tantousya")%>'
                                        ToolTip='<%#Eval("souhu_tantousya")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="80px" Text='<%#Eval("bukken_jyuusyo1")%>'
                                        ToolTip='<%#Eval("bukken_jyuusyo1")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="80px" Text='<%#Eval("bukken_jyuusyo2")%>'
                                        ToolTip='<%#Eval("bukken_jyuusyo2")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="82px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="80px" Text='<%#Eval("bukken_jyuusyo3")%>'
                                        ToolTip='<%#Eval("bukken_jyuusyo3")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="72px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="70px" Text='<%#Eval("tatemono_kouzou")%>'
                                        ToolTip='<%#Eval("tatemono_kouzou")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="72px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="70px" Text='<%#Eval("tatemono_kaisu")%>'
                                        ToolTip='<%#Eval("tatemono_kaisu")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="52px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="50px" Text='<%#Eval("fc_nm")%>'
                                        ToolTip='<%#Eval("fc_nm")%>' Style="font-size: 12px; white-space: nowrap; overflow: hidden;
                                        text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kameiten_tanto")%>'
                                        ToolTip='<%#Eval("kameiten_tanto")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="tatemono_kameiten_cd">
                                <ItemStyle Width="122px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="kanrihyou_out_flg">
                                <ItemStyle Width="122px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#GetstrDate(Eval("kanrihyou_out_date").ToString())%>'
                                        ToolTip='<%#Eval("kanrihyou_out_date")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="souhujyou_out_flg">
                                <ItemStyle Width="122px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#GetstrDate(Eval("souhujyou_out_date").ToString())%>'
                                        ToolTip='<%#Eval("souhujyou_out_date")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="kensa_hkks_out_flg">
                                <ItemStyle Width="152px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="137px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="130px" Text='<%#GetstrDate(Eval("kensa_hkks_out_date").ToString())%>'
                                        ToolTip='<%#Eval("kensa_hkks_out_date")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="tooshi_no">
                                <ItemStyle Width="72px" HorizontalAlign="Left" BorderColor="#999999" Font-Size="12px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm1")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm1")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm2")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm2")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm3")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm3")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm4")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm4")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm5")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm5")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm6")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm6")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm7")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm7")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm8")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm8")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm9")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm9")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("kensa_koutei_nm10")%>'
                                        ToolTip='<%#Eval("kensa_koutei_nm10")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi1").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi1")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi2").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi2")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi3").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi3")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi4").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi4")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi5").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi5")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi6").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi6")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi7").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi7")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi8").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi8")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi9").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi9")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="102px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("kensa_start_jissibi10").ToString())%>'
                                        ToolTip='<%#Eval("kensa_start_jissibi10")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm1")%>'
                                        ToolTip='<%#Eval("kensa_in_nm1")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm2")%>'
                                        ToolTip='<%#Eval("kensa_in_nm2")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm3")%>'
                                        ToolTip='<%#Eval("kensa_in_nm3")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm4")%>'
                                        ToolTip='<%#Eval("kensa_in_nm4")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm5")%>'
                                        ToolTip='<%#Eval("kensa_in_nm5")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm6")%>'
                                        ToolTip='<%#Eval("kensa_in_nm6")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm7")%>'
                                        ToolTip='<%#Eval("kensa_in_nm7")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm8")%>'
                                        ToolTip='<%#Eval("kensa_in_nm8")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm9")%>'
                                        ToolTip='<%#Eval("kensa_in_nm9")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#Eval("kensa_in_nm10")%>'
                                        ToolTip='<%#Eval("kensa_in_nm10")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="152px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("add_login_user_id")%>'
                                        ToolTip='<%#Eval("add_login_user_id")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="92px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("add_datetime").ToString())%>'
                                        ToolTip='<%#Eval("add_datetime")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="152px" HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="100px" Text='<%#Eval("upd_login_user_id")%>'
                                        ToolTip='<%#Eval("upd_login_user_id")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Left" BorderColor="#999999" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColumns2" runat="server" Width="90px" Text='<%#GetstrDate(Eval("upd_datetime").ToString())%>'
                                        ToolTip='<%#Eval("upd_datetime")%>' Style="font-size: 12px; white-space: nowrap;
                                        overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                </div>
            </td>
            <td valign="top" style="width: 17px; height: 130px;">
                <div id="divHiddenMeisaiV" runat="server" style="overflow: auto; height: 276px; width: 30px;
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
            <td style="width: 128px;">
                <div style="overflow: hidden; height: 18px; width: 128px; margin-top: -1px;">
                    <div id="divHiddenMeisaiH" runat="server" style="overflow: auto; height: 18px; width: 146px;
                        margin-top: 0px;" onscroll="fncScrollH();">
                        <table style="width: 6670px;">
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
    <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; width: 960px;
        margin-top: 5px;">
        <tr>
            <td style="text-align: right;">
                <asp:Button ID="btnGotoOutput" runat="server" Text="各帳票出力指示画面へ" Style="width: 180px;
                    padding-top: 2px;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidScroll" runat="server" />
    <asp:HiddenField ID="hidCsvFlg" runat="server" />
    <asp:HiddenField ID="hidCsvOut" runat="server" Value="" />
    <asp:Button ID="btnCsv" runat="server" Style="display: none;" />
</asp:Content>
