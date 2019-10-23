
''' <summary>
''' コンボボックス用のデータ取得基底クラスです
''' </summary>
''' <remarks></remarks>
Public Class DropDownLogicHelper

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コンボ用のデータテーブルを取得します
    ''' </summary>
    ''' <param name="data_access">コンボ設定データ取得用アクセスクラスのインスタンス</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>    
    ''' <param name="intDdlType" >ドロップダウンリストタイプ[名称M or 拡張名称M]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateList(ByVal data_access As AbsDataAccess, _
                        ByVal withSpaceRow As Boolean, _
                        Optional ByVal withCode As Boolean = True, _
                        Optional ByVal intDdlType As EarthEnum.emDdlType = EarthEnum.emDdlType.MExcpMeisyouSyubetu, _
                        Optional ByVal blnTorikesi As Boolean = True _
                        ) As ICollection

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateList", _
                                            data_access, _
                                            withSpaceRow, _
                                            withCode)
        ' コンボデータとなるDataTable
        Dim dt As DataTable = New DataTable()

        ' DataTableへのカラム設定
        dt.Columns.Add(New DataColumn("CmbTextField", GetType(String)))
        dt.Columns.Add(New DataColumn("CmbValueField", GetType(String)))

        ' ドロップダウンリストタイプ
        If intDdlType = EarthEnum.emDdlType.MMeisyouSyubetu Then
            data_access.GetMeisyouDropdownData(dt, withSpaceRow, withCode, blnTorikesi)
        ElseIf intDdlType = EarthEnum.emDdlType.MExcpMeisyouSyubetu Then
            data_access.GetDropdownData(dt, withSpaceRow, withCode, blnTorikesi)
        ElseIf intDdlType = EarthEnum.emDdlType.KtMeisyou Then
            data_access.GetKtMeisyouDropdownData(dt, withSpaceRow, withCode, blnTorikesi)
        End If

        ' DataView を作成し返却
        Dim dv As DataView = New DataView(dt)

        Return dv

    End Function

    ''' <summary>
    ''' コンボ用のデータテーブルを取得します(表示項目をパラメータで指定)
    ''' </summary>
    ''' <param name="data_access">コンボ設定データ取得用アクセスクラスのインスタンス</param>
    ''' <param name="type">拡張名称Mドロップダウンタイプ</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>    
    ''' <param name="intDdlType" >ドロップダウンリストタイプ[名称M or 拡張名称M]</param>
    ''' <param name="blnTorikesi">（任意）拡張名称Mの項目に取消は存在しない</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateHannyouList(ByVal data_access As AbsDataAccess, _
                               ByVal type As EarthEnum.emKtMeisyouType, _
                               ByVal withSpaceRow As Boolean, _
                               Optional ByVal withCode As Boolean = True, _
                               Optional ByVal intDdlType As EarthEnum.emDdlType = EarthEnum.emDdlType.MExcpMeisyouSyubetu, _
                               Optional ByVal blnTorikesi As Boolean = True _
                               ) As ICollection

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateHannyouList", _
                                                    data_access, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    blnTorikesi)
        ' コンボデータとなるDataTable
        Dim dt As DataTable = New DataTable()

        ' DataTableへのカラム設定
        dt.Columns.Add(New DataColumn("CmbTextField", GetType(String)))
        dt.Columns.Add(New DataColumn("CmbValueField", GetType(String)))

        ' ドロップダウンリストタイプ
        If intDdlType = EarthEnum.emDdlType.KtMeisyou Then
            data_access.GetKtMeisyouHannyouDropdownData(dt, type, withSpaceRow, withCode, blnTorikesi)
        End If

        ' DataView を作成し返却
        Dim dv As DataView = New DataView(dt)

        Return dv

    End Function
End Class
