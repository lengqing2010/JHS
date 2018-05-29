Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

Public Class KeikakuKanriErrorDetailsBC

    Private objKeikakuKanriErrorDetailsDA As New DataAccess.KeikakuKanriErrorDetailsDA

    ''' <summary>
    '''�v��Ǘ��\_�捞�G���[���擾
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <returns>�v��Ǘ��\_�捞�G���[���e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Function GetKeikakuTorikomiError(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDatetime As String) As DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                          , strEdiJouhouSakuseiDate _
                                                                                          , strSyoriDatetime)

        Return objKeikakuKanriErrorDetailsDA.SelKeikakuTorikomiError(strEdiJouhouSakuseiDate, strSyoriDatetime)

    End Function

    ''' <summary>
    '''�v��Ǘ��\_�捞�G���[��񌏐��擾
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <returns>�v��Ǘ��\_�捞�G���[���e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Function GetKeikakuTorikomiErrorCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDatetime As String) As String

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                          , strEdiJouhouSakuseiDate _
                                                                                          , strSyoriDatetime)

        Return objKeikakuKanriErrorDetailsDA.SelKeikakuTorikomiErrorCount(strEdiJouhouSakuseiDate, strSyoriDatetime)

    End Function
End Class
