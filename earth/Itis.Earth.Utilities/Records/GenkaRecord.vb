Public Class GenkaRecord

#Region "²¸ïÐR[h"
    ''' <summary>
    ''' ²¸ïÐR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ²¸ïÐR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐR[h</returns>
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

#Region "ÆR[h"
    ''' <summary>
    ''' ÆR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strJigyousyoCd As String
    ''' <summary>
    ''' ÆR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÆR[h</returns>
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
    ''' èæíÊ
    ''' </summary>
    ''' <remarks></remarks>
    Private intAitesakiSyubetu As Integer = Integer.MinValue
    ''' <summary>
    ''' èæíÊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>èæíÊ </returns>
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
    ''' èæR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strAitesakiCd As String
    ''' <summary>
    ''' èæR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>èæR[h </returns>
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

#Region "¤iR[h"
    ''' <summary>
    ''' ¤iR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ¤iR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤iR[h</returns>
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

#Region "²¸û@NO"
    ''' <summary>
    ''' ²¸û@NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHouhouNo As Integer = Integer.MinValue
    ''' <summary>
    ''' ²¸û@NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸û@NO</returns>
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

#Region "¿i1"
    ''' <summary>
    ''' ¿i1
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk1 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i1
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i1 </returns>
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

#Region "¿iÏXFLG1"
    ''' <summary>
    ''' ¿iÏXFLG1
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg1 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG1
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG1 </returns>
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

#Region "¿i2"
    ''' <summary>
    ''' ¿i2
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk2 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i2
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i2 </returns>
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

#Region "¿iÏXFLG2"
    ''' <summary>
    ''' ¿iÏXFLG2
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg2 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG2
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG2 </returns>
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

#Region "¿i3"
    ''' <summary>
    ''' ¿i3
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk3 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i3
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i3 </returns>
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

#Region "¿iÏXFLG3"
    ''' <summary>
    ''' ¿iÏXFLG3
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg3 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG3
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG3 </returns>
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

#Region "¿i4"
    ''' <summary>
    ''' ¿i4
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk4 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i4
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i4 </returns>
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

#Region "¿iÏXFLG4"
    ''' <summary>
    ''' ¿iÏXFLG4
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg4 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG4
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG4 </returns>
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

#Region "¿i5"
    ''' <summary>
    ''' ¿i5
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk5 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i5
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i5 </returns>
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

#Region "¿iÏXFLG5"
    ''' <summary>
    ''' ¿iÏXFLG5
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg5 As Integer= Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG5
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG5 </returns>
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

#Region "¿i6"
    ''' <summary>
    ''' ¿i6
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk6 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i6
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i6 </returns>
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

#Region "¿iÏXFLG6"
    ''' <summary>
    ''' ¿iÏXFLG6
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg6 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG6
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG6 </returns>
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

#Region "¿i7"
    ''' <summary>
    ''' ¿i7
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk7 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i7
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i7 </returns>
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

#Region "¿iÏXFLG7"
    ''' <summary>
    ''' ¿iÏXFLG7
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg7 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG7
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG7 </returns>
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

#Region "¿i8"
    ''' <summary>
    ''' ¿i8
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk8 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i8
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i8 </returns>
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

#Region "¿iÏXFLG8"
    ''' <summary>
    ''' ¿iÏXFLG8
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg8 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG8
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG8 </returns>
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

#Region "¿i9"
    ''' <summary>
    ''' ¿i9
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk9 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i9
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i9 </returns>
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

#Region "¿iÏXFLG9"
    ''' <summary>
    ''' ¿iÏXFLG9
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg9 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG9
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG9 </returns>
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

#Region "¿i10"
    ''' <summary>
    ''' ¿i10
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk10 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i10
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i10 </returns>
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

#Region "¿iÏXFLG10"
    ''' <summary>
    ''' ¿iÏXFLG10
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg10 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG10
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG10 </returns>
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

#Region "¿i11`19"
    ''' <summary>
    ''' ¿i11`19
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk11t19 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i11`19
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i11`19 </returns>
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

#Region "¿iÏXFLG11`19"
    ''' <summary>
    ''' ¿iÏXFLG11`19
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg11t19 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG11`19
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG11`19 </returns>
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

#Region "¿i20`29"
    ''' <summary>
    ''' ¿i20`29
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk20t29 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i20`29
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i20`29 </returns>
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

#Region "¿iÏXFLG20`29"
    ''' <summary>
    ''' ¿iÏXFLG20`29
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg20t29 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG20`29
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG20`29 </returns>
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

#Region "¿i30`39"
    ''' <summary>
    ''' ¿i30`39
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk30t39 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i30`39
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i30`39 </returns>
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

#Region "¿iÏXFLG30`39"
    ''' <summary>
    ''' ¿iÏXFLG30`39
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg30t39 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG30`39
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG30`39 </returns>
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

#Region "¿i40`49"
    ''' <summary>
    ''' ¿i40`49
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk40t49 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i40`49
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i40`49 </returns>
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

#Region "¿iÏXFLG40`49"
    ''' <summary>
    ''' ¿iÏXFLG40`49
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg40t49 As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG40`49
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG40`49 </returns>
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

#Region "¿i50`"
    ''' <summary>
    ''' ¿i50`
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkk50t As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿i50`
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿i50` </returns>
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

#Region "¿iÏXFLG50`"
    ''' <summary>
    ''' ¿iÏXFLG50`
    ''' </summary>
    ''' <remarks></remarks>
    Private intTouKkkHenkouFlg50t As Integer = Integer.MinValue
    ''' <summary>
    ''' ¿iÏXFLG50`
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿iÏXFLG50` </returns>
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

#Region "o^OC[U[ID"
    ''' <summary>
    ''' o^OC[U[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String= Integer.MinValue
    ''' <summary>
    ''' o^OC[U[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> o^OC[U[ID</returns>
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

#Region "o^ú"
    ''' <summary>
    ''' o^ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' o^ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> o^ú</returns>
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

#Region "XVOC[U[ID"
    ''' <summary>
    ''' XVOC[U[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' XVOC[U[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVOC[U[ID</returns>
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

#Region "XVú"
    ''' <summary>
    ''' XVú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' XVú
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVú</returns>
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