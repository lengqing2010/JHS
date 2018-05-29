Imports Lixil.JHS_EKKS.BizLogic
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Partial Class MainMenu
    Inherits System.Web.UI.Page

    Public CommonConstBC As CommonConstBC
    ''' <summary>
    ''' お知らせ情報を取得し、画面表示コントロールを作成する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setInfo()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        Dim today As Date = DateAndTime.Today
        Dim JibanLogic As New Lixil.JHS_EKKS.BizLogic.OsiraseBC
        Dim dataArray As New System.Collections.Generic.List(Of Lixil.JHS_EKKS.Utilities.OsiraseRecord)
        Dim lineCount As New Integer
        Dim limitCounter As New Integer   '表示上限件数カウンタ

        'お知らせ情報を取得
        dataArray = JibanLogic.GetOsiraseRecords()

        'カウンタ初期化
        limitCounter = 0

        lineCount = dataArray.Count

        'データが取得できた場合に処理実行
        If lineCount > 0 Then
            For Each recData As Lixil.JHS_EKKS.Utilities.OsiraseRecord In dataArray
                'お知らせ表示テーブル生成

                Dim objTr1 As New HtmlTableRow
                Dim objTr2 As New HtmlTableRow
                Dim objTr3 As New HtmlTableRow

                Dim objTdNew As New HtmlTableCell
                Dim objImgNew As New HtmlImage
                Dim objTdDate As New HtmlTableCell
                Dim objTdBusyoMei As New HtmlTableCell
                Dim objTdNaiyou As New HtmlTableCell

                Dim objTdBlank1 As New HtmlTableCell
                Dim objAncLink As New HtmlAnchor

                Dim objTdBlank2 As New HtmlTableCell

                '******** 1行目 ********
                '一週間前以降の日付の場合、「NEW」アイコンを付与
                If recData.Nengappi.Date >= Date.Now.AddDays(-7) Then
                    objImgNew.Src = "images/new.gif"
                    objImgNew.Alt = "new"
                    objTdNew.Controls.Add(objImgNew)
                End If

                objTdDate.InnerHtml = "【" & recData.Nengappi.Date & "】"
                objTdBusyoMei.InnerHtml = "&nbsp;&nbsp;" & recData.NyuuryokuBusyo & "&nbsp;&nbsp;" & recData.NyuuryokuMei
                objTdBusyoMei.Style("width") = "400px"

                '******** 2行目 ********
                objTdBlank1.ColSpan = 2
                objAncLink.Target = "_blunk"
                objAncLink.HRef = Trim(recData.LinkSaki)
                objAncLink.InnerHtml = recData.HyoujiNaiyou
                objTdNaiyou.Controls.Add(objAncLink)
                objTdNaiyou.Style("width") = "400px"

                '******** 3行目 ********
                objTdBlank2.Style("height") = "15px"
                objTdBlank2.ColSpan = 3

                '各、行コントロールに格納
                objTr1.Controls.Add(objTdNew)
                objTr1.Controls.Add(objTdDate)
                objTr1.Controls.Add(objTdBusyoMei)

                objTr2.Controls.Add(objTdBlank1)
                objTr2.Controls.Add(objTdNaiyou)

                objTr3.Controls.Add(objTdBlank2)

                'tbodyに挿入
                osiraseTbody.Controls.Add(objTr1)
                osiraseTbody.Controls.Add(objTr2)
                osiraseTbody.Controls.Add(objTr3)

                'カウントアップ
                limitCounter += 1

                '上限件数チェック
                If limitCounter >= Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings("OsiraseLimitCount")) Then
                    Exit For
                End If
            Next

        End If

    End Sub
    ''' <summary>
    ''' フォーム初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        If Not IsPostBack Then

            Dim CommonCheck As New CommonCheck
            Dim LoginUserInfoList As New LoginUserInfoList
            Dim UserId As String = ""
            '権限がある場合
            If CommonCheck.CommonNinsyou(UserId, LoginUserInfoList, kegen.UserIdOnly) Then

                '営業計画管理
                If LoginUserInfoList.Items(0).EigyouKeikakuKenriSansyou = -1 Then
                    Me.LinkEigyouKeikaku.HRef = "about:blank"
                    LinkEigyouKeikaku.Attributes("onclick") = "objEBI('" & Master.EigyouButton.ClientID & "').click();return false;"
                
                End If
                '売上予実管理
                If LoginUserInfoList.Items(0).UriYojituKanriSansyou = -1 Then
                    Me.LinkUriYojitu.HRef = "about:blank"
                    LinkUriYojitu.Attributes("onclick") = "objEBI('" & Master.UriYojituButton.ClientID & "').click();return false;"
               
                End If

                '計画用_加盟店情報照会
                Me.LinkKeikakuKanriKameitenKensakuSyoukai.HRef = "about:blank"
                LinkKeikakuKanriKameitenKensakuSyoukai.Attributes("onclick") = "objEBI('" & Master.KeikakuKanriKameitenKensakuSyoukaiButton.ClientID & "').click();return false;"

            End If
            Master.loginUserInfo = LoginUserInfoList
            'お知らせ情報を取得する。
            setInfo()
        End If
    End Sub

End Class
