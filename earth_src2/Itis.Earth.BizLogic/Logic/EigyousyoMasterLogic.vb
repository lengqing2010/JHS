Imports System.Transactions
Imports Itis.Earth.DataAccess
''' <summary>
''' ������Ѓ}�X�^
''' </summary>
''' <history>
''' <para>2010/05/15�@�n���R(��A)�@�V�K�쐬</para>
''' </history>
Public Class EigyousyoMasterLogic
    '�C���X�^���X����
    Private EigyousyoMasterDA As New EigyousyoMasterDataAccess

    ''' <summary>
    ''' �g�����̃}�X�^
    ''' </summary>
    ''' <param name="strSyubetu">���̎��</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable
        Return EigyousyoMasterDA.SelKakutyouInfo(strSyubetu)
    End Function

    ''' <summary>
    ''' �c�Ə��}�X�^
    ''' </summary>
    ''' <param name="strEigyousyo_Cd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    Public Function SelEigyousyoInfo(ByVal strEigyousyo_Cd As String, _
                                         ByVal strEigyousyoCd As String, _
                                         ByVal btn As String) As EigyousyoDataSet.m_eigyousyoDataTable
        Return EigyousyoMasterDA.SelEigyousyoInfo(strEigyousyo_Cd, strEigyousyoCd, btn)
    End Function

    ''' <summary>
    ''' Mail���擾
    ''' </summary>
    ''' <param name="yuubin_no">�X��NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet
        Return EigyousyoMasterDA.GetMailAddress(yuubin_no)
    End Function

    ''' <summary>
    ''' �X�֔ԍ����݃`�F�b�N
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable
        Return EigyousyoMasterDA.SelYuubinInfo(strBangou)
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
        Return EigyousyoMasterDA.SelSeikyuuSaki(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strTrue)
    End Function

    ''' <summary>
    ''' �C���{�^����������
    ''' </summary>
    ''' <param name="dtEigyousyo">�X�V�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdEigyousyo(ByVal dtEigyousyo As EigyousyoDataSet.m_eigyousyoDataTable, ByVal strTrue As String) As String
        '�r��
        Dim dtHaita As New DataTable
        '�d���`�F�b�N
        Dim blnJyuufukuS As Boolean = False
        Dim blnJyuufukuTH As Boolean = False

        '��������
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '�c�Ə��}�X�^�l���݃`�F�b�N
                If EigyousyoMasterDA.SelEigyousyoInfo("", dtEigyousyo(0).eigyousyo_cd, "btnSyuusei").Rows.Count = 0 Then
                    scope.Dispose()
                    Return "2"
                Else
                    '�r���`�F�b�N
                    dtHaita = SelHaita(dtEigyousyo(0).eigyousyo_cd, dtEigyousyo(0).upd_datetime)
                    If dtHaita.Rows.Count <> 0 Then
                        scope.Dispose()
                        Return String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "�c�Ə��}�X�^").ToString()
                    Else
                        '20100712�@�d�l�ύX�@�폜�@�n���R������
                        'If strTrue = "OK" Then
                        '    '�d���`�F�b�N_������}�X�^
                        '    If EigyousyoMasterDA.SelSeikyuuSaki(dtEigyousyo(0).seikyuu_saki_cd, dtEigyousyo(0).seikyuu_saki_brc, dtEigyousyo(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                        '        blnJyuufukuS = True
                        '    End If

                        '    '������}�X�^�o�^����
                        '    If blnJyuufukuS = False Then
                        '        '�d���`�F�b�N_������o�^���`�}�X�^
                        '        If EigyousyoMasterDA.SelSeikyuuSakiTH(dtEigyousyo(0).seikyuu_saki_brc).Rows.Count > 0 Then
                        '            blnJyuufukuTH = True
                        '        End If

                        '        '�o�^����
                        '        EigyousyoMasterDA.InsSeikyuuSaki(dtEigyousyo(0).seikyuu_saki_cd, dtEigyousyo(0).seikyuu_saki_brc, dtEigyousyo(0).upd_login_user_id, blnJyuufukuTH)
                        '    End If
                        'End If
                        '20100712�@�d�l�ύX�@�폜�@�n���R������

                        '�c�Ə��}�X�^�e�[�u���̏C��
                        EigyousyoMasterDA.UpdEigyousyo(dtEigyousyo)
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
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strKousinDate">�X�V����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strEigyousyoCd As String, ByVal strKousinDate As String) As DataTable
        '�߂�
        Return EigyousyoMasterDA.SelHaita(strEigyousyoCd, strKousinDate)
    End Function

    ''' <summary>
    ''' �c�Ə��}�X�^�o�^
    ''' </summary>
    ''' <param name="dtEigyousyo">�o�^�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsEigyousyo(ByVal dtEigyousyo As EigyousyoDataSet.m_eigyousyoDataTable, ByVal strTrue As String) As Boolean
        '������}�X�^�`�F�b�N�p
        Dim blnJyuufukuS As Boolean = False
        Dim blnJyuufukuTH As Boolean = False

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '20100712�@�d�l�ύX�@�폜�@�n���R������
                ''�y������V�K�o�^�z���\������Ă���ꍇ
                'If strTrue = "OK" Then
                '    '�d���`�F�b�N_������}�X�^
                '    If EigyousyoMasterDA.SelSeikyuuSaki(dtEigyousyo(0).seikyuu_saki_cd, dtEigyousyo(0).seikyuu_saki_brc, dtEigyousyo(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                '        blnJyuufukuS = True
                '    End If

                '    '������}�X�^�o�^����
                '    If blnJyuufukuS = False Then
                '        '�d���`�F�b�N_������o�^���`�}�X�^
                '        If EigyousyoMasterDA.SelSeikyuuSakiTH(dtEigyousyo(0).seikyuu_saki_brc).Rows.Count > 0 Then
                '            blnJyuufukuTH = True
                '        End If

                '        '�o�^����
                '        EigyousyoMasterDA.InsSeikyuuSaki(dtEigyousyo(0).seikyuu_saki_cd, dtEigyousyo(0).seikyuu_saki_brc, dtEigyousyo(0).upd_login_user_id, blnJyuufukuTH)
                '    End If
                'End If
                '20100712�@�d�l�ύX�@�폜�@�n���R������

                '�o�^����
                EigyousyoMasterDA.InsEigyousyo(dtEigyousyo)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
    End Function

    ''' <summary>
    ''' ������}�X�^�r���[
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTable1DataTable
        Return EigyousyoMasterDA.SelVSeikyuuSakiInfo(strKbn, strCd, strBrc, blnDelete)
    End Function

    ''' <summary>
    ''' ������.�����{�^������������_������o�^���`�}�X�^�`�F�b�N
    ''' </summary>
    ''' <param name="strSeikyuuSakiBrc">������}��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiTouroku(ByVal strSeikyuuSakiBrc As String) As DataTable
        Return EigyousyoMasterDA.SelSeikyuuSakiTouroku(strSeikyuuSakiBrc)
    End Function

    ''' <summary>
    ''' �����X�}�X�^���擾����B
    ''' </summary>
    Public Function SelKameiten(ByVal strEigyousyoCd As String) As DataTable
        Return EigyousyoMasterDA.SelKameiten(strEigyousyoCd)
    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^
    ''' </summary>
    Public Function SelKameitenJyuusyo(ByVal strKameitenCd As String, Optional ByVal strFlg As String = "") As DataTable
        Return EigyousyoMasterDA.SelKameitenJyuusyo(strKameitenCd, strFlg)
    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^�X�V�ƒǉ����e�擾
    ''' </summary>
    Public Function SelNaiyou(ByVal strKameitenCd As String) As DataTable
        Return EigyousyoMasterDA.SelNaiyou(strKameitenCd)
    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^�X�V�ǉ�����
    ''' </summary>
    ''' <param name="strEigyousyoCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetKousinTuika(ByVal strEigyousyoCd As String, ByVal strUserId As String) As Boolean
        Dim strKameitenCd As String = ""
        Dim dtNaiyou As New Data.DataTable
        Dim dtKameiten As New Data.DataTable
        Dim dtKameitenJyuusyo As New Data.DataTable
        Dim strFlg As String = ""
        '�����X�Z���}�X�^�A�g�Ǘ��e�[�u��
        Dim dtKameitenJyuusyoRenkei As New Data.DataTable

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '�����X�}�X�^���擾����
                dtKameiten = SelKameiten(strEigyousyoCd)
                For i As Integer = 0 To dtKameiten.Rows.Count - 1
                    '�����X�R�[�h�擾
                    strKameitenCd = Trim(dtKameiten.Rows(i).Item("kameiten_cd").ToString)
                    '�X�V�ǉ����e�擾
                    dtNaiyou = SelNaiyou(strKameitenCd)
                    '�����X�Z���}�X�^�̑��݃`�F�b�N
                    dtKameitenJyuusyo = SelKameitenJyuusyo(strKameitenCd)

                    '20101108 �n���R�@�����X�Z���}�X�^�A�g�Ǘ��e�[�u���͒ǉ��E�X�V����K�v������܂��@������
                    '�����X�Z���}�X�^�A�g�Ǘ��e�[�u���̑��݃`�F�b�N
                    dtKameitenJyuusyoRenkei = GetKameitenJyuusyoRenkei(strKameitenCd)
                    If dtKameitenJyuusyoRenkei.Rows.Count > 0 Then
                        '�}�X�^�̏������Ƃɉ����X�Z���}�X�^�A�g�Ǘ��e�[�u�����X�V����B
                        EigyousyoMasterDA.UpdKameitenJyuusyoRenkei(strKameitenCd, strUserId)
                    Else
                        '�����X�Z���}�X�^�A�g�Ǘ��e�[�u���ǉ�
                        EigyousyoMasterDA.InsKameitenJyuusyoRenkei(strKameitenCd, strUserId)
                    End If
                    '20101108 �n���R�@�����X�Z���}�X�^�A�g�Ǘ��e�[�u���͒ǉ��E�X�V����K�v������܂��@������

                    If dtKameitenJyuusyo.Rows.Count > 0 Then
                        '�}�X�^�̏������Ƃɉ����X�Z���}�X�^���X�V����B
                        EigyousyoMasterDA.UpdKameitenJyuusyo(strKameitenCd, dtNaiyou, strUserId)
                    Else
                        '�����X�Z���}�X�^�����݂��Ȃ��ꍇ
                        '�t���O�擾
                        If SelKameitenJyuusyo(strKameitenCd, "1").Rows.Count > 0 Then
                            strFlg = "0"
                        Else
                            strFlg = "-1"
                        End If

                        '�����X�Z���}�X�^�ǉ�
                        EigyousyoMasterDA.InsKameitenJyuusyo(strKameitenCd, dtNaiyou, strFlg, strUserId)
                    End If
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
    ''' �����X�Z���}�X�^�A�g�Ǘ��e�[�u��
    ''' </summary>
    ''' <history>20101108 �n���R �����X�Z���}�X�^�A�g�Ǘ��e�[�u�����ǉ��E�X�V����K�v������܂��B</history>
    Public Function GetKameitenJyuusyoRenkei(ByVal strKameitenCd As String) As DataTable
        Return EigyousyoMasterDA.SelKameitenJyuusyoRenkei(strKameitenCd)
    End Function

    ''' <summary>
    ''' CSV�f�[�^���擾����
    ''' </summary>
    ''' <returns>CSV�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function GetEigyousyoCsv() As Data.DataTable

        '�߂�l
        Return EigyousyoMasterDA.SelEigyousyoCsv()

    End Function

    ''' <summary>
    ''' �V�X�e�����t���擾����
    ''' </summary>
    ''' <returns>CSV�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function GetSystemDateYMD() As Data.DataTable

        '�߂�l
        Return EigyousyoMasterDA.SelSystemDateYMD()

    End Function

    ''' <summary>
    ''' ddl�̃f�[�^���擾����
    ''' </summary>
    ''' <returns>ddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function GetDdlList(ByVal strMeisyouSyubetu As Integer) As Data.DataTable

        '�߂�l
        Return EigyousyoMasterDA.SelDdlList(strMeisyouSyubetu)

    End Function

    ''' <summary>
    ''' ������Џ�񌏐����擾����
    ''' </summary>
    ''' <returns>ddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function GetTyousaKaisyaCount(ByVal strTyousaKaisyaCd As String) As Integer

        '�߂�l
        Return CInt(EigyousyoMasterDA.SelTyousaKaisyaCount(strTyousaKaisyaCd).Rows(0).Item(0))

    End Function

    ''' <summary>
    ''' ������Џ����擾����
    ''' </summary>
    ''' <returns>ddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function GetTyousaKaisyaInfo(ByVal strTyousaKaisyaCd As String) As Data.DataTable

        '�߂�l
        Return EigyousyoMasterDA.SelTyousaKaisyaInfo(strTyousaKaisyaCd)
    End Function

    ''' <summary>
    ''' �Œ�`���[�W�̓��͓����擾����
    ''' </summary>
    ''' <returns>ddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function GetKoteiTyaaji(ByVal strEigyousyoCd As String, ByVal blnThisMonth As Boolean) As Data.DataTable

        '�߂�l
        Return EigyousyoMasterDA.SelKoteiTyaaji(strEigyousyoCd, blnThisMonth)

    End Function

    ''' <summary>
    ''' �Œ�`���[�W�����Z�b�g����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetKoteiTyaaji(ByVal strEigyousyoCd As String, ByVal strUserId As String) As String

        Dim strMessage As String = String.Empty

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)

            Try

                '�u�X�ʐ����e�[�u���v��o�^����
                If Not EigyousyoMasterDA.InsTenbetuSeikyuu(strEigyousyoCd, strUserId) Then
                    strMessage = String.Format(Messages.Instance.MSG2070E, "�Œ�`���[�W")
                    Throw New ApplicationException
                End If

                '�X�ʐ����e�[�u���A�g�Ǘ��e�[�u���Ō�������
                Dim dtTenbetuSeikyuuRenkei As New Data.DataTable
                dtTenbetuSeikyuuRenkei = EigyousyoMasterDA.SelTenbetuSeikyuuRenkeiCount(strEigyousyoCd)

                If CInt(dtTenbetuSeikyuuRenkei.Rows(0).Item(0).ToString.Trim) > 0 Then
                    '���݂���A�X�V���s��
                    If Not EigyousyoMasterDA.UpdTenbetuSeikyuuRenkei(strEigyousyoCd, strUserId) Then
                        strMessage = Messages.Instance.MSG2071E
                        Throw New ApplicationException
                    End If
                Else
                    '���݂��Ȃ��A�}�����s��
                    If Not EigyousyoMasterDA.InsTenbetuSeikyuuRenkei(strEigyousyoCd, strUserId) Then
                        strMessage = Messages.Instance.MSG2071E
                        Throw New ApplicationException
                    End If
                End If

                scope.Complete()

            Catch ex As Exception
                scope.Dispose()
                If strMessage.Trim.Equals(String.Empty) Then
                    strMessage = String.Format(Messages.Instance.MSG2070E, "�Œ�`���[�W")
                End If
            End Try

        End Using

        Return strMessage

    End Function

    ''' <summary>
    ''' ������̑��݃`�F�b�N����
    ''' </summary>
    ''' <returns>������̌���</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelSeikyuusakiCheck(ByVal strEigyousyoCd As String) As Boolean

        Dim dtSeikyuusaki As New Data.DataTable
        dtSeikyuusaki = EigyousyoMasterDA.SelSeikyuusakiCheck(strEigyousyoCd)

        If CInt(dtSeikyuusaki.Rows(0).Item(0).ToString.Trim)>0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
