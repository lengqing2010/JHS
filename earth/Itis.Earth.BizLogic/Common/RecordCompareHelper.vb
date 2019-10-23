Imports System.Reflection
''' <summary>
''' ２つのレコードクラスより項目の比較を行います(静的メンバ)
''' </summary>
''' <remarks></remarks>
Public Class RecordCompareHelper

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New RecordCompareHelper()

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As RecordCompareHelper
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New RecordCompareHelper()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' ２つのレコードクラスの全項目を比較し、同一の場合True、相違の場合Falseを返します
    ''' </summary>
    ''' <param name="targetRecord1">比較対象１レコード</param>
    ''' <param name="targetRecord2">比較対象２レコード</param>
    ''' <returns>同一の場合:True 相違の場合:False</returns>
    ''' <remarks></remarks>
    Public Function CheckCompareAll(ByVal targetRecord1 As Object, _
                                    ByVal targetRecord2 As Object) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckCompareAll", _
                                            targetRecord1, _
                                            targetRecord2)

        ' 比較対象１レコードのプロパティ情報
        Dim target1Info As System.Reflection.PropertyInfo
        ' 比較対象２レコードのプロパティ情報
        Dim target2Info As System.Reflection.PropertyInfo

        ' 参照用のプロパティ情報分ループ
        For Each target1Info In targetRecord1.GetType().GetProperties

            ' 設定用のプロパティ情報分ループし、同一項目を探す
            For Each target2Info In targetRecord2.GetType().GetProperties

                ' プロパティの名称と属性が一致した場合、内容の比較を行う
                If target1Info.Name = target2Info.Name And _
                target1Info.GetType().ToString() = target2Info.GetType().ToString() Then

                    ' 比較対象１データの取得
                    Dim targetItemData1 As New Object
                    Try
                        targetItemData1 = target1Info.GetValue(targetRecord1, Nothing)


                    Catch ex As Exception
                        ' 取得に失敗した場合、比較しない
                        Exit For
                    End Try

                    ' 比較対象２データの取得
                    Dim targetItemData2 As New Object
                    Try
                        targetItemData2 = target2Info.GetValue(targetRecord2, Nothing)
                    Catch ex As Exception
                        ' 取得に失敗した場合、比較しない
                        Exit For
                    End Try

                    If (Not targetItemData2 Is Nothing And targetItemData1 Is Nothing) Or _
                       (targetItemData2 Is Nothing And Not targetItemData1 Is Nothing) Then
                        ' どちらかがNothingはFalse
                        Return False
                    ElseIf targetItemData2 Is Nothing And targetItemData1 Is Nothing Then
                        ' 何れもNothingはOK
                        Exit For
                    End If

                    ' 不一致の場合Falseを返す
                    If targetItemData1.Equals(targetItemData2) = False Then
                        Return False
                    End If

                End If
            Next
        Next

        Return True

    End Function
End Class
