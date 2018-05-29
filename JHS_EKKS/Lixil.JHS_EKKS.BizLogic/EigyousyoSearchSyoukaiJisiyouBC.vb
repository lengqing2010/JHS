Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �c�Ə�����_�v��Ǘ�_�����X�����Ɖ�w���pPOPUP
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchSyoukaiJisiyouBC

    Private eigyousyoSearchSyoukaiJisiyouDA As New DataAccess.EigyousyoSearchSyoukaiJisiyouDA

    ''' <summary>
    ''' �u�c�Ə��}�X�^�v����A�c�Ə��R�[�h���擾����
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoMei">�c�Ə���</param>
    ''' <param name="blnTorikesi">���ʂ̑މ�������X���`�F�b�N</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/07/18�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetEigyousyo(ByVal strRows As String, _
                                   ByVal strEigyousyoCd As String, _
                                   ByVal strEigyousyoMei As String, _
                                   ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strEigyousyoCd, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        Return eigyousyoSearchSyoukaiJisiyouDA.SelEigyousyo(strRows, strEigyousyoCd, strEigyousyoMei, blnTorikesi)
    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoMei">�c�Ə���</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/07/18�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetEigyousyoCount(ByVal strEigyousyoCd As String, _
                                        ByVal strEigyousyoMei As String, _
                                        ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strEigyousyoCd, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        Return eigyousyoSearchSyoukaiJisiyouDA.SelEigyousyoCount(strEigyousyoCd, strEigyousyoMei, blnTorikesi)
    End Function

End Class
