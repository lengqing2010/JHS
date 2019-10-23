<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SearchTyoufuku.aspx.vb"
    Inherits="Itis.Earth.WebUI.SearchTyoufuku" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EARTH 重複物件一覧</title>
</head>

<script type="text/javascript" src="js/jhsearth.js">
</script>

<script type="text/javascript">
    try{
        window.resizeTo(800,500);
    }catch(e){
        //アクセスが拒否されましたのエラーが出たら何もしない。
        if(e.number == 2147024891){
            throw e;
        }
    }
    
    /**
     * 明細行をダブルクリックした際の処理
     * @param objSelectedTr
     * @return
     */
    function returnSelectValue(objSelectedTr) {
        if(objSelectedTr.tagName == "TR"){
            var objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
            objSendTargetWin.value = "_self";
            sendKakunin(objSelectedTr);
        }
        return;
    }
    
    /**
     * 別ウィンドウアイコンをクリックした際の処理
     * @param objOnCklick
     * @return
     */
    function returnSelectValueOtherWin(objOnCklick){
        var objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
        objSendTargetWin.value = "_blank";
        sendKakunin(objOnCklick.parentNode);
    }

    /**
     * 物件確認画面表示処理
     * @param objSelectedTr
     * @return
     */
    function sendKakunin(objSelectedTr){
        //オブジェクトの再読込(Ajax再描画対応)
        var objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
        objSendTargetWin = objEBI("<%= sendTargetWin.clientID %>");
        if(objSendTargetWin.value == "")objSendTargetWin.value="_self";

        //戻り値郡配列(行の先頭セルの先頭オブジェクトから取得)
        var objSelRet = getChildArr(getChildArr(objSelectedTr,"TD")[0],"INPUT")[0];
        var arrReturnValue = objSelRet.value.split(sepStr);  

        //<!-- 画面引渡し情報 -->
        objSendForm = objEBI("<%= mstSearch.clientID %>");
        objSendVal_st = objEBI("st");
        objSendVal_Kubun = objEBI("sendPage_kubun");
        objSendVal_HosyousyoNo = objEBI("sendPage_hosyoushoNo");

        objSendForm.action="<%= UrlConst.IRAI_KAKUNIN %>";
        objSendForm.target=objSendTargetWin.value;
        objSendVal_st.value="<%=EarthConst.MODE_VIEW %>";
        objSendVal_Kubun.value=arrReturnValue[0];
        objSendVal_HosyousyoNo.value=arrReturnValue[1];
        window.open(objSendForm.action + "?sendPage_kubun=" + objSendVal_Kubun.value + "&sendPage_hosyoushoNo="+objSendVal_HosyousyoNo.value + "&st=" + objSendVal_st.value,objSendForm.target)
    }
</script>

<body>
    <form method="post" id="mstSearch" runat="server">
        <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
            class="titleTable">
            <tbody>
                <tr>
                    <th>
                        重複物件一覧</th>
                    <th style="text-align: right;">
                    </th>
                </tr>
                <tr>
                    <td colspan="2" rowspan="1">
                        <input id="btnCloseWin" value="閉じる" type="button" runat="server" /></td>
                </tr>
            </tbody>
        </table>
        <br />
        <input type="hidden" id="parmKubun" runat="server" />
        <input type="hidden" id="parmHosyousyoNo" runat="server" />
        <input type="hidden" id="parmSeshuNm" runat="server" />
        <input type="hidden" id="parmJyuusho1" runat="server" />
        <input type="hidden" id="parmJyuusho2" runat="server" />
        <input id="sendTargetWin" name="sendTargetWin" runat="server" type="hidden" />
        <input id="sendPage_kubun" name="sendPage_kubun" runat="server" type="hidden" />
        <input id="sendPage_hosyoushoNo" name="sendPage_hosyoushoNo" runat="server" type="hidden" />
        <input id="st" runat="server" name="st" type="hidden" />
        <input type="button" id="returnBtn" runat="server" style="display: none" />
        <div class="dataGridHeader" id="dataGridContent">
            <table class="scrolltablestyle" cellpadding="0" cellspacing="0" style="width: 840px">
                <thead>
                    <tr id="meisaiTableHeaderTr" style="position: relative; top: expression(this.offsetParent.scrollTop);">
                        <th style="width: 10px;" nowrap>
                            &nbsp;</th>
                        <th style="width: 70px;" nowrap>
                            破棄種別</th>
                        <th style="width: 40px;" nowrap>
                            区分</th>
                        <th style="width: 70px;" nowrap>
                            保証書NO</th>
                        <th style="width: 200px;" nowrap>
                            施主名</th>
                        <th style="width: 200px;" nowrap>
                            物件住所１</th>
                        <th style="width: 200px;" nowrap>
                            物件住所２</th>
                    </tr>
                </thead>
                <tbody id="searchGrid" runat="server">
                    <!-- 取得結果自動生成 -->
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
