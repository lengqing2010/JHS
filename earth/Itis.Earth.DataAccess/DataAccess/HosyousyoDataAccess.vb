Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �ۏ؏�No���s�Ɋ֌W����f�[�^�A�N�Z�X�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HosyousyoDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �敪�������ɕۏ؏�No�N�����擾���擾���܂�
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <returns>�ۏ؏�No�N��</returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoNoYM(ByVal kubun As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoNoYM", _
                                    kubun)

        ' �߂�l
        Dim strHosyousyoYm As String = ""

        ' �p�����[�^
        Const strParamKubun As String = "@KUBUN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,hosyousyo_hak_date,hosyousyo_no_nengetu,hkks_hassou_date ")
        commandTextSb.Append("  FROM m_hiduke_save WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE kbn = " & strParamKubun)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, kubun)}

        ' �f�[�^�擾���ԋp
        Dim cmnDtAcc As New CmnDataAccess
        Dim dTbl As New DataTable
        dTbl = cmnDtAcc.getDataTable(commandTextSb.ToString, commandParameters)
        If dTbl.Rows.Count > 0 Then
            If Not dTbl.Rows(0)("hosyousyo_no_nengetu") Is Nothing AndAlso Not dTbl.Rows(0)("hosyousyo_no_nengetu") Is DBNull.Value Then
                strHosyousyoYm = Format(dTbl.Rows(0)("hosyousyo_no_nengetu"), "yyyy/MM")
            End If
        End If

        Return strHosyousyoYm

    End Function

    ''' <summary>
    ''' �敪�ƔN���������ɕۏ؏�No�̍ŏI�ԍ����擾���܂�
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoYm">�ۏ؏�No�N��</param>
    ''' <returns>�ŏI�ԍ����</returns>
    ''' <remarks>�擾�ł��Ȃ��ꍇ�A-1��Ԃ��܂�</remarks>
    Public Function GetHosyousyoLastNo(ByVal strKubun As String, _
                                       ByVal strHosyousyoYm As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoLastNo", _
                                                    strKubun, _
                                                    strHosyousyoYm)

        ' �p�����[�^
        Const strParamKubun As String = "@KUBUN"
        Const strParamHosyouYm As String = "@HOSYOUYM"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,nengetu,saisyuu_no ")
        commandTextSb.Append("  FROM m_hosyousyo_no_saiban WITH(UPDLOCK) ")
        commandTextSb.Append("  WHERE kbn = " & strParamKubun)
        commandTextSb.Append("  AND   nengetu = " & strParamHosyouYm)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, strKubun), _
             SQLHelper.MakeParam(strParamHosyouYm, SqlDbType.Char, 6, strHosyousyoYm)}

        ' �f�[�^�̎擾
        Dim HosyousyoNoDataSet As New HosyousyoNoDataSet()

        ' �������s
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            HosyousyoNoDataSet, HosyousyoNoDataSet.HosyousyoNoTable.TableName, commandParameters)

        Dim hosyousyoTable As HosyousyoNoDataSet.HosyousyoNoTableDataTable = _
                    HosyousyoNoDataSet.HosyousyoNoTable

        ' �擾�ł��Ȃ������ꍇ�͐V�K�̔ԁi�߂�l��-1�j
        If hosyousyoTable.Count = 0 Then
            Return -1
        End If

        ' �擾�ł����ꍇ�A�s�����擾����
        Dim row As HosyousyoNoDataSet.HosyousyoNoTableRow = hosyousyoTable(0)

        Return row.saisyuu_no

    End Function

    ''' <summary>
    ''' TBL_�ۏ؏�NO�̔Ԃɍ̔Ԓl�𔽉f���܂�
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strYm">�N��</param>
    ''' <param name="intHosyousyoLastNo">�ŏI�ԍ�</param>
    ''' <param name="strMode">�������[�h N:�o�^ U:�X�V</param>
    ''' <param name="strLoginUserId">���O�C�����[�UID</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdateHosyousyoNo(ByVal strKubun As String, _
                                      ByVal strYm As String, _
                                      ByVal intHosyousyoLastNo As Integer, _
                                      ByVal strMode As String, _
                                      ByVal strLoginUserId As String _
                                      ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateHosyousyoNo", _
                                            strKubun, _
                                            strYm, _
                                            intHosyousyoLastNo, _
                                            strMode, _
                                            strLoginUserId)

        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder

        ' �p�����[�^
        Const strPrmKbn As String = "@KBN" '�敪
        Const strPrmNengetu As String = "@NENGETU" '�N��
        Const strPrmLastNo As String = "@LAST_NO" '�ŏINO
        Const strPrmAddDatatime As String = "@ADD_DATETIME" '�o�^����
        Const strPrmAddLoginUserID As String = "@ADD_LOGIN_USER_ID" '�o�^���O�C�����[�UID
        Const strPrmUpdDatatime As String = "@UPD_DATETIME" '�X�V����
        Const strPrmUpdLoginUserID As String = "@UPD_LOGIN_USER_ID" '�X�V���O�C�����[�UID

        ' �o�^���[�h "N" �̏ꍇ�AInsert
        If strMode = "N" Then

            cmdTextSb.Append(" INSERT INTO ")
            cmdTextSb.Append("      m_hosyousyo_no_saiban ")
            cmdTextSb.Append(" ( ")
            cmdTextSb.Append("      kbn ")
            cmdTextSb.Append("    , nengetu ")
            cmdTextSb.Append("    , saisyuu_no ")
            cmdTextSb.Append("    , add_login_user_id ")
            cmdTextSb.Append("    , add_datetime ")
            cmdTextSb.Append(" ) ")
            cmdTextSb.Append(" VALUES ")
            cmdTextSb.Append(" ( ")
            cmdTextSb.Append(strPrmKbn)
            cmdTextSb.Append("," & strPrmNengetu)
            cmdTextSb.Append("," & strPrmLastNo)
            cmdTextSb.Append("," & strPrmAddLoginUserID)
            cmdTextSb.Append("," & strPrmAddDatatime)
            cmdTextSb.Append(" ) ")

        Else 'UPDATE

            cmdTextSb.Append(" UPDATE ")
            cmdTextSb.Append("      m_hosyousyo_no_saiban ")
            cmdTextSb.Append(" SET ")
            cmdTextSb.Append("      saisyuu_no = " & strPrmLastNo)
            cmdTextSb.Append("      ,upd_login_user_id = " & strPrmUpdLoginUserID)
            cmdTextSb.Append("      ,upd_datetime = " & strPrmUpdDatatime)
            cmdTextSb.Append(" WHERE ")
            cmdTextSb.Append("      kbn = " & strPrmKbn)
            cmdTextSb.Append("  AND nengetu = " & strPrmNengetu)

        End If

        ' �p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = { _
             SQLHelper.MakeParam(strPrmKbn, SqlDbType.Char, 1, strKubun), _
             SQLHelper.MakeParam(strPrmNengetu, SqlDbType.Char, 6, strYm), _
             SQLHelper.MakeParam(strPrmLastNo, SqlDbType.Int, 4, intHosyousyoLastNo), _
             SQLHelper.MakeParam(strPrmAddLoginUserID, SqlDbType.VarChar, 30, strLoginUserId), _
             SQLHelper.MakeParam(strPrmAddDatatime, SqlDbType.DateTime, 16, DateTime.Now), _
             SQLHelper.MakeParam(strPrmUpdLoginUserID, SqlDbType.VarChar, 30, strLoginUserId), _
             SQLHelper.MakeParam(strPrmUpdDatatime, SqlDbType.DateTime, 16, DateTime.Now) _
         }

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString, _
                                    cmdParams)

        Return intResult
    End Function
End Class
