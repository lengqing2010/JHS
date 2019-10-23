Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 地盤共通処理クラス
''' </summary>
''' <remarks></remarks>
Public Class JibanLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Private mLogic As New MessageLogic

    'UtilitiesのStringLogicクラス
    Private sLogic As New StringLogic
    '加盟店検索Logicクラス
    Private ksLogic As New KameitenSearchLogic

    ' データアクセスクラス
    Private dataAccess As New JibanDataAccess

    Private cbLogic As New CommonBizLogic

#Region "保証書Noの採番値取得"
    ''' <summary>
    ''' 保証書Noを採番し結果を取得します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="kubun">区分</param>
    ''' <param name="intRentouBukkenSuu">連棟物件数</param>
    ''' <param name="strLoginUserId">ログインユーザID</param>
    ''' <returns>採番後の保証書No　SPACE：保証書NO年月未設定</returns>
    ''' <remarks>区分をKeyに保証書Noの採番を行い値を返却します</remarks>
    Public Function GetNewHosyousyoNo( _
                                        ByVal sender As Object, _
                                        ByRef kubun As String, _
                                        ByRef intRentouBukkenSuu As Integer, _
                                        ByVal strLoginUserId As String _
                                        ) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNewHosyousyoNo", _
                                            sender, _
                                            kubun, _
                                            intRentouBukkenSuu, _
                                            strLoginUserId _
                                            )

        ' 戻り値．保証書No
        Dim hosyousyoNo As String = ""
        ' 保証書No年月(YYYY/MM)
        Dim hosyousyoNoYm As String = ""
        ' 保証書No最終番号(DB登録用)
        Dim hosyousyoLastNo As Integer
        ' 保証書関連のデータアクセスクラスを生成
        Dim dataAccess As HosyousyoDataAccess = New HosyousyoDataAccess
        ' DBへの反映結果
        Dim intResult As Integer

        ' 保証書No年月を取得
        hosyousyoNoYm = dataAccess.GetHosyousyoNoYM(kubun)

        ' 取得に失敗した場合、空白値を返却
        If hosyousyoNoYm = "" Then
            Return hosyousyoNo
        End If

        If intRentouBukkenSuu <= 0 Then
            intRentouBukkenSuu = 1
        End If

        ' 最終保証書Noを取得
        hosyousyoLastNo = dataAccess.GetHosyousyoLastNo(kubun, hosyousyoNoYm.Replace("/", ""))

        ' 取得できなかった場合は新規採番
        If hosyousyoLastNo = -1 Then
            '番号を振り直す(連棟物件数)
            hosyousyoLastNo = intRentouBukkenSuu

            ' TBL_保証書NO採番に新規登録する
            intResult = dataAccess.UpdateHosyousyoNo(kubun, _
                                                      hosyousyoNoYm.Replace("/", ""), _
                                                      hosyousyoLastNo, _
                                                      "N", _
                                                      strLoginUserId)
        Else

            '最終番号 = 最終番号 + 連棟物件数
            hosyousyoLastNo += intRentouBukkenSuu

            ' 9999を超えた場合、番号を振り直す(連棟物件数)
            If hosyousyoLastNo > 9999 Then
                hosyousyoLastNo = intRentouBukkenSuu
            End If

            ' TBL_保証書NO採番を更新する
            intResult = dataAccess.UpdateHosyousyoNo(kubun, _
                                                      hosyousyoNoYm.Replace("/", ""), _
                                                      hosyousyoLastNo, _
                                                      "U", _
                                                      strLoginUserId)

        End If

        ' 更新に失敗した場合
        If intResult <= 0 Then
            hosyousyoNo = ""
            Return hosyousyoNo
        End If

        ' 保証書NOの編集 [ 保証書NO年月+連番(0001～9999) ] =>連棟物件の頭数を返却
        hosyousyoNo = hosyousyoNoYm.Replace("/", "") + Format(hosyousyoLastNo - intRentouBukkenSuu + 1, "0000")

        Return hosyousyoNo

    End Function

#End Region

#Region "商品設定場所情報の取得（商品コード１）"

    ''' <summary>
    ''' 商品コード1の情報を取得します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="hin1InfoRec">商品コード1設定用パラメータレコード</param>
    ''' <param name="hin1AutoSetRecord">商品コード1の自動設定データレコード</param>
    ''' <param name="intSetSts">取得セットステータス[0:デフォルト,1:工務店額セットエラー,2:実請求額セットエラー]</param>
    ''' <returns>処理結果 True:取得成功 False:取得失敗</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhin1Info(ByRef sender As Object, _
                                    ByVal hin1InfoRec As Syouhin1InfoRecord, _
                                    ByRef hin1AutoSetRecord As Syouhin1AutoSetRecord, _
                                    Optional ByRef intSetSts As Integer = 0) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin1Info", _
                                            sender, _
                                            hin1InfoRec, _
                                            hin1AutoSetRecord, _
                                            intSetSts)


        '**********************************************************************
        ' 調査概要を取得します（商品価格設定マスタ）
        '**********************************************************************
        Dim kakakuSetteiRec As New KakakuSetteiRecord
        kakakuSetteiRec.SyouhinKbn = hin1InfoRec.SyouhinKbn          ' 商品区分
        kakakuSetteiRec.TyousaHouhouNo = hin1InfoRec.TyousaHouhouNo  ' 調査方法
        kakakuSetteiRec.SyouhinCd = hin1InfoRec.SyouhinCd            ' 商品コード

        ' 価格設定レコードを参照渡しして調査概要と設定場所を取得する
        Dim syouhinAccess As New SyouhinDataAccess
        syouhinAccess.GetKakakuSetteiInfo(kakakuSetteiRec)

        ' 調査概要が取得できなかった場合
        If kakakuSetteiRec.TyousaGaiyou = 0 Then
            intSetSts = EarthEnum.emSyouhin1Error.GetTysGaiyou
            kakakuSetteiRec.TyousaGaiyou = 9
            'Return False
        End If

        '**********************************************************************
        ' 商品の詳細情報を取得します
        '**********************************************************************
        Dim tmpHin1AutoSetRecord As New Syouhin1AutoSetRecord
        Dim drList As List(Of Syouhin1AutoSetRecord)
        Dim dtResult As DataTable

        ' 商品情報を取得
        dtResult = syouhinAccess.GetSyouhinInfo(kakakuSetteiRec.SyouhinCd, hin1InfoRec.KameitenCd)
        'Listに格納
        drList = DataMappingHelper.Instance.getMapArray(Of Syouhin1AutoSetRecord)(GetType(Syouhin1AutoSetRecord), dtResult)

        ' 取得した情報をチェック(取得できない場合、処理終了)
        If drList.Count <= 0 Then
            intSetSts = EarthEnum.emSyouhin1Error.GetSyouhin1
            Return False
        Else
            tmpHin1AutoSetRecord = drList(0)
        End If

        '**********************************************************************
        ' 原価マスタの詳細情報を取得します
        '**********************************************************************
        Dim blnGenkaSyutoku As Boolean

        blnGenkaSyutoku = GetSyoudakusyoKingaku1(hin1InfoRec.SyouhinCd, _
                                                hin1InfoRec.Kubun, _
                                                hin1InfoRec.TyousaHouhouNo, _
                                                hin1InfoRec.TyousaGaiyou, _
                                                hin1InfoRec.DoujiIraiTousuu, _
                                                hin1InfoRec.TysKaisyaCd, _
                                                hin1InfoRec.KameitenCd, _
                                                0, _
                                                hin1InfoRec.KeiretuCd, _
                                                blnGenkaSyutoku)

        If blnGenkaSyutoku = False Then
            '原価マスタからのみ取得出来ない場合
            intSetSts = EarthEnum.emSyouhin1Error.GetGenka
        End If

        '**********************************************************************
        ' 販売価格マスタの詳細情報を取得します
        '**********************************************************************
        Dim blnHanbaiSyutoku As Boolean
        blnHanbaiSyutoku = GetHanbaiKingaku1(hin1InfoRec, hin1AutoSetRecord)

        If blnHanbaiSyutoku = False Then
            If blnGenkaSyutoku = True Then
                '販売価格マスタからのみ取得不可
                intSetSts = EarthEnum.emSyouhin1Error.GetHanbai
            Else
                '原価・販売価格マスタの両方取得出来ない場合
                intSetSts = EarthEnum.emSyouhin1Error.GetGenkaHanbai
            End If
        End If

        ' 取得した商品情報を戻り値にセット
        hin1AutoSetRecord.SyouhinCd = kakakuSetteiRec.SyouhinCd                                     ' 商品コード
        hin1AutoSetRecord.TyousaGaiyou = kakakuSetteiRec.TyousaGaiyou                               ' 調査概要
        hin1AutoSetRecord.KakakuSettei = kakakuSetteiRec.KakakuSettei                               ' 価格設定場所
        hin1AutoSetRecord.SyouhinNm = tmpHin1AutoSetRecord.SyouhinNm                                ' 商品名
        hin1AutoSetRecord.ZeiKbn = tmpHin1AutoSetRecord.ZeiKbn                                      ' 税区分
        hin1AutoSetRecord.Zeiritu = tmpHin1AutoSetRecord.Zeiritu                                    ' 税率
        hin1AutoSetRecord.TaxUri = Fix(hin1AutoSetRecord.JituGaku * tmpHin1AutoSetRecord.Zeiritu)   ' 消費税額
        hin1AutoSetRecord.KameitenCd = hin1InfoRec.KameitenCd                                       ' 加盟店コード
        '請求先が個別に指定されていない場合、デフォルトの請求先をセット
        If hin1AutoSetRecord.SeikyuuSakiCd = String.Empty Then
            hin1AutoSetRecord.SeikyuuSakiCd = tmpHin1AutoSetRecord.SeikyuuSakiCd                        ' 請求先コード
            hin1AutoSetRecord.SeikyuuSakiBrc = tmpHin1AutoSetRecord.SeikyuuSakiBrc                      ' 請求先枝番
            hin1AutoSetRecord.SeikyuuSakiKbn = tmpHin1AutoSetRecord.SeikyuuSakiKbn                      ' 請求先区分
        End If
        '請求先タイプをセット
        hin1InfoRec.Seikyuusaki = hin1AutoSetRecord.SeikyuuSakiType

        Return True

    End Function

    ''' <summary>
    ''' 価格設定場所を取得します
    ''' </summary>
    ''' <param name="syouhinKbn">商品区分</param>
    ''' <param name="tyousaHouhou">調査方法</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>価格設定場所をString形式（画面項目にそのままセットする）</returns>
    ''' <remarks></remarks>
    Public Function GetKakakuSetteiBasyo(ByVal syouhinKbn As Integer, _
                                         ByVal tyousaHouhou As Integer, _
                                         ByVal strSyouhinCd As String) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKakakuSetteiBasyo", _
                                            syouhinKbn, _
                                            tyousaHouhou, _
                                            strSyouhinCd)

        '**********************************************************************
        ' 価格設定情報を取得します
        '**********************************************************************
        Dim kakakuSetteiRec As New KakakuSetteiRecord
        kakakuSetteiRec.SyouhinKbn = syouhinKbn            ' 商品区分
        kakakuSetteiRec.TyousaHouhouNo = tyousaHouhou      ' 調査方法
        kakakuSetteiRec.SyouhinCd = strSyouhinCd           '商品コード

        ' 価格設定レコードを参照渡しして商品コードと設定場所を取得する
        Dim syouhinAccess As New SyouhinDataAccess
        syouhinAccess.GetKakakuSetteiInfo(kakakuSetteiRec)

        ' 一つでも値が取得できなかった場合、処理終了
        If kakakuSetteiRec.SyouhinCd = Nothing Or _
           kakakuSetteiRec.KakakuSettei = Nothing Then
            Return ""
        Else
            Return kakakuSetteiRec.KakakuSettei.ToString()
        End If
    End Function


    ''' <summary>
    ''' 商品価格調査概要を取得します
    ''' </summary>
    ''' <param name="recKakakuSettei"></param>
    ''' <remarks></remarks>
    Public Function GetTysGaiyou(ByRef recKakakuSettei As KakakuSetteiRecord) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin1Info", _
                                            recKakakuSettei)

        '調査概要を取得（商品価格設定マスタ）
        Dim syouhinAccess As New SyouhinDataAccess
        syouhinAccess.GetKakakuSetteiInfo(recKakakuSettei)

        ' 調査概要が取得できなかった場合
        If recKakakuSettei.TyousaGaiyou = 0 Then
            recKakakuSettei.TyousaGaiyou = 9
            Return False
        End If

        Return True
    End Function

#Region "加盟店M価格取得"
    ''' <summary>
    ''' 加盟店テーブルより価格を取得します
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="intTatemonoYouto">建物用途NO</param>
    ''' <param name="isYoutoAdd">建物用途による加算対象(True:加算する)</param>
    ''' <param name="strItem">取得対象項目名</param>
    ''' <param name="blnDelete">True:取消データを除外 False:取消データを対象</param>
    ''' <param name="kameitenKakaku">検索結果</param>
    ''' <returns>True:取得成功 False:取得失敗</returns>
    ''' <remarks></remarks>
    Function GetKameitenInfo(ByVal strKubun As String, _
                             ByVal strKameitenCd As String, _
                             ByVal intTatemonoYouto As Integer, _
                             ByVal isYoutoAdd As Boolean, _
                             ByVal strItem As String, _
                             ByVal blnDelete As Boolean, _
                             ByRef kameitenKakaku As Decimal) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenInfo", _
                                            strKubun, _
                                            strKameitenCd, _
                                            intTatemonoYouto, _
                                            isYoutoAdd, _
                                            strItem, _
                                            blnDelete, _
                                            kameitenKakaku)

        ' 加盟店コード未設定時は処理終了
        If strKameitenCd = "" Then
            Return False
        End If

        Dim kameitenAccess As New KameitenDataAccess

        If kameitenAccess.GetKameitenKakaku(strKubun, _
                                             strKameitenCd, _
                                             intTatemonoYouto, _
                                             isYoutoAdd, _
                                             strItem, _
                                             blnDelete, _
                                             kameitenKakaku) = True Then
            Return True
        End If

        Return False

    End Function
#End Region

#Region "請求金額取得"
    ''' <summary>
    ''' 他請求/3系列時の工務店請求金額、実請求金額の取得
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="intMode">取得モード<BR/>
    ''' 1 (商品コード1の加盟店向価格)   <BR/>
    ''' 2 (解約払戻の加盟店向価格)      <BR/>
    ''' 3 (本部(TH)向価格)              <BR/>
    ''' 4 (実請求金額 / 掛率)           <BR/>
    ''' 5 (工務店請求金額 * 掛率)       <BR/>
    ''' 6 (加盟店向価格→実請求金額/掛率)</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intKingaku">TH請求用価格マスタのKEY,掛率計算時の金額<BR/>
    ''' 取得モード=1 (TH請求用価格マスタのKEY)<BR/>
    ''' 取得モード=4 (実請求金額)<BR/>
    ''' 取得モード=5 (工務店請求金額)</param>
    ''' <param name="intReturnKingaku">取得金額（戻り値）</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="intTatemonoYouto">建物用途NO</param>
    ''' <param name="isYoutoAdd">建物用途による加算を行うか True:行う False:行わない</param>
    ''' <returns>True:取得OK,False:取得NG</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuGaku(ByRef sender As Object, _
                                       ByVal intMode As Integer, _
                                       ByVal strKeiretuCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal intKingaku As Integer, _
                                       ByRef intReturnKingaku As Integer, _
                                       Optional ByVal strKameitenCd As String = "", _
                                       Optional ByVal intTatemonoYouto As Integer = 1, _
                                       Optional ByVal isYoutoAdd As Boolean = False) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuGaku", _
                                            sender, _
                                            intMode, _
                                            strKeiretuCd, _
                                            strSyouhinCd, _
                                            intKingaku, _
                                            intReturnKingaku, _
                                            strKameitenCd, _
                                            intTatemonoYouto, _
                                            isYoutoAdd)

        ' 系列コード又は商品コードが空白の場合、処理終了
        If strKeiretuCd.Trim() = "" Or strSyouhinCd.Trim() = "" Then
            Return False
        End If

        Dim keiretu As IKeiretuDataAccess


        Dim dataAccess As New JibanDataAccess
        Dim thKasangaku As Integer = 0

        ' 系列による処理分岐
        Select Case strKeiretuCd
            Case EarthConst.KEIRETU_AIFURU
                ' 系列_アイフルホームのインスタンスを生成
                keiretu = New KeiretuAifuruDataAccess
            Case EarthConst.KEIRETU_TH
                ' 系列_TH友の会のインスタンスを生成
                keiretu = New KeiretuThDataAccess

                If isYoutoAdd = True Then
                    ' TH請求の場合は建物用途加算額を事前取得し、実請求額から差し引く(価格がKeyとなる為)
                    thKasangaku = dataAccess.GetTatemonoYoutoKasangaku(strKameitenCd, intTatemonoYouto)
                    intKingaku = intKingaku - thKasangaku
                End If
            Case EarthConst.KEIRETU_WANDA
                ' 系列_ワンダーホームのインスタンスを生成
                keiretu = New KeiretuWandaDataAccess
            Case Else
                Return False
        End Select


        ' 請求金額を取得する
        Dim retCd As Integer = keiretu.getSeikyuKingaku(intMode, _
                                                         strSyouhinCd.Trim, _
                                                         intKingaku, _
                                                         intReturnKingaku)
        ' 戻り値を判定
        Select Case retCd
            Case 1
                ' 取得した金額が０以外で取得モードが商品１の工務店請求額で建物用途加算対象に限り
                ' 建物用途加算額マスタの加算額をセットする
                If intReturnKingaku <> 0 And _
                   intMode = 1 And _
                   isYoutoAdd = True Then

                    Dim kasangaku As Integer = 0
                    ' 加算額を取得する
                    kasangaku = dataAccess.GetTatemonoYoutoKasangaku(strKameitenCd, intTatemonoYouto)

                    ' 取得した加算額を加算
                    intReturnKingaku = intReturnKingaku + kasangaku
                End If
                Return True
            Case 0
                If TypeOf sender Is String Then
                    sender = Messages.MSG001E
                Else
                    ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "alert", "alert('" & Messages.MSG001E & "')", True)
                End If
                Return False
            Case -1
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' 他請求/3系列時の工務店請求金額、実請求金額の取得(品質保証書検索画面用コピー)
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="intMode">取得モード<BR/>
    ''' 1 (商品コード1の加盟店向価格)   <BR/>
    ''' 2 (解約払戻の加盟店向価格)      <BR/>
    ''' 3 (本部(TH)向価格)              <BR/>
    ''' 4 (実請求金額 / 掛率)           <BR/>
    ''' 5 (工務店請求金額 * 掛率)       <BR/>
    ''' 6 (加盟店向価格→実請求金額/掛率)</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intKingaku">TH請求用価格マスタのKEY,掛率計算時の金額<BR/>
    ''' 取得モード=1 (TH請求用価格マスタのKEY)<BR/>
    ''' 取得モード=4 (実請求金額)<BR/>
    ''' 取得モード=5 (工務店請求金額)</param>
    ''' <param name="intReturnKingaku">取得金額（戻り値）</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="intTatemonoYouto">建物用途NO</param>
    ''' <param name="isYoutoAdd">建物用途による加算を行うか True:行う False:行わない</param>
    ''' <returns>True:取得OK,False:取得NG</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuGakuHinsitu(ByRef sender As Object, _
                                       ByVal intMode As Integer, _
                                       ByVal strKeiretuCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal intKingaku As Integer, _
                                       ByRef intReturnKingaku As Integer, _
                                       Optional ByVal strKameitenCd As String = "", _
                                       Optional ByVal intTatemonoYouto As Integer = 1, _
                                       Optional ByVal isYoutoAdd As Boolean = False) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuGaku", _
                                            sender, _
                                            intMode, _
                                            strKeiretuCd, _
                                            strSyouhinCd, _
                                            intKingaku, _
                                            intReturnKingaku, _
                                            strKameitenCd, _
                                            intTatemonoYouto, _
                                            isYoutoAdd)

        ' 系列コード又は商品コードが空白の場合、処理終了
        If strKeiretuCd.Trim() = "" Or strSyouhinCd.Trim() = "" Then
            Return False
        End If

        Dim keiretu As IKeiretuDataAccess


        Dim dataAccess As New JibanDataAccess
        Dim thKasangaku As Integer = 0

        ' 系列による処理分岐
        Select Case strKeiretuCd
            Case EarthConst.KEIRETU_AIFURU
                ' 系列_アイフルホームのインスタンスを生成
                keiretu = New KeiretuAifuruDataAccess
            Case EarthConst.KEIRETU_TH
                ' 系列_TH友の会のインスタンスを生成
                keiretu = New KeiretuThDataAccess

                If isYoutoAdd = True Then
                    ' TH請求の場合は建物用途加算額を事前取得し、実請求額から差し引く(価格がKeyとなる為)
                    thKasangaku = dataAccess.GetTatemonoYoutoKasangaku(strKameitenCd, intTatemonoYouto)
                    intKingaku = intKingaku - thKasangaku
                End If
            Case EarthConst.KEIRETU_WANDA
                ' 系列_ワンダーホームのインスタンスを生成
                keiretu = New KeiretuWandaDataAccess
            Case Else
                Return False
        End Select


        ' 請求金額を取得する
        Dim retCd As Integer = keiretu.getSeikyuKingaku(intMode, _
                                                         strSyouhinCd.Trim, _
                                                         intKingaku, _
                                                         intReturnKingaku)
        ' 戻り値を判定
        Select Case retCd
            Case 1
                ' 取得した金額が０以外で取得モードが商品１の工務店請求額で建物用途加算対象に限り
                ' 建物用途加算額マスタの加算額をセットする
                If intReturnKingaku <> 0 And _
                   intMode = 1 And _
                   isYoutoAdd = True Then

                    Dim kasangaku As Integer = 0
                    ' 加算額を取得する
                    kasangaku = dataAccess.GetTatemonoYoutoKasangaku(strKameitenCd, intTatemonoYouto)

                    ' 取得した加算額を加算
                    intReturnKingaku = intReturnKingaku + kasangaku
                End If
                Return True
            Case 0
                If TypeOf sender Is String Then
                    sender = Messages.MSG001E
                Else
                    'ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "alert", "alert('" & Messages.MSG001E & "')", True)
                End If
                Return False
            Case -1
                Return False
        End Select

        Return True
    End Function

#End Region

#End Region

#Region "重複物件チェック"

    ''' <summary>
    ''' 施主名の重複物件チェック
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strSesyu">施主名</param>
    ''' <returns>True:重複データ有り</returns>
    ''' <remarks>区分、施主名の重複チェックを行い結果を返却する。<br/>
    '''          保証書NOの年月が当月を含む過去６ヶ月以内のデータより<br/>
    '''          区分、施主名の重複するレコードを検索する（データ破棄含む）</remarks>
    Public Function ChkTyouhuku(ByVal strKubun As String, _
                                ByVal strHosyousyoNo As String, _
                                ByVal strSesyu As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkTyouhuku", _
                                            strKubun, _
                                            strHosyousyoNo, _
                                            strSesyu)

        ' 重複データ取得クラス
        Dim dataAccess As New TyoufukuDataAccess

        Dim table As TyoufukuDataSet.TyoufukuTableDataTable = GetTyouhuku(strKubun, _
                                                                          strHosyousyoNo, _
                                                                          " z.sesyu_mei COLLATE Japanese_CS_AS_KS_WS ", _
                                                                          strSesyu.Trim(), _
                                                                          "", _
                                                                          "")
        ' データが存在する場合、重複有り
        If table.Count <> 0 Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 住所の重複物件チェック
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strJyuusyo1">住所１</param>
    ''' <param name="strJyuusyo2">住所２</param>
    ''' <returns>True:重複データ有り</returns>
    ''' <remarks>区分、住所１，２の重複チェックを行い結果を返却する。<br/>
    '''          保証書NOの年月が当月を含む過去６ヶ月以内のデータより<br/>
    '''          区分、住所１，２の重複するレコードを検索する（データ破棄含む）</remarks>
    Public Function ChkTyouhuku(ByVal strKubun As String, _
                                ByVal strHosyousyoNo As String, _
                                ByVal strJyuusyo1 As String, _
                                ByVal strJyuusyo2 As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkTyouhuku", _
                                            strKubun, _
                                            strHosyousyoNo, _
                                            strJyuusyo1, _
                                            strJyuusyo2)

        ' 重複データ取得クラス
        Dim dataAccess As New TyoufukuDataAccess

        Dim table As TyoufukuDataSet.TyoufukuTableDataTable = GetTyouhuku(strKubun, _
                                                                          strHosyousyoNo, _
                                                                          " ISNULL(z.bukken_jyuusyo1,'') + ISNULL(z.bukken_jyuusyo2,'') COLLATE Japanese_CS_AS_KS_WS ", _
                                                                          strJyuusyo1.Trim() & strJyuusyo2.Trim(), _
                                                                          "", _
                                                                          "")

        ' データが存在する場合、重複有り
        If table.Count <> 0 Then
            Return True
        End If
        Return False

    End Function
#End Region

#Region "重複物件取得"
    ''' <summary>
    ''' 重複データ取得
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strHosyousyoNo">表示中の保証書NO</param>
    ''' <param name="searchItem1">検索条件項目名１（テーブルカラム）</param>
    ''' <param name="condition1">検索条件１</param>
    ''' <param name="searchItem2">検索条件項目名２（テーブルカラム）</param>
    ''' <param name="condition2">検索条件２</param>
    ''' <returns>重複データセットの重複データテーブル</returns>
    ''' <remarks>重複データの検索を行います<br/><br/>
    ''' 
    '''     検索方法１．施主名のみ指定する<br/><br/>
    ''' 
    '''         検索条件項目名１に "Z.sesyu_mei" 検索条件１に "○× △男" <br/>
    '''         検索条件項目名２、検索条件２を何れも空白でCallした場合、<br/>
    '''         施主名のみ重複するデータを検索します<br/><br/>
    ''' 
    '''     検索方法２．物件住所のみ指定する<br/><br/>
    ''' 
    '''         検索条件項目名１に "Z.bukken_jyuusyo1 ＋ Z.bukken_jyuusyo2" 検索条件１に 住所1と2を結合した値 <br/>
    '''         検索条件項目名２、検索条件２を何れも空白でCallした場合、<br/>
    '''         物件住所のみ重複するデータを検索します
    '''
    '''     検索方法３．施主名及び物件住所を指定する<br/><br/>
    ''' 
    '''         検索条件項目名１に "Z.sesyu_mei" 検索条件１に "○× △男" <br/>
    '''         検索条件項目名２に "Z.bukken_jyuusyo1 ＋ Z.bukken_jyuusyo2" 検索条件２に 住所1と2を結合した値<br/>
    '''         施主名又は物件住所で重複するデータを検索します
    ''' </remarks>
    Public Function GetTyouhuku(ByVal strKubun As String, _
                                ByVal strHosyousyoNo As String, _
                                ByVal searchItem1 As String, _
                                ByVal condition1 As String, _
                                ByVal searchItem2 As String, _
                                ByVal condition2 As String) As TyoufukuDataSet.TyoufukuTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyouhuku", _
                                            strKubun, _
                                            strHosyousyoNo, _
                                            searchItem1, _
                                            condition1, _
                                            searchItem2, _
                                            condition2)

        ' 重複データ取得クラス
        Dim dataAccess As New TyoufukuDataAccess

        Return dataAccess.GetDataBy(strKubun, strHosyousyoNo, searchItem1, condition1, searchItem2, condition2)

    End Function

    ''' <summary>
    ''' 重複データを取得します
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strHosyousyoNo">表示中の保証書NO</param>
    ''' <param name="sesyumei">施主名</param>
    ''' <param name="juusyo1">物件住所１</param>
    ''' <param name="juusyo2">物件住所２</param>
    ''' <returns>重複データのレコードリスト</returns>
    ''' <remarks>施主名または物件住所に重複データが存在する場合、<br/>
    '''          該当データを配列で返却します</remarks>
    Public Function GetTyouhukuRecords(ByVal strKubun As String, _
                                       ByVal strHosyousyoNo As String, _
                                       ByVal sesyumei As String, _
                                       ByVal juusyo1 As String, _
                                       ByVal juusyo2 As String) As List(Of TyoufukuRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyouhukuRecords", _
                                            strKubun, _
                                            strHosyousyoNo, _
                                            sesyumei, _
                                            juusyo1, _
                                            juusyo2)

        ' 戻り値となるリスト
        Dim returnRec As New List(Of TyoufukuRecord)

        ' 重複データ取得クラス
        Dim dataAccess As New TyoufukuDataAccess

        Dim jParam1 As String = ""
        Dim jParam2 As String = ""
        Dim jParam3 As String = ""
        Dim jParam4 As String = ""

        ' 施主名が未設定の場合は重複ではないので条件から除外する
        If sesyumei.Trim() <> "" Then
            jParam1 = " z.sesyu_mei COLLATE Japanese_CS_AS_KS_WS  "
            jParam2 = sesyumei.Trim()
        End If

        ' 住所情報が未設定の場合は重複ではないので条件から除外する
        If juusyo1.Trim() & juusyo2.Trim() <> "" Then
            If jParam1 = "" Then
                jParam1 = " (ISNULL(z.bukken_jyuusyo1,'') + ISNULL(z.bukken_jyuusyo2,'')) COLLATE Japanese_CS_AS_KS_WS "
                jParam2 = juusyo1.Trim() & juusyo2.Trim()
            Else
                jParam3 = " (ISNULL(z.bukken_jyuusyo1,'') + ISNULL(z.bukken_jyuusyo2,'')) COLLATE Japanese_CS_AS_KS_WS "
                jParam4 = juusyo1.Trim() & juusyo2.Trim()
            End If
        End If

        ' 値が取得できた場合、戻り値に設定する
        For Each row As TyoufukuDataSet.TyoufukuTableRow In dataAccess.GetDataBy(strKubun, _
                                                                                  strHosyousyoNo, _
                                                                                  jParam1, _
                                                                                  jParam2, _
                                                                                  jParam3, _
                                                                                  jParam4)
            Dim tyoufukuRec As New TyoufukuRecord

            tyoufukuRec.HakiSyubetu = row.haki_syubetu
            tyoufukuRec.Kubun = row.kbn
            tyoufukuRec.HosyousyoNo = row.hosyousyo_no
            tyoufukuRec.Sesyumei = row.sesyu_mei
            tyoufukuRec.Jyuusyo1 = row.bukken_jyuusyo1
            tyoufukuRec.Jyuusyo2 = row.bukken_jyuusyo2
            tyoufukuRec.KameitenNm = row.kameiten_mei1
            tyoufukuRec.Bikou = row.bikou
            ' リストにセット
            returnRec.Add(tyoufukuRec)

        Next

        Return returnRec

    End Function

#End Region

#Region "商品コード１金額取得"

    ''' <summary>
    ''' 商品コード１の承諾書金額設定
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード1（必須）</param>
    ''' <param name="strKubun">区分</param>
    ''' <param name="intTyousaHouhou">調査方法</param>
    ''' <param name="intTyousaGaiyou">調査概要</param>
    ''' <param name="intDoujiIraiSuu">同時依頼棟数が</param>
    ''' <param name="strTyousakaisyaCd">調査会社ｺｰﾄﾞ+事業所ｺｰﾄﾞ（必須）</param>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ（必須）</param>
    ''' <param name="intSyoudakuKingaku">承諾金額（取得用参照引数）</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="blnHenkouKahiFlg">承諾書金額変更可否FLG（取得用参照引数）</param>
    ''' <returns>true:取得成功 false:取得失敗</returns>
    ''' <remarks>上記引数の説明にある条件を満たさない場合、戻り値にFalseが返り、<br/>
    '''          承諾書金額は設定されません<br/><br/>
    ''' </remarks>
    Public Function GetSyoudakusyoKingaku1(ByVal strSyouhinCd As String, _
                                           ByVal strKubun As String, _
                                           ByVal intTyousaHouhou As Integer, _
                                           ByVal intTyousaGaiyou As Integer, _
                                           ByVal intDoujiIraiSuu As Integer, _
                                           ByVal strTyousakaisyaCd As String, _
                                           ByVal strKameitenCd As String, _
                                           ByRef intSyoudakuKingaku As Integer, _
                                           ByVal strKeiretuCd As String, _
                                           ByRef blnHenkouKahiFlg As Boolean) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyoudakusyoKingaku1", _
                                            strSyouhinCd, _
                                            strKubun, _
                                            intTyousaHouhou, _
                                            intTyousaGaiyou, _
                                            intDoujiIraiSuu, _
                                            strTyousakaisyaCd, _
                                            strKameitenCd, _
                                            intSyoudakuKingaku, _
                                            strKeiretuCd, _
                                            blnHenkouKahiFlg)

        '****************************************************
        ' 引数のチェック（一つでもNGの場合Falseで処理終了）
        '****************************************************

        '同時依頼棟数がなしor0は1の扱い
        If intDoujiIraiSuu = Integer.MinValue Or IsDBNull(intDoujiIraiSuu) Or intDoujiIraiSuu < 1 Then
            intDoujiIraiSuu = 1
        End If

        '商品コード1存在チェック（存在しない場合は算出しない）
        If strSyouhinCd = Nothing OrElse strSyouhinCd = String.Empty Then
            ' 未設定の場合、処理終了
            Return False
        End If

        '●例外1 調査会社コードチェック
        If strTyousakaisyaCd <> Nothing AndAlso strTyousakaisyaCd <> String.Empty Then
            If strTyousakaisyaCd.Trim() = EarthConst.KARI_TYOSA_KAISYA_CD Then
                '調査会社未決定の場合0円・変更不可
                intSyoudakuKingaku = 0
                blnHenkouKahiFlg = False
                Return True
            End If
        End If

        '●例外2 調査方法のチェック
        '未決定の場合0円・変更不可
        If intTyousaHouhou = EarthConst.KARI_TYOUSA_HOUHOU_CD Then
            intSyoudakuKingaku = 0
            blnHenkouKahiFlg = False
            Return True
        End If

        '●例外4 区分マスタ・原価マスタ非参照フラグを判断し、非参照の場合0円・変更不可
        Dim recKubun As New KubunRecord
        recKubun = getKubunRecord(strKubun)
        If recKubun.GenkaMasterHisansyouFlg = 1 Then
            intSyoudakuKingaku = 0
            blnHenkouKahiFlg = False
            Return True
        End If

        '●例外3 調査方法マスタ・原価設定不要フラグを判断し、不要の場合0円・変更可
        Dim recTysHouhou As New TyousahouhouRecord
        recTysHouhou = getTyousahouhouRecord(intTyousaHouhou)
        If recTysHouhou.GenkaSetteiFuyouFlg >= 1 Then
            intSyoudakuKingaku = 0
            blnHenkouKahiFlg = True
            Return True
        End If

        '○原価マスタ取得用の必須チェック
        '調査会社コードチェック
        If strTyousakaisyaCd = Nothing OrElse strTyousakaisyaCd = String.Empty Then
            '未設定の場合、処理終了
            Return False
        End If

        '新 承諾書金額の取得
        Dim daGenka As New GenkaDataAccess

        'JIO先FLGの取得
        Dim ksRec As KameitenSearchRecord = ksLogic.GetKameitenSearchResult(strKubun, strKameitenCd, strTyousakaisyaCd, False)
        Dim strJioSakiFlg As String = IIf(ksRec.JioSakiFLG = Integer.MinValue, String.Empty, CStr(ksRec.JioSakiFLG))

        '「加盟店」の原価算出
        If daGenka.GetSyoudakuKingaku(strTyousakaisyaCd, _
                                      EarthConst.AITESAKI_KAMEITEN, _
                                      strKameitenCd, _
                                      strSyouhinCd, _
                                      intTyousaHouhou, _
                                      intDoujiIraiSuu, _
                                      intSyoudakuKingaku, _
                                      blnHenkouKahiFlg, _
                                      True) = True Then
            Return True
        End If

        '加盟店M.JIO先FLG = 1の場合
        If strJioSakiFlg = EarthConst.ARI_VAL Then
            '「JIO」の原価算出
            If daGenka.GetSyoudakuKingaku(strTyousakaisyaCd, _
                                          EarthConst.AITESAKI_JIO_SAKI_FLG, _
                                          EarthConst.AITESAKI_JIO_SAKI_CD, _
                                          strSyouhinCd, _
                                          intTyousaHouhou, _
                                          intDoujiIraiSuu, _
                                          intSyoudakuKingaku, _
                                          blnHenkouKahiFlg, _
                                          True) = True Then
                Return True
            End If
        End If

        '「系列」の原価算出方法
        If daGenka.GetSyoudakuKingaku(strTyousakaisyaCd, _
                              EarthConst.AITESAKI_KEIRETU, _
                              strKeiretuCd, _
                              strSyouhinCd, _
                              intTyousaHouhou, _
                              intDoujiIraiSuu, _
                              intSyoudakuKingaku, _
                              blnHenkouKahiFlg, _
                              True) = True Then
            Return True
        End If

        '「指定無」の原価算出方法
        If daGenka.GetSyoudakuKingaku(strTyousakaisyaCd, _
                              EarthConst.AITESAKI_NASI, _
                              EarthConst.AITESAKI_NASI_CD, _
                              strSyouhinCd, _
                              intTyousaHouhou, _
                              intDoujiIraiSuu, _
                              intSyoudakuKingaku, _
                              blnHenkouKahiFlg, _
                              True) = True Then
            Return True
        End If

        ' 旧承諾書金額の取得
        'Dim dataAccess As New SyoudakusyoKingakuDataAccess
        'If dataAccess.GetSyoudakuKingaku(strTyousakaisyaCd, _
        '                                  strKameitenCd, _
        '                                  intDoujiIraiSuu, _
        '                                  intSyoudakuKingaku) = False Then

        '    Return False
        'End If



        ' 旧地盤ではこのタイミングで消費税額、税込金額の画面設定、
        ' 商品コード1,2の請求書発行日、売上年月日の連動を行っているので
        ' 画面処理側に必ず処理実装する必要あり

        Return False

    End Function

    ''' <summary>
    ''' 商品コード１の販売価格設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHanbaiKingaku1(ByVal hin1InfoRec As Syouhin1InfoRecord, _
                                      ByRef hin1AutoSetRecord As Syouhin1AutoSetRecord) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanbaiKingaku1", _
                                                    hin1InfoRec, _
                                                    hin1AutoSetRecord)

        '****************************************************
        ' 例外パターンのチェック（一つでもNGの場合Falseで処理終了）
        '****************************************************

        '商品コード1存在チェック
        If hin1InfoRec Is Nothing OrElse String.IsNullOrEmpty(hin1InfoRec.SyouhinCd) Then
            '未設定の場合処理終了
            Return False
        End If

        '事前に商品M.調査有無区分を調べる
        Dim recSyouhin As New SyouhinMeisaiRecord
        Dim blnTysUmKbn As Boolean = True          '調査有無区分（T：有効,F:無効）
        recSyouhin = GetSyouhinRecord(hin1InfoRec.SyouhinCd)
        If recSyouhin.TysUmuKbn <> 1 Then
            blnTysUmKbn = False
        End If
        '商品情報を設定
        hin1AutoSetRecord.ZeiKbn = recSyouhin.ZeiKbn            ' 税区分
        hin1AutoSetRecord.Zeiritu = recSyouhin.Zeiritu          ' 税率

        '●例外1 調査会社コードチェック
        If hin1InfoRec.TysKaisyaCd <> String.Empty Then
            If hin1InfoRec.TysKaisyaCd = EarthConst.KARI_TYOSA_KAISYA_CD Then
                '調査会社未決定の場合0円・変更不可
                hin1AutoSetRecord.KoumutenGaku = 0
                hin1AutoSetRecord.KoumutenGakuHenkouFlg = False
                hin1AutoSetRecord.JituGaku = 0
                hin1AutoSetRecord.JituGakuHenkouFlg = False
                Return True
            End If
        End If

        '●例外2 調査方法のチェック
        If hin1InfoRec.TyousaHouhouNo = EarthConst.KARI_TYOUSA_HOUHOU_CD Then
            '調査方法未決定の場合0円・変更不可
            hin1AutoSetRecord.KoumutenGaku = 0
            hin1AutoSetRecord.KoumutenGakuHenkouFlg = False
            hin1AutoSetRecord.JituGaku = 0
            hin1AutoSetRecord.JituGakuHenkouFlg = False
            Return True
        End If

        '●例外3 調査方法マスタ・価格設定不要フラグを判断し、不要の場合0円・変更可能
        Dim recTysHouhou As New TyousahouhouRecord
        If blnTysUmKbn Then
            '商品M.調査有無が無い場合は、価格設定不要フラグを見ない
            recTysHouhou = getTyousahouhouRecord(hin1InfoRec.TyousaHouhouNo)
            If recTysHouhou.KakakuSetteiFuyouFlg >= 1 Then
                hin1AutoSetRecord.KoumutenGaku = 0
                hin1AutoSetRecord.KoumutenGakuHenkouFlg = True
                hin1AutoSetRecord.JituGaku = 0
                hin1AutoSetRecord.JituGakuHenkouFlg = True
                Return True
            End If
        End If

        '****************************************************
        ' 販売価格マスタからの金額・変更FLG取得
        '****************************************************

        '●販売価格の取得
        Dim daHanbai As New HanbaiKakakuDataAccess

        '「加盟店」の販売価格算出
        If daHanbai.GetHanbaiKakaku(EarthConst.AITESAKI_KAMEITEN, _
                                    hin1InfoRec.KameitenCd, _
                                    hin1InfoRec.SyouhinCd, _
                                    hin1InfoRec.TyousaHouhouNo, _
                                    hin1AutoSetRecord.KoumutenGaku, _
                                    hin1AutoSetRecord.KoumutenGakuHenkouFlg, _
                                    hin1AutoSetRecord.JituGaku, _
                                    hin1AutoSetRecord.JituGakuHenkouFlg, _
                                    blnTysUmKbn, _
                                    True) Then
            Return True
        End If

        '「営業所」の販売価格算出
        If daHanbai.GetHanbaiKakaku(EarthConst.AITESAKI_EIGYOUSYO, _
                                    hin1InfoRec.EigyousyoCd, _
                                    hin1InfoRec.SyouhinCd, _
                                    hin1InfoRec.TyousaHouhouNo, _
                                    hin1AutoSetRecord.KoumutenGaku, _
                                    hin1AutoSetRecord.KoumutenGakuHenkouFlg, _
                                    hin1AutoSetRecord.JituGaku, _
                                    hin1AutoSetRecord.JituGakuHenkouFlg, _
                                    blnTysUmKbn, _
                                    True) Then
            Return True
        End If

        '「系列」の販売価格算出
        If daHanbai.GetHanbaiKakaku(EarthConst.AITESAKI_KEIRETU, _
                                    hin1InfoRec.KeiretuCd, _
                                    hin1InfoRec.SyouhinCd, _
                                    hin1InfoRec.TyousaHouhouNo, _
                                    hin1AutoSetRecord.KoumutenGaku, _
                                    hin1AutoSetRecord.KoumutenGakuHenkouFlg, _
                                    hin1AutoSetRecord.JituGaku, _
                                    hin1AutoSetRecord.JituGakuHenkouFlg, _
                                    blnTysUmKbn, _
                                    True) Then
            Return True
        End If

        '「指定無」の販売価格算出
        If daHanbai.GetHanbaiKakaku(EarthConst.AITESAKI_NASI, _
                                    EarthConst.AITESAKI_NASI_CD, _
                                    hin1InfoRec.SyouhinCd, _
                                    hin1InfoRec.TyousaHouhouNo, _
                                    hin1AutoSetRecord.KoumutenGaku, _
                                    hin1AutoSetRecord.KoumutenGakuHenkouFlg, _
                                    hin1AutoSetRecord.JituGaku, _
                                    hin1AutoSetRecord.JituGakuHenkouFlg, _
                                    blnTysUmKbn, _
                                    True) Then
            Return True
        End If


        Return False
    End Function

#End Region

#Region "商品ｺｰﾄﾞ2,3情報取得"
    ''' <summary>
    ''' 商品コード２または３の情報をSyouhin23RecordクラスのList(Of Syouhin23Record)で取得します
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSyouhinNm">商品名</param>
    ''' <param name="type">商品コード２、３の指定</param>
    ''' <param name="allCount">検索時の取得全件数</param>
    ''' <param name="TyousaHouhouNo">調査方法No</param>
    ''' <param name="kameitenCd">（任意）加盟店コード Default:""</param>
    ''' <param name="startRow">（任意）データ抽出時の開始行(1件目は1を指定)Default:1</param>
    ''' <param name="endRow">（任意）データ抽出時の終了行 Default:99999999</param>
    ''' <returns>Syouhin23RecordクラスのList(Of Syouhin23Record)</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhin23(ByVal strSyouhinCd As String, _
                                 ByVal strSyouhinNm As String, _
                                 ByVal type As EarthEnum.EnumSyouhinKubun, _
                                 ByRef allCount As Integer, _
                                 ByVal TyousaHouhouNo As Integer, _
                                 Optional ByVal kameitenCd As String = "", _
                                 Optional ByVal startRow As Integer = 1, _
                                 Optional ByVal endRow As Integer = 99999999) As List(Of Syouhin23Record)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin23", _
                                                    strSyouhinCd, _
                                                    strSyouhinNm, _
                                                    type, _
                                                    allCount, _
                                                    TyousaHouhouNo, _
                                                    kameitenCd, _
                                                    startRow, _
                                                    endRow)

        Dim dataAccess As New SyouhinDataAccess
        Dim list As List(Of Syouhin23Record)
        Dim total_count As Integer = 0
        Dim kameitenLogic As New KameitenSearchLogic
        Dim kameitenList As New List(Of KameitenSearchRecord)
        Dim recTysHouhou As New TyousahouhouRecord

        ' 入力された商品コードを大文字に変換
        If strSyouhinCd <> "" Then
            strSyouhinCd = UCase(strSyouhinCd)
        End If

        Dim table As DataTable = dataAccess.GetSyouhinInfo(strSyouhinCd, strSyouhinNm, Type, kameitenCd)

        ' 商品コード='A2001'の場合、加盟店Mから取得する
        If (type = EarthEnum.EnumSyouhinKubun.Syouhin3) AndAlso (strSyouhinCd = EarthConst.SYOUHIN3_AUTO_CD) AndAlso (kameitenCd <> "") Then
            kameitenList = kameitenLogic.GetKameitenSearchResult("", _
                                           kameitenCd, _
                                           True, _
                                           total_count)
        End If

        ' 件数を設定
        allCount = table.Rows.Count

        ' データを取得し、List(Of Syouhin23Record)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of Syouhin23Record)(GetType(Syouhin23Record), table, startRow, endRow)

        '調査方法NOのチェック
        If TyousaHouhouNo > Integer.MinValue Then
            recTysHouhou = Me.getTyousahouhouRecord(TyousaHouhouNo)
        End If

        ' 商品3の場合、該当加盟店で、商品コード='A2001' and 調査方法No='15'の場合、 加盟店M.SSGR価格で、
        '                            商品コード='A2001' and 調査方法No<>'15'の場合、加盟店M.解析保証価格で、listの標準価格を更新
        If (Not recTysHouhou Is Nothing) AndAlso (recTysHouhou.Torikesi = 0) Then
            If (type = EarthEnum.EnumSyouhinKubun.Syouhin3) AndAlso (strSyouhinCd = EarthConst.SYOUHIN3_AUTO_CD) Then
                If kameitenList.Count < 1 Then
                    'list(0).HyoujunKkk = 0
                ElseIf kameitenList.Count = 1 Then
                    If TyousaHouhouNo = EarthConst.TYOUSA_HOUHOU_CD_15 Then
                        list(0).HyoujunKkk = kameitenList(0).SsgrKkk
                    Else
                        list(0).HyoujunKkk = kameitenList(0).KaisekiHosyouKkk
                    End If
                End If
            End If
        End If

        Return list

    End Function

    ''' <summary>
    ''' 商品コード２、３の情報と画面の設定内容を元に請求情報を作成します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="syouhin23Info">商品コード２の情報と画面の設定内容</param>
    ''' <param name="intMode">承諾書金額の設定モード 1:設定,0:設定無し</param>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhin23SeikyuuData(ByRef sender As Object, _
                                           ByVal syouhin23Info As Syouhin23InfoRecord, _
                                           ByVal intMode As Integer, _
                                           Optional ByRef intSetSts As Integer = 0) As TeibetuSeikyuuRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin23SeikyuuData", _
                                            sender, _
                                            syouhin23Info, _
                                            intMode)

        ' 戻り値となる商品２、３の請求情報
        Dim seikyuuRec As New TeibetuSeikyuuRecord

        seikyuuRec.SyouhinCd = syouhin23Info.Syouhin2Rec.SyouhinCd  ' 商品コード
        seikyuuRec.ZeiKbn = syouhin23Info.Syouhin2Rec.ZeiKbn        ' 税区分
        If syouhin23Info.SeikyuuUmu <> 0 Then                       ' 請求有無
            seikyuuRec.SeikyuuUmu = 1
        Else
            seikyuuRec.SeikyuuUmu = syouhin23Info.SeikyuuUmu
        End If
        If syouhin23Info.HattyuusyoKakuteiFlg <> 0 Then            ' 発注書確定FLG
            seikyuuRec.HattyuusyoKakuteiFlg = 1
        Else
            seikyuuRec.HattyuusyoKakuteiFlg = syouhin23Info.HattyuusyoKakuteiFlg
        End If

        Dim blnSeikyuuYMD As Boolean = False

        If seikyuuRec.SeikyuuUmu = 1 Then

            blnSeikyuuYMD = True

            ' 請求先が直接請求の場合
            If syouhin23Info.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU Then
                ' 工務店請求額、売上金額に標準価格をセット
                seikyuuRec.KoumutenSeikyuuGaku = syouhin23Info.Syouhin2Rec.HyoujunKkk
                seikyuuRec.UriGaku = syouhin23Info.Syouhin2Rec.HyoujunKkk
            ElseIf syouhin23Info.KeiretuFlg = 1 Then
                '他請求の3系列
                ' 工務店請求額に標準価格をセット
                seikyuuRec.KoumutenSeikyuuGaku = syouhin23Info.Syouhin2Rec.HyoujunKkk

                If seikyuuRec.KoumutenSeikyuuGaku = 0 Then
                    ' 工務店請求額が０の場合、売上金額も０
                    seikyuuRec.UriGaku = 0
                Else
                    ' 売上金額の設定
                    If GetSeikyuuGaku(sender, _
                                   3, _
                                   syouhin23Info.KeiretuCd, _
                                   syouhin23Info.Syouhin2Rec.SyouhinCd, _
                                   0, _
                                   seikyuuRec.UriGaku) = False Then
                        intSetSts = 1
                    End If
                End If
            Else
                '他請求の3系列以外
                ' 工務店請求額=0、売上金額に標準価格をセット
                seikyuuRec.KoumutenSeikyuuGaku = 0
                seikyuuRec.UriGaku = syouhin23Info.Syouhin2Rec.HyoujunKkk
            End If

            ' 承諾書金額の設定
            If intMode = 1 Then
                ' 倉庫コード"110,115,120"の場合
                If syouhin23Info.Syouhin2Rec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_110 _
                    OrElse syouhin23Info.Syouhin2Rec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_115 _
                    OrElse syouhin23Info.Syouhin2Rec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_3 Then

                    If syouhin23Info.Syouhin2Rec.SiireKkk = Integer.MinValue Then
                        seikyuuRec.SiireGaku = Integer.MinValue
                    Else
                        seikyuuRec.SiireGaku = syouhin23Info.Syouhin2Rec.SiireKkk
                    End If
                End If
            End If

            ' 倉庫コードが"115"の場合、符号反転
            If syouhin23Info.Syouhin2Rec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_115 Then
                seikyuuRec.KoumutenSeikyuuGaku = Math.Abs(seikyuuRec.KoumutenSeikyuuGaku) * -1
                seikyuuRec.UriGaku = Math.Abs(seikyuuRec.UriGaku) * -1
                seikyuuRec.SiireGaku = Math.Abs(seikyuuRec.SiireGaku) * -1
            End If

        End If

        Return seikyuuRec

    End Function

#End Region

#Region "商品情報の取得(直接請求/他請求)"
    ''' <summary>
    ''' 商品コード、商品タイプをKeyに商品情報を取得します(直接請求/他請求)
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSyouhinType">商品タイプ(SyouhinDataAccess.enumSyouhinKubun)</param>
    ''' <returns>商品情報レコード</returns>
    ''' <remarks>取得できない場合はNothingを返却します</remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, _
                                   ByVal strSyouhinType As EarthEnum.EnumSyouhinKubun, _
                                   ByVal strKameitenCd As String) As Syouhin23Record

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo", _
                                                    strSyouhinCd, _
                                                    strSyouhinType)

        Dim dataAccess As New SyouhinDataAccess
        Dim list As List(Of Syouhin23Record)
        Dim table As DataTable = dataAccess.GetSyouhinInfo(strSyouhinCd, "", strSyouhinType, strKameitenCd)

        ' データを取得し、List(Of SyouhinRecord)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of Syouhin23Record)(GetType(Syouhin23Record), _
                                                      table)

        ' １件取得を目的としているので無条件に１件目を返却
        If list.Count > 0 Then
            Return list(0)
        End If

        Return Nothing
    End Function
#End Region

#Region "商品１請求書発行日,売上年月日の設定処理"

    ''' <summary>
    ''' 商品コード１の請求書発行日,売上年月日の処理を行います
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <param name="tyousaJissibi">調査実施日</param>
    ''' <param name="rec">設定対象となる商品１レコードの邸別請求レコード</param>
    ''' <param name="uriSet">売上年月日を無条件に再設定する場合:true</param>
    ''' <remarks>判定に使用する項目：確定区分</remarks>
    Public Sub SubSeikyuuUriageDateSet(ByVal kameitenCd As String, _
                                       ByVal tyousaJissibi As DateTime, _
                                       ByRef rec As TeibetuSeikyuuRecord, _
                                       ByVal uriSet As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SubSeikyuuUriageDateSet", _
                                            kameitenCd, _
                                            tyousaJissibi, _
                                            rec, _
                                            uriSet)
        Dim cBizLogic As New CommonBizLogic
        ' 調査実施日未指定時、処理終了
        If tyousaJissibi = DateTime.MinValue Then
            Exit Sub
        End If

        ' 請求有無未指定時、処理終了
        If rec.SeikyuuUmu = Integer.MinValue Then
            Exit Sub
        End If

        ' 請求有無：ありの場合、請求書発行日を設定する（商品１は調査実施日有りが前提）
        If rec.SeikyuuUmu = 1 Then
            ' 請求書発行日は未指定の場合のみ設定する
            If rec.SeikyuusyoHakDate = Date.MinValue Then
                Dim strSimeDate As String
                '請求書締め日を取得
                strSimeDate = cBizLogic.GetSeikyuuSimeDateFromKameiten(rec.SeikyuuSakiCd _
                                                                        , rec.SeikyuuSakiBrc _
                                                                        , rec.SeikyuuSakiKbn _
                                                                        , kameitenCd _
                                                                        , rec.SyouhinCd)
                ' 請求書発行日をセット
                rec.SeikyuusyoHakDate = cBizLogic.CalcSeikyuusyoHakkouDate(strSimeDate)
            End If
        End If

        ' 売上年月日：未入力の場合設定する
        If rec.UriDate = Date.MinValue Then
            rec.UriDate = Date.Now
        ElseIf uriSet = True Then
            rec.UriDate = Date.Now
        End If

    End Sub

#End Region

#Region "商品(邸別請求)レコード動的生成処理[共通]"

#Region "商品1"
    ''' <summary>
    ''' 商品コード１の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <remarks></remarks>
    Public Function Syouhin1Set(ByVal sender As System.Object, ByRef jibanRec As JibanRecordBase) As Boolean
        ' データ取得用パラメータクラス
        Dim param_rec As New Syouhin1InfoRecord
        ' 取得レコード格納クラス
        Dim syouhin_rec As New Syouhin1AutoSetRecord

        '●系列コードの取得
        Dim kameitenlogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = False
        Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(jibanRec.Kbn, jibanRec.KameitenCd, jibanRec.TysKaisyaCd + jibanRec.TysKaisyaJigyousyoCd, blnTorikesi)

        jibanRec.SyouhinKkk_KeiretuCd = record.KeiretuCd        '系列コードをセット
        jibanRec.SyouhinKkk_EigyousyoCd = record.EigyousyoCd    '営業所コードをセット

        '●1.商品設定に必要な画面情報の取得
        ' データ取得用のパラメータセット 
        '区分
        param_rec.Kubun = jibanRec.Kbn
        '商品区分
        param_rec.SyouhinKbn = jibanRec.SyouhinKbn
        '建物用途
        param_rec.TatemonoYouto = jibanRec.TatemonoYoutoNo
        '調査概要
        param_rec.TyousaGaiyou = jibanRec.TysGaiyou
        '商品コード
        param_rec.SyouhinCd = jibanRec.SyouhinCd1
        '調査会社コード+事業所コード
        param_rec.TysKaisyaCd = jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd
        '同時依頼棟数
        param_rec.DoujiIraiTousuu = jibanRec.DoujiIraiTousuu
        '調査方法
        param_rec.TyousaHouhouNo = jibanRec.TysHouhou
        '加盟店コード
        param_rec.KameitenCd = jibanRec.KameitenCd
        '系列コード
        param_rec.KeiretuCd = jibanRec.SyouhinKkk_KeiretuCd
        '系列フラグ
        param_rec.KeiretuFlg = Me.GetKeiretuFlg(jibanRec.SyouhinKkk_KeiretuCd)
        '営業所コード
        param_rec.EigyousyoCd = jibanRec.SyouhinKkk_EigyousyoCd
        '請求先
        If param_rec.KameitenCd.Trim() <> "" Then '加盟店コード指定時
            ' 加盟店コードと調査請求先が同一の場合、直接請求
            If param_rec.KameitenCd = record.TysSeikyuuSaki Then
                param_rec.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU ' 請求先
                jibanRec.SyouhinKkk_SeikyuuSaki = EarthConst.SEIKYU_TYOKUSETU ' 直接請求
            Else
                param_rec.Seikyuusaki = EarthConst.SEIKYU_TASETU ' 請求先
                jibanRec.SyouhinKkk_SeikyuuSaki = EarthConst.SEIKYU_TASETU ' 他請求
            End If
        End If

        '実請求金額(税抜金額)=>初期セットなので0
        param_rec.ZeinukiKingaku1 = 0

        '工務店請求額=>初期セットなので0
        param_rec.KoumutenKingaku1 = 0

        '取得ステータス
        Dim intSetSts As EarthEnum.emSyouhin1Error

        '●2.商品1情報の取得
        ' 商品１情報を取得し、レコードクラスにセット
        If Me.GetSyouhin1Info(sender, param_rec, syouhin_rec, intSetSts) = True Then

            '商品1取得ステータスエラー
            If intSetSts > 0 Then
                jibanRec.SyouhinKkk_ErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)
                Return False
                Exit Function
            End If

            '商品1レコードの生成
            If jibanRec.Syouhin1Record Is Nothing Then
                jibanRec.Syouhin1Record = New TeibetuSeikyuuRecord
            End If

            With jibanRec.Syouhin1Record
                '区分
                .Kbn = jibanRec.Kbn
                '保証書NO
                .HosyousyoNo = jibanRec.HosyousyoNo
                '分類コード
                .BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1
                '画面表示NO
                .GamenHyoujiNo = 1
                '商品コード
                .SyouhinCd = syouhin_rec.SyouhinCd
                '売上金額(実請求金額)
                .UriGaku = syouhin_rec.JituGaku
                '仕入金額(承諾書金額)(別メソッドで取得
                .SiireGaku = 0
                '税区分
                .ZeiKbn = syouhin_rec.ZeiKbn
                '税率
                .Zeiritu = syouhin_rec.Zeiritu
                '消費税額(レコードクラスで自動算出のため省略)
                '請求書発行日
                .SeikyuusyoHakDate = DateTime.MinValue '後続にてセット
                '売上年月日
                .UriDate = DateTime.MinValue
                '伝票売上年月日(ロジッククラスで自動セット)
                .DenpyouUriDate = DateTime.MinValue
                '請求有無
                .SeikyuuUmu = 1
                '確定区分
                .KakuteiKbn = Integer.MinValue
                '売上計上FLG
                .UriKeijyouFlg = 0
                '売上計上日
                .UriKeijyouDate = DateTime.MinValue
                '備考
                .Bikou = String.Empty
                '工務店請求金額
                .KoumutenSeikyuuGaku = syouhin_rec.KoumutenGaku
                '発注書金額
                .HattyuusyoGaku = Integer.MinValue
                '発注書確認日
                .HattyuusyoKakuninDate = DateTime.MinValue
                '一括入金FLG
                .IkkatuNyuukinFlg = Integer.MinValue
                '調査見積書作成日
                .TysMitsyoSakuseiDate = DateTime.MinValue
                '発注書確定FLG
                .HattyuusyoKakuteiFlg = 0
                '更新ログインユーザーID
                .UpdLoginUserId = jibanRec.UpdLoginUserId
                '***********************************
            End With

            ' 価格設定場所をセット
            jibanRec.SyouhinKkk_SetteiBasyo = syouhin_rec.KakakuSettei

            '請求先タイプをセット
            jibanRec.SyouhinKkk_SeikyuuSakiType = syouhin_rec.SeikyuuSakiType

        Else
            'ステータスにより、エラーメッセージ取得
            jibanRec.SyouhinKkk_ErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)

            Return False
            Exit Function
        End If

        Return True
    End Function

    ''' <summary>
    ''' 商品１承諾書金額の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub Syouhin1SyoudakuGakuSet(ByVal sender As System.Object, ByRef jibanRec As JibanRecordBase)
        ' 取得する承諾書価格
        Dim syoudaku_kakaku As Integer = 0

        Dim chousa_jouhou As Integer
        Dim chousa_gaiyou As Integer
        Dim kakaku_settei As Integer
        Dim irai_tousuu As Integer
        Dim blnSyoudakuHenkouFlg As Boolean '承諾書金額変更可能FLG

        With jibanRec

            ' 数値項目の変換
            cbLogic.SetDisplayString(.TysHouhou, chousa_jouhou)
            cbLogic.SetDisplayString(.TysGaiyou, chousa_gaiyou)
            cbLogic.SetDisplayString(.SyouhinKkk_SetteiBasyo, kakaku_settei)
            cbLogic.SetDisplayString(.DoujiIraiTousuu, irai_tousuu)

            If Me.GetSyoudakusyoKingaku1(.Syouhin1Record.SyouhinCd, _
                                         .Syouhin1Record.Kbn, _
                                         chousa_jouhou, _
                                         chousa_gaiyou, _
                                         irai_tousuu, _
                                         .TysKaisyaCd & .TysKaisyaJigyousyoCd, _
                                         .KameitenCd, _
                                         syoudaku_kakaku, _
                                         .SyouhinKkk_KeiretuCd, _
                                         blnSyoudakuHenkouFlg) = True Then

                ' 承諾書金額をセット
                .Syouhin1Record.SiireGaku = syoudaku_kakaku
            Else
                '●原価マスタに該当の組み合わせが無いかつ商品設定無しは、金額無し
                If .Syouhin1Record.SyouhinCd = Nothing OrElse .Syouhin1Record.SyouhinCd = "" Then
                    .Syouhin1Record.SiireGaku = Integer.MinValue
                End If
            End If

        End With

    End Sub

    ''' <summary>
    ''' 商品１請求書発行日、売上年月日の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="uri_set">売上年月日無条件設定時:true</param>
    ''' <param name="intGamenHyoujiNo">画面表示No指定時は該当の邸別請求レコードのみ処理を行なう</param>
    ''' <remarks></remarks>
    Public Sub Syouhin1UriageSeikyuDateSet(ByVal sender As System.Object, ByRef jibanRec As JibanRecordBase, ByVal uri_set As Boolean, Optional ByVal intGamenHyoujiNo As Integer = Integer.MinValue)
        Dim tyousa_jissi_date As DateTime '調査実施日
        Dim seikyuu_hakkou_date As DateTime '請求書発行日
        Dim uriage_date As DateTime '売上年月日

        seikyuu_hakkou_date = jibanRec.Syouhin1Record.SeikyuusyoHakDate
        uriage_date = jibanRec.Syouhin1Record.UriDate

        ' 邸別請求レコードに必要情報をセットし、請求書発行日、売上年月日を取得する
        Dim teibetu_rec As New TeibetuSeikyuuRecord

        ' 画面の情報をセット
        teibetu_rec.Kbn = jibanRec.Kbn
        teibetu_rec.SeikyuuUmu = jibanRec.Syouhin1Record.SeikyuuUmu
        teibetu_rec.SeikyuusyoHakDate = seikyuu_hakkou_date
        teibetu_rec.UriDate = uriage_date
        teibetu_rec.SyouhinCd = jibanRec.Syouhin1Record.SyouhinCd

        ' 調査実施日を日付型にする
        tyousa_jissi_date = jibanRec.TysJissiDate

        ' 請求書発行日、売上年月日を取得する
        Me.SubSeikyuuUriageDateSet(jibanRec.KameitenCd, tyousa_jissi_date, teibetu_rec, uri_set)

        ' 結果をセットする
        '請求書発行日
        jibanRec.Syouhin1Record.SeikyuusyoHakDate = teibetu_rec.SeikyuusyoHakDate

        '売上年月日
        jibanRec.Syouhin1Record.UriDate = teibetu_rec.UriDate

        With jibanRec
            ' 商品１，２の請求書発行日、売上年月日の連動
            If .Syouhin1Record.SeikyuuUmu = 1 Then '商品1レコード.請求有無=有の場合

                '商品2レコードの生成
                For intCnt As Integer = 1 To EarthConst.SYOUHIN2_COUNT
                    If intGamenHyoujiNo <> Integer.MinValue Then
                        If intGamenHyoujiNo <> intCnt Then
                            Continue For
                        End If
                    End If

                    '商品2情報セットあり
                    If .Syouhin2Records.ContainsKey(intCnt) Then

                        '商品2_*
                        With jibanRec.Syouhin2Records(intCnt)
                            If .SyouhinCd <> "" Then
                                '請求書発行日
                                .SeikyuusyoHakDate = IIf(.SeikyuusyoHakDate <> Date.MinValue, teibetu_rec.SeikyuusyoHakDate, .SeikyuusyoHakDate)
                                '売上年月日
                                .UriDate = IIf(.UriDate <> Date.MinValue, teibetu_rec.UriDate, .UriDate)
                            End If
                        End With
                    End If

                Next

            End If

        End With

    End Sub

    ''' <summary>
    ''' 商品１レコードオブジェクトの生成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub CreateSyouhin1Rec(ByVal sender As Object, ByRef jibanRec As JibanRecordBase)
        ' 商品コード１のリスト
        Dim syouhinHash As New TeibetuSeikyuuRecord
        Dim teibetuRec As New TeibetuSeikyuuRecord

        syouhinHash = jibanRec.Syouhin1Record

        If syouhinHash Is Nothing Then
            syouhinHash = New TeibetuSeikyuuRecord
        End If

        '商品2レコードの生成
        If Not jibanRec Is Nothing _
                AndAlso Not syouhinHash Is Nothing Then
        Else
            teibetuRec = New TeibetuSeikyuuRecord

            '商品1_*
            With teibetuRec
                '区分
                .Kbn = jibanRec.Kbn
                '保証書NO
                .HosyousyoNo = jibanRec.HosyousyoNo
                '分類コード
                .BunruiCd = "" '後続にてセット
                '画面表示NO
                .GamenHyoujiNo = 1
                '商品コード
                .SyouhinCd = "" '後続にてセット
                '売上金額(実請求金額)
                .UriGaku = 0              ' 実請求額
                '仕入金額(承諾書金額)
                .SiireGaku = 0
                '税区分
                .ZeiKbn = 0
                '消費税額(レコードクラスで自動算出のため省略)
                '請求書発行日
                .SeikyuusyoHakDate = DateTime.MinValue '後続にてセット
                '売上年月日
                .UriDate = DateTime.MinValue
                '伝票売上年月日(ロジッククラスで自動セット)
                .DenpyouUriDate = DateTime.MinValue
                '請求有無
                .SeikyuuUmu = 1
                '確定区分
                .KakuteiKbn = Integer.MinValue
                '売上計上FLG
                .UriKeijyouFlg = 0
                '売上計上日
                .UriKeijyouDate = DateTime.MinValue
                '備考
                .Bikou = String.Empty
                '工務店請求金額
                .KoumutenSeikyuuGaku = 0      ' 工務店請求額
                '発注書金額
                .HattyuusyoGaku = Integer.MinValue
                '発注書確認日
                .HattyuusyoKakuninDate = DateTime.MinValue
                '一括入金FLG
                .IkkatuNyuukinFlg = Integer.MinValue
                '調査見積書作成日
                .TysMitsyoSakuseiDate = DateTime.MinValue
                '発注書確定FLG
                .HattyuusyoKakuteiFlg = 0
                '更新ログインユーザーID
                .UpdLoginUserId = jibanRec.UpdLoginUserId
                '***********************************
            End With
        End If

        '商品2レコードをセット
        If jibanRec.Syouhin1Record Is Nothing Then
            jibanRec.Syouhin1Record = syouhinHash
        End If

    End Sub

#End Region

#Region "商品2/商品3"
    ''' <summary>
    ''' 商品２レコードオブジェクトの生成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="blnSyouhin3">商品３を対象とするか</param>
    ''' <remarks></remarks>
    Public Sub CreateSyouhin23Rec(ByVal sender As Object, ByRef jibanRec As JibanRecordBase, Optional ByVal blnSyouhin3 As Boolean = False)
        ' 商品コード２のリスト
        Dim syouhinHash As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim teibetuRec As New TeibetuSeikyuuRecord

        Dim intMax As Integer = 0

        If blnSyouhin3 = False Then
            intMax = EarthConst.SYOUHIN2_COUNT
            syouhinHash = jibanRec.Syouhin2Records
        Else
            intMax = EarthConst.SYOUHIN3_COUNT
            syouhinHash = jibanRec.Syouhin3Records
        End If

        If syouhinHash Is Nothing Then
            syouhinHash = New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        End If

        '商品2レコードの生成
        For intCnt As Integer = 1 To intMax
            If Not jibanRec Is Nothing _
                AndAlso Not syouhinHash Is Nothing _
                    AndAlso syouhinHash.ContainsKey(intCnt) = True Then
            Else
                teibetuRec = New TeibetuSeikyuuRecord

                '商品2_*,商品3_*
                With teibetuRec
                    '区分
                    .Kbn = jibanRec.Kbn
                    '保証書NO
                    .HosyousyoNo = jibanRec.HosyousyoNo
                    '分類コード
                    .BunruiCd = "" '後続にてセット
                    '画面表示NO
                    .GamenHyoujiNo = intCnt
                    '商品コード
                    .SyouhinCd = "" '後続にてセット
                    '売上金額(実請求金額)
                    .UriGaku = 0              ' 実請求額
                    '仕入金額(承諾書金額)
                    .SiireGaku = 0
                    '税区分
                    .ZeiKbn = 0
                    '消費税額(レコードクラスで自動算出のため省略)
                    '請求書発行日
                    .SeikyuusyoHakDate = DateTime.MinValue '後続にてセット
                    '売上年月日
                    .UriDate = DateTime.MinValue
                    '伝票売上年月日(ロジッククラスで自動セット)
                    .DenpyouUriDate = DateTime.MinValue
                    '請求有無
                    .SeikyuuUmu = 1
                    '確定区分
                    .KakuteiKbn = Integer.MinValue
                    '売上計上FLG
                    .UriKeijyouFlg = 0
                    '売上計上日
                    .UriKeijyouDate = DateTime.MinValue
                    '備考
                    .Bikou = String.Empty
                    '工務店請求金額
                    .KoumutenSeikyuuGaku = 0      ' 工務店請求額
                    '発注書金額
                    .HattyuusyoGaku = Integer.MinValue
                    '発注書確認日
                    .HattyuusyoKakuninDate = DateTime.MinValue
                    '一括入金FLG
                    .IkkatuNyuukinFlg = Integer.MinValue
                    '調査見積書作成日
                    .TysMitsyoSakuseiDate = DateTime.MinValue
                    '発注書確定FLG
                    .HattyuusyoKakuteiFlg = 0
                    '更新ログインユーザーID
                    .UpdLoginUserId = jibanRec.UpdLoginUserId
                    '***********************************
                End With

                syouhinHash.Add(intCnt, teibetuRec)

            End If
        Next

        If blnSyouhin3 = False Then
            '商品2レコードをセット
            If jibanRec.Syouhin2Records Is Nothing Then
                jibanRec.Syouhin2Records = syouhinHash
            End If
        Else
            '商品3レコードをセット
            If jibanRec.Syouhin3Records Is Nothing Then
                jibanRec.Syouhin3Records = syouhinHash
            End If
        End If

    End Sub

    ''' <summary>
    ''' 特定店商品２設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub TokuteitenSyouhin2Set(ByVal sender As Object, ByRef jibanRec As JibanRecordBase)
        Dim kameiten_cd As String = String.Empty

        ' 加盟店取得済みチェック（取消加盟店対策）
        If jibanRec.KameitenCd Is Nothing OrElse jibanRec.KameitenCd = String.Empty Then
            ' 加盟店名が空(＝加盟店未取)の場合、処理を抜ける
            Exit Sub
        End If

        ' 商品２設定済みチェック
        With jibanRec
            If .Syouhin2Records(1).SyouhinCd <> String.Empty OrElse _
                .Syouhin2Records(2).SyouhinCd <> String.Empty OrElse _
                .Syouhin2Records(3).SyouhinCd <> String.Empty OrElse _
                .Syouhin2Records(4).SyouhinCd <> String.Empty Then
                ' 一つでも商品２が設定されている場合、処理を抜ける
                Exit Sub
            End If
        End With

        ' 特定店商品２のコード取得処理
        Dim syouhinCd2List As New List(Of String)
        If Me.GetTokuteitenSyouhin2(jibanRec.KameitenCd, syouhinCd2List) = True Then
            Dim lineCount As Integer = 1
            For Each syouhinCd2 As String In syouhinCd2List
                setSyouhinCd23(sender, "2_" & lineCount.ToString, syouhinCd2, jibanRec)
                lineCount += 1
                If lineCount > 4 Then
                    Exit For
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 多棟割り商品２設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub TatouwariSet(ByVal sender As Object, ByRef jibanRec As JibanRecordBase)
        Dim syouhin_cd2 As String = ""

        With jibanRec

            ' 多棟割り設定時の商品コード２取得処理
            If Me.GetTatouwariSyouhinCd2(.Kbn, _
                                            .KameitenCd, _
                                            .DoujiIraiTousuu, _
                                            syouhin_cd2) = True Then

                ' 商品コードが取得できた場合、既に同一商品コードが存在しない且つ
                ' 商品コード２に空きがある場合、邸別請求より明細を取得して画面にセットする

                ' 同一コードのチェック
                If .Syouhin2Records(1).SyouhinCd = syouhin_cd2 Or _
                   .Syouhin2Records(2).SyouhinCd = syouhin_cd2 Or _
                   .Syouhin2Records(3).SyouhinCd = syouhin_cd2 Or _
                   .Syouhin2Records(4).SyouhinCd = syouhin_cd2 Then
                    ' 同一コードありの場合は設定しない
                    Exit Sub
                End If

                ' 空いている欄があれば設定する
                If .Syouhin2Records(1).SyouhinCd = String.Empty Then
                    setSyouhinCd23(sender, "2_1", syouhin_cd2, jibanRec)
                ElseIf .Syouhin2Records(2).SyouhinCd = String.Empty Then
                    setSyouhinCd23(sender, "2_2", syouhin_cd2, jibanRec)
                ElseIf .Syouhin2Records(3).SyouhinCd = String.Empty Then
                    setSyouhinCd23(sender, "2_3", syouhin_cd2, jibanRec)
                ElseIf .Syouhin2Records(4).SyouhinCd = String.Empty Then
                    setSyouhinCd23(sender, "2_4", syouhin_cd2, jibanRec)
                Else
                    ' 空きが無い場合は設定しない
                    Exit Sub
                End If

            End If

        End With

    End Sub

    ''' <summary>
    ''' 商品２/３レコードオブジェクトの破棄
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="blnSyouhin3">商品３を対象とするか</param>
    ''' <remarks>レコードオブジェクトのうち、商品コードが未設定のものは削除する</remarks>
    Public Sub DeleteSyouhin23Rec(ByVal sender As Object, ByRef jibanRec As JibanRecordBase, Optional ByVal blnSyouhin3 As Boolean = False)
        With jibanRec
            If blnSyouhin3 = False Then
                '商品2レコード
                For intCnt As Integer = EarthConst.SYOUHIN2_COUNT To 1 Step -1
                    '商品2_*
                    If Not .Syouhin2Records Is Nothing Then
                        '商品コード=未入力
                        If .Syouhin2Records.ContainsKey(intCnt) AndAlso .Syouhin2Records(intCnt).SyouhinCd = String.Empty Then
                            .Syouhin2Records.Remove(intCnt)
                        End If
                    End If
                Next
            Else
                '商品3レコード
                For intCnt As Integer = EarthConst.SYOUHIN3_COUNT To 1 Step -1
                    '商品3_*
                    If Not .Syouhin3Records Is Nothing Then
                        '商品コード=未入力
                        If .Syouhin3Records.ContainsKey(intCnt) AndAlso .Syouhin3Records(intCnt).SyouhinCd = String.Empty Then
                            .Syouhin3Records.Remove(intCnt)
                        End If
                    End If
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' 商品コード 画面設定
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="syouhin_type">設定したい商品行タイプ</param>
    ''' <param name="syouhin_cd">商品コード</param>
    ''' <remarks></remarks>
    Public Function setSyouhinCd23(ByVal sender As Object, _
                               ByVal syouhin_type As String, _
                               ByVal syouhin_cd As String, _
                               ByRef jibanRec As JibanRecordBase _
                               ) As Boolean

        If syouhin_cd = String.Empty Then
            Return False
            Exit Function
        End If

        ' 情報取得用のパラメータクラス
        Dim syouhin23_info As New Syouhin23InfoRecord

        ' 商品の基本情報を取得
        Dim syouhin23_rec As Syouhin23Record = GetSyouhinInfo(syouhin_type.Split("_")(0), syouhin_cd, jibanRec.KameitenCd)

        ' 取得できない場合、エラー
        If syouhin23_rec Is Nothing Then
            Return False
            Exit Function
        End If

        '商品2,3の対象インデックス
        Dim strSyouhin23Type As String = syouhin_type.Split("_")(0) '商品2or3
        Dim strSyouhin23Index As String = syouhin_type.Split("_")(1) 'インデックス
        Dim intIndex As Integer = CInt(strSyouhin23Index)
        Dim TeibetuTmpRecords As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim TeibetuTmpRecord As New TeibetuSeikyuuRecord

        '画面上で請求先が指定されている場合、レコードの請求先を上書き
        With jibanRec
            TeibetuTmpRecords = New Dictionary(Of Integer, TeibetuSeikyuuRecord)
            Select Case strSyouhin23Type
                Case "1" '商品1
                    TeibetuTmpRecord = .Syouhin1Record
                Case "2"
                    TeibetuTmpRecords = .Syouhin2Records
                    TeibetuTmpRecord = .Syouhin2Records(intIndex)
                Case "3"
                    TeibetuTmpRecords = .Syouhin3Records
                    TeibetuTmpRecord = .Syouhin3Records(intIndex)
                Case Else
                    TeibetuTmpRecord = Nothing
            End Select

            '画面上で請求先が指定されている場合、レコードの請求先を上書き
            If Not TeibetuTmpRecord Is Nothing AndAlso TeibetuTmpRecord.SeikyuuSakiCd <> String.Empty Then
                '請求先をレコードにセット
                syouhin23_rec.SeikyuuSakiCd = TeibetuTmpRecord.SeikyuuSakiCd
                syouhin23_rec.SeikyuuSakiBrc = TeibetuTmpRecord.SeikyuuSakiBrc
                syouhin23_rec.SeikyuuSakiKbn = TeibetuTmpRecord.SeikyuuSakiKbn
            End If
        End With

        ' 商品コード及び画面の情報をセット
        syouhin23_info.Syouhin2Rec = syouhin23_rec                            ' 商品の基本情報
        syouhin23_info.SeikyuuUmu = 1                                         ' 請求有無
        syouhin23_info.HattyuusyoKakuteiFlg = 0                               ' 発注書確定フラグ
        syouhin23_info.Seikyuusaki = syouhin23_rec.SeikyuuSakiType            ' 請求先タイプ
        syouhin23_info.KeiretuCd = jibanRec.SyouhinKkk_KeiretuCd                              ' 系列コード
        syouhin23_info.KeiretuFlg = Me.GetKeiretuFlg(jibanRec.SyouhinKkk_KeiretuCd)        ' 系列フラグ

        ' 請求レコードの取得(確実に結果が有る)
        Dim teibetu_seikyuu_rec As TeibetuSeikyuuRecord = getSyouhin23SeikyuuInfo(sender, syouhin23_info)
        If teibetu_seikyuu_rec Is Nothing Then
            Return False
            Exit Function
        End If

        Select Case syouhin23_rec.SoukoCd
            Case EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115 '商品2

                With jibanRec.Syouhin2Records(intIndex)
                    ' コード、名称をセット（多棟割りの場合、設定必須の為）
                    '倉庫コード(分類コード)
                    .BunruiCd = syouhin23_rec.SoukoCd
                    '画面表示No
                    .GamenHyoujiNo = intIndex
                    '商品コード
                    .SyouhinCd = syouhin23_rec.SyouhinCd

                    ' 価格情報をセット
                    '工務店請求金額
                    .KoumutenSeikyuuGaku = teibetu_seikyuu_rec.KoumutenSeikyuuGaku
                    '実請求金額
                    .UriGaku = teibetu_seikyuu_rec.UriGaku
                    '承諾書金額
                    .SiireGaku = teibetu_seikyuu_rec.SiireGaku
                    '税区分
                    .ZeiKbn = teibetu_seikyuu_rec.ZeiKbn
                    '税率
                    .Zeiritu = syouhin23_rec.Zeiritu
                    '●消費税額
                    .UriageSyouhiZeiGaku = Integer.MinValue
                    '発注書確定FLG
                    .HattyuusyoKakuteiFlg = 0

                    ' 商品２の場合、商品１と日付連動
                    If strSyouhin23Type = "2" Then
                        ' 売上は無条件
                        '売上年月日
                        .UriDate = jibanRec.Syouhin1Record.UriDate

                        '請求有無
                        If jibanRec.Syouhin1Record.SeikyuuUmu = 1 Then '有
                            ' 商品１，２の請求書発行日、売上年月日の連動
                            .SeikyuusyoHakDate = jibanRec.Syouhin1Record.SeikyuusyoHakDate
                        Else '無
                            Dim cBizLogic As New CommonBizLogic
                            Dim strSimeDate As String
                            '請求締め日の取得
                            strSimeDate = cBizLogic.GetSeikyuuSimeDateFromKameiten(.SeikyuuSakiCd _
                                                                                        , .SeikyuuSakiBrc _
                                                                                        , .SeikyuuSakiKbn _
                                                                                        , jibanRec.KameitenCd _
                                                                                        , .SyouhinCd)
                            ' 商品１の請求有無が無の場合のみ算出
                            .SeikyuusyoHakDate = cBizLogic.CalcSeikyuusyoHakkouDate(strSimeDate)
                        End If
                    End If

                End With

            Case EarthConst.SOUKO_CD_SYOUHIN_3 '商品3

                With jibanRec.Syouhin3Records(intIndex)
                    ' コード、名称をセット（多棟割りの場合、設定必須の為）
                    '倉庫コード(分類コード)
                    .BunruiCd = syouhin23_rec.SoukoCd
                    '画面表示No
                    .GamenHyoujiNo = intIndex
                    '商品コード
                    .SyouhinCd = syouhin23_rec.SyouhinCd

                    ' 価格情報をセット
                    '工務店請求金額
                    .KoumutenSeikyuuGaku = teibetu_seikyuu_rec.KoumutenSeikyuuGaku
                    '実請求金額
                    .UriGaku = teibetu_seikyuu_rec.UriGaku
                    '承諾書金額
                    .SiireGaku = teibetu_seikyuu_rec.SiireGaku
                    '税区分
                    .ZeiKbn = teibetu_seikyuu_rec.ZeiKbn
                    '税率
                    .Zeiritu = syouhin23_rec.Zeiritu
                    '●消費税額
                    .UriageSyouhiZeiGaku = Integer.MinValue
                    '発注書確定FLG
                    .HattyuusyoKakuteiFlg = 0

                    ' 商品３は確定区分をセット
                    If strSyouhin23Type = "3" Then
                        .KakuteiKbn = IIf(.KakuteiKbn = Integer.MinValue, "0", teibetu_seikyuu_rec.KakuteiKbn.ToString())
                        ' 確定状況により、請求書発行日・売上年月日設定
                        Me.SubChangeKakutei(jibanRec.KameitenCd, teibetu_seikyuu_rec)
                        cbLogic.SetDisplayString(.SeikyuusyoHakDate, teibetu_seikyuu_rec.SeikyuusyoHakDate)
                        cbLogic.SetDisplayString(.UriDate, teibetu_seikyuu_rec.UriDate)
                        cbLogic.SetDisplayString(.UriKeijyouFlg, teibetu_seikyuu_rec.UriKeijyouFlg)
                    End If
                End With

            Case Else
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' 商品２、３画面表示用の商品情報を取得します
    ''' </summary>
    ''' <param name="syouhin_type">商品２or３</param>
    ''' <param name="syouhin_cd">商品コード</param>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Function getSyouhinInfo(ByVal syouhin_type As String, _
                                    ByVal syouhin_cd As String, _
                                    ByVal strKamentenCd As String _
                                    ) As Syouhin23Record

        Dim syouhin23_rec As Syouhin23Record = Nothing
        Dim count As Integer = 0

        ' 商品情報を取得する（コード指定なので１件のみ取得される）
        Dim list As List(Of Syouhin23Record) = Me.GetSyouhin23(syouhin_cd, _
                                                                  "", _
                                                                  IIf(syouhin_type = "2", EarthEnum.EnumSyouhinKubun.Syouhin2_110, EarthEnum.EnumSyouhinKubun.Syouhin3), _
                                                                  count, _
                                                                  Integer.MinValue, _
                                                                  strKamentenCd)

        ' 取得できない場合
        If list.Count < 1 Then
            Return syouhin23_rec
        End If

        ' 取得できた場合のみセット
        syouhin23_rec = list(0)

        Return syouhin23_rec

    End Function

    ''' <summary>
    ''' 商品２、３画面表示用の邸別請求データを取得します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="syouhin23_info">商品２，３情報取得用のパラメータクラス</param>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Function getSyouhin23SeikyuuInfo(ByVal sender As Object, _
                                             ByVal syouhin23_info As Syouhin23InfoRecord _
                                             ) As TeibetuSeikyuuRecord

        Dim teibetu_rec As TeibetuSeikyuuRecord = Nothing
        Dim intSetSts As Integer = 0

        ' 請求データの取得
        teibetu_rec = Me.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1, intSetSts)

        If intSetSts <> 0 Then
            teibetu_rec = Nothing
        End If

        Return teibetu_rec

    End Function

#End Region

#End Region

#Region "商品３確定区分変更処理"

    ''' <summary>
    ''' 商品コード３の確定区分変更時の処理を行います
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <param name="rec">区分変更対象となる商品３レコードの邸別請求レコード</param>
    ''' <remarks>判定に使用する項目：確定区分</remarks>
    Public Sub SubChangeKakutei(ByVal kameitenCd As String, _
                                ByRef rec As TeibetuSeikyuuRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SubChangeKakutei", _
                                            kameitenCd, _
                                            rec)
        Dim cBizLogic As New CommonBizLogic

        ' 確定区分未設定時は無し：0とする
        If rec.KakuteiKbn = Integer.MinValue Then
            rec.KakuteiKbn = 0
        End If

        ' 無しの場合、請求書発行日、売上年月日をクリアして終了
        If rec.KakuteiKbn = 0 Then
            rec.SeikyuusyoHakDate = Date.MinValue
            rec.UriDate = Date.MinValue
            Exit Sub
        End If

        ' 請求有無未指定時、処理終了
        If rec.SeikyuuUmu = Integer.MinValue Then
            Exit Sub
        End If

        ' 請求有無：ありの場合、請求書発行日を設定する
        If rec.SeikyuuUmu = 1 Then
            ' 請求書発行日は未指定の場合のみ設定する
            If rec.SeikyuusyoHakDate = Date.MinValue Then
                Dim strSimeDate As String
                '請求書締め日を取得
                strSimeDate = cBizLogic.GetSeikyuuSimeDateFromKameiten(rec.SeikyuuSakiCd _
                                                                        , rec.SeikyuuSakiBrc _
                                                                        , rec.SeikyuuSakiKbn _
                                                                        , kameitenCd _
                                                                        , rec.SyouhinCd)
                ' 請求書発行日をセット
                rec.SeikyuusyoHakDate = cBizLogic.CalcSeikyuusyoHakkouDate(strSimeDate)
            End If
        End If

        ' 売上年月日：未入力の場合設定する
        If rec.UriDate = Date.MinValue Then
            rec.UriDate = Date.Now
        End If

    End Sub

#End Region

#Region "特定店商品コード2取得"
    ''' <summary>
    ''' 特定の加盟店に自動設定される商品コード２を取得します
    ''' 取得条件を満たない場合、商品コードを取得できない場合、Falseを返します
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <param name="syouhinCd2List">商品コード２リスト</param>
    ''' <returns></returns>
    ''' <remarks>区分：Aの場合は自動設定されません</remarks>
    Public Function GetTokuteitenSyouhin2(ByVal kameitenCd As String, _
                                          ByRef syouhinCd2List As List(Of String)) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokuteitenSyouhin2", _
                                            kameitenCd, _
                                            syouhinCd2List)

        '****************************************************
        ' 引数のチェック（一つでもNGの場合Falseで処理終了）
        '****************************************************
        ' 加盟店コードのチェック
        If kameitenCd.Trim() = "" Then
            ' 加盟店コードが""の場合処理終了
            Return False
        End If

        Dim data_access As New SyouhinDataAccess
        Dim syouhinDataTable As New SyouhinDataSet.SyouhinTableDataTable

        ' 特定店商品２を取得する
        syouhinDataTable = data_access.GetTokuteitenSyouhin2(kameitenCd)

        If syouhinDataTable.Rows.Count <= 0 Then
            Return False
        Else
            For Each syouhinRow As SyouhinDataSet.SyouhinTableRow In syouhinDataTable.Rows
                syouhinCd2List.Add(syouhinRow.syouhin_cd)
            Next
        End If

        Return True

    End Function
#End Region

#Region "多棟割商品コード2取得"
    ''' <summary>
    ''' 同時依頼棟数による多棟割商品コードを取得します
    ''' 取得条件を満たない場合、商品コードを取得できない場合、Falseを返します
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <param name="doujiIraiTousuu">同時依頼棟数（4以上）</param>
    ''' <param name="syouhinCd2"></param>
    ''' <returns></returns>
    ''' <remarks>区分：Aの場合は自動設定されません</remarks>
    Public Function GetTatouwariSyouhinCd2(ByVal kubun As String, _
                                           ByVal kameitenCd As String, _
                                           ByVal doujiIraiTousuu As Integer, _
                                           ByRef syouhinCd2 As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTatouwariSyouhinCd2", _
                                            kubun, _
                                            kameitenCd, _
                                            doujiIraiTousuu, _
                                            syouhinCd2)

        '****************************************************
        ' 引数のチェック（一つでもNGの場合Falseで処理終了）
        '****************************************************
        ' 区分のチェック
        If kubun.Trim() = "A" Then
            ' 区分が"A"の場合処理終了
            Return False
        End If

        ' 加盟店コードのチェック
        If kameitenCd.Trim() = "" Then
            ' 加盟店コードが""の場合処理終了
            Return False
        End If

        ' 同時依頼棟数のチェック
        If doujiIraiTousuu < 4 Then
            ' 同時依頼棟数が4未満の場合処理終了
            Return False
        End If

        ' 棟区分
        Dim touKbn As Integer

        '同時依頼棟数をもとに棟区分(KEY)を設定
        If doujiIraiTousuu >= 4 And doujiIraiTousuu <= 9 Then
            touKbn = 1
        ElseIf doujiIraiTousuu >= 10 And doujiIraiTousuu <= 19 Then
            touKbn = 2
        Else
            touKbn = 3
        End If

        Dim data_access As New SyouhinDataAccess

        ' 多棟割の商品コード２を取得する
        syouhinCd2 = data_access.GetTatouwariSyouhinCd(kameitenCd, touKbn)

        ' 取得できない場合、条件を変更し再検索する
        If syouhinCd2 = "" Then
            syouhinCd2 = data_access.GetTatouwariSyouhinCd(EarthConst.SH_CD_TATOUWARI, touKbn)
        End If

        ' 取得できない場合、又は取得したコードが"00000"の場合、Falseで処理終了
        If syouhinCd2 = "" Or syouhinCd2 = EarthConst.SH_CD_TATOUWARI_ER Then
            Return False
        End If

        Return True

    End Function
#End Region

#Region "お知らせ情報の取得"
    ''' <summary>
    ''' お知らせデータを取得します
    ''' </summary>
    ''' <returns>お知らせデータのレコードリスト</returns>
    ''' <remarks></remarks>
    Public Function GetOsiraseRecords() As List(Of OsiraseRecord)

        ' 戻り値となるリスト
        Dim returnRec As New List(Of OsiraseRecord)

        ' 重複データ取得クラス
        Dim dataAccess As New OsiraseDataAccess

        ' 値が取得できた場合、戻り値に設定する
        For Each row As OsiraseDataSet.OsiraseTableRow In dataAccess.GetOsiraseData()
            Dim osiraseRec As New OsiraseRecord

            osiraseRec.Nengappi = row.nengappi
            osiraseRec.NyuuryokuBusyo = row.nyuuryoku_busyo
            osiraseRec.NyuuryokuMei = row.nyuuryoku_mei
            osiraseRec.HyoujiNaiyou = row.hyouji_naiyou
            osiraseRec.LinkSaki = row.link_saki
            ' リストにセット
            returnRec.Add(osiraseRec)

        Next

        Return returnRec

    End Function
#End Region

#Region "地盤データ取得"
    ''' <summary>
    ''' 地盤データを取得します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="isNotDataHaki">データ破棄種別判断フラグ</param>
    ''' <returns>地盤レコード</returns>
    ''' <remarks>地盤レコードには紐付く邸別請求データが格納されております</remarks>
    Public Function GetJibanData(ByVal kbn, _
                                 ByVal hosyousyoNo, _
                        Optional ByVal isNotDataHaki = False) As JibanRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            isNotDataHaki)

        ' 地盤データ取得用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 地盤データの取得
        Dim jibanList As List(Of JibanRecord) = DataMappingHelper.Instance.getMapArray(Of JibanRecord)(GetType(JibanRecord), _
        dataAccess.GetJibanData(kbn, hosyousyoNo, isNotDataHaki))

        ' 地盤データ保持用のレコードクラス
        Dim jibanRec As New JibanRecord

        ' データが取得できた場合、レコードクラスに設定
        If jibanList.Count > 0 Then
            jibanRec = jibanList(0)

            '*****************************************************************
            ' 邸別請求データを取得する
            '*****************************************************************
            Dim teibetuSeikyuuList As List(Of TeibetuSeikyuuRecord) = _
            DataMappingHelper.Instance.getMapArray(Of TeibetuSeikyuuRecord)(GetType(TeibetuSeikyuuRecord), _
            dataAccess.GetTeibetuSeikyuuData(kbn, hosyousyoNo))

            ' 商品コード２のリスト
            Dim syouhin2Hash As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
            ' 商品コード３のリスト
            Dim syouhin3Hash As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
            ' 商品コード４のリスト
            Dim syouhin4Hash As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
            ' 商品以外の邸別請求リスト
            Dim otherList As New List(Of TeibetuSeikyuuRecord)
            ' 取得レコード分設定を行う
            For Each rec As TeibetuSeikyuuRecord In teibetuSeikyuuList

                ' 分類により格納場所を変更
                Select Case rec.BunruiCd
                    Case EarthConst.SOUKO_CD_SYOUHIN_1
                        ' 商品コード１は１件なのでレコードをセット
                        jibanRec.Syouhin1Record = rec
                    Case EarthConst.SOUKO_CD_SYOUHIN_2_110
                        ' 商品コード２(110)は複数件なのでDictionaryにセット
                        syouhin2Hash.Add(rec.GamenHyoujiNo, rec)
                    Case EarthConst.SOUKO_CD_SYOUHIN_2_115
                        '' 金額はマイナスにする（絶対値*-1）
                        'rec.KoumutenSeikyuuGaku = Math.Abs(rec.KoumutenSeikyuuGaku) * -1
                        'rec.UriGaku = Math.Abs(rec.UriGaku) * -1
                        'rec.SiireGaku = Math.Abs(rec.SiireGaku) * -1
                        ' 商品コード２(115)は複数件なのでDictionaryにセット
                        syouhin2Hash.Add(rec.GamenHyoujiNo, rec)
                    Case EarthConst.SOUKO_CD_SYOUHIN_3
                        ' 商品コード３は複数件なのでDictionaryにセット
                        syouhin3Hash.Add(rec.GamenHyoujiNo, rec)
                    Case EarthConst.SOUKO_CD_SYOUHIN_4
                        ' 商品コード４は複数件なのでDictionaryにセット
                        syouhin4Hash.Add(rec.GamenHyoujiNo, rec)
                    Case EarthConst.SOUKO_CD_KAIRYOU_KOUJI
                        ' 改良工事は１件なのでレコードをセット
                        jibanRec.KairyouKoujiRecord = rec
                    Case EarthConst.SOUKO_CD_TUIKA_KOUJI
                        ' 追加工事は１件なのでレコードをセット
                        jibanRec.TuikaKoujiRecord = rec
                    Case EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO
                        ' 調査報告書は１件なのでレコードをセット
                        jibanRec.TyousaHoukokusyoRecord = rec
                    Case EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO
                        ' 工事報告書は１件なのでレコードをセット
                        jibanRec.KoujiHoukokusyoRecord = rec
                    Case EarthConst.SOUKO_CD_HOSYOUSYO
                        ' 保証書は１件なのでレコードをセット
                        jibanRec.HosyousyoRecord = rec
                    Case EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI
                        ' 金額はマイナスにする（絶対値*-1）
                        rec.KoumutenSeikyuuGaku = Math.Abs(rec.KoumutenSeikyuuGaku) * -1
                        rec.UriGaku = Math.Abs(rec.UriGaku) * -1
                        rec.SiireGaku = Math.Abs(rec.SiireGaku) * -1
                        ' 解約払戻は１件なのでレコードをセット
                        jibanRec.KaiyakuHaraimodosiRecord = rec
                    Case Else
                        ' 上記以外はその他用リストにセット
                        otherList.Add(rec)
                End Select

            Next

            ' 商品コード２のリストにデータが存在する場合、地盤レコードにセット
            If syouhin2Hash.Count > 0 Then
                jibanRec.Syouhin2Records = syouhin2Hash
            End If

            ' 商品コード３のリストにデータが存在する場合、地盤レコードにセット
            If syouhin3Hash.Count > 0 Then
                jibanRec.Syouhin3Records = syouhin3Hash
            End If

            ' 商品コード４のリストにデータが存在する場合、地盤レコードにセット
            If syouhin4Hash.Count > 0 Then
                jibanRec.Syouhin4Records = syouhin4Hash
            End If

            ' 商品以外の邸別請求リストにデータが存在する場合、地盤レコードにセット
            If otherList.Count > 0 Then
                jibanRec.OtherTeibetuSeikyuuRecords = otherList
            End If


            '*****************************************************************
            ' 邸別入金データを取得する(分類コードをKeyとするDictionary)
            '*****************************************************************
            Dim listTeibetuNyuukin As List(Of TeibetuNyuukinRecord) = _
            DataMappingHelper.Instance.getMapArray(Of TeibetuNyuukinRecord)(GetType(TeibetuNyuukinRecord), _
            dataAccess.GetTeibetuNyuukinData(kbn, hosyousyoNo))

            ' 邸別入金リスト
            Dim dicTeibetuNyuukin As New Dictionary(Of String, TeibetuNyuukinRecord)

            ' 取得レコード分設定を行う
            For Each rec As TeibetuNyuukinRecord In listTeibetuNyuukin
                dicTeibetuNyuukin.Add(rec.BunruiCd, rec)
            Next

            ' 邸別入金リストにデータが存在する場合、地盤レコードにセット
            If dicTeibetuNyuukin.Count > 0 Then
                jibanRec.TeibetuNyuukinRecords = dicTeibetuNyuukin
            End If

            '*****************************************************************
            ' 邸別入金データを取得する
            '*****************************************************************
            Dim listTeibetuNyuukinMeisai As List(Of TeibetuNyuukinRecord) = _
            DataMappingHelper.Instance.getMapArray(Of TeibetuNyuukinRecord)(GetType(TeibetuNyuukinRecord), _
            dataAccess.GetTeibetuNyuukinDataKey(kbn, hosyousyoNo))
            Dim listTeibetuNyuukinUpdateMeisai As New List(Of TeibetuNyuukinUpdateRecord)
            Dim updRec As TeibetuNyuukinUpdateRecord

            For Each rec As TeibetuNyuukinRecord In listTeibetuNyuukinMeisai
                updRec = New TeibetuNyuukinUpdateRecord
                updRec.TeibetuNyuukinrecord = rec
                updRec.BunruiCd = rec.BunruiCd
                updRec.GamenHyoujiNo = rec.GamenHyoujiNo
                listTeibetuNyuukinUpdateMeisai.Add(updRec)
            Next
            If listTeibetuNyuukinUpdateMeisai.Count > 0 Then
                jibanRec.TeibetuNyuukinLists = listTeibetuNyuukinUpdateMeisai
            End If

        End If

        Return jibanRec

    End Function

    ''' <summary>
    ''' 検索画面用地盤データを取得します
    ''' </summary>
    ''' <param name="keyRec">地盤Keyレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>地盤検索用レコードのList(Of JibanSearchRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetJibanSearchData(ByVal sender As Object, _
                                       ByVal keyRec As JibanKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer) As List(Of JibanSearchRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanSearchData", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount)

        ' 検索条件生成クラス
        Dim keyRecAcc As New JibanSearchKeyRecord
        ' 検索実行クラス
        Dim dataAccess As New JibanDataAccess
        ' 取得データ格納用リスト
        Dim list As New List(Of JibanSearchRecord)

        Try
            ' 検索条件生成クラスのプロパティと引数の地盤Keyレコードデータをマッピング
            RecordMappingHelper.Instance.CopyRecordData(keyRec, keyRecAcc)

            ' 検索処理実行
            Dim table As DataTable = dataAccess.GetJibanSearchData(keyRecAcc)

            ' 総件数をセット
            allCount = table.Rows.Count

            ' 検索結果を格納用リストにセット
            list = DataMappingHelper.Instance.getMapArray(Of JibanSearchRecord)(GetType(JibanSearchRecord), table, startRow, endRow)

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return list

    End Function

#Region "品質保証書状況検索画面用レコード取得"

    ''' <summary>
    ''' 地盤データより、検索条件に合致するレコードを取得する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyRec">品質Keyレコード</param>
    '''  <param name="startRow">開始行</param>
    '''  <param name="endRow">最終行</param>
    '''  <param name="allCount">全件数</param>
    ''' <returns>品質検索用レコードのList(Of HinsituHosyousyoJyoukyouRecord)</returns>
    ''' <remarks></remarks>
    Public Function getJibanSearchHinsituRecord(ByVal sender As Object, _
                                                ByVal keyRec As HinsituHosyousyoJyoukyouRecord, _
                                                ByVal startRow As Integer, _
                                                ByVal endRow As Integer, _
                                                ByRef allCount As Integer) As List(Of HinsituHosyousyoJyoukyouSearchRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getJibanSearchHinsituRecord", _
                                                    keyRec, _
                                                    startRow, _
                                                    endRow, _
                                                    allCount)

        'データアクセスクラス
        Dim clsDataAcc As New JibanDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New List(Of HinsituHosyousyoJyoukyouSearchRecord)

        Try

            ' 検索処理実行
            dTblResult = clsDataAcc.GetJibanDataHinsitu(keyRec)

            ' 総件数をセット
            allCount = dTblResult.Rows.Count

            If dTblResult.Rows.Count > 0 Then
                ' 検索結果を格納用リストにセット
                recResult = DataMappingHelper.Instance.getMapArray(Of HinsituHosyousyoJyoukyouSearchRecord)(GetType(HinsituHosyousyoJyoukyouSearchRecord), dTblResult, startRow, endRow)
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return recResult

    End Function

    ''' <summary>
    ''' 地盤データより、CSV出力用データを取得する
    ''' </summary>
    ''' <param name="keyRec">Keyレコード</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>CSV出力用データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetJibanSearchHinsituRecordCsv(ByVal sender As Object, _
                                       ByVal keyRec As HinsituHosyousyoJyoukyouRecord, _
                                       ByRef allCount As Integer) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanSearchHinsituRecordCsv", _
                                            keyRec, _
                                            allCount)

        '検索実行クラス
        Dim dataAccess As New JibanDataAccess
        '取得データ格納用リスト
        Dim list As New List(Of HinsituHosyousyoJyoukyouRecord)

        Dim dtRet As New DataTable

        Try
            '検索処理の実行
            dtRet = dataAccess.GetJibanDataHinsituCsv(keyRec)

            ' 総件数をセット
            allCount = dtRet.Rows.Count

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return dtRet
    End Function

    ''' <summary>
    ''' 地盤データより、検索条件に合致するレコードを取得する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyRec">品質Keyレコード</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="endRow">最終行</param>
    '''  <param name="allCount">全件数</param>
    ''' <returns>品質検索用レコードのList(Of HinsituHosyousyoJyoukyouRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetJibanSearchIkkatuHinsituRecord(ByVal sender As Object, _
                                                ByVal keyRec As HinsituHosyousyoJyoukyouRecord, _
                                                ByVal startRow As Integer, _
                                                ByVal endRow As Integer, _
                                                ByRef allCount As Integer) As HinsituHosyousyoJyoukyouSearchRecord
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getJibanSearchHinsituRecord", _
                                                    keyRec, _
                                                    startRow, _
                                                    endRow, _
                                                    allCount)

        'データアクセスクラス
        Dim clsDataAcc As New JibanDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As HinsituHosyousyoJyoukyouSearchRecord = Nothing

        Try
            ' 検索処理実行
            dTblResult = clsDataAcc.GetJibanDataHinsitu(keyRec)

            ' 総件数をセット
            allCount = dTblResult.Rows.Count

            'セット先データテーブルのカラム名一覧のハッシュを設定
            Dim hashColumnNames As Hashtable = DataMappingHelper.Instance.getColumnNamesHashtable(dTblResult)

            For Each row As DataRow In dTblResult.Rows

                If dTblResult.Rows.Count > 0 Then
                    ' 検索結果を格納用リストにセット
                    recResult = DataMappingHelper.Instance.propertyMap((GetType(HinsituHosyousyoJyoukyouSearchRecord)), row, hashColumnNames)
                End If

            Next

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return recResult

    End Function

#End Region
#End Region

#Region "地盤データ追加"
    ''' <summary>
    ''' 地盤データを追加します。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="intRentouBukkenSuu">連棟物件数</param>
    ''' <returns>True:登録成功 False:登録失敗</returns>
    ''' <remarks></remarks>
    Public Function InsertJibanData(ByRef sender As Object, _
                                    ByRef kbn As String, _
                                    ByRef hosyousyoNo As String, _
                                    ByRef strLoginUserId As String, _
                                    ByRef intRentouBukkenSuu As Integer, _
                                    ByVal sinkiTourokuMotoKbnType As EarthEnum.EnumSinkiTourokuMotoKbnType _
                                    ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertJibanData", _
                                            sender, _
                                            kbn, _
                                            hosyousyoNo, _
                                            strLoginUserId, _
                                            intRentouBukkenSuu, _
                                            sinkiTourokuMotoKbnType _
                                            )


        Dim intTmpBangou As Integer = 0 '番号(作業用)
        Dim intCnt As Integer 'カウンタ
        Dim strTmpBangou As String = "" '番号(作業用)

        If intRentouBukkenSuu <= 0 Then '連棟物件数
            intRentouBukkenSuu = 1
        End If

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 地盤データ取得用データアクセスクラス
                Dim dataAccess As JibanDataAccess = New JibanDataAccess

                Select Case sinkiTourokuMotoKbnType
                    Case EarthEnum.EnumSinkiTourokuMotoKbnType.ReportJHS_FC
                        'FC申込の場合、既に採番済
                    Case Else
                        If kbn <> "" Then
                            ' 保証書NOを採番取得する
                            hosyousyoNo = GetNewHosyousyoNo(sender, kbn, intRentouBukkenSuu, strLoginUserId)
                        End If
                End Select

                ' 空白の場合、保証書NO年月が未登録（地盤システムで登録する必要あり）
                If hosyousyoNo = "" Then
                    mLogic.AlertMessage(sender, Messages.MSG002E)
                    Return False
                End If

                '番号(保証書NO)を作業用にコピー
                strTmpBangou = hosyousyoNo

                '連棟物件数分ループ
                For intCnt = 0 To intRentouBukkenSuu - 1

                    '地盤データに登録
                    If dataAccess.InsertJibanData(kbn, strTmpBangou, strLoginUserId, sinkiTourokuMotoKbnType) <= 0 Then
                        '登録に失敗したので処理中断
                        Return False
                    End If

                    ' 連携用テーブルに登録する（地盤－登録）
                    Dim renkeiJibanRec As New JibanRenkeiRecord
                    renkeiJibanRec.Kbn = kbn
                    renkeiJibanRec.HosyousyoNo = strTmpBangou
                    renkeiJibanRec.RenkeiSijiCd = 1
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = strLoginUserId

                    If dataAccess.InsertJibanRenkeiData(renkeiJibanRec) <> 1 Then
                        ' 登録に失敗したので処理中断
                        Return False
                    End If

                    intTmpBangou = CInt(strTmpBangou) + 1 'カウンタ
                    strTmpBangou = Format(intTmpBangou, "0000000000") 'フォーマット

                Next

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = 2627 Then  'エラーキャッチ：主キーの重複
                mLogic.AlertMessage(sender, String.Format(Messages.MSG110E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function
#End Region

#Region "地盤データ更新"
    ''' <summary>
    ''' 地盤データを更新します。関連する邸別請求データの更新も行われます
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">更新対象の地盤レコード</param>
    ''' <param name="jibanRecAfterExe">更新対象の地盤レコード</param>
    ''' <param name="brRec">物件履歴レコード</param>
    ''' <param name="listRec">特別対応レコード郡</param>
    ''' <returns>True:更新成功 False:エラーによる更新失敗</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanData(ByVal sender As Object, _
                                    ByVal jibanRec As JibanRecord, _
                                    ByRef jibanRecAfterExe As JibanRecord, _
                                    ByVal brRec As BukkenRirekiRecord, _
                                    ByVal listRec As List(Of TokubetuTaiouRecordBase) _
                                    ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanData", _
                                            sender, _
                                            jibanRec, _
                                            jibanRecAfterExe, _
                                            brRec, _
                                            listRec _
                                            )

        ' Updateに必要なSQL情報を自動生成するクラス
        Dim upadteMake As New UpdateStringHelper
        ' 排他制御用SQL文
        Dim sqlString As String = ""
        ' Update文
        Dim updateString As String = ""
        ' 排他チェック用パラメータの情報を格納するList(Of ParamRecord)
        Dim haitaList As New List(Of ParamRecord)
        ' 更新用パラメータの情報を格納するList(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        ' 排他チェック用レコード作成
        Dim jibanHaitaRec As New JibanHaitaRecord

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 新規登録、更新の判定（保証書NO採番時の新規登録を除く）
        Dim isNew As Boolean
        isNew = dataAccess.ChkJibanNew(jibanRec.Kbn, jibanRec.HosyousyoNo)

        ' 連携テーブルデータ登録用データの格納用
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' 更新日付取得
        Dim updDateTime As DateTime = DateTime.Now

        ' ReportJHS連係対象フラグ(デフォルト：False)
        Dim flgEditReportIf As Boolean

        ' 連棟物件数チェック
        If jibanRec.RentouBukkenSuu = Nothing OrElse jibanRec.RentouBukkenSuu <= 0 Then
            jibanRec.RentouBukkenSuu = 1
        End If
        ' 処理件数チェック
        If jibanRec.SyoriKensuu = Nothing OrElse jibanRec.SyoriKensuu <= 0 Then
            jibanRec.SyoriKensuu = 0
        End If

        ' 番号(保証書NO) 作業用
        Dim intTmpBangou As Integer = CInt(jibanRec.HosyousyoNo) + jibanRec.SyoriKensuu '番号+処理件数
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") 'フォーマット

        Dim strRetBangou As String = jibanRec.HosyousyoNo '画面再描画用

        ' 区分、保証書NO
        Dim kbn As String = jibanRec.Kbn
        Dim hosyousyoNo As String = jibanRec.HosyousyoNo

        ' 経由退避用
        Dim intInitKeiyu As Integer = jibanRec.Keiyu

        Dim intCnt As Integer  '処理カウンタ
        Dim intMax As Integer = 20 '処理最大数

        Dim pUpdDateTime As DateTime
        '更新日時取得（システム日時）
        pUpdDateTime = DateTime.Now

        Dim htTtUpdFlg As New Dictionary(Of Integer, Integer)
        Dim htTtDateTime As New Dictionary(Of Integer, DateTime)

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '20件ずつ処理
                For intCnt = 1 To intMax

                    '*************************
                    ' 連棟処理対応
                    '*************************
                    '処理件数 >= 連棟物件数 の場合、全処理終了
                    If jibanRec.SyoriKensuu >= jibanRec.RentouBukkenSuu Then
                        jibanRec.SyoriKensuu = jibanRec.RentouBukkenSuu '処理件数 = 連棟物件数
                        Exit For
                    End If

                    '更新対象の番号を指定
                    jibanRec.HosyousyoNo = strTmpBangou '地盤テーブル

                    ' 地盤データより区分、保証書NOを取得（空レコード確認用）
                    kbn = jibanRec.Kbn
                    hosyousyoNo = jibanRec.HosyousyoNo

                    '●物件名寄コードの自動設定
                    If jibanRec.BukkenNayoseCdFlg Then
                        jibanRec.BukkenNayoseCd = kbn & hosyousyoNo '自物件NOをセット
                    End If

                    If jibanRec.SyoriKensuu <= 0 Then
                        If Not listRec Is Nothing Then
                            For intTokuCnt As Integer = 0 To listRec.Count - 1
                                '更新フラグ
                                htTtUpdFlg.Add(listRec(intTokuCnt).TokubetuTaiouCd, listRec(intTokuCnt).UpdFlg)
                                '更新日時
                                If listRec(intTokuCnt).UpdDatetime <> DateTime.MinValue Then
                                    htTtDateTime.Add(listRec(intTokuCnt).TokubetuTaiouCd, listRec(intTokuCnt).UpdDatetime)
                                End If
                            Next
                        End If
                    End If

                    '*************************
                    ' 排他チェック処理
                    '*************************
                    ' 地盤レコードの同一項目を複製
                    RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

                    ' 排他チェック用SQL文自動生成
                    sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

                    ' 排他チェック実施
                    Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                                DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                                dataAccess.CheckHaita(sqlString, haitaList))

                    If haitaKekkaList.Count > 0 Then
                        Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                        ' 排他チェックエラー
                        mLogic.CallHaitaErrorMessage(sender, haitaErrorData.UpdLoginUserId, haitaErrorData.UpdDatetime, "地盤テーブル")
                        Return False

                    End If

                    '*************************
                    ' 与信チェック処理
                    '*************************
                    Dim jibanRecOld As New JibanRecord
                    jibanRecOld = GetJibanData(kbn, hosyousyoNo)

                    Dim YosinLogic As New YosinLogic
                    Dim intYosinResult As Integer = YosinLogic.YosinCheck(1, jibanRecOld, jibanRec)
                    Select Case intYosinResult
                        Case EarthConst.YOSIN_ERROR_YOSIN_OK
                            'エラーなし
                        Case EarthConst.YOSIN_ERROR_YOSIN_NG
                            '与信限度額超過
                            mLogic.AlertMessage(sender, Messages.MSG089E, 1)
                            Return False
                        Case Else
                            '6:与信管理情報取得エラー
                            '7:与信管理テーブル更新エラー
                            '8:メール送信処理エラー
                            '9:その他エラー
                            mLogic.AlertMessage(sender, String.Format(Messages.MSG090E, intYosinResult.ToString()), 1)
                            Return False
                    End Select

                    '*************************
                    ' 更新履歴テーブルの登録
                    '*************************
                    ' 施主名
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.SesyuMei, _
                                                   EarthConst.RIREKI_SESYU_MEI, _
                                                   jibanRec.SesyuMei, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 物件住所
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.Juusyo, _
                                                   EarthConst.RIREKI_BUKKEN_JYUUSYO, _
                                                   jibanRec.BukkenJyuusyo1 & jibanRec.BukkenJyuusyo2, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 加盟店コード
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.KameitenCd, _
                                                   EarthConst.RIREKI_BUKKEN_KAMEITEN_CD, _
                                                   jibanRec.KameitenCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 調査会社
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.TyousaKaisya, _
                                                   EarthConst.RIREKI_TYOUSA_KAISYA, _
                                                   jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 備考
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.Bikou, _
                                                   EarthConst.RIREKI_BIKOU, _
                                                   jibanRec.Bikou, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 連携用テーブルに登録する（地盤－更新）
                    renkeiJibanRec.Kbn = kbn
                    renkeiJibanRec.HosyousyoNo = hosyousyoNo
                    renkeiJibanRec.RenkeiSijiCd = 2
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                    If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                        ' 登録に失敗したので処理中断
                        Return False
                    End If

                    '*************************
                    ' R-JHS連携チェック
                    '*************************
                    ' ReportJHS連係対象チェックを行う(経由：0,1,5の場合のみ)
                    flgEditReportIf = False '初期化

                    '経由=0or1or5(先頭物件が反映対象外(経由=9)と判定された場合、以後以下の処理はスルー)
                    If jibanRec.Keiyu = 0 Or _
                        jibanRec.Keiyu = 1 Or _
                        jibanRec.Keiyu = 5 Then

                        'R-JHS連携チェック
                        If Me.ChkRJhsRenkei(jibanRec.TysKaisyaCd, jibanRec.TysKaisyaJigyousyoCd) Then
                            ' 対象の場合、フラグを立てる
                            flgEditReportIf = True
                            ' 反映対象なので経由を5に変更
                            jibanRec.Keiyu = 5
                        Else
                            ' 連携調査会社設定マスタの存在チェックでNGになった場合、経由を9に変更
                            jibanRec.Keiyu = 9
                        End If
                    End If

                    '連棟処理の場合
                    If jibanRec.RentouBukkenSuu > 1 Then
                        'R-JHS連携対象の場合
                        If flgEditReportIf Then
                            If intInitKeiyu = 0 AndAlso jibanRec.SyoriKensuu > 0 Then '画面.経由=0でかつ、2棟目以降の場合
                                '連棟時の連携で経由=0の場合、1棟目のみ連携
                                '1棟目：連携し、経由が5or9になる
                                '2棟目以降：連携せず、経由は0のまま
                                jibanRec.Keiyu = intInitKeiyu
                                flgEditReportIf = False
                            End If
                        End If
                    End If

                    '*************************
                    ' 地盤テーブルの更新
                    '*************************
                    ' 更新用UPDATE文自動生成
                    updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecord), jibanRec, list)

                    ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                    If dataAccess.UpdateJibanData(updateString, list) = True Then

                        '**************************************************************************
                        ' 邸別請求データの追加・更新・削除
                        '**************************************************************************
                        '邸別請求テーブルデータ操作用
                        Dim TeibetuSyuuseiLogic As New TeibetuSyuuseiLogic
                        Dim tempTeibetuRec As New TeibetuSeikyuuRecord

                        '***************************
                        ' 商品コード１
                        '***************************
                        '商品１レコードをテンポラリにセット
                        tempTeibetuRec = jibanRec.Syouhin1Record

                        If tempTeibetuRec IsNot Nothing Then
                            '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                            If jibanRec.RentouBukkenSuu > 1 Then
                                tempTeibetuRec.HosyousyoNo = strTmpBangou
                            End If
                        End If

                        '商品１データ処理
                        If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                 tempTeibetuRec, _
                                                                 EarthConst.SOUKO_CD_SYOUHIN_1, _
                                                                 1, _
                                                                 jibanRec, _
                                                                 renkeiTeibetuList, _
                                                                 GetType(TeibetuSeikyuuRecord) _
                                                                 ) = False Then
                            Return False
                        End If

                        '***************************
                        ' 商品コード２
                        '***************************
                        Dim i As Integer
                        For i = 1 To EarthConst.SYOUHIN2_COUNT
                            If jibanRec.Syouhin2Records.ContainsKey(i) = True Then

                                '商品２レコードをテンポラリにセット
                                tempTeibetuRec = jibanRec.Syouhin2Records.Item(i)

                                '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                                If jibanRec.RentouBukkenSuu > 1 Then
                                    tempTeibetuRec.HosyousyoNo = strTmpBangou
                                End If

                                ' 商品２の邸別請求データを更新します
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         tempTeibetuRec, _
                                                                         tempTeibetuRec.BunruiCd, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList, _
                                                                         GetType(TeibetuSeikyuuRecord) _
                                                                 ) = False Then
                                    Return False
                                End If
                            Else
                                ' 削除処理用(削除確認は商品２の分類コード何れかで問題なし)
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         Nothing, _
                                                                         EarthConst.SOUKO_CD_SYOUHIN_2_110, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList _
                                                                 ) = False Then
                                    Return False
                                End If
                            End If
                        Next

                        '***************************
                        ' 商品コード３
                        '***************************
                        i = 1
                        For i = 1 To EarthConst.SYOUHIN3_COUNT
                            If jibanRec.Syouhin3Records.ContainsKey(i) = True Then

                                '商品３レコードをテンポラリにセット
                                tempTeibetuRec = jibanRec.Syouhin3Records.Item(i)

                                '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                                If jibanRec.RentouBukkenSuu > 1 Then
                                    tempTeibetuRec.HosyousyoNo = strTmpBangou
                                End If

                                ' 商品３の邸別請求データを更新します
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         tempTeibetuRec, _
                                                                         tempTeibetuRec.BunruiCd, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList, _
                                                                         GetType(TeibetuSeikyuuRecord) _
                                                                 ) = False Then
                                    Return False
                                End If
                            Else
                                ' 削除処理用(削除確認は商品３の分類コード何れかで問題なし)
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         Nothing, _
                                                                         EarthConst.SOUKO_CD_SYOUHIN_3, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList _
                                                                 ) = False Then
                                    Return False
                                End If
                            End If
                        Next

                        ' 邸別請求連携反映対象が存在する場合、反映を行う
                        For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                            ' 連携用テーブルに反映する（邸別請求）
                            If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then
                                ' 登録に失敗したので処理中断
                                Return False
                            End If
                        Next

                        '*************************
                        ' R-JHS連携更新
                        '*************************
                        '先にチェックしておいたReportJHS連係対象チェックを元に、進捗データテーブル更新処理を行う
                        If flgEditReportIf Then
                            ' 進捗データテーブル更新処理呼出
                            If EditReportIfData(jibanRec) = False Then
                                ' エラー発生時処理終了
                                Return False
                            End If
                        End If

                        ' ●物件履歴テーブル追加(保証有無変更時のみ、物件履歴Tに新規追加する)
                        If Not brRec Is Nothing Then
                            Dim brLogic As New BukkenRirekiLogic

                            '新規追加用スクリプトおよび実行
                            brRec.Kbn = jibanRec.Kbn
                            brRec.HosyousyoNo = jibanRec.HosyousyoNo
                            If brLogic.InsertBukkenRireki(brRec) = False Then
                                Return False
                            End If
                        End If

                        '**********************************************
                        ' 特別対応データの更新(邸別請求データとの紐付)
                        '**********************************************
                        '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                        If jibanRec.RentouBukkenSuu > 1 Then
                            If Not listRec Is Nothing Then
                                '連棟用の保証書Noを連番でふりなおす
                                For intTokuCnt As Integer = 0 To listRec.Count - 1
                                    With listRec(intTokuCnt)
                                        '番号
                                        .HosyousyoNo = strTmpBangou
                                        '更新フラグ
                                        If htTtUpdFlg.ContainsKey(.TokubetuTaiouCd) Then
                                            .UpdFlg = htTtUpdFlg(.TokubetuTaiouCd)
                                        End If
                                        '更新日時
                                        If htTtDateTime.ContainsKey(.TokubetuTaiouCd) Then
                                            .UpdDatetime = htTtDateTime(.TokubetuTaiouCd)
                                        End If
                                    End With
                                Next
                            End If
                        End If

                        '特別対応データ更新
                        If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.Jutyuu) = False Then
                            Return False
                            Exit Function
                        End If

                        '*************************
                        ' 連棟処理対応
                        '*************************
                        '番号をカウントアップ
                        intTmpBangou = CInt(strTmpBangou) + 1 'カウンタ
                        strTmpBangou = Format(intTmpBangou, "0000000000") 'フォーマット

                        jibanRec.SyoriKensuu += 1 '処理件数

                    Else
                        Return False
                    End If

                Next

                '●物件名寄状況の最終チェック
                If Me.ChkLatestBukkenNayoseJyky(sender, jibanRec) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

                '*************************
                ' 連棟処理対応
                '*************************
                '画面再描画用に、地盤レコード再取得(番号は頭数)
                jibanRecAfterExe = GetJibanData(kbn, strRetBangou)

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        '処理件数(合計) += 処理件数(当処理)
        jibanRecAfterExe.SyoriKensuu += jibanRec.SyoriKensuu
        Return True

    End Function
#End Region

#Region "地盤データ削除"
    ''' <summary>
    ''' 地盤データを削除します。関連する邸別請求データの削除も行われます
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="deleteTeibetu">邸別請求レコードを削除する場合：True</param>
    ''' <param name="userId">ログインユーザーID</param>
    ''' <returns>True:削除成功 False:削除失敗</returns>
    ''' <remarks></remarks>
    Public Function DeleteJibanData(ByVal sender As System.Object, _
                                    ByVal kbn As String, _
                                    ByVal hosyousyoNo As String, _
                                    ByVal deleteTeibetu As Boolean, _
                                    ByVal userId As String) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteJibanData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            deleteTeibetu, _
                                            userId)

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 地盤データ取得用データアクセスクラス
                Dim dataAccess As JibanDataAccess = New JibanDataAccess
                '連携管理用ロジッククラス
                Dim clsRenkei As New RenkeiKanriLogic
                Dim intResult As Integer

                intResult = 0

                ' 削除する前に邸別請求データのKeyを全て取得しておく
                Dim teibetuSeikyuuList As List(Of TeibetuSeikyuuRecord) = _
                DataMappingHelper.Instance.getMapArray(Of TeibetuSeikyuuRecord)(GetType(TeibetuSeikyuuRecord), _
                dataAccess.GetTeibetuSeikyuuDataKey(kbn, hosyousyoNo))

                If teibetuSeikyuuList.Count <= 0 Then
                    deleteTeibetu = False
                End If

                ' 地盤データの削除を実施、邸別請求は必要に応じて削除可能（基本的に削除は同期必要）
                If dataAccess.DeleteJibanData(kbn, hosyousyoNo, deleteTeibetu, userId) = True Then

                    ' 連携用テーブルに登録する（地盤－削除）
                    Dim renkeiJibanRec As New JibanRenkeiRecord
                    renkeiJibanRec.Kbn = kbn
                    renkeiJibanRec.HosyousyoNo = hosyousyoNo
                    renkeiJibanRec.RenkeiSijiCd = 9
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = userId
                    If clsRenkei.DeleteJibanRenkeiData(renkeiJibanRec) <> 1 Then
                        '登録に失敗したので処理中断
                        Return False
                    End If

                    ' 連携テーブルに登録（邸別請求－削除）
                    For Each rec As TeibetuSeikyuuRecord In teibetuSeikyuuList
                        Dim renkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
                        renkeiTeibetuRec.Kbn = rec.Kbn
                        renkeiTeibetuRec.HosyousyoNo = rec.HosyousyoNo
                        renkeiTeibetuRec.BunruiCd = rec.BunruiCd
                        renkeiTeibetuRec.GamenHyoujiNo = rec.GamenHyoujiNo
                        renkeiTeibetuRec.RenkeiSijiCd = 9
                        renkeiTeibetuRec.SousinJykyCd = 0
                        renkeiTeibetuRec.UpdLoginUserId = userId
                        If clsRenkei.DeleteTeibetuSeikyuuRenkeiData(renkeiTeibetuRec) <> 1 Then
                            '登録に失敗したので処理中断
                            Return False
                        End If
                    Next

                    ' 削除に成功した場合、トランザクションをコミットする
                    scope.Complete()
                Else
                    Return False
                End If

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True

    End Function
#End Region

#Region "系列フラグ取得"
    ''' <summary>
    ''' 系列コードより系列フラグを取得します
    ''' </summary>
    ''' <param name="keiretuCd">系列コード</param>
    ''' <returns>系列フラグ</returns>
    ''' <remarks>1:系列</remarks>
    Public Function GetKeiretuFlg(ByVal keiretuCd As String) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKeiretuFlg", _
                                            keiretuCd)

        Select Case keiretuCd
            Case EarthConst.KEIRETU_AIFURU
                Return 1
            Case EarthConst.KEIRETU_TH
                Return 1
            Case EarthConst.KEIRETU_WANDA
                Return 1
        End Select

        Return 0

    End Function

    ''' <summary>
    ''' 系列コードより系列フラグを取得します(2系列のみ 0001,THTH)
    ''' </summary>
    ''' <param name="keiretuCd">系列コード</param>
    ''' <returns>系列フラグ</returns>
    ''' <remarks>1:系列</remarks>
    Public Function GetKeiretuFlg2(ByVal keiretuCd As String) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKeiretuFlg", _
                                            keiretuCd)

        Select Case keiretuCd
            Case EarthConst.KEIRETU_AIFURU
                Return 1
            Case EarthConst.KEIRETU_TH
                Return 1
        End Select

        Return 0

    End Function
#End Region

#Region "ビルダー情報取得"
    ''' <summary>
    ''' ビルダー情報をList(Of BuilderInfoRecord)で取得します
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>List(Of BuilderInfoRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetBuilderInfo(ByVal strKameitenCd As String) As List(Of BuilderInfoRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetBuilderInfo", _
                                            strKameitenCd)

        Dim dataAccess As New BuilderDataAccess
        Dim list As List(Of BuilderInfoRecord)

        ' データを取得し、List(Of BuilderInfoRecord)に格納する
        list = DataMappingHelper.Instance.getMapArray(Of BuilderInfoRecord)(GetType(BuilderInfoRecord), _
                                                      dataAccess.GetBuilderData(strKameitenCd))

        Return list

    End Function
#End Region

#Region "住所を進捗データ用に変換"
    ''' <summary>
    ''' 画面で入力された住所１，２，３を進捗データ用住所１，２に変換する
    ''' </summary>
    ''' <param name="juusyo1">画面で入力された物件住所１</param>
    ''' <param name="juusyo2">画面で入力された物件住所２</param>
    ''' <param name="juusyo3">画面で入力された物件住所３</param>
    ''' <returns>ArrayList：(0)に変換後住所１、(1)に変換後住所２</returns>
    ''' <remarks></remarks>
    Public Function ConvJuusyo2Report(ByVal juusyo1 As String, _
                                      ByVal juusyo2 As String, _
                                      ByVal juusyo3 As String) As Array

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ConvJuusyo2Report", _
                                            juusyo1, _
                                            juusyo2, _
                                            juusyo3)

        Dim retArr() As String = New String() {String.Empty, String.Empty}

        juusyo1 = IIf(juusyo1 Is Nothing, String.Empty, juusyo1)
        juusyo2 = IIf(juusyo2 Is Nothing, String.Empty, juusyo2)
        juusyo3 = IIf(juusyo3 Is Nothing, String.Empty, juusyo3)

        If sLogic.GetStrByteSJIS(juusyo3) <= 28 Then
            retArr(0) = juusyo1
            retArr(1) = juusyo2 & juusyo3
        Else
            Dim retJuusyo1 As String = juusyo1
            Dim retJuusyo2 As String = String.Empty

            '変換住所１に住所２を一文字ずつ追加し、60バイトを超えた時点で、
            '以降を変換住所２にセット
            For sc As Integer = 0 To juusyo2.Length - 1
                Dim tmpJuusyo1 As String = retJuusyo1 + juusyo2.Substring(sc, 1)
                If sLogic.GetStrByteSJIS(tmpJuusyo1) > 60 Then
                    retJuusyo2 = juusyo2.Substring(sc)
                    Exit For
                Else
                    retJuusyo1 = tmpJuusyo1
                End If
            Next

            '変換住所２に住所３を追加
            retJuusyo2 += juusyo3

            retArr(0) = retJuusyo1
            retArr(1) = retJuusyo2
        End If

        Return retArr

    End Function
#End Region

#Region "ReportIf連携データ編集"

    ''' <summary>
    ''' ReportIf連携チェック処理
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">事業所コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkRJhsRenkei(ByVal strTysKaisyaCd As String, ByVal strJigyousyoCd As String) As Boolean
        ' 進捗データ生成系データアクセスクラス
        Dim reportAccess As New ReportIfDataAccess

        ' 連携調査会社設定マスタの存在チェック
        If reportAccess.ChkRenkeiTyousaKaisya(strTysKaisyaCd, strJigyousyoCd) Then
            Return True
        End If
        Return False
    End Function
    ''' <summary>
    ''' 地盤レコードの内容より進捗データの編集を行います
    ''' </summary>
    ''' <param name="jibanRec"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EditReportIfData(ByVal jibanRec As JibanRecordBase) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditReportIfData", _
                                            jibanRec)

        ' 進捗データアクセスクラス
        Dim dataAccess As New ReportIfDataAccess

        ' 顧客NOは区分＋保証書NO
        Dim kokyakuNo As String = jibanRec.Kbn.Trim() & jibanRec.HosyousyoNo.Trim()

        ' 進捗テーブルの存在チェック用SQLを実行する
        Dim chkList As List(Of ReportIfChkRecord) = _
                                        DataMappingHelper.Instance.getMapArray(Of ReportIfChkRecord)( _
                                        GetType(ReportIfChkRecord), _
                                        dataAccess.GetReportIf(kokyakuNo))

        ' 進捗テーブルの登録・更新判定用
        Dim editMode As ReportIfDataAccess.EDIT_MODE

        If chkList.Count = 0 Then
            ' データが存在しないので新規
            editMode = ReportIfDataAccess.EDIT_MODE.MODE_INSERT
        Else
            ' データが存在するので更新
            editMode = ReportIfDataAccess.EDIT_MODE.MODE_UPDATE

            ' 進捗レコード存在チェック用レコードの作成
            Dim reportCheckRec As ReportIfChkRecord = chkList(0)

        End If

        ' 進捗レコードの作成
        Dim reportRec As New ReportIfRecord
        reportRec.KokyakuNo = kokyakuNo                                         ' 顧客番号
        reportRec.ChousaTachiai = GetTatiaiWord(jibanRec.TatiaisyaCd)           ' 調査立会者
        '※当処理が行われる前に特別対応データがDB更新されていること！
        reportRec.Options = Me.SetOptions(jibanRec)                             ' オプション

        ' 登録・更新を実行する
        If dataAccess.EditReportIf(reportRec, editMode) < 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 立会者コードより文字列を返却します
    ''' </summary>
    ''' <param name="tatiaiCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTatiaiWord(ByVal tatiaiCd As Integer) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTatiaiWord", _
                                            tatiaiCd)

        Dim retValue As String = ""

        Select Case tatiaiCd
            Case 1
                ' 施主様
                retValue = EarthConst.TATIAI_SESYU_SAMA
            Case 2
                ' 担当者
                retValue = EarthConst.TATIAI_TANTOUSYA
            Case 3
                ' 施主様、担当者
                retValue = EarthConst.TATIAI_SESYU_SAMA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_TANTOUSYA
            Case 4
                ' その他
                retValue = EarthConst.TATIAI_SONOTA
            Case 5
                ' 施主様、その他
                retValue = EarthConst.TATIAI_SESYU_SAMA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_SONOTA
            Case 6
                ' 担当者、その他
                retValue = EarthConst.TATIAI_TANTOUSYA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_SONOTA
            Case 7
                ' 施主様、担当者、その他
                retValue = EarthConst.TATIAI_SESYU_SAMA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_TANTOUSYA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_SONOTA
        End Select

        Return retValue

    End Function

    ''' <summary>
    ''' 進捗テーブル更新用のデータをコピーする
    ''' </summary>
    ''' <param name="motoJibanRecord">コピー元地盤レコード</param>
    ''' <param name="sakiJibanRecord">コピー先地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetSintyokuJibanData(ByVal motoJibanRecord As JibanRecordBase, ByRef sakiJibanRecord As JibanRecordBase)

        Dim JibanLogic As New JibanLogic

        sakiJibanRecord.Kbn = motoJibanRecord.Kbn                                                   ' 区分
        sakiJibanRecord.HosyousyoNo = motoJibanRecord.HosyousyoNo                                   ' 番号
        sakiJibanRecord.TysGaiyou = motoJibanRecord.TysGaiyou                                       ' サービス区分(調査概要コードをセット)
        sakiJibanRecord.TysHouhouMeiIf = motoJibanRecord.TysHouhouMeiIf                             ' 調査方法＝調査方法名
        sakiJibanRecord.KouzouMeiIf = motoJibanRecord.KouzouMeiIf                                   ' 計画建物＝構造名
        sakiJibanRecord.SesyuMei = motoJibanRecord.SesyuMei                                         ' 施主名
        sakiJibanRecord.BukkenJyuusyo1 = motoJibanRecord.BukkenJyuusyo1                             ' 物件住所１
        sakiJibanRecord.BukkenJyuusyo2 = motoJibanRecord.BukkenJyuusyo2                             ' 物件住所２
        sakiJibanRecord.BukkenJyuusyo3 = motoJibanRecord.BukkenJyuusyo3                             ' 物件住所３
        sakiJibanRecord.TysKibouDate = motoJibanRecord.TysKibouDate                                 ' 調査希望日
        sakiJibanRecord.TysKibouJikan = motoJibanRecord.TysKibouJikan                               ' 調査希望時間
        sakiJibanRecord.IraiTantousyaMei = motoJibanRecord.IraiTantousyaMei                         ' 調査担当者名
        sakiJibanRecord.TatiaisyaCd = motoJibanRecord.TatiaisyaCd                                   ' 調査立会者
        sakiJibanRecord.KameitenCd = motoJibanRecord.KameitenCd                                     ' 加盟店コード
        sakiJibanRecord.KameitenMeiIf = motoJibanRecord.KameitenMeiIf                               ' 加盟店名
        sakiJibanRecord.KameitenTelIf = motoJibanRecord.KameitenTelIf                               ' 加盟店電話番号
        sakiJibanRecord.KameitenFaxIf = motoJibanRecord.KameitenFaxIf                               ' 加盟店FAX番号
        sakiJibanRecord.KameitenMailIf = motoJibanRecord.KameitenMailIf                             ' 加盟店メールアドレス
        sakiJibanRecord.TysKaisyaCd = motoJibanRecord.TysKaisyaCd                                   ' 調査会社コード
        sakiJibanRecord.TysKaisyaJigyousyoCd = motoJibanRecord.TysKaisyaJigyousyoCd                 ' 調査会社事業所コード
        sakiJibanRecord.TysKaisyaMeiIf = motoJibanRecord.TysKaisyaMeiIf                             ' 調査会社名
        sakiJibanRecord.TysRenrakusakiAtesakiMei = motoJibanRecord.TysRenrakusakiAtesakiMei         ' 調査連絡先(宛先、会社名)
        sakiJibanRecord.TysRenrakusakiTel = motoJibanRecord.TysRenrakusakiTel                       ' 調査連絡先(TEL)
        sakiJibanRecord.TysRenrakusakiFax = motoJibanRecord.TysRenrakusakiFax                       ' 調査連絡先(FAX)
        sakiJibanRecord.TysRenrakusakiMail = motoJibanRecord.TysRenrakusakiMail                     ' 調査連絡先(MAIL)
        sakiJibanRecord.IraiTantousyaMei = motoJibanRecord.IraiTantousyaMei                         ' 調査連絡先(担当者)
        sakiJibanRecord.Kouzou = motoJibanRecord.Kouzou                                             ' 構造(ここではコードをセット。名称はDB登録時にSQLで取得)
        sakiJibanRecord.Kaisou = motoJibanRecord.Kaisou                                             ' 階層(ここではコードをセット。名称はDB登録時にSQLで取得)
        sakiJibanRecord.SekkeiKyoyouSijiryoku = motoJibanRecord.SekkeiKyoyouSijiryoku               ' 設計許容支持力
        sakiJibanRecord.NegiriHukasa = motoJibanRecord.NegiriHukasa                                 ' 根切り深さ
        sakiJibanRecord.YoteiKs = motoJibanRecord.YoteiKs                                           ' 予定基礎名(ここではコードをセット。名称はDB登録時にSQLで取得)
        sakiJibanRecord.Syako = motoJibanRecord.Syako                                               ' 車庫(ここではコードをセット。名称はDB登録時にSQLで取得)
        sakiJibanRecord.YoteiMoritutiAtusa = motoJibanRecord.YoteiMoritutiAtusa                     ' 予定盛土厚さ
        sakiJibanRecord.DoujiIraiTousuu = motoJibanRecord.DoujiIraiTousuu                           ' 同時依頼棟数
        sakiJibanRecord.TatemonoYoutoNo = motoJibanRecord.TatemonoYoutoNo                           ' 建物用途名(ここではコードをセット。名称はDB登録時にSQLで取得)
        sakiJibanRecord.Keiyu = motoJibanRecord.Keiyu                                               ' 経由

    End Sub

    ''' <summary>
    ''' 進捗データテーブルのOptionsにセットする値を編集する
    ''' </summary>
    ''' <param name="jibanRec">地盤レコードクラス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetOptions(ByVal jibanRec As JibanRecordBase) As String
        Dim strRet As String = String.Empty '戻り値

        Dim intTotalCnt As Integer = 0
        Dim listRes As List(Of TokubetuTaiouRecordBase)
        Dim dtRec As TokubetuTaiouRecordBase
        Dim intCnt As Integer = 0 'カウンタ
        Dim ttLogic As New TokubetuTaiouLogic
        Dim sender As New Object

        '特別対応マスタ
        With jibanRec
            listRes = ttLogic.GetTokubetuTaiouDataInfo(sender, .Kbn, .HosyousyoNo, String.Empty, intTotalCnt)
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
                    '取消でなく、特別対応コードが0以上99以下がセット対象
                    If .Torikesi = 0 AndAlso (.TokubetuTaiouCd >= 0 And .TokubetuTaiouCd <= 99) Then
                        If strRet = String.Empty Then
                            strRet = dtRec.TokubetuTaiouCd.ToString
                        Else
                            strRet &= EarthConst.SEP_STRING_REPORTIF & dtRec.TokubetuTaiouCd.ToString
                        End If
                    End If
                End With
            End If
        Next

        Return strRet
    End Function

    ''' <summary>
    ''' 進捗データのレコードの内容を取得します
    ''' </summary>
    ''' <param name="jibanRec">地盤レコードクラス</param>
    ''' <param name="reportRec">進捗データレコードクラス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReportIfData(ByVal jibanRec As JibanRecordBase, ByRef reportRec As ReportIfGetRecord) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetReportIfData", _
                                            jibanRec, _
                                            reportRec)

        ' 進捗データアクセスクラス
        Dim dataAccess As New ReportIfDataAccess

        ' 顧客NOは区分＋保証書NO
        Dim kokyakuNo As String = jibanRec.Kbn.Trim() & jibanRec.HosyousyoNo.Trim()

        ' 進捗テーブル取得SQLを実行する
        Dim List As List(Of ReportIfGetRecord) = _
                                        DataMappingHelper.Instance.getMapArray(Of ReportIfGetRecord)( _
                                        GetType(ReportIfGetRecord), _
                                        dataAccess.GetReportIfData(kokyakuNo))

        If List.Count > 0 Then
            reportRec = List(0)
        End If
        Return True

    End Function
#End Region

    ''' <summary>
    ''' 邸別請求レコードのデータをコピーする
    ''' </summary>
    ''' <param name="motoJibanRecord">コピー元地盤レコード</param>
    ''' <param name="sakiJibanRecord">コピー先地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub ps_CopyTeibetuSyouhinData(ByVal motoJibanRecord As JibanRecordBase, ByRef sakiJibanRecord As JibanRecordBase)

        sakiJibanRecord.Syouhin1Record = motoJibanRecord.Syouhin1Record
        sakiJibanRecord.Syouhin2Records = motoJibanRecord.Syouhin2Records
        sakiJibanRecord.Syouhin3Records = motoJibanRecord.Syouhin3Records

    End Sub

#Region "地盤T.分譲コードの存在チェックを行ないます"
    ''' <summary>
    ''' 地盤T.分譲コードの存在チェックを行ないます
    ''' </summary>
    ''' <param name="strBunjouCd">分譲コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkJibanBunjouCd( _
                                        ByVal strBunjouCd As String _
                                        ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanBunjouCd", strBunjouCd)
        Dim blnRes As Boolean = False

        ' 分譲コードを取得して結果を返す
        blnRes = dataAccess.GetBunjouCd(strBunjouCd)
        Return blnRes
    End Function
#End Region

#Region "地盤データの存在チェック"
    ''' <summary>
    ''' 地盤データの存在チェックを行ないます
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExistsJibanData(ByVal strKbn As String, ByVal strBangou As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ExistsJibanData", _
                                            strKbn, strBangou)

        Dim blnChk As Boolean = False

        ' 地盤データをチェック
        blnChk = dataAccess.IsJibanData(strKbn, strBangou)

        Return blnChk
    End Function

    ''' <summary>
    ''' 地盤データを走査し、自物件の名寄状況をチェックします
    ''' </summary>
    ''' <param name="strBukkenNo">画面.物件NO(区分 + 番号)</param>
    ''' <param name="strNayoseNo">画面.名寄先NO(区分 + 番号)</param>
    ''' <returns>True:存在する False:存在しない</returns>
    ''' <remarks></remarks>
    Public Function ChkBukkenNayoseJyky( _
                                        ByVal strBukkenNo As String, _
                                         ByVal strNayoseNo As String _
                                        ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBukkenNayoseJyky", _
                                            strBukkenNo, strNayoseNo)

        Dim blnChk As Boolean = False

        Dim strRet() As String = {}
        Dim strRetNayose() As String = {}

        Dim strKbn As String = String.Empty
        Dim strBangou As String = String.Empty

        '物件NOを区分と番号に分割して取得
        strRet = sLogic.GetSepBukkenNo(strBukkenNo)
        strRetNayose = sLogic.GetSepBukkenNo(strNayoseNo)

        ' 地盤データと名寄先コードをチェック
        If strBukkenNo <> "" Then
            blnChk = dataAccess.ChkJibanDataNayoseNotChildren(strRet(0), strRet(1), strRetNayose(0), strRetNayose(1))
        End If

        Return blnChk
    End Function

    ''' <summary>
    ''' 地盤データを走査して、該当物件が親物件かどうかの判断を行ないます
    ''' </summary>
    ''' <param name="strBukkenNo">物件NO(区分 + 番号)</param>
    ''' <param name="strNayoseCd">名寄先コード(区分 + 番号)</param>
    ''' <returns>True:存在する False:存在しない</returns>
    ''' <remarks></remarks>
    Public Function ChkBukkenNayoseOyaBukken( _
                                        ByVal strBukkenNo As String, _
                                        ByVal strNayoseCd As String _
                                        ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBukkenNayoseOyaBukken", _
                                            strBukkenNo, strNayoseCd)

        Dim blnChk As Boolean = False

        Dim strRet() As String = {}
        Dim strRetNayose() As String = {}

        Dim strKbn As String = String.Empty
        Dim strBangou As String = String.Empty

        '物件NOを区分と番号に分割して取得
        strRet = sLogic.GetSepBukkenNo(strBukkenNo)
        strRetNayose = sLogic.GetSepBukkenNo(strNayoseCd)

        ' 地盤データと名寄先コードをチェック
        If strNayoseCd <> "" Then
            blnChk = dataAccess.ChkJibanDataOyaBukken(strRet(0), strRet(1), strRetNayose(0), strRetNayose(1))
        End If

        Return blnChk
    End Function

#Region "物件名寄状況の最新状態をチェック"

    ''' <summary>
    ''' 物件名寄状況の最新状態をチェックする
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="dtRec">地盤レコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkLatestBukkenNayoseJyky(ByVal sender As Object, _
                                  ByVal dtRec As JibanRecordBase) As Boolean

        Dim blnRet As Boolean = True

        Dim jibanRec As New JibanRecordBase 'TMP用

        Dim strBukkenNo As String = String.Empty
        Dim strBukkenNayoseCd As String = String.Empty
        Dim strErrMsg As String = String.Empty

        '対象地盤レコードを作業用のレコードクラスに格納する
        jibanRec = dtRec

        strBukkenNo = jibanRec.Kbn & jibanRec.HosyousyoNo
        strBukkenNayoseCd = jibanRec.BukkenNayoseCd

        '名寄先が親物件かのチェック
        If Me.ChkBukkenNayoseOyaBukken(strBukkenNo, strBukkenNayoseCd) = False Then
            blnRet = False
            strErrMsg = Messages.MSG167E.Replace("@PARAM1", "名寄先の物件").Replace("@PARAM2", "子物件").Replace("@PARAM3", "物件名寄コード")
            mLogic.AlertMessage(sender, strErrMsg, 0, "ChkLatestBukkenNayoseJyky1")
            Exit Function
        End If

        '自物件の名寄状況チェック
        If Me.ChkBukkenNayoseJyky(strBukkenNo, strBukkenNayoseCd) = False Then
            blnRet = False
            strErrMsg = Messages.MSG167E.Replace("@PARAM1", "当物件NO").Replace("@PARAM2", "他物件の名寄先").Replace("@PARAM3", "物件名寄コード")
            mLogic.AlertMessage(sender, strErrMsg, 0, "ChkLatestBukkenNayoseJyky2")
            Exit Function
        End If

        Return blnRet
    End Function
#End Region

#End Region

#Region "邸別請求データの取得"
    ''' <summary>
    ''' 邸別請求データの取得
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <param name="strBunruiCd">分類コード[デフォルト：""]</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuData(ByVal strKbn As String, ByVal strBangou As String, Optional ByVal strBunruiCd As String = "") As DataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuData", _
                                            strKbn, strBangou, strBunruiCd)
        Dim dt As DataTable

        ' 邸別請求データを取得
        dt = dataAccess.GetTeibetuSeikyuuData(strKbn, strBangou, strBunruiCd)

        Return dt
    End Function
#End Region

#Region "保証商品有無の取得"

    ''' <summary>
    ''' 保証商品有無の取得
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyouSyouhinUmu(ByVal strKbn As String, ByVal strBangou As String) As String
        Dim strHosyouUmu As String = String.Empty

        strHosyouUmu = dataAccess.GetHosyouSyouhinUmu(strKbn, strBangou)

        Return strHosyouUmu
    End Function
#End Region

#Region "分譲コード採番処理"
    ''' <summary>
    ''' 分譲コード採番処理
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' M<param name="strLoginUserId">ログインユーザーID</param>
    ''' <returns>採番後の分譲コード</returns>
    ''' <remarks></remarks>
    Public Function getCntUpBunjouCd(ByVal sender As Object, ByVal strLoginUserId As String) As Integer
        Dim blnUpdate As Boolean
        Dim saibanDtAcc As New SaibanDataAccess
        Dim intBunjouCd As Integer

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))


                '分譲コードを更新
                blnUpdate = saibanDtAcc.updCntUpLastNo(EarthConst.SAIBAN_SYUBETU_BUNJOU_CD, strLoginUserId)

                If blnUpdate Then
                    intBunjouCd = saibanDtAcc.getSaibanRecord(EarthConst.SAIBAN_SYUBETU_BUNJOU_CD)
                    ' 更新に成功した場合、トランザクションをコミットする
                    scope.Complete()
                Else
                    intBunjouCd = Integer.MinValue
                    mLogic.AlertMessage(sender, Messages.MSG019E.Replace("@PARAM1", "分譲コードの採番"))
                    Return intBunjouCd
                End If

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return intBunjouCd
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return intBunjouCd
        End Try

        Return intBunjouCd
    End Function
#End Region

#Region "特別対応デフォルト登録処理"
    ''' <summary>
    ''' 特別対応デフォルト登録処理[画面用]：トランザクション未発行
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strTysHouhouNo">調査方法NO</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="dtUpdDatetime">更新日時</param>
    ''' <param name="strRentouBukkenSuu">連棟物件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insertTokubetuTaiouUI(ByVal sender As Object, _
                                        ByVal strKbn As String, _
                                        ByVal strHosyousyoNo As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strSyouhinCd As String, _
                                        ByVal strTysHouhouNo As String, _
                                        ByVal strLoginUserId As String, _
                                        ByVal dtUpdDatetime As DateTime, _
                                        ByVal strRentouBukkenSuu As String) As Boolean

        ' 加盟店商品調査方法特別対応マスタ取得用のロジッククラス
        Dim ttLogic As New TokubetuTaiouLogic
        ' 加盟店商品調査方法特別対応マスタ格納用のリスト
        Dim listKtRec As New List(Of KameiTokubetuTaiouRecord)

        ' 加盟店商品調査方法特別対応マスタの取得
        Dim total_count As Integer = 0 '取得件数
        listKtRec = ttLogic.GetDefaultTokubetuTaiouInfo(sender, strKameitenCd, strSyouhinCd, strTysHouhouNo, total_count)
        If total_count = -1 Then
            Return False
        ElseIf total_count = 0 Then
            Return True
        End If

        Dim intRentouBukkenSuu As Integer = 1 '連棟物件数
        Dim intSyoriKensuu As Integer = 0 '処理件数

        ' 番号(保証書NO) 作業用
        Dim intTmpBangou As Integer = CInt(strHosyousyoNo) + intSyoriKensuu '番号+処理件数
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") 'フォーマット

        '該当する加盟店商品調査方法特別対応マスタが存在した場合
        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 特別対応テーブルの追加
                If Not listKtRec Is Nothing AndAlso listKtRec.Count > 0 Then
                    ' 連棟物件数チェック
                    If strRentouBukkenSuu <> String.Empty Then
                        intRentouBukkenSuu = CInt(strRentouBukkenSuu)
                        If intRentouBukkenSuu < 1 Then
                            intRentouBukkenSuu = 1
                        End If
                    End If

                    '連棟分処理を行なう
                    For intSyoriKensuu = 0 To intRentouBukkenSuu - 1
                        '新規追加用スクリプトおよび実行
                        If ttLogic.InsertTokubetuTaiou(sender, listKtRec, strKbn, strTmpBangou, strLoginUserId, dtUpdDatetime) = False Then
                            Return False
                        End If

                        '*************************
                        ' 連棟処理対応
                        '*************************
                        '番号をカウントアップ
                        intTmpBangou = CInt(strTmpBangou) + 1 'カウンタ
                        strTmpBangou = Format(intTmpBangou, "0000000000") 'フォーマット
                    Next
                End If

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' 特別対応デフォルト登録処理[画面用](新規引継時のみ)：トランザクション未発行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNoPre">保証書No(前回登録分)</param>
    ''' <param name="strHosyousyoNo">保証書No(今回登録分)</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="dtUpdDatetime">>更新日時</param>
    ''' <param name="strRentouBukkenSuu">連棟物件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insertTokubetuTaiouUIHikitugi(ByVal sender As Object, _
                                        ByVal strKbn As String, _
                                        ByVal strHosyousyoNoPre As String, _
                                        ByVal strHosyousyoNo As String, _
                                        ByVal strLoginUserId As String, _
                                        ByVal dtUpdDatetime As DateTime, _
                                        ByVal strRentouBukkenSuu As String) As Boolean

        Dim listRes As New List(Of TokubetuTaiouRecordBase)
        Dim ttLogic As New TokubetuTaiouLogic
        Dim intTotalCnt As Integer = 0      '特別対応レコードカウント
        Dim intRentouBukkenSuu As Integer = 1 '連棟物件数
        Dim intSyoriKensuu As Integer = 0 '処理件数

        '特別対応リスト(トラン・1物件が対象)
        listRes = ttLogic.GetTokubetuTaiouDataInfo(sender, strKbn, strHosyousyoNoPre, String.Empty, intTotalCnt)

        ' 検索結果ゼロ件の場合
        If intTotalCnt < 0 Then
            '検索結果件数が-1の場合、エラーなので、処理終了
            Return False
        ElseIf intTotalCnt = 0 Then
            '件数が0件の場合は、引継ぐ特別対応が無いので、正常終了
            Return True
        End If

        ' 番号(保証書NO) 作業用
        Dim intTmpBangou As Integer = CInt(strHosyousyoNo) + intSyoriKensuu '番号+処理件数
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") 'フォーマット

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 特別対応テーブルの追加
                If Not listRes Is Nothing AndAlso listRes.Count > 0 Then
                    ' 連棟物件数チェック
                    If strRentouBukkenSuu <> String.Empty Then
                        intRentouBukkenSuu = CInt(strRentouBukkenSuu)
                        If intRentouBukkenSuu < 1 Then
                            intRentouBukkenSuu = 1
                        End If
                    End If

                    '連棟分処理を行なう
                    For intSyoriKensuu = 0 To intRentouBukkenSuu - 1
                        '新規追加用スクリプトおよび実行
                        If ttLogic.InsertTokubetuTaiou(sender, listRes, strKbn, strTmpBangou, strLoginUserId, dtUpdDatetime) = False Then
                            Return False
                        End If

                        '*************************
                        ' 連棟処理対応
                        '*************************
                        '番号をカウントアップ
                        intTmpBangou = CInt(strTmpBangou) + 1 'カウンタ
                        strTmpBangou = Format(intTmpBangou, "0000000000") 'フォーマット
                    Next
                End If

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True

    End Function

    ''' <summary>
    ''' 特別対応デフォルト登録処理[ロジック用]：トランザクション発行中
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strTysHouhouNo">調査方法NO</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="dtUpdDatetime">更新日時</param>
    ''' <param name="strRentouBukkenSuu">連棟物件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insertTokubetuTaiouLogic(ByVal sender As Object, _
                                        ByVal strKbn As String, _
                                        ByVal strHosyousyoNo As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strSyouhinCd As String, _
                                        ByVal strTysHouhouNo As String, _
                                        ByVal strLoginUserId As String, _
                                        ByVal dtUpdDatetime As DateTime, _
                                        ByVal strRentouBukkenSuu As String) As Boolean

        ' 加盟店商品調査方法特別対応マスタ取得用のロジッククラス
        Dim ttLogic As New TokubetuTaiouLogic
        ' 加盟店商品調査方法特別対応マスタ格納用のリスト
        Dim listKtRec As New List(Of KameiTokubetuTaiouRecord)

        ' 加盟店商品調査方法特別対応マスタの取得
        Dim total_count As Integer = 0 '取得件数
        listKtRec = ttLogic.GetDefaultTokubetuTaiouInfo(sender, strKameitenCd, strSyouhinCd, strTysHouhouNo, total_count)
        If total_count = -1 Then
            Return False
        ElseIf total_count = 0 Then
            Return True
        End If

        Dim intRentouBukkenSuu As Integer = 1 '連棟物件数
        Dim intSyoriKensuu As Integer = 0 '処理件数

        ' 番号(保証書NO) 作業用
        Dim intTmpBangou As Integer = CInt(strHosyousyoNo) + intSyoriKensuu '番号+処理件数
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") 'フォーマット

        '該当する加盟店商品調査方法特別対応マスタが存在した場合
        Try
            ' 特別対応テーブルの追加
            If Not listKtRec Is Nothing AndAlso listKtRec.Count > 0 Then
                ' 連棟物件数チェック
                If strRentouBukkenSuu <> String.Empty Then
                    intRentouBukkenSuu = CInt(strRentouBukkenSuu)
                    If intRentouBukkenSuu < 1 Then
                        intRentouBukkenSuu = 1
                    End If
                End If

                '連棟分処理を行なう
                For intSyoriKensuu = 0 To intRentouBukkenSuu - 1
                    '新規追加用スクリプトおよび実行
                    If ttLogic.InsertTokubetuTaiou(sender, listKtRec, strKbn, strTmpBangou, strLoginUserId, dtUpdDatetime) = False Then
                        Return False
                    End If

                    '*************************
                    ' 連棟処理対応
                    '*************************
                    '番号をカウントアップ
                    intTmpBangou = CInt(strTmpBangou) + 1 'カウンタ
                    strTmpBangou = Format(intTmpBangou, "0000000000") 'フォーマット
                Next
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function

#End Region

#Region "調査方法マスタレコード取得"

    ''' <summary>
    ''' 調査方法マスタより、主キーに紐付くレコードを取得する
    ''' </summary>
    ''' <param name="intTysHouhouNo">調査方法NO</param>
    ''' <returns>Key値に紐付くレコード</returns>
    ''' <remarks></remarks>
    Public Function getTyousahouhouRecord(ByVal intTysHouhouNo As Integer) As TyousahouhouRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getTyousahouhouRecord", intTysHouhouNo)

        'データアクセスクラス
        Dim clsDataAcc As New TyousahouhouDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New TyousahouhouRecord

        dTblResult = clsDataAcc.getTyousahouhouRecord(intTysHouhouNo)

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of TyousahouhouRecord)(GetType(TyousahouhouRecord), dTblResult)(0)
        End If

        Return recResult

    End Function


#End Region

#Region "区分マスタレコード取得"

    ''' <summary>
    ''' 区分マスタより、主キーに紐付くレコードを取得する
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <returns>Key値に紐付くレコード</returns>
    ''' <remarks></remarks>
    Public Function getKubunRecord(ByVal strKubun As String) As KubunRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getKubunRecord", strKubun)

        'データアクセスクラス
        Dim clsDataAcc As New KubunDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New KubunRecord

        If strKubun <> Nothing Then
            dTblResult = clsDataAcc.getKubunRecord(strKubun)
        End If

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of KubunRecord)(GetType(KubunRecord), dTblResult)(0)
        End If

        Return recResult

    End Function

#End Region

#Region "商品マスタレコード取得"

    ''' <summary>
    ''' 商品マスタより、主キーに紐付くレコードを取得する
    ''' </summary>
    ''' <param name="strSyouhinCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinRecord(ByVal strSyouhinCd As String) As SyouhinMeisaiRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getKubunRecord", strSyouhinCd)

        '商品DAクラス
        Dim daSyouhin As New SyouhinDataAccess
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードリスト
        Dim recResult As New SyouhinMeisaiRecord

        If strSyouhinCd <> Nothing Then
            dTblResult = daSyouhin.GetSyouhinInfo(strSyouhinCd)
        End If

        If dTblResult.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            recResult = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), dTblResult)(0)
        End If

        Return recResult
    End Function


#End Region

End Class
