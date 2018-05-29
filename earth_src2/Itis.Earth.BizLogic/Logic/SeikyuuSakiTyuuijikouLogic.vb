Imports System.Transactions
Imports Itis.Earth.DataAccess

''' <summary>
''' 請求先注意事項
''' </summary>
''' <history>
''' <para>2011/06/13　車龍(大連情報システム部)　新規作成</para>
''' </history>
Public Class SeikyuuSakiTyuuijikouLogic

    Private seikyuuSakiTyuuijikouDA As New SeikyuuSakiTyuuijikouDataAccess

    ''' <summary>
    ''' 請求先名を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSeikyuusakiMei(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String) As Data.DataTable

        Return seikyuuSakiTyuuijikouDA.SelSeikyuusakiMei(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc)
    End Function

    ''' <summary>
    ''' 種別情報を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSyubetu() As Data.DataTable

        Return seikyuuSakiTyuuijikouDA.SelSyubetu()
    End Function

    ''' <summary>
    ''' 請求先注意事項情報を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSeikyuuSakiTyuuijikouInfo(ByVal Param As Dictionary(Of String, String)) As SeikyuuSakiTyuuijikouDataSet.SeikyuuSakiTyuuijikouInfoTableDataTable

        Return seikyuuSakiTyuuijikouDA.SelSeikyuuSakiTyuuijikouInfo(Param)
    End Function

    ''' <summary>
    ''' 請求先注意事項件数を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSeikyuuSakiTyuuijikouCount(ByVal Param As Dictionary(Of String, String)) As Integer

        Return CInt(seikyuuSakiTyuuijikouDA.SelSeikyuuSakiTyuuijikouCount(Param).Rows(0).Item(0))
    End Function

    ''' <summary>
    ''' 請求先注意事項CSVを取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSeikyuuSakiTyuuijikouCSV(ByVal Param As Dictionary(Of String, String)) As Data.DataTable

        Return seikyuuSakiTyuuijikouDA.SelSeikyuuSakiTyuuijikouCSV(Param)
    End Function

    ''' <summary>
    ''' 請求先名を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSeikyuusakiInfo(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String) As Data.DataTable

        Return seikyuuSakiTyuuijikouDA.SelSeikyuusakiInfo(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc)
    End Function

    ''' <summary>
    ''' 請求先注意事項を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSeikyuusakiTyuuijikou(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String) As Data.DataTable

        Return seikyuuSakiTyuuijikouDA.SelSeikyuusakiTyuuijikou(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo)
    End Function

    ''' <summary>
    ''' 請求先の存在チェック
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSeikyuusakiCheck(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String) As Boolean

        '請求先を取得する
        Dim dtSeikyuusakiCheck As New Data.DataTable
        dtSeikyuusakiCheck = seikyuuSakiTyuuijikouDA.SelSeikyuusakiCheck(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc)

        If dtSeikyuusakiCheck.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 請求先注意事項の存在チェック
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSeikyuusakiTyuuijikouCheck(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String) As Data.DataTable

        Return seikyuuSakiTyuuijikouDA.SelSeikyuusakiTyuuijikouCheck(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo)
    End Function

    ''' <summary>
    ''' DBの時間を取得する
    ''' </summary>
    ''' <history>
    ''' <para>2011/06/14　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetDbTime() As Data.DataTable

        Return seikyuuSakiTyuuijikouDA.SelDbTime()
    End Function

    ''' <summary>請求先注意事項を登録する</summary>
    Public Function InsSeikyuusakiTyuuijikou(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String, ByVal strTorikesi As String, ByVal strSyubetuCd As String, ByVal strSyousai As String, ByVal strJyuuyoudo As String, ByVal strUserId As String) As Boolean

        If strNo.Trim.Equals(String.Empty) Then
            '請求先注意事項の最大入力Noを取得する
            Dim maxNyuuryokuNo As Integer = CInt(seikyuuSakiTyuuijikouDA.SelMaxNyuuryokuNo(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc).Rows(0).Item(0))
            strNo = CStr(maxNyuuryokuNo + 1)
        End If

        Return seikyuuSakiTyuuijikouDA.InsSeikyuusakiTyuuijikou(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo, strTorikesi, strSyubetuCd, strSyousai, strJyuuyoudo, strUserId)
    End Function

    ''' <summary>請求先注意事項を更新する</summary>
    Public Function UpdSeikyuusakiTyuuijikou(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String, ByVal strTorikesi As String, ByVal strSyubetuCd As String, ByVal strSyousai As String, ByVal strJyuuyoudo As String, ByVal strUserId As String) As Boolean

        Return seikyuuSakiTyuuijikouDA.UpdSeikyuusakiTyuuijikou(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo, strTorikesi, strSyubetuCd, strSyousai, strJyuuyoudo, strUserId)
    End Function




End Class
