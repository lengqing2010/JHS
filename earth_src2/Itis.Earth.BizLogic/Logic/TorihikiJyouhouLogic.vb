
Imports Itis.Earth.DataAccess
Imports System.Transactions

Public Class TorihikiJyouhouLogic

    Private TorihikiJyouhouDA As New TorihikiJyouhouDataAccess()
    Private KakakuJyouhouDA As New KakakuseikyuJyouhouDataAccess()
    Private KakakuJyouhouBL As New KakakuseikyuJyouhouLogic()
    Private CommonHaita As New HaitaCheck()

    ''' <summary>
    ''' [������]�p�@�����X�}�X�^�A���� : ���i.������񕔕���Function�𗘗p
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <returns>�����X�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetKameitenData(ByVal KametenCd As String) As KameitenDataSet.m_kameitenTableDataTable

        Return KakakuJyouhouDA.SelKameiten(KametenCd)

    End Function

    ''' <summary>
    ''' [������]�p�@������}�X�^�A����
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <returns>����e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTorihikiData(ByVal KametenCd As String) As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable
        ' 
        Return TorihikiJyouhouDA.SelTorihiki(KametenCd)

    End Function

    ''' <summary>
    ''' [������]�p�@������}�X�^�A�d���`�F�b�N�A�V�K�܂��X�V�𔻒f����
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <returns>TRUE:����,FALSE:�s����</returns>
    ''' <remarks></remarks>
    Public Function GetJyufukuData(ByVal KametenCd As String) As Boolean
        ' 
        Return TorihikiJyouhouDA.SelJyufukuData(KametenCd)

    End Function

    ''' <summary>
    ''' ����o�^����
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <param name="gameiDate">��ʎ���</param>
    ''' <param name="dtTourihiki">����e�[�u��</param>
    ''' <param name="bFlg">�����t���O</param>
    ''' <returns>���b�Z�[�W</returns>
    ''' <remarks></remarks>
    Public Function TorihikiTouroku(ByVal KametenCd As String, ByRef gameiDate As DateTime, ByVal dtTourihiki As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable, ByVal bFlg As String) As String

        Dim strKekka As String
        Dim kino As String
        Dim rCnt As Int16

        kino = "������_{0}�̓o�^"

        If gameiDate <> CType("0:00:00", DateTime) Then

            '�r���`�F�b�N
            strKekka = CommonHaita.CheckHaita(KametenCd, "m_kameiten_torihiki_jouhou", gameiDate)

            If strKekka <> "" Then
                Return strKekka
            End If

        End If

        rCnt = GetTorihikiData(KametenCd).Rows.Count

        '�o�^����
        If InsUpdTorihiki(KametenCd, dtTourihiki, bFlg) Then

            If rCnt <> 0 Then
                gameiDate = GetTorihikiData(KametenCd).Rows(0).Item("upd_datetime")
            End If

            If bFlg = "gyoumu" Then
                kino = KakakuJyouhouBL.MakeMessage(kino, "�Ɩ�")
                Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG018S, kino)
            Else
                kino = KakakuJyouhouBL.MakeMessage(kino, "�o��")
                Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG018S, kino)
            End If
        Else
            If bFlg = "gyoumu" Then
                kino = KakakuJyouhouBL.MakeMessage(kino, "�Ɩ�")
                Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG019E, kino)
            Else
                kino = KakakuJyouhouBL.MakeMessage(kino, "�o��")
                Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG019E, kino)
            End If
        End If

    End Function

    ''' <summary>
    ''' [������]�p�@������i�Ɩ�,�o���j�A�V�K�o�^�܂��X�V
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <param name="dtTourihiki">����e�[�u��</param>
    ''' <param name="bFlg">�����t���O</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function InsUpdTorihiki(ByVal KametenCd As String, ByVal dtTourihiki As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable, ByVal bFlg As String) As Boolean

        Dim strKousinFlg As String
        If GetJyufukuData(KametenCd) Then
            strKousinFlg = "upd"
        Else
            strKousinFlg = "ins"
        End If

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            If bFlg = "gyoumu" Then
                If TorihikiJyouhouDA.InsUpdTorihikiGyoumu(dtTourihiki, strKousinFlg) Then
                    scope.Complete()
                    Return True
                Else
                    scope.Dispose()
                    Return False
                End If
            ElseIf bFlg = "keiri" Then
                If TorihikiJyouhouDA.InsUpdTorihikiKeiri(dtTourihiki, strKousinFlg) Then
                    scope.Complete()
                    Return True
                Else
                    scope.Dispose()
                    Return False
                End If
            Else
                scope.Dispose()
                Return False
            End If

        End Using

    End Function

    ''' <summary>
    ''' [������]�p�@�����X�}�X�^�A�X�V
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <param name="dtKameiten">�����X�e�[�u��</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function UpdKameiten(ByVal KametenCd As String, ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable) As String

        Dim kino As String

        kino = "������̓o�^"

        '�o�^����
        If UpdKameiten(dtKameiten) Then
            Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG018S, kino)
        Else
            Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG019E, kino)
        End If

    End Function

    ''' <summary>
    ''' �����X�}�X�^�X�V����
    ''' </summary>
    ''' <param name="dtKameiten">�����X�e�[�u��</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function UpdKameiten(ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable) As Boolean

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            If TorihikiJyouhouDA.UpdKameiten(dtKameiten) Then
                scope.Complete()
                Return True
            Else
                scope.Dispose()
                Return False
            End If
        End Using

    End Function

    ''' <summary>
    ''' [������]�p�@��ʃ��X�g���ڃf�[�^�擾
    ''' </summary>
    ''' <param name="dt">���X�g�p�e�[�u��</param>
    ''' <param name="type">�ތ^</param>
    ''' <param name="withSpaceRow">�󔒍s</param>
    ''' <param name="withCd">�R�[�h</param>
    ''' <remarks></remarks>
    Public Sub GetListData(ByRef dt As DataTable, ByVal type As String, ByVal withSpaceRow As Boolean, Optional ByVal withCd As Boolean = True)

        ' ���ʂ̃R���{�f�[�^�ݒ胁�\�b�h���g�p
        TorihikiJyouhouDA.GetDropdownData(dt, type, withSpaceRow, withCd)

    End Sub


    ''' <summary>
    ''' [������]�p�@��ʃ��X�g���ڃf�[�^�擾
    ''' </summary>
    ''' <param name="dt">���X�g�p�e�[�u��</param>
    ''' <param name="type">�ތ^</param>
    ''' <param name="withSpaceRow">�󔒍s</param>
    ''' <param name="withCd">�R�[�h</param>
    ''' <remarks></remarks>
    Public Sub GetListData6869(ByRef dt As DataTable, ByVal type As String, ByVal withSpaceRow As Boolean, Optional ByVal withCd As Boolean = True)

        ' ���ʂ̃R���{�f�[�^�ݒ胁�\�b�h���g�p
        TorihikiJyouhouDA.GetDropdownData6869(dt, type, withSpaceRow, withCd)

    End Sub

End Class
