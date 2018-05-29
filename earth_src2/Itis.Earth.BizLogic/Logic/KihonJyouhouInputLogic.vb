Imports Itis.Earth.DataAccess
Imports System.Transactions

''' <summary>加盟店情報を新規登録する</summary>
''' <remarks>加盟店情報新規登録を提供する</remarks>
''' <history>
''' <para>2009/07/15　付龍(大連情報システム部)　新規作成</para>
''' </history>
Public Class KihonJyouhouInputLogic

    ''' <summary>加盟店情報新規登録クラスのインスタンス生成 </summary>
    Private kihonJyouhouInputDA As New KihonJyouhouInputDataAccess

    ''' <summary>加盟店コードの最大値を取得する</summary>
    ''' <param name="strKbn">パラメータ</param>
    ''' <returns>加盟店コード最大値データテーブル</returns>
    Public Function GetMaxKameitenCd(ByVal strKbn As String, _
                                    Optional ByVal strCdFrom As String = "", _
                                    Optional ByVal strCdTo As String = "") As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        Return kihonJyouhouInputDA.SelMaxKameitenCd(strKbn, strCdFrom, strCdTo)
    End Function

    ''' <summary>加盟店コードの最大値と採番設定の範囲を取得する</summary>
    ''' <param name="strKbn">区分</param>
    ''' <returns>加盟店コードの最大値と採番設定の範囲を取得する</returns>
    ''' <history>
    ''' <para>2012/11/19　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetMaxKameitenCd1(ByVal strKbn As String) As Data.DataTable

        Return kihonJyouhouInputDA.SelMaxKameitenCd1(strKbn)
    End Function

    ''' <summary>加盟店コードを取得する</summary>
    ''' <param name="strKbn">パラメータ</param>
    ''' <param name="strCd">パラメータ</param>
    ''' <returns>加盟店コードデータテーブル</returns>
    Public Function GetKameitenCd(ByVal strKbn As String, ByVal strCd As String) As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        Return kihonJyouhouInputDA.SelKameitenCd(strKbn, strCd)
    End Function

    ''' <summary>加盟店マスタテーブルに登録する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>成否</returns>
    Public Function SetKameitenInfo(ByVal dtParamKameitenInfo As KihonJyouhouInputDataSet.Param_KameitenInfoDataTable) As Boolean
        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            If kihonJyouhouInputDA.InsKameitenInfo(dtParamKameitenInfo) = True AndAlso _
                kihonJyouhouInputDA.InsKameitenRenkeiInfo(dtParamKameitenInfo) = True Then
                scope.Complete()
                Return True
            Else
                scope.Dispose()
                Return False
            End If
        End Using
    End Function

    ''' <summary>
    ''' 「取消」ddlのデータを取得する
    ''' </summary>
    ''' <returns>「取消」ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/03/27 車龍 405721案件の対応 追加</history>
    Public Function GetTorikesiList(Optional ByVal strCd As String = "") As Data.DataTable

        '戻り値
        Return kihonJyouhouInputDA.SelTorikesiList(strCd)

    End Function

End Class
