''' <summary>CSVt@CÌ¤¯`FbN</summary>
Public Class CsvInputCheck

    '´¿
    Public Const GENKA As String = "GENKA"
    'Ì
    Public Const HANBAI As String = "HANBAI"
    'Á¿X¤i²¸û@ÁÊÎ
    Public Const TAIOU As String = "TAIOU"
    'Á¿Xîñêæ
    Public Const KAMEITEN As String = "KAMEITEN"


    '´¿CSVt@CÌtB[h
    Public Const GENKA_FIELD_COUNT As Integer = 42
    'Ì¿iCSVt@CÌtB[h
    Public Const HANBAI_FIELD_COUNT As Integer = 14
    'Á¿X¤i²¸û@ÁÊÎCSVt@CÌtB[h
    Public Const TAIOU_FIELD_COUNT As Integer = 15
    'Á¿XîñêæCSVt@CÌtB[h
    '==========2012/05/09 Ô´ 407553ÌÎ C³«======================
    'Public Const KAMEITEN_FIELD_COUNT As Integer = 91
    '==========«2013/03/07 Ô´ 407584 C³«======================
    'Public Const KAMEITEN_FIELD_COUNT As Integer = 96
    Public Const KAMEITEN_FIELD_COUNT As Integer = 124
    '==========ª2013/03/07 Ô´ 407584 C³ª======================
    '==========2012/05/09 Ô´ 407553ÌÎ C³ª======================

    '´¿f[^ÌÚÅå·
    Public GENKA_MAX_LENGTH() As Integer = {40, 5, 2, 40, 5, 5, 40, 8, 40, 5, 32, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1, 10, 1}
    'Ì¿if[^ÌÚÅå·
    Public HANBAI_MAX_LENGTH() As Integer = {40, 5, 5, 40, 8, 40, 5, 32, 1, 10, 1, 10, 1, 1}
    'Á¿X¤i²¸û@ÁÊÎf[^ÌÚÅå·
    Public TAIOU_MAX_LENGTH() As Integer = {40, 1, 5, 40, 8, 40, 5, 32, 10, 40, 1, 8, 1, 10, 10}
    'Á¿XîñêæÌÚÅå·
    '==========2012/05/09 Ô´ 407553ÌÎ C³«======================
    'Public KAMEITEN_MAX_LENGTH() As Integer = {40, 1, 5, 1, 10, 40, 20, 40, 20, 9, 40, 5, 40, 5, 40, 80, 40, 2, 10, 1, 10, 16, 30, 16, 30, 10, 8, 40, 8, 40, 8, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 10, 1, 1, 8, 40, 30, 2, 10, 50, 20, 16, 16, 64, 30, 30, 2, 40, 2, 40, 2, 40, 2, 40, 2, 40, 80, 80, 80, 80, 80}
    '==========«2013/03/07 Ô´ 407584 C³«======================
    'Public KAMEITEN_MAX_LENGTH() As Integer = {40, 1, 5, 1, 10, 40, 20, 40, 20, 9, 40, 5, 40, 5, 40, 80, 40, 2, 10, 1, 10, 16, 30, 16, 30, 10, 8, 40, 8, 40, 8, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 10, 1, 1, 8, 40, 30, 2, 10, 50, 20, 16, 16, 64, 30, 30, 19, 19, 19, 19, 19, 2, 40, 2, 40, 2, 40, 2, 40, 2, 40, 80, 80, 80, 80, 80}
    Public KAMEITEN_MAX_LENGTH() As Integer = {40, 1, 5, 1, 10, 40, 20, 40, 20, 9, 40, 5, 40, 5, 40, 80, 40, 2, 10, 5, 1, 10, 16, 30, 16, 30, 1, 40, 1, 10, 8, 40, 8, 40, 8, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 1, 5, 2, 40, 2, 1, 1, 10, 1, 1, 8, 40, 30, 2, 10, 50, 20, 16, 16, 64, 30, 30, 16, 1, 8, 40, 8, 8, 16, 16, 30, 19, 19, 19, 19, 19, 2, 40, 2, 40, 2, 40, 2, 40, 2, 40, 80, 80, 80, 80, 80, 10, 10, 10, 10, 10, 10, 20, 1, 20, 10, 20, 10, 10}
    '==========ª2013/03/07 Ô´ 407584 C³ª======================
    '==========2012/05/09 Ô´ 407553ÌÎ C³ª======================
    'gpÖ~¶ñzñ
    Private arrayKinsiStr() As String = New String() {vbTab, """", "C", "'", "<", ">", "&", "$$$"}

    '´¿f[^Ìl^Úõø
    Private GENKA_NUM_INDEX() As Integer = {4, 9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41}
    'Ì¿if[^Ìl^Úõø
    Private HANBAI_NUM_INDEX() As Integer = {1, 6, 8, 9, 10, 11, 12, 13}
    'Á¿X¤i²¸û@ÁÊÎf[^Ìl^Úõø
    Private TAIOU_NUM_INDEX() As Integer = {1, 6, 8, 10, 12, 13, 14}
    'Á¿Xîñêæl^Úõø
    '==========2012/05/09 Ô´ 407553ÌÎ C³«======================
    'Private KAMEITEN_NUM_INDEX() As Integer = {3, 19, 25, 60, 62, 63, 76, 78, 80, 82, 84}
    '==========«2013/03/07 Ô´ 407584 C³«======================
    'Private KAMEITEN_NUM_INDEX() As Integer = {3, 19, 25, 60, 62, 63, 81, 83, 85, 87, 89}
    Private KAMEITEN_NUM_INDEX() As Integer = {3, 20, 26, 28, 29, 64, 65, 66, 68, 69, 83, 86, 87, 96, 98, 100, 102, 104}
    '==========ª2013/03/07 Ô´ 407584 C³ª======================
    '==========2012/05/09 Ô´ 407553ÌÎ C³ª======================

    '´¿f[^ÌK{üÍÚõø
    Private GENKA_NOTNULL_INDEX() As Integer = {1, 2, 4, 5, 7, 9}
    'Ì¿if[^ÌK{üÍÚõø
    Private HANBAI_NOTNULL_INDEX() As Integer = {1, 2, 4, 6}
    'Á¿X¤i²¸û@ÁÊÎf[^ÌK{üÍÚõø
    Private TAIOU_NOTNULL_INDEX() As Integer = {1, 2, 4, 6, 8}
    'Á¿XîñêæÌK{üÍÚõø
    Private KAMEITEN_NOTNULL_INDEX() As Integer = {1, 2, 3, 5, 6}

    '´¿f[^ÌXVðõø
    Private GENKA_KOUSIN_JYOUKEN_INDEX() As Integer = {11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40}
    'Ì¿if[^ÌXVðõø
    Private HANBAI_KOUSIN_JYOUKEN_INDEX() As Integer = {8, 9, 11}
    'Á¿X¤i²¸û@ÁÊÎf[^ÌXVðõø
    Private TAIOU_KOUSIN_JYOUKEN_INDEX() As Integer = {10}
    'Á¿XîñêæÌXVðõø
    Private KAMEITEN_KOUSIN_JYOUKEN_INDEX() As Integer = {}

    'H¿i
    Public Const KOJ As String = "KOJ"
    'H¿iCSVt@CÌtB[h
    Public Const KOJ_FIELD_COUNT As Integer = 12
    'H¿if[^ÌÚÅå·
    Public KOJ_MAX_LENGTH() As Integer = {40, 5, 5, 40, 8, 40, 7, 40, 1, 10, 1, 1}
    'H¿if[^Ìl^Úõø
    Private KOJ_NUM_INDEX() As Integer = {1, 8, 9, 10, 11}
    'H¿if[^ÌK{üÍÚõø
    Private KOJ_NOTNULL_INDEX() As Integer = {1, 2, 4, 6}
    'H¿if[^ÌXVðõø
    Private KOJ_KOUSIN_JYOUKEN_INDEX() As Integer = {8, 9}

    'Äpdü
    Public Const SIIRE As String = "SIIRE"
    'ÄpdüCSVt@CÌtB[h
    Public Const SIIRE_FIELD_COUNT As Integer = 18
    'Äpdüf[^ÌÚÅå·
    Public SIIRE_MAX_LENGTH() As Integer = {40, 1, 512, 10, 10, 5, 2, 5, 8, 5, 10, 1, 10, 1, 10, 1, 10, 50}
    'Äpdüf[^Ìl^Úõø
    Private SIIRE_NUM_INDEX() As Integer = {1, 13}
    'ÊAP¿AÁïÅz ªð}CiXl Î
    Private SIIRE_NUM_MAINASU_INDEX() As Integer = {9, 10, 12}

    'Äpdüf[^ÌK{üÍÚõø
    Private SIIRE_NOTNULL_INDEX() As Integer = {5, 6, 8, 9, 10, 11, 12, 13, 14}
    'Äpdüf[^Ìút`FbNÚõø
    Private SIIRE_NOTDATE_INDEX() As Integer = {3, 4, 14}
    'Äpã
    Public Const URIAGE As String = "URIAGE"
    '==========«2015/11/19 ºº addãXæª,ãXº°ÄÞ  C³«======================
    'ÄpãCSVt@CÌtB[h
    'Public Const URIAGE_FIELD_COUNT As Integer = 21
    Public Const URIAGE_FIELD_COUNT As Integer = 23
    'Äpãf[^ÌÚÅå·
    'Public URIAGE_MAX_LENGTH() As Integer = {40, 1, 512, 10, 10, 10, 5, 2, 1, 8, 40, 5, 10, 10, 1, 10, 1, 10, 1, 10, 50}
    Public URIAGE_MAX_LENGTH() As Integer = {40, 1, 512, 10, 10, 10, 1, 7, 5, 2, 1, 8, 40, 5, 10, 10, 1, 10, 1, 10, 1, 10, 50}
    'Äpãf[^Ìl^Úõø
    'Private URIAGE_NUM_INDEX() As Integer = {1, 11, 12, 13, 15, 16}
    Private URIAGE_NUM_INDEX() As Integer = {1, 6, 15, 18}

    'ÊAP¿AÁïÅz ªð}CiXl Î
    Private URIAGE_NUM_MAINASU_INDEX() As Integer = {13, 14, 17}


    'Äpãf[^ÌK{üÍÚõø
    'Private URIAGE_NOTNULL_INDEX() As Integer = {3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17}
    Private URIAGE_NOTNULL_INDEX() As Integer = {3, 4, 5, 8, 9, 10, 11, 12, 13, 14, 16, 17, 18, 19}
    'Äpdüf[^Ìút`FbNÚõø
    'Private URIAGE_NOTDATE_INDEX() As Integer = {3, 4, 5, 17}
    Private URIAGE_NOTDATE_INDEX() As Integer = {3, 4, 5, 19}
    'Äpdüf[^Ì¼pp`FbNÚõø
    Private URIAGE_HANKAKUEISUUJI_INDEX() As Integer = {7}
    '==========ª2015/11/19 ºº addãXæª,ãXº°ÄÞ  C³ª======================

    'Á¿Xf[^Ìút`FbNÚõø
    '==========«2013/03/07 Ô´ 407584 C³«======================
    'Private KAMEITEN_NOTDATE_INDEX() As Integer = {20, 61}
    Private KAMEITEN_NOTDATE_INDEX() As Integer = {21, 67}
    '==========ª2013/03/07 Ô´ 407584 C³ª======================
    ''' <summary>tB[h`FbN</summary>
    Public Function ChkFieldCount(ByVal intFieldCount As Integer, ByVal strCsvKbn As String) As Boolean

        Dim Count As Integer = 0

        Select Case strCsvKbn
            Case GENKA
                Count = GENKA_FIELD_COUNT

            Case HANBAI
                Count = HANBAI_FIELD_COUNT

            Case TAIOU
                Count = TAIOU_FIELD_COUNT
            Case KAMEITEN
                Count = KAMEITEN_FIELD_COUNT
            Case KOJ
                Count = KOJ_FIELD_COUNT
            Case SIIRE
                Count = SIIRE_FIELD_COUNT
            Case URIAGE
                Count = URIAGE_FIELD_COUNT
            Case Else
                Return False
        End Select

        If Not intFieldCount.Equals(Count) Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>ÚÅå·`FbN</summary>
    Public Function ChkMaxLength(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Select Case strCsvKbn
            Case GENKA
                For i As Integer = 0 To strLine.Split(",").Length - 1
                    Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(",")(i))
                    If btBytes.LongLength > GENKA_MAX_LENGTH(i) Then
                        Return False
                    End If
                Next

            Case HANBAI

                For i As Integer = 0 To strLine.Split(",").Length - 1
                    Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(",")(i))
                    If btBytes.LongLength > HANBAI_MAX_LENGTH(i) Then
                        Return False
                    End If
                Next

            Case TAIOU
                For i As Integer = 0 To strLine.Split(",").Length - 1
                    Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(",")(i))
                    If btBytes.LongLength > TAIOU_MAX_LENGTH(i) Then
                        Return False
                    End If
                Next
            Case KAMEITEN
                For i As Integer = 0 To strLine.Split(",").Length - 1
                    Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(",")(i))
                    If btBytes.LongLength > KAMEITEN_MAX_LENGTH(i) Then
                        Return False
                    End If
                Next
            Case KOJ
                For i As Integer = 0 To strLine.Split(",").Length - 1
                    Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(",")(i))
                    If btBytes.LongLength > KOJ_MAX_LENGTH(i) Then
                        Return False
                    End If
                Next
            Case SIIRE
                For i As Integer = 0 To strLine.Split(",").Length - 1
                    Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(",")(i))
                    If btBytes.LongLength > SIIRE_MAX_LENGTH(i) Then
                        Return False
                    End If
                Next
            Case URIAGE
                For i As Integer = 0 To strLine.Split(",").Length - 1
                    Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(",")(i))
                    If btBytes.LongLength > URIAGE_MAX_LENGTH(i) Then
                        Return False
                    End If
                Next
            Case Else
                Return False
        End Select

        Return True
    End Function


    ''' <summary>Ö¥¶`FbN</summary>
    Public Function ChkKinjiMoji(ByVal target As String) As Boolean

        For Each st As String In arrayKinsiStr

            If target.IndexOf(st) >= 0 Then
                Return False
            End If

        Next

        Return True
    End Function

    ''' <summary>l^Ú`FbN</summary>
    Public Function ChkSuuti(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        Select Case strCsvKbn
            Case GENKA
                For Each i As Integer In GENKA_NUM_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next

            Case HANBAI
                For Each i As Integer In HANBAI_NUM_INDEX

                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next

            Case TAIOU
                For Each i As Integer In TAIOU_NUM_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next

            Case KAMEITEN
                For Each i As Integer In KAMEITEN_NUM_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next

            Case KOJ
                For Each i As Integer In KOJ_NUM_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next
            Case SIIRE
                For Each i As Integer In SIIRE_NUM_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next
                'ÊAP¿AÁïÅz ªð}CiXl Î
                For Each i As Integer In SIIRE_NUM_MAINASU_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankakuMainasu(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next
            Case URIAGE

                For Each i As Integer In URIAGE_NUM_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next

                'ÊAP¿AÁïÅz ªð}CiXl
                For Each i As Integer In URIAGE_NUM_MAINASU_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankakuMainasu(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next


            Case Else
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' ¼pp`FbN
    ''' </summary>
    Function ChkHankakuEisuuji(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        Select Case strCsvKbn
            Case URIAGE
                For Each i As Integer In URIAGE_HANKAKUEISUUJI_INDEX
                    If (Not strLine.Split(",")(i).Trim.Equals(String.Empty)) AndAlso (Not ChkAZaz09(strLine.Split(",")(i))) Then
                        Return False
                    End If
                Next
            Case Else
                Return False
        End Select
        Return True
    End Function
    Function ChkAZaz09(ByVal inTarget As String) As Boolean
        Dim intCount As Long = 0
        Dim strTmp As String = 0

        For intCount = 1 To Len(inTarget)
            strTmp = Mid(inTarget, intCount, 1)
            If Not ((Asc(strTmp) >= Asc("a") And Asc(strTmp) <= Asc("z")) _
                Or (Asc(strTmp) >= Asc("A") And Asc(strTmp) <= Asc("Z")) _
                Or (Asc(strTmp) >= Asc("0") And Asc(strTmp) <= Asc("9"))) Then
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>®`FbN</summary>
    Function CheckHankaku(ByVal inTarget As String) As Boolean
        If inTarget.Length = System.Text.Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            Try
                Dim intTemp As Integer = CInt(inTarget)
            Catch ex As Exception
                Return False
            End Try

            If InStr(inTarget, ".") > 0 Or InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>®`FbN}CiX</summary>
    Function CheckHankakuMainasu(ByVal inTarget As String) As Boolean
        If inTarget.Length = System.Text.Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            Try
                Dim intTemp As Integer = CInt(inTarget)
            Catch ex As Exception
                Return False
            End Try

            If InStr(inTarget, ".") > 0 OrElse InStr(inTarget, "+") > 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function
    ''' <summary>K{`FbN</summary>
    Public Function ChkNotDate(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        Select Case strCsvKbn

            Case SIIRE
                For Each i As Integer In SIIRE_NOTDATE_INDEX
                    If Not strLine.Split(",")(i).Trim.Equals(String.Empty) AndAlso IsDate(strLine.Split(",")(i).Trim) = False Then
                        Return False
                    End If
                Next
            Case URIAGE
                For Each i As Integer In URIAGE_NOTDATE_INDEX
                    If Not strLine.Split(",")(i).Trim.Equals(String.Empty) AndAlso IsDate(strLine.Split(",")(i).Trim) = False Then
                        Return False
                    End If
                Next
            Case KAMEITEN
                For Each i As Integer In KAMEITEN_NOTDATE_INDEX
                    If Not strLine.Split(",")(i).Trim.Equals(String.Empty) AndAlso IsDate(strLine.Split(",")(i).Trim) = False Then
                        Return False
                    End If
                Next
            Case Else
                Return False
        End Select

        Return True
    End Function
    ''' <summary>K{`FbN</summary>
    Public Function ChkNotNull(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        Select Case strCsvKbn
            Case GENKA
                For Each i As Integer In GENKA_NOTNULL_INDEX
                    If strLine.Split(",")(i).Trim.Equals(String.Empty) Then
                        Return False
                    End If
                Next

            Case HANBAI
                For Each i As Integer In HANBAI_NOTNULL_INDEX

                    If strLine.Split(",")(i).Trim.Equals(String.Empty) Then
                        Return False
                    End If
                Next

            Case TAIOU
                For Each i As Integer In TAIOU_NOTNULL_INDEX
                    If strLine.Split(",")(i).Trim.Equals(String.Empty) Then
                        Return False
                    End If
                Next

            Case KAMEITEN
                For Each i As Integer In KAMEITEN_NOTNULL_INDEX
                    If strLine.Split(",")(i).Trim.Equals(String.Empty) Then
                        Return False
                    End If
                Next

            Case KOJ
                For Each i As Integer In KOJ_NOTNULL_INDEX
                    If strLine.Split(",")(i).Trim.Equals(String.Empty) Then
                        Return False
                    End If
                Next
            Case SIIRE
                For Each i As Integer In SIIRE_NOTNULL_INDEX
                    If strLine.Split(",")(i).Trim.Equals(String.Empty) Then
                        Return False
                    End If
                Next
            Case URIAGE
                For Each i As Integer In URIAGE_NOTNULL_INDEX
                    If strLine.Split(",")(i).Trim.Equals(String.Empty) Then
                        Return False
                    End If
                Next
            Case Else
                Return False
        End Select

        Return True
    End Function

    ''' <summary>XVð`FbN</summary>
    Public Function ChkKousinJyouken(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        Dim KousinJyouken As String = String.Empty

        Select Case strCsvKbn
            Case GENKA
                For Each i As Integer In GENKA_KOUSIN_JYOUKEN_INDEX
                    KousinJyouken = KousinJyouken & strLine.Split(",")(i).Trim
                Next

            Case HANBAI
                For Each i As Integer In HANBAI_KOUSIN_JYOUKEN_INDEX

                    KousinJyouken = KousinJyouken & strLine.Split(",")(i).Trim
                Next

            Case TAIOU
                For Each i As Integer In TAIOU_KOUSIN_JYOUKEN_INDEX
                    KousinJyouken = KousinJyouken & strLine.Split(",")(i).Trim
                Next

            Case KAMEITEN
                For Each i As Integer In KAMEITEN_KOUSIN_JYOUKEN_INDEX
                    KousinJyouken = KousinJyouken & strLine.Split(",")(i).Trim
                Next

            Case KOJ
                For Each i As Integer In KOJ_KOUSIN_JYOUKEN_INDEX
                    KousinJyouken = KousinJyouken & strLine.Split(",")(i).Trim
                Next
            Case Else
                Return False
        End Select

        If KousinJyouken.Trim.Equals(String.Empty) Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>Åå·ðØèæé</summary>
    Public Function CutMaxLength(ByVal strValue As String, ByVal intMaxByteCount As Integer) As String

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Dim intLengthCount As Integer = 0
        For i As Integer = strValue.Length To 0 Step -1
            Dim btBytes As Byte() = hEncoding.GetBytes(Left(strValue, i))
            If btBytes.LongLength <= intMaxByteCount Then
                intLengthCount = i
                Exit For
            End If
        Next

        Return Left(strValue, intLengthCount)
    End Function

End Class
