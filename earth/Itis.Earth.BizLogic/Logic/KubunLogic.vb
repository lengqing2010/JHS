
''' <summary>
''' �敪�f�[�^�̃r�W�l�X���W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class KubunLogic
    Inherits DropDownLogicHelper

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �敪�������ɋ敪���R�[�h���P���擾���܂�
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <returns>�敪���R�[�h</returns>
    ''' <remarks></remarks>
    Public Function GetInfo(ByVal kubun As String) As KubunRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetInfo", _
                                            kubun)

        Dim kubunRec As New KubunRecord
        Dim dataAccess As KubunDataAccess = New KubunDataAccess

        Dim torikeshi As Integer
        Dim kubunMei As String = ""

        If dataAccess.GetDataBy(kubun, torikeshi, kubunMei) = False Then
            Debug.WriteLine("�擾�o���܂���ł���")
            Return Nothing
        Else
            kubunRec.Kubun = kubun
            kubunRec.Torikeshi = torikeshi
            kubunRec.KubunMei = kubunMei

            Debug.WriteLine("kubun_rec.Kubun" + kubunRec.Kubun)
            Debug.WriteLine("kubun_rec.Torikeshi" + kubunRec.Torikeshi.ToString)
            Debug.WriteLine("kubun_rec.KubunMei" + kubunRec.KubunMei)

        End If

        Return kubunRec

    End Function

End Class
