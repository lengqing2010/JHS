Imports Itis.ApplicationBlocks.ExceptionManagement
Public Class DataLogic

    ''' <summary>
    ''' �I�u�W�F�N�g�̌^�𔻕ʂ��A�ŏ��l�������ꍇ��DBNull��Ԃ�
    ''' </summary>
    ''' <param name="itemObject"></param>
    ''' <remarks></remarks>
    Public Function fncMinValueToDBNull(ByVal itemObject As Object) As Object

        Dim flgSetNull As Boolean = False
        Dim retObj As Object = itemObject

        ' �^�𔻕ʂ��A�e�^�̍ŏ��l���ݒ肳��Ă����ꍇ�ADBNull���Z�b�g����
        If retObj Is Nothing Then
            flgSetNull = True
        ElseIf TypeOf retObj Is Int32 Then
            If retObj = Integer.MinValue Then
                flgSetNull = True
            End If
        ElseIf TypeOf retObj Is Int64 Then
            If retObj = Long.MinValue Then
                flgSetNull = True
            End If
        ElseIf TypeOf retObj Is Decimal Then
            If retObj = Decimal.MinValue Then
                flgSetNull = True
            End If
        ElseIf TypeOf retObj Is DateTime Then
            If retObj = DateTime.MinValue Then
                flgSetNull = True
            End If
        End If

        If flgSetNull Then
            retObj = New System.Object
            retObj = System.DBNull.Value
        End If

        Return retObj

    End Function

    ''' <summary>
    ''' �I�u�W�F�N�g�̌^�𔻕ʂ��A�ŏ��l�������ꍇ��DBNull��Ԃ�(�Q�Ɠn��)
    ''' </summary>
    ''' <param name="itemObject"></param>
    ''' <remarks></remarks>
    Public Sub subMinValueToDBNull(ByRef itemObject As Object)

        itemObject = fncMinValueToDBNull(itemObject)

    End Sub

#Region "�f�[�^�̌^�ϊ����\�b�h"
    'EMAB��Q�Ή����i�[����
    Public ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    ''' <summary>
    ''' ���������t�^�ɕϊ����܂�
    ''' </summary>
    ''' <param name="strValue">�ϊ�������������</param>
    ''' <returns>�ϊ���̓��t</returns>
    ''' <remarks></remarks>
    Public Function str2DtTime(ByVal strValue As String) As DateTime
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "str2DtTime" _
                                            , strValue)
        Dim dtRet As DateTime
        If strValue.Length > 0 Then
            dtRet = DateTime.Parse(strValue)
        Else
            dtRet = DateTime.MinValue
        End If
        Return dtRet
    End Function

    ''' <summary>
    ''' ���t�^�𕶎���ɕϊ����܂�
    ''' </summary>
    ''' <param name="dtValue">�ϊ����������t</param>
    ''' <param name="strDateFormat">�t�H�[�}�b�g������</param>
    ''' <returns>�ϊ���̕�����</returns>
    ''' <remarks></remarks>
    Public Function dtTime2Str(ByVal dtValue As Date, Optional ByVal strDateFormat As String = "") As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "dtTime2Str" _
                                            , dtValue _
                                            , strDateFormat)
        Dim strRet As String
        If dtValue = DateTime.MinValue Then
            strRet = ""
        Else
            If strDateFormat <> String.Empty Then
                strRet = dtValue.ToString(strDateFormat)
            Else
                strRet = dtValue.ToString("yyyy/MM/dd")
            End If
        End If
        Return strRet
    End Function

    ''' <summary>
    ''' ������𐔒l�^�ɕϊ����܂�(NumberStyles �񋓑̂��g�p)
    ''' </summary>
    ''' <param name="strValue">�ϊ�������������</param>
    ''' <returns>�ϊ���̐��l</returns>
    ''' <param name="NumStyle">NumberStyles �񋓑�</param>
    ''' <remarks></remarks>
    Public Function str2Int(ByVal strValue As String, ByVal NumStyle As System.Globalization.NumberStyles) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2Int" _
                                            , strValue _
                                            , NumStyle)
        Dim intRet As Integer
        If strValue Is Nothing OrElse strValue.Length = 0 Then
            intRet = 0
        Else
            If IsNumeric(strValue) Then
                intRet = Integer.Parse(strValue, NumStyle)
            Else
                intRet = 0
            End If
        End If
        Return intRet
    End Function

    ''' <summary>
    ''' ������𐔒l�^�ɕϊ����܂�
    ''' </summary>
    ''' <param name="strValue">�ϊ�������������</param>
    ''' <returns>�ϊ���̐��l</returns>
    ''' <remarks></remarks>
    Public Function str2Int(ByVal strValue As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2Int" _
                                            , strValue)
        Dim intRet As Integer
        If strValue Is Nothing OrElse strValue.Length = 0 Then
            intRet = 0
        Else
            If IsNumeric(strValue) Then
                intRet = Integer.Parse(strValue)
            Else
                intRet = 0
            End If
        End If
        Return intRet
    End Function

    ''' <summary>
    ''' ������𐔒l�^�ɕϊ����܂�(NumberStyles �񋓑̂��g�p)
    ''' </summary>
    ''' <param name="strValue">�ϊ�������������</param>
    ''' <returns>�ϊ���̐��l</returns>
    ''' <param name="NumStyle">NumberStyles �񋓑�</param>
    ''' <remarks></remarks>
    Public Function str2Long(ByVal strValue As String, ByVal NumStyle As System.Globalization.NumberStyles) As Long
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "str2Long" _
                                            , strValue _
                                            , NumStyle)
        Dim lngRet As Long
        If strValue Is Nothing OrElse strValue.Length = 0 Then
            lngRet = 0
        Else
            If IsNumeric(strValue) Then
                lngRet = Long.Parse(strValue, NumStyle)
            Else
                lngRet = 0
            End If
        End If
        Return lngRet
    End Function

    ''' <summary>
    ''' ������𐔒l�^�ɕϊ����܂�
    ''' </summary>
    ''' <param name="strValue">�ϊ�������������</param>
    ''' <returns>�ϊ���̐��l</returns>
    ''' <remarks></remarks>
    Public Function str2Long(ByVal strValue As String) As Long
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2Long" _
                                            , strValue)
        Dim lngRet As Long
        If strValue Is Nothing OrElse strValue.Length = 0 Then
            lngRet = 0
        Else
            If IsNumeric(strValue) Then
                lngRet = Long.Parse(strValue)
            Else
                lngRet = 0
            End If
        End If
        Return lngRet
    End Function

    ''' <summary>
    ''' ���l�𕶎���ɕϊ����܂�
    ''' </summary>
    ''' <param name="intValue">�ϊ����������l</param>
    ''' <returns>�ϊ���̕�����</returns>
    ''' <param name="strNumFormat"></param>
    ''' <remarks></remarks>
    Public Function int2Str(ByVal intValue As String, Optional ByVal strNumFormat As String = "") As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "int2Str" _
                                                    , intValue _
                                                    , strNumFormat)
        Dim strRet As String

        If intValue Is Nothing Then
            strRet = ""
        Else
            If strNumFormat <> String.Empty Then
                strRet = intValue.ToString
            Else
                strRet = intValue.ToString(strNumFormat)
            End If
        End If
        Return strRet
    End Function

    ''' <summary>
    ''' ������3����؂�̕�����ɕύX���܂�
    ''' </summary>
    ''' <param name="strNumber">�ϊ�����������</param>
    ''' <returns>�ϊ���̕�����</returns>
    ''' <remarks></remarks>
    Public Function strNum2Str(ByVal strNumber As String) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "strNum2Str" _
                                                , strNumber)
        Dim strRet As String
        If strNumber <> "" AndAlso IsNumeric(strNumber) Then
            strRet = Format(CLng(strNumber), EarthConst.FORMAT_KINGAKU_1)
        Else
            strRet = strNumber
        End If
        Return strRet
    End Function

#End Region

End Class
