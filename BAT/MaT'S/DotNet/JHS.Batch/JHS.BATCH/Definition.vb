Option Explicit On
Option Strict On

Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Data.SqlClient
Imports System.Globalization.CultureInfo

''' <summary>環境情報に関する共通処理</summary>
Public Class Definition
#Region "定数"
    'ファイル拡張子
    Private Const CON_INI As String = ".INI"
    Private Const CON_DAT As String = ".DAT"
    Private Const CON_SQL As String = ".SQL"
#End Region

#Region "プロパティ"
    ''' <summary>
    ''' 各ファイルパースの初期設定
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
    ''' 計画年度ファイル名（S0004用）
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
    ''' 計画年度ファイル名（S0005用）
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
    ''' INIファイルパス
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
    ''' 系列コード
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
    ''' 区分(S0003用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 大連/李宇 追加</para>
    Private Shared ReadOnly Property KubunName3() As String
        Get
            Return ConfigurationManager.AppSettings("KubunName3")
        End Get
    End Property

    ''' <summary>
    ''' 区分(S0004用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 大連/李宇 追加</para>
    Private Shared ReadOnly Property KubunName4() As String
        Get
            Return ConfigurationManager.AppSettings("KubunName4")
        End Get
    End Property

    ''' <summary>
    ''' 区分(S0005用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 大連/李宇 追加</para>
    Private Shared ReadOnly Property KubunName5() As String
        Get
            Return ConfigurationManager.AppSettings("KubunName5")
        End Get
    End Property

    ''' <summary>
    ''' 商品種別(S0005用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 大連/李宇 追加</para>
    Private Shared ReadOnly Property SyouhinSyubetuName5() As String
        Get
            Return ConfigurationManager.AppSettings("SyouhinSyubetuName5")
        End Get
    End Property

#End Region

#Region "ファイルメソッド"
    ''' <summary>
    ''' READERモードでファイルオープンする。
    ''' </summary>
    ''' <param name="strFile">ファイル</param>
    Public Shared Function OpenFileReader(ByVal strFile As String) As StreamReader
        Return New StreamReader(strFile, System.Text.Encoding.GetEncoding(932))
    End Function
#End Region

#Region "外部共通ファイルの設定メソッド"
    ''' <summary>
    ''' EXEファイル名の取得
    ''' </summary>
    ''' <param name="strID">バッチID</param>
    ''' <returns>EXEファイル名</returns>
    Public Shared Function getExeName(ByVal strID As String) As String
        Return "JHS.Batch." & strID
    End Function

    ''' <summary>
    ''' CTLファイル名の取得(S0004用)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCTLFile4() As String
        Return CTLFilePath & CTLFileName4 & CON_INI
    End Function

    ''' <summary>
    ''' CTLファイル名の取得(S0005用)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCTLFile5() As String
        Return CTLFilePath & CTLFileName5 & CON_INI
    End Function

    ''' <summary>
    ''' 系列コードの取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKeiretuCd() As String
        Return KeiretuCd
    End Function

    ''' <summary>
    '''  計画年度ファイルを読込み、計画年度を取得する
    ''' </summary>
    ''' <returns>計画年度</returns>
    ''' <history>
    ''' <para>2013/01/15 大連/王新 新規作成 P-44979</para>
    ''' </history>
    Public Shared Function GetKeikakuNendo(ByVal batId As String) As DataTable
        Dim strReader As StreamReader
        Dim dtValue As DataTable
        Dim drValue As DataRow
        Dim strYear() As String
        Dim strY As String
        Dim errEx As Exception
        Dim strPath As String           'ファイルパス
        Dim i As Integer

        Try
            'ファイルパスを取得する
            If batId = "S0004" Then
                strPath = Definition.GetCTLFile4()
            Else
                strPath = Definition.GetCTLFile5()
            End If

            strReader = Definition.OpenFileReader(strPath)
        Catch ex As Exception
            ex.Data.Add("ERROR_LOG", "計画年度ＩＮＩファイルが存在しませんでした。")
            Throw ex
        End Try

        '計画年度ＣＴＬファイルが存在しない場合、エラーを戻る
        If strReader Is Nothing Then
            errEx = New Exception()
            errEx.Data.Add("ERROR_LOG", "計画年度ＩＮＩファイルが存在しませんでした。")
            Throw errEx
        End If

        dtValue = New DataTable
        dtValue.Columns.Add("Year")
        dtValue.Columns.Add("BeginMonth")
        dtValue.Columns.Add("EndMonth")

        '計画年度を取得する
        strY = ""
        While Not strReader.EndOfStream
            drValue = dtValue.NewRow()

            strY = strReader.ReadLine()
            strYear = strY.Split(CChar(","))

            '計画年度を保存する
            drValue("Year") = strYear(0)

            If strYear.Length > 1 Then
                '開始月を保存する
                drValue("BeginMonth") = strYear(1)

                '結束月を保存する
                drValue("EndMonth") = strYear(2)
            Else
                '開始月を保存する
                drValue("BeginMonth") = strYear(0) & "/04/01"

                '結束月を保存する
                drValue("EndMonth") = DateAdd(DateInterval.Year, 1, Convert.ToDateTime(strYear(0) & "/03/31")).ToString("yyyy/MM/dd")

            End If


            dtValue.Rows.Add(drValue)
        End While

        strReader.Close()
        strReader.Dispose()
        strReader = Nothing

        '計画年度が空白の場合
        If dtValue Is Nothing OrElse dtValue.Rows.Count <= 0 Then
            errEx = New Exception()
            errEx.Data.Add("ERROR_LOG", "計画年度ＩＮＩファイルからレコードが取得できませんでした。")
            Throw errEx
        End If

        If dtValue.Rows(0)("Year") Is Nothing Then
            errEx = New Exception()
            errEx.Data.Add("ERROR_LOG", "計画年度ＩＮＩファイルからレコードが取得できませんでした。")
            Throw errEx
        End If

        '計画年度ＣＴＬファイルの有効レコードがない場合、エラーを戻る
        For i = 0 To dtValue.Rows.Count - 1
            If Not IsNumeric(dtValue.Rows(i)("Year")) Then
                errEx = New Exception()
                errEx.Data.Add("ERROR_LOG", "計画年度ＩＮＩファイルからレコードが取得できませんでした。")
                Throw errEx
            ElseIf Convert.ToInt32(dtValue.Rows(i)("Year")) < 1 OrElse Convert.ToInt32(dtValue.Rows(i)("Year")) > 9999 Then
                errEx = New Exception()
                errEx.Data.Add("ERROR_LOG", "計画年度ＩＮＩファイルからレコードが取得できませんでした。")
                Throw errEx
            End If
        Next

        Return dtValue

    End Function

    ''' <summary>
    ''' 区分(S0003用)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 大連/李宇 追加</para>
    Public Shared Function GetKubunName3() As String
        Return KubunName3
    End Function

    ''' <summary>
    ''' 区分(S0004用)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 大連/李宇 追加</para>
    Public Shared Function GetKubunName4() As String
        Return KubunName4
    End Function

    ''' <summary>
    ''' 区分(S0005用)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 大連/李宇 追加</para>
    Public Shared Function GetKubunName5() As String
        Return KubunName5
    End Function

    ''' <summary>
    ''' 商品種別(S0005用)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2013/10/23 大連/李宇 追加</para>
    Public Shared Function GetSyouhinSyubetuName5() As String
        Return SyouhinSyubetuName5
    End Function

#End Region

#Region "SQL文"
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
    ''' SQLファイルパス
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
    '''  計画年度ファイルを読込み、計画年度を取得する
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
                strFileName = "「" & strFileName & "」"
            End If
            ex.Data.Add("ERROR_LOG", "ＳＱＬファイル" & strFileName & "が存在しませんでした。")
            Throw ex
        End Try

        'ＳＱＬファイルが存在しない場合、エラーを戻る
        If strReader Is Nothing Then
            errEx = New Exception()
            If Not strFileName.Equals(String.Empty) Then
                strFileName = "「" & strFileName & "」"
            End If
            errEx.Data.Add("ERROR_LOG", "ＳＱＬファイル" & strFileName & "が存在しませんでした。")
            Throw errEx
        End If

        'SQL文を取得する
        While Not strReader.EndOfStream
            sqlStr.AppendLine(strReader.ReadLine())
        End While

        strReader.Close()
        strReader.Dispose()
        strReader = Nothing

        '計画年度が空白の場合
        If sqlStr.ToString.Trim.Equals(String.Empty) Then
            errEx = New Exception()
            If Not strFileName.Equals(String.Empty) Then
                strFileName = "「" & strFileName & "」"
            End If
            errEx.Data.Add("ERROR_LOG", "ＳＱＬファイル" & strFileName & "から内容を取得できませんでした。")
            Throw errEx
        End If

        Return sqlStr.ToString
    End Function

#End Region

#Region "GetMainDbConnectionString"
    ''' <summary>DB接続文字列の取得</summary>
    Public Shared Function GetConnectionStringJHS() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringJHS").ConnectionString
    End Function

    ''' <summary>DB接続文字列の取得</summary>
    Public Shared Function GetConnectionStringEarth() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringEarth").ConnectionString
    End Function

    ''' <summary>DB接続文字列の取得</summary>
    Public Shared Function GetConnectionStringASPSFA() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringASPSFA").ConnectionString
    End Function
#End Region

End Class

