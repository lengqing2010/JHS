Partial Class UriageSiireCsvDataSet
    Partial Class SiireCsvTableDataTable

        Private Sub SiireCsvTableDataTable_SiireCsvTableRowChanging(ByVal sender As System.Object, ByVal e As SiireCsvTableRowChangeEvent) Handles Me.SiireCsvTableRowChanging

        End Sub

        Private Sub SiireCsvTableDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.suuryouColumn.ColumnName) Then
                'ユーザー コードをここに追加してください
            End If

        End Sub

    End Class

End Class
