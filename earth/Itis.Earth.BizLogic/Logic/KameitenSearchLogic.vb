
''' <summary>
''' 加盟店マスタ検索
''' </summary>
''' <remarks></remarks>
Public Class KameitenSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "加盟店マスタ検索"
    ''' <summary>
    ''' 加盟店マスタ検索
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenNm">加盟店名</param>
    ''' <param name="strKameitenKana">加盟店カナ</param>
    ''' <param name="strKameitenTourokuJuusyo">加盟店登録住所</param>
    ''' <param name="strKameitenTelNo">加盟店電話番号</param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <param name="allRowCount">検索結果全件数</param>
    ''' <param name="startRow">（任意）データ抽出時の開始行(1件目は1を指定)Default:1</param>
    ''' <param name="endRow">（任意）データ抽出時の終了行 Default:99999999</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKameitenSearchResult(ByVal strKubun As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strKameitenNm As String, _
                                            ByVal strKameitenKana As String, _
                                            ByVal strKameitenTourokuJuusyo As String, _
                                            ByVal strKameitenTelNo As String, _
                                            ByVal blnTorikesi As Boolean, _
                                            ByRef allRowCount As Integer, _
                                            Optional ByVal startRow As Integer = 1, _
                                            Optional ByVal endRow As Integer = 99999999) As List(Of KameitenSearchRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenSearchResult", _
                                                    strKubun, _
                                                    strKameitenCd, _
                                                    strKameitenNm, _
                                                    strKameitenKana, _
                                                    strKameitenTourokuJuusyo, _
                                                    strKameitenTelNo, _
                                                    blnTorikesi, _
                                                    allRowCount, _
                                                    startRow, _
                                                    endRow)

        Dim dataAccess As KameitenSearchDataAccess = New KameitenSearchDataAccess

        'データアクセス実行
        Dim table As DataTable = dataAccess.GetKameitenKensakuData(strKubun, _
                                                                   strKameitenCd, _
                                                                   strKameitenNm, _
                                                                   strKameitenKana, _
                                                                   strKameitenTourokuJuusyo, _
                                                                   strKameitenTelNo, _
                                                                   blnTorikesi)

        ' 件数を設定
        allRowCount = table.Rows.Count
        Dim clsDMH As New DataMappingHelper
        Dim arrRtnData As List(Of KameitenSearchRecord) = _
                                    clsDMH.getMapArray(Of KameitenSearchRecord)(GetType(KameitenSearchRecord), table)

        Return arrRtnData
    End Function

    ''' <summary>
    ''' 加盟店マスタ検索(少条件)
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <param name="allRowCount">検索結果全件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKameitenSearchResult(ByVal strKubun As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal blnTorikesi As Boolean, _
                                            ByRef allRowCount As Integer) As List(Of KameitenSearchRecord)


        '条件を補完して、親メソッドを使用
        Return GetKameitenSearchResult(strKubun, _
                                       strKameitenCd, _
                                       String.Empty, _
                                       String.Empty, _
                                       String.Empty, _
                                       String.Empty, _
                                       blnTorikesi, _
                                       allRowCount)

    End Function

    ''' <summary>
    ''' 加盟店マスタ検索(Key)
    ''' </summary>
    ''' <param name="strKbn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKameitenRecord(ByVal strKbn As String _
                                      , ByVal strKameitenCd As String _
                                      , Optional ByVal blnTorikesi As Boolean = False) As KameitenSearchRecord

        Dim list As New List(Of KameitenSearchRecord)
        Dim recData As KameitenSearchRecord
        Dim allRowCount As Integer = 0

        If strKameitenCd <> String.Empty Then
            list = Me.GetKameitenSearchResult(strKbn, _
                                              strKameitenCd, _
                                              blnTorikesi, _
                                              allRowCount)
        End If

        If allRowCount = 1 Then
            recData = list(0)
            Return recData
        Else
            Return Nothing
        End If

    End Function
#End Region

#Region "加盟店マスタ検索(デフォルト請求先)"
    ''' <summary>
    ''' 加盟店マスタ検索(デフォルト請求先)
    ''' </summary>
    ''' <param name="strKameitencd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKameitenDefaultSeikyuuSakiInfo(ByVal strKameitencd As String) As KameitenDefaultSeikyuuSakiInfoRecord
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenSearchResult", _
                                                    strKameitencd)
        Dim dtAcc As New KameitenDataAccess
        Dim dtResult As DataTable
        Dim rec As New KameitenDefaultSeikyuuSakiInfoRecord

        dtResult = dtAcc.getKameitenDefaultSeikyuuSakiInfo(strKameitencd)
        If dtResult.Rows.Count = 1 Then
            rec = DataMappingHelper.Instance.getMapArray(Of KameitenDefaultSeikyuuSakiInfoRecord)(GetType(KameitenDefaultSeikyuuSakiInfoRecord), dtResult)(0)
        Else
            rec = Nothing
        End If

        Return rec
    End Function
#End Region

#Region "加盟店マスタ検索（依頼画面表示用）"
    ''' <summary>
    ''' 加盟店マスタ検索
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTyousakaisyaCd">調査会社コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKameitenSearchResult(ByVal strKubun As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strTyousakaisyaCd As String, _
                                            Optional ByVal blnDelete As Boolean = True) As KameitenSearchRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenSearchResult", _
                                                    strKubun, _
                                                    strKameitenCd, _
                                                    strTyousakaisyaCd, _
                                                    blnDelete)

        Dim dataAccess As KameitenSearchDataAccess = New KameitenSearchDataAccess

        Dim table As DataTable = dataAccess.GetKameitenKensakuData(strKubun, strKameitenCd, strTyousakaisyaCd, blnDelete)
        Dim record As New KameitenSearchRecord

        Dim arrRtnData As List(Of KameitenSearchRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of KameitenSearchRecord)(GetType(KameitenSearchRecord), table)

        If arrRtnData.Count > 0 Then
            record = arrRtnData(0)
        End If

        Return record
    End Function
#End Region

#Region "工事会社NG情報取得"
    ''' <summary>
    ''' 工事会社NG情報取得を行う
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKoujikaisyaCd">工事会社コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKoujiKaisyaNGResult(ByVal strKubun As String, _
                                           ByVal strKameitenCd As String, _
                                           ByVal strKoujikaisyaCd As String, _
                                           Optional ByVal blnDelete As Boolean = True) As KameitenSearchRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiKaisyaNGResult", _
                                                    strKubun, _
                                                    strKameitenCd, _
                                                    strKoujikaisyaCd, _
                                                    blnDelete)

        Dim dataAccess As KameitenSearchDataAccess = New KameitenSearchDataAccess

        Dim table As DataTable = dataAccess.GetKoujiKaisyaNGData(strKubun, strKameitenCd, strKoujikaisyaCd, blnDelete)
        Dim record As New KameitenSearchRecord

        Dim arrRtnData As List(Of KameitenSearchRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of KameitenSearchRecord)(GetType(KameitenSearchRecord), table)

        If arrRtnData.Count > 0 Then
            record = arrRtnData(0)
        End If

        Return record
    End Function
#End Region

#Region "付保証明書情報取得"
    ''' <summary>
    ''' 付保証明書情報を取得する
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnDelete">取消フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFuhoSyoumeisyoInfo(ByVal strKubun As String, _
                                          ByVal strKameitenCd As String, _
                                          Optional ByVal blnDelete As Boolean = True) As KameitenSearchRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetFuhoSyoumeisyoInfo", _
                                                    strKubun, _
                                                    strKameitenCd, _
                                                    blnDelete)

        Dim dataAccess As KameitenSearchDataAccess = New KameitenSearchDataAccess

        Dim table As DataTable = dataAccess.GetFuhoSyoumeisyoData(strKubun, strKameitenCd, blnDelete)
        Dim record As New KameitenSearchRecord

        Dim arrRtnData As List(Of KameitenSearchRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of KameitenSearchRecord)(GetType(KameitenSearchRecord), table)

        If arrRtnData.Count > 0 Then
            record = arrRtnData(0)
        End If

        Return record
    End Function
#End Region

#Region "ビルダー注意事項チェック"
    ''' <summary>
    ''' 加盟店のビルダー注意事項に地盤診断費用負担(=13)が存在するかのチェックを行なう。
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function ChkBuilderData13(ByVal strKameitenCd As String) As Boolean
        Dim dataAccess As New BuilderDataAccess

        If dataAccess.ChkBuilderData13(strKameitenCd) Then
            Return True
        End If

        Return False
    End Function
#End Region

#Region "ビルダー注意事項チェック55"
    ''' <summary>
    ''' 加盟店のビルダー注意事項に(=55)が存在するかのチェックを行なう。
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function ChkBuilderData55(ByVal strKameitenCd As String) As Boolean
        Dim dataAccess As New BuilderDataAccess

        If dataAccess.ChkBuilderData55(strKameitenCd) Then
            Return True
        End If

        Return False
    End Function
#End Region


#Region "営業所・系列コード取得処理"
    ''' <summary>
    ''' 加盟店コードを元に営業所コード・系列コードを取得する
    ''' </summary>
    ''' <param name="strKameitenCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEigyousyoKeiretuCd(ByVal strKameitenCd As String) As KameitenSearchRecord
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetEigyousyoKeiretuCd", strKameitenCd)

        Dim dataAccess As KameitenSearchDataAccess = New KameitenSearchDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコード
        Dim recResult As New KameitenSearchRecord

        dTblResult = dataAccess.GetEigyousyoKeiretuCd(strKameitenCd)

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of KameitenSearchRecord)(GetType(KameitenSearchRecord), dTblResult)(0)
        End If

        Return recResult
    End Function
#End Region

End Class
