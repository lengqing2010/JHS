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
    ''' Œv‰æŠÇ—‰æ–Ê‚É‚æ‚èAŒŸõŒ‹‰Ê‚Ìƒf[ƒ^‚ğ‚ğæ“¾‚·‚é
    ''' </summary>
    ''' <param name="KeikakuKanriRecord">ŒŸõğŒ</param>
    ''' <param name="SelectKbn">–¾×•”SQL‹æ•ª</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function SelKeikakuKanriData(ByVal KeikakuKanriRecord As KeikakuKanriRecord, ByVal SelectKbn As KeikakuKanriRecord.selectKbn, ByVal blnCsv As Boolean) As DataTable
        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, KeikakuKanriRecord)

        Dim dsInfo As New Data.DataSet
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim strTmp As String = ""
        'ƒpƒ‰ƒ[ƒ^Ši”[


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
    ''' ‹¤’ÊSELECTˆ—
    ''' </summary>
    ''' <param name="KeikakuKanriRecord">ŒŸõğŒ</param>
    ''' <param name="SelectKbn">–¾×•”SQL‹æ•ª</param>
    ''' <param name="paramList">ƒpƒ‰ƒ[ƒ^</param>
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
                    .AppendLine("  MKK.kameiten_mei ")              '‰Á–¿“X–¼
                    .AppendLine("  ,MKK.kameiten_cd  ")             '‰Á–¿“XƒR[ƒh
                    .AppendLine("  ,MKM.meisyou  ")                 '‰c‹Æ‹æ•ª
                    .AppendLine("  ,MKK.eigyou_tantousya_mei  ")    '‰c‹Æ’S“–Ò–¼
                    .AppendLine("  ,MKK.keikaku_nenkan_tousuu  ")   '”NŠÔ“”

                    .AppendLine("  ,TKK.[keikakuyou_nenkan_tousuu] ")   'Œv‰æ—p_”NŠÔ“”

                    .AppendLine("  ,MKK.gyoutai  ")                 '‹Æ‘Ô
                    .AppendLine("  ,TKJK.uri_hiritu  ")             '”„ã”ä—¦

                    .AppendLine("  ,TKK.[sds_kaisi_nengetu] ")      'SDSŠJn”NŒ

                    .AppendLine("  ,TKJK.koj_hantei_ritu  ")        'H–”»’è—¦
                    .AppendLine("  ,TKJK.koj_jyuchuu_ritu  ")       'H–ó’—¦
                    .AppendLine("  ,TKJK.tyoku_koj_ritu  ")         '’¼H–—¦
                    .AppendLine("  ,MKM2.meisyou AS meisyou2  ")    '–¼Ìí•Ê
                    .AppendLine("  ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei  ")   '¤•i–¼
                    If blnCsv Then
                        .AppendLine("  ,ISNULL(TJK.[zennen_heikin_tanka],0) AS zennen_heikin_tanka  ")     '‘O”N_•½‹Ï’P‰¿
                    Else

                        .AppendLine("  ,TJK.[zennen_heikin_tanka] AS zennen_heikin_tanka  ")     '‘O”N_•½‹Ï’P‰¿
                    End If

                    .AppendLine("  ,TJK.[4gatu_jisseki_kensuu] ")     '4Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[4gatu_jisseki_kingaku]")    '4Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[4gatu_jisseki_arari] ")      '4Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[5gatu_jisseki_kensuu] ")     '5Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[5gatu_jisseki_kingaku] ")    '5Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[5gatu_jisseki_arari] ")      '5Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[6gatu_jisseki_kensuu] ")     '6Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[6gatu_jisseki_kingaku] ")    '6Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[6gatu_jisseki_arari] ")      '6Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[7gatu_jisseki_kensuu] ")     '7Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[7gatu_jisseki_kingaku]")    '7Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[7gatu_jisseki_arari] ")      '7Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[8gatu_jisseki_kensuu] ")     '8Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[8gatu_jisseki_kingaku] ")    '8Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[8gatu_jisseki_arari] ")      '8Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[9gatu_jisseki_kensuu] ")     '9Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[9gatu_jisseki_kingaku] ")    '9Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[9gatu_jisseki_arari] ")      '9Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[10gatu_jisseki_kensuu] ")    '10Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[10gatu_jisseki_kingaku] ")   '10Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[10gatu_jisseki_arari] ")     '10Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[11gatu_jisseki_kensuu] ")    '11Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[11gatu_jisseki_kingaku] ")   '11Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[11gatu_jisseki_arari] ")     '11Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[12gatu_jisseki_kensuu] ")    '12Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[12gatu_jisseki_kingaku] ")   '12Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[12gatu_jisseki_arari] ")     '12Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[1gatu_jisseki_kensuu] ")     '1Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[1gatu_jisseki_kingaku] ")    '1Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[1gatu_jisseki_arari] ")      '1Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[2gatu_jisseki_kensuu] ")     '2Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[2gatu_jisseki_kingaku] ")    '2Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[2gatu_jisseki_arari] ")      '2Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[3gatu_jisseki_kensuu] ")     '3Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[3gatu_jisseki_kingaku] ")    '3Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[3gatu_jisseki_arari] ")      '3Œ_ÀÑ‘e—˜

                    .AppendLine("  ,TKK.[4gatu_keikaku_kensuu] ")         '4Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[4gatu_keikaku_kingaku] ")        '4Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[4gatu_keikaku_arari] ")          '4Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[5gatu_keikaku_kensuu] ")         '5Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[5gatu_keikaku_kingaku] ")        '5Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[5gatu_keikaku_arari] ")          '5Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[6gatu_keikaku_kensuu] ")         '6Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[6gatu_keikaku_kingaku] ")        '6Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[6gatu_keikaku_arari] ")          '6Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[7gatu_keikaku_kensuu] ")         '7Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[7gatu_keikaku_kingaku] ")        '7Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[7gatu_keikaku_arari] ")          '7Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[8gatu_keikaku_kensuu] ")         '8Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[8gatu_keikaku_kingaku] ")        '8Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[8gatu_keikaku_arari] ")          '8Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[9gatu_keikaku_kensuu] ")         '9Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[9gatu_keikaku_kingaku] ")        '9Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[9gatu_keikaku_arari] ")          '9Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[10gatu_keikaku_kensuu] ")        '10Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[10gatu_keikaku_kingaku] ")       '10Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[10gatu_keikaku_arari] ")         '10Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[11gatu_keikaku_kensuu] ")        '11Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[11gatu_keikaku_kingaku] ")       '11Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[11gatu_keikaku_arari] ")         '11Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[12gatu_keikaku_kensuu] ")        '12Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[12gatu_keikaku_kingaku] ")       '12Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[12gatu_keikaku_arari] ")         '12Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[1gatu_keikaku_kensuu] ")         '1Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[1gatu_keikaku_kingaku] ")        '1Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[1gatu_keikaku_arari] ")          '1Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[2gatu_keikaku_kensuu] ")         '2Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[2gatu_keikaku_kingaku] ")        '2Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[2gatu_keikaku_arari] ")          '2Œ_Œv‰æ‘e—˜
                    .AppendLine("  ,TKK.[3gatu_keikaku_kensuu] ")         '3Œ_Œv‰æŒ”
                    .AppendLine("  ,TKK.[3gatu_keikaku_kingaku] ")        '3Œ_Œv‰æ‹àŠz
                    .AppendLine("  ,TKK.[3gatu_keikaku_arari] ")          '3Œ_Œv‰æ‘e—˜

                    .AppendLine("  ,TYMK.[4gatu_mikomi_kensuu] ")         '4Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[4gatu_mikomi_kingaku] ")        '4Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[4gatu_mikomi_arari] ")          '4Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[5gatu_mikomi_kensuu] ")         '5Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[5gatu_mikomi_kingaku] ")        '5Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[5gatu_mikomi_arari] ")          '5Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[6gatu_mikomi_kensuu] ")         '6Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[6gatu_mikomi_kingaku] ")        '6Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[6gatu_mikomi_arari] ")          '6Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[7gatu_mikomi_kensuu] ")         '7Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[7gatu_mikomi_kingaku] ")        '7Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[7gatu_mikomi_arari] ")          '7Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[8gatu_mikomi_kensuu] ")         '8Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[8gatu_mikomi_kingaku] ")        '8Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[8gatu_mikomi_arari] ")          '8Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[9gatu_mikomi_kensuu] ")         '9Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[9gatu_mikomi_kingaku] ")        '9Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[9gatu_mikomi_arari] ")          '9Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[10gatu_mikomi_kensuu] ")        '10Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[10gatu_mikomi_kingaku] ")       '10Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[10gatu_mikomi_arari] ")         '10Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[11gatu_mikomi_kensuu] ")        '11Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[11gatu_mikomi_kingaku] ")       '11Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[11gatu_mikomi_arari] ")         '11Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[12gatu_mikomi_kensuu] ")        '12Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[12gatu_mikomi_kingaku] ")       '12Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[12gatu_mikomi_arari] ")         '12Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[1gatu_mikomi_kensuu] ")         '1Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[1gatu_mikomi_kingaku] ")        '1Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[1gatu_mikomi_arari] ")          '1Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[2gatu_mikomi_kensuu] ")         '2Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[2gatu_mikomi_kingaku] ")        '2Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[2gatu_mikomi_arari] ")          '2Œ_Œ©‘e—˜
                    .AppendLine("  ,TYMK.[3gatu_mikomi_kensuu] ")         '3Œ_Œ©Œ”
                    .AppendLine("  ,TYMK.[3gatu_mikomi_kingaku] ")        '3Œ_Œ©‹àŠz
                    .AppendLine("  ,TYMK.[3gatu_mikomi_arari] ")          '3Œ_Œ©‘e—˜


                    .AppendLine("  ,ZTJK.[4gatu_jisseki_kensuu] AS Z_4gatu_jisseki_kensuu ")     '‘O”N4Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[4gatu_jisseki_kingaku]  AS Z_4gatu_jisseki_kingaku ")    '‘O”N4Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[4gatu_jisseki_arari]  AS Z_4gatu_jisseki_arari ")      '‘O”N4Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[5gatu_jisseki_kensuu]  AS Z_5gatu_jisseki_kensuu ")     '‘O”N5Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[5gatu_jisseki_kingaku] AS Z_5gatu_jisseki_kingaku ")    '‘O”N5Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[5gatu_jisseki_arari] Z_5gatu_jisseki_arari")      '‘O”N5Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[6gatu_jisseki_kensuu] AS Z_6gatu_jisseki_kensuu  ")     '‘O”N6Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[6gatu_jisseki_kingaku] AS Z_6gatu_jisseki_kingaku  ")    '‘O”N6Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[6gatu_jisseki_arari] Z_6gatu_jisseki_arari")      '‘O”N6Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[7gatu_jisseki_kensuu]  AS Z_7gatu_jisseki_kensuu ")     '‘O”N7Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[7gatu_jisseki_kingaku]  AS Z_7gatu_jisseki_kingaku ")    '‘O”N7Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[7gatu_jisseki_arari] Z_7gatu_jisseki_arari")      '‘O”N7Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[8gatu_jisseki_kensuu]  AS Z_8gatu_jisseki_kensuu ")     '‘O”N8Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[8gatu_jisseki_kingaku]  AS Z_8gatu_jisseki_kingaku ")    '‘O”N8Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[8gatu_jisseki_arari] Z_8gatu_jisseki_arari")      '‘O”N8Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[9gatu_jisseki_kensuu]  AS Z_9gatu_jisseki_kensuu ")     '‘O”N9Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[9gatu_jisseki_kingaku]  AS Z_9gatu_jisseki_kingaku ")    '‘O”N9Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[9gatu_jisseki_arari] Z_9gatu_jisseki_arari")      '‘O”N9Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[10gatu_jisseki_kensuu]  AS Z_10gatu_jisseki_kensuu ")    '‘O”N10Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[10gatu_jisseki_kingaku]  AS Z_10gatu_jisseki_kingaku ")   '‘O”N10Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[10gatu_jisseki_arari] Z_10gatu_jisseki_arari")     '‘O”N10Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[11gatu_jisseki_kensuu]  AS Z_11gatu_jisseki_kensuu ")    '‘O”N11Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[11gatu_jisseki_kingaku]  AS Z_11gatu_jisseki_kingaku ")   '‘O”N11Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[11gatu_jisseki_arari] Z_11gatu_jisseki_arari")     '‘O”N11Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[12gatu_jisseki_kensuu]  AS Z_12gatu_jisseki_kensuu ")    '‘O”N12Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[12gatu_jisseki_kingaku]  AS Z_12gatu_jisseki_kingaku ")   '‘O”N12Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[12gatu_jisseki_arari] Z_12gatu_jisseki_arari")     '‘O”N12Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[1gatu_jisseki_kensuu]  AS Z_1gatu_jisseki_kensuu ")     '‘O”N1Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[1gatu_jisseki_kingaku] AS Z_1gatu_jisseki_kingaku  ")    '‘O”N1Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[1gatu_jisseki_arari] Z_1gatu_jisseki_arari")      '‘O”N1Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[2gatu_jisseki_kensuu]  AS Z_2gatu_jisseki_kensuu ")     '‘O”N2Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[2gatu_jisseki_kingaku] AS Z_2gatu_jisseki_kingaku  ")    '‘O”N2Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[2gatu_jisseki_arari] Z_2gatu_jisseki_arari")      '‘O”N2Œ_ÀÑ‘e—˜
                    .AppendLine("  ,ZTJK.[3gatu_jisseki_kensuu]  AS Z_3gatu_jisseki_kensuu ")     '‘O”N3Œ_ÀÑŒ”
                    .AppendLine("  ,ZTJK.[3gatu_jisseki_kingaku] AS Z_3gatu_jisseki_kingaku  ")    '‘O”N3Œ_ÀÑ‹àŠz
                    .AppendLine("  ,ZTJK.[3gatu_jisseki_arari] Z_3gatu_jisseki_arari")      '‘O”N3Œ_ÀÑ‘e—˜

                    .AppendLine("  ,TKK.[4gatu_keisanyou_koj_hantei_ritu] ")      '4Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[4gatu_keisanyou_koj_jyuchuu_ritu] ")     '4Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[4gatu_keisanyou_tyoku_koj_ritu] ")       '4Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[5gatu_keisanyou_koj_hantei_ritu] ")      '5Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[5gatu_keisanyou_koj_jyuchuu_ritu] ")     '5Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[5gatu_keisanyou_tyoku_koj_ritu] ")       '5Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[6gatu_keisanyou_koj_hantei_ritu] ")      '6Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[6gatu_keisanyou_koj_jyuchuu_ritu] ")     '6Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[6gatu_keisanyou_tyoku_koj_ritu] ")       '6Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[7gatu_keisanyou_koj_hantei_ritu] ")      '7Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[7gatu_keisanyou_koj_jyuchuu_ritu] ")     '7Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[7gatu_keisanyou_tyoku_koj_ritu] ")       '7Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[8gatu_keisanyou_koj_hantei_ritu] ")      '8Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[8gatu_keisanyou_koj_jyuchuu_ritu] ")     '8Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[8gatu_keisanyou_tyoku_koj_ritu] ")       '8Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[9gatu_keisanyou_koj_hantei_ritu] ")      '9Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[9gatu_keisanyou_koj_jyuchuu_ritu] ")     '9Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[9gatu_keisanyou_tyoku_koj_ritu] ")       '9Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[10gatu_keisanyou_koj_hantei_ritu] ")     '10Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[10gatu_keisanyou_koj_jyuchuu_ritu] ")    '10Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[10gatu_keisanyou_tyoku_koj_ritu] ")      '10Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[11gatu_keisanyou_koj_hantei_ritu] ")     '11Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[11gatu_keisanyou_koj_jyuchuu_ritu] ")    '11Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[11gatu_keisanyou_tyoku_koj_ritu] ")      '11Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[12gatu_keisanyou_koj_hantei_ritu] ")     '12Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[12gatu_keisanyou_koj_jyuchuu_ritu] ")    '12Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[12gatu_keisanyou_tyoku_koj_ritu] ")      '12Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[1gatu_keisanyou_koj_hantei_ritu] ")      '1Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[1gatu_keisanyou_koj_jyuchuu_ritu] ")     '1Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[1gatu_keisanyou_tyoku_koj_ritu] ")       '1Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[2gatu_keisanyou_koj_hantei_ritu] ")      '2Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[2gatu_keisanyou_koj_jyuchuu_ritu] ")     '2Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[2gatu_keisanyou_tyoku_koj_ritu] ")       '2Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,TKK.[3gatu_keisanyou_koj_hantei_ritu] ")      '3Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine("  ,TKK.[3gatu_keisanyou_koj_jyuchuu_ritu] ")     '3Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine("  ,TKK.[3gatu_keisanyou_tyoku_koj_ritu] ")       '3Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,MKK.eigyou_kbn  ")
                    .AppendLine("  ,TKK.add_datetime  ")
                    '.AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   'Œ©_SDS‘I‘ğ’Ç‰Á‹àŠz
                    '.AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4Œ_Œ©_SS_SDS“ü—Í
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
                    '.AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3Œ_Œ©_SS_SDS“ü—Í
                    '.AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ") 'Œv‰æ_SDS‘I‘ğ’Ç‰Á‹àŠz
                    '.AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4Œ_Œv‰æ_SS_SDS“ü—Í
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
                    '.AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3Œ_Œv‰æ_SS_SDS“ü—Í

                    .AppendLine("  ,TKK.[4gatu_keisanyou_uri_heikin_tanka] ")       '4Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[4gatu_keisanyou_siire_heikin_tanka] ")     '4Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[5gatu_keisanyou_uri_heikin_tanka] ")       '5Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[5gatu_keisanyou_siire_heikin_tanka] ")     '5Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[6gatu_keisanyou_uri_heikin_tanka] ")       '6Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[6gatu_keisanyou_siire_heikin_tanka] ")     '6Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[7gatu_keisanyou_uri_heikin_tanka] ")       '7Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[7gatu_keisanyou_siire_heikin_tanka] ")     '7Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[8gatu_keisanyou_uri_heikin_tanka] ")       '8Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[8gatu_keisanyou_siire_heikin_tanka] ")     '8Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[9gatu_keisanyou_uri_heikin_tanka] ")       '9Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[9gatu_keisanyou_siire_heikin_tanka] ")     '9Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[10gatu_keisanyou_uri_heikin_tanka] ")      '10Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[10gatu_keisanyou_siire_heikin_tanka] ")    '10Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[11gatu_keisanyou_uri_heikin_tanka] ")      '11Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[11gatu_keisanyou_siire_heikin_tanka] ")    '11Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[12gatu_keisanyou_uri_heikin_tanka] ")      '12Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[12gatu_keisanyou_siire_heikin_tanka] ")    '12Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[1gatu_keisanyou_uri_heikin_tanka] ")       '1Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[1gatu_keisanyou_siire_heikin_tanka] ")     '1Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[2gatu_keisanyou_uri_heikin_tanka] ")       '2Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[2gatu_keisanyou_siire_heikin_tanka] ")     '2Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[3gatu_keisanyou_uri_heikin_tanka] ")       '3Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[3gatu_keisanyou_siire_heikin_tanka] ")     '3Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿

                Case Utilities.KeikakuKanriRecord.selectKbn.goukei, Utilities.KeikakuKanriRecord.selectKbn.syoukei
                    .AppendLine(" SELECT  ")
                    .AppendLine("  NULL AS kameiten_mei")      '‰Á–¿“X–¼
                    .AppendLine("  ,NULL AS kameiten_cd ")             '‰Á–¿“XƒR[ƒh
                    If SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.syoukei Then
                        .AppendLine("  ,MKM.meisyou  ")                 '‰c‹Æ‹æ•ª
                    Else
                        .AppendLine("  ,'‘S‘ÌiFCœŠOj' AS  meisyou ")                 '‰c‹Æ‹æ•ª
                    End If

                    .AppendLine("  ,NULL AS eigyou_tantousya_mei ")     '‰c‹Æ’S“–Ò–¼
                    .AppendLine("  ,NULL AS keikaku_nenkan_tousuu")     '”NŠÔ“”

                    .AppendLine("  ,NULL AS [keikakuyou_nenkan_tousuu] ")   'Œv‰æ—p_”NŠÔ“”

                    .AppendLine("  ,NULL AS gyoutai ")                  '‹Æ‘Ô
                    .AppendLine("  ,NULL AS uri_hiritu ")               '”„ã”ä—¦

                    .AppendLine("  ,NULL AS [sds_kaisi_nengetu] ")      'SDSŠJn”NŒ

                    .AppendLine("  ,NULL AS koj_hantei_ritu ")          'H–”»’è—¦
                    .AppendLine("  ,NULL AS koj_jyuchuu_ritu ")         'H–ó’—¦
                    .AppendLine("  ,NULL AS tyoku_koj_ritu ")           '’¼H–—¦
                    .AppendLine("  ,MKM2.meisyou AS meisyou2  ")        '–¼Ìí•Ê
                    .AppendLine("  ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei  ")   '¤•i–¼
                    .AppendLine("  ,NULL AS [zennen_heikin_tanka]  ")   '‘O”N_•½‹Ï’P‰¿

                    .AppendLine(" ,sum(TJK.[4gatu_jisseki_kensuu] ) AS [4gatu_jisseki_kensuu] ") '4Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[4gatu_jisseki_kingaku] ) AS [4gatu_jisseki_kingaku] ") '4Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[4gatu_jisseki_arari] ) AS [4gatu_jisseki_arari] ") '4Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[5gatu_jisseki_kensuu] ) AS [5gatu_jisseki_kensuu] ") '5Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[5gatu_jisseki_kingaku] ) AS [5gatu_jisseki_kingaku] ") '5Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[5gatu_jisseki_arari] ) AS [5gatu_jisseki_arari] ") '5Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[6gatu_jisseki_kensuu] ) AS [6gatu_jisseki_kensuu] ") '6Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[6gatu_jisseki_kingaku] ) AS [6gatu_jisseki_kingaku] ") '6Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[6gatu_jisseki_arari] ) AS [6gatu_jisseki_arari] ") '6Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[7gatu_jisseki_kensuu] ) AS [7gatu_jisseki_kensuu] ") '7Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[7gatu_jisseki_kingaku] ) AS [7gatu_jisseki_kingaku] ") '7Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[7gatu_jisseki_arari] ) AS [7gatu_jisseki_arari] ") '7Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[8gatu_jisseki_kensuu] ) AS [8gatu_jisseki_kensuu] ") '8Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[8gatu_jisseki_kingaku] ) AS [8gatu_jisseki_kingaku] ") '8Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[8gatu_jisseki_arari] ) AS [8gatu_jisseki_arari] ") '8Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[9gatu_jisseki_kensuu] ) AS [9gatu_jisseki_kensuu] ") '9Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[9gatu_jisseki_kingaku] ) AS [9gatu_jisseki_kingaku] ") '9Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[9gatu_jisseki_arari] ) AS [9gatu_jisseki_arari] ") '9Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[10gatu_jisseki_kensuu] ) AS [10gatu_jisseki_kensuu] ") '10Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[10gatu_jisseki_kingaku] ) AS [10gatu_jisseki_kingaku] ") '10Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[10gatu_jisseki_arari] ) AS [10gatu_jisseki_arari] ") '10Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[11gatu_jisseki_kensuu] ) AS [11gatu_jisseki_kensuu] ") '11Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[11gatu_jisseki_kingaku] ) AS [11gatu_jisseki_kingaku] ") '11Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[11gatu_jisseki_arari] ) AS [11gatu_jisseki_arari] ") '11Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[12gatu_jisseki_kensuu] ) AS [12gatu_jisseki_kensuu] ") '12Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[12gatu_jisseki_kingaku] ) AS [12gatu_jisseki_kingaku] ") '12Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[12gatu_jisseki_arari] ) AS [12gatu_jisseki_arari] ") '12Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[1gatu_jisseki_kensuu] ) AS [1gatu_jisseki_kensuu] ") '1Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[1gatu_jisseki_kingaku] ) AS [1gatu_jisseki_kingaku] ") '1Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[1gatu_jisseki_arari] ) AS [1gatu_jisseki_arari] ") '1Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[2gatu_jisseki_kensuu] ) AS [2gatu_jisseki_kensuu] ") '2Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[2gatu_jisseki_kingaku] ) AS [2gatu_jisseki_kingaku] ") '2Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[2gatu_jisseki_arari] ) AS [2gatu_jisseki_arari] ") '2Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(TJK.[3gatu_jisseki_kensuu] ) AS [3gatu_jisseki_kensuu] ") '3Œ_ÀÑŒ”
                    .AppendLine(" ,sum(TJK.[3gatu_jisseki_kingaku] ) AS [3gatu_jisseki_kingaku] ") '3Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(TJK.[3gatu_jisseki_arari] ) AS [3gatu_jisseki_arari] ") '3Œ_ÀÑ‘e—˜

                    .AppendLine(" ,sum(TKK.[4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu] ") '4Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku] ") '4Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari] ") '4Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu] ") '5Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku] ") '5Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari] ") '5Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu] ") '6Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku] ") '6Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari] ") '6Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu] ") '7Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku] ") '7Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari] ") '7Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu] ") '8Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ") '8Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari] ") '8Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu] ") '9Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku] ") '9Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari] ") '9Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu] ") '10Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku] ") '10Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari] ") '10Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu] ") '11Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ") '11Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari] ") '11Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu] ") '12Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku] ") '12Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari] ") '12Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu] ") '1Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku] ") '1Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari] ") '1Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu] ") '2Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku] ") '2Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari] ") '2Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum(TKK.[3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu] ") '3Œ_Œv‰æŒ”
                    .AppendLine(" ,sum(TKK.[3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ") '3Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum(TKK.[3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari] ") '3Œ_Œv‰æ‘e—˜

                    .AppendLine(" ,sum(TYMK.[4gatu_mikomi_kensuu] ) AS [4gatu_mikomi_kensuu] ") '4Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[4gatu_mikomi_kingaku] ) AS [4gatu_mikomi_kingaku] ") '4Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[4gatu_mikomi_arari] ) AS [4gatu_mikomi_arari] ") '4Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[5gatu_mikomi_kensuu] ) AS [5gatu_mikomi_kensuu] ") '5Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[5gatu_mikomi_kingaku] ) AS [5gatu_mikomi_kingaku] ") '5Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[5gatu_mikomi_arari] ) AS [5gatu_mikomi_arari] ") '5Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[6gatu_mikomi_kensuu] ) AS [6gatu_mikomi_kensuu] ") '6Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[6gatu_mikomi_kingaku] ) AS [6gatu_mikomi_kingaku] ") '6Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[6gatu_mikomi_arari] ) AS [6gatu_mikomi_arari] ") '6Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[7gatu_mikomi_kensuu] ) AS [7gatu_mikomi_kensuu] ") '7Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[7gatu_mikomi_kingaku] ) AS [7gatu_mikomi_kingaku] ") '7Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[7gatu_mikomi_arari] ) AS [7gatu_mikomi_arari] ") '7Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[8gatu_mikomi_kensuu] ) AS [8gatu_mikomi_kensuu] ") '8Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[8gatu_mikomi_kingaku] ) AS [8gatu_mikomi_kingaku] ") '8Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[8gatu_mikomi_arari] ) AS [8gatu_mikomi_arari] ") '8Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[9gatu_mikomi_kensuu] ) AS [9gatu_mikomi_kensuu] ") '9Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[9gatu_mikomi_kingaku] ) AS [9gatu_mikomi_kingaku] ") '9Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[9gatu_mikomi_arari] ) AS [9gatu_mikomi_arari] ") '9Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[10gatu_mikomi_kensuu] ) AS [10gatu_mikomi_kensuu] ") '10Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[10gatu_mikomi_kingaku] ) AS [10gatu_mikomi_kingaku] ") '10Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[10gatu_mikomi_arari] ) AS [10gatu_mikomi_arari] ") '10Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[11gatu_mikomi_kensuu] ) AS [11gatu_mikomi_kensuu] ") '11Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[11gatu_mikomi_kingaku] ) AS [11gatu_mikomi_kingaku] ") '11Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[11gatu_mikomi_arari] ) AS [11gatu_mikomi_arari] ") '11Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[12gatu_mikomi_kensuu] ) AS [12gatu_mikomi_kensuu] ") '12Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[12gatu_mikomi_kingaku] ) AS [12gatu_mikomi_kingaku] ") '12Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[12gatu_mikomi_arari] ) AS [12gatu_mikomi_arari] ") '12Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[1gatu_mikomi_kensuu] ) AS [1gatu_mikomi_kensuu] ") '1Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[1gatu_mikomi_kingaku] ) AS [1gatu_mikomi_kingaku] ") '1Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[1gatu_mikomi_arari] ) AS [1gatu_mikomi_arari] ") '1Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[2gatu_mikomi_kensuu] ) AS [2gatu_mikomi_kensuu] ") '2Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[2gatu_mikomi_kingaku] ) AS [2gatu_mikomi_kingaku] ") '2Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[2gatu_mikomi_arari] ) AS [2gatu_mikomi_arari] ") '2Œ_Œ©‘e—˜
                    .AppendLine(" ,sum(TYMK.[3gatu_mikomi_kensuu] ) AS [3gatu_mikomi_kensuu] ") '3Œ_Œ©Œ”
                    .AppendLine(" ,sum(TYMK.[3gatu_mikomi_kingaku] ) AS [3gatu_mikomi_kingaku] ") '3Œ_Œ©‹àŠz
                    .AppendLine(" ,sum(TYMK.[3gatu_mikomi_arari] ) AS [3gatu_mikomi_arari] ") '3Œ_Œ©‘e—˜


                    .AppendLine(" ,sum(ZTJK.[4gatu_jisseki_kensuu] ) AS [Z_4gatu_jisseki_kensuu] ") '‘O”N4Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[4gatu_jisseki_kingaku] ) AS [Z_4gatu_jisseki_kingaku] ") '‘O”N4Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[4gatu_jisseki_arari] ) AS [Z_4gatu_jisseki_arari] ") '‘O”N4Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[5gatu_jisseki_kensuu] ) AS [Z_5gatu_jisseki_kensuu] ") '‘O”N5Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[5gatu_jisseki_kingaku] ) AS [Z_5gatu_jisseki_kingaku] ") '‘O”N5Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[5gatu_jisseki_arari] ) AS [Z_5gatu_jisseki_arari] ") '‘O”N5Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[6gatu_jisseki_kensuu] ) AS [Z_6gatu_jisseki_kensuu] ") '‘O”N6Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[6gatu_jisseki_kingaku] ) AS [Z_6gatu_jisseki_kingaku] ") '‘O”N6Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[6gatu_jisseki_arari] ) AS [Z_6gatu_jisseki_arari] ") '‘O”N6Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[7gatu_jisseki_kensuu] ) AS [Z_7gatu_jisseki_kensuu] ") '‘O”N7Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[7gatu_jisseki_kingaku] ) AS [Z_7gatu_jisseki_kingaku] ") '‘O”N7Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[7gatu_jisseki_arari] ) AS [Z_7gatu_jisseki_arari] ") '‘O”N7Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[8gatu_jisseki_kensuu] ) AS [Z_8gatu_jisseki_kensuu] ") '‘O”N8Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[8gatu_jisseki_kingaku] ) AS [Z_8gatu_jisseki_kingaku] ") '‘O”N8Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[8gatu_jisseki_arari] ) AS [Z_8gatu_jisseki_arari] ") '‘O”N8Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[9gatu_jisseki_kensuu] ) AS [Z_9gatu_jisseki_kensuu] ") '‘O”N9Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[9gatu_jisseki_kingaku] ) AS [Z_9gatu_jisseki_kingaku] ") '‘O”N9Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[9gatu_jisseki_arari] ) AS [Z_9gatu_jisseki_arari] ") '‘O”N9Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[10gatu_jisseki_kensuu] ) AS [Z_10gatu_jisseki_kensuu] ") '‘O”N10Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[10gatu_jisseki_kingaku] ) AS [Z_10gatu_jisseki_kingaku] ") '‘O”N10Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[10gatu_jisseki_arari] ) AS [Z_10gatu_jisseki_arari] ") '‘O”N10Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[11gatu_jisseki_kensuu] ) AS [Z_11gatu_jisseki_kensuu] ") '‘O”N11Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[11gatu_jisseki_kingaku] ) AS [Z_11gatu_jisseki_kingaku] ") '‘O”N11Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[11gatu_jisseki_arari] ) AS [Z_11gatu_jisseki_arari] ") '‘O”N11Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[12gatu_jisseki_kensuu] ) AS [Z_12gatu_jisseki_kensuu] ") '‘O”N12Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[12gatu_jisseki_kingaku] ) AS [Z_12gatu_jisseki_kingaku] ") '‘O”N12Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[12gatu_jisseki_arari] ) AS [Z_12gatu_jisseki_arari] ") '‘O”N12Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[1gatu_jisseki_kensuu] ) AS [Z_1gatu_jisseki_kensuu] ") '‘O”N1Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[1gatu_jisseki_kingaku] ) AS [Z_1gatu_jisseki_kingaku] ") '‘O”N1Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[1gatu_jisseki_arari] ) AS [Z_1gatu_jisseki_arari] ") '‘O”N1Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[2gatu_jisseki_kensuu] ) AS [Z_2gatu_jisseki_kensuu] ") '‘O”N2Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[2gatu_jisseki_kingaku] ) AS [Z_2gatu_jisseki_kingaku] ") '‘O”N2Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[2gatu_jisseki_arari] ) AS [Z_2gatu_jisseki_arari] ") '‘O”N2Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum(ZTJK.[3gatu_jisseki_kensuu] ) AS [Z_3gatu_jisseki_kensuu] ") '‘O”N3Œ_ÀÑŒ”
                    .AppendLine(" ,sum(ZTJK.[3gatu_jisseki_kingaku] ) AS [Z_3gatu_jisseki_kingaku] ") '‘O”N3Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum(ZTJK.[3gatu_jisseki_arari] ) AS [Z_3gatu_jisseki_arari] ") '‘O”N3Œ_ÀÑ‘e—˜

                    .AppendLine(" ,sum(TKK.[4gatu_keisanyou_koj_hantei_ritu] ) AS [4gatu_keisanyou_koj_hantei_ritu] ") '4Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[4gatu_keisanyou_koj_jyuchuu_ritu] ) AS [4gatu_keisanyou_koj_jyuchuu_ritu] ") '4Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[4gatu_keisanyou_tyoku_koj_ritu] ) AS [4gatu_keisanyou_tyoku_koj_ritu] ") '4Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[5gatu_keisanyou_koj_hantei_ritu] ) AS [5gatu_keisanyou_koj_hantei_ritu] ") '5Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[5gatu_keisanyou_koj_jyuchuu_ritu] ) AS [5gatu_keisanyou_koj_jyuchuu_ritu] ") '5Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[5gatu_keisanyou_tyoku_koj_ritu] ) AS [5gatu_keisanyou_tyoku_koj_ritu] ") '5Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[6gatu_keisanyou_koj_hantei_ritu] ) AS [6gatu_keisanyou_koj_hantei_ritu] ") '6Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[6gatu_keisanyou_koj_jyuchuu_ritu] ) AS [6gatu_keisanyou_koj_jyuchuu_ritu] ") '6Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[6gatu_keisanyou_tyoku_koj_ritu] ) AS [6gatu_keisanyou_tyoku_koj_ritu] ") '6Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[7gatu_keisanyou_koj_hantei_ritu] ) AS [7gatu_keisanyou_koj_hantei_ritu] ") '7Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[7gatu_keisanyou_koj_jyuchuu_ritu] ) AS [7gatu_keisanyou_koj_jyuchuu_ritu] ") '7Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[7gatu_keisanyou_tyoku_koj_ritu] ) AS [7gatu_keisanyou_tyoku_koj_ritu] ") '7Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[8gatu_keisanyou_koj_hantei_ritu] ) AS [8gatu_keisanyou_koj_hantei_ritu] ") '8Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[8gatu_keisanyou_koj_jyuchuu_ritu] ) AS [8gatu_keisanyou_koj_jyuchuu_ritu] ") '8Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[8gatu_keisanyou_tyoku_koj_ritu] ) AS [8gatu_keisanyou_tyoku_koj_ritu] ") '8Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[9gatu_keisanyou_koj_hantei_ritu] ) AS [9gatu_keisanyou_koj_hantei_ritu] ") '9Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[9gatu_keisanyou_koj_jyuchuu_ritu] ) AS [9gatu_keisanyou_koj_jyuchuu_ritu] ") '9Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[9gatu_keisanyou_tyoku_koj_ritu] ) AS [9gatu_keisanyou_tyoku_koj_ritu] ") '9Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[10gatu_keisanyou_koj_hantei_ritu] ) AS [10gatu_keisanyou_koj_hantei_ritu] ") '10Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[10gatu_keisanyou_koj_jyuchuu_ritu] ) AS [10gatu_keisanyou_koj_jyuchuu_ritu] ") '10Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[10gatu_keisanyou_tyoku_koj_ritu] ) AS [10gatu_keisanyou_tyoku_koj_ritu] ") '10Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[11gatu_keisanyou_koj_hantei_ritu] ) AS [11gatu_keisanyou_koj_hantei_ritu] ") '11Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[11gatu_keisanyou_koj_jyuchuu_ritu] ) AS [11gatu_keisanyou_koj_jyuchuu_ritu] ") '11Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[11gatu_keisanyou_tyoku_koj_ritu] ) AS [11gatu_keisanyou_tyoku_koj_ritu] ") '11Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[12gatu_keisanyou_koj_hantei_ritu] ) AS [12gatu_keisanyou_koj_hantei_ritu] ") '12Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[12gatu_keisanyou_koj_jyuchuu_ritu] ) AS [12gatu_keisanyou_koj_jyuchuu_ritu] ") '12Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[12gatu_keisanyou_tyoku_koj_ritu] ) AS [12gatu_keisanyou_tyoku_koj_ritu] ") '12Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[1gatu_keisanyou_koj_hantei_ritu] ) AS [1gatu_keisanyou_koj_hantei_ritu] ") '1Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[1gatu_keisanyou_koj_jyuchuu_ritu] ) AS [1gatu_keisanyou_koj_jyuchuu_ritu] ") '1Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[1gatu_keisanyou_tyoku_koj_ritu] ) AS [1gatu_keisanyou_tyoku_koj_ritu] ") '1Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[2gatu_keisanyou_koj_hantei_ritu] ) AS [2gatu_keisanyou_koj_hantei_ritu] ") '2Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[2gatu_keisanyou_koj_jyuchuu_ritu] ) AS [2gatu_keisanyou_koj_jyuchuu_ritu] ") '2Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[2gatu_keisanyou_tyoku_koj_ritu] ) AS [2gatu_keisanyou_tyoku_koj_ritu] ") '2Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum(TKK.[3gatu_keisanyou_koj_hantei_ritu] ) AS [3gatu_keisanyou_koj_hantei_ritu] ") '3Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum(TKK.[3gatu_keisanyou_koj_jyuchuu_ritu] ) AS [3gatu_keisanyou_koj_jyuchuu_ritu] ") '3Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum(TKK.[3gatu_keisanyou_tyoku_koj_ritu] ) AS [3gatu_keisanyou_tyoku_koj_ritu] ") '3Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,NULL  AS eigyou_kbn ")
                    .AppendLine("  ,NULL AS add_datetime ")
                    '.AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   'Œ©_SDS‘I‘ğ’Ç‰Á‹àŠz
                    '.AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4Œ_Œ©_SS_SDS“ü—Í
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
                    '.AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3Œ_Œ©_SS_SDS“ü—Í
                    '.AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ") 'Œv‰æ_SDS‘I‘ğ’Ç‰Á‹àŠz
                    '.AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4Œ_Œv‰æ_SS_SDS“ü—Í
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
                    '.AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3Œ_Œv‰æ_SS_SDS“ü—Í

                    .AppendLine("  ,NULL AS [4gatu_keisanyou_uri_heikin_tanka] ")       '4Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [4gatu_keisanyou_siire_heikin_tanka] ")     '4Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [5gatu_keisanyou_uri_heikin_tanka] ")       '5Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [5gatu_keisanyou_siire_heikin_tanka] ")     '5Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [6gatu_keisanyou_uri_heikin_tanka] ")       '6Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [6gatu_keisanyou_siire_heikin_tanka] ")     '6Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [7gatu_keisanyou_uri_heikin_tanka] ")       '7Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [7gatu_keisanyou_siire_heikin_tanka] ")     '7Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [8gatu_keisanyou_uri_heikin_tanka] ")       '8Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [8gatu_keisanyou_siire_heikin_tanka] ")     '8Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [9gatu_keisanyou_uri_heikin_tanka] ")       '9Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [9gatu_keisanyou_siire_heikin_tanka] ")     '9Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [10gatu_keisanyou_uri_heikin_tanka] ")      '10Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [10gatu_keisanyou_siire_heikin_tanka] ")    '10Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [11gatu_keisanyou_uri_heikin_tanka] ")      '11Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [11gatu_keisanyou_siire_heikin_tanka] ")    '11Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [12gatu_keisanyou_uri_heikin_tanka] ")      '12Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [12gatu_keisanyou_siire_heikin_tanka] ")    '12Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [1gatu_keisanyou_uri_heikin_tanka] ")       '1Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [1gatu_keisanyou_siire_heikin_tanka] ")     '1Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [2gatu_keisanyou_uri_heikin_tanka] ")       '2Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [2gatu_keisanyou_siire_heikin_tanka] ")     '2Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [3gatu_keisanyou_uri_heikin_tanka] ")       '3Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [3gatu_keisanyou_siire_heikin_tanka] ")     '3Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿

                Case Utilities.KeikakuKanriRecord.selectKbn.FC
                    .AppendLine(" SELECT  ")
                    .AppendLine("  NULL AS kameiten_mei")               '‰Á–¿“X–¼
                    .AppendLine("  ,NULL AS kameiten_cd ")              '‰Á–¿“XƒR[ƒh
                    .AppendLine("  ,'FC' AS  meisyou ")                 '‰c‹Æ‹æ•ª
                    .AppendLine("  ,NULL AS eigyou_tantousya_mei ")     '‰c‹Æ’S“–Ò–¼
                    .AppendLine("  ,NULL AS keikaku_nenkan_tousuu")     '”NŠÔ“”

                    .AppendLine("  ,NULL AS [keikakuyou_nenkan_tousuu] ")   'Œv‰æ—p_”NŠÔ“”

                    .AppendLine("  ,NULL AS gyoutai ")                  '‹Æ‘Ô
                    .AppendLine("  ,NULL AS uri_hiritu ")               '”„ã”ä—¦

                    .AppendLine("  ,NULL AS [sds_kaisi_nengetu] ")      'SDSŠJn”NŒ

                    .AppendLine("  ,NULL AS koj_hantei_ritu ")          'H–”»’è—¦
                    .AppendLine("  ,NULL AS koj_jyuchuu_ritu ")         'H–ó’—¦
                    .AppendLine("  ,NULL AS tyoku_koj_ritu ")           '’¼H–—¦
                    .AppendLine("  ,MKM2.meisyou AS meisyou2  ")        '–¼Ìí•Ê
                    .AppendLine("  ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei  ")            '¤•i–¼
                    .AppendLine("  ,TJK.zennen_heikin_tanka AS [zennen_heikin_tanka]  ")     '‘O”N_•½‹Ï’P‰¿

                    .AppendLine("  ,TJK.[4gatu_jisseki_kensuu] ")     '4Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[4gatu_jisseki_kingaku]")    '4Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[4gatu_jisseki_arari] ")      '4Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[5gatu_jisseki_kensuu] ")     '5Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[5gatu_jisseki_kingaku] ")    '5Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[5gatu_jisseki_arari] ")      '5Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[6gatu_jisseki_kensuu] ")     '6Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[6gatu_jisseki_kingaku] ")    '6Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[6gatu_jisseki_arari] ")      '6Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[7gatu_jisseki_kensuu] ")     '7Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[7gatu_jisseki_kingaku]")    '7Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[7gatu_jisseki_arari] ")      '7Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[8gatu_jisseki_kensuu] ")     '8Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[8gatu_jisseki_kingaku] ")    '8Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[8gatu_jisseki_arari] ")      '8Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[9gatu_jisseki_kensuu] ")     '9Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[9gatu_jisseki_kingaku] ")    '9Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[9gatu_jisseki_arari] ")      '9Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[10gatu_jisseki_kensuu] ")    '10Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[10gatu_jisseki_kingaku] ")   '10Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[10gatu_jisseki_arari] ")     '10Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[11gatu_jisseki_kensuu] ")    '11Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[11gatu_jisseki_kingaku] ")   '11Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[11gatu_jisseki_arari] ")     '11Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[12gatu_jisseki_kensuu] ")    '12Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[12gatu_jisseki_kingaku] ")   '12Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[12gatu_jisseki_arari] ")     '12Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[1gatu_jisseki_kensuu] ")     '1Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[1gatu_jisseki_kingaku] ")    '1Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[1gatu_jisseki_arari] ")      '1Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[2gatu_jisseki_kensuu] ")     '2Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[2gatu_jisseki_kingaku] ")    '2Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[2gatu_jisseki_arari] ")      '2Œ_ÀÑ‘e—˜
                    .AppendLine("  ,TJK.[3gatu_jisseki_kensuu] ")     '3Œ_ÀÑŒ”
                    .AppendLine("  ,TJK.[3gatu_jisseki_kingaku] ")    '3Œ_ÀÑ‹àŠz
                    .AppendLine("  ,TJK.[3gatu_jisseki_arari] ")      '3Œ_ÀÑ‘e—˜

                    .AppendLine(" ,(TKK.[4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu] ") '4Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku] ") '4Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari] ") '4Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu] ") '5Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku] ") '5Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari] ") '5Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu] ") '6Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku] ") '6Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari] ") '6Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu] ") '7Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku] ") '7Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari] ") '7Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu] ") '8Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ") '8Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari] ") '8Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu] ") '9Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku] ") '9Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari] ") '9Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu] ") '10Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku] ") '10Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari] ") '10Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu] ") '11Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ") '11Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari] ") '11Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu] ") '12Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku] ") '12Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari] ") '12Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu] ") '1Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku] ") '1Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari] ") '1Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu] ") '2Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku] ") '2Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari] ") '2Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,(TKK.[3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu] ") '3Œ_Œv‰æŒ”
                    .AppendLine(" ,(TKK.[3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ") '3Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,(TKK.[3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari] ") '3Œ_Œv‰æ‘e—˜

                    .AppendLine(" ,(TYMK.[4gatu_mikomi_kensuu] ) AS [4gatu_mikomi_kensuu] ") '4Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[4gatu_mikomi_kingaku] ) AS [4gatu_mikomi_kingaku] ") '4Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[4gatu_mikomi_arari] ) AS [4gatu_mikomi_arari] ") '4Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[5gatu_mikomi_kensuu] ) AS [5gatu_mikomi_kensuu] ") '5Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[5gatu_mikomi_kingaku] ) AS [5gatu_mikomi_kingaku] ") '5Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[5gatu_mikomi_arari] ) AS [5gatu_mikomi_arari] ") '5Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[6gatu_mikomi_kensuu] ) AS [6gatu_mikomi_kensuu] ") '6Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[6gatu_mikomi_kingaku] ) AS [6gatu_mikomi_kingaku] ") '6Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[6gatu_mikomi_arari] ) AS [6gatu_mikomi_arari] ") '6Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[7gatu_mikomi_kensuu] ) AS [7gatu_mikomi_kensuu] ") '7Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[7gatu_mikomi_kingaku] ) AS [7gatu_mikomi_kingaku] ") '7Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[7gatu_mikomi_arari] ) AS [7gatu_mikomi_arari] ") '7Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[8gatu_mikomi_kensuu] ) AS [8gatu_mikomi_kensuu] ") '8Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[8gatu_mikomi_kingaku] ) AS [8gatu_mikomi_kingaku] ") '8Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[8gatu_mikomi_arari] ) AS [8gatu_mikomi_arari] ") '8Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[9gatu_mikomi_kensuu] ) AS [9gatu_mikomi_kensuu] ") '9Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[9gatu_mikomi_kingaku] ) AS [9gatu_mikomi_kingaku] ") '9Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[9gatu_mikomi_arari] ) AS [9gatu_mikomi_arari] ") '9Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[10gatu_mikomi_kensuu] ) AS [10gatu_mikomi_kensuu] ") '10Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[10gatu_mikomi_kingaku] ) AS [10gatu_mikomi_kingaku] ") '10Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[10gatu_mikomi_arari] ) AS [10gatu_mikomi_arari] ") '10Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[11gatu_mikomi_kensuu] ) AS [11gatu_mikomi_kensuu] ") '11Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[11gatu_mikomi_kingaku] ) AS [11gatu_mikomi_kingaku] ") '11Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[11gatu_mikomi_arari] ) AS [11gatu_mikomi_arari] ") '11Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[12gatu_mikomi_kensuu] ) AS [12gatu_mikomi_kensuu] ") '12Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[12gatu_mikomi_kingaku] ) AS [12gatu_mikomi_kingaku] ") '12Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[12gatu_mikomi_arari] ) AS [12gatu_mikomi_arari] ") '12Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[1gatu_mikomi_kensuu] ) AS [1gatu_mikomi_kensuu] ") '1Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[1gatu_mikomi_kingaku] ) AS [1gatu_mikomi_kingaku] ") '1Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[1gatu_mikomi_arari] ) AS [1gatu_mikomi_arari] ") '1Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[2gatu_mikomi_kensuu] ) AS [2gatu_mikomi_kensuu] ") '2Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[2gatu_mikomi_kingaku] ) AS [2gatu_mikomi_kingaku] ") '2Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[2gatu_mikomi_arari] ) AS [2gatu_mikomi_arari] ") '2Œ_Œ©‘e—˜
                    .AppendLine(" ,(TYMK.[3gatu_mikomi_kensuu] ) AS [3gatu_mikomi_kensuu] ") '3Œ_Œ©Œ”
                    .AppendLine(" ,(TYMK.[3gatu_mikomi_kingaku] ) AS [3gatu_mikomi_kingaku] ") '3Œ_Œ©‹àŠz
                    .AppendLine(" ,(TYMK.[3gatu_mikomi_arari] ) AS [3gatu_mikomi_arari] ") '3Œ_Œ©‘e—˜


                    .AppendLine(" ,NULL AS [Z_4gatu_jisseki_kensuu] ") '‘O”N4Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_4gatu_jisseki_kingaku] ") '‘O”N4Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_4gatu_jisseki_arari] ") '‘O”N4Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_5gatu_jisseki_kensuu] ") '‘O”N5Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_5gatu_jisseki_kingaku] ") '‘O”N5Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_5gatu_jisseki_arari] ") '‘O”N5Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_6gatu_jisseki_kensuu] ") '‘O”N6Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_6gatu_jisseki_kingaku] ") '‘O”N6Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_6gatu_jisseki_arari] ") '‘O”N6Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_7gatu_jisseki_kensuu] ") '‘O”N7Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_7gatu_jisseki_kingaku] ") '‘O”N7Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_7gatu_jisseki_arari] ") '‘O”N7Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_8gatu_jisseki_kensuu] ") '‘O”N8Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_8gatu_jisseki_kingaku] ") '‘O”N8Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_8gatu_jisseki_arari] ") '‘O”N8Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_9gatu_jisseki_kensuu] ") '‘O”N9Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_9gatu_jisseki_kingaku] ") '‘O”N9Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_9gatu_jisseki_arari] ") '‘O”N9Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_10gatu_jisseki_kensuu] ") '‘O”N10Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_10gatu_jisseki_kingaku] ") '‘O”N10Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_10gatu_jisseki_arari] ") '‘O”N10Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_11gatu_jisseki_kensuu] ") '‘O”N11Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_11gatu_jisseki_kingaku] ") '‘O”N11Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_11gatu_jisseki_arari] ") '‘O”N11Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_12gatu_jisseki_kensuu] ") '‘O”N12Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_12gatu_jisseki_kingaku] ") '‘O”N12Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_12gatu_jisseki_arari] ") '‘O”N12Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_1gatu_jisseki_kensuu] ") '‘O”N1Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_1gatu_jisseki_kingaku] ") '‘O”N1Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_1gatu_jisseki_arari] ") '‘O”N1Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_2gatu_jisseki_kensuu] ") '‘O”N2Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_2gatu_jisseki_kingaku] ") '‘O”N2Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_2gatu_jisseki_arari] ") '‘O”N2Œ_ÀÑ‘e—˜
                    .AppendLine(" ,NULL AS [Z_3gatu_jisseki_kensuu] ") '‘O”N3Œ_ÀÑŒ”
                    .AppendLine(" ,NULL AS [Z_3gatu_jisseki_kingaku] ") '‘O”N3Œ_ÀÑ‹àŠz
                    .AppendLine(" ,NULL AS [Z_3gatu_jisseki_arari] ") '‘O”N3Œ_ÀÑ‘e—˜

                    .AppendLine(" ,(TKK.[4gatu_keisanyou_koj_hantei_ritu] ) AS [4gatu_keisanyou_koj_hantei_ritu] ") '4Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[4gatu_keisanyou_koj_jyuchuu_ritu] ) AS [4gatu_keisanyou_koj_jyuchuu_ritu] ") '4Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[4gatu_keisanyou_tyoku_koj_ritu] ) AS [4gatu_keisanyou_tyoku_koj_ritu] ") '4Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[5gatu_keisanyou_koj_hantei_ritu] ) AS [5gatu_keisanyou_koj_hantei_ritu] ") '5Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[5gatu_keisanyou_koj_jyuchuu_ritu] ) AS [5gatu_keisanyou_koj_jyuchuu_ritu] ") '5Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[5gatu_keisanyou_tyoku_koj_ritu] ) AS [5gatu_keisanyou_tyoku_koj_ritu] ") '5Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[6gatu_keisanyou_koj_hantei_ritu] ) AS [6gatu_keisanyou_koj_hantei_ritu] ") '6Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[6gatu_keisanyou_koj_jyuchuu_ritu] ) AS [6gatu_keisanyou_koj_jyuchuu_ritu] ") '6Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[6gatu_keisanyou_tyoku_koj_ritu] ) AS [6gatu_keisanyou_tyoku_koj_ritu] ") '6Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[7gatu_keisanyou_koj_hantei_ritu] ) AS [7gatu_keisanyou_koj_hantei_ritu] ") '7Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[7gatu_keisanyou_koj_jyuchuu_ritu] ) AS [7gatu_keisanyou_koj_jyuchuu_ritu] ") '7Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[7gatu_keisanyou_tyoku_koj_ritu] ) AS [7gatu_keisanyou_tyoku_koj_ritu] ") '7Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[8gatu_keisanyou_koj_hantei_ritu] ) AS [8gatu_keisanyou_koj_hantei_ritu] ") '8Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[8gatu_keisanyou_koj_jyuchuu_ritu] ) AS [8gatu_keisanyou_koj_jyuchuu_ritu] ") '8Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[8gatu_keisanyou_tyoku_koj_ritu] ) AS [8gatu_keisanyou_tyoku_koj_ritu] ") '8Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[9gatu_keisanyou_koj_hantei_ritu] ) AS [9gatu_keisanyou_koj_hantei_ritu] ") '9Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[9gatu_keisanyou_koj_jyuchuu_ritu] ) AS [9gatu_keisanyou_koj_jyuchuu_ritu] ") '9Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[9gatu_keisanyou_tyoku_koj_ritu] ) AS [9gatu_keisanyou_tyoku_koj_ritu] ") '9Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[10gatu_keisanyou_koj_hantei_ritu] ) AS [10gatu_keisanyou_koj_hantei_ritu] ") '10Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[10gatu_keisanyou_koj_jyuchuu_ritu] ) AS [10gatu_keisanyou_koj_jyuchuu_ritu] ") '10Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[10gatu_keisanyou_tyoku_koj_ritu] ) AS [10gatu_keisanyou_tyoku_koj_ritu] ") '10Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[11gatu_keisanyou_koj_hantei_ritu] ) AS [11gatu_keisanyou_koj_hantei_ritu] ") '11Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[11gatu_keisanyou_koj_jyuchuu_ritu] ) AS [11gatu_keisanyou_koj_jyuchuu_ritu] ") '11Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[11gatu_keisanyou_tyoku_koj_ritu] ) AS [11gatu_keisanyou_tyoku_koj_ritu] ") '11Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[12gatu_keisanyou_koj_hantei_ritu] ) AS [12gatu_keisanyou_koj_hantei_ritu] ") '12Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[12gatu_keisanyou_koj_jyuchuu_ritu] ) AS [12gatu_keisanyou_koj_jyuchuu_ritu] ") '12Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[12gatu_keisanyou_tyoku_koj_ritu] ) AS [12gatu_keisanyou_tyoku_koj_ritu] ") '12Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[1gatu_keisanyou_koj_hantei_ritu] ) AS [1gatu_keisanyou_koj_hantei_ritu] ") '1Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[1gatu_keisanyou_koj_jyuchuu_ritu] ) AS [1gatu_keisanyou_koj_jyuchuu_ritu] ") '1Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[1gatu_keisanyou_tyoku_koj_ritu] ) AS [1gatu_keisanyou_tyoku_koj_ritu] ") '1Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[2gatu_keisanyou_koj_hantei_ritu] ) AS [2gatu_keisanyou_koj_hantei_ritu] ") '2Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[2gatu_keisanyou_koj_jyuchuu_ritu] ) AS [2gatu_keisanyou_koj_jyuchuu_ritu] ") '2Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[2gatu_keisanyou_tyoku_koj_ritu] ) AS [2gatu_keisanyou_tyoku_koj_ritu] ") '2Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,(TKK.[3gatu_keisanyou_koj_hantei_ritu] ) AS [3gatu_keisanyou_koj_hantei_ritu] ") '3Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,(TKK.[3gatu_keisanyou_koj_jyuchuu_ritu] ) AS [3gatu_keisanyou_koj_jyuchuu_ritu] ") '3Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,(TKK.[3gatu_keisanyou_tyoku_koj_ritu] ) AS [3gatu_keisanyou_tyoku_koj_ritu] ") '3Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,NULL  AS eigyou_kbn ")
                    .AppendLine("  ,NULL AS add_datetime ")
                    '.AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   'Œ©_SDS‘I‘ğ’Ç‰Á‹àŠz
                    '.AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4Œ_Œ©_SS_SDS“ü—Í
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
                    '.AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3Œ_Œ©_SS_SDS“ü—Í
                    '.AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ") 'Œv‰æ_SDS‘I‘ğ’Ç‰Á‹àŠz
                    '.AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4Œ_Œv‰æ_SS_SDS“ü—Í
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
                    '.AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3Œ_Œv‰æ_SS_SDS“ü—Í

                    .AppendLine("  ,TKK.[4gatu_keisanyou_uri_heikin_tanka] ")       '4Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[4gatu_keisanyou_siire_heikin_tanka] ")     '4Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[5gatu_keisanyou_uri_heikin_tanka] ")       '5Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[5gatu_keisanyou_siire_heikin_tanka] ")     '5Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[6gatu_keisanyou_uri_heikin_tanka] ")       '6Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[6gatu_keisanyou_siire_heikin_tanka] ")     '6Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[7gatu_keisanyou_uri_heikin_tanka] ")       '7Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[7gatu_keisanyou_siire_heikin_tanka] ")     '7Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[8gatu_keisanyou_uri_heikin_tanka] ")       '8Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[8gatu_keisanyou_siire_heikin_tanka] ")     '8Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[9gatu_keisanyou_uri_heikin_tanka] ")       '9Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[9gatu_keisanyou_siire_heikin_tanka] ")     '9Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[10gatu_keisanyou_uri_heikin_tanka] ")      '10Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[10gatu_keisanyou_siire_heikin_tanka] ")    '10Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[11gatu_keisanyou_uri_heikin_tanka] ")      '11Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[11gatu_keisanyou_siire_heikin_tanka] ")    '11Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[12gatu_keisanyou_uri_heikin_tanka] ")      '12Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[12gatu_keisanyou_siire_heikin_tanka] ")    '12Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[1gatu_keisanyou_uri_heikin_tanka] ")       '1Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[1gatu_keisanyou_siire_heikin_tanka] ")     '1Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[2gatu_keisanyou_uri_heikin_tanka] ")       '2Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[2gatu_keisanyou_siire_heikin_tanka] ")     '2Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[3gatu_keisanyou_uri_heikin_tanka] ")       '3Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,TKK.[3gatu_keisanyou_siire_heikin_tanka] ")     '3Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿

                Case Utilities.KeikakuKanriRecord.selectKbn.goukeiFC
                    .AppendLine(" SELECT  ")
                    .AppendLine("  NULL AS kameiten_mei")           '‰Á–¿“X–¼
                    .AppendLine("  ,NULL AS kameiten_cd ")          '‰Á–¿“XƒR[ƒh
                    .AppendLine("  ,'‘S‘Ì' AS  meisyou ")           '‰c‹Æ‹æ•ª
                    .AppendLine("  ,NULL AS eigyou_tantousya_mei ") '‰c‹Æ’S“–Ò–¼
                    .AppendLine("  ,NULL AS keikaku_nenkan_tousuu") '”NŠÔ“”

                    .AppendLine("  ,NULL AS [keikakuyou_nenkan_tousuu] ") 'Œv‰æ—p_”NŠÔ“”

                    .AppendLine("  ,NULL AS gyoutai ")              '‹Æ‘Ô
                    .AppendLine("  ,NULL AS uri_hiritu ")           '”„ã”ä—¦

                    .AppendLine("  ,NULL AS [sds_kaisi_nengetu] ")  'SDSŠJn”NŒ

                    .AppendLine("  ,NULL AS koj_hantei_ritu ")      'H–”»’è—¦
                    .AppendLine("  ,NULL AS koj_jyuchuu_ritu ")     'H–ó’—¦
                    .AppendLine("  ,NULL AS tyoku_koj_ritu ")       '’¼H–—¦
                    .AppendLine("  ,meisyou2   ")                   '–¼Ìí•Ê
                    .AppendLine("  ,syouhin_mei  ")                 '¤•i–¼
                    .AppendLine("  ,NULL AS [zennen_heikin_tanka]  ")     '‘O”N_•½‹Ï’P‰¿

                    .AppendLine(" ,sum([4gatu_jisseki_kensuu] ) AS [4gatu_jisseki_kensuu] ") '4Œ_ÀÑŒ”
                    .AppendLine(" ,sum([4gatu_jisseki_kingaku] ) AS [4gatu_jisseki_kingaku] ") '4Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([4gatu_jisseki_arari] ) AS [4gatu_jisseki_arari] ") '4Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([5gatu_jisseki_kensuu] ) AS [5gatu_jisseki_kensuu] ") '5Œ_ÀÑŒ”
                    .AppendLine(" ,sum([5gatu_jisseki_kingaku] ) AS [5gatu_jisseki_kingaku] ") '5Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([5gatu_jisseki_arari] ) AS [5gatu_jisseki_arari] ") '5Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([6gatu_jisseki_kensuu] ) AS [6gatu_jisseki_kensuu] ") '6Œ_ÀÑŒ”
                    .AppendLine(" ,sum([6gatu_jisseki_kingaku] ) AS [6gatu_jisseki_kingaku] ") '6Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([6gatu_jisseki_arari] ) AS [6gatu_jisseki_arari] ") '6Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([7gatu_jisseki_kensuu] ) AS [7gatu_jisseki_kensuu] ") '7Œ_ÀÑŒ”
                    .AppendLine(" ,sum([7gatu_jisseki_kingaku] ) AS [7gatu_jisseki_kingaku] ") '7Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([7gatu_jisseki_arari] ) AS [7gatu_jisseki_arari] ") '7Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([8gatu_jisseki_kensuu] ) AS [8gatu_jisseki_kensuu] ") '8Œ_ÀÑŒ”
                    .AppendLine(" ,sum([8gatu_jisseki_kingaku] ) AS [8gatu_jisseki_kingaku] ") '8Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([8gatu_jisseki_arari] ) AS [8gatu_jisseki_arari] ") '8Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([9gatu_jisseki_kensuu] ) AS [9gatu_jisseki_kensuu] ") '9Œ_ÀÑŒ”
                    .AppendLine(" ,sum([9gatu_jisseki_kingaku] ) AS [9gatu_jisseki_kingaku] ") '9Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([9gatu_jisseki_arari] ) AS [9gatu_jisseki_arari] ") '9Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([10gatu_jisseki_kensuu] ) AS [10gatu_jisseki_kensuu] ") '10Œ_ÀÑŒ”
                    .AppendLine(" ,sum([10gatu_jisseki_kingaku] ) AS [10gatu_jisseki_kingaku] ") '10Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([10gatu_jisseki_arari] ) AS [10gatu_jisseki_arari] ") '10Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([11gatu_jisseki_kensuu] ) AS [11gatu_jisseki_kensuu] ") '11Œ_ÀÑŒ”
                    .AppendLine(" ,sum([11gatu_jisseki_kingaku] ) AS [11gatu_jisseki_kingaku] ") '11Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([11gatu_jisseki_arari] ) AS [11gatu_jisseki_arari] ") '11Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([12gatu_jisseki_kensuu] ) AS [12gatu_jisseki_kensuu] ") '12Œ_ÀÑŒ”
                    .AppendLine(" ,sum([12gatu_jisseki_kingaku] ) AS [12gatu_jisseki_kingaku] ") '12Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([12gatu_jisseki_arari] ) AS [12gatu_jisseki_arari] ") '12Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([1gatu_jisseki_kensuu] ) AS [1gatu_jisseki_kensuu] ") '1Œ_ÀÑŒ”
                    .AppendLine(" ,sum([1gatu_jisseki_kingaku] ) AS [1gatu_jisseki_kingaku] ") '1Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([1gatu_jisseki_arari] ) AS [1gatu_jisseki_arari] ") '1Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([2gatu_jisseki_kensuu] ) AS [2gatu_jisseki_kensuu] ") '2Œ_ÀÑŒ”
                    .AppendLine(" ,sum([2gatu_jisseki_kingaku] ) AS [2gatu_jisseki_kingaku] ") '2Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([2gatu_jisseki_arari] ) AS [2gatu_jisseki_arari] ") '2Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([3gatu_jisseki_kensuu] ) AS [3gatu_jisseki_kensuu] ") '3Œ_ÀÑŒ”
                    .AppendLine(" ,sum([3gatu_jisseki_kingaku] ) AS [3gatu_jisseki_kingaku] ") '3Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([3gatu_jisseki_arari] ) AS [3gatu_jisseki_arari] ") '3Œ_ÀÑ‘e—˜

                    .AppendLine(" ,sum([4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu] ") '4Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku] ") '4Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari] ") '4Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu] ") '5Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku] ") '5Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari] ") '5Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu] ") '6Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku] ") '6Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari] ") '6Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu] ") '7Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku] ") '7Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari] ") '7Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu] ") '8Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ") '8Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari] ") '8Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu] ") '9Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku] ") '9Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari] ") '9Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu] ") '10Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku] ") '10Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari] ") '10Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu] ") '11Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ") '11Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari] ") '11Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu] ") '12Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku] ") '12Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari] ") '12Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu] ") '1Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku] ") '1Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari] ") '1Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu] ") '2Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku] ") '2Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari] ") '2Œ_Œv‰æ‘e—˜
                    .AppendLine(" ,sum([3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu] ") '3Œ_Œv‰æŒ”
                    .AppendLine(" ,sum([3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ") '3Œ_Œv‰æ‹àŠz
                    .AppendLine(" ,sum([3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari] ") '3Œ_Œv‰æ‘e—˜

                    .AppendLine(" ,sum([4gatu_mikomi_kensuu] ) AS [4gatu_mikomi_kensuu] ") '4Œ_Œ©Œ”
                    .AppendLine(" ,sum([4gatu_mikomi_kingaku] ) AS [4gatu_mikomi_kingaku] ") '4Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([4gatu_mikomi_arari] ) AS [4gatu_mikomi_arari] ") '4Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([5gatu_mikomi_kensuu] ) AS [5gatu_mikomi_kensuu] ") '5Œ_Œ©Œ”
                    .AppendLine(" ,sum([5gatu_mikomi_kingaku] ) AS [5gatu_mikomi_kingaku] ") '5Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([5gatu_mikomi_arari] ) AS [5gatu_mikomi_arari] ") '5Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([6gatu_mikomi_kensuu] ) AS [6gatu_mikomi_kensuu] ") '6Œ_Œ©Œ”
                    .AppendLine(" ,sum([6gatu_mikomi_kingaku] ) AS [6gatu_mikomi_kingaku] ") '6Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([6gatu_mikomi_arari] ) AS [6gatu_mikomi_arari] ") '6Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([7gatu_mikomi_kensuu] ) AS [7gatu_mikomi_kensuu] ") '7Œ_Œ©Œ”
                    .AppendLine(" ,sum([7gatu_mikomi_kingaku] ) AS [7gatu_mikomi_kingaku] ") '7Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([7gatu_mikomi_arari] ) AS [7gatu_mikomi_arari] ") '7Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([8gatu_mikomi_kensuu] ) AS [8gatu_mikomi_kensuu] ") '8Œ_Œ©Œ”
                    .AppendLine(" ,sum([8gatu_mikomi_kingaku] ) AS [8gatu_mikomi_kingaku] ") '8Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([8gatu_mikomi_arari] ) AS [8gatu_mikomi_arari] ") '8Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([9gatu_mikomi_kensuu] ) AS [9gatu_mikomi_kensuu] ") '9Œ_Œ©Œ”
                    .AppendLine(" ,sum([9gatu_mikomi_kingaku] ) AS [9gatu_mikomi_kingaku] ") '9Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([9gatu_mikomi_arari] ) AS [9gatu_mikomi_arari] ") '9Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([10gatu_mikomi_kensuu] ) AS [10gatu_mikomi_kensuu] ") '10Œ_Œ©Œ”
                    .AppendLine(" ,sum([10gatu_mikomi_kingaku] ) AS [10gatu_mikomi_kingaku] ") '10Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([10gatu_mikomi_arari] ) AS [10gatu_mikomi_arari] ") '10Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([11gatu_mikomi_kensuu] ) AS [11gatu_mikomi_kensuu] ") '11Œ_Œ©Œ”
                    .AppendLine(" ,sum([11gatu_mikomi_kingaku] ) AS [11gatu_mikomi_kingaku] ") '11Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([11gatu_mikomi_arari] ) AS [11gatu_mikomi_arari] ") '11Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([12gatu_mikomi_kensuu] ) AS [12gatu_mikomi_kensuu] ") '12Œ_Œ©Œ”
                    .AppendLine(" ,sum([12gatu_mikomi_kingaku] ) AS [12gatu_mikomi_kingaku] ") '12Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([12gatu_mikomi_arari] ) AS [12gatu_mikomi_arari] ") '12Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([1gatu_mikomi_kensuu] ) AS [1gatu_mikomi_kensuu] ") '1Œ_Œ©Œ”
                    .AppendLine(" ,sum([1gatu_mikomi_kingaku] ) AS [1gatu_mikomi_kingaku] ") '1Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([1gatu_mikomi_arari] ) AS [1gatu_mikomi_arari] ") '1Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([2gatu_mikomi_kensuu] ) AS [2gatu_mikomi_kensuu] ") '2Œ_Œ©Œ”
                    .AppendLine(" ,sum([2gatu_mikomi_kingaku] ) AS [2gatu_mikomi_kingaku] ") '2Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([2gatu_mikomi_arari] ) AS [2gatu_mikomi_arari] ") '2Œ_Œ©‘e—˜
                    .AppendLine(" ,sum([3gatu_mikomi_kensuu] ) AS [3gatu_mikomi_kensuu] ") '3Œ_Œ©Œ”
                    .AppendLine(" ,sum([3gatu_mikomi_kingaku] ) AS [3gatu_mikomi_kingaku] ") '3Œ_Œ©‹àŠz
                    .AppendLine(" ,sum([3gatu_mikomi_arari] ) AS [3gatu_mikomi_arari] ") '3Œ_Œ©‘e—˜


                    .AppendLine(" ,sum([Z_4gatu_jisseki_kensuu] ) AS [Z_4gatu_jisseki_kensuu] ") '‘O”N4Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_4gatu_jisseki_kingaku] ) AS [Z_4gatu_jisseki_kingaku] ") '‘O”N4Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_4gatu_jisseki_arari] ) AS [Z_4gatu_jisseki_arari] ") '‘O”N4Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_5gatu_jisseki_kensuu] ) AS [Z_5gatu_jisseki_kensuu] ") '‘O”N5Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_5gatu_jisseki_kingaku] ) AS [Z_5gatu_jisseki_kingaku] ") '‘O”N5Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_5gatu_jisseki_arari] ) AS [Z_5gatu_jisseki_arari] ") '‘O”N5Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_6gatu_jisseki_kensuu] ) AS [Z_6gatu_jisseki_kensuu] ") '‘O”N6Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_6gatu_jisseki_kingaku] ) AS [Z_6gatu_jisseki_kingaku] ") '‘O”N6Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_6gatu_jisseki_arari] ) AS [Z_6gatu_jisseki_arari] ") '‘O”N6Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_7gatu_jisseki_kensuu] ) AS [Z_7gatu_jisseki_kensuu] ") '‘O”N7Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_7gatu_jisseki_kingaku] ) AS [Z_7gatu_jisseki_kingaku] ") '‘O”N7Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_7gatu_jisseki_arari] ) AS [Z_7gatu_jisseki_arari] ") '‘O”N7Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_8gatu_jisseki_kensuu] ) AS [Z_8gatu_jisseki_kensuu] ") '‘O”N8Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_8gatu_jisseki_kingaku] ) AS [Z_8gatu_jisseki_kingaku] ") '‘O”N8Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_8gatu_jisseki_arari] ) AS [Z_8gatu_jisseki_arari] ") '‘O”N8Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_9gatu_jisseki_kensuu] ) AS [Z_9gatu_jisseki_kensuu] ") '‘O”N9Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_9gatu_jisseki_kingaku] ) AS [Z_9gatu_jisseki_kingaku] ") '‘O”N9Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_9gatu_jisseki_arari] ) AS [Z_9gatu_jisseki_arari] ") '‘O”N9Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_10gatu_jisseki_kensuu] ) AS [Z_10gatu_jisseki_kensuu] ") '‘O”N10Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_10gatu_jisseki_kingaku] ) AS [Z_10gatu_jisseki_kingaku] ") '‘O”N10Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_10gatu_jisseki_arari] ) AS [Z_10gatu_jisseki_arari] ") '‘O”N10Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_11gatu_jisseki_kensuu] ) AS [Z_11gatu_jisseki_kensuu] ") '‘O”N11Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_11gatu_jisseki_kingaku] ) AS [Z_11gatu_jisseki_kingaku] ") '‘O”N11Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_11gatu_jisseki_arari] ) AS [Z_11gatu_jisseki_arari] ") '‘O”N11Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_12gatu_jisseki_kensuu] ) AS [Z_12gatu_jisseki_kensuu] ") '‘O”N12Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_12gatu_jisseki_kingaku] ) AS [Z_12gatu_jisseki_kingaku] ") '‘O”N12Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_12gatu_jisseki_arari] ) AS [Z_12gatu_jisseki_arari] ") '‘O”N12Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_1gatu_jisseki_kensuu] ) AS [Z_1gatu_jisseki_kensuu] ") '‘O”N1Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_1gatu_jisseki_kingaku] ) AS [Z_1gatu_jisseki_kingaku] ") '‘O”N1Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_1gatu_jisseki_arari] ) AS [Z_1gatu_jisseki_arari] ") '‘O”N1Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_2gatu_jisseki_kensuu] ) AS [Z_2gatu_jisseki_kensuu] ") '‘O”N2Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_2gatu_jisseki_kingaku] ) AS [Z_2gatu_jisseki_kingaku] ") '‘O”N2Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_2gatu_jisseki_arari] ) AS [Z_2gatu_jisseki_arari] ") '‘O”N2Œ_ÀÑ‘e—˜
                    .AppendLine(" ,sum([Z_3gatu_jisseki_kensuu] ) AS [Z_3gatu_jisseki_kensuu] ") '‘O”N3Œ_ÀÑŒ”
                    .AppendLine(" ,sum([Z_3gatu_jisseki_kingaku] ) AS [Z_3gatu_jisseki_kingaku] ") '‘O”N3Œ_ÀÑ‹àŠz
                    .AppendLine(" ,sum([Z_3gatu_jisseki_arari] ) AS [Z_3gatu_jisseki_arari] ") '‘O”N3Œ_ÀÑ‘e—˜

                    .AppendLine(" ,sum([4gatu_keisanyou_koj_hantei_ritu] ) AS [4gatu_keisanyou_koj_hantei_ritu] ") '4Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([4gatu_keisanyou_koj_jyuchuu_ritu] ) AS [4gatu_keisanyou_koj_jyuchuu_ritu] ") '4Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([4gatu_keisanyou_tyoku_koj_ritu] ) AS [4gatu_keisanyou_tyoku_koj_ritu] ") '4Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([5gatu_keisanyou_koj_hantei_ritu] ) AS [5gatu_keisanyou_koj_hantei_ritu] ") '5Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([5gatu_keisanyou_koj_jyuchuu_ritu] ) AS [5gatu_keisanyou_koj_jyuchuu_ritu] ") '5Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([5gatu_keisanyou_tyoku_koj_ritu] ) AS [5gatu_keisanyou_tyoku_koj_ritu] ") '5Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([6gatu_keisanyou_koj_hantei_ritu] ) AS [6gatu_keisanyou_koj_hantei_ritu] ") '6Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([6gatu_keisanyou_koj_jyuchuu_ritu] ) AS [6gatu_keisanyou_koj_jyuchuu_ritu] ") '6Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([6gatu_keisanyou_tyoku_koj_ritu] ) AS [6gatu_keisanyou_tyoku_koj_ritu] ") '6Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([7gatu_keisanyou_koj_hantei_ritu] ) AS [7gatu_keisanyou_koj_hantei_ritu] ") '7Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([7gatu_keisanyou_koj_jyuchuu_ritu] ) AS [7gatu_keisanyou_koj_jyuchuu_ritu] ") '7Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([7gatu_keisanyou_tyoku_koj_ritu] ) AS [7gatu_keisanyou_tyoku_koj_ritu] ") '7Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([8gatu_keisanyou_koj_hantei_ritu] ) AS [8gatu_keisanyou_koj_hantei_ritu] ") '8Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([8gatu_keisanyou_koj_jyuchuu_ritu] ) AS [8gatu_keisanyou_koj_jyuchuu_ritu] ") '8Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([8gatu_keisanyou_tyoku_koj_ritu] ) AS [8gatu_keisanyou_tyoku_koj_ritu] ") '8Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([9gatu_keisanyou_koj_hantei_ritu] ) AS [9gatu_keisanyou_koj_hantei_ritu] ") '9Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([9gatu_keisanyou_koj_jyuchuu_ritu] ) AS [9gatu_keisanyou_koj_jyuchuu_ritu] ") '9Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([9gatu_keisanyou_tyoku_koj_ritu] ) AS [9gatu_keisanyou_tyoku_koj_ritu] ") '9Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([10gatu_keisanyou_koj_hantei_ritu] ) AS [10gatu_keisanyou_koj_hantei_ritu] ") '10Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([10gatu_keisanyou_koj_jyuchuu_ritu] ) AS [10gatu_keisanyou_koj_jyuchuu_ritu] ") '10Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([10gatu_keisanyou_tyoku_koj_ritu] ) AS [10gatu_keisanyou_tyoku_koj_ritu] ") '10Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([11gatu_keisanyou_koj_hantei_ritu] ) AS [11gatu_keisanyou_koj_hantei_ritu] ") '11Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([11gatu_keisanyou_koj_jyuchuu_ritu] ) AS [11gatu_keisanyou_koj_jyuchuu_ritu] ") '11Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([11gatu_keisanyou_tyoku_koj_ritu] ) AS [11gatu_keisanyou_tyoku_koj_ritu] ") '11Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([12gatu_keisanyou_koj_hantei_ritu] ) AS [12gatu_keisanyou_koj_hantei_ritu] ") '12Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([12gatu_keisanyou_koj_jyuchuu_ritu] ) AS [12gatu_keisanyou_koj_jyuchuu_ritu] ") '12Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([12gatu_keisanyou_tyoku_koj_ritu] ) AS [12gatu_keisanyou_tyoku_koj_ritu] ") '12Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([1gatu_keisanyou_koj_hantei_ritu] ) AS [1gatu_keisanyou_koj_hantei_ritu] ") '1Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([1gatu_keisanyou_koj_jyuchuu_ritu] ) AS [1gatu_keisanyou_koj_jyuchuu_ritu] ") '1Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([1gatu_keisanyou_tyoku_koj_ritu] ) AS [1gatu_keisanyou_tyoku_koj_ritu] ") '1Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([2gatu_keisanyou_koj_hantei_ritu] ) AS [2gatu_keisanyou_koj_hantei_ritu] ") '2Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([2gatu_keisanyou_koj_jyuchuu_ritu] ) AS [2gatu_keisanyou_koj_jyuchuu_ritu] ") '2Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([2gatu_keisanyou_tyoku_koj_ritu] ) AS [2gatu_keisanyou_tyoku_koj_ritu] ") '2Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine(" ,sum([3gatu_keisanyou_koj_hantei_ritu] ) AS [3gatu_keisanyou_koj_hantei_ritu] ") '3Œ_ŒvZ—p_H–”»’è—¦
                    .AppendLine(" ,sum([3gatu_keisanyou_koj_jyuchuu_ritu] ) AS [3gatu_keisanyou_koj_jyuchuu_ritu] ") '3Œ_ŒvZ—p_H–ó’—¦
                    .AppendLine(" ,sum([3gatu_keisanyou_tyoku_koj_ritu] ) AS [3gatu_keisanyou_tyoku_koj_ritu] ") '3Œ_ŒvZ—p_’¼H–—¦
                    .AppendLine("  ,NULL  AS eigyou_kbn ")
                    .AppendLine("  ,NULL AS add_datetime ")
                    '.AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   'Œ©_SDS‘I‘ğ’Ç‰Á‹àŠz
                    '.AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4Œ_Œ©_SS_SDS“ü—Í
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
                    '.AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3Œ_Œ©_SS_SDS“ü—Í
                    '.AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ") 'Œv‰æ_SDS‘I‘ğ’Ç‰Á‹àŠz
                    '.AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4Œ_Œv‰æ_SS_SDS“ü—Í
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
                    '.AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3Œ_Œv‰æ_SS_SDS“ü—Í

                    .AppendLine("  ,NULL AS [4gatu_keisanyou_uri_heikin_tanka] ")       '4Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [4gatu_keisanyou_siire_heikin_tanka] ")     '4Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [5gatu_keisanyou_uri_heikin_tanka] ")       '5Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [5gatu_keisanyou_siire_heikin_tanka] ")     '5Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [6gatu_keisanyou_uri_heikin_tanka] ")       '6Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [6gatu_keisanyou_siire_heikin_tanka] ")     '6Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [7gatu_keisanyou_uri_heikin_tanka] ")       '7Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [7gatu_keisanyou_siire_heikin_tanka] ")     '7Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [8gatu_keisanyou_uri_heikin_tanka] ")       '8Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [8gatu_keisanyou_siire_heikin_tanka] ")     '8Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [9gatu_keisanyou_uri_heikin_tanka] ")       '9Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [9gatu_keisanyou_siire_heikin_tanka] ")     '9Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [10gatu_keisanyou_uri_heikin_tanka] ")      '10Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [10gatu_keisanyou_siire_heikin_tanka] ")    '10Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [11gatu_keisanyou_uri_heikin_tanka] ")      '11Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [11gatu_keisanyou_siire_heikin_tanka] ")    '11Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [12gatu_keisanyou_uri_heikin_tanka] ")      '12Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [12gatu_keisanyou_siire_heikin_tanka] ")    '12Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [1gatu_keisanyou_uri_heikin_tanka] ")       '1Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [1gatu_keisanyou_siire_heikin_tanka] ")     '1Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [2gatu_keisanyou_uri_heikin_tanka] ")       '2Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [2gatu_keisanyou_siire_heikin_tanka] ")     '2Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [3gatu_keisanyou_uri_heikin_tanka] ")       '3Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                    .AppendLine("  ,NULL AS [3gatu_keisanyou_siire_heikin_tanka] ")     '3Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿

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

            '=====add select item  197‚©‚ç============================
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
                .AppendLine("  t_yotei_mikomi_kanri.[4gatu_mikomi_kensuu] ")         '4Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[4gatu_mikomi_kingaku] ")        '4Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[4gatu_mikomi_arari] ")          '4Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_kensuu] ")         '5Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_kingaku] ")        '5Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_arari] ")          '5Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_kensuu] ")         '6Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_kingaku] ")        '6Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_arari] ")          '6Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_kensuu] ")         '7Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_kingaku] ")        '7Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_arari] ")          '7Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_kensuu] ")         '8Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_kingaku] ")        '8Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_arari] ")          '8Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_kensuu] ")         '9Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_kingaku] ")        '9Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_arari] ")          '9Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_kensuu] ")        '10Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_kingaku] ")       '10Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_arari] ")         '10Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_kensuu] ")        '11Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_kingaku] ")       '11Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_arari] ")         '11Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_kensuu] ")        '12Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_kingaku] ")       '12Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_arari] ")         '12Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_kensuu] ")         '1Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_kingaku] ")        '1Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_arari] ")          '1Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_kensuu] ")         '2Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_kingaku] ")        '2Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_arari] ")          '2Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_kensuu] ")         '3Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_kingaku] ")        '3Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_arari] ")          '3Œ_Œ©‘e—˜


                .AppendLine("  ,t_yotei_mikomi_kanri.keikaku_nendo ")
                .AppendLine("  ,t_yotei_mikomi_kanri.kameiten_cd  ")
                .AppendLine("  ,t_yotei_mikomi_kanri.keikaku_kanri_syouhin_cd AS syouhin_cd")
                .AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   'Œ©_SDS‘I‘ğ’Ç‰Á‹àŠz
                .AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4Œ_Œ©_SS_SDS“ü—Í
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
                .AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3Œ_Œ©_SS_SDS“ü—Í

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

                .AppendLine(" ,sum([4gatu_jisseki_kensuu] ) AS [4gatu_jisseki_kensuu] ") '4Œ_ÀÑŒ”
                .AppendLine(" ,sum([4gatu_jisseki_kingaku] ) AS [4gatu_jisseki_kingaku] ") '4Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([4gatu_jisseki_arari] ) AS [4gatu_jisseki_arari] ") '4Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([5gatu_jisseki_kensuu] ) AS [5gatu_jisseki_kensuu] ") '5Œ_ÀÑŒ”
                .AppendLine(" ,sum([5gatu_jisseki_kingaku] ) AS [5gatu_jisseki_kingaku] ") '5Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([5gatu_jisseki_arari] ) AS [5gatu_jisseki_arari] ") '5Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([6gatu_jisseki_kensuu] ) AS [6gatu_jisseki_kensuu] ") '6Œ_ÀÑŒ”
                .AppendLine(" ,sum([6gatu_jisseki_kingaku] ) AS [6gatu_jisseki_kingaku] ") '6Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([6gatu_jisseki_arari] ) AS [6gatu_jisseki_arari] ") '6Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([7gatu_jisseki_kensuu] ) AS [7gatu_jisseki_kensuu] ") '7Œ_ÀÑŒ”
                .AppendLine(" ,sum([7gatu_jisseki_kingaku] ) AS [7gatu_jisseki_kingaku] ") '7Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([7gatu_jisseki_arari] ) AS [7gatu_jisseki_arari] ") '7Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([8gatu_jisseki_kensuu] ) AS [8gatu_jisseki_kensuu] ") '8Œ_ÀÑŒ”
                .AppendLine(" ,sum([8gatu_jisseki_kingaku] ) AS [8gatu_jisseki_kingaku] ") '8Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([8gatu_jisseki_arari] ) AS [8gatu_jisseki_arari] ") '8Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([9gatu_jisseki_kensuu] ) AS [9gatu_jisseki_kensuu] ") '9Œ_ÀÑŒ”
                .AppendLine(" ,sum([9gatu_jisseki_kingaku] ) AS [9gatu_jisseki_kingaku] ") '9Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([9gatu_jisseki_arari] ) AS [9gatu_jisseki_arari] ") '9Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([10gatu_jisseki_kensuu] ) AS [10gatu_jisseki_kensuu] ") '10Œ_ÀÑŒ”
                .AppendLine(" ,sum([10gatu_jisseki_kingaku] ) AS [10gatu_jisseki_kingaku] ") '10Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([10gatu_jisseki_arari] ) AS [10gatu_jisseki_arari] ") '10Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([11gatu_jisseki_kensuu] ) AS [11gatu_jisseki_kensuu] ") '11Œ_ÀÑŒ”
                .AppendLine(" ,sum([11gatu_jisseki_kingaku] ) AS [11gatu_jisseki_kingaku] ") '11Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([11gatu_jisseki_arari] ) AS [11gatu_jisseki_arari] ") '11Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([12gatu_jisseki_kensuu] ) AS [12gatu_jisseki_kensuu] ") '12Œ_ÀÑŒ”
                .AppendLine(" ,sum([12gatu_jisseki_kingaku] ) AS [12gatu_jisseki_kingaku] ") '12Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([12gatu_jisseki_arari] ) AS [12gatu_jisseki_arari] ") '12Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([1gatu_jisseki_kensuu] ) AS [1gatu_jisseki_kensuu] ") '1Œ_ÀÑŒ”
                .AppendLine(" ,sum([1gatu_jisseki_kingaku] ) AS [1gatu_jisseki_kingaku] ") '1Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([1gatu_jisseki_arari] ) AS [1gatu_jisseki_arari] ") '1Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([2gatu_jisseki_kensuu] ) AS [2gatu_jisseki_kensuu] ") '2Œ_ÀÑŒ”
                .AppendLine(" ,sum([2gatu_jisseki_kingaku] ) AS [2gatu_jisseki_kingaku] ") '2Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([2gatu_jisseki_arari] ) AS [2gatu_jisseki_arari] ") '2Œ_ÀÑ‘e—˜
                .AppendLine(" ,sum([3gatu_jisseki_kensuu] ) AS [3gatu_jisseki_kensuu] ") '3Œ_ÀÑŒ”
                .AppendLine(" ,sum([3gatu_jisseki_kingaku] ) AS [3gatu_jisseki_kingaku] ") '3Œ_ÀÑ‹àŠz
                .AppendLine(" ,sum([3gatu_jisseki_arari] ) AS [3gatu_jisseki_arari] ") '3Œ_ÀÑ‘e—˜
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
    ''' SQL‚ÌŒ‹‡‚ÆŒŸõğŒ‚Ì‹¤’Êˆ—
    ''' </summary>
    ''' <param name="KeikakuKanriRecord">ŒŸõğŒ</param>
    ''' <param name="SelectKbn">–¾×•”SQL‹æ•ª</param>
    ''' <param name="paramList">ƒpƒ‰ƒ[ƒ^‚Ìİ’è</param>
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

                '”N“x
                strTmp = KeikakuKanriRecord.Nendo
                If strTmp <> "" Then
                    .AppendLine(" AND MKK.keikaku_nendo=@keikaku_nendo  ")
                    paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, KeikakuKanriRecord.Nendo))
                End If
                If Not (SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.FC OrElse SelectKbn = Utilities.KeikakuKanriRecord.selectKbn.goukeiFC) Then

                    'x“X(•”)
                    strTmp = KeikakuKanriRecord.Shiten
                    If strTmp <> "" Then
                        .AppendLine(" AND MKK.busyo_cd=@shiten_mei  ")
                        paramList.Add(MakeParam("@shiten_mei", SqlDbType.VarChar, 4, strTmp))
                    End If
                    'ƒ†[ƒU[(‰c‹Æƒ}ƒ“)
                    strTmp = KeikakuKanriRecord.User
                    If strTmp <> "" Then
                        .AppendLine(" AND MKK.eigyou_tantousya_id=@eigyou_tantousya_mei  ")
                        paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 30, strTmp))
                    End If
                    '‰Á–¿“X(“o˜^–‹ÆÒ)
                    strTmp = KeikakuKanriRecord.Kameiten
                    If strTmp <> "" Then
                        .AppendLine(" AND MKK.kameiten_cd=@kameiten_mei  ")
                        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 8, strTmp))
                    End If

                    '‰c‹ÆŠ
                    strTmp = KeikakuKanriRecord.Eigyou
                    If strTmp <> "" Then
                        .AppendLine(" AND MKK.eigyousyo_busyo_cd=@eigyousyo_mei  ")
                        paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 5, strTmp))
                    End If

                End If
            End With

            '‰c‹Æ‹æ•ª
            strTmp = sqlBuffer.ToString
            sqlBuffer.Remove(0, sqlBuffer.Length)
            If KeikakuKanriRecord.EigyouKBN.Eigyou OrElse KeikakuKanriRecord.EigyouKBN.EigyouNew Then '‰c‹Æ
                sqlEigyou.AppendLine(strTmp)
                sqlEigyou.AppendLine(" AND MKK.eigyou_kbn=@Eigyou  ")
                paramList.Add(MakeParam("@Eigyou", SqlDbType.Char, 1, "1"))

                If (KeikakuKanriRecord.EigyouKBN.Eigyou) AndAlso (Not KeikakuKanriRecord.EigyouKBN.EigyouNew) Then
                    '‰c‹Æ
                    sqlEigyou.AppendLine(" AND ISNULL(MKK.kameiten_sinki_flg,0)=@sinki0  ")
                ElseIf (Not KeikakuKanriRecord.EigyouKBN.Eigyou) AndAlso (KeikakuKanriRecord.EigyouKBN.EigyouNew) Then
                    '‰c‹Æ(V‹K)
                    sqlEigyou.AppendLine(" AND ISNULL(MKK.kameiten_sinki_flg,0)=@sinki1  ")
                End If

                'i‚İ‘I‘ğ
                intSuu = 0
                If KeikakuKanriRecord.Siborikomi.KeikakuTi Then 'Œv‰æ’l0
                    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                    sqlSiborikomi.AppendLine(" AND MKK.keikaku0_flg=@KeikakuTi  ")

                    intSuu = intSuu + 1
                End If
                'If KeikakuKanriRecord.Siborikomi.SinkiTouroku Then 'V‹K“o˜^–‹ÆŠ
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.kameiten_sinki_flg=@SinkiTouroku  ")

                '    intSuu = intSuu + 1
                'End If
                'If KeikakuKanriRecord.Siborikomi.Bunjyou Then '•ª÷50Ğ
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.bunjyou_50flg=@Bunjyou  ")

                '    intSuu = intSuu + 1
                'End If

                'If KeikakuKanriRecord.Siborikomi.Tyumon Then '’•¶50Ğ
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
            If KeikakuKanriRecord.EigyouKBN.Tokuhan OrElse KeikakuKanriRecord.EigyouKBN.TokuhanNew Then '“Á”Ì

                sqlEigyou.AppendLine(strTmp)
                sqlEigyou.AppendLine(" AND MKK.eigyou_kbn=@Tokuhan  ")
                paramList.Add(MakeParam("@Tokuhan", SqlDbType.Char, 1, "3"))

                If (KeikakuKanriRecord.EigyouKBN.Tokuhan) AndAlso (Not KeikakuKanriRecord.EigyouKBN.TokuhanNew) Then
                    '“Á”Ì
                    sqlEigyou.AppendLine(" AND ISNULL(MKK.kameiten_sinki_flg,0)=@sinki0  ")
                ElseIf (Not KeikakuKanriRecord.EigyouKBN.Tokuhan) AndAlso (KeikakuKanriRecord.EigyouKBN.TokuhanNew) Then
                    '“Á”Ì(V‹K)
                    sqlEigyou.AppendLine(" AND ISNULL(MKK.kameiten_sinki_flg,0)=@sinki1  ")
                End If

                'i‚İ‘I‘ğ
                intSuu = 0
                If KeikakuKanriRecord.Siborikomi.KeikakuTi Then 'Œv‰æ’l0
                    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                    sqlSiborikomi.AppendLine(" AND MKK.keikaku0_flg=@KeikakuTi  ")

                    intSuu = intSuu + 1
                End If
                'If KeikakuKanriRecord.Siborikomi.SinkiTouroku Then 'V‹K“o˜^–‹ÆŠ
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.kameiten_sinki_flg=@SinkiTouroku  ")

                '    intSuu = intSuu + 1
                'End If
                'If KeikakuKanriRecord.Siborikomi.Bunjyou Then '•ª÷50Ğ
                '    If intSuu <> 0 Then
                '        sqlSiborikomi.AppendLine(" UNION ")
                '    End If
                '    sqlSiborikomi.AppendLine(sqlEigyou.ToString)
                '    sqlSiborikomi.AppendLine(" AND MKK.bunjyou_50flg=@Bunjyou  ")

                '    intSuu = intSuu + 1
                'End If

                'If KeikakuKanriRecord.Siborikomi.Tyumon Then '’•¶50Ğ
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
            'If KeikakuKanriRecord.EigyouKBN.Sinki Then 'V‹K
            '    sqlEigyou.AppendLine(strTmp)
            '    sqlEigyou.AppendLine(" AND MKK.eigyou_kbn=@Sinki  ")
            '    paramList.Add(MakeParam("@Sinki", SqlDbType.Char, 1, "2"))

            '    'i‚İ‘I‘ğ
            '    intSuu = 0
            '    If KeikakuKanriRecord.Siborikomi.KeikakuTi Then 'Œv‰æ’l0
            '        sqlSiborikomi.AppendLine(sqlEigyou.ToString)
            '        sqlSiborikomi.AppendLine(" AND MKK.keikaku0_flg=@KeikakuTi  ")

            '        intSuu = intSuu + 1
            '    End If
            '    If KeikakuKanriRecord.Siborikomi.SinkiTouroku Then 'V‹K“o˜^–‹ÆŠ
            '        If intSuu <> 0 Then
            '            sqlSiborikomi.AppendLine(" UNION ")
            '        End If
            '        sqlSiborikomi.AppendLine(sqlEigyou.ToString)
            '        sqlSiborikomi.AppendLine(" AND MKK.kameiten_sinki_flg=@SinkiTouroku  ")

            '        intSuu = intSuu + 1
            '    End If
            '    If KeikakuKanriRecord.Siborikomi.Bunjyou Then '•ª÷50Ğ
            '        If intSuu <> 0 Then
            '            sqlSiborikomi.AppendLine(" UNION ")
            '        End If
            '        sqlSiborikomi.AppendLine(sqlEigyou.ToString)
            '        sqlSiborikomi.AppendLine(" AND MKK.bunjyou_50flg=@Bunjyou  ")

            '        intSuu = intSuu + 1
            '    End If

            '    If KeikakuKanriRecord.Siborikomi.Tyumon Then '’•¶50Ğ
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
                .AppendLine("	,MKKI.keikakuyou_nenkan_tousuu ") '--Œv‰æ—p_”NŠÔ“” ")
                .AppendLine("	,MKKI.sds_kaisi_nengetu ") '--SDSŠJn”NŒ ")
                .AppendLine("FROM ")
                .AppendLine("	( ")
                .AppendLine(strTmp)
                .AppendLine("	) AS SUB_MKK ")
                .AppendLine("	LEFT JOIN")
                .AppendLine("	m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED) ") '--Œv‰æŠÇ—_‰Á–¿“Xî•ñÏ½À ")
                .AppendLine("		ON ")
                .AppendLine("			SUB_MKK.kameiten_cd = MKKI.kameiten_cd ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_todoufuken AS MT WITH(READCOMMITTED) ") '--“s“¹•{Œ§Ï½À ")
                .AppendLine("		ON ")
                .AppendLine("			SUB_MKK.todouhuken_cd = MT.todouhuken_cd ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_eigyou_tantousya_syozoku_info AS METSI WITH(READCOMMITTED) ") '--‰c‹Æ’S“–Ò_Š‘®î•ñÏ½À ")
                .AppendLine("		ON ")
                .AppendLine("			SUB_MKK.eigyou_tantousya_id = METSI.eigyou_tantousya_id ")
                .AppendLine("WHERE ")
                .AppendLine("	1 = 1 ")
                If Not KeikakuKanriRecord.Syozoku.Equals(String.Empty) Then
                    .AppendLine("	AND METSI.syozoku_cd = @syozoku_cd ") '--Š‘® ")
                    paramList.Add(MakeParam("@syozoku_cd", SqlDbType.Int, 10, KeikakuKanriRecord.Syozoku)) 'Š‘®
                End If
                If Not KeikakuKanriRecord.Todoufuken.Equals(String.Empty) Then
                    .AppendLine("	AND MT.todouhuken_cd = @todouhuken_cd ") '--“s“¹•{Œ§ ")
                    paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, KeikakuKanriRecord.Todoufuken)) '“s“¹•{Œ§
                End If
                If Not KeikakuKanriRecord.TouituHoujin.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.touitsu_houjin_cd = @touitsu_houjin_cd ") '--“ˆê–@l ")
                    paramList.Add(MakeParam("@touitsu_houjin_cd", SqlDbType.VarChar, 8, KeikakuKanriRecord.TouituHoujin)) '“ˆê–@l
                End If
                If Not KeikakuKanriRecord.Houjin.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.houjin_cd = @houjin_cd ") '--–@l ")
                    paramList.Add(MakeParam("@houjin_cd", SqlDbType.VarChar, 8, KeikakuKanriRecord.Houjin)) '“ˆê–@l
                End If
                If Not KeikakuKanriRecord.Zokusei1.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_1 = @kameiten_zokusei_1 ") '--‘®«1 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei1)) '‘®«1
                End If
                If Not KeikakuKanriRecord.Zokusei2.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_2 = @kameiten_zokusei_2 ") '--‘®«2 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei2)) '‘®«2
                End If
                If Not KeikakuKanriRecord.Zokusei3.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_3 = @kameiten_zokusei_3 ") '--‘®«3 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei3)) '‘®«3
                End If
                If Not KeikakuKanriRecord.Zokusei4.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_4 = @kameiten_zokusei_4 ") '--‘®«4 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei4)) '‘®«4
                End If
                If Not KeikakuKanriRecord.Zokusei5.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_5 = @kameiten_zokusei_5 ") '--‘®«5 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.Int, 10, KeikakuKanriRecord.Zokusei5)) '‘®«5
                End If
                If Not KeikakuKanriRecord.Zokusei6.Equals(String.Empty) Then
                    .AppendLine("	AND MKKI.kameiten_zokusei_6 = @kameiten_zokusei_6 ") '--‘®«6 ")
                    paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, KeikakuKanriRecord.Zokusei6)) '‘®«6
                End If
                If (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) OrElse (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                    If KeikakuKanriRecord.NenkanTousuuHani.Keikakuyou Then
                        'Œv‰æ—p
                        If (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) AndAlso (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                            'from‚Æto
                            .AppendLine("	AND MKKI.keikakuyou_nenkan_tousuu BETWEEN @keikakuyou_nenkan_tousuu_from AND @keikakuyou_nenkan_tousuu_to ") '--Œv‰æ—p_”NŠÔ“” ")
                        ElseIf (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) AndAlso (KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                            'from‚Ì‚İ
                            .AppendLine("	AND MKKI.keikakuyou_nenkan_tousuu >= @keikakuyou_nenkan_tousuu_from  ") '--Œv‰æ—p_”NŠÔ“” ")
                        End If
                    Else
                        '”ñŒv‰æ—p
                        If (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) AndAlso (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                            'from‚Æto
                            .AppendLine("	AND SUB_MKK.keikaku_nenkan_tousuu BETWEEN @keikakuyou_nenkan_tousuu_from AND @keikakuyou_nenkan_tousuu_to ") '--”NŠÔ“” ")
                        ElseIf (Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty)) AndAlso (KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty)) Then
                            'from‚Ì‚İ
                            .AppendLine("	AND SUB_MKK.keikaku_nenkan_tousuu >= @keikakuyou_nenkan_tousuu_from  ") '--”NŠÔ“” ")
                        End If
                    End If
                    If Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom.Equals(String.Empty) Then
                        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuFrom)) '”NŠÔ“”From
                    Else
                        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_from", SqlDbType.Int, 10, DBNull.Value)) '”NŠÔ“”From
                    End If
                    If Not KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo.Equals(String.Empty) Then
                        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_to", SqlDbType.Int, 10, KeikakuKanriRecord.NenkanTousuuHani.NenkanTouSuuTo)) '”NŠÔ“”To
                    Else
                        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu_to", SqlDbType.Int, 10, DBNull.Value)) '”NŠÔ“”To
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
                .AppendLine("  t_keikaku_kanri.[4gatu_keisanyou_koj_hantei_ritu] ")      '4Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_koj_jyuchuu_ritu] ")     '4Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_tyoku_koj_ritu] ")       '4Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_koj_hantei_ritu] ")      '5Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_koj_jyuchuu_ritu] ")     '5Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_tyoku_koj_ritu] ")       '5Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_koj_hantei_ritu] ")      '6Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_koj_jyuchuu_ritu] ")     '6Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_tyoku_koj_ritu] ")       '6Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_koj_hantei_ritu] ")      '7Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_koj_jyuchuu_ritu] ")     '7Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_tyoku_koj_ritu] ")       '7Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_koj_hantei_ritu] ")      '8Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_koj_jyuchuu_ritu] ")     '8Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_tyoku_koj_ritu] ")       '8Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_koj_hantei_ritu] ")      '9Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_koj_jyuchuu_ritu] ")     '9Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_tyoku_koj_ritu] ")       '9Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_koj_hantei_ritu] ")     '10Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_koj_jyuchuu_ritu] ")    '10Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_tyoku_koj_ritu] ")      '10Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_koj_hantei_ritu] ")     '11Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_koj_jyuchuu_ritu] ")    '11Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_tyoku_koj_ritu] ")      '11Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_koj_hantei_ritu] ")     '12Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_koj_jyuchuu_ritu] ")    '12Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_tyoku_koj_ritu] ")      '12Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_koj_hantei_ritu] ")      '1Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_koj_jyuchuu_ritu] ")     '1Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_tyoku_koj_ritu] ")       '1Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_koj_hantei_ritu] ")      '2Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_koj_jyuchuu_ritu] ")     '2Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_tyoku_koj_ritu] ")       '2Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_koj_hantei_ritu] ")      '3Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_koj_jyuchuu_ritu] ")     '3Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_tyoku_koj_ritu] ")       '3Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_kensuu] ")         '4Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_kingaku] ")        '4Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_arari] ")          '4Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_kensuu] ")         '5Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_kingaku] ")        '5Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_arari] ")          '5Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_kensuu] ")         '6Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_kingaku] ")        '6Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_arari] ")          '6Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_kensuu] ")         '7Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_kingaku] ")        '7Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_arari] ")          '7Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_kensuu] ")         '8Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_kingaku] ")        '8Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_arari] ")          '8Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_kensuu] ")         '9Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_kingaku] ")        '9Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_arari] ")          '9Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_kensuu] ")        '10Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_kingaku] ")       '10Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_arari] ")         '10Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_kensuu] ")        '11Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_kingaku] ")       '11Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_arari] ")         '11Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_kensuu] ")        '12Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_kingaku] ")       '12Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_arari] ")         '12Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_kensuu] ")         '1Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_kingaku] ")        '1Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_arari] ")          '1Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_kensuu] ")         '2Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_kingaku] ")        '2Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_arari] ")          '2Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_kensuu] ")         '3Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_kingaku] ")        '3Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_arari] ")          '3Œ_Œv‰æ‘e—˜
                .AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ")  'Œv‰æ_SDS‘I‘ğ’Ç‰Á‹àŠz
                .AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4Œ_Œv‰æ_SS_SDS“ü—Í
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
                .AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3Œ_Œv‰æ_SS_SDS“ü—Í

                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_uri_heikin_tanka] ")       '4Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_siire_heikin_tanka] ")     '4Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_uri_heikin_tanka] ")       '5Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_siire_heikin_tanka] ")     '5Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_uri_heikin_tanka] ")       '6Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_siire_heikin_tanka] ")     '6Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_uri_heikin_tanka] ")       '7Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_siire_heikin_tanka] ")     '7Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_uri_heikin_tanka] ")       '8Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_siire_heikin_tanka] ")     '8Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_uri_heikin_tanka] ")       '9Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_siire_heikin_tanka] ")     '9Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_uri_heikin_tanka] ")      '10Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_siire_heikin_tanka] ")    '10Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_uri_heikin_tanka] ")      '11Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_siire_heikin_tanka] ")    '11Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_uri_heikin_tanka] ")      '12Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_siire_heikin_tanka] ")    '12Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_uri_heikin_tanka] ")       '1Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_siire_heikin_tanka] ")     '1Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_uri_heikin_tanka] ")       '2Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_siire_heikin_tanka] ")     '2Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_uri_heikin_tanka] ")       '3Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_siire_heikin_tanka] ")     '3Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿

                .AppendLine("  ,t_keikaku_kanri2.keikakuyou_nenkan_tousuu ")    'Œv‰æ—p_”NŠÔ“”
                .AppendLine("  ,t_keikaku_kanri2.sds_kaisi_nengetu ")           'SDSŠJn”NŒ

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

                .AppendLine("  SELECT t_keikaku_kanri.[4gatu_keisanyou_koj_hantei_ritu] ")      '4Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_koj_jyuchuu_ritu] ")     '4Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_tyoku_koj_ritu] ")       '4Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_koj_hantei_ritu] ")      '5Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_koj_jyuchuu_ritu] ")     '5Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_tyoku_koj_ritu] ")       '5Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_koj_hantei_ritu] ")      '6Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_koj_jyuchuu_ritu] ")     '6Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_tyoku_koj_ritu] ")       '6Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_koj_hantei_ritu] ")      '7Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_koj_jyuchuu_ritu] ")     '7Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_tyoku_koj_ritu] ")       '7Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_koj_hantei_ritu] ")      '8Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_koj_jyuchuu_ritu] ")     '8Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_tyoku_koj_ritu] ")       '8Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_koj_hantei_ritu] ")      '9Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_koj_jyuchuu_ritu] ")     '9Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_tyoku_koj_ritu] ")       '9Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_koj_hantei_ritu] ")     '10Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_koj_jyuchuu_ritu] ")    '10Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_tyoku_koj_ritu] ")      '10Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_koj_hantei_ritu] ")     '11Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_koj_jyuchuu_ritu] ")    '11Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_tyoku_koj_ritu] ")      '11Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_koj_hantei_ritu] ")     '12Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_koj_jyuchuu_ritu] ")    '12Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_tyoku_koj_ritu] ")      '12Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_koj_hantei_ritu] ")      '1Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_koj_jyuchuu_ritu] ")     '1Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_tyoku_koj_ritu] ")       '1Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_koj_hantei_ritu] ")      '2Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_koj_jyuchuu_ritu] ")     '2Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_tyoku_koj_ritu] ")       '2Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_koj_hantei_ritu] ")      '3Œ_ŒvZ—p_H–”»’è—¦
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_koj_jyuchuu_ritu] ")     '3Œ_ŒvZ—p_H–ó’—¦
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_tyoku_koj_ritu] ")       '3Œ_ŒvZ—p_’¼H–—¦
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_kensuu] ")         '4Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_kingaku] ")        '4Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keikaku_arari] ")          '4Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_kensuu] ")         '5Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_kingaku] ")        '5Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keikaku_arari] ")          '5Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_kensuu] ")         '6Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_kingaku] ")        '6Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keikaku_arari] ")          '6Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_kensuu] ")         '7Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_kingaku] ")        '7Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keikaku_arari] ")          '7Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_kensuu] ")         '8Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_kingaku] ")        '8Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keikaku_arari] ")          '8Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_kensuu] ")         '9Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_kingaku] ")        '9Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keikaku_arari] ")          '9Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_kensuu] ")        '10Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_kingaku] ")       '10Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keikaku_arari] ")         '10Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_kensuu] ")        '11Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_kingaku] ")       '11Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keikaku_arari] ")         '11Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_kensuu] ")        '12Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_kingaku] ")       '12Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keikaku_arari] ")         '12Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_kensuu] ")         '1Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_kingaku] ")        '1Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keikaku_arari] ")          '1Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_kensuu] ")         '2Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_kingaku] ")        '2Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keikaku_arari] ")          '2Œ_Œv‰æ‘e—˜
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_kensuu] ")         '3Œ_Œv‰æŒ”
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_kingaku] ")        '3Œ_Œv‰æ‹àŠz
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keikaku_arari] ")          '3Œ_Œv‰æ‘e—˜
                .AppendLine("  ,NULL AS [keikaku_sds_tuika_kingaku] ")  'Œv‰æ_SDS‘I‘ğ’Ç‰Á‹àŠz
                .AppendLine("  ,NULL AS [4gatu_keikaku_ss_sds] ")       '4Œ_Œv‰æ_SS_SDS“ü—Í
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
                .AppendLine("  ,NULL AS [3gatu_keikaku_ss_sds] ")       '3Œ_Œv‰æ_SS_SDS“ü—Í

                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_uri_heikin_tanka] ")       '4Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[4gatu_keisanyou_siire_heikin_tanka] ")     '4Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_uri_heikin_tanka] ")       '5Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[5gatu_keisanyou_siire_heikin_tanka] ")     '5Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_uri_heikin_tanka] ")       '6Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[6gatu_keisanyou_siire_heikin_tanka] ")     '6Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_uri_heikin_tanka] ")       '7Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[7gatu_keisanyou_siire_heikin_tanka] ")     '7Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_uri_heikin_tanka] ")       '8Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[8gatu_keisanyou_siire_heikin_tanka] ")     '8Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_uri_heikin_tanka] ")       '9Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[9gatu_keisanyou_siire_heikin_tanka] ")     '9Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_uri_heikin_tanka] ")      '10Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[10gatu_keisanyou_siire_heikin_tanka] ")    '10Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_uri_heikin_tanka] ")      '11Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[11gatu_keisanyou_siire_heikin_tanka] ")    '11Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_uri_heikin_tanka] ")      '12Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[12gatu_keisanyou_siire_heikin_tanka] ")    '12Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_uri_heikin_tanka] ")       '1Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[1gatu_keisanyou_siire_heikin_tanka] ")     '1Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_uri_heikin_tanka] ")       '2Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[2gatu_keisanyou_siire_heikin_tanka] ")     '2Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_uri_heikin_tanka] ")       '3Œ_ŒvZ—p__”„ã•½‹Ï’P‰¿
                .AppendLine("  ,t_keikaku_kanri.[3gatu_keisanyou_siire_heikin_tanka] ")     '3Œ_ŒvZ—p__d“ü•½‹Ï’P‰¿

                .AppendLine("  ,NULL AS keikakuyou_nenkan_tousuu ")    'Œv‰æ—p_”NŠÔ“”
                .AppendLine("  ,NULL AS sds_kaisi_nengetu ")           'SDSŠJn”NŒ

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
                .AppendLine("  t_yotei_mikomi_kanri.[4gatu_mikomi_kensuu] ")         '4Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[4gatu_mikomi_kingaku] ")        '4Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[4gatu_mikomi_arari] ")          '4Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_kensuu] ")         '5Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_kingaku] ")        '5Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[5gatu_mikomi_arari] ")          '5Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_kensuu] ")         '6Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_kingaku] ")        '6Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[6gatu_mikomi_arari] ")          '6Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_kensuu] ")         '7Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_kingaku] ")        '7Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[7gatu_mikomi_arari] ")          '7Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_kensuu] ")         '8Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_kingaku] ")        '8Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[8gatu_mikomi_arari] ")          '8Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_kensuu] ")         '9Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_kingaku] ")        '9Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[9gatu_mikomi_arari] ")          '9Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_kensuu] ")        '10Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_kingaku] ")       '10Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[10gatu_mikomi_arari] ")         '10Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_kensuu] ")        '11Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_kingaku] ")       '11Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[11gatu_mikomi_arari] ")         '11Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_kensuu] ")        '12Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_kingaku] ")       '12Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[12gatu_mikomi_arari] ")         '12Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_kensuu] ")         '1Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_kingaku] ")        '1Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[1gatu_mikomi_arari] ")          '1Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_kensuu] ")         '2Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_kingaku] ")        '2Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[2gatu_mikomi_arari] ")          '2Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_kensuu] ")         '3Œ_Œ©Œ”
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_kingaku] ")        '3Œ_Œ©‹àŠz
                .AppendLine("  ,t_yotei_mikomi_kanri.[3gatu_mikomi_arari] ")          '3Œ_Œ©‘e—˜
                .AppendLine("  ,t_yotei_mikomi_kanri.keikaku_nendo ")
                .AppendLine("  ,t_yotei_mikomi_kanri.add_datetime ")
                .AppendLine("  ,t_yotei_mikomi_kanri.busyo_cd ")
                .AppendLine("  ,t_yotei_mikomi_kanri2.syouhin_cd ")

                .AppendLine("  ,NULL AS mikomi_sds_tuika_kingaku ")   'Œ©_SDS‘I‘ğ’Ç‰Á‹àŠz
                .AppendLine("  ,NULL AS [4gatu_mikomi_ss_sds] ")        '4Œ_Œ©_SS_SDS“ü—Í
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
                .AppendLine("  ,NULL AS [3gatu_mikomi_ss_sds] ")        '3Œ_Œ©_SS_SDS“ü—Í



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
    ''' Œv‰æŠÇ—ƒe[ƒuƒ‹‚ÌXVˆ—
    ''' </summary>
    ''' <param name="strKousin">XVğŒ</param>
    ''' <param name="strKousinId">XVID</param>
    ''' <param name="blnKakutei">Šm’è‹æ•ª</param>
    ''' <returns></returns>
    ''' <remarks>2012/11/14 P-44979 ‚ V‹Kì¬</remarks>
    Public Function UpdKeikakuKanri(ByVal strKousin As String, ByVal strKousinId As String, ByVal blnKakutei As Boolean) As Integer
        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKousin, blnKakutei)
        'SQL•¶‚Ì¶¬
        Dim commandTextSb As New StringBuilder

        'ƒpƒ‰ƒ[ƒ^Ši”[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL•¶
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


        'ƒpƒ‰ƒ[ƒ^‚Ìİ’è
        With paramList

            .Add(MakeParam("@flg", SqlDbType.Int, 1, "1"))
            .Add(MakeParam("@flg2", SqlDbType.Int, 1, "0"))
            .Add(MakeParam("@kousin_id", SqlDbType.VarChar, 30, strKousinId))
        End With

        ' ƒNƒGƒŠÀs
        Return ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
    End Function
    ''' <summary>
    ''' ‘SĞŒv‰æŠÇ—ƒe[ƒuƒ‹‚©‚çŒv‰æî•ñ‚ğæ“¾‚·‚é
    ''' </summary>
    ''' <param name="KeikakuKanriRecord">ŒŸõî•ñ</param>
    ''' <returns>‘SĞ‚ÌŒv‰æî•ñ</returns>
    ''' <remarks>‘SĞŒv‰æŠÇ—ƒe[ƒuƒ‹‚©‚çŒv‰æî•ñ‚ğæ“¾‚·‚é</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ‚ V‹Kì¬ </para>
    ''' </history>
    Public Function SelSitenbetuTukiData(ByVal KeikakuKanriRecord As KeikakuKanriRecord) As DataTable
        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, KeikakuKanriRecord)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'ƒpƒ‰ƒ[ƒ^Ši”[
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

        'ƒpƒ‰ƒ[ƒ^‚Ìİ’è
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, KeikakuKanriRecord.Nendo))     'Œv‰æ”N“x(YYYY)
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.Char, 4, KeikakuKanriRecord.Shiten))     'x“X
        'paramList.Add(MakeParam("@busyo_cd", SqlDbType.Char, 4, "0202"))     'x“X
        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function
End Class
