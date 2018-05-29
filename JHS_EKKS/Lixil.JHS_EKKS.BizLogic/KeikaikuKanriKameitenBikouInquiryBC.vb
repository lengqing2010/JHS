Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

Public Class KeikaikuKanriKameitenBikouInquiryBC

    Private keikaikuKanriKameitenBikouInquiryDA As New DataAccess.KeikaikuKanriKameitenBikouInquiryDA

    ''' <summary>
    ''' 加盟店備考情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetBikouInfo(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        Return keikaikuKanriKameitenBikouInquiryDA.SelBikouInfo(strKameitenCd)

    End Function

    ''' <summary>
    ''' 加盟店備考更新日取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function GetKameitenBikouMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        Return keikaikuKanriKameitenBikouInquiryDA.SelKameitenBikouMaxUpdTime(strKameitenCd)

    End Function
        ''' <summary>
        ''' 加盟店種別取得
        ''' </summary>
        ''' <param name="code"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
    Public Function Getkameitensyubetu(ByVal code As String) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                code)

        Return keikaikuKanriKameitenBikouInquiryDA.Selkameitensyubetu(code)

    End Function

    ''' <summary>
    ''' 備考追加
    ''' </summary>
    ''' <param name="dicPrm">追加データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetInsBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.InsBikou(dicPrm)

    End Function

    ''' <summary>
    ''' 備考更新
    ''' </summary>
    ''' <param name="dicPrm">追加データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetUpdBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.UpdBikou(dicPrm)

    End Function

    ''' <summary>
    ''' 備考削除
    ''' </summary>
    ''' <param name="dicPrm">追加データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDelBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.DelBikou(dicPrm)

    End Function

End Class
