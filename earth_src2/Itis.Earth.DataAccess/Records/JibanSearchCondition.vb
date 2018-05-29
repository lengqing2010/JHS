Imports System.text
Imports System.Data.SqlClient
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data

Public Class JibanSearchCondition

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' 項目の初期化
        strKbn1 = String.Empty
        strKbn2 = String.Empty
        strKbn3 = String.Empty
        intHosyousyoNoHani = Integer.MinValue
        strHosyousyoNoFrom = String.Empty
        strHosyousyoNoTo = String.Empty
        strKameitenCd = String.Empty
        strTenmeiKana1 = String.Empty
        strKeiretuCd = String.Empty
        strTysKaisyaCd = String.Empty
        '        strTysKaisyaJigyousyoCd = String.Empty
        strSesyuMei = String.Empty
        strBukkenJyuusyo1 = String.Empty
    End Sub

    ''' <summary>
    ''' 本クラスに設定されているデータで検索用のSQL及びパラメータを生成します
    ''' </summary>
    ''' <param name="sql_params">SQLパラメータ配列（このインスタンスにパラメータが設定されます）</param> 
    ''' <returns>検索用のSQL文</returns>
    ''' <remarks></remarks>
    Public Function getSqlCondition(ByRef sql_params As SqlParameter()) As String

        Dim param_array As New ArrayList

        Dim sql As String = ""
        Dim sql_condition As New StringBuilder
        Dim isWhere As Boolean = False

        ' 検索の基本となるSQL文
        Dim base_sql As String = " SELECT  " & _
                            "      j.kbn " & _
                            "     ,j.hosyousyo_no " & _
                            "     ,h.haki_syubetu As data_haki_syubetu " & _
                            "     ,j.sesyu_mei " & _
                            "     ,ISNULL(j.bukken_jyuusyo1,'') +  " & _
                            "      ISNULL(j.bukken_jyuusyo2,'') +  " & _
                            "      ISNULL(j.bukken_jyuusyo3,'') As bukken_jyuusyo " & _
                            "     ,j.kameiten_cd " & _
                            "     ,k.kameiten_mei1 " & _
                            "     ,SUM(t.koumuten_seikyuu_gaku) As koumuten_seikyuu_gaku " & _
                            "     ,SUM(t.uri_gaku) As uri_gaku " & _
                            "     ,SUM(t.siire_gaku) As siire_gaku " & _
                            "     ,j.bikou " & _
                            "     ,ISNULL(j.tys_jissi_date,r.t_jissi_date) As tys_jissi_date  " & _
                            " FROM  " & _
                            "     t_jiban j  " & _
                            "     LEFT OUTER JOIN m_kameiten k ON j.kameiten_cd = k.kameiten_cd " & _
                            "     LEFT OUTER JOIN t_teibetu_seikyuu t ON  t.kbn = j.kbn  " & _
                            "                                         AND t.hosyousyo_no = j.hosyousyo_no  " & _
                            "                                         AND t.bunrui_cd IN ('100','110','115','120')   " & _
                            "     LEFT OUTER JOIN m_data_haki h ON h.data_haki_no = j.data_haki_syubetu  " & _
                            "     LEFT OUTER JOIN ReportIF r ON  r.kokyaku_no = j.kbn + j.hosyousyo_no " & _
                            "                                   AND r.kokyaku_brc = (SELECT " & _
                            "                                                            MAX(r2.kokyaku_brc) As tyousa_brc " & _
                            "                                                        FROM " & _
                            "                                                            ReportIF r2 " & _
                            "                                                        WHERE " & _
                            "                                                            r2.kokyaku_no = r.kokyaku_no " & _
                            "                                                        GROUP BY " & _
                            "                                                            r2.kokyaku_no)  " & _
                            " {0} " & _
                            " GROUP BY  " & _
                            "     j.kbn " & _
                            "     ,j.hosyousyo_no " & _
                            "     ,h.haki_syubetu " & _
                            "     ,j.sesyu_mei " & _
                            "     ,j.bukken_jyuusyo1 " & _
                            "     ,j.bukken_jyuusyo2 " & _
                            "     ,j.bukken_jyuusyo3 " & _
                            "     ,j.kameiten_cd " & _
                            "     ,k.kameiten_mei1 " & _
                            "     ,j.bikou " & _
                            "     ,j.tys_jissi_date " & _
                            "     ,j.tys_kaisya_cd " & _
                            "     ,k.keiretu_cd " & _
                            "     ,r.t_jissi_date" & _
                            " ORDER BY  " & _
                            "      j.kbn " & _
                            "     ,j.hosyousyo_no " & _
                            "     ,h.haki_syubetu " & _
                            "     ,k.kameiten_mei1 " & _
                            "     ,j.sesyu_mei "

        '***********************************************************************
        ' 区分条件の設定
        '***********************************************************************
        ' 一つでも設定されていた場合、条件を作成
        If Not strKbn1 Is String.Empty Or _
           Not strKbn2 Is String.Empty Or _
           Not strKbn3 Is String.Empty Then

            ' 区分項目設定確認用
            Dim kbn_check As Boolean = False

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            sql_condition.Append(" j.kbn IN ( ")

            ' 区分_1の判定
            If Not strKbn1 Is String.Empty Then
                param_array.Add(getParamRecord("@KBN1", SqlDbType.Char, 1, strKbn1))
                sql_condition.Append("@KBN1")
                kbn_check = True
            End If

            ' 区分_2の判定
            If Not strKbn2 Is String.Empty Then
                param_array.Add(getParamRecord("@KBN2", SqlDbType.Char, 1, strKbn2))
                If kbn_check = True Then
                    sql_condition.Append(",")
                End If
                sql_condition.Append("@KBN2")
                kbn_check = True
            End If

            ' 区分_3の判定
            If Not strKbn3 Is String.Empty Then
                If kbn_check = True Then
                    sql_condition.Append(",")
                End If
                param_array.Add(getParamRecord("@KBN3", SqlDbType.Char, 1, strKbn3))
                sql_condition.Append("@KBN3")
            End If

            sql_condition.Append(" ) ")

        End If

        Dim irai_date_flg As Boolean = False

        '***********************************************************************
        ' 保証書対象範囲条件（過去２年条件）
        '***********************************************************************
        ' 保証書対象範囲の判定
        If intHosyousyoNoHani = 1 Then

            ' 現在日時の２年前をKeyとする
            Dim key As String = Date.Now.AddYears(-2).Year.ToString("0000") & "010000"

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@HOSYOUSYONOHANI", SqlDbType.VarChar, 10, key))
            sql_condition.Append(" j.hosyousyo_no >= @HOSYOUSYONOHANI ")
            sql_condition.Append(" AND LEN(j.hosyousyo_no) = 10 ")
            'sql_condition.Append(" AND j.irai_date IS NOT NULL ")
            irai_date_flg = True
        End If

        '***********************************************************************
        ' 保証書範囲条件の設定
        '***********************************************************************
        ' 一つでも設定されている場合、条件を作成
        If Not strHosyousyoNoFrom Is String.Empty Or _
           Not strHosyousyoNoTo Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            If Not strHosyousyoNoFrom Is String.Empty And _
               Not strHosyousyoNoTo Is String.Empty Then
                ' 両方指定有りはBETWEEN
                sql_condition.Append(" j.hosyousyo_no BETWEEN @HOSYOUSYONOFROM AND @HOSYOUSYONOTO")
                param_array.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, strHosyousyoNoFrom))
                param_array.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, strHosyousyoNoTo))
            Else
                If Not strHosyousyoNoFrom Is String.Empty Then
                    ' 保証書Fromのみ
                    sql_condition.Append(" j.hosyousyo_no >= @HOSYOUSYONOFROM ")
                    param_array.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, strHosyousyoNoFrom))
                Else
                    ' 保証書Toのみ
                    sql_condition.Append(" j.hosyousyo_no <= @HOSYOUSYONOTO ")
                    param_array.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, strHosyousyoNoTo))
                End If
            End If

            If irai_date_flg = False Then
                sql_condition.Append(" AND LEN(j.hosyousyo_no) = 10 ")
                'sql_condition.Append(" AND j.irai_date IS NOT NULL ")
            End If
        End If

        '***********************************************************************
        ' 加盟店コードの設定
        '***********************************************************************
        ' 加盟店コードの判定
        If Not strKameitenCd Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@KAMEITENCD", SqlDbType.VarChar, 5, strKameitenCd))
            sql_condition.Append(" j.kameiten_cd = @KAMEITENCD ")
        End If

        '***********************************************************************
        ' 加盟店カナ１の設定
        '***********************************************************************
        ' 加盟店カナ１の判定
        If Not strTenmeiKana1 Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@TENMEIKANA1", SqlDbType.VarChar, 20, strTenmeiKana1 & "%"))
            sql_condition.Append(" (k.tenmei_kana1 like @TENMEIKANA1 OR k.tenmei_kana2 like @TENMEIKANA1)")
        End If

        '***********************************************************************
        ' 系列ｺｰﾄﾞの設定
        '***********************************************************************
        ' 系列ｺｰﾄﾞの判定
        If Not strKeiretuCd Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@KEIRETU", SqlDbType.VarChar, 5, strKeiretuCd))
            sql_condition.Append(" k.keiretu_cd = @KEIRETU ")
        End If

        '***********************************************************************
        ' 調査会社ｺｰﾄﾞの設定
        '***********************************************************************
        ' 調査会社ｺｰﾄﾞの判定
        If Not strTysKaisyaCd Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@TYSKAISYACD", SqlDbType.VarChar, 7, strTysKaisyaCd))
            sql_condition.Append(" RTRIM(j.tys_kaisya_cd) + RTRIM(j.tys_kaisya_jigyousyo_cd) = @TYSKAISYACD ")
            '            sql_condition.Append(" j.tys_kaisya_cd = @TYSKAISYACD ")
        End If

        ' 調査会社コードに含まれる
        '***********************************************************************
        ' 調査会社事業所ｺｰﾄﾞの設定
        '***********************************************************************
        ' 調査会社事業所ｺｰﾄﾞの判定
        'If Not strTysKaisyaJigyousyoCd Is String.Empty Then

        '    sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
        '    isWhere = True

        '    param_array.Add(getParamRecord("@TYSKAISYAJIGYOUSYOCD", SqlDbType.VarChar, 2, strTysKaisyaJigyousyoCd))
        '    sql_condition.Append(" j.tys_kaisya_jigyousyo_cd = @TYSKAISYAJIGYOUSYOCD ")
        'End If

        '***********************************************************************
        ' 施主名の設定
        '***********************************************************************
        ' 施主名の判定
        If Not strSesyuMei Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@SESYUMEI", SqlDbType.VarChar, 50, strSesyuMei & "%"))
            sql_condition.Append(" j.sesyu_mei like @SESYUMEI ")
        End If

        '***********************************************************************
        ' 物件住所1の設定
        '***********************************************************************
        ' 物件住所1の判定
        If Not strBukkenJyuusyo1 Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@BUKKENJYUUSYO1", SqlDbType.VarChar, 32, strBukkenJyuusyo1 & "%"))
            sql_condition.Append(" j.bukken_jyuusyo1 like @BUKKENJYUUSYO1 ")
        End If

        ' パラメータの作成
        Dim i As Integer
        Dim cmdParams(param_array.Count - 1) As SqlParameter
        For i = 0 To param_array.Count - 1
            Dim rec As ParamRecord = param_array(i)
            ' 必要な情報をセット
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' 返却用パラメータのセット
        sql_params = cmdParams

        ' 最終的なSQL文の生成
        sql = String.Format(base_sql, sql_condition.ToString())

        Return sql

    End Function


#Region "区分_1"
    ''' <summary>
    ''' 区分_1 kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn1 As String
    ''' <summary>
    ''' 区分_1 
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分_1</returns>
    ''' <remarks></remarks>
    Public Property Kbn1() As String
        Get
            Return strKbn1
        End Get
        Set(ByVal value As String)
            strKbn1 = value
        End Set
    End Property
#End Region
#Region "区分_2"
    ''' <summary>
    ''' 区分_2 kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn2 As String
    ''' <summary>
    ''' 区分_2
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分_2</returns>
    ''' <remarks></remarks>
    Public Property Kbn2() As String
        Get
            Return strKbn2
        End Get
        Set(ByVal value As String)
            strKbn2 = value
        End Set
    End Property
#End Region
#Region "区分_3"
    ''' <summary>
    ''' 区分_3  kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn3 As String
    ''' <summary>
    ''' 区分_3
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分_3</returns>
    ''' <remarks></remarks>
    Public Property Kbn3() As String
        Get
            Return strKbn3
        End Get
        Set(ByVal value As String)
            strKbn3 = value
        End Set
    End Property
#End Region
#Region "保証書NO 対象範囲"
    ''' <summary>
    ''' 保証書NO 対象範囲 
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoNoHani As Integer
    ''' <summary>
    ''' 保証書NO 対象範囲
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO 対象範囲</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoHani() As Integer
        Get
            Return intHosyousyoNoHani
        End Get
        Set(ByVal value As Integer)
            intHosyousyoNoHani = value
        End Set
    End Property
#End Region
#Region "保証書NO From"
    ''' <summary>
    ''' 保証書NO From hosyousyo_no
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoFrom As String
    ''' <summary>
    ''' 保証書NO From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoFrom() As String
        Get
            Return strHosyousyoNoFrom
        End Get
        Set(ByVal value As String)
            strHosyousyoNoFrom = value
        End Set
    End Property
#End Region
#Region "保証書NO To"
    ''' <summary>
    ''' 保証書NO To hosyousyo_no
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoTo As String
    ''' <summary>
    ''' 保証書NO To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoTo() As String
        Get
            Return strHosyousyoNoTo
        End Get
        Set(ByVal value As String)
            strHosyousyoNoTo = value
        End Set
    End Property
#End Region
#Region "加盟店ｺｰﾄﾞ"
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ kameiten_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region
#Region "加盟店カナ1"
    ''' <summary>
    ''' 加盟店カナ1 tenmei_kana1
    ''' </summary>
    ''' <remarks></remarks>
    Private strTenmeiKana1 As String
    ''' <summary>
    ''' 加盟店カナ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店カナ1</returns>
    ''' <remarks></remarks>
    Public Property TenmeiKana1() As String
        Get
            Return strTenmeiKana1
        End Get
        Set(ByVal value As String)
            strTenmeiKana1 = value
        End Set
    End Property
#End Region
#Region "系列ｺｰﾄﾞ"
    ''' <summary>
    ''' 系列ｺｰﾄﾞ keiretu_cd 
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 系列ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region
#Region "調査会社ｺｰﾄﾞ"
    ''' <summary>
    ''' 調査会社ｺｰﾄﾞ tys_kaisya_cd tys_kaisya_jigyousyo_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' 調査会社ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region
#Region "調査会社事業所ｺｰﾄﾞ"
    '''' <summary>
    '''' 調査会社事業所ｺｰﾄﾞ tys_kaisya_jigyousyo_cd 
    '''' </summary>
    '''' <remarks></remarks>
    'Private strTysKaisyaJigyousyoCd As String
    '''' <summary>
    '''' 調査会社事業所ｺｰﾄﾞ
    '''' </summary>
    '''' <value></value>
    '''' <returns> 調査会社事業所ｺｰﾄﾞ</returns>
    '''' <remarks></remarks>
    'Public Property TysKaisyaJigyousyoCd() As String
    '    Get
    '        Return strTysKaisyaJigyousyoCd
    '    End Get
    '    Set(ByVal value As String)
    '        strTysKaisyaJigyousyoCd = value
    '    End Set
    'End Property
#End Region
#Region "施主名"
    ''' <summary>
    ''' 施主名 sesyu_mei
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region
#Region "物件住所1"
    ''' <summary>
    ''' 物件住所1 bukken_jyuusyo1
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo1 As String
    ''' <summary>
    ''' 物件住所1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所1</returns>
    ''' <remarks></remarks>
    Public Property BukkenJyuusyo1() As String
        Get
            Return strBukkenJyuusyo1
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "パラメータレコード作成"
    ''' <summary>
    ''' パラメータレコードを作成します
    ''' </summary>
    ''' <param name="param_name">パラメータ名</param>
    ''' <param name="db_type">DB属性</param>
    ''' <param name="length">長さ</param>
    ''' <param name="data">設定するデータ</param>
    ''' <returns>パラメータレコード</returns>
    ''' <remarks></remarks>
    Private Function getParamRecord(ByVal param_name As String, _
                                    ByVal db_type As SqlDbType, _
                                    ByVal length As Integer, _
                                    ByVal data As Object) As ParamRecord
        Dim param_rec As New ParamRecord

        param_rec.Param = param_name
        param_rec.DbType = db_type
        param_rec.ParamLength = length
        param_rec.SetData = data

        Return param_rec
    End Function
#End Region
End Class