Imports System.text
Imports System.Data.SqlClient
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data

Public Class JibanSearchCondition

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' ���ڂ̏�����
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
    ''' �{�N���X�ɐݒ肳��Ă���f�[�^�Ō����p��SQL�y�уp�����[�^�𐶐����܂�
    ''' </summary>
    ''' <param name="sql_params">SQL�p�����[�^�z��i���̃C���X�^���X�Ƀp�����[�^���ݒ肳��܂��j</param> 
    ''' <returns>�����p��SQL��</returns>
    ''' <remarks></remarks>
    Public Function getSqlCondition(ByRef sql_params As SqlParameter()) As String

        Dim param_array As New ArrayList

        Dim sql As String = ""
        Dim sql_condition As New StringBuilder
        Dim isWhere As Boolean = False

        ' �����̊�{�ƂȂ�SQL��
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
        ' �敪�����̐ݒ�
        '***********************************************************************
        ' ��ł��ݒ肳��Ă����ꍇ�A�������쐬
        If Not strKbn1 Is String.Empty Or _
           Not strKbn2 Is String.Empty Or _
           Not strKbn3 Is String.Empty Then

            ' �敪���ڐݒ�m�F�p
            Dim kbn_check As Boolean = False

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            sql_condition.Append(" j.kbn IN ( ")

            ' �敪_1�̔���
            If Not strKbn1 Is String.Empty Then
                param_array.Add(getParamRecord("@KBN1", SqlDbType.Char, 1, strKbn1))
                sql_condition.Append("@KBN1")
                kbn_check = True
            End If

            ' �敪_2�̔���
            If Not strKbn2 Is String.Empty Then
                param_array.Add(getParamRecord("@KBN2", SqlDbType.Char, 1, strKbn2))
                If kbn_check = True Then
                    sql_condition.Append(",")
                End If
                sql_condition.Append("@KBN2")
                kbn_check = True
            End If

            ' �敪_3�̔���
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
        ' �ۏ؏��Ώ۔͈͏����i�ߋ��Q�N�����j
        '***********************************************************************
        ' �ۏ؏��Ώ۔͈͂̔���
        If intHosyousyoNoHani = 1 Then

            ' ���ݓ����̂Q�N�O��Key�Ƃ���
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
        ' �ۏ؏��͈͏����̐ݒ�
        '***********************************************************************
        ' ��ł��ݒ肳��Ă���ꍇ�A�������쐬
        If Not strHosyousyoNoFrom Is String.Empty Or _
           Not strHosyousyoNoTo Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            If Not strHosyousyoNoFrom Is String.Empty And _
               Not strHosyousyoNoTo Is String.Empty Then
                ' �����w��L���BETWEEN
                sql_condition.Append(" j.hosyousyo_no BETWEEN @HOSYOUSYONOFROM AND @HOSYOUSYONOTO")
                param_array.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, strHosyousyoNoFrom))
                param_array.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, strHosyousyoNoTo))
            Else
                If Not strHosyousyoNoFrom Is String.Empty Then
                    ' �ۏ؏�From�̂�
                    sql_condition.Append(" j.hosyousyo_no >= @HOSYOUSYONOFROM ")
                    param_array.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, strHosyousyoNoFrom))
                Else
                    ' �ۏ؏�To�̂�
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
        ' �����X�R�[�h�̐ݒ�
        '***********************************************************************
        ' �����X�R�[�h�̔���
        If Not strKameitenCd Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@KAMEITENCD", SqlDbType.VarChar, 5, strKameitenCd))
            sql_condition.Append(" j.kameiten_cd = @KAMEITENCD ")
        End If

        '***********************************************************************
        ' �����X�J�i�P�̐ݒ�
        '***********************************************************************
        ' �����X�J�i�P�̔���
        If Not strTenmeiKana1 Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@TENMEIKANA1", SqlDbType.VarChar, 20, strTenmeiKana1 & "%"))
            sql_condition.Append(" (k.tenmei_kana1 like @TENMEIKANA1 OR k.tenmei_kana2 like @TENMEIKANA1)")
        End If

        '***********************************************************************
        ' �n���ނ̐ݒ�
        '***********************************************************************
        ' �n���ނ̔���
        If Not strKeiretuCd Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@KEIRETU", SqlDbType.VarChar, 5, strKeiretuCd))
            sql_condition.Append(" k.keiretu_cd = @KEIRETU ")
        End If

        '***********************************************************************
        ' ������к��ނ̐ݒ�
        '***********************************************************************
        ' ������к��ނ̔���
        If Not strTysKaisyaCd Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@TYSKAISYACD", SqlDbType.VarChar, 7, strTysKaisyaCd))
            sql_condition.Append(" RTRIM(j.tys_kaisya_cd) + RTRIM(j.tys_kaisya_jigyousyo_cd) = @TYSKAISYACD ")
            '            sql_condition.Append(" j.tys_kaisya_cd = @TYSKAISYACD ")
        End If

        ' ������ЃR�[�h�Ɋ܂܂��
        '***********************************************************************
        ' ������Ў��Ə����ނ̐ݒ�
        '***********************************************************************
        ' ������Ў��Ə����ނ̔���
        'If Not strTysKaisyaJigyousyoCd Is String.Empty Then

        '    sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
        '    isWhere = True

        '    param_array.Add(getParamRecord("@TYSKAISYAJIGYOUSYOCD", SqlDbType.VarChar, 2, strTysKaisyaJigyousyoCd))
        '    sql_condition.Append(" j.tys_kaisya_jigyousyo_cd = @TYSKAISYAJIGYOUSYOCD ")
        'End If

        '***********************************************************************
        ' �{�喼�̐ݒ�
        '***********************************************************************
        ' �{�喼�̔���
        If Not strSesyuMei Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@SESYUMEI", SqlDbType.VarChar, 50, strSesyuMei & "%"))
            sql_condition.Append(" j.sesyu_mei like @SESYUMEI ")
        End If

        '***********************************************************************
        ' �����Z��1�̐ݒ�
        '***********************************************************************
        ' �����Z��1�̔���
        If Not strBukkenJyuusyo1 Is String.Empty Then

            sql_condition.Append(IIf(isWhere = False, " WHERE ", " AND "))
            isWhere = True

            param_array.Add(getParamRecord("@BUKKENJYUUSYO1", SqlDbType.VarChar, 32, strBukkenJyuusyo1 & "%"))
            sql_condition.Append(" j.bukken_jyuusyo1 like @BUKKENJYUUSYO1 ")
        End If

        ' �p�����[�^�̍쐬
        Dim i As Integer
        Dim cmdParams(param_array.Count - 1) As SqlParameter
        For i = 0 To param_array.Count - 1
            Dim rec As ParamRecord = param_array(i)
            ' �K�v�ȏ����Z�b�g
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' �ԋp�p�p�����[�^�̃Z�b�g
        sql_params = cmdParams

        ' �ŏI�I��SQL���̐���
        sql = String.Format(base_sql, sql_condition.ToString())

        Return sql

    End Function


#Region "�敪_1"
    ''' <summary>
    ''' �敪_1 kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn1 As String
    ''' <summary>
    ''' �敪_1 
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪_1</returns>
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
#Region "�敪_2"
    ''' <summary>
    ''' �敪_2 kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn2 As String
    ''' <summary>
    ''' �敪_2
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪_2</returns>
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
#Region "�敪_3"
    ''' <summary>
    ''' �敪_3  kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn3 As String
    ''' <summary>
    ''' �敪_3
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪_3</returns>
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
#Region "�ۏ؏�NO �Ώ۔͈�"
    ''' <summary>
    ''' �ۏ؏�NO �Ώ۔͈� 
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoNoHani As Integer
    ''' <summary>
    ''' �ۏ؏�NO �Ώ۔͈�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO �Ώ۔͈�</returns>
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
#Region "�ۏ؏�NO From"
    ''' <summary>
    ''' �ۏ؏�NO From hosyousyo_no
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoFrom As String
    ''' <summary>
    ''' �ۏ؏�NO From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO From</returns>
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
#Region "�ۏ؏�NO To"
    ''' <summary>
    ''' �ۏ؏�NO To hosyousyo_no
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoTo As String
    ''' <summary>
    ''' �ۏ؏�NO To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO To</returns>
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
#Region "�����X����"
    ''' <summary>
    ''' �����X���� kameiten_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' �����X����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X����</returns>
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
#Region "�����X�J�i1"
    ''' <summary>
    ''' �����X�J�i1 tenmei_kana1
    ''' </summary>
    ''' <remarks></remarks>
    Private strTenmeiKana1 As String
    ''' <summary>
    ''' �����X�J�i1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X�J�i1</returns>
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
#Region "�n����"
    ''' <summary>
    ''' �n���� keiretu_cd 
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' �n����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �n����</returns>
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
#Region "������к���"
    ''' <summary>
    ''' ������к��� tys_kaisya_cd tys_kaisya_jigyousyo_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ������к���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������к���</returns>
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
#Region "������Ў��Ə�����"
    '''' <summary>
    '''' ������Ў��Ə����� tys_kaisya_jigyousyo_cd 
    '''' </summary>
    '''' <remarks></remarks>
    'Private strTysKaisyaJigyousyoCd As String
    '''' <summary>
    '''' ������Ў��Ə�����
    '''' </summary>
    '''' <value></value>
    '''' <returns> ������Ў��Ə�����</returns>
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
#Region "�{�喼"
    ''' <summary>
    ''' �{�喼 sesyu_mei
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns> �{�喼</returns>
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
#Region "�����Z��1"
    ''' <summary>
    ''' �����Z��1 bukken_jyuusyo1
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo1 As String
    ''' <summary>
    ''' �����Z��1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��1</returns>
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

#Region "�p�����[�^���R�[�h�쐬"
    ''' <summary>
    ''' �p�����[�^���R�[�h���쐬���܂�
    ''' </summary>
    ''' <param name="param_name">�p�����[�^��</param>
    ''' <param name="db_type">DB����</param>
    ''' <param name="length">����</param>
    ''' <param name="data">�ݒ肷��f�[�^</param>
    ''' <returns>�p�����[�^���R�[�h</returns>
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