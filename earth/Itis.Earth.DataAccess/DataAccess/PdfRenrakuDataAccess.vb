Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' PDFデータの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class PdfRenrakuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

#Region "PDFデータの取得"
    ''' <summary>
    ''' PDFレコードを取得します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>   
    ''' <returns>PDFデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetPDFData(ByVal kbn As String, _
                               ByVal hosyousyoNo As String, _
                               ByVal accountno As Integer) As DataSet

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetPDFData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            accountno)

        ' パラメータ
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamaccountno As String = "@ACCOUNTNO"


        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strParamaccountno, SqlDbType.Int, 4, accountno)}

        'Dim commandText As String = "SELECT t_jiban.kbn ," & _
        '                            "t_jiban.hosyousyo_no , " & _
        '                            "m_kameiten.kameiten_mei1 ," & _
        '                            "m_eigyousyo.eigyousyo_mei," & _
        '                            "m_kameiten.kameiten_cd," & _
        '                            "m_eigyousyo.eigyousyo_mei_inji_umu, " & _
        '                            "m_kameiten.koj_tantou_flg, " & _
        '                            "t_jiban.koj_tantousya_mei, " & _
        '                            "t_jiban.irai_tantousya_mei, " & _
        '                            "t_jiban.sesyu_mei, " & _
        '                            "t_jiban.bukken_jyuusyo1, " & _
        '                            "t_jiban.bukken_jyuusyo2, " & _
        '                            "t_jiban.bukken_jyuusyo3, " & _
        '                            "t_jiban.tys_kibou_date, " & _
        '                            "t_jiban.tys_kibou_jikan, " & _
        '                            "t_jiban.tatiai_umu, " & _
        '                            "t_jiban.tatiaisya_cd, " & _
        '                            "m_tyousakaisya.tys_kaisya_mei, " & _
        '                            "m_tyousakaisya.tel_no, " & _
        '                            "m_tyousakaisya.fax_no, " & _
        '                            "m_jhs.yuubin_no, " & _
        '                            "m_jhs.jyuusyo1, " & _
        '                            "m_jhs.jyuusyo2, " & _
        '                            "m_jhs.kaiseki_tel, " & _
        '                            "m_tantousya.tantousya_mei " & _
        '                            "FROM t_jiban " & _
        '                            "LEFT JOIN m_tantousya " & _
        '                            "ON m_tantousya.tantousya_cd = " & strParamaccountno.Trim & _
        '                            " LEFT JOIN m_kameiten " & _
        '                            "ON t_jiban.kameiten_cd = m_kameiten.kameiten_cd " & _
        '                            "LEFT JOIN m_eigyousyo " & _
        '                            "ON m_kameiten.eigyousyo_cd = m_eigyousyo.eigyousyo_cd " & _
        '                            "LEFT JOIN m_tyousakaisya " & _
        '                            "ON m_tyousakaisya.tys_kaisya_cd = t_jiban.tys_kaisya_cd " & _
        '                            "AND m_tyousakaisya.jigyousyo_cd = t_jiban.tys_kaisya_jigyousyo_cd, " & _
        '                            "m_jhs " & _
        '                            " WHERE " & _
        '                            "    t_jiban.kbn = " & strParamKbn & _
        '                            " AND " & _
        '                            "    t_jiban.hosyousyo_no = " & strParamHosyousyoNo
        Dim commandText As String = "SELECT t_jiban.kbn ," & _
                                    "t_jiban.hosyousyo_no , " & _
                                    "m_kameiten.kameiten_mei1 ," & _
                                    "m_eigyousyo.eigyousyo_mei," & _
                                    "m_kameiten.kameiten_cd," & _
                                    "m_eigyousyo.eigyousyo_mei_inji_umu, " & _
                                    "m_kameiten.koj_tantou_flg, " & _
                                    "t_jiban.koj_tantousya_mei, " & _
                                    "t_jiban.irai_tantousya_mei, " & _
                                    "t_jiban.sesyu_mei, " & _
                                    "t_jiban.bukken_jyuusyo1, " & _
                                    "t_jiban.bukken_jyuusyo2, " & _
                                    "t_jiban.bukken_jyuusyo3, " & _
                                    "t_jiban.syoudakusyo_tys_date, " & _
                                    "t_jiban.tys_kibou_jikan, " & _
                                    "t_jiban.tatiai_umu, " & _
                                    "t_jiban.tatiaisya_cd, " & _
                                    "m_tyousakaisya.tys_kaisya_mei, " & _
                                    "m_tyousakaisya.tel_no, " & _
                                    "m_tyousakaisya.fax_no, " & _
                                    "m_jhs.yuubin_no, " & _
                                    "m_jhs.jyuusyo1, " & _
                                    "m_jhs.jyuusyo2, " & _
                                    "m_jhs.kaiseki_tel, " & _
                                    "m_tantousya.tantousya_mei " & _
                                    "FROM t_jiban WITH (READCOMMITTED) " & _
                                    "LEFT JOIN m_tantousya WITH (READCOMMITTED) " & _
                                    "ON m_tantousya.tantousya_cd = " & strParamaccountno.Trim & _
                                    " LEFT JOIN m_kameiten WITH (READCOMMITTED) " & _
                                    "ON t_jiban.kameiten_cd = m_kameiten.kameiten_cd " & _
                                    "LEFT JOIN m_eigyousyo WITH (READCOMMITTED) " & _
                                    "ON m_kameiten.eigyousyo_cd = m_eigyousyo.eigyousyo_cd " & _
                                    "LEFT JOIN m_tyousakaisya WITH (READCOMMITTED) " & _
                                    "ON m_tyousakaisya.tys_kaisya_cd = t_jiban.tys_kaisya_cd " & _
                                    "AND m_tyousakaisya.jigyousyo_cd = t_jiban.tys_kaisya_jigyousyo_cd, " & _
                                    "m_jhs  WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    t_jiban.kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    t_jiban.hosyousyo_no = " & strParamHosyousyoNo

        ' データの取得
        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataset(connStr, CommandType.Text, commandText, commandParameters)

        Return ds

    End Function
#End Region



    End Class
