
''' <summary>
''' 共通処理クラス(静的インスタンス)
''' </summary>
''' <remarks>このクラスにはアプリケーションで共有する処理以外実装しない事</remarks>
Public Class CommonLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '静的変数としてクラス型のインスタンスを生成
    Private Shared _instance = New CommonLogic()

    '静的関数としてクラス型のインスタンスを返す関数を用意
    Public Shared ReadOnly Property Instance() As CommonLogic
        Get
            '静的変数が解放されていた場合のみ、インスタンスを生成する
            If IsDBNull(_instance) Then
                _instance = New CommonLogic()
            End If
            Return _instance
        End Get
    End Property

    Private Const strOpenLinkScript As String = "window.open('about:Blank','_blank').location.href='{0}'"

    ''' <summary>
    ''' 画面表示用にオブジェクトを文字列変換する
    ''' </summary>
    ''' <param name="obj">表示対象のデータ</param>
    ''' <param name="str">未設定時の初期値</param>
    ''' <returns>表示形式の文字列</returns>
    ''' <remarks>
    ''' Decimal  : Minvalueを空白、それ以外は入力値をString型で返却<br/>
    ''' Integer  : Minvalueを空白、それ以外は入力値をString型で返却<br/>
    ''' DateTime : MinDateTimeを空白、それ以外は入力値を YYYY/MM/DD 形式のString型で返却<br/>
    ''' 上記以外 : そのまま。適宜追加してください
    ''' </remarks>
    Public Function GetDisplayString(ByVal obj As Object, _
                                     Optional ByVal str As String = "") As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDisplayString", _
                                                    obj, _
                                                    str)

        Dim cbLogic As New CommonBizLogic
        Return cbLogic.GetDisplayString(obj, str)
    End Function

    ''' <summary>
    ''' 画面表示用にオブジェクトを文字列変換する（日時）
    ''' </summary>
    ''' <param name="obj">表示対象のデータ（日時）</param>
    ''' <param name="str">未設定時の初期値</param>
    ''' <returns>表示形式の文字列</returns>
    ''' <remarks>
    ''' DateTime : MinDateTimeを空白、それ以外は入力値を yyyy/MM/dd HH:mm:ss形式のString型で返却<br/>
    ''' </remarks>
    Public Function GetDispStrDateTime(ByVal obj As Object, _
                                     Optional ByVal str As String = "") As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDispStrDateTime", _
                                                    obj, _
                                                    str)

        Dim cbLogic As New CommonBizLogic
        Return cbLogic.GetDispStrDateTime(obj, str)
    End Function

    ''' <summary>
    ''' 画面表示用文字列を指定した型に変換する
    ''' </summary>
    ''' <param name="strData">変換対象のデータ</param>
    ''' <param name="objChangeData">変換後の型データ（参照）</param>
    ''' <returns>変換後の型データ</returns>
    ''' <remarks>
    ''' Decimal  : 空白をMinvalue、それ以外は入力値をDecimalに変換<br/>
    ''' Integer  : 空白をMinvalue、それ以外は入力値をIntegerに変換<br/>
    ''' DateTime : 空白をMinvalue、それ以外は入力値をDateTimeに変換<br/>
    ''' 上記以外 : そのまま。適宜追加してください<br/>
    ''' 変換に失敗した場合はFalseを返し、指定型のMinValueをセットします
    ''' </remarks>
    Public Function SetDisplayString(ByVal strData As String, _
                                     ByRef objChangeData As Object) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDisplayString", _
                                                    strData, _
                                                    objChangeData)

        Dim cbLogic As New CommonBizLogic
        Return cbLogic.SetDisplayString(strData, objChangeData)
    End Function

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（文字列系）
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <param name="strReturn">戻したい値をセット</param>
    ''' <returns>[String]第一引数の文字列 or 第二引数の値</returns>
    ''' <remarks>
    ''' 第一引数の値が取得できない場合、""(空白)を戻す<br/>
    ''' 第二引数で戻したい値を指定可能<br/>
    ''' </remarks>
    Public Function GetDispStr(ByVal objArg As Object, Optional ByVal strReturn As String = "") As String
        Dim strTmp As String = Me.GetDisplayString(objArg)
        If strTmp.Length = 0 Then
            strTmp = ""
        Else
            If strReturn <> "" Then
                strTmp = strReturn
            End If
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（数値系）
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <param name="strReturn">戻したい値をセット</param>
    ''' <returns>[String]第一引数の文字列 or 第二引数の値</returns>
    ''' <remarks>
    ''' 第一引数の値が取得できない場合、""(空白)を戻す<br/>
    ''' 第二引数で戻したい値を指定可能<br/>
    ''' </remarks>
    Public Function GetDispNum(ByVal objArg As Object, Optional ByVal strReturn As String = "0") As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDispNum", _
                                                    objArg, _
                                                    strReturn)

        Dim cbLogic As New CommonBizLogic
        Return cbLogic.GetDispNum(objArg, strReturn)
    End Function

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（有無表示）
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <param name="blnShortType">有or無</param>
    ''' <param name="blnDispCode">コード値＋"：" 表示有無</param>
    ''' <returns>[String]有り、無し あるいは有、無</returns>
    ''' <remarks>
    ''' ・画面表示項目内の「有り」「無し」を表示する。<br/>
    ''' blnShortTypeがTrueの場合、「有」「無」で表示<br/>
    ''' blnShortTypeがFalseの場合、「有り」「無し」で表示<br/>
    ''' blnDispCodeがTrueの場合、「コード値」＋「：」で表示<br/>
    ''' </remarks>
    Public Function GetDispUmuStr(ByVal objArg As Object, _
                                    Optional ByVal blnShortType As Boolean = False, _
                                    Optional ByVal blnDispCode As Boolean = False _
                                ) As String

        Dim objTmp As Object = Me.GetDispStr(objArg)
        Dim strTmp As String = ""

        If objTmp.ToString = String.Empty Then
            Return strTmp
        End If

        If objTmp.ToString = "0" Then
            strTmp = EarthConst.NASI
        ElseIf objTmp.ToString = "1" Then
            strTmp = EarthConst.ARI
        End If

        If blnShortType <> False Then
            strTmp = Left(strTmp, 1)
        End If

        If blnDispCode <> False Then
            strTmp = objTmp.ToString & ":" & strTmp
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（売上処理済表示）
    ''' </summary>
    ''' <param name="objArg">「売上計上日」を指定</param>
    ''' <returns>[String]売上処理済 or ""</returns>
    ''' <remarks>
    ''' 「売上計上日」の入力がある場合、「売上処理済」を表示<br/>
    ''' 「売上計上日」の入力がない場合、「""(空白)」を表示<br/>
    ''' </remarks>
    Public Function GetDispUriageSyoriZumiStr(ByVal objArg As Object) As String

        Dim objTmp As Object = Me.GetDispStr(objArg)
        Dim strTmp As String = ""

        If objTmp.ToString.Length = 0 Then
            strTmp = ""
        Else
            strTmp = EarthConst.URIAGE_ZUMI
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' 画面表示用文字列からフラグに変換するファンクション(売上処理済表示)
    ''' </summary>
    ''' <param name="strZumiTmp">「売上処理済」文字列指定</param>
    ''' <returns>[String]"1" or ""</returns>
    ''' <remarks></remarks>
    Public Function GetUriageSyoriZumiFlg(ByVal strZumiTmp As String) As String
        Dim strTmp As String = ""

        If strZumiTmp = EarthConst.URIAGE_ZUMI Then
            strTmp = EarthConst.URIAGE_ZUMI_CODE
        Else
            strTmp = String.Empty
        End If

        Return strTmp
    End Function

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（発注書確定表示）
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <returns>[String]1:確定 or 0:未確定</returns>
    ''' <remarks>
    ''' 発注書確定FLGが1の場合、「確定」を表示<br/>
    ''' 発注書確定FLGが0の場合、「未確定」を表示<br/>
    ''' </remarks>
    Public Function GetDispHattyuusyoKakuteiStr(ByVal objArg As Object) As String

        Dim objTmp As Object = Me.GetDispStr(objArg)
        Dim strTmp As String = ""

        If objTmp.ToString = "0" Then
            strTmp = EarthConst.MIKAKUTEI
        ElseIf objTmp.ToString = "1" Then
            strTmp = EarthConst.KAKUTEI
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（加盟店・NG情報メッセージ表示）
    ''' </summary>
    ''' <param name="objArg">加盟店マスタ.可否区分</param>
    ''' <returns>[String]調査会社NG or ""</returns>
    ''' <remarks>
    ''' 加盟店マスタ.可否区分=9の場合、「調査会社NG」を表示<br/>
    ''' 加盟店マスタ.可否区分=9以外の場合、「""(空白)」を表示<br/>
    ''' </remarks>
    Public Function GetDispKahiStr(ByVal objArg As Object) As String

        Dim objTmp As Object = Me.GetDispStr(objArg)
        Dim strTmp As String = ""

        If objTmp.ToString = "9" Then
            strTmp = EarthConst.TYOUSA_KAISYA_NG
        Else
            strTmp = ""
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' 文字列をInteger型に変換するファンクション（全般）
    ''' </summary>
    ''' <param name="strArg"></param>
    ''' <returns>[Integer]第一引数値 or Integer.MinValue</returns>
    ''' <remarks>
    ''' <br/>
    ''' </remarks>
    Public Function ChgStrToInt(ByVal strArg As String) As Integer
        Dim intTmp As Integer = 0
        If strArg Is Nothing Then
            Return intTmp
        End If

        Dim strTmp As String = strArg.Trim
        strTmp = strTmp.Replace(",", "")
        strTmp = strTmp.Replace("/", "")

        If strTmp.Length = 0 Then
            intTmp = Integer.MinValue
        Else
            intTmp = Integer.Parse(strTmp)
        End If
        Return intTmp
    End Function

    Public Function getTyosaMitumoriFilePath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String
        Dim strReturn As String = String.Empty
        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strReturn = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                'Web.configより保証書DBのファイルパス構成を取得
                strPath = ConfigurationManager.AppSettings("HosyousyoDbFilePath")
                strReturn = strPath
            Else
                strReturn = String.Empty
            End If
        End If

        Return strReturn
    End Function

    ''' <summary>
    ''' 調査見積書ファイルリンクパス生成
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <param name="no">番号</param>
    ''' <returns>調査見積書ファイルパス</returns>
    ''' <remarks></remarks>
    Public Function getTyousaMitsumorisyoFilePath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strReturn As String = String.Empty
        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strReturn = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                'Web.configより調査見積書のファイルパス構成を取得
                strPath = ConfigurationManager.AppSettings("TyosaMitumoriFilePath")

                'パスに置換
                strPath = strPath.Replace("@KUBUN@", kubun)
                strPath = strPath.Replace("@NO@", no)
                strPath = strPath.Replace("@TYOUSAMITSUMORISYO.PDF@", EarthConst.PDF_TYOUSAMITSUMORISYO)
                strReturn = strPath
            Else
                strReturn = String.Empty
            End If
        End If

        'ボタンを渡されている場合、onclick設定
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strReturn = String.Empty, String.Empty, String.Format(strOpenLinkScript, strReturn))
        End If

        Return strReturn
    End Function

    ''' <summary>
    ''' 保証書DBファイルリンクパス生成
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <param name="no">番号</param>
    ''' <returns>保証書DBファイルパス</returns>
    ''' <remarks></remarks>
    Public Function getHosyousyoDbFilePath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strReturn As String = String.Empty
        Dim strPath As String = String.Empty
        Dim strConfig As String = String.Empty
        Dim cbLogic As New CommonBizLogic

        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strReturn = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                '番号を分解
                Dim server As String = String.Empty
                Dim year As String = no.Substring(0, 4)
                Dim month As String = no.Substring(4, 2)
                Dim ym As String = no.Substring(0, 6)
                Dim num1 As String = no.Substring(6, 2)
                Dim num2 As String = no.Substring(8, 2)
                Dim intNo As Integer = Integer.Parse(no.Substring(6, 4))
                Dim intFrom As Integer
                Dim intTo As Integer
                Dim noFrom As String
                Dim noTo As String
                Dim recResult As New HosyousyoDbRecord

                'Web.configより保証書DBのファイルパス構成を取得
                strPath = ConfigurationManager.AppSettings("HosyousyoDbFilePath")

                '●DBからサーバパスを取得
                recResult = cbLogic.GetHosyousyoDbLinkPath(kubun, ym)
                If Not recResult.KakunousakiFilePass Is Nothing Then
                    server = recResult.KakunousakiFilePass.ToString
                End If

                '番号下4桁が0000の場合、想定外なので、空文字を返す
                If intNo <= 0 Then
                    Return String.Empty
                End If

                'フォルダ名の範囲設定
                intFrom = ((intNo - 1) \ 100) * 100 + 1
                intTo = ((intNo - 1) \ 100 + 1) * 100

                noFrom = Format(intFrom, "0000")
                noTo = Format(intTo, "0000")

                'パスに置換
                strPath = strPath.Replace("@SERVER@", server)
                strPath = strPath.Replace("@YEAR@", year)
                strPath = strPath.Replace("@KUBUN@", kubun)
                strPath = strPath.Replace("@MONTH@", month)
                strPath = strPath.Replace("@NOFROM@", noFrom)
                strPath = strPath.Replace("@NOTO@", noTo)
                strPath = strPath.Replace("@NUM1@", num1)
                strPath = strPath.Replace("@NUM2@", num2)

                strReturn = strPath

                'DB値(ファイルパス)が取得出来ない場合は、パスを設定しない
                If String.IsNullOrEmpty(server) Then
                    strReturn = String.Empty
                End If
            Else
                strReturn = String.Empty
            End If
        End If

        'ボタンを渡されている場合、onclick設定
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strReturn = String.Empty, String.Empty, String.Format(strOpenLinkScript, strReturn))
        End If

        Return strReturn
    End Function

    ''' <summary>
    ''' ReportJHSリンクパス生成
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <param name="no">番号</param>
    ''' <returns>保証書DBファイルパス</returns>
    ''' <remarks></remarks>
    Public Function getReportJHSPath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strPath = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                Dim path As String = ConfigurationManager.AppSettings("ReportJHSPath")

                'パスに置換
                path = path.Replace("@KUBUN@", kubun)
                path = path.Replace("@NO@", no)

                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        'ボタンを渡されている場合、onclick設定
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
        End If

        Return strPath
    End Function

    ''' <summary>
    ''' 加盟店注意情報リンクパス生成
    ''' </summary>
    ''' <param name="kameitenCdClientId">加盟店コード入力項目のClientID</param>
    ''' <returns>加盟店注意情報ファイルパス</returns>
    ''' <remarks></remarks>
    Public Function getKameitenTyuuijouhouPath(ByVal kameitenCdClientId As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(kameitenCdClientId) Then
            strPath = String.Empty
        Else
            If kameitenCdClientId.Length >= 1 Then
                Dim path As String = UrlConst.EARTH2_KAMEITEN_TYUUIJIKOU
                'パスに置換
                path += "?strKameitenCd=" & "' + objEBI('" & kameitenCdClientId & "').value + '"
                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        'ボタンを渡されている場合、onclick設定
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
        End If

        Return strPath

    End Function

    ''' <summary>
    ''' 請求先マスタリンクパス生成
    ''' </summary>
    ''' <param name="prmSskCd">請求先コード.ClientID</param>
    ''' <param name="prmSskBrc">請求先枝番.ClientID</param>
    ''' <param name="prmSskKbn">請求先区分.ClientID</param>
    ''' <param name="targetCtrl">対象HTMLコントロール</param>
    ''' <returns>請求先マスタファイルパス</returns>
    ''' <remarks></remarks>
    Public Function getSeikyuuSakiMasterPath( _
                                            ByVal prmSskCd As String _
                                            , ByVal prmSskBrc As String _
                                            , ByVal prmSskKbn As String _
                                            , Optional ByRef targetCtrl As HtmlControl = Nothing _
                                            ) As String

        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(prmSskCd) _
            Or String.IsNullOrEmpty(prmSskBrc) _
                Or String.IsNullOrEmpty(prmSskKbn) Then

            strPath = String.Empty
        Else
            If (prmSskCd & prmSskBrc & prmSskKbn).Length >= 1 Then
                Dim path As String = UrlConst.EARTH2_SEIKYUUSAKI_MASTER '請求先M

                path += "?sendSearchTerms=" _
                         & prmSskCd & EarthConst.SEP_STRING _
                         & prmSskBrc & EarthConst.SEP_STRING _
                         & prmSskKbn

                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        'ボタンを渡されている場合、onclick設定
        If targetCtrl IsNot Nothing Then
            targetCtrl.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
            'スタイル設定
            If targetCtrl.GetType.Name = "HtmlAnchor" Then 'アンカーの場合
                targetCtrl.Attributes("style") = "text-decoration:underline;display:inherit;cursor:pointer;"
            End If
        End If

        Return strPath

    End Function

    ''' <summary>
    ''' 申込情報リンクパス生成
    ''' </summary>
    ''' <param name="strMousikomiNo">申込NO</param>
    ''' <returns>申込情報リンクパス</returns>
    ''' <remarks></remarks>
    Public Function getMousikomiPath(ByVal strMousikomiNo As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(strMousikomiNo) Then
            strPath = String.Empty
        Else
            If strMousikomiNo.Length >= 15 Then
                Dim path As String = ConfigurationManager.AppSettings("MousikomiPath")

                'パスに置換
                path = path.Replace("@NO@", strMousikomiNo)

                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        'ボタンを渡されている場合、onclick設定
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
        End If

        Return strPath
    End Function

    ''' <summary>
    ''' FC申込情報リンクパス生成
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <param name="no">番号</param>
    ''' <returns>申込情報リンクパス</returns>
    ''' <remarks></remarks>
    Public Function getFcMousikomiPath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strPath As String = String.Empty


        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strPath = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                Dim path As String = ConfigurationManager.AppSettings("FcMousikomiPath")

                'パスに置換
                path = path.Replace("@KUBUN@", kubun)
                path = path.Replace("@NO@", no)

                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        'ボタンを渡されている場合、onclick設定
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
        End If

        Return strPath
    End Function

    ''' <summary>
    ''' コントロールを表示スタイルに変更する
    ''' </summary>
    ''' <param name="tmpCtrl">切り替え対象コントロール</param>
    ''' <param name="tmpText">プルダウン等の値を表示するSpanコントロール</param>
    ''' <param name="blnBorder">テキストボックスの下線表示有無</param>
    ''' <remarks></remarks>
    Sub chgVeiwMode(ByRef tmpCtrl As Object, Optional ByRef tmpText As HtmlGenericControl = Nothing, Optional ByVal blnBorder As Boolean = False)

        'コントロールのタイプごとに処理を実行
        Select Case tmpCtrl.GetType.Name

            Case "HtmlInputText", "HtmlInputHidden", "HtmlTextArea"
                tmpCtrl.Attributes("readonly") = True
                Dim classString As String = String.Empty
                If tmpCtrl.Attributes("class") IsNot Nothing Then
                    classString = Replace(tmpCtrl.Attributes("class").ToString, "readOnlyStyle", "")
                    classString = Replace(classString, " readOnlyStyle", "")
                End If
                tmpCtrl.Attributes("class") = classString & " readOnlyStyle"
                If blnBorder = False Then
                    tmpCtrl.Style("border-style") = "none"
                End If
                tmpCtrl.Attributes("tabindex") = -1

            Case "TextBox"
                tmpCtrl.ReadOnly = True
                Dim classString As String = String.Empty
                If tmpCtrl.CssClass IsNot Nothing Then
                    classString = Replace(tmpCtrl.CssClass.ToString, "readOnlyStyle", "")
                    classString = Replace(classString, " readOnlyStyle", "")
                End If
                tmpCtrl.CssClass = classString & " readOnlyStyle"
                If blnBorder = False Then
                    tmpCtrl.Style("border-style") = "none"
                End If
                tmpCtrl.Tabindex = -1

            Case "HtmlInputRadioButton", "HtmlInputCheckBox", "CheckBox"
                If tmpText IsNot Nothing Then
                    tmpCtrl.Style("display") = "none"
                    If tmpCtrl.Checked = True Then
                        tmpText.InnerHtml = Replace(tmpText.InnerHtml, "&nbsp;【&nbsp;", String.Empty)
                        tmpText.InnerHtml = Replace(tmpText.InnerHtml, "&nbsp;】&nbsp;", String.Empty)
                        tmpText.InnerHtml = "&nbsp;【&nbsp;" & tmpText.InnerHtml & "&nbsp;】&nbsp;"
                        tmpText.Style("display") = "inline"
                    Else
                        tmpText.Style("display") = "none"
                    End If
                End If

            Case "HtmlSelect"
                If tmpText IsNot Nothing Then
                    tmpCtrl.Style("display") = "none"
                    tmpText.InnerHtml = "&nbsp;【&nbsp;" & tmpCtrl.Items(tmpCtrl.SelectedIndex).Text & "&nbsp;】&nbsp;"
                    tmpText.Style("display") = "inline"
                End If

            Case "DropDownList"
                If tmpText IsNot Nothing Then
                    tmpCtrl.Style("display") = "none"
                    tmpText.InnerHtml = "&nbsp;【&nbsp;" & tmpCtrl.SelectedItem.Text & "&nbsp;】&nbsp;"
                    tmpText.Style("display") = "inline"
                End If

            Case "HtmlInputButton"
                If tmpText IsNot Nothing Then
                    tmpCtrl.Style("display") = "none"
                End If

            Case Else
                Exit Sub

        End Select

    End Sub

    ''' <summary>
    ''' 商品行テキストボックスを表示スタイルに変更する
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Sub chgDispSyouhinText(ByRef text As Object)
        Dim strType As String = text.GetType.Name

        If strType = "HtmlInputText" Then
            text.Attributes.Remove("ReadOnly")
            If text.Attributes("Class") IsNot Nothing Then
                text.Attributes("Class") = _
                    text.Attributes("Class").Replace("readOnlyStyle", "")
            End If
            text.Attributes.Remove("tabindex")
        ElseIf strType = "TextBox" Then
            text.ReadOnly = False
            If text.CssClass IsNot Nothing Then
                text.CssClass = _
                    text.CssClass.ToString.Replace("readOnlyStyle", "")
            End If
            text.TabIndex = Nothing
        End If

        text.Style.Remove("border-style")
    End Sub

    ''' <summary>
    ''' 商品行プルダウンを表示スタイルに変更する
    ''' </summary>
    ''' <param name="pull"></param>
    ''' <param name="objSpan"></param>
    ''' <remarks></remarks>
    Sub chgDispSyouhinPull(ByRef pull As Object, ByRef objSpan As Object)
        pull.Style.Remove("display")
        objSpan.InnerHtml = String.Empty
    End Sub

    ''' <summary>
    ''' チェックボックスを表示スタイルに変更する
    ''' </summary>
    ''' <param name="pull"></param>
    ''' <param name="objSpan"></param>
    ''' <remarks></remarks>
    Sub chgDispCheckBox(ByRef pull As Object, ByRef objSpan As Object)
        pull.Style.Remove("display")
        objSpan.InnerHtml = String.Empty
    End Sub

    ''' <summary>
    ''' 系列コードより特定３系列かの判定を返します<br/>
    ''' 特定３系列
    ''' ・アイフルホーム "0001"
    ''' ・TH友の会 "THTH"
    ''' ・ワンダーホーム "NF03"
    ''' </summary>
    ''' <param name="keiretu_cd"></param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks> </remarks>
    Public Function getKeiretuFlg(ByVal keiretu_cd As String) As Boolean
        Select Case keiretu_cd
            Case EarthConst.KEIRETU_AIFURU
                Return True
            Case EarthConst.KEIRETU_TH
                Return True
            Case EarthConst.KEIRETU_WANDA
                Return True
        End Select
        Return False

    End Function

    ''' <summary>
    ''' 日付範囲チェック
    ''' </summary>
    ''' <param name="str">日付にパース可能な文字列</param>
    ''' <returns>チェック結果</returns>
    ''' <remarks></remarks>
    Public Function checkDateHanni(ByVal str As String) As Boolean
        Try
            If DateTime.Parse(str) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(str) < EarthConst.Instance.MIN_DATE Then
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' 日付範囲チェック(From,To指定)
    ''' </summary>
    ''' <param name="str">日付にパース可能な文字列</param>
    ''' <param name="dtFrom">From</param>
    ''' <param name="dtTo">To</param>
    ''' <returns>チェック結果</returns>
    ''' <remarks></remarks>
    Public Function checkDateHanniFromTo(ByVal str As String, _
                                         ByVal dtFrom As Date, _
                                         ByVal dtTo As Date) As Boolean
        Try
            If DateTime.Parse(str) >= dtFrom AndAlso DateTime.Parse(str) <= dtTo Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "地盤テーブル.更新者(kousinsya)"
    ''' <summary>
    ''' 画面表示用(地盤テーブル.更新者よりログインユーザ名、更新日付を取得する)
    ''' </summary>
    ''' <param name="strKousinsya">地盤テーブル.更新者(kousinsya)</param>
    ''' <param name="strUserName">ログインユーザ名</param>
    ''' <param name="strUpdDate">更新日付(yy/mm/dd hh:mm)</param>
    ''' <returns>ログインユーザID</returns>
    ''' <remarks>地盤テーブル.更新者(kousinsya)→ユーザ名＋更新日付(yy/mm/dd hh:mm)</remarks>
    Public Function SetKousinsya(ByVal strKousinsya As String, ByRef strUserName As String, ByRef strUpdDate As String) As String
        Dim strRetUserID As String = "" '戻り値(ログインユーザID)
        Dim strTmpUpdDate As String = "" '作業用(更新日付)
        Dim strTmp() As String = Nothing

        If strKousinsya Is Nothing OrElse strKousinsya.Trim = "" Then
            strUserName = ""
            strUpdDate = ""
            'ユーザIDを返却する
            Return strRetUserID
            Exit Function
        End If

        '地盤テーブル.更新者より、ユーザIDと更新日付に分割する
        strTmp = strKousinsya.Split("$")
        strRetUserID = strTmp(0)
        strTmpUpdDate = strTmp(1)

        'ユーザIDよりユーザ名を取得する
        Dim userLogic As New LoginUserLogic
        Dim userInfo As New LoginUserInfo

        'ユーザ名をセット
        If userLogic.MakeUserInfo(strRetUserID, userInfo) Then
            strUserName = userInfo.Name 'ユーザ名を返却
        Else
            strUserName = strRetUserID 'ユーザIDを返却
        End If

        '更新日付をフォーマットしてセット
        Dim dtTmp As New DateTime
        Try
            dtTmp = DateTime.ParseExact(strTmpUpdDate, EarthConst.FORMAT_DATE_TIME_4, Nothing)
            strUpdDate = Format(dtTmp, "yyyy/MM/dd HH:mm")
        Catch ex As Exception
            strUpdDate = strTmpUpdDate
        End Try

        'ユーザIDを返却する
        Return strRetUserID
    End Function

#End Region

#Region "[画面表示用]登録者名,更新者名"
    ''' <summary>
    ''' 画面表示用(ユーザIDより登録/更新ユーザ名を返却する)
    ''' </summary>
    ''' <param name="strUserID">ユーザID</param>
    ''' <remarks>ユーザ名(取得不可時、ユーザID)</remarks>
    Public Function SetDispUserNM(ByVal strUserID As String) As String
        Dim strRetVal As String = String.Empty '戻り値

        If strUserID Is Nothing OrElse strUserID.Trim = String.Empty Then
            '空白を返却する
            Return strRetVal
            Exit Function
        End If

        'ユーザIDよりユーザ名を取得する
        Dim userLogic As New LoginUserLogic
        Dim userInfo As New LoginUserInfo

        'ユーザ名をセット
        If userLogic.MakeUserInfo(strUserID, userInfo) Then
            If userInfo.DisplayName Is Nothing OrElse userInfo.DisplayName = String.Empty Then
                strRetVal = strUserID  'ユーザIDを返却
            Else
                strRetVal = userInfo.DisplayName  'ユーザ名を返却
            End If
        Else
            strRetVal = strUserID 'ユーザIDを返却
        End If

        Return strRetVal
    End Function

#End Region

#Region "商品コード判定(直工事=B2000番台)"

    ''' <summary>
    ''' 直工事商品チェック
    ''' </summary>
    ''' <param name="strSyouhinCd"></param>
    ''' <returns>True[B2000番台] or False[B2000番台以外]</returns>
    ''' <remarks>対象とする商品コードが直工事(=B2000番台)かどうか判定を行なう。</remarks>
    Public Function ChkSyouhinCdB2000(ByVal strSyouhinCd As String) As Boolean
        '入力チェック
        If strSyouhinCd Is Nothing OrElse strSyouhinCd.Trim = String.Empty Then
            Return False
        End If

        Dim strLogic As New StringLogic
        '先頭文字列が「B2」でかつ、商品コードの長さが5桁
        If strSyouhinCd.StartsWith("B2") And strLogic.GetStrByteSJIS(strSyouhinCd) = 5 Then
            Return True
        End If
        Return False
    End Function

#End Region

#Region "請求先/仕入先"

    ''' <summary>
    ''' 請求先区分、請求先(仕入先)コード、請求先(仕入先)枝番の表記文字列を返す
    ''' </summary>
    ''' <param name="strSeikyuuSakiKbn">請求先区分 or ""(仕入先)</param>
    ''' <param name="strSakiCd">請求先(仕入先)コード</param>
    ''' <param name="strSakiBrc">請求先(仕入先)枝番</param>
    ''' <param name="blnBlank">ブランクとして表示するかフラグ(True：ブランク表示 False："＆nbsp;"表示)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispSeikyuuSakiCd(ByVal strSeikyuuSakiKbn As String, ByVal strSakiCd As String, ByVal strSakiBrc As String, Optional ByVal blnBlank As Boolean = False) As String
        Const KAMEITEN As String = "加:"    '加盟店
        Const TYSKAISYA As String = "調:"   '調査会社
        Const EIGYOUSYO As String = "営:"   '営業所
        Const HYPHEN As String = " - "      'ハイフン
        Const SPACE As String = EarthConst.HANKAKU_SPACE    '半角スペース

        Dim strRet As String = String.Empty

        '請求先区分
        If Me.GetDisplayString(strSeikyuuSakiKbn) = String.Empty Then
            strRet = String.Empty
        Else
            If strSeikyuuSakiKbn = "0" Then
                strRet = KAMEITEN
            ElseIf strSeikyuuSakiKbn = "1" Then
                strRet = TYSKAISYA
            ElseIf strSeikyuuSakiKbn = "2" Then
                strRet = EIGYOUSYO
            Else
                strRet = String.Empty
            End If
        End If

        '請求先コード、請求先枝番
        If Me.GetDisplayString(strSakiCd) = String.Empty Or Me.GetDisplayString(strSakiBrc) = String.Empty Then
            If blnBlank = False Then
                strRet = SPACE
            Else
                strRet = String.Empty
            End If
        Else
            '連結
            strRet &= strSakiCd & HYPHEN & strSakiBrc
        End If

        Return strRet
    End Function

#End Region

#Region "支払/科目"

    ''' <summary>
    ''' 科目の画面表示文字列の整形を行ない、返却する
    ''' </summary>
    ''' <param name="intKamoku">科目</param>
    ''' <param name="blnBlank">ブランクとして表示するかフラグ(True：ブランク表示 False："＆nbsp;"表示)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispKamoku(ByVal intKamoku As Integer, Optional ByVal blnBlank As Boolean = False) As String
        Dim strRet As String = String.Empty

        If intKamoku = 0 Then
            strRet = EarthConst.KAMOKU_KAIKAKE
        ElseIf intKamoku = 1 Then
            strRet = EarthConst.KAMOKU_MIBARAI
        Else
            If blnBlank Then
                strRet = String.Empty
            Else
                strRet = EarthConst.HANKAKU_SPACE
            End If
        End If

        Return strRet
    End Function

#End Region

#Region "ドロップダウンリスト内のデータ存在チェック"
    ''' <summary>
    ''' ドロップダウンリスト内のデータ存在チェック
    ''' </summary>
    ''' <param name="drpArg">チェック対象ドロップダウンリスト</param>
    ''' <param name="strSearchCd">[String]コード値</param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks>第一引数のドロップダウンリスト内に、第二引数のデータが存在するかどうかを判断する</remarks>
    Public Function ChkDropDownList(ByVal drpArg As DropDownList, ByVal strSearchCd As String) As Boolean
        Dim intItemCnt As Integer = drpArg.Items.Count 'アイテム数
        Dim intCnt As Integer 'カウンタ

        For intCnt = 0 To intItemCnt - 1
            If strSearchCd = drpArg.Items(intCnt).Value Then
                Return True
            End If
        Next

        Return False
    End Function
#End Region

    ''' <summary>
    ''' 当画面を閉じるスクリプトを生成する
    ''' </summary>
    ''' <param name="objPage">ページクラスオブジェクト</param>
    ''' <remarks></remarks>
    Public Sub CloseWindow(ByVal objPage As Page)
        Dim tmpScript As String = "window.close();" '画面を閉じる
        ScriptManager.RegisterStartupScript(objPage, Me.GetType(), "CloseWindow", tmpScript, True)
    End Sub

    ''' <summary>
    ''' 文字列を数値型[Long]に変換します
    ''' </summary>
    ''' <param name="strValue">変換したい文字列</param>
    ''' <returns>変換後の数値</returns>
    ''' <remarks></remarks>
    Public Function Str2Long(ByVal strValue As String) As Long
        Dim lngRet As Long

        If strValue Is Nothing OrElse strValue.Length = 0 Then
            lngRet = 0
        Else
            strValue = strValue.Replace(",", "")
            If IsNumeric(strValue) Then
                lngRet = Long.Parse(strValue)
            Else
                lngRet = 0
            End If
        End If
        Return lngRet
    End Function

#Region "データファイル出力関連"

#Region "テキストをファイル出力する"
    ''' <summary>
    ''' テキストをファイル出力する
    ''' </summary>
    ''' <param name="strFileNm">出力ファイル名</param>
    ''' <param name="dtTable">データテーブル※出力用に整形済のこと</param>
    ''' <param name="strQuote">セパレータ(括り文字)</param>
    ''' <param name="strDelimiter">デリミタ(区切り文字)</param>
    ''' <param name="strLineFeedCd">改行コード(1レコード終端文字)</param>
    ''' <param name="blnHeaderUmu">ヘッダー情報の有無</param>
    ''' <returns>True,False</returns>
    ''' <remarks></remarks>
    Public Function OutPutFileFromDtTable( _
                                             ByVal strFileNm As String _
                                            , ByRef dtTable As DataTable _
                                            , Optional ByVal strQuote As String = EarthConst.CSV_QUOTE _
                                            , Optional ByVal strDelimiter As String = EarthConst.CSV_DELIMITER _
                                            , Optional ByVal strLineFeedCd As String = vbCrLf _
                                            , Optional ByVal blnHeaderUmu As Boolean = True _
                                            ) As Boolean

        Dim sbOutput As New StringBuilder '出力用文字列

        'ファイル名
        If strFileNm = String.Empty Then
            '未指定時は出力しない
            Return False
        Else
            'ファイル名の置換
            strFileNm = strFileNm.Replace("@yyyyMMddHHmmss@", DateTime.Now.ToString(EarthConst.FORMAT_DATE_TIME_6))
        End If

        '取得データを出力用文字列に格納
        Dim strErrRet As String = Me.dtTableToStringOutput(sbOutput, dtTable, strQuote, strDelimiter, strLineFeedCd, blnHeaderUmu)
        If strErrRet <> String.Empty Then
            ' 出力用文字列がないので、処理終了
            Return False
        End If

        Dim httpRes As HttpResponse = HttpContext.Current.Response

        'ファイルの出力を行う
        With httpRes
            .Clear()
            .AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileNm))
            .ContentType = "text/plain"
            .BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(sbOutput.ToString))
            .End()
        End With

        Return True
    End Function

#End Region

#Region "データテーブルの内容をテキスト化する"
    ''' <summary>
    ''' データテーブルの内容をテキスト化する
    ''' </summary>
    ''' <param name="sbText">出力用StringBuilder</param>
    ''' <param name="dtTable">出力対象データテーブル</param>
    ''' <param name="strQuote">セパレータ(括り文字)</param>
    ''' <param name="strDelimiter">デリミタ(区切り文字)</param>
    ''' <param name="strLineFeedCd">改行コード(1レコード終端文字)</param>
    ''' <param name="blnHeaderUmu">ヘッダー情報の有無</param>
    ''' <returns>リターンメッセージ</returns>
    ''' <remarks></remarks>
    Public Function dtTableToStringOutput( _
                                        ByRef sbText As StringBuilder _
                                        , ByVal dtTable As DataTable _
                                        , Optional ByVal strQuote As String = EarthConst.CSV_QUOTE _
                                        , Optional ByVal strDelimiter As String = EarthConst.CSV_DELIMITER _
                                        , Optional ByVal strLineFeedCd As String = vbCrLf _
                                        , Optional ByVal blnHeaderUmu As Boolean = True _
                                        ) As String

        Dim strRetMsg As String            'リターンメッセージ
        Dim sbTmp As New StringBuilder()   '出力用Stringセット用StrinBuilder
        Dim rowSb As New StringBuilder()   '作業用Stringセット用StrinBuilder
        Dim blnSentou As Boolean = True          'カウンタ

        Dim strTmpVal As String = String.Empty
        'カウンタ
        Dim intCnt As Integer = 0
        '出力最大件数
        Dim end_count As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        Try
            'ヘッダー情報ありの場合
            If blnHeaderUmu Then

                '********************
                '* ヘッダ行作成
                '********************
                'ヘッダ格納用データテーブル
                Dim dtHeadTbl As DataTable
                'ヘッダ格納用データテーブルの列
                Dim dtCol As DataColumn

                'ヘッダ格納用データテーブルをインスタンス生成
                dtHeadTbl = New DataTable

                '出力するデータテーブルの列分ループ
                For intColCnt As Integer = 0 To dtTable.Columns.Count - 1
                    'ヘッダ格納用データテーブルの列をインスタンス生成
                    dtCol = New DataColumn
                    'ヘッダ格納用データテーブル列の属性を設定
                    dtCol.DataType = System.Type.GetType("System.String")
                    'ヘッダ格納用データテーブル列をデータテーブルに追加
                    dtHeadTbl.Columns.Add(dtCol)
                Next

                'ヘッダ格納用データテーブルの行
                Dim dtRow As DataRow
                'ヘッダ格納用データテーブルの行をインスタンス生成
                dtRow = dtHeadTbl.NewRow

                'ヘッダ格納用データテーブルのカラム分ループ
                For intColCnt As Integer = 0 To dtHeadTbl.Columns.Count - 1
                    '出力するデータテーブルのカラム名を、インスタンス生成したヘッダ格納用データテーブル行にセットしていく
                    dtRow(intColCnt) = dtTable.Columns(intColCnt).ColumnName
                Next
                dtHeadTbl.Rows.Add(dtRow)

                '作成したヘッダ格納用データテーブルの行をテキストファイルの1行目へセット
                rowSb = New StringBuilder()
                blnSentou = True
                For Each tmpcol As Object In dtHeadTbl.Rows(0).ItemArray
                    strTmpVal = tmpcol.ToString

                    If blnSentou Then '先頭列
                        rowSb.Append(strQuote & strTmpVal & strQuote)
                        blnSentou = False
                    Else
                        rowSb.Append(strDelimiter & strQuote & strTmpVal & strQuote)
                    End If
                Next
                rowSb.Append(vbCrLf) '改行
                sbTmp.Append(rowSb.ToString)

            End If

            '********************
            '* データ行作成
            '********************
            'データテーブルの内容をString化
            For Each tmpRow As DataRow In dtTable.Rows
                intCnt += 1
                If intCnt > end_count Then
                    Exit For
                End If
                rowSb = New StringBuilder()
                blnSentou = True
                For Each tmpCol As Object In tmpRow.ItemArray
                    strTmpVal = tmpCol.ToString

                    If blnSentou Then '先頭列
                        rowSb.Append(strQuote & strTmpVal & strQuote)
                        blnSentou = False
                    Else
                        rowSb.Append(strDelimiter & strQuote & strTmpVal & strQuote)
                    End If
                Next
                rowSb.Append(vbCrLf) '改行
                sbTmp.Append(rowSb.ToString)
            Next

            '出力用Stringにセット
            sbText = sbTmp

            'データチェック
            If sbText.Length > 0 Then
                strRetMsg = String.Empty
            Else
                strRetMsg = Messages.MSG020E
            End If

        Catch ex As Exception
            strRetMsg = ex.Message
        End Try

        Return strRetMsg
    End Function
#End Region

#End Region

#Region "ToolTipを設定"
    ''' <summary>
    ''' Htmlコントロールにツールチップを設定する
    ''' </summary>
    ''' <param name="targetCtrl">対象のHtmlコントロール</param>
    ''' <param name="strDispTip">表示する文字列</param>
    ''' <remarks></remarks>
    Public Sub SetToolTipForCtrl(ByVal targetCtrl As HtmlControl, ByVal strDispTip As String)
        targetCtrl.Attributes("title") = strDispTip
    End Sub

#Region "ToolTip設定タイプ"
    ''' <summary>
    ''' ToolTip設定タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emToolTipSetType
        ''' <summary>
        ''' 請求書発行日
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoHakDate = 1
        ''' <summary>
        ''' 請求締め日
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuuSimeDate = 2
    End Enum
#End Region

    ''' <summary>
    ''' (11) 検索結果一覧文字色反転処理/請求書一覧画面、過去請求書一覧画面
    ''' </summary>
    ''' <param name="dtRec">請求データレコードクラス</param>
    ''' <param name="targetCtrl">対象コントロール</param>
    ''' <param name="emType">セットタイプ</param>
    ''' <remarks></remarks>
    Public Sub setSeikyuusyoToolTip(ByVal dtRec As SeikyuuDataRecord _
                                        , ByVal targetCtrl As HtmlControl _
                                        , ByVal emType As emToolTipSetType _
                                        )

        Const TOOLTIP1 As String = "請求先マスタの請求締め日と請求データの請求締め日が異なります。" & vbCrLf
        Const TOOLTIP2 As String = "請求先マスタの請求締め日と請求データの請求書発行日が異なります。" & vbCrLf
        Const TOOLTIP3 As String = "請求データの請求書発行日と異なる請求年月日をもつ売上データが含まれています。" & vbCrLf

        '請求鑑T.請求締め日(日にちのみ)
        Dim strTmpRecSimeDate As String = IIf(dtRec.SeikyuuSimeDate Is Nothing, String.Empty, dtRec.SeikyuuSimeDate)
        Dim intTmpRecSimeDate As Integer
        If strTmpRecSimeDate = String.Empty Then
            intTmpRecSimeDate = Integer.MinValue
        Else
            intTmpRecSimeDate = Integer.Parse(strTmpRecSimeDate)
        End If

        '請求鑑T.請求書発行日(日にちのみ)
        Dim intTmpRecHakDate As Integer
        Dim dtSeikyuusyoHakDate As Date = dtRec.SeikyuusyoHakDate
        If dtSeikyuusyoHakDate = Date.MinValue Then
            intTmpRecHakDate = Integer.MinValue
        Else
            intTmpRecHakDate = Integer.Parse(dtSeikyuusyoHakDate.Day.ToString)
        End If

        '請求先M.請求締め日(日にちのみ)
        Dim strTmpMstSimeDate As String = IIf(dtRec.SeikyuuSimeDateMst Is Nothing, String.Empty, dtRec.SeikyuuSimeDateMst)
        Dim intTmpMstSimeDate As Integer
        If strTmpMstSimeDate = String.Empty Then
            intTmpMstSimeDate = Integer.MinValue
        Else
            intTmpMstSimeDate = Integer.Parse(strTmpMstSimeDate)
        End If

        '請求鑑T.請求書発行日(年月)と請求先M.請求締め日(日)を基に請求書発行日を取得
        Dim dtTmpMstSeikyuusyoHakDate As Date
        Dim strTmpMstSeikyuusyoHakDate As String
        Dim cBizLogic As New CommonBizLogic
        If dtSeikyuusyoHakDate = Date.MinValue Or strTmpMstSimeDate = String.Empty Then
            dtTmpMstSeikyuusyoHakDate = Date.MinValue
        Else
            strTmpMstSeikyuusyoHakDate = cBizLogic.GetDateStrReplaceDay(dtSeikyuusyoHakDate, strTmpMstSimeDate)
            If Not IsDate(strTmpMstSeikyuusyoHakDate) Then
                strTmpMstSeikyuusyoHakDate = cBizLogic.GetEndOfMonth(dtSeikyuusyoHakDate)
            End If
            dtTmpMstSeikyuusyoHakDate = Date.Parse(strTmpMstSeikyuusyoHakDate)
        End If

        '請求書発行日の差異フラグ(1:差異あり、0:差異なし、NULL:いずれかNULL値のため、差異ありとみなす)
        Dim intSeikyuuDateFlg As Integer = IIf(dtRec.SeikyuuDateSaiFlg = Integer.MinValue, 1, dtRec.SeikyuuDateSaiFlg)

        Dim strTip1 As String = String.Empty
        Dim strTip2 As String = String.Empty
        Dim strTip3 As String = String.Empty

        Dim strSetTip As String = String.Empty

        '対象コントロール別に設定
        Select Case emType
            Case emToolTipSetType.SeikyuuSimeDate '【１】請求締め日項目
                '1. 請求先マスタ.請求締め日と請求鑑.請求締め日の比較
                If intTmpMstSimeDate <> intTmpRecSimeDate Then
                    strSetTip &= TOOLTIP1
                End If

            Case emToolTipSetType.SeikyuusyoHakDate '【２】請求書発行日項目
                '1.請求先マスタ.請求締め日と請求鑑T.請求書発行日（データ）の比較
                If dtTmpMstSeikyuusyoHakDate <> dtSeikyuusyoHakDate Then
                    strSetTip &= TOOLTIP2
                End If
                '2.請求鑑T.請求書発行日と請求鑑明細T.請求書NOに紐付く売上伝票の請求年月日の比較
                If intSeikyuuDateFlg = 1 Then
                    strSetTip &= TOOLTIP3
                End If
            Case Else
                Exit Sub
        End Select

        '●ツールチップ設定
        If strSetTip <> String.Empty Then
            Me.SetToolTipForCtrl(targetCtrl, strSetTip)
            'スタイル設定
            setStyleRedBold(targetCtrl.Style, True)
        End If

    End Sub

#End Region

#Region "請求先検索ポップアップ"
    ''' <summary>
    ''' 請求先検索画面呼出処理
    ''' </summary>
    ''' <param name="SelectKbn">請求先区分ドロップダウンリスト</param>
    ''' <param name="TextCd">請求先コードテキストボックス</param>
    ''' <param name="TextBrc">請求先枝番テキストボックス</param>
    ''' <param name="TextMei">請求先名テキストボックス</param>
    ''' <param name="HiddenCall">ウィンドウ呼出し判断Hidden</param>
    ''' <param name="HiddenOld">変更前請求先格納Hidden配列(区分,番号,枝番の順でセット)</param>
    ''' <param name="ButtonSearch">検索ボタン</param>
    ''' <param name="objPage">ページクラスオブジェクト</param>
    ''' <returns>処理成否(True/False)</returns>
    ''' <remarks>テキストボックスWebControlバージョン</remarks>
    Public Function CallSeikyuuSakiSearchWindow(ByVal sender As System.Object _
                                            , ByVal e As System.EventArgs _
                                            , ByVal objPage As Page _
                                            , ByVal SelectKbn As DropDownList _
                                            , ByVal TextCd As TextBox _
                                            , ByVal TextBrc As TextBox _
                                            , ByVal TextMei As TextBox _
                                            , ByVal ButtonSearch As HtmlInputButton _
                                            , Optional ByVal HiddenOld() As Object = Nothing _
                                            , Optional ByVal HiddenCall As Object = Nothing _
                                            ) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallSeikyuuSakiSearch", _
                                                    SelectKbn, _
                                                    TextCd, _
                                                    TextBrc, _
                                                    TextMei, _
                                                    objPage)
        Dim strSkKbn As String      '請求先区分
        Dim strSkCd As String       '請求先コード
        Dim strSkBrc As String      '請求先枝番
        Dim strSkKbnClId As String      '請求先区分ID
        Dim strSkCdClId As String       '請求先コードID
        Dim strSkBrcClId As String      '請求先枝番ID
        Dim strSkMeiClId As String      '請求先名ID

        Dim tmpScript As String = String.Empty

        '画面から請求先情報を取得
        strSkKbn = IIf(SelectKbn.SelectedValue <> String.Empty, SelectKbn.SelectedValue, String.Empty)
        strSkCd = IIf(TextCd.Text <> String.Empty, TextCd.Text, String.Empty)
        strSkBrc = IIf(TextBrc.Text <> String.Empty, TextBrc.Text, String.Empty)
        strSkKbnClId = SelectKbn.ClientID
        strSkCdClId = TextCd.ClientID
        strSkBrcClId = TextBrc.ClientID
        strSkMeiClId = TextMei.ClientID

        Dim dicSeikyuu As New Dictionary(Of String, String)
        dicSeikyuu.Add(strSkKbnClId, strSkKbn)
        dicSeikyuu.Add(strSkCdClId, strSkCd)
        dicSeikyuu.Add(strSkBrcClId, strSkBrc)
        dicSeikyuu.Add(strSkMeiClId, String.Empty)

        Dim blnResult As Boolean
        blnResult = CallSeikyuuSakiSearchWindowCmn(sender _
                                                    , strSkKbn _
                                                    , strSkCd _
                                                    , strSkBrc _
                                                    , strSkKbnClId _
                                                    , strSkCdClId _
                                                    , strSkBrcClId _
                                                    , strSkMeiClId _
                                                    , ButtonSearch _
                                                    , dicSeikyuu _
                                                    , HiddenOld _
                                                    , HiddenCall)

        SelectKbn.SelectedValue = dicSeikyuu(strSkKbnClId)
        TextCd.Text = dicSeikyuu(strSkCdClId)
        TextBrc.Text = dicSeikyuu(strSkBrcClId)
        TextMei.Text = dicSeikyuu(strSkMeiClId)

        Return blnResult

    End Function

    ''' <summary>
    ''' 請求先検索画面呼出処理
    ''' </summary>
    ''' <param name="SelectKbn">請求先区分ドロップダウンリスト</param>
    ''' <param name="TextCd">請求先コードテキストボックス</param>
    ''' <param name="TextBrc">請求先枝番テキストボックス</param>
    ''' <param name="TextMei">請求先名テキストボックス</param>
    ''' <param name="HiddenCall">ウィンドウ呼出し判断Hidden</param>
    ''' <param name="HiddenOld">変更前請求先格納Hidden</param>
    ''' <param name="ButtonSearch">検索ボタン</param>
    ''' <param name="objPage">ページクラスオブジェクト</param>
    ''' <returns>処理成否(True/False)</returns>
    ''' <remarks>テキストボックスHtmlControlバージョン</remarks>
    Public Function CallSeikyuuSakiSearchWindow(ByVal sender As System.Object _
                                            , ByVal e As System.EventArgs _
                                            , ByVal objPage As Page _
                                            , ByVal SelectKbn As DropDownList _
                                            , ByVal TextCd As HtmlInputText _
                                            , ByVal TextBrc As HtmlInputText _
                                            , ByVal TextMei As HtmlInputText _
                                            , ByVal ButtonSearch As HtmlInputButton _
                                            , Optional ByVal HiddenOld() As Object = Nothing _
                                            , Optional ByVal HiddenCall As Object = Nothing _
                                            ) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallSeikyuuSakiSearchWindow", _
                                                    SelectKbn, _
                                                    TextCd, _
                                                    TextBrc, _
                                                    TextMei, _
                                                    objPage)
        Dim strSkKbn As String      '請求先区分
        Dim strSkCd As String       '請求先コード
        Dim strSkBrc As String      '請求先枝番
        Dim strSkKbnClId As String      '請求先区分ID
        Dim strSkCdClId As String       '請求先コードID
        Dim strSkBrcClId As String      '請求先枝番ID
        Dim strSkMeiClId As String      '請求先名ID

        Dim tmpScript As String = String.Empty

        '画面から請求先情報を取得
        strSkKbn = IIf(SelectKbn.SelectedValue <> String.Empty, SelectKbn.SelectedValue, String.Empty)
        strSkCd = IIf(TextCd.Value <> String.Empty, TextCd.Value, String.Empty)
        strSkBrc = IIf(TextBrc.Value <> String.Empty, TextBrc.Value, String.Empty)
        strSkKbnClId = SelectKbn.ClientID
        strSkCdClId = TextCd.ClientID
        strSkBrcClId = TextBrc.ClientID
        strSkMeiClId = TextMei.ClientID

        Dim dicSeikyuu As New Dictionary(Of String, String)
        dicSeikyuu.Add(strSkKbnClId, strSkKbn)
        dicSeikyuu.Add(strSkCdClId, strSkCd)
        dicSeikyuu.Add(strSkBrcClId, strSkBrc)
        dicSeikyuu.Add(strSkMeiClId, String.Empty)

        Dim blnResult As Boolean
        blnResult = CallSeikyuuSakiSearchWindowCmn(sender _
                                                    , strSkKbn _
                                                    , strSkCd _
                                                    , strSkBrc _
                                                    , strSkKbnClId _
                                                    , strSkCdClId _
                                                    , strSkBrcClId _
                                                    , strSkMeiClId _
                                                    , ButtonSearch _
                                                    , dicSeikyuu _
                                                    , HiddenOld _
                                                    , HiddenCall)

        SelectKbn.SelectedValue = dicSeikyuu(strSkKbnClId)
        TextCd.Value = dicSeikyuu(strSkCdClId)
        TextBrc.Value = dicSeikyuu(strSkBrcClId)
        TextMei.Value = dicSeikyuu(strSkMeiClId)

        Return blnResult

    End Function

    ''' <summary>
    ''' 請求先検索画面呼出処理［共通］
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strSkKbn">請求先区分</param>
    ''' <param name="strSkCd">請求先コード</param>
    ''' <param name="strSkBrc">請求先枝番</param>
    ''' <param name="strSkKbnClId">請求先区分ドロップダウンリストID</param>
    ''' <param name="strSkCdClId">請求先コードテキストボックスID</param>
    ''' <param name="strSkBrcClId">請求先枝番テキストボックスID</param>
    ''' <param name="strSkMeiClId">請求先名テキストボックスID</param>
    ''' <param name="ButtonSearch">検索ボタン</param>
    ''' <param name="dicRet">請求先情報格納Dictionary</param>
    ''' <param name="HiddenOld">変更前請求先格納Hidden</param>
    ''' <param name="HiddenCall">ウィンドウ呼出し判断Hidden</param>
    ''' <returns>処理成否(True/False)</returns>
    ''' <remarks></remarks>
    Private Function CallSeikyuuSakiSearchWindowCmn(ByVal sender As System.Object _
                                                    , ByVal strSkKbn As String _
                                                    , ByVal strSkCd As String _
                                                    , ByVal strSkBrc As String _
                                                    , ByVal strSkKbnClId As String _
                                                    , ByVal strSkCdClId As String _
                                                    , ByVal strSkBrcClId As String _
                                                    , ByVal strSkMeiClId As String _
                                                    , ByVal ButtonSearch As HtmlInputButton _
                                                    , ByRef dicRet As Dictionary(Of String, String) _
                                                    , Optional ByRef HiddenOld() As Object = Nothing _
                                                    , Optional ByRef HiddenCall As Object = Nothing _
                                                    ) As Boolean
        Dim intAllCnt As Integer
        Dim list As New List(Of SeikyuuSakiInfoRecord)
        Dim uriageLogic As New UriageDataSearchLogic

        If strSkKbn <> String.Empty Or strSkCd <> String.Empty Or strSkBrc <> String.Empty Then
            intAllCnt = uriageLogic.GetSeikyuuSakiCnt(strSkCd, strSkBrc, strSkKbn, String.Empty, String.Empty, True)
            If intAllCnt = 1 Then
                list = uriageLogic.GetSeikyuuSakiInfo(strSkCd, strSkBrc, strSkKbn, String.Empty, String.Empty, intAllCnt, 1, 10, True)
            End If
        End If

        If intAllCnt = 1 Then
            Dim recData As SeikyuuSakiInfoRecord = list(0)
            dicRet(strSkKbnClId) = GetDisplayString(recData.SeikyuuSakiKbn)
            dicRet(strSkCdClId) = GetDisplayString(recData.SeikyuuSakiCd)
            dicRet(strSkBrcClId) = GetDisplayString(recData.SeikyuuSakiBrc)
            dicRet(strSkMeiClId) = GetDisplayString(recData.SeikyuuSakiMei)

            If HiddenOld IsNot Nothing Then
                If dicRet(strSkKbnClId) = String.Empty _
                        And dicRet(strSkCdClId) = String.Empty _
                        And dicRet(strSkBrcClId) = String.Empty Then
                    For intCnt As Integer = LBound(HiddenOld) To UBound(HiddenOld)
                        HiddenOld(intCnt).value = String.Empty
                    Next
                Else
                    If HiddenOld.Length = 1 Then
                        HiddenOld(0).value = dicRet(strSkKbnClId) & EarthConst.SEP_STRING _
                                            & dicRet(strSkCdClId) & EarthConst.SEP_STRING _
                                            & dicRet(strSkBrcClId)
                    Else
                        HiddenOld(0).value = dicRet(strSkKbnClId)
                        HiddenOld(1).value = dicRet(strSkCdClId)
                        HiddenOld(2).value = dicRet(strSkBrcClId)
                    End If
                End If
            End If
            Return True
        Else
            '請求先名をクリア
            dicRet(strSkMeiClId) = String.Empty

            'フォーカスセット
            Dim tmpFocusScript = "objEBI('" & ButtonSearch.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & strSkCdClId & EarthConst.SEP_STRING & _
                                            strSkBrcClId & EarthConst.SEP_STRING & _
                                            strSkKbnClId & "','" _
                                        & UrlConst.SEARCH_SEIKYUU_SAKI & "','" _
                                        & strSkCdClId & EarthConst.SEP_STRING & _
                                            strSkBrcClId & EarthConst.SEP_STRING & _
                                            strSkKbnClId & EarthConst.SEP_STRING & _
                                            strSkMeiClId & "','" _
                                        & ButtonSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            If HiddenCall IsNot Nothing Then
                If HiddenCall.Value <> String.Empty Then
                    ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            End If
            Return False
        End If

    End Function
#End Region

#Region "加盟店検索ポップアップ"
    ''' <summary>
    ''' 加盟店検索画面呼出処理
    ''' </summary>
    ''' <param name="objPage">ページクラスオブジェクト</param>
    ''' <param name="strKbnId">区分ClientID</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="TextCd">加盟店コードテキストボックス</param>
    ''' <param name="TextMei">加盟店名テキストボックス</param>
    ''' <param name="ButtonSearch">検索ボタン</param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <param name="TextTorikesi">加盟店取消理由テキストボックス</param>
    ''' <returns>処理成否(True/False)</returns>
    ''' <remarks></remarks>
    Public Function CallKameitenSearchWindow(ByVal sender As System.Object _
                                             , ByVal e As System.EventArgs _
                                             , ByVal objPage As Page _
                                             , ByVal strKbnId As String _
                                             , ByVal strKbn As String _
                                             , ByVal TextCd As Object _
                                             , ByVal TextMei As Object _
                                             , ByVal ButtonSearch As Object _
                                             , Optional ByVal blnTorikesi As Boolean = False _
                                             , Optional ByVal TextTorikesi As Object = Nothing _
                                             ) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallKameitenSearchWindow" _
                                                    , objPage _
                                                    , strKbnId _
                                                    , strKbn _
                                                    , TextCd _
                                                    , TextMei _
                                                    , TextTorikesi _
                                                    , ButtonSearch _
                                                    , blnTorikesi _
                                                    , TextTorikesi)

        Dim strKameitenCd As String = String.Empty  '加盟店コード
        Dim strKameitenCdClId As String             '加盟店コードID
        Dim strKameitenMeiClId As String            '加盟店名ID
        Dim strTorikeiClId As String                '請求先取消理由ID
        Dim tmpScript As String = String.Empty

        '画面から加盟店情報を取得
        If TextCd.GetType.Name = "HtmlInputText" Then
            strKameitenCd = IIf(TextCd.Value <> String.Empty, TextCd.Value, String.Empty)
        ElseIf TextCd.GetType.Name = "TextBox" Then
            strKameitenCd = IIf(TextCd.Text <> String.Empty, TextCd.Text, String.Empty)
        End If
        strKameitenCdClId = TextCd.ClientID
        strKameitenMeiClId = TextMei.ClientID

        Dim dicKameiten As New Dictionary(Of String, String)
        dicKameiten.Add(strKbnId, strKbn)
        dicKameiten.Add(strKameitenCdClId, strKameitenCd)
        dicKameiten.Add(strKameitenMeiClId, String.Empty)

        '取消理由
        If TextTorikesi Is Nothing Then
            strTorikeiClId = ""
        Else
            strTorikeiClId = TextTorikesi.ClientID
            dicKameiten.Add(strTorikeiClId, String.Empty)
            dicKameiten.Add(EarthConst.STYLE_FONT_COLOR, String.Empty)
        End If

        Dim blnResult As Boolean
        blnResult = CallKameitenSearchWindowCmn(sender _
                                                , strKbn _
                                                , strKameitenCd _
                                                , strKbnId _
                                                , strKameitenCdClId _
                                                , strKameitenMeiClId _
                                                , strTorikeiClId _
                                                , ButtonSearch _
                                                , dicKameiten _
                                                , blnTorikesi)

        '加盟店コード
        If TextCd.GetType.Name = "HtmlInputText" Then
            TextCd.Value = dicKameiten(strKameitenCdClId)
        ElseIf TextCd.GetType.Name = "TextBox" Then
            TextCd.Text = dicKameiten(strKameitenCdClId)
        End If
        '加盟店名
        If TextMei.GetType.Name = "HtmlInputText" Then
            TextMei.Value = dicKameiten(strKameitenMeiClId)
        ElseIf TextMei.GetType.Name = "TextBox" Then
            TextMei.Text = dicKameiten(strKameitenMeiClId)
        End If

        If strTorikeiClId <> "" Then
            If dicKameiten.ContainsKey(strTorikeiClId) Then
                '取消理由
                If TextTorikesi.GetType.Name = "HtmlInputText" Then
                    TextTorikesi.Value = dicKameiten(strTorikeiClId)
                ElseIf TextTorikesi.GetType.Name = "TextBox" Then
                    TextTorikesi.Text = dicKameiten(strTorikeiClId)
                End If
                '色替え処理
                setStyleFontColor(TextCd.Style, dicKameiten(EarthConst.STYLE_FONT_COLOR))
                setStyleFontColor(TextMei.Style, dicKameiten(EarthConst.STYLE_FONT_COLOR))
                setStyleFontColor(TextTorikesi.Style, dicKameiten(EarthConst.STYLE_FONT_COLOR))
            End If
        End If

        Return blnResult

    End Function

    ''' <summary>
    ''' 加盟店検索画面呼出処理［共通］
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKbnClId">区分テキストボックスID</param>
    ''' <param name="strKameitenCdClId">加盟店コードテキストボックスID</param>
    ''' <param name="strKameitenMeiClId">加盟店名テキストボックスID</param>
    ''' <param name="strTorikesiRiyuuClId">加盟店取消理由テキストボックスID</param>
    ''' <param name="ButtonSearch">検索ボタン</param>
    ''' <param name="dicRet">加盟店情報格納Dictionary</param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <returns>処理成否(True/False)</returns>
    ''' <remarks></remarks>
    Public Function CallKameitenSearchWindowCmn(ByVal sender As System.Object _
                                                 , ByVal strKbn As String _
                                                 , ByVal strKameitenCd As String _
                                                 , ByVal strKbnClId As String _
                                                 , ByVal strKameitenCdClId As String _
                                                 , ByVal strKameitenMeiClId As String _
                                                 , ByVal strTorikesiRiyuuClId As String _
                                                 , ByVal ButtonSearch As HtmlInputButton _
                                                 , ByRef dicRet As Dictionary(Of String, String) _
                                                 , Optional ByVal blnTorikesi As Boolean = False _
                                                 ) As Boolean

        Dim list As New List(Of KameitenSearchRecord)
        Dim kLogic As New KameitenSearchLogic
        Dim allRowCount As Integer = 0

        If strKameitenCd <> String.Empty Then
            list = kLogic.GetKameitenSearchResult(strKbn, _
                                                  strKameitenCd, _
                                                  blnTorikesi, _
                                                  allRowCount)

        End If

        If allRowCount = 1 Then
            Dim recData As KameitenSearchRecord = list(0)
            dicRet(strKameitenCdClId) = GetDisplayString(recData.KameitenCd)
            dicRet(strKameitenMeiClId) = GetDisplayString(recData.KameitenMei1)
            If strTorikesiRiyuuClId = "" Then
                dicRet(EarthConst.STYLE_FONT_COLOR) = Me.getKameitenFontColor(0)
            Else
                dicRet(strTorikesiRiyuuClId) = Me.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu)
                dicRet(EarthConst.STYLE_FONT_COLOR) = Me.getKameitenFontColor(recData.Torikesi)
            End If
            Return True
        Else
            '加盟店名、取消理由をクリア
            dicRet(strKameitenMeiClId) = String.Empty
            If strTorikesiRiyuuClId <> "" Then
                dicRet(strTorikesiRiyuuClId) = String.Empty
            End If

            'フォーカスセット
            Dim tmpFocusScript = "objEBI('" & ButtonSearch.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & strKbnClId & EarthConst.SEP_STRING _
                                       & strKameitenCdClId & "','" _
                                       & UrlConst.SEARCH_KAMEITEN & "','" _
                                       & strKameitenCdClId & EarthConst.SEP_STRING _
                                       & strKameitenMeiClId & "','" _
                                       & ButtonSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            Return False
        End If

    End Function
#End Region

#Region "営業所検索ポップアップ"
    ''' <summary>
    ''' 営業所検索画面呼出処理
    ''' </summary>
    ''' <param name="objPage">ページクラスオブジェクト</param>
    ''' <param name="TextCd">営業所コードテキストボックス</param>
    ''' <param name="TextMei">営業所名テキストボックス</param>
    ''' <param name="ButtonSearch">検索ボタン</param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <returns>処理成否(True/False)</returns>
    ''' <remarks></remarks>
    Public Function CallEigyousyoSearchWindow(ByVal sender As System.Object _
                                             , ByVal e As System.EventArgs _
                                             , ByVal objPage As Page _
                                             , ByVal TextCd As Object _
                                             , ByVal TextMei As Object _
                                             , ByVal ButtonSearch As Object _
                                             , Optional ByVal blnTorikesi As Boolean = False _
                                             ) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallKameitenSearchWindow" _
                                                    , objPage _
                                                    , TextCd _
                                                    , TextMei _
                                                    , ButtonSearch _
                                                    , blnTorikesi)

        Dim strEigyousyoCd As String = String.Empty  '営業所コード
        Dim strEigyousyoCdClId As String             '営業所コードID
        Dim strEigyousyoMeiClId As String            '営業所名ID
        Dim tmpScript As String = String.Empty

        '画面から加盟店情報を取得
        If TextCd.GetType.Name = "HtmlInputText" Then
            strEigyousyoCd = IIf(TextCd.Value <> String.Empty, TextCd.Value, String.Empty)
        ElseIf TextCd.GetType.Name = "TextBox" Then
            strEigyousyoCd = IIf(TextCd.Text <> String.Empty, TextCd.Text, String.Empty)
        End If
        strEigyousyoCdClId = TextCd.ClientID
        strEigyousyoMeiClId = TextMei.ClientID

        Dim dicEigyousyo As New Dictionary(Of String, String)
        dicEigyousyo.Add(strEigyousyoCdClId, strEigyousyoCd)
        dicEigyousyo.Add(strEigyousyoMeiClId, String.Empty)

        Dim blnResult As Boolean
        blnResult = CallEigyousyoSearchWindowCmn(sender _
                                                , strEigyousyoCd _
                                                , strEigyousyoCdClId _
                                                , strEigyousyoMeiClId _
                                                , ButtonSearch _
                                                , dicEigyousyo _
                                                , blnTorikesi)

        '営業所コード
        If TextCd.GetType.Name = "HtmlInputText" Then
            TextCd.Value = dicEigyousyo(strEigyousyoCdClId)
        ElseIf TextCd.GetType.Name = "TextBox" Then
            TextCd.Text = dicEigyousyo(strEigyousyoCdClId)
        End If
        '営業所名
        If TextMei.GetType.Name = "HtmlInputText" Then
            TextMei.Value = dicEigyousyo(strEigyousyoMeiClId)
        ElseIf TextMei.GetType.Name = "TextBox" Then
            TextMei.Text = dicEigyousyo(strEigyousyoMeiClId)
        End If

        Return blnResult

    End Function

    ''' <summary>
    ''' 営業所検索画面呼出処理［共通］
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strEigyousyoCdClId">営業所コードテキストボックスID</param>
    ''' <param name="strEigyousyoMeiClId">営業所名テキストボックスID</param>
    ''' <param name="ButtonSearch">検索ボタン</param>
    ''' <param name="dicRet">営業所情報格納Dictionary</param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <returns>処理成否(True/False)</returns>
    ''' <remarks></remarks>
    Public Function CallEigyousyoSearchWindowCmn(ByVal sender As System.Object _
                                                 , ByVal strEigyousyoCd As String _
                                                 , ByVal strEigyousyoCdClId As String _
                                                 , ByVal strEigyousyoMeiClId As String _
                                                 , ByVal ButtonSearch As HtmlInputButton _
                                                 , ByRef dicRet As Dictionary(Of String, String) _
                                                 , Optional ByVal blnTorikesi As Boolean = False _
                                                 ) As Boolean

        Dim list As New List(Of EigyousyoSearchRecord)
        Dim eLogic As New EigyousyoSearchLogic
        Dim allRowCount As Integer = 0

        If strEigyousyoCd <> String.Empty Then
            list = eLogic.GetEigyousyoSearchResult(strEigyousyoCd _
                                                   , String.Empty _
                                                   , blnTorikesi _
                                                   , allRowCount)

        End If

        If allRowCount = 1 Then
            Dim recData As EigyousyoSearchRecord = list(0)
            dicRet(strEigyousyoCdClId) = GetDisplayString(recData.EigyousyoCd)
            dicRet(strEigyousyoMeiClId) = GetDisplayString(recData.EigyousyoMei)

            Return True
        Else
            '営業所名をクリア
            dicRet(strEigyousyoMeiClId) = String.Empty

            'フォーカスセット
            Dim tmpFocusScript = "objEBI('" & ButtonSearch.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & strEigyousyoCdClId & "','" _
                                       & UrlConst.SEARCH_EIGYOUSYO & "','" _
                                       & strEigyousyoCdClId & EarthConst.SEP_STRING _
                                       & strEigyousyoMeiClId & "','" _
                                       & ButtonSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            Return False
        End If

    End Function
#End Region

    ''' <summary>
    ''' 参照モード取得メソッド
    ''' </summary>
    ''' <param name="strMode">モード設定（"1" or "1"以外)</param>
    ''' <param name="intKeiriKengen">経理権限</param>
    ''' <returns>strModeが"1"の場合、参照モード文字列／strModeが"1"以外の場合、ブランク</returns>
    ''' <remarks></remarks>
    Public Function GetViewMode(ByVal strMode As String, Optional ByVal intKeiriKengen As Integer = 0) As String
        Dim strViewMode As String

        If strMode = EarthConst.URIAGE_ZUMI_CODE Then
            strViewMode = EarthConst.MODE_VIEW
        Else
            strViewMode = String.Empty
        End If

        '経理権限がある場合は参照モードを未設定
        If intKeiriKengen = -1 Then
            strViewMode = String.Empty
        End If

        Return strViewMode

    End Function

    ''' <summary>
    ''' 請求種別(直接請求・他請求)を取得
    ''' </summary>
    ''' <param name="ctrlLink">請求先・仕入先リンク</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKojKaisyaCd">工事会社コード</param>
    ''' <returns>請求種別（"直接請求"/"他請求"）</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuTypeStr(ByVal ctrlLink As SeikyuuSiireLinkCtrl _
                                    , ByVal strKameitenCd As String _
                                    , ByVal strSyouhinCd As String _
                                    , Optional ByVal strKojKaisyaCd As String = "") As String
        Dim strSeikyuuSakiCd As String = ctrlLink.AccSeikyuuSakiCd.Value
        Dim strSeikyuuSakiBrc As String = ctrlLink.AccSeikyuuSakiBrc.Value
        Dim strSeikyuuSakiKbn As String = ctrlLink.AccSeikyuuSakiKbn.Value
        Dim strChkSeikyuuSaki As String
        Dim strSeikyuuType As String
        Dim dicSeikyuu As Dictionary(Of String, String)
        Dim cBizLogic As New CommonBizLogic

        If strSeikyuuSakiCd <> String.Empty And strSeikyuuSakiBrc <> String.Empty And strSeikyuuSakiKbn <> String.Empty Then
            If strKameitenCd = strSeikyuuSakiCd Then
                strSeikyuuType = EarthConst.SEIKYU_TYOKUSETU
            Else
                strSeikyuuType = EarthConst.SEIKYU_TASETU
            End If
            Return strSeikyuuType
            Exit Function
        End If

        '請求先の取得
        If strKojKaisyaCd = String.Empty Then
            dicSeikyuu = cBizLogic.getDefaultSeikyuuSaki(strKameitenCd, strSyouhinCd)
        Else
            dicSeikyuu = cBizLogic.getDefaultSeikyuuSaki(strKojKaisyaCd)
        End If
        strChkSeikyuuSaki = dicSeikyuu(cBizLogic.dicKeySeikyuuSakiCd)

        If strKameitenCd = strChkSeikyuuSaki Then
            strSeikyuuType = EarthConst.SEIKYU_TYOKUSETU
        Else
            strSeikyuuType = EarthConst.SEIKYU_TASETU
        End If

        Return strSeikyuuType

    End Function

#Region "ポップアップ起動処理"
    ''' <summary>
    ''' 商品4ポップアップ画面リンクパス生成
    ''' </summary>
    ''' <param name="btnPopup">商品4ボタン</param>
    ''' <param name="infLoginUser">ログインユーザ情報</param>
    ''' <param name="strKbnId">区分(コントロールID)</param>
    ''' <param name="strBangouId">保証書No(コントロールID)</param>
    ''' <param name="strKameiCdId">加盟店コード(コントロールID)</param>
    ''' <param name="strTysKaisyaCdId">調査会社コード(コントロールID)</param>
    ''' <remarks></remarks>
    Public Sub getSyouhin4MasterPath(ByVal btnPopup As HtmlInputButton _
                                    , ByVal infLoginUser As LoginUserInfo _
                                    , ByVal strKbnId As String _
                                    , ByVal strBangouId As String _
                                    , ByVal strKameiCdId As String _
                                    , ByVal strTysKaisyaCdId As String)

        btnPopup.Attributes("onclick") = "callModalSyouhin4('" & UrlConst.POPUP_SYOUHIN4 & "','" _
                                                               & strKbnId & "','" _
                                                               & strBangouId & "','" _
                                                               & strKameiCdId & "','" _
                                                               & strTysKaisyaCdId & "');"


    End Sub

    ''' <summary>
    ''' 特別対応ポップアップ画面リンクパス生成
    ''' </summary>
    ''' <param name="btnPopup">特別対応ボタン</param>
    ''' <param name="infLoginUser">ログインユーザ情報</param>
    ''' <param name="strKbnId">区分(コントロールID)</param>
    ''' <param name="strBangouId">保証書No(コントロールID)</param>
    ''' <param name="strKameiCdId">加盟店コード(コントロールID)</param>
    ''' <param name="strTysHouhouNoId">調査方法Noコード(コントロールID)</param>
    ''' <param name="strTysSyouhinCdId">商品コード(コントロールID)</param>
    ''' <remarks></remarks>
    Public Sub getTokubetuTaiouLinkPath(ByVal btnPopup As HtmlInputButton _
                                    , ByVal infLoginUser As LoginUserInfo _
                                    , ByVal strKbnId As String _
                                    , ByVal strBangouId As String _
                                    , ByVal strKameiCdId As String _
                                    , ByVal strTysHouhouNoId As String _
                                    , ByVal strTysSyouhinCdId As String)

        btnPopup.Attributes("onclick") = "callModalTokubetuTaiou('" & UrlConst.POPUP_TOKUBETU_TAIOU & "','" _
                                                                   & strKbnId & "','" _
                                                                   & strBangouId & "','" _
                                                                   & strKameiCdId & "','" _
                                                                   & strTysHouhouNoId & "','" _
                                                                   & strTysSyouhinCdId & "');"

    End Sub

    ''' <summary>
    ''' 特別対応ポップアップ画面リンクパス生成(受注・邸別修正画面用)
    ''' </summary>
    ''' <param name="btnPopup">特別対応ボタン</param>
    ''' <param name="infLoginUser">ログインユーザ情報</param>
    ''' <param name="strKbnId">区分(コントロールID)</param>
    ''' <param name="strBangouId">保証書No(コントロールID)</param>
    ''' <param name="strKameiCdId">加盟店コード(コントロールID)</param>
    ''' <param name="strTysHouhouNoId">調査方法Noコード(コントロールID)</param>
    ''' <param name="strTysSyouhinCdId">商品コード(コントロールID)</param>
    ''' <param name="strHdnKakuteiValue"></param>
    ''' <param name="strBtnTokubetu"></param>
    ''' <param name="emType">特別対応検索タイプ</param>
    ''' <param name="strKkkHaneiFlg">特別対応価格反映用フラグ</param>
    ''' <param name="strChgTokuCd">特別対応更新対象コード</param>
    ''' <param name="strRentouBukkenSuu">連棟物件数</param>
    ''' <param name="strBtnReloadId">画面再描画用HiddenボタンID</param>
    ''' <remarks></remarks>
    Public Sub getTokubetuTaiouLinkPathJT(ByVal btnPopup As HtmlInputButton _
                                    , ByVal infLoginUser As LoginUserInfo _
                                    , ByVal strKbnId As String _
                                    , ByVal strBangouId As String _
                                    , ByVal strKameiCdId As String _
                                    , ByVal strTysHouhouNoId As String _
                                    , ByVal strTysSyouhinCdId As String _
                                    , ByVal strHdnKakuteiValue As String _
                                    , ByVal strBtnTokubetu As String _
                                    , ByVal emType As EarthEnum.emTokubetuTaiouSearchType _
                                    , Optional ByVal strKkkHaneiFlg As String = "" _
                                    , Optional ByVal strChgTokuCd As String = "" _
                                    , Optional ByVal strRentouBukkenSuu As String = "" _
                                    , Optional ByVal strBtnReloadId As String = "")

        If emType = EarthEnum.emTokubetuTaiouSearchType.TeibetuSyuusei Then

            btnPopup.Attributes("onclick") = "callModalTokubetuTaiouJT('" & UrlConst.POPUP_TOKUBETU_TAIOU & "','" _
                                                                       & strKbnId & "','" _
                                                                       & strBangouId & "','" _
                                                                       & strKameiCdId & "','" _
                                                                       & strTysHouhouNoId & "','" _
                                                                       & strTysSyouhinCdId & "','" _
                                                                       & "" & "','" _
                                                                       & "" & "','" _
                                                                       & "" & "','" _
                                                                       & "" & "','" _
                                                                       & strChgTokuCd & "','" _
                                                                       & emType & "','" _
                                                                       & strKkkHaneiFlg & "','" _
                                                                       & strRentouBukkenSuu & "','" _
                                                                       & strHdnKakuteiValue & "','" _
                                                                       & strBtnTokubetu & "','" _
                                                                       & strBtnReloadId & "');"
        Else
            btnPopup.Attributes("onclick") = "callModalTokubetuTaiouJT('" & UrlConst.POPUP_TOKUBETU_TAIOU & "','" _
                                                                & strKbnId & "','" _
                                                                & strBangouId & "','" _
                                                                & strKameiCdId & "','" _
                                                                & strTysHouhouNoId & "','" _
                                                                & strTysSyouhinCdId & "'," _
                                                                & "createPrm(1)," _
                                                                & "createPrm(2)," _
                                                                & "createPrm(3)," _
                                                                & "createPrm(4),'" _
                                                                & strChgTokuCd & "','" _
                                                                & emType & "','" _
                                                                & strKkkHaneiFlg & "','" _
                                                                & strRentouBukkenSuu & "','" _
                                                                & strHdnKakuteiValue & "','" _
                                                                & strBtnTokubetu & "','" _
                                                                & strBtnReloadId & "');"
        End If

    End Sub

#End Region

    ''' <summary>
    ''' 商品コード、商品分類タイプ、加盟店コードと請求先情報を元に請求先タイプを取得する
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="emType">商品分類タイプ</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakibrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <returns>直接請求,他請求,""(空白)</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSakiTypeStr( _
                                            ByVal strSyouhinCd As String, _
                                            ByVal emType As EarthEnum.EnumSyouhinKubun, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strSeikyuuSakiCd As String, _
                                            ByVal strSeikyuuSakibrc As String, _
                                            ByVal strSeikyuuSakiKbn As String _
                                        ) As String

        Dim JibanLogic As New JibanLogic
        '請求先タイプ
        Dim strSeikyuuSakiType As String = EarthConst.SEIKYU_TASETU '商品が取れない場合に、"他請求"をデフォルトとする

        '請求先タイプの自動設定
        Dim syouhinRec As Syouhin23Record

        If strSyouhinCd = String.Empty Or strKameitenCd = String.Empty Then
            Return strSeikyuuSakiType
            Exit Function
        End If

        syouhinRec = JibanLogic.GetSyouhinInfo(strSyouhinCd, emType, strKameitenCd)
        If Not syouhinRec Is Nothing Then
            '画面上で請求先が指定されている場合、レコードの請求先を上書き
            If strSeikyuuSakiCd <> String.Empty Then
                '請求先をレコードにセット
                syouhinRec.SeikyuuSakiCd = strSeikyuuSakiCd
                syouhinRec.SeikyuuSakiBrc = strSeikyuuSakibrc
                syouhinRec.SeikyuuSakiKbn = strSeikyuuSakiKbn
            End If
            strSeikyuuSakiType = syouhinRec.SeikyuuSakiType '●請求先
        End If

        Return strSeikyuuSakiType
    End Function

    ''' <summary>
    ''' 発注停止フラグチェック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strOrderStopFlg">発注停止フラグ</param>
    ''' <remarks></remarks>
    Public Sub chkOrderStopFlg(ByVal sender As System.Object, ByVal strOrderStopFlg As String, ByRef strKameitenCd As String, ByRef strSaveCdOrderStop As String)
        Dim mLogic As New MessageLogic

        '発注停止チェック
        If EarthConst.Instance.HATTYUU_TEISI_FLGS.ContainsKey(strOrderStopFlg) Then
            mLogic.AlertMessage(sender, Messages.MSG166E)
            '退避していた加盟店コードを入れ直す
            strKameitenCd = strSaveCdOrderStop
            strSaveCdOrderStop = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 画面のHidden項目に排他用の更新日時(更新日時がない場合は登録日時)をセット
    ''' </summary>
    ''' <param name="dtAddDateTime">登録日時</param>
    ''' <param name="dtUpdDateTime">更新日時</param>
    ''' <param name="objHdnUpdDateTime">排他制御用更新日時格納Hiddenコントロール</param>
    ''' <param name="strDateFormat">日付フォーマット文字列</param>
    ''' <remarks></remarks>
    Public Sub setDispHaitaUpdTime(ByVal dtAddDateTime As DateTime, ByVal dtUpdDateTime As DateTime, ByVal objHdnUpdDateTime As Object, Optional ByVal strDateFormat As String = EarthConst.FORMAT_DATE_TIME_1)
        If dtUpdDateTime = DateTime.MinValue Then
            If dtAddDateTime = DateTime.MinValue Then
                objHdnUpdDateTime.Value = String.Empty
            Else
                objHdnUpdDateTime.Value = dtAddDateTime.ToString(strDateFormat)
            End If
        Else
            objHdnUpdDateTime.Value = dtUpdDateTime.ToString(strDateFormat)
        End If
    End Sub

    ''' <summary>
    ''' 画面のHidden項目から排他用の更新日時をDateTime型で取得
    ''' </summary>
    ''' <param name="objHdnDateTime">排他制御用更新日時格納Hiddenコントロール</param>
    ''' <param name="strDateFormat">日付フォーマット文字列</param>
    ''' <returns>更新日時</returns>
    ''' <remarks></remarks>
    Public Function getDispHaitaUpdTime(ByVal objHdnDateTime As Object, Optional ByVal strDateFormat As String = EarthConst.FORMAT_DATE_TIME_1) As DateTime
        Dim dtUpdDateTime As DateTime

        If objHdnDateTime.value = String.Empty Then
            dtUpdDateTime = DateTime.MinValue
        Else
            dtUpdDateTime = DateTime.ParseExact(objHdnDateTime.value, strDateFormat, Nothing)
        End If

        Return dtUpdDateTime
    End Function

#Region "月次確定予約管理テーブルの処理状況を取得"
    ''' <summary>
    ''' 月次確定予約管理テーブルの処理状況を取得
    ''' </summary>
    ''' <returns>True:当月のみ可,False:当月以前も可</returns>
    ''' <remarks></remarks>
    Public Function GetGetujiYoyakuJyky() As Boolean
        '月次確定予約管理テーブルの処理状況を取得(伝票売上年月日チェック用)
        Dim clsUpdLogic As New GetujiIkkatuUpdateLogic
        Dim targetYM As New DateTime
        Dim flgGetujiKakuteiYoyakuzumi As Boolean = False
        targetYM = Today.Year.ToString() & "/" & Today.Month.ToString("00") & "/01" '当月初日
        targetYM = targetYM.AddDays(-1)    '先月末日
        Dim syoriJoukyou As Object = clsUpdLogic.GetGetujiKakuteiYoyakuData(targetYM)
        If syoriJoukyou Is Nothing OrElse syoriJoukyou = 0 Then
            flgGetujiKakuteiYoyakuzumi = False
        Else
            flgGetujiKakuteiYoyakuzumi = True
        End If

        Return flgGetujiKakuteiYoyakuzumi
    End Function
#End Region

    ''' <summary>
    ''' 伝票売上年月日の月次確定チェック
    ''' </summary>
    ''' <param name="strTextDenUriDate">伝票売上年月日</param>
    ''' <returns>チェック結果(True：OK／False：NG)</returns>
    ''' <remarks>月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー</remarks>
    Public Function chkGetujiKakuteiYoyakuzumi(ByVal strTextDenUriDate As String, _
                                               ByRef dtGetujiKakuteiLastSyoriDate As DateTime) As Boolean
        Dim clsUpdLogic As New GetujiIkkatuUpdateLogic
        dtGetujiKakuteiLastSyoriDate = clsUpdLogic.getGetujiKakuteiLastSyoriDate()

        '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
        If dtGetujiKakuteiLastSyoriDate <> Date.MinValue AndAlso strTextDenUriDate <> String.Empty Then
            Try
                Dim dtDenUriDate As Date = Date.Parse(strTextDenUriDate)
                If dtDenUriDate.Year.ToString & dtDenUriDate.Month.ToString("00") <= dtGetujiKakuteiLastSyoriDate.Year.ToString & dtGetujiKakuteiLastSyoriDate.Month.ToString("00") Then
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End If

        Return True
    End Function

    ''' <summary>
    ''' 赤字太字を設定する
    ''' </summary>
    ''' <param name="objStyle">各コントロールのStyle</param>
    ''' <param name="blnSet">True：赤字・太字／False:通常フォント</param>
    ''' <remarks></remarks>
    Public Sub setStyleRedBold(ByRef objStyle As CssStyleCollection, ByVal blnSet As Boolean)
        If blnSet Then
            objStyle.Item(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            objStyle.Item(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
        Else
            objStyle.Item(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_NORMAL
            objStyle.Item(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLACK
        End If
    End Sub

    ''' <summary>
    ''' 青字太字を設定する
    ''' </summary>
    ''' <param name="objStyle">各コントロールのStyle</param>
    ''' <param name="blnSet">True：青字・太字／False:通常フォント</param>
    ''' <remarks></remarks>
    Public Sub setStyleBlueBold(ByRef objStyle As CssStyleCollection, ByVal blnSet As Boolean)
        If blnSet Then
            objStyle.Item(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            objStyle.Item(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLUE
        Else
            objStyle.Item(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_NORMAL
            objStyle.Item(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLACK
        End If
    End Sub

    ''' <summary>
    ''' 文字色を変更する
    ''' </summary>
    ''' <param name="objStyle">各コントロールのStyle</param>
    ''' <param name="strFontColor">設定色</param>
    ''' <remarks></remarks>
    Public Sub setStyleFontColor(ByRef objStyle As CssStyleCollection, ByVal strFontColor As String)

        '文字色を変更
        objStyle.Item(EarthConst.STYLE_FONT_COLOR) = strFontColor

    End Sub

#Region "変更箇所項目対応(※邸別データ修正画面)"
    ''' <summary>
    ''' ディクショナリとKEY値を元に、該当箇所の背景色を赤色に変更する
    ''' ※Hidden以外
    ''' </summary>
    ''' <param name="dic">ディクショナリ</param>
    ''' <param name="strKey">KEY値</param>
    ''' <remarks></remarks>
    Public Sub ChgHenkouCtrlBgColor(ByVal dic As Dictionary(Of String, Object), ByVal strKey As String)
        Dim objRet As New Object

        If Not dic Is Nothing Then
            '背景色変更処理
            If dic.ContainsKey(strKey) Then
                objRet = dic(strKey)
                If Not objRet Is Nothing Then
                    Dim strTmpId As String = objRet.GetType.Name
                    If strTmpId = "HtmlInputHidden" _
                        OrElse strTmpId = "HiddenField" Then 'Hiddenは対象外
                    Else
                        objRet.Style("background-color") = "red"
                    End If
                End If
            End If
        End If

    End Sub
#End Region

#Region "物件進捗状況"
    ''' <summary>
    ''' 物件進捗状況マスタリンクパス生成
    ''' </summary>
    ''' <param name="btnPopup">物件進捗状況ボタン</param>
    ''' <param name="infLoginUser">ログインユーザ情報</param>
    ''' <param name="strKbnId">区分(コントロールID)</param>
    ''' <param name="strBangouId">保証書No(コントロールID)</param>
    ''' <remarks></remarks>
    Public Sub getBukkenJykyMasterPath(ByVal btnPopup As HtmlInputButton _
                                    , ByVal infLoginUser As LoginUserInfo _
                                    , ByVal strKbnId As String _
                                    , ByVal strBangouId As String)

        btnPopup.Attributes("onclick") = "callSearch('" & strKbnId & EarthConst.SEP_STRING & strBangouId & "','" _
                                                        & UrlConst.POPUP_Bukken_SINTYOKU_JYKY & "','" _
                                                        & "','');"


    End Sub
#End Region

#Region "保証書関連対応"
    ''' <summary>
    ''' 物件毎に保証書管理状況を判断して、保証書管理T.物件状況に設定する値を返す
    ''' </summary>
    ''' <param name="jibanRec">地盤レコードクラス(値セット済みのこと)</param>
    ''' <remarks></remarks>
    Public Function ChkHosyousyoBukkenJyky(ByVal jibanRec As JibanRecordBase) As Integer

        Dim intRet As Integer = Integer.MinValue

        Dim cbLogic As New CommonBizLogic
        Dim jLogic As New JibanLogic
        Dim BJykyLogic As New BukkenSintyokuJykyLogic

        '保証有無(未発行リスト印字有無)
        Dim strHosyouUmu As String = cbLogic.GetHosyousyoHakJykyHosyouUmu(Me.GetDispStr(jibanRec.HosyousyoHakJyky))
        '地盤レコードOld
        Dim jibanRecOld As JibanRecordBase = jLogic.GetJibanData(jibanRec.Kbn, jibanRec.HosyousyoNo)

        Dim HosyouRec As New HosyousyoKanriRecord
        Dim sender As New Object
        HosyouRec = BJykyLogic.getSearchKeyDataRec(sender, jibanRecOld.Kbn, jibanRec.HosyousyoNo)

        '保証書管理T.物件状況Oldが未設定の場合
        If HosyouRec.BukkenJyky = Integer.MinValue Then

            '地盤T.保証書発行日Old
            If jibanRecOld.HosyousyoHakDate <> DateTime.MinValue Then
                intRet = 3

            Else
                '地盤T.データ破棄種別(未設定時:0)
                If jibanRec.DataHakiSyubetu <> 0 Then
                    intRet = 0

                ElseIf strHosyouUmu = "0" Then '保証書発行状況.保証有無
                    intRet = 0

                ElseIf Not jibanRecOld.KaiyakuHaraimodosiRecord Is Nothing Then '解約払戻の邸別請求レコードがある場合
                    intRet = 0

                Else
                    '保証書管理データを判定
                    If cbLogic.GetHosyousyoKanriJyky(jibanRec.Kbn, jibanRec.HosyousyoNo) = 1 Then
                        intRet = 1
                    Else
                        intRet = 2
                    End If
                End If
            End If

        Else
            intRet = HosyouRec.BukkenJyky
        End If

        Return intRet
    End Function
#End Region

#Region "保証商品有無"

    ''' <summary>
    ''' 保証商品の表示切替
    ''' ※保証あり："あり"(青字太字)
    ''' ※保証なし："なし"(赤字太字)
    ''' </summary>
    ''' <param name="strHosyouSyouhinUmu">保証商品有無(1:あり、以外:なし)</param>
    ''' <param name="txtTarget">対象テキストボックス</param>
    ''' <remarks></remarks>
    Public Sub ChgDispHosyouSyouhin(ByVal strHosyouSyouhinUmu As String, ByRef txtTarget As HtmlInputText)
        If strHosyouSyouhinUmu = "1" Then '有
            txtTarget.Value = EarthConst.ARI_HIRAGANA 'あり
            '青字
            txtTarget.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLUE
        Else
            txtTarget.Value = EarthConst.NASI_HIRAGANA 'なし
            '赤字
            txtTarget.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
        End If
        '太字
        txtTarget.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD

    End Sub

#End Region

#Region "画面表示切替[共通]"

    ''' <summary>
    ''' 指定値選択時の動き(画面表示時用)
    ''' ※構造、予定基礎、立会者　等
    ''' ※ドロップダウンリスト、チェックボックス、ラジオボタン
    ''' </summary>
    ''' <param name="checkVal">チェック元コントロールの有効値</param>
    ''' <param name="pull">チェック元コントロール（プルダウン）</param>
    ''' <param name="arrSonota">チェック元コントロールが有効値だった場合に表示/非表示を行うコントロール郡</param>
    ''' <remarks></remarks>
    Public Sub CheckVisible(ByVal checkVal As String, _
                             ByVal pull As Object, _
                             ByVal arrSonota As ArrayList)

        Dim arrChkVal() As String = Split(checkVal, EarthConst.SEP_STRING)
        Dim intCnt As Integer = 0
        Dim strObjTmpVal As String = String.Empty

        Select Case pull.GetType.Name
            Case "DropDownList" 'ドロップダウンリスト
                strObjTmpVal = pull.SelectedValue

            Case "HtmlInputCheckBox", "CheckBox", "HtmlInputRadioButton" 'チェックボックス,INPUTラジオボタン
                strObjTmpVal = pull.checked

            Case Else
                Exit Sub
        End Select

        'コントロール分ループ
        For Each sonota As Object In arrSonota
            '指定値が複数ある場合のループ
            For intCnt = 0 To arrChkVal.Length - 1
                '表示対象が見つかった場合、処理を抜ける
                If strObjTmpVal = arrChkVal(intCnt) Then
                    sonota.Style("visibility") = "visible"
                    Exit For
                Else
                    sonota.Style("visibility") = "hidden"
                End If
            Next
        Next

    End Sub

#Region "調査立会者コード"

    ''' <summary>
    ''' 立会者コードから画面項目への反映処理
    ''' </summary>
    ''' <param name="tatiaiCd">立会者コード</param>
    ''' <param name="CheckTTSesyuSama">施主様</param>
    ''' <param name="CheckTTTantousya">担当者</param>
    ''' <param name="CheckTTSonota">その他</param>
    ''' <remarks></remarks>
    Public Sub SetTatiaiCd( _
                            ByVal tatiaiCd As Integer _
                            , ByRef CheckTTSesyuSama As HtmlInputCheckBox _
                            , ByRef CheckTTTantousya As HtmlInputCheckBox _
                            , ByRef CheckTTSonota As HtmlInputCheckBox _
                            )

        Dim tSesyu As Integer = CheckTTSesyuSama.Value
        Dim tTant As Integer = CheckTTTantousya.Value
        Dim tOther As Integer = CheckTTSonota.Value
        Dim tSesyuTant As Integer = tSesyu + tTant
        Dim tSesyuOther As Integer = tSesyu + tOther
        Dim tTantOther As Integer = tTant + tOther
        Dim zAll As Integer = tSesyu + tTant + tOther

        CheckTTSesyuSama.Checked = False
        CheckTTTantousya.Checked = False
        CheckTTSonota.Checked = False

        Select Case tatiaiCd
            Case tSesyu
                CheckTTSesyuSama.Checked = True
            Case tTant
                CheckTTTantousya.Checked = True
            Case tOther
                CheckTTSonota.Checked = True
            Case tSesyuTant
                CheckTTSesyuSama.Checked = True
                CheckTTTantousya.Checked = True
            Case tTantOther
                CheckTTTantousya.Checked = True
                CheckTTSonota.Checked = True
            Case tSesyuOther
                CheckTTSesyuSama.Checked = True
                CheckTTSonota.Checked = True
            Case zAll
                CheckTTSesyuSama.Checked = True
                CheckTTTantousya.Checked = True
                CheckTTSonota.Checked = True

        End Select

    End Sub

    ''' <summary>
    ''' 画面項目から立会者コードへの変換処理
    ''' </summary>
    ''' <param name="CheckTTSesyuSama">施主様</param>
    ''' <param name="CheckTTTantousya">担当者</param>
    ''' <param name="CheckTTSonota">その他</param>
    ''' <returns>立会者コード</returns>
    ''' <remarks></remarks>
    Public Function GetTatiaiCd( _
                            ByRef CheckTTSesyuSama As HtmlInputCheckBox _
                            , ByRef CheckTTTantousya As HtmlInputCheckBox _
                            , ByRef CheckTTSonota As HtmlInputCheckBox _
                            ) As Integer

        Dim tmpCd As Integer = 0

        If CheckTTSesyuSama.Checked Then
            tmpCd = CheckTTSesyuSama.Value
        End If
        If CheckTTTantousya.Checked Then
            tmpCd = tmpCd + CheckTTTantousya.Value
        End If
        If CheckTTSonota.Checked Then
            tmpCd = tmpCd + CheckTTSonota.Value
        End If

        Return tmpCd
    End Function

#End Region

#End Region

#Region "経理追加対応"
    ''' <summary>
    ''' システム日付を元に、年度始め日付を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTermFirstDate(ByVal dteNow As DateTime) As DateTime

        Dim strTermFirstDate As String
        Dim dteTermFirstDate As DateTime
        Dim intMonth As Integer = dteNow.Month

        If intMonth <= 3 Then   '3月以前
            strTermFirstDate = CStr(dteNow.Year - 1) & EarthConst.TERM_FIRST_DATE
        Else                    '4月以降
            strTermFirstDate = CStr(dteNow.Year) & EarthConst.TERM_FIRST_DATE
        End If

        dteTermFirstDate = CDate(strTermFirstDate)

        Return dteTermFirstDate
    End Function

#End Region

#Region "調査方法のドロップダウンリスト表示処理"

    ''' <summary>
    ''' 調査方法のドロップダウンリスト表示処理
    ''' </summary>
    ''' <param name="intTysHouhou">画面にセットする調査方法NO</param>
    ''' <param name="objSelectTysHouhou">セット対象のドロップダウンリスト</param>
    ''' <param name="objTextTysHouhou">セット対象のテキストボックス</param>
    ''' <remarks></remarks>
    Public Sub ps_SetSelectTextBoxTysHouhou(ByVal intTysHouhou As Integer, ByRef objSelectTysHouhou As DropDownList, ByVal withCode As Boolean, Optional ByRef objTextTysHouhou As TextBox = Nothing)
        Dim JLogic As New JibanLogic
        Dim strTysHouhou As String = String.Empty

        strTysHouhou = Me.GetDisplayString(intTysHouhou)

        '調査方法
        If Me.ChkDropDownList(objSelectTysHouhou, strTysHouhou) Then
        ElseIf strTysHouhou <> String.Empty Then
            'DDLになければ、アイテムを追加
            Dim recTysHouhou As New TyousahouhouRecord
            recTysHouhou = JLogic.getTyousahouhouRecord(CInt(strTysHouhou))
            If withCode Then
                objSelectTysHouhou.Items.Add(New ListItem(recTysHouhou.TysHouhouNo & ":" & recTysHouhou.TysHouhouMei, recTysHouhou.TysHouhouNo))
            Else
                objSelectTysHouhou.Items.Add(New ListItem(recTysHouhou.TysHouhouMei, recTysHouhou.TysHouhouNo))
            End If
        End If

        '選択状態
        objSelectTysHouhou.SelectedValue = strTysHouhou

        If Not objTextTysHouhou Is Nothing Then
            objTextTysHouhou.Text = objSelectTysHouhou.SelectedItem.Text
        End If

    End Sub

#End Region

#Region "加盟店名表示切替"
    ''' <summary>
    ''' 画面表示用.取消理由を取得
    ''' </summary>
    ''' <param name="intTorikesi">取消(コード)</param>
    ''' <param name="strMeisyou">拡張名称マスタ.名称</param>
    ''' <param name="withCode">テキストにValue値＋"："をつける場合:true</param>
    ''' <param name="blnTorikesiRow">"0:設定なし"を表示する場合:true</param>
    ''' <returns>取消理由</returns>
    ''' <remarks></remarks>
    Public Function getTorikesiRiyuu(ByVal intTorikesi As Integer _
                                    , ByVal strMeisyou As String _
                                    , Optional ByVal withCode As Boolean = True _
                                    , Optional ByVal blnTorikesiRow As Boolean = False) As String

        Dim strTorikesiRiyuu As String                  '取消理由
        Dim strTorikesi As String                       '取消

        strTorikesi = Me.GetDisplayString(intTorikesi)

        If intTorikesi = 0 AndAlso blnTorikesiRow = False Then
            Return String.Empty
        End If

        If withCode Then
            strTorikesiRiyuu = strTorikesi & "：" & strMeisyou
        Else
            strTorikesiRiyuu = strMeisyou
        End If

        Return strTorikesiRiyuu
    End Function

    ''' <summary>
    ''' 画面表示用.文字色を判定
    ''' </summary>
    ''' <param name="intTorikesi">取消</param>
    ''' <returns>文字色</returns>
    ''' <remarks></remarks>
    Public Function getKameitenFontColor(ByVal intTorikesi As Integer) As String
        Dim strFontColor As String

        If intTorikesi > 0 Then
            strFontColor = EarthConst.STYLE_COLOR_RED
        Else
            strFontColor = EarthConst.STYLE_COLOR_BLACK
        End If

        Return strFontColor
    End Function

    ''' <summary>
    ''' 加盟店取消理由の判定および設定処理
    ''' </summary>
    ''' <param name="strSeikyuuSakiKbn">請求先区分(Value値)</param>
    ''' <param name="strKameitenCd">加盟店コード(Value値)</param>
    ''' <param name="objTorikesi">取消理由(オブジェクト)</param>
    ''' <param name="withCode">コード表示の有無</param>
    ''' <param name="blnTorikesi">取消レコードの検索可否</param>
    ''' <param name="objArray"></param>
    ''' <remarks></remarks>
    Public Sub GetKameitenTorikesiRiyuuMain(ByVal strSeikyuuSakiKbn As String _
                                        , ByVal strKameitenCd As String _
                                        , ByVal objTorikesi As Object _
                                        , Optional ByVal withCode As Boolean = True _
                                        , Optional ByVal blnTorikesi As Boolean = False _
                                        , Optional ByVal objArray() As Object = Nothing)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenTorikesiRiyuuMain" _
                                                    , strSeikyuuSakiKbn _
                                                    , strKameitenCd _
                                                    , objTorikesi _
                                                    , withCode _
                                                    , blnTorikesi _
                                                    , objArray)

        'コントロール格納用オブジェクト
        Dim objTmpCtrl As New Object

        '請求先区分が加盟店の場合、取消理由を取得設定
        If Not String.IsNullOrEmpty(strSeikyuuSakiKbn) AndAlso strSeikyuuSakiKbn = EarthConst.SEIKYUU_SAKI_KAMEI Then
            '加盟店取消理由取得設定処理
            GetKameitenTorikesiRiyuu(String.Empty, strKameitenCd, objTorikesi, withCode, blnTorikesi, objArray)
        Else
            '色付対象オブジェクトを標準色(黒)に戻す
            If Not objArray Is Nothing Then
                For Each objTmpCtrl In objArray
                    setStyleFontColor(objTmpCtrl.Style, EarthConst.STYLE_COLOR_BLACK)
                Next
            End If
            '取消理由をクリア
            If objTorikesi.GetType.Name = "HtmlInputText" Then
                objTorikesi.Value = String.Empty
            ElseIf objTorikesi.GetType.Name = "TextBox" Then
                objTorikesi.Text = String.Empty
            End If
        End If

    End Sub

    ''' <summary>
    ''' 加盟店取消理由設定処理
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="TextTorikesi">取消理由テキストボックス</param>
    ''' <param name="withCode">テキストにValue値＋"："をつける場合:true</param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <param name="objArray">色替え対象コントロールの配列</param>
    ''' <remarks></remarks>
    Public Sub GetKameitenTorikesiRiyuu(ByVal strKbn As String _
                                        , ByVal strKameitenCd As String _
                                        , ByVal TextTorikesi As Object _
                                        , Optional ByVal withCode As Boolean = True _
                                        , Optional ByVal blnTorikesi As Boolean = False _
                                        , Optional ByVal objArray() As Object = Nothing)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenTorikesiRiyuu" _
                                                    , strKbn _
                                                    , strKameitenCd _
                                                    , TextTorikesi _
                                                    , withCode _
                                                    , blnTorikesi _
                                                    , objArray)

        Dim recData As New KameitenSearchRecord
        Dim kLogic As New KameitenSearchLogic
        Dim allRowCount As Integer = 0
        Dim strTorikesiRiyuu As String
        Dim strFontColor As String
        Dim tmpCtrl As Object
        Dim intCnt As Integer = 0

        recData = kLogic.GetKameitenRecord(strKbn _
                                           , strKameitenCd _
                                           , blnTorikesi)

        If Not recData Is Nothing AndAlso recData.Torikesi > 0 Then
            strTorikesiRiyuu = Me.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu, withCode, False)  '取消理由
            strFontColor = Me.getKameitenFontColor(recData.Torikesi)                                    '文字色
        Else
            strTorikesiRiyuu = String.Empty             '取消理由
            strFontColor = Me.getKameitenFontColor(0)   '文字色
        End If

        '取消理由を設定
        If TextTorikesi.GetType.Name = "HtmlInputText" Then
            TextTorikesi.Value = strTorikesiRiyuu
        ElseIf TextTorikesi.GetType.Name = "TextBox" Then
            TextTorikesi.Text = strTorikesiRiyuu
        End If

        '取消理由の色替え処理
        setStyleFontColor(TextTorikesi.Style, strFontColor)

        'その他項目の色替え処理
        If Not objArray Is Nothing Then
            For Each tmpCtrl In objArray
                setStyleFontColor(tmpCtrl.Style, strFontColor)
            Next
        End If

    End Sub



#End Region
End Class
