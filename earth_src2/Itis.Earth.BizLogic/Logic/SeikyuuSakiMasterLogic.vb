Imports Itis.Earth.DataAccess
''' <summary>
''' ������}�X�^
''' </summary>
''' <history>
''' <para>2010/05/24�@�n���R(��A)�@�V�K�쐬</para>
''' </history>
Public Class SeikyuuSakiMasterLogic
    '�C���X�^���X����
    Private SeikyuuSakiMasterDA As New SeikyuuSakiMasterDataAccess

    ''' <summary>
    ''' ������o�^���`�}�X�^
    ''' </summary>
    Public Function SelSeikyuuSakiTourokuHinagataInfo() As DataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiTourokuHinagataInfo()
    End Function

    ''' <summary>
    ''' �g�����̃}�X�^
    ''' </summary>
    ''' <param name="strSyubetu">���̎��</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable
        Return SeikyuuSakiMasterDA.SelKakutyouInfo(strSyubetu)
    End Function

    ''' <summary>
    ''' ��������̎擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiInfo(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String) As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiInfo(strSeikyuuSakiCd, strSeikyuuSakiBrc, SeikyuuSakiKbn)
    End Function

    ''' <summary>
    ''' ������o�^���`�}�X�^���̎擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiHinagataInfo(ByVal strSeikyuuSakiBrc As String) As SeikyuuSakiHinagataDataSet.m_seikyuu_saki_touroku_hinagataDataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiHinagataInfo(strSeikyuuSakiBrc)
    End Function

    ''' <summary>
    ''' ������}�X�^�o�^
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">�o�^�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '�o�^����
                SeikyuuSakiMasterDA.InsSeikyuuSaki(dtSeikyuuSaki)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
    End Function

    ''' <summary>
    ''' �r���`�F�b�N�p
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">������R�[�h</param>
    ''' <param name="strSeikyuuSakiBrc">������R�[�h</param>
    ''' <param name="strKousinDate">�X�V����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String, ByVal strKousinDate As String) As DataTable
        '�߂�
        Return SeikyuuSakiMasterDA.SelHaita(strSeikyuuSakiCd, strSeikyuuSakiBrc, SeikyuuSakiKbn, strKousinDate)
    End Function

    ''' <summary>
    ''' ������}�X�^�C��
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">�o�^�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As String
        '�r��
        Dim dtHaita As New DataTable

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '�l���݃`�F�b�N
                If SeikyuuSakiMasterDA.SelSeikyuuSakiInfo(dtSeikyuuSaki(0).seikyuu_saki_cd, dtSeikyuuSaki(0).seikyuu_saki_brc, dtSeikyuuSaki(0).seikyuu_saki_kbn).Rows.Count = 0 Then
                    scope.Dispose()
                    Return "2"
                Else
                    '�r���`�F�b�N
                    dtHaita = SelHaita(dtSeikyuuSaki(0).seikyuu_saki_cd, dtSeikyuuSaki(0).seikyuu_saki_brc, dtSeikyuuSaki(0).seikyuu_saki_kbn, dtSeikyuuSaki(0).upd_datetime)
                    If dtHaita.Rows.Count <> 0 Then
                        scope.Dispose()
                        Return String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "������}�X�^").ToString()
                    Else
                        '�C������
                        SeikyuuSakiMasterDA.UpdSeikyuuSaki(dtSeikyuuSaki)
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
    ''' �V��v���Ə��}�X�^
    ''' </summary>
    Public Function SelSinkaikeiJigyousyoInfo(ByVal strSkkJigyousyoCd As String) As DataTable
        Return SeikyuuSakiMasterDA.SelSinkaikeiJigyousyoInfo(strSkkJigyousyoCd)
    End Function

    ''' <summary>
    ''' �����R�[�h�i�^�M�Ǘ��}�X�^�j
    ''' </summary>
    ''' <history>20100925�@�����R�[�h�A����於�@�ǉ��@�n���R</history>
    Public Function SelNayoseSakiInfo(ByVal strNayoseSakiCd As String) As DataTable
        Return SeikyuuSakiMasterDA.SelNayoseSakiInfo(strNayoseSakiCd)
    End Function

    ''' <summary>
    ''' ������}�X�^�r���[
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTableDataTable
        Return SeikyuuSakiMasterDA.SelVSeikyuuSakiInfo(strKbn, strCd, strBrc, blnDelete)
    End Function

    ''' <summary>
    ''' ���̑��}�X�^���݃`�F�b�N
    ''' </summary>
    Public Function SelSonzaiChk(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String) As DataTable
        Return SeikyuuSakiMasterDA.SelSonzaiChk(strKbn, strCd, strBrc)
    End Function

    ''' <summary>
    ''' Mail���擾
    ''' </summary>
    ''' <param name="yuubin_no">�X��NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet
        Return SeikyuuSakiMasterDA.GetMailAddress(yuubin_no)
    End Function

    ''' <summary>
    ''' �X�֔ԍ����݃`�F�b�N
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable
        Return SeikyuuSakiMasterDA.SelYuubinInfo(strBangou)
    End Function

    ''' <summary>
    ''' ���U�n�j�t���O���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function GetKutiburiOkFlg() As Data.DataTable

        '�߂�l
        Return SeikyuuSakiMasterDA.SelKutiburiOkFlg()

    End Function

    ''' <summary>
    ''' ��s�x�X�R�[�h���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/05/29 �k�o �������̋�s�x�X�R�[�h�����ɔ���Earth���C</history>
    Public Function GetGinkouSitenCd() As Data.DataTable

        '�߂�l
        Return SeikyuuSakiMasterDA.SelGinkouSitenCd()

    End Function

    ''' <summary>
    ''' Max���U�n�j�t���O���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function GetMaxKutiburiOkFlg() As String

        Dim strMaxKutiburiOkFlg As String = String.Empty

        Dim dtMaxKutiburiOkFlg As New Data.DataTable
        dtMaxKutiburiOkFlg = SeikyuuSakiMasterDA.SelMaxKutiburiOkFlg()

        If dtMaxKutiburiOkFlg.Rows.Count > 0 Then

            strMaxKutiburiOkFlg = dtMaxKutiburiOkFlg.Rows(0).Item("tougou_tokuisaki_cd_max").ToString.Trim

            If (strMaxKutiburiOkFlg.Equals(String.Empty)) OrElse (Not IsNumeric(strMaxKutiburiOkFlg)) Then
                strMaxKutiburiOkFlg = "1"
            Else
                strMaxKutiburiOkFlg = Convert.ToString(Convert.ToDouble(strMaxKutiburiOkFlg) + 1)
            End If
        Else
            strMaxKutiburiOkFlg = "1"
        End If


        '�߂�l
        Return strMaxKutiburiOkFlg

    End Function
End Class
