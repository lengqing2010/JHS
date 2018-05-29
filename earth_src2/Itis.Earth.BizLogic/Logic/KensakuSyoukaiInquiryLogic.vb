Imports Itis.Earth.DataAccess

''' <summary>加盟店情報を検索照会する</summary>
''' <remarks>加盟店検索照会機能を提供する</remarks>
''' <history>
''' <para>2009/07/15　付龍(大連情報システム部)　新規作成</para>
''' </history>
Public Class KensakuSyoukaiInquiryLogic
    ''' <summary>加盟店情報検索照会クラスのインスタンス生成 </summary>
    Private kensakuSyoukaiInquiryDA As New KensakuSyoukaiInquiryDataAccess

    ''' <summary>加盟店情報データを取得する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>加盟店情報データテーブル</returns>
    Public Function GetKameitenInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
            As KensakuSyoukaiInquiryDataSet.KameitenInfoTableDataTable
        Return kensakuSyoukaiInquiryDA.SelKameitenInfo(dtParamKameitenInfo)
    End Function

    ''' <summary>加盟店情報データ個数を取得する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>加盟店情報データ個数</returns>
    Public Function GetKameitenInfoCount(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As Integer
        Return kensakuSyoukaiInquiryDA.SelKameitenInfoCount(dtParamKameitenInfo)
    End Function

    ''' <summary>加盟店基本情報CSVデータを取得する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>加盟店基本情報CSVデータテーブル</returns>
    Public Function GetKihonJyouhouCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As Data.DataSet
        Return kensakuSyoukaiInquiryDA.SelKihonJyouhouCsvInfo(dtParamKameitenInfo)
    End Function

    ''' <summary>加盟店住所情報CSVデータを取得する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>加盟店住所情報CSVデータテーブル</returns>
    Public Function GetJyusyoJyouhouCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As Data.DataSet
        Return kensakuSyoukaiInquiryDA.SelJyusyoJyouhouCsvInfo(dtParamKameitenInfo)
    End Function

    ''' <summary>加盟店情報一括取込情報CSVデータを取得する</summary>
    ''' <returns>加盟店情報一括取込情報CSVデータテーブル</returns>
    Public Function GetKameitenJyuusyoCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) As Data.DataTable
        Return kensakuSyoukaiInquiryDA.SelKameitenJyuusyoCsvInfo(dtParamKameitenInfo)
    End Function

    ''' <summary>
    ''' 取引条件格納先管理マスタより、格納先ファイルパスを取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.22</history>
    Public Function GetKakunousakiFilePassJ(ByVal strKbn As String, ByVal strKameitenCd As String) As Data.DataTable
        Return kensakuSyoukaiInquiryDA.SelKakunousakiFilePassJ(strKbn, strKameitenCd)
    End Function

    ''' <summary>
    ''' 調査カード格納先管理マスタより、格納先ファイルパスを取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.22</history>
    Public Function GetKakunousakiFilePassC(ByVal strKbn As String, ByVal strKameitenCd As String) As Data.DataTable
        Return kensakuSyoukaiInquiryDA.SelKakunousakiFilePassC(strKbn, strKameitenCd)
    End Function

End Class
