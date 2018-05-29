
Imports Itis.Earth.DataAccess
Imports System.Transactions

Public Class TorihikiJyouhouLogic

    Private TorihikiJyouhouDA As New TorihikiJyouhouDataAccess()
    Private KakakuJyouhouDA As New KakakuseikyuJyouhouDataAccess()
    Private KakakuJyouhouBL As New KakakuseikyuJyouhouLogic()
    Private CommonHaita As New HaitaCheck()

    ''' <summary>
    ''' [取引情報]用　加盟店マスタ、検索 : 価格.請求情報部分のFunctionを利用
    ''' </summary>
    ''' <param name="KametenCd">加盟店コード</param>
    ''' <returns>加盟店テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetKameitenData(ByVal KametenCd As String) As KameitenDataSet.m_kameitenTableDataTable

        Return KakakuJyouhouDA.SelKameiten(KametenCd)

    End Function

    ''' <summary>
    ''' [取引情報]用　取引情報マスタ、検索
    ''' </summary>
    ''' <param name="KametenCd">加盟店コード</param>
    ''' <returns>取引テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTorihikiData(ByVal KametenCd As String) As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable
        ' 
        Return TorihikiJyouhouDA.SelTorihiki(KametenCd)

    End Function

    ''' <summary>
    ''' [取引情報]用　取引情報マスタ、重複チェック、新規また更新を判断する
    ''' </summary>
    ''' <param name="KametenCd">加盟店コード</param>
    ''' <returns>TRUE:存在,FALSE:不存在</returns>
    ''' <remarks></remarks>
    Public Function GetJyufukuData(ByVal KametenCd As String) As Boolean
        ' 
        Return TorihikiJyouhouDA.SelJyufukuData(KametenCd)

    End Function

    ''' <summary>
    ''' 取引登録処理
    ''' </summary>
    ''' <param name="KametenCd">加盟店コード</param>
    ''' <param name="gameiDate">画面時間</param>
    ''' <param name="dtTourihiki">取引テーブル</param>
    ''' <param name="bFlg">部分フラグ</param>
    ''' <returns>メッセージ</returns>
    ''' <remarks></remarks>
    Public Function TorihikiTouroku(ByVal KametenCd As String, ByRef gameiDate As DateTime, ByVal dtTourihiki As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable, ByVal bFlg As String) As String

        Dim strKekka As String
        Dim kino As String
        Dim rCnt As Int16

        kino = "取引情報_{0}の登録"

        If gameiDate <> CType("0:00:00", DateTime) Then

            '排他チェック
            strKekka = CommonHaita.CheckHaita(KametenCd, "m_kameiten_torihiki_jouhou", gameiDate)

            If strKekka <> "" Then
                Return strKekka
            End If

        End If

        rCnt = GetTorihikiData(KametenCd).Rows.Count

        '登録処理
        If InsUpdTorihiki(KametenCd, dtTourihiki, bFlg) Then

            If rCnt <> 0 Then
                gameiDate = GetTorihikiData(KametenCd).Rows(0).Item("upd_datetime")
            End If

            If bFlg = "gyoumu" Then
                kino = KakakuJyouhouBL.MakeMessage(kino, "業務")
                Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG018S, kino)
            Else
                kino = KakakuJyouhouBL.MakeMessage(kino, "経理")
                Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG018S, kino)
            End If
        Else
            If bFlg = "gyoumu" Then
                kino = KakakuJyouhouBL.MakeMessage(kino, "業務")
                Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG019E, kino)
            Else
                kino = KakakuJyouhouBL.MakeMessage(kino, "経理")
                Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG019E, kino)
            End If
        End If

    End Function

    ''' <summary>
    ''' [取引情報]用　取引情報（業務,経理）、新規登録また更新
    ''' </summary>
    ''' <param name="KametenCd">加盟店コード</param>
    ''' <param name="dtTourihiki">取引テーブル</param>
    ''' <param name="bFlg">部分フラグ</param>
    ''' <returns>TRUE:成功,FALSE:失敗</returns>
    ''' <remarks></remarks>
    Public Function InsUpdTorihiki(ByVal KametenCd As String, ByVal dtTourihiki As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable, ByVal bFlg As String) As Boolean

        Dim strKousinFlg As String
        If GetJyufukuData(KametenCd) Then
            strKousinFlg = "upd"
        Else
            strKousinFlg = "ins"
        End If

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            If bFlg = "gyoumu" Then
                If TorihikiJyouhouDA.InsUpdTorihikiGyoumu(dtTourihiki, strKousinFlg) Then
                    scope.Complete()
                    Return True
                Else
                    scope.Dispose()
                    Return False
                End If
            ElseIf bFlg = "keiri" Then
                If TorihikiJyouhouDA.InsUpdTorihikiKeiri(dtTourihiki, strKousinFlg) Then
                    scope.Complete()
                    Return True
                Else
                    scope.Dispose()
                    Return False
                End If
            Else
                scope.Dispose()
                Return False
            End If

        End Using

    End Function

    ''' <summary>
    ''' [取引情報]用　加盟店マスタ、更新
    ''' </summary>
    ''' <param name="KametenCd">加盟店コード</param>
    ''' <param name="dtKameiten">加盟店テーブル</param>
    ''' <returns>TRUE:成功,FALSE:失敗</returns>
    ''' <remarks></remarks>
    Public Function UpdKameiten(ByVal KametenCd As String, ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable) As String

        Dim kino As String

        kino = "取引情報の登録"

        '登録処理
        If UpdKameiten(dtKameiten) Then
            Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG018S, kino)
        Else
            Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG019E, kino)
        End If

    End Function

    ''' <summary>
    ''' 加盟店マスタ更新処理
    ''' </summary>
    ''' <param name="dtKameiten">加盟店テーブル</param>
    ''' <returns>TRUE:成功,FALSE:失敗</returns>
    ''' <remarks></remarks>
    Public Function UpdKameiten(ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable) As Boolean

        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            If TorihikiJyouhouDA.UpdKameiten(dtKameiten) Then
                scope.Complete()
                Return True
            Else
                scope.Dispose()
                Return False
            End If
        End Using

    End Function

    ''' <summary>
    ''' [取引情報]用　画面リスト項目データ取得
    ''' </summary>
    ''' <param name="dt">リスト用テーブル</param>
    ''' <param name="type">類型</param>
    ''' <param name="withSpaceRow">空白行</param>
    ''' <param name="withCd">コード</param>
    ''' <remarks></remarks>
    Public Sub GetListData(ByRef dt As DataTable, ByVal type As String, ByVal withSpaceRow As Boolean, Optional ByVal withCd As Boolean = True)

        ' 共通のコンボデータ設定メソッドを使用
        TorihikiJyouhouDA.GetDropdownData(dt, type, withSpaceRow, withCd)

    End Sub


    ''' <summary>
    ''' [取引情報]用　画面リスト項目データ取得
    ''' </summary>
    ''' <param name="dt">リスト用テーブル</param>
    ''' <param name="type">類型</param>
    ''' <param name="withSpaceRow">空白行</param>
    ''' <param name="withCd">コード</param>
    ''' <remarks></remarks>
    Public Sub GetListData6869(ByRef dt As DataTable, ByVal type As String, ByVal withSpaceRow As Boolean, Optional ByVal withCd As Boolean = True)

        ' 共通のコンボデータ設定メソッドを使用
        TorihikiJyouhouDA.GetDropdownData6869(dt, type, withSpaceRow, withCd)

    End Sub

End Class
