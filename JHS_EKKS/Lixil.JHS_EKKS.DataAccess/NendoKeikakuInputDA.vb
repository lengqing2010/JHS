Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �N�x�v��l�ݒ�
''' </summary>
''' <remarks>�N�x�v��l�ݒ�</remarks>
''' <history>
''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
''' </history>
Public Class NendoKeikakuInputDA

    ''' <summary>
    ''' �S�Ќv��Ǘ��e�[�u������v������擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�S�Ђ̌v����</returns>
    ''' <remarks>�S�Ќv��Ǘ��e�[�u������v������擾����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function SelZensyaKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT ")
            .AppendLine("     keikaku_nendo, ")                 '�v��N�x
            .AppendLine("     add_datetime, ")                  '�o�^����
            .AppendLine("     keikaku_kensuu, ")                '�v�撲������
            .AppendLine("     keikaku_uri_kingaku, ")           '�v�攄����z
            .AppendLine("     keikaku_arari, ")                 '�v��e��
            .AppendLine("     keikaku_settutei_kome, ")         '�v��ݒ莞����
            .AppendLine("     kakutei_flg ")                    '�m��FLG
            .AppendLine(" FROM ")
            .AppendLine("     t_zensya_keikaku_kanri AS TZKK WITH(READCOMMITTED) ")
            .AppendLine(" WHERE EXISTS ")
            .AppendLine(" ( ")
            .AppendLine("     SELECT keikaku_nendo, ")
            .AppendLine("         MAX(add_datetime) ")
            .AppendLine("     FROM t_zensya_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED)  ")
            .AppendLine("     WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine("     GROUP BY keikaku_nendo ")
            .AppendLine("     HAVING TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo ")
            .AppendLine("     AND CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                 + CONVERT(VARCHAR,TZKK.add_datetime,121)  ")
            .AppendLine("         = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                 + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)) ")
            .AppendLine(" ) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '�v��N�x(YYYY)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' �v��Ǘ�_�����XϽ��̃f�[�^���擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�v��Ǘ�_�����XϽ��̃f�[�^</returns>
    ''' <remarks>�v��Ǘ�_�����XϽ�����x�X�����擾����</remarks>
    Public Function SelKeikakuKameitenData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT ")
            .AppendLine("     kameiten_cd, ")
            .AppendLine("     eigyou_kbn, ")
            .AppendLine("     kameiten_mei, ")
            .AppendLine("     shiten_mei, ")
            .AppendLine("     busyo_cd ")
            .AppendLine(" FROM m_keikaku_kameiten ")
            .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strKeikakuNendo))     '�v��N�x(YYYY)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' �x�X�ʌv��Ǘ��e�[�u������e�x�X�̌v������擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�e�x�X�̌v����</returns>
    ''' <remarks>�x�X�ʌv��Ǘ��e�[�u������e�x�X�̌v������擾����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function SelSitenbetuKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT TSKK.busyo_cd, ")                       '�����R�[�h
            .AppendLine("     TSKK.busyo_mei, ")                         '������(�x�X��)
            .AppendLine("     TSKK.add_datetime, ")                     '�o�^����
            .AppendLine("     TSKK.kakutei_flg, ")                      '�m��FLG
            .AppendLine("     TSKK.eigyou_keikaku_kensuu, ")            '�c��_�v�撲������
            .AppendLine("     TSKK.tokuhan_keikaku_kensuu, ")           '����_�v�撲������
            .AppendLine("     TSKK.FC_keikaku_kensuu, ")                'FC_�v�撲������
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_kensuu,0) AS Sum_keikaku_kensuu, ")           '���v��������

            .AppendLine("     TSKK.eigyou_keikaku_uri_kingaku, ")       '�c��_�v�攄����z
            .AppendLine("     TSKK.tokuhan_keikaku_uri_kingaku, ")      '����_�v�攄����z
            .AppendLine("     TSKK.FC_keikaku_uri_kingaku, ")           'FC_�v�攄����z
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_uri_kingaku,0) AS Sum_keikaku_uri_kingaku, ") '���v������z

            .AppendLine("     TSKK.eigyou_keikaku_arari, ")             '�c��_�v��e��
            .AppendLine("     TSKK.tokuhan_keikaku_arari, ")            '����_�v��e��
            .AppendLine("     TSKK.FC_keikaku_arari, ")                 'FC_�v��e��
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_arari,0) AS Sum_keikaku_arari, ")              '���v�e��

            .AppendLine("     TJK.eigyou_jittuseki_kensuu, ")           '�ߋ����ь���(�c��)
            .AppendLine("     TJK.tokuhan_jittuseki_kensuu, ")          '�ߋ����ь���(����)
            .AppendLine("     TJK.FC_jittuseki_kensuu, ")               '�ߋ����ь���(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_kensuu,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_kensuu,0)  ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_kensuu,0)) AS Sum_jittuseki_kensuu, ")    '���v�ߋ����ь���

            .AppendLine("     TJK.eigyou_jittuseki_kingaku, ")          '�ߋ����ы��z(�c��)
            .AppendLine("     TJK.tokuhan_jittuseki_kingaku, ")         '�ߋ����ы��z(����)
            .AppendLine("     TJK.FC_jittuseki_kingaku, ")              '�ߋ����ы��z(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_kingaku,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_kingaku,0) ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_kingaku,0)) AS Sum_jittuseki_kingaku, ")  '���v�ߋ����ы��z
            .AppendLine("      ")
            .AppendLine("     TJK.eigyou_jittuseki_arari, ")            '�ߋ����ёe��(�c��)
            .AppendLine("     TJK.tokuhan_jittuseki_arari, ")           '�ߋ����ёe��(����)
            .AppendLine("     TJK.FC_jittuseki_arari, ")                '�ߋ����ёe��(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_arari,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_arari,0) ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_arari,0)) AS Sum_jittuseki_arari ")       '���v�ߋ����ёe��
            .AppendLine(" FROM ( ")
            .AppendLine("     SELECT ")
            .AppendLine("         siten_mei AS busyo_mei, ")
            .AppendLine("         busyo_cd, ")
            .AppendLine("         add_datetime, ")
            .AppendLine("         kakutei_flg, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_kensuu),'') AS eigyou_keikaku_kensuu, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_kensuu),'') AS tokuhan_keikaku_kensuu, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_kensuu),'') AS FC_keikaku_kensuu, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_kensuu,0) + ISNULL(tokuhan_keikaku_kensuu,0) + ISNULL(FC_keikaku_kensuu,0)) AS Sum_keikaku_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_uri_kingaku),'') AS eigyou_keikaku_uri_kingaku, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_uri_kingaku),'') AS tokuhan_keikaku_uri_kingaku, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_uri_kingaku),'') AS FC_keikaku_uri_kingaku, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_uri_kingaku,0) + ISNULL(tokuhan_keikaku_uri_kingaku,0) + ISNULL(FC_keikaku_uri_kingaku,0)) AS Sum_keikaku_uri_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_arari),'') AS eigyou_keikaku_arari, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_arari),'') AS tokuhan_keikaku_arari, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_arari),'') AS FC_keikaku_arari, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_arari,0) + ISNULL(tokuhan_keikaku_arari,0) + ISNULL(FC_keikaku_arari,0)) AS Sum_keikaku_arari ")
            .AppendLine("     FROM ")
            .AppendLine("         t_sitenbetu_keikaku_kanri AS TKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE EXISTS ")
            .AppendLine("     ( ")
            .AppendLine("         SELECT busyo_cd, ")
            .AppendLine("                 MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,add_datetime,121)) ")
            .AppendLine("         FROM  ")
            .AppendLine("             t_sitenbetu_keikaku_kanri AS SUBTKK WITH(READCOMMITTED) ")
            .AppendLine("         WHERE ")
            .AppendLine("             DATENAME(YYYY, keikaku_nendo) = @keikaku_nendo ")
            .AppendLine("         GROUP BY busyo_cd ")
            .AppendLine("         HAVING TKK.busyo_cd = SUBTKK.busyo_cd ")
            .AppendLine("         AND CASE WHEN ISNULL(CONVERT(VARCHAR,TKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,TKK.add_datetime,121)  ")
            .AppendLine("             = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUBTKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,SUBTKK.add_datetime,121)) ")
            .AppendLine("     ) ")
            .AppendLine("     AND DATENAME(YYYY, TKK.keikaku_nendo) = @keikaku_nendo ")
            .AppendLine(" ) AS TSKK ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("     SELECT busyo_cd, ")
            .AppendLine("            SUM(eigyou_jittuseki_kensuu) AS eigyou_jittuseki_kensuu, ")
            .AppendLine("            SUM(tokuhan_jittuseki_kensuu) AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("            SUM(FC_jittuseki_kensuu) AS FC_jittuseki_kensuu, ")
            .AppendLine("            SUM(eigyou_jittuseki_kingaku) AS eigyou_jittuseki_kingaku, ")
            .AppendLine("            SUM(tokuhan_jittuseki_kingaku) AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("            SUM(FC_jittuseki_kingaku) AS FC_jittuseki_kingaku, ")
            .AppendLine("            SUM(eigyou_jittuseki_arari) AS eigyou_jittuseki_arari, ")
            .AppendLine("            SUM(tokuhan_jittuseki_arari) AS tokuhan_jittuseki_arari, ")
            .AppendLine("            SUM(FC_jittuseki_arari) AS FC_jittuseki_arari ")
            .AppendLine("     FROM ( ")
            '���уf�[�^���擾����
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS eigyou_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS eigyou_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS eigyou_jittuseki_arari, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("         0 AS tokuhan_jittuseki_arari, ")
            .AppendLine("         0 AS FC_jittuseki_kensuu, ")
            .AppendLine("         0 AS FC_jittuseki_kingaku, ")
            .AppendLine("         0 AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn1 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     UNION ALL ")
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         0 AS eigyou_jittuseki_kensuu, ")
            .AppendLine("         0 AS eigyou_jittuseki_kingaku, ")
            .AppendLine("         0 AS eigyou_jittuseki_arari, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS tokuhan_jittuseki_arari, ")
            .AppendLine("         0 AS FC_jittuseki_kensuu, ")
            .AppendLine("         0 AS FC_jittuseki_kingaku, ")
            .AppendLine("         0 AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn2 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     UNION ALL ")
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         0 AS eigyou_jittuseki_kensuu, ")
            .AppendLine("         0 AS eigyou_jittuseki_kingaku, ")
            .AppendLine("         0 AS eigyou_jittuseki_arari, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("         0 AS tokuhan_jittuseki_arari, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS FC_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS FC_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn3 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     ) AS TJK   ")
            .AppendLine("     GROUP BY busyo_cd  ")
            .AppendLine(" ) AS TJK ")
            .AppendLine(" ON TSKK.busyo_cd = TJK.busyo_cd ")
            .AppendLine(" ORDER BY ")
            .AppendLine("     TSKK.busyo_cd ASC ")

        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '�v��N�x(YYYY)
        paramList.Add(MakeParam("@keikaku_nendo2", SqlDbType.Char, 4, Convert.ToInt32(strKeikakuNendo) - 1))     '�O�N�N�x(YYYY)
        paramList.Add(MakeParam("@eigyou_kbn1", SqlDbType.VarChar, 1, "1"))     '�c�Ƌ敪(�c��)
        paramList.Add(MakeParam("@eigyou_kbn2", SqlDbType.VarChar, 1, "3"))     '�c�Ƌ敪(����)
        paramList.Add(MakeParam("@eigyou_kbn3", SqlDbType.VarChar, 1, "4"))     '�c�Ƌ敪(FC)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' �����Ǘ��}�X�^����e�x�X�̌v������擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�e�x�X�̌v����</returns>
    ''' <remarks>�����Ǘ��}�X�^����e�x�X�̌v������擾����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function SelBusyoKanriKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT MBK.busyo_cd, ")                       '�����R�[�h
            .AppendLine("     MBK.busyo_mei, ")                         '������(�x�X��)
            .AppendLine("     TSKK.add_datetime, ")                     '�o�^����
            .AppendLine("     TSKK.kakutei_flg, ")                      '�m��FLG
            .AppendLine("     TSKK.eigyou_keikaku_kensuu, ")            '�c��_�v�撲������
            .AppendLine("     TSKK.tokuhan_keikaku_kensuu, ")           '����_�v�撲������
            .AppendLine("     TSKK.FC_keikaku_kensuu, ")                'FC_�v�撲������
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_kensuu,0) AS Sum_keikaku_kensuu, ")           '���v��������

            .AppendLine("     TSKK.eigyou_keikaku_uri_kingaku, ")       '�c��_�v�攄����z
            .AppendLine("     TSKK.tokuhan_keikaku_uri_kingaku, ")      '����_�v�攄����z
            .AppendLine("     TSKK.FC_keikaku_uri_kingaku, ")           'FC_�v�攄����z
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_uri_kingaku,0) AS Sum_keikaku_uri_kingaku, ") '���v������z

            .AppendLine("     TSKK.eigyou_keikaku_arari, ")             '�c��_�v��e��
            .AppendLine("     TSKK.tokuhan_keikaku_arari, ")            '����_�v��e��
            .AppendLine("     TSKK.FC_keikaku_arari, ")                 'FC_�v��e��
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_arari,0) AS Sum_keikaku_arari, ")              '���v�e��

            .AppendLine("     TJK.eigyou_jittuseki_kensuu, ")           '�ߋ����ь���(�c��)
            .AppendLine("     TJK.tokuhan_jittuseki_kensuu, ")          '�ߋ����ь���(����)
            .AppendLine("     TJK.FC_jittuseki_kensuu, ")               '�ߋ����ь���(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_kensuu,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_kensuu,0)  ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_kensuu,0)) AS Sum_jittuseki_kensuu, ")    '���v�ߋ����ь���

            .AppendLine("     TJK.eigyou_jittuseki_kingaku, ")          '�ߋ����ы��z(�c��)
            .AppendLine("     TJK.tokuhan_jittuseki_kingaku, ")         '�ߋ����ы��z(����)
            .AppendLine("     TJK.FC_jittuseki_kingaku, ")              '�ߋ����ы��z(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_kingaku,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_kingaku,0) ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_kingaku,0)) AS Sum_jittuseki_kingaku, ")  '���v�ߋ����ы��z
            .AppendLine("      ")
            .AppendLine("     TJK.eigyou_jittuseki_arari, ")            '�ߋ����ёe��(�c��)
            .AppendLine("     TJK.tokuhan_jittuseki_arari, ")           '�ߋ����ёe��(����)
            .AppendLine("     TJK.FC_jittuseki_arari, ")                '�ߋ����ёe��(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_arari,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_arari,0) ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_arari,0)) AS Sum_jittuseki_arari ")       '���v�ߋ����ёe��
            .AppendLine(" FROM m_busyo_kanri AS MBK WITH(READCOMMITTED) ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("     SELECT ")
            .AppendLine("         busyo_cd, ")
            .AppendLine("         add_datetime, ")
            .AppendLine("         kakutei_flg, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_kensuu),'') AS eigyou_keikaku_kensuu, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_kensuu),'') AS tokuhan_keikaku_kensuu, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_kensuu),'') AS FC_keikaku_kensuu, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_kensuu,0) + ISNULL(tokuhan_keikaku_kensuu,0) + ISNULL(FC_keikaku_kensuu,0)) AS Sum_keikaku_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_uri_kingaku),'') AS eigyou_keikaku_uri_kingaku, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_uri_kingaku),'') AS tokuhan_keikaku_uri_kingaku, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_uri_kingaku),'') AS FC_keikaku_uri_kingaku, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_uri_kingaku,0) + ISNULL(tokuhan_keikaku_uri_kingaku,0) + ISNULL(FC_keikaku_uri_kingaku,0)) AS Sum_keikaku_uri_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_arari),'') AS eigyou_keikaku_arari, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_arari),'') AS tokuhan_keikaku_arari, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_arari),'') AS FC_keikaku_arari, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_arari,0) + ISNULL(tokuhan_keikaku_arari,0) + ISNULL(FC_keikaku_arari,0)) AS Sum_keikaku_arari ")
            .AppendLine("     FROM ")
            .AppendLine("         t_sitenbetu_keikaku_kanri AS TKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE EXISTS ")
            .AppendLine("     ( ")
            .AppendLine("         SELECT busyo_cd, ")
            .AppendLine("                 MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,add_datetime,121)) ")
            .AppendLine("         FROM  ")
            .AppendLine("             t_sitenbetu_keikaku_kanri AS SUBTKK WITH(READCOMMITTED) ")
            .AppendLine("         WHERE ")
            .AppendLine("             DATENAME(YYYY, keikaku_nendo) = @keikaku_nendo ")
            .AppendLine("         GROUP BY busyo_cd ")
            .AppendLine("         HAVING TKK.busyo_cd = SUBTKK.busyo_cd ")
            .AppendLine("         AND CASE WHEN ISNULL(CONVERT(VARCHAR,TKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,TKK.add_datetime,121)  ")
            .AppendLine("             = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUBTKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,SUBTKK.add_datetime,121)) ")
            .AppendLine("     ) ")
            .AppendLine("     AND DATENAME(YYYY, TKK.keikaku_nendo) = @keikaku_nendo ")
            .AppendLine(" ) AS TSKK ")
            .AppendLine(" ON MBK.busyo_cd = TSKK.busyo_cd ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("     SELECT busyo_cd, ")
            .AppendLine("            SUM(eigyou_jittuseki_kensuu) AS eigyou_jittuseki_kensuu, ")
            .AppendLine("            SUM(tokuhan_jittuseki_kensuu) AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("            SUM(FC_jittuseki_kensuu) AS FC_jittuseki_kensuu, ")
            .AppendLine("            SUM(eigyou_jittuseki_kingaku) AS eigyou_jittuseki_kingaku, ")
            .AppendLine("            SUM(tokuhan_jittuseki_kingaku) AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("            SUM(FC_jittuseki_kingaku) AS FC_jittuseki_kingaku, ")
            .AppendLine("            SUM(eigyou_jittuseki_arari) AS eigyou_jittuseki_arari, ")
            .AppendLine("            SUM(tokuhan_jittuseki_arari) AS tokuhan_jittuseki_arari, ")
            .AppendLine("            SUM(FC_jittuseki_arari) AS FC_jittuseki_arari ")
            .AppendLine("     FROM ( ")
            '���уf�[�^���擾����
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS eigyou_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS eigyou_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS eigyou_jittuseki_arari, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("         0 AS tokuhan_jittuseki_arari, ")
            .AppendLine("         0 AS FC_jittuseki_kensuu, ")
            .AppendLine("         0 AS FC_jittuseki_kingaku, ")
            .AppendLine("         0 AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn1 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     UNION ALL ")
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         0 AS eigyou_jittuseki_kensuu, ")
            .AppendLine("         0 AS eigyou_jittuseki_kingaku, ")
            .AppendLine("         0 AS eigyou_jittuseki_arari, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS tokuhan_jittuseki_arari, ")
            .AppendLine("         0 AS FC_jittuseki_kensuu, ")
            .AppendLine("         0 AS FC_jittuseki_kingaku, ")
            .AppendLine("         0 AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn2 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     UNION ALL ")
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         0 AS eigyou_jittuseki_kensuu, ")
            .AppendLine("         0 AS eigyou_jittuseki_kingaku, ")
            .AppendLine("         0 AS eigyou_jittuseki_arari, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("         0 AS tokuhan_jittuseki_arari, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS FC_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS FC_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn3 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     ) AS TJK   ")
            .AppendLine("     GROUP BY busyo_cd  ")
            .AppendLine(" ) AS TJK ")
            .AppendLine(" ON MBK.busyo_cd = TJK.busyo_cd ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine(" m_keikakuyou_kakutyou_meisyou AS MKKM ")
            .AppendLine(" ON ")
            .AppendLine("   MKKM.meisyou_syubetu = '1' ")
            .AppendLine("   AND ")
            .AppendLine("   MBK.busyo_cd = MKKM.code")
            .AppendLine(" WHERE MBK.sosiki_level = @sosiki_level ")
            .AppendLine(" AND MBK.torikesi = @torikesi ")
            .AppendLine(" ORDER BY ")
            .AppendLine("     MKKM.hyouji_jyun ASC ")

        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '�v��N�x(YYYY)
        paramList.Add(MakeParam("@keikaku_nendo2", SqlDbType.Char, 4, Convert.ToInt32(strKeikakuNendo) - 1))     '�O�N�N�x(YYYY)
        paramList.Add(MakeParam("@eigyou_kbn1", SqlDbType.VarChar, 1, "1"))     '�c�Ƌ敪(�c��)
        paramList.Add(MakeParam("@eigyou_kbn2", SqlDbType.VarChar, 1, "3"))     '�c�Ƌ敪(����)
        paramList.Add(MakeParam("@eigyou_kbn3", SqlDbType.VarChar, 1, "4"))     '�c�Ƌ敪(FC)
        paramList.Add(MakeParam("@sosiki_level", SqlDbType.Int, 1, 4))          '�g�D���x��
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, 0))              '����t���O(0�F�L���A0�ȊO�F���)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' �S�Ђ̍ő�o�^�����̃f�[�^���擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�S�Ђ̍ő�o�^�����̃f�[�^</returns>
    ''' <remarks>�S�Ђ̍ő�o�^�����̃f�[�^���擾����</remarks>
    Public Function SelMaxZensyaKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT MAX(add_datetime) AS add_datetime ")
            .AppendLine(" FROM t_zensya_keikaku_kanri WITH(READCOMMITTED) ")
            .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine(" AND kakutei_flg = @kakutei_flg ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '�v��N�x(YYYY)
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Int, 1, 1))     '�m��FLG

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)

    End Function

    ''' <summary>
    ''' �x�X�̍ő�o�^�����̃f�[�^���擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�x�X�̍ő�o�^�����̃f�[�^</returns>
    ''' <remarks>�x�X�̍ő�o�^�����̃f�[�^���擾����</remarks>
    Public Function SelMaxSitenbetuKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT MAX(add_datetime) AS add_datetime ")
            .AppendLine(" FROM t_sitenbetu_keikaku_kanri WITH(READCOMMITTED) ")
            .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine(" AND kakutei_flg = @kakutei_flg ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '�v��N�x(YYYY)
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Int, 1, 1))     '�m��FLG

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)

    End Function

    ''' <summary>
    ''' �S�Ќv��Ǘ��e�[�u���ɓo�^����
    ''' </summary>
    ''' <param name="hstValues">�o�^�f�[�^</param>
    ''' <remarks>�S�Ќv��Ǘ��e�[�u���ɓo�^����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Sub InsZensyaKeikakuKanriData(ByVal hstValues As Hashtable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, hstValues)

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_zensya_keikaku_kanri WITH(UPDLOCK) ( ")
            .AppendLine("     keikaku_nendo, ")
            .AppendLine("     add_datetime, ")
            .AppendLine("     keikaku_kensuu, ")
            .AppendLine("     keikaku_uri_kingaku, ")
            .AppendLine("     keikaku_arari, ")
            .AppendLine("     keikaku_henkou_flg, ")
            .AppendLine("     keikaku_settutei_kome, ")
            .AppendLine("     kakutei_flg, ")
            .AppendLine("     keikaku_huhen_flg, ")
            .AppendLine("     add_login_user_id ")
            .AppendLine(" )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     GETDATE(), ")
            .AppendLine("     @keikaku_kensuu, ")
            .AppendLine("     @keikaku_uri_kingaku, ")
            .AppendLine("     @keikaku_arari, ")
            .AppendLine("     @keikaku_henkou_flg, ")
            .AppendLine("     @keikaku_settutei_kome, ")
            .AppendLine("     @kakutei_flg, ")
            .AppendLine("     @keikaku_huhen_flg, ")
            .AppendLine("     @add_login_user_id ")
            .AppendLine(" ) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, hstValues("keikaku_nendo")))                '�v��N�x(YYYY)

        If Convert.ToString(hstValues("keikaku_kensuu")).Equals(String.Empty) Then
            paramList.Add(MakeParam("@keikaku_kensuu", SqlDbType.BigInt, 12, DBNull.Value))                         '�v�撲������
        Else
            paramList.Add(MakeParam("@keikaku_kensuu", SqlDbType.BigInt, 12, hstValues("keikaku_kensuu")))          '�v�撲������
        End If

        If Convert.ToString(hstValues("keikaku_uri_kingaku")).Equals(String.Empty) Then
            paramList.Add(MakeParam("@keikaku_uri_kingaku", SqlDbType.BigInt, 12, DBNull.Value))                    '�v�攄����z
        Else
            paramList.Add(MakeParam("@keikaku_uri_kingaku", SqlDbType.BigInt, 12, hstValues("keikaku_uri_kingaku"))) '�v�攄����z
        End If

        If Convert.ToString(hstValues("keikaku_arari")).Equals(String.Empty) Then
            paramList.Add(MakeParam("@keikaku_arari", SqlDbType.BigInt, 12, DBNull.Value))                          '�v��e��
        Else
            paramList.Add(MakeParam("@keikaku_arari", SqlDbType.BigInt, 12, hstValues("keikaku_arari")))            '�v��e��
        End If

        paramList.Add(MakeParam("@keikaku_henkou_flg", SqlDbType.Int, 1, hstValues("keikaku_henkou_flg")))          '�v��ύXFLG
        paramList.Add(MakeParam("@keikaku_settutei_kome", SqlDbType.VarChar, 80, hstValues("keikaku_settutei_kome"))) '�v��ݒ莞����
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Int, 1, hstValues("kakutei_flg")))                        '�m��FLG
        paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, hstValues("keikaku_huhen_flg")))            '�v��l�s��FLG
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, hstValues("add_login_user_id")))       '�o�^��ID

        SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

    End Sub

    ''' <summary>
    ''' �x�X�ʌv��Ǘ��e�[�u���ɓo�^����
    ''' </summary>
    ''' <param name="drValue">�o�^�f�[�^</param>
    ''' <remarks>�x�X�ʌv��Ǘ��e�[�u���ɓo�^����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Sub InsSitenbetuKeikakuKanriData(ByVal drValue As DataRow)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue)

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_sitenbetu_keikaku_kanri WITH(UPDLOCK) ( ")
            .AppendLine("     keikaku_nendo, ")
            .AppendLine("     siten_mei, ")
            .AppendLine("     add_datetime, ")
            .AppendLine("     busyo_cd, ")
            .AppendLine("     eigyou_keikaku_kensuu, ")
            .AppendLine("     tokuhan_keikaku_kensuu, ")
            .AppendLine("     FC_keikaku_kensuu, ")
            .AppendLine("     eigyou_keikaku_uri_kingaku, ")
            .AppendLine("     tokuhan_keikaku_uri_kingaku, ")
            .AppendLine("     FC_keikaku_uri_kingaku, ")
            .AppendLine("     eigyou_keikaku_arari, ")
            .AppendLine("     tokuhan_keikaku_arari, ")
            .AppendLine("     FC_keikaku_arari, ")
            .AppendLine("     keikaku_henkou_flg, ")
            .AppendLine("     keikaku_settutei_kome, ")
            .AppendLine("     kakutei_flg, ")
            .AppendLine("     keikaku_huhen_flg, ")
            .AppendLine("     add_login_user_id ")
            .AppendLine(" )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     @siten_mei, ")
            .AppendLine("     @add_datetime, ")
            .AppendLine("     @busyo_cd, ")
            .AppendLine("     @eigyou_keikaku_kensuu, ")
            .AppendLine("     @tokuhan_keikaku_kensuu, ")
            .AppendLine("     @FC_keikaku_kensuu, ")
            .AppendLine("     @eigyou_keikaku_uri_kingaku, ")
            .AppendLine("     @tokuhan_keikaku_uri_kingaku, ")
            .AppendLine("     @FC_keikaku_uri_kingaku, ")
            .AppendLine("     @eigyou_keikaku_arari, ")
            .AppendLine("     @tokuhan_keikaku_arari, ")
            .AppendLine("     @FC_keikaku_arari, ")
            .AppendLine("     @keikaku_henkou_flg, ")
            .AppendLine("     @keikaku_settutei_kome, ")
            .AppendLine("     @kakutei_flg, ")
            .AppendLine("     @keikaku_huhen_flg, ")
            .AppendLine("     @add_login_user_id ")
            .AppendLine(" ) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                                 '�v��N�x(YYYY)
        paramList.Add(MakeParam("@siten_mei", SqlDbType.VarChar, 40, drValue("siten_mei")))                                     '�x�X��
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 20, drValue("add_datetime")))                              '�o�^����
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, drValue("busyo_cd")))                                        '�����R�[�h
        paramList.Add(MakeParam("@eigyou_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("eigyou_keikaku_kensuu")))              '�c��_�v�撲������
        paramList.Add(MakeParam("@tokuhan_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("tokuhan_keikaku_kensuu")))            '����_�v�撲������
        paramList.Add(MakeParam("@FC_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("FC_keikaku_kensuu")))                      'FC_�v�撲������
        paramList.Add(MakeParam("@eigyou_keikaku_uri_kingaku", SqlDbType.BigInt, 12, drValue("eigyou_keikaku_uri_kingaku")))    '�c��_�v�攄����z
        paramList.Add(MakeParam("@tokuhan_keikaku_uri_kingaku", SqlDbType.BigInt, 12, drValue("tokuhan_keikaku_uri_kingaku")))  '����_�v�攄����z
        paramList.Add(MakeParam("@FC_keikaku_uri_kingaku", SqlDbType.BigInt, 12, drValue("FC_keikaku_uri_kingaku")))            'FC_�v�攄����z
        paramList.Add(MakeParam("@eigyou_keikaku_arari", SqlDbType.BigInt, 12, drValue("eigyou_keikaku_arari")))                '�c��_�v��e��
        paramList.Add(MakeParam("@tokuhan_keikaku_arari", SqlDbType.BigInt, 12, drValue("tokuhan_keikaku_arari")))              '����_�v��e��
        paramList.Add(MakeParam("@FC_keikaku_arari", SqlDbType.BigInt, 12, drValue("FC_keikaku_arari")))                        'FC_�v��e��
        paramList.Add(MakeParam("@keikaku_henkou_flg", SqlDbType.Int, 1, drValue("keikaku_henkou_flg")))                        '�v��ύXFLG
        paramList.Add(MakeParam("@keikaku_settutei_kome", SqlDbType.VarChar, 80, DBNull.Value))                                 '�v��ݒ莞����
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Int, 1, drValue("kakutei_flg")))                                      '�m��FLG
        paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, drValue("keikaku_huhen_flg")))                          '�v��l�s��FLG
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, drValue("add_login_user_id")))                     '�o�^��ID

        SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

    End Sub
End Class
