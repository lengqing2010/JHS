Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Imports Lixil.JHS_EKKS.Utilities
Public Class KeikakuKanriSearchListDA
    Private strCountSQL As String
    Private paramList2 As New List(Of SqlClient.SqlParameter)
    Private strMeisaiSQL As String
    Public ReadOnly Property GetMeisaiSQL() As String
        Get
            Return strMeisaiSQL
        End Get
    End Property
    ''' <summary>
    ''' 計画管理画面により、検索結果のデータをを取得する
    ''' </summary>
    ''' <param name="KeikakuKanriRecord">検索条件</param>
    ''' <param name="SelectKbn">明細部SQL区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function SelKeikakuKanriData(ByVal KeikakuKanriRecord As KeikakuKanriRecord, ByVal SelectKbn As KeikakuKanriRecord.selectKbn, ByVal blnCsv As Boolean) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, KeikakuKanriRecord)

        Dim dsInfo As New Data.DataSet
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim strTmp As String = ""
        'パラメータ格納


        If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.goukeiFC Then
            strTmp = SelectCommon(KeikakuKanriRecord, Utilities.KeikakuKanriRecord.selectKbn.goukeiFC, blnCsv, paramList)
            strTmp = strTmp & SelectCommon(KeikakuKanriRecord, Utilities.KeikakuKanriRecord.selectKbn.meisai, blnCsv, paramList)
            'strTmp = strTmp & " GROUP BY MKM2.meisyou,MKKS.syouhin_mei,MKM2.hyouji_jyun,TKK.syouhin_cd "
            strTmp = strTmp & " UNION ALL "
            strTmp = strTmp & SelectCommon(KeikakuKanriRecord, Utilities.KeikakuKanriRecord.selectKbn.FC, blnCsv, paramList)
            'strTmp = strTmp & " GROUP BY MKM2.meisyou,MKKS.keikaku_kanri_syouhin_mei,MKM2.hyouji_jyun,MKKS.hyouji_jyun,TKK.syouhin_cd,MKKS.kensuu_count_umu,TKK.keikaku_kakutei_flg ) AS MAIN "
            strTmp = strTmp & "  ) AS MAIN "

        Else
            strTmp = SelectCommon(KeikakuKanriRecord, SelectKbn, blnCsv, paramList)
        End If

        Select Case SelectKbn
            Case Utilities.KeikakuKanriRecord.selectKbn.meisai
                paramList2 = paramList
                If KeikakuKanriRecord.Siborikomi.NenkanTouSuu Then
                    strTmp = strTmp & " ORDER BY MKK.keikaku_nenkan_tousuu DESC,MKM.hyouji_jyun,MKK.kameiten_cd,MKM2.hyouji_jyun,MKKS.hyouji_jyun"
                Else
                    strTmp = strTmp & " ORDER BY TKJK.uri_hiritu DESC,MKM.hyouji_jyun,MKK.kameiten_cd,MKM2.hyouji_jyun,MKKS.hyouji_jyun"
                End If
            Case Utilities.KeikakuKanriRecord.selectKbn.syoukei

                strTmp = strTmp & " GROUP BY MKM.meisyou,MKM2.meisyou,MKKS.keikaku_kanri_syouhin_mei,MKM2.hyouji_jyun,MKM.hyouji_jyun,TKK.syouhin_cd ,MKKS.hyouji_jyun"
                strTmp = strTmp & " ORDER BY MKM.hyouji_jyun,MKM2.hyouji_jyun,MKKS.hyouji_jyun "
            Case Utilities.KeikakuKanriRecord.selectKbn.goukei

                strTmp = strTmp & " GROUP BY MKM2.meisyou,MKKS.keikaku_kanri_syouhin_mei,MKM2.hyouji_jyun,TKK.syouhin_cd ,MKKS.hyouji_jyun "
                strTmp = strTmp & " ORDER BY MKM2.hyouji_jyun,MKKS.hyouji_jyun "
            Case Utilities.KeikakuKanriRecord.selectKbn.FC
                strTmp = strTmp & " ORDER BY MKM2.hyouji_jyun,MKKS.hyouji_jyun "
            Case Utilities.KeikakuKanriRecord.selectKbn.goukeiFC
                strTmp = strTmp & "  GROUP BY meisyou2,syouhin_mei,hyouji_jyun2,syouhin_cd,syouhin_jyun "
                strTmp = strTmp & " ORDER BY hyouji_jyun2,syouhin_jyun "
        End Select


        FillDataset(ManagerDA.Connection, CommandType.Text, strTmp, dsInfo, "dsInfo", paramList.ToArray())
        If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.meisai Then
            strMeisaiSQL = strTmp
        End If
        Return dsInfo.Tables(0)

    End Function

    Public Function SelCount() As Integer
        Dim dsInfo As New Data.DataSet
        FillDataset(ManagerDA.Connection, CommandType.Text, strCountSQL, dsInfo, "dsInfo", paramList2.ToArray())

        Return CInt(dsInfo.Tables(0).Rows(0).Item(0).ToString)
    End Function
    ''' <summary>
    ''' 共通SELECT処理
    ''' </summary>
    ''' <param name="KeikakuKanriRecord">検索条件</param>
    ''' <param name="SelectKbn">明細部SQL区分</param>
    ''' <param name="paramList">パラメータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SelectCommon(ByVal KeikakuKanriRecord As KeikakuKanriRecord, ByVal SelectKbn As KeikakuKanriRecord.selectKbn, ByVal blnCsv As Boolean, ByRef paramList As List(Of SqlClient.SqlParameter)) As String

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim sqlEigyou As New System.Text.StringBuilder
        Dim sqlSiborikomi As New System.Text.StringBuilder
        Dim sqlString As New System.Text.StringBuilder

        Dim intSuu As Integer = 0


        With sqlBuffer
            Select Case SelectKbn
                Case Utilities.KeikakuKanriRecord.selectKbn.meisai
                    .AppendLine("  SELECT  ")
                    .AppendLine("  MKK.kameiten_mei ")              '加盟店名
                    .AppendLine("  ,MKK.kameiten_cd  ")             '加盟店コード
                    .AppendLine("  ,MKM.meisyou  ")                 '営業区分
                    .AppendLine("  ,MKK.eigyou_tantousya_mei  ")    '営業担当者名
                    .AppendLine("  ,MKK.keikaku_nenkan_tousuu  ")   '年間棟数

                    .AppendLine("  ,TKK.[keikakuyou_nenkan_tousuu] ")   '計画用_年間棟数

                    .AppendLine("  ,MKK.gyoutai  ")                 '業態
                    .AppendLine("  ,TKJK.uri_hiritu  ")             '売上比率

                    .AppendLine("  ,TKK.[sds_kaisi_nengetu] ")      'SDS開始年月

                    .AppendLine("  ,TKJK.koj_hantei_ritu  ")        '工事判定率
                    .AppendLine("  ,TKJK.koj_jyuchuu_ritu  ")       '工事受注率
                    .AppendLine("  ,TKJK.tyoku_koj_ritu  ")         '直工事率
                    .AppendLine("  ,MKM2.meisyou AS meisyou2  ")    '名称種別
                    .AppendLine("  ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei  ")   '商品名
                    If blnCsv Then
                        .AppendLine("  ,ISNULL(TJK.[zennen_heikin_tanka],0) AS zennen_heikin_tanka  ")     '前年_平均単価
                    Else

                        .AppendLine("  ,TJK.[zennen_heikin_tanka] AS zennen_heikin_tanka  ")     '前年_平均単価
                    End If

                    .AppendLine("  ,TJK.[4gatu_jisseki_kensuu] ")     '4月_実績件数
                    .AppendLine("  ,TJK.[4gatu_jisseki_kingaku]")    '4月_実績金額
                    .AppendLine("  ,TJK.[4gatu_jisseki_arari] ")      '4月_実績粗利
                    .AppendLine("  ,TJK.[5gatu_jisseki_kensuu] ")     '5月_実績件数
                    .AppendLine("  ,TJK.[5gatu_jisseki_kingaku] ")    '5月_実績金額
                    .AppendLine("  ,TJK.[5gatu_jisseki_arari] ")      '5月_実績粗利
                    .AppendLine("  ,TJK.[6gatu_jisseki_kensuu] ")     '6月_実績件数
                    .AppendLine("  ,TJK.[6gatu_jisseki_kingaku] ")    '6月_実績金額
                    .AppendLine("  ,TJK.[6gatu_jisseki_arari] ")      '6月_実績粗利
                    .AppendLine("  ,TJK.[7gatu_jisseki_kensuu] ")     '7月_実績件数
                    .AppendLine("  ,TJK.[7gatu_jisseki_kingaku]")    '7月_実績金額
                    .AppendLine("  ,TJK.[7gatu_jisseki_arari] ")      '7月_実績粗利
                    .AppendLine("  ,TJK.[8gatu_jisseki_kensuu] ")     '8月_実績件数
                    .AppendLine("  ,TJK.[8gatu_jisseki_kingaku] ")    '8月_実績金額
                    .AppendLine("  ,TJK.[8gatu_jisseki_arari] ")      '8月_実績粗利
                    .AppendLine("  ,TJK.[9gatu_jisseki_kensuu] ")     '9月_実績件数
                    .AppendLine("  ,TJK.[9gatu_jisseki_kingaku] ")    '9月_実績金額
                    .AppendLine("  ,TJK.[9gatu_jisseki_arari] ")      '9月_実績粗利
                    .AppendLine("  ,TJK.[10gatu_jisseki_kensuu] ")    '10月_実績件数
                    .AppendLine("  ,TJK.[10gatu_jisseki_kingaku] ")   '10月_実績金額
                    .AppendLine("  ,TJK.[10gatu_jisseki_arari] ")     '10月_実績粗利
                    .AppendLine("  ,TJK.[11gatu_jisseki_kensuu] ")    '11月_実績件数
                    .AppendLine("  ,TJK.[11gatu_jisseki_kingaku] ")   '11月_実績金額
                    .AppendLine("  ,TJK.[11gatu_jisseki_arari] ")     '11月_実績粗利
                    .AppendLine("  ,TJK.[12gatu_jisseki_kensuu] ")    '12月_実績件数
                    .AppendLine("  ,TJK.[12gatu_jisseki_kingaku] ")   '12月_実績金額
                    .AppendLine("  ,TJK.[12gatu_jisseki_arari] ")     '12月_実績粗利
                    .AppendLine("  ,TJK.[1gatu_jisseki_kensuu] ")     '1月_実績件数
                    .AppendLine("  ,TJK.[1gatu_jisseki_kingaku] ")    '1月_実績金額
                    .AppendLine("  ,TJK.[1gatu_jisseki_arari] ")      '1月_実績粗利
                    .AppendLine("  ,TJK.[2gatu_jisseki_kensuu] ")     '2月_実績件数
                    .AppendLine("  ,TJK.[2gatu_jisseki_kingaku] ")    '2月_実績金額
                    .AppendLine("  ,TJK.[2gatu_jisseki_arari] ")      '2月_実績粗利
                    .AppendLine("  ,TJK.[3gatu_jisseki_kensuu] ")     '3月_実績件数
                    .AppendLine("  ,TJK.[3gatu_jisseki_kingaku] ")    '3月_実績金額
                    .AppendLine("  ,TJK.[3gatu_jisseki_arari] ")      '3月_実績粗利

                    .AppendLine("  ,TKK.[4gatu_keikaku_kensuu] ")         '4月_計画件数
                    .AppendLine("  ,TKK.[4gatu_keikaku_kingaku] ")        '4月_計画金額
                    .AppendLine("  ,TKK.[4gatu_keikaku_arari] ")          '4月_計画粗利
                    .AppendLine("  ,TKK.[5gatu_keikaku_kensuu] ")         '5月_計画件数
                    .AppendLine("  ,TKK.[5gatu_keikaku_kingaku] ")        '5月_計画金額
                    .AppendLine("  ,TKK.[5gatu_keikaku_arari] ")          '5月_計画粗利
                    .AppendLine("  ,TKK.[6gatu_keikaku_kensuu] ")         '6月_計画件数
                    .AppendLine("  ,TKK.[6gatu_keikaku_kingaku] ")        '6月_計画金額
                    .AppendLine("  ,TKK.[6gatu_keikaku_arari] ")          '6月_計画粗利
                    .AppendLine("  ,TKK.[7gatu_keikaku_kensuu] ")         '7月_計画件数
                    .AppendLine("  ,TKK.[7gatu_keikaku_kingaku] ")        '7月_計画金額
                    .AppendLine("  ,TKK.[7gatu_keikaku_arari] ")          '7月_計画粗利
                    .AppendLine("  ,TKK.[8gatu_keikaku_kensuu] ")         '8月_計画件数
                    .AppendLine("  ,TKK.[8gatu_keikaku_kingaku] ")        '8月_計画金額
                    .AppendLine("  ,TKK.[8gatu_keikaku_arari] ")          '8月_計画粗利
                    .AppendLine("  ,TKK.[9gatu_keikaku_kensuu] ")         '9月_計画件数
                    .AppendLine("  ,TKK.[9gatu_keikaku_kingaku] ")        '9月_計画金額
                    .AppendLine("  ,TKK.[9gatu_keikaku_arari] ")          '9月_計画粗利
                    .AppendLine("  ,TKK.[10gatu_keikaku_kensuu] ")        '10月_計画件数
                    .AppendLine("  ,TKK.[10gatu_keikaku_kingaku] ")       '10月_計画金額
                    .AppendLine("  ,TKK.[10gatu_keikaku_arari] ")         '10月_計画粗利
                    .AppendLine("  ,TKK.[11gatu_keikaku_kensuu] ")        '11月_計画件数
                    .AppendLine("  ,TKK.[11gatu_keikaku_kingaku] ")       '11月_計画金額
                    .AppendLine("  ,TKK.[11gatu_keikaku_arari] ")         '11月_計画粗利
                    .AppendLine("  ,TKK.[12gatu_keikaku_kensuu] ")        '12月_計画件数
                    .AppendLine("  ,TKK.[12gatu_keikaku_kingaku] ")       '12月_計画金額
                    .AppendLine("  ,TKK.[12gatu_keikaku_arari] ")         '12月_計画粗利
                    .AppendLine("  ,TKK.[1gatu_keikaku_kensuu] ")         '1月_計画件数
                    .AppendLine("  ,TKK.[1gatu_keikaku_kingaku] ")        '1月_計画金額
                    .AppendLine("  ,TKK.[1gatu_keikaku_arari] ")          '1月_計画粗利
                    .AppendLine("  ,TKK.[2gatu_keikaku_kensuu] ")         '2月_計画件数
                    .AppendLine("  ,TKK.[2gatu_keikaku_kingaku] ")        '2月_計画金額
                    .AppendLine("  ,TKK.[2gatu_keikaku_arari] ")          '2月_計画粗利
                    .AppendLine("  ,TKK.[3gatu_keikaku_kensuu] ")         '3月_計画件数
                    .AppendLine("  ,TKK.[3gatu_keikaku_kingaku] ")        '3月_計画金額
                    .AppendLine("  ,TKK.[3gatu_keikaku_arari] ")          '3月_計画粗利

                    .AppendLine("  ,TYMK.[4gatu_mikomi_kensuu] ")         '4月_見込件数
                    .AppendLine("  ,TYMK.[4gatu_mikomi_kingaku] ")        '4月_見込金額
                    .AppendLine("  ,TYMK.[4gatu_mikomi_arari] ")          '4月_見込粗利
                    .AppendLine("  ,TYMK.[5gatu_mikomi_kensuu] ")         '5月_見込件数
                    .AppendLine("  ,TYMK.[5gatu_mikomi_kingaku] ")        '5月_見込金額
                    .AppendLine("  ,TYMK.[5gatu_mikomi_arari] ")          '5月_見込粗利
                    .AppendLine("  ,TYMK.[6gatu_mikomi_kensuu] ")         '6月_見込件数
                    .AppendLine("  ,TYMK.[6gatu_mikomi_kingaku] ")        '6月_見込金額
                    .AppendLine("  ,TYMK.[6gatu_mikomi_arari] ")          '6月_見込粗利
                    .AppendLine("  ,TYMK.[7gatu_mikomi_kensuu] ")         '7月_見込件数
                    .AppendLine("  ,TYMK.[7gatu_mikomi_kingaku] ")        '7月_見込金額
                    .AppendLine("  ,TYMK.[7gatu_mikomi_arari] ")          '7月_見込粗利
                    .AppendLine("  ,TYMK.[8gatu_mikomi_kensuu] ")         '8月_見込件数
                    .AppendLine("  ,TYMK.[8gatu_mikomi_kingaku] ")        '8月_見込金額
                    .AppendLine("  ,TYMK.[8gatu_mikomi_arari] ")          '8月_見込粗利
                    .AppendLine("  ,TYMK.[9gatu_mikomi_kensuu] ")         '9月_見込件数
                    .AppendLine("  ,TYMK.[9gatu_mikomi_kingaku] ")        '9月_見込金額
                    .AppendLine("  ,TYMK.[9gatu_mikomi_arari] ")          '9月_見込粗利
                    .AppendLine("  ,TYMK.[10gatu_mikomi_kensuu] ")        '10月_見込件数
                    .AppendLine("  ,TYMK.[10gatu_mikomi_kingaku] ")       '10月_見込金額
                    .AppendLine("  ,TYMK.[10gatu_mikomi_arari] ")         '10月_見込粗利
                    .AppendLine("  ,TYMK.[11gatu_mikomi_kensuu] ")        '11月_見込件数
                    .AppendLine("  ,TYMK.[11gatu_mikomi_kingaku] ")       '11月_見込金額
                    .AppendLine("  ,TYMK.[11gatu_mikomi_arari] ")         '11月_見込粗利
                    .AppendLine("  ,TYMK.[12gatu_mikomi_kensuu] ")        '12月_見込件数
                    .AppendLine("  ,TYMK.[12gatu_mikomi_kingaku] ")       '12月_見込金額
                    .AppendLine("  ,TYMK.[12gatu_mikomi_arari] ")         '12月_見込粗利
                    .AppendLine("  ,TYMK.[1gatu_mikomi_kensuu] ")         '1月_見込件数
                    .AppendLine("  ,TYMK.[1gatu_mikomi_kingaku] ")        '1月_見込金額
                    .AppendLine("  ,TYMK.[1gatu_mikomi_arari] ")          '1月_見込粗利
                    .AppendLine("  ,TYMK.[2gatu_mikomi_kensuu] ")         '2月_見込件数
                    .AppendLine("  ,TYMK.[2gatu_mikomi_kingaku] ")        '2月_見込金額
                    .AppendLine("  ,TYMK.[2gatu_mikomi_arari] ")          '2月_見込粗利
                    .AppendLine("  ,TYMK.[3gatu_mikomi_kensuu] ")         '3月_見込件数
                    .AppendLine("  ,TYMK.[3gatu_mikomi_kingaku] ")        '3月_見込金額
                    .AppendLine("  ,TYMK.[3gatu_mikomi_arari] ")          '3月_見込粗利


                    .AppendLine("  ,ZTJK.[4gatu_jisseki_kensuu] AS Z_4gatu_jisseki_kensuu ")     '前年4月_実績件数
                    .AppendLine("  ,ZTJK.[4gatu_jisseki_kingaku]  AS Z_4gatu_jisseki_kingaku ")    '前年4月_実績金額
                    .AppendLine("  ,ZTJK.[4gatu_jisseki_arari]  AS Z_4gatu_jisseki_arari ")      '前年4月_実績粗利
                    .AppendLine("  ,ZTJK.[5gatu_jisseki_kensuu]  AS Z_5gatu_jisseki_kensuu ")     '前年5月_実績件数
                    .AppendLine("  ,ZTJK.[5gatu_jisseki_kingaku] AS Z_5gatu_jisseki_kingaku ")    '前年5月_実績金額
                    .AppendLine("  ,ZTJK.[5gatu_jisseki_arari] Z_5gatu_jisseki_arari")      '前年5月_実績粗利
                    .AppendLine("  ,ZTJK.[6gatu_jisseki_kensuu] AS Z_6gatu_jisseki_kensuu  ")     '前年6月_実績件数
                    .AppendLine("  ,ZTJK.[6gatu_jisseki_kingaku] AS Z_6gatu_jisseki_kingaku  ")    '前年6月_実績金額
                    .AppendLine("  ,ZTJK.[6gatu_jisseki_arari] Z_6gatu_jisseki_arari")      '前年6月_実績粗利
                    .AppendLine("  ,ZTJK.[7gatu_jisseki_kensuu]  AS Z_7gatu_jisseki_kensuu ")     '前年7月_実績件数
                    .AppendLine("  ,ZTJK.[7gatu_jisseki_kingaku]  AS Z_7gatu_jisseki_kingaku ")    '前年7月_実績金額
                    .AppendLine("  ,ZTJK.[7gatu_jisseki_arari] Z_7gatu_jisseki_arari")      '前年7月_実績粗利
                    .AppendLine("  ,ZTJK.[8gatu_jisseki_kensuu]  AS Z_8gatu_jisseki_kensuu ")     '前年8月_実績件数
                    .AppendLine("  ,ZTJK.[8gatu_jisseki_kingaku]  AS Z_8gatu_jisseki_kingaku ")    '前年8月_実績金額
                    .AppendLine("  ,ZTJK.[8gatu_jisseki_arari] Z_8gatu_jisseki_arari")      '前年8月_実績粗利
                    .AppendLine("  ,ZTJK.[9gatu_jisseki_kensuu]  AS Z_9gatu_jisseki_kensuu ")     '前年9月_実績件数
                    .AppendLine("  ,ZTJK.[9gatu_jisseki_kingaku]  AS Z_9gatu_jisseki_kingaku ")    '前年9月_実績金額
                    .AppendLine("  ,ZTJK.[9gatu_jisseki_arari] Z_9gatu_jisseki_arari")      '前年9月_実績粗利
                    .AppendLine("  ,ZTJK.[10gatu_jisseki_kensuu]  AS Z_10gatu_jisseki_kensuu ")    '前年10月_実績件数
                    .AppendLine("  ,ZTJK.[10gatu_jisseki_kingaku]  AS Z_10gatu_jisseki_kingaku ")   '前年10月_実績金額
                    .AppendLine("  ,ZTJK.[10gatu_jisseki_arari] Z_10gatu_jisseki_arari")     '前年10月_実績粗利
                    .AppendLine("  ,ZTJK.[11gatu_jisseki_kensuu]  AS Z_11gatu_jisseki_kensuu ")    '前年11月_実績件数
                    .AppendLine("  ,ZTJK.[11gatu_jisseki_kingaku]  AS Z_11gatu_jisseki_kingaku ")   '前年11月_実績金額
                    .AppendLine("  ,ZTJK.[11gatu_jisseki_arari] Z_11gatu_jisseki_arari")     '前年11月_実績粗利
                    .AppendLine("  ,ZTJK.[12gatu_jisseki_kensuu]  AS Z_12gatu_jisseki_kensuu ")    '前年12月_実績件数
                    .AppendLine("  ,ZTJK.[12gatu_jisseki_kingaku]  AS Z_12gatu_jisseki_kingaku ")   '前年12月_実績金額
                    .AppendLine("  ,ZTJK.[12gatu_jisseki_arari] Z_12gatu_jisseki_arari")     '前年12月_実績粗利
                    .AppendLine("  ,ZTJK.[1gatu_jisseki_kensuu]  AS Z_1gatu_jisseki_kensuu ")     '前年1月_実績件数
                    .AppendLine("  ,ZTJK.[1gatu_jisseki_kingaku] AS Z_1gatu_jisseki_kingaku  ")    '前年1月_実績金額
                    .AppendLine("  ,ZTJK.[1gatu_jisseki_arari] Z_1gatu_jisseki_arari")      '前年1月_実績粗利
                    .AppendLine("  ,ZTJK.[2gatu_jisseki_kensuu]  AS Z_2gatu_jisseki_kensuu ")     '前年2月_実績件数
                    .AppendLine("  ,ZTJK.[2gatu_jisseki_kingaku] AS Z_2gatu_jisseki_kingaku  ")    '前年2月_実績金額
                    .AppendLine("  ,ZTJK.[2gatu_jisseki_arari] Z_2gatu_jisseki_arari")      '前年2月_実績粗利
                    .AppendLine("  ,ZTJK.[3gatu_jisseki_kensuu]  AS Z_3gatu_jisseki_kensuu ")     '前年3月_実績件数
                    .AppendLine("  ,ZTJK.[3gatu_jisseki_kingaku] AS Z_3gatu_jisseki_kingaku  ")    '前年3月_実績金額
                    .AppendLine("  ,ZTJK.[3gatu_jisseki_arari] Z_3gatu_jisseki_arari")      '前年3月_実績粗利

                    .AppendLine("  ,TKK.[4gatu_keisanyou_koj_hantei_ritu] ")      '4月_計算用_工事判定率
                    .AppendLine("  ,TKK.[4gatu_keisanyou_koj_jyuchuu_ritu] ")     '4月_計算用_工事受注率
                    .AppendLine("  ,TKK.[4gatu_keisanyou_tyoku_koj_ritu] ")       '4月_計算用_直工事率
                    .AppendLine("  ,TKK.[5gatu_keisanyou_koj_hantei_ritu] ")      '5月_計算用_工事判定率
                    .AppendLine("  ,TKK.[5gatu_keisanyou_koj_jyuchuu_ritu] ")     '5月_計算用_工事受注率
                    .AppendLine("  ,TKK.[5gatu_keisanyou_tyoku_koj_ritu] ")       '5月_計算用_直工事率
                    .AppendLine("  ,TKK.[6gatu_keisanyou_koj_hantei_ritu] ")      '6月_計算用_工事判定率
                    .AppendLine("  ,TKK.[6gatu_keisanyou_koj_jyuchuu_ritu] ")     '6月_計算用_工事受注率
                    .AppendLine("  ,TKK.[6gatu_keisanyou_tyoku_koj_ritu] ")       '6月_計算用_直工事率
                    .AppendLine("  ,TKK.[7gatu_keisanyou_koj_hantei_ritu] ")      '7月_計算用_工事判定率
                    .AppendLine("  ,TKK.[7gatu_keisanyou_koj_jyuchuu_ritu] ")     '7月_計算用_工事受注率
                    .AppendLine("  ,TKK.[7gatu_keisanyou_tyoku_koj_ritu] ")       '7月_計算用_直工事率
                    .AppendLine("  ,TKK.[8gatu_keisanyou_koj_hantei_ritu] ")      '8月_計算用_工事判定率
                    .AppendLine("  ,TKK.[8gatu_keisanyou_koj_jyuchuu_ritu] ")     '8月_計算用_工事受注率
                    .AppendLine("  ,TKK.[8gatu_keisanyou_tyoku_koj_ritu] ")       '8月_計算用_直工事率
                    .AppendLine("  ,TKK.[9gatu_keisanyou_koj_hantei_ritu] ")      '9月_計算用_工事判定率
                    .AppendLine("  ,TKK.[9gatu_keisanyou_koj_jyuchuu_ritu] ")     '9月_計算用_工事受注率
                    .AppendLine("  ,TKK.[9gatu_keisanyou_tyoku_koj_ritu] ")       '9月_計算用_直工事率
                    .AppendLine("  ,TKK.[10gatu_keisanyou_koj_hantei_ritu] ")     '10月_計算用_工事判定率
                    .AppendLine("  ,TKK.[10gatu_keisanyou_koj_jyuchuu_ritu] ")    '10月_計算用_工事受注率
                    .AppendLine("  ,TKK.[10gatu_keisanyou_tyoku_koj_ritu] ")      '10月_計算用_直工事率
                    .AppendLine("  ,TKK.[11gatu_keisanyou_koj_hantei_ritu] ")     '11月_計算用_工事判定率
                    .AppendLine("  ,TKK.[11gatu_keisanyou_koj_jyuchuu_ritu] ")    '11月_計算用_工事受注率
                    .AppendLine("  ,TKK.[11gatu_keisanyou_tyoku_koj_ritu] ")      '11月_計算用_直工事率
                    .AppendLine("  ,TKK.[12gatu_keisanyou_koj_hantei_ritu] ")     '12月_計算用_工事判定率
                    .AppendLine("  ,TKK.[12gatu_keisanyou_koj_jyuchuu_ritu] ")    '12月_計算用_工事受注率
                    .AppendLine("  ,TKK.[12gatu_keisanyou_tyoku_koj_ritu] ")      '12月_計算用_直工事率
                    .AppendLine("  ,TKK.[1gatu_keisanyou_koj_hantei_ritu] ")      '1月_計算用_工事判定率
                    .AppendLine("  ,TKK.[1gatu_keisanyou_koj_jyuchuu_ritu] ")     '1月_計算用_工事受注率
                    .AppendLine("  ,TKK.[1gatu_keisanyou_tyoku_koj_ritu] ")       '1月_計算用_直工事率
                    .AppendLine("  ,TKK.[2gatu_keisanyou_koj_hantei_ritu] ")      '2月_計算用_工事判定率
                    .AppendLine("  ,TKK.[2gatu_keisanyou_koj_jyuchuu_ritu] ")     '2月_計算用_工事受注率
                    .AppendLine("  ,TKK.[2gatu_keisanyou_tyoku_koj_ritu] ")       '2月_計算用_直工事率
                    .AppendLine("  ,TKK.[3gatu_keisanyou_koj_hantei_ritu] ")      '3月_計算用_工事判定率
                    .AppendLine("  ,TKK.[3gatu_keisanyou_koj_jyuchuu_ritu] ")     '3月_計算用_工事受注率
                    .AppendLine("  ,TKK.[3gatu_keisanyou_tyoku_koj_ritu] ")       '3月_計算用_直工事率
                    .AppendLine("  ,MKK.eigyou_kbn  ")
                    .AppendLine("  ,TKK.add_datetime  ")
                    '.AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   '見込_SDS選択時追加金額
                    '.AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4月_見込_SS_SDS入力
                    '.AppendLine("  ,NULL AS [5gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [6gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [7gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [8gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [9gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [10gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [11gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [12gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [1gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [2gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3月_見込_SS_SDS入力
                    '.AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ") '計画_SDS選択時追加金額
                    '.AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4月_計画_SS_SDS入力
                    '.AppendLine("  ,NULL AS [5gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [6gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [7gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [8gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [9gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [10gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [11gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [12gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [1gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [2gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3月_計画_SS_SDS入力

                    .AppendLine("  ,TKK.[4gatu_keisanyou_uri_heikin_tanka] ")       '4月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[4gatu_keisanyou_siire_heikin_tanka] ")     '4月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[5gatu_keisanyou_uri_heikin_tanka] ")       '5月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[5gatu_keisanyou_siire_heikin_tanka] ")     '5月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[6gatu_keisanyou_uri_heikin_tanka] ")       '6月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[6gatu_keisanyou_siire_heikin_tanka] ")     '6月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[7gatu_keisanyou_uri_heikin_tanka] ")       '7月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[7gatu_keisanyou_siire_heikin_tanka] ")     '7月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[8gatu_keisanyou_uri_heikin_tanka] ")       '8月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[8gatu_keisanyou_siire_heikin_tanka] ")     '8月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[9gatu_keisanyou_uri_heikin_tanka] ")       '9月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[9gatu_keisanyou_siire_heikin_tanka] ")     '9月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[10gatu_keisanyou_uri_heikin_tanka] ")      '10月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[10gatu_keisanyou_siire_heikin_tanka] ")    '10月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[11gatu_keisanyou_uri_heikin_tanka] ")      '11月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[11gatu_keisanyou_siire_heikin_tanka] ")    '11月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[12gatu_keisanyou_uri_heikin_tanka] ")      '12月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[12gatu_keisanyou_siire_heikin_tanka] ")    '12月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[1gatu_keisanyou_uri_heikin_tanka] ")       '1月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[1gatu_keisanyou_siire_heikin_tanka] ")     '1月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[2gatu_keisanyou_uri_heikin_tanka] ")       '2月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[2gatu_keisanyou_siire_heikin_tanka] ")     '2月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[3gatu_keisanyou_uri_heikin_tanka] ")       '3月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[3gatu_keisanyou_siire_heikin_tanka] ")     '3月_計算用__仕入平均単価

                Case Utilities.KeikakuKanriRecord.selectKbn.goukei, Utilities.KeikakuKanriRecord.selectKbn.syoukei
                    .AppendLine(" SELECT  ")
                    .AppendLine("  NULL AS kameiten_mei")      '加盟店名
                    .AppendLine("  ,NULL AS kameiten_cd ")             '加盟店コード
                    If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.syoukei Then
                        .AppendLine("  ,MKM.meisyou  ")                 '営業区分
                    Else
                        .AppendLine("  ,'全体（FC除外）' AS  meisyou ")                 '営業区分
                    End If

                    .AppendLine("  ,NULL AS eigyou_tantousya_mei ")     '営業担当者名
                    .AppendLine("  ,NULL AS keikaku_nenkan_tousuu")     '年間棟数

                    .AppendLine("  ,NULL AS [keikakuyou_nenkan_tousuu] ")   '計画用_年間棟数

                    .AppendLine("  ,NULL AS gyoutai ")                  '業態
                    .AppendLine("  ,NULL AS uri_hiritu ")               '売上比率

                    .AppendLine("  ,NULL AS [sds_kaisi_nengetu] ")      'SDS開始年月

                    .AppendLine("  ,NULL AS koj_hantei_ritu ")          '工事判定率
                    .AppendLine("  ,NULL AS koj_jyuchuu_ritu ")         '工事受注率
                    .AppendLine("  ,NULL AS tyoku_koj_ritu ")           '直工事率
                    .AppendLine("  ,MKM2.meisyou AS meisyou2  ")        '名称種別
                    .AppendLine("  ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei  ")   '商品名
                    .AppendLine("  ,NULL AS [zennen_heikin_tanka]  ")   '前年_平均単価

                    .AppendLine(" ,sum(TJK.[4gatu_jisseki_kensuu] ) AS [4gatu_jisseki_kensuu] ") '4月_実績件数
                    .AppendLine(" ,sum(TJK.[4gatu_jisseki_kingaku] ) AS [4gatu_jisseki_kingaku] ") '4月_実績金額
                    .AppendLine(" ,sum(TJK.[4gatu_jisseki_arari] ) AS [4gatu_jisseki_arari] ") '4月_実績粗利
                    .AppendLine(" ,sum(TJK.[5gatu_jisseki_kensuu] ) AS [5gatu_jisseki_kensuu] ") '5月_実績件数
                    .AppendLine(" ,sum(TJK.[5gatu_jisseki_kingaku] ) AS [5gatu_jisseki_kingaku] ") '5月_実績金額
                    .AppendLine(" ,sum(TJK.[5gatu_jisseki_arari] ) AS [5gatu_jisseki_arari] ") '5月_実績粗利
                    .AppendLine(" ,sum(TJK.[6gatu_jisseki_kensuu] ) AS [6gatu_jisseki_kensuu] ") '6月_実績件数
                    .AppendLine(" ,sum(TJK.[6gatu_jisseki_kingaku] ) AS [6gatu_jisseki_kingaku] ") '6月_実績金額
                    .AppendLine(" ,sum(TJK.[6gatu_jisseki_arari] ) AS [6gatu_jisseki_arari] ") '6月_実績粗利
                    .AppendLine(" ,sum(TJK.[7gatu_jisseki_kensuu] ) AS [7gatu_jisseki_kensuu] ") '7月_実績件数
                    .AppendLine(" ,sum(TJK.[7gatu_jisseki_kingaku] ) AS [7gatu_jisseki_kingaku] ") '7月_実績金額
                    .AppendLine(" ,sum(TJK.[7gatu_jisseki_arari] ) AS [7gatu_jisseki_arari] ") '7月_実績粗利
                    .AppendLine(" ,sum(TJK.[8gatu_jisseki_kensuu] ) AS [8gatu_jisseki_kensuu] ") '8月_実績件数
                    .AppendLine(" ,sum(TJK.[8gatu_jisseki_kingaku] ) AS [8gatu_jisseki_kingaku] ") '8月_実績金額
                    .AppendLine(" ,sum(TJK.[8gatu_jisseki_arari] ) AS [8gatu_jisseki_arari] ") '8月_実績粗利
                    .AppendLine(" ,sum(TJK.[9gatu_jisseki_kensuu] ) AS [9gatu_jisseki_kensuu] ") '9月_実績件数
                    .AppendLine(" ,sum(TJK.[9gatu_jisseki_kingaku] ) AS [9gatu_jisseki_kingaku] ") '9月_実績金額
                    .AppendLine(" ,sum(TJK.[9gatu_jisseki_arari] ) AS [9gatu_jisseki_arari] ") '9月_実績粗利
                    .AppendLine(" ,sum(TJK.[10gatu_jisseki_kensuu] ) AS [10gatu_jisseki_kensuu] ") '10月_実績件数
                    .AppendLine(" ,sum(TJK.[10gatu_jisseki_kingaku] ) AS [10gatu_jisseki_kingaku] ") '10月_実績金額
                    .AppendLine(" ,sum(TJK.[10gatu_jisseki_arari] ) AS [10gatu_jisseki_arari] ") '10月_実績粗利
                    .AppendLine(" ,sum(TJK.[11gatu_jisseki_kensuu] ) AS [11gatu_jisseki_kensuu] ") '11月_実績件数
                    .AppendLine(" ,sum(TJK.[11gatu_jisseki_kingaku] ) AS [11gatu_jisseki_kingaku] ") '11月_実績金額
                    .AppendLine(" ,sum(TJK.[11gatu_jisseki_arari] ) AS [11gatu_jisseki_arari] ") '11月_実績粗利
                    .AppendLine(" ,sum(TJK.[12gatu_jisseki_kensuu] ) AS [12gatu_jisseki_kensuu] ") '12月_実績件数
                    .AppendLine(" ,sum(TJK.[12gatu_jisseki_kingaku] ) AS [12gatu_jisseki_kingaku] ") '12月_実績金額
                    .AppendLine(" ,sum(TJK.[12gatu_jisseki_arari] ) AS [12gatu_jisseki_arari] ") '12月_実績粗利
                    .AppendLine(" ,sum(TJK.[1gatu_jisseki_kensuu] ) AS [1gatu_jisseki_kensuu] ") '1月_実績件数
                    .AppendLine(" ,sum(TJK.[1gatu_jisseki_kingaku] ) AS [1gatu_jisseki_kingaku] ") '1月_実績金額
                    .AppendLine(" ,sum(TJK.[1gatu_jisseki_arari] ) AS [1gatu_jisseki_arari] ") '1月_実績粗利
                    .AppendLine(" ,sum(TJK.[2gatu_jisseki_kensuu] ) AS [2gatu_jisseki_kensuu] ") '2月_実績件数
                    .AppendLine(" ,sum(TJK.[2gatu_jisseki_kingaku] ) AS [2gatu_jisseki_kingaku] ") '2月_実績金額
                    .AppendLine(" ,sum(TJK.[2gatu_jisseki_arari] ) AS [2gatu_jisseki_arari] ") '2月_実績粗利
                    .AppendLine(" ,sum(TJK.[3gatu_jisseki_kensuu] ) AS [3gatu_jisseki_kensuu] ") '3月_実績件数
                    .AppendLine(" ,sum(TJK.[3gatu_jisseki_kingaku] ) AS [3gatu_jisseki_kingaku] ") '3月_実績金額
                    .AppendLine(" ,sum(TJK.[3gatu_jisseki_arari] ) AS [3gatu_jisseki_arari] ") '3月_実績粗利

                    .AppendLine(" ,sum(TKK.[4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu] ") '4月_計画件数
                    .AppendLine(" ,sum(TKK.[4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku] ") '4月_計画金額
                    .AppendLine(" ,sum(TKK.[4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari] ") '4月_計画粗利
                    .AppendLine(" ,sum(TKK.[5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu] ") '5月_計画件数
                    .AppendLine(" ,sum(TKK.[5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku] ") '5月_計画金額
                    .AppendLine(" ,sum(TKK.[5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari] ") '5月_計画粗利
                    .AppendLine(" ,sum(TKK.[6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu] ") '6月_計画件数
                    .AppendLine(" ,sum(TKK.[6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku] ") '6月_計画金額
                    .AppendLine(" ,sum(TKK.[6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari] ") '6月_計画粗利
                    .AppendLine(" ,sum(TKK.[7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu] ") '7月_計画件数
                    .AppendLine(" ,sum(TKK.[7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku] ") '7月_計画金額
                    .AppendLine(" ,sum(TKK.[7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari] ") '7月_計画粗利
                    .AppendLine(" ,sum(TKK.[8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu] ") '8月_計画件数
                    .AppendLine(" ,sum(TKK.[8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ") '8月_計画金額
                    .AppendLine(" ,sum(TKK.[8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari] ") '8月_計画粗利
                    .AppendLine(" ,sum(TKK.[9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu] ") '9月_計画件数
                    .AppendLine(" ,sum(TKK.[9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku] ") '9月_計画金額
                    .AppendLine(" ,sum(TKK.[9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari] ") '9月_計画粗利
                    .AppendLine(" ,sum(TKK.[10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu] ") '10月_計画件数
                    .AppendLine(" ,sum(TKK.[10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku] ") '10月_計画金額
                    .AppendLine(" ,sum(TKK.[10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari] ") '10月_計画粗利
                    .AppendLine(" ,sum(TKK.[11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu] ") '11月_計画件数
                    .AppendLine(" ,sum(TKK.[11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ") '11月_計画金額
                    .AppendLine(" ,sum(TKK.[11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari] ") '11月_計画粗利
                    .AppendLine(" ,sum(TKK.[12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu] ") '12月_計画件数
                    .AppendLine(" ,sum(TKK.[12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku] ") '12月_計画金額
                    .AppendLine(" ,sum(TKK.[12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari] ") '12月_計画粗利
                    .AppendLine(" ,sum(TKK.[1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu] ") '1月_計画件数
                    .AppendLine(" ,sum(TKK.[1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku] ") '1月_計画金額
                    .AppendLine(" ,sum(TKK.[1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari] ") '1月_計画粗利
                    .AppendLine(" ,sum(TKK.[2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu] ") '2月_計画件数
                    .AppendLine(" ,sum(TKK.[2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku] ") '2月_計画金額
                    .AppendLine(" ,sum(TKK.[2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari] ") '2月_計画粗利
                    .AppendLine(" ,sum(TKK.[3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu] ") '3月_計画件数
                    .AppendLine(" ,sum(TKK.[3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ") '3月_計画金額
                    .AppendLine(" ,sum(TKK.[3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari] ") '3月_計画粗利

                    .AppendLine(" ,sum(TYMK.[4gatu_mikomi_kensuu] ) AS [4gatu_mikomi_kensuu] ") '4月_見込件数
                    .AppendLine(" ,sum(TYMK.[4gatu_mikomi_kingaku] ) AS [4gatu_mikomi_kingaku] ") '4月_見込金額
                    .AppendLine(" ,sum(TYMK.[4gatu_mikomi_arari] ) AS [4gatu_mikomi_arari] ") '4月_見込粗利
                    .AppendLine(" ,sum(TYMK.[5gatu_mikomi_kensuu] ) AS [5gatu_mikomi_kensuu] ") '5月_見込件数
                    .AppendLine(" ,sum(TYMK.[5gatu_mikomi_kingaku] ) AS [5gatu_mikomi_kingaku] ") '5月_見込金額
                    .AppendLine(" ,sum(TYMK.[5gatu_mikomi_arari] ) AS [5gatu_mikomi_arari] ") '5月_見込粗利
                    .AppendLine(" ,sum(TYMK.[6gatu_mikomi_kensuu] ) AS [6gatu_mikomi_kensuu] ") '6月_見込件数
                    .AppendLine(" ,sum(TYMK.[6gatu_mikomi_kingaku] ) AS [6gatu_mikomi_kingaku] ") '6月_見込金額
                    .AppendLine(" ,sum(TYMK.[6gatu_mikomi_arari] ) AS [6gatu_mikomi_arari] ") '6月_見込粗利
                    .AppendLine(" ,sum(TYMK.[7gatu_mikomi_kensuu] ) AS [7gatu_mikomi_kensuu] ") '7月_見込件数
                    .AppendLine(" ,sum(TYMK.[7gatu_mikomi_kingaku] ) AS [7gatu_mikomi_kingaku] ") '7月_見込金額
                    .AppendLine(" ,sum(TYMK.[7gatu_mikomi_arari] ) AS [7gatu_mikomi_arari] ") '7月_見込粗利
                    .AppendLine(" ,sum(TYMK.[8gatu_mikomi_kensuu] ) AS [8gatu_mikomi_kensuu] ") '8月_見込件数
                    .AppendLine(" ,sum(TYMK.[8gatu_mikomi_kingaku] ) AS [8gatu_mikomi_kingaku] ") '8月_見込金額
                    .AppendLine(" ,sum(TYMK.[8gatu_mikomi_arari] ) AS [8gatu_mikomi_arari] ") '8月_見込粗利
                    .AppendLine(" ,sum(TYMK.[9gatu_mikomi_kensuu] ) AS [9gatu_mikomi_kensuu] ") '9月_見込件数
                    .AppendLine(" ,sum(TYMK.[9gatu_mikomi_kingaku] ) AS [9gatu_mikomi_kingaku] ") '9月_見込金額
                    .AppendLine(" ,sum(TYMK.[9gatu_mikomi_arari] ) AS [9gatu_mikomi_arari] ") '9月_見込粗利
                    .AppendLine(" ,sum(TYMK.[10gatu_mikomi_kensuu] ) AS [10gatu_mikomi_kensuu] ") '10月_見込件数
                    .AppendLine(" ,sum(TYMK.[10gatu_mikomi_kingaku] ) AS [10gatu_mikomi_kingaku] ") '10月_見込金額
                    .AppendLine(" ,sum(TYMK.[10gatu_mikomi_arari] ) AS [10gatu_mikomi_arari] ") '10月_見込粗利
                    .AppendLine(" ,sum(TYMK.[11gatu_mikomi_kensuu] ) AS [11gatu_mikomi_kensuu] ") '11月_見込件数
                    .AppendLine(" ,sum(TYMK.[11gatu_mikomi_kingaku] ) AS [11gatu_mikomi_kingaku] ") '11月_見込金額
                    .AppendLine(" ,sum(TYMK.[11gatu_mikomi_arari] ) AS [11gatu_mikomi_arari] ") '11月_見込粗利
                    .AppendLine(" ,sum(TYMK.[12gatu_mikomi_kensuu] ) AS [12gatu_mikomi_kensuu] ") '12月_見込件数
                    .AppendLine(" ,sum(TYMK.[12gatu_mikomi_kingaku] ) AS [12gatu_mikomi_kingaku] ") '12月_見込金額
                    .AppendLine(" ,sum(TYMK.[12gatu_mikomi_arari] ) AS [12gatu_mikomi_arari] ") '12月_見込粗利
                    .AppendLine(" ,sum(TYMK.[1gatu_mikomi_kensuu] ) AS [1gatu_mikomi_kensuu] ") '1月_見込件数
                    .AppendLine(" ,sum(TYMK.[1gatu_mikomi_kingaku] ) AS [1gatu_mikomi_kingaku] ") '1月_見込金額
                    .AppendLine(" ,sum(TYMK.[1gatu_mikomi_arari] ) AS [1gatu_mikomi_arari] ") '1月_見込粗利
                    .AppendLine(" ,sum(TYMK.[2gatu_mikomi_kensuu] ) AS [2gatu_mikomi_kensuu] ") '2月_見込件数
                    .AppendLine(" ,sum(TYMK.[2gatu_mikomi_kingaku] ) AS [2gatu_mikomi_kingaku] ") '2月_見込金額
                    .AppendLine(" ,sum(TYMK.[2gatu_mikomi_arari] ) AS [2gatu_mikomi_arari] ") '2月_見込粗利
                    .AppendLine(" ,sum(TYMK.[3gatu_mikomi_kensuu] ) AS [3gatu_mikomi_kensuu] ") '3月_見込件数
                    .AppendLine(" ,sum(TYMK.[3gatu_mikomi_kingaku] ) AS [3gatu_mikomi_kingaku] ") '3月_見込金額
                    .AppendLine(" ,sum(TYMK.[3gatu_mikomi_arari] ) AS [3gatu_mikomi_arari] ") '3月_見込粗利


                    .AppendLine(" ,sum(ZTJK.[4gatu_jisseki_kensuu] ) AS [Z_4gatu_jisseki_kensuu] ") '前年4月_実績件数
                    .AppendLine(" ,sum(ZTJK.[4gatu_jisseki_kingaku] ) AS [Z_4gatu_jisseki_kingaku] ") '前年4月_実績金額
                    .AppendLine(" ,sum(ZTJK.[4gatu_jisseki_arari] ) AS [Z_4gatu_jisseki_arari] ") '前年4月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[5gatu_jisseki_kensuu] ) AS [Z_5gatu_jisseki_kensuu] ") '前年5月_実績件数
                    .AppendLine(" ,sum(ZTJK.[5gatu_jisseki_kingaku] ) AS [Z_5gatu_jisseki_kingaku] ") '前年5月_実績金額
                    .AppendLine(" ,sum(ZTJK.[5gatu_jisseki_arari] ) AS [Z_5gatu_jisseki_arari] ") '前年5月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[6gatu_jisseki_kensuu] ) AS [Z_6gatu_jisseki_kensuu] ") '前年6月_実績件数
                    .AppendLine(" ,sum(ZTJK.[6gatu_jisseki_kingaku] ) AS [Z_6gatu_jisseki_kingaku] ") '前年6月_実績金額
                    .AppendLine(" ,sum(ZTJK.[6gatu_jisseki_arari] ) AS [Z_6gatu_jisseki_arari] ") '前年6月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[7gatu_jisseki_kensuu] ) AS [Z_7gatu_jisseki_kensuu] ") '前年7月_実績件数
                    .AppendLine(" ,sum(ZTJK.[7gatu_jisseki_kingaku] ) AS [Z_7gatu_jisseki_kingaku] ") '前年7月_実績金額
                    .AppendLine(" ,sum(ZTJK.[7gatu_jisseki_arari] ) AS [Z_7gatu_jisseki_arari] ") '前年7月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[8gatu_jisseki_kensuu] ) AS [Z_8gatu_jisseki_kensuu] ") '前年8月_実績件数
                    .AppendLine(" ,sum(ZTJK.[8gatu_jisseki_kingaku] ) AS [Z_8gatu_jisseki_kingaku] ") '前年8月_実績金額
                    .AppendLine(" ,sum(ZTJK.[8gatu_jisseki_arari] ) AS [Z_8gatu_jisseki_arari] ") '前年8月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[9gatu_jisseki_kensuu] ) AS [Z_9gatu_jisseki_kensuu] ") '前年9月_実績件数
                    .AppendLine(" ,sum(ZTJK.[9gatu_jisseki_kingaku] ) AS [Z_9gatu_jisseki_kingaku] ") '前年9月_実績金額
                    .AppendLine(" ,sum(ZTJK.[9gatu_jisseki_arari] ) AS [Z_9gatu_jisseki_arari] ") '前年9月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[10gatu_jisseki_kensuu] ) AS [Z_10gatu_jisseki_kensuu] ") '前年10月_実績件数
                    .AppendLine(" ,sum(ZTJK.[10gatu_jisseki_kingaku] ) AS [Z_10gatu_jisseki_kingaku] ") '前年10月_実績金額
                    .AppendLine(" ,sum(ZTJK.[10gatu_jisseki_arari] ) AS [Z_10gatu_jisseki_arari] ") '前年10月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[11gatu_jisseki_kensuu] ) AS [Z_11gatu_jisseki_kensuu] ") '前年11月_実績件数
                    .AppendLine(" ,sum(ZTJK.[11gatu_jisseki_kingaku] ) AS [Z_11gatu_jisseki_kingaku] ") '前年11月_実績金額
                    .AppendLine(" ,sum(ZTJK.[11gatu_jisseki_arari] ) AS [Z_11gatu_jisseki_arari] ") '前年11月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[12gatu_jisseki_kensuu] ) AS [Z_12gatu_jisseki_kensuu] ") '前年12月_実績件数
                    .AppendLine(" ,sum(ZTJK.[12gatu_jisseki_kingaku] ) AS [Z_12gatu_jisseki_kingaku] ") '前年12月_実績金額
                    .AppendLine(" ,sum(ZTJK.[12gatu_jisseki_arari] ) AS [Z_12gatu_jisseki_arari] ") '前年12月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[1gatu_jisseki_kensuu] ) AS [Z_1gatu_jisseki_kensuu] ") '前年1月_実績件数
                    .AppendLine(" ,sum(ZTJK.[1gatu_jisseki_kingaku] ) AS [Z_1gatu_jisseki_kingaku] ") '前年1月_実績金額
                    .AppendLine(" ,sum(ZTJK.[1gatu_jisseki_arari] ) AS [Z_1gatu_jisseki_arari] ") '前年1月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[2gatu_jisseki_kensuu] ) AS [Z_2gatu_jisseki_kensuu] ") '前年2月_実績件数
                    .AppendLine(" ,sum(ZTJK.[2gatu_jisseki_kingaku] ) AS [Z_2gatu_jisseki_kingaku] ") '前年2月_実績金額
                    .AppendLine(" ,sum(ZTJK.[2gatu_jisseki_arari] ) AS [Z_2gatu_jisseki_arari] ") '前年2月_実績粗利
                    .AppendLine(" ,sum(ZTJK.[3gatu_jisseki_kensuu] ) AS [Z_3gatu_jisseki_kensuu] ") '前年3月_実績件数
                    .AppendLine(" ,sum(ZTJK.[3gatu_jisseki_kingaku] ) AS [Z_3gatu_jisseki_kingaku] ") '前年3月_実績金額
                    .AppendLine(" ,sum(ZTJK.[3gatu_jisseki_arari] ) AS [Z_3gatu_jisseki_arari] ") '前年3月_実績粗利

                    .AppendLine(" ,sum(TKK.[4gatu_keisanyou_koj_hantei_ritu] ) AS [4gatu_keisanyou_koj_hantei_ritu] ") '4月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[4gatu_keisanyou_koj_jyuchuu_ritu] ) AS [4gatu_keisanyou_koj_jyuchuu_ritu] ") '4月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[4gatu_keisanyou_tyoku_koj_ritu] ) AS [4gatu_keisanyou_tyoku_koj_ritu] ") '4月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[5gatu_keisanyou_koj_hantei_ritu] ) AS [5gatu_keisanyou_koj_hantei_ritu] ") '5月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[5gatu_keisanyou_koj_jyuchuu_ritu] ) AS [5gatu_keisanyou_koj_jyuchuu_ritu] ") '5月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[5gatu_keisanyou_tyoku_koj_ritu] ) AS [5gatu_keisanyou_tyoku_koj_ritu] ") '5月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[6gatu_keisanyou_koj_hantei_ritu] ) AS [6gatu_keisanyou_koj_hantei_ritu] ") '6月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[6gatu_keisanyou_koj_jyuchuu_ritu] ) AS [6gatu_keisanyou_koj_jyuchuu_ritu] ") '6月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[6gatu_keisanyou_tyoku_koj_ritu] ) AS [6gatu_keisanyou_tyoku_koj_ritu] ") '6月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[7gatu_keisanyou_koj_hantei_ritu] ) AS [7gatu_keisanyou_koj_hantei_ritu] ") '7月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[7gatu_keisanyou_koj_jyuchuu_ritu] ) AS [7gatu_keisanyou_koj_jyuchuu_ritu] ") '7月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[7gatu_keisanyou_tyoku_koj_ritu] ) AS [7gatu_keisanyou_tyoku_koj_ritu] ") '7月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[8gatu_keisanyou_koj_hantei_ritu] ) AS [8gatu_keisanyou_koj_hantei_ritu] ") '8月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[8gatu_keisanyou_koj_jyuchuu_ritu] ) AS [8gatu_keisanyou_koj_jyuchuu_ritu] ") '8月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[8gatu_keisanyou_tyoku_koj_ritu] ) AS [8gatu_keisanyou_tyoku_koj_ritu] ") '8月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[9gatu_keisanyou_koj_hantei_ritu] ) AS [9gatu_keisanyou_koj_hantei_ritu] ") '9月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[9gatu_keisanyou_koj_jyuchuu_ritu] ) AS [9gatu_keisanyou_koj_jyuchuu_ritu] ") '9月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[9gatu_keisanyou_tyoku_koj_ritu] ) AS [9gatu_keisanyou_tyoku_koj_ritu] ") '9月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[10gatu_keisanyou_koj_hantei_ritu] ) AS [10gatu_keisanyou_koj_hantei_ritu] ") '10月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[10gatu_keisanyou_koj_jyuchuu_ritu] ) AS [10gatu_keisanyou_koj_jyuchuu_ritu] ") '10月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[10gatu_keisanyou_tyoku_koj_ritu] ) AS [10gatu_keisanyou_tyoku_koj_ritu] ") '10月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[11gatu_keisanyou_koj_hantei_ritu] ) AS [11gatu_keisanyou_koj_hantei_ritu] ") '11月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[11gatu_keisanyou_koj_jyuchuu_ritu] ) AS [11gatu_keisanyou_koj_jyuchuu_ritu] ") '11月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[11gatu_keisanyou_tyoku_koj_ritu] ) AS [11gatu_keisanyou_tyoku_koj_ritu] ") '11月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[12gatu_keisanyou_koj_hantei_ritu] ) AS [12gatu_keisanyou_koj_hantei_ritu] ") '12月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[12gatu_keisanyou_koj_jyuchuu_ritu] ) AS [12gatu_keisanyou_koj_jyuchuu_ritu] ") '12月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[12gatu_keisanyou_tyoku_koj_ritu] ) AS [12gatu_keisanyou_tyoku_koj_ritu] ") '12月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[1gatu_keisanyou_koj_hantei_ritu] ) AS [1gatu_keisanyou_koj_hantei_ritu] ") '1月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[1gatu_keisanyou_koj_jyuchuu_ritu] ) AS [1gatu_keisanyou_koj_jyuchuu_ritu] ") '1月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[1gatu_keisanyou_tyoku_koj_ritu] ) AS [1gatu_keisanyou_tyoku_koj_ritu] ") '1月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[2gatu_keisanyou_koj_hantei_ritu] ) AS [2gatu_keisanyou_koj_hantei_ritu] ") '2月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[2gatu_keisanyou_koj_jyuchuu_ritu] ) AS [2gatu_keisanyou_koj_jyuchuu_ritu] ") '2月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[2gatu_keisanyou_tyoku_koj_ritu] ) AS [2gatu_keisanyou_tyoku_koj_ritu] ") '2月_計算用_直工事率
                    .AppendLine(" ,sum(TKK.[3gatu_keisanyou_koj_hantei_ritu] ) AS [3gatu_keisanyou_koj_hantei_ritu] ") '3月_計算用_工事判定率
                    .AppendLine(" ,sum(TKK.[3gatu_keisanyou_koj_jyuchuu_ritu] ) AS [3gatu_keisanyou_koj_jyuchuu_ritu] ") '3月_計算用_工事受注率
                    .AppendLine(" ,sum(TKK.[3gatu_keisanyou_tyoku_koj_ritu] ) AS [3gatu_keisanyou_tyoku_koj_ritu] ") '3月_計算用_直工事率
                    .AppendLine("  ,NULL  AS eigyou_kbn ")
                    .AppendLine("  ,NULL AS add_datetime ")
                    '.AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   '見込_SDS選択時追加金額
                    '.AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4月_見込_SS_SDS入力
                    '.AppendLine("  ,NULL AS [5gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [6gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [7gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [8gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [9gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [10gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [11gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [12gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [1gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [2gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3月_見込_SS_SDS入力
                    '.AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ") '計画_SDS選択時追加金額
                    '.AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4月_計画_SS_SDS入力
                    '.AppendLine("  ,NULL AS [5gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [6gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [7gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [8gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [9gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [10gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [11gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [12gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [1gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [2gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3月_計画_SS_SDS入力

                    .AppendLine("  ,NULL AS [4gatu_keisanyou_uri_heikin_tanka] ")       '4月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [4gatu_keisanyou_siire_heikin_tanka] ")     '4月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [5gatu_keisanyou_uri_heikin_tanka] ")       '5月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [5gatu_keisanyou_siire_heikin_tanka] ")     '5月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [6gatu_keisanyou_uri_heikin_tanka] ")       '6月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [6gatu_keisanyou_siire_heikin_tanka] ")     '6月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [7gatu_keisanyou_uri_heikin_tanka] ")       '7月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [7gatu_keisanyou_siire_heikin_tanka] ")     '7月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [8gatu_keisanyou_uri_heikin_tanka] ")       '8月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [8gatu_keisanyou_siire_heikin_tanka] ")     '8月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [9gatu_keisanyou_uri_heikin_tanka] ")       '9月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [9gatu_keisanyou_siire_heikin_tanka] ")     '9月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [10gatu_keisanyou_uri_heikin_tanka] ")      '10月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [10gatu_keisanyou_siire_heikin_tanka] ")    '10月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [11gatu_keisanyou_uri_heikin_tanka] ")      '11月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [11gatu_keisanyou_siire_heikin_tanka] ")    '11月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [12gatu_keisanyou_uri_heikin_tanka] ")      '12月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [12gatu_keisanyou_siire_heikin_tanka] ")    '12月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [1gatu_keisanyou_uri_heikin_tanka] ")       '1月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [1gatu_keisanyou_siire_heikin_tanka] ")     '1月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [2gatu_keisanyou_uri_heikin_tanka] ")       '2月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [2gatu_keisanyou_siire_heikin_tanka] ")     '2月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [3gatu_keisanyou_uri_heikin_tanka] ")       '3月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [3gatu_keisanyou_siire_heikin_tanka] ")     '3月_計算用__仕入平均単価

                Case Utilities.KeikakuKanriRecord.selectKbn.FC
                    .AppendLine(" SELECT  ")
                    .AppendLine("  NULL AS kameiten_mei")               '加盟店名
                    .AppendLine("  ,NULL AS kameiten_cd ")              '加盟店コード
                    .AppendLine("  ,'FC' AS  meisyou ")                 '営業区分
                    .AppendLine("  ,NULL AS eigyou_tantousya_mei ")     '営業担当者名
                    .AppendLine("  ,NULL AS keikaku_nenkan_tousuu")     '年間棟数

                    .AppendLine("  ,NULL AS [keikakuyou_nenkan_tousuu] ")   '計画用_年間棟数

                    .AppendLine("  ,NULL AS gyoutai ")                  '業態
                    .AppendLine("  ,NULL AS uri_hiritu ")               '売上比率

                    .AppendLine("  ,NULL AS [sds_kaisi_nengetu] ")      'SDS開始年月

                    .AppendLine("  ,NULL AS koj_hantei_ritu ")          '工事判定率
                    .AppendLine("  ,NULL AS koj_jyuchuu_ritu ")         '工事受注率
                    .AppendLine("  ,NULL AS tyoku_koj_ritu ")           '直工事率
                    .AppendLine("  ,MKM2.meisyou AS meisyou2  ")        '名称種別
                    .AppendLine("  ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei  ")            '商品名
                    .AppendLine("  ,TJK.zennen_heikin_tanka AS [zennen_heikin_tanka]  ")     '前年_平均単価

                    .AppendLine("  ,TJK.[4gatu_jisseki_kensuu] ")     '4月_実績件数
                    .AppendLine("  ,TJK.[4gatu_jisseki_kingaku]")    '4月_実績金額
                    .AppendLine("  ,TJK.[4gatu_jisseki_arari] ")      '4月_実績粗利
                    .AppendLine("  ,TJK.[5gatu_jisseki_kensuu] ")     '5月_実績件数
                    .AppendLine("  ,TJK.[5gatu_jisseki_kingaku] ")    '5月_実績金額
                    .AppendLine("  ,TJK.[5gatu_jisseki_arari] ")      '5月_実績粗利
                    .AppendLine("  ,TJK.[6gatu_jisseki_kensuu] ")     '6月_実績件数
                    .AppendLine("  ,TJK.[6gatu_jisseki_kingaku] ")    '6月_実績金額
                    .AppendLine("  ,TJK.[6gatu_jisseki_arari] ")      '6月_実績粗利
                    .AppendLine("  ,TJK.[7gatu_jisseki_kensuu] ")     '7月_実績件数
                    .AppendLine("  ,TJK.[7gatu_jisseki_kingaku]")    '7月_実績金額
                    .AppendLine("  ,TJK.[7gatu_jisseki_arari] ")      '7月_実績粗利
                    .AppendLine("  ,TJK.[8gatu_jisseki_kensuu] ")     '8月_実績件数
                    .AppendLine("  ,TJK.[8gatu_jisseki_kingaku] ")    '8月_実績金額
                    .AppendLine("  ,TJK.[8gatu_jisseki_arari] ")      '8月_実績粗利
                    .AppendLine("  ,TJK.[9gatu_jisseki_kensuu] ")     '9月_実績件数
                    .AppendLine("  ,TJK.[9gatu_jisseki_kingaku] ")    '9月_実績金額
                    .AppendLine("  ,TJK.[9gatu_jisseki_arari] ")      '9月_実績粗利
                    .AppendLine("  ,TJK.[10gatu_jisseki_kensuu] ")    '10月_実績件数
                    .AppendLine("  ,TJK.[10gatu_jisseki_kingaku] ")   '10月_実績金額
                    .AppendLine("  ,TJK.[10gatu_jisseki_arari] ")     '10月_実績粗利
                    .AppendLine("  ,TJK.[11gatu_jisseki_kensuu] ")    '11月_実績件数
                    .AppendLine("  ,TJK.[11gatu_jisseki_kingaku] ")   '11月_実績金額
                    .AppendLine("  ,TJK.[11gatu_jisseki_arari] ")     '11月_実績粗利
                    .AppendLine("  ,TJK.[12gatu_jisseki_kensuu] ")    '12月_実績件数
                    .AppendLine("  ,TJK.[12gatu_jisseki_kingaku] ")   '12月_実績金額
                    .AppendLine("  ,TJK.[12gatu_jisseki_arari] ")     '12月_実績粗利
                    .AppendLine("  ,TJK.[1gatu_jisseki_kensuu] ")     '1月_実績件数
                    .AppendLine("  ,TJK.[1gatu_jisseki_kingaku] ")    '1月_実績金額
                    .AppendLine("  ,TJK.[1gatu_jisseki_arari] ")      '1月_実績粗利
                    .AppendLine("  ,TJK.[2gatu_jisseki_kensuu] ")     '2月_実績件数
                    .AppendLine("  ,TJK.[2gatu_jisseki_kingaku] ")    '2月_実績金額
                    .AppendLine("  ,TJK.[2gatu_jisseki_arari] ")      '2月_実績粗利
                    .AppendLine("  ,TJK.[3gatu_jisseki_kensuu] ")     '3月_実績件数
                    .AppendLine("  ,TJK.[3gatu_jisseki_kingaku] ")    '3月_実績金額
                    .AppendLine("  ,TJK.[3gatu_jisseki_arari] ")      '3月_実績粗利

                    .AppendLine(" ,(TKK.[4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu] ") '4月_計画件数
                    .AppendLine(" ,(TKK.[4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku] ") '4月_計画金額
                    .AppendLine(" ,(TKK.[4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari] ") '4月_計画粗利
                    .AppendLine(" ,(TKK.[5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu] ") '5月_計画件数
                    .AppendLine(" ,(TKK.[5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku] ") '5月_計画金額
                    .AppendLine(" ,(TKK.[5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari] ") '5月_計画粗利
                    .AppendLine(" ,(TKK.[6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu] ") '6月_計画件数
                    .AppendLine(" ,(TKK.[6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku] ") '6月_計画金額
                    .AppendLine(" ,(TKK.[6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari] ") '6月_計画粗利
                    .AppendLine(" ,(TKK.[7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu] ") '7月_計画件数
                    .AppendLine(" ,(TKK.[7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku] ") '7月_計画金額
                    .AppendLine(" ,(TKK.[7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari] ") '7月_計画粗利
                    .AppendLine(" ,(TKK.[8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu] ") '8月_計画件数
                    .AppendLine(" ,(TKK.[8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ") '8月_計画金額
                    .AppendLine(" ,(TKK.[8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari] ") '8月_計画粗利
                    .AppendLine(" ,(TKK.[9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu] ") '9月_計画件数
                    .AppendLine(" ,(TKK.[9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku] ") '9月_計画金額
                    .AppendLine(" ,(TKK.[9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari] ") '9月_計画粗利
                    .AppendLine(" ,(TKK.[10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu] ") '10月_計画件数
                    .AppendLine(" ,(TKK.[10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku] ") '10月_計画金額
                    .AppendLine(" ,(TKK.[10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari] ") '10月_計画粗利
                    .AppendLine(" ,(TKK.[11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu] ") '11月_計画件数
                    .AppendLine(" ,(TKK.[11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ") '11月_計画金額
                    .AppendLine(" ,(TKK.[11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari] ") '11月_計画粗利
                    .AppendLine(" ,(TKK.[12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu] ") '12月_計画件数
                    .AppendLine(" ,(TKK.[12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku] ") '12月_計画金額
                    .AppendLine(" ,(TKK.[12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari] ") '12月_計画粗利
                    .AppendLine(" ,(TKK.[1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu] ") '1月_計画件数
                    .AppendLine(" ,(TKK.[1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku] ") '1月_計画金額
                    .AppendLine(" ,(TKK.[1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari] ") '1月_計画粗利
                    .AppendLine(" ,(TKK.[2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu] ") '2月_計画件数
                    .AppendLine(" ,(TKK.[2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku] ") '2月_計画金額
                    .AppendLine(" ,(TKK.[2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari] ") '2月_計画粗利
                    .AppendLine(" ,(TKK.[3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu] ") '3月_計画件数
                    .AppendLine(" ,(TKK.[3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ") '3月_計画金額
                    .AppendLine(" ,(TKK.[3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari] ") '3月_計画粗利

                    .AppendLine(" ,(TYMK.[4gatu_mikomi_kensuu] ) AS [4gatu_mikomi_kensuu] ") '4月_見込件数
                    .AppendLine(" ,(TYMK.[4gatu_mikomi_kingaku] ) AS [4gatu_mikomi_kingaku] ") '4月_見込金額
                    .AppendLine(" ,(TYMK.[4gatu_mikomi_arari] ) AS [4gatu_mikomi_arari] ") '4月_見込粗利
                    .AppendLine(" ,(TYMK.[5gatu_mikomi_kensuu] ) AS [5gatu_mikomi_kensuu] ") '5月_見込件数
                    .AppendLine(" ,(TYMK.[5gatu_mikomi_kingaku] ) AS [5gatu_mikomi_kingaku] ") '5月_見込金額
                    .AppendLine(" ,(TYMK.[5gatu_mikomi_arari] ) AS [5gatu_mikomi_arari] ") '5月_見込粗利
                    .AppendLine(" ,(TYMK.[6gatu_mikomi_kensuu] ) AS [6gatu_mikomi_kensuu] ") '6月_見込件数
                    .AppendLine(" ,(TYMK.[6gatu_mikomi_kingaku] ) AS [6gatu_mikomi_kingaku] ") '6月_見込金額
                    .AppendLine(" ,(TYMK.[6gatu_mikomi_arari] ) AS [6gatu_mikomi_arari] ") '6月_見込粗利
                    .AppendLine(" ,(TYMK.[7gatu_mikomi_kensuu] ) AS [7gatu_mikomi_kensuu] ") '7月_見込件数
                    .AppendLine(" ,(TYMK.[7gatu_mikomi_kingaku] ) AS [7gatu_mikomi_kingaku] ") '7月_見込金額
                    .AppendLine(" ,(TYMK.[7gatu_mikomi_arari] ) AS [7gatu_mikomi_arari] ") '7月_見込粗利
                    .AppendLine(" ,(TYMK.[8gatu_mikomi_kensuu] ) AS [8gatu_mikomi_kensuu] ") '8月_見込件数
                    .AppendLine(" ,(TYMK.[8gatu_mikomi_kingaku] ) AS [8gatu_mikomi_kingaku] ") '8月_見込金額
                    .AppendLine(" ,(TYMK.[8gatu_mikomi_arari] ) AS [8gatu_mikomi_arari] ") '8月_見込粗利
                    .AppendLine(" ,(TYMK.[9gatu_mikomi_kensuu] ) AS [9gatu_mikomi_kensuu] ") '9月_見込件数
                    .AppendLine(" ,(TYMK.[9gatu_mikomi_kingaku] ) AS [9gatu_mikomi_kingaku] ") '9月_見込金額
                    .AppendLine(" ,(TYMK.[9gatu_mikomi_arari] ) AS [9gatu_mikomi_arari] ") '9月_見込粗利
                    .AppendLine(" ,(TYMK.[10gatu_mikomi_kensuu] ) AS [10gatu_mikomi_kensuu] ") '10月_見込件数
                    .AppendLine(" ,(TYMK.[10gatu_mikomi_kingaku] ) AS [10gatu_mikomi_kingaku] ") '10月_見込金額
                    .AppendLine(" ,(TYMK.[10gatu_mikomi_arari] ) AS [10gatu_mikomi_arari] ") '10月_見込粗利
                    .AppendLine(" ,(TYMK.[11gatu_mikomi_kensuu] ) AS [11gatu_mikomi_kensuu] ") '11月_見込件数
                    .AppendLine(" ,(TYMK.[11gatu_mikomi_kingaku] ) AS [11gatu_mikomi_kingaku] ") '11月_見込金額
                    .AppendLine(" ,(TYMK.[11gatu_mikomi_arari] ) AS [11gatu_mikomi_arari] ") '11月_見込粗利
                    .AppendLine(" ,(TYMK.[12gatu_mikomi_kensuu] ) AS [12gatu_mikomi_kensuu] ") '12月_見込件数
                    .AppendLine(" ,(TYMK.[12gatu_mikomi_kingaku] ) AS [12gatu_mikomi_kingaku] ") '12月_見込金額
                    .AppendLine(" ,(TYMK.[12gatu_mikomi_arari] ) AS [12gatu_mikomi_arari] ") '12月_見込粗利
                    .AppendLine(" ,(TYMK.[1gatu_mikomi_kensuu] ) AS [1gatu_mikomi_kensuu] ") '1月_見込件数
                    .AppendLine(" ,(TYMK.[1gatu_mikomi_kingaku] ) AS [1gatu_mikomi_kingaku] ") '1月_見込金額
                    .AppendLine(" ,(TYMK.[1gatu_mikomi_arari] ) AS [1gatu_mikomi_arari] ") '1月_見込粗利
                    .AppendLine(" ,(TYMK.[2gatu_mikomi_kensuu] ) AS [2gatu_mikomi_kensuu] ") '2月_見込件数
                    .AppendLine(" ,(TYMK.[2gatu_mikomi_kingaku] ) AS [2gatu_mikomi_kingaku] ") '2月_見込金額
                    .AppendLine(" ,(TYMK.[2gatu_mikomi_arari] ) AS [2gatu_mikomi_arari] ") '2月_見込粗利
                    .AppendLine(" ,(TYMK.[3gatu_mikomi_kensuu] ) AS [3gatu_mikomi_kensuu] ") '3月_見込件数
                    .AppendLine(" ,(TYMK.[3gatu_mikomi_kingaku] ) AS [3gatu_mikomi_kingaku] ") '3月_見込金額
                    .AppendLine(" ,(TYMK.[3gatu_mikomi_arari] ) AS [3gatu_mikomi_arari] ") '3月_見込粗利


                    .AppendLine(" ,NULL AS [Z_4gatu_jisseki_kensuu] ") '前年4月_実績件数
                    .AppendLine(" ,NULL AS [Z_4gatu_jisseki_kingaku] ") '前年4月_実績金額
                    .AppendLine(" ,NULL AS [Z_4gatu_jisseki_arari] ") '前年4月_実績粗利
                    .AppendLine(" ,NULL AS [Z_5gatu_jisseki_kensuu] ") '前年5月_実績件数
                    .AppendLine(" ,NULL AS [Z_5gatu_jisseki_kingaku] ") '前年5月_実績金額
                    .AppendLine(" ,NULL AS [Z_5gatu_jisseki_arari] ") '前年5月_実績粗利
                    .AppendLine(" ,NULL AS [Z_6gatu_jisseki_kensuu] ") '前年6月_実績件数
                    .AppendLine(" ,NULL AS [Z_6gatu_jisseki_kingaku] ") '前年6月_実績金額
                    .AppendLine(" ,NULL AS [Z_6gatu_jisseki_arari] ") '前年6月_実績粗利
                    .AppendLine(" ,NULL AS [Z_7gatu_jisseki_kensuu] ") '前年7月_実績件数
                    .AppendLine(" ,NULL AS [Z_7gatu_jisseki_kingaku] ") '前年7月_実績金額
                    .AppendLine(" ,NULL AS [Z_7gatu_jisseki_arari] ") '前年7月_実績粗利
                    .AppendLine(" ,NULL AS [Z_8gatu_jisseki_kensuu] ") '前年8月_実績件数
                    .AppendLine(" ,NULL AS [Z_8gatu_jisseki_kingaku] ") '前年8月_実績金額
                    .AppendLine(" ,NULL AS [Z_8gatu_jisseki_arari] ") '前年8月_実績粗利
                    .AppendLine(" ,NULL AS [Z_9gatu_jisseki_kensuu] ") '前年9月_実績件数
                    .AppendLine(" ,NULL AS [Z_9gatu_jisseki_kingaku] ") '前年9月_実績金額
                    .AppendLine(" ,NULL AS [Z_9gatu_jisseki_arari] ") '前年9月_実績粗利
                    .AppendLine(" ,NULL AS [Z_10gatu_jisseki_kensuu] ") '前年10月_実績件数
                    .AppendLine(" ,NULL AS [Z_10gatu_jisseki_kingaku] ") '前年10月_実績金額
                    .AppendLine(" ,NULL AS [Z_10gatu_jisseki_arari] ") '前年10月_実績粗利
                    .AppendLine(" ,NULL AS [Z_11gatu_jisseki_kensuu] ") '前年11月_実績件数
                    .AppendLine(" ,NULL AS [Z_11gatu_jisseki_kingaku] ") '前年11月_実績金額
                    .AppendLine(" ,NULL AS [Z_11gatu_jisseki_arari] ") '前年11月_実績粗利
                    .AppendLine(" ,NULL AS [Z_12gatu_jisseki_kensuu] ") '前年12月_実績件数
                    .AppendLine(" ,NULL AS [Z_12gatu_jisseki_kingaku] ") '前年12月_実績金額
                    .AppendLine(" ,NULL AS [Z_12gatu_jisseki_arari] ") '前年12月_実績粗利
                    .AppendLine(" ,NULL AS [Z_1gatu_jisseki_kensuu] ") '前年1月_実績件数
                    .AppendLine(" ,NULL AS [Z_1gatu_jisseki_kingaku] ") '前年1月_実績金額
                    .AppendLine(" ,NULL AS [Z_1gatu_jisseki_arari] ") '前年1月_実績粗利
                    .AppendLine(" ,NULL AS [Z_2gatu_jisseki_kensuu] ") '前年2月_実績件数
                    .AppendLine(" ,NULL AS [Z_2gatu_jisseki_kingaku] ") '前年2月_実績金額
                    .AppendLine(" ,NULL AS [Z_2gatu_jisseki_arari] ") '前年2月_実績粗利
                    .AppendLine(" ,NULL AS [Z_3gatu_jisseki_kensuu] ") '前年3月_実績件数
                    .AppendLine(" ,NULL AS [Z_3gatu_jisseki_kingaku] ") '前年3月_実績金額
                    .AppendLine(" ,NULL AS [Z_3gatu_jisseki_arari] ") '前年3月_実績粗利

                    .AppendLine(" ,(TKK.[4gatu_keisanyou_koj_hantei_ritu] ) AS [4gatu_keisanyou_koj_hantei_ritu] ") '4月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[4gatu_keisanyou_koj_jyuchuu_ritu] ) AS [4gatu_keisanyou_koj_jyuchuu_ritu] ") '4月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[4gatu_keisanyou_tyoku_koj_ritu] ) AS [4gatu_keisanyou_tyoku_koj_ritu] ") '4月_計算用_直工事率
                    .AppendLine(" ,(TKK.[5gatu_keisanyou_koj_hantei_ritu] ) AS [5gatu_keisanyou_koj_hantei_ritu] ") '5月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[5gatu_keisanyou_koj_jyuchuu_ritu] ) AS [5gatu_keisanyou_koj_jyuchuu_ritu] ") '5月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[5gatu_keisanyou_tyoku_koj_ritu] ) AS [5gatu_keisanyou_tyoku_koj_ritu] ") '5月_計算用_直工事率
                    .AppendLine(" ,(TKK.[6gatu_keisanyou_koj_hantei_ritu] ) AS [6gatu_keisanyou_koj_hantei_ritu] ") '6月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[6gatu_keisanyou_koj_jyuchuu_ritu] ) AS [6gatu_keisanyou_koj_jyuchuu_ritu] ") '6月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[6gatu_keisanyou_tyoku_koj_ritu] ) AS [6gatu_keisanyou_tyoku_koj_ritu] ") '6月_計算用_直工事率
                    .AppendLine(" ,(TKK.[7gatu_keisanyou_koj_hantei_ritu] ) AS [7gatu_keisanyou_koj_hantei_ritu] ") '7月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[7gatu_keisanyou_koj_jyuchuu_ritu] ) AS [7gatu_keisanyou_koj_jyuchuu_ritu] ") '7月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[7gatu_keisanyou_tyoku_koj_ritu] ) AS [7gatu_keisanyou_tyoku_koj_ritu] ") '7月_計算用_直工事率
                    .AppendLine(" ,(TKK.[8gatu_keisanyou_koj_hantei_ritu] ) AS [8gatu_keisanyou_koj_hantei_ritu] ") '8月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[8gatu_keisanyou_koj_jyuchuu_ritu] ) AS [8gatu_keisanyou_koj_jyuchuu_ritu] ") '8月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[8gatu_keisanyou_tyoku_koj_ritu] ) AS [8gatu_keisanyou_tyoku_koj_ritu] ") '8月_計算用_直工事率
                    .AppendLine(" ,(TKK.[9gatu_keisanyou_koj_hantei_ritu] ) AS [9gatu_keisanyou_koj_hantei_ritu] ") '9月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[9gatu_keisanyou_koj_jyuchuu_ritu] ) AS [9gatu_keisanyou_koj_jyuchuu_ritu] ") '9月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[9gatu_keisanyou_tyoku_koj_ritu] ) AS [9gatu_keisanyou_tyoku_koj_ritu] ") '9月_計算用_直工事率
                    .AppendLine(" ,(TKK.[10gatu_keisanyou_koj_hantei_ritu] ) AS [10gatu_keisanyou_koj_hantei_ritu] ") '10月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[10gatu_keisanyou_koj_jyuchuu_ritu] ) AS [10gatu_keisanyou_koj_jyuchuu_ritu] ") '10月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[10gatu_keisanyou_tyoku_koj_ritu] ) AS [10gatu_keisanyou_tyoku_koj_ritu] ") '10月_計算用_直工事率
                    .AppendLine(" ,(TKK.[11gatu_keisanyou_koj_hantei_ritu] ) AS [11gatu_keisanyou_koj_hantei_ritu] ") '11月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[11gatu_keisanyou_koj_jyuchuu_ritu] ) AS [11gatu_keisanyou_koj_jyuchuu_ritu] ") '11月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[11gatu_keisanyou_tyoku_koj_ritu] ) AS [11gatu_keisanyou_tyoku_koj_ritu] ") '11月_計算用_直工事率
                    .AppendLine(" ,(TKK.[12gatu_keisanyou_koj_hantei_ritu] ) AS [12gatu_keisanyou_koj_hantei_ritu] ") '12月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[12gatu_keisanyou_koj_jyuchuu_ritu] ) AS [12gatu_keisanyou_koj_jyuchuu_ritu] ") '12月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[12gatu_keisanyou_tyoku_koj_ritu] ) AS [12gatu_keisanyou_tyoku_koj_ritu] ") '12月_計算用_直工事率
                    .AppendLine(" ,(TKK.[1gatu_keisanyou_koj_hantei_ritu] ) AS [1gatu_keisanyou_koj_hantei_ritu] ") '1月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[1gatu_keisanyou_koj_jyuchuu_ritu] ) AS [1gatu_keisanyou_koj_jyuchuu_ritu] ") '1月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[1gatu_keisanyou_tyoku_koj_ritu] ) AS [1gatu_keisanyou_tyoku_koj_ritu] ") '1月_計算用_直工事率
                    .AppendLine(" ,(TKK.[2gatu_keisanyou_koj_hantei_ritu] ) AS [2gatu_keisanyou_koj_hantei_ritu] ") '2月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[2gatu_keisanyou_koj_jyuchuu_ritu] ) AS [2gatu_keisanyou_koj_jyuchuu_ritu] ") '2月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[2gatu_keisanyou_tyoku_koj_ritu] ) AS [2gatu_keisanyou_tyoku_koj_ritu] ") '2月_計算用_直工事率
                    .AppendLine(" ,(TKK.[3gatu_keisanyou_koj_hantei_ritu] ) AS [3gatu_keisanyou_koj_hantei_ritu] ") '3月_計算用_工事判定率
                    .AppendLine(" ,(TKK.[3gatu_keisanyou_koj_jyuchuu_ritu] ) AS [3gatu_keisanyou_koj_jyuchuu_ritu] ") '3月_計算用_工事受注率
                    .AppendLine(" ,(TKK.[3gatu_keisanyou_tyoku_koj_ritu] ) AS [3gatu_keisanyou_tyoku_koj_ritu] ") '3月_計算用_直工事率
                    .AppendLine("  ,NULL  AS eigyou_kbn ")
                    .AppendLine("  ,NULL AS add_datetime ")
                    '.AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   '見込_SDS選択時追加金額
                    '.AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4月_見込_SS_SDS入力
                    '.AppendLine("  ,NULL AS [5gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [6gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [7gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [8gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [9gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [10gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [11gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [12gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [1gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [2gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3月_見込_SS_SDS入力
                    '.AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ") '計画_SDS選択時追加金額
                    '.AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4月_計画_SS_SDS入力
                    '.AppendLine("  ,NULL AS [5gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [6gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [7gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [8gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [9gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [10gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [11gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [12gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [1gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [2gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3月_計画_SS_SDS入力

                    .AppendLine("  ,TKK.[4gatu_keisanyou_uri_heikin_tanka] ")       '4月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[4gatu_keisanyou_siire_heikin_tanka] ")     '4月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[5gatu_keisanyou_uri_heikin_tanka] ")       '5月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[5gatu_keisanyou_siire_heikin_tanka] ")     '5月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[6gatu_keisanyou_uri_heikin_tanka] ")       '6月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[6gatu_keisanyou_siire_heikin_tanka] ")     '6月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[7gatu_keisanyou_uri_heikin_tanka] ")       '7月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[7gatu_keisanyou_siire_heikin_tanka] ")     '7月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[8gatu_keisanyou_uri_heikin_tanka] ")       '8月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[8gatu_keisanyou_siire_heikin_tanka] ")     '8月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[9gatu_keisanyou_uri_heikin_tanka] ")       '9月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[9gatu_keisanyou_siire_heikin_tanka] ")     '9月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[10gatu_keisanyou_uri_heikin_tanka] ")      '10月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[10gatu_keisanyou_siire_heikin_tanka] ")    '10月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[11gatu_keisanyou_uri_heikin_tanka] ")      '11月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[11gatu_keisanyou_siire_heikin_tanka] ")    '11月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[12gatu_keisanyou_uri_heikin_tanka] ")      '12月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[12gatu_keisanyou_siire_heikin_tanka] ")    '12月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[1gatu_keisanyou_uri_heikin_tanka] ")       '1月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[1gatu_keisanyou_siire_heikin_tanka] ")     '1月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[2gatu_keisanyou_uri_heikin_tanka] ")       '2月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[2gatu_keisanyou_siire_heikin_tanka] ")     '2月_計算用__仕入平均単価
                    .AppendLine("  ,TKK.[3gatu_keisanyou_uri_heikin_tanka] ")       '3月_計算用__売上平均単価
                    .AppendLine("  ,TKK.[3gatu_keisanyou_siire_heikin_tanka] ")     '3月_計算用__仕入平均単価

                Case Utilities.KeikakuKanriRecord.selectKbn.goukeiFC
                    .AppendLine(" SELECT  ")
                    .AppendLine("  NULL AS kameiten_mei")           '加盟店名
                    .AppendLine("  ,NULL AS kameiten_cd ")          '加盟店コード
                    .AppendLine("  ,'全体' AS  meisyou ")           '営業区分
                    .AppendLine("  ,NULL AS eigyou_tantousya_mei ") '営業担当者名
                    .AppendLine("  ,NULL AS keikaku_nenkan_tousuu") '年間棟数

                    .AppendLine("  ,NULL AS [keikakuyou_nenkan_tousuu] ") '計画用_年間棟数

                    .AppendLine("  ,NULL AS gyoutai ")              '業態
                    .AppendLine("  ,NULL AS uri_hiritu ")           '売上比率

                    .AppendLine("  ,NULL AS [sds_kaisi_nengetu] ")  'SDS開始年月

                    .AppendLine("  ,NULL AS koj_hantei_ritu ")      '工事判定率
                    .AppendLine("  ,NULL AS koj_jyuchuu_ritu ")     '工事受注率
                    .AppendLine("  ,NULL AS tyoku_koj_ritu ")       '直工事率
                    .AppendLine("  ,meisyou2   ")                   '名称種別
                    .AppendLine("  ,syouhin_mei  ")                 '商品名
                    .AppendLine("  ,NULL AS [zennen_heikin_tanka]  ")     '前年_平均単価

                    .AppendLine(" ,sum([4gatu_jisseki_kensuu] ) AS [4gatu_jisseki_kensuu] ") '4月_実績件数
                    .AppendLine(" ,sum([4gatu_jisseki_kingaku] ) AS [4gatu_jisseki_kingaku] ") '4月_実績金額
                    .AppendLine(" ,sum([4gatu_jisseki_arari] ) AS [4gatu_jisseki_arari] ") '4月_実績粗利
                    .AppendLine(" ,sum([5gatu_jisseki_kensuu] ) AS [5gatu_jisseki_kensuu] ") '5月_実績件数
                    .AppendLine(" ,sum([5gatu_jisseki_kingaku] ) AS [5gatu_jisseki_kingaku] ") '5月_実績金額
                    .AppendLine(" ,sum([5gatu_jisseki_arari] ) AS [5gatu_jisseki_arari] ") '5月_実績粗利
                    .AppendLine(" ,sum([6gatu_jisseki_kensuu] ) AS [6gatu_jisseki_kensuu] ") '6月_実績件数
                    .AppendLine(" ,sum([6gatu_jisseki_kingaku] ) AS [6gatu_jisseki_kingaku] ") '6月_実績金額
                    .AppendLine(" ,sum([6gatu_jisseki_arari] ) AS [6gatu_jisseki_arari] ") '6月_実績粗利
                    .AppendLine(" ,sum([7gatu_jisseki_kensuu] ) AS [7gatu_jisseki_kensuu] ") '7月_実績件数
                    .AppendLine(" ,sum([7gatu_jisseki_kingaku] ) AS [7gatu_jisseki_kingaku] ") '7月_実績金額
                    .AppendLine(" ,sum([7gatu_jisseki_arari] ) AS [7gatu_jisseki_arari] ") '7月_実績粗利
                    .AppendLine(" ,sum([8gatu_jisseki_kensuu] ) AS [8gatu_jisseki_kensuu] ") '8月_実績件数
                    .AppendLine(" ,sum([8gatu_jisseki_kingaku] ) AS [8gatu_jisseki_kingaku] ") '8月_実績金額
                    .AppendLine(" ,sum([8gatu_jisseki_arari] ) AS [8gatu_jisseki_arari] ") '8月_実績粗利
                    .AppendLine(" ,sum([9gatu_jisseki_kensuu] ) AS [9gatu_jisseki_kensuu] ") '9月_実績件数
                    .AppendLine(" ,sum([9gatu_jisseki_kingaku] ) AS [9gatu_jisseki_kingaku] ") '9月_実績金額
                    .AppendLine(" ,sum([9gatu_jisseki_arari] ) AS [9gatu_jisseki_arari] ") '9月_実績粗利
                    .AppendLine(" ,sum([10gatu_jisseki_kensuu] ) AS [10gatu_jisseki_kensuu] ") '10月_実績件数
                    .AppendLine(" ,sum([10gatu_jisseki_kingaku] ) AS [10gatu_jisseki_kingaku] ") '10月_実績金額
                    .AppendLine(" ,sum([10gatu_jisseki_arari] ) AS [10gatu_jisseki_arari] ") '10月_実績粗利
                    .AppendLine(" ,sum([11gatu_jisseki_kensuu] ) AS [11gatu_jisseki_kensuu] ") '11月_実績件数
                    .AppendLine(" ,sum([11gatu_jisseki_kingaku] ) AS [11gatu_jisseki_kingaku] ") '11月_実績金額
                    .AppendLine(" ,sum([11gatu_jisseki_arari] ) AS [11gatu_jisseki_arari] ") '11月_実績粗利
                    .AppendLine(" ,sum([12gatu_jisseki_kensuu] ) AS [12gatu_jisseki_kensuu] ") '12月_実績件数
                    .AppendLine(" ,sum([12gatu_jisseki_kingaku] ) AS [12gatu_jisseki_kingaku] ") '12月_実績金額
                    .AppendLine(" ,sum([12gatu_jisseki_arari] ) AS [12gatu_jisseki_arari] ") '12月_実績粗利
                    .AppendLine(" ,sum([1gatu_jisseki_kensuu] ) AS [1gatu_jisseki_kensuu] ") '1月_実績件数
                    .AppendLine(" ,sum([1gatu_jisseki_kingaku] ) AS [1gatu_jisseki_kingaku] ") '1月_実績金額
                    .AppendLine(" ,sum([1gatu_jisseki_arari] ) AS [1gatu_jisseki_arari] ") '1月_実績粗利
                    .AppendLine(" ,sum([2gatu_jisseki_kensuu] ) AS [2gatu_jisseki_kensuu] ") '2月_実績件数
                    .AppendLine(" ,sum([2gatu_jisseki_kingaku] ) AS [2gatu_jisseki_kingaku] ") '2月_実績金額
                    .AppendLine(" ,sum([2gatu_jisseki_arari] ) AS [2gatu_jisseki_arari] ") '2月_実績粗利
                    .AppendLine(" ,sum([3gatu_jisseki_kensuu] ) AS [3gatu_jisseki_kensuu] ") '3月_実績件数
                    .AppendLine(" ,sum([3gatu_jisseki_kingaku] ) AS [3gatu_jisseki_kingaku] ") '3月_実績金額
                    .AppendLine(" ,sum([3gatu_jisseki_arari] ) AS [3gatu_jisseki_arari] ") '3月_実績粗利

                    .AppendLine(" ,sum([4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu] ") '4月_計画件数
                    .AppendLine(" ,sum([4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku] ") '4月_計画金額
                    .AppendLine(" ,sum([4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari] ") '4月_計画粗利
                    .AppendLine(" ,sum([5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu] ") '5月_計画件数
                    .AppendLine(" ,sum([5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku] ") '5月_計画金額
                    .AppendLine(" ,sum([5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari] ") '5月_計画粗利
                    .AppendLine(" ,sum([6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu] ") '6月_計画件数
                    .AppendLine(" ,sum([6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku] ") '6月_計画金額
                    .AppendLine(" ,sum([6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari] ") '6月_計画粗利
                    .AppendLine(" ,sum([7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu] ") '7月_計画件数
                    .AppendLine(" ,sum([7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku] ") '7月_計画金額
                    .AppendLine(" ,sum([7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari] ") '7月_計画粗利
                    .AppendLine(" ,sum([8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu] ") '8月_計画件数
                    .AppendLine(" ,sum([8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ") '8月_計画金額
                    .AppendLine(" ,sum([8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari] ") '8月_計画粗利
                    .AppendLine(" ,sum([9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu] ") '9月_計画件数
                    .AppendLine(" ,sum([9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku] ") '9月_計画金額
                    .AppendLine(" ,sum([9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari] ") '9月_計画粗利
                    .AppendLine(" ,sum([10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu] ") '10月_計画件数
                    .AppendLine(" ,sum([10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku] ") '10月_計画金額
                    .AppendLine(" ,sum([10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari] ") '10月_計画粗利
                    .AppendLine(" ,sum([11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu] ") '11月_計画件数
                    .AppendLine(" ,sum([11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ") '11月_計画金額
                    .AppendLine(" ,sum([11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari] ") '11月_計画粗利
                    .AppendLine(" ,sum([12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu] ") '12月_計画件数
                    .AppendLine(" ,sum([12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku] ") '12月_計画金額
                    .AppendLine(" ,sum([12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari] ") '12月_計画粗利
                    .AppendLine(" ,sum([1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu] ") '1月_計画件数
                    .AppendLine(" ,sum([1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku] ") '1月_計画金額
                    .AppendLine(" ,sum([1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari] ") '1月_計画粗利
                    .AppendLine(" ,sum([2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu] ") '2月_計画件数
                    .AppendLine(" ,sum([2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku] ") '2月_計画金額
                    .AppendLine(" ,sum([2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari] ") '2月_計画粗利
                    .AppendLine(" ,sum([3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu] ") '3月_計画件数
                    .AppendLine(" ,sum([3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ") '3月_計画金額
                    .AppendLine(" ,sum([3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari] ") '3月_計画粗利

                    .AppendLine(" ,sum([4gatu_mikomi_kensuu] ) AS [4gatu_mikomi_kensuu] ") '4月_見込件数
                    .AppendLine(" ,sum([4gatu_mikomi_kingaku] ) AS [4gatu_mikomi_kingaku] ") '4月_見込金額
                    .AppendLine(" ,sum([4gatu_mikomi_arari] ) AS [4gatu_mikomi_arari] ") '4月_見込粗利
                    .AppendLine(" ,sum([5gatu_mikomi_kensuu] ) AS [5gatu_mikomi_kensuu] ") '5月_見込件数
                    .AppendLine(" ,sum([5gatu_mikomi_kingaku] ) AS [5gatu_mikomi_kingaku] ") '5月_見込金額
                    .AppendLine(" ,sum([5gatu_mikomi_arari] ) AS [5gatu_mikomi_arari] ") '5月_見込粗利
                    .AppendLine(" ,sum([6gatu_mikomi_kensuu] ) AS [6gatu_mikomi_kensuu] ") '6月_見込件数
                    .AppendLine(" ,sum([6gatu_mikomi_kingaku] ) AS [6gatu_mikomi_kingaku] ") '6月_見込金額
                    .AppendLine(" ,sum([6gatu_mikomi_arari] ) AS [6gatu_mikomi_arari] ") '6月_見込粗利
                    .AppendLine(" ,sum([7gatu_mikomi_kensuu] ) AS [7gatu_mikomi_kensuu] ") '7月_見込件数
                    .AppendLine(" ,sum([7gatu_mikomi_kingaku] ) AS [7gatu_mikomi_kingaku] ") '7月_見込金額
                    .AppendLine(" ,sum([7gatu_mikomi_arari] ) AS [7gatu_mikomi_arari] ") '7月_見込粗利
                    .AppendLine(" ,sum([8gatu_mikomi_kensuu] ) AS [8gatu_mikomi_kensuu] ") '8月_見込件数
                    .AppendLine(" ,sum([8gatu_mikomi_kingaku] ) AS [8gatu_mikomi_kingaku] ") '8月_見込金額
                    .AppendLine(" ,sum([8gatu_mikomi_arari] ) AS [8gatu_mikomi_arari] ") '8月_見込粗利
                    .AppendLine(" ,sum([9gatu_mikomi_kensuu] ) AS [9gatu_mikomi_kensuu] ") '9月_見込件数
                    .AppendLine(" ,sum([9gatu_mikomi_kingaku] ) AS [9gatu_mikomi_kingaku] ") '9月_見込金額
                    .AppendLine(" ,sum([9gatu_mikomi_arari] ) AS [9gatu_mikomi_arari] ") '9月_見込粗利
                    .AppendLine(" ,sum([10gatu_mikomi_kensuu] ) AS [10gatu_mikomi_kensuu] ") '10月_見込件数
                    .AppendLine(" ,sum([10gatu_mikomi_kingaku] ) AS [10gatu_mikomi_kingaku] ") '10月_見込金額
                    .AppendLine(" ,sum([10gatu_mikomi_arari] ) AS [10gatu_mikomi_arari] ") '10月_見込粗利
                    .AppendLine(" ,sum([11gatu_mikomi_kensuu] ) AS [11gatu_mikomi_kensuu] ") '11月_見込件数
                    .AppendLine(" ,sum([11gatu_mikomi_kingaku] ) AS [11gatu_mikomi_kingaku] ") '11月_見込金額
                    .AppendLine(" ,sum([11gatu_mikomi_arari] ) AS [11gatu_mikomi_arari] ") '11月_見込粗利
                    .AppendLine(" ,sum([12gatu_mikomi_kensuu] ) AS [12gatu_mikomi_kensuu] ") '12月_見込件数
                    .AppendLine(" ,sum([12gatu_mikomi_kingaku] ) AS [12gatu_mikomi_kingaku] ") '12月_見込金額
                    .AppendLine(" ,sum([12gatu_mikomi_arari] ) AS [12gatu_mikomi_arari] ") '12月_見込粗利
                    .AppendLine(" ,sum([1gatu_mikomi_kensuu] ) AS [1gatu_mikomi_kensuu] ") '1月_見込件数
                    .AppendLine(" ,sum([1gatu_mikomi_kingaku] ) AS [1gatu_mikomi_kingaku] ") '1月_見込金額
                    .AppendLine(" ,sum([1gatu_mikomi_arari] ) AS [1gatu_mikomi_arari] ") '1月_見込粗利
                    .AppendLine(" ,sum([2gatu_mikomi_kensuu] ) AS [2gatu_mikomi_kensuu] ") '2月_見込件数
                    .AppendLine(" ,sum([2gatu_mikomi_kingaku] ) AS [2gatu_mikomi_kingaku] ") '2月_見込金額
                    .AppendLine(" ,sum([2gatu_mikomi_arari] ) AS [2gatu_mikomi_arari] ") '2月_見込粗利
                    .AppendLine(" ,sum([3gatu_mikomi_kensuu] ) AS [3gatu_mikomi_kensuu] ") '3月_見込件数
                    .AppendLine(" ,sum([3gatu_mikomi_kingaku] ) AS [3gatu_mikomi_kingaku] ") '3月_見込金額
                    .AppendLine(" ,sum([3gatu_mikomi_arari] ) AS [3gatu_mikomi_arari] ") '3月_見込粗利


                    .AppendLine(" ,sum([Z_4gatu_jisseki_kensuu] ) AS [Z_4gatu_jisseki_kensuu] ") '前年4月_実績件数
                    .AppendLine(" ,sum([Z_4gatu_jisseki_kingaku] ) AS [Z_4gatu_jisseki_kingaku] ") '前年4月_実績金額
                    .AppendLine(" ,sum([Z_4gatu_jisseki_arari] ) AS [Z_4gatu_jisseki_arari] ") '前年4月_実績粗利
                    .AppendLine(" ,sum([Z_5gatu_jisseki_kensuu] ) AS [Z_5gatu_jisseki_kensuu] ") '前年5月_実績件数
                    .AppendLine(" ,sum([Z_5gatu_jisseki_kingaku] ) AS [Z_5gatu_jisseki_kingaku] ") '前年5月_実績金額
                    .AppendLine(" ,sum([Z_5gatu_jisseki_arari] ) AS [Z_5gatu_jisseki_arari] ") '前年5月_実績粗利
                    .AppendLine(" ,sum([Z_6gatu_jisseki_kensuu] ) AS [Z_6gatu_jisseki_kensuu] ") '前年6月_実績件数
                    .AppendLine(" ,sum([Z_6gatu_jisseki_kingaku] ) AS [Z_6gatu_jisseki_kingaku] ") '前年6月_実績金額
                    .AppendLine(" ,sum([Z_6gatu_jisseki_arari] ) AS [Z_6gatu_jisseki_arari] ") '前年6月_実績粗利
                    .AppendLine(" ,sum([Z_7gatu_jisseki_kensuu] ) AS [Z_7gatu_jisseki_kensuu] ") '前年7月_実績件数
                    .AppendLine(" ,sum([Z_7gatu_jisseki_kingaku] ) AS [Z_7gatu_jisseki_kingaku] ") '前年7月_実績金額
                    .AppendLine(" ,sum([Z_7gatu_jisseki_arari] ) AS [Z_7gatu_jisseki_arari] ") '前年7月_実績粗利
                    .AppendLine(" ,sum([Z_8gatu_jisseki_kensuu] ) AS [Z_8gatu_jisseki_kensuu] ") '前年8月_実績件数
                    .AppendLine(" ,sum([Z_8gatu_jisseki_kingaku] ) AS [Z_8gatu_jisseki_kingaku] ") '前年8月_実績金額
                    .AppendLine(" ,sum([Z_8gatu_jisseki_arari] ) AS [Z_8gatu_jisseki_arari] ") '前年8月_実績粗利
                    .AppendLine(" ,sum([Z_9gatu_jisseki_kensuu] ) AS [Z_9gatu_jisseki_kensuu] ") '前年9月_実績件数
                    .AppendLine(" ,sum([Z_9gatu_jisseki_kingaku] ) AS [Z_9gatu_jisseki_kingaku] ") '前年9月_実績金額
                    .AppendLine(" ,sum([Z_9gatu_jisseki_arari] ) AS [Z_9gatu_jisseki_arari] ") '前年9月_実績粗利
                    .AppendLine(" ,sum([Z_10gatu_jisseki_kensuu] ) AS [Z_10gatu_jisseki_kensuu] ") '前年10月_実績件数
                    .AppendLine(" ,sum([Z_10gatu_jisseki_kingaku] ) AS [Z_10gatu_jisseki_kingaku] ") '前年10月_実績金額
                    .AppendLine(" ,sum([Z_10gatu_jisseki_arari] ) AS [Z_10gatu_jisseki_arari] ") '前年10月_実績粗利
                    .AppendLine(" ,sum([Z_11gatu_jisseki_kensuu] ) AS [Z_11gatu_jisseki_kensuu] ") '前年11月_実績件数
                    .AppendLine(" ,sum([Z_11gatu_jisseki_kingaku] ) AS [Z_11gatu_jisseki_kingaku] ") '前年11月_実績金額
                    .AppendLine(" ,sum([Z_11gatu_jisseki_arari] ) AS [Z_11gatu_jisseki_arari] ") '前年11月_実績粗利
                    .AppendLine(" ,sum([Z_12gatu_jisseki_kensuu] ) AS [Z_12gatu_jisseki_kensuu] ") '前年12月_実績件数
                    .AppendLine(" ,sum([Z_12gatu_jisseki_kingaku] ) AS [Z_12gatu_jisseki_kingaku] ") '前年12月_実績金額
                    .AppendLine(" ,sum([Z_12gatu_jisseki_arari] ) AS [Z_12gatu_jisseki_arari] ") '前年12月_実績粗利
                    .AppendLine(" ,sum([Z_1gatu_jisseki_kensuu] ) AS [Z_1gatu_jisseki_kensuu] ") '前年1月_実績件数
                    .AppendLine(" ,sum([Z_1gatu_jisseki_kingaku] ) AS [Z_1gatu_jisseki_kingaku] ") '前年1月_実績金額
                    .AppendLine(" ,sum([Z_1gatu_jisseki_arari] ) AS [Z_1gatu_jisseki_arari] ") '前年1月_実績粗利
                    .AppendLine(" ,sum([Z_2gatu_jisseki_kensuu] ) AS [Z_2gatu_jisseki_kensuu] ") '前年2月_実績件数
                    .AppendLine(" ,sum([Z_2gatu_jisseki_kingaku] ) AS [Z_2gatu_jisseki_kingaku] ") '前年2月_実績金額
                    .AppendLine(" ,sum([Z_2gatu_jisseki_arari] ) AS [Z_2gatu_jisseki_arari] ") '前年2月_実績粗利
                    .AppendLine(" ,sum([Z_3gatu_jisseki_kensuu] ) AS [Z_3gatu_jisseki_kensuu] ") '前年3月_実績件数
                    .AppendLine(" ,sum([Z_3gatu_jisseki_kingaku] ) AS [Z_3gatu_jisseki_kingaku] ") '前年3月_実績金額
                    .AppendLine(" ,sum([Z_3gatu_jisseki_arari] ) AS [Z_3gatu_jisseki_arari] ") '前年3月_実績粗利

                    .AppendLine(" ,sum([4gatu_keisanyou_koj_hantei_ritu] ) AS [4gatu_keisanyou_koj_hantei_ritu] ") '4月_計算用_工事判定率
                    .AppendLine(" ,sum([4gatu_keisanyou_koj_jyuchuu_ritu] ) AS [4gatu_keisanyou_koj_jyuchuu_ritu] ") '4月_計算用_工事受注率
                    .AppendLine(" ,sum([4gatu_keisanyou_tyoku_koj_ritu] ) AS [4gatu_keisanyou_tyoku_koj_ritu] ") '4月_計算用_直工事率
                    .AppendLine(" ,sum([5gatu_keisanyou_koj_hantei_ritu] ) AS [5gatu_keisanyou_koj_hantei_ritu] ") '5月_計算用_工事判定率
                    .AppendLine(" ,sum([5gatu_keisanyou_koj_jyuchuu_ritu] ) AS [5gatu_keisanyou_koj_jyuchuu_ritu] ") '5月_計算用_工事受注率
                    .AppendLine(" ,sum([5gatu_keisanyou_tyoku_koj_ritu] ) AS [5gatu_keisanyou_tyoku_koj_ritu] ") '5月_計算用_直工事率
                    .AppendLine(" ,sum([6gatu_keisanyou_koj_hantei_ritu] ) AS [6gatu_keisanyou_koj_hantei_ritu] ") '6月_計算用_工事判定率
                    .AppendLine(" ,sum([6gatu_keisanyou_koj_jyuchuu_ritu] ) AS [6gatu_keisanyou_koj_jyuchuu_ritu] ") '6月_計算用_工事受注率
                    .AppendLine(" ,sum([6gatu_keisanyou_tyoku_koj_ritu] ) AS [6gatu_keisanyou_tyoku_koj_ritu] ") '6月_計算用_直工事率
                    .AppendLine(" ,sum([7gatu_keisanyou_koj_hantei_ritu] ) AS [7gatu_keisanyou_koj_hantei_ritu] ") '7月_計算用_工事判定率
                    .AppendLine(" ,sum([7gatu_keisanyou_koj_jyuchuu_ritu] ) AS [7gatu_keisanyou_koj_jyuchuu_ritu] ") '7月_計算用_工事受注率
                    .AppendLine(" ,sum([7gatu_keisanyou_tyoku_koj_ritu] ) AS [7gatu_keisanyou_tyoku_koj_ritu] ") '7月_計算用_直工事率
                    .AppendLine(" ,sum([8gatu_keisanyou_koj_hantei_ritu] ) AS [8gatu_keisanyou_koj_hantei_ritu] ") '8月_計算用_工事判定率
                    .AppendLine(" ,sum([8gatu_keisanyou_koj_jyuchuu_ritu] ) AS [8gatu_keisanyou_koj_jyuchuu_ritu] ") '8月_計算用_工事受注率
                    .AppendLine(" ,sum([8gatu_keisanyou_tyoku_koj_ritu] ) AS [8gatu_keisanyou_tyoku_koj_ritu] ") '8月_計算用_直工事率
                    .AppendLine(" ,sum([9gatu_keisanyou_koj_hantei_ritu] ) AS [9gatu_keisanyou_koj_hantei_ritu] ") '9月_計算用_工事判定率
                    .AppendLine(" ,sum([9gatu_keisanyou_koj_jyuchuu_ritu] ) AS [9gatu_keisanyou_koj_jyuchuu_ritu] ") '9月_計算用_工事受注率
                    .AppendLine(" ,sum([9gatu_keisanyou_tyoku_koj_ritu] ) AS [9gatu_keisanyou_tyoku_koj_ritu] ") '9月_計算用_直工事率
                    .AppendLine(" ,sum([10gatu_keisanyou_koj_hantei_ritu] ) AS [10gatu_keisanyou_koj_hantei_ritu] ") '10月_計算用_工事判定率
                    .AppendLine(" ,sum([10gatu_keisanyou_koj_jyuchuu_ritu] ) AS [10gatu_keisanyou_koj_jyuchuu_ritu] ") '10月_計算用_工事受注率
                    .AppendLine(" ,sum([10gatu_keisanyou_tyoku_koj_ritu] ) AS [10gatu_keisanyou_tyoku_koj_ritu] ") '10月_計算用_直工事率
                    .AppendLine(" ,sum([11gatu_keisanyou_koj_hantei_ritu] ) AS [11gatu_keisanyou_koj_hantei_ritu] ") '11月_計算用_工事判定率
                    .AppendLine(" ,sum([11gatu_keisanyou_koj_jyuchuu_ritu] ) AS [11gatu_keisanyou_koj_jyuchuu_ritu] ") '11月_計算用_工事受注率
                    .AppendLine(" ,sum([11gatu_keisanyou_tyoku_koj_ritu] ) AS [11gatu_keisanyou_tyoku_koj_ritu] ") '11月_計算用_直工事率
                    .AppendLine(" ,sum([12gatu_keisanyou_koj_hantei_ritu] ) AS [12gatu_keisanyou_koj_hantei_ritu] ") '12月_計算用_工事判定率
                    .AppendLine(" ,sum([12gatu_keisanyou_koj_jyuchuu_ritu] ) AS [12gatu_keisanyou_koj_jyuchuu_ritu] ") '12月_計算用_工事受注率
                    .AppendLine(" ,sum([12gatu_keisanyou_tyoku_koj_ritu] ) AS [12gatu_keisanyou_tyoku_koj_ritu] ") '12月_計算用_直工事率
                    .AppendLine(" ,sum([1gatu_keisanyou_koj_hantei_ritu] ) AS [1gatu_keisanyou_koj_hantei_ritu] ") '1月_計算用_工事判定率
                    .AppendLine(" ,sum([1gatu_keisanyou_koj_jyuchuu_ritu] ) AS [1gatu_keisanyou_koj_jyuchuu_ritu] ") '1月_計算用_工事受注率
                    .AppendLine(" ,sum([1gatu_keisanyou_tyoku_koj_ritu] ) AS [1gatu_keisanyou_tyoku_koj_ritu] ") '1月_計算用_直工事率
                    .AppendLine(" ,sum([2gatu_keisanyou_koj_hantei_ritu] ) AS [2gatu_keisanyou_koj_hantei_ritu] ") '2月_計算用_工事判定率
                    .AppendLine(" ,sum([2gatu_keisanyou_koj_jyuchuu_ritu] ) AS [2gatu_keisanyou_koj_jyuchuu_ritu] ") '2月_計算用_工事受注率
                    .AppendLine(" ,sum([2gatu_keisanyou_tyoku_koj_ritu] ) AS [2gatu_keisanyou_tyoku_koj_ritu] ") '2月_計算用_直工事率
                    .AppendLine(" ,sum([3gatu_keisanyou_koj_hantei_ritu] ) AS [3gatu_keisanyou_koj_hantei_ritu] ") '3月_計算用_工事判定率
                    .AppendLine(" ,sum([3gatu_keisanyou_koj_jyuchuu_ritu] ) AS [3gatu_keisanyou_koj_jyuchuu_ritu] ") '3月_計算用_工事受注率
                    .AppendLine(" ,sum([3gatu_keisanyou_tyoku_koj_ritu] ) AS [3gatu_keisanyou_tyoku_koj_ritu] ") '3月_計算用_直工事率
                    .AppendLine("  ,NULL  AS eigyou_kbn ")
                    .AppendLine("  ,NULL AS add_datetime ")
                    '.AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   '見込_SDS選択時追加金額
                    '.AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4月_見込_SS_SDS入力
                    '.AppendLine("  ,NULL AS [5gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [6gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [7gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [8gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [9gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [10gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [11gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [12gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [1gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [2gatu_mikomi_ss_sds] ")
                    '.AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3月_見込_SS_SDS入力
                    '.AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ") '計画_SDS選択時追加金額
                    '.AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4月_計画_SS_SDS入力
                    '.AppendLine("  ,NULL AS [5gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [6gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [7gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [8gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [9gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [10gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [11gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [12gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [1gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [2gatu_keikaku_ss_sds] ")
                    '.AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3月_計画_SS_SDS入力

                    .AppendLine("  ,NULL AS [4gatu_keisanyou_uri_heikin_tanka] ")       '4月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [4gatu_keisanyou_siire_heikin_tanka] ")     '4月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [5gatu_keisanyou_uri_heikin_tanka] ")       '5月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [5gatu_keisanyou_siire_heikin_tanka] ")     '5月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [6gatu_keisanyou_uri_heikin_tanka] ")       '6月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [6gatu_keisanyou_siire_heikin_tanka] ")     '6月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [7gatu_keisanyou_uri_heikin_tanka] ")       '7月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [7gatu_keisanyou_siire_heikin_tanka] ")     '7月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [8gatu_keisanyou_uri_heikin_tanka] ")       '8月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [8gatu_keisanyou_siire_heikin_tanka] ")     '8月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [9gatu_keisanyou_uri_heikin_tanka] ")       '9月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [9gatu_keisanyou_siire_heikin_tanka] ")     '9月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [10gatu_keisanyou_uri_heikin_tanka] ")      '10月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [10gatu_keisanyou_siire_heikin_tanka] ")    '10月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [11gatu_keisanyou_uri_heikin_tanka] ")      '11月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [11gatu_keisanyou_siire_heikin_tanka] ")    '11月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [12gatu_keisanyou_uri_heikin_tanka] ")      '12月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [12gatu_keisanyou_siire_heikin_tanka] ")    '12月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [1gatu_keisanyou_uri_heikin_tanka] ")       '1月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [1gatu_keisanyou_siire_heikin_tanka] ")     '1月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [2gatu_keisanyou_uri_heikin_tanka] ")       '2月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [2gatu_keisanyou_siire_heikin_tanka] ")     '2月_計算用__仕入平均単価
                    .AppendLine("  ,NULL AS [3gatu_keisanyou_uri_heikin_tanka] ")       '3月_計算用__売上平均単価
                    .AppendLine("  ,NULL AS [3gatu_keisanyou_siire_heikin_tanka] ")     '3月_計算用__仕入平均単価

            End Select

            If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.goukeiFC Then
                .AppendLine("  ,hyouji_jyun2 ")
            Else
                .AppendLine("  ,MKM2.hyouji_jyun AS hyouji_jyun2 ")
            End If


            If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.meisai OrElse SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.syoukei Then
                .AppendLine("  ,MKM.hyouji_jyun  ")
            Else
                .AppendLine("  ,NULL AS hyouji_jyun  ")
            End If

            .AppendLine("  ," & CInt(SelectKbn) & " AS data_type  ")

            '=====add select item  197から============================
            If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.goukeiFC Then
                .AppendLine("  ,syouhin_cd   ")
                .AppendLine("  , syouhin_jyun  ")
                .AppendLine("  , NULL AS kensuu_count_umu  ")
                .AppendLine("  , NULL AS keikaku_kakutei_flg  ")
                .AppendLine("  , NULL AS keikaku_huhen_flg  ")
                .AppendLine("  , NULL AS zennen_siire_heikin_tanka  ")
                .AppendLine("  , NULL AS keikaku_ss_sds_umu  ")
                .AppendLine("  , NULL AS mikomi_ss_sds_umu  ")
                .AppendLine("  , NULL AS keikaku_sds_tuika_kin_umu  ")
                .AppendLine("  , NULL AS mikomi_sds_tuika_kin_umu  ")
                .AppendLine("   FROM  ( ")
                Return sqlBuffer.ToString
            Else
                .AppendLine("  ,TKK.syouhin_cd   ")
                .AppendLine("  ,MKKS.hyouji_jyun  AS syouhin_jyun  ")
                
                If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.meisai OrElse SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.FC Then
                    .AppendLine("  ,MKKS.kensuu_count_umu   ")
                    .AppendLine("  ,TKK.keikaku_kakutei_flg   ")
                    .AppendLine("  ,TKK.keikaku_huhen_flg   ")
                    .AppendLine("  ,TJK.zennen_siire_heikin_tanka AS zennen_siire_heikin_tanka   ")
                    .AppendLine("  , NULL AS keikaku_ss_sds_umu  ")
                    .AppendLine("  , NULL AS mikomi_ss_sds_umu  ")
                    .AppendLine("  , NULL AS keikaku_sds_tuika_kin_umu  ")
                    .AppendLine("  , NULL AS mikomi_sds_tuika_kin_umu  ")
                Else
                    .AppendLine("  ,NULL  AS kensuu_count_umu   ")
                    .AppendLine("  ,NULL  AS keikaku_kakutei_flg   ")
                    .AppendLine("  ,NULL  AS keikaku_huhen_flg   ")
                    .AppendLine("  ,NULL  AS zennen_siire_heikin_tanka   ")
                    .AppendLine("  , NULL AS keikaku_ss_sds_umu  ")
                    .AppendLine("  , NULL AS mikomi_ss_sds_umu  ")
                    .AppendLine("  , NULL AS keikaku_sds_tuika_kin_umu  ")
                    .AppendLine("  , NULL AS mikomi_sds_tuika_kin_umu  ")

                End If
                .AppendLine("   FROM  ( ")
            End If
            '================================= 

            .AppendLine(SQLCommon(KeikakuKanriRecord, SelectKbn, blnCsv, paramList))
            If Not SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.FC Then
                .AppendLine(" LEFT JOIN m_keikaku_kameiten AS MKK  WITH(READCOMMITTED)   ")
                .AppendLine("  ON MKK.keikaku_nendo=TKK.keikaku_nendo  ")
                .AppendLine("  AND MKK.kameiten_cd=TKK.kameiten_cd  ")
                .AppendLine("  LEFT JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED)   ")
                .AppendLine("  ON TKK.keikaku_nendo=TJK.keikaku_nendo  ")
                .AppendLine("  AND TKK.kameiten_cd=TJK.kameiten_cd  ")
                .AppendLine("  AND TKK.syouhin_cd=TJK.keikaku_kanri_syouhin_cd  ")
                .AppendLine("  LEFT JOIN t_jisseki_kanri AS ZTJK WITH(READCOMMITTED)   ")
                .AppendLine("  ON TKK.keikaku_nendo-1=ZTJK.keikaku_nendo  ")
                .AppendLine("  AND TKK.kameiten_cd=ZTJK.kameiten_cd  ")
                .AppendLine("  AND TKK.syouhin_cd=ZTJK.keikaku_kanri_syouhin_cd  ")
                .AppendLine("  LEFT JOIN   ")
                .AppendLine("  ( SELECT ")
                .AppendLine("  t_yotei_mikomi_kanri.[4gatu_mikomi_kensuu] ")         '4月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[4gatu_mikomi_kingaku] ")        '4月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[4gatu_mikomi_arari] ")          '4月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_kensuu] ")         '5月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_kingaku] ")        '5月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_arari] ")          '5月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_kensuu] ")         '6月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_kingaku] ")        '6月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_arari] ")          '6月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_kensuu] ")         '7月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_kingaku] ")        '7月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_arari] ")          '7月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_kensuu] ")         '8月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_kingaku] ")        '8月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_arari] ")          '8月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_kensuu] ")         '9月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_kingaku] ")        '9月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_arari] ")          '9月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_kensuu] ")        '10月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_kingaku] ")       '10月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_arari] ")         '10月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_kensuu] ")        '11月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_kingaku] ")       '11月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_arari] ")         '11月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_kensuu] ")        '12月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_kingaku] ")       '12月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_arari] ")         '12月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_kensuu] ")         '1月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_kingaku] ")        '1月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_arari] ")          '1月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_kensuu] ")         '2月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_kingaku] ")        '2月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_arari] ")          '2月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_kensuu] ")         '3月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_kingaku] ")        '3月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_arari] ")          '3月_見込粗利


                .AppendLine("  ,t_yotei_mikomi_kanri.keikaku_nendo ")
                .AppendLine("  ,t_yotei_mikomi_kanri.kameiten_cd  ")
                .AppendLine("  ,t_yotei_mikomi_kanri.keikaku_kanri_syouhin_cd AS syouhin_cd")
                .AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   '見込_SDS選択時追加金額
                .AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4月_見込_SS_SDS入力
                .AppendLine("  ,NULL AS [5gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [6gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [7gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [8gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [9gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [10gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [11gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [12gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [1gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [2gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3月_見込_SS_SDS入力

                .AppendLine("  FROM (SELECT keikaku_nendo,MAX(add_datetime) AS add_datetime,kameiten_cd,keikaku_kanri_syouhin_cd  ")
                .AppendLine("  FROM t_yotei_mikomi_kanri WITH(READCOMMITTED)   ")
                .AppendLine("  GROUP BY keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd) AS t_yotei_mikomi_kanri2  ")
                .AppendLine("  LEFT JOIN t_yotei_mikomi_kanri WITH(READCOMMITTED)   ")
                .AppendLine("  ON t_yotei_mikomi_kanri.keikaku_nendo=t_yotei_mikomi_kanri2.keikaku_nendo  ")
                .AppendLine("  AND  t_yotei_mikomi_kanri.add_datetime=t_yotei_mikomi_kanri2.add_datetime  ")
                .AppendLine("  AND  t_yotei_mikomi_kanri.kameiten_cd=t_yotei_mikomi_kanri2.kameiten_cd  ")
                .AppendLine("  AND  t_yotei_mikomi_kanri.keikaku_kanri_syouhin_cd=t_yotei_mikomi_kanri2.keikaku_kanri_syouhin_cd  ")
                .AppendLine("  )AS TYMK  ")
                .AppendLine("  ON TKK.keikaku_nendo=TYMK.keikaku_nendo  ")
                .AppendLine("  AND TKK.syouhin_cd=TYMK.syouhin_cd  ")
                .AppendLine("  AND TKK.kameiten_cd=TYMK.kameiten_cd  ")
                .AppendLine("  LEFT JOIN t_nendo_hiritu_kanri AS TKJK WITH(READCOMMITTED)   ")
                .AppendLine("  ON TKK.keikaku_nendo=TKJK.keikaku_nendo  ")
                .AppendLine("  AND TKK.kameiten_cd=TKJK.kameiten_cd  ")
            End If

            .AppendLine("  LEFT JOIN m_keikaku_kanri_syouhin AS MKKS WITH(READCOMMITTED)   ")
            .AppendLine("  ON TKK.keikaku_nendo=MKKS.keikaku_nendo  ")
            .AppendLine("  AND TKK.syouhin_cd=MKKS.keikaku_kanri_syouhin_cd  ")
            If Not SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.FC Then
                .AppendLine("  LEFT JOIN m_keikakuyou_meisyou AS MKM WITH(READCOMMITTED)   ")
                .AppendLine("  ON MKM.code=MKK.eigyou_kbn  ")
                .AppendLine("  AND MKM.meisyou_syubetu='05'  ")
            End If

            .AppendLine("  LEFT JOIN m_keikakuyou_meisyou AS MKM2 WITH(READCOMMITTED)   ")
            .AppendLine("  ON MKM2.code=MKKS.bunbetu_cd  ")
            .AppendLine("  AND MKM2.meisyou_syubetu='15'  ")
            If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.FC Then

                .AppendLine(" LEFT JOIN  ")
                .AppendLine(" (SELECT MKK.keikaku_nendo,MKK.busyo_cd,TJK.keikaku_kanri_syouhin_cd AS syouhin_cd ,  ")
                .AppendLine(" CASE WHEN COUNT(TJK.keikaku_nendo)=0 THEN NULL ELSE  SUM(ISNULL(TJK.zennen_heikin_tanka,0))/COUNT(TJK.keikaku_nendo) END AS zennen_heikin_tanka,  ")
                .AppendLine(" CASE WHEN COUNT(TJK.keikaku_nendo)=0 THEN NULL ELSE SUM(ISNULL(TJK.zennen_siire_heikin_tanka,0))/COUNT(TJK.keikaku_nendo) END AS zennen_siire_heikin_tanka ")

                .AppendLine(" ,sum([4gatu_jisseki_kensuu] ) AS [4gatu_jisseki_kensuu] ") '4月_実績件数
                .AppendLine(" ,sum([4gatu_jisseki_kingaku] ) AS [4gatu_jisseki_kingaku] ") '4月_実績金額
                .AppendLine(" ,sum([4gatu_jisseki_arari] ) AS [4gatu_jisseki_arari] ") '4月_実績粗利
                .AppendLine(" ,sum([5gatu_jisseki_kensuu] ) AS [5gatu_jisseki_kensuu] ") '5月_実績件数
                .AppendLine(" ,sum([5gatu_jisseki_kingaku] ) AS [5gatu_jisseki_kingaku] ") '5月_実績金額
                .AppendLine(" ,sum([5gatu_jisseki_arari] ) AS [5gatu_jisseki_arari] ") '5月_実績粗利
                .AppendLine(" ,sum([6gatu_jisseki_kensuu] ) AS [6gatu_jisseki_kensuu] ") '6月_実績件数
                .AppendLine(" ,sum([6gatu_jisseki_kingaku] ) AS [6gatu_jisseki_kingaku] ") '6月_実績金額
                .AppendLine(" ,sum([6gatu_jisseki_arari] ) AS [6gatu_jisseki_arari] ") '6月_実績粗利
                .AppendLine(" ,sum([7gatu_jisseki_kensuu] ) AS [7gatu_jisseki_kensuu] ") '7月_実績件数
                .AppendLine(" ,sum([7gatu_jisseki_kingaku] ) AS [7gatu_jisseki_kingaku] ") '7月_実績金額
                .AppendLine(" ,sum([7gatu_jisseki_arari] ) AS [7gatu_jisseki_arari] ") '7月_実績粗利
                .AppendLine(" ,sum([8gatu_jisseki_kensuu] ) AS [8gatu_jisseki_kensuu] ") '8月_実績件数
                .AppendLine(" ,sum([8gatu_jisseki_kingaku] ) AS [8gatu_jisseki_kingaku] ") '8月_実績金額
                .AppendLine(" ,sum([8gatu_jisseki_arari] ) AS [8gatu_jisseki_arari] ") '8月_実績粗利
                .AppendLine(" ,sum([9gatu_jisseki_kensuu] ) AS [9gatu_jisseki_kensuu] ") '9月_実績件数
                .AppendLine(" ,sum([9gatu_jisseki_kingaku] ) AS [9gatu_jisseki_kingaku] ") '9月_実績金額
                .AppendLine(" ,sum([9gatu_jisseki_arari] ) AS [9gatu_jisseki_arari] ") '9月_実績粗利
                .AppendLine(" ,sum([10gatu_jisseki_kensuu] ) AS [10gatu_jisseki_kensuu] ") '10月_実績件数
                .AppendLine(" ,sum([10gatu_jisseki_kingaku] ) AS [10gatu_jisseki_kingaku] ") '10月_実績金額
                .AppendLine(" ,sum([10gatu_jisseki_arari] ) AS [10gatu_jisseki_arari] ") '10月_実績粗利
                .AppendLine(" ,sum([11gatu_jisseki_kensuu] ) AS [11gatu_jisseki_kensuu] ") '11月_実績件数
                .AppendLine(" ,sum([11gatu_jisseki_kingaku] ) AS [11gatu_jisseki_kingaku] ") '11月_実績金額
                .AppendLine(" ,sum([11gatu_jisseki_arari] ) AS [11gatu_jisseki_arari] ") '11月_実績粗利
                .AppendLine(" ,sum([12gatu_jisseki_kensuu] ) AS [12gatu_jisseki_kensuu] ") '12月_実績件数
                .AppendLine(" ,sum([12gatu_jisseki_kingaku] ) AS [12gatu_jisseki_kingaku] ") '12月_実績金額
                .AppendLine(" ,sum([12gatu_jisseki_arari] ) AS [12gatu_jisseki_arari] ") '12月_実績粗利
                .AppendLine(" ,sum([1gatu_jisseki_kensuu] ) AS [1gatu_jisseki_kensuu] ") '1月_実績件数
                .AppendLine(" ,sum([1gatu_jisseki_kingaku] ) AS [1gatu_jisseki_kingaku] ") '1月_実績金額
                .AppendLine(" ,sum([1gatu_jisseki_arari] ) AS [1gatu_jisseki_arari] ") '1月_実績粗利
                .AppendLine(" ,sum([2gatu_jisseki_kensuu] ) AS [2gatu_jisseki_kensuu] ") '2月_実績件数
                .AppendLine(" ,sum([2gatu_jisseki_kingaku] ) AS [2gatu_jisseki_kingaku] ") '2月_実績金額
                .AppendLine(" ,sum([2gatu_jisseki_arari] ) AS [2gatu_jisseki_arari] ") '2月_実績粗利
                .AppendLine(" ,sum([3gatu_jisseki_kensuu] ) AS [3gatu_jisseki_kensuu] ") '3月_実績件数
                .AppendLine(" ,sum([3gatu_jisseki_kingaku] ) AS [3gatu_jisseki_kingaku] ") '3月_実績金額
                .AppendLine(" ,sum([3gatu_jisseki_arari] ) AS [3gatu_jisseki_arari] ") '3月_実績粗利
                .AppendLine("  FROM m_keikaku_kameiten AS MKK  WITH(READCOMMITTED)   ")
                .AppendLine(" LEFT JOIN t_jisseki_kanri AS TJK  WITH(READCOMMITTED)   ")
                .AppendLine(" ON TJK.keikaku_nendo=MKK.keikaku_nendo  ")
                .AppendLine(" AND TJK.kameiten_cd=MKK.kameiten_cd  ")
                .AppendLine(" WHERE  MKK.eigyou_kbn='4'  ")
                .AppendLine(" GROUP BY MKK.keikaku_nendo,MKK.busyo_cd,TJK.keikaku_kanri_syouhin_cd) AS TJK  ")
                .AppendLine(" ON TJK.keikaku_nendo=TKK.keikaku_nendo  ")
                .AppendLine(" AND TJK.busyo_cd=TKK.busyo_cd  ")
                .AppendLine(" AND TKK.syouhin_cd=TJK.syouhin_cd  ")
                '.AppendLine("   WHERE TKK.keikaku_nendo='2012'  ")
                '.AppendLine("   AND TKK.busyo_cd='0202'  ")
                .AppendLine("   WHERE TKK.keikaku_nendo=@keikaku_nendo1   ")
                .AppendLine("   AND TKK.busyo_cd=@busyo_cd1  ")
                paramList.Add(MakeParam("@keikaku_nendo1", SqlDbType.Char, 4, KeikakuKanriRecord.Nendo))
                paramList.Add(MakeParam("@busyo_cd1", SqlDbType.Char, 4, KeikakuKanriRecord.Shiten))
            End If
        End With


        Return sqlBuffer.ToString
    End Function
    ''' <summary>
    ''' SQLの結合と検索条件の共通処理
    ''' </summary>
    ''' <param name="KeikakuKanriRecord">検索条件</param>
    ''' <param name="SelectKbn">明細部SQL区分</param>
    ''' <param name="paramList">パラメータの設定</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SQLCommon(ByVal KeikakuKanriRecord As KeikakuKanriRecord, ByVal SelectKbn As KeikakuKanriRecord.selectKbn, ByVal blnCsv As Boolean, ByRef paramList As List(Of SqlClient.SqlParameter)) As String
        Dim sqlEigyou As New System.Text.StringBuilder
        Dim sqlSiborikomi As New System.Text.StringBuilder
        Dim intSuu As Integer = 0
        Dim strTmp As String = ""
        Dim strTmp2 As String = ""
        Dim sqlBuffer As New StringBuilder
        Dim sqlEXISTS As New StringBuilder
        Dim sqlEXISTS2 As New StringBuilder
        If CInt(SelectKbn) < SelectKbn.FC Then
            With sqlBuffer


                .AppendLine("SELECT ")
              

                .AppendLine(" keikaku_nenkan_tousuu,busyo_cd ")
                .AppendLine(" ,keikaku_nendo ")
                .AppendLine(" ,kameiten_cd ")
                .AppendLine(" ,todouhuken_cd ")
                .AppendLine(" ,eigyou_tantousya_id ")

                .AppendLine(" FROM m_keikaku_kameiten  AS MKK WITH(READCOMMITTED)  ")
                ' .AppendLine(" WHERE  (MKK.kameiten_cd='10006'  or MKK.kameiten_cd='10007')  and   MKK.keikaku_nendo='2012'  ")
                .AppendLine(" WHERE  1=1  ")

                '年度
                strTmp = KeikakuKanriRecord.Nendo
                If strTmp <> "" Then
                    .AppendLine(" AND MKK.keikaku_nendo=@keikaku_nendo  ")
                    paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, KeikakuKanriRecord.Nendo))
                End If
                If Not (SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.FC OrElse SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.goukeiFC) Then

                    '支店(部署)
                    strTmp = KeikakuKanriRecord.Shiten
                    If strTmp <> "" Then
                        .AppendLine(" AND MKK.busyo_cd=@shiten_mei  ")
                        paramList.Add(MakeParam("@shiten_mei", SqlDbType.VarChar, 4, strTmp))
                    End If
                    'ユーザー(営業マン)
                    strTmp = KeikakuKanriRecord.User
                    If strTmp <> "" Then
                        .AppendLine(" AND MKK.eigyou_tantousya_id=@eigyou_tantousya_mei  ")
                        paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 30, strTmp))
                    End If
                    '加盟店(登録事業者)
                    strTmp = KeikakuKanriRecord.Kameiten
                    If strTmp <> "" Then
                        .AppendLine(" AND MKK.kameiten_cd=@kameiten_mei  ")
                        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 8, strTmp))
                    End If

                    '営業所
                    strTmp = KeikakuKanriRecord.Eigyou
                    If strTmp <> "" Then
                        .AppendLine(" AND MKK.eigyousyo_busyo_cd=@eigyousyo_mei  ")
                        paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 5, strTmp))
                    End If

                End If
            End With

            '営業区分
            strTmp = sqlBuffer.ToString
            sqlBuffer.Remove(0, sqlBuffer.Length)
            If KeikakuKanriRecord.EigyouKBN.Eigyou OrElse KeikakuKanriRecord.EigyouKBN.EigyouNew Then '営業
                sqlEigyou.AppendLine(strTmp)
                sqlEigyou.AppendLine(" AND MKK.eigyou_kbn=@Eigyou  ")
                paramList.Add(MakeParam("@Eigyou", SqlDbType.Char, 1, "1"))

                If (KeikakuKanriRecord.EigyouKBN.Eigyou) AndAlso (Not KeikakuKanriRecord.EigyouKBN.EigyouNew) Then
                    '営業
                    sqlEigyou.AppendLine(" AND ISNULL(MKK.kameiten_sinki_flg,0)=@sinki0  ")
                ElseIf (Not KeikakuKanriRecord.EigyouKBN.Eigyou) AndAlso (KeikakuKanriRecord.EigyouKBN.EigyouNew) Then
                    '営業(新規)
                    sqlEigyou.AppendLine(" AND ISNULL(MKK.kameiten_sinki_flg,0)=@sinki1  ")
                End If

                '絞込み選択
                intSuu = 0
                If KeikakuKanriRecord.Siborikomi.KeikakuTi Then '計画値0
                    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                    sqlSiborikomi.AppendLine(" AND MKK.keikaku0_flg=@KeikakuTi  ")

                    intSuu = intSuu + 1
                End If
                'If KeikakuKanriRecord.Siborikomi.SinkiTouroku Then '新規登録事業所
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.kameiten_sinki_flg=@SinkiTouroku  ")

                '    intSuu = intSuu + 1
                'End If
                'If KeikakuKanriRecord.Siborikomi.Bunjyou Then '分譲50社
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.bunjyou_50flg=@Bunjyou  ")

                '    intSuu = intSuu + 1
                'End If

                'If KeikakuKanriRecord.Siborikomi.Tyumon Then '注文50社
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.tyuumon_50flg=@Tyumon  ")

                '    intSuu = intSuu + 1
                'End If
                If intSuu > 0 Then
                    strTmp2 = strTmp2 & sqlSiborikomi.ToString
                Else
                    strTmp2 = strTmp2 & sqlEigyou.ToString
                End If

            End If

            sqlEigyou.Remove(0, sqlEigyou.Length)
            sqlSiborikomi.Remove(0, sqlSiborikomi.Length)
            If KeikakuKanriRecord.EigyouKBN.Tokuhan OrElse KeikakuKanriRecord.EigyouKBN.TokuhanNew Then '特販

                sqlEigyou.AppendLine(strTmp)
                sqlEigyou.AppendLine(" AND MKK.eigyou_kbn=@Tokuhan  ")
                paramList.Add(MakeParam("@Tokuhan", SqlDbType.Char, 1, "3"))

                If (KeikakuKanriRecord.EigyouKBN.Tokuhan) AndAlso (Not KeikakuKanriRecord.EigyouKBN.TokuhanNew) Then
                    '特販
                    sqlEigyou.AppendLine(" AND ISNULL(MKK.kameiten_sinki_flg,0)=@sinki0  ")
                ElseIf (Not KeikakuKanriRecord.EigyouKBN.Tokuhan) AndAlso (KeikakuKanriRecord.EigyouKBN.TokuhanNew) Then
                    '特販(新規)
                    sqlEigyou.AppendLine(" AND ISNULL(MKK.kameiten_sinki_flg,0)=@sinki1  ")
                End If

                '絞込み選択
                intSuu = 0
                If KeikakuKanriRecord.Siborikomi.KeikakuTi Then '計画値0
                    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                    sqlSiborikomi.AppendLine(" AND MKK.keikaku0_flg=@KeikakuTi  ")

                    intSuu = intSuu + 1
                End If
                'If KeikakuKanriRecord.Siborikomi.SinkiTouroku Then '新規登録事業所
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.kameiten_sinki_flg=@SinkiTouroku  ")

                '    intSuu = intSuu + 1
                'End If
                'If KeikakuKanriRecord.Siborikomi.Bunjyou Then '分譲50社
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.bunjyou_50flg=@Bunjyou  ")

                '    intSuu = intSuu + 1
                'End If

                'If KeikakuKanriRecord.Siborikomi.Tyumon Then '注文50社
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.tyuumon_50flg=@Tyumon  ")

                '    intSuu = intSuu + 1
                'End If
                If intSuu > 0 Then
                    If strTmp2.Trim <> "" Then
                        strTmp2 = strTmp2 & " UNION " & vbCrLf & sqlSiborikomi.ToString
                    Else
                        strTmp2 = strTmp2 & sqlSiborikomi.ToString
                    End If
                Else
                    If strTmp2.Trim <> "" Then
                        strTmp2 = strTmp2 & " UNION " & vbCrLf & sqlEigyou.ToString
                    Else
                        strTmp2 = strTmp2 & sqlEigyou.ToString
                    End If
                End If

            End If

            paramList.Add(MakeParam("@sinki0", SqlDbType.Int, 1, 0))
            paramList.Add(MakeParam("@sinki1", SqlDbType.Int, 1, 1))

            'sqlEigyou.Remove(0, sqlEigyou.Length)
            'sqlSiborikomi.Remove(0, sqlSiborikomi.Length)
            'If KeikakuKanriRecord.EigyouKBN.Sinki Then '新規
            '    sqlEigyou.AppendLine(strTmp)
            '    sqlEigyou.AppendLine(" AND MKK.eigyou_kbn=@Sinki  ")
            '    paramList.Add(MakeParam("@Sinki", SqlDbType.Char, 1, "2"))

            '    '絞込み選択
            '    intSuu = 0
            '    If KeikakuKanriRecord.Siborikomi.KeikakuTi Then '計画値0
            '        sqlSiborikomi.AppendLine(sqlEigyou.ToString)
            '        sqlSiborikomi.AppendLine(" AND MKK.keikaku0_flg=@KeikakuTi  ")

            '        intSuu = intSuu + 1
            '    End If
            '    If KeikakuKanriRecord.Siborikomi.SinkiTouroku Then '新規登録事業所
            '        If intSuu <> 0 Then
            '            sqlSiborikomi.AppendLine(" UNION ")
            '        End If
            '        sqlSiborikomi.AppendLine(sqlEigyou.ToString)
            '        sqlSiborikomi.AppendLine(" AND MKK.kameiten_sinki_flg=@SinkiTouroku  ")

            '        intSuu = intSuu + 1
            '    End If
            '    If KeikakuKanriRecord.Siborikomi.Bunjyou Then '分譲50社
            '        If intSuu <> 0 Then
            '            sqlSiborikomi.AppendLine(" UNION ")
            '        End If
            '        sqlSiborikomi.AppendLine(sqlEigyou.ToString)
            '        sqlSiborikomi.AppendLine(" AND MKK.bunjyou_50flg=@Bunjyou  ")

            '        intSuu = intSuu + 1
            '    End If

            '    If KeikakuKanriRecord.Siborikomi.Tyumon Then '注文50社
            '        If intSuu <> 0 Then
            '            sqlSiborikomi.AppendLine(" UNION ")
            '        End If
            '        sqlSiborikomi.AppendLine(sqlEigyou.ToString)
            '        sqlSiborikomi.AppendLine(" AND MKK.tyuumon_50flg=@Tyumon  ")

            '        intSuu = intSuu + 1
            '    End If
            '    If intSuu > 0 Then
            '        If strTmp2.Trim <> "" Then
            '            strTmp2 = strTmp2 & " UNION " & vbCrLf & sqlSiborikomi.ToString
            '        Else
            '            strTmp2 = strTmp2 & sqlSiborikomi.ToString
            '        End If
            '    Else
            '        If strTmp2.Trim <> "" Then
            '            strTmp2 = strTmp2 & " UNION " & vbCrLf & sqlEigyou.ToString
            '        Else
            '            strTmp2 = strTmp2 & sqlEigyou.ToString
            '        End If
            '    End If

            'End If
            If strTmp2 <> "" Then
                strTmp = strTmp2
            End If

            Dim temp As New StringBuilder
            temp.Remove(0, temp.Length)
            With temp
                .AppendLine("SELECT ")
                .AppendLine("	SUB_MKK.keikaku_nenkan_tousuu ")
                .AppendLine("	,SUB_MKK.busyo_cd ")
                .AppendLine("	,SUB_MKK.keikaku_nendo ")
                .AppendLine("	,SUB_MKK.kameiten_cd ")
                .AppendLine("	,MKKI.keikakuyou_nenkan_tousuu ") '--計画用_年間棟数 ")
                .AppendLine("	,MKKI.sds_kaisi_nengetu ") '--SDS開始年月 ")
                .AppendLine("FROM ")
                .AppendLine("	( ")
                .AppendLine(strTmp)
                .AppendLine("	) AS SUB_MKK ")
                .AppendLine("	LEFT JOIN")
                .AppendLine("	m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED) ") '--計画管理_加盟店情報ﾏｽﾀ ")
                .AppendLine("		ON ")
                .AppendLine("			SUB_MKK.kameiten_cd = MKKI.kameiten_cd ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_todoufuken AS MT WITH(READCOMMITTED) ") '--都道府県ﾏｽﾀ ")
                .AppendLine("		ON ")
                .AppendLine("			SUB_MKK.todouhuken_cd = MT.todouhuken_cd ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_eigyou_tantousya_syozoku_info AS METSI WITH(READCOMMITTED) ") '--営業担当者_所属情報ﾏｽﾀ ")
                .AppendLine("		ON ")
                .AppendLine("			SUB_MKK.eigyou_tantousya_id = METSI.eigyou_tantousya_id ")
                .AppendLine("WHERE ")
                .AppendLine("	1 = 1 ")
                If Not KeikakuKanriRecord.Syozoku.Equals(String.Empty) Then
                    .AppendLine("	AND METSI.syozoku_cd = @syozoku_cd ") '--所属 ")
                    paramList.Add(MakeParam("@syozoku_cd", SqlDbType.Int, 10, KeikakuKanriRecord.Syozoku)) '所属
                End If
                If Not KeikakuKanriRecord.Todoufuken.Equals(String.Empty) Then
                    .AppendLine("	AND MT.todouhuken_cd = @todouhuken_cd ") '--都道府県 ")
                    paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, KeikakuKanriRecord.Todoufuken)) '都道府県
                End If
                If Not KeikakuKanriRecord.TouituHoujin.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.touitsu_houjin_cd = @touitsu_houjin_cd ") '--統一法人 ")
                    paramList.Add(MakeParam("@touitsu_houjin_cd", SqlDbType.VarChar, 8, KeikakuKanriRecord.TouituHoujin)) '統一法人
                End If
                If Not KeikakuKanriRecord.Houjin.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.houjin_cd = @houjin_cd ") '--法人 ")
                    paramList.Add(MakeParam("@houjin_cd", SqlDbType.VarChar, 8, KeikakuKanriRecord.Houjin)) '統一法人
                End If
                If Not KeikakuKanriRecord.Zokusei1.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_1 = @kameiten_zokusei_1 ") '--属性1 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei1)) '属性1
                End If
                If Not KeikakuKanriRecord.Zokusei2.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_2 = @kameiten_zokusei_2 ") '--属性2 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei2)) '属性2
                End If
                If Not KeikakuKanriRecord.Zokusei3.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_3 = @kameiten_zokusei_3 ") '--属性3 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei3)) '属性3
                End If
                If Not KeikakuKanriRecord.Zokusei4.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_4 = @kameiten_zokusei_4 ") '--属性4 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei4)) '属性4
                End If
                If Not KeikakuKanriRecord.Zokusei5.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_5 = @kameiten_zokusei_5 ") '--属性5 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei5)) '属性5
                End If
                If Not KeikakuKanriRecord.Zokusei6.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_6 = @kameiten_zokusei_6 ") '--属性6 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, KeikakuKanriRecord.Zokusei6)) '属性6
                End If
                If (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) OrElse (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                    If KeikakuKanriRecord.NenkanTousuuHani.Keikakuyou Then
                        '計画用
                        If (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) AndAlso (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                            'fromとto
                            .AppendLine("	AND MKKI.keikakuyou_nenkan_tousuu BETWEEN @keikakuyou_nenkan_tousuu_from AND @keikakuyou_nenkan_tousuu_to ") '--計画用_年間棟数 ")
                        ElseIf (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) AndAlso (KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                            'fromのみ
                            .AppendLine("	AND MKKI.keikakuyou_nenkan_tousuu >= @keikakuyou_nenkan_tousuu_from  ") '--計画用_年間棟数 ")
                        End If
                    Else
                        '非計画用
                        If (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) AndAlso (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                            'fromとto
                            .AppendLine("	AND SUB_MKK.keikaku_nenkan_tousuu BETWEEN @keikakuyou_nenkan_tousuu_from AND @keikakuyou_nenkan_tousuu_to ") '--年間棟数 ")
                        ElseIf (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) AndAlso (KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                            'fromのみ
                            .AppendLine("	AND SUB_MKK.keikaku_nenkan_tousuu >= @keikakuyou_nenkan_tousuu_from  ") '--年間棟数 ")
                        End If
                    End If
                    If Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty) Then
                        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom)) '年間棟数From
                    Else
                        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, DBNull.Value)) '年間棟数From
                    End If
                    If Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty) Then
                        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_to", SqlDbType.Int, 10, KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo)) '年間棟数To
                    Else
                        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_to", SqlDbType.Int, 10, DBNull.Value)) '年間棟数To
                    End If

                End If

            End With

            strTmp = temp.ToString()

            With sqlBuffer

                .AppendLine("  SELECT  MKK.busyo_cd,TKK.keikaku_nendo,MKK.kameiten_cd,TKK.keikaku_kanri_syouhin_cd, MKK.keikakuyou_nenkan_tousuu, MKK.sds_kaisi_nengetu   ")
                .AppendLine("  FROM    ")
                If blnCsv Then
                    .AppendLine("   (   ")

                    .AppendLine("    SELECT keikaku_nendo,null as kameiten_cd,keikaku_kanri_syouhin_cd FROM m_keikaku_kanri_syouhin  WITH(READCOMMITTED)      ")
                Else
                    .AppendLine("   (SELECT keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd FROM t_keikaku_kanri  WITH(READCOMMITTED)    ")
                    .AppendLine("   UNION ")
                    .AppendLine("   SELECT keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd FROM t_jisseki_kanri  WITH(READCOMMITTED)    ")
                    .AppendLine("   UNION ")
                    .AppendLine("   SELECT keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd FROM t_yotei_mikomi_kanri  WITH(READCOMMITTED)    ")
                End If
                .AppendLine("   )  AS TKK   ")
                If Not KeikakuKanriRecord.Siborikomi.NenkanTouSuu Then
                    sqlEXISTS.AppendLine("  LEFT JOIN t_nendo_hiritu_kanri AS TKJK WITH(READCOMMITTED)   ")
                    sqlEXISTS.AppendLine("   ON MKK.keikaku_nendo=TKJK.keikaku_nendo  ")
                    sqlEXISTS.AppendLine("   AND MKK.kameiten_cd=TKJK.kameiten_cd    ")
                Else

                End If
                sqlEXISTS.AppendLine(" WHERE EXISTS  ")
                sqlEXISTS.AppendLine(" ( SELECT keikaku_nendo+kameiten_cd   FROM  ")
                sqlEXISTS.AppendLine("   (        ")
                sqlEXISTS.AppendLine(" SELECT keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd FROM t_keikaku_kanri  WITH(READCOMMITTED)   ")
                sqlEXISTS.AppendLine(" UNION ")
                sqlEXISTS.AppendLine("  SELECT keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd FROM t_jisseki_kanri  WITH(READCOMMITTED) ")
                sqlEXISTS.AppendLine(" UNION ")
                sqlEXISTS.AppendLine("  SELECT keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd FROM t_yotei_mikomi_kanri  WITH(READCOMMITTED)   ")
                sqlEXISTS.AppendLine("  )  AS TKK WHERE keikaku_nendo= MKK.keikaku_nendo AND kameiten_cd=MKK.kameiten_cd  )    ")

                If KeikakuKanriRecord.Kensuu <> "" Then
                    If Not KeikakuKanriRecord.Siborikomi.NenkanTouSuu Then
                        sqlEXISTS2.AppendLine("   ORDER BY TKJK.uri_hiritu DESC ")
                    Else
                        sqlEXISTS2.AppendLine("   ORDER BY MKK.keikaku_nenkan_tousuu DESC  ")
                    End If
                    If blnCsv Then
                        .AppendLine("    RIGHT JOIN  ( select  TOP  " & KeikakuKanriRecord.Kensuu & " MKK.busyo_cd, MKK.keikaku_nendo, MKK.kameiten_cd, MKK.keikakuyou_nenkan_tousuu, MKK.sds_kaisi_nengetu FROM ( " & strTmp & ") AS MKK " & sqlEXISTS.ToString & sqlEXISTS2.ToString & " ) AS MKK ")
                        .AppendLine("    ON TKK.keikaku_nendo=MKK.keikaku_nendo    ")

                    Else
                        .AppendLine("    INNER JOIN ( select  TOP  " & KeikakuKanriRecord.Kensuu & "  MKK.busyo_cd,MKK.keikaku_nendo ,MKK.kameiten_cd, MKK.keikakuyou_nenkan_tousuu, MKK.sds_kaisi_nengetu FROM (  " & strTmp & ") AS MKK " & " ) AS MKK ")
                        .AppendLine("    ON TKK.keikaku_nendo=MKK.keikaku_nendo    ")
                        .AppendLine("    AND TKK.kameiten_cd=MKK.kameiten_cd   ")
                    End If

                Else
                    If blnCsv Then
                        .AppendLine("    RIGHT JOIN  ( select MKK.busyo_cd,MKK.keikaku_nendo ,MKK.kameiten_cd, MKK.keikakuyou_nenkan_tousuu, MKK.sds_kaisi_nengetu FROM ( " & strTmp & ") AS MKK " & sqlEXISTS.ToString & sqlEXISTS2.ToString & " ) AS MKK ")
                        .AppendLine("    ON TKK.keikaku_nendo=MKK.keikaku_nendo    ")

                    Else
                        .AppendLine("    INNER JOIN ( select MKK.busyo_cd,MKK.keikaku_nendo ,MKK.kameiten_cd, MKK.keikakuyou_nenkan_tousuu, MKK.sds_kaisi_nengetu FROM (  " & strTmp & ") AS MKK " & " ) AS MKK ")
                        .AppendLine("    ON TKK.keikaku_nendo=MKK.keikaku_nendo    ")
                        .AppendLine("    AND TKK.kameiten_cd=MKK.kameiten_cd   ")
                    End If
                End If



            End With
            strCountSQL = "select COUNT(MKK.kameiten_cd)  FROM ( " & strTmp & ") AS MKK " & sqlEXISTS.ToString

            paramList.Add(MakeParam("@KeikakuTi", SqlDbType.Char, 1, "1"))
            paramList.Add(MakeParam("@SinkiTouroku", SqlDbType.Char, 1, "1"))
            paramList.Add(MakeParam("@Bunjyou", SqlDbType.Char, 1, "1"))
            paramList.Add(MakeParam("@Tyumon", SqlDbType.Char, 1, "1"))

            strTmp = sqlBuffer.ToString
            sqlBuffer.Remove(0, sqlBuffer.Length)
            With sqlBuffer
                .AppendLine("  SELECT   ")
                .AppendLine("    t_keikaku_kanri2.busyo_cd,t_keikaku_kanri2.keikaku_nendo,t_keikaku_kanri2.kameiten_cd,t_keikaku_kanri2.keikaku_kanri_syouhin_cd, t_keikaku_kanri2.keikakuyou_nenkan_tousuu ,t_keikaku_kanri2.sds_kaisi_nengetu FROM   ")
            End With
            strTmp = sqlBuffer.ToString & "( " & strTmp & " GROUP BY  MKK.busyo_cd,TKK.keikaku_nendo,MKK.kameiten_cd,TKK.keikaku_kanri_syouhin_cd,MKK.keikakuyou_nenkan_tousuu,MKK.sds_kaisi_nengetu) AS t_keikaku_kanri2"
            sqlBuffer.Remove(0, sqlBuffer.Length)
            With sqlBuffer
                .AppendLine("  SELECT   ")
                .AppendLine("  t_keikaku_kanri.[4gatu_keisanyou_koj_hantei_ritu] ")      '4月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_koj_jyuchuu_ritu] ")     '4月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_tyoku_koj_ritu] ")       '4月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_koj_hantei_ritu] ")      '5月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_koj_jyuchuu_ritu] ")     '5月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_tyoku_koj_ritu] ")       '5月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_koj_hantei_ritu] ")      '6月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_koj_jyuchuu_ritu] ")     '6月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_tyoku_koj_ritu] ")       '6月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_koj_hantei_ritu] ")      '7月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_koj_jyuchuu_ritu] ")     '7月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_tyoku_koj_ritu] ")       '7月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_koj_hantei_ritu] ")      '8月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_koj_jyuchuu_ritu] ")     '8月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_tyoku_koj_ritu] ")       '8月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_koj_hantei_ritu] ")      '9月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_koj_jyuchuu_ritu] ")     '9月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_tyoku_koj_ritu] ")       '9月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_koj_hantei_ritu] ")     '10月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_koj_jyuchuu_ritu] ")    '10月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_tyoku_koj_ritu] ")      '10月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_koj_hantei_ritu] ")     '11月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_koj_jyuchuu_ritu] ")    '11月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_tyoku_koj_ritu] ")      '11月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_koj_hantei_ritu] ")     '12月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_koj_jyuchuu_ritu] ")    '12月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_tyoku_koj_ritu] ")      '12月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_koj_hantei_ritu] ")      '1月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_koj_jyuchuu_ritu] ")     '1月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_tyoku_koj_ritu] ")       '1月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_koj_hantei_ritu] ")      '2月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_koj_jyuchuu_ritu] ")     '2月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_tyoku_koj_ritu] ")       '2月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_koj_hantei_ritu] ")      '3月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_koj_jyuchuu_ritu] ")     '3月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_tyoku_koj_ritu] ")       '3月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_kensuu] ")         '4月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_kingaku] ")        '4月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_arari] ")          '4月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_kensuu] ")         '5月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_kingaku] ")        '5月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_arari] ")          '5月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_kensuu] ")         '6月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_kingaku] ")        '6月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_arari] ")          '6月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_kensuu] ")         '7月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_kingaku] ")        '7月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_arari] ")          '7月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_kensuu] ")         '8月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_kingaku] ")        '8月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_arari] ")          '8月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_kensuu] ")         '9月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_kingaku] ")        '9月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_arari] ")          '9月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_kensuu] ")        '10月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_kingaku] ")       '10月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_arari] ")         '10月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_kensuu] ")        '11月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_kingaku] ")       '11月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_arari] ")         '11月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_kensuu] ")        '12月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_kingaku] ")       '12月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_arari] ")         '12月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_kensuu] ")         '1月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_kingaku] ")        '1月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_arari] ")          '1月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_kensuu] ")         '2月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_kingaku] ")        '2月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_arari] ")          '2月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_kensuu] ")         '3月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_kingaku] ")        '3月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_arari] ")          '3月_計画粗利
                .AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ")  '計画_SDS選択時追加金額
                .AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4月_計画_SS_SDS入力
                .AppendLine("  ,NULL AS [5gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [6gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [7gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [8gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [9gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [10gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [11gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [12gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [1gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [2gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3月_計画_SS_SDS入力

                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_uri_heikin_tanka] ")       '4月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_siire_heikin_tanka] ")     '4月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_uri_heikin_tanka] ")       '5月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_siire_heikin_tanka] ")     '5月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_uri_heikin_tanka] ")       '6月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_siire_heikin_tanka] ")     '6月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_uri_heikin_tanka] ")       '7月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_siire_heikin_tanka] ")     '7月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_uri_heikin_tanka] ")       '8月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_siire_heikin_tanka] ")     '8月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_uri_heikin_tanka] ")       '9月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_siire_heikin_tanka] ")     '9月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_uri_heikin_tanka] ")      '10月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_siire_heikin_tanka] ")    '10月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_uri_heikin_tanka] ")      '11月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_siire_heikin_tanka] ")    '11月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_uri_heikin_tanka] ")      '12月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_siire_heikin_tanka] ")    '12月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_uri_heikin_tanka] ")       '1月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_siire_heikin_tanka] ")     '1月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_uri_heikin_tanka] ")       '2月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_siire_heikin_tanka] ")     '2月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_uri_heikin_tanka] ")       '3月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_siire_heikin_tanka] ")     '3月_計算用__仕入平均単価

                .AppendLine("  ,t_keikaku_kanri2.keikakuyou_nenkan_tousuu ")    '計画用_年間棟数
                .AppendLine("  ,t_keikaku_kanri2.sds_kaisi_nengetu ")           'SDS開始年月

                .AppendLine(" ,t_keikaku_kanri.keikaku_kakutei_flg,t_keikaku_kanri.keikaku_huhen_flg,t_keikaku_kanri2.keikaku_kanri_syouhin_cd AS  syouhin_cd ,t_keikaku_kanri2.keikaku_nendo,t_keikaku_kanri3.add_datetime,t_keikaku_kanri2.kameiten_cd,t_keikaku_kanri2.busyo_cd FROM ( ")
                .AppendLine(strTmp)
                .AppendLine(" )  AS  t_keikaku_kanri2 ")
                .AppendLine("  LEFT JOIN ( SELECT keikaku_nendo,MAX(add_datetime) AS add_datetime,kameiten_cd   ,keikaku_kanri_syouhin_cd  ")
                .AppendLine("   FROM t_keikaku_kanri WITH(READCOMMITTED)      GROUP BY keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd) AS t_keikaku_kanri3  ")
                .AppendLine("    ON t_keikaku_kanri2.keikaku_nendo=t_keikaku_kanri3.keikaku_nendo    ")
                .AppendLine("    AND  t_keikaku_kanri2.kameiten_cd=t_keikaku_kanri3.kameiten_cd ")
                .AppendLine("      AND  t_keikaku_kanri2.keikaku_kanri_syouhin_cd=t_keikaku_kanri3.keikaku_kanri_syouhin_cd  ")
                .AppendLine("    LEFT JOIN t_keikaku_kanri WITH(READCOMMITTED)    ")
                .AppendLine("    ON  ")
                .AppendLine("    t_keikaku_kanri3.keikaku_nendo=t_keikaku_kanri.keikaku_nendo  ")
                .AppendLine("    AND t_keikaku_kanri3.add_datetime=t_keikaku_kanri.add_datetime  ")
                .AppendLine("    AND t_keikaku_kanri3.keikaku_kanri_syouhin_cd=t_keikaku_kanri.keikaku_kanri_syouhin_cd  ")
                .AppendLine("    AND t_keikaku_kanri3.kameiten_cd=t_keikaku_kanri.kameiten_cd ) AS TKK   ")


            End With
        Else
            With sqlBuffer

                .AppendLine("  SELECT t_keikaku_kanri.[4gatu_keisanyou_koj_hantei_ritu] ")      '4月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_koj_jyuchuu_ritu] ")     '4月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_tyoku_koj_ritu] ")       '4月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_koj_hantei_ritu] ")      '5月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_koj_jyuchuu_ritu] ")     '5月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_tyoku_koj_ritu] ")       '5月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_koj_hantei_ritu] ")      '6月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_koj_jyuchuu_ritu] ")     '6月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_tyoku_koj_ritu] ")       '6月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_koj_hantei_ritu] ")      '7月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_koj_jyuchuu_ritu] ")     '7月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_tyoku_koj_ritu] ")       '7月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_koj_hantei_ritu] ")      '8月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_koj_jyuchuu_ritu] ")     '8月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_tyoku_koj_ritu] ")       '8月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_koj_hantei_ritu] ")      '9月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_koj_jyuchuu_ritu] ")     '9月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_tyoku_koj_ritu] ")       '9月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_koj_hantei_ritu] ")     '10月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_koj_jyuchuu_ritu] ")    '10月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_tyoku_koj_ritu] ")      '10月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_koj_hantei_ritu] ")     '11月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_koj_jyuchuu_ritu] ")    '11月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_tyoku_koj_ritu] ")      '11月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_koj_hantei_ritu] ")     '12月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_koj_jyuchuu_ritu] ")    '12月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_tyoku_koj_ritu] ")      '12月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_koj_hantei_ritu] ")      '1月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_koj_jyuchuu_ritu] ")     '1月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_tyoku_koj_ritu] ")       '1月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_koj_hantei_ritu] ")      '2月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_koj_jyuchuu_ritu] ")     '2月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_tyoku_koj_ritu] ")       '2月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_koj_hantei_ritu] ")      '3月_計算用_工事判定率
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_koj_jyuchuu_ritu] ")     '3月_計算用_工事受注率
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_tyoku_koj_ritu] ")       '3月_計算用_直工事率
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_kensuu] ")         '4月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_kingaku] ")        '4月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_arari] ")          '4月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_kensuu] ")         '5月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_kingaku] ")        '5月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_arari] ")          '5月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_kensuu] ")         '6月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_kingaku] ")        '6月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_arari] ")          '6月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_kensuu] ")         '7月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_kingaku] ")        '7月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_arari] ")          '7月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_kensuu] ")         '8月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_kingaku] ")        '8月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_arari] ")          '8月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_kensuu] ")         '9月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_kingaku] ")        '9月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_arari] ")          '9月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_kensuu] ")        '10月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_kingaku] ")       '10月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_arari] ")         '10月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_kensuu] ")        '11月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_kingaku] ")       '11月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_arari] ")         '11月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_kensuu] ")        '12月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_kingaku] ")       '12月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_arari] ")         '12月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_kensuu] ")         '1月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_kingaku] ")        '1月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_arari] ")          '1月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_kensuu] ")         '2月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_kingaku] ")        '2月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_arari] ")          '2月_計画粗利
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_kensuu] ")         '3月_計画件数
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_kingaku] ")        '3月_計画金額
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_arari] ")          '3月_計画粗利
                .AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ")  '計画_SDS選択時追加金額
                .AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4月_計画_SS_SDS入力
                .AppendLine("  ,NULL AS [5gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [6gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [7gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [8gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [9gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [10gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [11gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [12gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [1gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [2gatu_keikaku_ss_sds] ")
                .AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3月_計画_SS_SDS入力

                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_uri_heikin_tanka] ")       '4月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_siire_heikin_tanka] ")     '4月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_uri_heikin_tanka] ")       '5月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_siire_heikin_tanka] ")     '5月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_uri_heikin_tanka] ")       '6月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_siire_heikin_tanka] ")     '6月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_uri_heikin_tanka] ")       '7月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_siire_heikin_tanka] ")     '7月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_uri_heikin_tanka] ")       '8月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_siire_heikin_tanka] ")     '8月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_uri_heikin_tanka] ")       '9月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_siire_heikin_tanka] ")     '9月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_uri_heikin_tanka] ")      '10月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_siire_heikin_tanka] ")    '10月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_uri_heikin_tanka] ")      '11月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_siire_heikin_tanka] ")    '11月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_uri_heikin_tanka] ")      '12月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_siire_heikin_tanka] ")    '12月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_uri_heikin_tanka] ")       '1月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_siire_heikin_tanka] ")     '1月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_uri_heikin_tanka] ")       '2月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_siire_heikin_tanka] ")     '2月_計算用__仕入平均単価
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_uri_heikin_tanka] ")       '3月_計算用__売上平均単価
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_siire_heikin_tanka] ")     '3月_計算用__仕入平均単価

                .AppendLine("  ,NULL AS keikakuyou_nenkan_tousuu ")    '計画用_年間棟数
                .AppendLine("  ,NULL AS sds_kaisi_nengetu ")           'SDS開始年月

                .AppendLine("    ,t_keikaku_kanri.keikaku_kakutei_flg,t_keikaku_kanri.keikaku_huhen_flg,t_keikaku_kanri3.keikaku_nendo,t_keikaku_kanri2.add_datetime,t_keikaku_kanri3.busyo_cd ,t_keikaku_kanri3.keikaku_kanri_syouhin_cd AS syouhin_cd  ")
                .AppendLine("  FROM (SELECT keikaku_nendo,MAX(add_datetime) AS add_datetime,busyo_cd,keikaku_kanri_syouhin_cd   ")
                .AppendLine("  FROM t_fc_keikaku_kanri WITH(READCOMMITTED)    ")
                .AppendLine("  GROUP BY keikaku_nendo,busyo_cd,keikaku_kanri_syouhin_cd) AS t_keikaku_kanri2   ")
                .AppendLine("  RIGHT JOIN (   ")
                If blnCsv Then
                    .AppendLine("  SELECT keikaku_nendo,@busyo_cd_fc_csv AS busyo_cd,keikaku_kanri_syouhin_cd FROM m_keikaku_kanri_syouhin WITH(READCOMMITTED) ")
                    .AppendLine("  UNION ")
                    paramList.Add(MakeParam("@busyo_cd_fc_csv", SqlDbType.Char, 4, KeikakuKanriRecord.Shiten))
                End If
                .AppendLine("  SELECT keikaku_nendo,busyo_cd,keikaku_kanri_syouhin_cd FROM t_fc_keikaku_kanri WITH(READCOMMITTED) UNION  ")
                .AppendLine("  SELECT keikaku_nendo,busyo_cd,keikaku_kanri_syouhin_cd FROM t_fc_yotei_mikomi_kanri WITH(READCOMMITTED) UNION  ")
                .AppendLine("    SELECT keikaku_nendo,busyo_cd,keikaku_kanri_syouhin_cd FROM    ")
                .AppendLine("  (SELECT MKK.keikaku_nendo,MKK.busyo_cd,TJK.keikaku_kanri_syouhin_cd   ")
                .AppendLine("  FROM m_keikaku_kameiten AS MKK  WITH(READCOMMITTED)                   ")
                .AppendLine("  INNER JOIN t_jisseki_kanri AS TJK  WITH(READCOMMITTED)  ")
                .AppendLine("  ON TJK.keikaku_nendo=MKK.keikaku_nendo     ")
                .AppendLine("  AND TJK.kameiten_cd=MKK.kameiten_cd  ")
                .AppendLine("  WHERE  MKK.eigyou_kbn='4') AS MM    ")
                .AppendLine("  GROUP BY keikaku_nendo,busyo_cd,keikaku_kanri_syouhin_cd ) AS t_keikaku_kanri3   ")
                .AppendLine("   ON t_keikaku_kanri3.keikaku_nendo=t_keikaku_kanri2.keikaku_nendo    ")
                .AppendLine("  AND  t_keikaku_kanri3.busyo_cd=t_keikaku_kanri2.busyo_cd            ")
                .AppendLine("  AND  t_keikaku_kanri3.keikaku_kanri_syouhin_cd=t_keikaku_kanri2.keikaku_kanri_syouhin_cd       ")
                .AppendLine("  LEFT JOIN t_fc_keikaku_kanri AS t_keikaku_kanri     ")
                .AppendLine("   ON t_keikaku_kanri.keikaku_nendo=t_keikaku_kanri2.keikaku_nendo      ")
                .AppendLine("   AND  t_keikaku_kanri.add_datetime=t_keikaku_kanri2.add_datetime   ")
                .AppendLine("  AND  t_keikaku_kanri.busyo_cd=t_keikaku_kanri2.busyo_cd      ")
                .AppendLine("  AND  t_keikaku_kanri.keikaku_kanri_syouhin_cd=t_keikaku_kanri2.keikaku_kanri_syouhin_cd   ) AS TKK ")


                .AppendLine(" LEFT JOIN ( SELECT ")
                .AppendLine("  t_yotei_mikomi_kanri.[4gatu_mikomi_kensuu] ")         '4月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[4gatu_mikomi_kingaku] ")        '4月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[4gatu_mikomi_arari] ")          '4月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_kensuu] ")         '5月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_kingaku] ")        '5月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_arari] ")          '5月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_kensuu] ")         '6月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_kingaku] ")        '6月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_arari] ")          '6月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_kensuu] ")         '7月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_kingaku] ")        '7月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_arari] ")          '7月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_kensuu] ")         '8月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_kingaku] ")        '8月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_arari] ")          '8月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_kensuu] ")         '9月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_kingaku] ")        '9月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_arari] ")          '9月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_kensuu] ")        '10月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_kingaku] ")       '10月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_arari] ")         '10月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_kensuu] ")        '11月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_kingaku] ")       '11月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_arari] ")         '11月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_kensuu] ")        '12月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_kingaku] ")       '12月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_arari] ")         '12月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_kensuu] ")         '1月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_kingaku] ")        '1月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_arari] ")          '1月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_kensuu] ")         '2月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_kingaku] ")        '2月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_arari] ")          '2月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_kensuu] ")         '3月_見込件数
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_kingaku] ")        '3月_見込金額
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_arari] ")          '3月_見込粗利
                .AppendLine("  ,t_yotei_mikomi_kanri.keikaku_nendo ")
                .AppendLine("  ,t_yotei_mikomi_kanri.add_datetime ")
                .AppendLine("  ,t_yotei_mikomi_kanri.busyo_cd ")
                .AppendLine("  ,t_yotei_mikomi_kanri2.syouhin_cd ")

                .AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   '見込_SDS選択時追加金額
                .AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4月_見込_SS_SDS入力
                .AppendLine("  ,NULL AS [5gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [6gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [7gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [8gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [9gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [10gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [11gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [12gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [1gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [2gatu_mikomi_ss_sds] ")
                .AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3月_見込_SS_SDS入力



                .AppendLine("  FROM (SELECT keikaku_nendo,MAX(add_datetime) AS add_datetime,busyo_cd,keikaku_kanri_syouhin_cd AS syouhin_cd  ")
                .AppendLine("  FROM t_fc_yotei_mikomi_kanri WITH(READCOMMITTED)   ")
                .AppendLine("  GROUP BY keikaku_nendo,busyo_cd,keikaku_kanri_syouhin_cd) AS t_yotei_mikomi_kanri2  ")
                .AppendLine("  LEFT JOIN t_fc_yotei_mikomi_kanri AS t_yotei_mikomi_kanri WITH(READCOMMITTED)   ")
                .AppendLine("  ON t_yotei_mikomi_kanri.keikaku_nendo=t_yotei_mikomi_kanri2.keikaku_nendo  ")
                .AppendLine("  AND  t_yotei_mikomi_kanri.add_datetime=t_yotei_mikomi_kanri2.add_datetime  ")
                .AppendLine("  AND  t_yotei_mikomi_kanri.busyo_cd=t_yotei_mikomi_kanri2.busyo_cd  ")
                .AppendLine("  AND  t_yotei_mikomi_kanri.keikaku_kanri_syouhin_cd=t_yotei_mikomi_kanri2.syouhin_cd ) AS TYMK ")
                .AppendLine("  ON TKK.keikaku_nendo=TYMK.keikaku_nendo  ")
                .AppendLine("  AND TKK.syouhin_cd=TYMK.syouhin_cd  ")
                .AppendLine("  AND TKK.busyo_cd=TYMK.busyo_cd  ")
            End With

        End If

        Return sqlBuffer.ToString

    End Function
    ''' <summary>
    ''' 計画管理テーブルの更新処理
    ''' </summary>
    ''' <param name="strKousin">更新条件</param>
    ''' <param name="strKousinId">更新ID</param>
    ''' <param name="blnKakutei">確定区分</param>
    ''' <returns></returns>
    ''' <remarks>2012/11/14 P-44979 高 新規作成</remarks>
    Public Function UpdKeikakuKanri(ByVal strKousin As String, ByVal strKousinId As String, ByVal blnKakutei As Boolean) As Integer
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKousin, blnKakutei)
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        Dim iCnt As Integer = 0
        For iCnt = 0 To Split(strKousin, ",").Length - 1
            With commandTextSb
                .AppendLine("UPDATE ")
                .AppendLine("   t_keikaku_kanri WITH (UPDLOCK) ")
                .AppendLine("SET ")
                If blnKakutei Then
                    .AppendLine(" keikaku_kakutei_flg=@flg, ")
                    .AppendLine(" keikaku_kakutei_id = @kousin_id, ")
                    .AppendLine(" keikaku_kakutei_datetime = GETDATE(),  ")
                Else
                    .AppendLine(" keikaku_minaosi_flg=@flg, ")
                    .AppendLine(" kakutei_minaosi_id = @kousin_id, ")
                    .AppendLine(" kakutei_minaosi_datetime = GETDATE(),  ")
                    .AppendLine(" keikaku_kakutei_flg=@flg2, ")
                End If
                .AppendLine(" upd_datetime = GETDATE(),  ")
                .AppendLine(" upd_login_user_id=@kousin_id ")
                .AppendLine(" WHERE keikaku_nendo='" & Split(Split(strKousin, ",")(iCnt), "|")(0) & "'")
                .AppendLine(" AND add_datetime='" & Split(Split(strKousin, ",")(iCnt), "|")(2) & "'")
                .AppendLine(" AND kameiten_cd='" & Split(Split(strKousin, ",")(iCnt), "|")(1) & "'")
                .AppendLine(" AND keikaku_kanri_syouhin_cd='" & Split(Split(strKousin, ",")(iCnt), "|")(3) & "';")
            End With
        Next


        'パラメータの設定
        With paramList

            .Add(MakeParam("@flg", SqlDbType.Int, 1, "1"))
            .Add(MakeParam("@flg2", SqlDbType.Int, 1, "0"))
            .Add(MakeParam("@kousin_id", SqlDbType.VarChar, 30, strKousinId))
        End With

        ' クエリ実行
        Return ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
    End Function
    ''' <summary>
    ''' 全社計画管理テーブルから計画情報を取得する
    ''' </summary>
    ''' <param name="KeikakuKanriRecord">検索情報</param>
    ''' <returns>全社の計画情報</returns>
    ''' <remarks>全社計画管理テーブルから計画情報を取得する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 高 新規作成 </para>
    ''' </history>
    Public Function SelSitenbetuTukiData(ByVal KeikakuKanriRecord As KeikakuKanriRecord) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, KeikakuKanriRecord)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            For i As Integer = 1 To 4
                .AppendLine(" SELECT ")
                .AppendLine("     [4gatu_keikaku_kingaku], ")
                .AppendLine("     [5gatu_keikaku_kingaku], ")
                .AppendLine("     [6gatu_keikaku_kingaku], ")
                .AppendLine("     [7gatu_keikaku_kingaku], ")
                .AppendLine("     [8gatu_keikaku_kingaku], ")
                .AppendLine("     [9gatu_keikaku_kingaku], ")
                .AppendLine("     [10gatu_keikaku_kingaku], ")
                .AppendLine("     [11gatu_keikaku_kingaku], ")
                .AppendLine("     [12gatu_keikaku_kingaku], ")
                .AppendLine("     [1gatu_keikaku_kingaku], ")
                .AppendLine("     [2gatu_keikaku_kingaku], ")
                .AppendLine("     [3gatu_keikaku_kingaku], ")
                .AppendLine("     [eigyou_kbn] ")
                .AppendLine(" FROM ")
                .AppendLine("     t_sitenbetu_tuki_keikaku_kanri AS TZKK WITH(READCOMMITTED) ")
                .AppendLine(" WHERE EXISTS ")
                .AppendLine(" ( ")
                .AppendLine("     SELECT keikaku_nendo, ")
                .AppendLine("         MAX(add_datetime) ")
                .AppendLine("     FROM t_sitenbetu_tuki_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED)  ")
                .AppendLine("     WHERE keikaku_nendo = @keikaku_nendo ")
                .AppendLine("     AND busyo_cd = @busyo_cd ")
                .AppendLine("     AND eigyou_kbn = " & i)
                .AppendLine("     GROUP BY keikaku_nendo ")
                .AppendLine("     HAVING TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo ")
                .AppendLine("     AND CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
                .AppendLine("                 + CONVERT(VARCHAR,TZKK.add_datetime,121)  ")
                .AppendLine("         = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
                .AppendLine("                 + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)) ")
                .AppendLine(" ) AND eigyou_kbn =" & i)
                If i <> 4 Then
                    .AppendLine(" UNION ALL ")
                End If
            Next
            
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, KeikakuKanriRecord.Nendo))     '計画年度(YYYY)
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.Char, 4, KeikakuKanriRecord.Shiten))     '支店
        'paramList.Add(MakeParam("@busyo_cd", SqlDbType.Char, 4, "0202"))     '支店
        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function
End Class
