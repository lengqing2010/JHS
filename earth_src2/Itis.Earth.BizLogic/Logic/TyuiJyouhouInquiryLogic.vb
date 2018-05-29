Imports Itis.Earth.DataAccess
Imports System.Transactions
Public Class TyuiJyouhouInquiryLogic
    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private TyuiJyouhouDataSet As New TyuiJyouhouInquiryDataAccess

    ''' <summary>優先注意事項を取得する</summary>
    Public Function GetYuusenTyuuiJikouInfo(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        Return TyuiJyouhouDataSet.SelYuusenTyuuiJikouInfo(strKameitenCd, strUerId)
    End Function
    ''' <summary>通常注意事項を取得する</summary>
    Public Function GetTuujyouTyuuiJikouInfo(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        Return TyuiJyouhouDataSet.SelTuujyouTyuuiJikouInfo(strKameitenCd, strUerId)
    End Function
    ''' <summary>種別を取得する</summary>
    Public Function GetSyubetuInfo(ByVal flg As String) As TyuiJyouhouDataSet.MeisyouTableDataTable
        Return TyuiJyouhouDataSet.SelSyubetuInfo(flg)
    End Function
    ''' <summary>調査会社情報を取得する</summary>
    Public Function GetKaisyaJyouhouInfo(ByVal strKameitenCd As String, ByVal strKbn As String) As TyuiJyouhouDataSet.KaisyaTableDataTable
        Return TyuiJyouhouDataSet.SelKaisyaJyouhouInfo(strKameitenCd, strKbn)
    End Function
    ''' <summary>会社を取得する</summary>
    Public Function GetKaisyaInfo(Optional ByVal strKaisyaCd As String = "") As TyuiJyouhouDataSet.KaisyaTableDataTable
        Return TyuiJyouhouDataSet.SelKaisyaInfo(strKaisyaCd)
    End Function
    ''' <summary>注意事項の更新処理</summary>
    Public Function SetUpdTyuuiJikou(ByVal dtTyuiJyouhouData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable, ByVal strRowTime As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyuuiJikouHaita(dtTyuiJyouhouData)
            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else

                    TyuiJyouhouDataSet.UpdTyuuiJikou(dtTyuiJyouhouData)

                    TyuiJyouhouDataSet.UpdTyuuiJikouRenkei(dtTyuiJyouhouData, "2")

                    scope.Complete()
                    Return ""
                End If
            End If

        End Using

    End Function
    ''' <summary>注意事項の新規処理</summary>
    Public Function SetInsTyuuiJikou(ByVal dtTyuiJyouhouData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            TyuiJyouhouDataSet.InsTyuuiJikou(dtTyuiJyouhouData)
            If TyuiJyouhouDataSet.SelTyuuiJikouCount(dtTyuiJyouhouData) = 0 Then
                TyuiJyouhouDataSet.InsTyuuiJikouRenkei(dtTyuiJyouhouData)
            Else
                TyuiJyouhouDataSet.UpdTyuuiJikouRenkei(dtTyuiJyouhouData, "1")
            End If

            scope.Complete()
        End Using

        Return ""
    End Function
    ''' <summary>注意事項の削除処理</summary>
    Public Function SetDelTyuuiJikou(ByVal strKameitenCd As String, ByVal strNyuuryokuNo As String, ByVal strRowTime As String, ByVal strUserId As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtTyuiJyouhouData As New TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable
            dtTyuiJyouhouData.Rows.Add(dtTyuiJyouhouData.NewRow)
            dtTyuiJyouhouData.Rows(0).Item("kameiten_cd") = strKameitenCd
            dtTyuiJyouhouData.Rows(0).Item("nyuuryoku_no") = strNyuuryokuNo
            dtTyuiJyouhouData.Rows(0).Item("kousinsya") = strUserId
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyuuiJikouHaita(dtTyuiJyouhouData)
            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    TyuiJyouhouDataSet.InsTyuuiJikouRenkei2(dtTyuiJyouhouData, strNyuuryokuNo)
                    'TyuiJyouhouDataSet.DelTyuuiJikou(strKameitenCd, strNyuuryokuNo, strUserId)

                    'TyuiJyouhouDataSet.UpdTyuuiJikouRenkei(dtTyuiJyouhouData, "9")

                    scope.Complete()
                    Return ""
                End If
            End If
        End Using

    End Function
    ''' <summary>調査会社の更新処理</summary>
    Public Function SetUpdTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, ByVal strRowTime As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyousaKaisyaHaita(dtTyousaKaisyaUPDData)
            If dtReturn.Rows.Count = 1 And (Split(dtTyousaKaisyaUPDData.Rows(0).Item("tys_kaisya_cd"), ":")(0) & Split(dtTyousaKaisyaUPDData.Rows(0).Item("jigyousyo_cd"), ":")(0)) <> (Split(dtTyousaKaisyaUPDData.Rows(0).Item("tys_kaisya_cd"), ":")(1) & Split(dtTyousaKaisyaUPDData.Rows(0).Item("jigyousyo_cd"), ":")(1)) Then
                scope.Dispose()
                Return "E"
            Else
                dtReturn = TyuiJyouhouDataSet.SelTyousaKaisyaHaita(dtTyousaKaisyaUPDData, True)
                If dtReturn.Rows.Count = 0 Then
                    scope.Dispose()
                    Return "H"
                End If
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    'TyuiJyouhouDataSet.InsTyousaKaisyaRenkei2(dtTyousaKaisyaUPDData)
                    TyuiJyouhouDataSet.UpdTyousaKaisya(dtTyousaKaisyaUPDData)
                    TyuiJyouhouDataSet.UpdTyousaKaisyaRenkei(dtTyousaKaisyaUPDData, "2")
                    scope.Complete()
                    Return ""
                End If
            End If

        End Using
    End Function
    ''' <summary>調査会社の削除処理</summary>
    Public Function SetDelTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, ByVal strRowTime As String, ByVal intRow As Integer) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyousaKaisyaHaita(dtTyousaKaisyaUPDData, True)
            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    'TyuiJyouhouDataSet.InsTyousaKaisyaRenkei2(dtTyousaKaisyaUPDData)
                    TyuiJyouhouDataSet.DelTyousaKaisya(dtTyousaKaisyaUPDData, intRow)
                    TyuiJyouhouDataSet.UpdTyousaKaisyaRenkei(dtTyousaKaisyaUPDData, "9", intRow)
                    scope.Complete()
                    Return ""
                End If
            End If

        End Using
    End Function
    ''' <summary>基礎仕様を取得する</summary>
    Public Function GetKisoSiyouInfo(Optional ByVal strKsno As String = "") As TyuiJyouhouDataSet.KisoSiyouTableDataTable
        Return TyuiJyouhouDataSet.SelKisoSiyouInfo(strKsno)
    End Function
    '''<summary>基礎仕様設定を取得する</summary>
    Public Function GetKisoSiyouSetteiInfo(ByVal strKameitenCd As String, ByVal strKahiKbn As String) As TyuiJyouhouDataSet.KisoSiyouTableDataTable
        Return TyuiJyouhouDataSet.SelKisoSiyouSetteiInfo(strKameitenCd, strKahiKbn)
    End Function
    '''<summary>基礎仕様設定の新規処理</summary>
    Public Function SetInsKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strKousinsya As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelKisoSiyouHaita(strKameitenCd, strKsSiyouNo)
            If dtReturn.Rows.Count > 0 Then
                scope.Dispose()
                Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
            Else
                TyuiJyouhouDataSet.InsKisoSiyouSettei(strKameitenCd, strKahiKbn, strKsSiyouNo, strKousinsya)
                If TyuiJyouhouDataSet.SelKisoSiyouSetteiCount(strKameitenCd, strKsSiyouNo) = 0 Then
                    TyuiJyouhouDataSet.InsKisoSiyouSetteiRenkei(strKameitenCd, strKsSiyouNo, strKousinsya)
                Else
                    TyuiJyouhouDataSet.UpdKisoSiyouSetteiRenkei(strKameitenCd, strKsSiyouNo, strKousinsya, "1")
                End If
                'TyuiJyouhouDataSet.InsKisoSiyouSettei2(strKameitenCd, strKousinsya)
                scope.Complete()
                Return ""
            End If
        End Using

    End Function
    '''<summary>調査会社の新規処理</summary>
    Public Function SetInsTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelTyousaKaisyaHaita(dtTyousaKaisyaUPDData)
            If dtReturn.Rows.Count > 0 Then
                scope.Dispose()
                Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
            Else

                TyuiJyouhouDataSet.InsTyousaKaisya(dtTyousaKaisyaUPDData)
                If TyuiJyouhouDataSet.SelTyousaKaisyaCount(dtTyousaKaisyaUPDData) = 0 Then
                    TyuiJyouhouDataSet.InsTyousaKaisyaRenkei(dtTyousaKaisyaUPDData)
                Else
                    TyuiJyouhouDataSet.UpdTyousaKaisyaRenkei(dtTyousaKaisyaUPDData, "1")
                End If
                'TyuiJyouhouDataSet.InsTyousaKaisyaRenkei2(dtTyousaKaisyaUPDData)
                scope.Complete()
                Return ""
            End If
        End Using

    End Function
    '''<summary>基礎仕様設定の更新処理</summary>
    Public Function SetUpdKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strUerId As String, ByVal strRowTime As String) As String

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelKisoSiyouHaita(strKameitenCd, Split(strKsSiyouNo, ":")(0))
            If dtReturn.Rows.Count = 1 And (Split(strKsSiyouNo, ":")(1) <> Split(strKsSiyouNo, ":")(0)) Then
                scope.Dispose()
                Return "E"
            Else
                dtReturn = TyuiJyouhouDataSet.SelKisoSiyouHaita(strKameitenCd, Split(strKsSiyouNo, ":")(1))
                If dtReturn.Rows.Count = 0 Then
                    scope.Dispose()
                    Return "H"
                End If
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    'TyuiJyouhouDataSet.InsKisoSiyouSettei2(strKameitenCd, strUerId)
                    TyuiJyouhouDataSet.UpdKisoSiyouSettei(strKameitenCd, strKahiKbn, strKsSiyouNo, strUerId)
                    TyuiJyouhouDataSet.UpdKisoSiyouSetteiRenkei(strKameitenCd, strKsSiyouNo, strUerId, "2")
                    scope.Complete()
                    Return ""
                End If

            End If

        End Using
    End Function
    '''<summary>基礎仕様設定の削除処理</summary>
    Public Function SetDelKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strRowTime As String, ByVal strUserId As String, ByVal intRow As Integer) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)

            Dim dtReturn As DataTable = TyuiJyouhouDataSet.SelKisoSiyouHaita(strKameitenCd, strKsSiyouNo)
            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strRowTime < dtReturn.Rows(0).Item(1).ToString Then
                    scope.Dispose()
                    Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                Else
                    'TyuiJyouhouDataSet.InsKisoSiyouSettei2(strKameitenCd, strUserId)
                    TyuiJyouhouDataSet.DelKisoSiyouSettei(strKameitenCd, strKsSiyouNo, strKahiKbn, intRow)
                    TyuiJyouhouDataSet.UpdKisoSiyouSetteiRenkei(strKameitenCd, strKsSiyouNo, strUserId, "9", intRow, strKahiKbn)
                    scope.Complete()
                    Return ""
                End If

            End If

        End Using
    End Function

    ''' <summary>
    ''' 「取消」情報を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>「取消」データ</returns>
    ''' <hidtory>2012/03/28 車龍 405721案件の対応 追加</hidtory>
    Public Function GetTorikesi(ByVal strKameitenCd As String) As Data.DataTable

        '戻り値
        Return TyuiJyouhouDataSet.SelTorikesi(strKameitenCd)

    End Function

    ''' <summary>
    ''' 「工事売上種別」情報を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>「取消」データ</returns>
    ''' <hidtory>2012/05/15 車龍 407553の対応 追加</hidtory>
    Public Function GetKoujiUriageSyuubetu(ByVal strKameitenCd As String) As Data.DataTable

        '戻り値
        Return TyuiJyouhouDataSet.SelKoujiUriageSyuubetu(strKameitenCd)

    End Function

    '========2015/02/04 王莎莎 407679の対応 追加↓======================
    ''' <summary>名称種別＝33の名称を取得する</summary>
    Public Function GetSyubetuInfo33() As Data.DataTable

        '戻り値
        Return TyuiJyouhouDataSet.SelSyubetuInfo33()

    End Function

    ''' <summary>トラブル・クレーム情報を取得する</summary>
    Public Function GetTuujyouTyuuiJikouInfoTORA(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable

        '戻り値
        Return TyuiJyouhouDataSet.SelTuujyouTyuuiJikouInfoTORA(strKameitenCd, strUerId)

    End Function

    '========2015/02/04 王莎莎 407679の対応 追加↑======================

    ''' <summary>商品コードを取得する</summary>
    ''' <returns>商品コードデータテーブル</returns>
    ''' <history>2016/05/04　chel1　新規作成</history>
    Public Function GetSyouhinCd() As Data.DataTable

        '戻り値
        Return TyuiJyouhouDataSet.SelSyouhinCd()

    End Function

    ''' <summary>調査方法を取得する</summary>
    ''' <returns>調査方法データテーブル</returns>
    ''' <history>2016/05/04　chel1　新規作成</history>
    Public Function GetTyousaHouhou() As Data.DataTable

        '戻り値
        Return TyuiJyouhouDataSet.SelTyousaHouhou()

    End Function


    '''<summary>基本商品の更新処理</summary>
    Public Function SetKihonSyouhin(ByVal strKameitenCd As String, ByVal strKihonSyouhinCd As String, ByVal strKihonSyouhinTyuuibun As String) As Boolean

        '戻り値
        Return TyuiJyouhouDataSet.UpdKihonSyouhin(strKameitenCd, strKihonSyouhinCd, strKihonSyouhinTyuuibun)

    End Function

    '''<summary>基本調査方法の更新処理</summary>
    Public Function SetKihonTyousaHouhou(ByVal strKameitenCd As String, ByVal strKihonTyousaHouhouNo As String, ByVal strKihonTyousaHouhouTyuuibun As String) As Boolean

        '戻り値
        Return TyuiJyouhouDataSet.UpdKihonTyousaHouhou(strKameitenCd, strKihonTyousaHouhouNo, strKihonTyousaHouhouTyuuibun)

    End Function

    ''' <summary>
    ''' 基本商品と調査方法を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <history>2016/05/04　chel1　新規作成</history>
    Public Function GetKihouSyouhinAndTyousaHouhou(ByVal strKameitenCd As String) As Data.DataTable

        '戻り値
        Return TyuiJyouhouDataSet.SelKihouSyouhinAndTyousaHouhou(strKameitenCd)

    End Function

End Class
