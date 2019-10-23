Imports System.Reflection
''' <summary>
''' ２つのレコードクラスより同一名且つ同一属性のプロパティデータを複製します(静的メンバ)
''' </summary>
''' <remarks>参照レコードのプロパティ名、属性と同一の内容を持つ<br/>
''' 設定レコードのプロパティに参照用データを設定する</remarks>
Public Class RecordMappingHelper

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New RecordMappingHelper()

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As RecordMappingHelper
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New RecordMappingHelper()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' ２つのレコードクラスより同一名且つ同一属性のプロパティデータを複製します
    ''' </summary>
    ''' <param name="fromRecord">参照用レコード</param>
    ''' <param name="toRecord">設定用レコードレコード</param>
    ''' <returns>正常に処理が行われた場合:True</returns>
    ''' <remarks></remarks>
    Public Function CopyRecordData(ByVal fromRecord As Object, _
                                   ByRef toRecord As Object) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CopyRecordData", _
                                            fromRecord, _
                                            toRecord)

        ' 参照用レコードのプロパティ情報
        Dim fromPropertyInfo As System.Reflection.PropertyInfo
        ' 設定用レコードのプロパティ情報
        Dim toPropertyInfo As System.Reflection.PropertyInfo

        ' 参照用のプロパティ情報分ループ
        For Each fromPropertyInfo In fromRecord.GetType().GetProperties

            ' 設定用のプロパティ情報分ループし、同一項目を探す
            For Each toPropertyInfo In toRecord.GetType().GetProperties

                ' プロパティの名称と属性が一致した場合、設定先へデータをセットする
                If fromPropertyInfo.Name = toPropertyInfo.Name And _
                fromPropertyInfo.GetType().ToString() = toPropertyInfo.GetType().ToString() Then

                    ' 参照用データの取得
                    Dim fromItemData As Object
                    Try
                        fromItemData = fromRecord.GetType().InvokeMember(fromPropertyInfo.Name, _
                                    BindingFlags.GetProperty, _
                                    Nothing, _
                                    fromRecord, _
                                    Nothing)
                    Catch ex As Exception
                        ' 取得に失敗した場合、設定しない
                        Exit For
                    End Try


                    ' 設定用レコードにセット
                    ' 設定先プロパティへ設定
                    Try
                        toPropertyInfo.SetValue(toRecord, fromItemData, Nothing)
                    Catch ex As Exception
                        ' 取得に失敗した場合、設定しない
                        Exit For
                    End Try

                End If
            Next
        Next

        Return True

    End Function
End Class
