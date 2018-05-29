Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.Utilities.CommonMessage
Imports Lixil.JHS_EKKS.BizLogic
Imports System.Data.DataTable
Imports System.Collections.Generic

''' <summary>
''' 支店 月別計画値 EXCEL取込エラー
''' </summary>
''' <remarks></remarks>
Partial Class SitenTukibetuKeikakuchiErrorDetails
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Private sitenTukibetuKeikakuchiErrorDetailsBC As New SitenTukibetuKeikakuchiErrorDetailsBC
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC

#End Region

#Region "イベント"

    ''' <summary>
    ''' 画面の初期表示
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/27　李宇(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)

        'JavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then

            '明細データを取得する
            Call GetGamenMeisaiDate()

        End If

        '閉じるボタン
        Me.btnTojiru.Attributes.Add("onClick", "fncClose();return false;")

    End Sub

#End Region

#Region "メソッド"

    ''' <summary>
    ''' 画面明細データを取得する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/11/29　李宇(大連情報システム部)　新規作成</history>
    Private Sub GetGamenMeisaiDate()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                                MyMethod.GetCurrentMethod.Name)

        Dim strSyoriDate As String = String.Empty   '処理日時
        Dim strEdiDate As String = String.Empty     'EDI情報作成日

        If Not Request("sendSearchTerms") Is Nothing Then
            strEdiDate = Split(Request("sendSearchTerms").ToString, "$$$")(2)                   'EDI情報作成日
            strSyoriDate = Split(Request("sendSearchTerms").ToString, "$$$")(0)                 '処理日時
            Me.lblSyoriDate.Text = Left(Split(strSyoriDate, "$$$")(0), 4)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(4, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(6, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & " " & strSyoriDate.Substring(8, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(10, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(12, 2)   '処理日時
            Me.lblFileMei.Text = Split(Request("sendSearchTerms").ToString, "$$$")(1)           '入力ファイル名
            Me.lblFileMei.ToolTip = Split(Request("sendSearchTerms").ToString, "$$$")(1)        '入力ファイル名タイトル
        Else
            Me.lblSyoriDate.Text = String.Empty     '処理日時
            Me.lblFileMei.Text = String.Empty       '入力ファイル名

        End If

        'エラー情報を取得する
        Dim dtErrorJyouhou As Data.DataTable
        dtErrorJyouhou = sitenTukibetuKeikakuchiErrorDetailsBC.GetErrorJyouhou(strSyoriDate, strEdiDate)

        If dtErrorJyouhou.Rows.Count > 0 Then
            Me.grdErrorJyouhou.DataSource = dtErrorJyouhou
            Me.grdErrorJyouhou.DataBind()
        Else
            Me.grdErrorJyouhou.DataSource = Nothing
            Me.grdErrorJyouhou.DataBind()

            'メッセージを表示する
            Dim common As New Common
            common.SetShowMessage(Me, MSG011E)

        End If

        'エラー内容のデータすうを取得する
        Dim dtErrorJyouhouCount As Data.DataTable
        dtErrorJyouhouCount = sitenTukibetuKeikakuchiErrorDetailsBC.GetErrorJyouhouCount(strSyoriDate, strEdiDate)

        '明細行は＞100件
        If CDbl(dtErrorJyouhouCount.Rows(0).Item(0).ToString) > 100 Then

            Me.lblCount.Text = dtErrorJyouhou.Rows.Count & "/" & dtErrorJyouhouCount.Rows(0).Item(0).ToString
            Me.lblCount.Style.Add("color", "red")
        Else
            Me.lblCount.Text = dtErrorJyouhou.Rows.Count
            Me.lblCount.Style.Add("color", "black")

        End If

    End Sub

    ''' <summary>
    '''  JS作成
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/11/29　李宇(大連情報システム部)　新規作成</history>
    Protected Sub MakeJavaScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                                MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '画面の閉じる処理
            .AppendLine("function fncClose()")
            .AppendLine("{")
            .AppendLine("   self.close();")
            .AppendLine("}")
            .AppendLine("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

#End Region

End Class
