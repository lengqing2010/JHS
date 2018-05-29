Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Partial Public Class BukkenJyouhouList
    Inherits System.Web.UI.Page
    Private commonCheck As New CommonCheck
    Private EigyouJyouhouInquiryBL As New EigyouJyouhouInquiryLogic
    Dim user_info As New LoginUserInfo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '地盤画面共通クラス

        'ログインユーザーIDを取得する。
        ViewState("userId") = ninsyou.GetUserID()
        ' ユーザー基本認証
        jBn.userAuth(user_info)

        If ViewState("userId") = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E '（"該当ユーザーがありません。"）
            Server.Transfer("CommonErr.aspx")
        End If
        Dim dtDataSource As New DataTable
        If Not IsPostBack Then
            commonCheck.SetURL(Me, ViewState("userId"))
            Dim dtEigyouManKbn As EigyouJyouhouDataSet.eigyouManKbnDataTable
            dtEigyouManKbn = EigyouJyouhouInquiryBL.GetEigyouManKbnInfo(ViewState("userId"))
            If dtEigyouManKbn.Rows.Count > 0 Then
                '0:通常、1:新人
                ViewState("eigyouManKbn") = TrimNull(dtEigyouManKbn.Rows(0).Item("eigyou_man_kbn"))
                ViewState("busyo_cd") = TrimNull(dtEigyouManKbn.Rows(0).Item("busyo_cd"))
                ViewState("t_sansyou_busyo_cd") = TrimNull(dtEigyouManKbn.Rows(0).Item("t_sansyou_busyo_cd"))
                If ViewState("eigyouManKbn") = "" Then
                    ViewState("busyo_cd") = "0000"
                    ViewState("t_sansyou_busyo_cd") = "0000"
                    ViewState("eigyouManKbn") = "0"
                End If
            Else
                ViewState("busyo_cd") = "0000"
                ViewState("t_sansyou_busyo_cd") = "0000"
                ViewState("eigyouManKbn") = "0"
            End If
            

            Dim drTemp As DataRow
            '列の生成

            dtDataSource.Columns.Add(New DataColumn("kbn", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("Bangou", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("Irai", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("Keikakusyo", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("TyousaYotei", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("TyousaJissi", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("TyousaUriage", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("KoujiYotei", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("KoujiJissi", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("KoujiUriage", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("CHKKouji", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("KameitenCd", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("todoufuken", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("KeiretuCd", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("EigyousyoCd", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("sosikiLevel", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("BusyoCd", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("CHKBusyoCd", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("TantouEigyouID", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("kennsuu", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("loginUserId", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("busyo_cd", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("t_sansyou_busyo_cd", GetType(String)))
            dtDataSource.Columns.Add(New DataColumn("eigyouManKbn", GetType(String)))

            drTemp = dtDataSource.NewRow
            With drTemp
                drTemp.Item("kbn") = Request("kbn")
                drTemp.Item("Bangou") = Request("Bangou")
                drTemp.Item("Irai") = Request("Irai")
                drTemp.Item("Keikakusyo") = Request("Keikakusyo")
                drTemp.Item("TyousaYotei") = Request("TyousaYotei")
                drTemp.Item("TyousaJissi") = Request("TyousaJissi")
                drTemp.Item("TyousaUriage") = Request("TyousaUriage")
                drTemp.Item("KoujiYotei") = Request("KoujiYotei")
                drTemp.Item("KoujiJissi") = Request("KoujiJissi")
                drTemp.Item("KoujiUriage") = Request("KoujiUriage")
                drTemp.Item("CHKKouji") = Request("CHKKouji")
                drTemp.Item("KameitenCd") = Request("KameitenCd")
                drTemp.Item("todoufuken") = Request("todoufuken")
                drTemp.Item("KeiretuCd") = Request("KeiretuCd")
                drTemp.Item("EigyousyoCd") = Request("EigyousyoCd")
                drTemp.Item("SosikiLevel") = Request("SosikiLevel")
                drTemp.Item("BusyoCd") = Request("BusyoCd")
                drTemp.Item("CHKBusyoCd") = Request("CHKBusyoCd")
                drTemp.Item("TantouEigyouID") = Request("TantouEigyouID")
                drTemp.Item("kennsuu") = Request("kennsuu")
                drTemp.Item("loginUserId") = ViewState("userId")
                drTemp.Item("busyo_cd") = ViewState("busyo_cd")
                drTemp.Item("t_sansyou_busyo_cd") = ViewState("t_sansyou_busyo_cd")
                drTemp.Item("eigyouManKbn") = ViewState("eigyouManKbn")
            End With
            dtDataSource.Rows.Add(drTemp)
            ViewState.Item("dtDataSource") = dtDataSource
        Else

            dtDataSource = CType(ViewState.Item("dtDataSource"), DataTable)
            
            If IsNothing(ViewState.Item("kennsuu")) Then
                dtDataSource.Rows(0).Item("kennsuu") = hidkennsuu.Value
                ViewState.Item("kennsuu") = hidkennsuu.Value
                Dim BukkenJyouhouTableDataTable As New BukkenJyouhouDataSet.BukkenJyouhouTableDataTable
                Dim bukkenLogic As New BukkenJyouhouLogic
                BukkenJyouhouTableDataTable = bukkenLogic.GetBukkenJyouhouInfo(dtDataSource)
                ViewState.Item("dtTable") = BukkenJyouhouTableDataTable
                Dim intCount1 As Integer = bukkenLogic.GetBukkenJyouhouInfoCount(dtDataSource)
                ViewState.Item("intCount1") = intCount1
                ViewState.Item("btn") = ""
            End If

            '個数

            Dhosyousyo_no.ForeColor = Drawing.Color.IndianRed
            Dim dv As New DataView
            dv = CType(ViewState.Item("dtTable"), DataTable).DefaultView
            dv.Sort = "hosyousyo_no DESC"
            MeisaiStyle(dv.ToTable, CInt(ViewState.Item("intCount1")))
            If CInt(ViewState.Item("intCount1")) > 3000 Then
                btnCsv.Attributes.Add("onclick", "alert('該当件数が3千件を超えています。\n条件を変更して、再度検索してください。');return false;")
            End If
            Ahosyousyo_no.Attributes.Add("onclick", "return fncSort('Ahosyousyo_no')")
            Dhosyousyo_no.Attributes.Add("onclick", "return fncSort('Dhosyousyo_no')")
            Ahaki_syubetu.Attributes.Add("onclick", "return fncSort('Ahaki_syubetu')")
            Dhaki_syubetu.Attributes.Add("onclick", "return fncSort('Dhaki_syubetu')")
            Airai_date.Attributes.Add("onclick", "return fncSort('Airai_date')")
            Dirai_date.Attributes.Add("onclick", "return fncSort('Dirai_date')")
            Atys_kibou_date.Attributes.Add("onclick", "return fncSort('Atys_kibou_date')")
            Dtys_kibou_date.Attributes.Add("onclick", "return fncSort('Dtys_kibou_date')")
            Atys_houhou_mei_ryaku.Attributes.Add("onclick", "return fncSort('Atys_houhou_mei_ryaku')")
            Dtys_houhou_mei_ryaku.Attributes.Add("onclick", "return fncSort('Dtys_houhou_mei_ryaku')")
            Aks_siyou.Attributes.Add("onclick", "return fncSort('Aks_siyou')")
            Dks_siyou.Attributes.Add("onclick", "return fncSort('Dks_siyou')")
            Asyoudakusyo_tys_date.Attributes.Add("onclick", "return fncSort('Asyoudakusyo_tys_date')")
            Dsyoudakusyo_tys_date.Attributes.Add("onclick", "return fncSort('Dsyoudakusyo_tys_date')")
            Akairy_koj_kanry_yotei_date.Attributes.Add("onclick", "return fncSort('Akairy_koj_kanry_yotei_date')")
            Dkairy_koj_kanry_yotei_date.Attributes.Add("onclick", "return fncSort('Dkairy_koj_kanry_yotei_date')")
            Asesyu_mei.Attributes.Add("onclick", "return fncSort('Asesyu_mei')")
            Dsesyu_mei.Attributes.Add("onclick", "return fncSort('Dsesyu_mei')")
            Atys_jissi_date.Attributes.Add("onclick", "return fncSort('Atys_jissi_date')")
            Dtys_jissi_date.Attributes.Add("onclick", "return fncSort('Dtys_jissi_date')")
            Akairy_koj_date.Attributes.Add("onclick", "return fncSort('Akairy_koj_date')")
            Dkairy_koj_date.Attributes.Add("onclick", "return fncSort('Dkairy_koj_date')")
            Akameiten_cd.Attributes.Add("onclick", "return fncSort('Akameiten_cd')")
            Dkameiten_cd.Attributes.Add("onclick", "return fncSort('Dkameiten_cd')")
            Akameiten_mei1.Attributes.Add("onclick", "return fncSort('Akameiten_mei1')")
            Dkameiten_mei1.Attributes.Add("onclick", "return fncSort('Dkameiten_mei1')")
            Airai_tantousya_mei.Attributes.Add("onclick", "return fncSort('Airai_tantousya_mei')")
            Dirai_tantousya_mei.Attributes.Add("onclick", "return fncSort('Dirai_tantousya_mei')")

            Akairy_koj_syubetu.Attributes.Add("onclick", "return fncSort('Akairy_koj_syubetu')")
            Dkairy_koj_syubetu.Attributes.Add("onclick", "return fncSort('Dkairy_koj_syubetu')")
            Ahosyousyo_hak_date.Attributes.Add("onclick", "return fncSort('Ahosyousyo_hak_date')")
            Dhosyousyo_hak_date.Attributes.Add("onclick", "return fncSort('Dhosyousyo_hak_date')")
            Auri_date.Attributes.Add("onclick", "return fncSort('Auri_date')")
            Duri_date.Attributes.Add("onclick", "return fncSort('Duri_date')")
            Auri_date2.Attributes.Add("onclick", "return fncSort('Auri_date2')")
            Duri_date2.Attributes.Add("onclick", "return fncSort('Duri_date2')")

            tablhead.Visible = True
            tablhead1.Visible = True
            tablhead2.Visible = True
            btnClose.Visible = True
            btnCsv.Visible = True
        End If

        MakeJavaScript()
    End Sub
    Private Sub BtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
                                                            Ahaki_syubetu.Click, _
                                                            Ahosyousyo_hak_date.Click, _
                                                            Ahosyousyo_no.Click, _
                                                            Airai_date.Click, _
                                                            Airai_tantousya_mei.Click, _
                                                            Akairy_koj_date.Click, _
                                                            Akairy_koj_kanry_yotei_date.Click, _
                                                            Akairy_koj_syubetu.Click, _
                                                            Akameiten_cd.Click, _
                                                            Akameiten_mei1.Click, _
                                                            Aks_siyou.Click, _
                                                            Asesyu_mei.Click, _
                                                            Asyoudakusyo_tys_date.Click, _
                                                            Atys_houhou_mei_ryaku.Click, _
                                                            Atys_jissi_date.Click, _
                                                            Atys_kibou_date.Click, _
                                                            Auri_date.Click, _
                                                            Auri_date2.Click, _
                                                            Dhaki_syubetu.Click, _
                                                            Dhosyousyo_hak_date.Click, _
                                                            Dhosyousyo_no.Click, _
                                                            Dirai_date.Click, _
                                                            Dirai_tantousya_mei.Click, _
                                                            Dkairy_koj_date.Click, _
                                                            Dkairy_koj_kanry_yotei_date.Click, _
                                                            Dkairy_koj_syubetu.Click, _
                                                            Dkameiten_cd.Click, _
                                                            Dkameiten_mei1.Click, _
                                                            Dks_siyou.Click, _
                                                            Dsesyu_mei.Click, _
                                                            Dsyoudakusyo_tys_date.Click, _
                                                            Dtys_houhou_mei_ryaku.Click, _
                                                            Dtys_jissi_date.Click, _
                                                            Dtys_kibou_date.Click, _
                                                            Duri_date.Click, _
                                                            Duri_date2.Click

        Ahaki_syubetu.ForeColor = Drawing.Color.SkyBlue
        Ahosyousyo_hak_date.ForeColor = Drawing.Color.SkyBlue
        Ahosyousyo_no.ForeColor = Drawing.Color.SkyBlue
        Airai_date.ForeColor = Drawing.Color.SkyBlue
        Airai_tantousya_mei.ForeColor = Drawing.Color.SkyBlue
        Akairy_koj_date.ForeColor = Drawing.Color.SkyBlue
        Akairy_koj_kanry_yotei_date.ForeColor = Drawing.Color.SkyBlue
        Akairy_koj_syubetu.ForeColor = Drawing.Color.SkyBlue
        Akameiten_cd.ForeColor = Drawing.Color.SkyBlue
        Akameiten_mei1.ForeColor = Drawing.Color.SkyBlue
        Aks_siyou.ForeColor = Drawing.Color.SkyBlue
        Asesyu_mei.ForeColor = Drawing.Color.SkyBlue
        Asyoudakusyo_tys_date.ForeColor = Drawing.Color.SkyBlue
        Atys_houhou_mei_ryaku.ForeColor = Drawing.Color.SkyBlue
        Atys_jissi_date.ForeColor = Drawing.Color.SkyBlue
        Atys_kibou_date.ForeColor = Drawing.Color.SkyBlue
        Auri_date.ForeColor = Drawing.Color.SkyBlue
        Auri_date2.ForeColor = Drawing.Color.SkyBlue
        Dhaki_syubetu.ForeColor = Drawing.Color.SkyBlue
        Dhosyousyo_hak_date.ForeColor = Drawing.Color.SkyBlue
        Dhosyousyo_no.ForeColor = Drawing.Color.SkyBlue
        Dirai_date.ForeColor = Drawing.Color.SkyBlue
        Dirai_tantousya_mei.ForeColor = Drawing.Color.SkyBlue
        Dkairy_koj_date.ForeColor = Drawing.Color.SkyBlue
        Dkairy_koj_kanry_yotei_date.ForeColor = Drawing.Color.SkyBlue
        Dkairy_koj_syubetu.ForeColor = Drawing.Color.SkyBlue
        Dkameiten_cd.ForeColor = Drawing.Color.SkyBlue
        Dkameiten_mei1.ForeColor = Drawing.Color.SkyBlue
        Dks_siyou.ForeColor = Drawing.Color.SkyBlue
        Dsesyu_mei.ForeColor = Drawing.Color.SkyBlue
        Dsyoudakusyo_tys_date.ForeColor = Drawing.Color.SkyBlue
        Dtys_houhou_mei_ryaku.ForeColor = Drawing.Color.SkyBlue
        Dtys_jissi_date.ForeColor = Drawing.Color.SkyBlue
        Dtys_kibou_date.ForeColor = Drawing.Color.SkyBlue
        Duri_date.ForeColor = Drawing.Color.SkyBlue
        Duri_date2.ForeColor = Drawing.Color.SkyBlue
        Select Case hidSort.Value
            Case "Ahaki_syubetu"
                Ahaki_syubetu.ForeColor = Drawing.Color.IndianRed
            Case "Ahosyousyo_hak_date"
                Ahosyousyo_hak_date.ForeColor = Drawing.Color.IndianRed
            Case "Ahosyousyo_no"
                Ahosyousyo_no.ForeColor = Drawing.Color.IndianRed
            Case "Airai_date"
                Airai_date.ForeColor = Drawing.Color.IndianRed
            Case "Airai_tantousya_mei"
                Airai_tantousya_mei.ForeColor = Drawing.Color.IndianRed
            Case "Akairy_koj_date"
                Akairy_koj_date.ForeColor = Drawing.Color.IndianRed
            Case "Akairy_koj_kanry_yotei_date"
                Akairy_koj_kanry_yotei_date.ForeColor = Drawing.Color.IndianRed
            Case "Akairy_koj_syubetu"
                Akairy_koj_syubetu.ForeColor = Drawing.Color.IndianRed
            Case "Akameiten_cd"
                Akameiten_cd.ForeColor = Drawing.Color.IndianRed
            Case "Akameiten_mei1"
                Akameiten_mei1.ForeColor = Drawing.Color.IndianRed
            Case "Aks_siyou"
                Aks_siyou.ForeColor = Drawing.Color.IndianRed
            Case "Asesyu_mei"
                Asesyu_mei.ForeColor = Drawing.Color.IndianRed
            Case "Asyoudakusyo_tys_date"
                Asyoudakusyo_tys_date.ForeColor = Drawing.Color.IndianRed
            Case "Atys_houhou_mei_ryaku"
                Atys_houhou_mei_ryaku.ForeColor = Drawing.Color.IndianRed
            Case "Atys_jissi_date"
                Atys_jissi_date.ForeColor = Drawing.Color.IndianRed
            Case "Atys_kibou_date"
                Atys_kibou_date.ForeColor = Drawing.Color.IndianRed
            Case "Auri_date"
                Auri_date.ForeColor = Drawing.Color.IndianRed
            Case "Auri_date2"
                Auri_date2.ForeColor = Drawing.Color.IndianRed
            Case "Dhaki_syubetu"
                Dhaki_syubetu.ForeColor = Drawing.Color.IndianRed
            Case "Dhosyousyo_hak_date"
                Dhosyousyo_hak_date.ForeColor = Drawing.Color.IndianRed
            Case "Dhosyousyo_no"
                Dhosyousyo_no.ForeColor = Drawing.Color.IndianRed
            Case "Dirai_date"
                Dirai_date.ForeColor = Drawing.Color.IndianRed
            Case "Dirai_tantousya_mei"
                Dirai_tantousya_mei.ForeColor = Drawing.Color.IndianRed
            Case "Dkairy_koj_date"
                Dkairy_koj_date.ForeColor = Drawing.Color.IndianRed
            Case "Dkairy_koj_kanry_yotei_date"
                Dkairy_koj_kanry_yotei_date.ForeColor = Drawing.Color.IndianRed
            Case "Dkairy_koj_syubetu"
                Dkairy_koj_syubetu.ForeColor = Drawing.Color.IndianRed
            Case "Dkameiten_cd"
                Dkameiten_cd.ForeColor = Drawing.Color.IndianRed
            Case "Dkameiten_mei1"
                Dkameiten_mei1.ForeColor = Drawing.Color.IndianRed
            Case "Dks_siyou"
                Dks_siyou.ForeColor = Drawing.Color.IndianRed
            Case "Dsesyu_mei"
                Dsesyu_mei.ForeColor = Drawing.Color.IndianRed
            Case "Dsyoudakusyo_tys_date"
                Dsyoudakusyo_tys_date.ForeColor = Drawing.Color.IndianRed
            Case "Dtys_houhou_mei_ryaku"
                Dtys_houhou_mei_ryaku.ForeColor = Drawing.Color.IndianRed
            Case "Dtys_jissi_date"
                Dtys_jissi_date.ForeColor = Drawing.Color.IndianRed
            Case "Dtys_kibou_date"
                Dtys_kibou_date.ForeColor = Drawing.Color.IndianRed
            Case "Duri_date"
                Duri_date.ForeColor = Drawing.Color.IndianRed
            Case "Duri_date2"
                Duri_date2.ForeColor = Drawing.Color.IndianRed
        End Select

        Dim dv As New DataView
        dv = CType(ViewState.Item("dtTable"), DataTable).DefaultView
        dv.Sort = SortStyle(hidSort.Value)
        MeisaiStyle(dv.ToTable, ViewState.Item("intCount1"))
    End Sub
    Function SortStyle(ByVal inStr As String) As String

        If Left(inStr, 1) = "A" Then
            SortStyle = Right(inStr, Len(inStr) - 1) & " ASC"

        Else
            SortStyle = Right(inStr, Len(inStr) - 1) & " DESC"
        End If
    End Function


    Sub MeisaiStyle(ByVal BukkenJyouhouTableDataTable As DataTable, ByVal intCount1 As Integer)


        If BukkenJyouhouTableDataTable.Rows.Count > 0 Then
            If ViewState.Item("kennsuu").ToString = "100" Then
                If intCount1 > 100 Then
                    lblSearch.Text = "100/" & intCount1
                    Me.lblSearch.ForeColor = Drawing.Color.Red
                Else
                    lblSearch.Text = intCount1
                    Me.lblSearch.ForeColor = Drawing.Color.Black
                End If
            ElseIf ViewState.Item("kennsuu").ToString = "30" Then
                If intCount1 > 30 Then
                    lblSearch.Text = "30/" & intCount1
                    Me.lblSearch.ForeColor = Drawing.Color.Red
                Else
                    lblSearch.Text = intCount1
                    Me.lblSearch.ForeColor = Drawing.Color.Black
                End If
            Else
                lblSearch.Text = intCount1
                Me.lblSearch.ForeColor = Drawing.Color.Black
            End If
            'lblSearch.Text = " 検索結果：" & BukkenJyouhouTableDataTable.Rows.Count & "件"

            Dim dtDataNaiyou As New DataTable
            Dim drRow As DataRow
            Dim intCount As Integer = 0
            For intCount = 0 To 10
                dtDataNaiyou.Columns.Add(New DataColumn("col" & intCount, GetType(String)))
            Next
            For intCount = 0 To BukkenJyouhouTableDataTable.Rows.Count - 1
                drRow = dtDataNaiyou.NewRow
                dtDataNaiyou.Rows.Add(drRow)
                drRow = dtDataNaiyou.NewRow
                dtDataNaiyou.Rows.Add(drRow)
                drRow = dtDataNaiyou.NewRow
                dtDataNaiyou.Rows.Add(drRow)
            Next
            grdNaiyou.DataSource = dtDataNaiyou
            grdNaiyou.DataBind()
            Dim intRow As Integer = 0
            Dim intCol As Integer = 0
            For intCount = 1 To grdNaiyou.Rows.Count / 3
                '一行---------------------------------------------------------------------------
                intRow = intCount * 3 - 3
                grdNaiyou.Rows(intRow).Cells(0).ColumnSpan = 2
                grdNaiyou.Rows(intRow).Cells(1).Visible = False
                grdNaiyou.Rows(intRow).Cells(6).ColumnSpan = 3
                grdNaiyou.Rows(intRow).Cells(6).RowSpan = 2
                grdNaiyou.Rows(intRow).Cells(7).Visible = False
                grdNaiyou.Rows(intRow).Cells(8).Visible = False
                grdNaiyou.Rows(intRow).Attributes.Add("style", "height:20px;")
                grdNaiyou.Rows(intRow).Cells(6).Attributes.Add("style", "white-space:normal;")
                If intCount / 2 = intCount \ 2 Then
                    grdNaiyou.Rows(intRow).BackColor = Drawing.Color.LightCyan
                Else
                    grdNaiyou.Rows(intRow).BackColor = Drawing.Color.White
                End If
                Dim BKColor As Drawing.Color = Drawing.Color.Transparent

                '物件NO
                Dim comControl As New Label
                comControl.Width = Unit.Pixel(99)
                comControl.BackColor = BKColor
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("hosyousyo_no").ToString
                grdNaiyou.Rows(intRow).Cells(0).Controls.Add(comControl)
                '破棄
                comControl = New Label
                comControl.Width = Unit.Pixel(76)
                comControl.BackColor = BKColor
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("haki_syubetu").ToString
                grdNaiyou.Rows(intRow).Cells(2).Controls.Add(comControl)
                grdNaiyou.Rows(intRow).Cells(2).Attributes.Add("style", "white-space:normal;")
                '依頼日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(89)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("irai_date"))
                grdNaiyou.Rows(intRow).Cells(3).Controls.Add(comControl)
                '調査希望日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(108)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("tys_kibou_date"))
                grdNaiyou.Rows(intRow).Cells(4).Controls.Add(comControl)
                '方法
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(74)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("tys_houhou_mei_ryaku").ToString
                grdNaiyou.Rows(intRow).Cells(5).Controls.Add(comControl)
                '調査結果
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(330)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                If CInt(TrimNull(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("status"), True)) >= 700 Or TrimNull(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("keiyu")) = "9" Then
                    If Not IsDBNull(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("hantei_cd2")) Then
                        comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("ks_siyou").ToString & "他"
                    Else
                        comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("ks_siyou").ToString
                    End If
                Else
                    comControl.Text = "　"
                End If
                grdNaiyou.Rows(intRow).Cells(6).Controls.Add(comControl)
                '調査予定日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(74)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("syoudakusyo_tys_date"))
                grdNaiyou.Rows(intRow).Cells(9).Controls.Add(comControl)
                '工事予定日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(74)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("kairy_koj_kanry_yotei_date"))
                grdNaiyou.Rows(intRow).Cells(10).Controls.Add(comControl)

                '二行---------------------------------------------------------------------------
                intRow = intCount * 3 - 2
                grdNaiyou.Rows(intRow).Cells(1).ColumnSpan = 5
                grdNaiyou.Rows(intRow).Cells(2).Visible = False
                grdNaiyou.Rows(intRow).Cells(3).Visible = False
                grdNaiyou.Rows(intRow).Cells(4).Visible = False
                grdNaiyou.Rows(intRow).Cells(5).Visible = False
                grdNaiyou.Rows(intRow).Cells(6).Visible = False
                grdNaiyou.Rows(intRow).Cells(7).Visible = False
                grdNaiyou.Rows(intRow).Cells(8).Visible = False
                grdNaiyou.Rows(intRow).Attributes.Add("style", "height:20px;")
                'grdNaiyou.Rows(intRow).Cells(0).Attributes.Add("style", "width:8px;BACKGROUND-COLOR: transparent;border-bottom-style: none;border-left-style: none;border-top-style: none;")
                grdNaiyou.Rows(intRow).Cells(0).Attributes.Add("style", "BACKGROUND-COLOR: transparent;border-bottom-style: none;border-left-style: none;border-top-style: none;")
                If intCount / 2 = intCount \ 2 Then
                    grdNaiyou.Rows(intRow).BackColor = Drawing.Color.LightCyan
                Else
                    grdNaiyou.Rows(intRow).BackColor = Drawing.Color.White
                End If
                '""
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(26)

                comControl.Attributes.Add("style", "padding: 1px 1px;")
                grdNaiyou.Rows(intRow).Cells(0).Controls.Add(comControl)
                '施主名
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(430)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                Dim hLinkr As New HyperLink
                Dim strLink As String
                strLink = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("hosyousyo_no").ToString
                strLink = Left(strLink, 1) & "$$$" & Mid(strLink, 2)
                hLinkr.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("sesyu_mei").ToString
                hLinkr.NavigateUrl = "../jhs_earth/PopupBukkenRireki.aspx?sendSearchTerms=" & strLink
                If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
                    hLinkr.Attributes.Add("onclick", "window.open('../jhs_earth_dev/PopupBukkenRireki.aspx?sendSearchTerms=" & strLink & "', 'popWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');return false;")
                Else
                    hLinkr.Attributes.Add("onclick", "window.open('../jhs_earth/PopupBukkenRireki.aspx?sendSearchTerms=" & strLink & "', 'popWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');return false;")

                End If
                comControl.Controls.Add(hLinkr)

                grdNaiyou.Rows(intRow).Cells(1).Controls.Add(comControl)
                grdNaiyou.Rows(intRow).Cells(1).Attributes.Add("style", "white-space:normal;")
                '調査実施日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("tys_jissi_date"))
                grdNaiyou.Rows(intRow).Cells(9).Controls.Add(comControl)
                '工事実施日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("kairy_koj_date"))
                grdNaiyou.Rows(intRow).Cells(10).Controls.Add(comControl)


                '三行---------------------------------------------------------------------------
                intRow = intCount * 3 - 1
                '==================2012/03/29 車龍 405721案件の対応 修正↓=================================
                'grdNaiyou.Rows(intRow).Cells(2).ColumnSpan = 4
                grdNaiyou.Rows(intRow).Cells(2).ColumnSpan = 3
                grdNaiyou.Rows(intRow).Cells(3).Visible = False
                grdNaiyou.Rows(intRow).Cells(4).Visible = False
                'grdNaiyou.Rows(intRow).Cells(5).Visible = False
                '==================2012/03/29 車龍 405721案件の対応 修正↑=================================
                grdNaiyou.Rows(intRow).Attributes.Add("style", "height:20px;")
                If intCount <> grdNaiyou.Rows.Count / 3 Then
                    grdNaiyou.Rows(intRow).Cells(0).Attributes.Add("style", "BACKGROUND-COLOR: transparent;border-left-style: none;border-top-style: none;")
                Else
                    grdNaiyou.Rows(intRow).Cells(0).Attributes.Add("style", "BACKGROUND-COLOR: transparent;border-bottom-style: none;border-left-style: none;border-top-style: none;")
                End If
                If intCount / 2 = intCount \ 2 Then
                    grdNaiyou.Rows(intRow).BackColor = Drawing.Color.LightCyan
                Else
                    grdNaiyou.Rows(intRow).BackColor = Drawing.Color.White
                End If
                '加盟店コード
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(73)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("kameiten_cd").ToString
                grdNaiyou.Rows(intRow).Cells(1).Controls.Add(comControl)
                '加盟店名
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(275)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("kameiten_mei1").ToString
                grdNaiyou.Rows(intRow).Cells(2).Controls.Add(comControl)
                grdNaiyou.Rows(intRow).Cells(2).Attributes.Add("style", "white-space:normal;")
                '==================2012/03/29 車龍 405721案件の対応 追加↓=================================
                '取消
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(77)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("torikesi_txt").ToString.Trim
                grdNaiyou.Rows(intRow).Cells(5).Controls.Add(comControl)
                grdNaiyou.Rows(intRow).Cells(5).Attributes.Add("style", "white-space:normal;")

                '色をセットする
                If BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("torikesi").ToString.Trim.Equals("0") Then
                    grdNaiyou.Rows(intRow).Cells(1).ForeColor = Drawing.Color.Black
                    grdNaiyou.Rows(intRow).Cells(2).ForeColor = Drawing.Color.Black
                    grdNaiyou.Rows(intRow).Cells(5).ForeColor = Drawing.Color.Black
                Else
                    grdNaiyou.Rows(intRow).Cells(1).ForeColor = Drawing.Color.Red
                    grdNaiyou.Rows(intRow).Cells(2).ForeColor = Drawing.Color.Red
                    grdNaiyou.Rows(intRow).Cells(5).ForeColor = Drawing.Color.Red
                End If
                '==================2012/03/29 車龍 405721案件の対応 追加↑=================================
                '担当者
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(83)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("irai_tantousya_mei").ToString
                grdNaiyou.Rows(intRow).Cells(6).Controls.Add(comControl)
                '改良工事種別
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(125)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("kairy_koj_syubetu").ToString
                grdNaiyou.Rows(intRow).Cells(7).Controls.Add(comControl)
                grdNaiyou.Rows(intRow).Cells(2).Attributes.Add("style", "white-space:normal;")
                '保証書発行日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Width = Unit.Pixel(120)
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("hosyousyo_hak_date"))
                grdNaiyou.Rows(intRow).Cells(8).Controls.Add(comControl)
                '調査売上日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("uri_date"))
                grdNaiyou.Rows(intRow).Cells(9).Controls.Add(comControl)
                '工事売上日
                comControl = New Label
                comControl.BackColor = BKColor
                comControl.Attributes.Add("style", "padding: 1px 1px;")
                comControl.Text = SetDate(BukkenJyouhouTableDataTable.Rows(intCount - 1).Item("uri_date2"))
                grdNaiyou.Rows(intRow).Cells(10).Controls.Add(comControl)


            Next
        Else
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If
    End Sub
    ''' <summary>空白文字の削除処理</summary>
    Private Function TrimNull(ByVal objStr As Object, Optional ByVal blnNum As Boolean = False) As String
        If IsDBNull(objStr) Then
            If blnNum Then
                TrimNull = "0"
            Else
                TrimNull = ""
            End If

        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    Private Function SetDate(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            Return ""
        Else
            If objStr = "" Then
                Return ""
            ElseIf CDate(objStr).ToString("yyyy/MM/dd") = "1900/01/01" Then
                Return ""
            Else
                Return CDate(objStr).ToString("yyyy/MM/dd")
            End If
        End If
    End Function

    Private Sub btnBukkenJyouhouCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsv.Click
        'データを取得
        Dim BukkenJyouhouCSVTableDataTable As New BukkenJyouhouDataSet.BukkenJyouhouCSVTableDataTable
        Dim BukkenJyouhouLogic As New BukkenJyouhouLogic
        BukkenJyouhouCSVTableDataTable = BukkenJyouhouLogic.GetBukkenJyouhouInfoCSV(ViewState.Item("dtDataSource"))

        If BukkenJyouhouCSVTableDataTable.Rows.Count > 0 Then
            'CSVファイル名設定
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("BukkenJyouhouCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSVファイルヘッダ設定
            writer.WriteLine(EarthConst.conBukkenJyouhouCsvHeader)

            'CSVファイル内容設定
            For Each row As BukkenJyouhouDataSet.BukkenJyouhouCSVTableRow In BukkenJyouhouCSVTableDataTable

                writer.WriteLine(Replace(row.haki_syubetu, ",", "、"), _
                                 row.kbn, _
                                 row.hosyousyo_no, _
                                 Replace(row.sesyu_mei, ",", "、"), _
                                 SetDate(row.irai_date), _
                                 row.kameiten_cd, _
                                 Replace(row.kameiten_mei1, ",", "、"), _
                                 Replace(row.tys_renrakusaki_atesaki_mei, ",", "、"), _
                                 Replace(row.bukken_nayose_cd, ",", "、"), _
                                 row.irai_tantousya_mei, _
                                 row.todouhuken_mei, _
                                 row.eigyou_tantousya_mei, _
                                 Replace(row.bukken_jyuusyo1, ",", "、"), _
                                 Replace(row.bukken_jyuusyo2, ",", "、"), _
                                 Replace(row.bukken_jyuusyo3, ",", "、"), _
                                 Replace(row.bikou, ",", "、"), _
                                 Replace(row.tys_kaisya_cd, ",", "、"), _
                                 Replace(row.tys_kaisya_jigyousyo_cd, ",", "、"), _
                                 Replace(row.tys_kaisya_mei, ",", "、"), _
                                 Replace(row.tys_houhou_mei, ",", "、"), _
                                 SetDate(row.tys_kibou_date), _
                                 row.tys_kibou_jikan, _
                                 SetDate(row.syoudakusyo_tys_date), _
                                 SetDate(row.tys_jissi_date), _
                                 Replace(row.ks_siyou, ",", "、"), _
                                 SetDate(row.keikakusyo_sakusei_date), _
                                 SetDate(row.hosyousyo_hak_iraisyo_tyk_date), _
                                 Replace(row.hosyousyo_hak_jyky, ",", "、"), _
                                 SetDate(row.hosyousyo_hak_date), _
                                Replace(row.koj_gaisya_cd, ",", "、"), _
                                Replace(row.koj_gaisya_jigyousyo_cd, ",", "、"), _
                                 Replace(row.koj_gaisya_mei, ",", "、"), _
                                Replace(row.t_koj_kaisya_cd, ",", "、"), _
                                Replace(row.t_koj_kaisya_jigyousyo_cd, ",", "、"), _
                                Replace(row.koj_tys_kaisya_mei, ",", "、"), _
                                Replace(row.koj_siyou_kakunin, ",", "、"), _
                                SetDate(row.koj_siyou_kakunin_date), _
                                 SetDate(row.kairy_koj_kanry_yotei_date), _
                                 SetDate(row.kairy_koj_date), _
                                 SetDate(row.t_koj_kanry_yotei_date), _
                                 SetDate(row.t_koj_date), _
                                 SetDate(row.uri_date), _
                                 row.uri_gaku, _
                                 SetDate(row.seikyuusyo_hak_date), _
                                 row.nyuukin_gaku, _
                                row.hattyuusyo_gaku, _
                                SetDate(row.hattyuusyo_kakunin_date), _
                                 SetDate(row.kairyou_uri_date), _
                                 row.kairyou_uri_gaku, _
                                 SetDate(row.kairyou_seikyuusyo_hak_date), _
                                 row.koj_nyuukin_gaku, _
                                row.hattyuusyo_gaku1, _
                                SetDate(row.hattyuusyo_kakunin_date1), _
                                 SetDate(row.tuika_uri_date), _
                                 row.tuika_uri_gaku, _
                                 SetDate(row.tuika_seikyuusyo_hak_date), _
                                 row.tuika_koj_nyuukin_gaku, _
                                row.hattyuusyo_gaku2, _
                                SetDate(row.hattyuusyo_kakunin_date2))
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


    ''' <summary> javascript </summary>
    Private Sub MakeJavaScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "CheckScriptBlock"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("var activeRow=null;")
            .AppendLine("var defaultColor='';")
            .AppendLine("var motoRow=-1;")
            .AppendLine("var motoRow1=-1;")
            .AppendLine("var motoRow2=-1;")
            If Not IsPostBack Then
                .AppendLine("function window.onload(){" & vbCrLf)

                .AppendLine("document.getElementById('" & hidkennsuu.ClientID & "').value=window.parent.opener.document.getElementById('" & Request("objKennsuu") & "').value;" & vbCrLf)
                .AppendLine("document.forms[0].submit();" & vbCrLf)
                .AppendLine("       }")
            End If

            .Append("function fncSort(str){" & vbCrLf)
            .Append("eval('document.all.'+'" & hidSort.ClientID & "').value=str;" & vbCrLf)
            .Append("}" & vbCrLf)
            '行変色
            .AppendLine("function onListSelected(obj,rowNo){")
            .AppendLine("   if(!activeRow){")
            .AppendLine("       activeRow=obj;")
            .AppendLine("       defaultColor=activeRow.style.backgroundColor;")

            .AppendLine("       if (parseInt(rowNo+1)%3 == 0){")
            .AppendLine("       motoRow2=rowNo;")
            .AppendLine("       motoRow1=rowNo-1;")
            .AppendLine("       motoRow=rowNo-2;")
            .AppendLine("       }else{")
            .AppendLine("       if (parseInt(rowNo+2)%3 == 0){")
            .AppendLine("       motoRow2=rowNo+1;")
            .AppendLine("       motoRow1=rowNo;")
            .AppendLine("       motoRow=rowNo-1;")


            .AppendLine("       }else{")
            .AppendLine("       motoRow2=rowNo+2;")
            .AppendLine("       motoRow1=rowNo+1;")
            .AppendLine("       motoRow=rowNo;")

            .AppendLine("       }")
            .AppendLine("       }")
            .AppendLine("      objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor='pink';")
            .AppendLine("      objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow1].style.backgroundColor='pink';")
            .AppendLine("      objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow2].style.backgroundColor='pink';")
            .AppendLine("   }")
            .AppendLine("   else{")
            .AppendLine("           objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor=defaultColor;")
            .AppendLine("           objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow1].style.backgroundColor=defaultColor;")
            .AppendLine("           objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow2].style.backgroundColor=defaultColor;")
            .AppendLine("       activeRow=obj;")
            .AppendLine("       defaultColor=activeRow.style.backgroundColor;")
            .AppendLine("       if (parseInt(rowNo+1)%3 == 0){")
            .AppendLine("       motoRow2=rowNo;")
            .AppendLine("       motoRow1=rowNo-1;")
            .AppendLine("       motoRow=rowNo-2;")
            .AppendLine("       }else{")
            .AppendLine("       if (parseInt(rowNo+2)%3 == 0){")
            .AppendLine("       motoRow2=rowNo+1;")
            .AppendLine("       motoRow1=rowNo;")
            .AppendLine("       motoRow=rowNo-1;")


            .AppendLine("       }else{")
            .AppendLine("       motoRow2=rowNo+2;")
            .AppendLine("       motoRow1=rowNo+1;")
            .AppendLine("       motoRow=rowNo;")

            .AppendLine("       }")
            .AppendLine("       }")
            .AppendLine("           objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow2].style.backgroundColor='pink';")
            .AppendLine("           objEBI('" + Me.grdNaiyou.ClientID + "').childNodes[0].childNodes[motoRow1].style.backgroundColor='pink';")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("</script>")
        End With

        ClientScript.RegisterStartupScript(csType, csName, csScript.ToString)

    End Sub

    Private Sub grdNaiyou_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdNaiyou.RowDataBound
        e.Row.Attributes.Add("onclick", "onListSelected(this," & e.Row.RowIndex & ");")
    End Sub
End Class