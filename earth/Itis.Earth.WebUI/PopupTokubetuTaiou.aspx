<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupTokubetuTaiou.aspx.vb"
    Inherits="Itis.Earth.WebUI.PopupTokubetuTaiou" Title="EARTH 特別対応" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 特別対応</title>
</head>

<script type="text/javascript" src="js/jhsearth.js"> 
</script>

<script type="text/javascript">
        history.forward();
        
        _d = document;
        
        //ウィンドウサイズ変更
        try{
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(980,768);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        } 
                
        function toMainWin(){
            window.returnValue = objEBI("<%= HiddenPressMasterFlg.clientID %>").value;
            window.close();
        } 

        /****************************************
         * onload時の追加処理
         ****************************************/
        function funcAfterOnload(){           
            // 特別対応テーブル 各種レイアウト設定
            settingTable();
        }           

        /****************************************
         * 特別対応テーブル 各種レイアウト設定
         ****************************************/        
        function settingTable(){
            var syouhinData = objEBI("tblMeisai");
            setTableStripes(syouhinData,1);
        }
        
        /****************************************
         * ボタン押下時のフラグ設定
         ****************************************/        
        function setAction(prmFlg){
            //マスタ再取得ボタン押下時は、テーブル作成処理は行わない
            objEBI("<%= HiddenGetMasterFlg.clientID %>").value = prmFlg;
        }

        /****************************************
         * 登録ボタン押下時のチェック処理 
         ****************************************/        
        function CheckTouroku(){
            var objHdnTTaiouVal = objEBI("<%= HiddenOpenValues.clientID %>");       
            var tableMeisai = objEBI("<%=tblMeisai.clientID %>");
            var arrSakiTr = tableMeisai.rows;
            var objTd = null;
            var arrInput = null;
            var objTmpId = "";
            var objVal = "";
            var objValCd = 0;  
            
            var intTotal = 0;
            var strValNam = "";            //特別対応名称を区切文字で格納
            var strValNamKeijyouZumi = ""; //特別対応名称を区切文字で格納(計上済チェック用)
            
            var strCrlf = "\n";
            var strValCd = "";             //特別対応コードを区切文字で格納
            var strValCdKeijyouZumi = "";  //特別対応コードを区切文字で格納(計上済チェック用)
            
            var strMsg = "";               
                                 
            for ( var tri = 0; tri < arrSakiTr.length; tri++) {
                objTd = arrSakiTr[tri].cells[0];
                arrInput = getChildArr(objTd,"INPUT");                
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox"){
                        if(arrInput[ar].checked){                    
                            objTmpId = arrInput[4].id; //特別対応コード
                            objValCd = objEBI(objTmpId);                            
                            strValCd = strValCd + objValCd.value + sepStr;                           
                            
                            //特別対応コードが0以上9以下の特別名称を取得
                            if(objValCd.value >= 0 && objValCd.value <= 9){                                                              
                                intTotal += 1;
                                                                                                
                                objTmpId = arrInput[1].id; //特別対応名称
                                objVal = objEBI(objTmpId);                                                                                     
                                strValNam += objVal.value + sepStr;                                
                            }
                         }else{    
                             objTmpId = arrInput[11].id; //初期読込時のチェック状態
                             objValCd = objEBI(objTmpId);                            

                             //チェックがはずされた場合のみ
                             if(objValCd.value == "True"){
                                objTmpId = arrInput[6].id; //売上計上FLG
                                objValCd = objEBI(objTmpId);
                                                            
                                //売上計上FLGが1の場合
                                if(objValCd.value == "1"){
                                    objTmpId = arrInput[7].id; //価格処理FLG
                                    objValCd = objEBI(objTmpId);
                                    
                                    //価格処理FLGが1以外の場合、特別名称を取得
                                    if(objValCd.value != "1"){                                  
                                        objTmpId = arrInput[1].id; //特別対応名称(計上済チェック用)
                                        objVal = objEBI(objTmpId);
                                        strValNamKeijyouZumi += objVal.value + sepStr;  
                                
                                        objTmpId = arrInput[7].id; //価格処理FLG(計上済チェック用)
                                        strValCdKeijyouZumi  += objTmpId + sepStr;
                                    }
                                }
                             }
                          }
                     }                                                 
                 }
            }                
            strValNamKeijyouZumi = RemoveSepStr(strValNamKeijyouZumi);
            strValCdKeijyouZumi = RemoveSepStr(strValCdKeijyouZumi);
                       
            //変更チェック(特別対応コードで比較)
            if(objHdnTTaiouVal.value != strValCd){
                alert('<%= Messages.MSG193E %>' + '<%= Messages.MSG194E %>');
                
                //価格処理対象チェック(特別対応名を表示)
                if(strValNamKeijyouZumi != ""){
                    if(checkKeijyouZumi(strValCdKeijyouZumi, strValNamKeijyouZumi) == false){
                        return false;
                    }
                }
            }
              
            //個数チェック(特別対応名を表示)
            if(intTotal >= 2){
                strMsg = "<%= Messages.MSG192E %>";
                strValNam = strValNam.replace(/\$\$\$/g ,strCrlf);                    
                if(strValNam != ''){
                    strMsg += strValNam + strCrlf;               
                }
                alert(strMsg);                   
                return false;
            }                
            return true;
       }
       
        /****************************************
         * マスタ再取得ボタン押下時のチェック処理 
         ****************************************/        
        function CheckGetMaster(){
            var strAriVal = "<%=EarthConst.ARI_VAL %>"
            var strNasiVal = "<%=EarthConst.NASI_VAL %>"
            var tableMeisai = objEBI("<%=tblMeisai.clientID %>");
            var arrSakiTr = tableMeisai.rows;
            var objTd = null;
            var arrInput = null;
            var objTmpId = "";
            var objValCd = 0;
            var strCheckJyky = "";
            
            var strValNamKeijyouZumi = ""; //特別対応名称を区切文字で格納(計上済チェック用)
            var strValCdKeijyouZumi = "";  //特別対応コードを区切文字で格納(計上済チェック用)

            for ( var tri = 0; tri < arrSakiTr.length; tri++) {
                objTd = arrSakiTr[tri].cells[0];
                arrInput = getChildArr(objTd,"INPUT");
                for ( var ar = 0; ar < arrInput.length; ar++) {
                    if(arrInput[ar].type == "checkbox"){            
                        objTmpId = arrInput[6].id;          //売上計上FLG
                        objValCd = objEBI(objTmpId);
                                                                           
                        //売上計上FLG=1の場合
                        if(objValCd.value == strAriVal){
                            if(arrInput[ar].checked){       //画面のチェック状態
                                strCheckJyky = strAriVal
                            }else{
                                strCheckJyky = strNasiVal
                            }          
                            objTmpId = arrInput[5].id;      //マスタのチェック状態                
                            objValCd = objEBI(objTmpId);    
  
                            //マスタ再取得でチェックが外される場合
                            if(strCheckJyky == strAriVal && objValCd.value == strNasiVal){
                                objTmpId = arrInput[1].id;  //特別対応名称              
                                objValCd = objEBI(objTmpId);    
                                strValNamKeijyouZumi += objValCd.value + sepStr;

                                objTmpId = arrInput[7].id; //価格処理FLG(計上済チェック用)
                                strValCdKeijyouZumi  += objTmpId + sepStr;
                            }
                        }
                    }
                }
            }
            
            //計上済みの項目のチェックが外される場合
            if(strValNamKeijyouZumi != ""){
                if(checkKeijyouZumi(strValCdKeijyouZumi, strValNamKeijyouZumi) == false){
                    return false;
                }
            }
            return true;
        }
        
        /****************************************
         * 計上済の項目のチェック解除可否を確認する処理
         ****************************************/
        function checkKeijyouZumi(strClientID, strNam){
            var strCrlf = "\n";
            var strMsg = "";
            var strTmpNam = "";
     
            strMsg = "<%= Messages.MSG199C %>";
            strTmpNam = strNam.replace(/\$\$\$/g ,strCrlf);
            strMsg += strTmpNam
                    
            //OK押下の場合、価格処理フラグを設定する
            if(confirm(strMsg)){
                setKkkSyoriFlg(strClientID);
                return true;
            }else{
                return false;
            }      
        }
           
        /****************************************
         * 価格処理フラグ設定処理
         * (第一引数：ClientIDのセパレータ含む)
         ****************************************/        
        function setKkkSyoriFlg(strClientID){           
            var objTmpId = null;
            var objTmpSyoriFlg = null;
                        
            if(strClientID == ""){return false;}
            
            var arrSyoriFlg = strClientID.split(sepStr);
            
            for ( var tri = 0; tri < arrSyoriFlg.length; tri++) {
                if(arrSyoriFlg[tri] == "") continue;
                objTmpId = arrSyoriFlg[tri];
                objTmpSyoriFlg = objEBI(objTmpId);
                if(objTmpSyoriFlg == null) continue;
                objTmpSyoriFlg.value = 1;
            }
        }

        /****************************************
         * KEY情報の末尾のセパレータ文字列を除去する処理
         ****************************************/        
        function RemoveSepStr(strTarget){
            strTarget = strTarget.replace(/\$\$\$$/, "");
            
            return strTarget;
        }

        /****************************************
         * スクロール同期
         ****************************************/        
        function syncScroll(obj){
            objEBI("<%=DivTitle.clientID %>").scrollLeft=obj.scrollLeft;
        }   

</script>

<body style="margin: 15px 10px 15px 10px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="AjaxScriptManager" runat="server">
        </asp:ScriptManager>
        <%-- 隠し項目 --%>
        <input type="hidden" id="HiddenKubun" runat="server" />
        <input type="hidden" id="HiddenNo" runat="server" />
        <input type="hidden" id="HiddenRegUpdDate" runat="server" />
        <input type="hidden" id="HiddenKameitenCd" runat="server" />
        <input type="hidden" id="HiddenTysHouhouNo" runat="server" />
        <input type="hidden" id="HiddenSyouhinCd" runat="server" />
        <input type="hidden" id="HiddenGetMasterFlg" runat="server" />
        <input type="hidden" id="HiddenArrSyouhinCd" runat="server" />
        <input type="hidden" id="HiddenArrKeijyouFlg" runat="server" />  
        <input type="hidden" id="HiddenArrHattyuuKingaku" runat="server" />  
        <input type="hidden" id="HiddenArrUpdDatetime" runat="server" />    
        <input type="hidden" id="HiddenGamenMode" runat="server" />
        <input type="hidden" id="HiddenTokutaiKkkHaneiFlg" runat="server" />
        <input type="hidden" id="HiddenArrDisplayCd" runat="server" />  
        <input type="hidden" id="HiddenChgTokuCd" runat="server" />  
        <input type="hidden" id="HiddenRentouBukkenSuu" runat="server" />
        <%-- 画面起動時情報保持 --%>
        <asp:HiddenField ID="HiddenOpenValues" runat="server" />
        <%-- 画面タイトル --%>
        <table style="text-align: left; width: 900px" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tr>
                <th style="text-align: left; width: 120px;">
                    特別対応</th>
                <th style="width: 80px;">
                    <input type="button" id="ButtonClose" runat="server" value="閉じる" onclick="toMainWin();" />
                </th>
                <th style="width: 645px;">
                    &nbsp;<input type="button" id="ButtonGetMaster" runat="server" value="マスタ再取得" />
                    <input type="button" id="ButtonSetSetteiSaki" runat="server" value="設定先再設定" style="display:none;" />
                </th>
                <th>
                    <input type="button" runat="server" id="ButtonTouroku1" value="登録 / 修正実行" style="font-weight: bold;
                        font-size: 18px; width: 140px; color: black; height: 30px; background-color: fuchsia" />
                </th>
            </tr>
        </table>
        <br />
        <%-- 画面キー項目（表示のみ） --%>
        <table cellpadding="0" cellspacing="0" style="width: 900px; border-bottom: solid 2px gray;
            border-left: solid 2px gray;" class="mainTable">
            <tr>
                <td class="koumokuMei" style="width: 50px">
                    区分</td>
                <td>
                    <asp:TextBox runat="server" ID="TextKbn" Style="ime-mode: disabled; border-style: none;"
                        ReadOnly="true" CssClass="readOnlyStyle" TabIndex="-1" />
                    <input type="hidden" id="HiddenKbn" runat="server" />
                </td>
                <td class="koumokuMei" style="width: 40px">
                    番号</td>
                <td>
                    <asp:TextBox runat="server" ID="TextBangou" Style="width: 80px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
                <td class="koumokuMei" style="width: 50px">
                    施主名</td>
                <td>
                    <asp:TextBox runat="server" ID="TextSesyuMei" Style="width: 200px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei">
                    加盟店</td>
                <td>
                    <asp:TextBox runat="server" ID="TextKameitenCd" Style="width: 40px;" ReadOnly="true" CssClass="readOnlyStyle"
                        TabIndex="-1" /><asp:TextBox runat="server" ID="TextKameitenMei" Style="width: 200px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
                <td class="koumokuMei">
                    商品1</td>
                <td>
                    <asp:TextBox runat="server" ID="TextSyouhin1" Style="width: 200px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
                <td class="koumokuMei">
                    調査方法</td>
                <td>
                    <asp:TextBox runat="server" ID="TextTysHouhou" Style="width: 200px;" ReadOnly="true"
                        CssClass="readOnlyStyle" TabIndex="-1" />
                </td>
            </tr>
        </table>
        <br />
        <%-- メイン画面 --%>
        <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray;
            table-layout: fixed; border-left: solid 2px gray; border-right: solid 2px gray; width: 900px;">
            <tr>
                <td>
                    <table class="mainTable itemTableNarrow" style="width: 897px; border-bottom: none;
                        border-left: none; border-right: none;" id="Table2">
                        <%-- ヘッダ部 --%>
                        <!-- 1行目 -->
                        <tr class="shouhinTableTitle">
                            <td>
                                特別対応一覧</td>
                        </tr>
                     </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelTbl" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonGetMaster" EventName="ServerClick" />
                            <asp:AsyncPostBackTrigger ControlID="ButtonSetSetteiSaki" EventName="ServerClick" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="DivTitle" class="scrollDivRightTitleStyle2" runat="server" style="width: 897px;">
                                <table class="mainTable mainTableFont itemTableNarrow" style="width: 897px; 
                                    border-left: none; border-right:none; border-top: none; border-bottom: none; table-layout: fixed;">
                                    <thead id="tblHeader" runat="server">
                                        <tr>
                                            <td class="koumokuMei2" style="width: 290px; font-size:13px;">
                                                特別対応名</td>
                                            <td class="koumokuMei2" style="width: 77px; font-size:13px;">
                                                設定先</td>
                                            <td class="koumokuMei2" style="width: 45px; font-size:13px;">
                                                売上<br />
                                                計上</td>
                                            <td class="koumokuMei2" style="width: 65px; font-size:13px;">
                                                商品<br />
                                                コード</td>
                                            <td class="koumokuMei2" style="width: 230px; font-size:13px;">
                                                商品名</td>
                                            <td class="koumokuMei2" style="width: 77px; font-size:13px;">
                                                工務店請求<br />
                                                加算金額</td>
                                            <td class="koumokuMei2" style="width: 77px; font-size:13px;">
                                                実請求<br />
                                                加算金額</td>
                                            <td class="koumokuMei2" style="width: 50px; font-size:13px;">
                                                価格<br />
                                                処理</td>
                                            <td style="width: 14px;">
                                                </td>
                                        </tr>                                    
                                    </thead>
                                </table>
                            </div>
                            <div id="DivData" class="scrollDivStyle2" style="width: 897px; height: 420px; border:none;" runat="server" onscroll="syncScroll(this);">
                                <table class="mainTable" cellpadding="0" cellspacing="0" style="width: 897px; border-top: none; table-layout: fixed;
                                    border-left: none; border-right:none;">
                                    <!-- データ部 -->
                                    <tbody id="tblMeisai" runat="server">
                                    </tbody>
                                </table>
                            </div>
                            <input type="hidden" id="HiddenLineCnt" runat="server" value="0" />
                            <input type="hidden" id="HiddenPressMasterFlg" runat="server" value="0" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
