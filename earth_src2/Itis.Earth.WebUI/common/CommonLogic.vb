''' <summary>
''' ���ʏ����N���X(�ÓI�C���X�^���X)
''' </summary>
''' <remarks>���̃N���X�ɂ̓A�v���P�[�V�����ŋ��L���鏈���ȊO�������Ȃ���</remarks>
Public Class CommonLogic

    '�ÓI�ϐ��Ƃ��ăN���X�^�̃C���X�^���X�𐶐�
    Private Shared _instance = New CommonLogic()

    '�ÓI�֐��Ƃ��ăN���X�^�̃C���X�^���X��Ԃ��֐���p��
    Public Shared ReadOnly Property Instance() As CommonLogic
        Get
            '�ÓI�ϐ����������Ă����ꍇ�̂݁A�C���X�^���X�𐶐�����
            If IsDBNull(_instance) Then
                _instance = New CommonLogic()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' ��ʕ\���p�ɃI�u�W�F�N�g�𕶎���ϊ�����
    ''' </summary>
    ''' <param name="obj">�\���Ώۂ̃f�[�^</param>
    ''' <param name="str">���ݒ莞�̏����l</param>
    ''' <returns>�\���`���̕�����</returns>
    ''' <remarks>
    ''' Decimal  : Minvalue���󔒁A����ȊO�͓��͒l��String�^�ŕԋp<br/>
    ''' Integer  : Minvalue���󔒁A����ȊO�͓��͒l��String�^�ŕԋp<br/>
    ''' DateTime : MinDateTime���󔒁A����ȊO�͓��͒l�� YYYY/MM/DD �`����String�^�ŕԋp<br/>
    ''' ��L�ȊO : ���̂܂܁B�K�X�ǉ����Ă�������
    ''' </remarks>
    Public Function getDisplayString(ByVal obj As Object, Optional ByVal str As String = "") As String

        ' �߂�l�ƂȂ�String�f�[�^
        Dim ret As String = str

        If obj Is Nothing Then
            ' NULL �͊�{�I�ɋ󔒂�Ԃ�
            Return ret
        ElseIf obj.GetType().ToString() = GetType(String).ToString Then
            ' String�̏ꍇ
            If obj = "" Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(Integer).ToString Then
            ' Integer�̏ꍇ
            If obj = Integer.MinValue Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimal�̏ꍇ
            If obj = Decimal.MinValue Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTime�̏ꍇ
            If obj = DateTime.MinValue Then
                Return ret
            Else
                ret = Format(obj, "yyyy/MM/dd")
            End If
        Else
            ret = obj.ToString()
        End If

        Return ret

    End Function

    ''' <summary>
    ''' ��ʕ\���p��������w�肵���^�ɕϊ�����
    ''' </summary>
    ''' <param name="strData">�ϊ��Ώۂ̃f�[�^</param>
    ''' <param name="objChangeData">�ϊ���̌^�f�[�^�i�Q�Ɓj</param>
    ''' <returns>�ϊ���̌^�f�[�^</returns>
    ''' <remarks>
    ''' Decimal  : �󔒂�Minvalue�A����ȊO�͓��͒l��Decimal�ɕϊ�<br/>
    ''' Integer  : �󔒂�Minvalue�A����ȊO�͓��͒l��Integer�ɕϊ�<br/>
    ''' DateTime : �󔒂�Minvalue�A����ȊO�͓��͒l��DateTime�ɕϊ�<br/>
    ''' ��L�ȊO : ���̂܂܁B�K�X�ǉ����Ă�������<br/>
    ''' �ϊ��Ɏ��s�����ꍇ��False��Ԃ��A�w��^��MinValue���Z�b�g���܂�
    ''' </remarks>
    Public Function setDisplayString(ByVal strData As String, _
                                     ByRef objChangeData As Object) As Boolean


        If objChangeData.GetType().ToString() = GetType(Integer).ToString Then
            ' Integer�֕ϊ�
            If strData.Trim() = "" Then
                objChangeData = Integer.MinValue
                Return True
            Else
                Try
                    objChangeData = Integer.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' �ϊ��Ɏ��s�����ꍇ�AMinValue��ݒ肵�AFalse��ԋp
                    objChangeData = Integer.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimal�֕ϊ�
            If strData.Trim() = "" Then
                objChangeData = Decimal.MinValue
                Return True
            Else
                Try
                    objChangeData = Decimal.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' �ϊ��Ɏ��s�����ꍇ�AMinValue��ݒ肵�AFalse��ԋp
                    objChangeData = Decimal.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTime�֕ϊ�
            If strData.Trim() = "" Then
                objChangeData = DateTime.MinValue
                Return True
            Else
                Try
                    objChangeData = DateTime.Parse(strData)
                    Return True
                Catch ex As Exception
                    ' �ϊ��Ɏ��s�����ꍇ�AMinValue��ݒ肵�AFalse��ԋp
                    objChangeData = DateTime.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(String).ToString Then
            ' String��Trim����
            objChangeData = strData.Trim()
            Return True
        End If

        ' �ϊ��ΏۈȊO�̌^�̓G���[
        objChangeData = Nothing
        Return False

    End Function
    Public Function CreateHeadDataSource(ByVal strCol As String) As DataTable
        Dim intCols As Integer = 0
        Dim intColCount As Integer = 0
        Dim dtHeader As New DataTable
        Dim drTemp As DataRow
        intCols = Split(strCol, ",").Length - 1
        For intColCount = 0 To intCols
            dtHeader.Columns.Add(New DataColumn("col" & intColCount.ToString, GetType(String)))
        Next
        drTemp = dtHeader.NewRow
        
        With drTemp
            For intColCount = 0 To intCols
                .Item(intColCount) = Split(strCol, ",")(intColCount)

            Next

        End With

        dtHeader.Rows.Add(drTemp)

        Return dtHeader
    End Function
End Class
