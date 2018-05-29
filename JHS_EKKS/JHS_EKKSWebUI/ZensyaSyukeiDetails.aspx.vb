Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports System.Data

''' <summary>
''' 全社 集計 詳細
''' </summary>
''' <remarks>全社 集計 詳細</remarks>
''' <history>
''' <para>2012/11/24 P-44979 王新 新規作成 </para>
''' </history>
Partial Class ZensyaSyukeiDetails
    Inherits System.Web.UI.Page

#Region "プライベート変数"
    'インスタンス生成
    Private objZensyaSyukeiDetailsBC As New Lixil.JHS_EKKS.BizLogic.ZensyaSyukeiDetailsBC
    Private objCommonBC As New Lixil.JHS_EKKS.BizLogic.CommonBC
    Private objCommon As New Common
    Private objCommonCheck As New CommonCheck
#End Region

#Region "変数"
    'メニュー番号
    Private mstrMenuno As String
#End Region

#Region "イベント"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim strYear As String                   '年度
        Dim strKi As String                     '期
        Dim strBeginMonth As String             '開始月
        Dim strEndMonth As String               '終了月
        Dim arrList As ArrayList                '月リスト

        Dim dtKakoJiData As DataTable           '工事比率データ
        Dim dtEigyouData As DataTable           '営業のデータ
        Dim dtSinkiData As DataTable            '新規のデータ
        Dim dtTokuhanData As DataTable          '特販のデータ
        Dim dtFCData As DataTable               'FCのデータ
        Dim dtAllData As DataTable              '全体のデータ
        Dim dtAllFCNozokuData As DataTable      '全体FC除くのデータ

        If Not IsPostBack Then
            mstrMenuno = "U009"         '年度別
            mstrMenuno = "U010"         '期別
            mstrMenuno = "U011"         '月別

            '前画面の引数を取得する
            If Request.QueryString("strYear") Is Nothing Then
                strYear = String.Empty
            Else
                strYear = Request.QueryString("strYear")
            End If

            If Request.QueryString("strKi") Is Nothing Then
                strKi = String.Empty
            Else
                strKi = Request.QueryString("strKi")
            End If

            If Request.QueryString("strBeginMonth") Is Nothing Then
                strBeginMonth = String.Empty
            Else
                strBeginMonth = Request.QueryString("strBeginMonth")
            End If

            If Request.QueryString("strEndMonth") Is Nothing Then
                strEndMonth = strBeginMonth
            Else
                strEndMonth = Request.QueryString("strEndMonth")
                If strEndMonth.Equals(String.Empty) Then
                    strEndMonth = strBeginMonth
                End If
            End If

            'タイトルを設定する
            Me.Title = "全社集計詳細"
            Call SetTitle(strYear, strKi, strBeginMonth, strEndMonth)

            '工事比率を設定する
            dtKakoJiData = objZensyaSyukeiDetailsBC.GetKakoJittusekiKanriData(strYear)
            Call SetKojData(dtKakoJiData)

            '年度、期、月を判断する
            If Not strKi.Equals(String.Empty) Then
                '期別の場合
                arrList = objCommon.GetMonthKikan(CType(strKi, Common.MonthKikan))
            ElseIf Not strBeginMonth.Equals(String.Empty) Then
                '月別の場合
                arrList = objCommon.GetMonthKikan(Convert.ToInt32(strBeginMonth), Convert.ToInt32(strEndMonth))
            Else
                '年度別の場合
                arrList = objCommon.GetMonthKikan(Common.MonthKikan.All)
            End If

            '営業のデータを戻る
            dtEigyouData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "1", arrList)

            '営業部のデータを設定する
            Call SetGridData(dtEigyouData, "grdEigyouMeisai")

            '新規のデータを戻る
            dtSinkiData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "2", arrList)

            '新規部のデータを設定する
            Call SetGridData(dtSinkiData, "grdSinkiMeisai")

            '特販のデータを戻る
            dtTokuhanData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "3", arrList)

            '特販部のデータを設定する
            Call SetGridData(dtTokuhanData, "grdTokuhanMeisai")

            'FCのデータを戻る
            dtFCData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "4", arrList)

            'FC部のデータを設定する
            Call SetGridData(dtFCData, "grdFCMeisai")

            '全体のデータを戻る
            dtAllData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "1,2,3,4", arrList)

            '全体部のデータを設定する
            Call SetGridData(dtAllData, "grdAllMeisai")

            '全体のデータを戻る
            dtAllFCNozokuData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "1,2,3", arrList)

            '全体FC除く部のデータを設定する
            Call SetGridData(dtAllFCNozokuData, "grdAllNotFCMeisai")
        End If
    End Sub


#End Region

#Region "メンッド"
    ''' <summary>
    ''' タイトルを設定する
    ''' </summary>
    ''' <param name="strYear">計画年度</param>
    ''' <param name="strKi">期間</param>
    ''' <param name="strBeginMonth">開始月</param>
    ''' <param name="strEndMonth">終了月</param>
    ''' <remarks></remarks>
    Private Sub SetTitle(ByVal strYear As String, ByVal strKi As String, _
                         ByVal strBeginMonth As String, ByVal strEndMonth As String)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strYear, strKi, strBeginMonth, strEndMonth)

        Dim strTitle As String
        Dim strPara(1) As String
        strTitle = "全社集計 {0} 詳細"

        strPara(0) = strYear

        If Not strKi.Equals(String.Empty) Then
            Select Case Convert.ToInt16(strKi)
                Case 2
                    strPara(1) = "上期 "
                Case 3
                    strPara(1) = "下期 "
                Case 4
                    strPara(1) = "四半期(4,5,6月) "
                Case 5
                    strPara(1) = "四半期(7,8,9月) "
                Case 6
                    strPara(1) = "四半期(10,11,12月) "
                Case 7
                    strPara(1) = "四半期(1,2,3月) "
                Case Else
                    strPara(1) = ""
            End Select
        ElseIf Not strBeginMonth.Equals(String.Empty) Then
            strPara(1) = strBeginMonth & "月 ～ " & strEndMonth & "月 "
        Else
            strPara(1) = ""
        End If

        Me.lblNendo.Text = String.Format("全社 集計 {0}年度 {1}詳細", strPara)
    End Sub

    ''' <summary>
    ''' 工事比率を設定する
    ''' </summary>
    ''' <param name="dtValue">工事比率データ</param>
    ''' <remarks>工事比率を設定する</remarks>
    Private Sub SetKojData(ByVal dtValue As DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtValue)

        If dtValue Is Nothing OrElse dtValue.Rows.Count <= 0 Then
            Me.numKoj1_1.Value = "0"
            Me.numKoj1_2.Value = "0"
            Me.numKoj1_3.Value = "0"

            Me.numKoj2_1.Value = "0"
            Me.numKoj2_2.Value = "0"
            Me.numKoj2_3.Value = "0"

            Me.numKoj3_1.Value = "0"
            Me.numKoj3_2.Value = "0"
            Me.numKoj3_3.Value = "0"

            Me.numKoj4_1.Value = "0"
            Me.numKoj4_2.Value = "0"
            Me.numKoj4_3.Value = "0"

            Me.numKoj5_1.Value = "0"
            Me.numKoj5_2.Value = "0"
            Me.numKoj5_3.Value = "0"

            Me.numKoj6_1.Value = "0"
            Me.numKoj6_2.Value = "0"
            Me.numKoj6_3.Value = "0"
            Exit Sub
        End If

        Dim drTemp() As DataRow                     '調査/工事のデータ

        '営業の場合
        drTemp = dtValue.Select("eigyou_kbn='1'")
        If drTemp.Length > 0 Then
            Me.numKoj1_1.Value = Convert.ToString(drTemp(0).Item("koj_hantei_ritu"))    '工事判定率
            Me.numKoj1_2.Value = Convert.ToString(drTemp(0).Item("koj_jyuchuu_ritu"))   '工事受注率
            Me.numKoj1_3.Value = Convert.ToString(drTemp(0).Item("tyoku_koj_ritu"))     '直工事率
        Else
            Me.numKoj1_1.Value = "0"
            Me.numKoj1_2.Value = "0"
            Me.numKoj1_3.Value = "0"
        End If

        '新規の場合
        drTemp = dtValue.Select("eigyou_kbn='2'")
        If drTemp.Length > 0 Then
            Me.numKoj2_1.Value = Convert.ToString(drTemp(0).Item("koj_hantei_ritu"))    '工事判定率
            Me.numKoj2_2.Value = Convert.ToString(drTemp(0).Item("koj_jyuchuu_ritu"))   '工事受注率
            Me.numKoj2_3.Value = Convert.ToString(drTemp(0).Item("tyoku_koj_ritu"))     '直工事率
        Else
            Me.numKoj2_1.Value = "0"
            Me.numKoj2_2.Value = "0"
            Me.numKoj2_3.Value = "0"
        End If

        '特販の場合
        drTemp = dtValue.Select("eigyou_kbn='3'")
        If drTemp.Length > 0 Then
            Me.numKoj3_1.Value = Convert.ToString(drTemp(0).Item("koj_hantei_ritu"))    '工事判定率
            Me.numKoj3_2.Value = Convert.ToString(drTemp(0).Item("koj_jyuchuu_ritu"))   '工事受注率
            Me.numKoj3_3.Value = Convert.ToString(drTemp(0).Item("tyoku_koj_ritu"))     '直工事率
        Else
            Me.numKoj3_1.Value = "0"
            Me.numKoj3_2.Value = "0"
            Me.numKoj3_3.Value = "0"
        End If

        'FCの場合
        drTemp = dtValue.Select("eigyou_kbn='4'")
        If drTemp.Length > 0 Then
            Me.numKoj4_1.Value = Convert.ToString(drTemp(0).Item("koj_hantei_ritu"))    '工事判定率
            Me.numKoj4_2.Value = Convert.ToString(drTemp(0).Item("koj_jyuchuu_ritu"))   '工事受注率
            Me.numKoj4_3.Value = Convert.ToString(drTemp(0).Item("tyoku_koj_ritu"))     '直工事率
        Else
            Me.numKoj4_1.Value = "0"
            Me.numKoj4_2.Value = "0"
            Me.numKoj4_3.Value = "0"
        End If

        '全体の場合
        Me.numKoj5_1.Value = Convert.ToString(dtValue.Compute("SUM(koj_hantei_ritu)", ""))    '工事判定率
        Me.numKoj5_2.Value = Convert.ToString(dtValue.Compute("SUM(koj_jyuchuu_ritu)", ""))   '工事受注率
        Me.numKoj5_3.Value = Convert.ToString(dtValue.Compute("SUM(tyoku_koj_ritu)", ""))     '直工事率

        '全体FC除くの場合
        Me.numKoj6_1.Value = Convert.ToString(dtValue.Compute("SUM(koj_hantei_ritu)", "eigyou_kbn in ('1','2','3')"))    '工事判定率
        Me.numKoj6_2.Value = Convert.ToString(dtValue.Compute("SUM(koj_jyuchuu_ritu)", "eigyou_kbn in ('1','2','3')"))    '工事判定率
        Me.numKoj6_3.Value = Convert.ToString(dtValue.Compute("SUM(tyoku_koj_ritu)", "eigyou_kbn in ('1','2','3')"))    '工事判定率

    End Sub

    ''' <summary>
    ''' グリッドのデータを設定する
    ''' </summary>
    ''' <param name="dtValue">データ元</param>
    ''' <param name="strGridName">グリッド名</param>
    ''' <remarks></remarks>
    Private Sub SetGridData(ByVal dtValue As DataTable, ByVal strGridName As String)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtValue)

        Dim dtAllValue As DataTable                 '合計部データ
        Dim dtTemp As DataTable                     '各営業区分のデータ
        Dim drTemp() As DataRow                     '調査/工事のデータ
        Dim drValue As DataRow                      '臨時データ
        Dim numSumL(9) As Long                      '合計部データ(整数部)
        Dim numSumD(5) As Decimal                   '合計部データ(小数部)
        Dim intDataCount As Integer                 'レコード数

        'レコード数を設定する
        intDataCount = dtValue.Rows.Count

        '０件の場合、「1」に設定する
        If intDataCount = 0 Then
            intDataCount = 1
        End If

        dtAllValue = dtValue.Clone()

        '調査部のデータを検索する
        dtTemp = dtValue.Clone()
        drTemp = dtValue.Select("bunbetu_cd='1'")
        For Each drValue In drTemp
            dtTemp.ImportRow(drValue)
        Next

        'データ再設定、データ未満の考慮(4行に設定する)
        Call SetFormatDataTable(dtTemp, 4)

        CType(Me.FindControl(strGridName & "1"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "1"), DataList).DataBind()
        CType(Me.FindControl(strGridName & "2"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "2"), DataList).DataBind()

        '工事部のデータを検索する
        dtTemp = dtValue.Clone()
        drTemp = dtValue.Select("bunbetu_cd='2'")
        For Each drValue In drTemp
            dtTemp.ImportRow(drValue)
        Next

        'データ再設定、データ未満の考慮(2行に設定する)
        Call SetFormatDataTable(dtTemp, 2)

        CType(Me.FindControl(strGridName & "3"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "3"), DataList).DataBind()
        CType(Me.FindControl(strGridName & "4"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "4"), DataList).DataBind()

        'その他部のデータを検索する
        dtTemp = dtValue.Clone()
        drTemp = dtValue.Select("bunbetu_cd='9'")
        For Each drValue In drTemp
            dtTemp.ImportRow(drValue)
        Next

        'データ再設定、データ未満の考慮(1行に設定する)
        Call SetFormatDataTable(dtTemp, 1)

        CType(Me.FindControl(strGridName & "5"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "5"), DataList).DataBind()
        CType(Me.FindControl(strGridName & "6"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "6"), DataList).DataBind()

        '合計部のデータ
        numSumL(0) = 0
        numSumL(1) = 0
        numSumL(2) = 0
        numSumL(3) = 0
        numSumL(4) = 0
        numSumL(5) = 0
        numSumL(6) = 0
        numSumL(7) = 0
        numSumL(8) = 0
        numSumL(9) = 0
        numSumD(0) = 0
        numSumD(1) = 0
        numSumD(2) = 0
        numSumD(3) = 0
        numSumD(4) = 0
        numSumD(5) = 0
        For Each drValue In dtValue.Rows
            numSumL(0) = numSumL(0) + GetLongFromObj(drValue("g_jittuseki_kingaku"))    '前年実績金額
            numSumL(1) = numSumL(1) + GetLongFromObj(drValue("g_jittuseki_arari"))      '前年実績粗利

            numSumL(2) = numSumL(2) + GetLongFromObj(drValue("keikaku_kingaku"))        '計画金額
            numSumL(3) = numSumL(3) + GetLongFromObj(drValue("keikaku_arari"))          '計画粗利

            numSumL(4) = numSumL(4) + GetLongFromObj(drValue("mikomi_kingaku"))         '見込金額
            numSumL(5) = numSumL(5) + GetLongFromObj(drValue("mikomi_arari"))           '見込粗利

            numSumL(6) = numSumL(6) + GetLongFromObj(drValue("mikomi_keikaku_kingaku")) '見込-計画金額
            numSumL(7) = numSumL(7) + GetLongFromObj(drValue("mikomi_keikaku_arari"))   '見込-計画粗利

            numSumL(8) = numSumL(8) + GetLongFromObj(drValue("jittuseki_kingaku"))      '実績金額
            numSumL(9) = numSumL(9) + GetLongFromObj(drValue("jittuseki_arari"))        '実績粗利

            numSumD(0) = numSumD(0) + GetDecimalFromObj(drValue("tasseiritu_kensuu"))   '計画達成率件数
            numSumD(1) = numSumD(1) + GetDecimalFromObj(drValue("tasseiritu_kingaku"))  '計画達成率金額
            numSumD(2) = numSumD(2) + GetDecimalFromObj(drValue("tasseiritu_arari"))    '計画達成率粗利

            numSumD(3) = numSumD(3) + GetDecimalFromObj(drValue("sintyokuritu_kensuu")) '見込進捗率件数
            numSumD(4) = numSumD(4) + GetDecimalFromObj(drValue("sintyokuritu_kingaku")) '見込進捗率金額
            numSumD(5) = numSumD(5) + GetDecimalFromObj(drValue("sintyokuritu_arari"))  '見込進捗率粗利
        Next

        drValue = dtAllValue.NewRow()
        '新規とFCの場合、空白に設定する
        If strGridName.Equals("grdSinkiMeisai") OrElse strGridName.Equals("grdFCMeisai") Then
            drValue("g_jittuseki_kingaku") = ""
            drValue("g_jittuseki_arari") = ""
        Else
            drValue("g_jittuseki_kingaku") = numSumL(0)
            drValue("g_jittuseki_arari") = numSumL(1)
        End If

        drValue("keikaku_kingaku") = numSumL(2)
        drValue("keikaku_arari") = numSumL(3)
        drValue("mikomi_kingaku") = numSumL(4)
        drValue("mikomi_arari") = numSumL(5)
        drValue("mikomi_keikaku_kingaku") = numSumL(6)
        drValue("mikomi_keikaku_arari") = numSumL(7)
        drValue("jittuseki_kingaku") = numSumL(8)
        drValue("jittuseki_arari") = numSumL(9)

        drValue("tasseiritu_kensuu") = GetDecimalFromObj(numSumD(0) / intDataCount)
        drValue("tasseiritu_kingaku") = GetDecimalFromObj(numSumD(1) / intDataCount)
        drValue("tasseiritu_arari") = GetDecimalFromObj(numSumD(2) / intDataCount)
        drValue("sintyokuritu_kensuu") = GetDecimalFromObj(numSumD(3) / intDataCount)
        drValue("sintyokuritu_kingaku") = GetDecimalFromObj(numSumD(4) / intDataCount)
        drValue("sintyokuritu_arari") = GetDecimalFromObj(numSumD(5) / intDataCount)

        dtAllValue.Rows.Add(drValue)

        CType(Me.FindControl(strGridName & "Sum"), DataList).DataSource = dtAllValue
        CType(Me.FindControl(strGridName & "Sum"), DataList).DataBind()
    End Sub

    ''' <summary>
    ''' 空白行を追加する
    ''' </summary>
    ''' <param name="dtValue">データ対象</param>
    ''' <param name="intAddRowCount">空白行数</param>
    ''' <remarks></remarks>
    Private Sub SetFormatDataTable(ByRef dtValue As DataTable, ByVal intAddRowCount As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim dr As DataRow

        If Not dtValue Is Nothing Then
            If dtValue.Rows.Count < intAddRowCount Then
                For i = dtValue.Rows.Count To intAddRowCount - 1
                    dr = dtValue.NewRow()
                    For j = 0 To dtValue.Columns.Count - 1
                        dr(j) = DBNull.Value
                    Next

                    dtValue.Rows.Add(dr)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' 数値を戻る
    ''' </summary>
    ''' <param name="objValue">数値対象</param>
    ''' <returns>数値</returns>
    ''' <remarks></remarks>
    Private Function GetLongFromObj(ByVal objValue As Object) As Long
        Dim strValue As String

        strValue = Convert.ToString(objValue)

        'データがNULLの場合
        If strValue.Equals(String.Empty) Then
            Return 0
        End If

        If IsNumeric(strValue) Then
            Return Convert.ToInt64(strValue)
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' 数値を戻る
    ''' </summary>
    ''' <param name="objValue">数値対象</param>
    ''' <returns>数値</returns>
    ''' <remarks></remarks>
    Private Function GetDecimalFromObj(ByVal objValue As Object) As Decimal
        Dim strValue As String

        strValue = Convert.ToString(objValue)

        'データがNULLの場合
        If strValue.Equals(String.Empty) Then
            Return 0
        End If

        If IsNumeric(strValue) Then
            Return Convert.ToDecimal(strValue)
        Else
            Return 0
        End If
    End Function
#End Region

End Class
