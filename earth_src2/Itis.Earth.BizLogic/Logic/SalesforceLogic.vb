Imports Itis.Earth.DataAccess

Public Class SalesforceLogic

    ''' <summary> ���C�����j���[�N���X�̃C���X�^���X���� </summary>
    Private SalesforceDataAccess As New SalesforceDataAccess
    ''' <summary> ���ʃf�[�^���擾����</summary>
    Public Function GetSalesforceHikasseiFlg(ByVal strKameitenCd As String) As String
        Return SalesforceDataAccess.GetSalesforceHikasseiFlg(strKameitenCd)
    End Function

    Public Function GetSalesforceHikasseiFlgByKbn(ByVal kbn As String) As String
        Return SalesforceDataAccess.GetSalesforceHikasseiFlgByKbn(kbn)
    End Function


End Class
