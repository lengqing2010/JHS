Imports Itis.Earth.DataAccess
''' <summary>加盟店物件情報照会する</summary>
''' <remarks>加盟店物件情報照会機能を提供する</remarks>
''' <history>
''' <para>2009/07/15　馬艶軍(大連情報システム部)　新規作成</para>
''' </history>
Public Class BukkenJyouhouInquiryLogic

    ''' <summary>加盟店物件情報照会クラスのインスタンス生成 </summary>
    Private bukkenJyouhouInquiryDataAccess As New BukkenJyouhouInquiryDataAccess

    ''' <summary> 物件情報取得</summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>物件情報のデータ</returns>
    Public Function GetBukkenJyouhouInfo(ByVal strKameitenCd As String) As BukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTableDataTable
        'データ取得
        Return bukkenJyouhouInquiryDataAccess.SelBukkenJyouhouInfo(strKameitenCd)
    End Function

    ''' <summary>
    ''' 「取消」情報を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>「取消」データ</returns>
    ''' <hidtory>2012/03/28 車龍 405721案件の対応 追加</hidtory>
    Public Function GetTorikesi(ByVal strKameitenCd As String) As Data.DataTable

        '戻り値
        Return bukkenJyouhouInquiryDataAccess.SelTorikesi(strKameitenCd)

    End Function

End Class
