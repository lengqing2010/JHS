Imports System.text
Imports System.Transactions
Imports itis.Earth.Utilities.StringLogic

Public Class SeikyuusyoDataSakuseiLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    'メッセージロジック
    Private mLogic As New MessageLogic
    Private sLogic As New StringLogic

#Region "プロパティ"
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoHakDate As String

    Public WriteOnly Property AccSeikyuusyoHakDate() As String
        Set(ByVal value As String)
            strSeikyuusyoHakDate = value
        End Set
    End Property

    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> ログインユーザーID</returns>
    ''' <remarks></remarks>
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' 請求書発行対象の請求年月日最小締め日を取得する
    ''' </summary>
    ''' <param name="listSsi">指定請求先リスト</param>
    ''' <param name="strResultDate">最小請求年月日</param>
    ''' <param name="flgResult">チェックボックス有無</param>
    ''' <remarks></remarks>
    Public Sub getMinSeikyuuSimeDate(ByVal listSsi As List(Of SeikyuuSakiInfoRecord), ByRef strResultDate As String, ByRef flgResult As Integer, ByRef intErrFlg As EarthEnum.emHidukeSyutokuErr)

        Dim minSimeDate As Object = Nothing
        Dim dtAcc As New SeikyuusyoDataSakuseiDataAccess

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '請求書発行対象の請求年月日最小締め日を取得する
                minSimeDate = dtAcc.getMinSeikyuuSimeDate(Date.MaxValue.ToString(EarthConst.FORMAT_DATE_TIME_9), listSsi)

                'トランザクションスコープ コンプリート
                scope.Complete()

            End Using

            '請求書発行日取得チェック
            If minSimeDate Is DBNull.Value OrElse minSimeDate.Length < 9 Then
                intErrFlg = EarthEnum.emHidukeSyutokuErr.SyutokuErr
            Else
                '請求年月日と全対象FLGを切り分ける
                strResultDate = minSimeDate.substring(0, 4) + "/" + minSimeDate.substring(4, 2) + "/" + minSimeDate.substring(6, 2)
                flgResult = CType(minSimeDate, String).Substring(8, 1)
            End If

        Catch sqlTimeOut As System.Data.SqlClient.SqlException
            If sqlTimeOut.ErrorCode = -2146232060 _
            AndAlso sqlTimeOut.State = 0 _
            AndAlso sqlTimeOut.Number = -2 _
            AndAlso sqlTimeOut.Message.Contains(EarthConst.TIMEOUT_KEYWORD) = True Then
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                intErrFlg = EarthEnum.emHidukeSyutokuErr.SqlErr
                Exit Sub
            Else
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                intErrFlg = EarthEnum.emHidukeSyutokuErr.SqlErr
                Exit Sub
            End If
        Catch tranTimeOut As System.Transactions.TransactionException
            UnTrappedExceptionManager.Publish(tranTimeOut)
            intErrFlg = EarthEnum.emHidukeSyutokuErr.SqlErr
            Exit Sub
        Catch ex As Exception
            UnTrappedExceptionManager.Publish(ex)
            intErrFlg = EarthEnum.emHidukeSyutokuErr.SqlErr
            Exit Sub

        Finally

            '日付チェック
            If strResultDate IsNot Nothing AndAlso IsDate(strResultDate) Then
            Else
                '取得できなかった場合、当日日付を戻す
                strResultDate = Date.Now.ToShortDateString.ToString
                '取得できなかった場合、チェック未設定を戻す
                flgResult = "0"
                '日付形式エラーフラグ
                If intErrFlg = EarthEnum.emHidukeSyutokuErr.OK Then
                    intErrFlg = EarthEnum.emHidukeSyutokuErr.HidukeErr
                End If
            End If

        End Try

    End Sub

    ''' <summary>
    ''' 請求書データ作成処理
    ''' </summary>
    ''' <returns>完了メッセージ</returns>
    ''' <remarks></remarks>
    Public Function SeikyuusyoDataSakuseiSyori(ByVal strSeikyuusyoHakDate As String, ByVal blnAllSakuseiFlg As Boolean, ByVal listSsi As List(Of SeikyuuSakiInfoRecord)) As String
        Dim strResultMsg As String = String.Empty
        Dim listResult As New List(Of Integer)
        Dim intKagamiSetCount As Integer = 0    '請求鑑セット件数取得用
        Dim intRirekiSetCount As Integer = 0    '請求締め日履歴セット件数取得用
        Dim dtAcc As New SeikyuusyoDataSakuseiDataAccess

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '請求鑑/請求明細への登録処理
                dtAcc.createKagamiMeisai(strSeikyuusyoHakDate, blnAllSakuseiFlg, strLoginUserId, listSsi, listResult, intKagamiSetCount, intRirekiSetCount)

                '請求鑑・締め日履歴登録件数チェック
                If intKagamiSetCount < 1 And intRirekiSetCount < 1 Then
                    '請求鑑・締め日履歴登録件数が1未満の場合、該当データ無し のメッセージを戻す
                    Return Messages.MSG020E
                    Exit Function
                End If

                'トランザクションスコープ コンプリート
                scope.Complete()
            End Using

        Catch sqlTimeOut As System.Data.SqlClient.SqlException
            If sqlTimeOut.ErrorCode = -2146232060 _
            AndAlso sqlTimeOut.State = 0 _
            AndAlso sqlTimeOut.Number = -2 _
            AndAlso sqlTimeOut.Message.Contains(EarthConst.TIMEOUT_KEYWORD) = True Then
                strResultMsg = Messages.MSG116E & sLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.AddMethodEntrance("CatchEx", sLogic.listToString(listResult))
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strResultMsg
                Exit Function
            Else
                strResultMsg = Messages.MSG118E & sLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.AddMethodEntrance("CatchEx", sLogic.listToString(listResult))
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strResultMsg
                Exit Function
            End If
        Catch tranTimeOut As System.Transactions.TransactionException
            strResultMsg = Messages.MSG117E & sLogic.RemoveSpecStr(tranTimeOut.Message)
            UnTrappedExceptionManager.AddMethodEntrance("CatchEx", sLogic.listToString(listResult))
            UnTrappedExceptionManager.Publish(tranTimeOut)
            Return strResultMsg
            Exit Function
        Catch ex As Exception
            strResultMsg = Messages.MSG118E & sLogic.RemoveSpecStr(ex.Message)
            UnTrappedExceptionManager.AddMethodEntrance("CatchEx", sLogic.listToString(listResult))
            UnTrappedExceptionManager.Publish(ex)
            Return strResultMsg
            Exit Function
        End Try

        '処理完了メッセージを戻す
        strResultMsg = Messages.MSG018S.Replace("@PARAM1", "請求書データ作成") & intKagamiSetCount.ToString & "件の請求鑑データを登録しました。\r\n"
        strResultMsg += Messages.MSG018S.Replace("@PARAM1", "締め日履歴登録") & intRirekiSetCount.ToString & "件の締め日履歴データを登録しました。\r\n"
        Return strResultMsg

    End Function

End Class
