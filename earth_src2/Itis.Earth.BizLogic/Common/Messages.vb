''' <summary>
''' EARTHで使用するメッセージを管理するクラスです
''' 使用する場合は Messages.Instance.MSGXXXX と指定
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
    Public ReadOnly MSG001E As String = "マスターにありません。経理に追加の連絡をして下さい。"
    ''' <summary>
    ''' 保証書NO年月が未登録です
    ''' </summary>
    Public ReadOnly MSG002E As String = "保証書NO年月が未登録です。"
    ''' <summary>
    ''' 他の端末で更新された為キャンセルされました。再度読込みを行い内容を御確認下さい
    ''' </summary>
    Public ReadOnly MSG003E As String = "他の端末で更新された為キャンセルされました。再度読込みを行い内容を御確認下さい。 [{0}:{1}]"

    ''' <summary>
    ''' "区分が選択されていません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG004E As String = "区分が選択されていません。"

    ''' <summary>
    ''' "既に保証書NOは発行済みです。追加で発行しますか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG005C As String = "既に保証書NOは発行済みです。追加で発行しますか？"

    ''' <summary>
    ''' "「区分」は必須条件です。選択してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG006E As String = "「区分」は必須条件です。選択してください。"

    ''' <summary>
    ''' "検索上限件数に「無制限」が選択されています。\r\n画面表示に時間が掛かる可能性がありますが、実行してよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG007C As String = "検索上限件数に「無制限」が選択されています。\r\n画面表示に時間が掛かる可能性がありますが、実行してよろしいですか？"

    ''' <summary>
    ''' "項目に条件を入力するか、\r\n「対象範囲：全て」もしくは「全区分」のチェックをはずしてください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG008E As String = "項目に条件を入力するか、\r\n「対象範囲：全て」もしくは「全区分」のチェックをはずしてください。"

    ''' <summary>
    ''' "指定した商品コードは存在しません。検索画面より商品を選択するか、存在する商品コードを入力してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG009E As String = "指定した商品コードは存在しません。検索画面より商品を選択するか、存在する商品コードを入力してください。"

    ''' <summary>
    ''' "発注書金額が入力されているので、商品コードを削除できません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG010E As String = "発注書金額が入力されているので、商品コードを削除できません。"

    ''' <summary>
    ''' "発注書金額が違いますが、「確定」にしてよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG011C As String = "発注書金額が違いますが、「確定」にしてよろしいですか？"

    ''' <summary>
    ''' "発注書金額が同じなので、「確定」にしてよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG012C As String = "発注書金額が同じなので、「確定」にしてよろしいですか？"

    ''' <summary>
    ''' CSV出力確認メッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG013C As String = "CSV出力処理を実行します。よろしいですか？\r\n※検索結果は@PARAM1件を超えていますが、CSVは@PARAM1件まで出力します。"

    ''' <summary>
    ''' "@PARAM1は必須です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG013E As String = "@PARAM1は必須です。\r\n"

    ''' <summary>
    ''' "@PARAM1の値が有効日付範囲内にありません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG014E As String = "@PARAM1の値が有効日付範囲内にありません。\r\n"

    ''' <summary>
    ''' "@PARAM1に使用できない文字列が入力されています。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG015E As String = "@PARAM1に使用できない文字列が入力されています。\r\n"

    ''' <summary>
    ''' "@PARAM1の文字数が多すぎます。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG016E As String = "@PARAM1の文字数が多すぎます。\r\n"

    ''' <summary>
    ''' "重複物件の確認を行ってください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG017E As String = "重複物件の確認を行ってください。\r\n"

    ''' <summary>
    ''' "処理を実行します。よろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG017C As String = "処理を実行します。よろしいですか？"

    ''' <summary>
    ''' "@PARAM1処理が完了しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG018S As String = "@PARAM1処理が完了しました。"

    ''' <summary>
    ''' "@PARAM1処理が失敗しました。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG019E As String = "@PARAM1処理が失敗しました。"

    ''' <summary>
    ''' "該当データがありません。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG020E As String = "該当データがありません。"

    ''' <summary>
    ''' "セッション情報が取得できませんでした。タイムアウトになった可能性があります。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG021E As String = "セッション情報が取得できませんでした。タイムアウトになった可能性があります。"

    ''' <summary>
    ''' "保証書NOの大小が不適切です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG022E As String = "保証書NOの大小が不適切です。"

    ''' <summary>
    ''' "引き続き、調査予定連絡書を表示します。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG023S As String = "引き続き、調査予定連絡書を表示します。"

    ''' <summary>
    ''' MSG021E ＆ "\r\n\r\n「依頼内容」「商品１／商品２」の編集内容が失われています。再度入力してください。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG024E As String = MSG021E & "\r\n\r\n「依頼内容」「商品１／商品２」の編集内容が失われています。再度入力してください。"

    ''' <summary>
    ''' MSG021E ＆ "\r\n\r\n「依頼内容」「商品１／商品２」の編集内容が失われています。データの再読込を行います。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG025E As String = MSG021E & "\r\n\r\n「依頼内容」「商品１／商品２」の編集内容が失われています。データの再読込を行います。"

    ''' <summary>
    ''' "加盟店コードか、加盟店カナ名の何れかは必須条件です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG026E As String = "加盟店コードか加盟店カナ名の何れかは必須条件です。"

    ''' <summary>
    ''' "@PARAM1は整数@PARAM2桁、小数@PARAM3桁まで入力可能です。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG027E As String = "@PARAM1は整数@PARAM2桁、小数@PARAM3桁まで入力可能です。\r\n"

    ''' <summary>
    ''' "依頼内容を確定しないと画面遷移できません\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG028E As String = "【 依頼内容を確定しないと画面遷移できません 】\r\n\r\n"

    ''' <summary>
    ''' "商品区分：リフォーム が設定されております。\r\n処理を継続してよろしいですか？"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG029C As String = "商品区分：リフォーム が設定されております。\r\n処理を継続してよろしいですか？"

    ''' <summary>
    ''' "@PARAM1が変更されています。「検索」ボタンを押下し、情報を再取得してください。\r\n"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG030E As String = "@PARAM1が変更されています。「検索」ボタンを押下し、情報を再取得してください。\r\n"

    Public ReadOnly MSG091E As String = "{0}にマイナス{1}を入力することはできません。\r\n"
    '大連追加
    Public ReadOnly MSG031E As String = """宅地地盤調査主任資格""が無い会社は調査業務を行うことができません。\r\n"
    Public ReadOnly MSG032E As String = "取消操作を行う場合は取消理由を記入してください。\r\n"
    Public ReadOnly MSG033E As String = "請求先新規登録時、\r\n調査会社名に空白は登録出来ません。"
    Public ReadOnly MSG034E As String = "対象のデータが存在しませんでした。"
    Public ReadOnly MSG035E As String = "他のユーザがdatファイルを開いている。"
    Public ReadOnly MSG036E As String = "datファイルを作成するパスが不正です。"
    Public ReadOnly MSG037E As String = "@PARAM1を入力して下さい。\r\n"
    Public ReadOnly MSG038E As String = "@PARAM1を選択して下さい。\r\n"
    Public ReadOnly MSG039E As String = "該当するデータがありません。\r\n"
    Public ReadOnly MSG040E As String = "取り込みファイルの上限件数({0})を超過した為、アップロード処理を中断しました。\r\n ファイル内のレコード数を分割して、EDI情報作成日を訂正後に再度実行ください。"
    ''' <summary>
    ''' "@PARAM1か、@PARAM2の何れかは必須条件です。"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly MSG041E As String = "@PARAM1か@PARAM2の何れかは必須条件です。"

    Public ReadOnly MSG042E As String = "ファイルを指定してください。"

    Public ReadOnly MSG043E As String = "CSVファイルではありません。"

    Public ReadOnly MSG044E As String = "ファイルの容量が 0 バイトです。"

    Public ReadOnly MSG045C As String = "ファイル:「@PARAM2」から\r\n@PARAM1テーブルへ\r\nインポートを行ないますが宜しいですか？"

    Public ReadOnly MSG046E As String = "対象の加盟店が@PARAM1件を超えています。実行できません。"

    Public ReadOnly MSG046C As String = "データ取り込み処理は完了しました。"

    Public ReadOnly MSG047C As String = "データ取り込み処理は完了しました。\r\n※データ取り込みエラーが発生しました。"

    Public ReadOnly MSG048E As String = "取り込みファイルの明細データがありません。"

    Public ReadOnly MSG049E As String = "処理中にエラーが発生しました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n"

    Public ReadOnly MSG050E As String = "データベース反映中にエラーが発生しました。\r\nシステム管理者へお知らせ下さい。\r\n\r\n"

    Public ReadOnly MSG051E As String = "対象件数が@PARAM1件を超えています。実行できません。"
    Public ReadOnly MSG052E As String = "取込ファイルは既に取込処理のあったファイル名です。\r\n 2重登録ではないことを確認し、ファイル名を変更してください。"
    Public ReadOnly MSG053E As String = "{0}は{1}ﾏｽﾀにありません。\r\n{2}を見直しください"
    Public ReadOnly MSG054C As String = "ファイル:「@PARAM2」から\r\n@PARAM1\r\nを行ないますが宜しいですか？"
    Public ReadOnly MSG055E As String = "{0}は商品ﾏｽﾀ・倉庫ｺｰﾄﾞ=115にありません。\r\n{1}を見直しください"

    ''' <summary>
    ''' 加盟店住所情報：住所1は必須入力です
    ''' </summary>
    ''' <remarks></remarks>

    Public ReadOnly MSG2002E As String = "{0}は全角{1}文字以内で入力して下さい。\r\n"
    Public ReadOnly MSG2003E As String = "{0}は半角{1}文字以内で入力して下さい。\r\n"
    Public ReadOnly MSG2004E As String = "{0}は{1}桁で入力して下さい。\r\n"
    Public ReadOnly MSG2005E As String = "{0}は半角英数字で入力して下さい。\r\n"
    Public ReadOnly MSG2006E As String = "{0}は半角数字で入力して下さい。\r\n"
    Public ReadOnly MSG2007E As String = "{0}は全角で入力して下さい。\r\n"
    Public ReadOnly MSG2008E As String = "{0}が存在しません。\r\n"

    Public ReadOnly MSG2009E As String = "他の端末で削除された為キャンセルされました。再度読込みを行い内容を御確認下さい。"
    Public ReadOnly MSG2010E As String = "{0}は半角カタカナで入力して下さい。\r\n"
    Public ReadOnly MSG2011E As String = "{0}が整数ではありません。\r\n"
    Public ReadOnly MSG2012E As String = "{0}Toに{1}From以前の値を入力できません。\r\n"
    Public ReadOnly MSG2013E As String = "マスタにありません。経理に追加の連絡をして下さい。"
    Public ReadOnly MSG2000C As String = "データ登録されていますが継続しますか。"
    Public ReadOnly MSG2014E As String = "発注書が未設定です,調査、工事または検査を選択してください。\r\n"
    Public ReadOnly MSG2015E As String = "{0}締め日は31以内で入力してください。\r\n"
    Public ReadOnly MSG2016E As String = "加盟店コード(From)を入力して下さい。\r\n"
    Public ReadOnly MSG2017E As String = "日付以外が入力されています。\r\n"
    Public ReadOnly MSG2018E As String = "{0}を登録しました。"
    Public ReadOnly MSG2019E As String = "{0}はyyyy/mmで入力して下さい。\r\n"
    Public ReadOnly MSG2020E As String = "権限がありません。"
    Public ReadOnly MSG2021E As String = "{0}に登録できる桁数は、整数部8桁以内です。\r\n"
    Public ReadOnly MSG2022E As String = "住所が登録されていません。"
    Public ReadOnly MSG2023E As String = "登録情報に問題が発生しました。情報システムに連絡ください。"
    Public ReadOnly MSG2024E As String = "該当ユーザーがありません。"
    Public ReadOnly MSG2025E As String = "検索対象加盟店以上のコードはありません、コードを見直して検索してください。"
    Public ReadOnly MSG2026E As String = "区分{0}の加盟店は設定がありません。加盟店コードは直接入力してください。"
    Public ReadOnly MSG2027E As String = "加盟店コードが重複しています。"
    Public ReadOnly MSG2028E As String = "{0}は{1}バイト以内で入力して下さい。\r\n"
    Public ReadOnly MSG2029E As String = "{0}は存在しています。他の{1}を再入力してください。\r\n"
    Public ReadOnly MSG2030E As String = "データに問題が発生しました。情報システムに連絡ください。"
    Public ReadOnly MSG2031C As String = "登録済みデータです。上書きしてもよろしいですか。"
    Public ReadOnly MSG2032E As String = "半必須条件はいずれか入れてください。\r\n"
    Public ReadOnly MSG2033E As String = "他の端末で更新された為キャンセルされました。再度読込みを行い内容を御確認下さい。\r\n  ■更新ユーザーID：[{0}]\r\n  ■更新日時：[{1}]\r\n  ■対象：[{2}]"
    Public ReadOnly MSG2034E As String = "該当するデータが存在しません。"
    Public ReadOnly MSG2035E As String = "マスターに重複データが存在します。"
    Public ReadOnly MSG2036E As String = "{0}に存在しません。"
    Public ReadOnly MSG2037E As String = "検索条件は何れか一つが必須です。"
    Public ReadOnly MSG2038E As String = "{0}の合計値は100%です。"
    Public ReadOnly MSG2039E As String = "{0}の値が有効数値範囲内にありません。\r\n"
    Public ReadOnly MSG2049E As String = "該当データが存在しません。既に削除されている可能性があります。"
    Public ReadOnly MSG2050E As String = "調査方法・調査概要は何れか必須です。"
    Public ReadOnly MSG2051E As String = "{0}のみの検索は不可です。"
    Public ReadOnly MSG2052E As String = "割合が設定されているので、未選択は不可です。"
    Public ReadOnly MSG2053E As String = "調査方法・調査概要・商品コードは何れか必須です。"
    Public ReadOnly MSG2054E As String = "相手先が変更されています。「検索」ボタンを押下し、情報を再取得してください。"
    Public ReadOnly MSG2055E As String = "{0}は数値です。"
    Public ReadOnly MSG2056E As String = "{0}の入力範囲は(0～2147483647)です。"
    Public ReadOnly MSG2057E As String = "上書きしてもよろしいですか？"
    Public ReadOnly MSG2058E As String = "エラーが発生しました。登録できませんでした。"
    Public ReadOnly MSG2059E As String = "請求先が変更されています。「検索」ボタンを押下し、情報を再取得してください。"
    Public ReadOnly MSG2060E As String = "詳細は最大桁数が128バイトです。\r\n"
    Public ReadOnly MSG2061E As String = "更新できませんでした。\r\n該当データが変更されています。情報を再取得してください。"
    Public ReadOnly MSG2062E As String = "更新できませんでした。\r\n該当データを削除されています。情報を再取得してください。"
    Public ReadOnly MSG2063E As String = "該当請求先がありません。"
    Public ReadOnly MSG2064E As String = "請求先ｺｰﾄﾞ、種別、重要度、請求締め日、請求書必着日のいづれかは入力必須。"
    Public ReadOnly MSG2065E As String = "{0}-{1}の組合せがないものが存在します。新規登録を行いますか。"
    Public ReadOnly MSG2066E As String = "新規登録の加盟店ｺｰﾄﾞが重複してます。"
    Public ReadOnly MSG2067E As String = "[{0}]は{1}桁のｺｰﾄﾞです。入力内容の確認願います。"
    Public ReadOnly MSG2068E As String = "[{0}]は与信管理ﾏｽﾀに存在しません。"
    Public ReadOnly MSG2069E As String = "{0}が完了しました"
    Public ReadOnly MSG2070E As String = "{0}に失敗しました、システム管理者へご連絡下さい"
    Public ReadOnly MSG2071E As String = "店別請求テーブル連携管理テーブル追加・更新でエラー発生しました"
    Public ReadOnly MSG2072E As String = "安全協力会費は 円、割合（％）　どちらかでの登録となります"
    Public ReadOnly MSG2073E As String = "新規登録の取込内に　登録済み加盟店ｺｰﾄﾞがございます\r\nご確認後、もう1度取込処理実施願います"
    Public ReadOnly MSG2074E As String = "採番設定の上限となり、この区分に対する採番設定変更が必要です。\r\nシステム管理者へご連絡下さい。"
    Public ReadOnly MSG2075E As String = "{0}が登録料以外のｺｰﾄﾞです(倉庫ｺｰﾄﾞ200の商品を選択して下さい)。\r\n登録料_商品コードを見直しください。"
    Public ReadOnly MSG2076E As String = "この加盟店番号では過去に取引条件確認表を提出された記録がございません。\r\n支店の場合は、本社の加盟店番号で一度ご確認ください。"
    Public ReadOnly MSG2077E As String = "この加盟店番号では過去に調査カードを提出された記録がございません。\r\n支店の場合は、本社の加盟店番号で一度ご確認ください。"

    Public ReadOnly MSG2078E As String = "売上金額0円ではない場合、請求有無「無」は選択できません。"
    Public ReadOnly MSG2079E As String = "売上金額0円の場合、請求有無「有」は選択できません。"

    ''' <summary>
    ''' "一括セット発送日、送付担当者は両方入力が必要となります。"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2015/12/08 高兵兵</history>
    Public ReadOnly MSG2080E As String = "一括セット発送日、送付担当者は両方入力が必要となります。"
    Public ReadOnly MSG2081E As String = "取消がたっている行に発送日・送付担当者登録があります。問題ないでしょうか？"
    Public ReadOnly MSG2082E As String = "部数は整数1-6入力可能です。"
    Public ReadOnly MSG2083E As String = "区分・加盟店コード・保証書No が一致のデータ複数あります。検査報告書_管理画面で重複データを取消て下さい"

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New Messages()

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As Messages
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New Messages()
            End If
            Return _instance
        End Get
    End Property

    'コンストラクタ（非公開：外部からのアクセスは不可）
    Private Sub New()
        ' 必要に応じて実装
    End Sub
End Class
