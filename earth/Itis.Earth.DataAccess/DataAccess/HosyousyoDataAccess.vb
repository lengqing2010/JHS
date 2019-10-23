Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 保証書No発行に関係するデータアクセスクラスです
''' </summary>
''' <remarks></remarks>
Public Class HosyousyoDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 区分を引数に保証書No年月を取得を取得します
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <returns>保証書No年月</returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoNoYM(ByVal kubun As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoNoYM", _
                                    kubun)

        ' 戻り値
        Dim strHosyousyoYm As String = ""

        ' パラメータ
        Const strParamKubun As String = "@KUBUN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,hosyousyo_hak_date,hosyousyo_no_nengetu,hkks_hassou_date ")
        commandTextSb.Append("  FROM m_hiduke_save WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE kbn = " & strParamKubun)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, kubun)}

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Dim dTbl As New DataTable
        dTbl = cmnDtAcc.getDataTable(commandTextSb.ToString, commandParameters)
        If dTbl.Rows.Count > 0 Then
            If Not dTbl.Rows(0)("hosyousyo_no_nengetu") Is Nothing AndAlso Not dTbl.Rows(0)("hosyousyo_no_nengetu") Is DBNull.Value Then
                strHosyousyoYm = Format(dTbl.Rows(0)("hosyousyo_no_nengetu"), "yyyy/MM")
            End If
        End If

        Return strHosyousyoYm

    End Function

    ''' <summary>
    ''' 区分と年月を引数に保証書Noの最終番号を取得します
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strHosyousyoYm">保証書No年月</param>
    ''' <returns>最終番号情報</returns>
    ''' <remarks>取得できない場合、-1を返します</remarks>
    Public Function GetHosyousyoLastNo(ByVal strKubun As String, _
                                       ByVal strHosyousyoYm As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoLastNo", _
                                                    strKubun, _
                                                    strHosyousyoYm)

        ' パラメータ
        Const strParamKubun As String = "@KUBUN"
        Const strParamHosyouYm As String = "@HOSYOUYM"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,nengetu,saisyuu_no ")
        commandTextSb.Append("  FROM m_hosyousyo_no_saiban WITH(UPDLOCK) ")
        commandTextSb.Append("  WHERE kbn = " & strParamKubun)
        commandTextSb.Append("  AND   nengetu = " & strParamHosyouYm)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, strKubun), _
             SQLHelper.MakeParam(strParamHosyouYm, SqlDbType.Char, 6, strHosyousyoYm)}

        ' データの取得
        Dim HosyousyoNoDataSet As New HosyousyoNoDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            HosyousyoNoDataSet, HosyousyoNoDataSet.HosyousyoNoTable.TableName, commandParameters)

        Dim hosyousyoTable As HosyousyoNoDataSet.HosyousyoNoTableDataTable = _
                    HosyousyoNoDataSet.HosyousyoNoTable

        ' 取得できなかった場合は新規採番（戻り値に-1）
        If hosyousyoTable.Count = 0 Then
            Return -1
        End If

        ' 取得できた場合、行情報を取得する
        Dim row As HosyousyoNoDataSet.HosyousyoNoTableRow = hosyousyoTable(0)

        Return row.saisyuu_no

    End Function

    ''' <summary>
    ''' TBL_保証書NO採番に採番値を反映します
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strYm">年月</param>
    ''' <param name="intHosyousyoLastNo">最終番号</param>
    ''' <param name="strMode">処理モード N:登録 U:更新</param>
    ''' <param name="strLoginUserId">ログインユーザID</param>
    ''' <returns>更新結果</returns>
    ''' <remarks></remarks>
    Public Function UpdateHosyousyoNo(ByVal strKubun As String, _
                                      ByVal strYm As String, _
                                      ByVal intHosyousyoLastNo As Integer, _
                                      ByVal strMode As String, _
                                      ByVal strLoginUserId As String _
                                      ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateHosyousyoNo", _
                                            strKubun, _
                                            strYm, _
                                            intHosyousyoLastNo, _
                                            strMode, _
                                            strLoginUserId)

        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder

        ' パラメータ
        Const strPrmKbn As String = "@KBN" '区分
        Const strPrmNengetu As String = "@NENGETU" '年月
        Const strPrmLastNo As String = "@LAST_NO" '最終NO
        Const strPrmAddDatatime As String = "@ADD_DATETIME" '登録日時
        Const strPrmAddLoginUserID As String = "@ADD_LOGIN_USER_ID" '登録ログインユーザID
        Const strPrmUpdDatatime As String = "@UPD_DATETIME" '更新日時
        Const strPrmUpdLoginUserID As String = "@UPD_LOGIN_USER_ID" '更新ログインユーザID

        ' 登録モード "N" の場合、Insert
        If strMode = "N" Then

            cmdTextSb.Append(" INSERT INTO ")
            cmdTextSb.Append("      m_hosyousyo_no_saiban ")
            cmdTextSb.Append(" ( ")
            cmdTextSb.Append("      kbn ")
            cmdTextSb.Append("    , nengetu ")
            cmdTextSb.Append("    , saisyuu_no ")
            cmdTextSb.Append("    , add_login_user_id ")
            cmdTextSb.Append("    , add_datetime ")
            cmdTextSb.Append(" ) ")
            cmdTextSb.Append(" VALUES ")
            cmdTextSb.Append(" ( ")
            cmdTextSb.Append(strPrmKbn)
            cmdTextSb.Append("," & strPrmNengetu)
            cmdTextSb.Append("," & strPrmLastNo)
            cmdTextSb.Append("," & strPrmAddLoginUserID)
            cmdTextSb.Append("," & strPrmAddDatatime)
            cmdTextSb.Append(" ) ")

        Else 'UPDATE

            cmdTextSb.Append(" UPDATE ")
            cmdTextSb.Append("      m_hosyousyo_no_saiban ")
            cmdTextSb.Append(" SET ")
            cmdTextSb.Append("      saisyuu_no = " & strPrmLastNo)
            cmdTextSb.Append("      ,upd_login_user_id = " & strPrmUpdLoginUserID)
            cmdTextSb.Append("      ,upd_datetime = " & strPrmUpdDatatime)
            cmdTextSb.Append(" WHERE ")
            cmdTextSb.Append("      kbn = " & strPrmKbn)
            cmdTextSb.Append("  AND nengetu = " & strPrmNengetu)

        End If

        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
             SQLHelper.MakeParam(strPrmKbn, SqlDbType.Char, 1, strKubun), _
             SQLHelper.MakeParam(strPrmNengetu, SqlDbType.Char, 6, strYm), _
             SQLHelper.MakeParam(strPrmLastNo, SqlDbType.Int, 4, intHosyousyoLastNo), _
             SQLHelper.MakeParam(strPrmAddLoginUserID, SqlDbType.VarChar, 30, strLoginUserId), _
             SQLHelper.MakeParam(strPrmAddDatatime, SqlDbType.DateTime, 16, DateTime.Now), _
             SQLHelper.MakeParam(strPrmUpdLoginUserID, SqlDbType.VarChar, 30, strLoginUserId), _
             SQLHelper.MakeParam(strPrmUpdDatatime, SqlDbType.DateTime, 16, DateTime.Now) _
         }

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString, _
                                    cmdParams)

        Return intResult
    End Function
End Class
