Imports System.Transactions
Imports Itis.Earth.DataAccess

''' <summary>�����񍐏�_�e���[�o�͉��</summary>
''' <remarks>�����񍐏�_�e���[�o�͉�ʗp�@�\��񋟂���</remarks>
''' <history>
''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KensaHoukokusyoOutputLogic

    Private KensaHoukokusyoOutputDA As New KensaHoukokusyoOutputDataAccess
    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = Ninsyou.GetUserID()
    Private InputDate As String

    ''' <summary>
    ''' �����񍐏��Ǘ��e�[�u�����擾����
    ''' </summary>
    ''' <param name="strSendDateFrom">������From</param>
    ''' <param name="strSendDateTo">������To</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="tbxNoTo">�ԍ�From</param>
    ''' <param name="strNoTo">�ԍ�To</param>
    ''' <param name="strKameitenCdFrom">�����XCD</param>
    ''' <param name="strKensakuJyouken">�����������</param>
    ''' <param name="blnKensakuTaisyouGai">����͌����ΏۊO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function GetKensaHoukokusyoKanriSearch(ByVal strSendDateFrom As String, ByVal strSendDateTo As String, ByVal strKbn As String, _
                                                  ByVal tbxNoTo As String, ByVal strNoTo As String, ByVal strKameitenCdFrom As String, _
                                                  ByVal strKensakuJyouken As String, ByVal blnKensakuTaisyouGai As Boolean) As Data.DataTable

        Return KensaHoukokusyoOutputDA.SelKensaHoukokusyoKanriSearch(strSendDateFrom, strSendDateTo, strKbn, tbxNoTo, strNoTo, strKameitenCdFrom, strKensakuJyouken, blnKensakuTaisyouGai)

    End Function

    ''' <summary>
    ''' �Ǘ��\EXCEL�o�͂��擾����
    ''' </summary>
    ''' <param name="strKanriNo">�Ǘ�No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function GetKanrihyouExcelInfo(ByVal strKanriNo As String) As Data.DataTable

        Return KensaHoukokusyoOutputDA.SelKanrihyouExcelInfo(strKanriNo)
    End Function

    ''' <summary>
    ''' ���t��PDF�o�͂��擾����
    ''' </summary>
    ''' <param name="strKanriNo">�Ǘ�No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function GetSyoufujyouPdfInfo(ByVal strKanriNo As String) As Data.DataTable

        Return KensaHoukokusyoOutputDA.SelSyoufujyouPdfInfo(strKanriNo)
    End Function

    ''' <summary>
    ''' �񍐏�PDF�o�͂��擾����
    ''' </summary>
    ''' <param name="strKanriNo">�Ǘ�No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function GetHoukokusyoPdfInfo(ByVal strKanriNo As String) As Data.DataTable

        Return KensaHoukokusyoOutputDA.SelHoukokusyoPdfInfo(strKanriNo)
    End Function




    ''' <summary>
    ''' �����񍐏��Ǘ��������擾����
    ''' </summary>
    ''' <param name="strSendDateFrom">������From</param>
    ''' <param name="strSendDateTo">������To</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="tbxNoTo">�ԍ�From</param>
    ''' <param name="strNoTo">�ԍ�To</param>
    ''' <param name="strKameitenCdFrom">�����XCD</param>
    ''' <param name="strKensakuJyouken">�����������</param>
    ''' <param name="blnKensakuTaisyouGai">����͌����ΏۊO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function GetKensaHoukokusyoKanriSearchCount(ByVal strSendDateFrom As String, ByVal strSendDateTo As String, ByVal strKbn As String, _
                                                      ByVal tbxNoTo As String, ByVal strNoTo As String, ByVal strKameitenCdFrom As String, _
                                                      ByVal strKensakuJyouken As String, ByVal blnKensakuTaisyouGai As Boolean) As Integer

        Return KensaHoukokusyoOutputDA.SelKensaHoukokusyoKanriSearchCount(strSendDateFrom, strSendDateTo, strKbn, tbxNoTo, strNoTo, strKameitenCdFrom, strKensakuJyouken, blnKensakuTaisyouGai)

    End Function

    ''' <summary>
    ''' <summary>�����񍐏��Ǘ����X�V����</summary>
    ''' </summary>
    ''' <param name="dtKensa">�����񍐏��Ǘ��e�[�u��</param>
    ''' <param name="strFlg">�{�^���敪(1:�Ǘ��\EXCEL�o��;2:���t��PDF�o��;3:�񍐏�PDF�o��)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function UpdKensahoukokusho(ByVal dtKensa As DataTable, ByVal strFlg As String) As Boolean
        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                KensaHoukokusyoOutputDA.UpdKensahoukokusho(userId, dtKensa, strFlg)
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
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelMkameiten(ByVal strKameitenCd As String) As DataTable

        Return KensaHoukokusyoOutputDA.SelMkameiten(strKameitenCd)

    End Function

End Class

