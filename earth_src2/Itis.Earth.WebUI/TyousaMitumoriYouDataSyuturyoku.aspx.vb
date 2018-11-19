Imports Itis.Earth.BizLogic

Partial Public Class TyousaMitumoriYouDataSyuturyoku
    Inherits System.Web.UI.Page

    'インスタンス生成
    Private TyousaMitumoriYouDataSyuturyokuBL As New TyousaMitumoriYouDataSyuturyokuLogic
    '共通チェック
    Private commoncheck As New CommonCheck
    'ログインユーザーを取得する。
    Private Ninsyou As New Ninsyou()

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '基本認証
        If Ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        ViewState("UserId") = Ninsyou.GetUserID()

        'JavaScript
        MakeJavascript()

        If Not IsPostBack Then
            Me.divMeisai.Attributes.Add("style", "height: 220px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:933px;")

            hidCsvFlg.Value = ""

            If Not IsNothing(Request("sendSearchTerms")) Then
                Me.ddlKbn.SelectedValue = Split(Request("sendSearchTerms"), "$$$")(0)
                Me.tbxBangou1.Text = Split(Request("sendSearchTerms"), "$$$")(1)
                Me.tbxBangou2.Text = Split(Request("sendSearchTerms"), "$$$")(1)
                If Not Me.tbxBangou1.Text.Equals(String.Empty) Then
                    '検索
                    Call Me.btnKensaku_Click(sender, e)
                End If
            End If

        Else
            CloseCover()
        End If

        '加盟店
        tbxKameiTenMei.Attributes.Add("readonly", "true;")

        '==================2017/12/26 徐 条件を追加する 追加↓====================================
        '系列コード
        tbxKeiretuMei.Attributes.Add("readonly", "true;")
        '==================2017/12/26 徐 条件を追加する 追加↑====================================

        '==================2012/03/28 車龍 405821案件の対応 追加↓====================================
        '取消
        Me.tbxTorikesi.Attributes.Add("readonly", "true;")
        '加盟店の色をセットする
        Call Me.SetColor()
        '==================2012/03/28 車龍 405821案件の対応 追加↑====================================

        ''番号コピー
        'tbxBangou1.Attributes.Add("onblur", "SetOnaji(this);")
        '検索実行ボタン
        btnKensaku.Attributes.Add("onclick", "if(document.getElementById('" & Me.ddlSearchCount.ClientID & "').value=='max'){if(!confirm('検索上限件数に「無制限」が選択されています。\n画面表示に時間が掛かる可能性がありますが、実行してよろしいですか？')){return false;}else{fncShowModal();}}else{fncShowModal();};")
        'CSV出力ボタン
        btnCsvData.Attributes.Add("onclick", "if(document.getElementById('" & Me.hidCsvFlg.ClientID & "').value==''||document.getElementById('" & Me.hidCsvFlg.ClientID & "').value==0){alert('出力対象データを選択して下さい。');return false;}else{if(document.getElementById('" & Me.rbnFlg1.ClientID & "').checked==true){if(document.getElementById('" & Me.hidCsvFlg.ClientID & "').value>100){alert('対象件数が100件を超えています。\n対象を絞って、再度実行してください。');return false;}}else{if(document.getElementById('" & Me.hidCsvFlg.ClientID & "').value>3000){alert('対象件数が3千件を超えています。\n対象を絞って、再度実行してください。');return false;}}};")
        'すべて件選択
        chkAll.Attributes.Add("onclick", "fncShowModal();")
        'EXCELファイルDownLoad
        btnExcelDownLoad.Attributes.Add("onclick", "javascript:window.location.href='./Files/調査見積書作成用.lha';return false;")

        '==================2015/09/17 案件「430011」の対応 追加↓====================================
        '東建様用のEXCELファイルDownLoad
        btnToukenDownLoad.Attributes.Add("onclick", "javascript:window.location.href='./Files/東建様用_調査見積書作成用.lha';return false;")
        '==================2015/09/17 案件「430011」の対応 追加↑====================================
        
    End Sub

    '東西（東西ラジオボタンに入力有りの場合）
    Private Function GetTouZaiKbn() As String
        If Me.rbnTyou.Checked Then
            Return "0"
        ElseIf Me.rbnSei.Checked Then
            Return "1"
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' CSVデータ出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCsvData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvData.Click

        '見積書作成
        Dim strMitumoriFlg As String = String.Empty
        If Me.hidMitumori.Value = "1" Then
            strMitumoriFlg = "1"
        End If

        'CSV出力
        Dim dtCsvTable As New Data.DataTable
        If Not TyousaMitumoriYouDataSyuturyokuBL.SetCsvData(Response, Me.grdItiran, strMitumoriFlg, Me.rbnFlg1.Checked, Me.hidCsvFlg.Value, ViewState("UserId"), dtCsvTable, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn()) Then
            Response.Clear()
            'エラーが有る場合
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('システムエラーが発生しました、管理者に連絡してください。');", True)
        Else
            If dtCsvTable.Rows.Count > 0 Then

                'CSVデータ
                ViewState.Item("dtCsvTable") = dtCsvTable
                'チェック対象が無い
                Me.hidCsvFlg.Value = String.Empty

                'GridViewの値を再検索
                Dim dtTyousaMitumori As New Data.DataTable
                dtTyousaMitumori = TyousaMitumoriYouDataSyuturyokuBL.GetTyousaMitumoriInfo(ddlKbn.SelectedValue, tbxBangou1.Text.Trim, tbxBangou2.Text.Trim, tbxKameiTenCd.Text.Trim, strMitumoriFlg, ddlSearchCount.SelectedValue, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn())

                '総件数の取得
                Dim intCount As Int64
                intCount = TyousaMitumoriYouDataSyuturyokuBL.GetTyousaMitumoriCount(ddlKbn.SelectedValue, tbxBangou1.Text.Trim, tbxBangou2.Text.Trim, tbxKameiTenCd.Text.Trim, strMitumoriFlg, ddlSearchCount.SelectedValue, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn())
                If Me.ddlSearchCount.SelectedValue = "max" Then
                    Me.lblCount.Text = intCount
                    Me.lblCount.ForeColor = Drawing.Color.Black
                Else
                    If intCount > Me.ddlSearchCount.SelectedValue Then
                        Me.lblCount.Text = Me.ddlSearchCount.SelectedValue & " / " & intCount
                        Me.lblCount.ForeColor = Drawing.Color.Red
                    Else
                        Me.lblCount.Text = dtTyousaMitumori.Rows.Count
                        Me.lblCount.ForeColor = Drawing.Color.Black
                    End If
                End If

                If dtTyousaMitumori.Rows.Count > 0 Then
                    Me.chkAll.Checked = False
                    'データが０件以外の場合
                    grdItiran.Visible = True
                    Me.grdItiran.DataSource = dtTyousaMitumori
                    Me.grdItiran.DataBind()
                    If dtTyousaMitumori.Rows.Count <= 10 Then
                        Me.divMeisai.Attributes.Add("style", "height: 221px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:933px;")
                        Me.grdItiran.Columns(0).ItemStyle.Width = 56
                        Me.grdItiran.Columns(3).ItemStyle.Width = 276
                        Me.grdItiran.Columns(6).ItemStyle.Width = 254
                    Else
                        Me.divMeisai.Attributes.Add("style", "height: 220px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:950px;")
                        Me.grdItiran.Columns(0).ItemStyle.Width = 57
                        Me.grdItiran.Columns(3).ItemStyle.Width = 275
                        Me.grdItiran.Columns(6).ItemStyle.Width = 254
                    End If
                Else
                    grdItiran.Visible = False
                End If

                '再検索
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "window.setTimeout('objEBI(\'" & Me.btnNO.ClientID & "\').click()',10); ", True)
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "alert('" & Messages.Instance.MSG039E & "');", True)
            End If
        End If
    End Sub


    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click

        'CSVファイル名設定
        Dim strFileName As String = "TyousaMitumorisyo.csv"

        Response.Buffer = True
        Dim writer As New CsvWriter(Response.OutputStream, System.Text.Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conTyousaMitumoriCsvHeader)


        '画面データのソート順を設定する
        Dim dtCsvTable As Data.DataView = CType(ViewState.Item("dtCsvTable"), Data.DataTable).DefaultView
        dtCsvTable.Sort = "kbn,hosyousyo_no,bunrui_cd,syouhin_cd" & " " & "ASC"
        Dim dtCsvTable1 As Data.DataTable = dtCsvTable.ToTable()

        If dtCsvTable1.Rows.Count > 0 Then
            For intRow1 As Integer = 0 To dtCsvTable1.Rows.Count - 1
                With dtCsvTable1.Rows(intRow1)
                    If String.IsNullOrEmpty(dtCsvTable1.Rows(intRow1).Item(5).ToString.Trim) Then
                        writer.WriteLine(.Item(0).ToString, _
                                         .Item(1).ToString, _
                                         .Item(2).ToString, _
                                         .Item(3).ToString, _
                                         .Item(4).ToString, _
                                         "ご担当者", _
                                         .Item(6).ToString, _
                                         .Item(7).ToString, _
                                         .Item(8).ToString, _
                                         .Item(9).ToString, _
                                         .Item(10).ToString, _
                                         .Item(11).ToString, _
                                         .Item(12).ToString, _
                                         .Item(13).ToString, _
                                         Fix(.Item(14)).ToString, _
                                         Fix(.Item(15)).ToString, _
                                         .Item(16).ToString)
                    Else
                        writer.WriteLine(.Item(0).ToString, _
                                         .Item(1).ToString, _
                                         .Item(2).ToString, _
                                         .Item(3).ToString, _
                                         .Item(4).ToString, _
                                         .Item(5).ToString, _
                                         .Item(6).ToString, _
                                         .Item(7).ToString, _
                                         .Item(8).ToString, _
                                         .Item(9).ToString, _
                                         .Item(10).ToString, _
                                         .Item(11).ToString, _
                                         .Item(12).ToString, _
                                         .Item(13).ToString, _
                                         Fix(.Item(14)).ToString, _
                                         Fix(.Item(15)).ToString, _
                                         .Item(16).ToString)
                    End If
                End With
            Next
        End If

        'CSVファイルダウンロード
        Response.Charset = "shift-jis"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))

        Response.End()
    End Sub

    ''' <summary>
    ''' CHECKBOX
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItiran_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItiran.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("chkTaisyou"), CheckBox).Attributes.Add("onclick", "ChkCsvFlg(this," & e.Row.RowIndex & ");")
        End If
    End Sub

    ''' <summary>
    ''' 検索実行ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        '入力チェック
        Dim strErr As String = ""
        Dim strID As String = ""
        strID = InputCheck(strErr)

        'エラーがある時
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();document.getElementById('" & strID & "').select();", True)
        Else
            '見積書作成
            Dim strMitumoriFlg As String = String.Empty
            If rbnMitumori2.Checked = True Then
                strMitumoriFlg = "1"
                Me.hidMitumori.Value = "1"
            Else
                Me.hidMitumori.Value = String.Empty
            End If

            '画面情報の取得
            Dim dtTyousaMitumori As New Data.DataTable
            dtTyousaMitumori = TyousaMitumoriYouDataSyuturyokuBL.GetTyousaMitumoriInfo(ddlKbn.SelectedValue, tbxBangou1.Text.Trim, tbxBangou2.Text.Trim, tbxKameiTenCd.Text.Trim, strMitumoriFlg, ddlSearchCount.SelectedValue, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn())

            '総件数の取得
            Dim intCount As Int64
            intCount = TyousaMitumoriYouDataSyuturyokuBL.GetTyousaMitumoriCount(ddlKbn.SelectedValue, tbxBangou1.Text.Trim, tbxBangou2.Text.Trim, tbxKameiTenCd.Text.Trim, strMitumoriFlg, ddlSearchCount.SelectedValue, Me.tbxSesyuMei.Text.Trim, tbxKeiretuCd.Text.Trim, GetTouZaiKbn())
            If Me.ddlSearchCount.SelectedValue = "max" Then
                Me.lblCount.Text = intCount
                Me.lblCount.ForeColor = Drawing.Color.Black
            Else
                If intCount > Me.ddlSearchCount.SelectedValue Then
                    Me.lblCount.Text = Me.ddlSearchCount.SelectedValue & " / " & intCount
                    Me.lblCount.ForeColor = Drawing.Color.Red
                Else
                    Me.lblCount.Text = dtTyousaMitumori.Rows.Count
                    Me.lblCount.ForeColor = Drawing.Color.Black
                End If
            End If

            If dtTyousaMitumori.Rows.Count > 0 Then

                Me.chkAll.Checked = False

                Me.grdItiran.Visible = True
                'データが０件以外の場合
                Me.grdItiran.DataSource = dtTyousaMitumori
                Me.grdItiran.DataBind()
                If dtTyousaMitumori.Rows.Count <= 10 Then
                    Me.divMeisai.Attributes.Add("style", "height: 221px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:933px;")
                    Me.grdItiran.Columns(0).ItemStyle.Width = 56
                    Me.grdItiran.Columns(3).ItemStyle.Width = 276
                    Me.grdItiran.Columns(6).ItemStyle.Width = 254
                Else
                    Me.divMeisai.Attributes.Add("style", "height: 220px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:950px;")
                    Me.grdItiran.Columns(0).ItemStyle.Width = 57
                    Me.grdItiran.Columns(3).ItemStyle.Width = 275
                    Me.grdItiran.Columns(6).ItemStyle.Width = 254
                End If
            Else
                'データが０件の場合
                Me.grdItiran.Visible = False
                strErr = "alert('" & Messages.Instance.MSG039E & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            End If

        End If
    End Sub

    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")

            '系列ポップアップ
            .AppendLine("   function fncKeiretuSearch(){")
            '.AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            '.AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            '.AppendLine("           objKbn1.focus();")
            '.AppendLine("           return false;")
            '.AppendLine("       }")
            .AppendLine("       var strkbn='系列';")
            .AppendLine("       var blnTaikai ;")
            .AppendLine("       var arrKubun = '';")
            '.AppendLine("       if(objKbn1.selectedIndex!=0){")
            '.AppendLine("           arrKubun = arrKubun + objKbn1.value + ',';")
            '.AppendLine("       }")
            '.AppendLine("       if(objKbn2.selectedIndex!=0){")
            '.AppendLine("           arrKubun = arrKubun + objKbn2.value + ',';")
            '.AppendLine("       }")
            '.AppendLine("       if(objKbn3.selectedIndex!=0){")
            '.AppendLine("           arrKubun = arrKubun + objKbn3.value + ',';")
            '.AppendLine("       }")
            .AppendLine("       arrKubun = arrKubun.substring(0,arrKubun.length-1);")
            .AppendLine("       if(document.getElementById('" & Me.tbxKameiTenCd.ClientID & "').value != ''){")
            .AppendLine("           blnTaikai = 'False' ")
            .AppendLine("       }else{")
            .AppendLine("           blnTaikai = 'True' ")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxKeiretuCd.ClientID & "&objMei=" & Me.tbxKeiretuMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxKeiretuCd.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun='+arrKubun+'&blnDelete='+blnTaikai, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")

            '加盟店ポップアップ
            .AppendLine("   function fncKameitenSearch(strKbn){")
            .AppendLine("       var strkbn='加盟店'")
            .AppendLine("       var strClientID ")
            .AppendLine("       strClientID = '" & Me.tbxKameiTenCd.ClientID & "'")
            .AppendLine("       var strClientID ")
            .AppendLine("       strClientMei = '" & Me.tbxKameiTenMei.ClientID & "'")
            .AppendLine("       var blnTaikai ")
            '============2012/03/29 車龍 405721案件の対応 修正↓=========================
            .AppendLine("       var strHidTorikesiCd; ")
            .AppendLine("       var strTxtTorikesiCd; ")
            '.AppendLine("       var blnTaikai='True' ")
            .AppendLine("       var blnTaikai='False'; ")
            .AppendLine("       strHidTorikesiCd = '" & Me.hidTorikesi.ClientID & "'")
            .AppendLine("       strTxtTorikesiCd = '" & Me.tbxTorikesi.ClientID & "'")
            '============2012/03/29 車龍 405721案件の対応 修正↑=========================
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientID+'&objMei='+strClientMei+'&strCd='+escape(eval('document.all.'+strClientID).value)+'&strMei='+escape(eval('document.all.'+strClientMei).value)+")
            .AppendLine("       '&blnDelete='+blnTaikai+'&HidTorikesiCd='+strHidTorikesiCd+'&TxtdTorikesiCd='+strTxtTorikesiCd+'&btnChangeColorCd=btnChangeColor', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")

            '番号(To)に同じ値をコピー
            .AppendLine("function SetOnaji(e)")
            .AppendLine("{")
            .AppendLine("   var objtbxBangou2 ")
            .AppendLine("   objtbxBangou2=document.getElementById('" & Me.tbxBangou2.ClientID & "');")
            .AppendLine("   if(e.value!=''){")
            .AppendLine("       objtbxBangou2.value = e.value;")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("   function fncClearWin(){")
            .AppendLine("       document.getElementById('" & Me.ddlKbn.DdlClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & Me.tbxKameiTenCd.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxKameiTenMei.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxBangou1.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxBangou2.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.rbnMitumori1.ClientID & "').checked = true;")
            .AppendLine("       document.getElementById('" & Me.ddlSearchCount.ClientID & "').selectedIndex = 0;")
            '============2012/03/29 車龍 405721案件の対応 追加↓=========================
            .AppendLine("       document.getElementById('" & Me.hidTorikesi.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxTorikesi.ClientID & "').value = '';")
            .AppendLine("       fncChangeColor();")
            '============2012/03/29 車龍 405721案件の対応 追加↑=========================

            .AppendLine("       document.getElementById('" & Me.tbxKeiretuCd.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxKeiretuMei.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.tbxSesyuMei.ClientID & "').value = '';")
            .AppendLine("       document.getElementById('" & Me.rbnTyou.ClientID & "').checked = false;")
            .AppendLine("       document.getElementById('" & Me.rbnSei.ClientID & "').checked = false;")



            .AppendLine("   }")

            '対象CHECK
            .AppendLine("function ChkCsvFlg(e,rowNo)")
            .AppendLine("{")
            .AppendLine("   var hidCsvFlg ")
            .AppendLine("   hidCsvFlg=document.getElementById('" & Me.hidCsvFlg.ClientID & "');")
            .AppendLine("   var intCsvFlg=hidCsvFlg.value;")
            .AppendLine("   if(e.checked==true){")
            .AppendLine("       intCsvFlg++;")
            .AppendLine("   }else{")
            .AppendLine("       intCsvFlg--;")
            .AppendLine("   }")
            .AppendLine("   hidCsvFlg.value=intCsvFlg;")
            .AppendLine("}")

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
            .AppendLine("	document.getElementById('" & Me.tbxKameiTenCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKameiTenMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxTorikesi.ClientID & "').style.color = strColor; ")
            .AppendLine("}")
            '============2012/03/29 車龍 405721案件の対応 追加↑=========================

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub

    ''' <summary>DIV表示</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <param name="strErr">エラーメッセージ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '加盟店
        If strErr = "" And tbxKameiTenCd.Text <> "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxKameiTenCd.Text, "加盟店コード")
            strID = tbxKameiTenCd.ClientID
        End If
        '番号
        If strErr = "" Then
            '入力必須
            If tbxBangou1.Text.Trim = "" And tbxBangou2.Text.Trim = "" Then
                strErr = Messages.Instance.MSG037E.Replace("@PARAM1", "物件番号")
            End If
            strID = tbxBangou1.ClientID
        End If
        '番号FROM
        If strErr = "" And tbxBangou1.Text <> "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxBangou1.Text, "物件番号")
            strID = tbxBangou1.ClientID
        End If
        '番号TO
        If strErr = "" And tbxBangou2.Text <> "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxBangou2.Text, "物件番号")
            strID = tbxBangou2.ClientID
        End If
        '番号
        If strErr = "" Then
            '入力必須
            If rbnMitumori1.Checked = False And rbnMitumori2.Checked = False Then
                strErr = Messages.Instance.MSG038E.Replace("@PARAM1", "見積書作成")
            End If
            strID = rbnMitumori1.ClientID
        End If

        Return strID

    End Function

    ''' <summary>
    ''' カーソル移動時、加盟店名取得
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbxKameiTenCd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxKameiTenCd.TextChanged
        Dim dtTable As Data.DataTable
        If Me.tbxKameiTenCd.Text.Trim <> String.Empty Then
            dtTable = TyousaMitumoriYouDataSyuturyokuBL.GetKameitenMei(Me.tbxKameiTenCd.Text.Trim)
            If dtTable.Rows.Count > 0 Then
                '加盟店名
                Me.tbxKameiTenMei.Text = dtTable.Rows(0).Item("kameiten_mei1").ToString.Trim
                '============2012/03/29 車龍 405721案件の対応 追加↓=========================
                '取消
                Me.hidTorikesi.Value = dtTable.Rows(0).Item("torikesi").ToString.Trim
                If dtTable.Rows(0).Item("torikesi").ToString.Trim.Equals("0") Then
                    Me.tbxTorikesi.Text = String.Empty
                Else
                    Me.tbxTorikesi.Text = dtTable.Rows(0).Item("torikesi").ToString.Trim & ":" & dtTable.Rows(0).Item("torikesi_txt").ToString.Trim
                End If
                '============2012/03/29 車龍 405721案件の対応 追加↑=========================
            Else
                Me.tbxKameiTenCd.Text = String.Empty
                Me.tbxKameiTenMei.Text = String.Empty
                '============2012/03/29 車龍 405721案件の対応 追加↓=========================
                '取消
                Me.tbxTorikesi.Text = String.Empty
                Me.hidTorikesi.Value = String.Empty
                '============2012/03/29 車龍 405721案件の対応 追加↑=========================
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG2034E & "');document.getElementById('" & Me.tbxKameiTenCd.ClientID & "').focus();", True)
            End If
        Else
            Me.tbxKameiTenMei.Text = String.Empty
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

    End Sub

    ''' <summary>
    ''' 番号カーソル移動時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbxBangou1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxBangou1.TextChanged


        If commoncheck.ChkHankakuEisuuji(Me.tbxBangou1.Text.Trim, "") <> "" And Me.tbxBangou1.Text.Trim <> String.Empty Then
            Me.tbxBangou1.Text = String.Empty
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('英数字以外が入力されています');document.getElementById('" & Me.tbxBangou1.ClientID & "').focus();", True)
        Else
            If Me.tbxBangou2.Text.Trim = String.Empty And Me.tbxBangou1.Text.Trim <> String.Empty Then
                Me.tbxBangou2.Text = Me.tbxBangou1.Text.Trim
            End If
        End If

    End Sub

    ''' <summary>
    ''' 番号カーソル移動時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbxBangou2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxBangou2.TextChanged
        If commoncheck.ChkHankakuEisuuji(Me.tbxBangou2.Text.Trim, "") <> "" And Me.tbxBangou2.Text.Trim <> String.Empty Then
            Me.tbxBangou2.Text = String.Empty
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('英数字以外が入力されています');document.getElementById('" & Me.tbxBangou2.ClientID & "').focus();", True)
        End If
    End Sub

    ''' <summary>
    ''' すべて選択時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Dim intCount As Int64 = 0
        intCount = Me.grdItiran.Rows.Count
        If intCount > 0 Then
            If Me.chkAll.Checked = True Then
                Me.hidCsvFlg.Value = intCount.ToString
                For i As Int64 = 0 To intCount - 1
                    CType(Me.grdItiran.Rows(i).Cells(0).FindControl("chkTaisyou"), CheckBox).Checked = True
                Next
            Else
                Me.hidCsvFlg.Value = 0
                For i As Int64 = 0 To intCount - 1
                    CType(Me.grdItiran.Rows(i).Cells(0).FindControl("chkTaisyou"), CheckBox).Checked = False
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' 色を変更する
    ''' </summary>
    ''' <history>2012/03/27 車龍 405721案件の対応 追加</history>
    Private Sub SetColor()

        Dim strTorikesi As String
        strTorikesi = Me.hidTorikesi.Value.Trim

        If strTorikesi.Equals("0") OrElse strTorikesi.Equals(String.Empty) Then
            Me.tbxKameiTenCd.ForeColor = Drawing.Color.Black
            Me.tbxKameitenMei.ForeColor = Drawing.Color.Black
            Me.tbxTorikesi.ForeColor = Drawing.Color.Black
        Else
            Me.tbxKameiTenCd.ForeColor = Drawing.Color.Red
            Me.tbxKameitenMei.ForeColor = Drawing.Color.Red
            Me.tbxTorikesi.ForeColor = Drawing.Color.Red
        End If

    End Sub

    Protected Sub tbxKeiretuCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class