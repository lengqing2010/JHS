Imports Itis.Earth.BizLogic
Imports Itis.Framework.Report

Partial Public Class TyousaSijisyo
    Inherits System.Web.UI.Page
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "TyousaSijisyo"
    Private Const APOST As Char = ","c

    Public url1 As String = ""
    Public url2 As String = ""



    ''' <summary>
    ''' PageLoad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim paramKbn As String = Request.QueryString("Kbn")

            Dim paramBukkenNo As String = request.QueryString("HosyouSyoNo")

            ViewState("kbn") = paramKbn
            ViewState("bukkenNo") = paramBukkenNo

            Dim TyousaSijisyoLogic As New BizLogic.TyousaSijisyoLogic

            Dim dt As DataTable = TyousaSijisyoLogic.GetTyousaSijisyo(paramKbn, paramBukkenNo)

            Dim printDat As New System.Text.StringBuilder

            Dim dat As New System.Text.StringBuilder

            If dt.Rows.Count > 0 Then

                fcw = New FcwUtility(Page, "99999999", kinouId, ".fcp")

                With dt.Rows(0)
                    'dat.AppendLine("[Control Section]")
                    'dat.AppendLine("VERSION=6.3")
                    'dat.AppendLine("SEPARATOR=|")
                    'dat.AppendLine("AUTOPAGEBREAK=ON")
                    'dat.AppendLine("OPTION=FIELDATTR")
                    'dat.AppendLine(";")

                    printDat.Append(fcw.CreateDatHeader(APOST.ToString))

                    printDat.Append(fcw.CreateFormSection(""))

                    'dat.AppendLine("[Form Section]")
                    'dat.AppendLine("[Fixed Data Section]")
                    dat.AppendLine("調査会社=" & .Item("調査会社名").ToString)
                    dat.AppendLine("区分=" & .Item("区分").ToString)
                    dat.AppendLine("保証書No=" & .Item("物件番号").ToString)

                    If .Item("JIO先").ToString = "1" Then
                        dat.AppendLine("JIO先=JIO先")

                    End If

                    dat.AppendLine("加盟店名=" & .Item("加盟店名").ToString)
                    dat.AppendLine("物件名称=" & .Item("物件名称").ToString)
                    dat.AppendLine("物件住所=" & .Item("物件住所").ToString)
                    dat.AppendLine("商品=" & .Item("商品").ToString)
                    dat.AppendLine("調査方法=" & .Item("調査方法").ToString)

                    '仕様変更　2017/03/06 鄭鴻　変更↓

                    Dim Str As String = .Item("オプション調査方法").ToString()
                    Dim StrCount As Integer = 0
                    StrCount = System.Text.Encoding.Default.GetBytes(Str).Length

                    If StrCount > 136 Then
                        dat.AppendLine("オプション調査方法_11=" & .Item("オプション調査方法").ToString)
                    Else
                        dat.AppendLine("オプション調査方法_14=" & .Item("オプション調査方法").ToString)
                    End If

                    ' 仕様変更　2017/03/06 鄭鴻　変更↑

                    dat.AppendLine("依頼棟数=" & .Item("依頼棟数").ToString & " 棟")
                    'If .Item("予約状況").ToString.Trim = "1" Then
                    '    dat.AppendLine("予約状況=日程調整済み")
                    'Else
                    '    dat.AppendLine("予約状況=日程調整依頼")
                    'End If

                    dat.AppendLine("予約状況=" & .Item("予約状況").ToString.Trim)


                    '仕様変更　2017/03/06 鄭鴻　変更↓

                    'If .Item("立会い有無").ToString = "1" Then
                    '    dat.AppendLine("立会い有無=有り")
                    'ElseIf .Item("立会い有無").ToString = "0" Then
                    '    dat.AppendLine("立会い有無=未設定")
                    'Else
                    '    dat.AppendLine("立会い有無=無し")
                    'End If

                    If .Item("立会い有無").ToString = "1" Then

                        If .Item("立会者コード").ToString = "0" Then
                            dat.AppendLine("立会い有無=有")
                        ElseIf .Item("立会者コード").ToString = "1" Then
                            dat.AppendLine("立会い有無=有（施主様）")
                        ElseIf .Item("立会者コード").ToString = "2" Then
                            dat.AppendLine("立会い有無=有（担当者）")
                        ElseIf .Item("立会者コード").ToString = "3" Then
                            dat.AppendLine("立会い有無=有（施主様、担当者）")
                        ElseIf .Item("立会者コード").ToString = "4" Then
                            If .Item("立会い有無_その他備考") IsNot DBNull.Value AndAlso .Item("立会い有無_その他備考").ToString.Trim <> "" Then
                                dat.AppendLine("立会い有無=有（その他【" & .Item("立会い有無_その他備考").ToString & "】）")
                            Else
                                dat.AppendLine("立会い有無=有（その他）")
                            End If
                        ElseIf .Item("立会者コード").ToString = "5" Then
                            If .Item("立会い有無_その他備考") IsNot DBNull.Value AndAlso .Item("立会い有無_その他備考").ToString.Trim <> "" Then
                                dat.AppendLine("立会い有無=有（施主様、その他【" & .Item("立会い有無_その他備考").ToString & "】）")
                            Else
                                dat.AppendLine("立会い有無=有（施主様、その他）")
                            End If
                        ElseIf .Item("立会者コード").ToString = "6" Then
                            If .Item("立会い有無_その他備考") IsNot DBNull.Value AndAlso .Item("立会い有無_その他備考").ToString.Trim <> "" Then
                                dat.AppendLine("立会い有無=有（担当者、その他【" & .Item("立会い有無_その他備考").ToString & "】）")
                            Else
                                dat.AppendLine("立会い有無=有（担当者、その他）")
                            End If
                        ElseIf .Item("立会者コード").ToString = "7" Then
                            If .Item("立会い有無_その他備考") IsNot DBNull.Value AndAlso .Item("立会い有無_その他備考").ToString.Trim <> "" Then
                                dat.AppendLine("立会い有無=有（施主様、担当者、その他【" & .Item("立会い有無_その他備考").ToString & "】）")
                            Else
                                dat.AppendLine("立会い有無=有（施主様、担当者、その他）")
                            End If
                        End If
                    ElseIf .Item("立会い有無") Is DBNull.Value _
                            OrElse .Item("立会い有無").ToString.Trim = "" _
                            OrElse .Item("立会い有無").ToString = "0" Then

                        dat.AppendLine("立会い有無=無")
                        'Else
                        '    dat.AppendLine("立会い有無=無し")
                    End If

                    '仕様変更　2017/03/06 鄭鴻　変更↑


                    '    dat.AppendLine("立会い有無=" & .Item("立会い有無").ToString)


                    dat.AppendLine("調査日=" & Left(.Item("調査日").ToString, 10))
                    'dat.AppendLine("調査日=" & .Item("調査日").ToString)

                    If .Item("調査時刻").ToString.Length <= 10 Then
                        dat.AppendLine("調査開始時間_14=" & .Item("調査時刻").ToString)
                    Else
                        dat.AppendLine("調査開始時間_11=" & .Item("調査時刻").ToString)
                    End If


                    Dim txt As String = ""
                    If txt = "" Then txt = MakeNaiyouKoteiKoumoku(.Item("特記事項_変更履歴").ToString, 11, 68, 14)
                    If txt = "" Then txt = MakeNaiyouKoteiKoumoku(.Item("特記事項_変更履歴").ToString, 14, 87, 11)
                    If txt = "" Then txt = MakeNaiyouKoteiKoumoku(.Item("特記事項_変更履歴").ToString, 17, 105, 9)


                    dat.AppendLine("特記事項=" & .Item("特記事項").ToString)
                    dat.AppendLine(txt) '*****
                    dat.AppendLine("手配担当者=" & .Item("調査指示書作成者").ToString)

                    '仕様変更　2017/03/06 鄭鴻　変更↓

                    dat.AppendLine("JHS手配担当_所属=" & .Item("JHS手配担当_所属").ToString)

                    '仕様変更　2017/03/06 鄭鴻　変更↑

                    Dim 不足図面 As String = ""

                    If .Item("案内図").ToString = "1" Then
                        If 不足図面 <> "" Then 不足図面 &= "、"
                        不足図面 &= "案内図（区割図・測量図など）"
                    End If

                    If .Item("配置図").ToString = "1" Then
                        If 不足図面 <> "" Then 不足図面 &= "、"
                        不足図面 &= "配置図"
                    End If

                    If .Item("各階平面図").ToString = "1" Then
                        If 不足図面 <> "" Then 不足図面 &= "、"
                        不足図面 &= "各階平面図"
                    End If

                    dat.AppendLine("不足図面=" & 不足図面)

                    '[FixedDataSection] 部作成
                    printDat.Append(fcw.CreateFixedDataSection(dat.ToString))

                End With


                'DATファイル作成
                Dim errMsg As String = fcw.GetErrMsg(fcw.WriteData(printDat.ToString))

                ' 請求先元帳帳票データPDF　出力
                If Not errMsg.Equals(String.Empty) Then
                    'エラーがある場合
                    'Return errMsg

                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "alert('" & errMsg & "');window.close();", True)

                Else
                    url1 = fcw.GetUrlByFileName(paramKbn & paramBukkenNo & "-調査指示書")
                    Me.hiddenIframe1.Attributes.Item("src") = url1


                    'エラーがない場合、帳票をOPEN
                    'Dim url As String = fcw.GetUrl(paramKbn, _
                    '                               paramBukkenNo, _
                    '                               "1")
                    'Return String.Empty
                End If

            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "alert('" & Messages.Instance.MSG039E & "');window.close();", True)
            End If

        End If
        'System.IO.File.WriteAllText("\\10.160.192.25\大連情報システム部（jhs共有）\430054_Earth調査指示書(新帳票)作成機能追加\04_大連作成\test.dat", dat.ToString, System.Text.Encoding.Default)



    End Sub

    ''' <summary>
    ''' 内容固定項目作成
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="maxGyouSuu"></param>
    ''' <param name="gyouMonjiSuu"></param>
    ''' <param name="fontSize"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function MakeNaiyouKoteiKoumoku(ByVal text As String _
                                    , ByVal maxGyouSuu As Integer _
                                    , ByVal gyouMonjiSuu As Integer _
                                    , ByVal fontSize As Integer) As String

        text = text.Replace(Chr(13) & Chr(10), Chr(10))
        text = text.Replace(Chr(10) & Chr(13), Chr(10))

        Dim listKoteiKoumoku As New Generic.List(Of String)
        Dim gyouText As String = String.Empty
        Dim oneGyouCount As Integer = 0

        For i As Integer = 0 To text.Length - 1

            Dim str As String = text.Substring(i, 1)

            oneGyouCount += System.Text.Encoding.Default.GetBytes(str).Length

            If oneGyouCount > gyouMonjiSuu _
                OrElse str = Chr(10) _
                OrElse str = "●" _
                Then

                'new row
                If gyouText <> "" Then
                    listKoteiKoumoku.Add(gyouText)
                End If

                oneGyouCount = 0

                If str = Chr(10) Then
                    gyouText = String.Empty
                    oneGyouCount = 0
                Else
                    gyouText = str
                    oneGyouCount = System.Text.Encoding.Default.GetBytes(str).Length
                End If

            Else
                gyouText &= str

            End If

        Next

        If gyouText <> String.Empty Then
            listKoteiKoumoku.Add(gyouText)
        End If

        Dim rtv As New System.Text.StringBuilder

        For i As Integer = 0 To listKoteiKoumoku.Count - 1

            If fontSize = 9 Then
                If i < maxGyouSuu + 1 Then
                    rtv.AppendLine("指示_" & fontSize & "_" & i & "=" & listKoteiKoumoku.Item(i))
                End If
            Else
                rtv.AppendLine("指示_" & fontSize & "_" & i & "=" & listKoteiKoumoku.Item(i))
            End If

        Next

        If listKoteiKoumoku.Count > maxGyouSuu AndAlso fontSize <> 9 Then
            Return ""
        Else
            Return rtv.ToString
        End If

    End Function

    ''' <summary>
    ''' 指示書作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRun.Click

        Me.hiddenIframe1.Attributes.Item("src") = ""

        Dim TyouhyouPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouPath").ToString
        TyouhyouPath = TyouhyouPath & ViewState("kbn") & ViewState("bukkenNo") & "-調査指示書" & ".pdf"

        '格納サーバーAで帳票の保存PATH
        Dim TyousaSijisyoTyouhyouServerAPath As String = System.Configuration.ConfigurationManager.AppSettings("TyousaSijisyoTyouhyouServerAPath").ToString
        TyousaSijisyoTyouhyouServerAPath = TyousaSijisyoTyouhyouServerAPath & ViewState("kbn") & ViewState("bukkenNo") & "調査指示書.pdf"

        Dim idx As Integer = 0
Modoru:
        Try
            idx = idx + 1
            '格納サーバーAにCopy
            System.IO.File.Copy(TyouhyouPath, TyousaSijisyoTyouhyouServerAPath, True)
        Catch ex As Exception
            System.Threading.Thread.Sleep(2000)
            If idx < 15 Then
                GoTo Modoru
            End If
        End Try


        '帳票をOpenする
        Call Me.GetFile(TyouhyouPath)

    End Sub

    ''' <summary>
    ''' ファイル取得
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <remarks></remarks>
    Private Sub GetFile(ByVal strFileName As String)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function OpenFile(){")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').href = '" & "file:" & strFileName.Replace("\", "/") & "';")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').click();")
            .AppendLine("   setTimeout(function(){(window.open('','_self').opener=window).close();},500);")
            .AppendLine("}")
            .AppendLine("setTimeout('OpenFile()',1000);")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub
End Class