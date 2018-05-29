Imports Itis.Earth.DataAccess
Imports System.Transactions
Public Class KihonJyouhouLogic
    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private KihonJyouhouDA As New KihonJyouhouDataAccess
    Private KyoutuuJyouhouDataSet As New KyoutuuJyouhouDataAccess
    ''' <summary>基本情報を取得する</summary>
    Public Function GetKihonJyouhouInfo(ByVal strKameitenCd As String) As KihonJyouhouDataSet.KihonJyouhouTableDataTable
        Return KihonJyouhouDA.SelKihonJyouhouInfo(strKameitenCd)
    End Function
    ''' <summary>基本情報を更新する</summary>
    Public Function SetUpdKihonJyouhouInfo(ByVal dtKihonJyouhouData As KihonJyouhouDataSet.KihonJyouhouTableDataTable) As Boolean
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            KihonJyouhouDA.UpdKihonJyouhouInfo(dtKihonJyouhouData)
            KyoutuuJyouhouDataSet.UpdKyoutuuJyouhouRenkei(dtKihonJyouhouData.Rows(0).Item("kameiten_cd"), dtKihonJyouhouData.Rows(0).Item("upd_login_user_id"))
            scope.Complete()
            Return True
        End Using
    End Function
End Class
