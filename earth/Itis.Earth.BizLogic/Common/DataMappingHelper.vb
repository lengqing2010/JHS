Imports System.Reflection
''' <summary>
''' DBの検索結果とレコードクラスのマッピングを行うヘルパークラスです(静的メンバ)
''' </summary>
''' <remarks></remarks>
Public Class DataMappingHelper

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New DataMappingHelper()

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As DataMappingHelper
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New DataMappingHelper()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' データテーブルに格納されたデータを
    ''' 指定したレコードタイプのListに変換します
    ''' </summary>
    ''' <param name="recordType">設定先レコードのタイプ</param>
    ''' <param name="table">設定対象のデータテーブル</param>
    ''' <returns>設定先レコードのGeneric List(of T)</returns>
    ''' <remarks>
    ''' <example>
    ''' 以下の例はDataAccessクラスより取得したDataTableの内容を<br/>
    ''' 特定レコードクラスのGeneric List(of T)として取得します
    ''' <code>
    ''' ’以下のようなレコードクラスを用意します
    ''' Public Class LoginUserInfo
    '''     ''' <summary>
    '''     ''' アカウントNO
    '''     ''' </summary>
    '''     <remarks></remarks>
    '''     Private intAccountNo As Integer
    '''     ''' <summary>
    '''     ''' アカウントNO
    '''     ''' </summary>
    '''     ''' <value></value>
    '''     ''' <returns>アカウントNO</returns>
    '''     ''' <remarks></remarks>
    '''     ＜TableMap("account_no")＞ _
    '''     Public Property AccountNo() As Integer
    '''         Get
    '''             Return intAccountNo
    '''         End Get
    '''         Set(ByVal value As Integer)
    '''             intAccountNo = value
    '''         End Set
    '''     End Property
    ''' End Class
    ''' </code>
    ''' 
    ''' ＜TableMap("@@@@@")＞ の @@@@@ 部分にDataTableの項目を設定します<br/>
    ''' 注意点として属性も同じにして下さい（＜＞は実際半角です）<br/><br/>
    ''' 
    ''' DB検索を行い、データテーブルを返却クラスがあると仮定し、取得データを<br/>
    ''' レコードにマッピングする例です
    ''' 
    ''' <code>
    ''' Private Sub xxxxx()
    ''' 
    '''     Dim data_access As New xxDataAccess 
    '''     Dim data_table As New DataTable
    ''' 
    '''     ' DBより値を取得しデータテーブルへセット
    '''     data_table = data_access.getTableData()
    ''' 
    '''     ' データマッピング用クラス
    '''     Dim helper As New DataMappingHelper
    '''     Dim record As New LoginUserInfo
    ''' 
    '''     ' 設定したいレコードのタイプと設定対象のDataTableを引数にデータ設定済のGeneric Listを取得
    '''     Dim list As List(of LoginUserInfo) = helper.getMapArray(of LoginUserInfo)(record.getType(), data_table)
    ''' 
    '''     ' 取得したデータで各種編集処理を行う
    '''     For Each record As LoginUserInfo.AccountTableRow In list
    '''         ' このプロパティには取得したデータが設定されている
    '''         record.AccountNo
    '''     Next
    ''' 
    ''' End Sub
    ''' </code>
    ''' </example>
    ''' </remarks>
    Public Function getMapArray(Of T)(ByVal recordType As Type, _
                            ByVal table As DataTable) As List(Of T)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMapArray", recordType, table)

        Dim list As New List(Of T)

        'セット先データテーブルのカラム名一覧のハッシュを設定
        Dim hashColumnNames As Hashtable = getColumnNamesHashtable(table)
        If hashColumnNames Is Nothing OrElse hashColumnNames.Count = 0 Then
            Return list
        End If

        ' データテーブルの件数分処理を実施
        For Each row As DataRow In table.Rows
            ' データのマッピングを行い、設定したレコードをList(Of T)にセット
            list.Add(propertyMap(recordType, row, hashColumnNames))
        Next

        Return list

    End Function

    ''' <summary>
    ''' データテーブルに格納されたデータを
    ''' 指定したレコードタイプのList(Of T)に変換します
    ''' 開始位置と終了位置を指定してデータ抽出が可能です
    ''' ページ制御時等で利用
    ''' </summary>
    ''' <param name="recordType">設定先レコードのタイプ</param>
    ''' <param name="table">設定対象のデータテーブル</param>
    ''' <param name="startRow">抽出開始行(1行目は1) </param>
    ''' <param name="endRow">抽出終了行</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMapArray(Of T)(ByVal recordType As Type, _
                                ByVal table As DataTable, _
                                ByVal startRow As Integer, _
                                ByVal endRow As Integer) As List(Of T)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMapArray", _
                                                    recordType, _
                                                    table, _
                                                    startRow, _
                                                    endRow)

        Dim list As New List(Of T)

        Dim i As Integer

        ' 開始行＞終了行の場合、処理終了
        If startRow > endRow Then
            Return list
        End If

        ' 開始行＞データテーブル件数の場合、処理終了
        If startRow > table.Rows.Count Then
            Return list
        End If

        'セット先データテーブルのカラム名一覧のハッシュを設定
        Dim hashColumnNames As Hashtable = getColumnNamesHashtable(table)
        If hashColumnNames Is Nothing OrElse hashColumnNames.Count = 0 Then
            Return list
        End If

        ' 範囲指定分処理を実施
        For i = startRow To endRow

            ' 範囲内でデータが無くなったら設定を終了する
            If i > table.Rows.Count Then
                Exit For
            End If

            ' 行データを取得
            Dim row As DataRow = table.Rows(i - 1)

            ' リストにセット
            list.Add(propertyMap(recordType, row, hashColumnNames))

        Next

        Return list

    End Function

    ''' <summary>
    ''' セット先データテーブルのカラム名一覧のハッシュを設定
    ''' </summary>
    ''' <param name="table">対象データテーブル</param>
    ''' <returns>カラム名セット済みハッシュテーブル</returns>
    ''' <remarks></remarks>
    Function getColumnNamesHashtable(ByVal table As DataTable) As Hashtable
        Dim hashColumnNames As New Hashtable
        If table.Rows.Count > 0 Then
            Dim tmpRow As DataRow = table.Rows(0)
            For Each column As DataColumn In tmpRow.Table.Columns
                hashColumnNames.Add(column.ColumnName, True)
            Next
        End If
        Return hashColumnNames
    End Function

    ''' <summary>
    ''' レコードクラスのプロパティとデータテーブルのマッピングを行う
    ''' </summary>
    ''' <param name="recordType">設定先レコードクラスのタイプ</param>
    ''' <param name="row">設定元のDataRow</param>
    ''' <returns>設定先レコード</returns>
    ''' <remarks></remarks>
    Public Function propertyMap(ByVal recordType As Type, _
                                ByVal row As DataRow, _
                                ByVal hashColumnNames As Hashtable) As Object

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".propertyMap", _
                                            recordType, _
                                            row)

        ' 設定先レコードのプロパティ情報
        Dim propertyInfo As System.Reflection.PropertyInfo

        ' 設定先レコードのインスタンスを作成
        Dim target As Object = recordType.InvokeMember(Nothing, _
                                BindingFlags.CreateInstance, _
                                Nothing, _
                                Nothing, _
                                New Object() {})

        Dim exitLoop As Boolean = False

        ' 設定先のプロパティ情報分ループし、同一項目を探す
        For Each propertyInfo In recordType.GetProperties

            Dim list() As Object = propertyInfo.GetCustomAttributes(GetType(TableMapAttribute), False)
            Dim item As TableMapAttribute

            ' カスタムアトリビュート分処理を行う（この処理の場合は１件しかない為１回のみ）
            For Each item In list

                ' 設定元データのカラム名とアトリビュートのカラム名が一致した場合、
                ' 設定先レコードにデータをセットする
                ' 注）データテーブルの項目属性とレコードの属性は同一にして下さい
                ' 　　　データテーブルInt32 レコードInteger は問題なし
                ' 　　　データテーブルInt32 レコードBoolean はエラーになります

                ' 設定元データの取得
                Dim rowData As Object = Nothing
                Try
                    If hashColumnNames.ContainsKey(item.ItemName()) Then
                        'セット対象のカラムが存在する場合
                        ' DBNullの場合、設定しない（DBNull⇒各タイプへのキャスト例外が発生する為）
                        If Not row(item.ItemName()).GetType Is GetType(DBNull) Then
                            rowData = row(item.ItemName())
                        End If
                    Else
                        'セット対象カラムが存在しない場合、ループを抜ける
                        exitLoop = True
                        Exit For
                    End If

                Catch ex As Exception
                    ' 取得に失敗した場合、設定しない
                    exitLoop = True
                    Exit For
                End Try

                ' 取得値がNULLの場合は設定しない
                If rowData Is Nothing Then
                    exitLoop = True
                    Exit For
                End If

                ' 設定先プロパティへ設定
                recordType.InvokeMember(propertyInfo.Name, _
                                 BindingFlags.SetProperty, _
                                 Nothing, _
                                 target, _
                                 New Object() {rowData})

                exitLoop = True
                Exit For
            Next
        Next

        Return target

    End Function
End Class
