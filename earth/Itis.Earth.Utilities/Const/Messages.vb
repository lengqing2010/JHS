''' <summary>
''' EARTHで使用するメッセージを管理するクラスです
''' 使用する場合は Messages.MSGXXXX と指定
''' </summary>
''' <remarks>
''' 採番ルール MSG＋[000～999]連番3桁＋用途　<br/>
''' 用途（ほぼエラーメインと思われるので連番は一意で管理します）<br/>
'''   C - 確認<br/>
'''   S - 標準<br/>
'''   W - 警告<br/>
'''   E - エラー<br/>
''' </remarks>
Public Class Messages

    ''' <summary>
    ''' マスターにありません。経理に追加の連絡をして下さい。
    ''' </summary>
    Public Const MSG001E As String = "マスターにありません。経理に追加の連絡をして下さい。"
    ''' <summary>
    ''' 保証書NO年月が未登録です
    ''' </summary>
    Public Const MSG002E As String = "保証書NO年月が未登録です。"
    ''' <summary>
    ''' 他の端末で更新された為キャンセルされました。再度読込みを行い内容を御確認下さい。\r\n
    '''   ■更新ユーザーID：[{0}]\r\n
    '''   ■更新日時：[{1}]\r\n
    '''   ■対象：[{2}]
    ''' </summary>
    Public Const MSG003E As String = "他の端末で更新された為キャンセルされました。再度読込みを行い内容を御確認下さい。\r\n" & _
                                     "  ■更新ユーザーID：[{0}]\r\n  ■更新日時：[{1}]\r\n  ■対象：[{2}]"

    ''' <summary>
    ''' "区分が選択されていません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG004E As String = "区分が選択されていません。"

    ''' <summary>
    ''' "既に保証書NOは発行済みです。追加で発行しますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG005C As String = "既に保証書NOは発行済みです。追加で発行しますか？"

    ''' <summary>
    ''' "「区分」は必須条件です。選択してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG006E As String = "「区分」は必須条件です。選択してください。"

    ''' <summary>
    ''' "検索上限件数に「無制限」が選択されています。\r\n
    '''  画面表示に時間が掛かる可能性がありますが、実行してよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG007C As String = "検索上限件数に「無制限」が選択されています。\r\n" & _
                                     "画面表示に時間が掛かる可能性がありますが、実行してよろしいですか？"

    ''' <summary>
    ''' "項目に条件を入力するか、\r\n「対象範囲：全て」もしくは「全区分」のチェックをはずしてください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG008E As String = "項目に条件を入力するか、\r\n「対象範囲：全て」もしくは「全区分」のチェックをはずしてください。"

    ''' <summary>
    ''' "指定した商品コードは存在しません。検索画面より商品を選択するか、存在する商品コードを入力してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG009E As String = "指定した商品コードは存在しません。検索画面より商品を選択するか、存在する商品コードを入力してください。"

    ''' <summary>
    ''' "発注書金額が入力されているので、商品コードを削除できません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG010E As String = "発注書金額が入力されているので、商品コードを削除できません。"

    ''' <summary>
    ''' "発注書金額が違いますが、「確定」にしてよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG011C As String = "発注書金額が違いますが、「確定」にしてよろしいですか？"

    ''' <summary>
    ''' "発注書金額が同じなので、「確定」にしてよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG012C As String = "発注書金額が同じなので、「確定」にしてよろしいですか？"

    ''' <summary>
    ''' "@PARAM1は必須です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG013E As String = "@PARAM1は必須です。\r\n"

    ''' <summary>
    ''' "@PARAM1の値が有効日付範囲内にありません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG014E As String = "@PARAM1の値が有効日付範囲内にありません。\r\n"

    ''' <summary>
    ''' "@PARAM1に使用できない文字列が入力されています。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG015E As String = "@PARAM1に使用できない文字列が入力されています。\r\n"

    ''' <summary>
    ''' "@PARAM1の文字数が多すぎます。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG016E As String = "@PARAM1の文字数が多すぎます。\r\n"

    ''' <summary>
    ''' "重複物件の確認を行ってください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG017E As String = "重複物件の確認を行ってください。\r\n"

    ''' <summary>
    ''' "処理を実行します。よろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG017C As String = "処理を実行します。よろしいですか？"

    ''' <summary>
    ''' "@PARAM1処理が完了しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG018S As String = "@PARAM1処理が完了しました。"

    ''' <summary>
    ''' "@PARAM1処理が失敗しました。\r\n\r\n直前の入力内容を表示しますので、確認してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG019E As String = "@PARAM1処理が失敗しました。\r\n\r\n直前の入力内容を表示しますので、確認してください。"

    ''' <summary>
    ''' "該当するデータが存在しません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG020E As String = "該当するデータが存在しません。"

    ''' <summary>
    ''' "セッション情報が取得できませんでした。タイムアウトになった可能性があります。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG021E As String = "セッション情報が取得できませんでした。タイムアウトになった可能性があります。"

    ''' <summary>
    ''' "@PARAM1の大小が不適切です。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG022E As String = "@PARAM1の大小が不適切です。\r\n"

    ''' <summary>
    ''' "引き続き、調査予定連絡書を表示します。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG023S As String = "引き続き、調査予定連絡書を表示します。"

    ''' <summary>
    ''' MSG021E ＆ "\r\n\r\n「依頼内容」「商品１／商品２」の編集内容が失われています。再度入力してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG024E As String = MSG021E & "\r\n\r\n「依頼内容」「商品１／商品２」の編集内容が失われています。再度入力してください。"

    ''' <summary>
    ''' MSG021E ＆ "\r\n\r\n「依頼内容」「商品１／商品２」の編集内容が失われています。データの再読込を行います。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG025E As String = MSG021E & "\r\n\r\n「依頼内容」「商品１／商品２」の編集内容が失われています。データの再読込を行います。"

    ''' <summary>
    ''' "@PARAM1か@PARAM2の何れかは必須条件です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG026E As String = "@PARAM1か@PARAM2の何れかは必須条件です。"

    ''' <summary>
    ''' "@PARAM1は整数@PARAM2桁、小数@PARAM3桁まで入力可能です。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG027E As String = "@PARAM1は整数@PARAM2桁、小数@PARAM3桁まで入力可能です。\r\n"

    ''' <summary>
    ''' "依頼内容を確定しないと画面遷移できません\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG028E As String = "【 依頼内容を確定しないと画面遷移できません 】\r\n\r\n"

    ''' <summary>
    ''' "商品区分：リフォーム が設定されております。\r\n処理を継続してよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG029C As String = "商品区分：リフォーム が設定されております。\r\n処理を継続してよろしいですか？"

    ''' <summary>
    ''' "@PARAM1が変更されています。「検索」ボタンを押下し、情報を再取得してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG030E As String = "@PARAM1が変更されています。「検索」ボタンを押下し、情報を再取得してください。\r\n"

    ''' <summary>
    ''' "区分、番号を入力してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG031E As String = "区分、番号を入力してください。"

    ''' <summary>
    ''' "指定された条件では、商品１を設定できません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG032E As String = "指定された条件では、商品１を設定できません。\r\n"

    ''' <summary>
    ''' "From または To または message が 未設定です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG033E As String = "From または To または message が 未設定です。"

    ''' <summary>
    ''' "To、CC、および Bcc に受信者が指定されていません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG034E As String = "To、CC、および Bcc に受信者が指定されていません。"

    ''' <summary>
    ''' "この SmtpClient は、SendAsync の呼び出し中 または Host,Port が未設定です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG035E As String = "この SmtpClient は、SendAsync の呼び出し中 または Host,Port が未設定です。"

    ''' <summary>
    ''' "このオブジェクトは破棄されています。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG036E As String = "このオブジェクトは破棄されています。"

    ''' <summary>
    ''' "SMTP サーバーへの接続に失敗、または 認証に失敗、または操作がタイムアウトしました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG037E As String = "SMTP サーバーへの接続に失敗、または 認証に失敗、または操作がタイムアウトしました。"

    ''' <summary>
    ''' "To、CC、または Bcc 内の 1 人以上の受信者に、メッセージを配信できませんでした。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG038E As String = "To、CC、または Bcc 内の 1 人以上の受信者に、メッセージを配信できませんでした。"

    ''' <summary>
    ''' "To、CC、または Bcc 内の 1 人以上の受信者に、メッセージを配信できませんでした。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG039E As String = "区分または番号が存在しません。メイン画面を表示します。"

    ''' <summary>
    ''' "@PARAM1の入力が不正です"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG040E As String = "@PARAM1の入力が不正です。\r\n"

    ''' <summary>
    ''' "範囲指定に矛盾があります"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG041E As String = "範囲指定に矛盾があります。\r\n"

    ''' <summary>
    ''' "@PARAM1を実行します。宜しいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG042C As String = "@PARAM1を実行します。宜しいですか？"

    ''' <summary>
    ''' "@PARAM1が終了しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG043S As String = "@PARAM1が終了しました。"

    ''' <summary>
    ''' "修正するデータが存在しません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG044S As String = "修正するデータが存在しません。"

    ''' <summary>
    ''' "発注書金額が同じなので、発注書を「確定」にしました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG045C As String = "発注書金額が同じなので、発注書を「確定」にしました。"

    ''' <summary>
    ''' "発注書金額が違いますが、発注書を「確定」にしました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG046C As String = "発注書金額が違いますが、発注書を「確定」にしました。"

    ''' <summary>
    ''' "{0}件選択されています。\r\n{1}を行える件数は{2}件までとなっております。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG047E As String = "{0}件選択されています。\r\n{1}を行える件数は{2}件までとなっております。"

    ''' <summary>
    ''' "一括入金処理を行います。宜しいですか？\r\n請求総額：@PARAM1"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG048C As String = "一括入金処理を行います。宜しいですか？\r\n請求総額：@PARAM1"

    ''' <summary>
    ''' "既に一括入金処理されたデータが存在します。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG049E As String = "既に一括入金処理されたデータが存在します。"

    ''' <summary>
    ''' "一括入金処理は完了しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG050S As String = "一括入金処理は完了しました。"

    ''' <summary>
    ''' "請求総額に変更が加わりました。\r\n再度、処理を実行してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG051E As String = "請求総額に変更が加わりました。\r\n再度、処理を実行してください。"

    ''' <summary>
    ''' "JIO工事の改良工事は、ビルダー請求です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG052E As String = "JIO工事の改良工事は、ビルダー請求です。"

    ''' <summary>
    ''' "解約払戻し対象外です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG053E As String = "解約払戻し対象外です。"

    ''' <summary>
    ''' "保証書発行状況、保証開始日、保険会社、保険申請、保険申請月を上書きしました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG054S As String = "保証書発行状況、保証開始日、保険会社、保険申請、保険申請月を上書きしました。"

    ''' <summary>
    ''' "ファイル：「@PARAM1」から\r\n入金テーブルへ\r\nインポートを行ないますが宜しいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG055C As String = "ファイル：「@PARAM1」から\r\n入金テーブルへ\r\nインポートを行ないますが宜しいですか？"
    ''' <summary>
    ''' "ファイルを指定してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG056E As String = "ファイルを指定してください。"
    ''' <summary>
    ''' "ファイルの容量が 0 バイトです。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG057E As String = "ファイルの容量が 0 バイトです。"
    ''' <summary>
    ''' 取り込みファイルの形式が不正の為、データを取得できませんでした。\r\n
    ''' ファイルの内容を確認してください。\r\n
    ''' ■エラー発生箇所：{0} 行目
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG058E As String = "取り込みファイルの形式が不正の為、データを取得できませんでした。\r\n" & _
                                     "ファイルの内容を確認してください。\r\n■エラー発生箇所：{0} 行目"
    ''' <summary>
    ''' "指定されたファイルは既に取込まれております。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG059E As String = "指定されたファイルは既に取込まれております。"
    ''' <summary>
    ''' "入金データ取り込み処理は完了しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG060S As String = "入金データ取り込み処理は完了しました。"
    ''' <summary>
    ''' "\r\n※入金データ取り込みエラーが発生しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG061W As String = "\r\n※入金データ取り込みエラーが発生しました。"
    ''' <summary>
    ''' "@PARAM1は結果入力時の必須入力です。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG061E As String = "@PARAM1は結果入力時の必須入力です。\r\n"
    ''' <summary>
    ''' "@PARAM1を入力してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG062E As String = "@PARAM1を入力してください。\r\n"
    ''' <summary>
    ''' "保証書が既に発行されています。判定結果を変更しますか？\r\n元の判定ｺｰﾄﾞ1：（「@PARAM1」）\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG063C As String = "保証書が既に発行されています。判定結果を変更しますか？\r\n元の判定ｺｰﾄﾞ1：（「@PARAM1」）\r\n"
    ''' <summary>
    ''' データベースへの反映中にエラーが発生しました。\r\n
    ''' 以下の情報をシステム管理者へお知らせ下さい\r\n
    ''' ■処理内容　　：{0}\r\n
    ''' ■処理テーブル：{1}\r\n
    ''' ■キー情報　　：{2}
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG064E As String = "データベースへの反映中にエラーが発生しました。\r\n" & _
                                     "以下の情報をシステム管理者へお知らせ下さい\r\n" & _
                                     "■処理内容　　：{0}\r\n■処理テーブル：{1}\r\n■キー情報　　：{2}"
    ''' <summary>
    ''' "共通情報が未入力です。\r\n受注画面にて入力して下さい。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG065W As String = "共通情報が未入力です。\r\n受注画面にて入力して下さい。"
    ''' <summary>
    ''' "請求書発行日が@PARAM1より古い日付ですが\r\n請求書発行日を登録できるようにしますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG066C As String = "請求書発行日が@PARAM1より古い日付ですが\r\n請求書発行日を登録できるようにしますか？"
    ''' <summary>
    ''' "指定された物件は改良工事売上処理済のため、コピーできません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG067E As String = "指定された物件は改良工事売上処理済のため、コピーできません。"
    ''' <summary>
    ''' "コピー元と同じ物件が指定されています。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG068E As String = "コピー元と同じ物件が指定されています。\r\n別の番号を指定してください。"
    ''' <summary>
    ''' "コピー先の項目に、既に情報が登録されています。\r\nコピーを実行してよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG069C As String = "コピー先の項目に、既に情報が登録されています。\r\nコピーを実行してよろしいですか？"
    ''' <summary>
    ''' "コピー先には、既に【完工速報着日】が登録されています。\r\n本当にコピーを実行してよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG070C As String = "コピー先には、既に【完工速報着日】が登録されています。\r\n本当にコピーを実行してよろしいですか？"
    ''' <summary>
    ''' "データ破棄種別が入力済みのため、修正できません。\r\n別の番号を指定してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG071E As String = "データ破棄種別が入力済みのため、修正できません。\r\n別の番号を指定してください。"

    ''' <summary>
    ''' "データ更新前に月次処理を忘れずに"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG072W As String = "データ更新前に月次処理を忘れずに"

    ''' <summary>
    ''' "データ更新前に決算月処理を忘れずに"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG073W As String = "データ更新前に決算月処理を忘れずに"

    ''' <summary>
    ''' "伝票番号を初期化します。宜しいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG074C As String = "伝票番号を初期化します。宜しいですか？"

    ''' <summary>
    ''' "既に他の人によって処理が行われた可能性があります。\r\n画面を読み込みなおして確認してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG075E As String = "既に他の人によって処理が行われた可能性があります。\r\n画面を読み込みなおして確認してください。"

    ''' <summary>
    ''' "まだダウンロードデータが作成されておりません。\r\n先にデータ作成を行ってください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG076E As String = "まだダウンロードデータが作成されておりません。\r\n先にデータ作成を行ってください。"

    ''' <summary>
    ''' "改良工事と追加工事のどちらかがJIO商品である場合、両方にJIO商品を指定する必要があります。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG077W As String = "改良工事と追加工事のどちらかがJIO商品である場合、両方にJIO商品を指定する必要があります。"

    ''' <summary>
    ''' "改良工事日が未入力ですが請求書発行日を登録できるようにしますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG078W As String = "改良工事日が未入力ですが請求書発行日を登録できるようにしますか？"

    ''' <summary>
    ''' 入力した工事種別が判定と違います。\r\n判定を変更するか、工事種別の再入力をお願いします。\r\n
    ''' ①判定を変更する場合は【OK】ボタンを押して報告書画面にて修正して下さい。\r\n
    ''' ②工事種別を再入力する場合は【キャンセル】ボタンを押して下さい。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG079C As String = "入力した工事種別が判定と違います。\r\n" & _
                                     "判定を変更するか、工事種別の再入力をお願いします。\r\n" & _
                                     "①判定を変更する場合は【OK】ボタンを押して報告書画面にて修正して下さい。\r\n" & _
                                     "②工事種別を再入力する場合は【キャンセル】ボタンを押して下さい。"

    ''' <summary>
    ''' "変更前の値に戻しますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG080C As String = "変更前の値に戻しますか？"

    ''' <summary>
    ''' "改良売上金額＜改良仕入金額ですが、そのまま登録しますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG081C As String = "改良売上金額＜改良仕入金額ですが、そのまま登録しますか？"

    ''' <summary>
    ''' "改良工事 完工速報着日を確定してもよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG082C As String = "改良工事 完工速報着日を確定してもよろしいですか？"

    ''' <summary>
    ''' "追加工事 完工速報着日を確定してもよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG083C As String = "追加工事 完工速報着日を確定してもよろしいですか？"

    ''' <summary>
    ''' "追加改良売上金額＜追加改良仕入金額ですが、そのまま登録しますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG084C As String = "追加改良売上金額＜追加改良仕入金額ですが、そのまま登録しますか？"

    ''' <summary>
    ''' "追加改良工事日が未入力ですが請求書発行日を登録できるようにしますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG085W As String = "追加改良工事日が未入力ですが請求書発行日を登録できるようにしますか？"

    ''' <summary>
    ''' 処理がタイムアウトしました。\r\n処理対象の範囲が広すぎる可能性があります。\r\n
    ''' 範囲を絞って再度実行するか、システム管理者へお知らせください。\r\n\r\n
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG086E As String = "処理がタイムアウトしました。\r\n処理対象の範囲が広すぎる可能性があります。\r\n" & _
                                     "範囲を絞って再度実行するか、システム管理者へお知らせください。\r\n\r\n"

    ''' <summary>
    ''' "同じ物件が指定されていますが、よろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG087C As String = "同じ物件が指定されていますが、よろしいですか？"

    ''' <summary>
    ''' "入力された日付（@PARAM1）に誤りがあります。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG088E As String = "入力された日付（@PARAM1）に誤りがあります。\r\n"

    ''' <summary>
    ''' "与信限度額を超えているため、登録できません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG089E As String = "与信限度額を超えているため、登録できません。"

    ''' <summary>
    ''' "与信チェック処理でエラーが発生しました。[コード：{0}]"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG090E As String = "与信チェック処理でエラーが発生しました。[コード：{0}]"

    ''' <summary>
    ''' "{0}にマイナス{1}を入力することはできません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG091E As String = "{0}にマイナス{1}を入力することはできません。\r\n"

    ''' <summary>
    ''' "{0}に登録できる桁数は、整数部 {1} 桁以内です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG092E As String = "{0}に登録できる桁数は、整数部 {1} 桁以内です。\r\n"

    ''' <summary>
    ''' "登録料および販促品初期ツール料を登録しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG093S As String = "登録料および販促品初期ツール料を登録しました。"

    ''' <summary>
    ''' "登録料を登録しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG094S As String = "登録料を登録しました。"

    ''' <summary>
    ''' "追加する商品を選んでください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG095E As String = "追加する商品を選んでください。"

    ''' <summary>
    ''' "調査請求書発行日が設定されていないため、解約できません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG096E As String = "調査請求書発行日が設定されていないため、解約できません。"

    ''' <summary>
    ''' "保証書発行状況、保証開始日、保険会社、保険申請、保険申請月が入力されています。\r\n上書きしますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG097C As String = "保証書発行状況、保証開始日、保険会社、保険申請、保険申請月が入力されています。\r\n上書きしますか？"

    ''' <summary>
    ''' "調査実施日が未入力ですが保証書発行日を登録できるようにしますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG098C As String = "調査実施日が未入力ですが保証書発行日を登録できるようにしますか？"

    ''' <summary>
    ''' "保証書発行日が調査実施日より古い日付ですが、\r\n保証書発行日を登録できるようにしますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG099C As String = "保証書発行日が調査実施日より古い日付ですが、\r\n保証書発行日を登録できるようにしますか？"

    ''' <summary>
    ''' "@PARAM1が未入力のため、@PARAM2は入力できません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG100E As String = "@PARAM1が未入力のため、@PARAM2は入力できません。\r\n"

    ''' <summary>
    ''' "改良工事がJIO商品である場合、\r\n工事会社コード＝999800[工事仕様]を指定することはできません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG101E As String = "改良工事がJIO商品である場合、\r\n工事会社コード＝999800[工事仕様]を指定することはできません。"

    ''' <summary>
    ''' "コピー元に商品が設定されていません。\r\n本当にコピーを実行してよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG102C As String = "コピー元に商品が設定されていません。\r\nコピーを実行してよろしいですか？"

    ''' <summary>
    ''' "禁止工事会社が選択されました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG103W As String = "禁止工事会社が選択されました。"

    ''' <summary>
    ''' "自動設定処理を実行中です。もう一度ボタンを押下してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG104E As String = "自動設定処理を実行中です。もう一度ボタンを押下してください。"

    ''' <summary>
    ''' "一日に登録できる件数は{0}件までです。\r\n日を改めて登録してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG105E As String = "一日に登録できる件数は{0}件までです。\r\n日を改めて登録してください。"

    ''' <summary>
    ''' "以下の項目に外字が含まれています。\r\n{0}\r\nプレビューを確認してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG106W As String = "以下の項目に外字が含まれています。\r\n{0}\r\nプレビューを確認してください。"

    ''' <summary>
    ''' 処理の競合や時間切れのため、データの更新が中止されました。\r\n
    ''' 暫く時間をおいて、再度実行してください。\r\n
    ''' code:[{0}]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG107E As String = "処理の競合や時間切れのため、データの更新が中止されました。\r\n" & _
                                     "暫く時間をおいて、再度実行してください。\r\ncode:[{0}]"

    ''' <summary>
    ''' "付保証明書発行ビルダー登録がありませんがよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG108E As String = "付保証明書発行ビルダー登録がありませんがよろしいですか？"

    ''' <summary>
    ''' "付保証明書発行ビルダー登録先ですが、よろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG109E As String = "付保証明書発行ビルダー登録先ですが、よろしいですか？"

    ''' <summary>
    ''' "番号が9999を超えたため初期化しましたが、\r\n地盤データが既に存在しているため新規登録できません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG110E As String = "番号が9999を超えたため初期化しましたが、\r\n地盤データが既に存在しているため新規登録できません。"

    ''' <summary>
    ''' "@PARAM1には@PARAM2～@PARAM3の範囲内で指定して下さい。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG111E As String = "@PARAM1には@PARAM2～@PARAM3の範囲内で指定して下さい。\r\n"

    ''' <summary>
    ''' "「@PARAM1」と入力されました。処理を進めてもよろしいですか？。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG112C As String = "「@PARAM1」と入力されました。処理を進めてもよろしいですか？\r\n"

    ''' <summary>
    ''' "「@PARAM1」の取得に失敗しました。\r\nシステム管理者へお知らせ下さい。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG113E As String = "「@PARAM1」の取得に失敗しました。\r\nシステム管理者へお知らせ下さい。"

    ''' <summary>
    ''' "JIO式は指定工事会社のみです。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG114E As String = "JIO式は指定工事会社のみです。\r\n"

    ''' <summary>
    ''' "保証なしですが商品を入力しますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG115C As String = "保証なしですが商品を入力しますか？"

    ''' <summary>
    ''' "処理がタイムアウトしました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG116E As String = "処理がタイムアウトしました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n"

    ''' <summary>
    ''' "データベース反映中にエラーが発生しました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG117E As String = "データベース反映中にエラーが発生しました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n"

    ''' <summary>
    ''' "処理中にエラーが発生しました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG118E As String = "処理中にエラーが発生しました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n"

    ''' <summary>
    ''' "@PARAM1を変更すると@PARAM2をクリアします。\r\n変更しますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG119C As String = "@PARAM1を変更すると@PARAM2をクリアします。\r\n変更しますか？"

    ''' <summary>
    ''' "@PARAM1と@PARAM2が一致しません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG120E As String = "@PARAM1と@PARAM2が一致しません。\r\n"

    ''' <summary>
    ''' 帳票出力処理中にエラーが発生しました。\r\n
    ''' システム管理者へお知らせ下さい。\r\n
    '''   エラーコード：{0}
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG121E As String = "帳票出力処理中にエラーが発生しました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n  エラーコード：{0}"

    ''' <summary>
    ''' "「@PARAM1」の取得に失敗しました。\r\n経理に追加の連絡をして下さい。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG122E As String = "「@PARAM1」の取得に失敗しました。\r\n経理に追加の連絡をして下さい。"

    ''' <summary>
    ''' "@PARAM1にチェックが入っています。\r\n@PARAM2は必須です。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG123E As String = "@PARAM1にチェックが入っています。\r\n@PARAM2は必須です。\r\n"

    ''' <summary>
    ''' {0}件の物件が選択されています。\r\n
    ''' 一括変更を行える物件数は{1}件までとなっております。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG124E As String = "{0}件の物件が選択されています。\r\n一括変更を行える物件数は{1}件までとなっております。"

    ''' <summary>
    ''' 物件が選択されていません。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG125E As String = "物件が選択されていません。"

    ''' <summary>
    ''' "{0}が空欄です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG126E As String = "{0}が空欄です。"

    ''' <summary>
    ''' 金額項目以外に変更が加えられています。\r\n再度、内容チェックボタンを押下してください。\r\n\r\n[対象]\r\n
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG127E As String = "金額項目以外に変更が加えられています。\r\n再度、内容チェックボタンを押下してください。\r\n\r\n[対象]\r\n"

    ''' <summary>
    ''' 一物件に対して登録できる商品2は４つまでとなっております。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG128E As String = "一物件に対して登録できる商品2は４つまでとなっております。"

    ''' <summary>
    ''' 計算チェックボックスにチェックが入っている商品があります。\r\n内容チェックボタンを押下するか、チェックをはずしてください。\r\n\r\n[対象]\r\n
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG129E As String = "計算チェックボックスにチェックが入っている商品があります。\r\n内容チェックボタンを押下するか、チェックをはずしてください。\r\n\r\n[対象]\r\n"

    ''' <summary>
    ''' "取消すると変更できなくなります。\r\nよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG130C As String = "取消すると変更できなくなります。\r\nよろしいですか？"

    ''' <summary>
    ''' 禁止調査会社が選択されています
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG131W As String = "禁止調査会社が選択されています"

    ''' <summary>
    ''' 直接請求の場合、工務店請求金額と実請求金額が等しくなければなりません。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG132E As String = "直接請求の場合、工務店請求金額と実請求金額が等しくなければなりません。\r\n"

    ''' <summary>
    ''' {0}の{1}が使用不可な為、{0}の物件がコピーされませんでした。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG133E As String = "{0}の{1}が使用不可な為、{0}の物件がコピーされませんでした。"

    ''' <summary>
    ''' {0}の物件が使用不可な為、{0}の物件が追加されませんでした。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG134E As String = "{0}の物件が使用不可な為、{0}の物件が追加されませんでした。"

    ''' <summary>
    ''' "指定された物件は発注書金額が入力済みであり、\r\nコピー元に商品が設定されていないため、コピーできません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG135E As String = "指定された物件は発注書金額が入力済みであり、\r\nコピー元に商品が設定されていないため、コピーできません。"
    ''' <summary>
    ''' "ファイル：「@PARAM1」から\r\n入金ファイル取込テーブルへ\r\nインポートを行ないますが宜しいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG136C As String = "ファイル：「@PARAM1」から\r\n入金ファイル取込テーブルへ\r\nインポートを行ないますが宜しいですか？"

    ''' <summary>
    ''' "\r\n■エラー発生箇所：{0} 行目"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG137E As String = "\r\n■エラー発生箇所：{0} 行目"

    ''' <summary>
    '''  "入金日と伝票番号の組合せで重複したデータがありました。\r\n取込内容を確認してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG138W As String = "\r\n※入金日と伝票番号の組合せで重複したデータがありました。\r\n　取込内容を確認してください。"

    ''' <summary>
    '''  "以下の組み合わせはマスタにありません。\r\n"
    ''' </summary>
    ''' <remarks>※連結文字列：[項目名1][項目値1]\r\n[項目名2][項目値2]\r\n…</remarks>
    Public Const MSG139E As String = "以下の組み合わせはマスタにありません。\r\n"

    ''' <summary>
    '''  "対象データが1件も選択されていません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG140E As String = "対象データが1件も選択されていません。\r\n"

    ''' <summary>
    '''  "新規登録時は取消できません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG141E As String = "新規登録時は取消できません。"

    ''' <summary>
    ''' "工務店請求額、実請求額のどちらか一方に変更が加えられています。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG142E As String = "工務店請求額、実請求額のどちらか一方に変更が加えられています。\r\n"

    ''' <summary>
    ''' \r\n再度、内容チェックボタンを押下してください。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG143E As String = "\r\n再度、内容チェックボタンを押下してください。"

    ''' <summary>
    ''' @PARAM1と@PARAM2が異なるため、@PARAM3できません。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG144E As String = "@PARAM1と@PARAM2が異なるため、@PARAM3できません。\r\n"

    ''' <summary>
    ''' @PARAM1が@PARAM2では選択されている@PARAM3は使用できません。\r\n
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG145E As String = "@PARAM1が@PARAM2では選択されている@PARAM3は使用できません。\r\n"

    ''' <summary>
    ''' 選択されている@PARAM1は@PARAM2です。\r\n
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG146E As String = "選択されている@PARAM1は@PARAM2です。\r\n"

    ''' <summary>
    ''' "@PARAM1処理が失敗しました。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG147E As String = "@PARAM1処理が失敗しました。\r\n"

    ''' <summary>
    ''' "当画面の設定内容はまだ更新されていません。\r\n呼出し元画面でデータの更新を行ってください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG148W As String = "当画面の設定内容はまだ更新されていません。\r\n呼出し元画面でデータの更新を行ってください。"

    ''' <summary>
    ''' "@PARAM1の自動設定ができませんでした。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG149W As String = "@PARAM1の自動設定ができませんでした。"

    ''' <summary>
    ''' "{0}マスタにデータがありません。{0}マスタを確認してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG150E As String = "{0}マスタにデータがありません。{0}マスタを確認してください。"

    ''' <summary>
    ''' "@PARAM1は必須です。\r\n @PARAM2から指定して下さい。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG151E As String = "@PARAM1は必須です。@PARAM2から指定して下さい。\r\n"

    ''' <summary>
    ''' "変更された @PARAM1 は伝票が作成されないデータです。(邸別請求データは更新されます)\r\n処理を実行してもよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG152C As String = "変更された @PARAM1 は伝票が作成されないデータです。(邸別請求データは更新されます)\r\n処理を実行してもよろしいですか？"

    ''' <summary>
    ''' "@PARAM1が入力されているため、@PARAM2は必須です。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG153E As String = "@PARAM1が入力されているため、@PARAM2は必須です。\r\n"

    ''' <summary>
    ''' "入力値が最大値を超えています。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG154E As String = "入力値が最大値を超えています。\r\n"

    ''' <summary>
    ''' "@PARAM1処理が失敗しました。\r\n\r\n処理状況が変わっていた可能性がありますので、確認してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG155E As String = "@PARAM1処理が失敗しました。\r\n\r\n処理状況が変わっていた可能性がありますので、確認してください。"

    ''' <summary>
    ''' "{0}の請求有無と実請求税抜金額との整合性に不備がありますが、処理を実行してもよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG156C As String = "{0}の請求有無と実請求税抜金額との整合性に不備がありますが、処理を実行してもよろしいですか？\r\n"

    ''' <summary>
    ''' "{0}の請求有無と実請求税抜金額との整合性に不備があります。修正してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG157E As String = "{0}の請求有無と実請求税抜金額との整合性に不備があります。修正してください。\r\n"

    ''' <summary>
    ''' "請求先情報を変更した場合は、請求書を再作成しないと反映されません"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG158W As String = "請求先情報を変更した場合は、請求書を再作成しないと反映されません"

    ''' <summary>
    ''' "※現在編集中の内容は反映されません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG159E As String = "※現在編集中の内容は反映されません。"

    ''' <summary>
    ''' "@PARAM1の@PARAM2が選択されているため、処理を続行できません。\r\n該当データのチェックをはずしますか？\r\n\r\n@PARAM2:\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG160C As String = "@PARAM1の@PARAM2が選択されているため、処理を続行できません。\r\n該当データのチェックをはずしますか？\r\n\r\n@PARAM2:\r\n"

    ''' <summary>
    ''' "{0}月確定処理済みのため、{1}の伝票売上年月日には{2}月以降の日付を入力してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG161E As String = "{0}月確定処理済みのため、{1}の伝票売上年月日は{2}月以降の日付を入力してください。\r\n"

    ''' <summary>
    ''' "\r\n検索結果は@PARAM1件を超えていますが、CSVは@PARAM2件まで出力します"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG162C As String = "\r\n※検索結果は@PARAM1件を超えていますが、CSVは@PARAM2件まで出力します"

    ''' <summary>
    ''' "@PARAM1の取得に失敗しました。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG163E As String = "@PARAM1の取得に失敗しました。\r\n"

    ''' <summary>
    ''' "指定した@PARAM1が有効でないか、@PARAM2に存在しません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG164E As String = "指定した@PARAM1が有効でないか、@PARAM2に存在しません。\r\n"

    ''' <summary>
    ''' "指定した@PARAM1が@PARAM2に存在しません。\r\n存在する@PARAM3を指定するか、採番して下さい。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG165E As String = MSG164E & "存在する@PARAM3を指定するか、採番して下さい。\r\n"

    ''' <summary>
    ''' "指定した@PARAM1が@PARAM2に存在しません。\r\n存在する@PARAM3を指定するか、採番して下さい。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG166E As String = "調査発注停止です。"

    ''' <summary>
    ''' "@PARAM1が@PARAM2に指定されているため、@PARAM3を変更できません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG167E As String = "@PARAM1が@PARAM2に指定されているため、@PARAM3を変更できません。\r\n"

    ''' <summary>
    ''' "同一の共通情報の物件データを作成します。よろしいですか？\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG168C As String = "同一の共通情報の物件データを作成します。よろしいですか？\r\n" & "(" & MSG159E & ")\r\n\r\n"

    ''' <summary>
    ''' "@PARAM1が@PARAM2のため、選択できません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG169E As String = "@PARAM1が@PARAM2のため、選択できません。\r\n"

    ''' <summary>
    ''' "請求書が再作成されている為、取消解除は行えません。\r\n同じ伝票を含む請求書を取消した後、取消解除を行ってください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG170E As String = "請求書が再作成されている為、取消解除は行えません。\r\n同じ伝票を含む請求書を取消した後、取消解除を行ってください。"

    ''' <summary>
    ''' "保証書発行状況が設定されています。\r\n商品変更・追加を行なう場合は 保証書発行担当に連絡願います。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG171E As String = "保証書発行状況が設定されています。\r\n商品変更・追加を行なう場合は 保証書発行担当に連絡願います。\r\n"

    ''' <summary>
    ''' "物件名寄の指定に矛盾があります。該当物件の名寄状況を確認して下さい。\r\n【顧客番号】:@PARAM1、【物件名寄コード】:@PARAM2\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG172E As String = "物件名寄の指定に矛盾があります。該当物件の名寄状況を確認して下さい。\r\n【顧客番号】:@PARAM1、【物件名寄コード】:@PARAM2\r\n"

    ''' <summary>
    ''' "印刷可能な請求書が存在しませんでした。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG173C As String = "印刷可能な請求書が存在しませんでした。\r\n"

    ''' <summary>
    ''' "既に他の人によって変更が行われております。\r\n"
    ''' （排他エラーではなく、警告とする場合）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG174W As String = "既に他の人によって変更が行われております。\r\n"

    ''' <summary>
    ''' "@PARAM1には@PARAM2がセットされます。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG175C As String = "@PARAM1には@PARAM2がセットされます。\r\n"

    ''' <summary>
    ''' "{0}月確定処理済みのため、{1}は{2}月以降日付を入力してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG176E As String = "{0}月確定処理済みのため、{1}は{2}月以降の日付を入力してください。\r\n"

    ''' <summary>
    ''' "指定された日付が、請求先マスタの締日と異なります。指定された日付で更新を行ってもよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG177C As String = "指定された日付が、請求先マスタの締日と異なります。指定された日付で更新を行ってもよろしいですか？"

    ''' <summary>
    ''' "@PARAM1\r\n以上の項目が変更されていますが、更新処理を続行してよろしいですか？\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG178C As String = "@PARAM1\r\n以上の項目が変更されていますが、更新処理を続行してよろしいですか？\r\n"

    ''' <summary>
    ''' "EARTHにデータがあるのは2010年度以降です。\r\n2009年度以前はPCAを参照してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG179W As String = "EARTHにデータがあるのは2010年度以降です。\r\n2009年度以前はPCAを参照してください。\r\n"

    ''' <summary>
    ''' "原価マスターに登録がありません。マスタ管理者へ連絡してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG180E As String = "原価マスターに登録がありません。マスタ管理者へ連絡してください。\r\n"

    ''' <summary>
    ''' "マスタの状態で上書きしますが、よろしいですか？\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG181C As String = "マスタの状態で上書きしますが、よろしいですか？\r\n"

    ''' <summary>
    ''' "販売価格マスターに登録がありません。マスタ管理者へ連絡してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG182E As String = "販売価格マスターに登録がありません。マスタ管理者へ連絡してください。\r\n"

    ''' <summary>
    ''' "商品価格設定マスターに登録がありません。マスタ管理者へ連絡してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG183E As String = "商品価格設定マスターに登録がありません。マスタ管理者へ連絡してください。\r\n"

    ''' <summary>
    ''' "この内容で登録する場合は【登録/修正実行】ボタンを押してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG184E As String = "この内容で登録する場合は【登録/修正実行】ボタンを押してください。\r\n"

    ''' <summary>
    ''' "既に@PARAM1されているため、@PARAM2できません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG185E As String = "既に@PARAM1されているため、@PARAM2できません。\r\n"

    ''' <summary>
    ''' "同時依頼棟数が2棟以上ですが、よろしいですか？\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG186C As String = "同時依頼棟数が2棟以上ですが、よろしいですか？\r\n"

    ''' <summary>
    ''' "@PARAM1と@PARAM2の両方が選択されています。どちらか一方を選択し直してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG187E As String = "@PARAM1と@PARAM2の両方が選択されています。どちらか一方を選択し直してください。\r\n"

    ''' <summary>
    ''' "日付取得時と現在で請求先に差異があります。日付取得ボタンを押下し、情報を再取得して下さい。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG188E As String = "日付取得時と現在で請求先に差異があります。\r\n日付取得ボタンを押下し、情報を再取得して下さい。\r\n"

    ''' <summary>
    ''' "検索実行にて対象データをご確認の上、当処理を実行下さい。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG189E As String = "検索実行にて対象データをご確認の上、当処理を実行下さい。\r\n"

    ''' <summary>
    ''' 全対象チェックがあり、請求先が指定されています。\r\n指定の請求先でデータ作成を行いますが宜しいですか？
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG190C As String = "全対象チェックがあり、請求先が指定されています。\r\n指定の請求先でデータ作成を行いますが宜しいですか？"

    ''' <summary>
    ''' "重複物件が存在する為、申込修正画面で新規受注を行ってください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG191E As String = "重複物件がある為、申込修正画面で新規受注を行ってください。\r\n"

    ''' <summary>
    ''' "特別対応が2個以上指定されているため、処理を続行できません。\r\n\r\n”
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG192E As String = "特別対応が2個以上指定されているため、処理を続行できません。\r\n\r\n"

    ''' <summary>
    ''' "変更があります。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG193E As String = "変更があります。\r\n"

    ''' <summary>
    ''' "Report.JHSへ連携するには、呼び出し元画面の【登録/修正 実行】ボタンを忘れずに押下してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG194E As String = "Report.JHSへ連携するには、呼び出し元画面の【登録/修正 実行】ボタンを忘れずに押下してください。\r\n"

    ''' <summary>
    ''' "選択されたデータは最終請求書ではありません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG195E As String = "選択されたデータは最終請求書ではありません。\r\n"

    ''' <summary>
    ''' "@PARAM1の為、\r\n@PARAM2に@PARAM3を設定しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG196W As String = "@PARAM1為、\r\n@PARAM2に@PARAM3を設定しました。"

    ''' <summary>
    ''' "請求書も取り消されますがよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG197C As String = "請求書も取り消されますがよろしいですか？"

    ''' <summary>
    ''' "商品@PARAM1の@PARAM2にプラス値が入力されています。\r\nこの項目はプラス値を入力することはできません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG198E As String = "商品@PARAM1の@PARAM2にプラス値が入力されています。\r\nこの項目はプラス値を入力することはできません。\r\n"

    ''' <summary>
    ''' "計上されていますがよろしいですか？\r\n\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG199C As String = "計上されていますがよろしいですか？\r\n\r\n"

    ''' <summary>
    ''' "以下の特別対応は価格反映されませんでした。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG200W As String = "以下の特別対応は価格反映されませんでした。\r\n\r\n@PARAM1"

    ''' <summary>
    ''' "価格反映されていない特別対応が存在します。\r\n特別対応画面で登録ボタンを押してください。\r\n\r\n@PARAM1"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG201W As String = "価格反映されていない特別対応が存在します。\r\n特別対応画面で登録ボタンを押してください。\r\n\r\n@PARAM1"

    ''' <summary>
    ''' "商品1に価格反映されている特別対応が存在しますが、変更してもよろしいですか？\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG202C As String = "商品1に価格反映されている特別対応が存在しますが、変更してもよろしいですか？\r\n"

    ''' <summary>
    ''' "変更予定加盟店が異なる状態では保証書発行日は入力できません\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG203E As String = "変更予定加盟店が異なる状態では保証書発行日は入力できません\r\n"

    ''' <summary>
    ''' "@PARAM1の値が未来日付のため入力できません\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG204E As String = "@PARAM1の値が未来日付のため入力できません\r\n"

    ''' <summary>
    ''' "保証期間：@PARAM1年から@PARAM2年に変更されました。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG205S As String = "保証期間：@PARAM1年から@PARAM2年に変更されました。\r\n"

    ''' <summary>
    ''' "指定調査会社１に\r\n@PARAM1\r\nが設定されています。\r\n\r\n@PARAM1\r\nを登録して下さい。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG206E As String = "指定調査会社１に\r\n@PARAM1\r\nが設定されています。\r\n\r\n@PARAM1\r\nを登録して下さい。\r\n"

    ''' <summary>
    ''' "受付を確定させるには登録/修正 実行ボタンを押してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG207S As String = "受付を確定させるには登録/修正 実行ボタンを押してください。\r\n"

    ''' <summary>
    ''' "画面の内容に変更があったため受付できません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG208E As String = "画面の内容に変更があったため受付できません。\r\n"

    ''' <summary>
    ''' "保証開始日が入力されていません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG209E As String = "お引渡し日が入力されていません。\r\n"

    ''' <summary>
    ''' "セット発行日が入力されていません。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG210E As String = "セット発行日が入力されていません。\r\n"

    ''' <summary>
    ''' "セット発行日に過去日付が入力されています。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG211E As String = "セット発行日に過去日付が入力されています。\r\n"

    ''' <summary>
    ''' "依頼内容と相違がありますがよろしいですか？\r\n"
    ''' </summary>
    ''' <remarks>（OK・キャンセル）</remarks>
    Public Const MSG212C As String = "依頼内容と相違がありますがよろしいですか？\r\n"

    ''' <summary>
    ''' "重複依頼の可能性が有りますが進めてよろしいですか？\r\n"
    ''' </summary>
    ''' <remarks>（OK・キャンセル）</remarks>
    Public Const MSG213C As String = "重複依頼の可能性が有りますが進めてよろしいですか？\r\n"

    ''' <summary>
    ''' "金額訂正が必要です。受付済みとしますか？\r\n"
    ''' </summary>
    ''' <remarks>（OK・キャンセル）</remarks>
    Public Const MSG214C As String = "金額訂正が必要です。受付済みとしますか？\r\n"

    ''' <summary>
    ''' "発行依頼をキャンセルします。保証画面を登録せずに閉じますがよろしいですか？\r\n"
    ''' </summary>
    ''' <remarks>（OK・キャンセル）</remarks>
    Public Const MSG215C As String = "発行依頼をキャンセルします。保証画面を登録せずに閉じますがよろしいですか？\r\n"

    ''' <summary>
    ''' 対象とする処理件数が多すぎます。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG216E As String = "対象とする処理件数が多すぎます。"

    ''' <summary>
    ''' "お引渡し日が間違いの可能性が有ります。転記してよろしいですか？\r\n"
    ''' </summary>
    ''' <remarks>（OK・キャンセル）</remarks>
    Public Const MSG217C As String = "お引渡し日が間違いの可能性が有ります。転記してよろしいですか？\r\n"

    ''' <summary>
    ''' 他で更新があったため更新できなかった物件があります。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG218E As String = "他で更新があったため更新できなかった物件があります。\r\n"

    ''' <summary>
    ''' お引渡し日が不正な物件が有ります。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG219E As String = "お引渡し日が不正な物件が有ります。\r\n"

    ''' <summary>
    ''' 重複依頼の可能性がある物件があります。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG220E As String = "重複依頼の可能性がある物件があります。\r\n"

    ''' <summary>
    ''' 物件名または物件住所の桁数が大きすぎる物件があります。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG221E As String = "物件名または物件住所の桁数が大きすぎる物件があります。\r\n"

    ''' <summary>
    ''' 金額訂正が必要な物件があります。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG222E As String = "金額訂正が必要な物件があります。\r\n"

    ''' <summary>
    ''' 発行不可の物件があります。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG223E As String = "発行不可の物件があります。\r\n"

    ''' <summary>
    ''' 地盤モール連携対象外の物件があります。
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG224E As String = "地盤モール連携対象外の物件があります。\r\n"

    ''' <summary>
    ''' "@PARAM1の@PARAM2が選択されています。\r\n処理を続けますか？\r\n\r\n@PARAM2:\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG225C As String = "@PARAM1の@PARAM2が選択されています。\r\n処理を続けますか？\r\n\r\n@PARAM2:\r\n"
End Class
