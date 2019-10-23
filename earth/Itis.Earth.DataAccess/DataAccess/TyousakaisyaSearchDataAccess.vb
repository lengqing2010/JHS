Imports System.text
Imports System.Data.SqlClient

Public Class TyousakaisyaSearchDataAccess
    Inherits AbsDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "調査会社マスタ検索"
    ''' <summary>
    ''' 調査会社マスタの検索を行う
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社ｺｰﾄﾞ</param>
    ''' <param name="strJigyousyoCd">事業所ｺｰﾄﾞ</param>
    ''' <param name="strTysKaiNm">調査会社名</param>
    ''' <param name="strTysKaiKana">調査会社名ｶﾅ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="kameitenCd">加盟店コード(可否区分チェック用)</param>
    ''' <param name="kensakuType">検索タイプ(EnumTyousakaisyakensakuType)</param>
    ''' <returns>TyousakaisyaSearchTableDataTable</returns>
    ''' <remarks></remarks>
    Public Function GetTyousakaisyaKensakuData(ByVal strTysKaiCd As String, _
                                      ByVal strJigyousyoCd As String, _
                                      ByVal strTysKaiNm As String, _
                                      ByVal strTysKaiKana As String, _
                                      ByVal blnDelete As Boolean, _
                                      ByVal kameitenCd As String, _
                                      ByVal kensakuType As EarthEnum.EnumTyousakaisyaKensakuType _
                                      ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousakaisyaKensakuData", _
                                                    strTysKaiCd, _
                                                    strJigyousyoCd, _
                                                    strTysKaiNm, _
                                                    strTysKaiKana, _
                                                    blnDelete, _
                                                    kameitenCd)

        ' パラメータ
        Const strParamTysKaiCd As String = "@TYSKAICD"
        Const strParamJigyousyoCd As String = "@JIGYOUSHOCD"
        Const strParamTysKaiNm As String = "@TYSKAINM"
        Const strParamTysKaiKana As String = "@TYSKAIKANA"
        Const strParamSskSiharaisakiNm As String = "@SSK_SIHARAISAKI_NM"
        Const strParamSskSiharaisakiKana As String = "@SSK_SIHARAISAKI_KANA"
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append(" 	 t.tys_kaisya_cd ")
        commandTextSb.Append(" 	,t.jigyousyo_cd ")
        commandTextSb.Append(" 	,t.torikesi ")
        commandTextSb.Append(" 	,t.tys_kaisya_mei ")
        commandTextSb.Append(" 	,t.tys_kaisya_mei_kana ")
        commandTextSb.Append(" 	,ISNULL(t.seikyuu_saki_shri_saki_mei,t.tys_kaisya_mei) AS seikyuu_saki_shri_saki_mei ")
        commandTextSb.Append(" 	,ISNULL(t.seikyuu_saki_shri_saki_kana,t.tys_kaisya_mei_kana) AS seikyuu_saki_shri_saki_kana ")
        commandTextSb.Append(" 	,t.jyuusyo1 ")
        commandTextSb.Append(" 	,t.jyuusyo2 ")
        commandTextSb.Append(" 	,t.yuubin_no ")
        commandTextSb.Append(" 	,t.tel_no ")
        commandTextSb.Append(" 	,t.fax_no ")
        commandTextSb.Append(" 	,t.pca_siiresaki_cd ")
        commandTextSb.Append(" 	,t.pca_seikyuu_cd ")
        commandTextSb.Append(" 	,t.seikyuu_saki_cd ")
        commandTextSb.Append(" 	,t.seikyuu_saki_brc ")
        commandTextSb.Append(" 	,t.seikyuu_saki_kbn ")
        commandTextSb.Append(" 	,t.seikyuu_sime_date ")
        commandTextSb.Append(" 	,t.ss_kijyun_kkk ")
        commandTextSb.Append(" 	,t.fc_ten_cd ")
        commandTextSb.Append(" 	,t.fctring_kaisi_nengetu ")

        If kameitenCd = "" Then
            commandTextSb.Append(" ,5 As kahi_kbn")
            commandTextSb.Append(" FROM m_tyousakaisya t WITH (READCOMMITTED) ")
        Else
            commandTextSb.Append(" ,ISNULL(k.kahi_kbn,5) As kahi_kbn")
            commandTextSb.Append(" FROM m_tyousakaisya t WITH (READCOMMITTED) ")
            commandTextSb.Append(" LEFT OUTER JOIN m_kameiten_tyousakaisya k WITH (READCOMMITTED) ")
            commandTextSb.Append(" ON t.tys_kaisya_cd = k.tys_kaisya_cd ")
            commandTextSb.Append(" AND t.jigyousyo_cd = k.jigyousyo_cd ")
            '検索タイプが工事会社場合、会社区分=2、それ以外の場合、1
            If kensakuType = EarthEnum.EnumTyousakaisyaKensakuType.KOUJIKAISYA Then
                commandTextSb.Append(" AND k.kaisya_kbn = 2 ")
            Else
                commandTextSb.Append(" AND k.kaisya_kbn = 1 ")
            End If
            commandTextSb.Append(" AND k.kameiten_cd = " & strParamKameitenCd)
        End If

        commandTextSb.Append(" WHERE ")
        If blnDelete = True Then
            commandTextSb.Append(" t.torikesi = 0 ")
        Else
            commandTextSb.Append(" 0 = 0 ")
        End If
        '調査会社コードは「調査会社コード ＋ 事業所コード」で検索を行う
        If strTysKaiCd <> "" Then
            commandTextSb.Append(" AND t.tys_kaisya_cd + t.jigyousyo_cd Like " & strParamTysKaiCd)
        End If
        '会社名検索条件
        If strTysKaiNm <> "" Then
            '検索タイプ別に、検索先のカラムを変える
            If kensakuType = EarthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI Then
                '支払先検索の場合、請求先支払先名で検索
                commandTextSb.Append(" AND ISNULL(t.seikyuu_saki_shri_saki_mei,t.tys_kaisya_mei) Like " & strParamSskSiharaisakiNm)
            Else
                '上記以外の場合、調査会社名カナで検索
                commandTextSb.Append(" AND t.tys_kaisya_mei Like " & strParamTysKaiNm)
            End If
        End If
        '会社名カナ検索条件
        If strTysKaiKana <> "" Then
            '検索タイプ別に、検索先のカラムを変える
            If kensakuType = EarthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI Then
                '支払先検索の場合、請求先支払先名カナで検索
                commandTextSb.Append(" AND ISNULL(t.seikyuu_saki_shri_saki_kana,t.tys_kaisya_mei_kana) Like " & strParamSskSiharaisakiKana)
            Else
                '上記以外の場合、調査会社名カナで検索
                commandTextSb.Append(" AND t.tys_kaisya_mei_kana Like " & strParamTysKaiKana)
            End If
        End If
        '検索タイプが支払先の場合、事業所コード＝支払集計先事業所コードのレコードのみを対象とする
        If kensakuType = EarthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI Then
            commandTextSb.Append(" AND t.jigyousyo_cd = t.shri_jigyousyo_cd ")
        End If
        '加盟店コードが指定されている場合、並び替え順序を調整
        If kameitenCd = "" Then
            commandTextSb.Append(" ORDER BY t.tys_kaisya_mei_kana")
        Else
            commandTextSb.Append(" ORDER BY kahi_kbn, k.nyuuryoku_no, t.tys_kaisya_mei_kana")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamTysKaiCd, SqlDbType.VarChar, 7, strTysKaiCd & Chr(37)), _
             SQLHelper.MakeParam(strParamJigyousyoCd, SqlDbType.VarChar, 2, strJigyousyoCd), _
             SQLHelper.MakeParam(strParamTysKaiNm, SqlDbType.VarChar, 42, strTysKaiNm & Chr(37)), _
             SQLHelper.MakeParam(strParamTysKaiKana, SqlDbType.VarChar, 22, strTysKaiKana & Chr(37)), _
             SQLHelper.MakeParam(strParamSskSiharaisakiNm, SqlDbType.VarChar, 82, strTysKaiNm & Chr(37)), _
             SQLHelper.MakeParam(strParamSskSiharaisakiKana, SqlDbType.VarChar, 42, strTysKaiKana & Chr(37)), _
             SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(commandTextSb.ToString, commandParameters)

    End Function

    ''' <summary>
    ''' 調査会社マスタの検索結果件数を取得
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社ｺｰﾄﾞ</param>
    ''' <param name="strJigyousyoCd">事業所ｺｰﾄﾞ</param>
    ''' <param name="strTysKaiNm">調査会社名</param>
    ''' <param name="strTysKaiKana">調査会社名ｶﾅ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="kameitenCd">加盟店コード(可否区分チェック用)</param>
    ''' <param name="kensakuType">検索タイプ(EnumTyousakaisyakensakuType)</param>
    ''' <returns>検索結果件数</returns>
    ''' <remarks></remarks>
    Public Function GetTyousakaisyaKensakuDataCnt(ByVal strTysKaiCd As String, _
                                                  ByVal strJigyousyoCd As String, _
                                                  ByVal strTysKaiNm As String, _
                                                  ByVal strTysKaiKana As String, _
                                                  ByVal blnDelete As Boolean, _
                                                  ByVal kameitenCd As String, _
                                                  ByVal kensakuType As EarthEnum.EnumTyousakaisyaKensakuType _
                                                  ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousakaisyaKensakuDataCnt", _
                                                    strTysKaiCd, _
                                                    strJigyousyoCd, _
                                                    strTysKaiNm, _
                                                    strTysKaiKana, _
                                                    blnDelete, _
                                                    kameitenCd)
        ' パラメータ
        Const strParamTysKaiCd As String = "@TYSKAICD"
        Const strParamJigyousyoCd As String = "@JIGYOUSHOCD"
        Const strParamTysKaiNm As String = "@TYSKAINM"
        Const strParamTysKaiKana As String = "@TYSKAIKANA"
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT " & _
                                "  count(t.tys_kaisya_cd)")
        If kameitenCd = "" Then
            cmdTextSb.Append(" FROM m_tyousakaisya t WITH (READCOMMITTED) ")
        Else
            cmdTextSb.Append(" FROM m_tyousakaisya t WITH (READCOMMITTED) ")
            cmdTextSb.Append(" LEFT OUTER JOIN m_kameiten_tyousakaisya k WITH (READCOMMITTED) ")
            cmdTextSb.Append(" ON t.tys_kaisya_cd = k.tys_kaisya_cd ")
            cmdTextSb.Append(" AND t.jigyousyo_cd = k.jigyousyo_cd ")
            '検索タイプが工事会社場合、会社区分=2、それ以外の場合、1
            If kensakuType = EarthEnum.EnumTyousakaisyaKensakuType.KOUJIKAISYA Then
                cmdTextSb.Append(" AND k.kaisya_kbn = 2 ")
            Else
                cmdTextSb.Append(" AND k.kaisya_kbn = 1 ")
            End If
            cmdTextSb.Append(" AND k.kameiten_cd = " & strParamKameitenCd)
        End If

        cmdTextSb.Append(" WHERE ")
        If blnDelete = True Then
            cmdTextSb.Append(" t.torikesi = 0 ")
        Else
            cmdTextSb.Append(" 0 = 0 ")
        End If
        '調査会社コードは「調査会社コード ＋ 事業所コード」で検索を行う
        If strTysKaiCd <> "" Then
            cmdTextSb.Append(" AND t.tys_kaisya_cd + t.jigyousyo_cd Like " & strParamTysKaiCd)
        End If
        '会社名検索条件
        If strTysKaiNm <> "" Then
            '検索タイプ別に、検索先のカラムを変える
            If kensakuType = EarthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI Then
                '支払先検索の場合、請求先支払先名で検索
                cmdTextSb.Append(" AND t.seikyuu_saki_shri_saki_mei Like " & strParamTysKaiNm)
            Else
                '上記以外の場合、調査会社名カナで検索
                cmdTextSb.Append(" AND t.tys_kaisya_mei Like " & strParamTysKaiNm)
            End If
        End If
        '会社名カナ検索条件
        If strTysKaiKana <> "" Then
            '検索タイプ別に、検索先のカラムを変える
            If kensakuType = EarthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI Then
                '支払先検索の場合、請求先支払先名カナで検索
                cmdTextSb.Append(" AND t.seikyuu_saki_shri_saki_kana Like " & strParamTysKaiKana)
            Else
                '上記以外の場合、調査会社名カナで検索
                cmdTextSb.Append(" AND t.tys_kaisya_mei_kana Like " & strParamTysKaiKana)
            End If
        End If
        '検索タイプが支払先の場合、事業所コード＝支払集計先事業所コードのレコードのみを対象とする
        If kensakuType = EarthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI Then
            cmdTextSb.Append(" AND t.jigyousyo_cd = t.shri_jigyousyo_cd ")
        End If

        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamTysKaiCd, SqlDbType.VarChar, 7, strTysKaiCd & Chr(37)), _
             SQLHelper.MakeParam(strParamJigyousyoCd, SqlDbType.VarChar, 2, strJigyousyoCd), _
             SQLHelper.MakeParam(strParamTysKaiNm, SqlDbType.VarChar, 42, strTysKaiNm & Chr(37)), _
             SQLHelper.MakeParam(strParamTysKaiKana, SqlDbType.VarChar, 22, strTysKaiKana & Chr(37)), _
             SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        ' 検索結果件数の取得
        Dim data As Object = Nothing

        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' 取得出来ない場合、0を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return 0
        End If

        Return data

    End Function
#End Region

#Region "調査会社マスタ検索（名称取得）"
    ''' <summary>
    ''' 調査会社マスタの検索を行う
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社ｺｰﾄﾞ</param>
    ''' <param name="strJigyousyoCd">事業所ｺｰﾄﾞ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns>TyousakaisyaSearchTableDataTable</returns>
    ''' <remarks></remarks>
    Public Function GetTyousaKaisyaMei(ByVal strTysKaiCd As String, _
                                      ByVal strJigyousyoCd As String, _
                                      ByVal blnDelete As Boolean) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousaKaisyaMei", _
                                    strTysKaiCd, _
                                    strJigyousyoCd, _
                                    blnDelete)

        ' パラメータ
        Const strParamTysKaiCd As String = "@TYSKAICD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ISNULL(t.tys_kaisya_mei, ' ') ")
        commandTextSb.Append(" FROM   m_tyousakaisya t WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  t.tys_kaisya_cd + t.jigyousyo_cd = " & strParamTysKaiCd)

        If blnDelete = True Then
            commandTextSb.Append(" AND t.torikesi = 0 ")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamTysKaiCd, SqlDbType.VarChar, 7, strTysKaiCd & strJigyousyoCd)}

        ' 検索実行
        Dim ret As String = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandTextSb.ToString(), _
                                                    commandParameters)
        Return ret

    End Function
#End Region

#Region "調査会社マスタ検索（SDS保持取得）"
    ''' <summary>
    ''' 調査会社マスタの検索を行う
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社ｺｰﾄﾞ</param>
    ''' <param name="strJigyousyoCd">事業所ｺｰﾄﾞ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns>TyousakaisyaSearchTableDataTable</returns>
    ''' <remarks></remarks>
    Public Function GetTyousaKaisyaSDS(ByVal strTysKaiCd As String, _
                                      ByVal strJigyousyoCd As String, _
                                      ByVal blnDelete As Boolean) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyousaKaisyaSDS", _
                                    strTysKaiCd, _
                                    strJigyousyoCd, _
                                    blnDelete)

        ' パラメータ
        Const strParamTysKaiCd As String = "@TYSKAICD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ISNULL(t.sds_hoji_info,0) ")
        commandTextSb.Append(" FROM   m_tyousakaisya t WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  t.tys_kaisya_cd + t.jigyousyo_cd = " & strParamTysKaiCd)

        If blnDelete = True Then
            commandTextSb.Append(" AND t.torikesi = 0 ")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamTysKaiCd, SqlDbType.VarChar, 7, strTysKaiCd & strJigyousyoCd)}

        ' 検索実行
        Dim ret As Integer = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandTextSb.ToString(), _
                                                    commandParameters)
        Return ret

    End Function
#End Region

#Region "請求締め日取得"
    ''' <summary>
    ''' 調査会社マスタより請求締め日を取得する
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社コード＋事業所コード</param>
    ''' <param name="blnDelete">取消フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDate( _
                                        ByVal strTysKaiCd As String _
                                       , Optional ByVal blnDelete As Boolean = True _
                                       ) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiSeikyuuSimeDate", _
                            strTysKaiCd, _
                            blnDelete)

        '加盟店コードが未入力の場合
        If strTysKaiCd = "" Then
            Return Nothing
        End If

        ' パラメータ
        Const strParamTysKaiCd As String = "@TYSKAICD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    t.seikyuu_sime_date ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_tyousakaisya t WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    t.tys_kaisya_cd + t.jigyousyo_cd = " & strTysKaiCd)
        If blnDelete = True Then
            commandTextSb.Append(" AND t.torikesi = 0 ")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamTysKaiCd, SqlDbType.VarChar, 7, strTysKaiCd)}

        ' データの取得
        Dim data As Object

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return ""
        End If

        Return data

    End Function
#End Region

#Region "加盟店調査会社設定マスタ"
    ''' <summary>
    ''' 加盟店調査会社設定マスタより該当の調査会社コードが存在するかどうかを判定する
    ''' ※事業所コードは条件対象外
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysKaiCd">調査会社コード</param>
    ''' <returns>True or Fasse</returns>
    ''' <remarks></remarks>
    Public Function ExistTyousakaisyaCd( _
                                        ByVal strKameitenCd As String _
                                        , ByVal strTysKaiCd As String _
                                       ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ExistTyousakaisyaCd", _
                            strKameitenCd _
                            , strTysKaiCd _
                            )

        '加盟店コード or 調査会社コードが未入力の場合
        If strKameitenCd = "" Or strTysKaiCd = "" Then
            Return False
        End If

        ' パラメータ
        Dim strParamKameitenCd As String = "@KAMEITENCD"
        Dim strParamTysKaiCd As String = "@TYSKAICD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    COUNT(kameiten_cd) ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten_tyousakaisya kt WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    kt.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    kt.tys_kaisya_cd = " & strParamTysKaiCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    kt.kaisya_kbn = 2 ")
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    kt.kahi_kbn = 1 ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
            SQLHelper.MakeParam(strParamTysKaiCd, SqlDbType.VarChar, 4, strTysKaiCd)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return False
        End If
        '該当データなし
        If data = 0 Then
            Return False
        End If
        '該当データあり
        If data > 0 Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 加盟店コードに紐付く調査会社コードを取得する
    ''' ※取消区分=0の指定調査会社、優先調査会社で可否区分順、入力NO順で
    ''' 1レコード目の調査会社コードを返却する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysKaiCd">調査会社コード</param>
    ''' <returns>True or False</returns>
    ''' <remarks>該当の調査会社コードを返却</remarks>
    Public Function GetSiteiTyousakaisyaCd( _
                                        ByVal strKameitenCd As String _
                                        , ByRef strTysKaiCd As String _
                                       ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiteiTyousakaisyaCd", _
                            strKameitenCd _
                            , strTysKaiCd _
                            )

        '加盟店コードが未入力の場合
        If strKameitenCd = "" Then
            Return False
        End If

        ' パラメータ
        Dim strParamKameitenCd As String = "@KAMEITENCD"
        Dim strParamTysKaiCd As String = "@TYSKAICD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    ( kt.tys_kaisya_cd + kt.jigyousyo_cd ) AS TYSGAISYACD ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten_tyousakaisya kt WITH (READCOMMITTED) ")
        commandTextSb.Append(" INNER JOIN m_tyousakaisya t WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON kt.tys_kaisya_cd = t.tys_kaisya_cd ")
        commandTextSb.Append(" AND kt.jigyousyo_cd = t.jigyousyo_cd ")
        commandTextSb.Append(" AND t.torikesi = 0 ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    kt.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    kt.kaisya_kbn = 1 ")
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    kt.kahi_kbn IN ( 1 , 3 ) ")
        commandTextSb.Append(" ORDER BY kt.kahi_kbn,kt.nyuuryoku_no")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            strTysKaiCd = ""
            Return False
        End If
        '該当データなし
        If data = 0 Then
            strTysKaiCd = ""
            Return False
        End If
        '該当データあり
        If data > 0 Then
            strTysKaiCd = data.ToString
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 加盟店コードに紐付く調査会社コードを取得する
    ''' ※取消区分=0の指定調査会社、優先調査会社で可否区分順、入力NO順で
    ''' 1レコード目の調査会社コードを返却する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysKaiCd">調査会社コード</param>
    ''' <param name="kensakuType">検索タイプ(EnumKameitenTyousakaisyaKensakuType)</param>
    ''' <returns>True or False</returns>
    ''' <remarks>該当の調査会社コードを返却</remarks>
    Public Function GetSiteiYuusenTyousakaisyaCd( _
                                        ByVal strKameitenCd As String _
                                        , ByRef strTysKaiCd As String _
                                        , ByVal kensakuType As EarthEnum.EnumKameitenTyousakaisyaKensakuType _
                                       ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiteiYuusenTyousakaisyaCd", _
                            strKameitenCd _
                            , strTysKaiCd _
                            , kensakuType _
                            )

        '加盟店コードが未入力の場合
        If strKameitenCd = "" Then
            Return False
        End If

        ' パラメータ
        Dim strParamKameitenCd As String = "@KAMEITENCD"
        Dim strParamTysKaiCd As String = "@TYSKAICD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    ( kt.tys_kaisya_cd + kt.jigyousyo_cd ) AS TYSGAISYACD ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten_tyousakaisya kt WITH (READCOMMITTED) ")
        commandTextSb.Append(" INNER JOIN m_tyousakaisya t WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON kt.tys_kaisya_cd = t.tys_kaisya_cd ")
        commandTextSb.Append(" AND kt.jigyousyo_cd = t.jigyousyo_cd ")
        commandTextSb.Append(" AND t.torikesi = 0 ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    kt.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    kt.kaisya_kbn = 1 ")
        '検索タイプが指定調査会社の場合、可否区分=1、優先調査会社の場合、可否区分=3
        If kensakuType = EarthEnum.EnumKameitenTyousakaisyaKensakuType.SITEITYOUSAKAISYA Then
            commandTextSb.Append(" AND kt.kahi_kbn = 1 ")
        Else
            commandTextSb.Append(" AND kt.kahi_kbn = 3 ")
        End If
        commandTextSb.Append(" ORDER BY kt.kahi_kbn,kt.nyuuryoku_no")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            strTysKaiCd = ""
            Return False
        End If
        '該当データなし
        If data = 0 Then
            strTysKaiCd = ""
            Return False
        End If
        '該当データあり
        If data > 0 Then
            strTysKaiCd = data.ToString
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 加盟店コードに紐付く調査会社コードを取得する
    ''' ※会社区分=1/可否区分=1の指定調査会社で調査会社コードが7で始まる調査会社
    ''' 調査会社コード/事業所コード/入力NO順で複数レコードの調査会社コードを返却する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysKaiCd">調査会社コード</param>
    ''' <returns>True or False</returns>
    ''' <remarks>複数の調査会社コード,調査会社名を返却</remarks>
    Public Function GetKameitenTysTehaiCenter( _
                                        ByVal strKameitenCd As String _
                                        , ByRef strTysKaiCd As String _
                                       ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenTysTehaiCenter", _
                            strKameitenCd _
                            , strTysKaiCd _
                            )

        '加盟店コードあるいは調査会社コードが未入力の場合
        If strKameitenCd = String.Empty OrElse strTysKaiCd = String.Empty Then
            Return Nothing
        End If

        ' パラメータ
        Dim strParamKameitenCd As String = "@KAMEITENCD"
        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("      '[' + kt.tys_kaisya_cd + kt.jigyousyo_cd + ']' + t.tys_kaisya_mei AS TYSGAISYACD ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("      m_kameiten_tyousakaisya kt ")
        commandTextSb.Append("      WITH (READCOMMITTED) ")
        commandTextSb.Append("           INNER JOIN m_tyousakaisya t ")
        commandTextSb.Append("                WITH (READCOMMITTED) ")
        commandTextSb.Append("             ON kt.tys_kaisya_cd = t.tys_kaisya_cd ")
        commandTextSb.Append("            AND kt.jigyousyo_cd = t.jigyousyo_cd ")
        commandTextSb.Append("            AND t.torikesi = 0 ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("      kt.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append("  AND kt.tys_kaisya_cd like '7%' ")
        commandTextSb.Append("  AND kt.kaisya_kbn = 1 ")
        commandTextSb.Append("  AND kt.kahi_kbn = 1 ")
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("      kt.tys_kaisya_cd ")
        commandTextSb.Append("    , kt.jigyousyo_cd ")
        commandTextSb.Append("    , kt.nyuuryoku_no ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(commandTextSb.ToString, commandParameters)
    End Function

    ''' <summary>
    ''' 調査会社コードを引数に名称を取得します
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBy(ByVal code As Integer, _
                              ByRef name As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                                    code, _
                                                    name)

        ' パラメータ
        Const paramCode As String = "@TYSKAISYACODE"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT RTRIM(tys_kaisya_cd) + jigyousyo_cd code ")
        commandTextSb.Append("     , ISNULL(tys_kaisya_mei, '') meisyou")
        commandTextSb.Append("  FROM m_tyousakaisya WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE torikesi = '0' ")
        commandTextSb.Append("    AND RTRIM(tys_kaisya_cd) + jigyousyo_cd = " + paramCode)
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramCode, SqlDbType.Int, 1, code)}

        ' データの取得
        Dim TyousaKaisyaDataSet As New TyousakaisyaSearchDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TyousaKaisyaDataSet, TyousaKaisyaDataSet.TyousaKaisyaTable.TableName, commandParameters)

        Dim TyousaKaisyaTable As TyousakaisyaSearchDataSet.TyousaKaisyaTableDataTable = TyousaKaisyaDataSet.TyousaKaisyaTable

        If TyousaKaisyaTable.Count = 0 Then
            Debug.WriteLine("取得出来ませんでした")
            Return False
        Else
            Dim row As TyousakaisyaSearchDataSet.TyousaKaisyaTableRow = TyousaKaisyaTable(0)
            name = row.meisyou
        End If

        Return True

    End Function

    ''' <summary>
    ''' コンボボックス設定用の有効な調査概要レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", _
                                                    dt, _
                                                    withSpaceRow, _
                                                    withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As TyousakaisyaSearchDataSet.TyousaKaisyaTableRow

        commandTextSb.Append("SELECT RTRIM(tys_kaisya_cd) + jigyousyo_cd code ")
        commandTextSb.Append("     , ISNULL(tys_kaisya_mei, '') meisyou")
        commandTextSb.Append("  FROM m_tyousakaisya WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE torikesi = '0' ")
        commandTextSb.Append("  ORDER BY tys_kaisya_mei_kana ")

        ' データの取得
        Dim TyousaKaisyaDataSet As New TyousakaisyaSearchDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TyousaKaisyaDataSet, TyousaKaisyaDataSet.TyousaKaisyaTable.TableName)

        Dim TyousaKaisyaDataTable As TyousakaisyaSearchDataSet.TyousaKaisyaTableDataTable = _
                    TyousaKaisyaDataSet.TyousaKaisyaTable

        If TyousaKaisyaDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "9", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In TyousaKaisyaDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.code.ToString + ":" + row.meisyou, row.code, dt))
                Else
                    dt.Rows.Add(CreateRow(row.meisyou, row.code, dt))
                End If
            Next

        End If


    End Sub

#End Region

#Region "調査会社情報取得"
    ''' <summary>
    ''' 調査会社情報を取得するSQL文を生成
    ''' </summary>
    ''' <param name="strkaisyacd">調査会社コード</param>
    ''' <param name="strjigyousyocd">調査会社事業所コード</param>
    ''' <returns>調査会社情報を取得するSQL文</returns>
    ''' <remarks>※必要に応じて項目を追加してください。(項目削除は禁止)</remarks>
    Private Function makeTysKaisyaSql(ByVal strKaisyaCd As String, Optional ByVal strJigyousyoCd As String = "") As String

        Dim cmdTextSb As New StringBuilder()
        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      tys_kaisya_cd")
        cmdTextSb.Append("    , jigyousyo_cd")
        cmdTextSb.Append("    , torikesi")
        cmdTextSb.Append("    , tys_kaisya_mei")
        cmdTextSb.Append("    , LTRIM(RTRIM(seikyuu_saki_cd)) seikyuu_saki_cd")
        cmdTextSb.Append("    , LTRIM(RTRIM(seikyuu_saki_brc)) seikyuu_saki_brc")
        cmdTextSb.Append("    , LTRIM(RTRIM(seikyuu_saki_kbn)) seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MT.seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MT.seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MT.seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      seikyuu_saki_mei")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      m_tyousakaisya MT")
        cmdTextSb.Append(" WHERE")
        If strJigyousyoCd = String.Empty Then
            cmdTextSb.Append("      tys_kaisya_cd + jigyousyo_cd = @KAISYACD")
        Else
            cmdTextSb.Append("      tys_kaisya_cd = @KAISYACD")
            cmdTextSb.Append("      AND ")
            cmdTextSb.Append("      jigyousyo_cd = @JIGYOUSYOCD")
        End If

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 調査会社情報取得[PK結合版]
    ''' </summary>
    ''' <param name="strKaisyaCd">調査会社コード(結合済み)</param>
    ''' <returns>調査会社情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function searchTysKaisyaInfo(ByVal strKaisyaCd As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchTysKaisyaInfo", _
                                                    strKaisyaCd)

        Dim strSql As String = makeTysKaisyaSql(strKaisyaCd)

        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KAISYACD", SqlDbType.Char, 7, strKaisyaCd)}

        ' データの取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(strSql, cmdParams)

    End Function

    ''' <summary>
    ''' 調査会社情報取得[PK版]
    ''' </summary>
    ''' <param name="strKaisyaCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">調査会社事業所コード</param>
    ''' <returns>調査会社情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function searchTysKaisyaInfo(ByVal strKaisyaCd As String, ByVal strJigyousyoCd As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchTysKaisyaInfo", _
                                                    strKaisyaCd)

        Dim strSql As String = makeTysKaisyaSql(strKaisyaCd, strJigyousyoCd)

        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KAISYACD", SqlDbType.Char, 5, strKaisyaCd), _
            SQLHelper.MakeParam("@JIGYOUSYOCD", SqlDbType.Char, 2, strJigyousyoCd)}

        ' データの取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(strSql, cmdParams)

    End Function
#End Region

    Public Sub New()

    End Sub

#Region "新会計支払先マスタ検索"
    ''' <summary>
    ''' 新会計支払先マスタの検索を行う
    ''' </summary>
    ''' <param name="strSkkJigyousyoCd">新会計事業所コード</param>
    ''' <param name="strSkkShriSakiCd">新会計支払先コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSkkSiharaisakiKensakuData(ByVal strSkkJigyousyoCd As String, ByVal strSkkShriSakiCd As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSkkSiharaisakiKensakuData", _
                                                    strSkkJigyousyoCd, _
                                                    strSkkShriSakiCd)
        'パラメータ
        Const DbParamSkkJigyousyoCd As String = "@SKKJIGYOUSYOCD"
        Const DbParamSkkShriSakiCd As String = "@SKKSHRISAKICD"

        Dim dt As New DataTable
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      MSS.skk_jigyou_cd ")
        cmdTextSb.Append("    , MSS.skk_shri_saki_cd ")
        cmdTextSb.Append("    , shri_saki_mei_kanji ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      m_sinkaikei_siharai_saki MSS ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")

        '新会計事業所コード
        If Not String.IsNullOrEmpty(strSkkJigyousyoCd) Then
            cmdTextSb.Append("  AND MSS.skk_jigyou_cd = " & DbParamSkkJigyousyoCd)
        End If

        '新会計支払先コード
        If Not String.IsNullOrEmpty(strSkkShriSakiCd) Then
            cmdTextSb.Append("  AND MSS.skk_shri_saki_cd = " & DbParamSkkShriSakiCd)
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(DbParamSkkJigyousyoCd, SqlDbType.VarChar, 10, strSkkJigyousyoCd), _
             SQLHelper.MakeParam(DbParamSkkShriSakiCd, SqlDbType.VarChar, 10, strSkkShriSakiCd)}

        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, commandParameters)
    End Function
#End Region

End Class
