Imports System.Reflection
''' <summary>
''' DBの検索結果とレコードクラスのマッピングを行うヘルパークラスです(静的メンバ)
''' </summary>
''' <remarks></remarks>
Public Class DataMappingHelper

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New DataMappingHelper()

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
    ''' 指定したレコードタイプのArrayListに変換します
    ''' </summary>
    ''' <param name="record_type">設定先レコードのタイプ</param>
    ''' <param name="table">設定対象のデータテーブル</param>
    ''' <returns>設定先レコードのArrayList</returns>
    ''' <remarks>
    ''' <example>
    ''' 以下の例はDataAccessクラスより取得したDataTableの内容を<br/>
    ''' 特定レコードクラスのArrayListとして取得します
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
    '''     ' 設定したいレコードのタイプと設定対象のDataTableを引数にデータ設定済のArrayListを取得
    '''     Dim list As ArrayList = helper.getMapArray(record.getType(), data_table)
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
    Public Function getMapArray(ByVal record_type As Type, _
                                ByVal table As DataTable) As ArrayList

        Dim list As New ArrayList

        ' データテーブルの件数分処理を実施
        For Each row As DataRow In table.Rows
            ' データのマッピングを行い、設定したレコードをArrayListにセット
            list.Add(propertyMap(record_type, row))
        Next

        Return list

    End Function

    ''' <summary>
    ''' データテーブルに格納されたデータを
    ''' 指定したレコードタイプのArrayListに変換します
    ''' 開始位置と終了位置を指定してデータ抽出が可能です
    ''' ページ制御時等で利用
    ''' </summary>
    ''' <param name="record_type">設定先レコードのタイプ</param>
    ''' <param name="table">設定対象のデータテーブル</param>
    ''' <param name="start_row">抽出開始行(1行目は1) </param>
    ''' <param name="end_row">抽出終了行</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMapArray(ByVal record_type As Type, _
                                ByVal table As DataTable, _
                                ByVal start_row As Integer, _
                                ByVal end_row As Integer) As ArrayList

        Dim list As New ArrayList

        Dim i As Integer

        ' 開始行＞終了行の場合、処理終了
        If start_row > end_row Then
            Return list
        End If

        ' 開始行＞データテーブル件数の場合、処理終了
        If start_row > table.Rows.Count Then
            Return list
        End If

        ' 範囲指定分処理を実施
        For i = start_row To end_row

            ' 範囲内でデータが無くなったら設定を終了する
            If i > table.Rows.Count Then
                Exit For
            End If

            ' 行データを取得
            Dim row As DataRow = table.Rows(i - 1)

            ' リストにセット
            list.Add(propertyMap(record_type, row))

        Next

        Return list

    End Function

    ''' <summary>
    ''' レコードクラスのプロパティとデータテーブルのマッピングを行う
    ''' </summary>
    ''' <param name="record_type">設定先レコードクラスのタイプ</param>
    ''' <param name="row">設定元のDataRow</param>
    ''' <returns>設定先レコード</returns>
    ''' <remarks></remarks>
    Public Function propertyMap(ByVal record_type As Type, _
                                ByVal row As DataRow) As Object

        ' 設定元データのプロパティ情報
        Dim row_info As System.Reflection.PropertyInfo

        ' 設定先レコードのプロパティ情報
        Dim property_info As System.Reflection.PropertyInfo

        ' 設定先レコードのインスタンスを作成
        Dim target As Object = record_type.InvokeMember(Nothing, _
                                BindingFlags.CreateInstance, _
                                Nothing, _
                                Nothing, _
                                New Object() {})

        ' 設定元データに含まれるプロパティ分設定処理を行う
        For Each row_info In row.GetType().GetProperties

            Dim exit_loop As Boolean = False

            ' 設定先のプロパティ情報分ループし、同一項目を探す
            For Each property_info In record_type.GetProperties

                Dim list() As Object = property_info.GetCustomAttributes(GetType(TableMapAttribute), False)
                Dim item As TableMapAttribute

                ' カスタムアトリビュート分処理を行う（この処理の場合は１件しかない為１回のみ）
                For Each item In list

                    ' 設定元データのカラム名とアトリビュートのカラム名が一致した場合、
                    ' 設定先レコードにデータをセットする
                    ' 注）データテーブルの項目属性とレコードの属性は同一にして下さい
                    ' 　　　データテーブルInt32 レコードInteger は問題なし
                    ' 　　　データテーブルInt32 レコードBoolean はエラーになります
                    If row_info.Name = item.ItemName() Then

                        ' 設定元データの取得
                        Dim row_data As Object
                        Try
                            row_data = row.GetType().InvokeMember(item.ItemName(), _
                                        BindingFlags.GetProperty, _
                                        Nothing, _
                                        row, _
                                        Nothing)
                        Catch ex As Exception
                            ' 取得に失敗した場合、設定しない
                            exit_loop = True
                            Exit For
                        End Try

                        ' 取得値がNULLの場合は設定しない
                        If row_data Is Nothing Then
                            exit_loop = True
                            Exit For
                        End If

                        ' 設定先プロパティへ設定
                        record_type.InvokeMember(property_info.Name, _
                                         BindingFlags.SetProperty, _
                                         Nothing, _
                                         target, _
                                         New Object() {row_data})

                        exit_loop = True
                        Exit For
                    End If
                Next

                ' 設定済項目の場合、次の項目設定を行う
                If exit_loop = True Then
                    Exit For
                End If
            Next
        Next

        Return target

    End Function
End Class
