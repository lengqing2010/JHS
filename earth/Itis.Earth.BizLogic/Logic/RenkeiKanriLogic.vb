Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.text

''' <summary>
''' 各種連携管理テーブルに関する処理を行うロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class RenkeiKanriLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "邸別請求連携"
    ''' <summary>
    ''' 邸別請求テーブルの登録/更新内容を邸別請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">邸別請求連携管理レコード</param>
    ''' <returns>登録/更新結果</returns>
    ''' <remarks>１レコードを更新する場合</remarks>
    Public Function EditTeibetuSeikyuuRenkeiData(ByVal renkeiRec As TeibetuSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable
        Dim row As TeibetuRenkeiDataSet.RenkeiTableRow

        intResult = 0
        renkeiTable = clsDataAccess.SelectTeibetuSeikyuuRenkeiData(renkeiRec)

        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、邸別請求は新規でも更新)
            renkeiRec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If renkeiRec.RenkeiSijiCd <> 9 Then
                If renkeiRec.IsUpdate = True Then
                    ' 既存データの更新時は2
                    renkeiRec.RenkeiSijiCd = 2
                Else
                    renkeiRec.RenkeiSijiCd = 1
                End If
            End If
            intResult = clsDataAccess.InsertTeibetuSeikyuuRenkeiData(renkeiRec)

        Else
            row = renkeiTable.Rows(0)

            ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               renkeiRec.RenkeiSijiCd <> 9 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 1
            End If

            ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               renkeiRec.RenkeiSijiCd = 1 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 2
            End If

            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            intResult = clsDataAccess.UpdateTeibetuSeikyuuRenkeiData(renkeiRec, True)
        End If

        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブルの登録/更新内容を邸別請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiTable">邸別請求連携管理テーブルの更新対象のキーを格納したデータテーブル</param>
    ''' <param name="strLoginUserId">更新ログインユーザー</param>
    ''' <param name="blnIsUpdate">更新状況フラグ</param>
    ''' <param name="blnIsDelete">削除状況フラグ</param>
    ''' <returns>処理件数</returns>
    ''' <remarks>複数レコードを更新する場合</remarks>
    Public Function EditTeibetuSeikyuuRenkeiRecords(ByVal renkeiTable As TeibetuRenkeiDataSet.TeibetuRenkeiTargetDataTable, _
                                                    ByVal strLoginUserId As String, _
                                                    ByVal blnIsUpdate As Boolean, _
                                            Optional ByVal blnIsDelete As Boolean = False) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuSeikyuuRenkeiRecords", _
                                                    renkeiTable, _
                                                    strLoginUserId, _
                                                    blnIsUpdate, _
                                                    blnIsDelete)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiRec As New TeibetuSeikyuuRenkeiRecord
        Dim row As TeibetuRenkeiDataSet.TeibetuRenkeiTargetRow

        For intCnt As Integer = 0 To renkeiTable.Rows.Count - 1
            With renkeiTable.Rows(intCnt)
                renkeiRec.Kbn = .Item("kbn")
                renkeiRec.HosyousyoNo = .Item("hosyousyo_no")
                renkeiRec.BunruiCd = .Item("bunrui_cd")
                renkeiRec.GamenHyoujiNo = .Item("gamen_hyouji_no")
                renkeiRec.UpdLoginUserId = strLoginUserId
                renkeiRec.IsUpdate = blnIsUpdate
                If blnIsDelete Then
                    renkeiRec.RenkeiSijiCd = 9
                Else
                    renkeiRec.RenkeiSijiCd = 2
                End If
                renkeiRec.SousinJykyCd = 0
                If IsDBNull(.Item("sousin_jyky_cd")) Then
                    ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、邸別請求は新規でも更新)
                    renkeiRec.SousinJykyCd = 0
                    If blnIsDelete = False Then
                        If renkeiRec.IsUpdate = True Then
                            ' 既存データの更新時は2
                            renkeiRec.RenkeiSijiCd = 2
                        Else
                            renkeiRec.RenkeiSijiCd = 1
                        End If
                    End If
                    intResult += clsDataAccess.InsertTeibetuSeikyuuRenkeiData(renkeiRec)
                Else
                    row = renkeiTable.Rows(intCnt)
                    ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 1 And _
                       renkeiRec.RenkeiSijiCd <> 9 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 1
                    End If
                    ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 9 And _
                       renkeiRec.RenkeiSijiCd = 1 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 2
                    End If
                    ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
                    intResult += clsDataAccess.UpdateTeibetuSeikyuuRenkeiData(renkeiRec, True)
                End If
            End With
        Next
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブルの削除内容を邸別請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">邸別請求連携管理レコード</param>
    ''' <returns>削除結果</returns>
    ''' <remarks></remarks>
    Public Function DeleteTeibetuSeikyuuRenkeiData(ByVal renkeiRec As TeibetuSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteTeibetuSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable

        intResult = 0
        renkeiTable = clsDataAccess.SelectTeibetuSeikyuuRenkeiData(renkeiRec)

        renkeiRec.RenkeiSijiCd = 9
        renkeiRec.SousinJykyCd = 0

        If renkeiTable.Rows.Count < 1 Then
            '値が取得できない場合は新規でInsert
            intResult = clsDataAccess.InsertTeibetuSeikyuuRenkeiData(renkeiRec)
        End If
        intResult = clsDataAccess.UpdateTeibetuSeikyuuRenkeiData(renkeiRec, True)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求連携管理テーブルを一括更新します
    ''' </summary>
    ''' <param name="strInsetTmpSql">更新対象を連携用テンポラリーテーブルに登録するSQL</param>
    ''' <param name="cmdParams">更新対象絞込用SQLパラメータ</param>
    ''' <param name="strTmpTableName">テンポラリーテーブルの名前</param>
    ''' <param name="strLoginUserid">ログインユーザーID</param>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function EditTeibetuSeikyuuRenkeiLump(ByVal strInsetTmpSql As String, _
                                                ByVal cmdParams() As SqlParameter, _
                                                ByVal strTmpTableName As String, _
                                                ByVal strLoginUserid As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuSeikyuuRenkeiLump" _
                                                    , strInsetTmpSql _
                                                    , cmdParams _
                                                    , strTmpTableName _
                                                    , strLoginUserid)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdRenkeiParams() As SqlParameter

        Dim clsAcc As New RenkeiKanriDataAccess
        Dim cmnDtAcc As New CmnDataAccess

        'テンポラリーテーブル名を指定
        clsAcc.TmpTableTeibetuRenkei = strTmpTableName

        '邸別請求連携テーブル用テンポラリーテーブル作成用SQL
        cmdTextSb.Append(clsAcc.GetCreateSqlRenkeiTmpForTeibetu())

        '更新対象を連携用テンポラリーテーブルに登録するSQL
        cmdTextSb.Append(strInsetTmpSql)

        '連携用SQLパラメータを取得
        cmdRenkeiParams = clsAcc.GetRenkeiCmdParams(strLoginUserid)

        '更新対象絞込SQLパラメータと連携用SQLパラメータを結合
        cmnDtAcc.AddSqlParameter(cmdParams, cmdRenkeiParams)

        '邸別請求連携テーブルを更新
        intResult = clsAcc.ExecRenkeiTeibetuLump(cmdTextSb, cmdParams)

        Return intResult
    End Function

#End Region

#Region "店別初期請求連携"
    ''' <summary>
    ''' 店別初期請求テーブルの登録/更新内容を店別初期請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">店別初期請求連携管理レコード</param>
    ''' <returns>登録/更新結果</returns>
    ''' <remarks></remarks>
    Public Function EditTenbetuSyokiSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSyokiSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTenbetuSyokiSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable
        Dim row As TenbetuSyokiRenkeiDataSet.RenkeiTableRow

        intResult = 0
        renkeiTable = clsDataAccess.SelectTenbetuSyokiSeikyuuRenkeiData(renkeiRec)

        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする
            renkeiRec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If renkeiRec.RenkeiSijiCd <> 9 Then
                If renkeiRec.IsUpdate = True Then
                    ' 既存データの更新時は2
                    renkeiRec.RenkeiSijiCd = 2
                Else
                    renkeiRec.RenkeiSijiCd = 1
                End If
            End If
            intResult = clsDataAccess.InsertTenbetuSyokiSeikyuuRenkeiData(renkeiRec)

        Else
            row = renkeiTable.Rows(0)

            ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               renkeiRec.RenkeiSijiCd <> 9 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 1
            End If

            ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               renkeiRec.RenkeiSijiCd = 1 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 2
            End If

            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            intResult = clsDataAccess.UpdateTenbetuSyokiSeikyuuRenkeiData(renkeiRec, True)
        End If

        Return intResult
    End Function

    ''' <summary>
    ''' 店別初期請求テーブルの登録/更新内容を店別初期請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiTable">店別初期請求連携管理テーブルの更新対象のキーを格納したデータテーブル</param>
    ''' <param name="strLoginUserId">更新ログインユーザー</param>
    ''' <param name="blnIsUpdate">更新状況フラグ</param>
    ''' <param name="blnIsDelete">削除状況フラグ</param>
    ''' <returns>処理件数</returns>
    ''' <remarks>複数レコードを更新する場合</remarks>
    Public Function EditTenbetuSyokiSeikyuuRenkeiRecords(ByVal renkeiTable As TenbetuSyokiRenkeiDataSet.TenbetuSyokiRenkeiTargetDataTable, _
                                                    ByVal strLoginUserId As String, _
                                                    ByVal blnIsUpdate As Boolean, _
                                            Optional ByVal blnIsDelete As Boolean = False) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTenbetuSyokiSeikyuuRenkeiRecords", _
                                                    renkeiTable, _
                                                    strLoginUserId, _
                                                    blnIsUpdate, _
                                                    blnIsDelete)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiRec As New TenbetuSyokiSeikyuuRenkeiRecord
        Dim row As TenbetuSyokiRenkeiDataSet.TenbetuSyokiRenkeiTargetRow

        For intCnt As Integer = 0 To renkeiTable.Rows.Count - 1
            With renkeiTable.Rows(intCnt)
                renkeiRec.MiseCd = .Item("mise_cd")
                renkeiRec.BunruiCd = .Item("bunrui_cd")
                renkeiRec.UpdLoginUserId = strLoginUserId
                renkeiRec.IsUpdate = blnIsUpdate
                If blnIsDelete Then
                    renkeiRec.RenkeiSijiCd = 9
                Else
                    renkeiRec.RenkeiSijiCd = 2
                End If
                renkeiRec.SousinJykyCd = 0
                If IsDBNull(.Item("sousin_jyky_cd")) Then
                    ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、店別初期請求は新規でも更新)
                    renkeiRec.SousinJykyCd = 0
                    If blnIsDelete = False Then
                        If renkeiRec.IsUpdate = True Then
                            ' 既存データの更新時は2
                            renkeiRec.RenkeiSijiCd = 2
                        Else
                            renkeiRec.RenkeiSijiCd = 1
                        End If
                    End If
                    intResult += clsDataAccess.InsertTenbetuSyokiSeikyuuRenkeiData(renkeiRec)
                Else
                    row = renkeiTable.Rows(intCnt)
                    ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 1 And _
                       renkeiRec.RenkeiSijiCd <> 9 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 1
                    End If
                    ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 9 And _
                       renkeiRec.RenkeiSijiCd = 1 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 2
                    End If
                    ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
                    intResult += clsDataAccess.UpdateTenbetuSyokiSeikyuuRenkeiData(renkeiRec, True)
                End If
            End With
        Next
        Return intResult
    End Function

    ''' <summary>
    ''' 店別初期請求テーブルの削除内容を店別初期請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">店別初期請求連携管理レコード</param>
    ''' <returns>削除結果</returns>
    ''' <remarks></remarks>
    Public Function DeleteTenbetuSyokiSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSyokiSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteTenbetuSyokiSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable

        intResult = 0
        renkeiTable = clsDataAccess.SelectTenbetuSyokiSeikyuuRenkeiData(renkeiRec)

        renkeiRec.RenkeiSijiCd = 9
        renkeiRec.SousinJykyCd = 0

        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする
            intResult = clsDataAccess.InsertTenbetuSyokiSeikyuuRenkeiData(renkeiRec)
        End If
        intResult = clsDataAccess.UpdateTenbetuSyokiSeikyuuRenkeiData(renkeiRec, True)
        Return intResult
    End Function
#End Region

#Region "店別請求連携"
    ''' <summary>
    ''' 店別請求テーブルの登録/更新内容を店別請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">店別請求連携管理レコード</param>
    ''' <returns>登録/更新結果</returns>
    ''' <remarks></remarks>
    Public Function EditTenbetuSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTenbetuSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable
        Dim row As TenbetuRenkeiDataSet.RenkeiTableRow

        intResult = 0
        renkeiTable = clsDataAccess.SelectTenbetuSeikyuuRenkeiData(renkeiRec)

        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、店別請求は新規でも更新)
            renkeiRec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If renkeiRec.RenkeiSijiCd <> 9 Then

                If renkeiRec.IsUpdate = True Then
                    ' 既存データの更新時は2
                    renkeiRec.RenkeiSijiCd = 2
                Else
                    renkeiRec.RenkeiSijiCd = 1
                End If
            End If
            intResult = clsDataAccess.InsertTenbetuSeikyuuRenkeiData(renkeiRec)

        Else
            row = renkeiTable.Rows(0)

            ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               renkeiRec.RenkeiSijiCd <> 9 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 1
            End If

            ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               renkeiRec.RenkeiSijiCd = 1 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 2
            End If

            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            intResult = clsDataAccess.UpdateTenbetuSeikyuuRenkeiData(renkeiRec, True)
        End If

        Return intResult
    End Function

    ''' <summary>
    ''' 店別請求テーブルの登録/更新内容を店別請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiTable">店別請求連携管理テーブルの更新対象のキーを格納したデータテーブル</param>
    ''' <param name="strLoginUserId">更新ログインユーザー</param>
    ''' <param name="blnIsUpdate">更新状況フラグ</param>
    ''' <param name="blnIsDelete">削除状況フラグ</param>
    ''' <returns>処理件数</returns>
    ''' <remarks>複数レコードを更新する場合</remarks>
    Public Function EditTenbetuSeikyuuRenkeiRecords(ByVal renkeiTable As TenbetuRenkeiDataSet.TenbetuRenkeiTargetDataTable, _
                                                    ByVal strLoginUserId As String, _
                                                    ByVal blnIsUpdate As Boolean, _
                                            Optional ByVal blnIsDelete As Boolean = False) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTenbetuSeikyuuRenkeiRecords", _
                                                    renkeiTable, _
                                                    strLoginUserId, _
                                                    blnIsUpdate, _
                                                    blnIsDelete)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiRec As New TenbetuSeikyuuRenkeiRecord
        Dim row As TenbetuRenkeiDataSet.TenbetuRenkeiTargetRow

        For intCnt As Integer = 0 To renkeiTable.Rows.Count - 1
            With renkeiTable.Rows(intCnt)
                renkeiRec.MiseCd = .Item("mise_cd")
                renkeiRec.BunruiCd = .Item("bunrui_cd")
                renkeiRec.NyuuryokuDate = .Item("nyuuryoku_date")
                renkeiRec.NyuuryokuDateNo = .Item("nyuuryoku_date_no")
                renkeiRec.UpdLoginUserId = strLoginUserId
                renkeiRec.IsUpdate = blnIsUpdate
                If blnIsDelete Then
                    renkeiRec.RenkeiSijiCd = 9
                Else
                    renkeiRec.RenkeiSijiCd = 2
                End If
                renkeiRec.SousinJykyCd = 0
                If IsDBNull(.Item("sousin_jyky_cd")) Then
                    ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、店別請求は新規でも更新)
                    renkeiRec.SousinJykyCd = 0
                    If blnIsDelete = False Then
                        If renkeiRec.IsUpdate = True Then
                            ' 既存データの更新時は2
                            renkeiRec.RenkeiSijiCd = 2
                        Else
                            renkeiRec.RenkeiSijiCd = 1
                        End If
                    End If
                    intResult += clsDataAccess.InsertTenbetuSeikyuuRenkeiData(renkeiRec)
                Else
                    row = renkeiTable.Rows(intCnt)
                    ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 1 And _
                       renkeiRec.RenkeiSijiCd <> 9 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 1
                    End If
                    ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 9 And _
                       renkeiRec.RenkeiSijiCd = 1 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 2
                    End If
                    ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
                    intResult += clsDataAccess.UpdateTenbetuSeikyuuRenkeiData(renkeiRec, True)
                End If
            End With
        Next
        Return intResult
    End Function

    ''' <summary>
    ''' 店別請求テーブルの削除内容を店別請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">店別請求連携管理レコード</param>
    ''' <returns>削除結果</returns>
    ''' <remarks></remarks>
    Public Function DeleteTenbetuSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteTenbetuSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess

        renkeiRec.RenkeiSijiCd = 9
        renkeiRec.SousinJykyCd = 0
        intResult = clsDataAccess.UpdateTenbetuSeikyuuRenkeiData(renkeiRec, True)
        Return intResult
    End Function
#End Region

#Region "邸別入金連携"
    ''' <summary>
    ''' 邸別入金テーブルの登録/更新内容を邸別入金連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <returns>登録/更新結果</returns>
    ''' <remarks>１レコードを更新する場合</remarks>
    Public Function EditTeibetuNyuukinRenkeiData(ByVal renkeiRec As TeibetuNyuukinRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuNyuukinRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable
        Dim row As TeibetuNyuukinRenkeiDataSet.RenkeiTableRow

        intResult = 0
        renkeiTable = clsDataAccess.SelectTeibetuNyuukinRenkeiData(renkeiRec)

        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする
            renkeiRec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If renkeiRec.RenkeiSijiCd <> 9 Then
                If renkeiRec.IsUpdate = True Then
                    ' 既存データの更新時は2
                    renkeiRec.RenkeiSijiCd = 2
                Else
                    renkeiRec.RenkeiSijiCd = 1
                End If
            End If
            intResult = clsDataAccess.InsertTeibetuNyuukinRenkeiData(renkeiRec)

        Else
            row = renkeiTable.Rows(0)

            ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               renkeiRec.RenkeiSijiCd <> 9 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 1
            End If

            ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               renkeiRec.RenkeiSijiCd = 1 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 2
            End If

            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            intResult = clsDataAccess.UpdateTeibetuNyuukinRenkeiData(renkeiRec, True)
        End If

        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金テーブルの登録/更新内容を邸別入金連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiTable">邸別入金連携管理テーブルの更新対象のキーを格納したデータテーブル</param>
    ''' <param name="strLoginUserId">更新ログインユーザー</param>
    ''' <param name="blnIsUpdate">更新状況フラグ</param>
    ''' <param name="blnIsDelete">削除状況フラグ</param>
    ''' <returns>処理件数</returns>
    ''' <remarks>複数レコードを更新する場合</remarks>
    Public Function EditTeibetuNyuukinRenkeiRecords(ByVal renkeiTable As DataTable, _
                                                    ByVal strLoginUserId As String, _
                                                    ByVal blnIsUpdate As Boolean, _
                                            Optional ByVal blnIsDelete As Boolean = False) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuNyuukinRenkeiRecords", _
                                                    renkeiTable, _
                                                    strLoginUserId, _
                                                    blnIsUpdate, _
                                                    blnIsDelete)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiRec As New TeibetuNyuukinRenkeiRecord
        Dim row As TeibetuNyuukinRenkeiDataSet.TeibetuNyuukinRenkeiTargetRow

        For intCnt As Integer = 0 To renkeiTable.Rows.Count - 1
            With renkeiTable.Rows(intCnt)
                renkeiRec.Kbn = .Item("kbn")
                renkeiRec.HosyousyoNo = .Item("hosyousyo_no")
                renkeiRec.BunruiCd = .Item("bunrui_cd")
                renkeiRec.GamenHyoujiNo = .Item("gamen_hyouji_no")
                renkeiRec.UpdLoginUserId = strLoginUserId
                renkeiRec.IsUpdate = blnIsUpdate
                If blnIsDelete Then
                    renkeiRec.RenkeiSijiCd = 9
                Else
                    renkeiRec.RenkeiSijiCd = 2
                End If
                renkeiRec.SousinJykyCd = 0
                If IsDBNull(.Item("sousin_jyky_cd")) Then
                    ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、邸別入金は新規でも更新)
                    renkeiRec.SousinJykyCd = 0
                    If blnIsDelete = False Then
                        If renkeiRec.IsUpdate = True Then
                            ' 既存データの更新時は2
                            renkeiRec.RenkeiSijiCd = 2
                        Else
                            renkeiRec.RenkeiSijiCd = 1
                        End If
                    End If
                    intResult += clsDataAccess.InsertTeibetuNyuukinRenkeiData(renkeiRec)
                Else
                    row = renkeiTable.Rows(intCnt)
                    ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 1 And _
                       renkeiRec.RenkeiSijiCd <> 9 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 1
                    End If
                    ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 9 And _
                       renkeiRec.RenkeiSijiCd = 1 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 2
                    End If
                    ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
                    intResult += clsDataAccess.UpdateTeibetuNyuukinRenkeiData(renkeiRec, True)
                End If
            End With
        Next
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金テーブルの削除内容を邸別入金連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <returns>削除結果</returns>
    ''' <remarks></remarks>
    Public Function DeleteTeibetuNyuukinRenkeiData(ByVal renkeiRec As TeibetuNyuukinRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteTeibetuNyuukinRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable

        intResult = 0
        renkeiTable = clsDataAccess.SelectTeibetuNyuukinRenkeiData(renkeiRec)

        renkeiRec.RenkeiSijiCd = 9
        renkeiRec.SousinJykyCd = 0

        If renkeiTable.Rows.Count < 1 Then
            '値が取得できない場合は新規でInsert
            intResult = clsDataAccess.InsertTeibetuNyuukinRenkeiData(renkeiRec)
        End If
        intResult = clsDataAccess.UpdateTeibetuNyuukinRenkeiData(renkeiRec, True)
        Return intResult
    End Function
#End Region

#Region "邸別入金連携(旧バージョン－削除予定)"
    ''' <summary>
    ''' 邸別入金テーブルの登録/更新内容を邸別入金連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <returns>登録/更新結果</returns>
    ''' <remarks>１レコードを更新する場合</remarks>
    Public Function EditTeibetuNyuukinRenkeiDataOld(ByVal renkeiRec As TeibetuNyuukinRenkeiRecordOld) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuNyuukinRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable
        Dim row As TeibetuNyuukinRenkeiDataSet.RenkeiTableRow

        intResult = 0
        renkeiTable = clsDataAccess.SelectTeibetuNyuukinRenkeiDataOld(renkeiRec)

        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする
            renkeiRec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If renkeiRec.RenkeiSijiCd <> 9 Then
                If renkeiRec.IsUpdate = True Then
                    ' 既存データの更新時は2
                    renkeiRec.RenkeiSijiCd = 2
                Else
                    renkeiRec.RenkeiSijiCd = 1
                End If
            End If
            intResult = clsDataAccess.InsertTeibetuNyuukinRenkeiDataOld(renkeiRec)

        Else
            row = renkeiTable.Rows(0)

            ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               renkeiRec.RenkeiSijiCd <> 9 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 1
            End If

            ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               renkeiRec.RenkeiSijiCd = 1 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 2
            End If

            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            intResult = clsDataAccess.UpdateTeibetuNyuukinRenkeiDataOld(renkeiRec, True)
        End If

        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金テーブルの登録/更新内容を邸別入金連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiTable">邸別入金連携管理テーブルの更新対象のキーを格納したデータテーブル</param>
    ''' <param name="strLoginUserId">更新ログインユーザー</param>
    ''' <param name="blnIsUpdate">更新状況フラグ</param>
    ''' <param name="blnIsDelete">削除状況フラグ</param>
    ''' <returns>処理件数</returns>
    ''' <remarks>複数レコードを更新する場合</remarks>
    Public Function EditTeibetuNyuukinRenkeiRecordsOld(ByVal renkeiTable As TeibetuNyuukinRenkeiDataSet.TeibetuNyuukinRenkeiTargetDataTable, _
                                                    ByVal strLoginUserId As String, _
                                                    ByVal blnIsUpdate As Boolean, _
                                            Optional ByVal blnIsDelete As Boolean = False) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuNyuukinRenkeiRecords", _
                                                    renkeiTable, _
                                                    strLoginUserId, _
                                                    blnIsUpdate, _
                                                    blnIsDelete)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiRec As New TeibetuNyuukinRenkeiRecordOld
        Dim row As TeibetuNyuukinRenkeiDataSet.TeibetuNyuukinRenkeiTargetRow

        For intCnt As Integer = 0 To renkeiTable.Rows.Count - 1
            With renkeiTable.Rows(intCnt)
                renkeiRec.Kbn = .Item("kbn")
                renkeiRec.HosyousyoNo = .Item("hosyousyo_no")
                renkeiRec.BunruiCd = .Item("bunrui_cd")
                renkeiRec.UpdLoginUserId = strLoginUserId
                renkeiRec.IsUpdate = blnIsUpdate
                If blnIsDelete Then
                    renkeiRec.RenkeiSijiCd = 9
                Else
                    renkeiRec.RenkeiSijiCd = 2
                End If
                renkeiRec.SousinJykyCd = 0
                If IsDBNull(.Item("sousin_jyky_cd")) Then
                    ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、邸別入金は新規でも更新)
                    renkeiRec.SousinJykyCd = 0
                    If blnIsDelete = False Then
                        If renkeiRec.IsUpdate = True Then
                            ' 既存データの更新時は2
                            renkeiRec.RenkeiSijiCd = 2
                        Else
                            renkeiRec.RenkeiSijiCd = 1
                        End If
                    End If
                    intResult += clsDataAccess.InsertTeibetuNyuukinRenkeiDataOld(renkeiRec)
                Else
                    row = renkeiTable.Rows(intCnt)
                    ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 1 And _
                       renkeiRec.RenkeiSijiCd <> 9 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 1
                    End If
                    ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 9 And _
                       renkeiRec.RenkeiSijiCd = 1 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 2
                    End If
                    ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
                    intResult += clsDataAccess.UpdateTeibetuNyuukinRenkeiDataOld(renkeiRec, True)
                End If
            End With
        Next
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブルの削除内容を邸別請求連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">邸別請求連携管理レコード</param>
    ''' <returns>削除結果</returns>
    ''' <remarks></remarks>
    Public Function DeleteTeibetuNyuukinRenkeiDataOld(ByVal renkeiRec As TeibetuNyuukinRenkeiRecordOld) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteTeibetuNyuukinRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable

        intResult = 0
        renkeiTable = clsDataAccess.SelectTeibetuNyuukinRenkeiDataOld(renkeiRec)

        renkeiRec.RenkeiSijiCd = 9
        renkeiRec.SousinJykyCd = 0

        If renkeiTable.Rows.Count < 1 Then
            '値が取得できない場合は新規でInsert
            intResult = clsDataAccess.InsertTeibetuNyuukinRenkeiDataOld(renkeiRec)
        End If
        intResult = clsDataAccess.UpdateTeibetuNyuukinRenkeiDataOld(renkeiRec, True)
        Return intResult
    End Function
#End Region

#Region "地盤連携"
    ''' <summary>
    ''' 地盤テーブルの登録/更新内容を地盤連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">地盤連携管理レコード</param>
    ''' <returns>登録/更新結果</returns>
    ''' <remarks>１レコードを更新する場合</remarks>
    Public Function EditJibanRenkeiData(ByVal renkeiRec As JibanRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditJibanRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable
        Dim row As TeibetuRenkeiDataSet.RenkeiTableRow

        intResult = 0
        renkeiTable = clsDataAccess.SelectJibanRenkeiData(renkeiRec)

        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする
            renkeiRec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If renkeiRec.RenkeiSijiCd <> 9 Then
                If renkeiRec.IsUpdate = True Then
                    ' 既存データの更新時は2
                    renkeiRec.RenkeiSijiCd = 2
                Else
                    renkeiRec.RenkeiSijiCd = 1
                End If
            End If
            intResult = clsDataAccess.InsertJibanRenkeiData(renkeiRec)

        Else
            row = renkeiTable.Rows(0)

            ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               renkeiRec.RenkeiSijiCd <> 9 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 1
            End If

            ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               renkeiRec.RenkeiSijiCd = 1 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 2
            End If

            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            intResult = clsDataAccess.UpdateJibanRenkeiData(renkeiRec, True)
        End If

        Return intResult
    End Function

    ''' <summary>
    ''' 地盤テーブルの登録/更新内容を地盤連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiTable">地盤連携管理テーブルの更新対象のキーを格納したデータテーブル</param>
    ''' <param name="strLoginUserId">更新ログインユーザー</param>
    ''' <param name="blnIsUpdate">更新状況フラグ</param>
    ''' <param name="blnIsDelete">削除状況フラグ</param>
    ''' <returns>処理件数</returns>
    ''' <remarks>複数レコードを更新する場合</remarks>
    Public Function EditJibanRenkeiRecords(ByVal renkeiTable As JibanRenkeiDataSet.JibanRenkeiTargetDataTable, _
                                            ByVal strLoginUserId As String, _
                                                    ByVal blnIsUpdate As Boolean, _
                                            Optional ByVal blnIsDelete As Boolean = False) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditJibanRenkeiRecords", _
                                                    renkeiTable, _
                                                    strLoginUserId, _
                                                    blnIsUpdate, _
                                                    blnIsDelete)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiRec As New JibanRenkeiRecord
        Dim row As JibanRenkeiDataSet.JibanRenkeiTargetRow

        For intCnt As Integer = 0 To renkeiTable.Rows.Count - 1
            With renkeiTable.Rows(intCnt)
                renkeiRec.Kbn = .Item("kbn")
                renkeiRec.HosyousyoNo = .Item("hosyousyo_no")
                renkeiRec.UpdLoginUserId = strLoginUserId
                renkeiRec.IsUpdate = blnIsUpdate
                If blnIsDelete Then
                    renkeiRec.RenkeiSijiCd = 9
                Else
                    renkeiRec.RenkeiSijiCd = 2
                End If
                renkeiRec.SousinJykyCd = 0
                If IsDBNull(.Item("sousin_jyky_cd")) Then
                    ' 値が取得できない場合は新規でInsertする(地盤ありは更新の為、邸別入金は新規でも更新)
                    renkeiRec.SousinJykyCd = 0
                    If blnIsDelete = False Then
                        If renkeiRec.IsUpdate = True Then
                            ' 既存データの更新時は2
                            renkeiRec.RenkeiSijiCd = 2
                        Else
                            renkeiRec.RenkeiSijiCd = 1
                        End If
                    End If
                    intResult += clsDataAccess.InsertJibanRenkeiData(renkeiRec)
                Else
                    row = renkeiTable.Rows(intCnt)
                    ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 1 And _
                       renkeiRec.RenkeiSijiCd <> 9 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 1
                    End If
                    ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 9 And _
                       renkeiRec.RenkeiSijiCd = 1 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 2
                    End If
                    ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
                    intResult += clsDataAccess.UpdateJibanRenkeiData(renkeiRec, True)
                End If
            End With
        Next
        Return intResult
    End Function

    ''' <summary>
    ''' 地盤テーブルの削除内容を地盤連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">地盤連携管理レコード</param>
    ''' <returns>削除結果</returns>
    ''' <remarks></remarks>
    Public Function DeleteJibanRenkeiData(ByVal renkeiRec As JibanRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteJibanRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable

        intResult = 0
        renkeiTable = clsDataAccess.SelectJibanRenkeiData(renkeiRec)

        renkeiRec.RenkeiSijiCd = 9
        renkeiRec.SousinJykyCd = 0

        If renkeiTable.Rows.Count < 1 Then
            '値が取得できない場合は新規でInsert
            intResult = clsDataAccess.InsertJibanRenkeiData(renkeiRec)
        End If
        intResult = clsDataAccess.UpdateJibanRenkeiData(renkeiRec, True)
        Return intResult
    End Function
#End Region

#Region "更新履歴連携"
    ''' <summary>
    ''' 更新履歴テーブルの登録/更新内容を更新履歴連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">更新履歴連携管理レコード</param>
    ''' <returns>登録/更新結果</returns>
    ''' <remarks>１レコードを更新する場合</remarks>
    Public Function EditKousinRirekiRenkeiData(ByVal renkeiRec As KousinRirekiRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditKousinRirekiRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable
        Dim row As TeibetuRenkeiDataSet.RenkeiTableRow

        intResult = 0
        renkeiTable = clsDataAccess.SelectKousinRirekiRenkeiData(renkeiRec)

        If renkeiTable.Rows.Count < 1 Then
            ' 値が取得できない場合は新規でInsertする
            renkeiRec.SousinJykyCd = 0
            '削除の場合は送信状況コードを削除でInsert
            If renkeiRec.RenkeiSijiCd <> 9 Then
                If renkeiRec.IsUpdate = True Then
                    ' 既存データの更新時は2
                    renkeiRec.RenkeiSijiCd = 2
                Else
                    renkeiRec.RenkeiSijiCd = 1
                End If
            End If
            intResult = clsDataAccess.InsertKousinRirekiRenkeiData(renkeiRec)

        Else
            row = renkeiTable.Rows(0)

            ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               renkeiRec.RenkeiSijiCd <> 9 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 1
            End If

            ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               renkeiRec.RenkeiSijiCd = 1 Then
                renkeiRec.SousinJykyCd = 0
                renkeiRec.RenkeiSijiCd = 2
            End If

            ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
            intResult = clsDataAccess.UpdateKousinRirekiRenkeiData(renkeiRec, True)
        End If

        Return intResult
    End Function

    ''' <summary>
    ''' 更新履歴テーブルの登録/更新内容を更新履歴連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiTable">更新履歴連携管理テーブルの更新対象のキーを格納したデータテーブル</param>
    ''' <param name="strLoginUserId">更新ログインユーザー</param>
    ''' <param name="blnIsUpdate">更新状況フラグ</param>
    ''' <param name="blnIsDelete">削除状況フラグ</param>
    ''' <returns>処理件数</returns>
    ''' <remarks>複数レコードを更新する場合</remarks>
    Public Function EditKousinRirekiRenkeiRecords(ByVal renkeiTable As KousinRirekiRenkeiDataSet.KousinRirekiRenkeiTargetDataTable, _
                                            ByVal strLoginUserId As String, _
                                                    ByVal blnIsUpdate As Boolean, _
                                            Optional ByVal blnIsDelete As Boolean = False) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditKousinRirekiRenkeiRecords", _
                                                    renkeiTable, _
                                                    strLoginUserId, _
                                                    blnIsUpdate, _
                                                    blnIsDelete)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiRec As New KousinRirekiRenkeiRecord
        Dim row As KousinRirekiRenkeiDataSet.KousinRirekiRenkeiTargetRow

        For intCnt As Integer = 0 To renkeiTable.Rows.Count - 1
            With renkeiTable.Rows(intCnt)
                renkeiRec.UpdDatetime = .Item("upd_datetime_rireki")
                renkeiRec.Kbn = .Item("kbn")
                renkeiRec.HosyousyoNo = .Item("hosyousyo_no")
                renkeiRec.UpdKoumoku = .Item("upd_koumoku")
                renkeiRec.UpdLoginUserId = strLoginUserId
                renkeiRec.IsUpdate = blnIsUpdate
                If blnIsDelete Then
                    renkeiRec.RenkeiSijiCd = 9
                Else
                    renkeiRec.RenkeiSijiCd = 2
                End If
                renkeiRec.SousinJykyCd = 0
                If IsDBNull(.Item("sousin_jyky_cd")) Then
                    ' 値が取得できない場合は新規でInsertする(更新履歴ありは更新の為、邸別入金は新規でも更新)
                    renkeiRec.SousinJykyCd = 0
                    If blnIsDelete = False Then
                        If renkeiRec.IsUpdate = True Then
                            ' 既存データの更新時は2
                            renkeiRec.RenkeiSijiCd = 2
                        Else
                            renkeiRec.RenkeiSijiCd = 1
                        End If
                    End If
                    intResult += clsDataAccess.InsertKousinRirekiRenkeiData(renkeiRec)
                Else
                    row = renkeiTable.Rows(intCnt)
                    ' 新規データが未送信且つ今回削除以外の場合、新規・未送信のままとする
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 1 And _
                       renkeiRec.RenkeiSijiCd <> 9 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 1
                    End If
                    ' 削除データが未送信で今回新規の場合、更新・未送信に変更する
                    If row.sousin_jyky_cd = 0 And _
                       row.renkei_siji_cd = 9 And _
                       renkeiRec.RenkeiSijiCd = 1 Then
                        renkeiRec.SousinJykyCd = 0
                        renkeiRec.RenkeiSijiCd = 2
                    End If
                    ' 送信状況コードが送信済の場合、送信コード：未送信、連携指示コード：更新でUPDATE
                    intResult += clsDataAccess.UpdateKousinRirekiRenkeiData(renkeiRec, True)
                End If
            End With
        Next
        Return intResult
    End Function

    ''' <summary>
    ''' 更新履歴テーブルの削除内容を更新履歴連携管理テーブルに連携します
    ''' </summary>
    ''' <param name="renkeiRec">更新履歴連携管理レコード</param>
    ''' <returns>削除結果</returns>
    ''' <remarks></remarks>
    Public Function DeleteKousinRirekiRenkeiData(ByVal renkeiRec As KousinRirekiRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteKousinRirekiRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer
        Dim clsDataAccess As New RenkeiKanriDataAccess
        Dim renkeiTable As DataTable

        intResult = 0
        renkeiTable = clsDataAccess.SelectKousinRirekiRenkeiData(renkeiRec)

        renkeiRec.RenkeiSijiCd = 9
        renkeiRec.SousinJykyCd = 0

        If renkeiTable.Rows.Count < 1 Then
            '値が取得できない場合は新規でInsert
            intResult = clsDataAccess.InsertKousinRirekiRenkeiData(renkeiRec)
        End If
        intResult = clsDataAccess.UpdateKousinRirekiRenkeiData(renkeiRec, True)
        Return intResult
    End Function
#End Region


End Class
