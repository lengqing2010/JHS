Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>
''' �������Ϗ��쐬�w��
''' </summary>
''' <remarks></remarks>
''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
Public Class TyousaMitumorisyoSakuseiInquiryDataAccess

    ''' <summary>
    ''' �\���Z�� �I��
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelJyuusyoInfo() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   kanri_no")
            .AppendLine("  ,(convert(varchar,kanri_no) + '�F' + shiten_mei) AS shiten_mei")
            .AppendLine("FROM  ")
            .AppendLine("   m_mitumori_hyoujijyuusyo_kanri WITH(READCOMMITTED)")
            .AppendLine("ORDER BY kanri_no ASC")
        End With

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �u���Ϗ��쐬�񐔁v���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">���Ϗ��쐬��</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelSakuseiKaisuu(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   mit_sakusei_kaisuu")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   kbn = @kbn")
            .AppendLine("   AND hosyousyo_no = @hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ���F�� �I��
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelSyouninSyaInfo() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   syouninsya_id")
            .AppendLine("  ,syouninsya_mei")
            .AppendLine("FROM  ")
            .AppendLine("   m_syouninsya_syouninin_kanri WITH(READCOMMITTED)")
            .AppendLine("ORDER BY hyouji_jyun ASC")
        End With

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ���Ϗ��̑��݂𔻝Ђ���
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelMitumoriCount(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   COUNT(kbn)")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   kbn = @kbn")
            .AppendLine("   AND hosyousyo_no = @hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ���Ϗ��쐬�񐔂��X�V����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <param name="inMitSakuseiKaisuu">���Ϗ��쐬��</param>
    ''' <param name="inSyouhizeiHyouji">����ŕ\��</param>
    ''' <param name="inMooruTenkaiFlg">���[���W�JFLG</param>
    ''' <param name="inHyoujiJyuusyoNo">�\���Z��_�Ǘ�No</param>
    ''' <param name="strTourokuId">�S����ID</param>
    ''' <param name="strTantousyaMei">�S���Җ�</param>
    ''' <param name="strSyouninsyaId">���F��ID</param>
    ''' <param name="strSyouninsyaMei">���F�Җ�</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function UpdMitumoriKaisu(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal inMitSakuseiKaisuu As Integer, _
                                    ByVal inSyouhizeiHyouji As Integer, _
                                    ByVal inMooruTenkaiFlg As Integer, _
                                    ByVal inHyoujiJyuusyoNo As Integer, _
                                    ByVal strTourokuId As String, _
                                    ByVal strTantousyaMei As String, _
                                    ByVal strSyouninsyaId As String, _
                                    ByVal strSyouninsyaMei As String, _
                                    ByVal strSakuseiDate As String, _
                                    ByVal strIraiTantousyaMei As String) As Boolean

        '�X�V����
        Dim updCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("  t_tyousa_mitumori_sakusei_kanri WITH(UPDLOCK)")
            .AppendLine("SET ")
            .AppendLine("  mit_sakusei_kaisuu = @mit_sakusei_kaisuu")
            .AppendLine("  ,syouhizei_hyouji = @syouhizei_hyouji")
            .AppendLine("  ,mooru_tenkai_flg = @mooru_tenkai_flg")
            .AppendLine("  ,hyouji_jyuusyo_no = @hyouji_jyuusyo_no")
            .AppendLine("  ,tantousya_id = @tantousya_id")
            .AppendLine("  ,tantousya_mei = @tantousya_mei")
            .AppendLine("  ,syouninsya_id = @syouninsya_id")
            .AppendLine("  ,syouninsya_mei = @syouninsya_mei")
            .AppendLine("  ,tys_mit_sakusei_date = @tys_mit_sakusei_date")
            .AppendLine("  ,tys_mit_irai_tantousya_mei = @tys_mit_irai_tantousya_mei")
            .AppendLine("  ,upd_login_user_id = @upd_login_user_id")
            .AppendLine("  ,upd_datetime = GetDate()")
            .AppendLine("WHERE")
            .AppendLine("  kbn = @kbn")
            .AppendLine("  AND hosyousyo_no = @hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))                      '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO
        paramList.Add(MakeParam("@mit_sakusei_kaisuu", SqlDbType.Int, 10, inMitSakuseiKaisuu))    '�쐬��
        paramList.Add(MakeParam("@syouhizei_hyouji", SqlDbType.Int, 10, inSyouhizeiHyouji))    '����ŕ\��
        paramList.Add(MakeParam("@mooru_tenkai_flg", SqlDbType.Int, 10, inMooruTenkaiFlg))     '���[���W�JFLG
        paramList.Add(MakeParam("@hyouji_jyuusyo_no", SqlDbType.Int, 10, inHyoujiJyuusyoNo))   '�\���Z��_�Ǘ�No
        paramList.Add(MakeParam("@tantousya_id", SqlDbType.VarChar, 30, strTourokuId))        '�S����ID
        paramList.Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 128, strTantousyaMei))   '�S���Җ�
        paramList.Add(MakeParam("@syouninsya_id", SqlDbType.VarChar, 30, strSyouninsyaId))    '���F��ID
        paramList.Add(MakeParam("@syouninsya_mei", SqlDbType.VarChar, 128, strSyouninsyaMei)) '���F�Җ�
        paramList.Add(MakeParam("@tys_mit_sakusei_date", SqlDbType.DateTime, 30, strSakuseiDate)) '�������Ϗ��쐬��
        paramList.Add(MakeParam("@tys_mit_irai_tantousya_mei", SqlDbType.VarChar, 20, IIf(strIraiTantousyaMei.Equals(String.Empty), DBNull.Value, strIraiTantousyaMei))) '�������Ϗ�_�˗��S����
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strTourokuId))  '�X�V��

        '���s
        updCount = ExecuteNonQuery(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If Not updCount > 0 Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' �u�������Ϗ��쐬�Ǘ��e�[�u���v�ɓo�^����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <param name="inMitSakuseiKaisuu">���ύ쐬��</param>
    ''' <param name="inSyouhizeiHyouji">����ŕ\��</param>
    ''' <param name="inMooruTenkaiFlg">���[���W�JFLG</param>
    ''' <param name="inHyoujiJyuusyoNo">�\���Z��_�Ǘ�No</param>
    ''' <param name="strTourokuId">�S����ID</param>
    ''' <param name="strTantousyaMei">�S���Җ�</param>
    ''' <param name="strSyouninsyaId">���F��ID</param>
    ''' <param name="strSyouninsyaMei">���F�Җ�</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/14 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function InsMitumoriKaisu(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal inMitSakuseiKaisuu As Integer, _
                                    ByVal inSyouhizeiHyouji As Integer, _
                                    ByVal inMooruTenkaiFlg As Integer, _
                                    ByVal inHyoujiJyuusyoNo As Integer, _
                                    ByVal strTourokuId As String, _
                                    ByVal strTantousyaMei As String, _
                                    ByVal strSyouninsyaId As String, _
                                    ByVal strSyouninsyaMei As String, _
                                    ByVal strSakuseiDate As String, _
                                    ByVal strIraiTantousyaMei As String) As Boolean

        '�X�V����
        Dim inCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("INSERT INTO t_tyousa_mitumori_sakusei_kanri WITH(UPDLOCK)")
            .AppendLine("           (kbn")
            .AppendLine("           ,hosyousyo_no")
            .AppendLine("           ,mit_sakusei_kaisuu")
            .AppendLine("           ,syouhizei_hyouji")
            .AppendLine("           ,mooru_tenkai_flg")
            .AppendLine("           ,hyouji_jyuusyo_no")
            .AppendLine("           ,tantousya_id")
            .AppendLine("           ,tantousya_mei")
            .AppendLine("           ,syouninsya_id")
            .AppendLine("           ,syouninsya_mei")
            .AppendLine("           ,tys_mit_sakusei_date")
            .AppendLine("           ,tys_mit_irai_tantousya_mei")
            .AppendLine("           ,add_login_user_id")
            .AppendLine("           ,add_datetime")
            .AppendLine("           ,upd_login_user_id")
            .AppendLine("           ,upd_datetime)")
            .AppendLine("     VALUES")
            .AppendLine("           (@kbn")
            .AppendLine("           ,@hosyousyo_no")
            .AppendLine("           ,@mit_sakusei_kaisuu")
            .AppendLine("           ,@syouhizei_hyouji")
            .AppendLine("           ,@mooru_tenkai_flg")
            .AppendLine("           ,@hyouji_jyuusyo_no")
            .AppendLine("           ,@tantousya_id")
            .AppendLine("           ,@tantousya_mei")
            .AppendLine("           ,@syouninsya_id")
            .AppendLine("           ,@syouninsya_mei")
            .AppendLine("           ,@tys_mit_sakusei_date")
            .AppendLine("           ,@tys_mit_irai_tantousya_mei")
            .AppendLine("           ,@add_login_user_id")
            .AppendLine("           ,GetDate()")
            .AppendLine("           ,NULL")
            .AppendLine("           ,NULL)")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))                      '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO
        paramList.Add(MakeParam("@mit_sakusei_kaisuu", SqlDbType.Int, 10, inMitSakuseiKaisuu))    '�쐬��
        paramList.Add(MakeParam("@syouhizei_hyouji", SqlDbType.Int, 10, inSyouhizeiHyouji))    '����ŕ\��
        paramList.Add(MakeParam("@mooru_tenkai_flg", SqlDbType.Int, 10, inMooruTenkaiFlg))     '���[���W�JFLG
        paramList.Add(MakeParam("@hyouji_jyuusyo_no", SqlDbType.Int, 10, inHyoujiJyuusyoNo))   '�\���Z��_�Ǘ�No
        paramList.Add(MakeParam("@tantousya_id", SqlDbType.VarChar, 30, strTourokuId))        '�S����ID
        paramList.Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 128, strTantousyaMei))   '�S���Җ�
        paramList.Add(MakeParam("@syouninsya_id", SqlDbType.VarChar, 30, strSyouninsyaId))    '���F��ID
        paramList.Add(MakeParam("@syouninsya_mei", SqlDbType.VarChar, 128, strSyouninsyaMei)) '���F�Җ�
        paramList.Add(MakeParam("@tys_mit_sakusei_date", SqlDbType.DateTime, 30, strSakuseiDate)) '�������Ϗ��쐬��
        paramList.Add(MakeParam("@tys_mit_irai_tantousya_mei", SqlDbType.VarChar, 20, IIf(strIraiTantousyaMei.Equals(String.Empty), DBNull.Value, strIraiTantousyaMei))) '�������Ϗ�_�˗��S����
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strTourokuId))   '�o�^���O�C�����[�UID

        '���s
        inCount = ExecuteNonQuery(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If Not inCount > 0 Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' �S����ID�̑��݂𔻝Ђ���
    ''' </summary>
    ''' <param name="strTantousyaId">�S����ID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/14 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelSonzaiHandan(ByVal strTantousyaId As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   COUNT(tantousya_id)")
            .AppendLine("FROM  ")
            .AppendLine("   m_tantousya_syouninin_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE tantousya_id = @tantousya_id")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@tantousya_id", SqlDbType.VarChar, 30, strTantousyaId)) '�S����ID

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ���F�ҕR�t�����F��Ǘ��}�X�^����y���F��z���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelSyouninIn(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("  syou.syouninin_kakunousaki_pass")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri ty WITH(READCOMMITTED)")
            .AppendLine("   inner join  m_syouninsya_syouninin_kanri syou")
            .AppendLine("   on ty.syouninsya_id =  syou.syouninsya_id")
            .AppendLine("WHERE")
            .AppendLine("   ty.kbn = @kbn and ty.hosyousyo_no=@hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �S���ҕR�t�����F��Ǘ��}�X�^����y���F��z���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelTantouIn(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("  tanto.tantousyain_kakunousaki_pass")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri ty WITH(READCOMMITTED)")
            .AppendLine("   inner join  m_tantousya_syouninin_kanri tanto")
            .AppendLine("   on ty.tantousya_id =  tanto.tantousya_id")
            .AppendLine("WHERE")
            .AppendLine("   ty.kbn = @kbn and ty.hosyousyo_no=@hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' [Fixed Data Section]ONE
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelKihonInfoOne(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   CONVERT(varchar(100),ty.tys_mit_sakusei_date,111) AS hituke --�������Ϗ��쐬��")
            .AppendLine("   ,'��' + jyu.yuubin_no AS yuubin_no --�X�֔ԍ�")
            .AppendLine("   ,jyu.jyuusyo1 AS jyuusyo1 --�Z��1")
            .AppendLine("   ,jyu.jyuusyo2 AS jyuusyo2 --�Z��2")
            .AppendLine("   ,'�s�����F' + jyu.tel_no + '  ' + '�e�����F' + jyu.fax_no AS tel_fax --Tel")
            .AppendLine("   ,ty.tantousya_mei AS sousinsya --�������Ϗ��쐬��")
            .AppendLine("   ,ty.tys_mit_irai_tantousya_mei AS tys_mit_irai_tantousya_mei --�������Ϗ�_�˗��S����")
            '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
            .AppendLine("   ,jyu.hosoku  AS hosoku --�⑫")
            '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri ty WITH(READCOMMITTED)")
            .AppendLine("   inner join m_mitumori_hyoujijyuusyo_kanri jyu")
            .AppendLine("   on ty.hyouji_jyuusyo_no=jyu.kanri_no")
            .AppendLine("WHERE")
            .AppendLine("   ty.kbn = @kbn and ty.hosyousyo_no=@hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' [Fixed Data Section]TWO
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelKihonInfoTwo(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   j.irai_tantousya_mei AS attn --�˗��S����")
            .AppendLine("   ,k.kameiten_mei1 AS made --�����X��")
            .AppendLine("   ,j.kbn+j.hosyousyo_no AS bukken_no --�����ԍ�")
            .AppendLine("   ,j.sesyu_mei AS bukken_mei --�{�喼")
            .AppendLine("   ,j.bukken_jyuusyo1+j.bukken_jyuusyo2+j.bukken_jyuusyo3 AS bukken_jyuusyo --�[���ꏊ")
            .AppendLine("FROM  ")
            .AppendLine("   t_jiban j WITH(READCOMMITTED)")
            .AppendLine("   inner join m_kameiten k")
            .AppendLine("   on j.kameiten_cd = k.kameiten_cd")
            .AppendLine("WHERE")
            .AppendLine("   j.kbn = @kbn and j.hosyousyo_no=@hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �䌩�Ϗ��̃f�[�^
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�����ԍ�</param>
    ''' <param name="flg">�Ŕ��Ɛō��̋敪</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelTyouhyouDate(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal flg As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	jts.gamen_hyouji_no ��ʕ\��NO")
            .AppendLine("	,jts.syouhin_cd ���i�R�[�h")
            .AppendLine("	,CASE WHEN jts.a+jts.b IS NULL THEN")
            .AppendLine("			s.syouhin_mei")
            .AppendLine("		ELSE")
            .AppendLine("			(s.syouhin_mei)+'['+jts.tys_houhou_mei_ryaku+']'")
            .AppendLine("		END AS syouhin_mei --���i��")
            .AppendLine("	,'�P��' AS suuryou --����")
            If flg = "�Ŕ�" Then
                .AppendLine("	,jts.uri_gaku AS tanka --�Ŕ��P��")
                .AppendLine("	,1 * (jts.uri_gaku) AS kingaku --���z")
                .AppendLine("	,jts.syouhizei_gaku AS syouhizei --����Ŋz")
            ElseIf flg = "�ō�" Then
                .AppendLine("	,((jts.uri_gaku) + (jts.syouhizei_gaku)) AS tanka  --�ō��P��")
                .AppendLine("	,1 * ((jts.uri_gaku) + (jts.syouhizei_gaku)) AS kingaku --���z")
                .AppendLine("	,0 AS syouhizei --����Ŋz")
            End If
            .AppendLine("   ,'' bikou --���l")
            .AppendLine("FROM m_syouhin s  --���i�}�X�^")
            .AppendLine("INNER JOIN ")
            .AppendLine("	(SELECT")
            .AppendLine("		ts.gamen_hyouji_no")
            .AppendLine("		,ts.syouhin_cd")
            .AppendLine("		,ts.uri_gaku")
            .AppendLine("		,ts.syouhizei_gaku")
            .AppendLine("		,ts.bunrui_cd")
            .AppendLine("		,mt.tys_houhou_mei_ryaku")
            .AppendLine("		,km1.code as a")
            .AppendLine("		,km2.code as b")
            .AppendLine("	FROM t_jiban j --�n�Ճe�[�u��")
            .AppendLine("	INNER JOIN t_teibetu_seikyuu ts		--�@�ʐ����e�[�u��")
            .AppendLine("		ON j.kbn =  ts.kbn ")
            .AppendLine("		AND j.hosyousyo_no= ts.hosyousyo_no")
            .AppendLine("	LEFT JOIN m_tyousahouhou mt			--�������@�}�X�^")
            .AppendLine("		ON j.tys_houhou = mt.tys_houhou_no")
            .AppendLine("	LEFT JOIN m_kakutyou_meisyou AS km1 --�g�����̃}�X�^")
            .AppendLine("		ON km1.code = ts.syouhin_cd")
            .AppendLine("		AND km1.meisyou_syubetu = 26")
            .AppendLine("	LEFT JOIN m_kakutyou_meisyou AS km2 --�g�����̃}�X�^")
            .AppendLine("		ON km2.code = j.tys_houhou")
            .AppendLine("		AND km2.meisyou_syubetu = 27")
            .AppendLine("	where")
            .AppendLine("		j.kbn = @kbn ")
            .AppendLine("		AND j.hosyousyo_no = @hosyousyo_no ")
            .AppendLine("		AND ts.bunrui_cd IN('100','110','115','120') ")
            .AppendLine("		AND ts.uri_gaku <> '0'") '������z��'0'�ł͂Ȃ�
            .AppendLine("       AND ts.seikyuu_umu = '1'") '�����L���Ő����L��̏��i

            ''�ŗ���\����������(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/18��
            '.AppendLine("       AND ts.uri_date IS NULL")
            ''�ŗ���\����������(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/18��

            .AppendLine("	) jts")
            .AppendLine("ON jts.syouhin_cd = s.syouhin_cd")
            .AppendLine("ORDER BY bunrui_cd ASC")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))                      '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �u���[���W�J�v�̃Z�b�g�𔻝Ђ���
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�����ԍ�</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/12/02 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelMoruHandan(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   mooru_tenkai_flg")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   kbn = @kbn")
            .AppendLine("   AND hosyousyo_no = @hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �ŗ���ǉ�����
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2014/02/17 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function SelZeiritu(ByVal strKbn As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   meisyou")
            .AppendLine("FROM m_kakutyou_meisyou WITH(READCOMMITTED)")
            .AppendLine("WHERE 1 = 1")
            If strKbn.Equals("2") Then
                '�ō�
                .AppendLine("AND code = '2'")
            Else
                '�Ŕ�
                .AppendLine("AND code = '1'")
            End If
            .AppendLine("AND meisyou_syubetu ='63'")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strKbn", SqlDbType.Char, 1, strKbn)) '�敪

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �V�X�e������
    ''' </summary>
    ''' <returns></returns>
    Public Function SelSysTime() As Date
        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("   GETDATE() ")
        End With

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet")

        '�߂�
        Return CDate(dsDataSet.Tables(0).Rows(0).Item(0))

    End Function

    ''' <summary>
    ''' �u�������Ϗ�_�˗��S���ҁv���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function SelIraiTantousya(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   ISNULL(ty.tys_mit_irai_tantousya_mei, '') AS tys_mit_irai_tantousya_mei")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri ty WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   ty.kbn = @kbn and ty.hosyousyo_no=@hosyousyo_no")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '�敪
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '�ۏ؏�NO

        ' �������s
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function
End Class
