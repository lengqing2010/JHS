Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data

Public Class OsiraseDA

    ''' <summary>
    ''' ���m�点���R�[�h���擾���܂�
    ''' </summary>
    ''' <returns>���m�点�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetOsiraseData() As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�߂�f�[�^�Z�b�g
        Dim dsInfo As New Data.DataSet

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim commandText As String = " SELECT " & _
                                    "   nengappi, " & _
                                    "   nyuuryoku_busyo, " & _
                                    "   nyuuryoku_mei, " & _
                                    "   hyouji_naiyou, " & _
                                    "   link_saki" & _
                                    " FROM " & _
                                    "    t_osirase WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    torikesi = 0  " & _
                                    " ORDER BY nengappi desc"

      

        FillDataset(ManagerDA.Connection, CommandType.Text, commandText, _
            dsInfo, "dsInfo")



        Return dsInfo.Tables("dsInfo")

    End Function

End Class
