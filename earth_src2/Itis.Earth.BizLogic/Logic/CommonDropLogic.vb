Imports Itis.Earth.DataAccess

Public Class CommonDropLogic

    ''' <summary> ���C�����j���[�N���X�̃C���X�^���X���� </summary>
    Private CommonDropDA As New CommonDropDataAccess
    ''' <summary> ���ʃf�[�^���擾����</summary>
    Public Function GetCommonDropInfo(ByVal kbn As Integer) As CommonDropDataSet.DropTableDataTable
        Return CommonDropDA.SelCommonInfo(kbn)
    End Function

End Class
