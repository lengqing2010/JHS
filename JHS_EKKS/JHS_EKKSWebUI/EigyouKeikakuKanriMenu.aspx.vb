Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Data
Imports System.Collections.Generic
Partial Class EigyouKeikakuKanriMenu
    Inherits System.Web.UI.Page

#Region "イベント"
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC
    ''' <summary>
    ''' 初期処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)
        '年度計画値設定ボタンの字体を太字に設定
        btnNendoSeltuTei.Button.Style("font-weight") = "bold"
        '支店 月別計画値設定ボタンの字体を太字に設定
        btnSiten.Button.Style("font-weight") = "bold"
        '登録事業者別計画管理ボタンの字体を太字に設定
        btnTorokuKeikaku.Button.Style("font-weight") = "bold"

        Call SetJsEvent()

    End Sub

    ''' <summary>
    ''' 年度計画値設定ボタン
    ''' </summary>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnNendoSeltuTei_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '会社・支店別 年度計画値設定画面に遷移する
        Server.Transfer("NendoKeikakuInput.aspx")

    End Sub

    ''' <summary>
    ''' 支店 月別計画値設定ボタン
    ''' </summary>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnSiten_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '支店 月別計画値設定画面に遷移する
        Server.Transfer("SitenTukibetuKeikakuchiSearchList.aspx")

    End Sub

    ''' <summary>
    ''' 登録事業者別 計画管理ボタン
    ''' </summary>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnTorokuKeikaku_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '計画管理画面に遷移する
        Server.Transfer("KeikakuKanriSearchList.aspx")

    End Sub

#End Region

#Region "メンッド"
    ''' <summary>
    ''' OnClick事件を追加
    ''' </summary>
    ''' <history>																
    ''' <para>2012/11/16 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub SetJsEvent()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '年度計画値設定ボタンのClick事件
        Me.btnNendoSeltuTei.OnClick = "btnNendoSeltuTei_Click()"
        '支店 月別計画値設定ボタンのClick事件
        Me.btnSiten.OnClick = "btnSiten_Click()"
        '登録事業者別計画管理ボタンのClick事件
        Me.btnTorokuKeikaku.OnClick = "btnTorokuKeikaku_Click()"
    End Sub
#End Region



End Class
