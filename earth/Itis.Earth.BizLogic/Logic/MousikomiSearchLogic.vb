Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.text

Public Class MousikomiSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    'UtilitiesのStringLogicクラス
    Dim sLogic As New StringLogic

    Dim jLogic As New JibanLogic
    Dim miLogic As New MousikomiInputLogic
    Dim cbLogic As New CommonBizLogic
    Dim kLogic As New KameitenSearchLogic

    '更新日時保持用
    Dim pUpdDateTime As DateTime

    'エラーメッセージ格納変数
    Dim pErrMess As String = String.Empty

    '価格設定場所
    Dim pIntKakakuSetteiBasyo As Integer = Integer.MinValue
    '系列コード
    Dim pStrKeiretuCd As String = String.Empty
    '営業所コード
    Dim pStrEigyousyoCd As String = String.Empty
    '調査請求先
    Dim pStrTysSeikyuuSaki As String = String.Empty

    Private Const pStrInfoMousikomiNo As String = "[申込NO]"
    Private Const pStrInfoMousikomiContents As String = "【エラー内容】"
    Private Const pStrInfoMousikomiDetail As String = "【エラー詳細】"

#Region "申込データ取得"
    ''' <summary>
    ''' 検索画面用申込データを取得します
    ''' </summary>
    ''' <param name="keyRec">申込データKeyレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>申込データ検索用レコードのList(Of MousikomiRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetMousikomiDataInfo(ByVal sender As Object, _
                                         ByVal keyRec As MousikomiKeyRecord, _
                                         ByVal startRow As Integer, _
                                         ByVal endRow As Integer, _
                                         ByRef allCount As Integer _
                                        ) As List(Of MousikomiRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMousikomiDataInfo", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount _
                                            )

        '検索実行クラス
        Dim dataAccess As New MousikomiDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of MousikomiRecord)

        Try
            '検索処理の実行()
            Dim table As DataTable = dataAccess.GetSearchMousikomiIf(keyRec)

            '総件数をセット()
            allCount = table.Rows.Count

            '検索結果を格納用リストにセット()
            list = DataMappingHelper.Instance.getMapArray(Of MousikomiRecord)(GetType(MousikomiRecord), table, startRow, endRow)

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            '総件数カウントに-1をセット
            allCount = -1
        End Try

        Return list

    End Function

    ''' <summary>
    ''' 該当テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strMousikomiNo">請求書NO</param>
    ''' <returns>DB更新用レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>PKで該当テーブルの1レコードを取得</remarks>
    Public Function GetMousikomiDataRec(ByVal sender As Object _
                                                , ByVal strMousikomiNo As String _
                                                ) As MousikomiRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMousikomiDataRec", sender, strMousikomiNo)

        'データアクセスクラス
        Dim clsDataAcc As New MousikomiDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New MousikomiRecord

        If strMousikomiNo = String.Empty Then
            Return recResult
        End If

        dTblResult = clsDataAcc.GetMousikomiIfRec(strMousikomiNo)

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of MousikomiRecord)(GetType(MousikomiRecord), dTblResult)(0)
        End If
        Return recResult
    End Function
#End Region

#Region "申込データ更新"

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="listData">申込データのリスト</param>
    ''' <returns>処理成否(Boolean)</returns>
    ''' <remarks>ステータスの値で、処理を分岐する</remarks>
    Public Function saveData(ByVal sender As Object, _
                             ByRef listData As List(Of MousikomiRecord) _
                             ) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveData", _
                                                    sender, _
                                                    listData)

        '排他用データアクセスクラス
        Dim clsMousikomiDataAcc As New MousikomiDataAccess
        Dim recResult As New MousikomiRecord 'レコードクラス
        Dim recTmp As New MousikomiRecord '作業用
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper = New UpdateStringHelper '更新のみ
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)

        Dim recType As Type = Nothing
        'レコード更新タイプを設定()
        For Each recTmp In listData
            If recTmp.Status = EarthConst.MOUSIKOMI_STATUS_MI_JUTYUU Then '未受注時
                recType = GetType(MousikomiSyuuseiMijytyRecord)
            ElseIf recTmp.Status = EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU Then '受注済時
                recType = GetType(MousikomiSyuuseiJytyzumiRecord)
            ElseIf recTmp.Status = EarthConst.MOUSIKOMI_STATUS_HORYUU_JUTYUU Then '受注保留時
                recType = GetType(MousikomiSyuuseiJytyHoryuuRecord)
            Else
                Return False
                Exit Function
            End If
        Next

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 更新日時取得（システム日時）
                pUpdDateTime = DateTime.Now

                For Each recTmp In listData

                    If recTmp.MousikomiNo <> Long.MinValue Then 'UPDATE

                        '更新対象のレコードを取得
                        recResult = Me.GetMousikomiDataRec(sender, recTmp.MousikomiNo)

                        '●排他チェック
                        If recResult.UpdDatetime <> recTmp.UpdDatetime Then
                            ' 排他チェックエラー
                            mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "申込テーブル")
                            Return False
                        End If

                        '更新日時を設定
                        recTmp.UpdDatetime = pUpdDateTime

                        '更新用文字列とパラメータの作成
                        strSqlString = IFsqlMaker.MakeUpdateInfo(recType, recTmp, listParam, GetType(MousikomiRecord))

                        'DB反映処理
                        If Not clsMousikomiDataAcc.UpdateMousikomiData(strSqlString, listParam) Then
                            Return False
                            Exit Function
                        End If

                    End If
                Next

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
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True

    End Function

#End Region

#Region "新規受注処理"

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks>
    ''' コード入力値変更チェック<br/>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Public Function checkInput(ByVal jibanRec As JibanRecordBase) As Boolean
        'エラーメッセージ初期化
        Dim errMess As String = ""

        Dim strErrMsg As String = String.Empty '作業用
        Dim blnKamentenFlg As Boolean = False
        Dim kameitenSearchLogic As New KameitenSearchLogic

        '●コード入力値変更チェック
        'なし

        '●必須チェック
        '依頼日
        If cbLogic.GetDisplayString(jibanRec.IraiDate) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "依頼日")
        End If
        '区分
        If cbLogic.GetDisplayString(jibanRec.Kbn) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "区分")
        End If
        '登録番号(加盟店コード)
        If cbLogic.GetDisplayString(jibanRec.KameitenCd) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "登録番号")
            blnKamentenFlg = True 'フラグを立てる
        End If
        '物件名称
        If cbLogic.GetDisplayString(jibanRec.SesyuMei) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "物件名称")
        End If
        '施主名有無
        If cbLogic.GetDisplayString(jibanRec.SesyuMeiUmu) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "施主名有無")
        End If
        '今回同時依頼棟数
        If cbLogic.GetDispNum(jibanRec.DoujiIraiTousuu, String.Empty) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "今回同時依頼棟数")
        End If
        '物件住所1
        If cbLogic.GetDisplayString(jibanRec.BukkenJyuusyo1) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "物件住所1")
        End If
        '調査希望日
        If cbLogic.GetDisplayString(jibanRec.TysKibouDate) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "調査希望日")
        End If
        '商品1
        If cbLogic.GetDisplayString(jibanRec.SyouhinCd1) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "商品1")
        End If
        '調査方法NO
        If cbLogic.GetDisplayString(jibanRec.TysHouhou) = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "調査方法NO")
        End If

        '●日付チェック
        '(地盤モール側でブロックしているため不要)

        '●禁則文字チェック(文字列入力フィールドが対象)
        '(地盤モール側でブロックしているため不要)

        '●バイト数チェック(文字列入力フィールドが対象)
        '(既に申込Tに格納されているため不要)

        '●桁数チェック
        '(既に申込Tに格納されているため不要)

        '●その他チェック
        '(Chk02:日付項目.TOの値が、日付項目.FROMの値より以前の場合、エラーとする。)
        '登録処理実行前に、JSでチェック済み
        '基礎着工予定日FROM、TOチェック
        '(地盤モール側でブロックしているため不要)

        '(Chk03:画面.区分と加盟店M.区分が不一致の場合、エラーとする。(キー：画面.区分＝加盟店M.区分 AND 画面.登録番号＝加盟店M.加盟店コード)
        '区分、登録番号(加盟店コード)
        ' 入力されているコードをキーに、マスタを検索し、データを1件のみ取得できた場合は
        ' 画面情報を更新する。データが無い場合は、検索画面を表示する
        '(地盤モール側でブロックしているため不要)

        '●調査概要/同時依頼棟数チェック
        strErrMsg = String.Empty
        If cbLogic.ChkErrTysGaiyou(jibanRec.TysGaiyou, jibanRec.DoujiIraiTousuu, strErrMsg) = False Then
            errMess += strErrMsg
        End If
        '●ビルダー注意事項チェック(加盟店関連のエラーがない場合チェックする)
        strErrMsg = String.Empty
        Dim strKameitenTyuuiJikou As String = String.Empty

        If blnKamentenFlg = False Then
            If kameitenSearchLogic.ChkBuilderData13(jibanRec.KameitenCd) Then
                strKameitenTyuuiJikou = Boolean.TrueString
            Else
                strKameitenTyuuiJikou = Boolean.FalseString
            End If
            If cbLogic.ChkErrBuilderData(jibanRec.TysGaiyou, jibanRec.KameitenCd, strKameitenTyuuiJikou, strErrMsg) = False Then
                errMess += strErrMsg
            End If
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            pErrMess &= errMess
            Return False
        End If

        'エラー無しの場合、Trueを返す
        Return True

    End Function

    ''' <summary>
    ''' 申込データを元に地盤データ作成を行います。[EARTH上に受注処理を行なう]
    ''' ※申込テーブル.区分と番号には採番された値をセットし、ステータスを更新する
    ''' </summary>
    ''' <param name="listMi">申込レコード</param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function saveDataJutyuu( _
                            ByVal sender As Object _
                            , ByRef listMi As List(Of MousikomiRecord) _
                            , ByVal emType As EarthEnum.emMousikomiSinkiJutyuuType _
                            ) As Boolean

        Dim recTmp As New MousikomiRecord '作業用
        Dim recResult As New MousikomiRecord 'レコードクラス
        Dim jibanRec As New JibanRecordBase
        Dim total_count As Integer = 0
        Dim dataArray As New List(Of KameitenSearchRecord)

        '検索実行クラス
        Dim dataAccess As New MousikomiDataAccess

        Dim intCnt As Integer = 0
        Dim intRet As Integer = 0
        Dim strTmpMousikomiNo As String = String.Empty
        Dim recTysHouhou As New TyousahouhouRecord

        pUpdDateTime = DateTime.Now
        pErrMess = String.Empty

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                For intCnt = 0 To listMi.Count - 1

                    recTmp = New MousikomiRecord
                    recTmp = listMi(intCnt)
                    strTmpMousikomiNo = recTmp.MousikomiNo

                    recTysHouhou = New TyousahouhouRecord

                    '*************************
                    ' 処理前チェック
                    '*************************
                    '更新対象のレコードを取得
                    recResult = Me.GetMousikomiDataRec(sender, strTmpMousikomiNo)
                    If recResult Is Nothing OrElse recResult.MousikomiNo = Long.MinValue Then
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG147E.Replace("@PARAM1", "申込データ取得")), 1)
                        Return False
                    End If
                    ' ステータスチェック
                    If recResult.Status = EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU Then
                        '受注済の場合、処理をとばす
                        Continue For
                    End If
                    '指定商品コードのチェック
                    'If recResult.SyouhinCd <> EarthConst.SH_CD_SS Then
                    'mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, "指定商品コード[" & EarthConst.SH_CD_SS & "]ではありません。\r\n"), 1)
                    'Return False
                    'End If
                    '調査方法NOのチェック
                    recTysHouhou = jLogic.getTyousahouhouRecord(recResult.TysHouhou)
                    If Not recTysHouhou Is Nothing AndAlso recTysHouhou.Torikesi = 0 Then
                    Else
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, "調査方法[" & recResult.TysHouhou & "]が無効です。\r\n"), 1)
                        Return False
                    End If
                    '区分と加盟店M.区分のチェック
                    total_count = 0
                    If recResult.Kbn <> "" And recResult.KameitenCd <> "" Then
                        dataArray = kLogic.GetKameitenSearchResult(recResult.Kbn, _
                                                                    recResult.KameitenCd, _
                                                                    True, _
                                                                    total_count)
                    End If
                    If total_count = 1 Then '正常
                        '発注停止チェック
                        If EarthConst.Instance.HATTYUU_TEISI_FLGS.ContainsKey(dataArray(0).OrderStopFLG) Then
                            mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG166E), 1)
                            Return False
                        End If
                    Else '異常
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG120E.Replace("@PARAM1", "区分").Replace("@PARAM2", "加盟店コード")), 1)
                        Return False
                    End If
                    '●排他チェック
                    If recResult.UpdDatetime <> recTmp.UpdDatetime Then
                        ' 排他チェックエラー
                        mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "申込テーブル")
                        Return False
                    End If

                    '申込検索画面の場合
                    If emType = EarthEnum.emMousikomiSinkiJutyuuType.SearchMousikomi Then
                        ' チェックが不要になりました：コメントオフ
                        '●同時依頼棟数
                        ' If recResult.DoujiIraiTousuu > CInt(EarthConst.MOUSIKOMI_DOUJI_IRAI_1_TOU) Then
                        ' '1棟より大きい場合新規受注は行なわない
                        ' mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, "同時依頼棟数が2棟以上です。\r\n"), 1)
                        ' Return False
                        ' End If

                        '●重複チェック
                        Dim bolResult1 As Boolean = True
                        Dim bolResult2 As Boolean = True

                        '施主名が空白以外の場合、施主名の重複チェックを実施
                        If recResult.SesyuMei <> String.Empty Then
                            If jLogic.ChkTyouhuku(recResult.Kbn, _
                                                 String.Empty, _
                                                 recResult.SesyuMei) = True Then
                                bolResult1 = False
                            End If
                        End If

                        '物件住所が空白以外の場合、物件住所の重複チェックを実施
                        If recResult.BukkenJyuusyo1 <> String.Empty Or recResult.BukkenJyuusyo2 <> String.Empty Then
                            If jLogic.ChkTyouhuku(recResult.Kbn, _
                                                 String.Empty, _
                                                 recResult.BukkenJyuusyo1, _
                                                 recResult.BukkenJyuusyo2) = True Then
                                bolResult2 = False
                            End If
                        End If

                        If bolResult1 = False OrElse bolResult2 = False Then
                            '重複物件がある場合新規受注は行なわない
                            mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG191E), 1)
                            Return False
                        End If
                    End If

                    '*************************
                    ' 以下、受注処理
                    '*************************
                    '****************************************************************************
                    ' 地盤データ追加
                    '****************************************************************************
                    With recTmp
                        .Kbn = recResult.Kbn
                        .Status = EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU
                        .SyouhinCd = recResult.SyouhinCd
                        .Kousinsya = cbLogic.GetKousinsya(recTmp.UpdLoginUserId, pUpdDateTime)
                        .UpdDatetime = pUpdDateTime

                        '地盤データ追加(区分より番号を取得＆設定)
                        If jLogic.InsertJibanData(sender, .Kbn, .HosyousyoNo, .UpdLoginUserId, 1, EarthEnum.EnumSinkiTourokuMotoKbnType.ReportJHS) = False Then
                            mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG147E.Replace("@PARAM1", "地盤データ追加")), 1)
                            Return False
                        End If
                    End With

                    '****************************************************************************
                    ' 申込データ更新(番号、ステータス)
                    '****************************************************************************
                    intRet = dataAccess.UpdMousikomiJibanLink(recTmp)
                    If intRet <> 1 Then
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG147E.Replace("@PARAM1", "申込データ更新(番号、ステータス)")), 1)
                        Return False
                    End If

                    '****************************************************************************
                    ' 地盤データ更新(申込データ移送)
                    '****************************************************************************
                    intRet = dataAccess.UpdMousikomiData(recTmp)
                    If intRet <> 1 Then
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG147E.Replace("@PARAM1", "地盤データ更新(申込データ移送)")), 1)
                        Return False
                    End If

                    '****************************************************************************
                    ' 地盤データ取得
                    '****************************************************************************
                    jibanRec = New JibanRecordBase
                    jibanRec = jLogic.GetJibanData(recTmp.Kbn, recTmp.HosyousyoNo) '地盤データの取得
                    If jibanRec Is Nothing OrElse jibanRec.Kbn = String.Empty Then
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG147E.Replace("@PARAM1", "地盤データ取得")), 1)
                        Return False
                    End If

                    '****************************************************************************
                    ' 地盤データの自動設定：商品以外の自動設定
                    '****************************************************************************
                    '調査会社未決定=チェック時、仮調査会社を強制的に設定
                    If recTmp.TysKaisyaTaisyou Then
                        Dim tmpTys As String = EarthConst.KARI_TYOSA_KAISYA_CD
                        ' 仮調査会社
                        If tmpTys.Length >= 6 Then   '長さ6以上必須
                            jibanRec.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '調査会社コード
                            jibanRec.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '調査会社事業所コード
                        End If
                    End If

                    Me.ChkOtherAutoSetting(Me, jibanRec)

                    '****************************************************************************
                    ' 商品の自動設定(邸別請求データ)
                    '****************************************************************************
                    jibanRec.SyouhinCd1 = recResult.SyouhinCd

                    '商品1なし時はエラーを返す
                    If Me.ChkSyouhin12AutoSetting(sender, jibanRec) = False Then
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG147E.Replace("@PARAM1", "商品の自動設定") & "\r\n\r\n[詳細]\r\n" & jibanRec.SyouhinKkk_ErrMsg), 1)
                        Return False
                    End If

                    '****************************************************************************
                    ' 商品決定後の自動設定：調査概要の自動設定
                    '****************************************************************************
                    cbLogic.ps_AutoSetTysGaiyou(jibanRec)

                    '****************************************************************************
                    ' 新規受注時入力チェック
                    '****************************************************************************
                    If Me.checkInput(jibanRec) = False Then
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG147E.Replace("@PARAM1", "新規受注時入力チェック")), 1)
                        Return False
                    End If

                    '****************************************************************************
                    ' 地盤データ更新(自動設定)
                    '****************************************************************************
                    If Me.UpdateJibanData(sender, jibanRec) = False Then
                        mLogic.AlertMessage(sender, Me.SetErrMsg(strTmpMousikomiNo, Messages.MSG147E.Replace("@PARAM1", "地盤データ更新(自動設定)")), 1)
                        Return False
                    End If
                Next

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
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' 地盤データを更新します。関連する邸別請求データの更新も行われます
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">更新対象の地盤レコード</param>
    ''' <returns>True:更新成功 False:エラーによる更新失敗</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanData(ByVal sender As Object, _
                                    ByRef jibanRec As JibanRecordBase _
                                    ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanData", _
                                            sender, _
                                            jibanRec)

        Dim jLogic As New JibanLogic

        ' Updateに必要なSQL情報を自動生成するクラス
        Dim upadteMake As New UpdateStringHelper
        ' 排他制御用SQL文
        Dim sqlString As String = ""
        ' Update文
        Dim updateString As String = ""
        ' 排他チェック用パラメータの情報を格納するList(Of ParamRecord)
        Dim haitaList As New List(Of ParamRecord)
        ' 更新用パラメータの情報を格納するList(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        ' 排他チェック用レコード作成
        Dim jibanHaitaRec As New JibanHaitaRecord

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 連携テーブルデータ登録用データの格納用
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' ReportJHS連係対象フラグ(デフォルト：False)
        Dim flgEditReportIf As Boolean

        ' 連棟物件数チェック
        If jibanRec.RentouBukkenSuu = Nothing OrElse jibanRec.RentouBukkenSuu <= 0 Then
            jibanRec.RentouBukkenSuu = 1
        End If
        ' 処理件数チェック
        If jibanRec.SyoriKensuu = Nothing OrElse jibanRec.SyoriKensuu <= 0 Then
            jibanRec.SyoriKensuu = 0
        End If

        ' 番号(保証書NO) 作業用
        Dim intTmpBangou As Integer = CInt(jibanRec.HosyousyoNo) + jibanRec.SyoriKensuu '番号+処理件数
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") 'フォーマット

        Dim strRetBangou As String = jibanRec.HosyousyoNo '画面再描画用

        ' 区分、保証書NO
        Dim kbn As String = jibanRec.Kbn
        Dim hosyousyoNo As String = jibanRec.HosyousyoNo

        ' 特別対応データ取得用のロジッククラス
        Dim ttMLogic As New TokubetuTaiouMstLogic
        ' 特別対応データ格納用のリスト
        Dim listRec As New List(Of TokubetuTaiouRecordBase)

        ' 経由退避用
        Dim intInitKeiyu As Integer = jibanRec.Keiyu

        Dim intCnt As Integer  '処理カウンタ
        Dim intMax As Integer = 20 '処理最大数

        Try

            '*************************
            ' 特別対応データのマスタ取得＆データ登録(ループの外で実行)
            '*************************
            '地盤Tへは登録済みなので、初回のみ全件に対してデフォルト登録する
            If jibanRec.SyoriKensuu = 0 Then
                If jLogic.insertTokubetuTaiouLogic(sender, _
                                              kbn, _
                                              hosyousyoNo, _
                                              jibanRec.KameitenCd, _
                                              jibanRec.SyouhinCd1, _
                                              jibanRec.TysHouhou, _
                                              jibanRec.UpdLoginUserId, _
                                              pUpdDateTime, _
                                              jibanRec.RentouBukkenSuu) Then
                Else
                    'デフォルト登録失敗
                    mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "特別対応データの登録"), 1)
                    Return False
                End If
            End If

            '************************************************
            ' 特別対応データの取得
            '************************************************
            Dim total_count As Integer = 0 '取得件数
            listRec = ttMLogic.GetTokubetuTaiouInfo(sender, _
                                                    kbn, _
                                                    hosyousyoNo, _
                                                    jibanRec.KameitenCd, _
                                                    jibanRec.SyouhinCd1, _
                                                    jibanRec.TysHouhou, _
                                                    total_count)
            If total_count < 0 Then
                Return False
            End If

            '************************************************
            ' 特別対応価格の算出処理
            '************************************************
            Dim intTmpKingakuAction As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION

            '特別対応価格反映処理
            intTmpKingakuAction = cbLogic.pf_ChkTokubetuTaiouKkk(Me, listRec, jibanRec, False)
            If intTmpKingakuAction = EarthEnum.emKingakuAction.KINGAKU_ALERT Then
                mLogic.AlertMessage(sender, Messages.MSG200W.Replace("@PARAM1", cbLogic.AccTokubetuTaiouKkkMsg), 0, "KkkException")
            End If

            '20件ずつ処理
            For intCnt = 1 To intMax

                '*************************
                ' 連棟処理対応
                '*************************
                '処理件数 >= 連棟物件数 の場合、全処理終了
                If jibanRec.SyoriKensuu >= jibanRec.RentouBukkenSuu Then
                    jibanRec.SyoriKensuu = jibanRec.RentouBukkenSuu '処理件数 = 連棟物件数
                    Exit For
                End If

                '更新対象の番号を指定
                jibanRec.HosyousyoNo = strTmpBangou '地盤テーブル

                ' 地盤データより区分、保証書NOを取得（空レコード確認用）
                kbn = jibanRec.Kbn
                hosyousyoNo = jibanRec.HosyousyoNo

                '●物件名寄コードの自動設定
                jibanRec.BukkenNayoseCd = kbn & hosyousyoNo '常に自物件NOをセット

                '*************************
                ' 地盤排他チェック処理
                '*************************
                '対象外
                Dim jibanRecOld As New JibanRecord
                jibanRecOld = jLogic.GetJibanData(kbn, hosyousyoNo)

                '*************************
                ' 与信チェック処理
                '*************************
                '対象外

                '*************************
                ' 更新履歴テーブルの登録
                '*************************
                '対象外

                '*************************
                ' 地盤連携テーブルの更新
                '*************************
                '対象外(採番時に既に連携対象としているため)

                '*************************
                ' R-JHS連携チェック
                '*************************
                ' ReportJHS連係対象チェックを行う(経由：0,1,5の場合のみ)
                flgEditReportIf = False '初期化

                '経由=0or1or5(先頭物件が反映対象外(経由=9)と判定された場合、以後以下の処理はスルー)
                If jibanRec.Keiyu = 0 Or _
                    jibanRec.Keiyu = 1 Or _
                    jibanRec.Keiyu = 5 Then

                    'R-JHS連携チェック
                    If jLogic.ChkRJhsRenkei(jibanRec.TysKaisyaCd, jibanRec.TysKaisyaJigyousyoCd) Then
                        ' 対象の場合、フラグを立てる
                        flgEditReportIf = True
                        ' 反映対象なので経由を5に変更
                        jibanRec.Keiyu = 5
                    Else
                        ' 連携調査会社設定マスタの存在チェックでNGになった場合、経由を9に変更
                        jibanRec.Keiyu = 9
                    End If
                End If

                '連棟処理の場合
                If jibanRec.RentouBukkenSuu > 1 Then
                    'R-JHS連携対象の場合
                    If flgEditReportIf Then
                        If intInitKeiyu = 0 AndAlso jibanRec.SyoriKensuu > 0 Then '画面.経由=0でかつ、2棟目以降の場合
                            '連棟時の連携で経由=0の場合、1棟目のみ連携
                            '1棟目：連携し、経由が5or9になる
                            '2棟目以降：連携せず、経由は0のまま
                            jibanRec.Keiyu = intInitKeiyu
                            flgEditReportIf = False
                        End If
                    End If
                End If

                '****************************************************************************
                ' 地盤データの自動設定：保証書発行状況、保証書発行状況設定日、保証商品有無の自動設定
                '****************************************************************************
                '商品1～3の自動設定後に行なう
                cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.MousikomiSearch, jibanRec)

                '物件履歴データの自動セット
                Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cbLogic.GetDisplayString(jibanRecOld.HosyouSyouhinUmu), cbLogic.GetDisplayString(jibanRecOld.KeikakusyoSakuseiDate))

                If Not brRec Is Nothing Then
                    '物件履歴レコードのチェック
                    Dim strErrMsg As String = String.Empty
                    If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                        mLogic.AlertMessage(sender, strErrMsg, 0, "ErrBukkenRireki")
                        Exit Function
                    End If
                End If

                '*************************
                ' 地盤テーブルの更新
                '*************************
                '更新用文字列とパラメータの作成
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordMousikomiJutyuu), jibanRec, list, GetType(JibanRecordBase))

                ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                If dataAccess.UpdateJibanData(updateString, list) = True Then

                    '**************************************************************************
                    ' 邸別請求データの追加
                    '**************************************************************************
                    '邸別請求テーブルデータ操作用
                    Dim TeibetuSyuuseiLogic As New TeibetuSyuuseiLogic
                    Dim tempTeibetuRec As New TeibetuSeikyuuRecord

                    '***************************
                    ' 商品コード１の追加
                    '***************************
                    '商品１レコードをテンポラリにセット
                    tempTeibetuRec = jibanRec.Syouhin1Record

                    If tempTeibetuRec IsNot Nothing Then
                        '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                        If jibanRec.RentouBukkenSuu > 1 Then
                            tempTeibetuRec.HosyousyoNo = strTmpBangou
                        End If
                    End If

                    '商品１データ処理
                    If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                             tempTeibetuRec, _
                                                             EarthConst.SOUKO_CD_SYOUHIN_1, _
                                                             1, _
                                                             jibanRec, _
                                                             renkeiTeibetuList, _
                                                             GetType(TeibetuSeikyuuRecord) _
                                                             ) = False Then
                        Return False
                    End If

                    '***************************
                    ' 商品コード２の追加
                    '***************************
                    Dim i As Integer
                    For i = 1 To EarthConst.SYOUHIN2_COUNT
                        If Not jibanRec.Syouhin2Records Is Nothing AndAlso jibanRec.Syouhin2Records.ContainsKey(i) = True Then

                            '商品２レコードをテンポラリにセット
                            tempTeibetuRec = jibanRec.Syouhin2Records.Item(i)

                            '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                            If jibanRec.RentouBukkenSuu > 1 Then
                                tempTeibetuRec.HosyousyoNo = strTmpBangou
                            End If

                            ' 商品２の邸別請求データを更新します
                            If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                     tempTeibetuRec, _
                                                                     tempTeibetuRec.BunruiCd, _
                                                                     i, _
                                                                     jibanRec, _
                                                                     renkeiTeibetuList, _
                                                                     GetType(TeibetuSeikyuuRecord) _
                                                                     ) = False Then
                                Return False
                            End If
                        End If
                    Next

                    '***************************
                    ' 商品コード３の追加
                    '***************************
                    For i = 1 To EarthConst.SYOUHIN3_COUNT
                        If Not jibanRec.Syouhin3Records Is Nothing AndAlso jibanRec.Syouhin3Records.ContainsKey(i) = True Then

                            '商品３レコードをテンポラリにセット
                            tempTeibetuRec = jibanRec.Syouhin3Records.Item(i)

                            '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                            If jibanRec.RentouBukkenSuu > 1 Then
                                tempTeibetuRec.HosyousyoNo = strTmpBangou
                            End If

                            ' 商品３の邸別請求データを更新します
                            If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                     tempTeibetuRec, _
                                                                     tempTeibetuRec.BunruiCd, _
                                                                     i, _
                                                                     jibanRec, _
                                                                     renkeiTeibetuList, _
                                                                     GetType(TeibetuSeikyuuRecord) _
                                                                     ) = False Then
                                Return False
                            End If
                        End If
                    Next

                    ' 邸別請求連携反映対象が存在する場合、反映を行う
                    For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                        ' 連携用テーブルに反映する（邸別請求）
                        If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then
                            ' 登録に失敗したので処理中断
                            Return False
                        End If
                    Next

                    ' ●物件履歴テーブル追加(保証有無変更時のみ、物件履歴Tに新規追加する)
                    If Not brRec Is Nothing Then
                        Dim brLogic As New BukkenRirekiLogic

                        '新規追加用スクリプトおよび実行
                        brRec.Kbn = jibanRec.Kbn
                        brRec.HosyousyoNo = jibanRec.HosyousyoNo
                        If brLogic.InsertBukkenRireki(brRec) = False Then
                            Return False
                        End If
                    End If

                    '***************************
                    ' 特別対応テーブルの更新
                    '***************************
                    If Not listRec Is Nothing AndAlso listRec.Count > 0 Then
                        '連棟用の保証書Noを連番でふりなおす
                        For intTokuCnt As Integer = 0 To listRec.Count - 1
                            listRec(intTokuCnt).HosyousyoNo = strTmpBangou
                            listRec(intTokuCnt).UpdFlg = 1
                        Next

                        '特別対応データ更新
                        If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.MousikomiSearch) = False Then
                            mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "特別対応データの更新"), 1)
                            Return False
                        End If
                    End If

                    '先にチェックしておいたReportJHS連係対象チェックを元に、進捗データテーブル更新処理を行う
                    '※ReportJHS連携にて特別対応コードも連携対象のため、最後に処理する
                    If flgEditReportIf Then
                        ' 進捗データテーブル更新処理呼出
                        If jLogic.EditReportIfData(jibanRec) = False Then
                            ' エラー発生時処理終了
                            Return False
                        End If
                    End If

                    '番号をカウントアップ
                    intTmpBangou = CInt(strTmpBangou) + 1 'カウンタ
                    strTmpBangou = Format(intTmpBangou, "0000000000") 'フォーマット

                    jibanRec.SyoriKensuu += 1 '処理件数

                Else
                    Return False
                End If

            Next

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function

#Region "商品１、２の自動設定"
    ''' <summary>
    ''' 商品１、２の自動設定のチェックとセットを行なう。
    ''' ※正常時は該当商品を該当邸別請求レコードにセット
    ''' ※エラー時は処理中断
    ''' </summary>
    ''' <param name="jibanRec"></param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function ChkSyouhin12AutoSetting(ByVal sender As Object, ByRef jibanRec As JibanRecordBase) As Boolean
        '商品コード１の設定実行
        If jLogic.Syouhin1Set(sender, jibanRec) = False Then

        Else '商品1セットOK
            '商品１承諾書金額の設定
            jLogic.Syouhin1SyoudakuGakuSet(sender, jibanRec)

            '商品２レコードオブジェクトの生成
            jLogic.CreateSyouhin23Rec(sender, jibanRec)

            '商品ｺｰﾄﾞ2[多棟数割引]の自動設定
            jLogic.TatouwariSet(sender, jibanRec)

            '請求書発行日、売上年月日の設定実行
            jLogic.Syouhin1UriageSeikyuDateSet(sender, jibanRec, False)

            '商品２レコードオブジェクトの破棄
            jLogic.DeleteSyouhin23Rec(sender, jibanRec)

        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If jibanRec.SyouhinKkk_ErrMsg <> "" Then
            Return False
        End If

        Return True
    End Function

#End Region

#Region "商品以外の自動設定"

    ''' <summary>
    ''' 商品以外の自動設定処理を行なう。
    ''' ※調査会社、調査方法、
    ''' 保証期間、工事会社請求有無
    ''' 戸数、同時依頼棟数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanrec"></param>
    ''' <remarks></remarks>
    Public Sub ChkOtherAutoSetting(ByVal sender As Object, ByRef jibanRec As JibanRecordBase)

        Dim logic As New MousikomiInputLogic
        Dim blnSiteiTysGaisya As Boolean = False
        Dim tmpTys As String = ""

        '調査会社の自動設定
        '調査会社
        Dim strTysGaisyaTmp As String = jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd
        Dim strTysGaisyaAuto As String = strTysGaisyaTmp

        If strTysGaisyaAuto = String.Empty Then '未入力

            '指定調査会社の取得および設定
            blnSiteiTysGaisya = logic.ChkExistSiteiTysGaisya(jibanRec.KameitenCd, strTysGaisyaAuto)
            If blnSiteiTysGaisya Then
                ' 取得した調査会社
                tmpTys = strTysGaisyaAuto
            Else
                ' 仮調査会社
                tmpTys = EarthConst.KARI_TYOSA_KAISYA_CD
            End If

            If tmpTys.Length >= 6 Then   '長さ6以上必須
                jibanRec.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '調査会社コード
                jibanRec.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '調査会社事業所コード
            End If

        End If

        '調査方法の自動設定(未設定時のみ実施)
        If cbLogic.GetDisplayString(jibanRec.TysHouhou) = String.Empty Then
            '調査会社=未入力でかつ、調査方法=未入力の場合でかつ、自動設定の調査会社<>仮調査会社の場合
            If strTysGaisyaTmp = String.Empty _
                AndAlso cbLogic.GetDisplayString(jibanRec.TysHouhou) = String.Empty _
                    AndAlso tmpTys <> String.Empty _
                        AndAlso tmpTys <> EarthConst.KARI_TYOSA_KAISYA_CD Then

                jibanRec.TysHouhou = 1
            Else
                If cbLogic.GetDisplayString(jibanRec.TysHouhou) = String.Empty Then '未入力
                    jibanRec.TysHouhou = 90
                End If
            End If
        End If

        '加盟店による自動設定
        '加盟店関連項目の設定を行なう
        '加盟店検索実行後処理(加盟店詳細情報、ビルダー情報取得)
        Dim kslogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim record As KameitenSearchRecord

        record = kslogic.GetKameitenSearchResult("", jibanRec.KameitenCd, "", blnTorikesi)
        If Not record Is Nothing Then
            '保証書発行有無は０の場合無し、以外１（地盤仕様）
            jibanRec.HosyouUmu = IIf(record.HosyousyoHakUmu = 1, record.HosyousyoHakUmu, Integer.MinValue)
            jibanRec.HosyouKikan = record.HosyouKikan
            ' 工事会社請求有無設定
            jibanRec.KojGaisyaSeikyuuUmu = IIf(record.KojGaisyaSeikyuuUmu = 1, record.KojGaisyaSeikyuuUmu, Integer.MinValue)
        End If

        '●戸数
        If cbLogic.GetDispNum(jibanRec.Kosuu, String.Empty) = String.Empty Then
            jibanRec.Kosuu = 1
        End If

        '●同時依頼棟数
        jibanRec.DoujiIraiTousuu = cbLogic.SetAutoDoujiIraiTousuu(jibanRec.DoujiIraiTousuu)

    End Sub

#End Region

    ''' <summary>
    ''' エラー内容に記載するメッセージを生成する
    ''' </summary>
    ''' <param name="strTmpMousikomiNo">該当の申込NO</param>
    ''' <param name="strProcErrMsg">追加メッセージ</param>
    ''' <remarks></remarks>
    Public Function SetErrMsg(ByVal strTmpMousikomiNo As String, ByVal strProcErrMsg As String) As String
        Dim strTmp As String = pErrMess '退避

        '[申込NO]:該当の申込NO<改行><改行>
        pErrMess = pStrInfoMousikomiNo & strTmpMousikomiNo & EarthConst.CRLF_CODE & EarthConst.CRLF_CODE

        '【内容】<改行> + 処理メッセージ<改行>
        pErrMess &= pStrInfoMousikomiContents & EarthConst.CRLF_CODE & _
                        strProcErrMsg & EarthConst.CRLF_CODE

        '詳細情報がある場合、追加メッセージ
        If strTmp <> String.Empty Then
            '【詳細】<改行> + 追加メッセージ
            pErrMess &= pStrInfoMousikomiDetail & EarthConst.CRLF_CODE & strTmp
        End If

        Return pErrMess
    End Function

#End Region

End Class
