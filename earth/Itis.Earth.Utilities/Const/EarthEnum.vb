''' <summary>
''' 列挙型を格納します
''' </summary>
''' <remarks></remarks>
Public Class EarthEnum

#Region "商品区分種類"
    ''' <summary>
    ''' 商品区分種類
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumSyouhinKubun
        ''' <summary>
        ''' 全商品
        ''' </summary>
        ''' <remarks></remarks>
        AllSyouhin = -1
        ''' <summary>
        ''' 全商品(取消データ含む)
        ''' </summary>
        ''' <remarks></remarks>
        AllSyouhinTorikesi = -2
        ''' <summary>
        ''' 商品区分2
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin1 = 100
        ''' <summary>
        ''' 商品区分2
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2_110 = 110
        ''' <summary>
        ''' 商品区分2
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2_115 = 115
        ''' <summary>
        ''' 商品区分3
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin3 = 120
        ''' <summary>
        ''' 商品区分4
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin4 = 190
        ''' <summary>
        ''' 改良工事
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKouji = 130
        ''' <summary>
        ''' 追加工事
        ''' </summary>
        ''' <remarks></remarks>
        TuikaKouji = 140
        ''' <summary>
        ''' 調査報告書
        ''' </summary>
        ''' <remarks></remarks>
        TyousaHoukokusyo = 150
        ''' <summary>
        ''' 工事報告書
        ''' </summary>
        ''' <remarks></remarks>
        KoujiHoukokusyo = 160
        ''' <summary>
        ''' 保証書
        ''' </summary>
        ''' <remarks></remarks>
        Hosyousyo = 170
        ''' <summary>
        ''' 解約払戻
        ''' </summary>
        ''' <remarks></remarks>
        Kaiyaku = 180
        ''' <summary>
        ''' 登録料
        ''' </summary>
        ''' <remarks></remarks>
        TourokuRyou = 200
        ''' <summary>
        ''' 販促品初期ツール料
        ''' </summary>
        ''' <remarks></remarks>
        ToolRyou = 210
        ''' <summary>
        ''' FC以外販促品
        ''' </summary>
        ''' <remarks></remarks>
        HansokuNotFc = 220
        ''' <summary>
        ''' FC販促品
        ''' </summary>
        ''' <remarks></remarks>
        HansokuFc = 230
    End Enum
#End Region

#Region "請求タイプ"
    ''' <summary>
    ''' 請求タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumSeikyuuType

        ''' <summary>
        ''' 直接請求(３系列)
        ''' </summary>
        ''' <remarks></remarks>
        TyokusetuSeikyuuKeiretu = 0

        ''' <summary>
        ''' 他請求(３系列)
        ''' </summary>
        ''' <remarks></remarks>
        TaSeikyuuKeiretu = 1

        ''' <summary>
        ''' 直接請求(３系列以外)
        ''' </summary>
        ''' <remarks></remarks>
        TyokusetuSeikyuuNotKeiretu = 2

        ''' <summary>
        ''' 他請求(３系列以外)
        ''' </summary>
        ''' <remarks></remarks>
        TaSeikyuuNotKeiretu = 3

    End Enum
#End Region

#Region "加盟店請求先タイプ"
    ''' <summary>
    ''' 加盟店請求先タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumKameitenSeikyuuType
        ''' <summary>
        ''' 工事請求先
        ''' </summary>
        ''' <remarks></remarks>
        KoujiSeikyuusaki = 0
        ''' <summary>
        ''' 販促品請求先
        ''' </summary>
        ''' <remarks></remarks>
        HansokuhinSeikyuusaki = 1
    End Enum
#End Region

#Region "新規登録元区分タイプ"
    ''' <summary>
    ''' 新規登録元区分タイプ
    ''' </summary>
    ''' <remarks>地盤データを新規追加した画面情報を管理する</remarks>
    Enum EnumSinkiTourokuMotoKbnType
        ''' <summary>
        ''' 地盤S:0(or NULL)
        ''' </summary>
        ''' <remarks></remarks>
        JibanS = 0
        ''' <summary>
        ''' EARTH受注:1
        ''' </summary>
        ''' <remarks></remarks>
        EarthJyutyuu = 1
        ''' <summary>
        ''' EARTH申込入力:2
        ''' </summary>
        ''' <remarks></remarks>
        EarthMousikomi = 2
        ''' <summary>
        ''' ReportJHS(ビルダー申込):3
        ''' </summary>
        ''' <remarks></remarks>
        ReportJHS = 3
        ''' <summary>
        ''' ReportJHS(FC申込):4
        ''' </summary>
        ''' <remarks></remarks>
        ReportJHS_FC = 4
    End Enum
#End Region

#Region "ドロップダウンタイプ"
    ''' <summary>
    ''' ドロップダウンタイプ
    ''' </summary>
    ''' <remarks>名称M(名称種別or以外),拡張名称Mかの判断を行なう</remarks>
    Enum emDdlType
        ''' <summary>
        ''' 名称M.名称種別:0
        ''' </summary>
        ''' <remarks></remarks>
        MMeisyouSyubetu = 0
        ''' <summary>
        ''' 名称M.名称種別以外:1
        ''' </summary>
        ''' <remarks></remarks>
        MExcpMeisyouSyubetu = 1
        ''' <summary>
        ''' 拡張名称M:2
        ''' </summary>
        ''' <remarks></remarks>
        KtMeisyou = 2
    End Enum
#End Region

#Region "拡張名称Mドロップダウンタイプ"
    ''' <summary>
    ''' 拡張名称Mドロップダウンタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Enum emKtMeisyouType
        ''' <summary>
        ''' 汎用コード:1
        ''' </summary>
        ''' <remarks></remarks>
        HannyouCd = 1
        ''' <summary>
        ''' 汎用NO:2
        ''' </summary>
        ''' <remarks></remarks>
        HannyouNo = 2
    End Enum
#End Region

#Region "画面モード判定用"
    ''' <summary>
    ''' 画面モード判定用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emGamenMode
        ''' <summary>
        ''' 新規：0
        ''' </summary>
        ''' <remarks></remarks>
        SINKI = 0
        ''' <summary>
        ''' 更新：1
        ''' </summary>
        ''' <remarks></remarks>
        KOUSIN = 1
        ''' <summary>
        ''' 確認：2
        ''' </summary>
        ''' <remarks></remarks>
        KAKUNIN = 2
    End Enum
#End Region

#Region "調査会社マスタ検索処理タイプ"
    ''' <summary>
    ''' 調査会社マスタ検索処理タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EnumTyousakaisyaKensakuType
        ''' <summary>
        ''' 調査会社
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSAKAISYA = 0
        ''' <summary>
        ''' 仕入先
        ''' </summary>
        ''' <remarks></remarks>
        SIIRESAKI = 1
        ''' <summary>
        ''' 支払先
        ''' </summary>
        ''' <remarks></remarks>
        SIHARAISAKI = 2
        ''' <summary>
        ''' 工事会社
        ''' </summary>
        ''' <remarks></remarks>
        KOUJIKAISYA = 3
    End Enum
#End Region

#Region "加盟店調査会社設定マスタ検索処理タイプ"
    ''' <summary>
    ''' 加盟店調査会社設定マスタ検索処理タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EnumKameitenTyousakaisyaKensakuType
        ''' <summary>
        ''' 指定調査会社
        ''' </summary>
        ''' <remarks></remarks>
        SITEITYOUSAKAISYA = 0
        ''' <summary>
        ''' 優先調査会社
        ''' </summary>
        ''' <remarks></remarks>
        YUUSENTYOUSAKAISYA = 1
    End Enum
#End Region

#Region "商品区分３"
    ''' <summary>
    ''' 商品区分３
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSyouhinKbn3
        ''' <summary>
        ''' 調査請求先
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSA = 1
        ''' <summary>
        ''' 工事請求先
        ''' </summary>
        ''' <remarks></remarks>
        KOUJI = 2
        ''' <summary>
        ''' 販促品請求先
        ''' </summary>
        ''' <remarks></remarks>
        HANSOKUHIN = 3
    End Enum
#End Region

#Region "請求書関連"

#Region "請求データ検索タイプ"
    ''' <summary>
    ''' 請求データ検索タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSeikyuuSearchType
        ''' <summary>
        ''' 請求書一覧画面
        ''' </summary>
        ''' <remarks></remarks>
        SearchSeikyuusyo = 1
        ''' <summary>
        ''' 過去請求書一覧画面
        ''' </summary>
        ''' <remarks></remarks>
        KakoSearchSeikyuusyo = 2
        ''' <summary>
        ''' 請求書印字内容編集画面
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoSyuusei = 3
        ''' <summary>
        ''' 請求書締め日履歴照会画面
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoSimeDateRireki = 4
    End Enum
#End Region

#Region "請求データ更新タイプ"
    ''' <summary>
    ''' 請求データ更新タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSeikyuusyoUpdType
        ''' <summary>
        ''' 請求書印字内容修正(請求書印字内容編集画面)
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoSyuusei = 1
        ''' <summary>
        ''' 請求書印刷(請求書一覧画面)
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoPrint = 2
        ''' <summary>
        ''' 請求書取消(請求書一覧画面)
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoTorikesi = 3
    End Enum
#End Region

#Region "請求書一覧画面・実行ボタンタイプ"
    ''' <summary>
    ''' 請求書一覧画面・実行ボタンタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSearchSeikyuusyoBtnType
        ''' <summary>
        ''' 検索実行
        ''' </summary>
        ''' <remarks></remarks>
        Search = 0
        ''' <summary>
        ''' CSV出力
        ''' </summary>
        ''' <remarks></remarks>
        CsvOutput = 1
        ''' <summary>
        ''' 請求書印刷
        ''' </summary>
        ''' <remarks></remarks>
        Print = 2
        ''' <summary>
        ''' 請求書取消
        ''' </summary>
        ''' <remarks></remarks>
        Torikesi = 3
    End Enum
#End Region

#Region "請求書締日履歴照会画面・実行ボタンタイプ"
    ''' <summary>
    ''' 請求書締日履歴照会画面・実行ボタンタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSearchSeikyuuSimeDateRirekiBtnType
        ''' <summary>
        ''' 検索実行
        ''' </summary>
        ''' <remarks></remarks>
        Search = 0
        ''' <summary>
        ''' 履歴取消
        ''' </summary>
        ''' <remarks></remarks>
        Torikesi = 1
    End Enum
#End Region

#End Region

#Region "保証書関連"

#Region "物件進捗状況画面モード"
    ''' <summary>
    ''' 物件進捗状況画面モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emBukkenSintyokuJykyGamenMode
        ''' <summary>
        ''' 最終処理日時(日次)画面
        ''' </summary>
        ''' <remarks></remarks>
        Nitiji = 1
        ''' <summary>
        ''' 最終処理日時(月次)画面
        ''' </summary>
        ''' <remarks></remarks>
        Getuji = 2
    End Enum
#End Region

#End Region

#Region "商品1設定エラー"

    Public Enum emSyouhin1Error
        ''' <summary>
        ''' 価格設定場所取得エラー
        ''' </summary>
        ''' <remarks></remarks>
        GetKakakuSetteiBasyo = 1
        ''' <summary>
        ''' 商品1情報取得エラー
        ''' </summary>
        ''' <remarks></remarks>
        GetSyouhin1 = 2
        ''' <summary>
        ''' 加盟店マスタ価格取得商品対象外エラー
        ''' </summary>
        ''' <remarks></remarks>
        KameiSyouhinTaisyougai = 3
        ''' <summary>
        ''' 加盟店マスタ価格取得エラー
        ''' </summary>
        ''' <remarks></remarks>
        GetKameiKakaku = 4
        ''' <summary>
        ''' 原価マスタ取得エラー
        ''' </summary>
        ''' <remarks></remarks>
        GetGenka = 5
        ''' <summary>
        ''' 調査概要取得エラー
        ''' </summary>
        ''' <remarks></remarks>
        GetTysGaiyou = 6
        ''' <summary>
        ''' 販売価格マスタ取得エラー
        ''' </summary>
        ''' <remarks></remarks>
        GetHanbai = 7
        ''' <summary>
        ''' 原価・販売価格マスタ取得エラー
        ''' </summary>
        ''' <remarks></remarks>
        GetGenkaHanbai = 8
    End Enum

#End Region

#Region "特別対応関連"

#Region "特別対応画面・実行ボタンタイプ"
    ''' <summary>
    ''' 特別対応画面・実行ボタンタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTokubetuTaiouActBtn
        ''' <summary>
        ''' 初期読込処理
        ''' </summary>
        ''' <remarks></remarks>
        BtnLoad = -1
        ''' <summary>
        ''' マスタ再取得ボタン
        ''' </summary>
        ''' <remarks></remarks>
        BtnMaster = 0
        ''' <summary>
        ''' マスタ再取得ボタン以外
        ''' </summary>
        ''' <remarks></remarks>
        BtnOther = 1
        ''' <summary>
        ''' 登録ボタン押下済
        ''' </summary>
        ''' <remarks></remarks>
        PressBtnMstTouroku = 9
    End Enum
#End Region

#Region "特別対応検索タイプ"
    ''' <summary>
    ''' 特別対応検索タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTokubetuTaiouSearchType
        ''' <summary>
        ''' 未設定
        ''' </summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>
        ''' 依頼確認画面
        ''' </summary>
        ''' <remarks></remarks>
        IraiKakunin = 1
        ''' <summary>
        ''' 邸別修正画面
        ''' </summary>
        ''' <remarks></remarks>
        TeibetuSyuusei = 2
    End Enum

#End Region

#End Region

#Region "申込関連"

#Region "申込検索画面・実行ボタンタイプ"
    ''' <summary>
    ''' 申込検索画面・実行ボタンタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSearchMousikomiBtnType
        ''' <summary>
        ''' 検索実行
        ''' </summary>
        ''' <remarks></remarks>
        Search = 0
        ''' <summary>
        ''' 新規受注
        ''' </summary>
        ''' <remarks></remarks>
        SinkiJutyuu = 1
    End Enum

#End Region

#Region "(FC)申込修正画面・(FC)申込データ更新タイプ"
    ''' <summary>
    ''' (FC)申込修正画面・(FC)申込データ更新タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emMousikomiUpdType
        ''' <summary>
        ''' 未設定(Default)
        ''' </summary>
        ''' <remarks></remarks>
        Misettei = 0
        ''' <summary>
        ''' 修正ボタン
        ''' </summary>
        ''' <remarks></remarks>
        Syuusei = 1
        ''' <summary>
        ''' 保留ボタン
        ''' </summary>
        ''' <remarks></remarks>
        Horyuu = 2
        ''' <summary>
        ''' 新規受注ボタン
        ''' </summary>
        ''' <remarks></remarks>
        SinkiJutyuu = 3
    End Enum
#End Region

#Region "申込新規受注タイプ"
    ''' <summary>
    ''' 申込新規受注タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emMousikomiSinkiJutyuuType
        ''' <summary>
        ''' (FC)申込検索画面
        ''' </summary>
        ''' <remarks></remarks>
        SearchMousikomi = 1
        ''' <summary>
        ''' (FC)申込修正画面
        ''' </summary>
        ''' <remarks></remarks>
        MousikomiSyuusei = 2
    End Enum
#End Region

#End Region

#Region "請求年月日一括変更検索タイプ"
    ''' <summary>
    ''' 請求年月日一括変更検索タイプ
    ''' </summary>
    ''' <remarks>対象データを検索するテーブル</remarks>
    Enum emIkkatuHenkouDataSearchType
        ''' <summary>
        ''' 邸別請求テーブル
        ''' </summary>
        ''' <remarks></remarks>
        TeibetuSeikyuu = 1
        ''' <summary>
        ''' 店別請求テーブル
        ''' </summary>
        ''' <remarks></remarks>
        TenbetuSeikyuu = 2
        ''' <summary>
        ''' 店別初期請求テーブル
        ''' </summary>
        ''' <remarks></remarks>
        TenbetuSyoki = 3
        ''' <summary>
        ''' 汎用売上テーブル
        ''' </summary>
        ''' <remarks></remarks>
        HannyouUriage = 4
        ''' <summary>
        ''' 売上テーブル
        ''' </summary>
        ''' <remarks></remarks>
        UriageData = 5
        ''' <summary>
        ''' 売上テーブル(取消)
        ''' </summary>
        ''' <remarks></remarks>
        UriageDataTorikesiSeikyuuDate = 6

    End Enum
#End Region

#Region "工事価格関連"
    ''' <summary>
    ''' 工事価格取得結果
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKoujiKakaku
        ''' <summary>
        ''' 工事価格（加盟店）
        ''' </summary>
        ''' <remarks></remarks>
        Kameiten = 1
        ''' <summary>
        ''' 工事価格（営業所）
        ''' </summary>
        ''' <remarks></remarks>
        Eigyousyo = 2
        ''' <summary>
        ''' 工事価格（系列）
        ''' </summary>
        ''' <remarks></remarks>
        Keiretu = 3
        ''' <summary>
        ''' 工事価格（指定無）
        ''' </summary>
        ''' <remarks></remarks>
        SiteiNasi = 4
        ''' <summary>
        ''' 直工事その他(工事価格Mに組み合わせ無)
        ''' </summary>
        ''' <remarks></remarks>
        TyokuKojSonota = 5
        ''' <summary>
        ''' 商品マスタ
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin = 6
        ''' <summary>
        ''' パラメータ不足
        ''' </summary>
        ''' <remarks></remarks>
        PrmError = 7
    End Enum

    ''' <summary>
    ''' 実行元アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKojKkkActionType
        ''' <summary>
        ''' 加盟店コード
        ''' </summary>
        ''' <remarks></remarks>
        KameitenCd = 0

        ''' <summary>
        ''' 工事会社コード
        ''' </summary>
        ''' <remarks></remarks>
        KojKaisyaCd = 1

        ''' <summary>
        ''' 商品コード
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinCd = 2

        ''' <summary>
        ''' 請求有無
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuuUmu = 3
    End Enum
#End Region

#Region "請求書データ作成関連"
    ''' <summary>
    ''' 日付取得エラータイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emHidukeSyutokuErr

        ''' <summary>
        ''' 正常系
        ''' </summary>
        ''' <remarks></remarks>
        OK = 0

        ''' <summary>
        ''' 日付取得エラー
        ''' </summary>
        ''' <remarks></remarks>
        SyutokuErr = 1

        ''' <summary>
        ''' SQLエラー
        ''' </summary>
        ''' <remarks></remarks>
        SqlErr = 2

        ''' <summary>
        ''' 日付形式エラー
        ''' </summary>
        ''' <remarks></remarks>
        HidukeErr = 3
    End Enum

#End Region

#Region "特別対応価格対応"
    ''' <summary>
    ''' 邸別請求の分類
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTeibetuBunrui
        ''' <summary>
        ''' 商品設定なし
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_NOTHING = 0
        ''' <summary>
        ''' 商品1
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN1 = 1
        ''' <summary>
        ''' 商品2
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN2 = 2
        ''' <summary>
        ''' 商品3
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN3 = 3
    End Enum

    ''' <summary>
    ''' 邸別請求の設定先へのアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTeibetuAction
        ''' <summary>
        ''' なし
        ''' </summary>
        ''' <remarks></remarks>
        TEIBETU_NOTHING = 0
        ''' <summary>
        ''' 商品追加
        ''' </summary>
        ''' <remarks></remarks>
        TEIBETU_ADD = 1
        ''' <summary>
        ''' 商品更新
        ''' </summary>
        ''' <remarks></remarks>
        TEIBETU_UPD = 2
        ''' <summary>
        ''' 商品3へセット
        ''' </summary>
        ''' <remarks></remarks>
        TEIBETU_SYOUHIN_3_SET = 3
    End Enum

    ''' <summary>
    ''' 金額のアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKingakuAction
        ''' <summary>
        ''' エラー
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_ERROR = -2
        ''' <summary>
        ''' 警告
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_ALERT = -1
        ''' <summary>
        ''' なし
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_NOT_ACTION = 0
        ''' <summary>
        ''' 増額
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_PLUS = 1
        ''' <summary>
        ''' 減額
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_MINUS = 2
        ''' <summary>
        ''' 減額増額
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_MINUS_PLUS = 3

    End Enum

    ''' <summary>
    ''' 特別対応価格反映エラー
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKkkHanneiErr
        ''' <summary>
        ''' エラーなし
        ''' </summary>
        ''' <remarks></remarks>
        ERR_NOTHING = 0
        ''' <summary>
        ''' 加算金額エラー(0円、NULLなど)
        ''' </summary>
        ''' <remarks></remarks>
        KASAN_KINGAKU_ERR = 1
        ''' <summary>
        ''' 上記以外のエラー
        ''' </summary>
        ''' <remarks></remarks>
        ERR_OTHER = 2
    End Enum

    ''' <summary>
    ''' 商品1,2,3変更判断
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTeibetuSeikyuuKkkJyky
        ''' <summary>
        ''' 変更なし:0
        ''' </summary>
        ''' <remarks></remarks>
        NONE = 0
        ''' <summary>
        ''' 変更あり(追加):1
        ''' </summary>
        ''' <remarks></remarks>
        INSERT = 1
        ''' <summary>
        ''' 変更あり(更新):2
        ''' </summary>
        ''' <remarks></remarks>
        UPDATE = 2
        ''' <summary>
        ''' 変更あり(削除):3
        ''' </summary>
        ''' <remarks></remarks>
        DELETE = 3
    End Enum

    ''' <summary>
    ''' コード、名称の表示設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emGetDispType
        ''' <summary>
        ''' コード
        ''' </summary>
        ''' <remarks></remarks>
        CODE = 1
        ''' <summary>
        ''' 名称
        ''' </summary>
        ''' <remarks></remarks>
        MEISYOU = 2
        ''' <summary>
        ''' コード":"名称
        ''' </summary>
        ''' <remarks></remarks>
        CODE_MEISYOU = 3
    End Enum

    ''' <summary>
    ''' ツールチップ表示タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emToolTipType
        ''' <summary>
        ''' 表示対象外
        ''' </summary>
        ''' <remarks></remarks>
        NASI = 0
        ''' <summary>
        ''' ｢特｣表示
        ''' </summary>
        ''' <remarks></remarks>
        ARI = 1
        ''' <summary>
        ''' ｢修｣表示
        ''' </summary>
        ''' <remarks></remarks>
        SYUSEI = 2
    End Enum
#End Region

#Region "商品1,2,3振分判定"
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emGamenHyoujiNo
        ''' <summary>
        ''' 画面表示NO = 0:未設定
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NOTHING = 0
        ''' <summary>
        ''' 画面表示NO = 1
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_1 = 1
        ''' <summary>
        ''' 画面表示NO = 2
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_2 = 2
        ''' <summary>
        ''' 画面表示NO = 3
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_3 = 3
        ''' <summary>
        ''' 画面表示NO = 4
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_4 = 4
        ''' <summary>
        ''' 画面表示NO = 5
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_5 = 5
        ''' <summary>
        ''' 画面表示NO = 6
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_6 = 6
        ''' <summary>
        ''' 画面表示NO = 7
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_7 = 7
        ''' <summary>
        ''' 画面表示NO = 8
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_8 = 8
        ''' <summary>
        ''' 画面表示NO = 9
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_9 = 9
    End Enum
#End Region

#Region "画面情報"
    ''' <summary>
    ''' 画面情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emGamenInfo
        ''' <summary>
        ''' 設定なし
        ''' </summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>
        ''' 申込入力
        ''' </summary>
        ''' <remarks></remarks>
        MousikomiInput = 1
        ''' <summary>
        ''' 申込検索
        ''' </summary>
        ''' <remarks></remarks>
        MousikomiSearch = 2
        ''' <summary>
        ''' 申込検索/修正
        ''' </summary>
        ''' <remarks></remarks>
        MousikomiSyuusei = 3
        ''' <summary>
        ''' 受注
        ''' </summary>
        ''' <remarks></remarks>
        Jutyuu = 4
        ''' <summary>
        ''' 一括変更【調査商品情報】
        ''' </summary>
        ''' <remarks></remarks>
        IkkatuTysSyouhinInfo = 5
        ''' <summary>
        ''' 邸別データ修正
        ''' </summary>
        ''' <remarks></remarks>
        TeibetuSyuusei = 6
        ''' <summary>
        ''' 特別対応
        ''' </summary>
        ''' <remarks></remarks>
        TokubetuTaiou = 7
        ''' <summary>
        ''' 改良工事
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKouji = 8
        ''' <summary>
        ''' 保証
        ''' </summary>
        ''' <remarks></remarks>
        Hosyou = 9
        ''' <summary>
        ''' 報告書
        ''' </summary>
        ''' <remarks></remarks>
        Houkokusyo = 10
        ''' <summary>
        ''' FC申込検索
        ''' </summary>
        ''' <remarks></remarks>
        FcMousikomiSearch = 11
        ''' <summary>
        ''' FC申込修正
        ''' </summary>
        ''' <remarks></remarks>
        FcMousikomiSyuusei = 12
    End Enum
#End Region

#Region "実行ボタンタイプ"
    ''' <summary>
    ''' 実行ボタンタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Enum emExecBtnType
        ''' <summary>
        ''' 売上
        ''' </summary>
        ''' <remarks></remarks>
        BtnUriage = 1
        ''' <summary>
        ''' 仕入
        ''' </summary>
        ''' <remarks></remarks>
        BtnSiire = 2
        ''' <summary>
        ''' 入金
        ''' </summary>
        ''' <remarks></remarks>
        BtnNyuukin = 3
    End Enum
#End Region

End Class
