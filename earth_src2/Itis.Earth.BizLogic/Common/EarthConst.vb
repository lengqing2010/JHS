''' <summary>
''' EARTHで使用する定数を管理するクラスです
''' 使用する場合は EarthConst.XXXX と指定(配列等はEarthConst.Instance.XXXX)
''' </summary>
''' <remarks>
''' </remarks>
Public Class EarthConst

    ''' <summary>
    ''' JavaScript連携用区切り文字列
    ''' </summary>
    ''' <remarks></remarks>
    Public Const sepStr As String = "$$$"

    ''' <summary>
    ''' 画面モード「新規」
    ''' </summary>
    Public Const modeNew As String = "new"

    ''' <summary>
    ''' 画面モード「新規」＋編集ボタンで戻った場合
    ''' </summary>
    Public Const modeNewE As String = "newe"

    ''' <summary>
    ''' 画面モード「編集」
    ''' </summary>
    Public Const modeEdit As String = "edit"

    ''' <summary>
    ''' 画面モード「照会」
    ''' </summary>
    Public Const modeView As String = "view"

    ''' <summary>
    ''' 画面モード「確認」
    ''' </summary>
    Public Const modeKakunin As String = "kakunin"

    ''' <summary>
    ''' 確認画面処理モード「登録/修正」
    ''' </summary>
    Public Const modeExeTouroku As String = "touroku"

    ''' <summary>
    ''' 確認画面処理モード「新規引継」
    ''' </summary>
    Public Const modeExeHikitugi As String = "hikitugi"

    ''' <summary>
    ''' 確認画面処理モード「新規連続」
    ''' </summary>
    Public Const modeExeRenzoku As String = "renzoku"

    ''' <summary>
    ''' 確認画面処理モード「削除」
    ''' </summary>
    Public Const modeExeSakujo As String = "sakujo"

    ''' <summary>
    ''' 確認画面処理モード「調査予定連絡書」
    ''' </summary>
    Public Const modeExePdfRenraku As String = "pdfRenraku"

    ''' <summary>
    ''' 確認画面処理モード「ダイレクト登録/修正」
    ''' (各Stepから直接登録処理を実行する)
    ''' </summary>
    Public Const modeExeDirectTouroku As String = "directTouroku"

    ''' <summary>
    ''' 依頼画面Step1の画面モードをセッションに格納するキー
    ''' </summary>
    Public Const irai1ModeKey As String = "irai1Mode"

    ''' <summary>
    ''' 依頼画面Step2の画面モードをセッションに格納するキー
    ''' </summary>
    Public Const irai2ModeKey As String = "irai2Mode"

    ''' <summary>
    ''' 依頼画面Step1の画面データをセッションに格納するキー
    ''' </summary>
    Public Const irai1DataKey As String = "irai1Data"

    ''' <summary>
    ''' 依頼画面Step2の画面データをセッションに格納するキー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const irai2DataKey As String = "irai2Data"

    ''' <summary>
    ''' 受注確認画面での処理モードをセッションに格納するキー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const exeModeKey As String = "exeMode"

    ''' <summary>
    ''' DBから取得した地盤データをセッションに格納するキー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const jibanGetDataKey As String = "jibanGetData"

    ''' <summary>
    ''' DBに登録する地盤データをセッションに格納するキー
    ''' </summary>
    ''' <remarks></remarks>
    Public Const jibanSetDataKey As String = "jibanSetData"

    ''' <summary>
    ''' SS価格の商品コード "A1001"
    ''' </summary>
    Public Const conSH_CD_SS As String = "A1001"

    ''' <summary>
    ''' GR価格の商品コード(再調査価格) "A1002"
    ''' </summary>
    Public Const conSH_CD_GR As String = "A1002"

    ''' <summary>
    ''' SSGR価格の商品コード "A1003"
    ''' </summary>
    Public Const conSH_CD_SSGR As String = "A1003"

    ''' <summary>
    ''' SSリホーム価格の商品コード(解析保証価格) "A1004"
    ''' </summary>
    Public Const conSH_CD_SSRH As String = "A1004"

    ''' <summary>
    ''' GRリホーム価格の商品コード(保証無価格) "A1005"
    ''' </summary>
    Public Const conSH_CD_GRRH As String = "A1005"

    ''' <summary>
    ''' 解約払戻価格の商品コード "A9001"
    ''' </summary>
    Public Const conSH_CD_KAIYAKU As String = "A9001"

    ''' <summary>
    ''' 多棟割設定に使用する商品コード
    ''' </summary>
    Public Const conSH_CD_TATOUWARI As String = "AAAAA"

    ''' <summary>
    ''' 多棟割設定に使用する商品コード(除外に使用)
    ''' </summary>
    Public Const conSH_CD_TATOUWARI_ER As String = "00000"

    ''' <summary>
    ''' SS価格のカラム名
    ''' </summary>
    Public Const conTBL_COL_SS As String = "ss_kkk"

    ''' <summary>
    ''' GR価格のカラム名(再調査価格)
    ''' </summary>
    Public Const conTBL_COL_GR As String = "sai_tys_kkk"

    ''' <summary>
    ''' SSGR価格のカラム名(SSGR価格)
    ''' </summary>
    Public Const conTBL_COL_SSGR As String = "ssgr_kkk"

    ''' <summary>
    ''' SSリホーム価格のカラム名(解析保証価格)
    ''' </summary>
    Public Const conTBL_COL_SSRH As String = "kaiseki_hosyou_kkk"

    ''' <summary>
    ''' GRリホーム価格のカラム名(保証無価格)
    ''' </summary>
    Public Const conTBL_COL_GRRH As String = "hosyounasi_umu"

    ''' <summary>
    ''' 直接請求
    ''' </summary>
    Public Const conSEIKYU_TYOKUSETU As String = "直接請求"

    ''' <summary>
    ''' 他請求
    ''' </summary>
    Public Const conSEIKYU_TASETU As String = "他請求"

    ''' <summary>
    ''' 特定３系列 アイフルホーム "0001"
    ''' </summary>
    Public Const conKEIRETU_AIFURU As String = "0001"

    ''' <summary>
    ''' 特定３系列 TH友の会 "THTH"
    ''' </summary>
    Public Const conKEIRETU_TH As String = "THTH"

    ''' <summary>
    ''' 特定３系列 ワンダーホーム "NF03"
    ''' </summary>
    Public Const conKEIRETU_WANDA As String = "NF03"

    ''' <summary>
    ''' 商品1倉庫コード（分類コード）
    ''' </summary>
    Public Const conSH1_SOUKO_CD As String = "100"

    ''' <summary>
    ''' 商品2_1倉庫コード（分類コード）
    ''' </summary>
    Public Const conSH2_1_SOUKO_CD As String = "110"

    ''' <summary>
    ''' 商品2_2倉庫コード（分類コード）
    ''' </summary>
    Public Const conSH2_2_SOUKO_CD As String = "115"

    ''' <summary>
    ''' 商品3倉庫コード（分類コード）
    ''' </summary>
    Public Const conSH3_SOUKO_CD As String = "120"

    ''' <summary>
    ''' 解約払い戻し分類コード
    ''' </summary>
    Public Const conOTH_KAIYAKU_CD As String = "180"

    ''' <summary>
    ''' お知らせ表示上限件数
    ''' </summary>
    Public Const osiraseLimit As Integer = 30

    ''' <summary>
    ''' ビルダー情報有り
    ''' </summary>
    ''' <remarks></remarks>
    Public Const builder_info_ari As String = "ビルダー情報有り"

    ''' <summary>
    ''' ビルダー情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Const builder_info As String = "ビルダー情報"

    ''' <summary>
    ''' 調査会社NG
    ''' </summary>
    ''' <remarks></remarks>
    Public Const tyousa_kaisya_ng As String = "調査会社NG"

    ''' <summary>
    ''' 表示最大件数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conMaxResultCount As Integer = 999999999

    ''' <summary>
    ''' 商品郡の列番号配列
    ''' </summary>
    ''' <remarks></remarks>
    Public arrayShouhinLines() As String = New String() {"1_1", "2_1", "2_2", "2_3", "2_4", "3_1", "3_2", "3_3", "3_4", "3_5", "3_6", "3_7", "3_8", "3_9"}

    ''' <summary>
    ''' 更新履歴用　施主名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conSesyuMei As String = "施主名"

    ''' <summary>
    ''' 更新履歴用　物件住所
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conBukkenjyuusyo As String = "物件住所"

    ''' <summary>
    ''' 更新履歴用　調査会社
    ''' </summary>
    ''' <remarks></remarks>
    Public Const contyousaKaisya As String = "調査会社"

    ''' <summary>
    ''' 更新履歴用　備考
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conBikou As String = "備考"

    ''' <summary>
    ''' 日付フォーマット１（ミリ秒までを区切り文字無し）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const dateTimeFormat1 As String = "yyyyMMddHHmmssfff"

    ''' <summary>
    ''' 日付登録可能最大値
    ''' </summary>
    ''' <remarks></remarks>
    Public dateMax As Date = System.Data.SqlTypes.SqlDateTime.MaxValue

    ''' <summary>
    ''' 日付登録可能最小値
    ''' </summary>
    ''' <remarks></remarks>
    Public dateMin As Date = System.Data.SqlTypes.SqlDateTime.MinValue

    ''' <summary>
    ''' "売上処理済"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conUriageZumi As String = "売上処理済"

    ''' <summary>
    ''' 使用禁止文字列配列
    ''' </summary>
    ''' <remarks></remarks>
    Public arrayKinsiStr() As String = New String() {vbCrLf, vbTab, """", ",", "'", "<", ">", "&", sepStr}

    ''' <summary>
    ''' "立会者：施主様"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conTatiaiSesyuSama As String = "施主様"

    ''' <summary>
    ''' "立会者：担当者"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conTatiaiTantousya As String = "担当者"

    ''' <summary>
    ''' "立会者：その他"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conTatiaiSonota As String = "その他"

    ''' <summary>
    ''' "立会者：、"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conTatiaiSep As String = "、"

    ''' <summary>
    ''' "加盟店基本情報CSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conKihonJyouhouCsvHeader As String = "区分,加盟店ｺｰﾄﾞ,取消,加盟店名1,店名ｶﾅ1,加盟店名2,店名ｶﾅ2,営業所ｺｰﾄﾞ,系列ｺｰﾄﾞ,TH瑕疵ｺｰﾄﾞ,ﾋﾞﾙﾀﾞｰNO,発注停止フラグ,都道府県ｺｰﾄﾞ,年間棟数,営業担当者,引継完了日,旧営業担当者,地震補償FLG,地震補償登録日,付保証明FLG,付保証明書開始年月,工事売上種別,工事サポートシステム,JIO先FLG,正式名称,正式名称2,解約払戻価格,棟区分1商品ｺｰﾄﾞ,棟区分1商品名,棟区分2商品ｺｰﾄﾞ,棟区分2商品名,棟区分3商品ｺｰﾄﾞ,棟区分3商品名,調査請求先 区分,調査請求先コード,調査請求先枝番,調査請求先名,調査請求締め日,工事請求先 区分,工事請求先コード,工事請求先枝番,工事請求先名,工事請求締め日,販促品請求先 区分,販促品請求先コード,販促品請求先枝番,販促品請求先名,販促品請求締め日,建物検査請求先 区分,建物検査請求先コード,建物検査請求先枝番,建物検査請求先名,建物検査請求締め日,請求先区分5,請求先コード5,請求先枝番5,請求先5名,請求先5締め日,請求先区分6,請求先コード6,請求先枝番6,請求先6名,請求先6締め日,請求先区分7,請求先コード7,請求先枝番7,請求先7締め日,請求先7締め日,保証期間,保証書発行有無,入金確認条件,入金確認条件名,入金確認覚書,工事会社請求有無,工事担当FLG,調査見積書FLG,発注書FLG,見積書ﾌｧｲﾙ名,調査見積書,基礎断面図,多棟割引区分,多棟割引備考,特価申請,残土処分費,給水車代,地縄はり,杭芯出し,平均日数,標準基礎,JHS以外工事,調査報告書部数,工事報告書部数,検査報告書部数,調査報告書同封,工事報告書同封,検査報告書同封,入金前保証書発行,引渡ファイル,回収締め日,請求書必着日,支払予定月,支払予定日,現金割合,支払方法,手形割合,支払サイト,手形比率,調査発注書有無,工事発注書有無,検査発注書有無,先方指定請求書,フロー確認日,協力会費,協力会費比率,断面図1,断面図2,断面図3,断面図4,断面図5,断面図6,断面図7"

    ''' <summary>
    ''' "加盟店住所情報CSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conJyuusyoJyouhouCsvHeader As String = "取消,区分,加盟店ｺｰﾄﾞ,加盟店名1,店名ｶﾅ1,加盟店名2,店名ｶﾅ2,都道府県ｺｰﾄﾞ,県名,代表者名,登録年月,ﾋﾞﾙﾀﾞｰNO,営業所ｺｰﾄﾞ,営業所名,登録住所：郵便番号,登録住所：住所1,登録住所：住所2,登録住所：電話番号,登録住所：FAX番号,登録住所：部署名,登録住所：更新日,登録住所：備考1,登録住所：備考2,送付先1：請求書,送付先1：保証書,送付先1：瑕疵保証書,送付先1：定期刊行,送付先1：調査報告書,送付先1：工事報告書,送付先1：検査報告書,送付先1：郵便番号,送付先1：住所1,送付先1：住所2,送付先1：電話番号,送付先1：FAX番号,送付先1：部署名,送付先1：代表者名,送付先1：更新日,送付先1：備考,送付先2：請求書,送付先2：保証書,送付先2：瑕疵保証書,送付先2：定期刊行,送付先2：調査報告書,送付先2：工事報告書,送付先2：検査報告書,送付先2：郵便番号,送付先2：住所1,送付先2：住所2,送付先2：電話番号,送付先2：FAX番号,送付先2：部署名,送付先2：代表者名,送付先2：更新日,送付先2：備考,送付先3：請求書,送付先3：保証書,送付先3：瑕疵保証書,送付先3：定期刊行,送付先3：調査報告書,送付先3：工事報告書,送付先3：検査報告書,送付先3：郵便番号,送付先3：住所1,送付先3：住所2,送付先3：電話番号,送付先3：FAX番号,送付先3：部署名,送付先3：代表者名,送付先3：更新日,送付先3：備考,送付先4：請求書,送付先4：保証書,送付先4：瑕疵保証書,送付先4：定期刊行,送付先4：調査報告書,送付先4：工事報告書,送付先4：検査報告書,送付先4：郵便番号,送付先4：住所1,送付先4：住所2,送付先4：電話番号,送付先4：FAX番号,送付先4：部署名,送付先4：代表者名,送付先4：更新日,送付先4：備考"
    ''' <summary>
    ''' "加盟店情報一括取込情報CSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conKameitenInfoIttukatuCsvHeader As String = "EDI情報作成日,区分,加盟店ｺｰﾄﾞ,取消,発注停止フラグ,加盟店名1,店名ｶﾅ1,加盟店名2,店名ｶﾅ2,ﾋﾞﾙﾀﾞｰNO,ﾋﾞﾙﾀﾞｰ名,系列ｺｰﾄﾞ,系列名,営業所ｺｰﾄﾞ,営業所名,正式名称,正式名称2,都道府県ｺｰﾄﾞ,都道府県名,年間棟数,付保証明FLG,付保証明書開始年月,営業担当者,営業担当者名,旧営業担当者,旧営業担当者名,工事売上種別,工事売上種別名,JIO先フラグ,解約払戻価格,棟区分1商品ｺｰﾄﾞ,棟区分1商品名,棟区分2商品ｺｰﾄﾞ,棟区分2商品名,棟区分3商品ｺｰﾄﾞ,棟区分3商品名,調査請求先 区分,調査請求先コード,調査請求先枝番,調査請求先名,工事請求先 区分,工事請求先コード,工事請求先枝番,工事請求先名,販促品請求先 区分,販促品請求先コード,販促品請求先枝番,販促品請求先名,建物検査請求先 区分,建物検査請求先コード,建物検査請求先枝番,建物検査請求先名,請求先区分5,請求先コード5,請求先枝番5,請求先5名,請求先区分6,請求先コード6,請求先枝番6,請求先6名,請求先区分7,請求先コード7,請求先枝番7,請求先7名,保証期間,保証書発行有無,入金確認条件,入金確認覚書,調査見積書FLG,発注書FLG,郵便番号,住所1,住所2,所在地コード,所在地名,部署名,代表者名,電話番号,FAX番号,申込担当者,備考1,備考2,登録日,請求有無,商品コード,商品名,売上金額,工務店請求金額,請求書発行日,売上年月日,備考,加盟店マスタ_更新日時,多棟割引マスタ_更新日時1,多棟割引マスタ_更新日時2,多棟割引マスタ_更新日時3,加盟店住所マスタ_更新日時,追加_備考種別①,追加_備考種別①名,追加_備考種別②,追加_備考種別②名,追加_備考種別③,追加_備考種別③名,追加_備考種別④,追加_備考種別④名,追加_備考種別⑤,追加_備考種別⑤名,追加_内容①,追加_内容②,追加_内容③,追加_内容④,追加_内容⑤"
    ''' <summary>
    ''' "加盟店情報一括取込情報ErrorCSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conKameitenInfoIttukatuErrCsvHeader As String = "EDI情報作成日,行NO,処理日時,区分,加盟店ｺｰﾄﾞ,取消,発注停止フラグ,加盟店名1,店名ｶﾅ1,加盟店名2,店名ｶﾅ2,ﾋﾞﾙﾀﾞｰNO,ﾋﾞﾙﾀﾞｰ名,系列ｺｰﾄﾞ,系列名,営業所ｺｰﾄﾞ,営業所名,正式名称,正式名称2,都道府県ｺｰﾄﾞ,都道府県名,年間棟数,付保証明FLG,付保証明書開始年月,営業担当者,営業担当者名,旧営業担当者,旧営業担当者名,工事売上種別,工事売上種別名,JIO先フラグ,解約払戻価格,棟区分1商品ｺｰﾄﾞ,棟区分1商品名,棟区分2商品ｺｰﾄﾞ,棟区分2商品名,棟区分3商品ｺｰﾄﾞ,棟区分3商品名,調査請求先 区分,調査請求先コード,調査請求先枝番,調査請求先名,工事請求先 区分,工事請求先コード,工事請求先枝番,工事請求先名,販促品請求先 区分,販促品請求先コード,販促品請求先枝番,販促品請求先名,建物検査請求先 区分,建物検査請求先コード,建物検査請求先枝番,建物検査請求先名,請求先区分5,請求先コード5,請求先枝番5,請求先5名,請求先区分6,請求先コード6,請求先枝番6,請求先6名,請求先区分7,請求先コード7,請求先枝番7,請求先7名,保証期間,保証書発行有無,入金確認条件,入金確認覚書,調査見積書FLG,発注書FLG,郵便番号,住所1,住所2,所在地コード,所在地名,部署名,代表者名,電話番号,FAX番号,申込担当者,備考1,備考2,登録日,請求有無,商品コード,商品名,売上金額,工務店請求金額,請求書発行日,売上年月日,備考,加盟店マスタ_更新日時,多棟割引マスタ_更新日時1,多棟割引マスタ_更新日時2,多棟割引マスタ_更新日時3,加盟店住所マスタ_更新日時,追加_備考種別①,追加_備考種別①名,追加_備考種別②,追加_備考種別②名,追加_備考種別③,追加_備考種別③名,追加_備考種別④,追加_備考種別④名,追加_備考種別⑤,追加_備考種別⑤名,追加_内容①,追加_内容②,追加_内容③,追加_内容④,追加_内容⑤,登録ログインユーザID,登録日時,更新ログインユーザID,更新日時"

    ''' <summary>
    ''' "物件情報CSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conBukkenJyouhouCsvHeader As String = "破棄種別,区分,番号,施主名,依頼日,加盟店コード,加盟店名,調査連絡先,物件名寄コード,依頼担当者,都道府県名,担当営業ID,物件住所1,物件住所2,物件住所3,備考,調査会社コード,調査会社事業所コード,調査会社名,調査方法,調査希望日,調査希望時間,承諾書調査日,調査実施日,調査結果,計画書作成日,保証書発行依頼書着日,保証書発行状況,保証書発行日,工事会社コード,工事会社事業所コード,工事会社名,追加工事会社コード,追加工事会社事業所コード,追加工事会社名,仕様確認確認有無,工事仕様確認日,改良工事完了予定日,改良工事日,追加工事予定日,追加工事実施日,調査売上年月日,調査売上金額（1+2-解約）,調査請求書発行日,調査入金金額,調査発注書金額,調査発注書確認日,改良工事売上年月日,改良工事売上金額,改良工事請求日,工事入金金額,工事発注書金額,工事発注書確認日,追加工事売上年月日,追加工事売上金額,追加工事請求日,追加工事入金金額,追加工事発注書金額,追加工事発注書確認日"
    ''' <summary>
    ''' "物件情報CSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const conYosinCsvHeader As String = "加盟店コード,加盟店名,県,物件NO,施主名,種別,予・実,売上日,予定日,売上金額,商品名,依頼日"

    ''' <summary>
    ''' "EXCEL仕訳売上CSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/13 馬艶軍</history>
    Public Const conExcelSiwakeUriageCsvHeader As String = "摘要,借方_税区分,借方_科目名,借方_科目,借方_細目,借方_形式,借方_用途,借方_付替先,借方_ﾗｲﾝ,借方_相手先,借方_金額,貸方_税区分,貸方_科目名,貸方_科目,貸方_細目,貸方_形式,貸方_用途,貸方_付替先,貸方_ﾗｲﾝ,貸方_相手先,貸方_金額"

    ''' <summary>
    ''' "EXCEL仕訳仕入CSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/13 馬艶軍</history>
    Public Const conExcelSiwakeSiireCsvHeader As String = "摘要,借方_税区分,借方_科目名,借方_科目,借方_細目,借方_形式,借方_用途,借方_付替先,借方_ﾗｲﾝ,借方_相手先,借方_金額,貸方_税区分,貸方_科目名,貸方_科目,貸方_細目,貸方_形式,貸方_用途,貸方_付替先,貸方_ﾗｲﾝ,貸方_相手先,貸方_金額"

    ''' <summary>
    ''' "EXCEL仕訳入金CSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/13 馬艶軍</history>
    Public Const conExcelSiwakeNyuukinCsvHeader As String = "摘要,借方_税区分,借方_科目名,借方_科目,借方_細目,借方_形式,借方_用途,借方_付替先,借方_ﾗｲﾝ,借方_相手先,借方_金額,貸方_税区分,貸方_科目名,貸方_科目,貸方_細目,貸方_形式,貸方_用途,貸方_付替先,貸方_ﾗｲﾝ,貸方_相手先,貸方_金額"

    ''' <summary>
    ''' "売掛金残高表"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/13 馬艶軍</history>
    Public Const conUrikakekinZandakaHyouCsvHeader As String = "データ区分,得意先コード,得意先名１,得意先名２,繰越残高,現金・振込,手形,相殺他,入金合計,未回収残高,売上高,消費税等,差引残高,手形残高,売上債権"

    ''' <summary>
    ''' "請求先マスタCSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/12 車龍(大連情報システム部)　新規作成</history>
    Public Const conM_seikyuu_sakiCsvHeader As String = "得意先コード,得意先名1,得意先名2,先方担当者名,メールアドレス,マスター区分,請求先コード,実績管理,住所1,住所2,郵便番号,電話番号,入金口座番号,得意先区分1,得意先区分2,得意先区分3,適用売価[売価No],適用売価[掛率],適用売価[税換算],主担当者コード,請求締日,消費税端数,消費税通知,回収種別1,回収種別境界額,回収種別2,回収予定日,回収方法,与信限度額,繰越残高,納品書用紙,納品書社名,請求書用紙,請求書社名,官公庁区分,敬称,社店コード,取引先コード"

    ''' <summary>
    ''' "調査会社マスタCSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/13 車龍(大連情報システム部)　新規作成</history>
    Public Const conM_tyousakaisyaCsvHeader As String = "調査会社コード,事業所コード,取消,調査会社名,調査会社名カナ,請求先支払先名,請求先支払先名カナ,住所1,住所2,郵便番号,電話番号,FAX番号,PCA用仕入先コード,PCA請求先コード,請求先コード,請求先枝番,請求先区分,請求締め日,請求書送付先住所1,請求書送付先住所2,請求書送付先郵便番号,請求書送付先電話番号,新会計支払先コード,新会計事業所コード,支払明細集計先事業所コード,支払集計先事業所コード,支払締め日,支払予定月数,ファクタリング開始年月,支払用FAX番号,SS基準価格,FC店コード,検査センターコード,工事報告書直送,工事報告書直送変更ログインユーザーID,工事報告書直送変更日時,調査会社フラグ,工事会社フラグ,JAPAN会区分,JAPAN会入会年月,JAPAN会退会年月,FC店区分,FC入会年月,FC退会年月,取消理由,ReportJHSトークン有無フラグ,宅地地盤調査主任資格有無フラグ"

    ''' <summary>
    ''' "商品マスタCSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/13 車龍(大連情報システム部)　新規作成</history>
    Public Const conM_syouhinCsvHeader As String = "商品コード,品名,システム区分,マスター区分,在庫管理,実績管理,単位名,入数,規格・型番,色,サイズ,商品区分１,商品区分２,商品区分３,税区分,税込区分,小数桁[単価],小数桁[数量],標準価格,原価,売価１,売価２,売価３,売価４,売価５,倉庫コード,主仕入先コード,在庫単価,仕入単価"

    ''' <summary>
    ''' "銀行マスタCSVヘッダ"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/14 車龍(大連情報システム部)　新規作成</history>
    Public Const conM_ginkouCsvHeader As String = "銀行コード,銀行名,支店コード,支店名,最新フラグ"

    ''' <summary>
    ''' "売掛金残高表"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/07/30 趙東莉</history>
    Public Const conKaikakekinZandakaHyouCsvHeader As String = "データ区分,支払先コード,支払先名１,支払先名２,繰越残高,振込,相殺,支払合計,未払残高,仕入等,消費税等,差引残高"

    ''' <summary>
    ''' "調査見積作成用データ"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/09/25 馬艶軍</history>
    Public Const conTyousaMitumoriCsvHeader As String = "区分,物件No,施主名,加盟店コード,加盟店名,依頼担当者,物件住所1,物件住所2,物件住所3,分類コード,商品コード,商品名,数量,単位,単価,金額,消費税"

    ''' <summary>
    ''' "原価データ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2011/03/10 車龍</history>
    Public Const conGenkaCsvHeader As String = "EDI情報作成日,調査会社コード,事業所コード,調査会社名,相手先種別,相手先コード,相手先名,商品コード,商品名,調査方法NO,調査方法,取消,棟価格1,棟価格変更FLG1,棟価格2,棟価格変更FLG2,棟価格3,棟価格変更FLG3,棟価格4,棟価格変更FLG4,棟価格5,棟価格変更FLG5,棟価格6,棟価格変更FLG6,棟価格7,棟価格変更FLG7,棟価格8,棟価格変更FLG8,棟価格9,棟価格変更FLG9,棟価格10,棟価格変更FLG10,棟価格11～19,棟価格変更FLG11～19,棟価格20～29,棟価格変更FLG20～29,棟価格30～39,棟価格変更FLG30～39,棟価格40～49,棟価格変更FLG40～49,棟価格50～,棟価格変更FLG50～"

    ''' <summary>
    ''' "原価エラーデータ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2011/03/02 車龍</history>
    Public Const conGenkaErrCsvHeader As String = "EDI情報作成日,行NO,処理日時,調査会社コード,事業所コード,調査会社名,相手先種別,相手先コード,相手先名,商品コード,商品名,調査方法NO,調査方法,取消,棟価格1,棟価格変更FLG1,棟価格2,棟価格変更FLG2,棟価格3,棟価格変更FLG3,棟価格4,棟価格変更FLG4,棟価格5,棟価格変更FLG5,棟価格6,棟価格変更FLG6,棟価格7,棟価格変更FLG7,棟価格8,棟価格変更FLG8,棟価格9,棟価格変更FLG9,棟価格10,棟価格変更FLG10,棟価格11～19,棟価格変更FLG11～19,棟価格20～29,棟価格変更FLG20～29,棟価格30～39,棟価格変更FLG30～39,棟価格40～49,棟価格変更FLG40～49,棟価格50～,棟価格変更FLG50～,登録日時,登録ログインユーザID,更新日時,更新ログインユーザID"

    ''' <summary>
    ''' "販売価格データ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2011/03/02 呉営</history>
    Public Const conHanbaiKakakuCsvHeader As String = "EDI情報作成日,相手先種別,相手先コード,相手先名,商品コード,商品名,調査方法NO,調査方法,取消,工務店請求金額,工務店請求金額変更FLG,実請求金額,実請求金額変更FLG,公開フラグ"

    ''' <summary>
    ''' "販売価格エラーデータ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2011/03/02 呉営</history>
    Public Const conHanbaiKakakuErrCsvHeader As String = "EDI情報作成日,行NO,処理日時,相手先種別,相手先コード,相手先名,商品コード,商品名,調査方法NO,調査方法,取消,工務店請求金額,工務店請求金額変更FLG,実請求金額,実請求金額変更FLG,公開フラグ,登録ログインユーザID,登録日時,更新ログインユーザID,更新日時"

    ''' <summary>
    ''' "加盟店商品調査方法特別対応作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2011/03/14 ジン登閣</history>
    Public Const conTokubetuTaiouCsvHeader As String = "EDI情報作成日,相手先種別,相手先コード,相手先名,商品コード,商品名,調査方法NO,調査方法,特別対応コード,特別対応名称,取消,金額加算商品コード,初期値,実請求加算金額,工務店請求加算金額"

    ''' <summary>
    ''' "加盟店商品調査方法特別対応エラーデータ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2011/03/14 ジン登閣</history>
    Public Const conTokubetuTaiouErrCsvHeader As String = "EDI情報作成日,行NO,処理日時,相手先種別,相手先コード,相手先名,商品コード,商品名,調査方法NO,調査方法,特別対応コード,特別対応名称,取消,金額加算商品コード,金額加算商品名,初期値,実請求加算金額,工務店請求加算金額,登録ログインユーザーID,登録日時,更新ログインユーザーID,更新日時"

    ''' <summary>
    ''' "請求先注意事項データ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2011/06/16 車龍</history>
    Public Const conSeikyuuSakiTyuuijikouCsvHeader As String = "請求先ｺｰﾄﾞ,枝番,区分,請求先名,入力№,取消,種別コード+名称,詳細,重要度,請求締め日,請求書必着日,登録ID,登録日時,更新ID,更新日時"

    ''' <summary>
    ''' "工事価格データ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>高</history>
    Public Const conKojKakakuCsvHeader As String = "EDI情報作成日,相手先種別,相手先コード,相手先名,商品コード,商品名,工事会社コード,工事会社名称,取消,売上金額,工事会社請求有無,請求有無"

    ''' <summary>
    ''' "工事価格エラーデータ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2011/03/02 呉営</history>
    Public Const conKojKakakuErrCsvHeader As String = "EDI情報作成日,行NO,処理日時,相手先種別,相手先コード,相手先名,商品コード,商品名,工事会社コード,工事会社名,取消,売上金額,工事会社請求有無,請求有無,登録ログインユーザID,登録日時,更新ログインユーザID,更新日時"

    ''' <summary>
    ''' "汎用仕入エラーデータ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/01/12 陳琳</history>
    Public Const conHanyouSiireErrCsvHeader As String = "EDI情報作成日,行NO,処理日時,取消,加盟店コード,調査会社コード,調査会社事業所コード,商品コード,数量,単価,税区分,消費税額,仕入年月日,伝票仕入年月日,仕入処理FLG,仕入処理日,区分,番号,施主名,摘要,登録ログインユーザID,登録ログインユーザー名,登録日時,更新ログインユーザID,更新ログインユーザ名,更新日時"

    ''' <summary>
    ''' "汎用仕入エラーデータ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/01/12 陳琳</history>
    Public Const conHanyouUriageErrCsvHeader As String = "EDI情報作成日,行NO,処理日時,取消,売上店区分,売上店コード,請求先区分,請求先コード,請求先枝番,商品コード,品名,数量,単価,社内原価,税区分,消費税額,売上年月日,請求年月日,伝票売上年月日修正,売上処理FLG(売上計上FLG),売上処理日(売上計上日),区分,番号,施主名,摘要,登録ログインユーザID,登録ログインユーザー名,登録日時,更新ログインユーザID,更新ログインユーザ名,更新日時"
    '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓========================================
    'Public Const conHanyouUriageErrCsvHeader As String = "EDI情報作成日,行NO,処理日時,取消,請求先区分,請求先コード,請求先枝番,商品コード,品名,数量,単価,社内原価,税区分,消費税額,売上年月日,請求年月日,伝票売上年月日修正,売上処理FLG(売上計上FLG),売上処理日(売上計上日),区分,番号,施主名,摘要,登録ログインユーザID,登録ログインユーザー名,登録日時,更新ログインユーザID,更新ログインユーザ名,更新日時"
    '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑========================================


    ''' <summary>
    ''' "営業所CSV出力用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 車龍</history>
    Public Const conEigyousyoCsvHeader As String = "ﾃﾘﾄﾘｰ,AF,FC加入,法人,加盟店名,法人名,役職名,代表者名,郵便番号,所在地,TEL,FAX,入力日,請求書発行日,商品ｺｰﾄﾞ,商品名,工務店請求税抜金額,実請求税抜金額,消費税,税込金額,単価,数,請求先ｺｰﾄﾞ,請求先名"

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
    ''' "検査報告書管理データ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2015/12/08 高兵兵</history>
    Public Const conKensahoukokushoCsvHeader As String = "取消,管理No,区分,保証書NO,施主名,加盟店コード,加盟店名,格納日,発送日,検査報告書発行部数,検査報告書送付先住所1,検査報告書送付先住所2,郵便番号,電話番号,部署名,都道府県コード,都道府県名,発送日入力フラグ,送付担当者,物件住所1,物件住所2,物件住所3,建物構造,建物階数,FC名,依頼担当者名,建物加盟店コード,管理表出力フラグ,管理表出力日,送付状出力フラグ,送付状出力日,検査報告書出力フラグ,検査報告書出力日,通しNo,検査工程名1,検査工程名2,検査工程名3,検査工程名4,検査工程名5,検査工程名6,検査工程名7,検査工程名8,検査工程名9,検査工程名10,検査実施日1,検査実施日2,検査実施日3,検査実施日4,検査実施日5,検査実施日6,検査実施日7,検査実施日8,検査実施日9,検査実施日10,検査員名1,検査員名2,検査員名3,検査員名4,検査員名5,検査員名6,検査員名7,検査員名8,検査員名9,検査員名10,登録ログインユーザID,登録日時,更新ログインユーザID,更新日時"

    ''' <summary>
    ''' "検査報告書管理データ作成用"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2015/12/15 高兵兵</history>
    Public Const conKensahoukokushoOutputCsvHeader As String = "管理No,区分,保証書NO,施主名,加盟店コード,加盟店名,検査報告書発行部数,格納日,発送日,管理表出力フラグ,送付状出力フラグ,検査報告書出力フラグ"

End Class
