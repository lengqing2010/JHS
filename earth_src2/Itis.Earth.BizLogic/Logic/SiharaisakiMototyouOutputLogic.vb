Imports Itis.Earth.DataAccess
''' <summary>請求先元帳帳票出力処理</summary>
''' <history>
''' <para>2010/07/14　王霽陽(大連情報システム部)　新規作成</para>
''' </history>
Public Class SiharaisakiMototyouOutputLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Private siharaisakiMototyouOutputDataAccess As New SiharaisakiMototyouOutputDataAccess

#Region "支払先元帳_繰越残高取得"
    ''' <summary>
    ''' 支払先元帳_繰越残高取得
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strFromDate">年月日FROM</param>
    ''' <returns>繰越残高(Long型)</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiSakiMototyouKurikosiZan(ByVal strTysKaisyaCd As String, _
                                                      ByVal strFromDate As String _
                                                      ) As Long


        Dim Data As Object
        Dim retData As Long

        'データ取得
        Data = siharaisakiMototyouOutputDataAccess.SelSiharaiSakiMototyouKurikosiZan(strTysKaisyaCd, strFromDate)

        ' 取得出来ない場合、空白を返却
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'Longにキャスト
                retData = CType(Data, Long)
            Catch ex As Exception
                '失敗したらゼロ
                retData = 0
            End Try
        End If

        '値戻し
        Return retData

    End Function

    ''' <summary>仕入データテーブル、支払データテーブルデータを取得する</summary>
    ''' <param name="strTysKaisyaCd"> 調査会社コード</param>
    ''' <param name="strJigyousyoCd"> 支払集計先事業所コード</param>
    ''' <param name="strFromDate"> 抽出期間FROM(YYYY/MM/DD)</param>
    ''' <param name="strToDate"> 抽出期間TO(YYYY/MM/DD)</param>
    ''' <returns>売上、入金のデータ</returns>
    Public Function GetSiharaiSakiMototyouData(ByVal strTysKaisyaCd As String, _
                                                   ByVal strJigyousyoCd As String, _
                                                   ByVal strFromDate As String, _
                                                   ByVal strToDate As String) As Data.DataTable
        'データ取得
        Return siharaisakiMototyouOutputDataAccess.SelSiharaiSakiMototyouData(strTysKaisyaCd, strJigyousyoCd, strFromDate, strToDate)

    End Function
#End Region

End Class
