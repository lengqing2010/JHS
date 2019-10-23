Imports System.Configuration

''' <summary>
''' EARTHで使用する定数を管理するクラスです
''' 使用する場合は EarthConst.XXXX と指定(配列等はEarthConst.Instance.XXXX)
''' </summary>
''' <remarks>
''' </remarks>
Public Class EarthConst

    ''' <summary>
    ''' システム名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SYS_NAME As String = "EARTH"

    ''' <summary>
    ''' トランザクションタイムアウト
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TRAN_TIMEOUT As Double = 3.0

    ''' <summary>
    ''' EARTH内部用区切り文字列
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEP_STRING As String = "$$$"

    ''' <summary>
    ''' EARTH内部用区切り文字列2(EARTH-ReportJHS連携用)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEP_STRING_REPORTIF As String = "###"

    ''' <summary>
    ''' 改行コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CRLF_CODE As String = "\r\n"

    ''' <summary>
    ''' EARTHメインウィンドウ名
    ''' </summary>
    Public Const MAIN_WINDOW_NAME As String = "earthMainWindow"

    ''' <summary>
    ''' 画面モード「新規」
    ''' </summary>
    Public Const MODE_NEW As String = "new"

    ''' <summary>
    ''' 画面モード「新規」＋編集ボタンで戻った場合
    ''' </summary>
    Public Const MODE_NEW_EDIT As String = "newe"

    ''' <summary>
    ''' 画面モード「編集」
    ''' </summary>
    Public Const MODE_EDIT As String = "edit"

    ''' <summary>
    ''' 画面モード「照会」
    ''' </summary>
    Public Const MODE_VIEW As String = "view"

    ''' <summary>
    ''' 画面モード「確認」
    ''' </summary>
    Public Const MODE_KAKUNIN As String = "kakunin"

    ''' <summary>
    ''' 確認画面処理モード「登録/修正」
    ''' </summary>
    Public Const MODE_EXE_TOUROKU As String = "touroku"

    ''' <summary>
    ''' 確認画面処理モード「新規引継」
    ''' </summary>
    Public Const MODE_EXE_HIKITUGI As String = "hikitugi"

    ''' <summary>
    ''' 確認画面処理モード「新規連続」
    ''' </summary>
    Public Const MODE_EXE_RENZOKU As String = "renzoku"

    ''' <summary>
    ''' 確認画面処理モード「共通情報コピー」
    ''' </summary>
    Public Const MODE_EXE_COPY As String = "copy"

    ''' <summary>
    ''' 確認画面処理モード「削除」
    ''' </summary>
    Public Const MODE_EXE_SAKUJO As String = "sakujo"

    ''' <summary>
    ''' 確認画面処理モード「調査予定連絡書」
    ''' </summary>
    Public Const MODE_EXE_PDF_RENRAKU As String = "pdfRenraku"

    ''' <summary>
    ''' 確認画面処理モード「登録＆調査見積書作成」
    ''' </summary>
    Public Const MODE_EXE_TYOUSAMITSUMORISYO_SAKUSEI As String = "tyousaMitsumorisyoSakusei"

    ''' <summary>
    ''' 確認画面処理モード「ダイレクト登録/修正」
    ''' (各Stepから直接登録処理を実行する)
    ''' </summary>
    Public Const MODE_EXE_DIRECT_TOUROKU As String = "directTouroku"

    ''' <summary>
    ''' 確認画面の調査見積書ボタンから表示させるPDFファイル名の固定値部分
    ''' 「調査見積書.pdf」
    ''' </summary>
    Public Const PDF_TYOUSAMITSUMORISYO As String = "調査見積書.pdf"

    ''' <summary>
    ''' 依頼画面Step1の画面モードをセッションに格納するキー
    ''' </summary>
    Public Const MODE_KEY_IRAI1 As String = "irai1Mode"

    ''' <summary>
    ''' 依頼画面Step2の画面モードをセッションに格納するキー
    ''' </summary>
    Public Const MODE_KEY_IRAI2 As String = "irai2Mode"

    ''' <summary>
    ''' 依頼画面Step1の画面データをセッションに格納するキー
    ''' </summary>
    Public Const DATA_KEY_IRAI1 As String = "irai1Data"

    ''' <summary>
    ''' 依頼画面Step2の画面データをセッションに格納するキー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATA_KEY_IRAI2 As String = "irai2Data"

    ''' <summary>
    ''' 受注確認画面での処理モードをセッションに格納するキー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MODE_KEY_EXE As String = "exeMode"

    ''' <summary>
    ''' DBから取得した地盤データをセッションに格納するキー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATA_KEY_JIBAN_GET_DATA As String = "jibanGetData"

    ''' <summary>
    ''' DBに登録する地盤データをセッションに格納するキー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATA_KEY_JIBAN_SET_DATA As String = "jibanSetData"

    ''' <summary>
    ''' SS価格の商品コード "A1001"
    ''' </summary>
    Public Const SH_CD_SS As String = "A1001"

    ''' <summary>
    ''' GR価格の商品コード(再調査価格) "A1002"
    ''' </summary>
    Public Const SH_CD_GR As String = "A1002"

    ''' <summary>
    ''' SSGR価格の商品コード "A1003"
    ''' </summary>
    Public Const SH_CD_SSGR As String = "A1003"

    ''' <summary>
    ''' SSリホーム価格の商品コード(解析保証価格) "A1004"
    ''' </summary>
    Public Const SH_CD_SSRH As String = "A1004"

    ''' <summary>
    ''' GRリホーム価格の商品コード(保証無価格) "A1005"
    ''' </summary>
    Public Const SH_CD_GRRH As String = "A1005"

    ''' <summary>
    ''' 商品3で自動設定する商品コード "A2001"
    ''' </summary>
    Public Const SYOUHIN3_AUTO_CD As String = "A2001"

    ''' <summary>
    ''' 解約払戻価格の商品コード "A9001"
    ''' </summary>
    Public Const SH_CD_KAIYAKU As String = "A9001"

    ''' <summary>
    ''' 多棟割設定に使用する商品コード
    ''' </summary>
    Public Const SH_CD_TATOUWARI As String = "AAAAA"

    ''' <summary>
    ''' 多棟割設定に使用する商品コード(除外に使用)
    ''' </summary>
    Public Const SH_CD_TATOUWARI_ER As String = "00000"

    ''' <summary>
    ''' 商品コード：JIO固定コード1
    ''' </summary>
    Public Const SH_CD_JIO As String = "B2008"

    ''' <summary>
    ''' 商品コード：JIO固定コード2
    ''' </summary>
    Public Const SH_CD_JIO2 As String = "B2009"

#Region "特別対応価格対応"
    ''' <summary>
    ''' 商品コード：特別対応固定コード
    ''' </summary>
    Public Const SH_CD_TOKUBETU_TAIOU As String = "A9117"

    ''' <summary>
    ''' 特別対応コード振分設定(最小値)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TOKUBETU_TAIOU_CD_FURIWAKE_MIN As Integer = 80

    ''' <summary>
    ''' 特別対応コード振分設定(最大値)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TOKUBETU_TAIOU_CD_FURIWAKE_MAX As Integer = 99

    ''' <summary>
    ''' 特別対応ツールチップ："特"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TOKU_TOOL_TIP As String = "特"

    ''' <summary>
    ''' 特別対応ツールチップ："修"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SYUU_TOOL_TIP As String = "修"

#End Region
    
    ''' <summary>
    ''' 工事会社コード：工事仕様
    ''' </summary>
    Public Const KOJ_K_CD_JIO As String = "999800"

    ''' <summary>
    ''' 調査会社コード：仮調査会社コード
    ''' </summary>
    Public Const KARI_TYOSA_KAISYA_CD As String = "900000"

    ''' <summary>
    ''' 調査方法コード：仮調査方法コード
    ''' </summary>
    Public Const KARI_TYOUSA_HOUHOU_CD As String = "90"

    ''' <summary>
    ''' 調査方法コード：調査方法コード
    ''' </summary>
    Public Const TYOUSA_HOUHOU_CD_15 As Integer = 15

    ''' <summary>
    ''' SS価格のカラム名
    ''' </summary>
    Public Const TBL_COL_SS As String = "ss_kkk"

    ''' <summary>
    ''' GR価格のカラム名(再調査価格)
    ''' </summary>
    Public Const TBL_COL_GR As String = "sai_tys_kkk"

    ''' <summary>
    ''' SSGR価格のカラム名(SSGR価格)
    ''' </summary>
    Public Const TBL_COL_SSGR As String = "ssgr_kkk"

    ''' <summary>
    ''' SSリホーム価格のカラム名(解析保証価格)
    ''' </summary>
    Public Const TBL_COL_SSRH As String = "kaiseki_hosyou_kkk"

    ''' <summary>
    ''' GRリホーム価格のカラム名(保証無価格)
    ''' </summary>
    Public Const TBL_COL_GRRH As String = "hosyounasi_umu"

    ''' <summary>
    ''' 直接請求
    ''' </summary>
    Public Const SEIKYU_TYOKUSETU As String = "直接請求"
    ''' <summary>
    ''' 直接請求
    ''' </summary>
    Public Const SEIKYU_TYOKUSETU_SHORT As String = "直接"

    ''' <summary>
    ''' 他請求
    ''' </summary>
    Public Const SEIKYU_TASETU As String = "他請求"
    ''' <summary>
    ''' 他請求
    ''' </summary>
    Public Const SEIKYU_TASETU_SHORT As String = "他"

    ''' <summary>
    ''' ＦＣ請求
    ''' </summary>
    Public Const SEIKYU_FCSETU As String = "ＦＣ請求"

    ''' <summary>
    ''' ＦＣ請求
    ''' </summary>
    Public Const SEIKYU_FCSETU_SHORT As String = "ＦＣ"

    ''' <summary>
    ''' 特定３系列 アイフルホーム "0001"
    ''' </summary>
    Public Const KEIRETU_AIFURU As String = "0001"

    ''' <summary>
    ''' 特定３系列 TH友の会 "THTH"
    ''' </summary>
    Public Const KEIRETU_TH As String = "THTH"

    ''' <summary>
    ''' 特定３系列 ワンダーホーム "NF03"
    ''' </summary>
    Public Const KEIRETU_WANDA As String = "NF03"

    ''' <summary>
    ''' 商品1倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_SYOUHIN_1 As String = "100"

    ''' <summary>
    ''' 商品2_1倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_SYOUHIN_2_110 As String = "110"

    ''' <summary>
    ''' 商品2_2倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_SYOUHIN_2_115 As String = "115"

    ''' <summary>
    ''' 商品3倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_SYOUHIN_3 As String = "120"

    ''' <summary>
    ''' 商品4倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_SYOUHIN_4 As String = "190"

    ''' <summary>
    ''' 改良工事倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_KAIRYOU_KOUJI As String = "130"

    ''' <summary>
    ''' 追加工事倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_TUIKA_KOUJI As String = "140"

    ''' <summary>
    ''' 調査報告書再発行手数料倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_TYOUSA_HOUKOKUSYO As String = "150"

    ''' <summary>
    ''' 工事報告書再発行手数料倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_KOUJI_HOUKOKUSYO As String = "160"

    ''' <summary>
    ''' 保証書再発行手数料倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_HOSYOUSYO As String = "170"

    ''' <summary>
    ''' 解約払戻倉庫コード（分類コード）
    ''' </summary>
    Public Const SOUKO_CD_KAIYAKU_HARAIMODOSI As String = "180"

    ''' <summary>
    ''' 登録手数料
    ''' </summary>
    Public Const SOUKO_CD_TOUROKU_TESUURYOU As String = "200"

    ''' <summary>
    ''' 初期ツール料
    ''' </summary>
    Public Const SOUKO_CD_SYOKI_TOOL_RYOU As String = "210"

    ''' <summary>
    ''' FC外販促品
    ''' </summary>
    Public Const SOUKO_CD_FC_GAI_HANSOKUHIN As String = "220"

    ''' <summary>
    ''' FC販促品
    ''' </summary>
    Public Const SOUKO_CD_FC_HANSOKUHIN As String = "230"

    ''' <summary>
    ''' 月額割引
    ''' </summary>
    Public Const SOUKO_CD_GETUGAKU_WARIBIKI As String = "240"

    ''' <summary>
    ''' お知らせ表示上限件数
    ''' </summary>
    Public Const OSIRASE_LIMIT_COUNT As Integer = 30

    ''' <summary>
    ''' ビルダー情報有り
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BUILDER_INFO_ARI As String = "ビルダー情報有り"

    ''' <summary>
    ''' ビルダー情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BUILDER_INFO As String = "ビルダー情報"

    ''' <summary>
    ''' 調査会社NG
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TYOUSA_KAISYA_NG As String = "調査会社NG"

    ''' <summary>
    ''' 工事会社NG
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KOUJI_KAISYA_NG As String = "工事会社NG"

    ''' <summary>
    ''' 判定NG
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HANTEI_NG As String = "判定NG"

    ''' <summary>
    ''' 表示最大件数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_RESULT_COUNT As Integer = 999999999

    ''' <summary>
    ''' 商品郡の列番号配列
    ''' </summary>
    ''' <remarks></remarks>
    Public ARRAY_SHOUHIN_LINES() As String = New String() {"1_1", "2_1", "2_2", "2_3", "2_4", "3_1", "3_2", "3_3", "3_4", "3_5", "3_6", "3_7", "3_8", "3_9"}

    ''' <summary>
    ''' 商品１のレコード数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SYOUHIN1_COUNT As Integer = 1

    ''' <summary>
    ''' 商品２のレコード数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SYOUHIN2_COUNT As Integer = 4

    ''' <summary>
    ''' 商品３のレコード数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SYOUHIN3_COUNT As Integer = 9

    ''' <summary>
    ''' 更新履歴用　施主名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const RIREKI_SESYU_MEI As String = "施主名"

    ''' <summary>
    ''' 更新履歴用　物件住所
    ''' </summary>
    ''' <remarks></remarks>
    Public Const RIREKI_BUKKEN_JYUUSYO As String = "物件住所"

    ''' <summary>
    ''' 更新履歴用　加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const RIREKI_BUKKEN_KAMEITEN_CD As String = "加盟店コード"

    ''' <summary>
    ''' 更新履歴用　調査会社
    ''' </summary>
    ''' <remarks></remarks>
    Public Const RIREKI_TYOUSA_KAISYA As String = "調査会社"

    ''' <summary>
    ''' 更新履歴用　備考
    ''' </summary>
    ''' <remarks></remarks>
    Public Const RIREKI_BIKOU As String = "備考"

    ''' <summary>
    ''' 更新履歴用　備考2
    ''' </summary>
    ''' <remarks></remarks>
    Public Const RIREKI_BIKOU2 As String = "備考2"

    ''' <summary>
    ''' 日付フォーマット１（ミリ秒までを区切り文字無し）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_1 As String = "yyyyMMddHHmmssfff"

    ''' <summary>
    ''' 日付フォーマット２（ミリ秒までを区切り文字無し）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_2 As String = "yyyy/MM/dd HH:mm:ss.fff"

    ''' <summary>
    ''' 日付フォーマット３（yyyyMMdd）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_3 As String = "yyyyMMdd"

    ''' <summary>
    ''' 日付フォーマット４（yy/mm/dd hh:mm）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_4 As String = "yy/MM/dd HH:mm"

    ''' <summary>
    ''' 日付フォーマット５（yyyy/mm/dd hh:mm）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_5 As String = "yyyy/MM/dd HH:mm"

    ''' <summary>
    ''' 日付フォーマット６（秒までを区切り文字無し）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_6 As String = "yyyyMMddHHmmss"

    ''' <summary>
    ''' 日付フォーマット７（yyyy/mm/dd hh:mm:ss）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_7 As String = "yyyy/MM/dd HH:mm:ss"

    ''' <summary>
    ''' 日付フォーマット８（yyyy/MM）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_8 As String = "yyyy/MM"

    ''' <summary>
    ''' 日付フォーマット９（yyyy/MM/dd）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_DATE_TIME_9 As String = "yyyy/MM/dd"

    ''' <summary>
    ''' 金額フォーマット1[Integer]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_KINGAKU_1 = "###,###,##0"

    ''' <summary>
    ''' 金額フォーマット2[Long]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_KINGAKU_2 = "#,###,###,##0"

    ''' <summary>
    ''' 金額フォーマット3
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FORMAT_KINGAKU_3 = "#,##0"

    ''' <summary>
    ''' 日付登録可能最大値
    ''' </summary>
    ''' <remarks></remarks>
    Public MAX_DATE As Date = System.Data.SqlTypes.SqlDateTime.MaxValue

    ''' <summary>
    ''' 日付登録可能最小値
    ''' </summary>
    ''' <remarks></remarks>
    Public MIN_DATE As Date = System.Data.SqlTypes.SqlDateTime.MinValue

    ''' <summary>
    ''' "売上処理済"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const URIAGE_ZUMI As String = "売上処理済"

    ''' <summary>
    ''' "売上計上済"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const URIAGE_KEI_ZUMI As String = "売上計上済"

    ''' <summary>
    ''' "仕入処理済"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SIIRE_ZUMI As String = "仕入処理済"

    ''' <summary>
    ''' "仕入計上済"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SIIRE_KEI_ZUMI As String = "仕入計上済"

    ''' <summary>
    ''' "売上処理済コード"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const URIAGE_ZUMI_CODE As String = "1"

    ''' <summary>
    ''' 使用禁止文字列配列
    ''' </summary>
    ''' <remarks></remarks>
    Public ARRAY_KINSI_STRING() As String = New String() {vbTab, """", ",", "'", "<", ">", "&", SEP_STRING}

    ''' <summary>
    ''' "立会者：施主様"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TATIAI_SESYU_SAMA As String = "施主様"

    ''' <summary>
    ''' "立会者：担当者"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TATIAI_TANTOUSYA As String = "担当者"

    ''' <summary>
    ''' "立会者：その他"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TATIAI_SONOTA As String = "その他"

    ''' <summary>
    ''' "立会者：、(セパレータ)"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TATIAI_SEP_STRING As String = "、"

    ''' <summary>
    ''' javascript:changeDisplay('{0}');
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SCRIPT_JS_CHANGE_DISPLAY As String = "javascript:changeDisplay('{0}');"

    ''' <summary>
    ''' "入金額（税込）"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NYUUKINGAKU_ZEIKOMI As String = "入金額（税込）"

    ''' <summary>
    ''' "返金額（税込）"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HENKINGAKU_ZEIKOMI As String = "返金額（税込）"

    ''' <summary>
    ''' (改良工事)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CTRL_TITLE_KAIRYOU As String = "(改良工事)"

    ''' <summary>
    ''' 工事会社
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CTRL_KOUJI_KAIRYOU As String = "工事会社 "

    ''' <summary>
    ''' 改良売上金額
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CTRL_URIAGE_KAIRYOU As String = "改良売上金額"

    ''' <summary>
    ''' 改良仕入金額
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CTRL_SIIRE_KAIRYOU As String = "改良仕入金額"

    ''' <summary>
    ''' (追加工事)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CTRL_TITLE_TUIKA As String = "(追加工事)"

    ''' <summary>
    ''' 追加工事会社
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CTRL_KOUJI_TUIKA As String = "追加工事会社"

    ''' <summary>
    ''' 追加改良売上金額
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CTRL_URIAGE_TUIKA As String = "追加改良売上金額"

    ''' <summary>
    ''' 追加改良仕入金額
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CTRL_SIIRE_TUIKA As String = "追加改良仕入金額"

    ''' <summary>
    ''' 解約払戻\n申請有無
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAIYAKU_UMU As String = "解約払戻<BR>申請有無"

    ''' <summary>
    ''' 確定
    ''' </summary>
    Public Const KAKUTEI As String = "確定"

    ''' <summary>
    ''' 未確定
    ''' </summary>
    Public Const MIKAKUTEI As String = "未確定"

    ''' <summary>
    ''' 有り
    ''' </summary>
    Public Const ARI As String = "有り"

    ''' <summary>
    ''' 無し
    ''' </summary>
    Public Const NASI As String = "無し"

    ''' <summary>
    ''' あり
    ''' </summary>
    Public Const ARI_HIRAGANA As String = "あり"

    ''' <summary>
    ''' なし
    ''' </summary>
    Public Const NASI_HIRAGANA As String = "なし"

    ''' <summary>
    ''' あり(値="1")
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ARI_VAL As String = "1"

    ''' <summary>
    ''' なし(値="0")
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NASI_VAL As String = "0"

    ''' <summary>
    ''' ・区分={0} ・保証書NO={1} ・分類コード={2} ・画面表示NO={3}
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TEIBETU_KEY As String = "邸別請求TBL ・区分={0} ・保証書NO={1} ・分類コード={2} ・画面表示NO={3}"

    ''' <summary>
    ''' ・区分={0} ・保証書NO={1} ・分類コード={2}
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TEIBETU_KEY2 As String = "邸別入金TBL ・区分={0} ・保証書NO={1} ・分類コード={2}"

    ''' <summary>
    ''' 返金済
    ''' </summary>
    Public Const HENKIN_ZUMI As String = "返金済"

    ''' <summary>
    ''' 返金処理済
    ''' </summary>
    Public Const HENKIN_SYORI_ZUMI As String = "返金処理済"

    ''' <summary>
    ''' 与信警告状況[1:注意(70%以上）]
    ''' </summary>
    Public Const YOSIN_KEIKOKU_70OVER As Integer = 1

    ''' <summary>
    ''' 与信警告状況[2:注意(80%以上）]
    ''' </summary>
    Public Const YOSIN_KEIKOKU_80OVER As Integer = 2

    ''' <summary>
    ''' 与信警告状況[3:警告(90%以上)]
    ''' </summary>
    Public Const YOSIN_KEIKOKU_90OVER As Integer = 3

    ''' <summary>
    ''' 与信警告状況[4:与信超過]
    ''' </summary>
    Public Const YOSIN_KEIKOKU_OVER As Integer = 4

    ''' <summary>
    ''' 与信チェックエラー[0:与信OK]
    ''' </summary>
    Public Const YOSIN_ERROR_YOSIN_OK As Integer = 0

    ''' <summary>
    ''' 与信チェックエラー[1:与信NG＝与信限度額超過：登録拒否]
    ''' </summary>
    Public Const YOSIN_ERROR_YOSIN_NG As Integer = 1

    ''' <summary>
    ''' 与信チェックエラー[6:与信管理マスタ取得エラー]
    ''' </summary>
    Public Const YOSIN_ERROR_YKANRI_GET_ERROR As Integer = 6

    ''' <summary>
    ''' 与信チェックエラー[7:与信管理マスタ更新エラー]
    ''' </summary>
    Public Const YOSIN_ERROR_YKANRI_UPDATE_ERROR As Integer = 7

    ''' <summary>
    ''' 与信チェックエラー[8:与信警告メール送信処理エラー]
    ''' </summary>
    Public Const YOSIN_ERROR_YOSIN_MAIL_ERROR As Integer = 8

    ''' <summary>
    ''' 与信チェックエラー[9:与信チェック処理その他エラー]
    ''' </summary>
    Public Const YOSIN_ERROR_OTHER_ERROR As Integer = 9

    ''' <summary>
    ''' 工事会社請求
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KOUJIGAISYA_SEIKYUU As String = "工事会社請求"

    ''' <summary>
    ''' 外字の文字コードMIN（SJIS）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GAIJI_CODE_MIN As Integer = -4032

    ''' <summary>
    ''' 外字の文字コードMAX（SJIS）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GAIJI_CODE_MAX As Integer = -1540

    ''' <summary>
    ''' タイムアウト
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TIMEOUT_KEYWORD As String = "タイムアウト"

    ''' <summary>
    ''' ReportIF進捗ステータス 登録済(100)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IF_STATUS_TOUROKU_ZUMI As String = "100"

    ''' <summary>
    ''' ReportIF進捗ステータス配列
    ''' </summary>
    ''' <remarks></remarks>
    Public IF_STATUS As Dictionary(Of String, String) = ReportIfStatus()

    ''' <summary>
    ''' JIO先
    ''' </summary>
    Public Const JIO_SAKI As String = "JIO先"

    ''' <summary>
    ''' 計上済
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KEIJOU_ZUMI = "計上済"

    ''' <summary>
    ''' 顧客番号:
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KOKYAKU_BANGOU = "顧客番号:"

    ''' <summary>
    ''' " "(ブランク一文字)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BRANK_STRING = " "

    ''' <summary>
    ''' "BR"(改行コード)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BR_HTML_CD = "<BR>"

    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TORIKESI = "取消"

    ''' <summary>
    ''' 変更不可
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HENKOU_FUKA = "変更不可"

    ''' <summary>
    ''' 売上赤伝票種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UR = "UR"
    ''' <summary>
    ''' 仕入赤伝票種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SR = "SR"
    ''' <summary>
    ''' 入金赤伝票種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FR = "FR"

    ''' <summary>
    ''' アンダースコア
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UNDER_SCORE = "_"
    ''' <summary>
    ''' ハイフン(全角)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HYPHEN_FULL_CHAR = "－"

    ''' <summary>
    ''' 半角スペース
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HANKAKU_SPACE = "&nbsp;"

    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TYS_SEIKYUU_SAKI = "調査請求先"
    ''' <summary>
    ''' 工事請求先
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KOJ_SEIKYUU_SAKI = "工事請求先"
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HANSOKU_SEIKYUU_SAKI = "販促品請求先"

    ''' <summary>
    ''' リンクに設定するTip文字列
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LINK_TIP_STRING = "請：{0}" & vbCrLf & "仕：{1}"

    ''' <summary>
    ''' CSVファイル出力時のデフォルトデリミタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CSV_DELIMITER = ","

    ''' <summary>
    ''' CSVファイル出力時の括り文字
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CSV_QUOTE = """"

    ''' <summary>
    ''' 請求書式ドロップダウンリスト用(ISNULL) VALUE値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ISNULL = "isnull"

    ''' <summary>
    ''' 登録
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TOUROKU As String = GAMEN_MODE_NEW
    ''' <summary>
    ''' 登録有り
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TOUROKU_ARI As String = TOUROKU & ARI
    ''' <summary>
    ''' 登録無し
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TOUROKU_NASI As String = TOUROKU & NASI
    ''' <summary>
    ''' 対象外
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TAISYOU_GAI As String = HYPHEN_FULL_CHAR

#Region "CSSスタイル"
    ''' <summary>
    ''' 文字色スタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_FONT_COLOR As String = "color"
    ''' <summary>
    ''' 背景色スタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_BACK_GROUND_COLOR As String = "background-color"
    ''' <summary>
    ''' 赤字スタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_COLOR_RED As String = "red"
    ''' <summary>
    ''' 青字スタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_COLOR_BLUE As String = "blue"
    ''' <summary>
    ''' 黒字スタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_COLOR_BLACK As String = "black"
    ''' <summary>
    ''' 白字スタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_COLOR_WHITE As String = "white"
    ''' <summary>
    ''' フォントの太さスタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_FONT_WEIGHT As String = "font-weight"
    ''' <summary>
    ''' 一般的な太字（700と同じ）スタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_WEIGHT_BOLD As String = "bold"
    ''' <summary>
    ''' 通常フォントスタイル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_WEIGHT_NORMAL As String = "normal"

#Region "表示スタイル"
    ''' <summary>
    ''' 表示スタイル:display
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_DISPLAY As String = "display"

    ''' <summary>
    ''' 表示スタイル:表示(inline)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_DISPLAY_INLINE As String = "inline"

    ''' <summary>
    ''' 表示スタイル:非表示(none)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_DISPLAY_NONE As String = "none"
#End Region

    ''' <summary>
    ''' フォーカス順:tabindex
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STYLE_TAB_INDEX As String = "tabindex"

#End Region

    ''' <summary>
    ''' 採番種別：分譲コード
    ''' </summary>
    ''' <remarks>採番マスタの採番種別</remarks>
    Public Const SAIBAN_SYUBETU_BUNJOU_CD As String = "01"

    ''' <summary>
    ''' 請求書発行日(yyyy/MM/dd)のdd固定値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEIKYUUSYO_SIME_DATE_DD As String = "/01"

#Region "(FC)申込関連"

#Region "(FC)申込連携・ステータス"
    ''' <summary>
    ''' (Fc)MousikomiIFステータス 未受注(100)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOUSIKOMI_STATUS_MI_JUTYUU As String = "100"

    ''' <summary>
    ''' FcMousikomiIFステータス 保留(150)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOUSIKOMI_STATUS_HORYUU_JUTYUU As String = "150"

    ''' <summary>
    ''' (Fc)MousikomiIFステータス 受注済(200)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOUSIKOMI_STATUS_ZUMI_JUTYUU As String = "200"

    ''' <summary>
    ''' ReportIF進捗ステータス配列
    ''' </summary>
    ''' <remarks></remarks>
    Public MOUSIKOMI_STATUS As Dictionary(Of String, String) = MousikomiIfStatus()
#End Region

#Region "同時依頼棟数"
    ''' <summary>
    ''' MousikomiIF同時依頼棟数(1棟)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOUSIKOMI_DOUJI_IRAI_1_TOU As String = "1"
#End Region

#Region "地下車庫計画(有/無)"
    ''' <summary>
    ''' MousikomiIF地下車庫計画 有
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOUSIKOMI_TIKASYAKOKEIKAKU_ARI As String = "1"
    ''' <summary>
    ''' MousikomiIF地下車庫計画 無
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOUSIKOMI_TIKASYAKOKEIKAKU_NASI As String = "0"
#End Region

#Region "重複物件(有/無)"
    ''' <summary>
    ''' MousikomiIF地下車庫計画 有
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TYOUFUKU_ARI As String = "1"
    ''' <summary>
    ''' MousikomiIF地下車庫計画 無
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TYOUFUKU_NASI As String = "0"
#End Region

#End Region

#Region "商品レコードユーザーコントロールID"
    Public Const USR_CTRL_ID_ITEM1 As String = "CtrlItem1"              '商品1
    Public Const USR_CTRL_ID_ITEM2 As String = "CtrlItem2_"             '商品2
    Public Const USR_CTRL_ID_ITEM3 As String = "CtrlItem3_"             '商品3
    Public Const USR_CTRL_ID_ITEM4 As String = "CtrlItem4_"             '商品4
    Public Const USR_CTRL_ID_K_KOUJI As String = "CtrlKrKoj"            '改良工事
    Public Const USR_CTRL_ID_T_KOUJI As String = "CtrlTkKoj"            '追加工事
    Public Const USR_CTRL_ID_T_HOUKOKU As String = "CtrlTysHokokusyo"   '調査報告書
    Public Const USR_CTRL_ID_K_HOUKOKU As String = "CtrlKojHokokusyo"   '工事報告書
    Public Const USR_CTRL_ID_HOSYOUSYO As String = "CtrlHosyousyo"      '保証書
    Public Const USR_CTRL_ID_KAIYAKU As String = "CtrlKaiyaku"          '解約払戻
#End Region

#Region "商品分類名"
    Public Const ITEM_BUNRUI_NAME_ITEM1 As String = "商品１"                '商品1
    Public Const ITEM_BUNRUI_NAME_ITEM2 As String = "商品２"                '商品2
    Public Const ITEM_BUNRUI_NAME_ITEM3 As String = "商品３"                '商品3
    Public Const ITEM_BUNRUI_NAME_ITEM4 As String = "商品４"                '商品4
    Public Const ITEM_BUNRUI_NAME_K_KOUJI As String = "改良工事"            '改良工事
    Public Const ITEM_BUNRUI_NAME_T_KOUJI As String = "追加工事"            '追加工事
    Public Const ITEM_BUNRUI_NAME_T_HOUKOKU As String = "調査報告書再発行"  '調査報告書
    Public Const ITEM_BUNRUI_NAME_K_HOUKOKU As String = "工事報告書再発行"  '工事報告書
    Public Const ITEM_BUNRUI_NAME_HOSYOUSYO As String = "保証書再発行"      '保証書
    Public Const ITEM_BUNRUI_NAME_KAIYAKU As String = "解約払戻"            '解約払戻
#End Region

#Region "支払/科目"

    ''' <summary>
    ''' 買掛
    ''' </summary>
    Public Const KAMOKU_KAIKAKE As String = "買掛"

    ''' <summary>
    ''' 未払
    ''' </summary>
    Public Const KAMOKU_MIBARAI As String = "未払"

#End Region

#Region "拡張子"

    ''' <summary>
    ''' .csv
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXT_CSV = ".csv"

    ''' <summary>
    ''' .tsv
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXT_TSV = ".tsv"

    ''' <summary>
    ''' .txt
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXT_TXT = ".txt"

#End Region

#Region "Web.config読込"

#Region "上限件数"
    ''' <summary>
    ''' 一括変更画面起動上限物件数
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MAX_IKKATU_KIDOU As String = ConfigurationManager.AppSettings("IkkatuKidouMax")
    ''' <summary>
    ''' CSV出力上限件数
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MAX_CSV_OUTPUT As String = ConfigurationManager.AppSettings("CsvOutputMax")

    ''' <summary>
    ''' CSV出力選択上限件数
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MAX_CSV_SELECT As String = ConfigurationManager.AppSettings("CsvSelectMax")

    ''' <summary>
    ''' 新規受注上限物件数
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MAX_SINKI_JUTYUU As String = ConfigurationManager.AppSettings("SinkiJutyuuMax")

    ''' <summary>
    ''' 選択物件一括受付上限物件数
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MAX_IKKATU_UKETUKE As String = ConfigurationManager.AppSettings("IkkatuUketukeMax")

#End Region

#Region "出力ファイル名"
    ''' <summary>
    ''' CSV出力ファイル名(入金伝票照会画面)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly FILE_NAME_CSV_NYUUKIN_DATA As String = ConfigurationManager.AppSettings("NyuukinData")
    ''' <summary>
    ''' CSV出力ファイル名(売上データ照会画面)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly FILE_NAME_CSV_URIAGE_DATA As String = ConfigurationManager.AppSettings("UriageData")
    ''' <summary>
    ''' CSV出力ファイル名(仕入データ照会画面)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly FILE_NAME_CSV_SIIRE_DATA As String = ConfigurationManager.AppSettings("SiireData")
    ''' <summary>
    ''' CSV出力ファイル名(支払データ照会画面)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly FILE_NAME_CSV_SIHARAI_DATA As String = ConfigurationManager.AppSettings("SiharaiData")
    ''' <summary>
    ''' CSV出力ファイル名(請求書一覧画面)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly FILE_NAME_CSV_SEIKYUUSYO_DATA As String = ConfigurationManager.AppSettings("SeikyuusyosyoData")
    ''' <summary>
    ''' CSV出力ファイル名(過去請求書一覧画面)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly FILE_NAME_CSV_KAKO_SEIKYUUSYO_DATA As String = ConfigurationManager.AppSettings("KakoSeikyuusyosyoData")
    ''' <summary>
    ''' CSV出力ファイル名(品質保証書状況検索画面)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly FILE_NAME_CSV_HINSITU_HOSYOUSYO_JYOUKYOU_DATA As String = ConfigurationManager.AppSettings("HinsituHosyousyoJyoukyouData")
#End Region

#End Region

#Region "画面モード/共通利用(画面名、ボタン名 等) ※注 受注画面以外"
    ''' <summary>
    ''' 画面モード「登録」※受注画面以外
    ''' </summary>
    Public Const GAMEN_MODE_NEW As String = "登録"

    ''' <summary>
    ''' 画面モード「修正」※受注画面以外
    ''' </summary>
    Public Const GAMEN_MODE_EDIT As String = "修正"

    ''' <summary>
    ''' 画面モード「確認」※受注画面以外
    ''' </summary>
    Public Const GAMEN_MODE_VIEW As String = "確認"
#End Region

#Region "画面共通利用(ボタン名) ※請求書系画面"
    ''' <summary>
    ''' 「請求書印刷」ボタン表記名
    ''' </summary>
    Public Const BUTTON_SEIKYUUSYO_PRINT As String = "印刷"

    ''' <summary>
    ''' 「請求書再印刷」ボタン表記名
    ''' </summary>
    Public Const BUTTON_SEIKYUUSYO_RE_PRINT As String = "再印刷"
#End Region

#Region "保証書発行状況の自動設定(保証有無対応)"
    '保証書発行状況の保証有無表示
    Public Const DISP_HOSYOU_ARI_HOSYOUSYO_HAK_JYKY As String = "保証あり"
    Public Const DISP_HOSYOU_NASI_HOSYOUSYO_HAK_JYKY As String = "保証なし"

    '地盤T.保証商品有無の保証有無表示
    Public Const DISP_HOSYOU_ARI_HOSYOU_SYOUHIN_UMU As String = "保証商品あり"
    Public Const DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU As String = "保証商品なし"

    '保証書発行状況ドロップダウンリスト内の表示
    Public Const DISP_DDL_HOSYOU_ARI As String = "保証あり 自動設定"
    Public Const DISP_DDL_HOSYOU_NASI As String = "保証なし 自動設定"

    '保証書発行状況M(「自動設定」のVALUE値)
    Public Const AUTO_SET_VAL_HOSYOU_ARI As String = "91" '保証有(自動設定)
    Public Const AUTO_SET_VAL_HOSYOU_NASI As String = "99" '保証無(自動設定)
#End Region

#Region "物件履歴Tへの自動設定処理"

#Region "物件履歴T.履歴種別"
    '報告書(判定変更)
    Public Const BUKKEN_RIREKI_RIREKI_SYUBETU_HANTEI As String = "24"
    '保証(保証書発行状況変更)
    Public Const BUKKEN_RIREKI_RIREKI_SYUBETU_HOSYOUSYO_HAK_JYKY As String = "28"
#End Region
#Region "物件履歴Tへの書込内容(定型文)"
    '報告書(判定変更)
    Public Const BUKKEN_RIREKI_SEP_STR As String = "★" '区切り文字
    Public Const BUKKEN_RIREKI_HENKOU_MAE As String = BUKKEN_RIREKI_SEP_STR & "変更前" '★変更前
    Public Const BUKKEN_RIREKI_HENKOU_ATO As String = BUKKEN_RIREKI_SEP_STR & "変更後" '★変更後
    Public Const BUKKEN_RIREKI_HENKOU_RIYUU As String = BUKKEN_RIREKI_SEP_STR & "【変更理由】"

    '保証(保証書発行状況変更)
    Public Const BUKKEN_RIREKI_SEP_STR_HOSYOU As String = ";" '区切り文字

#End Region

#End Region

#Region "発注停止フラグ"

    ''' <summary>
    ''' 発注停止フラグ配列
    ''' </summary>
    ''' <remarks></remarks>
    Public HATTYUU_TEISI_FLGS As Dictionary(Of String, String) = dicHattyuuTeisiFlg()

    ''' <summary>
    ''' 加盟店M.発注停止フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Function dicHattyuuTeisiFlg() As Dictionary(Of String, String)
        Dim dic As New Dictionary(Of String, String)
        dic("1") = "1"
        dic("2") = "2"
        Return dic
    End Function

#End Region

#Region "経理関連"
    ''' <summary>
    ''' 経理対応/過去データ境界日付
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KEIRI_DATA_MIN_DATE As String = "2010/04/01"

    ''' <summary>
    ''' 経理追加/年度始め日付
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TERM_FIRST_DATE As String = "/04/01"

#End Region

    ''' <summary>
    ''' TBL_M名称の名称種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emMeisyouType
        ''' <summary>
        ''' 01：商品区分（地盤データ・商品価格設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_KBN = 1
        ''' <summary>
        ''' 02：調査概要（地盤データ・商品価格設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSA_GAIYOU = 2
        ''' <summary>
        ''' 03：価格設定場所（地盤データ・商品価格設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        KAKAKU_SETTEI = 3
        ''' <summary>
        ''' 04：保険申請区分（地盤データ・商品価格設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        HOKEN_SINSEI = 4
        ''' <summary>
        ''' 05：入金確認条件名（加盟店マスタ・地盤データ）
        ''' </summary>
        ''' <remarks></remarks>
        NYUUKIN_KAKUNIN = 5
        ''' <summary>
        ''' 06：経由名（地盤データ）
        ''' </summary>
        ''' <remarks></remarks>
        KEIYU_MEI = 6
        ''' <summary>
        ''' 07：発注書FLG（加盟店マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        HATTYUUSYO_FLG = 7
        ''' <summary>
        ''' 08：調査見積書FLG（加盟店マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        TYS_MITUMORI_FLG = 8
        ''' <summary>
        ''' 09：加盟店備考（加盟店備考設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        KAMEITEN_BIKO = 9
        ''' <summary>
        ''' 10：加盟店注意事項（加盟店注意事項設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        KAMEITEN_TYUUIJIKOU = 10
        ''' <summary>
        ''' 11：手形比率名
        ''' </summary>
        ''' <remarks></remarks>
        TEGATA_HIRITU = 11
        ''' <summary>
        ''' 12：協力会費比率名
        ''' </summary>
        ''' <remarks></remarks>
        KYOURYOKU_KAIHI = 12
        ''' <summary>
        ''' 13：置換工事写真受理名
        ''' </summary>
        ''' <remarks></remarks>
        SYASIN_JURI = 13
        ''' <summary>
        ''' 14：新規登録元区分
        ''' </summary>
        ''' <remarks></remarks>
        SINKI_TOUROKU_MOTO_KBN = 14
        ''' <summary>
        ''' 15：物件履歴項目
        ''' </summary>
        ''' <remarks></remarks>
        BUKKEN_RIREKI_KOUMOKU = 15
        ''' <summary>
        ''' 16：物件履歴(種別)
        ''' </summary>
        ''' <remarks></remarks>
        BUKKEN_RIREKI_SYUBETU = 16
        ''' <summary>
        ''' 17：物件履歴(コード17)
        ''' </summary>
        ''' <remarks></remarks>
        BUKKEN_RIREKI_CODE17 = 17
        ''' <summary>
        ''' 18：物件履歴(コード18)
        ''' </summary>
        ''' <remarks></remarks>
        BUKKEN_RIREKI_CODE18 = 18

    End Enum

    ''' <summary>
    ''' TBL_M拡張名称の名称種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKtMeisyouType
        ''' <summary>
        ''' 1:請求先区分
        ''' </summary>
        ''' <remarks></remarks>
        SEIKYUUSAKI_KBN = 1

        ''' <summary>
        ''' 2:回収種別,支払種別
        ''' </summary>
        ''' <remarks></remarks>
        KAISYUU_SYUBETU = 2

        ''' <summary>
        ''' 3:回収請求書用紙
        ''' </summary>
        ''' <remarks></remarks>
        KAISYUU_SEIKYUUSYO_YOUSI = 3

        ''' <summary>
        ''' 4:預金種別
        ''' </summary>
        ''' <remarks></remarks>
        YOKIN_SYUBETU = 4

        ''' <summary>
        ''' 5:振込先負担先
        ''' </summary>
        ''' <remarks></remarks>
        FURIKOMISAKI_FUTANSAKI = 5

        ''' <summary>
        ''' 9:保証書タイプ
        ''' </summary>
        ''' <remarks></remarks>
        HOSYOUSYO_TYPE = 9

        ''' <summary>
        ''' 10:物件状況
        ''' </summary>
        ''' <remarks></remarks>
        BUKKEN_JYKY = 10

        ''' <summary>
        ''' 11:解析完了
        ''' </summary>
        ''' <remarks></remarks>
        KAISEKI_KANRY = 11

        ''' <summary>
        ''' 12:工事有無
        ''' </summary>
        ''' <remarks></remarks>
        KOJ_UMU = 12

        ''' <summary>
        ''' 13:工事完了
        ''' </summary>
        ''' <remarks></remarks>
        KOJ_KANRY = 13

        ''' <summary>
        ''' 14:入金状況
        ''' </summary>
        ''' <remarks></remarks>
        NYUUKIN_JYKY = 14

        ''' <summary>
        ''' 15:瑕疵
        ''' </summary>
        ''' <remarks></remarks>
        KASI = 15

        ''' <summary>
        ''' 16:引渡し前保険/引渡し後保険
        ''' </summary>
        ''' <remarks></remarks>
        HW_HKN = 16

        ''' <summary>
        ''' 18:保証なし理由
        ''' </summary>
        ''' <remarks></remarks>
        HOSYOU_NASI_RIYUU = 18

        ''' <summary>
        ''' 21:更新項目
        ''' </summary>
        ''' <remarks></remarks>
        KOUSIN_KOUMOKU = 21

        ''' <summary>
        ''' 41:商品マスタ.商品区分1
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_KBN1 = 41

        ''' <summary>
        ''' 42:商品マスタ.商品区分2
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_KBN2 = 42

        ''' <summary>
        ''' 43:商品マスタ.商品区分3
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_KBN3 = 43

        ''' <summary>
        ''' 44:商品マスタ.工事タイプ
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_KOUJI_TYPE = 44

        ''' <summary>
        ''' 70:商品マスタ.倉庫コード
        ''' </summary>
        ''' <remarks></remarks>
        SOUKO_CD = 70

        ''' <summary>
        ''' 100:宅地造成機関
        ''' </summary>
        ''' <remarks></remarks>
        TAKUTI_ZOUSEI_KIKAN = 100

        ''' <summary>
        ''' 101:切土盛土区分
        ''' </summary>
        ''' <remarks></remarks>
        KIRI_MORI_KBN = 101

        ''' <summary>
        ''' 未定:加盟店マスタ.発注書停止フラグ
        ''' </summary>
        ''' <remarks></remarks>
        HATYUUSYO_TEISI_FLG = 999 '仮

    End Enum

    ''' <summary>
    ''' SQL種別判断フラグ列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enSqlTypeFlg
        ''' <summary>
        ''' 更新
        ''' </summary>
        ''' <remarks></remarks>
        UPDATE = 0
        ''' <summary>
        ''' 登録
        ''' </summary>
        ''' <remarks></remarks>
        INSERT = 1
        ''' <summary>
        ''' 削除
        ''' </summary>
        ''' <remarks></remarks>
        DELETE = 9
    End Enum

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New EarthConst()

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As EarthConst
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New EarthConst()
            End If
            Return _instance
        End Get
    End Property

    'コンストラクタ（非公開：外部からのアクセスは不可）
    Private Sub New()
        ' 必要に応じて実装
    End Sub

    ''' <summary>
    ''' ReportIF進捗ステータス配列生成メソッド
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReportIfStatus() As Dictionary(Of String, String)
        Dim dic As New Dictionary(Of String, String)
        dic("100") = "登録済"
        dic("200") = "承諾済"
        dic("400") = "入力開始"
        dic("500") = "入力完了"
        dic("600") = "解析中"
        dic("650") = "解析済"
        dic("700") = "承認"
        dic("800") = "速報発送済"
        dic("850") = "報告書最終承認"
        dic("900") = "発送"
        Return dic
    End Function

    ''' <summary>
    ''' MousikomiIFステータス配列生成メソッド
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MousikomiIfStatus() As Dictionary(Of String, String)
        Dim dic As New Dictionary(Of String, String)
        dic("100") = "未受注"
        dic("200") = "受注済"
        dic("900") = "エラー"
        Return dic
    End Function

#Region "相手先種別"

    ''' <summary>
    ''' 0:指定無
    ''' </summary>
    Public Const AITESAKI_NASI As Integer = 0

    ''' <summary>
    ''' 1:加盟店
    ''' </summary>
    Public Const AITESAKI_KAMEITEN As Integer = 1

    ''' <summary>
    ''' 3:JIO先FLG
    ''' </summary>
    Public Const AITESAKI_JIO_SAKI_FLG As Integer = 3

    ''' <summary>
    ''' 5:営業所
    ''' </summary>
    Public Const AITESAKI_EIGYOUSYO As Integer = 5

    ''' <summary>
    ''' 7：系列
    ''' </summary>
    ''' <remarks></remarks>
    Public Const AITESAKI_KEIRETU As Integer = 7

    ''' <summary>
    ''' JIO先コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const AITESAKI_JIO_SAKI_CD As String = "JIO"

    ''' <summary>
    ''' 相手先なしコード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const AITESAKI_NASI_CD As String = "ALL"

#End Region

#Region "請求先区分"

    ''' <summary>
    ''' 0:加盟店
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEIKYUU_SAKI_KAMEI As String = "0"

    ''' <summary>
    ''' 1:調査会社
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEIKYUU_SAKI_TYS As String = "1"

    ''' <summary>
    ''' 2:営業所
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEIKYUU_SAKI_EIGYO As String = "2"

#End Region

#Region "消費税増税対応"
    ''' <summary>
    ''' 旧税率
    ''' </summary>
    Public Const KYUU_ZEIRITU_005 As Double = 0.05

    ''' <summary>
    ''' 税率0.00%
    ''' </summary>
    Public Const ZEIRITU_000 As Double = 0.0

    ''' <summary>
    ''' 旧税区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KYUU_ZEI_KBN As String = "2"

    ''' <summary>
    ''' 新税率適用開始年月日
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SIN_ZEIRITU_FROM_DATE As String = "20140401"

#End Region

    ''' <summary>
    ''' 物件進捗状況画面 地盤モール公開状況 発行依頼(発行済)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_ZUMI As String = "発行済"

    ''' <summary>
    ''' 物件進捗状況画面 地盤モール公開状況 発行依頼(受付完了)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_KANRY As String = "受付完了"

    ''' <summary>
    ''' 物件進捗状況画面 地盤モール公開状況 発行依頼(ご依頼済)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_IRAIZUMI As String = "ご依頼済"

    ''' <summary>
    ''' 物件進捗状況画面 地盤モール公開状況 発行依頼(未)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_MI As String = "未"

    ''' <summary>
    ''' 品質保証書状況検索画面 発行タイミング(自動発行)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_JIDOU As String = "自動発行"

    ''' <summary>
    ''' 品質保証書状況検索画面 発行タイミング(依頼書)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_IRAISYO As String = "依頼書"

    ''' <summary>
    ''' 品質保証書状況検索画面 発行タイミング(地盤モール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_JIBANMALL As String = "地盤モール"

    ''' <summary>
    ''' 調査会社コード：SDS調整用
    ''' </summary>
    Public Const SDS_TYOSA_KAISYA_CD As String = "900000"

End Class
