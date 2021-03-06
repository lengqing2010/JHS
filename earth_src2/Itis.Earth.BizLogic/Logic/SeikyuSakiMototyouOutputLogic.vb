Imports Itis.Earth.DataAccess
Imports System.Data
Imports System.Data.SqlClient
''' <summary>Ώζ³  [oΝ</summary>
''' <history>
''' <para>2010/07/14@€θΗz(εAξρVXe)@VKμ¬</para>
''' </history>
Public Class SeikyuSakiMototyouOutputLogic

    ''' <summary>Ώζ³  [oΝNXΜCX^XΆ¬ </summary>
    Private seikyuSakiMototyouOutputDataAccess As New SeikyuSakiMototyouOutputDataAccess

#Region "Ώζ³ _JzcζΎ"
    ''' <summary>
    ''' Ώζ³ _JzcζΎ
    ''' </summary>
    ''' <param name="seikyuuSakiCd">ΏζR[h</param>
    ''' <param name="seikyuuSakiBrc">Ώζ}Τ</param>
    ''' <param name="seikyuuSakiKbn">Ώζζͺ</param>
    ''' <param name="fromDate">NϊFROM</param>
    ''' <returns>Jzc(Long^)</returns>
    ''' <remarks></remarks>
    Public Function GetKurikosiZandaData(ByVal seikyuuSakiCd As String, _
                                                      ByVal seikyuuSakiBrc As String, _
                                                      ByVal seikyuuSakiKbn As String, _
                                                      ByVal fromDate As String _
                                                      ) As Long


        Dim Data As Data.DataTable
        Dim retData As Long

        'f[^ζΎ
        Data = seikyuSakiMototyouOutputDataAccess.SelKurikosiZandaData(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate)

        ' ζΎoΘ’κAσπΤp
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'LongΙLXg
                retData = CType(Data.Rows(0).Item("kurikosi_zan").ToString.Trim, Long)
            Catch ex As Exception
                'Έs΅½η[
                retData = 0
            End Try
        End If

        'lί΅
        Return retData

    End Function
#End Region

    ''' <summary>γf[^e[uAόΰf[^e[uf[^πζΎ·ι</summary>
    ''' <param name="strSeikyuuSakiCd"> ΏζR[h</param>
    ''' <param name="strSeikyuuSakiBrc"> Ώζ}Τ</param>
    ''' <param name="strSeikyuuSakiKbn"> Ώζζͺ</param>
    ''' <param name="strFromDate"> oϊΤFROM(YYYY/MM/DD)</param>
    ''' <param name="strToDate"> oϊΤTO(YYYY/MM/DD)</param>
    ''' <returns>γAόΰΜf[^</returns>
    Public Function GetUriageNyukinData(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String, ByVal strFromDate As String, ByVal strToDate As String) As Data.DataTable
        'f[^ζΎ
        Return SeikyuSakiMototyouOutputDataAccess.SelUriageNyukinData(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strFromDate, strToDate)

    End Function

End Class
