Imports Itis.Earth.DataAccess

''' <summary>�^�M�Ǘ������擾����</summary>
''' <remarks>�^�M�Ǘ����Ɖ��񋟂���</remarks>
''' <history>
''' <para>2009/07/16�@���c(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class YosinJyouhouInputLogic
    ''' <summary> �^�M�Ǘ����N���X�̃C���X�^���X���� </summary>
    Private YosinJyouhouInputDA As New YosinJyouhouInputDataAccess
    ''' <summary>
    ''' �^�M�Ǘ������擾����
    ''' </summary>
    ''' <param name="nayose_cd">�����R�[�h</param>
    ''' <returns>�^�M�Ǘ����f�[�^�e�[�u��</returns>
    Public Function GetYosinKanriInfo(ByVal nayose_cd As String) As YosinJyouhouInputDataSet.YosinKanriInfoTableDataTable
        Return YosinJyouhouInputDA.GetYosinKanriInfo(nayose_cd)
    End Function
    Public Function GetYosinMeisai(ByVal nayose_cd As String, ByVal tyousa As Boolean, ByVal kouji As Boolean, ByVal sonota As Boolean, ByVal yotei As Boolean, ByVal jisseki As Boolean) As DataSet
        Return YosinJyouhouInputDA.GetYosinMeisai(nayose_cd, tyousa, kouji, sonota, yotei, jisseki)
    End Function
    Public Function GetNayoseInfo(ByVal kameiten_cd As String) As DataTable
        Return YosinJyouhouInputDA.GetNayoseInfo(kameiten_cd)
    End Function
    ''' <summary>
    ''' �����\������擾����
    ''' </summary>
    ''' <param name="nayose_cd">�����R�[�h</param>
    ''' <returns>�����\����f�[�^�e�[�u��</returns>
    Public Function GetNyuukinYoteiInfo(ByVal nayose_cd As String, _
                                        ByVal ruiseki_nyuukin_set_date As DateTime) As YosinJyouhouInputDataSet.NyuukinYoteiInfoTableDataTable
        Return YosinJyouhouInputDA.GetNyuukinYoteiInfo(nayose_cd, ruiseki_nyuukin_set_date)
    End Function
End Class
