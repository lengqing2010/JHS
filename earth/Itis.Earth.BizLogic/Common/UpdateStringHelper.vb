Imports System.Reflection

''' <summary>
''' ���R�[�h�N���X���X�VSQL����������������w���p�[�N���X�ł�
''' </summary>
''' <remarks>�X�VSQL�쐬�ɕK�v�ȃp�����[�^�����쐬���܂�<br/>
''' �{�����Ɋ֌W���� TableMapAttribute �̐���<br/>
''' �@�EIsKey     : �X�VKey�ƂȂ郌�R�[�h��True��ݒ肵�܂�<br/>
''' �@�EIsUpdate  : �X�V�Ώۂ̃��R�[�h��True��ݒ肵�܂�<br/>
''' �@�ESqlType   : SQL�̍��ڑ������w�肵�܂�<br/>
''' �@�ESqlLength : ���ڂ̌������w�肵�܂��i�����񎞕K�{�j</remarks>
Public Class UpdateStringHelper
    Implements ISqlStringHelper

    Dim dataLogic As New DataLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' ���R�[�h�N���X�̃v���p�e�B�����Update���𐶐�����
    ''' </summary>
    ''' <param name="recordType">�ݒ�Ώۂ̃��R�[�h�^�C�v</param>
    ''' <param name="row">�X�V�f�[�^���R�[�h</param>
    ''' <param name="paramList">�p�����[�^���R�[�h�̃��X�g</param>
    ''' <param name="recordTypeRow">�X�V�f�[�^���R�[�h�^�C�v</param>
    ''' <returns>�X�VSQL��</returns>
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

        '�X�V�f�[�^���R�[�h�^�C�v�����w��̏ꍇ�A�ݒ�Ώۂ̃��R�[�h�^�C�v���g�p�i�f�t�H���g�j
        If recordTypeRow Is Nothing Then
            recordTypeRow = recordType
        End If

        ' �X�V�Ώۂ̃e�[�u��ID
        Dim tableName As String = ""
        ' �X�V���ڕ���
        Dim setItemString As String = ""
        ' �X�V��������
        Dim keyString As String = ""
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

        ' ��{�ƂȂ�UPDATE��
        Dim updateString As String = "UPDATE " & tableName & " SET {0} {1}"

        ' �ݒ茳���R�[�h�̃v���p�e�B���
        Dim propertyInfo As System.Reflection.PropertyInfo
        ' �ݒ��̃v���p�e�B��񕪃��[�v���A���ꍀ�ڂ�T��
        For Each propertyInfo In recordType.GetProperties

            Dim list() As Object = propertyInfo.GetCustomAttributes(GetType(TableMapAttribute), False)

            ' �J�X�^���A�g���r���[�g���������s���i���̏����̏ꍇ�͂P�������Ȃ��ׂP��̂݁j
            For Each item As TableMapAttribute In list

                If item.IsKey = True Then

                    Dim paramRec As New ParamRecord
                    Dim paramText As String = "@" & StrConv(propertyInfo.Name, VbStrConv.Uppercase) & "K" ' KEY��K��t��

                    ' Key���ڂ̏ꍇ��Where�����ɕt������
                    If keyString = "" Then
                        keyString = " WHERE "
                    Else
                        keyString = keyString & " AND "
                    End If

                    ' ���ꃋ�[��
                    If propertyInfo.Name = "UpdDatetime" Then
                        ' �S�e�[�u��.�X�V���t�̓��ꃋ�[��(NULL�͏���X�V�Ȃ̂ő��Ⴕ�Ă�OK)
                        keyString = keyString & " ( upd_datetime IS NULL OR " & _
                                                item.ItemName & " = " & paramText & ") "
                    ElseIf recordType Is GetType(TeibetuSeikyuuRecord) AndAlso propertyInfo.Name = "BunruiCd" Then
                        ' �@�ʐ���.���ރR�[�h�X�V���̓��ꃋ�[��[���ރR�[�h�͑O��2����v]
                        keyString = keyString & item.ItemName & " LIKE " & paramText & " "
                    Else
                        keyString = keyString & item.ItemName & " = " & paramText & " "
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

                        ' �@�ʐ���.���ރR�[�h�X�V���̓��ꃋ�[��
                        If itemData IsNot DBNull.Value AndAlso _
                           recordType Is GetType(TeibetuSeikyuuRecord) AndAlso _
                           propertyInfo.Name = "BunruiCd" Then
                            ' ���i�Q(110�A115)�����[���ރR�[�h�͑O��2����v]
                            If itemData.ToString = EarthConst.SOUKO_CD_SYOUHIN_2_110 Or itemData.ToString = EarthConst.SOUKO_CD_SYOUHIN_2_115 Then
                                itemData = itemData.ToString.Substring(0, 2) & "_"
                            End If
                        End If

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

                ' UPDATE�Ώۂ̏ꍇ�͍��ڂ�ǉ�
                If item.IsUpdate = True Then

                    Dim paramRec As New ParamRecord
                    Dim paramText As String = "@" & StrConv(propertyInfo.Name, VbStrConv.Uppercase)

                    If setItemString = "" Then
                        setItemString = " " & item.ItemName & " = " & paramText & " "
                    Else
                        setItemString = setItemString & "," & item.ItemName & " = " & paramText & " "
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

                        ' �X�V���t�̓��ꃋ�[��(�r�������p)
                        If propertyInfo.Name = "UpdDatetime" Then
                            ' �ݒ肷��̂̓V�X�e�����t
                            itemData = DateTime.Now
                        End If

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

        updateString = String.Format(updateString, setItemString, keyString)

        Return updateString

    End Function

    ''' <summary>
    ''' ���R�[�h�N���X�̃v���p�e�B�����r���`�F�b�N�p��SQL���𐶐�����
    ''' </summary>
    ''' <param name="recordType">�ݒ�Ώۂ̃��R�[�h�^�C�v</param>
    ''' <param name="row">�r���`�F�b�N�p�f�[�^���R�[�h</param>
    ''' <param name="paramList">�p�����[�^���R�[�h�̃��X�g</param>
    ''' <returns>�r���`�F�b�NSQL��</returns>
    ''' <remarks>�r���`�F�b�N�pSQL�͍X�V���t�̈قȂ�ꍇ�Ƀ��[�UID�A�X�V���t��Ԃ��܂�<br/>
    '''          �����R�[�h�N���X�� IsUpdate �A�g���r���[�g True �̍��ڂ�SQL�Ŏ擾���܂�<br/>
    '''          �X�V������upd_datetime�ŗL�鎖���O��ł�</remarks>
    Public Function MakeHaitaSQLInfo(ByVal recordType As Type, _
                                   ByVal row As Object, _
                                   ByRef paramList As List(Of ParamRecord)) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeHaitaSQLInfo", _
                                            recordType, _
                                            row, _
                                            paramList)

        ' �X�V�Ώۂ̃e�[�u��ID
        Dim tableName As String = ""
        ' �X�V���ڕ���
        Dim setItemString As String = ""
        ' �X�V��������
        Dim keyString As String = ""
        ' �p�����[�^���̃��X�g
        paramList = New List(Of ParamRecord)

        ' �ݒ茳���R�[�h�̃v���p�e�B���
        Dim classList() As Object = recordType.GetCustomAttributes(GetType(TableClassMapAttribute), False)
        For Each class_item As TableClassMapAttribute In classList
            tableName = class_item.TableName
        Next

        ' �e�[�u�������擾�ł��Ȃ��ꍇ�A�X�V�ł��Ȃ��̂ŏ����I��
        If tableName = "" Then
            Return ""
        End If

        ' ��{�ƂȂ�SELECT��
        Dim sqlString As String = "SELECT {0} FROM " & tableName & " WITH (UPDLOCK) {1}"

        ' �ݒ茳���R�[�h�̃v���p�e�B���
        Dim propertyInfo As System.Reflection.PropertyInfo
        ' �ݒ��̃v���p�e�B��񕪃��[�v���A���ꍀ�ڂ�T��
        For Each propertyInfo In recordType.GetProperties

            Dim list() As Object = propertyInfo.GetCustomAttributes(GetType(TableMapAttribute), False)

            ' �J�X�^���A�g���r���[�g���������s���i���̏����̏ꍇ�͂P�������Ȃ��ׂP��̂݁j
            For Each item As TableMapAttribute In list

                Dim paramRec As New ParamRecord
                Dim paramText As String = "@" & StrConv(propertyInfo.Name, VbStrConv.Uppercase)

                If item.IsKey = True Then

                    ' Key���ڂ̏ꍇ��Where�����ɕt������
                    If keyString = "" Then
                        keyString = " WHERE upd_datetime IS NOT NULL AND "
                    Else
                        keyString = keyString & " AND "
                    End If

                    If item.ItemName = "upd_datetime" Then
                        ' �X�V�����͈قȂ���̂���������
                        keyString = keyString & item.ItemName & " <> " & paramText & " "
                    Else
                        keyString = keyString & item.ItemName & " = " & paramText & " "
                    End If

                    ' �ݒ茳�f�[�^�̎擾
                    Dim itemData As Object
                    Try
                        itemData = recordType.InvokeMember(propertyInfo.Name, _
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

                ' UPDATE�Ώۂ̃A�g���r���[�g�͔r���`�F�b�N�̎擾�ΏۂƂ��Ēǉ�
                If item.IsUpdate = True Then

                    If setItemString = "" Then
                        setItemString = " " & item.ItemName & " "
                    Else
                        setItemString = setItemString & "," & item.ItemName & " "
                    End If

                End If
            Next
        Next

        sqlString = String.Format(sqlString, setItemString, keyString)

        Return sqlString

    End Function

End Class