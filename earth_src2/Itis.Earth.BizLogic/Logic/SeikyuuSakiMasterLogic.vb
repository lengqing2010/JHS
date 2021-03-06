Imports Itis.Earth.DataAccess
''' <summary>
''' Ώζ}X^
''' </summary>
''' <history>
''' <para>2010/05/24@nR(εA)@VKμ¬</para>
''' </history>
Public Class SeikyuuSakiMasterLogic
    'CX^XΆ¬
    Private SeikyuuSakiMasterDA As New SeikyuuSakiMasterDataAccess

    ''' <summary>
    ''' Ώζo^`}X^
    ''' </summary>
    Public Function SelSeikyuuSakiTourokuHinagataInfo() As DataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiTourokuHinagataInfo()
    End Function

    ''' <summary>
    ''' g£ΌΜ}X^
    ''' </summary>
    ''' <param name="strSyubetu">ΌΜνΚ</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable
        Return SeikyuuSakiMasterDA.SelKakutyouInfo(strSyubetu)
    End Function

    ''' <summary>
    ''' ΏζξρΜζΎ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiInfo(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String) As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiInfo(strSeikyuuSakiCd, strSeikyuuSakiBrc, SeikyuuSakiKbn)
    End Function

    ''' <summary>
    ''' Ώζo^`}X^ξρΜζΎ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiHinagataInfo(ByVal strSeikyuuSakiBrc As String) As SeikyuuSakiHinagataDataSet.m_seikyuu_saki_touroku_hinagataDataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiHinagataInfo(strSeikyuuSakiBrc)
    End Function

    ''' <summary>
    ''' Ώζ}X^o^
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">o^f[^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                'o^
                SeikyuuSakiMasterDA.InsSeikyuuSaki(dtSeikyuuSaki)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
    End Function

    ''' <summary>
    ''' rΌ`FbNp
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">ΏζR[h</param>
    ''' <param name="strSeikyuuSakiBrc">ΏζR[h</param>
    ''' <param name="strKousinDate">XVΤ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String, ByVal strKousinDate As String) As DataTable
        'ίι
        Return SeikyuuSakiMasterDA.SelHaita(strSeikyuuSakiCd, strSeikyuuSakiBrc, SeikyuuSakiKbn, strKousinDate)
    End Function

    ''' <summary>
    ''' Ώζ}X^C³
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">o^f[^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As String
        'rΌ
        Dim dtHaita As New DataTable

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                'lΆέ`FbN
                If SeikyuuSakiMasterDA.SelSeikyuuSakiInfo(dtSeikyuuSaki(0).seikyuu_saki_cd, dtSeikyuuSaki(0).seikyuu_saki_brc, dtSeikyuuSaki(0).seikyuu_saki_kbn).Rows.Count = 0 Then
                    scope.Dispose()
                    Return "2"
                Else
                    'rΌ`FbN
                    dtHaita = SelHaita(dtSeikyuuSaki(0).seikyuu_saki_cd, dtSeikyuuSaki(0).seikyuu_saki_brc, dtSeikyuuSaki(0).seikyuu_saki_kbn, dtSeikyuuSaki(0).upd_datetime)
                    If dtHaita.Rows.Count <> 0 Then
                        scope.Dispose()
                        Return String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "Ώζ}X^").ToString()
                    Else
                        'C³
                        SeikyuuSakiMasterDA.UpdSeikyuuSaki(dtSeikyuuSaki)
                        scope.Complete()
                        Return "0"
                    End If
                End If
            Catch ex As Exception
                scope.Dispose()
                Return "1"
            End Try
        End Using
    End Function

    ''' <summary>
    ''' VοvΖ}X^
    ''' </summary>
    Public Function SelSinkaikeiJigyousyoInfo(ByVal strSkkJigyousyoCd As String) As DataTable
        Return SeikyuuSakiMasterDA.SelSinkaikeiJigyousyoInfo(strSkkJigyousyoCd)
    End Function

    ''' <summary>
    ''' ΌρζR[hi^MΗ}X^j
    ''' </summary>
    ''' <history>20100925@ΌρζR[hAΌρζΌ@ΗΑ@nR</history>
    Public Function SelNayoseSakiInfo(ByVal strNayoseSakiCd As String) As DataTable
        Return SeikyuuSakiMasterDA.SelNayoseSakiInfo(strNayoseSakiCd)
    End Function

    ''' <summary>
    ''' Ώζ}X^r[
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTableDataTable
        Return SeikyuuSakiMasterDA.SelVSeikyuuSakiInfo(strKbn, strCd, strBrc, blnDelete)
    End Function

    ''' <summary>
    ''' »ΜΌ}X^Άέ`FbN
    ''' </summary>
    Public Function SelSonzaiChk(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String) As DataTable
        Return SeikyuuSakiMasterDA.SelSonzaiChk(strKbn, strCd, strBrc)
    End Function

    ''' <summary>
    ''' MailξρζΎ
    ''' </summary>
    ''' <param name="yuubin_no">XΦNO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet
        Return SeikyuuSakiMasterDA.GetMailAddress(yuubin_no)
    End Function

    ''' <summary>
    ''' XΦΤΆέ`FbN
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable
        Return SeikyuuSakiMasterDA.SelYuubinInfo(strBangou)
    End Function

    ''' <summary>
    ''' ϋUnjtOπζΎ·ι
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 Τ΄ 407553ΜΞ ΗΑ</history>
    Public Function GetKutiburiOkFlg() As Data.DataTable

        'ίθl
        Return SeikyuuSakiMasterDA.SelKutiburiOkFlg()

    End Function

    ''' <summary>
    ''' βsxXR[hπζΎ·ι
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/05/29 ko ΏΜβsxXR[hΑΙΊ€EarthόC</history>
    Public Function GetGinkouSitenCd() As Data.DataTable

        'ίθl
        Return SeikyuuSakiMasterDA.SelGinkouSitenCd()

    End Function

    ''' <summary>
    ''' MaxϋUnjtOπζΎ·ι
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 Τ΄ 407553ΜΞ ΗΑ</history>
    Public Function GetMaxKutiburiOkFlg() As String

        Dim strMaxKutiburiOkFlg As String = String.Empty

        Dim dtMaxKutiburiOkFlg As New Data.DataTable
        dtMaxKutiburiOkFlg = SeikyuuSakiMasterDA.SelMaxKutiburiOkFlg()

        If dtMaxKutiburiOkFlg.Rows.Count > 0 Then

            strMaxKutiburiOkFlg = dtMaxKutiburiOkFlg.Rows(0).Item("tougou_tokuisaki_cd_max").ToString.Trim

            If (strMaxKutiburiOkFlg.Equals(String.Empty)) OrElse (Not IsNumeric(strMaxKutiburiOkFlg)) Then
                strMaxKutiburiOkFlg = "1"
            Else
                strMaxKutiburiOkFlg = Convert.ToString(Convert.ToDouble(strMaxKutiburiOkFlg) + 1)
            End If
        Else
            strMaxKutiburiOkFlg = "1"
        End If


        'ίθl
        Return strMaxKutiburiOkFlg

    End Function
End Class
