Imports System.Text
Imports System.Reflection

''' <summary>
''' ���R�[�h�N���X���o�^SQL����������������w���p�[�N���X�ł�
''' </summary>
''' <remarks>�o�^SQL�쐬�ɕK�v�ȃp�����[�^�����쐬���܂�<br/>
''' �{�����Ɋ֌W���� TableMapAttribute �̐���<br/>
''' �@�EIsInsert  : �o�^�Ώۂ̃��R�[�h��True��ݒ肵�܂��B���L�[�͕K��True�ɂ��ĉ�����<br/>
''' �@�ESqlType   : SQL�̍��ڑ������w�肵�܂�<br/>
''' �@�ESqlLength : ���ڂ̌������w�肵�܂��i�����񎞕K�{�j</remarks>
Public Class InsertStringHelper
    Implements ISqlStringHelper

    Dim dataLogic As New DataLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' ���R�[�h�N���X�̃v���p�e�B�����Insert���𐶐�����
    ''' </summary>
    ''' <param name="recordType">�ݒ�Ώۂ̃��R�[�h�^�C�v</param>
    ''' <param name="row">�o�^�f�[�^���R�[�h</param>
    ''' <param name="paramList">�p�����[�^���R�[�h�̃��X�g</param>
    ''' <param name="recordTypeRow">�o�^�f�[�^���R�[�h�^�C�v</param>
    ''' <returns>�o�^SQL��</returns>
    ''' <remarks></remarks>
    Public Function MakeUpdateInfo(ByVal recordType As Type, _
                                   ByVal row As Object, _
                                   ByRef paramList As List(Of ParamRecord), _
                                   Optional ByVal recordTypeRow As Type = Nothing) As String Implements ISqlStringHelper.MakeUpdateInfo

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeUpdateInfo", _
                                            recordType, _
                                            row, _
                                            paramList)

        '�o�^�f�[�^���R�[�h�^�C�v�����w��̏ꍇ�A�ݒ�Ώۂ̃��R�[�h�^�C�v���g�p�i�f�t�H���g�j
        If recordTypeRow Is Nothing Then
            recordTypeRow = recordType
        End If

        ' �o�^�Ώۂ̃e�[�u��ID
        Dim tableName As String = ""
        ' �o�^���ڕ���
        Dim setItemString As New StringBuilder
        ' �p�����[�^���ڕ���
        Dim setParamString As New StringBuilder
        ' �p�����[�^���̃��X�g
        paramList = New List(Of ParamRecord)

        ' �ݒ茳���R�[�h�̃v���p�e�B���
        Dim classList() As Object = recordType.GetCustomAttributes(GetType(TableClassMapAttribute), False)
        For Each classItem As TableClassMapAttribute In classList
            tableName = classItem.TableName
        Next

        ' �e�[�u�������擾�ł��Ȃ��ꍇ�A�X�V�ł��Ȃ��̂ŏ����I��
        If tableName = "" Then
            Return ""
        End If

        ' ��{�ƂȂ�Insert��
        Dim insertString As String = "INSERT INTO " & tableName & "( {0} ) VALUES ({1})"

        ' �ݒ茳���R�[�h�̃v���p�e�B���
        Dim propertyInfo As System.Reflection.PropertyInfo
        ' �ݒ��̃v���p�e�B��񕪃��[�v���A���ꍀ�ڂ�T��
        For Each propertyInfo In recordType.GetProperties

            Dim list() As Object = propertyInfo.GetCustomAttributes(GetType(TableMapAttribute), False)

            ' �J�X�^���A�g���r���[�g���������s���i���̏����̏ꍇ�͂P�������Ȃ��ׂP��̂݁j
            For Each item As TableMapAttribute In list

                ' Insert�Ώۂ̏ꍇ�͍��ڂ�ǉ�
                If item.IsInsert = True Then

                    Dim paramRec As New ParamRecord
                    Dim paramText As String = "@" & StrConv(propertyInfo.Name, VbStrConv.Uppercase)

                    If setItemString.ToString() = "" Then
                        setItemString.Append(item.ItemName)
                        setParamString.Append(paramText)
                    Else
                        setItemString.Append("," & item.ItemName)
                        setParamString.Append("," & paramText)
                    End If

                    ' �ݒ茳�f�[�^�̎擾
                    Dim itemData As Object
                    Try
                        itemData = recordTypeRow.InvokeMember(propertyInfo.Name, _
                                    BindingFlags.GetProperty, _
                                    Nothing, _
                                    row, _
                                    Nothing)

                        ' �����̓f�[�^��DBNull�ɂ���
                        dataLogic.subMinValueToDBNull(itemData)

                    Catch ex As Exception
                        ' �擾�Ɏ��s�����ꍇ�A�ݒ肵�Ȃ�
                        Exit For
                    End Try

                    ' �p�����[�^���p�����[�^�ݒ�p���R�[�h�ɕێ�
                    paramRec.Param = paramText
                    paramRec.DbType = item.SqlType
                    paramRec.ParamLength = item.SqlLength
                    paramRec.SetData = itemData

                    paramList.Add(paramRec)

                End If
            Next
        Next

        insertString = String.Format(insertString, setItemString, setParamString)

        Return insertString

    End Function

End Class