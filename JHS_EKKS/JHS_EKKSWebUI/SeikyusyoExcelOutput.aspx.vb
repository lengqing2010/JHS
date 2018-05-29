Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data

Partial Class SeikyusyoExcelOutput
    Inherits System.Web.UI.Page

    Protected XltFile As String = ""
    Protected Const CsvFolder As String = "E:\JHS_EKKS\JHS_EKKS2\JHS_EKKSWebUI\data\download\"
    Protected CsvDataFile As String = ""
    Protected csvData As String = ""
    Protected strErr As String = ""
    ''' <summary>
    '''　前画面データ格納
    ''' </summary>
    ''' <param name="strId"></param>
    ''' <remarks></remarks>
    ''' <history>2010/09/19 付龍(大連情報システム部)　新規作成</history>
    Protected Sub GetScriptValue(ByVal strId As String)

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

    Protected Sub MakeJavaScript()

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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strKensaku As String = Request.QueryString("strNo").ToString
        Dim objCommonBC As New CommonBC
        Me.MakeJavaScript()
        Dim strSysTuki As DateTime = Convert.ToDateTime(objCommonBC.SelSystemDate.Rows(0).Item(0).ToString)
        strSysTuki.ToString("yyyy_MM_dd_HH_mm_ss")
        CsvDataFile = strSysTuki.ToString("yyyy_MM_dd_HH_mm_ss") & "計画管理.csv"
        XltFile = "計画管理フォーマット.xlt"
        If Not IsPostBack Then
            GetScriptValue(Request.QueryString("divID").ToString)
        Else
            Dim CommonCheck As New CommonCheck
            Dim LoginUserInfoList As New LoginUserInfoList
            '権限がある場合
            CommonCheck.CommonNinsyou("", LoginUserInfoList, kegen.UserIdOnly)


            '検索条件
            Dim KeikakuKanriSearchListBC As New KeikakuKanriSearchListBC
            Dim dtData As New Data.DataTable
            Dim dtTmp As New Data.DataTable
            Dim KeikakuKanriRecord As KeikakuKanriRecord = SetKensaku(strKensaku)
            dtTmp = KeikakuKanriSearchListBC.GetSitenbetuTukiData(KeikakuKanriRecord)
            Dim data As New StringBuilder()
            With data
                For Each dr As DataRow In dtTmp.Rows
                    For i As Integer = 0 To dr.ItemArray.Length - 1

                        .Append(dr.Item(i).ToString)
                        ' 行の最後の項目かどうか
                        If i.Equals(dr.ItemArray.Length - 1) Then
                            .Append("@@@")
                        Else
                            ' 最後の項目でなければ、「,」で区切る
                            .Append(",")
                        End If

                    Next i
                Next
                .Append(KeikakuKanriRecord.Nendo)
                .Append(",")
                .Append(KeikakuKanriRecord.Shiten)
                .Append(",")
                .Append(KeikakuKanriRecord.ShitenMei)
                .Append(",")
                .Append(LoginUserInfoList.Items(0).Simei)
                .Append(",HEAD_END@@@")
            End With

            dtData = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.meisai, True)
            If dtData.Rows.Count > 0 Then
                dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.syoukei, True)
                DataTableAdd(dtData, dtTmp)

                If KeikakuKanriRecord.EigyouKBN.FC Then
                    dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.FC, True)
                    DataTableAdd(dtData, dtTmp)


                End If
                dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.goukeiFC, True)
                DataTableAdd(dtData, dtTmp)
                dtTmp = KeikakuKanriSearchListBC.GetKeikakuKanriData(KeikakuKanriRecord, KeikakuKanriRecord.selectKbn.goukei, True)
                DataTableAdd(dtData, dtTmp)
                Response.Buffer = True
                Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)
                With data
                    For Each dr As DataRow In dtData.Rows

                        For i As Integer = 0 To dr.ItemArray.Length - 1
                            If (i <= KeikakuKanriRecord.selectIndex.zennen_heikin_tanka) OrElse _
                            (i >= KeikakuKanriRecord.selectIndex.gatu_keikaku_kensuu4 AndAlso i <= KeikakuKanriRecord.selectIndex.gatu_keisanyou_tyoku_koj_ritu3) OrElse _
                            i = KeikakuKanriRecord.selectIndex.data_type OrElse i = KeikakuKanriRecord.selectIndex.syouhin_cd OrElse i = KeikakuKanriRecord.selectIndex.kensuu_count_umu OrElse i = KeikakuKanriRecord.selectIndex.zennen_siire_heikin_tanka Then
                                '.Append(dr.Item(i).ToString)
                                writer.Write(dr.Item(i).ToString)
                            Else
                                .Append("")
                            End If

                            ' 行の最後の項目かどうか
                            If i.Equals(dr.ItemArray.Length - 1) Then
                                ' 最後の項目ならば、改行コードの変わりに「@@@」を埋め込む
                                .Append(vbCrLf)
                            Else
                                ' 最後の項目でなければ、「,」で区切る
                                .Append(",")
                            End If

                        Next i
                    Next

                End With
                'ClientScript.RegisterStartupScript(Me.GetType(), "CLOSE", "funBtnEnable('" & CsvDataFile & "');", True)
                Response.Buffer = True
                'CSVファイルダウンロード
                Response.Charset = "utf-8"
                Response.ContentType = "text/plain"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(CsvDataFile))
                Response.End()
                'Dim objFso As New Scripting.FileSystemObject
                'Dim objTxtD As Scripting.TextStream = objFso.CreateTextFile(CsvFolder & CsvDataFile, True, False)



                'Dim strRepAfterD As String = Replace(data.ToString, "@@@", vbNewLine)    ' 置換後の文字列 
                '' 「@@@」を改行コードに置き換える 


                '' テキストファイルに書き出し 

                'objTxtD.WriteLine(strRepAfterD)

                'objTxtD.Close()
                'objFso = Nothing

            Else

            End If


        End If
    End Sub
    Sub DataTableAdd(ByRef dtMain As DataTable, ByVal dtAdd As DataTable)
        Dim selectIndex As New KeikakuKanriRecord.selectIndex
        For i As Integer = 0 To dtAdd.Rows.Count - 1
            Dim dtRow As DataRow = dtMain.NewRow
            For j As Integer = 0 To selectIndex.max
                dtRow.Item(j) = dtAdd.Rows(i).Item(j)
            Next
            dtMain.Rows.Add(dtRow)
        Next
    End Sub
    Private Function SetKensaku(ByVal strKensaku As String) As KeikakuKanriRecord
        Dim KeikakuKanriRecord As New KeikakuKanriRecord

        Dim TaisyouKikan As New KeikakuKanriRecord.taisyouKikan
        '営業区分
        Dim EigyouKBNjyouken As New EigyouJyouken
        '絞込み選択
        Dim SiborikomiJyouken As New SiborikomiJyouken
        '表示欄選択
        Dim HyoujiSentakuJyouken As New HyoujiSentakuJyouken
        Dim arrKensaku() As String = Split(strKensaku, ",")
        Dim SitenSearchBC As New SitenSearchBC

        With KeikakuKanriRecord
            .Nendo = arrKensaku(0)
            .Shiten = arrKensaku(1)
            .User = arrKensaku(2)
            .Kameiten = arrKensaku(3)
            '=============
            'BDからを取得。
            If .Shiten = "" Then
                .ShitenMei = ""
            Else
                .ShitenMei = SitenSearchBC.GetBusyoKanri("0", "", False, False, .Shiten).Rows(0).Item(0).ToString
            End If

            '===========
            .Eigyou = arrKensaku(5)
            '表示対象期間
            .Taisyou = CInt(arrKensaku(6))
            
            '営業区分
            If arrKensaku(7) Then
                EigyouKBNjyouken.Eigyou = True
            End If
            If arrKensaku(8) Then
                EigyouKBNjyouken.Tokuhan = True
            End If
            If arrKensaku(9) Then
                EigyouKBNjyouken.Sinki = True
            End If
            If arrKensaku(10) Then
                EigyouKBNjyouken.FC = True
            End If
            .EigyouKBN = EigyouKBNjyouken
            '絞込み選択
            If arrKensaku(11) Then
                SiborikomiJyouken.KeikakuTi = True
            End If
            If arrKensaku(12) Then
                SiborikomiJyouken.SinkiTouroku = True
            End If
            If arrKensaku(13) Then
                SiborikomiJyouken.Bunjyou = True
            End If
            If arrKensaku(14) Then
                SiborikomiJyouken.Tyumon = True
            End If
            If arrKensaku(15) Then
                SiborikomiJyouken.NenkanTouSuu = True
            End If
            .Siborikomi = SiborikomiJyouken
            '表示欄選択
            If arrKensaku(16) Then
                HyoujiSentakuJyouken.Keikaku = True
            End If
            If arrKensaku(17) Then
                HyoujiSentakuJyouken.Mikomi = True
            End If
            If arrKensaku(18) Then
                HyoujiSentakuJyouken.Jisseki = True
            End If
            If arrKensaku(19) Then
                HyoujiSentakuJyouken.Tassei = True
            End If
            If arrKensaku(20) Then
                HyoujiSentakuJyouken.Sintyoku = True
            End If
            .HyoujiSentaku = HyoujiSentakuJyouken
            .Kensuu = arrKensaku(21)
        End With

        Return KeikakuKanriRecord
    End Function
End Class
