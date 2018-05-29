Imports Itis.Earth.DataAccess
''' <summary>�����挳�����[�o�͏���</summary>
''' <history>
''' <para>2010/07/14�@���Ǘz(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class SiharaisakiMototyouOutputLogic
    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Private siharaisakiMototyouOutputDataAccess As New SiharaisakiMototyouOutputDataAccess

#Region "�x���挳��_�J�z�c���擾"
    ''' <summary>
    ''' �x���挳��_�J�z�c���擾
    ''' </summary>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strFromDate">�N����FROM</param>
    ''' <returns>�J�z�c��(Long�^)</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiSakiMototyouKurikosiZan(ByVal strTysKaisyaCd As String, _
                                                      ByVal strFromDate As String _
                                                      ) As Long


        Dim Data As Object
        Dim retData As Long

        '�f�[�^�擾
        Data = siharaisakiMototyouOutputDataAccess.SelSiharaiSakiMototyouKurikosiZan(strTysKaisyaCd, strFromDate)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'Long�ɃL���X�g
                retData = CType(Data, Long)
            Catch ex As Exception
                '���s������[��
                retData = 0
            End Try
        End If

        '�l�߂�
        Return retData

    End Function

    ''' <summary>�d���f�[�^�e�[�u���A�x���f�[�^�e�[�u���f�[�^���擾����</summary>
    ''' <param name="strTysKaisyaCd"> ������ЃR�[�h</param>
    ''' <param name="strJigyousyoCd"> �x���W�v�掖�Ə��R�[�h</param>
    ''' <param name="strFromDate"> ���o����FROM(YYYY/MM/DD)</param>
    ''' <param name="strToDate"> ���o����TO(YYYY/MM/DD)</param>
    ''' <returns>����A�����̃f�[�^</returns>
    Public Function GetSiharaiSakiMototyouData(ByVal strTysKaisyaCd As String, _
                                                   ByVal strJigyousyoCd As String, _
                                                   ByVal strFromDate As String, _
                                                   ByVal strToDate As String) As Data.DataTable
        '�f�[�^�擾
        Return siharaisakiMototyouOutputDataAccess.SelSiharaiSakiMototyouData(strTysKaisyaCd, strJigyousyoCd, strFromDate, strToDate)

    End Function
#End Region

End Class
