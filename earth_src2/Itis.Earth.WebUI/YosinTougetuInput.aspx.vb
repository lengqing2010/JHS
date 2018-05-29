Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Partial Public Class YosinTougetuInput
    Inherits System.Web.UI.Page

    Private YosinJyouhouInputLogic As New YosinJyouhouInputLogic
    Protected scrollHeight As Integer = 0
    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ユーザーチェック
        Dim Ninsyou As New Ninsyou()
        If Ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")
        '権限チェック
        Dim user_info As New LoginUserInfo
        Dim jBn As New Jiban
        'ユーザー基本認証
        jBn.userAuth(user_info)
        If user_info Is Nothing Then
            'Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            'Server.Transfer("CommonErr.aspx")
        End If

        If (IsPostBack = False) Then
            '名寄先コード
            If Context.Items("nayose_cd") IsNot Nothing Then
                ViewState("nayose_cd") = Context.Items("nayose_cd")
                ViewState("nayose_mei") = Context.Items("nayose_mei")
                ViewState("strKameitenCd") = Context.Items("strKameitenCd")
                ViewState("modoru") = Context.Items("modoru")
            Else
                ViewState("nayose_cd") = Request.QueryString("strNayoseCd")
                ViewState("nayose_mei") = Request.QueryString("nayose_mei")
                ViewState("strKameitenCd") = Request.QueryString("strKameitenCd")
                ViewState("modoru") = Request.QueryString("modoru")
            End If
            ViewState("goukei") = 0
            'ViewState("nayose_cd") = "66150"
            'ViewState("nayose_mei") = "㈱マスターピース"
            tbxNayoseSakiCd.Text = ViewState("nayose_cd").ToString
            tbxNayoseSakiName1.Text = ViewState("nayose_mei").ToString
            tbxNayoseSakiCd.Attributes.Add("readonly", "true")
            tbxNayoseSakiName1.Attributes.Add("readonly", "true")
            Dim commonChk As New CommonCheck
            commonChk.SetURL(Me, Ninsyou.GetUserID())
            Akameiten_cd.Attributes.Add("onclick", "return fncSort('Akameiten_cd')")
            Dkameiten_cd.Attributes.Add("onclick", "return fncSort('Dkameiten_cd')")

            Akameiten_mei1.Attributes.Add("onclick", "return fncSort('Akameiten_mei1')")
            Dkameiten_mei1.Attributes.Add("onclick", "return fncSort('Dkameiten_mei1')")

            Atodouhuken_cd.Attributes.Add("onclick", "return fncSort('Atodouhuken_cd')")
            Dtodouhuken_cd.Attributes.Add("onclick", "return fncSort('Dtodouhuken_cd')")

            Ahosyousyo_no.Attributes.Add("onclick", "return fncSort('Ahosyousyo_no')")
            Dhosyousyo_no.Attributes.Add("onclick", "return fncSort('Dhosyousyo_no')")

            Asesyu_mei.Attributes.Add("onclick", "return fncSort('Asesyu_mei')")
            Dsesyu_mei.Attributes.Add("onclick", "return fncSort('Dsesyu_mei')")
            Airai_date.Attributes.Add("onclick", "return fncSort('Airai_date')")
            Dirai_date.Attributes.Add("onclick", "return fncSort('Dirai_date')")

            Acol1.Attributes.Add("onclick", "return fncSort('Acol1')")
            Dcol1.Attributes.Add("onclick", "return fncSort('Dcol1')")

            Acol2.Attributes.Add("onclick", "return fncSort('Acol2')")
            Dcol2.Attributes.Add("onclick", "return fncSort('Dcol2')")
            Auri_date.Attributes.Add("onclick", "return fncSort('Auri_date')")
            Duri_date.Attributes.Add("onclick", "return fncSort('Duri_date')")
            Asyoudakusyo_tys_date.Attributes.Add("onclick", "return fncSort('Asyoudakusyo_tys_date')")
            Dsyoudakusyo_tys_date.Attributes.Add("onclick", "return fncSort('Dsyoudakusyo_tys_date')")

            btnCHK.Attributes.Add("onclick", "fncClearWin(1);return false;")
            btnCLN.Attributes.Add("onclick", "fncClearWin(2);return false;")
            Akin.Attributes.Add("onclick", "return fncSort('Akin')")
            Dkin.Attributes.Add("onclick", "return fncSort('Dkin')")
            Asyouhin_mei.Attributes.Add("onclick", "return fncSort('Asyouhin_mei')")
            Dsyouhin_mei.Attributes.Add("onclick", "return fncSort('Dsyouhin_mei')")

            Kensaku("NotPostBack")
        Else
            scrollHeight = ViewState("scrollHeight")
        End If
        MakeJavascript()
    End Sub
    Sub Kensaku(ByVal strSort As String)
        If ViewState.Item("dtTable") Is Nothing Then
            Dim dtYosinMeisa As New DataTable
            dtYosinMeisa = YosinJyouhouInputLogic.GetYosinMeisai(ViewState("nayose_cd"), chkA.Checked, chkB.Checked, chkC.Checked, chk1.Checked, chk2.Checked).Tables(0)
            ViewState.Item("dtTable") = dtYosinMeisa
        End If

        Dim dv As New DataView
        dv = CType(ViewState.Item("dtTable"), DataTable).DefaultView
        If dv.Table.Rows.Count <> 0 Then

            dv.Sort = SortStyle(strSort)

        End If
        hidSort.Value = strSort
        grdItiranLeft.DataSource = CreateBodyData(dv.ToTable, "left")
        grdItiranLeft.DataBind()
        GridMeisaiStyle(grdItiranLeft, "left")
        grdItiranRight.DataSource = CreateBodyData(dv.ToTable, "right")
        grdItiranRight.DataBind()
        GridMeisaiStyle(grdItiranRight, "right")
        scrollHeight = grdItiranRight.Rows.Count * 42 + 1
        ViewState("scrollHeight") = scrollHeight
        '画面横スクロール位置を設定する
        SetScroll()
        tbxGoukei.Text = addFigure(ViewState("goukei").ToString)
    End Sub
    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("   function fncClearWin(str){")
            .AppendLine(" if (str==1){")
            .AppendLine("   document.getElementById('" & Me.chkA.ClientID & "').checked =true;")
            .AppendLine("   document.getElementById('" & Me.chkA.ClientID & "').checked =true;")
            .AppendLine("   document.getElementById('" & Me.chkB.ClientID & "').checked =true;")
            .AppendLine("   document.getElementById('" & Me.chkC.ClientID & "').checked =true;")
            .AppendLine("   document.getElementById('" & Me.chk1.ClientID & "').checked =true;")
            .AppendLine("   document.getElementById('" & Me.chk2.ClientID & "').checked =true;")
            .AppendLine("   }else{")
            .AppendLine("   document.getElementById('" & Me.chkA.ClientID & "').checked =false;")
            .AppendLine("   document.getElementById('" & Me.chkA.ClientID & "').checked =false;")
            .AppendLine("   document.getElementById('" & Me.chkB.ClientID & "').checked =false;")
            .AppendLine("   document.getElementById('" & Me.chkC.ClientID & "').checked =false;")
            .AppendLine("   document.getElementById('" & Me.chk1.ClientID & "').checked =false;")
            .AppendLine("   document.getElementById('" & Me.chk2.ClientID & "').checked =false;")
            .AppendLine("   }")
            .AppendLine("   }")
            .AppendLine("   function fncSetLineColor(obj,index){")
            .AppendLine("       var obj1 = objEBI('" + Me.grdItiranRight.ClientID + "').childNodes[0].childNodes[index] ")
            .AppendLine("       setSelectedLineColor(obj,obj1);")
            .AppendLine("   }")
            .AppendLine("   function fncScrollV(){")
            .AppendLine("       var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("       divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("       divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   }")

            .AppendLine("   function fncScrollH(){")
            .AppendLine("       var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("       divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("       divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function fncSetScroll(){")
            .AppendLine("       var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("       document.getElementById('" & Me.hidScroll.ClientID & "').value = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function wheel(event){")
            .AppendLine("       var delta = 0;")
            .AppendLine("       if(!event)")
            .AppendLine("           event = window.event;")
            .AppendLine("       if (event.wheelDelta){")
            .AppendLine("           delta = event.wheelDelta/120;")
            .AppendLine("           if (window.opera)")
            .AppendLine("               delta = -delta;")
            .AppendLine("       } else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("       if (delta)")
            .AppendLine("           handle(delta);")
            .AppendLine("   }")
            .AppendLine("   function handle(delta){")
            .AppendLine("      var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("      if (delta < 0){")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("      }else{")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("      }")
            .AppendLine("   }")
            .Append("function fncSort(str){" & vbCrLf)
            .Append("fncSetScroll();" & vbCrLf)
            .Append("eval('document.all.'+'" & hidSort.ClientID & "').value=str;" & vbCrLf)
            .Append("}" & vbCrLf)

            
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub
    Sub GridMeisaiStyle(ByVal dtBodyTable As GridView, ByVal bln As String)
        Dim rowCount As Integer
        Dim strTemp As String = ""
        Dim strBln As String = ""

        For rowCount = 0 To dtBodyTable.Rows.Count - 1
            If bln = "left" Then
                grdItiranLeft.Rows(rowCount).Cells(0).Attributes.Add("style", "width:66px;white-space:normal;")

                grdItiranLeft.Rows(rowCount).Cells(1).Attributes.Add("style", "width:100px;white-space:normal;")
                grdItiranLeft.Rows(rowCount).Cells(2).Attributes.Add("style", "width:49px;white-space:normal;")
                grdItiranLeft.Rows(rowCount).Cells(3).Attributes.Add("style", "width:84px;white-space:normal;")
                grdItiranLeft.Rows(rowCount).Cells(4).Attributes.Add("style", "width:135px;white-space:normal;")
                grdItiranLeft.Rows(rowCount).Cells(5).Attributes.Add("style", "width:42px;white-space:normal;")
                grdItiranLeft.Rows(rowCount).Cells(6).Attributes.Add("style", "width:42px;white-space:normal;")
                grdItiranLeft.Rows(rowCount).Cells(7).Attributes.Add("style", "width:70px;white-space:normal;")
                grdItiranLeft.Rows(rowCount).Attributes.Add("style", "height:42px;")
            Else
                grdItiranRight.Rows(rowCount).Attributes.Add("style", "height:42px;")


               
                grdItiranRight.Rows(rowCount).Cells(0).Attributes.Add("style", "width:72px;white-space:normal;")

                grdItiranRight.Rows(rowCount).Cells(1).Attributes.Add("style", "width:84px;text-align:right;white-space:normal;")
                grdItiranRight.Rows(rowCount).Cells(2).Attributes.Add("style", "width:130px;white-space:normal;")
                grdItiranRight.Rows(rowCount).Attributes.Add("onclick", "setSelectedLineColor(" & grdItiranLeft.Rows(rowCount).ClientID & "," & grdItiranRight.Rows(rowCount).ClientID & ")")
                grdItiranLeft.Rows(rowCount).Attributes.Add("onclick", "setSelectedLineColor(" & grdItiranLeft.Rows(rowCount).ClientID & "," & grdItiranRight.Rows(rowCount).ClientID & ")")
            End If
        Next




    End Sub
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
    Function CreateBodyData(ByVal dtBodyTable As DataTable, ByVal bln As String) As DataTable
        Dim rowCount As Integer = 0
        Dim colCount As Integer = 0
        Dim goukei As Integer = 0
        Dim dtBody As New DataTable
        Dim drTemp As DataRow
        If bln = "left" Then
            For colCount = 0 To 7
                dtBody.Columns.Add(New DataColumn(colCount.ToString, GetType(String)))
            Next

            For rowCount = 0 To dtBodyTable.Rows.Count - 1
                drTemp = dtBody.NewRow
                With drTemp
                    .Item(0) = TrimNull(dtBodyTable.Rows(rowCount).Item("kameiten_cd"))
                    .Item(1) = TrimNull(dtBodyTable.Rows(rowCount).Item("kameiten_mei1"))
                    .Item(2) = TrimNull(dtBodyTable.Rows(rowCount).Item("todouhuken_mei"))
                    .Item(3) = TrimNull(dtBodyTable.Rows(rowCount).Item("hosyousyo_no"))
                    .Item(4) = TrimNull(dtBodyTable.Rows(rowCount).Item("sesyu_mei"))
                    .Item(5) = TrimNull(dtBodyTable.Rows(rowCount).Item("col1"))
                    .Item(6) = TrimNull(dtBodyTable.Rows(rowCount).Item("col2"))
                    .Item(7) = Replace(Replace(TrimDate(dtBodyTable.Rows(rowCount).Item("uri_date")), "1900/01/01", ""), "9999/12/31", "")
                End With
                dtBody.Rows.Add(drTemp)
            Next

        Else
            For colCount = 0 To 3
                dtBody.Columns.Add(New DataColumn(colCount.ToString, GetType(String)))
            Next

            For rowCount = 0 To dtBodyTable.Rows.Count - 1
                drTemp = dtBody.NewRow
                With drTemp
                    .Item(0) = Replace(TrimDate(dtBodyTable.Rows(rowCount).Item("syoudakusyo_tys_date")), "1900/01/01", "")
                    .Item(1) = addFigure(TrimNull(dtBodyTable.Rows(rowCount).Item("kin")))
                    .Item(2) = TrimNull(dtBodyTable.Rows(rowCount).Item("syouhin_mei"))
                    .Item(3) = Replace(TrimDate(dtBodyTable.Rows(rowCount).Item("irai_date")), "1900/01/01", "")
                End With
                dtBody.Rows.Add(drTemp)
                goukei = goukei + addFigure(TrimNull(dtBodyTable.Rows(rowCount).Item("kin")))
            Next
            ViewState("goukei") = goukei
        End If

        Return dtBody

    End Function
    Function TrimDate(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return ""
        Else
            If obj.ToString = "" Then
                Return ""
            Else
                Return CDate(obj).ToString("yyyy/MM/dd")
            End If
        End If
    End Function
    Function TrimNull(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return ""
        Else
            Return obj.ToString
        End If
    End Function
    ''' <summary>
    ''' 金額の年月日FROMを設定
    ''' </summary>
    ''' <param name="strZengetsuSaikenSetDate">前月債権額設定年月</param>
    ''' <remarks></remarks>
    Function setKingakuNengetuFrom(ByVal strZengetsuSaikenSetDate As String) As String

        If strZengetsuSaikenSetDate = String.Empty Then
            Return String.Empty
        Else
            Return CDate(strZengetsuSaikenSetDate).AddMonths(1).ToString("yyyy/MM") & "/01"
        End If

    End Function
    ''' <summary>
    ''' 金額の年月日TOを設定
    ''' </summary>
    ''' <param name="KingakuNengetuTo">金額の年月日TO</param>
    ''' <remarks></remarks>
    Function setKingakuNengetuTo(ByVal KingakuNengetuTo As Object) As String

        If IsDBNull(KingakuNengetuTo) Then
            Return String.Empty
        Else
            Return Left(KingakuNengetuTo, 10)
        End If

    End Function
    ''' <summary>
    ''' カンマを付与
    ''' </summary>
    ''' <param name="kingaku">金額</param>
    ''' <remarks></remarks>
    Function addFigure(ByVal kingaku As String) As String
        If kingaku <> "" Then
            Return Format(Convert.ToInt64(kingaku), "#,0")
        Else
            Return 0
        End If


    End Function
    ''' <summary>
    ''' 戻るボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Context.Items("nayose_cd") = ViewState("nayose_cd")
        Context.Items("strKameitenCd") = ViewState("strKameitenCd")
        Context.Items("modoru") = ViewState("modoru")
        Server.Transfer("YosinJyouhouInput.aspx")
        '加盟店与信情報画面へ遷移する
        'Server.Transfer("YosinJyouhouInput.aspx?strKameitenCd=" & ViewState("strKameitenCd") & "&nayose_cd=" & ViewState("nayose_cd"))

    End Sub

    Protected Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If chkA.Checked = False And chkB.Checked = False And chkC.Checked = False And chk1.Checked = False And chk2.Checked = False Then
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>alert('" & Replace(Messages.Instance.MSG006E, "区分", "種別、予定・実績") & "');</script>")

        Else
            ViewState.Item("dtTable") = Nothing
            Kensaku(hidSort.Value)
        End If

    End Sub
    Private Sub BtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Atodouhuken_cd.Click, Acol1.Click, Acol2.Click, Ahosyousyo_no.Click, Airai_date.Click, Akameiten_cd.Click, Akameiten_mei1.Click, Akin.Click, Asesyu_mei.Click, Asyoudakusyo_tys_date.Click, Asyouhin_mei.Click, Auri_date.Click, _
                                                                                            Dtodouhuken_cd.Click, Dcol1.Click, Dcol2.Click, Dhosyousyo_no.Click, Dirai_date.Click, Dkameiten_cd.Click, Dkameiten_mei1.Click, Dkin.Click, Dsesyu_mei.Click, Dsyoudakusyo_tys_date.Click, Dsyouhin_mei.Click, Duri_date.Click
        Atodouhuken_cd.ForeColor = Drawing.Color.SkyBlue
        Dtodouhuken_cd.ForeColor = Drawing.Color.SkyBlue
        Akameiten_cd.ForeColor = Drawing.Color.SkyBlue
        Dkameiten_cd.ForeColor = Drawing.Color.SkyBlue
        Akameiten_mei1.ForeColor = Drawing.Color.SkyBlue
        Dkameiten_mei1.ForeColor = Drawing.Color.SkyBlue
        Ahosyousyo_no.ForeColor = Drawing.Color.SkyBlue
        Dhosyousyo_no.ForeColor = Drawing.Color.SkyBlue
        Asesyu_mei.ForeColor = Drawing.Color.SkyBlue
        Dsesyu_mei.ForeColor = Drawing.Color.SkyBlue

        Airai_date.ForeColor = Drawing.Color.SkyBlue
        Dirai_date.ForeColor = Drawing.Color.SkyBlue
        Acol1.ForeColor = Drawing.Color.SkyBlue
        Dcol1.ForeColor = Drawing.Color.SkyBlue
        Acol2.ForeColor = Drawing.Color.SkyBlue
        Dcol2.ForeColor = Drawing.Color.SkyBlue

        Auri_date.ForeColor = Drawing.Color.SkyBlue
        Duri_date.ForeColor = Drawing.Color.SkyBlue
        Asyoudakusyo_tys_date.ForeColor = Drawing.Color.SkyBlue
        Dsyoudakusyo_tys_date.ForeColor = Drawing.Color.SkyBlue

        Akin.ForeColor = Drawing.Color.SkyBlue
        Dkin.ForeColor = Drawing.Color.SkyBlue
        Asyouhin_mei.ForeColor = Drawing.Color.SkyBlue
        Dsyouhin_mei.ForeColor = Drawing.Color.SkyBlue
        Select Case hidSort.Value
            Case "Akameiten_cd"
                Akameiten_cd.ForeColor = Drawing.Color.IndianRed
            Case "Dkameiten_cd"
                Dkameiten_cd.ForeColor = Drawing.Color.IndianRed
            Case "Atodouhuken_cd"
                Atodouhuken_cd.ForeColor = Drawing.Color.IndianRed
            Case "Dtodouhuken_cd"
                Dtodouhuken_cd.ForeColor = Drawing.Color.IndianRed
            Case "Akameiten_mei1"
                Akameiten_mei1.ForeColor = Drawing.Color.IndianRed
            Case "Dkameiten_mei1"
                Dkameiten_mei1.ForeColor = Drawing.Color.IndianRed
            Case "Ahosyousyo_no"
                Ahosyousyo_no.ForeColor = Drawing.Color.IndianRed
            Case "Dhosyousyo_no"
                Dhosyousyo_no.ForeColor = Drawing.Color.IndianRed
            Case "Asesyu_mei"
                Asesyu_mei.ForeColor = Drawing.Color.IndianRed
            Case "Dsesyu_mei"
                Dsesyu_mei.ForeColor = Drawing.Color.IndianRed

            Case "Airai_date"
                Airai_date.ForeColor = Drawing.Color.IndianRed
            Case "Dirai_date"
                Dirai_date.ForeColor = Drawing.Color.IndianRed

            Case "Acol1"
                Acol1.ForeColor = Drawing.Color.IndianRed
            Case "Dcol1"
                Dcol1.ForeColor = Drawing.Color.IndianRed
            Case "Acol2"
                Acol2.ForeColor = Drawing.Color.IndianRed
            Case "Dcol2"
                Dcol2.ForeColor = Drawing.Color.IndianRed
            Case "Auri_date"
                Auri_date.ForeColor = Drawing.Color.IndianRed
            Case "Duri_date"
                Duri_date.ForeColor = Drawing.Color.IndianRed
            Case "Asyoudakusyo_tys_date"
                Asyoudakusyo_tys_date.ForeColor = Drawing.Color.IndianRed
            Case "Dsyoudakusyo_tys_date"
                Dsyoudakusyo_tys_date.ForeColor = Drawing.Color.IndianRed

            Case "Akin"
                Akin.ForeColor = Drawing.Color.IndianRed
            Case "Dkin"
                Dkin.ForeColor = Drawing.Color.IndianRed

            Case "Asyouhin_mei"
                Asyouhin_mei.ForeColor = Drawing.Color.IndianRed
            Case "Dsyouhin_mei"
                Dsyouhin_mei.ForeColor = Drawing.Color.IndianRed
        End Select

        Kensaku(hidSort.Value)

    End Sub
    Function SortStyle(ByVal inStr As String) As String
        If inStr = "NotPostBack" Then
            SortStyle = "uri_date ASC,syoudakusyo_tys_date ASC,kameiten_cd ASC"
        Else
            If Left(inStr, 1) = "A" Then
                SortStyle = Right(inStr, Len(inStr) - 1) & " ASC"

            Else
                SortStyle = Right(inStr, Len(inStr) - 1) & " DESC"
            End If
        End If

    End Function

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCSV.Click
        Dim dt As New DataTable
        Dim dv As New DataView
        dv = CType(ViewState.Item("dtTable"), DataTable).DefaultView
        If dv.ToTable.Rows.Count <> 0 Then
            dv.Sort = SortStyle(hidSort.Value)
        End If

        dt = dv.ToTable

        Dim intCount As Integer = 0
        If dt.Rows.Count > 0 Then
            'CSVファイル名設定
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("Yoshinmeisai").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSVファイルヘッダ設定
            writer.WriteLine(EarthConst.conYosinCsvHeader)

            'CSVファイル内容設定
            For intCount = 0 To dt.Rows.Count - 1

                writer.WriteLine(ReplaceNull(dt.Rows(intCount).Item("kameiten_cd"), ",", "、"), ReplaceNull(dt.Rows(intCount).Item("kameiten_mei1"), ",", "、") _
                , ReplaceNull(dt.Rows(intCount).Item("todouhuken_mei"), ",", "、"), ReplaceNull(dt.Rows(intCount).Item("hosyousyo_no"), ",", "、") _
 , ReplaceNull(dt.Rows(intCount).Item("sesyu_mei"), ",", "、"), ReplaceNull(dt.Rows(intCount).Item("col1"), ",", "、") _
  , ReplaceNull(dt.Rows(intCount).Item("col2"), ",", "、"), ReplaceNull(dt.Rows(intCount).Item("uri_date"), ",", "date") _
  , ReplaceNull(dt.Rows(intCount).Item("syoudakusyo_tys_date"), ",", "date"), ReplaceNull(dt.Rows(intCount).Item("kin"), ",", "、") _
  , ReplaceNull(dt.Rows(intCount).Item("syouhin_mei"), ",", "、"), ReplaceNull(dt.Rows(intCount).Item("irai_date"), ",", "date"))





            Next

            'CSVファイルダウンロード
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()
        Else
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If
    End Sub
    Function ReplaceNull(ByVal obj As Object, ByVal param1 As String, ByVal param2 As String) As String
        If IsDBNull(obj) Then
            Return ""
        Else
            If param2 = "date" Then
                Return Replace(Replace(TrimDate(obj), "1900/01/01", ""), "9999/12/31", "")
            Else
                Return Replace(obj.ToString, param1, param2)
            End If

        End If
    End Function
   
End Class