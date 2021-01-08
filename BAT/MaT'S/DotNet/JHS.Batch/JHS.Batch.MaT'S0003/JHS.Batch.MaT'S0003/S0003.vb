Option Explicit On
Option Strict On

Imports System.Data
Imports System.Text
Imports System.IO
Imports System.Transactions
Imports JHS.Batch.SqlExecutor
Imports JHS.Batch
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

''' <summary>
''' �N�x�䗦�Ǘ��f�[�^��N�x�䗦�Ǘ��e�[�u���Ɋi�[����
''' </summary>
''' <remarks>JHS Earth ���� �� �����X�}�X�^�@�̓��e�@��
'''          �v��Ǘ�_�����X�}�X�^�@�p�ɉ��H���ā@�o�^����</remarks>
''' <history>
''' <para>2013/1/15 ��A/�k�o �V�K�쐬 P-45026</para>
''' </history>
Public Class S0003

#Region "�ϐ�"
    '�eEvent/Method�̓��쎞�ɂ�����A"EMAB��Q�Ή����̊i�[����"�����A���N���X��
    Private ReadOnly mMyNamePeriod As String = MyClass.GetType.FullName
    'DB�ڑ��X�g�����O
    Private mDBconnectionEarth As String
    Private mDBconnectionJHS As String
    'DB�ڑ�
    Private mConnectionEarth As SqlExecutor
    Private mConnectionJHS As SqlExecutor

    '���O���b�Z�[�W
    Private mLogMsg As New StringBuilder()
    '�V�K����
    Private mInsCount As Integer = 0
#End Region

#Region "Main����"
    ''' <summary>
    ''' Main����
    ''' </summary>
    ''' <param name="argv"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Public Shared Function Main(ByVal argv As String()) As Integer
        Dim btProcess As S0003

        '������
        btProcess = New S0003()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(btProcess.mMyNamePeriod & MyMethod.GetCurrentMethod.Name, argv)

        Try
            'DB�ڑ��X�g�����O
            btProcess.mDBconnectionEarth = Definition.GetConnectionStringEarth()
            btProcess.mDBconnectionJHS = Definition.GetConnectionStringJHS()

            'DB�ڑ�
            btProcess.mConnectionEarth = New SqlExecutor(btProcess.mDBconnectionEarth)
            btProcess.mConnectionJHS = New SqlExecutor(btProcess.mDBconnectionJHS)

            '�又�����Ăэ���()
            Call btProcess.Main_Process()

            Return 0
        Catch ex As Exception
            Dim strErrorMsg As String = ""
            If ex.Data.Item("ERROR_LOG") IsNot Nothing Then
                strErrorMsg = Convert.ToString(ex.Data.Item("ERROR_LOG"))
                btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & strErrorMsg)
            End If

            '�ُ�𔭐�����ꍇ�A���O�t�@�C���ɏo�͂���
            btProcess.mLogMsg.AppendLine(ex.Message)

            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & "��L�̏��������[���o�b�N���܂����B")
            btProcess.mInsCount = 0
            Return 9
        Finally
            If btProcess.mConnectionEarth IsNot Nothing Then
                'DB�ڑ��̃O���[�Y
                btProcess.mConnectionEarth.Close()
                btProcess.mConnectionEarth.Dispose()
            End If

            If btProcess.mConnectionJHS IsNot Nothing Then
                'DB�ڑ��̃O���[�Y
                btProcess.mConnectionJHS.Close()
                btProcess.mConnectionJHS.Dispose()
            End If

            '�V�K���������O�t�@�C���ɏo�͂���
            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
            "�v��Ǘ�_�����X�}�X�^�e�[�u���ɍ��v�F" & _
            Convert.ToString(btProcess.mInsCount) & _
            "���f�[�^���}������܂����B")

            '���O�o��
            Console.WriteLine(btProcess.mLogMsg.ToString())
        End Try
    End Function

#End Region

#Region "�又��"
    ''' <summary>
    ''' �又��
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Sub Main_Process()
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name)

        Dim strYear As String                   '�v��N�x
        Dim dtEarthData As DataTable            '�d���������f�[�^
        Dim dtAspsfaData As DataTable            '��A���f�[�^
        Dim options As New Transactions.TransactionOptions
        Dim j As Integer

        '�o�b�`��������
        strYear = Convert.ToString(DateAdd(DateInterval.Month, -3, Now).Year)
        'strYear = Now.Year.ToString

        mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                            "�d���������̌v��Ǘ�_�����X�f�[�^�̎擾�������J�n���܂����B")

        'DB�ڑ��̃I�[�v��
        mConnectionEarth.Open()
        mConnectionJHS.Open(True)

        Try
            '�d���������̌v��Ǘ�_�����X�f�[�^���擾����
            dtEarthData = SelEarthData(mConnectionEarth, strYear)

            If dtEarthData IsNot Nothing Then
                mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                                    "�d���������̃f�[�^����" & _
                                    Convert.ToString(dtEarthData.Rows.Count) & _
                                    "���f�[�^���擾����܂����B")
            End If

            '2013/10/10 ���F�ǉ��@��
            mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                              "��A���̌v��Ǘ�_�����X�f�[�^�̎擾�������J�n���܂����B")

            '��A���̃f�[�^���擾����
            dtAspsfaData = SelASPSFAData()

            If dtAspsfaData IsNot Nothing Then
                mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                                    "��A���̃f�[�^����" & _
                                    Convert.ToString(dtAspsfaData.Rows.Count) & _
                                    "���f�[�^���擾����܂����B")
            End If
            '2013/10/10 ���F�ǉ��@��


            '�Y���N�x�̃f�[�^���폜����
            DelJHSData(mConnectionJHS, strYear)

            Dim strKameiten As String = String.Empty
            Dim dtKameiten As Data.DataTable
            Dim intCount As Integer = 0
            Dim i As Integer
            dtKameiten = SelKeikakuKameiten(mConnectionJHS, strYear)
            '�v��l�s�ς̉����X�̎擾
            For i = 0 To dtKameiten.Rows.Count - 1
                strKameiten = strKameiten & "," & dtKameiten.Rows(i).Item(0).ToString
            Next

            Dim dtEigyouKbn As Data.DataTable
            Dim blnEigyouKbn As Boolean
            dtEigyouKbn = SelKeikakuyouMeisyou(mConnectionJHS)

            '�d���������̌v��Ǘ�_�����X�f�[�^�ɂ��A���[�v����
            For j = 0 To dtEarthData.Rows.Count - 1
                blnEigyouKbn = False
                For i = 0 To dtEigyouKbn.Rows.Count - 1
                    If dtEarthData.Rows(j).Item("�c�Ƌ敪").ToString = dtEigyouKbn.Rows(i).Item("meisyou").ToString Then
                        '�c�Ƌ敪�̃Z�b�g
                        dtEarthData.Columns("�c�Ƌ敪").ReadOnly = False
                        dtEarthData.Rows(j).Item("�c�Ƌ敪") = dtEigyouKbn.Rows(i).Item("code").ToString
                        blnEigyouKbn = True
                        Exit For
                    End If
                Next

                If strKameiten.IndexOf(dtEarthData.Rows(j).Item("�����X����").ToString) = -1 AndAlso blnEigyouKbn Then

                    dtEarthData.Columns("�v��l0FLG").ReadOnly = False
                    '�����X���v��l�s�ς̉����X����Ȃ��ꍇ�A�}������
                    SetJHSData(dtEarthData.Rows(j), strYear)
                    intCount = intCount + 1
                End If

            Next

            '2013/10/10 ���F�C���@��
            '�V�K���������O�t�@�C���ɏo�͂���
            mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
            "�v��Ǘ�_�����X�}�X�^�e�[�u����" & _
            Convert.ToString(intCount) & _
            "���d���������̃f�[�^���}������܂����B")
            '2013/10/10 ���F�C���@��

            '2013/10/10 ���F�폜�@��
            'mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
            '    "��A���̌v��Ǘ�_�����X�f�[�^�̎擾�������J�n���܂����B")

            ''��A���̃f�[�^���擾����
            'dtAspsfaData = SelASPSFAData()

            'If dtAspsfaData IsNot Nothing Then
            '    mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
            '                        "��A���̃f�[�^����" & _
            '                        Convert.ToString(dtAspsfaData.Rows.Count) & _
            '                        "���f�[�^���擾����܂����B")
            'End If
            '2013/10/10 ���F�폜�@��

            intCount = 0
            For j = 0 To dtAspsfaData.Rows.Count - 1

                If SelKeikakuKameitenCount(mConnectionJHS, strYear, dtAspsfaData.Rows(j).Item("�����X����").ToString).Rows(0).Item(0).Equals(0) Then
                    '�d�������������XϽ��@�ɂȂ� 
                    '��A���V�X�e���́@�����XϽ��@�̏����@�v��Ǘ��p�̉����XϽ����쐬����
                    dtAspsfaData.Rows(j).Item("�o�^����") = SetDateTime(dtAspsfaData.Rows(j).Item("�o�^����").ToString)
                    dtAspsfaData.Rows(j).Item("�X�V����") = SetDateTime(dtAspsfaData.Rows(j).Item("�X�V����").ToString)
                    SetJHSData(dtAspsfaData.Rows(j), strYear)
                    intCount = intCount + 1

                End If

            Next

            '2013/10/10 ���F�C���@��
            '�V�K���������O�t�@�C���ɏo�͂���
            mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
            "�v��Ǘ�_�����X�}�X�^�e�[�u����" & _
            Convert.ToString(intCount) & _
            "����A���̃f�[�^���}������܂����B")
            '2013/10/10 ���F�C���@��

            '�f�[�^���������
            If dtEarthData IsNot Nothing Then
                dtEarthData.Dispose()
                dtEarthData = Nothing
            End If

            If dtAspsfaData IsNot Nothing Then
                dtAspsfaData.Dispose()
                dtAspsfaData = Nothing
            End If

            '�����̏ꍇ
            mConnectionJHS.Commit()

        Catch ex As Exception
            '���s�̏ꍇ
            mConnectionJHS.Rollback()
            Throw ex
        End Try
        'End Using
    End Sub
#End Region

#Region "�f�[�^�ҏW����"

    ''' <summary>
    ''' �f�[�^�ҏW����
    ''' </summary>
    ''' <param name="drData">�d���������̃f�[�^</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Sub SetJHSData(ByVal drData As DataRow, ByVal strYear As String)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name, drData)

        Dim i As Integer
        Dim dtKeikakuKensuu As Data.DataTable
        dtKeikakuKensuu = SelKeikakuKanri(mConnectionJHS, strYear, drData.Item("�����X����").ToString)

        Dim intKeikaku0Flg As Integer = 1
        For i = 0 To dtKeikakuKensuu.Rows.Count - 1
            '�v��l0FLG�̃Z�b�g
            If Convert.ToInt32(dtKeikakuKensuu.Rows(i).Item(0).ToString) > 0 Then
                intKeikaku0Flg = 0
                Exit For
            End If
        Next

        '�v��l0FLG�̃Z�b�g
        drData.Item("�v��l0FLG") = intKeikaku0Flg.ToString

        '�󒍃f�[�^���[�N�V�K����
        mInsCount = mInsCount + InsJHSData(mConnectionJHS, drData, strYear)

    End Sub

#End Region

#Region "SQL��"
    ''' <summary>
    ''' �d���������̃f�[�^���擾����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strYear">�Ώ۔N�x</param>
    ''' <returns>�v��Ǘ�_�����X�f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function SelEarthData(ByVal objConnection As SqlExecutor, _
                                  ByVal strYear As String) As Data.DataTable
        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�J�n�N�x
        Dim strBeginYear As String
        Dim strEndYear As String
        strBeginYear = strYear & "0401"
        strEndYear = DateAdd(DateInterval.Year, 1, Convert.ToDateTime(strYear & "/03/31")).ToString("yyyyMMdd")
        '�n��R�[�h
        Dim strKeiretuCd As String
        strKeiretuCd = Definition.GetKeiretuCd

        '2013/10/23 ���F�ǉ��@��
        '�敪
        Dim strKubun As String
        strKubun = Definition.GetKubunName3
        '2013/10/23 ���F�ǉ��@��
        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" SELECT")
            '    .AppendLine(" 	k.kameiten_cd �����X���� ")
            '    .AppendLine(" 	,k.torikesi ��� ")
            '    .AppendLine(" 	,k.hattyuu_teisi_flg ������~FLG ")
            '    .AppendLine(" 	,CASE WHEN k.kbn in ('A','C') THEN --�敪���@A�AC�@�̏ꍇ   �uFC�v ")
            '    .AppendLine(" 		'FC' ")
            '    .AppendLine(" 	ELSE ")
            '    '2013/10/10 ���F�폜�@��
            '    '.AppendLine(" 		CASE WHEN CONVERT(varchar(100), k.add_datetime, 112) <= @endYear AND CONVERT(varchar(100), k.add_datetime, 112) >= @beginYear  ")
            '    '.AppendLine(" 				  OR replace(KJ.add_nengetu,'/','')+'01' <= @endYear AND replace(KJ.add_nengetu,'/','')+'01' >= @beginYear THEN ")
            '    '.AppendLine(" 			'�V�K' ")
            '    '.AppendLine(" 		ELSE ")
            '    '.AppendLine(" 			case when k.keiretu_cd in ('TAMA','REOH','LEOP','ACEH','0001','6100','6800') then  ")
            '    '2013/10/10 ���F�폜�@��
            '    .AppendLine(" 			case when k.keiretu_cd in (" & strKeiretuCd & ") then  ")
            '    .AppendLine(" 				'����'  ")
            '    .AppendLine(" 			else  ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine(" 				CASE WHEN k.kbn in (" & strKubun & ") THEN ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine(" 					'�c��'  ")
            '    .AppendLine(" 				END ")
            '    .AppendLine(" 			end  ")
            '    '2013/10/10 ���F�폜�@��
            '    '.AppendLine(" 		END ")
            '    '2013/10/10 ���F�폜�@��
            '    .AppendLine(" 	END �c�Ƌ敪 ")
            '    .AppendLine(" 	,k.kameiten_mei1 �����X�� ")
            '    .AppendLine(" 	,case when b1.sosiki_level=4 then b1.busyo_mei else b2.busyo_mei end �x�X��/*�����������x�X�ł͂Ȃ��Ƃ��́A��ʕ������擾*/ ")
            '    .AppendLine(" 	,case when b1.sosiki_level=4 then b1.busyo_cd else b2.busyo_cd end �������� ")
            '    .AppendLine(" 	,k.eigyou_tantousya_mei �c�ƒS����  ")
            '    .AppendLine(" 	,mb.displayname �c�ƒS���Җ� ")
            '    .AppendLine(" 	,NULL �K�v�ʒk�� ")
            '    .AppendLine(" 	,case when b1.sosiki_level=5 then b1.busyo_cd else null end �c�Ə�_�������� ")
            '    .AppendLine(" 	,case when b1.sosiki_level=5 then b1.busyo_mei else null end �c�Ə���/*�����������x�X�ł͂Ȃ��Ƃ��́Anull���Z�b�g*/ ")
            '    .AppendLine(" 	,k.todouhuken_cd �s���{������ ")
            '    .AppendLine(" 	,f.todouhuken_mei �s���{���� ")
            '    .AppendLine(" 	,k.keiretu_cd �n���� ")
            '    .AppendLine(" 	,ke.keiretu_mei �n�� ")
            '    .AppendLine(" 	,k.eigyousyo_cd �c�Ə����� ")
            '    .AppendLine(" 	,k.nenkan_tousuu �N�ԓ��� ")
            '    .AppendLine(" 	,NULL �O�N����_�N�ԓ��� ")
            '    .AppendLine(" 	,null �v��l0FLG ")
            '    .AppendLine(" 	,KJ.add_nengetu �����X�o�^�N�� ")
            '    '2013/10/10 ���F�C���@��
            '    '.AppendLine(" 	,CASE WHEN CONVERT(varchar(100), k.add_datetime, 112) <= @endYear AND CONVERT(varchar(100), k.add_datetime, 112) >= @beginYear THEN ")
            '    '.AppendLine(" 		'1' ")
            '    '.AppendLine(" 	ELSE ")
            '    '.AppendLine(" 		'0'  ")
            '    '.AppendLine(" 	END �����X�V�KFLG ")
            '    .AppendLine("	,CASE WHEN KJ.add_nengetu IS NULL THEN")
            '    .AppendLine("		NULL")
            '    .AppendLine("	ELSE")
            '    .AppendLine("		CASE WHEN (REPLACE(KJ.add_nengetu,'/','') + '01') BETWEEN @beginYear AND @endYear THEN ")
            '    .AppendLine("			'1'  ")
            '    .AppendLine("		ELSE  ")
            '    .AppendLine("			'0'  ")
            '    .AppendLine("		END ")
            '    .AppendLine("	END �����X�V�KFLG  ")
            '    '2013/10/10 ���F�C���@��
            '    .AppendLine(" 	,isnull(tmax.naiyou,'') �Ƒ� ")
            '    .AppendLine(" 	,null �������50��FLG ")
            '    .AppendLine(" 	,null �������50��FLG ")
            '    .AppendLine(" 	,null �������50��FLG ")
            '    .AppendLine(" 	,0 �v��l�s��FLG ")
            '    .AppendLine(" 	,k.add_login_user_id �o�^ID ")
            '    .AppendLine(" 	,k.add_datetime �o�^���� ")
            '    .AppendLine(" 	,k.upd_login_user_id �X�VID ")
            '    .AppendLine(" 	,k.upd_datetime �X�V���� ")
            '    .AppendLine(" from m_kameiten k with(readuncommitted) ")
            '    .AppendLine(" LEFT JOIN m_kameiten_jyuusyo KJ  ")
            '    .AppendLine(" 	ON KJ.kameiten_cd = K.kameiten_cd ")
            '    .AppendLine(" 	AND KJ.jyuusyo_no = '1' ")
            '    .AppendLine(" LEFT JOIN m_todoufuken f  ")
            '    .AppendLine(" 	ON f.todouhuken_cd = k.todouhuken_cd --�s���{���}�X�^ ")
            '    .AppendLine(" LEFT JOIN m_busyo_kanri b1  ")
            '    .AppendLine(" 	ON b1.busyo_cd = f.busyo_cd /*�s���{���̒�������*/ ")
            '    .AppendLine(" LEFT JOIN m_busyo_kanri b2  ")
            '    .AppendLine(" 	ON b2.busyo_cd = b1.joui_soiki /*���������̏�ʑg�D�����i�����������x�X�ł͂Ȃ��ꍇ�A�x�X���擾�ł���j*/ ")
            '    .AppendLine(" LEFT JOIN m_jhs_mailbox mb with(readuncommitted)  ")
            '    .AppendLine(" 	ON k.eigyou_tantousya_mei=mb.aliasname ")
            '    .AppendLine(" LEFT JOIN m_keiretu ke --�n��}�X�^ ")
            '    .AppendLine(" 	ON ke.kbn = k.kbn ")
            '    .AppendLine(" 	AND ke.keiretu_cd = k.keiretu_cd  ")
            '    .AppendLine(" /*�����X���ӎ����}�X�^�̒��ӎ������61�̍ő����NO�̓��e�擾�B100�ȉ��E100�������E100�������EHM���擾�ł���*/ ")
            '    .AppendLine(" LEFT JOIN ( ")
            '    .AppendLine(" 	 select t.kameiten_cd,naiyou from m_kameiten_tyuuijikou t with(readuncommitted)  ")
            '    .AppendLine(" 	 inner join ( ")
            '    .AppendLine(" 		 select kameiten_cd,max(nyuuryoku_no) maxno from m_kameiten_tyuuijikou with(readuncommitted)  ")
            '    .AppendLine(" 		 where tyuuijikou_syubetu='61' group by kameiten_cd ")
            '    .AppendLine(" 	 ) t61 on t.kameiten_cd=t61.kameiten_cd and t.nyuuryoku_no=t61.maxno ")
            '    .AppendLine(" ) tmax on k.kameiten_cd=tmax.kameiten_cd ")
            '    .AppendLine(" where ")
            '    .AppendLine(" 	    k.kbn in ('A','C') /*FC�̏ꍇ*/ ")
            '    '2013/10/10 ���F�폜�@��
            '    '.AppendLine(" 		OR CONVERT(varchar(100), k.add_datetime, 112) <= @endYear AND CONVERT(varchar(100), k.add_datetime, 112) >= @beginYear /*�V�K�̏ꍇ*/ ")
            '    '.AppendLine(" 		OR replace(KJ.add_nengetu,'/','')+'01' <= @endYear AND replace(KJ.add_nengetu,'/','')+'01' >= @beginYear /*�V�K�̏ꍇ*/ ")
            '    '2013/10/10 ���F�폜�@��
            '    '.AppendLine(" 		OR k.keiretu_cd in ('TAMA','REOH','LEOP','ACEH','0001','6100','6800') /*���̂̏ꍇ*/ ")
            '    .AppendLine(" 		OR k.keiretu_cd in (" & strKeiretuCd & ") /*���̂̏ꍇ*/ ")
            '    .AppendLine(" 							/*�n��'TAMA','REOH','LEOP','ACEH','0001','6100','6800'��E�EG�ES�����Ȃ��̂ō��͂����OK�i����͈ȊO�̌n����ǉ��ɂȂ�\���L�Ȃ̂Œ��Ӂj*/ ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine(" 		OR k.kbn in (" & strKubun & ") /*�c�Ƃ̏ꍇ*/ ")
            '    '2013/10/23 ���F�C���@��
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_01)
            strSql = strSql.Replace("@strKeiretuCd", strKeiretuCd)
            strSql = strSql.Replace("@strKubun", strKubun)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@beginYear", SqlDbType.VarChar, 8, strBeginYear))      '�J�n�N
            paramList.Add(MakeParam("@endYear", SqlDbType.VarChar, 8, strEndYear))          '�I���N

            '�d���������̃f�[�^
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�d���������̃f�[�^�̎擾�������ُ�I�����܂����B")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ��A���V�X�e���̃f�[�^���擾����
    ''' </summary>
    ''' <returns>�v��Ǘ�_�����X�f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function SelASPSFAData() As Data.DataTable

        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder
        Dim ds As New Data.DataSet

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine("	SELECT ")
            '    .AppendLine("		SFUM.UCCRPCOD AS �����X���� ")
            '    .AppendLine("	,	SFUM.UCDELFLG AS ��� ")
            '    .AppendLine("	,	9 AS ������~FLG ")
            '    '2013/10/10 ���F�C���@��
            '    .AppendLine("	,	CASE WHEN UCSTROT8 IS NOT NULL THEN ")
            '    .AppendLine("			CASE WHEN SUBSTR(UCSTROT8,-1) IN ('0','1','2','3','4','5','6','7','8','9') THEN")
            '    .AppendLine("               to_char(SUBSTR(UCSTROT8, -1))")
            '    .AppendLine("			ELSE")
            '    .AppendLine("               '0'")
            '    .AppendLine("           End")
            '    .AppendLine("		ELSE")
            '    .AppendLine("           '0'")
            '    .AppendLine("		END AS �c�Ƌ敪 ")
            '    '.AppendLine("	,	2 AS �c�Ƌ敪			--�u2:�V�K�v�Œ� ")
            '    '2013/10/10 ���F�C���@��
            '    .AppendLine("	,	SFUM.UCCRPNAM AS �����X�� ")
            '    .AppendLine("	,	CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '04' THEN SFOM.OGPSTNAM ")
            '    .AppendLine("		     ELSE SFOM.SUB_OGPSTNAM END AS �x�X�� ")
            '    .AppendLine("	,	CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '04' THEN SFOM.OGPSTCOD ")
            '    .AppendLine("		     ELSE SFOM.SUB_OGPSTCOD END AS �������� ")
            '    .AppendLine("	,	SOM1.OEUSRNMR AS �c�ƒS����  ")
            '    .AppendLine("	,	SOM1.OEBASLID AS �c�ƒS���Җ� ")
            '    .AppendLine("	,	NULL AS �K�v�ʒk�� ")
            '    .AppendLine("	,	CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '05' THEN SFOM.OGPSTCOD ")
            '    .AppendLine("		     ELSE NULL END AS �c�Ə�_�������� ")
            '    .AppendLine("	,	CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '05' THEN SFOM.OGPSTNAM ")
            '    .AppendLine("		     ELSE NULL END AS �c�Ə��� ")
            '    .AppendLine("	,	NULL AS �s���{������ ")
            '    .AppendLine("	,	NULL AS �s���{���� ")
            '    .AppendLine("	,	NULL AS �n���� ")
            '    .AppendLine("	,	NULL AS �n�� ")
            '    .AppendLine("	,	NULL AS �c�Ə����� ")
            '    .AppendLine("	,	NULL AS �N�ԓ��� ")
            '    .AppendLine("	,	NULL AS �O�N����_�N�ԓ��� ")
            '    .AppendLine("	,	NULL AS �v��l0FLG ")
            '    .AppendLine("	,	NULL AS �����X�o�^�N�� ")
            '    '2013/10/10 ���F�C���@��
            '    .AppendLine("	,	'1' AS �����X�V�KFLG ")
            '    '.AppendLine("	,	NULL AS �����X�V�KFLG ")
            '    '2013/10/10 ���F�C���@��
            '    .AppendLine("	,	NULL AS �Ƒ� ")
            '    .AppendLine("	,	NULL AS �������50��FLG ")
            '    .AppendLine("	,	NULL AS �������50��FLG ")
            '    .AppendLine("	,	NULL AS �������50��FLG ")
            '    .AppendLine("	,	NULL AS �o�^ID ")
            '    .AppendLine("	,	SFUM.UCMAKDAT AS �o�^���� ")
            '    .AppendLine("	,	NULL AS �X�VID ")
            '    .AppendLine("	,	SFUM.UCDBADAT AS �X�V���� ")
            '    .AppendLine("FROM SFAMT_USRCORP_MVR  SFUM				--��A���̉����XϽ� ")
            '    .AppendLine("	LEFT JOIN ( ")
            '    .AppendLine("	    SELECT SFOM.OGPSTSEQ,						--����No ")
            '    .AppendLine("	           SFOM.OGPSTCOD,						--�����R�[�h ")
            '    .AppendLine("	           SFOM.OGPSTNAM,						--������ ")
            '    .AppendLine("	           SUB_SFOM.OGPSTCOD AS SUB_OGPSTCOD,	--��ʑg�D�̕����R�[�h ")
            '    .AppendLine("	           SUB_SFOM.OGPSTNAM AS SUB_OGPSTNAM	--��ʑg�D�̕����� ")
            '    .AppendLine("	    FROM SFAMT_OWNPOSGRP_MVR  SFOM			--��A���̕���Ͻ� ")
            '    .AppendLine("	    LEFT JOIN SFAMT_OWNPOSGRP_MVR  SUB_SFOM	--��A���̕���Ͻ� ")
            '    .AppendLine("	    ON SFOM.OGPHLSEQ = SUB_SFOM.OGPSTSEQ		--��ʑg�D ")
            '    .AppendLine("	)  SFOM ")
            '    .AppendLine("	ON SFUM.UCPSTSEQ = SFOM.OGPSTSEQ				--����No ")
            '    .AppendLine("	LEFT JOIN ( ")
            '    .AppendLine("		SELECT MIN(OGEMPSEQ) OGEMPSEQ ")
            '    .AppendLine("			   ,OGCRPSEQ	 ")
            '    .AppendLine("		FROM SFAMT_OWNCSTCHG_MVR ")
            '    .AppendLine("		GROUP BY OGCRPSEQ ")
            '    .AppendLine("		) SOM				--��A���̉����X_�S����Ͻ�  ")
            '    .AppendLine("	ON SFUM.UCCRPSEQ = SOM.OGCRPSEQ					--�S���R�t������  ")
            '    .AppendLine("	LEFT JOIN SFAMT_OWNEMP_MVR SOM1					--��A���̒S����Ͻ�  ")
            '    .AppendLine("	ON SOM.OGEMPSEQ = SOM1.OEEMPSEQ					--�S���Һ��� ")
            '    .AppendLine("	WHERE length(nvl(SFUM.UCCRPCOD,'')) = 8	--8�� �œo�^�̂���R�[�h���擾���� ")
            '    '2013/10/10 ���F�ǉ��@��
            '    .AppendLine("         AND SFUM.UCDELFLG = '0'")
            '    '2013/10/10 ���F�ǉ��@��
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_02)
            sqlBuffer.AppendLine(strSql)

            '��A���̃f�[�^
            ORCLHelper.FillDataset(Definition.GetConnectionStringASPSFA, CommandType.Text, sqlBuffer.ToString, ds, "dtKeikakuKameiten")
            Return ds.Tables("dtKeikakuKameiten")

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "��A���̃f�[�^�̎擾�������ُ�I�����܂����B")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' JHS�̃f�[�^���폜����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strYear">�Ώ۔N�x</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Sub DelJHSData(ByVal objConnection As SqlExecutor, _
                           ByVal strYear As String)

        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" delete from ")
            '    .AppendLine("     m_keikaku_kameiten ")
            '    .AppendLine(" where keikaku_nendo = @year ")
            '    .AppendLine(" and isnull(keikaku_huhen_flg,0) <> @keikaku_huhen_flg ")

            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_03)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@year", SqlDbType.VarChar, 4, strYear))      '�N�x
            paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, 1))   '�v��l�s��FLG

            objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", strYear & "�v��Ǘ�_�����X�}�X�^�̃f�[�^�̃N���A�������ُ�I�����܂����B")
            End If
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' JHS�̃f�[�^��o�^����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="drData">�o�^�f�[�^</param>
    ''' <param name="strYear">�N�x</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function InsJHSData(ByVal objConnection As SqlExecutor, _
                                ByVal drData As DataRow, _
                                ByVal strYear As String) As Integer

        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�����X�̃Z�b�g
        Dim strKemeiten As String = drData("�����X��").ToString
        If Encoding.Default.GetByteCount(strKemeiten) > 40 Then
            strKemeiten = Encoding.Default.GetString(Encoding.Default.GetBytes(strKemeiten), 0, 40)
            If strKemeiten.EndsWith("�E") Then
                strKemeiten = Encoding.Default.GetString(Encoding.Default.GetBytes(strKemeiten), 0, 39)
            End If

        End If

        Try

            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" INSERT INTO m_keikaku_kameiten WITH(UPDLOCK)( ")          '�v��Ǘ�_�����XϽ�           
            '    .AppendLine("    keikaku_nendo ")                          '�v��_�N�x
            '    .AppendLine("    ,kameiten_cd ")                           '�����X����
            '    .AppendLine("    ,torikesi ")                              '���
            '    .AppendLine("    ,hattyuu_teisi_flg ")                     '������~FLG
            '    .AppendLine("    ,eigyou_kbn ")                            '�c�Ƌ敪
            '    .AppendLine("    ,kameiten_mei ")                          '�����X��
            '    .AppendLine("    ,shiten_mei ")                            '�x�X��
            '    .AppendLine("    ,busyo_cd ")                              '��������
            '    .AppendLine("    ,eigyou_tantousya_id ")                   '�c�ƒS����
            '    .AppendLine("    ,eigyou_tantousya_mei ")                  '�c�ƒS���Җ�
            '    .AppendLine("    ,hituyou_mendan_kaisuu ")                 '�K�v�ʒk��
            '    .AppendLine("    ,eigyousyo_busyo_cd ")                    '�c�Ə�_��������
            '    .AppendLine("    ,eigyousyo_mei ")                         '�c�Ə���
            '    .AppendLine("    ,todouhuken_cd ")                         '�s���{������
            '    .AppendLine("    ,todouhuken_mei ")                        '�s���{����
            '    .AppendLine("    ,keiretu_cd ")                            '�n����
            '    .AppendLine("    ,keiretu_mei ")                           '�n��
            '    .AppendLine("    ,eigyousyo_cd ")                          '�c�Ə�����
            '    .AppendLine("    ,keikaku_nenkan_tousuu ")                 '�N�ԓ���
            '    .AppendLine("    ,zenjitu_nenkan_tousuu ")                 '�O�N����_�N�ԓ���
            '    .AppendLine("    ,keikaku0_flg ")                          '�v��l0FLG
            '    .AppendLine("    ,kameiten_add_datetime ")                 '�����X�o�^�N��
            '    .AppendLine("    ,kameiten_sinki_flg ")                    '�����X�V�KFLG
            '    .AppendLine("    ,gyoutai ")                               '�Ƒ�
            '    .AppendLine("    ,bunjyou_50flg ")                         '�������50��FLG
            '    .AppendLine("    ,tyuumon_50flg ")                         '�������50��FLG
            '    .AppendLine("    ,heibai_50flg ")                          '�������50��FLG
            '    .AppendLine("    ,keikaku_huhen_flg ")                     '�v��l�s��FLG
            '    .AppendLine("    ,add_login_user_id ")                     '�o�^���O�C�����[�U�[ID
            '    .AppendLine("    ,add_datetime ")                          '�o�^����
            '    .AppendLine("    ,upd_login_user_id ")                     '�X�V���O�C�����[�U�[ID
            '    .AppendLine("    ,upd_datetime ")                          '�X�V����
            '    .AppendLine(" ) ")
            '    .AppendLine(" VALUES ( ")
            '    .AppendLine("	@keikaku_nendo ")
            '    .AppendLine("	,@kameiten_cd ")
            '    .AppendLine("	,@torikesi ")
            '    .AppendLine("	,@hattyuu_teisi_flg ")
            '    .AppendLine("	,@eigyou_kbn ")
            '    .AppendLine("	,@kameiten_mei ")
            '    .AppendLine("	,@shiten_mei ")
            '    .AppendLine("	,@busyo_cd ")
            '    .AppendLine("	,@eigyou_tantousya_id ")
            '    .AppendLine("	,@eigyou_tantousya_mei ")
            '    .AppendLine("	,@hituyou_mendan_kaisuu ")
            '    .AppendLine("	,@eigyousyo_busyo_cd ")
            '    .AppendLine("	,@eigyousyo_mei ")
            '    .AppendLine("	,@todouhuken_cd ")
            '    .AppendLine("	,@todouhuken_mei ")
            '    .AppendLine("	,@keiretu_cd ")
            '    .AppendLine("	,@keiretu_mei ")
            '    .AppendLine("	,@eigyousyo_cd ")
            '    .AppendLine("	,@keikaku_nenkan_tousuu ")
            '    .AppendLine("	,@zenjitu_nenkan_tousuu ")
            '    .AppendLine("	,@keikaku0_flg ")
            '    .AppendLine("	,@kameiten_add_datetime ")
            '    .AppendLine("	,@kameiten_sinki_flg ")
            '    .AppendLine("	,@gyoutai ")
            '    .AppendLine("	,@bunjyou_50flg ")
            '    .AppendLine("	,@tyuumon_50flg ")
            '    .AppendLine("	,@heibai_50flg ")
            '    .AppendLine("	,@keikaku_huhen_flg ")
            '    .AppendLine("	,@add_login_user_id ")
            '    .AppendLine("	,@add_datetime ")
            '    .AppendLine("	,@upd_login_user_id ")
            '    .AppendLine("	,@upd_datetime ")
            '    .AppendLine(" ) ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_07)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            With paramList
                paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strYear))                              '�v��_�N�x
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 16, drData("�����X����")))                '�����X����
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, drData("���")))
                paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, drData("������~FLG")))        '������~FLG
                paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 1, drData("�c�Ƌ敪")))                   '�c�Ƌ敪
                paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, strKemeiten))                       '�����X��
                paramList.Add(MakeParam("@shiten_mei", SqlDbType.VarChar, 160, drData("�x�X��")))                    '�x�X��
                paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 16, IIf(drData("��������").ToString.Length > 4, "0000", drData("��������"))))                     '��������
                paramList.Add(MakeParam("@eigyou_tantousya_id", SqlDbType.VarChar, 30, drData("�c�ƒS����")))       '�c�ƒS����
                paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 100, drData("�c�ƒS���Җ�")))    '�c�ƒS���Җ�
                paramList.Add(MakeParam("@hituyou_mendan_kaisuu", SqlDbType.Int, 3, drData("�K�v�ʒk��")))
                paramList.Add(MakeParam("@eigyousyo_busyo_cd", SqlDbType.VarChar, 16, drData("�c�Ə�_��������")))    '�c�Ə�_��������
                paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 160, drData("�c�Ə���")))               '�c�Ə���
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, drData("�s���{������")))            '�s���{������
                paramList.Add(MakeParam("@todouhuken_mei", SqlDbType.VarChar, 10, drData("�s���{����")))            '�s���{����
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, drData("�n����")))                   '�n����
                paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, drData("�n��")))                   '�n��
                paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, drData("�c�Ə�����")))               '�c�Ə�����
                'TODO
                'paramList.Add(MakeParam("@keikaku_nenkan_tousuu", SqlDbType.BigInt, 12, IIf(drData("�N�ԓ���").ToString = String.Empty, DBNull.Value, drData("�N�ԓ���"))))         '�N�ԓ���
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu", SqlDbType.VarChar, 5, drData("�N�ԓ���")))         '�N�ԓ���
                paramList.Add(MakeParam("@zenjitu_nenkan_tousuu", SqlDbType.BigInt, 12, drData("�O�N����_�N�ԓ���")))         '�O�N����_�N�ԓ���
                paramList.Add(MakeParam("@keikaku0_flg", SqlDbType.Int, 1, drData("�v��l0FLG")))                  '�v��l0FLG
                paramList.Add(MakeParam("@kameiten_add_datetime", SqlDbType.VarChar, 7, drData("�����X�o�^�N��")))         '�����X�o�^�N��
                paramList.Add(MakeParam("@kameiten_sinki_flg", SqlDbType.Int, 1, drData("�����X�V�KFLG")))         '�����X�V�KFLG
                paramList.Add(MakeParam("@gyoutai", SqlDbType.VarChar, 20, drData("�Ƒ�")))                   '�Ƒ�
                paramList.Add(MakeParam("@bunjyou_50flg", SqlDbType.Int, 2, drData("�������50��FLG")))          '�������50��FLG
                paramList.Add(MakeParam("@tyuumon_50flg", SqlDbType.Int, 2, drData("�������50��FLG")))          '�������50��FLG
                paramList.Add(MakeParam("@heibai_50flg", SqlDbType.Int, 2, drData("�������50��FLG")))          '�������50��FLG
                paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, "0"))          '�v��l�s��FLG
                paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, drData("�o�^ID")))          '�o�^���O�C�����[�U�[ID
                paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 25, IIf(drData("�o�^����").ToString = String.Empty, DBNull.Value, drData("�o�^����"))))            '�o�^����
                paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, drData("�X�VID")))          '�X�V���O�C�����[�U�[ID
                paramList.Add(MakeParam("@upd_datetime", SqlDbType.DateTime, 25, IIf(drData("�X�V����").ToString = String.Empty, DBNull.Value, drData("�X�V����"))))            '�X�V����
            End With

            Return objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())


        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�v��Ǘ�_�����X�}�X�^�̃f�[�^�̑}���������ُ�I�����܂����B")
            End If
            Throw ex

        End Try

    End Function

    ''' <summary>
    ''' �v��Ǘ�_�����X�̃f�[�^���擾����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strYear">�Ώ۔N�x</param>
    ''' <returns>�v��Ǘ�_�����X�f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function SelKeikakuKameiten(ByVal objConnection As SqlExecutor, _
                                        ByVal strYear As String) As Data.DataTable
        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" SELECT  ")
            '    .AppendLine(" 	k.kameiten_cd ")
            '    .AppendLine(" FROM m_keikaku_kameiten k with(readuncommitted) ")
            '    .AppendLine(" WHERE  k.keikaku_nendo = @year ")
            '    .AppendLine(" AND k.keikaku_huhen_flg = @keikaku_huhen_flg ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_04)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@year", SqlDbType.VarChar, 4, strYear))            '�N�x
            paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, 1))   '�v��l�s��FLG

            '�d���������̃f�[�^
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�v��Ǘ�_�����X�̃f�[�^�̎擾�������ُ�I�����܂����B")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' �v��Ǘ�_�����X�f�[�^�̑��݃`�F�b�N
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strYear">�Ώ۔N�x</param>
    ''' <param name="strKameitenCd">�����X����</param>
    ''' <returns>�v��Ǘ�_�����X�f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function SelKeikakuKameitenCount(ByVal objConnection As SqlExecutor, _
                                        ByVal strYear As String, _
                                        ByVal strKameitenCd As String) As Data.DataTable
        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" SELECT  ")
            '    .AppendLine(" 	count(k.kameiten_cd) ")
            '    .AppendLine(" FROM m_keikaku_kameiten k with(readuncommitted) ")
            '    .AppendLine(" WHERE  k.keikaku_nendo = @year ")
            '    .AppendLine(" AND k.kameiten_cd = @kameiten_cd ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_08)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@year", SqlDbType.VarChar, 4, strYear))            '�N�x
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))   '�����X����

            '�d���������̃f�[�^
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�v��Ǘ�_�����X�̃f�[�^�̎擾�������ُ�I�����܂����B")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' �v��Ǘ��e�[�u������f�[�^�������擾����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strYear">�Ώ۔N�x</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�v��Ǘ��e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function SelKeikakuKanri(ByVal objConnection As SqlExecutor, _
                                        ByVal strYear As String, _
                                        ByVal strKameitenCd As String) As Data.DataTable
        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" SELECT  ")
            '    .AppendLine(" 	isnull([4gatu_keikaku_kensuu],0) + isnull([5gatu_keikaku_kensuu],0) +isnull([6gatu_keikaku_kensuu],0) ")
            '    .AppendLine(" +	isnull([7gatu_keikaku_kensuu],0) + isnull([8gatu_keikaku_kensuu],0) +isnull([9gatu_keikaku_kensuu],0) ")
            '    .AppendLine(" +	isnull([10gatu_keikaku_kensuu],0) + isnull([11gatu_keikaku_kensuu],0) +isnull([12gatu_keikaku_kensuu],0) ")
            '    .AppendLine(" +	isnull([1gatu_keikaku_kensuu],0) + isnull([2gatu_keikaku_kensuu],0) +isnull([3gatu_keikaku_kensuu],0) ")
            '    .AppendLine(" as keikaku_kensuu ")
            '    .AppendLine(" FROM t_keikaku_kanri with(readuncommitted) ")
            '    .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            '    .AppendLine(" AND kameiten_cd = @kameiten_cd ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_06)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strYear))            '�v��_�N�x
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))     '�����X����

            '�d���������̃f�[�^
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�v��Ǘ��̃f�[�^�̎擾�������ُ�I�����܂����B")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' �v��p_���̃}�X�^����c�Ƌ敪���擾����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <returns>�v��p_���̃}�X�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function SelKeikakuyouMeisyou(ByVal objConnection As SqlExecutor) As Data.DataTable

        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" SELECT  ")
            '    .AppendLine(" 	code ")
            '    .AppendLine("   ,meisyou ")
            '    .AppendLine(" FROM m_keikakuyou_meisyou with(readuncommitted) ")
            '    .AppendLine(" WHERE meisyou_syubetu = @meisyou_syubetu ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_05)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "05"))            '�c�Ƌ敪

            '�d���������̃f�[�^
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�v��p_���̃}�X�^�̃f�[�^�̎擾�������ُ�I�����܂����B")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' �f�[�^�ҏW����
    ''' </summary>
    ''' <param name="strDateTime">����</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function SetDateTime(ByVal strDateTime As String) As String

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name, strDateTime)

        If strDateTime <> String.Empty Then
            strDateTime = Left(strDateTime, 4) _
                            & "/" & Mid(strDateTime, 5, 2) _
                            & "/" & Mid(strDateTime, 7, 2) _
                            & " " & Mid(strDateTime, 9, 2) _
                            & ":" & Mid(strDateTime, 11, 2) _
                            & ":" & Mid(strDateTime, 13, 2)

        End If

        Return strDateTime

    End Function

#End Region

End Class

