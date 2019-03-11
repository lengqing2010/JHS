Imports Itis.Earth.DataAccess
Imports System.Transactions
Public Class KihonJyouhouLogic
    ''' <summary> ���C�����j���[�N���X�̃C���X�^���X���� </summary>
    Private KihonJyouhouDA As New KihonJyouhouDataAccess
    Private KyoutuuJyouhouDataSet As New KyoutuuJyouhouDataAccess
    ''' <summary>��{�����擾����</summary>
    Public Function GetKihonJyouhouInfo(ByVal strKameitenCd As String) As KihonJyouhouDataSet.KihonJyouhouTableDataTable
        Return KihonJyouhouDA.SelKihonJyouhouInfo(strKameitenCd)
    End Function
    ''' <summary>��{�����X�V����</summary>
    Public Function SetUpdKihonJyouhouInfo(ByVal dtKihonJyouhouData As KihonJyouhouDataSet.KihonJyouhouTableDataTable) As Boolean
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            KihonJyouhouDA.UpdKihonJyouhouInfo(dtKihonJyouhouData)
            KyoutuuJyouhouDataSet.UpdKyoutuuJyouhouRenkei(dtKihonJyouhouData.Rows(0).Item("kameiten_cd"), dtKihonJyouhouData.Rows(0).Item("upd_login_user_id"))
            KyoutuuJyouhouDataSet.UpdKameitenTaiouSyouhinKbnRireki(dtKihonJyouhouData.Rows(0).Item("kameiten_cd"), dtKihonJyouhouData.Rows(0).Item("upd_login_user_id"), dtKihonJyouhouData.Rows(0).Item("taiou_syouhin_kbn"))
            scope.Complete()
            Return True
        End Using
    End Function

    ''' <summary>
    ''' ddl�̃f�[�^���擾����
    ''' </summary>
    ''' <returns>�u����vddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetKakutyouMeisyouList(ByVal meisyou_syubetu As Integer, ByVal strCd As String) As Data.DataTable
        Return KihonJyouhouDA.SelKakutyouMeisyouList(meisyou_syubetu, strCd)
    End Function
End Class
