Imports Itis.Earth.DataAccess

''' <summary>加盟店営業情報を検索する</summary>
''' <remarks>加盟店営業情報検索機能を提供する</remarks>
''' <history>
''' <para>2009/07/16　高雅娟(大連情報システム部)　新規作成</para>
''' </history>
Public Class EigyouJyouhouInquiryLogic

    ''' <summary> 加盟店営業情報クラスのインスタンス生成 </summary>
    Private EigyouJyouhouDataAccess As New EigyouJyouhouInquiryDataAccess

    ''' <summary>
    ''' 組織レベルを取得する。
    ''' </summary>
    ''' <returns>組織レベルデータテーブル</returns>
    Public Function GetSosikiLabelInfo() As EigyouJyouhouDataSet.sosikiLabelDataTable
        Return EigyouJyouhouDataAccess.selSosikiLevel()
    End Function
    Public Function GetSosikiLabelInfo2(ByVal strSosiki As String, ByVal strSosiki2 As String, ByVal strBusyoCd As String, ByVal strBusyoCd2 As String, ByVal strKbn As String) As EigyouJyouhouDataSet.sosikiLabelDataTable
        Return EigyouJyouhouDataAccess.selSosikiLevel2(strSosiki, strSosiki2, strBusyoCd, strBusyoCd2, strKbn)
    End Function
    ''' <summary>
    ''' 部署コードと名称を取得する。
    ''' </summary>
    ''' <param name="strSosikiLevel">組織レベル</param>
    ''' <returns>部署情報データテーブル</returns>
    Public Function GetbusyoCdInfo(ByVal strSosikiLevel As String) As EigyouJyouhouDataSet.busyoCdDataTable
        Return EigyouJyouhouDataAccess.selBusyoCd(strSosikiLevel)
    End Function
    Public Function GetbusyoCdInfo2(ByVal strSosikiLevel As String, ByVal strBusyoCd As String, ByVal strSansyouBusyoCd As String) As EigyouJyouhouDataSet.busyoCdDataTable
        Return EigyouJyouhouDataAccess.selBusyoCd2(strSosikiLevel, strBusyoCd, strSansyouBusyoCd)
    End Function

    ''' <summary>
    ''' ログインユーザーの営業マン区分を取得する。
    ''' </summary>
    ''' <param name="strUserId">ログインユーザーID</param>
    ''' <returns>ログインユーザーの営業マン区分データテーブル</returns>
    Public Function GetEigyouManKbnInfo(ByVal strUserId As String) As EigyouJyouhouDataSet.eigyouManKbnDataTable
        Return EigyouJyouhouDataAccess.selEigyouManKbn(strUserId)
    End Function

    ''' <summary>
    ''' 加盟店営業情報データ総数を取得する。
    ''' </summary>
    ''' <param name="dtParamEigyouInfo">検索条件テーブル</param>
    ''' <returns>加盟店営業情報データ総数</returns>
    Public Function GetEigyouJyouhouCountInfo(ByVal dtParamEigyouInfo As EigyouJyouhouDataSet.paramEigyouJyouhouDataTable _
                                 , ByVal chkBusyoCd As Boolean) As Integer
        Return EigyouJyouhouDataAccess.selEigyouJyouhouCount(dtParamEigyouInfo, chkBusyoCd)
    End Function

    ''' <summary>
    ''' 加盟店営業情報を取得する。
    ''' </summary>
    ''' <param name="dtParamEigyouInfo">検索条件テーブル</param>
    ''' <returns>加盟店営業情報データテーブル</returns>
    Public Function GetEigyouJyouhouInfo(ByVal dtParamEigyouInfo As EigyouJyouhouDataSet.paramEigyouJyouhouDataTable _
                                    , ByVal chkBusyoCd As Boolean) As EigyouJyouhouDataSet.eigyouJyouhouDataTable
        Return EigyouJyouhouDataAccess.selEigyouJyouhou(dtParamEigyouInfo, chkBusyoCd)
    End Function



End Class
