Imports System.text
Imports System.Data.SqlClient

Public Class KisoSiyouDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "判定取得"
    ''' <summary>
    ''' 基礎仕様マスタより判定名称を取得する
    ''' </summary>
    ''' <param name="kisoSiyouNo">基礎仕様NO</param>
    ''' <returns>判定名称(基礎仕様)</returns>
    ''' <remarks></remarks>
    Public Function GetHanteiMei(ByVal kisoSiyouNo As Integer) As KisoSiyouDataSet.KisoSiyouTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanteiMei", _
                            kisoSiyouNo)

        ' パラメータ
        Const paramKsSiyoNo As String = "@KSSIYONO"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("     ks_siyou_no, ")
        commandTextSb.Append("     ks_siyou, ")
        commandTextSb.Append("     koj_hantei_flg ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_ks_siyou WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("     ks_siyou_no = " & paramKsSiyoNo)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKsSiyoNo, SqlDbType.Int, 4, kisoSiyouNo)}

        ' データの取得
        Dim dsKisoSiyou As New KisoSiyouDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              commandTextSb.ToString(), _
                              dsKisoSiyou, _
                              dsKisoSiyou.KisoSiyouTable.TableName, _
                              commandParameters)

        Dim dtKisoSiyou As KisoSiyouDataSet.KisoSiyouTableDataTable = _
            dsKisoSiyou.KisoSiyouTable


        ' 取得できた場合、結果を返却する
        If dtKisoSiyou.Count <> 0 Then
            Return dtKisoSiyou
        End If

        ' 取得出来ない場合、空白を返却
        Return Nothing

    End Function

    ''' <summary>
    ''' 基礎仕様マスタより判定接続詞名称を取得する
    ''' </summary>
    ''' <param name="kisoSiyouSetuzokusiNo">基礎仕様接続詞NO</param>
    ''' <returns>判定接続詞名称(基礎仕様)</returns>
    ''' <remarks></remarks>
    Public Function GetHanteiSetuzokusiMei(ByVal kisoSiyouSetuzokusiNo As Integer) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanteiSetuzokusiMei", _
                            kisoSiyouSetuzokusiNo)

        ' パラメータ
        Const paramKsSiyoSetuzokusiNo As String = "@KSSIYOSETUZOKUSINO"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("     ks_siyou_setuzokusi_no, ")
        commandTextSb.Append("     ks_siyou_setuzokusi ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_ks_siyou_setuzokusi WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("     ks_siyou_setuzokusi_no = " & paramKsSiyoSetuzokusiNo)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKsSiyoSetuzokusiNo, SqlDbType.Int, 4, kisoSiyouSetuzokusiNo)}

        ' データの取得
        Dim dsKisoSiyouSetuzokusi As New KisoSiyouDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              commandTextSb.ToString(), _
                              dsKisoSiyouSetuzokusi, _
                              dsKisoSiyouSetuzokusi.KisoSiyouSetuzokusiTable.TableName, _
                              commandParameters)

        Dim dtKisoSiyouSetuzokusi As KisoSiyouDataSet.KisoSiyouSetuzokusiTableDataTable = _
            dsKisoSiyouSetuzokusi.KisoSiyouSetuzokusiTable


        ' 取得できた場合、結果を返却する
        If dtKisoSiyouSetuzokusi.Count <> 0 Then
            Dim row As KisoSiyouDataSet.KisoSiyouSetuzokusiTableRow
            row = dtKisoSiyouSetuzokusi.Rows(0)
            Return row.ks_siyou_setuzokusi
        End If

        ' 取得出来ない場合、空白を返却
        Return ""

    End Function

#End Region

#Region "基礎仕様マスタ検索"
    ''' <summary>
    ''' 基礎仕様マスタの検索を行う
    ''' </summary>
    ''' <param name="strKisoSiyouNo">基礎仕様NO</param>
    ''' <param name="strKisoSiyouNm">基礎仕様名</param>
    ''' <param name="blnMatchType">基礎仕様NO検索タイプ（True:完全一致 or False:前方一致）</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouKensakuData(ByVal strKisoSiyouNo As String, _
                                           ByVal strKisoSiyouNm As String, _
                                           ByVal strKameitenCd As String, _
                                           ByVal blnMatchType As Boolean, _
                                           ByVal blnDelete As Boolean) As KisoSiyouDataSet.KisoSiyouSearchTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouKensakuData", _
                                            strKisoSiyouNo, _
                                            strKisoSiyouNm, _
                                            strKameitenCd, _
                                            blnDelete)

        ' パラメータ
        Const intParamKisoSiyouNo As String = "@KISOSIYOUNO"
        Const strParamKisoSiyouNm As String = "@KISOSIYOUNM"
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        Dim kbnCount As Integer = 1

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT")
        commandTextSb.Append("   ks.ks_siyou_no AS ks_siyou_no")
        commandTextSb.Append("   , ks_siyou")
        commandTextSb.Append("   , koj_hantei_flg")
        commandTextSb.Append("   , kameiten_cd")
        commandTextSb.Append("   , isnull(kahi_kbn, 5) AS kahi_kbn")
        commandTextSb.Append("   , nyuuryoku_no ")
        commandTextSb.Append(" FROM")
        commandTextSb.Append("   m_ks_siyou ks ")
        commandTextSb.Append("   LEFT OUTER JOIN ( ")
        commandTextSb.Append("     SELECT")
        commandTextSb.Append("       kameiten_cd")
        commandTextSb.Append("       , kahi_kbn")
        commandTextSb.Append("       , nyuuryoku_no")
        commandTextSb.Append("       , ks_siyou_no ")
        commandTextSb.Append("     FROM")
        commandTextSb.Append("       m_kameiten_ks_siyou_settei ")
        If strParamKameitenCd <> "" Then
            commandTextSb.Append("     WHERE")
            commandTextSb.Append("       kameiten_cd = " & strParamKameitenCd)
        End If
        commandTextSb.Append("   ) kp ")
        commandTextSb.Append("     ON kp.ks_siyou_no = ks.ks_siyou_no ")
        commandTextSb.Append(" WHERE")
        commandTextSb.Append("   0 = 0 ")
        If strKisoSiyouNo <> "" Then
            commandTextSb.Append("   AND ks.ks_siyou_no LIKE " & intParamKisoSiyouNo)
        End If
        If strKisoSiyouNm <> "" Then
            commandTextSb.Append("   AND ks.ks_siyou LIKE " & strParamKisoSiyouNm)
        End If
        commandTextSb.Append(" ORDER By")
        commandTextSb.Append("   (CASE ")
        commandTextSb.Append("     WHEN isnull(kahi_kbn, 1) = 9 ")
        commandTextSb.Append("     THEN 1 ")
        commandTextSb.Append("     ELSE 0 ")
        commandTextSb.Append("     END) ")
        commandTextSb.Append("   , koj_hantei_flg ")
        commandTextSb.Append("   , isnull(kahi_kbn, 5) ")
        commandTextSb.Append("   , nyuuryoku_no ")
        commandTextSb.Append("   , ks.ks_siyou_no ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            { _
             SQLHelper.MakeParam(intParamKisoSiyouNo, SqlDbType.VarChar, 6, strKisoSiyouNo & IIf(blnMatchType, String.Empty, Chr(37))), _
             SQLHelper.MakeParam(strParamKisoSiyouNm, SqlDbType.VarChar, 82, strKisoSiyouNm & Chr(37)), _
             SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd) _
             }

        ' データの取得
        Dim kisoSiyouDataSet As New KisoSiyouDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kisoSiyouDataSet, kisoSiyouDataSet.KisoSiyouSearchTable.TableName, commandParameters)

        Dim kameitenTable As KisoSiyouDataSet.KisoSiyouSearchTableDataTable = _
                    kisoSiyouDataSet.KisoSiyouSearchTable

        Return kameitenTable

    End Function
#End Region

#Region "判定工事種別設定マスタ取得"

    ''' <summary>
    ''' 判定工事種別設定マスタより、該当データが存在するか判断する。
    ''' </summary>
    ''' <param name="strHanteiSetuzokuMoji">判定接続文字コード</param>
    ''' <param name="strKjSyubetu">改良工事種別コード</param>
    ''' <param name="strKisoSiyouNo1">基礎仕様NO1[判定1]</param>
    ''' <param name="strKisoSiyouNo2">基礎仕様NO2[判定2]</param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks></remarks>
    Public Function GetHanteiKoujiSyubetuSettei( _
                                        ByVal strHanteiSetuzokuMoji As String _
                                        , ByVal strKjSyubetu As String _
                                        , ByVal strKisoSiyouNo1 As String _
                                        , Optional ByVal strKisoSiyouNo2 As String = "" _
                                        ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanteiKoujiSyubetuSettei", _
                            strHanteiSetuzokuMoji _
                            , strKjSyubetu _
                            , strKisoSiyouNo1 _
                            , strKisoSiyouNo2 _
                            )

        Dim intKjSyubetu As Integer = 0
        Dim intKisoSiyouNo1 As Integer = 0
        Dim intKisoSiyouNo2 As Integer = 0

        intKjSyubetu = Integer.Parse(strKjSyubetu)
        If strKisoSiyouNo1 <> "" Then
            intKisoSiyouNo1 = Integer.Parse(strKisoSiyouNo1)
        End If
        If strKisoSiyouNo2 <> "" Then
            intKisoSiyouNo2 = Integer.Parse(strKisoSiyouNo2)
        End If

        ' パラメータ
        Const paramKsKjSyubetu As String = "@KSKJSYUBETU"
        Const paramKsSiyoNo1 As String = "@KSSIYONO1"
        Const paramKsSiyoNo2 As String = "@KSSIYONO2"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("     ks_siyou_no ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_hantei_koji_syubetu WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("     kairy_koj_syubetu_no = " & paramKsKjSyubetu)
        If strHanteiSetuzokuMoji = "" Then
            commandTextSb.Append(" AND  ")
            commandTextSb.Append("     ks_siyou_no = " & paramKsSiyoNo1)
        ElseIf strHanteiSetuzokuMoji <> "2" Then
            commandTextSb.Append(" AND ( ")
            commandTextSb.Append("     ks_siyou_no = " & paramKsSiyoNo1)
            commandTextSb.Append(" OR  ")
            commandTextSb.Append("     ks_siyou_no = " & paramKsSiyoNo2)
            commandTextSb.Append("     ) ")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKsKjSyubetu, SqlDbType.Int, 4, intKjSyubetu), _
                SQLHelper.MakeParam(paramKsSiyoNo1, SqlDbType.Int, 4, intKisoSiyouNo1)}

        If strHanteiSetuzokuMoji <> "2" Then
            ' パラメータへ設定
            commandParameters = New SqlParameter() _
                {SQLHelper.MakeParam(paramKsKjSyubetu, SqlDbType.Int, 4, intKjSyubetu), _
                    SQLHelper.MakeParam(paramKsSiyoNo1, SqlDbType.Int, 4, intKisoSiyouNo1), _
                    SQLHelper.MakeParam(paramKsSiyoNo2, SqlDbType.Int, 4, intKisoSiyouNo2)}
        End If

        ' データの取得
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandTextSb.ToString, _
                                                    commandParameters)

        If ret Is Nothing OrElse IsDBNull(ret) Then
            Return False
        End If

        Return True

    End Function

#End Region
End Class
