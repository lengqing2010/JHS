Imports System.Transactions
''' <summary>
''' 共通のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class CommonBizLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Private mLogic As New MessageLogic
    Dim strLogic As New StringLogic

#Region "プロパティ"
    ''' <summary>
    ''' エラー用特別対応コード群
    ''' </summary>
    ''' <remarks>価格反映対象外となった特別対応名を各々改行毎に格納</remarks>
    Private pStrErrTokubetuTaiouCds As String = String.Empty
    ''' <summary>
    ''' 外部からのアクセス用 for pStrErrTokubetuTaiouCds
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AccTokubetuTaiouKkkMsg() As String
        Get
            Return pStrErrTokubetuTaiouCds
        End Get
    End Property

#End Region

#Region "税区分より消費税率を取得"
    ''' <summary>
    ''' 税区分をもとに、消費税率(%表示取得可)を取得する
    ''' </summary>
    ''' <param name="strZeiKbn">税区分</param>
    ''' <param name="blnPercent">False:税率,True:%表示</param>
    ''' <returns>[String]消費税率or消費税率(%表示)</returns>
    ''' <remarks></remarks>
    Public Function getSyouhiZeiritu(ByVal strZeiKbn As String, Optional ByVal blnPercent As Boolean = False) As String
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSyouhiZeiritu" _
                                                    , strZeiKbn _
                                                    , blnPercent)
        Dim dataAccess As New SyouhizeiAccess
        Dim strZeiritu As String = String.Empty

        If dataAccess.getMeisyou(strZeiKbn, strZeiritu, blnPercent) = False Then
            strZeiritu = String.Empty
        End If
        Return strZeiritu
    End Function
#End Region

#Region "売上日より税区分・消費税率を取得"
    ''' <summary>
    ''' 売上日をもとに、税区分・消費税率を取得する
    ''' </summary>
    ''' <param name="strUriDate">売上年月日</param>
    '''  <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strZeiKbn">税区分</param>
    ''' <param name="strZeiritu">税率</param>
    ''' <returns>T/F</returns>
    ''' <remarks></remarks>
    Public Function getSyouhiZeirituUriage(ByVal strUriDate As String _
                                          , ByVal strSyouhinCd As String _
                                          , ByRef strZeiKbn As String _
                                          , ByRef strZeiritu As String _
                                          ) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSyouhiZeirituUriage" _
                                                    , strUriDate _
                                                    , strSyouhinCd _
                                                    , strZeiKbn _
                                                    , strZeiritu _
                                                    )

        Dim syouhinDtAcc As New SyouhinDataAccess
        Dim dtbl As DataTable
        Dim syouhinRec As SyouhinMeisaiRecord = Nothing

        '売上日チェック
        If strUriDate = String.Empty Then
            strUriDate = Date.Today.ToString("yyyyMMdd")
        End If

        '商品情報の取得
        dtbl = syouhinDtAcc.GetSyouhinInfo(strSyouhinCd)
        If dtbl.Rows.Count > 0 Then
            syouhinRec = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), dtbl)(0)
        Else
            Return False
        End If

        '売上日が基準日より小さい場合、かつ、取得した税率が0％の場合
        If strUriDate < EarthConst.SIN_ZEIRITU_FROM_DATE AndAlso _
           syouhinRec.Zeiritu = EarthConst.ZEIRITU_000 Then
            '税区分・税率は0を適用する
            strZeiKbn = syouhinRec.ZeiKbn
            strZeiritu = EarthConst.ZEIRITU_000
            Return True
            '売上日が基準日より小さい場合、かつ、取得した税率が0%でない場合
        ElseIf strUriDate < EarthConst.SIN_ZEIRITU_FROM_DATE AndAlso _
               syouhinRec.Zeiritu <> EarthConst.ZEIRITU_000 Then
            '税区分・税率は5％を適用する
            strZeiKbn = EarthConst.KYUU_ZEI_KBN
            strZeiritu = EarthConst.KYUU_ZEIRITU_005
            Return True
        Else
            '上記以外の場合、商品マスタから取得した新税区分・消費税率を取得する

            '取得した税区分・税率のセット
            If syouhinRec IsNot Nothing Then
                strZeiKbn = syouhinRec.ZeiKbn
                strZeiritu = syouhinRec.Zeiritu
            End If
            Return True
        End If

    End Function
#End Region

#Region "伝票売上年月日、伝票仕入年月日の自動設定"
    ''' <summary>
    ''' 伝票売上年月日、伝票仕入年月日の自動設定
    ''' </summary>
    ''' <param name="teibetuRec">更新用邸別請求レコードクラス</param>
    ''' <param name="checkRec">DB値チェック用邸別請求レコードクラス</param>
    ''' <param name="teibetuType">邸別請求レコードクラスのタイプ</param>
    ''' <remarks>
    ''' 売上年月日がセットされた際の、伝票売上年月日、伝票仕入年月日の自動設定を行なう
    ''' ※teibetuType is Nothing：邸別データ修正、商品４
    ''' </remarks>
    Public Sub SetAutoDenUriSiireDate(ByRef teibetuRec As TeibetuSeikyuuRecord _
                                    , ByVal checkRec As TeibetuSeikyuuRecord _
                                    , Optional ByVal teibetuType As Type = Nothing _
                                    )
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetAutoDenUriDate" _
                                                    , teibetuRec _
                                                    , checkRec _
                                                    , teibetuType)

        'レコードに値が無い場合、処理終了
        If teibetuRec Is Nothing OrElse checkRec Is Nothing Then
            Exit Sub
        End If

        'teibetuTypeによって、伝票売上年月日を自動設定するかを判断する
        If teibetuType Is Nothing Then
            'teibetuTypeが設定無しの場合、自動設定はせず、処理終了
            '(主に邸別請求データ修正画面、商品４画面用：画面で設定された値[teibetuRec]で更新)
            Exit Sub
        End If

        '自動設定を行う場合、計上済か否かをチェック
        If checkRec.UriKeijyouDate = DateTime.MinValue Then
            '画面.伝票売上年月日 = 画面.売上年月日(画面.売上年月日と同期)
            teibetuRec.DenpyouUriDate = teibetuRec.UriDate
            '画面.伝票仕入年月日 = 画面.売上年月日(画面.売上年月日と同期)
            teibetuRec.DenpyouSiireDate = teibetuRec.UriDate
        Else
            '画面.伝票売上年月日 = DB.伝票売上年月日(値の変更無し)
            teibetuRec.DenpyouUriDate = checkRec.DenpyouUriDate
            '画面.伝票仕入年月日 = DB.伝票仕入年月日(値の変更無し)
            teibetuRec.DenpyouSiireDate = checkRec.DenpyouSiireDate
        End If

    End Sub
#End Region

#Region "請求先/仕入先の設定"
    ''' <summary>
    ''' 請求先/仕入先の設定
    ''' </summary>
    ''' <param name="teibetuRec">更新用邸別請求レコードクラス</param>
    ''' <param name="checkRec">DB値チェック用邸別請求レコードクラス</param>
    ''' <remarks>
    ''' 邸別請求T更新時、請求先/仕入先の設定を変更しないように、DBの元の値をセットする
    ''' </remarks>
    Public Sub SetSeikyuuSiireSakiInfo(ByRef teibetuRec As TeibetuSeikyuuRecord _
                                        , ByVal checkRec As TeibetuSeikyuuRecord _
                                        )
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSeikyuuSiireSakiInfo" _
                                                    , teibetuRec _
                                                    , checkRec)

        If teibetuRec Is Nothing OrElse checkRec Is Nothing Then
            Exit Sub
        End If

        With teibetuRec
            '画面.請求先/仕入先がセットされている場合
            If .SeikyuuSakiCd IsNot Nothing _
                And .SeikyuuSakiBrc IsNot Nothing _
                    And .SeikyuuSakiKbn IsNot Nothing Then
                '何もしない
            Else
                'DBの値をセット(変更しない)
                '請求先
                .SeikyuuSakiCd = checkRec.SeikyuuSakiCd
                .SeikyuuSakiBrc = checkRec.SeikyuuSakiBrc
                .SeikyuuSakiKbn = checkRec.SeikyuuSakiKbn
                '仕入先
                .TysKaisyaCd = checkRec.TysKaisyaCd
                .TysKaisyaJigyousyoCd = checkRec.TysKaisyaJigyousyoCd
            End If
        End With

    End Sub
#End Region

#Region "請求書発行日取得ロジック"

#Region "共通(請求先Ｍから)"
    ''' <summary>
    ''' 請求先マスタから請求書締日を取得
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDate(ByVal strSeikyuuSakiCd As String _
                                        , ByVal strSeikyuuSakiBrc As String _
                                        , ByVal strSeikyuuSakiKbn As String) As String
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSimeDate" _
                                                    , strSeikyuuSakiCd _
                                                    , strSeikyuuSakiBrc _
                                                    , strSeikyuuSakiKbn)
        Dim dtAccUriage As New UriageDataAccess
        Dim strSimeDate As String

        '請求先Mから取得
        strSimeDate = dtAccUriage.GetSeikyuuSimeDate(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn)

        ' 請求書締め日をセット
        Return strSimeDate

    End Function
#End Region

#Region "調査・工事・商品(加盟店から)"
    ''' <summary>
    ''' 請求先マスタから請求書締日を取得（加盟店ベース）
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>請求書締め日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDateFromKameiten(ByVal strSeikyuuSakiCd As String _
                                            , ByVal strSeikyuuSakiBrc As String _
                                            , ByVal strSeikyuuSakiKbn As String _
                                            , ByVal strKameitenCd As String _
                                            , ByVal strSyouhinCd As String) As String
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSimeDateFromKameiten" _
                                                    , strSeikyuuSakiCd _
                                                    , strSeikyuuSakiBrc _
                                                    , strSeikyuuSakiKbn _
                                                    , strKameitenCd _
                                                    , strSyouhinCd)
        Dim dtAccUriage As New UriageDataAccess
        Dim strSimeDate As String

        '請求先Mから取得
        strSimeDate = GetSeikyuuSimeDate(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn)
        If strSeikyuuSakiCd = String.Empty And strSeikyuuSakiBrc = String.Empty And strSeikyuuSakiKbn = String.Empty Then
            '加盟店から取得
            strSimeDate = dtAccUriage.GetSeikyuuSimeDateFromKameiten(strKameitenCd, strSyouhinCd)
        End If

        ' 請求書締め日セット
        Return strSimeDate

    End Function
#End Region

#Region "工事(調査会社から)"
    ''' <summary>
    ''' 請求先マスタから請求書締め日を取得（調査会社ベース）
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <param name="strTysKaisyaCd">調査会社コード(会社コード + 事業所コード)</param>
    ''' <returns>請求書締め日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDateFromTyousa(ByVal strSeikyuuSakiCd As String _
                                                    , ByVal strSeikyuuSakiBrc As String _
                                                    , ByVal strSeikyuuSakiKbn As String _
                                                    , ByVal strTysKaisyaCd As String) As String
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoHakkouDateFromTyousa" _
                                                    , strSeikyuuSakiCd _
                                                    , strSeikyuuSakiBrc _
                                                    , strSeikyuuSakiKbn _
                                                    , strTysKaisyaCd)
        Dim dtAccUriage As New UriageDataAccess
        Dim strSimeDate As String

        '請求先Mから取得
        strSimeDate = GetSeikyuuSimeDate(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn)
        If strSeikyuuSakiCd = String.Empty And strSeikyuuSakiBrc = String.Empty And strSeikyuuSakiKbn = String.Empty Then
            '調査会社から取得
            strSimeDate = dtAccUriage.GetSeikyuuSimeDateFromTyousa(strTysKaisyaCd)
        End If

        ' 請求書締め日をセット
        Return strSimeDate

    End Function

    ''' <summary>
    ''' 請求先マスタから請求書発行日を取得（調査会社ベース）
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード(会社コード + 事業所コード)</param>
    ''' <returns>請求書発行日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoHakkouDateFromTyousa(ByVal strTysKaisyaCd As String _
                                                    , Optional ByVal strCalcDate As String = "") As Date
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoHakkouDateFromTyousa" _
                                                    , strTysKaisyaCd)
        Dim dtAccUriage As New UriageDataAccess
        Dim strSimeDate As String

        '調査会社から取得
        strSimeDate = dtAccUriage.GetSeikyuuSimeDateFromTyousa(strTysKaisyaCd)

        ' 請求書発行日をセット
        Return CalcSeikyuusyoHakkouDate(strSimeDate, strCalcDate)

    End Function
#End Region

#Region "販促品請求(営業所から)"
    ''' <summary>
    ''' 請求先マスタから請求書締め日を取得（営業所ベース）
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <returns>請求書締め日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDateFromEigyousyo(ByVal strSeikyuuSakiCd As String _
                                                    , ByVal strSeikyuuSakiBrc As String _
                                                    , ByVal strSeikyuuSakiKbn As String _
                                                    , ByVal strEigyousyoCd As String) As String
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSimeDateFromEigyousyo" _
                                                    , strSeikyuuSakiCd _
                                                    , strSeikyuuSakiBrc _
                                                    , strSeikyuuSakiKbn _
                                                    , strEigyousyoCd)
        Dim dtAccUriage As New UriageDataAccess
        Dim strSimeDate As String

        '請求先Mから取得
        strSimeDate = GetSeikyuuSimeDate(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn)
        If strSeikyuuSakiCd = String.Empty And strSeikyuuSakiBrc = String.Empty And strSeikyuuSakiKbn = String.Empty Then
            '営業所から取得
            strSimeDate = dtAccUriage.GetSeikyuuSimeDateFromEigyousyo(strEigyousyoCd)
        End If

        ' 請求書締め日をセット
        Return strSimeDate

    End Function
#End Region

    Private Const BEGIN_DAY As String = "01"
    Private Const FORMAT_DAY As String = "00"

    ''' <summary>
    ''' 取得した請求締め日を元に請求書発行日を算出する
    ''' </summary>
    ''' <param name="strSimeDate">請求締め日</param>
    ''' <param name="strCalcDate">算出元日付</param>
    ''' <returns>算出した請求書発行日</returns>
    ''' <remarks></remarks>
    Public Function CalcSeikyuusyoHakkouDate(ByVal strSimeDate As String, Optional ByVal strCalcDate As String = "") As Date
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoHakkouDate", strSimeDate, strCalcDate)
        '算出元日付
        Dim dtCalcDate As Date = Nothing
        '算出日付のチェック日付
        Dim dtCalcCheck As Date = Today

        If IsDate(strCalcDate) Then
            dtCalcDate = Date.Parse(strCalcDate)
            dtCalcCheck = dtCalcDate
        End If

        If dtCalcDate = Nothing OrElse dtCalcDate < Today Then
            dtCalcDate = Today
        End If

        Dim datSstring As String = GetDateStrReplaceDay(dtCalcDate, strSimeDate)

        ' 戻り値
        Dim editDate As Date

        ' 当月初
        Dim dtThisMonthBegin As Date = Date.Parse(GetDateStrReplaceDay(Today, BEGIN_DAY))

        ' 算出初
        Dim dtCalcDateBegin As Date = Date.Parse(GetDateStrReplaceDay(dtCalcDate, BEGIN_DAY))

        Try
            ' 日付型に変換する
            editDate = Date.Parse(datSstring)
        Catch ex As Exception
            ' 不正値の場合、算出月末を締め日とする
            editDate = dtCalcDateBegin.AddMonths(1).AddDays(-1)
        End Try

        ' 当日日付以前の場合、翌月にする
        If editDate < dtCalcCheck Then

            datSstring = GetDateStrReplaceDay(dtCalcCheck.AddMonths(1), strSimeDate)
            Try
                ' 日付型に変換する
                editDate = Date.Parse(datSstring)
            Catch ex As Exception
                ' 不正値の場合、翌月末を締め日とする
                editDate = dtThisMonthBegin.AddMonths(2).AddDays(-1)
            End Try
        End If

        Return editDate
    End Function

    ''' <summary>
    ''' 月末取得関数
    ''' </summary>
    ''' <param name="dtDate">月末を取得したい月の任意日</param>
    ''' <returns>月末日付</returns>
    ''' <remarks></remarks>
    Public Function GetEndOfMonth(ByVal dtDate As Date) As Date
        Dim dtEndOfMonth As Date

        dtEndOfMonth = Date.Parse(GetDateStrReplaceDay(dtDate, BEGIN_DAY)).AddMonths(1).AddDays(-1)

        Return dtEndOfMonth

    End Function

    ''' <summary>
    ''' 日付の日を指定した日に置換する
    ''' </summary>
    ''' <param name="dtDate">置換する日付</param>
    ''' <param name="strDay">指定日</param>
    ''' <returns></returns>
    ''' <remarks>指定日に置換した日付文字列</remarks>
    Public Function GetDateStrReplaceDay(ByVal dtDate As Date, ByVal strDay As String) As String
        Dim strDate As String
        strDate = dtDate.Year.ToString & "/" & dtDate.Month.ToString(FORMAT_DAY) & "/" & strDay

        Return strDate
    End Function
#End Region

#Region "基本請求先取得処理"
    Public ReadOnly dicKeySeikyuuSakiCd As String = "CD"
    Public ReadOnly dicKeySeikyuuSakiBrc As String = "BRC"
    Public ReadOnly dicKeySeikyuuSakiKbn As String = "KBN"
    Public ReadOnly dicKeySeikyuuSakiMei As String = "MEI"

    ''' <summary>
    ''' 基本請求先設定処理(調査会社から)
    ''' </summary>
    ''' <param name="strSearchMiseCd">調査会社コード</param>
    ''' <returns>請求先のKeyを格納したDictionary</returns>
    ''' <remarks></remarks>
    Public Function getDefaultSeikyuuSaki(ByVal strSearchMiseCd As String) As Dictionary(Of String, String)
        Dim strDefaultSeikyuuSaki As String = String.Empty

        '調査会社マスタから請求先を取得
        Dim tysLogic As New TyousakaisyaSearchLogic
        Dim tysRec As TysKaisyaRecord
        Dim retDic As New Dictionary(Of String, String)
        Dim listSeikyuuSaki As List(Of SeikyuuSakiInfoRecord)
        Dim UriDtAcc As New UriageDataSearchLogic
        Dim strSeikyuuSakiMei As String

        tysRec = tysLogic.getTysKaisyaInfo(strSearchMiseCd)
        With tysRec
            listSeikyuuSaki = UriDtAcc.GetSeikyuuSakiInfo(.SeikyuuSakiCd, .SeikyuuSakiBrc, .SeikyuuSakiKbn)
            If listSeikyuuSaki IsNot Nothing AndAlso listSeikyuuSaki.Count > 0 Then
                strSeikyuuSakiMei = listSeikyuuSaki(0).SeikyuuSakiMei
            Else
                strSeikyuuSakiMei = String.Empty
            End If

            retDic.Add(dicKeySeikyuuSakiCd, .SeikyuuSakiCd)
            retDic.Add(dicKeySeikyuuSakiBrc, .SeikyuuSakiBrc)
            retDic.Add(dicKeySeikyuuSakiKbn, .SeikyuuSakiKbn)
            retDic.Add(dicKeySeikyuuSakiMei, strSeikyuuSakiMei)
        End With

        Return retDic
    End Function

    ''' <summary>
    ''' 基本請求先設定処理(加盟店から)
    ''' </summary>
    ''' <param name="strSearchMiseCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>請求先のKeyを格納したDictionary</returns>
    ''' <remarks></remarks>
    Public Function getDefaultSeikyuuSaki(ByVal strSearchMiseCd As String, ByVal strSyouhinCd As String) As Dictionary(Of String, String)
        Dim strDefaultSeikyuuSaki As String = String.Empty

        '加盟店マスタから請求先を取得
        Dim uriLogic As New UriageDataSearchLogic
        Dim listSeikyuuSakiRec As List(Of SeikyuuSakiInfoRecord)
        Dim retDic As New Dictionary(Of String, String)

        listSeikyuuSakiRec = uriLogic.GetKameitenSeikyuuSakiInfo(strSearchMiseCd, strSyouhinCd)
        If listSeikyuuSakiRec IsNot Nothing AndAlso listSeikyuuSakiRec.Count > 0 Then

            With listSeikyuuSakiRec(0)
                retDic.Add(dicKeySeikyuuSakiCd, .SeikyuuSakiCd)
                retDic.Add(dicKeySeikyuuSakiBrc, .SeikyuuSakiBrc)
                retDic.Add(dicKeySeikyuuSakiKbn, .SeikyuuSakiKbn)
                retDic.Add(dicKeySeikyuuSakiMei, .SeikyuuSakiMei)
            End With
        Else
            retDic.Add(dicKeySeikyuuSakiCd, String.Empty)
            retDic.Add(dicKeySeikyuuSakiBrc, String.Empty)
            retDic.Add(dicKeySeikyuuSakiKbn, String.Empty)
            retDic.Add(dicKeySeikyuuSakiMei, String.Empty)
        End If

        Return retDic
    End Function

#End Region

#Region "売上金額の消費税額設定"
    ''' <summary>
    ''' 消費税額の設定
    ''' </summary>
    ''' <param name="teibetuRec">更新用邸別請求レコードクラス</param>
    ''' <param name="checkRec">DB値チェック用邸別請求レコードクラス</param>
    ''' <remarks>
    ''' 邸別請求T更新時、消費税額を自動設定する()
    ''' </remarks>
    Public Sub SetUriageSyouhiZeiGaku(ByRef teibetuRec As TeibetuSeikyuuRecord _
                                    , Optional ByVal checkRec As TeibetuSeikyuuRecord = Nothing)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSyouhiZeiGaku" _
                                                    , teibetuRec _
                                                    , checkRec)
        If teibetuRec Is Nothing Then
            Exit Sub
        End If

        Dim intSyouhiZeiGaku As Integer = 0
        Dim syouhinDtAcc As New SyouhinDataAccess
        Dim dtbl As DataTable
        Dim syouhinRec As SyouhinMeisaiRecord = Nothing

        '商品情報の取得
        dtbl = syouhinDtAcc.GetSyouhinInfo(teibetuRec.SyouhinCd)
        If dtbl.Rows.Count > 0 Then
            syouhinRec = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), dtbl)(0)
        End If

        '消費税額の設定
        With teibetuRec
            If checkRec IsNot Nothing Then
                '基本は売上金額 + 消費税額（邸別請求テーブルの消費税額）
                .UriageSyouhiZeiGaku = checkRec.UriageSyouhiZeiGaku
                .SiireSyouhiZeiGaku = checkRec.SiireSyouhiZeiGaku
            End If
            If checkRec Is Nothing OrElse .SyouhinCd <> checkRec.SyouhinCd Then
                '商品が変更され、変更された商品の税率が取得出来た場合
                If syouhinRec IsNot Nothing Then
                    '売上金額 * 税率（商品マスタの税区分）
                    .UriageSyouhiZeiGaku = Fix(.UriGaku * syouhinRec.Zeiritu)
                    .SiireSyouhiZeiGaku = Fix(.SiireGaku * syouhinRec.Zeiritu)
                End If
            End If
            If checkRec IsNot Nothing AndAlso .SyouhinCd = checkRec.SyouhinCd AndAlso .UriGaku <> checkRec.UriGaku Then
                '金額が変更された場合
                If syouhinRec IsNot Nothing Then
                    '売上金額 * 税率（商品マスタの税区分）
                    .UriageSyouhiZeiGaku = Fix(.UriGaku * checkRec.Zeiritu)
                End If
            End If
            If checkRec IsNot Nothing AndAlso .SyouhinCd = checkRec.SyouhinCd AndAlso .SiireGaku <> checkRec.SiireGaku Then
                '金額が変更された場合
                If syouhinRec IsNot Nothing Then
                    '仕入金額 * 税率（商品マスタの税区分）
                    .SiireSyouhiZeiGaku = Fix(.SiireGaku * checkRec.Zeiritu)
                End If
            End If

        End With
    End Sub
#End Region

#Region "保証書商品有無対応"
    ''' <summary>
    ''' 保証書発行状況Mの保証あり/なしを取得する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetHosyousyoHakJykyInfo(ByRef dictmp As Dictionary(Of String, String))

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoHakJykyInfo")

        Dim dataAccess As New HosyousyoHakJykyAccess
        Dim dtTbl As New DataTable

        dataAccess.GetHosyousyoHakJykyInfo(dtTbl, dictmp)

    End Sub

    ''' <summary>
    ''' 商品群の保証有無を取得する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSyouhinHosyouUmu(ByVal strSyouhinCds As String) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinHosyouUmu" & strSyouhinCds)

        Dim dataAccess As New SyouhinDataAccess
        Dim dtTbl As New DataTable
        Dim strRet As String = String.Empty

        dtTbl = dataAccess.GetSyouhinHosyouUmuInfo(strSyouhinCds)
        ' 取得できなかった場合は空白を返却
        If dtTbl.Rows.Count > 0 Then
            Dim row As DataRow = dtTbl.Rows(0)
            strRet = row("hosyou_umu").ToString()
        End If

        Return strRet
    End Function

    ''' <summary>
    ''' 保証書発行状況NOを元に、名称を取得する
    ''' </summary>
    ''' <param name="strHosyousyoHakJykyNo">保証書発行状況NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoHakJykyMei(ByVal strHosyousyoHakJykyNo As String) As String
        Dim strRetMei As String = String.Empty

        Dim dataAccess As New HosyousyoHakJykyAccess

        If strHosyousyoHakJykyNo <> String.Empty Then
            dataAccess.getMeisyou(strHosyousyoHakJykyNo, strRetMei)
        End If

        Return strRetMei
    End Function

#Region "保証書発行状況の自動設定(保証商品有無対応)"

    ''' <summary>
    ''' 保証書発行状況の自動設定を行なうかの判断を返す
    ''' </summary>
    ''' <param name="strHosyousyoHakJyky">保証書発行状況</param>
    ''' <returns>True：自動設定する、False：自動設定しない</returns>
    ''' <remarks></remarks>
    Public Function ChkAutoHosyousyoHakJyky( _
                                            ByVal strHosyousyoHakJyky As String _
                                            ) As Boolean

        Dim blnAutoSetFlg As Boolean = False '自動設定可否判断フラグ

        ' 保証書発行状況
        If strHosyousyoHakJyky = String.Empty Then '空白
            blnAutoSetFlg = True

        ElseIf strHosyousyoHakJyky = EarthConst.AUTO_SET_VAL_HOSYOU_ARI Then '保証有(自動設定)
            blnAutoSetFlg = True

        ElseIf strHosyousyoHakJyky = EarthConst.AUTO_SET_VAL_HOSYOU_NASI Then '保証無(自動設定)
            blnAutoSetFlg = True

        End If

        Return blnAutoSetFlg
    End Function

    ''' <summary>
    ''' 保証書発行状況の自動設定の判定を行ない、値を取得する
    ''' </summary>
    ''' <param name="strHosyousyoHakJykyOld">保証書発行状況Old</param>
    ''' <param name="strHosyouSyouhinUmuOld">保証商品有無Old</param>
    ''' <param name="strHosyouSyouhinUmu">保証商品有無</param>
    ''' <returns>保証書発行状況</returns>
    ''' <remarks></remarks>
    Public Function GetAutoHosyousyoHakJyky( _
                                            ByVal strHosyousyoHakJykyOld As String _
                                            , ByVal strHosyouSyouhinUmuOld As String _
                                            , ByVal strHosyouSyouhinUmu As String _
                                            ) As String

        Dim strRetHosyousyoHakJyky As String = String.Empty

        '画面.保証書発行状況＝未入力or自動設定(保証あり、なし)
        If strHosyousyoHakJykyOld = String.Empty _
            OrElse strHosyousyoHakJykyOld = EarthConst.AUTO_SET_VAL_HOSYOU_ARI _
                OrElse strHosyousyoHakJykyOld = EarthConst.AUTO_SET_VAL_HOSYOU_NASI Then

            '保証商品有無Old
            If strHosyouSyouhinUmuOld = String.Empty Then '未入力(=新規)
                If strHosyouSyouhinUmu = "1" Then
                    ' 保証書発行状況
                    strRetHosyousyoHakJyky = EarthConst.AUTO_SET_VAL_HOSYOU_ARI

                ElseIf strHosyouSyouhinUmu = "0" Then
                    ' 保証書発行状況
                    strRetHosyousyoHakJyky = EarthConst.AUTO_SET_VAL_HOSYOU_NASI

                Else '上記以外の場合(変更なし)
                    strRetHosyousyoHakJyky = strHosyousyoHakJykyOld

                End If

            ElseIf strHosyouSyouhinUmuOld = "0" Then
                If strHosyouSyouhinUmu = "1" Then
                    ' 保証書発行状況
                    strRetHosyousyoHakJyky = EarthConst.AUTO_SET_VAL_HOSYOU_ARI

                ElseIf strHosyouSyouhinUmu = "0" Then
                    ' 保証書発行状況(変更なし。ただし、未設定の場合自動設定)
                    If strHosyousyoHakJykyOld = String.Empty Then
                        strHosyousyoHakJykyOld = EarthConst.AUTO_SET_VAL_HOSYOU_NASI
                    End If
                    strRetHosyousyoHakJyky = strHosyousyoHakJykyOld

                Else '上記以外の場合(変更なし)
                    strRetHosyousyoHakJyky = strHosyousyoHakJykyOld

                End If

            ElseIf strHosyouSyouhinUmuOld = "1" Then
                If strHosyouSyouhinUmu = "1" Then
                    ' 保証書発行状況(変更なし。ただし、未設定の場合自動設定)
                    If strHosyousyoHakJykyOld = String.Empty Then
                        strHosyousyoHakJykyOld = EarthConst.AUTO_SET_VAL_HOSYOU_ARI
                    End If
                    ' 保証書発行状況
                    strRetHosyousyoHakJyky = strHosyousyoHakJykyOld

                ElseIf strHosyouSyouhinUmu = "0" Then
                    strRetHosyousyoHakJyky = EarthConst.AUTO_SET_VAL_HOSYOU_NASI

                Else '上記以外の場合(変更なし)
                    strRetHosyousyoHakJyky = strHosyousyoHakJykyOld

                End If

            End If

        Else '上記以外
            ' 保証書発行状況(変更なし)
            strRetHosyousyoHakJyky = strHosyousyoHakJykyOld

        End If

        Return strRetHosyousyoHakJyky
    End Function

    ''' <summary>
    ''' 該当の保証書発行状況の保証あり/なしを返却する
    ''' </summary>
    ''' <param name="strHosyousyoHakJyky">保証書発行状況</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoHakJykyHosyouUmu(ByVal strHosyousyoHakJyky As String) As String
        Dim strRet As String = "0"
        Dim cbLogic As New CommonBizLogic
        Dim dicHosyousyoHakInfo = New Dictionary(Of String, String)

        '未設定時
        If strHosyousyoHakJyky = String.Empty Then
            Return strRet
        End If

        '●保証書発行状況 保証商品有無情報の取得
        cbLogic.GetHosyousyoHakJykyInfo(dicHosyousyoHakInfo)

        If Not dicHosyousyoHakInfo Is Nothing Then
            '保証書発行状況
            If strHosyousyoHakJyky <> String.Empty Then '選択あり
                If dicHosyousyoHakInfo.ContainsKey(strHosyousyoHakJyky) Then
                    If dicHosyousyoHakInfo.Item(strHosyousyoHakJyky) = "0" Then
                        strRet = "0"

                    ElseIf dicHosyousyoHakInfo.Item(strHosyousyoHakJyky) = "1" Then
                        strRet = "1"
                    End If

                End If
            End If
        End If

        Return strRet
    End Function

    ''' <summary>
    ''' 商品の変更時における保証有無の判定を行い、結果を返却する
    ''' </summary>
    ''' <param name="strSyouhinCds">商品コード群(セパレータ区切り)</param>
    ''' <returns>保証有無(1:保証あり,0:保証なし)</returns>
    ''' <remarks></remarks>
    Public Function ChkChgSyouhinHosyouSyouhinUmu(ByVal strSyouhinCds As String) As String
        Dim strRet As String = String.Empty
        Dim cbLogic As New CommonBizLogic

        If strSyouhinCds <> String.Empty Then
            strRet = cbLogic.GetSyouhinHosyouUmu(strSyouhinCds)
        End If

        Return strRet
    End Function

    ''' <summary>
    ''' 画面で変更された商品を元に、保証商品有無を判定する(商品１～３)
    ''' ※必須条件：地盤Tに紐づく邸別請求T(商品１～３)が既に設定されていること※
    ''' </summary>
    ''' <param name="jibanRec">地盤レコードクラス</param>
    ''' <returns>保証有無(1:保証あり,0:保証なし)</returns>
    ''' <remarks></remarks>
    Public Function ChkChgSyouhinHosyouUmu(ByVal jibanRec As JibanRecordBase) As String
        Dim jibanTmp As New JibanRecordBase '作業用
        Dim strRet As String = String.Empty '保証商品有無(0:無し,1:有り)
        Dim strSyouhinCds As String = String.Empty '商品コード１～３のセパレータ区切り

        '地盤レコードが存在する場合
        If jibanRec Is Nothing Then
            Return strRet
            Exit Function
        End If

        '商品1
        If Not jibanRec.Syouhin1Record Is Nothing Then
            If jibanRec.Syouhin1Record.SyouhinCd <> String.Empty Then
                strSyouhinCds &= jibanRec.Syouhin1Record.SyouhinCd & EarthConst.SEP_STRING
            End If
        End If

        '商品2,3の取得
        jibanTmp.Syouhin2Records = jibanRec.Syouhin2Records
        jibanTmp.Syouhin3Records = jibanRec.Syouhin3Records

        '商品2
        If Not jibanTmp.Syouhin2Records Is Nothing Then
            For intCnt As Integer = 1 To EarthConst.SYOUHIN2_COUNT
                If jibanTmp.Syouhin2Records.ContainsKey(intCnt) Then
                    strSyouhinCds &= jibanTmp.Syouhin2Records.Item(intCnt).SyouhinCd() & EarthConst.SEP_STRING
                End If
            Next
        End If

        '商品3
        If Not jibanTmp.Syouhin3Records Is Nothing Then
            For intCnt As Integer = 1 To EarthConst.SYOUHIN3_COUNT
                If jibanTmp.Syouhin3Records.ContainsKey(intCnt) Then
                    strSyouhinCds &= jibanTmp.Syouhin3Records.Item(intCnt).SyouhinCd() & EarthConst.SEP_STRING
                End If
            Next
        End If

        'いずれかの商品がセットされている場合、保証有無を判定
        If strSyouhinCds <> String.Empty Then
            strRet = Me.ChkChgSyouhinHosyouSyouhinUmu(strSyouhinCds)
        End If

        '取得できなかった場合、なしを明示的に返却
        If strRet = String.Empty Then
            strRet = "0"
        End If

        Return strRet
    End Function

    ''' <summary>
    ''' 保証書発行状況および保証書発行状況設定日、保証商品有無の自動設定処理を行なう。
    ''' ※商品１～３の保証あり/なし状況と、保証書発行状況の保証有無を判定し、更新用レコードクラスに自動設定する
    ''' </summary>
    ''' <param name="emGamenInfo">呼出元画面情報</param>
    ''' <param name="jibanRec">自動設定対象の地盤レコード</param>
    ''' <param name="strHosyousyoHakJyky">最新の保証書発行状況(保証画面は画面値をセットする)</param>
    ''' <remarks></remarks>
    Public Sub ps_AutoSetHosyousyoHakJyjy( _
                                        ByVal emGamenInfo As EarthEnum.emGamenInfo _
                                        , ByRef jibanRec As JibanRecordBase _
                                        , Optional ByVal strHosyousyoHakJyky As String = "" _
                                        )

        Dim jibanRecOld As New JibanRecordBase
        Dim JLogic As New JibanLogic

        ' 現在の地盤データをDBから再取得する
        jibanRecOld = JLogic.GetJibanData(jibanRec.Kbn, jibanRec.HosyousyoNo)

        '保証書発行状況Old
        Dim strHosyousyoHakJykyOld As String = String.Empty
        '保証書発行状況設定日Old
        Dim strHosyousyoHakJykySetteiDataOld As String = String.Empty
        '保証商品有無Old
        Dim strHosyouSyouhinUmuOld As String = String.Empty

        '保証書発行状況(更新用)
        Dim strTmpValHosyousyoHakJyky As String = String.Empty
        '保証書発行状況設定日(更新用)
        Dim strTmpValHosyousyoHakJykySetteiDate As String = String.Empty
        '保証商品有無を取得(更新用)
        Dim strHosyouSyouhinUmu As String = String.Empty

        '●Old値
        strHosyousyoHakJykyOld = Me.GetDispNum(jibanRecOld.HosyousyoHakJyky, String.Empty)
        strHosyousyoHakJykySetteiDataOld = Me.GetDisplayString(jibanRecOld.HosyousyoHakJykySetteiDate)
        strHosyouSyouhinUmuOld = Me.GetDispNum(jibanRecOld.HosyouSyouhinUmu)

        '●保証商品有無
        Select Case emGamenInfo
            Case EarthEnum.emGamenInfo.MousikomiInput, EarthEnum.emGamenInfo.Jutyuu, EarthEnum.emGamenInfo.IkkatuTysSyouhinInfo, EarthEnum.emGamenInfo.TeibetuSyuusei, _
                    EarthEnum.emGamenInfo.MousikomiSearch, EarthEnum.emGamenInfo.MousikomiSyuusei, _
                        EarthEnum.emGamenInfo.FcMousikomiSearch, EarthEnum.emGamenInfo.FcMousikomiSyuusei

                '画面値をセット
                strHosyouSyouhinUmu = Me.ChkChgSyouhinHosyouUmu(jibanRec)

            Case Else
                strHosyouSyouhinUmu = Me.ChkChgSyouhinHosyouUmu(jibanRecOld)

        End Select

        '●保証書発行状況
        Select Case emGamenInfo
            Case EarthEnum.emGamenInfo.Hosyou '保証画面
                '画面値をセット
                strTmpValHosyousyoHakJyky = strHosyousyoHakJyky

            Case Else
                strTmpValHosyousyoHakJyky = Me.GetDispNum(jibanRecOld.HosyousyoHakJyky, "")

        End Select

        '自動設定対象かをチェックする
        If Me.ChkAutoHosyousyoHakJyky(strTmpValHosyousyoHakJyky) Then '自動設定対象
            '保証書発行状況
            strTmpValHosyousyoHakJyky = Me.GetAutoHosyousyoHakJyky(strTmpValHosyousyoHakJyky, strHosyouSyouhinUmuOld, strHosyouSyouhinUmu) '自動設定
        End If
        ' ●保証書発行状況
        Me.SetDisplayString(strTmpValHosyousyoHakJyky, jibanRec.HosyousyoHakJyky)

        ' 保証書発行状況の変更チェック
        If strTmpValHosyousyoHakJyky = String.Empty Then
            strTmpValHosyousyoHakJykySetteiDate = String.Empty
        ElseIf strTmpValHosyousyoHakJyky = strHosyousyoHakJykyOld Then
            strTmpValHosyousyoHakJykySetteiDate = strHosyousyoHakJykySetteiDataOld
        Else
            strTmpValHosyousyoHakJykySetteiDate = Date.Now.ToString("yyyy/MM/dd")
        End If
        ' ●保証書発行状況設定日
        Me.SetDisplayString(strTmpValHosyousyoHakJykySetteiDate, jibanRec.HosyousyoHakJykySetteiDate)

        ' ●保証商品有無
        Me.SetDisplayString(strHosyouSyouhinUmu, jibanRec.HosyouSyouhinUmu)

    End Sub

#Region "物件履歴Tへの自動設定内容"
#Region "保証書発行状況の自動設定(保証有無対応)"

    ''' <summary>
    ''' 物件履歴レコードのチェック
    ''' </summary>
    ''' <param name="brRec">物件履歴レコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkInputBukkenRireki(ByRef brRec As BukkenRirekiRecord, ByRef errMess As String) As Boolean
        Dim strCtrlName As String = "内容"
        Dim strNaiyou As String = brRec.Naiyou

        '●必須チェック
        '履歴NOが以下でない場合、エラー
        If brRec.RirekiNo = 1 Then
        Else
            errMess += Messages.MSG147E.Replace("@PARAM1", "物件履歴データの自動設定")
        End If
        '内容
        If strNaiyou = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", strCtrlName)
        End If

        '●禁則文字チェック(文字列入力フィールドが対象)
        '禁則文字を置換
        strLogic.KinsiStrClear(strNaiyou)
        If strLogic.KinsiStrCheck(strNaiyou) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", strCtrlName)
        End If
        '改行変換(物件履歴T.内容)
        If strNaiyou <> "" Then
            strNaiyou = strNaiyou.Replace(vbCrLf, " ")
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        If strLogic.ByteCheckSJIS(strNaiyou, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", strCtrlName)
        End If

        'エラー発生時処理
        If errMess <> "" Then
            Return False
            Exit Function
        End If

        brRec.Naiyou = strNaiyou
        Return True
    End Function

    ''' <summary>
    ''' 物件履歴データを作成する[共通]
    ''' </summary>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="strHosyouSyouhinUmuOld">保証商品有無Old</param>
    ''' <param name="strKeikakusyoSakuseiDateOld">計画書作成日Old</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MakeBukkenRirekiRecHosyouUmu( _
                                                ByVal jibanRec As JibanRecordBase _
                                                , ByVal strHosyouSyouhinUmuOld As String _
                                                , ByVal strKeikakusyoSakuseiDateOld As String _
                                                ) As BukkenRirekiRecord

        '保証商品有無(更新用レコードクラスに画面で判定した保証商品有無をセットしていること)
        Dim strHosyouSyouhinUmu As String = Me.GetDisplayString(jibanRec.HosyouSyouhinUmu)

        ' 以下の条件に該当する場合、物件履歴への書込は行わない
        ' 保証商品有無Old(設定済)<>画面.保証商品有無でかつ、計画書作成日Old<>NULL
        If strHosyouSyouhinUmuOld <> String.Empty _
            AndAlso strHosyouSyouhinUmuOld <> strHosyouSyouhinUmu _
                AndAlso strKeikakusyoSakuseiDateOld <> String.Empty Then
        Else
            Return Nothing
        End If

        '物件履歴登録用レコード
        Dim record As New BukkenRirekiRecord

        ' 区分
        record.Kbn = jibanRec.Kbn
        ' 保証書NO
        record.HosyousyoNo = jibanRec.HosyousyoNo
        '履歴種別
        record.RirekiSyubetu = EarthConst.BUKKEN_RIREKI_RIREKI_SYUBETU_HOSYOUSYO_HAK_JYKY
        '履歴NO
        record.RirekiNo = 1
        '入力NO
        record.NyuuryokuNo = Integer.MinValue
        '内容
        record.Naiyou = Me.SetNaiyou(strHosyouSyouhinUmuOld, strHosyouSyouhinUmu, jibanRec)
        '汎用日付
        Me.SetDisplayString("", record.HanyouDate)
        '汎用コード
        Me.SetDisplayString("", record.HanyouCd)
        '管理日付
        Me.SetDisplayString("", record.KanriDate)
        '管理コード
        Me.SetDisplayString("", record.KanriCd)
        '変更可否フラグ
        record.HenkouKahiFlg = 1
        '取消
        record.Torikesi = 0
        '登録(更新)ログインユーザID
        record.UpdLoginUserId = jibanRec.UpdLoginUserId
        '登録(更新)日時
        record.UpdDatetime = jibanRec.UpdDatetime

        Return record
    End Function

    ''' <summary>
    ''' 条件毎に内容をセットする
    ''' </summary>
    ''' <param name="strHosyouSyouhinUmuOld">保証商品有無Old</param>
    ''' <param name="strHosyouSyouhinUmu">保証商品有無</param>
    ''' <param name="jibanRec">更新用地盤レコードクラス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetNaiyou(ByVal strHosyouSyouhinUmuOld As String, ByVal strHosyouSyouhinUmu As String, ByVal jibanRec As JibanRecordBase) As String
        Dim strRetVal As String = String.Empty '戻り値

        Dim cbLogic As New CommonBizLogic

        '保証書発行状況
        Dim strHosyousyoHakJyky As String = Me.GetDisplayString(jibanRec.HosyousyoHakJyky)
        '保証書発行状況名
        Dim strHosyousyoHakJykyMei As String = cbLogic.GetHosyousyoHakJykyMei(strHosyousyoHakJyky)
        '登録日付
        Dim strUpdDateTime As String = Format(DateTime.Now, EarthConst.FORMAT_DATE_TIME_2)
        '登録ユーザID
        Dim strUpdLoginUserId As String = Me.GetDisplayString(jibanRec.UpdLoginUserId)

        '**************
        '*変更前
        '**************
        '保証商品有無
        strRetVal = strHosyouSyouhinUmuOld

        '**************
        '*変更後
        '**************
        '保証商品有無
        strRetVal &= EarthConst.BUKKEN_RIREKI_SEP_STR_HOSYOU & strHosyouSyouhinUmu

        '**************
        '* 保証書発行状況
        '**************
        strRetVal &= EarthConst.BUKKEN_RIREKI_SEP_STR_HOSYOU & strHosyousyoHakJyky & EarthConst.BUKKEN_RIREKI_SEP_STR_HOSYOU & strHosyousyoHakJykyMei

        '**************
        '* 更新日時
        '**************
        strRetVal &= EarthConst.BUKKEN_RIREKI_SEP_STR_HOSYOU & strUpdDateTime

        '**************
        '* 更新者ID
        '**************
        strRetVal &= EarthConst.BUKKEN_RIREKI_SEP_STR_HOSYOU & strUpdLoginUserId

        Return strRetVal
    End Function

#End Region
#End Region

#End Region

#End Region

#Region "調査概要の自動設定"
    ''' <summary>
    ''' 調査概要の自動設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ps_AutoSetTysGaiyou(ByRef jibanRec As JibanRecordBase)
        Dim JibanLogic As New JibanLogic

        ' ●調査概要
        '設定・取得用 商品価格設定レコード
        Dim recKakakuSettei As New KakakuSetteiRecord

        '商品区分
        recKakakuSettei.SyouhinKbn = jibanRec.SyouhinKbn
        '調査方法
        Me.SetDisplayString(jibanRec.TysHouhou, recKakakuSettei.TyousaHouhouNo)
        '商品コード
        Me.SetDisplayString(jibanRec.SyouhinCd1, recKakakuSettei.SyouhinCd)

        '商品価格設定マスタから値の取得
        JibanLogic.GetTysGaiyou(recKakakuSettei)

        '調査概要の設定
        jibanRec.TysGaiyou = Me.GetDispNum(recKakakuSettei.TyousaGaiyou, Integer.MinValue)
    End Sub

#End Region

#Region "保証書関連対応"
    ''' <summary>
    ''' 保証書管理データの判定処理を行なう
    ''' ・解析完了：完了
    ''' ・工事有り無し：
    '''      有りの場合、工事完了：工事完了
    '''      無しの場合、工事完了：受注無し
    ''' ・入金確認条件：
    '''      2の場合、入金状況：発注確認書有り
    '''      6の場合、入金状況：不要
    '''      2,6以外(NULL含む)の場合、入金状況：入金有り
    ''' ・瑕疵：無し or 有完了
    ''' 
    ''' 保証書管理データが未存在の場合、Falseを返却
    ''' 保証書管理データが存在する場合、すべて"完了"の場合、1:Trueを返却
    ''' 以外の場合、0:Falseを返却
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoKanriJyky(ByVal strKbn As String, ByVal strBangou As String) As Integer
        Dim intRet As Integer = Integer.MinValue

        Dim dataAccess As New BukkenSintyokuJykyDataAccess

        If strKbn <> String.Empty And strBangou <> String.Empty Then
            intRet = dataAccess.ChkHosyousyoJyky(strKbn, strBangou)
        End If

        Return intRet
    End Function

#End Region

#Region "商品設定関連"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="emSetSts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyouhin1ErrMsg(ByVal emSetSts As EarthEnum.emSyouhin1Error) As String

        Dim strRtnMsg As String = String.Empty
        Dim strCrlf As String = "\r\n"

        Select Case emSetSts
            Case EarthEnum.emSyouhin1Error.GetGenka
            Case EarthEnum.emSyouhin1Error.GetHanbai
            Case EarthEnum.emSyouhin1Error.GetGenkaHanbai
            Case EarthEnum.emSyouhin1Error.GetTysGaiyou
            Case Else
                '商品1取得不可時
                strRtnMsg = Messages.MSG032E & strCrlf
        End Select

        If emSetSts = EarthEnum.emSyouhin1Error.GetKakakuSetteiBasyo Then
            strRtnMsg += Messages.MSG122E.Replace("@PARAM1", "商品1価格情報") & "【価格設定場所取得エラー】\r\n"
        ElseIf emSetSts = EarthEnum.emSyouhin1Error.GetSyouhin1 Then
            strRtnMsg += Messages.MSG122E.Replace("@PARAM1", "商品1情報") & "【商品1取得エラー】\r\n"
        ElseIf emSetSts = EarthEnum.emSyouhin1Error.KameiSyouhinTaisyougai Then
            strRtnMsg += Messages.MSG122E.Replace("@PARAM1", "商品1価格情報") & "【加盟店マスタ価格取得商品対象外エラー】\r\n"
        ElseIf emSetSts = EarthEnum.emSyouhin1Error.GetKameiKakaku Then
            strRtnMsg += Messages.MSG122E.Replace("@PARAM1", "商品1価格情報") & "【加盟店マスタ価格取得エラー】\r\n"
        ElseIf emSetSts = EarthEnum.emSyouhin1Error.GetGenkaHanbai Then
            strRtnMsg += Messages.MSG180E
            strRtnMsg += Messages.MSG182E
        ElseIf emSetSts = EarthEnum.emSyouhin1Error.GetGenka Then
            strRtnMsg += Messages.MSG180E
        ElseIf emSetSts = EarthEnum.emSyouhin1Error.GetTysGaiyou Then
            strRtnMsg += Messages.MSG183E
        ElseIf emSetSts = EarthEnum.emSyouhin1Error.GetHanbai Then
            strRtnMsg += Messages.MSG182E
        End If

        Return strRtnMsg
    End Function

#End Region

#Region "データ入出力関連"

    ''' <summary>
    ''' 画面表示用にオブジェクトを文字列変換する
    ''' </summary>
    ''' <param name="obj">表示対象のデータ</param>
    ''' <param name="str">未設定時の初期値</param>
    ''' <returns>表示形式の文字列</returns>
    ''' <remarks>
    ''' Decimal  : Minvalueを空白、それ以外は入力値をString型で返却<br/>
    ''' Integer  : Minvalueを空白、それ以外は入力値をString型で返却<br/>
    ''' DateTime : MinDateTimeを空白、それ以外は入力値を YYYY/MM/DD 形式のString型で返却<br/>
    ''' 上記以外 : そのまま。適宜追加してください
    ''' </remarks>
    Public Function GetDisplayString(ByVal obj As Object, _
                                     Optional ByVal str As String = "") As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDisplayString", _
                                                    obj, _
                                                    str)

        ' 戻り値となるStringデータ
        Dim ret As String = str

        If obj Is Nothing Then
            ' NULL は基本的に空白を返す
            Return ret
        ElseIf obj.GetType().ToString() = GetType(String).ToString Then
            ' Stringの場合
            If Trim(obj) = "" Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(Integer).ToString Then
            ' Integerの場合
            If obj = Integer.MinValue Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimalの場合
            If obj = Decimal.MinValue Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTimeの場合
            If obj = DateTime.MinValue Then
                Return ret
            Else
                ret = Format(obj, "yyyy/MM/dd")
            End If
        ElseIf obj.GetType().ToString() = GetType(Long).ToString Then
            ' Longの場合
            If obj = Long.MinValue Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        Else
            ret = obj.ToString()
        End If

        Return ret

    End Function

    ''' <summary>
    ''' 画面表示用にオブジェクトを文字列変換する[Datetimeの年月日時分秒まで表示]
    ''' </summary>
    ''' <param name="obj">表示対象のデータ</param>
    ''' <param name="str">未設定時の初期値</param>
    ''' <returns>表示形式の文字列</returns>
    ''' <remarks>
    ''' DateTime : MinDateTimeを空白、それ以外は入力値を YYYY/MM/DD 形式のString型で返却<br/>
    ''' </remarks>
    Public Function GetDispStrDateTime(ByVal obj As Object, _
                                     Optional ByVal str As String = "") As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDispStrDateTime", _
                                                    obj, _
                                                    str)

        ' 戻り値となるStringデータ
        Dim ret As String = str

        If obj Is Nothing Then
            ' NULL は基本的に空白を返す
            Return ret
        ElseIf obj.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTimeの場合
            If obj = DateTime.MinValue Then
                Return ret
            Else
                ret = Format(obj, EarthConst.FORMAT_DATE_TIME_7)
            End If
        Else
            ret = obj.ToString()
        End If

        Return ret

    End Function

    ''' <summary>
    ''' 画面表示用文字列を指定した型に変換する
    ''' </summary>
    ''' <param name="strData">変換対象のデータ</param>
    ''' <param name="objChangeData">変換後の型データ（参照）</param>
    ''' <returns>変換後の型データ</returns>
    ''' <remarks>
    ''' Decimal  : 空白をMinvalue、それ以外は入力値をDecimalに変換<br/>
    ''' Integer  : 空白をMinvalue、それ以外は入力値をIntegerに変換<br/>
    ''' DateTime : 空白をMinvalue、それ以外は入力値をDateTimeに変換<br/>
    ''' 上記以外 : そのまま。適宜追加してください<br/>
    ''' 変換に失敗した場合はFalseを返し、指定型のMinValueをセットします
    ''' </remarks>
    Public Function SetDisplayString(ByVal strData As String, _
                                     ByRef objChangeData As Object) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDisplayString", _
                                                    strData, _
                                                    objChangeData)

        If objChangeData Is Nothing Then
            ' 多分String
            objChangeData = strData
        End If

        If objChangeData.GetType().ToString() = GetType(Integer).ToString Then
            ' Integerへ変換
            If strData.Trim() = "" Then
                objChangeData = Integer.MinValue
                Return True
            Else
                Try
                    objChangeData = Integer.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' 変換に失敗した場合、MinValueを設定し、Falseを返却
                    objChangeData = Integer.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimalへ変換
            If strData.Trim() = "" Then
                objChangeData = Decimal.MinValue
                Return True
            Else
                Try
                    objChangeData = Decimal.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' 変換に失敗した場合、MinValueを設定し、Falseを返却
                    objChangeData = Decimal.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTimeへ変換
            If strData.Trim() = "" Then
                objChangeData = DateTime.MinValue
                Return True
            Else
                Try
                    objChangeData = DateTime.Parse(strData)
                    Return True
                Catch ex As Exception
                    ' 変換に失敗した場合、MinValueを設定し、Falseを返却
                    objChangeData = DateTime.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(String).ToString Then
            ' StringはTrimする
            objChangeData = strData.Trim()
            Return True
        ElseIf objChangeData.GetType().ToString() = GetType(Long).ToString Then
            ' Longへ変換
            If strData.Trim() = "" Then
                objChangeData = Long.MinValue
                Return True
            Else
                Try
                    objChangeData = Long.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' 変換に失敗した場合、MinValueを設定し、Falseを返却
                    objChangeData = Long.MinValue
                    Return False
                End Try
            End If
        End If

        ' 変換対象以外の型はエラー
        objChangeData = Nothing
        Return False

    End Function

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（数値系）
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <param name="strReturn">戻したい値をセット</param>
    ''' <returns>[String]第一引数の文字列 or 第二引数の値</returns>
    ''' <remarks>
    ''' 第一引数の値が取得できない場合、""(空白)を戻す<br/>
    ''' 第二引数で戻したい値を指定可能<br/>
    ''' </remarks>
    Public Function GetDispNum(ByVal objArg As Object, Optional ByVal strReturn As String = "0") As String
        Dim strTmp As String = Me.GetDisplayString(objArg)
        If strTmp.Length = 0 Then
            strTmp = "0"
        End If
        If strTmp = "0" And strReturn <> "0" Then
            strTmp = strReturn
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' DB登録用(ログインユーザ名、更新日付より地盤テーブル.更新者に設定する文字列を生成する)
    ''' </summary>
    ''' <param name="strUserID">ログインユーザID</param>
    ''' <param name="dtUpdDate">更新日付(yy/mm/dd hh:mm)</param>
    ''' <returns>ログインユーザID(15桁) + "$(区切り文字)" + 更新日付(yy/mm/dd hh:mm)</returns>
    ''' <remarks>ログインユーザIDが15桁超の場合、15桁で切る</remarks>
    Public Function GetKousinsya(ByVal strUserID As String, ByVal dtUpdDate As DateTime) As String
        Dim strLogic As New StringLogic
        Dim strReturn As String = ""

        'ログインユーザID(最大15桁) + "$(区切り文字)" + 更新日付(yy/mm/dd hh:mm)を返却する
        strReturn = strLogic.GetKousinsya(strUserID, dtUpdDate)
        Return strReturn
    End Function

#End Region

#Region "調査概要変更時エラーチェック[共通]"
    ''' <summary>
    ''' 調査概要と同時依頼棟数のチェック処理[共通]
    ''' </summary>
    ''' <param name="strTysGaiyou">調査概要</param>
    ''' <param name="strDoujiIraiTousuu">同時依頼棟数</param>
    ''' <param name="strErrMsg">エラーメッセージ</param>
    ''' <returns>
    ''' 調査概要と同時依頼棟数のチェックを行ない、
    ''' 条件に該当する場合、エラーメッセージを返却する
    ''' </returns>
    ''' <remarks>調査概要={62.63.64.65}、同時依頼棟数={9以下、10以上}</remarks>
    Public Function ChkErrTysGaiyou(ByVal strTysGaiyou As String, ByVal strDoujiIraiTousuu As String, ByRef strErrMsg As String) As Boolean
        Dim strTargetVal As New Dictionary(Of String, String)

        Dim intTmpTousuu As Integer

        'チェック対象の調査概要
        strTargetVal.Add("62", "62")
        strTargetVal.Add("63", "63")
        strTargetVal.Add("64", "64")
        strTargetVal.Add("65", "65")

        '同時依頼棟数=未入力の場合、「1」として扱う
        If strDoujiIraiTousuu = String.Empty Then
            intTmpTousuu = 1
        Else
            '数値チェック
            If IsNumeric(strDoujiIraiTousuu) Then
                intTmpTousuu = CInt(strDoujiIraiTousuu)
            End If
        End If

        '調査概要
        If strTargetVal.ContainsKey(strTysGaiyou) Then
            '同時依頼棟数
            If intTmpTousuu < 10 Then
                '調査概要
                If strTysGaiyou = "64" Or strTysGaiyou = "65" Then
                    strErrMsg = Messages.MSG145E.Replace("@PARAM1", "同時依頼棟数").Replace("@PARAM2", "9棟以下").Replace("@PARAM3", "調査概要")
                    Return False
                End If

            ElseIf intTmpTousuu >= 10 Then
                '調査概要
                If strTysGaiyou = "62" Or strTysGaiyou = "63" Then
                    strErrMsg = Messages.MSG145E.Replace("@PARAM1", "同時依頼棟数").Replace("@PARAM2", "10棟以上").Replace("@PARAM3", "調査概要")
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' ビルダー注意事項エラーチェック[共通]
    ''' </summary>
    ''' <param name="strTysGaiyou">調査概要</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strExistFlg">注意事項フラグ:ビルダー地盤診断費用負担(=13)が存在するかの文字列フラグ
    ''' [存在する:Boolean.TrueString,存在しない:Boolean.FalseString,加盟店未設定:String.Empty]
    ''' ※当処理呼出前に判断しておく必要有</param>
    ''' <param name="strErrMsg">エラーメッセージ</param>
    ''' <returns>
    ''' 調査概要とビルダー地盤診断費用負担(=13)のチェックを行ない、
    ''' 条件に該当する場合、エラーメッセージを返却する
    ''' </returns>
    ''' <remarks>調査概要={63.65}、注意事項フラグ</remarks>
    Public Function ChkErrBuilderData(ByVal strTysGaiyou As String, ByVal strKameitenCd As String, ByVal strExistFlg As String, ByRef strErrMsg As String) As Boolean
        Dim strTargetVal As New Dictionary(Of String, String)

        '加盟店=未入力時、スルー
        If strKameitenCd = String.Empty Then
            Return True
        End If

        'チェック対象の調査概要
        strTargetVal.Add("63", "63")
        strTargetVal.Add("65", "65")

        '調査概要
        If strTargetVal.ContainsKey(strTysGaiyou) Then
            '注意事項フラグ
            If strExistFlg = Boolean.FalseString Then
                strErrMsg = Messages.MSG146E.Replace("@PARAM1", "加盟店").Replace("@PARAM2", "地盤診断支払代行不可")
                Return False
            End If
        End If

        Return True

    End Function
#End Region

#Region "同時依頼棟数の自動設定"
    ''' <summary>
    ''' 同時依頼棟数の自動設定
    ''' </summary>
    ''' <param name="intDoujiIraiTousuu">同時依頼棟数</param>
    ''' <returns>自動設定後_同時依頼棟数</returns>
    ''' <remarks>intDoujiIraiTousuu = Integer.MinValue or 0 or 1の場合、1を返却する</remarks>
    Public Function SetAutoDoujiIraiTousuu(ByVal intDoujiIraiTousuu As Integer) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetAutoDoujiIraiTousuu" _
                                                    , intDoujiIraiTousuu)

        Dim intRet As Integer = 1

        If intDoujiIraiTousuu = Integer.MinValue OrElse intDoujiIraiTousuu = 0 OrElse intDoujiIraiTousuu = 1 Then
            intRet = 1
        Else
            intRet = intDoujiIraiTousuu
        End If

        Return intRet
    End Function
#End Region

#Region "保証書DB格納先管理レコードを取得する"
    ''' <summary>
    ''' 保証書DB格納先管理レコードを取得する
    ''' </summary>
    ''' <param name="kubun"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoDbLinkPath(ByVal kubun As String, ByVal ym As String) As HosyousyoDbRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoDbLinkPath", kubun, ym)

        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '保証書DBレコード
        Dim recResult As New HosyousyoDbRecord
        '保証書データアクセスクラス
        Dim daHosyousyoDb As New HosyousyoDbDataAccess

        '保証書DB格納先管理マスタを検索
        If Not String.IsNullOrEmpty(kubun) AndAlso Not String.IsNullOrEmpty(ym) Then
            dTblResult = daHosyousyoDb.GetHosyousyoDbInfo(kubun, ym)
        End If

        If dTblResult.Rows.Count > 0 Then
            '検索結果をレコードにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of HosyousyoDbRecord)(GetType(HosyousyoDbRecord), dTblResult)(0)
        End If

        Return recResult
    End Function
#End Region

#Region "工事商品価格設定対応"

    ''' <summary>
    ''' 工事価格マスタのKEY情報をレコードクラスに設定して返却する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strKojKaisyaCd">工事会社コード+工事会社事業所コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKojKkkMstKeyRec(ByVal strKameitenCd As String _
                                        , ByVal strEigyousyoCd As String _
                                        , ByVal strKeiretuCd As String _
                                        , ByVal strSyouhinCd As String _
                                        , ByVal strKojKaisyaCd As String _
                                        ) As KoujiKakakuKeyRecord

        Dim keyRec As New KoujiKakakuKeyRecord
        With keyRec
            '加盟店コード
            .KameitenCd = strKameitenCd
            '営業所コード
            .EigyousyoCd = strEigyousyoCd
            '系列コード
            .KeiretuCd = strKeiretuCd
            '商品コード
            .SyouhinCd = strSyouhinCd
            '工事会社コード
            .KojGaisyaCd = strKojKaisyaCd
        End With
        Return keyRec
    End Function

#End Region

#Region "特別対応価格対応"

#Region "価格反映処理"

    ''' <summary>
    ''' 価格反映処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ttList">特別対応レコードのリスト</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="blnSyouhin1Henkou">商品1変更フラグ(True:変更あり,False:変更なし)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' ※引き渡すパラメータ
    ''' ・特別対応データ
    ''' 更新フラグ、価格処理フラグ
    ''' ・邸別請求
    ''' データ分類コード、画面表示NO、商品情報(商品コード、売上計上FLG、発注書金額)
    ''' </remarks>
    Public Function pf_ChkTokubetuTaiouKkk(ByVal sender As Object _
                                            , ByRef ttList As List(Of TokubetuTaiouRecordBase) _
                                            , ByRef jibanRec As JibanRecordBase _
                                            , ByVal blnSyouhin1Henkou As Boolean _
                                            ) As EarthEnum.emKingakuAction
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".pf_ChkTokubetuTaiouKkk" _
                                                    , sender _
                                                    , ttList _
                                                    , jibanRec _
                                                    , sender _
                                                    , blnSyouhin1Henkou)

        '作業用
        Dim intBunrui As EarthEnum.emTeibetuBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN_NOTHING '邸別請求/分類コード
        Dim intGamenHyoujiNo As EarthEnum.emGamenHyoujiNo = EarthEnum.emGamenHyoujiNo.HYOUJI_NOTHING '邸別請求/画面表示NO
        Dim intAct As EarthEnum.emTeibetuAction = EarthEnum.emTeibetuAction.TEIBETU_NOTHING '邸別請求/アクション(追加、更新、削除)
        Dim intKingaku As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION '金額/アクション(増額、減額)

        'Old値用(マスタ再取得により差異が発生する)
        Dim intBunruiOld As EarthEnum.emTeibetuBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN_NOTHING
        Dim intGamenHyoujiNoOld As EarthEnum.emGamenHyoujiNo = EarthEnum.emGamenHyoujiNo.HYOUJI_NOTHING
        Dim intActOld As EarthEnum.emTeibetuAction = EarthEnum.emTeibetuAction.TEIBETU_NOTHING
        Dim intKingakuOld As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION

        Dim intCnt As Integer = 0
        Dim intTmpTokubetuTaiouCd As Integer = Integer.MinValue '特別対応コード
        Dim strTmpJibanSyouhin1Cd As String = String.Empty '商品コード1
        Dim strTmpSoukoCd As String = String.Empty '倉庫コード

        Dim strTmpKasanSyouhinCd As String = String.Empty '金額加算商品コード
        Dim strTmpBunruiCd As String = String.Empty '分類コード
        Dim intTmpGamenHyoujiNo As Integer = Integer.MinValue '画面表示NO
        Dim intTmpKoumutenGaku As Integer = Integer.MinValue '工務店請求加算金額
        Dim intTmpJituGaku As Integer = Integer.MinValue '実請求加算金額

        pStrErrTokubetuTaiouCds = String.Empty 'エラーメッセージ用(特別対応コードを$$$区切りで格納)

        If Not ttList Is Nothing _
            AndAlso Not jibanRec Is Nothing _
                AndAlso Not jibanRec.Syouhin1Record Is Nothing Then

            '商品コード1
            strTmpJibanSyouhin1Cd = jibanRec.Syouhin1Record.SyouhinCd
            '倉庫コード
            strTmpSoukoCd = Me.pf_getBunruiCd(strTmpJibanSyouhin1Cd)

            '特別対応レコードのリストをベースにループ処理
            For intCnt = 0 To ttList.Count - 1
                intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN_NOTHING
                intGamenHyoujiNo = EarthEnum.emGamenHyoujiNo.HYOUJI_NOTHING
                intAct = EarthEnum.emTeibetuAction.TEIBETU_NOTHING
                intKingaku = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION

                intBunruiOld = EarthEnum.emTeibetuBunrui.SYOUHIN_NOTHING
                intGamenHyoujiNoOld = EarthEnum.emGamenHyoujiNo.HYOUJI_NOTHING
                intActOld = EarthEnum.emTeibetuAction.TEIBETU_NOTHING
                intKingakuOld = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION

                '退避
                strTmpKasanSyouhinCd = ttList(intCnt).KasanSyouhinCd
                strTmpBunruiCd = ttList(intCnt).BunruiCd
                intTmpGamenHyoujiNo = ttList(intCnt).GamenHyoujiNo
                intTmpKoumutenGaku = ttList(intCnt).KoumutenKasanGaku
                intTmpJituGaku = ttList(intCnt).UriKasanGaku

                '特別対応コード(特別対応マスタ)
                intTmpTokubetuTaiouCd = ttList(intCnt).TokubetuTaiouCd

                '*********************************************************
                '* 金額アクション判定処理
                '*********************************************************
                '更新フラグ=1でかつ、価格処理フラグ<>1のデータが対象
                If ttList(intCnt).UpdFlg = 1 AndAlso ttList(intCnt).KkkSyoriFlg <> 1 Then
                    '【増額処理】
                    '金額加算商品コードが設定されている場合
                    If ttList(intCnt).KasanSyouhinCd <> String.Empty Then

                        '●特例処理(商品1):特別対応の価格反映先が商品1であった場合、価格反映し直す
                        If blnSyouhin1Henkou Then
                            '設定先が商品1の場合
                            If ttList(intCnt).SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_1 Then
                                '特別対応T.金額加算商品コードと商品1ドロップダウンリストに差異がある場合
                                If ttList(intCnt).KasanSyouhinCd <> strTmpJibanSyouhin1Cd Then
                                    If ttList(intCnt).BunruiCd = String.Empty AndAlso (ttList(intCnt).GamenHyoujiNo = 0 OrElse ttList(intCnt).GamenHyoujiNo = Integer.MinValue) Then
                                        '金額加算商品コードを上書き
                                        ttList(intCnt).KasanSyouhinCd = strTmpJibanSyouhin1Cd

                                        '特別対応データが有効な場合
                                        If ttList(intCnt).Torikesi = 0 Then
                                            '設定先をクリア
                                            ttList(intCnt).BunruiCd = String.Empty
                                            ttList(intCnt).GamenHyoujiNo = 0
                                        Else
                                            '商品1変更時は、商品1にかかる特別対応を全て更新対象とする
                                            ttList(intCnt).ChgVal = True
                                        End If

                                    End If

                                End If
                            End If
                        End If

                        If ttList(intCnt).Torikesi = 0 Then
                            '【増額処理】
                            If ttList(intCnt).BunruiCd = String.Empty AndAlso (ttList(intCnt).GamenHyoujiNo = 0 OrElse ttList(intCnt).GamenHyoujiNo = Integer.MinValue) Then
                                intKingaku = EarthEnum.emKingakuAction.KINGAKU_PLUS

                            ElseIf ttList(intCnt).BunruiCd <> String.Empty AndAlso ttList(intCnt).GamenHyoujiNo > 0 Then
                                '【減額/増額処理】
                                '※金額加算商品コード、工務店請求加算金額、実請求加算金額に差異がある場合
                                If Me.pf_ChkMinusPlusKkkChg(ttList(intCnt)) Then
                                    intKingaku = EarthEnum.emKingakuAction.KINGAKU_MINUS_PLUS
                                End If
                            End If

                        Else '【減額処理】
                            If ttList(intCnt).BunruiCd <> String.Empty AndAlso ttList(intCnt).GamenHyoujiNo > 0 Then
                                '金額加算商品コードOld=設定済の場合
                                If ttList(intCnt).KasanSyouhinCdOld <> String.Empty Then
                                    intKingaku = EarthEnum.emKingakuAction.KINGAKU_MINUS
                                End If
                            End If
                        End If

                    ElseIf ttList(intCnt).KasanSyouhinCdOld <> String.Empty Then '金額加算商品コードOld=設定済の場合
                        '【減額処理】
                        If ttList(intCnt).BunruiCd <> String.Empty AndAlso ttList(intCnt).GamenHyoujiNo > 0 Then
                            intKingaku = EarthEnum.emKingakuAction.KINGAKU_MINUS
                        End If
                    End If
                End If

                If intKingaku > EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION Then
                    '*********************************************************
                    '* 価格特例処理
                    '*********************************************************
                    Select Case intKingaku
                        Case EarthEnum.emKingakuAction.KINGAKU_MINUS_PLUS '減額増額処理
                            '●マスタ再取得処理により、マスタとトラン値で差異がある場合でかつ増額処理を行なう場合は、先に減額処理を行なっておく
                            '減額(アクションを変更)
                            intKingakuOld = EarthEnum.emKingakuAction.KINGAKU_MINUS

                            intBunruiOld = EarthEnum.emTeibetuBunrui.SYOUHIN_NOTHING
                            intGamenHyoujiNoOld = EarthEnum.emGamenHyoujiNo.HYOUJI_NOTHING
                            intActOld = EarthEnum.emTeibetuAction.TEIBETU_NOTHING

                            'Old値で計算
                            ttList(intCnt).KasanSyouhinCd = ttList(intCnt).KasanSyouhinCdOld
                            ttList(intCnt).KoumutenKasanGaku = ttList(intCnt).KoumutenKasanGakuOld
                            ttList(intCnt).UriKasanGaku = ttList(intCnt).UriKasanGakuOld

                            '邸別請求レコードへの設定先および処理の判定を行なう
                            If Me.pf_getSetteiSakiInfo(ttList, intCnt, jibanRec, intBunruiOld, intGamenHyoujiNoOld, intActOld, intKingakuOld, False) = False Then
                                Dim recResult As New TokubetuTaiouRecordBase    'レコードクラス
                                Dim ttLogic As New TokubetuTaiouLogic
                                Dim total_count As Integer = 0 '取得件数

                                recResult = ttLogic.GetTokubetuTaiouDataRec(sender, ttList(intCnt).Kbn, ttList(intCnt).HosyousyoNo, ttList(intCnt).TokubetuTaiouCd, total_count)
                                If total_count = -1 Then
                                    ' 検索結果件数が-1の場合、エラーだが処理は続ける
                                    Continue For
                                End If

                                '画面.金額加算商品コードと特別対応T.金額加算商品コードが一致している場合
                                If strTmpKasanSyouhinCd = recResult.KasanSyouhinCd Then
                                    ttList(intCnt).ChgVal = True
                                End If

                                '減額できなかった場合、増額しない
                                Continue For

                            Else
                                '商品および金額設定処理
                                If Me.pf_setTokubetuTaiouKkk(sender, ttList(intCnt), jibanRec, intBunruiOld, intGamenHyoujiNoOld, intActOld, intKingakuOld) > EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR Then
                                    '▲価格反映対象であるのに価格反映されなかった特別対応レコード
                                    intKingaku = EarthEnum.emKingakuAction.KINGAKU_ALERT
                                    pStrErrTokubetuTaiouCds &= ttList(intCnt).TokubetuTaiouMeisyou & EarthConst.CRLF_CODE
                                    Continue For
                                End If
                            End If

                            '増額(アクションを変更)
                            intKingaku = EarthEnum.emKingakuAction.KINGAKU_PLUS

                            '金額加算商品コードを復元
                            ttList(intCnt).KasanSyouhinCd = strTmpKasanSyouhinCd
                            '●金額加算商品コードが異なる場合、設定先をクリア
                            If ttList(intCnt).KasanSyouhinCdOld <> strTmpKasanSyouhinCd Then
                                ttList(intCnt).BunruiCd = String.Empty
                                ttList(intCnt).GamenHyoujiNo = 0
                            End If
                            '金額を復元
                            ttList(intCnt).KoumutenKasanGaku = intTmpKoumutenGaku
                            ttList(intCnt).UriKasanGaku = intTmpJituGaku

                        Case EarthEnum.emKingakuAction.KINGAKU_MINUS '減額処理のみ

                            'Old値で計算
                            ttList(intCnt).KasanSyouhinCd = ttList(intCnt).KasanSyouhinCdOld
                            ttList(intCnt).KoumutenKasanGaku = ttList(intCnt).KoumutenKasanGakuOld
                            ttList(intCnt).UriKasanGaku = ttList(intCnt).UriKasanGakuOld

                    End Select

                    '*********************************************************
                    '* 設定先・振分算出処理
                    '*********************************************************
                    '邸別請求レコードへの設定先および処理の判定を行なう
                    Me.pf_getSetteiSakiInfo(ttList, intCnt, jibanRec, intBunrui, intGamenHyoujiNo, intAct, intKingaku)

                    '商品および金額設定処理
                    If Me.pf_setTokubetuTaiouKkk(sender, ttList(intCnt), jibanRec, intBunrui, intGamenHyoujiNo, intAct, intKingaku) > EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR Then
                        '▲価格反映対象であるのに価格反映されなかった特別対応レコード
                        intKingaku = EarthEnum.emKingakuAction.KINGAKU_ALERT
                        pStrErrTokubetuTaiouCds &= ttList(intCnt).TokubetuTaiouMeisyou & EarthConst.CRLF_CODE
                    End If

                    '補正したので元に戻す
                    Select Case intKingaku
                        Case EarthEnum.emKingakuAction.KINGAKU_MINUS '減額処理のみ
                            'Old値で計算
                            ttList(intCnt).KasanSyouhinCd = strTmpKasanSyouhinCd
                            ttList(intCnt).KoumutenKasanGaku = intTmpKoumutenGaku
                            ttList(intCnt).UriKasanGaku = intTmpJituGaku
                    End Select

                End If
            Next
        End If

        '価格反映されなかった特別対応コード群が存在する場合、エラーMSG表示
        If pStrErrTokubetuTaiouCds <> String.Empty Then
            intKingaku = EarthEnum.emKingakuAction.KINGAKU_ALERT
        End If

        Return intKingaku
    End Function

#End Region

#Region "設定先振分処理"

    ''' <summary>
    ''' 商品コードより分類コードを取得する
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_getBunruiCd(ByVal strSyouhinCd As String) As String
        '●特別対応レコードより、倉庫コードを取得する
        'KEY:金額加算商品コード
        Dim strSoukoCd As String = String.Empty
        Dim arrSyouhin123 As String() = {EarthConst.SOUKO_CD_SYOUHIN_1, EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115, EarthConst.SOUKO_CD_SYOUHIN_3}

        '**********************************************************************
        ' 商品の詳細情報を取得します
        '**********************************************************************
        Dim syouhinAccess As New SyouhinDataAccess
        Dim tmpHin1AutoSetRecord As New Syouhin1AutoSetRecord
        Dim drList As List(Of Syouhin1AutoSetRecord)
        Dim dtResult As DataTable

        ' 商品情報を取得
        dtResult = syouhinAccess.GetSyouhinInfo(strSyouhinCd, True)
        'Listに格納
        drList = DataMappingHelper.Instance.getMapArray(Of Syouhin1AutoSetRecord)(GetType(Syouhin1AutoSetRecord), dtResult)
        ' 取得した情報をチェック(取得できない場合、処理終了)
        If drList.Count <= 0 Then
            Return strSoukoCd
        Else
            tmpHin1AutoSetRecord = drList(0)
            strSoukoCd = tmpHin1AutoSetRecord.SoukoCd
        End If

        '分類コードチェック
        Dim blnChkBunrui As Boolean = False

        If strSoukoCd = String.Empty Then
        Else
            For Each strTmpCd As String In arrSyouhin123
                If strTmpCd = strSoukoCd Then
                    blnChkBunrui = True
                    Exit For
                End If
            Next
        End If
        If blnChkBunrui = False Then
            Return strSoukoCd
        End If

        Return strSoukoCd
    End Function

    ''' <summary>
    ''' 金額加算商品コードより、設定先を決定する
    ''' </summary>
    ''' <param name="ttList">特別対応レコードのリスト</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="intBunrui">設定先(分類コード)</param>
    ''' <param name="intGamenHyoujiNo">設定先(画面表示NO)</param>
    ''' <param name="intAct">設定先(アクション)</param>
    ''' <remarks></remarks>
    Public Function pf_getSetteiSakiInfo( _
                                ByRef ttList As List(Of TokubetuTaiouRecordBase) _
                                , ByRef intCnt As Integer _
                                , ByRef jibanRec As JibanRecordBase _
                                , ByRef intBunrui As EarthEnum.emTeibetuBunrui _
                                , ByRef intGamenHyoujiNo As EarthEnum.emGamenHyoujiNo _
                                , ByRef intAct As EarthEnum.emTeibetuAction _
                                , ByVal intKingaku As EarthEnum.emKingakuAction _
                                , Optional ByVal blnSyouhin3SetFlgOk As Boolean = True _
                                ) As Boolean
        Dim blnRet As Boolean = False

        '●特別対応コード毎の設定先に基づき、邸別請求情報に特別対応情報をセットする
        Dim blnSyouhin3SetFlg As Boolean  '商品3へのセット判断フラグ
        Dim blnSyouhinGassanFlg As Boolean  '合算フラグ
        Dim blnSyouhinAdd As Boolean  '商品行追加フラグ

        '初期化
        blnSyouhin3SetFlg = False
        blnSyouhinGassanFlg = False
        blnSyouhinAdd = False

        '設定先を算出する
        If Me.pf_setTeibetuSetteiJyky(ttList, intCnt, jibanRec, blnSyouhin3SetFlg, blnSyouhinGassanFlg, blnSyouhinAdd, intBunrui, intGamenHyoujiNo, intAct, intKingaku) = False Then
            '商品3振分OKフラグ=Trueの場合
            If blnSyouhin3SetFlgOk Then
                '上記までで算出できなかった特別対応レコードを商品3へ振分(固定商品コード)
                'デフォルトの設定先が商品3以外への設定で、上記まで商品3に振分判定された場合は再度振分を行なう
                If blnSyouhin3SetFlg = False AndAlso blnSyouhinGassanFlg = False AndAlso blnSyouhinAdd = False Then
                    If ttList(intCnt).KasanSyouhinCd <> String.Empty Then
                        '特別対応コード(80～99)の場合、商品3への再振分はしない
                        If Me.pf_ChkFuriwakeTokubetuTaiou80To99(ttList(intCnt).TokubetuTaiouCd) = False Then
                            '倉庫コード=115は再振分しない
                            Select Case ttList(intCnt).SoukoCd
                                Case EarthConst.SOUKO_CD_SYOUHIN_2_115
                                    pStrErrTokubetuTaiouCds &= ttList(intCnt).TokubetuTaiouMeisyou & EarthConst.CRLF_CODE
                                    Exit Function
                            End Select

                            '初期化
                            blnSyouhin3SetFlg = False
                            blnSyouhinGassanFlg = False
                            blnSyouhinAdd = False

                            ttList(intCnt).KasanSyouhinCd = EarthConst.SH_CD_TOKUBETU_TAIOU '●固定商品コード

                            If Me.pf_setTeibetuSetteiJyky(ttList, intCnt, jibanRec, blnSyouhin3SetFlg, blnSyouhinGassanFlg, blnSyouhinAdd, intBunrui, intGamenHyoujiNo, intAct, intKingaku) = False Then
                                pStrErrTokubetuTaiouCds &= ttList(intCnt).TokubetuTaiouMeisyou & EarthConst.CRLF_CODE
                            Else
                                blnRet = True
                            End If

                        End If
                    End If
                End If
            End If
        Else
            blnRet = True
        End If

        Return blnRet
    End Function

    ''' <summary>
    ''' 邸別請求レコードの設定状況により、特別対応の設定先を判断する
    ''' </summary>
    ''' <param name="ttList">特別対応レコードのリスト</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="blnSyouhin3SetFlg">商品3設定フラグ</param>
    ''' <param name="blnSyouhinGassanFlg">合算フラグ</param>
    ''' <param name="blnSyouhinAdd">追加フラグ</param>
    ''' <param name="intBunrui">設定先(分類コード)</param>
    ''' <param name="intGamenHyoujiNo">設定先(画面表示NO)</param>
    ''' <param name="intAct">設定先(アクション)</param>
    ''' <remarks></remarks>
    Public Function pf_setTeibetuSetteiJyky( _
                                    ByRef ttList As List(Of TokubetuTaiouRecordBase) _
                                    , ByRef intCnt As Integer _
                                    , ByRef jibanRec As JibanRecordBase _
                                    , ByRef blnSyouhin3SetFlg As Boolean _
                                    , ByRef blnSyouhinGassanFlg As Boolean _
                                    , ByRef blnSyouhinAdd As Boolean _
                                    , ByRef intBunrui As EarthEnum.emTeibetuBunrui _
                                    , ByRef intGamenHyoujiNo As EarthEnum.emGamenHyoujiNo _
                                    , ByRef intAct As EarthEnum.emTeibetuAction _
                                    , ByVal intKingaku As EarthEnum.emKingakuAction _
                                    ) As Boolean

        '初期化
        Dim blnRet As Boolean = False
        Dim blnSyouhinBlank As Boolean = False
        Dim intTmpCnt As Integer = 1
        Dim intTmpBlankNo As Integer = 0

        Dim strTmpSyouhinCd As String = String.Empty
        Dim intTmpKeijouFlg As Integer = 0
        Dim intMax As Integer = 0
        Dim TeibetuTmpRecords As Dictionary(Of Integer, TeibetuSeikyuuRecord)

        Dim blnTokurei8099 As Boolean = False '振分ルール判定用フラグ(80～99は個別)

        Dim strSoukoCd As String = String.Empty  '倉庫コード
        Dim strKasanSyouhinCd As String = String.Empty '金額加算商品コード
        Dim intTokubetuTaiouCd As Integer = Integer.MinValue '特別対応コード

        intTokubetuTaiouCd = ttList(intCnt).TokubetuTaiouCd '特別対応コード

        strKasanSyouhinCd = ttList(intCnt).KasanSyouhinCd '金額加算商品コード
        strSoukoCd = Me.pf_getBunruiCd(strKasanSyouhinCd) '倉庫コード
        ttList(intCnt).SoukoCd = strSoukoCd

        Dim strTtBunruiCd As String = ttList(intCnt).BunruiCd '特別対応T.分類コード
        Dim intTtGamenHyoujiNo As Integer = ttList(intCnt).GamenHyoujiNo  '特別対応T.画面表示NO

        With jibanRec
            Select Case strSoukoCd
                '商品1～3
                Case EarthConst.SOUKO_CD_SYOUHIN_1, EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115, EarthConst.SOUKO_CD_SYOUHIN_3
                    '初期化
                    TeibetuTmpRecords = New Dictionary(Of Integer, TeibetuSeikyuuRecord)

                    blnSyouhin3SetFlg = False
                    blnSyouhinGassanFlg = False
                    blnSyouhinBlank = False
                    blnSyouhinAdd = False

                    '増額時
                    If intKingaku = EarthEnum.emKingakuAction.KINGAKU_PLUS Then
                        '倉庫コードから設定先を判断する
                        Select Case strSoukoCd
                            Case EarthConst.SOUKO_CD_SYOUHIN_1 '商品1
                                TeibetuTmpRecords.Add(1, .Syouhin1Record)
                                intMax = EarthConst.SYOUHIN1_COUNT
                            Case EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115  '商品2
                                TeibetuTmpRecords = .Syouhin2Records
                                intMax = EarthConst.SYOUHIN2_COUNT
                            Case EarthConst.SOUKO_CD_SYOUHIN_3 '商品3
                                TeibetuTmpRecords = .Syouhin3Records
                                intMax = EarthConst.SYOUHIN3_COUNT
                            Case Else
                                blnRet = False
                                pStrErrTokubetuTaiouCds &= ttList(intCnt).TokubetuTaiouMeisyou & EarthConst.CRLF_CODE
                                Exit Function
                        End Select

                    Else '減額時

                        '倉庫コードを上書き
                        strSoukoCd = strTtBunruiCd
                        ttList(intCnt).SoukoCd = strSoukoCd

                        '特別対応T.分類コードから設定先を判断する
                        Select Case strTtBunruiCd
                            Case EarthConst.SOUKO_CD_SYOUHIN_1 '商品1
                                TeibetuTmpRecords.Add(1, .Syouhin1Record)
                                intMax = EarthConst.SYOUHIN1_COUNT
                            Case EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115  '商品2
                                TeibetuTmpRecords = .Syouhin2Records
                                intMax = EarthConst.SYOUHIN2_COUNT
                            Case EarthConst.SOUKO_CD_SYOUHIN_3 '商品3
                                TeibetuTmpRecords = .Syouhin3Records
                                intMax = EarthConst.SYOUHIN3_COUNT
                            Case Else
                                blnRet = False
                                pStrErrTokubetuTaiouCds &= ttList(intCnt).TokubetuTaiouMeisyou & EarthConst.CRLF_CODE
                                Exit Function
                        End Select
                    End If

                    '特別対応コード(80～99)の場合、金額加算商品コードは商品3でなければエラー
                    If Me.pf_ChkFuriwakeTokubetuTaiou80To99(intTokubetuTaiouCd) Then
                        If strSoukoCd <> EarthConst.SOUKO_CD_SYOUHIN_3 Then
                            blnRet = False
                            pStrErrTokubetuTaiouCds &= ttList(intCnt).TokubetuTaiouMeisyou & EarthConst.CRLF_CODE
                            Exit Function
                        End If
                    End If

                    '加算商品コード=入力ありの場合
                    If strKasanSyouhinCd <> String.Empty Then
                        '邸別請求レコードをベースにループ
                        For intTmpCnt = 1 To intMax
                            '減額時、設定先まで処理を飛ばす
                            If intKingaku = EarthEnum.emKingakuAction.KINGAKU_MINUS Then
                                If intTmpCnt = intTtGamenHyoujiNo Then
                                Else
                                    Continue For
                                End If
                            End If

                            '初期化
                            strTmpSyouhinCd = String.Empty
                            intTmpKeijouFlg = 0

                            blnTokurei8099 = False

                            '作業用
                            If Not TeibetuTmpRecords Is Nothing AndAlso TeibetuTmpRecords.ContainsKey(intTmpCnt) Then
                                strTmpSyouhinCd = TeibetuTmpRecords(intTmpCnt).SyouhinCd
                                intTmpKeijouFlg = TeibetuTmpRecords(intTmpCnt).UriKeijyouFlg
                            End If

                            '空白行の存在有無チェック
                            If strTmpSyouhinCd = String.Empty Then
                                If blnSyouhinBlank = False Then
                                    blnSyouhinBlank = True
                                    intTmpBlankNo = intTmpCnt '退避
                                End If
                            ElseIf intKingaku = EarthEnum.emKingakuAction.KINGAKU_MINUS OrElse _
                                (intKingaku = EarthEnum.emKingakuAction.KINGAKU_PLUS AndAlso strTmpSyouhinCd = strKasanSyouhinCd) Then '商品コード=加算商品コード

                                '対象の特別対応コードが80～99の場合
                                If Me.pf_ChkFuriwakeTokubetuTaiou80To99(intTokubetuTaiouCd) Then

                                    '●特別対応のリストをベースにループ処理
                                    For intListCnt As Integer = 0 To ttList.Count - 1
                                        '邸別請求レコードに対して、どの特別対応レコードが関連付けされているかをチェック
                                        If ttList(intListCnt).BunruiCd = strSoukoCd AndAlso ttList(intListCnt).GamenHyoujiNo = intTmpCnt Then
                                            '対象の特別対応コードが80～99の場合、でかつ対象の特別対応コードではない場合、合算してはいけない
                                            If Me.pf_ChkFuriwakeTokubetuTaiou80To99(ttList(intListCnt).TokubetuTaiouCd) AndAlso ttList(intListCnt).TokubetuTaiouCd <> intTokubetuTaiouCd Then
                                                blnTokurei8099 = True
                                            End If
                                            Exit For
                                        End If
                                    Next

                                    If blnTokurei8099 = False Then
                                        '設定先が未設定の場合、追加。設定済の場合、更新。
                                        If ttList(intCnt).BunruiCd = String.Empty AndAlso ttList(intCnt).GamenHyoujiNo = 0 Then
                                        Else
                                            '設定先が一致する場合
                                            If ttList(intCnt).BunruiCd = strSoukoCd AndAlso ttList(intCnt).GamenHyoujiNo = intTmpCnt Then
                                                '計上済
                                                If intTmpKeijouFlg = 1 Then
                                                    blnRet = False
                                                    pStrErrTokubetuTaiouCds &= ttList(intCnt).TokubetuTaiouMeisyou & EarthConst.CRLF_CODE
                                                    Exit Function
                                                Else
                                                    blnSyouhinGassanFlg = True
                                                    Exit For
                                                End If
                                            End If
                                        End If
                                    End If

                                Else '特別対応コード=80～99には合算したりしないようにする
                                    '●特別対応のリストをベースにループ処理
                                    For intListCnt As Integer = 0 To ttList.Count - 1
                                        '邸別請求レコードに対して、どの特別対応レコードが関連付けされているかをチェック
                                        If ttList(intListCnt).BunruiCd = strSoukoCd AndAlso ttList(intListCnt).GamenHyoujiNo = intTmpCnt Then
                                            '対象の特別対応コードが80～99の場合、合算してはいけない
                                            If Me.pf_ChkFuriwakeTokubetuTaiou80To99(ttList(intListCnt).TokubetuTaiouCd) Then
                                                blnTokurei8099 = True
                                            End If
                                            Exit For
                                        End If
                                    Next

                                    If blnTokurei8099 = False Then
                                        '計上済
                                        If intTmpKeijouFlg = 1 Then
                                        Else
                                            blnSyouhinGassanFlg = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            End If

                        Next
                    End If

                    'どこにも設定されない場合
                    If blnSyouhinGassanFlg Then
                    ElseIf blnSyouhin3SetFlg Then
                    ElseIf blnSyouhinBlank Then
                        '空白行がある場合に追加
                        blnSyouhinAdd = True
                    Else
                        '●特別対応による自動設定不可MSG表示
                        blnRet = False
                        Exit Function
                    End If

            End Select

        End With

        '邸別請求/アクションを取得
        intAct = pf_setTeibetuAction(blnSyouhin3SetFlg, blnSyouhinGassanFlg, blnSyouhinAdd)

        '邸別請求/分類を取得
        intBunrui = pf_setTeibetuBunrui(blnSyouhin3SetFlg, blnSyouhinGassanFlg, blnSyouhinAdd, strSoukoCd)

        '邸別請求/画面表示NOを取得
        Select Case intAct
            Case EarthEnum.emTeibetuAction.TEIBETU_ADD
                intGamenHyoujiNo = intTmpBlankNo '空白行番号をセット
            Case Else
                intGamenHyoujiNo = intTmpCnt
        End Select

        Return True
    End Function

    ''' <summary>
    ''' 邸別請求データへのアクションを判定する
    ''' </summary>
    ''' <param name="blnSyouhin3SetFlg">商品3設定フラグ</param>
    ''' <param name="blnSyouhinGassanFlg">合算フラグ</param>
    ''' <param name="blnSyouhinAdd">追加フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_setTeibetuAction(ByRef blnSyouhin3SetFlg As Boolean _
                                    , ByRef blnSyouhinGassanFlg As Boolean _
                                    , ByRef blnSyouhinAdd As Boolean _
                                    ) As EarthEnum.emTeibetuAction

        Dim intAct As EarthEnum.emTeibetuAction = EarthEnum.emTeibetuAction.TEIBETU_NOTHING

        'アクション判定
        If blnSyouhin3SetFlg Then
            intAct = EarthEnum.emTeibetuAction.TEIBETU_SYOUHIN_3_SET
        ElseIf blnSyouhinGassanFlg Then
            intAct = EarthEnum.emTeibetuAction.TEIBETU_UPD
        ElseIf blnSyouhinAdd Then
            intAct = EarthEnum.emTeibetuAction.TEIBETU_ADD
        Else
            intAct = EarthEnum.emTeibetuAction.TEIBETU_NOTHING
        End If

        Return intAct
    End Function

    ''' <summary>
    ''' 邸別請求データの分類を判定する
    ''' </summary>
    ''' <param name="blnSyouhin3SetFlg">商品3設定フラグ</param>
    ''' <param name="blnSyouhinGassanFlg">合算フラグ</param>
    ''' <param name="blnSyouhinAdd">追加フラグ</param>
    ''' <param name="strSoukoCd">倉庫コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_setTeibetuBunrui(ByRef blnSyouhin3SetFlg As Boolean _
                                , ByRef blnSyouhinGassanFlg As Boolean _
                                , ByRef blnSyouhinAdd As Boolean _
                                , ByVal strSoukoCd As String _
                                ) As EarthEnum.emTeibetuBunrui

        Dim intBunrui As EarthEnum.emTeibetuBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN_NOTHING

        If blnSyouhin3SetFlg Then
            intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN3

        ElseIf blnSyouhinGassanFlg Then
            Select Case strSoukoCd
                Case EarthConst.SOUKO_CD_SYOUHIN_1 '商品1
                    intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN1
                Case EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115 '商品2
                    intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN2
                Case EarthConst.SOUKO_CD_SYOUHIN_3 '商品3
                    intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN3
            End Select

        ElseIf blnSyouhinAdd Then
            Select Case strSoukoCd
                Case EarthConst.SOUKO_CD_SYOUHIN_1 '商品1
                    intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN1
                Case EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115 '商品2
                    intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN2
                Case EarthConst.SOUKO_CD_SYOUHIN_3 '商品3
                    intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN3
            End Select
        End If

        Return intBunrui
    End Function

    ''' <summary>
    ''' 特別対応レコードをもとに、邸別請求レコードへ反映する。
    ''' 紐付け処理を行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ttRec">特別対応レコード</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="intBunrui">設定先(分類コード)</param>
    ''' <param name="intGamenHyoujiNo">設定先(画面表示NO)</param>
    ''' <param name="intAct">設定先(アクション)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_setTokubetuTaiouKkk(ByVal sender As Object _
                                        , ByRef ttRec As TokubetuTaiouRecordBase _
                                        , ByRef jibanRec As JibanRecordBase _
                                        , ByVal intBunrui As EarthEnum.emTeibetuBunrui _
                                        , ByVal intGamenHyoujiNo As EarthEnum.emGamenHyoujiNo _
                                        , ByVal intAct As EarthEnum.emTeibetuAction _
                                        , ByVal intKingaku As EarthEnum.emKingakuAction _
                                        ) As EarthEnum.emKkkHanneiErr

        Dim strKasanSyouhinCd As String = ttRec.KasanSyouhinCd '金額加算商品コード
        Dim strSoukoCd As String = Me.pf_getBunruiCd(strKasanSyouhinCd) '倉庫コード

        Dim intCnt As Integer = 1
        Dim msLogic As New MousikomiSearchLogic
        Dim jLogic As New JibanLogic

        '作業用
        Dim strTmpSyouhinCd As String = String.Empty '商品コード
        Dim intTmpUriKeijouFlg As Integer = 0 '売上計上FLG
        Dim intTmpUriGaku As Integer = 0 '実請求金額
        Dim intTmpKoumutenGaku As Integer = 0 '工務店請求金額
        Dim intRet As EarthEnum.emKkkHanneiErr = EarthEnum.emKkkHanneiErr.ERR_OTHER '邸別請求の価格反映結果

        With jibanRec
            Select Case intAct
                Case EarthEnum.emTeibetuAction.TEIBETU_ADD '●追加
                    If intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN1 Then '商品1
                        '商品1への追加機能なし
                        intRet = EarthEnum.emKkkHanneiErr.ERR_OTHER
                        Exit Select

                    ElseIf intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN2 Then '商品2

                        '商品２レコードオブジェクトの生成
                        jLogic.CreateSyouhin23Rec(sender, jibanRec)

                        '●特別対応価格対応
                        For intCnt = 1 To EarthConst.SYOUHIN2_COUNT
                            '該当の画面表示NOまで処理をとばす
                            If intGamenHyoujiNo <> intCnt Then
                                Continue For
                            End If

                            strTmpSyouhinCd = String.Empty
                            intTmpUriGaku = 0
                            intTmpKoumutenGaku = 0

                            If Not .Syouhin2Records Is Nothing AndAlso .Syouhin2Records.ContainsKey(intCnt) Then
                                Me.SetDisplayString(.Syouhin2Records(intCnt).SyouhinCd, strTmpSyouhinCd)
                                Me.SetDisplayString(.Syouhin2Records(intCnt).UriKeijyouFlg, intTmpUriKeijouFlg)
                                Me.SetDisplayString(.Syouhin2Records(intCnt).UriGaku, intTmpUriGaku)
                                Me.SetDisplayString(.Syouhin2Records(intCnt).KoumutenSeikyuuGaku, intTmpKoumutenGaku)
                            End If

                            ' 空いている商品行があれば設定する
                            If strTmpSyouhinCd = String.Empty Then
                                '紐付可否のチェック
                                If Me.pf_ChkHimodukeKingaku(ttRec.UriKasanGaku, ttRec.KoumutenKasanGaku) Then
                                    '●商品設定処理
                                    If jLogic.setSyouhinCd23(sender, "2_" & intCnt.ToString, strKasanSyouhinCd, jibanRec) = False Then
                                        intRet = EarthEnum.emKkkHanneiErr.ERR_OTHER '結果
                                        Exit Select
                                    Else
                                        '分類コードの判定
                                        If strSoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_110 Then
                                            '●金額の設定
                                            '実請求金額 = 実請求加算金額
                                            .Syouhin2Records(intCnt).UriGaku = ttRec.UriKasanGaku
                                            '工務店請求金額 = 工務店請求加算金額
                                            .Syouhin2Records(intCnt).KoumutenSeikyuuGaku = ttRec.KoumutenKasanGaku
                                            '※消費税額 = 実請求金額と税率より換算
                                            .Syouhin2Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                            '変更状況
                                            .Syouhin2Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin2Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT)
                                            '価格反映対象確認フラグ
                                            ttRec.ChgVal = True

                                            '●邸別請求との紐付け
                                            ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_110
                                            ttRec.GamenHyoujiNo = intCnt

                                            intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                            Exit For 'セットしたら処理を抜ける

                                        ElseIf strSoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_115 Then

                                            '●金額の設定
                                            '実請求金額 = 実請求加算金額 * -1
                                            .Syouhin2Records(intCnt).UriGaku = ttRec.UriKasanGaku * -1
                                            '工務店請求金額 = 工務店請求加算金額 * -1
                                            .Syouhin2Records(intCnt).KoumutenSeikyuuGaku = ttRec.KoumutenKasanGaku * -1
                                            '※消費税額 = 実請求金額と税率より換算
                                            .Syouhin2Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                            '変更状況
                                            .Syouhin2Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin2Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT)
                                            '価格反映対象確認フラグ
                                            ttRec.ChgVal = True

                                            '●邸別請求との紐付け
                                            ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_115
                                            ttRec.GamenHyoujiNo = intCnt

                                            intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                            Exit For 'セットしたら処理を抜ける
                                        End If
                                    End If
                                Else
                                    intRet = EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR  '結果
                                    Exit Select
                                End If
                            End If
                        Next

                        '請求書発行日、売上年月日の設定実行●該当の邸別請求のみ変更する
                        jLogic.Syouhin1UriageSeikyuDateSet(sender, jibanRec, False, intGamenHyoujiNo)

                        '商品２レコードオブジェクトの破棄
                        jLogic.DeleteSyouhin23Rec(sender, jibanRec)

                    ElseIf intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN3 Then '商品3

                        '商品３レコードオブジェクトの生成
                        jLogic.CreateSyouhin23Rec(sender, jibanRec, True)

                        For intCnt = 1 To EarthConst.SYOUHIN3_COUNT
                            '該当の画面表示NOまで処理をとばす
                            If intGamenHyoujiNo <> intCnt Then
                                Continue For
                            End If

                            strTmpSyouhinCd = String.Empty
                            intTmpUriGaku = 0
                            intTmpKoumutenGaku = 0

                            If Not .Syouhin3Records Is Nothing AndAlso .Syouhin3Records.ContainsKey(intCnt) Then
                                Me.SetDisplayString(.Syouhin3Records(intCnt).SyouhinCd, strTmpSyouhinCd)
                                Me.SetDisplayString(.Syouhin3Records(intCnt).UriKeijyouFlg, intTmpUriKeijouFlg)
                                Me.SetDisplayString(.Syouhin3Records(intCnt).UriGaku, intTmpUriGaku)
                                Me.SetDisplayString(.Syouhin3Records(intCnt).KoumutenSeikyuuGaku, intTmpKoumutenGaku)
                            End If

                            ' 空いている商品行があれば設定する
                            If strTmpSyouhinCd = String.Empty Then
                                '紐付可否のチェック
                                If Me.pf_ChkHimodukeKingaku(ttRec.UriKasanGaku, ttRec.KoumutenKasanGaku) Then
                                    '●商品設定処理
                                    If jLogic.setSyouhinCd23(sender, "3_" & intCnt.ToString, strKasanSyouhinCd, jibanRec) = False Then
                                        intRet = EarthEnum.emKkkHanneiErr.ERR_OTHER '結果
                                        Exit Select
                                    Else
                                        '●金額の設定
                                        '実請求金額 = 実請求加算金額
                                        .Syouhin3Records(intCnt).UriGaku = ttRec.UriKasanGaku
                                        '工務店請求金額 = 工務店請求加算金額
                                        .Syouhin3Records(intCnt).KoumutenSeikyuuGaku = ttRec.KoumutenKasanGaku
                                        '※消費税額 = 実請求金額と税率より換算
                                        .Syouhin3Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                        '変更状況
                                        .Syouhin3Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin3Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT)
                                        '価格反映対象確認フラグ
                                        ttRec.ChgVal = True

                                        '●邸別請求との紐付け
                                        ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3
                                        ttRec.GamenHyoujiNo = intCnt

                                        intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                        Exit For 'セットしたら処理を抜ける
                                    End If
                                Else
                                    intRet = EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR  '結果
                                    Exit Select
                                End If
                            End If
                        Next

                        '商品３レコードオブジェクトの破棄
                        jLogic.DeleteSyouhin23Rec(sender, jibanRec, True)
                    End If

                Case EarthEnum.emTeibetuAction.TEIBETU_UPD '●更新

                    If intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN1 Then '商品1
                        strTmpSyouhinCd = String.Empty
                        intTmpUriGaku = 0
                        intTmpKoumutenGaku = 0

                        If Not .Syouhin1Record Is Nothing Then
                            Me.SetDisplayString(.Syouhin1Record.SyouhinCd, strTmpSyouhinCd)
                            Me.SetDisplayString(.Syouhin1Record.UriKeijyouFlg, intTmpUriKeijouFlg)
                            Me.SetDisplayString(.Syouhin1Record.UriGaku, intTmpUriGaku)
                            Me.SetDisplayString(.Syouhin1Record.KoumutenSeikyuuGaku, intTmpKoumutenGaku)
                        End If

                        '未計上の場合
                        If intTmpUriKeijouFlg <> 1 Then
                            '●金額の設定
                            Select Case intKingaku
                                Case EarthEnum.emKingakuAction.KINGAKU_PLUS
                                    '紐付可否のチェック
                                    If Me.pf_ChkHimodukeKingaku(ttRec.UriKasanGaku, ttRec.KoumutenKasanGaku) Then
                                        '実請求金額 += 実請求加算金額
                                        .Syouhin1Record.UriGaku += ttRec.UriKasanGaku
                                        '工務店請求金額 += 工務店請求加算金額
                                        .Syouhin1Record.KoumutenSeikyuuGaku += ttRec.KoumutenKasanGaku
                                        '※消費税額 = 実請求金額と税率より換算
                                        .Syouhin1Record.UriageSyouhiZeiGaku = Integer.MinValue

                                        '変更状況
                                        .Syouhin1Record.KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin1Record.KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE)
                                        '価格反映対象確認フラグ
                                        ttRec.ChgVal = True

                                        '●邸別請求との紐付け
                                        ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1
                                        ttRec.GamenHyoujiNo = intCnt

                                        intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                    Else
                                        intRet = EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR  '結果
                                        Exit Select
                                    End If

                                Case EarthEnum.emKingakuAction.KINGAKU_MINUS
                                    '画面表示NOと分類コードが一致する場合
                                    If ttRec.GamenHyoujiNo = intCnt AndAlso ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1 Then
                                        '実請求金額 += 実請求加算金額
                                        .Syouhin1Record.UriGaku -= ttRec.UriKasanGaku
                                        '工務店請求金額 += 工務店請求加算金額
                                        .Syouhin1Record.KoumutenSeikyuuGaku -= ttRec.KoumutenKasanGaku
                                        '※消費税額 = 実請求金額と税率より換算
                                        .Syouhin1Record.UriageSyouhiZeiGaku = Integer.MinValue

                                        '変更状況
                                        .Syouhin1Record.KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin1Record.KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE)
                                        '価格反映対象確認フラグ
                                        ttRec.ChgVal = True

                                        '●邸別請求との紐付け
                                        ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1
                                        ttRec.GamenHyoujiNo = intCnt

                                        intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                    End If
                            End Select

                        End If

                    ElseIf intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN2 Then '商品2

                        '商品２レコードオブジェクトの生成
                        jLogic.CreateSyouhin23Rec(sender, jibanRec)

                        '●特別対応価格対応
                        For intCnt = 1 To EarthConst.SYOUHIN2_COUNT
                            '該当の画面表示NOまで処理をとばす
                            If intGamenHyoujiNo <> intCnt Then
                                Continue For
                            End If

                            strTmpSyouhinCd = String.Empty
                            intTmpUriGaku = 0
                            intTmpKoumutenGaku = 0

                            If Not .Syouhin2Records Is Nothing AndAlso .Syouhin2Records.ContainsKey(intCnt) Then
                                Me.SetDisplayString(.Syouhin2Records(intCnt).SyouhinCd, strTmpSyouhinCd)
                                Me.SetDisplayString(.Syouhin2Records(intCnt).UriKeijyouFlg, intTmpUriKeijouFlg)
                                Me.SetDisplayString(.Syouhin2Records(intCnt).UriGaku, intTmpUriGaku)
                                Me.SetDisplayString(.Syouhin2Records(intCnt).KoumutenSeikyuuGaku, intTmpKoumutenGaku)
                            End If

                            '●金額の設定
                            Select Case intKingaku
                                Case EarthEnum.emKingakuAction.KINGAKU_PLUS '増額
                                    '未計上の場合
                                    If intTmpUriKeijouFlg <> 1 Then
                                        '倉庫コード判断
                                        If strSoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_110 Then
                                            '紐付可否のチェック
                                            If Me.pf_ChkHimodukeKingaku(ttRec.UriKasanGaku, ttRec.KoumutenKasanGaku) Then
                                                '実請求金額 += 実請求加算金額
                                                .Syouhin2Records(intCnt).UriGaku += ttRec.UriKasanGaku
                                                '工務店請求金額 += 工務店請求加算金額
                                                .Syouhin2Records(intCnt).KoumutenSeikyuuGaku += ttRec.KoumutenKasanGaku
                                                '消費税額 = 実請求金額と税率より換算
                                                .Syouhin2Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                                '変更状況
                                                .Syouhin2Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin2Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE)
                                                '価格反映対象確認フラグ
                                                ttRec.ChgVal = True

                                                '●邸別請求との紐付け
                                                ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_110
                                                ttRec.GamenHyoujiNo = intCnt

                                                intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                                Exit For 'セットしたら処理を抜ける
                                            Else
                                                intRet = EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR  '結果
                                                Exit Select
                                            End If

                                        ElseIf strSoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_115 Then '※割引商品のため、プラスマイナスに注意
                                            '紐付可否のチェック
                                            If Me.pf_ChkHimodukeKingaku(ttRec.UriKasanGaku, ttRec.KoumutenKasanGaku) Then
                                                '実請求金額 += 実請求加算金額
                                                .Syouhin2Records(intCnt).UriGaku -= ttRec.UriKasanGaku
                                                '工務店請求金額 += 工務店請求加算金額
                                                .Syouhin2Records(intCnt).KoumutenSeikyuuGaku -= ttRec.KoumutenKasanGaku
                                                '消費税額 = 実請求金額と税率より換算
                                                .Syouhin2Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                                '変更状況
                                                .Syouhin2Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin2Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE)
                                                '価格反映対象確認フラグ
                                                ttRec.ChgVal = True

                                                '●邸別請求との紐付け
                                                ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_115
                                                ttRec.GamenHyoujiNo = intCnt

                                                intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                                Exit For 'セットしたら処理を抜ける
                                            Else
                                                intRet = EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR  '結果
                                                Exit Select
                                            End If
                                        End If
                                    End If

                                Case EarthEnum.emKingakuAction.KINGAKU_MINUS '減額
                                    '画面表示NOと分類コードが一致する場合
                                    If ttRec.GamenHyoujiNo = intCnt AndAlso (ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_110 Or ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_115) Then
                                        '未計上の場合
                                        If intTmpUriKeijouFlg <> 1 Then
                                            '倉庫コード判断
                                            If strSoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_110 Then
                                                '実請求金額 += 実請求加算金額
                                                .Syouhin2Records(intCnt).UriGaku -= ttRec.UriKasanGaku
                                                '工務店請求金額 += 工務店請求加算金額
                                                .Syouhin2Records(intCnt).KoumutenSeikyuuGaku -= ttRec.KoumutenKasanGaku
                                                '消費税額 = 実請求金額と税率より換算
                                                .Syouhin2Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                                '変更状況
                                                .Syouhin2Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin2Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE)
                                                '価格反映対象確認フラグ
                                                ttRec.ChgVal = True

                                                '●邸別請求との紐付け
                                                ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_110
                                                ttRec.GamenHyoujiNo = intCnt

                                                intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                                Exit For 'セットしたら処理を抜ける

                                            ElseIf strSoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_115 Then '※割引商品のため、プラスマイナスに注意
                                                '紐付可否のチェック
                                                If Me.pf_ChkHimodukeKingaku(ttRec.UriKasanGaku, ttRec.KoumutenKasanGaku) Then
                                                    '実請求金額 += 実請求加算金額
                                                    .Syouhin2Records(intCnt).UriGaku += ttRec.UriKasanGaku
                                                    '工務店請求金額 += 工務店請求加算金額
                                                    .Syouhin2Records(intCnt).KoumutenSeikyuuGaku += ttRec.KoumutenKasanGaku
                                                    '消費税額 = 実請求金額と税率より換算
                                                    .Syouhin2Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                                    '変更状況
                                                    .Syouhin2Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin2Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE)
                                                    '価格反映対象確認フラグ
                                                    ttRec.ChgVal = True

                                                    '●邸別請求との紐付け
                                                    ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_2_115
                                                    ttRec.GamenHyoujiNo = intCnt

                                                    intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                                    Exit For 'セットしたら処理を抜ける
                                                Else
                                                    intRet = EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR  '結果
                                                    Exit Select
                                                End If
                                            End If
                                        End If
                                    End If
                            End Select
                        Next

                        '商品２レコードオブジェクトの破棄
                        jLogic.DeleteSyouhin23Rec(sender, jibanRec)

                    ElseIf intBunrui = EarthEnum.emTeibetuBunrui.SYOUHIN3 Then '商品3

                        '商品３レコードオブジェクトの生成
                        jLogic.CreateSyouhin23Rec(sender, jibanRec, True)

                        For intCnt = 1 To EarthConst.SYOUHIN3_COUNT
                            '該当の画面表示NOまで処理をとばす
                            If intGamenHyoujiNo <> intCnt Then
                                Continue For
                            End If

                            strTmpSyouhinCd = String.Empty
                            intTmpUriGaku = 0
                            intTmpKoumutenGaku = 0

                            If Not .Syouhin3Records Is Nothing AndAlso .Syouhin3Records.ContainsKey(intCnt) Then
                                Me.SetDisplayString(.Syouhin3Records(intCnt).SyouhinCd, strTmpSyouhinCd)
                                Me.SetDisplayString(.Syouhin3Records(intCnt).UriKeijyouFlg, intTmpUriKeijouFlg)
                                Me.SetDisplayString(.Syouhin3Records(intCnt).UriGaku, intTmpUriGaku)
                                Me.SetDisplayString(.Syouhin3Records(intCnt).KoumutenSeikyuuGaku, intTmpKoumutenGaku)
                            End If

                            '●金額の設定
                            Select Case intKingaku
                                Case EarthEnum.emKingakuAction.KINGAKU_PLUS '増額
                                    '未計上の場合
                                    If intTmpUriKeijouFlg <> 1 Then
                                        '紐付可否のチェック
                                        If Me.pf_ChkHimodukeKingaku(ttRec.UriKasanGaku, ttRec.KoumutenKasanGaku) Then
                                            '実請求金額 += 実請求加算金額
                                            .Syouhin3Records(intCnt).UriGaku += ttRec.UriKasanGaku
                                            '工務店請求金額 += 工務店請求加算金額
                                            .Syouhin3Records(intCnt).KoumutenSeikyuuGaku += ttRec.KoumutenKasanGaku
                                            '消費税額 = 実請求金額と税率より換算
                                            .Syouhin3Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                            '変更状況
                                            .Syouhin3Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin3Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE)
                                            '価格反映対象確認フラグ
                                            ttRec.ChgVal = True

                                            '●邸別請求との紐付け
                                            ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3
                                            ttRec.GamenHyoujiNo = intCnt

                                            intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                            Exit For 'セットしたら処理を抜ける
                                        Else
                                            intRet = EarthEnum.emKkkHanneiErr.KASAN_KINGAKU_ERR  '結果
                                            Exit Select
                                        End If
                                    End If

                                Case EarthEnum.emKingakuAction.KINGAKU_MINUS '減額
                                    '画面表示NOと分類コードが一致する場合
                                    If ttRec.GamenHyoujiNo = intCnt AndAlso ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3 Then
                                        '未計上の場合
                                        If intTmpUriKeijouFlg <> 1 Then
                                            '実請求金額 += 実請求加算金額
                                            .Syouhin3Records(intCnt).UriGaku -= ttRec.UriKasanGaku
                                            '工務店請求金額 += 工務店請求加算金額
                                            .Syouhin3Records(intCnt).KoumutenSeikyuuGaku -= ttRec.KoumutenKasanGaku
                                            '消費税額 = 実請求金額と税率より換算
                                            .Syouhin3Records(intCnt).UriageSyouhiZeiGaku = Integer.MinValue

                                            '変更状況
                                            .Syouhin3Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin3Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE)
                                            '価格反映対象確認フラグ
                                            ttRec.ChgVal = True

                                            '●邸別請求との紐付け
                                            ttRec.BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3
                                            ttRec.GamenHyoujiNo = intCnt

                                            '特別対応コード(80～99)の特別対応を減額した場合でかつ、発注書金額<>0の場合、商品を削除する
                                            '※後続処理のため、商品コードはクリアしないでおく
                                            '以外は商品は削除せず、金額の減額のみを行なう。
                                            If Me.pf_ChkFuriwakeTokubetuTaiou80To99(ttRec.TokubetuTaiouCd) _
                                                AndAlso (.Syouhin3Records(intCnt).HattyuusyoGaku = Integer.MinValue OrElse .Syouhin3Records(intCnt).HattyuusyoGaku = 0) Then
                                                '変更状況
                                                .Syouhin3Records(intCnt).KkkHenkouCheck = Me.pf_ChkTeibetuActionJyky(.Syouhin3Records(intCnt).KkkHenkouCheck, EarthEnum.emTeibetuSeikyuuKkkJyky.DELETE)
                                            End If

                                            intRet = EarthEnum.emKkkHanneiErr.ERR_NOTHING  '結果
                                            Exit For 'セットしたら処理を抜ける
                                        End If
                                    End If
                            End Select
                        Next

                        '商品３レコードオブジェクトの破棄
                        jLogic.DeleteSyouhin23Rec(sender, jibanRec, True)
                    End If
            End Select
        End With

        Return intRet
    End Function

    ''' <summary>
    ''' 特別対応コードの振分け先を取得
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ttRec">特別対応レコードクラス</param>
    ''' <param name="chkTorikesiFlg">取消=0のみ対象とするフラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkDevideTaisyou(ByVal sender As Object, _
                                       ByVal ttRec As TokubetuTaiouRecordBase, _
                                       Optional ByVal chkTorikesiFlg As Boolean = False) As String

        Dim strResult As String = String.Empty  '振分先

        '特別対応コードが存在しない場合、処理対象外
        If ttRec.TokubetuTaiouCd = Integer.MinValue Then
            Return strResult
        End If

        '設定先(振分先)が無い場合、処理対象外
        If ttRec.BunruiCd = String.Empty OrElse ttRec.GamenHyoujiNo = 0 Then
            Return strResult
        End If

        '受注(特別対応画面で登録ボタン押下後)
        If chkTorikesiFlg Then
            '取消、あるいは金額加算商品コード=未設定の場合、処理対象外
            If ttRec.Torikesi = 1 _
                OrElse ttRec.KasanSyouhinCd = String.Empty Then
                Return strResult
            End If
        End If

        '価格処理フラグ=1の場合
        If ttRec.KkkSyoriFlg = 1 Then
            '取消=1の場合、処理対象外
            If ttRec.Torikesi = 1 Then
                Return strResult
            End If
        End If

        '振分け先を取得
        strResult = Me.DevideTokubetuCd(sender, ttRec.BunruiCd, ttRec.GamenHyoujiNo)

        Return strResult
    End Function

    ''' <summary>
    ''' 特別対応ツールチップに表示する値をセットするかを判断する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ttRec">特別対応レコードクラス</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="strGamenHyoujiNo">画面表示NO</param>
    ''' <param name="strUriKeijouFlg">売上計上FLG(受注・邸別)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkToolTipSetValue(ByVal sender As Object, _
                                         ByVal ttRec As TokubetuTaiouRecordBase, _
                                         ByVal strBunruiCd As String, _
                                         ByVal strGamenHyoujiNo As String, _
                                         Optional ByVal strUriKeijouFlg As String = "") As EarthEnum.emToolTipType

        '分類コード、画面表示NOがセットされている場合
        If ttRec.BunruiCd <> String.Empty And (ttRec.GamenHyoujiNo <> Integer.MinValue OrElse ttRec.GamenHyoujiNo <> 0) Then

            '画面表示NO
            Dim ttGamenHyoujiNo As String
            If ttRec.GamenHyoujiNo = Integer.MinValue Then
                ttGamenHyoujiNo = "0"
            Else
                ttGamenHyoujiNo = ttRec.GamenHyoujiNo.ToString
            End If

            '分類コード、画面表示NOが一致する場合
            If ttRec.BunruiCd = strBunruiCd AndAlso ttGamenHyoujiNo = strGamenHyoujiNo Then
                '画面.売上計上FLG=1かつ、価格処理フラグ=1の場合
                If strUriKeijouFlg = "1" AndAlso ttRec.KkkSyoriFlg = 1 Then
                    '取消=1の場合、処理対象外
                    If ttRec.Torikesi = 1 Then
                        Return EarthEnum.emToolTipType.NASI
                    Else '｢修｣
                        Return EarthEnum.emToolTipType.SYUSEI
                    End If
                Else '｢特｣
                    Return EarthEnum.emToolTipType.ARI
                End If

            End If
        End If

        Return EarthEnum.emToolTipType.NASI
    End Function

    ''' <summary>
    ''' 振分先を取得する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="intGamenHyoujiNo">画面表示NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DevideTokubetuCd(ByVal sender As Object, ByVal strBunruiCd As String, ByVal intGamenHyoujiNo As Integer) As String
        '結果格納用
        Dim strResult As String = String.Empty

        '分類コード
        Select Case strBunruiCd
            Case EarthConst.SOUKO_CD_SYOUHIN_1  '商品1
                '画面表示NO
                Select Case intGamenHyoujiNo
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_1
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(0)
                End Select

            Case EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115 '商品2
                '画面表示NO
                Select Case intGamenHyoujiNo
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_1
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(1)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_2
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(2)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_3
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(3)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_4
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(4)
                End Select

            Case EarthConst.SOUKO_CD_SYOUHIN_3  '商品3
                '画面表示NO
                Select Case intGamenHyoujiNo
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_1
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(5)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_2
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(6)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_3
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(7)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_4
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(8)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_5
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(9)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_6
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(10)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_7
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(11)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_8
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(12)
                    Case EarthEnum.emGamenHyoujiNo.HYOUJI_NO_9
                        strResult = EarthConst.Instance.ARRAY_SHOUHIN_LINES(13)
                End Select
        End Select

        Return strResult
    End Function

    ''' <summary>
    ''' 振分先を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetTokubetuTaiouCdTeibetuKey(ByRef htTtCd As Dictionary(Of Integer, String), ByVal emTeibetuType As EarthEnum.emTeibetuBunrui, ByVal strTeibetuInfo() As String)
        '結果格納用
        Dim strResult As String = String.Empty

        Dim arrTokubetuCds() As String = strTeibetuInfo
        Dim arrTokubetuCd() As String = strTeibetuInfo

        Dim intTeibetuBunruiCnt As Integer = 0
        Dim intTtCnt As Integer = 0

        Dim strTmpTtCd As String = String.Empty
        Dim intTmpTtCd As Integer = Integer.MinValue

        Dim intTmpStart As Integer = 0

        '分類コード
        Select Case emTeibetuType
            Case EarthEnum.emTeibetuBunrui.SYOUHIN1
                intTmpStart = 0

                For intTeibetuBunruiCnt = 0 To arrTokubetuCds.Length - 1
                    arrTokubetuCd = Split(arrTokubetuCds(intTeibetuBunruiCnt), EarthConst.SEP_STRING)
                    For intTtCnt = 0 To arrTokubetuCd.Length - 1
                        strTmpTtCd = Trim(arrTokubetuCd(intTtCnt))
                        If strTmpTtCd <> String.Empty Then
                            htTtCd.Add(CInt(strTmpTtCd), EarthConst.Instance.ARRAY_SHOUHIN_LINES(intTmpStart + intTeibetuBunruiCnt))
                        End If
                    Next
                Next

            Case EarthEnum.emTeibetuBunrui.SYOUHIN2
                intTmpStart = 1

                For intTeibetuBunruiCnt = 0 To arrTokubetuCds.Length - 1
                    arrTokubetuCd = Split(arrTokubetuCds(intTeibetuBunruiCnt), EarthConst.SEP_STRING)
                    For intTtCnt = 0 To arrTokubetuCd.Length - 1
                        strTmpTtCd = Trim(arrTokubetuCd(intTtCnt))
                        If strTmpTtCd <> String.Empty Then
                            htTtCd.Add(CInt(strTmpTtCd), EarthConst.Instance.ARRAY_SHOUHIN_LINES(intTmpStart + intTeibetuBunruiCnt))
                        End If
                    Next
                Next

            Case EarthEnum.emTeibetuBunrui.SYOUHIN3
                intTmpStart = 5

                For intTeibetuBunruiCnt = 0 To arrTokubetuCds.Length - 1
                    arrTokubetuCd = Split(arrTokubetuCds(intTeibetuBunruiCnt), EarthConst.SEP_STRING)
                    For intTtCnt = 0 To arrTokubetuCd.Length - 1
                        strTmpTtCd = Trim(arrTokubetuCd(intTtCnt))
                        If strTmpTtCd <> String.Empty Then
                            htTtCd.Add(CInt(strTmpTtCd), EarthConst.Instance.ARRAY_SHOUHIN_LINES(intTmpStart + intTeibetuBunruiCnt))
                        End If
                    Next
                Next
        End Select

        Return strResult
    End Function

    ''' <summary>
    ''' 特別対応データの更新(邸別請求データとの紐付)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="listRes">特別対応レコードのリスト</param>
    ''' <param name="strLoginUserId">更新ログインユーザID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_setKey_TokubetuTaiou_TeibetuSeikyuu(ByVal sender As Object, ByVal listRes As List(Of TokubetuTaiouRecordBase), ByVal strLoginUserId As String, ByVal emGamenType As EarthEnum.emGamenInfo) As Boolean

        Dim intCnt As Integer = 0
        Dim dtRec As New TokubetuTaiouRecordBase
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recResult As New TokubetuTaiouRecordBase    'レコードクラス
        Dim total_count As Integer = 0 '取得件数
        Dim ttLogic As New TokubetuTaiouLogic

        '**********************************************
        ' 特別対応データの更新(邸別請求データとの紐付)
        '**********************************************
        '処理対象がない場合には処理を抜ける
        If listRes Is Nothing Then
            Return True
            Exit Function
        End If

        For intCnt = 0 To listRes.Count - 1
            dtRec = listRes(intCnt)
            With dtRec
                '●特別対応データの更新(邸別請求データとの紐付)
                If Not dtRec Is Nothing _
                    AndAlso .UpdFlg = 1 _
                        AndAlso .KkkSyoriFlg <> 1 _
                            AndAlso .Kbn <> String.Empty AndAlso .HosyousyoNo <> String.Empty _
                                AndAlso .BunruiCd <> String.Empty AndAlso .GamenHyoujiNo > 0 Then
                    '初期化
                    total_count = 0
                    recResult = New TokubetuTaiouRecordBase

                    '更新対象の特別対応データをレコードで取得
                    recResult = ttLogic.GetTokubetuTaiouDataRec(sender, dtRec.Kbn, dtRec.HosyousyoNo, dtRec.TokubetuTaiouCd, total_count)
                    If total_count = -1 Then
                        ' 検索結果件数が-1の場合、エラーなので、処理終了
                        Exit Function
                    End If

                    Select Case emGamenType
                        '下記の画面は同トランでINSERT処理をするため、排他しない
                        Case EarthEnum.emGamenInfo.MousikomiInput
                        Case EarthEnum.emGamenInfo.MousikomiSearch
                        Case EarthEnum.emGamenInfo.MousikomiSyuusei
                        Case Else
                            '特別対応データが存在する場合、UPDATE
                            If Not recResult Is Nothing _
                                AndAlso recResult.Kbn <> String.Empty Then

                                '●排他チェック
                                If recResult.UpdDatetime <> dtRec.UpdDatetime Then
                                    ' 排他チェックエラー
                                    mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "特別対応データテーブル")
                                    Return False
                                End If
                            End If
                    End Select

                    IFsqlMaker = New UpdateStringHelper 'UPDATE

                    '更新ログインユーザーIDを設定
                    .UpdLoginUserId = strLoginUserId
                    '更新日時を設定
                    .UpdDatetime = DateTime.Now
                    '更新フラグ
                    .UpdFlg = 0

                    '●Old値退避
                    .KasanSyouhinCdOld = .KasanSyouhinCd
                    .KoumutenKasanGakuOld = .KoumutenKasanGaku
                    .UriKasanGakuOld = .UriKasanGaku

                    If .DeleteFlg = False Then
                        If .BunruiCd <> String.Empty AndAlso .GamenHyoujiNo > 0 Then

                            If .Torikesi = 0 Then
                                '金額加算商品コード=未設定の場合
                                If .KasanSyouhinCd = String.Empty Then
                                    '紐付け解除
                                    .BunruiCd = Nothing
                                    .GamenHyoujiNo = Integer.MinValue
                                End If

                            Else '取消の場合、紐付け解除
                                '価格処理フラグがたっている場合、紐付け解除しない
                                If .KkkSyoriFlg = 1 Then
                                    Continue For
                                Else
                                    '紐付け解除
                                    .BunruiCd = Nothing
                                    .GamenHyoujiNo = Integer.MinValue
                                End If

                            End If
                        End If

                    Else '特例削除(受注画面からの削除指定処理)

                        '価格処理フラグがたっている場合、紐付け解除しない
                        If .KkkSyoriFlg = 1 Then
                            Continue For
                        Else
                            '紐付け解除
                            .BunruiCd = Nothing
                            .GamenHyoujiNo = Integer.MinValue
                        End If

                    End If

                    '特例価格セット処理(邸別データ画面からの指定処理)
                    If .KkkSetFlg Then
                        .Torikesi = recResult.Torikesi
                        .KkkSyoriFlg = 1

                        '登録/更新用文字列とパラメータの作成
                        strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(TokubetuTaiouKkkSyoriRecord), dtRec, listParam, GetType(TokubetuTaiouRecordBase))
                    Else
                        '登録/更新用文字列とパラメータの作成
                        strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(TokubetuTaiouTeibetuKeyRecord), dtRec, listParam, GetType(TokubetuTaiouRecordBase))
                    End If

                    'DB反映処理
                    If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                        Return False
                        Exit Function
                    End If

                End If

            End With

        Next

        Return True
    End Function

    ''' <summary>
    ''' 特別対応コード=80～99の場合、Trueを返却。以外はFalseを返却
    ''' </summary>
    ''' <param name="intTokubetuTaiouCd">特別対応コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_ChkFuriwakeTokubetuTaiou80To99(ByVal intTokubetuTaiouCd As Integer) As Boolean
        Dim blnRet As Boolean = False

        Select Case intTokubetuTaiouCd
            '特別対応コード=80～99の場合、Trueを返却
            Case EarthConst.TOKUBETU_TAIOU_CD_FURIWAKE_MIN To EarthConst.TOKUBETU_TAIOU_CD_FURIWAKE_MAX
                blnRet = True
        End Select

        Return blnRet
    End Function

    ''' <summary>
    ''' 実請求加算金額と工務店請求加算金額がともに0円の場合は、紐付け対象としないためのチェック関数
    ''' ※NULLの場合も同様
    ''' </summary>
    ''' <param name="intJituGaku">実請求加算金額</param>
    ''' <param name="intKoumutenGaku">工務店請求加算金額</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_ChkHimodukeKingaku(ByVal intJituGaku As Integer, ByVal intKoumutenGaku As Integer) As Boolean
        Dim blnRet As Boolean = True

        If (intJituGaku = Integer.MinValue OrElse intJituGaku = 0) _
            AndAlso (intKoumutenGaku = Integer.MinValue OrElse intKoumutenGaku = 0) Then
            blnRet = False
        End If

        Return blnRet
    End Function
#End Region

    ''' <summary>
    ''' 進捗データテーブルのOptionsにセットする値を編集する
    ''' </summary>
    ''' <param name="jibanRec">地盤レコードクラス</param>
    ''' <param name="emGetDispType">表示タイプ</param>
    ''' <returns></returns>
    ''' <remarks>※区分、番号、加盟店コード、商品コード1、調査方法NO</remarks>
    Public Function GetTokubetuTaiouKkkNoneSetUp(ByVal jibanRec As JibanRecordBase, ByVal emGetDispType As EarthEnum.emGetDispType) As String
        Dim strRet As String = String.Empty '戻り値

        Dim strTmp As String = String.Empty '作業用
        Dim blnSet As Boolean = False

        Dim intTotalCnt As Integer = 0
        Dim listRes As New List(Of TokubetuTaiouRecordBase)
        Dim dtRec As TokubetuTaiouRecordBase
        Dim intCnt As Integer = 0 'カウンタ
        Dim mttLogic As New TokubetuTaiouMstLogic
        Dim sender As New Object

        Dim strTmpCd As String = String.Empty '特別対応コード
        Dim strTmpMeisyou As String = String.Empty '特別対応名

        '特別対応マスタ
        With jibanRec
            If Not jibanRec Is Nothing AndAlso Not .Syouhin1Record Is Nothing Then
                listRes = mttLogic.GetTokubetuTaiouInfo(sender, .Kbn, .HosyousyoNo, .KameitenCd, .Syouhin1Record.SyouhinCd, .TysHouhou, intTotalCnt)
            End If
        End With

        ' 検索結果ゼロ件の場合
        If intTotalCnt <= 0 Then
            '検索結果件数が-1の場合、エラーなので、処理終了
            Return strRet
            Exit Function
        End If

        '対象データより抽出
        For intCnt = 0 To listRes.Count - 1
            'レコードが存在する場合のみ画面表示
            dtRec = listRes(intCnt)

            If Not dtRec Is Nothing AndAlso dtRec.TokubetuTaiouCd <> Integer.MinValue Then
                With dtRec
                    blnSet = False
                    strTmpCd = .TokubetuTaiouCd.ToString
                    strTmpMeisyou = .TokubetuTaiouMeisyou

                    '更新フラグ=1
                    If .UpdFlg = 1 Then
                        '価格処理フラグ<>1
                        If .KkkSyoriFlg <> 1 Then
                            '有効データでかつ、設定先がない：増額
                            '取消データでかつ、設定先がある：減額
                            If .Torikesi = 0 Then
                                'New値
                                If .KasanSyouhinCd <> String.Empty Then
                                    If .BunruiCd = String.Empty And .GamenHyoujiNo = 0 Then
                                        If Me.pf_ChkHimodukeKingaku(.UriKasanGaku, .KoumutenKasanGaku) Then
                                            '実請求加算金額<>0でかつ工務店請求金額<>0
                                            If Me.pf_ChkFuriwakeTokubetuTaiou80To99(dtRec.TokubetuTaiouCd) = False Then
                                                blnSet = True
                                            Else
                                                '特別対応コード=80～99でかつ、倉庫コード=120の場合
                                                If dtRec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_3 Then
                                                    blnSet = True
                                                End If
                                            End If
                                        End If
                                    ElseIf .BunruiCd <> String.Empty And .GamenHyoujiNo > 0 Then
                                        If Me.pf_ChkMinusPlusKkkChg(dtRec) Then
                                            blnSet = True
                                        End If
                                    End If
                                End If

                            Else 'Old値
                                If .KasanSyouhinCdOld <> String.Empty Then
                                    If .BunruiCd <> String.Empty And .GamenHyoujiNo > 0 Then
                                        If Me.pf_ChkHimodukeKingaku(.UriKasanGakuOld, .KoumutenKasanGakuOld) Then
                                            '実請求加算金額<>0でかつ工務店請求金額<>0
                                            If Me.pf_ChkFuriwakeTokubetuTaiou80To99(dtRec.TokubetuTaiouCd) = False Then
                                                blnSet = True
                                            Else
                                                '特別対応コード=80～99でかつ、倉庫コード=120の場合
                                                If dtRec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_3 Then
                                                    blnSet = True
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    '該当レコードが存在する場合、名称をセット
                    If blnSet Then
                        Select Case emGetDispType
                            Case EarthEnum.emGetDispType.CODE
                                strTmp = strTmpCd
                            Case EarthEnum.emGetDispType.MEISYOU
                                strTmp = strTmpMeisyou
                            Case EarthEnum.emGetDispType.CODE_MEISYOU
                                strTmp = strTmpCd & ":" & strTmpMeisyou
                        End Select

                        If strRet = String.Empty Then
                            strRet = strTmp
                        Else
                            strRet &= EarthConst.SEP_STRING & strTmp
                        End If
                    End If
                End With
            End If
        Next

        Return strRet
    End Function

    ''' <summary>
    ''' 減額/増額チェック
    ''' ※金額加算商品コードOld=設定済でかつ、金額加算商品コード、工務店請求加算金額、実請求加算金額とOld値のいずれかが異なる場合、Trueを返却する
    ''' </summary>
    ''' <param name="dtRec">特別対応レコードのクラス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_ChkMinusPlusKkkChg(ByVal dtRec As TokubetuTaiouRecordBase) As Boolean
        Dim blnRet As Boolean = False

        Dim strKasanSyouhinCd As String = String.Empty
        Dim intUriKasanGaku As Integer = Integer.MinValue
        Dim intKoumutenKasanGaku As Integer = Integer.MinValue

        If Not dtRec Is Nothing Then
            With dtRec
                If .KasanSyouhinCdOld = String.Empty Then
                Else
                    If .KasanSyouhinCd <> .KasanSyouhinCdOld _
                        OrElse .UriKasanGaku <> .UriKasanGakuOld _
                            OrElse .KoumutenKasanGaku <> .KoumutenKasanGakuOld Then
                        blnRet = True
                    End If
                End If
            End With
        End If

        Return blnRet
    End Function

    ''' <summary>
    ''' 邸別請求へのアクション設定上書き判断処理
    ''' </summary>
    ''' <param name="emTeibetuActTypeMae">前のアクション</param>
    ''' <param name="emTeibetuActTypeAto">後のアクション</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function pf_ChkTeibetuActionJyky(ByVal emTeibetuActTypeMae As EarthEnum.emTeibetuSeikyuuKkkJyky, ByVal emTeibetuActTypeAto As EarthEnum.emTeibetuSeikyuuKkkJyky) As EarthEnum.emTeibetuSeikyuuKkkJyky
        Dim emRet As EarthEnum.emTeibetuSeikyuuKkkJyky = EarthEnum.emTeibetuSeikyuuKkkJyky.NONE

        '未設定の場合、変更後でセット
        If emTeibetuActTypeMae = EarthEnum.emTeibetuSeikyuuKkkJyky.NONE Then
            emRet = emTeibetuActTypeAto

        ElseIf emTeibetuActTypeMae = EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT Then
            '追加で更新は、追加
            If emTeibetuActTypeAto = EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE Then
                emRet = EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT
            Else
                emRet = emTeibetuActTypeAto
            End If

        ElseIf emTeibetuActTypeMae = EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE Then
            '更新で追加は、更新
            If emTeibetuActTypeAto = EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT Then
                emRet = EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE
            ElseIf emTeibetuActTypeAto = EarthEnum.emTeibetuSeikyuuKkkJyky.DELETE Then
                '更新で削除は、削除
                emRet = EarthEnum.emTeibetuSeikyuuKkkJyky.DELETE
            Else
                emRet = emTeibetuActTypeAto
            End If

        ElseIf emTeibetuActTypeMae = EarthEnum.emTeibetuSeikyuuKkkJyky.DELETE Then
            '削除で追加は、更新
            If emTeibetuActTypeAto = EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT Then
                emRet = EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE
            Else
                emRet = emTeibetuActTypeAto
            End If

        Else
            emRet = emTeibetuActTypeAto

        End If

        Return emRet
    End Function
#End Region

#Region "加盟店調査会社チェック(調査手配センター)"

    ''' <summary>
    ''' 加盟店調査会社チェック(調査手配センター)
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysGaisyaCd">調査会社コード(調査会社コード＋事業所コード)</param>
    ''' <returns>True or False</returns>
    ''' <remarks>該当の調査会社コードが加盟店調査会社設定マスタ.に存在するかを判定する</remarks>
    Public Function ChkExistKameitenTysTehaiCenter( _
                                     ByVal strKameitenCd As String _
                                     , ByRef strTysGaisyaCd As String _
                                     ) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkExistKameitenTysTehaiCenter", _
                                            strKameitenCd _
                                            , strTysGaisyaCd _
                                            )

        Dim dataAccess As New TyousakaisyaSearchDataAccess
        Dim dtTbl As New DataTable
        Dim strResult As String = String.Empty

        If Left(strTysGaisyaCd, 4) <> "9000" Then
            Return strResult
        End If

        '調査会社設定Mより指定調査会社の存在チェックを行なう
        dtTbl = dataAccess.GetKameitenTysTehaiCenter(strKameitenCd, strTysGaisyaCd)
        ' 取得できなかった場合は空白を返却
        If dtTbl.Rows.Count > 0 Then
            For intCnt As Integer = 0 To dtTbl.Rows.Count - 1
                If intCnt > 0 Then
                    strResult &= ","
                End If
                strResult &= dtTbl.Rows(intCnt)("TYSGAISYACD").ToString()
            Next
        End If
        Return strResult
    End Function

#End Region

    ''' <summary>
    ''' 商品コード、加盟店コードを元にチェックする
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>結果</returns>
    ''' <remarks></remarks>
    Public Function ChkTysJidouSet(ByVal strSyouhinCd As String, _
                                    ByVal strKameitenCd As String, _
                                    ByRef strTysGaisyaCd As String) As Boolean

        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim kameitenList As New List(Of KameitenSearchRecord)
        Dim total_count As Integer

        Dim JibanLogic As New JibanLogic
        Dim recSyouhin As New SyouhinMeisaiRecord

        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True

        Dim dataAccess As New TyousakaisyaSearchDataAccess
        Dim blnRet As Boolean = False

        Dim strSiteiTyousakaisyaCd As String = String.Empty     '指定調査会社
        Dim strYuusenTyousakaisyaCd As String = String.Empty    '優先調査会社
        Dim intSiteiSdsHojiInfo As Integer = Integer.MinValue   '指定調査会社のSDS保持情報
        Dim intYuusenSdsHojiInfo As Integer = Integer.MinValue  '優先調査会社のSDS保持情報

        If strKameitenCd <> String.Empty AndAlso strSyouhinCd <> String.Empty Then
            '加盟店と商品の取得
            kameitenList = kameitenSearchLogic.GetKameitenSearchResult("", _
                                   strKameitenCd, _
                                   True, _
                                   total_count)
            recSyouhin = JibanLogic.GetSyouhinRecord(strSyouhinCd)

            If (kameitenList.Count = 1) AndAlso (Not recSyouhin Is Nothing) Then
                '加盟店チェック(SDS自動設定情報=1)と商品チェック(SDS自動設定=1)
                If (kameitenList(0).SdsJidoouSetInfo = 1) AndAlso (recSyouhin.SdsJidouSet = 1) Then
                    '指定調査会社の取得
                    dataAccess.GetSiteiYuusenTyousakaisyaCd(strKameitenCd, strSiteiTyousakaisyaCd, EarthEnum.EnumKameitenTyousakaisyaKensakuType.SITEITYOUSAKAISYA)
                    '取得した調査会社のSDS保持の取得
                    intSiteiSdsHojiInfo = tyousakaisyaSearchLogic.GetTyousaKaisyaSDS(strSiteiTyousakaisyaCd, "", False)

                    '優先調査会社の取得
                    dataAccess.GetSiteiYuusenTyousakaisyaCd(strKameitenCd, strYuusenTyousakaisyaCd, EarthEnum.EnumKameitenTyousakaisyaKensakuType.YUUSENTYOUSAKAISYA)
                    '取得した調査会社のSDS保持の取得
                    intYuusenSdsHojiInfo = tyousakaisyaSearchLogic.GetTyousaKaisyaSDS(strYuusenTyousakaisyaCd, "", False)

                    If intSiteiSdsHojiInfo = 1 Then
                        '指定調査会社の調査会社マスタ.SDS保持情報=1の場合
                        strTysGaisyaCd = strSiteiTyousakaisyaCd
                    ElseIf intYuusenSdsHojiInfo = 1 Then
                        '上記以外でかつ、優先調査会社の調査会社マスタ.SDS保持情報=1の場合
                        strTysGaisyaCd = strYuusenTyousakaisyaCd
                    Else
                        '上記以外の場合
                        strTysGaisyaCd = EarthConst.SDS_TYOSA_KAISYA_CD
                    End If
                    blnRet = True
                End If
            End If
        End If

        Return blnRet
    End Function

End Class
