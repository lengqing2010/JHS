Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>���[�U�[�Ǘ������Ɖ�o�^����</summary>
''' <remarks>���[�U�[�Ǘ������Ɖ�o�^�@�\��񋟂���</remarks>
''' <history>
''' <para>2009/07/17�@����N(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KanrisyaMenuInquiryInputDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �Ɩ��敪�����擾����B
    ''' </summary>
    ''' <returns>�Ɩ��敪���f�[�^�Z�b�g</returns>
    Public Function selGyoumuKubun() As KanrisyaMenuInquiryInputDataSet.gyoumuKubunDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsGyoumu As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" code, ")
        commandTextSb.AppendLine(" meisyou ")
        commandTextSb.AppendLine(" FROM m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  meisyou_syubetu= @meisyou_syubetu ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "53"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsGyoumu, _
                    dsGyoumu.gyoumuKubun.TableName, paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        Return dsGyoumu.gyoumuKubun
    End Function

    ''' <summary>
    ''' ���O�C�����[�U�[�̎Q�ƌ����Ǘ���FLG���擾����B
    ''' </summary>
    ''' <param name="strUserId">���O�C�����[�U�[ID</param>
    ''' <returns>�Q�ƌ����Ǘ���FLG�f�[�^�e�[�u��</returns>
    Public Function selUserKengenKanriFlg(ByVal strUserId As String) _
            As KanrisyaMenuInquiryInputDataSet.userKengenKanriFlgDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsUserKengenKanriFlg As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" sansyou_kengen_kanri_flg ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsUserKengenKanriFlg, _
                    dsUserKengenKanriFlg.userKengenKanriFlg.TableName, paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        Return dsUserKengenKanriFlg.userKengenKanriFlg

    End Function

    ''' <summary>
    ''' ���������ύX�������擾����B
    ''' </summary>
    ''' <param name="strUserId">��ʂɓ��͂������[�U�[ID</param>
    ''' <returns>���������ύX�����f�[�^�Z�b�g</returns>
    Public Function selSyozokuHenkouDate(ByVal strUserId As String) _
            As KanrisyaMenuInquiryInputDataSet.syozokuHenkouDateDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsSyozokuHenkouDate As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" login_user_id, ")
        commandTextSb.AppendLine(" syozoku_henkou_date ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId.ToString))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsSyozokuHenkouDate, _
                    dsSyozokuHenkouDate.syozokuHenkouDate.TableName, paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        Return dsSyozokuHenkouDate.syozokuHenkouDate
    End Function

    ''' <summary>
    ''' ���[�U�[�����擾����B
    ''' </summary>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strSyozokuHenkouDate">���������ύX��</param>
    ''' <returns>���[�U�[���f�[�^�Z�b�g</returns>
    Public Function selUserInfo(ByVal strUserId As String, ByVal strSyozokuHenkouDate As String) _
                                        As KanrisyaMenuInquiryInputDataSet.kanrisyaJyouhouDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsUserInfo As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" A.login_user_id, ")
        commandTextSb.AppendLine(" B.DisplayName, ")
        commandTextSb.AppendLine(" C.busyo_mei, ")
        commandTextSb.AppendLine(" D.meisyou AS sosikiMei, ")
        commandTextSb.AppendLine(" E.yakusyoku, ")
        commandTextSb.AppendLine(" A.sansyou_kengen_kanri_flg, ")
        commandTextSb.AppendLine(" A.eigyou_man_kbn, ")
        commandTextSb.AppendLine(" A.gyoumu_kbn, ")
        commandTextSb.AppendLine(" A.t_sansyou_busyo_cd, ")
        commandTextSb.AppendLine(" C.sosiki_level, ")
        commandTextSb.AppendLine(" C.busyo_cd, ")
        commandTextSb.AppendLine(" F.irai_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.sinki_nyuuryoku_kengen, ")
        commandTextSb.AppendLine(" F.data_haki_kengen, ")
        commandTextSb.AppendLine(" F.kekka_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hosyou_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hkks_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.koj_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.keiri_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hansoku_uri_kengen, ")
        commandTextSb.AppendLine(" F.hattyuusyo_kanri_kengen, ")
        commandTextSb.AppendLine(" F.kaiseki_master_kanri_kengen, ")
        commandTextSb.AppendLine(" F.eigyou_master_kanri_kengen, ")
        commandTextSb.AppendLine(" F.kkk_master_kanri_kengen, ")
        commandTextSb.AppendLine(" F.system_kanrisya_kengen, ")

        commandTextSb.AppendLine(" F.tyousaka_kanrisya_kengen, ")
        commandTextSb.AppendLine(" F.kensa_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hanyou1_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hanyou2_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hanyou3_gyoumu_kengen, ")
        commandTextSb.AppendLine(" A.account_no, ")

        commandTextSb.AppendLine(" ISNULL(A.upd_datetime,'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou  A WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_jhs_mailbox B ")
        commandTextSb.AppendLine(" ON A.login_user_id=B.PrimaryWindowsNTAccount ")
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_busyo_kanri C ")
        If strSyozokuHenkouDate.ToString <> "" Then
            If strSyozokuHenkouDate.ToString.Substring(0, 10) > Date.Today.ToString.Substring(0, 10) Then
                commandTextSb.AppendLine(" ON A.kyuu_syozoku_busyo_cd=C.busyo_cd ")
            Else
                commandTextSb.AppendLine(" ON A.busyo_cd=C.busyo_cd ")
            End If
        Else
            commandTextSb.AppendLine(" ON A.busyo_cd=C.busyo_cd ")
        End If

        commandTextSb.AppendLine(" LEFT OUTER JOIN m_meisyou D ")
        commandTextSb.AppendLine(" ON D.meisyou_syubetu=@meisyou_syubetu1 ")
        commandTextSb.AppendLine("    AND	C.sosiki_level=D.code ")
        commandTextSb.AppendLine(" LEFT OUTER JOIN ")
        commandTextSb.AppendLine(" (SELECT ")
        commandTextSb.AppendLine(" C.login_user_id, ")
        commandTextSb.AppendLine(" D.meisyou AS yakusyoku ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("  (SELECT ")
        commandTextSb.AppendLine("  TOP 1 A.yakusyoku_cd, ")
        commandTextSb.AppendLine("  B.sosiki_level, ")
        commandTextSb.AppendLine("  A.login_user_id ")
        commandTextSb.AppendLine("  FROM m_kanrityou A ")
        commandTextSb.AppendLine("  LEFT OUTER JOIN m_busyo_kanri B ")
        commandTextSb.AppendLine("  ON  A.busyo_cd=B.busyo_cd ")
        commandTextSb.AppendLine("  WHERE A.login_user_id=@login_user_id ")
        commandTextSb.AppendLine("  ORDER BY   B.sosiki_level ASC) AS C ")
        commandTextSb.AppendLine("  INNER JOIN m_meisyou D ")
        commandTextSb.AppendLine("  ON C.yakusyoku_cd=D.code AND D.meisyou_syubetu=@meisyou_syubetu2) AS E ")
        commandTextSb.AppendLine(" ON A.login_user_id=E.login_user_id ")
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_account F ")
        commandTextSb.AppendLine(" ON A.account_no=F.account_no ")
        commandTextSb.AppendLine(" WHERE  ")
        commandTextSb.AppendLine(" A.login_user_id=@login_user_id ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu1", SqlDbType.VarChar, 2, "50"))
        paramList.Add(MakeParam("@meisyou_syubetu2", SqlDbType.VarChar, 2, "51"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsUserInfo, _
                    dsUserInfo.kanrisyaJyouhou.TableName, paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        Return dsUserInfo.kanrisyaJyouhou
    End Function

    ''' <summary>
    ''' �ǉ��Q�ƕ����R�[�h�ɂ���Ď擾�����g�D���x��
    ''' </summary>
    ''' <param name="strBusyoCd">�ǉ��Q�ƕ����R�[�h</param>
    ''' <returns>�g�D���x���f�[�^�e�[�u��</returns>
    Public Function selLevel(ByVal strBusyoCd As String) As KanrisyaMenuInquiryInputDataSet.sansyouLevelDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsLevel As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" sosiki_level ")
        commandTextSb.AppendLine(" FROM m_busyo_kanri WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  busyo_cd= @busyo_cd ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd.ToString))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsLevel, _
                    dsLevel.sansyouLevel.TableName, paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        Return dsLevel.sansyouLevel

    End Function


    ''' <summary>
    ''' �ǉ��Q�ƕ����R�[�h�ɂ���Ď擾�����g�D���x��
    ''' </summary>
    ''' <returns>�g�D���x���f�[�^�e�[�u��</returns>
    Public Function SelBusyoList() As DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim ds As New DataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" busyo_cd,busyo_mei ")
        commandTextSb.AppendLine(" FROM m_busyo_kanri WITH (READCOMMITTED) ")


        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), ds, _
                    "tmp")

        '�I������
        commandTextSb = Nothing

        Return ds.Tables(0)

    End Function





    ''' <summary>
    ''' �o�^�������[�U�[���n�ՔF�؃}�X�^�ɑ��݃`�F�b�N
    ''' </summary>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <returns>���[�U�[���e�[�u��</returns>
    Public Function selJibanNinsyouHaita(ByVal strUserId As String) As KanrisyaMenuInquiryInputDataSet.jibanNinsyouDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsJibanNinsyou As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" login_user_id ,")
        commandTextSb.AppendLine(" upd_datetime ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId.ToString))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsJibanNinsyou, _
                    dsJibanNinsyou.jibanNinsyou.TableName, paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        Return dsJibanNinsyou.jibanNinsyou

    End Function

    ''' <summary>
    ''' �n�ՔF�؃}�X�^.�Q�ƌ����Ǘ���FLG �擾����
    ''' </summary>
    ''' <param name="strUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSansyouKengenKanriFlg(ByVal strUserId As String) As String

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim ds As New Data.DataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" isnull(sansyou_kengen_kanri_flg,'') ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId.ToString))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), ds, _
                    "tmp", paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item(0).ToString
        Else
            Return ""
        End If
    End Function


    ''' <summary>
    ''' �o�^�������[�U�[���n�ՔF�؃}�X�^�A�g�Ǘ��e�[�u���ɑ��݃`�F�b�N
    ''' </summary>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <returns>���[�U�[���e�[�u��</returns>
    Public Function selJibanNinsyouRenkeiHaita(ByVal strUserId As String) As KanrisyaMenuInquiryInputDataSet.jibanNinsyouDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsJibanNinsyou As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" login_user_id ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou_renkei WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId.ToString))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsJibanNinsyou, _
                    dsJibanNinsyou.jibanNinsyou.TableName, paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        Return dsJibanNinsyou.jibanNinsyou

    End Function

    ''' <summary>
    ''' �n�ՔF�؃}�X�^���X�V����B
    ''' </summary>
    ''' <param name="dtUPDData">�X�V���ڂ̃e�[�u��</param>
    ''' <returns>true or false</returns>
    Public Function UpdJibanNinsyou(ByVal account_no As String, ByVal dtUPDData As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable) As Boolean
        '�߂�l
        UpdJibanNinsyou = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_jiban_ninsyou ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" eigyou_man_kbn=@eigyou_man_kbn, ")
        commandTextSb.AppendLine(" gyoumu_kbn=@gyoumu_kbn, ")
        commandTextSb.AppendLine(" busyo_cd=@ss_busyo_cd, ")
        commandTextSb.AppendLine(" t_sansyou_busyo_cd=@busyo_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE login_user_id = @login_user_id  ")



        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_account ")
        commandTextSb.AppendLine(" SET ")

        commandTextSb.AppendLine(" irai_gyoumu_kengen='" & dtUPDData.Rows(0).Item("irai_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" sinki_nyuuryoku_kengen='" & dtUPDData.Rows(0).Item("sinki_nyuuryoku_kengen") & "', ")
        commandTextSb.AppendLine(" data_haki_kengen='" & dtUPDData.Rows(0).Item("data_haki_kengen") & "', ")
        commandTextSb.AppendLine(" kekka_gyoumu_kengen='" & dtUPDData.Rows(0).Item("kekka_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hosyou_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hosyou_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hkks_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hkks_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" koj_gyoumu_kengen='" & dtUPDData.Rows(0).Item("koj_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" keiri_gyoumu_kengen='" & dtUPDData.Rows(0).Item("keiri_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hansoku_uri_kengen='" & dtUPDData.Rows(0).Item("hansoku_uri_kengen") & "', ")
        commandTextSb.AppendLine(" hattyuusyo_kanri_kengen='" & dtUPDData.Rows(0).Item("hattyuusyo_kanri_kengen") & "', ")
        commandTextSb.AppendLine(" kaiseki_master_kanri_kengen='" & dtUPDData.Rows(0).Item("kaiseki_master_kanri_kengen") & "', ")
        commandTextSb.AppendLine(" eigyou_master_kanri_kengen='" & dtUPDData.Rows(0).Item("eigyou_master_kanri_kengen") & "', ")
        commandTextSb.AppendLine(" kkk_master_kanri_kengen='" & dtUPDData.Rows(0).Item("kkk_master_kanri_kengen") & "', ")
        commandTextSb.AppendLine(" tyousaka_kanrisya_kengen='" & dtUPDData.Rows(0).Item("tyousaka_kanrisya_kengen") & "', ")
        commandTextSb.AppendLine(" kensa_gyoumu_kengen='" & dtUPDData.Rows(0).Item("kensa_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hanyou1_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hanyou1_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hanyou2_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hanyou2_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hanyou3_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hanyou3_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" system_kanrisya_kengen='" & dtUPDData.Rows(0).Item("system_kanrisya_kengen") & "', ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")

        commandTextSb.AppendLine("WHERE account_no = " & account_no & "  ")





        '�p�����[�^�̐ݒ�
        With dtUPDData.Rows(0)
            If dtUPDData.Rows(0).Item("eigyou_man_kbn") <> "" Then
                paramList.Add(MakeParam("@eigyou_man_kbn", SqlDbType.Int, 4, .Item("eigyou_man_kbn")))
            Else
                paramList.Add(MakeParam("@eigyou_man_kbn", SqlDbType.Int, 4, DBNull.Value))
            End If
            If dtUPDData.Rows(0).Item("gyoumu_kbn") <> "" Then
                paramList.Add(MakeParam("@gyoumu_kbn", SqlDbType.Int, 4, .Item("gyoumu_kbn")))
            Else
                paramList.Add(MakeParam("@gyoumu_kbn", SqlDbType.Int, 4, DBNull.Value))
            End If
            paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, .Item("busyo_cd")))
            paramList.Add(MakeParam("@ss_busyo_cd", SqlDbType.VarChar, 4, .Item("ss_busyo_cd")))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("upd_login_user_id")))
            paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, .Item("user_id")))

        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdJibanNinsyou = True

    End Function

    ''' <summary>
    ''' �n�ՔF�؃}�X�^�A�g�Ǘ��e�[�u����o�^����B
    ''' </summary>
    ''' <param name="dtUPDData">�o�^���ڂ̃e�[�u��</param>
    ''' <returns>true or false</returns>
    Public Function InsJibanNinsyouRenkei(ByVal dtUPDData As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable) As Boolean
        '�߂�l
        InsJibanNinsyouRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_jiban_ninsyou_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" (")
        commandTextSb.AppendLine("  login_user_id,")
        commandTextSb.AppendLine("  renkei_siji_cd,")
        commandTextSb.AppendLine("  sousin_jyky_cd,")
        commandTextSb.AppendLine("  sousin_kanry_datetime,")
        commandTextSb.AppendLine("  upd_login_user_id,")
        commandTextSb.AppendLine("  upd_datetime")
        commandTextSb.AppendLine(" ) ")
        commandTextSb.AppendLine(" VALUES( ")
        commandTextSb.AppendLine(" @login_user_id, ")
        commandTextSb.AppendLine(" @renkei_siji_cd, ")
        commandTextSb.AppendLine(" @sousin_jyky_cd, ")
        commandTextSb.AppendLine(" NULL, ")
        commandTextSb.AppendLine(" @upd_login_user_id, ")
        commandTextSb.AppendLine(" GETDATE() ")
        commandTextSb.AppendLine(" ) ")

        '�p�����[�^�̐ݒ�
        With dtUPDData.Rows(0)
            paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, .Item("user_id")))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, .Item("renkei_siji_cd")))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, .Item("sousin_jyky_cd")))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("upd_login_user_id")))
        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsJibanNinsyouRenkei = True

    End Function

    ''' <summary>
    ''' �n�ՔF�؃}�X�^�A�g�Ǘ��e�[�u�����X�V����B
    ''' </summary>
    ''' <param name="dtUPDData">�X�V���ڂ̃e�[�u��</param>
    ''' <returns>true or false</returns>
    Public Function UpdJibanNinsyouRenkei(ByVal dtUPDData As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable) As Boolean
        '�߂�l
        UpdJibanNinsyouRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_jiban_ninsyou_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" renkei_siji_cd=@renkei_siji_cd, ")
        commandTextSb.AppendLine(" sousin_jyky_cd=@sousin_jyky_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE login_user_id = @login_user_id  ")

        '�p�����[�^�̐ݒ�
        With dtUPDData.Rows(0)
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, .Item("renkei_siji_cd")))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, .Item("sousin_jyky_cd")))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("upd_login_user_id")))
            paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, .Item("user_id")))

        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdJibanNinsyouRenkei = True

    End Function

End Class
