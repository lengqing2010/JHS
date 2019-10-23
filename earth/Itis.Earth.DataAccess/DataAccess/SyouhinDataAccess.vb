Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' ���i���Ɋւ��鏈�����s���f�[�^�A�N�Z�X�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class SyouhinDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Dim cmnDtAcc As New CmnDataAccess

#Region "���i�̕��ރR�[�h"
    ''' <summary>
    ''' ���i�̕��ރR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum BunruiCdType
        ''' <summary>
        '''�w��Ȃ�
        ''' </summary>
        ''' <remarks></remarks>
        SITEINASI = 999
        ''' <summary>
        '''100�F���������i�˗����.���i����1�j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN100 = 100
        ''' <summary>
        '''110�F���������i�˗����.���i����2�j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN110 = 110
        ''' <summary>
        '''115�F���������i�˗����.���i����2�̔�����z���}�C�i�X�̃f�[�^�j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN115 = 115
        ''' <summary>
        '''120�F���������i�˗����.���i����3�j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN120 = 120
        ''' <summary>
        '''130�F�H�������i�H�����.���ǍH���j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN130 = 130
        ''' <summary>
        '''140�F�ǉ����ǍH���i�H�����.�ǉ����ǍH���j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN140 = 140
        ''' <summary>
        '''150�F�����񍐏��Ĕ��s�萔���i�񍐏����.�����񍐏��Ĕ��s�j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN150 = 150
        ''' <summary>
        '''160�F�H���񍐏��Ĕ��s�萔���i�񍐏����.�H���񍐏��Ĕ��s�j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN160 = 160
        ''' <summary>
        '''170�F�ۏ؏��Ĕ��s�萔���i�ۏ؉��.�񍐏��Ĕ��s�j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN170 = 170
        ''' <summary>
        '''180�F��񕥖߁i�ۏ؉��.��񕥖߁j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN180 = 180
        ''' <summary>
        ''' �o�^��
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN200 = 200
        ''' <summary>
        ''' �̑��i�����c�[����
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN210 = 210
        ''' <summary>
        ''' FC�ȊO�̑��i
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN220 = 220
        ''' <summary>
        ''' FC�̑��i
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN230 = 230
        ''' <summary>
        ''' FC���z����
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN240 = 240
        ''' <summary>
        ''' ���i2(���ރR�[�h:110,115)
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN2 = 800
    End Enum
#End Region

    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �����H�����
    ''' </summary>
    ''' <remarks></remarks>
    Private pIntMode As Integer

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="mode"></param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal mode As BunruiCdType = BunruiCdType.SITEINASI)
        pIntMode = mode
    End Sub

#Region "���i�ݒ���擾"
    ''' <summary>
    ''' ���i���i�ݒ�}�X�^��菤�i�����擾���܂�
    ''' </summary>
    ''' <param name="kakakuSettei">���i���i�ݒ���</param>
    ''' <remarks>�Y���f�[�^�Ȃ��̏ꍇ�A���R�[�h�̏��i�R�[�h���󔒂Őݒ�</remarks>
    Public Sub GetKakakuSetteiInfo(ByRef kakakuSettei As KakakuSetteiRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKakakuSetteiInfo", _
                                                    kakakuSettei)

        ' �p�����[�^
        Const strParamSyouhinKubun As String = "@SYOUHINKUBUN"
        Const strParamCyousaHouhou As String = "@CHOUSAHOUHOU"
        'Const strParamCyousaGaiyou As String = "@CHOUSAGAIYOU"
        Const strParamSyouhinCd As String = "@SYOUHINCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT syouhin_kbn ")
        commandTextSb.Append("  ,tys_gaiyou ")
        commandTextSb.Append("  ,tys_houhou_no ")
        commandTextSb.Append("  ,ISNULL(syouhin_cd,'')    AS syouhin_cd  ")
        commandTextSb.Append("  ,ISNULL(kkk_settei_basyo,0) AS kkk_settei_basyo ")
        commandTextSb.Append(" FROM m_syouhin_kakakusettei WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE syouhin_kbn = " & strParamSyouhinKubun)
        commandTextSb.Append("   AND tys_houhou_no = " & strParamCyousaHouhou)
        commandTextSb.Append("   AND syouhin_cd = " & strParamSyouhinCd)
        'commandTextSb.Append("   AND tys_gaiyou = " & strParamCyousaGaiyou)
        commandTextSb.Append(" ORDER BY tys_gaiyou ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamSyouhinKubun, SqlDbType.Int, 0, kakakuSettei.SyouhinKbn), _
             SQLHelper.MakeParam(strParamCyousaHouhou, SqlDbType.Int, 0, kakakuSettei.TyousaHouhouNo), _
             SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.VarChar, 8, kakakuSettei.SyouhinCd)}

        ' �f�[�^�̎擾
        Dim kakakuSetteiDataSet As New KakakuSetteiDataSet()

        ' �������s
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kakakuSetteiDataSet, kakakuSetteiDataSet.SyouhinKakakuSetteiTable.TableName, commandParameters)

        Dim kakakuSetteiTable As KakakuSetteiDataSet.SyouhinKakakuSetteiTableDataTable = _
                    kakakuSetteiDataSet.SyouhinKakakuSetteiTable

        If kakakuSetteiTable.Count <> 0 Then
            ' �擾�ł����ꍇ�A�s�����擾���Q�ƃ��R�[�h�ɐݒ肷��
            Dim row As KakakuSetteiDataSet.SyouhinKakakuSetteiTableRow = kakakuSetteiTable(0)
            kakakuSettei.SyouhinCd = row.syouhin_cd
            kakakuSettei.KakakuSettei = row.kkk_settei_basyo
            kakakuSettei.TyousaGaiyou = row.tys_gaiyou
        End If

    End Sub
#End Region

#Region "���i���擾(���i�P�� �Q�Ɠn��)"
    ''' <summary>
    ''' ���i���A��������擾���܂�
    ''' </summary>
    ''' <param name="syouhinCd">���i�R�[�h</param>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Public Function GetSyouhinInfo(ByVal syouhinCd As String, ByVal kameitenCd As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo", _
                                                    syouhinCd, kameitenCd)

        ' �p�����[�^
        Const strParamSyouhinCd As String = "@SYOUHIN_CD"
        Const strParamKameitenCd As String = "@KAMEITEN_CD"

        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    s.syouhin_cd")
        sb.AppendLine("    , ISNULL(s.souko_cd, '') AS souko_cd")
        sb.AppendLine("    , ISNULL(s.syouhin_mei, '') AS syouhin_mei")
        sb.AppendLine("    , ISNULL(s.zei_kbn, '') AS zei_kbn")
        sb.AppendLine("    , ISNULL(s.hyoujun_kkk, 0) AS hyoujun_kkk")
        sb.AppendLine("    , ISNULL(z.zeiritu, 0) AS zeiritu")
        sb.AppendLine("    , ISNULL(vsk.kameiten_cd, '') AS kameiten_cd")
        sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_cd, '') AS seikyuu_saki_cd")
        sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_kbn, '') AS seikyuu_saki_kbn")
        sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_brc, '') AS seikyuu_saki_brc ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.m_syouhin s WITH (READCOMMITTED) ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_syouhizei z WITH (READCOMMITTED) ")
        sb.AppendLine("        ON s.zei_kbn = z.zei_kbn ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.v_syouhin_seikyuusaki_kameiten vsk ")
        sb.AppendLine("        ON s.syouhin_cd = vsk.syouhin_cd ")
        sb.AppendLine("        AND vsk.kameiten_cd = " & strParamKameitenCd)
        sb.AppendLine("WHERE")
        sb.AppendLine("    s.syouhin_cd = " & strParamSyouhinCd)
        sb.AppendLine(" AND ")
        sb.AppendLine("    s.souko_cd = '100' ")
        'sb.AppendLine(" AND ")
        'sb.AppendLine("    s.torikesi = 0 ")
        sb.AppendLine("")

        ' �p�����[�^�֐ݒ�
        Dim sqlParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.VarChar, 8, syouhinCd), _
             SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)

    End Function
#End Region

    ''' <summary>
    ''' ���i���擾
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, Optional ByVal blnTorikesi As Boolean = False) As DataTable
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfoRec" _
                                                    , strSyouhinCd)
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      MS.syouhin_cd  ")
        cmdTextSb.Append("    , MS.syouhin_mei ")
        cmdTextSb.Append("    , MS.torikesi  ")
        cmdTextSb.Append("    , MS.zei_kbn ")
        cmdTextSb.Append("    , MS.hyoujun_kkk ")
        cmdTextSb.Append("    , MS.souko_cd ")
        cmdTextSb.Append("    , MS.koj_type  ")
        cmdTextSb.Append("    , MS.tys_umu_kbn  ")
        cmdTextSb.Append("    , MZ.zeiritu  ")
        cmdTextSb.Append("    , MS.sds_jidou_set  ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      m_syouhin MS ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_syouhizei MZ ")
        cmdTextSb.Append("             ON MS.zei_kbn= MZ.zei_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      MS.syouhin_cd = @SYOUHINCD ")
        If blnTorikesi Then
            cmdTextSb.Append("  AND MS.torikesi = 0")
        End If

        ' �p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam("@SYOUHINCD", SqlDbType.VarChar, 8, strSyouhinCd)}

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

#Region "���i���擾"
    ''' <summary>
    ''' ���i�����擾���܂�
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strSyouhinNm">���i��</param>
    ''' <param name="kbnType">���i���ނ̎��ʃ^�C�v</param>
    ''' <param name="kameitenCd">�����X�R�[�h(Optional)</param>
    ''' <returns>���i���DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, _
                                   ByVal strSyouhinNm As String, _
                                   ByRef kbnType As EarthEnum.EnumSyouhinKubun, _
                                   Optional ByVal kameitenCd As String = "") _
                                   As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin23Info", _
                                                    strSyouhinCd, _
                                                    strSyouhinNm, _
                                                    kbnType, _
                                                    kameitenCd)

        ' �p�����[�^
        Const strParamSyouhinCd As String = "@SYOUHINCD"
        Const strParamSyouhinNm As String = "@SYOUHINNM"
        Const strParamKameitenCd As String = "@KAMEITEN_CD"

        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    s.syouhin_cd")
        sb.AppendLine("    , s.syouhin_mei AS syouhin_mei")
        sb.AppendLine("    , s.syouhin_kbn3 AS syouhin_kbn3")
        sb.AppendLine("    , s.hyoujun_kkk AS hyoujun_kkk")
        sb.AppendLine("    , s.souko_cd AS souko_cd")
        sb.AppendLine("    , s.zei_kbn AS zei_kbn")
        sb.AppendLine("    , s.siire_kkk AS siire_kkk")
        sb.AppendLine("    , ISNULL(z.zeiritu, 0) AS zeiritu")
        sb.AppendLine("    , s.hosyou_umu AS hosyou_umu")
        If kameitenCd <> String.Empty Then
            sb.AppendLine("    , ISNULL(vsk.kameiten_cd, '') AS kameiten_cd")
            sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_cd, '') AS seikyuu_saki_cd")
            sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_kbn, '') AS seikyuu_saki_kbn")
            sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_brc, '') AS seikyuu_saki_brc ")
        End If
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.m_syouhin s WITH (READCOMMITTED) ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_syouhizei z WITH (READCOMMITTED) ")
        sb.AppendLine("        ON s.zei_kbn = z.zei_kbn ")
        If kameitenCd <> String.Empty Then
            sb.AppendLine("    LEFT OUTER JOIN jhs_sys.v_syouhin_seikyuusaki_kameiten vsk ")
            sb.AppendLine("        ON s.syouhin_cd = vsk.syouhin_cd ")
            sb.AppendLine("        AND vsk.kameiten_cd = " & strParamKameitenCd)
        End If
        sb.AppendLine(" WHERE 0 = 0 ")

        ' �擾���鏤�i���ނɂ������𕪂���
        Dim strSoukoCd As String = String.Empty
        Select Case kbnType
            Case EarthEnum.EnumSyouhinKubun.Syouhin2_110, EarthEnum.EnumSyouhinKubun.Syouhin2_115
                strSoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_110 + "' OR s.souko_cd = '" + EarthConst.SOUKO_CD_SYOUHIN_2_115
            Case EarthEnum.EnumSyouhinKubun.AllSyouhin, EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi
                strSoukoCd = String.Empty
            Case Else
                strSoukoCd = CInt(kbnType).ToString
        End Select
        If strSoukoCd <> String.Empty Then
            sb.AppendLine(" AND ( s.souko_cd = '" + strSoukoCd + "' ) ")
        Else
            '�����Ǎ����̏ꍇ�A���i����f�[�^(�q�ɃR�[�h=0)�����o�����Ɋ܂߂�
            If kbnType = EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi Then
            Else
                '�q�ɃR�[�h���w�肵�Ȃ��ꍇ�́A�q�ɃR�[�h'0'�����O
                sb.AppendLine(" AND  s.souko_cd <> '0'  ")
            End If

        End If

        ' ���i�R�[�h�������ɑ��݂���ꍇ
        If strSyouhinCd <> String.Empty Then
            sb.AppendLine(" AND s.syouhin_cd like " & strParamSyouhinCd)
        End If

        ' ���i���������ɑ��݂���ꍇ
        If strSyouhinNm <> String.Empty Then
            sb.AppendLine(" AND s.syouhin_mei like " & strParamSyouhinNm)
        End If

        ' �p�����[�^�֐ݒ�
        Dim sqlParams() As SqlParameter = {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.VarChar, 9, strSyouhinCd & Chr(37)), _
                                           SQLHelper.MakeParam(strParamSyouhinNm, SqlDbType.VarChar, 40, strSyouhinNm & Chr(37)), _
                                           SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}
        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)

    End Function
#End Region

#Region "����X���i�Q�擾"
    ''' <summary>
    ''' ����X���i�Q���擾���܂�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>���i���DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetTokuteitenSyouhin2(ByVal strKameitenCd As String) _
                                     As SyouhinDataSet.SyouhinTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokuteitenSyouhin2", _
                                                    strKameitenCd)

        ' �p�����[�^
        Const strParamKameitenCd As String = "@KAMEITENCD"

        ' SQL����
        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT s.syouhin_cd ")
        commandTextSb.Append("   ,s.syouhin_mei AS syouhin_mei ")
        commandTextSb.Append(" FROM m_tokuteiten_syouhin2_settei tok WITH (READCOMMITTED) ")
        commandTextSb.Append(" INNER JOIN m_syouhin s WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON tok.syouhin_cd = s.syouhin_cd ")
        commandTextSb.Append(" WHERE ( s.souko_cd = '110' OR s.souko_cd = '115' ) ")
        commandTextSb.Append(" AND tok.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND s.torikesi = 0 ")
        commandTextSb.Append(" ORDER BY s.syouhin_cd ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = Nothing

        ' �p�����[�^��ݒ�
        commandParameters = New SqlParameter() {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' �f�[�^�̎擾
        Dim syouhinDataSet As New SyouhinDataSet()

        ' �������s(�p�����[�^�L��)
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            syouhinDataSet, syouhinDataSet.SyouhinTable.TableName, commandParameters)

        Dim syouhinTable As SyouhinDataSet.SyouhinTableDataTable = _
                    syouhinDataSet.SyouhinTable

        Return syouhinTable

    End Function
#End Region

#Region "���������i�R�[�h�Q�擾"
    ''' <summary>
    ''' ���������ݒ�}�X�^��菤�i�R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <param name="touKbn">���敪</param>
    ''' <returns>�������̏��i�R�[�h�Q</returns>
    ''' <remarks>�Y���f�[�^�Ȃ��̏ꍇ�A�߂�l���󔒂ŕԋp</remarks>
    Public Function GetTatouwariSyouhinCd(ByVal kameitenCd As String, _
                                          ByVal touKbn As Integer) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTatouwariSyouhinCd", _
                                                    kameitenCd, _
                                                    touKbn)

        ' �p�����[�^
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamTouKubun As String = "@TOUKUBUN"

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TS.syouhin_cd ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      m_tatouwaribiki_settei TS ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_syouhin MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("  (   TS.syouhin_cd = '00000' ")
        cmdTextSb.Append("      OR ")
        cmdTextSb.Append("      (MS.torikesi = 0 AND MS.souko_cd <> '0') ")
        cmdTextSb.Append("  ) ")
        cmdTextSb.Append("  AND TS.kameiten_cd = " & strParamKameitenCd)
        cmdTextSb.Append("  AND TS.toukubun = " & strParamTouKubun)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 8, kameitenCd), _
             SQLHelper.MakeParam(strParamTouKubun, SqlDbType.Int, 0, touKbn)}

        ' �f�[�^�̎擾
        Dim syouhinDataSet As New SyouhinDataSet()

        ' �������s
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            syouhinDataSet, syouhinDataSet.TatouwariTable.TableName, commandParameters)

        Dim tatouSetteiTable As SyouhinDataSet.TatouwariTableDataTable = _
                    syouhinDataSet.TatouwariTable

        If tatouSetteiTable.Count <> 0 Then

            ' �擾�ł����ꍇ�A�s�����擾
            Dim row As SyouhinDataSet.TatouwariTableRow = tatouSetteiTable(0)

            ' �擾�l��Null�̏ꍇ�͋󔒂�Ԃ�
            If row.syouhin_cd Is Nothing OrElse row.syouhin_cd = String.Empty Then
                Return EarthConst.SH_CD_TATOUWARI_ER
            Else
                Return row.syouhin_cd
            End If
        End If

        Return ""

    End Function
#End Region

#Region "���i�R���{�f�[�^�쐬"
    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���ȏ��i���R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As SyouhinDataSet.SyouhinTableRow

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    syouhin_cd ")
        commandTextSb.Append("    ,syouhin_mei ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    jhs_sys.m_syouhin WITH (READCOMMITTED) ")
        If pIntMode = BunruiCdType.SITEINASI Then '�w��Ȃ�
        Else
            commandTextSb.Append(" WHERE ")
            If pIntMode = BunruiCdType.SYOUHIN2 Then '���i2���w��(110,115)
                commandTextSb.Append(String.Format(" souko_cd IN ('{0}','{1}') ", CStr(BunruiCdType.SYOUHIN110), CStr(BunruiCdType.SYOUHIN115)))
            Else '���ރR�[�h��P��w��
                commandTextSb.Append(String.Format(" souko_cd = '{0}' ", pIntMode.ToString))
            End If
        End If
        '����̏ꍇ��DDL�Ɋ܂߂Ȃ�
        If blnTorikesi Then
            commandTextSb.Append(" AND torikesi = 0 ")
        End If
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("    syouhin_cd")

        ' �f�[�^�̎擾
        Dim dsSyouhin As New SyouhinDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            dsSyouhin, dsSyouhin.SyouhinTable.TableName)

        Dim syouhinDataTable As SyouhinDataSet.SyouhinTableDataTable = _
                    dsSyouhin.SyouhinTable

        If syouhinDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In syouhinDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.syouhin_cd + ":" + row.syouhin_mei, row.syouhin_cd, dt))
                Else
                    dt.Rows.Add(CreateRow(row.syouhin_mei, row.syouhin_cd, dt))
                End If
            Next

        End If

    End Sub
#End Region

#Region "�ۏ؏��i�L��"

    ''' <summary>
    ''' ���i���擾
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinHosyouUmuInfo(ByVal strSyouhinCd As String) As DataTable
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinHosyouUmuInfo" _
                                                    , strSyouhinCd)
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      ISNULL(MAX(MS.hosyou_umu), '') AS hosyou_umu ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      m_syouhin MS ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND MS.syouhin_cd IN('" & strSyouhinCd.Replace(EarthConst.SEP_STRING, "','") & "') ")

        ' �p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = {}

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

#End Region

#Region "�q�ɃR�[�h"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="intMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinSoukoCd(ByVal intMode As Integer) As DataTable
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinSoukoCd" _
                                                    , intMode)

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("    syouhin_cd ")
        cmdTextSb.AppendLine("    ,souko_cd ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("    jhs_sys.m_syouhin WITH (READCOMMITTED) ")
        cmdTextSb.AppendLine(" WHERE ")

        If intMode = BunruiCdType.SYOUHIN2 Then '���i2���w��(110,115)
            cmdTextSb.Append(String.Format(" souko_cd IN ('{0}','{1}') ", CStr(BunruiCdType.SYOUHIN110), CStr(BunruiCdType.SYOUHIN115)))
        Else '���ރR�[�h��P��w��
            cmdTextSb.Append(String.Format(" souko_cd = '{0}' ", pIntMode.ToString))
        End If
        '����̏ꍇ��DDL�Ɋ܂߂Ȃ�
        If intMode = BunruiCdType.SYOUHIN100 Then
            cmdTextSb.Append(" AND torikesi = 0 ")
        End If

        cmdTextSb.AppendLine(" ORDER BY ")
        cmdTextSb.AppendLine("    syouhin_cd")

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString)

    End Function
#End Region

End Class
