Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class ZensyaSyukeiInquiryDA


    '''' <summary>
    '''' �f�[�^�L��
    '''' </summary>
    '''' <param name="strKeikakuNendo">�v��_�N�x</param>
    '''' <returns>�N�x�f�[�^</returns>
    '''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    'Public Function SelNendoData(ByVal strKeikakuNendo As String) As Data.DataTable
    '    'EMAB��Q�Ή����̊i�[����
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
    '    '�߂�f�[�^�Z�b�g
    '    Dim dsReturn As New Data.DataSet

    '    'SQL�R�����g
    '    Dim commandTextSb As New StringBuilder

    '    '�o�����^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("	* ")
    '        .AppendLine("FROM ")
    '        .AppendLine("	t_keikaku_kanri WITH (READCOMMITTED) ")
    '        .AppendLine("WHERE ")
    '        .AppendLine("keikaku_nendo=@strKeikakuNendo")

    '    End With
    '    '�o�����^
    '    paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x
    '    '���s
    '    FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoData", paramList.ToArray)

    '    '�߂�l
    '    Return dsReturn.Tables("nendoData")
    'End Function

    ''' <summary>
    ''' 4���`3���̌v�挏���̏W�v�l�A�v����z�̏W�v�l�A�v��e���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��_�N�x</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelNendoKeikaku(ByVal strKeikakuNendo As String) As DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("       sum(sub.keikakuKensuuCount)")
            .AppendLine("      ,sum(sub.keikakuKingakuCount)")
            .AppendLine("      ,sum(sub.keikakuArariCount)")
            .AppendLine("      ,sum(rowsCount)")
            .AppendLine("FROM(")
            .AppendLine("SELECT ")
            .AppendLine("	(ISNULL(sum(TZKK.[1gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[2gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[3gatu_keikaku_kensuu]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[4gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[5gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[6gatu_keikaku_kensuu]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[7gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[8gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[9gatu_keikaku_kensuu]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[10gatu_keikaku_kensuu]),0)+ISNULL(sum([11gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[12gatu_keikaku_kensuu]),0))")
            .AppendLine("    as keikakuKensuuCount ")    '4���`3���v�挏���̏W�v�l
            .AppendLine("   ,(ISNULL(sum(TZKK.[1gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[2gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[3gatu_keikaku_kingaku]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[4gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[5gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[6gatu_keikaku_kingaku]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[7gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[8gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[9gatu_keikaku_kingaku]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[10gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[11gatu_keikaku_kingaku]),0)+ISNULL(sum([12gatu_keikaku_kingaku]),0))")
            .AppendLine("    as keikakuKingakuCount ")  '4���`3���v����z�̏W�v�l
            .AppendLine("   ,(ISNULL(sum(TZKK.[1gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[2gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[3gatu_keikaku_arari]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[4gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[5gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[6gatu_keikaku_arari]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[7gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[8gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[9gatu_keikaku_arari]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[10gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[11gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[12gatu_keikaku_arari]),0))")
            .AppendLine("   as keikakuArariCount ")     '4���`3���v��e���̏W�v�l
            .AppendLine("  ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("	   t_keikaku_kanri AS TZKK WITH (READCOMMITTED)   ")
            .AppendLine("      INNER JOIN   ")
            .AppendLine("      m_keikaku_kameiten AS MKK  WITH (READCOMMITTED) ")
            .AppendLine("      ON")
            .AppendLine("            TZKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("            AND")
            .AppendLine("            TZKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("      INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("      ON")
            .AppendLine("      TZKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("      AND")
            .AppendLine("      TZKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine("WHERE TZKK.keikaku_nendo=@strKeikakuNendo")
            .AppendLine("      AND EXISTS(")
            .AppendLine("                      SELECT keikaku_nendo")
            .AppendLine("                      FROM  ")
            .AppendLine("                             t_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED) ")
            .AppendLine("                      WHERE")
            .AppendLine("                             SUB_TZKK.keikaku_nendo =TZKK.keikaku_nendo ")
            .AppendLine("                             AND   SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND   SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                      GROUP BY")
            .AppendLine("                             keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd")
            .AppendLine("                      HAVING")
            .AppendLine("                             TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                             AND ")
            .AppendLine("                             CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,TZKK.add_datetime,121) ")
            .AppendLine("                             = MAX")
            .AppendLine("                             (")
            .AppendLine("                              CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)")
            .AppendLine("                             ) ")
            .AppendLine("                ) ")
            .AppendLine(" UNION ALL")
            .AppendLine("SELECT")
            .AppendLine("         (ISNULL(sum(TFKK.[1gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[2gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[3gatu_keikaku_kensuu]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[4gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[5gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[6gatu_keikaku_kensuu]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[7gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[8gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[9gatu_keikaku_kensuu]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[10gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[11gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[12gatu_keikaku_kensuu]),0))")
            .AppendLine("         as keikakuKensuuCount ")
            .AppendLine("         ,(ISNULL(sum(TFKK.[1gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[2gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[3gatu_keikaku_kingaku]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[4gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[5gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[6gatu_keikaku_kingaku]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[7gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[8gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[9gatu_keikaku_kingaku]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[10gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[11gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[12gatu_keikaku_kingaku]),0))")
            .AppendLine("         as keikakuKingakuCount ")
            .AppendLine("         , (ISNULL(sum(TFKK.[1gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[2gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[3gatu_keikaku_arari]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[4gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[5gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[6gatu_keikaku_arari]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[7gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[8gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[9gatu_keikaku_arari]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[10gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[11gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[12gatu_keikaku_arari]),0))")
            .AppendLine("         as keikakuArariCount ")
            .AppendLine("         ,COUNT(*) AS rowsCount")
            .AppendLine(" FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("  ON")
            .AppendLine("      TFKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("  AND")
            .AppendLine("      TFKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine(" WHERE EXISTS(  ")
            .AppendLine("     SELECT keikaku_nendo,  ")
            .AppendLine("            MAX(add_datetime) AS add_datetime,  ")
            .AppendLine("            busyo_cd,  ")
            .AppendLine("            keikaku_kanri_syouhin_cd  ")
            .AppendLine("     FROM t_fc_keikaku_kanri AS SUB_TFKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE keikaku_nendo = @strKeikakuNendo   ")
            .AppendLine("     GROUP BY keikaku_nendo,  ")
            .AppendLine("              busyo_cd,  ")
            .AppendLine("              keikaku_kanri_syouhin_cd  ")
            .AppendLine("     HAVING TFKK.keikaku_nendo = SUB_TFKK.keikaku_nendo  ")
            .AppendLine("     AND TFKK.busyo_cd = SUB_TFKK.busyo_cd  ")
            .AppendLine("     AND TFKK.keikaku_kanri_syouhin_cd = SUB_TFKK.keikaku_kanri_syouhin_cd  ")
            .AppendLine("     AND CASE WHEN ISNULL(CONVERT(VARCHAR,TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END  ")
            .AppendLine("         + CONVERT(VARCHAR,TFKK.add_datetime,121)  ")
            .AppendLine("         = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("         + CONVERT(VARCHAR,SUB_TFKK.add_datetime,121))  ")
            .AppendLine("     )  ")
            .AppendLine(" AND TFKK.keikaku_nendo = @strKeikakuNendo ")
            .AppendLine(" GROUP BY TFKK.keikaku_kanri_syouhin_cd ) sub  ")

        End With

        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x
        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoKeikaku", paramList.ToArray)

        Return dsReturn.Tables("nendoKeikaku")

    End Function

    '''' <summary>
    '''' �f�[�^�L��
    '''' </summary>
    '''' <param name="strKeikakuNendo">�N�x</param>
    '''' <returns>�N�x�f�[�^</returns>
    '''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    'Public Function SelNendoKensuuData(ByVal strKeikakuNendo As String) As Data.DataTable
    '    'EMAB��Q�Ή����̊i�[����
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
    '    '�߂�f�[�^�Z�b�g
    '    Dim dsReturn As New Data.DataSet

    '    'SQL�R�����g
    '    Dim commandTextSb As New StringBuilder

    '    '�o�����^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("	* ")
    '        .AppendLine("FROM ")
    '        .AppendLine("	t_jisseki_kanri WITH (READCOMMITTED) ")
    '        .AppendLine("WHERE ")
    '        .AppendLine("keikaku_nendo=@strKeikakuNendo")

    '    End With
    '    '�o�����^
    '    paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x
    '    '���s
    '    FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoKensuuData", paramList.ToArray)

    '    '�߂�l
    '    Return dsReturn.Tables("nendoKensuuData")
    'End Function

    ''' <summary>
    ''' �I��N�x�ɉ������N�x�́u���ь����v
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��_�N�x</param>
    ''' <returns>���ь����W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelNendoJissekiKensuu(ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	(ISNULL(SUM([1gatu_jisseki_kensuu]),0)+ISNULL(SUM([2gatu_jisseki_kensuu]),0)+ISNULL(SUM([3gatu_jisseki_kensuu]),0)  ")
            .AppendLine("      +ISNULL(SUM([4gatu_jisseki_kensuu]),0)+ISNULL(SUM([5gatu_jisseki_kensuu]),0)+ISNULL(SUM([6gatu_jisseki_kensuu]),0)")
            .AppendLine("    +ISNULL(SUM([7gatu_jisseki_kensuu]),0)+ISNULL(SUM([8gatu_jisseki_kensuu]),0)+ISNULL(SUM([9gatu_jisseki_kensuu]),0)")
            .AppendLine("      +ISNULL(SUM([10gatu_jisseki_kensuu]),0)+ISNULL(SUM([11gatu_jisseki_kensuu]),0)+ISNULL(SUM([12gatu_jisseki_kensuu]),0))")
            .AppendLine("   AS  keikakuKensuuCount ")     '4���`3�����ь����̏W�v�l    
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")
            .AppendLine("    AND")
            .AppendLine("    MKKS.bunbetu_cd='1' ")

        End With
        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x
        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoJittusekiKensuu", paramList.ToArray)

        Return dsReturn.Tables("nendoJittusekiKensuu")
    End Function

    ''' <summary>
    ''' ���ы��z�A���ёe���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��_�N�x</param>
    ''' <returns>���ы��z�A���ёe���̏W�v�l�̃f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelNendoJisseki(ByVal strKeikakuNendo As String) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	(ISNULL(SUM([1gatu_jisseki_kingaku]),0)+ISNULL(SUM([2gatu_jisseki_kingaku]),0)+ISNULL(SUM([3gatu_jisseki_kingaku]),0)")
            .AppendLine("   +ISNULL(SUM([4gatu_jisseki_kingaku]),0)+ISNULL(SUM([5gatu_jisseki_kingaku]),0)+ISNULL(SUM([6gatu_jisseki_kingaku]),0)")
            .AppendLine("   +ISNULL(SUM([7gatu_jisseki_kingaku]),0)+ISNULL(SUM([8gatu_jisseki_kingaku]),0)+ISNULL(SUM([9gatu_jisseki_kingaku]),0)")
            .AppendLine("   +ISNULL(SUM([10gatu_jisseki_kingaku]),0)+ISNULL(SUM([11gatu_jisseki_kingaku]),0)+ISNULL(SUM([12gatu_jisseki_kingaku]),0))")
            .AppendLine("   AS jissekiKingakuCount ")  '4���`3�����ы��z�̏W�v�l
            .AppendLine("   ,(ISNULL(SUM([1gatu_jisseki_arari]),0)+ISNULL(SUM([2gatu_jisseki_arari]),0)+ISNULL(SUM([3gatu_jisseki_arari]),0)")
            .AppendLine("   +ISNULL(SUM([4gatu_jisseki_arari]),0)+ISNULL(SUM([5gatu_jisseki_arari]),0)+ISNULL(SUM([6gatu_jisseki_arari]),0)")
            .AppendLine("   +ISNULL(SUM([7gatu_jisseki_arari]),0)+ISNULL(SUM([8gatu_jisseki_arari]),0)+ISNULL(SUM([9gatu_jisseki_arari]),0)")
            .AppendLine("   +ISNULL(SUM([10gatu_jisseki_arari]),0)+ISNULL(SUM([11gatu_jisseki_arari]),0)+ISNULL(SUM([12gatu_jisseki_arari]),0))")
            .AppendLine("   AS jissekiArariCount")     '4���`3�����ёe���̏W�v�l
            .AppendLine("   ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")

        End With
        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x
        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoJittuseki", paramList.ToArray)

        Return dsReturn.Tables("nendoJittuseki")
    End Function

    ''' <summary>
    ''' ���Ԍv�挏���̏W�v�l�A�v����z�̏W�v�l�A�v��e���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="strKikan">����</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKikanKeikaku(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("       sum(sub.keikakuKensuuCount)")
            .AppendLine("      ,sum(sub.keikakuKingakuCount)")
            .AppendLine("      ,sum(sub.keikakuArariCount)")
            .AppendLine("      ,sum(rowsCount)")

            .AppendLine("FROM(")
            .AppendLine("SELECT ")
            If strKikan = "���" Then
                .AppendLine("ISNULL(SUM(TZKK.[4gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_kensuu]),0)") '�v�挏��
            End If
            If strKikan = "�l����(4,5,6��)" Then
                .AppendLine("ISNULL(SUM(TZKK.[4gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_kensuu]),0)") '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "���" Then
                .AppendLine("+")
            End If
            If strKikan = "���" Then
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_kensuu]),0)")          '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "�l����(7,8,9��)" Then
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_kensuu]),0)")          '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "����" Then
                .AppendLine("ISNULL(SUM(TZKK.[10gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_kensuu]),0)")       '�v�挏��
            End If
            If strKikan = "�l����(10,11,12��)" Then
                .AppendLine("ISNULL(SUM(TZKK.[10gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_kensuu]),0)")       '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" Then
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_kensuu]),0)")          '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "�l����(1,2,3��)" Then
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_kensuu]),0)")          '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "���" Then
                .AppendLine(",ISNULL(SUM(TZKK.[4gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_kingaku]),0)")       '�v����z
            End If
            If strKikan = "�l����(4,5,6��)" Then
                .AppendLine(",ISNULL(SUM(TZKK.[4gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "���" Then
                .AppendLine("+")
            End If
            If strKikan = "���" Then
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "�l����(7,8,9��)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "����" Then
                .AppendLine(",ISNULL(SUM(TZKK.[10gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_kingaku]),0)")    '�v����z
            End If
            If strKikan = "�l����(10,11,12��)" Then
                .AppendLine(",ISNULL(SUM(TZKK.[10gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_kingaku]),0)")    '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" Then
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "�l����(1,2,3��)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "���" Then
                .AppendLine(",ISNULL(SUM(TZKK.[4gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_arari]),0)")             '�v��e��
            End If
            If strKikan = "�l����(4,5,6��)" Then
                .AppendLine(",ISNULL(SUM(TZKK.[4gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_arari]),0)")             '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "���" Then
                .AppendLine(" +")
            End If
            If strKikan = "���" Then
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_arari]),0) ")            '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "�l����(7,8,9��)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_arari]),0) ")            '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "����" Then
                .AppendLine(",ISNULL(SUM(TZKK.[10gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_arari]),0) ")         '�v��e��
            End If
            If strKikan = "�l����(10,11,12��)" Then
                .AppendLine(",ISNULL(SUM(TZKK.[10gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_arari]),0) ")         '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" Then
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_arari]),0)")             '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "�l����(1,2,3��)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_arari]),0)")             '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            .AppendLine(",COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("	   t_keikaku_kanri AS TZKK WITH (READCOMMITTED)   ")
            .AppendLine("      INNER JOIN   ")
            .AppendLine("      m_keikaku_kameiten AS MKK  WITH (READCOMMITTED) ")
            .AppendLine("      ON")
            .AppendLine("            TZKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("            AND")
            .AppendLine("            TZKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("      INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("      ON")
            .AppendLine("      TZKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("      AND")
            .AppendLine("      TZKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine("WHERE TZKK.keikaku_nendo=@strKeikakuNendo")
            .AppendLine("      AND EXISTS(")
            .AppendLine("                      SELECT keikaku_nendo")
            .AppendLine("                      FROM  ")
            .AppendLine("                             t_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED) ")
            .AppendLine("                      WHERE")
            .AppendLine("                             SUB_TZKK.keikaku_nendo =TZKK.keikaku_nendo ")
            .AppendLine("                             AND   SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND   SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                      GROUP BY")
            .AppendLine("                             keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd")
            .AppendLine("                      HAVING")
            .AppendLine("                             TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                             AND ")
            .AppendLine("                             CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,TZKK.add_datetime,121) ")
            .AppendLine("                             = MAX")
            .AppendLine("                             (")
            .AppendLine("                              CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)")
            .AppendLine("                             ) ")
            .AppendLine("                ) ")
            .AppendLine(" UNION ALL")
            .AppendLine("SELECT")
            If strKikan = "���" Then
                .AppendLine("ISNULL(SUM(TFKK.[4gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_kensuu]),0)") '�v�挏��
            End If
            If strKikan = "�l����(4,5,6��)" Then
                .AppendLine("ISNULL(SUM(TFKK.[4gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_kensuu]),0)") '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "���" Then
                .AppendLine("+")
            End If
            If strKikan = "���" Then
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_kensuu]),0)")          '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "�l����(7,8,9��)" Then
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_kensuu]),0)")          '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "����" Then
                .AppendLine("ISNULL(SUM(TFKK.[10gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_kensuu]),0)")       '�v�挏��
            End If
            If strKikan = "�l����(10,11,12��)" Then
                .AppendLine("ISNULL(SUM(TFKK.[10gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_kensuu]),0)")       '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" Then
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_kensuu]),0)")          '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "�l����(1,2,3��)" Then
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_kensuu]),0)")          '�v�挏��
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "���" Then
                .AppendLine(",ISNULL(SUM(TFKK.[4gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_kingaku]),0)")       '�v����z
            End If
            If strKikan = "�l����(4,5,6��)" Then
                .AppendLine(",ISNULL(SUM(TFKK.[4gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "���" Then
                .AppendLine("+")
            End If
            If strKikan = "���" Then
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "�l����(7,8,9��)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "����" Then
                .AppendLine(",ISNULL(SUM(TFKK.[10gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_kingaku]),0)")    '�v����z
            End If
            If strKikan = "�l����(10,11,12��)" Then
                .AppendLine(",ISNULL(SUM(TFKK.[10gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_kingaku]),0)")    '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" Then
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "�l����(1,2,3��)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_kingaku]),0)")       '�v����z
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "���" Then
                .AppendLine(",ISNULL(SUM(TFKK.[4gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_arari]),0)")             '�v��e��
            End If
            If strKikan = "�l����(4,5,6��)" Then
                .AppendLine(",ISNULL(SUM(TFKK.[4gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_arari]),0)")             '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "���" Then
                .AppendLine(" +")
            End If
            If strKikan = "���" Then
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_arari]),0) ")            '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "�l����(7,8,9��)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_arari]),0) ")            '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "����" Then
                .AppendLine(",ISNULL(SUM(TFKK.[10gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_arari]),0) ")         '�v��e��
            End If
            If strKikan = "�l����(10,11,12��)" Then
                .AppendLine(",ISNULL(SUM(TFKK.[10gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_arari]),0) ")         '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" Then
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_arari]),0)")             '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "�l����(1,2,3��)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_arari]),0)")             '�v��e��
                .AppendLine(" as keikakuArariCount")
            End If
            .AppendLine(",COUNT(*) AS rowsCount")
            .AppendLine(" FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("  ON")
            .AppendLine("      TFKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("  AND")
            .AppendLine("      TFKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine(" WHERE EXISTS(  ")
            .AppendLine("     SELECT keikaku_nendo,  ")
            .AppendLine("            MAX(add_datetime) AS add_datetime,  ")
            .AppendLine("            busyo_cd,  ")
            .AppendLine("            keikaku_kanri_syouhin_cd  ")
            .AppendLine("     FROM t_fc_keikaku_kanri AS SUB_TFKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE keikaku_nendo = @strKeikakuNendo   ")
            .AppendLine("     GROUP BY keikaku_nendo,  ")
            .AppendLine("              busyo_cd,  ")
            .AppendLine("              keikaku_kanri_syouhin_cd  ")
            .AppendLine("     HAVING TFKK.keikaku_nendo = SUB_TFKK.keikaku_nendo  ")
            .AppendLine("     AND TFKK.busyo_cd = SUB_TFKK.busyo_cd  ")
            .AppendLine("     AND TFKK.keikaku_kanri_syouhin_cd = SUB_TFKK.keikaku_kanri_syouhin_cd  ")
            .AppendLine("     AND CASE WHEN ISNULL(CONVERT(VARCHAR,TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END  ")
            .AppendLine("         + CONVERT(VARCHAR,TFKK.add_datetime,121)  ")
            .AppendLine("         = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("         + CONVERT(VARCHAR,SUB_TFKK.add_datetime,121))  ")
            .AppendLine("     )  ")
            .AppendLine(" AND TFKK.keikaku_nendo = @strKeikakuNendo ")
            .AppendLine(" GROUP BY TFKK.keikaku_kanri_syouhin_cd ) sub  ")
        End With
        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "kikanKeikaku", paramList.ToArray)

        Return dsReturn.Tables("kikanKeikaku")
    End Function

    ''' <summary>
    ''' ���Ԏ��ь����̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="strKikan">����</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKikanJissekiKensuu(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT ")
            If strKikan = "���" OrElse strKikan = "�l����(4,5,6��)" Then
                .AppendLine("ISNULL(SUM([4gatu_jisseki_kensuu]),0)+ISNULL(SUM([5gatu_jisseki_kensuu]),0)+ISNULL(SUM([6gatu_jisseki_kensuu]),0)	 ") '���ь���
            End If
            If strKikan = "���" Then
                .AppendLine("+")
            End If
            If strKikan = "���" OrElse strKikan = "�l����(7,8,9��)" Then
                .AppendLine("ISNULL(SUM([7gatu_jisseki_kensuu]),0)+ISNULL(SUM([8gatu_jisseki_kensuu]),0)+ISNULL(SUM([9gatu_jisseki_kensuu]),0)	 ") '���ь���
            End If
            If strKikan = "����" OrElse strKikan = "�l����(10,11,12��)" Then
                .AppendLine("ISNULL(SUM([10gatu_jisseki_kensuu]),0)+ISNULL(SUM([11gatu_jisseki_kensuu]),0)+ISNULL(SUM([12gatu_jisseki_kensuu]),0)	 ") '���ь���
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" OrElse strKikan = "�l����(1,2,3��)" Then
                .AppendLine("ISNULL(SUM([1gatu_jisseki_kensuu]),0)+ISNULL(SUM([2gatu_jisseki_kensuu]),0)+ISNULL(SUM([3gatu_jisseki_kensuu]),0)	 ") '���ь���
            End If
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")
            .AppendLine("    AND")
            .AppendLine("    MKKS.bunbetu_cd='1' ")
        End With
        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "kikanJittusekiKensuu", paramList.ToArray)

        Return dsReturn.Tables("kikanJittusekiKensuu")
    End Function

    ''' <summary>
    ''' ���Ԏ��ы��z�A�e���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="strKikan">����</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKikanJisseki(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT ")
            If strKikan = "���" OrElse strKikan = "�l����(4,5,6��)" Then
                .AppendLine("ISNULL(SUM([4gatu_jisseki_kingaku]),0)+ISNULL(SUM([5gatu_jisseki_kingaku]),0)+ISNULL(SUM([6gatu_jisseki_kingaku]),0)	 ") '���ы��z
            End If
            If strKikan = "���" Then
                .AppendLine("+")
            End If
            If strKikan = "���" OrElse strKikan = "�l����(7,8,9��)" Then
                .AppendLine("ISNULL(SUM([7gatu_jisseki_kingaku]),0)+ISNULL(SUM([8gatu_jisseki_kingaku]),0)+ISNULL(SUM([9gatu_jisseki_kingaku]),0) ") '���ы��z
            End If
            If strKikan = "����" OrElse strKikan = "�l����(10,11,12��)" Then
                .AppendLine("ISNULL(SUM([10gatu_jisseki_kingaku]),0)+ISNULL(SUM([11gatu_jisseki_kingaku]),0)+ISNULL(SUM([12gatu_jisseki_kingaku]),0)	 ") '���ы��z
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" OrElse strKikan = "�l����(1,2,3��)" Then
                .AppendLine("ISNULL(SUM([1gatu_jisseki_kingaku]),0)+ISNULL(SUM([2gatu_jisseki_kingaku]),0)+ISNULL(SUM([3gatu_jisseki_kingaku]),0)") '���ы��z
            End If
            If strKikan = "���" OrElse strKikan = "�l����(4,5,6��)" Then
                .AppendLine(",ISNULL(SUM([4gatu_jisseki_arari]),0)+ISNULL(SUM([5gatu_jisseki_arari]),0)+ISNULL(SUM([6gatu_jisseki_arari]),0)	 ") '���ёe��
            End If
            If strKikan = "���" Then
                .AppendLine("+")
            End If
            If strKikan = "���" OrElse strKikan = "�l����(7,8,9��)" Then
                If strKikan = "�l����(7,8,9��)" Then
                    .Append(",")
                End If
                .AppendLine("ISNULL(SUM([7gatu_jisseki_arari]),0)+ISNULL(SUM([8gatu_jisseki_arari]),0)+ISNULL(SUM([9gatu_jisseki_arari]),0)  ") '���ёe��
            End If
            If strKikan = "����" OrElse strKikan = "�l����(10,11,12��)" Then
                .AppendLine(",ISNULL(SUM([10gatu_jisseki_arari]),0)+ISNULL(SUM([11gatu_jisseki_arari]),0)+ISNULL(SUM([12gatu_jisseki_arari]),0)  	 ") '���ёe��
            End If
            If strKikan = "����" Then
                .AppendLine("+")
            End If
            If strKikan = "����" OrElse strKikan = "�l����(1,2,3��)" Then
                If strKikan = "�l����(1,2,3��)" Then
                    .Append(",")
                End If
                .AppendLine("ISNULL(SUM([1gatu_jisseki_arari]),0)+ISNULL(SUM([2gatu_jisseki_arari]),0)+ISNULL(SUM([3gatu_jisseki_arari]),0)") '���ёe��
            End If
            .AppendLine("         ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")

        End With
        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "kikanJittuseki", paramList.ToArray)

        Return dsReturn.Tables("kikanJittuseki")
    End Function

    ''' <summary>
    ''' �v�挏���A�v����z�A�v��e���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="intBegin">������</param>
    ''' <param name="intEnd">���܂�</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTukiKeikaku(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("       sum(sub.keikakuKensuuCount)")
            .AppendLine("      ,sum(sub.keikakuKingakuCount)")
            .AppendLine("      ,sum(sub.keikakuArariCount)")
            .AppendLine("      ,sum(rowsCount)")
            .AppendLine("FROM(")
            .AppendLine("SELECT ")
            .AppendLine("    (")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kensuu]),0)") '4���`5���v�挏���̏W�v�l
                Next
            Else
                .AppendLine("+ ISNULL(SUM(TZKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kensuu]),0)")
            End If

            .AppendLine(" ) AS keikakuKensuuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kingaku]),0)") '4���`5���v����z�̏W�v�l
                Next
            Else
                .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kingaku]),0)")
            End If

            .AppendLine(" ) AS keikakuKingakuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_arari]),0)") '4���`5���v��e���̏W�v�l
                Next
            Else
                .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_arari]),0)")

            End If

            .AppendLine(" ) AS keikakuArariCount")
            .AppendLine("  ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("	   t_keikaku_kanri AS TZKK WITH (READCOMMITTED)   ")
            .AppendLine("      INNER JOIN   ")
            .AppendLine("      m_keikaku_kameiten AS MKK  WITH (READCOMMITTED) ")
            .AppendLine("      ON")
            .AppendLine("            TZKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("            AND")
            .AppendLine("            TZKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("      INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("      ON")
            .AppendLine("      TZKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("      AND")
            .AppendLine("      TZKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine("WHERE TZKK.keikaku_nendo=@strKeikakuNendo")
            .AppendLine("      AND EXISTS(")
            .AppendLine("                      SELECT keikaku_nendo")
            .AppendLine("                      FROM  ")
            .AppendLine("                             t_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED) ")
            .AppendLine("                      WHERE")
            .AppendLine("                             SUB_TZKK.keikaku_nendo =TZKK.keikaku_nendo ")
            .AppendLine("                             AND   SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND   SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                      GROUP BY")
            .AppendLine("                             keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd")
            .AppendLine("                      HAVING")
            .AppendLine("                             TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                             AND ")
            .AppendLine("                             CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,TZKK.add_datetime,121) ")
            .AppendLine("                             = MAX")
            .AppendLine("                             (")
            .AppendLine("                              CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)")
            .AppendLine("                             ) ")
            .AppendLine("                ) ")
            .AppendLine("UNION ALL")
            .AppendLine("SELECT ")
            .AppendLine("    (")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kensuu]),0)") '4���`5���v�挏���̏W�v�l
                Next
            Else
                .AppendLine("+ ISNULL(SUM(TFKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kensuu]),0)")
            End If

            .AppendLine(" ) AS keikakuKensuuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kingaku]),0)") '4���`5���v����z�̏W�v�l
                Next
            Else
                .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kingaku]),0)")
            End If

            .AppendLine(" ) AS keikakuKingakuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_arari]),0)") '4���`5���v��e���̏W�v�l
                Next
            Else
                .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_arari]),0)")

            End If
            .AppendLine(" ) AS keikakuArariCount")
            .AppendLine("  ,COUNT(*) AS rowsCount")
            .AppendLine(" FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("  ON")
            .AppendLine("      TFKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("  AND")
            .AppendLine("      TFKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine(" WHERE EXISTS(  ")
            .AppendLine("     SELECT keikaku_nendo,  ")
            .AppendLine("            MAX(add_datetime) AS add_datetime,  ")
            .AppendLine("            busyo_cd,  ")
            .AppendLine("            keikaku_kanri_syouhin_cd  ")
            .AppendLine("     FROM t_fc_keikaku_kanri AS SUB_TFKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE keikaku_nendo = @strKeikakuNendo   ")
            .AppendLine("     GROUP BY keikaku_nendo,  ")
            .AppendLine("              busyo_cd,  ")
            .AppendLine("              keikaku_kanri_syouhin_cd  ")
            .AppendLine("     HAVING TFKK.keikaku_nendo = SUB_TFKK.keikaku_nendo  ")
            .AppendLine("     AND TFKK.busyo_cd = SUB_TFKK.busyo_cd  ")
            .AppendLine("     AND TFKK.keikaku_kanri_syouhin_cd = SUB_TFKK.keikaku_kanri_syouhin_cd  ")
            .AppendLine("     AND CASE WHEN ISNULL(CONVERT(VARCHAR,TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END  ")
            .AppendLine("         + CONVERT(VARCHAR,TFKK.add_datetime,121)  ")
            .AppendLine("         = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("         + CONVERT(VARCHAR,SUB_TFKK.add_datetime,121))  ")
            .AppendLine("     )  ")
            .AppendLine(" AND TFKK.keikaku_nendo = @strKeikakuNendo ")
            .AppendLine(" GROUP BY TFKK.keikaku_kanri_syouhin_cd ) sub  ")
        End With
        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "tukiKeikaku", paramList.ToArray)

        Return dsReturn.Tables("tukiKeikaku")
    End Function

    ''' <summary>
    ''' ���ь����̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="intBegin">������</param>
    ''' <param name="intEnd">���܂�</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTukiJissekiKensuu(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    (")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kensuu]),0)") '���ь����̏W�v�l
                Next
            Else
                .AppendLine(" + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kensuu]),0)") '���ь����̏W�v�l
            End If
           
            .AppendLine(" ) AS jissekikensuuCount")

            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")
            .AppendLine("    AND")
            .AppendLine("    MKKS.bunbetu_cd='1' ")
        End With

        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '�v��_�N�x

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "tukiJissekiKensuu", paramList.ToArray)

        Return dsReturn.Tables("tukiJissekiKensuu")
    End Function

    ''' <summary>
    ''' ���ы��z�A���ёe���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="intBegin">������</param>
    ''' <param name="intEnd">���܂�</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTukiJisseki(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    (")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kingaku]),0)") '���ы��z�̏W�v�l
                Next
            Else
                .AppendLine("+ ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kingaku]),0)")
            End If
            .AppendLine(" ) AS jissekiKensuuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_arari]),0)") '���ёe���̏W�v�l
                Next
            Else
                .AppendLine("+ ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_arari]),0)")
            End If
            
            .AppendLine(" ) AS keikakuArariCount")
            .AppendLine("         ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")

        End With

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        '�o�����^
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))   '�v��_�N�x

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "tukiJissekiTwo", paramList.ToArray)

        Return dsReturn.Tables("tukiJissekiTwo")
    End Function

End Class
