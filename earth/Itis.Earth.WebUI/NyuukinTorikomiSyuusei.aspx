<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/EarthMasterPage.Master"
    Codebehind="NyuukinTorikomiSyuusei.aspx.vb" Inherits="Itis.Earth.WebUI.NyuukinTorikomiSyuusei"
    Title="EARTH 入金取込データ修正" %>

<%@ Import Namespace="Itis.Earth.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <script type="text/javascript" src="js/jhsearth.js">
    </script>

    <script type="text/javascript">
        history.forward();
        
        //ウィンドウサイズ変更
        try{
            if(window.name != "<%=EarthConst.MAIN_WINDOW_NAME %>") window.resizeTo(720,724);
        }catch(e){
            //アクセスが拒否されましたのエラーが出たら何もしない。
            if(e.number == 2147024891) throw e;
        }
               
        _d = document;

        /*====================================
         *グローバル変数宣言（画面部品）
         ====================================*/        
        //画面表示部品
        var objTotalGaku = null;
        var objGenkin = null;
        var objKogitte = null;
        var objKouzaFurikae = null;
        var objFurikomi = null;
        var objTegata = null;
        var objKyouryokuKaihi = null;
        var objFurikomiTesuuRyou = null;
        var objSousai = null;
        var objNebiki = null;
        var objSonota = null;

        /*************************************
         * onload時の追加処理
         *************************************/
        function funcAfterOnload() {
            //画面表示部品セット
            setGlobalObj();
            
            //合計金額の計算
            CalcTotalGaku();      
        }
        
       /*********************************************
        * 画面表示部品オブジェクトをグローバル変数化
        *********************************************/
        function setGlobalObj() {
            //オブジェクトの取得
            objTotalGaku = objEBI("<%= TextDenpyouGoukeiGaku.clientID %>");
            
            objGenkin = objEBI("<%= TextNkGenkin.clientID %>");
            objKogitte = objEBI("<%= TextNkKogitte.clientID %>");   
            objKouzaFurikae = objEBI("<%= TextNkKouzaFurikae.clientID %>");
            
            objFurikomi = objEBI("<%= TextNkFurikomi.clientID %>");
            objTegata = objEBI("<%= TextNkTegata.clientID %>");
            objKyouryokuKaihi = objEBI("<%= TextNkKyouryokuKaihi.clientID %>");
            
            objFurikomiTesuuRyou = objEBI("<%= TextNkFurikomiTesuuryou.clientID %>");
            objSousai = objEBI("<%= TextNkSousai.clientID %>");
            objNebiki = objEBI("<%= TextNkNebiki.clientID %>");
            
            objSonota = objEBI("<%= TextNkSonota.clientID %>");

        }
               
        //入金額内変更時処理(伝票合計金額計算処理)
        function CalcTotalGaku(){                   
            var varTmp = 0; //伝票合計金額(作業用)
                       
            var varNkGenkin = removeFigure(objGenkin.value);
            var varNkKogitte = removeFigure(objKogitte.value);
            var varNkKouzaFurikae = removeFigure(objKouzaFurikae.value);

            var varNkFurikomi = removeFigure(objFurikomi.value);
            var varNkTegata = removeFigure(objTegata.value);
            var varNkKyouryokuKaihi = removeFigure(objKyouryokuKaihi.value);
            
            var varNkFurikomiTesuuRyou = removeFigure(objFurikomiTesuuRyou.value);
            var varNkSousai = removeFigure(objSousai.value);
            var varNkNebiki = removeFigure(objNebiki.value);
            
            var varNkSonota = removeFigure(objSonota.value);
            
            //伝票合計金額の算出
            varTmp = Number(varNkGenkin) + Number(varNkKogitte) + Number(varNkKouzaFurikae) 
                    + Number(varNkFurikomi) + Number(varNkTegata) + Number(varNkKyouryokuKaihi) 
                    + Number(varNkFurikomiTesuuRyou) + Number(varNkSousai) + Number(varNkNebiki) 
                    + Number(varNkSonota);  
                    
            //金額の設定＆カンマ付与
            objTotalGaku.value = addFigure(varTmp);           
        }
        
         /*********************
         * 請求先情報クリア
         *********************/
        function clrSeikyuuSakiInfo(){
                //値のクリア
                objEBI("<%= TextTorikesiRiyuu.clientID %>").value = "";
                //色をデフォルトへ
                objEBI("<%= SelectSeikyuuSakiKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= SpanSeikyuuKbn.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiCd.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiBrc.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
                objEBI("<%= TextSeikyuuSakiMei.clientID %>").style.color = "<%= EarthConst.STYLE_COLOR_BLACK %>"
        }
        
    </script>

    <asp:ScriptManagerProxy ID="SMP1" runat="server">
    </asp:ScriptManagerProxy>
    <input type="hidden" id="HiddenUpdDatetime" runat="server" />
    <input type="hidden" id="HiddenGamenMode" runat="server" />
    <%-- 画面起動時情報保持 --%>
    <asp:HiddenField ID="HiddenOpenValues" runat="server" />
    <div>
        <%-- 画面タイトル --%>
        <table>
            <tr>
                <td>
                    <table style="text-align: left; width: 100%;" border="0" cellpadding="0" cellspacing="2"
                        class="titleTable">
                        <tr>
                            <th style="text-align: left; width: 160px;">
                                <span id="SpanTitle" runat="server"></span>
                            </th>
                            <th style="width: 70px">
                                <input type="button" id="ButtonClose" value="閉じる" onclick="window.close();" runat="server" />
                            </th>
                            <th style="text-align: right">
                                <input type="button" id="ButtonUpdate" runat="server" value="" style="font-weight: bold;
                                    font-size: 18px; width: 120px; color: black; height: 30px; background-color: fuchsia" />
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <%-- 画面上部[データ詳細] --%>
        <table cellpadding="0" cellspacing="0" style="border-bottom: solid 2px gray; border-left: solid 2px gray;"
            class="mainTable">
            <tr>
                <td class="koumokuMei" style="width: 110px" colspan="2">
                    入金取込NO</td>
                <td>
                    <asp:TextBox ID="TextNyuukinTorikomiNo" runat="server" Style="width: 50px" CssClass="number readOnlyStyle"
                        ReadOnly="true" TabIndex="-1" />
                </td>
                <td class="koumokuMei">
                    EDI情報作成日</td>
                <td colspan="2">
                    <asp:TextBox ID="TextEdiJouhouSakuseiDate" runat="server" Style="width: 280px" CssClass="date readOnlyStyle"
                        ReadOnly="true" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    取消</td>
                <td style="width: 70px">
                    <asp:CheckBox ID="CheckTorikesi" runat="server" /><span id="SpanTorikesi" runat="server"></span></td>
                <td class="koumokuMei">
                    入金日</td>
                <td colspan="2">
                    <asp:TextBox ID="TextNyuukinDate" runat="server" Style="width: 70px" CssClass="date hissu"
                        MaxLength="10" /></td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    取込伝票番号</td>
                <td>
                    <asp:TextBox ID="TextTorikomiDenpyouNo" runat="server" Style="width: 60px" CssClass="codeNumber hissu"
                        MaxLength="6" />
                </td>
                <td class="koumokuMei">
                    照合口座No.</td>
                <td colspan="2">
                    <asp:TextBox ID="TextSyougouKouzaNo" runat="server" Style="width: 90px" CssClass="codeNumber hissu"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2" rowspan="2">
                    請求先</td>
                <td colspan="2" style="width: 300px;">
                    <asp:UpdatePanel ID="UpdatePanel_seikyuu" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="SelectSeikyuuSakiKbn" runat="server" CssClass="hissu">
                            </asp:DropDownList><span id="SpanSeikyuuKbn" runat="server"></span>
                            <asp:TextBox ID="TextSeikyuuSakiCd" runat="server" MaxLength="5" Style="width: 40px;"
                                CssClass="codeNumber hissu" />&nbsp;-
                            <asp:TextBox ID="TextSeikyuuSakiBrc" runat="server" MaxLength="2" Style="width: 20px;"
                                CssClass="codeNumber hissu" />
                            <input id="ButtonSearchSeikyuuSaki" runat="server" type="button" value="検索" class="gyoumuSearchBtn" />
                            <input type="hidden" id="HiddenSeikyuuSakiCdOld" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiBrcOld" runat="server" />
                            <input type="hidden" id="HiddenSeikyuuSakiKbnOld" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="koumokuMei" style="width: 100px">
                    取消</td>
                <td id="TdTorikesiRiyuu">
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiToikesi" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonSearchSeikyuuSaki" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="TextTorikesiRiyuu" runat="server" Style="width: 10em;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel_SeikyuusakiMei" UpdateMode="Conditional" runat="server"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonSearchSeikyuuSaki" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="TextSeikyuuSakiMei" Style="width: 420px;" runat="server" MaxLength="80" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" rowspan="10">
                    入金額</td>
                <td class="shouhinTableTitleNyuukin" style="width: 110px">
                    現金</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkGenkin" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    小切手</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkKogitte" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    口座振替</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkKouzaFurikae" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    振込</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkFurikomi" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    手形</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkTegata" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    協力会費</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkKyouryokuKaihi" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    振込<br />
                    手数料</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkFurikomiTesuuryou" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    相殺</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkSousai" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    値引</td>
                <td colspan="5">
                    <asp:TextBox ID="TextNkNebiki" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="shouhinTableTitleNyuukin">
                    その他</td>
                <td>
                    <asp:TextBox ID="TextNkSonota" runat="server" Style="width: 90px" CssClass="kingaku"
                        MaxLength="10" />
                </td>
                <td class="shouhinTableTitleNyuukin" style="width: 120px">
                    伝票合計金額</td>
                <td colspan="2">
                    <asp:TextBox ID="TextDenpyouGoukeiGaku" runat="server" Style="width: 120px" CssClass="kingaku readOnlyStyle"
                        ReadOnly="true" TabIndex="-1" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    手形期日</td>
                <td>
                    <asp:TextBox ID="TextTegataKijitu" runat="server" Style="width: 70px" CssClass="date"
                        MaxLength="10" />
                </td>
                <td class="koumokuMei">
                    手形No.</td>
                <td colspan="2">
                    <asp:TextBox ID="TextTegataNo" runat="server" Style="width: 90px" CssClass="codeNumber"
                        MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class="koumokuMei" colspan="2">
                    摘要名</td>
                <td colspan="5">
                    <textarea id="TextAreaTekiyou" runat="server" cols="80" onfocus="this.select();"
                        rows="4" style="ime-mode: active; font-family: Sans-Serif"></textarea>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
