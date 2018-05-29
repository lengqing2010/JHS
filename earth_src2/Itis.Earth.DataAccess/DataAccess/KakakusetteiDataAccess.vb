Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class KakakusetteiDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    '============================2011/04/26 �ԗ� �C�� �J�n��=====================================
    'Public Function SelKakakuInfo(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As DataTable
    Public Function SelKakakuInfo(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String, Optional ByVal strSyouhinCd As String = "") As DataTable
        '========================2011/04/26 �ԗ� �C�� �I����=====================================

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" CONVERT(VARCHAR(10),m_syouhin_kakakusettei.syouhin_kbn)+':'+ISNULL(m_meisyou_01.meisyou,'')  ")
        commandTextSb.AppendLine(" ,CONVERT(VARCHAR(10),m_syouhin_kakakusettei.tys_houhou_no)+':'+ISNULL(m_tyousahouhou.tys_houhou_mei_ryaku,'')  ")
        commandTextSb.AppendLine(" ,CONVERT(VARCHAR(10),m_syouhin_kakakusettei.tys_gaiyou)+':'+ISNULL(m_meisyou_02.meisyou,'')  ")
        commandTextSb.AppendLine(" ,ISNULL(m_syouhin_kakakusettei.syouhin_cd,'')+':'+ISNULL(m_syouhin.syouhin_mei,'')  ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(VARCHAR(10),m_syouhin_kakakusettei.kkk_settei_basyo),'')  ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(VARCHAR(19),m_syouhin_kakakusettei.upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_syouhin_kakakusettei WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_meisyou as m_meisyou_01 WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_syouhin_kakakusettei.syouhin_kbn=m_meisyou_01.code  ")
        commandTextSb.AppendLine(" AND m_meisyou_01.meisyou_syubetu='01'  ")
        commandTextSb.AppendLine(" LEFT JOIN m_tyousahouhou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_tyousahouhou.tys_houhou_no=m_syouhin_kakakusettei.tys_houhou_no  ")
        commandTextSb.AppendLine(" LEFT JOIN m_meisyou as m_meisyou_02 WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_syouhin_kakakusettei.tys_gaiyou=m_meisyou_02.code  ")
        commandTextSb.AppendLine(" AND m_meisyou_02.meisyou_syubetu='02'  ")
        commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_syouhin_kakakusettei.syouhin_cd=m_syouhin.syouhin_cd  ")
        commandTextSb.AppendLine(" LEFT JOIN m_meisyou as m_meisyou_03 WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_syouhin_kakakusettei.kkk_settei_basyo=m_meisyou_03.code  ")
        commandTextSb.AppendLine(" AND m_meisyou_03.meisyou_syubetu='03'  ")
        commandTextSb.AppendLine(" WHERE syouhin_kbn IS NOT NULL  ")
        '============================2011/04/26 �ԗ� �C�� �J�n��=====================================
        'If strKbn.Trim = "" And strHouhou.Trim = "" And strGaiyou.Trim = "" Then
        If strKbn.Trim = "" And strHouhou.Trim = "" And strGaiyou.Trim = "" And strSyouhinCd.Trim.Equals(String.Empty) Then
            '========================2011/04/26 �ԗ� �C�� �I����=====================================
            commandTextSb.AppendLine(" AND syouhin_kbn IS NULL  ")
        Else
            If strKbn.Trim <> "" Then
                commandTextSb.AppendLine(" AND m_syouhin_kakakusettei.syouhin_kbn =@syouhin_kbn ")
                paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strKbn))
            End If
            If strHouhou.Trim <> "" Then
                commandTextSb.AppendLine(" AND m_syouhin_kakakusettei.tys_houhou_no =@tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 10, strHouhou))
            End If
            If strGaiyou.Trim <> "" Then
                commandTextSb.AppendLine(" AND m_syouhin_kakakusettei.tys_gaiyou =@tys_gaiyou ")
                paramList.Add(MakeParam("@tys_gaiyou", SqlDbType.VarChar, 10, strGaiyou))
            End If
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            '����i�R�[�h�
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                commandTextSb.AppendLine(" AND m_syouhin_kakakusettei.syouhin_cd =@syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
            End If
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
        End If
        commandTextSb.AppendLine(" ORDER BY m_syouhin_kakakusettei.syouhin_kbn,m_syouhin_kakakusettei.tys_houhou_no,m_syouhin_kakakusettei.tys_gaiyou")
        '�p�����[�^�̐ݒ�

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function
    Public Function SelKakutyouInfo(ByVal strSyubetu As String, Optional ByVal strTable As String = "") As DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        If strTable = "m_tyousahouhou" Then
            commandTextSb.AppendLine(" SELECT   ")
            commandTextSb.AppendLine(" tys_houhou_no AS code   ")
            commandTextSb.AppendLine(" ,tys_houhou_mei_ryaku AS meisyou   ")
            commandTextSb.AppendLine(" ,tys_houhou_mei AS meisyou2   ")
            commandTextSb.AppendLine(" FROM  ")
            commandTextSb.AppendLine(" m_tyousahouhou   WITH (READCOMMITTED)  ")

            commandTextSb.AppendLine(" ORDER BY tys_houhou_no ")
            '�p�����[�^�̐ݒ�
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 3, strSyubetu))
        Else
            commandTextSb.AppendLine(" SELECT   ")
            commandTextSb.AppendLine(" code   ")
            commandTextSb.AppendLine(" ,meisyou   ")
            commandTextSb.AppendLine(" FROM  ")
            commandTextSb.AppendLine(" m_meisyou   WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" WHERE  meisyou_syubetu=@meisyou_syubetu  ")
            commandTextSb.AppendLine(" ORDER BY hyouji_jyun ")
            '�p�����[�^�̐ݒ�
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, strSyubetu))
        End If

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)


    End Function
    Public Function SelJyuufuku(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" ISNULL(CONVERT(varchar(19),upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_syouhin_kakakusettei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@strKbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@strHouhou  ")
        commandTextSb.AppendLine(" AND tys_gaiyou=@strGaiyou  ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@strHouhou", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@strGaiyou", SqlDbType.VarChar, 10, strGaiyou))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function InsKakakusettei(ByVal strKbn As String, _
                               ByVal strHouhou As String, _
                               ByVal strGaiyou As String, _
                               ByVal strSyouhinCd As String, _
                               ByVal strSetteiBasyo As String, _
                               ByVal strUserId As String) As Integer



        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_syouhin_kakakusettei WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(syouhin_kbn")
        commandTextSb.AppendLine(",tys_houhou_no")
        commandTextSb.AppendLine(",tys_gaiyou")
        commandTextSb.AppendLine(",syouhin_cd")
        commandTextSb.AppendLine(",kkk_settei_basyo")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime)")

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @syouhin_kbn")
        commandTextSb.AppendLine(",@tys_houhou_no")
        commandTextSb.AppendLine(",@tys_gaiyou")
        commandTextSb.AppendLine(",@syouhin_cd")
        commandTextSb.AppendLine(",@kkk_settei_basyo")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")


        '�p�����[�^�̐ݒ�()
        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@tys_gaiyou", SqlDbType.VarChar, 10, strGaiyou))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@kkk_settei_basyo", SqlDbType.VarChar, 10, strSetteiBasyo))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' �N�G�����s
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
    Public Function SelHaita(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String, ByVal strKousin As String) As DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" upd_login_user_id  ")
        commandTextSb.AppendLine(" ,upd_datetime  ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_syouhin_kakakusettei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@strKbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@strHouhou  ")
        commandTextSb.AppendLine(" AND tys_gaiyou=@strGaiyou  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@strHouhou", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@strGaiyou", SqlDbType.VarChar, 10, strGaiyou))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function UpdKakakusettei(ByVal strKbn As String, _
                               ByVal strHouhou As String, _
                               ByVal strGaiyou As String, _
                               ByVal strSyouhinCd As String, _
                               ByVal strSetteiBasyo As String, _
                               ByVal strUserId As String) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        commandTextSb.AppendLine(" UPDATE m_syouhin_kakakusettei  WITH (UPDLOCK)    ")
        commandTextSb.AppendLine(" SET syouhin_cd=@syouhin_cd ")
        commandTextSb.AppendLine(" ,kkk_settei_basyo=@kkk_settei_basyo ")
        commandTextSb.AppendLine(" ,upd_login_user_id=@add_login_user_id")
        commandTextSb.AppendLine(" ,upd_datetime=GETDATE()")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@syouhin_kbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@tys_houhou_no  ")
        commandTextSb.AppendLine(" AND tys_gaiyou=@tys_gaiyou  ")
        '�p�����[�^�̐ݒ�()
        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@tys_gaiyou", SqlDbType.VarChar, 10, strGaiyou))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@kkk_settei_basyo", SqlDbType.VarChar, 10, strSetteiBasyo))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' �N�G�����s
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)



    End Function

    Public Function DelKakakusettei(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As Integer

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New SyouhinDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("DELETE FROM m_syouhin_kakakusettei WITH (UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@strKbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@strHouhou  ")
        commandTextSb.AppendLine(" AND tys_gaiyou=@strGaiyou  ")


        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@strHouhou", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@strGaiyou", SqlDbType.VarChar, 10, strGaiyou))

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function

    '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
    ''' <summary>
    ''' �d���`�F�b�N(���i�敪�A�������@�A���i����)
    ''' </summary>
    ''' <param name="strKbn">���i�敪</param>
    ''' <param name="strHouhou">�������@</param>
    ''' <param name="strSyouhin">���i����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2011/04/26�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelJyuufukuSyouhin(ByVal strKbn As String, ByVal strHouhou As String, ByVal strSyouhin As String) As DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" ISNULL(CONVERT(varchar(19),upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_syouhin_kakakusettei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@strKbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@strHouhou  ")
        commandTextSb.AppendLine(" AND syouhin_cd=@syouhin_cd  ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@strHouhou", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhin))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function
    '========================2011/04/26 �ԗ� �ǉ� �I����============================

End Class
