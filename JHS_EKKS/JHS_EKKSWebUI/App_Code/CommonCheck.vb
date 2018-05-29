Imports Lixil.JHS_EKKS.BizLogic
Imports Messages = Lixil.JHS_EKKS.Utilities.CommonMessage
Public Enum kbn As Integer
    HANKAKU = 1
    ZENKAKU = 2
End Enum

''' <summary>
''' 権限
''' </summary>
''' <remarks></remarks>
Public Enum kegen As Integer
    ''' <summary>
    ''' 営業計画管理_参照権限
    ''' </summary>
    ''' <remarks></remarks>
    EigyouKeikakuKenriSansyou = 1
    ''' <summary>
    ''' 売上予実_参照権限
    ''' </summary>
    ''' <remarks></remarks>
    UriYojituKanriSansyou = 2
    ''' <summary>
    ''' 全社計画_確定権限
    ''' </summary>
    ''' <remarks></remarks>
    ZensyaKeikakuKengen = 3
    ''' <summary>
    ''' 支店別年度計画_確定権限
    ''' </summary>
    ''' <remarks></remarks>
    SitenbetuNenKeikakuKengen = 4
    ''' <summary>
    ''' 支店別月次計画_取込権限
    ''' </summary>
    ''' <remarks></remarks>
    SitenbetuGetujiKeikakuTorikomi = 5
    ''' <summary>
    ''' 支店別月次計画_確定権限
    ''' </summary>
    ''' <remarks></remarks>
    SitenbetuGetujiKeikakuKakutei = 6
    ''' <summary>
    ''' 計画値見直し_権限
    ''' </summary>
    ''' <remarks></remarks>
    KeikakuMinaosiKengen = 7
    ''' <summary>
    ''' 計画値確定_権限
    ''' </summary>
    ''' <remarks></remarks>
    KeikakuKakuteiKengen = 8
    ''' <summary>
    ''' 支店別月別計画_確定権限
    ''' </summary>
    ''' <remarks></remarks>
    KeikakuTorikomiKengen = 9
    ''' <summary>
    ''' ユーザーのみ
    ''' </summary>
    ''' <remarks></remarks>
    UserIdOnly = 10
    ''' <summary>
    ''' 支店別月次計画_見直し権限
    ''' </summary>
    ''' <remarks></remarks>
    SitenbetuGetujiKeikakuMinaosi = 11
End Enum
Public Class CommonCheck

    ''' <summary>
    ''' 稼動チェック
    ''' </summary>
    ''' <remarks></remarks>
    Public Function KadouCheck() As Boolean
        Dim Context As HttpContext = HttpContext.Current
        Dim Common As New Common
        Dim dteKadou As String = Format(Common.GetSystemDate, "HH:mm:ss")
        Dim strKadouJikan As String = System.Configuration.ConfigurationManager.AppSettings("KadouJikan")
        Dim blnJikan As Boolean = False
        If strKadouJikan = "" Then
            blnJikan = False
        Else
            Dim strDanJikan() As String = Split(strKadouJikan, ",")
            For intDan As Integer = 0 To strDanJikan.Length - 1
                Dim strJikan() As String = Split(strDanJikan(intDan), "~")
                For intJikan As Integer = 0 To Split(strJikan(intDan), "~").Length - 1
                    If dteKadou >= strJikan(0) AndAlso dteKadou <= strJikan(1) Then
                        blnJikan = True
                    End If
                Next

            Next
        End If
        Return blnJikan

    End Function
    ''' <summary>
    ''' 文字列のバイト数を取得（Shift-JIS）
    ''' </summary>
    ''' <param name="str">対象文字列</param>
    ''' <returns>Integer：バイト数</returns>
    ''' <remarks></remarks>
    Public Function getStrByteSJIS(ByVal str As String) As Integer

        'Shift-JISでのバイト数を取得
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

    End Function
    ''' <summary>
    ''' 禁則文字チェック
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks></remarks>
    Public Function kinsiStrCheck(ByVal target As String) As Boolean

        For Each st As String In CommonConstBC.Instance.arrayKinsiStr
            If target.IndexOf(st) >= 0 Then
                Return False
            End If
        Next

        Return True

    End Function
    ''' <summary>
    ''' バイト数チェック(SJIS)
    ''' </summary>
    ''' <param name="target">チェックする文字列</param>
    ''' <param name="max">最大OKバイト数</param>
    ''' <returns>true:チェックOK / false:チェックNG</returns>
    ''' <remarks></remarks>
    Public Function byteCheckSJIS(ByVal target As String, ByVal max As Integer) As Boolean

        Return getStrByteSJIS(target) <= max

    End Function
    ''' <summary>
    ''' ユーザー権限のチェック
    ''' </summary>
    ''' <param name="strUserId">ユーザーコード</param>
    ''' <param name="userInfo">ユーザーinfor</param>
    ''' <param name="strNinsyou">ユーザー権限</param>
    ''' <returns></returns>
    Function CommonNinsyou(ByRef strUserId As String, ByRef userInfo As LoginUserInfoList, ByVal strNinsyou As kegen) As Boolean
        Dim Context As HttpContext = HttpContext.Current

        Dim login_logic As New NinsyouBC
        Dim ninsyou As New NinsyouBC()


        ' ユーザー認証
        If (Not ninsyou.IsUserLogon()) Then
            '認証失敗
            ninsyou.EndResponseWithAccessDeny()
        End If
        ' ユーザー基本認証
        Context.Items("strFailureMsg") = String.Format(Messages.MSG022E, "ユーザー") '（"該当ユーザーがありません。"）
        If ninsyou.GetUserID() = "" Then
            Context.Server.Transfer("./CommonErr.aspx")
            Return False
        End If
        userInfo = login_logic.GetUserInfo(ninsyou.GetUserID())

        Context.Items("strFailureMsg") = Messages.MSG023E '（"権限がありません。"）
        If userInfo.Items.Count = 0 Then
            Return False
        Else
            If userInfo.Items.Count > 1 Then
                Return False
            Else
                strUserId = userInfo.Items(0).Account
            End If

        End If

        Select Case strNinsyou
            Case kegen.EigyouKeikakuKenriSansyou
                If userInfo.Items(0).EigyouKeikakuKenriSansyou = -1 Then
                    Return True
                End If
            Case kegen.KeikakuKakuteiKengen
                If userInfo.Items(0).KeikakuKakuteiKengen = -1 Then
                    Return True
                End If
            Case kegen.KeikakuMinaosiKengen
                If userInfo.Items(0).KeikakuMinaosiKengen = -1 Then
                    Return True
                End If
            Case kegen.SitenbetuGetujiKeikakuKakutei
                If userInfo.Items(0).SitenbetuGetujiKeikakuKakutei = -1 Then
                    Return True
                End If
            Case kegen.SitenbetuGetujiKeikakuTorikomi
                If userInfo.Items(0).SitenbetuGetujiKeikakuTorikomi = -1 Then
                    Return True
                End If
            Case kegen.SitenbetuNenKeikakuKengen
                If userInfo.Items(0).SitenbetuNenKeikakuKengen = -1 Then
                    Return True
                End If
            Case kegen.KeikakuTorikomiKengen
                If userInfo.Items(0).KeikakuTorikomiKengen = -1 Then
                    Return True
                End If
            Case kegen.UriYojituKanriSansyou
                If userInfo.Items(0).UriYojituKanriSansyou = -1 Then
                    Return True
                End If
            Case kegen.ZensyaKeikakuKengen
                If userInfo.Items(0).ZensyaKeikakuKengen = -1 Then
                    Return True
                End If
            Case kegen.UserIdOnly
                Return True

            Case kegen.SitenbetuGetujiKeikakuMinaosi
                If userInfo.Items(0).SitenbetuGetujiKeikakuMinaosi = -1 Then
                    Return True
                End If
        End Select
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
            Return String.Format(Messages.MSG001E, PARAM)
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
            Return String.Format(Messages.MSG024E, PARAM, PARAM).ToString
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
            If data > CommonConstBC.Instance.dateMax Or data < CommonConstBC.Instance.dateMin Then

                Return String.Format(Messages.MSG025E, PARAM).ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return String.Format(Messages.MSG025E, PARAM).ToString
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

        '値のCheckを行なう
        If inTarget = String.Empty Then
            Return ""
        End If

        '文字数チェック
        If Not byteCheckSJIS(inTarget, intLength) Then
            Dim csScript As New StringBuilder

            'MsgBox 表示
            If type = kbn.HANKAKU Then
                Return String.Format(Messages.MSG026E, PARAM, intLength)
            Else
                '全角　{0}に登録できる文字数は、全角{1}文字以内です。
                Return String.Format(Messages.MSG027E, PARAM, intLength / 2)
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
            Return String.Format(Messages.MSG028E, PARAM).ToString
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
                    Return String.Format(Messages.MSG029E, PARAM).ToString
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Else
            If kbn = "1" Then
                Return String.Format(Messages.MSG029E, PARAM).ToString
            Else
                Return String.Format(Messages.MSG030E, PARAM).ToString
            End If

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
                Return String.Format(Messages.MSG031E, PARAM).ToString
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
                        Return String.Format(Messages.MSG032E, PARAM).ToString
                    End If
                Else
                    Return String.Format(Messages.MSG032E, PARAM).ToString
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

        '禁則文字チェック
        If Not kinsiStrCheck(inTarget) Then
            Return String.Format(Messages.MSG033E, PARAM).ToString
        Else
            Return ""
        End If
    End Function

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

    Public Sub New()

    End Sub
End Class
