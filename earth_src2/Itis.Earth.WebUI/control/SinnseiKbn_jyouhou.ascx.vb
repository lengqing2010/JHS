Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Partial Public Class SinnseiKbn_jyouhou
    Inherits System.Web.UI.UserControl
    Public msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus
    Private KihonJyouhouLogic As New KihonJyouhouLogic
    Private strKameitenCd As String
    Private strLoginUserId As String
    Private _kenngenn As Boolean

    ''' <summary>加盟店コード属性をセット</summary>
    Public WriteOnly Property GetKameitenCd() As String
        Set(ByVal KameitenCd As String)
            strKameitenCd = KameitenCd
        End Set
    End Property
    Public WriteOnly Property GetLoginUserId() As String
        Set(ByVal LoginUserId As String)
            strLoginUserId = LoginUserId
        End Set
    End Property
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ListInit(Me.ddl_shinsei_syoshiki, "68", True, True)
            PageDataInit()
        End If
    End Sub

    Public Sub PageDataInit()
        Dim SinnseiKbnJyouhouLogic As New SinnseiKbnJyouhouLogic
        Dim dt As DataTable = SinnseiKbnJyouhouLogic.GetSinnseiInfo(strKameitenCd)
        If dt.Rows.Count > 0 Then

            '2行目
            'shinsei_syoshiki = ddl_shinsei_syoshiki.Items(ddl_shinsei_syoshiki.SelectedIndex).Value
            ddl_shinsei_syoshiki.SelectedValue = NullToEmpty(dt.Rows(0).Item("shinsei_syoshiki"))
            'shinsei_kbn_shinki = IIf(cbshinsei_kbn_shinki.Checked, "1", "0")
            cbshinsei_kbn_shinki.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_shinki")) = "1", True, False)

            'shinsei_kbn_jig_shinki = IIf(cbshinsei_kbn_jig_shinki.Checked, "1", "0")
            cbshinsei_kbn_jig_shinki.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_jig_shinki")) = "1", True, False)
            'shinsei_kbn_jig_fudousan = IIf(cbshinsei_kbn_jig_fudousan.Checked, "1", "0")
            cbshinsei_kbn_jig_fudousan.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_jig_fudousan")) = "1", True, False)
            'shinsei_kbn_jig_reform = IIf(cbshinsei_kbn_jig_reform.Checked, "1", "0")
            cbshinsei_kbn_jig_reform.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_jig_reform")) = "1", True, False)
            'shinsei_kbn_jig_sonota = IIf(cbshinsei_kbn_jig_sonota.Checked, "1", "0")


            cbshinsei_kbn_jig_sonota.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_jig_sonota")) = "1", True, False)
            'shinsei_kbn_ser_jibantyousa = IIf(cbshinsei_kbn_ser_jibantyousa.Checked, "1", "0")
            tbxShinsei_kbn_jig_sonota_hosoku.Enabled = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_jig_sonota")) = "1", True, False)
            tbxShinsei_kbn_jig_sonota_hosoku.Text = NullToEmpty(dt.Rows(0).Item("shinsei_kbn_jig_sonota_hosoku"))




            '2行目
            'その他
            cbshinsei_kbn_sonota.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_sonota")) = "1", True, False)

            '地盤調査関連ｻｰﾋﾞｽ
            cbshinsei_kbn_ser_jibantyousa.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_ser_jibantyousa")) = "1", True, False)

            '建物検査関連ｻｰﾋﾞｽ 
            cbshinsei_kbn_ser_tatemonokensa.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_ser_tatemonokensa")) = "1", True, False)

            'その他
            cbshinsei_kbn_ser_sonota.Checked = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_ser_sonota")) = "1", True, False)
            tbxShinsei_kbn_ser_sonota_hosoku.Enabled = IIf(NullToEmpty(dt.Rows(0).Item("shinsei_kbn_ser_sonota")) = "1", True, False)
            tbxShinsei_kbn_ser_sonota_hosoku.Text = NullToEmpty(dt.Rows(0).Item("shinsei_kbn_ser_sonota_hosoku"))



  
        End If

    End Sub


    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        Dim shinsei_syoshiki As String
        Dim shinsei_kbn_shinki As String
        Dim shinsei_kbn_sonota As String
        Dim shinsei_kbn_jig_shinki As String
        Dim shinsei_kbn_jig_fudousan As String
        Dim shinsei_kbn_jig_reform As String
        Dim shinsei_kbn_jig_sonota As String
        Dim shinsei_kbn_ser_jibantyousa As String
        Dim shinsei_kbn_ser_tatemonokensa As String
        Dim shinsei_kbn_jig_sonota_hosoku As String
        Dim shinsei_kbn_ser_sonota As String
        Dim shinsei_kbn_ser_sonota_hosoku As String

        shinsei_syoshiki = ddl_shinsei_syoshiki.Items(ddl_shinsei_syoshiki.SelectedIndex).Value

        shinsei_kbn_shinki = IIf(cbshinsei_kbn_shinki.Checked, "1", "0")
        shinsei_kbn_sonota = IIf(cbshinsei_kbn_sonota.Checked, "1", "0")
        shinsei_kbn_jig_shinki = IIf(cbshinsei_kbn_jig_shinki.Checked, "1", "0")
        shinsei_kbn_jig_fudousan = IIf(cbshinsei_kbn_jig_fudousan.Checked, "1", "0")
        shinsei_kbn_jig_reform = IIf(cbshinsei_kbn_jig_reform.Checked, "1", "0")
        shinsei_kbn_jig_sonota = IIf(cbshinsei_kbn_jig_sonota.Checked, "1", "0")

        shinsei_kbn_ser_jibantyousa = IIf(cbshinsei_kbn_ser_jibantyousa.Checked, "1", "0")
        shinsei_kbn_ser_tatemonokensa = IIf(cbshinsei_kbn_ser_tatemonokensa.Checked, "1", "0")


        shinsei_kbn_jig_sonota_hosoku = tbxShinsei_kbn_jig_sonota_hosoku.Text
        shinsei_kbn_ser_sonota = IIf(cbshinsei_kbn_ser_sonota.Checked, "1", "0")
        shinsei_kbn_ser_sonota_hosoku = tbxShinsei_kbn_ser_sonota_hosoku.Text


        '排他
        Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
        If Not otherPageFunction.DoFunction(Parent.Page, "Haitakameiten") Then
            Exit Sub
        End If


        Dim updKekka As String

        Dim SinnseiKbnJyouhouLogic As New SinnseiKbnJyouhouLogic

        updKekka = SinnseiKbnJyouhouLogic.UpdSinnseiInfo(strKameitenCd _
                                                        , shinsei_syoshiki _
                                                        , shinsei_kbn_shinki _
                                                        , shinsei_kbn_sonota _
                                                        , shinsei_kbn_jig_shinki _
                                                        , shinsei_kbn_jig_fudousan _
                                                        , shinsei_kbn_jig_reform _
                                                        , shinsei_kbn_jig_sonota _
                                                        , shinsei_kbn_ser_jibantyousa _
                                                        , shinsei_kbn_ser_tatemonokensa _
                                                        , shinsei_kbn_jig_sonota_hosoku _
                                                        , shinsei_kbn_ser_sonota _
                                                        , shinsei_kbn_ser_sonota_hosoku _
                                                        , strLoginUserId)


        '最新更新時間---更新
        If InStr(updKekka, Messages.Instance.MSG018S.ToString.Substring(7)) Then
            otherPageFunction.DoFunction(Parent.Page, "SetKyoutuuKousin")
        End If
        Dim csScript As New StringBuilder
        ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err", csScript.Append("alert('" & updKekka & "');").ToString, True)


    End Sub

    Private Function NullToEmpty(ByVal v As Object) As Object

        If v Is DBNull.Value Then
            Return String.Empty
        Else
            Return v.ToString()
        End If
    End Function


    ''' <summary>
    ''' トッロプダウンリストデータ設定
    ''' </summary>
    ''' <param name="ddl">トッロプダウンリストコントロール</param>
    ''' <param name="withSpc">空白行</param>
    ''' <param name="withCd">コード</param>
    ''' <remarks></remarks>
    Protected Sub ListInit(ByRef ddl As DropDownList, ByVal meisyou_syubetu As String, ByVal withSpc As Boolean, Optional ByVal withCd As Boolean = True)

        Dim dtTemp As New DataTable

        ' DataTableへのカラム設定
        dtTemp.Columns.Add(New DataColumn("CmbTextField", GetType(String)))
        dtTemp.Columns.Add(New DataColumn("CmbValueField", GetType(String)))
        Dim TorihikiBL As New TorihikiJyouhouLogic()
        TorihikiBL.GetListData6869(dtTemp, meisyou_syubetu, withSpc, withCd)
        ddl.DataSource = dtTemp
        ddl.DataTextField = "CmbTextField"
        ddl.DataValueField = "CmbValueField"
        ddl.DataBind()

    End Sub

    ''' <summary>リンクをクリック</summary>
    Private Sub lnkTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTitle.Click
        If meisaiTbody1.Style.Item("display") = "none" Then
            meisaiTbody1.Style.Item("display") = ""
            btnTouroku.Style.Item("display") = ""
        Else
            meisaiTbody1.Style.Item("display") = "none"
            btnTouroku.Style.Item("display") = "none"
        End If

    End Sub

    Protected Sub cbshinsei_kbn_jig_sonota_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        tbxShinsei_kbn_jig_sonota_hosoku.Enabled = cbshinsei_kbn_jig_sonota.Checked

        If Not cbshinsei_kbn_jig_sonota.Checked Then
            tbxShinsei_kbn_jig_sonota_hosoku.Text = ""
        End If

    End Sub
    Protected Sub cbshinsei_kbn_ser_sonota_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        tbxShinsei_kbn_ser_sonota_hosoku.Enabled = cbshinsei_kbn_ser_sonota.Checked

        If Not cbshinsei_kbn_ser_sonota.Checked Then
            tbxShinsei_kbn_ser_sonota_hosoku.Text = ""
        End If
    End Sub
End Class