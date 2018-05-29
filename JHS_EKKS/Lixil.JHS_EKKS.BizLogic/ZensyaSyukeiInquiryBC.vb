Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

Public Class ZensyaSyukeiInquiryBC
    Private zensyaSyukeiInquiryDA As New DataAccess.ZensyaSyukeiInquiryDA
    
    '''' <summary>
    '''' データ有無
    '''' </summary>
    '''' <param name="strKeikakuNendo">計画_年度</param>
    '''' <returns>年度データ</returns>
    '''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    'Public Function GetNendoData(ByVal strKeikakuNendo As String) As Data.DataTable
    '    'EMAB障害対応情報の格納処理
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
    '    Return zensyaSyukeiInquiryDA.SelNendoData(strKeikakuNendo)
    'End Function

    ''' <summary>
    ''' 4月～3月の計画件数の集計値、計画金額の集計値、計画粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画_年度</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetNendoKeikaku(ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return zensyaSyukeiInquiryDA.SelNendoKeikaku(strKeikakuNendo)

    End Function

    '''' <summary>
    '''' データ有無
    '''' </summary>
    '''' <param name="strKeikakuNendo">計画_年度</param>
    '''' <returns>年度データ</returns>
    '''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    'Public Function GetNendoKensuuData(ByVal strKeikakuNendo As String) As Data.DataTable
    '    'EMAB障害対応情報の格納処理
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
    '    Return zensyaSyukeiInquiryDA.SelNendoKensuuData(strKeikakuNendo)
    'End Function

    ''' <summary>
    ''' 選択年度に応じた年度の「実績件数」
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画_年度</param>
    ''' <returns>実績件数集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetNendoJissekiKensuu(ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return zensyaSyukeiInquiryDA.SelNendoJissekiKensuu(strKeikakuNendo)

    End Function

    ''' <summary>
    ''' 実績金額、実績粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画_年度</param>
    ''' <returns>実績金額、実績粗利の集計値のデータ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetNendoJisseki(ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return zensyaSyukeiInquiryDA.SelNendoJisseki(strKeikakuNendo)

    End Function

    ''' <summary>
    ''' 期間計画件数の集計値、計画金額の集計値、計画粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="strKikan">期間</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetKikanKeikaku(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)

        Return zensyaSyukeiInquiryDA.SelKikanKeikaku(strKeikakuNendo, strKikan)

    End Function

    ''' <summary>
    ''' 期間実績件数の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="strKikan">期間</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetKikanJissekiKensuu(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)

        Return zensyaSyukeiInquiryDA.SelKikanJissekiKensuu(strKeikakuNendo, strKikan)

    End Function

    ''' <summary>
    ''' 期間実績金額、粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="strKikan">期間</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetKikanJisseki(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)

        Return zensyaSyukeiInquiryDA.SelKikanJisseki(strKeikakuNendo, strKikan)

    End Function

    ''' <summary>
    ''' 計画件数、計画金額、計画粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="intBegin">月から</param>
    ''' <param name="intEnd">月まで</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetTukiKeikaku(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)

        Return zensyaSyukeiInquiryDA.SelTukiKeikaku(strKeikakuNendo, intBegin, intEnd)

    End Function

    ''' <summary>
    ''' 実績件数の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="intBegin">月から</param>
    ''' <param name="intEnd">月まで</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetTukiJissekiKensuu(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)

        Return zensyaSyukeiInquiryDA.SelTukiJissekiKensuu(strKeikakuNendo, intBegin, intEnd)

    End Function

    ''' <summary>
    ''' 実績金額、実績粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="intBegin">月から</param>
    ''' <param name="intEnd">月まで</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetTukiJisseki(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)

        Return zensyaSyukeiInquiryDA.SelTukiJisseki(strKeikakuNendo, intBegin, intEnd)

    End Function

End Class
