Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class BukkenJyouhouInquiry
    Inherits System.Web.UI.Page
    
    ''' <summary>加盟店物件情報照会する</summary>
    ''' <remarks>加盟店物件情報照会機能を提供する</remarks>
    ''' <history>
    ''' <para>2009/07/15　馬艶軍(大連情報システム部)　新規作成</para>
    ''' </history>
    Private bukkenJyouhouInquiryLogic As New BukkenJyouhouInquiryLogic
    Private commonCheck As New CommonCheck
    'ログインユーザーを取得する。
    Private ninsyou As New Ninsyou()
    '高
    Public scrollWidth As Integer = 0
    '幅
    Public scrollHeight As Integer = 0

    ''' <summary> ページロッド </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '基本認証
        If ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If

        'JavaScript
        MakeJavaScript()

        If Not IsPostBack Then

            '加盟店コード
            ViewState.Item("strKameitenCd") = Request.QueryString("strKameitenCd")

            'ログインユーザーIDを取得する。
            Dim commonChk As New CommonCheck
            commonChk.SetURL(Me, Ninsyou.GetUserID())

            If ViewState.Item("strKameitenCd") <> String.Empty Then
                Dim dtCreateDataSource As Data.DataTable
                'データ取得
                dtCreateDataSource = CreateDataSource(ViewState.Item("strKameitenCd"))
                If dtCreateDataSource.Rows.Count > 0 Then
                    '加盟店コード
                    Me.tbxKameiTenCd.Text = ViewState.Item("strKameitenCd")
                    '加盟店名
                    Me.tbxKameiTenName.Text = ViewState("strKameiTenName")
                    '加盟店カナ
                    Me.tbxKameiTenKana.Text = ViewState("strKameiTenKana")
                    '担当者
                    Me.tbxTantou.Text = ViewState("strTantou")

                    '==================2012/03/28 車龍 405721案件の対応 追加↓==============================
                    '「取消」をセットする
                    Call Me.SetTorikesi()
                    '==================2012/03/28 車龍 405721案件の対応 追加↑==============================

                    '左側GRIDVIEW
                    grdBodyLeft.DataSource = dtCreateDataSource
                    grdBodyLeft.DataBind()

                    '右側GRIDVIEW
                    grdBodyRight.DataSource = dtCreateDataSource
                    grdBodyRight.DataBind()

                    '仕様を設定
                    SetStyle()

                    '依頼日の降順
                    btnSortIraiDate2.ForeColor = Drawing.Color.IndianRed

                Else
                    'メッセージ
                    Context.Items("strFailureMsg") = Messages.Instance.MSG020E
                    Server.Transfer("CommonErr.aspx")
                End If
            Else
                'メッセージ
                Context.Items("strFailureMsg") = Messages.Instance.MSG2030E
                Server.Transfer("CommonErr.aspx")
            End If
        End If
        btnTojiru.Attributes.Add("onclick", "window.close();")
    End Sub

    ''' <summary> javascript </summary>
    Private Sub MakeJavaScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "CheckScriptBlock"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("var objWin = window;")
            .AppendLine("objWin.name = 'earthMainWindow'")
            .AppendLine("initPage();")
            .AppendLine("var activeRow=null;")
            .AppendLine("var defaultColor='';")
            .AppendLine("var motoRow=-1;")
            '行変色
            .AppendLine("function onListSelected(obj,rowNo){")
            .AppendLine("   if(!activeRow){")
            .AppendLine("       activeRow=obj;")
            .AppendLine("       defaultColor=activeRow.style.backgroundColor;")
            .AppendLine("       if (parseInt(rowNo)%2 == 0){")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].nextSibling.style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].nextSibling.style.backgroundColor='pink';")
            .AppendLine("       }else{")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].previousSibling.style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].previousSibling.style.backgroundColor='pink';")
            .AppendLine("       }")
            .AppendLine("       motoRow = rowNo;")
            .AppendLine("   }")
            .AppendLine("   else{")
            .AppendLine("       if (parseInt(motoRow)%2 == 0){")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor=defaultColor;")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor=defaultColor;")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[motoRow].nextSibling.style.backgroundColor=defaultColor;")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[motoRow].nextSibling.style.backgroundColor=defaultColor;")
            .AppendLine("       }else{")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor=defaultColor;")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor=defaultColor;")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[motoRow].previousSibling.style.backgroundColor=defaultColor;")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[motoRow].previousSibling.style.backgroundColor=defaultColor;")
            .AppendLine("       }")
            .AppendLine("       activeRow=obj;")
            .AppendLine("       defaultColor=activeRow.style.backgroundColor;")
            .AppendLine("       if (parseInt(rowNo)%2 == 0){")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].nextSibling.style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].nextSibling.style.backgroundColor='pink';")
            .AppendLine("       }else{")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyRight.ClientID + "').childNodes[0].childNodes[rowNo].previousSibling.style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdBodyLeft.ClientID + "').childNodes[0].childNodes[rowNo].previousSibling.style.backgroundColor='pink';")
            .AppendLine("       }")
            .AppendLine("       motoRow = rowNo;")
            .AppendLine("   }")
            .AppendLine("}")

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

            .AppendLine("   function fncSetScroll(){")
            .AppendLine("       var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("       document.getElementById('" & Me.hidScroll.ClientID & "').value = divHscroll.scrollLeft;")
            .AppendLine("   }")

            .AppendLine("</script>")
        End With

        ClientScript.RegisterStartupScript(csType, csName, csScript.ToString)

    End Sub

    ''' <summary> 右側GRIDVIEW設定 </summary>
    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyRight.RowDataBound
        '行変色
        e.Row.Attributes.Add("onclick", "onListSelected(this," & e.Row.RowIndex & ");")
    End Sub

    ''' <summary> 左側GRIDVIEW設定 </summary>
    Private Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound

        e.Row.Attributes.Add("onclick", "onListSelected(this," & e.Row.RowIndex & ");")

    End Sub

    ''' <summary> データを取得 </summary>
    Public Function CreateDataSource(ByVal strKameitenCd As String) As Data.DataTable

        'インスタンスの生成
        Dim dtBukkenJyouhouInquiryDataTable As BukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTableDataTable
        'データを取得
        dtBukkenJyouhouInquiryDataTable = bukkenJyouhouInquiryLogic.GetBukkenJyouhouInfo(strKameitenCd)

        ViewState("dtBukkenJyouhouInquiryDataTable") = dtBukkenJyouhouInquiryDataTable

        Return ChangeStyle(dtBukkenJyouhouInquiryDataTable)

    End Function

    ''' <summary> 仕様を設定 </summary>
    Private Sub SetStyle()
        '行色を設定
        For t As Integer = 2 To Me.grdBodyLeft.Rows.Count - 1 Step 4
            grdBodyLeft.Rows(t).BackColor = Drawing.Color.LightCyan
            grdBodyLeft.Rows(t + 1).BackColor = Drawing.Color.LightCyan
            grdBodyRight.Rows(t).BackColor = Drawing.Color.LightCyan
            grdBodyRight.Rows(t + 1).BackColor = Drawing.Color.LightCyan
        Next

        '様式を設定
        For intIndex As Integer = 0 To Me.grdBodyRight.Rows.Count - 1 Step 2
            Me.grdBodyRight.Rows(intIndex).Cells(0).RowSpan = 2
            Me.grdBodyRight.Rows(intIndex).Cells(5).RowSpan = 2
            Me.grdBodyRight.Rows(intIndex + 1).Cells(0).Visible = False
            Me.grdBodyRight.Rows(intIndex + 1).Cells(5).Visible = False
        Next
        For intIndex As Integer = 0 To Me.grdBodyLeft.Rows.Count - 1
            Me.grdBodyRight.Rows(intIndex).Cells(9).HorizontalAlign = HorizontalAlign.Right
            Me.grdBodyRight.Rows(intIndex).Cells(9).Attributes.Add("style", "padding-right: 2pt;")
        Next

        '高を設定
        scrollHeight = (Me.grdBodyLeft.Rows.Count + 1) * 30 + 2
        If scrollHeight <= 300 Then
            Me.divHiddenMeisaiV.Style.Add("display", "none")
        Else
            Me.divBodyLeft.Style.Add("height", "301px")
            Me.divBodyRight.Style.Add("height", "301px")
        End If
        Me.tblDivHMV.Height = scrollHeight

        '幅を設定
        scrollWidth = Me.grdBodyRight.Width.Value
        Me.tblDivHMH.Width = scrollWidth

        '画面仕様設定
        Me.divHiddenMeisaiV.Attributes.Add("onscroll", "fncScrollV();")
        Me.grdBodyLeft.Attributes.Add("onmousewheel", "wheel();")
        Me.grdBodyRight.Attributes.Add("onmousewheel", "wheel();")
        Me.divHiddenMeisaiH.Attributes.Add("onscroll", "fncScrollH();")

    End Sub

    ''' <summary> データを処理 </summary>
    Public Function ChangeStyle(ByVal dtBukkenJyouhouInquiryDataTable As DataTable) As Data.DataTable

        Dim dtDataSource As New DataTable
        Dim drTemp As DataRow
        Dim intCount As Integer

        '行を取得
        intCount = dtBukkenJyouhouInquiryDataTable.Rows.Count
        If intCount > 0 Then
            '加盟店名
            ViewState("strKameiTenName") = dtBukkenJyouhouInquiryDataTable.Rows(0).Item("kameiten_mei1").ToString
            '加盟店カナ
            ViewState("strKameiTenKana") = dtBukkenJyouhouInquiryDataTable.Rows(0).Item("tenmei_kana1").ToString
            '担当者
            ViewState("strTantou") = dtBukkenJyouhouInquiryDataTable.Rows(0).Item("eigyou_tantousya_mei").ToString

            '列の生成
            For intColCount As Integer = 0 To 12
                dtDataSource.Columns.Add(New DataColumn("col" & intColCount.ToString, GetType(String)))
            Next

            'データ生成
            For intRowCount As Integer = 0 To intCount - 1
                drTemp = dtDataSource.NewRow
                With drTemp
                    .Item(0) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("hosyousyo_no").ToString
                    .Item(1) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("bukken_jyuusyo").ToString
                    .Item(2) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("irai_date").ToString
                    .Item(3) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("tys_jissi_date").ToString
                    .Item(4) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("tys_hkks_hak_date").ToString
                    .Item(5) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("ks_siyou").ToString
                    .Item(6) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("kouhou").ToString
                    .Item(7) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("kairy_koj_date").ToString
                    .Item(8) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("koj_hkks_hassou_date").ToString
                    .Item(9) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("hosyousyo_hak_jyky").ToString
                    If dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("uriagegaku").ToString = "" Then
                        .Item(10) = 0
                    Else
                        .Item(10) = FormatNumber(Int(dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("uriagegaku").ToString), 0)
                    End If
                    .Item(11) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("tys_kaisya_cd").ToString
                    .Item(12) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("koj_gaisya_cd").ToString

                End With
                dtDataSource.Rows.Add(drTemp)

                drTemp = dtDataSource.NewRow
                With drTemp
                    .Item(0) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("sesyu_mei").ToString
                    .Item(1) = String.Empty
                    .Item(2) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("syoudakusyo_tys_date").ToString
                    .Item(3) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("keikakusyo_sakusei_date").ToString
                    .Item(4) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("tantousya_mei").ToString
                    .Item(5) = String.Empty
                    .Item(6) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("kairy_koj_kanry_yotei_date").ToString
                    .Item(7) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("kairy_koj_sokuhou_tyk_date").ToString
                    .Item(8) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("kouji_tantousya_mei").ToString
                    .Item(9) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("hosyousyo_hak_date").ToString
                    If dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("nyukingaku").ToString = "" Then
                        .Item(10) = 0
                    Else
                        .Item(10) = FormatNumber(Int(dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("nyukingaku").ToString), 0)
                    End If
                    .Item(11) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("tys_kaisya_mei").ToString
                    .Item(12) = dtBukkenJyouhouInquiryDataTable.Rows(intRowCount).Item("koj_gaisya_mei").ToString

                End With
                dtDataSource.Rows.Add(drTemp)
            Next
        End If

        Return dtDataSource

    End Function

    ''' <summary>画面タイトルに並び順をクリック時</summary>
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSortHosyousyoNo1.Click, _
                                                                                           btnSortHosyousyoNo2.Click, _
                                                                                           btnSortSesyuMei1.Click, _
                                                                                           btnSortSesyuMei2.Click, _
                                                                                           btnSortBukkenJyuusyo1.Click, _
                                                                                           btnSortBukkenJyuusyo2.Click, _
                                                                                           btnSortIraiDate1.Click, _
                                                                                           btnSortIraiDate2.Click, _
                                                                                           btnSortTysJissiDate1.Click, _
                                                                                           btnSortTysJissiDate2.Click, _
                                                                                           btnSortTysHkksHakDate1.Click, _
                                                                                           btnSortTysHkksHakDate2.Click, _
                                                                                           btnSortKsSiyou1.Click, _
                                                                                           btnSortKsSiyou2.Click, _
                                                                                           btnSortKouhou1.Click, _
                                                                                           btnSortKouhou2.Click, _
                                                                                           btnSortKairy_koj_date1.Click, _
                                                                                           btnSortKairy_koj_date2.Click, _
                                                                                           btnSortKoj_hkks_hassou_date1.Click, _
                                                                                           btnSortKoj_hkks_hassou_date2.Click, _
                                                                                           btnSortHosyousyo_hak_jyky1.Click, _
                                                                                           btnSortHosyousyo_hak_jyky2.Click, _
                                                                                           btnSortUriagegaku1.Click, _
                                                                                           btnSortUriagegaku2.Click, _
                                                                                           btnSortSyoudakusyo_tys_date1.Click, _
                                                                                           btnSortSyoudakusyo_tys_date2.Click, _
                                                                                           btnSortKeikakusyo_sakusei_date1.Click, _
                                                                                           btnSortKeikakusyo_sakusei_date2.Click, _
                                                                                           btnSortTantousya_mei1.Click, _
                                                                                           btnSortTantousya_mei2.Click, _
                                                                                           btnSortKairy_koj_kanry_yotei_date1.Click, _
                                                                                           btnSortKairy_koj_kanry_yotei_date2.Click, _
                                                                                           btnSortKairy_koj_sokuhou_tyk_date1.Click, _
                                                                                           btnSortKairy_koj_sokuhou_tyk_date2.Click, _
                                                                                           btnSortKouji_tantousya_mei1.Click, _
                                                                                           btnSortKouji_tantousya_mei2.Click, _
                                                                                           btnSortHosyousyo_hak_date1.Click, _
                                                                                           btnSortHosyousyo_hak_date2.Click, _
                                                                                           btnSortNyukingaku1.Click, _
                                                                                           btnSortNyukingaku2.Click, _
                                                                                           btnKaisyaCd1.Click, _
                                                                                           btnKaisyaCd2.Click, _
                                                                                           btnGaisyaCd1.Click, _
                                                                                           btnGaisyaCd2.Click, _
                                                                                           btnKaisyaMei1.Click, _
                                                                                           btnKaisyaMei2.Click, _
                                                                                           btnGaisyaMei1.Click, _
                                                                                           btnGaisyaMei2.Click

        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        btnSortHosyousyoNo1.ForeColor = Drawing.Color.SkyBlue
        btnSortHosyousyoNo2.ForeColor = Drawing.Color.SkyBlue
        btnSortSesyuMei1.ForeColor = Drawing.Color.SkyBlue
        btnSortSesyuMei2.ForeColor = Drawing.Color.SkyBlue
        btnSortBukkenJyuusyo1.ForeColor = Drawing.Color.SkyBlue
        btnSortBukkenJyuusyo2.ForeColor = Drawing.Color.SkyBlue
        btnSortIraiDate1.ForeColor = Drawing.Color.SkyBlue
        btnSortIraiDate2.ForeColor = Drawing.Color.SkyBlue
        btnSortTysJissiDate1.ForeColor = Drawing.Color.SkyBlue
        btnSortTysJissiDate2.ForeColor = Drawing.Color.SkyBlue
        btnSortTysHkksHakDate1.ForeColor = Drawing.Color.SkyBlue
        btnSortTysHkksHakDate2.ForeColor = Drawing.Color.SkyBlue
        btnSortKsSiyou1.ForeColor = Drawing.Color.SkyBlue
        btnSortKsSiyou2.ForeColor = Drawing.Color.SkyBlue
        btnSortKouhou1.ForeColor = Drawing.Color.SkyBlue
        btnSortKouhou2.ForeColor = Drawing.Color.SkyBlue
        btnSortKairy_koj_date1.ForeColor = Drawing.Color.SkyBlue
        btnSortKairy_koj_date2.ForeColor = Drawing.Color.SkyBlue
        btnSortKoj_hkks_hassou_date1.ForeColor = Drawing.Color.SkyBlue
        btnSortKoj_hkks_hassou_date2.ForeColor = Drawing.Color.SkyBlue
        btnSortHosyousyo_hak_jyky1.ForeColor = Drawing.Color.SkyBlue
        btnSortHosyousyo_hak_jyky2.ForeColor = Drawing.Color.SkyBlue
        btnSortUriagegaku1.ForeColor = Drawing.Color.SkyBlue
        btnSortUriagegaku2.ForeColor = Drawing.Color.SkyBlue
        btnSortSyoudakusyo_tys_date1.ForeColor = Drawing.Color.SkyBlue
        btnSortSyoudakusyo_tys_date2.ForeColor = Drawing.Color.SkyBlue
        btnSortKeikakusyo_sakusei_date1.ForeColor = Drawing.Color.SkyBlue
        btnSortKeikakusyo_sakusei_date2.ForeColor = Drawing.Color.SkyBlue
        btnSortTantousya_mei1.ForeColor = Drawing.Color.SkyBlue
        btnSortTantousya_mei2.ForeColor = Drawing.Color.SkyBlue
        btnSortKairy_koj_kanry_yotei_date1.ForeColor = Drawing.Color.SkyBlue
        btnSortKairy_koj_kanry_yotei_date2.ForeColor = Drawing.Color.SkyBlue
        btnSortKairy_koj_sokuhou_tyk_date1.ForeColor = Drawing.Color.SkyBlue
        btnSortKairy_koj_sokuhou_tyk_date2.ForeColor = Drawing.Color.SkyBlue
        btnSortKouji_tantousya_mei1.ForeColor = Drawing.Color.SkyBlue
        btnSortKouji_tantousya_mei2.ForeColor = Drawing.Color.SkyBlue
        btnSortHosyousyo_hak_date1.ForeColor = Drawing.Color.SkyBlue
        btnSortHosyousyo_hak_date2.ForeColor = Drawing.Color.SkyBlue
        btnSortNyukingaku1.ForeColor = Drawing.Color.SkyBlue
        btnSortNyukingaku2.ForeColor = Drawing.Color.SkyBlue
        btnKaisyaCd1.ForeColor = Drawing.Color.SkyBlue
        btnKaisyaCd2.ForeColor = Drawing.Color.SkyBlue
        btnGaisyaCd1.ForeColor = Drawing.Color.SkyBlue
        btnGaisyaCd2.ForeColor = Drawing.Color.SkyBlue
        btnKaisyaMei1.ForeColor = Drawing.Color.SkyBlue
        btnKaisyaMei2.ForeColor = Drawing.Color.SkyBlue
        btnGaisyaMei1.ForeColor = Drawing.Color.SkyBlue
        btnGaisyaMei2.ForeColor = Drawing.Color.SkyBlue

        '画面にソート順をクリック時
        Select Case CType(sender, LinkButton).ID
            Case btnSortHosyousyoNo1.ID
                strSort = "hosyousyo_no"
                strUpDown = "ASC"
                btnSortHosyousyoNo1.ForeColor = Drawing.Color.IndianRed
            Case btnSortHosyousyoNo2.ID
                strSort = "hosyousyo_no"
                strUpDown = "DESC"
                btnSortHosyousyoNo2.ForeColor = Drawing.Color.IndianRed
            Case btnSortSesyuMei1.ID
                strSort = "sesyu_mei"
                strUpDown = "ASC"
                btnSortSesyuMei1.ForeColor = Drawing.Color.IndianRed
            Case btnSortSesyuMei2.ID
                strSort = "sesyu_mei"
                strUpDown = "DESC"
                btnSortSesyuMei2.ForeColor = Drawing.Color.IndianRed
            Case btnSortBukkenJyuusyo1.ID
                strSort = "bukken_jyuusyo"
                strUpDown = "ASC"
                btnSortBukkenJyuusyo1.ForeColor = Drawing.Color.IndianRed
            Case btnSortBukkenJyuusyo2.ID
                strSort = "bukken_jyuusyo"
                strUpDown = "DESC"
                btnSortBukkenJyuusyo2.ForeColor = Drawing.Color.IndianRed
            Case btnSortIraiDate1.ID
                strSort = "irai_date"
                strUpDown = "ASC"
                btnSortIraiDate1.ForeColor = Drawing.Color.IndianRed
            Case btnSortIraiDate2.ID
                strSort = "irai_date"
                strUpDown = "DESC"
                btnSortIraiDate2.ForeColor = Drawing.Color.IndianRed
            Case btnSortTysJissiDate1.ID
                strSort = "tys_jissi_date"
                strUpDown = "ASC"
                btnSortTysJissiDate1.ForeColor = Drawing.Color.IndianRed
            Case btnSortTysJissiDate2.ID
                strSort = "tys_jissi_date"
                strUpDown = "DESC"
                btnSortTysJissiDate2.ForeColor = Drawing.Color.IndianRed
            Case btnSortTysHkksHakDate1.ID
                strSort = "tys_hkks_hak_date"
                strUpDown = "ASC"
                btnSortTysHkksHakDate1.ForeColor = Drawing.Color.IndianRed
            Case btnSortTysHkksHakDate2.ID
                strSort = "tys_hkks_hak_date"
                strUpDown = "DESC"
                btnSortTysHkksHakDate2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKsSiyou1.ID
                strSort = "ks_siyou"
                strUpDown = "ASC"
                btnSortKsSiyou1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKsSiyou2.ID
                strSort = "ks_siyou"
                strUpDown = "DESC"
                btnSortKsSiyou2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKouhou1.ID
                strSort = "kouhou"
                strUpDown = "ASC"
                btnSortKouhou1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKouhou2.ID
                strSort = "kouhou"
                strUpDown = "DESC"
                btnSortKouhou2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKairy_koj_date1.ID
                strSort = "kairy_koj_date"
                strUpDown = "ASC"
                btnSortKairy_koj_date1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKairy_koj_date2.ID
                strSort = "kairy_koj_date"
                strUpDown = "DESC"
                btnSortKairy_koj_date2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKoj_hkks_hassou_date1.ID
                strSort = "koj_hkks_hassou_date"
                strUpDown = "ASC"
                btnSortKoj_hkks_hassou_date1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKoj_hkks_hassou_date2.ID
                strSort = "koj_hkks_hassou_date"
                strUpDown = "DESC"
                btnSortKoj_hkks_hassou_date2.ForeColor = Drawing.Color.IndianRed
            Case btnSortHosyousyo_hak_jyky1.ID
                strSort = "hosyousyo_hak_jyky"
                strUpDown = "ASC"
                btnSortHosyousyo_hak_jyky1.ForeColor = Drawing.Color.IndianRed
            Case btnSortHosyousyo_hak_jyky2.ID
                strSort = "hosyousyo_hak_jyky"
                strUpDown = "DESC"
                btnSortHosyousyo_hak_jyky2.ForeColor = Drawing.Color.IndianRed
            Case btnSortUriagegaku1.ID
                strSort = "uriagegaku"
                strUpDown = "ASC"
                btnSortUriagegaku1.ForeColor = Drawing.Color.IndianRed
            Case btnSortUriagegaku2.ID
                strSort = "uriagegaku"
                strUpDown = "DESC"
                btnSortUriagegaku2.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyoudakusyo_tys_date1.ID
                strSort = "syoudakusyo_tys_date"
                strUpDown = "ASC"
                btnSortSyoudakusyo_tys_date1.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyoudakusyo_tys_date2.ID
                strSort = "syoudakusyo_tys_date"
                strUpDown = "DESC"
                btnSortSyoudakusyo_tys_date2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKeikakusyo_sakusei_date1.ID
                strSort = "keikakusyo_sakusei_date"
                strUpDown = "ASC"
                btnSortKeikakusyo_sakusei_date1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKeikakusyo_sakusei_date2.ID
                strSort = "keikakusyo_sakusei_date"
                strUpDown = "DESC"
                btnSortKeikakusyo_sakusei_date2.ForeColor = Drawing.Color.IndianRed
            Case btnSortTantousya_mei1.ID
                strSort = "tantousya_mei"
                strUpDown = "ASC"
                btnSortTantousya_mei1.ForeColor = Drawing.Color.IndianRed
            Case btnSortTantousya_mei2.ID
                strSort = "tantousya_mei"
                strUpDown = "DESC"
                btnSortTantousya_mei2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKairy_koj_kanry_yotei_date1.ID
                strSort = "kairy_koj_kanry_yotei_date"
                strUpDown = "ASC"
                btnSortKairy_koj_kanry_yotei_date1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKairy_koj_kanry_yotei_date2.ID
                strSort = "kairy_koj_kanry_yotei_date"
                strUpDown = "DESC"
                btnSortKairy_koj_kanry_yotei_date2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKairy_koj_sokuhou_tyk_date1.ID
                strSort = "kairy_koj_sokuhou_tyk_date"
                strUpDown = "ASC"
                btnSortKairy_koj_sokuhou_tyk_date1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKairy_koj_sokuhou_tyk_date2.ID
                strSort = "kairy_koj_sokuhou_tyk_date"
                strUpDown = "DESC"
                btnSortKairy_koj_sokuhou_tyk_date2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKouji_tantousya_mei1.ID
                strSort = "kouji_tantousya_mei"
                strUpDown = "ASC"
                btnSortKouji_tantousya_mei1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKouji_tantousya_mei2.ID
                strSort = "kouji_tantousya_mei"
                strUpDown = "DESC"
                btnSortKouji_tantousya_mei2.ForeColor = Drawing.Color.IndianRed
            Case btnSortHosyousyo_hak_date1.ID
                strSort = "hosyousyo_hak_date"
                strUpDown = "ASC"
                btnSortHosyousyo_hak_date1.ForeColor = Drawing.Color.IndianRed
            Case btnSortHosyousyo_hak_date2.ID
                strSort = "hosyousyo_hak_date"
                strUpDown = "DESC"
                btnSortHosyousyo_hak_date2.ForeColor = Drawing.Color.IndianRed
            Case btnSortNyukingaku1.ID
                strSort = "nyukingaku"
                strUpDown = "ASC"
                btnSortNyukingaku1.ForeColor = Drawing.Color.IndianRed
            Case btnSortNyukingaku2.ID
                strSort = "nyukingaku"
                strUpDown = "DESC"
                btnSortNyukingaku2.ForeColor = Drawing.Color.IndianRed

            Case btnKaisyaCd1.ID
                strSort = "tys_kaisya_cd"
                strUpDown = "ASC"
                btnKaisyaCd1.ForeColor = Drawing.Color.IndianRed
            Case btnKaisyaCd2.ID
                strSort = "tys_kaisya_cd"
                strUpDown = "DESC"
                btnKaisyaCd2.ForeColor = Drawing.Color.IndianRed
            Case btnGaisyaCd1.ID
                strSort = "koj_gaisya_cd"
                strUpDown = "ASC"
                btnGaisyaCd1.ForeColor = Drawing.Color.IndianRed
            Case btnGaisyaCd2.ID
                strSort = "koj_gaisya_cd"
                strUpDown = "DESC"
                btnGaisyaCd2.ForeColor = Drawing.Color.IndianRed
            Case btnKaisyaMei1.ID
                strSort = "tys_kaisya_mei"
                strUpDown = "ASC"
                btnKaisyaMei1.ForeColor = Drawing.Color.IndianRed
            Case btnKaisyaMei2.ID
                strSort = "tys_kaisya_mei"
                strUpDown = "DESC"
                btnKaisyaMei2.ForeColor = Drawing.Color.IndianRed
            Case btnGaisyaMei1.ID
                strSort = "koj_gaisya_mei"
                strUpDown = "ASC"
                btnGaisyaMei1.ForeColor = Drawing.Color.IndianRed
            Case btnGaisyaMei2.ID
                strSort = "koj_gaisya_mei"
                strUpDown = "DESC"
                btnGaisyaMei2.ForeColor = Drawing.Color.IndianRed
        End Select

        '画面データのソート順を設定する
        Dim dvKameitenInfo As Data.DataView = CType(ViewState("dtBukkenJyouhouInquiryDataTable"), Data.DataTable).DefaultView

        dvKameitenInfo.Sort = strSort & " " & strUpDown

        ChangeStyle(dvKameitenInfo.ToTable)

        Me.grdBodyLeft.DataSource = ChangeStyle(dvKameitenInfo.ToTable)
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = ChangeStyle(dvKameitenInfo.ToTable)
        Me.grdBodyRight.DataBind()

        '仕様を設定
        SetStyle()

        '画面横スクロール位置を設定する
        SetScroll()

    End Sub

    ''' <summary>横スクロール設定</summary>
    Public Sub SetScroll()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("divheadright.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
            .AppendLine("divbodyright.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
            .AppendLine("divHscroll.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
        End With
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetScroll", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' 「取消」をセットする
    ''' </summary>
    ''' <hidtory>2012/03/28 車龍 405721案件の対応 追加</hidtory>
    Private Sub SetTorikesi()

        'データを取得する
        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic
        Dim dtTorikesi As New Data.DataTable
        dtTorikesi = bukkenJyouhouInquiryLogic.GetTorikesi(ViewState("strKameitenCd").ToString.Trim)

        If (dtTorikesi.Rows.Count > 0) AndAlso (Not dtTorikesi.Rows(0).Item("torikesi").ToString.Trim.Equals("0")) Then

            Me.tbxTorikesi.Text = dtTorikesi.Rows(0).Item("torikesi").ToString.Trim & ":" & dtTorikesi.Rows(0).Item("meisyou").ToString.Trim

            '色をセットする
            Call Me.SetColor(Drawing.Color.Red)
        Else
            Me.tbxTorikesi.Text = String.Empty

            '色をセットする
            Call Me.SetColor(Drawing.Color.Black)
        End If

    End Sub

    ''' <summary>
    ''' 色をセットする
    ''' </summary>
    ''' <hidtory>2012/03/28 車龍 405721案件の対応 追加</hidtory>
    Private Sub SetColor(ByVal color As System.Drawing.Color)

        Me.tbxKameiTenCd.ForeColor = color
        Me.tbxKameiTenName.ForeColor = color
        Me.tbxTorikesi.ForeColor = color
        Me.tbxKameiTenKana.ForeColor = color
        Me.tbxTantou.ForeColor = color

    End Sub

End Class