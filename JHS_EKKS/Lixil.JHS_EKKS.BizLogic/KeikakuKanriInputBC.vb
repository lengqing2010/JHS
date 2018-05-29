Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess
Imports Lixil.JHS_EKKS.Utilities
Imports System.Text.RegularExpressions

''' <summary>
''' 計画管理 CSV取込
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriInputBC
    Private objCommonDA As New CommonDA
    Private objKeikakuKanriInputDA As New DataAccess.KeikakuKanriInputDA

    Private Const csvKeikaku As String = "B1"
    Private Const csvMikomi As String = "B2"
    Private Const csvFcKeikaku As String = "B3"
    Private Const csvFcMikomi As String = "B4"

    '使用禁止文字列配列
    Private arrayKinsiStr() As String = New String() {vbTab, """", "，", "'", "<", ">", "&", "$$$"}
    'CSVアップロード上限件数
    Private CsvInputMaxLineCount As Integer = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings("CsvInputMaxLineCount"))

    '「計画管理テーブル」の必須入力項目索引
    Private KEIKAKU_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 4, 5, 6, 7}
    '「予定見込管理テーブル」の必須入力項目索引
    Private MIKOMI_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 4, 5, 6, 7}
    '「FC用計画管理テーブル」の必須入力項目索引
    Private FC_KEIKAKU_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 4, 5, 6}
    '「FC用予定見込管理テーブル」の必須入力項目索引
    Private FC_MIKOMI_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 4, 5, 6}

    '計画CSVの項目最大長
    Private KEIKAKU_MAX_LENGTH() As Integer = {2, 40, 1, 4, 8, 8, 40, 3, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12}
    '見込CSVの項目最大長
    Private MIKOMI_MAX_LENGTH() As Integer = {2, 40, 1, 4, 8, 8, 40, 3, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12}
    'ＦＣ用計画CSVの項目最大長
    Private FC_KEIKAKU_MAX_LENGTH() As Integer = {2, 40, 4, 4, 8, 40, 3, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12}
    'ＦＣ用見込CSVの項目最大長
    Private FC_MIKOMI_MAX_LENGTH() As Integer = {2, 40, 4, 4, 8, 40, 3, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12}

    '計画CSVの数値型項目索引
    Private KEIKAKU_NUM_INDEX() As Integer = {8, 9, 13, 14, 15, 16, 17, 21, 22, 23, 24, 25, 29, 30, 31, 32, 33, 37, 38, 39, 40, 41, 45, 46, 47, 48, 49, 53, 54, 55, 56, 57, 61, 62, 63, 64, 65, 69, 70, 71, 72, 73, 77, 78, 79, 80, 81, 85, 86, 87, 88, 89, 93, 94, 95, 96, 97, 101, 102, 103}
    '見込CSVの数値型項目索引
    Private MIKOMI_NUM_INDEX() As Integer = {8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43}
    'ＦＣ用計画CSVの数値型項目索引
    Private FC_KEIKAKU_NUM_INDEX() As Integer = {7, 8, 12, 13, 14, 15, 16, 20, 21, 22, 23, 24, 28, 29, 30, 31, 32, 36, 37, 38, 39, 40, 44, 45, 46, 47, 48, 52, 53, 54, 55, 56, 60, 61, 62, 63, 64, 68, 69, 70, 71, 72, 76, 77, 78, 79, 80, 84, 85, 86, 87, 88, 92, 93, 94, 95, 96, 100, 101, 102}
    'ＦＣ用見込CSVの数値型項目索引
    Private FC_MIKOMI_NUM_INDEX() As Integer = {7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42}

    '計画CSVの小数型項目索引
    Private KEIKAKU_DEC_INDEX() As Integer = {10, 11, 12, 18, 19, 20, 26, 27, 28, 34, 35, 36, 42, 43, 44, 50, 51, 52, 58, 59, 60, 66, 67, 68, 74, 75, 76, 82, 83, 84, 90, 91, 92, 98, 99, 100}
    'ＦＣ用計画CSVの小数型項目索引
    Private FC_KEIKAKU_DEC_INDEX() As Integer = {9, 10, 11, 17, 18, 19, 25, 26, 27, 33, 34, 35, 41, 42, 43, 49, 50, 51, 57, 58, 59, 65, 66, 67, 73, 74, 75, 81, 82, 83, 89, 90, 91, 97, 98, 99}

    '計画CSVファイル.項目名
    Private Enum csvKeikakuItems
        keikaku_nendo = 3                   '計画_年度
        kameiten_cd                         '加盟店ｺｰﾄﾞ
        keikaku_kanri_syouhin_cd            '計画管理_商品コード
        kameiten_mei                        '加盟店名
        bunbetu_cd                          '分別コード

        gatu4_keisanyou_uri_heikin_tanka    '4月_計算用__売上平均単価
        gatu4_keisanyou_siire_heikin_tanka  '4月_計算用__仕入平均単価

        gatu4_keisanyou_koj_hantei_ritu     '4月_計算用_工事判定率
        gatu4_keisanyou_koj_jyuchuu_ritu    '4月_計算用_工事受注率
        gatu4_keisanyou_tyoku_koj_ritu      '4月_計算用_直工事率
        gatu4_keikaku_kensuu                '4月_計画件数
        gatu4_keikaku_kingaku               '4月_計画金額
        gatu4_keikaku_arari                 '4月_計画粗利

        gatu5_keisanyou_uri_heikin_tanka    '5月_計算用__売上平均単価
        gatu5_keisanyou_siire_heikin_tanka  '5月_計算用__仕入平均単価

        gatu5_keisanyou_koj_hantei_ritu     '5月_計算用_工事判定率
        gatu5_keisanyou_koj_jyuchuu_ritu    '5月_計算用_工事受注率
        gatu5_keisanyou_tyoku_koj_ritu      '5月_計算用_直工事率
        gatu5_keikaku_kensuu                '5月_計画件数
        gatu5_keikaku_kingaku               '5月_計画金額
        gatu5_keikaku_arari                 '5月_計画粗利

        gatu6_keisanyou_uri_heikin_tanka    '6月_計算用__売上平均単価
        gatu6_keisanyou_siire_heikin_tanka  '6月_計算用__仕入平均単価

        gatu6_keisanyou_koj_hantei_ritu     '6月_計算用_工事判定率
        gatu6_keisanyou_koj_jyuchuu_ritu    '6月_計算用_工事受注率
        gatu6_keisanyou_tyoku_koj_ritu      '6月_計算用_直工事率
        gatu6_keikaku_kensuu                '6月_計画件数
        gatu6_keikaku_kingaku               '6月_計画金額
        gatu6_keikaku_arari                 '6月_計画粗利

        gatu7_keisanyou_uri_heikin_tanka    '7月_計算用__売上平均単価
        gatu7_keisanyou_siire_heikin_tanka  '7月_計算用__仕入平均単価

        gatu7_keisanyou_koj_hantei_ritu     '7月_計算用_工事判定率
        gatu7_keisanyou_koj_jyuchuu_ritu    '7月_計算用_工事受注率
        gatu7_keisanyou_tyoku_koj_ritu      '7月_計算用_直工事率
        gatu7_keikaku_kensuu                '7月_計画件数
        gatu7_keikaku_kingaku               '7月_計画金額
        gatu7_keikaku_arari                 '7月_計画粗利

        gatu8_keisanyou_uri_heikin_tanka    '8月_計算用__売上平均単価
        gatu8_keisanyou_siire_heikin_tanka  '8月_計算用__仕入平均単価

        gatu8_keisanyou_koj_hantei_ritu     '8月_計算用_工事判定率
        gatu8_keisanyou_koj_jyuchuu_ritu    '8月_計算用_工事受注率
        gatu8_keisanyou_tyoku_koj_ritu      '8月_計算用_直工事率
        gatu8_keikaku_kensuu                '8月_計画件数
        gatu8_keikaku_kingaku               '8月_計画金額
        gatu8_keikaku_arari                 '8月_計画粗利

        gatu9_keisanyou_uri_heikin_tanka    '9月_計算用__売上平均単価
        gatu9_keisanyou_siire_heikin_tanka  '9月_計算用__仕入平均単価

        gatu9_keisanyou_koj_hantei_ritu     '9月_計算用_工事判定率
        gatu9_keisanyou_koj_jyuchuu_ritu    '9月_計算用_工事受注率
        gatu9_keisanyou_tyoku_koj_ritu      '9月_計算用_直工事率
        gatu9_keikaku_kensuu                '9月_計画件数
        gatu9_keikaku_kingaku               '9月_計画金額
        gatu9_keikaku_arari                 '9月_計画粗利

        gatu10_keisanyou_uri_heikin_tanka    '10月_計算用__売上平均単価
        gatu10_keisanyou_siire_heikin_tanka  '10月_計算用__仕入平均単価

        gatu10_keisanyou_koj_hantei_ritu    '10月_計算用_工事判定率
        gatu10_keisanyou_koj_jyuchuu_ritu   '10月_計算用_工事受注率
        gatu10_keisanyou_tyoku_koj_ritu     '10月_計算用_直工事率
        gatu10_keikaku_kensuu               '10月_計画件数
        gatu10_keikaku_kingaku              '10月_計画金額
        gatu10_keikaku_arari                '10月_計画粗利

        gatu11_keisanyou_uri_heikin_tanka    '11月_計算用__売上平均単価
        gatu11_keisanyou_siire_heikin_tanka  '11月_計算用__仕入平均単価

        gatu11_keisanyou_koj_hantei_ritu    '11月_計算用_工事判定率
        gatu11_keisanyou_koj_jyuchuu_ritu   '11月_計算用_工事受注率
        gatu11_keisanyou_tyoku_koj_ritu     '11月_計算用_直工事率
        gatu11_keikaku_kensuu               '11月_計画件数
        gatu11_keikaku_kingaku              '11月_計画金額
        gatu11_keikaku_arari                '11月_計画粗利

        gatu12_keisanyou_uri_heikin_tanka    '12月_計算用__売上平均単価
        gatu12_keisanyou_siire_heikin_tanka  '12月_計算用__仕入平均単価

        gatu12_keisanyou_koj_hantei_ritu    '12月_計算用_工事判定率
        gatu12_keisanyou_koj_jyuchuu_ritu   '12月_計算用_工事受注率
        gatu12_keisanyou_tyoku_koj_ritu     '12月_計算用_直工事率
        gatu12_keikaku_kensuu               '12月_計画件数
        gatu12_keikaku_kingaku              '12月_計画金額
        gatu12_keikaku_arari                '12月_計画粗利

        gatu1_keisanyou_uri_heikin_tanka    '1月_計算用__売上平均単価
        gatu1_keisanyou_siire_heikin_tanka  '1月_計算用__仕入平均単価

        gatu1_keisanyou_koj_hantei_ritu     '1月_計算用_工事判定率
        gatu1_keisanyou_koj_jyuchuu_ritu    '1月_計算用_工事受注率
        gatu1_keisanyou_tyoku_koj_ritu      '1月_計算用_直工事率
        gatu1_keikaku_kensuu                '1月_計画件数
        gatu1_keikaku_kingaku               '1月_計画金額
        gatu1_keikaku_arari                 '1月_計画粗利

        gatu2_keisanyou_uri_heikin_tanka    '2月_計算用__売上平均単価
        gatu2_keisanyou_siire_heikin_tanka  '2月_計算用__仕入平均単価

        gatu2_keisanyou_koj_hantei_ritu     '2月_計算用_工事判定率
        gatu2_keisanyou_koj_jyuchuu_ritu    '2月_計算用_工事受注率
        gatu2_keisanyou_tyoku_koj_ritu      '2月_計算用_直工事率
        gatu2_keikaku_kensuu                '2月_計画件数
        gatu2_keikaku_kingaku               '2月_計画金額
        gatu2_keikaku_arari                 '2月_計画粗利

        gatu3_keisanyou_uri_heikin_tanka    '3月_計算用__売上平均単価
        gatu3_keisanyou_siire_heikin_tanka  '3月_計算用__仕入平均単価

        gatu3_keisanyou_koj_hantei_ritu     '3月_計算用_工事判定率
        gatu3_keisanyou_koj_jyuchuu_ritu    '3月_計算用_工事受注率
        gatu3_keisanyou_tyoku_koj_ritu      '3月_計算用_直工事率
        gatu3_keikaku_kensuu                '3月_計画件数
        gatu3_keikaku_kingaku               '3月_計画金額
        gatu3_keikaku_arari                 '3月_計画粗利
    End Enum

    '見込CSVファイル.項目名
    Private Enum csvMikomiItems
        keikaku_nendo = 3               '計画_年度
        kameiten_cd                     '加盟店ｺｰﾄﾞ
        keikaku_kanri_syouhin_cd        '計画管理_商品コード
        kameiten_mei                    '加盟店名
        bunbetu_cd                      '分別コード
        gatu4_mikomi_kensuu             '4月_見込件数
        gatu4_mikomi_kingaku            '4月_見込金額
        gatu4_mikomi_arari              '4月_見込粗利
        gatu5_mikomi_kensuu             '5月_見込件数
        gatu5_mikomi_kingaku            '5月_見込金額
        gatu5_mikomi_arari              '5月_見込粗利
        gatu6_mikomi_kensuu             '6月_見込件数
        gatu6_mikomi_kingaku            '6月_見込金額
        gatu6_mikomi_arari              '6月_見込粗利
        gatu7_mikomi_kensuu             '7月_見込件数
        gatu7_mikomi_kingaku            '7月_見込金額
        gatu7_mikomi_arari              '7月_見込粗利
        gatu8_mikomi_kensuu             '8月_見込件数
        gatu8_mikomi_kingaku            '8月_見込金額
        gatu8_mikomi_arari              '8月_見込粗利
        gatu9_mikomi_kensuu             '9月_見込件数
        gatu9_mikomi_kingaku            '9月_見込金額
        gatu9_mikomi_arari              '9月_見込粗利
        gatu10_mikomi_kensuu            '10月_見込件数
        gatu10_mikomi_kingaku           '10月_見込金額
        gatu10_mikomi_arari             '10月_見込粗利
        gatu11_mikomi_kensuu            '11月_見込件数
        gatu11_mikomi_kingaku           '11月_見込金額
        gatu11_mikomi_arari             '11月_見込粗利
        gatu12_mikomi_kensuu            '12月_見込件数
        gatu12_mikomi_kingaku           '12月_見込金額
        gatu12_mikomi_arari             '12月_見込粗利
        gatu1_mikomi_kensuu             '1月_見込件数
        gatu1_mikomi_kingaku            '1月_見込金額
        gatu1_mikomi_arari              '1月_見込粗利
        gatu2_mikomi_kensuu             '2月_見込件数
        gatu2_mikomi_kingaku            '2月_見込金額
        gatu2_mikomi_arari              '2月_見込粗利
        gatu3_mikomi_kensuu             '3月_見込件数
        gatu3_mikomi_kingaku            '3月_見込金額
        gatu3_mikomi_arari              '3月_見込粗利
    End Enum

    'ＦＣ用計画CSVファイル.項目名
    Private Enum csvFcKeikakuItems
        keikaku_nendo = 2                           '計画_年度
        busyo_cd                                    '部署ｺｰﾄﾞ
        keikaku_kanri_syouhin_cd                    '計画管理_商品コード
        siten_mei                                   '支店名
        bunbetu_cd                                  '分別コード

        gatu4_keisanyou_uri_heikin_tanka    '4月_計算用__売上平均単価
        gatu4_keisanyou_siire_heikin_tanka  '4月_計算用__仕入平均単価

        gatu4_keisanyou_koj_hantei_ritu             '4月_計算用_工事判定率
        gatu4_keisanyou_koj_jyuchuu_ritu            '4月_計算用_工事受注率
        gatu4_keisanyou_tyoku_koj_ritu              '4月_計算用_直工事率
        gatu4_keikaku_kensuu                        '4月_計画件数
        gatu4_keikaku_kingaku                       '4月_計画金額
        gatu4_keikaku_arari                         '4月_計画粗利

        gatu5_keisanyou_uri_heikin_tanka    '5月_計算用__売上平均単価
        gatu5_keisanyou_siire_heikin_tanka  '5月_計算用__仕入平均単価

        gatu5_keisanyou_koj_hantei_ritu             '5月_計算用_工事判定率
        gatu5_keisanyou_koj_jyuchuu_ritu            '5月_計算用_工事受注率
        gatu5_keisanyou_tyoku_koj_ritu              '5月_計算用_直工事率
        gatu5_keikaku_kensuu                        '5月_計画件数
        gatu5_keikaku_kingaku                       '5月_計画金額
        gatu5_keikaku_arari                         '5月_計画粗利

        gatu6_keisanyou_uri_heikin_tanka    '6月_計算用__売上平均単価
        gatu6_keisanyou_siire_heikin_tanka  '6月_計算用__仕入平均単価

        gatu6_keisanyou_koj_hantei_ritu             '6月_計算用_工事判定率
        gatu6_keisanyou_koj_jyuchuu_ritu            '6月_計算用_工事受注率
        gatu6_keisanyou_tyoku_koj_ritu              '6月_計算用_直工事率
        gatu6_keikaku_kensuu                        '6月_計画件数
        gatu6_keikaku_kingaku                       '6月_計画金額
        gatu6_keikaku_arari                         '6月_計画粗利

        gatu7_keisanyou_uri_heikin_tanka    '7月_計算用__売上平均単価
        gatu7_keisanyou_siire_heikin_tanka  '7月_計算用__仕入平均単価

        gatu7_keisanyou_koj_hantei_ritu             '7月_計算用_工事判定率
        gatu7_keisanyou_koj_jyuchuu_ritu            '7月_計算用_工事受注率
        gatu7_keisanyou_tyoku_koj_ritu              '7月_計算用_直工事率
        gatu7_keikaku_kensuu                        '7月_計画件数
        gatu7_keikaku_kingaku                       '7月_計画金額
        gatu7_keikaku_arari                         '7月_計画粗利

        gatu8_keisanyou_uri_heikin_tanka    '8月_計算用__売上平均単価
        gatu8_keisanyou_siire_heikin_tanka  '8月_計算用__仕入平均単価

        gatu8_keisanyou_koj_hantei_ritu             '8月_計算用_工事判定率
        gatu8_keisanyou_koj_jyuchuu_ritu            '8月_計算用_工事受注率
        gatu8_keisanyou_tyoku_koj_ritu              '8月_計算用_直工事率
        gatu8_keikaku_kensuu                        '8月_計画件数
        gatu8_keikaku_kingaku                       '8月_計画金額
        gatu8_keikaku_arari                         '8月_計画粗利

        gatu9_keisanyou_uri_heikin_tanka    '9月_計算用__売上平均単価
        gatu9_keisanyou_siire_heikin_tanka  '9月_計算用__仕入平均単価

        gatu9_keisanyou_koj_hantei_ritu             '9月_計算用_工事判定率
        gatu9_keisanyou_koj_jyuchuu_ritu            '9月_計算用_工事受注率
        gatu9_keisanyou_tyoku_koj_ritu              '9月_計算用_直工事率
        gatu9_keikaku_kensuu                        '9月_計画件数
        gatu9_keikaku_kingaku                       '9月_計画金額
        gatu9_keikaku_arari                         '9月_計画粗利

        gatu10_keisanyou_uri_heikin_tanka    '10月_計算用__売上平均単価
        gatu10_keisanyou_siire_heikin_tanka  '10月_計算用__仕入平均単価

        gatu10_keisanyou_koj_hantei_ritu            '10月_計算用_工事判定率
        gatu10_keisanyou_koj_jyuchuu_ritu           '10月_計算用_工事受注率
        gatu10_keisanyou_tyoku_koj_ritu             '10月_計算用_直工事率
        gatu10_keikaku_kensuu                       '10月_計画件数
        gatu10_keikaku_kingaku                      '10月_計画金額
        gatu10_keikaku_arari                        '10月_計画粗利

        gatu11_keisanyou_uri_heikin_tanka    '11月_計算用__売上平均単価
        gatu11_keisanyou_siire_heikin_tanka  '11月_計算用__仕入平均単価

        gatu11_keisanyou_koj_hantei_ritu            '11月_計算用_工事判定率
        gatu11_keisanyou_koj_jyuchuu_ritu           '11月_計算用_工事受注率
        gatu11_keisanyou_tyoku_koj_ritu             '11月_計算用_直工事率
        gatu11_keikaku_kensuu                       '11月_計画件数
        gatu11_keikaku_kingaku                      '11月_計画金額
        gatu11_keikaku_arari                        '11月_計画粗利

        gatu12_keisanyou_uri_heikin_tanka    '12月_計算用__売上平均単価
        gatu12_keisanyou_siire_heikin_tanka  '12月_計算用__仕入平均単価

        gatu12_keisanyou_koj_hantei_ritu            '12月_計算用_工事判定率
        gatu12_keisanyou_koj_jyuchuu_ritu           '12月_計算用_工事受注率
        gatu12_keisanyou_tyoku_koj_ritu             '12月_計算用_直工事率
        gatu12_keikaku_kensuu                       '12月_計画件数
        gatu12_keikaku_kingaku                      '12月_計画金額
        gatu12_keikaku_arari                        '12月_計画粗利

        gatu1_keisanyou_uri_heikin_tanka    '1月_計算用__売上平均単価
        gatu1_keisanyou_siire_heikin_tanka  '1月_計算用__仕入平均単価

        gatu1_keisanyou_koj_hantei_ritu             '1月_計算用_工事判定率
        gatu1_keisanyou_koj_jyuchuu_ritu            '1月_計算用_工事受注率
        gatu1_keisanyou_tyoku_koj_ritu              '1月_計算用_直工事率
        gatu1_keikaku_kensuu                        '1月_計画件数
        gatu1_keikaku_kingaku                       '1月_計画金額
        gatu1_keikaku_arari                         '1月_計画粗利

        gatu2_keisanyou_uri_heikin_tanka    '2月_計算用__売上平均単価
        gatu2_keisanyou_siire_heikin_tanka  '2月_計算用__仕入平均単価

        gatu2_keisanyou_koj_hantei_ritu             '2月_計算用_工事判定率
        gatu2_keisanyou_koj_jyuchuu_ritu            '2月_計算用_工事受注率
        gatu2_keisanyou_tyoku_koj_ritu              '2月_計算用_直工事率
        gatu2_keikaku_kensuu                        '2月_計画件数
        gatu2_keikaku_kingaku                       '2月_計画金額
        gatu2_keikaku_arari                         '2月_計画粗利

        gatu3_keisanyou_uri_heikin_tanka    '3月_計算用__売上平均単価
        gatu3_keisanyou_siire_heikin_tanka  '3月_計算用__仕入平均単価

        gatu3_keisanyou_koj_hantei_ritu             '3月_計算用_工事判定率
        gatu3_keisanyou_koj_jyuchuu_ritu            '3月_計算用_工事受注率
        gatu3_keisanyou_tyoku_koj_ritu              '3月_計算用_直工事率
        gatu3_keikaku_kensuu                        '3月_計画件数
        gatu3_keikaku_kingaku                       '3月_計画金額
        gatu3_keikaku_arari                         '3月_計画粗利
    End Enum

    'ＦＣ用見込CSVファイル.項目名
    Private Enum csvFcMikomiItems
        keikaku_nendo = 2                  '計画_年度
        busyo_cd                           '部署ｺｰﾄﾞ
        keikaku_kanri_syouhin_cd           '計画管理_商品コード
        siten_mei                          '支店名
        bunbetu_cd                         '分別コード
        gatu4_mikomi_kensuu                '4月_見込件数
        gatu4_mikomi_kingaku               '4月_見込金額
        gatu4_mikomi_arari                 '4月_見込粗利
        gatu5_mikomi_kensuu                '5月_見込件数
        gatu5_mikomi_kingaku               '5月_見込金額
        gatu5_mikomi_arari                 '5月_見込粗利
        gatu6_mikomi_kensuu                '6月_見込件数
        gatu6_mikomi_kingaku               '6月_見込金額
        gatu6_mikomi_arari                 '6月_見込粗利
        gatu7_mikomi_kensuu                '7月_見込件数
        gatu7_mikomi_kingaku               '7月_見込金額
        gatu7_mikomi_arari                 '7月_見込粗利
        gatu8_mikomi_kensuu                '8月_見込件数
        gatu8_mikomi_kingaku               '8月_見込金額
        gatu8_mikomi_arari                 '8月_見込粗利
        gatu9_mikomi_kensuu                '9月_見込件数
        gatu9_mikomi_kingaku               '9月_見込金額
        gatu9_mikomi_arari                 '9月_見込粗利
        gatu10_mikomi_kensuu               '10月_見込件数
        gatu10_mikomi_kingaku              '10月_見込金額
        gatu10_mikomi_arari                '10月_見込粗利
        gatu11_mikomi_kensuu               '11月_見込件数
        gatu11_mikomi_kingaku              '11月_見込金額
        gatu11_mikomi_arari                '11月_見込粗利
        gatu12_mikomi_kensuu               '12月_見込件数
        gatu12_mikomi_kingaku              '12月_見込金額
        gatu12_mikomi_arari                '12月_見込粗利
        gatu1_mikomi_kensuu                '1月_見込件数
        gatu1_mikomi_kingaku               '1月_見込金額
        gatu1_mikomi_arari                 '1月_見込粗利
        gatu2_mikomi_kensuu                '2月_見込件数
        gatu2_mikomi_kingaku               '2月_見込金額
        gatu2_mikomi_arari                 '2月_見込粗利
        gatu3_mikomi_kensuu                '3月_見込件数
        gatu3_mikomi_kingaku               '3月_見込金額
        gatu3_mikomi_arari                 '3月_見込粗利
    End Enum

    ''' <summary>
    ''' 検索結果取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function GetInputKanri() As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return objKeikakuKanriInputDA.SelInputKanri()

    End Function

    ''' <summary>
    ''' 全検索結果件数取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function GetInputKanriCount() As String
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return objKeikakuKanriInputDA.SelInputKanriCount()

    End Function

    ''' <summary>
    ''' CSV取込ファイルのデータをチェックする
    ''' </summary>
    ''' <param name="fupCsv">CSV取込のコントロール</param>
    ''' <param name="strCsvKbn">CSV区分</param>
    ''' <param name="strUmuFlg">エラー有無</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function ChkCsvFile(ByVal fupCsv As Web.UI.WebControls.FileUpload, _
                               ByVal strCsvKbn As String, _
                               ByRef strUmuFlg As String) As String
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, fupCsv, strCsvKbn, strUmuFlg)

        Dim fileStream As IO.Stream                         '入出力ストリーム
        Dim fileReader As IO.StreamReader                   'ストリームリーダー
        Dim strReadLine As String                           '取込ファイル読込み行
        Dim intLineCount As Integer = 0                     'ライン数
        Dim strCsvLine() As String                          'CSVファイル内容
        Dim strFileMei As String                            'CSVファイル名
        Dim strEdiJouhouSakuseiDate As String               'EDI情報作成日
        Dim strUploadDate As String                         'アップロード日時
        Dim dtOkKeikakuKanri As New Data.DataTable          '計画管理テーブル
        Dim dtOkYoteiMikomiKanri As New Data.DataTable      '予定見込管理テーブル
        Dim dtOkFcKeikakuKanri As New Data.DataTable        'FC用計画管理テーブル
        Dim dtOkFcYoteiMikomiKanri As New Data.DataTable    'FC用予定見込管理テーブル
        Dim intCsvFieldCount As Integer                     'CSVファイルフィールド数
        Dim strCsvKbnName As String                         'CSV区分名称
        Dim strErrorNaiyou As String                        'エラー内容
        Dim intErrorFlg As Integer = 0                      'エラーFLG
        Dim strEigyouKbn As String                          '営業区分
        Dim strKeikakuNendo As String                       '計画年度
        Dim strKameitenCd As String                         '加盟店ｺｰﾄﾞ
        Dim strBusyoCd As String                            '部署ｺｰﾄﾞ
        Dim strSyouhinCd As String                          '計画管理_商品コード
        Dim dtError As New Data.DataTable                   'エラー
        Dim ninsyou As New NinsyouBC
        Dim strUserId As String                             'ユーザーID

        '2013/10/15 李宇追加 ↓
        Dim strUccrpdev As String = String.Empty '報連相_顧客区分
        Dim strUccrpseq As String = String.Empty '報連相_顧客コードSEQ
        '2013/10/15 李宇追加 ↑

        Try
            '「処理日時」取得
            strUploadDate = objCommonDA.SelSystemDate().Rows(0).Item(0).ToString

            'ユーザーID
            strUserId = ninsyou.GetUserID()
            strErrorNaiyou = String.Empty
            strCsvKbnName = String.Empty
            strEigyouKbn = String.Empty
            Select Case strCsvKbn
                Case csvKeikaku
                    '計画管理テーブルを作成
                    CreateOkKeikakuKanri(dtOkKeikakuKanri)
                    intCsvFieldCount = 104
                    strCsvKbnName = "計画ＣＳＶ"
                Case csvMikomi
                    '予定見込管理テーブルを作成
                    CreateOkYoteiMikomiKanri(dtOkYoteiMikomiKanri)
                    intCsvFieldCount = 44
                    strCsvKbnName = "見込ＣＳＶ"
                Case csvFcKeikaku
                    'FC用計画管理テーブルを作成
                    CreateOkFcKeikakuKanri(dtOkFcKeikakuKanri)
                    intCsvFieldCount = 103
                    strCsvKbnName = "ＦＣ用計画ＣＳＶ"
                Case csvFcMikomi
                    'FC用予定見込管理テーブルを作成
                    CreateOkFcYoteiMikomiKanri(dtOkFcYoteiMikomiKanri)
                    intCsvFieldCount = 43
                    strCsvKbnName = "ＦＣ用見込ＣＳＶ"
            End Select

            '計画管理表_取込エラー情報テーブルを作成
            CreateErrorDataTable(dtError)

            'CSVファイル名
            strFileMei = CutMaxLength(fupCsv.FileName, 128)

            '入出力ストリーム
            fileStream = fupCsv.FileContent

            'ストリームリーダー
            fileReader = New IO.StreamReader(fileStream, System.Text.Encoding.GetEncoding(932))
            Do
                '取込ファイルを読み込む
                ReDim Preserve strCsvLine(intLineCount)
                strCsvLine(intLineCount) = fileReader.ReadLine()
                intLineCount += 1
            Loop Until fileReader.EndOfStream

            'CSVアップロード上限件数チェック
            For i As Integer = strCsvLine.Length - 1 To 0 Step -1
                If Not strCsvLine(i).Trim.Equals(String.Empty) Then
                    If i >= CsvInputMaxLineCount Then
                        Return String.Format(CommonMessage.MSG061E, CStr(CsvInputMaxLineCount))
                    End If
                End If
            Next

            'EDI情報作成日
            strEdiJouhouSakuseiDate = strCsvLine(0).Split(","c)(1).Trim

            'CSVファイルをチェック
            For i As Integer = 0 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'CSV種類チェック
                    If strReadLine.Split(CChar(","))(0) <> strCsvKbn Then
                        'エラーメッセージを表示して、処理終了する
                        Return String.Format(CommonMessage.MSG056E, strCsvKbnName)
                    End If

                    'カンマを追加
                    If strReadLine.Split(","c).Length < intCsvFieldCount Then
                        strReadLine = strReadLine & StrDup(intCsvFieldCount - strReadLine.Split(","c).Length, ",")
                    End If

                    'フィールド数チェック
                    If strReadLine.Split(","c).Length > intCsvFieldCount Then
                        strErrorNaiyou = CommonMessage.MSG065E
                        'エラーデータを作成する
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '必須チェック
                    If Not ChkNotNull(strReadLine, strCsvKbn) Then
                        Select Case strCsvKbn
                            Case csvKeikaku
                                strErrorNaiyou = String.Format(CommonMessage.MSG046E, "計画_年度,加盟店ｺｰﾄﾞ,計画管理_商品コード")
                            Case csvMikomi
                                strErrorNaiyou = String.Format(CommonMessage.MSG046E, "計画_年度,加盟店ｺｰﾄﾞ,計画管理_商品コード")
                            Case csvFcKeikaku
                                strErrorNaiyou = String.Format(CommonMessage.MSG046E, "計画_年度,部署ｺｰﾄﾞ,計画管理_商品コード")
                            Case csvFcMikomi
                                strErrorNaiyou = String.Format(CommonMessage.MSG046E, "計画_年度,部署ｺｰﾄﾞ,計画管理_商品コード")
                        End Select
                        'エラーデータを作成する
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '項目最大長チェック
                    If Not ChkMaxLength(strReadLine, strCsvKbn) Then
                        strErrorNaiyou = CommonMessage.MSG045E
                        'エラーデータを作成する
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '禁則文字チェック
                    If Not ChkKinjiMoji(strReadLine) Then
                        strErrorNaiyou = CommonMessage.MSG044E
                        'エラーデータを作成する
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '数値型項目チェック
                    If Not ChkSuuti(strReadLine, strCsvKbn) Then
                        strErrorNaiyou = CommonMessage.MSG057E
                        'エラーデータを作成する
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '小数型項目チェック
                    If ChkSyouSuu(strReadLine, strCsvKbn) = 1 Then
                        strErrorNaiyou = CommonMessage.MSG057E
                        'エラーデータを作成する
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    ElseIf ChkSyouSuu(strReadLine, strCsvKbn) = 2 Then
                        strErrorNaiyou = String.Format(CommonMessage.MSG006E, "工事判定率,工事受注率,直工事率", "2", "1")
                        'エラーデータを作成する
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    If strCsvKbn = csvKeikaku OrElse strCsvKbn = csvMikomi Then
                        strEigyouKbn = strReadLine.Split(","c)(2)
                        strKeikakuNendo = strReadLine.Split(","c)(3)
                        strKameitenCd = strReadLine.Split(","c)(4)
                        strSyouhinCd = strReadLine.Split(","c)(5)
                        '営業区分が2（新規）の場合
                        If strEigyouKbn = "2" AndAlso Not String.IsNullOrEmpty(strKameitenCd) Then
                            '加盟店ｺｰﾄﾞ存在チェック
                            If Not objKeikakuKanriInputDA.SelKameitenCd(strKeikakuNendo, strKameitenCd) Then
                                strErrorNaiyou = String.Format(CommonMessage.MSG022E, "加盟店ｺｰﾄﾞ")
                                'エラーデータを作成する
                                Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                                intErrorFlg = 1
                            End If
                        End If
                    End If

                    '2013/10/15 李宇追加 ↓
                    '加盟店コードはNULLではない
                    If Not String.IsNullOrEmpty(strReadLine.Split(","c)(4).Trim) Then
                        '加盟店コードは8桁です
                        If strReadLine.Split(","c)(4).Trim.Length.Equals(8) Then

                            '報連送を取得する
                            Dim dt As Data.DataTable
                            dt = objKeikakuKanriInputDA.GetHorennSou(strReadLine.Split(","c)(csvKeikakuItems.kameiten_cd).Trim)
                            '8桁の加盟店コードAnd報連送データを取得できる
                            If (dt.Rows.Count = 1) _
                                AndAlso (Not String.IsNullOrEmpty(dt.Rows(0).Item("uccrpdev").ToString)) _
                                AndAlso (Not String.IsNullOrEmpty(dt.Rows(0).Item("uccrpseq").ToString)) Then

                                strUccrpdev = dt.Rows(0).Item("uccrpdev").ToString
                                strUccrpseq = dt.Rows(0).Item("uccrpseq").ToString

                                '8桁の加盟店コードAnd報連送データは複数を取得する
                            ElseIf dt.Rows.Count > 1 Then
                                strErrorNaiyou = CommonMessage.MSG084E
                                'エラーデータを作成する
                                Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                                intErrorFlg = 1

                                '加盟店コードは8桁ですので、報連送データの取得できない
                            Else
                                strErrorNaiyou = CommonMessage.MSG083E
                                'エラーデータを作成する
                                Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                                intErrorFlg = 1
                            End If

                        Else
                            '加盟店コードは5桁ですので、報連送データの取得は必要がない
                            If strReadLine.Split(","c)(4).Trim.Length.Equals(5) Then
                                '「報連相_顧客区分」と「報連相_顧客コードSEQ」はNULLをセットする
                                strUccrpdev = String.Empty
                                strUccrpseq = String.Empty
                            End If
                        End If
                    End If
                    '2013/10/15 李宇追加 ↑

                    '計画CSV取込時、取込不可チェック
                    If strCsvKbn = csvKeikaku Then
                        strKeikakuNendo = strReadLine.Split(","c)(3)
                        strKameitenCd = strReadLine.Split(","c)(4)
                        strSyouhinCd = strReadLine.Split(","c)(5)
                        If (Not String.IsNullOrEmpty(strKeikakuNendo) AndAlso Not String.IsNullOrEmpty(strKameitenCd) AndAlso Not String.IsNullOrEmpty(strSyouhinCd)) AndAlso _
                           objKeikakuKanriInputDA.SelKeikakuKakuteiCount(strKeikakuNendo, strKameitenCd, strSyouhinCd) Then
                            Return CommonMessage.MSG064E
                        End If
                    End If

                    'ＦＣ用計画CSV取込時、取込不可チェック
                    If strCsvKbn = csvFcKeikaku Then
                        strKeikakuNendo = strReadLine.Split(","c)(2)
                        strBusyoCd = strReadLine.Split(","c)(3)
                        strSyouhinCd = strReadLine.Split(","c)(4)
                        If (Not String.IsNullOrEmpty(strKeikakuNendo) AndAlso Not String.IsNullOrEmpty(strKeikakuNendo) AndAlso Not String.IsNullOrEmpty(strSyouhinCd)) AndAlso _
                         objKeikakuKanriInputDA.SelFCKeikakuKakuteiCount(strKeikakuNendo, strBusyoCd, strSyouhinCd) Then
                            Return CommonMessage.MSG064E
                        End If
                    End If

                    '合格データの処理
                    If intErrorFlg = 0 Then
                        Select Case strCsvKbn
                            Case csvKeikaku
                                '計画管理OKデータの処理
                                Call Me.SetOkDataRow(strReadLine, strCsvKbn, dtOkKeikakuKanri, strUccrpdev, strUccrpseq)
                            Case csvMikomi
                                '予定見込管理OKデータの処理
                                Call Me.SetOkDataRow(strReadLine, strCsvKbn, dtOkYoteiMikomiKanri, strUccrpdev, strUccrpseq)
                            Case csvFcKeikaku
                                'FC用計画管理OKデータの処理
                                Call Me.SetOkDataRow(strReadLine, strCsvKbn, dtOkFcKeikakuKanri, strUccrpdev, strUccrpseq)
                            Case csvFcMikomi
                                'FC用予定見込管理OKデータの処理
                                Call Me.SetOkDataRow(strReadLine, strCsvKbn, dtOkFcYoteiMikomiKanri, strUccrpdev, strUccrpseq)
                        End Select
                    Else
                        'エラー有無を設定
                        strUmuFlg = "1"
                    End If
                End If
            Next

            'CSVファイルを取込
            Select Case strCsvKbn
                Case csvKeikaku
                    '計画CSV取込
                    If Not CsvFileInput(dtOkKeikakuKanri, dtError, strUploadDate, strUserId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn) Then
                        Return CommonMessage.MSG070E
                    End If
                Case csvMikomi
                    '見込CSV取込
                    If Not CsvFileInput(dtOkYoteiMikomiKanri, dtError, strUploadDate, strUserId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn) Then
                        Return CommonMessage.MSG070E
                    End If
                Case csvFcKeikaku
                    'ＦＣ用計画CSV取込
                    If Not CsvFileInput(dtOkFcKeikakuKanri, dtError, strUploadDate, strUserId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn) Then
                        Return CommonMessage.MSG070E
                    End If
                Case csvFcMikomi
                    'ＦＣ用見込CSV取込
                    If Not CsvFileInput(dtOkFcYoteiMikomiKanri, dtError, strUploadDate, strUserId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn) Then
                        Return CommonMessage.MSG070E
                    End If
            End Select

        Catch ex As Exception
            Return CommonMessage.MSG069E
        End Try

        Return String.Empty
    End Function

    ''' <summary>
    ''' CSVファイルを取込する
    ''' </summary>
    ''' <param name="dtOk">OKデータ</param>
    ''' <param name="dtError">エラーデータ</param>
    ''' <param name="strUploadDate">処理日時</param>
    ''' <param name="userId">ログインユーザーID</param>
    ''' <param name="strFileMei">ファイル名</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/20 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function CsvFileInput(ByVal dtOk As Data.DataTable, _
                                 ByVal dtError As Data.DataTable, _
                                 ByVal strUploadDate As String, _
                                 ByVal userId As String, _
                                 ByVal strFileMei As String, _
                                 ByVal strEdiJouhouSakuseiDate As String, _
                                 ByVal intErrorFlg As Integer, _
                                 ByVal strCsvKbn As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                               dtOk, dtError, strUploadDate, userId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn)

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)

            Try
                If intErrorFlg = 0 Then
                    Select Case strCsvKbn
                        Case csvKeikaku
                            '計画管理テーブルに登録する
                            For i As Integer = 0 To dtOk.Rows.Count - 1
                                If Not objKeikakuKanriInputDA.InsKeikakuKanriData(dtOk.Rows(i), userId, strUploadDate) Then
                                    scope.Dispose()
                                    Throw New ApplicationException
                                End If
                            Next
                        Case csvMikomi
                            '予定見込管理テーブルに登録する
                            For i As Integer = 0 To dtOk.Rows.Count - 1
                                If Not objKeikakuKanriInputDA.InsYoteiMikomiKanriData(dtOk.Rows(i), userId, strUploadDate) Then
                                    scope.Dispose()
                                    Throw New ApplicationException
                                End If
                            Next
                        Case csvFcKeikaku
                            'FC用計画管理テーブルに登録する
                            For i As Integer = 0 To dtOk.Rows.Count - 1
                                If Not objKeikakuKanriInputDA.InsFCKeikakuKanriData(dtOk.Rows(i), userId, strUploadDate) Then
                                    scope.Dispose()
                                    Throw New ApplicationException
                                End If
                            Next
                        Case csvFcMikomi
                            'FC用予定見込管理テーブルに登録する
                            For i As Integer = 0 To dtOk.Rows.Count - 1
                                If Not objKeikakuKanriInputDA.InsFCYoteiMikomiKanriData(dtOk.Rows(i), userId, strUploadDate) Then
                                    scope.Dispose()
                                    Throw New ApplicationException
                                End If
                            Next
                    End Select
                Else
                    '計画管理表_取込エラー情報テーブルに登録する
                    For i As Integer = 0 To dtError.Rows.Count - 1
                        If Not objKeikakuKanriInputDA.InsKeikakuTorikomiError(strEdiJouhouSakuseiDate, strUploadDate, dtError.Rows(i), userId) Then
                            scope.Dispose()
                            Throw New ApplicationException
                        End If
                    Next
                End If

                'アップロード管理テーブルを登録
                If Not objKeikakuKanriInputDA.InsInputKanri(strUploadDate, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, userId) Then
                    scope.Dispose()
                    Throw New ApplicationException
                End If

                scope.Complete()

                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using

        Return True
    End Function


    Private Function GetValue(ByVal strValue As String) As Object

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strValue)

        If String.IsNullOrEmpty(strValue.Trim) Then
            Return System.DBNull.Value
        Else
            Return strValue.Trim
        End If

    End Function

    ''' <summary>
    ''' OKデータラインを追加する
    ''' </summary>
    ''' <param name="strLine">データライン</param>
    ''' <param name="strCsvKbn">CSV取込区分</param>
    ''' <param name="dtOkTable">OKデータテーブル</param>
    ''' <param name="strUccrpdev">報連相_顧客区分</param>
    ''' <param name="strUccrpseq">報連相_顧客コードSEQ</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/20 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Sub SetOkDataRow(ByVal strLine As String, ByVal strCsvKbn As String, ByRef dtOkTable As Data.DataTable, _
                            ByVal strUccrpdev As String, ByVal strUccrpseq As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strLine, strCsvKbn, dtOkTable, strUccrpdev, strUccrpseq)
        Dim dr As Data.DataRow

        dr = dtOkTable.NewRow
        Select Case strCsvKbn
            Case csvKeikaku
                '計画管理テーブルのデータ追加
                dr.Item("keikaku_nendo") = strLine.Split(","c)(csvKeikakuItems.keikaku_nendo).Trim
                dr.Item("kameiten_cd") = strLine.Split(","c)(csvKeikakuItems.kameiten_cd).Trim
                dr.Item("keikaku_kanri_syouhin_cd") = strLine.Split(","c)(csvKeikakuItems.keikaku_kanri_syouhin_cd).Trim
                dr.Item("kameiten_mei") = strLine.Split(","c)(csvKeikakuItems.kameiten_mei).Trim
                dr.Item("bunbetu_cd") = strLine.Split(","c)(csvKeikakuItems.bunbetu_cd).Trim

                '4月_計算用__売上平均単価
                dr.Item("4gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_uri_heikin_tanka))

                '4月_計算用__仕入平均単価
                dr.Item("4gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_siire_heikin_tanka))

                '4月_計算用_工事判定率
                dr.Item("4gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_koj_hantei_ritu))

                '4月_計算用_工事受注率
                dr.Item("4gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_koj_jyuchuu_ritu))

                '4月_計算用_直工事率
                dr.Item("4gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_tyoku_koj_ritu))

                '4月_計画件数
                dr.Item("4gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keikaku_kensuu))

                '4月_計画金額
                dr.Item("4gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keikaku_kingaku))

                '4月_計画粗利
                dr.Item("4gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keikaku_arari))

                '5月_計算用__売上平均単価
                dr.Item("5gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_uri_heikin_tanka))

                '5月_計算用__仕入平均単価
                dr.Item("5gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_siire_heikin_tanka))

                '5月_計算用_工事判定率
                dr.Item("5gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_koj_hantei_ritu))

                '5月_計算用_工事受注率
                dr.Item("5gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_koj_jyuchuu_ritu))

                '5月_計算用_直工事率
                dr.Item("5gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_tyoku_koj_ritu))

                '5月_計画件数
                dr.Item("5gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keikaku_kensuu))

                '5月_計画金額
                dr.Item("5gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keikaku_kingaku))

                '5月_計画粗利
                dr.Item("5gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keikaku_arari))

                '6月_計算用__売上平均単価
                dr.Item("6gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_uri_heikin_tanka))

                '6月_計算用__仕入平均単価
                dr.Item("6gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_siire_heikin_tanka))

                '6月_計算用_工事判定率
                dr.Item("6gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_koj_hantei_ritu))

                '6月_計算用_工事受注率
                dr.Item("6gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_koj_jyuchuu_ritu))

                '6月_計算用_直工事率
                dr.Item("6gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_tyoku_koj_ritu))

                '6月_計画件数
                dr.Item("6gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keikaku_kensuu))

                '6月_計画金額
                dr.Item("6gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keikaku_kingaku))

                '6月_計画粗利
                dr.Item("6gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keikaku_arari))

                '7月_計算用__売上平均単価
                dr.Item("7gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_uri_heikin_tanka))

                '7月_計算用__仕入平均単価
                dr.Item("7gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_siire_heikin_tanka))

                '7月_計算用_工事判定率
                dr.Item("7gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_koj_hantei_ritu))

                '7月_計算用_工事受注率
                dr.Item("7gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_koj_jyuchuu_ritu))

                '7月_計算用_直工事率
                dr.Item("7gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_tyoku_koj_ritu))

                '7月_計画件数
                dr.Item("7gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keikaku_kensuu))

                '7月_計画金額
                dr.Item("7gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keikaku_kingaku))

                '7月_計画粗利
                dr.Item("7gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keikaku_arari))

                '8月_計算用__売上平均単価
                dr.Item("8gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_uri_heikin_tanka))

                '8月_計算用__仕入平均単価
                dr.Item("8gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_siire_heikin_tanka))

                '8月_計算用_工事判定率
                dr.Item("8gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_koj_hantei_ritu))

                '8月_計算用_工事受注率
                dr.Item("8gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_koj_jyuchuu_ritu))

                '8月_計算用_直工事率
                dr.Item("8gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_tyoku_koj_ritu))

                '8月_計画件数
                dr.Item("8gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keikaku_kensuu))

                '8月_計画金額
                dr.Item("8gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keikaku_kingaku))

                '8月_計画粗利
                dr.Item("8gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keikaku_arari))

                '9月_計算用__売上平均単価
                dr.Item("9gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_uri_heikin_tanka))

                '9月_計算用__仕入平均単価
                dr.Item("9gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_siire_heikin_tanka))

                '9月_計算用_工事判定率
                dr.Item("9gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_koj_hantei_ritu))

                '9月_計算用_工事受注率
                dr.Item("9gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_koj_jyuchuu_ritu))

                '9月_計算用_直工事率
                dr.Item("9gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_tyoku_koj_ritu))

                '9月_計画件数
                dr.Item("9gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keikaku_kensuu))

                '9月_計画金額
                dr.Item("9gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keikaku_kingaku))

                '9月_計画粗利
                dr.Item("9gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keikaku_arari))

                '10月_計算用__売上平均単価
                dr.Item("10gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_uri_heikin_tanka))

                '10月_計算用__仕入平均単価
                dr.Item("10gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_siire_heikin_tanka))

                '10月_計算用_工事判定率
                dr.Item("10gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_koj_hantei_ritu))

                '10月_計算用_工事受注率
                dr.Item("10gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_koj_jyuchuu_ritu))

                '10月_計算用_直工事率
                dr.Item("10gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_tyoku_koj_ritu))

                '10月_計画件数
                dr.Item("10gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keikaku_kensuu))

                '10月_計画金額
                dr.Item("10gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keikaku_kingaku))

                '10月_計画粗利
                dr.Item("10gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keikaku_arari))

                '11月_計算用__売上平均単価
                dr.Item("11gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_uri_heikin_tanka))

                '11月_計算用__仕入平均単価
                dr.Item("11gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_siire_heikin_tanka))

                '11月_計算用_工事判定率
                dr.Item("11gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_koj_hantei_ritu))

                '11月_計算用_工事受注率
                dr.Item("11gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_koj_jyuchuu_ritu))

                '11月_計算用_直工事率
                dr.Item("11gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_tyoku_koj_ritu))

                '11月_計画件数
                dr.Item("11gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keikaku_kensuu))

                '11月_計画金額
                dr.Item("11gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keikaku_kingaku))

                '11月_計画粗利
                dr.Item("11gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keikaku_arari))

                '12月_計算用__売上平均単価
                dr.Item("12gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_uri_heikin_tanka))

                '12月_計算用__仕入平均単価
                dr.Item("12gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_siire_heikin_tanka))

                '12月_計算用_工事判定率
                dr.Item("12gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_koj_hantei_ritu))

                '12月_計算用_工事受注率
                dr.Item("12gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_koj_jyuchuu_ritu))

                '12月_計算用_直工事率
                dr.Item("12gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_tyoku_koj_ritu))

                '12月_計画件数
                dr.Item("12gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keikaku_kensuu))

                '12月_計画金額
                dr.Item("12gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keikaku_kingaku))

                '12月_計画粗利
                dr.Item("12gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keikaku_arari))

                '1月_計算用__売上平均単価
                dr.Item("1gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_uri_heikin_tanka))

                '1月_計算用__仕入平均単価
                dr.Item("1gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_siire_heikin_tanka))

                '1月_計算用_工事判定率
                dr.Item("1gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_koj_hantei_ritu))

                '1月_計算用_工事受注率
                dr.Item("1gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_koj_jyuchuu_ritu))

                '1月_計算用_直工事率
                dr.Item("1gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_tyoku_koj_ritu))

                '1月_計画件数
                dr.Item("1gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keikaku_kensuu))

                '1月_計画金額
                dr.Item("1gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keikaku_kingaku))

                '1月_計画粗利
                dr.Item("1gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keikaku_arari))

                '2月_計算用__売上平均単価
                dr.Item("2gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_uri_heikin_tanka))

                '2月_計算用__仕入平均単価
                dr.Item("2gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_siire_heikin_tanka))

                '2月_計算用_工事判定率
                dr.Item("2gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_koj_hantei_ritu))

                '2月_計算用_工事受注率
                dr.Item("2gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_koj_jyuchuu_ritu))

                '2月_計算用_直工事率
                dr.Item("2gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_tyoku_koj_ritu))

                '2月_計画件数
                dr.Item("2gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keikaku_kensuu))

                '2月_計画金額
                dr.Item("2gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keikaku_kingaku))

                '2月_計画粗利
                dr.Item("2gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keikaku_arari))

                '3月_計算用__売上平均単価
                dr.Item("3gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_uri_heikin_tanka))

                '3月_計算用__仕入平均単価
                dr.Item("3gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_siire_heikin_tanka))

                '3月_計算用_工事判定率
                dr.Item("3gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_koj_hantei_ritu))

                '3月_計算用_工事受注率
                dr.Item("3gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_koj_jyuchuu_ritu))

                '3月_計算用_直工事率
                dr.Item("3gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_tyoku_koj_ritu))

                '3月_計画件数
                dr.Item("3gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keikaku_kensuu))

                '3月_計画金額
                dr.Item("3gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keikaku_kingaku))

                '3月_計画粗利
                dr.Item("3gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keikaku_arari))

                '2013/10/15 李宇追加 ↓
                '報連相_顧客区分
                dr.Item("UCCRPDEV") = Me.GetValue(strUccrpdev)

                '報連相_顧客コードSEQ
                dr.Item("UCCRPSEQ") = Me.GetValue(strUccrpseq)
                '2013/10/15 李宇追加 ↑

            Case csvMikomi
                '予定見込管理テーブルのデータ追加
                dr.Item("keikaku_nendo") = strLine.Split(","c)(csvMikomiItems.keikaku_nendo).Trim
                dr.Item("kameiten_cd") = strLine.Split(","c)(csvMikomiItems.kameiten_cd).Trim
                dr.Item("keikaku_kanri_syouhin_cd") = strLine.Split(","c)(csvMikomiItems.keikaku_kanri_syouhin_cd).Trim
                dr.Item("kameiten_mei") = strLine.Split(","c)(csvMikomiItems.kameiten_mei).Trim
                dr.Item("bunbetu_cd") = strLine.Split(","c)(csvMikomiItems.bunbetu_cd).Trim
                '4月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_kensuu).Trim) Then
                    dr.Item("4gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("4gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_kensuu).Trim
                End If
                '4月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_kingaku).Trim) Then
                    dr.Item("4gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("4gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_kingaku).Trim
                End If
                '4月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_arari).Trim) Then
                    dr.Item("4gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("4gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_arari).Trim
                End If
                '5月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_kensuu).Trim) Then
                    dr.Item("5gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("5gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_kensuu).Trim
                End If
                '5月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_kingaku).Trim) Then
                    dr.Item("5gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("5gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_kingaku).Trim
                End If
                '5月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_arari).Trim) Then
                    dr.Item("5gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("5gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_arari).Trim
                End If
                '6月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_kensuu).Trim) Then
                    dr.Item("6gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("6gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_kensuu).Trim
                End If
                '6月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_kingaku).Trim) Then
                    dr.Item("6gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("6gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_kingaku).Trim
                End If
                '6月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_arari).Trim) Then
                    dr.Item("6gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("6gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_arari).Trim
                End If
                '7月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_kensuu).Trim) Then
                    dr.Item("7gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("7gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_kensuu).Trim
                End If
                '7月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_kingaku).Trim) Then
                    dr.Item("7gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("7gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_kingaku).Trim
                End If
                '7月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_arari).Trim) Then
                    dr.Item("7gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("7gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_arari).Trim
                End If
                '8月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_kensuu).Trim) Then
                    dr.Item("8gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("8gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_kensuu).Trim
                End If
                '8月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_kingaku).Trim) Then
                    dr.Item("8gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("8gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_kingaku).Trim
                End If
                '8月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_arari).Trim) Then
                    dr.Item("8gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("8gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_arari).Trim
                End If
                '9月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_kensuu).Trim) Then
                    dr.Item("9gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("9gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_kensuu).Trim
                End If
                '9月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_kingaku).Trim) Then
                    dr.Item("9gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("9gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_kingaku).Trim
                End If
                '9月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_arari).Trim) Then
                    dr.Item("9gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("9gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_arari).Trim
                End If
                '10月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_kensuu).Trim) Then
                    dr.Item("10gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("10gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_kensuu).Trim
                End If
                '10月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_kingaku).Trim) Then
                    dr.Item("10gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("10gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_kingaku).Trim
                End If
                '10月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_arari).Trim) Then
                    dr.Item("10gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("10gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_arari).Trim
                End If
                '11月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_kensuu).Trim) Then
                    dr.Item("11gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("11gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_kensuu).Trim
                End If
                '11月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_kingaku).Trim) Then
                    dr.Item("11gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("11gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_kingaku).Trim
                End If
                '11月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_arari).Trim) Then
                    dr.Item("11gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("11gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_arari).Trim
                End If
                '12月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_kensuu).Trim) Then
                    dr.Item("12gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("12gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_kensuu).Trim
                End If
                '12月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_kingaku).Trim) Then
                    dr.Item("12gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("12gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_kingaku).Trim
                End If
                '12月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_arari).Trim) Then
                    dr.Item("12gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("12gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_arari).Trim
                End If
                '1月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_kensuu).Trim) Then
                    dr.Item("1gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("1gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_kensuu).Trim
                End If
                '1月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_kingaku).Trim) Then
                    dr.Item("1gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("1gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_kingaku).Trim
                End If
                '1月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_arari).Trim) Then
                    dr.Item("1gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("1gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_arari).Trim
                End If
                '2月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_kensuu).Trim) Then
                    dr.Item("2gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("2gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_kensuu).Trim
                End If
                '2月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_kingaku).Trim) Then
                    dr.Item("2gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("2gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_kingaku).Trim
                End If
                '2月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_arari).Trim) Then
                    dr.Item("2gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("2gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_arari).Trim
                End If
                '3月_見込件数
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_kensuu).Trim) Then
                    dr.Item("3gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("3gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_kensuu).Trim
                End If
                '3月_見込金額
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_kingaku).Trim) Then
                    dr.Item("3gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("3gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_kingaku).Trim
                End If
                '3月_見込粗利
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_arari).Trim) Then
                    dr.Item("3gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("3gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_arari).Trim
                End If
            Case csvFcKeikaku
                'FC用計画管理テーブルのデータ追加
                dr.Item("keikaku_nendo") = strLine.Split(","c)(csvFcKeikakuItems.keikaku_nendo).Trim
                dr.Item("busyo_cd") = strLine.Split(","c)(csvFcKeikakuItems.busyo_cd).Trim
                dr.Item("keikaku_kanri_syouhin_cd") = strLine.Split(","c)(csvFcKeikakuItems.keikaku_kanri_syouhin_cd).Trim
                dr.Item("siten_mei") = strLine.Split(","c)(csvFcKeikakuItems.siten_mei).Trim
                dr.Item("bunbetu_cd") = strLine.Split(","c)(csvFcKeikakuItems.bunbetu_cd).Trim

                '4月_計算用__売上平均単価
                dr.Item("4gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_uri_heikin_tanka))

                '4月_計算用__仕入平均単価
                dr.Item("4gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_siire_heikin_tanka))

                '4月_計算用_工事判定率
                dr.Item("4gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_hantei_ritu))

                '4月_計算用_工事受注率
                dr.Item("4gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_jyuchuu_ritu))

                '4月_計算用_直工事率
                dr.Item("4gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_tyoku_koj_ritu))

                '4月_計画件数
                dr.Item("4gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kensuu))

                '4月_計画金額
                dr.Item("4gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kingaku))

                '4月_計画粗利
                dr.Item("4gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_arari))

                '5月_計算用__売上平均単価
                dr.Item("5gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_uri_heikin_tanka))

                '5月_計算用__仕入平均単価
                dr.Item("5gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_siire_heikin_tanka))

                '5月_計算用_工事判定率
                dr.Item("5gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_hantei_ritu))

                '5月_計算用_工事受注率
                dr.Item("5gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_jyuchuu_ritu))

                '5月_計算用_直工事率
                dr.Item("5gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_tyoku_koj_ritu))

                '5月_計画件数
                dr.Item("5gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kensuu))

                '5月_計画金額
                dr.Item("5gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kingaku))

                '5月_計画粗利
                dr.Item("5gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_arari))

                '6月_計算用__売上平均単価
                dr.Item("6gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_uri_heikin_tanka))

                '6月_計算用__仕入平均単価
                dr.Item("6gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_siire_heikin_tanka))

                '6月_計算用_工事判定率
                dr.Item("6gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_hantei_ritu))

                '6月_計算用_工事受注率
                dr.Item("6gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_jyuchuu_ritu))

                '6月_計算用_直工事率
                dr.Item("6gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_tyoku_koj_ritu))

                '6月_計画件数
                dr.Item("6gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kensuu))

                '6月_計画金額
                dr.Item("6gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kingaku))

                '6月_計画粗利
                dr.Item("6gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_arari))

                '7月_計算用__売上平均単価
                dr.Item("7gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_uri_heikin_tanka))

                '7月_計算用__仕入平均単価
                dr.Item("7gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_siire_heikin_tanka))

                '7月_計算用_工事判定率
                dr.Item("7gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_hantei_ritu))

                '7月_計算用_工事受注率
                dr.Item("7gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_jyuchuu_ritu))

                '7月_計算用_直工事率
                dr.Item("7gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_tyoku_koj_ritu))

                '7月_計画件数
                dr.Item("7gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kensuu))

                '7月_計画金額
                dr.Item("7gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kingaku))

                '7月_計画粗利
                dr.Item("7gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_arari))

                '8月_計算用__売上平均単価
                dr.Item("8gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_uri_heikin_tanka))

                '8月_計算用__仕入平均単価
                dr.Item("8gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_siire_heikin_tanka))

                '8月_計算用_工事判定率
                dr.Item("8gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_hantei_ritu))

                '8月_計算用_工事受注率
                dr.Item("8gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_jyuchuu_ritu))

                '8月_計算用_直工事率
                dr.Item("8gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_tyoku_koj_ritu))

                '8月_計画件数
                dr.Item("8gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kensuu))

                '8月_計画金額
                dr.Item("8gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kingaku))

                '8月_計画粗利
                dr.Item("8gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_arari))

                '9月_計算用__売上平均単価
                dr.Item("9gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_uri_heikin_tanka))

                '9月_計算用__仕入平均単価
                dr.Item("9gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_siire_heikin_tanka))

                '9月_計算用_工事判定率
                dr.Item("9gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_hantei_ritu))

                '9月_計算用_工事受注率
                dr.Item("9gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_jyuchuu_ritu))

                '9月_計算用_直工事率
                dr.Item("9gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_tyoku_koj_ritu))

                '9月_計画件数
                dr.Item("9gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kensuu))

                '9月_計画金額
                dr.Item("9gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kingaku))

                '9月_計画粗利
                dr.Item("9gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_arari))

                '10月_計算用__売上平均単価
                dr.Item("10gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_uri_heikin_tanka))

                '10月_計算用__仕入平均単価
                dr.Item("10gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_siire_heikin_tanka))

                '10月_計算用_工事判定率
                dr.Item("10gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_hantei_ritu))

                '10月_計算用_工事受注率
                dr.Item("10gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_jyuchuu_ritu))

                '10月_計算用_直工事率
                dr.Item("10gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_tyoku_koj_ritu))

                '10月_計画件数
                dr.Item("10gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kensuu))

                '10月_計画金額
                dr.Item("10gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kingaku))

                '10月_計画粗利
                dr.Item("10gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_arari))

                '11月_計算用__売上平均単価
                dr.Item("11gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_uri_heikin_tanka))

                '11月_計算用__仕入平均単価
                dr.Item("11gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_siire_heikin_tanka))

                '11月_計算用_工事判定率
                dr.Item("11gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_hantei_ritu))

                '11月_計算用_工事受注率
                dr.Item("11gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_jyuchuu_ritu))

                '11月_計算用_直工事率
                dr.Item("11gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_tyoku_koj_ritu))

                '11月_計画件数
                dr.Item("11gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kensuu))

                '11月_計画金額
                dr.Item("11gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kingaku))

                '11月_計画粗利
                dr.Item("11gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_arari))

                '12月_計算用__売上平均単価
                dr.Item("12gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_uri_heikin_tanka))

                '12月_計算用__仕入平均単価
                dr.Item("12gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_siire_heikin_tanka))

                '12月_計算用_工事判定率
                dr.Item("12gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_hantei_ritu))

                '12月_計算用_工事受注率
                dr.Item("12gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_jyuchuu_ritu))

                '12月_計算用_直工事率
                dr.Item("12gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_tyoku_koj_ritu))

                '12月_計画件数
                dr.Item("12gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kensuu))

                '12月_計画金額
                dr.Item("12gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kingaku))

                '12月_計画粗利
                dr.Item("12gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_arari))

                '1月_計算用__売上平均単価
                dr.Item("1gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_uri_heikin_tanka))

                '1月_計算用__仕入平均単価
                dr.Item("1gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_siire_heikin_tanka))

                '1月_計算用_工事判定率
                dr.Item("1gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_hantei_ritu))

                '1月_計算用_工事受注率
                dr.Item("1gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_jyuchuu_ritu))

                '1月_計算用_直工事率
                dr.Item("1gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_tyoku_koj_ritu))

                '1月_計画件数
                dr.Item("1gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kensuu))

                '1月_計画金額
                dr.Item("1gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kingaku))

                '1月_計画粗利
                dr.Item("1gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_arari))

                '2月_計算用__売上平均単価
                dr.Item("2gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_uri_heikin_tanka))

                '2月_計算用__仕入平均単価
                dr.Item("2gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_siire_heikin_tanka))

                '2月_計算用_工事判定率
                dr.Item("2gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_hantei_ritu))

                '2月_計算用_工事受注率
                dr.Item("2gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_jyuchuu_ritu))

                '2月_計算用_直工事率
                dr.Item("2gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_tyoku_koj_ritu))

                '2月_計画件数
                dr.Item("2gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kensuu))

                '2月_計画金額
                dr.Item("2gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kingaku))

                '2月_計画粗利
                dr.Item("2gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_arari))

                '3月_計算用__売上平均単価
                dr.Item("3gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_uri_heikin_tanka))

                '3月_計算用__仕入平均単価
                dr.Item("3gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_siire_heikin_tanka))

                '3月_計算用_工事判定率
                dr.Item("3gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_hantei_ritu))

                '3月_計算用_工事受注率
                dr.Item("3gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_jyuchuu_ritu))

                '3月_計算用_直工事率
                dr.Item("3gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_tyoku_koj_ritu))

                '3月_計画件数
                dr.Item("3gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kensuu))

                '3月_計画金額
                dr.Item("3gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kingaku))

                '3月_計画粗利
                dr.Item("3gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_arari))



                '    '4月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("4gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '4月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("4gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '4月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("4gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '4月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kensuu).Trim) Then
                '        dr.Item("4gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kensuu).Trim
                '    End If
                '    '4月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kingaku).Trim) Then
                '        dr.Item("4gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kingaku).Trim
                '    End If
                '    '4月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_arari).Trim) Then
                '        dr.Item("4gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_arari).Trim
                '    End If
                '    '5月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("5gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '5月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("5gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '5月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("5gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '5月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kensuu).Trim) Then
                '        dr.Item("5gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kensuu).Trim
                '    End If
                '    '5月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kingaku).Trim) Then
                '        dr.Item("5gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kingaku).Trim
                '    End If
                '    '5月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_arari).Trim) Then
                '        dr.Item("5gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_arari).Trim
                '    End If
                '    '6月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("6gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '6月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("6gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '6月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("6gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '6月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kensuu).Trim) Then
                '        dr.Item("6gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kensuu).Trim
                '    End If
                '    '6月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kingaku).Trim) Then
                '        dr.Item("6gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kingaku).Trim
                '    End If
                '    '6月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_arari).Trim) Then
                '        dr.Item("6gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_arari).Trim
                '    End If
                '    '7月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("7gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '7月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("7gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '7月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("7gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '7月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kensuu).Trim) Then
                '        dr.Item("7gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kensuu).Trim
                '    End If
                '    '7月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kingaku).Trim) Then
                '        dr.Item("7gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kingaku).Trim
                '    End If
                '    '7月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_arari).Trim) Then
                '        dr.Item("7gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_arari).Trim
                '    End If
                '    '8月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("8gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '8月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("8gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '8月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("8gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '8月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kensuu).Trim) Then
                '        dr.Item("8gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kensuu).Trim
                '    End If
                '    '8月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kingaku).Trim) Then
                '        dr.Item("8gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kingaku).Trim
                '    End If
                '    '8月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_arari).Trim) Then
                '        dr.Item("8gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_arari).Trim
                '    End If
                '    '9月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("9gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '9月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("9gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '9月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("9gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '9月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kensuu).Trim) Then
                '        dr.Item("9gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kensuu).Trim
                '    End If
                '    '9月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kingaku).Trim) Then
                '        dr.Item("9gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kingaku).Trim
                '    End If
                '    '9月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_arari).Trim) Then
                '        dr.Item("9gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_arari).Trim
                '    End If
                '    '10月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("10gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '10月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("10gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '10月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("10gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '10月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kensuu).Trim) Then
                '        dr.Item("10gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kensuu).Trim
                '    End If
                '    '10月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kingaku).Trim) Then
                '        dr.Item("10gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kingaku).Trim
                '    End If
                '    '10月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_arari).Trim) Then
                '        dr.Item("10gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_arari).Trim
                '    End If
                '    '11月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("11gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '11月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("11gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '11月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("11gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '11月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kensuu).Trim) Then
                '        dr.Item("11gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kensuu).Trim
                '    End If
                '    '11月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kingaku).Trim) Then
                '        dr.Item("11gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kingaku).Trim
                '    End If
                '    '11月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_arari).Trim) Then
                '        dr.Item("11gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_arari).Trim
                '    End If
                '    '12月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("12gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '12月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("12gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '12月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("12gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '12月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kensuu).Trim) Then
                '        dr.Item("12gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kensuu).Trim
                '    End If
                '    '12月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kingaku).Trim) Then
                '        dr.Item("12gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kingaku).Trim
                '    End If
                '    '12月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_arari).Trim) Then
                '        dr.Item("12gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_arari).Trim
                '    End If
                '    '1月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("1gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '1月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("1gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '1月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("1gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '1月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kensuu).Trim) Then
                '        dr.Item("1gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kensuu).Trim
                '    End If
                '    '1月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kingaku).Trim) Then
                '        dr.Item("1gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kingaku).Trim
                '    End If
                '    '1月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_arari).Trim) Then
                '        dr.Item("1gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_arari).Trim
                '    End If
                '    '2月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("2gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '2月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("2gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '2月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("2gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '2月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kensuu).Trim) Then
                '        dr.Item("2gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kensuu).Trim
                '    End If
                '    '2月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kingaku).Trim) Then
                '        dr.Item("2gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kingaku).Trim
                '    End If
                '    '2月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_arari).Trim) Then
                '        dr.Item("2gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_arari).Trim
                '    End If
                '    '3月_計算用_工事判定率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("3gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '3月_計算用_工事受注率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("3gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '3月_計算用_直工事率
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("3gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '3月_計画件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kensuu).Trim) Then
                '        dr.Item("3gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kensuu).Trim
                '    End If
                '    '3月_計画金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kingaku).Trim) Then
                '        dr.Item("3gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kingaku).Trim
                '    End If
                '    '3月_計画粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_arari).Trim) Then
                '        dr.Item("3gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_arari).Trim
                '    End If
                'Case csvFcMikomi
                '    'FC用予定見込管理テーブルのデータ追加
                '    dr.Item("keikaku_nendo") = strLine.Split(","c)(csvFcMikomiItems.keikaku_nendo).Trim
                '    dr.Item("busyo_cd") = strLine.Split(","c)(csvFcMikomiItems.busyo_cd).Trim
                '    dr.Item("keikaku_kanri_syouhin_cd") = strLine.Split(","c)(csvFcMikomiItems.keikaku_kanri_syouhin_cd).Trim
                '    dr.Item("siten_mei") = strLine.Split(","c)(csvFcMikomiItems.siten_mei).Trim
                '    dr.Item("bunbetu_cd") = strLine.Split(","c)(csvFcMikomiItems.bunbetu_cd).Trim
                '    '4月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_kensuu).Trim) Then
                '        dr.Item("4gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_kensuu).Trim
                '    End If
                '    '4月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_kingaku).Trim) Then
                '        dr.Item("4gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_kingaku).Trim
                '    End If
                '    '4月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_arari).Trim) Then
                '        dr.Item("4gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_arari).Trim
                '    End If
                '    '5月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_kensuu).Trim) Then
                '        dr.Item("5gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_kensuu).Trim
                '    End If
                '    '5月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_kingaku).Trim) Then
                '        dr.Item("5gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_kingaku).Trim
                '    End If
                '    '5月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_arari).Trim) Then
                '        dr.Item("5gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_arari).Trim
                '    End If
                '    '6月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_kensuu).Trim) Then
                '        dr.Item("6gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_kensuu).Trim
                '    End If
                '    '6月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_kingaku).Trim) Then
                '        dr.Item("6gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_kingaku).Trim
                '    End If
                '    '6月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_arari).Trim) Then
                '        dr.Item("6gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_arari).Trim
                '    End If
                '    '7月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_kensuu).Trim) Then
                '        dr.Item("7gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_kensuu).Trim
                '    End If
                '    '7月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_kingaku).Trim) Then
                '        dr.Item("7gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_kingaku).Trim
                '    End If
                '    '7月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_arari).Trim) Then
                '        dr.Item("7gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_arari).Trim
                '    End If
                '    '8月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_kensuu).Trim) Then
                '        dr.Item("8gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_kensuu).Trim
                '    End If
                '    '8月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_kingaku).Trim) Then
                '        dr.Item("8gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_kingaku).Trim
                '    End If
                '    '8月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_arari).Trim) Then
                '        dr.Item("8gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_arari).Trim
                '    End If
                '    '9月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_kensuu).Trim) Then
                '        dr.Item("9gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_kensuu).Trim
                '    End If
                '    '9月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_kingaku).Trim) Then
                '        dr.Item("9gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_kingaku).Trim
                '    End If
                '    '9月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_arari).Trim) Then
                '        dr.Item("9gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_arari).Trim
                '    End If
                '    '10月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_kensuu).Trim) Then
                '        dr.Item("10gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_kensuu).Trim
                '    End If
                '    '10月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_kingaku).Trim) Then
                '        dr.Item("10gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_kingaku).Trim
                '    End If
                '    '10月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_arari).Trim) Then
                '        dr.Item("10gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_arari).Trim
                '    End If
                '    '11月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_kensuu).Trim) Then
                '        dr.Item("11gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_kensuu).Trim
                '    End If
                '    '11月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_kingaku).Trim) Then
                '        dr.Item("11gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_kingaku).Trim
                '    End If
                '    '11月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_arari).Trim) Then
                '        dr.Item("11gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_arari).Trim
                '    End If
                '    '12月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_kensuu).Trim) Then
                '        dr.Item("12gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_kensuu).Trim
                '    End If
                '    '12月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_kingaku).Trim) Then
                '        dr.Item("12gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_kingaku).Trim
                '    End If
                '    '12月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_arari).Trim) Then
                '        dr.Item("12gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_arari).Trim
                '    End If
                '    '1月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_kensuu).Trim) Then
                '        dr.Item("1gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_kensuu).Trim
                '    End If
                '    '1月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_kingaku).Trim) Then
                '        dr.Item("1gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_kingaku).Trim
                '    End If
                '    '1月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_arari).Trim) Then
                '        dr.Item("1gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_arari).Trim
                '    End If
                '    '2月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_kensuu).Trim) Then
                '        dr.Item("2gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_kensuu).Trim
                '    End If
                '    '2月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_kingaku).Trim) Then
                '        dr.Item("2gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_kingaku).Trim
                '    End If
                '    '2月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_arari).Trim) Then
                '        dr.Item("2gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_arari).Trim
                '    End If
                '    '3月_見込件数
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_kensuu).Trim) Then
                '        dr.Item("3gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_kensuu).Trim
                '    End If
                '    '3月_見込金額
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_kingaku).Trim) Then
                '        dr.Item("3gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_kingaku).Trim
                '    End If
                '    '3月_見込粗利
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_arari).Trim) Then
                '        dr.Item("3gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_arari).Trim
                '    End If
        End Select

        dtOkTable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 計画管理OKデータのスキーマを作成
    ''' </summary>
    ''' <param name="dtOk">計画管理OKデータ</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Sub CreateOkKeikakuKanri(ByRef dtOk As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtOk)

        dtOk.Columns.Add("keikaku_nendo")                      '計画_年度
        dtOk.Columns.Add("kameiten_cd")                        '加盟店ｺｰﾄﾞ
        dtOk.Columns.Add("keikaku_kanri_syouhin_cd")           '計画管理_商品コード
        dtOk.Columns.Add("kameiten_mei")                       '加盟店名
        dtOk.Columns.Add("bunbetu_cd")                         '分別コード

        dtOk.Columns.Add("4gatu_keisanyou_uri_heikin_tanka")   '4月_計算用__売上平均単価
        dtOk.Columns.Add("4gatu_keisanyou_siire_heikin_tanka") '4月_計算用__仕入平均単価

        dtOk.Columns.Add("4gatu_keisanyou_koj_hantei_ritu")    '4月_計算用_工事判定率
        dtOk.Columns.Add("4gatu_keisanyou_koj_jyuchuu_ritu")   '4月_計算用_工事受注率
        dtOk.Columns.Add("4gatu_keisanyou_tyoku_koj_ritu")     '4月_計算用_直工事率
        dtOk.Columns.Add("4gatu_keikaku_kensuu")               '4月_計画件数
        dtOk.Columns.Add("4gatu_keikaku_kingaku")              '4月_計画金額
        dtOk.Columns.Add("4gatu_keikaku_arari")                '4月_計画粗利

        dtOk.Columns.Add("5gatu_keisanyou_uri_heikin_tanka")   '5月_計算用__売上平均単価
        dtOk.Columns.Add("5gatu_keisanyou_siire_heikin_tanka") '5月_計算用__仕入平均単価

        dtOk.Columns.Add("5gatu_keisanyou_koj_hantei_ritu")    '5月_計算用_工事判定率
        dtOk.Columns.Add("5gatu_keisanyou_koj_jyuchuu_ritu")   '5月_計算用_工事受注率
        dtOk.Columns.Add("5gatu_keisanyou_tyoku_koj_ritu")     '5月_計算用_直工事率
        dtOk.Columns.Add("5gatu_keikaku_kensuu")               '5月_計画件数
        dtOk.Columns.Add("5gatu_keikaku_kingaku")              '5月_計画金額
        dtOk.Columns.Add("5gatu_keikaku_arari")                '5月_計画粗利価

        dtOk.Columns.Add("6gatu_keisanyou_uri_heikin_tanka")   '6月_計算用__売上平均単価
        dtOk.Columns.Add("6gatu_keisanyou_siire_heikin_tanka") '6月_計算用__仕入平均単価

        dtOk.Columns.Add("6gatu_keisanyou_koj_hantei_ritu")    '6月_計算用_工事判定率
        dtOk.Columns.Add("6gatu_keisanyou_koj_jyuchuu_ritu")   '6月_計算用_工事受注率
        dtOk.Columns.Add("6gatu_keisanyou_tyoku_koj_ritu")     '6月_計算用_直工事率
        dtOk.Columns.Add("6gatu_keikaku_kensuu")               '6月_計画件数
        dtOk.Columns.Add("6gatu_keikaku_kingaku")              '6月_計画金額
        dtOk.Columns.Add("6gatu_keikaku_arari")                '6月_計画粗利

        dtOk.Columns.Add("7gatu_keisanyou_uri_heikin_tanka")   '7月_計算用__売上平均単価
        dtOk.Columns.Add("7gatu_keisanyou_siire_heikin_tanka") '7月_計算用__仕入平均単価

        dtOk.Columns.Add("7gatu_keisanyou_koj_hantei_ritu")    '7月_計算用_工事判定率
        dtOk.Columns.Add("7gatu_keisanyou_koj_jyuchuu_ritu")   '7月_計算用_工事受注率
        dtOk.Columns.Add("7gatu_keisanyou_tyoku_koj_ritu")     '7月_計算用_直工事率
        dtOk.Columns.Add("7gatu_keikaku_kensuu")               '7月_計画件数
        dtOk.Columns.Add("7gatu_keikaku_kingaku")              '7月_計画金額
        dtOk.Columns.Add("7gatu_keikaku_arari")                '7月_計画粗利

        dtOk.Columns.Add("8gatu_keisanyou_uri_heikin_tanka")   '8月_計算用__売上平均単価
        dtOk.Columns.Add("8gatu_keisanyou_siire_heikin_tanka") '8月_計算用__仕入平均単価

        dtOk.Columns.Add("8gatu_keisanyou_koj_hantei_ritu")    '8月_計算用_工事判定率
        dtOk.Columns.Add("8gatu_keisanyou_koj_jyuchuu_ritu")   '8月_計算用_工事受注率
        dtOk.Columns.Add("8gatu_keisanyou_tyoku_koj_ritu")     '8月_計算用_直工事率
        dtOk.Columns.Add("8gatu_keikaku_kensuu")               '8月_計画件数
        dtOk.Columns.Add("8gatu_keikaku_kingaku")              '8月_計画金額
        dtOk.Columns.Add("8gatu_keikaku_arari")                '8月_計画粗利

        dtOk.Columns.Add("9gatu_keisanyou_uri_heikin_tanka")   '9月_計算用__売上平均単価
        dtOk.Columns.Add("9gatu_keisanyou_siire_heikin_tanka") '9月_計算用__仕入平均単価

        dtOk.Columns.Add("9gatu_keisanyou_koj_hantei_ritu")    '9月_計算用_工事判定率
        dtOk.Columns.Add("9gatu_keisanyou_koj_jyuchuu_ritu")   '9月_計算用_工事受注率
        dtOk.Columns.Add("9gatu_keisanyou_tyoku_koj_ritu")     '9月_計算用_直工事率
        dtOk.Columns.Add("9gatu_keikaku_kensuu")               '9月_計画件数
        dtOk.Columns.Add("9gatu_keikaku_kingaku")              '9月_計画金額
        dtOk.Columns.Add("9gatu_keikaku_arari")                '9月_計画粗利

        dtOk.Columns.Add("10gatu_keisanyou_uri_heikin_tanka")   '10月_計算用__売上平均単価
        dtOk.Columns.Add("10gatu_keisanyou_siire_heikin_tanka") '10月_計算用__仕入平均単価

        dtOk.Columns.Add("10gatu_keisanyou_koj_hantei_ritu")   '10月_計算用_工事判定率
        dtOk.Columns.Add("10gatu_keisanyou_koj_jyuchuu_ritu")  '10月_計算用_工事受注率
        dtOk.Columns.Add("10gatu_keisanyou_tyoku_koj_ritu")    '10月_計算用_直工事率
        dtOk.Columns.Add("10gatu_keikaku_kensuu")              '10月_計画件数
        dtOk.Columns.Add("10gatu_keikaku_kingaku")             '10月_計画金額
        dtOk.Columns.Add("10gatu_keikaku_arari")               '10月_計画粗利

        dtOk.Columns.Add("11gatu_keisanyou_uri_heikin_tanka")   '11月_計算用__売上平均単価
        dtOk.Columns.Add("11gatu_keisanyou_siire_heikin_tanka") '11月_計算用__仕入平均単価

        dtOk.Columns.Add("11gatu_keisanyou_koj_hantei_ritu")   '11月_計算用_工事判定率
        dtOk.Columns.Add("11gatu_keisanyou_koj_jyuchuu_ritu")  '11月_計算用_工事受注率
        dtOk.Columns.Add("11gatu_keisanyou_tyoku_koj_ritu")    '11月_計算用_直工事率
        dtOk.Columns.Add("11gatu_keikaku_kensuu")              '11月_計画件数
        dtOk.Columns.Add("11gatu_keikaku_kingaku")             '11月_計画金額
        dtOk.Columns.Add("11gatu_keikaku_arari")               '11月_計画粗利

        dtOk.Columns.Add("12gatu_keisanyou_uri_heikin_tanka")   '12月_計算用__売上平均単価
        dtOk.Columns.Add("12gatu_keisanyou_siire_heikin_tanka") '12月_計算用__仕入平均単価

        dtOk.Columns.Add("12gatu_keisanyou_koj_hantei_ritu")   '12月_計算用_工事判定率
        dtOk.Columns.Add("12gatu_keisanyou_koj_jyuchuu_ritu")  '12月_計算用_工事受注率
        dtOk.Columns.Add("12gatu_keisanyou_tyoku_koj_ritu")    '12月_計算用_直工事率
        dtOk.Columns.Add("12gatu_keikaku_kensuu")              '12月_計画件数
        dtOk.Columns.Add("12gatu_keikaku_kingaku")             '12月_計画金額
        dtOk.Columns.Add("12gatu_keikaku_arari")               '12月_計画粗利

        dtOk.Columns.Add("1gatu_keisanyou_uri_heikin_tanka")   '1月_計算用__売上平均単価
        dtOk.Columns.Add("1gatu_keisanyou_siire_heikin_tanka") '1月_計算用__仕入平均単価

        dtOk.Columns.Add("1gatu_keisanyou_koj_hantei_ritu")    '1月_計算用_工事判定率
        dtOk.Columns.Add("1gatu_keisanyou_koj_jyuchuu_ritu")   '1月_計算用_工事受注率
        dtOk.Columns.Add("1gatu_keisanyou_tyoku_koj_ritu")     '1月_計算用_直工事率
        dtOk.Columns.Add("1gatu_keikaku_kensuu")               '1月_計画件数
        dtOk.Columns.Add("1gatu_keikaku_kingaku")              '1月_計画金額
        dtOk.Columns.Add("1gatu_keikaku_arari")                '1月_計画粗利

        dtOk.Columns.Add("2gatu_keisanyou_uri_heikin_tanka")   '2月_計算用__売上平均単価
        dtOk.Columns.Add("2gatu_keisanyou_siire_heikin_tanka") '2月_計算用__仕入平均単価

        dtOk.Columns.Add("2gatu_keisanyou_koj_hantei_ritu")    '2月_計算用_工事判定率
        dtOk.Columns.Add("2gatu_keisanyou_koj_jyuchuu_ritu")   '2月_計算用_工事受注率
        dtOk.Columns.Add("2gatu_keisanyou_tyoku_koj_ritu")     '2月_計算用_直工事率
        dtOk.Columns.Add("2gatu_keikaku_kensuu")               '2月_計画件数
        dtOk.Columns.Add("2gatu_keikaku_kingaku")              '2月_計画金額
        dtOk.Columns.Add("2gatu_keikaku_arari")                '2月_計画粗利

        dtOk.Columns.Add("3gatu_keisanyou_uri_heikin_tanka")   '3月_計算用__売上平均単価
        dtOk.Columns.Add("3gatu_keisanyou_siire_heikin_tanka") '3月_計算用__仕入平均単価

        dtOk.Columns.Add("3gatu_keisanyou_koj_hantei_ritu")    '3月_計算用_工事判定率
        dtOk.Columns.Add("3gatu_keisanyou_koj_jyuchuu_ritu")   '3月_計算用_工事受注率
        dtOk.Columns.Add("3gatu_keisanyou_tyoku_koj_ritu")     '3月_計算用_直工事率
        dtOk.Columns.Add("3gatu_keikaku_kensuu")               '3月_計画件数
        dtOk.Columns.Add("3gatu_keikaku_kingaku")              '3月_計画金額
        dtOk.Columns.Add("3gatu_keikaku_arari")                '3月_計画粗利

        '2013/10/14 李宇追加　↓
        dtOk.Columns.Add("UCCRPDEV")                '報連相_顧客区分
        dtOk.Columns.Add("UCCRPSEQ")                '報連相_顧客コードSEQ
        '2013/10/14 李宇追加　↑

    End Sub

    ''' <summary>
    ''' 予定見込管理OKデータのスキーマを作成
    ''' </summary>
    ''' <param name="dtOk">予定見込管理OKデータ</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Sub CreateOkYoteiMikomiKanri(ByRef dtOk As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtOk)

        dtOk.Columns.Add("keikaku_nendo")                      '計画_年度
        dtOk.Columns.Add("kameiten_cd")                        '加盟店ｺｰﾄﾞ
        dtOk.Columns.Add("keikaku_kanri_syouhin_cd")           '計画管理_商品コード
        dtOk.Columns.Add("kameiten_mei")                       '加盟店名
        dtOk.Columns.Add("bunbetu_cd")                         '分別コード
        dtOk.Columns.Add("4gatu_mikomi_kensuu")                '4月_見込件数
        dtOk.Columns.Add("4gatu_mikomi_kingaku")               '4月_見込金額
        dtOk.Columns.Add("4gatu_mikomi_arari")                 '4月_見込粗利
        dtOk.Columns.Add("5gatu_mikomi_kensuu")                '5月_見込件数
        dtOk.Columns.Add("5gatu_mikomi_kingaku")               '5月_見込金額
        dtOk.Columns.Add("5gatu_mikomi_arari")                 '5月_見込粗利
        dtOk.Columns.Add("6gatu_mikomi_kensuu")                '6月_見込件数
        dtOk.Columns.Add("6gatu_mikomi_kingaku")               '6月_見込金額
        dtOk.Columns.Add("6gatu_mikomi_arari")                 '6月_見込粗利
        dtOk.Columns.Add("7gatu_mikomi_kensuu")                '7月_見込件数
        dtOk.Columns.Add("7gatu_mikomi_kingaku")               '7月_見込金額
        dtOk.Columns.Add("7gatu_mikomi_arari")                 '7月_見込粗利
        dtOk.Columns.Add("8gatu_mikomi_kensuu")                '8月_見込件数
        dtOk.Columns.Add("8gatu_mikomi_kingaku")               '8月_見込金額
        dtOk.Columns.Add("8gatu_mikomi_arari")                 '8月_見込粗利
        dtOk.Columns.Add("9gatu_mikomi_kensuu")                '9月_見込件数
        dtOk.Columns.Add("9gatu_mikomi_kingaku")               '9月_見込金額
        dtOk.Columns.Add("9gatu_mikomi_arari")                 '9月_見込粗利
        dtOk.Columns.Add("10gatu_mikomi_kensuu")               '10月_見込件数
        dtOk.Columns.Add("10gatu_mikomi_kingaku")              '10月_見込金額
        dtOk.Columns.Add("10gatu_mikomi_arari")                '10月_見込粗利
        dtOk.Columns.Add("11gatu_mikomi_kensuu")               '11月_見込件数
        dtOk.Columns.Add("11gatu_mikomi_kingaku")              '11月_見込金額
        dtOk.Columns.Add("11gatu_mikomi_arari")                '11月_見込粗利
        dtOk.Columns.Add("12gatu_mikomi_kensuu")               '12月_見込件数
        dtOk.Columns.Add("12gatu_mikomi_kingaku")              '12月_見込金額
        dtOk.Columns.Add("12gatu_mikomi_arari")                '12月_見込粗利
        dtOk.Columns.Add("1gatu_mikomi_kensuu")                '1月_見込件数
        dtOk.Columns.Add("1gatu_mikomi_kingaku")               '1月_見込金額
        dtOk.Columns.Add("1gatu_mikomi_arari")                 '1月_見込粗利
        dtOk.Columns.Add("2gatu_mikomi_kensuu")                '2月_見込件数
        dtOk.Columns.Add("2gatu_mikomi_kingaku")               '2月_見込金額
        dtOk.Columns.Add("2gatu_mikomi_arari")                 '2月_見込粗利
        dtOk.Columns.Add("3gatu_mikomi_kensuu")                '3月_見込件数
        dtOk.Columns.Add("3gatu_mikomi_kingaku")               '3月_見込金額
        dtOk.Columns.Add("3gatu_mikomi_arari")                 '3月_見込粗利
    End Sub

    ''' <summary>
    ''' FC用計画管理OKデータのスキーマを作成
    ''' </summary>
    ''' <param name="dtOk">FC用計画管理OKデータ</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Sub CreateOkFcKeikakuKanri(ByRef dtOk As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtOk)

        dtOk.Columns.Add("keikaku_nendo")                           '計画_年度
        dtOk.Columns.Add("busyo_cd")                                '部署ｺｰﾄﾞ
        dtOk.Columns.Add("keikaku_kanri_syouhin_cd")                '計画管理_商品コード
        dtOk.Columns.Add("siten_mei")                               '支店名
        dtOk.Columns.Add("bunbetu_cd")                              '分別コード

        dtOk.Columns.Add("4gatu_keisanyou_uri_heikin_tanka")   '4月_計算用__売上平均単価
        dtOk.Columns.Add("4gatu_keisanyou_siire_heikin_tanka") '4月_計算用__仕入平均単価

        dtOk.Columns.Add("4gatu_keisanyou_koj_hantei_ritu")         '4月_計算用_工事判定率
        dtOk.Columns.Add("4gatu_keisanyou_koj_jyuchuu_ritu")        '4月_計算用_工事受注率
        dtOk.Columns.Add("4gatu_keisanyou_tyoku_koj_ritu")          '4月_計算用_直工事率
        dtOk.Columns.Add("4gatu_keikaku_kensuu")                    '4月_計画件数
        dtOk.Columns.Add("4gatu_keikaku_kingaku")                   '4月_計画金額
        dtOk.Columns.Add("4gatu_keikaku_arari")                     '4月_計画粗利

        dtOk.Columns.Add("5gatu_keisanyou_uri_heikin_tanka")   '5月_計算用__売上平均単価
        dtOk.Columns.Add("5gatu_keisanyou_siire_heikin_tanka") '5月_計算用__仕入平均単価

        dtOk.Columns.Add("5gatu_keisanyou_koj_hantei_ritu")         '5月_計算用_工事判定率
        dtOk.Columns.Add("5gatu_keisanyou_koj_jyuchuu_ritu")        '5月_計算用_工事受注率
        dtOk.Columns.Add("5gatu_keisanyou_tyoku_koj_ritu")          '5月_計算用_直工事率
        dtOk.Columns.Add("5gatu_keikaku_kensuu")                    '5月_計画件数
        dtOk.Columns.Add("5gatu_keikaku_kingaku")                   '5月_計画金額
        dtOk.Columns.Add("5gatu_keikaku_arari")                     '5月_計画粗利

        dtOk.Columns.Add("6gatu_keisanyou_uri_heikin_tanka")   '6月_計算用__売上平均単価
        dtOk.Columns.Add("6gatu_keisanyou_siire_heikin_tanka") '6月_計算用__仕入平均単価

        dtOk.Columns.Add("6gatu_keisanyou_koj_hantei_ritu")         '6月_計算用_工事判定率
        dtOk.Columns.Add("6gatu_keisanyou_koj_jyuchuu_ritu")        '6月_計算用_工事受注率
        dtOk.Columns.Add("6gatu_keisanyou_tyoku_koj_ritu")          '6月_計算用_直工事率
        dtOk.Columns.Add("6gatu_keikaku_kensuu")                    '6月_計画件数
        dtOk.Columns.Add("6gatu_keikaku_kingaku")                   '6月_計画金額
        dtOk.Columns.Add("6gatu_keikaku_arari")                     '6月_計画粗利

        dtOk.Columns.Add("7gatu_keisanyou_uri_heikin_tanka")   '7月_計算用__売上平均単価
        dtOk.Columns.Add("7gatu_keisanyou_siire_heikin_tanka") '7月_計算用__仕入平均単価

        dtOk.Columns.Add("7gatu_keisanyou_koj_hantei_ritu")         '7月_計算用_工事判定率
        dtOk.Columns.Add("7gatu_keisanyou_koj_jyuchuu_ritu")        '7月_計算用_工事受注率
        dtOk.Columns.Add("7gatu_keisanyou_tyoku_koj_ritu")          '7月_計算用_直工事率
        dtOk.Columns.Add("7gatu_keikaku_kensuu")                    '7月_計画件数
        dtOk.Columns.Add("7gatu_keikaku_kingaku")                   '7月_計画金額
        dtOk.Columns.Add("7gatu_keikaku_arari")                     '7月_計画粗利

        dtOk.Columns.Add("8gatu_keisanyou_uri_heikin_tanka")   '8月_計算用__売上平均単価
        dtOk.Columns.Add("8gatu_keisanyou_siire_heikin_tanka") '8月_計算用__仕入平均単価

        dtOk.Columns.Add("8gatu_keisanyou_koj_hantei_ritu")         '8月_計算用_工事判定率
        dtOk.Columns.Add("8gatu_keisanyou_koj_jyuchuu_ritu")        '8月_計算用_工事受注率
        dtOk.Columns.Add("8gatu_keisanyou_tyoku_koj_ritu")          '8月_計算用_直工事率
        dtOk.Columns.Add("8gatu_keikaku_kensuu")                    '8月_計画件数
        dtOk.Columns.Add("8gatu_keikaku_kingaku")                   '8月_計画金額
        dtOk.Columns.Add("8gatu_keikaku_arari")                     '8月_計画粗利

        dtOk.Columns.Add("9gatu_keisanyou_uri_heikin_tanka")   '9月_計算用__売上平均単価
        dtOk.Columns.Add("9gatu_keisanyou_siire_heikin_tanka") '9月_計算用__仕入平均単価

        dtOk.Columns.Add("9gatu_keisanyou_koj_hantei_ritu")         '9月_計算用_工事判定率
        dtOk.Columns.Add("9gatu_keisanyou_koj_jyuchuu_ritu")        '9月_計算用_工事受注率
        dtOk.Columns.Add("9gatu_keisanyou_tyoku_koj_ritu")          '9月_計算用_直工事率
        dtOk.Columns.Add("9gatu_keikaku_kensuu")                    '9月_計画件数
        dtOk.Columns.Add("9gatu_keikaku_kingaku")                   '9月_計画金額
        dtOk.Columns.Add("9gatu_keikaku_arari")                     '9月_計画粗利

        dtOk.Columns.Add("10gatu_keisanyou_uri_heikin_tanka")   '10月_計算用__売上平均単価
        dtOk.Columns.Add("10gatu_keisanyou_siire_heikin_tanka") '10月_計算用__仕入平均単価

        dtOk.Columns.Add("10gatu_keisanyou_koj_hantei_ritu")        '10月_計算用_工事判定率
        dtOk.Columns.Add("10gatu_keisanyou_koj_jyuchuu_ritu")       '10月_計算用_工事受注率
        dtOk.Columns.Add("10gatu_keisanyou_tyoku_koj_ritu")         '10月_計算用_直工事率
        dtOk.Columns.Add("10gatu_keikaku_kensuu")                   '10月_計画件数
        dtOk.Columns.Add("10gatu_keikaku_kingaku")                  '10月_計画金額
        dtOk.Columns.Add("10gatu_keikaku_arari")                    '10月_計画粗利

        dtOk.Columns.Add("11gatu_keisanyou_uri_heikin_tanka")   '11月_計算用__売上平均単価
        dtOk.Columns.Add("11gatu_keisanyou_siire_heikin_tanka") '11月_計算用__仕入平均単価

        dtOk.Columns.Add("11gatu_keisanyou_koj_hantei_ritu")        '11月_計算用_工事判定率
        dtOk.Columns.Add("11gatu_keisanyou_koj_jyuchuu_ritu")       '11月_計算用_工事受注率
        dtOk.Columns.Add("11gatu_keisanyou_tyoku_koj_ritu")         '11月_計算用_直工事率
        dtOk.Columns.Add("11gatu_keikaku_kensuu")                   '11月_計画件数
        dtOk.Columns.Add("11gatu_keikaku_kingaku")                  '11月_計画金額
        dtOk.Columns.Add("11gatu_keikaku_arari")                    '11月_計画粗利

        dtOk.Columns.Add("12gatu_keisanyou_uri_heikin_tanka")   '12月_計算用__売上平均単価
        dtOk.Columns.Add("12gatu_keisanyou_siire_heikin_tanka") '12月_計算用__仕入平均単価

        dtOk.Columns.Add("12gatu_keisanyou_koj_hantei_ritu")        '12月_計算用_工事判定率
        dtOk.Columns.Add("12gatu_keisanyou_koj_jyuchuu_ritu")       '12月_計算用_工事受注率
        dtOk.Columns.Add("12gatu_keisanyou_tyoku_koj_ritu")         '12月_計算用_直工事率
        dtOk.Columns.Add("12gatu_keikaku_kensuu")                   '12月_計画件数
        dtOk.Columns.Add("12gatu_keikaku_kingaku")                  '12月_計画金額
        dtOk.Columns.Add("12gatu_keikaku_arari")                    '12月_計画粗利

        dtOk.Columns.Add("1gatu_keisanyou_uri_heikin_tanka")   '1月_計算用__売上平均単価
        dtOk.Columns.Add("1gatu_keisanyou_siire_heikin_tanka") '1月_計算用__仕入平均単価

        dtOk.Columns.Add("1gatu_keisanyou_koj_hantei_ritu")         '1月_計算用_工事判定率
        dtOk.Columns.Add("1gatu_keisanyou_koj_jyuchuu_ritu")        '1月_計算用_工事受注率
        dtOk.Columns.Add("1gatu_keisanyou_tyoku_koj_ritu")          '1月_計算用_直工事率
        dtOk.Columns.Add("1gatu_keikaku_kensuu")                    '1月_計画件数
        dtOk.Columns.Add("1gatu_keikaku_kingaku")                   '1月_計画金額
        dtOk.Columns.Add("1gatu_keikaku_arari")                     '1月_計画粗利

        dtOk.Columns.Add("2gatu_keisanyou_uri_heikin_tanka")   '2月_計算用__売上平均単価
        dtOk.Columns.Add("2gatu_keisanyou_siire_heikin_tanka") '2月_計算用__仕入平均単価

        dtOk.Columns.Add("2gatu_keisanyou_koj_hantei_ritu")         '2月_計算用_工事判定率
        dtOk.Columns.Add("2gatu_keisanyou_koj_jyuchuu_ritu")        '2月_計算用_工事受注率
        dtOk.Columns.Add("2gatu_keisanyou_tyoku_koj_ritu")          '2月_計算用_直工事率
        dtOk.Columns.Add("2gatu_keikaku_kensuu")                    '2月_計画件数
        dtOk.Columns.Add("2gatu_keikaku_kingaku")                   '2月_計画金額
        dtOk.Columns.Add("2gatu_keikaku_arari")                     '2月_計画粗利

        dtOk.Columns.Add("3gatu_keisanyou_uri_heikin_tanka")   '3月_計算用__売上平均単価
        dtOk.Columns.Add("3gatu_keisanyou_siire_heikin_tanka") '3月_計算用__仕入平均単価

        dtOk.Columns.Add("3gatu_keisanyou_koj_hantei_ritu")         '3月_計算用_工事判定率
        dtOk.Columns.Add("3gatu_keisanyou_koj_jyuchuu_ritu")        '3月_計算用_工事受注率
        dtOk.Columns.Add("3gatu_keisanyou_tyoku_koj_ritu")          '3月_計算用_直工事率
        dtOk.Columns.Add("3gatu_keikaku_kensuu")                    '3月_計画件数
        dtOk.Columns.Add("3gatu_keikaku_kingaku")                   '3月_計画金額
        dtOk.Columns.Add("3gatu_keikaku_arari")                     '3月_計画粗利
        dtOk.Columns.Add("keikaku_kakutei_flg")                     '計画確定FLG
        dtOk.Columns.Add("keikaku_kakutei_id")                      '計画確定者ID
        dtOk.Columns.Add("keikaku_kakutei_datetime")                '計画確定日時
        dtOk.Columns.Add("kakutei_minaosi_id")                      '確定見直し者ID
        dtOk.Columns.Add("kakutei_minaosi_datetime")                '確定見直し日時
        dtOk.Columns.Add("keikaku_minaosi_flg")                     '計画見直しFLG
        dtOk.Columns.Add("keikaku_huhen_flg")                       '計画値不変FLG
    End Sub

    ''' <summary>
    ''' FC用予定見込管理OKデータのスキーマを作成
    ''' </summary>
    ''' <param name="dtOk">FC用予定見込管理OKデータ</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Sub CreateOkFcYoteiMikomiKanri(ByRef dtOk As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtOk)

        dtOk.Columns.Add("keikaku_nendo")                      '計画_年度
        dtOk.Columns.Add("busyo_cd")                           '部署ｺｰﾄﾞ
        dtOk.Columns.Add("keikaku_kanri_syouhin_cd")           '計画管理_商品コード
        dtOk.Columns.Add("siten_mei")                          '支店名
        dtOk.Columns.Add("bunbetu_cd")                         '分別コード
        dtOk.Columns.Add("4gatu_mikomi_kensuu")                '4月_見込件数
        dtOk.Columns.Add("4gatu_mikomi_kingaku")               '4月_見込金額
        dtOk.Columns.Add("4gatu_mikomi_arari")                 '4月_見込粗利
        dtOk.Columns.Add("5gatu_mikomi_kensuu")                '5月_見込件数
        dtOk.Columns.Add("5gatu_mikomi_kingaku")               '5月_見込金額
        dtOk.Columns.Add("5gatu_mikomi_arari")                 '5月_見込粗利
        dtOk.Columns.Add("6gatu_mikomi_kensuu")                '6月_見込件数
        dtOk.Columns.Add("6gatu_mikomi_kingaku")               '6月_見込金額
        dtOk.Columns.Add("6gatu_mikomi_arari")                 '6月_見込粗利
        dtOk.Columns.Add("7gatu_mikomi_kensuu")                '7月_見込件数
        dtOk.Columns.Add("7gatu_mikomi_kingaku")               '7月_見込金額
        dtOk.Columns.Add("7gatu_mikomi_arari")                 '7月_見込粗利
        dtOk.Columns.Add("8gatu_mikomi_kensuu")                '8月_見込件数
        dtOk.Columns.Add("8gatu_mikomi_kingaku")               '8月_見込金額
        dtOk.Columns.Add("8gatu_mikomi_arari")                 '8月_見込粗利
        dtOk.Columns.Add("9gatu_mikomi_kensuu")                '9月_見込件数
        dtOk.Columns.Add("9gatu_mikomi_kingaku")               '9月_見込金額
        dtOk.Columns.Add("9gatu_mikomi_arari")                 '9月_見込粗利
        dtOk.Columns.Add("10gatu_mikomi_kensuu")               '10月_見込件数
        dtOk.Columns.Add("10gatu_mikomi_kingaku")              '10月_見込金額
        dtOk.Columns.Add("10gatu_mikomi_arari")                '10月_見込粗利
        dtOk.Columns.Add("11gatu_mikomi_kensuu")               '11月_見込件数
        dtOk.Columns.Add("11gatu_mikomi_kingaku")              '11月_見込金額
        dtOk.Columns.Add("11gatu_mikomi_arari")                '11月_見込粗利
        dtOk.Columns.Add("12gatu_mikomi_kensuu")               '12月_見込件数
        dtOk.Columns.Add("12gatu_mikomi_kingaku")              '12月_見込金額
        dtOk.Columns.Add("12gatu_mikomi_arari")                '12月_見込粗利
        dtOk.Columns.Add("1gatu_mikomi_kensuu")                '1月_見込件数
        dtOk.Columns.Add("1gatu_mikomi_kingaku")               '1月_見込金額
        dtOk.Columns.Add("1gatu_mikomi_arari")                 '1月_見込粗利
        dtOk.Columns.Add("2gatu_mikomi_kensuu")                '2月_見込件数
        dtOk.Columns.Add("2gatu_mikomi_kingaku")               '2月_見込金額
        dtOk.Columns.Add("2gatu_mikomi_arari")                 '2月_見込粗利
        dtOk.Columns.Add("3gatu_mikomi_kensuu")                '3月_見込件数
        dtOk.Columns.Add("3gatu_mikomi_kingaku")               '3月_見込金額
        dtOk.Columns.Add("3gatu_mikomi_arari")                 '3月_見込粗利

    End Sub

    ''' <summary>
    ''' 計画管理表_取込エラー情報データのスキーマを作成
    ''' </summary>
    ''' <param name="dtError">計画管理表_取込エラー情報データテーブル</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成</para>																															
    ''' </history>
    Public Sub CreateErrorDataTable(ByRef dtError As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtError)

        dtError.Columns.Add("edi_jouhou_sakusei_date")             'EDI情報作成日
        dtError.Columns.Add("syori_datetime")                      '処理日時
        dtError.Columns.Add("gyou_no")                             '行NO
        dtError.Columns.Add("error_naiyou")                        'エラー内容
    End Sub

    ''' <summary>
    ''' 計画管理表_取込エラー情報テーブルを作成する
    ''' </summary>
    ''' <param name="intLineNo">取込データ当り前行</param>
    ''' <param name="strErrorNaiyou">エラー区分</param>
    ''' <param name="dtKeikakuTorikomiError">計画管理表_取込エラー情報テーブル</param>
    ''' <remarks>計画管理表_取込エラー情報テーブルを作成する</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Private Sub SetKeikakuTorikomiErrorData(ByVal intLineNo As Integer, _
                                            ByVal strErrorNaiyou As String, _
                                            ByRef dtKeikakuTorikomiError As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                intLineNo, strErrorNaiyou, dtKeikakuTorikomiError)

        Dim dr As Data.DataRow
        dr = dtKeikakuTorikomiError.NewRow
        dr.Item("gyou_no") = intLineNo                                  '計画_年度
        dr.Item("error_naiyou") = strErrorNaiyou.Replace("\r\n", "")    'エラー区分

        dtKeikakuTorikomiError.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <param name="strLine">取込データ当り前行</param>
    ''' <param name="strCsvKbn">取込CSV区分</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前行必須存在チェック</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Public Function ChkNotNull(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, strCsvKbn)

        Select Case strCsvKbn
            Case csvKeikaku
                For Each i As Integer In KEIKAKU_NOTNULL_INDEX
                    'データ項目を必須チェック
                    If String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) Then
                        Return False
                    End If
                Next
            Case csvMikomi
                For Each i As Integer In MIKOMI_NOTNULL_INDEX
                    'データ項目を必須チェック
                    If String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) Then
                        Return False
                    End If
                Next
            Case csvFcKeikaku
                For Each i As Integer In FC_KEIKAKU_NOTNULL_INDEX
                    'データ項目を必須チェック
                    If String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) Then
                        Return False
                    End If
                Next
            Case csvFcMikomi
                For Each i As Integer In FC_MIKOMI_NOTNULL_INDEX
                    'データ項目を必須チェック
                    If String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) Then
                        Return False
                    End If
                Next
        End Select

        Return True
    End Function

    ''' <summary>
    ''' 項目最大長チェック
    ''' </summary>
    ''' <param name="strLine">当り前行</param>
    ''' <param name="strCsvKbn">取込CSV区分</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前行各個項目最大長チェック</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Public Function ChkMaxLength(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, strCsvKbn)

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        For i As Integer = 0 To strLine.Split(CChar(",")).Length - 1
            Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(CChar(","))(i))
            Select Case strCsvKbn
                Case csvKeikaku
                    If i < KEIKAKU_MAX_LENGTH.Length AndAlso btBytes.LongLength > KEIKAKU_MAX_LENGTH(i) Then
                        Return False
                    End If
                Case csvMikomi
                    If i < MIKOMI_MAX_LENGTH.Length AndAlso btBytes.LongLength > MIKOMI_MAX_LENGTH(i) Then
                        Return False
                    End If
                Case csvFcKeikaku
                    If i < FC_KEIKAKU_MAX_LENGTH.Length AndAlso btBytes.LongLength > FC_KEIKAKU_MAX_LENGTH(i) Then
                        Return False
                    End If
                Case csvFcMikomi
                    If i < FC_MIKOMI_MAX_LENGTH.Length AndAlso btBytes.LongLength > FC_MIKOMI_MAX_LENGTH(i) Then
                        Return False
                    End If
            End Select
        Next

        Return True
    End Function

    ''' <summary>
    ''' 最大長を切り取る
    ''' </summary>
    ''' <param name="strValue">取込ファイル詳細</param>
    ''' <param name="intMaxByteCount">"128"を固定</param>
    ''' <returns>取込ファイルの名称</returns>
    ''' <remarks>取込ファイルの名称を取得する</remarks>
    ''' <history>
    ''' <para>2012/12/20 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Public Function CutMaxLength(ByVal strValue As String, ByVal intMaxByteCount As Integer) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strValue, intMaxByteCount)

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Dim intLengthCount As Integer = 0
        For i As Integer = strValue.Length To 0 Step -1
            Dim btBytes As Byte() = hEncoding.GetBytes(Left(strValue, i))
            If btBytes.LongLength <= intMaxByteCount Then
                intLengthCount = i
                Exit For
            End If
        Next

        Return Left(strValue, intLengthCount)
    End Function

    ''' <summary>
    ''' 禁則文字チェック
    ''' </summary>
    ''' <param name="target">当り前項目</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前項目禁則文字チェック</remarks>
    ''' <history>
    ''' <para>2012/12/20 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Public Function ChkKinjiMoji(ByVal target As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, target)

        For Each st As String In arrayKinsiStr

            If target.IndexOf(st) >= 0 Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' 数値型項目チェック
    ''' </summary>
    ''' <param name="strLine">取込データ当り前行</param>
    ''' <param name="strCsvKbn">取込CSV区分</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前行数値型項目チェック</remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Public Function ChkSuuti(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, strCsvKbn)

        Select Case strCsvKbn
            Case csvKeikaku
                '計画CSVの数値型項目チェック
                For Each i As Integer In KEIKAKU_NUM_INDEX

                    If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                        Return False
                    End If
                Next
            Case csvMikomi
                '見込CSVの数値型項目チェック
                For Each i As Integer In MIKOMI_NUM_INDEX

                    If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                        Return False
                    End If
                Next
            Case csvFcKeikaku
                'ＦＣ用計画CSVの数値型項目チェック
                For Each i As Integer In FC_KEIKAKU_NUM_INDEX

                    If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                        Return False
                    End If
                Next
            Case csvFcMikomi
                'ＦＣ用見込CSVの数値型項目チェック
                For Each i As Integer In FC_MIKOMI_NUM_INDEX

                    If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                        Return False
                    End If
                Next
        End Select

        Return True
    End Function

    ''' <summary>
    ''' 小数型項目チェック
    ''' </summary>
    ''' <param name="strLine">取込データ当り前行</param>
    ''' <param name="strCsvKbn">取込CSV区分</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前行小数型項目チェック</remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Public Function ChkSyouSuu(ByVal strLine As String, ByVal strCsvKbn As String) As Integer
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, strCsvKbn)

        Select Case strCsvKbn
            Case csvKeikaku
                '計画CSVの小数型項目チェック
                For Each i As Integer In KEIKAKU_DEC_INDEX
                    If strLine.Split(CChar(","))(i).Trim <> "100" Then
                        If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i).Replace(".", ""))) Then
                            Return 1
                        End If
                        If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckDecimal(strLine.Split(CChar(","))(i), 2, 1)) Then
                            Return 2
                        End If
                    End If
                Next
            Case csvFcKeikaku
                'ＦＣ用計画CSVの小数型項目チェック
                For Each i As Integer In FC_KEIKAKU_DEC_INDEX
                    If strLine.Split(CChar(","))(i).Trim <> "100" Then
                        If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i).Replace(".", ""))) Then
                            Return 1
                        End If

                        If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckDecimal(strLine.Split(CChar(","))(i), 2, 1)) Then
                            Return 2
                        End If
                    End If
                Next
        End Select

        Return 0
    End Function

    ''' <summary>
    ''' 整数チェック
    ''' </summary>
    ''' <param name="inTarget">当り前項目</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前項目整数チェック</remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Function CheckHankaku(ByVal inTarget As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, inTarget)

        If inTarget.Length = System.Text.Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            Try
                Dim intTemp As Long = CLng(inTarget)
            Catch ex As Exception
                Return False
            End Try

            If InStr(inTarget, ".") > 0 Or InStr(inTarget, "+") > 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 正整数 + 小数のチェック
    ''' </summary>
    ''' <param name="checkValue">チェック対象</param>
    ''' <param name="seisuuCount">整数桁数</param>
    ''' <param name="syousuuCount">小数桁数</param>
    ''' <returns>チェック結果</returns>
    ''' <remarks>正整数 + 小数のチェック</remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Public Function CheckDecimal(ByVal checkValue As String, _
                                 ByVal seisuuCount As Integer, _
                                 ByVal syousuuCount As Integer) As Boolean
        Dim patternValue As String
        If syousuuCount > 0 Then
            patternValue = "^[0-9]{1," & Convert.ToInt32(seisuuCount) & _
                           "}$|^[0-9]{1," & Convert.ToInt32(seisuuCount) & _
                           "}\.[0-9]{1," & Convert.ToInt32(syousuuCount) & "}$"

        Else
            patternValue = "^[0-9]{1," & Convert.ToInt32(seisuuCount) & "}$"

        End If

        Return CheckRegex(checkValue, patternValue)
    End Function

    ''' <summary>
    ''' 正規表現制御
    ''' </summary>
    ''' <param name="checkValue">制御文字</param>
    ''' <param name="patternValue">正規表現式</param>
    ''' <returns>正規表現制御</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 曹敬仁 新規作成 </para>
    ''' </history>
    Private Function CheckRegex(ByVal checkValue As String, ByVal patternValue As String) As Boolean
        If checkValue.Equals(String.Empty) Then
            Return True
        End If

        Return Regex.Match(checkValue, patternValue).Success
    End Function
End Class
