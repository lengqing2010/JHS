Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

Partial Class KeikakuKanriErrorDetails
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Private objCommon As New Common
    Private objKeikakuKanriErrorDetailsBC As New Lixil.JHS_EKKS.BizLogic.KeikakuKanriErrorDetailsBC
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC

#End Region

#Region "定数"

    Private Const CON_TITLE As String = "計画管理 EXCEL取込エラー"

#End Region

#Region "イベント"

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)

        If Not IsPostBack Then

            '明細データ取込む(初期データをセットする)
            Call GetGrdMeisai()

        End If

        '｢閉じる｣ボタン処理
        Me.btnClose.Attributes.Add("onClick", "self.close();return false;")

    End Sub

    ''' <summary>
    ''' 初期データをセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Sub GetGrdMeisai()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim strSyoriDate As String = String.Empty   '処理日時
        Dim strEdiDate As String = String.Empty     'EDI情報作成日

        If Not Request("sendSearchTerms") Is Nothing Then
            'EDI情報作成日
            strEdiDate = Split(Request("sendSearchTerms").ToString, "$$$")(2)
            '処理日時
            strSyoriDate = Split(Request("sendSearchTerms").ToString, "$$$")(0)
            '画面の取込日時 
            Me.lblSyoriDatetime.Text = Left(strSyoriDate, 4) & "/" & _
                                        strSyoriDate.Substring(4, 2) & "/" & _
                                        strSyoriDate.Substring(6, 2) & " " & _
                                        strSyoriDate.Substring(8, 2) & ":" & _
                                        strSyoriDate.Substring(10, 2) & ":" & _
                                        strSyoriDate.Substring(12, 2)
            If strSyoriDate.Length = 14 Then
                strSyoriDate = strSyoriDate & "000"
            End If

            '画面の取込ファイル名 
            Me.lblFileName.Text = Split(Request("sendSearchTerms").ToString, "$$$")(1)           '入力ファイル名
            Me.lblFileName.ToolTip = Split(Request("sendSearchTerms").ToString, "$$$")(1)        '入力ファイル名タイトル
        Else
            strEdiDate = String.Empty
            strSyoriDate = String.Empty
            Me.lblSyoriDatetime.Text = String.Empty     '処理日時
            Me.lblFileName.Text = String.Empty          '入力ファイル名

        End If

        'EDI情報作成日
        ViewState("ediJouhouSakuseiDate") = strEdiDate
        '処理日時
        ViewState("syoriDatetime") = strSyoriDate

        '検索データを取得する
        Dim dtErrorList As Data.DataTable
        dtErrorList = objKeikakuKanriErrorDetailsBC.GetKeikakuTorikomiError(strEdiDate, strSyoriDate)

        '検索結果を設定する
        Me.grdErrorJyouhou.DataSource = dtErrorList
        Me.grdErrorJyouhou.DataBind()

        '検索結果件数を取得する
        Me.lblCount.Text = objKeikakuKanriErrorDetailsBC.GetKeikakuTorikomiErrorCount(strEdiDate, strSyoriDate)
        '検索結果件数を設定する
        SetKensakuCount(CInt(Me.lblCount.Text))

        'データが0件の場合、エラーメッセージを表示する
        If Me.lblCount.Text = "0" Then
            'メッセージを表示する
            objCommon.SetShowMessage(Me, CommonMessage.MSG011E)
        End If

    End Sub

    ''' <summary>
    ''' 検索結果件数を設定する
    ''' </summary>
    ''' <param name="intCount">検索結果件数</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If intCount > 100 Then
            Me.lblCount.Text = "100 / " & intCount
            Me.lblCount.ForeColor = Drawing.Color.Red
        Else
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
        End If

    End Sub

#End Region

End Class
