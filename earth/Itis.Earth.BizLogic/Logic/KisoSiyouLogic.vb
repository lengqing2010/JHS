
''' <summary>
''' 基礎仕様データ編集用クラス
''' </summary>
''' <remarks></remarks>
Public Class KisoSiyouLogic

    '判定種別タイプ
    Private Const pStrKouji = "【工事】"
    Private Const pStrTyokusetuKiso = "【直接基礎】"
    Private Const pStrYouSyasin = "【要写真】"

    '判定種別判断文字列
    Private Const pStrTigyou = "地業"
    Private Const pStrTutinoTikan = "土の置換"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

    End Sub

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 基礎仕様NOより判定名（基礎仕様）を取得します
    ''' </summary>
    ''' <param name="ksSiyouNo">基礎仕様NO</param>
    ''' <returns>判定名（基礎仕様）</returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouMei(ByVal ksSiyouNo As Integer) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouMei", _
                                                    ksSiyouNo)

        ' 基礎仕様データアクセスクラス
        Dim dataAccess As New KisoSiyouDataAccess()

        Dim table As KisoSiyouDataSet.KisoSiyouTableDataTable = dataAccess.GetHanteiMei(ksSiyouNo)

        If Not table Is Nothing Then
            Dim row As KisoSiyouDataSet.KisoSiyouTableRow
            row = table.Rows(0)
            ' 取得結果を返却
            Return row.ks_siyou
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' 基礎仕様NOより基礎仕様レコードを取得します
    ''' </summary>
    ''' <param name="ksSiyouNo">基礎仕様NO</param>
    ''' <returns>基礎仕様レコード</returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouRec(ByVal ksSiyouNo As Integer) As KisoSiyouRecord

        Dim list As List(Of KisoSiyouRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouMei", _
                                                    ksSiyouNo)

        ' 基礎仕様データアクセスクラス
        Dim dataAccess As New KisoSiyouDataAccess()

        Dim table As KisoSiyouDataSet.KisoSiyouTableDataTable = dataAccess.GetHanteiMei(ksSiyouNo)

        If Not table Is Nothing Then

            list = DataMappingHelper.Instance.getMapArray(Of KisoSiyouRecord)(GetType(KisoSiyouRecord), table)

            ' 取得結果を返却
            Return list(0)
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' 基礎仕様接続詞NOより判定名（基礎仕様接続詞）を取得します
    ''' </summary>
    ''' <param name="ksSiyouSetuzokusiNo">基礎仕様接続詞NO</param>
    ''' <returns>判定名（基礎仕様接続詞）</returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouSetuzokusiMei(ByVal ksSiyouSetuzokusiNo As Integer) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouSetuzokusiMei", _
                                                    ksSiyouSetuzokusiNo)

        ' 基礎仕様データアクセスクラス
        Dim dataAccess As New KisoSiyouDataAccess()

        ' 取得結果を返却
        Return dataAccess.GetHanteiSetuzokusiMei(ksSiyouSetuzokusiNo)

    End Function

    ''' <summary>
    ''' 基礎仕様マスタ検索
    ''' </summary>
    ''' <param name="strKisoSiyouNo">基礎仕様NO</param>
    ''' <param name="strKisoSiyouNm">基礎仕様名</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnMatchType">基礎仕様NO検索タイプ（True:完全一致 or False:前方一致）</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="allRowCount">検索結果全件数</param>
    ''' <param name="startRow">（任意）データ抽出時の開始行(1件目は1を指定)Default:1</param>
    ''' <param name="endRow">（任意）データ抽出時の終了行 Default:99999999</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKisoSiyouSearchResult(ByVal strKisoSiyouNo As String, _
                                    ByVal strKisoSiyouNm As String, _
                                    ByVal strKameitenCd As String, _
                                    ByVal blnMatchType As Boolean, _
                                    ByVal blnDelete As Boolean, _
                                    ByRef allRowCount As Integer, _
                                    Optional ByVal startRow As Integer = 1, _
                                    Optional ByVal endRow As Integer = 99999999) As List(Of KisoSiyouRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKisoSiyouSearchResult", _
                                            strKisoSiyouNo, _
                                            strKisoSiyouNm, _
                                            strKameitenCd, _
                                            blnMatchType, _
                                            blnDelete, _
                                            allRowCount, _
                                            startRow, _
                                            endRow)

        Dim dataAccess As KisoSiyouDataAccess = New KisoSiyouDataAccess

        Dim table As DataTable = dataAccess.GetKisoSiyouKensakuData(strKisoSiyouNo, _
                                                                   strKisoSiyouNm, _
                                                                   strKameitenCd, _
                                                                   blnMatchType, _
                                                                   blnDelete)


        ' 件数を設定
        allRowCount = table.Rows.Count

        Dim arrRtnData As List(Of KisoSiyouRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of KisoSiyouRecord)(GetType(KisoSiyouRecord), _
                                                                             table)

        Return arrRtnData
    End Function


    ''' <summary>
    ''' 基礎仕様NOより判定種別表示を返却します
    ''' </summary>
    ''' <param name="intHantei1Cd">判定コード1</param>
    ''' <param name="intHantei2Cd">判定コード2</param>
    ''' <returns>判定種別名</returns>
    ''' <remarks></remarks>
    Public Function GetHanteiSyunetuDisp(ByVal intHantei1Cd As Integer, ByVal intHantei2Cd As Integer) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanteiSyunetuDisp", _
                                                    intHantei1Cd _
                                                    , intHantei2Cd _
                                                    )

        Dim strReturn As String = "" '返却値

        If intHantei2Cd <> 0 And intHantei2Cd > 0 Then
            Return strReturn 
        End If

        strReturn = JudgeHanteiSyunetuDisp(intHantei1Cd)

        Return strReturn
    End Function

    ''' <summary>
    ''' 基礎仕様NOより判定種別表示を判定し、返却します
    ''' </summary>
    ''' <param name="ksSiyouNo">基礎仕様NO</param>
    ''' <returns>判定種別名</returns>
    ''' <remarks></remarks>
    Public Function JudgeHanteiSyunetuDisp(ByVal ksSiyouNo As Integer) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanteiSyunetuDisp", _
                                                    ksSiyouNo)

        ' 基礎仕様データアクセスクラス
        Dim dataAccess As New KisoSiyouDataAccess()
        Dim strReturn As String = "" '返却値

        Dim table As KisoSiyouDataSet.KisoSiyouTableDataTable = dataAccess.GetHanteiMei(ksSiyouNo)

        If Not table Is Nothing Then
            Dim row As KisoSiyouDataSet.KisoSiyouTableRow
            row = table.Rows(0)

            '工事判定フラグ
            Select Case row.koj_hantei_flg
                Case 0
                    If row.ks_siyou.Contains(pStrTigyou) Or row.ks_siyou.Contains(pStrTutinoTikan) Then
                        strReturn = pStrYouSyasin
                    Else
                        strReturn = pStrTyokusetuKiso
                    End If

                Case 1
                    strReturn = pStrKouji

                Case Else
                    strReturn = ""

            End Select

        Else
            strReturn = ""

        End If

        Return strReturn
    End Function
End Class
