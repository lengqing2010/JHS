

Partial Public Class YosinDataSet
    Partial Class YosinKanriTableDataTable

        Private Sub YosinKanriTableDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.nayose_saki_cdColumn.ColumnName) Then
                'ユーザー コードをここに追加してください
            End If

        End Sub

    End Class

End Class
