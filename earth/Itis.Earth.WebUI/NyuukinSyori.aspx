<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="NyuukinSyori.aspx.vb" Inherits="Itis.Earth.WebUI.NyuukinSyori" Title="EARTH 入金処理" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        function executeConfirm(objCtrl){
            var strFileName;
            var strMessage;
            if(objCtrl == objEBI("<%= ButtonNyuukinDataTorikomi.clientID %>")){
                strFileName = objEBI("<%= FileNyuukinDataTorikomi.clientID %>").value;
                if (strFileName.length > 0){
                    if(!confirm("<%= Messages.MSG055C %>".replace("@PARAM1", strFileName))){
                        return false;
                    }
                    setWindowOverlay(objCtrl);
                    objEBI("<%= ButtonNyuukinDataTorikomiNext.clientID %>").click();
                }else{
                    alert("<%= Messages.MSG056E %>");
                }
            }else if(objCtrl == objEBI("<%= ButtonJhsNyuukinDataTorikomi.clientID %>")){
                strFileName = objEBI("<%= FileJhsNyuukinDataTorikomi.clientID %>").value;
                if (strFileName.length > 0){
                    if(!confirm("<%= Messages.MSG055C %>".replace("@PARAM1", strFileName))){
                        return false;
                    }
                    setWindowOverlay(objCtrl);
                    objEBI("<%= ButtonJhsNyuukinDataTorikomiNext.clientID %>").click();
                }else{
                    alert("<%= Messages.MSG056E %>");
                }
            }
        }
    </script>

    <input type="hidden" id="HiddenChkUriageGaku" runat="server" />
    <!-- 画面上部・ヘッダ[Table1] -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    入金処理
                </th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <!-- 処理タイトル -->
    <div style="width: 396px; height: 20px; text-align: center; background-color: lemonchiffon;
        padding-top: 6px; border: 2px solid gray; border-bottom: 0px; font-weight: bold;
        display: none;">
        一括入金処理</div>
    <!-- 1行目[Table2] -->
    <table style="text-align: left; width: 400px; height: 120px;" id="" class="mainTable"
        cellpadding="1" cellspacing="1" border="1" style="display: none;">
        <!-- 1行目 -->
        <tr>
            <td style="width: 100px" class="koumokuMei">
                請求書発行日
            </td>
            <td>
                <input type="text" id="TextSeikyuusyoHakkoubiFrom" runat="server" maxlength="10"
                    class="date" onblur="checkDate(this);" />
                &nbsp;～&nbsp;
                <input type="text" id="TextSeikyuusyoHakkoubiTo" runat="server" maxlength="10" class="date"
                    onblur="checkDate(this);" />
            </td>
        </tr>
        <!-- 2行目 -->
        <tr>
            <td class="koumokuMei">
                系列コード
            </td>
            <td>
                <select id="SelectKeiretuCode" runat="server">
                </select>
            </td>
        </tr>
        <!-- 3行目 -->
        <tr>
            <td colspan="2" style="text-align: center;">
                <input type="button" id="ButtonIkkatuNyuukinSyori" runat="server" value="一括入金処理"
                    style="font-size: 12px; width: 200px; color: black; height: 30px;" />
                <input type="button" id="ButtonIkkatuNyuukinSyoriNext" runat="server" value="一括入金処理"
                    style="font-size: 12px; width: 200px; color: black; height: 30px; display: none;" />
            </td>
        </tr>
    </table>
    <br />
    <!-- 処理タイトル -->
    <div style="float: left">
        <div style="width: 396px; height: 20px; text-align: center; background-color: lemonchiffon;
            padding-top: 6px; border: 2px solid gray; border-bottom: 0px; font-weight: bold;">
            邸別入金データ取込</div>
        <!-- 画面下部 -->
        <table style="text-align: left; width: 400px; height: 200px; float: left" id="Table1"
            class="mainTable" cellpadding="1" cellspacing="1" border="1">
            <tbody>
                <tr>
                    <td style="width: 100px; height: 30px;" class="koumokuMei">
                        前回取込日時
                    </td>
                    <td>
                        <input type="text" id="TextZenkaiTorikomiNitiji" runat="server" class="date readOnlyStyle"
                            maxlength="10" style="width: 120px" readonly="readonly" tabindex="-1" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; height: 30px;" class="koumokuMei">
                        前回取込ファイル名
                    </td>
                    <td>
                        <input type="text" id="TextZenkaiTorikomiFileMei" runat="server" class="readOnlyStyle"
                            maxlength="128" style="width: 200px" readonly="readonly" tabindex="-1" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; height: 30px;" class="koumokuMei">
                        前回エラー有無
                    </td>
                    <td>
                        &nbsp;<asp:HyperLink ID="LinkZenkaiErrorUmu" runat="server" NavigateUrl="~/NyuukinError.aspx"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 150px;" colspan="2">
                        <asp:FileUpload ID="FileNyuukinDataTorikomi" Style="width: 350px" runat="server" /><br />
                        <br />
                        <input type="button" id="ButtonNyuukinDataTorikomi" runat="server" value="入金データ取り込み"
                            style="font-size: 12px; width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
                        <input type="button" id="ButtonNyuukinDataTorikomiNext" runat="server" value="入金データ取り込み"
                            style="font-size: 12px; width: 200px; color: black; height: 30px; display: none;" /><br />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!-- 処理タイトル -->
    <div style="width: 396px; height: 20px; text-align: center; background-color: lemonchiffon;
        padding-top: 6px; border: 2px solid gray; border-bottom: 0px; font-weight: bold;">
        請求先別入金データ取込</div>
    <!-- JHS入金データ取込 -->
    <table style="text-align: left; width: 400px; height: 200px;" id="Table2" class="mainTable"
        cellpadding="1" cellspacing="1" border="1">
        <tbody>
            <tr>
                <td style="width: 100px; height: 30px;" class="koumokuMei">
                    前回取込日時
                </td>
                <td>
                    <input type="text" id="TextJhsZenkaiTorikomiNitiji" runat="server" class="date readOnlyStyle"
                        maxlength="10" style="width: 120px" readonly="readonly" tabindex="-1" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px; height: 30px;" class="koumokuMei">
                    前回取込ファイル名
                </td>
                <td>
                    <input type="text" id="TextJhsZenkaiTorikomiFileMei" runat="server" class="readOnlyStyle"
                        maxlength="128" style="width: 200px" readonly="readonly" tabindex="-1" />
                </td>
            </tr>
            <%--
            <tr>
                <td style="width: 150px; height: 30px;" class="koumokuMei">
                    前回エラー有無
                </td>
                <td>
                    &nbsp;<asp:HyperLink ID="LinkJhsZenkaiErrorUmu" runat="server" NavigateUrl="~/NyuukinError.aspx"></asp:HyperLink></td>
            </tr>
--%>
            <tr>
                <td style="text-align: center; height: 188px;" colspan="2">
                    <asp:FileUpload ID="FileJhsNyuukinDataTorikomi" Style="width: 350px" runat="server" /><br />
                    <br />
                    <input type="button" id="ButtonJhsNyuukinDataTorikomi" runat="server" value="JHS入金データ取り込み"
                        style="font-size: 12px; width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
                    <input type="button" id="ButtonJhsNyuukinDataTorikomiNext" runat="server" value="JHS入金データ取り込み"
                        style="font-size: 12px; width: 200px; color: black; height: 30px; display: none;" /><br />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
