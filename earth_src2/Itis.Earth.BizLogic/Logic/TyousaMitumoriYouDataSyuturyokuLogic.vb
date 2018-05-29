Imports Itis.Earth.DataAccess
Imports System.Transactions

Public Class TyousaMitumoriYouDataSyuturyokuLogic

    'インスタンス生成
    Private TyousaMitumoriYouDataSyuturyokuDA As New TyousaMitumoriYouDataSyuturyokuDataAccess

    Private bolFlg As Boolean = False

    ''' <summary>
    ''' 調査見積データ出力
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strHosyousyoNo1">保証書NO1</param>
    ''' <param name="strHosyousyoNo2">保証書NO2</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strMitumoriFlg">見積書作成フラグ</param>
    ''' <history>20100925　馬艶軍</history>
    Public Function GetTyousaMitumoriInfo(ByVal strKubun As String, _
                                          ByVal strHosyousyoNo1 As String, _
                                          ByVal strHosyousyoNo2 As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal strMitumoriFlg As String, _
                                          ByVal strKoFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As DataTable
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.SelTyousaMitumoriInfo(strKubun, strHosyousyoNo1, strHosyousyoNo2, strKameitenCd, strMitumoriFlg, strKoFlg, strSesyuMei, strKeiretuCd, strTS)

    End Function

    ''' <summary>
    ''' 調査見積データ出力の総件数
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strHosyousyoNo1">保証書NO1</param>
    ''' <param name="strHosyousyoNo2">保証書NO2</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strMitumoriFlg">見積書作成フラグ</param>
    ''' <history>20100925　馬艶軍</history>
    Public Function GetTyousaMitumoriCount(ByVal strKubun As String, _
                                          ByVal strHosyousyoNo1 As String, _
                                          ByVal strHosyousyoNo2 As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal strMitumoriFlg As String, ByVal strKoFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As Int64
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.SelTyousaMitumoriCount(strKubun, strHosyousyoNo1, strHosyousyoNo2, strKameitenCd, strMitumoriFlg, strKoFlg, strSesyuMei, strKeiretuCd, strTS)

    End Function

    ''' <summary>
    ''' CSVデータ（個別の場合）
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strMitumoriFlg">見積書作成フラグ</param>
    ''' <history>20100925　馬艶軍</history>
    Public Function GetCsvDataKobetu(ByVal strKubun As String, _
                                          ByVal strHosyousyoNo As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal strMitumoriFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As DataTable
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.SelCsvDataKobetu(strKubun, strHosyousyoNo, strKameitenCd, strMitumoriFlg, strSesyuMei, strKeiretuCd, strTS)
    End Function

    ''' <summary>
    ''' CSVデータ（連棟の場合）
    ''' </summary>
    ''' <param name="strKubun_HosyousyoNo">区分_保証書NO</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strMitumoriFlg">見積書作成フラグ</param>
    ''' <history>20100925　馬艶軍</history>
    Public Function GetCsvDataRentou(ByVal strKubun_HosyousyoNo As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal intCount As Integer, _
                                          ByVal strMitumoriFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As DataTable
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.SelCsvDataRentou(strKubun_HosyousyoNo, strKameitenCd, intCount, strMitumoriFlg, strSesyuMei, strKeiretuCd, strTS)
    End Function

    ''' <summary>
    ''' 物件履歴表に、データが存在チェック
    ''' </summary>
    ''' <param name="drRow">検索条件</param>
    ''' <history>20100925　馬艶軍</history>
    Public Function GetBukkenRirekiChk(ByVal drRow As DataRow) As DataTable
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.SelBukkenRirekiChk(drRow)
    End Function

    ''' <summary>
    ''' 物件履歴表に、入力NOの取得
    ''' </summary>
    ''' <param name="drRow">検索条件</param>
    ''' <history>20100925　馬艶軍</history>
    Public Function GetBukkenRirekiNo(ByVal drRow As DataRow) As DataTable
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.SelBukkenRirekiNo(drRow)
    End Function

    ''' <summary>
    ''' データが存在しない時、新規データを登録する
    ''' </summary>
    ''' <param name="drRow">新規データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsBukkenRireki(ByVal drRow As DataRow, Optional ByVal bloSonzaiFlg As Boolean = True) As Boolean
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.InsBukkenRireki(drRow, bloSonzaiFlg)
    End Function

    ''' <summary>
    ''' データが存在する時、データを更新する
    ''' </summary>
    ''' <param name="drRow">検索条件</param>
    ''' <history>20100925　馬艶軍</history>
    Public Function UpdBukkenRireki(ByVal drRow As DataRow) As Boolean
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.UpdBukkenRireki(drRow)
    End Function

    ''' <summary>
    ''' カーソル移動時、加盟店名取得
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <history>20100925　馬艶軍</history>
    Public Function GetKameitenMei(ByVal strKameitenCd As String) As DataTable
        'データ取得
        Return TyousaMitumoriYouDataSyuturyokuDA.SelKameitenMei(strKameitenCd)
    End Function

    ''' <summary>
    ''' カーソル移動時、加盟店名取得
    ''' </summary>
    ''' <history>20100925　馬艶軍</history>
    Public Function SetCsvData(ByVal Response As System.Web.HttpResponse, ByVal grdItiran As Web.UI.WebControls.GridView, ByVal strMitumoriFlg As String, ByVal rbnFlg1 As Boolean, ByVal strCsvFlg As String, ByVal strUserId As String _
                                , ByRef dtCsvTable As Data.DataTable, _
                                ByVal strSesyuMei As String, _
                                ByVal strKeiretuCd As String, _
                                ByVal strTS As String) As Boolean

        Dim dtBukkenRirekiNo As New Data.DataTable
        'CSVデータ
        With dtCsvTable.Columns
            .Add(New System.Data.DataColumn("kbn", GetType(String)))
            .Add(New System.Data.DataColumn("hosyousyo_no", GetType(String)))
            .Add(New System.Data.DataColumn("sesyu_mei", GetType(String)))
            .Add(New System.Data.DataColumn("kameiten_cd", GetType(String)))
            .Add(New System.Data.DataColumn("kameiten_mei1", GetType(String)))
            .Add(New System.Data.DataColumn("irai_tantousya_mei", GetType(String)))
            .Add(New System.Data.DataColumn("bukken_jyuusyo1", GetType(String)))
            .Add(New System.Data.DataColumn("bukken_jyuusyo2", GetType(String)))
            .Add(New System.Data.DataColumn("bukken_jyuusyo3", GetType(String)))
            .Add(New System.Data.DataColumn("bunrui_cd", GetType(String)))
            .Add(New System.Data.DataColumn("syouhin_cd", GetType(String)))
            .Add(New System.Data.DataColumn("syouhin_mei", GetType(String)))
            .Add(New System.Data.DataColumn("suuryou", GetType(String)))
            .Add(New System.Data.DataColumn("tani", GetType(String)))
            .Add(New System.Data.DataColumn("tanka", GetType(String)))
            .Add(New System.Data.DataColumn("kingaku", GetType(String)))
            '====================↓2015/06/18 430002 修正↓====================
            .Add(New System.Data.DataColumn("zeiritu", GetType(String)))
            '====================↑2015/06/18 430002 修正↑====================
        End With
        Dim drCsvRow As Data.DataRow

        'パラメータを作成
        Dim dtTable As New Data.DataTable
        With dtTable.Columns
            .Add(New System.Data.DataColumn("kbn", GetType(String)))
            .Add(New System.Data.DataColumn("hosyousyo_no", GetType(String)))
            .Add(New System.Data.DataColumn("rireki_syubetu", GetType(String)))
            .Add(New System.Data.DataColumn("rireki_no", GetType(String)))
            .Add(New System.Data.DataColumn("nyuuryoku_no", GetType(String)))
            .Add(New System.Data.DataColumn("hanyou_cd", GetType(String)))
            .Add(New System.Data.DataColumn("henkou_kahi_flg", GetType(String)))
            .Add(New System.Data.DataColumn("torikesi", GetType(String)))
            .Add(New System.Data.DataColumn("add_login_user_id", GetType(String)))
            .Add(New System.Data.DataColumn("upd_login_user_id", GetType(String)))
        End With

        Using scope As New TransactionScope(TransactionScopeOption.Required, TimeSpan.Zero)
            Try
                'CSVデータ
                Dim CsvTable As New Data.DataTable
                If rbnFlg1 = True Then
                    '個別の場合
                    For intRow As Integer = 0 To grdItiran.Rows.Count - 1
                        If CType(grdItiran.Rows(intRow).FindControl("chkTaisyou"), Web.UI.WebControls.CheckBox).Checked = True Then

                            'CSVデータを出力
                            CsvTable = GetCsvDataKobetu(grdItiran.Rows(intRow).Cells(1).Text.Trim, grdItiran.Rows(intRow).Cells(2).Text.Trim, grdItiran.Rows(intRow).Cells(5).Text.Trim, strMitumoriFlg, strSesyuMei, strKeiretuCd, strTS)

                            If CsvTable.Rows.Count > 0 Then
                                '物件履歴表処理↓
                                dtTable.Rows.Clear()
                                Dim drRow As DataRow
                                drRow = dtTable.NewRow
                                drRow.Item("kbn") = grdItiran.Rows(intRow).Cells(1).Text.Trim
                                drRow.Item("hosyousyo_no") = grdItiran.Rows(intRow).Cells(2).Text.Trim
                                drRow.Item("rireki_syubetu") = "27"
                                drRow.Item("rireki_no") = "1"
                                dtTable.Rows.Add(drRow)

                                '入力NO
                                Dim strNyuuryokuNo As String = "0"
                                dtBukkenRirekiNo = GetBukkenRirekiNo(dtTable.Rows(0))
                                If dtBukkenRirekiNo.Rows.Count > 0 Then
                                    strNyuuryokuNo = dtBukkenRirekiNo.Rows(0).Item("nyuuryoku_no").ToString
                                End If

                                dtTable.Rows.Clear()
                                drRow = dtTable.NewRow
                                drRow.Item("kbn") = grdItiran.Rows(intRow).Cells(1).Text.Trim
                                drRow.Item("hosyousyo_no") = grdItiran.Rows(intRow).Cells(2).Text.Trim
                                drRow.Item("rireki_syubetu") = "27"
                                drRow.Item("rireki_no") = "1"
                                drRow.Item("nyuuryoku_no") = (CInt(strNyuuryokuNo) + 1).ToString
                                drRow.Item("hanyou_cd") = "010"
                                drRow.Item("henkou_kahi_flg") = "1"
                                drRow.Item("torikesi") = "0"
                                drRow.Item("add_login_user_id") = strUserId
                                drRow.Item("upd_login_user_id") = strUserId
                                dtTable.Rows.Add(drRow)

                                If GetBukkenRirekiChk(dtTable.Rows(0)).Rows.Count = 0 Then
                                    '登録
                                    InsBukkenRireki(dtTable.Rows(0), False)
                                Else
                                    '更新
                                    InsBukkenRireki(dtTable.Rows(0), True)
                                End If
                                '物件履歴表処理↑

                                'CSVファイル内容設定
                                For intRow1 As Integer = 0 To CsvTable.Rows.Count - 1
                                    drCsvRow = dtCsvTable.NewRow
                                    drCsvRow = CsvTable.Rows(intRow1)
                                    dtCsvTable.Rows.Add(drCsvRow.ItemArray)

                                    '邸別請求テーブルへ下記内容を書き込み
                                    TyousaMitumoriYouDataSyuturyokuDA.UpdTeibetuSeikyuu(grdItiran.Rows(intRow).Cells(1).Text.Trim, grdItiran.Rows(intRow).Cells(2).Text.Trim, CsvTable.Rows(intRow1).Item("bunrui_cd").ToString, strUserId)
                                Next
                            End If
                        End If
                    Next
                Else
                    For intRow As Integer = 0 To grdItiran.Rows.Count - 1
                        If CType(grdItiran.Rows(intRow).FindControl("chkTaisyou"), Web.UI.WebControls.CheckBox).Checked = True Then

                            '連棟の場合
                            CsvTable = GetCsvDataRentou(grdItiran.Rows(intRow).Cells(1).Text.Trim + grdItiran.Rows(intRow).Cells(2).Text.Trim, grdItiran.Rows(intRow).Cells(5).Text.Trim, CInt(strCsvFlg), strMitumoriFlg, strSesyuMei, strKeiretuCd, strTS)

                            If CsvTable.Rows.Count > 0 Then

                                '物件履歴表処理↓
                                dtTable.Rows.Clear()
                                Dim drRow As DataRow
                                drRow = dtTable.NewRow
                                drRow.Item("kbn") = grdItiran.Rows(intRow).Cells(1).Text.Trim
                                drRow.Item("hosyousyo_no") = grdItiran.Rows(intRow).Cells(2).Text.Trim
                                drRow.Item("rireki_syubetu") = "27"
                                drRow.Item("rireki_no") = "1"
                                dtTable.Rows.Add(drRow)

                                '入力NO
                                Dim strNyuuryokuNo As String = "0"
                                dtBukkenRirekiNo = GetBukkenRirekiNo(dtTable.Rows(0))
                                If dtBukkenRirekiNo.Rows.Count > 0 Then
                                    strNyuuryokuNo = dtBukkenRirekiNo.Rows(0).Item("nyuuryoku_no").ToString
                                End If

                                dtTable.Rows.Clear()
                                drRow = dtTable.NewRow
                                drRow.Item("kbn") = grdItiran.Rows(intRow).Cells(1).Text.Trim
                                drRow.Item("hosyousyo_no") = grdItiran.Rows(intRow).Cells(2).Text.Trim
                                drRow.Item("rireki_syubetu") = "27"
                                drRow.Item("rireki_no") = "1"
                                drRow.Item("nyuuryoku_no") = (CInt(strNyuuryokuNo) + 1).ToString
                                drRow.Item("hanyou_cd") = "010"
                                drRow.Item("henkou_kahi_flg") = "1"
                                drRow.Item("torikesi") = "0"
                                drRow.Item("add_login_user_id") = strUserId
                                drRow.Item("upd_login_user_id") = strUserId
                                dtTable.Rows.Add(drRow)

                                If GetBukkenRirekiChk(dtTable.Rows(0)).Rows.Count = 0 Then
                                    '登録
                                    InsBukkenRireki(dtTable.Rows(0), False)
                                Else
                                    '更新
                                    InsBukkenRireki(dtTable.Rows(0), True)
                                End If
                                '物件履歴表処理↑

                                'CSVファイル内容設定
                                If bolFlg = False Then
                                    For intRow1 As Integer = 0 To CsvTable.Rows.Count - 1
                                        drCsvRow = dtCsvTable.NewRow
                                        drCsvRow = CsvTable.Rows(intRow1)
                                        dtCsvTable.Rows.Add(drCsvRow.ItemArray)
                                    Next
                                    bolFlg = True
                                End If

                                '邸別請求テーブルの更新
                                For intRow1 As Integer = 0 To CsvTable.Rows.Count - 1
                                    '邸別請求テーブルへ下記内容を書き込み
                                    TyousaMitumoriYouDataSyuturyokuDA.UpdTeibetuSeikyuu(grdItiran.Rows(intRow).Cells(1).Text.Trim, grdItiran.Rows(intRow).Cells(2).Text.Trim, CsvTable.Rows(intRow1).Item("bunrui_cd").ToString, strUserId)
                                Next
                            End If
                        End If
                    Next
                End If

                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using

    End Function
End Class
