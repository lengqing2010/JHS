Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �v��Ǘ�_�����X�����Ɖ�w��
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriKameitenKensakuSyoukaiInquiryDA

    ''' <summary>
    ''' �敪�����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKbnInfo() As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kbn AS cd ")
            .AppendLine("   ,(kbn + '�F' + kbn_mei) AS mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_data_kbn WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   torikesi=@torikesi ")
            .AppendLine("ORDER BY ")
            .AppendLine("   kbn ASC ")
        End With

        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 5, "0"))

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKbnInfo", paramList.ToArray)

        Return dsReturn.Tables("dtKbnInfo")

    End Function

    ''' <summary>
    ''' �Ǌ��x�X�����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSitenInfo() As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    busyo_cd")
            .AppendLine("     ,(busyo_cd + '�F' + busyo_mei) AS busyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_busyo_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   sosiki_level = '4' ")
            .AppendLine("ORDER BY ")
            .AppendLine("   busyo_cd ")
        End With

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSitenInfo")

        Return dsReturn.Tables("dtSitenInfo")

    End Function

    ''' <summary>
    ''' �s���{�������擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTodoufukenInfo() As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   todouhuken_cd AS cd ")
            .AppendLine("   ,(todouhuken_cd + '�F' + todouhuken_mei) AS mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_todoufuken WITH(READCOMMITTED) ")
            .AppendLine("ORDER BY ")
            .AppendLine("   todouhuken_cd ASC ")
        End With

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTodoufukenInfo")

        Return dsReturn.Tables("dtTodoufukenInfo")

    End Function

    ''' <summary>
    ''' ���̏����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelMeisyouInfo(ByVal strMeisyouSyubetu As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strMeisyouSyubetu)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    code")
            .AppendLine("     ,(CONVERT(VARCHAR(10),code) + '�F' + meisyou) AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("   m_keikakuyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("ORDER BY ")
            .AppendLine("   hyouji_jyun ")
        End With

        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, strMeisyouSyubetu))

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMeisyouInfo", paramList.ToArray)

        Return dsReturn.Tables("dtMeisyouInfo")

    End Function

    ''' <summary>
    ''' �g�����̏����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKakutyouMeisyouInfo(ByVal strMeisyouSyubetu As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strMeisyouSyubetu)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    code")
            .AppendLine("     ,(CONVERT(VARCHAR(10),code) + '�F' + meisyou) AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("   m_keikakuyou_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("ORDER BY ")
            .AppendLine("   hyouji_jyun ")
        End With

        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, strMeisyouSyubetu))

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKakutyouMeisyouInfo", paramList.ToArray)

        Return dsReturn.Tables("dtKakutyouMeisyouInfo")

    End Function

    ''' <summary>
    ''' �����X���׏����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKameitenInfo(ByVal dicPrm As Dictionary(Of String, String)) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dicPrm)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            If dicPrm("KensakuJyouken") = "100" Then
                .AppendLine("   TOP 100 ")
            ElseIf dicPrm("KensakuJyouken") = "200" Then
                .AppendLine("   TOP 200 ")
            End If
            .AppendLine("	ISNULL(MKK.torikesi,0) AS Torikesi ") '--��� ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MKK.torikesi,0) = 0 THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			CONVERT(VARCHAR(10),MKK.torikesi) + '�F' + ISNULL(MKKM_TORIKESI.meisyou,'') ")
            .AppendLine("		END AS TorikesiText ") '--����� ")
            .AppendLine("	,ISNULL(MKKI.kbn,'') AS Kbn ") '--�敪 ")
            .AppendLine("	,ISNULL(MKK.kameiten_cd,'') AS KameitenCd ") '--�����X�R�[�h ")
            .AppendLine("	,ISNULL(MKK.kameiten_mei,'') AS KameitenMei ") '--�����X�� ")
            .AppendLine("	,ISNULL(MKK.eigyou_kbn,'') AS EigyouKbn ") '--�c�Ƌ敪 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MKK.eigyou_kbn,'') = '' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			ISNULL(MKK.eigyou_kbn,'') + '�F' + ISNULL(MKM_EIGYOU_KBN.meisyou,'') ")
            .AppendLine("		END AS EigyouKbnText ") '--�c�Ƌ敪�� ")
            .AppendLine("	,ISNULL(MKK.eigyou_tantousya_mei,'') AS EigyouTantousya ") '--�c�ƒS���Җ� ")
            .AppendLine("	,ISNULL(MKK.shiten_mei,'') AS KankatuSiten ") '--�x�X�� ")
            .AppendLine("	,ISNULL(MKK.todouhuken_mei,'') AS Todoufuken ") '--�s���{���� ")
            .AppendLine("	,ISNULL(MKK.eigyousyo_cd,'') AS EigyousyoCd ") '--�c�Ə����� ")
            .AppendLine("	,ISNULL(MKK.keiretu_cd,'') AS KeiretuCd ") '--�n���� ")
            .AppendLine("	,ISNULL(METS.syozoku,'') AS EigyouTantouSyozaku ") '--���� ")
            .AppendLine("	,MKKI.gyoutai AS GyoutaiCd ") '--�ƑԃR�[�h ")
            .AppendLine("	,ISNULL(MKM_GYOUTAI.meisyou,'') AS Gyoutai ") '--�ƑԖ� ")
            .AppendLine("	,MKK.keikaku_nenkan_tousuu AS NenkanTousuu ") '--�N�ԓ��� ")
            .AppendLine("	,MKKI.keikakuyou_nenkan_tousuu AS KeikakuyouNenkanTousuu ") '--�v��p_�N�ԓ��� ")
            .AppendLine("	,MKKI.kameiten_zokusei_1 ") '--�����X����1 ")
            .AppendLine("	,ISNULL(MKM_ZOKUSEI1.meisyou,'') AS KameitenZokusei1 ") '--�����X����1�� ")
            .AppendLine("	,MKKI.kameiten_zokusei_2 ") '--�����X����2 ")
            .AppendLine("	,ISNULL(MKM_ZOKUSEI2.meisyou,'') AS KameitenZokusei2 ") '--�����X����2�� ")
            .AppendLine("	,MKKI.kameiten_zokusei_3 ") '--�����X����3 ")
            .AppendLine("	,ISNULL(MKM_ZOKUSEI3.meisyou,'') AS KameitenZokusei3 ") '--�����X����3�� ")
            .AppendLine("	,MKKI.kameiten_zokusei_4 ") '--�����X����4 ")
            .AppendLine("	,ISNULL(MKKM_ZOKUSEI4.meisyou,'') AS KameitenZokusei4 ") '--�����X����4�� ")
            .AppendLine("	,MKKI.kameiten_zokusei_5 ") '--�����X����5 ")
            .AppendLine("	,ISNULL(MKKM_ZOKUSEI5.meisyou,'') AS KameitenZokusei5 ") '--�����X����5�� ")
            .AppendLine("	,ISNULL(MKKI.kameiten_zokusei_6,'') AS KameitenZokusei6 ") '--�����X����6 ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ") '--�v��Ǘ�_�����X�}�X�^ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED) ") '--�v��Ǘ�_�����X���}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MKK.kameiten_cd = MKKI.kameiten_cd ") '--�����X�R�[�h ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_eigyou_tantousya_syozoku_info AS METS WITH(READCOMMITTED) ") '--�c�ƒS����_�������}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MKK.eigyou_tantousya_id = METS.eigyou_tantousya_id ") '--�c�ƒS���Җ� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_TORIKESI WITH(READCOMMITTED) ") '--�g�����̃}�X�^(���) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_TORIKESI.meisyou_syubetu = '56' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_TORIKESI.code = MKK.torikesi ") '--��� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_EIGYOU_KBN WITH(READCOMMITTED) ") '--���̃}�X�^(�c�Ƌ敪) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_EIGYOU_KBN.meisyou_syubetu = '05' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_EIGYOU_KBN.code = MKK.eigyou_kbn ") '--�c�Ƌ敪 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_GYOUTAI WITH(READCOMMITTED) ") '--���̃}�X�^(�Ƒ�) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_GYOUTAI.meisyou_syubetu = '20' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_GYOUTAI.code = MKKI.gyoutai ") '--�Ƒ� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI1 WITH(READCOMMITTED) ") '--���̃}�X�^(�����X����1) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI1.meisyou_syubetu = '21' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI1.code = MKKI.kameiten_zokusei_1 ") '--�����X����1 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI2 WITH(READCOMMITTED) ") '--���̃}�X�^(�����X����2) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI2.meisyou_syubetu = '22' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI2.code = MKKI.kameiten_zokusei_2 ") '--�����X����2 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI3 WITH(READCOMMITTED) ") '--���̃}�X�^(�����X����3) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI3.meisyou_syubetu = '23' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI3.code = MKKI.kameiten_zokusei_3 ") '--�����X����3 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_ZOKUSEI4 WITH(READCOMMITTED) ") '--�g�����̃}�X�^(�����X����4) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_ZOKUSEI4.meisyou_syubetu = '21' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_ZOKUSEI4.code = MKKI.kameiten_zokusei_4 ") '--�����X����4 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_ZOKUSEI5 WITH(READCOMMITTED) ") '--�g�����̃}�X�^(�����X����5) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_ZOKUSEI5.meisyou_syubetu = '22' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_ZOKUSEI5.code = MKKI.kameiten_zokusei_5 ") '--�����X����5 ")
            .AppendLine("WHERE ")
            .AppendLine("	MKK.keikaku_nendo = @keikaku_nendo ") '--�v��N�x ")
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, dicPrm("KeikakuNendo")))
            '�敪
            If Not dicPrm("Kbn").Equals(String.Empty) Then
                Dim arrKbn() As String = dicPrm("Kbn").Split(CChar(","))
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kbn IN ") '--�敪 ")
                .AppendLine("	( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("       @kbn" & i)
                    Else
                        .AppendLine("       ,@kbn" & i)
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.VarChar, 1, arrKbn(i)))
                Next
                .AppendLine("   ) ")
            End If
            '���
            If Not dicPrm("Torikesi").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.torikesi = @torikesi ") '--��� ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, dicPrm("Torikesi")))
            End If
            '�����X��
            If Not dicPrm("KameitenMei").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.kameiten_mei LIKE @kameiten_mei ") '--�����X�� ")
                paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 41, dicPrm("KameitenMei") & "%"))
            End If
            '�����X�R�[�h
            If (Not dicPrm("KameitenCd1").Equals(String.Empty)) AndAlso (Not dicPrm("KameitenCd2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ") '--�����X�R�[�h ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 16, dicPrm("KameitenCd1")))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 16, dicPrm("KameitenCd2")))
            Else
                If Not dicPrm("KameitenCd1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.kameiten_cd = @kameiten_cd_from ") '--�����X�R�[�h ")
                    paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 16, dicPrm("KameitenCd1")))
                End If
            End If
            '�c�Ə��R�[�h
            If (Not dicPrm("EigyousyaCd1").Equals(String.Empty)) AndAlso (Not dicPrm("EigyousyaCd2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ") '--�c�Ə��R�[�h ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd1")))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd2")))
            Else
                If Not dicPrm("EigyousyaCd1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.eigyousyo_cd = @eigyousyo_cd_from ") '--�c�Ə��R�[�h ")
                    paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd1")))
                End If
            End If
            '�n��R�[�h
            If Not dicPrm("KeiretuCd").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keiretu_cd = @keiretu_cd ") '--�n��R�[�h ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dicPrm("KeiretuCd")))
            End If
            '�Ǌ��x�X
            If Not dicPrm("Siten").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.busyo_cd = @busyo_cd ") '--�Ǌ��x�X ")
                paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 16, dicPrm("Siten")))
            End If
            '�s���{���R�[�h
            If Not dicPrm("TodoufukenCd").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.todouhuken_cd = @todouhuken_cd ") '--�s���{���R�[�h ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dicPrm("TodoufukenCd")))
            End If
            '�c�ƒS����
            If Not dicPrm("EigyouTantousya").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyou_tantousya_id = @eigyou_tantousya_id ") '--�c�ƒS���� ")
                paramList.Add(MakeParam("@eigyou_tantousya_id", SqlDbType.VarChar, 30, dicPrm("EigyouTantousya")))
            End If
            '�c�Ƌ敪
            If Not dicPrm("EigyouKbn").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyou_kbn = @eigyou_kbn ") '--�c�Ƌ敪 ")
                paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 1, dicPrm("EigyouKbn")))
            End If
            '�c�ƒS������
            If Not dicPrm("EigyouTantouSyozaku").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	METS.syozoku_cd = @syozoku_cd ") '--�c�ƒS������ ")
                paramList.Add(MakeParam("@syozoku_cd", SqlDbType.Int, 10, dicPrm("EigyouTantouSyozaku")))
            End If
            '�Ƒ�
            If Not dicPrm("Gyoutai").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.gyoutai = @gyoutai ") '--�Ƒ� ")
                paramList.Add(MakeParam("@gyoutai", SqlDbType.Int, 10, dicPrm("Gyoutai")))
            End If
            '�N�ԓ���
            If (Not dicPrm("NenkanTousuu1").Equals(String.Empty)) AndAlso (Not dicPrm("NenkanTousuu2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keikaku_nenkan_tousuu BETWEEN @keikaku_nenkan_tousuu_from AND @keikaku_nenkan_tousuu_to ") '--�N�ԓ��� ")
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu_from", SqlDbType.Int, 5, dicPrm("NenkanTousuu1")))
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu_to", SqlDbType.Int, 5, dicPrm("NenkanTousuu2")))
            Else
                If Not dicPrm("NenkanTousuu1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.keikaku_nenkan_tousuu = @keikaku_nenkan_tousuu_from ") '--�N�ԓ��� ")
                    paramList.Add(MakeParam("@keikaku_nenkan_tousuu_from", SqlDbType.Int, 5, dicPrm("NenkanTousuu1")))
                End If
            End If
            '�v��p_�N�ԓ���
            If (Not dicPrm("KeikakuyouNenkanTousuu1").Equals(String.Empty)) AndAlso (Not dicPrm("KeikakuyouNenkanTousuu2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.keikakuyou_nenkan_tousuu BETWEEN @keikakuyou_nenkan_tousuu_from AND @keikakuyou_nenkan_tousuu_to ") '--�v��p_�N�ԓ��� ")
                paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu1")))
                paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_to", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu2")))
            Else
                If Not dicPrm("KeikakuyouNenkanTousuu1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKKI.keikakuyou_nenkan_tousuu = @keikakuyou_nenkan_tousuu_from ") '--�v��p_�N�ԓ��� ")
                    paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu1")))
                End If
            End If
            '�����X����1
            If Not dicPrm("KameitenZokusei1").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_1 = @kameiten_zokusei_1 ") '--�����X����1 ")
                paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 2, dicPrm("KameitenZokusei1")))
            End If
            '�����X����2
            If Not dicPrm("KameitenZokusei2").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_2 = @kameiten_zokusei_2 ") '--�����X����2 ")
                paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 2, dicPrm("KameitenZokusei2")))
            End If
            '�����X����3
            If Not dicPrm("KameitenZokusei3").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_3 = @kameiten_zokusei_3 ") '--�����X����3 ")
                paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 2, dicPrm("KameitenZokusei3")))
            End If
            '�����X����4
            If Not dicPrm("KameitenZokusei4").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_4 = @kameiten_zokusei_4 ") '--�����X����4 ")
                paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.Int, 2, dicPrm("KameitenZokusei4")))
            End If
            '�����X����5
            If Not dicPrm("KameitenZokusei5").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_5 = @kameiten_zokusei_5 ") '--�����X����2 ")
                paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.Int, 2, dicPrm("KameitenZokusei5")))
            End If
            '�����X����6
            If Not dicPrm("KameitenZokusei6").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_6 = @kameiten_zokusei_6 ") '--�����X����6 ")
                paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, dicPrm("KameitenZokusei6")))
            End If
            '�v��l�L��
            If Not dicPrm("Keikaku0Flg").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keikaku0_flg = @keikaku0_flg ") '--�v��l�L�� ")
                paramList.Add(MakeParam("@keikaku0_flg", SqlDbType.Int, 1, dicPrm("Keikaku0Flg")))
            End If
            .AppendLine("ORDER BY ")
            .AppendLine("	MKKI.kbn ASC ") '--�敪 ")
            .AppendLine("	,MKK.kameiten_cd ASC ") '--�����X�R�[�h ")
        End With

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameitenInfo", paramList.ToArray)

        Return dsReturn.Tables("dtKameitenInfo")

    End Function

    ''' <summary>
    ''' �����X���׌������擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKameitenCount(ByVal dicPrm As Dictionary(Of String, String)) As Integer

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dicPrm)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(MKK.kameiten_cd) AS CNT ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ") '--�v��Ǘ�_�����X�}�X�^ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED) ") '--�v��Ǘ�_�����X���}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MKK.kameiten_cd = MKKI.kameiten_cd ") '--�����X�R�[�h ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_eigyou_tantousya_syozoku_info AS METS WITH(READCOMMITTED) ") '--�c�ƒS����_�������}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MKK.eigyou_tantousya_id = METS.eigyou_tantousya_id ") '--�c�ƒS���Җ� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_TORIKESI WITH(READCOMMITTED) ") '--�g�����̃}�X�^(���) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_TORIKESI.meisyou_syubetu = '56' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_TORIKESI.code = MKK.torikesi ") '--��� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_EIGYOU_KBN WITH(READCOMMITTED) ") '--���̃}�X�^(�c�Ƌ敪) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_EIGYOU_KBN.meisyou_syubetu = '05' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_EIGYOU_KBN.code = MKK.eigyou_kbn ") '--�c�Ƌ敪 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_GYOUTAI WITH(READCOMMITTED) ") '--���̃}�X�^(�Ƒ�) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_GYOUTAI.meisyou_syubetu = '20' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_GYOUTAI.code = MKKI.gyoutai ") '--�Ƒ� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI1 WITH(READCOMMITTED) ") '--���̃}�X�^(�����X����1) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI1.meisyou_syubetu = '21' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI1.code = MKKI.kameiten_zokusei_1 ") '--�����X����1 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI2 WITH(READCOMMITTED) ") '--���̃}�X�^(�����X����2) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI2.meisyou_syubetu = '22' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI2.code = MKKI.kameiten_zokusei_2 ") '--�����X����2 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM_ZOKUSEI3 WITH(READCOMMITTED) ") '--���̃}�X�^(�����X����3) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM_ZOKUSEI3.meisyou_syubetu = '23' ")
            .AppendLine("		AND ")
            .AppendLine("		MKM_ZOKUSEI3.code = MKKI.kameiten_zokusei_3 ") '--�����X����3 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_ZOKUSEI4 WITH(READCOMMITTED) ") '--�g�����̃}�X�^(�����X����4) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_ZOKUSEI4.meisyou_syubetu = '21' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_ZOKUSEI4.code = MKKI.kameiten_zokusei_4 ") '--�����X����4 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_kakutyou_meisyou AS MKKM_ZOKUSEI5 WITH(READCOMMITTED) ") '--�g�����̃}�X�^(�����X����5) ")
            .AppendLine("	ON ")
            .AppendLine("		MKKM_ZOKUSEI5.meisyou_syubetu = '22' ")
            .AppendLine("		AND ")
            .AppendLine("		MKKM_ZOKUSEI5.code = MKKI.kameiten_zokusei_5 ") '--�����X����5 ")
            .AppendLine("WHERE ")
            .AppendLine("	MKK.keikaku_nendo = @keikaku_nendo ") '--�v��N�x ")
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, dicPrm("KeikakuNendo")))
            '�敪
            If Not dicPrm("Kbn").Equals(String.Empty) Then
                Dim arrKbn() As String = dicPrm("Kbn").Split(CChar(","))
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kbn IN ") '--�敪 ")
                .AppendLine("	( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("       @kbn" & i)
                    Else
                        .AppendLine("       ,@kbn" & i)
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.VarChar, 1, arrKbn(i)))
                Next
                .AppendLine("   ) ")
            End If
            '���
            If Not dicPrm("Torikesi").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.torikesi = @torikesi ") '--��� ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, dicPrm("Torikesi")))
            End If
            '�����X��
            If Not dicPrm("KameitenMei").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.kameiten_mei LIKE @kameiten_mei ") '--�����X�� ")
                paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 41, dicPrm("KameitenMei") & "%"))
            End If
            '�����X�R�[�h
            If (Not dicPrm("KameitenCd1").Equals(String.Empty)) AndAlso (Not dicPrm("KameitenCd2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ") '--�����X�R�[�h ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 16, dicPrm("KameitenCd1")))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 16, dicPrm("KameitenCd2")))
            Else
                If Not dicPrm("KameitenCd1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.kameiten_cd = @kameiten_cd_from ") '--�����X�R�[�h ")
                    paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 16, dicPrm("KameitenCd1")))
                End If
            End If
            '�c�Ə��R�[�h
            If (Not dicPrm("EigyousyaCd1").Equals(String.Empty)) AndAlso (Not dicPrm("EigyousyaCd2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ") '--�c�Ə��R�[�h ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd1")))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd2")))
            Else
                If Not dicPrm("EigyousyaCd1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.eigyousyo_cd = @eigyousyo_cd_from ") '--�c�Ə��R�[�h ")
                    paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dicPrm("EigyousyaCd1")))
                End If
            End If
            '�n��R�[�h
            If Not dicPrm("KeiretuCd").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keiretu_cd = @keiretu_cd ") '--�n��R�[�h ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dicPrm("KeiretuCd")))
            End If
            '�Ǌ��x�X
            If Not dicPrm("Siten").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.busyo_cd = @busyo_cd ") '--�Ǌ��x�X ")
                paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 16, dicPrm("Siten")))
            End If
            '�s���{���R�[�h
            If Not dicPrm("TodoufukenCd").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.todouhuken_cd = @todouhuken_cd ") '--�s���{���R�[�h ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dicPrm("TodoufukenCd")))
            End If
            '�c�ƒS����
            If Not dicPrm("EigyouTantousya").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyou_tantousya_id = @eigyou_tantousya_id ") '--�c�ƒS���� ")
                paramList.Add(MakeParam("@eigyou_tantousya_id", SqlDbType.VarChar, 30, dicPrm("EigyouTantousya")))
            End If
            '�c�Ƌ敪
            If Not dicPrm("EigyouKbn").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.eigyou_kbn = @eigyou_kbn ") '--�c�Ƌ敪 ")
                paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 1, dicPrm("EigyouKbn")))
            End If
            '�c�ƒS������
            If Not dicPrm("EigyouTantouSyozaku").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	METS.syozoku_cd = @syozoku_cd ") '--�c�ƒS������ ")
                paramList.Add(MakeParam("@syozoku_cd", SqlDbType.Int, 10, dicPrm("EigyouTantouSyozaku")))
            End If
            '�Ƒ�
            If Not dicPrm("Gyoutai").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.gyoutai = @gyoutai ") '--�Ƒ� ")
                paramList.Add(MakeParam("@gyoutai", SqlDbType.Int, 10, dicPrm("Gyoutai")))
            End If
            '�N�ԓ���
            If (Not dicPrm("NenkanTousuu1").Equals(String.Empty)) AndAlso (Not dicPrm("NenkanTousuu2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keikaku_nenkan_tousuu BETWEEN @keikaku_nenkan_tousuu_from AND @keikaku_nenkan_tousuu_to ") '--�N�ԓ��� ")
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu_from", SqlDbType.Int, 5, dicPrm("NenkanTousuu1")))
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu_to", SqlDbType.Int, 5, dicPrm("NenkanTousuu2")))
            Else
                If Not dicPrm("NenkanTousuu1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKK.keikaku_nenkan_tousuu = @keikaku_nenkan_tousuu_from ") '--�N�ԓ��� ")
                    paramList.Add(MakeParam("@keikaku_nenkan_tousuu_from", SqlDbType.Int, 5, dicPrm("NenkanTousuu1")))
                End If
            End If
            '�v��p_�N�ԓ���
            If (Not dicPrm("KeikakuyouNenkanTousuu1").Equals(String.Empty)) AndAlso (Not dicPrm("KeikakuyouNenkanTousuu2").Equals(String.Empty)) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.keikakuyou_nenkan_tousuu BETWEEN @keikakuyou_nenkan_tousuu_from AND @keikakuyou_nenkan_tousuu_to ") '--�v��p_�N�ԓ��� ")
                paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu1")))
                paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_to", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu2")))
            Else
                If Not dicPrm("KeikakuyouNenkanTousuu1").Equals(String.Empty) Then
                    .AppendLine("	AND ")
                    .AppendLine("	MKKI.keikakuyou_nenkan_tousuu = @keikakuyou_nenkan_tousuu_from ") '--�v��p_�N�ԓ��� ")
                    paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, dicPrm("KeikakuyouNenkanTousuu1")))
                End If
            End If
            '�����X����1
            If Not dicPrm("KameitenZokusei1").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_1 = @kameiten_zokusei_1 ") '--�����X����1 ")
                paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 2, dicPrm("KameitenZokusei1")))
            End If
            '�����X����2
            If Not dicPrm("KameitenZokusei2").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_2 = @kameiten_zokusei_2 ") '--�����X����2 ")
                paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 2, dicPrm("KameitenZokusei2")))
            End If
            '�����X����3
            If Not dicPrm("KameitenZokusei3").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_3 = @kameiten_zokusei_3 ") '--�����X����3 ")
                paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 2, dicPrm("KameitenZokusei3")))
            End If
            '�����X����4
            If Not dicPrm("KameitenZokusei4").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_4 = @kameiten_zokusei_4 ") '--�����X����4 ")
                paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.Int, 2, dicPrm("KameitenZokusei4")))
            End If
            '�����X����5
            If Not dicPrm("KameitenZokusei5").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_5 = @kameiten_zokusei_5 ") '--�����X����2 ")
                paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.Int, 2, dicPrm("KameitenZokusei5")))
            End If
            '�����X����6
            If Not dicPrm("KameitenZokusei6").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKKI.kameiten_zokusei_6 = @kameiten_zokusei_6 ") '--�����X����6 ")
                paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, dicPrm("KameitenZokusei6")))
            End If
            '�v��l�L��
            If Not dicPrm("Keikaku0Flg").Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	MKK.keikaku0_flg = @keikaku0_flg ") '--�v��l�L�� ")
                paramList.Add(MakeParam("@keikaku0_flg", SqlDbType.Int, 1, dicPrm("Keikaku0Flg")))
            End If
        End With

        ' �������s
        Dim intCount As Integer
        intCount = Convert.ToInt32(SQLHelper.ExecuteScalar(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray))

        Return intCount

    End Function


End Class
