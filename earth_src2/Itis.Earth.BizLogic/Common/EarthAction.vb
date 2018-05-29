Imports System.Text

Public Class EarthAction
    '�N���X��
    Private ReadOnly className As String = _
                                        Reflection.MethodBase.GetCurrentMethod.DeclaringType.FullName
    Private inquiryLogic As New KensakuSyoukaiInquiryLogic

    Public Enum paramType As Integer
        _String = 0
        _Ingeger = 1
    End Enum

    ''' <summary>
    ''' �o�͂���f�[�^���擾
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function JoinData(ByVal dt As Data.DataTable, ByVal separator As String) As String

        Dim strListData As New StringBuilder
        '��ؕ���
        Dim intRow As Integer
        Dim intCol As Integer

        For intRow = 0 To dt.Rows.Count - 1
            strListData.Append(dt.Rows(intRow).Item(0).ToString)
            For intCol = 1 To dt.Columns.Count - 1
                strListData.Append(separator & dt.Rows(intRow).Item(intCol).ToString)
            Next
            strListData.Append(vbCrLf)
        Next

        Return strListData.ToString

    End Function



    ''' <summary>
    ''' �o�͂���f�[�^���擾
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function JoinDataTable(ByVal dt As Data.DataTable, ByVal separator As String, ByVal arrParam As String, Optional ByVal arrParamType As paramType = paramType._String) As String
 
        Dim strListData As New StringBuilder
        '��ؕ���
        Dim intRow As Integer
        Dim intCol As Integer
        Dim arr() As String

        Dim maxLength As Integer



        arr = arrParam.Split(","c)
        maxLength = arr.Length - 1

        If arrParamType = paramType._String Then

            For intRow = 0 To dt.Rows.Count - 1
                strListData.Append(dt.Rows(intRow).Item(arr(0)).ToString)

                For intCol = 1 To maxLength
                    strListData.Append(separator & dt.Rows(intRow).Item(arr(intCol)).ToString)
                Next
                If intRow <> dt.Rows.Count - 1 Then
                    strListData.Append(vbCrLf)
                End If
            Next

        Else
            For intRow = 0 To dt.Rows.Count - 1
                strListData.Append(dt.Rows(intRow).Item(Convert.ToInt32(arr(0))).ToString)

                For intCol = 1 To maxLength
                    strListData.Append(separator & dt.Rows(intRow).Item(Convert.ToInt32(arr(0))).ToString)
                Next
                If intRow <> dt.Rows.Count - 1 Then
                    strListData.Append(vbCrLf)
                End If

            Next

        End If


        Return strListData.ToString

    End Function


    ''' <summary>�󔒕����̍폜����</summary>
    Public Function TrimNull(ByVal objStr As Object) As String
   
        If IsDBNull(objStr) Then
            TrimNull = String.Empty
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function


    ''' <summary>���z�\��</summary>
    Public Function SetKingaku(ByVal strKingaku As String) As String
   
        If strKingaku = String.Empty Then
            Return "0"
        Else
            'Dim intValue As Integer
            Return Int(Convert.ToDecimal(strKingaku)).ToString("#,0")
            'intValue = Convert.ToInt32(strKingaku)
            'Return intValue.ToString("#,0")
        End If
    End Function


    ''' <summary>
    ''' Enter Delete
    ''' </summary>
    ''' <param name="datas"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelEnter(ByVal ParamArray datas() As Object) As String

        Dim text As New StringBuilder
        If (datas.Length > 0) Then

            For i As Integer = 1 To datas.Length - 1
                text.Append(Convert.ToString(datas(i)).Replace(Chr(13), String.Empty) & ",")
            Next
        End If

        Return text.ToString
    End Function

    ''' <summary>
    ''' ��������m�F�\
    ''' </summary>
    ''' <param name="strKmeitenCd">�����X�R�[�h</param>
    ''' <param name="strKbn">�敪</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.26</history>
    Public Function TorihukiJyoukenKakuninhyou(ByVal strKmeitenCd As String, ByVal strKbn As String)

        Dim NoFromTo As String '�I�����������X�R�[�h
        Dim NoFrom As String   '@NOFROM@
        Dim NoTo As String     '@NOTO@
        Dim num1 As String     '@NUM1@
        Dim num2 As String     '@NUM2@
        'Dim no As String       '��

        NoFromTo = strKmeitenCd
        ' -------------����������A�����J�[�h�����̃t�H���_�쐬���[�����ύX���邱�Ƃ�Ή����� ��-------------
        'no = Int(NoFromTo / 1000)
        'If no.Length = "1" AndAlso no = "0" Then
        '    NoFrom = "00001"
        'ElseIf (no.Length = "1") AndAlso (Not no.Equals("0")) Then
        '    NoFrom = "0" & (no) & "001"
        'ElseIf no.Length = "2" Then
        '    NoFrom = (no - 1) & "001"
        'Else
        '    NoFrom = ""
        'End If

        'If Not NoFrom.Equals(String.Empty) Then
        '    NoTo = (CInt(NoFrom) + 999)
        '    If NoTo.Length = "4" Then
        '        NoTo = "0" & NoTo
        '    Else
        '        NoTo = NoTo
        '    End If
        'Else
        '    NoTo = ""
        'End If
        NoFrom = Left(strKmeitenCd, 3) & "00"
        NoTo = Left(strKmeitenCd, 3) & "99"
        '-------------����������A�����J�[�h�����̃t�H���_�쐬���[�����ύX���邱�Ƃ�Ή����� ��-------------

        num1 = Left(NoFromTo, 1)
        num2 = Right(NoFromTo, 4)

        '��������i�[��Ǘ��}�X�^���A�i�[��t�@�C���p�X���擾����
        Dim KakunousakiFilePassJ As String
        Dim dtKakunousakiFilePassJ As Data.DataTable
        dtKakunousakiFilePassJ = inquiryLogic.GetKakunousakiFilePassJ(strKbn, NoFromTo)
        If dtKakunousakiFilePassJ.Rows.Count > 0 Then
            KakunousakiFilePassJ = dtKakunousakiFilePassJ.Rows(0).Item("kakunousaki_file_pass").ToString
        Else
            KakunousakiFilePassJ = String.Empty
        End If

        'file://@SERVER@/@NOFROM@�`@NOTO@/@NUM1@@NUM2@.xbd
        Dim TorihikiJyoukenFilePath As String = System.Configuration.ConfigurationManager.AppSettings("TorihikiJyoukenFilePath").ToString
        Dim FilePath As String
        FilePath = TorihikiJyoukenFilePath.Replace("@SERVER@", KakunousakiFilePassJ).Replace("@NOFROM@", NoFrom).Replace("@NOTO@", NoTo).Replace("@NUM1@", num1).Replace("@NUM2@", num2)
        'FilePath = "\\dam104\SHARE(��108)\00203.lzh"

        Return FilePath

    End Function

    ''' <summary>
    ''' �����J�[�h
    ''' </summary>
    ''' <param name="strKmeitenCd">�����X�R�[�h</param>
    ''' <param name="strKbn">�敪</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.26</history>
    Public Function TyousaCard(ByVal strKmeitenCd As String, ByVal strKbn As String)

        Dim NoFromTo As String '�I�����������X�R�[�h
        Dim NoFrom As String   '@NOFROM@
        Dim NoTo As String     '@NOTO@
        Dim num1 As String     '@NUM1@
        Dim num2 As String     '@NUM2@
        'Dim no As String       '��

        NoFromTo = strKmeitenCd
        ' -------------����������A�����J�[�h�����̃t�H���_�쐬���[�����ύX���邱�Ƃ�Ή����� ��-------------
        'no = Int(NoFromTo / 1000)
        'If no.Length = "1" AndAlso no = "0" Then
        '    NoFrom = "00001"
        'ElseIf (no.Length = "1") AndAlso (Not no.Equals("0")) Then
        '    NoFrom = "0" & (no) & "001"
        'ElseIf no.Length = "2" Then
        '    NoFrom = (no - 1) & "001"
        'Else
        '    NoFrom = ""
        'End If

        'If Not NoFrom.Equals(String.Empty) Then
        '    NoTo = (CInt(NoFrom) + 999)
        '    If NoTo.Length = "4" Then
        '        NoTo = "0" & NoTo
        '    Else
        '        NoTo = NoTo
        '    End If
        'Else
        '    NoTo = ""
        'End If
        NoFrom = Left(strKmeitenCd, 3) & "00"
        NoTo = Left(strKmeitenCd, 3) & "99"
        '-------------����������A�����J�[�h�����̃t�H���_�쐬���[�����ύX���邱�Ƃ�Ή����� ��-------------

        num1 = Left(NoFromTo, 1)
        num2 = Right(NoFromTo, 4)

        '�����J�[�h�i�[��Ǘ��}�X�^���A�i�[��t�@�C���p�X���擾����
        Dim KakunousakiFilePassC As String
        Dim dtKakunousakiFilePassC As Data.DataTable
        dtKakunousakiFilePassC = inquiryLogic.GetKakunousakiFilePassC(strKbn, NoFromTo)
        If dtKakunousakiFilePassC.Rows.Count > 0 Then
            KakunousakiFilePassC = dtKakunousakiFilePassC.Rows(0).Item("kakunousaki_file_pass").ToString
        Else
            KakunousakiFilePassC = String.Empty
        End If

        'file://@SERVER@/@NOFROM@�`@NOTO@/@NUM1@@NUM2@.xbd
        Dim TyousaCardFilePath As String = System.Configuration.ConfigurationManager.AppSettings("TyousaCardFilePath").ToString
        Dim FilePath As String
        FilePath = TyousaCardFilePath.Replace("@SERVER@", KakunousakiFilePassC).Replace("@NOFROM@", NoFrom).Replace("@NOTO@", NoTo).Replace("@NUM1@", num1).Replace("@NUM2@", num2)
        'FilePath = "\\dam104\SHARE(��108)\00203.lzh"

        Return FilePath

    End Function

End Class

