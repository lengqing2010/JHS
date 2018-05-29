Imports Itis.Earth.DataAccess
Imports System.Data
Imports System.Data.SqlClient
''' <summary>請求先元帳帳票出力処理</summary>
''' <history>
''' <para>2010/07/14　王霽陽(大連情報システム部)　新規作成</para>
''' </history>
Public Class SeikyuSakiMototyouOutputLogic

    ''' <summary>請求先元帳帳票出力クラスのインスタンス生成 </summary>
    Private seikyuSakiMototyouOutputDataAccess As New SeikyuSakiMototyouOutputDataAccess

#Region "請求先元帳_繰越残高取得"
    ''' <summary>
    ''' 請求先元帳_繰越残高取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <returns>繰越残高(Long型)</returns>
    ''' <remarks></remarks>
    Public Function GetKurikosiZandaData(ByVal seikyuuSakiCd As String, _
                                                      ByVal seikyuuSakiBrc As String, _
                                                      ByVal seikyuuSakiKbn As String, _
                                                      ByVal fromDate As String _
                                                      ) As Long


        Dim Data As Data.DataTable
        Dim retData As Long

        'データ取得
        Data = seikyuSakiMototyouOutputDataAccess.SelKurikosiZandaData(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate)

        ' 取得出来ない場合、空白を返却
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'Longにキャスト
                retData = CType(Data.Rows(0).Item("kurikosi_zan").ToString.Trim, Long)
            Catch ex As Exception
                '失敗したらゼロ
                retData = 0
            End Try
        End If

        '値戻し
        Return retData

    End Function
#End Region

    ''' <summary>売上データテーブル、入金データテーブルデータを取得する</summary>
    ''' <param name="strSeikyuuSakiCd"> 請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc"> 請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn"> 請求先区分</param>
    ''' <param name="strFromDate"> 抽出期間FROM(YYYY/MM/DD)</param>
    ''' <param name="strToDate"> 抽出期間TO(YYYY/MM/DD)</param>
    ''' <returns>売上、入金のデータ</returns>
    Public Function GetUriageNyukinData(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String, ByVal strFromDate As String, ByVal strToDate As String) As Data.DataTable
        'データ取得
        Return SeikyuSakiMototyouOutputDataAccess.SelUriageNyukinData(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strFromDate, strToDate)

    End Function

End Class
