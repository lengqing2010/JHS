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
''' <remarks>JHS Earth ���� �� �����ް��@�Ɓ@�d���ް��@�� �����XϽ��@����
'''          �����X���Ƃ� ����䗦�A�H�����藦�A�H���󒍗��A���H����
'''          ���@�N�x�䗦�Ǘ��e�[�u���@�Ɋi�[����@</remarks>
''' <history>
''' <para>2013/1/15 ��A/�k�o �V�K�쐬 P-45026</para>
''' </history>
Public Class S0005

#Region "�萔"
    '�o�b�`ID
    Private Const CON_BATCH_ID As String = "bat_set5"
#End Region

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
        Dim btProcess As S0005

        '������
        btProcess = New S0005()

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
            "�v��Ǘ�_�N�x�䗦�e�[�u����" & _
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

        Dim dtYear As Data.DataTable                  '�v��N�x
        Dim dtEarthData As DataTable            '�d���������f�[�^
        Dim options As New Transactions.TransactionOptions
        Dim i As Integer
        Dim j As Integer

        Dim strYear As String
        Dim strBeginMonth As String
        Dim strEndMonth As String

        mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                            "�v��N�x�h�m�h�t�@�C���ǎ�鏈�����J�n���܂����B")

        '�v��N�x�h�m�h�t�@�C����Ǎ��݁A�v��N�x���擾����
        dtYear = Definition.GetKeikakuNendo("S0005")

        '����ɓǎ�邱�Ƃ��ł��Ȃ������ꍇ�A�I������
        If dtYear Is Nothing OrElse dtYear.Rows.Count = 0 Then
            Exit Sub
        End If

        mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                            "�d���������̔N�x�䗦�Ǘ��f�[�^�̎擾�������J�n���܂����B")

        'DB�ڑ��̃I�[�v��
        mConnectionEarth.Open()
        mConnectionJHS.Open(True)

        Try
            For i = 0 To dtYear.Rows.Count - 1

                strYear = Convert.ToString(dtYear.Rows(i)("Year"))
                strBeginMonth = Convert.ToString(dtYear.Rows(i)("BeginMonth"))
                strEndMonth = Convert.ToString(dtYear.Rows(i)("EndMonth"))

                '�d���������̌v��Ǘ�_�N�x�䗦�f�[�^�f�[�^���擾����
                dtEarthData = SelEarthData(mConnectionEarth, strBeginMonth, strEndMonth)

                If dtEarthData IsNot Nothing Then
                    mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                                        strYear & "�N�x�ɂd���������̃f�[�^����" & _
                                        Convert.ToString(dtEarthData.Rows.Count) & _
                                        "���f�[�^���擾����܂����B")
                End If

                If dtEarthData.Rows.Count > 0 Then

                    '�Y���N�x�̃f�[�^���폜����
                    DelJHSData(mConnectionJHS, strYear)

                    '�d���������̌v��Ǘ�_�N�x�䗦�f�[�^�ɂ��A���[�v����
                    For j = 0 To dtEarthData.Rows.Count - 1

                        mInsCount = mInsCount + InsJHSData(mConnectionJHS, dtEarthData.Rows(j), strYear)

                    Next

                End If

                '�f�[�^���������
                If dtEarthData IsNot Nothing Then
                    dtEarthData.Dispose()
                    dtEarthData = Nothing
                End If
            Next

            '�����̏ꍇ
            mConnectionJHS.Commit()
        Catch ex As Exception
            '���s�̏ꍇ
            mConnectionJHS.Rollback()
            Throw ex
        End Try
    End Sub
#End Region

#Region "SQL��"
    ''' <summary>
    ''' �d���������̃f�[�^���擾����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strBeginMonth">�ΏۊJ�n�N�x</param>
    ''' <param name="strEndMonth">�ΏۏI���N�x</param>
    ''' <returns>�N�x�䗦�Ǘ��f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/�k�o �V�K�쐬 P-45026</para>
    ''' </history>
    Private Function SelEarthData(ByVal objConnection As SqlExecutor, _
                                  ByVal strBeginMonth As String, _
                                  ByVal strEndMonth As String) As Data.DataTable
        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim strKikan As String
        strKikan = "between '" & strBeginMonth & "' and '" & strEndMonth & "'"

        '2013/10/23 ���F�ǉ��@��
        '�敪
        Dim strKubun As String
        strKubun = Definition.GetKubunName5

        '���i���
        Dim strSyouhinSyubetu As String
        strSyouhinSyubetu = Definition.GetSyouhinSyubetuName5
        '2013/10/23 ���F�ǉ��@��
        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine("select  ")
            '    .AppendLine("  k.kameiten_cd �����X����   ")
            '    .AppendLine("  ,k.kameiten_mei1 �����X��   ")
            '    .AppendLine("  ,case when isnull(usum_eigyo.urigaku,0)=0 then ")
            '    .AppendLine("	   0  ")
            '    .AppendLine("   else  ")
            '    .AppendLine("	   case when convert(float,isnull(usum.urigaku,0))/isnull(usum_eigyo.urigaku,0) >= 1 then ")
            '    .AppendLine("	       100 ")
            '    .AppendLine("	   else  ")
            '    .AppendLine("	       round(convert(float,isnull(usum.urigaku,0))/isnull(usum_eigyo.urigaku,0)*100,1)  ")
            '    .AppendLine("	   end ")
            '    .AppendLine("   end ����䗦�c�ƃ}��   ")
            '    .AppendLine("  ,case when isnull(kaiseki_cnt.cnt,0)=0 then ")
            '    .AppendLine("       0  ")
            '    .AppendLine("   else  ")
            '    .AppendLine("	   case when convert(float,isnull(koj_cnt.cnt,0))/isnull(kaiseki_cnt.cnt,0) >= 1 then ")
            '    .AppendLine("	       100 ")
            '    .AppendLine("	   else  ")
            '    .AppendLine("	       round(convert(float,isnull(koj_cnt.cnt,0))/isnull(kaiseki_cnt.cnt,0)*100,1)  ")
            '    .AppendLine("	   end ")
            '    .AppendLine("   end �H�����藦   ")
            '    .AppendLine("  ,case when isnull(koj_cnt.cnt,0)=0 then  ")
            '    .AppendLine("	   0  ")
            '    .AppendLine("   else  ")
            '    .AppendLine("	   case when convert(float,isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))/isnull(koj_cnt.cnt,0) >= 1 then ")
            '    .AppendLine("	       100 ")
            '    .AppendLine("	   else  ")
            '    .AppendLine("	       round(convert(float,isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))/isnull(koj_cnt.cnt,0)*100,1)  ")
            '    .AppendLine("	   end ")
            '    .AppendLine("   end �H���󒍗�   ")
            '    .AppendLine("  ,case when isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0)=0 then  ")
            '    .AppendLine("	   0  ")
            '    .AppendLine("   else  ")
            '    .AppendLine("	   case when convert(float,isnull(tyoku_koj_cnt.cnt,0))/(isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0)) >= 1 then ")
            '    .AppendLine("	       100 ")
            '    .AppendLine("	   else  ")
            '    .AppendLine("	       round(convert(float,isnull(tyoku_koj_cnt.cnt,0))/(isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))*100,1)  ")
            '    .AppendLine("	   end ")
            '    .AppendLine("   end ���H����   ")
            '    .AppendLine("from m_kameiten k with(readuncommitted)   ")
            '    '/*2011�N�x�E�Ώۏ��i�̔�����z�������X���ƂɏW�v*/
            '    .AppendLine("/*2011�N�x�E�Ώۏ��i�̔�����z�������X���ƂɏW�v*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select kameiten_cd,sum(uri_gaku) urigaku from t_uriage_data u with(readuncommitted)    ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on u.syouhin_cd=s.syouhin_cd   ")
            '    .AppendLine("	where denpyou_uri_date " & strKikan & "  ")
            '    .AppendLine("	and isnull(s.syouhin_syubetu1,'')<>''    ")
            '    .AppendLine("	group by kameiten_cd   ")
            '    .AppendLine(") usum on k.kameiten_cd=usum.kameiten_cd   ")
            '    '/*2011�N�x�E�Ώۏ��i�̔�����z���c�ƒS�����ƂɏW�v
            '    '�i�c�ƒS�������������X�ɂ͓����l���Z�b�g�����E�u����䗦�c�ƃ}���v�̌v�Z�ɗ��p�j*/
            '    .AppendLine("/*2011�N�x�E�Ώۏ��i�̔�����z���c�ƒS�����ƂɏW�v ")
            '    .AppendLine("�i�c�ƒS�������������X�ɂ͓����l���Z�b�g�����E�u����䗦�c�ƃ}���v�̌v�Z�ɗ��p�j*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select eigyou_tantousya_mei,sum(uri_gaku) urigaku from t_uriage_data u with(readuncommitted)    ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on u.syouhin_cd=s.syouhin_cd   ")
            '    .AppendLine("	inner join m_kameiten k with(readuncommitted) on u.kameiten_cd=k.kameiten_cd   ")
            '    .AppendLine("	where denpyou_uri_date " & strKikan & "   ")
            '    .AppendLine("	and isnull(s.syouhin_syubetu1,'')<>''    ")
            '    .AppendLine("	group by eigyou_tantousya_mei   ")
            '    .AppendLine(") usum_eigyo on k.eigyou_tantousya_mei=usum_eigyo.eigyou_tantousya_mei   ")
            '    '/*2011�N�x�E�H�����藦�v�Z���p���i�̔��㌏���i���蔻�蕨���������j�������X���ƂɏW�v
            '    '�i�u�H�����藦�v�̌v�Z�ɗ��p�j*/
            '    .AppendLine("/*2011�N�x�E�H�����藦�v�Z���p���i�̔��㌏���i���蔻�蕨���������j�������X���ƂɏW�v ")
            '    .AppendLine("�i�u�H�����藦�v�̌v�Z�ɗ��p�j*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    ")
            '    .AppendLine("	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine("	where isnull(s.syouhin_syubetu1,'') in (" & strSyouhinSyubetu & ")    ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine("	and t.uri_date " & strKikan & "     ")
            '    .AppendLine("	and hantei_cd1 not in(97,113,1635)   ")
            '    .AppendLine("	group by j.kameiten_cd   ")
            '    .AppendLine(") kaiseki_cnt on k.kameiten_cd=kaiseki_cnt.kameiten_cd   ")
            '    '/*2011�N�x�E�H�����藦�v�Z���p���i�E�H�����茋��FLG=1�̔��㌏���������X���ƂɏW�v
            '    '�i�u�H�����藦�v�A�u�H���󒍗��v�̌v�Z�ɗ��p�j*/
            '    .AppendLine("/*2011�N�x�E�H�����藦�v�Z���p���i�E�H�����茋��FLG=1�̔��㌏���������X���ƂɏW�v ")
            '    .AppendLine("�i�u�H�����藦�v�A�u�H���󒍗��v�̌v�Z�ɗ��p�j*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    ")
            '    .AppendLine("	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine("	where isnull(s.syouhin_syubetu1,'') in (" & strSyouhinSyubetu & ")    ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine("	and t.uri_date " & strKikan & "     ")
            '    .AppendLine("	and j.koj_hantei_kekka_flg=1   ")
            '    .AppendLine("	group by j.kameiten_cd   ")
            '    .AppendLine(") koj_cnt on k.kameiten_cd=koj_cnt.kameiten_cd   ")
            '    '/*2011�N�x�E�H���Ώۏ��i�i���޺���130�j�̔��㌏���������X���ƂɏW�v
            '    '�i�u�H���󒍗��v�A�u���H�����v�̌v�Z�ɗ��p�j*/
            '    .AppendLine("/*2011�N�x�E�H���Ώۏ��i�i���޺���130�j�̔��㌏���������X���ƂɏW�v ")
            '    .AppendLine("�i�u�H���󒍗��v�A�u���H�����v�̌v�Z�ɗ��p�j*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    ")
            '    .AppendLine("	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    ")
            '    .AppendLine("	where t.bunrui_cd='130'  ")
            '    .AppendLine("	and isnull(s.syouhin_syubetu1,'') = 'Ke2001'    ")
            '    .AppendLine("	and t.uri_date " & strKikan & "    ")
            '    .AppendLine("	group by j.kameiten_cd   ")
            '    .AppendLine(") JHS_koj_cnt on k.kameiten_cd=JHS_koj_cnt.kameiten_cd   ")
            '    '/*2011�N�x�E�H���Ώۏ��i�i���޺���130�j�̔��㌏���������X���ƂɏW�v
            '    '�i�u�H���󒍗��v�A�u���H�����v�̌v�Z�ɗ��p�j*/
            '    .AppendLine("/*2011�N�x�E�H���Ώۏ��i�i���޺���130�j�̔��㌏���������X���ƂɏW�v ")
            '    .AppendLine("�i�u�H���󒍗��v�A�u���H�����v�̌v�Z�ɗ��p�j*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    ")
            '    .AppendLine("	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    ")
            '    .AppendLine("	where t.bunrui_cd='130'  ")
            '    .AppendLine("	and isnull(s.syouhin_syubetu1,'') = 'Ke2002'    ")
            '    .AppendLine("	and t.uri_date " & strKikan & " ")
            '    .AppendLine("	group by j.kameiten_cd   ")
            '    .AppendLine(") tyoku_koj_cnt on k.kameiten_cd=tyoku_koj_cnt.kameiten_cd   ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine("where k.kbn in (" & strKubun & ") ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine("/*�n��'TAMA','REOH','LEOP','ACEH','0001','6100','6800'��E�EG�ES�����Ȃ��̂ō��͂����OK ")
            '    .AppendLine("�i����͈ȊO�̌n����ǉ��ɂȂ�\���L�Ȃ̂Œ��Ӂj*/   ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0005_01)
            strSql = strSql.Replace("@strKikan", strKikan)
            strSql = strSql.Replace("@strKubun", strKubun)
            sqlBuffer.AppendLine(strSql)

            '�d���������̃f�[�^
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�d���������̔N�x�䗦�Ǘ��f�[�^�̎擾�������ُ�I�����܂����B")
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
            '    .AppendLine("     t_nendo_hiritu_kanri ")
            '    .AppendLine(" where keikaku_nendo = @year ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0005_02)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@year", SqlDbType.VarChar, 4, strYear))      '�N�x

            objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", strYear & "�N�x�䗦�Ǘ��̃f�[�^�̃N���A�������ُ�I�����܂����B")
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

        Try

            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" INSERT INTO t_nendo_hiritu_kanri WITH(UPDLOCK) ( ")     '�N�x�䗦�Ǘ��e�[�u��           
            '    .AppendLine("	keikaku_nendo ")                        '�v��_�N�x
            '    .AppendLine("	,kameiten_cd ")                         '�����X����
            '    .AppendLine("	,kameiten_mei ")                        '�����X��
            '    .AppendLine("	,uri_hiritu ")                          '����䗦
            '    .AppendLine("	,koj_hantei_ritu ")                     '�H�����藦
            '    .AppendLine("	,koj_jyuchuu_ritu ")                    '�H���󒍗�
            '    .AppendLine("	,tyoku_koj_ritu ")                      '���H����
            '    .AppendLine("	,add_login_user_id ")                   '�o�^���O�C�����[�U�[ID
            '    .AppendLine("	,add_datetime ")                        '�o�^����
            '    .AppendLine("	,upd_login_user_id ")                   '�X�V���O�C�����[�U�[ID
            '    .AppendLine("	,upd_datetime ")                        '�X�V����
            '    .AppendLine(" ) ")
            '    .AppendLine(" VALUES ( ")
            '    .AppendLine("	@keikaku_nendo ")                       '�v��_�N�x
            '    .AppendLine("	,@kameiten_cd ")                        '�����X����
            '    .AppendLine("	,@kameiten_mei ")                       '�����X��
            '    .AppendLine("	,@uri_hiritu ")                         '����䗦
            '    .AppendLine("	,@koj_hantei_ritu ")                    '�H�����藦
            '    .AppendLine("	,@koj_jyuchuu_ritu ")                   '�H���󒍗�
            '    .AppendLine("	,@tyoku_koj_ritu ")                     '���H����
            '    .AppendLine("	,@add_login_user_id ")                                               '�o�^���O�C�����[�U�[ID
            '    .AppendLine("	,GETDATE()  ")                                                       '�o�^����
            '    .AppendLine("	,null ")                                                             '�X�V���O�C�����[�U�[ID
            '    .AppendLine("	,null  ")                                                            '�X�V����
            '    .AppendLine(" ); ")

            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0005_03)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strYear))    '�v��_�N�x
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, drData.Item("�����X����").ToString)) '�����X����
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, drData.Item("�����X��").ToString))   '�����X��
            paramList.Add(MakeParam("@uri_hiritu", SqlDbType.VarChar, 5, drData.Item("����䗦�c�ƃ}��").ToString))      '����䗦
            paramList.Add(MakeParam("@koj_hantei_ritu", SqlDbType.VarChar, 5, drData.Item("�H�����藦").ToString))    '�H�����藦
            paramList.Add(MakeParam("@koj_jyuchuu_ritu", SqlDbType.VarChar, 5, drData.Item("�H���󒍗�").ToString)) '�H���󒍗�
            paramList.Add(MakeParam("@tyoku_koj_ritu", SqlDbType.VarChar, 5, drData.Item("���H����").ToString)) '���H����
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, CON_BATCH_ID)) '�o�^���O�C�����[�U�[ID

            Return objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())


        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", strYear & "�N�x�䗦�Ǘ��̃f�[�^�̑}���������ُ�I�����܂����B")
            End If

            Throw ex

        End Try

    End Function

#End Region

End Class

