Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports Itis.Earth.DataAccess
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 認証・アカウントデータの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class AccountDataAccess

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' アカウントレコードを取得します
    ''' </summary>
    ''' <param name="login_user_id">ログインユーザーID</param>
    ''' <returns>アカウントデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function getAccountData(ByVal login_user_id As String) As AccountDataSet.AccountTableDataTable

        ' パラメータ
        Const strParamLoginUser As String = "@LOGINUSER"

        Dim commandText As String = " SELECT  " & _
                                    "     N.login_user_id, " & _
                                    "     A.account_no, " & _
                                    "     A.account, " & _
                                    "     M.DisplayName AS simei, " & _
                                    "     A.bikou, " & _
                                    "     A.irai_gyoumu_kengen, " & _
                                    "     A.kekka_gyoumu_kengen, " & _
                                    "     A.hosyou_gyoumu_kengen, " & _
                                    "     A.hkks_gyoumu_kengen, " & _
                                    "     A.koj_gyoumu_kengen, " & _
                                    "     A.keiri_gyoumu_kengen, " & _
                                    "     A.kaiseki_master_kanri_kengen, " & _
                                    "     A.eigyou_master_kanri_kengen, " & _
                                    "     A.kkk_master_kanri_kengen, " & _
                                    "     A.hansoku_uri_kengen, " & _
                                    "     A.data_haki_kengen, " & _
                                    "     A.system_kanrisya_kengen, " & _
                                    "     A.sinki_nyuuryoku_kengen, " & _
                                    "     M.Department, " & _
                                    "     A.hattyuusyo_kanri_kengen " & _
                                    " FROM  " & _
                                    "     m_jiban_ninsyou N " & _
                                    " LEFT JOIN " & _
                                    "     m_account A ON A.account_no = N.account_no " & _
                                    " LEFT OUTER JOIN " & _
                                    "     m_jhs_mailbox M ON M.PrimaryWindowsNTAccount = N.login_user_id " & _
                                    " WHERE " & _
                                    "      N.torikesi = 0  AND (A.torikesi =0 OR (A.account_no IS NULL AND  A.torikesi IS NULL )) " & _
                                    " AND " & _
                                    "     N.login_user_id = " & strParamLoginUser

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamLoginUser, SqlDbType.VarChar, 30, login_user_id)}

        ' データの取得
        Dim accountDataSet As New AccountDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            accountDataSet, accountDataSet.AccountTable.TableName, commandParameters)

        Dim AccountTable As AccountDataSet.AccountTableDataTable = AccountDataSet.AccountTable

        Return AccountTable

    End Function

End Class
