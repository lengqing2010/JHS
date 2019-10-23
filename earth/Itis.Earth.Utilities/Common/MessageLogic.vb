Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports Itis.ApplicationBlocks.ExceptionManagement

Public Class MessageLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New MessageLogic()

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As MessageLogic
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New MessageLogic()
            End If
            Return _instance
        End Get
    End Property

#Region "アラートメッセージ表示"
    ''' <summary>
    ''' アラートメッセージ表示
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="message">メッセージ本文</param>
    ''' <param name="type">表示タイプ(0:RegisterStartupScript / !0:RegisterClientScriptBlock)デフォルト=0</param>
    ''' <param name="scriptID">スクリプトマネージャ用ID：デフォルト=EarthJsAlert</param>
    ''' <remarks></remarks>
    Public Sub AlertMessage(ByVal sender As Object, _
                            ByVal message As String, _
                            Optional ByVal type As Integer = 0, _
                            Optional ByVal scriptID As String = "EarthJsAlert")

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DbErrorMessage", _
                                                    sender, _
                                                    message, _
                                                    type, _
                                                    scriptID)

        '特殊文字をエスケープ(JSエラー対策)
        Dim sLogic As New StringLogic
        message = sLogic.RemoveSpecStr(message)

        If type = 0 Then
            ScriptManager.RegisterStartupScript(sender, _
                                                sender.GetType(), _
                                                scriptID, _
                                                "alert('" & _
                                                message & _
                                                "');", _
                                                True)
        Else
            ScriptManager.RegisterClientScriptBlock(sender, _
                                                    sender.GetType(), _
                                                    scriptID, _
                                                    "alert('" & _
                                                    message & _
                                                    "');", _
                                                    True)
        End If


    End Sub
#End Region

#Region "排他チェックエラー時のアラートメッセージ表示"
    ''' <summary>
    ''' 排他チェックエラー時のアラートメッセージ表示
    ''' </summary>
    ''' <param name="sender">sender object</param>
    ''' <param name="userId">userId</param>
    ''' <param name="time">time</param>
    ''' <remarks></remarks>
    Public Sub CallHaitaErrorMessage(ByVal sender As Object, ByVal userId As String, ByVal time As DateTime, Optional ByVal comment As String = "")

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallHaitaErrorMessage", _
                                                    userId, _
                                                    time)
        'メッセージ本文
        Dim message As String = String.Format(Messages.MSG003E, userId, Format(time, "yyyy/MM/dd HH:mm:ss"), comment)

        'alertスクリプト出力
        AlertMessage(sender, message, 1, "EarthJsHaitaAlert")

    End Sub
#End Region

#Region "DBアクセスエラー時のアラートメッセージ表示"
    ''' <summary>
    ''' DBアクセスエラー時のアラートメッセージ表示
    ''' </summary>
    ''' <param name="sender">sender object</param>
    ''' <param name="process">process name</param>
    ''' <param name="table">table name</param>
    ''' <param name="key">key values (Optional)</param>
    ''' <remarks></remarks>
    Public Sub DbErrorMessage(ByVal sender As Object, _
                              ByVal process As String, _
                              ByVal table As String, _
                              Optional ByVal key As String = "")

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DbErrorMessage", _
                                                    sender, _
                                                    process, _
                                                    table, _
                                                    key)

        'メッセージ本文
        Dim message As String = String.Format(Messages.MSG064E, process, table, key)

        'alertスクリプト出力
        AlertMessage(sender, message, 1, "EarthJsDbAlert")

    End Sub
#End Region


End Class
