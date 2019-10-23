
''' <summary>
''' �c�Ə��}�X�^����
''' </summary>
''' <remarks></remarks>
''' 
Public Class EigyousyoSearchLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �c�Ə��}�X�^����
    ''' </summary>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoKana">�c�Ə��J�i</param>
    ''' <param name="blnDelete">����Ώۃt���O</param>
    ''' <param name="allRowCount">�������ʑS����</param>
    ''' <param name="startRow">�i�C�Ӂj�f�[�^���o���̊J�n�s(1���ڂ�1���w��)Default:1</param>
    ''' <param name="endRow">�i�C�Ӂj�f�[�^���o���̏I���s Default:99999999</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEigyousyoSearchResult(ByVal strEigyousyoCd As String, _
                                    ByVal strEigyousyoKana As String, _
                                    ByVal blnDelete As Boolean, _
                                    ByRef allRowCount As Integer, _
                                    Optional ByVal startRow As Integer = 1, _
                                    Optional ByVal endRow As Integer = 99999999) As List(Of EigyousyoSearchRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetEigyousyoSearchResult", _
                                            strEigyousyoCd, _
                                            strEigyousyoKana, _
                                            blnDelete, _
                                            allRowCount, _
                                            startRow, _
                                            endRow)

        Dim dataAccess As EigyousyoSearchDataAccess = New EigyousyoSearchDataAccess

        '�c�Ə��}�X�^���猟���������w�肵���e�[�u�����擾
        Dim table As DataTable = dataAccess.GetEigyosyoKensakuData(strEigyousyoCd, _
                                                                   strEigyousyoKana, _
                                                                   blnDelete)


        ' ������ݒ�
        allRowCount = table.Rows.Count

        Dim arrRtnData As List(Of EigyousyoSearchRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of EigyousyoSearchRecord)(GetType(EigyousyoSearchRecord), table)

        Return arrRtnData
    End Function

End Class