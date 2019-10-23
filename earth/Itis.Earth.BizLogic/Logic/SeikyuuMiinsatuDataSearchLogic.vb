Imports System.Data.SqlClient
Imports System.Web.UI

Public Class SeikyuuMiinsatuDataSearchLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic

    'Utilities��StringLogic�N���X
    Dim sLogic As New StringLogic

    '�X�V�����ێ��p
    Dim pUpdDateTime As DateTime

    ''' <summary>
    ''' ������������̐����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="dtRec">�����f�[�^���R�[�h</param>
    ''' <param name="startRow">�J�n�s</param>
    ''' <param name="endRow">�ŏI�s</param>
    ''' <param name="allCount">�S����</param>
    ''' <returns>�����f�[�^�����p���R�[�h��List(Of SeikyuuDataRecord)</returns>
    ''' <remarks></remarks>

    Public Function GetSeikyuuMiinsatuData(ByVal sender As Object, _
                                   ByVal dtRec As SeikyuuDataRecord, _
                                   ByVal startRow As Integer, _
                                   ByVal endRow As Integer, _
                                   ByRef allCount As Integer) As List(Of SeikyuuDataRecord)


        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuMiinsatuData", _
                                            dtRec, _
                                            startRow, _
                                            endRow, _
                                            allCount _
                                            )

        '�������s�N���X
        Dim dataAccess As New SeikyuuMiinsatuDataAccess

        '�擾�f�[�^�i�[�p���X�g
        Dim list As New List(Of SeikyuuDataRecord)

        Try
            '���������̎��s
            Dim table As DataTable = dataAccess.GetSearchSeikyuusyoTbl(dtRec)

            ' ���������Z�b�g
            allCount = table.Rows.Count

            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            list = DataMappingHelper.Instance.getMapArray(Of SeikyuuDataRecord)(GetType(SeikyuuDataRecord), table, startRow, endRow)

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

        Return list
    End Function

End Class
