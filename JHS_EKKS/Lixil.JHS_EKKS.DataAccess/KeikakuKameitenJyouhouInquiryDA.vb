Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �v��_�����X���Ɖ�
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKameitenJyouhouInquiryDA

    ''' <summary>
    ''' ��{���f�[�^���擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/06�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKihonJyouSyutoku(ByVal strKameitenCd As String, ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikakuNendo)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	 MKK.torikesi")                 '--�v��Ǘ�_�����X�}�X�^.���
            .AppendLine("	,MKKM1.meisyou AS torikesiMei") '--�v��p_�g�����̃}�X�^.����
            .AppendLine("	,MKK.hattyuu_teisi_flg")        '--�v��Ǘ�_�����X�}�X�^.������~FLG
            .AppendLine("	,MKKM2.meisyou AS flgMei")      '--�v��p_�g�����̃}�X�^.����
            .AppendLine("	,MKKI.kbn")              '--�v��Ǘ�_�����X���}�X�^.�敪
            .AppendLine("	,MDK.kbn_mei")           '--�敪�}�X�^.�敪��
            .AppendLine("	,MKK.kameiten_cd")       '--��Ǘ�_�����X�}�X�^.�����X����
            .AppendLine("	,MKK.kameiten_mei")      '--�v��Ǘ�_�����X�}�X�^.�����X��
            .AppendLine("	,MKK.eigyou_kbn")        '--�v��Ǘ�_�����X�}�X�^.�c�Ƌ敪
            .AppendLine("	,MKM.meisyou AS eigyouKbnMei") '--�v��p_���̃}�X�^.����
            .AppendLine("	,MKK.eigyou_tantousya_id")   '--�v��Ǘ�_�����X�}�X�^.�c�ƒS����
            .AppendLine("	,MKK.eigyou_tantousya_mei")  '--�v��Ǘ�_�����X�}�X�^.�c�ƒS���Җ�
            .AppendLine("	,MKK.shiten_mei")            '--�v��Ǘ�_�����X�}�X�^.�x�X��
            .AppendLine("	,MKK.todouhuken_cd")         '--�v��Ǘ�_�����X�}�X�^.�s���{������
            .AppendLine("	,MKK.todouhuken_mei")        '--�v��Ǘ�_�����X�}�X�^.�s���{����
            .AppendLine("	,MKK.eigyousyo_cd")          '--�v��Ǘ�_�����X�}�X�^.�c�Ə�����
            .AppendLine("	,ME.eigyousyo_mei")          '--�c�Ə��}�X�^.�c�Ə���
            .AppendLine("	,MKK.keiretu_cd")            '--�v��Ǘ�_�����X�}�X�^.�n����
            .AppendLine("	,MK.keiretu_mei")            '--�n��}�X�^.�n��
            .AppendLine("	,METSI.syozoku")             '--�c�ƒS����_�������}�X�^.����
            .AppendLine("	,MKK.keikaku_nenkan_tousuu") '--�v��Ǘ�_�����X�}�X�^.�N�ԓ���
            .AppendLine("	,CASE ")
            .AppendLine("		When MKK.keikaku0_flg = '0' Then ")
            .AppendLine("            '�v��l�L��' ")
            .AppendLine("		When MKK.keikaku0_flg = '1' Then  ")
            .AppendLine("           '�v��l����' ")
            .AppendLine("		When ISNULL(MKK.keikaku0_flg,'') = '' Then")
            .AppendLine("           ''")
            .AppendLine("		End AS keikaku0_flg ")  '--�v��Ǘ�_�����X�}�X�^.�v��l0FLG
            .AppendLine("	,MKKI.add_login_user_id")    '--�o�^���O�C�����[�U�[ID	
            .AppendLine("	,MKKI.add_datetime")         '--�o�^����
            .AppendLine("	,MKKI.upd_login_user_id")    '--�X�V���O�C�����[�U�[ID
            .AppendLine("	,MKKI.upd_datetime")         '--�X�V����

            .AppendLine("FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")   '--�v��Ǘ�_�����X�}�X�^
            '--�v��p_�g�����̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_kakutyou_meisyou AS MKKM1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKM1.meisyou_syubetu = '56'")
            .AppendLine("	AND MKK.torikesi = MKKM1.code ")
            '--�v��p_�g�����̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_kakutyou_meisyou AS MKKM2 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKM2.meisyou_syubetu = '8'")
            .AppendLine("	AND MKK.hattyuu_teisi_flg = MKKM2.code ")
            '--�v��Ǘ�_�����X���}�X�^
            .AppendLine("	LEFT JOIN m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED)")
            .AppendLine("	ON MKK.kameiten_cd = MKKI.kameiten_cd ")
            '--�v��p_���̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM WITH(READCOMMITTED)")
            .AppendLine("	ON MKM.meisyou_syubetu = '05'")
            .AppendLine("	AND MKK.eigyou_kbn = MKM.code ")
            '--�c�ƒS����_�������}�X�^
            .AppendLine("	LEFT JOIN m_eigyou_tantousya_syozoku_info AS METSI WITH(READCOMMITTED)")
            .AppendLine("	ON MKK.eigyou_tantousya_id = METSI.eigyou_tantousya_id ")
            '--�敪�}�X�^
            .AppendLine("	LEFT JOIN m_data_kbn AS MDK WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.kbn = MDK.kbn ")
            '--�c�Ə��}�X�^
            .AppendLine("	LEFT JOIN m_eigyousyo AS ME WITH(READCOMMITTED)")
            .AppendLine("	ON MKK.eigyousyo_cd = ME.eigyousyo_cd ")
            '--�n��}�X�^
            .AppendLine("	LEFT JOIN m_keiretu AS MK WITH(READCOMMITTED)")
            .AppendLine("	ON MKK.keiretu_cd = MK.keiretu_cd ")
            .AppendLine("WHERE")
            .AppendLine("	MKK.kameiten_cd = @strKameitenCd")
            .AppendLine("	AND MKK.keikaku_nendo = @strNendo")
        End With

        paramList.Add(MakeParam("@strKameitenCd", SqlDbType.VarChar, 16, strKameitenCd))                 '�����X�R�[�h
        paramList.Add(MakeParam("@strNendo", SqlDbType.Char, 4, strKeikakuNendo))                        '�v��_�N�x

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKihonJyouhouSyutoku", paramList.ToArray())
        Return dsReturn.Tables("dtKihonJyouhouSyutoku")
    End Function

    ''' <summary>
    ''' �v��Ǘ��p_�����X���f�[�^�擾
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/06�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKameitenJyouhou(ByVal strKameitenCd As String, ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikakuNendo)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	MKKI.keikakuyou_kameitenmei")    '--�v��Ǘ�_�����X���}�X�^.�v��p_�����X��
            .AppendLine("	,MKKI.gyoutai")                  '--�v��Ǘ�_�����X���}�X�^.�Ƒ�
            '.AppendLine("	,(MKKI.gyoutai + ':' + MKM1.meisyou) AS gyoutaiMei")    '--�v��p_���̃}�X�^.����
            .AppendLine("	,MKKI.touitsu_houjin_cd")        '--�v��Ǘ�_�����X���}�X�^.����@�l�R�[�h
            .AppendLine("   ,MKK1.kameiten_mei AS touitsuHoujinMei")             '--�v��Ǘ�_�����XϽ�.�����X��
            .AppendLine("	,MKKI.houjin_cd ")               '--�v��Ǘ�_�����X���}�X�^.�@�l�R�[�h
            .AppendLine("   ,MKK2.kameiten_mei AS houjinMei") '--�v��Ǘ�_�����XϽ�.�����X��
            .AppendLine("	,MKKI.keikaku_nayose_cd")        '--�v��Ǘ�_�����X���}�X�^.�v�於��R�[�h
            .AppendLine("   ,MKK3.kameiten_mei AS keikakuNayoseMei") '--�v��Ǘ�_�����XϽ�.�����X��
            .AppendLine("	,MKKI.keikakuyou_nenkan_tousuu") '--�v��Ǘ�_�����X���}�X�^.�v��p_�N�ԓ���
            .AppendLine("	,MKKI.keikakuji_eigyou_tantousya_id")  '--�v��Ǘ�_�����X���}�X�^.�v�掞_�c�ƒS����
            '.AppendLine("	,MKKI.keikakuji_eigyou_tantousya_mei") ' --�v��Ǘ�_�����X���}�X�^.�v�掞_�c�ƒS���Җ�
            .AppendLine("   ,MJM.DisplayName")                     '--�Ј��A�J�E���g���}�X�^.�\����
            .AppendLine("	,METSI.syozoku")                       '--�c�ƒS����_�������}�X�^.����
            .AppendLine("	,MKKI.keikakuyou_eigyou_kbn")          '--�v��Ǘ�_�����X���}�X�^.�v��p_�c�Ƌ敪
            .AppendLine("	,MKM2.meisyou AS eigyouKbnMei")        '--�v��p_���̃}�X�^.����_�v��p_�c�Ƌ敪�̓��e�擾
            .AppendLine("	,MKKI.keikakuyou_kannkatsu_siten")     '--�v��Ǘ�_�����X���}�X�^.�v��p_�Ǌ��x�X
            .AppendLine("	,MBK1.busyo_cd")                       '--�����Ǘ��}�X�^.�����R�[�h_�v��p_�Ǌ��x�X�̓��e�擾
            .AppendLine("	,MKKI.keikakuyou_kannkatsu_siten_mei ") '--�v��Ǘ�_�����X���}�X�^.�v��p_�Ǌ��x�X��
            .AppendLine("	,MBK1.busyo_mei ")              '--�����Ǘ��}�X�^.������_�v��p_�Ǌ��x�X�̓��e�擾
            .AppendLine("	,MKKI.sds_kaisi_nengetu")       '--�v��Ǘ�_�����X���}�X�^.SDS�J�n�N��
            .AppendLine("	,MKKI.kameiten_zokusei_1")      '--�v��Ǘ�_�����X���}�X�^.�����X����1
            .AppendLine("	,MKM3.meisyou AS zokusei1Mei")  '--�v��p_���̃}�X�^.����_�����X����1�̓��e�擾
            .AppendLine("	,MKKI.kameiten_zokusei_2")      '--�v��Ǘ�_�����X���}�X�^.�����X����2
            .AppendLine("	,MKM4.meisyou AS zokusei2Mei")  '--�v��p_���̃}�X�^.����_�����X����2�̓��e�擾
            .AppendLine("	,MKKI.kameiten_zokusei_3")      '--�v��Ǘ�_�����X���}�X�^.�����X����3
            .AppendLine("	,MKM5.meisyou AS zokusei3Mei")  '--�v��p_���̃}�X�^.����_�����X����3�̓��e�擾
            .AppendLine("	,MKKI.kameiten_zokusei_4")      '--�v��Ǘ�_�����X���}�X�^.�����X����4
            .AppendLine("	,MKKM1.meisyou AS zokusei4Mei") '--�v��p_�g�����̃}�X�^.����_�����X����4�̓��e�擾
            .AppendLine("	,MKKI.kameiten_zokusei_5")      '--�v��Ǘ�_�����X���}�X�^.�����X����5
            .AppendLine("	,MKKM2.meisyou AS zokusei5Mei") '--�v��p_�g�����̃}�X�^.����_�����X����5�̓��e�擾
            .AppendLine("	,MKKI.kameiten_zokusei_6")      '--�v��Ǘ�_�����X���}�X�^.�����X����6
            .AppendLine("FROM")
            .AppendLine("	m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED)") '--�v��Ǘ�_�����X���}�X�^
            '--�v��p_���̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM1.meisyou_syubetu = '20'")
            .AppendLine("	AND MKKI.gyoutai = MKM1.code ")
            '--�c�ƒS����_�������}�X�^
            .AppendLine("	LEFT JOIN m_eigyou_tantousya_syozoku_info AS METSI WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.keikakuji_eigyou_tantousya_id = METSI.eigyou_tantousya_id ")
            '--�v��p_���̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM2 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM2.meisyou_syubetu = '05'")
            .AppendLine("	AND MKKI.keikakuyou_eigyou_kbn = MKM2.code ")
            '--�����Ǘ��}�X�^
            .AppendLine("	LEFT JOIN m_busyo_kanri AS MBK1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.keikakuyou_kannkatsu_siten = MBK1.busyo_cd")
            .AppendLine("	AND MBK1.sosiki_level = '4'")
            '--�v��p_���̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM3 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM3.meisyou_syubetu = '21'")
            .AppendLine("	AND MKKI.kameiten_zokusei_1 = MKM3.code")
            '--�v��p_���̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM4 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM4.meisyou_syubetu = '22'")
            .AppendLine("	AND MKKI.kameiten_zokusei_2 = MKM4.code")
            '--�v��p_���̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM5 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM5.meisyou_syubetu = '23'")
            .AppendLine("	AND MKKI.kameiten_zokusei_3 = MKM5.code")
            '--�v��p_�g�����̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_kakutyou_meisyou AS MKKM1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKM1.meisyou_syubetu = '21'")
            .AppendLine("	AND MKKI.kameiten_zokusei_4 = MKKM1.code")
            '--�v��p_�g�����̃}�X�^
            .AppendLine("	LEFT JOIN m_keikakuyou_kakutyou_meisyou AS MKKM2 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKM2.meisyou_syubetu = '22'")
            .AppendLine("	AND MKKI.kameiten_zokusei_5 = MKKM2.code")
            '�v��Ǘ�_�����XϽ�
            .AppendLine("	LEFT JOIN m_keikaku_kameiten AS MKK1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.touitsu_houjin_cd = MKK1.kameiten_cd")
            .AppendLine("   AND MKK1.keikaku_nendo = @strNendo")
            '�v��Ǘ�_�����XϽ�
            .AppendLine("	LEFT JOIN m_keikaku_kameiten AS MKK2 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.houjin_cd = MKK2.kameiten_cd")
            .AppendLine("   AND MKK2.keikaku_nendo = @strNendo")
            '�v��Ǘ�_�����XϽ�
            .AppendLine("	LEFT JOIN m_keikaku_kameiten AS MKK3 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.keikaku_nayose_cd = MKK3.kameiten_cd")
            .AppendLine("   AND MKK3.keikaku_nendo = @strNendo")
            '--�Ј��A�J�E���g���}�X�^
            .AppendLine("   LEFT JOIN m_jhs_mailbox AS MJM WITH(READCOMMITTED)")
            .AppendLine("   ON MKKI.keikakuji_eigyou_tantousya_id = MJM.PrimaryWindowsNTAccount")

            .AppendLine("WHERE")
            .AppendLine("	MKKI.kameiten_cd =@strKameitenCd")
        End With

        paramList.Add(MakeParam("@strKameitenCd", SqlDbType.VarChar, 5, strKameitenCd))                 '�����X�R�[�h
        paramList.Add(MakeParam("@strNendo", SqlDbType.Char, 4, strKeikakuNendo))                        '�v��_�N�x

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKameitenJyouhou", paramList.ToArray())
        Return dsReturn.Tables("dtKameitenJyouhou")

    End Function

    ''' <summary>
    ''' ��ʂ̉����X���ނ��v��Ǘ�_�����X���}�X�^.�����X���ނɑ��݂��邩�ǂ����𔻒f����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelCount(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New System.Text.StringBuilder
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   count(kameiten_cd)")
            .AppendLine("FROM ")
            .AppendLine("   m_keikaku_kameiten_info WITH(READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @strKameitenCd ")
        End With

        paramList.Add(MakeParam("@strKameitenCd", SqlDbType.VarChar, 5, strKameitenCd))   '�����X�R�[�h

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtCount", paramList.ToArray())
        Return dsReturn.Tables("dtCount")

    End Function


    ''' <summary>
    ''' �u�v��Ǘ�_�����X���Ͻ��v�̒ǉ�����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X����</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKbnMei">�敪��</param>
    ''' <param name="strKeikakuyouKameitenmei">�v��p_�����X��</param>
    ''' <param name="inGyoutai">�Ƒ�</param>
    ''' <param name="strTouitsuHoujinCd">����@�l�R�[�h</param>
    ''' <param name="strHoujinCd">�@�l�R�[�h</param>
    ''' <param name="strKeikakuNayoseCd">�v�於��R�[�h</param>
    ''' <param name="inKeikakuyouNenkanTousuu">�v��p_�N�ԓ���</param>
    ''' <param name="strKeikakujiEigyouTantousyaId">�v�掞_�c�ƒS����</param>
    ''' <param name="strKeikakujiEigyouTantousyaMei">�v�掞_�c�ƒS���Җ�</param>
    ''' <param name="inKeikakuyouEigyouKbn">�v��p_�c�Ƌ敪</param>
    ''' <param name="strKeikakuyouKannkatsuSiten">�v��p_�Ǌ��x�X</param>
    ''' <param name="strKeikakuyouKannkatsuSitenMei">�v��p_�Ǌ��x�X��</param>
    ''' <param name="strSdsKaisiNengetu">SDS�J�n�N��</param>
    ''' <param name="inKameitenZokusei1">�����X����1</param>
    ''' <param name="inKameitenZokusei2">�����X����2</param>
    ''' <param name="inKameitenZokusei3">�����X����3</param>
    ''' <param name="strKameitenZokusei4">�����X����4</param>
    ''' <param name="strKameitenZokusei5">�����X����5</param>
    ''' <param name="strKameitenZokusei6">�����X����6</param>
    ''' <param name="strAddLoginUserId">�o�^���O�C�����[�U�[ID</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsMKeikakuKameitenInfo(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strKbnMei As String, _
                                            ByVal strKeikakuyouKameitenmei As String, ByVal inGyoutai As String, ByVal strTouitsuHoujinCd As String, _
                                            ByVal strHoujinCd As String, ByVal strKeikakuNayoseCd As String, ByVal inKeikakuyouNenkanTousuu As String, _
                                            ByVal strKeikakujiEigyouTantousyaId As String, ByVal strKeikakujiEigyouTantousyaMei As String, _
                                            ByVal inKeikakuyouEigyouKbn As String, ByVal strKeikakuyouKannkatsuSiten As String, _
                                            ByVal strKeikakuyouKannkatsuSitenMei As String, ByVal strSdsKaisiNengetu As String, _
                                            ByVal inKameitenZokusei1 As String, ByVal inKameitenZokusei2 As String, _
                                            ByVal inKameitenZokusei3 As String, ByVal strKameitenZokusei4 As String, _
                                            ByVal strKameitenZokusei5 As String, ByVal strKameitenZokusei6 As String, _
                                            ByVal strAddLoginUserId As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strAddLoginUserId)

        'SQL�R�����g
        Dim commandTextSb As New System.Text.StringBuilder
        '�X�V��
        Dim insCount As Integer
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO m_keikaku_kameiten_info WITH(UPDLOCK)")
            .AppendLine("(")
            .AppendLine("	kameiten_cd ")              '--�����X����
            .AppendLine("	,kbn")                      '--�敪
            .AppendLine("	,kbn_mei")                  '--�敪��
            .AppendLine("	,keikakuyou_kameitenmei ")  '--�v��p_�����X��
            .AppendLine("	,gyoutai")                  '--�Ƒ�
            .AppendLine("	,touitsu_houjin_cd")        '--����@�l�R�[�h
            .AppendLine("	,houjin_cd")                '--�@�l�R�[�h
            .AppendLine("	,keikaku_nayose_cd")                '--�v�於��R�[�h
            .AppendLine("	,keikakuyou_nenkan_tousuu ")        '--�v��p_�N�ԓ���
            .AppendLine("	,keikakuji_eigyou_tantousya_id")    '--�v�掞_�c�ƒS����
            .AppendLine("	,keikakuji_eigyou_tantousya_mei")   '--�v�掞_�c�ƒS���Җ�
            .AppendLine("	,keikakuyou_eigyou_kbn")            '--�v��p_�c�Ƌ敪
            .AppendLine("	,keikakuyou_kannkatsu_siten ")      '--�v��p_�Ǌ��x�X(�R�[�h)
            .AppendLine("	,keikakuyou_kannkatsu_siten_mei")   '--�v��p_�Ǌ��x�X��
            .AppendLine("	,sds_kaisi_nengetu")  '--SDS�J�n�N��
            .AppendLine("	,kameiten_zokusei_1") '--�����X����1
            .AppendLine("	,kameiten_zokusei_2") '--�����X����2
            .AppendLine("	,kameiten_zokusei_3") '--�����X����3
            .AppendLine("	,kameiten_zokusei_4") '--�����X����4
            .AppendLine("	,kameiten_zokusei_5") '--�����X����5
            .AppendLine("	,kameiten_zokusei_6") '--�����X����6
            .AppendLine("	,add_login_user_id")  '--�o�^���O�C�����[�U�[ID
            .AppendLine("	,add_datetime")       '--�o�^����
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("	@kameiten_cd")
            .AppendLine("	,@kbn")
            .AppendLine("	,@kbn_mei")
            .AppendLine("	,@keikakuyou_kameitenmei")
            .AppendLine("	,@gyoutai")
            .AppendLine("	,@touitsu_houjin_cd")
            .AppendLine("	,@houjin_cd ")
            .AppendLine("	,@keikaku_nayose_cd	")
            .AppendLine("	,@keikakuyou_nenkan_tousuu")
            .AppendLine("	,@keikakuji_eigyou_tantousya_id")
            .AppendLine("	,@keikakuji_eigyou_tantousya_mei")
            .AppendLine("	,@keikakuyou_eigyou_kbn")
            .AppendLine("	,@keikakuyou_kannkatsu_siten")
            .AppendLine("	,@keikakuyou_kannkatsu_siten_mei")
            .AppendLine("	,@sds_kaisi_nengetu")
            .AppendLine("	,@kameiten_zokusei_1")
            .AppendLine("	,@kameiten_zokusei_2")
            .AppendLine("	,@kameiten_zokusei_3")
            .AppendLine("	,@kameiten_zokusei_4")
            .AppendLine("	,@kameiten_zokusei_5")
            .AppendLine("	,@kameiten_zokusei_6")
            .AppendLine("	,@add_login_user_id")
            .AppendLine("	,GetDate()")
            .AppendLine(")")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@kbn_mei", SqlDbType.VarChar, 40, strKbnMei))
        paramList.Add(MakeParam("@keikakuyou_kameitenmei", SqlDbType.VarChar, 80, strKeikakuyouKameitenmei))
        paramList.Add(MakeParam("@gyoutai", SqlDbType.Int, 10, IIf(inGyoutai.Equals(String.Empty), DBNull.Value, inGyoutai)))
        paramList.Add(MakeParam("@touitsu_houjin_cd", SqlDbType.VarChar, 8, strTouitsuHoujinCd))
        paramList.Add(MakeParam("@houjin_cd", SqlDbType.VarChar, 8, strHoujinCd))
        paramList.Add(MakeParam("@keikaku_nayose_cd", SqlDbType.VarChar, 8, strKeikakuNayoseCd))
        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu", SqlDbType.Int, 10, IIf(inKeikakuyouNenkanTousuu.Equals(String.Empty), DBNull.Value, inKeikakuyouNenkanTousuu)))
        paramList.Add(MakeParam("@keikakuji_eigyou_tantousya_id", SqlDbType.VarChar, 30, strKeikakujiEigyouTantousyaId))
        paramList.Add(MakeParam("@keikakuji_eigyou_tantousya_mei", SqlDbType.VarChar, 100, strKeikakujiEigyouTantousyaMei))
        paramList.Add(MakeParam("@keikakuyou_eigyou_kbn", SqlDbType.Int, 2, IIf(inKeikakuyouEigyouKbn.Equals(String.Empty), DBNull.Value, inKeikakuyouEigyouKbn)))
        paramList.Add(MakeParam("@keikakuyou_kannkatsu_siten", SqlDbType.VarChar, 4, strKeikakuyouKannkatsuSiten))
        paramList.Add(MakeParam("@keikakuyou_kannkatsu_siten_mei", SqlDbType.VarChar, 40, strKeikakuyouKannkatsuSitenMei))
        paramList.Add(MakeParam("@sds_kaisi_nengetu", SqlDbType.VarChar, 7, strSdsKaisiNengetu))
        paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 2, IIf(inKameitenZokusei1.Equals(String.Empty), DBNull.Value, inKameitenZokusei1)))
        paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 2, IIf(inKameitenZokusei2.Equals(String.Empty), DBNull.Value, inKameitenZokusei2)))
        paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 2, IIf(inKameitenZokusei3.Equals(String.Empty), DBNull.Value, inKameitenZokusei3)))
        paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.VarChar, 10, strKameitenZokusei4))
        paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.VarChar, 10, strKameitenZokusei5))
        paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, strKameitenZokusei6))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strAddLoginUserId))

        insCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, paramList.ToArray())

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' �u�v��Ǘ�_�����X���Ͻ��v�̍X�V����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X����</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKbnMei">�敪��</param>
    ''' <param name="strKeikakuyouKameitenmei">�v��p_�����X��</param>
    ''' <param name="inGyoutai">�Ƒ�</param>
    ''' <param name="strTouitsuHoujinCd">����@�l�R�[�h</param>
    ''' <param name="strHoujinCd">�@�l�R�[�h</param>
    ''' <param name="strKeikakuNayoseCd">�v�於��R�[�h</param>
    ''' <param name="inKeikakuyouNenkanTousuu">�v��p_�N�ԓ���</param>
    ''' <param name="strKeikakujiEigyouTantousyaId">�v�掞_�c�ƒS����</param>
    ''' <param name="strKeikakujiEigyouTantousyaMei">�v�掞_�c�ƒS���Җ�</param>
    ''' <param name="inKeikakuyouEigyouKbn">�v��p_�c�Ƌ敪</param>
    ''' <param name="strKeikakuyouKannkatsuSiten">�v��p_�Ǌ��x�X</param>
    ''' <param name="strKeikakuyouKannkatsuSitenMei">�v��p_�Ǌ��x�X��</param>
    ''' <param name="strSdsKaisiNengetu">SDS�J�n�N��</param>
    ''' <param name="inKameitenZokusei1">�����X����1</param>
    ''' <param name="inKameitenZokusei2">�����X����2</param>
    ''' <param name="inKameitenZokusei3">�����X����3</param>
    ''' <param name="strKameitenZokusei4">�����X����4</param>
    ''' <param name="strKameitenZokusei5">�����X����5</param>
    ''' <param name="strKameitenZokusei6">�����X����6</param>
    ''' <param name="strUpdLoginUserId">�X�V���O�C�����[�U�[ID</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function UpdMKeikakuKameitenInfo(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strKbnMei As String, _
                                            ByVal strKeikakuyouKameitenmei As String, ByVal inGyoutai As String, ByVal strTouitsuHoujinCd As String, _
                                            ByVal strHoujinCd As String, ByVal strKeikakuNayoseCd As String, ByVal inKeikakuyouNenkanTousuu As String, _
                                            ByVal strKeikakujiEigyouTantousyaId As String, ByVal strKeikakujiEigyouTantousyaMei As String, _
                                            ByVal inKeikakuyouEigyouKbn As String, ByVal strKeikakuyouKannkatsuSiten As String, _
                                            ByVal strKeikakuyouKannkatsuSitenMei As String, ByVal strSdsKaisiNengetu As String, _
                                            ByVal inKameitenZokusei1 As String, ByVal inKameitenZokusei2 As String, _
                                            ByVal inKameitenZokusei3 As String, ByVal strKameitenZokusei4 As String, _
                                            ByVal strKameitenZokusei5 As String, ByVal strKameitenZokusei6 As String, _
                                            ByVal strUpdLoginUserId As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strUpdLoginUserId)

        'SQL�R�����g
        Dim commandTextSb As New System.Text.StringBuilder
        '�X�V��
        Dim updCount As Integer
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("UPDATE m_keikaku_kameiten_info WITH(UPDLOCK)")
            .AppendLine("SET ")
            .AppendLine("	kbn = @kbn")
            .AppendLine("	,kbn_mei = @kbn_mei")
            .AppendLine("	,keikakuyou_kameitenmei = @keikakuyou_kameitenmei")
            .AppendLine("	,gyoutai = @gyoutai")
            .AppendLine("	,touitsu_houjin_cd = @touitsu_houjin_cd")
            .AppendLine("	,houjin_cd = @houjin_cd ")
            .AppendLine("	,keikaku_nayose_cd = @keikaku_nayose_cd	")
            .AppendLine("	,keikakuyou_nenkan_tousuu = @keikakuyou_nenkan_tousuu")
            .AppendLine("	,keikakuji_eigyou_tantousya_id = @keikakuji_eigyou_tantousya_id")
            .AppendLine("	,keikakuji_eigyou_tantousya_mei = @keikakuji_eigyou_tantousya_mei")
            .AppendLine("	,keikakuyou_eigyou_kbn = @keikakuyou_eigyou_kbn")
            .AppendLine("	,keikakuyou_kannkatsu_siten =@keikakuyou_kannkatsu_siten")
            .AppendLine("	,keikakuyou_kannkatsu_siten_mei = @keikakuyou_kannkatsu_siten_mei")
            .AppendLine("	,sds_kaisi_nengetu = @sds_kaisi_nengetu	")
            .AppendLine("	,kameiten_zokusei_1 = @kameiten_zokusei_1")
            .AppendLine("	,kameiten_zokusei_2 = @kameiten_zokusei_2")
            .AppendLine("	,kameiten_zokusei_3 = @kameiten_zokusei_3")
            .AppendLine("	,kameiten_zokusei_4 = @kameiten_zokusei_4")
            .AppendLine("	,kameiten_zokusei_5 = @kameiten_zokusei_5")
            .AppendLine("	,kameiten_zokusei_6 = @kameiten_zokusei_6")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id")
            .AppendLine("	,upd_datetime = GetDate()")
            .AppendLine("WHERE kameiten_cd = @kameiten_cd")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@kbn_mei", SqlDbType.VarChar, 40, strKbnMei))
        paramList.Add(MakeParam("@keikakuyou_kameitenmei", SqlDbType.VarChar, 80, strKeikakuyouKameitenmei))
        paramList.Add(MakeParam("@gyoutai", SqlDbType.Int, 10, IIf(inGyoutai.Equals(String.Empty), DBNull.Value, inGyoutai)))
        paramList.Add(MakeParam("@touitsu_houjin_cd", SqlDbType.VarChar, 8, strTouitsuHoujinCd))
        paramList.Add(MakeParam("@houjin_cd", SqlDbType.VarChar, 8, strHoujinCd))
        paramList.Add(MakeParam("@keikaku_nayose_cd", SqlDbType.VarChar, 8, strKeikakuNayoseCd))
        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu", SqlDbType.Int, 10, IIf(inKeikakuyouNenkanTousuu.Equals(String.Empty), DBNull.Value, inKeikakuyouNenkanTousuu)))
        paramList.Add(MakeParam("@keikakuji_eigyou_tantousya_id", SqlDbType.VarChar, 30, strKeikakujiEigyouTantousyaId))
        paramList.Add(MakeParam("@keikakuji_eigyou_tantousya_mei", SqlDbType.VarChar, 100, strKeikakujiEigyouTantousyaMei))
        paramList.Add(MakeParam("@keikakuyou_eigyou_kbn", SqlDbType.Int, 2, IIf(inKeikakuyouEigyouKbn.Equals(String.Empty), DBNull.Value, inKeikakuyouEigyouKbn)))
        paramList.Add(MakeParam("@keikakuyou_kannkatsu_siten", SqlDbType.VarChar, 4, strKeikakuyouKannkatsuSiten))
        paramList.Add(MakeParam("@keikakuyou_kannkatsu_siten_mei", SqlDbType.VarChar, 40, strKeikakuyouKannkatsuSitenMei))
        paramList.Add(MakeParam("@sds_kaisi_nengetu", SqlDbType.VarChar, 7, strSdsKaisiNengetu))
        paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 2, IIf(inKameitenZokusei1.Equals(String.Empty), DBNull.Value, inKameitenZokusei1)))
        paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 2, IIf(inKameitenZokusei2.Equals(String.Empty), DBNull.Value, inKameitenZokusei2)))
        paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 2, IIf(inKameitenZokusei3.Equals(String.Empty), DBNull.Value, inKameitenZokusei3)))
        paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.VarChar, 10, strKameitenZokusei4))
        paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.VarChar, 10, strKameitenZokusei5))
        paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, strKameitenZokusei6))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUpdLoginUserId))

        updCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, paramList.ToArray())

        If updCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' �v��Ǘ�_�����X���}�X�^�̔r������
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKameitenMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT TOP 1 ")
            .AppendLine("	ISNULL(MAX(upd_datetime),MAX(add_datetime)) AS maxtime ")
            .AppendLine("	,ISNULL(upd_login_user_id,add_login_user_id) as theuser ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten_info WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("GROUP BY  ")
            .AppendLine("	upd_login_user_id ")
            .AppendLine("	,add_login_user_id ")
            .AppendLine("ORDER BY  ")
            .AppendLine("	maxtime DESC ")
        End With

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMaxUpdTime", paramList.ToArray)

        Return dsReturn.Tables("dtMaxUpdTime")

    End Function

    ''' <summary>
    ''' �c�ƒS���Җ����擾����
    ''' </summary>
    ''' <param name="strTantousyaId">�c�ƒS����ID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelEigyouTantousyaMei(ByVal strTantousyaId As String) As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	DisplayName")
            .AppendLine("FROM ")
            .AppendLine("	m_jhs_mailbox WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	PrimaryWindowsNTAccount = @PrimaryWindowsNTAccount ")
        End With

        paramList.Add(MakeParam("@PrimaryWindowsNTAccount", SqlDbType.VarChar, 64, strTantousyaId))

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMei", paramList.ToArray)

        Return dsReturn.Tables("dtMei")

    End Function

End Class
