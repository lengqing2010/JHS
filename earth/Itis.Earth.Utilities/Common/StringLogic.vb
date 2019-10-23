Imports System.Web.UI
Imports System.Text
Imports Itis.ApplicationBlocks.ExceptionManagement
''' <summary>
''' 文字列操作クラス
''' </summary>
''' <remarks></remarks>
Public Class StringLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "対象文字列に外字が存在しないかチェック"
    ''' <summary>
    ''' 対象文字列に外字が存在しないかチェックします
    ''' </summary>
    ''' <param name="strChkObj">チェック対象文字列</param>
    ''' <returns>チェック結果（True：外字なし／False：外字あり）</returns>
    ''' <remarks></remarks>
    Public Function GaijiExistCheck(ByVal strChkObj As String) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GaijiExistCheck" _
                                            , strChkObj)
        Dim chrChkObj As Char = ""
        Dim intAsc As Integer = 0

        For intChrCnt As Integer = 0 To strChkObj.Length - 1
            chrChkObj = strChkObj.Substring(intChrCnt)
            intAsc = Asc(chrChkObj)

            If intAsc <= EarthConst.GAIJI_CODE_MAX AndAlso intAsc >= EarthConst.GAIJI_CODE_MIN Then
                Return False
                Exit Function
            End If
        Next

        Return True
    End Function
#End Region

#Region "文字列のバイト数を取得（Shift-JIS）"
    ''' <summary>
    ''' 文字列のバイト数を取得（Shift-JIS）
    ''' </summary>
    ''' <param name="str">対象文字列</param>
    ''' <returns>Integer：バイト数</returns>
    ''' <remarks></remarks>
    Public Function GetStrByteSJIS(ByVal str As String) As Integer

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'Shift-JISでのバイト数を取得
        Return hEncoding.GetByteCount(str)

    End Function
#End Region

#Region "Substringのバイト版（Shift-JIS）"
    ''' <summary>
    ''' Substringのバイト切り版（Shift-JIS）
    '''   ※開始位置で文字が切れる場合、その次の文字から開始※
    '''   ※終了位置で文字が切れる場合、その前の文字までを使用※
    ''' </summary>
    ''' <param name="strTarget">元文字列</param>
    ''' <param name="intStart">開始位置（バイト数指定）</param>
    ''' <param name="intByteSize">終了位置（バイト数指定）</param>
    ''' <returns>切り取った文字列</returns>
    ''' <remarks></remarks>
    Public Function SubstringByByte(ByVal strTarget As String, ByVal intStart As Integer, ByVal intByteSize As Integer) As String

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim btBytes As Byte() = hEncoding.GetBytes(strTarget)

        '文字バイト数より開始位置が多い場合や、切り出しバイト数がゼロの場合、空を返す
        If GetStrByteSJIS(strTarget) < intStart OrElse intByteSize = 0 Then
            Return String.Empty
        End If

        Dim strReturn As String = String.Empty
        Dim tmpString As String = String.Empty

        '開始位置を取得(開始位置で文字が切れる場合、その次の文字から開始)
        For lc As Integer = 0 To strTarget.Length - 1
            If GetStrByteSJIS(tmpString) >= intStart Then
                strReturn = strTarget.Substring(lc)
                Exit For
            Else
                tmpString += strTarget.Substring(lc, 1)
            End If
        Next

        '終了位置までの文字を取得(終了位置で文字が切れる場合、その前の文字までを使用)
        For lcE As Integer = 0 To strReturn.Length
            If GetStrByteSJIS(strReturn.Substring(0, lcE)) > intByteSize Then
                strReturn = strReturn.Substring(0, lcE - 1)
                Exit For
            End If
        Next

        Return strReturn

    End Function
#End Region

    ''' <summary>
    ''' DB登録用(地盤テーブル.更新者)に文字列成型する
    ''' </summary>
    ''' <param name="strUserID">ログインユーザID</param>
    ''' <param name="dtUpdDate">更新日付(yy/mm/dd hh:mm)</param>
    ''' <returns>ログインユーザID(最大15桁) + "$(区切り文字)" + 更新日付(yy/mm/dd hh:mm)</returns>
    ''' <remarks>ログインユーザIDが15桁超の場合、15桁で切る</remarks>
    Public Function GetKousinsya(ByVal strUserID As String, ByVal dtUpdDate As DateTime) As String
        Dim intByteChk As Integer = 15 'バイト数チェック用桁数(15桁)
        Dim intRet As Integer = 0 'バイト数戻り値用
        Dim strRetUserID As String = "" '作業用UserID
        Dim strRetUpdDate As String = ""

        'ログインユーザIDの15桁チェックを行なう
        intRet = GetStrByteSJIS(strUserID)
        If intRet > intByteChk Then '15桁以上の場合、15桁で切る
            Dim strTmp As String = ""
            Dim intByte As Integer = 0

            For intCnt As Integer = 0 To strUserID.Length - 1
                strTmp = strRetUserID & strUserID.Substring(intCnt, 1)
                intByte = GetStrByteSJIS(strTmp)
                If intByte > intByteChk Then '15バイトより大きい場合
                    Exit For
                Else '15バイト未満の場合
                    strRetUserID = strTmp
                End If
            Next

        Else '15桁未満
            strRetUserID = strUserID

        End If

        '更新日付をフォーマットする
        strRetUpdDate = Format(dtUpdDate, EarthConst.FORMAT_DATE_TIME_4)

        'ログインユーザID(最大15桁) + "$(区切り文字)" + 更新日付(yy/mm/dd hh:mm)を返却する
        Return strRetUserID & "$" & strRetUpdDate
    End Function

    ''' <summary>
    ''' 特殊文字をエスケープ文字に変換する。
    ''' </summary>
    ''' <param name="strTarget">対象文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RemoveSpecStr(ByVal strTarget As String, Optional ByVal flgAddKakko As Boolean = False) As String

        '既にエスケープ済みの文字を初期化
        strTarget = strTarget.Replace("\'", "'").Replace("\""", """").Replace("\r\n", vbCrLf)

        '特殊文字をエスケープ
        strTarget = strTarget.Replace("'", "\'").Replace("""", "\""").Replace(vbCrLf, "\r\n")
        strTarget = strTarget.Replace(vbCr, "\r\n").Replace(vbLf, "\r\n")

        '文字列を括弧で囲む場合
        If flgAddKakko Then
            strTarget = "【" & strTarget & "】"
        End If

        Return strTarget

    End Function

    ''' <summary>
    ''' 指定したバイト数で文字列を切り捨てます
    ''' </summary>
    ''' <param name="strTarget">対象文字列</param>
    ''' <param name="intLimitBytes">取得バイト数</param>
    ''' <returns>指定したバイと数までで切り捨てた文字列</returns>
    ''' <remarks></remarks>
    Public Function StringCutter(ByVal strTarget As String, ByVal intLimitBytes As Integer) As String
        Dim strRetString As String

        strRetString = ""

        If strTarget Is Nothing Then
            Return strRetString
            Exit Function
        End If

        For intCnt As Integer = 0 To strTarget.Length - 1
            Dim strTemp As String = strRetString & strTarget.Substring(intCnt, 1)
            If Me.GetStrByteSJIS(strTemp) > intLimitBytes Then
                Exit For
            Else
                strRetString = strTemp
            End If
        Next

        Return strRetString
    End Function

    ''' <summary>
    ''' 物件NO(区分 + 番号)を区分と番号に分割して返却する
    ''' </summary>
    ''' <param name="strBukkenNo">物件NO(区分 + 番号)</param>
    ''' <returns>区分,番号</returns>
    ''' <remarks></remarks>
    Public Function GetSepBukkenNo(ByVal strBukkenNo As String) As String()
        Dim strRet(1) As String   '返却用
        Dim strKbn As String = String.Empty '区分
        Dim strBangou As String = String.Empty '番号

        If strBukkenNo.Trim.Length <> 0 Then
            If strBukkenNo.Length = 11 Then
                strKbn = strBukkenNo.Substring(0, 1)
                strBangou = strBukkenNo.Substring(1, 10)
                strRet(0) = strKbn
                strRet(1) = strBangou
            End If
        End If
        Return strRet
    End Function

    ''' <summary>
    ''' 改行コードを文字置換する
    ''' ※置換文字列は指定可
    ''' </summary>
    ''' <param name="strVal">対象文字列</param>
    ''' <param name="strReplace">置換文字列</param>
    ''' <remarks></remarks>
    Public Sub ChgVbCrlfToStr(ByRef strVal As String, Optional ByVal strReplace As String = "")
        '改行変換
        If strVal <> String.Empty Then
            If strReplace = "" Then
                strVal = strVal.Replace(vbCrLf, EarthConst.BRANK_STRING)
            Else
                strVal = strVal.Replace(vbCrLf, strReplace)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Listの内容を文字列化する
    ''' </summary>
    ''' <param name="list">文字列化したいList</param>
    ''' <param name="sepStr">セパレータ文字列(デフォルト：,)</param>
    ''' <returns>文字列</returns>
    ''' <remarks></remarks>
    Public Function listToString(ByVal list As ICollection, Optional ByVal sepStr As String = ",") As String
        Dim sb As New StringBuilder
        For Each obj As Object In list
            sb.Append(obj.ToString() & ",")
        Next
        Return sb.ToString
    End Function

    ''' <summary>
    ''' バイト数チェック(SJIS)
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <param name="max">最大OKバイト数</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks></remarks>
    Public Function ByteCheckSJIS(ByVal target As String, _
                                  ByVal max As Integer) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ByteCheckSJIS", _
                                                    target, _
                                                    max)

        Return Me.GetStrByteSJIS(target) <= max

    End Function

    ''' <summary>
    ''' 禁則文字チェック
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks></remarks>
    Public Function KinsiStrCheck(ByVal target As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".KinsiStrCheck", _
                                                    target)

        For Each st As String In EarthConst.Instance.ARRAY_KINSI_STRING
            If target.IndexOf(st) >= 0 Then
                Return False
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 禁則文字クリア
    ''' </summary>
    ''' <param name="target">チェック対象文字列</param>
    ''' <remarks>第一引数の文字列を、指定の文字列(アンダースコア:"_")に変換する</remarks>
    Public Sub KinsiStrClear(ByRef target As String)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".KinsiStrClear", _
                                                    target)

        For Each st As String In EarthConst.Instance.ARRAY_KINSI_STRING
            target = target.Replace(st, EarthConst.UNDER_SCORE)
        Next

    End Sub

End Class
