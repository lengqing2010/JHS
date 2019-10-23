<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FcMousikomiSyuusei.aspx.vb"
    Inherits="Itis.Earth.WebUI.FcMousikomiSyuusei" Title="EARTH FC申込修正" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH FC申込修正</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script language="javascript" type="text/javascript">
        history.forward();

        //ウィンドウサイズ変更
        try{
            window.resizeTo(875,768);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }


        //プルダウンの値が特定値の場合、指定のエレメントのReadOnlyを外し、項目名ラベルの表示状態を切替える
        function checkSonota(flgVal, targetId){
            if(flgVal){
                objEBI(targetId).style.visibility = "visible";

            }else{
                objEBI(targetId).value = "";
                objEBI(targetId).style.visibility = "hidden";
            }
        }
        
        //プルダウンの値が特定値の場合、指定のエレメントのReadOnlyを外し、項目名ラベルの表示状態を切替える(複数チェックを管理)
        function checkMultCtlSonota(flgVal1, flgVal2, targetId){
            if(flgVal1){
                objEBI(targetId).style.visibility = "visible";

            }else if(flgVal2){
                objEBI(targetId).style.visibility = "visible";
            }else{
                objEBI(targetId).value = "";
                objEBI(targetId).style.visibility = "hidden";
            }
        }
        
        //担当者連絡先TELの選択状況に応じて、指定のエレメントのReadOnlyを外し、項目名ラベルの表示状態を切替える
        function ChgDispTantousyaRenrakusaki(){
            var objRadioTysRenrakusakiTel = objEBI("<%= RadioTantuosyaTel1.clientID %>");            
            var objTextTysRenrakusakiTel = objEBI("<%= TextTantousyaTel.clientID %>");
            
            if(objRadioTysRenrakusakiTel.checked == true){
                //表示
                objTextTysRenrakusakiTel.style.visibility = "visible";

            }else{
                //非表示
                objTextTysRenrakusakiTel.value = "";
                objTextTysRenrakusakiTel.style.visibility = "hidden";
            }
        }
               
        //調査立会者の有無によってチェックボックスの表示切替を行なう
        function ChgDispTatiaisya(){       
            var objAri = objEBI("<%= RadioTTAri.clientID %>");            
            var objSesyusama = objEBI("<%= CheckTTSesyuSama.clientID %>");
            var objTantousya = objEBI("<%= CheckTTTantousya.clientID %>");
            var objSonota = objEBI("<%= CheckTTSonota.clientID %>");
            var objSonotaHosoku = objEBI("<%= TextAreaTTSonotaHosoku.clientID %>");

            //チェックボックスの活性化制御
            if(objAri.checked){
                //活性化
                objSesyusama.disabled = false;
                objTantousya.disabled = false;
                objSonota.disabled = false;
            }else{
                //チェックをはずす
                objSesyusama.checked = false;
                objTantousya.checked = false;
                objSonota.checked = false;
                //非活性化
                objSesyusama.disabled = true;
                objTantousya.disabled = true;
                objSonota.disabled = true;               
            }
            //チェックボックスのチェック状態に応じて、立会者(その他補足)のReadOnlyを外し、項目名ラベルの表示状態を切替える
            if(objSonota.checked == true){
                //表示
                objSonotaHosoku.style.visibility = "visible";

            }else{
                //非表示
                objSonotaHosoku.value = "";
                objSonotaHosoku.style.visibility = "hidden";
            }
        }
        
        //登録ボタン押下時の登録許可確認を行なう。
        function CheckTouroku(){            
            //Chk03 同時依頼棟数
            var objTBDoujiIraiTousuu = objEBI("<%= HiddenDoujiIraiTousuu.clientID %>");
            if(objTBDoujiIraiTousuu.value > "<%= EarthConst.MOUSIKOMI_DOUJI_IRAI_1_TOU %>" && objEBI("<%= HiddenTBChk03.clientID %>").value == ""){
                if(objEBI("<%= HiddenTBChk03.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG186C %>")){
                        objEBI("<%= HiddenTBChk03.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }
            
            return true;　//チェック完了
        }
        
        //登録処理前チェック
        function CheckJikkou(){            
            //基礎着工予定日 大小チェック
            if(!CheckDaiSyou(objEBI("<%= TextKYKsTyakkouYoteiDateFrom.clientID %>") ,objEBI("<%= TextKYKsTyakkouYoteiDateTo.clientID %>"),"基礎着工予定日"))return false;
            
            return true;
        }
        
        /**
         * 大小チェック
         * @return true/false
         */
        function CheckDaiSyou(objFrom,objTo,mess){
            if(objFrom.value != "" && objTo.value != ""){
                if(Number(removeSlash(objFrom.value)) > Number(removeSlash(objTo.value))){
                    alert("<%= Messages.MSG022E %>".replace("@PARAM1",mess));
                    objFrom.select();
                    return false;
                }
            }
            return true;
        }

        //重複物件チェック処理
        function ChgTyoufukuBukken(objThis){
            objEBI("<%= HiddenTyoufukuKakuninTargetId.clientID %>").value = objThis.id;
            objEBI("<%= ButtonExeTyoufukuCheck.clientID %>").click();
        }
        
</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="AjaxScriptManager" runat="server">
        </asp:ScriptManager>
        <%--Hidden--%>
        <input type="hidden" id="HiddenMousikomiNo" runat="server" />
        <input type="hidden" id="HiddenUpdDatetime" runat="server" />
        <input type="hidden" id="HiddenDoujiIraiTousuu" runat="server" />
        <%-- 同時依頼棟数 新規受注用Hidden --%>
        <input type="hidden" id="HiddenTBChk03" runat="server" />
        <%-- 同時依頼棟数2以上チェック用：Check03 --%>
        <%-- 画面タイトル --%>
        <table style="text-align: left; width: 825px; table-layout: fixed;" border="0" cellpadding="0"
            cellspacing="2" class="titleTable">
            <tr>
                <th style="text-align: left; width: 100px;">
                    FC申込修正</th>
                <th style="width: 100px;">
                    <input type="button" id="ButtonClose" runat="server" value="閉じる" onclick="window.close();" />
                </th>
                <th style="text-align: left;">
                    <input type="button" runat="server" id="ButtonSyuusei" value="修正" style="font-weight: bold;
                        font-size: 18px; width: 100px; color: black; height: 30px; background-color: yellow;" />&nbsp;
                    <input type="button" runat="server" id="ButtonHoryuu" value="保留" style="font-weight: bold;
                        font-size: 18px; width: 100px; color: black; height: 30px; background-color: yellow" />&nbsp;
                    <input type='button' runat="server" id="ButtonSinkiJutyuu" value="新規受注" style="font-weight: bold;
                        font-size: 18px; width: 100px; color: black; height: 30px; background-color: fuchsia" />&nbsp;
                    <input type="button" id="ButtonFcMousikomi" runat="server" value="FC申込" class="openMousikomi" />
                </th>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="UpdatePanelHoll" UpdateMode="conditional" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <table style="text-align: left; width: 825px;" id="TableMousikomi" class="mainTable"
                    cellpadding="1">
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            要注意情報</td>
                        <td colspan="3">
                            <textarea id="TextAreaYouTyuuiJouhou" runat="server" cols="90" onfocus="this.select();"
                                rows="3" style="ime-mode: active; font-family: Sans-Serif; color: Red;" tabindex="10">
                            </textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2" style="width: 265px;">
                            申込NO</td>
                        <td style="width: 300px;">
                            <asp:TextBox ID="TextMousikomiNo" CssClass="readOnlyStyle" ReadOnly="true" runat="server"
                                Style="width: 110px" TabIndex="-1" />
                        </td>
                        <td class="koumokuMei" style="width: 100px;">
                            受注状況</td>
                        <td style="width: 160px;">
                            <a id="LinkBukkenDirect" runat="server" target="BukkenDirectWin"><span id="SpanJutyuuJyky"
                                runat="server"></span></a>
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            区分</td>
                        <td>
                            <asp:TextBox ID="TextKbn" CssClass="readOnlyStyle" ReadOnly="true" runat="server"
                                Style="width: 110px" TabIndex="-1" />
                            <input type="hidden" id="HiddenKbn" runat="server" />
                        </td>
                        <td class="koumokuMei">
                            番号</td>
                        <td>
                            <asp:TextBox ID="TextHosyousyoNo" CssClass="readOnlyStyle" ReadOnly="true" MaxLength="10"
                                runat="server" Style="width: 70px" TabIndex="-1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            物件名寄コード</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextBukkenNayoseCd" CssClass="readOnlyStyle" ReadOnly="true" runat="server"
                                Style="width: 90px" TabIndex="-1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            担当調査会社</td>
                        <td>
                            <asp:TextBox ID="TextTantouTysKaisya" CssClass="readOnlyStyle" ReadOnly="true" Style="width: 260px" runat="server" TabIndex="-1" />
                        </td>
                        <td class="koumokuMei">
                            調査方法</td>
                        <td>
                            <asp:TextBox ID="TextBoxTysHouhou" CssClass="readOnlyStyle" ReadOnly="true" runat="server"
                                Style="width: 155px" TabIndex="-1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            申込調査会社</td>
                        <td>
                            <asp:TextBox ID="TextMousikomiTysKaisya" CssClass="readOnlyStyle" ReadOnly="true" Style="width: 260px" runat="server" TabIndex="-1" />
                        </td>
                        <td class="koumokuMei">
                            調査会社担当者</td>
                        <td>
                            <asp:TextBox ID="TextTysKaisyaCdTantousya" Style="width: 150px" runat="server" TabIndex="10" MaxLength="256" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            加盟店</td>
                        <td style="width: 560px;">
                            <asp:TextBox ID="TextKameitenCd" CssClass="readOnlyStyle" ReadOnly="true" runat="server"
                                Style="width: 260px" TabIndex="-1" />
                        </td>
                        <td class="koumokuMei">
                            取消</td>
                        <td>
                            <asp:TextBox ID="TextTorikesiRiyuu" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            商品1</td>
                        <td>
                            <asp:TextBox ID="TextSyouhin1Cd" CssClass="readOnlyStyle" ReadOnly="true" runat="server"
                                Style="width: 260px" TabIndex="-1" />
                        </td>
                        <td class="koumokuMei">
                            依頼日</td>
                        <td>
                            <asp:TextBox ID="TextIraiDate" CssClass="readOnlyStyle" ReadOnly="true" runat="server"
                                Style="width: 130px" MaxLength="18" TabIndex="-1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            加盟店様物件管理番号(契約NO)</td>
                        <td>
                            <asp:TextBox ID="TextKameiBukkenKanriNo" CssClass="codeNumber" runat="server" Style="width: 150px"
                                MaxLength="20" TabIndex="10" />
                        </td>
                        <td class="koumokuMei">
                            経由</td>
                        <td>
                            <asp:TextBox ID="TextKeiyu" CssClass="readOnlyStyle" ReadOnly="true" runat="server"
                                Style="width: 155px" TabIndex="-1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            調査連絡先_宛先・担当者名</td>
                        <td colspan="3" style="width: 580px;">
                            <asp:TextBox ID="TextTysRenrakusakiAtesakiMei" runat="server" Style="width: 415px"
                                MaxLength="60" TabIndex="10" />
                            <asp:TextBox ID="TextTantousya" runat="server" Style="width: 145px" MaxLength="20"
                                TabIndex="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            担当者連絡先TEL</td>
                        <td colspan="3" style="width: 560px;">
                            <input type="radio" name="TantousyaTel" id="RadioTantuosyaTel0" runat="server" value="0"
                                tabindex="10" />加盟店様TEL<span id="SpanTantuosyaTel0" runat="server"></span>
                            <input type="radio" name="TantousyaTel" id="RadioTantuosyaTel1" runat="server" value="1"
                                tabindex="10" />その他<span id="SpanTantuosyaTel1" runat="server"></span>
                            <asp:TextBox ID="TextTantousyaTel" CssClass="codeNumber" runat="server" Style="width: 150px"
                                MaxLength="20" TabIndex="10" />
                            <input type="radio" name="TantousyaTel" id="RadioTantuosyaTelDummy" runat="server"
                                style="display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            調査連絡先FAX</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTysRenrakusakiFax" CssClass="codeNumber" runat="server" Style="width: 145px"
                                MaxLength="20" TabIndex="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            調査連絡先MAIL</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTysRenrakusakiMail" CssClass="codeNumber" runat="server" Style="width: 450px"
                                MaxLength="64" TabIndex="10" />
                        </td>
                    </tr>
                    <!-- 物件名称[接頭辞:BM] -->
                    <tr>
                        <td class="koumokuMei" colspan="2" style="width: 115px;">
                            物件名称</td>
                        <td colspan="3" style="width: 560px;">
                            <asp:TextBox ID="TextBMBukkenMeisyou" runat="server" CssClass="hissu" Style="width: 500px;"
                                MaxLength="50" TabIndex="10" />様 邸
                        </td>
                    </tr>
                    <!-- 施主名有無 -->
                    <tr>
                        <td class="koumokuMei" colspan="2" style="width: 115px;">
                            施主名</td>
                        <td colspan="3" style="width: 560px;">
                            <input type="radio" name="RadioSesyumei" id="RadioSesyumei1" runat="server" tabindex="10"
                                class="hissu" />有<span id="SpanSesyumei1" runat="server"></span>
                            <input type="radio" name="RadioSesyumei" id="RadioSesyumei0" runat="server" tabindex="10"
                                class="hissu" />無<span id="SpanSesyumei0" runat="server"></span>
                    </tr>
                    <!-- 同時依頼棟数[接頭辞:DI] -->
                    <tr>
                        <td class="koumokuMei" colspan="2" style="width: 115px;">
                            同時依頼棟数</td>
                        <td colspan="3" style="width: 560px;">
                            <asp:TextBox ID="TextDIDoujiIraiTousuu" runat="server" CssClass="hissu codeNumber"
                                Style="width: 20px;" MaxLength="2" TabIndex="10" />棟
                        </td>
                    </tr>
                    <!-- 調査場所[接頭辞:TB] -->
                    <tr>
                        <td class="koumokuMei" style="border-right-width: 0px;" rowspan="3">
                            調査場所</td>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            市区町村</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTBBukkenJyuusyo1" runat="server" CssClass="hissu" Style="width: 400px;"
                                MaxLength="32" TabIndex="10" />
                            <asp:UpdatePanel ID="UpdatePanelTyoufukuCheck" UpdateMode="conditional" runat="server"
                                RenderMode="Inline">
                                <Triggers>
                                </Triggers>
                                <ContentTemplate>
                                    <input type="button" id="ButtonTyoufukuCheck" value="重複物件なし" class="" runat="server"
                                        tabindex="10" />
                                    <input id="ButtonExeTyoufukuCheck" style="display: none" type="button" value="重複チェック呼び出し"
                                        runat="server" onserverclick="ButtonExeTyoufukuCheck_ServerClick" />
                                    <input type="hidden" id="HiddenTyoufukuKakuninFlg1" value="" runat="server" />
                                    <input type="hidden" id="HiddenTyoufukuKakuninFlg2" value="" runat="server" />
                                    <input type="hidden" id="HiddenTyoufukuKakuninTargetId" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            番地１</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTBBukkenJyuusyo2" runat="server" Style="width: 400px;" MaxLength="32"
                                TabIndex="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            番地２</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTBBukkenJyuusyo3" runat="server" Style="width: 540px;" MaxLength="54"
                                TabIndex="10" />
                        </td>
                    </tr>
                    <!-- 調査希望[接頭辞:TK] -->
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            調査希望日</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTKTyousaKibouDate" Style="width: 100px" runat="server" CssClass="hissu hizuke"
                                MaxLength="10" TabIndex="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            調査希望(区分)</td>
                        <td>
                            <asp:TextBox ID="TextTKTyousaKibouKbn" Style="width: 35px" runat="server" MaxLength="4"
                                TabIndex="10" />
                        </td>
                        <td class="koumokuMei">
                            調査開始希望時間</td>
                        <td>
                            <asp:TextBox ID="TextTKTyousaKaisiKibouJikan" Style="width: 70px" runat="server"
                                MaxLength="8" TabIndex="10" />
                        </td>
                    </tr>
                    <!-- 調査 立会者[接頭辞:TT] -->
                    <tr>
                        <td class="koumokuMei" colspan="2" rowspan="2">
                            調査 立会者</td>
                        <td colspan="3">
                            <input type="radio" name="TyousaTaiaisya" id="RadioTTAri" runat="server" tabindex="10" />有
                            (<input type="checkbox" id="CheckTTSesyuSama" runat="server" value="1" tabindex="10" />施主様<span
                                id="SpanTTSesyuSama" runat="server"></span>
                            <input type="checkbox" id="CheckTTTantousya" runat="server" value="2" tabindex="10" />担当者<span
                                id="SpanTTTantousya" runat="server"></span>
                            <input type="checkbox" id="CheckTTSonota" runat="server" value="4" tabindex="10" />その他<span
                                id="SpanTTSonota" runat="server"></span>)
                            <input type="radio" name="TyousaTaiaisya" id="RadioTTNasi" runat="server" tabindex="10" />無
                            <input type="radio" name="TyousaTaiaisya" id="RadioTTTysDummy" runat="server" style="display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <textarea id="TextAreaTTSonotaHosoku" runat="server" cols="90" onfocus="this.select();"
                                rows="3" style="ime-mode: active; font-family: Sans-Serif" tabindex="10"></textarea>
                        </td>
                    </tr>
                    <!-- 基礎着工予定日[接頭辞:KY] -->
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            基礎着工予定日</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextKYKsTyakkouYoteiDateFrom" Style="width: 100px" runat="server"
                                CssClass="hizuke" MaxLength="10" TabIndex="10" />
                            ～
                            <asp:TextBox ID="TextKYKsTyakkouYoteiDateTo" Style="width: 100px" runat="server"
                                CssClass="hizuke" MaxLength="10" TabIndex="10" />
                            頃</td>
                    </tr>
                    <!-- 建物概要[接頭辞:TG] -->
                    <tr>
                        <td class="koumokuMei" rowspan="6">
                            建物概要</td>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;" rowspan="2">
                            構造種別</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTGKouzouSyubetuCd" runat="server" CssClass="pullCd" MaxLength="1"
                                TabIndex="10" />
                            <asp:DropDownList ID="SelectTGKouzouSyubetu" runat="server" TabIndex="10">
                            </asp:DropDownList><span id="SpanTGKouzouSyubetu" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="TextTGKouzouSyubetuSonota" runat="server" Style="ime-mode: active;
                                width: 550px;" MaxLength="80" TabIndex="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            新築･建替</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTGSintikuTatekaeCd" runat="server" CssClass="pullCd" MaxLength="1"
                                TabIndex="10" />
                            <asp:DropDownList ID="SelectTGSintikuTatekae" runat="server" TabIndex="10">
                            </asp:DropDownList><span id="SpanTGSintikuTatekae" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            階層(地上)</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTGKaisouTijyou" runat="server" CssClass="codeNumber" Style="width: 25px;"
                                MaxLength="2" TabIndex="10" />
                            <asp:DropDownList ID="SelectTGKaisouTijyou" runat="server" TabIndex="10">
                            </asp:DropDownList><span id="SpanTGKaisouTijou" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            建物用途</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTGTatemonoYouto" runat="server" CssClass="pullCd" MaxLength="1"
                                TabIndex="10" />
                            <asp:DropDownList ID="SelectTGTatemonoYouto" runat="server" TabIndex="10">
                            </asp:DropDownList><span id="SpanTGTatemonoYouto" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            設計許容支持力</td>
                        <td colspan="3">
                            <span class="koumokuMei"></span>
                            <asp:TextBox ID="TextTGSekkeiKyoyouSijiryoku" Style="width: 50px" runat="server"
                                CssClass="number" MaxLength="6" TabIndex="10" />
                            kN/㎡
                        </td>
                    </tr>
                    <!-- 地業および予定基礎状況[接頭辞:TY] -->
                    <tr>
                        <td class="koumokuMei" rowspan="4">
                            地業および<br />
                            予定基礎状況</td>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            根切り深さ</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTYNegiriHukasa" Style="width: 95px" runat="server" CssClass="number"
                                MaxLength="13" TabIndex="10" />
                            mm
                        </td>
                    </tr>
                    <tr>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;">
                            予定盛土厚さ</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTYYoteiMoritutiAtusa" Style="width: 95px" runat="server" CssClass="number"
                                MaxLength="13" TabIndex="10" />
                            mm
                        </td>
                    </tr>
                    <tr>
                        <td class="mousikomiTableSubTitle" style="font-size: 12px;" rowspan="2">
                            予定基礎</td>
                        <td colspan="3">
                            <asp:TextBox ID="TextTYYoteiKiso" runat="server" CssClass="pullCd" MaxLength="1"
                                TabIndex="10" />
                            <asp:DropDownList ID="SelectTYYoteiKiso" runat="server" TabIndex="10">
                            </asp:DropDownList><span id="SpanTYToteiKiso" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="TextTYYoteiKisoMemo" runat="server" Style="ime-mode: active; width: 550px;"
                                MaxLength="80" TabIndex="10" />
                        </td>
                    </tr>
                    <!-- 備考[接頭辞:BK] -->
                    <tr>
                        <td class="koumokuMei" colspan="2">
                            備考</td>
                        <td colspan="3">
                            <textarea id="TextAreaBKBikou" runat="server" cols="90" onfocus="this.select();"
                                rows="3" style="ime-mode: active; font-family: Sans-Serif" tabindex="10"></textarea>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <table style="text-align: right; width: 825px; table-layout: fixed;" border="0" cellpadding="0"
            cellspacing="2">
            <tr>
                <th style="width: 400px;">
                </th>
                <th style="text-align: right;">
                    <input type="button" runat="server" id="ButtonSyuusei2" value="修正" style="font-weight: bold;
                        font-size: 18px; width: 100px; color: black; height: 30px; background-color: yellow;
                        display: none;" tabindex="10" />&nbsp;
                    <input type='button' runat="server" id="ButtonSinkiJutyuu2" value="新規受注" style="font-weight: bold;
                        font-size: 18px; width: 100px; color: black; height: 30px; background-color: fuchsia"
                        tabindex="10" />
                </th>
            </tr>
        </table>
    </form>
    <!-- 検索画面遷移用フォーム -->
    <form id="searchForm" method="post" action="">
        <!-- 検索条件値格納用 -->
        <input type="hidden" id="sendSearchTerms" name="sendSearchTerms" />
        <!-- 検索結果セット先ID格納用 -->
        <input type="hidden" id="returnTargetIds" name="returnTargetIds" />
        <!-- 結果セット後実行ボタンID格納用 -->
        <input type="hidden" id="afterEventBtnId" name="afterEventBtnId" />
    </form>
</body>
</html>
