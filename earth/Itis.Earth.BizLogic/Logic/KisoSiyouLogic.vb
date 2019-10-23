
''' <summary>
''' ��b�d�l�f�[�^�ҏW�p�N���X
''' </summary>
''' <remarks></remarks>
Public Class KisoSiyouLogic

    '�����ʃ^�C�v
    Private Const pStrKouji = "�y�H���z"
    Private Const pStrTyokusetuKiso = "�y���ڊ�b�z"
    Private Const pStrYouSyasin = "�y�v�ʐ^�z"

    '�����ʔ��f������
    Private Const pStrTigyou = "�n��"
    Private Const pStrTutinoTikan = "�y�̒u��"

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

    End Sub

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' ��b�d�lNO��蔻�薼�i��b�d�l�j���擾���܂�
    ''' </summary>
    ''' <param name="ksSiyouNo">��b�d�lNO</param>
    ''' <returns>���薼�i��b�d�l�j</returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouMei(ByVal ksSiyouNo As Integer) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouMei", _
                                                    ksSiyouNo)

        ' ��b�d�l�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As New KisoSiyouDataAccess()

        Dim table As KisoSiyouDataSet.KisoSiyouTableDataTable = dataAccess.GetHanteiMei(ksSiyouNo)

        If Not table Is Nothing Then
            Dim row As KisoSiyouDataSet.KisoSiyouTableRow
            row = table.Rows(0)
            ' �擾���ʂ�ԋp
            Return row.ks_siyou
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' ��b�d�lNO����b�d�l���R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="ksSiyouNo">��b�d�lNO</param>
    ''' <returns>��b�d�l���R�[�h</returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouRec(ByVal ksSiyouNo As Integer) As KisoSiyouRecord

        Dim list As List(Of KisoSiyouRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouMei", _
                                                    ksSiyouNo)

        ' ��b�d�l�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As New KisoSiyouDataAccess()

        Dim table As KisoSiyouDataSet.KisoSiyouTableDataTable = dataAccess.GetHanteiMei(ksSiyouNo)

        If Not table Is Nothing Then

            list = DataMappingHelper.Instance.getMapArray(Of KisoSiyouRecord)(GetType(KisoSiyouRecord), table)

            ' �擾���ʂ�ԋp
            Return list(0)
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' ��b�d�l�ڑ���NO��蔻�薼�i��b�d�l�ڑ����j���擾���܂�
    ''' </summary>
    ''' <param name="ksSiyouSetuzokusiNo">��b�d�l�ڑ���NO</param>
    ''' <returns>���薼�i��b�d�l�ڑ����j</returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouSetuzokusiMei(ByVal ksSiyouSetuzokusiNo As Integer) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouSetuzokusiMei", _
                                                    ksSiyouSetuzokusiNo)

        ' ��b�d�l�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As New KisoSiyouDataAccess()

        ' �擾���ʂ�ԋp
        Return dataAccess.GetHanteiSetuzokusiMei(ksSiyouSetuzokusiNo)

    End Function

    ''' <summary>
    ''' ��b�d�l�}�X�^����
    ''' </summary>
    ''' <param name="strKisoSiyouNo">��b�d�lNO</param>
    ''' <param name="strKisoSiyouNm">��b�d�l��</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="blnMatchType">��b�d�lNO�����^�C�v�iTrue:���S��v or False:�O����v�j</param>
    ''' <param name="blnDelete">����Ώۃt���O</param>
    ''' <param name="allRowCount">�������ʑS����</param>
    ''' <param name="startRow">�i�C�Ӂj�f�[�^���o���̊J�n�s(1���ڂ�1���w��)Default:1</param>
    ''' <param name="endRow">�i�C�Ӂj�f�[�^���o���̏I���s Default:99999999</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouSearchResult(ByVal strKisoSiyouNo As String, _
                                    ByVal strKisoSiyouNm As String, _
                                    ByVal strKameitenCd As String, _
                                    ByVal blnMatchType As Boolean, _
                                    ByVal blnDelete As Boolean, _
                                    ByRef allRowCount As Integer, _
                                    Optional ByVal startRow As Integer = 1, _
                                    Optional ByVal endRow As Integer = 99999999) As List(Of KisoSiyouRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouSearchResult", _
                                            strKisoSiyouNo, _
                                            strKisoSiyouNm, _
                                            strKameitenCd, _
                                            blnMatchType, _
                                            blnDelete, _
                                            allRowCount, _
                                            startRow, _
                                            endRow)

        Dim dataAccess As KisoSiyouDataAccess = New KisoSiyouDataAccess

        Dim table As DataTable = dataAccess.GetKisoSiyouKensakuData(strKisoSiyouNo, _
                                                                   strKisoSiyouNm, _
                                                                   strKameitenCd, _
                                                                   blnMatchType, _
                                                                   blnDelete)


        ' ������ݒ�
        allRowCount = table.Rows.Count

        Dim arrRtnData As List(Of KisoSiyouRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of KisoSiyouRecord)(GetType(KisoSiyouRecord), _
                                                                             table)

        Return arrRtnData
    End Function


    ''' <summary>
    ''' ��b�d�lNO��蔻���ʕ\����ԋp���܂�
    ''' </summary>
    ''' <param name="intHantei1Cd">����R�[�h1</param>
    ''' <param name="intHantei2Cd">����R�[�h2</param>
    ''' <returns>�����ʖ�</returns>
    ''' <remarks></remarks>
    Public Function GetHanteiSyunetuDisp(ByVal intHantei1Cd As Integer, ByVal intHantei2Cd As Integer) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanteiSyunetuDisp", _
                                                    intHantei1Cd _
                                                    , intHantei2Cd _
                                                    )

        Dim strReturn As String = "" '�ԋp�l

        If intHantei2Cd <> 0 And intHantei2Cd > 0 Then
            Return strReturn 
        End If

        strReturn = JudgeHanteiSyunetuDisp(intHantei1Cd)

        Return strReturn
    End Function

    ''' <summary>
    ''' ��b�d�lNO��蔻���ʕ\���𔻒肵�A�ԋp���܂�
    ''' </summary>
    ''' <param name="ksSiyouNo">��b�d�lNO</param>
    ''' <returns>�����ʖ�</returns>
    ''' <remarks></remarks>
    Public Function JudgeHanteiSyunetuDisp(ByVal ksSiyouNo As Integer) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanteiSyunetuDisp", _
                                                    ksSiyouNo)

        ' ��b�d�l�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As New KisoSiyouDataAccess()
        Dim strReturn As String = "" '�ԋp�l

        Dim table As KisoSiyouDataSet.KisoSiyouTableDataTable = dataAccess.GetHanteiMei(ksSiyouNo)

        If Not table Is Nothing Then
            Dim row As KisoSiyouDataSet.KisoSiyouTableRow
            row = table.Rows(0)

            '�H������t���O
            Select Case row.koj_hantei_flg
                Case 0
                    If row.ks_siyou.Contains(pStrTigyou) Or row.ks_siyou.Contains(pStrTutinoTikan) Then
                        strReturn = pStrYouSyasin
                    Else
                        strReturn = pStrTyokusetuKiso
                    End If

                Case 1
                    strReturn = pStrKouji

                Case Else
                    strReturn = ""

            End Select

        Else
            strReturn = ""

        End If

        Return strReturn
    End Function
End Class
