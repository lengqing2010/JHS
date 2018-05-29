''' <summary>
''' 共通処理クラス(静的インスタンス)
''' </summary>
''' <remarks>このクラスにはアプリケーションで共有する処理以外実装しない事</remarks>
Public Class CommonLogic

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New CommonLogic()

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As CommonLogic
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New CommonLogic()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' 画面表示用にオブジェクトを文字列変換する
    ''' </summary>
    ''' <param name="obj">表示対象のデータ</param>
    ''' <param name="str">未設定時の初期値</param>
    ''' <returns>表示形式の文字列</returns>
    ''' <remarks>
    ''' Decimal  : Minvalueを空白、それ以外は入力値をString型で返却<br/>
    ''' Integer  : Minvalueを空白、それ以外は入力値をString型で返却<br/>
    ''' DateTime : MinDateTimeを空白、それ以外は入力値を YYYY/MM/DD 形式のString型で返却<br/>
    ''' 上記以外 : そのまま。適宜追加してください
    ''' </remarks>
    Public Function getDisplayString(ByVal obj As Object, Optional ByVal str As String = "") As String

        ' 戻り値となるStringデータ
        Dim ret As String = str

        If obj Is Nothing Then
            ' NULL は基本的に空白を返す
            Return ret
        ElseIf obj.GetType().ToString() = GetType(String).ToString Then
            ' Stringの場合
            If obj = "" Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(Integer).ToString Then
            ' Integerの場合
            If obj = Integer.MinValue Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimalの場合
            If obj = Decimal.MinValue Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTimeの場合
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
    ''' 画面表示用文字列を指定した型に変換する
    ''' </summary>
    ''' <param name="strData">変換対象のデータ</param>
    ''' <param name="objChangeData">変換後の型データ（参照）</param>
    ''' <returns>変換後の型データ</returns>
    ''' <remarks>
    ''' Decimal  : 空白をMinvalue、それ以外は入力値をDecimalに変換<br/>
    ''' Integer  : 空白をMinvalue、それ以外は入力値をIntegerに変換<br/>
    ''' DateTime : 空白をMinvalue、それ以外は入力値をDateTimeに変換<br/>
    ''' 上記以外 : そのまま。適宜追加してください<br/>
    ''' 変換に失敗した場合はFalseを返し、指定型のMinValueをセットします
    ''' </remarks>
    Public Function setDisplayString(ByVal strData As String, _
                                     ByRef objChangeData As Object) As Boolean


        If objChangeData.GetType().ToString() = GetType(Integer).ToString Then
            ' Integerへ変換
            If strData.Trim() = "" Then
                objChangeData = Integer.MinValue
                Return True
            Else
                Try
                    objChangeData = Integer.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' 変換に失敗した場合、MinValueを設定し、Falseを返却
                    objChangeData = Integer.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimalへ変換
            If strData.Trim() = "" Then
                objChangeData = Decimal.MinValue
                Return True
            Else
                Try
                    objChangeData = Decimal.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' 変換に失敗した場合、MinValueを設定し、Falseを返却
                    objChangeData = Decimal.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTimeへ変換
            If strData.Trim() = "" Then
                objChangeData = DateTime.MinValue
                Return True
            Else
                Try
                    objChangeData = DateTime.Parse(strData)
                    Return True
                Catch ex As Exception
                    ' 変換に失敗した場合、MinValueを設定し、Falseを返却
                    objChangeData = DateTime.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(String).ToString Then
            ' StringはTrimする
            objChangeData = strData.Trim()
            Return True
        End If

        ' 変換対象以外の型はエラー
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
