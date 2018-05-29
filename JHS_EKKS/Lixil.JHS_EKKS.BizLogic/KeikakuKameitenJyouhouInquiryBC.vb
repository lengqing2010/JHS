Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Microsoft.VisualBasic
Imports System.Web
Imports system.Web.HttpContext
Imports System.Configuration
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 計画_加盟店情報照会
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKameitenJyouhouInquiryBC

    Private KeikakuKameitenJyouhouInquiryDA As New DataAccess.KeikakuKameitenJyouhouInquiryDA

    ''' <summary>
    ''' 基本情報データを取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/06　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKihonJyouSyutoku(ByVal strKameitenCd As String, ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikakuNendo)

        Return KeikakuKameitenJyouhouInquiryDA.SelKihonJyouSyutoku(strKameitenCd, strKeikakuNendo)

    End Function

    ''' <summary>
    ''' 計画管理用_加盟店情報データ取得
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/09　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKameitenJyouhou(ByVal strKameitenCd As String, ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikakuNendo)

        Return KeikakuKameitenJyouhouInquiryDA.SelKameitenJyouhou(strKameitenCd, strKeikakuNendo)

    End Function

    ''' <summary>
    ''' 画面の加盟店ｺｰﾄﾞが計画管理_加盟店情報マスタ.加盟店ｺｰﾄﾞに存在するかどうかを判断する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/09　李宇(大連情報システム部)　新規作成</history>
    Public Function GetCount(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd)

        Return KeikakuKameitenJyouhouInquiryDA.SelCount(strKameitenCd)

    End Function

    ''' <summary>
    ''' 「計画管理_加盟店情報ﾏｽﾀ」の追加処理
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKbnMei">区分名</param>
    ''' <param name="strKeikakuyouKameitenmei">計画用_加盟店名</param>
    ''' <param name="inGyoutai">業態</param>
    ''' <param name="strTouitsuHoujinCd">統一法人コード</param>
    ''' <param name="strHoujinCd">法人コード</param>
    ''' <param name="strKeikakuNayoseCd">計画名寄コード</param>
    ''' <param name="inKeikakuyouNenkanTousuu">計画用_年間棟数</param>
    ''' <param name="strKeikakujiEigyouTantousyaId">計画時_営業担当者</param>
    ''' <param name="strKeikakujiEigyouTantousyaMei">計画時_営業担当者名</param>
    ''' <param name="inKeikakuyouEigyouKbn">計画用_営業区分</param>
    ''' <param name="strKeikakuyouKannkatsuSiten">計画用_管轄支店</param>
    ''' <param name="strKeikakuyouKannkatsuSitenMei">計画用_管轄支店名</param>
    ''' <param name="strSdsKaisiNengetu">SDS開始年月</param>
    ''' <param name="inKameitenZokusei1">加盟店属性1</param>
    ''' <param name="inKameitenZokusei2">加盟店属性2</param>
    ''' <param name="inKameitenZokusei3">加盟店属性3</param>
    ''' <param name="strKameitenZokusei4">加盟店属性4</param>
    ''' <param name="strKameitenZokusei5">加盟店属性5</param>
    ''' <param name="strKameitenZokusei6">加盟店属性6</param>
    ''' <param name="strAddLoginUserId">登録ログインユーザーID</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Public Function GetInsMKeikakuKameitenInfo(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strKbnMei As String, _
                                               ByVal strKeikakuyouKameitenmei As String, ByVal inGyoutai As String, ByVal strTouitsuHoujinCd As String, _
                                               ByVal strHoujinCd As String, ByVal strKeikakuNayoseCd As String, ByVal inKeikakuyouNenkanTousuu As String, _
                                               ByVal strKeikakujiEigyouTantousyaId As String, ByVal strKeikakujiEigyouTantousyaMei As String, _
                                               ByVal inKeikakuyouEigyouKbn As String, ByVal strKeikakuyouKannkatsuSiten As String, _
                                               ByVal strKeikakuyouKannkatsuSitenMei As String, ByVal strSdsKaisiNengetu As String, _
                                               ByVal inKameitenZokusei1 As String, ByVal inKameitenZokusei2 As String, _
                                               ByVal inKameitenZokusei3 As String, ByVal strKameitenZokusei4 As String, _
                                               ByVal strKameitenZokusei5 As String, ByVal strKameitenZokusei6 As String, _
                                               ByVal strAddLoginUserId As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strAddLoginUserId)

        Return KeikakuKameitenJyouhouInquiryDA.InsMKeikakuKameitenInfo(strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strAddLoginUserId)

    End Function

    ''' <summary>
    ''' 「計画管理_加盟店情報ﾏｽﾀ」の更新処理
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKbnMei">区分名</param>
    ''' <param name="strKeikakuyouKameitenmei">計画用_加盟店名</param>
    ''' <param name="inGyoutai">業態</param>
    ''' <param name="strTouitsuHoujinCd">統一法人コード</param>
    ''' <param name="strHoujinCd">法人コード</param>
    ''' <param name="strKeikakuNayoseCd">計画名寄コード</param>
    ''' <param name="inKeikakuyouNenkanTousuu">計画用_年間棟数</param>
    ''' <param name="strKeikakujiEigyouTantousyaId">計画時_営業担当者</param>
    ''' <param name="strKeikakujiEigyouTantousyaMei">計画時_営業担当者名</param>
    ''' <param name="inKeikakuyouEigyouKbn">計画用_営業区分</param>
    ''' <param name="strKeikakuyouKannkatsuSiten">計画用_管轄支店</param>
    ''' <param name="strKeikakuyouKannkatsuSitenMei">計画用_管轄支店名</param>
    ''' <param name="strSdsKaisiNengetu">SDS開始年月</param>
    ''' <param name="inKameitenZokusei1">加盟店属性1</param>
    ''' <param name="inKameitenZokusei2">加盟店属性2</param>
    ''' <param name="inKameitenZokusei3">加盟店属性3</param>
    ''' <param name="strKameitenZokusei4">加盟店属性4</param>
    ''' <param name="strKameitenZokusei5">加盟店属性5</param>
    ''' <param name="strKameitenZokusei6">加盟店属性6</param>
    ''' <param name="strUpdLoginUserId">更新ログインユーザーID</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Public Function GetUpdMKeikakuKameitenInfo(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strKbnMei As String, _
                                               ByVal strKeikakuyouKameitenmei As String, ByVal inGyoutai As String, ByVal strTouitsuHoujinCd As String, _
                                               ByVal strHoujinCd As String, ByVal strKeikakuNayoseCd As String, ByVal inKeikakuyouNenkanTousuu As String, _
                                               ByVal strKeikakujiEigyouTantousyaId As String, ByVal strKeikakujiEigyouTantousyaMei As String, _
                                               ByVal inKeikakuyouEigyouKbn As String, ByVal strKeikakuyouKannkatsuSiten As String, _
                                               ByVal strKeikakuyouKannkatsuSitenMei As String, ByVal strSdsKaisiNengetu As String, _
                                               ByVal inKameitenZokusei1 As String, ByVal inKameitenZokusei2 As String, _
                                               ByVal inKameitenZokusei3 As String, ByVal strKameitenZokusei4 As String, _
                                               ByVal strKameitenZokusei5 As String, ByVal strKameitenZokusei6 As String, _
                                               ByVal strUpdLoginUserId As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strUpdLoginUserId)

        Return KeikakuKameitenJyouhouInquiryDA.UpdMKeikakuKameitenInfo(strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strUpdLoginUserId)

    End Function

    ''' <summary>
    ''' 計画管理_加盟店情報マスタの排他処理
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKameitenMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        Return KeikakuKameitenJyouhouInquiryDA.SelKameitenMaxUpdTime(strKameitenCd)

    End Function

    ''' <summary>
    ''' 営業担当者名を取得する
    ''' </summary>
    ''' <param name="strTantousyaId">営業担当者ID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/27　李宇(大連情報システム部)　新規作成</history>
    Public Function GetEigyouTantousyaMei(ByVal strTantousyaId As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strTantousyaId)

        Return KeikakuKameitenJyouhouInquiryDA.SelEigyouTantousyaMei(strTantousyaId)

    End Function

End Class
