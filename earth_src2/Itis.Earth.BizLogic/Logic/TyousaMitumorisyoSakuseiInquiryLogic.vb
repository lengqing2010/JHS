Imports Itis.Earth.DataAccess
Imports System.Transactions

''' <summary>
''' 調査見積書作成指示
''' </summary>
''' <remarks></remarks>
''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
Public Class TyousaMitumorisyoSakuseiInquiryLogic

    'インスタンス生成
    Private TyousaMitumorisyoSakuseiInquiryDA As New TyousaMitumorisyoSakuseiInquiryDataAccess

    ''' <summary>
    ''' 表示住所 選択
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function GetJyuusyoInfo() As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelJyuusyoInfo()

    End Function

    ''' <summary>
    ''' 「見積書作成回数」を取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">見積書作成回数</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function GetSakuseiKaisuu(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelSakuseiKaisuu(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' 承認者 選択
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function GetSyouninSyaInfo() As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelSyouninSyaInfo()

    End Function

    ''' <summary>
    ''' 見積書の存在を判斷する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function GetMitumoriCount(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelMitumoriCount(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' 見積書作成回数を更新する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <param name="inMitSakuseiKaisuu">見積書作成回数</param>
    ''' <param name="inSyouhizeiHyouji">消費税表示</param>
    ''' <param name="inMooruTenkaiFlg">モール展開FLG</param>
    ''' <param name="inHyoujiJyuusyoNo">表示住所_管理No</param>
    ''' <param name="strTourokuId">担当者ID</param>
    ''' <param name="strTantousyaMei">担当者名</param>
    ''' <param name="strSyouninsyaId">承認者ID</param>
    ''' <param name="strSyouninsyaMei">承認者名</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 李宇(大連情報システム部) 新規作成</history>
    Public Function GetUpdMitumoriKaisu(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal inMitSakuseiKaisuu As Integer, _
                                    ByVal inSyouhizeiHyouji As Integer, _
                                    ByVal inMooruTenkaiFlg As Integer, _
                                    ByVal inHyoujiJyuusyoNo As Integer, _
                                    ByVal strTourokuId As String, _
                                    ByVal strTantousyaMei As String, _
                                    ByVal strSyouninsyaId As String, _
                                        ByVal strSyouninsyaMei As String, _
                                        ByVal strSakuseiDate As String, _
                                        ByVal strIraiTantousyaMei As String) As Boolean

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.UpdMitumoriKaisu(strKbn, strBultukennNo, inMitSakuseiKaisuu, inSyouhizeiHyouji, _
                                                                  inMooruTenkaiFlg, inHyoujiJyuusyoNo, strTourokuId, strTantousyaMei, _
                                                                  strSyouninsyaId, strSyouninsyaMei, strSakuseiDate, strIraiTantousyaMei)

    End Function

    ''' <summary>
    ''' 「調査見積書作成管理テーブル」に登録する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <param name="inMitSakuseiKaisuu">見積作成回数</param>
    ''' <param name="inSyouhizeiHyouji">消費税表示</param>
    ''' <param name="inMooruTenkaiFlg">モール展開FLG</param>
    ''' <param name="inHyoujiJyuusyoNo">表示住所_管理No</param>
    ''' <param name="strTourokuId">担当者ID</param>
    ''' <param name="strTantousyaMei">担当者名</param>
    ''' <param name="strSyouninsyaId">承認者ID</param>
    ''' <param name="strSyouninsyaMei">承認者名</param>
    ''' <param name="strSakuseiDate">調査見積書作成日</param>
    ''' <param name="strIraiTantousyaMei">調査見積書_依頼担当者</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/14 李宇(大連情報システム部) 新規作成</history>
    Public Function GetInsMitumoriKaisu(ByVal strKbn As String, _
                                        ByVal strBultukennNo As String, _
                                        ByVal inMitSakuseiKaisuu As Integer, _
                                        ByVal inSyouhizeiHyouji As Integer, _
                                        ByVal inMooruTenkaiFlg As Integer, _
                                        ByVal inHyoujiJyuusyoNo As Integer, _
                                        ByVal strTourokuId As String, _
                                        ByVal strTantousyaMei As String, _
                                        ByVal strSyouninsyaId As String, _
                                        ByVal strSyouninsyaMei As String, _
                                        ByVal strSakuseiDate As String, _
                                        ByVal strIraiTantousyaMei As String) As Boolean

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.InsMitumoriKaisu(strKbn, strBultukennNo, inMitSakuseiKaisuu, inSyouhizeiHyouji, _
                                                                  inMooruTenkaiFlg, inHyoujiJyuusyoNo, strTourokuId, strTantousyaMei, _
                                                                  strSyouninsyaId, strSyouninsyaMei, strSakuseiDate, strIraiTantousyaMei)

    End Function

    ''' <summary>
    ''' 担当者IDの存在を判斷する
    ''' </summary>
    ''' <param name="strTantousyaId">担当者ID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 李宇(大連情報システム部) 新規作成</history>
    Public Function GetSonzaiHandan(ByVal strTantousyaId As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelSonzaiHandan(strTantousyaId)

    End Function

    ''' <summary>
    ''' 承認者紐付け承認印管理マスタから【承認印】を取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function GetSyouninIn(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelSyouninIn(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' 担当者紐付け承認印管理マスタから【承認印】を取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function GetTantouIn(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelTantouIn(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' [Fixed Data Section]ONE
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Public Function GetKihonInfoOne(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelKihonInfoOne(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' [Fixed Data Section]TWO
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Public Function GetKihonInfoTwo(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelKihonInfoTwo(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' 御見積書のデータ
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">物件番号</param>
    ''' <param name="flg">税抜と税込の区分</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Public Function GetTyouhyouDate(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal flg As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelTyouhyouDate(strKbn, strBultukennNo, flg)

    End Function

    ''' <summary>
    ''' 「モール展開」のセットを判斷する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">物件番号</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/12/02 李宇(大連情報システム部) 新規作成</history>
    Public Function GetMoruHandan(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'データ取得
        Return TyousaMitumorisyoSakuseiInquiryDA.SelMoruHandan(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' 税率を追加する
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2014/02/17 李宇(大連情報システム部) 新規作成</history>
    Public Function GetZeiritu(ByVal strKbn As String) As Data.DataTable

        'データを取得する
        Return TyousaMitumorisyoSakuseiInquiryDA.SelZeiritu(strKbn)

    End Function

    ''' <summary>
    ''' システム時間
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSysTime() As Date

        Return TyousaMitumorisyoSakuseiInquiryDA.SelSysTime()

    End Function


    ''' <summary>
    ''' 「調査見積書_依頼担当者」を取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetIraiTantousya(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        Return TyousaMitumorisyoSakuseiInquiryDA.SelIraiTantousya(strKbn, strBultukennNo)

    End Function

End Class
