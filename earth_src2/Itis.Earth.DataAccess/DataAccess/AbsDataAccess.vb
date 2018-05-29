''' <summary>
''' データアクセスクラス用抽象クラスです
''' 主にコンボボックスデータの設定に使用する為、
''' コンボボックスデータの作成に関係ない場合はInherits不要
''' </summary>
''' <remarks>画面表示用のデータカラム　"CmbTextField"<br/>
'''          Valueのデータカラム　     "CmbValueField"</remarks>
Public Class AbsDataAccess

    ''' <summary>
    ''' コンボボックス設定用のデータテーブルを取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True)
        ' 基底クラスでは処理しません
    End Sub

    ''' <summary>
    ''' 区分情報のDataRowを作成します
    ''' </summary>
    ''' <param name="Text">画面表示されるテキスト 区分：区分名</param>
    ''' <param name="Value">区分</param>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateRow(ByVal Text As String, ByVal Value As String, ByVal dt As DataTable) As DataRow

        Dim dr As DataRow = dt.NewRow()
        dr(0) = Text
        dr(1) = Value
        Return dr

    End Function

End Class
