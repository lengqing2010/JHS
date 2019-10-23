<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TeibetuIraiNaiyouCtrl.ascx.vb"
    Inherits="Itis.Earth.WebUI.TeibetuIraiNaiyouCtrl" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>

<script language="javascript" type="text/javascript">
    <!--

    var _gStrDoujiIrai = null; //同時依頼棟数

    /**
     * ## onChange代替処理用(同時依頼棟数専用) ##
     *   テンポラリグローバル変数に、対象オブジェクトの値をセットする
     *   ※onfocusイベントハンドラからコール
     * @param obj:対象オブジェクト
     */
    function setTempValueForOnBlur_DoujiIrai(obj){
      _gStrDoujiIrai = obj.value;
    }

    /**
     * ## onChange代替処理用(同時依頼棟数専用) ##
     *   テンポラリグローバル変数と、対象オブジェクトの値を比較する
     *     値がテンポラリと異なっている＝値が変更されている：True
     *     値がテンポラリと異なっていない＝値が変更されていない：False
     *   ※onblurイベントハンドラからコール
     * @param obj:対象オブジェクト
     * @return 変更されているか否か(Boolean)
     */
    function checkTempValueForOnBlur_DoujiIrai(obj){
      return _gStrDoujiIrai != obj.value;
    }

    //商品１設定処理呼び出し
    function callSetSyouhin1(objThis){
        //ボタン押下
        objEBI("<%= ButtonSetSyouhin1.clientID %>").click();
    }

    //商品１設定処理呼び出し
    function callSetSyouhin1TysGaiyou(objThis){
        //ボタン押下
        objEBI("<%= ButtonSetSyouhin1TysGaiyou.clientID %>").click();
    }
    
    /**
     * 表示中のエレメントを非表示にし、非表示中のエレメントを表示にする
     * @param strTargetId:表示切替対象エレメントのID
     * @param strTmpDispSettingId:display状態を保持するオブジェクトのID
     * @return
     */
    function changeDisplayIrai(strTargetId,strTmpDispSettingId) {
      var objTarget = objEBI('<%= TBodyIrai.clientID %>');
      var infoBar = objEBI('<%= IraiTitleInfobar.clientID %>');
      var kameitenCd = objEBI('<%= TextKameitenCd.clientID %>');
      var kameitenMei = objEBI('<%= TextKameitenMei.clientID %>');
      var TysKaisyaCd = objEBI('<%= TextTyousaKaishaCd.clientID %>');
      var TysKaisyaMei = objEBI('<%= TextTyousaKaishaNm.clientID %>');
      
      infoBar.innerText = '  【' +
                     kameitenCd.value + '】 【' + 
                     kameitenMei.value + '】 【' + 
                     TysKaisyaCd.value + '】 【' + 
                     TysKaisyaMei.value + '】';

      if (objTarget.style.display != "none") {
        objTarget.style.display = "none";
      } else {
        objTarget.style.display = "inline";
      }
      
      if(strTmpDispSettingId != undefined && strTmpDispSettingId != ""){
        objEBI(strTmpDispSettingId).value = objTarget.style.display;
      }
    }
    
    //調査概要と同時依頼棟数のチェック処理[共通]
    //調査概要変更時、同時依頼棟数変更時に行なう
    function callChkTysGaiyou(objThis){
        var objTysGaiyou = objEBI("<%= SelectTyousaGaiyou.clientID %>");
        var strTysGaiyouPreVal = objEBI("<%= HiddenTyousaGaiyou.clientID %>").value;
        var objIraiTousuu = objEBI("<%= TextDoujiIraiTousuu.clientID %>");
        var strErrMsg = "<%= Messages.MSG145E %>";
        var intIraiTousuu = 1;
        var ReturnVal = ''; //戻り値
        if(objThis.id.indexOf('SelectTyousaGaiyou') > 0){
            ReturnVal = strTysGaiyouPreVal;
        }else if(objThis.id.indexOf('TextDoujiIraiTousuu') > 0){
            ReturnVal = _gStrDoujiIrai;
        }
        
        //同時依頼棟数=未入力の場合、"1"として扱う
        if(objIraiTousuu.value == ""){
            intIraiTousuu = 1;
        }else{
            intIraiTousuu = Number(objIraiTousuu.value);
        }
        
        if(objTysGaiyou.value == "62" || objTysGaiyou.value == "63" || objTysGaiyou.value == "64" || objTysGaiyou.value == "65" ){
            if(intIraiTousuu < 10){ //9棟以下
                if(objTysGaiyou.value == "64" || objTysGaiyou.value == "65"){
                    strErrMsg = strErrMsg.replace("@PARAM1", "同時依頼棟数");
                    strErrMsg = strErrMsg.replace("@PARAM2", "9棟以下");
                    strErrMsg = strErrMsg.replace("@PARAM3", "調査概要");
                    alert(strErrMsg);
                    objThis.value = ReturnVal;
                    objThis.focus();
                    return false;
                }
            }else{ //10棟以上
                if(objTysGaiyou.value == "62" || objTysGaiyou.value == "63"){
                    strErrMsg = strErrMsg.replace("@PARAM1", "同時依頼棟数");
                    strErrMsg = strErrMsg.replace("@PARAM2", "10棟以上");
                    strErrMsg = strErrMsg.replace("@PARAM3", "調査概要");
                    alert(strErrMsg);
                    objThis.value = ReturnVal;
                    objThis.focus();
                    return false;
                }
            }
        }
        return true;
    }

    //調査概要とビルダー注意事項のチェック処理[共通]
    //調査概要変更時、同時依頼棟数変更時に行なう
    function callChkBuilder(objThis){
        var objTysGaiyou = objEBI("<%= SelectTyousaGaiyou.clientID %>");
        var strTysGaiyouPreVal = objEBI("<%= HiddenTyousaGaiyou.clientID %>").value;
        var objBuilderFlg = objEBI("<%= HiddenKameitenTyuuiJikou.clientID %>");
        var strErrMsg = "<%= Messages.MSG146E %>";
        var ReturnVal = ''; //戻り値
        
        if(objThis.id.indexOf('SelectTyousaGaiyou') > 0){
            ReturnVal = strTysGaiyouPreVal;
        }
        
        if(objTysGaiyou.value == "63" || objTysGaiyou.value == "65"){
            if(objBuilderFlg.value == 'False'){
                strErrMsg = strErrMsg.replace("@PARAM1", "加盟店");
                strErrMsg = strErrMsg.replace("@PARAM2", "地盤診断支払代行不可");
                alert(strErrMsg);
                objThis.value = ReturnVal;
                objThis.focus();
                return false;
            }
        }
        return true;
    }
        
    -->
</script>

<asp:ScriptManagerProxy ID="ScriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>
<table style="text-align: left; width: 100%;" id="Table1" class="mainTable" cellpadding="0"
    cellspacing="0" border="1">
    <!-- ヘッダ部 -->
    <thead>
        <tr>
            <th class="tableTitle" colspan="6" style="height: 20px">
                <a id="IraiDispLink" runat="server">依頼内容</a> <span id="IraiTitleInfobar" style="display: inline;"
                    runat="server"></span>
            </th>
        </tr>
    </thead>
    <!-- ボディ部 -->
    <tbody id="TBodyIrai" style="display: none;" runat="server">
        <tr>
            <td class="koumokuMei" style="width: 80px; height: 50px">
                加盟店
            </td>
            <td colspan="5">
                <asp:UpdatePanel ID="UpdatePanelKameiten" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <span id="SpanTyousaMitumoriFlg" style="color: red; font-weight: bold; float: right;"
                            runat="server"></span>
                        <input name="" type="text" id="TextKameitenCd" maxlength="5" class="codeNumber hissu"
                            style="width: 40px;" value="" runat="server" /><input type="hidden" id="HiddenKameitenTyuuiJikou"
                                runat="server" /><input type="hidden" id="HiddenKameitenTyuuiJikouPre" runat="server" />
                        <input name="" type="button" id="ButtonKameitenKensaku" value="検索" class="gyoumuSearchBtn"
                            onserverclick="ButtonKameitenKensaku_ServerClick" runat="server" />
                        &nbsp;
                        <input name="" type="text" id="TextKameitenMei" size="40" class="readOnlyStyle" readonly="readonly"
                            tabindex="-1" value="" runat="server" />
                        <input type="text" id="TextTorikesiRiyuu" runat="server" />
                        <input id="ButtonKameitenTyuuijouhou" class="btnKameitenTyuuijouhou" runat="server"
                            type="button" value="注意情報" />
                        <br />
                        <span id="SpanHattyuusyoFlg" style="color: red; font-weight: bold; float: right;"
                            runat="server"></span>&nbsp; 系列：<input name="keiretuNm" type="text" id="TextKeiretuNm"
                                readonly="readonly" tabindex="-1" style="width: 230px" class="readOnlyStyle"
                                runat="server" />
                        &nbsp; 営業所/法人名：<input name="EigyousyoMei" type="text" id="TextEigyousyoMei" readonly="readonly"
                            tabindex="-1" style="width: 230px" class="readOnlyStyle" runat="server" />
                        &nbsp; &nbsp;
                        <input id="TextJioSakiFlg" runat="server" style="font-weight: bold; width: 50px;
                            color: red; border-bottom-color: red;" class="readOnlyStyle" readonly="readOnly"
                            tabindex="-1" />
                        <asp:HiddenField ID="HiddenKbn" runat="server" />
                        <asp:HiddenField ID="HiddenKeiretuCd" runat="server" />
                        <asp:HiddenField ID="HiddenEigyousyoCd" runat="server" />
                        <asp:HiddenField ID="HiddenTysSeikyuuSaki" runat="server" />
                        <asp:HiddenField ID="HiddenHansokuSeikyuuSaki" runat="server" />
                        <asp:HiddenField ID="HiddenSeikyuuType" runat="server" />
                        <asp:HiddenField ID="HiddenHansokuSeikyuuType" runat="server" />
                        <asp:HiddenField ID="HiddenkameitenCdOld" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <!-- 1行目 -->
        <tr>
            <td class="koumokuMei">
                &nbsp;
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanelSyouhinKbn" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <input value="1" name="rdIT" type="radio" id="RadioSyouhinKbn1" runat="server" style="display: none"
                            disabled="disabled" />
                        <span id="SpanSyouhinKbn1" runat="server">60年保証</span>
                        <input value="2" name="rdIT" type="radio" id="RadioSyouhinKbn2" runat="server" style="display: none"
                            disabled="disabled" />
                        <span id="SpanSyouhinKbn2" runat="server">土地販売</span>
                        <input value="3" name="rdIT" type="radio" id="RadioSyouhinKbn3" runat="server" style="display: none"
                            disabled="disabled" />
                        <span id="SpanSyouhinKbn3" runat="server">リフォーム</span>
                        <input value="9" name="rdIT" type="radio" id="RadioSyouhinKbn9" runat="server" style="display: none"
                            disabled="disabled" checked />
                        <span id="SpanSyouhinKbn9" style="display: none" runat="server">&nbsp;</span>
                        <input type="hidden" id="itemKbPre" value="9" runat="server" />
                        <asp:HiddenField ID="HiddenSyouhinKbn" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="koumokuMei">
                同時依頼棟数
            </td>
            <td>
                <input name="" type="text" id="TextDoujiIraiTousuu" class="number" size="5" value="0"
                    runat="server" maxlength="4" />棟
            </td>
            <td class="koumokuMei">
                建物用途
            </td>
            <td>
                <input name="" type="text" id="TextTatemonoYoutoCd" maxlength="1" class="pullCd"
                    value="1" runat="server" /><asp:DropDownList ID="SelectTatemonoYouto" runat="server"
                        CssClass="hissu">
                    </asp:DropDownList>
            </td>
        </tr>
        <!-- 2行目 -->
        <tr>
            <td class="koumokuMei">
                調査会社
            </td>
            <td colspan="3">
                <span>
                    <asp:UpdatePanel ID="UpdatePanelTysKaisya" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <input type="text" id="TextTyousaKaishaCd" maxlength="7" style="width: 60px; color: blue;"
                                class="codeNumber hissu" runat="server" />
                            <input type="button" id="ButtonTyousaKaishaKensaku" value="検索" class="gyoumuSearchBtn"
                                onserverclick="ButtonTyousaKaishaKensaku_ServerClick" runat="server" />
                            &nbsp;
                            <input type="text" id="TextTyousaKaishaNm" style="width: 260px; color: blue;" class="readOnlyStyle"
                                readonly="readonly" tabindex="-1" value="" runat="server" /><asp:HiddenField ID="HiddenTysKaisyaCd"
                                    runat="server" />
                            <asp:HiddenField ID="HiddenTysKaisyaJigyousyoCd" runat="server" />
                            <asp:HiddenField ID="HiddenTysKaisyaNg" runat="server" />
                            <asp:HiddenField ID="HiddenTysKaisyaCdOld" runat="server" />
                            <asp:HiddenField ID="HiddenTysKaisyaNmOld" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span><span>
                    <asp:UpdatePanel ID="UpdatePanelSyouhin1Set" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <input id="ButtonSetSyouhin1" runat="server" onserverclick="ButtonSetSyouhin1_ServerClick"
                                type="button" value="商品１自動設定" style="display: none" />
                            <input id="ButtonSetSyouhin1TysGaiyou" runat="server" onserverclick="ButtonSetSyouhin1TysGaiyou_ServerClick"
                                type="button" value="商品１自動設定(調査概要)" style="display: none" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanelSyouhin1Seikyuu" runat="server" RenderMode="Inline"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="HiddenJituSeikyuuGaku" runat="server" />
                            <asp:HiddenField ID="HiddenKoumutenSeikyuugaku" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
            </td>
            <td class="koumokuMei">
                進捗ステータス</td>
            <td>
                <input id="TextStatusIf" runat="server" maxlength="3" class="readOnlyStyle" style="width: 100px;"
                    tabindex="-1" readonly="readOnly" />
                <input type="hidden" id="HiddenStatusIf" runat="server" /></td>
        </tr>
        <!-- 3行目 -->
        <tr>
            <td class="koumokuMei">
                商品1</td>
            <td colspan="5">
                <asp:UpdatePanel ID="UpdatePanelSyouhin1" UpdateMode="Conditional" runat="server"
                    RenderMode="Inline">
                    <ContentTemplate>
                        <asp:DropDownList ID="SelectSyouhin1" runat="server" Width="310px" CssClass="hissu">
                        </asp:DropDownList><span id="SpanSISyouhin1" runat="server"></span><input type="hidden"
                            id="HiddenSyouhin1Pre" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <!-- 4行目 -->
        <tr>
            <td class="koumokuMei">
                調査方法
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanelTyousaHouhou" runat="server" RenderMode="Inline"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <input name="chousaHouhouCode" type="text" id="TextTyousaHouhouCd" maxlength="2"
                            class="pullCd" runat="server" />
                        <asp:DropDownList ID="SelectTyousaHouhou" runat="server" CssClass="hissu">
                        </asp:DropDownList>
                        <asp:HiddenField ID="HiddenTyousaHouhou" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="koumokuMei">
                調査概要
            </td>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanelTyousaGaiyou" runat="server" RenderMode="Inline"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <select id="SelectTyousaGaiyou" runat="server" style="width: 266px">
                            <option selected="selected"></option>
                        </select>
                        <input id="HiddenTyousaGaiyouCd" runat="server" style="display: none" type="text" />
                        <asp:HiddenField ID="HiddenTyousaGaiyou" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </tbody>
</table>
<asp:HiddenField ID="HiddenKeiriGyoumuKengen" runat="server" />
<asp:HiddenField ID="HiddenHattyuuKingaku" runat="server" />
<asp:HiddenField runat="server" ID="HiddenOpenValue" />
<asp:HiddenField runat="server" ID="HiddenKeyValue" />
