Imports System.text
Imports System.Data.SqlClient


Public Class KousinRirekiDataAccess

    Public Function getSearchTable(ByVal keyRec As KousinRirekiDataKeyRecord) As DataTable

        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        Const strKubun As String = "@KUBUN" '区分
        Const strHosyousyoNo As String = "@HOSYOUSYO_NO" '保証書No
        Const strKousinbiFrom As String = "@KOUSINBI_FROM" '更新日 From
        Const strKousinbiTo As String = "@KOUSINBI_TO" '更新日 To
        Const strKousinKoumoku As String = "@KOUSIN_KOUMOKU" '更新項目
        Const strKousinsya As String = "@KOUSINBISYA" '更新者

        Const strKameitenCd As String = "@KAMEITENCD" '最新加盟店
        Const strKameitenKana As String = "@KAMEITENKANA" '最新加盟店カナ
        Const strKousinBeforeValue As String = "@KOUSINBEFOREVALUE" '更新前値
        Const strKousinAfterValue As String = "@KOUSINAFTERVALUE" '更新後値

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TKR.upd_datetime ")
        cmdTextSb.Append("      , TKR.kbn ")
        cmdTextSb.Append("      , TKR.hosyousyo_no ")
        cmdTextSb.Append("      , upd_koumoku ")
        cmdTextSb.Append("      , upd_mae_atai ")
        cmdTextSb.Append("      , upd_go_atai ")
        cmdTextSb.Append("      , TKR.kousinsya ")
        cmdTextSb.Append("   FROM ")
        cmdTextSb.Append("      t_kousin_rireki TKR ")

        '***********************************************************************
        ' 外部結合(地盤テーブル)
        '***********************************************************************
        cmdTextSb.Append("   LEFT JOIN ")
        cmdTextSb.Append("      t_jiban TJ ")
        cmdTextSb.Append("   ON ")
        cmdTextSb.Append("      TJ.kbn = TKR.kbn ")
        cmdTextSb.Append("   AND TJ.hosyousyo_no = TKR.hosyousyo_no ")

        '***********************************************************************
        ' 外部結合(加盟店マスタ)
        '***********************************************************************
        cmdTextSb.Append("   LEFT JOIN ")
        cmdTextSb.Append("      m_kameiten MK ")
        cmdTextSb.Append("   ON ")
        cmdTextSb.Append("      MK.kameiten_cd = TJ.kameiten_cd ")

        cmdTextSb.Append("   WHERE 1 = 1 ")

        '***********************************************************************
        ' 区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.Kubun) Then
            cmdTextSb.Append(" AND TKR.kbn = " & strKubun)
        End If

        '***********************************************************************
        '保証書NO
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.HosyousyoNo) Then
            cmdTextSb.Append(" AND TKR.hosyousyo_no = " & strHosyousyoNo)
        End If

        '***********************************************************************
        ' 最新加盟店
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.KameitenCd) Then
            cmdTextSb.Append(" AND MK.kameiten_cd = " & strKameitenCd)
        End If

        '***********************************************************************
        ' 最新加盟店カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.KameitenKana) Then
            cmdTextSb.Append(" AND (MK.tenmei_kana1 like " & strKameitenKana)
            cmdTextSb.Append(" OR ISNULL(MK.tenmei_kana2,'') like " & strKameitenKana & ") ")
        End If

        '***********************************************************************
        ' 更新項目
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.KousinKoumoku) Then
            cmdTextSb.Append(" AND TKR.upd_koumoku = " & strKousinKoumoku)
        End If

        '***********************************************************************
        ' 更新前値
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.KousinBeforeValue) Then
            cmdTextSb.Append(" AND TKR.upd_mae_atai like " & strKousinBeforeValue)
        End If

        '***********************************************************************
        ' 更新後値
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.KousinAfterValue) Then
            cmdTextSb.Append(" AND TKR.upd_go_atai like " & strKousinAfterValue)
        End If

        '***********************************************************************
        '更新日(From,To)
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.KousinbiFrom <> DateTime.MinValue Or _
            keyRec.KousinbiTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.KousinbiFrom <> DateTime.MinValue And _
                keyRec.KousinbiTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, TKR.upd_datetime ,111) BETWEEN " & strKousinbiFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strKousinbiTo)
            Else
                If keyRec.KousinbiFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, TKR.upd_datetime ,111) >= " & strKousinbiFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, TKR.upd_datetime ,111) <= " & strKousinbiTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 更新者
        '***********************************************************************
        'If Not String.IsNullOrEmpty(keyRec.Kousinsya) Then
        '    cmdTextSb.Append(" AND kousinsya = " & strKousinsya)
        'End If


        cmdTextSb.Append("   ORDER BY ")
        cmdTextSb.Append("      TKR.upd_datetime DESC ")
        cmdTextSb.Append("      , TKR.kbn ASC ")
        cmdTextSb.Append("      , TKR.hosyousyo_no ASC ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(strKubun, SqlDbType.Char, 1, keyRec.Kubun), _
            SQLHelper.MakeParam(strHosyousyoNo, SqlDbType.VarChar, 10, keyRec.HosyousyoNo), _
            SQLHelper.MakeParam(strKousinbiFrom, SqlDbType.DateTime, 16, IIf(keyRec.KousinbiFrom = DateTime.MinValue, DBNull.Value, keyRec.KousinbiFrom)), _
            SQLHelper.MakeParam(strKousinbiTo, SqlDbType.DateTime, 16, IIf(keyRec.KousinbiTo = DateTime.MinValue, DBNull.Value, keyRec.KousinbiTo)), _
            SQLHelper.MakeParam(strKousinKoumoku, SqlDbType.VarChar, 30, keyRec.KousinKoumoku), _
            SQLHelper.MakeParam(strKousinsya, SqlDbType.VarChar, 30, keyRec.Kousinsya), _
            SQLHelper.MakeParam(strKameitenCd, SqlDbType.VarChar, 5, keyRec.KameitenCd), _
            SQLHelper.MakeParam(strKameitenKana, SqlDbType.VarChar, 20, keyRec.KameitenKana & Chr(37)), _
            SQLHelper.MakeParam(strKousinBeforeValue, SqlDbType.VarChar, 512, Chr(37) & keyRec.KousinBeforeValue & Chr(37)), _
            SQLHelper.MakeParam(strKousinAfterValue, SqlDbType.VarChar, 512, Chr(37) & keyRec.KousinAfterValue & Chr(37)) _
            }

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)


    End Function


End Class
