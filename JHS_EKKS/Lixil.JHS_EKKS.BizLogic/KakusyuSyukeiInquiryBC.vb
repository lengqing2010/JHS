Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Microsoft.VisualBasic
Imports System.Web
Imports system.Web.HttpContext
Imports System.Configuration
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 各種集計
''' </summary>
''' <remarks></remarks>
Public Class KakusyuSyukeiInquiryBC

    Private kakusyuSyukeiInquiryDA As New DataAccess.KakusyuSyukeiInquiryDA

    ''' <summary>
    ''' 月次ToListを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/04　李宇(大連情報システム部)　新規作成</history>
    Public Function GetTukinamiListData() As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return kakusyuSyukeiInquiryDA.SelTukinamiListData()

    End Function

    ''' <summary>
    ''' 画面明細を取得する
    ''' </summary>
    ''' <param name="strTodouhukenCd">都道府県コード</param>
    ''' <param name="strSitenCd">支店コード</param>
    ''' <param name="strEigyousyoBusyoCd">営業所コード</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strEigyouTantousyaId">営業マンコード</param>
    ''' <param name="strKameitenCd">登録事業者コード</param>
    ''' <param name="strNendo">年度</param>
    ''' <param name="intBegin">初め</param>
    ''' <param name="intEnd">終わり</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/27　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKakusyuSyukeiData(ByVal strSitenCd As String, _
                                         ByVal strTodouhukenCd As String, _
                                         ByVal strEigyousyoBusyoCd As String, _
                                         ByVal strKeiretuCd As String, _
                                         ByVal strEigyouTantousyaId As String, _
                                         ByVal strKameitenCd As String, _
                                         ByVal strNendo As String, _
                                         ByVal intBegin As Integer, _
                                         ByVal intEnd As Integer, _
                                         ByVal strEigyouKbn As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strTodouhukenCd, _
                                                                                            strEigyousyoBusyoCd, _
                                                                                            strKeiretuCd, _
                                                                                            strEigyouTantousyaId, _
                                                                                            strKameitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd, _
                                                                                            strEigyouKbn)


        Return kakusyuSyukeiInquiryDA.SelKakusyuSyukeiData(strSitenCd, _
                                                           strTodouhukenCd, _
                                                           strEigyousyoBusyoCd, _
                                                           strKeiretuCd, _
                                                           strEigyouTantousyaId, _
                                                           strKameitenCd, _
                                                           strNendo, _
                                                           intBegin, _
                                                           intEnd, _
                                                           strEigyouKbn)

    End Function

    ''' <summary>
    ''' 画面明細を取得する
    ''' </summary>
    ''' <param name="strSitenCd">支店コード</param>
    ''' <param name="strNendo">年度</param>
    ''' <param name="intBegin">初め</param>
    ''' <param name="intEnd">終わり</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/27　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKakusyuSyukeiFCData(ByVal strSitenCd As String, _
                                           ByVal strNendo As String, _
                                           ByVal intBegin As Integer, _
                                           ByVal intEnd As Integer) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd)


        Return kakusyuSyukeiInquiryDA.SelKakusyuSyukeiFCData(strSitenCd, _
                                                             strNendo, _
                                                             intBegin, _
                                                             intEnd)

    End Function


    Public Function GetKakusyuSyukeiSubeteData(ByVal strSitenCd As String, _
                                               ByVal strNendo As String, _
                                               ByVal intBegin As Integer, _
                                               ByVal intEnd As Integer, _
                                               ByVal strEigyouKbn As String) As Data.DataTable


        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd, _
                                                                                            strEigyouKbn)

        Return kakusyuSyukeiInquiryDA.SelKakusyuSyukeiSubeteData(strSitenCd, _
                                                                 strNendo, _
                                                                 intBegin, _
                                                                 intEnd, _
                                                                 strEigyouKbn)

    End Function

End Class
