Imports System.Reflection
''' <summary>
''' �Q�̃��R�[�h�N���X��荀�ڂ̔�r���s���܂�(�ÓI�����o)
''' </summary>
''' <remarks></remarks>
Public Class RecordCompareHelper

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '�ÓI�ϐ��Ƃ��ăN���X�^�̃C���X�^���X�𐶐�
    Private Shared _instance = New RecordCompareHelper()

    '�ÓI�֐��Ƃ��ăN���X�^�̃C���X�^���X��Ԃ��֐���p��
    Public Shared ReadOnly Property Instance() As RecordCompareHelper
        Get
            '�ÓI�ϐ����������Ă����ꍇ�̂݁A�C���X�^���X�𐶐�����
            If IsDBNull(_instance) Then
                _instance = New RecordCompareHelper()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' �Q�̃��R�[�h�N���X�̑S���ڂ��r���A����̏ꍇTrue�A����̏ꍇFalse��Ԃ��܂�
    ''' </summary>
    ''' <param name="targetRecord1">��r�ΏۂP���R�[�h</param>
    ''' <param name="targetRecord2">��r�ΏۂQ���R�[�h</param>
    ''' <returns>����̏ꍇ:True ����̏ꍇ:False</returns>
    ''' <remarks></remarks>
    Public Function CheckCompareAll(ByVal targetRecord1 As Object, _
                                    ByVal targetRecord2 As Object) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckCompareAll", _
                                            targetRecord1, _
                                            targetRecord2)

        ' ��r�ΏۂP���R�[�h�̃v���p�e�B���
        Dim target1Info As System.Reflection.PropertyInfo
        ' ��r�ΏۂQ���R�[�h�̃v���p�e�B���
        Dim target2Info As System.Reflection.PropertyInfo

        ' �Q�Ɨp�̃v���p�e�B��񕪃��[�v
        For Each target1Info In targetRecord1.GetType().GetProperties

            ' �ݒ�p�̃v���p�e�B��񕪃��[�v���A���ꍀ�ڂ�T��
            For Each target2Info In targetRecord2.GetType().GetProperties

                ' �v���p�e�B�̖��̂Ƒ�������v�����ꍇ�A���e�̔�r���s��
                If target1Info.Name = target2Info.Name And _
                target1Info.GetType().ToString() = target2Info.GetType().ToString() Then

                    ' ��r�ΏۂP�f�[�^�̎擾
                    Dim targetItemData1 As New Object
                    Try
                        targetItemData1 = target1Info.GetValue(targetRecord1, Nothing)


                    Catch ex As Exception
                        ' �擾�Ɏ��s�����ꍇ�A��r���Ȃ�
                        Exit For
                    End Try

                    ' ��r�ΏۂQ�f�[�^�̎擾
                    Dim targetItemData2 As New Object
                    Try
                        targetItemData2 = target2Info.GetValue(targetRecord2, Nothing)
                    Catch ex As Exception
                        ' �擾�Ɏ��s�����ꍇ�A��r���Ȃ�
                        Exit For
                    End Try

                    If (Not targetItemData2 Is Nothing And targetItemData1 Is Nothing) Or _
                       (targetItemData2 Is Nothing And Not targetItemData1 Is Nothing) Then
                        ' �ǂ��炩��Nothing��False
                        Return False
                    ElseIf targetItemData2 Is Nothing And targetItemData1 Is Nothing Then
                        ' �����Nothing��OK
                        Exit For
                    End If

                    ' �s��v�̏ꍇFalse��Ԃ�
                    If targetItemData1.Equals(targetItemData2) = False Then
                        Return False
                    End If

                End If
            Next
        Next

        Return True

    End Function
End Class
