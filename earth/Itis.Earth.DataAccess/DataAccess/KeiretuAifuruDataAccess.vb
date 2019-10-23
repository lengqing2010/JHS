Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 系列_アイフルホーム
''' </summary>
''' <remarks>アイフルホームに関係する処理はこのクラスに実装します<BR/>
''' ３系列に共通する処理は継承元の親クラス[KeiretuDataAccess]に実装します</remarks>
Public Class KeiretuAifuruDataAccess
    Inherits KeiretuDataAccess
    Implements IKeiretuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 請求金額を取得します
    ''' </summary>
    ''' <param name="intMode">取得モード<BR/>
    ''' 1 (商品コード1の加盟店向価格)   <BR/>
    ''' 2 (解約払戻の加盟店向価格)      <BR/>
    ''' 3 (本部(TH)向価格)              <BR/>
    ''' 4 (実請求金額 / 掛率)           <BR/>
    ''' 5 (工務店請求金額 * 掛率)       <BR/>
    ''' 6 (加盟店向価格→実請求金額/掛率)</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intKingaku">TH請求用価格マスタのKEY,掛率計算時の金額<BR/>
    ''' 取得モード=1 (TH請求用価格マスタのKEY)<BR/>
    ''' 取得モード=4 (実請求金額)<BR/>
    ''' 取得モード=5 (工務店請求金額)</param>
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

        ' 基本となるSQLを設定します
        If SetBaseSQLData(intMode, "honbumuke_kkk") = False Then
            Return -1
        End If

        commandTextSb.Append(" FROM m_honbu_seikyuu WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE jhs_syouhin_cd = " & strParamSyouhinCd)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.Char, 8, strSyouhinCd)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            seikyuuKakakuDataSet, dataTableName, commandParameters)

        If GetKingaku(intMode, intKingaku, intReturnKingaku) = False Then
            ' "マスターにありません。経理に追加の連絡をして下さい。" 
            Return 0
        End If

        Return 1

    End Function

End Class
