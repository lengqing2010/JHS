Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

Public Class SitenTukibetuKeikakuchiSearchListDA

    ''' <summary>
    '''支店別月別計画管理テーブルにより、明細データを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="strBusyoCd">部署コード</param>
    ''' <returns>支店別月別計画管理テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSitenbetuTukiKeikakuKanri(ByVal strKeikakuNendo As String, ByVal strBusyoCd As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strBusyoCd)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb

            .AppendLine("SELECT  ")
            .AppendLine("	STKK_MAIN.eigyou_kbn ")
            .AppendLine("	,[4gatu_keikaku_kensuu] ")
            .AppendLine("	,[4gatu_keikaku_kingaku] ")
            .AppendLine("	,[4gatu_keikaku_arari] ")
            .AppendLine("	,[5gatu_keikaku_kensuu] ")
            .AppendLine("	,[5gatu_keikaku_kingaku] ")
            .AppendLine("	,[5gatu_keikaku_arari] ")
            .AppendLine("	,[6gatu_keikaku_kensuu] ")
            .AppendLine("	,[6gatu_keikaku_kingaku] ")
            .AppendLine("	,[6gatu_keikaku_arari] ")
            .AppendLine("	,[7gatu_keikaku_kensuu] ")
            .AppendLine("	,[7gatu_keikaku_kingaku] ")
            .AppendLine("	,[7gatu_keikaku_arari] ")
            .AppendLine("	,[8gatu_keikaku_kensuu] ")
            .AppendLine("	,[8gatu_keikaku_kingaku] ")
            .AppendLine("	,[8gatu_keikaku_arari] ")
            .AppendLine("	,[9gatu_keikaku_kensuu] ")
            .AppendLine("	,[9gatu_keikaku_kingaku] ")
            .AppendLine("	,[9gatu_keikaku_arari] ")
            .AppendLine("	,[10gatu_keikaku_kensuu] ")
            .AppendLine("	,[10gatu_keikaku_kingaku] ")
            .AppendLine("	,[10gatu_keikaku_arari] ")
            .AppendLine("	,[11gatu_keikaku_kensuu] ")
            .AppendLine("	,[11gatu_keikaku_kingaku] ")
            .AppendLine("	,[11gatu_keikaku_arari] ")
            .AppendLine("	,[12gatu_keikaku_kensuu] ")
            .AppendLine("	,[12gatu_keikaku_kingaku] ")
            .AppendLine("	,[12gatu_keikaku_arari] ")
            .AppendLine("	,[1gatu_keikaku_kensuu] ")
            .AppendLine("	,[1gatu_keikaku_kingaku] ")
            .AppendLine("	,[1gatu_keikaku_arari] ")
            .AppendLine("	,[2gatu_keikaku_kensuu] ")
            .AppendLine("	,[2gatu_keikaku_kingaku] ")
            .AppendLine("	,[2gatu_keikaku_arari] ")
            .AppendLine("	,[3gatu_keikaku_kensuu] ")
            .AppendLine("	,[3gatu_keikaku_kingaku] ")
            .AppendLine("	,[3gatu_keikaku_arari] ")
            .AppendLine("	--合計(四半期) ")
            .AppendLine("	,isnull([4gatu_keikaku_kensuu],0)+isnull([5gatu_keikaku_kensuu],0)+isnull([6gatu_keikaku_kensuu],0) as [456gatu_keikaku_kensuu] ")
            .AppendLine("	,isnull([4gatu_keikaku_kingaku],0)+isnull([5gatu_keikaku_kingaku],0)+isnull([6gatu_keikaku_kingaku],0) as [456gatu_keikaku_kingaku] ")
            .AppendLine("	,isnull([4gatu_keikaku_arari],0)+isnull([5gatu_keikaku_arari],0)+isnull([6gatu_keikaku_arari],0) as [456gatu_keikaku_arari] ")
            .AppendLine("	,isnull([7gatu_keikaku_kensuu],0)+isnull([8gatu_keikaku_kensuu],0)+isnull([9gatu_keikaku_kensuu],0) as [789gatu_keikaku_kensuu] ")
            .AppendLine("	,isnull([7gatu_keikaku_kingaku],0)+isnull([8gatu_keikaku_kingaku],0)+isnull([9gatu_keikaku_kingaku],0) as [789gatu_keikaku_kingaku] ")
            .AppendLine("	,isnull([7gatu_keikaku_arari],0)+isnull([8gatu_keikaku_arari],0)+isnull([9gatu_keikaku_arari],0) as [789gatu_keikaku_arari] ")
            .AppendLine("	,isnull([10gatu_keikaku_kensuu],0)+isnull([11gatu_keikaku_kensuu],0)+isnull([12gatu_keikaku_kensuu],0) as [101112gatu_keikaku_kensuu] ")
            .AppendLine("	,isnull([10gatu_keikaku_kingaku],0)+isnull([11gatu_keikaku_kingaku],0)+isnull([12gatu_keikaku_kingaku],0) as [101112gatu_keikaku_kingaku] ")
            .AppendLine("	,isnull([10gatu_keikaku_arari],0)+isnull([11gatu_keikaku_arari],0)+isnull([12gatu_keikaku_arari],0) as [101112gatu_keikaku_arari] ")
            .AppendLine("	,isnull([1gatu_keikaku_kensuu],0)+isnull([2gatu_keikaku_kensuu],0)+isnull([3gatu_keikaku_kensuu],0) as [123gatu_keikaku_kensuu] ")
            .AppendLine("	,isnull([1gatu_keikaku_kingaku],0)+isnull([2gatu_keikaku_kingaku],0)+isnull([3gatu_keikaku_kingaku],0) as [123gatu_keikaku_kingaku] ")
            .AppendLine("	,isnull([1gatu_keikaku_arari],0)+isnull([2gatu_keikaku_arari],0)+isnull([3gatu_keikaku_arari],0) as [123gatu_keikaku_arari] ")
            .AppendLine(" 	--合計(上期・下期) ")
            .AppendLine("	,isnull([4gatu_keikaku_kensuu],0)+isnull([5gatu_keikaku_kensuu],0)+isnull([6gatu_keikaku_kensuu],0) ")
            .AppendLine("	+isnull([7gatu_keikaku_kensuu],0)+isnull([8gatu_keikaku_kensuu],0)+isnull([9gatu_keikaku_kensuu],0) as [kamikigatu_keikaku_kensuu] ")
            .AppendLine("	,isnull([4gatu_keikaku_kingaku],0)+isnull([5gatu_keikaku_kingaku],0)+isnull([6gatu_keikaku_kingaku],0) ")
            .AppendLine("	+isnull([7gatu_keikaku_kingaku],0)+isnull([8gatu_keikaku_kingaku],0)+isnull([9gatu_keikaku_kingaku],0) as [kamikigatu_keikaku_kingaku] ")
            .AppendLine("	,isnull([4gatu_keikaku_arari],0)+isnull([5gatu_keikaku_arari],0)+isnull([6gatu_keikaku_arari],0) ")
            .AppendLine("	+isnull([7gatu_keikaku_arari],0)+isnull([8gatu_keikaku_arari],0)+isnull([9gatu_keikaku_arari],0) as [kamikigatu_keikaku_arari] ")
            .AppendLine("	,isnull([10gatu_keikaku_kensuu],0)+isnull([11gatu_keikaku_kensuu],0)+isnull([12gatu_keikaku_kensuu],0) ")
            .AppendLine("	+isnull([1gatu_keikaku_kensuu],0)+isnull([2gatu_keikaku_kensuu],0)+isnull([3gatu_keikaku_kensuu],0) as [simokigatu_keikaku_kensuu] ")
            .AppendLine("	,isnull([10gatu_keikaku_kingaku],0)+isnull([11gatu_keikaku_kingaku],0)+isnull([12gatu_keikaku_kingaku],0) ")
            .AppendLine("	+isnull([1gatu_keikaku_kingaku],0)+isnull([2gatu_keikaku_kingaku],0)+isnull([3gatu_keikaku_kingaku],0) as [simokigatu_keikaku_kingaku] ")
            .AppendLine("	,isnull([10gatu_keikaku_arari],0)+isnull([11gatu_keikaku_arari],0)+isnull([12gatu_keikaku_arari],0) ")
            .AppendLine("	+isnull([1gatu_keikaku_arari],0)+isnull([2gatu_keikaku_arari],0)+isnull([3gatu_keikaku_arari],0) as [simokigatu_keikaku_arari] ")
            .AppendLine(" 	--合計(年度集計)   	 ")
            .AppendLine("	,isnull([4gatu_keikaku_kensuu],0)+isnull([5gatu_keikaku_kensuu],0)+isnull([6gatu_keikaku_kensuu],0) ")
            .AppendLine("	+isnull([7gatu_keikaku_kensuu],0)+isnull([8gatu_keikaku_kensuu],0)+isnull([9gatu_keikaku_kensuu],0) ")
            .AppendLine("	+isnull([10gatu_keikaku_kensuu],0)+isnull([11gatu_keikaku_kensuu],0)+isnull([12gatu_keikaku_kensuu],0) ")
            .AppendLine("	+isnull([1gatu_keikaku_kensuu],0)+isnull([2gatu_keikaku_kensuu],0)+isnull([3gatu_keikaku_kensuu],0) as [nendogatu_keikaku_kensuu] ")
            .AppendLine("	,isnull([4gatu_keikaku_kingaku],0)+isnull([5gatu_keikaku_kingaku],0)+isnull([6gatu_keikaku_kingaku],0) ")
            .AppendLine("	+isnull([7gatu_keikaku_kingaku],0)+isnull([8gatu_keikaku_kingaku],0)+isnull([9gatu_keikaku_kingaku],0) ")
            .AppendLine("	+isnull([10gatu_keikaku_kingaku],0)+isnull([11gatu_keikaku_kingaku],0)+isnull([12gatu_keikaku_kingaku],0) ")
            .AppendLine("	+isnull([1gatu_keikaku_kingaku],0)+isnull([2gatu_keikaku_kingaku],0)+isnull([3gatu_keikaku_kingaku],0) as [nendogatu_keikaku_kingaku] ")
            .AppendLine("	,isnull([4gatu_keikaku_arari],0)+isnull([5gatu_keikaku_arari],0)+isnull([6gatu_keikaku_arari],0) ")
            .AppendLine("	+isnull([7gatu_keikaku_arari],0)+isnull([8gatu_keikaku_arari],0)+isnull([9gatu_keikaku_arari],0) ")
            .AppendLine("	+isnull([10gatu_keikaku_arari],0)+isnull([11gatu_keikaku_arari],0)+isnull([12gatu_keikaku_arari],0) ")
            .AppendLine("	+isnull([1gatu_keikaku_arari],0)+isnull([2gatu_keikaku_arari],0)+isnull([3gatu_keikaku_arari],0) as [nendogatu_keikaku_arari] ")
            .AppendLine("	,STKK_MAIN.kakutei_flg ")
            .AppendLine("	,STKK_MAIN.keikaku_huhen_flg ")
            .AppendLine("	,STKK_MAIN.add_datetime ")
            .AppendLine("FROM t_sitenbetu_tuki_keikaku_kanri STKK_MAIN WITH(READCOMMITTED) ")
            .AppendLine("INNER JOIN ( ")
            .AppendLine("			SELECT  ")
            .AppendLine("				keikaku_nendo ")
            .AppendLine("				,busyo_cd ")
            .AppendLine("				,eigyou_kbn ")
            .AppendLine("				,MAX(add_datetime) AS add_datetime ")
            .AppendLine("			FROM t_sitenbetu_tuki_keikaku_kanri  ")
            .AppendLine("			WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine("			AND busyo_cd = @busyo_cd ")
            .AppendLine("			AND eigyou_kbn IN (@eigyou_kbn1,@eigyou_kbn3,@eigyou_kbn4) ")
            .AppendLine("			GROUP BY  ")
            .AppendLine("				keikaku_nendo ")
            .AppendLine("				,busyo_cd ")
            .AppendLine("				,eigyou_kbn ")
            .AppendLine("			) STKK_KEY ")
            .AppendLine("ON  STKK_KEY.keikaku_nendo = STKK_MAIN.keikaku_nendo ")
            .AppendLine("AND STKK_KEY.busyo_cd = STKK_MAIN.busyo_cd ")
            .AppendLine("AND STKK_KEY.add_datetime = STKK_MAIN.add_datetime ")
            .AppendLine("AND STKK_KEY.eigyou_kbn = STKK_MAIN.eigyou_kbn ")
            .AppendLine("ORDER BY STKK_MAIN.eigyou_kbn ")

        End With

        paramList.Add(SQLHelper.MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))
        paramList.Add(SQLHelper.MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd))
        paramList.Add(SQLHelper.MakeParam("@eigyou_kbn1", SqlDbType.VarChar, 1, "1"))
        paramList.Add(SQLHelper.MakeParam("@eigyou_kbn3", SqlDbType.VarChar, 1, "3"))
        paramList.Add(SQLHelper.MakeParam("@eigyou_kbn4", SqlDbType.VarChar, 1, "4"))

        '実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "SitenbetuTukiKeikakuKanri", paramList.ToArray)

        '戻る値
        Return dsReturn.Tables("SitenbetuTukiKeikakuKanri")

    End Function

    ''' <summary>
    '''支実績管理テーブルにより、前年明細データを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="strBusyoCd">部署コード</param>
    ''' <returns>実績管理テーブルテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelJissekiKanri(ByVal strKeikakuNendo As String, ByVal strBusyoCd As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strBusyoCd)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb

            .AppendLine("SELECT  ")
            .AppendLine("    eigyou_kbn ")
            .AppendLine("	,[4gatu_jisseki_kensuu] ")
            .AppendLine("	,[4gatu_jisseki_kingaku] ")
            .AppendLine("	,[4gatu_jisseki_arari] ")
            .AppendLine("	,[5gatu_jisseki_kensuu] ")
            .AppendLine("	,[5gatu_jisseki_kingaku] ")
            .AppendLine("	,[5gatu_jisseki_arari] ")
            .AppendLine("	,[6gatu_jisseki_kensuu] ")
            .AppendLine("	,[6gatu_jisseki_kingaku] ")
            .AppendLine("	,[6gatu_jisseki_arari] ")
            .AppendLine("	,[7gatu_jisseki_kensuu] ")
            .AppendLine("	,[7gatu_jisseki_kingaku] ")
            .AppendLine("	,[7gatu_jisseki_arari] ")
            .AppendLine("	,[8gatu_jisseki_kensuu] ")
            .AppendLine("	,[8gatu_jisseki_kingaku] ")
            .AppendLine("	,[8gatu_jisseki_arari] ")
            .AppendLine("	,[9gatu_jisseki_kensuu] ")
            .AppendLine("	,[9gatu_jisseki_kingaku] ")
            .AppendLine("	,[9gatu_jisseki_arari] ")
            .AppendLine("	,[10gatu_jisseki_kensuu] ")
            .AppendLine("	,[10gatu_jisseki_kingaku] ")
            .AppendLine("	,[10gatu_jisseki_arari] ")
            .AppendLine("	,[11gatu_jisseki_kensuu] ")
            .AppendLine("	,[11gatu_jisseki_kingaku] ")
            .AppendLine("	,[11gatu_jisseki_arari] ")
            .AppendLine("	,[12gatu_jisseki_kensuu] ")
            .AppendLine("	,[12gatu_jisseki_kingaku] ")
            .AppendLine("	,[12gatu_jisseki_arari] ")
            .AppendLine("	,[1gatu_jisseki_kensuu] ")
            .AppendLine("	,[1gatu_jisseki_kingaku] ")
            .AppendLine("	,[1gatu_jisseki_arari] ")
            .AppendLine("	,[2gatu_jisseki_kensuu] ")
            .AppendLine("	,[2gatu_jisseki_kingaku] ")
            .AppendLine("	,[2gatu_jisseki_arari] ")
            .AppendLine("	,[3gatu_jisseki_kensuu] ")
            .AppendLine("	,[3gatu_jisseki_kingaku] ")
            .AppendLine("	,[3gatu_jisseki_arari] ")
            .AppendLine("	--合計(四半期) ")
            .AppendLine("	,[4gatu_jisseki_kensuu]+[5gatu_jisseki_kensuu]+[6gatu_jisseki_kensuu] as [456gatu_jisseki_kensuu] ")
            .AppendLine("	,[4gatu_jisseki_kingaku]+[5gatu_jisseki_kingaku]+[6gatu_jisseki_kingaku] as [456gatu_jisseki_kingaku] ")
            .AppendLine("	,[4gatu_jisseki_arari]+[5gatu_jisseki_arari]+[6gatu_jisseki_arari] as [456gatu_jisseki_arari] ")
            .AppendLine("	,[7gatu_jisseki_kensuu]+[8gatu_jisseki_kensuu]+[9gatu_jisseki_kensuu] as [789gatu_jisseki_kensuu] ")
            .AppendLine("	,[7gatu_jisseki_kingaku]+[8gatu_jisseki_kingaku]+[9gatu_jisseki_kingaku] as [789gatu_jisseki_kingaku] ")
            .AppendLine("	,[7gatu_jisseki_arari]+[8gatu_jisseki_arari]+[9gatu_jisseki_arari] as [789gatu_jisseki_arari] ")
            .AppendLine("	,[10gatu_jisseki_kensuu]+[11gatu_jisseki_kensuu]+[12gatu_jisseki_kensuu] as [101112gatu_jisseki_kensuu] ")
            .AppendLine("	,[10gatu_jisseki_kingaku]+[11gatu_jisseki_kingaku]+[12gatu_jisseki_kingaku] as [101112gatu_jisseki_kingaku] ")
            .AppendLine("	,[10gatu_jisseki_arari]+[11gatu_jisseki_arari]+[12gatu_jisseki_arari] as [101112gatu_jisseki_arari] ")
            .AppendLine("	,[1gatu_jisseki_kensuu]+[2gatu_jisseki_kensuu]+[3gatu_jisseki_kensuu] as [123gatu_jisseki_kensuu] ")
            .AppendLine("	,[1gatu_jisseki_kingaku]+[2gatu_jisseki_kingaku]+[3gatu_jisseki_kingaku] as [123gatu_jisseki_kingaku] ")
            .AppendLine("	,[1gatu_jisseki_arari]+[2gatu_jisseki_arari]+[3gatu_jisseki_arari] as [123gatu_jisseki_arari] ")
            .AppendLine(" 	--合計(上期・下期) ")
            .AppendLine("	,[4gatu_jisseki_kensuu]+[5gatu_jisseki_kensuu]+[6gatu_jisseki_kensuu] ")
            .AppendLine("	+[7gatu_jisseki_kensuu]+[8gatu_jisseki_kensuu]+[9gatu_jisseki_kensuu] as [kamikigatu_jisseki_kensuu] ")
            .AppendLine("	,[4gatu_jisseki_kingaku]+[5gatu_jisseki_kingaku]+[6gatu_jisseki_kingaku] ")
            .AppendLine("	+[7gatu_jisseki_kingaku]+[8gatu_jisseki_kingaku]+[9gatu_jisseki_kingaku] as [kamikigatu_jisseki_kingaku] ")
            .AppendLine("	,[4gatu_jisseki_arari]+[5gatu_jisseki_arari]+[6gatu_jisseki_arari] ")
            .AppendLine("	+[7gatu_jisseki_arari]+[8gatu_jisseki_arari]+[9gatu_jisseki_arari] as [kamikigatu_jisseki_arari] ")
            .AppendLine("	,[10gatu_jisseki_kensuu]+[11gatu_jisseki_kensuu]+[12gatu_jisseki_kensuu] ")
            .AppendLine("	+[1gatu_jisseki_kensuu]+[2gatu_jisseki_kensuu]+[3gatu_jisseki_kensuu] as [simokigatu_jisseki_kensuu] ")
            .AppendLine("	,[10gatu_jisseki_kingaku]+[11gatu_jisseki_kingaku]+[12gatu_jisseki_kingaku] ")
            .AppendLine("	+[1gatu_jisseki_kingaku]+[2gatu_jisseki_kingaku]+[3gatu_jisseki_kingaku] as [simokigatu_jisseki_kingaku] ")
            .AppendLine("	,[10gatu_jisseki_arari]+[11gatu_jisseki_arari]+[12gatu_jisseki_arari] ")
            .AppendLine("	+[1gatu_jisseki_arari]+[2gatu_jisseki_arari]+[3gatu_jisseki_arari] as [simokigatu_jisseki_arari] ")
            .AppendLine(" 	--合計(年度集計)   	 ")
            .AppendLine("	,[4gatu_jisseki_kensuu]+[5gatu_jisseki_kensuu]+[6gatu_jisseki_kensuu] ")
            .AppendLine("	+[7gatu_jisseki_kensuu]+[8gatu_jisseki_kensuu]+[9gatu_jisseki_kensuu] ")
            .AppendLine("	+[10gatu_jisseki_kensuu]+[11gatu_jisseki_kensuu]+[12gatu_jisseki_kensuu] ")
            .AppendLine("	+[1gatu_jisseki_kensuu]+[2gatu_jisseki_kensuu]+[3gatu_jisseki_kensuu] as [nendogatu_jisseki_kensuu] ")
            .AppendLine("	,[4gatu_jisseki_kingaku]+[5gatu_jisseki_kingaku]+[6gatu_jisseki_kingaku] ")
            .AppendLine("	+[7gatu_jisseki_kingaku]+[8gatu_jisseki_kingaku]+[9gatu_jisseki_kingaku] ")
            .AppendLine("	+[10gatu_jisseki_kingaku]+[11gatu_jisseki_kingaku]+[12gatu_jisseki_kingaku] ")
            .AppendLine("	+[1gatu_jisseki_kingaku]+[2gatu_jisseki_kingaku]+[3gatu_jisseki_kingaku] as [nendogatu_jisseki_kingaku] ")
            .AppendLine("	,[4gatu_jisseki_arari]+[5gatu_jisseki_arari]+[6gatu_jisseki_arari] ")
            .AppendLine("	+[7gatu_jisseki_arari]+[8gatu_jisseki_arari]+[9gatu_jisseki_arari] ")
            .AppendLine("	+[10gatu_jisseki_arari]+[11gatu_jisseki_arari]+[12gatu_jisseki_arari] ")
            .AppendLine("	+[1gatu_jisseki_arari]+[2gatu_jisseki_arari]+[3gatu_jisseki_arari] as [nendogatu_jisseki_arari]FROM (    ")
            .AppendLine("    SELECT  ")
            .AppendLine("    	eigyou_kbn ")
            .AppendLine("    	,SUM(ISNULL([4gatu_jisseki_kensuu],0)) AS [4gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([4gatu_jisseki_kingaku],0)) AS [4gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([4gatu_jisseki_arari],0)) AS [4gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([5gatu_jisseki_kensuu],0)) AS [5gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([5gatu_jisseki_kingaku],0)) AS [5gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([5gatu_jisseki_arari],0)) AS [5gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([6gatu_jisseki_kensuu],0)) AS [6gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([6gatu_jisseki_kingaku],0)) AS [6gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([6gatu_jisseki_arari],0)) AS [6gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([7gatu_jisseki_kensuu],0)) AS [7gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([7gatu_jisseki_kingaku],0)) AS [7gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([7gatu_jisseki_arari],0)) AS [7gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([8gatu_jisseki_kensuu],0)) AS [8gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([8gatu_jisseki_kingaku],0)) AS [8gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([8gatu_jisseki_arari],0)) AS [8gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([9gatu_jisseki_kensuu],0)) AS [9gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([9gatu_jisseki_kingaku],0)) AS [9gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([9gatu_jisseki_arari],0)) AS [9gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([10gatu_jisseki_kensuu],0)) AS [10gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([10gatu_jisseki_kingaku],0)) AS [10gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([10gatu_jisseki_arari],0)) AS [10gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([11gatu_jisseki_kensuu],0)) AS [11gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([11gatu_jisseki_kingaku],0)) AS [11gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([11gatu_jisseki_arari],0)) AS [11gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([12gatu_jisseki_kensuu],0)) AS [12gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([12gatu_jisseki_kingaku],0)) AS [12gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([12gatu_jisseki_arari],0)) AS [12gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([1gatu_jisseki_kensuu],0)) AS [1gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([1gatu_jisseki_kingaku],0)) AS [1gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([1gatu_jisseki_arari],0)) AS [1gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([2gatu_jisseki_kensuu],0)) AS [2gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([2gatu_jisseki_kingaku],0)) AS [2gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([2gatu_jisseki_arari],0)) AS [2gatu_jisseki_arari] ")
            .AppendLine("    	,SUM(ISNULL([3gatu_jisseki_kensuu],0)) AS [3gatu_jisseki_kensuu] ")
            .AppendLine("    	,SUM(ISNULL([3gatu_jisseki_kingaku],0)) AS [3gatu_jisseki_kingaku] ")
            .AppendLine("    	,SUM(ISNULL([3gatu_jisseki_arari],0)) AS [3gatu_jisseki_arari] ")
            .AppendLine("    FROM t_jisseki_kanri TJK WITH(READCOMMITTED) ")
            .AppendLine("    INNER JOIN m_keikaku_kameiten MKK WITH(READCOMMITTED) ")
            .AppendLine("    ON MKK.keikaku_nendo = TJK.keikaku_nendo  ")
            .AppendLine("    AND MKK.kameiten_cd = TJK.kameiten_cd  ")
            .AppendLine("    WHERE TJK.keikaku_nendo = @keikaku_nendo ")
            .AppendLine("    AND MKK.busyo_cd = @busyo_cd ")
            .AppendLine("	 AND eigyou_kbn IN (@eigyou_kbn1,@eigyou_kbn3,@eigyou_kbn4) ")
            .AppendLine("    GROUP BY eigyou_kbn ")
            .AppendLine("	 ) MAIN ")

        End With

        paramList.Add(SQLHelper.MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))
        paramList.Add(SQLHelper.MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd))
        paramList.Add(SQLHelper.MakeParam("@eigyou_kbn1", SqlDbType.VarChar, 1, "1"))
        paramList.Add(SQLHelper.MakeParam("@eigyou_kbn3", SqlDbType.VarChar, 1, "3"))
        paramList.Add(SQLHelper.MakeParam("@eigyou_kbn4", SqlDbType.VarChar, 1, "4"))

        '実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "JissekiKanri", paramList.ToArray)

        '戻る値
        Return dsReturn.Tables("JissekiKanri")

    End Function

    ''' <summary>
    '''支店別計画管理テーブルにより、CSVタイトル合計データを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="strBusyoCd">部署コード</param>
    ''' <returns>支店別計画管理テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelSitenbetuKeikakuKanri(ByVal strKeikakuNendo As String, ByVal strBusyoCd As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strBusyoCd)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb

            .AppendLine("SELECT  ")
            .AppendLine("	eigyou_keikaku_kensuu ")
            .AppendLine("	,tokuhan_keikaku_kensuu ")
            .AppendLine("	,FC_keikaku_kensuu ")
            .AppendLine("	,eigyou_keikaku_uri_kingaku ")
            .AppendLine("	,tokuhan_keikaku_uri_kingaku ")
            .AppendLine("	,FC_keikaku_uri_kingaku ")
            .AppendLine("	,eigyou_keikaku_arari ")
            .AppendLine("	,tokuhan_keikaku_arari ")
            .AppendLine("	,FC_keikaku_arari ")
            .AppendLine("FROM  ")
            .AppendLine("	t_sitenbetu_keikaku_kanri TSKK WITH(READCOMMITTED) ")
            .AppendLine("INNER JOIN ( ")
            .AppendLine("		SELECT  ")
            .AppendLine("			keikaku_nendo ")
            .AppendLine("			,busyo_cd ")
            .AppendLine("			,MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("			 + CONVERT(VARCHAR,add_datetime,121)) as add_datetime ")
            .AppendLine("		FROM t_sitenbetu_keikaku_kanri ")
            .AppendLine("		WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine("		AND busyo_cd = @busyo_cd ")
            .AppendLine("		GROUP BY  ")
            .AppendLine("			keikaku_nendo ")
            .AppendLine("			,busyo_cd ")
            .AppendLine("		) TSKK_KEY ")
            .AppendLine("ON  TSKK_KEY.keikaku_nendo = TSKK.keikaku_nendo ")
            .AppendLine("AND TSKK_KEY.busyo_cd = TSKK.busyo_cd ")
            .AppendLine("AND TSKK_KEY.add_datetime = CASE WHEN ISNULL(CONVERT(VARCHAR,TSKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                            + CONVERT(VARCHAR,TSKK.add_datetime,121) ")

        End With

        paramList.Add(SQLHelper.MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))
        paramList.Add(SQLHelper.MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd))

        '実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "SitenbetuKeikakuKanri", paramList.ToArray)

        '戻る値
        Return dsReturn.Tables("SitenbetuKeikakuKanri")

    End Function

    ''' <summary>
    ''' 確定FLGを更新する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画_年度</param>
    ''' <param name="strBusyoCd">部署ｺｰﾄﾞ</param>
    ''' <param name="strAddDatetime">登録日時</param>
    ''' <param name="strAddEigyouKbn">営業区分</param>
    ''' <param name="strUserId">更新ログインユーザーID</param>
    ''' <param name="strKakuteiFlg">確定FLG</param>
    ''' <returns>true/false</returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/11　楊双(大連情報システム部)　新規作成</history>
    Public Function UpdKakuteiFlg(ByVal strKeikakuNendo As String, ByVal strBusyoCd As String, _
                                  ByVal strAddDatetime As String, ByVal strAddEigyouKbn As String, ByVal strUserId As String, _
                                  ByVal strKakuteiFlg As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strKeikakuNendo, strBusyoCd, strAddDatetime, strAddEigyouKbn, strUserId, strKakuteiFlg)

        '登録件数
        Dim updCount As Integer = 0

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("	t_sitenbetu_tuki_keikaku_kanri WITH(UPDLOCK) ") '--支店別月別計画管理テーブル ")
            .AppendLine("SET ")
            .AppendLine("	 kakutei_flg = @kakutei_flg ") '--確定FLG ")
            If strKakuteiFlg = "0" Then
                .AppendLine("	 ,keikaku_henkou_flg = @keikaku_henkou_flg ") '--計画変更FLG ")
            End If
            .AppendLine("    ,upd_datetime = GETDATE() ")
            .AppendLine("    ,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("WHERE ")
            .AppendLine("	 keikaku_nendo = @keikaku_nendo ") '--計画_年度 ")
            .AppendLine("AND busyo_cd = @busyo_cd ") '--部署ｺｰﾄﾞ ")
            .AppendLine("AND add_datetime = @add_datetime ") '--登録日時 ")
            .AppendLine("AND eigyou_kbn = @eigyou_kbn ") '--営業区分 ")
        End With

        'バラメタ
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Char, 1, strKakuteiFlg)) '--確定FLG(更新値)
        If strKakuteiFlg = "0" Then
            paramList.Add(MakeParam("@keikaku_henkou_flg", SqlDbType.Char, 1, "1")) '--計画変更FLG(更新値)
        End If
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId)) '--更新ログインユーザーID
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo)) '--計画_年度
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd)) '--部署ｺｰﾄﾞ
        paramList.Add(MakeParam("@add_datetime", SqlDbType.VarChar, 30, strAddDatetime)) '--登録日時
        paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 1, strAddEigyouKbn)) '--営業区分

        '実行	
        updCount = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If updCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
