Imports System.Reflection
''' <summary>
''' �Q�̃��R�[�h�N���X��蓯�ꖼ�����ꑮ���̃v���p�e�B�f�[�^�𕡐����܂�(�ÓI�����o)
''' </summary>
''' <remarks>�Q�ƃ��R�[�h�̃v���p�e�B���A�����Ɠ���̓��e������<br/>
''' �ݒ背�R�[�h�̃v���p�e�B�ɎQ�Ɨp�f�[�^��ݒ肷��</remarks>
Public Class RecordMappingHelper

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '�ÓI�ϐ��Ƃ��ăN���X�^�̃C���X�^���X�𐶐�
    Private Shared _instance = New RecordMappingHelper()

    '�ÓI�֐��Ƃ��ăN���X�^�̃C���X�^���X��Ԃ��֐���p��
    Public Shared ReadOnly Property Instance() As RecordMappingHelper
        Get
            '�ÓI�ϐ����������Ă����ꍇ�̂݁A�C���X�^���X�𐶐�����
            If IsDBNull(_instance) Then
                _instance = New RecordMappingHelper()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' �Q�̃��R�[�h�N���X��蓯�ꖼ�����ꑮ���̃v���p�e�B�f�[�^�𕡐����܂�
    ''' </summary>
    ''' <param name="fromRecord">�Q�Ɨp���R�[�h</param>
    ''' <param name="toRecord">�ݒ�p���R�[�h���R�[�h</param>
    ''' <returns>����ɏ������s��ꂽ�ꍇ:True</returns>
    ''' <remarks></remarks>
    Public Function CopyRecordData(ByVal fromRecord As Object, _
                                   ByRef toRecord As Object) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CopyRecordData", _
                                            fromRecord, _
                                            toRecord)

        ' �Q�Ɨp���R�[�h�̃v���p�e�B���
        Dim fromPropertyInfo As System.Reflection.PropertyInfo
        ' �ݒ�p���R�[�h�̃v���p�e�B���
        Dim toPropertyInfo As System.Reflection.PropertyInfo

        ' �Q�Ɨp�̃v���p�e�B��񕪃��[�v
        For Each fromPropertyInfo In fromRecord.GetType().GetProperties

            ' �ݒ�p�̃v���p�e�B��񕪃��[�v���A���ꍀ�ڂ�T��
            For Each toPropertyInfo In toRecord.GetType().GetProperties

                ' �v���p�e�B�̖��̂Ƒ�������v�����ꍇ�A�ݒ��փf�[�^���Z�b�g����
                If fromPropertyInfo.Name = toPropertyInfo.Name And _
                fromPropertyInfo.GetType().ToString() = toPropertyInfo.GetType().ToString() Then

                    ' �Q�Ɨp�f�[�^�̎擾
                    Dim fromItemData As Object
                    Try
                        fromItemData = fromRecord.GetType().InvokeMember(fromPropertyInfo.Name, _
                                    BindingFlags.GetProperty, _
                                    Nothing, _
                                    fromRecord, _
                                    Nothing)
                    Catch ex As Exception
                        ' �擾�Ɏ��s�����ꍇ�A�ݒ肵�Ȃ�
                        Exit For
                    End Try


                    ' �ݒ�p���R�[�h�ɃZ�b�g
                    ' �ݒ��v���p�e�B�֐ݒ�
                    Try
                        toPropertyInfo.SetValue(toRecord, fromItemData, Nothing)
                    Catch ex As Exception
                        ' �擾�Ɏ��s�����ꍇ�A�ݒ肵�Ȃ�
                        Exit For
                    End Try

                End If
            Next
        Next

        Return True

    End Function
End Class
