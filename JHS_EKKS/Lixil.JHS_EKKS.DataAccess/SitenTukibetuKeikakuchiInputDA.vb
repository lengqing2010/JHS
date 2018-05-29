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
''' �x�X ���ʌv��l �b�r�u�捞DA
''' </summary>
''' <remarks></remarks>
''' <history>
''' <para>2012/11/23 P-44979 ���� �V�K�쐬 </para>
''' </history>
Public Class SitenTukibetuKeikakuchiInputDA

    ''' <summary>
    ''' ��ʈꗗ�f�[�^�擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <returns>�A�b�v���[�h�Ǘ��e�[�u���̃f�[�^</returns>
    ''' <remarks>�A�b�v���[�h�Ǘ��e�[�u���̃f�[�^���擾����</remarks>
    ''' <history>
    ''' <para>2012/11/23 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function SelTUploadKanri(ByVal strKbn As String) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKbn)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        With sqlBuffer
            .AppendLine("SELECT")
            If strKbn = "1" Then
                .AppendLine("      TOP 100")
                .AppendLine("      CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime")
                .AppendLine("	 , CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') AS torikomi_datetime") '�捞����
                .AppendLine("    , nyuuryoku_file_mei")
                .AppendLine("    , CASE error_umu ")
                .AppendLine("        WHEN '1' THEN '�L��' ")
                .AppendLine("        WHEN '0' THEN '����' ")
                .AppendLine("        ELSE '' ")
                .AppendLine("        END AS error_umu")
                .AppendLine("    ,edi_jouhou_sakusei_date ")                'EDI���쐬��
            Else
                .AppendLine("    COUNT(syori_datetime)")
            End If
            .AppendLine("FROM")
            .AppendLine("    t_upload_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("    file_kbn = '1'")
            If strKbn = "1" Then
                .AppendLine("ORDER BY")
                .AppendLine("    syori_datetime DESC")
            End If
        End With

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "tUploadKanri")

        Return dsInfo.Tables(0)

    End Function

    ''' <summary>
    ''' �������ޑ��݃`�F�b�N
    ''' </summary>
    ''' <param name="strBusyoCd">��������</param>
    ''' <returns>�������ޑ��ݐ�</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/14 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function SelMBusyoKanri(ByVal strBusyoCd As String) As Integer
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strBusyoCd)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("SELECT")
            .AppendLine("    busyo_cd")                          '��������
            .AppendLine("FROM")
            .AppendLine("    m_busyo_kanri WITH(READCOMMITTED)") '�����Ǘ��}�X�^
            .AppendLine("WHERE")
            .AppendLine("    busyo_cd = @busyo_cd")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd)) '�����R�[�h

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "busyoCdCount", paramList.ToArray)

        Return dsInfo.Tables(0).Rows.Count

    End Function

    ''' <summary>
    ''' �u�o�^CSV�t�@�C���v�̃f�[�^���m�F�ς݃`�F�b�N
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��_�N�x</param>
    ''' <param name="strBusyoCd">��������</param>
    ''' <returns>�f�[�^���ݐ�</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/14 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function SelTSitenbetuTukiKeikakuKanriCount(ByVal strKeikakuNendo As String, ByVal strBusyoCd As String) As Integer
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strBusyoCd)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("SELECT")
            .AppendLine("    keikaku_nendo")
            .AppendLine("FROM")
            .AppendLine("    t_sitenbetu_tuki_keikaku_kanri WITH(READCOMMITTED)") '�x�X�ʌ��ʌv��Ǘ��e�[�u��
            .AppendLine("WHERE")
            .AppendLine("    keikaku_nendo = @keikaku_nendo")
            .AppendLine("    AND busyo_cd = @busyo_cd")
            .AppendLine("    AND (kakutei_flg = '1'")
            .AppendLine("    OR keikaku_huhen_flg = '1')")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo)) '�v��_�N�x
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd)) '�����R�[�h

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "tSitenbetuTukiKeikakuKanriCount", paramList.ToArray)

        Return dsInfo.Tables(0).Rows.Count

    End Function

    ''' <summary>
    ''' �u�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u���v�ɑ}������B
    ''' </summary>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strGyouNo">�sNo</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <returns>TRUE�F�����AFALSE�F���s</returns>
    ''' <remarks>�u�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u���v�f�[�^�}������</remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function InsTSitenTukibetuTorikomiError(ByVal strSyoriDatetime As String, _
                                                   ByVal strEdiJouhouSakuseiDate As String, _
                                                   ByVal strGyouNo As String, _
                                                   ByVal strKbn As String, _
                                                   ByVal strUserId As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strSyoriDatetime, strEdiJouhouSakuseiDate, strGyouNo, strKbn, strUserId)

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("INSERT INTO")
            .AppendLine("    t_siten_tukibetu_torikomi_error WITH(UPDLOCK)")
            .AppendLine("(")
            .AppendLine("      edi_jouhou_sakusei_date")
            .AppendLine("    , gyou_no")
            .AppendLine("    , syori_datetime")
            .AppendLine("    , error_naiyou")
            .AppendLine("    , add_login_user_id")
            .AppendLine("    , add_datetime")
            .AppendLine("    , upd_login_user_id")
            .AppendLine("    , upd_datetime")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("      @edi_jouhou_sakusei_date")
            .AppendLine("    , @gyou_no")
            .AppendLine("    , @syori_datetime")
            If strKbn = "1" Then
                .AppendLine("    , '�Y���s�̍��ڐ����s���ł��B'")
            End If
            If strKbn = "2" Then
                .AppendLine("    , '�捞�Ώۂ�CSV�ŕK�{����(�v��N�x�A�������ށA�c�Ƌ敪)�Ŗ����͂̂��̂�����܂��B�捞�Ώۂ�CSV���e�����m�F�������B'")
            End If
            If strKbn = "3" Then
                .AppendLine("    , '�捞�����R�[�h�������Ǘ��}�X�^�ɑ��݂��܂���B'")
            End If
            If strKbn = "4" Then
                .AppendLine("    , '�c�Ƌ敪���c�ƁA���́A�e�b�ł͂���܂���B'")
            End If
            If strKbn = "5" Then
                .AppendLine("    , '�c�Ƌ敪���d���ł��B'")
            End If
            If strKbn = "6" Then
                .AppendLine("    , '�捞�Ώۂ�CSV�ɋ֑��������܂܂�Ă���܂��B�捞�Ώۂ�CSV���e�����m�F�������B'")
            End If
            If strKbn = "7" Then
                .AppendLine("    , '�捞�Ώۂ�CSV�ɔ��p�����ȊO���܂܂�Ă���܂��BEXCEL���e�����m�F�̏�ACSV�o�́|�捞�� �ēx���{ �������B'")
            End If
            If strKbn = "8" Then
                .AppendLine("    , '�捞�Ώۂ�CSV�Ō����I�[�o�[�̓��͂��������܂��B�捞�Ώۂ�CSV���e�����m�F�������B'")
            End If
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine(")")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate)) 'EDI���쐬��
        paramList.Add(MakeParam("@gyou_no", SqlDbType.VarChar, 12, strGyouNo)) '�sNO
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 20, strSyoriDatetime)) '��������
        paramList.Add(MakeParam("@user_id", SqlDbType.VarChar, 30, strUserId)) '���[�U�[ID

        '�}�����ꂽ�f�[�^�Z�b�g�� DB �֏�������
        If ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray) > 0 Then
            '�I������
            sqlBuffer = Nothing
            Return True
        Else
            '�I������
            sqlBuffer = Nothing
            Return False
        End If

    End Function

    ''' <summary>
    ''' �u�x�X�ʌ��ʌv��Ǘ��e�[�u���v�ɑ}������B
    ''' </summary>
    ''' <param name="drExcelData">CSV�f�[�^</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <returns>TRUE�F�����AFALSE�F���s</returns>
    ''' <remarks>�u�x�X�ʌ��ʌv��Ǘ��e�[�u���v�f�[�^�}������</remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function InsTSitenbetuTukiKeikakuKanri(ByVal drExcelData As DataRow, ByVal strUserId As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drExcelData, strUserId)

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("INSERT INTO")
            .AppendLine("    t_sitenbetu_tuki_keikaku_kanri WITH(UPDLOCK)") '�x�X�ʌ��ʌv��Ǘ��e�[�u��
            .AppendLine("(")
            .AppendLine("      [keikaku_nendo]")              '�v��_�N�x
            .AppendLine("    , [busyo_cd]")                   '��������
            .AppendLine("    , [add_datetime]")               '�o�^����
            .AppendLine("    , [siten_mei]")                  '�x�X��
            .AppendLine("    , [eigyou_cd]")                  '�c�Ə�
            .AppendLine("    , [eigyou_mei]")                 '�c�Ə���
            .AppendLine("    , [eigyou_kbn]")                 '�c�Ƌ敪
            .AppendLine("    , [4gatu_keikaku_kensuu]")       '4��_�v�挏��
            .AppendLine("    , [4gatu_keikaku_kingaku]")      '4��_�v����z
            .AppendLine("    , [4gatu_keikaku_arari]")        '4��_�v��e��
            .AppendLine("    , [5gatu_keikaku_kensuu]")       '5��_�v�挏��
            .AppendLine("    , [5gatu_keikaku_kingaku]")      '5��_�v����z
            .AppendLine("    , [5gatu_keikaku_arari]")        '5��_�v��e��
            .AppendLine("    , [6gatu_keikaku_kensuu]")       '6��_�v�挏��
            .AppendLine("    , [6gatu_keikaku_kingaku]")      '6��_�v����z
            .AppendLine("    , [6gatu_keikaku_arari]")        '6��_�v��e��
            .AppendLine("    , [7gatu_keikaku_kensuu]")       '7��_�v�挏��
            .AppendLine("    , [7gatu_keikaku_kingaku]")      '7��_�v����z
            .AppendLine("    , [7gatu_keikaku_arari]")        '7��_�v��e��
            .AppendLine("    , [8gatu_keikaku_kensuu]")       '8��_�v�挏��
            .AppendLine("    , [8gatu_keikaku_kingaku]")      '8��_�v����z
            .AppendLine("    , [8gatu_keikaku_arari]")        '8��_�v��e��
            .AppendLine("    , [9gatu_keikaku_kensuu]")       '9��_�v�挏��
            .AppendLine("    , [9gatu_keikaku_kingaku]")      '9��_�v����z
            .AppendLine("    , [9gatu_keikaku_arari]")        '9��_�v��e��
            .AppendLine("    , [10gatu_keikaku_kensuu]")      '10��_�v�挏��
            .AppendLine("    , [10gatu_keikaku_kingaku]")     '10��_�v����z
            .AppendLine("    , [10gatu_keikaku_arari]")       '10��_�v��e��
            .AppendLine("    , [11gatu_keikaku_kensuu]")      '11��_�v�挏��
            .AppendLine("    , [11gatu_keikaku_kingaku]")     '11��_�v����z
            .AppendLine("    , [11gatu_keikaku_arari]")       '11��_�v��e��
            .AppendLine("    , [12gatu_keikaku_kensuu]")      '12��_�v�挏��
            .AppendLine("    , [12gatu_keikaku_kingaku]")     '12��_�v����z
            .AppendLine("    , [12gatu_keikaku_arari]")       '12��_�v��e��
            .AppendLine("    , [1gatu_keikaku_kensuu]")       '1��_�v�挏��
            .AppendLine("    , [1gatu_keikaku_kingaku]")      '1��_�v����z
            .AppendLine("    , [1gatu_keikaku_arari]")        '1��_�v��e��
            .AppendLine("    , [2gatu_keikaku_kensuu]")       '2��_�v�挏��
            .AppendLine("    , [2gatu_keikaku_kingaku]")      '2��_�v����z
            .AppendLine("    , [2gatu_keikaku_arari]")        '2��_�v��e��
            .AppendLine("    , [3gatu_keikaku_kensuu]")       '3��_�v�挏��
            .AppendLine("    , [3gatu_keikaku_kingaku]")      '3��_�v����z
            .AppendLine("    , [3gatu_keikaku_arari]")        '3��_�v��e��
            .AppendLine("    , [keikaku_henkou_flg]")         '�v��ύXFLG
            .AppendLine("    , [keikaku_settutei_kome]")      '�v��ݒ莞����
            .AppendLine("    , [kakutei_flg]")                '�m��FLG
            .AppendLine("    , [keikaku_huhen_flg]")          '�v��l�s��FLG
            .AppendLine("    , [add_login_user_id]")          '�o�^���O�C�����[�U�[ID
            .AppendLine("    , [upd_login_user_id]")          '�X�V���O�C�����[�U�[ID
            .AppendLine("    , [upd_datetime]")               '�X�V����
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("      @keikaku_nendo")
            .AppendLine("    , @busyo_cd")
            .AppendLine("    , GetDate()")
            .AppendLine("    , @siten_mei")
            .AppendLine("    , NULL")
            .AppendLine("    , NULL")
            .AppendLine("    , @eigyou_kbn")
            .AppendLine("    , @4gatu_keikaku_kensuu")
            .AppendLine("    , @4gatu_keikaku_kingaku")
            .AppendLine("    , @4gatu_keikaku_arari")
            .AppendLine("    , @5gatu_keikaku_kensuu")
            .AppendLine("    , @5gatu_keikaku_kingaku")
            .AppendLine("    , @5gatu_keikaku_arari")
            .AppendLine("    , @6gatu_keikaku_kensuu")
            .AppendLine("    , @6gatu_keikaku_kingaku")
            .AppendLine("    , @6gatu_keikaku_arari")
            .AppendLine("    , @7gatu_keikaku_kensuu")
            .AppendLine("    , @7gatu_keikaku_kingaku")
            .AppendLine("    , @7gatu_keikaku_arari")
            .AppendLine("    , @8gatu_keikaku_kensuu")
            .AppendLine("    , @8gatu_keikaku_kingaku")
            .AppendLine("    , @8gatu_keikaku_arari")
            .AppendLine("    , @9gatu_keikaku_kensuu")
            .AppendLine("    , @9gatu_keikaku_kingaku")
            .AppendLine("    , @9gatu_keikaku_arari")
            .AppendLine("    , @10gatu_keikaku_kensuu")
            .AppendLine("    , @10gatu_keikaku_kingaku")
            .AppendLine("    , @10gatu_keikaku_arari")
            .AppendLine("    , @11gatu_keikaku_kensuu")
            .AppendLine("    , @11gatu_keikaku_kingaku")
            .AppendLine("    , @11gatu_keikaku_arari")
            .AppendLine("    , @12gatu_keikaku_kensuu")
            .AppendLine("    , @12gatu_keikaku_kingaku")
            .AppendLine("    , @12gatu_keikaku_arari")
            .AppendLine("    , @1gatu_keikaku_kensuu")
            .AppendLine("    , @1gatu_keikaku_kingaku")
            .AppendLine("    , @1gatu_keikaku_arari")
            .AppendLine("    , @2gatu_keikaku_kensuu")
            .AppendLine("    , @2gatu_keikaku_kingaku")
            .AppendLine("    , @2gatu_keikaku_arari")
            .AppendLine("    , @3gatu_keikaku_kensuu")
            .AppendLine("    , @3gatu_keikaku_kingaku")
            .AppendLine("    , @3gatu_keikaku_arari")
            .AppendLine("    , NULL")
            .AppendLine("    , NULL")
            .AppendLine("    , NULL")
            .AppendLine("    , NULL")
            .AppendLine("    , @user_id")
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine(")")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drExcelData.Item("keikaku_nendo").ToString.Trim)) '�v��_�N�x
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, drExcelData.Item("busyo_cd").ToString.Trim)) '��������
        paramList.Add(MakeParam("@siten_mei", SqlDbType.VarChar, 40, drExcelData.Item("siten_mei").ToString.Trim)) '�x�X��
        paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 5, drExcelData.Item("eigyou_kbn").ToString.Trim)) '�c�Ƌ敪

        paramList.Add(MakeParam("@4gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("4gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("4gatu_keikaku_kensuu").ToString.Trim))) '4��_�v�挏��
        paramList.Add(MakeParam("@4gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("4gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("4gatu_keikaku_kingaku").ToString.Trim))) '4��_�v����z
        paramList.Add(MakeParam("@4gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("4gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("4gatu_keikaku_arari").ToString.Trim))) '4��_�v��e��

        paramList.Add(MakeParam("@5gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("5gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("5gatu_keikaku_kensuu").ToString.Trim))) '5��_�v�挏��
        paramList.Add(MakeParam("@5gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("5gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("5gatu_keikaku_kingaku").ToString.Trim))) '5��_�v����z
        paramList.Add(MakeParam("@5gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("5gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("5gatu_keikaku_arari").ToString.Trim))) '5��_�v��e��

        paramList.Add(MakeParam("@6gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("6gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("6gatu_keikaku_kensuu").ToString.Trim))) '6��_�v�挏��
        paramList.Add(MakeParam("@6gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("6gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("6gatu_keikaku_kingaku").ToString.Trim))) '6��_�v����z
        paramList.Add(MakeParam("@6gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("6gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("6gatu_keikaku_arari").ToString.Trim))) '6��_�v��e��

        paramList.Add(MakeParam("@7gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("7gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("7gatu_keikaku_kensuu").ToString.Trim))) '7��_�v�挏��
        paramList.Add(MakeParam("@7gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("7gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("7gatu_keikaku_kingaku").ToString.Trim))) '7��_�v����z
        paramList.Add(MakeParam("@7gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("7gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("7gatu_keikaku_arari").ToString.Trim))) '7��_�v��e��

        paramList.Add(MakeParam("@8gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("8gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("8gatu_keikaku_kensuu").ToString.Trim))) '8��_�v�挏��
        paramList.Add(MakeParam("@8gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("8gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("8gatu_keikaku_kingaku").ToString.Trim))) '8��_�v����z
        paramList.Add(MakeParam("@8gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("8gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("8gatu_keikaku_arari").ToString.Trim))) '8��_�v��e��

        paramList.Add(MakeParam("@9gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("9gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("9gatu_keikaku_kensuu").ToString.Trim))) '9��_�v�挏��
        paramList.Add(MakeParam("@9gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("9gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("9gatu_keikaku_kingaku").ToString.Trim))) '9��_�v����z
        paramList.Add(MakeParam("@9gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("9gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("9gatu_keikaku_arari").ToString.Trim))) '9��_�v��e��

        paramList.Add(MakeParam("@10gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("10gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("10gatu_keikaku_kensuu").ToString.Trim))) '10��_�v�挏��
        paramList.Add(MakeParam("@10gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("10gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("10gatu_keikaku_kingaku").ToString.Trim))) '10��_�v����z
        paramList.Add(MakeParam("@10gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("10gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("10gatu_keikaku_arari").ToString.Trim))) '10��_�v��e��

        paramList.Add(MakeParam("@11gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("11gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("11gatu_keikaku_kensuu").ToString.Trim))) '11��_�v�挏��
        paramList.Add(MakeParam("@11gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("11gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("11gatu_keikaku_kingaku").ToString.Trim))) '11��_�v����z
        paramList.Add(MakeParam("@11gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("11gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("11gatu_keikaku_arari").ToString.Trim))) '11��_�v��e��

        paramList.Add(MakeParam("@12gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("12gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("12gatu_keikaku_kensuu").ToString.Trim))) '12��_�v�挏��
        paramList.Add(MakeParam("@12gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("12gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("12gatu_keikaku_kingaku").ToString.Trim))) '12��_�v����z
        paramList.Add(MakeParam("@12gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("12gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("12gatu_keikaku_arari").ToString.Trim))) '12��_�v��e��

        paramList.Add(MakeParam("@1gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("1gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("1gatu_keikaku_kensuu").ToString.Trim))) '1��_�v�挏��
        paramList.Add(MakeParam("@1gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("1gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("1gatu_keikaku_kingaku").ToString.Trim))) '1��_�v����z
        paramList.Add(MakeParam("@1gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("1gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("1gatu_keikaku_arari").ToString.Trim))) '1��_�v��e��

        paramList.Add(MakeParam("@2gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("2gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("2gatu_keikaku_kensuu").ToString.Trim))) '2��_�v�挏��
        paramList.Add(MakeParam("@2gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("2gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("2gatu_keikaku_kingaku").ToString.Trim))) '2��_�v����z
        paramList.Add(MakeParam("@2gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("2gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("2gatu_keikaku_arari").ToString.Trim))) '2��_�v��e��

        paramList.Add(MakeParam("@3gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("3gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("3gatu_keikaku_kensuu").ToString.Trim))) '3��_�v�挏��
        paramList.Add(MakeParam("@3gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("3gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("3gatu_keikaku_kingaku").ToString.Trim))) '3��_�v����z
        paramList.Add(MakeParam("@3gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("3gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("3gatu_keikaku_arari").ToString.Trim))) '3��_�v��e��

        paramList.Add(MakeParam("@user_id", SqlDbType.VarChar, 30, strUserId)) '���[�U�[ID

        '�}�����ꂽ�f�[�^�Z�b�g�� DB �֏�������
        If ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray) > 0 Then
            '�I������
            sqlBuffer = Nothing
            Return True
        Else
            '�I������
            sqlBuffer = Nothing
            Return False
        End If

    End Function

    ''' <summary>
    ''' �u�A�b�v���[�h�Ǘ��e�[�u���v�ɑ}������B
    ''' </summary>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strErrorUmu">�G���[�L��</param>
    ''' <param name="strNyuuryokuFileMei">���̓t�@�C����</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <returns>TRUE�F�����AFALSE�F���s</returns>
    ''' <remarks>�u�A�b�v���[�h�Ǘ��e�[�u���v�f�[�^�}������</remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function InsTUploadKanri(ByVal strSyoriDatetime As String, _
                                    ByVal strEdiJouhouSakuseiDate As String, _
                                    ByVal strErrorUmu As String, _
                                    ByVal strNyuuryokuFileMei As String, _
                                    ByVal strUserId As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strSyoriDatetime, strEdiJouhouSakuseiDate, strNyuuryokuFileMei, strUserId)

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("INSERT INTO")
            .AppendLine("    t_upload_kanri WITH(UPDLOCK)")
            .AppendLine("(")
            .AppendLine("      syori_datetime")
            .AppendLine("    , nyuuryoku_file_mei")
            .AppendLine("    , edi_jouhou_sakusei_date")
            .AppendLine("    , error_umu")
            .AppendLine("    , file_kbn")
            .AppendLine("    , add_login_user_id")
            .AppendLine("    , add_datetime")
            .AppendLine("    , upd_login_user_id")
            .AppendLine("    , upd_datetime")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("      @syori_datetime")
            .AppendLine("    , @nyuuryoku_file_mei")
            .AppendLine("    , @edi_jouhou_sakusei_date")
            .AppendLine("    , @error_umu")
            .AppendLine("    , 1")
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine(")")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 20, strSyoriDatetime)) '��������
        paramList.Add(MakeParam("@error_umu", SqlDbType.VarChar, 1, strErrorUmu)) '�G���[�L��
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strNyuuryokuFileMei)) '���̓t�@�C����
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate)) 'EDI���쐬��
        paramList.Add(MakeParam("@user_id", SqlDbType.VarChar, 30, strUserId)) '���[�U�[ID


        '�}�����ꂽ�f�[�^�Z�b�g�� DB �֏�������
        If ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray) > 0 Then
            '�I������
            sqlBuffer = Nothing
            Return True
        Else
            '�I������
            sqlBuffer = Nothing
            Return False
        End If

    End Function

End Class
