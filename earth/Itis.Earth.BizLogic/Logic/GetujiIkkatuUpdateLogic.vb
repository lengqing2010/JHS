Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 月次データを一括更新するロジッククラスです。
''' </summary>
''' <remarks></remarks>
Public Class GetujiIkkatuUpdateLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'メッセージロジック
    Dim mLogic As New MessageLogic

#Region "定数"
    Private Const KAIRYOU_KOUJI As String = "130"
    Private Const TUIKA_KAIRYOU_KOUJI As String = "140"
    Private Const URI_KEIJYOU_FLG As Integer = 0
#End Region
#Region "列挙型"
    ''' <summary>
    ''' 更新対象日付
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumTgtDt
        ''' <summary>
        ''' 売上年月日
        ''' </summary>
        ''' <remarks></remarks>
        Uriage = 0
        ''' <summary>
        ''' 請求書発行日
        ''' </summary>
        ''' <remarks></remarks>
        Seikyuu = 1
    End Enum
    ''' <summary>
    ''' 更新対象テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumTgtTbl
        ''' <summary>
        ''' 邸別請求テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Teibetu = 0
        ''' <summary>
        ''' 店別初期請求テーブル
        ''' </summary>
        ''' <remarks></remarks>
        TenbetuSyoki = 1
        ''' <summary>
        ''' 店別請求テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Tenbetu = 2
    End Enum
#End Region
#Region "プロパティ"
#Region "画面情報の取得用Setterのみ"
    ''' <summary>
    ''' 売上年月日FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageFrom As String
    ''' <summary>
    ''' 売上年月日TO
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageTo As String
    ''' <summary>
    ''' 請求書発行日FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuFrom As String
    ''' <summary>
    ''' 請求書発行日TO
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuTo As String

    ''' <summary>
    ''' 画面から取得用 for 売上年月日FROM
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccUriageFrom() As String
        Set(ByVal value As String)
            strUriageFrom = value
        End Set
    End Property
    ''' <summary>
    ''' 画面から取得用 for 売上年月日TO
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccUriageTo() As String
        Set(ByVal value As String)
            strUriageTo = value
        End Set
    End Property
    ''' <summary>
    ''' 画面から取得用 for 請求書発行日FROM
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccSeikyuuFrom() As String
        Set(ByVal value As String)
            strSeikyuuFrom = value
        End Set
    End Property
    ''' <summary>
    ''' 画面から取得用 for 請求書発行日TO
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccSeikyuuTo() As String
        Set(ByVal value As String)
            strSeikyuuTo = value
        End Set
    End Property
#End Region
#Region "ログインユーザーID"
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
#End Region

#Region "月次処理"
    ''' <summary>
    ''' 月次処理を行います。
    ''' </summary>
    ''' <returns>合計処理件数</returns>
    ''' <remarks></remarks>
    Public Function GetujiSyori(ByVal sender As System.Object) As Integer
        Dim intResult As Integer = 0
        Dim strBunruiCd As String
        Dim intUriKeijoFlg As Integer
        Dim dtFrom As Date
        Dim dtTo As Date
        Dim enTgtDt As enumTgtDt

        Try
            '邸別請求テーブルと邸別請求連携管理テーブルの同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '邸別請求テーブルに対し、改良工事・追加改良工事の売上年月日・請求書発行日を一括修正する。                   
                '一括修正を行ったデータは連携テーブルへ反映する                 
                '   1. 改良工事データの一括修正
                '*** 1-1.改良工事データの売上年月日の変更
                '引数の設定
                strBunruiCd = KAIRYOU_KOUJI
                intUriKeijoFlg = URI_KEIJYOU_FLG
                dtFrom = DateTime.Parse(strUriageFrom)
                dtTo = DateTime.Parse(strUriageTo)
                enTgtDt = enumTgtDt.Uriage
                'データの更新
                intResult += GetujiUpdSyori(strBunruiCd, intUriKeijoFlg, dtFrom, dtTo, enTgtDt)

                '*** 1-2.改良工事データの請求所発行日の変更 
                '引数の設定
                strBunruiCd = KAIRYOU_KOUJI
                intUriKeijoFlg = URI_KEIJYOU_FLG
                dtFrom = DateTime.Parse(strSeikyuuFrom)
                dtTo = DateTime.Parse(strSeikyuuTo)
                enTgtDt = enumTgtDt.Seikyuu
                'データの更新
                intResult += GetujiUpdSyori(strBunruiCd, intUriKeijoFlg, dtFrom, dtTo, enTgtDt)

                '   2. 追加改良工事データの一括修正                
                '*** 2-1.追加改良工事データの売上年月日の変更               
                '引数の設定
                strBunruiCd = TUIKA_KAIRYOU_KOUJI
                intUriKeijoFlg = URI_KEIJYOU_FLG
                dtFrom = DateTime.Parse(strUriageFrom)
                dtTo = DateTime.Parse(strUriageTo)
                enTgtDt = enumTgtDt.Uriage
                'データの更新
                intResult += GetujiUpdSyori(strBunruiCd, intUriKeijoFlg, dtFrom, dtTo, enTgtDt)


                '*** 2-2.追加改良工事データの請求所発行日の変更             
                '引数の設定
                strBunruiCd = TUIKA_KAIRYOU_KOUJI
                intUriKeijoFlg = URI_KEIJYOU_FLG
                dtFrom = DateTime.Parse(strSeikyuuFrom)
                dtTo = DateTime.Parse(strSeikyuuTo)
                enTgtDt = enumTgtDt.Seikyuu
                'データの更新
                intResult += GetujiUpdSyori(strBunruiCd, intUriKeijoFlg, dtFrom, dtTo, enTgtDt)

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()
            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            intResult = Integer.MinValue
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            intResult = Integer.MinValue
        End Try

        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブルと邸別請求テーブル連携管理テーブルを一括更新します。
    ''' </summary>
    ''' <param name="strBunruiCD">分類コード</param>
    ''' <param name="intUriKeijoFlg">売上計上FLG</param>
    ''' <param name="dtFrom">画面日付From</param>
    ''' <param name="dtTo">画面日付To</param>
    ''' <param name="enTgtDt">更新対象判断列挙体（0：売上年月日／1：請求書発行日）</param>
    ''' <returns>合計処理件数（Update,Delete,Insert）</returns>
    ''' <remarks></remarks>
    Private Function GetujiUpdSyori(ByVal strBunruiCD As String _
                                , ByVal intUriKeijoFlg As Integer _
                                , ByVal dtFrom As Date _
                                , ByVal dtTo As Date _
                                , ByVal enTgtDt As enumTgtDt) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetujiUpdSyori" _
                                                    , strBunruiCD _
                                                    , intUriKeijoFlg _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , enTgtDt)
        Dim clsDataAcc As New GetujiIkkatuUpdateDataAccess
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        'Dim clsRenkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
        Dim intResult As Integer = 0

        Dim strInsetTmpSql As String
        Dim cmdParams() As SqlParameter

        '連携用テンポラリーテーブルの名前を指定
        Const P_TMP_TABLE_NAME As String = "#TEMP_TEIBETU_RENKEI_WORK"

        '更新対象を連携用テンポラリーテーブルに登録するSQLを作成
        strInsetTmpSql = clsDataAcc.GetInsertSqlRenkeiTmpForGetujiUpd(P_TMP_TABLE_NAME, strBunruiCD, enTgtDt)

        '更新対象絞込用SQLパラメータの作成
        cmdParams = clsDataAcc.GetRenkeiCmdParamsForGetujiUpd( _
                                                                EarthConst.enSqlTypeFlg.UPDATE, _
                                                                strBunruiCD, _
                                                                intUriKeijoFlg, _
                                                                dtFrom, _
                                                                dtTo)
        '邸別請求連携管理テーブルの一括更新
        intResult += clsRenkeiLogic.EditTeibetuSeikyuuRenkeiLump(strInsetTmpSql, cmdParams, P_TMP_TABLE_NAME, strLoginUserId)

        '邸別請求データの一括更新
        intResult += clsDataAcc.UpdTeibetuSeikyuuDataAll(strBunruiCD, intUriKeijoFlg, dtFrom, dtTo, strLoginUserId, enTgtDt)

        Return intResult
    End Function
#End Region

#Region "決算月処理"
    ''' <summary>
    ''' 決算月処理を行います。
    ''' </summary>
    ''' <returns>合計処理件数</returns>
    ''' <remarks></remarks>
    Public Function KessanSyori(ByVal sender As System.Object) As Integer
        Dim intResult As Integer = 0

        Try
            '各種テーブルと各種連携管理テーブルの同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '更新日時を取得
                Dim dtNowDtTime As DateTime = DateTime.Now

                '1.邸別請求、2.店別初期請求、3.店別請求テーブルに対し、請求書発行日の設定を行う                   
                '一括修正を行ったデータは連携テーブルへ反映する                 
                intResult += KessanUpdSyori(dtNowDtTime)

                '4. 汎用売上テーブルの一括修正
                intResult += KessanUpdSyoriHannyouUriage(dtNowDtTime)

                '5. 売上データテーブルの一括修正
                intResult += KessanUpdSyoriUriageData(dtNowDtTime)

                scope.Complete()
            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            intResult = Integer.MinValue
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            intResult = Integer.MinValue
        End Try

        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブル、店別初期請求テーブル、店別請求テーブルとそれぞれの連携管理テーブルを一括更新します。
    ''' </summary>
    ''' <param name="dtNowDtTime">更新日時</param>
    ''' <returns>合計処理件数（Update,Delete,Insert）</returns>
    ''' <remarks></remarks>
    Private Function KessanUpdSyori(ByVal dtNowDtTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".KessanUpdSyori", dtNowDtTime)

        Dim dataAccess As New GetujiIkkatuUpdateDataAccess
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        Dim clsRenkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
        Dim clsRenkeiTenbetuSyokiRec As New TenbetuSyokiSeikyuuRenkeiRecord
        Dim clsRenkeiTenbetuRec As New TenbetuSeikyuuRenkeiRecord
        Dim intResult As Integer
        Dim intRenkeiResult As Integer
        Dim dtUriFrom As Date
        Dim dtUriTo As Date
        Dim dtSeikyuuFrom As Date
        Dim dtSeikyuuTo As Date
        Dim strTgtTbl As String = ""
        Dim dtUpdDate As Date
        Dim renkeiDataAccess As New RenkeiKanriDataAccess

        '日付の設定
        Select Case Now.Month
            Case 9, 10
                dtUriFrom = DateTime.Parse(Now.Year & "/09/01")
                dtUriTo = DateTime.Parse(Now.Year & "/09/30")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/10/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/10/31")
                dtUpdDate = DateTime.Parse(Now.Year & "/09/30")
            Case 3, 4
                dtUriFrom = DateTime.Parse(Now.Year & "/03/01")
                dtUriTo = DateTime.Parse(Now.Year & "/03/31")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/04/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/04/30")
                dtUpdDate = DateTime.Parse(Now.Year & "/03/31")
        End Select

        '邸別請求データの更新0
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan0(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '邸別請求データの更新1
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan1(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '邸別請求データの更新2
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan2(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '邸別請求データの更新3
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan3(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '邸別請求データの更新4
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan4(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '邸別請求データの更新5
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan5(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '邸別請求連携管理テーブルにUPDATE/INSERT
        intRenkeiResult += renkeiDataAccess.setTeibetuSeikyuuRenkei(dtNowDtTime, LoginUserId)

        '店別初期請求データの更新
        intResult += dataAccess.UpdTenbetuSyokiSeikyuuDataForKessan(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '店別初期請求連携管理テーブルにUPDATE/INSERT
        intRenkeiResult += renkeiDataAccess.setTenbetuSyokiSeikyuuRenkei(dtNowDtTime, LoginUserId)

        '店別請求データの更新1
        intResult += dataAccess.UpdTenbetuSeikyuuDataForKessan1(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '店別請求データの更新2
        intResult += dataAccess.UpdTenbetuSeikyuuDataForKessan2(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '店別請求連携管理テーブルにUPDATE/INSERT
        intRenkeiResult += renkeiDataAccess.setTenbetuSeikyuuRenkei(dtNowDtTime, LoginUserId)

        Return intResult
    End Function

    ''' <summary>
    ''' 汎用売上データテーブルを一括更新します。
    ''' </summary>
    ''' <param name="dtNowDtTime">更新日時</param>
    ''' <returns></returns>
    ''' <remarks>処理件数</remarks>
    Private Function KessanUpdSyoriHannyouUriage(ByVal dtNowDtTime As DateTime) As Integer
        Dim intResult As Integer
        Dim dtUriFrom As Date
        Dim dtUriTo As Date
        Dim dtSeikyuuFrom As Date
        Dim dtSeikyuuTo As Date
        Dim dtUpdDate As Date
        Dim dtAcc As New GetujiIkkatuUpdateDataAccess

        '日付の設定
        Select Case Now.Month
            Case 9, 10
                dtUriFrom = DateTime.Parse(Now.Year & "/09/01")
                dtUriTo = DateTime.Parse(Now.Year & "/09/30")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/10/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/10/31")
                dtUpdDate = DateTime.Parse(Now.Year & "/09/30")
            Case 3, 4
                dtUriFrom = DateTime.Parse(Now.Year & "/03/01")
                dtUriTo = DateTime.Parse(Now.Year & "/03/31")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/04/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/04/30")
                dtUpdDate = DateTime.Parse(Now.Year & "/03/31")
        End Select

        '汎用売上データテーブルの一括更新
        intResult = dtAcc.UpdHannyouUriageDataForKessan(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)

        Return intResult
    End Function

    ''' <summary>
    ''' 売上データテーブルを一括更新します。
    ''' </summary>
    ''' <param name="dtNowDtTime">更新日時</param>
    ''' <returns></returns>
    ''' <remarks>処理件数</remarks>
    Private Function KessanUpdSyoriUriageData(ByVal dtNowDtTime As DateTime) As Integer
        Dim intResult As Integer
        Dim dtUriFrom As Date
        Dim dtUriTo As Date
        Dim dtSeikyuuFrom As Date
        Dim dtSeikyuuTo As Date
        Dim dtUpdDate As Date
        Dim dtAcc As New GetujiIkkatuUpdateDataAccess

        '日付の設定
        Select Case Now.Month
            Case 9, 10
                dtUriFrom = DateTime.Parse(Now.Year & "/09/01")
                dtUriTo = DateTime.Parse(Now.Year & "/09/30")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/10/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/10/31")
                dtUpdDate = DateTime.Parse(Now.Year & "/09/30")
            Case 3, 4
                dtUriFrom = DateTime.Parse(Now.Year & "/03/01")
                dtUriTo = DateTime.Parse(Now.Year & "/03/31")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/04/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/04/30")
                dtUpdDate = DateTime.Parse(Now.Year & "/03/31")
        End Select

        '売上データテーブルの一括更新
        intResult = dtAcc.UpdUriageDataForKessan(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)

        Return intResult
    End Function
#End Region

#Region "月次確定処理"
#Region "現在の月次確定予約管理テーブルから、処理状況を取得"
    ''' <summary>
    ''' 現在の月次確定予約管理テーブルから、処理状況を取得
    ''' </summary>
    ''' <param name="targetYM">処理年月(YYYY/MM/01)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGetujiKakuteiYoyakuData( _
                                               ByVal targetYM As Date _
                                               ) As Object
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetGetujiKakuteiYoyakuData", _
                                                    targetYM _
                                                    )

        Dim dataAccess As New GetujiIkkatuUpdateDataAccess
        Dim dbResult As Object

        'データ取得
        dbResult = dataAccess.searchGetujiKakuteiYoyakuData(targetYM)

        '戻り
        Return dbResult
    End Function
#End Region

#Region "月次確定処理予約登録・解除メイン"
    ''' <summary>
    ''' 月次確定処理予約登録・解除メイン
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="exeType">実行タイプ(1:予約、2:予約解除)</param>
    ''' <param name="targetYM">対象年月(YYYY/MM/末日)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EditGetujiKakuteiYoyaku(ByVal sender As System.Object, _
                                            ByVal exeType As Integer, _
                                            ByVal targetYM As Date) As Integer
        Dim intResult As Integer
        Dim nowSyoriJoukyou As Object

        Try

            '各種テーブルと各種連携管理テーブルの同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                nowSyoriJoukyou = GetGetujiKakuteiYoyakuData(targetYM)

                If exeType = 1 Then
                    '予約
                    If nowSyoriJoukyou Is Nothing Then
                        '新規登録
                        intResult = SetGetujiKakuteiYoyaku(targetYM, 1)

                    ElseIf nowSyoriJoukyou = 0 Then
                        '処理状況更新(予約)
                        intResult = UpdGetujiKakuteiYoyakuJoukyou(targetYM, 0, 1)

                    Else
                        intResult = Integer.MinValue

                    End If

                ElseIf exeType = 2 Then
                    '予約解除
                    If nowSyoriJoukyou IsNot Nothing AndAlso nowSyoriJoukyou = 1 Then
                        '処理状況更新(解除)
                        intResult = UpdGetujiKakuteiYoyakuJoukyou(targetYM, 1, 0)

                    Else
                        intResult = Integer.MinValue

                    End If

                End If

                'トランザクションスコープの確定(コミット)
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            intResult = Integer.MinValue
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            intResult = Integer.MinValue
        End Try

        '結果戻し
        Return intResult

    End Function


#End Region

#Region "月次確定処理予約登録"
    ''' <summary>
    ''' 月次確定処理予約登録
    ''' </summary>
    ''' <param name="targetYM">対象年月(YYYY/MM/末日)</param>
    ''' <param name="joukyou">セットする処理状況</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetGetujiKakuteiYoyaku(ByVal targetYM As Date, _
                                           ByVal joukyou As Integer _
                                           ) As Integer
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetGetujiKakuteiYoyakuData", _
                                                    targetYM _
                                                    )

        Dim dataAccess As New GetujiIkkatuUpdateDataAccess
        Dim dbResult As Integer

        'データ取得
        dbResult = dataAccess.insertGetujiKakuteiYoyaku(targetYM, _
                                                        joukyou, _
                                                        LoginUserId)

        '戻り
        Return dbResult
    End Function
#End Region

#Region "月次確定処理予約状況更新"
    ''' <summary>
    ''' 月次確定処理予約状況更新
    ''' </summary>
    ''' <param name="targetYM">対象年月(YYYY/MM/末日)</param>
    ''' <param name="joukyouFrom">変更前処理状況</param>
    ''' <param name="joukyouTo">変更後処理状況</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdGetujiKakuteiYoyakuJoukyou(ByVal targetYM As Date, _
                                                  ByVal joukyouFrom As Integer, _
                                                  ByVal joukyouTo As Integer _
                                                  ) As Integer
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetGetujiKakuteiYoyakuData", _
                                                    targetYM _
                                                    )

        Dim dataAccess As New GetujiIkkatuUpdateDataAccess
        Dim dbResult As Integer

        'データ取得
        dbResult = dataAccess.updateGetujiKakuteiYoyakuJoukyou(targetYM, _
                                                               joukyouFrom, _
                                                               joukyouTo, _
                                                               LoginUserId)

        '戻り
        Return dbResult
    End Function
#End Region

#Region "直近の月次確定処理年月日を取得する"
    ''' <summary>
    ''' 直近の月次確定処理年月日を取得する
    ''' </summary>
    ''' <returns>直近の月次確定処理年月日</returns>
    ''' <remarks></remarks>
    Public Function getGetujiKakuteiLastSyoriDate() As Date
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getGetujiKakuteiLastSyoriDate")

        Dim dtAcc As New GetujiIkkatuUpdateDataAccess
        Dim dbResult As Object

        'データ取得
        dbResult = dtAcc.getGetujiKakuteiLastSyoriDate()

        If dbResult Is Nothing Then
            Return Date.MinValue
        Else
            Return Date.Parse(dbResult)
        End If


    End Function
#End Region

#End Region

End Class
