Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' ����R�n��̋��ʏ������s���e�N���X�ł� 
''' </summary>
''' <remarks></remarks>
Public Class KeiretuDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�ϐ���`"
    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Protected connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �R�}���h�e�L�X�g
    ''' </summary>
    ''' <remarks></remarks>
    Protected commandTextSb As New StringBuilder()

    ''' <summary>
    ''' �f�[�^�Z�b�g
    ''' </summary>
    ''' <remarks></remarks>
    Protected seikyuuKakakuDataSet As New SeikyuKingakuDataSet()

    ''' <summary>
    ''' �f�[�^�e�[�u����
    ''' </summary>
    ''' <remarks></remarks>
    Protected dataTableName As String
#End Region

    ''' <summary>
    ''' ��{�ƂȂ�SQL�̐ݒ���s���܂�
    ''' </summary>
    ''' <param name="strItem"></param>
    ''' <param name="intMode"></param>
    ''' <remarks></remarks>
    Protected Function SetBaseSQLData(ByVal intMode As Integer, _
                                      ByVal strItem As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetBaseSQLData", _
                                            intMode, _
                                            strItem)

        ' �擾���[�h�ɂ�鏈������
        Select Case intMode
            Case 1, 2, 6
                commandTextSb.Append(" SELECT ISNULL(kameiten_muke_kkk,0) AS kameiten_muke_kkk, ISNULL(kakeritu,0) AS kakeritu ")
                dataTableName = seikyuuKakakuDataSet.dt_KameitenMuke.TableName
            Case 3
                commandTextSb.Append(String.Format(" SELECT ISNULL({0},0) AS syutoku_kakaku ", strItem))
                dataTableName = seikyuuKakakuDataSet.dt_SyutokuKakaku.TableName
            Case 4, 5
                commandTextSb.Append(" SELECT ISNULL(kakeritu,0) AS kakeritu ")
                dataTableName = seikyuuKakakuDataSet.dt_Kakeritu.TableName
            Case Else
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' SQL�����s�����i��ݒ肵�܂�
    ''' </summary>
    ''' <param name="intMode">�擾���[�h<BR/>
    ''' 1 (���i�R�[�h1�̉����X�����i)   <BR/>
    ''' 2 (��񕥖߂̉����X�����i)      <BR/>
    ''' 3 (�{��(TH)�����i)              <BR/>
    ''' 4 (���������z / �|��)           <BR/>
    ''' 5 (�H���X�������z * �|��)       <BR/>
    ''' 6 (�����X�����i�����������z/�|��)</param>
    ''' <param name="intKingaku">TH�����p���i�}�X�^��KEY,�|���v�Z���̋��z<BR/>
    ''' �擾���[�h=1 (TH�����p���i�}�X�^��KEY)<BR/>
    ''' �擾���[�h=4 (���������z)<BR/>
    ''' �擾���[�h=5 (�H���X�������z)</param>
    ''' <param name="inrReturnKingaku">�擾���z�i�߂�l�j</param>
    ''' <returns>True:�擾OK,False:�擾NG</returns>
    ''' <remarks></remarks>
    Protected Function GetKingaku(ByVal intMode As Integer, _
                                  ByVal intKingaku As Integer, _
                                  ByRef inrReturnKingaku As Integer) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKingaku", _
                                    intMode, _
                                    intKingaku, _
                                    inrReturnKingaku)

        ' �擾���[�h�ɂ�鏈������
        Select Case intMode
            Case 1, 2
                Dim kameitenMukeTable As SeikyuKingakuDataSet.dt_KameitenMukeDataTable = _
                    seikyuuKakakuDataSet.dt_KameitenMuke

                If kameitenMukeTable.Count <> 0 Then
                    ' �擾�ł����ꍇ�A�s�����擾���Q�ƃ��R�[�h�ɐݒ肷��
                    Dim row As SeikyuKingakuDataSet.dt_KameitenMukeRow = kameitenMukeTable(0)
                    inrReturnKingaku = row.kameiten_muke_kkk
                Else
                    Return False
                End If
            Case 3
                Dim syutokuKakakuTable As SeikyuKingakuDataSet.dt_SyutokuKakakuDataTable = _
                    seikyuuKakakuDataSet.dt_SyutokuKakaku

                If syutokuKakakuTable.Count <> 0 Then
                    ' �擾�ł����ꍇ�A�s�����擾���Q�ƃ��R�[�h�ɐݒ肷��
                    Dim row As SeikyuKingakuDataSet.dt_SyutokuKakakuRow = syutokuKakakuTable(0)
                    inrReturnKingaku = row.syutoku_kakaku
                Else
                    Return False
                End If
            Case 4
                Dim kakerituTable As SeikyuKingakuDataSet.dt_KakerituDataTable = _
                    seikyuuKakakuDataSet.dt_Kakeritu

                If kakerituTable.Count <> 0 Then
                    ' �擾�ł����ꍇ�A�s�����擾���Q�ƃ��R�[�h�ɐݒ肷��
                    Dim row As SeikyuKingakuDataSet.dt_KakerituRow = kakerituTable(0)

                    If row.kakeritu <> 0 Then
                        inrReturnKingaku = Fix(intKingaku / row.kakeritu)
                    Else
                        inrReturnKingaku = 0
                    End If
                Else
                    Return False
                End If
            Case 5
                Dim kakerituTable As SeikyuKingakuDataSet.dt_KakerituDataTable = _
                    seikyuuKakakuDataSet.dt_Kakeritu

                If kakerituTable.Count <> 0 Then
                    ' �擾�ł����ꍇ�A�s�����擾���Q�ƃ��R�[�h�ɐݒ肷��
                    Dim row As SeikyuKingakuDataSet.dt_KakerituRow = kakerituTable(0)

                    inrReturnKingaku = Fix(intKingaku * row.kakeritu)
                Else
                    Return False
                End If
            Case 6
                Dim kameitenMukeTable As SeikyuKingakuDataSet.dt_KameitenMukeDataTable = _
                    seikyuuKakakuDataSet.dt_KameitenMuke

                If kameitenMukeTable.Count <> 0 Then
                    ' �擾�ł����ꍇ�A�s�����擾���Q�ƃ��R�[�h�ɐݒ肷��
                    Dim row As SeikyuKingakuDataSet.dt_KameitenMukeRow = kameitenMukeTable(0)

                    ' �������ʂ̉����X���i��0�̏ꍇ
                    If row.kameiten_muke_kkk = 0 Then
                        If row.kakeritu <> 0 Then
                            inrReturnKingaku = Fix(intKingaku / row.kakeritu)
                        Else
                            inrReturnKingaku = 0
                        End If
                    Else
                        inrReturnKingaku = row.kameiten_muke_kkk
                    End If
                Else
                    Return False
                End If
            Case Else
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���ȋ敪���R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As KeiretuSearchDataSet.KeiretuTableRow

        commandTextSb.Append(" SELECT")
        commandTextSb.Append("    i.keiretu_cd")
        commandTextSb.Append("    ,k.keiretu_mei")
        commandTextSb.Append(" FROM")
        commandTextSb.Append("    jhs_sys.m_ikkatu_nyuukin_taisyou i")
        commandTextSb.Append(" LEFT JOIN (")
        commandTextSb.Append("			SELECT")
        commandTextSb.Append("				keiretu_cd")
        commandTextSb.Append("        		,keiretu_mei")
        commandTextSb.Append("        	FROM")
        commandTextSb.Append("        		jhs_sys.m_keiretu")
        commandTextSb.Append("        	GROUP BY")
        commandTextSb.Append("        		keiretu_cd,keiretu_mei")
        commandTextSb.Append("        	) k")
        commandTextSb.Append("           	ON (i.keiretu_cd = k.keiretu_cd)")
        commandTextSb.Append(" ORDER BY")
        commandTextSb.Append("    i.keiretu_cd")

        ' �f�[�^�̎擾
        Dim keiretuDataSet As New KeiretuSearchDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            keiretuDataSet, keiretuDataSet.KeiretuTable.TableName)

        Dim keiretuDataTable As KeiretuSearchDataSet.KeiretuTableDataTable = _
                    keiretuDataSet.KeiretuTable

        If keiretuDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In keiretuDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.keiretu_cd + ":" + row.keiretu_mei, row.keiretu_cd, dt))
                Else
                    dt.Rows.Add(CreateRow(row.keiretu_mei, row.keiretu_cd, dt))
                End If
            Next

        End If

    End Sub
End Class
