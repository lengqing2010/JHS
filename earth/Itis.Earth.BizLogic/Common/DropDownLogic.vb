''' <summary>
''' ドロップダウンデータ取得用ロジックです
''' </summary>
''' <remarks></remarks>
Public Class DropDownLogic
    Inherits DropDownLogicHelper

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' ドロップダウン用のデータテーブルを取得します
    ''' </summary>
    ''' <param name="type" >取得対象のデータタイプ（このクラスのDropDownTypeを指定）</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>   ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetComboList(ByVal type As Long, _
                          ByVal withSpaceRow As Boolean, _
                          Optional ByVal withCode As Boolean = True, _
                          Optional ByVal blnTorikesi As Boolean = True _
                          ) As ICollection

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetComboList", _
                                            type, _
                                            withCode)

        Dim data_access As AbsDataAccess = Nothing

        Select Case type
            Case 0
                ' 区分情報を取得するデータアクセスクラス
                data_access = New KubunDataAccess
            Case 1
                ' 調査概要情報を取得するデータアクセスクラス
                data_access = New TyousagaiyouDataAccess
            Case 2
                ' 調査方法情報を取得するデータアクセスクラス
                data_access = New TyousahouhouDataAccess
            Case 3
                ' 階層情報を取得するデータアクセスクラス
                data_access = New KaisouDataAccess
            Case 4
                ' 構造情報を取得するデータアクセスクラス
                data_access = New KouzouDataAccess
            Case 5
                ' 新築建替情報を取得するデータアクセスクラス
                data_access = New ShintikuTatekaeDataAccess
            Case 6
                ' 車庫情報を取得するデータアクセスクラス
                data_access = New SyakoDataAccess
            Case 7
                ' 建物用途情報を取得するデータアクセスクラス
                data_access = New TatemonoYoutoDataAccess
            Case 8
                ' 予定基礎情報を取得するデータアクセスクラス
                data_access = New YoteiKisoDataAccess
            Case 9
                ' ﾃﾞｰﾀ破棄情報を取得するデータアクセスクラス
                data_access = New DataHakiDataAccess
            Case 10
                ' 経由情報を取得するデータアクセスクラス
                data_access = New KeiyuDataAccess
            Case 11
                ' 区分情報を取得するデータアクセスクラス(保証書NO年月付与版)
                data_access = New KubunDataAccess2
            Case 12
                ' 保証書発行状況情報を取得するデータアクセスクラス
                data_access = New HosyousyoHakJykyAccess
            Case 13
                ' 保険会社情報を取得するデータアクセスクラス
                data_access = New HokenKaisyaAccess
            Case 14
                ' 消費税情報を取得するデータアクセスクラス
                data_access = New SyouhizeiAccess
            Case 15
                ' 都道府県情報を取得するデータアクセスクラス
                data_access = New TodoufukenDataAccess
            Case 16
                Return data_access
            Case 17
                Return data_access
            Case 18
                Return data_access
            Case 19
                Return data_access
            Case 20
                Return data_access
            Case 21
                Return data_access
            Case 22
                Return data_access
            Case 23
                Return data_access
            Case 24
                Return data_access
            Case 25
                Return data_access
            Case 26
                ' 改良工事種別情報を取得するデータアクセスクラス
                data_access = New KairyouKoujiSyubetuAccess
            Case 27
                ' 受理情報を取得するデータアクセスクラス
                data_access = New HkksJuriAccess
            Case 28
                ' 担当者(解析/工事)情報を取得するデータアクセスクラス
                data_access = New TantousyaAccess
            Case 29
                ' 基礎仕様接続詞情報を取得するデータアクセスクラス
                data_access = New KsSiyouSetuzokusiAccess
            Case 30
                ' 系列コード情報を取得するデータアクセスクラス
                data_access = New KeiretuDataAccess
            Case 31
                Return data_access
            Case 32
                Return data_access
            Case 33
                Return data_access
            Case 34
                '店別データ修正の登録料商品を取得するデータアクセスクラス
                data_access = New TenbetuSyuuseiSyouhinDataAccess(TenbetuSyuuseiSyouhinDataAccess.SearchMode.TOUROKU_RYOU)
            Case 35
                '店別データ修正の販促品初期ツール料を取得するデータアクセスクラス
                data_access = New TenbetuSyuuseiSyouhinDataAccess(TenbetuSyuuseiSyouhinDataAccess.SearchMode.TOOL_RYOU)
            Case 36
                '店別データ修正の登録料商品を取得するデータアクセスクラス
                data_access = New TenbetuSyuuseiSyouhinDataAccess(TenbetuSyuuseiSyouhinDataAccess.SearchMode.FC_RYOU)
            Case 37
                '店別データ修正の販促品初期ツール料を取得するデータアクセスクラス
                data_access = New TenbetuSyuuseiSyouhinDataAccess(TenbetuSyuuseiSyouhinDataAccess.SearchMode.NOT_FC_RYOU)

                '************************
                '* 商品コード情報
                '************************
            Case 38
                ' 商品情報を取得するデータアクセスクラス(分類コード:100)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN100)
            Case 39
                ' 商品情報を取得するデータアクセスクラス(分類コード:110)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN110)
            Case 40
                ' 商品情報を取得するデータアクセスクラス(分類コード:115)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN115)
            Case 41
                ' 商品情報を取得するデータアクセスクラス(分類コード:120)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN120)
            Case 42
                ' 商品情報を取得するデータアクセスクラス(分類コード:130)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN130)
            Case 43
                ' 商品情報を取得するデータアクセスクラス(分類コード:140)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN140)
            Case 44
                ' 商品情報を取得するデータアクセスクラス(分類コード:150)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN150)
            Case 45
                ' 商品情報を取得するデータアクセスクラス(分類コード:160)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN160)
            Case 46
                ' 商品情報を取得するデータアクセスクラス(分類コード:170)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN170)
            Case 47
                ' 商品情報を取得するデータアクセスクラス(分類コード:180)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN180)
            Case 48
                ' 商品情報を取得するデータアクセスクラス(分類コード:110,115)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN2)
            Case 49
                Return data_access
            Case 50
                Return data_access
            Case 51
                Return data_access
            Case 52
                Return data_access
            Case 53
                Return data_access
            Case 55
                ' 調査会社情報を取得するデータアクセスクラス
                data_access = New TyousakaisyaSearchDataAccess

            Case Else
                Return data_access
        End Select

        ' 要求されたコンボデータを取得
        Return CreateList(data_access, withSpaceRow, withCode, EarthEnum.emDdlType.MExcpMeisyouSyubetu, blnTorikesi)

    End Function

    ''' <summary>
    ''' ドロップダウン用のデータテーブルを取得します(名称種別専用)
    ''' </summary>
    ''' <param name="type" >取得対象のデータタイプ※EarthConstの名称タイプを指定する</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>   ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMeisyouComboList(ByVal type As EarthConst.emMeisyouType, _
                          ByVal withSpaceRow As Boolean, _
                          Optional ByVal withCode As Boolean = True, _
                          Optional ByVal blnTorikesi As Boolean = True _
                          ) As ICollection

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMeisyouComboList", _
                                            type, _
                                            withCode)

        Dim data_access As AbsDataAccess = Nothing

        ' 名称種別情報を取得するデータアクセスクラス
        data_access = New MeisyouDataAccess(type)

        ' 要求されたコンボデータを取得
        Return CreateList(data_access, withSpaceRow, withCode, EarthEnum.emDdlType.MMeisyouSyubetu, blnTorikesi)

    End Function

    ''' <summary>
    ''' ドロップダウン用のデータテーブルを取得します(拡張名称M専用)
    ''' </summary>
    ''' <param name="type" >取得対象のデータタイプ※EarthConstの名称タイプを指定する</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>   ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetKtMeisyouComboList(ByVal type As EarthConst.emKtMeisyouType, _
                          ByVal withSpaceRow As Boolean, _
                          Optional ByVal withCode As Boolean = True, _
                          Optional ByVal blnTorikesi As Boolean = True _
                          ) As ICollection

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouComboList", _
                                            type, _
                                            withCode)

        Dim data_access As AbsDataAccess = Nothing

        ' 名称種別情報を取得するデータアクセスクラス
        data_access = New KakutyouMeisyouDataAccess(type)

        ' 要求されたコンボデータを取得
        Return CreateList(data_access, withSpaceRow, withCode, EarthEnum.emDdlType.KtMeisyou, blnTorikesi)

    End Function

    ''' <summary>
    ''' ドロップダウン用のデータテーブルを取得します(拡張名称M専用、表示項目をパラメータで指定)
    ''' </summary>
    ''' <param name="type" >取得対象のデータタイプ※EarthConstの名称タイプを指定する</param>
    ''' <param name="ktMeisyouType">拡張名称Mドロップダウンタイプ</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <param name="blnTorikesi"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetKtMeisyouHannyouComboList(ByVal type As EarthConst.emKtMeisyouType, _
                                          ByVal ktMeisyouType As EarthEnum.emKtMeisyouType, _
                                          ByVal withSpaceRow As Boolean, _
                                          Optional ByVal withCode As Boolean = True, _
                                          Optional ByVal blnTorikesi As Boolean = True _
                                          ) As ICollection

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouHannyouComboList", _
                                                    type, _
                                                    ktMeisyouType, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    blnTorikesi)

        Dim data_access As AbsDataAccess = Nothing

        ' 名称種別情報を取得するデータアクセスクラス
        data_access = New KakutyouMeisyouDataAccess(type)

        ' 要求されたコンボデータを取得
        Return CreateHannyouList(data_access, ktMeisyouType, withSpaceRow, withCode, EarthEnum.emDdlType.KtMeisyou, blnTorikesi)

    End Function
End Class
