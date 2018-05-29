Imports Itis.Earth.DataAccess
''' <summary>
''' 調査会社マスタ
''' </summary>
''' <history>
''' <para>2010/05/15　馬艶軍(大連)　新規作成</para>
''' </history>
Public Class TyousaKaisyaMasterLogic

    'インスタンス生成
    Private TyousaKaisyaMasterDA As New TyousaKaisyaMasterDataAccess

    ''' <summary>
    ''' 拡張名称マスタ
    ''' </summary>
    ''' <param name="strSyubetu">名称種別</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable
        Return TyousaKaisyaMasterDA.SelKakutyouInfo(strSyubetu)
    End Function

    ''' <summary>
    ''' 調査会社マスタ
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">事業所コード</param>
    Public Function SelMTyousaKaisyaInfo(ByVal strTyousaKaisya_Cd As String, _
                                         ByVal strTysKaisyaCd As String, _
                                         ByVal strJigyousyoCd As String, _
                                         ByVal btn As String) As TyousaKaisyaDataSet.m_tyousakaisyaDataTable

        Return TyousaKaisyaMasterDA.SelMTyousaKaisyaInfo(strTyousaKaisya_Cd, strTysKaisyaCd, strJigyousyoCd, btn)
    End Function

    ''' <summary>
    ''' 修正ボタンを押下時
    ''' </summary>
    ''' <param name="dtTyousaKaisya">更新データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdTyousaKaisya(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable, ByVal strHenkou As String, ByVal strDisplayName As String, ByVal strTrue As String) As String
        '排他
        Dim dtHaita As New DataTable
        '重複チェック
        Dim blnJyuufukuS As Boolean = False
        Dim blnJyuufukuTH As Boolean = False

        '事務処理
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '調査会社マスタ値存在チェック
                If TyousaKaisyaMasterDA.SelMTyousaKaisyaInfo("", dtTyousaKaisya(0).tys_kaisya_cd, dtTyousaKaisya(0).jigyousyo_cd, "btnSyuusei").Rows.Count = 0 Then
                    scope.Dispose()
                    Return "2"
                Else
                    ''重複チェック_請求先マスタ
                    'If TyousaKaisyaMasterDA.SelSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                    '    blnJyuufukuS = True
                    'End If

                    '排他チェック
                    dtHaita = SelHaita(dtTyousaKaisya(0).tys_kaisya_cd, dtTyousaKaisya(0).jigyousyo_cd, dtTyousaKaisya(0).upd_datetime)
                    If dtHaita.Rows.Count <> 0 Then
                        scope.Dispose()
                        Return String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "調査会社マスタ").ToString()
                    Else
                        If strTrue = "OK" Then
                            '重複チェック_請求先マスタ
                            If TyousaKaisyaMasterDA.SelSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                                blnJyuufukuS = True
                            End If

                            '請求先マスタ登録処理
                            If blnJyuufukuS = False Then
                                '重複チェック_請求先登録雛形マスタ
                                If TyousaKaisyaMasterDA.SelSeikyuuSakiTH(dtTyousaKaisya(0).seikyuu_saki_brc).Rows.Count > 0 Then
                                    blnJyuufukuTH = True
                                End If

                                '登録処理
                                TyousaKaisyaMasterDA.InsSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).upd_login_user_id, blnJyuufukuTH)
                            End If
                        End If

                        '調査会社マスタテーブルの修正
                        TyousaKaisyaMasterDA.UpdTyousaKaisya(dtTyousaKaisya, strHenkou, strDisplayName)

                        '==============2011/06/24 車龍 【連携調査会社マスタの登録・削除処理】の追加 開始↓======================
                        '連携調査会社マスタの登録・削除処理
                        If Not SetRenkeiTyousakaisyaMaster(dtTyousaKaisya) Then
                            Throw New ApplicationException
                        End If
                        '==============2011/06/24 車龍 【連携調査会社マスタの登録・削除処理】の追加 終了↑======================

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
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">事業所コード</param>
    ''' <param name="strKousinDate">更新時間</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strTysKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKousinDate As String) As DataTable
        '戻る
        Return TyousaKaisyaMasterDA.SelHaita(strTysKaisyaCd, strJigyousyoCd, strKousinDate)
    End Function

    ''' <summary>
    ''' 請求先.検索ボタン押下時処理_請求先登録雛形マスタチェック
    ''' </summary>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiTouroku(ByVal strSeikyuuSakiBrc As String) As DataTable
        Return TyousaKaisyaMasterDA.SelSeikyuuSakiTouroku(strSeikyuuSakiBrc)
    End Function

    ''' <summary>
    ''' 調査会社マスタ登録
    ''' </summary>
    ''' <param name="dtTyousaKaisya">登録データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsTyousaKaisya(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable, ByVal strHenkou As String, ByVal strDisplayName As String, ByVal strTrue As String) As Boolean
        '請求先マスタチェック用
        Dim blnJyuufukuS As Boolean = False
        Dim blnJyuufukuTH As Boolean = False

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                '【請求先新規登録】が表示されている場合
                If strTrue = "OK" Then
                    '重複チェック_請求先マスタ
                    If TyousaKaisyaMasterDA.SelSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).seikyuu_saki_kbn).Rows.Count > 0 Then
                        blnJyuufukuS = True
                    End If

                    '請求先マスタ登録処理
                    If blnJyuufukuS = False Then
                        '重複チェック_請求先登録雛形マスタ
                        If TyousaKaisyaMasterDA.SelSeikyuuSakiTH(dtTyousaKaisya(0).seikyuu_saki_brc).Rows.Count > 0 Then
                            blnJyuufukuTH = True
                        End If

                        '登録処理
                        TyousaKaisyaMasterDA.InsSeikyuuSaki(dtTyousaKaisya(0).seikyuu_saki_cd, dtTyousaKaisya(0).seikyuu_saki_brc, dtTyousaKaisya(0).upd_login_user_id, blnJyuufukuTH)
                    End If
                End If

                '登録処理
                TyousaKaisyaMasterDA.InsTyousaKaisya(dtTyousaKaisya, strHenkou, strDisplayName)

                '==============2011/06/24 車龍 【連携調査会社マスタの登録・削除処理】の追加 開始↓======================
                '連携調査会社マスタの登録・削除処理
                If Not SetRenkeiTyousakaisyaMaster(dtTyousaKaisya) Then
                    Throw New ApplicationException
                End If
                '==============2011/06/24 車龍 【連携調査会社マスタの登録・削除処理】の追加 終了↑======================

                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using
    End Function

    '''' <summary>
    '''' 建物検査.FCマスタ
    '''' </summary>
    '''' <param name="strFCCd">建物検査センターコード</param>
    'Public Function SelMfcInfo(ByVal strFCCd As String) As DataTable
    '    Return TyousaKaisyaMasterDA.SelMfcInfo(strFCCd)
    'End Function

    ''' <summary>
    ''' 工事報告書直送情報取得
    ''' </summary>
    ''' <param name="strUserId">ログインユーザ</param>
    Public Function SelKoujiInfo(ByVal strUserId As String) As DataTable
        Return TyousaKaisyaMasterDA.SelKoujiInfo(strUserId)
    End Function

    ''' <summary>
    ''' Mail情報取得
    ''' </summary>
    ''' <param name="yuubin_no">郵便NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet
        Return TyousaKaisyaMasterDA.GetMailAddress(yuubin_no)
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
        Return TyousaKaisyaMasterDA.SelSeikyuuSaki(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strTrue)
    End Function

    ''' <summary>
    ''' FCコード存在チェック
    ''' </summary>
    Public Function SelFCTenInfo(ByVal strFcCd As String) As DataTable
        Return TyousaKaisyaMasterDA.SelFCTenInfo(strFcCd)
    End Function

    ''' <summary>
    ''' 郵便番号存在チェック
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable
        Return TyousaKaisyaMasterDA.SelYuubinInfo(strBangou)
    End Function

    ''' <summary>
    ''' 調査会社マスタ
    ''' </summary>
    Public Function SelTyousaKaisya(ByVal strKaisyaCd As String, ByVal strJigyouCd As String, ByVal bloKbn As Boolean) As CommonSearchDataSet.tyousakaisyaTableDataTable
        Return TyousaKaisyaMasterDA.SelTyousaKaisya(strKaisyaCd, strJigyouCd, bloKbn)
    End Function

    ''' <summary>
    ''' FC店マスタ
    ''' </summary>
    Public Function SelFCTen(ByVal strFCCd As String) As CommonSearchDataSet.KeiretuTableDataTable
        Return TyousaKaisyaMasterDA.SelFCTen(strFCCd)
    End Function

    ''' <summary>
    ''' 新会計支払先マスタ
    ''' </summary>
    Public Function SelSKK(ByVal strJigyouCd As String, ByVal strShriCd As String) As DataTable
        Return TyousaKaisyaMasterDA.SelSKK(strJigyouCd, strShriCd)
    End Function

    ''' <summary>
    ''' 請求先マスタビュー
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTable1DataTable
        Return TyousaKaisyaMasterDA.SelVSeikyuuSakiInfo(strKbn, strCd, strBrc, blnDelete)
    End Function

    ''' <summary>
    ''' 営業所マスタ
    ''' </summary>
    Public Function SelEigyousyo(ByVal strFCCd As String) As CommonSearchDataSet.EigyousyoTableDataTable
        Return TyousaKaisyaMasterDA.SelEigyousyo(strFCCd)
    End Function

    '==============2011/06/24 車龍 【連携調査会社マスタの登録・削除処理】の追加 開始↓======================
    ''' <summary>
    ''' 連携調査会社マスタの登録・削除処理
    ''' </summary>
    Private Function SetRenkeiTyousakaisyaMaster(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable) As Boolean
        '調査会社コード
        Dim strTyousakaisyaCd As String = dtTyousaKaisya.Item(0).tys_kaisya_cd.Trim
        '事業所コード
        Dim strJigyousyoCd As String = dtTyousaKaisya.Item(0).jigyousyo_cd.Trim
        'Ｒ－ＪＨＳトークン
        Dim strRJhsTokenFlg As String = dtTyousaKaisya.Item(0).report_jhs_token_flg.Trim
        'ユーザーID
        Dim strUserId As String = dtTyousaKaisya.Item(0).add_login_user_id.Trim

        '連携調査会社マスタを検索する（KEY：調査会社コード、事業所コード）
        Dim dtRenkeiTyousakaisyaMaster As New Data.DataTable
        dtRenkeiTyousakaisyaMaster = TyousaKaisyaMasterDA.SelRenkeiTyousakaisyaMaster(strTyousakaisyaCd, strJigyousyoCd)
        '該当データがありの時
        If dtRenkeiTyousakaisyaMaster.Rows.Count > 0 Then
            '画面.Ｒ－ＪＨＳトークン＝”無し”の時
            If strRJhsTokenFlg.Equals("0") Then
                '該当の連携調査会社マスタのレコードを削除する
                If Not TyousaKaisyaMasterDA.DelRenkeiTyousakaisyaMaster(strTyousakaisyaCd, strJigyousyoCd) Then
                    Return False
                End If
            End If
        Else
            '該当データがなしの時
            '画面.Ｒ－ＪＨＳトークン＝”有り”の時
            If strRJhsTokenFlg.Equals("1") Then
                '連携調査会社マスタにレコードを追加する
                If Not TyousaKaisyaMasterDA.InsRenkeiTyousakaisyaMaster(strTyousakaisyaCd, strJigyousyoCd, strUserId) Then
                    Return False
                End If
            End If
        End If

        Return True
    End Function
    '==============2011/06/24 車龍 【連携調査会社マスタの登録・削除処理】の追加 終了↑======================

End Class
