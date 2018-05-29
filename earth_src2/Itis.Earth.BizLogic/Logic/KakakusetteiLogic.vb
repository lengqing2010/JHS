Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class KakakusetteiLogic
    Private KakakusetteiDA As New KakakusetteiDataAccess

    '============================2011/04/26 �ԗ� �C�� �J�n��=====================================
    'Public Function SelKakakuInfo(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As DataTable
    '    Return KakakusetteiDA.SelKakakuInfo(strKbn, strHouhou, strGaiyou)
    'End Function
    ''' <summary>
    ''' ���i���i�ݒ�����擾����
    ''' </summary>
    ''' <param name="strKbn">���i�敪</param>
    ''' <param name="strHouhou">�������@</param>
    ''' <param name="strGaiyou">�����T�v</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <returns>���i���i�ݒ���e�[�u��</returns>
    ''' <remarks>���i���i�ݒ�����擾����</remarks>
    ''' <history>2011/04/26�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKakakuInfo(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String, Optional ByVal strSyouhinCd As String = "") As DataTable
        Return KakakusetteiDA.SelKakakuInfo(strKbn, strHouhou, strGaiyou, strSyouhinCd)
    End Function
    '============================2011/04/26 �ԗ� �C�� �I����=====================================

    Public Function SelKakutyouInfo(ByVal strSyubetu As String, Optional ByVal strTable As String = "") As DataTable
        Return KakakusetteiDA.SelKakutyouInfo(strSyubetu, strTable)
    End Function
    Public Function SelHaita(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String, ByVal strKousin As String) As DataTable

        Return KakakusetteiDA.SelHaita(strKbn, strHouhou, strGaiyou, strKousin)
    End Function

    Public Function SelJyuufuku(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As Boolean

        If KakakusetteiDA.SelJyuufuku(strKbn, strHouhou, strGaiyou).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function InsKakakusettei(ByVal strKbn As String, _
                                 ByVal strHouhou As String, _
                                 ByVal strGaiyou As String, _
                                 ByVal strSyouhinCd As String, _
                                 ByVal strSetteiBasyo As String, _
                                 ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                KakakusetteiDA.InsKakakusettei(strKbn, _
                                    strHouhou, _
                                    strGaiyou, _
                                    strSyouhinCd, _
                                    strSetteiBasyo, _
                                    strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function

    Public Function UpdKakakusettei(ByVal strKbn As String, _
                                 ByVal strHouhou As String, _
                                 ByVal strGaiyou As String, _
                                 ByVal strSyouhinCd As String, _
                                 ByVal strSetteiBasyo As String, _
                                 ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                If KakakusetteiDA.SelKakakuInfo(strKbn, strHouhou, strGaiyou).Rows.Count = 0 Then
                    scope.Dispose()
                    Return 2
                Else

                    KakakusetteiDA.UpdKakakusettei(strKbn, _
                                    strHouhou, _
                                    strGaiyou, _
                                    strSyouhinCd, _
                                    strSetteiBasyo, _
                                    strUserId)
                    scope.Complete()
                    Return 1
                End If

            Catch ex As Exception
                scope.Dispose()
                Return 0
            End Try

        End Using

    End Function

    Public Function DelKakakusettei(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try

                KakakusetteiDA.DelKakakusettei(strKbn, strHouhou, strGaiyou)
                scope.Complete()
                Return True

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function

    '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
    ''' <summary>
    ''' �d���`�F�b�N(���i�敪�A�������@�A���i����)
    ''' </summary>
    ''' <param name="strKbn">���i�敪</param>
    ''' <param name="strHouhou">�������@</param>
    ''' <param name="strSyouhin">���i����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2011/04/26�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function CheckJyuufukuSyouhin(ByVal strKbn As String, ByVal strHouhou As String, ByVal strSyouhin As String) As Boolean

        If KakakusetteiDA.SelJyuufukuSyouhin(strKbn, strHouhou, strSyouhin).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    '========================2011/04/26 �ԗ� �ǉ� �I����============================
End Class
