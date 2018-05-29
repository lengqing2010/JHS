Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �N�x�v��l�ݒ�
''' </summary>
''' <remarks>�N�x�v��l�ݒ�</remarks>
''' <history>
''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
''' </history>
Public Class ZensyaSyukeiDetailsBC
    Private objZensyaSyukeiDetailsDA As New ZensyaSyukeiDetailsDA

    ''' <summary>
    ''' �N�x�䗦�Ǘ��e�[�u������H���䗦���擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�H���䗦</returns>
    ''' <remarks>�N�x�䗦�Ǘ��e�[�u������H���䗦���擾����</remarks>
    ''' <history>
    ''' <para>2012/11/28 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function GetKakoJittusekiKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return objZensyaSyukeiDetailsDA.SelKakoJittusekiKanriData(strKeikakuNendo)
    End Function

    ''' <summary>
    ''' �S�Ђ̏ڍ׏W�v�f�[�^���擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <param name="strEigyouKbn">�c�Ƌ敪</param>
    ''' <param name="arrList">�����X�g</param>
    ''' <returns>�ڍ׏W�v�f�[�^</returns>
    ''' <remarks>�S�Ђ̏ڍ׏W�v�f�[�^���擾����</remarks>
    ''' <history>
    ''' <para>2012/11/28 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function GetSensyaSyuukeiData(ByVal strKeikakuNendo As String, _
                                         ByVal strEigyouKbn As String, _
                                         ByVal arrList As ArrayList) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return objZensyaSyukeiDetailsDA.SelSensyaSyuukeiData(strKeikakuNendo, strEigyouKbn, arrList)
    End Function

End Class
