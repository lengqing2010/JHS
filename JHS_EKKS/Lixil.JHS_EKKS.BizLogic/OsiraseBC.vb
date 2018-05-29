Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.DataAccess
Public Class OsiraseBC
    Private OsiraseDA As New OsiraseDA
#Region "���m�点���̎擾"

    ''' <summary>
    ''' ���m�点�f�[�^���擾���܂�
    ''' </summary>
    ''' <returns>���m�点�f�[�^�̃��R�[�h���X�g</returns>
    ''' <remarks></remarks>
    Public Function GetOsiraseRecords() As List(Of OsiraseRecord)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        ' �߂�l�ƂȂ郊�X�g
        Dim returnRec As New List(Of OsiraseRecord)

        ' �d���f�[�^�擾�N���X
        Dim dataAccess As DataTable = OsiraseDA.GetOsiraseData

        ' �l���擾�ł����ꍇ�A�߂�l�ɐݒ肷��
        For i As Integer = 0 To dataAccess.Rows.Count - 1
            Dim osiraseRec As New OsiraseRecord

            osiraseRec.Nengappi = CType(dataAccess.Rows(i).Item(0), Date)
            osiraseRec.NyuuryokuBusyo = dataAccess.Rows(i).Item(1).ToString
            osiraseRec.NyuuryokuMei = dataAccess.Rows(i).Item(2).ToString
            osiraseRec.HyoujiNaiyou = dataAccess.Rows(i).Item(3).ToString
            osiraseRec.LinkSaki = dataAccess.Rows(i).Item(4).ToString
            ' ���X�g�ɃZ�b�g
            returnRec.Add(osiraseRec)
        Next


        Return returnRec

    End Function
#End Region
End Class
