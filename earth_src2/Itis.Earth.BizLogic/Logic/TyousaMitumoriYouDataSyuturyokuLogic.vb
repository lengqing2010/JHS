Imports Itis.Earth.DataAccess
Imports System.Transactions

Public Class TyousaMitumoriYouDataSyuturyokuLogic

    '�C���X�^���X����
    Private TyousaMitumoriYouDataSyuturyokuDA As New TyousaMitumoriYouDataSyuturyokuDataAccess

    Private bolFlg As Boolean = False

    ''' <summary>
    ''' �������σf�[�^�o��
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoNo1">�ۏ؏�NO1</param>
    ''' <param name="strHosyousyoNo2">�ۏ؏�NO2</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strMitumoriFlg">���Ϗ��쐬�t���O</param>
    ''' <history>20100925�@�n���R</history>
    Public Function GetTyousaMitumoriInfo(ByVal strKubun As String, _
                                          ByVal strHosyousyoNo1 As String, _
                                          ByVal strHosyousyoNo2 As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal strMitumoriFlg As String, _
                                          ByVal strKoFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As DataTable
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.SelTyousaMitumoriInfo(strKubun, strHosyousyoNo1, strHosyousyoNo2, strKameitenCd, strMitumoriFlg, strKoFlg, strSesyuMei, strKeiretuCd, strTS)

    End Function

    ''' <summary>
    ''' �������σf�[�^�o�͂̑�����
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoNo1">�ۏ؏�NO1</param>
    ''' <param name="strHosyousyoNo2">�ۏ؏�NO2</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strMitumoriFlg">���Ϗ��쐬�t���O</param>
    ''' <history>20100925�@�n���R</history>
    Public Function GetTyousaMitumoriCount(ByVal strKubun As String, _
                                          ByVal strHosyousyoNo1 As String, _
                                          ByVal strHosyousyoNo2 As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal strMitumoriFlg As String, ByVal strKoFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As Int64
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.SelTyousaMitumoriCount(strKubun, strHosyousyoNo1, strHosyousyoNo2, strKameitenCd, strMitumoriFlg, strKoFlg, strSesyuMei, strKeiretuCd, strTS)

    End Function

    ''' <summary>
    ''' CSV�f�[�^�i�ʂ̏ꍇ�j
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strMitumoriFlg">���Ϗ��쐬�t���O</param>
    ''' <history>20100925�@�n���R</history>
    Public Function GetCsvDataKobetu(ByVal strKubun As String, _
                                          ByVal strHosyousyoNo As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal strMitumoriFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As DataTable
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.SelCsvDataKobetu(strKubun, strHosyousyoNo, strKameitenCd, strMitumoriFlg, strSesyuMei, strKeiretuCd, strTS)
    End Function

    ''' <summary>
    ''' CSV�f�[�^�i�A���̏ꍇ�j
    ''' </summary>
    ''' <param name="strKubun_HosyousyoNo">�敪_�ۏ؏�NO</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strMitumoriFlg">���Ϗ��쐬�t���O</param>
    ''' <history>20100925�@�n���R</history>
    Public Function GetCsvDataRentou(ByVal strKubun_HosyousyoNo As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal intCount As Integer, _
                                          ByVal strMitumoriFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As DataTable
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.SelCsvDataRentou(strKubun_HosyousyoNo, strKameitenCd, intCount, strMitumoriFlg, strSesyuMei, strKeiretuCd, strTS)
    End Function

    ''' <summary>
    ''' ��������\�ɁA�f�[�^�����݃`�F�b�N
    ''' </summary>
    ''' <param name="drRow">��������</param>
    ''' <history>20100925�@�n���R</history>
    Public Function GetBukkenRirekiChk(ByVal drRow As DataRow) As DataTable
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.SelBukkenRirekiChk(drRow)
    End Function

    ''' <summary>
    ''' ��������\�ɁA����NO�̎擾
    ''' </summary>
    ''' <param name="drRow">��������</param>
    ''' <history>20100925�@�n���R</history>
    Public Function GetBukkenRirekiNo(ByVal drRow As DataRow) As DataTable
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.SelBukkenRirekiNo(drRow)
    End Function

    ''' <summary>
    ''' �f�[�^�����݂��Ȃ����A�V�K�f�[�^��o�^����
    ''' </summary>
    ''' <param name="drRow">�V�K�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsBukkenRireki(ByVal drRow As DataRow, Optional ByVal bloSonzaiFlg As Boolean = True) As Boolean
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.InsBukkenRireki(drRow, bloSonzaiFlg)
    End Function

    ''' <summary>
    ''' �f�[�^�����݂��鎞�A�f�[�^���X�V����
    ''' </summary>
    ''' <param name="drRow">��������</param>
    ''' <history>20100925�@�n���R</history>
    Public Function UpdBukkenRireki(ByVal drRow As DataRow) As Boolean
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.UpdBukkenRireki(drRow)
    End Function

    ''' <summary>
    ''' �J�[�\���ړ����A�����X���擾
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <history>20100925�@�n���R</history>
    Public Function GetKameitenMei(ByVal strKameitenCd As String) As DataTable
        '�f�[�^�擾
        Return TyousaMitumoriYouDataSyuturyokuDA.SelKameitenMei(strKameitenCd)
    End Function

    ''' <summary>
    ''' �J�[�\���ړ����A�����X���擾
    ''' </summary>
    ''' <history>20100925�@�n���R</history>
    Public Function SetCsvData(ByVal Response As System.Web.HttpResponse, ByVal grdItiran As Web.UI.WebControls.GridView, ByVal strMitumoriFlg As String, ByVal rbnFlg1 As Boolean, ByVal strCsvFlg As String, ByVal strUserId As String _
                                , ByRef dtCsvTable As Data.DataTable, _
                                ByVal strSesyuMei As String, _
                                ByVal strKeiretuCd As String, _
                                ByVal strTS As String) As Boolean

        Dim dtBukkenRirekiNo As New Data.DataTable
        'CSV�f�[�^
        With dtCsvTable.Columns
            .Add(New System.Data.DataColumn("kbn", GetType(String)))
            .Add(New System.Data.DataColumn("hosyousyo_no", GetType(String)))
            .Add(New System.Data.DataColumn("sesyu_mei", GetType(String)))
            .Add(New System.Data.DataColumn("kameiten_cd", GetType(String)))
            .Add(New System.Data.DataColumn("kameiten_mei1", GetType(String)))
            .Add(New System.Data.DataColumn("irai_tantousya_mei", GetType(String)))
            .Add(New System.Data.DataColumn("bukken_jyuusyo1", GetType(String)))
            .Add(New System.Data.DataColumn("bukken_jyuusyo2", GetType(String)))
            .Add(New System.Data.DataColumn("bukken_jyuusyo3", GetType(String)))
            .Add(New System.Data.DataColumn("bunrui_cd", GetType(String)))
            .Add(New System.Data.DataColumn("syouhin_cd", GetType(String)))
            .Add(New System.Data.DataColumn("syouhin_mei", GetType(String)))
            .Add(New System.Data.DataColumn("suuryou", GetType(String)))
            .Add(New System.Data.DataColumn("tani", GetType(String)))
            .Add(New System.Data.DataColumn("tanka", GetType(String)))
            .Add(New System.Data.DataColumn("kingaku", GetType(String)))
            '====================��2015/06/18 430002 �C����====================
            .Add(New System.Data.DataColumn("zeiritu", GetType(String)))
            '====================��2015/06/18 430002 �C����====================
        End With
        Dim drCsvRow As Data.DataRow

        '�p�����[�^���쐬
        Dim dtTable As New Data.DataTable
        With dtTable.Columns
            .Add(New System.Data.DataColumn("kbn", GetType(String)))
            .Add(New System.Data.DataColumn("hosyousyo_no", GetType(String)))
            .Add(New System.Data.DataColumn("rireki_syubetu", GetType(String)))
            .Add(New System.Data.DataColumn("rireki_no", GetType(String)))
            .Add(New System.Data.DataColumn("nyuuryoku_no", GetType(String)))
            .Add(New System.Data.DataColumn("hanyou_cd", GetType(String)))
            .Add(New System.Data.DataColumn("henkou_kahi_flg", GetType(String)))
            .Add(New System.Data.DataColumn("torikesi", GetType(String)))
            .Add(New System.Data.DataColumn("add_login_user_id", GetType(String)))
            .Add(New System.Data.DataColumn("upd_login_user_id", GetType(String)))
        End With

        Using scope As New TransactionScope(TransactionScopeOption.Required, TimeSpan.Zero)
            Try
                'CSV�f�[�^
                Dim CsvTable As New Data.DataTable
                If rbnFlg1 = True Then
                    '�ʂ̏ꍇ
                    For intRow As Integer = 0 To grdItiran.Rows.Count - 1
                        If CType(grdItiran.Rows(intRow).FindControl("chkTaisyou"), Web.UI.WebControls.CheckBox).Checked = True Then

                            'CSV�f�[�^���o��
                            CsvTable = GetCsvDataKobetu(grdItiran.Rows(intRow).Cells(1).Text.Trim, grdItiran.Rows(intRow).Cells(2).Text.Trim, grdItiran.Rows(intRow).Cells(5).Text.Trim, strMitumoriFlg, strSesyuMei, strKeiretuCd, strTS)

                            If CsvTable.Rows.Count > 0 Then
                                '��������\������
                                dtTable.Rows.Clear()
                                Dim drRow As DataRow
                                drRow = dtTable.NewRow
                                drRow.Item("kbn") = grdItiran.Rows(intRow).Cells(1).Text.Trim
                                drRow.Item("hosyousyo_no") = grdItiran.Rows(intRow).Cells(2).Text.Trim
                                drRow.Item("rireki_syubetu") = "27"
                                drRow.Item("rireki_no") = "1"
                                dtTable.Rows.Add(drRow)

                                '����NO
                                Dim strNyuuryokuNo As String = "0"
                                dtBukkenRirekiNo = GetBukkenRirekiNo(dtTable.Rows(0))
                                If dtBukkenRirekiNo.Rows.Count > 0 Then
                                    strNyuuryokuNo = dtBukkenRirekiNo.Rows(0).Item("nyuuryoku_no").ToString
                                End If

                                dtTable.Rows.Clear()
                                drRow = dtTable.NewRow
                                drRow.Item("kbn") = grdItiran.Rows(intRow).Cells(1).Text.Trim
                                drRow.Item("hosyousyo_no") = grdItiran.Rows(intRow).Cells(2).Text.Trim
                                drRow.Item("rireki_syubetu") = "27"
                                drRow.Item("rireki_no") = "1"
                                drRow.Item("nyuuryoku_no") = (CInt(strNyuuryokuNo) + 1).ToString
                                drRow.Item("hanyou_cd") = "010"
                                drRow.Item("henkou_kahi_flg") = "1"
                                drRow.Item("torikesi") = "0"
                                drRow.Item("add_login_user_id") = strUserId
                                drRow.Item("upd_login_user_id") = strUserId
                                dtTable.Rows.Add(drRow)

                                If GetBukkenRirekiChk(dtTable.Rows(0)).Rows.Count = 0 Then
                                    '�o�^
                                    InsBukkenRireki(dtTable.Rows(0), False)
                                Else
                                    '�X�V
                                    InsBukkenRireki(dtTable.Rows(0), True)
                                End If
                                '��������\������

                                'CSV�t�@�C�����e�ݒ�
                                For intRow1 As Integer = 0 To CsvTable.Rows.Count - 1
                                    drCsvRow = dtCsvTable.NewRow
                                    drCsvRow = CsvTable.Rows(intRow1)
                                    dtCsvTable.Rows.Add(drCsvRow.ItemArray)

                                    '�@�ʐ����e�[�u���։��L���e����������
                                    TyousaMitumoriYouDataSyuturyokuDA.UpdTeibetuSeikyuu(grdItiran.Rows(intRow).Cells(1).Text.Trim, grdItiran.Rows(intRow).Cells(2).Text.Trim, CsvTable.Rows(intRow1).Item("bunrui_cd").ToString, strUserId)
                                Next
                            End If
                        End If
                    Next
                Else
                    For intRow As Integer = 0 To grdItiran.Rows.Count - 1
                        If CType(grdItiran.Rows(intRow).FindControl("chkTaisyou"), Web.UI.WebControls.CheckBox).Checked = True Then

                            '�A���̏ꍇ
                            CsvTable = GetCsvDataRentou(grdItiran.Rows(intRow).Cells(1).Text.Trim + grdItiran.Rows(intRow).Cells(2).Text.Trim, grdItiran.Rows(intRow).Cells(5).Text.Trim, CInt(strCsvFlg), strMitumoriFlg, strSesyuMei, strKeiretuCd, strTS)

                            If CsvTable.Rows.Count > 0 Then

                                '��������\������
                                dtTable.Rows.Clear()
                                Dim drRow As DataRow
                                drRow = dtTable.NewRow
                                drRow.Item("kbn") = grdItiran.Rows(intRow).Cells(1).Text.Trim
                                drRow.Item("hosyousyo_no") = grdItiran.Rows(intRow).Cells(2).Text.Trim
                                drRow.Item("rireki_syubetu") = "27"
                                drRow.Item("rireki_no") = "1"
                                dtTable.Rows.Add(drRow)

                                '����NO
                                Dim strNyuuryokuNo As String = "0"
                                dtBukkenRirekiNo = GetBukkenRirekiNo(dtTable.Rows(0))
                                If dtBukkenRirekiNo.Rows.Count > 0 Then
                                    strNyuuryokuNo = dtBukkenRirekiNo.Rows(0).Item("nyuuryoku_no").ToString
                                End If

                                dtTable.Rows.Clear()
                                drRow = dtTable.NewRow
                                drRow.Item("kbn") = grdItiran.Rows(intRow).Cells(1).Text.Trim
                                drRow.Item("hosyousyo_no") = grdItiran.Rows(intRow).Cells(2).Text.Trim
                                drRow.Item("rireki_syubetu") = "27"
                                drRow.Item("rireki_no") = "1"
                                drRow.Item("nyuuryoku_no") = (CInt(strNyuuryokuNo) + 1).ToString
                                drRow.Item("hanyou_cd") = "010"
                                drRow.Item("henkou_kahi_flg") = "1"
                                drRow.Item("torikesi") = "0"
                                drRow.Item("add_login_user_id") = strUserId
                                drRow.Item("upd_login_user_id") = strUserId
                                dtTable.Rows.Add(drRow)

                                If GetBukkenRirekiChk(dtTable.Rows(0)).Rows.Count = 0 Then
                                    '�o�^
                                    InsBukkenRireki(dtTable.Rows(0), False)
                                Else
                                    '�X�V
                                    InsBukkenRireki(dtTable.Rows(0), True)
                                End If
                                '��������\������

                                'CSV�t�@�C�����e�ݒ�
                                If bolFlg = False Then
                                    For intRow1 As Integer = 0 To CsvTable.Rows.Count - 1
                                        drCsvRow = dtCsvTable.NewRow
                                        drCsvRow = CsvTable.Rows(intRow1)
                                        dtCsvTable.Rows.Add(drCsvRow.ItemArray)
                                    Next
                                    bolFlg = True
                                End If

                                '�@�ʐ����e�[�u���̍X�V
                                For intRow1 As Integer = 0 To CsvTable.Rows.Count - 1
                                    '�@�ʐ����e�[�u���։��L���e����������
                                    TyousaMitumoriYouDataSyuturyokuDA.UpdTeibetuSeikyuu(grdItiran.Rows(intRow).Cells(1).Text.Trim, grdItiran.Rows(intRow).Cells(2).Text.Trim, CsvTable.Rows(intRow1).Item("bunrui_cd").ToString, strUserId)
                                Next
                            End If
                        End If
                    Next
                End If

                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using

    End Function
End Class
