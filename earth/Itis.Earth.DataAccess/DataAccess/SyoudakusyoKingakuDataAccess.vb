Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' ���������z�̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class SyoudakusyoKingakuDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ���������z���擾���܂�
    ''' </summary>
    ''' <param name="strTyousakaisyaCd">������к���+���Ə�����</param>
    ''' <param name="strKameitenCd">�����X����</param>
    ''' <param name="intDoujiIraiSuu">�����˗�����</param>
    ''' <param name="intSyoudakuKingaku">���������z</param>
    ''' <returns>True:�擾�����CFalse:�擾���s</returns>
    ''' <remarks>�d�����i�}�X�^��艿�i���擾�ł��Ȃ��ꍇ�A<br/>
    '''          ������Ѓ}�X�^�̕W�����i���擾����</remarks>
    Public Function GetSyoudakuKingaku(ByVal strTyousakaisyaCd As String, _
                                       ByVal strKameitenCd As String, _
                                       ByVal intDoujiIraiSuu As Integer, _
                                       ByRef intSyoudakuKingaku As Integer) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyoudakuKingaku", _
                                                    strTyousakaisyaCd, _
                                                    strKameitenCd, _
                                                    intDoujiIraiSuu, _
                                                    intSyoudakuKingaku)
        ' �p�����[�^
        Const paramTyousakaisyaCd As String = "@TYOUSAGAIYOU"
        Const paramKameitenCd As String = "@KAMEITEN"

        Dim commandTextSb As New StringBuilder()
        Dim itemName As String = ""

        ' �����˗������ɂ��擾���ڂ̐ݒ�
        Select Case intDoujiIraiSuu
            Case 1 To 3
                itemName = "siire_kkk1"
            Case 4 To 9
                itemName = "siire_kkk2"
            Case 10 To 19
                itemName = "siire_kkk3"
            Case Else
                Return False
        End Select

        commandTextSb.Append(String.Format("SELECT {0} AS siire_kkk FROM m_siire_kakaku WITH (READCOMMITTED) ", itemName))
        commandTextSb.Append("  WHERE RTRIM(tys_kaisya_cd) + RTRIM(jigyousyo_cd) = " & paramTyousakaisyaCd)
        commandTextSb.Append("  AND kameiten_cd = " & paramKameitenCd)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramTyousakaisyaCd, SqlDbType.VarChar, 7, strTyousakaisyaCd), _
             SQLHelper.MakeParam(paramKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' �f�[�^�̎擾
        Dim syoudakusyoKingakuDataSet As New SyoudakusyoKingakuDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            syoudakusyoKingakuDataSet, syoudakusyoKingakuDataSet.SyoudakusyoKingakuTable.TableName, commandParameters)

        Dim syoudakusyoTable As SyoudakusyoKingakuDataSet.SyoudakusyoKingakuTableDataTable = syoudakusyoKingakuDataSet.SyoudakusyoKingakuTable

        If syoudakusyoTable.Count = 0 Then

            'TBL_M������ЁDSS����i���擾
            If GetKijunKakaku(strTyousakaisyaCd, intSyoudakuKingaku) = False Then
                ' �擾�Ɏ��s�����ꍇ�AFalse��ԋp
                Return False
            End If
        Else
            Dim row As SyoudakusyoKingakuDataSet.SyoudakusyoKingakuTableRow = syoudakusyoTable(0)

            If row.siire_kkk = Nothing Then

                'TBL_M������ЁDSS����i���擾
                If GetKijunKakaku(strTyousakaisyaCd, intSyoudakuKingaku) = False Then
                    ' �擾�Ɏ��s�����ꍇ�AFalse��ԋp
                    Return False
                End If
            Else
                intSyoudakuKingaku = Integer.Parse(row.siire_kkk)
            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^���SS����i���擾����
    ''' </summary>
    ''' <param name="strTyousakaisyaCd">������к���+���Ə�����</param>
    ''' <param name="intSyoudakuKingaku">���������z</param>
    ''' <returns>True:�擾�����CFalse:�擾���s</returns>
    ''' <remarks></remarks>
    Public Function GetKijunKakaku(ByVal strTyousakaisyaCd As String, _
                                   ByRef intSyoudakuKingaku As Integer) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKijunKakaku", _
                                                    strTyousakaisyaCd, _
                                                    intSyoudakuKingaku)

        ' �p�����[�^
        Const paramTyousaGaiyouCd As String = "@TYOUSAGAISYA"

        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT ss_kijyun_kkk AS siire_kkk FROM m_tyousakaisya WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE RTRIM(tys_kaisya_cd) + RTRIM(jigyousyo_cd) = " & paramTyousaGaiyouCd)
        commandTextSb.Append("  AND torikesi = 0 ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramTyousaGaiyouCd, SqlDbType.VarChar, 7, strTyousakaisyaCd)}

        ' �f�[�^�̎擾
        Dim syoudakusyoKingakuDataSet As New SyoudakusyoKingakuDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            syoudakusyoKingakuDataSet, syoudakusyoKingakuDataSet.SyoudakusyoKingakuTable.TableName, commandParameters)

        Dim syoudakusyoTable As SyoudakusyoKingakuDataSet.SyoudakusyoKingakuTableDataTable = syoudakusyoKingakuDataSet.SyoudakusyoKingakuTable

        If syoudakusyoTable.Count = 0 Then
            ' �擾�Ɏ��s�����ꍇ�AFalse��ԋp
            Return False
        Else
            Dim row As SyoudakusyoKingakuDataSet.SyoudakusyoKingakuTableRow = syoudakusyoTable(0)

            If row.siire_kkk = Nothing Then
                Return False
            Else
                intSyoudakuKingaku = Integer.Parse(row.siire_kkk)
            End If
        End If

        Return True
    End Function

End Class
