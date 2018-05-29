Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 年度計画値設定
''' </summary>
''' <remarks>年度計画値設定</remarks>
''' <history>
''' <para>2012/11/14 P-44979 王新 新規作成 </para>
''' </history>
Public Class ZensyaSyukeiDetailsBC
    Private objZensyaSyukeiDetailsDA As New ZensyaSyukeiDetailsDA

    ''' <summary>
    ''' 年度比率管理テーブルから工事比率を取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>工事比率</returns>
    ''' <remarks>年度比率管理テーブルから工事比率を取得する</remarks>
    ''' <history>
    ''' <para>2012/11/28 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function GetKakoJittusekiKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return objZensyaSyukeiDetailsDA.SelKakoJittusekiKanriData(strKeikakuNendo)
    End Function

    ''' <summary>
    ''' 全社の詳細集計データを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="strEigyouKbn">営業区分</param>
    ''' <param name="arrList">月リスト</param>
    ''' <returns>詳細集計データ</returns>
    ''' <remarks>全社の詳細集計データを取得する</remarks>
    ''' <history>
    ''' <para>2012/11/28 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function GetSensyaSyuukeiData(ByVal strKeikakuNendo As String, _
                                         ByVal strEigyouKbn As String, _
                                         ByVal arrList As ArrayList) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return objZensyaSyukeiDetailsDA.SelSensyaSyuukeiData(strKeikakuNendo, strEigyouKbn, arrList)
    End Function

End Class
