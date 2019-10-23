Imports System.Transactions


''' <summary>
''' JHS入金取込処理に関する処理を行うロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class JhsNyuukinTorikomiLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    'UtilitiesのStringLogicクラス
    Dim clsStrLogic As New StringLogic
#Region "プロパティ"
#Region "ログインユーザーID"
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> ログインユーザーID</returns>
    ''' <remarks></remarks>
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region
#End Region

    ''' <summary>
    ''' 前回JHS入金データ取込情報の取得
    ''' </summary>
    ''' <returns>前回JHS入金データ取込情報のデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetZenkaiTorikomiData() As UploadKanriRecord
        Dim clsDataAcc As New NyuukinSyoriDataAccess
        Dim zenkaiTorikomiTable As UploadKanriDataSet.UploadJouhouDataTable
        Dim recUpload As New UploadKanriRecord

        zenkaiTorikomiTable = clsDataAcc.GetZenkaiTorikomiData(1)

        If zenkaiTorikomiTable.Rows.Count > 0 Then
            recUpload = DataMappingHelper.Instance.getMapArray(Of UploadKanriRecord)(GetType(UploadKanriRecord), _
                                                                zenkaiTorikomiTable)(0)
        Else
            recUpload = Nothing
        End If

        Return recUpload
    End Function

    ''' <summary>
    ''' JHS入金データ取り込み処理
    ''' </summary>
    ''' <param name="ctlFileUpload">ファイルアップロードコントロール</param>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Public Function NyuukinDataTorikomi(ByVal ctlFileUpload As Web.UI.WebControls.FileUpload) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".NyuukinDataTorikomi" _
                                                    , ctlFileUpload)
        Dim strRetMsg As String = ""                        '戻り値用メッセージ格納変数
        Dim clsDataAcc As New JhsNyuukinTorikomiDataAccess  'JHS入金ファイル取込データアクセス
        Dim clsJbnAcc As New JibanDataAccess                '地盤データアクセス

        Dim myStream As IO.Stream                           '入出力ストリーム
        Dim myReader As IO.StreamReader                     'ストリームリーダー
        Dim strReadLine As String                           '取込ファイル読込み行

        Dim recTorikomi As NyuukinFileTorikomiRecord        '入金ファイル取込テーブル用レコードクラス
        Dim recUpload As UploadKanriRecord                  'アップロード管理テーブル用レコードクラス
        'Dim recError As NyuukinErrorRecord                  '入金エラー情報テーブル用レコードクラス

        Dim dtTorikomiDate As DateTime = DateTime.Now       '処理日時

        Dim intReadCnt As Integer = 0       '読込件数
        Dim intErrResult As Integer = 0     'エラー件数

        'Updateに必要なSQL情報を自動生成するクラス
        Dim insertMake As New InsertStringHelper
        'Insert文
        Dim insertString As String = ""
        '登録用パラメータの情報を格納するList(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        'CSVファイルの桁数
        Const NY_DATE As Integer = 8        '入金年月日
        Const DP_NO As Integer = 6          '伝票NO
        Const SAKI_CD As Integer = 5        '請求先コード
        Const SAKI_BRC As Integer = 2       '請求先枝番
        Const SAKI_KBN As Integer = 1       '請求先区分
        Const SAKI_MEI As Integer = 40      '請求先名
        Const SYG_KZ_NO As Integer = 10     '照合口座NO
        Const NYUUKIN_GAKU As Integer = 10  '入金額(10項目)
        Const TG_DATE As Integer = 8        '手形期日
        Const TG_NO As Integer = 10         '手形NO
        Const TEKIYOU As Integer = 30       '摘要名
        Const EDI_INFO As Integer = 14       'EDI情報作成日
        'CSVファイルの総桁数
        Const CSV_MAX_DIGIT As Integer = NY_DATE _
                                        + DP_NO _
                                        + SAKI_CD _
                                        + SAKI_BRC _
                                        + SAKI_KBN _
                                        + SAKI_MEI _
                                        + SYG_KZ_NO _
                                        + NYUUKIN_GAKU * 10 _
                                        + TG_DATE _
                                        + TG_NO _
                                        + TEKIYOU _
                                        + EDI_INFO
        Dim intCsvDigit As Integer          '項目取得開始位置(桁目)

        'アップロード機能によりファイルのストリームを取得する
        myStream = ctlFileUpload.FileContent
        myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '取込データの編集
                Do
                    '取込ファイルを読み込む
                    strReadLine = myReader.ReadLine()
                    intReadCnt += 1
                    '読込み行の文字列長を確認
                    If clsStrLogic.GetStrByteSJIS(strReadLine) <> CSV_MAX_DIGIT Then
                        Return String.Format(Messages.MSG058E, intReadCnt)
                        Exit Function
                    End If

                    '読込み行を型を変換してレコードクラスに格納する
                    recTorikomi = New NyuukinFileTorikomiRecord

                    '項目取得開始位置のクリア
                    intCsvDigit = 0
                    With recTorikomi
                        '入金日
                        .NyuukinDate = fnStringToDate(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NY_DATE))
                        intCsvDigit += NY_DATE
                        '伝票番号
                        .TorikomiDenpyouNo = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, DP_NO)
                        intCsvDigit += DP_NO
                        '請求先コード
                        .SeikyuuSakiCd = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, SAKI_CD)
                        intCsvDigit += SAKI_CD
                        '請求先枝番
                        .SeikyuuSakiBrc = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, SAKI_BRC)
                        intCsvDigit += SAKI_BRC
                        '請求先区分
                        .SeikyuuSakiKbn = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, SAKI_KBN)
                        intCsvDigit += SAKI_KBN
                        '請求先名
                        .SeikyuuSakiMei = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, SAKI_MEI)
                        intCsvDigit += SAKI_MEI
                        '照合口座No.
                        .SyougouKouzaNo = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, SYG_KZ_NO)
                        intCsvDigit += SYG_KZ_NO
                        '入金額[現金]
                        .Genkin = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[小切手]
                        .Kogitte = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[口座振替]
                        .KouzaFurikae = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[振込]
                        .Furikomi = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[手形]
                        .Tegata = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[協力会費]
                        .KyouryokuKaihi = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[振込手数料]
                        .FurikomiTesuuryou = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[相殺]
                        .Sousai = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[値引]
                        .Nebiki = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '入金額[その他]
                        .Sonota = Long.Parse(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, NYUUKIN_GAKU))
                        intCsvDigit += NYUUKIN_GAKU
                        '手形期日
                        .TegataKijitu = fnStringToDate(clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, TG_DATE))
                        intCsvDigit += TG_DATE
                        '手形No.
                        .TegataNo = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, TG_NO)
                        intCsvDigit += TG_NO
                        '摘要名
                        .TekiyouMei = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, TEKIYOU)
                        intCsvDigit += TEKIYOU
                        'EDI情報作成日
                        .EdiJouhouSakuseiDate = clsStrLogic.SubstringByByte(strReadLine, intCsvDigit, EDI_INFO)
                        intCsvDigit += EDI_INFO

                        '取消
                        .Torikesi = 0
                        '登録ログインユーザーID
                        .AddLoginUserId = LoginUserId
                        '登録日時
                        .AddDatetime = dtTorikomiDate

                        '必須チェック
                        If .NyuukinDate = DateTime.MinValue Then
                            strRetMsg &= Messages.MSG013E.Replace("@PARAM1", "入金日")
                            strRetMsg &= String.Format(Messages.MSG137E, intReadCnt)
                            Return strRetMsg
                            Exit Function
                        End If

                        '重複チェック
                        If intReadCnt = 1 Then
                            If clsDataAcc.chkTyoufuku(.EdiJouhouSakuseiDate) Then
                                Return String.Format(Messages.MSG059E)
                                Exit Function
                            End If
                        End If

                        '読込み行を入金ファイル取込テーブルへ登録する
                        insertString = insertMake.MakeUpdateInfo(GetType(NyuukinFileTorikomiRecord), recTorikomi, list)
                        clsJbnAcc.UpdateJibanData(insertString, list)

                    End With

                Loop Until myReader.EndOfStream

                'アップロード管理テーブルへ入金情報を登録する
                recUpload = New UploadKanriRecord

                recUpload.FileKbn = 1                                   '入金区分
                recUpload.SyoriDatetime = dtTorikomiDate                '処理日時
                recUpload.NyuuryokuFileMei = ctlFileUpload.FileName     '入力ファイル名
                recUpload.EdiJouhouSakuseiDate = recTorikomi.EdiJouhouSakuseiDate   'EDI情報作成日
                If intErrResult > 0 Then
                    recUpload.ErrorUmu = 1                              'エラー有無
                Else
                    recUpload.ErrorUmu = 0                              'エラー有無
                End If
                recUpload.AddLoginUserId = LoginUserId                  '登録ログインユーザーID
                recUpload.AddDatetime = dtTorikomiDate                  '更新ログインユーザーID

                insertString = insertMake.MakeUpdateInfo(GetType(UploadKanriRecord), recUpload, list)
                clsJbnAcc.UpdateJibanData(insertString, list)

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()
            End Using

        Catch sqlTimeOut As System.Data.SqlClient.SqlException
            If sqlTimeOut.ErrorCode = -2146232060 _
            AndAlso sqlTimeOut.State = 0 _
            AndAlso sqlTimeOut.Number = -2 _
            AndAlso sqlTimeOut.Message.Contains(EarthConst.TIMEOUT_KEYWORD) = True Then
                strRetMsg = Messages.MSG116E & clsStrLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strRetMsg
                Exit Function
            Else
                strRetMsg = Messages.MSG118E & clsStrLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strRetMsg
                Exit Function
            End If
        Catch tranTimeOut As System.Transactions.TransactionException
            strRetMsg = Messages.MSG117E & clsStrLogic.RemoveSpecStr(tranTimeOut.Message)
            UnTrappedExceptionManager.Publish(tranTimeOut)
            Return strRetMsg
            Exit Function
        Catch ex As Exception
            strRetMsg = Messages.MSG118E & clsStrLogic.RemoveSpecStr(ex.Message) & String.Format(Messages.MSG137E, intReadCnt)
            UnTrappedExceptionManager.Publish(ex)
            Return strRetMsg
            Exit Function
        End Try

        '完了メッセージを表示
        strRetMsg = Messages.MSG060S

        '警告メッセージを表示
        If intErrResult > 0 Then
            strRetMsg &= Messages.MSG138W
        End If

        Return strRetMsg

    End Function

    ''' <summary>
    ''' 文字列を日付型に変換します
    ''' </summary>
    ''' <param name="strDate">日付文字列</param>
    ''' <returns>日付型に変換した日付</returns>
    ''' <remarks>例：19820514 ⇒ 1982/05/14</remarks>
    Private Function fnStringToDate(ByVal strDate As String) As Date
        Dim dtRet As Date
        If strDate = "00000000" OrElse strDate.Trim = String.Empty Then
            dtRet = Date.MinValue
        Else
            dtRet = Date.Parse(Format(Integer.Parse(strDate), "0000/00/00"))
        End If

        Return dtRet
    End Function

End Class
