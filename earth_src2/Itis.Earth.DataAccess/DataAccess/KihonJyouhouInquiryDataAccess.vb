Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class KihonJyouhouInquiryDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    Private Const SEIKYUUSIMEDATE As Integer = 0
    Private Const HANSOHUHINSEIKYUUSIMEDATE As Integer = 1

    ''' <summary>
    ''' MailAddress�@�擾
    ''' </summary>
    ''' <param name="yuubin_no">�X��No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet

        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT (yuubin_no + ',' + isnull(todoufuken_mei,'')+     isnull(sikutyouson_mei,'')+      isnull(tiiki_mei,'')) as mei,m_todoufuken.todouhuken_cd AS todouhuken_cd")
        sql.AppendLine("    from ")
        sql.AppendLine("    m_yuubin LEFT JOIN m_todoufuken ")
        sql.AppendLine("    ON m_yuubin.todoufuken_mei=m_todoufuken.todouhuken_mei ")
        sql.AppendLine("    where ")
        sql.AppendLine("    yuubin_no like @yuubin_no order by yuubin_no")

        paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, yuubin_no & "%"))
        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                   "mei", paramList.ToArray)

        Return data

    End Function

    ''' <summary>
    ''' �V�X�e������
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSysDate() As String
        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("    getdate()")


        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                   "time", paramList.ToArray)

        Return data.Tables(0).Rows(0).Item(0).ToString

    End Function

    ''' <summary>
    ''' �����X�}�X�g�����擾����
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelkameitenInfo(ByVal kameiten_cd As String _
                                      ) As KameitenDataSet.m_kameitenTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim KameitenDataSet As New KameitenDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("    m_kameiten.kbn")
        sql.AppendLine("    , m_kameiten.kameiten_cd")
        sql.AppendLine("    , m_kameiten.torikesi")
        sql.AppendLine("    , m_kameiten.kameiten_mei1")
        sql.AppendLine("    , m_kameiten.tenmei_kana1")
        sql.AppendLine("    , m_kameiten.kameiten_mei2")
        sql.AppendLine("    , m_kameiten.tenmei_kana2")
        sql.AppendLine("    , m_kameiten.eigyousyo_cd")
        sql.AppendLine("    , m_kameiten.keiretu_cd")
        sql.AppendLine("    , m_kameiten.th_kasi_cd")
        sql.AppendLine("    , m_kameiten.danmenzu1")
        sql.AppendLine("    , m_kameiten.danmenzu2")
        sql.AppendLine("    , m_kameiten.danmenzu3")
        sql.AppendLine("    , m_kameiten.danmenzu4")
        sql.AppendLine("    , m_kameiten.danmenzu5")
        sql.AppendLine("    , m_kameiten.danmenzu6")
        sql.AppendLine("    , m_kameiten.danmenzu7")
        sql.AppendLine("    , m_kameiten.tys_seikyuu_saki")
        sql.AppendLine("    , m_kameiten.tys_seikyuu_sime_date")
        sql.AppendLine("    , m_kameiten.koj_seikyuusaki")
        sql.AppendLine("    , m_kameiten.koj_seikyuu_sime_date")
        sql.AppendLine("    , m_kameiten.hansokuhin_seikyuusaki")
        sql.AppendLine("    , m_kameiten.hansokuhin_seikyuu_sime_date")
        sql.AppendLine("    , m_kameiten.ss_kkk")
        sql.AppendLine("    , m_kameiten.sai_tys_kkk")
        sql.AppendLine("    , m_kameiten.ssgr_kkk")
        sql.AppendLine("    , m_kameiten.kaiseki_hosyou_kkk")
        sql.AppendLine("    , m_kameiten.hosyounasi_umu")
        sql.AppendLine("    , m_kameiten.kaiyaku_haraimodosi_kkk")
        sql.AppendLine("    , m_kameiten.todouhuken_cd")
        sql.AppendLine("    , m_kameiten.hosyou_kikan")
        sql.AppendLine("    , m_kameiten.hosyousyo_hak_umu")
        sql.AppendLine("    , m_kameiten.builder_no")
        sql.AppendLine("    , m_kameiten.koj_gaisya_seikyuu_umu")
        sql.AppendLine("    , m_kameiten.koj_tantou_flg")
        sql.AppendLine("    , m_kameiten.nenkan_tousuu")
        sql.AppendLine("    , m_kameiten.nyuukin_kakunin_jyouken")
        sql.AppendLine("    , m_kameiten.nyuukin_kakunin_oboegaki")
        sql.AppendLine("    , m_kameiten.eigyou_tantousya_mei")
        sql.AppendLine("    , m_kameiten.tys_mitsyo_flg")
        sql.AppendLine("    , m_kameiten.hattyuusyo_flg")
        sql.AppendLine("    , m_kameiten.mitsyo_file_mei")
        sql.AppendLine("    , m_kameiten.jizen_tys_kkk")
        sql.AppendLine("    , m_kameiten.jisin_hosyou_flg")
        sql.AppendLine("    , m_kameiten.jisin_hosyou_add_date")
        sql.AppendLine("    , m_kameiten.hikitugi_kanryou_date")
        sql.AppendLine("    , m_kameiten.kyuu_eigyou_tantousya_mei")
        sql.AppendLine("    , m_kameiten.add_login_user_id")
        sql.AppendLine("    , m_kameiten.add_datetime")
        sql.AppendLine("    , isnull(m_kameiten.upd_login_user_id,m_kameiten.add_login_user_id) as upd_login_user_id ")
        sql.AppendLine("    , isnull(m_kameiten.upd_datetime,m_kameiten.add_datetime) as upd_datetime ")
        sql.AppendLine(" ,m_data_kbn.kbn_mei AS kbn_mei ")
        sql.AppendLine(" ,m_kameiten2.kameiten_mei1  AS builder_mei")
        sql.AppendLine(" ,m_eigyousyo.eigyousyo_mei  AS eigyousyo_mei")
        sql.AppendLine(" ,m_keiretu.keiretu_mei  AS keiretu_mei")
        sql.AppendLine(" ,m_jhs_mailbox.displayname  AS simei")
        sql.AppendLine(" ,GETDATE()  AS sansyou_date")
        sql.AppendLine(" ,hattyuu_teisi_flg")
        '�n���R  2010/08/17 ����������R�[�h��ǉ����� ��
        sql.AppendLine(" ,tys_seikyuu_saki_cd")
        '�n���R  2010/08/17 ����������R�[�h��ǉ����� ��
        sql.AppendLine(" FROM  ")
        sql.AppendLine(" m_kameiten  WITH (READCOMMITTED) ")
        sql.AppendLine(" LEFT JOIN m_data_kbn  WITH (READCOMMITTED) ")
        sql.AppendLine(" ON  m_kameiten.kbn=m_data_kbn.kbn ")
        sql.AppendLine(" LEFT JOIN  ")
        sql.AppendLine(" (SELECT ")
        sql.AppendLine(" kameiten_cd, ")
        sql.AppendLine(" kameiten_mei1 ")
        sql.AppendLine(" FROM  ")
        sql.AppendLine(" m_kameiten  WITH (READCOMMITTED) ")
        'sql.AppendLine(" WHERE ")
        'sql.AppendLine(" kbn NOT IN  ")
        'sql.AppendLine(" (@kbn0,@kbn1) ")
        sql.AppendLine(" ) AS m_kameiten2 ")
        sql.AppendLine(" ON  m_kameiten.builder_no=m_kameiten2.kameiten_cd ")
        sql.AppendLine(" LEFT JOIN m_eigyousyo  WITH (READCOMMITTED) ")
        sql.AppendLine(" ON  m_kameiten.eigyousyo_cd=m_eigyousyo.eigyousyo_cd ")
        sql.AppendLine(" LEFT JOIN m_keiretu  WITH (READCOMMITTED) ")
        sql.AppendLine(" ON  m_kameiten.keiretu_cd=m_keiretu.keiretu_cd ")
        sql.AppendLine(" LEFT JOIN m_jhs_mailbox  WITH (READCOMMITTED) ")
        sql.AppendLine(" ON  m_jhs_mailbox.PrimaryWindowsNTAccount=isnull(m_kameiten.upd_login_user_id ,m_kameiten.add_login_user_id)")
        sql.AppendLine(" WHERE m_kameiten.kameiten_cd=@kameiten_cd ")

        paramList.Add(MakeParam("@kbn0", SqlDbType.Char, 1, "T"))
        paramList.Add(MakeParam("@kbn1", SqlDbType.Char, 1, "J"))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), KameitenDataSet, _
                    KameitenDataSet.m_kameitenTable.TableName, paramList.ToArray)

        Return KameitenDataSet.m_kameitenTable

    End Function

    ''' <summary>
    ''' �����X�c�Ə����擾
    ''' </summary>
    ''' <param name="eigyousyo">�c�Ə�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelEigyousyo(ByVal eigyousyo As String) As KameitenDataSet.eigyousyoTableDataTable
        ' DataSet�C���X�^���X�̐���
        Dim KameitenDataSet As New KameitenDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.Append("yuubin_no")
        sql.Append(",jyuusyo1")
        sql.Append(",jyuusyo2")
        sql.Append(",tel_no")
        sql.Append(",fax_no")
        sql.Append(",busyo_mei")
        sql.AppendLine(" FROM  ")
        sql.AppendLine(" m_eigyousyo  WITH (READCOMMITTED) ")
        sql.AppendLine(" WHERE eigyousyo_cd=@eigyousyo_cd ")
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, eigyousyo))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), KameitenDataSet, _
                    KameitenDataSet.eigyousyoTable.TableName, paramList.ToArray)

        Return KameitenDataSet.eigyousyoTable
    End Function

    ''' <summary>
    ''' �����X�}�X�g�����擾����
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelkeiretuCd(ByVal kameiten_cd As String _
                                      ) As String

        ' DataSet�C���X�^���X�̐���
        Dim KameitenDataSet As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("     m_kameiten.keiretu_cd")

        sql.AppendLine(" FROM  ")
        sql.AppendLine(" m_kameiten  WITH (READCOMMITTED) ")
        sql.AppendLine(" WHERE kameiten_cd=@kameiten_cd ")


        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), KameitenDataSet, _
                    "data", paramList.ToArray)

        If KameitenDataSet.Tables(0).Rows.Count > 0 Then

            Return KameitenDataSet.Tables(0).Rows(0).Item(0).ToString()
        Else

            Return String.Empty

        End If


    End Function

    ''' <summary>
    ''' �����X�}�X�g�����X�V����
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updkameitenInfo(ByVal kameiten_cd As String, _
                                     ByVal insdata As KameitenDataSet.m_kameitenTableDataTable) As Integer

        ' DataSet�C���X�^���X�̐���
        Dim KameitenDataSet As New KameitenDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine(" UPDATE  m_kameiten WITH(UPDLOCK) set ")
        sql.AppendLine("    danmenzu1=@danmenzu1")
        sql.AppendLine("    , danmenzu2=@danmenzu2")
        sql.AppendLine("    , danmenzu3=@danmenzu3")
        sql.AppendLine("    , danmenzu4=@danmenzu4")
        sql.AppendLine("    , danmenzu5=@danmenzu5")
        sql.AppendLine("    , danmenzu6=@danmenzu6")
        sql.AppendLine("    , danmenzu7=@danmenzu7")
        sql.AppendLine("    , upd_login_user_id=@upd_login_user_id")
        sql.AppendLine("    , upd_datetime=getdate()")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")


        If insdata(0).Isdanmenzu1Null Then
            paramList.Add(MakeParam("@danmenzu1", SqlDbType.Int, 32, DBNull.Value))
        Else
            paramList.Add(MakeParam("@danmenzu1", SqlDbType.Int, 32, insdata(0).danmenzu1))

        End If

        If insdata(0).Isdanmenzu2Null Then
            paramList.Add(MakeParam("@danmenzu2", SqlDbType.Int, 32, DBNull.Value))
        Else
            paramList.Add(MakeParam("@danmenzu2", SqlDbType.Int, 32, insdata(0).danmenzu2))

        End If

        If insdata(0).Isdanmenzu3Null Then
            paramList.Add(MakeParam("@danmenzu3", SqlDbType.Int, 32, DBNull.Value))
        Else
            paramList.Add(MakeParam("@danmenzu3", SqlDbType.Int, 32, insdata(0).danmenzu3))

        End If

        If insdata(0).Isdanmenzu4Null Then
            paramList.Add(MakeParam("@danmenzu4", SqlDbType.Int, 32, DBNull.Value))
        Else
            paramList.Add(MakeParam("@danmenzu4", SqlDbType.Int, 32, insdata(0).danmenzu4))

        End If

        If insdata(0).Isdanmenzu5Null Then
            paramList.Add(MakeParam("@danmenzu5", SqlDbType.Int, 32, DBNull.Value))
        Else
            paramList.Add(MakeParam("@danmenzu5", SqlDbType.Int, 32, insdata(0).danmenzu5))

        End If

        If insdata(0).Isdanmenzu6Null Then
            paramList.Add(MakeParam("@danmenzu6", SqlDbType.Int, 32, DBNull.Value))
        Else
            paramList.Add(MakeParam("@danmenzu6", SqlDbType.Int, 32, insdata(0).danmenzu6))

        End If

        If insdata(0).Isdanmenzu7Null Then
            paramList.Add(MakeParam("@danmenzu7", SqlDbType.Int, 32, DBNull.Value))
        Else
            paramList.Add(MakeParam("@danmenzu7", SqlDbType.Int, 32, insdata(0).danmenzu7))

        End If
        'paramList.Add(MakeParam("@danmenzu2", SqlDbType.Int, 32, insdata(0).danmenzu2))
        'paramList.Add(MakeParam("@danmenzu3", SqlDbType.Int, 32, insdata(0).danmenzu3))
        'paramList.Add(MakeParam("@danmenzu4", SqlDbType.Int, 32, insdata(0).danmenzu4))
        'paramList.Add(MakeParam("@danmenzu5", SqlDbType.Int, 32, insdata(0).danmenzu5))
        'paramList.Add(MakeParam("@danmenzu6", SqlDbType.Int, 32, insdata(0).danmenzu6))
        'paramList.Add(MakeParam("@danmenzu7", SqlDbType.Int, 32, insdata(0).danmenzu7))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, insdata(0).upd_login_user_id))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))


        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        UpdKyoutuuJyouhouRenkei(kameiten_cd, insdata(0).upd_login_user_id)

        Return intResult

    End Function

    ''' <summary>
    ''' �����X���ނ������X�Z�����擾����
    ''' 
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelKameitenJyushoInfo(ByVal kameiten_cd As String) As KameitenjyushoDataSet

        ' DataSet�C���X�^���X�̐���
        Dim Kameitenjyusho As New KameitenjyushoDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("SELECT          ")
        sql.AppendLine("kameiten_cd")           '	�����X����
        sql.AppendLine(",jyuusyo_no")           '	�Z��NO
        sql.AppendLine(",jyuusyo1")         '	�Z��1
        sql.AppendLine(",jyuusyo2")         '	�Z��2
        sql.AppendLine(",yuubin_no")            '	�X�֔ԍ�
        sql.AppendLine(",tel_no")           '	�d�b�ԍ�
        sql.AppendLine(",fax_no")           '	FAX�ԍ�
        sql.AppendLine(",busyo_mei")            '	������
        sql.AppendLine(",daihyousya_mei")           '	��\�Җ�
        sql.AppendLine(",add_nengetu")          '	�o�^�N��
        sql.AppendLine(",isnull(seikyuusyo_flg,0) as seikyuusyo_flg")           '	������FLG
        sql.AppendLine(",isnull(hosyousyo_flg,0) as hosyousyo_flg")            '	�ۏ؏�FLG
        sql.AppendLine(",isnull(hkks_flg,0) as hkks_flg")         '	�񍐏�FLG
        sql.AppendLine(",isnull(teiki_kankou_flg,0) as teiki_kankou_flg")         '	������sFLG
        sql.AppendLine(",bikou1")           '	���l1
        sql.AppendLine(",bikou2")           '	���l2
        sql.AppendLine(",upd_date")         '	�X�V��
        sql.AppendLine(",mail_address")         '	Ұٱ��ڽ
        sql.AppendLine(",isnull(kasi_hosyousyo_flg,0) as kasi_hosyousyo_flg")           '	���r�ۏ؏�FLG
        sql.AppendLine(",isnull(koj_hkks_flg,0) as koj_hkks_flg")         '	�H���񍐏�FLG
        sql.AppendLine(",isnull(kensa_hkks_flg,0) as kensa_hkks_flg")           '	�����񍐏�FLG

        sql.AppendLine(",add_login_user_id")
        sql.AppendLine(",add_datetime")
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime ")
        sql.AppendLine(",todouhuken_cd ")

        sql.AppendLine("FROM            ")
        sql.AppendLine("    m_kameiten_jyuusyo  WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), Kameitenjyusho, _
                    Kameitenjyusho.m_kameiten_jyuusyo.TableName, paramList.ToArray)

        Return Kameitenjyusho


    End Function

    ''' <summary>
    ''' �����X�̍X�V���擾
    ''' 
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelMaxUpdDate(ByVal kameiten_cd As String) As String

        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("SELECT     top 1       ")

        sql.AppendLine("isnull(max(upd_datetime),max(add_datetime)) as maxtime")
        sql.AppendLine("       , isnull(upd_login_user_id,add_login_user_id) as theuser")

        sql.AppendLine("FROM            ")
        sql.AppendLine("    m_kameiten_jyuusyo  WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")


        sql.AppendLine("        group by upd_login_user_id,add_login_user_id ")

        sql.AppendLine("     order by maxtime desc")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                   "maxTime", paramList.ToArray)

        If data.Tables(0).Rows.Count > 0 Then
            Dim returnValue As String
            returnValue = data.Tables(0).Rows(0).Item(0).ToString & "," & data.Tables(0).Rows(0).Item(1).ToString
            Return returnValue
        Else
            Return ","
        End If



    End Function

    ''' <summary>
    ''' �����X���ނ������X�Z�����擾����
    ''' 
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelKameitenJyushoAru(ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer) As Boolean

        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("SELECT  COUNT(*) AS COUNT  ")

        sql.AppendLine("FROM            ")
        sql.AppendLine("    m_kameiten_jyuusyo  WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and jyuusyo_no = @jyuusyo_no")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, jyuusyo_no))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                    "count", paramList.ToArray)

        If data.Tables(0).Rows(0).Item(0) > 0 Then

            Return True
        Else
            Return False
        End If


    End Function

    ''' <summary>
    ''' �����X�Z���X�V
    ''' </summary>
    ''' <param name="kameiten_cd">�����XCD</param>
    ''' <param name="jyuusyo_no">�Z��NO</param>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updKameitenjyusho(ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer, ByVal data As KameitenjyushoDataSet)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" UPDATE  m_kameiten_jyuusyo WITH(UPDLOCK) set ")
        sql.AppendLine("jyuusyo1=@jyuusyo1")
        sql.AppendLine(",jyuusyo2=@jyuusyo2")
        sql.AppendLine(",yuubin_no=@yuubin_no")
        sql.AppendLine(",tel_no=@tel_no")
        sql.AppendLine(",fax_no=@fax_no")
        sql.AppendLine(",busyo_mei=@busyo_mei")
        sql.AppendLine(",daihyousya_mei=@daihyousya_mei")
        sql.AppendLine(",add_nengetu=@add_nengetu")
        sql.AppendLine(",seikyuusyo_flg=@seikyuusyo_flg")
        sql.AppendLine(",hosyousyo_flg=@hosyousyo_flg")
        sql.AppendLine(",hkks_flg=@hkks_flg")
        sql.AppendLine(",teiki_kankou_flg=@teiki_kankou_flg")
        sql.AppendLine(",bikou1=@bikou1")
        sql.AppendLine(",bikou2=@bikou2")
        sql.AppendLine(",upd_date=convert(char(10),GetDate(),101)")
        sql.AppendLine(",mail_address=@mail_address")
        sql.AppendLine(",kasi_hosyousyo_flg=@kasi_hosyousyo_flg")
        sql.AppendLine(",koj_hkks_flg=@koj_hkks_flg")
        sql.AppendLine(",kensa_hkks_flg=@kensa_hkks_flg")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")
        sql.AppendLine(",todouhuken_cd	=	@todouhuken_cd")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and jyuusyo_no = @jyuusyo_no")

        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).upd_login_user_id))

        '�Z��NO���Ƃ�SQL�����쐬����
        Select Case jyuusyo_no
            Case 1

                paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, data.m_kameiten_jyuusyo(0).jyuusyo1))
                paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).jyuusyo2))
                paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, data.m_kameiten_jyuusyo(0).yuubin_no))

                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, data.m_kameiten_jyuusyo(0).tel_no))
                paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, data.m_kameiten_jyuusyo(0).fax_no))
                paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, data.m_kameiten_jyuusyo(0).busyo_mei))
                paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, data.m_kameiten_jyuusyo(0).daihyousya_mei))
                paramList.Add(MakeParam("@add_nengetu", SqlDbType.VarChar, 7, data.m_kameiten_jyuusyo(0).add_nengetu))

                paramList.Add(MakeParam("@seikyuusyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).seikyuusyo_flg))
                paramList.Add(MakeParam("@hosyousyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).hosyousyo_flg))
                paramList.Add(MakeParam("@hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).hkks_flg))
                paramList.Add(MakeParam("@teiki_kankou_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).teiki_kankou_flg))

                paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).bikou1))
                paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).bikou2))

                paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, data.m_kameiten_jyuusyo(0).mail_address))
                paramList.Add(MakeParam("@kasi_hosyousyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg))
                paramList.Add(MakeParam("@koj_hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).koj_hkks_flg))
                paramList.Add(MakeParam("@kensa_hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).kensa_hkks_flg))

                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, data.m_kameiten_jyuusyo(0).kameiten_cd))
                paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, data.m_kameiten_jyuusyo(0).jyuusyo_no))
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, data.m_kameiten_jyuusyo(0).todouhuken_cd))

                ' �N�G�����s
                intResult = ExecuteNonQuery(connStr, _
                                            CommandType.Text, _
                                            sql.ToString(), _
                                            paramList.ToArray)

            Case 2, 3, 4, 5, 6, 7, 8
                paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, data.m_kameiten_jyuusyo(0).jyuusyo1))
                paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).jyuusyo2))
                paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, data.m_kameiten_jyuusyo(0).yuubin_no))

                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, data.m_kameiten_jyuusyo(0).tel_no))
                paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, data.m_kameiten_jyuusyo(0).fax_no))
                paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, data.m_kameiten_jyuusyo(0).busyo_mei))
                paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, data.m_kameiten_jyuusyo(0).daihyousya_mei))
                paramList.Add(MakeParam("@add_nengetu", SqlDbType.VarChar, 7, DBNull.Value))

                paramList.Add(MakeParam("@seikyuusyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).seikyuusyo_flg))
                paramList.Add(MakeParam("@hosyousyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).hosyousyo_flg))
                paramList.Add(MakeParam("@hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).hkks_flg))
                paramList.Add(MakeParam("@teiki_kankou_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).teiki_kankou_flg))

                paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).bikou1))
                paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, DBNull.Value))

                paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, DBNull.Value))
                paramList.Add(MakeParam("@kasi_hosyousyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg))
                paramList.Add(MakeParam("@koj_hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).koj_hkks_flg))
                paramList.Add(MakeParam("@kensa_hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).kensa_hkks_flg))

                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, data.m_kameiten_jyuusyo(0).kameiten_cd))
                paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, data.m_kameiten_jyuusyo(0).jyuusyo_no))
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, data.m_kameiten_jyuusyo(0).todouhuken_cd))

                ' �N�G�����s
                intResult = ExecuteNonQuery(connStr, _
                                            CommandType.Text, _
                                            sql.ToString(), _
                                            paramList.ToArray)
        End Select

        If SelKameitenJyushorenkei(kameiten_cd, jyuusyo_no) Then
            updKameitenjyushorenkei(2, kameiten_cd, jyuusyo_no, data.m_kameiten_jyuusyo(0).upd_login_user_id)
        Else
            InsKameitenjyushorenkei(2, kameiten_cd, jyuusyo_no, data.m_kameiten_jyuusyo(0).upd_login_user_id)
        End If

        Return intResult


    End Function

    ''' <summary>
    ''' �����X���ނ������X�Z���A�g���擾����
    ''' 
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelKameitenJyushorenkei(ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer) As Boolean

        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("SELECT          ")
        sql.AppendLine("kameiten_cd")           '	�����X����
        sql.AppendLine("FROM            ")
        sql.AppendLine("    m_kameiten_jyuusyo_renkei  WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and jyuusyo_no = @jyuusyo_no")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, jyuusyo_no))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                 "data", paramList.ToArray)

        If data.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If


    End Function

    ''' <summary>
    ''' �����X�Z���A�g�X�V
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="jyuusyo_no">�Z��No.</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updKameitenjyushorenkei(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer, ByVal upd_login_user_id As String)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim i As Integer
        For i = 1 To jyuusyo_no
            If SelKameitenJyushorenkei(kameiten_cd, i) Then
            Else
                InsKameitenjyushorenkei2(2, kameiten_cd, i, upd_login_user_id)
            End If
        Next

        '�V�K�ǉ�
        sql.AppendLine(" UPDATE  m_kameiten_jyuusyo_renkei WITH(UPDLOCK) set ")
        sql.AppendLine("renkei_siji_cd=@renkei_siji_cd")
        sql.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=getdate()")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and jyuusyo_no = @jyuusyo_no")

        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, jyuusyo_no))

        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)



        Return intResult


    End Function

    ''' <summary>
    ''' �����X�Z���A�g�폜
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="jyuusyo_no">�Z��No</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updKameitenjyushorenkeiDel(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer, ByVal upd_login_user_id As String)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim i As Integer
        For i = 1 To jyuusyo_no
            If SelKameitenJyushorenkei(kameiten_cd, i) Then
            Else
                InsKameitenjyushorenkei2(2, kameiten_cd, i, upd_login_user_id)
            End If
        Next


        sql.AppendLine("DECLARE @jyuusyono int")
        sql.AppendLine("set @jyuusyono = (select isnull( (select max(jyuusyo_no) from m_kameiten_jyuusyo_renkei Where kameiten_cd = @kameiten_cd), 0))")

        '�V�K�ǉ�
        sql.AppendLine(" UPDATE  m_kameiten_jyuusyo_renkei WITH(UPDLOCK) set ")
        sql.AppendLine("     jyuusyo_no = @jyuusyono+1")
        sql.AppendLine(",renkei_siji_cd=9")
        sql.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and jyuusyo_no = @jyuusyo_no")

        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, jyuusyo_no))

        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)


        Dim sql1 As New StringBuilder
        sql1.AppendLine(" DECLARE jyuusyoCursor cursor scroll dynamic")
        sql1.AppendLine("  for")
        sql1.AppendLine("  select")
        sql1.AppendLine(" 	jyuusyo_no")
        sql1.AppendLine(" from ")
        sql1.AppendLine(" m_kameiten_jyuusyo_renkei")
        sql1.AppendLine(" where")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" and jyuusyo_no>@jyuusyo_no")
        sql1.AppendLine(" OPEN jyuusyoCursor")
        sql1.AppendLine(" DECLARE @jyuusyoNo int")
        sql1.AppendLine(" FETCH next from jyuusyoCursor into @jyuusyoNo")
        sql1.AppendLine(" WHILE(@@FETCH_status=0)")
        sql1.AppendLine(" BEGIN")
        sql1.AppendLine(" 	update m_kameiten_jyuusyo_renkei ")
        sql1.AppendLine(" 	set jyuusyo_no = @jyuusyoNo-1")
        sql1.AppendLine(" 	,renkei_siji_cd = case renkei_siji_cd when '9' then '9' else '2' end")
        sql1.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        sql1.Append(",upd_login_user_id	=	@upd_login_user_id")
        sql1.Append(",upd_datetime	=	getdate()")
        sql1.AppendLine(" where")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" and jyuusyo_no=@jyuusyoNo")
        sql1.AppendLine(" 	FETCH next from jyuusyoCursor into @jyuusyoNo")
        sql1.AppendLine(" END")
        sql1.AppendLine(" close jyuusyoCursor")
        sql1.AppendLine(" deallocate jyuusyoCursor")

        ExecuteNonQuery(connStr, _
                                           CommandType.Text, _
                                           sql1.ToString(), _
                                           paramList.ToArray)

        Return intResult


    End Function

    ''' <summary>
    ''' �����X�Z���A�g�ǉ�
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="jyuusyo_no">�Z��No</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsKameitenjyushorenkei(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer, ByVal upd_login_user_id As String) As Integer
        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim i As Integer
        For i = 1 To jyuusyo_no - 1
            If SelKameitenJyushorenkei(kameiten_cd, i) Then
            Else
                InsKameitenjyushorenkei2(2, kameiten_cd, i, upd_login_user_id)
            End If
        Next

        '�V�K�ǉ�
        sql.AppendLine(" INSERT INTO m_kameiten_jyuusyo_renkei WITH(UPDLOCK) (")
        sql.AppendLine("kameiten_cd")           '	�����X����
        sql.AppendLine(",jyuusyo_no")           '	�Z��NO
        sql.AppendLine(",renkei_siji_cd")         '	�Z��1
        sql.AppendLine(",sousin_jyky_cd")         '	�Z��2
        sql.AppendLine(",sousin_kanry_datetime")            '	�X�֔ԍ�
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime")
        sql.AppendLine(") VALUES ( ")
        sql.AppendLine("@kameiten_cd")           '	�����X����
        sql.AppendLine(",@jyuusyo_no")
        sql.AppendLine(",@renkei_siji_cd")
        sql.AppendLine(",@sousin_jyky_cd")
        sql.AppendLine(",@sousin_kanry_datetime")
        sql.AppendLine(",@upd_login_user_id")
        sql.AppendLine(",getdate())")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, jyuusyo_no))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 10, DBNull.Value))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))



        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)



        Return intResult
    End Function

    ''' <summary>
    ''' �����X�Z���A�g�ǉ�
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="jyuusyo_no">�Z��No</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsKameitenjyushorenkei2(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer, ByVal upd_login_user_id As String) As Integer
        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("DECLARE @updatetime datetime")
        sql.AppendLine("DECLARE @upd_login_user_id VARCHAR(200) ")
        sql.AppendLine("set @updatetime = (select  max(upd_datetime) from m_kameiten_jyuusyo Where kameiten_cd = @kameiten_cd and jyuusyo_no=@jyuusyo_no)")
        sql.AppendLine("set @upd_login_user_id = (select  max(upd_login_user_id) from m_kameiten_jyuusyo Where kameiten_cd = @kameiten_cd and jyuusyo_no=@jyuusyo_no)")

        '�V�K�ǉ�
        sql.AppendLine(" INSERT INTO m_kameiten_jyuusyo_renkei WITH(UPDLOCK) (")
        sql.AppendLine("kameiten_cd")           '	�����X����
        sql.AppendLine(",jyuusyo_no")
        sql.AppendLine(",renkei_siji_cd")
        sql.AppendLine(",sousin_jyky_cd")
        sql.AppendLine(",sousin_kanry_datetime")            '	�X�֔ԍ�
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime")
        sql.AppendLine(") VALUES ( ")
        sql.AppendLine("@kameiten_cd")           '	�����X����
        sql.AppendLine(",@jyuusyo_no")
        sql.AppendLine(",@renkei_siji_cd")
        sql.AppendLine(",@sousin_jyky_cd")
        sql.AppendLine(",@sousin_kanry_datetime")
        sql.AppendLine(",@upd_login_user_id")
        sql.AppendLine(",@updatetime)")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, jyuusyo_no))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 10, DBNull.Value))


        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        Return intResult
    End Function

    ''' <summary>
    ''' �����X�̑��t��Flg�X�V
    ''' </summary>
    ''' <param name="kameiten_cd">�����XCd</param>
    ''' <param name="index">index</param>
    ''' <param name="value">value</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updKameitenjyushoFlg(ByVal kameiten_cd As String, ByVal upd_login_user_id As String, ByVal index As Integer, ByVal value As Integer)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim sql2 As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" UPDATE  m_kameiten_jyuusyo WITH(UPDLOCK) set ")
        If index = 1 Then
            sql.AppendLine("seikyuusyo_flg=@seikyuusyo_flg")
        ElseIf index = 2 Then
            sql.AppendLine("hosyousyo_flg=@hosyousyo_flg")
        ElseIf index = 3 Then
            sql.AppendLine("hkks_flg=@hkks_flg")
        ElseIf index = 4 Then
            sql.AppendLine("teiki_kankou_flg=@teiki_kankou_flg")
        ElseIf index = 5 Then
            sql.AppendLine("kasi_hosyousyo_flg=@kasi_hosyousyo_flg")
        ElseIf index = 6 Then
            sql.AppendLine("koj_hkks_flg=@koj_hkks_flg")
        ElseIf index = 7 Then

            sql.AppendLine("kensa_hkks_flg=@kensa_hkks_flg")
        End If
        sql.AppendLine(",upd_date=convert(char(10),GetDate(),101)")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")
        sql.AppendLine("WHERE")

        If value = 0 Then
            If index = 1 Then
                sql.AppendLine("seikyuusyo_flg=@seikyuusyo_flg2")
            ElseIf index = 2 Then
                sql.AppendLine(" hosyousyo_flg=@hosyousyo_flg2")
            ElseIf index = 3 Then
                sql.AppendLine(" hkks_flg=@hkks_flg2")
            ElseIf index = 4 Then
                sql.AppendLine(" teiki_kankou_flg=@teiki_kankou_flg2")
            ElseIf index = 5 Then
                sql.AppendLine(" kasi_hosyousyo_flg=@kasi_hosyousyo_flg2")
            ElseIf index = 6 Then
                sql.AppendLine(" koj_hkks_flg=@koj_hkks_flg2")
            ElseIf index = 7 Then
                sql.AppendLine(" kensa_hkks_flg=@kensa_hkks_flg2")
            End If

        Else
            sql.AppendLine(" 1=1 ")
        End If

        sql.AppendLine(" and   kameiten_cd = @kameiten_cd")

        If value = -1 Then
            sql.AppendLine("    and jyuusyo_no = @jyuusyo_no")

        End If

        paramList.Add(MakeParam("@seikyuusyo_flg", SqlDbType.Decimal, 2, value))
        paramList.Add(MakeParam("@hosyousyo_flg", SqlDbType.Decimal, 2, value))
        paramList.Add(MakeParam("@hkks_flg", SqlDbType.Decimal, 2, value))
        paramList.Add(MakeParam("@teiki_kankou_flg", SqlDbType.Decimal, 2, value))
        paramList.Add(MakeParam("@kasi_hosyousyo_flg", SqlDbType.Decimal, 2, value))
        paramList.Add(MakeParam("@koj_hkks_flg", SqlDbType.Decimal, 2, value))
        paramList.Add(MakeParam("@kensa_hkks_flg", SqlDbType.Decimal, 2, value))


        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, 1))
        ' �N�G�����s
        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' �p�����[�^�i�[

        sql2.AppendLine("SELECT  jyuusyo_no  ")
        sql2.AppendLine("FROM            ")
        sql2.AppendLine("    m_kameiten_jyuusyo  WITH (READCOMMITTED)")
        sql2.AppendLine("WHERE")
        If index = 1 Then
            sql2.AppendLine("seikyuusyo_flg=@seikyuusyo_flg2")
        ElseIf index = 2 Then
            sql2.AppendLine(" hosyousyo_flg=@hosyousyo_flg2")
        ElseIf index = 3 Then
            sql2.AppendLine(" hkks_flg=@hkks_flg2")
        ElseIf index = 4 Then
            sql2.AppendLine(" teiki_kankou_flg=@teiki_kankou_flg2")
        ElseIf index = 5 Then
            sql2.AppendLine(" kasi_hosyousyo_flg=@kasi_hosyousyo_flg2")
        ElseIf index = 6 Then
            sql2.AppendLine(" koj_hkks_flg=@koj_hkks_flg2")
        ElseIf index = 7 Then
            sql2.AppendLine(" kensa_hkks_flg=@kensa_hkks_flg2")
        End If
        sql2.AppendLine("   and  kameiten_cd = @kameiten_cd")


        paramList.Add(MakeParam("@seikyuusyo_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@hosyousyo_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@hkks_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@teiki_kankou_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@kasi_hosyousyo_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@koj_hkks_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@kensa_hkks_flg2", SqlDbType.Decimal, 2, -1))


        ' �������s
        FillDataset(connStr, CommandType.Text, sql2.ToString(), data, _
                    "jyuusyoNo", paramList.ToArray)

        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)




        Dim jyuusyoNo As String = String.Empty
        If data.Tables(0).Rows.Count > 0 Then
            jyuusyoNo = data.Tables(0).Rows(0).Item(0).ToString
        End If

        If SelKameitenJyushorenkei(kameiten_cd, jyuusyoNo) Then
            updKameitenjyushorenkei(2, kameiten_cd, jyuusyoNo, upd_login_user_id)
        Else
            InsKameitenjyushorenkei(2, kameiten_cd, jyuusyoNo, upd_login_user_id)
        End If

        If value = -1 Then
            If SelKameitenJyushorenkei(kameiten_cd, 1) Then
                updKameitenjyushorenkei(2, kameiten_cd, 1, upd_login_user_id)
            Else
                InsKameitenjyushorenkei(2, kameiten_cd, 1, upd_login_user_id)
            End If
        End If

        Return intResult

    End Function

    ''' <summary>
    ''' �����X�̑��t��Flg�X�V
    ''' </summary>
    ''' <param name="kameiten_cd">�����XCd</param>
    ''' <param name="index">index</param>
    ''' <param name="jyuusyo_no">�Z��No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updKameitenjyushoFlg2(ByVal kameiten_cd As String, ByVal upd_login_user_id As String, ByVal index As Integer, ByVal jyuusyo_no As Integer)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim sql2 As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)


        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' �p�����[�^�i�[

        sql2.AppendLine("SELECT  jyuusyo_no  ")
        sql2.AppendLine("FROM            ")
        sql2.AppendLine("    m_kameiten_jyuusyo  WITH (READCOMMITTED)")
        sql2.AppendLine("WHERE")
        If index = 1 Then
            sql2.AppendLine("seikyuusyo_flg=@seikyuusyo_flg2")
        ElseIf index = 2 Then
            sql2.AppendLine(" hosyousyo_flg=@hosyousyo_flg2")
        ElseIf index = 3 Then
            sql2.AppendLine(" hkks_flg=@hkks_flg2")
        ElseIf index = 4 Then
            sql2.AppendLine(" teiki_kankou_flg=@teiki_kankou_flg2")
        ElseIf index = 5 Then
            sql2.AppendLine(" kasi_hosyousyo_flg=@kasi_hosyousyo_flg2")
        ElseIf index = 6 Then
            sql2.AppendLine(" koj_hkks_flg=@koj_hkks_flg2")
        ElseIf index = 7 Then
            sql2.AppendLine(" kensa_hkks_flg=@kensa_hkks_flg2")
        End If
        sql2.AppendLine("   and  kameiten_cd = @kameiten_cd")


        paramList.Add(MakeParam("@seikyuusyo_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@hosyousyo_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@hkks_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@teiki_kankou_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@kasi_hosyousyo_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@koj_hkks_flg2", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@kensa_hkks_flg2", SqlDbType.Decimal, 2, -1))
        '�V�K�ǉ�
        sql.AppendLine(" UPDATE  m_kameiten_jyuusyo WITH(UPDLOCK) set ")
        If index = 1 Then
            sql.AppendLine("seikyuusyo_flg=@seikyuusyo_flg")
        ElseIf index = 2 Then
            sql.AppendLine("hosyousyo_flg=@hosyousyo_flg")
        ElseIf index = 3 Then
            sql.AppendLine("hkks_flg=@hkks_flg")
        ElseIf index = 4 Then
            sql.AppendLine("teiki_kankou_flg=@teiki_kankou_flg")
        ElseIf index = 5 Then
            sql.AppendLine("kasi_hosyousyo_flg=@kasi_hosyousyo_flg")
        ElseIf index = 6 Then
            sql.AppendLine("koj_hkks_flg=@koj_hkks_flg")
        ElseIf index = 7 Then

            sql.AppendLine("kensa_hkks_flg=@kensa_hkks_flg")
        End If
        sql.AppendLine(",upd_date=convert(char(10),GetDate(),101)")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")
        sql.AppendLine("WHERE")
        sql.AppendLine(" kameiten_cd = @kameiten_cd")
        sql.AppendLine(" and jyuusyo_no = @jyuusyo_no")


        paramList.Add(MakeParam("@seikyuusyo_flg", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@hosyousyo_flg", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@hkks_flg", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@teiki_kankou_flg", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@kasi_hosyousyo_flg", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@koj_hkks_flg", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@kensa_hkks_flg", SqlDbType.Decimal, 2, -1))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, 1))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql2.ToString(), data, _
                    "jyuusyoNo", paramList.ToArray)

        Dim jyuusyoNo As String = String.Empty
        If data.Tables(0).Rows.Count > 0 Then
            jyuusyoNo = data.Tables(0).Rows(0).Item(0).ToString
        End If

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        If SelKameitenJyushorenkei(kameiten_cd, jyuusyoNo) Then
            updKameitenjyushorenkei(2, kameiten_cd, jyuusyoNo, upd_login_user_id)
        Else
            InsKameitenjyushorenkei(2, kameiten_cd, jyuusyoNo, upd_login_user_id)
        End If

        If SelKameitenJyushorenkei(kameiten_cd, 1) Then
            updKameitenjyushorenkei(2, kameiten_cd, 1, upd_login_user_id)
        Else
            InsKameitenjyushorenkei(2, kameiten_cd, 1, upd_login_user_id)
        End If

        Return intResult

    End Function

    ''' <summary>
    ''' �����X�Z�����ǉ�
    ''' </summary>
    ''' <param name="kameiten_cd">�����XCd</param>
    ''' <param name="jyuusyo_no">�Z��No</param>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsKameitenjyusho(ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer, ByVal data As KameitenjyushoDataSet) As Integer
        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" INSERT INTO m_kameiten_jyuusyo WITH(UPDLOCK) (")
        sql.AppendLine("kameiten_cd")           '	�����X����
        sql.AppendLine(",jyuusyo_no")           '	�Z��NO
        sql.AppendLine(",jyuusyo1")         '	�Z��1
        sql.AppendLine(",jyuusyo2")         '	�Z��2
        sql.AppendLine(",yuubin_no")            '	�X�֔ԍ�
        sql.AppendLine(",tel_no")           '	�d�b�ԍ�
        sql.AppendLine(",fax_no")           '	FAX�ԍ�
        sql.AppendLine(",busyo_mei")            '	������
        sql.AppendLine(",daihyousya_mei")           '	��\�Җ�
        sql.AppendLine(",add_nengetu")          '	�o�^�N��
        sql.AppendLine(",seikyuusyo_flg")           '	������FLG
        sql.AppendLine(",hosyousyo_flg")            '	�ۏ؏�FLG
        sql.AppendLine(",hkks_flg")         '	�񍐏�FLG
        sql.AppendLine(",teiki_kankou_flg")         '	������sFLG
        sql.AppendLine(",bikou1")           '	���l1
        sql.AppendLine(",bikou2")           '	���l2

        sql.AppendLine(",upd_date")

        sql.AppendLine(",mail_address")         '	Ұٱ��ڽ
        sql.AppendLine(",kasi_hosyousyo_flg")           '	���r�ۏ؏�FLG
        sql.AppendLine(",koj_hkks_flg")         '	�H���񍐏�FLG
        sql.AppendLine(",kensa_hkks_flg")           '	�����񍐏�FLG
        sql.AppendLine(",add_login_user_id")
        sql.AppendLine(",add_datetime")
        sql.AppendLine(",upd_login_user_id	")
        sql.AppendLine(",upd_datetime	")
        sql.AppendLine(",todouhuken_cd	")
        '�Z��NO���Ƃ�SQL�����쐬����
        Select Case jyuusyo_no
            Case 1
                sql.AppendLine(") VALUES ( ")
                sql.AppendLine("@kameiten_cd")           '	�����X����
                sql.AppendLine(",@jyuusyo_no")           '	�Z��NO
                sql.AppendLine(",@jyuusyo1")         '	�Z��1
                sql.AppendLine(",@jyuusyo2")         '	�Z��2
                sql.AppendLine(",@yuubin_no")            '	�X�֔ԍ�

                sql.AppendLine(",@tel_no")           '	�d�b�ԍ�
                sql.AppendLine(",@fax_no")           '	FAX�ԍ�
                sql.AppendLine(",@busyo_mei")            '	������
                sql.AppendLine(",@daihyousya_mei")           '	��\�Җ�
                sql.AppendLine(",@add_nengetu")          '	�o�^�N��

                sql.AppendLine(",@seikyuusyo_flg")           '	������FLG
                sql.AppendLine(",@hosyousyo_flg")            '	�ۏ؏�FLG
                sql.AppendLine(",@hkks_flg")         '	�񍐏�FLG
                sql.AppendLine(",@teiki_kankou_flg")         '	������sFLG
                sql.AppendLine(",@bikou1")           '	���l1

                sql.AppendLine(",@bikou2")           '	���l2
                sql.AppendLine(",convert(char(10),GetDate(),101)")

                sql.AppendLine(",@mail_address")         '	Ұٱ��ڽ
                sql.AppendLine(",@kasi_hosyousyo_flg")           '	���r�ۏ؏�FLG
                sql.AppendLine(",@koj_hkks_flg")         '	�H���񍐏�FLG
                sql.AppendLine(",@kensa_hkks_flg")           '	�����񍐏�FLG
                sql.AppendLine(",@add_login_user_id")
                sql.AppendLine(",getdate()")
                sql.AppendLine(",@add_login_user_id")
                sql.AppendLine(",getdate()")
                sql.AppendLine(",@todouhuken_cd)")
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, data.m_kameiten_jyuusyo(0).kameiten_cd))
                paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, data.m_kameiten_jyuusyo(0).jyuusyo_no))
                paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, data.m_kameiten_jyuusyo(0).jyuusyo1))
                paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).jyuusyo2))
                paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, data.m_kameiten_jyuusyo(0).yuubin_no))

                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, data.m_kameiten_jyuusyo(0).tel_no))
                paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, data.m_kameiten_jyuusyo(0).fax_no))
                paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, data.m_kameiten_jyuusyo(0).busyo_mei))
                paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, data.m_kameiten_jyuusyo(0).daihyousya_mei))
                paramList.Add(MakeParam("@add_nengetu", SqlDbType.VarChar, 7, data.m_kameiten_jyuusyo(0).add_nengetu))

                paramList.Add(MakeParam("@seikyuusyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).seikyuusyo_flg))
                paramList.Add(MakeParam("@hosyousyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).hosyousyo_flg))
                paramList.Add(MakeParam("@hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).hkks_flg))
                paramList.Add(MakeParam("@teiki_kankou_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).teiki_kankou_flg))
                paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).bikou1))

                paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).bikou2))
                paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, data.m_kameiten_jyuusyo(0).mail_address))
                paramList.Add(MakeParam("@kasi_hosyousyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg))
                paramList.Add(MakeParam("@koj_hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).koj_hkks_flg))
                paramList.Add(MakeParam("@kensa_hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).kensa_hkks_flg))
                paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).add_login_user_id))
                ' paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 10, insData(0).add_datetime))
                paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).upd_login_user_id))
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, data.m_kameiten_jyuusyo(0).todouhuken_cd))

                ' paramList.Add(MakeParam("@upd_datetime", SqlDbType.DateTime, 10, insData(0).upd_datetime))
                ' �N�G�����s
                intResult = ExecuteNonQuery(connStr, _
                                            CommandType.Text, _
                                            sql.ToString(), _
                                            paramList.ToArray)
            Case 2, 3, 4, 5, 6, 7, 8
                sql.AppendLine(" )VALUES ( ")
                sql.AppendLine("@kameiten_cd")           '	�����X����
                sql.AppendLine(",@jyuusyo_no")           '	�Z��NO
                sql.AppendLine(",@jyuusyo1")         '	�Z��1
                sql.AppendLine(",@jyuusyo2")         '	�Z��2
                sql.AppendLine(",@yuubin_no")            '	�X�֔ԍ�

                sql.AppendLine(",@tel_no")           '	�d�b�ԍ�
                sql.AppendLine(",@fax_no")           '	FAX�ԍ�
                sql.AppendLine(",@busyo_mei")            '	������
                sql.AppendLine(",@daihyousya_mei")           '	��\�Җ�
                sql.AppendLine(",@add_nengetu")          '	�o�^�N��

                sql.AppendLine(",@seikyuusyo_flg")           '	������FLG
                sql.AppendLine(",@hosyousyo_flg")            '	�ۏ؏�FLG
                sql.AppendLine(",@hkks_flg")         '	�񍐏�FLG
                sql.AppendLine(",@teiki_kankou_flg")         '	������sFLG
                sql.AppendLine(",@bikou1")           '	���l1

                sql.AppendLine(",@bikou2")           '	���l2

                sql.AppendLine(",convert(char(10),GetDate(),101)")
                sql.AppendLine(",@mail_address")         '	Ұٱ��ڽ
                sql.AppendLine(",@kasi_hosyousyo_flg")           '	���r�ۏ؏�FLG
                sql.AppendLine(",@koj_hkks_flg")         '	�H���񍐏�FLG
                sql.AppendLine(",@kensa_hkks_flg")           '	�����񍐏�FLG
                sql.AppendLine(",@add_login_user_id")
                sql.AppendLine(",getdate()")
                sql.AppendLine(",@add_login_user_id")
                sql.AppendLine(",getdate()")
                sql.AppendLine(",@todouhuken_cd)")
                paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).add_login_user_id))
                paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).upd_login_user_id))
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, data.m_kameiten_jyuusyo(0).kameiten_cd))
                paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, data.m_kameiten_jyuusyo(0).jyuusyo_no))
                paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, data.m_kameiten_jyuusyo(0).jyuusyo1))
                paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).jyuusyo2))
                paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, data.m_kameiten_jyuusyo(0).yuubin_no))

                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, data.m_kameiten_jyuusyo(0).tel_no))
                paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, data.m_kameiten_jyuusyo(0).fax_no))
                paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, data.m_kameiten_jyuusyo(0).busyo_mei))
                paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, data.m_kameiten_jyuusyo(0).daihyousya_mei))
                paramList.Add(MakeParam("@add_nengetu", SqlDbType.VarChar, 7, DBNull.Value))

                paramList.Add(MakeParam("@seikyuusyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).seikyuusyo_flg))
                paramList.Add(MakeParam("@hosyousyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).hosyousyo_flg))
                paramList.Add(MakeParam("@hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).hkks_flg))
                paramList.Add(MakeParam("@teiki_kankou_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).teiki_kankou_flg))

                paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, data.m_kameiten_jyuusyo(0).bikou1))
                paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, DBNull.Value))

                paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, DBNull.Value))
                paramList.Add(MakeParam("@kasi_hosyousyo_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg))
                paramList.Add(MakeParam("@koj_hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).koj_hkks_flg))
                paramList.Add(MakeParam("@kensa_hkks_flg", SqlDbType.Decimal, 1, data.m_kameiten_jyuusyo(0).kensa_hkks_flg))
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, data.m_kameiten_jyuusyo(0).todouhuken_cd))




                ' �N�G�����s
                intResult = ExecuteNonQuery(connStr, _
                                            CommandType.Text, _
                                            sql.ToString(), _
                                            paramList.ToArray)



        End Select
        If SelKameitenJyushorenkei(kameiten_cd, jyuusyo_no) Then
            updKameitenjyushorenkei(1, kameiten_cd, jyuusyo_no, data.m_kameiten_jyuusyo(0).upd_login_user_id)
        Else
            InsKameitenjyushorenkei(1, kameiten_cd, jyuusyo_no, data.m_kameiten_jyuusyo(0).upd_login_user_id)
        End If
        Return intResult
    End Function

    ''' <summary>
    ''' �����X�Z���폜
    ''' </summary>
    ''' <param name="kameiten_cd"></param>
    ''' <param name="jyuusyo_no"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelKameitenjyusho(ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer, ByVal upd_login_user_id As String) As Integer

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" DELETE FROM m_kameiten_jyuusyo WITH(UPDLOCK)")

        sql.AppendLine("WHERE")
        sql.AppendLine(" kameiten_cd = @kameiten_cd")
        sql.AppendLine(" and jyuusyo_no = @jyuusyo_no")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Decimal, 2, jyuusyo_no))

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)


        Dim sql1 As New StringBuilder
        sql1.AppendLine(" DECLARE jyuusyoCursor cursor scroll dynamic")
        sql1.AppendLine("  for")
        sql1.AppendLine("  select")
        sql1.AppendLine(" 	jyuusyo_no")
        sql1.AppendLine(" from ")
        sql1.AppendLine(" m_kameiten_jyuusyo")
        sql1.AppendLine(" where")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" and jyuusyo_no>@jyuusyo_no")
        sql1.AppendLine(" OPEN jyuusyoCursor")
        sql1.AppendLine(" DECLARE @jyuusyoNo int")
        sql1.AppendLine(" FETCH next from jyuusyoCursor into @jyuusyoNo")
        sql1.AppendLine(" WHILE(@@FETCH_status=0)")
        sql1.AppendLine(" BEGIN")
        sql1.AppendLine(" 	update m_kameiten_jyuusyo ")
        sql1.AppendLine(" 	set jyuusyo_no = @jyuusyoNo-1")
        sql1.Append(",upd_login_user_id	=	@upd_login_user_id")
        sql1.Append(",upd_datetime	=	getdate()")
        sql1.AppendLine(" where")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" and jyuusyo_no=@jyuusyoNo")
        sql1.AppendLine(" 	FETCH next from jyuusyoCursor into @jyuusyoNo")
        sql1.AppendLine(" END")
        sql1.AppendLine(" close jyuusyoCursor")
        sql1.AppendLine(" deallocate jyuusyoCursor")
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))
        ExecuteNonQuery(connStr, _
                                           CommandType.Text, _
                                           sql1.ToString(), _
                                           paramList.ToArray)

        If SelKameitenJyushorenkei(kameiten_cd, jyuusyo_no) Then
            updKameitenjyushorenkeiDel(2, kameiten_cd, jyuusyo_no, upd_login_user_id)
        Else
            InsKameitenjyushorenkei(9, kameiten_cd, jyuusyo_no, upd_login_user_id)
            updKameitenjyushorenkeiDel(2, kameiten_cd, jyuusyo_no, upd_login_user_id)
        End If

        Return intResult

    End Function

    ''' <summary>
    ''' �X�ʏ��������擾
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="bunrui_cd">���ރR�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelTenbetuSyokiSeikyu(ByVal kameiten_cd As String, ByVal bunrui_cd As String) As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable
        ' DataSet�C���X�^���X�̐���
        Dim KameitenjyushoDataSet As New KameitenjyushoDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("mise_cd")
        sql.AppendLine(",bunrui_cd")
        sql.AppendLine(",add_date")
        sql.AppendLine(",seikyuusyo_hak_date")
        sql.AppendLine(",uri_date")
        sql.AppendLine(",seikyuu_umu")
        sql.AppendLine(",uri_keijyou_flg")
        sql.AppendLine(",uri_keijyou_date")
        sql.AppendLine(",syouhin_cd")
        sql.AppendLine(",uri_gaku")
        sql.AppendLine(",zei_kbn")
        sql.AppendLine(",bikou")
        sql.AppendLine(",koumuten_seikyuu_gaku")
        sql.AppendLine(",add_login_user_id")
        sql.AppendLine(",add_datetime")
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime")
        sql.AppendLine(",syouhizei_gaku")
        sql.AppendLine("FROM            ")
        sql.AppendLine("    t_tenbetu_syoki_seikyuu WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    mise_cd = @kameiten_cd")
        sql.AppendLine("    And bunrui_cd = @bunrui_cd")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, bunrui_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), KameitenjyushoDataSet, _
                    KameitenjyushoDataSet.t_tenbetu_syoki_seikyuu.TableName, paramList.ToArray)

        Return KameitenjyushoDataSet.t_tenbetu_syoki_seikyuu



    End Function

    ''' <summary>
    ''' ���ߏ��
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="kbn">�敪</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <histroy>�n���R 2010/08/17 �������ߓ��擾���@��ύX����</histroy>
    Public Function SelSimeDate(ByVal kameiten_cd As String, _
                                              ByVal kbn As String) As String

        ' DataSet�C���X�^���X�̐���
        Dim simedateDataSet As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")

        'If kbn = SEIKYUUSIMEDATE Then
        '    '�����������ߓ����擾
        '    sql.AppendLine("    tys_seikyuu_sime_date")
        'Else
        '    '�̑��i�������ߓ����擾
        '    sql.AppendLine("    hansokuhin_seikyuu_sime_date")
        'End If
        sql.AppendLine("    m_seikyuu_saki.seikyuu_sime_date ")

        sql.AppendLine("FROM            ")
        sql.AppendLine("    m_kameiten WITH (READCOMMITTED)")
        If kbn = SEIKYUUSIMEDATE Then
            sql.AppendLine("LEFT JOIN m_seikyuu_saki (READCOMMITTED) ")
            sql.AppendLine("ON ")
            sql.AppendLine("    m_kameiten.tys_seikyuu_saki_cd = m_seikyuu_saki.seikyuu_saki_cd ")
            sql.AppendLine("AND ")
            sql.AppendLine("    m_kameiten.tys_seikyuu_saki_brc = m_seikyuu_saki.seikyuu_saki_brc ")
            sql.AppendLine("AND ")
            sql.AppendLine("    m_kameiten.tys_seikyuu_saki_kbn = m_seikyuu_saki.seikyuu_saki_kbn ")
        Else
            sql.AppendLine("LEFT JOIN m_seikyuu_saki (READCOMMITTED) ")
            sql.AppendLine("ON ")
            sql.AppendLine("    m_kameiten.hansokuhin_seikyuu_saki_cd = m_seikyuu_saki.seikyuu_saki_cd ")
            sql.AppendLine("AND ")
            sql.AppendLine("    m_kameiten.hansokuhin_seikyuu_saki_brc = m_seikyuu_saki.seikyuu_saki_brc ")
            sql.AppendLine("AND ")
            sql.AppendLine("    m_kameiten.hansokuhin_seikyuu_saki_kbn = m_seikyuu_saki.seikyuu_saki_kbn ")
        End If

        '�n���R 2010/08/17 �o�^���A�̑��i�����c�[�����̐��������s���v�Z�̕ύX ��
        sql.AppendLine("WHERE")
        sql.AppendLine("    m_kameiten.kameiten_cd = @kameiten_cd")


        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))


        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), simedateDataSet, _
                    "simedateDataTable", paramList.ToArray)

        If simedateDataSet.Tables(0).Rows.Count > 0 Then
            Return simedateDataSet.Tables(0).Rows(0).Item(0).ToString
        Else
            Return String.Empty
        End If

    End Function

    ''' <summary>
    ''' �e�����}�X�^����{��(TH)�������i����ъ|�����擾
    ''' </summary>
    ''' <param name="item">item</param>
    ''' <param name="table">table</param>
    ''' <param name="key">key</param>
    ''' <param name="shouhin">���i</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelKakaku(ByVal item As String, _
                                                ByVal table As String, _
                                                ByVal key As String, _
                                                ByVal shouhin As String, _
                                                ByVal kbn As String, _
                                                ByVal jitu As Long, _
                                                ByVal koumuten As Long) As String

        ' DataSet�C���X�^���X�̐���
        Dim KakakuDataSet As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT " & item & ",kakeritu")
        sql.AppendLine(" FROM " & table)
        sql.AppendLine("  WITH (READCOMMITTED) WHERE " & key & " = '" & shouhin & "'")

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), KakakuDataSet, _
                    "KakakuDataTable", paramList.ToArray)



        If KakakuDataSet.Tables(0).Rows.Count > 0 Then

            '�}�X�^�ɑΏۏ��i���ނ����݂���ꍇ
            Select Case kbn
                Case "B"
                    '���������z�ύX��
                    '�H���X�������z�����������z / �|��
                    Return Fix(jitu / IIf(KakakuDataSet.Tables(0).Rows(0).Item(1) Is DBNull.Value, 0, KakakuDataSet.Tables(0).Rows(0).Item(1)))


                Case "A"
                    '���i�R�[�h�E�����L���ύX��
                    '���������z���e�����}�X�^�̖{��(TH)�������i
                    Return IIf(KakakuDataSet.Tables(0).Rows(0).Item(0) Is DBNull.Value, 0, KakakuDataSet.Tables(0).Rows(0).Item(0))
                Case Else
                    '�H���X�������z�ύX��
                    '���������z���H���X�������z * �|��
                    Return Fix(koumuten * IIf(KakakuDataSet.Tables(0).Rows(0).Item(1) Is DBNull.Value, 0, KakakuDataSet.Tables(0).Rows(0).Item(1)))

            End Select
        Else
            Return String.Empty
        End If

    End Function

    ''' <summary>
    ''' ����Ń}�X�^���擾����
    ''' </summary>
    ''' <param name="zei_kbn">�ŋ敪</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelKakaku(ByVal zei_kbn As String) As String

        ' DataSet�C���X�^���X�̐���
        Dim dataset As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine(" SELECT zeiritu FROM m_syouhizei  WITH (READCOMMITTED) ")
        sql.AppendLine(" WHERE zei_kbn = @zei_kbn")
        paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, zei_kbn))


        FillDataset(connStr, CommandType.Text, sql.ToString(), dataset, _
                    "DataTable", paramList.ToArray)

        If dataset.Tables(0).Rows.Count > 0 Then
            Return dataset.Tables(0).Rows(0).Item(0).ToString()
        Else
            Return "0"
        End If

    End Function

    ''' <summary>
    ''' ���i���擾
    ''' </summary>
    ''' <param name="syouhin_cd">���iCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSyouhin(ByVal syouhin_cd As String, ByVal souko_cd As String) As KameitenjyushoDataSet.m_syouhinDataTable
        ' DataSet�C���X�^���X�̐���
        Dim Syouhin As New KameitenjyushoDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("SELECT          ")
        sql.AppendLine("syouhin_cd")
        sql.AppendLine(",syouhin_mei")
        sql.AppendLine(",tani")
        sql.AppendLine(",seikyuusaki_kbn")
        sql.AppendLine(",zei_kbn")
        sql.AppendLine(",zeikomi_kbn")
        sql.AppendLine(",hyoujun_kkk")
        sql.AppendLine(",keiretu_cd")
        sql.AppendLine(",builder_seikyuugaku")
        sql.AppendLine(",souko_cd")
        sql.AppendLine(",siire_kkk")
        sql.AppendLine(",add_login_user_id")
        sql.AppendLine(",add_datetime")
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime")
        sql.AppendLine(" from m_syouhin  WITH (READCOMMITTED) ")

        sql.AppendLine(" WHERE ")
        sql.AppendLine("    syouhin_cd = @syouhin_cd")
        sql.AppendLine("    and souko_cd = @souko_cd")
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, syouhin_cd))
        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, souko_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), Syouhin, _
                    Syouhin.m_syouhin.TableName.ToString, paramList.ToArray)

        Return Syouhin.m_syouhin

    End Function

    ''' <summary>
    ''' ���i���擾
    ''' </summary>
    ''' <param name="syouhin_cd">���iCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSyouhin(ByVal syouhin_cd As String) As KameitenjyushoDataSet.m_syouhinDataTable
        ' DataSet�C���X�^���X�̐���
        Dim Syouhin As New KameitenjyushoDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("SELECT          ")
        sql.AppendLine("syouhin_cd")
        sql.AppendLine(",syouhin_mei")
        sql.AppendLine(",tani")
        sql.AppendLine(",seikyuusaki_kbn")
        sql.AppendLine(",zei_kbn")
        sql.AppendLine(",zeikomi_kbn")
        sql.AppendLine(",hyoujun_kkk")
        sql.AppendLine(",keiretu_cd")
        sql.AppendLine(",builder_seikyuugaku")
        sql.AppendLine(",souko_cd")
        sql.AppendLine(",siire_kkk")
        sql.AppendLine(",add_login_user_id")
        sql.AppendLine(",add_datetime")
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime")
        sql.AppendLine(" from m_syouhin  WITH (READCOMMITTED) ")
        sql.AppendLine(" WHERE ")
        sql.AppendLine("    syouhin_cd = @syouhin_cd")

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, syouhin_cd))


        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), Syouhin, _
                    Syouhin.m_syouhin.TableName.ToString, paramList.ToArray)

        Return Syouhin.m_syouhin

    End Function

    ''' <summary>
    ''' �X��Row�擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelTenbetuRowAndData(ByVal kameiten_cd As String, ByVal bunrui_cd As String)
        ' DataSet�C���X�^���X�̐���
        Dim tenbetu As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("SELECT COUNT(*) AS COUNT,Max(uri_keijyou_flg) AS ����v�� FROM t_tenbetu_syoki_seikyuu  WITH (READCOMMITTED)")

        sql.AppendLine("WHERE")
        sql.AppendLine("    mise_cd = @kameiten_cd")
        sql.AppendLine("    And bunrui_cd = @bunrui_cd")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, bunrui_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), tenbetu, _
                    "tenbetu".ToString, paramList.ToArray)

        Return tenbetu.Tables(0)

    End Function

    ''' <summary>
    ''' �X�ʏ��������ǉ�
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="insData">�ǉ��f�[�^</param>
    ''' <param name="bunrui_cd">���ރR�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsTenbetuSyokiSeikyuu(ByVal kameiten_cd As String, _
                            ByVal insData As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable, _
                            ByVal bunrui_cd As String) As Boolean

        Dim tenbetu As DataTable
        tenbetu = SelTenbetuRowAndData(kameiten_cd, bunrui_cd)

        If tenbetu.Rows(0).Item(0) = 0 Then

            insData(0).bunrui_cd = bunrui_cd
            insData(0).uri_keijyou_flg = 0
            insData(0).uri_keijyou_date = "null"

            InsTenbetuSyokiSeikyuu(insData)
        Else
            If tenbetu.Rows(0).Item(1) = 1 Then
                updTenbetuSyokiSeikyuuMini(kameiten_cd, bunrui_cd, insData)
            Else

                insData(0).bunrui_cd = bunrui_cd
                insData(0).uri_keijyou_flg = 0
                insData(0).uri_keijyou_date = "null"
                updTenbetuSyokiSeikyuu(kameiten_cd, insData)
            End If

        End If


        Return True

    End Function

    ''' <summary>
    ''' �X�ʏ��������ǉ�
    ''' </summary>
    ''' <param name="insData">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insTenbetuSyokiSeikyuu(ByVal insData As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" INSERT INTO t_tenbetu_syoki_seikyuu WITH(UPDLOCK) (")
        sql.AppendLine("mise_cd")
        sql.AppendLine(",bunrui_cd")
        sql.AppendLine(",add_date")
        sql.AppendLine(",seikyuusyo_hak_date")
        sql.AppendLine(",uri_date")
        '=========================2011/07/08 �ԗ� �ǉ� �J�n��=========================
        sql.AppendLine(",denpyou_uri_date")  '--�`�[����N���� = ���.<�o�^��>����N����
        '=========================2011/07/08 �ԗ� �ǉ� �I����=========================
        sql.AppendLine(",seikyuu_umu")
        sql.AppendLine(",uri_keijyou_flg")
        sql.AppendLine(",uri_keijyou_date")
        sql.AppendLine(",syouhin_cd")
        sql.AppendLine(",uri_gaku")
        sql.AppendLine(",zei_kbn")
        sql.AppendLine(",bikou")
        sql.AppendLine(",koumuten_seikyuu_gaku")
        sql.AppendLine(",add_login_user_id")
        sql.AppendLine(",add_datetime")
        sql.AppendLine(",syouhizei_gaku")
        sql.AppendLine(" )VALUES ( ")
        sql.AppendLine("@mise_cd")
        sql.AppendLine(",@bunrui_cd")
        sql.AppendLine(",@add_date")
        sql.AppendLine(",@seikyuusyo_hak_date")
        sql.AppendLine(",@uri_date")
        '=========================2011/07/08 �ԗ� �ǉ� �J�n��=========================
        sql.AppendLine(",@denpyou_uri_date")  '--�`�[����N����
        '=========================2011/07/08 �ԗ� �ǉ� �I����=========================
        sql.AppendLine(",@seikyuu_umu")
        sql.AppendLine(",@uri_keijyou_flg")
        sql.AppendLine(",@uri_keijyou_date")
        sql.AppendLine(",@syouhin_cd")
        sql.AppendLine(",@uri_gaku")
        sql.AppendLine(",@zei_kbn")
        sql.AppendLine(",@bikou")
        sql.AppendLine(",@koumuten_seikyuu_gaku")
        sql.AppendLine(",@add_login_user_id")
        sql.AppendLine(",getdate()")
        sql.AppendLine(",@syouhizei_gaku)")

        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, insData(0).mise_cd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, insData(0).bunrui_cd))

        If insData(0).add_date Is Nothing OrElse insData(0).add_date = String.Empty Then
            paramList.Add(MakeParam("@add_date", SqlDbType.DateTime, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@add_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).add_date)))
        End If

        If insData(0).seikyuusyo_hak_date Is Nothing OrElse insData(0).seikyuusyo_hak_date = String.Empty Then
            paramList.Add(MakeParam("@seikyuusyo_hak_date", SqlDbType.DateTime, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@seikyuusyo_hak_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).seikyuusyo_hak_date)))
        End If
        If insData(0).uri_date Is Nothing OrElse insData(0).uri_date = String.Empty Then
            paramList.Add(MakeParam("@uri_date", SqlDbType.DateTime, 10, DBNull.Value))
            '=========================2011/07/08 �ԗ� �ǉ� �J�n��=========================
            paramList.Add(MakeParam("@denpyou_uri_date", SqlDbType.DateTime, 10, DBNull.Value))
            '=========================2011/07/08 �ԗ� �ǉ� �I����=========================
        Else
            paramList.Add(MakeParam("@uri_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).uri_date)))
            '=========================2011/07/08 �ԗ� �ǉ� �J�n��=========================
            paramList.Add(MakeParam("@denpyou_uri_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).uri_date)))
            '=========================2011/07/08 �ԗ� �ǉ� �I����=========================
        End If

        paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.Int, 1, insData(0).seikyuu_umu))
        paramList.Add(MakeParam("@uri_keijyou_flg", SqlDbType.Int, 1, insData(0).uri_keijyou_flg))
        If insData(0).uri_keijyou_date = "null" Then
            paramList.Add(MakeParam("@uri_keijyou_date", SqlDbType.DateTime, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@uri_keijyou_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).uri_keijyou_date)))
        End If

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, insData(0).syouhin_cd))

        If insData(0).uri_gaku = String.Empty Then
            insData(0).uri_gaku = "0"
        End If
        paramList.Add(MakeParam("@uri_gaku", SqlDbType.Int, 8, insData(0).uri_gaku))

        paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, insData(0).zei_kbn))
        paramList.Add(MakeParam("@bikou", SqlDbType.VarChar, 30, insData(0).bikou))

        If insData(0).koumuten_seikyuu_gaku = String.Empty Then
            insData(0).koumuten_seikyuu_gaku = "0"
        End If
        paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.Int, 8, Convert.ToInt32(insData(0).koumuten_seikyuu_gaku)))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, insData(0).add_login_user_id))

        If insData(0).syouhizei_gaku = String.Empty Then
            insData(0).syouhizei_gaku = "0"
        End If
        paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.Int, 8, insData(0).syouhizei_gaku))
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)


        If SelTenbetuSyokiSeikyuRenkei(insData(0).mise_cd, insData(0).bunrui_cd) Then

            updTenbetuSyokiSeikyuuRenkei(1, insData(0).mise_cd, insData(0).bunrui_cd, insData(0).upd_login_user_id)
        Else
            insTenbetuSyokiSeikyuuRenkei(1, insData)
        End If
        Return intResult
    End Function

    ''' <summary>
    ''' �X�ʏ��������A�g�ǉ�
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="insData">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insTenbetuSyokiSeikyuuRenkei(ByVal renkei_siji_cd As Integer, ByVal insData As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" INSERT INTO t_tenbetu_syoki_seikyuu_renkei WITH(UPDLOCK) (")
        sql.AppendLine("mise_cd")
        sql.AppendLine(",bunrui_cd")
        sql.AppendLine(",renkei_siji_cd")
        sql.AppendLine(",sousin_jyky_cd")
        sql.AppendLine(",sousin_kanry_datetime")
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime")
        sql.AppendLine(" )VALUES ( ")
        sql.AppendLine("@mise_cd")
        sql.AppendLine(",@bunrui_cd")
        sql.AppendLine(",@renkei_siji_cd")         '	�Z��1
        sql.AppendLine(",@sousin_jyky_cd")         '	�Z��2
        sql.AppendLine(",@sousin_kanry_datetime")            '	�X�֔ԍ�
        sql.AppendLine(",@upd_login_user_id")
        sql.AppendLine(",getdate())")

        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, insData(0).mise_cd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, insData(0).bunrui_cd))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 10, DBNull.Value))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, insData(0).upd_login_user_id))

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)



        Return intResult
    End Function

    ''' <summary>
    ''' �X�ʏ��������A�g�X�V
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updTenbetuSyokiSeikyuuRenkei(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal bunrui_cd As String, ByVal upd_login_user_id As String) As Integer

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" update t_tenbetu_syoki_seikyuu_renkei WITH(UPDLOCK) set")
        sql.AppendLine("renkei_siji_cd=@renkei_siji_cd")
        sql.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")
        sql.AppendLine("WHERE")
        sql.AppendLine("    mise_cd = @kameiten_cd")
        sql.AppendLine("    and bunrui_cd = @bunrui_cd")
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, bunrui_cd))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))

        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        Return intResult

    End Function

    ''' <summary>
    ''' �X�ʏ��������A�g���擾
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="bunrui_cd">���ރR�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelTenbetuSyokiSeikyuRenkei(ByVal kameiten_cd As String, ByVal bunrui_cd As String) As Boolean
        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("mise_cd")
        sql.AppendLine("FROM            ")
        sql.AppendLine("    t_tenbetu_syoki_seikyuu_renkei WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    mise_cd = @kameiten_cd")
        sql.AppendLine("    And bunrui_cd = @bunrui_cd")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, bunrui_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                   "name", paramList.ToArray)

        If data.Tables(0).Rows.Count > 0 Then

            Return True
        Else
            Return False

        End If

    End Function

    ''' <summary>
    ''' �X�ʏ��������X�V
    ''' 
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="insData">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updTenbetuSyokiSeikyuuMini(ByVal kameiten_cd As String, _
    ByVal kbn As String, _
    ByVal insData As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable)



        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" update t_tenbetu_syoki_seikyuu WITH(UPDLOCK)  set ")
        sql.AppendLine("bikou=@bikou")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")

        sql.AppendLine("WHERE")
        sql.AppendLine("    mise_cd = @kameiten_cd")
        sql.AppendLine("    And bunrui_cd = @bunrui_cd")

        paramList.Add(MakeParam("@bikou", SqlDbType.VarChar, 30, insData(0).bikou))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, kbn))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, insData(0).upd_login_user_id))


        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        If SelTenbetuSyokiSeikyuRenkei(insData(0).mise_cd, insData(0).bunrui_cd) Then
            updTenbetuSyokiSeikyuuRenkei(2, insData(0).mise_cd, insData(0).bunrui_cd, insData(0).upd_login_user_id)
        Else
            insTenbetuSyokiSeikyuuRenkei(2, insData)
        End If



        Return intResult
    End Function

    ''' <summary>
    ''' �X�ʏ��������X�V
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="insData">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updTenbetuSyokiSeikyuu(ByVal kameiten_cd As String, ByVal insData As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable)



        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" update t_tenbetu_syoki_seikyuu WITH(UPDLOCK) set")
        sql.AppendLine("mise_cd	=	@mise_cd")
        sql.AppendLine(",bunrui_cd	=	@bunrui_cd")
        sql.AppendLine(",add_date	=	@add_date")
        sql.AppendLine(",seikyuusyo_hak_date	=	@seikyuusyo_hak_date")
        sql.AppendLine(",uri_date	=	@uri_date")
        '=========================2011/07/08 �ԗ� �ǉ� �J�n��=========================
        sql.AppendLine(",denpyou_uri_date = @denpyou_uri_date")  '--�`�[����N���� = ���.<�o�^��>����N����
        '=========================2011/07/08 �ԗ� �ǉ� �I����=========================
        sql.AppendLine(",seikyuu_umu	=	@seikyuu_umu")
        sql.AppendLine(",uri_keijyou_flg	=	@uri_keijyou_flg")
        sql.AppendLine(",uri_keijyou_date	=	@uri_keijyou_date")
        sql.AppendLine(",syouhin_cd	=	@syouhin_cd")
        sql.AppendLine(",uri_gaku	=	@uri_gaku")
        sql.AppendLine(",zei_kbn	=	@zei_kbn")
        sql.AppendLine(",bikou	=	@bikou")
        sql.AppendLine(",koumuten_seikyuu_gaku	=	@koumuten_seikyuu_gaku")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")
        sql.AppendLine(",syouhizei_gaku	=	@syouhizei_gaku")
        sql.AppendLine("WHERE")
        sql.AppendLine("    mise_cd = @kameiten_cd")
        sql.AppendLine("    And bunrui_cd = @bunrui_cd")


        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, insData(0).mise_cd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, insData(0).bunrui_cd))

        If insData(0).add_date Is Nothing OrElse insData(0).add_date = String.Empty Then
            paramList.Add(MakeParam("@add_date", SqlDbType.DateTime, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@add_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).add_date)))
        End If

        If insData(0).seikyuusyo_hak_date = String.Empty Then
            paramList.Add(MakeParam("@seikyuusyo_hak_date", SqlDbType.DateTime, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@seikyuusyo_hak_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).seikyuusyo_hak_date)))
        End If

        If insData(0).uri_date = String.Empty Then
            paramList.Add(MakeParam("@uri_date", SqlDbType.DateTime, 10, DBNull.Value))
            '=========================2011/07/08 �ԗ� �ǉ� �J�n��=========================
            paramList.Add(MakeParam("@denpyou_uri_date", SqlDbType.DateTime, 10, DBNull.Value))
            '=========================2011/07/08 �ԗ� �ǉ� �I����=========================
        Else
            paramList.Add(MakeParam("@uri_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).uri_date)))
            '=========================2011/07/08 �ԗ� �ǉ� �J�n��=========================
            paramList.Add(MakeParam("@denpyou_uri_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).uri_date)))
            '=========================2011/07/08 �ԗ� �ǉ� �I����=========================
        End If


        paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.Int, 1, insData(0).seikyuu_umu))
        paramList.Add(MakeParam("@uri_keijyou_flg", SqlDbType.Int, 1, insData(0).uri_keijyou_flg))
        If insData(0).uri_keijyou_date = "null" Then
            paramList.Add(MakeParam("@uri_keijyou_date", SqlDbType.DateTime, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@uri_keijyou_date", SqlDbType.DateTime, 10, Convert.ToDateTime(insData(0).uri_keijyou_date)))
        End If

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, insData(0).syouhin_cd))
        If insData(0).uri_gaku = String.Empty Then
            insData(0).uri_gaku = "0"
        End If
        paramList.Add(MakeParam("@uri_gaku", SqlDbType.Int, 8, insData(0).uri_gaku))

        paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, insData(0).zei_kbn))
        paramList.Add(MakeParam("@bikou", SqlDbType.VarChar, 30, insData(0).bikou))

        If insData(0).koumuten_seikyuu_gaku = String.Empty Then
            insData(0).koumuten_seikyuu_gaku = "0"
        End If
        paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.Int, 8, Convert.ToInt32(insData(0).koumuten_seikyuu_gaku)))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, insData(0).upd_login_user_id))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 3, "200"))

        If insData(0).syouhizei_gaku = String.Empty Then
            insData(0).syouhizei_gaku = "0"
        End If
        paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.Int, 8, insData(0).syouhizei_gaku))
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        If SelTenbetuSyokiSeikyuRenkei(insData(0).mise_cd, insData(0).bunrui_cd) Then

            updTenbetuSyokiSeikyuuRenkei(2, insData(0).mise_cd, insData(0).bunrui_cd, insData(0).upd_login_user_id)
        Else
            insTenbetuSyokiSeikyuuRenkei(2, insData)
        End If

        Return intResult
    End Function

    ''' <summary>
    ''' �����X���l���擾
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelkameitenBikouInfo(ByVal kameiten_cd As String) As KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable

        ' DataSet�C���X�^���X�̐���
        Dim kameitenBikouDataSet As New KameitenBikouDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("a.kameiten_cd")
        sql.AppendLine(",a.bikou_syubetu")
        sql.AppendLine(",a.nyuuryoku_no")
        sql.AppendLine(",a.naiyou")
        sql.AppendLine(",a.upd_datetime")
        sql.AppendLine(",a.add_login_user_id")
        sql.AppendLine(",a.add_datetime")
        sql.AppendLine(",a.upd_login_user_id")
        sql.AppendLine(",b.meisyou")
        sql.AppendLine(" FROM            ")
        sql.AppendLine("    m_kameiten_bikou_settei as a WITH (READCOMMITTED)")
        sql.AppendLine("  left join  m_meisyou as b WITH (READCOMMITTED)")
        sql.AppendLine(" on a.bikou_syubetu = b.code and b.meisyou_syubetu=@meisyou_syubetu ")
        sql.AppendLine("WHERE")
        sql.AppendLine("    a.kameiten_cd = @kameiten_cd order by a.nyuuryoku_no ")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "09"))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), kameitenBikouDataSet, _
                    kameitenBikouDataSet.m_kameiten_bikou_settei.TableName, paramList.ToArray)

        Return kameitenBikouDataSet.m_kameiten_bikou_settei

    End Function

    ''' <summary>
    ''' �����X���l�擾
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="bikou_syubetu">���l���</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelkameitenBikouInfo(ByVal kameiten_cd As String, ByVal bikou_syubetu As String) As Boolean

        ' DataSet�C���X�^���X�̐���
        Dim kameitenBikouDataSet As New KameitenBikouDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("a.kameiten_cd")
        sql.AppendLine(",a.bikou_syubetu")
        sql.AppendLine(",a.nyuuryoku_no")
        sql.AppendLine(",a.naiyou")

        sql.AppendLine(",a.upd_datetime")
        sql.AppendLine(",a.add_login_user_id")
        sql.AppendLine(",a.add_datetime")
        sql.AppendLine(",a.upd_login_user_id")
        sql.AppendLine(",b.meisyou")
        sql.AppendLine(" FROM            ")
        sql.AppendLine("    m_kameiten_bikou_settei as a WITH (READCOMMITTED)")
        sql.AppendLine("  left join  m_meisyou as b WITH (READCOMMITTED)")
        sql.AppendLine(" on a.bikou_syubetu = b.code and b.meisyou_syubetu=@meisyou_syubetu ")
        sql.AppendLine("WHERE")
        sql.AppendLine("    a.kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and a.bikou_syubetu = @bikou_syubetu")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 32, bikou_syubetu))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "09"))
        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), kameitenBikouDataSet, _
                    kameitenBikouDataSet.m_kameiten_bikou_settei.TableName, paramList.ToArray)
        If kameitenBikouDataSet.m_kameiten_bikou_settei.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If


    End Function

    ''' <summary>
    ''' �����X���lcount�擾
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelkameitenBikouInfoCount(ByVal kameiten_cd As String) As Integer

        ' DataSet�C���X�^���X�̐���
        Dim kameitenBikouDataSet As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("count(a.kameiten_cd)")

        sql.AppendLine(" FROM            ")
        sql.AppendLine("    m_kameiten_bikou_settei as a WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    a.kameiten_cd = @kameiten_cd")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), kameitenBikouDataSet, _
                   "data", paramList.ToArray)


        Return kameitenBikouDataSet.Tables(0).Rows(0).Item(0)

    End Function

    ''' <summary>
    ''' �����X���l���擾
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="bikou_syubetu">���l���</param>
    ''' <param name="nyuuryoku_no">����No.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelkameitenBikouInfo(ByVal kameiten_cd As String, ByVal bikou_syubetu As String, ByVal nyuuryoku_no As String) As String

        ' DataSet�C���X�^���X�̐���
        Dim kameitenBikouDataSet As New KameitenBikouDataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("a.kameiten_cd")
        sql.AppendLine(",a.bikou_syubetu")
        sql.AppendLine(",a.nyuuryoku_no")
        sql.AppendLine(",a.naiyou")
        sql.AppendLine(",a.upd_datetime")
        sql.AppendLine(",a.add_login_user_id")
        sql.AppendLine(",a.add_datetime")
        sql.AppendLine(",a.upd_login_user_id")
        sql.AppendLine(",b.meisyou")
        sql.AppendLine(" FROM            ")
        sql.AppendLine("    m_kameiten_bikou_settei as a WITH (READCOMMITTED)")
        sql.AppendLine("  left join  m_meisyou as b WITH (READCOMMITTED)")
        sql.AppendLine(" on a.bikou_syubetu = b.code and b.meisyou_syubetu=@meisyou_syubetu ")
        sql.AppendLine("WHERE")
        sql.AppendLine("    a.kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and a.bikou_syubetu = @bikou_syubetu")
        sql.AppendLine("    and a.nyuuryoku_no = @nyuuryoku_no")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 32, bikou_syubetu))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 32, nyuuryoku_no))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "09"))
        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), kameitenBikouDataSet, _
                    kameitenBikouDataSet.m_kameiten_bikou_settei.TableName, paramList.ToArray)
        If kameitenBikouDataSet.m_kameiten_bikou_settei.Rows.Count > 0 Then
            If kameitenBikouDataSet.m_kameiten_bikou_settei(0).Isupd_datetimeNull Then
                Return kameitenBikouDataSet.m_kameiten_bikou_settei(0).add_login_user_id & "," & kameitenBikouDataSet.m_kameiten_bikou_settei(0).add_datetime
            Else
                Return kameitenBikouDataSet.m_kameiten_bikou_settei(0).upd_login_user_id & "," & kameitenBikouDataSet.m_kameiten_bikou_settei(0).upd_datetime

            End If

        Else
            Return String.Empty
        End If


    End Function

    ''' <summary>
    ''' �����X���l�X�V���擾
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelkameitenBikouMaxUpdInfo(ByVal kameiten_cd As String) As String

        ' DataSet�C���X�^���X�̐���
        Dim dataset As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 

        sql.AppendLine("SELECT     top 1       ")
        sql.AppendLine("isnull(max(upd_datetime),max(add_datetime)) as maxtime")
        sql.AppendLine("       , isnull(upd_login_user_id,add_login_user_id) as theuser")

        sql.AppendLine(" FROM            ")
        sql.AppendLine("    m_kameiten_bikou_settei WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")


        sql.AppendLine("        group by upd_login_user_id,add_login_user_id ")

        sql.AppendLine("     order by maxtime desc")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))



        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), dataset, _
                    "maxdate", paramList.ToArray)

        If dataset.Tables(0).Rows.Count > 0 Then
            Return dataset.Tables(0).Rows(0).Item(0).ToString & "," & dataset.Tables(0).Rows(0).Item(1).ToString
        Else
            Return String.Empty
        End If

    End Function

    ''' <summary>
    ''' ���l�X�V
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="bikou_syubetu">���l���</param>
    ''' <param name="nyuuryoku_no">����No</param>
    ''' <param name="insData">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updBikou(ByVal kameiten_cd As String, ByVal bikou_syubetu As String, ByVal nyuuryoku_no As String, ByVal insData As KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable) As Integer

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" update m_kameiten_bikou_settei WITH(UPDLOCK) set")
        sql.AppendLine("bikou_syubetu	=	@bikou_syubetu")
        sql.AppendLine(",naiyou	=	@naiyou ")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")

        sql.AppendLine(" WHERE ")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("   and nyuuryoku_no = @nyuuryoku_no")
        sql.AppendLine("   and bikou_syubetu = @bikou_syubetu2")

        paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.VarChar, 5, insData(0).bikou_syubetu))
        paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, insData(0).naiyou))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, insData(0).upd_login_user_id))

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 20, Convert.ToInt32(nyuuryoku_no)))
        paramList.Add(MakeParam("@bikou_syubetu2", SqlDbType.Int, 20, Convert.ToInt32(bikou_syubetu)))

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        If SelKameitenBikouRenkei(kameiten_cd, Convert.ToInt32(nyuuryoku_no)) Then
            updKameitenBikouRenkei(2, kameiten_cd, Convert.ToInt32(nyuuryoku_no), insData(0).upd_login_user_id)

        Else
            InsKameitenBikourenkei(2, kameiten_cd, Convert.ToInt32(nyuuryoku_no), insData(0).upd_login_user_id)

        End If
        Return intResult
    End Function

    ''' <summary>
    ''' ���l�폜
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="bikou_syubetu">���l���</param>
    ''' <param name="nyuuryoku_no">����No</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelBikou(ByVal kameiten_cd As String, ByVal bikou_syubetu As String, ByVal nyuuryoku_no As String, ByVal upd_login_user_id As String) As Integer



        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�V�K�ǉ�
        sql.AppendLine(" delete from m_kameiten_bikou_settei WITH(UPDLOCK) ")
        sql.AppendLine(" WHERE ")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("   and nyuuryoku_no = @nyuuryoku_no")
        sql.AppendLine("   and bikou_syubetu = @bikou_syubetu2")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 20, Convert.ToInt32(nyuuryoku_no)))
        paramList.Add(MakeParam("@bikou_syubetu2", SqlDbType.Int, 20, Convert.ToInt32(bikou_syubetu)))

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        Dim sql1 As New StringBuilder
        sql1.AppendLine(" DECLARE nyuuryokuCursor cursor scroll dynamic")
        sql1.AppendLine("  for")
        sql1.AppendLine("  select")
        sql1.AppendLine(" 	nyuuryoku_no")
        sql1.AppendLine(" from ")
        sql1.AppendLine(" m_kameiten_bikou_settei")
        sql1.AppendLine(" where")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" and nyuuryoku_no>@nyuuryoku_no")
        sql1.AppendLine(" OPEN nyuuryokuCursor")
        sql1.AppendLine(" DECLARE @nyuuryokuNo int")
        sql1.AppendLine(" FETCH next from nyuuryokuCursor into @nyuuryokuNo")
        sql1.AppendLine(" WHILE(@@FETCH_status=0)")
        sql1.AppendLine(" BEGIN")
        sql1.AppendLine(" 	update m_kameiten_bikou_settei ")
        sql1.AppendLine(" 	set nyuuryoku_no = @nyuuryokuNo-1")
        sql1.Append(",upd_login_user_id	=	@upd_login_user_id")
        sql1.Append(",upd_datetime	=	getdate()")
        sql1.AppendLine(" where")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" and nyuuryoku_no=@nyuuryokuNo")
        sql1.AppendLine(" 	FETCH next from nyuuryokuCursor into @nyuuryokuNo")
        sql1.AppendLine(" END")
        sql1.AppendLine(" close nyuuryokuCursor")
        sql1.AppendLine(" deallocate nyuuryokuCursor")
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))

        ExecuteNonQuery(connStr, _
                                           CommandType.Text, _
                                           sql1.ToString(), _
                                           paramList.ToArray)


        If SelKameitenBikouRenkei(kameiten_cd, nyuuryoku_no) Then
            updKameitenBikouRenkeiDel(9, kameiten_cd, nyuuryoku_no, upd_login_user_id)

        Else
            InsKameitenBikourenkei(9, kameiten_cd, Convert.ToInt32(nyuuryoku_no), upd_login_user_id)
            updKameitenBikouRenkeiDel(9, kameiten_cd, nyuuryoku_no, upd_login_user_id)
        End If
        Return intResult
    End Function

    ''' <summary>
    ''' ���l�ǉ�
    ''' </summary>
    ''' <param name="insData">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsBikou(ByVal insData As KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable) As Integer



        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("DECLARE @nyuuryokuno int")
        sql.AppendLine("set @nyuuryokuno = (select isnull( (select max(nyuuryoku_no) from m_kameiten_bikou_settei Where kameiten_cd = @kameiten_cd), 0))")


        '�V�K�ǉ�
        sql.AppendLine("  INSERT INTO m_kameiten_bikou_settei WITH(UPDLOCK) (")

        sql.AppendLine("kameiten_cd")
        sql.AppendLine(",bikou_syubetu")
        sql.AppendLine(",nyuuryoku_no")
        sql.AppendLine(",naiyou")
        sql.AppendLine(",add_login_user_id")
        sql.AppendLine(",add_datetime")
        sql.AppendLine(",upd_login_user_id	")
        sql.AppendLine(",upd_datetime	")
        sql.AppendLine(" )VALUES ( ")
        sql.AppendLine("@kameiten_cd")
        sql.AppendLine(",@bikou_syubetu")
        sql.AppendLine(",@nyuuryokuno+1")
        sql.AppendLine(",@naiyou")
        sql.AppendLine(",@add_login_user_id")
        sql.AppendLine(",getdate()")
        sql.AppendLine(",@add_login_user_id")
        sql.AppendLine(",getdate())")


        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, insData(0).kameiten_cd))
        paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.VarChar, 5, insData(0).bikou_syubetu))
        paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, insData(0).naiyou))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, insData(0).add_login_user_id))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, insData(0).upd_login_user_id))

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        If SelKameitenBikouRenkei(insData(0).kameiten_cd, Convert.ToInt32(insData(0).nyuuryoku_no)) Then
            updKameitenBikouRenkei(1, insData(0).kameiten_cd, Convert.ToInt32(insData(0).nyuuryoku_no), insData(0).upd_login_user_id)

        Else
            InsKameitenBikourenkei(1, insData(0).kameiten_cd, Convert.ToInt32(insData(0).nyuuryoku_no), insData(0).upd_login_user_id)

        End If
        Return intResult
    End Function

    ''' <summary>
    ''' �����X���l�A�g�擾
    ''' </summary>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="nyuuryoku_no">����No.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelKameitenBikouRenkei(ByVal kameiten_cd As String, ByVal nyuuryoku_no As Integer) As Boolean

        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("SELECT          ")
        sql.AppendLine("kameiten_cd")           '	�����X����
        sql.AppendLine("FROM            ")
        sql.AppendLine("    m_kameiten_bikou_settei_renkei  WITH (READCOMMITTED)")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and nyuuryoku_no = @nyuuryoku_no")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Decimal, 2, nyuuryoku_no))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                 "data", paramList.ToArray)

        If data.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If


    End Function

    ''' <summary>
    ''' �����X���l�A�g�X�V
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="nyuuryoku_no">����No</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updKameitenBikouRenkei(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal nyuuryoku_no As Integer, ByVal upd_login_user_id As String)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim i As Integer
        For i = 1 To nyuuryoku_no
            If SelKameitenBikouRenkei(kameiten_cd, i) Then
            Else
                InsKameitenBikourenkei2(2, kameiten_cd, i, upd_login_user_id)
            End If
        Next
        '�V�K�ǉ�
        sql.AppendLine(" UPDATE  m_kameiten_bikou_settei_renkei WITH(UPDLOCK) set ")
        sql.AppendLine("renkei_siji_cd=@renkei_siji_cd")
        sql.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and nyuuryoku_no = @nyuuryoku_no")

        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Decimal, 2, nyuuryoku_no))

        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)



        Return intResult


    End Function

    ''' <summary>
    ''' �����X���l�A�g�폜
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="nyuuryoku_no">����No</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updKameitenBikouRenkeiDel(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal nyuuryoku_no As Integer, ByVal upd_login_user_id As String)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim i As Integer
        For i = 1 To nyuuryoku_no
            If SelKameitenBikouRenkei(kameiten_cd, i) Then
            Else
                InsKameitenBikourenkei2(2, kameiten_cd, i, upd_login_user_id)
            End If
        Next

        sql.AppendLine("DECLARE @nyuuryokuno int")
        sql.AppendLine("set @nyuuryokuno = (select isnull( (select max(nyuuryoku_no) from m_kameiten_bikou_settei_renkei Where kameiten_cd = @kameiten_cd), 0))")


        '�V�K�ǉ�
        sql.AppendLine(" UPDATE  m_kameiten_bikou_settei_renkei WITH(UPDLOCK) set ")
        sql.AppendLine("     nyuuryoku_no = @nyuuryokuno+1")
        sql.AppendLine(",renkei_siji_cd=@renkei_siji_cd")
        sql.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        sql.AppendLine(",upd_datetime	=	getdate()")
        sql.AppendLine("WHERE")
        sql.AppendLine("    kameiten_cd = @kameiten_cd")
        sql.AppendLine("    and nyuuryoku_no = @nyuuryoku_no")

        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Decimal, 2, nyuuryoku_no))

        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)

        Dim sql1 As New StringBuilder
        sql1.AppendLine(" DECLARE nyuuryokuCursor cursor scroll dynamic")
        sql1.AppendLine("  for")
        sql1.AppendLine("  select")
        sql1.AppendLine(" 	nyuuryoku_no")
        sql1.AppendLine(" from ")
        sql1.AppendLine(" m_kameiten_bikou_settei_renkei")
        sql1.AppendLine(" where")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" and nyuuryoku_no>@nyuuryoku_no")
        sql1.AppendLine(" OPEN nyuuryokuCursor")
        sql1.AppendLine(" DECLARE @nyuuryokuNo int")
        sql1.AppendLine(" FETCH next from nyuuryokuCursor into @nyuuryokuNo")
        sql1.AppendLine(" WHILE(@@FETCH_status=0)")
        sql1.AppendLine(" BEGIN")
        sql1.AppendLine(" 	update m_kameiten_bikou_settei_renkei ")
        sql1.AppendLine(" 	set nyuuryoku_no = @nyuuryokuNo-1")
        sql1.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        sql1.AppendLine(" 	,renkei_siji_cd = case renkei_siji_cd when '9' then '9' else '2' end")
        sql1.Append(",upd_login_user_id	=	@upd_login_user_id")
        sql1.Append(",upd_datetime	=	getdate()")
        sql1.AppendLine(" where")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" and nyuuryoku_no=@nyuuryokuNo")
        sql1.AppendLine(" 	FETCH next from nyuuryokuCursor into @nyuuryokuNo")
        sql1.AppendLine(" END")
        sql1.AppendLine(" close nyuuryokuCursor")
        sql1.AppendLine(" deallocate nyuuryokuCursor")

        ExecuteNonQuery(connStr, _
                                           CommandType.Text, _
                                           sql1.ToString(), _
                                           paramList.ToArray)

        Return intResult


    End Function

    ''' <summary>
    ''' �����X���l�A�g
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="nyuuryoku_no">����No</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsKameitenBikourenkei(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal nyuuryoku_no As Integer, ByVal upd_login_user_id As String) As Integer
        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim i As Integer
        For i = 1 To nyuuryoku_no - 1
            If SelKameitenBikouRenkei(kameiten_cd, i) Then
            Else
                InsKameitenBikourenkei2(2, kameiten_cd, i, upd_login_user_id)
            End If
        Next
        '�V�K�ǉ�
        sql.AppendLine(" INSERT INTO m_kameiten_bikou_settei_renkei WITH(UPDLOCK) (")
        sql.AppendLine("kameiten_cd")           '	�����X����
        sql.AppendLine(",nyuuryoku_no")
        sql.AppendLine(",renkei_siji_cd")
        sql.AppendLine(",sousin_jyky_cd")
        sql.AppendLine(",sousin_kanry_datetime")
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime")



        sql.AppendLine(") VALUES ( ")
        sql.AppendLine("@kameiten_cd")           '	�����X����
        sql.AppendLine(",@nyuuryoku_no")
        sql.AppendLine(",@renkei_siji_cd")
        sql.AppendLine(",@sousin_jyky_cd")
        sql.AppendLine(",@sousin_kanry_datetime")
        sql.AppendLine(",@upd_login_user_id")
        sql.AppendLine(",getdate())")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Decimal, 2, nyuuryoku_no))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 10, DBNull.Value))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))



        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)



        Return intResult
    End Function

    ''' <summary>
    ''' �����X���l�A�g
    ''' </summary>
    ''' <param name="renkei_siji_cd">�A�g�w���R�[�h</param>
    ''' <param name="kameiten_cd">�����X�R�[�h</param>
    ''' <param name="nyuuryoku_no">����No</param>
    ''' <param name="upd_login_user_id">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsKameitenBikourenkei2(ByVal renkei_siji_cd As Integer, ByVal kameiten_cd As String, ByVal nyuuryoku_no As Integer, ByVal upd_login_user_id As String) As Integer
        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        sql.AppendLine("DECLARE @updatetime datetime")
        sql.AppendLine("DECLARE @upd_login_user_id VARCHAR(200) ")
        sql.AppendLine("set @updatetime = (select  max(isnull(upd_datetime,add_datetime)) from m_kameiten_bikou_settei Where kameiten_cd = @kameiten_cd and nyuuryoku_no=@nyuuryoku_no)")
        sql.AppendLine("set @upd_login_user_id = (select  max(isnull(upd_login_user_id,add_login_user_id)) from m_kameiten_bikou_settei Where kameiten_cd = @kameiten_cd and nyuuryoku_no=@nyuuryoku_no)")
  
        '�V�K�ǉ�
        sql.AppendLine(" INSERT INTO m_kameiten_bikou_settei_renkei WITH(UPDLOCK) (")
        sql.AppendLine("kameiten_cd")           '	�����X����
        sql.AppendLine(",nyuuryoku_no")
        sql.AppendLine(",renkei_siji_cd")
        sql.AppendLine(",sousin_jyky_cd")
        sql.AppendLine(",sousin_kanry_datetime")
        sql.AppendLine(",upd_login_user_id")
        sql.AppendLine(",upd_datetime")



        sql.AppendLine(") VALUES ( ")
        sql.AppendLine("@kameiten_cd")           '	�����X����
        sql.AppendLine(",@nyuuryoku_no")
        sql.AppendLine(",@renkei_siji_cd")
        sql.AppendLine(",@sousin_jyky_cd")
        sql.AppendLine(",@sousin_kanry_datetime")
        sql.AppendLine(",@upd_login_user_id")
        sql.AppendLine(",@updatetime)")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Decimal, 2, nyuuryoku_no))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, renkei_siji_cd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 10, DBNull.Value))



        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql.ToString(), _
                                    paramList.ToArray)



        Return intResult
    End Function

    ''' <summary>
    ''' �����X��ʎ擾
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Selkameitensyubetu(ByVal code As Integer) As String

        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("code")
        sql.AppendLine(",meisyou")
        sql.AppendLine(" FROM            ")
        sql.AppendLine("  m_meisyou WITH (READCOMMITTED)")

        sql.AppendLine("WHERE")
        sql.AppendLine(" meisyou_syubetu=@meisyou_syubetu ")
        sql.AppendLine(" and code = @code")
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "09"))
        paramList.Add(MakeParam("@code", SqlDbType.Int, 4, code))


        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                   "data", paramList.ToArray)

        If data.Tables(0).Rows.Count > 0 Then
            Return data.Tables(0).Rows(0).Item(1).ToString
        Else
            Return Nothing
        End If



    End Function

    ''' <summary>
    ''' �����X�A�g�X�V
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strUserId">�X�V��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdKyoutuuJyouhouRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" DELETE FROM ")
            .AppendLine("   m_kameiten_renkei ")
            .AppendLine(" WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            .AppendLine(" INSERT INTO ")
            .AppendLine("   m_kameiten_renkei WITH(UPDLOCK) ")
            .AppendLine("   (kameiten_cd, ")
            .AppendLine("   renkei_siji_cd, ")
            .AppendLine("   sousin_jyky_cd, ")
            .AppendLine("   sousin_kanry_datetime, ")
            .AppendLine("   upd_login_user_id, ")
            .AppendLine("   upd_datetime) ")
            .AppendLine(" VALUES( ")
            .AppendLine("   @kameiten_cd, ")
            .AppendLine("   @renkei_siji_cd, ")
            .AppendLine("   @sousin_jyky_cd, ")
            .AppendLine("   @sousin_kanry_datetime, ")
            .AppendLine("   @upd_login_user_id, ")
            .AppendLine("   GETDATE()) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, 2))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 20, DBNull.Value))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))

        Try
            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class

