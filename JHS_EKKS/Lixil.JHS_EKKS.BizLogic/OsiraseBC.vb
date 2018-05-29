Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.DataAccess
Public Class OsiraseBC
    Private OsiraseDA As New OsiraseDA
#Region "お知らせ情報の取得"

    ''' <summary>
    ''' お知らせデータを取得します
    ''' </summary>
    ''' <returns>お知らせデータのレコードリスト</returns>
    ''' <remarks></remarks>
    Public Function GetOsiraseRecords() As List(Of OsiraseRecord)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        ' 戻り値となるリスト
        Dim returnRec As New List(Of OsiraseRecord)

        ' 重複データ取得クラス
        Dim dataAccess As DataTable = OsiraseDA.GetOsiraseData

        ' 値が取得できた場合、戻り値に設定する
        For i As Integer = 0 To dataAccess.Rows.Count - 1
            Dim osiraseRec As New OsiraseRecord

            osiraseRec.Nengappi = CType(dataAccess.Rows(i).Item(0), Date)
            osiraseRec.NyuuryokuBusyo = dataAccess.Rows(i).Item(1).ToString
            osiraseRec.NyuuryokuMei = dataAccess.Rows(i).Item(2).ToString
            osiraseRec.HyoujiNaiyou = dataAccess.Rows(i).Item(3).ToString
            osiraseRec.LinkSaki = dataAccess.Rows(i).Item(4).ToString
            ' リストにセット
            returnRec.Add(osiraseRec)
        Next


        Return returnRec

    End Function
#End Region
End Class
