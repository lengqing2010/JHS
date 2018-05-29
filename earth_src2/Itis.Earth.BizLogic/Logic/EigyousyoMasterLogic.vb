Imports System.Transactions
Imports Itis.Earth.DataAccess
''' <summary>
''' 調査会社マスタ
''' </summary>
''' <history>
''' <para>2010/05/15　馬艶軍(大連)　新規作成</para>
''' </history>
Public Class EigyousyoMasterLogic
    'インスタンス生成
    Private EigyousyoMasterDA As New EigyousyoMasterDataAccess

    ''' <summary>
    ''' 拡張名称マスタ
    ''' </summary>
    ''' <param name="strSyubetu">名称種別</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable
        Return EigyousyoMasterDA.SelKakutyouInfo(strSyubetu)
    End Function

    ''' <summary>
    ''' 営業所マスタ
    ''' </summary>
    ''' <param name="strEigyousyo_Cd">営業所コード</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    Public Function SelEigyousyoInfo(ByVal strEigyousyo_Cd As String, _
                                         ByVal strEigyousyoCd As String, _
                                         ByVal btn As String) As EigyousyoDataSet.m_eigyousyoDataTable
        Return EigyousyoMasterDA.SelEigyousyoInfo(strEigyousyo_Cd, strEigyousyoCd, btn)
    End Function

    ''' <summary>
    ''' Mail情報取得
    ''' </summary>
    ''' <param name="yuubin_no">郵便NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet
        Return EigyousyoMasterDA.GetMailAddress(yuubin_no)
    End Function

    ''' <summary>
    ''' 郵便番号存在チェック
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable
        Return EigyousyoMasterDA.SelYuubinInfo(strBangou)
    End Function

    ''' <summary>
    ''' 請求先マスタ存在チェック
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd"></param>
    ''' <param name="strSeikyuuSakiBrc"></param>
    ''' <param name="strSeikyuuSakiKbn"></param>
    ''' <param name="strTrue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSaki(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String, Optional ByVal strTrue As Boolean = False) As DataTable
        Return EigyousyoMasterDA.SelSeikyuuSaki(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strTrue)
    End Function

    ''' <summary>
    ''' 修正ボタンを押下時
    ''' </summary>
    ''' <param name="dtEigyousyo">更新データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdEigyousyo(ByVal dtEigyousyo As EigyousyoDataSet.m_eigyousyoDataTable, ByVal strTrue As String) As String
        '排他
        Dim dtHaita As New DataTable
        '重複チェック
        Dim blnJyuufukuS As Boolean = False
        Dim blnJyuufukuTH As Boolean = False

        '事務処理
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '営業所マスタ値存在チェック
                If EigyousyoMasterDA.SelEigyousyoInfo("", dtEigyousyo(0).eigyousyo_cd, "btnSyuusei").Rows.Count = 0 Then
                    scope.Dispose()
                    Return "2"
                Else
                    '排他チェック
                    dtHaita = SelHaita(dtEigyousyo(0).eigyousyo_cd, dtEigyousyo(0).upd_datetime)
                    If dtHaita.Rows.Count <> 0 Then
                        scope.Dispose()
                        Return String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "営業所マスタ").ToString()
                    Else
                        '20100712　仕様変更　削除　馬艶軍↓↓↓
                        'If strTrue = "OK" Then
                        '    '重複チェック_請求先マスタ
                        '    If EigyousyoMasterDA.SelSeikyuuSaki(dtEigyousyo(0).seikyuu_saki_cd, dtEigyousyo(0).seikyuu_saki_brc, dtEigyousyo(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                        '        blnJyuufukuS = True
                        '    End If

                        '    '請求先マスタ登録処理
                        '    If blnJyuufukuS = False Then
                        '        '重複チェック_請求先登録雛形マスタ
                        '        If EigyousyoMasterDA.SelSeikyuuSakiTH(dtEigyousyo(0).seikyuu_saki_brc).Rows.Count > 0 Then
                        '            blnJyuufukuTH = True
                        '        End If

                        '        '登録処理
                        '        EigyousyoMasterDA.InsSeikyuuSaki(dtEigyousyo(0).seikyuu_saki_cd, dtEigyousyo(0).seikyuu_saki_brc, dtEigyousyo(0).upd_login_user_id, blnJyuufukuTH)
                        '    End If
                        'End If
                        '20100712　仕様変更　削除　馬艶軍↑↑↑

                        '営業所マスタテーブルの修正
                        EigyousyoMasterDA.UpdEigyousyo(dtEigyousyo)
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
    ''' 排他チェック用
    ''' </summary>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strKousinDate">更新時間</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strEigyousyoCd As String, ByVal strKousinDate As String) As DataTable
        '戻る
        Return EigyousyoMasterDA.SelHaita(strEigyousyoCd, strKousinDate)
    End Function

    ''' <summary>
    ''' 営業所マスタ登録
    ''' </summary>
    ''' <param name="dtEigyousyo">登録データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsEigyousyo(ByVal dtEigyousyo As EigyousyoDataSet.m_eigyousyoDataTable, ByVal strTrue As String) As Boolean
        '請求先マスタチェック用
        Dim blnJyuufukuS As Boolean = False
        Dim blnJyuufukuTH As Boolean = False

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '20100712　仕様変更　削除　馬艶軍↓↓↓
                ''【請求先新規登録】が表示されている場合
                'If strTrue = "OK" Then
                '    '重複チェック_請求先マスタ
                '    If EigyousyoMasterDA.SelSeikyuuSaki(dtEigyousyo(0).seikyuu_saki_cd, dtEigyousyo(0).seikyuu_saki_brc, dtEigyousyo(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                '        blnJyuufukuS = True
                '    End If

                '    '請求先マスタ登録処理
                '    If blnJyuufukuS = False Then
                '        '重複チェック_請求先登録雛形マスタ
                '        If EigyousyoMasterDA.SelSeikyuuSakiTH(dtEigyousyo(0).seikyuu_saki_brc).Rows.Count > 0 Then
                '            blnJyuufukuTH = True
                '        End If

                '        '登録処理
                '        EigyousyoMasterDA.InsSeikyuuSaki(dtEigyousyo(0).seikyuu_saki_cd, dtEigyousyo(0).seikyuu_saki_brc, dtEigyousyo(0).upd_login_user_id, blnJyuufukuTH)
                '    End If
                'End If
                '20100712　仕様変更　削除　馬艶軍↑↑↑

                '登録処理
                EigyousyoMasterDA.InsEigyousyo(dtEigyousyo)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 請求先マスタビュー
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTable1DataTable
        Return EigyousyoMasterDA.SelVSeikyuuSakiInfo(strKbn, strCd, strBrc, blnDelete)
    End Function

    ''' <summary>
    ''' 請求先.検索ボタン押下時処理_請求先登録雛形マスタチェック
    ''' </summary>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiTouroku(ByVal strSeikyuuSakiBrc As String) As DataTable
        Return EigyousyoMasterDA.SelSeikyuuSakiTouroku(strSeikyuuSakiBrc)
    End Function

    ''' <summary>
    ''' 加盟店マスタを取得する。
    ''' </summary>
    Public Function SelKameiten(ByVal strEigyousyoCd As String) As DataTable
        Return EigyousyoMasterDA.SelKameiten(strEigyousyoCd)
    End Function

    ''' <summary>
    ''' 加盟店住所マスタ
    ''' </summary>
    Public Function SelKameitenJyuusyo(ByVal strKameitenCd As String, Optional ByVal strFlg As String = "") As DataTable
        Return EigyousyoMasterDA.SelKameitenJyuusyo(strKameitenCd, strFlg)
    End Function

    ''' <summary>
    ''' 加盟店住所マスタ更新と追加内容取得
    ''' </summary>
    Public Function SelNaiyou(ByVal strKameitenCd As String) As DataTable
        Return EigyousyoMasterDA.SelNaiyou(strKameitenCd)
    End Function

    ''' <summary>
    ''' 加盟店住所マスタ更新追加処理
    ''' </summary>
    ''' <param name="strEigyousyoCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetKousinTuika(ByVal strEigyousyoCd As String, ByVal strUserId As String) As Boolean
        Dim strKameitenCd As String = ""
        Dim dtNaiyou As New Data.DataTable
        Dim dtKameiten As New Data.DataTable
        Dim dtKameitenJyuusyo As New Data.DataTable
        Dim strFlg As String = ""
        '加盟店住所マスタ連携管理テーブル
        Dim dtKameitenJyuusyoRenkei As New Data.DataTable

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '加盟店マスタを取得する
                dtKameiten = SelKameiten(strEigyousyoCd)
                For i As Integer = 0 To dtKameiten.Rows.Count - 1
                    '加盟店コード取得
                    strKameitenCd = Trim(dtKameiten.Rows(i).Item("kameiten_cd").ToString)
                    '更新追加内容取得
                    dtNaiyou = SelNaiyou(strKameitenCd)
                    '加盟店住所マスタの存在チェック
                    dtKameitenJyuusyo = SelKameitenJyuusyo(strKameitenCd)

                    '20101108 馬艶軍　加盟店住所マスタ連携管理テーブルは追加・更新する必要があります　↓↓↓
                    '加盟店住所マスタ連携管理テーブルの存在チェック
                    dtKameitenJyuusyoRenkei = GetKameitenJyuusyoRenkei(strKameitenCd)
                    If dtKameitenJyuusyoRenkei.Rows.Count > 0 Then
                        'マスタの情報をもとに加盟店住所マスタ連携管理テーブルを更新する。
                        EigyousyoMasterDA.UpdKameitenJyuusyoRenkei(strKameitenCd, strUserId)
                    Else
                        '加盟店住所マスタ連携管理テーブル追加
                        EigyousyoMasterDA.InsKameitenJyuusyoRenkei(strKameitenCd, strUserId)
                    End If
                    '20101108 馬艶軍　加盟店住所マスタ連携管理テーブルは追加・更新する必要があります　↑↑↑

                    If dtKameitenJyuusyo.Rows.Count > 0 Then
                        'マスタの情報をもとに加盟店住所マスタを更新する。
                        EigyousyoMasterDA.UpdKameitenJyuusyo(strKameitenCd, dtNaiyou, strUserId)
                    Else
                        '加盟店住所マスタが存在しない場合
                        'フラグ取得
                        If SelKameitenJyuusyo(strKameitenCd, "1").Rows.Count > 0 Then
                            strFlg = "0"
                        Else
                            strFlg = "-1"
                        End If

                        '加盟店住所マスタ追加
                        EigyousyoMasterDA.InsKameitenJyuusyo(strKameitenCd, dtNaiyou, strFlg, strUserId)
                    End If
                Next
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 加盟店住所マスタ連携管理テーブル
    ''' </summary>
    ''' <history>20101108 馬艶軍 加盟店住所マスタ連携管理テーブルも追加・更新する必要があります。</history>
    Public Function GetKameitenJyuusyoRenkei(ByVal strKameitenCd As String) As DataTable
        Return EigyousyoMasterDA.SelKameitenJyuusyoRenkei(strKameitenCd)
    End Function

    ''' <summary>
    ''' CSVデータを取得する
    ''' </summary>
    ''' <returns>CSVデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function GetEigyousyoCsv() As Data.DataTable

        '戻り値
        Return EigyousyoMasterDA.SelEigyousyoCsv()

    End Function

    ''' <summary>
    ''' システム日付を取得する
    ''' </summary>
    ''' <returns>CSVデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function GetSystemDateYMD() As Data.DataTable

        '戻り値
        Return EigyousyoMasterDA.SelSystemDateYMD()

    End Function

    ''' <summary>
    ''' ddlのデータを取得する
    ''' </summary>
    ''' <returns>ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function GetDdlList(ByVal strMeisyouSyubetu As Integer) As Data.DataTable

        '戻り値
        Return EigyousyoMasterDA.SelDdlList(strMeisyouSyubetu)

    End Function

    ''' <summary>
    ''' 調査会社情報件数を取得する
    ''' </summary>
    ''' <returns>ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function GetTyousaKaisyaCount(ByVal strTyousaKaisyaCd As String) As Integer

        '戻り値
        Return CInt(EigyousyoMasterDA.SelTyousaKaisyaCount(strTyousaKaisyaCd).Rows(0).Item(0))

    End Function

    ''' <summary>
    ''' 調査会社情報を取得する
    ''' </summary>
    ''' <returns>ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function GetTyousaKaisyaInfo(ByVal strTyousaKaisyaCd As String) As Data.DataTable

        '戻り値
        Return EigyousyoMasterDA.SelTyousaKaisyaInfo(strTyousaKaisyaCd)
    End Function

    ''' <summary>
    ''' 固定チャージの入力日を取得する
    ''' </summary>
    ''' <returns>ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function GetKoteiTyaaji(ByVal strEigyousyoCd As String, ByVal blnThisMonth As Boolean) As Data.DataTable

        '戻り値
        Return EigyousyoMasterDA.SelKoteiTyaaji(strEigyousyoCd, blnThisMonth)

    End Function

    ''' <summary>
    ''' 固定チャージ情報をセットする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetKoteiTyaaji(ByVal strEigyousyoCd As String, ByVal strUserId As String) As String

        Dim strMessage As String = String.Empty

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)

            Try

                '「店別請求テーブル」を登録する
                If Not EigyousyoMasterDA.InsTenbetuSeikyuu(strEigyousyoCd, strUserId) Then
                    strMessage = String.Format(Messages.Instance.MSG2070E, "固定チャージ")
                    Throw New ApplicationException
                End If

                '店別請求テーブル連携管理テーブルで検索する
                Dim dtTenbetuSeikyuuRenkei As New Data.DataTable
                dtTenbetuSeikyuuRenkei = EigyousyoMasterDA.SelTenbetuSeikyuuRenkeiCount(strEigyousyoCd)

                If CInt(dtTenbetuSeikyuuRenkei.Rows(0).Item(0).ToString.Trim) > 0 Then
                    '存在する、更新を行う
                    If Not EigyousyoMasterDA.UpdTenbetuSeikyuuRenkei(strEigyousyoCd, strUserId) Then
                        strMessage = Messages.Instance.MSG2071E
                        Throw New ApplicationException
                    End If
                Else
                    '存在しない、挿入を行う
                    If Not EigyousyoMasterDA.InsTenbetuSeikyuuRenkei(strEigyousyoCd, strUserId) Then
                        strMessage = Messages.Instance.MSG2071E
                        Throw New ApplicationException
                    End If
                End If

                scope.Complete()

            Catch ex As Exception
                scope.Dispose()
                If strMessage.Trim.Equals(String.Empty) Then
                    strMessage = String.Format(Messages.Instance.MSG2070E, "固定チャージ")
                End If
            End Try

        End Using

        Return strMessage

    End Function

    ''' <summary>
    ''' 請求先の存在チェックする
    ''' </summary>
    ''' <returns>請求先の件数</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Public Function SelSeikyuusakiCheck(ByVal strEigyousyoCd As String) As Boolean

        Dim dtSeikyuusaki As New Data.DataTable
        dtSeikyuusaki = EigyousyoMasterDA.SelSeikyuusakiCheck(strEigyousyoCd)

        If CInt(dtSeikyuusaki.Rows(0).Item(0).ToString.Trim)>0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
