<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="NyuukinSyori.aspx.vb" Inherits="Itis.Earth.WebUI.NyuukinSyori" Title="EARTH ��������" %>

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
    <!-- ��ʏ㕔�E�w�b�_[Table1] -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    ��������
                </th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <!-- �����^�C�g�� -->
    <div style="width: 396px; height: 20px; text-align: center; background-color: lemonchiffon;
        padding-top: 6px; border: 2px solid gray; border-bottom: 0px; font-weight: bold;
        display: none;">
        �ꊇ��������</div>
    <!-- 1�s��[Table2] -->
    <table style="text-align: left; width: 400px; height: 120px;" id="" class="mainTable"
        cellpadding="1" cellspacing="1" border="1" style="display: none;">
        <!-- 1�s�� -->
        <tr>
            <td style="width: 100px" class="koumokuMei">
                ���������s��
            </td>
            <td>
                <input type="text" id="TextSeikyuusyoHakkoubiFrom" runat="server" maxlength="10"
                    class="date" onblur="checkDate(this);" />
                &nbsp;�`&nbsp;
                <input type="text" id="TextSeikyuusyoHakkoubiTo" runat="server" maxlength="10" class="date"
                    onblur="checkDate(this);" />
            </td>
        </tr>
        <!-- 2�s�� -->
        <tr>
            <td class="koumokuMei">
                �n��R�[�h
            </td>
            <td>
                <select id="SelectKeiretuCode" runat="server">
                </select>
            </td>
        </tr>
        <!-- 3�s�� -->
        <tr>
            <td colspan="2" style="text-align: center;">
                <input type="button" id="ButtonIkkatuNyuukinSyori" runat="server" value="�ꊇ��������"
                    style="font-size: 12px; width: 200px; color: black; height: 30px;" />
                <input type="button" id="ButtonIkkatuNyuukinSyoriNext" runat="server" value="�ꊇ��������"
                    style="font-size: 12px; width: 200px; color: black; height: 30px; display: none;" />
            </td>
        </tr>
    </table>
    <br />
    <!-- �����^�C�g�� -->
    <div style="float: left">
        <div style="width: 396px; height: 20px; text-align: center; background-color: lemonchiffon;
            padding-top: 6px; border: 2px solid gray; border-bottom: 0px; font-weight: bold;">
            �@�ʓ����f�[�^�捞</div>
        <!-- ��ʉ��� -->
        <table style="text-align: left; width: 400px; height: 200px; float: left" id="Table1"
            class="mainTable" cellpadding="1" cellspacing="1" border="1">
            <tbody>
                <tr>
                    <td style="width: 100px; height: 30px;" class="koumokuMei">
                        �O��捞����
                    </td>
                    <td>
                        <input type="text" id="TextZenkaiTorikomiNitiji" runat="server" class="date readOnlyStyle"
                            maxlength="10" style="width: 120px" readonly="readonly" tabindex="-1" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; height: 30px;" class="koumokuMei">
                        �O��捞�t�@�C����
                    </td>
                    <td>
                        <input type="text" id="TextZenkaiTorikomiFileMei" runat="server" class="readOnlyStyle"
                            maxlength="128" style="width: 200px" readonly="readonly" tabindex="-1" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px; height: 30px;" class="koumokuMei">
                        �O��G���[�L��
                    </td>
                    <td>
                        &nbsp;<asp:HyperLink ID="LinkZenkaiErrorUmu" runat="server" NavigateUrl="~/NyuukinError.aspx"></asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 150px;" colspan="2">
                        <asp:FileUpload ID="FileNyuukinDataTorikomi" Style="width: 350px" runat="server" /><br />
                        <br />
                        <input type="button" id="ButtonNyuukinDataTorikomi" runat="server" value="�����f�[�^��荞��"
                            style="font-size: 12px; width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
                        <input type="button" id="ButtonNyuukinDataTorikomiNext" runat="server" value="�����f�[�^��荞��"
                            style="font-size: 12px; width: 200px; color: black; height: 30px; display: none;" /><br />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!-- �����^�C�g�� -->
    <div style="width: 396px; height: 20px; text-align: center; background-color: lemonchiffon;
        padding-top: 6px; border: 2px solid gray; border-bottom: 0px; font-weight: bold;">
        ������ʓ����f�[�^�捞</div>
    <!-- JHS�����f�[�^�捞 -->
    <table style="text-align: left; width: 400px; height: 200px;" id="Table2" class="mainTable"
        cellpadding="1" cellspacing="1" border="1">
        <tbody>
            <tr>
                <td style="width: 100px; height: 30px;" class="koumokuMei">
                    �O��捞����
                </td>
                <td>
                    <input type="text" id="TextJhsZenkaiTorikomiNitiji" runat="server" class="date readOnlyStyle"
                        maxlength="10" style="width: 120px" readonly="readonly" tabindex="-1" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px; height: 30px;" class="koumokuMei">
                    �O��捞�t�@�C����
                </td>
                <td>
                    <input type="text" id="TextJhsZenkaiTorikomiFileMei" runat="server" class="readOnlyStyle"
                        maxlength="128" style="width: 200px" readonly="readonly" tabindex="-1" />
                </td>
            </tr>
            <%--
            <tr>
                <td style="width: 150px; height: 30px;" class="koumokuMei">
                    �O��G���[�L��
                </td>
                <td>
                    &nbsp;<asp:HyperLink ID="LinkJhsZenkaiErrorUmu" runat="server" NavigateUrl="~/NyuukinError.aspx"></asp:HyperLink></td>
            </tr>
--%>
            <tr>
                <td style="text-align: center; height: 188px;" colspan="2">
                    <asp:FileUpload ID="FileJhsNyuukinDataTorikomi" Style="width: 350px" runat="server" /><br />
                    <br />
                    <input type="button" id="ButtonJhsNyuukinDataTorikomi" runat="server" value="JHS�����f�[�^��荞��"
                        style="font-size: 12px; width: 200px; color: black; height: 30px;" onclick="executeConfirm(this);" /><br />
                    <input type="button" id="ButtonJhsNyuukinDataTorikomiNext" runat="server" value="JHS�����f�[�^��荞��"
                        style="font-size: 12px; width: 200px; color: black; height: 30px; display: none;" /><br />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
