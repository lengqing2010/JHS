Public Class TenbetuHansokuHinRecord

#Region "店コード"
    ''' <summary>
    ''' 店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strMiseCd As String
    ''' <summary>
    ''' 店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 店コード</returns>
    ''' <remarks></remarks>
    <TableMap("mise_cd", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Property MiseCd() As String
        Get
            Return strMiseCd
        End Get
        Set(ByVal value As String)
            strMiseCd = value
        End Set
    End Property
#End Region

#Region "分類コード"
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類コード</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=3)> _
    Public Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "合計単価"
    ''' <summary>
    ''' 合計単価
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSumTanka As Long
    ''' <summary>
    ''' 合計単価
    ''' </summary>
    ''' <value></value>
    ''' <returns> 合計単価</returns>
    ''' <remarks></remarks>
    <TableMap("sum_tanka")> _
    Public Property SumTanka() As Long
        Get
            Return lngSumTanka
        End Get
        Set(ByVal value As Long)
            lngSumTanka = value
        End Set
    End Property
#End Region

#Region "合計消費税"
    ''' <summary>
    ''' 合計消費税
    ''' </summary>
    ''' <remarks></remarks>
    Private decSumSyouhiZei As Decimal
    ''' <summary>
    ''' 合計消費税
    ''' </summary>
    ''' <value></value>
    ''' <returns> 合計消費税</returns>
    ''' <remarks></remarks>
    <TableMap("sum_syouhi_zei")> _
    Public Property SumSyouhiZei() As Decimal
        Get
            Return decSumSyouhiZei
        End Get
        Set(ByVal value As Decimal)
            decSumSyouhiZei = value
        End Set
    End Property
#End Region

#Region "合計税込金額"
    ''' <summary>
    ''' 合計税込金額
    ''' </summary>
    ''' <remarks></remarks>
    Private decSumZeikomiGaku As Decimal
    ''' <summary>
    ''' 合計税込金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 合計税込金額</returns>
    ''' <remarks></remarks>
    <TableMap("sum_zeikomi_gaku")> _
    Public Property SumZeikomiGaku() As Decimal
        Get
            Return decSumZeikomiGaku
        End Get
        Set(ByVal value As Decimal)
            decSumZeikomiGaku = value
        End Set
    End Property
#End Region

#Region "合計工務店請求単価"
    ''' <summary>
    ''' 合計工務店請求単価
    ''' </summary>
    ''' <remarks></remarks>
    Private decSumKoumuGaku As Decimal
    ''' <summary>
    ''' 合計工務店請求単価
    ''' </summary>
    ''' <value></value>
    ''' <returns>合計工務店請求単価</returns>
    ''' <remarks></remarks>
    ''' 
    <TableMap("sum_koumuten_gaku")> _
    Public Property SumKoumuGaku() As Decimal
        Get
            Return decSumKoumuGaku
        End Get
        Set(ByVal value As Decimal)
            decSumKoumuGaku = value
        End Set
    End Property
#End Region

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "商品名"
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinMei As String
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品名</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property SyouhinMei() As String
        Get
            Return strSyouhinMei
        End Get
        Set(ByVal value As String)
            strSyouhinMei = value
        End Set
    End Property
#End Region

#Region "税率"
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal = Decimal.MinValue
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税率</returns>
    ''' <remarks></remarks>
    <TableMap("zeiritu")> _
    Public Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region

#Region "工務店請求単価"
    ''' <summary>
    ''' 工務店請求単価
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenSeikyuuTanka As Integer
    ''' <summary>
    ''' 工務店請求単価
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求単価</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_seikyuu_tanka", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KoumutenSeikyuuTanka() As Integer
        Get
            Return intKoumutenSeikyuuTanka
        End Get
        Set(ByVal value As Integer)
            intKoumutenSeikyuuTanka = value
        End Set
    End Property
#End Region

#Region "単価"
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <value></value>
    ''' <returns> 単価</returns>
    ''' <remarks></remarks>
    <TableMap("tanka", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
        End Set
    End Property
#End Region

#Region "数量"
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <remarks></remarks>
    Private intSuu As Integer
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <value></value>
    ''' <returns> 数量</returns>
    ''' <remarks></remarks>
    <TableMap("suu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Suu() As Integer
        Get
            Return intSuu
        End Get
        Set(ByVal value As Integer)
            intSuu = value
        End Set
    End Property
#End Region

#Region "売上金額"
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <remarks></remarks>
    Private lngUriGaku As Long
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property UriGaku() As Long
        Get
            '単価 * 数量
            Return Tanka * Suu
        End Get
    End Property
#End Region

#Region "消費税"
    ''' <summary>
    ''' 消費税
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhiZei As Integer = Integer.MinValue
    ''' <summary>
    ''' 消費税
    ''' </summary>
    ''' <value></value>
    ''' <returns> 消費税</returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_gaku")> _
    Public Property SyouhiZei() As Integer
        Get
            Dim intTmpUriGaku As Integer    '売上金額
            Dim decTmpZeiritu As Decimal    '税率
            '売上金額の取得
            If UriGaku = Integer.MinValue Then
                intTmpUriGaku = 0
            Else
                intTmpUriGaku = UriGaku
            End If
            '税率の取得
            If Zeiritu = Decimal.MinValue Then
                decTmpZeiritu = 0
            Else
                decTmpZeiritu = Zeiritu
            End If

            '消費税額
            If intSyouhiZei = Integer.MinValue Then 'NULLの場合、売上金額と税率から換算
                intSyouhiZei = Fix(intTmpUriGaku * decTmpZeiritu) '消費税額 = 売上金額 * 税率
            End If
            Return intSyouhiZei
        End Get
        Set(ByVal value As Integer)
            intSyouhiZei = value
        End Set
    End Property
#End Region

#Region "税区分"
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeiKbn As String
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税区分</returns>
    ''' <remarks></remarks>
    <TableMap("zei_kbn")> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "税込金額"
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intZeikomiGaku As Integer
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税込金額</returns>
    ''' <remarks></remarks>
    <TableMap("zeikomi_gaku")> _
    Public ReadOnly Property ZeikomiGaku() As Integer
        Get
            Dim intTmpUriGaku As Integer    '売上金額
            Dim decTmpZeiritu As Decimal    '税率
            '売上金額の取得
            If UriGaku = Integer.MinValue Then
                intTmpUriGaku = 0
            Else
                intTmpUriGaku = UriGaku
            End If
            '税率の取得
            If Zeiritu = Decimal.MinValue Then
                decTmpZeiritu = 0
            Else
                decTmpZeiritu = Zeiritu
            End If

            If SyouhiZei = Integer.MinValue Then '消費税額=NULLの場合、税率から算出
                intTmpUriGaku = intTmpUriGaku + Fix(intTmpUriGaku * decTmpZeiritu) '税込売上金額 = 売上金額 + (売上金額 * 消費税率)
            Else
                intTmpUriGaku = intTmpUriGaku + SyouhiZei '税込売上金額 = 売上金額 + 消費税額
            End If
            Return intTmpUriGaku
        End Get
    End Property
#End Region

#Region "請求書発行日"
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoHakDate As DateTime
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hak_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SeikyuusyoHakDate() As DateTime
        Get
            Return dateSeikyuusyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakDate = value
        End Set
    End Property
#End Region

#Region "売上年月日"
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDate As DateTime
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上年月日</returns>
    ''' <remarks></remarks>
    <TableMap("uri_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UriDate() As DateTime
        Get
            Return dateUriDate
        End Get
        Set(ByVal value As DateTime)
            dateUriDate = value
        End Set
    End Property
#End Region

#Region "伝票売上年月日"
    ''' <summary>
    ''' 伝票売上年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenUriDate As DateTime
    ''' <summary>
    ''' 伝票売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票売上年月日</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenpyouUriDate() As DateTime
        Get
            Return dateDenUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenUriDate = value
        End Set
    End Property
#End Region

#Region "入力日"
    ''' <summary>
    ''' 入力日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuuryokuDate As DateTime
    ''' <summary>
    ''' 入力日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力日</returns>
    ''' <remarks></remarks>
    <TableMap("nyuuryoku_date", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property NyuuryokuDate() As DateTime
        Get
            Return dateNyuuryokuDate
        End Get
        Set(ByVal value As DateTime)
            dateNyuuryokuDate = value
        End Set
    End Property
#End Region

#Region "入力日NO"
    ''' <summary>
    ''' 入力日NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuuryokuDateNo As Integer
    ''' <summary>
    ''' 入力日NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力日NO</returns>
    ''' <remarks></remarks>
    <TableMap("nyuuryoku_date_no", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property NyuuryokuDateNo() As Integer
        Get
            Return intNyuuryokuDateNo
        End Get
        Set(ByVal value As Integer)
            intNyuuryokuDateNo = value
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
    <TableMap("uri_keijyou_flg")> _
    Public Property UriKeijyouFlg() As Integer
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
    <TableMap("uri_keijyou_date")> _
    Public Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザーID"
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "調査請求先"
    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysSeikyuuSaki As String
    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査請求先</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki")> _
    Public Property TysSeikyuuSaki() As String
        Get
            Return strTysSeikyuuSaki
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSaki = value
        End Set
    End Property
#End Region

#Region "販促品請求先"
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuusaki As String
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuusaki")> _
    Public Property HansokuhinSeikyuusaki() As String
        Get
            Return strHansokuhinSeikyuusaki
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuusaki = value
        End Set
    End Property
#End Region

#Region "系列コード"
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列コード</returns>
    ''' <remarks></remarks>
    <TableMap("keiretu_cd")> _
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region

#Region "SQL種別判断フラグ"
    ''' <summary>
    ''' SQL種別判断フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSqlTypeFlg As Integer
    ''' <summary>
    ''' SQL種別判断フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>SQL種別判断フラグ</returns>
    ''' <remarks></remarks>
    Public Property SqlTypeFlg() As Integer
        Get
            Return intSqlTypeFlg
        End Get
        Set(ByVal value As Integer)
            intSqlTypeFlg = value
        End Set
    End Property

#End Region

#Region "FC判断フラグ"
    ''' <summary>
    ''' FC判断フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intIsFc As Integer
    ''' <summary>
    ''' FC判断フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>FC判断フラグ</returns>
    ''' <remarks></remarks>
    Public Property IsFc() As Integer
        Get
            Return intIsFc
        End Get
        Set(ByVal value As Integer)
            intIsFc = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Private intMode As Integer
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>コントロールの表示モード</returns>
    ''' <remarks></remarks>
    Public Property enMode() As Integer
        Get
            Return intMode
        End Get
        Set(ByVal value As Integer)
            intMode = value
        End Set
    End Property

#Region "発送日"
    ''' <summary>
    ''' 発送日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHassouDate As DateTime
    ''' <summary>
    ''' 発送日
    ''' </summary>
    ''' <value></value>
    ''' <returns>発送日</returns>
    ''' <remarks></remarks>
    ''' 
    <TableMap("hassou_date")> _
    Public Property HassouDate() As DateTime
        Get
            Return dateHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateHassouDate = value
        End Set
    End Property
#End Region

End Class