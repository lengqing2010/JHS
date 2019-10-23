''' <summary>
''' 物件履歴データの排他チェック用レコードクラスです
''' </summary>
''' <remarks>
''' Kye項目と更新ユーザー、更新日時を保持し、他端末の更新チェックを行います。<br />
''' ※JibanhaitaRecordを継承している為、区分、保証書NO、更新日時、更新ユーザーIDは記述不要
''' </remarks>
<TableClassMap("t_bukken_rireki")> _
Public Class BukkenRirekiHaitaRecord
    Inherits JibanHaitaRecord

#Region "入力NO"
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuuryokuNo As Integer
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力NO</returns>
    ''' <remarks></remarks>
    <TableMap("nyuuryoku_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property NyuuryokuNo() As Integer
        Get
            Return intNyuuryokuNo
        End Get
        Set(ByVal value As Integer)
            intNyuuryokuNo = value
        End Set
    End Property
#End Region
End Class
