Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI


Public Class KousinRirekiLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic



    ''' <summary>
    ''' �Y���e�[�u���̏����擾���܂�
    ''' </summary>
    ''' <param name="recKey">��������</param>
    ''' <returns>�Y�����R�[�h���i�[�������X�g �^ 0���̏ꍇ��Nothing</returns>
    ''' <remarks>����������KEY�ɂ��Ď擾</remarks>
    Public Function getSearchKeyDataList(ByVal sender As Object _
                                                , ByVal recKey As KousinRirekiDataKeyRecord _
                                                , ByVal startRow As Integer _
                                                , ByVal endRow As Integer _
                                                , ByRef allCount As Integer) As List(Of KousinRirekiDataRecord)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchKeyDataTable", recKey _
                                                                                                , startRow _
                                                                                                , endRow _
                                                                                                , allCount _
                                                                                                )

        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New KousinRirekiDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim listResult As New List(Of KousinRirekiDataRecord)

        Try
            dTblResult = clsDataAcc.getSearchTable(recKey)

            ' ���������Z�b�g
            allCount = dTblResult.Rows.Count

            If allCount > 0 Then
                ' �������ʂ��i�[�p���X�g�ɃZ�b�g
                listResult = DataMappingHelper.Instance.getMapArray(Of KousinRirekiDataRecord)(GetType(KousinRirekiDataRecord), dTblResult, startRow, endRow)
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��-1���Z�b�g
            allCount = -1
        End Try

        Return listResult
    End Function

End Class
