Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �n��_�A�C�t���z�[��
''' </summary>
''' <remarks>�A�C�t���z�[���Ɋ֌W���鏈���͂��̃N���X�Ɏ������܂�<BR/>
''' �R�n��ɋ��ʂ��鏈���͌p�����̐e�N���X[KeiretuDataAccess]�Ɏ������܂�</remarks>
Public Class KeiretuAifuruDataAccess
    Inherits KeiretuDataAccess
    Implements IKeiretuDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �������z���擾���܂�
    ''' </summary>
    ''' <param name="intMode">�擾���[�h<BR/>
    ''' 1 (���i�R�[�h1�̉����X�����i)   <BR/>
    ''' 2 (��񕥖߂̉����X�����i)      <BR/>
    ''' 3 (�{��(TH)�����i)              <BR/>
    ''' 4 (���������z / �|��)           <BR/>
    ''' 5 (�H���X�������z * �|��)       <BR/>
    ''' 6 (�����X�����i�����������z/�|��)</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="intKingaku">TH�����p���i�}�X�^��KEY,�|���v�Z���̋��z<BR/>
    ''' �擾���[�h=1 (TH�����p���i�}�X�^��KEY)<BR/>
    ''' �擾���[�h=4 (���������z)<BR/>
    ''' �擾���[�h=5 (�H���X�������z)</param>
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

        ' ��{�ƂȂ�SQL��ݒ肵�܂�
        If SetBaseSQLData(intMode, "honbumuke_kkk") = False Then
            Return -1
        End If

        commandTextSb.Append(" FROM m_honbu_seikyuu WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE jhs_syouhin_cd = " & strParamSyouhinCd)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.Char, 8, strSyouhinCd)}

        ' �������s
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            seikyuuKakakuDataSet, dataTableName, commandParameters)

        If GetKingaku(intMode, intKingaku, intReturnKingaku) = False Then
            ' "�}�X�^�[�ɂ���܂���B�o���ɒǉ��̘A�������ĉ������B" 
            Return 0
        End If

        Return 1

    End Function

End Class
