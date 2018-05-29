Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

Public Class KeikakuKanriKameitenKensakuSyoukaiInquiryBC

    Private keikakuKanriKameitenKensakuSyoukaiInquiryDA As New DataAccess.KeikakuKanriKameitenKensakuSyoukaiInquiryDA

    ''' <summary>
    ''' 区分情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetKbnInfo() As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelKbnInfo()

    End Function

    ''' <summary>
    ''' 管轄支店情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetSitenInfo() As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelSitenInfo()

    End Function

    ''' <summary>
    ''' 都道府県情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetTodoufukenInfo() As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelTodoufukenInfo()

    End Function

    ''' <summary>
    ''' 名称情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetMeisyouInfo(ByVal strMeisyouSyubetu As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strMeisyouSyubetu)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelMeisyouInfo(strMeisyouSyubetu)

    End Function

    ''' <summary>
    ''' 拡張名称情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetKakutyouMeisyouInfo(ByVal strMeisyouSyubetu As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strMeisyouSyubetu)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelKakutyouMeisyouInfo(strMeisyouSyubetu)

    End Function

    ''' <summary>
    ''' 加盟店明細情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetKameitenInfo(ByVal dicPrm As Dictionary(Of String, String)) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dicPrm)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelKameitenInfo(dicPrm)

    End Function

    ''' <summary>
    ''' 加盟店明細件数を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetKameitenCount(ByVal dicPrm As Dictionary(Of String, String)) As Integer

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dicPrm)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelKameitenCount(dicPrm)

    End Function

End Class
