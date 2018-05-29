Imports Itis.Earth.DataAccess

Public Class SeikyusyoFcwOutputLogic

    ''' <summary>
    ''' 請求書帳票出力データ検索する
    ''' </summary>
    ''' <param name="strSeikyusyo_no">請求先コード</param>
    ''' <returns>請求書帳票出力データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　孫微微(大連)　新規作成
    ''' </history>
    Public Function GetSeikyusyoData(ByVal strSeikyusyo_no As String) As Data.DataTable

        'データ取得
        Return (New SeikyusyoFcwOutputDataAccess).SelSeikyusyoFcwOutputData(strSeikyusyo_no)

    End Function

    ''' <summary>
    ''' 請求先書Noデータを取得する
    ''' </summary>
    ''' <param name="strSeikyusyo_no">請求先書No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　孫微微(大連)　新規作成
    ''' </history>
    Public Function GetSeikyusyoNoData(ByVal strSeikyusyo_no As String) As Data.DataTable

        'データ取得
        Return (New SeikyusyoFcwOutputDataAccess).SelSeikyusyoNoData(strSeikyusyo_no)

    End Function

    ''' <summary>
    ''' ログインユーザー情報を取得する。
    ''' </summary>
    ''' <param name="loginID">ログインユーザーコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　楚建偉(大連)　新規作成
    ''' </history>
    Public Function GetLoginUserName(ByVal loginID As String) As String

        'データ取得
        Return (New SeikyusyoFcwOutputDataAccess).SelLoginUserName(loginID)

    End Function

    ''' <summary>
    ''' 種類一の店名を取得する
    ''' </summary>
    ''' <param name="UA_kbn">区分</param>
    ''' <param name="UA_bangou">番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　楚建偉(大連)　新規作成
    ''' </history>
    Public Function GetHimodekeSyurui_1_tenmei(ByVal UA_kbn As String, ByVal UA_bangou As String) _
As Data.DataTable

        'データ取得
        Return (New SeikyusyoFcwOutputDataAccess).SelHimodekeSyurui_1_tenmei(UA_kbn, UA_bangou)

    End Function

    ''' <summary>
    ''' 種類二の店名を取得する
    ''' </summary>
    ''' <param name="UA_himodukecd_ten_cd">店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　楚建偉(大連)　新規作成
    ''' </history>
    Public Function GetHimodekeSyurui_2_tenmei(ByVal UA_himodukecd_ten_cd As String) _
As Data.DataTable

        'データ取得
        Return (New SeikyusyoFcwOutputDataAccess).SelHimodekeSyurui_2_tenmei(UA_himodukecd_ten_cd)

    End Function

    ''' <summary>
    ''' 種類三の店名を取得する
    ''' </summary>
    ''' <param name="UA_himodukecd_ten_cd">店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　楚建偉(大連)　新規作成
    ''' </history>
    Public Function GetHimodekeSyurui_3_tenmei(ByVal UA_himodukecd_ten_cd As String) _
As Data.DataTable

        'データ取得
        Return (New SeikyusyoFcwOutputDataAccess).SelHimodekeSyurui_3_tenmei(UA_himodukecd_ten_cd)

    End Function


End Class
