Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Partial Public Class WaribikiMasterSearch
    Inherits System.Web.UI.Page
#Region "*** [Private]Common Variable Definition"

    Private WaribikiBL As New Itis.Earth.BizLogic.WaribikiMasterLogic
    Private commonCheck As New CommonCheck
    Private ninsyou As New Ninsyou
    Protected scrollHeight As Integer = 0
    Protected recordCont As Integer = 0
    Protected dtWaribikiBL As New DataTable
    '加盟店コードFROM
    Private Const mconKameitenCdFrom As String = "KameitenCdFrom"
    '加盟店コードTo
    Private Const mconKameitenCdTo As String = "KameitenCdTo"
    '加盟店名
    Private Const mconKameitenMei As String = "KameitenMei"
    '加盟店カナ
    Private Const mconKameitenKana As String = "KameitenKana"
    '系列コード
    Private Const mconKeiretuCd As String = "KeiretuCd"
    '商品コード
    Private Const mconSyouhin As String = "Syouhin"
    '検索件数
    Private Const mconSearchCount As String = "SearchCount"

#End Region
    ''' <summary>
    ''' フォームロード時
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim user_info As New LoginUserInfo

        Dim jBn As New Jiban '地盤画面共通クラス
        ' ユーザー基本認証
        jBn.userAuth(user_info)
        If ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        If user_info Is Nothing Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

        'javascript 作成
        MakeJavascript()
        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            ViewState("UserId") = ninsyou.GetUserID()
            commonCheck.SetURL(Me, ViewState("UserId"))
        Else
            CloseCover()
        End If

        ViewState(mconKameitenCdFrom) = Me.tbxKameitenCdFrom.Text
        ViewState(mconKameitenCdTo) = Me.tbxKameitenCdTo.Text
        ViewState(mconKameitenMei) = Me.tbxKameitenMei.Text
        ViewState(mconKameitenKana) = Me.tbxKameitenKana.Text
        ViewState(mconKeiretuCd) = Me.tbxKeiretuCd.Text
        ViewState(mconSyouhin) = Me.tbxSyouhin.Text
        ViewState(mconSearchCount) = Me.ddlSearchCount.SelectedValue

        SetStyle(recordCont)
        '大文字変換
        Me.btnKensaku.Attributes.Add("onClick", "if(fncNyuuryokuCheck()==true){fncSyouhinMei();fncShowModal();}else{return false}")
        Me.tbxKameitenCdFrom.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxKameitenCdTo.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxKeiretuCd.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxSyouhin.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxKameitenKana.Attributes.Add("onBlur", "fncTokomozi(this);")
    End Sub
    ''' <summary>
    ''' 検索実行ボタンの処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub btnKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        '入力内容チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId).ToString
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '表示最大件数
        Dim intCount As Integer

        intCount = WaribikiBL.GetWaribikiCount(Convert.ToString(ViewState(mconKameitenCdFrom)), _
                                               Convert.ToString(ViewState(mconKameitenCdTo)), _
                                               Convert.ToString(ViewState(mconKameitenMei)), _
                                               Convert.ToString(ViewState(mconKameitenKana)), _
                                               Convert.ToString(ViewState(mconKeiretuCd)), _
                                               Convert.ToString(ViewState(mconSyouhin)))

        If Me.ddlSearchCount.SelectedValue = "max" Then
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
            SetStyle(intCount)
        Else
            If intCount > Me.ddlSearchCount.SelectedValue Then
                Me.lblCount.Text = Me.ddlSearchCount.SelectedValue & " / " & intCount
                Me.lblCount.ForeColor = Drawing.Color.Red
                SetStyle(Me.ddlSearchCount.SelectedValue)
            Else
                Me.lblCount.Text = intCount
                Me.lblCount.ForeColor = Drawing.Color.Black
                SetStyle(intCount)
            End If
        End If

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
        '系列名を設置。
        If Me.tbxKeiretuCd.Text <> String.Empty Then
            Dim dtKeiretuTable As New CommonSearchDataSet.KeiretuTableDataTable
            dtKeiretuTable = CommonSearchLogic.GetKeiretuKensakuInfo(1, _
                                                                    "", _
                                                                    ViewState(mconKeiretuCd), _
                                                                    "", _
                                                                    True)
            If dtKeiretuTable.Rows.Count > 0 Then
                Me.tbxKeiretuMei.Text = dtKeiretuTable.Rows(0).Item("keiretu_mei").ToString
            Else
                Me.tbxKeiretuMei.Text = String.Empty
            End If
        Else
            Me.tbxKeiretuMei.Text = String.Empty
        End If

        '商品名を設置。
        If Me.tbxSyouhin.Text <> String.Empty Then
            Dim dtSyouhinTable As New CommonSearchDataSet.SyouhinTableDataTable
            dtSyouhinTable = CommonSearchLogic.GetSyouhinInfo(1, _
                                                              ViewState(mconSyouhin), _
                                                              "", _
                                                              115)
            If dtSyouhinTable.Rows.Count > 0 Then
                Me.tbxSyouhinMei.Text = dtSyouhinTable.Rows(0).Item("syouhin_mei").ToString
            Else
                Me.tbxSyouhinMei.Text = String.Empty
            End If
        Else
            Me.tbxSyouhinMei.Text = String.Empty
        End If

        ' 検索結果ゼロ件の場合、メッセージを表示
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
        End If

        '結果を画面に表示
        grdDataBind(Convert.ToString(ViewState(mconKameitenCdFrom)), _
                    Convert.ToString(ViewState(mconKameitenCdTo)), _
                    Convert.ToString(ViewState(mconKameitenMei)), _
                    Convert.ToString(ViewState(mconKameitenKana)), _
                    Convert.ToString(ViewState(mconKeiretuCd)), _
                    Convert.ToString(ViewState(mconSyouhin)), _
                    Convert.ToString(ViewState(mconSearchCount)))

    End Sub
    ''' <summary>
    ''' gridViewのスタイルを設定
    ''' </summary>
    ''' <param name="recordCont">データ件数</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Public Sub SetStyle(ByVal recordCont As Integer)

        scrollHeight = (recordCont + 1) * 21 + 2

        If scrollHeight < 253 Then
            Me.divBodyLeft.Style.Add("height", "253px")
            Me.divBodyRight.Style.Add("height", "253px")
            Me.divHiddenMeisaiV.Style.Add("display", "none")
        Else
            Me.divHiddenMeisaiV.Style.Add("display", "block")
            Me.divBodyLeft.Style.Add("height", "253px")
            Me.divBodyRight.Style.Add("height", "253px")
        End If

        '画面仕様設定
        Me.divHiddenMeisaiH.Attributes.Add("onscroll", "fncScrollH();")
        Me.divHiddenMeisaiV.Attributes.Add("onscroll", "fncScrollV();")
        Me.grdBodyLeft.Attributes.Add("onmousewheel", "wheel();")
        Me.grdBodyRight.Attributes.Add("onmousewheel", "wheel();")
    End Sub
    ''' <summary>
    ''' gridViewを作成する
    ''' </summary>
    ''' <param name="strKameitenCdFrom">加盟店コード（FROM）</param>
    ''' <param name="strKameitenCdTo">加盟店コード（TO）</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="strKameitenKana">加盟店カナ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhin">商品コード</param>
    ''' <param name="strSearchCount">検索件数</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub grdDataBind(ByVal strKameitenCdFrom As String, _
                            ByVal strKameitenCdTo As String, _
                            ByVal strKameitenMei As String, _
                            ByVal strKameitenKana As String, _
                            ByVal strKeiretuCd As String, _
                            ByVal strSyouhin As String, _
                            ByVal strSearchCount As String)

        dtWaribikiBL = WaribikiBL.GetWaribiki(Convert.ToString(ViewState(mconKameitenCdFrom)), _
                             Convert.ToString(ViewState(mconKameitenCdTo)), _
                             Convert.ToString(ViewState(mconKameitenMei)), _
                             Convert.ToString(ViewState(mconKameitenKana)), _
                             Convert.ToString(ViewState(mconKeiretuCd)), _
                             Convert.ToString(ViewState(mconSyouhin)), _
                             Convert.ToString(ViewState(mconSearchCount)))

        Me.grdBodyLeft.DataSource = dtWaribikiBL
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtWaribikiBL
        Me.grdBodyRight.DataBind()
    End Sub
    ''' <summary>
    ''' 入力内容チェック
    ''' </summary>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckInput(ByRef strObjId As String) As StringBuilder
        Dim csScript As New StringBuilder
        With csScript
            '加盟店コード(From)
            If Me.tbxKameitenCdFrom.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdFrom.Text, "加盟店コード(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCdFrom.ClientID
                End If
            End If
            '加盟店コード(From)
            If Me.tbxKameitenCdFrom.Text = String.Empty And Me.tbxKameitenCdTo.Text <> String.Empty Then
                .Append(String.Format(Messages.Instance.MSG2016E).ToString)
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCdFrom.ClientID
                End If
            End If
            '加盟店コード(To)
            If Me.tbxKameitenCdFrom.Text <> String.Empty And Me.tbxKameitenCdTo.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdTo.Text, "加盟店コード(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCdTo.ClientID
                End If
                '加盟店コード範囲
                If commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdFrom.Text, "加盟店コード(From)") = String.Empty _
                And commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdTo.Text, "加盟店コード(To)") = String.Empty Then
                    If Me.tbxKameitenCdFrom.Text > Me.tbxKameitenCdTo.Text Then
                        .Append(String.Format(Messages.Instance.MSG2012E, "加盟店コード", "加盟店コード").ToString)
                        If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                            strObjId = Me.tbxKameitenCdFrom.ClientID
                        End If
                    End If
                End If
            End If
            '加盟店名
            If Me.tbxKameitenMei.Text <> String.Empty Then
                .Append(commonCheck.CheckKinsoku(Me.tbxKameitenMei.Text, "加盟店名"))
                .Append(commonCheck.CheckByte(Me.tbxKameitenMei.Text, "40", "加盟店名", kbn.ZENKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenMei.ClientID
                End If
            End If
            '加盟店名カナ
            If Me.tbxKameitenKana.Text.Replace("%", String.Empty) <> String.Empty Then
                .Append(commonCheck.CheckKatakana(Me.tbxKameitenKana.Text.Replace("%", String.Empty), "加盟店カナ", True))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenKana.ClientID
                End If
            End If
            '系列コード
            If Me.tbxKeiretuCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text, "系列コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKeiretuCd.ClientID
                End If
            End If
            '商品コード
            If Me.tbxSyouhin.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxSyouhin.Text, "商品コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSyouhin.ClientID
                End If
            End If
        End With
        Return csScript
    End Function
    ''' <summary>
    ''' javascript 作成
    ''' </summary>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub MakeJavascript()

        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript

            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("   function fncKameitenSearch(strKbn){")
            .AppendLine("       var strkbn='加盟店'")
            .AppendLine("       var strClientID ")
            .AppendLine("       if(strKbn=='1'){")
            .AppendLine("           strClientID = '" & Me.tbxKameitenCdFrom.ClientID & "'")
            .AppendLine("       }else{")
            .AppendLine("           strClientID = '" & Me.tbxKameitenCdTo.ClientID & "'")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientID+'&strCd='+escape(eval('document.all.'+strClientID).value),")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            .AppendLine("   function fncKeiretuSearch(){")
            .AppendLine("       var strkbn='系列'")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxKeiretuCd.ClientID & "&objMei=" & Me.tbxKeiretuMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxKeiretuCd.ClientID & "').value)+")
            .AppendLine("       '&strMei='+escape(eval('document.all.'+'" & Me.tbxKeiretuMei.ClientID & "').value),")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            .AppendLine("   function fncSyouhinSearch(strKbn){")
            .AppendLine("       var strkbn='商品'")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxSyouhin.ClientID & "&objMei=" & Me.tbxSyouhinMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxSyouhin.ClientID & "').value)+")
            .AppendLine("       '&strMei='+escape(eval('document.all.'+'" & Me.tbxSyouhinMei.ClientID & "').value),")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            .AppendLine("var activeRow=null;" & vbCrLf)
            .AppendLine("var defaultColor='';" & vbCrLf)
            .AppendLine("var motoRow=-1;" & vbCrLf)
            .AppendLine("function onListSelected(obj,rowNo){" & vbCrLf)
            .AppendLine("     if(!activeRow){" & vbCrLf)
            .AppendLine("         activeRow=obj;" & vbCrLf)
            .AppendLine("  defaultColor=activeRow.style.backgroundColor;" & vbCrLf)
            .AppendLine("  objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("  objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("  motoRow = rowNo;")
            .AppendLine("     }" & vbCrLf)
            .AppendLine("    else{" & vbCrLf)
            .AppendLine("  objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor=defaultColor;")
            .AppendLine("  objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor=defaultColor;")
            .AppendLine("  activeRow=obj;" & vbCrLf)
            .AppendLine("  defaultColor=activeRow.style.backgroundColor;" & vbCrLf)
            .AppendLine("  objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("  objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("  motoRow = rowNo;")
            .AppendLine(" }   ")
            .AppendLine(" }" & vbCrLf)
            .AppendLine("function showDetail(obj,rowNo){" & vbCrLf)
            .AppendLine("var kameitenCd = objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].childNodes[0].innerText;")
            .AppendLine("objEBI('" + Me.HidKameitenCd.ClientID + "').value =  kameitenCd;")
            .AppendLine("       objSrchWin = window.open('KihonJyouhouInquiry.aspx?&strKameitenCd='+escape(eval('document.all.'+'" & Me.HidKameitenCd.ClientID & "').value),")
            .AppendLine("       'popup');")
            .AppendLine("}" & vbCrLf)
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
            .AppendLine("       document.getElementById('" & Me.ddlSearchCount.ClientID & "').selectedIndex = 0;")
            .AppendLine("   }")
            .AppendLine("   function fncSyouhinMei(){")
            .AppendLine("       document.getElementById('" & Me.HidSyouhinMei.ClientID & "').value = document.getElementById('" & Me.tbxSyouhinMei.ClientID & "').value;")
            .AppendLine("       document.getElementById('" & Me.HidKeiretuMei.ClientID & "').value = document.getElementById('" & Me.tbxKeiretuMei.ClientID & "').value;")
            .AppendLine("   }")

            .AppendLine("var objWin = window;")
            .AppendLine("objWin.name = 'earthMainWindow'")
            .AppendLine("initPage();")
            .AppendLine("var activeRow=null;")
            .AppendLine("var defaultColor='';")
            .AppendLine("var motoRow=-1;")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")

            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("}")

            .AppendLine("function wheel(event){")
            .AppendLine("   var delta = 0;")
            .AppendLine("   if(!event)")
            .AppendLine("       event = window.event;")
            .AppendLine("   if (event.wheelDelta){")
            .AppendLine("       delta = event.wheelDelta/120;")
            .AppendLine("       if (window.opera)")
            .AppendLine("           delta = -delta;")
            .AppendLine("   } else if(event.detail){")
            .AppendLine("       delta = -event.detail/3;")
            .AppendLine("   }")
            .AppendLine("   if (delta)")
            .AppendLine("      handle(delta);")
            .AppendLine("}")

            .AppendLine("function handle(delta){")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   if (delta < 0){")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("   }else{")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("   function fncNyuuryokuCheck(){")
            .AppendLine("       if( document.getElementById('" & Me.ddlSearchCount.ClientID & "').value=='max'){")
            .AppendLine("           if (confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("               document.forms[0].submit();")
            .AppendLine("           }else{")
            .AppendLine("               return false;")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("   function fncShowModal(){")
            .AppendLine("      var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("      var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("      if(buyDiv.style.display=='none')")
            .AppendLine("      {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("      }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("      }")
            .AppendLine("   }")
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")

            .AppendLine("</script>")

        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub
    ''' <summary>
    ''' grdBodyLeftの処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound
        e.Row.Attributes.Add("onclick", "onListSelected(this," & e.Row.RowIndex & ");")

        e.Row.Attributes.Add("ondblclick", "showDetail(this," & e.Row.RowIndex & ");")
    End Sub
    ''' <summary>
    ''' grdBodyRightの処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyRight.RowDataBound
        e.Row.Attributes.Add("onclick", "onListSelected(this," & e.Row.RowIndex & ");")

        e.Row.Attributes.Add("ondblclick", "showDetail(this," & e.Row.RowIndex & ");")
    End Sub
    ''' <summary>DIV表示</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub
    ''' <summary>
    ''' 検索条件によって、CSV出力
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub btnCsvOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOut.Click

        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId).ToString
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        'データ取得
        dtWaribikiBL = WaribikiBL.GetWaribiki(Convert.ToString(ViewState(mconKameitenCdFrom)), _
                             Convert.ToString(ViewState(mconKameitenCdTo)), _
                             Convert.ToString(ViewState(mconKameitenMei)), _
                             Convert.ToString(ViewState(mconKameitenKana)), _
                             Convert.ToString(ViewState(mconKeiretuCd)), _
                             Convert.ToString(ViewState(mconSyouhin)), _
                             "")

        '＊＊＊＊＊＊＊＊＊＊＊初期値設定＊＊＊＊＊＊＊＊＊＊

        ' 検索結果ゼロ件の場合、メッセージを表示
        If dtWaribikiBL.Rows.Count = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        End If

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        Dim sbTitle As String = "TatouWaribikiMst.csv"

        'CSVファイルのヘッダー部分を生成する
        writer.WriteLine("区分,加盟店ｺｰﾄﾞ,加盟店名,棟1-商品ｺｰﾄﾞ,棟1-商品名,棟2-商品ｺｰﾄﾞ,棟2-商品名,棟3-商品ｺｰﾄﾞ,棟3-商品名")

        '＊＊＊＊＊＊＊＊＊＊＊CSV生成＊＊＊＊＊＊＊＊＊＊＊
        'CSVファイル内容設定
        For intRow As Integer = 0 To dtWaribikiBL.Rows.Count - 1
            writer.WriteLine(dtWaribikiBL.Rows(intRow).Item(0).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(1).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(2).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(3).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(4).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(5).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(6).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(7).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(8).ToString)
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(sbTitle))
        Response.End()

    End Sub
    ''' <summary>
    ''' CSV全件出力
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub btnZenkenCSV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZenkenCSV.Click

        'データ取得
        dtWaribikiBL = WaribikiBL.GetWaribiki("", "", "", "", "", "", "")
        '＊＊＊＊＊＊＊＊＊＊＊初期値設定＊＊＊＊＊＊＊＊＊＊

        ' 検索結果ゼロ件の場合、メッセージを表示
        If dtWaribikiBL.Rows.Count = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        End If

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            Dim sbTitle As String = "TatouWaribikiMst.csv"

            'CSVファイルのヘッダー部分を生成する
            writer.WriteLine("区分,加盟店ｺｰﾄﾞ,加盟店名,棟1-商品ｺｰﾄﾞ,棟1-商品名,棟2-商品ｺｰﾄﾞ,棟2-商品名,棟3-商品ｺｰﾄﾞ,棟3-商品名")

            '＊＊＊＊＊＊＊＊＊＊＊CSV生成＊＊＊＊＊＊＊＊＊＊＊
            'CSVファイル内容設定
        For intRow As Integer = 0 To dtWaribikiBL.Rows.Count - 1
            writer.WriteLine(dtWaribikiBL.Rows(intRow).Item(0).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(1).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(2).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(3).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(4).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(5).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(6).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(7).ToString, _
                            dtWaribikiBL.Rows(intRow).Item(8).ToString)
        Next

            'CSVファイルダウンロード
            Response.Charset = "utf-8"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(sbTitle))
            Response.End()
    End Sub
    ''' <summary>
    ''' エラーメッセージ表示
    ''' </summary>
    ''' <param name="strMessage">エラーメッセージ</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("   document.getElementById('" & strObjId & "').select();")
            End If
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub
    ''' <summary>
    ''' 戻るボタンの処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub
End Class