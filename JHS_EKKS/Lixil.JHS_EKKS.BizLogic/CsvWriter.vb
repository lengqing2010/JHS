Imports System.IO
Imports System.Text

'Any chars Separated Values stream Writer
''' <summary>TSVやCSV形式でストリームに書き込み</summary>
Public Class CsvWriter : Inherits ContextBoundObject

    ''' <summary>書き込むストリーム</summary>
    Private mStream As Stream

    ''' <summary>エンコーディング</summary>
    Private mEncoding As Encoding

    ''' <summary>区切文字</summary>
    Private mSeparator() As Byte

    ''' <summary>改行文字</summary>
    Private mLinefeed() As Byte

    ''' <summary>コンストラクタ</summary>
    ''' <param name="stream">書き込むStream</param>
    ''' <param name="encoding">エンコーディング</param>
    ''' <param name="separator">区切文字</param>
    ''' <param name="linefeed">改行文字</param>
    Public Sub New(ByVal stream As Stream, ByVal encoding As Encoding, ByVal separator As String, ByVal linefeed As String)
        mStream = stream
        mEncoding = encoding
        mSeparator = encoding.GetBytes(separator)
        mLinefeed = encoding.GetBytes(linefeed)
    End Sub

#Region "書き込み"
    ''' <summary>一行書き込む</summary>
    ''' <param name="datas">値の配列</param>
    Public Sub WriteLine(ByVal ParamArray datas() As Object)
        If (datas.Length > 0) Then
            If datas(0) Is Nothing Or IsDBNull(datas(0)) Then
                datas(0) = String.Empty
            End If
            Me.Write(datas(0).ToString)
            For i As Integer = 1 To datas.Length - 1
                Me.Write(Me.mSeparator)
                Me.Write(Convert.ToString(datas(i)).Replace(Chr(13), String.Empty))
            Next
        End If

        Me.Write(Me.mLinefeed)
    End Sub

    ''' <summary>一項目書き込む</summary>
    ''' <param name="data">データ</param>
    Public Sub Write(ByVal data As String)
        Dim buf() As Byte = mEncoding.GetBytes(data)

        Me.Write(buf)
    End Sub

    ''' <summary>一項目書き込む</summary>
    ''' <param name="data">データ</param>
    Private Sub Write(ByVal data() As Byte)
        If (data.Length = 0) Then Return
        Me.mStream.Write(data, 0, data.Length)
    End Sub
#End Region

    Protected Overrides Sub Finalize()
        mStream.Dispose()
        MyBase.Finalize()
    End Sub
End Class
