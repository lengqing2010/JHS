Public Class GenkaRecord

#Region "’²¸‰ïĞƒR[ƒh"
    ''' <summary>
    ''' ’²¸‰ïĞƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ’²¸‰ïĞƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸‰ïĞƒR[ƒh</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd")> _
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "–‹ÆŠƒR[ƒh"
    ''' <summary>
    ''' –‹ÆŠƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private strJigyousyoCd As String
    ''' <summary>
    ''' –‹ÆŠƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> –‹ÆŠƒR[ƒh</returns>
    ''' <remarks></remarks>
    <TableMap("jigyousyo_cd")> _
    Public Property JigyousyoCd() As String
        Get
            Return strJigyousyoCd
        End Get
        Set(ByVal value As String)
            strJigyousyoCd = value
        End Set
    End Property
#End Region

#Region ""
    ''' <summary>
    ''' ‘Šèæí•Ê
    ''' </summary>
    ''' <remarks></remarks>
    Private intAitesakiSyubetu As Integer = Integer.MinValue
    ''' <summary>
    ''' ‘Šèæí•Ê
    ''' </summary>
    ''' <value></value>
    ''' <returns>‘Šèæí•Ê </returns>
    ''' <remarks></remarks>
    <TableMap("aitesaki_syubetu")> _
    Public Property AitesakiSyubetu() As Integer
        Get
            Return intAitesakiSyubetu
        End Get
        Set(ByVal value As Integer)
            intAitesakiSyubetu = value
        End Set
    End Property
#End Region

#Region ""
    ''' <summary>
    ''' ‘ŠèæƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private strAitesakiCd As String
    ''' <summary>
    ''' ‘ŠèæƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns>‘ŠèæƒR[ƒh </returns>
    ''' <remarks></remarks>
    <TableMap("aitesaki_cd")> _
    Public Property AitesakiCd() As String
        Get
            Return strAitesakiCd
        End Get
        Set(ByVal value As String)
            strAitesakiCd = value
        End Set
    End Property
#End Region

#Region "¤•iƒR[ƒh"
    ''' <summary>
    ''' ¤•iƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ¤•iƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤•iƒR[ƒh</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd")> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "’²¸•û–@NO"
    ''' <summary>
    ''' ’²¸•û–@NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHouhouNo As Integer = Integer.MinValue
    ''' <summary>
    ''' ’²¸•û–@NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸•û–@NO</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_no")> _
    Public Property TysHouhouNo() As Integer
        Get
            Return intTysHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTysHouhouNo = value
        End Set
    End Property
#End Region

#Region "æÁ"
    ''' <summary>
    ''' æÁ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' æÁ
    ''' </summary>
    ''' <value></value>
    ''' <returns> æÁ</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi")> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši1"
    ''' <summary>
    ''' “‰¿Ši1
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk1 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši1
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši1 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk1")> _
    Public Property TouKkk1() As Integer
        Get
            Return intTouKkk1
        End Get
        Set(ByVal value As Integer)
            intTouKkk1 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG1"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG1
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg1 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG1
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG1 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg1")> _
    Public Property TouKkkHenkouFlg1() As Integer
        Get
            Return intTouKkkHenkouFlg1
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg1 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši2"
    ''' <summary>
    ''' “‰¿Ši2
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk2 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši2
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši2 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk2")> _
    Public Property TouKkk2() As Integer
        Get
            Return intTouKkk2
        End Get
        Set(ByVal value As Integer)
            intTouKkk2 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG2"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG2
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg2 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG2
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG2 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg2")> _
    Public Property TouKkkHenkouFlg2() As Integer
        Get
            Return intTouKkkHenkouFlg2
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg2 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši3"
    ''' <summary>
    ''' “‰¿Ši3
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk3 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši3
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši3 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk3")> _
    Public Property TouKkk3() As Integer
        Get
            Return intTouKkk3
        End Get
        Set(ByVal value As Integer)
            intTouKkk3 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG3"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG3
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg3 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG3
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG3 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg3")> _
    Public Property TouKkkHenkouFlg3() As Integer
        Get
            Return intTouKkkHenkouFlg3
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg3 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši4"
    ''' <summary>
    ''' “‰¿Ši4
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk4 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši4
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši4 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk4")> _
    Public Property TouKkk4() As Integer
        Get
            Return intTouKkk4
        End Get
        Set(ByVal value As Integer)
            intTouKkk4 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG4"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG4
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg4 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG4
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG4 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg4")> _
    Public Property TouKkkHenkouFlg4() As Integer
        Get
            Return intTouKkkHenkouFlg4
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg4 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši5"
    ''' <summary>
    ''' “‰¿Ši5
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk5 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši5
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši5 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk5")> _
    Public Property TouKkk5() As Integer
        Get
            Return intTouKkk5
        End Get
        Set(ByVal value As Integer)
            intTouKkk5 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG5"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG5
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg5 As Integer= Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG5
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG5 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg5")> _
    Public Property TouKkkHenkouFlg5() As Integer
        Get
            Return intTouKkkHenkouFlg5
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg5 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši6"
    ''' <summary>
    ''' “‰¿Ši6
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk6 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši6
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši6 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk6")> _
    Public Property TouKkk6() As Integer
        Get
            Return intTouKkk6
        End Get
        Set(ByVal value As Integer)
            intTouKkk6 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG6"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG6
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg6 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG6
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG6 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg6")> _
    Public Property TouKkkHenkouFlg6() As Integer
        Get
            Return intTouKkkHenkouFlg6
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg6 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši7"
    ''' <summary>
    ''' “‰¿Ši7
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk7 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši7
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši7 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk7")> _
    Public Property TouKkk7() As Integer
        Get
            Return intTouKkk7
        End Get
        Set(ByVal value As Integer)
            intTouKkk7 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG7"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG7
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg7 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG7
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG7 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg7")> _
    Public Property TouKkkHenkouFlg7() As Integer
        Get
            Return intTouKkkHenkouFlg7
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg7 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši8"
    ''' <summary>
    ''' “‰¿Ši8
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk8 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši8
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši8 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk8")> _
    Public Property TouKkk8() As Integer
        Get
            Return intTouKkk8
        End Get
        Set(ByVal value As Integer)
            intTouKkk8 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG8"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG8
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg8 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG8
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG8 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg8")> _
    Public Property TouKkkHenkouFlg8() As Integer
        Get
            Return intTouKkkHenkouFlg8
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg8 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši9"
    ''' <summary>
    ''' “‰¿Ši9
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk9 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši9
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši9 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk9")> _
    Public Property TouKkk9() As Integer
        Get
            Return intTouKkk9
        End Get
        Set(ByVal value As Integer)
            intTouKkk9 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG9"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG9
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg9 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG9
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG9 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg9")> _
    Public Property TouKkkHenkouFlg9() As Integer
        Get
            Return intTouKkkHenkouFlg9
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg9 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši10"
    ''' <summary>
    ''' “‰¿Ši10
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk10 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši10
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši10 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk10")> _
    Public Property TouKkk10() As Integer
        Get
            Return intTouKkk10
        End Get
        Set(ByVal value As Integer)
            intTouKkk10 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG10"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG10
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg10 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG10
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG10 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg10")> _
    Public Property TouKkkHenkouFlg10() As Integer
        Get
            Return intTouKkkHenkouFlg10
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg10 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši11`19"
    ''' <summary>
    ''' “‰¿Ši11`19
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk11t19 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši11`19
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši11`19 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk11t19")> _
    Public Property TouKkk11t19() As Integer
        Get
            Return intTouKkk11t19
        End Get
        Set(ByVal value As Integer)
            intTouKkk11t19 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG11`19"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG11`19
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg11t19 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG11`19
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG11`19 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg11t19")> _
    Public Property TouKkkHenkouFlg11t19() As Integer
        Get
            Return intTouKkkHenkouFlg11t19
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg11t19 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši20`29"
    ''' <summary>
    ''' “‰¿Ši20`29
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk20t29 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši20`29
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši20`29 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk20t29")> _
    Public Property TouKkk20t29() As Integer
        Get
            Return intTouKkk20t29
        End Get
        Set(ByVal value As Integer)
            intTouKkk20t29 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG20`29"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG20`29
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg20t29 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG20`29
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG20`29 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg20t29")> _
    Public Property TouKkkHenkouFlg20t29() As Integer
        Get
            Return intTouKkkHenkouFlg20t29
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg20t29 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši30`39"
    ''' <summary>
    ''' “‰¿Ši30`39
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk30t39 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši30`39
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši30`39 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk30t39")> _
    Public Property TouKkk30t39() As Integer
        Get
            Return intTouKkk30t39
        End Get
        Set(ByVal value As Integer)
            intTouKkk30t39 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG30`39"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG30`39
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg30t39 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG30`39
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG30`39 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg30t39")> _
    Public Property TouKkkHenkouFlg30t39() As Integer
        Get
            Return intTouKkkHenkouFlg30t39
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg30t39 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši40`49"
    ''' <summary>
    ''' “‰¿Ši40`49
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk40t49 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši40`49
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši40`49 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk40t49")> _
    Public Property TouKkk40t49() As Integer
        Get
            Return intTouKkk40t49
        End Get
        Set(ByVal value As Integer)
            intTouKkk40t49 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG40`49"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG40`49
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg40t49 As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG40`49
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG40`49 </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg40t49")> _
    Public Property TouKkkHenkouFlg40t49() As Integer
        Get
            Return intTouKkkHenkouFlg40t49
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg40t49 = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši50`"
    ''' <summary>
    ''' “‰¿Ši50`
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk50t As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši50`
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši50` </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk50t")> _
    Public Property TouKkk50t() As Integer
        Get
            Return intTouKkk50t
        End Get
        Set(ByVal value As Integer)
            intTouKkk50t = value
        End Set
    End Property
#End Region

#Region "“‰¿Ši•ÏXFLG50`"
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG50`
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg50t As Integer = Integer.MinValue
    ''' <summary>
    ''' “‰¿Ši•ÏXFLG50`
    ''' </summary>
    ''' <value></value>
    ''' <returns>“‰¿Ši•ÏXFLG50` </returns>
    ''' <remarks></remarks>
    <TableMap("tou_kkk_henkou_flg50t")> _
    Public Property TouKkkHenkouFlg50t() As Integer
        Get
            Return intTouKkkHenkouFlg50t
        End Get
        Set(ByVal value As Integer)
            intTouKkkHenkouFlg50t = value
        End Set
    End Property
#End Region

#Region "“o˜^ƒƒOƒCƒ“ƒ†[ƒU[ID"
    ''' <summary>
    ''' “o˜^ƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String= Integer.MinValue
    ''' <summary>
    ''' “o˜^ƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> “o˜^ƒƒOƒCƒ“ƒ†[ƒU[ID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id")> _
    Public Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "“o˜^“ú"
    ''' <summary>
    ''' “o˜^“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' “o˜^“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> “o˜^“ú</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime")> _
    Public Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "XVƒƒOƒCƒ“ƒ†[ƒU[ID"
    ''' <summary>
    ''' XVƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' XVƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVƒƒOƒCƒ“ƒ†[ƒU[ID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id")> _
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "XV“ú"
    ''' <summary>
    ''' XV“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' XV“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> XV“ú</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime")> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

End Class