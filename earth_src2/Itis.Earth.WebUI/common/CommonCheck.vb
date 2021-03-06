Imports Itis.Earth.BizLogic
Public Enum kbn As Integer
    HANKAKU = 1
    ZENKAKU = 2

End Enum
Public Class CommonCheck

    Function CommonNinnsyou(ByRef UserId As String, ByVal strNinsyou As String) As Boolean
        Dim jBn As New Jiban 'nÕæÊ¤ÊNX
        Dim Context As HttpContext = HttpContext.Current
        Dim Ninsyou As New BizLogic.Ninsyou
        Dim user_info As New LoginUserInfo
        Dim intCount As Integer

        jBn.userAuth(user_info)

        ' [U[î{FØ
        Context.Items("strFailureMsg") = Messages.Instance.MSG2024E 'i"Y[U[ª èÜ¹ñB"j
        If Ninsyou.GetUserID() = "" Then
            Context.Server.Transfer("./CommonErr.aspx")
        End If
        Context.Items("strFailureMsg") = Messages.Instance.MSG2020E 'i" Àª èÜ¹ñB"j
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
    ''' K{üÍ`FbN
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
    ''' útÍÍ`FbN
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
    ''' Løút`FbN
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
    ''' oCg`FbN
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="intLength"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckByte(ByVal inTarget As String, ByVal intLength As Long, ByVal PARAM As String, Optional ByVal type As kbn = kbn.HANKAKU) As String
        Dim jBn As New Jiban
        'lÌCheckðsÈ¤
        If inTarget = String.Empty Then
            Return ""
        End If

        '¶`FbN
        If Not jBn.byteCheckSJIS(inTarget, intLength) Then
            Dim csScript As New StringBuilder

            'MsgBox \¦
            If type = kbn.HANKAKU Then
                Return String.Format(Messages.Instance.MSG2003E, PARAM, intLength)
            Else
                'Sp@{0}Éo^Å«é¶ÍASp{1}¶ÈàÅ·B
                Return String.Format(Messages.Instance.MSG2002E, PARAM, intLength / 2)
            End If
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' Sp`FbN
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
    '''  ¼p`FbN
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <param name="kbn">1:®`FbN</param>
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
    '''  ®`FbN
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <param name="kbn">1:}CiX</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckNum(ByVal inTarget As String, ByVal PARAM As String, Optional ByVal kbn As String = "") As String
        Dim strTarget As String = ""
        strTarget = Replace(inTarget, ",", "") & ""
        If strTarget.Length = Encoding.Default.GetByteCount(strTarget) And IsNumeric(strTarget) And InStr(strTarget, ".") = 0 And InStr(strTarget, "+") = 0 Then
            If kbn = "" Then
                If InStr(strTarget, "-") > 0 Then
                    Return String.Format(Messages.Instance.MSG091E, PARAM, "àz")
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
    ''' ¼pp`FbN
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
    ''' J^Ji`FbN
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckKatakana(ByVal inTarget As String, ByVal PARAM As String, Optional ByVal blnKbn As Boolean = False) As String
        Dim chkstr As String
        Dim meisai_next_cnt As Integer
        CheckKatakana = ""
        chkstr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnm±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜ¦Ý§¨©ª«¯¬­®~!@#$%^&*()_+`-={}|:<>?[]\;,./ °¶Þ·Þ¸Þ¹ÞºÞ»Þ¼Þ½Þ¾Þ¿ÞÀÞÃÞÄÞÊÞËÞÌÞÍÞÎÞÊßËßÌßÍßÎß'"

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
    ''' Ö¥¶`FbN
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckKinsoku(ByVal inTarget As String, ByVal PARAM As String) As String
        Dim jBn As New Jiban

        'Ö¥¶`FbN
        If Not jBn.kinsiStrCheck(inTarget) Then
            Return Messages.Instance.MSG015E.Replace("@PARAM1", PARAM)
        Else
            Return ""
        End If
    End Function


    ''' <summary>
    '''  À`FbN
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
    ''' ¼p¬¶ÉÏ··éB
    ''' </summary>
    ''' <param name="value">value</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetTokomozi(ByVal value As String) As String
        Dim komoziString As String = "±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜ¦Ý§¨©ª«¯¬­®0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnm±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜ¦Ý§¨©ª«¯¬­®~!@#$%^&*()_+`-={}|:<>?[]\;,./ "
        Dim daiString As String = " ¢¤¦¨©«­¯±³µ·¹»½¿ÂÄÆÈÉÊËÌÍÐÓÖÙÜÝÞßàâäæçèéêëíðñ¡£¥§ÁáãåOPQRSTUVWX`abcdefghijklmnopqrstuvwxyACEGIJLNPRTVXZ\^`cegijklmnqtwz}~@BDFHb`IOijQ{e|opbFHuvGABE@"
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

        str1 = str1.Replace("ª", "¶Þ")

        str1 = str1.Replace("¬", "·Þ")
        str1 = str1.Replace("®", "¸Þ")
        str1 = str1.Replace("°", "¹Þ")
        str1 = str1.Replace("²", "ºÞ")

        str1 = str1.Replace("´", "»Þ")
        str1 = str1.Replace("¶", "¼Þ")
        str1 = str1.Replace("¸", "½Þ")
        str1 = str1.Replace("º", "¾Þ")
        str1 = str1.Replace("¼", "¿Þ")

        str1 = str1.Replace("¾", "ÀÞ")
        str1 = str1.Replace("Å", "ÃÞ")
        str1 = str1.Replace("Ç", "ÄÞ")

        str1 = str1.Replace("Î", "ÊÞ")
        str1 = str1.Replace("Ñ", "ËÞ")
        str1 = str1.Replace("Ô", "ÌÞ")
        str1 = str1.Replace("×", "ÍÞ")
        str1 = str1.Replace("Ú", "ÎÞ")

        str1 = str1.Replace("Ï", "Êß")
        str1 = str1.Replace("Ò", "Ëß")
        str1 = str1.Replace("Õ", "Ìß")
        str1 = str1.Replace("Ø", "Íß")
        str1 = str1.Replace("Û", "Îß")

        str1 = str1.Replace("K", "¶Þ")

        str1 = str1.Replace("M", "·Þ")
        str1 = str1.Replace("O", "¸Þ")
        str1 = str1.Replace("Q", "¹Þ")
        str1 = str1.Replace("S", "ºÞ")

        str1 = str1.Replace("U", "»Þ")
        str1 = str1.Replace("W", "¼Þ")
        str1 = str1.Replace("Y", "½Þ")
        str1 = str1.Replace("[", "¾Þ")
        str1 = str1.Replace("]", "¿Þ")

        str1 = str1.Replace("_", "ÀÞ")
        str1 = str1.Replace("f", "ÃÞ")
        str1 = str1.Replace("h", "ÄÞ")

        str1 = str1.Replace("o", "ÊÞ")
        str1 = str1.Replace("r", "ËÞ")
        str1 = str1.Replace("u", "ÌÞ")
        str1 = str1.Replace("x", "ÍÞ")
        str1 = str1.Replace("{", "ÎÞ")

        str1 = str1.Replace("p", "Êß")
        str1 = str1.Replace("s", "Ëß")
        str1 = str1.Replace("v", "Ìß")
        str1 = str1.Replace("y", "Íß")
        str1 = str1.Replace("|", "Îß")

        str1 = str1.Replace("[", "°")
        Return str1

    End Function
    'nRÇÁ«
    ''' <summary>
    '''  ¼pÆnCt`FbN
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <param name="kbn">1:®`FbN</param>
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
    ''' ÷ßú`FbNÆ`FbN
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
    ''' ñû\è`FbN-lÍÍÍ0`12ÜÅi®ÌÝj
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <history>2010/12/02 nR@ÇÁ</history>
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
    ''' lÍÍÍ1`100ÜÅ`FbN
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
    ''' `FbN
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

    'nRÇÁª

    ''' <summary>
    '''  ¬`FbN
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/17 Ô´ 407553ÌÎ ÇÁ</history>
    Function CheckSyousuu(ByVal inTarget As String, ByVal PARAM1 As String, ByVal PARAM2 As Integer, ByVal PARAM3 As Integer) As String

        Dim strMessage As String = Messages.Instance.MSG027E.Replace("@PARAM1", PARAM1).Replace("@PARAM2", PARAM2.ToString()).Replace("@PARAM3", PARAM3.ToString())

        If inTarget.Length = Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            If InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                Return strMessage
            Else
                If InStr(inTarget, ".") > 0 Then
                    '¬Ìê
                    If (inTarget.Split(".")(0).Length > PARAM2) OrElse (inTarget.Split(".")(1).Length > PARAM3) Then
                        Return strMessage
                    End If
                Else
                    '®Ìê
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
