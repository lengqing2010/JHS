Imports System.IO
Imports System.Text
Imports Itis.ApplicationBlocks.ExceptionManagement

''' <summary>
''' �t�@�C���_�E�����[�h�@�\�̃w���p�[�N���X�ł�
''' </summary>
''' <remarks>���X�g�f�[�^���t�@�C���������s�����肵�܂�</remarks>
Public Class FileDownloadHelper

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�t�@�C���̍쐬�^�C�v"
    ''' <summary>
    ''' ��������t�@�C���̃^�C�v�ł�<br/>
    ''' CSV,TSV�t�@�C�����_�u���N�E�H�g�L���Ŏw��\
    ''' </summary>
    ''' <remarks>�f�[�^�ɃJ���}��^�u���܂܂��ꍇ��<br/>
    '''          �_�u���N�E�H�[�g�t���������߂��܂�</remarks>
    Enum MakeFileType
        ''' <summary>
        ''' CSV�t�@�C���𐶐����܂� �_�u���N�E�H�[�g�t��<br/>
        ''' ["@@@@","@@@@","@@@@"]
        ''' </summary>
        ''' <remarks></remarks>
        TYPE_CSV_WITH_QUOTE = 0
        ''' <summary>
        ''' CSV�t�@�C���𐶐����܂� �_�u���N�E�H�[�g����<br/>
        ''' [@@@@,@@@@,@@@@]
        ''' </summary>
        ''' <remarks></remarks>
        TYPE_CSV_WITHOUT_QUOTE = 1
        ''' <summary>
        ''' TSV�t�@�C���𐶐����܂� �_�u���N�E�H�[�g�t��<br/>
        ''' ["@@@@"\t"@@@@"\t"@@@@"]
        ''' </summary>
        ''' <remarks></remarks>
        TYPE_TSV_WITH_QUOTE = 2
        ''' <summary>
        ''' TSV�t�@�C���𐶐����܂� �_�u���N�E�H�[�g����<br/>
        ''' [@@@@\t@@@@\t@@@@]
        ''' </summary>
        ''' <remarks></remarks>
        TYPE_TSV_WITHOUT_QUOTE = 3
    End Enum
#End Region

#Region "�v���p�e�B"

#Region "��������t�@�C���̃^�C�v"
    ''' <summary>
    ''' ��������t�@�C���̃^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Private _fileType As MakeFileType
    ''' <summary>
    ''' ��������t�@�C���̃^�C�v
    ''' </summary>
    ''' <value>��������t�@�C���̃^�C�v</value>
    ''' <returns>��������t�@�C���̃^�C�v</returns>
    ''' <remarks></remarks>
    Public Property FileType() As MakeFileType
        Get
            Return _fileType
        End Get
        Set(ByVal value As MakeFileType)
            _fileType = value
        End Set
    End Property
#End Region

#Region "���s����"
    ''' <summary>
    ''' ���s����
    ''' </summary>
    ''' <remarks></remarks>
    Private _paragraphWord As String = vbCrLf
    ''' <summary>
    ''' ���s����
    ''' </summary>
    ''' <value>���s����</value>
    ''' <returns>���s����</returns>
    ''' <remarks></remarks>
    Public Property ParagraphWord() As String
        Get
            Return _paragraphWord
        End Get
        Set(ByVal value As String)
            _paragraphWord = value
        End Set
    End Property
#End Region

#End Region

#Region "�R���X�g���N�^"
    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="type">��������t�@�C���^�C�v</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal type As MakeFileType)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".New", _
                                                    type)
        ' �o�̓^�C�v�̎w��
        _fileType = type

    End Sub
#End Region

#Region "�t�@�C������"
    ''' <summary>
    ''' �����̔z����w�肵���p�X�Ƀt�@�C���o�͂��܂�
    ''' </summary>
    ''' <param name="list">
    ''' �t�@�C���쐬�Ώۂ̃��X�g<br/>
    ''' 1������String�z��̃��X�g</param>
    ''' <param name="path">�t�@�C���̍쐬��</param>
    ''' <returns>�s���R�[�h��ێ�����List</returns>
    ''' <remarks></remarks>
    Public Function MakeFile(ByVal list As List(Of String()), _
                             ByVal path As String) As List(Of String)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeFile", _
                                                    list, _
                                                    path)

        Dim dbSaveList As New List(Of String)

        ' �㏑�����[�h�ŃX�g���[���𐶐�
        Using sw As StreamWriter = New StreamWriter(path)
            Dim i As Integer
            Dim separator As String = GetSeparator()

            For Each lineData As String() In list

                For i = 0 To lineData.Length - 1

                    Dim saveLineData As New StringBuilder

                    ' �P���ڂ��ݒ�
                    saveLineData.Append(GetWithDoubleQuoteString(lineData(i)))

                    ' �Ō�̍��ڂ͉��s��������Z�b�g�A����ȊO�͋�؂蕶�����Z�b�g
                    If i = lineData.Length - 1 Then
                        saveLineData.Append(_paragraphWord)
                    Else
                        saveLineData.Append(separator)
                    End If

                    ' �X�g���[���ɏ�������
                    sw.Write(saveLineData.ToString())

                    ' DB�ۑ��p�̔z��ɐݒ�
                    dbSaveList.Add(saveLineData.ToString())
                Next
            Next

            sw.Close()
        End Using

        Return dbSaveList

    End Function
#End Region

#Region "�����͂�"
    ''' <summary>
    ''' �������K�v�ɉ����ĕҏW���ԋp���܂�<br/>
    ''' ���[�h�ɉ����ă_�u���N�E�H�[�g�ň͂�ŕԋp���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetWithDoubleQuoteString(ByVal word As String) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetWithDoubleQuoteString", _
                                                    word)

        Dim quoteWord As String = word

        Select Case (_paragraphWord)
            Case MakeFileType.TYPE_CSV_WITH_QUOTE
                quoteWord = String.Format("""{0}""", word)
            Case MakeFileType.TYPE_TSV_WITH_QUOTE
                quoteWord = String.Format("""{0}""", word)
        End Select

        Return quoteWord

    End Function
#End Region

#Region "��؂蕶���擾"
    ''' <summary>
    ''' ��؂蕶�����擾���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeparator() As String

        Dim separator As String = ""

        Select Case (_paragraphWord)
            Case MakeFileType.TYPE_CSV_WITH_QUOTE
                separator = ","
            Case MakeFileType.TYPE_CSV_WITHOUT_QUOTE
                separator = ","
            Case MakeFileType.TYPE_TSV_WITH_QUOTE
                separator = vbTab
            Case MakeFileType.TYPE_TSV_WITHOUT_QUOTE
                separator = vbTab
        End Select

        Return separator

    End Function
#End Region

End Class
