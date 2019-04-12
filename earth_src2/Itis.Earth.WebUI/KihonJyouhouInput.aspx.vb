Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class KihonJyouhouInput
    Inherits System.Web.UI.Page

    '編集項目非活性、活性設定対応　20180905　李松涛　対応　↓
    'salesforce項目_編集非活性フラグ 取得
    Private Function Iskassei(ByVal KameitenCd As String, ByVal kbn As String) As Boolean

        If kbn.Trim <> "" Then
            If ViewState("Iskassei") Is Nothing Then
                If kbn = "" Then
                    ViewState("Iskassei") = ""
                Else
                    ViewState("Iskassei") = (New Salesforce).GetSalesforceHikasseiFlgByKbn(kbn)
                End If

            End If
        Else

            If ViewState("Iskassei") Is Nothing Then
                ViewState("Iskassei") = (New Salesforce).GetSalesforceHikasseiFlg(KameitenCd)
            End If

        End If
        Return ViewState("Iskassei").ToString <> "1"
    End Function

    '編集項目非活性、活性設定する
    Public Sub SetKassei()

        ViewState("Iskassei") = Nothing
        Dim kbn As String

        If tbxKyoutuKubun.Visible AndAlso tbxKyoutuKubun.Text <> "" Then
            kbn = tbxKyoutuKubun.Text
        Else
            If comdrp.Visible Then
                kbn = Me.comdrp.SelectedValue
            Else
                kbn = Me.common_drop1.SelectedValue
            End If
        End If


        Dim itKassei As Boolean = Iskassei(tbxKyoutuKameitenCd.Text, kbn)


        tbxKyoutuKameitenMei2.ReadOnly = Not itKassei
        tbxKyoutuKameitenMei2.CssClass = GetCss(itKassei, tbxKyoutuKameitenMei2.CssClass)


        tbxKyoutukakeMei2.ReadOnly = Not itKassei
        tbxKyoutukakeMei2.CssClass = GetCss(itKassei, tbxKyoutukakeMei2.CssClass)
        tbxKeiretuCd.ReadOnly = Not itKassei
        tbxKeiretuCd.CssClass = GetCss(itKassei, tbxKeiretuCd.CssClass)
        btnKeiretuCd.Enabled = itKassei


        tbxKyoutuKameitenMei2.Text = ""
        tbxKyoutukakeMei2.Text = ""
        tbxKeiretuCd.Text = ""
        tbxKeiretuMei.Text = ""

    End Sub

    Public Function GetCss(ByVal itKassei As Boolean, ByVal css As String)
        If itKassei Then
            Return Microsoft.VisualBasic.Strings.Replace(css, "readOnly", "", 1, -1, CompareMethod.Text)
        Else
            Return css & " readOnly"
        End If
    End Function

    '編集項目非活性、活性設定対応　20180905　李松涛　対応　↑

    ''' <summary>加盟店情報を新規登録する</summary>
    ''' <remarks>加盟店情報新規登録を提供する</remarks>
    ''' <history>
    ''' <para>2009/07/15　付龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Private kihonJyouhouInputLogic As New KihonJyouhouInputLogic
    Private commonCheck As New CommonCheck

    ''' <summary>ページロッド</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '編集項目非活性、活性設定対応　20180905　李松涛　対応　↓
        comdrp.OnChange = "SetKassei()"
        common_drop1.OnChange = "SetKassei()"
        '編集項目非活性、活性設定対応　20180905　李松涛　対応　↑

        '権限チェック
        Dim user_info As New LoginUserInfo
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '地盤画面共通クラス
        ' ユーザー基本認証
        jBn.userAuth(user_info)
        If ninsyou.GetUserID() = String.Empty Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        If user_info Is Nothing Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        Else
            If user_info.EigyouMasterKanriKengen <> -1 Then
                Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
                Server.Transfer("CommonErr.aspx")
            End If
        End If

        WindowOnload()

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            CommonCheck.SetURL(Me, ninsyou.GetUserID())
            CType(Me.common_drop1.FindControl("ddlCommonDrop"), DropDownList).Focus()

            '==============2012/03/21 車龍 405721案件の対応 追加↓=============================
            '「取消」ddlをセットする
            Call Me.SetTorikesiDDL()
            '==============2012/03/21 車龍 405721案件の対応 追加↑=============================
            '編集項目非活性、活性設定対応　20180905　李松涛　対応　↓
            SetKassei()
            '編集項目非活性、活性設定対応　20180905　李松涛　対応　↑
        Else
            If hidTourokuFlg.Value = "1" Then
                hidTourokuFlg.Value = String.Empty
                InsKameitenInfo()
            End If
        End If
        '==================2012/03/27 車龍 405721案件の対応 追加↓=========================
        Me.ddlTorikesi.Attributes.Add("onChange", "fncSetTorikesi();")
        '色をセットする
        Call Me.SetColor()
        '==================2012/03/27 車龍 405721案件の対応 追加↑=========================

        Me.btnKameitenSearch.Attributes.Add("onclick", "return fncNyuuryokuCheck('" & common_drop1.DdlClientID & "');")

        'Javascript作成
        MakeJavascript()

        btnHansokuSina.Attributes.Add("onclick", "return fncHansokuSina();")
        tbxBirudaNo.Attributes.Add("onBlur", "fncToUpper(this);")
        'tbxKeiretuCd.Attributes.Add("onBlur", "fncToUpper(this);")
        'tbxEigyousyoCd.Attributes.Add("onBlur", "fncToUpper(this);")
        tbxThKasiCd.Attributes.Add("onBlur", "fncToUpper(this);")
        tbxKyoutukakeMei1.Attributes.Add("onBlur", "fncTokomozi(this);")
        tbxKyoutukakeMei2.Attributes.Add("onBlur", "fncTokomozi(this);")


    End Sub

    ''' <summary>直接入力ボタンをクリック時</summary>
    Protected Sub btnTyokusetuNyuuryoku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTyokusetuNyuuryoku.Click
        comdrp.Visible = True
        tbxKyoutuKubun.Visible = False
        tbxKubunMei.Visible = False
        tbxKyoutuKameitenCd.Text = String.Empty
        tbxKyoutuKameitenCd.ReadOnly = False
        tbxKyoutuKameitenCd.CssClass = "codeNumber"
        tbxKyoutuKameitenCd.TabIndex = 0
        common_drop1.SelectedValue = String.Empty
        common_drop1.Enabled = False
        common_drop1.CssClass = "readOnly"
        tbxKameitenCd.Text = String.Empty
        tbxKameitenCd.Enabled = False
        tbxKameitenCd.CssClass = "readOnly"
        tbxBirudaNo.Text = String.Empty
        tbxBirudaMei.Text = String.Empty
        btnKameitenSearch.Enabled = False
        btnTyokusetuNyuuryoku.Visible = False
        btnTyokusetuNyuuryokuTyuusi.Visible = True
        btnTouroku.Enabled = True
        btnTouroku.Attributes.Add("onclick", "if(fncNyuuryokuCheck('" & comdrp.DdlClientID & "')==true){}else{return false}")
    End Sub

    ''' <summary>直接入力中止ボタンをクリック時</summary>
    Private Sub btnTyokusetuNyuuryokuTyuusi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyokusetuNyuuryokuTyuusi.Click
        comdrp.SelectedValue = String.Empty
        comdrp.Visible = False
        tbxKyoutuKubun.Visible = True
        tbxKubunMei.Visible = True
        tbxKyoutuKubun.Text = String.Empty
        tbxKubunMei.Text = String.Empty
        tbxKyoutuKameitenCd.Text = String.Empty
        tbxKyoutuKameitenCd.ReadOnly = True
        tbxKyoutuKameitenCd.CssClass = "readOnly"
        tbxKyoutuKameitenCd.TabIndex = -1
        common_drop1.Enabled = True
        common_drop1.CssClass = String.Empty
        tbxKameitenCd.Enabled = True
        tbxKameitenCd.CssClass = "codeNumber"
        btnKameitenSearch.Enabled = True
        btnTyokusetuNyuuryoku.Visible = True
        btnTyokusetuNyuuryokuTyuusi.Visible = False
        btnTouroku.Enabled = False
        btnTouroku.Attributes.Add("onclick", "")
    End Sub

    ''' <summary>検索ボタンをクリック時</summary>
    Private Sub btnKameitenSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKameitenSearch.Click

        SetKassei()

        Dim csScript As New StringBuilder
        Dim commonCheck As New CommonCheck

        '入力チェック
        With csScript
            If Me.tbxKameitenCd.Text <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxKameitenCd.Text, "加盟店コード"))
                If csScript.ToString = String.Empty AndAlso Me.tbxKameitenCd.Text.Length <> 5 Then
                    .Append(String.Format(Messages.Instance.MSG2004E, "加盟店コード", "5").ToString)
                End If
            End If
        End With
        If csScript.ToString <> String.Empty Then
            ShowMsg(csScript.ToString, Me.tbxKameitenCd.ClientID)
            Exit Sub
        End If

        '加盟店コードチェック
        If tbxKameitenCd.Text = String.Empty Then
            '===========↓2012/11/19 車龍 407597の対応 修正↓==================
            'Dim dtKameitenCd As KihonJyouhouInputDataSet.KameitenCdTableDataTable = kihonJyouhouInputLogic.GetMaxKameitenCd(common_drop1.SelectedValue)
            ''加盟店コードの最大値が存在の場合
            'If dtKameitenCd.Rows.Count <> 0 AndAlso dtKameitenCd(0).kameiten_cd.ToString <> String.Empty Then
            '    '加盟店コードの最大値=99999の場合
            '    If dtKameitenCd(0).kameiten_cd.ToString = "99999" Then
            '        tbxKyoutuKubun.Text = String.Empty
            '        tbxKubunMei.Text = String.Empty
            '        tbxKyoutuKameitenCd.Text = String.Empty
            '        tbxBirudaNo.Text = String.Empty
            '        btnTouroku.Enabled = False
            '        ShowMsg(Messages.Instance.MSG2025E, tbxKameitenCd.ClientID)
            '        Exit Sub
            '    End If

            '    Dim intKameitenCd As String = Right("00000" & (CInt(dtKameitenCd(0).kameiten_cd) + 1).ToString, 5)

            '    '設定した加盟店コードを存在チェック
            '    Dim dtKameitenCd1 As KihonJyouhouInputDataSet.KameitenCdTableDataTable
            '    dtKameitenCd1 = kihonJyouhouInputLogic.GetKameitenCd(String.Empty, intKameitenCd)

            '    Do While dtKameitenCd1.Rows.Count <> 0
            '        intKameitenCd = Right("00000" & (CInt(intKameitenCd) + 1).ToString, 5)
            '        If intKameitenCd = "99999" Then
            '            tbxKyoutuKubun.Text = String.Empty
            '            tbxKubunMei.Text = String.Empty
            '            tbxKyoutuKameitenCd.Text = String.Empty
            '            tbxBirudaNo.Text = String.Empty
            '            btnTouroku.Enabled = False
            '            ShowMsg(Messages.Instance.MSG2025E, tbxKameitenCd.ClientID)
            '            Exit Sub
            '        End If
            '        dtKameitenCd1 = kihonJyouhouInputLogic.GetKameitenCd(String.Empty, intKameitenCd)
            '    Loop
            '    tbxKyoutuKameitenCd.Text = intKameitenCd
            'Else
            '    tbxKyoutuKubun.Text = String.Empty
            '    tbxKubunMei.Text = String.Empty
            '    tbxKyoutuKameitenCd.Text = String.Empty
            '    tbxBirudaNo.Text = String.Empty
            '    btnTouroku.Enabled = False
            '    ShowMsg(String.Format(Messages.Instance.MSG2026E, common_drop1.SelectedValue), tbxKameitenCd.ClientID)
            '    Exit Sub
            'End If

            '対象区分＆加盟店コードの最大値を加盟店採番設定の範囲から取得する
            Dim dtMaxKameitencd As New Data.DataTable
            dtMaxKameitencd = kihonJyouhouInputLogic.GetMaxKameitenCd1(common_drop1.SelectedValue)
            '加盟店コードの最大値
            Dim strkameitenCdMax As String = dtMaxKameitencd.Rows(0).Item("kameiten_cd_max").ToString.Trim
            '加盟店採番_From
            Dim strKameitenSaibanFrom As String = dtMaxKameitencd.Rows(0).Item("kameiten_saiban_from").ToString.Trim
            '加盟店採番_To
            Dim strKameitenSaibanTo As String = dtMaxKameitencd.Rows(0).Item("kameiten_saiban_to").ToString.Trim

            Dim intNewKameitenCd As Integer

            If strkameitenCdMax.Equals(String.Empty) Then
                '加盟店コードの最大値がない場合

                If strKameitenSaibanFrom.Equals(String.Empty) Then
                    '加盟店採番_Fromがない場合
                    intNewKameitenCd = 1
                Else
                    '加盟店採番_Fromがある場合
                    intNewKameitenCd = Convert.ToInt32(strKameitenSaibanFrom)
                End If
            Else
                '加盟店コードの最大値がある場合
                If Not commonCheck.CheckHankaku(strkameitenCdMax, String.Empty, "1").Equals(String.Empty) Then
                    '半角数字ではない場合

                    intNewKameitenCd = Convert.ToInt32(strKameitenSaibanFrom)
                Else
                    '半角数字の場合

                    If (Not strKameitenSaibanFrom.Equals(String.Empty)) AndAlso (Not strKameitenSaibanTo.Equals(String.Empty)) Then
                        '加盟店採番_From、加盟店採番_To がある場合

                        If (Convert.ToInt32(strKameitenSaibanFrom) <= Convert.ToInt32(strkameitenCdMax)) _
                            AndAlso (Convert.ToInt32(strkameitenCdMax) <= Convert.ToInt32(strKameitenSaibanTo)) Then
                            '加盟店採番_From <= 最大値 <= 加盟店採番_To の場合

                            'MAX(加盟店マスタ.加盟店コード)+1
                            intNewKameitenCd = Convert.ToInt32(strkameitenCdMax) + 1
                        Else
                            '取得した最大値が　区分マスタ.加盟店採番_From　And　区分マスタ.加盟店採番_To　に存在しない場合

                            '区分マスタ.加盟店採番_From　の値を登録する加盟店コードにセットする
                            intNewKameitenCd = Convert.ToInt32(strKameitenSaibanFrom)
                        End If
                    ElseIf (strKameitenSaibanFrom.Equals(String.Empty)) AndAlso (strKameitenSaibanTo.Equals(String.Empty)) Then
                        '加盟店採番_From、加盟店採番_To がない場合

                        'MAX(加盟店マスタ.加盟店コード)+1
                        intNewKameitenCd = Convert.ToInt32(strkameitenCdMax) + 1

                    ElseIf (Not strKameitenSaibanFrom.Equals(String.Empty)) AndAlso (strKameitenSaibanTo.Equals(String.Empty)) Then
                        '加盟店採番_Fromがある、加盟店採番_Toがない場合
                        If Convert.ToInt32(strkameitenCdMax) >= Convert.ToInt32(strKameitenSaibanFrom) Then

                            'MAX(加盟店マスタ.加盟店コード)+1
                            intNewKameitenCd = Convert.ToInt32(strkameitenCdMax) + 1
                        Else
                            intNewKameitenCd = Convert.ToInt32(strKameitenSaibanFrom)
                        End If

                    ElseIf (strKameitenSaibanFrom.Equals(String.Empty)) AndAlso (Not strKameitenSaibanTo.Equals(String.Empty)) Then
                        '加盟店採番_Fromがない、加盟店採番_Toがある場合

                        'MAX(加盟店マスタ.加盟店コード)+1
                        intNewKameitenCd = Convert.ToInt32(strkameitenCdMax) + 1
                    End If
                End If
            End If

            Dim blnErrorFlg As Boolean
            If strKameitenSaibanTo.Equals(String.Empty) Then
                blnErrorFlg = intNewKameitenCd >= 99999
            Else
                blnErrorFlg = intNewKameitenCd > Convert.ToInt32(strKameitenSaibanTo)
            End If

            If blnErrorFlg Then
                tbxKyoutuKubun.Text = String.Empty
                tbxKubunMei.Text = String.Empty
                tbxKyoutuKameitenCd.Text = String.Empty
                tbxBirudaNo.Text = String.Empty
                btnTouroku.Enabled = False

                'エラーを表示する
                ShowMsg(Messages.Instance.MSG2074E, tbxKameitenCd.ClientID)
                '処理終了
                Exit Sub
            End If

            tbxKyoutuKameitenCd.Text = Right(StrDup(5, "0") & intNewKameitenCd.ToString, 5)
            '===========↑2012/11/19 車龍 407597の対応 修正↑==================
            tbxKyoutuKubun.Text = common_drop1.SelectedValue
            tbxKubunMei.Text = common_drop1.SelectedText.Split("：")(1)
            tbxBirudaNo.Text = tbxKyoutuKameitenCd.Text
            btnTouroku.Enabled = True
        Else
            Dim i As Integer = 5
            Dim bln As Boolean = False
            For i = tbxKameitenCd.Text.Length To 1 Step -1
                Dim j As Integer = Mid(tbxKameitenCd.Text, i, 1)
                If j = 0 Then
                    bln = True
                Else
                    Exit For
                End If
            Next
            Dim strCd As String = Mid(tbxKameitenCd.Text, 1, i) & Mid("99999", 1, 5 - i)

            If bln = True Then
                Dim dtKameitenCd As KihonJyouhouInputDataSet.KameitenCdTableDataTable
                dtKameitenCd = kihonJyouhouInputLogic.GetMaxKameitenCd(common_drop1.SelectedValue, tbxKameitenCd.Text, strCd)
                If dtKameitenCd.Rows.Count <> 0 AndAlso dtKameitenCd(0).kameiten_cd.ToString <> String.Empty Then
                    If dtKameitenCd(0).kameiten_cd < strCd Then
                        tbxKyoutuKameitenCd.Text = Right("00000" & (dtKameitenCd(0).kameiten_cd + 1).ToString, 5)
                    Else
                        tbxKyoutuKubun.Text = String.Empty
                        tbxKubunMei.Text = String.Empty
                        tbxKyoutuKameitenCd.Text = String.Empty
                        tbxBirudaNo.Text = String.Empty
                        btnTouroku.Enabled = False
                        ShowMsg(Messages.Instance.MSG2025E, tbxKameitenCd.ClientID)
                        Exit Sub
                    End If
                Else
                    tbxKyoutuKameitenCd.Text = tbxKameitenCd.Text
                End If
                tbxKyoutuKubun.Text = common_drop1.SelectedValue
                tbxKubunMei.Text = common_drop1.SelectedText.Split("：")(1)
                tbxBirudaNo.Text = tbxKyoutuKameitenCd.Text
                btnTouroku.Enabled = True
            Else
                Dim dtKameitenCd As KihonJyouhouInputDataSet.KameitenCdTableDataTable
                dtKameitenCd = kihonJyouhouInputLogic.GetKameitenCd(common_drop1.SelectedValue, tbxKameitenCd.Text)
                '入力した加盟店コードが存在しない場合
                If dtKameitenCd.Rows.Count = 0 Then
                    tbxKyoutuKameitenCd.Text = tbxKameitenCd.Text
                Else
                    '入力した加盟店コード=99999の場合
                    If tbxKameitenCd.Text = "99999" Then
                        tbxKyoutuKubun.Text = String.Empty
                        tbxKubunMei.Text = String.Empty
                        tbxKyoutuKameitenCd.Text = String.Empty
                        tbxBirudaNo.Text = String.Empty
                        btnTouroku.Enabled = False
                        ShowMsg(Messages.Instance.MSG2025E, tbxKameitenCd.ClientID)
                        Exit Sub
                    End If
                    dtKameitenCd = kihonJyouhouInputLogic.GetKameitenCd(common_drop1.SelectedValue, Right("00000" & (tbxKameitenCd.Text + 1).ToString, 5))
                    '入力した加盟店コード+1が存在しない場合
                    If dtKameitenCd.Rows.Count = 0 Then
                        tbxKyoutuKameitenCd.Text = Right("00000" & (tbxKameitenCd.Text + 1).ToString, 5)
                    Else
                        'エラー
                        tbxKyoutuKubun.Text = String.Empty
                        tbxKubunMei.Text = String.Empty
                        tbxKyoutuKameitenCd.Text = String.Empty
                        tbxBirudaNo.Text = String.Empty
                        btnTouroku.Enabled = False
                        ShowMsg(Messages.Instance.MSG2027E, tbxKameitenCd.ClientID)
                        Exit Sub
                    End If
                End If
                tbxKyoutuKubun.Text = common_drop1.SelectedValue
                tbxKubunMei.Text = common_drop1.SelectedText.Split("：")(1)
                tbxBirudaNo.Text = tbxKyoutuKameitenCd.Text
                btnTouroku.Enabled = True
            End If
        End If

    End Sub

    ''' <summary>登録ボタンをクリック時</summary>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Dim commonSearch As New CommonSearchLogic
        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErr As String = CheckInput(strObjId)
        If strErr <> String.Empty Then
            ShowMsg(strErr, strObjId)
            Exit Sub
        End If

        '設定した加盟店コードを存在チェック
        Dim dtKameitenCd As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        dtKameitenCd = KihonJyouhouInputLogic.GetKameitenCd(String.Empty, tbxKyoutuKameitenCd.Text)
        If dtKameitenCd.Rows.Count <> 0 Then
            ShowMsg(String.Format(Messages.Instance.MSG2029E, "加盟店コード", "加盟店コード").ToString, btnTouroku.ClientID)
            Exit Sub
        End If

        Dim strKbn As String
        '区分設定
        If comdrp.Visible Then
            strKbn = comdrp.SelectedValue
        Else
            strKbn = common_drop1.SelectedValue
        End If
        '系列コードを設置。
        If Me.tbxKeiretuCd.Text <> String.Empty Then
            Dim dtKeiretuTable As Data.DataTable = commonSearch.GetCommonInfo(tbxKeiretuCd.Text, "m_keiretu", strKbn)
            If dtKeiretuTable.Rows.Count > 0 Then
                Me.tbxKeiretuCd.Text = dtKeiretuTable.Rows(0).Item("cd").ToString
                Me.tbxKeiretuMei.Text = dtKeiretuTable.Rows(0).Item("mei").ToString
            End If
        End If
        '営業所コードを設置。
        If Me.tbxEigyousyoCd.Text <> String.Empty Then
            Dim dtEigyousyoTable As Data.DataTable = commonSearch.GetCommonInfo(tbxEigyousyoCd.Text, "m_eigyousyo")
            If dtEigyousyoTable.Rows.Count > 0 Then
                Me.tbxEigyousyoCd.Text = dtEigyousyoTable.Rows(0).Item("cd").ToString
                Me.tbxEigyousyoMei.Text = dtEigyousyoTable.Rows(0).Item("mei").ToString
            End If
        End If

        '登録確認メッセージ
        ShowConfirm(btnTouroku.ClientID)

    End Sub

    ''' <summary>加盟店マスタテーブルに登録する</summary>
    ''' <returns>成否</returns>
    Private Function InsKameitenInfo() As Boolean
        Dim ninsyou As New Ninsyou()
        Dim strKbn As String
        '区分設定
        If btnTyokusetuNyuuryoku.Visible = True Then
            strKbn = tbxKyoutuKubun.Text
        Else
            strKbn = comdrp.SelectedValue
        End If

        '画面に入力したデータを設定する
        Dim dtParamKameitenInfo As New KihonJyouhouInputDataSet.Param_KameitenInfoDataTable
        Dim row As KihonJyouhouInputDataSet.Param_KameitenInfoRow = dtParamKameitenInfo.NewParam_KameitenInfoRow
        row.kameiten_cd = tbxKyoutuKameitenCd.Text
        row.kbn = strKbn
        '==============2012/03/21 車龍 405721案件の対応 修正↓=============================
        'row.torikesi = tbxTorikesi.Text
        row.torikesi = Me.ddlTorikesi.SelectedValue.Trim
        '==============2012/03/21 車龍 405721案件の対応 修正↑=============================
        row.kameiten_mei1 = tbxKyoutuKameitenMei1.Text
        row.tenmei_kana1 = CommonCheck.SetTokomozi(tbxKyoutukakeMei1.Text)
        row.kameiten_mei2 = IIf(tbxKyoutuKameitenMei2.Text = String.Empty, String.Empty, tbxKyoutuKameitenMei2.Text)
        row.tenmei_kana2 = IIf(tbxKyoutukakeMei2.Text = String.Empty, String.Empty, CommonCheck.SetTokomozi(tbxKyoutukakeMei2.Text))
        row.builder_no = IIf(tbxBirudaNo.Text = String.Empty, String.Empty, tbxBirudaNo.Text)
        row.keiretu_cd = IIf(tbxKeiretuCd.Text = String.Empty, String.Empty, tbxKeiretuCd.Text)
        row.eigyousyo_cd = IIf(tbxEigyousyoCd.Text = String.Empty, String.Empty, tbxEigyousyoCd.Text)
        row.th_kasi_cd = IIf(tbxThKasiCd.Text = String.Empty, String.Empty, tbxThKasiCd.Text)
        row.add_login_user_id = ninsyou.GetUserID()
        dtParamKameitenInfo.AddParam_KameitenInfoRow(row)

        '加盟店マスタテーブルに登録する
        If KihonJyouhouInputLogic.SetKameitenInfo(dtParamKameitenInfo) Then
            Server.Transfer("KihonJyouhouInquiry.aspx?strKameitenCd=" & dtParamKameitenInfo(0).kameiten_cd & "")
        Else
            ShowMsg(Replace(Messages.Instance.MSG019E, "@PARAM1", "加盟店新規登録"), btnTouroku.ClientID)
        End If

    End Function

    ''' <summary>入力チェック</summary>
    ''' <param name="strObjId">クライアントID</param>
    ''' <returns>エラーメッセージ</returns>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim commonSearch As New CommonSearchLogic
        Dim csScript As New StringBuilder
        Dim strKbn As String
        Dim jBn As New Jiban

        '区分設定
        If comdrp.Visible Then
            strKbn = comdrp.SelectedValue
        Else
            strKbn = common_drop1.SelectedValue
        End If

        With csScript
            '==============2012/03/21 車龍 405721案件の対応 削除↓=============================
            ''取消
            'If Me.tbxTorikesi.Text <> String.Empty Then
            '    .Append(commonCheck.CheckHankaku(Me.tbxTorikesi.Text, "取消"))
            '    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
            '        strObjId = Me.tbxTorikesi.ClientID
            '    End If
            'Else
            '    .Append(commonCheck.CheckHissuNyuuryoku(Me.tbxTorikesi.Text, "取消"))
            '    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
            '        strObjId = Me.tbxTorikesi.ClientID
            '    End If
            'End If
            '==============2012/03/21 車龍 405721案件の対応 削除↑=============================
            '加盟店コード
            If Me.tbxKyoutuKameitenCd.Text <> String.Empty Then
                .Append(CommonCheck.CheckHankaku(Me.tbxKyoutuKameitenCd.Text, "加盟店コード"))
                If csScript.ToString = String.Empty AndAlso Me.tbxKyoutuKameitenCd.Text.Length <> 5 Then
                    .Append(String.Format(Messages.Instance.MSG2004E, "加盟店コード", "5").ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenCd.ClientID
                End If
            Else
                .Append(CommonCheck.CheckHissuNyuuryoku(Me.tbxKyoutuKameitenCd.Text, "加盟店コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenCd.ClientID
                End If
            End If
            '加盟店名１
            If Me.tbxKyoutuKameitenMei1.Text <> String.Empty Then
                .Append(CommonCheck.CheckKinsoku(Me.tbxKyoutuKameitenMei1.Text, "加盟店名１"))
                .Append(CommonCheck.CheckByte(Me.tbxKyoutuKameitenMei1.Text, 40, "加盟店名１", kbn.ZENKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenMei1.ClientID
                End If
            Else
                .Append(CommonCheck.CheckHissuNyuuryoku(Me.tbxKyoutuKameitenMei1.Text, "加盟店名１"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenMei1.ClientID
                End If
            End If
            '店カナ名１
            If Me.tbxKyoutukakeMei1.Text <> String.Empty Then
                .Append(CommonCheck.CheckKatakana(CommonCheck.SetTokomozi(Me.tbxKyoutukakeMei1.Text), "店カナ名１"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutukakeMei1.ClientID
                End If
            Else
                .Append(CommonCheck.CheckHissuNyuuryoku(Me.tbxKyoutukakeMei1.Text, "店カナ名１"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutukakeMei1.ClientID
                End If
            End If
            '加盟店名２
            If Me.tbxKyoutuKameitenMei2.Text <> String.Empty Then
                .Append(CommonCheck.CheckKinsoku(Me.tbxKyoutuKameitenMei2.Text, "加盟店名２"))
                .Append(CommonCheck.CheckByte(Me.tbxKyoutuKameitenMei2.Text, 40, "加盟店名２", kbn.ZENKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenMei2.ClientID
                End If
            End If
            '店カナ名２
            If Me.tbxKyoutukakeMei2.Text <> String.Empty Then
                .Append(CommonCheck.CheckKatakana(CommonCheck.SetTokomozi(Me.tbxKyoutukakeMei2.Text), "店カナ名２"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutukakeMei2.ClientID
                End If
            End If
            'ビルダ－NO
            If Me.tbxBirudaNo.Text <> String.Empty Then
                .Append(CommonCheck.ChkHankakuEisuuji(Me.tbxBirudaNo.Text, "ビルダ－NO"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxBirudaNo.ClientID
                End If
            End If
            '系列コード
            If Me.tbxKeiretuCd.Text <> String.Empty Then
                .Append(CommonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text, "系列コード"))
                If CommonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text, "系列コード") = String.Empty Then
                    If commonSearch.GetCommonInfo(tbxKeiretuCd.Text, "m_keiretu", strKbn).Rows.Count = 0 Then
                        .Append(String.Format(Messages.Instance.MSG2008E, "系列コード").ToString)
                    End If
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKeiretuCd.ClientID
                End If
            End If
            '営業所コード
            If Me.tbxEigyousyoCd.Text <> String.Empty Then
                .Append(CommonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd.Text, "営業所コード"))
                If CommonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd.Text, "営業所コード") = String.Empty Then
                    If commonSearch.GetCommonInfo(tbxEigyousyoCd.Text, "m_eigyousyo").Rows.Count = 0 Then
                        .Append(String.Format(Messages.Instance.MSG2008E, "営業所コード").ToString)
                    End If
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxEigyousyoCd.ClientID
                End If
            End If
            'TH瑕疵ｺｰﾄﾞ
            If Me.tbxThKasiCd.Text <> String.Empty Then
                .Append(CommonCheck.CheckKinsoku(Me.tbxThKasiCd.Text, "TH瑕疵ｺｰﾄﾞ"))
                If CommonCheck.CheckKinsoku(Me.tbxThKasiCd.Text, "TH瑕疵ｺｰﾄﾞ") = String.Empty Then
                    If Not jBn.byteCheckSJIS(Me.tbxThKasiCd.Text, 7) Then
                        .Append(String.Format(Messages.Instance.MSG2028E, "TH瑕疵ｺｰﾄﾞ", "7"))
                    End If
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxThKasiCd.ClientID
                End If
            End If
        End With

        Return csScript.ToString
    End Function

    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            'ビルダーポップアップ
            .AppendLine("   function fncKameitenSearch(){")
            .AppendLine("       var strkbn='ビルダー'")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxBirudaNo.ClientID & "&objMei=" & Me.tbxBirudaMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxBirudaNo.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun=&blnDelete=True', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   function fncHansokuSina(){")
            .AppendLine("       var objKbn1 = document.getElementById('" & Me.common_drop1.DdlClientID & "')")
            .AppendLine("       var objKbn2 = document.getElementById('" & Me.comdrp.DdlClientID & "')")
            .AppendLine("       var objKameitenCd = document.getElementById('" & Me.tbxKyoutuKameitenCd.ClientID & "')")
            .AppendLine("       try{if(objKbn1.disabled==false && objKbn1.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.disabled==false && objKbn2.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn2.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }}catch(e){}")
            .AppendLine("       if(objKameitenCd.value==''){")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "加盟店コード") & "');")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(!chkHankakuSuuji(objKameitenCd.value)){")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2006E, "加盟店コード") & "');")
            .AppendLine("           objKameitenCd.focus();")
            .AppendLine("           objKameitenCd.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(objKameitenCd.value.length!=5){")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2004E, "加盟店コード", "5") & "');")
            .AppendLine("           objKameitenCd.focus();")
            .AppendLine("           objKameitenCd.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       var arrKubun ")
            .AppendLine("       if(objKbn1.disabled==true){")
            .AppendLine("           arrKubun = objKbn2.value;")
            .AppendLine("       }else{")
            .AppendLine("           arrKubun = objKbn1.value;")
            .AppendLine("       }")
            .AppendLine("       var flg ")
            .AppendLine("       if(arrKubun=='A'){")
            .AppendLine("           flg = '1';")
            .AppendLine("       }else{")
            .AppendLine("           flg = '0';")
            .AppendLine("       }")
            .AppendLine("       return funcMove('" & UrlConst.TENBETU_SYUUSEI & "','2',flg,objKameitenCd.value);")
            .AppendLine("   }")
            '系列ポップアップ
            .AppendLine("   function fncKeiretuSearch(){")
            .AppendLine("       var objKbn1 = document.getElementById('" & Me.common_drop1.DdlClientID & "')")
            .AppendLine("       var objKbn2 = document.getElementById('" & Me.comdrp.DdlClientID & "')")
            .AppendLine("       try{if(objKbn1.disabled==false && objKbn1.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.disabled==false && objKbn2.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn2.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }}catch(e){}")
            .AppendLine("       var strkbn='系列'")
            .AppendLine("       var arrKubun ")
            .AppendLine("       if(objKbn1.disabled==true){")
            .AppendLine("           arrKubun = objKbn2.value;")
            .AppendLine("       }else{")
            .AppendLine("           arrKubun = objKbn1.value;")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxKeiretuCd.ClientID & "&objMei=" & Me.tbxKeiretuMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxKeiretuCd.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun='+arrKubun+'&blnDelete=True', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '営業所ポップアップ
            .AppendLine("   function fncEigyousyoSearch(){")
            .AppendLine("       var strkbn='営業所'")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxEigyousyoCd.ClientID & "&objMei=" & Me.tbxEigyousyoMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxEigyousyoCd.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun=&blnDelete=True', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   function fncNyuuryokuCheck(strClientID){")
            .AppendLine("       var objKbn = document.getElementById(strClientID)")
            .AppendLine("       if(objKbn.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG004E & "');")
            .AppendLine("           objKbn.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       return true;")
            .AppendLine("   }")
            '==============2012/03/21 車龍 405721案件の対応 追加↓=============================
            '取消を変更する時
            .AppendLine("function fncSetTorikesi() ")
            .AppendLine("{ ")
            .AppendLine("	var strTorikesi = document.getElementById('" & Me.ddlTorikesi.ClientID & "').value; ")
            .AppendLine("	if(strTorikesi =='0') ")
            .AppendLine("	{ ")
            .AppendLine("		fncSetColor(''); ")
            .AppendLine("	} ")
            .AppendLine("	else ")
            .AppendLine("	{ ")
            .AppendLine("		fncSetColor('red'); ")
            .AppendLine("	} ")
            .AppendLine("} ")
            '色をセットする
            .AppendLine("function fncSetColor(strColor) ")
            .AppendLine("{ ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKubun.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKubunMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.ddlTorikesi.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenMei1.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutukakeMei1.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenMei2.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutukakeMei2.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxBirudaNo.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxBirudaMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKeiretuCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKeiretuMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxEigyousyoCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxEigyousyoMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxThKasiCd.ClientID & "').style.color = strColor; ")
            .AppendLine("} ")
            '==============2012/03/21 車龍 405721案件の対応 追加↑=============================
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub

    ''' <summary>登録確認メッセージ</summary>
    ''' <param name="strObjId">クライアントID</param>
    Private Sub ShowConfirm(ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("   document.getElementById('" & hidTourokuFlg.ClientID & "').value = '1';")
            .AppendLine("   document.forms[0].submit();")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowConfirm", csScript.ToString, True)
    End Sub

    ''' <summary>メッセージ表示</summary>
    ''' <param name="strMessage">メッセージ</param>
    ''' <param name="strID">クライアントID</param>
    Private Sub ShowMsg(ByVal strMessage As String, ByVal strID As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("alert('" & strMessage & "');")
            .AppendLine("if(document.getElementById('" & strID & "').type != 'submit'){")
            .AppendLine("    document.getElementById('" & strID & "').focus();")
            .AppendLine("    document.getElementById('" & strID & "').select();")
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowMsg", csScript.ToString, True)
    End Sub

    Private Sub WindowOnload()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload(){")
            .AppendLine("   document.getElementById('" & Me.hidTourokuFlg.ClientID & "').value='';")
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "WindowOnload", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' 「取消」ddlをセットする
    ''' </summary>
    ''' <history>2012/03/27 車龍 405721案件の対応 追加</history>
    Private Sub SetTorikesiDDL()

        'データを取得する
        Dim dtTorikesi As New Data.DataTable
        dtTorikesi = KihonJyouhouInputLogic.GetTorikesiList()

        With Me.ddlTorikesi
            'ddlをBoundする
            .DataValueField = "code"
            .DataTextField = "meisyou"
            .DataSource = dtTorikesi
            .DataBind()

            '先頭行
            '=============2012/04/20 車龍 405721の要望対応 修正↓==================
            '.Items.Insert(0, New ListItem("0", "0"))
            .Items.Insert(0, New ListItem(String.Empty, "0"))
            '=============2012/04/20 車龍 405721の要望対応 修正↑==================
        End With

    End Sub

    ''' <summary>
    ''' 色を変更する
    ''' </summary>
    ''' <history>2012/03/27 車龍 405721案件の対応 追加</history>
    Private Sub SetColor()

        Dim strTorikesi As String
        strTorikesi = Me.ddlTorikesi.SelectedValue.Trim

        If strTorikesi.Equals("0") OrElse strTorikesi.Equals(String.Empty) Then
            Me.tbxKyoutuKubun.ForeColor = Drawing.Color.Black
            Me.tbxKubunMei.ForeColor = Drawing.Color.Black
            Me.ddlTorikesi.ForeColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenCd.ForeColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenMei1.ForeColor = Drawing.Color.Black
            Me.tbxKyoutukakeMei1.ForeColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenMei2.ForeColor = Drawing.Color.Black
            Me.tbxKyoutukakeMei2.ForeColor = Drawing.Color.Black
            Me.tbxBirudaNo.ForeColor = Drawing.Color.Black
            Me.tbxBirudaMei.ForeColor = Drawing.Color.Black
            Me.tbxKeiretuCd.ForeColor = Drawing.Color.Black
            Me.tbxKeiretuMei.ForeColor = Drawing.Color.Black
            Me.tbxEigyousyoCd.ForeColor = Drawing.Color.Black
            Me.tbxEigyousyoMei.ForeColor = Drawing.Color.Black
            Me.tbxThKasiCd.ForeColor = Drawing.Color.Black
        Else
            Me.tbxKyoutuKubun.ForeColor = Drawing.Color.Red
            Me.tbxKubunMei.ForeColor = Drawing.Color.Red
            Me.ddlTorikesi.ForeColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenCd.ForeColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenMei1.ForeColor = Drawing.Color.Red
            Me.tbxKyoutukakeMei1.ForeColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenMei2.ForeColor = Drawing.Color.Red
            Me.tbxKyoutukakeMei2.ForeColor = Drawing.Color.Red
            Me.tbxBirudaNo.ForeColor = Drawing.Color.Red
            Me.tbxBirudaMei.ForeColor = Drawing.Color.Red
            Me.tbxKeiretuCd.ForeColor = Drawing.Color.Red
            Me.tbxKeiretuMei.ForeColor = Drawing.Color.Red
            Me.tbxEigyousyoCd.ForeColor = Drawing.Color.Red
            Me.tbxEigyousyoMei.ForeColor = Drawing.Color.Red
            Me.tbxThKasiCd.ForeColor = Drawing.Color.Red
        End If

    End Sub

End Class