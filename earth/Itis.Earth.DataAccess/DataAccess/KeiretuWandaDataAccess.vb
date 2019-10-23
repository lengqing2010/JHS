Imports System.text
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' �n��_�����_�[�z�[��
''' </summary>
''' <remarks>�����_�[�z�[���Ɋ֌W���鏈���͂��̃N���X�Ɏ������܂�<BR/>
''' �R�n��ɋ��ʂ��鏈���͌p�����̐e�N���X[KeiretuDataAccess]�Ɏ������܂�</remarks>
Public Class KeiretuWandaDataAccess
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

        ' ��{�ƂȂ�SQL��ݒ肵�܂�
        If SetBaseSQLData(intMode, "honbumuke_kkk") = False Then
            Return -1
        End If

        commandTextSb.Append(" FROM m_wh_seikyuuyou_kakaku WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE syouhin_cd = " & strParamSyouhinCd)

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
