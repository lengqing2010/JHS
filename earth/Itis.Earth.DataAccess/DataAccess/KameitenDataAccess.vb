Imports System.text
Imports System.Data.SqlClient

Public Class KameitenDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "�����X�}�X�^���i�擾"
    ''' <summary>
    ''' �����X�}�X�^��艿�i���擾���܂�
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="intTatemonoYouto">�����p�rNO</param>
    ''' <param name="isYoutoAdd">�����p�r�ɂ����Z�Ώ�(True:���Z����)</param>
    ''' <param name="strItem">�擾�Ώۂ̃J������</param>
    ''' <param name="blnDelete">True:����f�[�^���擾�Ώۂ��珜�O False:����f�[�^��Ώ�</param>
    ''' <param name="kameitenKakaku">��������</param>
    ''' <returns>True:�擾���� False:�擾���s</returns>
    ''' <remarks></remarks>
    Public Function GetKameitenKakaku(ByVal strKubun As String, _
                                      ByVal strKameitenCd As String, _
                                      ByVal intTatemonoYouto As Integer, _
                                      ByVal isYoutoAdd As Boolean, _
                                      ByVal strItem As String, _
                                      ByVal blnDelete As Boolean, _
                                      ByRef kameitenKakaku As Decimal) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenKakaku", _
                                                    strKubun, _
                                                    strKameitenCd, _
                                                    intTatemonoYouto, _
                                                    isYoutoAdd, _
                                                    strItem, _
                                                    blnDelete, _
                                                    kameitenKakaku)

        ' �p�����[�^
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKubun As String = "@KUBUN"

        Dim youto As String = ""
        Dim commandTextSb As New StringBuilder()

        ' �����p�r��2�`9�̏ꍇ�A���z�ɉ��Z�z�����Z����(�A�����i�R�[�h��A1001,A1003,A1004�̏ꍇ)
        If intTatemonoYouto >= 2 And intTatemonoYouto <= 9 And isYoutoAdd = True Then
            youto = " , ISNULL(kasangaku" & intTatemonoYouto.ToString() & ",0) AS tatemono_youto"
        Else
            youto = " , 0 AS tatemono_youto"
        End If

        commandTextSb.Append("SELECT kbn,K.kameiten_cd,ISNULL(")
        commandTextSb.Append(strItem & ",0) As kameiten_master_kakaku ")
        commandTextSb.Append(youto)
        commandTextSb.Append("  FROM m_kameiten K WITH (READCOMMITTED) ")
        ' �����p�r��2�`9�̏ꍇ�A���z�ɉ��Z�z�����Z����(�A�����i�R�[�h��A1001,A1003,A1004�̏ꍇ)
        If intTatemonoYouto >= 2 And intTatemonoYouto <= 9 And isYoutoAdd = True Then
            commandTextSb.Append(" LEFT OUTER JOIN m_tatemono_youto_kasangaku Y WITH (READCOMMITTED) ON K.kameiten_cd = Y.kameiten_cd ")
        End If
        commandTextSb.Append("  WHERE K.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append("  AND   kbn = " & strParamKubun)

        If blnDelete = True Then
            commandTextSb.Append("  AND torikesi = 0 ")
        End If

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.Char, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, strKubun)}

        ' �f�[�^�̎擾
        Dim kameitenDataSet As New KameitenKakakuDataSet()

        ' �������s
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kameitenDataSet, kameitenDataSet.KameitenMasterKakakuTable.TableName, commandParameters)

        Dim kameitenTable As KameitenKakakuDataSet.KameitenMasterKakakuTableDataTable = _
                    kameitenDataSet.KameitenMasterKakakuTable

        If kameitenTable.Count <> 0 Then
            ' �擾�ł����ꍇ�A�s�����擾���Q�ƃ��R�[�h�ɐݒ肷��
            Dim row As KameitenKakakuDataSet.KameitenMasterKakakuTableRow = kameitenTable(0)

            kameitenKakaku = row.kameiten_master_kakaku

            ' ���i���O�ȊO�̏ꍇ�A�����p�r�����Z����
            If kameitenKakaku <> 0 Then
                kameitenKakaku = kameitenKakaku + row.tatemono_youto
            End If

            Return True
        End If

        Return False

    End Function
#End Region

    ''' <summary>
    ''' �����X�}�X�^�̃f�t�H���g��������擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getKameitenDefaultSeikyuuSakiInfo(ByVal strKameitenCd As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getKameitenSeikyuuSakiInfo", _
                                                    strKameitenCd)
        Dim cmdTextSb As New StringBuilder()
        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      MK.kameiten_cd")
        cmdTextSb.Append("    , MK.kameiten_mei1")
        cmdTextSb.Append("    , MK.tenmei_kana1")
        cmdTextSb.Append("    , MK.tys_seikyuu_saki_cd")
        cmdTextSb.Append("    , MK.tys_seikyuu_saki_brc")
        cmdTextSb.Append("    , MK.tys_seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.tys_seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MK.tys_seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MK.tys_seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      tys_seikyuu_saki_mei")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           MSH.kameiten_cd")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_seikyuu_saki_henkou MSH")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.kameiten_cd = MSH.kameiten_cd")
        cmdTextSb.Append("       AND (substring(MSH.syouhin_kbn, 1, 1) = 't'")
        cmdTextSb.Append("        OR substring(MSH.syouhin_kbn, 1, 1) = 'T')")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      tys_henkou_umu")
        cmdTextSb.Append("    , MK.koj_seikyuu_saki_cd")
        cmdTextSb.Append("    , MK.koj_seikyuu_saki_brc")
        cmdTextSb.Append("    , MK.koj_seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.koj_seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MK.koj_seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MK.koj_seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      koj_seikyuu_saki_mei")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           MSH.kameiten_cd")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_seikyuu_saki_henkou MSH")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.kameiten_cd = MSH.kameiten_cd")
        cmdTextSb.Append("       AND (substring(MSH.syouhin_kbn, 1, 1) = 'k'")
        cmdTextSb.Append("        OR substring(MSH.syouhin_kbn, 1, 1) = 'K')")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      koj_henkou_umu")
        cmdTextSb.Append("    , MK.hansokuhin_seikyuu_saki_cd")
        cmdTextSb.Append("    , MK.hansokuhin_seikyuu_saki_brc")
        cmdTextSb.Append("    , MK.hansokuhin_seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.hansokuhin_seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MK.hansokuhin_seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MK.hansokuhin_seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      hansokuhin_seikyuu_saki_mei")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           MSH.kameiten_cd")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_seikyuu_saki_henkou MSH")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.kameiten_cd = MSH.kameiten_cd")
        cmdTextSb.Append("       AND (substring(MSH.syouhin_kbn, 1, 1) = 'h'")
        cmdTextSb.Append("        OR substring(MSH.syouhin_kbn, 1, 1) = 'H')")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      hansoku_henkou_umu")
        cmdTextSb.Append("    , MK.tatemono_seikyuu_saki_cd")
        cmdTextSb.Append("    , MK.tatemono_seikyuu_saki_brc")
        cmdTextSb.Append("    , MK.tatemono_seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.tatemono_seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MK.tatemono_seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MK.tatemono_seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      tatemono_seikyuu_saki_mei")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           MSH.kameiten_cd")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_seikyuu_saki_henkou MSH")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.kameiten_cd = MSH.kameiten_cd")
        cmdTextSb.Append("       AND (substring(MSH.syouhin_kbn, 1, 1) = 's'")
        cmdTextSb.Append("        OR substring(MSH.syouhin_kbn, 1, 1) = 'S')")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      tatemono_henkou_umu")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      jhs_sys.m_kameiten MK")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      MK.kameiten_cd = @KAMEITENCD")

        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KAMEITENCD", SqlDbType.Char, 5, strKameitenCd)}

        ' �f�[�^�̎擾���ԋp
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

End Class
