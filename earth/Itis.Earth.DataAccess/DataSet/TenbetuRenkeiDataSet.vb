

Partial Public Class TenbetuRenkeiDataSet
    Partial Class TenbetuKeyDataTable

        Private Sub TenbetuKeyDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.nyuuryoku_dateColumn.ColumnName) Then
                'ユーザー コードをここに追加してください
            End If

        End Sub

    End Class

End Class
