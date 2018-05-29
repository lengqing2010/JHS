Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Partial Public Class BukkenJyouhouSearch
    Inherits System.Web.UI.Page
    '営業情報検索BL
    Private EigyouJyouhouInquiryBL As New EigyouJyouhouInquiryLogic
    Dim user_info As New LoginUserInfo
    Private commonCheck As New CommonCheck
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ログインユーザーを取得する。
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '地盤画面共通クラス
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")
        'ログインユーザーIDを取得する。
        ViewState("userId") = ninsyou.GetUserID()
        ' ユーザー基本認証
        jBn.userAuth(user_info)

        If ViewState("userId") = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E '（"該当ユーザーがありません。"）
            Server.Transfer("CommonErr.aspx")
        End If
        MakeJavascript()
        If Not IsPostBack Then
            CommonCheck.SetURL(Me, ViewState("userId"))
            getEigyouManKbn()
            '組織レベルを設定する
            setSosikiLabel()
        Else
            closecover()
        End If
        Me.chkKubunAll.Attributes.Add("onClick", "fncSetKubunVal();")


        txtBangouF.Attributes.Add("Style", "ime-mode:disabled;")

        txtBangouT.Attributes.Add("Style", "ime-mode:disabled;")

        tbxIraiF.Attributes.Add("onblur", "checkDate(this);")
        tbxIraiF.Attributes.Add("Style", "ime-mode:disabled;")
        tbxIraiT.Attributes.Add("onblur", "checkDate(this);")
        tbxIraiT.Attributes.Add("Style", "ime-mode:disabled;")
        tbxKeikakusyoF.Attributes.Add("onblur", "checkDate(this);")
        tbxKeikakusyoF.Attributes.Add("Style", "ime-mode:disabled;")
        tbxKeikakusyoT.Attributes.Add("onblur", "checkDate(this);")
        tbxKeikakusyoT.Attributes.Add("Style", "ime-mode:disabled;")
        tbxYoteiF.Attributes.Add("onblur", "checkDate(this);")
        tbxYoteiF.Attributes.Add("Style", "ime-mode:disabled;")
        tbxYoteiT.Attributes.Add("onblur", "checkDate(this);")
        tbxYoteiT.Attributes.Add("Style", "ime-mode:disabled;")
        tbxJissiF.Attributes.Add("onblur", "checkDate(this);")
        tbxJissiF.Attributes.Add("Style", "ime-mode:disabled;")
        tbxJissiT.Attributes.Add("onblur", "checkDate(this);")
        tbxJissiT.Attributes.Add("Style", "ime-mode:disabled;")
        tbxuriageF.Attributes.Add("onblur", "checkDate(this);")
        tbxuriageF.Attributes.Add("Style", "ime-mode:disabled;")
        tbxUriageT.Attributes.Add("onblur", "checkDate(this);")
        tbxUriageT.Attributes.Add("Style", "ime-mode:disabled;")
        tbxKYoteiF.Attributes.Add("onblur", "checkDate(this);")
        tbxKYoteiF.Attributes.Add("Style", "ime-mode:disabled;")
        tbxKYoteiT.Attributes.Add("onblur", "checkDate(this);")
        tbxKYoteiT.Attributes.Add("Style", "ime-mode:disabled;")

        tbxKJissiF.Attributes.Add("onblur", "checkDate(this);")
        tbxKJissiF.Attributes.Add("Style", "ime-mode:disabled;")
        tbxKJissiT.Attributes.Add("onblur", "checkDate(this);")
        tbxKJissiT.Attributes.Add("Style", "ime-mode:disabled;")

        tbxKUriageF.Attributes.Add("onblur", "checkDate(this);")
        tbxKUriageF.Attributes.Add("Style", "ime-mode:disabled;")
        tbxKUriageT.Attributes.Add("onblur", "checkDate(this);")
        tbxKUriageT.Attributes.Add("Style", "ime-mode:disabled;")


        tbxEigyousyoMei.Attributes.Add("readonly", "true")
        tbxKeiretuMei.Attributes.Add("readonly", "true")
        tbxKameitenMei.Attributes.Add("readonly", "true")
        tbxTantouEigyouSyaMei.Attributes.Add("readonly", "true")

        Me.tbxKameitenCd1.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxKeiretuCd.Attributes.Add("onBlur", "fncToUpper(this);")


        tbxKameitenCd1.Attributes.Add("Style", "ime-mode:disabled;")
        tbxKeiretuCd.Attributes.Add("Style", "ime-mode:disabled;")
        tbxEigyousyoCd.Attributes.Add("Style", "ime-mode:disabled;")
        tbxTantouEigyouID.Attributes.Add("Style", "ime-mode:disabled;")



        Me.btnKensaku.Attributes.Add("onClick", "if(fncNyuuryokuCheck('1')==true){ShowModal();}else{return false}")

        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        Me.tbxTorikesi.Attributes.Add("readonly", "true")
        '加盟店の色をセットする
        Call Me.SetColor()
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================

    End Sub
    Private Sub setSosikiLabel()

        Dim dtSosikiLevel As EigyouJyouhouDataSet.sosikiLabelDataTable
        If ViewState("busyo_cd") = "0000" Or ViewState("t_sansyou_busyo_cd") = "0000" Then
            If ViewState("eigyouManKbn") <> "1" Then
                dtSosikiLevel = EigyouJyouhouInquiryBL.GetSosikiLabelInfo()
            Else
                dtSosikiLevel = EigyouJyouhouInquiryBL.GetSosikiLabelInfo2(ViewState("sosikiLevel"), ViewState("sosikiLevel2"), ViewState("busyo_cd"), ViewState("t_sansyou_busyo_cd"), ViewState("eigyouManKbn"))

            End If


        Else
            dtSosikiLevel = EigyouJyouhouInquiryBL.GetSosikiLabelInfo2(ViewState("sosikiLevel"), ViewState("sosikiLevel2"), ViewState("busyo_cd"), ViewState("t_sansyou_busyo_cd"), ViewState("eigyouManKbn"))

        End If
        Me.ddlSosikiLevel.Items.Clear()
        Me.ddlSosikiLevel.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        For i As Integer = 0 To dtSosikiLevel.Rows.Count - 1
            Dim ddlist As New ListItem
            ddlist = New ListItem
            ddlist.Text = dtSosikiLevel.Rows(i).Item(0).ToString & "：" & dtSosikiLevel.Rows(i).Item(1).ToString
            ddlist.Value = dtSosikiLevel.Rows(i).Item(0).ToString
            ddlSosikiLevel.Items.Add(ddlist)
        Next

    End Sub
    ''' <summary>ログインユーザーの営業マン区分と組織レベルを取得する。</summary>
    Private Sub getEigyouManKbn()

        Dim dtEigyouManKbn As EigyouJyouhouDataSet.eigyouManKbnDataTable
        dtEigyouManKbn = EigyouJyouhouInquiryBL.GetEigyouManKbnInfo(ViewState("userId"))

        If dtEigyouManKbn.Rows.Count > 0 Then
            '0:通常、1:新人
            ViewState("eigyouManKbn") = TrimNull(dtEigyouManKbn.Rows(0).Item("eigyou_man_kbn"))

            ViewState("sosikiLevel") = TrimNull(dtEigyouManKbn.Rows(0).Item("sosiki_level"))

            ViewState("busyo_cd") = TrimNull(dtEigyouManKbn.Rows(0).Item("busyo_cd"))

            ViewState("t_sansyou_busyo_cd") = TrimNull(dtEigyouManKbn.Rows(0).Item("t_sansyou_busyo_cd"))

            ViewState("sosikiLevel2") = TrimNull(Replace(dtEigyouManKbn.Rows(0).Item("sosiki_level2"), "-1", ""))

            If ViewState("eigyouManKbn") = "" Then
                ViewState("busyo_cd") = "0000"
                ViewState("t_sansyou_busyo_cd") = "0000"
                ViewState("eigyouManKbn") = "0"
                ViewState("sosikiLevel") = "0"
                ViewState("sosikiLevel2") = "-1"
            End If

        Else
            ViewState("busyo_cd") = "0000"
            ViewState("t_sansyou_busyo_cd") = "0000"
            ViewState("eigyouManKbn") = "0"
            ViewState("sosikiLevel") = "0"
            ViewState("sosikiLevel2") = "-1"


        End If
        If ViewState("eigyouManKbn") = "1" Then
            chkBusyoCd.Checked = True
            chkBusyoCd.Enabled = False
        End If

    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn12.Click, btn2.Click, btn3.Click, btn6.Click, btn1.Click
        Dim strNum As String = GetNum(CType(sender, Button).ID)
        txtBangouF.Text = Split(strNum, ",")(0)
        txtBangouT.Text = Split(strNum, ",")(1)
    End Sub
    Function GetNum(ByVal strName As String) As String
        Dim intMonth As Integer = CInt(Replace(strName, "btn", ""))
        Dim dateNow As Date = Now.AddMonths(1 - intMonth)

        GetNum = Year(dateNow) & Right("0" & Month(dateNow), 2) & "0001" & "," & Year(Now) & Right("0" & Month(Now), 2) & "9999"

    End Function
    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("   var objKbn1 = document.getElementById('" & Me.Common_drop1.DdlClientID & "')")
            .AppendLine("   var objKbn2 = document.getElementById('" & Me.Common_drop2.DdlClientID & "')")
            .AppendLine("   var objKbn3 = document.getElementById('" & Me.Common_drop3.DdlClientID & "')")
            .AppendLine("   var objKbnAll = document.getElementById('" & Me.chkKubunAll.ClientID & "')")
          
            .AppendLine("   function fncSetKubunVal(){")
            .AppendLine("       if(objKbnAll.checked == true){")
            .AppendLine("           objKbn1.selectedIndex = 0;")
            .AppendLine("           objKbn2.selectedIndex = 0;")
            .AppendLine("           objKbn3.selectedIndex = 0;")
            .AppendLine("           objKbn1.disabled = true;")
            .AppendLine("           objKbn2.disabled = true;")
            .AppendLine("           objKbn3.disabled = true;")
            .AppendLine("       }else{")
            .AppendLine("           objKbn1.disabled = false;")
            .AppendLine("           objKbn2.disabled = false;")
            .AppendLine("           objKbn3.disabled = false;")
            .AppendLine("       }")
            .AppendLine("   }")
            '加盟店ポップアップ
            .AppendLine("   function fncKameitenSearch(strKbn){")
            .AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       var strkbn='加盟店';")
            .AppendLine("       var strClientID; ")
            .AppendLine("       var strClientMei; ")
            '============2012/03/29 車龍 405721案件の対応 修正↓=========================
            .AppendLine("       var strHidTorikesiCd; ")
            .AppendLine("       var strTxtTorikesiCd; ")
            '.AppendLine("       var blnTaikai='True' ")
            .AppendLine("       var blnTaikai='False'; ")
            '============2012/03/29 車龍 405721案件の対応 修正↑=========================
            .AppendLine("       var arrKubun = ''")
            .AppendLine("       if(objKbn1.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn1.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn2.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn3.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn3.value + ',';")
            .AppendLine("       }")
            .AppendLine("       arrKubun = arrKubun.substring(0,arrKubun.length-1);")
            .AppendLine("       strClientID = '" & Me.tbxKameitenCd1.ClientID & "'")
            .AppendLine("       strClientMei = '" & Me.tbxKameitenMei.ClientID & "'")
            '============2012/03/29 車龍 405721案件の対応 追加↓=========================
            .AppendLine("       strHidTorikesiCd = '" & Me.hidTorikesi.ClientID & "'")
            .AppendLine("       strTxtTorikesiCd = '" & Me.tbxTorikesi.ClientID & "'")
            '============2012/03/29 車龍 405721案件の対応 追加↑=========================
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientID+'&objMei='+strClientMei+'&strMei='+escape(eval('document.all.'+strClientMei).value)+'&strCd='+escape(eval('document.all.'+strClientID).value)+")
            .AppendLine("       '&KensakuKubun='+arrKubun+'&blnDelete='+blnTaikai+'&HidTorikesiCd='+strHidTorikesiCd+'&TxtdTorikesiCd='+strTxtTorikesiCd+'&btnChangeColorCd=btnChangeColor', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '営業所ポップアップ
            .AppendLine("   function fncEigyousyoSearch(strKbn){")
            .AppendLine("       var strkbn='営業所'")
            .AppendLine("       var strClientID ")
            .AppendLine("       var strClientMei ")
            .AppendLine("       var blnTaikai='True' ")
            .AppendLine("       strClientID = '" & Me.tbxEigyousyoCd.ClientID & "'")
            .AppendLine("       strClientMei = '" & Me.tbxEigyousyoMei.ClientID & "'")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd='+strClientID+'&objMei='+strClientMei+'&strMei='+escape(eval('document.all.'+strClientMei).value)+'&strCd='+escape(eval('document.all.'+strClientID).value)+")
            .AppendLine("       '&KensakuKubun=&blnDelete='+blnTaikai, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '系列ポップアップ
            .AppendLine("   function fncKeiretuSearch(){")
            .AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       var strkbn='系列'")
            .AppendLine("       var blnTaikai='True' ")
            .AppendLine("       var arrKubun = ''")
            .AppendLine("       if(objKbn1.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn1.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn2.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn3.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn3.value + ',';")
            .AppendLine("       }")
            .AppendLine("       arrKubun = arrKubun.substring(0,arrKubun.length-1);")

            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxKeiretuCd.ClientID & "&objMei=" & Me.tbxKeiretuMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxKeiretuCd.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun='+arrKubun+'&blnDelete='+blnTaikai, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '営業担当IDを検索する。
            .AppendLine("   function fncUserSearch(){")
            .AppendLine("       var strkbn='ユーザー';")
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd=" & Me.tbxTantouEigyouID.ClientID & "&objMei=" & Me.tbxTantouEigyouSyaMei.ClientID & "&strCd='+escape(eval('document.all.'+'" & Me.tbxTantouEigyouID.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & Me.tbxTantouEigyouSyaMei.ClientID & "').value)+'&blnDelete=True', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("   return false;")
            .AppendLine("   }")
            .AppendLine("   function fncClearWin(){")
            .AppendLine("       for(var i = 0;i < document.forms.length;i++){")
            .AppendLine("          c_form = document.forms[i];")
            .AppendLine("          for (var j = 0;j < c_form.elements.length;j++){")
            .AppendLine("              if(c_form.elements[j].type == 'text'){")
            .AppendLine("                  c_form.elements[j].value = '';")
            .AppendLine("              }")
            .AppendLine("              if(c_form.elements[j].type == 'checkbox'){")
            .AppendLine("                  c_form.elements[j].checked = false;")
            .AppendLine("              }")
            .AppendLine("          }")
            .AppendLine("       }")
            '============2012/03/29 車龍 405721案件の対応 追加↓=========================
            .AppendLine("       document.getElementById('" & Me.hidTorikesi.ClientID & "').value = '';")
            '============2012/03/29 車龍 405721案件の対応 追加↑=========================
            .AppendLine("       objKbn1.selectedIndex = 0;")
            .AppendLine("       objKbn1.disabled = true;")
            .AppendLine("       objKbn2.selectedIndex = 0;")
            .AppendLine("       objKbn2.disabled = true;")
            .AppendLine("       objKbn3.selectedIndex = 0;")
            .AppendLine("       objKbn3.disabled = true;")
            .AppendLine("       objKbnAll.checked = true;")
            '※
            .AppendLine("       document.getElementById('" & chkKouji.ClientID & "').checked = true;")
            .AppendLine("       document.getElementById('" & CType(Me.Common_drop4.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & CType(Me.Common_drop5.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & CType(Me.Common_drop6.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & Me.ddlSearchCount.ClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & Me.ddlSosikiLevel.ClientID & "').selectedIndex = 0;")

            .AppendLine("       for(var i = document.getElementById('" & Me.ddlBusyoCd.ClientID & "').options.length-1;i >=0;i--){")
            .AppendLine("           document.getElementById('" & Me.ddlBusyoCd.ClientID & "').options.remove(i);")
            .AppendLine("       }")

            .AppendLine("   }")
            '画面入力チェック
            .AppendLine("   function fncNyuuryokuCheck(strButtonFlg){")
            .AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '.AppendLine("       if(strButtonFlg=='1' && document.getElementById('" & Me.ddlSearchCount.ClientID & "').value=='max'){")
            '.AppendLine("           if (confirm('" & Messages.Instance.MSG007C & "\r\n（件数によってはエラーになる可能性があります。）" & "')){")
            '.AppendLine("               document.forms[0].submit();")
            '.AppendLine("           }else{")
            '.AppendLine("               return false;")
            '.AppendLine("           }")
            '.AppendLine("       }")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("function ShowModal()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")

            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("   if(buyDiv.style.display=='none')")
            .AppendLine("   {")
            .AppendLine("    buyDiv.style.display='';")
            .AppendLine("    disable.style.display='';")
            .AppendLine("    disable.focus();")
            .AppendLine("   }else{")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("}")


            .AppendLine("function closecover()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")

            .AppendLine("}")
            '============2012/03/29 車龍 405721案件の対応 追加↓=========================
            .AppendLine("function fncChangeColor()")
            .AppendLine("{")
            .AppendLine("   var strColor;")
            .AppendLine("   if((document.getElementById('" & Me.hidTorikesi.ClientID & "').value == '0')||(document.getElementById('" & Me.hidTorikesi.ClientID & "').value == ''))")
            .AppendLine("   {")
            .AppendLine("       strColor = 'black';")
            .AppendLine("   }")
            .AppendLine("   else")
            .AppendLine("   {")
            .AppendLine("       strColor = 'red';")
            .AppendLine("   }")
            .AppendLine("	document.getElementById('" & Me.tbxKameitenCd1.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKameitenMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxTorikesi.ClientID & "').style.color = strColor; ")
            .AppendLine("}")
            '============2012/03/29 車龍 405721案件の対応 追加↑=========================
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub
    ''' <summary>空白文字の削除処理</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    Private Sub ddlSosikiLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSosikiLevel.SelectedIndexChanged
        If Me.ddlSosikiLevel.SelectedIndex = 0 Then
            Me.ddlBusyoCd.Items.Clear()
        Else
            setBusyo(Me.ddlSosikiLevel.SelectedValue.ToString)
        End If
    End Sub
    ''' <summary>部署コードを設定する。</summary>
    ''' <param name="strSosikiCd">選択された組織レベルコード</param>
    Private Sub setBusyo(ByVal strSosikiCd As String)

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        Dim dtBusyoCd As EigyouJyouhouDataSet.busyoCdDataTable
        If ViewState("eigyouManKbn") <> "0" Then

            If ViewState("sosikiLevel") = strSosikiCd Then
                dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo2(strSosikiCd, ViewState("busyo_cd"), "")
            Else
                dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo2(strSosikiCd, "", ViewState("t_sansyou_busyo_cd"))
            End If
        Else
            If ViewState("busyo_cd") = "0000" Or ViewState("t_sansyou_busyo_cd") = "0000" Then
                dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo(strSosikiCd)
            Else
                dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo2(strSosikiCd, ViewState("busyo_cd"), ViewState("t_sansyou_busyo_cd"))

            End If

        End If

        Me.ddlBusyoCd.Items.Clear()
        If dtBusyoCd.Rows.Count <> 1 Then
            Me.ddlBusyoCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        End If
        For i As Integer = 0 To dtBusyoCd.Rows.Count - 1
            Dim ddlist As New ListItem
            ddlist = New ListItem
            ddlist.Text = dtBusyoCd.Rows(i).Item(0).ToString & "：" & dtBusyoCd.Rows(i).Item(1).ToString
            ddlist.Value = dtBusyoCd.Rows(i).Item(0).ToString
            ddlBusyoCd.Items.Add(ddlist)

        Next

    End Sub

    Protected Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        '入力チェック
        Dim blnDrop As Boolean = False
        Dim strUrl As String = ""
        Dim strUrlTMP As String = ""
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId, blnDrop).ToString
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId, blnDrop)
            Exit Sub
        End If
        If Common_drop1.SelectedValue <> "" Then
            strUrl = strUrl & Common_drop1.SelectedValue & ","
        End If
        If Common_drop2.SelectedValue <> "" Then
            strUrl = strUrl & Common_drop2.SelectedValue & ","
        End If
        If Common_drop3.SelectedValue <> "" Then
            strUrl = strUrl & Common_drop3.SelectedValue & ","
        End If
        If strUrl = "" Then
            strUrl = " "
        End If
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
        If tbxKameitenCd1.Text <> "" Then
            Dim dtKameitenSearchTable As New Itis.Earth.DataAccess.CommonSearchDataSet.KameitenSearchTableDataTable
            dtKameitenSearchTable = CommonSearchLogic.GetKameitenKensakuInfo("1", _
                                                                        Left(strUrl, Len(strUrl) - 1), _
                                                                        tbxKameitenCd1.Text, _
                                                                        "", False)
            If dtKameitenSearchTable.Rows.Count = 1 Then
                tbxKameitenMei.Text = TrimNull(dtKameitenSearchTable.Rows(0).Item("kameiten_mei1"))

                '============2012/03/29 車龍 405721案件の対応 追加↓=========================
                '取消
                Me.hidTorikesi.Value = TrimNull(dtKameitenSearchTable.Rows(0).Item("torikesi")).Trim
                If TrimNull(dtKameitenSearchTable.Rows(0).Item("torikesi")).Trim.Equals("0") Then
                    Me.tbxTorikesi.Text = String.Empty
                Else
                    Me.tbxTorikesi.Text = TrimNull(dtKameitenSearchTable.Rows(0).Item("torikesi")).Trim & ":" & TrimNull(dtKameitenSearchTable.Rows(0).Item("torikesi_txt")).Trim
                End If
            Else
                tbxKameitenMei.Text = ""
                Me.tbxTorikesi.Text = String.Empty
                Me.hidTorikesi.Value = String.Empty
                '============2012/03/29 車龍 405721案件の対応 追加↑=========================
            End If
        Else
            tbxKameitenMei.Text = ""
            '============2012/03/29 車龍 405721案件の対応 追加↓=========================
            '取消
            Me.tbxTorikesi.Text = String.Empty
            Me.hidTorikesi.Value = String.Empty
            '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        End If

        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        '加盟店の色をセットする
        Call Me.SetColor()
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================

        If tbxKeiretuCd.Text <> "" Then

            Dim dtKeiretuTable As New Itis.Earth.DataAccess.CommonSearchDataSet.KeiretuTableDataTable
            dtKeiretuTable = CommonSearchLogic.GetKeiretuKensakuInfo("1", _
                                                                        Left(strUrl, Len(strUrl) - 1), _
                                                                        tbxKeiretuCd.Text, _
                                                                        "", _
                                                                        False)
            If dtKeiretuTable.Rows.Count = 1 Then
                tbxKeiretuMei.Text = TrimNull(dtKeiretuTable.Rows(0).Item("keiretu_mei"))
            End If
        Else
            tbxKeiretuMei.Text = ""
        End If
        If tbxEigyousyoCd.Text <> "" Then
            Dim dtEigyousyoTable As New Itis.Earth.DataAccess.CommonSearchDataSet.EigyousyoTableDataTable
            dtEigyousyoTable = CommonSearchLogic.GetEigyousyoInfo("1", tbxEigyousyoCd.Text, "", False)
            If dtEigyousyoTable.Rows.Count = 1 Then
                tbxEigyousyoMei.Text = TrimNull(dtEigyousyoTable.Rows(0).Item("eigyousyo_mei"))
            End If
        Else
            tbxEigyousyoMei.Text = ""
        End If

        If tbxTantouEigyouID.Text <> "" Then
            Dim dtBirudaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.BirudaTableDataTable
            dtBirudaTable = CommonSearchLogic.GetUserInfo("1", _
                                                                        tbxTantouEigyouID.Text, _
                                                                        "", _
                                                                        False)
            If dtBirudaTable.Rows.Count = 1 Then
                tbxTantouEigyouSyaMei.Text = TrimNull(dtBirudaTable.Rows(0).Item("mei"))
            End If
        Else
            tbxTantouEigyouSyaMei.Text = ""
        End If

        If strUrl.Trim <> "" Then
            strUrl = "kbn=" & Left(strUrl, Len(strUrl) - 1)
        Else
            strUrl = "kbn="
        End If

        If txtBangouF.Text <> "" Then
            strUrl = strUrl & "&Bangou=" & txtBangouF.Text
        End If
        If txtBangouT.Text <> "" Then
            strUrl = strUrl & "," & txtBangouT.Text
        ElseIf txtBangouF.Text <> "" Then
            strUrl = strUrl & ","
        End If

        If tbxIraiF.Text <> "" Then
            strUrl = strUrl & "&Irai=" & tbxIraiF.Text
        End If
        If tbxIraiT.Text <> "" Then
            strUrl = strUrl & "," & tbxIraiT.Text
        ElseIf tbxIraiF.Text <> "" Then
            strUrl = strUrl & ","

        End If

        If tbxKeikakusyoF.Text <> "" Then
            strUrl = strUrl & "&Keikakusyo=" & tbxKeikakusyoF.Text
        End If
        If tbxKeikakusyoT.Text <> "" Then
            strUrl = strUrl & "," & tbxKeikakusyoT.Text
        ElseIf tbxKeikakusyoF.Text <> "" Then
            strUrl = strUrl & ","

        End If

        If tbxYoteiF.Text <> "" Then
            strUrl = strUrl & "&TyousaYotei=" & tbxYoteiF.Text
        End If
        If tbxYoteiT.Text <> "" Then
            strUrl = strUrl & "," & tbxYoteiT.Text
        ElseIf tbxYoteiF.Text <> "" Then
            strUrl = strUrl & ","

        End If

        If tbxJissiF.Text <> "" Then
            strUrl = strUrl & "&TyousaJissi=" & tbxJissiF.Text
        End If
        If tbxJissiT.Text <> "" Then
            strUrl = strUrl & "," & tbxJissiT.Text
        ElseIf tbxJissiF.Text <> "" Then
            strUrl = strUrl & ","

        End If

        If tbxUriageF.Text <> "" Then
            strUrl = strUrl & "&TyousaUriage=" & tbxUriageF.Text
        End If
        If tbxUriageT.Text <> "" Then
            strUrl = strUrl & "," & tbxUriageT.Text
        ElseIf tbxUriageF.Text <> "" Then
            strUrl = strUrl & ","

        End If


        If tbxKYoteiF.Text <> "" Then
            strUrl = strUrl & "&KoujiYotei=" & tbxKYoteiF.Text

        End If
        If tbxKYoteiT.Text <> "" Then
            strUrl = strUrl & "," & tbxKYoteiT.Text
        ElseIf tbxKYoteiF.Text <> "" Then
            strUrl = strUrl & ","

        End If

        If tbxKJissiF.Text <> "" Then
            strUrl = strUrl & "&KoujiJissi=" & tbxKJissiF.Text
        End If
        If tbxKJissiT.Text <> "" Then
            strUrl = strUrl & "," & tbxKJissiT.Text
        ElseIf tbxKJissiF.Text <> "" Then
            strUrl = strUrl & ","

        End If

        If tbxKUriageF.Text <> "" Then
            strUrl = strUrl & "&KoujiUriage=" & tbxKUriageF.Text
        End If
        If tbxKUriageT.Text <> "" Then
            strUrl = strUrl & "," & tbxKUriageT.Text
        ElseIf tbxKUriageF.Text <> "" Then
            strUrl = strUrl & ","

        End If

        strUrl = strUrl & "&CHKKouji=" & chkKouji.Checked

        If tbxKameitenCd1.Text <> "" Then
            strUrl = strUrl & "&KameitenCd=" & tbxKameitenCd1.Text
        End If

        If Common_drop4.SelectedValue <> "" Then
            strUrlTMP = strUrlTMP & Common_drop4.SelectedValue & ","
        End If
        If Common_drop5.SelectedValue <> "" Then
            strUrlTMP = strUrlTMP & Common_drop5.SelectedValue & ","
        End If
        If Common_drop6.SelectedValue <> "" Then
            strUrlTMP = strUrlTMP & Common_drop6.SelectedValue & ","
        End If

        If strUrlTMP <> "" Then
            strUrl = strUrl & "&todoufuken=" & Left(strUrlTMP, Len(strUrlTMP) - 1)
        End If
        If tbxKeiretuCd.Text <> "" Then
            strUrl = strUrl & "&KeiretuCd=" & tbxKeiretuCd.Text
        End If

        If tbxEigyousyoCd.Text <> "" Then
            strUrl = strUrl & "&EigyousyoCd=" & tbxEigyousyoCd.Text
        End If

        If Me.ddlSosikiLevel.SelectedItem.Text = "0：ALL" Then

            strUrl = strUrl & "&BusyoCd=0000"
        Else
            If ddlSosikiLevel.SelectedValue <> "" Then
                strUrl = strUrl & "&SosikiLevel=" & ddlSosikiLevel.SelectedValue
            End If

            If ddlBusyoCd.SelectedValue <> "" Then
                strUrl = strUrl & "&BusyoCd=" & ddlBusyoCd.SelectedValue
            End If

        End If
        strUrl = strUrl & "&CHKBusyoCd=" & chkBusyoCd.Checked

        If tbxTantouEigyouID.Text <> "" Then
            strUrl = strUrl & "&TantouEigyouID=" & tbxTantouEigyouID.Text
        End If

        If ddlSearchCount.SelectedValue <> "" Then
            strUrl = strUrl & "&objKennsuu=" & hidkennsuu.ClientID
        End If
        hidkennsuu.Value = ddlSearchCount.SelectedValue
        ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>window.open('" & "BukkenJyouhouList.aspx?" & strUrl & "','');</script>")

    End Sub
    Function CheckInput(ByRef strObjId As String, ByRef blnDrop As Boolean) As String
        Dim csScript As New StringBuilder
        Dim strHan As String = ""

        With csScript
            If txtBangouF.Text <> "" Then
                .Append(commonCheck.CheckHankaku(Me.txtBangouF.Text, "番号(From)", "1"))
                .Append(commonCheck.CheckByte(txtBangouF.Text, 10, "番号(From)", kbn.HANKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.txtBangouF.ClientID
                End If
            End If
            strHan = strHan & txtBangouF.Text
            If txtBangouT.Text <> "" Then
                .Append(commonCheck.CheckHankaku(Me.txtBangouT.Text, "番号(To)", "1"))
                .Append(commonCheck.CheckByte(txtBangouT.Text, 10, "番号(To)", kbn.HANKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.txtBangouT.ClientID
                End If
            End If
            If txtBangouF.Text > txtBangouT.Text And txtBangouT.Text <> "" Then
                .Append(String.Format(Messages.Instance.MSG2012E, "番号", "番号").ToString)
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then

                    strObjId = Me.txtBangouF.ClientID
                End If

            End If
            If txtBangouF.Text = "" And txtBangouT.Text <> "" Then
                .Append(String.Format(Messages.Instance.MSG2012E, "番号", "番号").ToString)
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.txtBangouF.ClientID
                End If
            End If
            strHan = strHan & txtBangouT.Text

            .Append(CheckDate(tbxIraiF, tbxIraiT, "依頼日", strObjId))
            strHan = strHan & tbxIraiF.Text & tbxIraiT.Text

            .Append(CheckDate(tbxKeikakusyoF, tbxKeikakusyoT, "計画書作成日", strObjId))
            strHan = strHan & tbxKeikakusyoF.Text & tbxKeikakusyoT.Text

            .Append(CheckDate(tbxYoteiF, tbxYoteiT, "調査予定日", strObjId))
            strHan = strHan & tbxYoteiF.Text & tbxYoteiT.Text

            .Append(CheckDate(tbxJissiF, tbxJissiT, "調査実施日", strObjId))
            strHan = strHan & tbxJissiF.Text & tbxJissiT.Text

            .Append(CheckDate(tbxUriageF, tbxUriageT, "調査売上日", strObjId))
            strHan = strHan & tbxUriageF.Text & tbxUriageT.Text


            .Append(CheckDate(tbxKYoteiF, tbxKYoteiT, "工事予定日", strObjId))
            strHan = strHan & tbxKYoteiF.Text & tbxKYoteiT.Text

            .Append(CheckDate(tbxKJissiF, tbxKJissiT, "工事実施日", strObjId))
            strHan = strHan & tbxKJissiF.Text & tbxKJissiT.Text

            .Append(CheckDate(tbxKUriageF, tbxKUriageT, "工事売上日", strObjId))
            strHan = strHan & tbxKUriageF.Text & tbxKUriageT.Text
            If strHan = "" Then
                .Append(Messages.Instance.MSG2032E)
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.txtBangouF.ClientID
                End If

            End If

            '加盟店コード
            If Me.tbxKameitenCd1.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd1.Text, "加盟店コード"))
                .Append(commonCheck.CheckByte(tbxKameitenCd1.Text, 5, "加盟店コード", kbn.HANKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCd1.ClientID
                End If
            End If

            '系列コード
            If Me.tbxKeiretuCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text, "系列コード"))
                .Append(commonCheck.CheckByte(tbxKeiretuCd.Text, 5, "系列コード", kbn.HANKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKeiretuCd.ClientID
                End If
            End If

            '営業所コード
            If Me.tbxEigyousyoCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd.Text, "営業所コード"))
                .Append(commonCheck.CheckByte(tbxEigyousyoCd.Text, 5, "営業所コード", kbn.HANKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxEigyousyoCd.ClientID
                End If
            End If

            If ddlSosikiLevel.SelectedValue <> "" Then
                .Append(commonCheck.CheckHissuNyuuryoku(ddlBusyoCd.SelectedValue, "部署コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.ddlBusyoCd.ClientID
                    blnDrop = True
                End If
            End If
            '担当営業ID半角英数チェック
            If Me.tbxTantouEigyouID.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxTantouEigyouID.Text, "担当営業ID"))
                .Append(commonCheck.CheckByte(tbxTantouEigyouID.Text, 30, "担当営業ID", kbn.HANKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTantouEigyouID.ClientID
                End If
            End If

        End With
        Return csScript.ToString
    End Function

    Function CheckDate(ByVal strObjNengetu1 As TextBox, ByVal strObjNengetu2 As TextBox, ByVal strNengetu As String, ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '登録年月(From)
            If strObjNengetu1.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(strObjNengetu1.Text, strNengetu & "(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = strObjNengetu1.ClientID
                End If
            End If
            '登録年月(To)
            If strObjNengetu2.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(strObjNengetu2.Text, strNengetu & "(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = strObjNengetu2.ClientID
                End If
            End If
            '登録年月範囲
            If strObjNengetu1.Text <> String.Empty And strObjNengetu2.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(strObjNengetu1.Text, strNengetu & "(From)") = String.Empty _
                   And commonCheck.CheckYuukouHiduke(strObjNengetu2.Text, strNengetu & "(To)") = String.Empty Then
                    .Append(commonCheck.CheckHidukeHani(strObjNengetu1.Text, strObjNengetu2.Text, strNengetu))
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = strObjNengetu1.ClientID
                    End If
                End If
            End If
            If strObjNengetu1.Text = String.Empty And strObjNengetu2.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(strObjNengetu2.Text, strNengetu & "(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, strNengetu, strNengetu).ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = strObjNengetu1.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function
    ''' <summary>メッセージ表示</summary>
    ''' <param name="strMessage">メッセージ</param>
    ''' <param name="strObjId">クライアントID</param>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String, ByVal blnDrop As Boolean)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("   fncSetKubunVal();")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
          
                If blnDrop Then
                    .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                Else
                    .AppendLine("   document.getElementById('" & strObjId & "').select();")
                End If
            End If
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub
    ''' <summary>
    ''' CLOSE Div
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub closecover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "closecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "closecover1", _
                                        csScript.ToString, _
                                        True)
    End Sub

    ''' <summary>
    ''' 色を変更する
    ''' </summary>
    ''' <history>2012/03/27 車龍 405721案件の対応 追加</history>
    Private Sub SetColor()

        Dim strTorikesi As String
        strTorikesi = Me.hidTorikesi.Value.Trim

        If strTorikesi.Equals("0") OrElse strTorikesi.Equals(String.Empty) Then
            Me.tbxKameitenCd1.ForeColor = Drawing.Color.Black
            Me.tbxKameitenMei.ForeColor = Drawing.Color.Black
            Me.tbxTorikesi.ForeColor = Drawing.Color.Black
        Else
            Me.tbxKameitenCd1.ForeColor = Drawing.Color.Red
            Me.tbxKameitenMei.ForeColor = Drawing.Color.Red
            Me.tbxTorikesi.ForeColor = Drawing.Color.Red
        End If

    End Sub

End Class
