Imports Itis.Earth.DataAccess
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 地盤共通処理クラス
''' </summary>
''' <remarks></remarks>
Public Class JibanLogic


#Region "文字列のバイト数を取得（Shift-JIS）"
    ''' <summary>
    ''' 文字列のバイト数を取得（Shift-JIS）
    ''' </summary>
    ''' <param name="str">対象文字列</param>
    ''' <returns>Integer：バイト数</returns>
    ''' <remarks></remarks>
    Public Function getStrByteSJIS(ByVal str As String) As Integer

        'Shift-JISでのバイト数を取得
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

    End Function
#End Region



End Class
