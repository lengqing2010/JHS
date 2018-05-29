Imports Itis.Earth.DataAccess
''' <summary>
''' 請求先マスタ
''' </summary>
''' <history>
''' <para>2010/05/24　馬艶軍(大連)　新規作成</para>
''' </history>
Public Class SeikyuuSakiMasterLogic
    'インスタンス生成
    Private SeikyuuSakiMasterDA As New SeikyuuSakiMasterDataAccess

    ''' <summary>
    ''' 請求先登録雛形マスタ
    ''' </summary>
    Public Function SelSeikyuuSakiTourokuHinagataInfo() As DataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiTourokuHinagataInfo()
    End Function

    ''' <summary>
    ''' 拡張名称マスタ
    ''' </summary>
    ''' <param name="strSyubetu">名称種別</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable
        Return SeikyuuSakiMasterDA.SelKakutyouInfo(strSyubetu)
    End Function

    ''' <summary>
    ''' 請求先情報の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiInfo(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String) As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiInfo(strSeikyuuSakiCd, strSeikyuuSakiBrc, SeikyuuSakiKbn)
    End Function

    ''' <summary>
    ''' 請求先登録雛形マスタ情報の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiHinagataInfo(ByVal strSeikyuuSakiBrc As String) As SeikyuuSakiHinagataDataSet.m_seikyuu_saki_touroku_hinagataDataTable
        Return SeikyuuSakiMasterDA.SelSeikyuuSakiHinagataInfo(strSeikyuuSakiBrc)
    End Function

    ''' <summary>
    ''' 請求先マスタ登録
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">登録データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '登録処理
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
    ''' 排他チェック用
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先コード</param>
    ''' <param name="strKousinDate">更新時間</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String, ByVal strKousinDate As String) As DataTable
        '戻る
        Return SeikyuuSakiMasterDA.SelHaita(strSeikyuuSakiCd, strSeikyuuSakiBrc, SeikyuuSakiKbn, strKousinDate)
    End Function

    ''' <summary>
    ''' 請求先マスタ修正
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">登録データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As String
        '排他
        Dim dtHaita As New DataTable

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '値存在チェック
                If SeikyuuSakiMasterDA.SelSeikyuuSakiInfo(dtSeikyuuSaki(0).seikyuu_saki_cd, dtSeikyuuSaki(0).seikyuu_saki_brc, dtSeikyuuSaki(0).seikyuu_saki_kbn).Rows.Count = 0 Then
                    scope.Dispose()
                    Return "2"
                Else
                    '排他チェック
                    dtHaita = SelHaita(dtSeikyuuSaki(0).seikyuu_saki_cd, dtSeikyuuSaki(0).seikyuu_saki_brc, dtSeikyuuSaki(0).seikyuu_saki_kbn, dtSeikyuuSaki(0).upd_datetime)
                    If dtHaita.Rows.Count <> 0 Then
                        scope.Dispose()
                        Return String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "請求先マスタ").ToString()
                    Else
                        '修正処理
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
    ''' 新会計事業所マスタ
    ''' </summary>
    Public Function SelSinkaikeiJigyousyoInfo(ByVal strSkkJigyousyoCd As String) As DataTable
        Return SeikyuuSakiMasterDA.SelSinkaikeiJigyousyoInfo(strSkkJigyousyoCd)
    End Function

    ''' <summary>
    ''' 名寄先コード（与信管理マスタ）
    ''' </summary>
    ''' <history>20100925　名寄先コード、名寄先名　追加　馬艶軍</history>
    Public Function SelNayoseSakiInfo(ByVal strNayoseSakiCd As String) As DataTable
        Return SeikyuuSakiMasterDA.SelNayoseSakiInfo(strNayoseSakiCd)
    End Function

    ''' <summary>
    ''' 請求先マスタビュー
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTableDataTable
        Return SeikyuuSakiMasterDA.SelVSeikyuuSakiInfo(strKbn, strCd, strBrc, blnDelete)
    End Function

    ''' <summary>
    ''' その他マスタ存在チェック
    ''' </summary>
    Public Function SelSonzaiChk(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String) As DataTable
        Return SeikyuuSakiMasterDA.SelSonzaiChk(strKbn, strCd, strBrc)
    End Function

    ''' <summary>
    ''' Mail情報取得
    ''' </summary>
    ''' <param name="yuubin_no">郵便NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet
        Return SeikyuuSakiMasterDA.GetMailAddress(yuubin_no)
    End Function

    ''' <summary>
    ''' 郵便番号存在チェック
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable
        Return SeikyuuSakiMasterDA.SelYuubinInfo(strBangou)
    End Function

    ''' <summary>
    ''' 口振ＯＫフラグを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 車龍 407553の対応 追加</history>
    Public Function GetKutiburiOkFlg() As Data.DataTable

        '戻り値
        Return SeikyuuSakiMasterDA.SelKutiburiOkFlg()

    End Function

    ''' <summary>
    ''' 銀行支店コードを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/05/29 楊双 請求書の銀行支店コード増加に伴うEarth改修</history>
    Public Function GetGinkouSitenCd() As Data.DataTable

        '戻り値
        Return SeikyuuSakiMasterDA.SelGinkouSitenCd()

    End Function

    ''' <summary>
    ''' Max口振ＯＫフラグを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 車龍 407553の対応 追加</history>
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


        '戻り値
        Return strMaxKutiburiOkFlg

    End Function
End Class
