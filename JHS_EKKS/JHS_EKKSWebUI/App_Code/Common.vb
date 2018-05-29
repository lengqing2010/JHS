Imports Microsoft.VisualBasic
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Public Class Common
    Private CommonBC As New CommonBC

#Region "�萔"
    ''' <summary>
    ''' ���̊��Ԕ͈�
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MonthKikan As Integer
        All = 1                     '�S�N�x
        MonthTopHalf = 2            '���(4�� - 9��)
        MonthDownHalf = 3           '����(10�� - 3��)
        Month4_6 = 4                '�l����(4,5,6��)
        Month7_9 = 5                '�l����(7,8,9��)
        Month10_12 = 6              '�l����(10,11,12��)
        Month1_3 = 7                '�l����(1,2,3��)
        Other = 0                   '���̑�����
    End Enum

#End Region

    ''' <summary>
    ''' �A�N�Z�X�L�^�̕ێ�
    ''' </summary>
    ''' <param name="objPage">�y�[�W</param>
    ''' <param name="strUserId">���[�U�[�R�[�h</param>
    ''' <param name="strSousa">����L�^</param>
    ''' <remarks></remarks>
    Public Sub SetURL(ByVal objPage As Object, ByVal strUserId As String, ByVal strSousa As String)
        EMAB.AddMethodEntrance(objPage.Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                       MyMethod.GetCurrentMethod.Name, objPage, strUserId, strSousa)
        CommonBC.SetUserInfo(objPage.Request.Url.OriginalString, strUserId, strSousa)
    End Sub
    ''' <summary>
    ''' �f�[�^�擾��
    ''' </summary>
    ''' <param name="objPage">�y�[�W</param>
    ''' <param name="functionName">�֐���</param>
    ''' <remarks></remarks>
    Public Function RunSub(ByVal objPage As Page, ByVal functionName As String) As Boolean

        EMAB.AddMethodEntrance(objPage.Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, objPage, functionName)

        Dim csScript As New StringBuilder
        Dim pPage As Page = objPage
        Dim pType As Type = pPage.GetType
        Dim fname As String = functionName.Replace("(", String.Empty).Replace(")", String.Empty).Replace(";", String.Empty).Trim
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod(fname)

        If Not methodInfo Is Nothing Then
            Return Convert.ToBoolean(methodInfo.Invoke(pPage, New Object() {}))
        End If
        Return False
    End Function
    ''' <summary>�V�X�e�����擾</summary>
    Public Function GetSystemDate() As Date
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyClass.GetType.BaseType.FullName & "." & _
                       MyMethod.GetCurrentMethod.Name)
        Return Convert.ToDateTime(CommonBC.SelSystemDate.Rows(0).Item(0).ToString)
    End Function

    ''' <summary>�V�X�e���̔N�x���擾(��v�N�x)</summary>
    Public Function GetSystemYear() As String
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyClass.GetType.BaseType.FullName & "." & _
               MyMethod.GetCurrentMethod.Name)
        Return Convert.ToString(DateAdd(DateInterval.Month, -3, Me.GetSystemDate()).Year)
    End Function

    ''' <summary>�V�X�e���̔N�x���擾(��v�N�x)</summary>
    Public Function GetSystemYear(ByVal dtSysDate As DateTime) As String
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyClass.GetType.BaseType.FullName & "." & _
               MyMethod.GetCurrentMethod.Name)
        Return Convert.ToString(DateAdd(DateInterval.Month, -3, dtSysDate).Year)
    End Function

    ''' <summary>
    ''' ���b�Z�[�W��\������
    ''' </summary>
    ''' <param name="objPage">���</param>
    ''' <param name="strMsgCd">���b�Z�[�W�ԍ�</param>
    ''' <param name="args">���b�Z�[�W�Q��</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/15�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Sub SetShowMessage(ByVal objPage As Page, ByVal strMsgCd As String, ByVal ParamArray args() As Object)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(objPage.Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                       MyMethod.GetCurrentMethod.Name, objPage, strMsgCd, args)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            '.AppendLine("setTimeout('fncShowMessage();',10);")
            '.AppendLine("function fncShowMessage()")
            '.AppendLine("{")
            .AppendLine("  alert('" & String.Format(strMsgCd, args) & "');")
            '.AppendLine("}")

            .AppendLine("</script>")
        End With

        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        objPage.ClientScript.RegisterStartupScript(objPage.GetType, strMsgCd, csScript.ToString)

    End Sub

    ''' <summary>
    ''' ���z���J���}��؂蕶����ŕ\������(�����_�l����)
    ''' </summary>
    ''' <param name="money">���z</param>
    ''' <param name="fixedpoint">�������̌���(�L�������ȉ��͐؎̂�)</param>
    ''' <returns>�t�H�[�}�b�g�㕶����</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/06/07 P-42186 ���F �V�K�쐬 </para>
    ''' </history>
    Public Function FormatComma(ByVal money As String, ByVal fixedpoint As Integer) As String

        If String.IsNullOrEmpty(money) = True Then

            Return String.Empty
        Else
            money = money.Replace("&nbsp;", String.Empty)
            Return FormatNumber(money, fixedpoint)
        End If

    End Function

    ''' <summary>
    ''' ���̊��Ԃ𔻒f����
    ''' </summary>
    ''' <param name="intFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMonthKikan(ByVal intFlg As MonthKikan) As ArrayList
        Dim intBeginMonth As Integer
        Dim intEndMonth As Integer

        Select Case intFlg
            Case MonthKikan.All
                intBeginMonth = 4
                intEndMonth = 3
            Case MonthKikan.MonthTopHalf
                intBeginMonth = 4
                intEndMonth = 9
            Case MonthKikan.MonthDownHalf
                intBeginMonth = 10
                intEndMonth = 3
            Case MonthKikan.Month1_3
                intBeginMonth = 1
                intEndMonth = 3
            Case MonthKikan.Month4_6
                intBeginMonth = 4
                intEndMonth = 6
            Case MonthKikan.Month7_9
                intBeginMonth = 7
                intEndMonth = 9
            Case MonthKikan.Month10_12
                intBeginMonth = 10
                intEndMonth = 12
        End Select

        Return GetMonthKikan(intBeginMonth, intEndMonth)
    End Function

    ''' <summary>
    ''' ���̊��Ԃ�߂�
    ''' </summary>
    ''' <param name="intBeginMonth">�J�n��</param>
    ''' <param name="intEndMonth">�I����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMonthKikan(ByVal intBeginMonth As Integer, ByVal intEndMonth As Integer) As ArrayList
        Dim arrList As ArrayList
        Dim intBeginM As Integer
        Dim intEndM As Integer
        Dim i As Integer

        arrList = New ArrayList
        If intBeginMonth > intEndMonth Then
            intBeginM = intBeginMonth
            intEndM = intEndMonth + 12
        Else
            intBeginM = intBeginMonth
            intEndM = intEndMonth
        End If

        For i = intBeginM To intEndM
            If i > 12 Then
                arrList.Add(i - 12)
            Else
                arrList.Add(i)
            End If
        Next

        Return arrList
    End Function

    ''' <summary>
    ''' ���l��ϊ�����
    ''' </summary>
    ''' <param name="objValue">���l�Ώ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLongFromObj(ByVal objValue As Object) As Int64
        If objValue Is Nothing Then
            Return 0
        End If

        If Convert.ToString(objValue).Equals(String.Empty) Then
            Return 0
        End If

        If Not IsNumeric(objValue) Then
            Return 0
        End If

        Return Convert.ToInt64(objValue)
    End Function

    ''' <summary>
    ''' ���l(����)��ϊ�����
    ''' </summary>
    ''' <param name="objValue">���l�Ώ�</param>
    ''' <param name="intNumCount">��������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDoubleFromObj(ByVal objValue As Object, Optional ByVal intNumCount As Integer = 1) As Double
        If objValue Is Nothing Then
            Return 0
        End If

        If Convert.ToString(objValue).Equals(String.Empty) Then
            Return 0
        End If

        If Not IsNumeric(objValue) Then
            Return 0
        End If

        Return Math.Round(Convert.ToDouble(objValue), intNumCount)
    End Function
End Class
