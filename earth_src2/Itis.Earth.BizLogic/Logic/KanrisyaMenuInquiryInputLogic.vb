Imports Itis.Earth.DataAccess
Imports System.Transactions

''' <summary>���[�U�[�Ǘ������Ɖ�o�^����</summary>
''' <remarks>���[�U�[�Ǘ������Ɖ�o�^�@�\��񋟂���</remarks>
''' <history>
''' <para>2009/07/17�@����N(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KanrisyaMenuInquiryInputLogic

    ''' <summary> ���[�U�[�Ǘ��Ɖ�o�^�N���X�̃C���X�^���X���� </summary>
    Private KanrisyaMenuInquiryInputDataAccess As New KanrisyaMenuInquiryInputDataAccess

    ''' <summary>
    ''' �Ɩ��敪���擾����B
    ''' </summary>
    ''' <returns>�Ɩ��敪�e�[�u��</returns>
    Public Function GetGyoumuKubunInfo() As KanrisyaMenuInquiryInputDataSet.gyoumuKubunDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selGyoumuKubun
    End Function

    ''' <summary>
    ''' ���O�C�����[�U�[�̎Q�ƌ����Ǘ���FLG���擾����B
    ''' </summary>
    ''' <param name="strUserId">���O�C�����[�U�[ID</param>
    ''' <returns>�Q�ƌ����Ǘ���FLG�f�[�^�e�[�u��</returns>
    Public Function GetUserKengenKanriFlg(ByVal strUserId As String) As KanrisyaMenuInquiryInputDataSet.userKengenKanriFlgDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selUserKengenKanriFlg(strUserId)
    End Function

    ''' <summary>
    ''' ���������ύX�����擾����B
    ''' </summary>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <returns>���������ύX���e�[�u��</returns>
    Public Function GetSyozokuHenkouDateInfo(ByVal strUserId As String) As KanrisyaMenuInquiryInputDataSet.syozokuHenkouDateDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selSyozokuHenkouDate(strUserId)
    End Function

    ''' <summary>
    ''' ���[�U�[�̌��������擾����B
    ''' </summary>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strSyozokuHenkouDate">���[�U�[�̏��������ύX��</param>
    ''' <returns>���[�U�[�̌������e�[�u��</returns>
    Public Function GetUserInfo(ByVal strUserId As String, ByVal strSyozokuHenkouDate As String) As KanrisyaMenuInquiryInputDataSet.kanrisyaJyouhouDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selUserInfo(strUserId, strSyozokuHenkouDate)
    End Function

    ''' <summary>
    ''' �ǉ��Q�ƕ����R�[�h�ɂ���Ď擾�����g�D���x��
    ''' </summary>
    ''' <param name="strBusyoCd">�ǉ��Q�ƕ����R�[�h</param>
    ''' <returns>�g�D���x���f�[�^�e�[�u��</returns>
    Public Function GetLevelInfo(ByVal strBusyoCd As String) As KanrisyaMenuInquiryInputDataSet.sansyouLevelDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selLevel(strBusyoCd)
    End Function

    ''' <summary>
    ''' �n�ՔF�؃}�X�^�ƕ����Ǘ��}�X�^���X�V����B
    ''' </summary>
    ''' <param name="dtUPDData">�X�V���ڃe�[�u��</param>
    ''' <returns>������</returns>
    Public Function SetUpdJibanNinsyou(ByVal account_no As String, ByVal dtUPDData As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable, ByVal strHaita As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable
            Dim dtReturnNinsyouRenkei As DataTable

            '�o�^�������[�U�[���n�ՔF�؃}�X�^�ɑ��݃`�F�b�N
            dtReturn = KanrisyaMenuInquiryInputDataAccess.selJibanNinsyouHaita(dtUPDData.Rows(0).Item("user_id").ToString)

            '�o�^�������[�U�[���n�ՔF�؃}�X�^�A�g�Ǘ��e�[�u���ɑ��݃`�F�b�N
            dtReturnNinsyouRenkei = KanrisyaMenuInquiryInputDataAccess.selJibanNinsyouRenkeiHaita(dtUPDData.Rows(0).Item("user_id").ToString)

            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strHaita <> "" Then
                    If strHaita < dtReturn.Rows(0).Item("upd_datetime") Then
                        scope.Dispose()
                        Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                    End If
                End If
                If dtReturnNinsyouRenkei.Rows.Count = 0 Then
                    '�n�ՔF�؃}�X�^�A�g�Ǘ��e�[�u����o�^����B
                    If KanrisyaMenuInquiryInputDataAccess.InsJibanNinsyouRenkei(dtUPDData) = True Then
                        '�n�ՔF�؃}�X�^���X�V����B
                        If KanrisyaMenuInquiryInputDataAccess.UpdJibanNinsyou(account_no, dtUPDData) = True Then
                            scope.Complete()
                            Return "1"
                        Else
                            scope.Dispose()
                            Return "H"
                        End If
                    Else
                        scope.Dispose()
                        Return "H"
                    End If
                Else
                    '�n�ՔF�؃}�X�^�A�g�Ǘ��e�[�u�����X�V����B
                    If KanrisyaMenuInquiryInputDataAccess.UpdJibanNinsyouRenkei(dtUPDData) = True Then
                        '�n�ՔF�؃}�X�^���X�V����B
                        If KanrisyaMenuInquiryInputDataAccess.UpdJibanNinsyou(account_no, dtUPDData) = True Then
                            scope.Complete()
                            Return "1"
                        Else
                            scope.Dispose()
                            Return "H"
                        End If
                    Else
                        scope.Dispose()
                        Return "H"
                    End If
                End If
            End If

        End Using

    End Function

End Class
