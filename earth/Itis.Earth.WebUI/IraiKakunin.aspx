<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="IraiKakunin.aspx.vb" Inherits="Itis.Earth.WebUI.IraiKakunin" Title="EARTH 受注 【確認】" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<%@ Register Src="control/IraiCtrl1.ascx" TagName="IraiCtrl1" TagPrefix="uc1" %>
<%@ Register Src="control/IraiCtrl2.ascx" TagName="IraiCtrl2" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        //帳票表示用リダイレクト処理用変数
        var redirectUrl = null;
        
        //画面起動時にウィンドウサイズをディスプレイに合わせる
        try{
            window.moveTo(0, 0);
            window.resizeTo(window.screen.availWidth, window.screen.availHeight);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891){
                throw e;
            }
        }
        
        /*====================================
        *グローバル変数宣言（画面部品）
        ====================================*/
        //画面表示部品
        //商品コード
        var objSyouhinCd_1_1 = null;
        var objSyouhinCd_2_1 = null;
        var objSyouhinCd_2_2 = null;
        var objSyouhinCd_2_3 = null;
        var objSyouhinCd_2_4 = null;
        var objSyouhinCd_3_1 = null;
        var objSyouhinCd_3_2 = null;
        var objSyouhinCd_3_3 = null;
        var objSyouhinCd_3_4 = null;
        var objSyouhinCd_3_5 = null;
        var objSyouhinCd_3_6 = null;
        var objSyouhinCd_3_7 = null;
        var objSyouhinCd_3_8 = null;
        var objSyouhinCd_3_9 = null;
        
        //計上FLG
        var objUriKeijyouFlg_1_1 = null;
        var objUriKeijyouFlg_2_1 = null;
        var objUriKeijyouFlg_2_2 = null;
        var objUriKeijyouFlg_2_3 = null;
        var objUriKeijyouFlg_2_4 = null;
        var objUriKeijyouFlg_3_1 = null;
        var objUriKeijyouFlg_3_2 = null;
        var objUriKeijyouFlg_3_3 = null;
        var objUriKeijyouFlg_3_4 = null;
        var objUriKeijyouFlg_3_5 = null;
        var objUriKeijyouFlg_3_6 = null;
        var objUriKeijyouFlg_3_7 = null;
        var objUriKeijyouFlg_3_8 = null;
        var objUriKeijyouFlg_3_9 = null;
        
        //発注書金額
        var objHattyuuKingaku_1_1 = null;
        var objHattyuuKingaku_2_1 = null;
        var objHattyuuKingaku_2_2 = null;
        var objHattyuuKingaku_2_3 = null;
        var objHattyuuKingaku_2_4 = null;
        var objHattyuuKingaku_3_1 = null;
        var objHattyuuKingaku_3_2 = null;
        var objHattyuuKingaku_3_3 = null;
        var objHattyuuKingaku_3_4 = null;
        var objHattyuuKingaku_3_5 = null;
        var objHattyuuKingaku_3_6 = null;
        var objHattyuuKingaku_3_7 = null;
        var objHattyuuKingaku_3_8 = null;
        var objHattyuuKingaku_3_9 = null;
        
        //特別対応ツールチップDisplayコード
        var objDisplayCd_1_1 = null;
        var objDisplayCd_2_1 = null;
        var objDisplayCd_2_2 = null;
        var objDisplayCd_2_3 = null;
        var objDisplayCd_2_4 = null;
        var objDisplayCd_3_1 = null;
        var objDisplayCd_3_2 = null;
        var objDisplayCd_3_3 = null;
        var objDisplayCd_3_4 = null;
        var objDisplayCd_3_5 = null;
        var objDisplayCd_3_6 = null;
        var objDisplayCd_3_7 = null;
        var objDisplayCd_3_8 = null;
        var objDisplayCd_3_9 = null;
        
        //登録ボタン系 活性・非活性制御
        var blnShinkiHikitugiEnbl = null;   //新規引継ぎ(画面上部)
        var blnShinkiRenzokuEnbl = null;    //新規(画面上部)
        var blnShinkiHikitugiEnbl2 = null;  //新規引継ぎ(画面下部)
        var blnShinkiRenzokuEnbz2 = null;   //新規(画面下部)

        //onload後処理
        function funcAfterOnload(){
            //連棟処理
            var callRentouNextFlg = objEBI("<%=HiddenCallRentouNextFlg.clientID %>").value;
            //連棟処理フラグがtureの場合、引き続き実行処理を行う
            if(callRentouNextFlg){
                objEBI("<%=actBtnId.value %>").click();
            }
            //帳票表示用リダイレクト処理
            if(redirectUrl != null && redirectUrl != ""){
                var tmpUrl = redirectUrl;
                redirectUrl = null;
                window.location.replace(tmpUrl);
                //リダイレクトしているため、これ以降のスクリプト処理は無効
            }
        }
        
        //各種実行ボタン押下時の処理
        function actClickButton(obj){
            var objCallRentouNextFlg = objEBI("<%=HiddenCallRentouNextFlg.clientID %>");
            var objHiddenActButtonId = objEBI("<%=actBtnId.clientID %>");
            var syorikensuuButton = objEBI("<%=ButtonDisplaySyoriKensuu.clientID %>");
            var objAccRentouBukkenSuu = objEBI("<%= IraiCtrl1_1.AccRentouBukkenSuu.clientID %>");
            var objAccSyorikenSuu = objEBI("<%= IraiCtrl1_1.AccSyoriKensuu.clientID %>");
            
            if(objCallRentouNextFlg.value){
                if(objAccRentouBukkenSuu.value > 1){
                    syorikensuuButton.value = "連棟物件登録処理中....  [ " + objAccSyorikenSuu.value + " / " + objAccRentouBukkenSuu.value + " ] 件 完了";
                }
                objHiddenActButtonId.value=obj.id;
                setWindowOverlay(obj,syorikensuuButton);
                objCallRentouNextFlg.value = "";
            }else if(confirm("<%=Messages.MSG017C %>")){
                objHiddenActButtonId.value=obj.id;
                setWindowOverlay(obj);
            }else{
                objHiddenActButtonId.value='';
                return false;
            }
        }

        //新規引き継ぎ＆新規ボタン用 連棟物件数指定プロンプト表示
        function actSinki(obj,type){
            //連棟物件数のチェック
            var strRentouSuu = "";//連棟物件数
            var strMsg = "<%= Messages.MSG062E %>";
            strMsg = strMsg.replace('@PARAM1','連棟物件数');
            strRentouSuu = prompt("\r\n" + strMsg,"");
            if(strRentouSuu == null){ //キャンセルボタン押下時
                return false;
            }
            if(strRentouSuu == "" || strRentouSuu == undefined){
                strMsg = "<%= Messages.MSG013E %>";
                strMsg = strMsg.replace('@PARAM1','連棟物件数');
                alert(strMsg);
                return false;
            }
            strRentouSuu = strRentouSuu.z2hDigit();
            if(strRentouSuu <= 0 || strRentouSuu > 999){
                strMsg = "<%= Messages.MSG111E %>";
                strMsg = strMsg.replace('@PARAM1','連棟物件数');
                strMsg = strMsg.replace('@PARAM2','1');
                strMsg = strMsg.replace('@PARAM3','999');
                alert(strMsg);
                return false;
            }
            if(strRentouSuu >= 21){
                strMsg = "<%= Messages.MSG112C %>";
                strMsg = strMsg.replace('@PARAM1',strRentouSuu + '棟');
                if(confirm(strMsg) == false){
                    return false;
                }
            }

            //連棟物件数をhiddenにセット
            objEBI("<%=IraiCtrl1_1.AccRentouBukkenSuu.clientID %>").value = strRentouSuu;
            actClickButton(obj);

        }
        
        //共通情報コピーボタン押下時処理
        function exeBtnCopy(obj){
            var objSelectCopyKbn = objEBI("<%=SelectKubunCopy.clientID %>");
            var objAccNayoseCd = objEBI("<%= IraiCtrl1_1.AccNayoseCd.clientID %>");
            var objAccKbn = objEBI("<%= IraiCtrl1_1.AccCbokubun.clientID %>");
            var objAccBangou = objEBI("<%= IraiCtrl1_1.AccHoshouno.clientID %>");
            var strBukkenNayoseCd = "";
            
            var strDispMsg1 = "<%= Messages.MSG168C %>";
            var strDispMsg2 = "<%= Messages.MSG175C %>";
            
            //区分
            if(objSelectCopyKbn.value == ""){
                alert("<%= Messages.MSG004E %>");
                return false;
            }
            
            //物件名寄コード=入力の場合
            if(objAccNayoseCd.value != ""){
                strBukkenNayoseCd = objAccNayoseCd.value;
                
                //MSG追加
                strDispMsg2 = strDispMsg2.replace('@PARAM1','物件名寄コード');
                strDispMsg2 = strDispMsg2.replace('@PARAM2',strBukkenNayoseCd);
                strDispMsg1 = strDispMsg1 + strDispMsg2;
            }
                        
            //確認MSG
            if(confirm(strDispMsg1)){
                actSinki(obj);
            }else{
                return false;
            }
            return true;
        }

        /*********************************************
        * 画面遷移用のパラメータを作成
        *********************************************/        
        function createPrm(type){
            //パラメータ
            var strSendVal_Prm = "";

            //画面表示部品セット
            setGlobalObj(type);

            //1：商品123情報
            if(type == 1){
                           
                //商品1(商品コード)
                strSendVal_Prm = setPrmObj(objSyouhinCd_1_1, strSendVal_Prm, type);
                strSendVal_Prm += sepStr;
                //商品2(商品コード)
                strSendVal_Prm = setPrmObj(objSyouhinCd_2_1, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_2_2, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_2_3, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_2_4, strSendVal_Prm, type);
                strSendVal_Prm += sepStr;
                //商品3(商品コード)
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_1, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_2, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_3, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_4, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_5, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_6, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_7, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_8, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objSyouhinCd_3_9, strSendVal_Prm, type);
                
            //2：計上FLG 
            }else if(type == 2){
                                            
                //商品1(計上FLG)
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_1_1, strSendVal_Prm, type);
                strSendVal_Prm += sepStr;
                //商品2(計上FLG)
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_2_1, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_2_2, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_2_3, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_2_4, strSendVal_Prm, type);
                strSendVal_Prm += sepStr;
                //商品3(計上FLG)
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_1, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_2, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_3, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_4, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_5, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_6, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_7, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_8, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objUriKeijyouFlg_3_9, strSendVal_Prm, type);
            
            //3：発注書金額     
            }else if(type == 3){
                                            
                //商品1(発注書金額)
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_1_1, strSendVal_Prm, type);
                strSendVal_Prm += sepStr;
                //商品2(発注書金額)
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_2_1, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_2_2, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_2_3, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_2_4, strSendVal_Prm, type);
                strSendVal_Prm += sepStr;
                //商品3(発注書金額)
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_1, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_2, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_3, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_4, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_5, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_6, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_7, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_8, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objHattyuuKingaku_3_9, strSendVal_Prm, type);
                
            //4：特別対応ツールチップ(Displayコード) 
            }else if(type == 4){
                                            
                //商品1(Displayコード)
                strSendVal_Prm = setPrmObj(objDisplayCd_1_1, strSendVal_Prm, type);
                strSendVal_Prm += sepStr;
                //商品2(Displayコード)
                strSendVal_Prm = setPrmObj(objDisplayCd_2_1, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_2_2, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_2_3, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_2_4, strSendVal_Prm, type);
                strSendVal_Prm += sepStr;
                //商品3(Displayコード)
                strSendVal_Prm = setPrmObj(objDisplayCd_3_1, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_3_2, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_3_3, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_3_4, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_3_5, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_3_6, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_3_7, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_3_8, strSendVal_Prm, type);
                strSendVal_Prm = setPrmObj(objDisplayCd_3_9, strSendVal_Prm, type);        
            }            
             
            //末尾のセパレータを除去
            strSendVal_Prm = RemoveSepStr(strSendVal_Prm); 
            strSendVal_Prm = RemoveSepStr(strSendVal_Prm); 
           
            return strSendVal_Prm;
        }
 
        /*********************************************
        * 画面表示部品オブジェクトをグローバル変数化
        *********************************************/
        function setGlobalObj(type) {
            //画面表示部品
            if(type == 1){            
                //商品コード        
                objSyouhinCd_1_1 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_1_1.clientID %>");
                objSyouhinCd_2_1 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_2_1.clientID %>");
                objSyouhinCd_2_2 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_2_2.clientID %>");
                objSyouhinCd_2_3 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_2_3.clientID %>");
                objSyouhinCd_2_4 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_2_4.clientID %>");
                objSyouhinCd_3_1 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_1.clientID %>");
                objSyouhinCd_3_2 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_2.clientID %>");
                objSyouhinCd_3_3 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_3.clientID %>");
                objSyouhinCd_3_4 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_4.clientID %>");
                objSyouhinCd_3_5 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_5.clientID %>");
                objSyouhinCd_3_6 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_6.clientID %>");
                objSyouhinCd_3_7 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_7.clientID %>");
                objSyouhinCd_3_8 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_8.clientID %>");
                objSyouhinCd_3_9 = objEBI("<%= IraiCtrl2_1.AccTxtSyouhinCd_3_9.clientID %>");            
            
            }else if(type == 2){
                //計上FLG
                objUriKeijyouFlg_1_1 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_1_1.clientID %>");
                objUriKeijyouFlg_2_1 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_2_1.clientID %>");
                objUriKeijyouFlg_2_2 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_2_2.clientID %>");
                objUriKeijyouFlg_2_3 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_2_3.clientID %>");
                objUriKeijyouFlg_2_4 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_2_4.clientID %>");
                objUriKeijyouFlg_3_1 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_1.clientID %>");
                objUriKeijyouFlg_3_2 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_2.clientID %>");
                objUriKeijyouFlg_3_3 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_3.clientID %>");
                objUriKeijyouFlg_3_4 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_4.clientID %>");
                objUriKeijyouFlg_3_5 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_5.clientID %>");
                objUriKeijyouFlg_3_6 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_6.clientID %>");
                objUriKeijyouFlg_3_7 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_7.clientID %>");
                objUriKeijyouFlg_3_8 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_8.clientID %>");
                objUriKeijyouFlg_3_9 = objEBI("<%= IraiCtrl2_1.AccTxtUriKeijyouFlg_3_9.clientID %>");                     
            
            }else if(type == 3){
                //発注書金額
                objHattyuuKingaku_1_1 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_1_1.clientID %>");
                objHattyuuKingaku_2_1 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_2_1.clientID %>");
                objHattyuuKingaku_2_2 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_2_2.clientID %>");
                objHattyuuKingaku_2_3 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_2_3.clientID %>");
                objHattyuuKingaku_2_4 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_2_4.clientID %>");
                objHattyuuKingaku_3_1 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_1.clientID %>");
                objHattyuuKingaku_3_2 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_2.clientID %>");
                objHattyuuKingaku_3_3 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_3.clientID %>");
                objHattyuuKingaku_3_4 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_4.clientID %>");
                objHattyuuKingaku_3_5 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_5.clientID %>");
                objHattyuuKingaku_3_6 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_6.clientID %>");
                objHattyuuKingaku_3_7 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_7.clientID %>");
                objHattyuuKingaku_3_8 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_8.clientID %>");
                objHattyuuKingaku_3_9 = objEBI("<%= IraiCtrl2_1.AccTxtHattyuuKingaku_3_9.clientID %>");                     
            
            }else if(type == 4){
                //特別対応ツールチップ(Displayコード) 
                objDisplayCd_1_1 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_1_1.AccDisplayCd.clientID %>");
                objDisplayCd_2_1 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_2_1.AccDisplayCd.clientID %>");
                objDisplayCd_2_2 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_2_2.AccDisplayCd.clientID %>");
                objDisplayCd_2_3 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_2_3.AccDisplayCd.clientID %>");
                objDisplayCd_2_4 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_2_4.AccDisplayCd.clientID %>");
                objDisplayCd_3_1 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_1.AccDisplayCd.clientID %>");
                objDisplayCd_3_2 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_2.AccDisplayCd.clientID %>");
                objDisplayCd_3_3 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_3.AccDisplayCd.clientID %>");
                objDisplayCd_3_4 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_4.AccDisplayCd.clientID %>");
                objDisplayCd_3_5 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_5.AccDisplayCd.clientID %>");
                objDisplayCd_3_6 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_6.AccDisplayCd.clientID %>");
                objDisplayCd_3_7 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_7.AccDisplayCd.clientID %>");
                objDisplayCd_3_8 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_8.AccDisplayCd.clientID %>");
                objDisplayCd_3_9 = objEBI("<%= IraiCtrl2_1.AccTokubetuTaiouToolTip_3_9.AccDisplayCd.clientID %>");                          
            }
        }
        
        /*********************************************
        * パラメータをセット
        *********************************************/               
        function setPrmObj(obj, strPrm, type){
            //空白の時
            if(obj.value == ""){
                if(type == "3"){
                    strPrm += "0" + sepStr + sepStr;
                }else{          
                    strPrm += "<%=EarthConst.BRANK_STRING %>" + sepStr + sepStr;
                }
                    
            }else{
                strPrm += obj.value + sepStr + sepStr;
            }
            
            return strPrm
        }
        
       /*********************************************
        * 特別対応価格反映処理呼び出し
        *********************************************/
        var flgAjaxRunning = false;
        var tmpAjVal = null;
        function callTokutaiKkk(objThis){
            if(flgAjaxRunning){
                //Ajax処理中は、待つ
                if(tmpAjVal==null)tmpAjVal = objThis.value;
                setTimeout(function(){callTokutaiKkk(objThis)},100);
            }else{
                if(tmpAjVal!=null)objEBI(objThis.id).value = tmpAjVal;
                //ボタン押下
                tmpAjValS1 = null;
                objEBI("<%= ButtonHiddenSyouhinReload.clientID %>").click();
            }
        }
        
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
        function InitializeRequestHandler(sender, args){
            var mng = Sys.WebForms.PageRequestManager.getInstance();
            if (mng.get_isInAsyncPostBack()){
                //非同期ポストバックが既に実行中の場合、新たにリクエストされたポストバック処理をキャンセルする。
                if(args.get_postBackElement().id != "<%=ButtonHiddenSyouhinReloadPre.ClientID %>"){
                    //商品１設定処理以外
                    args.set_cancel(true);
                    return false;
                }
            }
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=1;
            flgAjaxRunning = true;
            var arrCtrl = new Array();
            arrCtrl[0] = objEBI("<%= btn_shinkiTouroku.clientID %>");
            arrCtrl[1] = objEBI("<%= btn_shinkiHikitugi.clientID %>");
            arrCtrl[2] = objEBI("<%= btn_shinkiRenzoku.clientID %>");
            arrCtrl[3] = objEBI("<%= btn_sakujo.clientID %>");
            arrCtrl[4] = objEBI("<%= btn_pdf.clientID %>");
            arrCtrl[5] = objEBI("<%= ButtonKyoutuuInfoCopy.clientID %>");
            arrCtrl[6] = objEBI("<%= IraiCtrl1_1.AccBtn_irai1.clientID %>");
            arrCtrl[7] = objEBI("<%= IraiCtrl2_1.AccBtn_irai2.clientID %>");
            arrCtrl[8] = objEBI("<%= btn_shinkiTouroku2.clientID %>");
            arrCtrl[9] = objEBI("<%= btn_shinkiHikitugi2.clientID %>");
            arrCtrl[10] = objEBI("<%= btn_shinkiRenzoku2.clientID %>");
            arrCtrl[11] = objEBI("<%= btn_pdf2.clientID %>");
            arrCtrl[12] = objEBI("<%= btn_tyousaMitsumorisyoSakusei.clientID %>");
            
            //活性化状態を退避
            if(objEBI("<%= btn_shinkiHikitugi.clientID %>") != null){
                blnShinkiHikitugiEnbl = objEBI("<%= btn_shinkiHikitugi.clientID %>").disabled;
            }
            if(objEBI("<%= btn_shinkiRenzoku.clientID %>") != null){
                blnShinkiRenzokuEnbl = objEBI("<%= btn_shinkiRenzoku.clientID %>").disabled;
            }
            if(objEBI("<%= btn_shinkiHikitugi2.clientID %>") != null){
                blnShinkiHikitugiEnbl2 = objEBI("<%= btn_shinkiHikitugi2.clientID %>").disabled;
            }
            if(objEBI("<%= btn_shinkiRenzoku2.clientID %>") != null){
                blnShinkiRenzokuEnbl2 = objEBI("<%= btn_shinkiRenzoku2.clientID %>").disabled;
            }
                        
            for(ci=0;ci<arrCtrl.length;ci++){
                if(arrCtrl[ci]!=null)arrCtrl[ci].disabled = true;
            }
            
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args){
            var arrCtrl = new Array();
            arrCtrl[0] = objEBI("<%= btn_shinkiTouroku.clientID %>");
            arrCtrl[1] = objEBI("<%= btn_shinkiHikitugi.clientID %>");
            arrCtrl[2] = objEBI("<%= btn_shinkiRenzoku.clientID %>");
            arrCtrl[3] = objEBI("<%= btn_sakujo.clientID %>");
            arrCtrl[4] = objEBI("<%= btn_pdf.clientID %>");
            arrCtrl[5] = objEBI("<%= ButtonKyoutuuInfoCopy.clientID %>");
            arrCtrl[6] = objEBI("<%= IraiCtrl1_1.AccBtn_irai1.clientID %>");
            arrCtrl[7] = objEBI("<%= IraiCtrl2_1.AccBtn_irai2.clientID %>");
            arrCtrl[8] = objEBI("<%= btn_shinkiTouroku2.clientID %>");
            arrCtrl[9] = objEBI("<%= btn_shinkiHikitugi2.clientID %>");
            arrCtrl[10] = objEBI("<%= btn_shinkiRenzoku2.clientID %>");
            arrCtrl[11] = objEBI("<%= btn_pdf2.clientID %>");
            arrCtrl[12] = objEBI("<%= btn_tyousaMitsumorisyoSakusei.clientID %>");
            
            for(ci=0;ci<arrCtrl.length;ci++){
                if(arrCtrl[ci]!=null)arrCtrl[ci].disabled = false
            }
            
            //活性化状態を復元
            if(objEBI("<%= btn_shinkiHikitugi.clientID %>") != null){
                objEBI("<%= btn_shinkiHikitugi.clientID %>").disabled = blnShinkiHikitugiEnbl;
            }
            if(objEBI("<%= btn_shinkiRenzoku.clientID %>") != null){
                objEBI("<%= btn_shinkiRenzoku.clientID %>").disabled = blnShinkiRenzokuEnbl;
            }
            if(objEBI("<%= btn_shinkiHikitugi2.clientID %>") != null){
                objEBI("<%= btn_shinkiHikitugi2.clientID %>").disabled = blnShinkiHikitugiEnbl2;
            }
            if(objEBI("<%= btn_shinkiRenzoku2.clientID %>") != null){
                objEBI("<%= btn_shinkiRenzoku2.clientID %>").disabled = blnShinkiRenzokuEnbl2;
            }
            
            objEBI("<%=HiddenAjaxFlg.ClientID %>").value=0;
            flgAjaxRunning = false;
        }
        
    </script>

    <input type="hidden" id="actBtnId" runat="server" />
    <input type="hidden" id="st" runat="server" />
    <input type="hidden" id="tourokuKanryouFlg" runat="server" />
    <input type="hidden" id="callModalFlg" runat="server" />
    <input type="hidden" id="HiddenCallRentouNextFlg" runat="server" />
    <asp:UpdatePanel ID="updPanelReloadBtn" UpdateMode="Conditional" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <input type="button" id="ButtonHiddenSyouhinReload" runat="server" style="display: none"
                value="再描画(非表示)" />
                            <input type="button" id="ButtonHiddenSyouhinReloadPre" runat="server" style="display: none"
                value="再描画前処理(非表示)" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <input type="button" id="ButtonDisplaySyoriKensuu" class="SyoriKensuuMessageButton"
        runat="server" tabindex="-1" onfocus="window.focus();" style="display: none;"
        value="処理中・・・" />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: left; width: 110px;" rowspan="2">
                    受注 【確認】
                </th>
                <th style="text-align: left;">
                    <input id="btn_shinkiTouroku" value="登録 / 修正実行" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 130px; color: black; height: 30px; background-color: fuchsia" />
                    <input id="btn_shinkiHikitugi" value="新規(引継)" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 90px; color: black; height: 30px; background-color: fuchsia" />
                    <input id="btn_shinkiRenzoku" value="新規" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 50px; color: black; height: 30px; background-color: fuchsia" />
                    <input id="btn_sakujo" value="削除" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 60px; color: black; height: 30px; background-color: mistyrose;
                        display: none;" />
                    <input id="btn_pdf" value="登録＆調査予定連絡書表示" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 200px; color: black; height: 30px; background-color: #ffff69" />
                    <asp:DropDownList ID="SelectKubunCopy" runat="server" Style="width: 40px">
                    </asp:DropDownList><asp:DropDownList ID="SelectKubunDummy" runat="server" Style="display: none">
                    </asp:DropDownList><input id="ButtonKyoutuuInfoCopy" value="名寄物件作成" type="button"
                        runat="server" style="font-weight: bold; font-size: 15px; width: 120px; color: black;
                        height: 30px; background-color: #c0ffc0" onclick="exeBtnCopy(this);" />&nbsp;
                </th>
                <th style="text-align: left;">
                    <input type="button" id="ButtonTokubetuTaiou" runat="server" value="特別対応" />
                </th>
                <th style="text-align: right; font-size: 11px;" rowspan="2">
                    最終更新者：<input id="saishuuKousinSha" class="readOnlyStyle" style="width: 120px" type="text"
                        readonly="readOnly" runat="server" /><br />
                    最終更新日時：<input id="saishuuKousinDate" class="readOnlyStyle" style="width: 100px" type="text"
                        readonly="readOnly" runat="server" />
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
    <input type="hidden" id="gamenId" value="shinki" runat="server" />
    <input type="hidden" id="HiddenAjaxFlg" runat="server" />
    <uc1:IraiCtrl1 ID="IraiCtrl1_1" runat="server" />
    <br />
    <uc2:IraiCtrl2 ID="IraiCtrl2_1" runat="server" />
    <br />
    <br />
    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
        class="titleTable">
        <tbody>
            <tr>
                <th style="text-align: center;">
                    <input id="btn_shinkiTouroku2" value="登録 / 修正 実行" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 130px; color: black; height: 30px; background-color: fuchsia" />&nbsp;
                    <input id="btn_shinkiHikitugi2" value="新規(引継)" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 90px; color: black; height: 30px; background-color: fuchsia" />&nbsp;
                    <input id="btn_shinkiRenzoku2" value="新規" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 50px; color: black; height: 30px; background-color: fuchsia" />&nbsp;
                    <input id="btn_sakujo2" value="削除" type="button" runat="server" style="font-weight: bold;
                        font-size: 18px; width: 60px; color: black; height: 30px; background-color: mistyrose;
                        display: none;" />&nbsp;
                    <input id="btn_pdf2" value="登録＆調査予定連絡書表示" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 200px; color: black; height: 30px; background-color: #ffff69" />&nbsp;
                    <input id="btn_tyousaMitsumorisyoSakusei" value="登録＆調査見積書作成" type="button" runat="server" style="font-weight: bold;
                        font-size: 15px; width: 200px; color: black; height: 30px; background-color: #ff8c00" />
                </th>
            </tr>
        </tbody>
    </table>
</asp:Content>
