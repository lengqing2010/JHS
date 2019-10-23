Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 保証書管理テーブルの情報を取得します
''' </summary>
''' <remarks></remarks>
Public Class BukkenSintyokuJykyDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 該当テーブルの情報を取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSearchTable(ByVal strKbn As String, ByVal strHosyousyoNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchTable" _
                                                    , strKbn _
                                                    , strHosyousyoNo _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT ")
        cmdTextSb.Append("       kbn ")
        cmdTextSb.Append("       , hosyousyo_no ")
        cmdTextSb.Append("       , bukken_jyky ")
        cmdTextSb.Append("       , kaiseki_kanry ")
        cmdTextSb.Append("       , koj_umu ")
        cmdTextSb.Append("       , koj_kanry ")
        cmdTextSb.Append("       , nyuukin_kakunin_jyouken ")
        cmdTextSb.Append("       , nyuukin_jyky ")
        cmdTextSb.Append("       , kasi ")
        cmdTextSb.Append("       , hoken_kaisya ")
        cmdTextSb.Append("       , hoken_sinsei_tuki ")
        cmdTextSb.Append("       , hoken_sinsei_kbn ")
        cmdTextSb.Append("       , hw_mae_hkn ")
        cmdTextSb.Append("       , hw_mae_hkn_date ")
        cmdTextSb.Append("       , hw_mae_hkn_jissi_date ")
        cmdTextSb.Append("       , hw_mae_hkn_tekiyou_yotei_jissi_date ")
        cmdTextSb.Append("       , hw_ato_hkn ")
        cmdTextSb.Append("       , hw_ato_hkn_date ")
        cmdTextSb.Append("       , hw_ato_hkn_jissi_date ")
        cmdTextSb.Append("       , hw_ato_hkn_tekiyou_yotei_jissi_date ")
        cmdTextSb.Append("       , hw_ato_hkn_torikesi_syubetsu ")
        cmdTextSb.Append("       , syori_flg ")
        cmdTextSb.Append("       , syori_datetime ")
        cmdTextSb.Append("       , hosyousyo_type ")
        cmdTextSb.Append("       , gyoumu_kanry_date ")
        cmdTextSb.Append("       , hosyou_kaisi_gyoumu_naiyou ")
        cmdTextSb.Append("       , add_login_user_id ")
        cmdTextSb.Append("       , add_datetime ")
        cmdTextSb.Append("       , upd_login_user_id ")
        cmdTextSb.Append("       , upd_datetime")
        cmdTextSb.Append("       FROM t_hosyousyo_kanri ")
        cmdTextSb.Append("       WHERE kbn          = @KBN")
        cmdTextSb.Append("       AND hosyousyo_no = @HOSYOUSYONO")


        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strHosyousyoNo)}

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 物件毎の保証書管理状況の判定結果を取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkHosyousyoJyky(ByVal strKbn As String, ByVal strBangou As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkHosyousyoJyky" _
                                                    , strKbn _
                                                    , strBangou _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT [jhs_sys].[fnGetHosyousyoJyky](@KBN,@HOSYOUSYONO) ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strBangou)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return -1
        End If

        Return data

    End Function

    ''' <summary>
    ''' 保証書管理テーブル追加/更新処理(ストアドプロシージャ)
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">保証書NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setHosyousyoKanriData(ByVal strKbn As String, ByVal strBangou As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetHosyousyoKanriData" _
                                                    , strKbn _
                                                    , strBangou _
                                                    )

        Dim cmdParams() As SqlClient.SqlParameter   'パラメータ
        Dim commandText As String                   'ストアドプロシージャ
        Dim intResult As Integer = 0                

        'ストアドプロシージャ名を設定
        commandText = "[jhs_sys].[spSetHosyousyoKanri_bukken]"

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@COUNT_HK_INSERT", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@COUNT_HK_INSERT_UPDATE", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@COUNT_HK_BUKKEN_JYKY_UPDATE", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@COUNT_IF_UPDATE", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@ERR_NO", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@BANGOU", SqlDbType.VarChar, 10, strBangou), _
            SQLHelper.MakeParam("@ERR_NO_RETURN", SqlDbType.Int, 4, 0)}

        '戻り値格納用
        cmdParams(7).Direction = ParameterDirection.ReturnValue

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.StoredProcedure, _
                                    commandText, _
                                    cmdParams)

        Return cmdParams(7).Value

    End Function

    ''' <summary>
    ''' 入金予定日を取得
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">保証書NO</param>
    ''' <param name="dtHkUpdDatetime">保証書管理データ.更新日時</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setNyuukinYoteiDate(ByVal strKbn As String, ByVal strBangou As String, ByVal dtHkUpdDatetime As DateTime) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setNyuukinYoteiDate" _
                                                    , strKbn _
                                                    , strBangou _
                                                    , dtHkUpdDatetime _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter
        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine("SELECT ")
        cmdTextSb.AppendLine("     [jhs_sys].[fnGetNyuukinNasiInfo](2, @KBN, @HOSYOUSYONO, @UPDDATETIME) ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strBangou), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 10, dtHkUpdDatetime)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return String.Empty
        End If

        Return data.ToString

    End Function

End Class
