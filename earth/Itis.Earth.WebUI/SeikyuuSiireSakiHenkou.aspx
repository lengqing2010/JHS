<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="SeikyuuSiireSakiHenkou.aspx.vb" Inherits="Itis.Earth.WebUI.SeikyuuSiireSakiHenkou"
    Title="EARTH 請求先・仕入先変更" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ Register Src="control/SeikyuuSiireSyouhinHeaderCtrl.ascx" TagName="HeaderUsrCtrl"
    TagPrefix="uc1" %>
<%@ Register Src="control/SeikyuuSiireSyouhinRecordCtrl.ascx" TagName="RecordUsrCtrl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        // 基本請求先をセット
        function setDefaultSeikyuuSaki(objCdId, objBrcId, objKbnId, objMeiId, objDefCdId, objDefBrcId, objDefKbnId, objDefMeiId, objOldId){
            objEBI(objCdId).value  = objEBI(objDefCdId).value;
            objEBI(objBrcId).value = objEBI(objDefBrcId).value;
            objEBI(objKbnId).value = objEBI(objDefKbnId).value;
            objEBI(objMeiId).value = objEBI(objDefMeiId).value;
            objEBI(objOldId).value = objEBI(objDefKbnId).value + "<%= EarthConst.SEP_STRING %>" 
                                    + objEBI(objDefCdId).value + "<%= EarthConst.SEP_STRING %>" 
                                    + objEBI(objDefBrcId).value
            objEBI(objCdId).style.fontWeight = "normal";
            objEBI(objCdId).style.color = "black";
            objEBI(objBrcId).style.fontWeight = "normal";
            objEBI(objBrcId).style.color = "black";
            objEBI(objKbnId).style.fontWeight = "normal";
            objEBI(objKbnId).style.color = "black";
            objEBI(objMeiId).style.fontWeight = "normal";
            objEBI(objMeiId).style.color = "black";
        }
        // 基本仕入先をセット
        function setDefaultSiireSaki(objCdId, objBrcId, objMeiId, objDefCdId, objDefBrcId, objDefMeiId, objOldId){
            objEBI(objCdId).value  = objEBI(objDefCdId).value;
            objEBI(objBrcId).value = objEBI(objDefBrcId).value;
            objEBI(objMeiId).value = objEBI(objDefMeiId).value;
            objEBI(objOldId).value = objEBI(objDefCdId).value + "<%= EarthConst.SEP_STRING %>" 
                                    + objEBI(objDefBrcId).value
            objEBI(objCdId).style.fontWeight = "normal";
            objEBI(objCdId).style.color = "black";
            objEBI(objBrcId).style.fontWeight = "normal";
            objEBI(objBrcId).style.color = "black";
            objEBI(objMeiId).style.fontWeight = "normal";
            objEBI(objMeiId).style.color = "black";
        }
        
    </script>

    <!-- 画面上部・ヘッダ[Table1] -->
    <table class="titleTable" style="text-align: left; width: 100%;" border="0" cellpadding="0"
        cellspacing="2">
        <tbody>
            <tr>
                <th style="text-align: left">
                    請求先・仕入先変更
                </th>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <table class="mainTable" style="text-align: left;">
        <tr>
            <td class="koumokuMei" style="font-weight: bold; width: 50px;">
                区分</td>
            <td colspan="2" style="width: 195px;">
                <asp:DropDownList ID="selectKbn" runat="server" CssClass="hissu">
                </asp:DropDownList>
                <td class="koumokuMei" style="width: 50px;">
                    番号</td>
                <td colspan="2" style="width: 80px;">
                    <input id="TextBangou" runat="server" maxlength="10" class="codeNumber hissu" style="width: 72px;" /></td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button ID="ButtonEdit" runat="server" Text="編集" CssClass="button" Style="width: 80px;" /></td>
        </tr>
    </table>
    <table id="tblBukkenInfo" runat="server" class="mainTable" style="text-align: left;
        margin-top: 5px;">
        <tr style="height: 21px;">
            <td colspan="8">
                <asp:Button ID="ButtonSubmit" runat="server" Text="修正実行" CssClass="button" Style="width: 80px;
                    background-color: fuchsia;" />
                <asp:Button ID="ButtonClear" runat="server" Text="クリア" CssClass="button" Style="width: 80px;" />
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="font-weight: bold; width: 50px;">
                区分</td>
            <td id="tdKbn" runat="server" style="width: 188px;">
                <asp:DropDownList ID="selectKbnLbl" runat="server">
                </asp:DropDownList>
                <span id="spanKbn" runat="server"></span>
            </td>
            <td class="koumokuMei" style="width: 34px;">
                番号</td>
            <td id="tdBangou" colspan="5" runat="server" style="width: 50px;">
                <asp:TextBox ID="TextBangouLbl" runat="server" MaxLength="10" CssClass="codeNumber"
                    Style="width: 72px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="koumokuMei" style="font-weight: bold; width: 50px;">
                施主名
            </td>
            <td id="tdSesyuMei" runat="server">
                <asp:TextBox ID="TextSesyuMeiLbl" runat="server" MaxLength="50" Style="width: 280px;"></asp:TextBox>
            </td>
            <td class="koumokuMei" style="font-weight: bold; width: 50px;">
                加盟店
            </td>
            <td colspan="3" id="tdKameiten" runat="server">
                <asp:TextBox ID="TextKameitenCdLbl" runat="server" MaxLength="5" Style="width: 40px;"></asp:TextBox>
                <asp:TextBox ID="TextKameitenMeiLbl" runat="server" MaxLength="40" Style="width: 230px;"></asp:TextBox>
            </td>
            <td class="koumokuMei" style="width: 40px;">
                取消</td>
            <td style="width: 110px;">
                <asp:TextBox ID="TextTorikesiRiyuuKihon" runat="server" TabIndex="-1" Width="100px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input type="button" id="ButtonDisp" value="一括展開" class="button" style="margin-top: 10px;"
        onclick="openDisplay('<%= TBodyDefaultSeikyuuSaki.clientID %>,<%= TBodyDefaultSiireSaki.clientID %>,<%= TBodySyouhin12.clientID %>,<%= TBodySyouhin3.clientID %>,<%= TBodySyouhin4.clientID %>,<%= TBodyKoj.clientID %>,<%= TBodyHoukokusyo.clientID %>,<%= TBodyHosyou.clientID %>');" />
    <table id="tblDefaultSeikyuuSaki" class="mainTable" style="text-align: left; margin-top: 5px;
        margin-right: 10px; float: left; width: 470px;">
        <tr>
            <th colspan="3" class="tableTitle" style="border-right: solid 1px gray; text-align: center;
                width: 400px;">
                <a href="JavaScript:changeDisplay('<%= TBodyDefaultSeikyuuSaki.clientID %>');changeDisplay('<%= TBodyDefaultSiireSaki.clientID %>');">
                    基本請求先</a></th>
            <th class="tableTitle" style="border-right: solid 1px gray; text-align: center;">
                請求先変更</th>
        </tr>
        <tbody id="TBodyDefaultSeikyuuSaki" style="display: none;" runat="server">
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    調査</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultTysSeikyuuCdLbl" runat="server" Style="width: 80px;"
                        CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultTysSeikyuuMeiLbl" runat="server" Style="width: 200px;"></asp:TextBox></td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultTysSeikyuuHenkouLbl" runat="server" CssClass="textCenter"
                        Style="width: 60px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    改良工事</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultKaiKojSeikyuuCdLbl" runat="server" Style="width: 80px;"
                        CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultKaiKojSeikyuuMeiLbl" runat="server" Style="width: 200px;"></asp:TextBox></td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultKaiKojSeikyuuHenkouLbl" runat="server" CssClass="textCenter"
                        Style="width: 60px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    追加工事</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultTuiKojSeikyuuCdLbl" runat="server" Style="width: 80px;"
                        CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultTuiKojSeikyuuMeiLbl" runat="server" Style="width: 200px;"></asp:TextBox></td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultTuiKojSeikyuuHenkouLbl" runat="server" CssClass="textCenter"
                        Style="width: 60px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    販促品</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultHnskSeikyuuCdLbl" runat="server" Style="width: 80px;"
                        CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultHnskSeikyuuMeiLbl" runat="server" Style="width: 200px;"></asp:TextBox></td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultHnskSeikyuuHenkouLbl" runat="server" CssClass="textCenter"
                        Style="width: 60px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    設計確認</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultKasiSeikyuuCdLbl" runat="server" Style="width: 80px;"
                        CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultKasiSeikyuuMeiLbl" runat="server" Style="width: 200px;"></asp:TextBox></td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultKasiSeikyuuHenkouLbl" runat="server" CssClass="textCenter"
                        Style="width: 60px;"></asp:TextBox></td>
            </tr>
        </tbody>
    </table>
    <table id="tblDefaultSiireSaki" class="mainTable" style="text-align: left; margin-top: 5px;
        width: 470px;">
        <tr>
            <th colspan="3" class="tableTitle" style="border-right: solid 1px gray; text-align: center;">
                <a href="JavaScript:changeDisplay('<%= TBodyDefaultSeikyuuSaki.clientID %>');changeDisplay('<%= TBodyDefaultSiireSaki.clientID %>');">
                    基本仕入先</a></th>
        </tr>
        <tbody id="TBodyDefaultSiireSaki" style="display: none;" runat="server">
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    調査</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultTysSiireCdLbl" runat="server" Style="width: 75px;" CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultTysSiireMeiLbl" runat="server" Style="width: 270px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    改良工事</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultKaiKojSiireCdLbl" runat="server" Style="width: 75px;"
                        CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultKaiKojSiireMeiLbl" runat="server" Style="width: 270px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    追加工事</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultTuiKojSiireCdLbl" runat="server" Style="width: 75px;"
                        CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultTuiKojSiireMeiLbl" runat="server" Style="width: 270px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 70px;">
                    設計確認</td>
                <td class="textCenter">
                    <asp:TextBox ID="TextDefaultKasiSiireCdLbl" runat="server" Style="width: 75px;" CssClass="textCenter"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextDefaultKasiSiireMeiLbl" runat="server" Style="width: 270px;"></asp:TextBox></td>
            </tr>
        </tbody>
    </table>
    <table class="mainTable" style="text-align: left; margin-top: 15px; width: 954px;">
        <tr>
            <th class="tableTitle" colspan="2">
                <a href="JavaScript:changeDisplay('<%= TBodySyouhin12.clientID %>');">商品１ ／ 商品２</a>
            </th>
        </tr>
        <tbody id="TBodySyouhin12" style="display: none;" runat="server">
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    商品１</td>
                <td id="tdSyouhin1" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="Syouhin1Header" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    商品２</td>
                <td id="tdSyouhin2" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="Syouhin2Header" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <table class="mainTable" style="text-align: left; width: 954px; border-top-width: 0px;">
        <tr>
            <th class="tableTitle" colspan="2">
                <a href="JavaScript:changeDisplay('<%= TBodySyouhin3.clientID %>');">商品３</a>
            </th>
        </tr>
        <tbody id="TBodySyouhin3" style="display: none;" runat="server">
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    商品３</td>
                <td id="tdSyouhin3" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="Syouhin3Header" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <table class="mainTable" style="text-align: left; width: 954px; border-top-width: 0px;">
        <tr>
            <th class="tableTitle" colspan="2">
                <a href="JavaScript:changeDisplay('<%= TBodySyouhin4.clientID %>');">商品４</a>
            </th>
        </tr>
        <tbody id="TBodySyouhin4" style="display: none;" runat="server">
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    商品４</td>
                <td id="tdSyouhin4" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="Syouhin4Header" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <table class="mainTable" style="text-align: left; margin-top: 10px; width: 954px;">
        <tr>
            <th class="tableTitle" colspan="2">
                <a href="JavaScript:changeDisplay('<%= TBodyKoj.clientID %>');">工事</a>
            </th>
        </tr>
        <tbody id="TBodyKoj" style="display: none;" runat="server">
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    改良<br />
                    &nbsp;工事</td>
                <td id="tdKaiKoj" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="KairyouKojHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    追加<br />
                    &nbsp;工事</td>
                <td id="TdTuiKoj" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="TuikaKojHeader" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <table class="mainTable" style="text-align: left; margin-top: 10px; width: 954px;">
        <tr>
            <th class="tableTitle" colspan="2">
                <a href="JavaScript:changeDisplay('<%= TBodyHoukokusyo.clientID %>');">報告書</a>
            </th>
        </tr>
        <tbody id="TBodyHoukokusyo" style="display: none;" runat="server">
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    調査<br />
                    報告書</td>
                <td id="TdTysHokoku" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="HeaderUsrCtrl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    工事<br />
                    報告書</td>
                <td id="TdKojHokoku" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="HeaderUsrCtrl2" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <table class="mainTable" style="text-align: left; margin-top: 10px; width: 954px;">
        <tr>
            <th class="tableTitle" colspan="2">
                <a href="JavaScript:changeDisplay('<%= TBodyHosyou.clientID %>');">保証</a>
            </th>
        </tr>
        <tbody id="TBodyHosyou" style="display: none;" runat="server">
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    保証書</td>
                <td id="TdHosyosyo" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="HeaderUsrCtrl3" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" style="width: 50px;">
                    解約<br />
                    &nbsp;払戻</td>
                <td id="TdKaiyaku" style="padding: 0px; border-left: 0px;" runat="server">
                    <uc1:HeaderUsrCtrl ID="HeaderUsrCtrl4" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <asp:HiddenField ID="HiddenRemakeInfo" runat="server" />
    <asp:HiddenField ID="HiddenJibanUpdDateTime" runat="server" />
</asp:Content>
