<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SeikyuusyoSyuusei.aspx.vb"
    Inherits="Itis.Earth.WebUI.SeikyuusyoSyuusei" Title="EARTH 請求書印字内容編集" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script language="javascript" type="text/javascript">
        history.forward();

        //ウィンドウサイズ変更
        try{
            window.resizeTo(800,520);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }
        
        _d = document;

        /****************************************
         * 実行ボタン押下時の処理
         ****************************************/
        function executeConfirm(objCtrl){
            var objExeBtn = null;
            
            //請求先マスタ情報ボタン押下時
            if(objCtrl == objEBI("<%=BtnSeikyuuSakiCall.clientID %>")){
                alert('<%=Messages.MSG158W %>');
                objExeBtn = objEBI("<%=BtnSeikyuuSaki.clientID %>");
            }
        //実行ボタン押下
        objExeBtn.click();
        }
</script>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="SM1" runat="server">
            </asp:ScriptManager>
            <input type="hidden" id="HiddenUpdDatetime" runat="server" />
            <input type="hidden" id="HiddenSeikyuusyoNo" runat="server" />
            <input type="hidden" id="HiddenGamenMode" runat="server" />
            <input type="hidden" id="HiddenPrintTaisyougaiFlg" runat="server" />
            <%--画面タイトル--%>
            <table style="text-align: left; width: 750px; table-layout: fixed;" border="0" cellpadding="0"
                cellspacing="2" class="titleTable">
                <tbody>
                    <tr>
                        <th style="text-align: left;">
                            請求書印字内容編集 &nbsp;&nbsp;&nbsp;<input type="button" id="BtnClose" value="閉じる" onclick="window.close();"
                                runat="server" />&nbsp;
                            <input type="button" id="BtnSyusei" value="修正実行" runat="server" />&nbsp;
                            <input type="button" id="BtnInsatu" runat="server" style="background-color: #ffff69" />&nbsp;
                            &nbsp;
                            <input type="button" id="BtnTorikesi" runat="server" />
                        </th>
                    </tr>
                </tbody>
            </table>
            <br />
            <%--請求関連--%>
            <table style="width: 750px; table-layout: fixed;" cellpadding="2" class="mainTable mainTablefont">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            請求書NO</td>
                        <td colspan="5">
                            <asp:TextBox ID="TextSeikyuusyoNo" runat="server" MaxLength="15" Style="width: 120px;"
                                CssClass="number readOnlyStyle" ReadOnly="true" TabIndex="-1" /></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            請求先</td>
                        <td>
                            <asp:TextBox ID="TextSeikyuuSakiCd" runat="server" MaxLength="10" Style="width: 90px;"
                                CssClass="readOnlyStyle" ReadOnly="true" TabIndex="-1" /></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextSeikyuuSakiMei" runat="server" Style="width: 250px;" CssClass="readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" />
                            &nbsp;&nbsp;
                            <input type="button" id="BtnSeikyuuSakiCall" runat="server" value="請求先マスタ情報" onclick="executeConfirm(this);" />
                            <input type="button" id="BtnSeikyuuSaki" runat="server" value="請求先マスタ情報" style="display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            請求先名</td>
                        <td colspan="5">
                            <asp:TextBox ID="TextSeikyuuSakiMei1" runat="server" MaxLength="80" Style="width: 525px;"
                                CssClass="hissu" TabIndex="10" /></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            請求先名２</td>
                        <td colspan="5">
                            <asp:TextBox ID="TextSeikyuuSakiMei2" runat="server" MaxLength="80" Style="width: 525px;"
                                TabIndex="10" /></td>
                    </tr>
                </tbody>
            </table>
            <br />
            <%--住所関連--%>
            <table style="width: 750px; table-layout: fixed;" cellpadding="2" class="mainTable mainTablefont">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            郵便番号</td>
                        <td>
                            <asp:TextBox ID="TextYuubin" runat="server" MaxLength="8" Style="width: 60px;" CssClass="codeNumber hissu"
                                TabIndex="10" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            電話番号</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTellNo" runat="server" MaxLength="16" Style="width: 115px;"
                                CssClass="codeNumber" TabIndex="10" /></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" rowspan="2" style="width: 130px">
                            住所</td>
                        <td colspan="5">
                            <asp:TextBox ID="TextJuusyo1" runat="server" MaxLength="40" Style="width: 265px;"
                                CssClass="hissu" TabIndex="10" />
                            <asp:TextBox ID="TextJuusyo2" runat="server" MaxLength="40" Style="width: 265px;"
                                CssClass="" TabIndex="10" /></td>
                    </tr>
                </tbody>
            </table>
            <br />
            <%--金額関連--%>
            <table style="width: 750px; table-layout: fixed;" cellpadding="2" class="mainTable mainTablefont">
                <tbody>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            明細件数</td>
                        <td>
                            <asp:TextBox ID="TextMeisai" runat="server" Style="width: 90px;" CssClass="number readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            担当者</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTantousyaMei" runat="server" MaxLength="40" Style="width: 265px;"
                                CssClass="" TabIndex="10" /></td>
                    </tr>
                </tbody>
                <tbody style="display: inline;">
                    <tr class="tableSpacer">
                        <td colspan="6">
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            前回御請求額</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextZenSeikyuuGaku" runat="server" CssClass="kingaku readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" style="width:100px" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            今回御請求額</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextKonSeikyuuGaku" runat="server" CssClass="kingaku readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" style="width:100px" /></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            御入金額</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextNyuukinGaku" runat="server" CssClass="kingaku readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" style="width:100px" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            前回繰越残高</td>
                        <td>
                            <asp:TextBox ID="TextZenZandaka" runat="server" CssClass="kingaku readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" style="width:100px" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            繰越残高</td>
                        <td>
                            <asp:TextBox ID="TextKonZandaka" runat="server" CssClass="kingaku readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" style="width:100px" /></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            相殺額</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextSousaiGaku" runat="server" CssClass="kingaku readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" style="width:100px" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            調整額</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextTyouseiGaku" runat="server" CssClass="kingaku readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" style="width:100px" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            入金予定日</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextNyuukinYoteiDate" runat="server" MaxLength="10" Style="width: 70px;"
                                CssClass="date" TabIndex="10" /></td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" style="width: 130px">
                            データ作成時締め日</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextDataSakuseijiSimeDate" runat="server" Style="width: 30px;" CssClass="date readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            請求書発行日</td>
                        <td>
                            <asp:TextBox ID="TextSeikyuusyoHkDate" runat="server" Style="width: 70px;" CssClass="date readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" /></td>
                        <td class="koumokuMei" style="width: 115px">
                            請求書印刷日</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextSeikyuusyoInsatuDate" runat="server" Style="width: 70px;" CssClass="date readOnlyStyle"
                                ReadOnly="true" TabIndex="-1" /></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
