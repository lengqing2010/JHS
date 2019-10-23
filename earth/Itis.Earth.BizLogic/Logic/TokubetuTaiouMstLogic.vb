Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 特別対応マスタLogicクラス
''' </summary>
''' <remarks></remarks>
Public Class TokubetuTaiouMstLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic
    '加盟店検索ロジック
    Dim ksLogic As New KameitenSearchLogic

#Region "特別対応マスタ取得"
    ''' <summary>
    ''' 特別対応マスタをベースに特別対応データを取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>特別対応マスタレコードのList(Of TokubetuTaiouMstRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetTokubetuTaiouInfo(ByVal sender As Object, _
                                       ByVal strKbn As String, _
                                       ByVal strHosyousyoNo As String, _
                                       ByVal strKameitenCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal strTysHouhouNo As String, _
                                       ByRef allCount As Integer) As List(Of TokubetuTaiouRecordBase)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouMstInfo", allCount)

        '検索実行クラス
        Dim dataAccess As New TokubetuTaiouMstDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '取得データ格納用リスト
        Dim list As New List(Of TokubetuTaiouRecordBase)
        '加盟店検索結果格納用レコード
        Dim recKameiten As New KameitenSearchRecord

        Try
            '加盟店コードを元に営業所・系列コードを取得
            recKameiten = ksLogic.GetEigyousyoKeiretuCd(strKameitenCd)

            '検索処理の実行
            dTblResult = dataAccess.GetTokubetuTaiouMstInfo(strKbn, _
                                                            strHosyousyoNo, _
                                                            strKameitenCd, _
                                                            recKameiten.EigyousyoCd, _
                                                            recKameiten.KeiretuCd, _
                                                            strSyouhinCd, _
                                                            strTysHouhouNo)

            ' 総件数をセット
            allCount = dTblResult.Rows.Count

            ' 検索結果を格納用リストにセット
            list = DataMappingHelper.Instance.getMapArray(Of TokubetuTaiouRecordBase)(GetType(TokubetuTaiouRecordBase), dTblResult)

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return list
    End Function

    ''' <summary>
    ''' 該当テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="intTokubetuTaiouCd">特別対応コード</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>DB更新用レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>PKで該当テーブルの1レコードを取得</remarks>
    Public Function GetTokubetuTaiouMstRec(ByVal sender As Object, _
                                            ByVal intTokubetuTaiouCd As Integer, _
                                            ByRef allCount As Integer _
                                            ) As TokubetuTaiouRecordBase
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouDataRec", _
                                                    sender, _
                                                    intTokubetuTaiouCd _
                                                    )
        'データアクセスクラス
        Dim clsDataAcc As New TokubetuTaiouMstDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードクラス
        Dim recResult As New TokubetuTaiouRecordBase

        Try
            If intTokubetuTaiouCd = Integer.MinValue Then
                Return Nothing
            End If

            '検索処理の実行
            dTblResult = clsDataAcc.GetTokubetuTaiouMstRec(intTokubetuTaiouCd)

            '総件数をセット
            allCount = dTblResult.Rows.Count

            If allCount = 0 Then
                Return Nothing
            Else
                ' 検索結果を格納用レコードクラスにセット
                recResult = DataMappingHelper.Instance.getMapArray(Of TokubetuTaiouRecordBase)(GetType(TokubetuTaiouRecordBase), dTblResult)(0)
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return recResult
    End Function

    ''' <summary>
    ''' 特別対応ツールチップ用/特別対応マスタをPKで取得します
    ''' </summary>
    ''' <returns>特別対応マスタのリスト</returns>
    ''' <remarks></remarks>
    Public Function GetTokubetuTaiouToolTip(ByVal sender As Object, _
                                            ByVal listMtt As List(Of TokubetuTaiouMstRecord) _
                                            ) As List(Of TokubetuTaiouMstRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouToolTip", _
                                                    sender, _
                                                    listMtt _
                                                    )
        '検索実行クラス
        Dim dataAccess As New TokubetuTaiouMstDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '取得データ格納用リスト
        Dim list As New List(Of TokubetuTaiouMstRecord)

        Try

            '検索処理の実行
            dTblResult = dataAccess.GetTokubetuTaiouToolTip(listMtt)

            ' 検索結果を格納用リストにセット
            list = DataMappingHelper.Instance.getMapArray(Of TokubetuTaiouMstRecord)(GetType(TokubetuTaiouMstRecord), dTblResult)

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
        End Try

        Return list
    End Function

#End Region

End Class
