Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 計画_加盟店情報照会
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKameitenJyouhouInquiryDA

    ''' <summary>
    ''' 基本情報データを取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/06　李宇(大連情報システム部)　新規作成</history>
    Public Function SelKihonJyouSyutoku(ByVal strKameitenCd As String, ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikakuNendo)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	 MKK.torikesi")                 '--計画管理_加盟店マスタ.取消
            .AppendLine("	,MKKM1.meisyou AS torikesiMei") '--計画用_拡張名称マスタ.名称
            .AppendLine("	,MKK.hattyuu_teisi_flg")        '--計画管理_加盟店マスタ.発注停止FLG
            .AppendLine("	,MKKM2.meisyou AS flgMei")      '--計画用_拡張名称マスタ.名称
            .AppendLine("	,MKKI.kbn")              '--計画管理_加盟店情報マスタ.区分
            .AppendLine("	,MDK.kbn_mei")           '--区分マスタ.区分名
            .AppendLine("	,MKK.kameiten_cd")       '--画管理_加盟店マスタ.加盟店ｺｰﾄﾞ
            .AppendLine("	,MKK.kameiten_mei")      '--計画管理_加盟店マスタ.加盟店名
            .AppendLine("	,MKK.eigyou_kbn")        '--計画管理_加盟店マスタ.営業区分
            .AppendLine("	,MKM.meisyou AS eigyouKbnMei") '--計画用_名称マスタ.名称
            .AppendLine("	,MKK.eigyou_tantousya_id")   '--計画管理_加盟店マスタ.営業担当者
            .AppendLine("	,MKK.eigyou_tantousya_mei")  '--計画管理_加盟店マスタ.営業担当者名
            .AppendLine("	,MKK.shiten_mei")            '--計画管理_加盟店マスタ.支店名
            .AppendLine("	,MKK.todouhuken_cd")         '--計画管理_加盟店マスタ.都道府県ｺｰﾄﾞ
            .AppendLine("	,MKK.todouhuken_mei")        '--計画管理_加盟店マスタ.都道府県名
            .AppendLine("	,MKK.eigyousyo_cd")          '--計画管理_加盟店マスタ.営業所ｺｰﾄﾞ
            .AppendLine("	,ME.eigyousyo_mei")          '--営業所マスタ.営業所名
            .AppendLine("	,MKK.keiretu_cd")            '--計画管理_加盟店マスタ.系列ｺｰﾄﾞ
            .AppendLine("	,MK.keiretu_mei")            '--系列マスタ.系列名
            .AppendLine("	,METSI.syozoku")             '--営業担当者_所属情報マスタ.所属
            .AppendLine("	,MKK.keikaku_nenkan_tousuu") '--計画管理_加盟店マスタ.年間棟数
            .AppendLine("	,CASE ")
            .AppendLine("		When MKK.keikaku0_flg = '0' Then ")
            .AppendLine("            '計画値有り' ")
            .AppendLine("		When MKK.keikaku0_flg = '1' Then  ")
            .AppendLine("           '計画値無し' ")
            .AppendLine("		When ISNULL(MKK.keikaku0_flg,'') = '' Then")
            .AppendLine("           ''")
            .AppendLine("		End AS keikaku0_flg ")  '--計画管理_加盟店マスタ.計画値0FLG
            .AppendLine("	,MKKI.add_login_user_id")    '--登録ログインユーザーID	
            .AppendLine("	,MKKI.add_datetime")         '--登録日時
            .AppendLine("	,MKKI.upd_login_user_id")    '--更新ログインユーザーID
            .AppendLine("	,MKKI.upd_datetime")         '--更新日時

            .AppendLine("FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")   '--計画管理_加盟店マスタ
            '--計画用_拡張名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_kakutyou_meisyou AS MKKM1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKM1.meisyou_syubetu = '56'")
            .AppendLine("	AND MKK.torikesi = MKKM1.code ")
            '--計画用_拡張名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_kakutyou_meisyou AS MKKM2 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKM2.meisyou_syubetu = '8'")
            .AppendLine("	AND MKK.hattyuu_teisi_flg = MKKM2.code ")
            '--計画管理_加盟店情報マスタ
            .AppendLine("	LEFT JOIN m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED)")
            .AppendLine("	ON MKK.kameiten_cd = MKKI.kameiten_cd ")
            '--計画用_名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM WITH(READCOMMITTED)")
            .AppendLine("	ON MKM.meisyou_syubetu = '05'")
            .AppendLine("	AND MKK.eigyou_kbn = MKM.code ")
            '--営業担当者_所属情報マスタ
            .AppendLine("	LEFT JOIN m_eigyou_tantousya_syozoku_info AS METSI WITH(READCOMMITTED)")
            .AppendLine("	ON MKK.eigyou_tantousya_id = METSI.eigyou_tantousya_id ")
            '--区分マスタ
            .AppendLine("	LEFT JOIN m_data_kbn AS MDK WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.kbn = MDK.kbn ")
            '--営業所マスタ
            .AppendLine("	LEFT JOIN m_eigyousyo AS ME WITH(READCOMMITTED)")
            .AppendLine("	ON MKK.eigyousyo_cd = ME.eigyousyo_cd ")
            '--系列マスタ
            .AppendLine("	LEFT JOIN m_keiretu AS MK WITH(READCOMMITTED)")
            .AppendLine("	ON MKK.keiretu_cd = MK.keiretu_cd ")
            .AppendLine("WHERE")
            .AppendLine("	MKK.kameiten_cd = @strKameitenCd")
            .AppendLine("	AND MKK.keikaku_nendo = @strNendo")
        End With

        paramList.Add(MakeParam("@strKameitenCd", SqlDbType.VarChar, 16, strKameitenCd))                 '加盟店コード
        paramList.Add(MakeParam("@strNendo", SqlDbType.Char, 4, strKeikakuNendo))                        '計画_年度

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKihonJyouhouSyutoku", paramList.ToArray())
        Return dsReturn.Tables("dtKihonJyouhouSyutoku")
    End Function

    ''' <summary>
    ''' 計画管理用_加盟店情報データ取得
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/06　李宇(大連情報システム部)　新規作成</history>
    Public Function SelKameitenJyouhou(ByVal strKameitenCd As String, ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikakuNendo)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	MKKI.keikakuyou_kameitenmei")    '--計画管理_加盟店情報マスタ.計画用_加盟店名
            .AppendLine("	,MKKI.gyoutai")                  '--計画管理_加盟店情報マスタ.業態
            '.AppendLine("	,(MKKI.gyoutai + ':' + MKM1.meisyou) AS gyoutaiMei")    '--計画用_名称マスタ.名称
            .AppendLine("	,MKKI.touitsu_houjin_cd")        '--計画管理_加盟店情報マスタ.統一法人コード
            .AppendLine("   ,MKK1.kameiten_mei AS touitsuHoujinMei")             '--計画管理_加盟店ﾏｽﾀ.加盟店名
            .AppendLine("	,MKKI.houjin_cd ")               '--計画管理_加盟店情報マスタ.法人コード
            .AppendLine("   ,MKK2.kameiten_mei AS houjinMei") '--計画管理_加盟店ﾏｽﾀ.加盟店名
            .AppendLine("	,MKKI.keikaku_nayose_cd")        '--計画管理_加盟店情報マスタ.計画名寄コード
            .AppendLine("   ,MKK3.kameiten_mei AS keikakuNayoseMei") '--計画管理_加盟店ﾏｽﾀ.加盟店名
            .AppendLine("	,MKKI.keikakuyou_nenkan_tousuu") '--計画管理_加盟店情報マスタ.計画用_年間棟数
            .AppendLine("	,MKKI.keikakuji_eigyou_tantousya_id")  '--計画管理_加盟店情報マスタ.計画時_営業担当者
            '.AppendLine("	,MKKI.keikakuji_eigyou_tantousya_mei") ' --計画管理_加盟店情報マスタ.計画時_営業担当者名
            .AppendLine("   ,MJM.DisplayName")                     '--社員アカウント情報マスタ.表示名
            .AppendLine("	,METSI.syozoku")                       '--営業担当者_所属情報マスタ.所属
            .AppendLine("	,MKKI.keikakuyou_eigyou_kbn")          '--計画管理_加盟店情報マスタ.計画用_営業区分
            .AppendLine("	,MKM2.meisyou AS eigyouKbnMei")        '--計画用_名称マスタ.名称_計画用_営業区分の内容取得
            .AppendLine("	,MKKI.keikakuyou_kannkatsu_siten")     '--計画管理_加盟店情報マスタ.計画用_管轄支店
            .AppendLine("	,MBK1.busyo_cd")                       '--部署管理マスタ.部署コード_計画用_管轄支店の内容取得
            .AppendLine("	,MKKI.keikakuyou_kannkatsu_siten_mei ") '--計画管理_加盟店情報マスタ.計画用_管轄支店名
            .AppendLine("	,MBK1.busyo_mei ")              '--部署管理マスタ.部署名_計画用_管轄支店の内容取得
            .AppendLine("	,MKKI.sds_kaisi_nengetu")       '--計画管理_加盟店情報マスタ.SDS開始年月
            .AppendLine("	,MKKI.kameiten_zokusei_1")      '--計画管理_加盟店情報マスタ.加盟店属性1
            .AppendLine("	,MKM3.meisyou AS zokusei1Mei")  '--計画用_名称マスタ.名称_加盟店属性1の内容取得
            .AppendLine("	,MKKI.kameiten_zokusei_2")      '--計画管理_加盟店情報マスタ.加盟店属性2
            .AppendLine("	,MKM4.meisyou AS zokusei2Mei")  '--計画用_名称マスタ.名称_加盟店属性2の内容取得
            .AppendLine("	,MKKI.kameiten_zokusei_3")      '--計画管理_加盟店情報マスタ.加盟店属性3
            .AppendLine("	,MKM5.meisyou AS zokusei3Mei")  '--計画用_名称マスタ.名称_加盟店属性3の内容取得
            .AppendLine("	,MKKI.kameiten_zokusei_4")      '--計画管理_加盟店情報マスタ.加盟店属性4
            .AppendLine("	,MKKM1.meisyou AS zokusei4Mei") '--計画用_拡張名称マスタ.名称_加盟店属性4の内容取得
            .AppendLine("	,MKKI.kameiten_zokusei_5")      '--計画管理_加盟店情報マスタ.加盟店属性5
            .AppendLine("	,MKKM2.meisyou AS zokusei5Mei") '--計画用_拡張名称マスタ.名称_加盟店属性5の内容取得
            .AppendLine("	,MKKI.kameiten_zokusei_6")      '--計画管理_加盟店情報マスタ.加盟店属性6
            .AppendLine("FROM")
            .AppendLine("	m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED)") '--計画管理_加盟店情報マスタ
            '--計画用_名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM1.meisyou_syubetu = '20'")
            .AppendLine("	AND MKKI.gyoutai = MKM1.code ")
            '--営業担当者_所属情報マスタ
            .AppendLine("	LEFT JOIN m_eigyou_tantousya_syozoku_info AS METSI WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.keikakuji_eigyou_tantousya_id = METSI.eigyou_tantousya_id ")
            '--計画用_名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM2 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM2.meisyou_syubetu = '05'")
            .AppendLine("	AND MKKI.keikakuyou_eigyou_kbn = MKM2.code ")
            '--部署管理マスタ
            .AppendLine("	LEFT JOIN m_busyo_kanri AS MBK1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.keikakuyou_kannkatsu_siten = MBK1.busyo_cd")
            .AppendLine("	AND MBK1.sosiki_level = '4'")
            '--計画用_名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM3 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM3.meisyou_syubetu = '21'")
            .AppendLine("	AND MKKI.kameiten_zokusei_1 = MKM3.code")
            '--計画用_名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM4 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM4.meisyou_syubetu = '22'")
            .AppendLine("	AND MKKI.kameiten_zokusei_2 = MKM4.code")
            '--計画用_名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_meisyou AS MKM5 WITH(READCOMMITTED)")
            .AppendLine("	ON MKM5.meisyou_syubetu = '23'")
            .AppendLine("	AND MKKI.kameiten_zokusei_3 = MKM5.code")
            '--計画用_拡張名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_kakutyou_meisyou AS MKKM1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKM1.meisyou_syubetu = '21'")
            .AppendLine("	AND MKKI.kameiten_zokusei_4 = MKKM1.code")
            '--計画用_拡張名称マスタ
            .AppendLine("	LEFT JOIN m_keikakuyou_kakutyou_meisyou AS MKKM2 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKM2.meisyou_syubetu = '22'")
            .AppendLine("	AND MKKI.kameiten_zokusei_5 = MKKM2.code")
            '計画管理_加盟店ﾏｽﾀ
            .AppendLine("	LEFT JOIN m_keikaku_kameiten AS MKK1 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.touitsu_houjin_cd = MKK1.kameiten_cd")
            .AppendLine("   AND MKK1.keikaku_nendo = @strNendo")
            '計画管理_加盟店ﾏｽﾀ
            .AppendLine("	LEFT JOIN m_keikaku_kameiten AS MKK2 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.houjin_cd = MKK2.kameiten_cd")
            .AppendLine("   AND MKK2.keikaku_nendo = @strNendo")
            '計画管理_加盟店ﾏｽﾀ
            .AppendLine("	LEFT JOIN m_keikaku_kameiten AS MKK3 WITH(READCOMMITTED)")
            .AppendLine("	ON MKKI.keikaku_nayose_cd = MKK3.kameiten_cd")
            .AppendLine("   AND MKK3.keikaku_nendo = @strNendo")
            '--社員アカウント情報マスタ
            .AppendLine("   LEFT JOIN m_jhs_mailbox AS MJM WITH(READCOMMITTED)")
            .AppendLine("   ON MKKI.keikakuji_eigyou_tantousya_id = MJM.PrimaryWindowsNTAccount")

            .AppendLine("WHERE")
            .AppendLine("	MKKI.kameiten_cd =@strKameitenCd")
        End With

        paramList.Add(MakeParam("@strKameitenCd", SqlDbType.VarChar, 5, strKameitenCd))                 '加盟店コード
        paramList.Add(MakeParam("@strNendo", SqlDbType.Char, 4, strKeikakuNendo))                        '計画_年度

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKameitenJyouhou", paramList.ToArray())
        Return dsReturn.Tables("dtKameitenJyouhou")

    End Function

    ''' <summary>
    ''' 画面の加盟店ｺｰﾄﾞが計画管理_加盟店情報マスタ.加盟店ｺｰﾄﾞに存在するかどうかを判断する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Public Function SelCount(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New System.Text.StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   count(kameiten_cd)")
            .AppendLine("FROM ")
            .AppendLine("   m_keikaku_kameiten_info WITH(READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @strKameitenCd ")
        End With

        paramList.Add(MakeParam("@strKameitenCd", SqlDbType.VarChar, 5, strKameitenCd))   '加盟店コード

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtCount", paramList.ToArray())
        Return dsReturn.Tables("dtCount")

    End Function


    ''' <summary>
    ''' 「計画管理_加盟店情報ﾏｽﾀ」の追加処理
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKbnMei">区分名</param>
    ''' <param name="strKeikakuyouKameitenmei">計画用_加盟店名</param>
    ''' <param name="inGyoutai">業態</param>
    ''' <param name="strTouitsuHoujinCd">統一法人コード</param>
    ''' <param name="strHoujinCd">法人コード</param>
    ''' <param name="strKeikakuNayoseCd">計画名寄コード</param>
    ''' <param name="inKeikakuyouNenkanTousuu">計画用_年間棟数</param>
    ''' <param name="strKeikakujiEigyouTantousyaId">計画時_営業担当者</param>
    ''' <param name="strKeikakujiEigyouTantousyaMei">計画時_営業担当者名</param>
    ''' <param name="inKeikakuyouEigyouKbn">計画用_営業区分</param>
    ''' <param name="strKeikakuyouKannkatsuSiten">計画用_管轄支店</param>
    ''' <param name="strKeikakuyouKannkatsuSitenMei">計画用_管轄支店名</param>
    ''' <param name="strSdsKaisiNengetu">SDS開始年月</param>
    ''' <param name="inKameitenZokusei1">加盟店属性1</param>
    ''' <param name="inKameitenZokusei2">加盟店属性2</param>
    ''' <param name="inKameitenZokusei3">加盟店属性3</param>
    ''' <param name="strKameitenZokusei4">加盟店属性4</param>
    ''' <param name="strKameitenZokusei5">加盟店属性5</param>
    ''' <param name="strKameitenZokusei6">加盟店属性6</param>
    ''' <param name="strAddLoginUserId">登録ログインユーザーID</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Public Function InsMKeikakuKameitenInfo(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strKbnMei As String, _
                                            ByVal strKeikakuyouKameitenmei As String, ByVal inGyoutai As String, ByVal strTouitsuHoujinCd As String, _
                                            ByVal strHoujinCd As String, ByVal strKeikakuNayoseCd As String, ByVal inKeikakuyouNenkanTousuu As String, _
                                            ByVal strKeikakujiEigyouTantousyaId As String, ByVal strKeikakujiEigyouTantousyaMei As String, _
                                            ByVal inKeikakuyouEigyouKbn As String, ByVal strKeikakuyouKannkatsuSiten As String, _
                                            ByVal strKeikakuyouKannkatsuSitenMei As String, ByVal strSdsKaisiNengetu As String, _
                                            ByVal inKameitenZokusei1 As String, ByVal inKameitenZokusei2 As String, _
                                            ByVal inKameitenZokusei3 As String, ByVal strKameitenZokusei4 As String, _
                                            ByVal strKameitenZokusei5 As String, ByVal strKameitenZokusei6 As String, _
                                            ByVal strAddLoginUserId As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strAddLoginUserId)

        'SQLコメント
        Dim commandTextSb As New System.Text.StringBuilder
        '更新数
        Dim insCount As Integer
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO m_keikaku_kameiten_info WITH(UPDLOCK)")
            .AppendLine("(")
            .AppendLine("	kameiten_cd ")              '--加盟店ｺｰﾄﾞ
            .AppendLine("	,kbn")                      '--区分
            .AppendLine("	,kbn_mei")                  '--区分名
            .AppendLine("	,keikakuyou_kameitenmei ")  '--計画用_加盟店名
            .AppendLine("	,gyoutai")                  '--業態
            .AppendLine("	,touitsu_houjin_cd")        '--統一法人コード
            .AppendLine("	,houjin_cd")                '--法人コード
            .AppendLine("	,keikaku_nayose_cd")                '--計画名寄コード
            .AppendLine("	,keikakuyou_nenkan_tousuu ")        '--計画用_年間棟数
            .AppendLine("	,keikakuji_eigyou_tantousya_id")    '--計画時_営業担当者
            .AppendLine("	,keikakuji_eigyou_tantousya_mei")   '--計画時_営業担当者名
            .AppendLine("	,keikakuyou_eigyou_kbn")            '--計画用_営業区分
            .AppendLine("	,keikakuyou_kannkatsu_siten ")      '--計画用_管轄支店(コード)
            .AppendLine("	,keikakuyou_kannkatsu_siten_mei")   '--計画用_管轄支店名
            .AppendLine("	,sds_kaisi_nengetu")  '--SDS開始年月
            .AppendLine("	,kameiten_zokusei_1") '--加盟店属性1
            .AppendLine("	,kameiten_zokusei_2") '--加盟店属性2
            .AppendLine("	,kameiten_zokusei_3") '--加盟店属性3
            .AppendLine("	,kameiten_zokusei_4") '--加盟店属性4
            .AppendLine("	,kameiten_zokusei_5") '--加盟店属性5
            .AppendLine("	,kameiten_zokusei_6") '--加盟店属性6
            .AppendLine("	,add_login_user_id")  '--登録ログインユーザーID
            .AppendLine("	,add_datetime")       '--登録日時
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("	@kameiten_cd")
            .AppendLine("	,@kbn")
            .AppendLine("	,@kbn_mei")
            .AppendLine("	,@keikakuyou_kameitenmei")
            .AppendLine("	,@gyoutai")
            .AppendLine("	,@touitsu_houjin_cd")
            .AppendLine("	,@houjin_cd ")
            .AppendLine("	,@keikaku_nayose_cd	")
            .AppendLine("	,@keikakuyou_nenkan_tousuu")
            .AppendLine("	,@keikakuji_eigyou_tantousya_id")
            .AppendLine("	,@keikakuji_eigyou_tantousya_mei")
            .AppendLine("	,@keikakuyou_eigyou_kbn")
            .AppendLine("	,@keikakuyou_kannkatsu_siten")
            .AppendLine("	,@keikakuyou_kannkatsu_siten_mei")
            .AppendLine("	,@sds_kaisi_nengetu")
            .AppendLine("	,@kameiten_zokusei_1")
            .AppendLine("	,@kameiten_zokusei_2")
            .AppendLine("	,@kameiten_zokusei_3")
            .AppendLine("	,@kameiten_zokusei_4")
            .AppendLine("	,@kameiten_zokusei_5")
            .AppendLine("	,@kameiten_zokusei_6")
            .AppendLine("	,@add_login_user_id")
            .AppendLine("	,GetDate()")
            .AppendLine(")")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@kbn_mei", SqlDbType.VarChar, 40, strKbnMei))
        paramList.Add(MakeParam("@keikakuyou_kameitenmei", SqlDbType.VarChar, 80, strKeikakuyouKameitenmei))
        paramList.Add(MakeParam("@gyoutai", SqlDbType.Int, 10, IIf(inGyoutai.Equals(String.Empty), DBNull.Value, inGyoutai)))
        paramList.Add(MakeParam("@touitsu_houjin_cd", SqlDbType.VarChar, 8, strTouitsuHoujinCd))
        paramList.Add(MakeParam("@houjin_cd", SqlDbType.VarChar, 8, strHoujinCd))
        paramList.Add(MakeParam("@keikaku_nayose_cd", SqlDbType.VarChar, 8, strKeikakuNayoseCd))
        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu", SqlDbType.Int, 10, IIf(inKeikakuyouNenkanTousuu.Equals(String.Empty), DBNull.Value, inKeikakuyouNenkanTousuu)))
        paramList.Add(MakeParam("@keikakuji_eigyou_tantousya_id", SqlDbType.VarChar, 30, strKeikakujiEigyouTantousyaId))
        paramList.Add(MakeParam("@keikakuji_eigyou_tantousya_mei", SqlDbType.VarChar, 100, strKeikakujiEigyouTantousyaMei))
        paramList.Add(MakeParam("@keikakuyou_eigyou_kbn", SqlDbType.Int, 2, IIf(inKeikakuyouEigyouKbn.Equals(String.Empty), DBNull.Value, inKeikakuyouEigyouKbn)))
        paramList.Add(MakeParam("@keikakuyou_kannkatsu_siten", SqlDbType.VarChar, 4, strKeikakuyouKannkatsuSiten))
        paramList.Add(MakeParam("@keikakuyou_kannkatsu_siten_mei", SqlDbType.VarChar, 40, strKeikakuyouKannkatsuSitenMei))
        paramList.Add(MakeParam("@sds_kaisi_nengetu", SqlDbType.VarChar, 7, strSdsKaisiNengetu))
        paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 2, IIf(inKameitenZokusei1.Equals(String.Empty), DBNull.Value, inKameitenZokusei1)))
        paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 2, IIf(inKameitenZokusei2.Equals(String.Empty), DBNull.Value, inKameitenZokusei2)))
        paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 2, IIf(inKameitenZokusei3.Equals(String.Empty), DBNull.Value, inKameitenZokusei3)))
        paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.VarChar, 10, strKameitenZokusei4))
        paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.VarChar, 10, strKameitenZokusei5))
        paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, strKameitenZokusei6))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strAddLoginUserId))

        insCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, paramList.ToArray())

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 「計画管理_加盟店情報ﾏｽﾀ」の更新処理
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKbnMei">区分名</param>
    ''' <param name="strKeikakuyouKameitenmei">計画用_加盟店名</param>
    ''' <param name="inGyoutai">業態</param>
    ''' <param name="strTouitsuHoujinCd">統一法人コード</param>
    ''' <param name="strHoujinCd">法人コード</param>
    ''' <param name="strKeikakuNayoseCd">計画名寄コード</param>
    ''' <param name="inKeikakuyouNenkanTousuu">計画用_年間棟数</param>
    ''' <param name="strKeikakujiEigyouTantousyaId">計画時_営業担当者</param>
    ''' <param name="strKeikakujiEigyouTantousyaMei">計画時_営業担当者名</param>
    ''' <param name="inKeikakuyouEigyouKbn">計画用_営業区分</param>
    ''' <param name="strKeikakuyouKannkatsuSiten">計画用_管轄支店</param>
    ''' <param name="strKeikakuyouKannkatsuSitenMei">計画用_管轄支店名</param>
    ''' <param name="strSdsKaisiNengetu">SDS開始年月</param>
    ''' <param name="inKameitenZokusei1">加盟店属性1</param>
    ''' <param name="inKameitenZokusei2">加盟店属性2</param>
    ''' <param name="inKameitenZokusei3">加盟店属性3</param>
    ''' <param name="strKameitenZokusei4">加盟店属性4</param>
    ''' <param name="strKameitenZokusei5">加盟店属性5</param>
    ''' <param name="strKameitenZokusei6">加盟店属性6</param>
    ''' <param name="strUpdLoginUserId">更新ログインユーザーID</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Public Function UpdMKeikakuKameitenInfo(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strKbnMei As String, _
                                            ByVal strKeikakuyouKameitenmei As String, ByVal inGyoutai As String, ByVal strTouitsuHoujinCd As String, _
                                            ByVal strHoujinCd As String, ByVal strKeikakuNayoseCd As String, ByVal inKeikakuyouNenkanTousuu As String, _
                                            ByVal strKeikakujiEigyouTantousyaId As String, ByVal strKeikakujiEigyouTantousyaMei As String, _
                                            ByVal inKeikakuyouEigyouKbn As String, ByVal strKeikakuyouKannkatsuSiten As String, _
                                            ByVal strKeikakuyouKannkatsuSitenMei As String, ByVal strSdsKaisiNengetu As String, _
                                            ByVal inKameitenZokusei1 As String, ByVal inKameitenZokusei2 As String, _
                                            ByVal inKameitenZokusei3 As String, ByVal strKameitenZokusei4 As String, _
                                            ByVal strKameitenZokusei5 As String, ByVal strKameitenZokusei6 As String, _
                                            ByVal strUpdLoginUserId As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strUpdLoginUserId)

        'SQLコメント
        Dim commandTextSb As New System.Text.StringBuilder
        '更新数
        Dim updCount As Integer
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("UPDATE m_keikaku_kameiten_info WITH(UPDLOCK)")
            .AppendLine("SET ")
            .AppendLine("	kbn = @kbn")
            .AppendLine("	,kbn_mei = @kbn_mei")
            .AppendLine("	,keikakuyou_kameitenmei = @keikakuyou_kameitenmei")
            .AppendLine("	,gyoutai = @gyoutai")
            .AppendLine("	,touitsu_houjin_cd = @touitsu_houjin_cd")
            .AppendLine("	,houjin_cd = @houjin_cd ")
            .AppendLine("	,keikaku_nayose_cd = @keikaku_nayose_cd	")
            .AppendLine("	,keikakuyou_nenkan_tousuu = @keikakuyou_nenkan_tousuu")
            .AppendLine("	,keikakuji_eigyou_tantousya_id = @keikakuji_eigyou_tantousya_id")
            .AppendLine("	,keikakuji_eigyou_tantousya_mei = @keikakuji_eigyou_tantousya_mei")
            .AppendLine("	,keikakuyou_eigyou_kbn = @keikakuyou_eigyou_kbn")
            .AppendLine("	,keikakuyou_kannkatsu_siten =@keikakuyou_kannkatsu_siten")
            .AppendLine("	,keikakuyou_kannkatsu_siten_mei = @keikakuyou_kannkatsu_siten_mei")
            .AppendLine("	,sds_kaisi_nengetu = @sds_kaisi_nengetu	")
            .AppendLine("	,kameiten_zokusei_1 = @kameiten_zokusei_1")
            .AppendLine("	,kameiten_zokusei_2 = @kameiten_zokusei_2")
            .AppendLine("	,kameiten_zokusei_3 = @kameiten_zokusei_3")
            .AppendLine("	,kameiten_zokusei_4 = @kameiten_zokusei_4")
            .AppendLine("	,kameiten_zokusei_5 = @kameiten_zokusei_5")
            .AppendLine("	,kameiten_zokusei_6 = @kameiten_zokusei_6")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id")
            .AppendLine("	,upd_datetime = GetDate()")
            .AppendLine("WHERE kameiten_cd = @kameiten_cd")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@kbn_mei", SqlDbType.VarChar, 40, strKbnMei))
        paramList.Add(MakeParam("@keikakuyou_kameitenmei", SqlDbType.VarChar, 80, strKeikakuyouKameitenmei))
        paramList.Add(MakeParam("@gyoutai", SqlDbType.Int, 10, IIf(inGyoutai.Equals(String.Empty), DBNull.Value, inGyoutai)))
        paramList.Add(MakeParam("@touitsu_houjin_cd", SqlDbType.VarChar, 8, strTouitsuHoujinCd))
        paramList.Add(MakeParam("@houjin_cd", SqlDbType.VarChar, 8, strHoujinCd))
        paramList.Add(MakeParam("@keikaku_nayose_cd", SqlDbType.VarChar, 8, strKeikakuNayoseCd))
        paramList.Add(MakeParam("@keikakuyou_nenkan_tousuu", SqlDbType.Int, 10, IIf(inKeikakuyouNenkanTousuu.Equals(String.Empty), DBNull.Value, inKeikakuyouNenkanTousuu)))
        paramList.Add(MakeParam("@keikakuji_eigyou_tantousya_id", SqlDbType.VarChar, 30, strKeikakujiEigyouTantousyaId))
        paramList.Add(MakeParam("@keikakuji_eigyou_tantousya_mei", SqlDbType.VarChar, 100, strKeikakujiEigyouTantousyaMei))
        paramList.Add(MakeParam("@keikakuyou_eigyou_kbn", SqlDbType.Int, 2, IIf(inKeikakuyouEigyouKbn.Equals(String.Empty), DBNull.Value, inKeikakuyouEigyouKbn)))
        paramList.Add(MakeParam("@keikakuyou_kannkatsu_siten", SqlDbType.VarChar, 4, strKeikakuyouKannkatsuSiten))
        paramList.Add(MakeParam("@keikakuyou_kannkatsu_siten_mei", SqlDbType.VarChar, 40, strKeikakuyouKannkatsuSitenMei))
        paramList.Add(MakeParam("@sds_kaisi_nengetu", SqlDbType.VarChar, 7, strSdsKaisiNengetu))
        paramList.Add(MakeParam("@kameiten_zokusei_1", SqlDbType.Int, 2, IIf(inKameitenZokusei1.Equals(String.Empty), DBNull.Value, inKameitenZokusei1)))
        paramList.Add(MakeParam("@kameiten_zokusei_2", SqlDbType.Int, 2, IIf(inKameitenZokusei2.Equals(String.Empty), DBNull.Value, inKameitenZokusei2)))
        paramList.Add(MakeParam("@kameiten_zokusei_3", SqlDbType.Int, 2, IIf(inKameitenZokusei3.Equals(String.Empty), DBNull.Value, inKameitenZokusei3)))
        paramList.Add(MakeParam("@kameiten_zokusei_4", SqlDbType.VarChar, 10, strKameitenZokusei4))
        paramList.Add(MakeParam("@kameiten_zokusei_5", SqlDbType.VarChar, 10, strKameitenZokusei5))
        paramList.Add(MakeParam("@kameiten_zokusei_6", SqlDbType.VarChar, 40, strKameitenZokusei6))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUpdLoginUserId))

        updCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, paramList.ToArray())

        If updCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 計画管理_加盟店情報マスタの排他処理
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Public Function SelKameitenMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT TOP 1 ")
            .AppendLine("	ISNULL(MAX(upd_datetime),MAX(add_datetime)) AS maxtime ")
            .AppendLine("	,ISNULL(upd_login_user_id,add_login_user_id) as theuser ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten_info WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("GROUP BY  ")
            .AppendLine("	upd_login_user_id ")
            .AppendLine("	,add_login_user_id ")
            .AppendLine("ORDER BY  ")
            .AppendLine("	maxtime DESC ")
        End With

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMaxUpdTime", paramList.ToArray)

        Return dsReturn.Tables("dtMaxUpdTime")

    End Function

    ''' <summary>
    ''' 営業担当者名を取得する
    ''' </summary>
    ''' <param name="strTantousyaId">営業担当者ID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/27　李宇(大連情報システム部)　新規作成</history>
    Public Function SelEigyouTantousyaMei(ByVal strTantousyaId As String) As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	DisplayName")
            .AppendLine("FROM ")
            .AppendLine("	m_jhs_mailbox WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	PrimaryWindowsNTAccount = @PrimaryWindowsNTAccount ")
        End With

        paramList.Add(MakeParam("@PrimaryWindowsNTAccount", SqlDbType.VarChar, 64, strTantousyaId))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMei", paramList.ToArray)

        Return dsReturn.Tables("dtMei")

    End Function

End Class
