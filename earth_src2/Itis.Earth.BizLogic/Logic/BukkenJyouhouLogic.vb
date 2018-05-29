Imports Itis.Earth.DataAccess
''' <summary>物件情報検索する</summary>
''' <remarks>物件情報検索機能を提供する</remarks>
''' <history>
''' <para>2009/10/27　高(大連情報システム部)　新規作成</para>
''' </history>
Public Class BukkenJyouhouLogic

    ''' <summary>加盟店物件情報照会クラスのインスタンス生成 </summary>
    Private bukkenJyouhouDataAccess As New BukkenJyouhouDataAccess

    ''' <summary> 物件情報取得</summary>
    ''' <param name="dtParam">Paramデータセート</param>
    ''' <returns>物件情報のデータ</returns>
    Public Function GetBukkenJyouhouInfo(ByVal dtParam As DataTable) As BukkenJyouhouDataSet.BukkenJyouhouTableDataTable
        'データ取得
        Return bukkenJyouhouDataAccess.SelBukkenJyouhouInfo(dtParam)
    End Function

    ''' <summary>加盟店情報データ個数を取得する</summary>
    ''' <param name="dtParam">Paramデータセート</param>
    ''' <returns>加盟店情報データ個数</returns>
    Public Function GetBukkenJyouhouInfoCount(ByVal dtParam As DataTable) As Integer
        'データ取得
        Return bukkenJyouhouDataAccess.SelBukkenJyouhouInfoCount(dtParam)
    End Function

    ''' <summary> CSV情報取得</summary>
    ''' <param name="dtParam">Paramデータセート</param>
    ''' <returns>CSV情報のデータ</returns>
    Public Function GetBukkenJyouhouInfoCSV(ByVal dtParam As DataTable) As BukkenJyouhouDataSet.BukkenJyouhouCSVTableDataTable
        'データ取得
        Return bukkenJyouhouDataAccess.SelBukkenJyouhouInfoCSV(dtParam)
    End Function
End Class
