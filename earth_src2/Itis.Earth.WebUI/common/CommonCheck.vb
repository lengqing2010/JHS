Imports Itis.Earth.BizLogic
Public Enum kbn As Integer
    HANKAKU = 1
    ZENKAKU = 2

End Enum
Public Class CommonCheck

    Function CommonNinnsyou(ByRef UserId As String, ByVal strNinsyou As String) As Boolean
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim Context As HttpContext = HttpContext.Current
        Dim Ninsyou As New BizLogic.Ninsyou
        Dim user_info As New LoginUserInfo
        Dim intCount As Integer

        jBn.userAuth(user_info)

        ' ユーザー基本認証
        Context.Items("strFailureMsg") = Messages.Instance.MSG2024E '（"該当ユーザーがありません。"）
        If Ninsyou.GetUserID() = "" Then
            Context.Server.Transfer("./CommonErr.aspx")
        End If
        Context.Items("strFailureMsg") = Messages.Instance.MSG2020E '（"権限がありません。"）
        If user_info Is Nothing Then
            UserId = Ninsyou.GetUserID()
        Else
            UserId = user_info.LoginUserId
        End If
        Dim dtAccountTable As DataAccess.CommonSearchDataSet.AccountTableDataTable

        If user_info Is Nothing Then
            Context.Server.Transfer("./CommonErr.aspx")
            Return False
        Else
            If strNinsyou = "" Then
                Context.Server.Transfer("./CommonErr.aspx")
                Return False
            Else
                dtAccountTable = CheckKengen(user_info.AccountNo)
                If dtAccountTable.Rows.Count = 0 Then
                    Context.Server.Transfer("./CommonErr.aspx")
                    Return False
                End If
                For intCount = 0 To Split(strNinsyou, ",").Length - 1
                    If dtAccountTable.Rows(0).Item(Split(strNinsyou, ",")(intCount)) = -1 Then
                        Return True
                    End If
                Next

            End If

        End If
        Return False
    End Function
    ''' <summary>
    ''' 必須入力チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckHissuNyuuryoku(ByVal inTarget As String, ByVal PARAM As String) As String
        If inTarget.Trim = "" Then
            Return Messages.Instance.MSG013E.Replace("@PARAM1", PARAM)
        Else
            Return ""
        End If
    End Function
    ''' <summary>
    ''' 日付範囲チェック
    ''' </summary>
    ''' <param name="kaisiHiduke"></param>
    ''' <param name="syuuryouHiduke"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckHidukeHani(ByVal kaisiHiduke As Date, ByVal syuuryouHiduke As Date, ByVal PARAM As String) As String
        If Date.Compare(kaisiHiduke, syuuryouHiduke) > 0 Then
            Return String.Format(Messages.Instance.MSG2012E, PARAM, PARAM).ToString
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' 有効日付チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckYuukouHiduke(ByVal inTarget As String, ByVal PARAM As String) As String

        If inTarget = String.Empty Then
            Return String.Empty
        End If
        Dim data As Date
        Try
            data = Convert.ToDateTime(inTarget)
            If data > EarthConst.Instance.dateMax Or data < EarthConst.Instance.dateMin Then
                Return Messages.Instance.MSG014E.Replace("@PARAM1", PARAM)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG014E.Replace("@PARAM1", PARAM)
        End Try
    End Function
    ''' <summary>
    ''' バイト数チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="intLength"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckByte(ByVal inTarget As String, ByVal intLength As Long, ByVal PARAM As String, Optional ByVal type As kbn = kbn.HANKAKU) As String
        Dim jBn As New Jiban
        '値のCheckを行なう
        If inTarget = String.Empty Then
            Return ""
        End If

        '文字数チェック
        If Not jBn.byteCheckSJIS(inTarget, intLength) Then
            Dim csScript As New StringBuilder

            'MsgBox 表示
            If type = kbn.HANKAKU Then
                Return String.Format(Messages.Instance.MSG2003E, PARAM, intLength)
            Else
                '全角　{0}に登録できる文字数は、全角{1}文字以内です。
                Return String.Format(Messages.Instance.MSG2002E, PARAM, intLength / 2)
            End If
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' 全角チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckZenkaku(ByVal inTarget As String, ByVal PARAM As String) As String

        If Not (inTarget.Length * 2 = Encoding.Default.GetByteCount(inTarget)) Then
            Return String.Format(Messages.Instance.MSG2007E, PARAM).ToString
        Else
            Return ""
        End If
    End Function
    ''' <summary>
    '''  半角数字チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <param name="kbn">1:整数チェック</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckHankaku(ByVal inTarget As String, ByVal PARAM As String, Optional ByVal kbn As String = "") As String
        If inTarget.Length = Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            If kbn = "1" Then
                If InStr(inTarget, ".") > 0 Or InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                    Return String.Format(Messages.Instance.MSG2011E, PARAM).ToString
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Else
            If kbn = "1" Then
                Return String.Format(Messages.Instance.MSG2011E, PARAM).ToString
            Else
                Return String.Format(Messages.Instance.MSG2006E, PARAM).ToString
            End If

        End If
    End Function
    ''' <summary>
    '''  整数チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <param name="kbn">1:マイナス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckNum(ByVal inTarget As String, ByVal PARAM As String, Optional ByVal kbn As String = "") As String
        Dim strTarget As String = ""
        strTarget = Replace(inTarget, ",", "") & ""
        If strTarget.Length = Encoding.Default.GetByteCount(strTarget) And IsNumeric(strTarget) And InStr(strTarget, ".") = 0 And InStr(strTarget, "+") = 0 Then
            If kbn = "" Then
                If InStr(strTarget, "-") > 0 Then
                    Return String.Format(Messages.Instance.MSG091E, PARAM, "金額")
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Else
            Return String.Format(Messages.Instance.MSG2011E, PARAM).ToString
        End If
    End Function
    ''' <summary>
    ''' 半角英数字チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChkHankakuEisuuji(ByVal inTarget As String, ByVal PARAM As String) As String
        Dim intCount As Long = 0
        Dim strTmp As String = 0
        ChkHankakuEisuuji = ""
        For intCount = 1 To Len(inTarget)
            strTmp = Mid(inTarget, intCount, 1)
            If Not ((Asc(strTmp) >= Asc("a") And Asc(strTmp) <= Asc("z")) _
                Or (Asc(strTmp) >= Asc("A") And Asc(strTmp) <= Asc("Z")) _
                Or (Asc(strTmp) >= Asc("0") And Asc(strTmp) <= Asc("9"))) Then
                Return String.Format(Messages.Instance.MSG2005E, PARAM).ToString
            End If
        Next

    End Function
    ''' <summary>
    ''' カタカナチェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckKatakana(ByVal inTarget As String, ByVal PARAM As String, Optional ByVal blnKbn As Boolean = False) As String
        Dim chkstr As String
        Dim meisai_next_cnt As Integer
        CheckKatakana = ""
        chkstr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnmｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｯｬｭｮ~!@#$%^&*()_+`-={}|:<>?[]\;,./ ｰｶﾞｷﾞｸﾞｹﾞｺﾞｻﾞｼﾞｽﾞｾﾞｿﾞﾀﾞﾃﾞﾄﾞﾊﾞﾋﾞﾌﾞﾍﾞﾎﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ'"

        For meisai_next_cnt = 1 To Len(inTarget)
            If InStr(chkstr, Mid(inTarget, meisai_next_cnt, 1)) = 0 Then
                If blnKbn Then
                    If Mid(inTarget, meisai_next_cnt, 1) <> "*" Then
                        Return String.Format(Messages.Instance.MSG2010E, PARAM).ToString
                    End If
                Else
                    Return String.Format(Messages.Instance.MSG2010E, PARAM).ToString
                End If

            End If
        Next
    End Function
    ''' <summary>
    ''' 禁則文字チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckKinsoku(ByVal inTarget As String, ByVal PARAM As String) As String
        Dim jBn As New Jiban

        '禁則文字チェック
        If Not jBn.kinsiStrCheck(inTarget) Then
            Return Messages.Instance.MSG015E.Replace("@PARAM1", PARAM)
        Else
            Return ""
        End If
    End Function


    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="strAccountNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckKengen(ByVal strAccountNo As String) As Itis.Earth.DataAccess.CommonSearchDataSet.AccountTableDataTable
        Dim CommonSearchBL As New CommonSearchLogic
        Dim strReturn As String = ""
        Return CommonSearchBL.GetKengen(strAccountNo)
    End Function
    Sub SetURL(ByVal objPage As Object, ByVal strUserId As String)
        Dim CommonSearchBL As New CommonSearchLogic
        CommonSearchBL.SetInsLog(objPage.Request.Url.OriginalString, strUserId)
    End Sub
    ''' <summary>
    ''' 半角小文字に変換する。
    ''' </summary>
    ''' <param name="value">value</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetTokomozi(ByVal value As String) As String
        Dim komoziString As String = "ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｯｬｭｮ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnmｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｯｬｭｮ~!@#$%^&*()_+`-={}|:<>?[]\;,./ "
        Dim daiString As String = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをんぁぃぅぇぉっゃゅょ０１２３４５６７８９ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺｑｗｅｒｔｙｕｉｏｐａｓｄｆｇｈｊｋｌｚｘｃｖｂｎｍアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲンァィゥェォッャュョ～！＠＃＄％＾＆＊（）＿＋‘－＝｛｝｜：＜＞？「」￥；、。・　"
        Dim i As Integer
        Dim str As Char
        Dim str1 As String = ""
        Dim intIndex As Integer

        For i = 0 To value.Length - 1

            str = value.Chars(i)
            intIndex = daiString.IndexOf(str)
            If intIndex <> -1 Then
                str1 = str1 & komoziString.Chars(intIndex)
            Else
                str1 = str1 & str
            End If

        Next

        str1 = str1.Replace("が", "ｶﾞ")

        str1 = str1.Replace("ぎ", "ｷﾞ")
        str1 = str1.Replace("ぐ", "ｸﾞ")
        str1 = str1.Replace("げ", "ｹﾞ")
        str1 = str1.Replace("ご", "ｺﾞ")

        str1 = str1.Replace("ざ", "ｻﾞ")
        str1 = str1.Replace("じ", "ｼﾞ")
        str1 = str1.Replace("ず", "ｽﾞ")
        str1 = str1.Replace("ぜ", "ｾﾞ")
        str1 = str1.Replace("ぞ", "ｿﾞ")

        str1 = str1.Replace("だ", "ﾀﾞ")
        str1 = str1.Replace("で", "ﾃﾞ")
        str1 = str1.Replace("ど", "ﾄﾞ")

        str1 = str1.Replace("ば", "ﾊﾞ")
        str1 = str1.Replace("び", "ﾋﾞ")
        str1 = str1.Replace("ぶ", "ﾌﾞ")
        str1 = str1.Replace("べ", "ﾍﾞ")
        str1 = str1.Replace("ぼ", "ﾎﾞ")

        str1 = str1.Replace("ぱ", "ﾊﾟ")
        str1 = str1.Replace("ぴ", "ﾋﾟ")
        str1 = str1.Replace("ぷ", "ﾌﾟ")
        str1 = str1.Replace("ぺ", "ﾍﾟ")
        str1 = str1.Replace("ぽ", "ﾎﾟ")

        str1 = str1.Replace("ガ", "ｶﾞ")

        str1 = str1.Replace("ギ", "ｷﾞ")
        str1 = str1.Replace("グ", "ｸﾞ")
        str1 = str1.Replace("ゲ", "ｹﾞ")
        str1 = str1.Replace("ゴ", "ｺﾞ")

        str1 = str1.Replace("ザ", "ｻﾞ")
        str1 = str1.Replace("ジ", "ｼﾞ")
        str1 = str1.Replace("ズ", "ｽﾞ")
        str1 = str1.Replace("ゼ", "ｾﾞ")
        str1 = str1.Replace("ゾ", "ｿﾞ")

        str1 = str1.Replace("ダ", "ﾀﾞ")
        str1 = str1.Replace("デ", "ﾃﾞ")
        str1 = str1.Replace("ド", "ﾄﾞ")

        str1 = str1.Replace("バ", "ﾊﾞ")
        str1 = str1.Replace("ビ", "ﾋﾞ")
        str1 = str1.Replace("ブ", "ﾌﾞ")
        str1 = str1.Replace("ベ", "ﾍﾞ")
        str1 = str1.Replace("ボ", "ﾎﾞ")

        str1 = str1.Replace("パ", "ﾊﾟ")
        str1 = str1.Replace("ピ", "ﾋﾟ")
        str1 = str1.Replace("プ", "ﾌﾟ")
        str1 = str1.Replace("ペ", "ﾍﾟ")
        str1 = str1.Replace("ポ", "ﾎﾟ")

        str1 = str1.Replace("ー", "ｰ")
        Return str1

    End Function
    '馬艶軍追加↓
    ''' <summary>
    '''  半角数字とハイフンチェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <param name="kbn">1:整数チェック</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckHankakuHaifun(ByVal inTarget As String, ByVal PARAM As String, Optional ByVal kbn As String = "") As String
        inTarget = inTarget.Replace("-", "")
        If inTarget.Length = Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            If kbn = "1" Then
                If InStr(inTarget, ".") > 0 Or InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                    Return String.Format(Messages.Instance.MSG2011E, PARAM).ToString
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Else
            If kbn = "1" Then
                Return String.Format(Messages.Instance.MSG2011E, PARAM).ToString
            Else
                Return String.Format(Messages.Instance.MSG2006E, PARAM).ToString
            End If

        End If
    End Function
    ''' <summary>
    ''' 締め日チェックと月数チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckSime(ByVal inTarget As String, ByVal PARAM As String, ByVal maxValue As String) As String

        If inTarget = String.Empty Then
            Return String.Empty
        End If
        If inTarget.Length = Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) And InStr(inTarget, ".") = 0 And InStr(inTarget, ",") = 0 Then
            If InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                Return Messages.Instance.MSG014E.Replace("@PARAM1", PARAM)
            Else
                If CType(inTarget, Int16) > maxValue Or CType(inTarget, Int16) <= 0 Then
                    Return Messages.Instance.MSG014E.Replace("@PARAM1", PARAM)
                Else
                    Return String.Empty
                End If
            End If
        Else
            Return Messages.Instance.MSG014E.Replace("@PARAM1", PARAM)
        End If

    End Function

    ''' <summary>
    ''' 回収予定月数チェック-数値範囲は0～12まで（整数のみ）
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <history>2010/12/02 馬艶軍　追加</history>
    Function CheckSime1(ByVal inTarget As String, ByVal PARAM As String, ByVal maxValue As String) As String

        If inTarget = String.Empty Then
            Return String.Empty
        End If
        If inTarget.Length = Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) And InStr(inTarget, ".") = 0 And InStr(inTarget, ",") = 0 Then
            If InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                Return Messages.Instance.MSG014E.Replace("@PARAM1", PARAM)
            Else
                If CType(inTarget, Int16) > maxValue Or CType(inTarget, Int16) < 0 Then
                    Return Messages.Instance.MSG014E.Replace("@PARAM1", PARAM)
                Else
                    Return String.Empty
                End If
            End If
        Else
            Return Messages.Instance.MSG014E.Replace("@PARAM1", PARAM)
        End If

    End Function

    ''' <summary>
    ''' 数値範囲は1～100までチェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckSuuti(ByVal inTarget As String, ByVal PARAM As String, ByVal maxValue As String) As String

        If inTarget = String.Empty Then
            Return String.Empty
        End If
        If inTarget.Length = Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) And InStr(inTarget, ".") = 0 And InStr(inTarget, ",") = 0 Then
            If InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                Return String.Format(Messages.Instance.MSG2039E, PARAM).ToString()
            Else
                If CType(inTarget, Int16) > maxValue Or CType(inTarget, Int16) < 1 Then
                    Return String.Format(Messages.Instance.MSG2039E, PARAM).ToString()
                Else
                    Return String.Empty
                End If
            End If
        Else
            Return String.Format(Messages.Instance.MSG2039E, PARAM).ToString()
        End If

    End Function

    ''' <summary>
    ''' 割合チェック
    ''' </summary>
    ''' <param name="inTarget1"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckWariai(ByVal inTarget1 As String, ByVal inTarget2 As String, ByVal inTarget3 As String, ByVal PARAM As String, ByVal maxValue As String) As String

        If inTarget1 = String.Empty And inTarget2 = String.Empty And inTarget3 = String.Empty Then
            Return String.Empty
        End If

        If inTarget1 = String.Empty Then
            inTarget1 = 0
        End If
        If inTarget2 = String.Empty Then
            inTarget2 = 0
        End If
        If inTarget3 = String.Empty Then
            inTarget3 = 0
        End If

        If (CType(inTarget1, Int16) + CType(inTarget2, Int16) + CType(inTarget3, Int16)).ToString <> maxValue Then
            Return String.Format(Messages.Instance.MSG2038E, PARAM).ToString()
        Else
            Return String.Empty
        End If

    End Function

    '馬艶軍追加↑

    ''' <summary>
    '''  小数チェック
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/17 車龍 407553の対応 追加</history>
    Function CheckSyousuu(ByVal inTarget As String, ByVal PARAM1 As String, ByVal PARAM2 As Integer, ByVal PARAM3 As Integer) As String

        Dim strMessage As String = Messages.Instance.MSG027E.Replace("@PARAM1", PARAM1).Replace("@PARAM2", PARAM2.ToString()).Replace("@PARAM3", PARAM3.ToString())

        If inTarget.Length = Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            If InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                Return strMessage
            Else
                If InStr(inTarget, ".") > 0 Then
                    '小数の場合
                    If (inTarget.Split(".")(0).Length > PARAM2) OrElse (inTarget.Split(".")(1).Length > PARAM3) Then
                        Return strMessage
                    End If
                Else
                    '整数の場合
                    If (inTarget.Length > PARAM2) Then
                        Return strMessage
                    End If
                End If
            End If
        Else
            Return strMessage
        End If

        Return String.Empty

    End Function
End Class
