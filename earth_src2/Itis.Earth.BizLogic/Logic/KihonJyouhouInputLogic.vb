Imports Itis.Earth.DataAccess
Imports System.Transactions

''' <summary>�����X����V�K�o�^����</summary>
''' <remarks>�����X���V�K�o�^��񋟂���</remarks>
''' <history>
''' <para>2009/07/15�@�t��(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KihonJyouhouInputLogic

    ''' <summary>�����X���V�K�o�^�N���X�̃C���X�^���X���� </summary>
    Private kihonJyouhouInputDA As New KihonJyouhouInputDataAccess

    ''' <summary>�����X�R�[�h�̍ő�l���擾����</summary>
    ''' <param name="strKbn">�p�����[�^</param>
    ''' <returns>�����X�R�[�h�ő�l�f�[�^�e�[�u��</returns>
    Public Function GetMaxKameitenCd(ByVal strKbn As String, _
                                    Optional ByVal strCdFrom As String = "", _
                                    Optional ByVal strCdTo As String = "") As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        Return kihonJyouhouInputDA.SelMaxKameitenCd(strKbn, strCdFrom, strCdTo)
    End Function

    ''' <summary>�����X�R�[�h�̍ő�l�ƍ̔Ԑݒ�͈̔͂��擾����</summary>
    ''' <param name="strKbn">�敪</param>
    ''' <returns>�����X�R�[�h�̍ő�l�ƍ̔Ԑݒ�͈̔͂��擾����</returns>
    ''' <history>
    ''' <para>2012/11/19�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Function GetMaxKameitenCd1(ByVal strKbn As String) As Data.DataTable

        Return kihonJyouhouInputDA.SelMaxKameitenCd1(strKbn)
    End Function

    ''' <summary>�����X�R�[�h���擾����</summary>
    ''' <param name="strKbn">�p�����[�^</param>
    ''' <param name="strCd">�p�����[�^</param>
    ''' <returns>�����X�R�[�h�f�[�^�e�[�u��</returns>
    Public Function GetKameitenCd(ByVal strKbn As String, ByVal strCd As String) As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        Return kihonJyouhouInputDA.SelKameitenCd(strKbn, strCd)
    End Function

    ''' <summary>�����X�}�X�^�e�[�u���ɓo�^����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>����</returns>
    Public Function SetKameitenInfo(ByVal dtParamKameitenInfo As KihonJyouhouInputDataSet.Param_KameitenInfoDataTable) As Boolean
        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            If kihonJyouhouInputDA.InsKameitenInfo(dtParamKameitenInfo) = True AndAlso _
                kihonJyouhouInputDA.InsKameitenRenkeiInfo(dtParamKameitenInfo) = True Then
                scope.Complete()
                Return True
            Else
                scope.Dispose()
                Return False
            End If
        End Using
    End Function

    ''' <summary>
    ''' �u����vddl�̃f�[�^���擾����
    ''' </summary>
    ''' <returns>�u����vddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Public Function GetTorikesiList(Optional ByVal strCd As String = "") As Data.DataTable

        '�߂�l
        Return kihonJyouhouInputDA.SelTorikesiList(strCd)

    End Function

End Class
