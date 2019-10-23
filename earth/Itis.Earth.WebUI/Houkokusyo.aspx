<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="Houkokusyo.aspx.vb" Inherits="Itis.Earth.WebUI.Houkokusyo" Title="EARTH 報告書" %>

<%@ Register Src="control/GyoumuKyoutuuCtrl.ascx" TagName="GyoumuKyoutuuCtrl" TagPrefix="uc1" %>
<%@ Register Src="control/SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl" TagPrefix="uc2" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //画面起動時にウィンドウサイズをディスプレイに合わせる
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);
        
        //変更前コントロールの値を退避して、該当コントロール(Hidden)に保持する
        function SetChangeMaeValue(strTaihiID, strTargetID){
           document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
        }

        //判定1コード検索処理を呼び出す
        function callHantei1Search(obj){
            objEBI("<%= HiddenHanteiSearchType1.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= HiddenHanteiSearchType1.clientID %>").value = "1";
                objEBI("<%= ButtonHantei1.clientID %>").click();
            }
        }
        
        //判定2コード検索処理を呼び出す
        function callHantei2Search(obj){
            objEBI("<%= HiddenHanteiSearchType2.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= HiddenHanteiSearchType2.clientID %>").value = "1";
                objEBI("<%= ButtonHantei2.clientID %>").click();
            }
        }
       
       //判定1キャンセル処理
        var tmpConfirmResultButtonId = "";
        function callHantei1Cancel(strMsg, btnId1){
            if(confirm(strMsg)){
            }else{
                objEBI("<%= HiddenHanteiSearchType1.clientID %>").value = "1";
                objEBI("<%= TextHantei1Cd.ClientID %>").value = objEBI("<%= HiddenHantei1CdOld.ClientID %>").value;
                tmpConfirmResultButtonId = btnId1;
            }
        }
        
        //登録処理前チェック
        function CheckTouroku(){           
            //判定変更チェック
            if(ChkHantei() == false) return false;
            return true;
        }
        
        //判定変更時処理
        //判定変更時は、判定変更理由を入力する
        function ChkHantei(){
            var blnChkFlg = true;
            
            //Old
            var strHantei1CdOld = objEBI("<%= HiddenHantei1CdOld.clientID %>").value; //判定1コードOld
            var strHantei2CdOld = objEBI("<%= HiddenHantei2CdOld.clientID %>").value; //判定2コードOld
            var strHanteiSetuzokuOld = objEBI("<%= HiddenHanteiSetuzokuMojiOld.clientID %>").value; //判定接続詞Old
            //画面
            var strHantei1Cd = objEBI("<%= TextHantei1Cd.clientID %>").value; //判定1コードOld
            var strHantei2Cd = objEBI("<%= TextHantei2Cd.clientID %>").value; //判定2コードOld
            var strHanteiSetuzoku = objEBI("<%= SelectHanteiSetuzokuMoji.clientID %>").value; //判定接続詞Old
            
            var objRiyuu = objEBI("<%= HiddenHanteiHenkouRiyuu.clientID %>");
            objRiyuu.value = ''; //初期化
            var retRiyuu = ''; //返却値(判定変更理由)
            
            //変更チェック
            if(strHantei1CdOld == ''){ //判定1=未入力
            }else{ //判定1=入力済
                if(CompVal(strHantei1CdOld,strHantei1Cd) == false){ //判定1
                    blnChkFlg = false;
                }
                if(CompVal(strHantei2CdOld,strHantei2Cd) == false){ //判定2
                    blnChkFlg = false;
                }
                if(CompVal(strHanteiSetuzokuOld,strHanteiSetuzoku) == false){ //判定接続詞
                    blnChkFlg = false;
                }            
            }
            
            if(blnChkFlg == false){
                //確認MSG表示
                var strMSG = "<%= Messages.MSG062E %>";
                strMSG = strMSG.replace('@PARAM1','判定変更理由');
                retRiyuu = prompt("\r\n" + strMSG,"");
                if(retRiyuu == null){ //キャンセルボタン押下時
                    return false;
                }
                if(retRiyuu.length == 0){ //OKボタン押下時
                    strMsg = "<%= Messages.MSG013E %>";
                    strMsg = strMsg.replace('@PARAM1','判定変更理由');
                    alert(strMsg);
                    return false;
                }

                //判定変更理由をHiddenセット
                objRiyuu.value = retRiyuu;
            }
            return true;
        }
        
        //第一引数と第二引数の値を比較し、同一の場合Trueを返す。
        function CompVal(strValOld, strVal){
            if(strValOld == strVal){
                return true;
            }else{
                return false;
            }
        }
        
       //Ajax実行時処理
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
            function InitializeRequestHandler(sender, args){
                objEBI("<%=HiddenAjaxFlg.ClientID %>").value=1;
        }
        //Ajaxロード後処理
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            if(tmpConfirmResultButtonId != ""){
                objEBI(tmpConfirmResultButtonId).click();
                tmpConfirmResultButtonId = "";
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=0;
        }
          
    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <!-- 画面上部・ヘッダ -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 150px;">
                    報告書
                </th>
                <th style="text-align: left;">
                    <input type="button" id="ButtonTouroku1" runat="server" value="登録 / 修正 実行" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia" />&nbsp;&nbsp;&nbsp;
                </th>
                <th style="text-align: right; font-size: 11px;">
                    最終更新者：
                    <asp:TextBox ID="TextSaisyuuKousinSya" runat="server" CssClass="readOnlyStyle" Style="width: 120px"
                        Text="" ReadOnly="true" TabIndex="-1" />
                    <br />
                    最終更新日時：
                    <asp:TextBox ID="TextSaisyuuKousinDate" runat="server" CssClass="readOnlyStyle" Style="width: 100px"
                        Text="" ReadOnly="true" TabIndex="-1" />
                </th>
            </tr>
            <tr>
                <td colspan="2" rowspan="1" style="height: 10px">
                </td>
                <td colspan="1" rowspan="1" style="height: 10px">
                </td>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="UpdatePanelHoll" UpdateMode="conditional" runat="server"
        RenderMode="Inline">
        <ContentTemplate>
            <uc1:GyoumuKyoutuuCtrl ID="ucGyoumuKyoutuu" runat="server" DispMode="HOUKOKUSYO" />
            <br />
            <div id="divTyousaHoukokusyo" runat="server">
                <asp:UpdatePanel ID="UpdatePanelHoukokusyo" UpdateMode="always" runat="server" RenderMode="inline"
                    ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="SelectJuri" />
                        <asp:AsyncPostBackTrigger ControlID="TextHassouDate" />
                        <asp:AsyncPostBackTrigger ControlID="TextSaihakkouDate" />
                        <asp:AsyncPostBackTrigger ControlID="ButtonHantei1" />
                        <asp:AsyncPostBackTrigger ControlID="ButtonHantei2" />
                    </Triggers>
                    <ContentTemplate>
                        <!-- hidden項目-->
                        <input type="hidden" id="HiddenAjaxFlg" runat="server" /><%--Ajax処理中フラグ--%>
                        <input type="hidden" id="HiddenUriageKeijouDate" value="" runat="server" />
                        <input type="hidden" id="HiddenZeikomiNyuukingaku" value="" runat="server" />
                        <input type="hidden" id="HiddenZeikomiHenkinkingaku" value="" runat="server" />
                        <input type="hidden" id="HiddenHassouDateMae" value="" runat="server" /><%--発送日前--%>
                        <input type="hidden" id="HiddenSaihakkouDateMae" value="" runat="server" /><%--再発行日前--%>
                        <input type="hidden" id="HiddenTyousaKaishaJigyousyoCd" value="" runat="server" />
                        <input type="hidden" id="HiddenHantei1CdOld" runat="server" value="" /><%--判定1コードOld--%>
                        <input type="hidden" id="HiddenHantei2CdOld" runat="server" value="" /><%--判定2コードOld--%>
                        <input type="hidden" id="HiddenHanteiSetuzokuMojiOld" runat="server" value="" /><%--判定接続文字コードOld--%>
                        <input type="hidden" id="HiddenHantei1Old" runat="server" value="" /><%--判定1コード名Old--%>
                        <input type="hidden" id="HiddenHantei1CdMae" runat="server" value="" /><%--判定1コード変更前--%>
                        <input type="hidden" id="HiddenHantei2CdMae" runat="server" value="" /><%--判定2コード変更前--%>
                        <input type="hidden" id="HiddenHosyousyoHakkouDate" runat="server" value="" /><%--保証書発行日--%>
                        <input type="hidden" id="HiddenUpdateTeibetuSeikyuuDatetime" runat="server" value="" /><%--邸別請求データ用更新日時--%>
                        <input type="hidden" id="HiddenTyousaKekkaTourokuDate" runat="server" value="" /><%--地盤テーブル・調査結果登録日時--%>
                        <input type="hidden" id="HiddenTyousaKekkaUpdateDate" runat="server" value="" /><%--地盤テーブル・調査結果更新日時--%>
                        <input type="hidden" id="HiddenHanteiHenkouRiyuu" runat="server" value="" /><%--物件履歴テーブル・判定変更理由--%>
                        <input type="hidden" id="HiddenKojHanteiKekkaFlgOld" runat="server" /><%--地盤テーブル・工事判定結果FLGOld--%>
                        <input type="hidden" id="HiddenKameitenCd" runat="server" /><%--加盟店コード--%>
                        <!-- 画面中央部・報告書情報 -->
                        <table style="text-align: left; width: 100%;" id="TableTyousaHoukokusyo" class="mainTable"
                            cellpadding="0" cellspacing="0">
                            <!-- ヘッダ部 -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="" colspan="6">
                                        <a id="AncTysHoukokusyo" runat="server">調査報告書情報</a>
                                        <input type="hidden" id="HiddenHoukokusyoInfoStyle" runat="server" value="inline" />
                                    </th>
                                </tr>
                            </thead>
                            <!-- ボディ部 -->
                            <tbody id="TBodyHoukokushoInfo" class="scrolltablestyle" runat="server">
                                <!-- 1行目 -->
                                <tr>
                                    <td style="width: 80px" class="koumokuMei">
                                        依頼担当者
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextIraiTantousya" runat="server" CssClass="readOnlyStyle" Style="width: 100px"
                                            Text="" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                    <td class="koumokuMei">
                                        調査会社
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaKaisyaMei" runat="server" CssClass="readOnlyStyle" Style="width: 260px"
                                            Text="" ReadOnly="true" TabIndex="-1" />
                                        <input type="hidden" id="HiddenDefaultSiireSakiCdForLink" runat="server" value="" />
                                    </td>
                                    <td class="koumokuMei">
                                        調査方法
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaHouhou" runat="server" CssClass="readOnlyStyle" Style="width: 260px"
                                            Text="" ReadOnly="true" TabIndex="-1" />
                                    </td>
                                </tr>
                                <!-- 2行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        調査実施日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaJissiDate" runat="server" CssClass="date readOnlyStyle"
                                            Text="" ReadOnly="true" TabIndex="-1" MaxLength="10" />
                                    </td>
                                    <td class="koumokuMei">
                                        計画書作成日
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextKeikakusyoSakuseiDate" runat="server" CssClass="date readOnlyStyle"
                                            Style="" Text="" ReadOnly="true" TabIndex="-1" MaxLength="10" />
                                    </td>
                                </tr>
                                <!-- 3行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        受理
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SelectJuri" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectJuri_SelectedIndexChanged">
                                        </asp:DropDownList><span id="SpanJuri" runat="server" style="display: none;"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        受理詳細
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextJuriSyousai" runat="server" CssClass="" Style="width: 300px"
                                            Text="" MaxLength="40" />
                                    </td>
                                </tr>
                                <!-- 4行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        受理日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextJuriDate" runat="server" CssClass="date" Style="" Text="" MaxLength="10" />
                                    </td>
                                    <td class="koumokuMei">
                                        発送日
                                    </td>
                                    <td colspan="3" runat="server" id="TdHassouDate">
                                        <asp:TextBox ID="TextHassouDate" runat="server" CssClass="date" Style="" Text=""
                                            MaxLength="10" OnTextChanged="TextHassouDate_ServerChange" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="koumokuMei">
                                        置換工事
                                    </td>
                                    <td colspan="5" style="padding: 0px">
                                        <table class="subTable" style="font-weight: bold;">
                                            <tr>
                                                <td>
                                                    写真受理：
                                                    <asp:DropDownList ID="SelectSyasinJuri" runat="server">
                                                    </asp:DropDownList><span id="SpanSyasinJuri" runat="server" style="display: none;"></span>
                                                </td>
                                                <td>
                                                    写真コメント：
                                                    <asp:TextBox ID="TextSyasinComment" runat="server" CssClass="" Style="width: 500px"
                                                        Text="" MaxLength="100" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 10行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        再発行日
                                    </td>
                                    <td colspan="5" runat="server" id="TdSaihakkouDate">
                                        <asp:TextBox ID="TextSaihakkouDate" runat="server" CssClass="date" Style="" Text=""
                                            MaxLength="10" OnTextChanged="TextSaihakkouDate_TextChanged" />
                                        &nbsp;&nbsp;&nbsp; <span id="SpanUriageSyorizumi" style="color: red; font-weight: bold;"
                                            runat="server"></span>
                                            <input type="hidden" id="HiddenUriageKeijyouDate" runat="server" />
                                    </td>
                                </tr>
                                <!-- 11行目 -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <asp:UpdatePanel ID="UpdatePanelSyouhinInfo" UpdateMode="conditional" runat="server"
                                            RenderMode="inline" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="TextKoumutenSeikyuuKingaku" />
                                                <asp:AsyncPostBackTrigger ControlID="TextJituseikyuuKingaku" />
                                                <asp:AsyncPostBackTrigger ControlID="TextHattyuusyoKingaku" />
                                                <asp:AsyncPostBackTrigger ControlID="SelectSeikyuuUmu" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <!-- hidden項目-->
                                                <input type="hidden" id="HiddenHosyousyoHakJykyOld" runat="server" /><%--保証書発行状況Old--%>
                                                <input type="hidden" id="HiddenHosyousyoHakJyky" runat="server" /><%--保証書発行状況--%>
                                                <input type="hidden" id="HiddenHosyousyoHakJykySetteiDate" runat="server" /><%--保証書発行状況設定日--%>
                                                <input type="hidden" id="HiddenZeiKbn" runat="server" /><%--税区分--%>
                                                <input type="hidden" id="HiddenZeiritu" value="0" runat="server" /><%--税率--%>
                                                <input type="hidden" id="HiddenNyuukingakuOld" runat="server" /><%--入金額--%>
                                                <input type="hidden" id="HiddenKoumutenSeikyuuKingakuMae" runat="server" /><%--工務店請求税抜請求金額前--%>
                                                <input type="hidden" id="HiddenJituseikyuuKingakuMae" runat="server" /><%--実請求税抜請求金額前--%>
                                                <input type="hidden" id="HiddenSeikyuuUmuMae" runat="server" /><%--請求有無前--%>
                                                <input type="hidden" id="HiddenJituseikyuu1Flg" runat="server" /><%--実請求税抜金額1フラグ--%>
                                                <table id="TableSyouhin" class="itemTable innerTable" cellpadding="0" cellspacing="0">
                                                    <tr class="shouhinTableTitle firstRow">
                                                        <td class="firstCol">
                                                            商品コード
                                                        </td>
                                                        <td class="itemMei_small" rowspan="2">
                                                            工務店請求<br />
                                                            税抜金額
                                                        </td>
                                                        <td class="itemMei_small" rowspan="2">
                                                            実請求<br />
                                                            税抜金額
                                                        </td>
                                                        <td>
                                                            消費税
                                                        </td>
                                                        <td rowspan="2">
                                                            請求書<br />
                                                            発行日
                                                        </td>
                                                        <td rowspan="2">
                                                            売上<br />
                                                            年月日
                                                        </td>
                                                        <td>
                                                            請求
                                                        </td>
                                                        <td rowspan="2">
                                                            発注書<br />
                                                            金額
                                                        </td>
                                                        <td rowspan="2">
                                                            発注書<br />
                                                            確定
                                                        </td>
                                                        <td rowspan="2">
                                                            発注書<br />
                                                            確認日
                                                        </td>
                                                    </tr>
                                                    <!-- 12行目 -->
                                                    <tr class="shouhinTableTitle">
                                                        <td class="firstCol">
                                                            商品名
                                                        </td>
                                                        <td>
                                                            税込金額
                                                        </td>
                                                        <td>
                                                            請求先
                                                        </td>
                                                    </tr>
                                                    <!-- 13行目 -->
                                                    <tr runat="server" id="TrSyouhin">
                                                        <td style="width: 200px" class="itemNm firstCol">
                                                            <asp:TextBox ID="TextItemCd" runat="server" CssClass="itemCd readOnlyStyle" Style="size: 8"
                                                                Text="" ReadOnly="true" TabIndex="-1" />
                                                            <uc2:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink" runat="server" />
                                                            <br />
                                                            <span id="SpanItemMei" class="itemNm" runat="server"></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKoumutenSeikyuuKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                                                                Text="" OnTextChanged="TextKoumutenSeikyuuKingaku_TextChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextJituseikyuuKingaku" runat="server" CssClass="kingaku" MaxLength="7"
                                                                Text="" OnTextChanged="TextJituseikyuuKingaku_TextChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextSyouhizei" runat="server" CssClass="kingaku readOnlyStyle" ReadOnly="true"
                                                                MaxLength="7" Text="" Style="size: 9" TabIndex="-1" />
                                                            <br />
                                                            <asp:TextBox ID="TextZeikomiKingaku" runat="server" CssClass="kingaku readOnlyStyle"
                                                                ReadOnly="true" MaxLength="7" Text="" Style="size: 9" TabIndex="-1" />
                                                            <asp:HiddenField ID="HiddenSiireGaku" runat="server" />
                                                            <asp:HiddenField ID="HiddenSiireSyouhiZei" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextSeikyuusyoHakkouDate" runat="server" CssClass="date" Style=""
                                                                Text="" MaxLength="10" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextUriageNengappi" runat="server" CssClass="date readOnlyStyle"
                                                                Style="" Text="" ReadOnly="true" TabIndex="-1" MaxLength="10" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="SelectSeikyuuUmu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectSeikyuuUmu_SelectedIndexChanged">
                                                                <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="無"></asp:ListItem>
                                                            </asp:DropDownList><span id="SpanSeikyuUmu" runat="server"></span>
                                                            <br />
                                                            <asp:TextBox ID="TextSeikyuusaki" runat="server" CssClass="readOnlyStyle" Style="width: 60px"
                                                                Text="" ReadOnly="true" TabIndex="-1" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextHattyuusyoKingaku" runat="server" CssClass="kingaku readOnlyStyle"
                                                                MaxLength="7" Text="" ReadOnly="true" TabIndex="-1" Style="width: 60px" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextHattyuusyoKakutei" runat="server" CssClass="readOnlyStyle" Style="width: 40px"
                                                                TabIndex="-1" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextHattyuusyoKakuninDate" runat="server" CssClass="date readOnlyStyle"
                                                                Style="" Text="" ReadOnly="true" TabIndex="-1" MaxLength="10" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <!-- 14行目 -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <table id="Table5" class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td style="width: 90px" class="koumokuMei firstCol">
                                                    再発行理由
                                                </td>
                                                <td id="TdSaihakkouRiyuu" runat="server">
                                                    <asp:TextBox ID="TextSaihakkouRiyuu" runat="server" CssClass="" Style="width: 300px"
                                                        Text="" MaxLength="40" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    入金額<br />
                                                    (税込)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextNyuukingaku" runat="server" CssClass="kingaku readOnlyStyle"
                                                        Style="size: 9" Text="0" ReadOnly="true" TabIndex="-1" MaxLength="7" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    残額
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextZangaku" runat="server" CssClass="kingaku readOnlyStyle" Style="size: 9"
                                                        Text="0" ReadOnly="true" TabIndex="-1" MaxLength="7" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <!-- 14行目 -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <table id="Table1" class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td class="koumokuMei firstCol" style="width: 90px;">
                                                    解析担当者
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKaisekiTantousyaCd" runat="server" CssClass="codeNumber" Style="width: 30px"
                                                        Text="" MaxLength="3" />
                                                    <asp:DropDownList ID="SelectKaisekiTantousya" runat="server">
                                                    </asp:DropDownList><span id="SpanKaisekiTantousya" runat="server" style="display: none;"></span>
                                                </td>
                                                <td class="koumokuMei">
                                                    解析承認者
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKaisekiSyouninsya" runat="server" CssClass="readOnlyStyle" Style="width: 150px"
                                                        Text="" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                                <td class="koumokuMei">
                                                    工事担当者
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKoujiTantousyaCd" runat="server" CssClass="codeNumber readOnlyStyle"
                                                        Style="width: 30px" Text="" ReadOnly="true" TabIndex="-1" MaxLength="7" />
                                                    <asp:TextBox ID="TextKoujiTantousya" runat="server" CssClass="readOnlyStyle" Style="width: 150px"
                                                        Text="" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="koumokuMei firstCol" style="width: 90px;">
                                                    判定
                                                </td>
                                                <td colspan="6" id="TdHantei" runat="server" style="width: 800px;">
                                                    <asp:TextBox ID="TextHantei1Cd" runat="server" CssClass="codeNumber" Style="width: 40px;"
                                                        MaxLength="5" Text="" /><input type="hidden" id="HiddenHanteiSearchType1" runat="server"
                                                            value="" />
                                                    <input type="button" id="ButtonHantei1" value="検索" class="gyoumuSearchBtn" runat="server"
                                                        onserverclick="ButtonHantei1_ServerClick" />
                                                    <span id="SpanHantei1" runat="server" style="width: 200px" class="readOnlyStyle"></span>
                                                    <asp:DropDownList ID="SelectHanteiSetuzokuMoji" runat="server" Width="70px">
                                                    </asp:DropDownList><span id="SpanHanteiSetuzokuMoji" runat="server" style="display: none;"></span>
                                                    <asp:TextBox ID="TextHantei2Cd" runat="server" CssClass="codeNumber" Style="width: 40px;"
                                                        MaxLength="5" Text="" /><input type="hidden" id="HiddenHanteiSearchType2" runat="server"
                                                            value="" />
                                                    <input type="button" id="ButtonHantei2" value="検索" class="gyoumuSearchBtn" runat="server"
                                                        onserverclick="ButtonHantei2_ServerClick" />
                                                    <span id="SpanHantei2" runat="server" style="width: 200px" class="readOnlyStyle"></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <!-- 画面下部・ボタン -->
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: center;">
                    <input type="button" id="ButtonTouroku2" runat="server" value="登録 / 修正 実行" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia" />&nbsp;
                </th>
            </tr>
        </tbody>
    </table>
</asp:Content>
