Imports System.Text
Imports System.IO

Public Class MessageList

    ''' <summary>
    ''' ���b�Z�[�W�Ǘ��N���X�ɑ��݂��郁�b�Z�[�W�̈ꗗ��TSV�t�@�C���Ƃ���<br/>
    ''' �o�͂��܂�<br/>
    ''' MSG001E\t���b�Z�[�W�P�ł�\r\n<br/>
    ''' MSG002E\t���b�Z�[�W�Q�ł�\r\n<br/>
    ''' </summary>
    ''' <param name="output_path">�o�͐�̃p�X</param>
    ''' <remarks></remarks>
    Public Sub getTsvFile(ByVal output_path As String)

        ' ���b�Z�[�W�Ǘ��N���X�̃t�B�[���h���
        Dim field_info As System.Reflection.FieldInfo

        Dim out_data As New StringBuilder

        For Each field_info In GetType(Messages).GetFields

            ' String�̃v���p�e�B�̓��b�Z�[�W�萔�݂̂Ȃ̂�String�Œ��o
            If field_info.FieldType.FullName = GetType(String).ToString() Then

                ' ���b�Z�[�W�����i�[�p
                Dim value As New Object
                ' ���b�Z�[�W���擾
                value = field_info.GetValue(Messages.Instance)

                out_data.Append(field_info.Name & vbTab & value & vbCrLf)

            End If

        Next

        ' �f�[�^������΃t�@�C���o�͂���
        If out_data.ToString().Trim() <> "" Then
            Try
                Using sw As StreamWriter = New StreamWriter(output_path)
                    sw.Write(out_data.ToString())
                    sw.Close()
                End Using
            Catch ex As Exception
                Debug.WriteLine("�o�͎��s")
            End Try
        End If

    End Sub

End Class
