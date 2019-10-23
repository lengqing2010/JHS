Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �ۏ؏��Ǘ��e�[�u���̏����擾���܂�
''' </summary>
''' <remarks></remarks>
Public Class BukkenSintyokuJykyDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �Y���e�[�u���̏����擾���܂�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNo">�ԍ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSearchTable(ByVal strKbn As String, ByVal strHosyousyoNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchTable" _
                                                    , strKbn _
                                                    , strHosyousyoNo _
                                                    )
        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter

        'SQL�N�G������
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT ")
        cmdTextSb.Append("       kbn ")
        cmdTextSb.Append("       , hosyousyo_no ")
        cmdTextSb.Append("       , bukken_jyky ")
        cmdTextSb.Append("       , kaiseki_kanry ")
        cmdTextSb.Append("       , koj_umu ")
        cmdTextSb.Append("       , koj_kanry ")
        cmdTextSb.Append("       , nyuukin_kakunin_jyouken ")
        cmdTextSb.Append("       , nyuukin_jyky ")
        cmdTextSb.Append("       , kasi ")
        cmdTextSb.Append("       , hoken_kaisya ")
        cmdTextSb.Append("       , hoken_sinsei_tuki ")
        cmdTextSb.Append("       , hoken_sinsei_kbn ")
        cmdTextSb.Append("       , hw_mae_hkn ")
        cmdTextSb.Append("       , hw_mae_hkn_date ")
        cmdTextSb.Append("       , hw_mae_hkn_jissi_date ")
        cmdTextSb.Append("       , hw_mae_hkn_tekiyou_yotei_jissi_date ")
        cmdTextSb.Append("       , hw_ato_hkn ")
        cmdTextSb.Append("       , hw_ato_hkn_date ")
        cmdTextSb.Append("       , hw_ato_hkn_jissi_date ")
        cmdTextSb.Append("       , hw_ato_hkn_tekiyou_yotei_jissi_date ")
        cmdTextSb.Append("       , hw_ato_hkn_torikesi_syubetsu ")
        cmdTextSb.Append("       , syori_flg ")
        cmdTextSb.Append("       , syori_datetime ")
        cmdTextSb.Append("       , hosyousyo_type ")
        cmdTextSb.Append("       , gyoumu_kanry_date ")
        cmdTextSb.Append("       , hosyou_kaisi_gyoumu_naiyou ")
        cmdTextSb.Append("       , add_login_user_id ")
        cmdTextSb.Append("       , add_datetime ")
        cmdTextSb.Append("       , upd_login_user_id ")
        cmdTextSb.Append("       , upd_datetime")
        cmdTextSb.Append("       FROM t_hosyousyo_kanri ")
        cmdTextSb.Append("       WHERE kbn          = @KBN")
        cmdTextSb.Append("       AND hosyousyo_no = @HOSYOUSYONO")


        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strHosyousyoNo)}

        ' �f�[�^�擾���ԋp
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' �������̕ۏ؏��Ǘ��󋵂̔��茋�ʂ��擾���܂�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ԍ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkHosyousyoJyky(ByVal strKbn As String, ByVal strBangou As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkHosyousyoJyky" _
                                                    , strKbn _
                                                    , strBangou _
                                                    )
        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter

        'SQL�N�G������
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT [jhs_sys].[fnGetHosyousyoJyky](@KBN,@HOSYOUSYONO) ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strBangou)}

        ' �f�[�^�̎擾
        Dim data As Object = Nothing

        ' �������s
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If data Is Nothing OrElse IsDBNull(data) Then
            Return -1
        End If

        Return data

    End Function

    ''' <summary>
    ''' �ۏ؏��Ǘ��e�[�u���ǉ�/�X�V����(�X�g�A�h�v���V�[�W��)
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ۏ؏�NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setHosyousyoKanriData(ByVal strKbn As String, ByVal strBangou As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetHosyousyoKanriData" _
                                                    , strKbn _
                                                    , strBangou _
                                                    )

        Dim cmdParams() As SqlClient.SqlParameter   '�p�����[�^
        Dim commandText As String                   '�X�g�A�h�v���V�[�W��
        Dim intResult As Integer = 0                

        '�X�g�A�h�v���V�[�W������ݒ�
        commandText = "[jhs_sys].[spSetHosyousyoKanri_bukken]"

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@COUNT_HK_INSERT", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@COUNT_HK_INSERT_UPDATE", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@COUNT_HK_BUKKEN_JYKY_UPDATE", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@COUNT_IF_UPDATE", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@ERR_NO", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@BANGOU", SqlDbType.VarChar, 10, strBangou), _
            SQLHelper.MakeParam("@ERR_NO_RETURN", SqlDbType.Int, 4, 0)}

        '�߂�l�i�[�p
        cmdParams(7).Direction = ParameterDirection.ReturnValue

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.StoredProcedure, _
                                    commandText, _
                                    cmdParams)

        Return cmdParams(7).Value

    End Function

    ''' <summary>
    ''' �����\������擾
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ۏ؏�NO</param>
    ''' <param name="dtHkUpdDatetime">�ۏ؏��Ǘ��f�[�^.�X�V����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setNyuukinYoteiDate(ByVal strKbn As String, ByVal strBangou As String, ByVal dtHkUpdDatetime As DateTime) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setNyuukinYoteiDate" _
                                                    , strKbn _
                                                    , strBangou _
                                                    , dtHkUpdDatetime _
                                                    )
        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter
        'SQL�N�G������
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine("SELECT ")
        cmdTextSb.AppendLine("     [jhs_sys].[fnGetNyuukinNasiInfo](2, @KBN, @HOSYOUSYONO, @UPDDATETIME) ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strBangou), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 10, dtHkUpdDatetime)}

        ' �f�[�^�̎擾
        Dim data As Object = Nothing

        ' �������s
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If data Is Nothing OrElse IsDBNull(data) Then
            Return String.Empty
        End If

        Return data.ToString

    End Function

End Class
