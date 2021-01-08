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
    ''' INI�t�@�C���p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property INIFilePath() As String
        Get
            Return DiskMei & ConfigurationManager.AppSettings("INIFilePath")
        End Get
    End Property

    ''' <summary>
    ''' �����񍐏��R�s�[���̃p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property KensaHoukokusyoCopyFromPath() As String
        Get
            Return ConfigurationManager.AppSettings("KensaHoukokusyoCopyFromPath")
        End Get
    End Property

    ''' <summary>
    ''' �����񍐏��R�s�[��̃p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property KensaHoukokusyoCopyToPath() As String
        Get
            Return ConfigurationManager.AppSettings("KensaHoukokusyoCopyToPath")
        End Get
    End Property

    ''' <summary>
    ''' �����񍐏��G���[�t�H���_��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property KensaHoukokusyoCopyErrorFolderName() As String
        Get
            Return ConfigurationManager.AppSettings("KensaHoukokusyoCopyErrorFolderName")
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
    ''' �����񍐏��R�s�[���̃p�X���擾����
    ''' </summary>
    Public Shared Function GetKensaHoukokusyoCopyFromPath() As String
        Return KensaHoukokusyoCopyFromPath
    End Function

    ''' <summary>
    ''' �����񍐏��R�s�[��̃p�X���擾����
    ''' </summary>
    Public Shared Function GetKensaHoukokusyoCopyToPath() As String
        Return KensaHoukokusyoCopyToPath
    End Function

    ''' <summary>
    ''' �����񍐏��G���[�t�H���_�����擾����
    ''' </summary>
    Public Shared Function GetKensaHoukokusyoCopyErrorFolderName() As String
        Return KensaHoukokusyoCopyErrorFolderName
    End Function

#End Region

#Region "GetMainDbConnectionString"
    ''' <summary>DB�ڑ�������̎擾</summary>
    Public Shared Function GetConnectionStringEarth() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringEarth").ConnectionString
    End Function

    ''' <summary>DB�ڑ�������̎擾</summary>
    Public Shared Function GetConnectionStringJhsfgm() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringJhsfgm").ConnectionString
    End Function

#End Region

End Class

