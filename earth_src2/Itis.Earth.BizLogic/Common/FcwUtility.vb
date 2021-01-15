Imports System.Configuration.ConfigurationManager
Imports System.Text
Imports System.IO
Imports Itis.Framework.Report

Public Class FcwUtility
    '�N���X��
    Private ReadOnly className As String = _
                                        Reflection.MethodBase.GetCurrentMethod.DeclaringType.FullName

    Public fcpFileName As String

    Public dataFileName As String

    Public dataFileNameOut As String

    Public fcwURL As String

    Public folderName As String

    Public webpage As System.Web.UI.Page

    Public DatHeader As New StringBuilder

    Public FormSection As New StringBuilder

    Public DatFixedDataSection As New StringBuilder

    Public DatTableDataSection As New StringBuilder

    Public DatBodyDataSection As New StringBuilder

    Public Report As New ReportManager
    Enum ExcelStatus As Integer
        OK = 0                              '����
        IOException = 1                     '�G���[(���̃��[�U��CSV�t�@�C�����J���Ă���)
        UnauthorizedAccessException = 2     '�G���[(CSV�t�@�C�����쐬����p�X���s��)
        NoData = 3                          '�Ώۂ̃f�[�^���擾�ł��܂���B

    End Enum
    Public Enum paramType As Integer
        _String = 0
        _Ingeger = 1

    End Enum


    ''' <summary>
    '''     �N���X������
    ''' </summary>
    Public Sub New(ByVal page As System.Web.UI.Page, ByVal syainCd As String, ByVal kinouId As String, Optional ByVal syurui As String = ".fcp")

        'EMAB��Q�Ή����̊i�[����
        Dim datName As String = ""
        '�t�@�C����
        folderName = ConnectionStrings("conFolderName").ConnectionString

        'Fcw�@Path�@Name
        fcpFileName = folderName & kinouId & syurui

        datName = GetFileName(kinouId, syainCd)
        'Dat�@Path�@Name
        dataFileName = System.Configuration.ConfigurationManager.AppSettings("ReportTempFileServerName").ToString() & _
                       System.Configuration.ConfigurationManager.AppSettings("ReportTempFilePath").ToString() & datName
        dataFileNameOut = folderName & datName

        webpage = page

    End Sub

    ''' <summary>
    ''' ���[���������
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenPdf()

        'EMAB��Q�Ή����̊i�[����
        Dim methodName As String = className & "." & Reflection.MethodBase.GetCurrentMethod.Name
        Dim Report As New ReportManager(fcpFileName, dataFileNameOut, PrintMode.Preview)
        Report.ReportServerURL = System.Configuration.ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        webpage.Response.Redirect(Report.URL)
        'webpage.Response.Redirect(fcwURL)

    End Sub

    ''' <summary>
    ''' Url���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUrl(ByVal kbn As String, ByVal bultukenNo As String, ByVal sakuseiKisau As String)
        Dim methodName As String = className & "." & Reflection.MethodBase.GetCurrentMethod.Name
        Dim Report As New ReportManager(fcpFileName, dataFileNameOut, PrintMode.ServerSave)
        Report.ReportServerURL = System.Configuration.ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Report.Parameters.OutFile = "JHSEarth\" & kbn & bultukenNo & "-0-" & "�������Ϗ�000" & "-" & sakuseiKisau

        Return Report.URL
    End Function

    ''' <summary>
    ''' Url���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUrlKensaHoukokusyo(ByVal strOutFileName As String)
        Dim methodName As String = className & "." & Reflection.MethodBase.GetCurrentMethod.Name
        Dim Report As New ReportManager(fcpFileName, dataFileNameOut, PrintMode.ServerSave)
        'Dim Report As New ReportManager(fcpFileName, dataFileNameOut, PrintMode.Preview)
        Report.ReportServerURL = System.Configuration.ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Report.Parameters.OutFile = "JHSEarth\" & strOutFileName

        Return Report.URL
    End Function


    ''' <summary>
    ''' Url���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>2016/11/30 �������@�ǉ�</remarks>
    Public Function GetUrlByFileName(ByVal strOutFileName As String)
        Dim methodName As String = className & "." & Reflection.MethodBase.GetCurrentMethod.Name
        Dim Report As New ReportManager(fcpFileName, dataFileNameOut, PrintMode.ServerSave)
        Report.ReportServerURL = System.Configuration.ConfigurationManager.AppSettings("ReportServerUrl").ToString()
        Report.Parameters.OutFile = "JHSEarth\" & strOutFileName

        Return Report.URL
    End Function


    ''' <summary>
    ''' Dat�̖����쐬����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFileName(ByVal kinouId As String, ByVal syainCd As String) As String

        'EMAB��Q�Ή����̊i�[����
        Dim methodName As String = className & "." & Reflection.MethodBase.GetCurrentMethod.Name

        Return kinouId & "_" & syainCd & "_" & Format(Now(), "yyyyMMddHHmmssfff") & ".dat"
    End Function
    ''' <summary>
    ''' DAT�̃w�b�_�[
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CreateFormSection(ByVal text As String) As String
        'EMAB��Q�Ή����̊i�[����
        Dim methodName As String = className & "." & Reflection.MethodBase.GetCurrentMethod.Name

        FormSection.Length = 0

        '�f�[�^ �t�@�C���̃w�b�_�[�����𐶐�����
        FormSection.AppendLine("[Form Section]")
        FormSection.AppendLine(text)
        FormSection.AppendLine(";")

        Return FormSection.ToString
    End Function
    ''' <summary>
    ''' DAT�̃w�b�_�[
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CreateDatHeader(Optional ByVal SEPRT As String = "") As String

        '�f�[�^ �t�@�C���̃w�b�_�[�����𐶐�����
        DatHeader.AppendLine("[Control Section]")
        DatHeader.AppendLine("VERSION=3.1")
        DatHeader.AppendLine("OPTION=FIELDATTR")
        DatHeader.AppendLine("AUTOPAGEBREAK=ON")

        If SEPRT <> "" Then
            DatHeader.AppendLine("SEPARATOR=" & SEPRT)
        End If

        DatHeader.AppendLine(";")

        Return DatHeader.ToString
    End Function

    ''' <summary>
    ''' DAT�́uFixedDataSection�v
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Public Function CreateFixedDataSection(ByVal text As String, Optional ByVal flag As Integer = 0) As String

        DatFixedDataSection.Length = 0

        DatFixedDataSection.AppendLine("[Fixed Data Section]")
        DatFixedDataSection.AppendLine(text)

        If flag = 0 Then
            DatFixedDataSection.AppendLine(";")
        End If

        Return DatFixedDataSection.ToString

    End Function

    ''' <summary>
    ''' DAT�́uFixedDataSection�v
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="blnStart"></param>
    ''' <param name="blnEnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateFixedDataSectionBln(ByVal text As String, Optional ByVal blnStart As Boolean = True, Optional ByVal blnEnd As Boolean = True) As String

        DatFixedDataSection.Length = 0

        If blnStart Then
            DatFixedDataSection.AppendLine("[Fixed Data Section]")
        End If

        DatFixedDataSection.AppendLine(text)

        If blnEnd Then
            DatFixedDataSection.AppendLine(";")
        End If

        Return DatFixedDataSection.ToString

    End Function

    ''' <summary>
    ''' �@DAT�́uTable�@Data�@Section�v
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Public Function CreateTableDataSection(ByVal text As String, Optional ByVal flg As Integer = 0) As String

        DatTableDataSection.Length = 0

        DatTableDataSection.AppendLine("[Table Data Section]")
        DatTableDataSection.Append(text)

        If flg = 1 Then
            DatTableDataSection.Append(vbCrLf)
        End If

        Return DatTableDataSection.ToString

    End Function
    ''' <summary>
    ''' �@DAT�́uBody�@Data�@Section�v
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Public Function CreateBodyDataSection(ByVal text As String) As String

        DatBodyDataSection.Length = 0

        DatBodyDataSection.AppendLine("[Body Data Section]")
        DatBodyDataSection.AppendLine(text)
        DatBodyDataSection.AppendLine(";")

        Return DatBodyDataSection.ToString
    End Function
    ''' <summary>
    ''' �f�[�^���擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDatData(Optional ByVal text As String = "") As String

        Dim sb As New StringBuilder
        If text = "" Then
            sb.Append(DatHeader.ToString)

            If FormSection IsNot Nothing Then
                sb.Append(FormSection.ToString)
            End If

            If DatFixedDataSection IsNot Nothing Then
                sb.Append(DatFixedDataSection.ToString)
            End If

            If DatBodyDataSection IsNot Nothing Then
                sb.Append(DatBodyDataSection.ToString)
            End If

            If DatTableDataSection IsNot Nothing Then
                sb.Append(DatTableDataSection.ToString)
            End If

        Else
            sb.Append(text)
        End If
        Return sb.ToString

    End Function

    ''' <summary>
    ''' �f�[�^�@�uFixedDataSection�v���擾
    ''' </summary>
    ''' <param name="arrParam"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedDataSection(ByVal arrParam As Generic.List(Of String), ByVal data As DataTable) As String

        Dim indexFor As Integer
        Dim maxLength As Integer

        maxLength = arrParam.Count - 1
        Dim sb As New StringBuilder

        For indexFor = 0 To maxLength
            sb.AppendLine(arrParam(indexFor) & "=" & data.Rows(0).Item(arrParam(indexFor)).ToString)
        Next

        Return sb.ToString

    End Function

    ''' <summary>
    ''' �f�[�^�@�uFixedDataSection�v���擾
    ''' </summary>
    ''' <param name="arrParam">arrParam</param>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedDataSection(ByVal arrParam As Generic.List(Of Integer), ByVal data As DataTable) As String

        Dim indexFor As Integer
        Dim maxLength As Integer

        maxLength = arrParam.Count - 1
        Dim sb As New StringBuilder

        For indexFor = 0 To maxLength
            sb.AppendLine(arrParam(indexFor) & "=" & data.Rows(0).Item(arrParam(indexFor)).ToString)
        Next

        Return sb.ToString

    End Function

    ''' <summary>
    ''' �f�[�^�@�uFixedDataSection�v���擾
    ''' </summary>
    ''' <param name="arrParam">arrParam</param>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedDataSection(ByVal arrParam As String, ByVal data As DataTable, Optional ByVal arrParamType As paramType = paramType._String) As String

        Dim indexFor As Integer
        Dim maxLength As Integer
        Dim arr() As String
        arr = arrParam.Split(","c)

        maxLength = arr.Length - 1
        Dim sb As New StringBuilder

        For indexFor = 0 To maxLength
            Dim name As Object
            If arrParamType = paramType._String Then
                name = (arr(indexFor))
                If indexFor = maxLength Then
                    sb.Append(data.Columns(Convert.ToString(name).ToString.Trim).ColumnName & "=" & data.Rows(0).Item(arr(indexFor).ToString.Trim).ToString)

                Else
                    sb.AppendLine(data.Columns(Convert.ToString(name).ToString.Trim).ColumnName & "=" & data.Rows(0).Item(arr(indexFor).ToString.Trim).ToString)

                End If
            Else
                name = (arr(indexFor))
                If indexFor = maxLength Then
                    sb.Append(data.Columns(Convert.ToInt32(name).ToString.Trim).ColumnName & "=" & data.Rows(0).Item(arr(indexFor)).ToString)

                Else
                    sb.AppendLine(data.Columns(Convert.ToInt32(name).ToString.Trim).ColumnName & "=" & data.Rows(0).Item(arr(indexFor)).ToString)

                End If

            End If
        Next

        Return sb.ToString

    End Function


    ''' <summary>
    ''' Dat������
    ''' </summary>
    ''' <param name="text">text</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WriteData(ByVal text As String, Optional ByVal EntFlg As Boolean = False) As Integer
 
        If text = "" Then
            Return ExcelStatus.NoData
        Else
            Try
                Using sw As New StreamWriter(dataFileName, False, System.Text.Encoding.Default)
                    If EntFlg Then
                        sw.Write(text)
                    Else
                        sw.WriteLine(text)
                    End If

                    Return ExcelStatus.OK
                End Using
            Catch ex As IOException
                Return ExcelStatus.IOException
            Catch ex As UnauthorizedAccessException
                Return ExcelStatus.UnauthorizedAccessException
            End Try
        End If

    End Function

    ''' <summary>
    ''' �`�F�b�N�@�G���[
    ''' </summary>
    ''' <param name="excelDownLoadStatus">DownLoadStatus</param>
    ''' <remarks></remarks>
    Public Function GetErrMsg(ByVal excelDownLoadStatus As Integer) As String

        Dim msg As String = String.Empty

        Select Case excelDownLoadStatus

            Case ExcelStatus.NoData
                msg = Messages.Instance.MSG034E

            Case ExcelStatus.IOException
                msg = Messages.Instance.MSG035E

            Case ExcelStatus.UnauthorizedAccessException
                msg = Messages.Instance.MSG036E

        End Select


        Return msg

    End Function
End Class
