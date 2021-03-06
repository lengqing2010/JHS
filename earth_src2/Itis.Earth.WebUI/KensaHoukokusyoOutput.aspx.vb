Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

''' <summary>
''' 検査報告書_各帳票出力画面
''' </summary>
''' <remarks></remarks>
''' <history>
''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
''' </history>
Partial Public Class KensaHoukokusyoOutput
    Inherits System.Web.UI.Page

#Region "パス"
    Public Const sDirName As String = "C:\JHS\earth\KensaHoukokusyo"
#End Region

#Region "定数"
    Private Const SEP_STRING As String = "$$$"
    Private Const OUTPUT_WAIT_TIME As Integer = 4000
#End Region

#Region "サービスパース"
    Public sv_templist As String = getfiledatetimelist("data\KensaHoukokusyo")
#End Region
    Private KensaLogic As New KensaHoukokusyoOutputLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

    Public gridviewId As String = ""
    Public tblRightId As String = ""
    Public tblLeftId As String = ""

    Private HoukokusyoCnt As Integer = 0

    ''' <summary>
    ''' ページロッド
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call Me.MakeJavascript()

        If Not IsPostBack Then
            'Call Me.Kensakuzikkou()

            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(False)

            Me.tbxSendDateFrom.Focus()
        End If
        '｢クリア｣ボタン処理
        Me.btnClear.Attributes.Add("onclick", "fncClear();return false;")
        '検索実行ボタン処理
        Me.btnKensakujiltukou.Attributes.Add("onclick", ";if(document.getElementById('" & Me.ddlKensakuJyouken.ClientID & "').value=='max'){if(!confirm('検索上限件数に「無制限」が選択されています。\n画面表示に時間が掛かる可能性がありますが、実行してよろしいですか？')){return false;}else{return true;}}else{return true;};")
        '発送日From
        Me.tbxSendDateFrom.Attributes.Add("onBlur", "checkDate(this);")
        '発送日To
        Me.tbxSendDateTo.Attributes.Add("onBlur", "checkDate(this);")
        '管理表EXCEL出力
        Me.btnKanriHyouExcelOutput.Attributes.Add("onClick", "if(fncCheckBusuu()==true){}else{return false}")
        '送付状PDF出力
        Me.btnSoufuJyouPdfOutput.Attributes.Add("onClick", "if(fncCheckBusuu()==true){}else{return false}")
        '報告書PDF出力
        Me.btnHoukokusyoPdfOutput.Attributes.Add("onClick", "if(fncCheckBusuu()==true){}else{return false}")
        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")
        '加盟店名
        Me.tbxKameitenMei.Attributes.Add("readOnly", "true")
        Me.tbxKameitenMei.Attributes.Add("onkeydown", "return false;")
        If Me.tbxKameitenCd.Text.Trim.Equals(String.Empty) Then
            Me.tbxKameitenMei.Text = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 検索実行ボタン処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>   
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history> 
    Protected Sub btnKensakujiltukou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakujiltukou.Click
        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        Call Kensakuzikkou()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Call Me.SetScrollBar()
    End Sub

    ''' <summary>
    ''' 検査報告書管理画面へ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub btnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click

        Server.Transfer("KensaHoukokusyoKanriSearchList.aspx")

    End Sub

    ''' <summary>
    ''' 管理表EXCEL出力ボタン処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub btnKanriHyouExcelOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKanriHyouExcelOutput.Click

        Dim strReturn As Boolean
        Dim strErr As String = ""
        Dim intCount As Integer = 0
        '画面データを取得する
        Dim dt As Data.DataTable = Me.GetData
        '【通しNo】の設定
        Me.SetTooshiNo(dt)

        If (dt.Rows.Count > 0) Then
            For i As Integer = 0 To dt.Rows.Count - 1
                For j As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("Bukukazu").ToString = dt.Rows(j)("Bukukazu").ToString Then
                        intCount = intCount + 1
                    End If
                Next
            Next
            If intCount > dt.Rows.Count Then
                strErr = "alert('" & Messages.Instance.MSG2083E & "');"
            Else
                'DB更新
                strReturn = KensaLogic.UpdKensahoukokusho(dt, "1")
                If strReturn Then
                    '更新成功
                    'excel出力
                    Dim lstKanriNo As New System.Collections.Generic.List(Of String)
                    For Each row As Data.DataRow In dt.Rows
                        lstKanriNo.Add(row.Item("kanri_no").ToString)
                    Next
                    Dim kanriNo As String = String.Join(SEP_STRING, lstKanriNo.ToArray)
                    Call Me.CheckXlt(kanriNo)
                    ClientScript.RegisterStartupScript(Page.GetType, "KanriHyouExcelOutput", "body_onLoad(""1"");", True)

                    Me.Kensakuzikkou()
                Else
                    '更新失敗
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "管理表EXCEL出力") & "');"
                End If
            End If
        Else
            strErr = "alert('" & Messages.Instance.MSG020E & "');"
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)

    End Sub

    ''' <summary>
    ''' 送付状PDF出力ボタン処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub btnSoufuJyouPdfOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSoufuJyouPdfOutput.Click
        Dim strReturn As Boolean
        Dim strErr As String = ""
        Dim intCount As Integer = 0
        '画面データを取得する
        Dim dt As Data.DataTable = Me.GetData
        '【通しNo】の設定
        Me.SetTooshiNo(dt)

        If (dt.Rows.Count > 0) Then
            For i As Integer = 0 To dt.Rows.Count - 1
                For j As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("Bukukazu").ToString = dt.Rows(j)("Bukukazu").ToString Then
                        intCount = intCount + 1
                    End If
                Next
            Next
            If intCount > dt.Rows.Count Then
                strErr = "alert('" & Messages.Instance.MSG2083E & "');"
            Else
                'DB更新
                strReturn = KensaLogic.UpdKensahoukokusho(dt, "2")

                If strReturn Then
                    'pdf出力
                    Dim lstKanriNo As New System.Collections.Generic.List(Of String)
                    For Each row As Data.DataRow In dt.Rows
                        lstKanriNo.Add(row.Item("kanri_no").ToString)
                    Next
                    Dim kanriNo As String = String.Join(SEP_STRING, lstKanriNo.ToArray)
                    Call Me.SoufujyouPdfOutput(kanriNo)

                    Me.Kensakuzikkou()
                Else
                    '更新失敗
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "送付状PDF出力") & "');"
                End If
            End If
        Else
            strErr = "alert('" & Messages.Instance.MSG020E & "');"
        End If


            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)

    End Sub

    ''' <summary>
    ''' 報告書PDF出力ボタン処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub btnHoukokusyoPdfOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHoukokusyoPdfOutput.Click
        Dim strReturn As Boolean
        Dim strErr As String = ""
        Dim intCount As Integer = 0
        '画面データを取得する
        Dim dt As Data.DataTable = Me.GetData
        '【通しNo】の設定
        Me.SetTooshiNo(dt)

        If (dt.Rows.Count > 0) Then
            For i As Integer = 0 To dt.Rows.Count - 1
                For j As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("Bukukazu").ToString = dt.Rows(j)("Bukukazu").ToString Then
                        intCount = intCount + 1
                    End If
                Next
            Next
            If intCount > dt.Rows.Count Then
                strErr = "alert('" & Messages.Instance.MSG2083E & "');"
            Else
                'DB更新
                strReturn = KensaLogic.UpdKensahoukokusho(dt, "3")
                If strReturn Then
                    'pdf出力        
                    Dim lstKanriNo As New System.Collections.Generic.List(Of String)
                    For Each row As Data.DataRow In dt.Rows
                        lstKanriNo.Add(row.Item("kanri_no").ToString)
                    Next

                    'Dim kanriNo As String = String.Join(",", lstKanriNo.ToArray)
                    Dim kanriNo As String = String.Join(SEP_STRING, lstKanriNo.ToArray)
                    Call Me.HoukokusyoPdfOutput(kanriNo)

                    Me.Kensakuzikkou()
                Else
                    '更新失敗
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "送付状PDF出力") & "');"
                End If
            End If
        Else
            strErr = "alert('" & Messages.Instance.MSG020E & "');"
        End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)

    End Sub

    ''' <summary>
    ''' 【通しNo】の設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetTooshiNo(ByRef dt As Data.DataTable)

        '加盟店番号でソートする
        Dim dv As Data.DataView = dt.DefaultView
        'dv.Sort = "hassou_date ASC, souhu_tantousya ASC, kameiten_cd ASC"
        '発送日・加盟店コード・物件No 順に付与する
        dv.Sort = "hassou_date ASC, kameiten_cd ASC, hosyousyo_no ASC, souhu_tantousya ASC"
        dt = dv.ToTable

        If Not dt.Columns.Contains("tooshi_no") Then
            dt.Columns.Add(New Data.DataColumn("tooshi_no"))
        End If

        '部数
        Dim busuu As Integer = 0
        '部数の合計
        Dim busuuSum As Integer = 0

        '通しNo
        Dim tooshiNo As Integer = 0
        '加盟店コード
        Dim kameitenCd As String = String.Empty
        '加盟店コード(旧)
        Dim kameitenCd_old As String = String.Empty
        '発送日
        Dim hassouDate As String = String.Empty
        '発送日(旧)
        Dim hassouDate_old As String = String.Empty
        '送付担当者
        Dim soufuTantousya As String = String.Empty
        '送付担当者(旧)
        Dim soufuTantousya_old As String = String.Empty

        For i As Integer = 0 To dt.Rows.Count - 1
            hassouDate = dt.Rows(i).Item("hassou_date").ToString '発送日
            soufuTantousya = dt.Rows(i).Item("souhu_tantousya").ToString '送付担当者
            kameitenCd = dt.Rows(i).Item("kameiten_cd").ToString '加盟店コード

            busuu = Convert.ToInt32(dt.Rows(i).Item("kensa_hkks_busuu")) '部数

            'If (Not kameitenCd.Equals(kameitenCd_old)) Then
            '    '加盟店コードが変えた場合
            '    tooshiNo = 0
            '    busuuSum = 0
            '    kameitenCd_old = kameitenCd
            '    hassouDate_old = String.Empty
            '    soufuTantousya_old = String.Empty
            'End If

            'If (busuu + busuuSum > 6) OrElse (Not hassouDate.Equals(hassouDate_old)) OrElse (Not soufuTantousya.Equals(soufuTantousya_old)) Then
            '    '且つ 6部を超える場合、発送日が変えた場合、送付担当者が変えた場合
            '    tooshiNo = tooshiNo + 1
            '    busuuSum = busuu
            '    hassouDate_old = hassouDate
            '    soufuTantousya_old = soufuTantousya
            'Else
            '    busuuSum = busuuSum + busuu
            'End If


            '通しNo（通しNoの＋１条件）は
            '発送日・送付担当者・加盟店コード ごとに付与される。
            '上記グルーピングした際で部数6部より大きくなる場合、通しNo＋1となる
            If (Not hassouDate.Equals(hassouDate_old)) _
                OrElse (Not soufuTantousya.Equals(soufuTantousya_old)) _
                OrElse (Not kameitenCd.Equals(kameitenCd_old)) _
                OrElse (busuu + busuuSum > 6) Then

                tooshiNo = tooshiNo + 1
                busuuSum = busuu
                hassouDate_old = hassouDate
                soufuTantousya_old = soufuTantousya
                kameitenCd_old = kameitenCd
            Else
                busuuSum = busuuSum + busuu
            End If

            dt.Rows(i).Item("tooshi_no") = tooshiNo

        Next

    End Sub

    ''' <summary>
    ''' '検査報告書管理テーブル作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Function GetData() As Data.DataTable

        Dim dt As New Data.DataTable
        Dim dr As Data.DataRow

        dt.Columns.Add(New Data.DataColumn("kanri_no"))         '管理No
        dt.Columns.Add(New Data.DataColumn("kbn"))              '区分
        dt.Columns.Add(New Data.DataColumn("hosyousyo_no"))     '保証書NO
        dt.Columns.Add(New Data.DataColumn("kameiten_cd"))      '加盟店コード
        dt.Columns.Add(New Data.DataColumn("kensa_hkks_busuu")) '検査報告書発行部数
        dt.Columns.Add(New Data.DataColumn("hassou_date"))      '発送日
        dt.Columns.Add(New Data.DataColumn("souhu_tantousya"))  '送付担当者(非表示)
        dt.Columns.Add(New Data.DataColumn("Bukukazu"))         '複数

        If Me.grdBodyLeft.Rows.Count > 0 Then
            For i As Integer = 0 To Me.grdBodyLeft.Rows.Count - 1
                dr = dt.NewRow
                '管理No
                dr.Item("kanri_no") = CType(Me.grdBodyLeft.Rows(i).FindControl("lblKanriNo"), Label).Text
                '区分
                dr.Item("kbn") = Me.grdBodyLeft.Rows(i).Cells(1).Text.Trim
                '保証書NO
                dr.Item("hosyousyo_no") = Me.grdBodyLeft.Rows(i).Cells(2).Text.Trim
                '加盟店コード
                dr.Item("kameiten_cd") = Me.grdBodyLeft.Rows(i).Cells(4).Text.Trim
                '部数
                dr.Item("kensa_hkks_busuu") = Convert.ToInt32(CType(Me.grdBodyLeft.Rows(i).FindControl("tbxBusuu"), TextBox).Text)
                '発送日
                dr.Item("hassou_date") = Me.grdBodyLeft.Rows(i).Cells(8).Text.Trim
                '送付担当者(非表示)
                dr.Item("souhu_tantousya") = Me.grdBodyLeft.Rows(i).Cells(Me.grdBodyLeft.Columns.Count - 1).Text.Trim
                '複数
                dr.Item("Bukukazu") = Me.grdBodyLeft.Rows(i).Cells(1).Text.Trim + Me.grdBodyLeft.Rows(i).Cells(2).Text.Trim + Me.grdBodyLeft.Rows(i).Cells(4).Text.Trim
                dt.Rows.Add(dr)
            Next
        End If

        Return dt
    End Function

    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("function wheel(event){")
            .AppendLine("   var delta = 0;")
            .AppendLine("   if(!event)")
            .AppendLine("       event = window.event;")
            .AppendLine("   if(event.wheelDelta){")
            .AppendLine("       delta = event.wheelDelta/120;")
            .AppendLine("       if(window.opera)")
            .AppendLine("           delta = -delta;")
            .AppendLine("       }else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("   if(delta)")
            .AppendLine("       handle(delta);")
            .AppendLine("}")
            .AppendLine("function handle(delta){")
            .AppendLine("   var divVscroll=" & divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   if (delta < 0){")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("   }else{")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            '.AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            '.AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            '.AppendLine("function fncScrollH(){")
            '.AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            '.AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            '.AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            '.AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            '.AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            '.AppendLine("}")
            '.AppendLine("function fncSetScroll(){")
            '.AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            '.AppendLine("   document.getElementById('" & Me.hidScroll.ClientID & "').value = divHscroll.scrollLeft;")
            '.AppendLine("}")
            .AppendLine("	 ")
            .AppendLine("	function LTrim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		var i; ")
            .AppendLine("		for(i=0;i<str.length;i++)  ")
            .AppendLine("		{  ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='　')break;  ")
            .AppendLine("		}  ")
            .AppendLine("		str=str.substring(i,str.length);  ")
            .AppendLine("		return str;  ")
            .AppendLine("	}  ")
            .AppendLine("	function RTrim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		var i;  ")
            .AppendLine("		for(i=str.length-1;i>=0;i--)  ")
            .AppendLine("		{  ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='　')break;  ")
            .AppendLine("		}  ")
            .AppendLine("		str=str.substring(0,i+1);  ")
            .AppendLine("		return str;  ")
            .AppendLine("	} ")
            .AppendLine("	function Trim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		return LTrim(RTrim(str));  ")
            .AppendLine("	}  ")
            .AppendLine("	 ")
            'excel出力
            .AppendLine("    function ShowModal(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       if(buyDiv.style.display=='none')")
            .AppendLine("       {")
            .AppendLine("        buyDiv.style.display='';")
            .AppendLine("        disable.style.display='';")
            '.AppendLine("        disable.style.width=995;")
            '.AppendLine("        disable.style.height=600;")
            .AppendLine("        disable.focus();")
            .AppendLine("       }else{")
            .AppendLine("        buyDiv.style.display='none';")
            .AppendLine("        disable.style.display='none';")
            .AppendLine("       }")
            .AppendLine("    }")
            '.AppendLine("    function ShowHoukokusyoPdf(index,kanriNo){")
            '.AppendLine("       var intTop = 0;")
            '.AppendLine("       var intLeft = 0;")
            '.AppendLine("       window.open(encodeURI('HoukokusyoPdfOutput.aspx?index=' + index + '&KanriNo=' + kanriNo),'HoukokusyoPdfOutput' + (new Date()).getTime() + index.toString(),'width=1024,height=768,top=' + intTop + ',left=' + intLeft + ',status=yes,resizable=yes,directories=yes,scrollbars=yes');" & vbCrLf)
            '.AppendLine("    }")
            '加盟店ポップアップ
            .AppendLine("   function fncKameitenSearch(strKbn){")
            .AppendLine("       var strkbn='加盟店'")
            .AppendLine("       var strClientID ")
            .AppendLine("       strClientID = '" & Me.tbxKameitenCd.ClientID & "'")
            .AppendLine("       strClientMei = '" & Me.tbxKameitenMei.ClientID & "'")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientID+'&objMei='+strClientMei+'&strMei='+escape(eval('document.all.'+strClientMei).value)+'&strCd='+escape(eval('document.all.'+strClientID).value),")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            '「クリア」ボタン
            .AppendLine("function fncClear()")
            .AppendLine("{")
            '発送日From
            .AppendLine("   var tbxSendDateFrom = document.getElementById('" & Me.tbxSendDateFrom.ClientID & "'); ")
            '発送日To
            .AppendLine("   var tbxSendDateTo = document.getElementById('" & Me.tbxSendDateTo.ClientID & "'); ")
            '区分
            .AppendLine("   var ddlKbn = document.getElementById('" & Me.ddlKbn.DdlClientID & "'); ")
            '番号From
            .AppendLine("   var tbxNoFrom = document.getElementById('" & Me.tbxNoFrom.ClientID & "'); ")
            '番号To
            .AppendLine("   var tbxNoTo = document.getElementById('" & Me.tbxNoTo.ClientID & "'); ")
            '加盟店CD
            .AppendLine("   var tbxKameitenCd = document.getElementById('" & Me.tbxKameitenCd.ClientID & "'); ")
            '加盟店
            .AppendLine("   var tbxKameitenMei = document.getElementById('" & Me.tbxKameitenMei.ClientID & "'); ")
            '検索上限件数
            .AppendLine("   var ddlKensakuJyouken = document.getElementById('" & Me.ddlKensakuJyouken.ClientID & "'); ")
            '取消は検索対象外
            .AppendLine("   var chkKensakuTaisyouGai = document.getElementById('" & Me.chkKensakuTaisyouGai.ClientID & "'); ")
            .AppendLine("   tbxSendDateFrom.value = '';")
            .AppendLine("   tbxSendDateTo.value = '';")
            .AppendLine("   ddlKbn.selectedIndex = '0';")
            .AppendLine("   tbxNoFrom.value = '';")
            .AppendLine("   tbxNoTo.value = '';")
            .AppendLine("   tbxKameitenCd.value = '';")
            .AppendLine("   tbxKameitenMei.value = '';")
            .AppendLine("   ddlKensakuJyouken.selectedIndex = '1';")
            .AppendLine("   chkKensakuTaisyouGai.checked = true;")
            .AppendLine("}")

            '部数チェック
            .AppendLine("function fncCheckBusuu()")
            .AppendLine("        {	")
            .AppendLine("            var gvtable = document.getElementById('" & Me.grdBodyLeft.ClientID & "');	")
            .AppendLine("            if (gvtable != null)	")
            .AppendLine("            {	")
            .AppendLine("                for (var i = 0; i < gvtable.rows.length; i++)	")
            .AppendLine("                {	")
            .AppendLine("                    var Txb = gvtable.rows(i).cells(6).children(0);	")
            .AppendLine("                    if(Txb!=null)	")
            .AppendLine("                    {	")
            .AppendLine("                        if (Txb.value !='1' && Txb.value !='2' && Txb.value !='3' && Txb.value !='4' && Txb.value !='5' && Txb.value !='6') {  ")
            .AppendLine("                            window.alert('" & Messages.Instance.MSG2082E & "');	")
            .AppendLine("                            Txb.focus(); ")
            .AppendLine("                            return  false;	")
            .AppendLine("                        } ")
            .AppendLine("                    }	")
            .AppendLine("                }	")
            .AppendLine("            }	")
            .AppendLine("            return  true;	")
            .AppendLine("        }	")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>
    ''' 送付状PDF出力
    ''' </summary>
    ''' <param name="kanriNo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub SoufujyouPdfOutput(ByVal kanriNo As String)
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("window.open(encodeURI('SoufujyouPdfOutput.aspx?KanriNo=" & kanriNo & "'),'SoufujyouPdfOutput','width=1024,height=768,top=0,left=0,status=yes,resizable=yes,directories=yes,scrollbars=yes');" & vbCrLf)
        End With

        Page.ClientScript.RegisterStartupScript(Page.GetType, "SoufuJyouPdfOutput", sbScript.ToString, True)
    End Sub

    ''' <summary>
    ''' 報告書PDF出力
    ''' </summary>
    ''' <param name="kanriNo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub HoukokusyoPdfOutput(ByVal kanriNo As String)

        'Dim sbScript As New StringBuilder
        'Dim strPraram(0) As String
        'With sbScript
        '    .AppendLine("       var buyDiv1=document.getElementById('" & Me.buySelName.ClientID & "');")
        '    .AppendLine("       var disable1=document.getElementById('" & Me.disableDiv.ClientID & "');")
        '    .AppendLine("        buyDiv1.style.display='';")
        '    .AppendLine("        disable1.style.display='';")
        '    .AppendLine("        disable1.focus();")
        '    For i As Integer = 0 To kanriNo.Split(",").Length - 1
        '        .AppendLine("setTimeout(""ShowHoukokusyoPdf('" & i.ToString & "','" & kanriNo.Split(",")(i) & "');""," & i * OUTPUT_WAIT_TIME & ");" & vbCrLf)
        '        '.AppendLine("ShowHoukokusyoPdf('" & HoukokusyoCnt & "','" & kanriNo.Split(",")(i) & "');" & vbCrLf)
        '    Next

        '    .AppendLine("setTimeout(""ShowModal();""," & kanriNo.Split(",").Length * OUTPUT_WAIT_TIME & ");")
        'End With

        'Page.ClientScript.RegisterStartupScript(Page.GetType, "HoukokusyoPdfOutput", sbScript.ToString, True)

        'HoukokusyoCnt = HoukokusyoCnt + 1


        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("var buyDiv1=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("var disable1=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("buyDiv1.style.display='';")
            .AppendLine("disable1.style.display='';")
            .AppendLine("disable1.focus();")

            .AppendLine("var winHoukokusyoPdfOutput;")
            .AppendLine("function fncHideCover()")
            .AppendLine("{")
            .AppendLine("   if(winHoukokusyoPdfOutput.closed)")
            .AppendLine("   {")
            .AppendLine("        buyDiv1.style.display='none';")
            .AppendLine("        disable1.style.display='none';")
            .AppendLine("   }")
            .AppendLine("   else")
            .AppendLine("   {")
            .AppendLine("       setTimeout('fncHideCover();',1000);")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("winHoukokusyoPdfOutput = window.open(encodeURI('HoukokusyoPdfOutput.aspx?index=0&KanriNo=" & kanriNo & "'),'HoukokusyoPdfOutput' + (new Date()).getTime(),'width=1024,height=768,top=0,left=0,status=yes,resizable=yes,directories=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("setTimeout('fncHideCover();',1000);")
        End With

        Page.ClientScript.RegisterStartupScript(Page.GetType, "HoukokusyoPdfOutput", sbScript.ToString, True)
    End Sub

    ''' <summary>
    ''' 管理表EXCEL出力
    ''' </summary>
    ''' <param name="kanriNo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub CheckXlt(ByVal kanriNo As String)

        Dim csType As Type = Page.GetType
        Dim csName As String = "CheckXlt"
        Dim csScript As New StringBuilder

        csScript.Append("<script language='vbscript' type='text/vbscript'>" & vbCrLf)
        csScript.Append("function body_onLoad(obj)" & vbCrLf)
        csScript.Append("   Dim sv_templist,cr_templist,strHour, strMinute,strYEAR, strMONTH, strDAY" & vbCrLf)
        csScript.Append("   Dim TimeStamp,dwn_flg,c_obj_Fso,File_Path,c_obj_Folder,c_obj_File,fl,i" & vbCrLf)
        csScript.Append("   On Error Resume Next" & vbCrLf)
        csScript.Append("       dwn_flg = false" & vbCrLf)
        csScript.Append("       sv_templist=""" & sv_templist & """" & vbCrLf)
        csScript.Append("       cr_templist =""""" & vbCrLf)
        csScript.Append("       File_Path =""" & sDirName & """" & vbCrLf)
        csScript.Append("       If sv_templist <> """" Then" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = c_obj_Fso.GetFolder(File_Path)" & vbCrLf)
        csScript.Append("           Set c_obj_File = c_obj_Folder.Files" & vbCrLf)
        csScript.Append("           For Each fl In c_obj_File" & vbCrLf)

        csScript.Append("               If (Ucase(right(fl.Name,4)) = ""XLTM"") Then" & vbCrLf)
        csScript.Append("                   If cr_templist <> """" Then" & vbCrLf)
        csScript.Append("                       cr_templist = cr_templist & "",""" & vbCrLf)
        csScript.Append("                   End If" & vbCrLf)
        csScript.Append("                   strYEAR   = Right(""0000"" & CStr(YEAR(fl.DateLastModified)), 4)" & vbCrLf)
        csScript.Append("                   strMONTH  = Right(""00"" & CStr(MONTH(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strDAY    = Right(""00"" & Cstr(DAY(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strHour   = Right(""00"" & CStr(Hour(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strMinute = Right(""00"" & CStr(Minute(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   timeStamp = strYEAR & strMONTH & strDAY & strHour & strMinute" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"" "","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,""/"","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"":"","""")" & vbCrLf)

        csScript.Append("                   cr_templist = cr_templist & trim(fl.Name) & "":"" & trim(timestamp)" & vbCrLf)

        csScript.Append("               End If" & vbCrLf)
        csScript.Append("           Next" & vbCrLf)

        csScript.Append("           Set c_obj_File = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = Nothing" & vbCrLf)
        csScript.Append("           If Err > 0 or cr_templist = """" Then" & vbCrLf)
        csScript.Append("               dwn_flg = true" & vbCrLf)
        csScript.Append("           Else" & vbCrLf)
        csScript.Append("               ar_cr = split(Ucase(cr_templist), "","")" & vbCrLf)
        csScript.Append("               ar_sv = split(Ucase(sv_templist), "","")" & vbCrLf)
        csScript.Append("               For i = 0 to Ubound(ar_sv)" & vbCrLf)
        csScript.Append("                   rc = Filter(ar_cr, ar_sv(i))" & vbCrLf)
        csScript.Append("                   If Ubound(rc) = 0 then" & vbCrLf)
        csScript.Append("                       If rc(0) = """" then" & vbCrLf)
        csScript.Append("                           dwn_flg = true" & vbCrLf)
        csScript.Append("                           Exit For" & vbCrLf)
        csScript.Append("                       End if" & vbCrLf)
        csScript.Append("                   Else" & vbCrLf)
        csScript.Append("                       dwn_flg = true" & vbCrLf)
        csScript.Append("                       Exit For" & vbCrLf)
        csScript.Append("                   End if" & vbCrLf)
        csScript.Append("               Next" & vbCrLf)
        csScript.Append("           End if" & vbCrLf)
        csScript.Append("           If dwn_flg = true Then" & vbCrLf)
        csScript.Append("               call download(obj)" & vbCrLf)
        csScript.Append("           End If" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("       If dwn_flg = false Then" & vbCrLf)
        csScript.Append("           fncSubmit(obj)" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("End function" & vbCrLf)

        csScript.Append("function body_onLoad2(obj)" & vbCrLf)
        csScript.Append("   Dim sv_templist,cr_templist,strHour, strMinute,strYEAR, strMONTH, strDAY" & vbCrLf)
        csScript.Append("   Dim TimeStamp,dwn_flg,c_obj_Fso,File_Path,c_obj_Folder,c_obj_File,fl,i" & vbCrLf)
        csScript.Append("   On Error Resume Next" & vbCrLf)
        csScript.Append("       dwn_flg = false" & vbCrLf)
        csScript.Append("       sv_templist=""" & sv_templist & """" & vbCrLf)
        csScript.Append("       cr_templist =""""" & vbCrLf)
        csScript.Append("       File_Path =""" & sDirName & """" & vbCrLf)
        csScript.Append("       If sv_templist <> """" Then" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = c_obj_Fso.GetFolder(File_Path)" & vbCrLf)
        csScript.Append("           Set c_obj_File = c_obj_Folder.Files" & vbCrLf)
        csScript.Append("           For Each fl In c_obj_File" & vbCrLf)
        csScript.Append("               If (Ucase(right(fl.Name,4)) = ""XLTM"") Then" & vbCrLf)
        csScript.Append("                   If cr_templist <> """" Then" & vbCrLf)
        csScript.Append("                       cr_templist = cr_templist & "",""" & vbCrLf)
        csScript.Append("                   End If" & vbCrLf)
        csScript.Append("                   strYEAR   = Right(""0000"" & CStr(YEAR(fl.DateLastModified)), 4)" & vbCrLf)
        csScript.Append("                   strMONTH  = Right(""00"" & CStr(MONTH(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strDAY    = Right(""00"" & Cstr(DAY(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strHour   = Right(""00"" & CStr(Hour(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strMinute = Right(""00"" & CStr(Minute(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   timeStamp = strYEAR & strMONTH & strDAY & strHour & strMinute" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"" "","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,""/"","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"":"","""")" & vbCrLf)
        csScript.Append("                   cr_templist = cr_templist & trim(fl.Name) & "":"" & trim(timestamp)" & vbCrLf)
        csScript.Append("               End If" & vbCrLf)
        csScript.Append("           Next" & vbCrLf)

        csScript.Append("           Set c_obj_File = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = Nothing" & vbCrLf)
        csScript.Append("           If Err > 0 or cr_templist = """" Then" & vbCrLf)
        csScript.Append("               dwn_flg = true" & vbCrLf)
        csScript.Append("           Else" & vbCrLf)
        csScript.Append("               ar_cr = split(Ucase(cr_templist), "","")" & vbCrLf)
        csScript.Append("               ar_sv = split(Ucase(sv_templist), "","")" & vbCrLf)
        csScript.Append("               For i = 0 to Ubound(ar_sv)" & vbCrLf)
        csScript.Append("                   rc = Filter(ar_cr, ar_sv(i))" & vbCrLf)
        csScript.Append("                   If Ubound(rc) = 0 then" & vbCrLf)
        csScript.Append("                       If rc(0) = """" then" & vbCrLf)
        csScript.Append("                           dwn_flg = true" & vbCrLf)
        csScript.Append("                           Exit For" & vbCrLf)
        csScript.Append("                       End if" & vbCrLf)
        csScript.Append("                   Else" & vbCrLf)
        csScript.Append("                       dwn_flg = true" & vbCrLf)
        csScript.Append("                       Exit For" & vbCrLf)
        csScript.Append("                   End if" & vbCrLf)
        csScript.Append("               Next" & vbCrLf)
        csScript.Append("           End if" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("       If dwn_flg = false Then" & vbCrLf)
        csScript.Append("           call fncSubmit(obj)" & vbCrLf)
        csScript.Append("       else" & vbCrLf)
        csScript.Append("           call body_load3(obj)" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("End function" & vbCrLf)
        csScript.Append("</script>" & vbCrLf)

        csScript.Append("<script language='javascript' type='text/javascript'>" & vbCrLf)
        csScript.Append("function download(obj){" & vbCrLf)
        csScript.Append("   window.location.href='data/KensaHoukokusyo/earth_kensaHoukokusyo.lha';" & vbCrLf)
        csScript.Append("   body_load3(obj);" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.Append("function body_load3(obj){" & vbCrLf)
        csScript.Append("   setTimeout('body_onLoad2(' + obj + ')',1000);" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.Append("function fncSubmit(obj) {" & vbCrLf)
        csScript.Append("fncExcelOutput();" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.AppendLine("    function fncExcelOutput(){")
        csScript.AppendLine("       ShowModal();")
        csScript.AppendLine("       var objwindow=window.open(encodeURI('WaitMsg.aspx?url=KanriHyouExcelOutput.aspx?strNoAddId=" & kanriNo & "," & Me.buySelName.ClientID & "," & Me.disableDiv.ClientID & "'),'proxy_operation','width=450,height=150,status=no,resizable=no,directories=no,scrollbars=no,left=0,top=0');" & vbCrLf)
        csScript.AppendLine("       objwindow.focus();")
        csScript.AppendLine("    }" & vbCrLf)

        csScript.Append("</script>" & vbCrLf)

        ClientScript.RegisterStartupScript(csType, csName, csScript.ToString)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub SetScrollBar()
        If Me.grdBodyLeft.Rows.Count > 0 Then
            Me.gridviewId = Me.grdBodyLeft.ClientID
            Me.tblRightId = Me.tblLeft.ClientID
        Else
            Me.gridviewId = String.Empty
            Me.tblRightId = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Function getfiledatetimelist(ByVal path As String) As String
        Dim fo As New Scripting.FileSystemObject
        Dim fp As String
        Dim fr As Scripting.Folder
        Dim fc As Scripting.Files
        Dim fl As Scripting.File
        Dim fname As String, s As String, timestamp As String
        Dim strHour As String, strMinute As String
        Dim strYEAR As String, strMONTH As String, strDAY As String

        fp = Server.MapPath(path)
        fr = fo.GetFolder(fp)
        fc = fr.Files
        s = ""
        For Each fl In fc
            fname = fl.Name
            If (UCase(Right(fname, 4)) = "XLTM") Then
                If s <> "" Then
                    s = s & ","
                End If
                timestamp = CStr(fl.DateLastModified)
                ''日付時間軸の整形
                strYEAR = Right("0000" & CStr(Year(fl.DateLastModified)), 4)
                strMONTH = Right("00" & CStr(Month(fl.DateLastModified)), 2)
                strDAY = Right("00" & CStr(Day(fl.DateLastModified)), 2)
                strHour = Right("00" & CStr(Hour(fl.DateLastModified)), 2)
                strMinute = Right("00" & CStr(Minute(fl.DateLastModified)), 2)
                timestamp = strYEAR & strMONTH & strDAY & strHour & strMinute
                timestamp = Replace(timestamp, " ", "")
                timestamp = Replace(timestamp, "/", "")
                timestamp = Replace(timestamp, ":", "")
                s = s & Trim(fname) & ":" & Trim(timestamp)
            End If
        Next
        getfiledatetimelist = s
        fo = Nothing
        fp = Nothing
        fr = Nothing
        fc = Nothing
        fl = Nothing
    End Function

    ''' <summary>
    ''' <summary>CSV出力ボタンをクリック時</summary>
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click
        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '検索件数
        Dim intCount As Long
        'CSV出力上限件数
        Dim intCsvMax As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax"))

        '加盟店名を設定
        Call Me.SetkameitenCd(tbxKameitenCd.Text)
        '検査報告書管理テーブルを取得
        Dim dtKensaHkksKanri As New DataTable
        dtKensaHkksKanri = KensaLogic.GetKensaHoukokusyoKanriSearch(Me.tbxSendDateFrom.Text.Trim, _
                                                                  Me.tbxSendDateTo.Text.Trim, _
                                                                  Me.ddlKbn.SelectedValue.Trim, _
                                                                  Me.tbxNoFrom.Text.Trim, _
                                                                  Me.tbxNoTo.Text.Trim, _
                                                                  Me.tbxKameitenCd.Text.Trim, _
                                                                  Me.ddlKensakuJyouken.SelectedValue.Trim, _
                                                                  Me.chkKensakuTaisyouGai.Checked)

        If dtKensaHkksKanri.Rows.Count > 0 Then

            ViewState("dtKensaHkksKanri") = dtKensaHkksKanri

            ViewState("dtKensaHkksKanri") = dtKensaHkksKanri
            Me.grdBodyLeft.DataSource = ViewState("dtKensaHkksKanri")
            Me.grdBodyLeft.DataBind()
            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(True)

            'リンクボタンの色を設定
            Call Me.setUpDownColor()

            ''検索結果を設定
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(True)

            '検索結果を設定
            intCount = Me.SetKensakuResult(True)

        Else
            ViewState("dtKensaHkksKanri") = Nothing

            Me.grdBodyLeft.DataSource = Nothing
            Me.grdBodyLeft.DataBind()
            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(False)

            '検索結果を設定
            intCount = Me.SetKensakuResult(False)
        End If

        ViewState("scrollHeight") = scrollHeight

        If intCount > intCsvMax Then
            strErrMessage = Messages.Instance.MSG051E.Replace("@PARAM1", intCsvMax)
            ShowMessage(strErrMessage, String.Empty)
        Else
            Me.hidCsvOut.Value = "1"
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.getElementById('" & Me.btnCsv.ClientID & "').click();</script>")
        End If
    End Sub

    Private Sub btnCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsv.Click
        'CSV出力
        Call CsvOutPut()
    End Sub

    ''' <summary>画面タイトルに並び順をクリック時</summary>
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKanriNoUp.Click, _
                                                                                            btnKanriNoDown.Click, _
                                                                                            btnKbnUp.Click, _
                                                                                            btnKbnDown.Click, _
                                                                                            btnHosyousyoNoUp.Click, _
                                                                                            btnHosyousyoNoDown.Click, _
                                                                                            btnKameitenCdUp.Click, _
                                                                                            btnKameitenCdDown.Click, _
                                                                                            btnKakunouDateUp.Click, _
                                                                                            btnKakunouDateDown.Click, _
                                                                                            btnKameitenMeiUp.Click, _
                                                                                            btnKameitenMeiDown.Click, _
                                                                                            btnSesyuMeiUp.Click, _
                                                                                            btnSesyuMeiDown.Click, _
                                                                                            btnBusuuUp.Click, _
                                                                                            btnBusuuDown.Click, _
                                                                                            btnHassouDateUp.Click, _
                                                                                            btnHassouDateDown.Click ', _
        'btnKanrihyouUp.Click, _
        'btnKanrihyouDown.Click, _
        'btnSoufujyouUp.Click, _
        'btnSoufujyouDown.Click, _
        'btnHoukokuhyouUp.Click, _
        'btnHoukokuhyouDown.Click





        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        Call Me.setUpDownColor()

        '画面にソート順をクリック時
        Select Case CType(sender, LinkButton).ID
            Case btnKanriNoUp.ID
                strSort = "kanri_no"
                strUpDown = "ASC"
                btnKanriNoUp.ForeColor = Drawing.Color.IndianRed
            Case btnKanriNoDown.ID
                strSort = "kanri_no"
                strUpDown = "DESC"
                btnKanriNoDown.ForeColor = Drawing.Color.IndianRed
            Case btnKbnUp.ID
                strSort = "kbn"
                strUpDown = "ASC"
                btnKbnUp.ForeColor = Drawing.Color.IndianRed
            Case btnKbnDown.ID
                strSort = "kbn"
                strUpDown = "DESC"
                btnKbnDown.ForeColor = Drawing.Color.IndianRed
            Case btnHosyousyoNoUp.ID
                strSort = "hosyousyo_no"
                strUpDown = "ASC"
                btnHosyousyoNoUp.ForeColor = Drawing.Color.IndianRed
            Case btnHosyousyoNoDown.ID
                strSort = "hosyousyo_no"
                strUpDown = "DESC"
                btnHosyousyoNoDown.ForeColor = Drawing.Color.IndianRed
            Case btnSesyuMeiUp.ID
                strSort = "sesyu_mei"
                strUpDown = "ASC"
                btnSesyuMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSesyuMeiDown.ID
                strSort = "sesyu_mei"
                strUpDown = "DESC"
                btnSesyuMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnKameitenCdUp.ID
                strSort = "kameiten_cd"
                strUpDown = "ASC"
                btnKameitenCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnKameitenCdDown.ID
                strSort = "kameiten_cd"
                strUpDown = "DESC"
                btnKameitenCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnKameitenMeiUp.ID
                strSort = "kameiten_mei"
                strUpDown = "ASC"
                btnKameitenMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnKameitenMeiDown.ID
                strSort = "kameiten_mei"
                strUpDown = "DESC"
                btnKameitenMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnBusuuUp.ID
                strSort = "kensa_hkks_busuu"
                strUpDown = "ASC"
                btnBusuuUp.ForeColor = Drawing.Color.IndianRed
            Case btnBusuuDown.ID
                strSort = "kensa_hkks_busuu"
                strUpDown = "DESC"
                btnBusuuDown.ForeColor = Drawing.Color.IndianRed
            Case btnKakunouDateUp.ID
                strSort = "kakunou_date"
                strUpDown = "ASC"
                btnKakunouDateUp.ForeColor = Drawing.Color.IndianRed
            Case btnKakunouDateDown.ID
                strSort = "kakunou_date"
                strUpDown = "DESC"
                btnKakunouDateDown.ForeColor = Drawing.Color.IndianRed
            Case btnHassouDateUp.ID
                strSort = "hassou_date"
                strUpDown = "ASC"
                btnHassouDateUp.ForeColor = Drawing.Color.IndianRed
            Case btnHassouDateDown.ID
                strSort = "hassou_date"
                strUpDown = "DESC"
                btnHassouDateDown.ForeColor = Drawing.Color.IndianRed
                'Case btnKanrihyouUp.ID
                '    strSort = "kanrihyou_out_flg"
                '    strUpDown = "ASC"
                '    btnKanrihyouUp.ForeColor = Drawing.Color.IndianRed
                'Case btnKanrihyouDown.ID
                '    strSort = "kanrihyou_out_flg"
                '    strUpDown = "DESC"
                '    btnKanrihyouDown.ForeColor = Drawing.Color.IndianRed
                'Case btnSoufujyouUp.ID
                '    strSort = "souhujyou_out_flg"
                '    strUpDown = "ASC"
                '    btnSoufujyouUp.ForeColor = Drawing.Color.IndianRed
                'Case btnSoufujyouDown.ID
                '    strSort = "souhujyou_out_flg"
                '    strUpDown = "DESC"
                '    btnSoufujyouDown.ForeColor = Drawing.Color.IndianRed
                'Case btnHoukokuhyouUp.ID
                '    strSort = "kensa_hkks_out_flg"
                '    strUpDown = "ASC"
                '    btnHoukokuhyouUp.ForeColor = Drawing.Color.IndianRed
                'Case btnHoukokuhyouDown.ID
                '    strSort = "kensa_hkks_out_flg"
                '    strUpDown = "DESC"
                '    btnHoukokuhyouDown.ForeColor = Drawing.Color.IndianRed
        End Select

        '画面データのソート順を設定する
        Dim dvKameitenInfo As Data.DataView = CType(ViewState("dtKensaHkksKanri"), Data.DataTable).DefaultView
        dvKameitenInfo.Sort = strSort & " " & strUpDown

        Me.grdBodyLeft.DataSource = dvKameitenInfo
        Me.grdBodyLeft.DataBind()

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

    End Sub

    ''' <summary>
    ''' 検索実行
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/14　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub Kensakuzikkou()

        '加盟店名を設定
        Call Me.SetkameitenCd(tbxKameitenCd.Text)

        '検査報告書管理テーブルを取得
        Dim dtKensaHkksKanri As New DataTable
        dtKensaHkksKanri = KensaLogic.GetKensaHoukokusyoKanriSearch(Me.tbxSendDateFrom.Text.Trim, _
                                                                  Me.tbxSendDateTo.Text.Trim, _
                                                                  Me.ddlKbn.SelectedValue.Trim, _
                                                                  Me.tbxNoFrom.Text.Trim, _
                                                                  Me.tbxNoTo.Text.Trim, _
                                                                  Me.tbxKameitenCd.Text.Trim, _
                                                                  Me.ddlKensakuJyouken.SelectedValue.Trim, _
                                                                  Me.chkKensakuTaisyouGai.Checked)

        If dtKensaHkksKanri.Rows.Count > 0 Then

            ViewState("dtKensaHkksKanri") = dtKensaHkksKanri
            Me.grdBodyLeft.DataSource = ViewState("dtKensaHkksKanri")
            Me.grdBodyLeft.DataBind()

            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(True)

            'リンクボタンの色を設定
            Call Me.setUpDownColor()

            ''検索結果を設定
            ViewState("intCount") = dtKensaHkksKanri.Rows.Count
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(True)

        Else
            ViewState("dtKensaHkksKanri") = Nothing

            Me.grdBodyLeft.DataSource = Nothing
            Me.grdBodyLeft.DataBind()

            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(False)

            '検索結果を設定
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(False)
            'エラーメッセージを表示
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
        End If

        ViewState("scrollHeight") = scrollHeight

    End Sub

    Private Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("tbxBusuu"), TextBox).Attributes.Add("onfocus", "this.select();")
            e.Row.Cells(e.Row.Cells.Count - 1).Visible = False
        End If
    End Sub

    ''' <summary>
    ''' CSV出力
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub CsvOutPut()
        '検査報告書管理テーブルを取得
        Dim dtKensaHkksKanri As New DataTable
        dtKensaHkksKanri = KensaLogic.GetKensaHoukokusyoKanriSearch(Me.tbxSendDateFrom.Text.Trim, _
                                                                  Me.tbxSendDateTo.Text.Trim, _
                                                                  Me.ddlKbn.SelectedValue.Trim, _
                                                                  Me.tbxNoFrom.Text.Trim, _
                                                                  Me.tbxNoTo.Text.Trim, _
                                                                  Me.tbxKameitenCd.Text.Trim, _
                                                                  Me.ddlKensakuJyouken.SelectedValue.Trim, _
                                                                  Me.chkKensakuTaisyouGai.Checked)

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("KensahoukokushoOutputCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conKensahoukokushoOutputCsvHeader)

        'CSVファイル内容設定
        For i As Integer = 0 To dtKensaHkksKanri.Rows.Count - 1
            With dtKensaHkksKanri.Rows(i)
                writer.WriteLine(.Item("kanri_no"), .Item("kbn"), .Item("hosyousyo_no"), .Item("sesyu_mei"), .Item("kameiten_cd"), _
                                 .Item("kameiten_mei"), .Item("kensa_hkks_busuu"), .Item("kakunou_date"), .Item("hassou_date"), _
                                 .Item("kanrihyou_out_flg"), .Item("souhujyou_out_flg"), .Item("kensa_hkks_out_flg"))
            End With
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    ''' <summary>加盟店名を設定</summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub SetkameitenCd(ByVal strKameitenCd As String)

        If Not strKameitenCd.Trim.Equals(String.Empty) Then
            '加盟店名データを取得
            Dim dtKameitenMei As New Data.DataTable
            dtKameitenMei = KensaLogic.SelMkameiten(strKameitenCd)

            If dtKameitenMei.Rows.Count > 0 Then
                '加盟店名
                Me.tbxKameitenMei.Text = dtKameitenMei.Rows(0).Item(0).ToString.Trim
            Else
                Me.tbxKameitenMei.Text = String.Empty
            End If
        Else
            Me.tbxKameitenMei.Text = String.Empty
        End If

    End Sub

    ''' <summary>入力チェック</summary>
    ''' <param name="strObjId">クライアントID</param>
    ''' <returns>エラーメッセージ</returns>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            If Me.tbxSendDateFrom.Text = String.Empty AndAlso Me.tbxSendDateTo.Text = String.Empty Then
                '必須入力チェック
                .Append(commonCheck.CheckHissuNyuuryoku(Me.tbxSendDateFrom.Text, "発送日"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSendDateFrom.ClientID
                End If
            End If
            '発送日(From)
            If Me.tbxSendDateFrom.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(Me.tbxSendDateFrom.Text, "発送日(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSendDateFrom.ClientID
                End If
            End If
            '発送日(To)
            If Me.tbxSendDateTo.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(Me.tbxSendDateTo.Text, "発送日(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSendDateTo.ClientID
                End If
            End If
            '発送日範囲
            If Me.tbxSendDateFrom.Text <> String.Empty And Me.tbxSendDateTo.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(Me.tbxSendDateFrom.Text, "発送日(From)") = String.Empty _
                   And commonCheck.CheckYuukouHiduke(Me.tbxSendDateTo.Text, "発送日(To)") = String.Empty Then
                    .Append(commonCheck.CheckHidukeHani(Me.tbxSendDateFrom.Text, Me.tbxSendDateTo.Text, "発送日"))
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxSendDateFrom.ClientID
                    End If
                End If
            End If
            If Me.tbxSendDateFrom.Text = String.Empty And Me.tbxSendDateTo.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(Me.tbxSendDateTo.Text, "発送日(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, "発送日", "発送日").ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSendDateFrom.ClientID
                End If
            End If

            '加盟店コード
            If Me.tbxKameitenCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd.Text, "加盟店コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCd.ClientID
                End If
            End If

            '番号From
            If tbxNoFrom.Text <> "" Then
                .Append(commonCheck.CheckHankaku(Me.tbxNoFrom.Text, "番号(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxNoFrom.ClientID
                End If
            End If
            '番号To
            If tbxNoTo.Text <> "" Then
                .Append(commonCheck.CheckHankaku(Me.tbxNoTo.Text, "番号(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxNoTo.ClientID
                End If
            End If
            '番号範囲
            If Me.tbxNoFrom.Text <> String.Empty And Me.tbxNoTo.Text <> String.Empty Then
                If commonCheck.CheckHankaku(Me.tbxNoTo.Text, "番号(From)") = String.Empty _
                   And commonCheck.CheckHankaku(Me.tbxNoTo.Text, "番号(To)") = String.Empty Then
                    If tbxNoFrom.Text > tbxNoTo.Text And tbxNoTo.Text <> "" Then
                        .Append(String.Format(Messages.Instance.MSG2012E, "番号", "番号").ToString)
                    End If
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxNoFrom.ClientID
                    End If
                End If
            End If
            If Me.tbxNoFrom.Text = String.Empty And Me.tbxNoTo.Text <> String.Empty Then
                If commonCheck.CheckHankaku(Me.tbxNoTo.Text, "番号(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, "番号", "番号").ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxNoFrom.ClientID
                End If
            End If

        End With

        Return csScript.ToString
    End Function

    ''' <summary>
    ''' <summary>検索結果を設定</summary>
    ''' </summary>
    ''' <param name="blnFlg"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Function SetKensakuResult(ByVal blnFlg As Boolean) As Integer

        '件数を取得
        Dim intGenkaJyouhouCount As Integer = 0

        If blnFlg.Equals(True) Then

            intGenkaJyouhouCount = KensaLogic.GetKensaHoukokusyoKanriSearchCount(Me.tbxSendDateFrom.Text.Trim, _
                                                                              Me.tbxSendDateTo.Text.Trim, _
                                                                              Me.ddlKbn.SelectedValue.Trim, _
                                                                              Me.tbxNoFrom.Text.Trim, _
                                                                              Me.tbxNoTo.Text.Trim, _
                                                                              Me.tbxKameitenCd.Text.Trim, _
                                                                              Me.ddlKensakuJyouken.SelectedValue.Trim, _
                                                                              Me.chkKensakuTaisyouGai.Checked)

            If Me.ddlKensakuJyouken.SelectedValue.Trim.Equals("max") Then

                Me.lblCount.Text = CStr(intGenkaJyouhouCount)
                '黒色
                Me.lblCount.ForeColor = Drawing.Color.Black

                scrollHeight = intGenkaJyouhouCount * 25 + 1
            Else
                If intGenkaJyouhouCount > CInt(Me.ddlKensakuJyouken.SelectedValue) Then
                    Me.lblCount.Text = Me.ddlKensakuJyouken.SelectedValue.Trim & " / " & CStr(intGenkaJyouhouCount)
                    '赤色
                    Me.lblCount.ForeColor = Drawing.Color.Red

                    scrollHeight = Me.ddlKensakuJyouken.SelectedValue * 25 + 1
                Else
                    Me.lblCount.Text = CStr(intGenkaJyouhouCount)
                    '黒色
                    Me.lblCount.ForeColor = Drawing.Color.Black

                    scrollHeight = intGenkaJyouhouCount * 25 + 1
                End If
            End If
        Else
            Me.lblCount.Text = "0"
            '黒色
            Me.lblCount.ForeColor = Drawing.Color.Black

            scrollHeight = intGenkaJyouhouCount * 25 + 1

        End If

        Return intGenkaJyouhouCount

    End Function

    ''' <summary>
    ''' <summary>リンクボタンの色を設定</summary>
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Public Sub setUpDownColor()

        btnKanrinoUp.ForeColor = Drawing.Color.SkyBlue
        btnKanrinoDown.ForeColor = Drawing.Color.SkyBlue
        btnKbnUp.ForeColor = Drawing.Color.SkyBlue
        btnKbnDown.ForeColor = Drawing.Color.SkyBlue
        btnHosyousyonoUp.ForeColor = Drawing.Color.SkyBlue
        btnHosyousyonoDown.ForeColor = Drawing.Color.SkyBlue
        btnKameitencdUp.ForeColor = Drawing.Color.SkyBlue
        btnKameitencdDown.ForeColor = Drawing.Color.SkyBlue
        btnKakunoudateUp.ForeColor = Drawing.Color.SkyBlue
        btnKakunouDateDown.ForeColor = Drawing.Color.SkyBlue
        btnBusuuUp.ForeColor = Drawing.Color.SkyBlue
        btnBusuuDown.ForeColor = Drawing.Color.SkyBlue
        btnKameitenmeiUp.ForeColor = Drawing.Color.SkyBlue
        btnKameitenmeiDown.ForeColor = Drawing.Color.SkyBlue
        btnSesyumeiUp.ForeColor = Drawing.Color.SkyBlue
        btnSesyumeiDown.ForeColor = Drawing.Color.SkyBlue
        btnHassoudateUp.ForeColor = Drawing.Color.SkyBlue
        btnHassouDateDown.ForeColor = Drawing.Color.SkyBlue
        'btnKanrihyouUp.ForeColor = Drawing.Color.SkyBlue
        'btnKanrihyouDown.ForeColor = Drawing.Color.SkyBlue
        'btnSoufujyouUp.ForeColor = Drawing.Color.SkyBlue
        'btnSoufujyouDown.ForeColor = Drawing.Color.SkyBlue
        'btnHoukokuhyouUp.ForeColor = Drawing.Color.SkyBlue
        'btnHoukokuhyouDown.ForeColor = Drawing.Color.SkyBlue

    End Sub

    ''' <summary>
    ''' <summary>リンクボタンの表示を設定</summary>
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Public Sub SetUpDownHyouji(ByVal blnHyoujiFlg As Boolean)

        If blnHyoujiFlg = True Then
            btnKanrinoUp.Visible = True
            btnKanrinoDown.Visible = True
            'btnKbnUp.Visible = True
            'btnKbnDown.Visible = True
            btnHosyousyonoUp.Visible = True
            btnHosyousyonoDown.Visible = True
            btnKameitencdUp.Visible = True
            btnKameitencdDown.Visible = True
            btnKakunoudateUp.Visible = True
            btnKakunoudateDown.Visible = True
            btnKameitenmeiUp.Visible = True
            btnKameitenmeiDown.Visible = True
            btnSesyumeiUp.Visible = True
            btnSesyumeiDown.Visible = True
            btnHassoudateUp.Visible = True
            btnHassouDateDown.Visible = True
            btnBusuuUp.Visible = True
            btnBusuuDown.Visible = True
            'btnKanrihyouUp.Visible = True
            'btnKanrihyouDown.Visible = True
            'btnSoufujyouUp.Visible = True
            'btnSoufujyouDown.Visible = True
            'btnHoukokuhyouUp.Visible = True
            'btnHoukokuhyouDown.Visible = True
        Else
            btnKanrinoUp.Visible = False
            btnKanrinoDown.Visible = False
            btnKbnUp.Visible = False
            btnKbnDown.Visible = False
            btnHosyousyonoUp.Visible = False
            btnHosyousyonoDown.Visible = False
            btnKameitencdUp.Visible = False
            btnKameitencdDown.Visible = False
            btnKakunoudateUp.Visible = False
            btnKakunoudateDown.Visible = False
            btnKameitenmeiUp.Visible = False
            btnKameitenmeiDown.Visible = False
            btnSesyumeiUp.Visible = False
            btnSesyumeiDown.Visible = False
            btnHassoudateUp.Visible = False
            btnHassouDateDown.Visible = False
            btnBusuuUp.Visible = False
            btnBusuuDown.Visible = False
            'btnKanrihyouUp.Visible = False
            'btnKanrihyouDown.Visible = False
            'btnSoufujyouUp.Visible = False
            'btnSoufujyouDown.Visible = False
            'btnHoukokuhyouUp.Visible = False
            'btnHoukokuhyouDown.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' エラーメッセージ表示
    ''' </summary>
    ''' <param name="strMessage">メッセージ</param>
    ''' <param name="strObjId">クライアントID</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
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
    ''' 日付型変更処理
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Public Function GetstrDate(ByVal s As Object) As String
        If s Is DBNull.Value OrElse s.ToString = String.Empty Then
            Return String.Empty
        Else
            Return CStr(s).Substring(0, 10)
        End If
    End Function

End Class