Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Microsoft.VisualBasic
Imports System.Web
Imports system.Web.HttpContext
Imports System.Configuration
Imports Lixil.JHS_EKKS.DataAccess
Public Class CommonBC
    Private CommonDA As New CommonDA
    '''<summary>LOG�̐V�K����</summary>
    Public Function SetUserInfo(ByVal strUrl As String, ByVal strUserId As String, ByVal strSousa As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strUrl, strUserId, strSousa)

        Return CommonDA.InsUrlLog(strUrl, strUserId, strSousa)
    End Function
    ''' <summary>�V�X�e�����擾</summary>
    Public Function SelSystemDate() As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return CommonDA.SelSystemDate()
    End Function

    ''' <summary>�v��p_���̃}�X�^����v��N�x���X�g���擾����</summary>
    Public Function GetKeikakuNendoData() As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)
        Return CommonDA.SelKeikakuNendoData()
    End Function
End Class
