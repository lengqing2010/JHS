
''' <summary>
''' 営業所マスタ検索
''' </summary>
''' <remarks></remarks>
''' 
Public Class EigyousyoSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 営業所マスタ検索
    ''' </summary>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strEigyousyoKana">営業所カナ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="allRowCount">検索結果全件数</param>
    ''' <param name="startRow">（任意）データ抽出時の開始行(1件目は1を指定)Default:1</param>
    ''' <param name="endRow">（任意）データ抽出時の終了行 Default:99999999</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEigyousyoSearchResult(ByVal strEigyousyoCd As String, _
                                    ByVal strEigyousyoKana As String, _
                                    ByVal blnDelete As Boolean, _
                                    ByRef allRowCount As Integer, _
                                    Optional ByVal startRow As Integer = 1, _
                                    Optional ByVal endRow As Integer = 99999999) As List(Of EigyousyoSearchRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetEigyousyoSearchResult", _
                                            strEigyousyoCd, _
                                            strEigyousyoKana, _
                                            blnDelete, _
                                            allRowCount, _
                                            startRow, _
                                            endRow)

        Dim dataAccess As EigyousyoSearchDataAccess = New EigyousyoSearchDataAccess

        '営業所マスタから検索条件を指定したテーブルを取得
        Dim table As DataTable = dataAccess.GetEigyosyoKensakuData(strEigyousyoCd, _
                                                                   strEigyousyoKana, _
                                                                   blnDelete)


        ' 件数を設定
        allRowCount = table.Rows.Count

        Dim arrRtnData As List(Of EigyousyoSearchRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of EigyousyoSearchRecord)(GetType(EigyousyoSearchRecord), table)

        Return arrRtnData
    End Function

End Class