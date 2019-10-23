Imports System.text
Imports System.Data.SqlClient

Public Class EigyousyoSearchDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "�c�Ə��}�X�^����"
    ''' <summary>
    ''' �c�Ə��}�X�^�̌������s��
    ''' </summary>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoKana">�c�Ə��J�i</param>
    ''' <param name="blnDelete">����Ώۃt���O</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEigyosyoKensakuData(ByVal strEigyousyoCd As String, _
                                           ByVal strEigyousyoKana As String, _
                                           ByVal blnDelete As Boolean) As EigyousyoSearchDataSet.EigyousyoSearchTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetEigyosyoKensakuData", _
                                            strEigyousyoCd, _
                                            strEigyousyoKana, _
                                            blnDelete)

        ' �p�����[�^
        Const strParamEigyousyoCd As String = "@EIGYOUSYOCD"
        Const strParamEigyousyoKana As String = "@EIGYOUSYOKANA"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT ")
        commandTextSb.Append("  eigyousyo_cd ")
        commandTextSb.Append("  ,torikesi ")
        commandTextSb.Append("  ,eigyousyo_mei ")
        commandTextSb.Append("  ,eigyousyo_kana ")
        commandTextSb.Append("  ,seikyuu_saki_cd ")
        commandTextSb.Append("  ,seikyuu_saki_brc ")
        commandTextSb.Append("  ,seikyuu_saki_kbn ")
        commandTextSb.Append("  ,seikyuu_saki_mei ")
        commandTextSb.Append("  ,seikyuu_saki_kana ")
        commandTextSb.Append(" FROM m_eigyousyo WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE 0 = 0 ")
        If strEigyousyoCd <> "" Then
            commandTextSb.Append(" AND eigyousyo_cd like " & strParamEigyousyoCd)
        End If
        If strEigyousyoKana <> "" Then
            commandTextSb.Append(" AND eigyousyo_kana Like " & strParamEigyousyoKana)
        End If
        If blnDelete Then
            commandTextSb.Append(" AND torikesi = 0 ")
        End If

        '' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamEigyousyoCd, SqlDbType.VarChar, 6, strEigyousyoCd & Chr(37)), _
             SQLHelper.MakeParam(strParamEigyousyoKana, SqlDbType.VarChar, 22, Chr(37) & strEigyousyoKana & Chr(37)) _
             }

        '' �f�[�^�̎擾
        Dim EigyousyoDataSet As New EigyousyoSearchDataSet()

        '' �������s
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            EigyousyoDataSet, EigyousyoDataSet.EigyousyoSearchTable.TableName, commandParameters)

        Dim eigyousyoTable As EigyousyoSearchDataSet.EigyousyoSearchTableDataTable = _
                    EigyousyoDataSet.EigyousyoSearchTable

        Return eigyousyoTable

    End Function
#End Region


End Class
