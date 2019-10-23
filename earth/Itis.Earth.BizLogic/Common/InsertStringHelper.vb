Imports System.Text
Imports System.Reflection

''' <summary>
''' レコードクラスより登録SQL文を自動生成するヘルパークラスです
''' </summary>
''' <remarks>登録SQL作成に必要なパラメータ情報も作成します<br/>
''' 本処理に関係する TableMapAttribute の説明<br/>
''' 　・IsInsert  : 登録対象のレコードにTrueを設定します。★キーは必ずTrueにして下さい<br/>
''' 　・SqlType   : SQLの項目属性を指定します<br/>
''' 　・SqlLength : 項目の桁数を指定します（文字列時必須）</remarks>
Public Class InsertStringHelper
    Implements ISqlStringHelper

    Dim dataLogic As New DataLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' レコードクラスのプロパティ情報よりInsert文を生成する
    ''' </summary>
    ''' <param name="recordType">設定対象のレコードタイプ</param>
    ''' <param name="row">登録データレコード</param>
    ''' <param name="paramList">パラメータレコードのリスト</param>
    ''' <param name="recordTypeRow">登録データレコードタイプ</param>
    ''' <returns>登録SQL文</returns>
    ''' <remarks></remarks>
    Public Function MakeUpdateInfo(ByVal recordType As Type, _
                                   ByVal row As Object, _
                                   ByRef paramList As List(Of ParamRecord), _
                                   Optional ByVal recordTypeRow As Type = Nothing) As String Implements ISqlStringHelper.MakeUpdateInfo

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeUpdateInfo", _
                                            recordType, _
                                            row, _
                                            paramList)

        '登録データレコードタイプが未指定の場合、設定対象のレコードタイプを使用（デフォルト）
        If recordTypeRow Is Nothing Then
            recordTypeRow = recordType
        End If

        ' 登録対象のテーブルID
        Dim tableName As String = ""
        ' 登録項目部分
        Dim setItemString As New StringBuilder
        ' パラメータ項目部分
        Dim setParamString As New StringBuilder
        ' パラメータ情報のリスト
        paramList = New List(Of ParamRecord)

        ' 設定元レコードのプロパティ情報
        Dim classList() As Object = recordType.GetCustomAttributes(GetType(TableClassMapAttribute), False)
        For Each classItem As TableClassMapAttribute In classList
            tableName = classItem.TableName
        Next

        ' テーブル名が取得できない場合、更新できないので処理終了
        If tableName = "" Then
            Return ""
        End If

        ' 基本となるInsert文
        Dim insertString As String = "INSERT INTO " & tableName & "( {0} ) VALUES ({1})"

        ' 設定元レコードのプロパティ情報
        Dim propertyInfo As System.Reflection.PropertyInfo
        ' 設定先のプロパティ情報分ループし、同一項目を探す
        For Each propertyInfo In recordType.GetProperties

            Dim list() As Object = propertyInfo.GetCustomAttributes(GetType(TableMapAttribute), False)

            ' カスタムアトリビュート分処理を行う（この処理の場合は１件しかない為１回のみ）
            For Each item As TableMapAttribute In list

                ' Insert対象の場合は項目を追加
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

        insertString = String.Format(insertString, setItemString, setParamString)

        Return insertString

    End Function

End Class