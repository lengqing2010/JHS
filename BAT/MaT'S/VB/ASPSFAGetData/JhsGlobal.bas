Attribute VB_Name = "JhsGlobal"
Option Explicit

'******************************
'* ORACLE接続情報定義
'******************************
'*OLEｵﾌﾞｼﾞｪｸﾄ
Global gOraDatabase  As Object   'ﾃﾞｰﾀﾍﾞｰｽ接続情報 1
Global gOraSession   As Object   'ﾃﾞｰﾀﾍﾞｰｽ接続情報 2
Global gConnect      As String   'ﾃﾞｰﾀﾍﾞｰｽ接続情報 3
Global gDatabaseName As String   'ﾃﾞｰﾀﾍﾞｰｽ接続情報 4

Global gOraHostName As String    'ﾎｽﾄ名
Global gOraUserID   As String    'ﾕｰｻﾞID
Global gOraPassID   As String    'ﾊﾟｽﾜ-ﾄﾞ
Global gVersion     As String    'ｼｽﾃﾑﾊﾞｰｼﾞｮﾝ

Global gOwner     As String      'DBオンナ


