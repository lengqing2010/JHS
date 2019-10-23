Imports System.text
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 系列_TH友の会
''' </summary>
''' <remarks>TH友の会に関係する処理はこのクラスに実装します<BR/>
''' ３系列に共通する処理は継承元の親クラス[KeiretuDataAccess]に実装します</remarks>
Public Class KeiretuThDataAccess
    Inherits KeiretuDataAccess
    Implements IKeiretuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 請求金額を取得します
    ''' </summary>
    ''' <param name="intMode">取得モード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intKingaku">TH請求用価格マスタのKEY,掛率計算時の金額</param>
    ''' <param name="intReturnKingaku">取得金額（戻り値）</param>
    ''' <returns>True:取得OK,False:取得NG</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuKingaku(ByVal intMode As Integer, _
                                     ByVal strSyouhinCd As String, _
                                     ByVal intKingaku As Integer, _
                                     ByRef intReturnKingaku As Integer) As Integer Implements IKeiretuDataAccess.getSeikyuKingaku

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuKingaku", _
                                            intMode, _
                                            strSyouhinCd, _
                                            intKingaku, _
                                            intReturnKingaku)

        ' パラメータ
        Const strParamSyouhinCd As String = "@SYOUHINCD"
        Const strParamKingaku As String = "@KINGAKU"

        ' 基本となるSQLを設定します
        If SetBaseSQLData(intMode, "th_muke_kkk") = False Then
            Return -1
        End If

        Dim strSqlcondition As String = ""

        If intMode = 1 Or intMode = 6 Then
            commandTextSb.Append(" FROM m_th_seikyuuyou_kakaku WITH (READCOMMITTED) ")
            strSqlcondition = " AND th_muke_kkk = " & strParamKingaku
        Else
            commandTextSb.Append(" FROM v_th_seikyuu ")
        End If

        commandTextSb.Append(" WHERE syouhin_cd = " & strParamSyouhinCd)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter

        ' 追加条件
        If strSqlcondition <> "" Then
            commandTextSb.Append(strSqlcondition)
            commandParameters = New SqlParameter() {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.Char, 8, strSyouhinCd) _
                                                  , SQLHelper.MakeParam(strParamKingaku, SqlDbType.Int, 0, intKingaku)}
        Else
            commandParameters = New SqlParameter() {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.Char, 8, strSyouhinCd)}
        End If

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            seikyuuKakakuDataSet, dataTableName, commandParameters)

        Dim kameitenMukeTable As SeikyuKingakuDataSet.dt_KameitenMukeDataTable = _
                    seikyuuKakakuDataSet.dt_KameitenMuke

        If kameitenMukeTable.Count = 0 And intMode = 6 Then
            ' 商品ｺｰﾄﾞのみでデータを再抽出（加盟店向価格は0とする）
            Dim commandTextTH As New StringBuilder()
            commandTextTH.Append(" SELECT 0 AS kameiten_muke_kkk, ISNULL(kakeritu,0) AS kakeritu ")
            commandTextTH.Append(" FROM v_th_seikyuu WITH (READCOMMITTED) ")
            commandTextTH.Append(" WHERE syouhin_cd = " & strParamSyouhinCd)

            ' 検索実行
            SQLHelper.FillDataset(connStr, CommandType.Text, commandTextTH.ToString(), _
                seikyuuKakakuDataSet, dataTableName, commandParameters)
        End If

        If GetKingaku(intMode, intKingaku, intReturnKingaku) = False Then
            ' "マスターにありません。経理に追加の連絡をして下さい。"
            Return 0
        End If

        Return 1


    End Function


End Class
