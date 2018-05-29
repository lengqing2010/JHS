Imports Itis.Earth.DataAccess
Imports Itis.Earth
Imports System.Transactions

Public Class KakakuseikyuJyouhouLogic

    ''' <summary> ���C�����j���[�N���X�̃C���X�^���X���� </summary>
    Private KihonJyouhouDA As New KakakuseikyuJyouhouDataAccess()
    Private CommonHaita As New HaitaCheck()

    ''' <summary>
    ''' ���b�Z�[�W�ݒ�
    ''' </summary>
    ''' <param name="msg">�����b�Z�[�W</param>
    ''' <param name="param1">�����P</param>
    ''' <param name="param2">�����Q</param>
    ''' <param name="param3">�����R</param>
    ''' <param name="param4">�����S</param>
    ''' <returns>���b�Z�[�W</returns>
    ''' <remarks></remarks>
    Public Function MakeMessage(ByVal msg As String, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "") As String

        msg = msg.Replace("@PARAM1", param1) _
                  .Replace("@PARAM2", param2) _
                  .Replace("@PARAM3", param3) _
                  .Replace("@PARAM4", param4)

        Return String.Format(msg, param1, param2, param3, param4)

    End Function

    '�F�j100606�@������@�ǉ��@����������������������������������

    ''' <summary>
    ''' [���i�E�������]�p�@�����搗�`�}�X�^�擾
    ''' </summary>
    ''' <param name="seikyuu_saki_brc">������}��</param>
    ''' <param name="torikesi">���</param>
    Public Function GetSeikyusakiHina(ByVal seikyuu_saki_brc As String, _
                                    ByVal torikesi As Integer) As Data.DataTable

        Return KihonJyouhouDA.SelSeikyusakiHina(seikyuu_saki_brc, _
                                             torikesi)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�c�Ə��}�X�^�擾
    ''' </summary>
    ''' <param name="eigyousyo_cd">�c�Ə��R�[�h</param>
    ''' <param name="torikesi">���</param>
    Public Function GetEigyousyo(ByVal eigyousyo_cd As String, _
                                    ByVal torikesi As Integer) As Data.DataTable

        Return KihonJyouhouDA.SelEigyousyo(eigyousyo_cd, _
                                             torikesi)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�c�Ə��}�X�^�擾
    ''' </summary>
    ''' <param name="eigyousyo_cd">�c�Ə��R�[�h</param>
    Public Function GetEigyousyoForSeikyusaki(ByVal eigyousyo_cd As String) As Data.DataTable

        Return KihonJyouhouDA.SelEigyousyoForSeikyusaki(eigyousyo_cd)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@������Ѓ}�X�^�擾
    ''' </summary>
    ''' <param name="tys_kaisya_cd">������ЃR�[�h</param>
    ''' <param name="jigyousyo_cd">���Ə��R�[�h</param>
    ''' <param name="torikesi">���</param>
    Public Function GetTyousakaisya(ByVal tys_kaisya_cd As String, _
                                    ByVal jigyousyo_cd As String, _
                                    ByVal torikesi As Integer) As Data.DataTable

        Return KihonJyouhouDA.SelTyousakaisya(tys_kaisya_cd, _
                                             jigyousyo_cd, _
                                             torikesi)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@������}�X�^�擾
    ''' </summary>
    ''' <param name="seikyuu_saki_kbn">������敪</param>
    ''' <param name="seikyuu_saki_cd">������R�[�h</param>
    ''' <param name="seikyuu_saki_brc">������}��</param>
    ''' <param name="torikesi">���</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeikyusaki(ByVal seikyuu_saki_kbn As String, _
                                    ByVal seikyuu_saki_cd As String, _
                                    ByVal seikyuu_saki_brc As String, _
                                    ByVal torikesi As Integer, _
                                    Optional ByVal torikesiKbn As Boolean = True) As Data.DataTable

        Return KihonJyouhouDA.SelSeikyusaki(seikyuu_saki_kbn, _
                                             seikyuu_saki_cd, _
                                             seikyuu_saki_brc, _
                                             torikesi, torikesiKbn)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@��{�Z�[�g�p�f�[�^�擾
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKihonsetData(ByVal kameitenCd As String)
        Return KihonJyouhouDA.SelKihonsetData(kameitenCd)
    End Function
    ''' <summary>
    ''' [���i�E�������]�p�@��{�Z�[�g�p�f�[�^����
    ''' (�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���)
    ''' </summary>
    '''<history>2016/01/29 chel1 �ǉ�</history>
    Public Function GetKihonsetDataBrc30(ByVal kameitenCd As String) As Data.DataTable
        Return KihonJyouhouDA.SelKihonsetDataBrc30(kameitenCd)
    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�g�����̃}�X�^.������敪�f�[�^�擾
    ''' </summary>
    ''' <param name="Syubetu">���</param>
    ''' <returns>�g�����̃}�X�^</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyusakuKBN(ByVal Syubetu As String)
        Return KihonJyouhouDA.SelSeikyusakiKbn(Syubetu)
    End Function

    '������@�ǉ��@������������������������������������

    ''' <summary>
    ''' [���i�E�������]�p�@�����X�f�[�^�擾
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�����X�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetKameiten(ByVal strKameitenCd As String) As KameitenDataSet.m_kameitenTableDataTable
        Return KihonJyouhouDA.SelKameiten(strKameitenCd)
    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@���������f�[�^�擾
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTatou(ByVal strKameitenCd As String) As KameitenDataSet.m_tatouwaribiki_setteiTableDataTable
        Return KihonJyouhouDA.SelTatou(strKameitenCd)
    End Function

    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
    ''' <summary>
    ''' [���i�E�������]�p�@���������f�[�^�擾
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTatouKakaku(ByVal strKameitenCd As String) As Data.DataTable
        Return KihonJyouhouDA.SelTatouKakaku(strKameitenCd)
    End Function
    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

    ''' <summary>
    ''' [���i�E�������]�p�@���i�}�X�^�����A���i�R�[�h�`�F�b�N
    ''' </summary>
    ''' <param name="syouhinCd">���i�R�[�h</param>
    ''' <param name="errorNo">�G���[�ԍ�</param>
    ''' <returns>�G���[���b�Z�[�W</returns>
    ''' <remarks></remarks>
    Public Function CheckSyouhin(ByVal syouhinCd As String(), ByRef errorNo As String) As String

        Dim strKekka = ""

        Dim kubun As Integer
        For kubun = 0 To syouhinCd.Length - 1
            If syouhinCd(kubun) = "" Or syouhinCd(kubun) = "00000" Then
                '���͂������i�R�[�h�͋�̏ꍇ�`�`
                'doNothing
            Else
                If KihonJyouhouDA.SelsyouhinCd(syouhinCd(kubun)) <> True Then

                    strKekka = MakeMessage(Messages.Instance.MSG2008E, "���i�R�[�h")
                    errorNo = (kubun + 1).ToString
                    Exit For
                End If
            End If
        Next

        Return strKekka

    End Function

    ''' <summary>
    ''' �����}�X�^�r������
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="dtTatou">�����e�[�u��</param>
    ''' <returns>�G���[���b�Z�[�W</returns>
    ''' <remarks></remarks>
    Public Function TatouHaita(ByVal strKameitenCd As String, ByVal dtTatou As KameitenDataSet.m_tatouwaribiki_setteiTableDataTable) As String

        Dim strReturn As String = ""
        Dim i As Int16
        Dim returnMsg As String = ""

        For i = 0 To dtTatou.Rows.Count - 1
            strReturn = KihonJyouhouDA.SelTatouHaita(strKameitenCd, CType(dtTatou.Rows(i).Item("toukubun"), Int16))
            If strReturn = "" Then
                returnMsg = Messages.Instance.MSG2009E
            Else
                If dtTatou.Rows(i).Item("upd_datetime").ToString <> "" Then
                    If strReturn.Split(",")(1).Trim <> "" Then
                        If CType(strReturn.Split(",")(1), DateTime) > CType(dtTatou.Rows(i).Item("upd_datetime"), DateTime) Then
                            returnMsg = MakeMessage(Messages.Instance.MSG003E, Split(strReturn, ",")(0), Split(strReturn, ",")(1))
                            Exit For
                        Else

                        End If
                    Else

                    End If

                Else
                    If strReturn.Split(",")(1).Trim <> "" And strReturn.Split(",")(1).ToString.Trim <> "1900/01/01" Then
                        returnMsg = MakeMessage(Messages.Instance.MSG003E, Split(strReturn, ",")(0), Split(strReturn, ",")(1))
                        Exit For
                    Else

                    End If
                End If

            End If
        Next

        Return returnMsg

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�����X�}�X�^�A�r���`�F�b�N
    ''' </summary>
    ''' <param name="errorSyouhinNo">�G���[���i�ԍ�</param>
    ''' <param name="syouhinCd">���i�R�[�h</param>
    ''' <param name="dtTatouHaita">�����r���e�[�u��</param>
    ''' <param name="dtTatou">�����e�[�u��</param>
    ''' <param name="dtKameiten">�����X�e�[�u��</param>
    ''' <returns>���b�Z�[�W</returns>
    ''' <remarks></remarks>
    Public Function TourokuSyori(ByRef errorSyouhinNo As String, _
                                ByVal syouhinCd As String(), _
                                ByRef dtTatouHaita As KameitenDataSet.m_tatouwaribiki_setteiTableDataTable, _
                                ByVal dtTatou As TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable, _
                                ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable, _
                                ByVal dtSeikyusaki As Data.DataTable) As String

        Dim strKameitenCd As String
        Dim strKekka As String
        Dim kino As String

        kino = "���i�E�������̓o�^"

        strKameitenCd = dtKameiten.Rows(0).Item("kameiten_cd").ToString


        '���i�R�[�h���݃`�F�b�N
        strKekka = CheckSyouhin(syouhinCd, errorSyouhinNo)

        If strKekka <> "" Then
            Return strKekka
        End If

        '�����r���`�F�b�N
        strKekka = TatouHaita(strKameitenCd, dtTatouHaita)

        If strKekka <> "" Then
            Return strKekka
        End If

        '�o�^����
        If UpdKameiten(dtTatou, dtKameiten, dtSeikyusaki) Then
            Return MakeMessage(Messages.Instance.MSG018S, kino)
        Else
            Return MakeMessage(Messages.Instance.MSG019E, kino)
        End If

    End Function


    ''' <summary>
    ''' [���i�E�������]�p�@���������}�X�^�A�d���`�F�b�N(1,2,3�̂�)�A�V�K�܂��X�V�𔻒f����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="kubunn">���敪</param>
    ''' <returns>TRUE:����,FALSE:�s����</returns>
    ''' <remarks></remarks>
    Public Function CheckTatouJyufuku(ByVal strKameitenCd As String, ByVal kubunn As Integer) As Boolean

        Return KihonJyouhouDA.SelTatouJyufuku(strKameitenCd)(kubunn - 1)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@���������}�X�^�A�}���܂��X�V
    ''' </summary>
    ''' <param name="dtTatou">�����e�[�u��</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function InsUpdTatou(ByVal dtTatou As TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable) As Boolean

        Dim kubun As Integer
        Dim i As Integer
        Dim rowNo As Integer
        Dim Kekka As Integer

        Kekka = 0

        Dim dtTatouKubun1 As New TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable
        Dim dtTatouKubun2 As New TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable
        Dim dtTatouKubun3 As New TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable

        For rowNo = 0 To dtTatou.Rows.Count - 1

            If dtTatou.Rows(rowNo).Item("toukubun") = 1 Then
                For i = 0 To dtTatou.Columns.Count - 1
                    dtTatouKubun1.Rows.Add(dtTatouKubun1.NewRow)
                    dtTatouKubun1.Rows(0).Item(i) = dtTatou.Rows(rowNo).Item(i)
                Next
            ElseIf dtTatou.Rows(rowNo).Item("toukubun") = 2 Then
                For i = 0 To dtTatou.Columns.Count - 1
                    dtTatouKubun2.Rows.Add(dtTatouKubun2.NewRow)
                    dtTatouKubun2.Rows(0).Item(i) = dtTatou.Rows(rowNo).Item(i)
                Next
            ElseIf dtTatou.Rows(rowNo).Item("toukubun") = 3 Then
                For i = 0 To dtTatou.Columns.Count - 1
                    dtTatouKubun3.Rows.Add(dtTatouKubun3.NewRow)
                    dtTatouKubun3.Rows(0).Item(i) = dtTatou.Rows(rowNo).Item(i)
                Next
            End If

        Next

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            For kubun = 0 To dtTatou.Rows.Count - 1

                If dtTatou.Rows(kubun).Item("syouhin_cd") = "" Then

                    If CheckTatouJyufuku(dtTatou.Rows(kubun).Item("kameiten_cd"), dtTatou.Rows(kubun).Item("toukubun")) Then
                        If dtTatou.Rows(kubun).Item("toukubun") = 1 Then

                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun1, "del") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                            End If

                        ElseIf dtTatou.Rows(kubun).Item("toukubun") = 2 Then

                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun2, "del") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                            End If

                        ElseIf dtTatou.Rows(kubun).Item("toukubun") = 3 Then

                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun3, "del") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                            End If

                        End If
                    Else
                        Kekka = Kekka + 1
                    End If

                Else

                    If CheckTatouJyufuku(dtTatou.Rows(kubun).Item("kameiten_cd"), dtTatou.Rows(kubun).Item("toukubun")) Then
                        If dtTatou.Rows(kubun).Item("toukubun") = 1 Then

                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun1, "upd") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                            End If

                        ElseIf dtTatou.Rows(kubun).Item("toukubun") = 2 Then

                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun2, "upd") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                            End If

                        ElseIf dtTatou.Rows(kubun).Item("toukubun") = 3 Then

                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun3, "upd") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                            End If

                        End If
                    Else
                        If dtTatou.Rows(kubun).Item("toukubun") = 1 Then


                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun1, "ins") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                            End If

                        ElseIf dtTatou.Rows(kubun).Item("toukubun") = 2 Then

                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun2, "ins") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                            End If

                        ElseIf dtTatou.Rows(kubun).Item("toukubun") = 3 Then

                            If KihonJyouhouDA.InsUpdTatou(dtTatouKubun3, "ins") Then
                                Kekka = Kekka + 1
                            Else
                                scope.Dispose()
                                Exit Function
                            End If

                        End If
                    End If

                End If

            Next

            If Kekka = dtTatou.Rows.Count Then
                scope.Complete()
                Return True
            Else
                Return False
            End If

        End Using


    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�����X�}�X�^�A�X�V
    ''' </summary>
    ''' <param name="dtKameiten">�����X�e�[�u��</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function UpdKameiten(ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable) As Boolean
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            If KihonJyouhouDA.UpdKameiten(dtKameiten) Then
                scope.Complete()
                Return True
            Else
                scope.Dispose()
                Return False
            End If
        End Using
    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@������}�X�^�A�V�K
    ''' </summary>
    ''' <param name="dtSKU">������}�X�^</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function InsSeikyusaki(ByVal dtSKU As Data.DataTable) As Boolean

        Dim i As Integer
        Dim insDtHina As New Data.DataTable
        Dim insdeDt As New Data.DataTable
        Dim insSKUdt As New Data.DataTable

        insSKUdt.Columns.Add("seikyuu_saki_cd")
        insSKUdt.Columns.Add("seikyuu_saki_brc")
        insSKUdt.Columns.Add("seikyuu_saki_kbn")
        insSKUdt.Columns.Add("torikesi")

        insSKUdt.Columns.Add("seikyuu_sime_date")
        insSKUdt.Columns.Add("senpou_seikyuu_sime_date")
        insSKUdt.Columns.Add("tyk_koj_seikyuu_timing_flg")
        insSKUdt.Columns.Add("sousai_flg")
        insSKUdt.Columns.Add("kaisyuu_yotei_gessuu")
        insSKUdt.Columns.Add("kaisyuu_yotei_date")
        insSKUdt.Columns.Add("seikyuusyo_hittyk_date")
        insSKUdt.Columns.Add("kaisyuu1_syubetu1")
        insSKUdt.Columns.Add("kaisyuu1_wariai1")
        insSKUdt.Columns.Add("kaisyuu1_tegata_site_gessuu")
        insSKUdt.Columns.Add("kaisyuu1_tegata_site_date")
        insSKUdt.Columns.Add("kaisyuu1_seikyuusyo_yousi")
        insSKUdt.Columns.Add("kaisyuu1_syubetu2")
        insSKUdt.Columns.Add("kaisyuu1_wariai2")
        insSKUdt.Columns.Add("kaisyuu1_syubetu3")
        insSKUdt.Columns.Add("kaisyuu1_wariai3")
        insSKUdt.Columns.Add("kaisyuu_kyoukaigaku")
        insSKUdt.Columns.Add("kaisyuu2_syubetu1")
        insSKUdt.Columns.Add("kaisyuu2_wariai1")
        insSKUdt.Columns.Add("kaisyuu2_tegata_site_gessuu")
        insSKUdt.Columns.Add("kaisyuu2_tegata_site_date")
        insSKUdt.Columns.Add("kaisyuu2_seikyuusyo_yousi")
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        '��s�x�X�R�[�h
        insSKUdt.Columns.Add("ginkou_siten_cd")
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

        insSKUdt.Columns.Add("add_login_user_id")
        insSKUdt.Columns.Add("add_datetime")
        insSKUdt.Rows.Add()

        '==========================2011/06/28 �ԗ� �ǉ� �J�n��================================
        'For i = 0 To 3
        For i = 0 To 6
            '======================2011/06/28 �ԗ� �ǉ� �I����================================

            If dtSKU.Rows(i).Item("seikyuu_saki_cd").ToString = "" And _
                dtSKU.Rows(i).Item("seikyuu_saki_brc").ToString = "" And _
                dtSKU.Rows(i).Item("seikyuu_saki_kbn").ToString = "" Then

                Continue For

            End If

            insSKUdt.Rows.RemoveAt(0)
            insSKUdt.Rows.Add()

            insSKUdt.Rows(0).Item("seikyuu_saki_cd") = dtSKU.Rows(i).Item("seikyuu_saki_cd")
            insSKUdt.Rows(0).Item("seikyuu_saki_brc") = dtSKU.Rows(i).Item("seikyuu_saki_brc")
            insSKUdt.Rows(0).Item("seikyuu_saki_kbn") = dtSKU.Rows(i).Item("seikyuu_saki_kbn")
            insSKUdt.Rows(0).Item("torikesi") = dtSKU.Rows(i).Item("torikesi")

            insSKUdt.Rows(0).Item("add_login_user_id") = dtSKU.Rows(i).Item("add_login_user_id")


            If dtSKU.Rows(i).Item("seikyuu_saki_cd").ToString <> "" And _
                dtSKU.Rows(i).Item("seikyuu_saki_brc").ToString <> "" And _
                dtSKU.Rows(i).Item("seikyuu_saki_kbn").ToString <> "" Then

                insdeDt = KihonJyouhouDA.SelSeikyusaki(dtSKU.Rows(i).Item("seikyuu_saki_kbn").ToString, _
                                                    dtSKU.Rows(i).Item("seikyuu_saki_cd").ToString, _
                                                    dtSKU.Rows(i).Item("seikyuu_saki_brc").ToString, 0)

                If insdeDt.Rows.Count > 0 Then

                    'DO NOTHING

                Else
                    insDtHina = KihonJyouhouDA.SelSeikyusakiHinaTouroku(dtSKU.Rows(i).Item("seikyuu_saki_brc").ToString, 0)

                    If insDtHina.Rows.Count > 0 Then

                        insSKUdt.Rows(0).Item("seikyuu_sime_date") = insDtHina.Rows(0).Item("seikyuu_sime_date")
                        insSKUdt.Rows(0).Item("senpou_seikyuu_sime_date") = insDtHina.Rows(0).Item("senpou_seikyuu_sime_date")
                        insSKUdt.Rows(0).Item("tyk_koj_seikyuu_timing_flg") = insDtHina.Rows(0).Item("tyk_koj_seikyuu_timing_flg")
                        insSKUdt.Rows(0).Item("sousai_flg") = insDtHina.Rows(0).Item("sousai_flg")
                        insSKUdt.Rows(0).Item("kaisyuu_yotei_gessuu") = insDtHina.Rows(0).Item("kaisyuu_yotei_gessuu")
                        insSKUdt.Rows(0).Item("kaisyuu_yotei_date") = insDtHina.Rows(0).Item("kaisyuu_yotei_date")
                        insSKUdt.Rows(0).Item("seikyuusyo_hittyk_date") = insDtHina.Rows(0).Item("seikyuusyo_hittyk_date")
                        insSKUdt.Rows(0).Item("kaisyuu1_syubetu1") = insDtHina.Rows(0).Item("kaisyuu1_syubetu1")
                        insSKUdt.Rows(0).Item("kaisyuu1_wariai1") = insDtHina.Rows(0).Item("kaisyuu1_wariai1")
                        insSKUdt.Rows(0).Item("kaisyuu1_tegata_site_gessuu") = insDtHina.Rows(0).Item("kaisyuu1_tegata_site_gessuu")
                        insSKUdt.Rows(0).Item("kaisyuu1_tegata_site_date") = insDtHina.Rows(0).Item("kaisyuu1_tegata_site_date")
                        insSKUdt.Rows(0).Item("kaisyuu1_seikyuusyo_yousi") = insDtHina.Rows(0).Item("kaisyuu1_seikyuusyo_yousi")
                        insSKUdt.Rows(0).Item("kaisyuu1_syubetu2") = insDtHina.Rows(0).Item("kaisyuu1_syubetu2")
                        insSKUdt.Rows(0).Item("kaisyuu1_wariai2") = insDtHina.Rows(0).Item("kaisyuu1_wariai2")
                        insSKUdt.Rows(0).Item("kaisyuu1_syubetu3") = insDtHina.Rows(0).Item("kaisyuu1_syubetu3")
                        insSKUdt.Rows(0).Item("kaisyuu1_wariai3") = insDtHina.Rows(0).Item("kaisyuu1_wariai3")
                        insSKUdt.Rows(0).Item("kaisyuu_kyoukaigaku") = insDtHina.Rows(0).Item("kaisyuu_kyoukaigaku")
                        insSKUdt.Rows(0).Item("kaisyuu2_syubetu1") = insDtHina.Rows(0).Item("kaisyuu2_syubetu1")
                        insSKUdt.Rows(0).Item("kaisyuu2_wariai1") = insDtHina.Rows(0).Item("kaisyuu2_wariai1")
                        insSKUdt.Rows(0).Item("kaisyuu2_tegata_site_gessuu") = insDtHina.Rows(0).Item("kaisyuu2_tegata_site_gessuu")
                        insSKUdt.Rows(0).Item("kaisyuu2_tegata_site_date") = insDtHina.Rows(0).Item("kaisyuu2_tegata_site_date")
                        insSKUdt.Rows(0).Item("kaisyuu2_seikyuusyo_yousi") = insDtHina.Rows(0).Item("kaisyuu2_seikyuusyo_yousi")
                        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
                        '��s�x�X�R�[�h
                        insSKUdt.Rows(0).Item("ginkou_siten_cd") = insDtHina.Rows(0).Item("ginkou_siten_cd")
                        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��


                    End If

                    If KihonJyouhouDA.InsSeikyusaki_for_kakaku(insSKUdt) = False Then
                        Return False
                    End If

                End If

            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@��ʃf�[�^�ŁA�f�[�^�x�[�X(�����X�}�X�^,���������}�X�^)�X�V
    ''' </summary>
    ''' <param name="dtTatou">�����e�[�u��</param>
    ''' <param name="dtKameiten">�����X�e�[�u��</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function updKameiten(ByVal dtTatou As TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable, ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable, ByVal dtSKU As Data.DataTable) As Boolean
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)

            If updKameiten(dtKameiten) = False Or InsUpdTatou(dtTatou) = False Or InsSeikyusaki(dtSKU) = False Then
                scope.Dispose()
                Return False
            Else
                scope.Complete()
                Return True
            End If

        End Using
    End Function

    '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
    ''' <summary>
    ''' �����捀�ږ����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusakiKoumokuMei() As Data.DataTable

        Return KihonJyouhouDA.SelSeikyuusakiKoumokuMei()
    End Function
    '==================2011/06/28 �ԗ� �ǉ� �I����==========================

End Class
