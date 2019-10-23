''' <summary>
''' 邸別請求テーブル/更新用レコードクラス[調査報告書再発行]
''' ※下記プロパティ項目で更新対象を設定変更する
''' </summary>
''' <remarks>
''' </remarks>
<TableClassMap("t_teibetu_seikyuu")> _
Public Class TeibetuSeikyuuRecordTysHoukokusyo
    Inherits TeibetuSeikyuuRecord

#Region "分類ｺｰﾄﾞ"
    ''' <summary>
    ''' 分類ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' 分類ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "確定区分"
    ''' <summary>
    ''' 確定区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakuteiKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' 確定区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 確定区分</returns>
    ''' <remarks></remarks>
    <TableMap("kakutei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KakuteiKbn() As Integer
        Get
            Return intKakuteiKbn
        End Get
        Set(ByVal value As Integer)
            intKakuteiKbn = value
        End Set
    End Property
#End Region

#Region "売上計上FLG"
    ''' <summary>
    ''' 売上計上FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKeijyouFlg As Integer
    ''' <summary>
    ''' 売上計上FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上計上FLG</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_flg", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UriKeijyouFlg() As Integer
        Get
            Return intUriKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intUriKeijyouFlg = value
        End Set
    End Property
#End Region

#Region "売上計上日"
    ''' <summary>
    ''' 売上計上日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriKeijyouDate As DateTime
    ''' <summary>
    ''' 売上計上日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上計上日</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
        End Set
    End Property
#End Region

#Region "備考"
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns> 備考</returns>
    ''' <remarks></remarks>
    <TableMap("bikou", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "発注書金額"
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発注書金額</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HattyuusyoGaku() As Integer
        Get
            If intHattyuusyoGaku = 0 Then
                Return Integer.MinValue
            End If

            Return intHattyuusyoGaku
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoGaku = value
        End Set
    End Property
#End Region

#Region "発注書確認日"
    ''' <summary>
    ''' 発注書確認日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHattyuusyoKakuninDate As DateTime
    ''' <summary>
    ''' 発注書確認日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発注書確認日</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_kakunin_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HattyuusyoKakuninDate() As DateTime
        Get
            Return dateHattyuusyoKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateHattyuusyoKakuninDate = value
        End Set
    End Property
#End Region

#Region "調査見積書作成日"
    ''' <summary>
    ''' 調査見積書作成日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysMitsyoSakuseiDate As DateTime
    ''' <summary>
    ''' 調査見積書作成日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査見積書作成日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_mitsyo_sakusei_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysMitsyoSakuseiDate() As DateTime
        Get
            Return dateTysMitsyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysMitsyoSakuseiDate = value
        End Set
    End Property
#End Region

End Class