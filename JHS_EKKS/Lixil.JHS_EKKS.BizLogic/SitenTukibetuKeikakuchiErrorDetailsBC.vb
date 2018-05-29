Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �x�X ���ʌv��l EXCEL�捞�G���[
''' </summary>
''' <remarks></remarks>
Public Class SitenTukibetuKeikakuchiErrorDetailsBC

    Private sitenTukibetuKeikakuchiErrorDetailsDA As New DataAccess.SitenTukibetuKeikakuchiErrorDetailsDA

    ''' <summary>
    ''' �G���[���e���擾����
    ''' </summary>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetErrorJyouhou(ByVal strSyoriDatetime As String, _
                                         ByVal strEdiJouhouSakuseiDate As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strSyoriDatetime, _
                                                                                          strEdiJouhouSakuseiDate)

        Return sitenTukibetuKeikakuchiErrorDetailsDA.SelErrorJyouhou(strSyoriDatetime, strEdiJouhouSakuseiDate)

    End Function


    ''' <summary>
    ''' �G���[���e�̃f�[�^�������擾����
    ''' </summary>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetErrorJyouhouCount(ByVal strSyoriDatetime As String, _
                                         ByVal strEdiJouhouSakuseiDate As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strSyoriDatetime, _
                                                                                          strEdiJouhouSakuseiDate)

        Return sitenTukibetuKeikakuchiErrorDetailsDA.SelErrorJyouhouCount(strSyoriDatetime, strEdiJouhouSakuseiDate)

    End Function

End Class
