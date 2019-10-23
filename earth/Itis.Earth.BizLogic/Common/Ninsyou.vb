Imports Microsoft.VisualBasic
Imports System.Web
Imports system.Web.HttpContext
Imports System.Configuration

Public Class Ninsyou

    Private Const ACCESSDENIED As String = "401 access denied"
    Private Const LOGON_USER As String = "LOGON_USER"
    Private Const HTTP_UID As String = "HTTP_UID"
    Private Const REMOTE_ADDR As String = "REMOTE_ADDR"

    ''' <summary>
    ''' IceWall の IP リストを取得します。
    ''' </summary>
    ''' <returns>取得したIPを1次元配列で返します。</returns>
    Private Function GetIceWallIpList() As String()
        Return ConfigurationManager.AppSettings("IceWallIPList").Split(","c)
    End Function


    ''' <summary>
    ''' ユーザIDを取得します。
    ''' </summary>
    ''' <returns>ユーザID</returns>
    ''' <history>
    ''' 	[システム技術部]	2006/10/05	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetUserID() As String
        Dim req As HttpRequest = Current.Request
        Dim userID As String
        If Me.IsViaSso() Then
            userID = Mid(req.ServerVariables(HTTP_UID), InStr(req.ServerVariables(HTTP_UID), System.IO.Path.DirectorySeparatorChar) + 1)
        Else
            userID = Mid(req.ServerVariables(LOGON_USER), InStr(req.ServerVariables(LOGON_USER), System.IO.Path.DirectorySeparatorChar) + 1)
        End If
        Return userID
    End Function

    ''' <summary>
    ''' HTTP レスポンスをアクセス拒否で終了します。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EndResponseWithAccessDeny()
        Dim response As System.Web.HttpResponse = HttpContext.Current.Response
        response.Status = ACCESSDENIED
        response.End()
    End Sub

    ''' <summary>
    ''' ユーザがログオンしているかどうかを判別します。
    ''' </summary>
    ''' <returns>ユーザがログオンしている場合は True、そうでない場合は False を返します。</returns>
    Public Function IsUserLogon() As Boolean
        If ((Not Me.IsViaSso()) _
            And String.IsNullOrEmpty( _
                HttpContext.Current.Request.ServerVariables(LOGON_USER))) Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' SSO 経由のアクセスかどうかを判別する。
    ''' </summary>
    ''' <returns>SSO経由の場合はTrue、そうでない場合はFalseを返す。</returns>
    ''' <remarks></remarks>
    Private Function IsViaSso() As Boolean
        For Each iceWallIp As String In Me.GetIceWallIpList()
            If (HttpContext.Current.Request. _
                ServerVariables(REMOTE_ADDR).Contains(iceWallIp)) Then
                Return True
            End If
        Next
        Return False
    End Function

End Class
