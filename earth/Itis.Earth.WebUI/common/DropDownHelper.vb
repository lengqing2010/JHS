Imports System.Web.UI.WebControls


''' <summary>
''' ドロップダウンリスト生成ヘルパークラス
''' </summary>
''' <remarks></remarks>
Public Class DropDownHelper

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "ドロップダウンリストの作成種類"
    ''' <summary>
    ''' ドロップダウンリストの作成種類
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DropDownType As Long
        ''' <summary>
        ''' 区分
        ''' </summary>
        ''' <remarks></remarks>
        Kubun = 0
        ''' <summary>
        ''' 調査概要
        ''' </summary>
        ''' <remarks></remarks>
        TyousaGaiyou = 1
        ''' <summary>
        ''' 調査方法
        ''' </summary>
        ''' <remarks></remarks>
        TyousaHouhou = 2
        ''' <summary>
        ''' 階層
        ''' </summary>
        ''' <remarks></remarks>
        Kaisou = 3
        ''' <summary>
        ''' 構造
        ''' </summary>
        ''' <remarks></remarks>
        Kouzou = 4
        ''' <summary>
        ''' 新築建替
        ''' </summary>
        ''' <remarks></remarks>
        ShintikuTatekae = 5
        ''' <summary>
        ''' 車庫
        ''' </summary>
        ''' <remarks></remarks>
        Syako = 6
        ''' <summary>
        ''' 建物用途
        ''' </summary>
        ''' <remarks></remarks>
        TatemonoYouto = 7
        ''' <summary>
        ''' 予定基礎
        ''' </summary>
        ''' <remarks></remarks>
        YoteiKiso = 8
        ''' <summary>
        ''' ﾃﾞｰﾀ破棄種別
        ''' </summary>
        ''' <remarks></remarks>
        DataHaki = 9
        ''' <summary>
        ''' 経由
        ''' </summary>
        ''' <remarks></remarks>
        Keiyu = 10
        ''' <summary>
        ''' 区分2（保証書NO年月付与版）
        ''' </summary>
        ''' <remarks></remarks>
        Kubun2 = 11
        ''' <summary>
        ''' 保証書発行状況
        ''' </summary>
        ''' <remarks></remarks>
        HosyousyoHakJyky = 12
        ''' <summary>
        ''' 保険会社
        ''' </summary>
        ''' <remarks></remarks>
        HokenKaisya = 13
        ''' <summary>
        ''' 消費税
        ''' </summary>
        ''' <remarks></remarks>
        Syouhizei = 14
        ''' <summary>
        ''' 都道府県
        ''' </summary>
        ''' <remarks></remarks>
        Todoufuken = 15
        ''' <summary>
        ''' 改良工事種別
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKoujiSyubetu = 26
        ''' <summary>
        ''' 受理
        ''' </summary>
        ''' <remarks></remarks>
        HkksJuri = 27
        ''' <summary>
        ''' 担当者(解析/工事)
        ''' </summary>
        ''' <remarks></remarks>
        Tantousya = 28
        ''' <summary>
        ''' 基礎仕様接続詞
        ''' </summary>
        ''' <remarks></remarks>
        KsSiyouSetuzokusi = 29
        ''' <summary>
        ''' 系列コード
        ''' </summary>
        ''' <remarks></remarks>
        KeiretuCd = 30
        ''' <summary>
        ''' 登録料商品
        ''' </summary>
        ''' <remarks></remarks>
        TourokuRyouSyouhin = 34
        ''' <summary>
        ''' 販促品初期ツール料
        ''' </summary>
        ''' <remarks></remarks>
        ToolRYouSyouhin = 35
        ''' <summary>
        ''' FC販促品商品
        ''' </summary>
        ''' <remarks></remarks>
        FcSyouhin = 36
        ''' <summary>
        ''' FC以外販促品商品
        ''' </summary>
        ''' <remarks></remarks>
        NotFcSyouhin = 37
        ''' <summary>
        ''' 商品(分類コード:100)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin1 = 38
        ''' <summary>
        ''' 商品(分類コード:110)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2_110 = 39
        ''' <summary>
        ''' 商品(分類コード:115)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2_115 = 40
        ''' <summary>
        ''' 商品(分類コード:120)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin3 = 41
        ''' <summary>
        ''' 商品(分類コード:130)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinKouji = 42
        ''' <summary>
        ''' 商品(分類コード:140)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinTuika = 43
        ''' <summary>
        ''' 商品(分類コード:150)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinTysHkSaihakkou = 44
        ''' <summary>
        ''' 商品(分類コード:160)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinKoujiHkSaihakkou = 45
        ''' <summary>
        ''' 商品(分類コード:170)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinHosyousyoSaihakkou = 46
        ''' <summary>
        ''' 商品(分類コード:180)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinKaiyakuHaraiModosi = 47
        ''' <summary>
        ''' 商品2(分類コード:110,115)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2Group = 48
        ''' <summary>
        ''' 調査会社
        ''' </summary>
        ''' <remarks></remarks>
        TyousaKaisya = 55
    End Enum
#End Region

    ''' <summary>
    ''' ドロップダウンリストの設定を行います<br/>
    ''' HtmlSelect用
    ''' </summary>
    ''' <param name="dropdown">設定するﾄﾞﾛｯﾌﾟﾀﾞｳﾝﾘｽﾄ</param>
    ''' <param name="type">設定するデータ種類</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >テキストにValue値＋"："をつける場合:true</param>
    ''' <param name="selected" >初期選択値をインデックスで指定する(withSpaceRow=Trueの場合など指定に注意)</param>
    ''' <returns>ドロップダウンリストのインスタンス</returns>
    ''' <remarks></remarks>
    Public Function SetDropDownList(ByRef dropdown As HtmlSelect, _
                                    ByVal type As DropDownType, _
                                    Optional ByVal withSpaceRow As Boolean = True, _
                                    Optional ByVal withCode As Boolean = True, _
                                    Optional ByVal selected As Integer = 0, _
                                    Optional ByVal blnTorikesi As Boolean = True _
                                    )

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetComboList(type, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

    ''' <summary>
    ''' ドロップダウンリストの設定を行います）<br/>
    ''' DropDownList用
    ''' </summary>
    ''' <param name="dropdown">設定するﾄﾞﾛｯﾌﾟﾀﾞｳﾝﾘｽﾄ</param>
    ''' <param name="type">設定するデータ種類</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >テキストにValue値＋"："をつける場合:true</param>
    ''' <param name="selected" >初期選択値をインデックスで指定する(withSpaceRow=Trueの場合など指定に注意)</param>
    ''' <returns>ドロップダウンリストのインスタンス</returns>
    ''' <remarks></remarks>
    Public Function SetDropDownList(ByRef dropdown As DropDownList, _
                                    ByVal type As DropDownType, _
                                    Optional ByVal withSpaceRow As Boolean = True, _
                                    Optional ByVal withCode As Boolean = True, _
                                    Optional ByVal selected As Integer = 0, _
                                    Optional ByVal blnTorikesi As Boolean = True _
                                    )

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetComboList(type, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

    ''' <summary>
    ''' ドロップダウンリストの設定を行います(※名称Mの名称種別専用)<br/>
    ''' DropDownList用
    ''' </summary>
    ''' <param name="dropdown">設定するﾄﾞﾛｯﾌﾟﾀﾞｳﾝﾘｽﾄ</param>
    ''' <param name="type">設定するデータ種類※EarthConstの名称タイプを指定する</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >テキストにValue値＋"："をつける場合:true</param>
    ''' <param name="selected" >初期選択値をインデックスで指定する(withSpaceRow=Trueの場合など指定に注意)</param>
    ''' <returns>ドロップダウンリストのインスタンス</returns>
    ''' <remarks></remarks>
    Public Function SetMeisyouDropDownList(ByRef dropdown As DropDownList, _
                                    ByVal type As EarthConst.emMeisyouType, _
                                    Optional ByVal withSpaceRow As Boolean = True, _
                                    Optional ByVal withCode As Boolean = True, _
                                    Optional ByVal selected As Integer = 0, _
                                    Optional ByVal blnTorikesi As Boolean = True _
                                    )

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetMeisyouDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetMeisyouComboList(type, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

    ''' <summary>
    ''' ドロップダウンリストの設定を行います(※拡張名称M専用)<br/>
    ''' DropDownList用
    ''' </summary>
    ''' <param name="dropdown">設定するﾄﾞﾛｯﾌﾟﾀﾞｳﾝﾘｽﾄ</param>
    ''' <param name="type">設定するデータ種類※EarthConstの名称タイプを指定する</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >テキストにValue値＋"："をつける場合:true</param>
    ''' <param name="selected" >初期選択値をインデックスで指定する(withSpaceRow=Trueの場合など指定に注意)</param>
    ''' <returns>ドロップダウンリストのインスタンス</returns>
    ''' <remarks></remarks>
    Public Function SetKtMeisyouDropDownList(ByRef dropdown As DropDownList, _
                                    ByVal type As EarthConst.emKtMeisyouType, _
                                    Optional ByVal withSpaceRow As Boolean = True, _
                                    Optional ByVal withCode As Boolean = True, _
                                    Optional ByVal selected As Integer = 0, _
                                    Optional ByVal blnTorikesi As Boolean = True _
                                    )

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetKtMeisyouDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetKtMeisyouComboList(type, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

    ''' <summary>
    ''' ドロップダウンリストの設定を行います(※拡張名称M専用、表示項目をパラメータで指定)<br/>
    ''' DropDownList用
    ''' </summary>
    ''' <param name="dropdown">設定するﾄﾞﾛｯﾌﾟﾀﾞｳﾝﾘｽﾄ</param>
    ''' <param name="type">設定するデータ種類※EarthConstの名称タイプを指定する</param>
    ''' <param name="ktMeisyouType">拡張名称Mドロップダウンタイプ</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >テキストにValue値＋"："をつける場合:true</param>
    ''' <param name="selected" >初期選択値をインデックスで指定する(withSpaceRow=Trueの場合など指定に注意)</param>
    ''' <returns>ドロップダウンリストのインスタンス</returns>
    ''' <remarks></remarks>
    Public Function SetKtMeisyouHannyouDropDownList(ByRef dropdown As DropDownList, _
                                                    ByVal type As EarthConst.emKtMeisyouType, _
                                                    ByVal ktMeisyouType As EarthEnum.emKtMeisyouType, _
                                                    Optional ByVal withSpaceRow As Boolean = True, _
                                                    Optional ByVal withCode As Boolean = True, _
                                                    Optional ByVal selected As Integer = 0, _
                                                    Optional ByVal blnTorikesi As Boolean = True _
                                                    )
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetKtMeisyouDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    ktMeisyouType, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected, _
                                                    blnTorikesi)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetKtMeisyouHannyouComboList(type, ktMeisyouType, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

End Class
