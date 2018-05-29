Imports Microsoft.VisualBasic
Imports System.Web
Imports system.Web.HttpContext
Imports System.Configuration
Imports Lixil.JHS_EKKS.DataAccess
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Public Class NinsyouBC

    Private Const ACCESSDENIED As String = "401 access denied"
    Private Const LOGON_USER As String = "LOGON_USER"
    Private Const HTTP_UID As String = "HTTP_UID"
    Private Const REMOTE_ADDR As String = "REMOTE_ADDR"

    ''' <summary>
    ''' IceWall の IP リストを取得します。
    ''' </summary>
    ''' <returns>取得したIPを1次元配列で返します。</returns>
    Private Function GetIceWallIpList() As String()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

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
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)
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
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)
        Dim response As System.Web.HttpResponse = HttpContext.Current.Response
        response.Status = ACCESSDENIED
        response.End()
    End Sub

    ''' <summary>
    ''' ユーザがログオンしているかどうかを判別します。
    ''' </summary>
    ''' <returns>ユーザがログオンしている場合は True、そうでない場合は False を返します。</returns>
    Public Function IsUserLogon() As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)
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
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)
        For Each iceWallIp As String In Me.GetIceWallIpList()
            If (HttpContext.Current.Request. _
                ServerVariables(REMOTE_ADDR).Contains(iceWallIp)) Then
                Return True
            End If
        Next
        Return False
    End Function
   
    '''<summary>ログインユーザーIDより権限管理マスタを取得します</summary>
    Public Function GetUserInfo(ByVal strAccountNo As String) As LoginUserInfoList
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strAccountNo)
        Dim NinsyouDA As New NinsyouDA
        Return GetMapArray(NinsyouDA.SelUserData(strAccountNo))
    End Function
    '''<summary>データテープルはリストを変換する</summary>
    Public Function GetMapArray(ByVal dtUserInfo As DataTable) As LoginUserInfoList

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtUserInfo)
        Dim LoginUserInfoList As New LoginUserInfoList

        ' データテーブルの件数分処理を実施

        For intCount As Integer = 0 To dtUserInfo.Rows.Count - 1
            Dim list As New LoginUserInfoBC
            list.AccountNo = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(0))
            list.Account = dtUserInfo.Rows(intCount).Item(1).ToString
            list.Torikesi = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(2))
            list.Simei = dtUserInfo.Rows(intCount).Item(3).ToString
            list.SyozokuBusyo = dtUserInfo.Rows(intCount).Item(4).ToString
            list.Bikou = dtUserInfo.Rows(intCount).Item(5).ToString
            list.EigyouKeikakuKenriSansyou = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(6))
            list.UriYojituKanriSansyou = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(7))
            list.ZensyaKeikakuKengen = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(8))
            list.SitenbetuNenKeikakuKengen = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(9))
            list.SitenbetuGetujiKeikakuTorikomi = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(10))
            list.SitenbetuGetujiKeikakuKakutei = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(11))
            list.KeikakuMinaosiKengen = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(12))
            list.KeikakuKakuteiKengen = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(13))
            list.KeikakuTorikomiKengen = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(14))
            list.SitenbetuGetujiKeikakuMinaosi = Convert.ToInt32(dtUserInfo.Rows(intCount).Item(15))
            LoginUserInfoList.Items.Add(list)
        Next
        Return LoginUserInfoList

    End Function
End Class
