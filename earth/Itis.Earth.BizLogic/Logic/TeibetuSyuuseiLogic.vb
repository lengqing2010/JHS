Imports System.Transactions
Imports System.Web.UI
Imports System.text

''' <summary>
''' 邸別データ修正用のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class TeibetuSyuuseiLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic
    Dim cbLogic As New CommonBizLogic

#Region "SQL作成種別"
    ''' <summary>
    ''' SQL作成種別
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SqlType
        ''' <summary>
        ''' 登録SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_INSERT = 1
        ''' <summary>
        ''' 更新SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_UPDATE = 2
        ''' <summary>
        ''' 削除SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_DELETE = 3
    End Enum
#End Region

#Region "邸別請求テーブル/商品分類判断"
    ''' <summary>
    ''' 邸別請求テーブル/商品分類判断
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum TeiberuSyouhinType
        ''' <summary>
        ''' 商品1
        ''' </summary>
        ''' <remarks></remarks>
        SQL_INSERT = 1
        ''' <summary>
        ''' 更新SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_UPDATE = 2
        ''' <summary>
        ''' 削除SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_DELETE = 3
    End Enum
#End Region

#Region "商品情報の取得"
    ''' <summary>
    ''' 商品コード、商品タイプをKeyに商品情報を取得します
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSyouhinType">商品タイプ(SyouhinDataAccess.enumSyouhinKubun)</param>
    ''' <returns>商品情報レコード</returns>
    ''' <remarks>取得できない場合はNothingを返却します</remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String _
                                    , ByVal strSyouhinType As EarthEnum.EnumSyouhinKubun _
                                    , Optional ByVal strKameitenCd As String = "") As SyouhinMeisaiRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo" _
                                                    , strSyouhinCd _
                                                    , strSyouhinType _
                                                    , strKameitenCd)

        Dim dataAccess As New SyouhinDataAccess
        Dim list As List(Of SyouhinMeisaiRecord)
        Dim table As DataTable = dataAccess.GetSyouhinInfo(strSyouhinCd, "", strSyouhinType, strKameitenCd)

        ' データを取得し、List(Of SyouhinRecord)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), _
                                                      table)

        ' １件取得を目的としているので無条件に１件目を返却
        If list.Count > 0 Then
            Return list(0)
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' 解約払戻価格を取得します
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <param name="kbn">区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKaiyakuKakaku(ByVal kameitenCd As String, _
                                     ByVal kbn As String) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKaiyakuKakaku", _
                                                    kameitenCd, _
                                                    kbn)

        Dim dataAccess As New KameitenSearchDataAccess

        Return dataAccess.GetKaiyakuKakaku(kameitenCd, kbn)

    End Function

    ''' <summary>
    ''' 売上データ発行済みチェックデータ取得
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <returns>String(売上データが必要だが、存在しない邸別データキー情報(カンマ区切り))</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuDenpyouHakkouZumiUriageData(ByVal strKbn As String, _
                                                                 ByVal strBangou As String _
                                                                ) As String
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuDenpyouHakkouZumiUriageData", _
                                                    strKbn, _
                                                    strBangou _
                                                    )

        Dim dataAccess As New UriageDataAccess
        Dim dtResult As DataTable
        Dim row As DataRow
        Dim retSB As New StringBuilder

        'データ取得
        dtResult = dataAccess.searchTeibetuSeikyuuDenpyouHakkouZumiUriageData(strKbn, strBangou)

        '取得データをListにセット
        If dtResult.Rows.Count >= 1 Then
            For Each row In dtResult.Rows
                retSB.Append(row(0).ToString & EarthConst.SEP_STRING)
                retSB.Append(row(1).ToString & EarthConst.SEP_STRING)
                retSB.Append(row(2).ToString & EarthConst.SEP_STRING)
                retSB.Append(row(3).ToString & ",")
            Next
        Else
            retSB.Append(String.Empty)
        End If

        '戻り
        Return retSB.ToString
    End Function

#End Region

#Region "データ更新"
    ''' <summary>
    ''' 地盤テーブル・邸別請求テーブルを更新します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="brRec">物件履歴レコード</param>
    ''' <returns>邸別データ修正画面専用</returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordTeibetuSyuusei, _
                                  ByVal brRec As BukkenRirekiRecord, _
                                  ByVal listRec As List(Of TokubetuTaiouRecordBase) _
                                  ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    jibanRec, _
                                                    brRec, _
                                                    listRec)


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
        ' 地盤レコードの同一項目を複製
        RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

        ' 排他チェック用SQL文自動生成
        sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 連携テーブルデータ登録用データの格納用
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        Dim pUpdDateTime As DateTime
        '更新日時取得（システム日時）
        pUpdDateTime = DateTime.Now

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                'JibanLogicクラス
                Dim jibanLogic As New JibanLogic

                ' 排他チェック実施
                Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                            DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                            dataAccess.CheckHaita(sqlString, haitaList))

                If haitaKekkaList.Count > 0 Then
                    Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                    ' 排他チェックエラー
                    ' 排他チェックエラー
                    mLogic.CallHaitaErrorMessage(sender, _
                                                       haitaErrorData.UpdLoginUserId, _
                                                       haitaErrorData.UpdDatetime, _
                                                       "地盤テーブル")

                    Return False
                End If

                '与信チェック処理
                Dim jibanRecOld As New JibanRecord
                jibanRecOld = jibanLogic.GetJibanData(jibanRec.Kbn, jibanRec.HosyousyoNo)

                Dim YosinLogic As New YosinLogic
                Dim intYosinResult As Integer = YosinLogic.YosinCheck(5, jibanRecOld, jibanRec)
                Select Case intYosinResult
                    Case EarthConst.YOSIN_ERROR_YOSIN_OK
                        'エラーなし
                    Case EarthConst.YOSIN_ERROR_YOSIN_NG
                        '与信限度額超過
                        mLogic.AlertMessage(sender, Messages.MSG089E, 1)
                        Return False
                    Case Else
                        '6:与信管理情報取得エラー
                        '7:与信管理テーブル更新エラー
                        '8:メール送信処理エラー
                        '9:その他エラー
                        mLogic.AlertMessage(sender, String.Format(Messages.MSG090E, intYosinResult.ToString()), 1)
                        Return False
                End Select

                ' 更新履歴
                ' 加盟店コード
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                               jibanRec.HosyousyoNo, _
                                               JibanDataAccess.enumItemName.KameitenCd, _
                                               EarthConst.RIREKI_BUKKEN_KAMEITEN_CD, _
                                               jibanRec.KameitenCd, _
                                               jibanRec.UpdLoginUserId) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If
                ' 調査会社
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                               jibanRec.HosyousyoNo, _
                                               JibanDataAccess.enumItemName.TyousaKaisya, _
                                               EarthConst.RIREKI_TYOUSA_KAISYA, _
                                               jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                               jibanRec.UpdLoginUserId) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If
                ' 備考
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.Bikou, _
                                            EarthConst.RIREKI_BIKOU, _
                                            jibanRec.Bikou, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

                ' 地盤データ
                ' 連携用テーブルに登録する（地盤－更新）
                renkeiJibanRec.Kbn = jibanRec.Kbn
                renkeiJibanRec.HosyousyoNo = jibanRec.HosyousyoNo
                renkeiJibanRec.RenkeiSijiCd = 2
                renkeiJibanRec.SousinJykyCd = 0
                renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                    ' 登録失敗時、処理を中断する
                    Return False
                End If

                ' 更新用UPDATE文自動生成
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordTeibetuSyuusei), jibanRec, list)

                ' 地盤テーブルの更新を行う
                If dataAccess.UpdateJibanData(updateString, list) = False Then
                    Return False
                End If

                '*************************
                '* 邸別請求データ
                '*************************

                '基本請求先のセット(商品１）
                setDefaultSeikyuuSaki(jibanRec.Syouhin1Record, jibanRec, jibanRecOld.Syouhin1Record)

                ' 商品１
                If EditTeibetuRecord(sender, _
                                    jibanRec.Syouhin1Record, _
                                    EarthConst.SOUKO_CD_SYOUHIN_1, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                ' 商品２
                Dim i As Integer
                Dim tmpTeibetuRec As TeibetuSeikyuuRecord
                If Not jibanRec.Syouhin2Records Is Nothing Then
                    For i = 1 To EarthConst.SYOUHIN2_COUNT
                        If jibanRec.Syouhin2Records.ContainsKey(i) = True Then
                            'DB情報を格納した邸別請求レコードの取得
                            If jibanRecOld.Syouhin2Records IsNot Nothing Then
                                If jibanRecOld.Syouhin2Records.ContainsKey(i) = True Then
                                    tmpTeibetuRec = jibanRecOld.Syouhin2Records.Item(i)
                                Else
                                    tmpTeibetuRec = Nothing
                                End If
                            Else
                                tmpTeibetuRec = Nothing
                            End If
                            '基本請求先のセット(商品２）
                            setDefaultSeikyuuSaki(jibanRec.Syouhin2Records.Item(i), jibanRec, tmpTeibetuRec)

                            ' 商品２の邸別請求データを更新します
                            If EditTeibetuRecord(sender, _
                                                 jibanRec.Syouhin2Records.Item(i), _
                                                 jibanRec.Syouhin2Records.Item(i).BunruiCd, _
                                                 i, _
                                                 jibanRec, _
                                                 renkeiTeibetuList) = False Then
                                Return False
                            End If
                        Else
                            ' 削除処理用(削除確認は商品２の分類コード何れかで問題なし)
                            If EditTeibetuRecord(sender, _
                                                 Nothing, _
                                                 EarthConst.SOUKO_CD_SYOUHIN_2_110, _
                                                 i, _
                                                 jibanRec, _
                                                 renkeiTeibetuList) = False Then
                                Return False
                            End If
                        End If
                    Next
                End If

                If Not jibanRec.Syouhin3Records Is Nothing Then
                    For i = 1 To EarthConst.SYOUHIN3_COUNT
                        If jibanRec.Syouhin3Records.ContainsKey(i) = True Then
                            'DB情報を格納した邸別請求レコードの取得
                            If jibanRecOld.Syouhin3Records IsNot Nothing Then
                                If jibanRecOld.Syouhin3Records.ContainsKey(i) = True Then
                                    tmpTeibetuRec = jibanRecOld.Syouhin3Records.Item(i)
                                Else
                                    tmpTeibetuRec = Nothing
                                End If
                            Else
                                tmpTeibetuRec = Nothing
                            End If
                            '基本請求先のセット(商品３）
                            setDefaultSeikyuuSaki(jibanRec.Syouhin3Records.Item(i), jibanRec, tmpTeibetuRec)

                            ' 商品３の邸別請求データを更新します
                            If EditTeibetuRecord(sender, _
                                                jibanRec.Syouhin3Records.Item(i), _
                                                jibanRec.Syouhin3Records.Item(i).BunruiCd, _
                                                i, _
                                                jibanRec, _
                                                renkeiTeibetuList) = False Then
                                Return False
                            End If
                        Else
                            ' 削除処理用(削除確認は商品３の分類コード何れかで問題なし)
                            If EditTeibetuRecord(sender, _
                                                Nothing, _
                                                EarthConst.SOUKO_CD_SYOUHIN_3, _
                                                i, _
                                                jibanRec, _
                                                renkeiTeibetuList) = False Then
                                Return False
                            End If
                        End If
                    Next
                End If

                '基本請求先のセット(改良工事）
                setDefaultSeikyuuSaki(jibanRec.KairyouKoujiRecord, jibanRec, jibanRecOld.KairyouKoujiRecord)

                ' 改良工事の邸別請求データを更新します
                If EditTeibetuRecord(sender, _
                                    jibanRec.KairyouKoujiRecord, _
                                    EarthConst.SOUKO_CD_KAIRYOU_KOUJI, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '基本請求先のセット(追加工事）
                setDefaultSeikyuuSaki(jibanRec.TuikaKoujiRecord, jibanRec, jibanRecOld.TuikaKoujiRecord)

                ' 追加工事の邸別請求データを更新します
                If EditTeibetuRecord(sender, _
                                    jibanRec.TuikaKoujiRecord, _
                                    EarthConst.SOUKO_CD_TUIKA_KOUJI, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '基本請求先のセット(追加工事）
                setDefaultSeikyuuSaki(jibanRec.TyousaHoukokusyoRecord, jibanRec, jibanRecOld.TyousaHoukokusyoRecord)

                ' 調査報告書の邸別請求データを更新します
                If EditTeibetuRecord(sender, _
                                    jibanRec.TyousaHoukokusyoRecord, _
                                    EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '基本請求先のセット(追加工事）
                setDefaultSeikyuuSaki(jibanRec.KoujiHoukokusyoRecord, jibanRec, jibanRecOld.KoujiHoukokusyoRecord)

                ' 工事報告書の邸別請求データを更新します
                If EditTeibetuRecord(sender, _
                                    jibanRec.KoujiHoukokusyoRecord, _
                                    EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '基本請求先のセット(保証書）
                setDefaultSeikyuuSaki(jibanRec.HosyousyoRecord, jibanRec, jibanRecOld.HosyousyoRecord)

                ' 保証書の邸別請求データを更新します
                If EditTeibetuRecord(sender, _
                                    jibanRec.HosyousyoRecord, _
                                    EarthConst.SOUKO_CD_HOSYOUSYO, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '基本請求先のセット(解約払い戻し）
                setDefaultSeikyuuSaki(jibanRec.KaiyakuHaraimodosiRecord, jibanRec, jibanRecOld.KaiyakuHaraimodosiRecord)

                ' 解約払い戻しの邸別請求データを更新します
                If EditTeibetuRecord(sender, _
                                    jibanRec.KaiyakuHaraimodosiRecord, _
                                    EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If


                ' 邸別請求連携反映対象が存在する場合、反映を行う
                For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                    ' 連携用テーブルに反映する（邸別請求）
                    If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then

                        ' 登録時エラー
                        mLogic.DbErrorMessage(sender, _
                                                    "登録", _
                                                    "邸別請求連携", _
                                                    String.Format(EarthConst.TEIBETU_KEY, _
                                                                 New String() {renkeiTeibetuRec.Kbn, _
                                                                               renkeiTeibetuRec.HosyousyoNo, _
                                                                               renkeiTeibetuRec.BunruiCd, _
                                                                               renkeiTeibetuRec.GamenHyoujiNo}))
                        ' 登録に失敗したので処理中断
                        Return False
                    End If
                Next

                '特別対応価格対応
                If Not listRec Is Nothing Then
                    '対象データのみ更新
                    For intTokuCnt As Integer = 0 To listRec.Count - 1
                        listRec(intTokuCnt).UpdFlg = 1
                        listRec(intTokuCnt).KkkSetFlg = True
                    Next

                    '特別対応データ更新
                    If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.TeibetuSyuusei) = False Then
                        Return False
                        Exit Function
                    End If
                End If

                ' 更新に成功した場合、進捗テーブルへの反映及び連携データの作成を行う
                ' 進捗テーブルの更新(系列：0,1の場合のみ)
                If jibanRec.Keiyu = 0 Or _
                   jibanRec.Keiyu = 1 Or _
                   jibanRec.Keiyu = 5 Then

                    ' 進捗データ生成系データアクセスクラス
                    Dim reportAccess As New ReportIfDataAccess

                    ' 連携調査会社設定マスタの存在チェック
                    If reportAccess.ChkRenkeiTyousaKaisya(jibanRec.TysKaisyaCd, _
                                                           jibanRec.TysKaisyaJigyousyoCd) = True Then
                        ' 存在する場合、連携テーブルへ反映し経由を5に変更
                        If jibanLogic.EditReportIfData(jibanRec) = False Then
                            ' エラー発生時処理終了
                            Return False
                        End If

                        ' 反映したので経由を5に変更
                        '（データ内容が同一の場合は更新されないが、扱い的には送信済）
                        jibanRec.Keiyu = 5
                    Else
                        ' 連携調査会社設定マスタの存在チェックでNGになった場合、経由を9に変更
                        jibanRec.Keiyu = 9
                    End If
                End If

                ' 経由のみ地盤テーブルの更新を行う
                If dataAccess.UpdateJibanKeiyu(jibanRec.Kbn, _
                                               jibanRec.HosyousyoNo, _
                                               jibanRec.UpdLoginUserId, _
                                               jibanRec.Keiyu) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

                ' ●物件履歴テーブル追加(保証有無変更時のみ、物件履歴Tに新規追加する)
                If Not brRec Is Nothing Then
                    Dim brLogic As New BukkenRirekiLogic

                    '新規追加用スクリプトおよび実行
                    If brLogic.InsertBukkenRireki(brRec) = False Then
                        Return False
                    End If
                End If

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
    ''' 邸別請求データの登録・更新・削除を実施します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="teibetuRec">DB反映対象の邸別請求レコード</param>
    ''' <param name="bunruCd">分類コード</param>
    ''' <param name="gamenHyoujiNo">画面表示NO</param>
    ''' <param name="jibanRec">邸別データ用地盤レコード</param>
    ''' <param name="renkeiRecList">邸別連携テーブルレコードのリスト（参照渡し）</param>
    ''' <param name="teibetuType">邸別請求Tのレコードタイプ</param>
    ''' <returns>処理結果 true:成功 false:失敗</returns>
    ''' <remarks></remarks>
    Public Function EditTeibetuRecord(ByVal sender As Object, _
                                      ByVal teibetuRec As TeibetuSeikyuuRecord, _
                                      ByVal bunruCd As String, _
                                      ByVal gamenHyoujiNo As Integer, _
                                      ByVal jibanRec As JibanRecordBase, _
                                      ByRef renkeiRecList As List(Of TeibetuSeikyuuRenkeiRecord), _
                                      Optional ByVal teibetuType As Type = Nothing _
                                      ) As Boolean

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 現在のレコードを取得
        Dim chkTeibetuList As List(Of TeibetuSeikyuuRecord) = _
                DataMappingHelper.Instance.getMapArray(Of TeibetuSeikyuuRecord)( _
                GetType(TeibetuSeikyuuRecord), _
                dataAccess.GetTeibetuSeikyuuData(jibanRec.Kbn, _
                                                 jibanRec.HosyousyoNo, _
                                                 bunruCd, _
                                                 gamenHyoujiNo))

        ' 存在チェック用レコード保持
        Dim checkRec As New TeibetuSeikyuuRecord

        '登録/更新日時、ログインユーザーIDをセット
        Dim newUpdateDatetime As DateTime = DateTime.Now
        Dim newUpdLoginUserId As String = jibanRec.UpdLoginUserId

        ' 現在データを保持
        If chkTeibetuList.Count > 0 Then        'DB値がある場合
            checkRec = chkTeibetuList(0)

            ' 排他チェック
            ' 現在のDB更新(追加)日時を取得
            Dim dbDate As DateTime = IIf(checkRec.UpdDatetime = DateTime.MinValue, checkRec.AddDatetime, checkRec.UpdDatetime)
            If teibetuRec IsNot Nothing Then
                If teibetuRec.UpdDatetime = dbDate Then
                Else
                    ' 排他チェックエラー
                    mLogic.CallHaitaErrorMessage(sender, _
                                                 IIf(checkRec.UpdLoginUserId = String.Empty, checkRec.AddLoginUserId, checkRec.UpdLoginUserId), _
                                                 dbDate, _
                                                 String.Format(EarthConst.TEIBETU_KEY, _
                                                               New String() {checkRec.Kbn, _
                                                                             checkRec.HosyousyoNo, _
                                                                             checkRec.BunruiCd, _
                                                                             checkRec.GamenHyoujiNo}))
                    Return False
                End If
            End If
        End If



        ' 邸別請求データの登録・更新・削除を実施します
        If teibetuRec Is Nothing Then

            ' 削除されたレコードか確認する
            If chkTeibetuList.Count > 0 Then

                ' 邸別請求DELETE
                If EditTeibetuSeikyuuData(checkRec, SqlType.SQL_DELETE, newUpdLoginUserId) = False Then

                    ' 削除時エラー
                    mLogic.DbErrorMessage(sender, _
                                                "削除", _
                                                "邸別請求", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {checkRec.Kbn, _
                                                                           checkRec.HosyousyoNo, _
                                                                           checkRec.BunruiCd, _
                                                                           checkRec.GamenHyoujiNo}))

                    ' 削除に失敗したので処理中断
                    Return False
                End If

                ' 連携テーブルに登録（削除－邸別請求）
                Dim renkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
                renkeiTeibetuRec.Kbn = checkRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = checkRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = checkRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = checkRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 9
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = newUpdLoginUserId
                renkeiTeibetuRec.IsUpdate = True ' 地盤Sに有り

                ' 更新用リストに格納
                renkeiRecList.Add(renkeiTeibetuRec)

            End If

        Else
            ' 更新or追加
            If chkTeibetuList.Count > 0 Then
                ' 既存データが有るので更新

                '更新日時、更新ログインユーザーIDをセット
                teibetuRec.UpdDatetime = newUpdateDatetime
                teibetuRec.UpdLoginUserId = newUpdLoginUserId
                '標準データをセット
                teibetuRec.UriKeijyouFlg = IIf(teibetuRec.UriKeijyouFlg = 1, teibetuRec.UriKeijyouFlg, 0)
                teibetuRec.IkkatuNyuukinFlg = IIf(teibetuRec.IkkatuNyuukinFlg = 1, teibetuRec.IkkatuNyuukinFlg, Integer.MinValue)
                '●伝票売上年月日のセット
                cbLogic.SetAutoDenUriSiireDate(teibetuRec, checkRec, teibetuType)

                '邸別請求UPDATE
                If EditTeibetuSeikyuuData(teibetuRec, SqlType.SQL_UPDATE, Nothing, teibetuType) = False Then

                    ' 更新時エラー
                    mLogic.DbErrorMessage(sender, _
                                                "更新", _
                                                "邸別請求", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {teibetuRec.Kbn, _
                                                                           teibetuRec.HosyousyoNo, _
                                                                           teibetuRec.BunruiCd, _
                                                                           teibetuRec.GamenHyoujiNo}))
                    ' 更新失敗時、処理を中断する
                    Return False
                End If

                ' 連携用テーブルに登録する（更新－邸別請求）
                Dim renkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
                renkeiTeibetuRec.Kbn = teibetuRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = teibetuRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = teibetuRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = teibetuRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 2
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = newUpdLoginUserId
                renkeiTeibetuRec.IsUpdate = True ' 地盤Sに有り

                ' 更新用リストに格納
                renkeiRecList.Add(renkeiTeibetuRec)

            Else
                ' 既存データが無いので登録

                '登録日時、登録ログインユーザーIDをセット
                teibetuRec.AddDatetime = newUpdateDatetime
                teibetuRec.AddLoginUserId = newUpdLoginUserId
                '更新日時、更新ログインユーザーIDをクリア
                teibetuRec.UpdDatetime = DateTime.MinValue
                teibetuRec.UpdLoginUserId = Nothing
                '標準データをセット
                teibetuRec.UriKeijyouFlg = IIf(teibetuRec.UriKeijyouFlg = 1, teibetuRec.UriKeijyouFlg, 0)
                teibetuRec.IkkatuNyuukinFlg = IIf(teibetuRec.IkkatuNyuukinFlg = 1, teibetuRec.IkkatuNyuukinFlg, Integer.MinValue)
                '●伝票売上年月日のセット
                cbLogic.SetAutoDenUriSiireDate(teibetuRec, checkRec, teibetuType)

                '邸別請求INSERT
                If EditTeibetuSeikyuuData(teibetuRec, SqlType.SQL_INSERT, Nothing, teibetuType) = False Then

                    ' 登録時エラー
                    mLogic.DbErrorMessage(sender, _
                                                "登録", _
                                                "邸別請求", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {teibetuRec.Kbn, _
                                                                           teibetuRec.HosyousyoNo, _
                                                                           teibetuRec.BunruiCd, _
                                                                           teibetuRec.GamenHyoujiNo}))

                    ' 登録失敗時、処理を中断する
                    Return False
                End If

                ' 連携用テーブルに登録する（新規－邸別請求）
                Dim renkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
                renkeiTeibetuRec.Kbn = teibetuRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = teibetuRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = teibetuRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = teibetuRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 1
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = newUpdLoginUserId
                renkeiTeibetuRec.IsUpdate = False ' 全くの新規

                ' 更新用リストに格納
                renkeiRecList.Add(renkeiTeibetuRec)
            End If
        End If

        Return True
    End Function

#Region "邸別請求データ編集"
    ''' <summary>
    ''' 邸別請求データの追加・更新・削除を実行します。
    ''' </summary>
    ''' <param name="teibetuSeikyuuRec">邸別請求レコード</param>
    ''' <param name="_sqlType">SQL Type</param>
    ''' <param name="loginUserId">ログインユーザID</param>
    ''' <param name="teibetuType">邸別請求Tのレコードタイプ</param>
    ''' <returns>True:登録成功 False:登録失敗</returns>
    ''' <remarks></remarks>
    Private Function EditTeibetuSeikyuuData(ByVal teibetuSeikyuuRec As TeibetuSeikyuuRecord, _
                                            ByVal _sqlType As SqlType, _
                                            Optional ByVal loginUserId As String = Nothing, _
                                            Optional ByVal teibetuType As Type = Nothing _
                                            ) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuSeikyuuData", _
                                            teibetuSeikyuuRec, _
                                            _sqlType, _
                                            loginUserId, _
                                            teibetuType)

        ' SQL情報を自動生成するInterface
        Dim sqlMake As ISqlStringHelper = Nothing
        ' SQL格納用
        Dim sqlString As String = ""
        ' パラメータの情報を格納する List(Of ParamRecord)
        Dim list As New List(Of ParamRecord)
        ' 地盤データ取得用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 処理によりインスタンスを生成する
        Select Case _sqlType
            Case SqlType.SQL_INSERT
                sqlMake = New InsertStringHelper
            Case SqlType.SQL_UPDATE
                sqlMake = New UpdateStringHelper
            Case SqlType.SQL_DELETE
                sqlMake = New DeleteStringHelper
                If loginUserId IsNot Nothing Then
                    '削除処理時は、トリガ用ローカル一時テーブルを生成するSQLを追加
                    sqlString &= dataAccess.CreateUserInfoTempTableSQL(loginUserId)
                End If
        End Select

        ' データアクセス用SQL文を生成
        If teibetuType Is Nothing Then
            teibetuType = GetType(TeibetuSeikyuuRecord)
        End If

        'TeibetuSeikyuuRecordクラスとのマッピングを行い、クエリを生成する
        sqlString &= sqlMake.MakeUpdateInfo(teibetuType, teibetuSeikyuuRec, list, GetType(TeibetuSeikyuuRecord))

        ' DB反映処理
        If dataAccess.UpdateJibanData(sqlString, list) = False Then
            Return False
        End If

        Return True
    End Function
#End Region

    ''' <summary>
    ''' 基本請求先情報設定処理
    ''' </summary>
    ''' <param name="dispRec">画面情報を格納した邸別請求レコード</param>
    ''' <param name="dbJrec">DB情報を格納した地盤レコード</param>
    ''' <param name="dbTrec">DB情報を格納した邸別請求レコード</param>
    ''' <remarks></remarks>
    Private Sub setDefaultSeikyuuSaki(ByRef dispRec As TeibetuSeikyuuRecord, ByVal dbJrec As JibanRecordBase, ByVal dbTrec As TeibetuSeikyuuRecord)
        Dim dicSeikyuuSaki As Dictionary(Of String, String) = Nothing   '基本請求先格納Dictionary

        '対象レコードなし
        If dispRec Is Nothing And dbTrec Is Nothing Then
            Exit Sub
        End If
        '対象レコードが削除の場合(削除に関しては上層のロジックで処理から除外されるが念の為)
        If dispRec Is Nothing And dbTrec IsNot Nothing Then
            Exit Sub
        End If


        '****************************************************************
        '* 追加か更新のパターン(dispRec Isnot Nothing)で、計上済みの場合
        '****************************************************************
        '画面に請求先情報がセットされている場合
        If dispRec.SeikyuuSakiCd <> String.Empty _
                Or dispRec.SeikyuuSakiBrc <> String.Empty _
                Or dispRec.SeikyuuSakiKbn <> String.Empty Then
            Exit Sub
        End If

        '対象レコードが更新の場合
        If dbTrec IsNot Nothing Then
            '画面が計上済みでない場合
            If dispRec.UriKeijyouDate = DateTime.MinValue Or dispRec.UriKeijyouFlg <> 1 Then
                Exit Sub
            End If
            '元のDBの値が計上済みの場合
            If dbTrec.UriKeijyouDate <> DateTime.MinValue Or dbTrec.UriKeijyouFlg = 1 Then
                Exit Sub
            End If
        End If

        '画面が計上済みでない場合(対象レコードが追加の場合)
        If dispRec.UriKeijyouDate = DateTime.MinValue Or dispRec.UriKeijyouFlg <> 1 Then
            Exit Sub
        End If

        '未計上⇒計上済にした場合、請求先情報がNULLだった場合は、デフォルトの請求先を設定する
        '工事会社の基本請求先を取得
        Select Case dispRec.BunruiCd
            Case "130"
                If dbJrec.KojGaisyaSeikyuuUmu = 1 Then
                    dicSeikyuuSaki = cbLogic.getDefaultSeikyuuSaki(dbJrec.KojGaisyaCd + dbJrec.KojGaisyaJigyousyoCd)
                End If
            Case "140"
                If dbJrec.TKojKaisyaSeikyuuUmu = 1 Then
                    dicSeikyuuSaki = cbLogic.getDefaultSeikyuuSaki(dbJrec.TKojKaisyaCd + dbJrec.TKojKaisyaJigyousyoCd)
                End If
        End Select
        '加盟店から基本請求先を取得
        If dicSeikyuuSaki Is Nothing OrElse dicSeikyuuSaki(cbLogic.dicKeySeikyuuSakiCd) = String.Empty Then
            dicSeikyuuSaki = cbLogic.getDefaultSeikyuuSaki(dbJrec.KameitenCd, dispRec.SyouhinCd)
        End If

        dispRec.SeikyuuSakiCd = dicSeikyuuSaki(cbLogic.dicKeySeikyuuSakiCd)
        dispRec.SeikyuuSakiBrc = dicSeikyuuSaki(cbLogic.dicKeySeikyuuSakiBrc)
        dispRec.SeikyuuSakiKbn = dicSeikyuuSaki(cbLogic.dicKeySeikyuuSakiKbn)

    End Sub
#End Region

#Region "画面の変更箇所取得用"
    ''' <summary>
    ''' 邸別商品用Hiddenの値を格納した配列をDictionaryに登録
    ''' </summary>
    ''' <param name="strItem1ChgValues">計算処理用Hiddenの値を格納した配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDicItem(ByVal strItem1ChgValues() As String) As Dictionary(Of String, String)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem", _
                                            strItem1ChgValues)

        Dim dic As New Dictionary(Of String, String)

        With dic
            '※画面の項目と順番の同期を必ず取ること!
            For intCnt As Integer = 0 To strItem1ChgValues.Length - 1
                .Add(CStr(intCnt), strItem1ChgValues(intCnt))
            Next
        End With

        Return dic
    End Function

#End Region

End Class
