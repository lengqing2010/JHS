Imports Itis.Earth.DataAccess
Imports System.Transactions
Public Class TyuiJyouhouInquiryLogic
    ''' <summary> ���C�����j���[�N���X�̃C���X�^���X���� </summary>
    Private TyuiJyouhouDataSet As New TyuiJyouhouInquiryDataAccess

    ''' <summary>�D�撍�ӎ������擾����</summary>
    Public Function GetYuusenTyuuiJikouInfo(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        Return TyuiJyouhouDataSet.SelYuusenTyuuiJikouInfo(strKameitenCd, strUerId)
    End Function
    ''' <summary>�ʏ풍�ӎ������擾����</summary>
    Public Function GetTuujyouTyuuiJikouInfo(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        Return TyuiJyouhouDataSet.SelTuujyouTyuuiJikouInfo(strKameitenCd, strUerId)
    End Function
    ''' <summary>��ʂ��擾����</summary>
    Public Function GetSyubetuInfo(ByVal flg As String) As TyuiJyouhouDataSet.MeisyouTableDataTable
        Return TyuiJyouhouDataSet.SelSyubetuInfo(flg)
    End Function
    ''' <summary>������Џ����擾����</summary>
    Public Function GetKaisyaJyouhouInfo(ByVal strKameitenCd As String, ByVal strKbn As String) As TyuiJyouhouDataSet.KaisyaTableDataTable
        Return TyuiJyouhouDataSet.SelKaisyaJyouhouInfo(strKameitenCd, strKbn)
    End Function
    ''' <summary>��Ђ��擾����</summary>
    Public Function GetKaisyaInfo(Optional ByVal strKaisyaCd As String = "") As TyuiJyouhouDataSet.KaisyaTableDataTable
        Return TyuiJyouhouDataSet.SelKaisyaInfo(strKaisyaCd)
    End Function
    ''' <summary>���ӎ����̍X�V����</summary>
    Public Function SetUpdTyuuiJikou(ByVal dtTyuiJyouhouData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable, ByVal strRowTime As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyuuiJikouHaita(dtTyuiJyouhouData)
            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else

                    TyuiJyouhouDataSet.UpdTyuuiJikou(dtTyuiJyouhouData)

                    TyuiJyouhouDataSet.UpdTyuuiJikouRenkei(dtTyuiJyouhouData, "2")

                    scope.Complete()
                    Return ""
                End If
            End If

        End Using

    End Function
    ''' <summary>���ӎ����̐V�K����</summary>
    Public Function SetInsTyuuiJikou(ByVal dtTyuiJyouhouData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            TyuiJyouhouDataSet.InsTyuuiJikou(dtTyuiJyouhouData)
            If TyuiJyouhouDataSet.SelTyuuiJikouCount(dtTyuiJyouhouData) = 0 Then
                TyuiJyouhouDataSet.InsTyuuiJikouRenkei(dtTyuiJyouhouData)
            Else
                TyuiJyouhouDataSet.UpdTyuuiJikouRenkei(dtTyuiJyouhouData, "1")
            End If

            scope.Complete()
        End Using

        Return ""
    End Function
    ''' <summary>���ӎ����̍폜����</summary>
    Public Function SetDelTyuuiJikou(ByVal strKameitenCd As String, ByVal strNyuuryokuNo As String, ByVal strRowTime As String, ByVal strUserId As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtTyuiJyouhouData As New TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable
            dtTyuiJyouhouData.Rows.Add(dtTyuiJyouhouData.NewRow)
            dtTyuiJyouhouData.Rows(0).Item("kameiten_cd") = strKameitenCd
            dtTyuiJyouhouData.Rows(0).Item("nyuuryoku_no") = strNyuuryokuNo
            dtTyuiJyouhouData.Rows(0).Item("kousinsya") = strUserId
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyuuiJikouHaita(dtTyuiJyouhouData)
            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    TyuiJyouhouDataSet.InsTyuuiJikouRenkei2(dtTyuiJyouhouData, strNyuuryokuNo)
                    'TyuiJyouhouDataSet.DelTyuuiJikou(strKameitenCd, strNyuuryokuNo, strUserId)

                    'TyuiJyouhouDataSet.UpdTyuuiJikouRenkei(dtTyuiJyouhouData, "9")

                    scope.Complete()
                    Return ""
                End If
            End If
        End Using

    End Function
    ''' <summary>������Ђ̍X�V����</summary>
    Public Function SetUpdTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, ByVal strRowTime As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyousaKaisyaHaita(dtTyousaKaisyaUPDData)
            If dtReturn.Rows.Count = 1 And (Split(dtTyousaKaisyaUPDData.Rows(0).Item("tys_kaisya_cd"), ":")(0) & Split(dtTyousaKaisyaUPDData.Rows(0).Item("jigyousyo_cd"), ":")(0)) <> (Split(dtTyousaKaisyaUPDData.Rows(0).Item("tys_kaisya_cd"), ":")(1) & Split(dtTyousaKaisyaUPDData.Rows(0).Item("jigyousyo_cd"), ":")(1)) Then
                scope.Dispose()
                Return "E"
            Else
                dtReturn = TyuiJyouhouDataSet.SelTyousaKaisyaHaita(dtTyousaKaisyaUPDData, True)
                If dtReturn.Rows.Count = 0 Then
                    scope.Dispose()
                    Return "H"
                End If
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    'TyuiJyouhouDataSet.InsTyousaKaisyaRenkei2(dtTyousaKaisyaUPDData)
                    TyuiJyouhouDataSet.UpdTyousaKaisya(dtTyousaKaisyaUPDData)
                    TyuiJyouhouDataSet.UpdTyousaKaisyaRenkei(dtTyousaKaisyaUPDData, "2")
                    scope.Complete()
                    Return ""
                End If
            End If

        End Using
    End Function
    ''' <summary>������Ђ̍폜����</summary>
    Public Function SetDelTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, ByVal strRowTime As String, ByVal intRow As Integer) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyousaKaisyaHaita(dtTyousaKaisyaUPDData, True)
            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    'TyuiJyouhouDataSet.InsTyousaKaisyaRenkei2(dtTyousaKaisyaUPDData)
                    TyuiJyouhouDataSet.DelTyousaKaisya(dtTyousaKaisyaUPDData, intRow)
                    TyuiJyouhouDataSet.UpdTyousaKaisyaRenkei(dtTyousaKaisyaUPDData, "9", intRow)
                    scope.Complete()
                    Return ""
                End If
            End If

        End Using
    End Function
    ''' <summary>��b�d�l���擾����</summary>
    Public Function GetKisoSiyouInfo(Optional ByVal strKsno As String = "") As TyuiJyouhouDataSet.KisoSiyouTableDataTable
        Return TyuiJyouhouDataSet.SelKisoSiyouInfo(strKsno)
    End Function
    '''<summary>��b�d�l�ݒ���擾����</summary>
    Public Function GetKisoSiyouSetteiInfo(ByVal strKameitenCd As String, ByVal strKahiKbn As String) As TyuiJyouhouDataSet.KisoSiyouTableDataTable
        Return TyuiJyouhouDataSet.SelKisoSiyouSetteiInfo(strKameitenCd, strKahiKbn)
    End Function
    '''<summary>��b�d�l�ݒ�̐V�K����</summary>
    Public Function SetInsKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strKousinsya As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelKisoSiyouHaita(strKameitenCd, strKsSiyouNo)
            If dtReturn.Rows.Count > 0 Then
                scope.Dispose()
                Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
            Else
                TyuiJyouhouDataSet.InsKisoSiyouSettei(strKameitenCd, strKahiKbn, strKsSiyouNo, strKousinsya)
                If TyuiJyouhouDataSet.SelKisoSiyouSetteiCount(strKameitenCd, strKsSiyouNo) = 0 Then
                    TyuiJyouhouDataSet.InsKisoSiyouSetteiRenkei(strKameitenCd, strKsSiyouNo, strKousinsya)
                Else
                    TyuiJyouhouDataSet.UpdKisoSiyouSetteiRenkei(strKameitenCd, strKsSiyouNo, strKousinsya, "1")
                End If
                'TyuiJyouhouDataSet.InsKisoSiyouSettei2(strKameitenCd, strKousinsya)
                scope.Complete()
                Return ""
            End If
        End Using

    End Function
    '''<summary>������Ђ̐V�K����</summary>
    Public Function SetInsTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyousaKaisyaHaita(dtTyousaKaisyaUPDData)
            If dtReturn.Rows.Count > 0 Then
                scope.Dispose()
                Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
            Else

                TyuiJyouhouDataSet.InsTyousaKaisya(dtTyousaKaisyaUPDData)
                If TyuiJyouhouDataSet.SelTyousaKaisyaCount(dtTyousaKaisyaUPDData) = 0 Then
                    TyuiJyouhouDataSet.InsTyousaKaisyaRenkei(dtTyousaKaisyaUPDData)
                Else
                    TyuiJyouhouDataSet.UpdTyousaKaisyaRenkei(dtTyousaKaisyaUPDData, "1")
                End If
                'TyuiJyouhouDataSet.InsTyousaKaisyaRenkei2(dtTyousaKaisyaUPDData)
                scope.Complete()
                Return ""
            End If
        End Using

    End Function
    '''<summary>��b�d�l�ݒ�̍X�V����</summary>
    Public Function SetUpdKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strUerId As String, ByVal strRowTime As String) As String

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelKisoSiyouHaita(strKameitenCd, Split(strKsSiyouNo, ":")(0))
            If dtReturn.Rows.Count = 1 And (Split(strKsSiyouNo, ":")(1) <> Split(strKsSiyouNo, ":")(0)) Then
                scope.Dispose()
                Return "E"
            Else
                dtReturn = TyuiJyouhouDataSet.SelKisoSiyouHaita(strKameitenCd, Split(strKsSiyouNo, ":")(1))
                If dtReturn.Rows.Count = 0 Then
                    scope.Dispose()
                    Return "H"
                End If
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    'TyuiJyouhouDataSet.InsKisoSiyouSettei2(strKameitenCd, strUerId)
                    TyuiJyouhouDataSet.UpdKisoSiyouSettei(strKameitenCd, strKahiKbn, strKsSiyouNo, strUerId)
                    TyuiJyouhouDataSet.UpdKisoSiyouSetteiRenkei(strKameitenCd, strKsSiyouNo, strUerId, "2")
                    scope.Complete()
                    Return ""
                End If

            End If

        End Using
    End Function
    '''<summary>��b�d�l�ݒ�̍폜����</summary>
    Public Function SetDelKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strRowTime As String, ByVal strUserId As String, ByVal intRow As Integer) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)

            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelKisoSiyouHaita(strKameitenCd, strKsSiyouNo)
            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    'TyuiJyouhouDataSet.InsKisoSiyouSettei2(strKameitenCd, strUserId)
                    TyuiJyouhouDataSet.DelKisoSiyouSettei(strKameitenCd, strKsSiyouNo, strKahiKbn, intRow)
                    TyuiJyouhouDataSet.UpdKisoSiyouSetteiRenkei(strKameitenCd, strKsSiyouNo, strUserId, "9", intRow, strKahiKbn)
                    scope.Complete()
                    Return ""
                End If

            End If

        End Using
    End Function

    ''' <summary>
    ''' �u����v�����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�u����v�f�[�^</returns>
    ''' <hidtory>2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ�</hidtory>
    Public Function GetTorikesi(ByVal strKameitenCd As String) As Data.DataTable

        '�߂�l
        Return TyuiJyouhouDataSet.SelTorikesi(strKameitenCd)

    End Function

    ''' <summary>
    ''' �u�H�������ʁv�����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�u����v�f�[�^</returns>
    ''' <hidtory>2012/05/15 �ԗ� 407553�̑Ή� �ǉ�</hidtory>
    Public Function GetKoujiUriageSyuubetu(ByVal strKameitenCd As String) As Data.DataTable

        '�߂�l
        Return TyuiJyouhouDataSet.SelKoujiUriageSyuubetu(strKameitenCd)

    End Function

    '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
    ''' <summary>���̎�ʁ�33�̖��̂��擾����</summary>
    Public Function GetSyubetuInfo33() As Data.DataTable

        '�߂�l
        Return TyuiJyouhouDataSet.SelSyubetuInfo33()

    End Function

    ''' <summary>�g���u���E�N���[�������擾����</summary>
    Public Function GetTuujyouTyuuiJikouInfoTORA(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable

        '�߂�l
        Return TyuiJyouhouDataSet.SelTuujyouTyuuiJikouInfoTORA(strKameitenCd, strUerId)

    End Function

    '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================

    ''' <summary>���i�R�[�h���擾����</summary>
    ''' <returns>���i�R�[�h�f�[�^�e�[�u��</returns>
    ''' <history>2016/05/04�@chel1�@�V�K�쐬</history>
    Public Function GetSyouhinCd() As Data.DataTable

        '�߂�l
        Return TyuiJyouhouDataSet.SelSyouhinCd()

    End Function

    ''' <summary>�������@���擾����</summary>
    ''' <returns>�������@�f�[�^�e�[�u��</returns>
    ''' <history>2016/05/04�@chel1�@�V�K�쐬</history>
    Public Function GetTyousaHouhou() As Data.DataTable

        '�߂�l
        Return TyuiJyouhouDataSet.SelTyousaHouhou()

    End Function


    '''<summary>��{���i�̍X�V����</summary>
    Public Function SetKihonSyouhin(ByVal strKameitenCd As String, ByVal strKihonSyouhinCd As String, ByVal strKihonSyouhinTyuuibun As String) As Boolean

        '�߂�l
        Return TyuiJyouhouDataSet.UpdKihonSyouhin(strKameitenCd, strKihonSyouhinCd, strKihonSyouhinTyuuibun)

    End Function

    '''<summary>��{�������@�̍X�V����</summary>
    Public Function SetKihonTyousaHouhou(ByVal strKameitenCd As String, ByVal strKihonTyousaHouhouNo As String, ByVal strKihonTyousaHouhouTyuuibun As String) As Boolean

        '�߂�l
        Return TyuiJyouhouDataSet.UpdKihonTyousaHouhou(strKameitenCd, strKihonTyousaHouhouNo, strKihonTyousaHouhouTyuuibun)

    End Function

    ''' <summary>
    ''' ��{���i�ƒ������@���擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <history>2016/05/04�@chel1�@�V�K�쐬</history>
    Public Function GetKihouSyouhinAndTyousaHouhou(ByVal strKameitenCd As String) As Data.DataTable

        '�߂�l
        Return TyuiJyouhouDataSet.SelKihouSyouhinAndTyousaHouhou(strKameitenCd)

    End Function

End Class
