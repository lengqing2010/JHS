Imports System.Transactions
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 年度計画値設定
''' </summary>
''' <remarks>年度計画値設定</remarks>
''' <history>
''' <para>2012/11/14 P-44979 王新 新規作成 </para>
''' </history>
Public Class NendoKeikakuInputBC
    Private objNendoKeikakuInputDA As New NendoKeikakuInputda

    ''' <summary>
    ''' 全社計画管理テーブルから計画情報を取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>全社の計画情報</returns>
    ''' <remarks>全社計画管理テーブルから計画情報を取得する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function GetZensyaKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return objNendoKeikakuInputDA.SelZensyaKeikakuKanriData(strKeikakuNendo)
    End Function

    ''' <summary>
    ''' 各支店の計画情報を取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="intSysYearFlg">0:システム年度がない、1:システム年度</param>
    ''' <returns>各支店の計画情報</returns>
    ''' <remarks>各支店の計画情報を取得する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function GetSitenbetuKeikakuKanriData(ByVal strKeikakuNendo As String, ByVal intSysYearFlg As Integer) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        If intSysYearFlg = 0 Then
            'システム年度がない場合
            Return objNendoKeikakuInputDA.SelBusyoKanriKeikakuKanriData(strKeikakuNendo)
        Else
            'システム年度の場合
            Return objNendoKeikakuInputDA.SelSitenbetuKeikakuKanriData(strKeikakuNendo)
        End If

    End Function

    ''' <summary>
    ''' 全社の最大登録日時のデータを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>全社の最大登録日時のデータ</returns>
    ''' <remarks>全社の最大登録日時のデータを取得する</remarks>
    Public Function GetMaxZensyaKeikakuKanriData(ByVal strKeikakuNendo As String) As String
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dtValue As DataTable
        Dim strReturnValue As String
        dtValue = objNendoKeikakuInputDA.SelMaxZensyaKeikakuKanriData(strKeikakuNendo)

        If dtValue Is Nothing OrElse dtValue.Rows.Count <= 0 Then
            strReturnValue = ""
        Else
            strReturnValue = Convert.ToString(dtValue.Rows(0)("add_datetime"))
        End If

        Return strReturnValue
    End Function

    ''' <summary>
    ''' 支店の最大登録日時のデータを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>支店の最大登録日時のデータ</returns>
    ''' <remarks>支店の最大登録日時のデータを取得する</remarks>
    Public Function GetMaxSitenbetuKeikakuKanriData(ByVal strKeikakuNendo As String) As String
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)


        Dim dtValue As DataTable
        Dim strReturnValue As String
        dtValue = objNendoKeikakuInputDA.SelMaxSitenbetuKeikakuKanriData(strKeikakuNendo)

        If dtValue Is Nothing OrElse dtValue.Rows.Count <= 0 Then
            strReturnValue = ""
        Else
            strReturnValue = Convert.ToString(dtValue.Rows(0)("add_datetime"))
        End If

        Return strReturnValue
    End Function

    ''' <summary>
    ''' 全社計画管理テーブルに登録する
    ''' </summary>
    ''' <param name="hstValues">登録データ</param>
    ''' <remarks>全社計画管理テーブルに登録する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Sub SetZensyaKeikakuKanriData(ByVal hstValues As Hashtable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, hstValues)

        objNendoKeikakuInputDA.InsZensyaKeikakuKanriData(hstValues)
    End Sub

    ''' <summary>
    ''' 支店別計画管理テーブルに登録する
    ''' </summary>
    ''' <param name="dtValue">登録データ</param>
    ''' <remarks>支店別計画管理テーブルに登録する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Sub SetSitenbetuKeikakuKanriData(ByVal dtValue As DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtValue)

        Dim i As Integer
        Dim options As New TransactionOptions

        '分離レベル スナップショットに明示的に指定
        options.IsolationLevel = IsolationLevel.Snapshot
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Try
                For i = 0 To dtValue.Rows.Count - 1
                    objNendoKeikakuInputDA.InsSitenbetuKeikakuKanriData(dtValue.Rows(i))
                Next
                '成功の場合
                scope.Complete()
            Catch ex As Exception
                '失敗の場合
                scope.Dispose()
                Throw ex
            End Try
        End Using
    End Sub

End Class
