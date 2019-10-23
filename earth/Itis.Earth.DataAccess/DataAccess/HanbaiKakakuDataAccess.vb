Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �̔����i(�H���X�������z�E�������Ŕ����z)
''' </summary>
''' <remarks></remarks>
Public Class HanbaiKakakuDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim cmnDtAcc As New CmnDataAccess
    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �̔����i�}�X�^���̔����i�����擾���܂�
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">�������</param>
    ''' <param name="strAitesakiCd">�����R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="intTysHouhouNo">�������@NO</param>
    ''' <param name="intKmtnKingaku">�H���X�������z</param>
    ''' <param name="blnKmtnKingakuKahi">�H���X�������z�ύXFLG</param>
    ''' <param name="intJskuKingaku">�������Ŕ������z</param>
    ''' <param name="blnJskuKingakuKahi">�������Ŕ����z</param>
    ''' <param name="blnTysHouhou">�������@�����t���O</param>
    ''' <param name="blnTorikesi">����t���O</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHanbaiKakaku(ByVal intAitesakiSyubetu As Integer, _
                               ByVal strAitesakiCd As String, _
                               ByVal strSyouhinCd As String, _
                               ByVal intTysHouhouNo As Integer, _
                               ByRef intKmtnKingaku As Integer, _
                               ByRef blnKmtnKingakuKahi As Boolean, _
                               ByRef intJskuKingaku As Integer, _
                               ByRef blnJskuKingakuKahi As Boolean, _
                               ByVal blnTysHouhou As Boolean, _
                               Optional ByVal blnTorikesi As Boolean = False) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanbaiKakaku", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    intTysHouhouNo, _
                                                    intKmtnKingaku, _
                                                    blnKmtnKingakuKahi, _
                                                    intJskuKingaku, _
                                                    blnJskuKingakuKahi, _
                                                    blnTorikesi)
        '�p�����[�^
        Const paramAitesakiSyubetu As String = "@AITESAKISYUBETU"
        Const paramAitesakiCd As String = "@AITESAKICD"
        Const paramSyouhinCd As String = "@SYOUHINCD"
        Const paramTysHouhouNo As String = "@TYSHOUHOUNO"
        Dim strKoumuKakaku As String = "koumuten_seikyuu_gaku"
        Dim strKoumuKakakuKahi As String = "koumuten_seikyuu_gaku_henkou_flg"
        Dim strJituKakaku As String = "jitu_seikyuu_gaku"
        Dim strJituKakakuKahi As String = "jitu_seikyuu_gaku_henkou_flg"

        Dim cmdTextSb As New StringBuilder()
        Dim dt As New DataTable

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      MHK.aitesaki_syubetu ")
        cmdTextSb.AppendLine("    , MHK.aitesaki_cd ")
        cmdTextSb.AppendLine("    , MHK.syouhin_cd ")
        cmdTextSb.AppendLine("    , MHK.tys_houhou_no ")
        cmdTextSb.AppendLine("    , MHK.koumuten_seikyuu_gaku ")
        cmdTextSb.AppendLine("    , MHK.koumuten_seikyuu_gaku_henkou_flg ")
        cmdTextSb.AppendLine("    , MHK.jitu_seikyuu_gaku ")
        cmdTextSb.AppendLine("    , MHK.jitu_seikyuu_gaku_henkou_flg ")
        cmdTextSb.AppendLine("    , MHK.koukai_flg ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_hanbai_kakaku MHK ")
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      MHK.aitesaki_syubetu = " & paramAitesakiSyubetu)
        cmdTextSb.AppendLine("  AND MHK.aitesaki_cd = " & paramAitesakiCd)
        cmdTextSb.AppendLine("  AND MHK.syouhin_cd = " & paramSyouhinCd)
        '���iM.�����L���敪�Ɖc�Ə��ɂ�錟���ɂ��A�������@NO��؂蕪����
        If blnTysHouhou Then
            cmdTextSb.AppendLine("  AND MHK.tys_houhou_no = " & paramTysHouhouNo)
        Else
            cmdTextSb.AppendLine("  AND MHK.tys_houhou_no = 0 ")
        End If
        '����t���O
        If blnTorikesi Then
            cmdTextSb.AppendLine("  AND MHK.torikesi = 0 ")
        End If

        ' �p�����[�^�֐ݒ�
        Dim sqlParams() As SqlParameter = {SQLHelper.MakeParam(paramAitesakiSyubetu, SqlDbType.Int, 4, intAitesakiSyubetu), _
                                           SQLHelper.MakeParam(paramAitesakiCd, SqlDbType.VarChar, 5, strAitesakiCd), _
                                           SQLHelper.MakeParam(paramSyouhinCd, SqlDbType.VarChar, 8, strSyouhinCd), _
                                           SQLHelper.MakeParam(paramTysHouhouNo, SqlDbType.Int, 4, intTysHouhouNo)}

        '�f�[�^�擾
        dt = cmnDtAcc.getDataTable(cmdTextSb.ToString, sqlParams)

        If dt.Rows.Count > 0 Then

            '���H���X�������z
            If Not IsDBNull(dt.Rows(0)(strKoumuKakaku)) Then
                intKmtnKingaku = dt.Rows(0)(strKoumuKakaku).ToString
            Else
                intKmtnKingaku = 0
            End If
            If IsDBNull(dt.Rows(0)(strKoumuKakakuKahi)) OrElse dt.Rows(0)(strKoumuKakakuKahi).ToString = 0 Then
                '0orNull�̏ꍇ�A�ύX�s��
                blnKmtnKingakuKahi = False
            Else
                '1�ȏ�̏ꍇ�A�ύX��
                blnKmtnKingakuKahi = True
            End If

            '���������Ŕ����z
            If Not IsDBNull(dt.Rows(0)(strJituKakaku)) Then
                intJskuKingaku = dt.Rows(0)(strJituKakaku).ToString
            Else
                intJskuKingaku = 0
            End If
            If IsDBNull(dt.Rows(0)(strJituKakakuKahi)) OrElse dt.Rows(0)(strJituKakakuKahi).ToString = 0 Then
                '0orNull�̏ꍇ�A�ύX�s��
                blnJskuKingakuKahi = False
            Else
                '1�ȏ�̏ꍇ�A�ύX��
                blnJskuKingakuKahi = True
            End If

            Return True
        End If


        Return False
    End Function

End Class