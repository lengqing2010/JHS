Imports System.IO
Imports System.Text
Imports Itis.ApplicationBlocks.ExceptionManagement

''' <summary>
''' ファイルダウンロード機能のヘルパークラスです
''' </summary>
''' <remarks>リストデータよりファイル生成を行ったりします</remarks>
Public Class FileDownloadHelper

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "ファイルの作成タイプ"
    ''' <summary>
    ''' 生成するファイルのタイプです<br/>
    ''' CSV,TSVファイルをダブルクウォト有無で指定可能
    ''' </summary>
    ''' <remarks>データにカンマやタブが含まれる場合は<br/>
    '''          ダブルクウォート付きをお勧めします</remarks>
    Enum MakeFileType
        ''' <summary>
        ''' CSVファイルを生成します ダブルクウォート付き<br/>
        ''' ["@@@@","@@@@","@@@@"]
        ''' </summary>
        ''' <remarks></remarks>
        TYPE_CSV_WITH_QUOTE = 0
        ''' <summary>
        ''' CSVファイルを生成します ダブルクウォート無し<br/>
        ''' [@@@@,@@@@,@@@@]
        ''' </summary>
        ''' <remarks></remarks>
        TYPE_CSV_WITHOUT_QUOTE = 1
        ''' <summary>
        ''' TSVファイルを生成します ダブルクウォート付き<br/>
        ''' ["@@@@"\t"@@@@"\t"@@@@"]
        ''' </summary>
        ''' <remarks></remarks>
        TYPE_TSV_WITH_QUOTE = 2
        ''' <summary>
        ''' TSVファイルを生成します ダブルクウォート無し<br/>
        ''' [@@@@\t@@@@\t@@@@]
        ''' </summary>
        ''' <remarks></remarks>
        TYPE_TSV_WITHOUT_QUOTE = 3
    End Enum
#End Region

#Region "プロパティ"

#Region "生成するファイルのタイプ"
    ''' <summary>
    ''' 生成するファイルのタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private _fileType As MakeFileType
    ''' <summary>
    ''' 生成するファイルのタイプ
    ''' </summary>
    ''' <value>生成するファイルのタイプ</value>
    ''' <returns>生成するファイルのタイプ</returns>
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

#Region "改行文字"
    ''' <summary>
    ''' 改行文字
    ''' </summary>
    ''' <remarks></remarks>
    Private _paragraphWord As String = vbCrLf
    ''' <summary>
    ''' 改行文字
    ''' </summary>
    ''' <value>改行文字</value>
    ''' <returns>改行文字</returns>
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

#Region "コンストラクタ"
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="type">生成するファイルタイプ</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal type As MakeFileType)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".New", _
                                                    type)
        ' 出力タイプの指定
        _fileType = type

    End Sub
#End Region

#Region "ファイル生成"
    ''' <summary>
    ''' 引数の配列を指定したパスにファイル出力します
    ''' </summary>
    ''' <param name="list">
    ''' ファイル作成対象のリスト<br/>
    ''' 1次元のString配列のリスト</param>
    ''' <param name="path">ファイルの作成先</param>
    ''' <returns>行レコードを保持するList</returns>
    ''' <remarks></remarks>
    Public Function MakeFile(ByVal list As List(Of String()), _
                             ByVal path As String) As List(Of String)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeFile", _
                                                    list, _
                                                    path)

        Dim dbSaveList As New List(Of String)

        ' 上書きモードでストリームを生成
        Using sw As StreamWriter = New StreamWriter(path)
            Dim i As Integer
            Dim separator As String = GetSeparator()

            For Each lineData As String() In list

                For i = 0 To lineData.Length - 1

                    Dim saveLineData As New StringBuilder

                    ' １項目ずつ設定
                    saveLineData.Append(GetWithDoubleQuoteString(lineData(i)))

                    ' 最後の項目は改行文字列をセット、それ以外は区切り文字をセット
                    If i = lineData.Length - 1 Then
                        saveLineData.Append(_paragraphWord)
                    Else
                        saveLineData.Append(separator)
                    End If

                    ' ストリームに書き込む
                    sw.Write(saveLineData.ToString())

                    ' DB保存用の配列に設定
                    dbSaveList.Add(saveLineData.ToString())
                Next
            Next

            sw.Close()
        End Using

        Return dbSaveList

    End Function
#End Region

#Region "文字囲い"
    ''' <summary>
    ''' 文字列を必要に応じて編集し返却します<br/>
    ''' モードに応じてダブルクウォートで囲んで返却します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetWithDoubleQuoteString(ByVal word As String) As String

        'メソッド名、引数の情報の退避
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

#Region "区切り文字取得"
    ''' <summary>
    ''' 区切り文字を取得します
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
