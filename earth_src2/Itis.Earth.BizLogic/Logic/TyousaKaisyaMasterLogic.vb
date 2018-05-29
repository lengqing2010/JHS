Imports Itis.Earth.DataAccess
''' <summary>
''' ������Ѓ}�X�^
''' </summary>
''' <history>
''' <para>2010/05/15�@�n���R(��A)�@�V�K�쐬</para>
''' </history>
Public Class TyousaKaisyaMasterLogic

    '�C���X�^���X����
    Private TyousaKaisyaMasterDA As New TyousaKaisyaMasterDataAccess

    ''' <summary>
    ''' �g�����̃}�X�^
    ''' </summary>
    ''' <param name="strSyubetu">���̎��</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable
        Return TyousaKaisyaMasterDA.SelKakutyouInfo(strSyubetu)
    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^
    ''' </summary>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strJigyousyoCd">���Ə��R�[�h</param>
    Public Function SelMTyousaKaisyaInfo(ByVal strTyousaKaisya_Cd As String, _
                                         ByVal strTysKaisyaCd As String, _
                                         ByVal strJigyousyoCd As String, _
                                         ByVal btn As String) As TyousaKaisyaDataSet.m_tyousakaisyaDataTable

        Return TyousaKaisyaMasterDA.SelMTyousaKaisyaInfo(strTyousaKaisya_Cd, strTysKaisyaCd, strJigyousyoCd, btn)
    End Function

    ''' <summary>
    ''' �C���{�^����������
    ''' </summary>
    ''' <param name="dtTyousaKaisya">�X�V�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdTyousaKaisya(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable, ByVal strHenkou As String, ByVal strDisplayName As String, ByVal strTrue As String) As String
        '�r��
        Dim dtHaita As New DataTable
        '�d���`�F�b�N
        Dim blnJyuufukuS As Boolean = False
        Dim blnJyuufukuTH As Boolean = False

        '��������
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '������Ѓ}�X�^�l���݃`�F�b�N
                If TyousaKaisyaMasterDA.SelMTyousaKaisyaInfo("", dtTyousaKaisya(0).tys_kaisya_cd, dtTyousaKaisya(0).jigyousyo_cd, "btnSyuusei").Rows.Count = 0 Then
                    scope.Dispose()
                    Return "2"
                Else
                    ''�d���`�F�b�N_������}�X�^
                    'If TyousaKaisyaMasterDA.SelSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                    '    blnJyuufukuS = True
                    'End If

                    '�r���`�F�b�N
                    dtHaita = SelHaita(dtTyousaKaisya(0).tys_kaisya_cd, dtTyousaKaisya(0).jigyousyo_cd, dtTyousaKaisya(0).upd_datetime)
                    If dtHaita.Rows.Count <> 0 Then
                        scope.Dispose()
                        Return String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "������Ѓ}�X�^").ToString()
                    Else
                        If strTrue = "OK" Then
                            '�d���`�F�b�N_������}�X�^
                            If TyousaKaisyaMasterDA.SelSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                                blnJyuufukuS = True
                            End If

                            '������}�X�^�o�^����
                            If blnJyuufukuS = False Then
                                '�d���`�F�b�N_������o�^���`�}�X�^
                                If TyousaKaisyaMasterDA.SelSeikyuuSakiTH(dtTyousaKaisya(0).seikyuu_saki_brc).Rows.Count > 0 Then
                                    blnJyuufukuTH = True
                                End If

                                '�o�^����
                                TyousaKaisyaMasterDA.InsSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).upd_login_user_id, blnJyuufukuTH)
                            End If
                        End If

                        '������Ѓ}�X�^�e�[�u���̏C��
                        TyousaKaisyaMasterDA.UpdTyousaKaisya(dtTyousaKaisya, strHenkou, strDisplayName)

                        '==============2011/06/24 �ԗ� �y�A�g������Ѓ}�X�^�̓o�^�E�폜�����z�̒ǉ� �J�n��======================
                        '�A�g������Ѓ}�X�^�̓o�^�E�폜����
                        If Not SetRenkeiTyousakaisyaMaster(dtTyousaKaisya) Then
                            Throw New ApplicationException
                        End If
                        '==============2011/06/24 �ԗ� �y�A�g������Ѓ}�X�^�̓o�^�E�폜�����z�̒ǉ� �I����======================

                        scope.Complete()
                        Return "0"
                    End If
                End If
            Catch ex As Exception
                scope.Dispose()
                Return "1"
            End Try
        End Using

    End Function

    ''' <summary>
    ''' �r���`�F�b�N�p
    ''' </summary>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strJigyousyoCd">���Ə��R�[�h</param>
    ''' <param name="strKousinDate">�X�V����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strTysKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKousinDate As String) As DataTable
        '�߂�
        Return TyousaKaisyaMasterDA.SelHaita(strTysKaisyaCd, strJigyousyoCd, strKousinDate)
    End Function

    ''' <summary>
    ''' ������.�����{�^������������_������o�^���`�}�X�^�`�F�b�N
    ''' </summary>
    ''' <param name="strSeikyuuSakiBrc">������}��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiTouroku(ByVal strSeikyuuSakiBrc As String) As DataTable
        Return TyousaKaisyaMasterDA.SelSeikyuuSakiTouroku(strSeikyuuSakiBrc)
    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^�o�^
    ''' </summary>
    ''' <param name="dtTyousaKaisya">�o�^�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsTyousaKaisya(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable, ByVal strHenkou As String, ByVal strDisplayName As String, ByVal strTrue As String) As Boolean
        '������}�X�^�`�F�b�N�p
        Dim blnJyuufukuS As Boolean = False
        Dim blnJyuufukuTH As Boolean = False

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '�y������V�K�o�^�z���\������Ă���ꍇ
                If strTrue = "OK" Then
                    '�d���`�F�b�N_������}�X�^
                    If TyousaKaisyaMasterDA.SelSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                        blnJyuufukuS = True
                    End If

                    '������}�X�^�o�^����
                    If blnJyuufukuS = False Then
                        '�d���`�F�b�N_������o�^���`�}�X�^
                        If TyousaKaisyaMasterDA.SelSeikyuuSakiTH(dtTyousaKaisya(0).seikyuu_saki_brc).Rows.Count > 0 Then
                            blnJyuufukuTH = True
                        End If

                        '�o�^����
                        TyousaKaisyaMasterDA.InsSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).upd_login_user_id, blnJyuufukuTH)
                    End If
                End If

                '�o�^����
                TyousaKaisyaMasterDA.InsTyousaKaisya(dtTyousaKaisya, strHenkou, strDisplayName)

                '==============2011/06/24 �ԗ� �y�A�g������Ѓ}�X�^�̓o�^�E�폜�����z�̒ǉ� �J�n��======================
                '�A�g������Ѓ}�X�^�̓o�^�E�폜����
                If Not SetRenkeiTyousakaisyaMaster(dtTyousaKaisya) Then
                    Throw New ApplicationException
                End If
                '==============2011/06/24 �ԗ� �y�A�g������Ѓ}�X�^�̓o�^�E�폜�����z�̒ǉ� �I����======================

                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
    End Function

    '''' <summary>
    '''' ��������.FC�}�X�^
    '''' </summary>
    '''' <param name="strFCCd">���������Z���^�[�R�[�h</param>
    'Public Function SelMfcInfo(ByVal strFCCd As String) As DataTable
    '    Return TyousaKaisyaMasterDA.SelMfcInfo(strFCCd)
    'End Function

    ''' <summary>
    ''' �H���񍐏��������擾
    ''' </summary>
    ''' <param name="strUserId">���O�C�����[�U</param>
    Public Function SelKoujiInfo(ByVal strUserId As String) As DataTable
        Return TyousaKaisyaMasterDA.SelKoujiInfo(strUserId)
    End Function

    ''' <summary>
    ''' Mail���擾
    ''' </summary>
    ''' <param name="yuubin_no">�X��NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet
        Return TyousaKaisyaMasterDA.GetMailAddress(yuubin_no)
    End Function

    ''' <summary>
    ''' ������}�X�^���݃`�F�b�N
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd"></param>
    ''' <param name="strSeikyuuSakiBrc"></param>
    ''' <param name="strSeikyuuSakiKbn"></param>
    ''' <param name="strTrue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSaki(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String, Optional ByVal strTrue As Boolean = False) As DataTable
        Return TyousaKaisyaMasterDA.SelSeikyuuSaki(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strTrue)
    End Function

    ''' <summary>
    ''' FC�R�[�h���݃`�F�b�N
    ''' </summary>
    Public Function SelFCTenInfo(ByVal strFcCd As String) As DataTable
        Return TyousaKaisyaMasterDA.SelFCTenInfo(strFcCd)
    End Function

    ''' <summary>
    ''' �X�֔ԍ����݃`�F�b�N
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable
        Return TyousaKaisyaMasterDA.SelYuubinInfo(strBangou)
    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^
    ''' </summary>
    Public Function SelTyousaKaisya(ByVal strKaisyaCd As String, ByVal strJigyouCd As String, ByVal bloKbn As Boolean) As CommonSearchDataSet.tyousakaisyaTableDataTable
        Return TyousaKaisyaMasterDA.SelTyousaKaisya(strKaisyaCd, strJigyouCd, bloKbn)
    End Function

    ''' <summary>
    ''' FC�X�}�X�^
    ''' </summary>
    Public Function SelFCTen(ByVal strFCCd As String) As CommonSearchDataSet.KeiretuTableDataTable
        Return TyousaKaisyaMasterDA.SelFCTen(strFCCd)
    End Function

    ''' <summary>
    ''' �V��v�x����}�X�^
    ''' </summary>
    Public Function SelSKK(ByVal strJigyouCd As String, ByVal strShriCd As String) As DataTable
        Return TyousaKaisyaMasterDA.SelSKK(strJigyouCd, strShriCd)
    End Function

    ''' <summary>
    ''' ������}�X�^�r���[
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTable1DataTable
        Return TyousaKaisyaMasterDA.SelVSeikyuuSakiInfo(strKbn, strCd, strBrc, blnDelete)
    End Function

    ''' <summary>
    ''' �c�Ə��}�X�^
    ''' </summary>
    Public Function SelEigyousyo(ByVal strFCCd As String) As CommonSearchDataSet.EigyousyoTableDataTable
        Return TyousaKaisyaMasterDA.SelEigyousyo(strFCCd)
    End Function

    '==============2011/06/24 �ԗ� �y�A�g������Ѓ}�X�^�̓o�^�E�폜�����z�̒ǉ� �J�n��======================
    ''' <summary>
    ''' �A�g������Ѓ}�X�^�̓o�^�E�폜����
    ''' </summary>
    Private Function SetRenkeiTyousakaisyaMaster(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable) As Boolean
        '������ЃR�[�h
        Dim strTyousakaisyaCd As String = dtTyousaKaisya.Item(0).tys_kaisya_cd.Trim
        '���Ə��R�[�h
        Dim strJigyousyoCd As String = dtTyousaKaisya.Item(0).jigyousyo_cd.Trim
        '�q�|�i�g�r�g�[�N��
        Dim strRJhsTokenFlg As String = dtTyousaKaisya.Item(0).report_jhs_token_flg.Trim
        '���[�U�[ID
        Dim strUserId As String = dtTyousaKaisya.Item(0).add_login_user_id.Trim

        '�A�g������Ѓ}�X�^����������iKEY�F������ЃR�[�h�A���Ə��R�[�h�j
        Dim dtRenkeiTyousakaisyaMaster As New Data.DataTable
        dtRenkeiTyousakaisyaMaster = TyousaKaisyaMasterDA.SelRenkeiTyousakaisyaMaster(strTyousakaisyaCd, strJigyousyoCd)
        '�Y���f�[�^������̎�
        If dtRenkeiTyousakaisyaMaster.Rows.Count > 0 Then
            '���.�q�|�i�g�r�g�[�N�����h�����h�̎�
            If strRJhsTokenFlg.Equals("0") Then
                '�Y���̘A�g������Ѓ}�X�^�̃��R�[�h���폜����
                If Not TyousaKaisyaMasterDA.DelRenkeiTyousakaisyaMaster(strTyousakaisyaCd, strJigyousyoCd) Then
                    Return False
                End If
            End If
        Else
            '�Y���f�[�^���Ȃ��̎�
            '���.�q�|�i�g�r�g�[�N�����h�L��h�̎�
            If strRJhsTokenFlg.Equals("1") Then
                '�A�g������Ѓ}�X�^�Ƀ��R�[�h��ǉ�����
                If Not TyousaKaisyaMasterDA.InsRenkeiTyousakaisyaMaster(strTyousakaisyaCd, strJigyousyoCd, strUserId) Then
                    Return False
                End If
            End If
        End If

        Return True
    End Function
    '==============2011/06/24 �ԗ� �y�A�g������Ѓ}�X�^�̓o�^�E�폜�����z�̒ǉ� �I����======================

End Class
