Imports System.Data
Imports System.Data.SqlClient

Public Class MototyouLogic
    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
#Region "�����挳��"

#Region "�����挳��_�`�[�f�[�^�擾"
    ''' <summary>
    ''' �����挳��_�`�[�f�[�^�擾
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="fromDate">�N����FROM</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetUrikakeDataNewest(ByVal seikyuuSakiCd As String, _
                                         ByVal seikyuuSakiBrc As String, _
                                         ByVal seikyuuSakiKbn As String, _
                                         Optional ByVal fromDate As Date = Nothing _
                                         ) As UrikakeDataRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiMototyouDenpyouData", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate _
                                                    )
        If fromDate = Nothing Then
            fromDate = EarthConst.Instance.MIN_DATE
        End If

        Dim dataAccess As New MototyouDataAccess
        Dim dtResult As DataTable
        Dim drList As List(Of UrikakeDataRecord)

        '�f�[�^�擾
        dtResult = dataAccess.urikakeDataNewest(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate)

        'List�Ɋi�[
        drList = DataMappingHelper.Instance.getMapArray(Of UrikakeDataRecord)(GetType(UrikakeDataRecord), dtResult)

        If drList.Count <= 0 Then
            Return New UrikakeDataRecord
        End If

        '�l�߂�
        Return drList(0)

    End Function
#End Region

#Region "�����挳��_�J�z�c���擾"
    ''' <summary>
    ''' �����挳��_�J�z�c���擾
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="fromDate">�N����FROM</param>
    ''' <returns>�J�z�c��(Long�^)</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSakiMototyouKurikosiZan(ByVal seikyuuSakiCd As String, _
                                                      ByVal seikyuuSakiBrc As String, _
                                                      ByVal seikyuuSakiKbn As String, _
                                                      ByVal fromDate As Date _
                                                      ) As Long

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiMototyouKurikosiZan", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate _
                                                    )

        Dim dataAccess As New MototyouDataAccess
        Dim Data As Object
        Dim retData As Long

        '�f�[�^�擾
        Data = dataAccess.seikyuuSakiMototyouKurikosiZan(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'Long�ɃL���X�g
                retData = CType(Data, Long)
            Catch ex As Exception
                '���s������[��
                retData = 0
            End Try
        End If

        '�l�߂�
        Return retData

    End Function
#End Region

#Region "�����挳��_�`�[�f�[�^�擾"
    ''' <summary>
    ''' �����挳��_�`�[�f�[�^�擾
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="fromDate">�N����FROM</param>
    ''' <param name="toDate">�N����TO</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSakiMototyouDenpyouData(ByVal seikyuuSakiCd As String, _
                                                      ByVal seikyuuSakiBrc As String, _
                                                      ByVal seikyuuSakiKbn As String, _
                                                      ByVal fromDate As Date, _
                                                      ByVal toDate As Date _
                                                      ) As List(Of SeikyuuSakiMototyouRecord)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiMototyouDenpyouData", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate, _
                                                    toDate _
                                                    )
        Dim dataAccess As New MototyouDataAccess
        Dim dtResult As DataTable
        Dim drList As List(Of SeikyuuSakiMototyouRecord)

        '�f�[�^�擾
        dtResult = dataAccess.seikyuuSakiMototyouDenpyouData(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate, toDate)

        'List�Ɋi�[
        drList = DataMappingHelper.Instance.getMapArray(Of SeikyuuSakiMototyouRecord)(GetType(SeikyuuSakiMototyouRecord), dtResult)

        '�l�߂�
        Return drList

    End Function
#End Region

#End Region

#Region "�x���挳��"

#Region "�x���挳��_�`�[�f�[�^�擾"
    ''' <summary>
    ''' �x���挳��_�`�[�f�[�^�擾
    ''' </summary>
    ''' <param name="tysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="fromDate">�N����FROM</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetKaikakeDataNewest(ByVal tysKaisyaCd As String, _
                                         Optional ByVal fromDate As Date = Nothing _
                                         ) As KaikakeDataRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiSakiMototyouDenpyouData", _
                                                    tysKaisyaCd, _
                                                    fromDate _
                                                    )
        If fromDate = Nothing Then
            fromDate = EarthConst.Instance.MIN_DATE
        End If

        Dim dataAccess As New MototyouDataAccess
        Dim dtResult As DataTable
        Dim drList As List(Of KaikakeDataRecord)

        '�f�[�^�擾
        dtResult = dataAccess.kaikakeDataNewest(tysKaisyaCd, fromDate)

        'List�Ɋi�[
        drList = DataMappingHelper.Instance.getMapArray(Of KaikakeDataRecord)(GetType(KaikakeDataRecord), dtResult)

        If drList.Count <= 0 Then
            Return New KaikakeDataRecord
        End If

        '�l�߂�
        Return drList(0)

    End Function
#End Region

#Region "�x���挳��_�J�z�c���擾"
    ''' <summary>
    ''' �x���挳��_�J�z�c���擾
    ''' </summary>
    ''' <param name="tysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="fromDate">�N����FROM</param>
    ''' <returns>�J�z�c��(Long�^)</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiSakiMototyouKurikosiZan(ByVal tysKaisyaCd As String, _
                                                      ByVal fromDate As Date _
                                                      ) As Long

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiSakiMototyouKurikosiZan", _
                                                    tysKaisyaCd, _
                                                    fromDate _
                                                    )

        Dim dataAccess As New MototyouDataAccess
        Dim Data As Object
        Dim retData As Long

        '�f�[�^�擾
        Data = dataAccess.siharaiSakiMototyouKurikosiZan(tysKaisyaCd, fromDate)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'Long�ɃL���X�g
                retData = CType(Data, Long)
            Catch ex As Exception
                '���s������[��
                retData = 0
            End Try
        End If

        '�l�߂�
        Return retData

    End Function
#End Region

#Region "�x���挳��_�`�[�f�[�^�擾"
    ''' <summary>
    ''' �x���挳��_�`�[�f�[�^�擾
    ''' </summary>
    ''' <param name="tysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="fromDate">�N����FROM</param>
    ''' <param name="toDate">�N����TO</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiSakiMototyouDenpyouData(ByVal tysKaisyaCd As String, _
                                                      ByVal fromDate As Date, _
                                                      ByVal toDate As Date _
                                                      ) As List(Of SiharaiSakiMototyouRecord)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiSakiMototyouDenpyouData", _
                                                    tysKaisyaCd, _
                                                    fromDate, _
                                                    toDate _
                                                    )
        Dim dataAccess As New MototyouDataAccess
        Dim dtResult As DataTable
        Dim drList As List(Of SiharaiSakiMototyouRecord)

        '�f�[�^�擾
        dtResult = dataAccess.siharaiSakiMototyouDenpyouData(tysKaisyaCd, fromDate, toDate)

        'List�Ɋi�[
        drList = DataMappingHelper.Instance.getMapArray(Of SiharaiSakiMototyouRecord)(GetType(SiharaiSakiMototyouRecord), dtResult)

        '�l�߂�
        Return drList

    End Function
#End Region

#End Region

End Class
