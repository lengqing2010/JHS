Imports System.Web.UI.HtmlControls
''' <summary>
''' €iξρΜRg[QΖpR[hNX
''' </summary>
''' <remarks></remarks>
Public Class SyouhinCtrlRecord

#Region "€is(TR)"
    ''' <summary>
    ''' €is(TR)
    ''' </summary>
    ''' <remarks></remarks>
    Private trSyouhinLine As HtmlTableRow
    ''' <summary>
    ''' €is(TR)
    ''' </summary>
    ''' <value></value>
    ''' <returns>€is(TR)</returns>
    ''' <remarks></remarks>
    Public Property SyouhinLine() As HtmlTableRow
        Get
            Return trSyouhinLine
        End Get
        Set(ByVal value As HtmlTableRow)
            trSyouhinLine = value
        End Set
    End Property
#End Region
#Region "ͺήR[h"
    ''' <summary>
    ''' ͺήR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private txtBunruiCd As HtmlInputHidden
    ''' <summary>
    ''' ͺήR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>ͺήR[h</returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As HtmlInputHidden
        Get
            Return txtBunruiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtBunruiCd = value
        End Set
    End Property
#End Region
#Region "€iR[h"
    ''' <summary>
    ''' €iR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinCd As HtmlInputText
    ''' <summary>
    ''' €iR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>€iR[h</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As HtmlInputText
        Get
            Return txtSyouhinCd
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyouhinCd = value
        End Set
    End Property
#End Region
#Region "€iR[hOld"
    ''' <summary>
    ''' €iR[hOld
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinCdOld As HtmlInputHidden
    ''' <summary>
    ''' €iR[hOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>€iR[hOld</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCdOld() As HtmlInputHidden
        Get
            Return txtSyouhinCdOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtSyouhinCdOld = value
        End Set
    End Property
#End Region
#Region "€iυ{^"
    ''' <summary>
    ''' €iυ{^
    ''' </summary>
    ''' <remarks></remarks>
    Private btnShouhinSearch As HtmlInputButton
    ''' <summary>
    ''' €iυ{^
    ''' </summary>
    ''' <value></value>
    ''' <returns>€iυ{^</returns>
    ''' <remarks></remarks>
    Public Property ShouhinSearchBtn() As HtmlInputButton
        Get
            Return btnShouhinSearch
        End Get
        Set(ByVal value As HtmlInputButton)
            btnShouhinSearch = value
        End Set
    End Property
#End Region
#Region "€iΌ"
    ''' <summary>
    ''' €iΌ
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinNm As HtmlInputText
    ''' <summary>
    ''' €iΌ
    ''' </summary>
    ''' <value></value>
    ''' <returns>€iΌ</returns>
    ''' <remarks></remarks>
    Public Property SyouhinNm() As HtmlInputText
        Get
            Return txtSyouhinNm
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyouhinNm = value
        End Set
    End Property
#End Region
#Region "€iΌi\¦pj"
    ''' <summary>
    ''' €iΌi\¦pj
    ''' </summary>
    ''' <remarks></remarks>
    Private txtDispSyouhinNm As HtmlGenericControl
    ''' <summary>
    ''' €iΌi\¦pj
    ''' </summary>
    ''' <value></value>
    ''' <returns>€iΌi\¦pj</returns>
    ''' <remarks></remarks>
    Public Property DispSyouhinNm() As HtmlGenericControl
        Get
            Return txtDispSyouhinNm
        End Get
        Set(ByVal value As HtmlGenericControl)
            txtDispSyouhinNm = value
        End Set
    End Property
#End Region
#Region "mθζͺ"
    ''' <summary>
    ''' mθζͺ
    ''' </summary>
    ''' <remarks></remarks>
    Private selKakuteiKbn As HtmlSelect
    ''' <summary>
    ''' mθζͺ
    ''' </summary>
    ''' <value></value>
    ''' <returns>mθζͺ</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbn() As HtmlSelect
        Get
            Return selKakuteiKbn
        End Get
        Set(ByVal value As HtmlSelect)
            selKakuteiKbn = value
        End Set
    End Property
#End Region
#Region "mθζͺSPAN"
    ''' <summary>
    ''' mθζͺSPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcKakuteiKbnSpan As HtmlGenericControl
    ''' <summary>
    ''' mθζͺSPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>mθζͺSPAN</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbnSpan() As HtmlGenericControl
        Get
            Return gcKakuteiKbnSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcKakuteiKbnSpan = value
        End Set
    End Property
#End Region
#Region "H±XΏΕ²ΰz"
    ''' <summary>
    ''' H±XΏΕ²ΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtKoumutenSeikyuuGaku As HtmlInputText
    ''' <summary>
    ''' H±XΏΕ²ΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns>H±XΏΕ²ΰz</returns>
    ''' <remarks></remarks>
    Public Property KoumutenSeikyuuGaku() As HtmlInputText
        Get
            Return txtKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtKoumutenSeikyuuGaku = value
        End Set
    End Property
#End Region
#Region "ΐΏΰz"
    ''' <summary>
    ''' ΐΏΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtJituSeikyuuGaku As HtmlInputText
    ''' <summary>
    ''' ΐΏΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns>ΐΏΰz</returns>
    ''' <remarks></remarks>
    Public Property JituSeikyuuGaku() As HtmlInputText
        Get
            Return txtJituSeikyuuGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtJituSeikyuuGaku = value
        End Set
    End Property
#End Region
#Region "ΑοΕz"
    ''' <summary>
    ''' ΑοΕz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiGaku As HtmlInputText
    ''' <summary>
    ''' ΑοΕz
    ''' </summary>
    ''' <value></value>
    ''' <returns>ΑοΕz</returns>
    ''' <remarks></remarks>
    Public Property ZeiGaku() As HtmlInputText
        Get
            Return txtZeiGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtZeiGaku = value
        End Set
    End Property
#End Region
#Region "Εζͺ"
    ''' <summary>
    ''' Εζͺ
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeikubun As HtmlInputHidden
    ''' <summary>
    ''' Εζͺ
    ''' </summary>
    ''' <value></value>
    ''' <returns>Εζͺ</returns>
    ''' <remarks></remarks>
    Public Property ZeiKubun() As HtmlInputHidden
        Get
            Return txtZeikubun
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtZeikubun = value
        End Set
    End Property
#End Region
#Region "Ε¦"
    ''' <summary>
    ''' ΑοΕz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiRitu As HtmlInputHidden
    ''' <summary>
    ''' ΑοΕz
    ''' </summary>
    ''' <value></value>
    ''' <returns>ΑοΕz</returns>
    ''' <remarks></remarks>
    Public Property ZeiRitu() As HtmlInputHidden
        Get
            Return txtZeiRitu
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtZeiRitu = value
        End Set
    End Property
#End Region
#Region "Εΰz"
    ''' <summary>
    ''' Εΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiKomiGaku As HtmlInputText
    ''' <summary>
    ''' Εΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns>Εΰz</returns>
    ''' <remarks></remarks>
    Public Property ZeiKomiGaku() As HtmlInputText
        Get
            Return txtZeiKomiGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtZeiKomiGaku = value
        End Set
    End Property
#End Region
#Region "³ψΰz"
    ''' <summary>
    ''' ³ψΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyoudakusyoKingaku As HtmlInputText
    ''' <summary>
    ''' ³ψΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns>³ψΰz</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoKingaku() As HtmlInputText
        Get
            Return txtSyoudakusyoKingaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyoudakusyoKingaku = value
        End Set
    End Property
#End Region
#Region "Ώ­sϊ"
    ''' <summary>
    ''' Ώ­sϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSeikyuusyoHakkouDate As HtmlInputText
    ''' <summary>
    ''' Ώ­sϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>Ώ­sϊ</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoHakkouDate() As HtmlInputText
        Get
            Return txtSeikyuusyoHakkouDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtSeikyuusyoHakkouDate = value
        End Set
    End Property
#End Region
#Region "γNϊ"
    ''' <summary>
    ''' γNϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageDate As HtmlInputText
    ''' <summary>
    ''' γNϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>γNϊ</returns>
    ''' <remarks></remarks>
    Public Property UriageDate() As HtmlInputText
        Get
            Return txtUriageDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtUriageDate = value
        End Set
    End Property
#End Region
#Region "ΏL³"
    ''' <summary>
    ''' ΏL³
    ''' </summary>
    ''' <remarks></remarks>
    Private selSeikyuuUmu As HtmlSelect
    ''' <summary>
    ''' ΏL³
    ''' </summary>
    ''' <value></value>
    ''' <returns>ΏL³</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmu() As HtmlSelect
        Get
            Return selSeikyuuUmu
        End Get
        Set(ByVal value As HtmlSelect)
            selSeikyuuUmu = value
        End Set
    End Property
#End Region
#Region "ΏL³SAPN"
    ''' <summary>
    ''' ΏL³SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcSeikyuuUmuSpan As HtmlGenericControl
    ''' <summary>
    ''' ΏL³SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>ΏL³SPAN</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmuSpan() As HtmlGenericControl
        Get
            Return gcSeikyuuUmuSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcSeikyuuUmuSpan = value
        End Set
    End Property
#End Region
#Region "©Ομ¬ϊ"
    ''' <summary>
    ''' ©Ομ¬ϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private txtMitumoriDate As HtmlInputText
    ''' <summary>
    ''' ©Ομ¬ϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>©Ομ¬ϊ</returns>
    ''' <remarks></remarks>
    Public Property MitumoriDate() As HtmlInputText
        Get
            Return txtMitumoriDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtMitumoriDate = value
        End Set
    End Property
#End Region
#Region "­mθFLG"
    ''' <summary>
    ''' ­mθFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private selHattyuusyoKakutei As HtmlSelect
    ''' <summary>
    ''' ­mθFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>­mθFLG</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakutei() As HtmlSelect
        Get
            Return selHattyuusyoKakutei
        End Get
        Set(ByVal value As HtmlSelect)
            selHattyuusyoKakutei = value
        End Set
    End Property
#End Region
#Region "­mθSPAN"
    ''' <summary>
    ''' ­mθSPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcHattyuusyoKakuteiSpan As HtmlGenericControl
    ''' <summary>
    ''' ­mθSPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>­mθSPAN</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiSpan() As HtmlGenericControl
        Get
            Return gcHattyuusyoKakuteiSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcHattyuusyoKakuteiSpan = value
        End Set
    End Property
#End Region
#Region "­mθFLGOld"
    ''' <summary>
    ''' ­mθFLGOld
    ''' </summary>
    ''' <remarks></remarks>
    Private selHattyuusyoKakuteiOld As HtmlInputHidden
    ''' <summary>
    ''' ­mθFLGOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>­mθFLGOld</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiOld() As HtmlInputHidden
        Get
            Return selHattyuusyoKakuteiOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            selHattyuusyoKakuteiOld = value
        End Set
    End Property
#End Region
#Region "­ΰz"
    ''' <summary>
    ''' ­ΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKingaku As HtmlInputText
    ''' <summary>
    ''' ­ΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns>­ΰz</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKingaku() As HtmlInputText
        Get
            Return txtHattyuusyoKingaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtHattyuusyoKingaku = value
        End Set
    End Property
#End Region
#Region "­ΰzOld"
    ''' <summary>
    ''' ­ΰzOld
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKingakuOld As HtmlInputHidden
    ''' <summary>
    ''' ­ΰzOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>­ΰzOld</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKingakuOld() As HtmlInputHidden
        Get
            Return txtHattyuusyoKingakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtHattyuusyoKingakuOld = value
        End Set
    End Property
#End Region
#Region "­mFϊ"
    ''' <summary>
    ''' ­mFϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKakuninbi As HtmlInputText
    ''' <summary>
    ''' ­mFϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>­mFϊ</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuninbi() As HtmlInputText
        Get
            Return txtHattyuusyoKakuninbi
        End Get
        Set(ByVal value As HtmlInputText)
            txtHattyuusyoKakuninbi = value
        End Set
    End Property
#End Region
#Region "ΰzΟXtO"
    ''' <summary>
    ''' ΰzΟXtO
    ''' </summary>
    ''' <remarks></remarks>
    Private txtKingakuFlg As HtmlInputHidden
    ''' <summary>
    ''' ΰzΟXtO
    ''' </summary>
    ''' <value></value>
    ''' <returns>ΰzΟXtO</returns>
    ''' <remarks></remarks>
    Public Property KingakuFlg() As HtmlInputHidden
        Get
            Return txtKingakuFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtKingakuFlg = value
        End Set
    End Property
#End Region
#Region "γσ΅i€iRΜέj"
    ''' <summary>
    ''' γσ΅ uriageJyoukyou
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageJyoukyou As HtmlInputHidden
    ''' <summary>
    ''' γσ΅i€iRΜέj
    ''' </summary>
    ''' <value></value>
    ''' <returns>γσ΅</returns>
    ''' <remarks></remarks>
    Public Property UriageJyoukyou() As HtmlInputHidden
        Get
            Return txtUriageJyoukyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageJyoukyou = value
        End Set
    End Property
#End Region
#Region "γvγtO"
    ''' <summary>
    ''' γvγtO uriageKeijyouFlg
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageKeijyouFlg As HtmlInputHidden
    ''' <summary>
    ''' γvγtO
    ''' </summary>
    ''' <value></value>
    ''' <returns>γvγtO</returns>
    ''' <remarks></remarks>
    Public Property UriageKeijyouFlg() As HtmlInputHidden
        Get
            Return txtUriageKeijyouFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageKeijyouFlg = value
        End Set
    End Property
#End Region
#Region "γvγϊ"
    ''' <summary>
    ''' γvγϊ uriageKeijyouBi
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageKeijyouBi As HtmlInputHidden
    ''' <summary>
    ''' γvγϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>γvγϊ</returns>
    ''' <remarks></remarks>
    Public Property UriageKeijyouBi() As HtmlInputHidden
        Get
            Return txtUriageKeijyouBi
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageKeijyouBi = value
        End Set
    End Property
#End Region
#Region "υl"
    ''' <summary>
    ''' υl bikou
    ''' </summary>
    ''' <remarks></remarks>
    Private txtBikou As HtmlInputHidden
    ''' <summary>
    ''' υl
    ''' </summary>
    ''' <value></value>
    ''' <returns>υl</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As HtmlInputHidden
        Get
            Return txtBikou
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtBikou = value
        End Set
    End Property
#End Region
#Region "κόΰFLG"
    ''' <summary>
    ''' κόΰFLG ikkatuNyuukinFlg
    ''' </summary>
    ''' <remarks></remarks>
    Private txtIkkatuNyuukinFlg As HtmlInputHidden
    ''' <summary>
    ''' κόΰFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>κόΰFLG</returns>
    ''' <remarks></remarks>
    Public Property IkkatuNyuukinFlg() As HtmlInputHidden
        Get
            Return txtIkkatuNyuukinFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtIkkatuNyuukinFlg = value
        End Set
    End Property
#End Region

End Class
