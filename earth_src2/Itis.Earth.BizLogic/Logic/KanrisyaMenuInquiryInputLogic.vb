Imports Itis.Earth.DataAccess
Imports System.Transactions

''' <summary>ユーザー管理情報を照会登録する</summary>
''' <remarks>ユーザー管理情報を照会登録機能を提供する</remarks>
''' <history>
''' <para>2009/07/17　高雅娟(大連情報システム部)　新規作成</para>
''' </history>
Public Class KanrisyaMenuInquiryInputLogic

    ''' <summary> ユーザー管理照会登録クラスのインスタンス生成 </summary>
    Private KanrisyaMenuInquiryInputDataAccess As New KanrisyaMenuInquiryInputDataAccess

    ''' <summary>
    ''' 業務区分を取得する。
    ''' </summary>
    ''' <returns>業務区分テーブル</returns>
    Public Function GetGyoumuKubunInfo() As KanrisyaMenuInquiryInputDataSet.gyoumuKubunDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selGyoumuKubun
    End Function

    ''' <summary>
    ''' ログインユーザーの参照権限管理者FLGを取得する。
    ''' </summary>
    ''' <param name="strUserId">ログインユーザーID</param>
    ''' <returns>参照権限管理者FLGデータテーブル</returns>
    Public Function GetUserKengenKanriFlg(ByVal strUserId As String) As KanrisyaMenuInquiryInputDataSet.userKengenKanriFlgDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selUserKengenKanriFlg(strUserId)
    End Function

    ''' <summary>
    ''' 所属部署変更日を取得する。
    ''' </summary>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <returns>所属部署変更日テーブル</returns>
    Public Function GetSyozokuHenkouDateInfo(ByVal strUserId As String) As KanrisyaMenuInquiryInputDataSet.syozokuHenkouDateDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selSyozokuHenkouDate(strUserId)
    End Function

    ''' <summary>
    ''' ユーザーの権限情報を取得する。
    ''' </summary>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strSyozokuHenkouDate">ユーザーの所属部署変更日</param>
    ''' <returns>ユーザーの権限情報テーブル</returns>
    Public Function GetUserInfo(ByVal strUserId As String, ByVal strSyozokuHenkouDate As String) As KanrisyaMenuInquiryInputDataSet.kanrisyaJyouhouDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selUserInfo(strUserId, strSyozokuHenkouDate)
    End Function

    ''' <summary>
    ''' 追加参照部署コードによって取得した組織レベル
    ''' </summary>
    ''' <param name="strBusyoCd">追加参照部署コード</param>
    ''' <returns>組織レベルデータテーブル</returns>
    Public Function GetLevelInfo(ByVal strBusyoCd As String) As KanrisyaMenuInquiryInputDataSet.sansyouLevelDataTable
        Return KanrisyaMenuInquiryInputDataAccess.selLevel(strBusyoCd)
    End Function

    ''' <summary>
    ''' 地盤認証マスタと部署管理マスタを更新する。
    ''' </summary>
    ''' <param name="dtUPDData">更新項目テーブル</param>
    ''' <returns>文字列</returns>
    Public Function SetUpdJibanNinsyou(ByVal account_no As String, ByVal dtUPDData As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable, ByVal strHaita As String) As String
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Dim dtReturn As DataTable
            Dim dtReturnNinsyouRenkei As DataTable

            '登録したユーザーが地盤認証マスタに存在チェック
            dtReturn = KanrisyaMenuInquiryInputDataAccess.selJibanNinsyouHaita(dtUPDData.Rows(0).Item("user_id").ToString)

            '登録したユーザーが地盤認証マスタ連携管理テーブルに存在チェック
            dtReturnNinsyouRenkei = KanrisyaMenuInquiryInputDataAccess.selJibanNinsyouRenkeiHaita(dtUPDData.Rows(0).Item("user_id").ToString)

            If dtReturn.Rows.Count = 0 Then
                scope.Dispose()
                Return "H"
            Else
                If strHaita <> "" Then
                    If strHaita < dtReturn.Rows(0).Item("upd_datetime") Then
                        scope.Dispose()
                        Return dtReturn.Rows(0).Item(0) & "," & dtReturn.Rows(0).Item(1)
                    End If
                End If
                If dtReturnNinsyouRenkei.Rows.Count = 0 Then
                    '地盤認証マスタ連携管理テーブルを登録する。
                    If KanrisyaMenuInquiryInputDataAccess.InsJibanNinsyouRenkei(dtUPDData) = True Then
                        '地盤認証マスタを更新する。
                        If KanrisyaMenuInquiryInputDataAccess.UpdJibanNinsyou(account_no, dtUPDData) = True Then
                            scope.Complete()
                            Return "1"
                        Else
                            scope.Dispose()
                            Return "H"
                        End If
                    Else
                        scope.Dispose()
                        Return "H"
                    End If
                Else
                    '地盤認証マスタ連携管理テーブルを更新する。
                    If KanrisyaMenuInquiryInputDataAccess.UpdJibanNinsyouRenkei(dtUPDData) = True Then
                        '地盤認証マスタを更新する。
                        If KanrisyaMenuInquiryInputDataAccess.UpdJibanNinsyou(account_no, dtUPDData) = True Then
                            scope.Complete()
                            Return "1"
                        Else
                            scope.Dispose()
                            Return "H"
                        End If
                    Else
                        scope.Dispose()
                        Return "H"
                    End If
                End If
            End If

        End Using

    End Function

End Class
