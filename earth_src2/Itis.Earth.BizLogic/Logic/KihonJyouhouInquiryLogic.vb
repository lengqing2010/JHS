Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Namespace kihonjyouhou

    ''' <summary>
    ''' ��{���
    ''' </summary>
    ''' <remarks></remarks>
    Public Class KihonJyouhouInquiryLogic
        ''' <summary> ���C�����j���[�N���X�̃C���X�^���X���� </summary>
        Private KihonJyouhouInquiryDA As New KihonJyouhouInquiryDataAccess

        ''' <summary>
        ''' �����X�}�X�g�����X�V����
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="insdata">�ǉ��f�[�^</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetkameitenInfo(ByVal kameiten_cd As String, _
                                          ByVal insdata As KameitenDataSet.m_kameitenTableDataTable) As Boolean

            Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)

                Try

                    KihonJyouhouInquiryDA.updkameitenInfo(kameiten_cd, insdata)

                    scope.Complete()
                    Return True
                Catch ex As Exception
                    scope.Dispose()
                    Return False
                End Try

            End Using



        End Function

        ''' <summary>
        ''' Mail���擾
        ''' </summary>
        ''' <param name="yuubin_no">�X��NO</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet
            Return KihonJyouhouInquiryDA.GetMailAddress(yuubin_no)
        End Function

        ''' <summary>
        ''' �V�X�e�����Ԏ擾
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSysDate() As String
            Return KihonJyouhouInquiryDA.GetSysDate()
        End Function

        ''' <summary>
        ''' �����X�}�X�g�����擾����
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetkameitenInfo(ByVal kameiten_cd As String _
                                       ) As KameitenDataSet.m_kameitenTableDataTable
            Return KihonJyouhouInquiryDA.SelkameitenInfo(kameiten_cd)
        End Function

        ''' <summary>
        ''' �����X�Z���}�X�g�����擾����
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetkameitenJyushoInfo(ByVal kameiten_cd As String) _
                                                    As KameitenjyushoDataSet
            Return KihonJyouhouInquiryDA.SelKameitenJyushoInfo(kameiten_cd)

        End Function

        ''' <summary>
        ''' �����X���ނ������X�Z�����擾����
        ''' 
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetMaxUpdDate(ByVal kameiten_cd As String) As String
            Dim data As String
            data = KihonJyouhouInquiryDA.SelMaxUpdDate(kameiten_cd)

            If data = String.Empty Then
                Return String.Empty

            Else
                Return data.Split(",")(0).ToString

            End If

        End Function

        ''' <summary>
        ''' ���̒[���ōX�V�`�F�b�N
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="updateDate">�X�V����</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ChkJyushoTouroku(ByVal kameiten_cd As String, ByVal updateDate As String) As String

            Dim dataBaseUpdTime As String = Convert.ToDateTime(GetSysDate()).ToString("yyyy/MM/dd HH:mm:ss")
            Dim dataBaseUpdUser As String = String.Empty
            Dim data As String
            data = KihonJyouhouInquiryDA.SelMaxUpdDate(kameiten_cd)

            If updateDate = String.Empty Then
                If data <> "," Then

                    Dim msg As New System.Text.StringBuilder
                    msg.AppendFormat(Messages.Instance.MSG003E, data.Split(",")(1), Convert.ToDateTime(data.Split(",")(0)).ToString("yyyy/MM/dd HH:mm:ss"))
                    Return msg.ToString

                Else
                    Return String.Empty
                End If

            Else



                If data <> "," Then

                    '�Z�����擾
                    dataBaseUpdTime = data.Split(",")(0)
                    dataBaseUpdUser = data.Split(",")(1)

                    '���̒[�� �ō폜
                    If dataBaseUpdTime = String.Empty Then
                        If updateDate <> String.Empty Then
                            Dim msg As New System.Text.StringBuilder
                            msg.AppendFormat(Messages.Instance.MSG2009E)
                            Return msg.ToString

                        Else
                            Return String.Empty

                        End If
                    End If
                    '���̒[���ōX�V
                    If updateDate < dataBaseUpdTime Then
                        Dim msg As New System.Text.StringBuilder
                        msg.AppendFormat(Messages.Instance.MSG003E, dataBaseUpdUser, Format(Convert.ToDateTime(dataBaseUpdTime), "yyyy/MM/dd HH:mm:ss"))
                        Return msg.ToString

                    Else
                        Return String.Empty

                    End If

                Else
                    Dim msg As New System.Text.StringBuilder
                    msg.AppendFormat(Messages.Instance.MSG2009E)
                    Return msg.ToString


                End If
            End If
        End Function

        ''' <summary>
        ''' �����X���ނ������X�Z���`�F�b�N
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CheckKameitenJyushoAru(ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer) As Boolean
            Return KihonJyouhouInquiryDA.SelKameitenJyushoAru(kameiten_cd, jyuusyo_no)
        End Function

        ''' <summary>
        ''' �����X�Z������ݒ�
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="jyuusyo_no">�Z��No</param>
        ''' <param name="data">�f�[�^</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetKameitenJyushoInfo(ByVal kameiten_cd As String, _
                                                            ByVal jyuusyo_no As Integer, _
                                                            ByVal data As KameitenjyushoDataSet) As Boolean

            Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
                Try

            If CheckKameitenJyushoAru(kameiten_cd, jyuusyo_no) Then

                ' DataSet�C���X�^���X�̐���
                Dim Kameitenjyusho As New KameitenjyushoDataSet
                Kameitenjyusho = KihonJyouhouInquiryDA.SelKameitenJyushoInfo(kameiten_cd)

                '�o�^�Z��
                If jyuusyo_no = 1 Then

                    '������
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("seikyuusyo_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).seikyuusyo_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).seikyuusyo_flg = -1
                            End If

                    '�ۏ؏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("hosyousyo_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).hosyousyo_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).hosyousyo_flg = -1
                            End If

                    '�񍐏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("hkks_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).hkks_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).hkks_flg = -1
                            End If

                    '������s
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("teiki_kankou_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).teiki_kankou_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).teiki_kankou_flg = -1
                            End If

                    '���r�ۏ؏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("kasi_hosyousyo_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg = -1
                            End If

                    '�H���񍐏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("koj_hkks_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).koj_hkks_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).koj_hkks_flg = -1
                            End If

                    '�H���񍐏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("kensa_hkks_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).kensa_hkks_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).kensa_hkks_flg = -1
                            End If

                    KihonJyouhouInquiryDA.updKameitenjyusho(kameiten_cd, jyuusyo_no, data)

                Else
                    '�Z��1~4
                    If data.m_kameiten_jyuusyo(0).seikyuusyo_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 1, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("seikyuusyo_flg='-1' and jyuusyo_no<>" & jyuusyo_no & " ").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 1, -1)
                                End If

                    End If

                    If data.m_kameiten_jyuusyo(0).hosyousyo_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 2, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("hosyousyo_flg='-1' and jyuusyo_no<>" & jyuusyo_no & "").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 2, -1)
                                End If
                    End If

                    If data.m_kameiten_jyuusyo(0).hkks_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 3, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("hkks_flg='-1' and jyuusyo_no<>" & jyuusyo_no & "").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 3, -1)
                                End If
                    End If

                    If data.m_kameiten_jyuusyo(0).teiki_kankou_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 4, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("teiki_kankou_flg='-1' and jyuusyo_no<>" & jyuusyo_no & "").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 4, -1)
                                End If
                    End If

                    If data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 5, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("kasi_hosyousyo_flg='-1' and jyuusyo_no<>" & jyuusyo_no & "").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 5, -1)
                                End If

                    End If

                    If data.m_kameiten_jyuusyo(0).koj_hkks_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 6, 0)
                    Else

                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("koj_hkks_flg='-1' and jyuusyo_no<>" & jyuusyo_no & "").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 6, -1)
                                End If

                    End If

                    If data.m_kameiten_jyuusyo(0).kensa_hkks_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 7, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("kensa_hkks_flg='-1' and jyuusyo_no<>" & jyuusyo_no & "").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 7, -1)
                                End If
                    End If


                    KihonJyouhouInquiryDA.updKameitenjyusho(kameiten_cd, jyuusyo_no, data)
                End If


            Else

                ' DataSet�C���X�^���X�̐���
                Dim Kameitenjyusho As New KameitenjyushoDataSet
                Kameitenjyusho = KihonJyouhouInquiryDA.SelKameitenJyushoInfo(kameiten_cd)

                '�o�^�Z��
                If jyuusyo_no = 1 Then

                    '������
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("seikyuusyo_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).seikyuusyo_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).seikyuusyo_flg = -1
                            End If

                    '�ۏ؏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("hosyousyo_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).hosyousyo_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).hosyousyo_flg = -1
                            End If

                    '�񍐏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("hkks_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).hkks_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).hkks_flg = -1
                            End If

                    '������s
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("teiki_kankou_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).teiki_kankou_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).teiki_kankou_flg = -1
                            End If

                    '���r�ۏ؏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("kasi_hosyousyo_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg = -1
                            End If

                    '�H���񍐏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("koj_hkks_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).koj_hkks_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).koj_hkks_flg = -1
                            End If

                    '�H���񍐏�
                            If Kameitenjyusho.m_kameiten_jyuusyo.Select("kensa_hkks_flg<>'0' and jyuusyo_no>=2").Length > 0 Then
                                data.m_kameiten_jyuusyo(0).kensa_hkks_flg = 0
                            Else
                                data.m_kameiten_jyuusyo(0).kensa_hkks_flg = -1
                            End If

                    KihonJyouhouInquiryDA.InsKameitenjyusho(kameiten_cd, jyuusyo_no, data)

                Else
                    '�Z��1~4
                    '�Z��1~4
                    If data.m_kameiten_jyuusyo(0).seikyuusyo_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 1, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("seikyuusyo_flg='-1'").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 1, -1)
                                End If

                    End If

                    If data.m_kameiten_jyuusyo(0).hosyousyo_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 2, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("hosyousyo_flg='-1'").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 2, -1)
                                End If
                    End If

                    If data.m_kameiten_jyuusyo(0).hkks_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 3, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("hkks_flg='-1'").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 3, -1)
                                End If
                    End If

                    If data.m_kameiten_jyuusyo(0).teiki_kankou_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 4, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("teiki_kankou_flg='-1'").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 4, -1)
                                End If
                    End If

                    If data.m_kameiten_jyuusyo(0).kasi_hosyousyo_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 5, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("kasi_hosyousyo_flg='-1'").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 5, -1)
                                End If

                    End If

                    If data.m_kameiten_jyuusyo(0).koj_hkks_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 6, 0)
                    Else

                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("koj_hkks_flg='-1'").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 6, -1)
                                End If

                    End If

                    If data.m_kameiten_jyuusyo(0).kensa_hkks_flg = -1 Then
                                KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 7, 0)
                    Else
                                If Kameitenjyusho.m_kameiten_jyuusyo.Select("kensa_hkks_flg='-1'").Length > 0 Then
                                Else
                                    KihonJyouhouInquiryDA.updKameitenjyushoFlg(kameiten_cd, data.m_kameiten_jyuusyo(0).upd_login_user_id, 7, -1)
                                End If
                    End If

                    KihonJyouhouInquiryDA.InsKameitenjyusho(kameiten_cd, jyuusyo_no, data)

                End If

            End If
                    scope.Complete()

                Catch ex As Exception

                    scope.Dispose()
                    Return False
                End Try
            End Using

            Return True
        End Function

        ''' <summary>
        ''' �����X�Z�������폜
        ''' </summary>
        ''' <param name="kameiten_cd"></param>
        ''' <param name="jyuusyo_no"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Public Function DelKameitenjyousyo(ByVal kameiten_cd As String, ByVal jyuusyo_no As Integer, ByVal upd_login_user_id As String) As Integer

            Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)


                Try

                    ' DataSet�C���X�^���X�̐���
                    Dim Kameitenjyusho As New KameitenjyushoDataSet
                    Kameitenjyusho = KihonJyouhouInquiryDA.SelKameitenJyushoInfo(kameiten_cd)

                    If Kameitenjyusho.m_kameiten_jyuusyo.Select("seikyuusyo_flg='-1' and jyuusyo_no=" & jyuusyo_no & "").Length > 0 Then

                        KihonJyouhouInquiryDA.updKameitenjyushoFlg2(kameiten_cd, upd_login_user_id, 1, 1)

                    End If

                    '�ۏ؏�
                    If Kameitenjyusho.m_kameiten_jyuusyo.Select("hosyousyo_flg='-1' and jyuusyo_no=" & jyuusyo_no & "").Length > 0 Then
                        KihonJyouhouInquiryDA.updKameitenjyushoFlg2(kameiten_cd, upd_login_user_id, 2, 1)
                    End If

                    '�񍐏�
                    If Kameitenjyusho.m_kameiten_jyuusyo.Select("hkks_flg='-1' and jyuusyo_no=" & jyuusyo_no & "").Length > 0 Then
                        KihonJyouhouInquiryDA.updKameitenjyushoFlg2(kameiten_cd, upd_login_user_id, 3, 1)
                    End If

                    '������s
                    If Kameitenjyusho.m_kameiten_jyuusyo.Select("teiki_kankou_flg='-1' and jyuusyo_no=" & jyuusyo_no & "").Length > 0 Then
                        KihonJyouhouInquiryDA.updKameitenjyushoFlg2(kameiten_cd, upd_login_user_id, 4, jyuusyo_no)
                    End If

                    '���r�ۏ؏�
                    If Kameitenjyusho.m_kameiten_jyuusyo.Select("kasi_hosyousyo_flg='-1' and jyuusyo_no=" & jyuusyo_no & "").Length > 0 Then
                        KihonJyouhouInquiryDA.updKameitenjyushoFlg2(kameiten_cd, upd_login_user_id, 5, 1)
                    End If

                    '�H���񍐏�
                    If Kameitenjyusho.m_kameiten_jyuusyo.Select("koj_hkks_flg='-1' and jyuusyo_no=" & jyuusyo_no & "").Length > 0 Then
                        KihonJyouhouInquiryDA.updKameitenjyushoFlg2(kameiten_cd, upd_login_user_id, 6, 1)
                    End If

                    '�H���񍐏�
                    If Kameitenjyusho.m_kameiten_jyuusyo.Select("kensa_hkks_flg='-1' and jyuusyo_no=" & jyuusyo_no & "").Length > 0 Then
                          KihonJyouhouInquiryDA.updKameitenjyushoFlg2(kameiten_cd, upd_login_user_id, 7, jyuusyo_no)
                    End If

                    If KihonJyouhouInquiryDA.DelKameitenjyusho(kameiten_cd, jyuusyo_no, upd_login_user_id) = 1 Then
                        scope.Complete()
                        Return 1
                    Else
                        Return 0
                    End If


                Catch ex As Exception
                    scope.Dispose()
                    Return -1
                End Try

            End Using

            Return True
        End Function

        ''' <summary>
        ''' �����X�}�X�g���ߓ��擾
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="kbn">�敪</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSimeDate(ByVal kameiten_cd As String, _
                                                  ByVal kbn As String) As String
            Return KihonJyouhouInquiryDA.SelSimeDate(kameiten_cd, kbn)
        End Function

        ''' <summary>
        ''' �e�����}�X�^����{��(TH)�������i����ъ|�����擾
        ''' </summary>
        ''' <param name="item">item</param>
        ''' <param name="table">table</param>
        ''' <param name="key">key</param>
        ''' <param name="shouhin">���i</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GelKakaku(ByVal item As String, _
                                                    ByVal table As String, _
                                                    ByVal key As String, _
                                                    ByVal shouhin As String, _
                                                    ByVal kbn As String, _
                                                    ByVal jitu As Long, _
                                                    ByVal koumuten As Long) As String

            Return KihonJyouhouInquiryDA.SelKakaku(item, table, key, shouhin, kbn, jitu, koumuten)

        End Function

        ''' <summary>
        ''' ����Ń}�X�^���擾����
        ''' </summary>
        ''' <param name="zei_kbn">�ŋ敪</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetKakaku(ByVal zei_kbn As String) As String
            Return KihonJyouhouInquiryDA.SelKakaku(zei_kbn)
        End Function

        ''' <summary>
        ''' ���i���擾
        ''' </summary>
        ''' <param name="syouhin_cd">���iCD</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSyouhin(ByVal syouhin_cd As String, ByVal souko_cd As String) As KameitenjyushoDataSet.m_syouhinDataTable
            Return KihonJyouhouInquiryDA.SelSyouhin(syouhin_cd, souko_cd)
        End Function

        ''' <summary>
        ''' ���i���擾
        ''' </summary>
        ''' <param name="syouhin_cd">���iCD</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSyouhin(ByVal syouhin_cd As String) As KameitenjyushoDataSet.m_syouhinDataTable
            Return KihonJyouhouInquiryDA.SelSyouhin(syouhin_cd)
        End Function

        ''' <summary>
        ''' �o�^�����o�^
        ''' </summary>
        ''' <param name="kameiten_cd"></param>
        ''' <param name="insData"></param>
        ''' <param name="bunrui_cd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetTenbetuSyokiSeikyuu(ByVal kameiten_cd As String, _
                        ByVal insData As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable, _
                        ByVal bunrui_cd As String) As Boolean

            Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
                Try
                    KihonJyouhouInquiryDA.InsTenbetuSyokiSeikyuu(kameiten_cd, insData, bunrui_cd)
                    scope.Complete()
                    Return True
                Catch ex As Exception
                    scope.Dispose()
                    Return False
                End Try

            End Using

        End Function

        ''' <summary>
        ''' �o�^�����o�^
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetTenbetuSyokiSeikyuu(ByVal kameiten_cd As String, _
                        ByVal insData1 As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable, _
                        ByVal insData2 As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable) As Boolean

            Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)

                Try

                    KihonJyouhouInquiryDA.InsTenbetuSyokiSeikyuu(kameiten_cd, insData1, "200")
                    KihonJyouhouInquiryDA.InsTenbetuSyokiSeikyuu(kameiten_cd, insData2, "210")

                    scope.Complete()
                    Return True
                Catch ex As Exception
                    scope.Dispose()
                    Return False
                End Try

            End Using


        End Function

        ''' <summary>
        ''' �X�ʏ����������擾
        ''' </summary>
        ''' <param name="kameiten_cd"></param>
        ''' <param name="bunrui_cd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTenbetuSyokiSeikyu(ByVal kameiten_cd As String, ByVal bunrui_cd As String) As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable

            Return KihonJyouhouInquiryDA.SelTenbetuSyokiSeikyu(kameiten_cd, bunrui_cd)

        End Function

        ''' <summary>
        ''' ���̒[���ōX�V�`�F�b�N
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="updateDate">�X�V����</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ChkTourokuryouTouroku(ByVal kameiten_cd As String, ByVal kbn As String, ByVal updateDate As String) As String

            Dim dataBaseUpdTime As String = String.Empty
            Dim dataBaseUpdUser As String = String.Empty
            Dim data As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable
            data = GetTenbetuSyokiSeikyu(kameiten_cd, kbn)

            If data.Rows.Count > 0 Then
                '�Z�����擾
                dataBaseUpdTime = Convert.ToDateTime(data(0).upd_datetime).ToString("yyyy/MM/dd HH:mm:ss")
                dataBaseUpdUser = data(0).upd_login_user_id
            End If

            '���̒[�� �ō폜
            If dataBaseUpdTime = String.Empty Then
                If updateDate <> String.Empty Then
                    Dim msg As New System.Text.StringBuilder
                    msg.AppendFormat(Messages.Instance.MSG2009E)
                    Return msg.ToString
                Else
                    Return String.Empty
                End If

            End If

            '���̒[���ōX�V
            If updateDate < dataBaseUpdTime Then
                Dim msg As New System.Text.StringBuilder
                msg.AppendFormat(Messages.Instance.MSG003E, dataBaseUpdUser, Format(Convert.ToDateTime(dataBaseUpdTime), "yyyy/MM/dd HH:mm:ss"))
                Return msg.ToString

            Else

                Return String.Empty
            End If
        End Function

        ''' <summary>
        ''' �����X���l���擾
        ''' </summary>
        ''' <param name="kameiten_cd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetkameitenBikouInfo(ByVal kameiten_cd As String) As KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable
            Return KihonJyouhouInquiryDA.SelkameitenBikouInfo(kameiten_cd)
        End Function

        ''' <summary>
        ''' �����X���l���擾
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="bikou_syubetu">���l���</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetkameitenBikouInfo(ByVal kameiten_cd As String, ByVal bikou_syubetu As String) As Boolean
            Return KihonJyouhouInquiryDA.SelkameitenBikouInfo(kameiten_cd, bikou_syubetu)
        End Function

        ''' <summary>
        ''' �����X���l�X�V���擾
        ''' </summary>
        ''' <param name="kameiten_cd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetkameitenBikouMaxUpdInfo(ByVal kameiten_cd As String) As String
            Return KihonJyouhouInquiryDA.SelkameitenBikouMaxUpdInfo(kameiten_cd)
        End Function
        ''' <summary>
        ''' �����X���lcount�擾
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SelkameitenBikouInfoCount(ByVal kameiten_cd As String) As Integer
            Return KihonJyouhouInquiryDA.SelkameitenBikouInfoCount(kameiten_cd)
        End Function
        ''' <summary>
        ''' ���l�ݒ�
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="bikou_syubetu">���l���</param>
        ''' <param name="nyuuryoku_no">����No.</param>
        ''' <param name="insData">�ǉ��f�[�^</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetBikou(ByVal kameiten_cd As String, ByVal bikou_syubetu As String, ByVal nyuuryoku_no As String, ByVal insData As KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable) As Integer
            Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
                Try
                    KihonJyouhouInquiryDA.updBikou(kameiten_cd, bikou_syubetu, nyuuryoku_no, insData)
                    scope.Complete()
                    Return True
                Catch ex As Exception
                    scope.Dispose()
                    Return False
                End Try

            End Using

        End Function

        ''' <summary>
        ''' �����X���l���擾
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="bikou_syubetu">���l���</param>
        ''' <param name="nyuuryoku_no">����No.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SelkameitenBikouInfo(ByVal kameiten_cd As String, ByVal bikou_syubetu As String, ByVal nyuuryoku_no As String) As String
            Return KihonJyouhouInquiryDA.SelkameitenBikouInfo(kameiten_cd, bikou_syubetu, nyuuryoku_no)
        End Function

        ''' <summary>
        ''' ���l���폜
        ''' </summary>
        ''' <param name="kameiten_cd">�����X�R�[�h</param>
        ''' <param name="bikou_syubetu">���l���</param>
        ''' <param name="nyuuryoku_no">����No.</param>
        ''' <param name="upd_login_user_id">�X�V��</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DelBikou(ByVal kameiten_cd As String, ByVal bikou_syubetu As String, ByVal nyuuryoku_no As String, ByVal upd_login_user_id As String) As Boolean

            Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)

                Try
                    KihonJyouhouInquiryDA.DelBikou(kameiten_cd, bikou_syubetu, nyuuryoku_no, upd_login_user_id)
                    scope.Complete()
                    Return True
                Catch ex As Exception
                    scope.Dispose()
                    Return False
                End Try

            End Using

        End Function

        ''' <summary>
        ''' ���l�ǉ�
        ''' </summary>
        ''' <param name="insData">�ǉ��f�[�^</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InsBikou(ByVal insData As KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable) As Boolean

            Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
                Try
                    KihonJyouhouInquiryDA.InsBikou(insData)
                    scope.Complete()
                    Return True
                Catch ex As Exception
                    scope.Dispose()
                    Return False
                End Try

            End Using
        End Function

        ''' <summary>
        ''' �n��CD�@�擾
        ''' </summary>
        ''' <param name="kameiten_cd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetkeiretuCd(ByVal kameiten_cd As String _
                                          ) As String
            Return KihonJyouhouInquiryDA.SelkeiretuCd(kameiten_cd)
        End Function

        ''' <summary>
        ''' �����X��ʎ擾
        ''' </summary>
        ''' <param name="code">�R�[�h</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Getkameitensyubetu(ByVal code As Integer) As String
            Return KihonJyouhouInquiryDA.Selkameitensyubetu(code)
        End Function


        ''' <summary>
        ''' �����X�c�Ə����擾
        ''' </summary>
        ''' <param name="eigyousyo">�c�Ə�</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetEigyousyo(ByVal eigyousyo As String) As KameitenDataSet.eigyousyoTableDataTable
            Return KihonJyouhouInquiryDA.SelEigyousyo(eigyousyo)
        End Function

    End Class

    ''' <summary>
    ''' MESSAGE�̕\��Class
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MessageAndFocus

        Private _msgParam As New System.Text.StringBuilder
        Private _controlParam As New List(Of System.Web.UI.Control)

        ''' <summary>
        ''' MESSAGE�ǉ�
        ''' </summary>
        ''' <param name="appendValue">MESSAGE</param>
        ''' <remarks></remarks>
        Public Sub Append(ByVal appendValue As String)
            _msgParam.Append(appendValue)
        End Sub

        ''' <summary>
        ''' MESSAGE�ǉ�
        ''' </summary>
        ''' <param name="msg">MESSAGE</param>
        ''' <remarks></remarks>
        ''' <param name="param1">param</param>
        ''' <param name="param2">param</param>
        ''' <param name="param3">param</param>
        ''' <param name="param4">param</param>
        Public Sub AppendFormat(ByVal msg As String, _
                                    Optional ByVal param1 As String = "", _
                                    Optional ByVal param2 As String = "", _
                                    Optional ByVal param3 As String = "", _
                                    Optional ByVal param4 As String = "")

            msg = msg.Replace("@PARAM1", param1) _
                          .Replace("@PARAM2", param2) _
                          .Replace("@PARAM3", param3) _
                          .Replace("@PARAM4", param4)
            _msgParam.AppendFormat( _
                                            "" & msg & "", _
                                            param1, param2, param3, param4)

        End Sub

        ''' <summary>
        ''' control�ǉ�
        ''' </summary>
        ''' <param name="control"></param>
        ''' <remarks></remarks>
        Public Sub AppendFocusCtrl(ByVal control As System.Web.UI.Control)
            If _controlParam.Count = 0 Then
                _controlParam.Add(control)
            End If
        End Sub

        ''' <summary>
        ''' MESSAGE�ǉ�
        ''' </summary>
        ''' <param name="msg">MESSAGE</param>
        ''' <param name="control">control</param>
        ''' <param name="param1">param</param>
        ''' <param name="param2">param</param>
        ''' <param name="param3">param</param>
        ''' <param name="param4">param</param>
        Public Sub AppendMsgAndCtrl(ByVal control As System.Web.UI.Control, _
                                                                    ByVal msg As String, _
                                                                    Optional ByVal param1 As String = "", _
                                                                    Optional ByVal param2 As String = "", _
                                                                    Optional ByVal param3 As String = "", _
                                                                    Optional ByVal param4 As String = "")
            msg = msg.Replace("@PARAM1", param1) _
                              .Replace("@PARAM2", param2) _
                              .Replace("@PARAM3", param3) _
                              .Replace("@PARAM4", param4)

            _msgParam.AppendFormat( _
                                            "" & msg & "", _
                                            param1, param2, param3, param4)

            If _controlParam.Count = 0 Then
                _controlParam.Add(control)
            End If


        End Sub

        ''' <summary>
        ''' Message
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Message() As String
            Get
                Return _msgParam.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Control
        ''' </summary>
        ''' <value>value</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property focusCtrl() As System.Web.UI.Control
            Get
                If _controlParam.Count > 0 Then
                    Return _controlParam(0)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Sub setFocus(ByVal page As System.Web.UI.Page, ByVal control As System.Web.UI.Control)
            Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(page)
            Scriptmangaer1.SetFocus(control)
        End Sub

    End Class

    ''' <summary>
    ''' Do�@Other Page Class
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OtherPageFunction

        ''' <summary>
        ''' Run Function
        ''' </summary>
        ''' <param name="page">Page��</param>
        ''' <param name="fncName">function��</param>
        ''' <returns>Object</returns>
        ''' <remarks></remarks>
        Public Function DoFunction(ByVal page As System.Web.UI.Page, ByVal fncName As String) As Object
            Dim pType As Type = page.GetType
            Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod(fncName)
            If Not methodInfo Is Nothing Then
                Return methodInfo.Invoke(page, New Object() {})
            Else
                Return Nothing
            End If
        End Function

    End Class

End Namespace

