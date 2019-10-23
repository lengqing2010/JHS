
Partial Public Class TeibetuKyoutuuCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim cLogic As New CommonLogic

#Region "プロパティ"
#Region "共通情報"
    ''' <summary>
    ''' 共通情報
    ''' </summary>
    ''' <value></value>
    ''' <returns> 共通情報リンクID</returns>
    ''' <remarks></remarks>
    Public Property KyoutuuInfo() As HtmlGenericControl
        Get
            Return TBodyKyotuInfo
        End Get
        Set(ByVal value As HtmlGenericControl)
            TBodyKyotuInfo = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kubun() As String
        Get
            Return TextKubun.Value
        End Get
        Set(ByVal value As String)
            TextKubun.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 番号（旧保証書NO）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bangou() As String
        Get
            Return TextBangou.Value
        End Get
        Set(ByVal value As String)
            TextBangou.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 備考１
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bikou1() As String
        Get
            Return TextBikou1.Value
        End Get
        Set(ByVal value As String)
            TextBikou1.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 備考２
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bikou2() As String
        Get
            Return TextBikou2.Value
        End Get
        Set(ByVal value As String)
            TextBikou2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sesyumei() As String
        Get
            Return TextSesyuMei.Value
        End Get
        Set(ByVal value As String)
            TextSesyuMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 物件住所１
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo1() As String
        Get
            Return TextJyuusyo1.Value
        End Get
        Set(ByVal value As String)
            TextJyuusyo1.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 物件住所２
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo2() As String
        Get
            Return TextJyuusyo2.Value
        End Get
        Set(ByVal value As String)
            TextJyuusyo2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 物件住所３
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo3() As String
        Get
            Return TextJyuusyo3.Value
        End Get
        Set(ByVal value As String)
            TextJyuusyo3.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 調査実施日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaJissibi() As String
        Get
            Return TextTyousaJissibi.Value
        End Get
        Set(ByVal value As String)

            If Not value Is Nothing Then
                If value.Trim() <> "" Then
                    value = IIf(DateTime.Parse(value) = DateTime.MinValue, "", DateTime.Parse(value).ToString("yyyy/MM/dd"))
                End If
            End If

            TextTyousaJissibi.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 改良工事実施日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KairyouKoujiJissibi() As Date
        Get
            Return Date.Parse(TextKairyouKoujiJissibi.Value)
        End Get
        Set(ByVal value As Date)

            If value = Date.MinValue Then
                TextKairyouKoujiJissibi.Value = ""
            Else
                TextKairyouKoujiJissibi.Value = value.ToString("yyyy/MM/dd")
            End If

        End Set
    End Property

    ''' <summary>
    ''' 改良工事完工速報着日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KairyouKoujiKankou() As Date
        Get
            Return Date.Parse(TextKairyouKoujiKankou.Value)
        End Get
        Set(ByVal value As Date)

            If value = Date.MinValue Then
                TextKairyouKoujiKankou.Value = ""
            Else
                TextKairyouKoujiKankou.Value = value.ToString("yyyy/MM/dd")
            End If

        End Set
    End Property

    ''' <summary>
    ''' 追加工事実施日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TuikaKoujiJissibi() As Date
        Get
            Return Date.Parse(TextTuikaKoujiJissibi.Value)
        End Get
        Set(ByVal value As Date)

            If value = Date.MinValue Then
                TextTuikaKoujiJissibi.Value = ""
            Else
                TextTuikaKoujiJissibi.Value = value.ToString("yyyy/MM/dd")
            End If

        End Set
    End Property

    ''' <summary>
    ''' 追加工事完工速報着日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TuikaKoujiKankou() As Date
        Get
            Return Date.Parse(TextTuikaKoujiKankou.Value)
        End Get
        Set(ByVal value As Date)

            If value = Date.MinValue Then
                TextTuikaKoujiKankou.Value = ""
            Else
                TextTuikaKoujiKankou.Value = value.ToString("yyyy/MM/dd")
            End If

        End Set
    End Property

    ''' <summary>
    ''' 解析担当者コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KaisekiTantouCd() As String
        Get
            Return TextKaisekiTantouCd.Value
        End Get
        Set(ByVal value As String)
            TextKaisekiTantouCd.Value = IIf(value = 0, "", value)
        End Set
    End Property

    ''' <summary>
    ''' 解析担当者名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KaisekiTantouMei() As String
        Get
            Return TextKaisekiTantouMei.Value
        End Get
        Set(ByVal value As String)
            TextKaisekiTantouMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 工事担当者コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiTantouCd() As String
        Get
            Return TextKoujiTantouCd.Value
        End Get
        Set(ByVal value As String)
            TextKoujiTantouCd.Value = IIf(value = 0, "", value)
        End Set
    End Property

    ''' <summary>
    ''' 工事担当者名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiTantouMei() As String
        Get
            Return TextKoujiTantouMei.Value
        End Get
        Set(ByVal value As String)
            TextKoujiTantouMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextKubun
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKubun() As HtmlInputText
        Get
            Return TextKubun
        End Get
        Set(ByVal value As HtmlInputText)
            TextKubun = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBangou
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBangou() As HtmlInputText
        Get
            Return TextBangou
        End Get
        Set(ByVal value As HtmlInputText)
            TextBangou = value
        End Set
    End Property


#Region "判定１"
    ''' <summary>
    ''' 判定１
    ''' </summary>
    ''' <remarks></remarks>
    Private _hanteiCd1 As Integer
    ''' <summary>
    ''' 判定１
    ''' </summary>
    ''' <value></value>
    ''' <returns> 判定１</returns>
    ''' <remarks></remarks>
    Public Property HanteiCd1() As Integer
        Get
            Return _hanteiCd1
        End Get
        Set(ByVal value As Integer)
            _hanteiCd1 = value
        End Set
    End Property
#End Region

#Region "判定２"
    ''' <summary>
    ''' 判定２
    ''' </summary>
    ''' <remarks></remarks>
    Private _hanteiCd2 As Integer
    ''' <summary>
    ''' 判定２
    ''' </summary>
    ''' <value></value>
    ''' <returns> 判定２</returns>
    ''' <remarks></remarks>
    Public Property HanteiCd2() As Integer
        Get
            Return _hanteiCd2
        End Get
        Set(ByVal value As Integer)
            _hanteiCd2 = value
        End Set
    End Property
#End Region

#Region "判定接続詞"
    ''' <summary>
    ''' 判定接続詞
    ''' </summary>
    ''' <remarks></remarks>
    Private _hanteiSetuzokuMoji As Integer
    ''' <summary>
    ''' 判定接続詞
    ''' </summary>
    ''' <value></value>
    ''' <returns> 判定接続詞</returns>
    ''' <remarks></remarks>
    Public Property HanteiSetuzokuMoji() As Integer
        Get
            Return _hanteiSetuzokuMoji
        End Get
        Set(ByVal value As Integer)
            _hanteiSetuzokuMoji = value
        End Set
    End Property
#End Region

#Region "経理業務権限"
    ''' <summary>
    ''' 経理業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiriGyoumuKengen() As Integer
        Get
            Return HiddenKeiriGyoumuKengen.Value
        End Get
        Set(ByVal value As Integer)
            HiddenKeiriGyoumuKengen.Value = value
        End Set
    End Property
#End Region

#End Region

#Region "イベント"
    ''' <summary>
    ''' ページロード時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            SetHanteiData()

            ' 共通情報タイトル部、情報設定
            SetKyoutuuTitleInfo()

            KyoutuuDispLink.HRef = "javascript:changeDisplay('" & _
                                 TBodyKyotuInfo.ClientID & _
                                 "');changeDisplay('" & _
                                 KyoutuuTitleInfobar.ClientID & _
                                 "');"

            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            ' 他システムへのリンクボタン設定
            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            '保証書DB
            cLogic.getHosyousyoDbFilePath(TextKubun.Value, TextBangou.Value, ButtonHosyousyoDB)

            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            ' イベントハンドラ設定
            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            '物件履歴表示ボタン
            ButtonBukkenRireki.Attributes("onclick") = "callSearch('" & TextKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.POPUP_BUKKEN_RIREKI & "','','');"

            '更新履歴表示ボタン
            ButtonKousinRireki.Attributes("onclick") = "callSearch('" & TextKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.SEARCH_KOUSIN_RIREKI & "','','');"

            '画面表示時点の値を、Hiddenに保持(仕入 変更チェック用)
            If Me.HiddenOpenValue.Value = String.Empty Then
                Me.HiddenOpenValue.Value = Me.getCtrlValuesStringAll()
            End If

        End If

    End Sub
#End Region

#Region "プライベートメソッド"
    ''' <summary>
    ''' 判定内容の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHanteiData()

        ' 基礎仕様ロジッククラス
        Dim kisoSiyouLogic As New KisoSiyouLogic()



        ' 判定１
        If _hanteiCd1 > 0 Then
            Dim record As KisoSiyouRecord = kisoSiyouLogic.GetKisoSiyouRec(_hanteiCd1)

            If record.KahiKbn = 9 Then
                SpanHantei1.Style("color") = "red"
            Else
                SpanHantei1.Style("color") = "blue"
            End If

            SpanHantei1.InnerHtml = record.KsSiyou ' 判定１
        End If


        ' 判定１
        If _hanteiCd1 > 0 Then
            SpanHantei1.InnerHtml = kisoSiyouLogic.GetKisoSiyouMei(_hanteiCd1)
        End If

        ' 判定２
        If _hanteiCd2 > 0 Then

            Dim record As KisoSiyouRecord = kisoSiyouLogic.GetKisoSiyouRec(_hanteiCd2)

            If record.KahiKbn = 9 Then
                SpanHantei2.Style("color") = "red"
            Else
                SpanHantei2.Style("color") = "blue"
            End If

            SpanHantei2.InnerHtml = record.KsSiyou ' 判定２
        End If

        ' 判定接続詞
        If _hanteiSetuzokuMoji > 0 Then
            SpanHanteiSetuzoku.InnerHtml = kisoSiyouLogic.GetKisoSiyouSetuzokusiMei(_hanteiSetuzokuMoji)
        End If
    End Sub

    ''' <summary>
    ''' 共通情報タイトル部の情報設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKyoutuuTitleInfo()

        KyoutuuTitleInfobar.InnerHtml = "&nbsp;&nbsp;【" & _
                                     TextKubun.Value & "】 【" & _
                                     TextBangou.Value & "】 【" & _
                                     TextSesyuMei.Value & "】 【" & _
                                     TextJyuusyo1.Value & "】"

    End Sub

    ''' <summary>
    ''' テキストエリア単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableTextArea(ByRef ctrl As HtmlTextArea, _
                               ByVal enabled As Boolean)

        If enabled Then
            ctrl.Attributes("ReadOnly") = ""
            ctrl.Attributes("class") = "codeNumber"
        Else
            ctrl.Attributes("ReadOnly") = "readonly"
            ctrl.Attributes("class") = "readOnlyStyle"
        End If

    End Sub

#Region "画面コントロールの変更箇所対応"

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(全項目)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAll() As String

        Dim sb As New StringBuilder

        '●表示項目
        sb.Append(Me.TextBikou1.Value & EarthConst.SEP_STRING) '備考1

        'KEY情報の取得
        Me.getCtrlValuesStringAllKey()

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を結合し、文字列化する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKey()
        Dim dic As New Dictionary(Of String, String)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        '画面表示時のDB値の連結値を取得
        If Me.HiddenKeyValue.Value = String.Empty Then

            With dic
                .Add("0", "備考1")
            End With

            strRecString = iLogic.getJoinString(dic.Values.GetEnumerator)
            Me.HiddenKeyValue.Value = strRecString
        End If

    End Sub

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を管理し、対象の項目が存在した場合、背景色を赤色に変更する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKeyCtrlId(ByVal strKey As String)
        Dim objRet As New Object
        Dim dic As New Dictionary(Of String, Object)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        'Key毎にオブジェクトをセット
        With dic
            .Add("0", Me.TextBikou1)
        End With

        '背景色変更処理
        Call cLogic.ChgHenkouCtrlBgColor(dic, strKey)

    End Sub

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を結合し、文字列化する。
    ''' 変更箇所の背景色を変更する
    ''' </summary>
    ''' <param name="strKey">KEY値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAllKeyName(ByVal strKey As String, ByVal strCtrlNameKey As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim MyLogic As New TeibetuSyuuseiLogic

        Dim strKeyValues() As String
        Dim strHiddenKeyValues() As String
        Dim strRet As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty
        Dim dicItem1 As Dictionary(Of String, String)
        Dim strColorId As String = String.Empty

        If strKey = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB値
        strKeyValues = iLogic.getArrayFromDollarSep(strKey)

        '項目名を取得
        strHiddenKeyValues = iLogic.getArrayFromDollarSep(strCtrlNameKey)
        dicItem1 = MyLogic.getDicItem(strHiddenKeyValues)

        For intCnt = 0 To strHiddenKeyValues.Length - 1

            If strKeyValues.Length <= intCnt Then Exit For

            strTmp1 = strKeyValues(intCnt)
            If strTmp1 <> String.Empty Then
                If dicItem1.ContainsKey(strTmp1) Then
                    If intCnt <> 0 Then '最初の項目に","は付けない
                        strRet &= ","
                    End If
                    '変更箇所の項目名称を取得
                    strRet &= dicItem1(strTmp1)
                    '背景色の変更
                    Me.getCtrlValuesStringAllKeyCtrlId(strTmp1)
                End If
            End If
        Next

        Return strRet
    End Function

    ''' <summary>
    ''' 変更のあったコントロール名称を文字列結合し、返却する
    ''' </summary>
    ''' <param name="strDbVal">DB値</param>
    ''' <param name="strChgVal">変更値</param>
    ''' <param name="strCtrlNm">コントロール名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkChgCtrlName(ByVal strDbVal As String, ByVal strChgVal As String, ByVal strCtrlNm As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strDbValues() As String
        Dim strChgValues() As String
        Dim strRet As String = String.Empty
        Dim strKey As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty

        'DB値あるいは変更値が未入力の場合
        If strDbVal = String.Empty OrElse strChgVal = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB値
        strDbValues = iLogic.getArrayFromDollarSep(strDbVal)
        '画面の値
        strChgValues = iLogic.getArrayFromDollarSep(strChgVal)

        '項目数が同じ場合
        If strDbValues.Length = strChgValues.Length Then
            For intCnt = 0 To strDbValues.Length - 1
                strTmp1 = strDbValues(intCnt)
                strTmp2 = strChgValues(intCnt)
                '変更箇所があればindexを退避
                If strTmp1 <> strTmp2 Then
                    strKey &= CStr(intCnt) & EarthConst.SEP_STRING
                End If
            Next
        End If

        'indexを元に、変更箇所の名称と背景色変更を行なう
        strRet = Me.getCtrlValuesStringAllKeyName(strKey, strCtrlNm)

        Return strRet
    End Function

#End Region

#End Region

#Region "パブリックメソッド"
    ''' <summary>
    ''' エラーチェック
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByRef strChgPartMess As String _
                          )

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckInput", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    strChgPartMess)

        '地盤画面共通クラス
        Dim jBn As New Jiban

        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "："
        End If

        '禁則文字数チェック
        If jBn.KinsiStrCheck(TextBikou1.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", setuzoku & "備考1")
            arrFocusTargetCtrl.Add(TextBikou1)
        End If

        If jBn.KinsiStrCheck(TextBikou2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", setuzoku & "備考2")
            arrFocusTargetCtrl.Add(TextBikou2)
        End If

        '改行変換
        If TextBikou1.Value <> "" Then
            TextBikou1.Value = TextBikou1.Value.Replace(vbCrLf, " ")
        End If
        If TextBikou2.Value <> "" Then
            TextBikou2.Value = TextBikou2.Value.Replace(vbCrLf, " ")
        End If

        'バイト数チェック
        If jBn.ByteCheckSJIS(TextBikou1.Value, 256) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", setuzoku & "備考1")
            arrFocusTargetCtrl.Add(TextBikou1)
        End If

        If jBn.ByteCheckSJIS(TextBikou2.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", setuzoku & "備考2")
            arrFocusTargetCtrl.Add(TextBikou2)
        End If

        '比較実施(変更チェック)
        Dim strChgVal As String = Me.getCtrlValuesStringAll()
        If Me.HiddenOpenValue.Value <> strChgVal Then
            Dim strCtrlNm As String = String.Empty
            strCtrlNm = Me.ChkChgCtrlName(Me.HiddenOpenValue.Value, strChgVal, Me.HiddenKeyValue.Value) '変更箇所名の取得
            strChgPartMess += "[" & typeWord & "]\r\n" & strCtrlNm & "\r\n"
        End If

    End Sub
#End Region

End Class