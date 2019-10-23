<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="KairyouKouji.aspx.vb" Inherits="Itis.Earth.WebUI.KairyouKouji" Title="EARTH 改良工事" %>

<%@ Register Src="control/NyuukinZangakuCtrl.ascx" TagName="NyuukinZangakuCtrl" TagPrefix="uc2" %>
<%@ Register Src="control/GyoumuKyoutuuCtrl.ascx" TagName="GyoumuKyoutuuCtrl" TagPrefix="uc1" %>
<%@ Register Src="control/SeikyuuSiireLinkCtrl.ascx" TagName="SeikyuuSiireLinkCtrl"
    TagPrefix="uc3" %>
<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //画面起動時にウィンドウサイズをディスプレイに合わせる
        window.moveTo(0, 0);
        window.resizeTo(window.screen.availWidth, window.screen.availHeight);
        
        _d = document;
        
        //onload後処理
        function funcAfterOnload(){
        
            //登録完了時に次の物件へ遷移するポップアップを表示する
            if(objEBI("<%=callModalFlg.clientID %>").value == "<%=Boolean.TrueString %>"){
                objEBI("<%=callModalFlg.clientID %>").value = ""
                rtnArg = callModalBukken("<%=UrlConst.POPUP_BUKKEN_SITEI %>","<%=UrlConst.KAIRYOU_KOUJI %>","koujiA",true,"<%= ucGyoumuKyoutuu.Kubun %>","<%= ucGyoumuKyoutuu.Bangou %>");
                if(rtnArg == "null" && window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>"){
                    window.close();
                    return;
                }
            }else if(objEBI("<%=ButtonDummy.clientID %>").value == '0'){
               //ダミーボタンにてPostBack
               objEBI("<%=ButtonDummy.clientID %>").click(); 
            }
        }

        //変更前コントロールの値を退避して、該当コントロール(Hidden)に保持する
        function SetChangeMaeValue(strTaihiID, strTargetID){
           document.getElementById(strTaihiID).value = document.getElementById(strTargetID).value;
        }

        //改良工事・工事会社コード検索処理を呼び出す
        var JSkoujiKaisyaSearchTypeKj = 0;
        function callKjKoujiKaisyaSearch(obj){
            objEBI("<%= HiddenKoujiKaisyaSearchTypeKj.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= HiddenKoujiKaisyaSearchTypeKj.clientID %>").value = "1";
                objEBI("<%= ButtonKjKoujiKaisyaSearch.clientID %>").click();
            }
        }

        //追加工事・工事会社コード検索処理を呼び出す
        var JSkoujiKaisyaSearchTypeTj = 0;
        function callTjKoujiKaisyaSearch(obj){
            objEBI("<%= HiddenKoujiKaisyaSearchTypeTj.clientID %>").value = "";
            if(obj.value == ""){
                objEBI("<%= HiddenKoujiKaisyaSearchTypeTj.clientID %>").value = "1";
                objEBI("<%= ButtonTjKoujiKaisyaSearch.clientID %>").click();
            }
        }
        
        //登録ボタン押下時の登録許可確認を行なう。
        function CheckTouroku(){
        
            //Chk05 改良工事 売上金額税抜金額
            var objKjUriage = objEBI("<%= TextKjUriageZeinukiKingaku.clientID %>");
            var objKjSiire = objEBI("<%= TextKjSiireZeinukiKingaku.clientID %>");
            var chk1 = removeFigure(objKjUriage.value);
            var chk2 = removeFigure(objKjSiire.value);

            //売上金額税抜金額 ＜ 仕入金額税抜金額の場合
            if (Number(chk1) < Number(chk2)){      
                if(objEBI("<%= HiddenKjChk05.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG081C %>")){
                        objEBI("<%= HiddenKjChk05.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }
            
            //Chk05 追加工事 売上金額税抜金額
            var objTjUriage = objEBI("<%= TextTjUriageZeinukiKingaku.clientID %>");
            var objTjSiire = objEBI("<%= TextTjSiireZeinukiKingaku.clientID %>");
            var chk3 = removeFigure(objTjUriage.value);
            var chk4 = removeFigure(objTjSiire.value);

            //売上金額税抜金額 ＜ 仕入金額税抜金額の場合
            if (Number(chk3) < Number(chk4)){
                if(objEBI("<%= HiddenTjChk05.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG084C %>")){
                        objEBI("<%= HiddenTjChk05.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }

            //Chk14 改良工事 完工速報着日
            var objKjTyakuDate = objEBI("<%= TextKjKankouSokuhouTyakuDate.clientID %>");
            if(objKjTyakuDate.value != "" && objEBI("<%= HiddenKjChk14.clientID %>").value == "0"){
                if(objEBI("<%= HiddenKjChk14.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG082C %>")){
                        objEBI("<%= HiddenKjChk14.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }

            //Chk15 追加工事 完工速報着日
            var objTjTyakuDate = objEBI("<%= TextTjKankouSokuhouTyakuDate.clientID %>");
            if(objTjTyakuDate.value != "" && objEBI("<%= HiddenTjChk15.clientID %>").value == "0"){
                if(objEBI("<%= HiddenTjChk15.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG083C %>")){
                        objEBI("<%= HiddenTjChk15.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }

            //Chk08 改良工事 請求書発行日＝入力、かつ改良工事日＝未入力
            var objKjSeikyuusyoDate = objEBI("<%= TextKjSeikyuusyoHakkouDate.clientID %>");
            var objKjKoujiDate = objEBI("<%= TextKjKoujiDate.clientID %>");
            if(objKjSeikyuusyoDate.value != "" && objKjKoujiDate.value == ""){
                if(objEBI("<%= HiddenKjSeikyuusyoHakkouDateMsg1.clientID %>").value != "1" && objEBI("<%= HiddenKjChk08.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG078W %>")){
                        objEBI("<%= HiddenKjChk08.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }

            //Chk09 請求書発行日＜改良工事日の場合
            //請求書発行日＝入力、かつ工事日＝入力の場合
            var varMsg09 = "<%= Messages.MSG066C %>";
            if(objKjSeikyuusyoDate.value != "" && objKjKoujiDate.value != ""){
                if(Number(removeSlash(objKjSeikyuusyoDate.value)) < Number(removeSlash(objKjKoujiDate.value))){
                    if(objEBI("<%= HiddenKjSeikyuusyoHakkouDateMsg2.clientID %>").value != "1" && objEBI("<%= HiddenKjChk09.clientID %>").value != "1"){
                        if(confirm(varMsg09.replace('@PARAM1','工事日'))){
                            objEBI("<%= HiddenKjChk09.clientID %>").value = "1";
                        }else{
                            return false;
                        }
                    }
                }
            }

            //Chk10 追加工事 請求書発行日＝入力、かつ追加工事日＝未入力
            var objTjSeikyuusyoDate = objEBI("<%= TextTjSeikyuusyoHakkouDate.clientID %>");
            var objTjKoujiDate = objEBI("<%= TextTjKoujiDate.clientID %>");
            if(objTjSeikyuusyoDate.value != "" && objTjKoujiDate.value == ""){
                if(objEBI("<%= HiddenTjSeikyuusyoHakkouDateMsg1.clientID %>").value != "1" && objEBI("<%= HiddenTjChk10.clientID %>").value != "1"){
                    if(confirm("<%= Messages.MSG085W %>")){
                        objEBI("<%= HiddenTjChk10.clientID %>").value = "1";
                    }else{
                        return false;
                    }
                }
            }

            //Chk11 請求書発行日＜改良工事日の場合
            //請求書発行日＝入力、かつ工事日＝入力の場合
            var varMsg11 = "<%= Messages.MSG066C %>";
            if(objTjSeikyuusyoDate.value != "" && objTjKoujiDate.value != ""){
                if(Number(removeSlash(objTjSeikyuusyoDate.value)) < Number(removeSlash(objTjKoujiDate.value))){
                    if(objEBI("<%= HiddenTjSeikyuusyoHakkouDateMsg2.clientID %>").value != "1" && objEBI("<%= HiddenTjChk11.clientID %>").value != "1"){
                        if(confirm(varMsg11.replace('@PARAM1','追加工事日'))){
                            objEBI("<%= HiddenTjChk11.clientID %>").value = "1";
                        }else{
                            return false;
                        }
                    }
                }
            }
            
            return true;　//チェック完了
        }
        
        //工事種別変更時の判別メッセージ
        function checkKoujiSyubetu(mess1,mess2,pullId,maeId){
            //空白時は未チェック
            if(objEBI(pullId).value==""){
                return false;
            }
            
            _d = document;
            if(confirm(mess1)){
            
            }else{
                if(confirm(mess2)){
                    if(objEBI(maeId).value==""){
                        objEBI(pullId).selectedIndex = 0;
                    }else{
                        objEBI(pullId).value = objEBI(maeId).value;
                    }
                    objEBI(pullId).onchange();
                }
            }
        }
        
        //商品コード変更時の確認メッセージ
        function ChkSyohinCd(mess1,pullId,maeId){
            //空白時は未チェック
            if(objEBI(pullId).value==""){
                return false;
            }
            
            //_d = document;
            if(confirm(mess1)){
            }else{
                document.getElementById(pullId).selectedIndex = 0;
                document.getElementById(pullId).onchange();
            }
        }
      
        //改良工事会社コードをHiddenへ退避
        function copyToKjHidden(obj){
            objEBI("<%=HiddenKjKojKaisyaCd.clientID %>").value = obj.value
        }
        
        //追加工事会社コードをHiddenへ退避
        function copyToTjHidden(obj){
            objEBI("<%=HiddenTjKojKaisyaCd.clientID %>").value = obj.value
        }


    </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <!-- 画面上部・ヘッダ -->
    <input type="hidden" id="callModalFlg" runat="server" />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 150px;">
                    改良工事
                </th>
                <th style="text-align: left;">
                    <input type="button" id="ButtonTouroku1" value="登録 / 修正 実行" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                        runat="server" />&nbsp;&nbsp;&nbsp;
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
    <asp:UpdatePanel ID="UpdatePanelKairyouKouji" UpdateMode="conditional" runat="server"
        RenderMode="Inline">
        <ContentTemplate>
            <!-- hidden項目-->
            <input type="hidden" id="HiddenUpdateTeibetuSeikyuuDatetimeKj" runat="server" value="" /><%--邸別請求データ用更新日時[改良工事]--%>
            <input type="hidden" id="HiddenUpdateTeibetuSeikyuuDatetimeTj" runat="server" value="" /><%--邸別請求データ用更新日時[追加工事]--%>
            <input type="hidden" id="HiddenUpdateTeibetuSeikyuuDatetimeKh" runat="server" value="" /><%--邸別請求データ用更新日時[報告書]--%>
            <input type="hidden" id="HiddenKjSyouhinCdOld" runat="server" value="" /><%--改良工事・商品コードOld--%>
            <input type="hidden" id="HiddenKjUriageNengappiOld" runat="server" value="" /><%--改良工事・売上年月日Old--%>
            <input type="hidden" id="HiddenTyousaGaiyou" runat="server" value="" /><%--地盤・調査概要--%>
            <input type="hidden" id="HiddenKameitenCd" runat="server" /><%--加盟店コード--%>
            <input type="hidden" id="HiddenHosyouSyouhinUmuOld" runat="server" /><%--保証商品有無Old--%>
            <input type="hidden" id="HiddenKjKoujiDateOld" runat="server" /><%--改良工事・工事日Old--%>
            <input type="hidden" id="HiddenKjKankouSokuhouTyakuDateOld" runat="server" /><%--改良工事・完工速報着日Old--%>
            <input type="hidden" id="HiddenTjKoujiDateOld" runat="server" /><%--追加工事・工事日Old--%>
            <input type="hidden" id="HiddenTjKankouSokuhouTyakuDateOld" runat="server" /><%--追加工事・完工速報着日Old--%>
            <uc1:GyoumuKyoutuuCtrl ID="ucGyoumuKyoutuu" runat="server" DispMode="KAIRYOU" />
            <br />
            <div id="divKairyouKouji" runat="server">
                <asp:UpdatePanel ID="UpdatePanelKairyouKoujiInfo" UpdateMode="conditional" runat="server"
                    RenderMode="inline" ChildrenAsTriggers="true">
                    <Triggers>
                        <%--業務共通タブ--%>
                        <asp:AsyncPostBackTrigger ControlID="ucGyoumuKyoutuu" />
                    </Triggers>
                    <ContentTemplate>
                        <input type="button" id="ButtonDummy" runat="server" value="0" style="display: none"
                            onserverclick="ButtonDummy_ServerClick" />
                        <!-- [改良工事情報] -->
                        <table style="text-align: left; width: 100%;" id="TableKairyouKouji" class="mainTable"
                            cellpadding="0" cellspacing="0">
                            <!-- ヘッダ部 -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="" colspan="3">
                                        <a id="AncKairyouKouji" runat="server">改良工事情報</a>
                                        <input type="hidden" id="HiddenKairyouKoujiInfoStyle" runat="server" value="inline" />
                                    </th>
                                    <th style="background-color: #ccffff; text-align: right;">
                                        <input type="button" id="ButtonSyouhin4" runat="server" value="商品４" class="openHosyousyoDB" /></th>
                                </tr>
                            </thead>
                            <!-- ボディ部 -->
                            <tbody id="TBodyKairyouKoujiInfo" runat="server">
                                <!-- 1行目 -->
                                <tr>
                                    <td class="koumokuMei" style="width: 100px; height: 20px">
                                        調査会社
                                    </td>
                                    <td style="height: 20px;" colspan="3">
                                        <asp:TextBox ID="TextTyousaKaisyaMei" Style="width: 350px" Text="" CssClass="readOnlyStyle"
                                            ReadOnly="true" TabIndex="-1" runat="server" />
                                        <input type="hidden" id="HiddenDefaultSiireSakiCdForLink" runat="server" value="" />
                                    </td>
                                </tr>
                                <!-- 2行目 -->
                                <tr>
                                    <td class="koumokuMei" style="width: 100px">
                                        調査実施日
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextTyousaJissiDate" CssClass="date readOnlyStyle" MaxLength="10"
                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                    <td class="koumokuMei" style="width: 100px;">
                                        計画書作成日
                                    </td>
                                    <td style="width: 350px">
                                        <asp:TextBox ID="TextKeikakusyoSakuseiDate" CssClass="date readOnlyStyle" MaxLength="10"
                                            Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableSpacer" colspan="4">
                                    </td>
                                </tr>
                                <!-- 3行目[Table1] -->
                                <tr>
                                    <td colspan="4" style="padding: 0px">
                                        <table style="" id="Table1" class="innerTable" cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <td class="koumokuMei firstCol">
                                                    判定者
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextHanteisya" Style="width: 9em;" CssClass="readOnlyStyle" Text=""
                                                        ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="koumokuMei" style="">
                                                    工事担当者
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TextKoujiTantousyaCd" MaxLength="3" Style="width: 30px;" CssClass="codeNumber"
                                                        Text="" runat="server" />
                                                    <asp:DropDownList ID="SelectKoujiTantousya" runat="server">
                                                    </asp:DropDownList><span id="SpanKoujiTantousyaCd" style="display: none;" runat="server"></span>
                                                </td>
                                                <td class="koumokuMei">
                                                    判定
                                                </td>
                                                <td style="width: 350px">
                                                    <span id="SpanHantei1" runat="server" style="width: 12em" class="readOnlyStyle"></span>
                                                    <input type="hidden" id="HiddenHantei1Cd" runat="server" value="" />
                                                    <span id="SpanHanteiSetuzokuMoji" runat="server" style="width: 50px" class="readOnlyStyle">
                                                    </span>
                                                    <input type="hidden" id="HiddenHanteiSetuzokuMoji" runat="server" value="" />
                                                    <span id="SpanHantei2" runat="server" style="width: 12em" class="readOnlyStyle"></span>
                                                    <input type="hidden" id="HiddenHantei2Cd" runat="server" value="" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableSpacer" colspan="4">
                                    </td>
                                </tr>
                                <!-- 4行目[Table2] -->
                                <!-- [改良工事] -->
                                <tr>
                                    <td colspan="4" style="padding: 0px">
                                        <input type="hidden" id="HiddenKjZeiKbn" runat="server" /><%--改良工事・税区分--%>
                                        <input type="hidden" id="HiddenTjZeiKbn" runat="server" /><%--追加工事・税区分--%>
                                        <input type="hidden" id="HiddenKjZeiritu" value="0" runat="server" /><%--改良工事・税率--%>
                                        <input type="hidden" id="HiddenTjZeiritu" value="0" runat="server" /><%--追加工事・税率--%>
                                        <input type="hidden" id="HiddenKjKoujiKaisyaCdMae" runat="server" value="" /><%--改良工事・工事会社コード変更前--%>
                                        <input type="hidden" id="HiddenTjKoujiKaisyaCdMae" runat="server" value="" /><%--追加工事・工事会社コード変更前--%>
                                        <input type="hidden" id="HiddenKjSyouhinCdMae" runat="server" value="" /><%--改良工事・商品コード変更前--%>
                                        <input type="hidden" id="HiddenTjSyouhinCdMae" runat="server" value="" /><%--追加工事・商品コード変更前--%>
                                        <input type="hidden" id="HiddenKjKairyouKoujiSyubetuMae" runat="server" value="" /><%--改良工事・改良種別変更前--%>
                                        <input type="hidden" id="HiddenTjKairyouKoujiSyubetuMae" runat="server" value="" /><%--追加工事・改良種別変更前--%>
                                        <input type="hidden" id="HiddenKjSeikyuusyoHakkouDateMsg1" runat="server" value="" /><%--改良工事・請求書発行日MSG確認1--%>
                                        <input type="hidden" id="HiddenKjSeikyuusyoHakkouDateMsg2" runat="server" value="" /><%--改良工事・請求書発行日MSG確認2--%>
                                        <input type="hidden" id="HiddenTjSeikyuusyoHakkouDateMsg1" runat="server" value="" /><%--追加工事・請求書発行日MSG確認1--%>
                                        <input type="hidden" id="HiddenTjSeikyuusyoHakkouDateMsg2" runat="server" value="" /><%--追加工事・請求書発行日MSG確認2--%>
                                        <input type="hidden" id="HiddenKjChk05" runat="server" value="" /><%--改良工事・Chk05--%>
                                        <input type="hidden" id="HiddenTjChk05" runat="server" value="" /><%--追加工事・Chk05--%>
                                        <input type="hidden" id="HiddenKjChk14" runat="server" value="" /><%--改良工事・Chk14--%>
                                        <input type="hidden" id="HiddenTjChk15" runat="server" value="" /><%--追加工事・Chk15--%>
                                        <input type="hidden" id="HiddenKjChk08" runat="server" value="" /><%--改良工事・Chk08--%>
                                        <input type="hidden" id="HiddenKjChk09" runat="server" value="" /><%--改良工事・Chk09--%>
                                        <input type="hidden" id="HiddenTjChk10" runat="server" value="" /><%--追加工事・Chk10--%>
                                        <input type="hidden" id="HiddenTjChk11" runat="server" value="" /><%--追加工事・Chk11--%>
                                        <table style="text-align: left; width: 100%;" id="TableKairyouKoujiInfo" class="innerTable"
                                            cellpadding="0" cellspacing="0">
                                            <tr class="firstRow">
                                                <!-- 左ヘッダ -->
                                                <td class="koumokuMei firstCol" colspan="1" rowspan="9" style="text-align: center;
                                                    font-size: 16px; width: 25px;">
                                                    改<br />
                                                    良<br />
                                                    工<br />
                                                    事
                                                </td>
                                                <!-- 1行目 -->
                                                <td class="koumokuMei" style="width: 100px">
                                                    工事仕様確認
                                                </td>
                                                <td id="TdKjKoujiSiyouKakunin" runat="server">
                                                    <asp:DropDownList ID="SelectKjKoujiSiyouKakunin" CssClass="" Style="display: inline;"
                                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectKjKoujiSiyouKakunin_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                                    </asp:DropDownList><span id="SpanKjKoujiSiyouKakunin" runat="server"></span>
                                                </td>
                                                <td class="koumokuMei">
                                                    確認日
                                                </td>
                                                <td colspan="8" id="TdKjKakuninDate" runat="server">
                                                    <asp:TextBox ID="TextKjKakuninDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 2行目 -->
                                            <tr>
                                                <td class="koumokuMei" style="width: 80px">
                                                    工事会社
                                                </td>
                                                <td colspan="5" style="width: 350px" id="TdKjKoujiKaisyaCd" runat="server">
                                                    <asp:TextBox ID="TextKjKoujiKaisyaCd" MaxLength="7" Style="width: 60px;" CssClass="codeNumber"
                                                        Text="" runat="server" />
                                                    <input type="button" id="ButtonKjKoujiKaisyaSearch" value="検索" class="gyoumuSearchBtn"
                                                        runat="server" onserverclick="ButtonKjKoujiKaisyaSearch_ServerClick" />
                                                    <input type="hidden" id="HiddenKoujiKaisyaSearchTypeKj" runat="server" value="" />
                                                    <asp:TextBox ID="TextKjKoujiKaisyaMei" Style="width: 200px;" CssClass="readOnlyStyle"
                                                        TabIndex="-1" ReadOnly="true" Text="" runat="server" />
                                                </td>
                                                <td class="koumokuMei" id="TdKjKoujiKaisyaSeikyuu" runat="server">
                                                    <asp:CheckBox ID="CheckBoxKjKoujiKaisyaSeikyuu" runat="server" AutoPostBack="True"
                                                        Text="工事会社請求" OnCheckedChanged="CheckBoxKjKoujiKaisyaSeikyuu_CheckedChanged" /><span
                                                            id="SpanKjKoujiKaisyaSeikyuu" runat="server" style="display: none">工事会社請求</span>
                                                    <input type="hidden" id="HiddenKjKojKaisyaCd" runat="server" value="" /><%--工事会社コード(改良工事)--%>
                                                </td>
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <!-- 3行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    改良工事種別
                                                </td>
                                                <td colspan="10">
                                                    <asp:DropDownList ID="SelectKjKairyouKoujiSyubetu" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="SelectKjKairyouKoujiSyubetu_SelectedIndexChanged">
                                                    </asp:DropDownList><span id="SpanKjKairyouKoujiSyubetu" runat="server"></span>
                                                </td>
                                            </tr>
                                            <!-- 4行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    完了予定日
                                                </td>
                                                <td colspan="10">
                                                    <asp:TextBox ID="TextKjKanryouYoteiDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 5行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    商品コード
                                                </td>
                                                <td colspan="8" id="TdKjSyouhinCd" runat="server">
                                                    <asp:DropDownList ID="SelectKjSyouhinCd" Style="width: 260px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="SelectKjSyouhinCd_SelectedIndexChanged">
                                                    </asp:DropDownList><span id="SpanKjSyouhinCd" runat="server"></span> &nbsp;&nbsp;&nbsp;
                                                    <span id="SpanKjUriageSyoriZumi" style="color: red; font-weight: bold;" runat="server">
                                                    </span>
                                                    <input type="hidden" id="HiddenKjUriageKeijyouDate" runat="server" />
                                                    <uc3:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLinkKai" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    請求
                                                </td>
                                                <td id="TdKjSeikyuuUmu" runat="server">
                                                    <asp:DropDownList ID="SelectKjSeikyuuUmu" CssClass="" Style="display: inline;" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="SelectKjSeikyuuUmu_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                                        <asp:ListItem Value="1">有</asp:ListItem>
                                                        <asp:ListItem Value="0">無</asp:ListItem>
                                                    </asp:DropDownList><span id="SpanKjSeikyuuUmu" runat="server"></span>
                                                </td>
                                            </tr>
                                            <!-- 6行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    売上金額
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    税抜金額
                                                </td>
                                                <td id="TdKjUriageKingaku" runat="server">
                                                    <asp:TextBox ID="TextKjUriageZeinukiKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                        Style="width: 60px" runat="server" OnTextChanged="TextKjUriageZeinukiKingaku_TextChanged" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    消費税
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjUriageSyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" runat="server" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    税込金額
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjUriageZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" runat="server" ReadOnly="true" TabIndex="-1" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    入金額<br />
                                                    (税込)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjNyuukingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    残額
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7" Text="0"
                                                        Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 7行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    仕入金額
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    税抜金額
                                                </td>
                                                <td id="TdKjSiireKingaku" runat="server">
                                                    <asp:TextBox ID="TextKjSiireZeinukiKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                        Style="width: 60px" runat="server" OnTextChanged="TextKjSiireZeinukiKingaku_TextChanged" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    消費税
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjSiireSyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" runat="server" TabIndex="-1" ReadOnly="true" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    税込金額
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TextKjSiireZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    請求先
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjSeikyuusaki" Style="width: 90px" CssClass="readOnlyStyle"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 8行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    工事日
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="TextKjKoujiDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    請求書<br />
                                                    発行日
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    売上<br />
                                                    年月日
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    発注書<br />
                                                    確定
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    発注書<br />
                                                    金額
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    発注書<br />
                                                    確認日
                                                </td>
                                            </tr>
                                            <!-- 9行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    完工速報着日
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="TextKjKankouSokuhouTyakuDate" CssClass="date" MaxLength="10" Text=""
                                                        runat="server" OnTextChanged="TextKjKankouSokuhouTyakuDate_TextChanged" />
                                                </td>
                                                <td id="TdKjSeikyuusyoHakkouDate" runat="server">
                                                    <asp:TextBox ID="TextKjSeikyuusyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                                        runat="server" OnTextChanged="TextKjSeikyuusyoHakkouDate_TextChanged" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjUriageNengappi" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjHattyuusyoKakutei" Style="width: 60px" CssClass="readOnlyStyle"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjHattyuusyoKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextKjHattyuusyoKakuninDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tableSpacer firstCol" style="border: 1px solid gray;" colspan="12">
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- [追加工事] -->
                                        <table style="text-align: left; width: 100%;" id="TableTuikaKairyouKoujiInfo" class="innerTable"
                                            cellpadding="0" cellspacing="0">
                                            <tr class="">
                                                <!-- 左ヘッダ -->
                                                <td class="koumokuMei firstCol" colspan="1" rowspan="8" style="text-align: center;
                                                    font-size: 16px; width: 25px;" id="TableTuikaKoujiInfo" runat="server">
                                                    追<br />
                                                    加<br />
                                                    工<br />
                                                    事
                                                </td>
                                                <!-- 1行目 -->
                                                <td style="width: 80px" class="koumokuMei">
                                                    工事会社
                                                </td>
                                                <td colspan="5" style="width: 350px" id="TdTjKoujiKaisyaCd" runat="server">
                                                    <asp:TextBox ID="TextTjKoujiKaisyaCd" MaxLength="7" Style="width: 60px;" CssClass="codeNumber"
                                                        Text="" runat="server" />
                                                    <input type="button" id="ButtonTjKoujiKaisyaSearch" value="検索" class="gyoumuSearchBtn"
                                                        runat="server" onserverclick="ButtonTjKoujiKaisyaSearch_ServerClick" /><input type="hidden"
                                                            id="HiddenKoujiKaisyaSearchTypeTj" runat="server" value="" />
                                                    <asp:TextBox ID="TextTjKoujiKaisyaMei" Style="width: 200px;" CssClass="readOnlyStyle"
                                                        TabIndex="-1" ReadOnly="true" Text="" runat="server" />
                                                </td>
                                                <td class="koumokuMei" id="TdTjKoujiKaisyaSeikyuu" runat="server">
                                                    <asp:CheckBox ID="CheckBoxTjKoujiKaisyaSeikyuu" runat="server" AutoPostBack="True"
                                                        Text="工事会社請求" OnCheckedChanged="CheckBoxTjKoujiKaisyaSeikyuu_CheckedChanged" /><span
                                                            id="SpanTjKoujiKaisyaSeikyuu" runat="server" style="display: none">工事会社請求</span>
                                                    <input type="hidden" id="HiddenTjKojKaisyaCd" runat="server" value="" /><%--工事会社コード(追加工事)--%>
                                                </td>
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <!-- 3行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    改良工事種別
                                                </td>
                                                <td colspan="10" id="TdTjKairyouKoujiSyubetu" runat="server">
                                                    <asp:DropDownList ID="SelectTjKairyouKoujiSyubetu" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="SelectTjKairyouKoujiSyubetu_SelectedIndexChanged">
                                                    </asp:DropDownList><span id="SpanTjKairyouKoujiSyubetu" runat="server"></span>
                                                </td>
                                            </tr>
                                            <!-- 4行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    完了予定日
                                                </td>
                                                <td colspan="10" id="TdTjKanryouYoteiDate" runat="server">
                                                    <asp:TextBox ID="TextTjKanryouYoteiDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 5行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    商品コード
                                                </td>
                                                <td colspan="8" id="TdTjSyouhinCd" runat="server">
                                                    <asp:DropDownList ID="SelectTjSyouhinCd" Style="width: 260px" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="SelectTjSyouhinCd_SelectedIndexChanged">
                                                    </asp:DropDownList><span id="SpanTjSyouhinCd" runat="server"></span>&nbsp;&nbsp;&nbsp;
                                                    <span id="SpanTjUriageSyorizumi" style="color: red; font-weight: bold;" runat="server">
                                                    </span>
                                                    <input type="hidden" id="HiddenTjUriageKeijyouDate" runat="server" />
                                                    <uc3:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLinkTui" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    請求
                                                </td>
                                                <td id="TdTjSeikyuuUmu" runat="server">
                                                    <asp:DropDownList ID="SelectTjSeikyuuUmu" CssClass="display: inline;" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="SelectTjSeikyuuUmu_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="無"></asp:ListItem>
                                                    </asp:DropDownList><span id="SpanTjSeikyuuUmu" runat="server"></span>
                                                </td>
                                            </tr>
                                            <!-- 6行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    売上金額
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    税抜金額
                                                </td>
                                                <td id="TdTjUriageKingaku" runat="server">
                                                    <asp:TextBox ID="TextTjUriageZeinukiKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                        Style="width: 60px" runat="server" OnTextChanged="TextTjUriageZeinukiKingaku_TextChanged" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    消費税
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjUriageSyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    税込金額
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjUriageZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    入金額<br />
                                                    (税込)
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjNyuuKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="0" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitleNyuukin">
                                                    残額
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7" Text="0"
                                                        Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 7行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    仕入金額
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    税抜金額
                                                </td>
                                                <td id="TdTjSiireKingaku" runat="server">
                                                    <asp:TextBox ID="TextTjSiireZeinukiKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                        Style="width: 60px" runat="server" OnTextChanged="TextTjSiireZeinukiKingaku_TextChanged" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    消費税
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjSiireSyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    税込金額
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TextTjSiireZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    請求先
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjSeikyuusaki" Style="width: 90px" CssClass="readOnlyStyle"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                            <!-- 8行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    工事日
                                                </td>
                                                <td colspan="5" id="TdTjKoujiDate" runat="server">
                                                    <asp:TextBox ID="TextTjKoujiDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    請求書<br />
                                                    発行日
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    売上<br />
                                                    年月日
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    発注書<br />
                                                    確定
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    発注書<br />
                                                    金額
                                                </td>
                                                <td class="shouhinTableTitle">
                                                    発注書<br />
                                                    確認日
                                                </td>
                                            </tr>
                                            <!-- 9行目 -->
                                            <tr>
                                                <td class="koumokuMei">
                                                    完工速報着日
                                                </td>
                                                <td colspan="5" id="TdTjKankouSokuhouTyakuDate" runat="server">
                                                    <asp:TextBox ID="TextTjKankouSokuhouTyakuDate" CssClass="date" MaxLength="10" Text=""
                                                        runat="server" OnTextChanged="TextTjKankouSokuhouTyakuDate_TextChanged" />
                                                </td>
                                                <td id="TdTjSeikyuusyoHakkouDate" runat="server">
                                                    <asp:TextBox ID="TextTjSeikyuusyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                                        runat="server" OnTextChanged="TextTjSeikyuusyoHakkouDate_TextChanged" />
                                                </td>
                                                <td id="TdTjUriageNengappi" runat="server">
                                                    <asp:TextBox ID="TextTjUriageNengappi" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjHattyuusyoKakutei" Style="width: 60px" CssClass="readOnlyStyle"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjHattyuusyoKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                        Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextTjHattyuusyoKakuninDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                        Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <div id="divKairyouKoujiHoukokusyo" runat="server">
                <asp:UpdatePanel ID="UpdatePanelKairyouKoujiHoukokusyoInfo" UpdateMode="conditional"
                    runat="server" RenderMode="inline" ChildrenAsTriggers="true">
                    <Triggers>
                        <%--業務共通タブ--%>
                        <asp:AsyncPostBackTrigger ControlID="ucGyoumuKyoutuu" />
                    </Triggers>
                    <ContentTemplate>
                        <input type="hidden" id="HiddenKhHassouDateMae" value="" runat="server" /><%--発送日前--%>
                        <input type="hidden" id="HiddenKhSaihakkouDateMae" value="" runat="server" /><%--再発行日前--%>
                        <!-- [改良工事報告書情報] -->
                        <table style="text-align: left; width: 100%;" id="TableKairyouKoujiHoukokusyoInfo"
                            class="mainTable" cellpadding="0" cellspacing="0">
                            <!-- ヘッダ部 -->
                            <thead>
                                <tr>
                                    <th class="tableTitle" style="" colspan="6">
                                        <a id="AncKoujiHoukokusyo" runat="server">
                                            改良工事報告書情報</a>
                                        <input type="hidden" id="HiddenKoujiHoukokusyoInfoStyle" runat="server" value="inline" />
                                    </th>
                                </tr>
                            </thead>
                            <!-- ボディ部 -->
                            <tbody id="TBodyKoujiHoukokusyoInfo" runat="server">
                                <!-- 1行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        受理
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="SelectKhJuri" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectKhJuri_SelectedIndexChanged">
                                        </asp:DropDownList><span id="SpanThjJuri" runat="server"></span>
                                    </td>
                                    <td class="koumokuMei">
                                        受理詳細
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextKhJuriSyousai" Style="width: 300px" CssClass="" Text="" runat="server"
                                            MaxLength="40" />
                                    </td>
                                </tr>
                                <!-- 4行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        受理日
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextKhJuriDate" CssClass="date" MaxLength="10" Text="" runat="server" />
                                    </td>
                                    <td class="koumokuMei">
                                        発送日
                                    </td>
                                    <td colspan="2" runat="server" id="TdKhHassouDate">
                                        <asp:TextBox ID="TextKhHassouDate" CssClass="date" MaxLength="10" Text="" runat="server"
                                            OnTextChanged="TextKhHassouDate_TextChanged" />
                                    </td>
                                </tr>
                                <!-- 5行目 -->
                                <tr>
                                    <td class="koumokuMei">
                                        再発行日
                                    </td>
                                    <td colspan="5" id="TdKhSaihakkouDate" runat="server">
                                        <asp:TextBox ID="TextKhSaihakkouDate" CssClass="date" MaxLength="10" Text="" runat="server"
                                            OnTextChanged="TextKhSaihakkouDate_TextChanged" />
                                        &nbsp;&nbsp;&nbsp; <span id="SpanKhUriageSyorizumi" style="color: red; font-weight: bold;"
                                            runat="server"></span>
                                        <input type="hidden" id="HiddenKhUriageKeijyouDate" runat="server" />
                                    </td>
                                </tr>
                                <!-- 6行目 -->
                                <tr>
                                    <td colspan="6" style="padding: 0px;">
                                        <asp:UpdatePanel ID="UpdatePanelSyouhinInfo" UpdateMode="conditional" runat="server"
                                            RenderMode="inline" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="SelectKhSeikyuuUmu" />
                                                <asp:AsyncPostBackTrigger ControlID="TextKhKoumutenSeikyuuKingaku" />
                                                <asp:AsyncPostBackTrigger ControlID="TextKhJituseikyuuKingaku" />
                                                <asp:AsyncPostBackTrigger ControlID="TextKhSaihakkouDate" />
                                                <asp:AsyncPostBackTrigger ControlID="TextKhHassouDate" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <!-- hidden項目-->
                                                <input type="hidden" id="HiddenKhJituseikyuu1Flg" value="" runat="server" /><%--実請求税抜金額1フラグ--%>
                                                <input type="hidden" id="HiddenKhZeiKbn" runat="server" /><%--改良工事報告書・税区分--%>
                                                <input type="hidden" id="HiddenKhZeiritu" value="0" runat="server" /><%--改良工事報告書・税率--%>
                                                <input type="hidden" id="HiddenKhSeikyuuUmuMae" value="" runat="server" /><%--請求有無前--%>
                                                <table style="text-align: left; width: 100%;" id="TableKhSyouhin" class="itemTable innerTable"
                                                    cellpadding="0" cellspacing="0">
                                                    <!-- 1行目 -->
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
                                                    <!-- 2行目 -->
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
                                                    <!-- 3行目 -->
                                                    <tr runat="server" id="TrKhSyouhin">
                                                        <td style="width: 200px" class="itemNm firstCol">
                                                            <asp:TextBox ID="TextKhSyouhinCd" CssClass="itemCd readOnlyStyle" Style="width: 70px;"
                                                                ReadOnly="true" Text="" TabIndex="-1" runat="server" />
                                                            <uc3:SeikyuuSiireLinkCtrl ID="ucSeikyuuSiireLink" runat="server" />
                                                            <br />
                                                            <span id="SpanKhSyouhinMei" class="itemNm" runat="server"></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKhKoumutenSeikyuuKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                                Style="width: 60px" runat="server" OnTextChanged="TextKhKoumutenSeikyuuKingaku_TextChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKhJituseikyuuKingaku" CssClass="kingaku" MaxLength="7" Text=""
                                                                Style="width: 60px" runat="server" OnTextChanged="TextKhJituseikyuuKingaku_TextChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKhSyouhizei" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                                Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                            <br />
                                                            <asp:TextBox ID="TextKhZeikomiKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                                Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                            <asp:HiddenField ID="HiddenKhSiireGaku" runat="server" />
                                                            <asp:HiddenField ID="HiddenKhSiireSyouhiZei" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKhSeikyuusyoHakkouDate" CssClass="date" MaxLength="10" Text=""
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKhUriageNengappi" CssClass="date readOnlyStyle" MaxLength="10"
                                                                Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="SelectKhSeikyuuUmu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectKhSeikyuuUmu_SelectedIndexChanged">
                                                                <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="有"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="無"></asp:ListItem>
                                                            </asp:DropDownList><span id="SpanKhSeikyuuUmu" runat="server"></span>
                                                            <br />
                                                            <asp:TextBox ID="TextKhSeikyuusaki" CssClass="readOnlyStyle" Style="width: 80px;"
                                                                Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKhHattyuusyoKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                                Text="" Style="width: 60px" ReadOnly="true" TabIndex="-1" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKhHattyuusyoKakutei" CssClass="readOnlyStyle" Style="width: 60px;"
                                                                Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextKhHattyuusyoKakuninDate" CssClass="date readOnlyStyle" MaxLength="10"
                                                                Text="" ReadOnly="true" TabIndex="-1" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10" style="padding: 0px;" class="firstCol">
                                                            <table id="Table6" class="innerTable" cellpadding="0" cellspacing="0">
                                                                <tr class="firstRow">
                                                                    <td style="width: 90px; text-align: left;" class="koumokuMei firstCol">
                                                                        再発行理由
                                                                    </td>
                                                                    <td id="TdKhSaihakkouRiyuu" runat="server" style="text-align: left;">
                                                                        <asp:TextBox ID="TextKhSaihakkouRiyuu" Style="width: 300px" CssClass="" Text="" runat="server"
                                                                            MaxLength="40" />
                                                                    </td>
                                                                    <td class="shouhinTableTitleNyuukin">
                                                                        入金額<br />
                                                                        (税込)
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextKhNyuuKingaku" CssClass="kingaku readOnlyStyle" MaxLength="7"
                                                                            Text="0" Style="width: 60px" runat="server" ReadOnly="true" TabIndex="-1" />
                                                                    </td>
                                                                    <td class="shouhinTableTitleNyuukin">
                                                                        残額
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextKhZangaku" CssClass="kingaku readOnlyStyle" MaxLength="7" Text="0"
                                                                            Style="width: 60px" runat="server" ReadOnly="true" TabIndex="-1" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <!-- 7行目 -->
                            </tbody>
                        </table>
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
                    <input type="button" id="ButtonTouroku2" value="登録 / 修正 実行" style="font-weight: bold;
                        font-size: 18px; width: 155px; color: black; height: 30px; background-color: fuchsia"
                        runat="server" />&nbsp;
                </th>
            </tr>
        </tbody>
    </table>
</asp:Content>
