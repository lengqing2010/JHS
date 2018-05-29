Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports System.Data

''' <summary>
''' 年度計画値設定
''' </summary>
''' <remarks>年度計画値設定</remarks>
''' <history>
''' <para>2012/11/14 P-44979 王新 新規作成 </para>
''' </history>
Partial Class NendoKeikakuInput
    Inherits System.Web.UI.Page

#Region "プライベート変数"
    'インスタンス生成
    Private objNendoKeikakuInputBC As New Lixil.JHS_EKKS.BizLogic.NendoKeikakuInputBC
    Private objCommonBC As New Lixil.JHS_EKKS.BizLogic.CommonBC
    Private objCommon As New Common
    Private objCommonCheck As New CommonCheck
#End Region

#Region "定数"
    'メニュー番号
    Private Const conMenuno As String = "K002"
#End Region

#Region "イベント"
    ''' <summary>
    ''' 初期処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim strYear As String
        Dim strUserID As String = ""

        If Not IsPostBack Then
            objCommonCheck.CommonNinsyou(strUserID, Me.Master.loginUserInfo, kegen.UserIdOnly)

            'ログインユーザーを保存する
            ViewState("LoginUserID") = strUserID

            'システム日時を取得する
            strYear = objCommon.GetSystemYear()

            '初期年度リストに設定する
            Me.drpNendo.DataSource = objCommonBC.GetKeikakuNendoData()
            Me.drpNendo.DataBind()

            '初期年度を設定する
            Me.drpNendo.SelectedIndex = -1

            'システム年度を設定する
            For i As Integer = 0 To Me.drpNendo.Items.Count - 1
                If Me.drpNendo.Items(i).Value.Equals(Convert.ToString(strYear)) Then
                    Me.drpNendo.Items(i).Selected = True
                    Exit For
                End If
            Next

            '年度を判断する
            If Me.drpNendo.SelectedValue.Equals(String.Empty) Then
                '年度が空白の場合
                Call SetPageClear()
            Else
                '支店別の計画年度を設定する
                Me.lblNendo.Text = Me.drpNendo.SelectedValue & "年度"

                '年度により、全社と各支店のデータを設定する
                Call SetNendoData(strYear)
            End If
        End If

        '画面のJS EVENT設定
        Call SetJsEvent()
    End Sub

    ''' <summary>
    ''' 年度を選択する時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Protected Sub drpNendo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpNendo.SelectedIndexChanged
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim strNendo As String              '年度

        '年度を取得する
        strNendo = Me.drpNendo.SelectedValue

        If strNendo = "" Then
            Call SetPageClear()
        Else
            Me.tbSum.Visible = True

            Me.btnKensuuCopy.Enabled = True
            Me.btnUriKingakuCopy.Enabled = True
            Me.btnArariCopy.Enabled = True

            '支店別の年度を設定する
            Me.lblNendo.Text = strNendo & "年度"

            '年度により、全社と各支店のデータを設定する
            Call SetNendoData(strNendo)

        End If

    End Sub

    ''' <summary>
    ''' 明細設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Protected Sub grdMeisai_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles grdMeisai.ItemDataBound
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim intRowIndex As Integer
        intRowIndex = e.Item.ItemIndex

        '明細レコードを設定する
        If Not Me.grdMeisai.DataSource Is Nothing Then
            CType(e.Item.FindControl("numKeikaku1_1"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku1_1"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku1_2"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku1_2"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku1_3"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku1_3"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku2_1"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku2_1"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku2_2"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku2_2"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku2_3"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku2_3"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku3_1"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku3_1"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku3_2"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku3_2"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"

            CType(e.Item.FindControl("numKeikaku3_3"), CommonNumber).Attributes("onfocusout") = _
                CType(e.Item.FindControl("numKeikaku3_3"), CommonNumber).Attributes("onfocusout") & "fncKeikakuSum(this.id);"
        End If
    End Sub

    ''' <summary>
    ''' 全社の計画値保存ボタンをクリックする時
    ''' </summary>
    ''' <returns>共通処理フラグ</returns>
    ''' <remarks>全社の計画値保存ボタンをクリックする時</remarks>
    ''' <history>
    ''' <para>2012/11/22 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function BtnAllSave_Click() As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim hsData As Hashtable
        Dim strYear As String               '計画年度

        '計画年度を取得する
        strYear = Me.drpNendo.SelectedValue

        '計画年度の必入力チェック
        If strYear.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG009E, "計画年度")
            Me.drpNendo.Focus()
            Return False
        End If

        If Not Me.txtKome.Text.Trim().Equals(String.Empty) Then
            '禁則文字チェック
            If Not objCommonCheck.kinsiStrCheck(Me.txtKome.Text.Trim()) Then
                objCommon.SetShowMessage(Me, CommonMessage.MSG068E, "コメント")
                Me.txtKome.Focus()
                Return False
            End If
        End If

        '全社の計画データを取得する
        hsData = GetZensyaData(0)

        '全社の計画データをDBに登録する
        objNendoKeikakuInputBC.SetZensyaKeikakuKanriData(hsData)

        '年度により、全社と各支店のデータを設定する
        Call SetNendoData(strYear)

        '総合計を再計算する
        Call SetSumData()

        '保存完了した後、確認メッセージを表示する
        objCommon.SetShowMessage(Me, CommonMessage.MSG012E, Me.drpNendo.SelectedValue)

        Return True
    End Function

    ''' <summary>
    ''' 全社の確定ボタンをクリックする時
    ''' </summary>
    ''' <returns>共通処理フラグ</returns>
    ''' <remarks>全社の確定ボタンをクリックする時</remarks>
    ''' <history>
    ''' <para>2012/11/22 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function BtnAllConfirm_Click() As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim hsData As Hashtable
        Dim strYear As String               '計画年度
        Dim strMaxAddDateTime As String     '最大登録日時(関連チェック用)
        Dim blnKakuteiFlg As Boolean        '確定フラグ

        '確定フラグの設定
        If Me.btnAllSave.Enabled Then
            blnKakuteiFlg = True
        Else
            blnKakuteiFlg = False
        End If

        '計画年度を取得する
        strYear = Me.drpNendo.SelectedValue

        '計画年度の必入力チェック
        If strYear.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG009E, "計画年度")
            Me.drpNendo.Focus()
            Return False
        End If

        '計画調査件数の必入力チェック
        If Me.numKeikakuKensuu.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "計画調査件数")
            Me.numKeikakuKensuu.Focus()
            Return False
        End If

        '計画売上金額の必入力チェック
        If Me.numKeikakuUriKingaku.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "計画売上金額")
            Me.numKeikakuUriKingaku.Focus()
            Return False
        End If

        '計画粗利金額の必入力チェック
        If Me.numKeikakuArari.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "計画粗利金額")
            Me.numKeikakuArari.Focus()
            Return False
        End If

        '同年度で既に確定データがある場合
        If Not blnKakuteiFlg Then
            'コメントの必入力チェック
            If Me.txtKome.Text.Trim().Equals(String.Empty) Then
                objCommon.SetShowMessage(Me, CommonMessage.MSG014E)
                Me.txtKome.Focus()
                Return False
            End If

            '禁則文字チェック
            If Not objCommonCheck.kinsiStrCheck(Me.txtKome.Text.Trim()) Then
                objCommon.SetShowMessage(Me, CommonMessage.MSG068E, "コメント")
                Me.txtKome.Focus()
                Return False
            End If
        End If

        '関連チェック
        If Not ViewState("MaxAddDateTime") Is Nothing Then
            '該当年度の最大登録日時を取得する(支店から)
            strMaxAddDateTime = objNendoKeikakuInputBC.GetMaxSitenbetuKeikakuKanriData(strYear)

            '該当年度の最大登録日時と明細の年度を比較する
            If Not Convert.ToString(ViewState("MaxAddDateTime")).Equals(strMaxAddDateTime) Then
                'エラーメッセージを表示する
                objCommon.SetShowMessage(Me, CommonMessage.MSG042E)
                Return False
            End If
        End If

        '全社の計画データを取得する
        hsData = GetZensyaData(1)

        '全社の計画データをDBに登録する
        objNendoKeikakuInputBC.SetZensyaKeikakuKanriData(hsData)

        '年度により、全社と各支店のデータを設定する
        Call SetNendoData(strYear)

        '総合計を再計算する
        Call SetSumData()

        '同年度で既に確定データがある場合
        If Not blnKakuteiFlg Then
            '確定完了した後、確認メッセージを表示する
            objCommon.SetShowMessage(Me, CommonMessage.MSG015E)
        Else
            '確定完了した後、確認メッセージを表示する
            objCommon.SetShowMessage(Me, CommonMessage.MSG013E, Me.drpNendo.SelectedValue)
        End If

        Return True
    End Function

    ''' <summary>
    ''' 支店別の計画値保存ボタンをクリックする時
    ''' </summary>
    ''' <returns>共通処理フラグ</returns>
    ''' <remarks>支店別の計画値保存ボタンをクリックする時</remarks>
    ''' <history>
    ''' <para>2012/11/22 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function BtnSitenbetuSave_Click() As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtValue As DataTable
        Dim strYear As String               '計画年度

        '計画年度を取得する
        strYear = Me.drpNendo.SelectedValue

        '計画年度の必入力チェック
        If strYear.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG009E, "計画年度")
            Me.drpNendo.Focus()
            Return False
        End If

        '支店の計画データを取得する
        dtValue = GetSitenbetuData(0)

        '全社の計画データをDBに登録する
        objNendoKeikakuInputBC.SetSitenbetuKeikakuKanriData(dtValue)

        '年度により、全社と各支店のデータを設定する
        Call SetNendoData(strYear)

        '総合計を再計算する
        Call SetSumData()

        '保存完了した後、確認メッセージを表示する
        objCommon.SetShowMessage(Me, CommonMessage.MSG016E)

        Return True
    End Function

    ''' <summary>
    ''' 支店別の確定ボタンをクリックする時
    ''' </summary>
    ''' <returns>共通処理フラグ</returns>
    ''' <remarks>支店別の確定ボタンをクリックする時</remarks>
    ''' <history>
    ''' <para>2012/11/22 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function BtnSitenbetuConfirm_Click() As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtValue As DataTable
        Dim strYear As String               '計画年度
        Dim strMaxAddDateTime As String     '最大登録日時(関連チェック用)
        Dim strMsg As String                'エラーメッセージ内容
        Dim blnKakuteiFlg As Boolean        '確定フラグ

        '確定フラグの設定
        If Me.btnSitenbetuSave.Enabled Then
            blnKakuteiFlg = True
        Else
            blnKakuteiFlg = False
        End If

        '計画年度を取得する
        strYear = Me.drpNendo.SelectedValue

        '計画年度の必入力チェック
        If strYear.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG009E, "計画年度")
            Me.drpNendo.Focus()
            Return False
        End If

        '計画調査件数の必入力チェック
        If Me.numKeikakuKensuu.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "計画調査件数")
            Me.numKeikakuKensuu.Focus()
            Return False
        End If

        '計画売上金額の必入力チェック
        If Me.numKeikakuUriKingaku.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "計画売上金額")
            Me.numKeikakuUriKingaku.Focus()
            Return False
        End If

        '計画粗利金額の必入力チェック
        If Me.numKeikakuArari.Value.Equals(String.Empty) Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "計画粗利金額")
            Me.numKeikakuArari.Focus()
            Return False
        End If

        '画面関連チェック
        If Not Me.numKeikaku1_4_Sum.Value.Equals(Me.numKeikakuKensuu.Value) OrElse _
           Not Me.numKeikaku2_4_Sum.Value.Equals(Me.numKeikakuUriKingaku.Value) OrElse _
           Not Me.numKeikaku3_4_Sum.Value.Equals(Me.numKeikakuArari.Value) Then
            '確認メッセージを表示する
            objCommon.SetShowMessage(Me, CommonMessage.MSG017E)

            strMsg = ""

            '支店別計画各項目　<　全体計画各項目　の場合
            If objCommon.GetLongFromObj(Me.numKeikaku1_4_Sum.Value) < _
                objCommon.GetLongFromObj(Me.numKeikakuKensuu.Value) Then
                strMsg = CommonMessage.MSG018E.Replace("{0}", "調査件数") & "\r\n"
            End If

            If objCommon.GetLongFromObj(Me.numKeikaku2_4_Sum.Value) < _
                objCommon.GetLongFromObj(Me.numKeikakuUriKingaku.Value) Then
                strMsg = strMsg & CommonMessage.MSG018E.Replace("{0}", "売上金額") & "\r\n"
            End If

            If objCommon.GetLongFromObj(Me.numKeikaku3_4_Sum.Value) < _
                objCommon.GetLongFromObj(Me.numKeikakuArari.Value) Then
                strMsg = strMsg & CommonMessage.MSG018E.Replace("{0}", "粗利金額") & "\r\n"
            End If

            If strMsg <> "" Then
                strMsg = strMsg.Remove(strMsg.LastIndexOf("\r\n"))
                'エラーメッセージを表示する
                objCommon.SetShowMessage(Me, strMsg)
                Return False
            End If
        End If

        'DB関連チェック
        '該当年度の最大登録日時を取得する(全社から)
        strMaxAddDateTime = objNendoKeikakuInputBC.GetMaxZensyaKeikakuKanriData(strYear)

        '最大登録日時の有無を判断する
        If Not strMaxAddDateTime.Equals(String.Empty) Then
            '全社のデータの有無を判断する
            If Not ViewState("ZensyaData") Is Nothing Then
                '該当年度の最大登録日時と明細の年度を比較する
                If Not Convert.ToString(CType(ViewState("ZensyaData"), DataTable).Rows(0)("add_datetime")).Equals(strMaxAddDateTime) Then
                    'エラーメッセージを表示する
                    objCommon.SetShowMessage(Me, CommonMessage.MSG041E)
                    Return False
                End If
            End If
        End If

        '支店の計画データを取得する
        dtValue = GetSitenbetuData(1)

        '全社の計画データをDBに登録する
        objNendoKeikakuInputBC.SetSitenbetuKeikakuKanriData(dtValue)

        '年度により、全社と各支店のデータを設定する
        Call SetNendoData(strYear)

        '総合計を再計算する
        Call SetSumData()

        '同年度で既に確定データがある場合
        If Not blnKakuteiFlg Then
            '確定完了した後、確認メッセージを表示する
            objCommon.SetShowMessage(Me, CommonMessage.MSG021E)
        Else
            '確定完了した後、確認メッセージを表示する
            objCommon.SetShowMessage(Me, CommonMessage.MSG019E)
        End If

        Return True
    End Function

    ''' <summary>
    ''' 月別計画値設定ボタンをクリックする時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSitenbetu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSitenbetu.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        '月別計画値設定画面へ遷移する
        Server.Transfer("SitenTukibetuKeikakuchiSearchList.aspx")
    End Sub
#End Region

#Region "メンッド"
    ''' <summary>
    ''' 画面のJS EVENT設定
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/05/28 P-42186 王新 新規作成 </para>																															
    ''' </history>	
    Private Sub SetJsEvent()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '全社の計画値保存ボタン押下時
        Me.btnAllSave.OnClick = "BtnAllSave_Click()"

        '全社の確定ボタン押下時
        Me.btnAllConfirm.OnClick = "BtnAllConfirm_Click()"

        '支店別の計画値保存ボタン押下時
        Me.btnSitenbetuSave.OnClick = "BtnSitenbetuSave_Click()"

        '支店別の確定ボタン押下時
        Me.btnSitenbetuConfirm.OnClick = "BtnSitenbetuConfirm_Click()"

    End Sub

    ''' <summary>
    ''' 年度により、全社と各支店のデータを設定する
    ''' </summary>
    ''' <param name="strNendo">年度</param>
    ''' <remarks>年度により、全社と各支店のデータを設定する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Private Sub SetNendoData(ByVal strNendo As String)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strNendo)

        Dim dtZensyaData As DataTable           '全社計画データ
        Dim dtSitenbetuData As DataTable        '支店別データ
        Dim strKakuteiFlg As String             '確定FLG
        Dim intSysYearFlg As Integer            'システム年度フラグ(0:システム年度がない、1:システム年度)

        'システム年度を判断する
        If objCommon.GetSystemYear().Equals(strNendo) Then
            'システム年度
            intSysYearFlg = 1

            '保存ボタンと確定ボタンを使用不可になる
            Me.btnAllSave.Enabled = True
            Me.btnAllConfirm.Enabled = True
            Me.btnSitenbetuSave.Enabled = True
            Me.btnSitenbetuConfirm.Enabled = True
        Else
            'システム年度がない
            intSysYearFlg = 0
        End If

        '全社部
        dtZensyaData = objNendoKeikakuInputBC.GetZensyaKeikakuKanriData(strNendo)

        If Not dtZensyaData Is Nothing AndAlso dtZensyaData.Rows.Count > 0 Then
            '全社計画データにより、画面項目を設定する
            Me.numKeikakuKensuu.Value = Convert.ToString(dtZensyaData.Rows(0)("keikaku_kensuu"))
            Me.numKeikakuUriKingaku.Value = Convert.ToString(dtZensyaData.Rows(0)("keikaku_uri_kingaku"))
            Me.numKeikakuArari.Value = Convert.ToString(dtZensyaData.Rows(0)("keikaku_arari"))
            Me.txtKome.Text = Convert.ToString(dtZensyaData.Rows(0)("keikaku_settutei_kome"))
            strKakuteiFlg = Convert.ToString(dtZensyaData.Rows(0)("kakutei_flg"))

            '全社のデータを保存する
            ViewState("ZensyaData") = dtZensyaData
        Else
            'ヘッダ部の様式を設定する
            Me.tdMeisaiHead.Attributes.Add("style", "")
            Me.numKeikakuKensuu.Value = ""
            Me.numKeikakuUriKingaku.Value = ""
            Me.numKeikakuArari.Value = ""
            Me.txtKome.Text = ""
            strKakuteiFlg = ""

            '全社のデータをクリアする
            ViewState("ZensyaData") = Nothing

            '全社計画データを解放する
            dtZensyaData.Dispose()
            dtZensyaData = Nothing
        End If

        '同年度で既に確定データがある場合
        If strKakuteiFlg = "1" Then
            Me.btnAllSave.Enabled = False
        Else
            Me.btnAllSave.Enabled = True
        End If

        'システム年度により、支店別のデータを戻る
        If intSysYearFlg = 1 Then
            'システム年度の場合
            dtSitenbetuData = objNendoKeikakuInputBC.GetSitenbetuKeikakuKanriData(strNendo, 0)
        Else
            'システム年度がない場合
            dtSitenbetuData = objNendoKeikakuInputBC.GetSitenbetuKeikakuKanriData(strNendo, 0)
        End If

        If Not dtSitenbetuData Is Nothing AndAlso dtSitenbetuData.Rows.Count > 0 Then
            'ヘッダ部の様式を設定する
            Me.tdMeisaiHead.Attributes.Add("style", "border-bottom: black 2px solid;")
            Me.tbSum.Visible = True
            Me.grdMeisai.DataSource = dtSitenbetuData
            Me.grdMeisai.DataBind()

            Me.btnKensuuCopy.Enabled = True
            Me.btnUriKingakuCopy.Enabled = True
            Me.btnArariCopy.Enabled = True

            '同年度で既に確定データがある場合
            If Convert.ToString(dtSitenbetuData.Rows(0)("kakutei_flg")).Equals("1") Then
                Me.btnSitenbetuSave.Enabled = False
            Else
                Me.btnSitenbetuSave.Enabled = True
            End If

            '支店別のデータを保存する
            ViewState("SitenbetuData") = dtSitenbetuData

            '該当年度の最大登録日時を保存する
            ViewState("MaxAddDateTime") = objNendoKeikakuInputBC.GetMaxSitenbetuKeikakuKanriData(strNendo)
        Else
            'ヘッダ部の様式をクリアする
            Me.tdMeisaiHead.Attributes.Add("style", "")
            Me.tbSum.Visible = False
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()
            Me.btnSitenbetuSave.Enabled = False
            Me.btnSitenbetuConfirm.Enabled = False
            Me.btnKensuuCopy.Enabled = False
            Me.btnUriKingakuCopy.Enabled = False
            Me.btnArariCopy.Enabled = False

            '保存する支店別のデータをクリアする
            ViewState("SitenbetuData") = Nothing

            '該当年度の最大登録日時を保存する
            ViewState("MaxAddDateTime") = Nothing
        End If

        '総合計を再計算する
        Call SetSumData()
    End Sub

    ''' <summary>
    ''' 明細データを合計する
    ''' </summary>
    ''' <remarks>明細データを合計する</remarks>
    Private Sub SetSumData()
        Dim intJittuseki1_1 As Long
        Dim intJittuseki1_2 As Long
        Dim intJittuseki1_3 As Long
        Dim intJittuseki1_4 As Long

        Dim intJittuseki2_1 As Long
        Dim intJittuseki2_2 As Long
        Dim intJittuseki2_3 As Long
        Dim intJittuseki2_4 As Long

        Dim intJittuseki3_1 As Long
        Dim intJittuseki3_2 As Long
        Dim intJittuseki3_3 As Long
        Dim intJittuseki3_4 As Long

        Dim intKeikaku1_1 As Long
        Dim intKeikaku1_2 As Long
        Dim intKeikaku1_3 As Long
        Dim intKeikaku1_4 As Long

        Dim intKeikaku2_1 As Long
        Dim intKeikaku2_2 As Long
        Dim intKeikaku2_3 As Long
        Dim intKeikaku2_4 As Long

        Dim intKeikaku3_1 As Long
        Dim intKeikaku3_2 As Long
        Dim intKeikaku3_3 As Long
        Dim intKeikaku3_4 As Long

        Dim i As Integer

        If Me.grdMeisai.DataSource Is Nothing Then
            Me.numJittuseki1_1_Sum.Value = "0"
            Me.numJittuseki1_2_Sum.Value = "0"
            Me.numJittuseki1_3_Sum.Value = "0"
            Me.numJittuseki1_4_Sum.Value = "0"

            Me.numJittuseki2_1_Sum.Value = "0"
            Me.numJittuseki2_2_Sum.Value = "0"
            Me.numJittuseki2_3_Sum.Value = "0"
            Me.numJittuseki2_4_Sum.Value = "0"

            Me.numJittuseki3_1_Sum.Value = "0"
            Me.numJittuseki3_2_Sum.Value = "0"
            Me.numJittuseki3_3_Sum.Value = "0"
            Me.numJittuseki3_4_Sum.Value = "0"

            Me.numKeikaku1_1_Sum.Value = "0"
            Me.numKeikaku1_2_Sum.Value = "0"
            Me.numKeikaku1_3_Sum.Value = "0"
            Me.numKeikaku1_4_Sum.Value = "0"

            Me.numKeikaku2_1_Sum.Value = "0"
            Me.numKeikaku2_2_Sum.Value = "0"
            Me.numKeikaku2_3_Sum.Value = "0"
            Me.numKeikaku2_4_Sum.Value = "0"

            Me.numKeikaku3_1_Sum.Value = "0"
            Me.numKeikaku3_2_Sum.Value = "0"
            Me.numKeikaku3_3_Sum.Value = "0"
            Me.numKeikaku3_4_Sum.Value = "0"
        Else

            For i = 0 To Me.grdMeisai.Items.Count - 1
                intJittuseki1_1 = intJittuseki1_1 + GetGridValue(i, "numJittuseki1_1")
                intJittuseki1_2 = intJittuseki1_2 + GetGridValue(i, "numJittuseki1_2")
                intJittuseki1_3 = intJittuseki1_3 + GetGridValue(i, "numJittuseki1_3")
                intJittuseki1_4 = intJittuseki1_4 + GetGridValue(i, "numJittuseki1_4")

                intJittuseki2_1 = intJittuseki2_1 + GetGridValue(i, "numJittuseki2_1")
                intJittuseki2_2 = intJittuseki2_2 + GetGridValue(i, "numJittuseki2_2")
                intJittuseki2_3 = intJittuseki2_3 + GetGridValue(i, "numJittuseki2_3")
                intJittuseki2_4 = intJittuseki2_4 + GetGridValue(i, "numJittuseki2_4")

                intJittuseki3_1 = intJittuseki3_1 + GetGridValue(i, "numJittuseki3_1")
                intJittuseki3_2 = intJittuseki3_2 + GetGridValue(i, "numJittuseki3_2")
                intJittuseki3_3 = intJittuseki3_3 + GetGridValue(i, "numJittuseki3_3")
                intJittuseki3_4 = intJittuseki3_4 + GetGridValue(i, "numJittuseki3_4")

                intKeikaku1_1 = intKeikaku1_1 + GetGridValue(i, "numKeikaku1_1")
                intKeikaku1_2 = intKeikaku1_2 + GetGridValue(i, "numKeikaku1_2")
                intKeikaku1_3 = intKeikaku1_3 + GetGridValue(i, "numKeikaku1_3")
                intKeikaku1_4 = intKeikaku1_4 + GetGridValue(i, "numKeikaku1_4")

                intKeikaku2_1 = intKeikaku2_1 + GetGridValue(i, "numKeikaku2_1")
                intKeikaku2_2 = intKeikaku2_2 + GetGridValue(i, "numKeikaku2_2")
                intKeikaku2_3 = intKeikaku2_3 + GetGridValue(i, "numKeikaku2_3")
                intKeikaku2_4 = intKeikaku2_4 + GetGridValue(i, "numKeikaku2_4")

                intKeikaku3_1 = intKeikaku3_1 + GetGridValue(i, "numKeikaku3_1")
                intKeikaku3_2 = intKeikaku3_2 + GetGridValue(i, "numKeikaku3_2")
                intKeikaku3_3 = intKeikaku3_3 + GetGridValue(i, "numKeikaku3_3")
                intKeikaku3_4 = intKeikaku3_4 + GetGridValue(i, "numKeikaku3_4")

            Next

            Me.numJittuseki1_1_Sum.Value = Convert.ToString(intJittuseki1_1)
            Me.numJittuseki1_2_Sum.Value = Convert.ToString(intJittuseki1_2)
            Me.numJittuseki1_3_Sum.Value = Convert.ToString(intJittuseki1_3)
            Me.numJittuseki1_4_Sum.Value = Convert.ToString(intJittuseki1_4)

            Me.numJittuseki2_1_Sum.Value = Convert.ToString(intJittuseki2_1)
            Me.numJittuseki2_2_Sum.Value = Convert.ToString(intJittuseki2_2)
            Me.numJittuseki2_3_Sum.Value = Convert.ToString(intJittuseki2_3)
            Me.numJittuseki2_4_Sum.Value = Convert.ToString(intJittuseki2_4)

            Me.numJittuseki3_1_Sum.Value = Convert.ToString(intJittuseki3_1)
            Me.numJittuseki3_2_Sum.Value = Convert.ToString(intJittuseki3_2)
            Me.numJittuseki3_3_Sum.Value = Convert.ToString(intJittuseki3_3)
            Me.numJittuseki3_4_Sum.Value = Convert.ToString(intJittuseki3_4)

            Me.numKeikaku1_1_Sum.Value = Convert.ToString(intKeikaku1_1)
            Me.numKeikaku1_2_Sum.Value = Convert.ToString(intKeikaku1_2)
            Me.numKeikaku1_3_Sum.Value = Convert.ToString(intKeikaku1_3)
            Me.numKeikaku1_4_Sum.Value = Convert.ToString(intKeikaku1_4)

            Me.numKeikaku2_1_Sum.Value = Convert.ToString(intKeikaku2_1)
            Me.numKeikaku2_2_Sum.Value = Convert.ToString(intKeikaku2_2)
            Me.numKeikaku2_3_Sum.Value = Convert.ToString(intKeikaku2_3)
            Me.numKeikaku2_4_Sum.Value = Convert.ToString(intKeikaku2_4)

            Me.numKeikaku3_1_Sum.Value = Convert.ToString(intKeikaku3_1)
            Me.numKeikaku3_2_Sum.Value = Convert.ToString(intKeikaku3_2)
            Me.numKeikaku3_3_Sum.Value = Convert.ToString(intKeikaku3_3)
            Me.numKeikaku3_4_Sum.Value = Convert.ToString(intKeikaku3_4)
        End If
    End Sub

    ''' <summary>
    ''' 明細項目のデータを取得する
    ''' </summary>
    ''' <param name="intRowIndex">行番号</param>
    ''' <param name="strControlId">コントロール名</param>
    ''' <returns>明細項目のデータ</returns>
    ''' <remarks>明細項目のデータを取得する</remarks>
    Private Function GetGridValue(ByVal intRowIndex As Integer, _
                                  ByVal strControlId As String) As Long
        Dim strValue As String

        strValue = CType(Me.grdMeisai.Items(intRowIndex).FindControl(strControlId), CommonNumber).Value

        If strValue = "" Then
            Return 0
        Else
            Return Convert.ToInt64(strValue)
        End If
    End Function

    ''' <summary>
    ''' 明細項目のデータを取得する
    ''' </summary>
    ''' <param name="intRowIndex">行番号</param>
    ''' <param name="strControlId">コントロール名</param>
    ''' <param name="intReturnFlg">0:空白の場合、「0」を戻る、1:空白の場合、「NULL」を戻る</param>
    ''' <returns>明細項目のデータ</returns>
    ''' <remarks>明細項目のデータを取得する</remarks>
    Private Function GetGridValue(ByVal intRowIndex As Integer, _
                                  ByVal strControlId As String, _
                                  ByVal intReturnFlg As Integer) As Object
        Dim strValue As String

        strValue = CType(Me.grdMeisai.Items(intRowIndex).FindControl(strControlId), CommonNumber).Value

        If strValue = "" Then
            If intReturnFlg = 0 Then
                Return 0
            Else
                Return DBNull.Value
            End If
        Else
            Return Convert.ToInt64(strValue)
        End If
    End Function

    ''' <summary>
    ''' 全社計画データを取得する
    ''' </summary>
    ''' <param name="intButtonFlg">保存ボタン、確定ボタンの区分</param>
    ''' <returns>全社計画データ</returns>
    ''' <remarks>全社計画データを取得する</remarks>
    Private Function GetZensyaData(ByVal intButtonFlg As Integer) As Hashtable
        Dim hsData As Hashtable

        hsData = New Hashtable

        hsData("keikaku_nendo") = Me.drpNendo.SelectedValue             '年度
        hsData("keikaku_kensuu") = Me.numKeikakuKensuu.Value            '計画調査件数
        hsData("keikaku_uri_kingaku") = Me.numKeikakuUriKingaku.Value   '計画売上金額
        hsData("keikaku_arari") = Me.numKeikakuArari.Value              '計画粗利
        hsData("keikaku_settutei_kome") = Me.txtKome.Text               '計画設定時ｺﾒﾝﾄ
        hsData("keikaku_henkou_flg") = DBNull.Value                     '計画変更FLG

        '保存ボタン又は確定ボタンを判断する
        If intButtonFlg = 0 Then
            '保存ボタン
            hsData("kakutei_flg") = 0                                   '確定FLG
            hsData("keikaku_huhen_flg") = DBNull.Value                  '計画値不変FLG
        Else
            '確定ボタン
            hsData("kakutei_flg") = 1                                   '確定FLG

            '同年度で既に確定データがある場合
            If Me.btnAllSave.Enabled = False Then
                hsData("keikaku_huhen_flg") = 1                         '計画値不変FLG
            Else
                hsData("keikaku_huhen_flg") = DBNull.Value              '計画値不変FLG
            End If
        End If

        hsData("add_login_user_id") = ViewState("LoginUserID")          '登録者ID

        Return hsData
    End Function

    ''' <summary>
    ''' 支店別のデータを取得する
    ''' </summary>
    ''' <param name="intButtonFlg">保存ボタン、確定ボタンの区分</param>
    ''' <returns>支店別のデータ</returns>
    ''' <remarks>支店別のデータを取得する</remarks>
    Private Function GetSitenbetuData(ByVal intButtonFlg As Integer) As DataTable
        Dim dtViewStateData As DataTable
        Dim dtValue As DataTable
        Dim drValue As DataRow
        Dim strSystemDate As String
        Dim i As Integer

        If Me.grdMeisai.DataSource Is Nothing Then
            dtValue = Nothing
        End If

        '支店別のデータを取得する
        dtViewStateData = CType(ViewState("SitenbetuData"), DataTable)

        dtValue = New DataTable
        dtValue.Columns.Add("keikaku_nendo")                '計画年度
        dtValue.Columns.Add("siten_mei")                    '支店名
        dtValue.Columns.Add("add_datetime")                 '登録日時
        dtValue.Columns.Add("busyo_cd")                     '部署コード
        dtValue.Columns.Add("eigyou_keikaku_kensuu")        '営業_計画調査件数
        dtValue.Columns.Add("tokuhan_keikaku_kensuu")       '特販_計画調査件数
        dtValue.Columns.Add("FC_keikaku_kensuu")            'FC_計画調査件数
        dtValue.Columns.Add("eigyou_keikaku_uri_kingaku")   '営業_計画売上金額
        dtValue.Columns.Add("tokuhan_keikaku_uri_kingaku")  '特販_計画売上金額
        dtValue.Columns.Add("FC_keikaku_uri_kingaku")       'FC_計画売上金額
        dtValue.Columns.Add("eigyou_keikaku_arari")         '営業_計画粗利
        dtValue.Columns.Add("tokuhan_keikaku_arari")        '特販_計画粗利
        dtValue.Columns.Add("FC_keikaku_arari")             'FC_計画粗利

        dtValue.Columns.Add("keikaku_henkou_flg")           '計画変更FLG
        dtValue.Columns.Add("keikaku_settutei_kome")        '計画設定時ｺﾒﾝﾄ
        dtValue.Columns.Add("kakutei_flg")                  '確定FLG
        dtValue.Columns.Add("keikaku_huhen_flg")            '計画値不変FLG
        dtValue.Columns.Add("add_login_user_id")            '登録者ID

        'システム時間を取得する
        strSystemDate = objCommon.GetSystemDate().ToString()

        '明細データを保存する
        For i = 0 To Me.grdMeisai.Items.Count - 1
            drValue = dtValue.NewRow()
            drValue("keikaku_nendo") = Me.drpNendo.SelectedValue                        '計画年度 
            drValue("siten_mei") = dtViewStateData.Rows(i)("busyo_mei")                 '支店名 
            drValue("add_datetime") = strSystemDate                                     '登録日時
            drValue("busyo_cd") = dtViewStateData.Rows(i)("busyo_cd")                   '部署コード 

            drValue("eigyou_keikaku_kensuu") = GetGridValue(i, "numKeikaku1_1", 1)      '営業_計画調査件数 
            drValue("tokuhan_keikaku_kensuu") = GetGridValue(i, "numKeikaku1_2", 1)     '特販_計画調査件数 
            drValue("FC_keikaku_kensuu") = GetGridValue(i, "numKeikaku1_3", 1)          'FC_計画調査件数 
            drValue("eigyou_keikaku_uri_kingaku") = GetGridValue(i, "numKeikaku2_1", 1) '営業_計画売上金額 
            drValue("tokuhan_keikaku_uri_kingaku") = GetGridValue(i, "numKeikaku2_2", 1) '特販_計画売上金額 
            drValue("FC_keikaku_uri_kingaku") = GetGridValue(i, "numKeikaku2_3", 1)     'FC_計画売上金額 
            drValue("eigyou_keikaku_arari") = GetGridValue(i, "numKeikaku3_1", 1)       '営業_計画粗利 
            drValue("tokuhan_keikaku_arari") = GetGridValue(i, "numKeikaku3_2", 1)      '特販_計画粗利 
            drValue("FC_keikaku_arari") = GetGridValue(i, "numKeikaku3_3", 1)           'FC_計画粗利 

            drValue("keikaku_henkou_flg") = DBNull.Value                                '計画変更FLG
            drValue("keikaku_settutei_kome") = DBNull.Value                             '計画設定時ｺﾒﾝﾄ

            '保存ボタン又は確定ボタンを判断する
            If intButtonFlg = 0 Then
                '保存ボタン
                drValue("kakutei_flg") = 0                                   '確定FLG
                drValue("keikaku_huhen_flg") = DBNull.Value                  '計画値不変FLG
            Else
                '確定ボタン
                drValue("kakutei_flg") = 1                                   '確定FLG

                '同年度で既に確定データがある場合
                If Me.btnSitenbetuSave.Enabled = False Then
                    drValue("keikaku_huhen_flg") = 1                         '計画値不変FLG
                Else
                    drValue("keikaku_huhen_flg") = DBNull.Value              '計画値不変FLG
                End If
            End If

            drValue("add_login_user_id") = ViewState("LoginUserID")
            dtValue.Rows.Add(drValue)
        Next

        dtViewStateData.Dispose()
        Return dtValue
    End Function

    ''' <summary>
    ''' 画面データをクリアする
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPageClear()
        '全社のデータをクリアする
        Me.numKeikakuKensuu.Value = ""
        Me.numKeikakuUriKingaku.Value = ""
        Me.numKeikakuArari.Value = ""
        Me.txtKome.Text = ""

        '支店別の年度をクリアする
        Me.lblNendo.Text = ""

        'ヘッダ部の様式をクリアする
        Me.tdMeisaiHead.Attributes.Add("style", "")

        Me.grdMeisai.DataSource = Nothing
        Me.grdMeisai.DataBind()

        Me.btnKensuuCopy.Enabled = False
        Me.btnUriKingakuCopy.Enabled = False
        Me.btnArariCopy.Enabled = False

        Me.btnAllSave.Enabled = True
        Me.btnAllConfirm.Enabled = True
        Me.btnSitenbetuSave.Enabled = True
        Me.btnSitenbetuConfirm.Enabled = True

        Call SetSumData()

        Me.tbSum.Visible = False
    End Sub
#End Region

    
End Class
