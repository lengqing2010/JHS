<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="MasterHiduke.aspx.vb" Inherits="Itis.Earth.WebUI.MasterHiduke" Title="EARTH ���t�}�X�^�X�V" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
    	<!-- 
		//�ύX�m�FMSG�\��
		function Js_ChkReg(strCTLID){
            var varRes;
            varRes = confirm('���t��ύX���܂��B��낵���ł����H');
            if (varRes == true){

            }else{

            }
		}
	-->
    </script>

    <!-- ��ʏ㕔�E�w�b�_[Table1] -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left">
                    ���t�}�X�^�ҏW
                </th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <table style="width: 300px;" class="mainTable" cellpadding="3">
        <!-- 1�s�� -->
        <tr>
            <th colspan="2" style="font-size: 16px;" class="">
                �����ݒ�p�f�[�^
            </th>
        </tr>
        <tr>
            <td style="width: 60px" class="koumokuMei">
                �敪
            </td>
            <td>
                <asp:DropDownList ID="SelectKubun" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <!-- 2�s�� -->
        <tr>
            <td colspan="2">
                <table id="Table2" class="miniTable" cellpadding="3">
                    <!-- 1�s�� -->
                    <tr>
                        <td style="width: 100px" class="koumokuMei">
                            �ۏ؏����s��
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelHosyousyoHakkou" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextHosyousyoHakkouDate" CssClass="date readOnlyStyle" MaxLength="10"
                                        Style="color: Red;" Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="TextHosyousyoHakkouHenkou" MaxLength="10" Style="" CssClass="date"
                                        Text="" runat="server" />&nbsp;&nbsp;<input type="button" id="ButtonHosyousyoHakkouHenkou"
                                            style="width: 40px" class="" value="�ύX" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="SelectKubun" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- 2�s�� -->
                    <tr>
                        <td class="koumokuMei">
                            �񍐏�������
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelHoukokusyoHassou" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextHoukokusyoHassouDate" MaxLength="10" Style="color: Red" CssClass="date readOnlyStyle"
                                        Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="TextHoukokusyoHassouHenkou" MaxLength="10" Style="" CssClass="date"
                                        Text="" runat="server" />&nbsp;&nbsp;<input type="button" id="ButtonHoukokusyoHassouHenkou"
                                            style="width: 40px" class="" value="�ύX" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="SelectKubun" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!-- 3�s�� -->
                    <tr>
                        <td class="koumokuMei">
                            �ۏ؏�NO�N��
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelHosyousyoNo" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextHosyousyoNo" MaxLength="10" Style="color: Red;" CssClass="date readOnlyStyle"
                                        Text="" runat="server" ReadOnly="true" TabIndex="-1" />
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="TextHosyousyoNoHenkou" MaxLength="7" Style="" CssClass="date" Text=""
                                        runat="server" />&nbsp;&nbsp;<input type="button" id="ButtonHosyousyoNoHenkou" style="width: 40px"
                                            class="" value="�ύX" runat="server" />
                                    <asp:HiddenField ID="HiddenUpdDateTime" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="SelectKubun" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
