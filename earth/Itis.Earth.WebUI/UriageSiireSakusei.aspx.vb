Partial Public Class UriageSiireSakusei
    Inherits System.Web.UI.Page

#Region "変数"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Private CLogic As New CommonLogic
    Private DLogic As New DataLogic
#End Region
#Region "定数"
#Region "伝票種別定数"
    ''' <summary>
    ''' 売上-邸別（調査）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URIAGE_TYOUSA As String = "0"
    ''' <summary>
    ''' 売上-邸別（工事）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URIAGE_KOUJI As String = "1"
    ''' <summary>
    ''' 仕入-邸別（調査）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SIIRE_TYOUSA As String = "2"
    ''' <summary>
    ''' 仕入-邸別（工事）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SIIRE_KOUJI As String = "3"
    ''' <summary>
    ''' 売上-邸別（その他）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URIAGE_HOKA As String = "4"
    ''' <summary>
    ''' 売上-店別
    ''' </summary>
    ''' <remarks></remarks>
    Private Const URIAGE_TENBETU As String = "5"
#End Region
#End Region

#Region "ページ処理"
    ''' <summary>
    ''' ページ初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'onclickイベントの設定
        Me.radioUriageTyousa.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textUriageTyousa.ClientID & "',1)")
        Me.radioUriageKouji.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textUriageKouji.ClientID & "',2)")
        Me.radioUriageHoka.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textUriageHoka.ClientID & "',3)")
        Me.radioUriageTenbetu.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textUriageTenbetu.ClientID & "',4)")
        Me.radioSiireTyousa.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textSiireTyousa.ClientID & "',5)")
        Me.radioSiireKouji.Attributes.Add("onclick", "js_ChgRadioControl(this.id,'" & Me.textSiireKouji.ClientID & "',6)")
        Me.tdUriageTyousa.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioUriageTyousa.ClientID & "','" & Me.textUriageTyousa.ClientID & "',1)")
        Me.tdUriageKouji.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioUriageKouji.ClientID & "','" & Me.textUriageKouji.ClientID & "',2)")
        Me.tdUriageHoka.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioUriageHoka.ClientID & "','" & Me.textUriageHoka.ClientID & "',3)")
        Me.tdUriageTenbetu.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioUriageTenbetu.ClientID & "','" & Me.textUriageTenbetu.ClientID & "',4)")
        Me.tdSiireTyousa.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioSiireTyousa.ClientID & "','" & Me.textSiireTyousa.ClientID & "',5)")
        Me.tdSiireKouji.Attributes.Add("onclick", "js_ChgRadioControl('" & Me.radioSiireKouji.ClientID & "','" & Me.textSiireKouji.ClientID & "',6)")
        'ボタンの非活性化
        Me.buttonMakeData.Disabled = True
        Me.buttonMakeDataCall.Disabled = True
        Me.buttonReDownLoad.Disabled = True
        Me.buttonReDownLoadCall.Disabled = True
        Me.buttonMakeDataGetuGaku.Disabled = True
        Me.buttonMakeDataGetuGakuCall.Disabled = True
        Me.buttonReDownLoadGetuGaku.Disabled = True
        Me.buttonReDownLoadGetuGakuCall.Disabled = True
        Me.buttonClearDenpyouNoCall.Disabled = True
        'ボタンの非表示化
        Me.buttonRelease.Style("display") = "none"
        '伝票NOの初期化
        Me.textDenpyou.Value = ""

    End Sub

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If
        '権限制御
        If user_info.KeiriGyoumuKengen <> -1 Then
            '権限が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then
            'ボタンの設定
            Me.setBtnEvent()
        End If

    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim intResult As Integer
        Dim mLogic As New MessageLogic

        '画面項目のセッティング
        setDispAction()

        With clsLogic
            'ログインユーザー情報をクラスへセット
            .LoginUserId = user_info.LoginUserId
            .UpdDatetime = DateTime.Now
        End With

        '処理終了メッセージ表示
        If intResult <> 0 Then
            If intResult = Integer.MinValue Then
                mLogic.AlertMessage(sender, Messages.MSG019E.Replace("@PARAM1", "旧データ退避"))
            End If
        End If

    End Sub
#End Region

#Region "ボタン押下処理"
    ''' <summary>
    ''' 伝票NOのクリアボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonClearDenpyouNo_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonClearDenpyouNo.ServerClick
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim intFileType As Integer
        Dim strResult As String
        Dim tmpScript As String

        'ファイル出力タイプとファイル名を決定
        Select Case Me.hiddenSelectedRadioCID.Value
            Case Me.radioUriageTyousa.ClientID
                'CHOUSA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa
            Case Me.radioUriageKouji.ClientID
                'KOUJI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageKouji
            Case Me.radioUriageHoka.ClientID
                'SONOTA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageSonota
            Case Me.radioUriageTenbetu.ClientID
                'MISEURI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageMiseuri
            Case Me.radioSiireTyousa.ClientID
                '調査仕入明細.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa
            Case Me.radioSiireKouji.ClientID
                '工事仕入明細.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireKouji
            Case Else
                intFileType = -1
        End Select

        With clsLogic
            .AccFileType = intFileType
            If intFileType <> UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa _
                And intFileType <> UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa Then
                .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(2)) - 1
            Else
                .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(1)) - 60000 - 1
            End If
            .LoginUserId = user_info.LoginUserId
            .UpdDatetime = DateTime.Now
        End With

        strResult = clsLogic.ResetDenpyouNo(intFileType.ToString)

        'エラーメッセージを表示
        If strResult.Length > 0 Then
            tmpScript = "alert('" & strResult & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' データ作成ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonMakeData_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonMakeData.ServerClick
        Dim strInfoMsg As String
        Dim tmpScript As String
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim listCsvRec As New List(Of UriageDataCsvRecord)
        Dim strOutputString As String
        Dim httpRes As HttpResponse
        Dim strFileName As String
        Dim intFileType As Integer

        '入力チェック
        If Not CheckInput() Then
            Exit Sub
        End If

        strFileName = ""
        'ファイル出力タイプとファイル名を決定
        Select Case Me.hiddenSelectedRadioCID.Value
            Case Me.radioUriageTyousa.ClientID
                'CHOUSA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa
                strFileName = ConfigurationManager.AppSettings("UriageTyousa")
            Case Me.radioUriageKouji.ClientID
                'KOUJI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageKouji
                strFileName = ConfigurationManager.AppSettings("UriageKouji")
            Case Me.radioUriageHoka.ClientID
                'SONOTA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageSonota
                strFileName = ConfigurationManager.AppSettings("UriageSonota")
            Case Me.radioUriageTenbetu.ClientID
                'MISEURI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageMiseuri
                strFileName = ConfigurationManager.AppSettings("UriageMiseuri")
            Case Me.radioSiireTyousa.ClientID
                '調査仕入明細.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa
                strFileName = ConfigurationManager.AppSettings("SiireTyousa")
            Case Me.radioSiireKouji.ClientID
                '工事仕入明細.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireKouji
                strFileName = ConfigurationManager.AppSettings("SiireKouji")
        End Select

        With clsLogic
            '画面表示内容をクラスへセット
            .AccUriageFrom = Me.textUriFrom.Value
            .AccUriageTo = Me.textUriTo.Value
            If intFileType <> UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa _
            And intFileType <> UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa Then
                .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(2)) - 1
            Else
                .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(1)) - 1 - 60000
            End If
            .AccFileType = intFileType
            .LoginUserId = user_info.LoginUserId
            .UpdDatetime = DateTime.Now
            '売上データCSVファイル出力用の文字列を生成
            strInfoMsg = .MakeData()
            strOutputString = .OutPutString
        End With

        'エラーメッセージを表示
        If clsLogic.OutPutString = String.Empty And strInfoMsg.Length > 0 Then
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "procInfoMsg", tmpScript, True)
            Exit Sub
        End If

        If strFileName <> String.Empty And clsLogic.OutPutString <> String.Empty Then
            'ファイルの出力を行う
            httpRes = HttpContext.Current.Response
            httpRes.Clear()
            httpRes.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileName))
            httpRes.ContentType = "text/plain"
            httpRes.BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(strOutputString))
            httpRes.End()
        End If

    End Sub

    ''' <summary>
    ''' データ作成（月別割引）ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonMakeDataGetuGaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonMakeDataGetuGaku.ServerClick
        'Dim strInfoMsg As String
        'Dim tmpScript As String
        'Dim clsLogic As New UriageSiireSakuseiLogic
        'Dim strOutputString As String
        'Dim httpRes As HttpResponse
        'Dim strFileName As String

        ''入力チェック
        'If Not CheckInput() Then
        '    Exit Sub
        'End If

        ''ファイル名を設定
        'strFileName = ConfigurationManager.AppSettings("UriageWaribiki")

        'With clsLogic
        '    '画面表示内容をクラスへセット
        '    .AccUriageFrom = Me.textUriFrom.Value
        '    .AccUriageTo = Me.textUriTo.Value
        '    .AccDenpyouNo = Integer.Parse(Me.textDenpyou.Value.Substring(2)) - 1
        '    .AccFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageMiseuri
        '    .LoginUserId = user_info.LoginUserId
        '    .UpdDatetime = DateTime.Now
        '    '売上データCSVファイル出力用の文字列を生成
        '    strInfoMsg = .MakeDataWaribiki
        '    strOutputString = .OutPutString
        'End With

        ''エラーメッセージを表示
        'If strInfoMsg.Length > 0 Then
        '    tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
        '    Exit Sub
        'End If

        ''ファイルの出力を行う
        'httpRes = HttpContext.Current.Response
        'httpRes.Clear()
        'httpRes.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileName))
        'httpRes.ContentType = "text/plain"
        'httpRes.BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(strOutputString))
        'httpRes.End()
    End Sub

    ''' <summary>
    ''' 再ダウンロードボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonReDownLoad_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonReDownLoad.ServerClick
        Dim strInfoMsg As String
        Dim tmpScript As String
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim listCsvRec As New List(Of UriageDataCsvRecord)
        Dim strOutputString As String
        Dim httpRes As HttpResponse
        Dim strFileName As String
        Dim intFileType As Integer

        strFileName = ""
        'ファイル出力タイプとファイル名を設定
        Select Case Me.hiddenSelectedRadioCID.Value
            Case Me.radioUriageTyousa.ClientID
                'CHOUSA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageTyousa
                strFileName = ConfigurationManager.AppSettings("UriageTyousa")
            Case Me.radioUriageKouji.ClientID
                'KOUJI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageKouji
                strFileName = ConfigurationManager.AppSettings("UriageKouji")
            Case Me.radioUriageHoka.ClientID
                'SONOTA.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageSonota
                strFileName = ConfigurationManager.AppSettings("UriageSonota")
            Case Me.radioUriageTenbetu.ClientID
                'MISEURI.txt
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageMiseuri
                strFileName = ConfigurationManager.AppSettings("UriageMiseuri")
            Case Me.radioSiireTyousa.ClientID
                '調査仕入明細.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireTyousa
                strFileName = ConfigurationManager.AppSettings("SiireTyousa")
            Case Me.radioSiireKouji.ClientID
                '工事仕入明細.csv
                intFileType = UriageSiireSakuseiLogic.enOutputFileType.SiireKouji
                strFileName = ConfigurationManager.AppSettings("SiireKouji")
        End Select

        With clsLogic
            'ファイルタイプの設定
            .AccFileType = intFileType
            '出力文字列の取得
            strInfoMsg = .ReDownLoad()
            strOutputString = .OutPutString
        End With

        'エラーメッセージを表示
        If strInfoMsg.Length > 0 Then
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        'ファイルの出力を行う
        httpRes = HttpContext.Current.Response
        httpRes.Clear()
        httpRes.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileName))
        httpRes.ContentType = "text/plain"
        httpRes.BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(strOutputString))
        httpRes.End()

    End Sub

    ''' <summary>
    ''' 再ダウンロードボタン（月別割引）押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonReDownLoadGetuGaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonReDownLoadGetuGaku.ServerClick
        Dim strInfoMsg As String
        Dim tmpScript As String
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim listCsvRec As New List(Of UriageDataCsvRecord)
        Dim strOutputString As String
        Dim httpRes As HttpResponse
        Dim strFileName As String
        Dim intFileType As Integer

        'ファイル出力タイプとファイル名を設定
        strFileName = ConfigurationManager.AppSettings("UriageWaribiki")
        intFileType = UriageSiireSakuseiLogic.enOutputFileType.UriageWaribiki

        With clsLogic
            'ファイルタイプの設定
            .AccFileType = intFileType
            '出力文字列の取得
            strInfoMsg = .ReDownLoad()
            strOutputString = .OutPutString
        End With

        'エラーメッセージを表示
        If strInfoMsg.Length > 0 Then
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        'ファイルの出力を行う
        httpRes = HttpContext.Current.Response
        httpRes.Clear()
        httpRes.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileName))
        httpRes.ContentType = "text/plain"
        httpRes.BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(strOutputString))
        httpRes.End()

    End Sub

    ''' <summary>
    ''' 画面制御解除ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonRelease_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonRelease.ServerClick

    End Sub

    ''' <summary>
    ''' 売上/仕入/入金ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>売上/仕入/入金データT.統合会計送信フラグを更新する</remarks>
    Protected Sub ps_UpdTgkSouinFlg(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonUriage.ServerClick _
                                                                                                                , buttonSiire.ServerClick _
                                                                                                                , buttonNyuukin.ServerClick
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim intResult As Integer
        Dim tmpScript As String = String.Empty

        Dim emBtnType As EarthEnum.emExecBtnType
        Dim strMsg As String = String.Empty

        '実行元ボタンID
        Dim strTargetId As String = CType(sender, HtmlInputButton).ID

        Select Case strTargetId
            Case buttonUriage.ID
                emBtnType = EarthEnum.emExecBtnType.BtnUriage
                strMsg &= "[売上]" & EarthConst.CRLF_CODE & EarthConst.CRLF_CODE
            Case buttonSiire.ID
                emBtnType = EarthEnum.emExecBtnType.BtnSiire
                strMsg &= "[仕入]" & EarthConst.CRLF_CODE & EarthConst.CRLF_CODE
            Case buttonNyuukin.ID
                emBtnType = EarthEnum.emExecBtnType.BtnNyuukin
                strMsg &= "[入金]" & EarthConst.CRLF_CODE & EarthConst.CRLF_CODE
            Case Else
                Exit Sub
        End Select

        '入力チェック
        If Not CheckInput_Tgk() Then
            Exit Sub
        End If

        '更新処理
        intResult = clsLogic.UpdTgkSouinFlg(sender, emBtnType, user_info.LoginUserId, Me.textUriTo.Value)

        '処理結果をメッセージ表示
        If intResult < 0 Then
            strMsg &= "更新エラーです。" & EarthConst.CRLF_CODE
        ElseIf intResult >= 0 Then
            strMsg &= "[" & intResult.ToString & "]件更新しました。" & EarthConst.CRLF_CODE
        End If

        If strMsg <> String.Empty Then
            tmpScript = "alert('" & strMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ps_UpdTgkSouinFlg", tmpScript, True)
        End If
    End Sub

#End Region

#Region "画面表示制御処理"
    ''' <summary>
    ''' 画面項目の動きをセッティングします
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        Dim clsLogic As New UriageSiireSakuseiLogic
        Dim clsRec As DenpyouRecord
        Dim dtNow As DateTime
        Dim dtYesterday As DateTime
        Dim dtUriageTyousa As DateTime
        Dim dtUriageKouji As DateTime
        Dim dtUriageHoka As DateTime
        Dim dtUriageTenbetu As DateTime
        Dim dtSiireTyousa As DateTime
        Dim dtSiireKouji As DateTime
        Dim dicDenpyouInfo As New Dictionary(Of Integer, DenpyouRecord)

        'データ作成日と伝票NOの取得
        With clsLogic
            '売上データ：邸別（調査）
            clsRec = .GetLastUpdDate(URIAGE_TYOUSA)
            dicDenpyouInfo.Add(URIAGE_TYOUSA, clsRec)
            '売上データ：邸別（工事）
            clsRec = .GetLastUpdDate(URIAGE_KOUJI)
            dicDenpyouInfo.Add(URIAGE_KOUJI, clsRec)
            '売上データ：邸別（その他）
            clsRec = .GetLastUpdDate(URIAGE_HOKA)
            dicDenpyouInfo.Add(URIAGE_HOKA, clsRec)
            '売上データ：店別
            clsRec = .GetLastUpdDate(URIAGE_TENBETU)
            dicDenpyouInfo.Add(URIAGE_TENBETU, clsRec)
            '仕入データ：邸別（調査）
            clsRec = .GetLastUpdDate(SIIRE_TYOUSA)
            dicDenpyouInfo.Add(SIIRE_TYOUSA, clsRec)
            '仕入データ：邸別（工事）
            clsRec = .GetLastUpdDate(SIIRE_KOUJI)
            dicDenpyouInfo.Add(SIIRE_KOUJI, clsRec)
        End With

        'データ作成日の表示
        dtUriageTyousa = dicDenpyouInfo(URIAGE_TYOUSA).LastSakuseiDateTime
        dtUriageKouji = dicDenpyouInfo(URIAGE_KOUJI).LastSakuseiDateTime
        dtUriageHoka = dicDenpyouInfo(URIAGE_HOKA).LastSakuseiDateTime
        dtUriageTenbetu = dicDenpyouInfo(URIAGE_TENBETU).LastSakuseiDateTime
        dtSiireTyousa = dicDenpyouInfo(SIIRE_TYOUSA).LastSakuseiDateTime
        dtSiireKouji = dicDenpyouInfo(SIIRE_KOUJI).LastSakuseiDateTime

        Me.textUriageTyousa.Value = DLogic.dtTime2Str(dtUriageTyousa, EarthConst.FORMAT_DATE_TIME_7)
        Me.textUriageKouji.Value = DLogic.dtTime2Str(dtUriageKouji, EarthConst.FORMAT_DATE_TIME_7)
        Me.textUriageHoka.Value = DLogic.dtTime2Str(dtUriageHoka, EarthConst.FORMAT_DATE_TIME_7)
        Me.textUriageTenbetu.Value = DLogic.dtTime2Str(dtUriageTenbetu, EarthConst.FORMAT_DATE_TIME_7)
        Me.textSiireTyousa.Value = DLogic.dtTime2Str(dtSiireTyousa, EarthConst.FORMAT_DATE_TIME_7)
        Me.textSiireKouji.Value = DLogic.dtTime2Str(dtSiireKouji, EarthConst.FORMAT_DATE_TIME_7)

        '伝票NOの退避
        Me.hiddenDenpyouNoUriageTyousa.Value = (dicDenpyouInfo(URIAGE_TYOUSA).DenpyouNo + 60001).ToString.PadLeft(6, "0"c)
        Me.hiddenDenpyouNoUriageKouji.Value = "01" & (dicDenpyouInfo(URIAGE_KOUJI).DenpyouNo + 1).ToString.PadLeft(4, "0"c)
        Me.hiddenDenpyouNoUriageHoka.Value = "02" & (dicDenpyouInfo(URIAGE_HOKA).DenpyouNo + 1).ToString.PadLeft(4, "0"c)
        Me.hiddenDenpyouNoUriageTenbetu.Value = "03" & ((dicDenpyouInfo(URIAGE_TENBETU).DenpyouNo) + 1).ToString.PadLeft(4, "0"c)
        Me.hiddenDenpyouNoSiireTyousa.Value = (dicDenpyouInfo(SIIRE_TYOUSA).DenpyouNo + 60001).ToString.PadLeft(6, "0"c)
        Me.hiddenDenpyouNoSiireKouji.Value = "01" & (dicDenpyouInfo(SIIRE_KOUJI).DenpyouNo + 1).ToString.PadLeft(4, "0"c)

        'データ作成日の赤色判定
        dtNow = DateTime.Now
        dtYesterday = DateTime.Now.AddHours(-24)
        If dtUriageTyousa >= dtYesterday And dtUriageTyousa <= dtNow Then
            Me.textUriageTyousa.Style.Item("color") = "red"
        End If
        If dtUriageKouji >= dtYesterday And dtUriageKouji <= dtNow Then
            Me.textUriageKouji.Style.Item("color") = "red"
        End If
        If dtUriageHoka >= dtYesterday And dtUriageHoka <= dtNow Then
            Me.textUriageHoka.Style.Item("color") = "red"
        End If
        If dtUriageTenbetu > dtYesterday And dtUriageTenbetu <= dtNow Then
            Me.textUriageTenbetu.Style.Item("color") = "red"
        End If
        If dtSiireTyousa >= dtYesterday And dtSiireTyousa <= dtNow Then
            Me.textSiireTyousa.Style.Item("color") = "red"
        End If
        If dtSiireKouji >= dtYesterday And dtSiireKouji <= dtNow Then
            Me.textSiireKouji.Style.Item("color") = "red"
        End If

        '月次処理警告メッセージの設定
        Me.SpanGetujiMessage.InnerText = ""
        If dtUriageKouji.Month = DateTime.Now.AddMonths(-1).Month Then
            Me.SpanGetujiMessage.InnerText = Messages.MSG072W
        End If

        '決算月処理警告メッセージの設定
        Me.SpanKessanMessage.InnerText = ""
        If DateTime.Now.Month = 3 Or DateTime.Now.Month = 9 Then
            Me.SpanKessanMessage.InnerText = Messages.MSG073W
        End If
        If DateTime.Now.Month = 4 Or DateTime.Now.Month = 10 Then
            If _
                    (dtUriageTyousa.Month = 3 OrElse dtUriageTyousa.Month = 9) _
            AndAlso (dtUriageKouji.Month = 3 OrElse dtUriageKouji.Month = 9) _
            AndAlso (dtUriageHoka.Month = 3 OrElse dtUriageHoka.Month = 9) _
            AndAlso (dtUriageTenbetu.Month = 3 OrElse dtUriageTenbetu.Month = 9) _
            AndAlso (dtSiireTyousa.Month = 3 OrElse dtSiireTyousa.Month = 9) _
            AndAlso (dtSiireKouji.Month = 3 OrElse dtSiireKouji.Month = 9) Then
                Me.SpanKessanMessage.InnerText = Messages.MSG073W
            End If
        End If

    End Sub

    ''' <summary>
    ''' 実行ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新の確認を行なう。<br/>
    ''' OK時：DB更新を行なう。<br/>
    ''' キャンセル時：DB更新を中断する。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()
        'イベントハンドラ登録
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        Me.buttonUriage.Attributes("onclick") = tmpTouroku
        Me.buttonSiire.Attributes("onclick") = tmpTouroku
        Me.buttonNyuukin.Attributes("onclick") = tmpTouroku
    End Sub

    ''' <summary>
    ''' 入力項目チェック(統合会計連動対応[送信])
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInput_Tgk() As Boolean
        'エラーメッセージ初期化
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = ""

        '必須チェック
        If Me.textUriTo.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "売上年月日TO")
            arrFocusTargetCtrl.Add(Me.textUriTo)
        End If
        '日付の大小チェック
        If Me.textUriFrom.Value <> "" And Me.textUriTo.Value <> "" Then
            If DateTime.Parse(Me.textUriFrom.Value) > DateTime.Parse(Me.textUriTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(Me.textUriFrom)
            End If
        End If
        '日付の不正値チェック
        If Me.textUriTo.Value <> "" Then
            If DateTime.Parse(Me.textUriTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.textUriTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "売上年月日TO")
                arrFocusTargetCtrl.Add(Me.textUriTo)
            End If
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        'エラー無しの場合、Trueを返す
        Return True
    End Function

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean
        'エラーメッセージ初期化
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = ""

        '必須チェック
        If Me.textUriFrom.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "売上年月日FROM")
            arrFocusTargetCtrl.Add(Me.textUriFrom)
        End If
        'TO項目未入力時入力補完
        If Me.textUriTo.Value = "" Then
            Me.textUriTo.Value = Me.textUriFrom.Value
        End If
        '日付の大小チェック
        If Me.textUriFrom.Value <> "" And Me.textUriTo.Value <> "" Then
            If DateTime.Parse(Me.textUriFrom.Value) > DateTime.Parse(Me.textUriTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(Me.textUriFrom)
            End If
        End If
        '日付の不正値チェック
        If Me.textUriFrom.Value <> "" Then
            If DateTime.Parse(Me.textUriFrom.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.textUriFrom.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "売上年月日FROM")
                arrFocusTargetCtrl.Add(Me.textUriFrom)
            End If
        End If
        If Me.textUriTo.Value <> "" Then
            If DateTime.Parse(Me.textUriTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.textUriTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "売上年月日TO")
                arrFocusTargetCtrl.Add(Me.textUriTo)
            End If
        End If
        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        'エラー無しの場合、Trueを返す
        Return True
    End Function
#End Region

End Class