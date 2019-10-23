''' <summary>
''' データアクセスクラス用抽象クラスです
''' 主にコンボボックスデータの設定に使用する為、
''' コンボボックスデータの作成に関係ない場合はInherits不要
''' </summary>
''' <remarks>画面表示用のデータカラム　"CmbTextField"<br/>
'''          Valueのデータカラム　     "CmbValueField"</remarks>
Public Class AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コンボボックス設定用のデータテーブルを取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetDropdownData(ByRef dt As DataTable, _
                                           ByVal withSpaceRow As Boolean, _
                                           Optional ByVal withCode As Boolean = True, _
                                           Optional ByVal blnTorikesi As Boolean = True)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", _
                                            dt, _
                                            withSpaceRow, _
                                            withCode, _
                                            blnTorikesi)

        ' 基底クラスでは処理しません

    End Sub

    ''' <summary>
    ''' コンボボックス設定用のデータテーブルを取得します(※名称M.名称種別専用)
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetMeisyouDropdownData(ByRef dt As DataTable, _
                                           ByVal withSpaceRow As Boolean, _
                                           Optional ByVal withCode As Boolean = True, _
                                           Optional ByVal blnTorikesi As Boolean = True)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMeisyouDropdownData", _
                                            dt, _
                                            withSpaceRow, _
                                            withCode)

        ' 基底クラスでは処理しません

    End Sub

    ''' <summary>
    ''' コンボボックス設定用のデータテーブルを取得します(※拡張名称M専用)
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetKtMeisyouDropdownData(ByRef dt As DataTable, _
                                           ByVal withSpaceRow As Boolean, _
                                           Optional ByVal withCode As Boolean = True, _
                                           Optional ByVal blnTorikesi As Boolean = True)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouDropdownData", _
                                            dt, _
                                            withSpaceRow, _
                                            withCode)

        ' 基底クラスでは処理しません

    End Sub

    ''' <summary>
    ''' コンボボックス設定用のデータテーブルを取得します(※拡張名称M専用、表示項目をパラメータで指定)
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="type">拡張名称Mドロップダウンタイプ</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <param name="blnTorikesi">（任意）拡張名称Mの項目に取消は存在しない</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetKtMeisyouHannyouDropdownData(ByRef dt As DataTable, _
                                                           ByVal type As EarthEnum.emKtMeisyouType, _
                                                           ByVal withSpaceRow As Boolean, _
                                                           Optional ByVal withCode As Boolean = True, _
                                                           Optional ByVal blnTorikesi As Boolean = True)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouHannyouDropdownData", _
                                                    dt, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    blnTorikesi)

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
    Function CreateRow(ByVal Text As String, _
                       ByVal Value As String, _
                       ByVal dt As DataTable) As DataRow

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateRow", _
                                            Text, _
                                            Value, _
                                            dt)

        Dim dr As DataRow = dt.NewRow()
        dr(0) = Text
        dr(1) = Value
        Return dr

    End Function

End Class
