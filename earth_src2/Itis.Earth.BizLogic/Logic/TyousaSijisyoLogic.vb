Imports Itis.Earth.DataAccess

Public Class TyousaSijisyoLogic

    ''' <summary>�����X�������Ɖ�N���X�̃C���X�^���X���� </summary>
    Private TyousaSijisyoDataAccess As New TyousaSijisyoDataAccess

    ''' <summary>�����w�����</summary>
    ''' <returns>�����w�����</returns>
    ''' <history>2016/11/24�@������(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTyousaSijisyo(ByVal kbn As String _
                                   , ByVal hosyousyo_no As String) As Data.DataTable

        Return TyousaSijisyoDataAccess.SelTyousaSijisyo(kbn, hosyousyo_no)


    End Function
End Class
