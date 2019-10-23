
''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class SyouhinSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 商品情報を取得します
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSyouhinNm">商品名</param>
    ''' <param name="IsSyouhin2">商品２の場合:True 商品３の場合:False</param>
    ''' <param name="allRowCount">検索結果全件数</param>
    ''' <param name="TyousaHouhouNo">調査方法No</param>
    ''' <param name="startRow">（任意）データ抽出時の開始行(1件目は1を指定)Default:1</param>
    ''' <param name="endRow">（任意）データ抽出時の終了行 Default:99999999</param>
    ''' <returns>List(Of Syouhin23Record)</returns>
    ''' <remarks>
    ''' <example>商品情報を取得して結果を設定するサンプルコード（商品２の場合）
    ''' <code>
    ''' Dim total_row As Integer<br/>
    ''' ' 商品２の情報を１件目から１００件目まで取得する場合
    ''' For Each data As Syouhin23Record IN getSyouhinInfo(True, total_row, 1, 100)<br/>
    '''     [商品コードの設定先] = data.SyouhinCd <br/>
    '''     [商品名の設定先] = data.SyouhinMei <br/>
    ''' Next <br/>
    ''' </code>
    ''' </example>
    ''' </remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinNm As String, _
                                    ByVal IsSyouhin2 As Boolean, _
                                    ByRef allRowCount As Integer, _
                                    ByVal TyousaHouhouNo As Integer, _
                                    Optional ByVal startRow As Integer = 1, _
                                    Optional ByVal endRow As Integer = 99999999) As List(Of Syouhin23Record)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo", _
                                            strSyouhinCd, _
                                            strSyouhinNm, _
                                            IsSyouhin2, _
                                            TyousaHouhouNo, _
                                            allRowCount, _
                                            startRow, _
                                            endRow)

        Dim logic As New JibanLogic
        Dim list As List(Of Syouhin23Record)

        If IsSyouhin2 = True Then
            ' 商品２レコードを全て取得します
            list = logic.GetSyouhin23(strSyouhinCd, _
                                      strSyouhinNm, _
                                      EarthEnum.EnumSyouhinKubun.Syouhin2_110, _
                                      allRowCount, _
                                      Integer.MinValue, _
                                      "", _
                                      startRow, _
                                      endRow)
        Else
            ' 商品３レコードを全て取得します
            list = logic.GetSyouhin23(strSyouhinCd, _
                          strSyouhinNm, _
                          EarthEnum.EnumSyouhinKubun.Syouhin3, _
                          allRowCount, _
                          TyousaHouhouNo, _
                          "", _
                          startRow, _
                          endRow)

        End If

        Return list

    End Function

    ''' <summary>
    ''' 商品情報を取得します
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSyouhinNm">商品名</param>
    ''' <param name="intSyouhinKbn">商品区分</param>
    ''' <param name="allRowCount">検索結果全件数</param>
    ''' <param name="startRow">（任意）データ抽出時の開始行(1件目は1を指定)Default:1</param>
    ''' <param name="endRow">（任意）データ抽出時の終了行 Default:99999999</param>
    ''' <returns>List(Of SyouhinMeisaiRecord)</returns>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, _
                                ByVal strSyouhinNm As String, _
                                ByVal intSyouhinKbn As Integer, _
                                ByRef allRowCount As Integer, _
                                Optional ByVal startRow As Integer = 1, _
                                Optional ByVal endRow As Integer = 99999999) As List(Of SyouhinMeisaiRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo", _
                                            strSyouhinCd, _
                                            strSyouhinNm, _
                                            intSyouhinKbn, _
                                            allRowCount, _
                                            startRow, _
                                            endRow)

        Dim dataAccess As New SyouhinDataAccess
        Dim logic As New JibanLogic
        Dim list As New List(Of SyouhinMeisaiRecord)
        Dim dtlSyouhin As New DataTable

        ' 商品情報の取得
        dtlSyouhin = dataAccess.GetSyouhinInfo(strSyouhinCd, strSyouhinNm, intSyouhinKbn)

        ' 件数を設定
        allRowCount = dtlSyouhin.Rows.Count

        ' データを取得し、List(Of SyouhinMeisaiRecord)に格納する
        If allRowCount > 0 Then
            list = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), _
                                                          dtlSyouhin, _
                                                          startRow, _
                                                          endRow)
        End If

        Return list

    End Function

    ''' <summary>
    ''' 倉庫コードを元に商品情報を取得します
    ''' </summary>
    ''' <param name="intMode">倉庫コードor商品2</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinSoukoCd(ByVal intMode As Integer) As List(Of SyouhinMeisaiRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinSoukoCd", _
                                                    intMode)

        Dim dataAccess As New SyouhinDataAccess
        Dim dtlSyouhin As New DataTable
        Dim list As New List(Of SyouhinMeisaiRecord)

        ' 商品情報の取得
        dtlSyouhin = dataAccess.GetSyouhinSoukoCd(intMode)

        ' データを取得し、List(Of SyouhinMeisaiRecord)に格納する
        If dtlSyouhin.Rows.Count > 0 Then
            list = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), dtlSyouhin)
        End If

        Return list

    End Function

End Class
