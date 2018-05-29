Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class YosinJyouhouInquiry_aspx
    Inherits System.Web.UI.Page
#Region "*** [Private]Common Constant Definition"
    Private commonCheck As New CommonCheck
    Private ninsyou As New Ninsyou

    '名寄先コード
    Private Const mconNayoseCd As String = "nayose_cd"

#End Region
#Region "*** [Private]Common Variable Definition"
    '
    Private YosinJyouhouBL As New Itis.Earth.BizLogic.YosinJyouhouInquiryLogic

#End Region
    ''' <summary>
    ''' フォームロード時
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/10　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理

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
            'Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            'Server.Transfer("CommonErr.aspx")
        End If
        Kyoutuu_jyouhou1.GetKameitenCd = Request.QueryString("strKameitenCd")
        If Not IsPostBack Then
            ViewState("KameitenCd") = Request.QueryString("strKameitenCd")
            ViewState("UserId") = ninsyou.GetUserID()
            commonCheck.SetURL(Me, ViewState("UserId"))

            tyousaDispLink.HRef = "javascript:changeDisplay('" & kyoutuTBody.ClientID & "');changeDisplay('" & tyousaTitleInfobar.ClientID & "');"
            koujiDispLink.HRef = "javascript:changeDisplay('" & koujiTBody.ClientID & "');changeDisplay('" & koujiTitleInfobar.ClientID & "');"
            hansakuhinDispLink.HRef = "javascript:changeDisplay('" & hansakuhinTBody.ClientID & "');changeDisplay('" & hansakuhinTitleInfobar.ClientID & "');"
            tatemonoDispLink.HRef = "javascript:changeDisplay('" & tatemonoTBody.ClientID & "');changeDisplay('" & tatemonoTitleInfobar.ClientID & "');"

            '画面の初期表示する
            InitCheck()
        End If
        'javascript 作成
        MakeJavascript()
        Kyoutuu_jyouhou1.SetItemValue(ViewState("KameitenCd"))
        Kyoutuu_jyouhou1.SetReadonly(True)
        If Me.tbxTyousaNayoseCd.Text <> String.Empty Then
            Me.tbxTyousaNayoseCd.CssClass = "makePopup"
            Me.tbxTyousaNayoseCd.Attributes("ondblclick") = "return fncNayoseCdSearch('" + Me.tbxTyousaNayoseCd.Text + "');"
            Me.lblTyousaNayose.Text = "名寄せコードをダブルクリックすると<br>名寄先与信情報が表示されます"
        Else
            Me.tbxTyousaNayoseCd.CssClass = "readOnly"
        End If
        If Me.tbxKoujiNayoseCd.Text <> String.Empty Then
            Me.tbxKoujiNayoseCd.CssClass = "makePopup"
            Me.tbxKoujiNayoseCd.Attributes("ondblclick") = "return fncNayoseCdSearch('" + Me.tbxKoujiNayoseCd.Text + "');"
            Me.lblKoujiNayose.Text = "名寄せコードをダブルクリックすると<br>名寄先与信情報が表示されます"
        Else
            Me.tbxKoujiNayoseCd.CssClass = "readOnly"
        End If
        If Me.tbxHansokuhinNayoseCd.Text <> String.Empty Then
            Me.tbxHansokuhinNayoseCd.CssClass = "makePopup"
            Me.tbxHansokuhinNayoseCd.Attributes("ondblclick") = "return fncNayoseCdSearch('" + Me.tbxHansokuhinNayoseCd.Text + "');"
            Me.lblHansokuhinNayose.Text = "名寄せコードをダブルクリックすると<br>名寄先与信情報が表示されます"
        Else
            Me.tbxHansokuhinNayoseCd.CssClass = "readOnly"
        End If

        If Me.tbxTatemonoNayoseCd.Text <> String.Empty Then
            Me.tbxTatemonoNayoseCd.CssClass = "makePopup"
            Me.tbxTatemonoNayoseCd.Attributes("ondblclick") = "return fncNayoseCdSearch('" + Me.tbxTatemonoNayoseCd.Text + "');"
            Me.lblTatemonoNayose.Text = "名寄せコードをダブルクリックすると<br>名寄先与信情報が表示されます"
        Else
            Me.tbxTatemonoNayoseCd.CssClass = "readOnly"
        End If
    End Sub
    Function SetKbnMei(ByVal instr As String) As String
        Select Case Trim(instr)
            Case "0"
                Return "加盟店"
            Case "1"
                Return "調査会社"
            Case "2"
                Return "営業所"
            Case Else
                Return ""
        End Select
    End Function
    ''' <summary>
    ''' 画面の初期表示する
    ''' </summary>
    ''' <history>
    ''' 2009/07/10　王穎(大連開発)　新規作成　P-*****
    ''' </history>
    Private Sub InitCheck()

        'データ取得
        Dim dtYoshinJyouhou As DataAccess.YosinJyouhouDataSet.YosinJyouhouTableDataTable

        dtYoshinJyouhou = YosinJyouhouBL.GetYosinJyouhou(ViewState("KameitenCd"))
        If dtYoshinJyouhou.Rows.Count > 0 Then
            '調査請求先コード
            Me.tbxTyousaSeikyuuCd.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_seikyuu_saki_cd"))
            Me.tbxTyousaSeikyuuBrc.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_seikyuu_saki_brc"))
            Me.lblTyousaSeikyuuKbn.Text = SetKbnMei(Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_seikyuu_saki_kbn")))
            '調査請求先名1
            Me.tbxTyousaSeikyuuName1.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_seikyuu_saki_name1"))

            '調査請求先名2
            Me.tbxTyousaSeikyuuName2.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_seikyuu_saki_name2"))

            '調査名寄先コード
            Me.tbxTyousaNayoseCd.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_nayose_saki"))

            '調査名寄先名1
            Me.tbxTyousaNayoseName1.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_nayose_saki_name1"))

            '調査名寄先名2
            Me.tbxTyousaNayoseName2.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_nayose_saki_name2"))

            '与信警告状況
            If dtYoshinJyouhou.Rows(0).Item("tys_keikoku_jyoukyou").Equals(DBNull.Value) Then
                Me.tbxTyousaYoshinKeikokuJyoukyou.Text = String.Empty
                Me.lblTyousaYoshinKeikokuJyoukyou.Text = "なし"
            Else
                Me.tbxTyousaYoshinKeikokuJyoukyou.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_keikoku_jyoukyou"))
                Me.lblTyousaYoshinKeikokuJyoukyou.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tys_keikoku"))
            End If

            '工事請求コード
            Me.tbxKoujiSeikyuuCd.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_seikyuu_saki_cd"))

            Me.tbxKoujiSeikyuuBrc.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_seikyuu_saki_brc"))
            Me.lblKoujiSeikyuuKbn.Text = SetKbnMei(Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_seikyuu_saki_kbn")))

            '工事請求先名1
            Me.tbxKoujiSeikyuuName1.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_seikyuusaki_name1"))

            '工事請求先名2

            Me.tbxKoujiSeikyuuName2.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_seikyuusaki_name2"))

            '工事名寄先コード
            Me.tbxKoujiNayoseCd.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_nayosesaki"))

            '工事名寄先名1
            Me.tbxKoujiNayoseName1.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_nayosesaki_name1"))

            '工事名寄先名2
            Me.tbxKoujiNayoseName2.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_nayosesaki_name2"))

            '工事警告状況
            If dtYoshinJyouhou.Rows(0).Item("koj_keikoku_jyoukyou").Equals(DBNull.Value) Then
                Me.tbxKoujiYoshinKeikokuJyoukyou.Text = String.Empty
                Me.lblKoujiYoshinKeikokuJyoukyou.Text = "なし"
            Else
                Me.tbxKoujiYoshinKeikokuJyoukyou.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_keikoku_jyoukyou"))
                Me.lblKoujiYoshinKeikokuJyoukyou.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("koj_keikoku"))
            End If

            '販促品請求コード
            Me.tbxHansokuhinSeikyuuCd.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_seikyuu_saki_cd"))
            Me.tbxHansokuhinSeikyuuBrc.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_seikyuu_saki_brc"))
            Me.lblHansokuhinSeikyuuKbn.Text = SetKbnMei(Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_seikyuu_saki_kbn")))

            '販促品請求先名1
            Me.tbxHansokuhinSeikyuuName1.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_seikyuusaki_name1"))

            '販促品請求先名2
            Me.tbxHansokuhinSeikyuuName2.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_seikyuusaki_name2"))

            '販促品名寄先コード
            Me.tbxHansokuhinNayoseCd.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_nayosesaki"))

            '販促品名寄先名1
            Me.tbxHansokuhinNayoseName1.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_nayosesaki_name1"))

            '販促品名寄先名2
            Me.tbxHansokuhinNayoseName2.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_nayosesaki_name2"))

            '販促品警告状況
            If dtYoshinJyouhou.Rows(0).Item("hansokuhin_keikoku_jyoukyou").Equals(DBNull.Value) Then
                Me.tbxHansokuhinYoshinKeikokuJyoukyou.Text = String.Empty
                Me.lblHansokuhinYoshinKeikokuJyoukyou.Text = "なし"
            Else
                Me.tbxHansokuhinYoshinKeikokuJyoukyou.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_keikoku_jyoukyou"))
                Me.lblHansokuhinYoshinKeikokuJyoukyou.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("hansokuhin_keikoku"))
            End If


            '設計確認請求コード
            Me.tbxTatemonoSeikyuuCd.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_seikyuu_saki_cd"))
            Me.tbxTatemonoSeikyuuBrc.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_seikyuu_saki_brc"))
            Me.lblTatemonoSeikyuuKbn.Text = SetKbnMei(Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_seikyuu_saki_kbn")))

            '設計確認請求先名1
            Me.tbxTatemonoSeikyuuName1.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_seikyuusaki_name1"))

            '設計確認請求先名2
            Me.tbxTatemonoSeikyuuName2.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_seikyuusaki_name2"))

            '設計確認名寄先コード
            Me.tbxTatemonoNayoseCd.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_nayosesaki"))

            '設計確認名寄先名1
            Me.tbxTatemonoNayoseName1.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_nayosesaki_name1"))

            '設計確認名寄先名2
            Me.tbxTatemonoNayoseName2.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_nayosesaki_name2"))

            '設計確認警告状況
            If dtYoshinJyouhou.Rows(0).Item("tatemono_keikoku_jyoukyou").Equals(DBNull.Value) Then
                Me.tbxTatemonoYoshinKeikokuJyoukyou.Text = String.Empty
                Me.lblTatemonoYoshinKeikokuJyoukyou.Text = "なし"
            Else
                Me.tbxTatemonoYoshinKeikokuJyoukyou.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_keikoku_jyoukyou"))
                Me.lblTatemonoYoshinKeikokuJyoukyou.Text = Convert.ToString(dtYoshinJyouhou.Rows(0).Item("tatemono_keikoku"))
            End If
        End If
    End Sub
    ''' <summary>
    ''' javascript 作成
    ''' </summary>
    ''' <history>
    ''' <para>2009/07/10　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Private Sub MakeJavascript()

        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("   function fncNayoseCdSearch(srtNayoseCd){")
            .Append("objEBI('" + Me.hidNayoseCd.ClientID + "').value = srtNayoseCd;" & vbCrLf)
            .Append("objEBI('" + Me.btnTemp.ClientID + "').click();" & vbCrLf)
            .AppendLine("   }")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub
    ''' <summary>
    ''' ボタンの処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>
    ''' <para>2009/07/10　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <remarks></remarks>
    Protected Sub btnTemp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTemp.Click

        Context.Items(mconNayoseCd) = Me.hidNayoseCd.Value.ToString

        Server.Transfer("YosinJyouhouInput.aspx?modoru=YosinJyouhouInquiry.aspx&strKameitenCd=" & ViewState("KameitenCd") & "")

    End Sub

    ''' <summary>
    ''' メッセージ表示　Focus
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg2(ByVal id As String, ByVal msg As String)

        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "alert('" & msg & "');").ToString()

        'フォーカス
        csScript.AppendFormat("if(document.getElementById('" & id & "').type != ""submit"") document.getElementById('" & id & "').select();")
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "err2", _
                                        csScript.ToString, _
                                        True)
    End Sub
End Class