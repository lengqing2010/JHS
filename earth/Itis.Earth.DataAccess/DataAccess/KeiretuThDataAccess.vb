Imports System.text
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' �n��_TH�F�̉�
''' </summary>
''' <remarks>TH�F�̉�Ɋ֌W���鏈���͂��̃N���X�Ɏ������܂�<BR/>
''' �R�n��ɋ��ʂ��鏈���͌p�����̐e�N���X[KeiretuDataAccess]�Ɏ������܂�</remarks>
Public Class KeiretuThDataAccess
    Inherits KeiretuDataAccess
    Implements IKeiretuDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �������z���擾���܂�
    ''' </summary>
    ''' <param name="intMode">�擾���[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="intKingaku">TH�����p���i�}�X�^��KEY,�|���v�Z���̋��z</param>
    ''' <param name="intReturnKingaku">�擾���z�i�߂�l�j</param>
    ''' <returns>True:�擾OK,False:�擾NG</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuKingaku(ByVal intMode As Integer, _
                                     ByVal strSyouhinCd As String, _
                                     ByVal intKingaku As Integer, _
                                     ByRef intReturnKingaku As Integer) As Integer Implements IKeiretuDataAccess.getSeikyuKingaku

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuKingaku", _
                                            intMode, _
                                            strSyouhinCd, _
                                            intKingaku, _
                                            intReturnKingaku)

        ' �p�����[�^
        Const strParamSyouhinCd As String = "@SYOUHINCD"
        Const strParamKingaku As String = "@KINGAKU"

        ' ��{�ƂȂ�SQL��ݒ肵�܂�
        If SetBaseSQLData(intMode, "th_muke_kkk") = False Then
            Return -1
        End If

        Dim strSqlcondition As String = ""

        If intMode = 1 Or intMode = 6 Then
            commandTextSb.Append(" FROM m_th_seikyuuyou_kakaku WITH (READCOMMITTED) ")
            strSqlcondition = " AND th_muke_kkk = " & strParamKingaku
        Else
            commandTextSb.Append(" FROM v_th_seikyuu ")
        End If

        commandTextSb.Append(" WHERE syouhin_cd = " & strParamSyouhinCd)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter

        ' �ǉ�����
        If strSqlcondition <> "" Then
            commandTextSb.Append(strSqlcondition)
            commandParameters = New SqlParameter() {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.Char, 8, strSyouhinCd) _
                                                  , SQLHelper.MakeParam(strParamKingaku, SqlDbType.Int, 0, intKingaku)}
        Else
            commandParameters = New SqlParameter() {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.Char, 8, strSyouhinCd)}
        End If

        ' �������s
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            seikyuuKakakuDataSet, dataTableName, commandParameters)

        Dim kameitenMukeTable As SeikyuKingakuDataSet.dt_KameitenMukeDataTable = _
                    seikyuuKakakuDataSet.dt_KameitenMuke

        If kameitenMukeTable.Count = 0 And intMode = 6 Then
            ' ���i���ނ݂̂Ńf�[�^���Ē��o�i�����X�����i��0�Ƃ���j
            Dim commandTextTH As New StringBuilder()
            commandTextTH.Append(" SELECT 0 AS kameiten_muke_kkk, ISNULL(kakeritu,0) AS kakeritu ")
            commandTextTH.Append(" FROM v_th_seikyuu WITH (READCOMMITTED) ")
            commandTextTH.Append(" WHERE syouhin_cd = " & strParamSyouhinCd)

            ' �������s
            SQLHelper.FillDataset(connStr, CommandType.Text, commandTextTH.ToString(), _
                seikyuuKakakuDataSet, dataTableName, commandParameters)
        End If

        If GetKingaku(intMode, intKingaku, intReturnKingaku) = False Then
            ' "�}�X�^�[�ɂ���܂���B�o���ɒǉ��̘A�������ĉ������B"
            Return 0
        End If

        Return 1


    End Function


End Class
