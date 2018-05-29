Imports Itis.Earth.DataAccess
Imports System.Transactions
Public Class KyoutuuJyouhouLogic
    ''' <summary> ���C�����j���[�N���X�̃C���X�^���X���� </summary>
    Private KyoutuuJyouhouDataSet As New KyoutuuJyouhouDataAccess
    ''' <summary>�����X�}�X�g�����擾����</summary>
    Public Function GetKameitenInfo(ByVal strKameitenCd As String) As KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
        Return KyoutuuJyouhouDataSet.SelKyoutuuJyouhouInfo(strKameitenCd)
    End Function
    ''' <summary>�����X�}�X�g�����X�V����</summary>
    Public Function SetUpdKyoutuuJyouhouInfo(ByVal dtKyoutuuJyouhouData As KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable) As Boolean
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            KyoutuuJyouhouDataSet.UpdKyoutuuJyouhouInfo(dtKyoutuuJyouhouData)
            KyoutuuJyouhouDataSet.UpdKyoutuuJyouhouRenkei(dtKyoutuuJyouhouData.Rows(0).Item("kameiten_cd"), dtKyoutuuJyouhouData.Rows(0).Item("simei"))
            scope.Complete()
            Return True
        End Using
    End Function
    Public Function SelKensuu(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strNengetu As String) As DataTable

        Return KyoutuuJyouhouDataSet.SelKensuu(strKameitenCd, strKbn, strNengetu)

    End Function
    Public Function SelHosyousyo(ByVal strKameitenCd As String, ByVal strNengetu As String, ByVal strFlg As String) As DataTable
        Return KyoutuuJyouhouDataSet.SelHosyousyo(strKameitenCd, strNengetu, strFlg)
    End Function

    Public Function SetUPDJiban(ByVal strParam As String, ByVal strUserId As String) As Boolean
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                For i As Integer = 0 To Split(strParam, ",").Length - 1
                    Dim strkbn As String = Left(Split(Split(strParam, ",")(i), "|")(0), 1)
                    Dim strNo As String = Right(Split(Split(strParam, ",")(i), "|")(0), 10)
                    Dim strFlg As String = Split(Split(strParam, ",")(i), "|")(1)
                    KyoutuuJyouhouDataSet.UpdJiban(strkbn, strNo, strFlg, strUserId)
                    KyoutuuJyouhouDataSet.UpdJibanRenkei(strkbn, strNo, strUserId)
                Next

                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using
    End Function

    ''' <summary>
    ''' �u����vddl�̃f�[�^���擾����
    ''' </summary>
    ''' <returns>�u����vddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ�</history>
    Public Function GetTorikesiList(Optional ByVal strCd As String = "") As Data.DataTable

        '�߂�l
        Return KyoutuuJyouhouDataSet.SelTorikesiList(strCd)

    End Function

    ''' <summary>
    ''' FC������Ђ��擾����
    ''' </summary>
    ''' <returns>�u����vddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Public Function GetFcTyousaKaisya(ByVal strEigyousyoCd As String) As Data.DataTable

        '�߂�l
        Return KyoutuuJyouhouDataSet.SelFcTyousaKaisya(strEigyousyoCd)

    End Function

End Class
