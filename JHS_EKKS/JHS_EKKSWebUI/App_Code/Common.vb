Imports Microsoft.VisualBasic
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Public Class Common
    Private CommonBC As New CommonBC

#Region "定数"
    ''' <summary>
    ''' 月の期間範囲
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MonthKikan As Integer
        All = 1                     '全年度
        MonthTopHalf = 2            '上期(4月 - 9月)
        MonthDownHalf = 3           '下期(10月 - 3月)
        Month4_6 = 4                '四半期(4,5,6月)
        Month7_9 = 5                '四半期(7,8,9月)
        Month10_12 = 6              '四半期(10,11,12月)
        Month1_3 = 7                '四半期(1,2,3月)
        Other = 0                   'その他期間
    End Enum

#End Region

    ''' <summary>
    ''' アクセス記録の保持
    ''' </summary>
    ''' <param name="objPage">ページ</param>
    ''' <param name="strUserId">ユーザーコード</param>
    ''' <param name="strSousa">操作記録</param>
    ''' <remarks></remarks>
    Public Sub SetURL(ByVal objPage As Object, ByVal strUserId As String, ByVal strSousa As String)
        EMAB.AddMethodEntrance(objPage.Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                       MyMethod.GetCurrentMethod.Name, objPage, strUserId, strSousa)
        CommonBC.SetUserInfo(objPage.Request.Url.OriginalString, strUserId, strSousa)
    End Sub
    ''' <summary>
    ''' データ取得時
    ''' </summary>
    ''' <param name="objPage">ページ</param>
    ''' <param name="functionName">関数名</param>
    ''' <remarks></remarks>
    Public Function RunSub(ByVal objPage As Page, ByVal functionName As String) As Boolean

        EMAB.AddMethodEntrance(objPage.Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, objPage, functionName)

        Dim csScript As New StringBuilder
        Dim pPage As Page = objPage
        Dim pType As Type = pPage.GetType
        Dim fname As String = functionName.Replace("(", String.Empty).Replace(")", String.Empty).Replace(";", String.Empty).Trim
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod(fname)

        If Not methodInfo Is Nothing Then
            Return Convert.ToBoolean(methodInfo.Invoke(pPage, New Object() {}))
        End If
        Return False
    End Function
    ''' <summary>システムを取得</summary>
    Public Function GetSystemDate() As Date
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyClass.GetType.BaseType.FullName & "." & _
                       MyMethod.GetCurrentMethod.Name)
        Return Convert.ToDateTime(CommonBC.SelSystemDate.Rows(0).Item(0).ToString)
    End Function

    ''' <summary>システムの年度を取得(会計年度)</summary>
    Public Function GetSystemYear() As String
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyClass.GetType.BaseType.FullName & "." & _
               MyMethod.GetCurrentMethod.Name)
        Return Convert.ToString(DateAdd(DateInterval.Month, -3, Me.GetSystemDate()).Year)
    End Function

    ''' <summary>システムの年度を取得(会計年度)</summary>
    Public Function GetSystemYear(ByVal dtSysDate As DateTime) As String
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyClass.GetType.BaseType.FullName & "." & _
               MyMethod.GetCurrentMethod.Name)
        Return Convert.ToString(DateAdd(DateInterval.Month, -3, dtSysDate).Year)
    End Function

    ''' <summary>
    ''' メッセージを表示する
    ''' </summary>
    ''' <param name="objPage">画面</param>
    ''' <param name="strMsgCd">メッセージ番号</param>
    ''' <param name="args">メッセージ参数</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　李宇(大連情報システム部)　新規作成</history>
    Public Sub SetShowMessage(ByVal objPage As Page, ByVal strMsgCd As String, ByVal ParamArray args() As Object)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(objPage.Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                       MyMethod.GetCurrentMethod.Name, objPage, strMsgCd, args)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            '.AppendLine("setTimeout('fncShowMessage();',10);")
            '.AppendLine("function fncShowMessage()")
            '.AppendLine("{")
            .AppendLine("  alert('" & String.Format(strMsgCd, args) & "');")
            '.AppendLine("}")

            .AppendLine("</script>")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        objPage.ClientScript.RegisterStartupScript(objPage.GetType, strMsgCd, csScript.ToString)

    End Sub

    ''' <summary>
    ''' 金額をカンマ区切り文字列で表示する(小数点考慮版)
    ''' </summary>
    ''' <param name="money">金額</param>
    ''' <param name="fixedpoint">小数部の桁数(有効桁数以下は切捨て)</param>
    ''' <returns>フォーマット後文字列</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/06/07 P-42186 李宇 新規作成 </para>
    ''' </history>
    Public Function FormatComma(ByVal money As String, ByVal fixedpoint As Integer) As String

        If String.IsNullOrEmpty(money) = True Then

            Return String.Empty
        Else
            money = money.Replace("&nbsp;", String.Empty)
            Return FormatNumber(money, fixedpoint)
        End If

    End Function

    ''' <summary>
    ''' 月の期間を判断する
    ''' </summary>
    ''' <param name="intFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMonthKikan(ByVal intFlg As MonthKikan) As ArrayList
        Dim intBeginMonth As Integer
        Dim intEndMonth As Integer

        Select Case intFlg
            Case MonthKikan.All
                intBeginMonth = 4
                intEndMonth = 3
            Case MonthKikan.MonthTopHalf
                intBeginMonth = 4
                intEndMonth = 9
            Case MonthKikan.MonthDownHalf
                intBeginMonth = 10
                intEndMonth = 3
            Case MonthKikan.Month1_3
                intBeginMonth = 1
                intEndMonth = 3
            Case MonthKikan.Month4_6
                intBeginMonth = 4
                intEndMonth = 6
            Case MonthKikan.Month7_9
                intBeginMonth = 7
                intEndMonth = 9
            Case MonthKikan.Month10_12
                intBeginMonth = 10
                intEndMonth = 12
        End Select

        Return GetMonthKikan(intBeginMonth, intEndMonth)
    End Function

    ''' <summary>
    ''' 月の期間を戻る
    ''' </summary>
    ''' <param name="intBeginMonth">開始月</param>
    ''' <param name="intEndMonth">終了月</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMonthKikan(ByVal intBeginMonth As Integer, ByVal intEndMonth As Integer) As ArrayList
        Dim arrList As ArrayList
        Dim intBeginM As Integer
        Dim intEndM As Integer
        Dim i As Integer

        arrList = New ArrayList
        If intBeginMonth > intEndMonth Then
            intBeginM = intBeginMonth
            intEndM = intEndMonth + 12
        Else
            intBeginM = intBeginMonth
            intEndM = intEndMonth
        End If

        For i = intBeginM To intEndM
            If i > 12 Then
                arrList.Add(i - 12)
            Else
                arrList.Add(i)
            End If
        Next

        Return arrList
    End Function

    ''' <summary>
    ''' 数値を変換する
    ''' </summary>
    ''' <param name="objValue">数値対象</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLongFromObj(ByVal objValue As Object) As Int64
        If objValue Is Nothing Then
            Return 0
        End If

        If Convert.ToString(objValue).Equals(String.Empty) Then
            Return 0
        End If

        If Not IsNumeric(objValue) Then
            Return 0
        End If

        Return Convert.ToInt64(objValue)
    End Function

    ''' <summary>
    ''' 数値(小数)を変換する
    ''' </summary>
    ''' <param name="objValue">数値対象</param>
    ''' <param name="intNumCount">小数桁数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDoubleFromObj(ByVal objValue As Object, Optional ByVal intNumCount As Integer = 1) As Double
        If objValue Is Nothing Then
            Return 0
        End If

        If Convert.ToString(objValue).Equals(String.Empty) Then
            Return 0
        End If

        If Not IsNumeric(objValue) Then
            Return 0
        End If

        Return Math.Round(Convert.ToDouble(objValue), intNumCount)
    End Function
End Class
