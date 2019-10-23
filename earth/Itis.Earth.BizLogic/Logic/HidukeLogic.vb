''' <summary>
''' 日付マスタ編集用ロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class HidukeLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "SQL作成種別"
    ''' <summary>
    ''' SQL作成種別
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SqlType
        ''' <summary>
        ''' 登録SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_INSERT = 1
        ''' <summary>
        ''' 更新SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_UPDATE = 2
        ''' <summary>
        ''' 削除SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_DELETE = 3
    End Enum
#End Region

    ''' <summary>
    ''' 日付Saveマスタの情報を取得します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <returns>日付Saveマスタレコード</returns>
    ''' <remarks></remarks>
    Public Function GetHidukeRecord(ByVal kbn As String) As HidukeSaveRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHidukeRecord", _
                                                    kbn)
        Dim record As HidukeSaveRecord = Nothing

        Dim dataaccess As New HidukeSaveDataAccess

        Dim hidukeSaveList As List(Of HidukeSaveRecord) = _
                 DataMappingHelper.Instance.getMapArray(Of HidukeSaveRecord)(GetType(HidukeSaveRecord), _
                 dataaccess.GetHidukeSaveData(kbn))

        If hidukeSaveList.Count > 0 Then
            record = hidukeSaveList(0)
        End If

        Return record

    End Function

#Region "日付Saveマスタ更新"
    ''' <summary>
    ''' 日付Saveマスタの登録・更新・削除を実施します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="hidukeRec">DB反映対象の日付Saveマスタレコード</param>
    ''' <returns>処理結果 true:成功 false:失敗</returns>
    ''' <remarks></remarks>
    Public Function EditHidukeSaveRecord(ByVal sender As Object, _
                                       ByVal hidukeRec As HidukeSaveRecord) As Boolean

        ' 地盤データ用データアクセスクラス
        Dim dataaccess As New HidukeSaveDataAccess

        ' 現在のレコードを取得
        Dim chkHidukeList As List(Of HidukeSaveRecord) = _
                DataMappingHelper.Instance.getMapArray(Of HidukeSaveRecord)( _
                GetType(HidukeSaveRecord), _
                dataaccess.GetHidukeSaveData(hidukeRec.Kbn))

        ' 存在チェック用レコード保持
        Dim checkRec As New HidukeSaveRecord

        ' 現在データを保持
        If chkHidukeList.Count > 0 Then
            checkRec = chkHidukeList(0)
        End If

        ' 日付Saveマスタデータの登録・更新・削除を実施します
        If hidukeRec Is Nothing Then

            ' 削除されたレコードか確認する
            If chkHidukeList.Count > 0 Then

                ' 日付Saveマスタ 削除実施
                If EditTeibetuSeikyuuData(checkRec, SqlType.SQL_DELETE) = False Then

                    'UtilitiesのMessegeLogicクラス
                    Dim messageLogic As New MessageLogic

                    ' 削除時エラー
                    messageLogic.DbErrorMessage(sender, _
                                                "削除", _
                                                "日付Saveマスタ", _
                                                "区分:" & checkRec.Kbn)

                    ' 削除に失敗したので処理中断
                    Return False
                End If

            End If

        Else
            If chkHidukeList.Count > 0 Then

                ' 排他チェック
                If hidukeRec.UpdDatetime = checkRec.UpdDatetime Then

                    '更新日時、更新ログインユーザーIDをセット
                    hidukeRec.UpdDatetime = DateTime.Now
                    hidukeRec.UpdLoginUserId = hidukeRec.UpdLoginUserId

                    ' 更新
                    If EditTeibetuSeikyuuData(hidukeRec, SqlType.SQL_UPDATE) = False Then

                        'UtilitiesのMessegeLogicクラス
                        Dim messageLogic As New MessageLogic

                        ' 更新時エラー
                        messageLogic.DbErrorMessage(sender, _
                                                    "更新", _
                                                    "日付Saveマスタ", _
                                                    "区分:" & checkRec.Kbn)
                        ' 更新失敗時、処理を中断する
                        Return False
                    End If

                Else
                    ' 排他チェックエラー
                    'UtilitiesのMessegeLogicクラス
                    Dim messageLogic As New MessageLogic

                    ' 排他チェックエラー
                    messageLogic.CallHaitaErrorMessage(sender, _
                                                       checkRec.UpdLoginUserId, _
                                                       checkRec.UpdDatetime, _
                                                       "区分:" & checkRec.Kbn)
                    Return False
                End If
            Else
                '登録日時、登録ログインユーザーIDをセット
                hidukeRec.AddDatetime = DateTime.Now
                hidukeRec.AddLoginUserId = hidukeRec.UpdLoginUserId

                ' 既存データが無いので登録
                If EditTeibetuSeikyuuData(hidukeRec, SqlType.SQL_INSERT) = False Then

                    'UtilitiesのMessegeLogicクラス
                    Dim messageLogic As New MessageLogic

                    ' 登録時エラー
                    messageLogic.DbErrorMessage(sender, _
                                                "登録", _
                                                "日付Saveマスタ", _
                                                "区分:" & checkRec.Kbn)

                    ' 登録失敗時、処理を中断する
                    Return False
                End If
            End If
        End If

        Return True
    End Function

#Region "日付Saveマスタデータ編集"
    ''' <summary>
    ''' 日付Saveマスタデータの追加・更新・削除を実行します。
    ''' </summary>
    ''' <param name="hidukeRec">日付Saveマスタレコード</param>
    ''' <returns>True:登録成功 False:登録失敗</returns>
    ''' <remarks></remarks>
    Private Function EditTeibetuSeikyuuData(ByVal hidukeRec As HidukeSaveRecord, _
                                            ByVal _sqlType As SqlType) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuSeikyuuData", _
                                                    hidukeRec)

        ' SQL情報を自動生成するInterface
        Dim sqlMake As ISqlStringHelper = Nothing

        ' 処理によりインスタンスを生成する
        Select Case _sqlType
            Case SqlType.SQL_INSERT
                sqlMake = New InsertStringHelper
            Case SqlType.SQL_UPDATE
                sqlMake = New UpdateStringHelper
            Case SqlType.SQL_DELETE
                sqlMake = New DeleteStringHelper
        End Select

        Dim sqlString As String = ""
        ' パラメータの情報を格納する List(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        sqlString = sqlMake.MakeUpdateInfo(GetType(HidukeSaveRecord), hidukeRec, list)

        ' 地盤データ取得用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' DB反映処理
        If dataAccess.UpdateJibanData(sqlString, list) = False Then
            Return False
        End If

        Return True

    End Function
#End Region

#End Region

#Region "画面表示I/O"
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
                                                    Str)

        ' 戻り値となるStringデータ
        Dim ret As String = str

        If obj Is Nothing Then
            ' NULL は基本的に空白を返す
            Return ret
        ElseIf obj.GetType().ToString() = GetType(String).ToString Then
            ' Stringの場合
            If obj = "" Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(Integer).ToString Then
            ' Integerの場合
            If obj = Integer.MinValue Then
                Return ret
            Else
                Dim intData As Integer = obj
                ret = intData.ToString("###,###,###")
            End If
        ElseIf obj.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimalの場合
            If obj = Decimal.MinValue Then
                Return ret
            Else
                Dim decData As Decimal = obj
                ret = decData.ToString("###,###,###.###")
            End If
        ElseIf obj.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTimeの場合
            If obj = DateTime.MinValue Then
                Return ret
            Else
                ret = Format(obj, "yyyy/MM/dd")
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
    ''' <returns>変換結果</returns>
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
        End If

        ' 変換対象以外の型はエラー
        objChangeData = Nothing
        Return False

    End Function
#End Region

End Class
