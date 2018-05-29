Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports System.Data

Partial Class SitenTukibetuKeikakuchiExcelOutput
    Inherits System.Web.UI.Page

    Protected Const XltFolder As String = "C:\jhs_ekks\"
    Protected XltFile As String = "支店_月別計画管理フォーマット.xlt"
    Protected Const CsvFolder As String = "C:\jhs_ekks\download\"
    Protected CsvDataFile As String = "SitenbetuTukiKeikakuKanri.csv"
    Protected csvData As String = ""
    Protected strErr As String = ""
    ''' <summary>
    '''　前画面データ格納
    ''' </summary>
    ''' <param name="strId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Protected Sub GetScriptValue(ByVal strId As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("   document.getElementById('" & Me.hidSelName.ClientID & "').value='" & Split(strId, ",")(0) & "';" & vbCrLf)
            .Append("   document.getElementById('" & Me.hidDisableDiv.ClientID & "').value='" & Split(strId, ",")(1) & "';" & vbCrLf)
            .Append("   form1.submit();" & vbCrLf)
            .Append("</script>" & vbCrLf)
        End With
        ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub MakeJavaScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("function funOutError(strERR)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if (strERR==''){" & vbCrLf)
            .Append("   alert('指定した条件でデータはありません。');}else{" & vbCrLf)
            .Append("   alert(strERR);" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '検索条件
        Dim strKensaku As String = Server.UrlDecode(Request.QueryString("strNo").ToString)

        Me.MakeJavaScript()

        If Not IsPostBack Then
            GetScriptValue(Request.QueryString("divID").ToString)
        Else
            Dim CommonCheck As New CommonCheck
            Dim LoginUserInfoList As New LoginUserInfoList
            '権限がある場合
            CommonCheck.CommonNinsyou("", LoginUserInfoList, kegen.UserIdOnly)

            '検索条件
            Dim objSitenTukibetuKeikakuchiSearchListBC As New SitenTukibetuKeikakuchiSearchListBC
            Dim dtData As New Data.DataTable
            Dim dtTmp As New Data.DataTable

            '支店別合計データ
            Dim dtSitenbetuGoukei As New Data.DataTable
            dtSitenbetuGoukei = objSitenTukibetuKeikakuchiSearchListBC.GetSitenbetuKeikakuKanri(Split(strKensaku, ",")(0), Split(strKensaku, ",")(1))
            '計画データ
            Dim dtKeikaku As New Data.DataTable
            dtKeikaku = objSitenTukibetuKeikakuchiSearchListBC.GetSitenbetuTukiKeikakuKanri(Split(strKensaku, ",")(0), Split(strKensaku, ",")(1))
            '去年データ
            Dim dtLastYear As New Data.DataTable
            dtLastYear = objSitenTukibetuKeikakuchiSearchListBC.GetJissekiKanri(Split(strKensaku, ",")(0), Split(strKensaku, ",")(1))

            If dtKeikaku.Rows.Count < 3 Then
                '事業ごとに計画データの作成
                dtKeikaku = MeisaiDataSakusei(dtKeikaku)
            End If

            If dtLastYear.Rows.Count < 3 Then
                '事業ごとに前年データの作成
                dtLastYear = MeisaiDataSakusei(dtLastYear)
            End If

            Dim data As New StringBuilder()
            With data
                .Append(Split(strKensaku, ",")(0))
                .Append(",")
                .Append(Split(strKensaku, ",")(1))
                .Append(",")
                .Append(Split(strKensaku, ",")(2))
                .Append(",")
                If dtSitenbetuGoukei.Rows.Count = 0 Then
                    '列数で繰り返し
                    For i As Integer = 1 To 9
                        If i = 9 Then
                            .Append("@@@")
                        Else
                            ' 最後の項目でなければ、「,」で区切る
                            .Append(",")
                        End If
                    Next

                Else

                    For i As Integer = 0 To dtSitenbetuGoukei.Rows(0).ItemArray.Length - 1

                        .Append(dtSitenbetuGoukei.Rows(0).Item(i).ToString)
                        ' 行の最後の項目かどうか
                        If i.Equals(dtSitenbetuGoukei.Rows(0).ItemArray.Length - 1) Then
                            .Append("@@@")
                        Else
                            ' 最後の項目でなければ、「,」で区切る
                            .Append(",")
                        End If

                    Next i
                End If

            End With

            With data
                For j As Integer = 0 To 2

                    For i As Integer = 1 To 36

                        .Append(dtLastYear.Rows(j).Item(i).ToString)
                        .Append(",")
                        .Append(dtKeikaku.Rows(j).Item(i).ToString)
                        ' 行の最後の項目かどうか
                        If i = 36 Then
                            ' 最後の項目ならば、改行コードの変わりに「@@@」を埋め込む
                            .Append("@@@")
                        Else
                            ' 最後の項目でなければ、「,」で区切る
                            .Append(",")
                        End If

                    Next i
                Next

            End With

            csvData = data.ToString

        End If
    End Sub

    ''' <summary>
    ''' 明細データを作成する
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Function MeisaiDataSakusei(ByVal dtTableBefore As Data.DataTable) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtTable As New Data.DataTable
        Dim drEigyouKbn As Data.DataRow()

        dtTable = dtTableBefore.Copy
        dtTable.Clear()

        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '1' ")
        '営業
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(0).Item("eigyou_kbn") = "1"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If
        '特販
        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '3' ")
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(1).Item("eigyou_kbn") = "3"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If
        'ＦＣ
        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '4' ")
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(2).Item("eigyou_kbn") = "4"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If

        Return dtTable

    End Function

End Class
