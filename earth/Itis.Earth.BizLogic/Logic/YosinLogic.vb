Imports System.Configuration
Imports System.Web
Imports System.Text
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 与信処理クラス
''' </summary>
''' <remarks></remarks>
Public Class YosinLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '現在時刻
    Dim updDateTime As DateTime = DateTime.Now
    'DataMappingHelperインスタンス
    Dim clsDMH As New DataMappingHelper
    'DBアクセス時に取消区分を考慮するか否かのデフォルト値
    Dim blnDeleteDefault As Boolean = False

#Region "与信チェック処理"
    ''' <summary>
    ''' 与信チェック処理
    ''' </summary>
    ''' <param name="intType">処理タイプ（1:受注、2:報告書、3:改良工事、4:保証、5:邸別データ修正）</param>
    ''' <param name="JibanRecordOld">更新前DBのJibanRecord</param>
    ''' <param name="JibanRecordNew">画面入力データのJibanRecord</param>
    ''' <returns>0:与信OK / 1:与信限度額超過(登録拒否) / 6:与信管理情報取得エラー / 
    '''          7:与信管理テーブル更新エラー / 8:メール送信処理エラー / 9:その他エラー</returns>
    ''' <remarks></remarks>
    Public Function YosinCheck(ByVal intType As Integer, _
                                ByVal JibanRecordOld As JibanRecordBase, _
                                ByVal JibanRecordNew As JibanRecordBase) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".YosinCheck", _
                                                    JibanRecordOld, _
                                                    JibanRecordNew)

        ''戻り値
        'Dim intReturn As Integer = EarthConst.YOSIN_ERROR_YOSIN_OK

        ''新旧の売上金額合計
        'Dim intUriageGakuGoukeiOld As New Integer
        'Dim intUriageGakuGoukeiNew As New Integer

        ''新旧加盟店に紐づく与信管理レコード
        'Dim YosinKanriRecordOld As New YosinKanriRecord
        'Dim YosinKanriRecordNew As New YosinKanriRecord

        ''メール送信先リスト
        'Dim YosinMailTargetRecordList As New List(Of YosinMailTargetRecord)

        ''破棄チェック
        'Dim intHakiCheckResult As Integer = YosinHakiCheck(JibanRecordOld, JibanRecordNew)
        'Select Case intHakiCheckResult
        '    Case 0
        '        '新旧共に破棄でない場合、与信チェック処理継続
        '    Case 1
        '        '新旧共に破棄の場合、与信OKとする
        '        Return EarthConst.YOSIN_ERROR_YOSIN_OK
        '    Case 2
        '        '物件復活の場合与信チェック処理継続
        '    Case 3
        '        '物件破棄の場合、与信OKとする
        '        Return EarthConst.YOSIN_ERROR_YOSIN_OK
        '    Case EarthConst.YOSIN_ERROR_YKANRI_UPDATE_ERROR
        '        '与信管理マスタ更新エラー
        '        Return EarthConst.YOSIN_ERROR_YKANRI_UPDATE_ERROR
        '    Case EarthConst.YOSIN_ERROR_OTHER_ERROR
        '        'その他エラー
        '        Return EarthConst.YOSIN_ERROR_OTHER_ERROR
        'End Select

        ''新旧の売上金額合計を取得
        'Select Case intType
        '    Case 1  '受注
        '        '商品１，２，３
        '        intUriageGakuGoukeiOld = GetUriageGoukeiSyouhin123(JibanRecordOld)
        '        intUriageGakuGoukeiNew = GetUriageGoukeiSyouhin123(JibanRecordNew)
        '        intReturn = YosinCheckForType(1, JibanRecordNew, JibanRecordOld, _
        '                                      intUriageGakuGoukeiNew, intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '    Case 2 '報告書
        '        '調査報告書再発行手数料
        '        intUriageGakuGoukeiOld = GetTeibetuUriGaku(JibanRecordOld.TyousaHoukokusyoRecord)
        '        intUriageGakuGoukeiNew = GetTeibetuUriGaku(JibanRecordNew.TyousaHoukokusyoRecord)
        '        intReturn = YosinCheckForType(3, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
        '                                      intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '    Case 3 '工事
        '        '改良工事、追加改良工事
        '        intUriageGakuGoukeiOld = GetUriageGoukeiKouji(JibanRecordOld)
        '        intUriageGakuGoukeiNew = GetUriageGoukeiKouji(JibanRecordNew)
        '        intReturn = YosinCheckForType(2, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
        '                                      intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '        If intReturn = 0 Then
        '            '工事報告書再発行手数料
        '            intUriageGakuGoukeiOld = GetTeibetuUriGaku(JibanRecordOld.KoujiHoukokusyoRecord)
        '            intUriageGakuGoukeiNew = GetTeibetuUriGaku(JibanRecordNew.KoujiHoukokusyoRecord)
        '            intReturn = YosinCheckForType(3, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
        '                                          intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '        End If
        '    Case 4 '保証
        '        '保証書再発行手数料
        '        intUriageGakuGoukeiOld = GetTeibetuUriGaku(JibanRecordOld.HosyousyoRecord)
        '        intUriageGakuGoukeiNew = GetTeibetuUriGaku(JibanRecordNew.HosyousyoRecord)
        '        intReturn = YosinCheckForType(3, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
        '                                      intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '        If intReturn = 0 Then
        '            '解約払戻
        '            intUriageGakuGoukeiOld = GetTeibetuUriGaku(JibanRecordOld.KaiyakuHaraimodosiRecord, True, JibanRecordOld.TysKibouDate)
        '            intUriageGakuGoukeiNew = GetTeibetuUriGaku(JibanRecordNew.KaiyakuHaraimodosiRecord, True, JibanRecordNew.TysKibouDate)
        '            intReturn = YosinCheckForType(1, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
        '                                          intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '        End If
        '    Case 5 '邸別データ修正
        '        '調査請求先向け(商品１，２，３、解約払戻)
        '        intUriageGakuGoukeiOld = GetUriageGoukeiTyousa(JibanRecordOld)
        '        intUriageGakuGoukeiNew = GetUriageGoukeiTyousa(JibanRecordNew)
        '        intReturn = YosinCheckForType(1, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
        '                                      intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '        If intReturn = 0 Then
        '            '工事請求先向け(改良工事、追加改良工事)
        '            JibanRecordNew.KairyKojKanryYoteiDate = JibanRecordOld.KairyKojKanryYoteiDate   '改良工事予定日を現DBデータから取得
        '            JibanRecordNew.TKojKanryYoteiDate = JibanRecordOld.TKojKanryYoteiDate           '追加工事予定日を現DBデータから取得
        '            intUriageGakuGoukeiOld = GetUriageGoukeiKouji(JibanRecordOld)
        '            intUriageGakuGoukeiNew = GetUriageGoukeiKouji(JibanRecordNew)
        '            intReturn = YosinCheckForType(2, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
        '                                          intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '        End If
        '        If intReturn = 0 Then
        '            '販促品請求先向け(調査報告書再発行手数料、工事報告書再発行手数料、保証書再発行手数料)
        '            intUriageGakuGoukeiOld = GetUriageGoukeiHansoku(JibanRecordOld)
        '            intUriageGakuGoukeiNew = GetUriageGoukeiHansoku(JibanRecordNew)
        '            intReturn = YosinCheckForType(3, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
        '                                          intUriageGakuGoukeiOld, YosinMailTargetRecordList)
        '        End If

        'End Select


        'If intReturn <> EarthConst.YOSIN_ERROR_YOSIN_OK Then
        '    Return intReturn
        'End If

        Return EarthConst.YOSIN_ERROR_YOSIN_OK

    End Function

#End Region

#Region "与信チェック処理 店別請求テーブルを対象"
    ''' <summary>
    ''' 与信チェック処理 店別請求テーブルを対象
    ''' </summary>
    ''' <param name="intType">処理タイプ（6:登録手数料、初期ツール料 7:FC外販促品）</param>
    ''' <param name="YosinTenbetuRecordOld">更新前のYosinTenbetuRecord</param>
    ''' <param name="YosinTenbetuRecordNew">画面入力データのYosinTenbetuRecord</param>
    ''' <returns>0:与信OK / 1:与信限度額超過(登録拒否) / 6:与信管理情報取得エラー / 
    '''          7:与信管理テーブル更新エラー / 8:メール送信処理エラー / 9:その他エラー</returns>
    ''' <remarks></remarks>
    Public Function YosinCheckTenbetu(ByVal intType As Integer, _
                                    ByVal YosinTenbetuRecordOld As YosinTenbetuRecord, _
                                    ByVal YosinTenbetuRecordNew As YosinTenbetuRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".YosinCheckTenbetu", _
                                            YosinTenbetuRecordOld, _
                                            YosinTenbetuRecordNew)

        ''戻り値
        'Dim intReturn As Integer = 0

        ''新旧の売上金額合計
        'Dim intUriageGakuGoukeiOld As New Integer
        'Dim intUriageGakuGoukeiNew As New Integer

        ''新旧加盟店に紐づく与信管理レコード
        'Dim YosinKanriRecordOld As New YosinKanriRecord
        'Dim YosinKanriRecordNew As New YosinKanriRecord

        ''メール送信フラグ
        'Dim intMailSendFlg As Integer = 0

        ''メール送信先リスト
        'Dim YosinMailTargetRecordList As New List(Of YosinMailTargetRecord)

        ''請求先タイプ
        'Dim intSeikyusakiType As Integer

        ''新旧の売上金額合計を取得
        'Select Case intType
        '    Case 6 '登録手数料、初期ツール料
        '        intSeikyusakiType = 1
        '        '与信管理レコード取得(調査請求先)
        '        YosinKanriRecordNew = GetYosinKnariRecord(intSeikyusakiType, YosinTenbetuRecordNew.KameitenCd, blnDeleteDefault)
        '        If YosinKanriRecordNew Is Nothing Then
        '            Return EarthConst.YOSIN_ERROR_YOSIN_OK    '与信管理マスタ取得エラー -> とりあえず与信OKとして扱う
        '        End If

        '        '当日受注額算出
        '        ' = 現当日受注額 + (新合計金額 - 旧合計金額))
        '        intUriageGakuGoukeiOld = YosinTenbetuRecordOld.TourokuTesuuryouUriGaku + YosinTenbetuRecordOld.SyokiToolRyouUriGaku
        '        intUriageGakuGoukeiNew = YosinTenbetuRecordNew.TourokuTesuuryouUriGaku + YosinTenbetuRecordNew.SyokiToolRyouUriGaku

        '    Case 7 'FC外販促品
        '        intSeikyusakiType = 3
        '        '与信管理レコード取得(販促品請求先)
        '        YosinKanriRecordNew = GetYosinKnariRecord(intSeikyusakiType, YosinTenbetuRecordNew.KameitenCd, blnDeleteDefault)
        '        If YosinKanriRecordNew Is Nothing Then
        '            Return EarthConst.YOSIN_ERROR_YOSIN_OK    '与信管理マスタ取得エラー -> とりあえず与信OKとして扱う
        '        End If

        '        '当日受注額算出
        '        ' = 現当日受注額 + (新合計金額 - 旧合計金額))
        '        intUriageGakuGoukeiOld = IIf(YosinTenbetuRecordOld.HansokuGoukei > Integer.MaxValue, _
        '                                     Integer.MaxValue, YosinTenbetuRecordOld.HansokuGoukei)
        '        intUriageGakuGoukeiNew = IIf(YosinTenbetuRecordNew.HansokuGoukei > Integer.MaxValue, _
        '                                     Integer.MaxValue, YosinTenbetuRecordNew.HansokuGoukei)

        'End Select

        ''当日受注額をセット
        'YosinKanriRecordNew.ToujitsuJyutyuuGaku += (intUriageGakuGoukeiNew - intUriageGakuGoukeiOld)

        ''与信限度額チェック
        'Dim intYosinGendoResult As Integer = CheckYosinGendo(YosinKanriRecordNew, _
        '                                                    intUriageGakuGoukeiOld, _
        '                                                    intUriageGakuGoukeiNew, _
        '                                                    intMailSendFlg)
        'If intYosinGendoResult = EarthConst.YOSIN_ERROR_YOSIN_NG Then
        '    '戻り値=1：受注管理フラグが設定されていない状態で、与信限度額超過になった場合、
        '    '与信エラーとして与信管理マスタ更新対象外にする。

        'Else
        '    '与信管理マスタ更新
        '    Dim intEditYosinKanriResultNew As Integer = 0
        '    YosinKanriRecordNew.UpdLoginUserId = YosinTenbetuRecordNew.UpdLoginUserId
        '    YosinKanriRecordNew.UpdDatetime = updDateTime
        '    intEditYosinKanriResultNew = SetEditYosinKanri(YosinKanriRecordNew)

        '    If intEditYosinKanriResultNew <> 0 Then
        '        '与信管理マスタ更新エラー
        '        Return EarthConst.YOSIN_ERROR_YKANRI_UPDATE_ERROR
        '    End If
        'End If


        ''メール送信
        'If intMailSendFlg > 0 Then
        '    '与信メール送信処理
        '    Dim intMailResult = SendYosinMail(intSeikyusakiType, YosinTenbetuRecordNew.KameitenCd, intYosinGendoResult)
        '    If intMailResult <> 0 Then
        '        'メール送信エラー
        '        Return EarthConst.YOSIN_ERROR_YOSIN_MAIL_ERROR
        '    End If
        'End If

        'Return intYosinGendoResult

        Return EarthConst.YOSIN_ERROR_YOSIN_OK

    End Function

#End Region

#Region "タイプ別与信チェック処理（実処理）"
    ''' <summary>
    ''' タイプ別与信チェック処理（実処理）
    ''' </summary>
    ''' <param name="intType">1:調査請求先、2:工事請求先、3:販促品請求先</param>
    ''' <param name="JibanRecordNew">画面入力データのJibanRecord</param>
    ''' <param name="JibanRecordOld">更新前DBのJibanRecord</param>
    ''' <param name="intUriageGakuGoukeiNew">画面入力データの合計金額</param>
    ''' <param name="intUriageGakuGoukeiOld">更新前DBの合計金額</param>
    ''' <param name="YosinMailTargetRecordList">メール送信先レコードリスト</param>
    ''' <returns>処理結果</returns>
    ''' <remarks></remarks>
    Public Function YosinCheckForType(ByVal intType As Integer, _
                                        ByRef JibanRecordNew As JibanRecordBase, _
                                        ByRef JibanRecordOld As JibanRecordBase, _
                                        ByVal intUriageGakuGoukeiNew As Integer, _
                                        ByVal intUriageGakuGoukeiOld As Integer, _
                                        ByRef YosinMailTargetRecordList As List(Of YosinMailTargetRecord), _
                                        Optional ByVal flgUpdYosinKanriHissu As Boolean = False) As Integer

        'メール送信フラグ
        Dim intMailSendFlg As Integer = 0

        '新旧加盟店に紐づく与信管理レコード
        Dim YosinKanriRecordOld As New YosinKanriRecord
        Dim YosinKanriRecordNew As New YosinKanriRecord

        If JibanRecordOld.KameitenCd Is Nothing Or JibanRecordOld.KameitenCd = JibanRecordNew.KameitenCd Then
            '新旧加盟店コードが同一の場合

            '与信管理レコード取得
            YosinKanriRecordNew = GetYosinKnariRecord(intType, JibanRecordNew.KameitenCd, blnDeleteDefault)
            If YosinKanriRecordNew Is Nothing Then
                Return EarthConst.YOSIN_ERROR_YOSIN_OK    '与信管理マスタ取得エラー -> とりあえず与信OKとして扱う
            End If
            '当日受注額算出
            ' = 現当日受注額 + (新合計金額 - 旧合計金額))
            YosinKanriRecordNew.ToujitsuJyutyuuGaku += (intUriageGakuGoukeiNew - intUriageGakuGoukeiOld)


        Else
            '新旧加盟店コードが異なる場合

            '与信管理レコード取得
            YosinKanriRecordOld = GetYosinKnariRecord(intType, JibanRecordOld.KameitenCd, blnDeleteDefault)
            YosinKanriRecordNew = GetYosinKnariRecord(intType, JibanRecordNew.KameitenCd, blnDeleteDefault)
            If YosinKanriRecordOld Is Nothing Then
                Return EarthConst.YOSIN_ERROR_YOSIN_OK    '与信管理マスタ取得エラー -> とりあえず与信OKとして扱う
            End If
            If YosinKanriRecordNew Is Nothing Then
                Return EarthConst.YOSIN_ERROR_YOSIN_OK    '与信管理マスタ取得エラー -> とりあえず与信OKとして扱う
            End If

            '当日受注額算出
            ' = 現当日受注額 - 旧合計金額
            YosinKanriRecordOld.ToujitsuJyutyuuGaku -= intUriageGakuGoukeiOld
            ' = 現当日受注額 + 新合計金額
            YosinKanriRecordNew.ToujitsuJyutyuuGaku += intUriageGakuGoukeiNew

        End If

        '与信限度額チェック
        Dim intYosinGendoResult As Integer = CheckYosinGendo(YosinKanriRecordNew, _
                                                            intUriageGakuGoukeiOld, _
                                                            intUriageGakuGoukeiNew, _
                                                            intMailSendFlg)
        If intYosinGendoResult = EarthConst.YOSIN_ERROR_YOSIN_NG And flgUpdYosinKanriHissu = False Then
            '戻り値=1：受注管理フラグが設定されていない状態で、与信限度額超過になった場合、与信エラーとして与信管理マスタ更新対象外にする。

        Else
            '与信管理マスタ更新
            Dim intEditYosinKanriResultOld As Integer = 0
            Dim intEditYosinKanriResultNew As Integer = 0
            If YosinKanriRecordOld.NayoseSakiCd IsNot Nothing And JibanRecordOld.KameitenCd IsNot Nothing Then
                '名寄せ先コード、加盟店コードが指定されていた場合のみ
                YosinKanriRecordOld.UpdLoginUserId = JibanRecordNew.UpdLoginUserId
                YosinKanriRecordOld.UpdDatetime = updDateTime
                intEditYosinKanriResultOld = SetEditYosinKanri(YosinKanriRecordOld)
            End If
            YosinKanriRecordNew.UpdLoginUserId = JibanRecordNew.UpdLoginUserId
            YosinKanriRecordNew.UpdDatetime = updDateTime
            intEditYosinKanriResultNew = SetEditYosinKanri(YosinKanriRecordNew)

            If intEditYosinKanriResultOld + intEditYosinKanriResultNew <> 0 Then
                '与信管理マスタ更新エラー
                Return EarthConst.YOSIN_ERROR_YKANRI_UPDATE_ERROR
            End If

        End If

        'メール送信
        If intMailSendFlg > 0 Then
            '与信メール送信先取得
            Dim intMailResult = SendYosinMail(intType, JibanRecordNew.KameitenCd, intYosinGendoResult)
            If intMailResult <> 0 Then
                'メール送信エラー
                Return EarthConst.YOSIN_ERROR_YOSIN_MAIL_ERROR
            End If
        End If

        Return intYosinGendoResult

    End Function
#End Region

#Region "与信限度額チェック"
    ''' <summary>
    ''' 与信限度額チェック
    ''' </summary>
    ''' <param name="YosinKanriRecordNew">与信管理マスタレコード</param>
    ''' <param name="intUriageGakuGoukeiOld">売上金額合計（旧）</param>
    ''' <param name="intUriageGakuGoukeiNew">売上金額合計（新）</param>
    ''' <param name="intMailSendFlg">メール送信フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckYosinGendo(ByRef YosinKanriRecordNew As YosinKanriRecord, _
                                    ByVal intUriageGakuGoukeiOld As Integer, _
                                    ByVal intUriageGakuGoukeiNew As Integer, _
                                    ByRef intMailSendFlg As Integer) As Integer

        '現時点でのDB上の与信警告状況を保持
        Dim intKeikokuJoukyouOld As Integer = YosinKanriRecordNew.KeikokuJoukyou
        '現時点でのDB上の与信警告状況を保持
        Dim dateSoushinDateOld As DateTime = YosinKanriRecordNew.SoushinDate
        '受注合計額算出
        Dim intJutyuuGakuGoukei As New Integer
        intJutyuuGakuGoukei = YosinKanriRecordNew.ZengetsuSaikenGaku + _
                            YosinKanriRecordNew.RuisekiJyutyuuGaku + _
                            YosinKanriRecordNew.ToujitsuJyutyuuGaku - _
                            YosinKanriRecordNew.RuisekiNyuukinGaku

        '与信限度額チェック
        If intUriageGakuGoukeiNew = intUriageGakuGoukeiOld Then
            '画面とDBでの差額がゼロの場合、スルー
        ElseIf intJutyuuGakuGoukei = 0 Then
            '受注合計額がゼロの場合
            '警告状況をリセット
            YosinKanriRecordNew.KeikokuJoukyou = Integer.MinValue
        ElseIf YosinKanriRecordNew.YosinGendoGaku = Integer.MinValue Then
            '与信限度額未設定時はスルー
        Else

            If intJutyuuGakuGoukei > YosinKanriRecordNew.YosinGendoGaku Or YosinKanriRecordNew.YosinGendoGaku = 0 Then
                '与信限度額超過

                '与信警告状況を4:与信オーバーに設定
                YosinKanriRecordNew.KeikokuJoukyou = EarthConst.YOSIN_KEIKOKU_OVER

                If YosinKanriRecordNew.JyutyuuKanriFlg = 0 Then
                    '受注管理フラグが設定されていない場合、与信エラーとして以後の処理は対象外にする。(メールは送信する。)
                    '与信警告メール送信対象か否かをチェック＆設定
                    setMailSousinTaisyou(YosinKanriRecordNew, intMailSendFlg, intKeikokuJoukyouOld, dateSoushinDateOld)
                    '与信NGで返す
                    Return EarthConst.YOSIN_ERROR_YOSIN_NG
                Else
                    '受注管理フラグが設定されている場合、与信超過メールを送信する。以後の処理は継続。
                    '与信警告メール送信対象か否かをチェック＆設定
                    setMailSousinTaisyou(YosinKanriRecordNew, intMailSendFlg, intKeikokuJoukyouOld, dateSoushinDateOld)
                End If

            Else
                '与信限度額以下

                '与信警告率チェック
                Dim deciYosinKeikokuRitu As Decimal = intJutyuuGakuGoukei / YosinKanriRecordNew.YosinGendoGaku

                If YosinKanriRecordNew.YosinKeikouKaisiritsu > 0 Then
                    '与信警告開始率が設定されている場合
                    If deciYosinKeikokuRitu > YosinKanriRecordNew.YosinKeikouKaisiritsu Then
                        '与信警告率 > 与信警告開始率 の場合

                        '警告状況更新
                        GetKeikokuJoukyou(deciYosinKeikokuRitu, YosinKanriRecordNew.KeikokuJoukyou)

                        '与信警告メール送信対象か否かをチェック＆設定
                        setMailSousinTaisyou(YosinKanriRecordNew, intMailSendFlg, intKeikokuJoukyouOld, dateSoushinDateOld)

                    Else
                        '警告状況をリセット(与信警告率が開始率より低い)
                        YosinKanriRecordNew.KeikokuJoukyou = Integer.MinValue
                    End If
                Else
                    '警告状況をリセット(与信警告開始率が設定されていない)
                    YosinKanriRecordNew.KeikokuJoukyou = Integer.MinValue
                End If

            End If
        End If

        Return 0

    End Function
#End Region

#Region "物件破棄時与信処理"
    ''' <summary>
    ''' 物件破棄時与信処理
    ''' </summary>
    ''' <param name="JibanRecordOld">更新前DBのJibanRecord</param>
    ''' <param name="JibanRecordNew">画面入力データのJibanRecord</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function YosinHakiCheck(ByVal JibanRecordOld As JibanRecordBase, _
                                        ByVal JibanRecordNew As JibanRecordBase) As Integer

        If JibanRecordOld.DataHakiSyubetu = JibanRecordNew.DataHakiSyubetu Then
            If JibanRecordOld.DataHakiSyubetu > 0 Then
                '新旧共に破棄
                Return 1
            Else
                '新旧共に破棄種別が設定されていない
                Return 0
            End If
        Else
            Dim arrType As New ArrayList
            arrType.Add(1)  '調査請求先用
            arrType.Add(2)  '工事請求先用
            arrType.Add(3)  '販促品請求先用

            For Each intType As Integer In arrType

                Dim intUriageGakuGoukeiOld As New Integer
                Dim YosinKanriRecordOld As New YosinKanriRecord

                Select Case intType
                    Case 1
                        '旧データ売上金額合計取得
                        intUriageGakuGoukeiOld = GetUriageGoukeiTyousa(JibanRecordOld)
                    Case 2
                        '旧データ売上金額合計取得
                        intUriageGakuGoukeiOld = GetUriageGoukeiKouji(JibanRecordOld)
                    Case 3
                        '旧データ売上金額合計取得
                        intUriageGakuGoukeiOld = GetUriageGoukeiHansoku(JibanRecordOld)
                End Select

                '旧データに紐づく与信管理マスタ取得
                YosinKanriRecordOld = GetYosinKnariRecord(intType, JibanRecordOld.KameitenCd, blnDeleteDefault)
                If JibanRecordOld.DataHakiSyubetu > 0 Then
                    '破棄物件を復活させた場合
                    '各当日受注額に加算
                    If YosinKanriRecordOld IsNot Nothing Then   '与信管理マスタ取得成功時のみ
                        YosinKanriRecordOld.ToujitsuJyutyuuGaku += intUriageGakuGoukeiOld
                    End If
                    '戻り値設定
                    YosinHakiCheck = 2
                Else
                    '物件破棄した場合
                    '各当日受注額から減算
                    If YosinKanriRecordOld IsNot Nothing Then   '与信管理マスタ取得成功時のみ
                        YosinKanriRecordOld.ToujitsuJyutyuuGaku -= intUriageGakuGoukeiOld
                    End If
                    '戻り値設定
                    YosinHakiCheck = 3
                End If

                '与信管理マスタ更新
                If YosinKanriRecordOld IsNot Nothing Then   '与信管理マスタ取得成功時のみ
                    YosinKanriRecordOld.UpdLoginUserId = JibanRecordNew.UpdLoginUserId
                    YosinKanriRecordOld.UpdDatetime = updDateTime
                    Dim intEditYosinKanriResultOld As Integer = SetEditYosinKanri(YosinKanriRecordOld)
                    If intEditYosinKanriResultOld <> 0 Then
                        '与信管理マスタ更新エラー
                        Return EarthConst.YOSIN_ERROR_YKANRI_UPDATE_ERROR
                    End If
                End If
            Next

        End If

    End Function
#End Region

#Region "調査請求先向けの売上金額合計を取得"
    ''' <summary>
    ''' 調査請求先向けの売上金額合計を取得
    ''' </summary>
    ''' <param name="JibanRecord">合計算出対象地盤レコードデータ</param>
    ''' <returns>差額</returns>
    ''' <remarks>当日与信残高増減額取得<br/>
    ''' </remarks>
    Private Function GetUriageGoukeiTyousa(ByVal JibanRecord As JibanRecordBase) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageGoukeiTyousa", _
                                            JibanRecord)

        Dim uriGakuGoukei As Integer = 0
        Dim kijunDate As New DateTime

        '前後30日チェック用基準日を設定
        kijunDate = JibanRecord.TysKibouDate
        'If JibanRecord.SyoudakusyoTysDate <> DateTime.MinValue Then
        '    '承諾書調査日が設定されている場合、基準日に承諾書調査日を使用
        '    kijunDate = JibanRecord.SyoudakusyoTysDate
        'Else
        '    '承諾書調査日が設定されていない場合、基準日に調査希望日を使用
        '    kijunDate = JibanRecord.TysKibouDate
        'End If

        '商品１、２、３の売上合計額取得
        uriGakuGoukei = GetUriageGoukeiSyouhin123(JibanRecord)

        '解約払戻売上額取得
        uriGakuGoukei += GetTeibetuUriGaku(JibanRecord.KaiyakuHaraimodosiRecord, True, kijunDate)

        Return uriGakuGoukei

    End Function

    ''' <summary>
    ''' 調査請求先向けの売上金額合計を取得（商品１，２，３のみ）
    ''' </summary>
    ''' <param name="JibanRecord">合計算出対象地盤レコードデータ</param>
    ''' <returns>差額</returns>
    ''' <remarks>当日与信残高増減額取得<br/>
    ''' </remarks>
    Private Function GetUriageGoukeiSyouhin123(ByVal JibanRecord As JibanRecordBase) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageGoukeiSyouhin123", _
                                            JibanRecord)

        Dim uriGakuGoukei As Integer = 0
        Dim kijunDate As New DateTime
        Dim i As New Integer

        '前後30日チェック用基準日を設定
        kijunDate = JibanRecord.TysKibouDate
        'If JibanRecord.SyoudakusyoTysDate <> DateTime.MinValue Then
        '    '承諾書調査日が設定されている場合、基準日に承諾書調査日を使用
        '    kijunDate = JibanRecord.SyoudakusyoTysDate
        'Else
        '    '承諾書調査日が設定されていない場合、基準日に調査希望日を使用
        '    kijunDate = JibanRecord.TysKibouDate
        'End If

        '商品1の売上金額合計を加算
        uriGakuGoukei = GetTeibetuUriGaku(JibanRecord.Syouhin1Record, True, kijunDate)

        '商品2の売上金額合計を加算
        If JibanRecord.Syouhin2Records IsNot Nothing Then
            i = 1
            For i = 1 To 4
                If JibanRecord.Syouhin2Records.ContainsKey(i) Then
                    uriGakuGoukei += GetTeibetuUriGaku(JibanRecord.Syouhin2Records(i), True, kijunDate)
                End If
            Next
        End If

        '商品3の売上金額合計を加算
        If JibanRecord.Syouhin3Records IsNot Nothing Then
            i = 1
            For i = 1 To 9
                If JibanRecord.Syouhin3Records.ContainsKey(i) Then
                    uriGakuGoukei += GetTeibetuUriGaku(JibanRecord.Syouhin3Records(i), True, kijunDate)
                End If
            Next
        End If

        Return uriGakuGoukei

    End Function
#End Region

#Region "工事請求先向けの売上金額合計を取得"
    ''' <summary>
    ''' 工事請求先向けの売上金額合計取得
    ''' </summary>
    ''' <param name="JibanRecord">合計算出対象地盤レコードデータ</param>
    ''' <returns>差額</returns>
    ''' <remarks>当日与信残高増減額取得<br/>
    ''' </remarks>
    Private Function GetUriageGoukeiKouji(ByVal JibanRecord As JibanRecordBase) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageGoukeiKouji", _
                                            JibanRecord)

        Dim uriGakuGoukei As Integer = 0
        Dim i As Integer = 1
        Dim KoujiTeibetuRecord As New TeibetuSeikyuuRecord
        Dim KojKanryYoteiDate As New DateTime
        Dim KojGaisyaSeikyuuUmu As New Integer
        Dim arrType As New ArrayList
        arrType.Add(1)  '改良工事
        arrType.Add(2)  '追加改良工事

        For Each intType As Integer In arrType

            Select Case intType
                Case 1
                    '改良工事
                    KoujiTeibetuRecord = JibanRecord.KairyouKoujiRecord
                    KojKanryYoteiDate = JibanRecord.KairyKojKanryYoteiDate
                    KojGaisyaSeikyuuUmu = JibanRecord.KojGaisyaSeikyuuUmu
                Case 2
                    '追加改良工事
                    KoujiTeibetuRecord = JibanRecord.TuikaKoujiRecord
                    KojKanryYoteiDate = JibanRecord.TKojKanryYoteiDate
                    KojGaisyaSeikyuuUmu = JibanRecord.TKojKaisyaSeikyuuUmu
            End Select
            If KojGaisyaSeikyuuUmu <> 1 Then
                '工事、追加工事の売上金額合計を加算
                uriGakuGoukei += GetTeibetuUriGaku(KoujiTeibetuRecord, True, KojKanryYoteiDate)
            End If
        Next

        GetUriageGoukeiKouji = uriGakuGoukei

    End Function
#End Region

#Region "販促品請求先向けの売上金額合計を取得"
    ''' <summary>
    ''' 販促品請求先向けの売上金額合計取得
    ''' </summary>
    ''' <param name="JibanRecord">合計算出対象地盤レコードデータ</param>
    ''' <returns>差額</returns>
    ''' <remarks>当日与信残高増減額取得<br/>
    ''' </remarks>
    Private Function GetUriageGoukeiHansoku(ByVal JibanRecord As JibanRecordBase) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageGoukeiHansoku", _
                                            JibanRecord)

        Dim uriGakuGoukei As Integer = 0
        Dim i As Integer = 1

        uriGakuGoukei += GetTeibetuUriGaku(JibanRecord.TyousaHoukokusyoRecord)
        uriGakuGoukei += GetTeibetuUriGaku(JibanRecord.KoujiHoukokusyoRecord)
        uriGakuGoukei += GetTeibetuUriGaku(JibanRecord.HosyousyoRecord)

        Return uriGakuGoukei

    End Function
#End Region

#Region "邸別請求レコードから売上金額を取得"
    ''' <summary>
    ''' 邸別請求レコードから売上金額を取得
    ''' 邸別請求レコードがNothinguの場合、ゼロ返す
    ''' </summary>
    ''' <param name="TeibetuSeikyuuRecord">合計算出対象地盤レコードデータ</param>
    ''' <param name="flgKijunDateCheck">与信チェック対象基準日付のチェックを行うかを指定(チェックを行う場合、True)</param>
    ''' <param name="kijunDate">与信チェック対象基準日付(調査、工事の前後30日チェック用)</param>
    ''' <returns>差額</returns>
    ''' <remarks>当日与信残高増減額取得<br/>
    ''' </remarks>
    Private Function GetTeibetuUriGaku(ByVal TeibetuSeikyuuRecord As TeibetuSeikyuuRecord, _
                                       Optional ByVal flgKijunDateCheck As Boolean = False, _
                                       Optional ByVal kijunDate As DateTime = Nothing) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuUriGaku", _
                                            TeibetuSeikyuuRecord)

        If TeibetuSeikyuuRecord Is Nothing Then
            '邸別請求レコードが無い場合、金額ゼロ円でReturn
            Return 0
        Else
            '与信チェック対象基準日付が指定されている場合、基準日前後30日チェックを行う
            If flgKijunDateCheck Then
                If TeibetuSeikyuuRecord.UriDate <> DateTime.MinValue Then
                    '売上年月日が入力されている場合、基準日に関わらず売上金額を取得
                Else
                    '売上年月日が入力されていない場合で、
                    Select Case kijunDate
                        Case updDateTime.AddDays(-30) To updDateTime.AddDays(30)
                            '工事完了予定日がシステム日付の前後30日の場合、売上金額を取得
                        Case Else
                            'その他の場合、金額ゼロ円でReturn
                            Return 0
                    End Select
                End If
            End If

            '売上金額をReturn
            Return TeibetuSeikyuuRecord.ZeikomiUriGaku
        End If

    End Function
#End Region

#Region "与信管理マスタ情報を取得"
    ''' <summary>
    ''' 与信管理マスタ情報を取得
    ''' </summary>
    ''' <param name="intSeikyusakiType">1:調査請求先、2:工事請求先、3:販促品請求先</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetYosinKnariRecord(ByVal intSeikyusakiType As Integer, _
                                         ByVal strKameitenCd As String, _
                                         ByVal blnDelete As Boolean) As YosinKanriRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinKnariRecord", _
                                            intSeikyusakiType, _
                                            strKameitenCd, _
                                            blnDelete)

        Dim dataAccess As YosinDataAccess = New YosinDataAccess

        Dim table As DataTable = dataAccess.GetYosinKanriData(intSeikyusakiType, strKameitenCd, blnDelete)
        Dim record As New YosinKanriRecord

        Dim arrRtnData As List(Of YosinKanriRecord) = clsDMH.getMapArray(Of YosinKanriRecord)(GetType(YosinKanriRecord), table)

        If arrRtnData.Count > 0 Then
            If arrRtnData(0).NayoseSakiCd Is Nothing OrElse arrRtnData(0).NayoseSakiCd = String.Empty Then
                record = Nothing
            Else
                record = arrRtnData(0)
            End If
        Else
            record = Nothing
        End If

        Return record

    End Function
#End Region

#Region "警告状況を取得"
    ''' <summary>
    ''' 警告状況を取得
    ''' </summary>
    ''' <param name="deciYosinKeikokuRitu">与信警告率(受注額合計/与信限度額)</param>
    ''' <param name="KeikokuJoukyou">警告状況</param>
    ''' <remarks></remarks>
    Private Sub GetKeikokuJoukyou(ByVal deciYosinKeikokuRitu As Decimal, ByRef KeikokuJoukyou As Integer)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKeikokuJoukyou", _
                                            deciYosinKeikokuRitu, _
                                            KeikokuJoukyou)

        '与信警告状況を設定
        If deciYosinKeikokuRitu >= 0.9 Then
            KeikokuJoukyou = EarthConst.YOSIN_KEIKOKU_90OVER
        ElseIf deciYosinKeikokuRitu >= 0.8 Then
            KeikokuJoukyou = EarthConst.YOSIN_KEIKOKU_80OVER
        ElseIf deciYosinKeikokuRitu >= 0.7 Then
            KeikokuJoukyou = EarthConst.YOSIN_KEIKOKU_70OVER
        Else
            KeikokuJoukyou = Integer.MinValue
        End If

    End Sub
#End Region

#Region "与信管理マスタ更新呼び出し"
    ''' <summary>
    ''' 与信管理マスタ更新呼び出し
    ''' </summary>
    ''' <param name="YosinKanriRecord">更新するデータの入った与信管理レコード</param>
    ''' <remarks></remarks>
    Private Function SetEditYosinKanri(ByVal YosinKanriRecord As YosinKanriRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetEditYosinKanri", _
                                                  YosinKanriRecord)

        '名寄せ先コードが無い場合、終了
        If YosinKanriRecord.NayoseSakiCd Is Nothing Then
            Return EarthConst.YOSIN_ERROR_YKANRI_UPDATE_ERROR
        End If

        If YosinKanriRecord.KeikokuJoukyou = 0 Then
            YosinKanriRecord.KeikokuJoukyou = Integer.MinValue
        End If

        Dim YosinDataAccess As YosinDataAccess = New YosinDataAccess
        Dim result As Integer = YosinDataAccess.EditYosinKanri(YosinKanriRecord.NayoseSakiCd, _
                                                YosinKanriRecord.KeikokuJoukyou, _
                                                YosinKanriRecord.SoushinDate, _
                                                YosinKanriRecord.ToujitsuJyutyuuGaku, _
                                                YosinKanriRecord.UpdLoginUserId, _
                                                YosinKanriRecord.UpdDatetime)
        Return IIf(result = 1, 0, result)
    End Function
#End Region

#Region "チェック結果メール送信対象チェック＆設定"
    ''' <summary>
    ''' 与信チェック結果のメールを送信するか否かをチェックし、必要情報をセットする
    ''' </summary>
    ''' <param name="YosinKanriRecordNew"></param>
    ''' <param name="intMailSendFlg"></param>
    ''' <param name="intKeikokuJoukyouOld"></param>
    ''' <param name="dateSoushinDateOld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setMailSousinTaisyou(ByRef YosinKanriRecordNew As YosinKanriRecord, _
                                          ByRef intMailSendFlg As Integer, _
                                          ByVal intKeikokuJoukyouOld As Integer, _
                                          ByVal dateSoushinDateOld As DateTime) As Boolean

        'メールが一度でも送信されているかを、送信日時からチェック
        Dim flgMailSousinZumi As Boolean = (YosinKanriRecordNew.SoushinDate <> DateTime.MinValue)

        'メールが一度も送信されていない、もしくは新しい警告状況が現在より上位である場合、メール送信対象とする
        If Not flgMailSousinZumi OrElse intKeikokuJoukyouOld < YosinKanriRecordNew.KeikokuJoukyou Then
            'メール送信指示フラグをセット
            intMailSendFlg = 1
            '送信日付を更新
            YosinKanriRecordNew.SoushinDate = updDateTime
        Else
            intMailSendFlg = 0
        End If

    End Function
#End Region

#Region "チェック結果メール送信先取得"
    ''' <summary>
    ''' チェック結果メール送信先取得
    ''' </summary>
    ''' <param name="intSeikyusakiType">1:調査請求先、2:工事請求先、3:販促品請求先</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="intYosinGendoResult">与信限度額チェック結果（1：与信超過＆受注管理フラグ無し）</param>
    ''' <returns>メール送信先レコード</returns>
    ''' <remarks></remarks>
    Private Function GetYosinMailTargetRecord(ByVal intSeikyusakiType As Integer, _
                                              ByVal strKameitenCd As String, _
                                              ByVal intYosinGendoResult As Integer, _
                                              ByVal blnDelete As Boolean) As List(Of YosinMailTargetRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinMailTargetRecord", _
                                            intSeikyusakiType, _
                                            strKameitenCd, _
                                            intYosinGendoResult, _
                                            blnDelete)

        Dim dataAccess As YosinDataAccess = New YosinDataAccess

        Dim table As DataTable = dataAccess.GetYosinMailTarget(intSeikyusakiType, strKameitenCd, intYosinGendoResult, blnDelete)

        Dim arrRtnData As List(Of YosinMailTargetRecord) = clsDMH.getMapArray(Of YosinMailTargetRecord)(GetType(YosinMailTargetRecord), table)

        Return arrRtnData

    End Function

#End Region

#Region "加盟店営業担当者メール送信先取得"
    ''' <summary>
    ''' 加盟店営業担当者メール送信先取得
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns>加盟店営業担当者メール送信先レコード</returns>
    ''' <remarks></remarks>
    Private Function GetYosinEigyouTantousyaMailTargetRecord(ByVal strKameitenCd As String, _
                                                             ByVal blnDelete As Boolean) As YosinEigyouTantousyaMailTargetRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinKanrisyaRecord", _
                                                   blnDelete)

        Dim dataAccess As YosinDataAccess = New YosinDataAccess

        Dim table As DataTable = dataAccess.GetYosinEigyouTantousyaMailTarget(strKameitenCd, blnDelete)

        Dim arrRtnData As List(Of YosinEigyouTantousyaMailTargetRecord) = clsDMH.getMapArray(Of YosinEigyouTantousyaMailTargetRecord)( _
                                                                          GetType(YosinEigyouTantousyaMailTargetRecord), table)

        If arrRtnData.Count >= 1 Then
            Return arrRtnData(0)
        Else
            Return Nothing
        End If

    End Function

#End Region

#Region "与信管理者マスタ取得"
    ''' <summary>
    ''' 与信管理者マスタ取得
    ''' </summary>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns>与信管理者レコード</returns>
    ''' <remarks></remarks>
    Private Function GetYosinKanrisyaRecord(ByVal blnDelete As Boolean) As List(Of YosinKanrisyaRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetYosinKanrisyaRecord", _
                                                   blnDelete)

        Dim dataAccess As YosinDataAccess = New YosinDataAccess

        Dim table As DataTable = dataAccess.GetYosinKanrisyaData(blnDelete)

        Dim arrRtnData As List(Of YosinKanrisyaRecord) = clsDMH.getMapArray(Of YosinKanrisyaRecord)(GetType(YosinKanrisyaRecord), table)

        Return arrRtnData

    End Function

#End Region

#Region "チェック結果メール送信処理"
    ''' <summary>
    ''' チェック結果メール送信処理
    ''' </summary>
    ''' <param name="intSeikyusakiType">1:調査請求先、2:工事請求先、3:販促品請求先</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="intYosinGendoResult">与信限度額チェック結果（1：与信超過＆受注管理フラグ無し）</param>
    ''' <returns>メール送信結果</returns>
    ''' <remarks></remarks>
    Private Function SendYosinMail(ByVal intSeikyusakiType As Integer, _
                                    ByVal strKameitenCd As String, _
                                    ByVal intYosinGendoResult As Integer) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SendYosinMail", _
                                                    intSeikyusakiType, _
                                                    strKameitenCd, _
                                                    intYosinGendoResult)

        Dim intResult As Integer = 0
        Dim strResult As String = String.Empty

        Dim SendMailHelper As New SendMailHelper

        Dim MailServerName As String = String.Empty
        Dim MailFromAddress As String = String.Empty
        Dim MailToAddressSb As New StringBuilder()
        Dim MailSubject As String = String.Empty
        Dim MailBody As String = String.Empty
        Dim dummyFileUpload As New System.Web.UI.WebControls.FileUpload

        'web.configからメール設定情報を取得
        MailServerName = ConfigurationManager.AppSettings("MailServerName")
        MailFromAddress = ConfigurationManager.AppSettings("MailFromAddress")
        MailSubject = ConfigurationManager.AppSettings("YosinMailSubject")
        MailBody = ConfigurationManager.AppSettings("YosinMailBody")

        '与信メール送信先取得
        Dim YosinMailTargetRecordList As List(Of YosinMailTargetRecord) = _
                GetYosinMailTargetRecord(intSeikyusakiType, strKameitenCd, intYosinGendoResult, blnDeleteDefault)
        '宛先データチェック(送信先情報が取得できなかった場合、エラー)
        If YosinMailTargetRecordList Is Nothing Then
            SendYosinMail = EarthConst.YOSIN_ERROR_YOSIN_MAIL_ERROR
            Exit Function
        End If
        If YosinMailTargetRecordList.Count <= 0 Then
            SendYosinMail = EarthConst.YOSIN_ERROR_YOSIN_MAIL_ERROR
            Exit Function
        End If
        '宛先設定
        For Each mailRec As YosinMailTargetRecord In YosinMailTargetRecordList
            If mailRec.EmailAddresses IsNot Nothing Then
                MailToAddressSb.Append(mailRec.EmailAddresses)
                MailToAddressSb.Append(",")
            End If
        Next

        '加盟店営業担当者メール送信先取得
        Dim YosinEigyouTantousyaMailTargetRecordList As YosinEigyouTantousyaMailTargetRecord = _
                GetYosinEigyouTantousyaMailTargetRecord(strKameitenCd, blnDeleteDefault)
        '宛先データチェック -> 宛先設定
        If YosinEigyouTantousyaMailTargetRecordList Is Nothing OrElse _
            YosinEigyouTantousyaMailTargetRecordList.EmailAddresses Is Nothing OrElse _
            YosinEigyouTantousyaMailTargetRecordList.EmailAddresses = "" Then
            '取得できなかった場合、追加しない
        Else
            MailToAddressSb.Append(YosinEigyouTantousyaMailTargetRecordList.EmailAddresses)
            MailToAddressSb.Append(",")
        End If

        '与信管理者マスタ取得 -> 宛先設定
        Dim YosinKanrisyaRecordList As List(Of YosinKanrisyaRecord) = GetYosinKanrisyaRecord(blnDeleteDefault)
        For Each mailRec As YosinKanrisyaRecord In YosinKanrisyaRecordList
            MailToAddressSb.Append(mailRec.EmailAddresses)
            MailToAddressSb.Append(",")
        Next


        '件名設定
        MailSubject = MailSubject.Replace("@KEIKOKU@", YosinMailTargetRecordList(0).Meisyou)
        MailSubject = MailSubject.Replace("@KAMEITENCD@", YosinMailTargetRecordList(0).KameitenCd)
        MailSubject = MailSubject.Replace("@KAMEITENMEI1@", YosinMailTargetRecordList(0).KameitenMei1)

        '本文設定
        MailBody = MailBody.Replace("@KEIKOKU@", YosinMailTargetRecordList(0).Meisyou)
        MailBody = MailBody.Replace("@KAMEITENCD@", YosinMailTargetRecordList(0).KameitenCd)
        MailBody = MailBody.Replace("@KAMEITENMEI1@", YosinMailTargetRecordList(0).KameitenMei1)
        MailBody = MailBody.Replace("@NAYOSESAKICD@", YosinMailTargetRecordList(0).NayoseSakiCd)
        MailBody = MailBody.Replace("@NAYOSESAKINAME1@", YosinMailTargetRecordList(0).NayoseSakiName1)
        MailBody = MailBody.Replace("@EARTH2PATH@", ConfigurationManager.AppSettings("Earth2Path"))

        'メール送信実行
        strResult = SendMailHelper.SendMail(MailServerName, _
                            MailFromAddress, _
                            MailToAddressSb.ToString(), _
                            "", _
                            "", _
                            MailSubject, _
                            MailBody, _
                            dummyFileUpload)

        ' エラーがない場合、終了メッセージを表示する
        If strResult = "" Then
            intResult = 0
        Else
            intResult = EarthConst.YOSIN_ERROR_YOSIN_MAIL_ERROR
        End If

        Return intResult

    End Function

#End Region

#Region "【進捗データ連携バッチ実行時の与信チェック】"

#Region "与信チェック対象管理テーブル追加 呼び出し"
    ''' <summary>
    ''' 与信チェック対象管理テーブル追加 呼び出し
    ''' </summary>
    ''' <param name="strSyoriId">処理ID(進捗データ連携バッチ処理ID)</param>
    ''' <param name="flgRecvSts">進捗テーブル受信ステータス 対象フラグ</param>
    ''' <remarks></remarks>
    Public Function SetInsertYosinCheckTaisyouKanri(ByVal strSyoriId As String, _
                                                     ByVal flgRecvSts As Integer) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetInsertYosinCheckTaisyouKanri", _
                                                    strSyoriId, _
                                                    flgRecvSts)

        Dim result As Integer

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

            '処理IDが無い場合、終了
            If strSyoriId = String.Empty Then
                Return 9
            End If

            Dim YosinDataAccess As YosinDataAccess = New YosinDataAccess
            result = YosinDataAccess.DeleteYosinCheckTaisyouKanri()
            result = YosinDataAccess.InsertYosinCheckTaisyouKanri(strSyoriId, flgRecvSts)

            ' 更新に成功した場合、トランザクションをコミットする
            scope.Complete()

        End Using

        Return result

    End Function
#End Region

#Region "与信チェック対象管理テーブルからの与信チェック"
    ''' <summary>
    ''' 与信チェック対象管理テーブル追加 呼び出し
    ''' </summary>
    ''' <param name="strSyoriId">処理ID(進捗データ連携バッチ処理ID)</param>
    ''' <remarks></remarks>
    Public Function YosinCheckFromTaisyouKanri(ByVal strSyoriId As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".YosinCheckFromTaisyouKanri", _
                                                    strSyoriId)

        '戻り値
        Dim intReturn As Integer = EarthConst.YOSIN_ERROR_YOSIN_OK

        Dim YosinDataAccess As YosinDataAccess = New YosinDataAccess
        Dim JibanLogic As New JibanLogic

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

            '処理IDが無い場合、終了
            If strSyoriId = String.Empty Then
                Return 9
            End If

            '与信チェック対象管理テーブル取得
            Dim table As DataTable = YosinDataAccess.GetYosinCheckTaisyouKanriData(strSyoriId)
            Dim arrRtnData As List(Of YosinCheckTaisyouKanriRecord) = _
                    clsDMH.getMapArray(Of YosinCheckTaisyouKanriRecord)(GetType(YosinCheckTaisyouKanriRecord), table)

            '物件数分ループ
            Dim intDBCnt As Integer
            Dim dbRec As YosinCheckTaisyouKanriRecord
            Dim JibanRecordOld As New JibanRecord
            Dim JibanRecordNew As New JibanRecord


            '新旧の売上金額合計
            Dim intUriageGakuGoukeiOld As New Integer
            Dim intUriageGakuGoukeiNew As New Integer

            '新旧加盟店に紐づく与信管理レコード
            Dim YosinKanriRecordOld As New YosinKanriRecord
            Dim YosinKanriRecordNew As New YosinKanriRecord

            'メール送信先リスト
            Dim YosinMailTargetRecordList As New List(Of YosinMailTargetRecord)

            For intDBCnt = 0 To arrRtnData.Count - 1
                dbRec = arrRtnData(intDBCnt)
                JibanRecordNew = JibanLogic.GetJibanData(dbRec.Kbn, dbRec.HosyousyoNo)
                JibanRecordOld = JibanRecordNew

                '調査請求先向け(商品１，２，３、解約払戻)
                intUriageGakuGoukeiOld = dbRec.TysUriGakuGoukei
                intUriageGakuGoukeiNew = GetUriageGoukeiTyousa(JibanRecordNew)
                intReturn = YosinCheckForType(1, JibanRecordNew, JibanRecordOld, intUriageGakuGoukeiNew, _
                                              intUriageGakuGoukeiOld, YosinMailTargetRecordList, True)

            Next

            ' 更新に成功した場合、トランザクションをコミットする
            scope.Complete()

        End Using

        If intReturn <> EarthConst.YOSIN_ERROR_YOSIN_OK Then
            Return intReturn
        End If

        Return EarthConst.YOSIN_ERROR_YOSIN_OK

    End Function
#End Region

#End Region

End Class
