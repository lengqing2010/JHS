Option Explicit On
Option Strict On

Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Data.SqlClient
Imports System.Globalization.CultureInfo

''' <summary>�����Ɋւ��鋤�ʏ���</summary>
Public Class Definition
#Region "�萔"
    '�t�@�C���g���q
    Private Const CON_INI As String = ".INI"
    Private Const CON_DAT As String = ".DAT"
    Private Const CON_SQL As String = ".SQL"
#End Region

#Region "�v���p�e�B"
    ''' <summary>
    ''' �e�t�@�C���p�[�X�̏����ݒ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property DiskMei() As String
        Get
            Return ConfigurationManager.AppSettings("DISK_MEI")
        End Get
    End Property

    ''' <summary>
    ''' �v��N�x�t�@�C�����iS0004�p�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property CTLFileName4() As String
        Get
            Return ConfigurationManager.AppSettings("CTLFileName4")
        End Get
    End Property

    ''' <summary>
    ''' �v��N�x�t�@�C�����iS0005�p�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property CTLFileName5() As String
        Get
            Return ConfigurationManager.AppSettings("CTLFileName5")
        End Get
    End Property

    ''' <summary>
    ''' INI�t�@�C���p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property CTLFilePath() As String
        Get
            Return DiskMei & ConfigurationManager.AppSettings("CTLFilePath")
        End Get
    End Property

    ''' <summary>
    ''' �n��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property KeiretuCd() As String
        Get
            Return ConfigurationManager.AppSettings("KeiretuCd")
        End Get
    End Property

    ''' <summary>
    ''' �敪(S0003�p)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 ��A/���F �ǉ�</para>
    Private Shared ReadOnly Property KubunName3() As String
        Get
            Return ConfigurationManager.AppSettings("KubunName3")
        End Get
    End Property

    ''' <summary>
    ''' �敪(S0004�p)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 ��A/���F �ǉ�</para>
    Private Shared ReadOnly Property KubunName4() As String
        Get
            Return ConfigurationManager.AppSettings("KubunName4")
        End Get
    End Property

    ''' <summary>
    ''' �敪(S0005�p)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 ��A/���F �ǉ�</para>
    Private Shared ReadOnly Property KubunName5() As String
        Get
            Return ConfigurationManager.AppSettings("KubunName5")
        End Get
    End Property

    ''' <summary>
    ''' ���i���(S0005�p)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 ��A/���F �ǉ�</para>
    Private Shared ReadOnly Property SyouhinSyubetuName5() As String
        Get
            Return ConfigurationManager.AppSettings("SyouhinSyubetuName5")
        End Get
    End Property

#End Region

#Region "�t�@�C�����\�b�h"
    ''' <summary>
    ''' READER���[�h�Ńt�@�C���I�[�v������B
    ''' </summary>
    ''' <param name="strFile">�t�@�C��</param>
    Public Shared Function OpenFileReader(ByVal strFile As String) As StreamReader
        Return New StreamReader(strFile, System.Text.Encoding.GetEncoding(932))
    End Function
#End Region

#Region "�O�����ʃt�@�C���̐ݒ胁�\�b�h"
    ''' <summary>
    ''' EXE�t�@�C�����̎擾
    ''' </summary>
    ''' <param name="strID">�o�b�`ID</param>
    ''' <returns>EXE�t�@�C����</returns>
    Public Shared Function getExeName(ByVal strID As String) As String
        Return "JHS.Batch." & strID
    End Function

    ''' <summary>
    ''' CTL�t�@�C�����̎擾(S0004�p)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCTLFile4() As String
        Return CTLFilePath & CTLFileName4 & CON_INI
    End Function

    ''' <summary>
    ''' CTL�t�@�C�����̎擾(S0005�p)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCTLFile5() As String
        Return CTLFilePath & CTLFileName5 & CON_INI
    End Function

    ''' <summary>
    ''' �n��R�[�h�̎擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKeiretuCd() As String
        Return KeiretuCd
    End Function

    ''' <summary>
    '''  �v��N�x�t�@�C����Ǎ��݁A�v��N�x���擾����
    ''' </summary>
    ''' <returns>�v��N�x</returns>
    ''' <history>
    ''' <para>2013/01/15 ��A/���V �V�K�쐬 P-44979</para>
    ''' </history>
    Public Shared Function GetKeikakuNendo(ByVal batId As String) As DataTable
        Dim strReader As StreamReader
        Dim dtValue As DataTable
        Dim drValue As DataRow
        Dim strYear() As String
        Dim strY As String
        Dim errEx As Exception
        Dim strPath As String           '�t�@�C���p�X
        Dim i As Integer

        Try
            '�t�@�C���p�X���擾����
            If batId = "S0004" Then
                strPath = Definition.GetCTLFile4()
            Else
                strPath = Definition.GetCTLFile5()
            End If

            strReader = Definition.OpenFileReader(strPath)
        Catch ex As Exception
            ex.Data.Add("ERROR_LOG", "�v��N�x�h�m�h�t�@�C�������݂��܂���ł����B")
            Throw ex
        End Try

        '�v��N�x�b�s�k�t�@�C�������݂��Ȃ��ꍇ�A�G���[��߂�
        If strReader Is Nothing Then
            errEx = New Exception()
            errEx.Data.Add("ERROR_LOG", "�v��N�x�h�m�h�t�@�C�������݂��܂���ł����B")
            Throw errEx
        End If

        dtValue = New DataTable
        dtValue.Columns.Add("Year")
        dtValue.Columns.Add("BeginMonth")
        dtValue.Columns.Add("EndMonth")

        '�v��N�x���擾����
        strY = ""
        While Not strReader.EndOfStream
            drValue = dtValue.NewRow()

            strY = strReader.ReadLine()
            strYear = strY.Split(CChar(","))

            '�v��N�x��ۑ�����
            drValue("Year") = strYear(0)

            If strYear.Length > 1 Then
                '�J�n����ۑ�����
                drValue("BeginMonth") = strYear(1)

                '��������ۑ�����
                drValue("EndMonth") = strYear(2)
            Else
                '�J�n����ۑ�����
                drValue("BeginMonth") = strYear(0) & "/04/01"

                '��������ۑ�����
                drValue("EndMonth") = DateAdd(DateInterval.Year, 1, Convert.ToDateTime(strYear(0) & "/03/31")).ToString("yyyy/MM/dd")

            End If


            dtValue.Rows.Add(drValue)
        End While

        strReader.Close()
        strReader.Dispose()
        strReader = Nothing

        '�v��N�x���󔒂̏ꍇ
        If dtValue Is Nothing OrElse dtValue.Rows.Count <= 0 Then
            errEx = New Exception()
            errEx.Data.Add("ERROR_LOG", "�v��N�x�h�m�h�t�@�C�����烌�R�[�h���擾�ł��܂���ł����B")
            Throw errEx
        End If

        If dtValue.Rows(0)("Year") Is Nothing Then
            errEx = New Exception()
            errEx.Data.Add("ERROR_LOG", "�v��N�x�h�m�h�t�@�C�����烌�R�[�h���擾�ł��܂���ł����B")
            Throw errEx
        End If

        '�v��N�x�b�s�k�t�@�C���̗L�����R�[�h���Ȃ��ꍇ�A�G���[��߂�
        For i = 0 To dtValue.Rows.Count - 1
            If Not IsNumeric(dtValue.Rows(i)("Year")) Then
                errEx = New Exception()
                errEx.Data.Add("ERROR_LOG", "�v��N�x�h�m�h�t�@�C�����烌�R�[�h���擾�ł��܂���ł����B")
                Throw errEx
            ElseIf Convert.ToInt32(dtValue.Rows(i)("Year")) < 1 OrElse Convert.ToInt32(dtValue.Rows(i)("Year")) > 9999 Then
                errEx = New Exception()
                errEx.Data.Add("ERROR_LOG", "�v��N�x�h�m�h�t�@�C�����烌�R�[�h���擾�ł��܂���ł����B")
                Throw errEx
            End If
        Next

        Return dtValue

    End Function

    ''' <summary>
    ''' �敪(S0003�p)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 ��A/���F �ǉ�</para>
    Public Shared Function GetKubunName3() As String
        Return KubunName3
    End Function

    ''' <summary>
    ''' �敪(S0004�p)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 ��A/���F �ǉ�</para>
    Public Shared Function GetKubunName4() As String
        Return KubunName4
    End Function

    ''' <summary>
    ''' �敪(S0005�p)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 ��A/���F �ǉ�</para>
    Public Shared Function GetKubunName5() As String
        Return KubunName5
    End Function

    ''' <summary>
    ''' ���i���(S0005�p)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 ��A/���F �ǉ�</para>
    Public Shared Function GetSyouhinSyubetuName5() As String
        Return SyouhinSyubetuName5
    End Function

#End Region

#Region "SQL��"
    Public Enum SqlStringKbn As Integer
        S0003_01 = 0
        S0003_02
        S0003_03
        S0003_04
        S0003_05
        S0003_06
        S0003_07
        S0003_08

        S0004_01
        S0004_02
        S0004_03
        S0004_04

        S0005_01
        S0005_02
        S0005_03
    End Enum

    ''' <summary>
    ''' SQL�t�@�C���p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property SqlFilePath() As String
        Get
            Return DiskMei & ConfigurationManager.AppSettings("SqlFilePath")
        End Get
    End Property

    Private Shared ReadOnly Property SqlFileNameS0003_01() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0003_01")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0003_02() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0003_02")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0003_03() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0003_03")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0003_04() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0003_04")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0003_05() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0003_05")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0003_06() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0003_06")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0003_07() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0003_07")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0003_08() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0003_08")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0004_01() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0004_01")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0004_02() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0004_02")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0004_03() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0004_03")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0004_04() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0004_04")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0005_01() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0005_01")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0005_02() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0005_02")
        End Get
    End Property
    Private Shared ReadOnly Property SqlFileNameS0005_03() As String
        Get
            Return ConfigurationManager.AppSettings("SqlFileNameS0005_03")
        End Get
    End Property

    ''' <summary>
    '''  �v��N�x�t�@�C����Ǎ��݁A�v��N�x���擾����
    ''' </summary>
    Public Shared Function GetSqlString(ByVal sqlKbn As SqlStringKbn) As String
        Dim strPath As String = String.Empty
        Dim strReader As StreamReader = Nothing
        Dim errEx As Exception
        Dim sqlStr As New System.Text.StringBuilder
        Dim strFileName As String = String.Empty

        Try
            Select Case sqlKbn
                Case SqlStringKbn.S0003_01
                    strFileName = SqlFileNameS0003_01 & CON_SQL
                Case SqlStringKbn.S0003_02
                    strFileName = SqlFileNameS0003_02 & CON_SQL
                Case SqlStringKbn.S0003_03
                    strFileName = SqlFileNameS0003_03 & CON_SQL
                Case SqlStringKbn.S0003_04
                    strFileName = SqlFileNameS0003_04 & CON_SQL
                Case SqlStringKbn.S0003_05
                    strFileName = SqlFileNameS0003_05 & CON_SQL
                Case SqlStringKbn.S0003_06
                    strFileName = SqlFileNameS0003_06 & CON_SQL
                Case SqlStringKbn.S0003_07
                    strFileName = SqlFileNameS0003_07 & CON_SQL
                Case SqlStringKbn.S0003_08
                    strFileName = SqlFileNameS0003_08 & CON_SQL

                Case SqlStringKbn.S0004_01
                    strFileName = SqlFileNameS0004_01 & CON_SQL
                Case SqlStringKbn.S0004_02
                    strFileName = SqlFileNameS0004_02 & CON_SQL
                Case SqlStringKbn.S0004_03
                    strFileName = SqlFileNameS0004_03 & CON_SQL
                Case SqlStringKbn.S0004_04
                    strFileName = SqlFileNameS0004_04 & CON_SQL

                Case SqlStringKbn.S0005_01
                    strFileName = SqlFileNameS0005_01 & CON_SQL
                Case SqlStringKbn.S0005_02
                    strFileName = SqlFileNameS0005_02 & CON_SQL
                Case SqlStringKbn.S0005_03
                    strFileName = SqlFileNameS0005_03 & CON_SQL

                Case Else
                    strPath = String.Empty

            End Select

            strPath = SqlFilePath & strFileName

            strReader = Definition.OpenFileReader(strPath)
        Catch ex As Exception
            If Not strFileName.Equals(String.Empty) Then
                strFileName = "�u" & strFileName & "�v"
            End If
            ex.Data.Add("ERROR_LOG", "�r�p�k�t�@�C��" & strFileName & "�����݂��܂���ł����B")
            Throw ex
        End Try

        '�r�p�k�t�@�C�������݂��Ȃ��ꍇ�A�G���[��߂�
        If strReader Is Nothing Then
            errEx = New Exception()
            If Not strFileName.Equals(String.Empty) Then
                strFileName = "�u" & strFileName & "�v"
            End If
            errEx.Data.Add("ERROR_LOG", "�r�p�k�t�@�C��" & strFileName & "�����݂��܂���ł����B")
            Throw errEx
        End If

        'SQL�����擾����
        While Not strReader.EndOfStream
            sqlStr.AppendLine(strReader.ReadLine())
        End While

        strReader.Close()
        strReader.Dispose()
        strReader = Nothing

        '�v��N�x���󔒂̏ꍇ
        If sqlStr.ToString.Trim.Equals(String.Empty) Then
            errEx = New Exception()
            If Not strFileName.Equals(String.Empty) Then
                strFileName = "�u" & strFileName & "�v"
            End If
            errEx.Data.Add("ERROR_LOG", "�r�p�k�t�@�C��" & strFileName & "������e���擾�ł��܂���ł����B")
            Throw errEx
        End If

        Return sqlStr.ToString
    End Function

#End Region

#Region "GetMainDbConnectionString"
    ''' <summary>DB�ڑ�������̎擾</summary>
    Public Shared Function GetConnectionStringJHS() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringJHS").ConnectionString
    End Function

    ''' <summary>DB�ڑ�������̎擾</summary>
    Public Shared Function GetConnectionStringEarth() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringEarth").ConnectionString
    End Function

    ''' <summary>DB�ڑ�������̎擾</summary>
    Public Shared Function GetConnectionStringASPSFA() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringASPSFA").ConnectionString
    End Function
#End Region

End Class

