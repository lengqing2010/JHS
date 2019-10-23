Imports System.Text
Imports System.Reflection

''' <summary>
''' レコードクラスより削除SQL文を自動生成するヘルパークラスです
''' </summary>
''' <remarks>削除SQL作成に必要なパラメータ情報も作成します<br/>
''' 本処理に関係する TableMapAttribute の説明<br/>
''' 　・DeleteKey  : 削除キーのレコードにTrueを設定します<br/>
''' 　・SqlType   : SQLの項目属性を指定します<br/>
''' 　・SqlLength : 項目の桁数を指定します（文字列時必須）</remarks>
Public Class DeleteStringHelper
    Implements ISqlStringHelper

    Dim dataLogic As New DataLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' レコードクラスのプロパティ情報よりDelete文を生成する
    ''' </summary>
    ''' <param name="recordType">設定対象のレコードタイプ</param>
    ''' <param name="row">削除データレコード</param>
    ''' <param name="paramList">パラメータレコードのリスト</param>
    ''' <param name="recordTypeRow">削除データレコードタイプ</param>
    ''' <returns>削除SQL文</returns>
    ''' <remarks></remarks>
    Public Function MakeUpdateInfo(ByVal recordType As Type, _
                                   ByVal row As Object, _
                                   ByRef paramList As List(Of ParamRecord), _
                                   Optional ByVal recordTypeRow As Type = Nothing) As String Implements ISqlStringHelper.MakeUpdateInfo

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeDeleteInfo", _
                                            recordType, _
                                            row, _
                                            paramList)

        '削除データレコードタイプが未指定の場合、設定対象のレコードタイプを使用（デフォルト）
        If recordTypeRow Is Nothing Then
            recordTypeRow = recordType
        End If

        ' 削除対象のテーブルID
        Dim tableName As String = ""
        ' 削除項目部分
        Dim setItemString As String = ""
        ' 削除条件部分
        Dim keyString As New StringBuilder
        ' パラメータ情報のリスト
        paramList = New List(Of ParamRecord)

        ' 設定元レコードのプロパティ情報
        Dim classList() As Object = recordType.GetCustomAttributes(GetType(TableClassMapAttribute), False)
        For Each classItem As TableClassMapAttribute In classList
            tableName = classItem.TableName
        Next

        ' テーブル名が取得できない場合、削除できないので処理終了
        If tableName = "" Then
            Return ""
        End If

        ' 基本となるUPDATE文
        Dim deleteString As String = "DELETE FROM " & tableName & " {0}"

        ' 設定元レコードのプロパティ情報
        Dim propertyInfo As System.Reflection.PropertyInfo
        ' 設定先のプロパティ情報分ループし、同一項目を探す
        For Each propertyInfo In recordType.GetProperties

            Dim list() As Object = propertyInfo.GetCustomAttributes(GetType(TableMapAttribute), False)

            ' カスタムアトリビュート分処理を行う（この処理の場合は１件しかない為１回のみ）
            For Each item As TableMapAttribute In list

                If item.DeleteKey = True Then

                    Dim paramRec As New ParamRecord
                    Dim paramText As String = "@" & StrConv(propertyInfo.Name, VbStrConv.Uppercase) & "K" ' KEYはKを付加

                    ' Key項目の場合はWhere条件に付加する
                    If keyString.ToString() = "" Then
                        keyString.Append(" WHERE " & item.ItemName & " = " & paramText & " ")
                    Else
                        keyString.Append(" AND " & item.ItemName & " = " & paramText & " ")
                    End If

                    ' 設定元データの取得
                    Dim itemData As Object
                    Try
                        itemData = recordTypeRow.InvokeMember(propertyInfo.Name, _
                                    BindingFlags.GetProperty, _
                                    Nothing, _
                                    row, _
                                    Nothing)

                        ' 未入力データはDBNullにする
                        dataLogic.subMinValueToDBNull(itemData)

                        ' 削除日付の特殊ルール(排他処理用)
                        If propertyInfo.Name = "UpdDatetime" Then
                            ' 設定するのはシステム日付
                            itemData = DateTime.Now
                        End If

                    Catch ex As Exception
                        ' 取得に失敗した場合、設定しない
                        Exit For
                    End Try

                    ' パラメータをパラメータ設定用レコードに保持
                    paramRec.Param = paramText
                    paramRec.DbType = item.SqlType
                    paramRec.ParamLength = item.SqlLength
                    paramRec.SetData = itemData

                    paramList.Add(paramRec)

                End If
            Next
        Next

        deleteString = String.Format(deleteString, keyString)

        Return deleteString

    End Function

    ''' <summary>
    ''' レコードクラスのプロパティ情報より排他チェック用のSQL文を生成する
    ''' </summary>
    ''' <param name="recordType">設定対象のレコードタイプ</param>
    ''' <param name="row">排他チェック用データレコード</param>
    ''' <param name="paramList">パラメータレコードのリスト</param>
    ''' <returns>排他チェックSQL文</returns>
    ''' <remarks>排他チェック用SQLは削除日付の異なる場合にユーザID、削除日付を返します<br/>
    '''          ★レコードクラスの IsDelete アトリビュート True の項目をSQLで取得します<br/>
    '''          削除日時はupd_datetimeで有る事が前提です</remarks>
    Public Function MakeHaitaSQLInfo(ByVal recordType As Type, _
                                   ByVal row As Object, _
                                   ByRef paramList As List(Of ParamRecord)) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeHaitaSQLInfo", _
                                            recordType, _
                                            row, _
                                            paramList)

        ' 削除対象のテーブルID
        Dim tableName As String = ""
        ' 削除項目部分
        Dim setItemString As String = ""
        ' 削除条件部分
        Dim keyString As String = ""
        ' パラメータ情報のリスト
        paramList = New List(Of ParamRecord)

        ' 設定元レコードのプロパティ情報
        Dim classList() As Object = recordType.GetCustomAttributes(GetType(TableClassMapAttribute), False)
        For Each class_item As TableClassMapAttribute In classList
            tableName = class_item.TableName
        Next

        ' テーブル名が取得できない場合、削除できないので処理終了
        If tableName = "" Then
            Return ""
        End If

        ' 基本となるSELECT文
        Dim sqlString As String = "SELECT {0} FROM " & tableName & " {1}"

        ' 設定元レコードのプロパティ情報
        Dim propertyInfo As System.Reflection.PropertyInfo
        ' 設定先のプロパティ情報分ループし、同一項目を探す
        For Each propertyInfo In recordType.GetProperties

            Dim list() As Object = propertyInfo.GetCustomAttributes(GetType(TableMapAttribute), False)

            ' カスタムアトリビュート分処理を行う（この処理の場合は１件しかない為１回のみ）
            For Each item As TableMapAttribute In list

                Dim paramRec As New ParamRecord
                Dim paramText As String = "@" & StrConv(propertyInfo.Name, VbStrConv.Uppercase)

                If item.IsKey = True Then

                    ' Key項目の場合はWhere条件に付加する
                    If keyString = "" Then
                        keyString = " WHERE upd_datetime IS NOT NULL AND "
                    Else
                        keyString = keyString & " AND "
                    End If

                    If item.ItemName = "upd_datetime" Then
                        ' 削除日時は異なるものを検索する
                        keyString = keyString & item.ItemName & " <> " & paramText & " "
                    Else
                        keyString = keyString & item.ItemName & " = " & paramText & " "
                    End If

                    ' 設定元データの取得
                    Dim itemData As Object
                    Try
                        itemData = recordType.InvokeMember(propertyInfo.Name, _
                                    BindingFlags.GetProperty, _
                                    Nothing, _
                                    row, _
                                    Nothing)

                        ' 未入力データはDBNullにする
                        dataLogic.subMinValueToDBNull(itemData)

                    Catch ex As Exception
                        ' 取得に失敗した場合、設定しない
                        Exit For
                    End Try

                    ' パラメータをパラメータ設定用レコードに保持
                    paramRec.Param = paramText
                    paramRec.DbType = item.SqlType
                    paramRec.ParamLength = item.SqlLength
                    paramRec.SetData = itemData

                    paramList.Add(paramRec)

                End If

            Next
        Next

        sqlString = String.Format(sqlString, setItemString, keyString)

        Return sqlString

    End Function

End Class