Imports System.Transactions
Imports Itis.Earth.DataAccess

''' <summary>�����񍐏��Ǘ�</summary>
''' <remarks>�����񍐏��Ǘ��p�@�\��񋟂���</remarks>
''' <history>
''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KensaHoukokusyoKanriSearchListLogic

    Private KensaHoukokusyoKanriSearchDA As New KensaHoukokusyoKanriSearchListDataAccess
    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = ninsyou.GetUserID()
    Private InputDate As String

    ''' <summary>
    ''' �����񍐏��Ǘ��e�[�u�����擾����
    ''' </summary>
    ''' <param name="strKakunouDateFrom">�i�[��From</param>
    ''' <param name="strKakunouDateTo">�i�[��To</param>
    ''' <param name="strSendDateFrom">������From</param>
    ''' <param name="strSendDateTo">������To</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strNoFrom">�ԍ�From</param>
    ''' <param name="strNoTo">�ԍ�To</param>
    ''' <param name="strKameitenCdFrom">�����XCD</param>
    ''' <param name="strKensakuJyouken">�����������</param>
    ''' <param name="blnKensakuTaisyouGai">����͌����ΏۊO</param>
    ''' <param name="blnSendDateTaisyouGai">�������Z�b�g�ς݂͑ΏۊO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function GetKensaHoukokusyoKanriSearch(ByVal strKakunouDateFrom As String, ByVal strKakunouDateTo As String, ByVal strSendDateFrom As String, _
                                                  ByVal strSendDateTo As String, ByVal strKbn As String, ByVal strNoFrom As String, _
                                                  ByVal strNoTo As String, ByVal strKameitenCdFrom As String, ByVal strKensakuJyouken As String, _
                                                  ByVal blnKensakuTaisyouGai As Boolean, ByVal blnSendDateTaisyouGai As Boolean) As DataTable

        Return KensaHoukokusyoKanriSearchDA.SelKensaHoukokusyoKanriSearch(strKakunouDateFrom, strKakunouDateTo, strSendDateFrom, strSendDateTo, strKbn, strNoFrom, _
                                                                        strNoTo, strKameitenCdFrom, strKensakuJyouken, blnKensakuTaisyouGai, blnSendDateTaisyouGai)

    End Function

    ''' <summary>
    ''' �����񍐏��Ǘ��������擾����
    ''' </summary>
    ''' <param name="strKakunouDateFrom">�i�[��From</param>
    ''' <param name="strKakunouDateTo">�i�[��To</param>
    ''' <param name="strSendDateFrom">������From</param>
    ''' <param name="strSendDateTo">������To</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strNoFrom">�ԍ�From</param>
    ''' <param name="strNoTo">�ԍ�To</param>
    ''' <param name="strKameitenCdFrom">�����XCD</param>
    ''' <param name="strKensakuJyouken">�����������</param>
    ''' <param name="blnKensakuTaisyouGai">����͌����ΏۊO</param>
    ''' <param name="blnSendDateTaisyouGai">�������Z�b�g�ς݂͑ΏۊO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function GetKensaHoukokusyoKanriSearchCount(ByVal strKakunouDateFrom As String, ByVal strKakunouDateTo As String, ByVal strSendDateFrom As String, _
                                                  ByVal strSendDateTo As String, ByVal strKbn As String, ByVal strNoFrom As String, _
                                                  ByVal strNoTo As String, ByVal strKameitenCdFrom As String, ByVal strKensakuJyouken As String, _
                                                  ByVal blnKensakuTaisyouGai As Boolean, ByVal blnSendDateTaisyouGai As Boolean) As Integer

        Return KensaHoukokusyoKanriSearchDA.SelKensaHoukokusyoKanriSearchCount(strKakunouDateFrom, strKakunouDateTo, strSendDateFrom, strSendDateTo, strKbn, strNoFrom, _
                                                                        strNoTo, strKameitenCdFrom, strKensakuJyouken, blnKensakuTaisyouGai, blnSendDateTaisyouGai)

    End Function

    ''' <summary>
    ''' <summary>�����񍐏��Ǘ����X�V����</summary>
    ''' </summary>
    ''' <param name="strHassoudate">������</param>
    ''' <param name="strSouhutantousya">���t�S����</param>
    ''' <param name="dtKensa">�����񍐏��Ǘ��e�[�u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function UpdKensahoukokusho(ByVal strHassoudate As String, ByVal strSouhutantousya As String, ByVal dtKensa As DataTable) As Boolean
        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                KensaHoukokusyoKanriSearchDA.UpdKensahoukokusho(strHassoudate, strSouhutantousya, userId, dtKensa)
                scope.Complete()

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
        Return True
    End Function

    ''' <summary>
    ''' <summary>�����񍐏��Ǘ����X�V����</summary>
    ''' </summary>
    ''' <param name="dtKensa">�����񍐏��Ǘ��e�[�u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function UpdKensahoukokushoTorikesi(ByVal dtKensa As DataTable) As Boolean
        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try

                KensaHoukokusyoKanriSearchDA.UpdKensahoukokushoTorikesi(userId, dtKensa)
                scope.Complete()
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
        Return True
    End Function

    ''' <summary>
    ''' �����񍐏��Ǘ��e�[�u�����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����XCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelMkameiten(ByVal strKameitenCd As String) As DataTable

        Return KensaHoukokusyoKanriSearchDA.SelMkameiten(strKameitenCd)

    End Function

End Class
