''' <summary>
''' 調査会社マスタ検索
''' </summary>
''' <remarks></remarks>
Public Class TyousakaisyaSearchLogic

    Dim earthEnum As New EarthEnum

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

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
    ''' <remarks></remarks>
    Public Function GetTyousakaisyaSearchResult(ByVal strTysKaiCd As String, _
                                                ByVal strJigyousyoCd As String, _
                                                ByVal strTysKaiNm As String, _
                                                ByVal strTysKaiKana As String, _
                                                ByVal blnDelete As Boolean, _
                                                Optional ByVal kameitenCd As String = "", _
                                                Optional ByVal kensakuType As EarthEnum.EnumTyousakaisyaKensakuType = earthEnum.EnumTyousakaisyaKensakuType.TYOUSAKAISYA _
                                                ) As List(Of TyousakaisyaSearchRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousakaisyaSearchResult", _
                                            strTysKaiCd, _
                                            strJigyousyoCd, _
                                            strTysKaiNm, _
                                            blnDelete, _
                                            kameitenCd, _
                                            kensakuType)

        Dim dataAccess As TyousakaisyaSearchDataAccess = New TyousakaisyaSearchDataAccess

        Dim arrRtnData As List(Of TyousakaisyaSearchRecord) = DataMappingHelper.Instance.getMapArray(Of TyousakaisyaSearchRecord)(GetType(TyousakaisyaSearchRecord), _
                                                              dataAccess.GetTyousakaisyaKensakuData(strTysKaiCd, _
                                                                                                    strJigyousyoCd, _
                                                                                                    strTysKaiNm, _
                                                                                                    strTysKaiKana, _
                                                                                                    blnDelete, _
                                                                                                    kameitenCd, _
                                                                                                    kensakuType _
                                                                                                    ))

        Return arrRtnData
    End Function

    ''' <summary>
    ''' 調査会社マスタ検索結果件数取得
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社ｺｰﾄﾞ</param>
    ''' <param name="strJigyousyoCd">事業所ｺｰﾄﾞ</param>
    ''' <param name="strTysKaiNm">調査会社名</param>
    ''' <param name="strTysKaiKana">調査会社名ｶﾅ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="kameitenCd">加盟店コード([任意]可否区分チェック用)</param>
    ''' <param name="kensakuType">検索タイプ([任意](EnumTyousakaisyakensakuType))</param>
    ''' <returns>検索結果件数取得</returns>
    ''' <remarks></remarks>
    Public Function GetTyousakaisyaSearchResultCnt(ByVal strTysKaiCd As String, _
                                                ByVal strJigyousyoCd As String, _
                                                ByVal strTysKaiNm As String, _
                                                ByVal strTysKaiKana As String, _
                                                ByVal blnDelete As Boolean, _
                                                Optional ByVal kameitenCd As String = "", _
                                                Optional ByVal kensakuType As EarthEnum.EnumTyousakaisyaKensakuType = earthEnum.EnumTyousakaisyaKensakuType.TYOUSAKAISYA _
                                                ) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousakaisyaSearchResultCnt", _
                                            strTysKaiCd, _
                                            strJigyousyoCd, _
                                            strTysKaiNm, _
                                            blnDelete, _
                                            kameitenCd, _
                                            kensakuType)

        Dim dtAcc As TyousakaisyaSearchDataAccess = New TyousakaisyaSearchDataAccess

        Dim intCnt As Integer = dtAcc.GetTyousakaisyaKensakuDataCnt(strTysKaiCd, strJigyousyoCd, strTysKaiNm, strTysKaiKana, blnDelete, kameitenCd, kensakuType)

        Return intCnt
    End Function

    ''' <summary>
    ''' 調査会社マスタ検索(工事会社取得用に複製)
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社ｺｰﾄﾞ</param>
    ''' <param name="strJigyousyoCd">事業所ｺｰﾄﾞ</param>
    ''' <param name="strTysKaiNm">調査会社名</param>
    ''' <param name="strTysKaiKana">調査会社名ｶﾅ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="kameitenCd">加盟店コード([任意]可否区分チェック用)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKoujikaishaSearchResult(ByVal strTysKaiCd As String, _
                                               ByVal strJigyousyoCd As String, _
                                               ByVal strTysKaiNm As String, _
                                               ByVal strTysKaiKana As String, _
                                               ByVal blnDelete As Boolean, _
                                               ByVal kameitenCd As String) As List(Of TyousakaisyaSearchRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousakaisyaSearchResult", _
                                                    strTysKaiCd, _
                                                    strJigyousyoCd, _
                                                    strTysKaiNm, _
                                                    blnDelete, _
                                                    kameitenCd)

        '調査会社マスタ検索を使用
        Return GetTyousakaisyaSearchResult(strTysKaiCd, _
                                           strTysKaiCd, _
                                           strJigyousyoCd, _
                                           strTysKaiKana, _
                                           blnDelete, _
                                           kameitenCd, _
                                           earthEnum.EnumTyousakaisyaKensakuType.KOUJIKAISYA)

    End Function

    ''' <summary>
    ''' 調査会社名を取得します
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">調査会社事業所コード</param>
    ''' <param name="blnDelete">取消しを含まない場合True</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTyousaKaisyaMei(ByVal strTysKaiCd As String, _
                                       ByVal strJigyousyoCd As String, _
                                       ByVal blnDelete As Boolean) As String

        ' Key情報無しの場合、検索しないで終了
        If strTysKaiCd Is Nothing Or strJigyousyoCd Is Nothing Then
            Return ""
        End If

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousaKaisyaMei", _
                                                    strTysKaiCd, _
                                                    strJigyousyoCd, _
                                                    blnDelete)

        Dim dataAccess As TyousakaisyaSearchDataAccess = New TyousakaisyaSearchDataAccess

        ' 調査会社名を取得
        Dim tyousaKaisyaMei As String = dataAccess.GetTyousaKaisyaMei(strTysKaiCd, strJigyousyoCd, blnDelete)

        If tyousaKaisyaMei Is Nothing Then
            tyousaKaisyaMei = ""
        End If

        Return tyousaKaisyaMei

    End Function

    ''' <summary>
    ''' 調査会社のSDS保持情報を取得します
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">調査会社事業所コード</param>
    ''' <param name="blnDelete">取消しを含まない場合True</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTyousaKaisyaSDS(ByVal strTysKaiCd As String, _
                                       ByVal strJigyousyoCd As String, _
                                       ByVal blnDelete As Boolean) As Integer

        ' Key情報無しの場合、検索しないで終了
        If strTysKaiCd Is Nothing Or strJigyousyoCd Is Nothing Then
            Return ""
        End If

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousaKaisyaSDS", _
                                                    strTysKaiCd, _
                                                    strJigyousyoCd, _
                                                    blnDelete)

        Dim dataAccess As TyousakaisyaSearchDataAccess = New TyousakaisyaSearchDataAccess

        ' 調査会社のSDS保持情報を取得
        Dim tyousaKaisyaSDS As Integer = dataAccess.GetTyousaKaisyaSDS(strTysKaiCd, strJigyousyoCd, blnDelete)

        Return tyousaKaisyaSDS

    End Function

    ''' <summary>
    ''' 調査会社情報を取得
    ''' </summary>
    ''' <param name="strKaisyaCd">調査会社コード(会社コード + 事業所コード)</param>
    ''' <returns>調査会社レコード</returns>
    ''' <remarks></remarks>
    Public Function getTysKaisyaInfo(ByVal strKaisyaCd) As TysKaisyaRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getTysKaisyaInfo", _
                                                    strKaisyaCd)
        Dim tysKaisyaDataAcc As New TyousakaisyaSearchDataAccess
        Dim dtbl As DataTable
        Dim dataRec As TysKaisyaRecord

        '調査会社情報を取得
        dtbl = tysKaisyaDataAcc.searchTysKaisyaInfo(strKaisyaCd)
        If dtbl.Rows.Count > 0 Then
            dataRec = DataMappingHelper.Instance.getMapArray(Of TysKaisyaRecord)(GetType(TysKaisyaRecord), dtbl, 1, dtbl.Rows.Count)(0)
        Else
            dataRec = New TysKaisyaRecord
        End If

        Return dataRec
    End Function

    ''' <summary>
    ''' 調査会社情報を取得
    ''' </summary>
    ''' <param name="strKaisyaCd">調査会社コード(会社コード + 事業所コード)</param>
    ''' <returns>調査会社レコード</returns>
    ''' <remarks></remarks>
    Public Function getTysKaisyaInfo(ByVal strKaisyaCd As String, ByVal strJigyousyoCd As String) As TysKaisyaRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getTysKaisyaInfo", _
                                                    strKaisyaCd, strJigyousyoCd)
        Dim tysKaisyaDataAcc As New TyousakaisyaSearchDataAccess
        Dim dtbl As DataTable
        Dim dataRec As New TysKaisyaRecord

        '調査会社情報を取得
        dtbl = tysKaisyaDataAcc.searchTysKaisyaInfo(strKaisyaCd, strJigyousyoCd)
        If dtbl.Rows.Count > 0 Then
            dataRec = DataMappingHelper.Instance.getMapArray(Of TysKaisyaRecord)(GetType(TysKaisyaRecord), dtbl, 1, dtbl.Rows.Count)(0)
        Else
            dataRec = Nothing
        End If

        Return dataRec
    End Function

    ''' <summary>
    ''' 新会計支払先マスタ
    ''' </summary>
    ''' <param name="strSkkJigyousyoCd">新会計事業所コード</param>
    ''' <param name="strSkkShriSakiCd">新会計支払先コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSkkSiharaisakiSearchResult(ByVal strSkkJigyousyoCd As String, ByVal strSkkShriSakiCd As String) As List(Of SinkaikeiSiharaiSakiRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousakaisyaSearchResult", _
                                            strSkkJigyousyoCd, _
                                            strSkkShriSakiCd)

        Dim dataAccess As TyousakaisyaSearchDataAccess = New TyousakaisyaSearchDataAccess
        Dim dtRet As New DataTable

        dtRet = dataAccess.GetSkkSiharaisakiKensakuData(strSkkJigyousyoCd, strSkkShriSakiCd)

        Dim arrRtnData As List(Of SinkaikeiSiharaiSakiRecord) = DataMappingHelper.Instance.getMapArray(Of SinkaikeiSiharaiSakiRecord)(GetType(SinkaikeiSiharaiSakiRecord), dtRet)

        Return arrRtnData
    End Function

End Class
