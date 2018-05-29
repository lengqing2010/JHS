Imports Itis.Earth.DataAccess
Public Class KakusyuDataSyuturyokuMenuLogic

    'インスタンス生成
    Private KakusyuDataSyuturyokuMenuDA As New KakusyuDataSyuturyokuMenuDataAccess

    ''' <summary>
    ''' システム時間
    ''' </summary>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetSysTime() As String
        Return KakusyuDataSyuturyokuMenuDA.GetSysTime()
    End Function

    ''' <summary>
    ''' Excel仕訳売上
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetExcelSiwakeUriage(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeUriageDataTableDataTable
        Return KakusyuDataSyuturyokuMenuDA.GetExcelSiwakeUriage(strDateFrom, strDateTo)
    End Function

    ''' <summary>
    ''' Excel仕訳仕入
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetExcelSiwakeSiire(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeSiireDataTableDataTable
        Return KakusyuDataSyuturyokuMenuDA.GetExcelSiwakeSiire(strDateFrom, strDateTo)
    End Function

    ''' <summary>
    ''' 売掛金残高表
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetUrikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.UrikakekinZandakaHyouDataTable
        Return KakusyuDataSyuturyokuMenuDA.GetUrikakekinZandakaHyou(strDateFrom, strDateTo)
    End Function

    ''' <summary>
    ''' Excel仕訳入金
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetExcelSiwakeNyuukin(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeNyuukinDataTableDataTable
        Return KakusyuDataSyuturyokuMenuDA.GetExcelSiwakeNyuukin(strDateFrom, strDateTo)
    End Function
    ''' <summary>
    ''' 請求先マスタのCSV情報取得
    ''' </summary>
    ''' <returns>請求先マスタCSVテーブル</returns>
    ''' <remarks>請求先マスタのCSV情報のデータ</remarks>
    ''' <history>2010/07/12 車龍(大連情報システム部)　新規作成</history>
    Public Function Getm_seikyuu_sakiCSV() As KakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTableDataTable
        'データ取得
        Return KakusyuDataSyuturyokuMenuDA.Selm_seikyuu_sakiCSV()
    End Function

    ''' <summary>
    ''' 調査会社マスタのCSV情報取得
    ''' </summary>
    ''' <returns>調査会社マスタCSVテーブル</returns>
    ''' <remarks>調査会社マスタのCSV情報のデータ</remarks>
    ''' <history>2010/07/13 車龍(大連情報システム部)　新規作成</history>
    Public Function Getm_tyousakaisyaCSV() As KakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTableDataTable
        'データ取得
        Return KakusyuDataSyuturyokuMenuDA.Selm_tyousakaisyaCSV()
    End Function

    ''' <summary>
    ''' 商品マスタのCSV情報取得
    ''' </summary>
    ''' <returns>商品マスタCSVテーブル</returns>
    ''' <remarks>商品マスタのCSV情報のデータ</remarks>
    ''' <history>2010/07/13 車龍(大連情報システム部)　新規作成</history>
    Public Function Getm_syouhinCSV() As KakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTableDataTable
        'データ取得
        Return KakusyuDataSyuturyokuMenuDA.Selm_syouhinCSV()
    End Function

    ''' <summary>
    ''' 銀行マスタのCSV情報取得
    ''' </summary>
    ''' <returns>銀行マスタCSVテーブル</returns>
    ''' <remarks>銀行マスタのCSV情報のデータ</remarks>
    ''' <history>2010/07/14 車龍(大連情報システム部)　新規作成</history>
    Public Function Getm_ginkouCSV() As KakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTableDataTable
        'データ取得
        Return KakusyuDataSyuturyokuMenuDA.Selm_ginkouCSV()
    End Function

    ''' <summary>
    ''' 買掛金残高表csv出力のデータを取得
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/20 趙東莉(大連情報システム部)　新規作成</history>
    Public Function GetKaikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.KaikakekinZandakaHyouDataTable
        'データ取得
        Return KakusyuDataSyuturyokuMenuDA.SelKaikakekinZandakaHyou(strDateFrom, strDateTo)
    End Function

    ''' <summary>
    ''' 売上データ出力のCSV情報取得
    ''' </summary>
    ''' <returns>売上データ出力CSVテーブル</returns>
    ''' <remarks>売上データ出力のCSV情報のデータ</remarks>
    ''' <history>
    ''' 2010/07/15 車龍(大連情報システム部)　新規作成
    ''' 2015/03/03 曹敬仁(大連情報システム部)　修正
    ''' </history>
    Public Function Geturiage_data_syuturyokuCSV(ByVal fromDate As String, _
                                             ByVal toDate As String, _
                                             ByVal lstSeikyuuSakiCd As List(Of String), _
                                             ByVal lstSeikyuuSakiBrc As List(Of String), _
                                             ByVal lstSeikyuuSakiKbn As List(Of String) _
                                            ) As KakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTableDataTable
        'Public Function Geturiage_data_syuturyokuCSV(ByVal fromDate As String, _
        '                                             ByVal toDate As String, _
        '                                             ByVal seikyuu_saki_cd As String, _
        '                                             ByVal seikyuu_saki_brc As String, _
        '                                             ByVal seikyuu_saki_kbn As String _
        '                                            ) As KakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTableDataTable

        'データ取得
        'Return KakusyuDataSyuturyokuMenuDA.Seluriage_data_syuturyokuCSV(fromDate, toDate, seikyuu_saki_cd, seikyuu_saki_brc, seikyuu_saki_kbn)
        Return KakusyuDataSyuturyokuMenuDA.Seluriage_data_syuturyokuCSV(fromDate, toDate, lstSeikyuuSakiCd, lstSeikyuuSakiBrc, lstSeikyuuSakiKbn)
    End Function

    ''' <summary>
    ''' 仕入データ出力のCSV情報取得
    ''' </summary>
    ''' <returns>仕入データ出力CSVテーブル</returns>
    ''' <remarks>仕入データ出力のCSV情報のデータ</remarks>
    ''' <history>2010/07/16 車龍(大連情報システム部)　新規作成</history>
    Public Function Getsiire_data_syuturyokuCSV(ByVal fromDate As String, _
                                                ByVal toDate As String, _
                                                ByVal strSiireCd As String _
                                                ) As KakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTableDataTable

        'データ取得
        Return KakusyuDataSyuturyokuMenuDA.Selsiire_data_syuturyokuCSV(fromDate, toDate, strSiireCd)
    End Function
    ''' <summary>
    ''' 請求先コードの情報をSeikyuuSakiInfoRecordクラスのList(Of SeikyuuSakiInfoRecord)で取得します
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="seikyuuSakiMei">請求先名</param>
    ''' <param name="seikyuuSakiKana">請求先カナ</param>
    ''' <returns>SeikyuuSakiInfoRecordクラスのList</returns>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                       ByVal seikyuuSakiBrc As String, _
                                       ByVal seikyuuSakiKbn As String, _
                                       ByVal seikyuuSakiMei As String, _
                                       ByVal seikyuuSakiKana As String, _
                                       ByRef allCount As Integer, _
                                       Optional ByVal startRow As Integer = 1, _
                                       Optional ByVal endRow As Integer = 99999999, _
                                       Optional ByVal blnTorikesi As Boolean = False) As DataTable

        Return KakusyuDataSyuturyokuMenuDA.searchSeikyuuSakiInfo(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, seikyuuSakiMei, seikyuuSakiKana, blnTorikesi)

    End Function

    ''' <summary>
    ''' 調査会社マスタ検索処理タイプ
    ''' </summary>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Enum EnumTyousakaisyaKensakuType
        ''' <summary>
        ''' 調査会社
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSAKAISYA = 0
        ''' <summary>
        ''' 仕入先
        ''' </summary>
        ''' <remarks></remarks>
        SIIRESAKI = 1
        ''' <summary>
        ''' 支払先
        ''' </summary>
        ''' <remarks></remarks>
        SIHARAISAKI = 2
        ''' <summary>
        ''' 工事会社
        ''' </summary>
        ''' <remarks></remarks>
        KOUJIKAISYA = 3
    End Enum

    ''' <summary>
    ''' 調査会社マスタ検索
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社ｺｰﾄﾞ</param>
    ''' <param name="strJigyousyoCd">事業所ｺｰﾄﾞ</param>
    ''' <param name="strTysKaiNm">調査会社名</param>
    ''' <param name="strTysKaiKana">調査会社名ｶﾅ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="kameitenCd">加盟店コード([任意]可否区分チェック用)</param>
    ''' <param name="kensakuType">検索タイプ([任意](EnumTyousakaisyakensakuType))</param>
    ''' <returns></returns>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetTyousakaisyaSearchResult(ByVal strTysKaiCd As String, _
                                                ByVal strJigyousyoCd As String, _
                                                ByVal strTysKaiNm As String, _
                                                ByVal strTysKaiKana As String, _
                                                ByVal blnDelete As Boolean, _
                                                Optional ByVal kameitenCd As String = "", _
                                                Optional ByVal kensakuType As EnumTyousakaisyaKensakuType = EnumTyousakaisyaKensakuType.TYOUSAKAISYA _
                                                ) As DataTable


        Return KakusyuDataSyuturyokuMenuDA.GetTyousakaisyaKensakuData(strTysKaiCd, _
                                                                    strJigyousyoCd, _
                                                                    strTysKaiNm, _
                                                                    strTysKaiKana, _
                                                                    blnDelete, _
                                                                    kameitenCd, _
                                                                    kensakuType _
                                                                    )
    End Function

End Class
