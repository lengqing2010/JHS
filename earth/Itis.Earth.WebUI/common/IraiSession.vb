
''' <summary>
''' 依頼登録セッションデータクラス
''' </summary>
''' <remarks></remarks>
Public Class IraiSession

#Region "依頼1データ"
    ''' <summary>
    ''' 依頼1データ
    ''' </summary>
    ''' <remarks></remarks>
    Private htIrai1Data As Hashtable
    ''' <summary>
    ''' 依頼1データ
    ''' </summary>
    ''' <value></value>
    ''' <returns>依頼1データ</returns>
    ''' <remarks></remarks>
    Public Property Irai1Data() As Hashtable
        Get
            Return htIrai1Data
        End Get
        Set(ByVal value As Hashtable)
            htIrai1Data = value
        End Set
    End Property
#End Region

#Region "依頼2データ"
    ''' <summary>
    ''' 依頼2データ
    ''' </summary>
    ''' <remarks></remarks>
    Private htIrai2Data As Hashtable
    ''' <summary>
    ''' 依頼2データ
    ''' </summary>
    ''' <value></value>
    ''' <returns>依頼2データ</returns>
    ''' <remarks></remarks>
    Public Property Irai2Data() As Hashtable
        Get
            Return htIrai2Data
        End Get
        Set(ByVal value As Hashtable)
            htIrai2Data = value
        End Set
    End Property
#End Region

#Region "ドロップダウンリストデータ"
    ''' <summary>
    ''' DDLデータ(商品1用)
    ''' </summary>
    ''' <remarks></remarks>
    Private htDdlDataSyouhin1 As Hashtable
    ''' <summary>
    ''' DDLデータ(商品1用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DdlDataSyouhin1() As Hashtable
        Get
            Return htDdlDataSyouhin1
        End Get
        Set(ByVal value As Hashtable)
            htDdlDataSyouhin1 = value
        End Set
    End Property

    ''' <summary>
    ''' DDLデータ(調査方法用)
    ''' </summary>
    ''' <remarks></remarks>
    Private htDdlDataTysHouhou As Hashtable
    ''' <summary>
    ''' DDLデータ(調査方法用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DdlDataTysHouhou() As Hashtable
        Get
            Return htDdlDataTysHouhou
        End Get
        Set(ByVal value As Hashtable)
            htDdlDataTysHouhou = value
        End Set
    End Property

#End Region

#Region "依頼1処理モード"
    ''' <summary>
    ''' 依頼1処理モード
    ''' </summary>
    ''' <remarks></remarks>
    Private strIrai1Mode As String
    ''' <summary>
    ''' 依頼1処理モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>依頼1処理モード</returns>
    ''' <remarks></remarks>
    Public Property Irai1Mode() As String
        Get
            Return strIrai1Mode
        End Get
        Set(ByVal value As String)
            strIrai1Mode = value
        End Set
    End Property
#End Region

#Region "依頼2処理モード"
    ''' <summary>
    ''' 依頼2処理モード
    ''' </summary>
    ''' <remarks></remarks>
    Private strIrai2Mode As String
    ''' <summary>
    ''' 依頼2処理モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>依頼2処理モード</returns>
    ''' <remarks></remarks>
    Public Property Irai2Mode() As String
        Get
            Return strIrai2Mode
        End Get
        Set(ByVal value As String)
            strIrai2Mode = value
        End Set
    End Property
#End Region

#Region "確認画面処理実行モード"
    ''' <summary>
    ''' 確認画面処理実行モード"
    ''' </summary>
    ''' <remarks></remarks>
    Private strExeMode As String
    ''' <summary>
    ''' 確認画面処理実行モード"
    ''' </summary>
    ''' <value></value>
    ''' <returns>確認画面処理モード"</returns>
    ''' <remarks></remarks>
    Public Property ExeMode() As String
        Get
            Return strExeMode
        End Get
        Set(ByVal value As String)
            strExeMode = value
        End Set
    End Property
#End Region

#Region "地盤データ"
    ''' <summary>
    ''' 地盤データ
    ''' </summary>
    ''' <remarks></remarks>
    Private jrJibanData As JibanRecordBase
    ''' <summary>
    ''' 地盤データ
    ''' </summary>
    ''' <value></value>
    ''' <returns>地盤データ</returns>
    ''' <remarks></remarks>
    Public Property JibanData() As JibanRecordBase
        Get
            Return jrJibanData
        End Get
        Set(ByVal value As JibanRecordBase)
            jrJibanData = value
        End Set
    End Property
#End Region

#Region "ログインユーザ情報"
    ''' <summary>
    ''' ログインユーザ情報
    ''' </summary>
    ''' <remarks></remarks>
    Private luiUserInfo As LoginUserInfo
    ''' <summary>
    ''' ログインユーザ情報
    ''' </summary>
    ''' <value></value>
    ''' <returns>ログインユーザ情報</returns>
    ''' <remarks></remarks>
    Public Property UserInfo() As LoginUserInfo
        Get
            Return luiUserInfo
        End Get
        Set(ByVal value As LoginUserInfo)
            luiUserInfo = value
        End Set
    End Property
#End Region


#Region "依頼ステータス"
    ''' <summary>
    ''' 依頼ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strIraiST As String
    ''' <summary>
    ''' 依頼ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns>依頼ステータス</returns>
    ''' <remarks></remarks>
    Public Property IraiST() As String
        Get
            Return strIraiST
        End Get
        Set(ByVal value As String)
            strIraiST = value
        End Set
    End Property
#End Region

#Region "依頼1データ文字列"
    ''' <summary>
    ''' 依頼1データ文字列
    ''' </summary>
    ''' <remarks></remarks>
    Private strIrai1DataStr As String
    ''' <summary>
    ''' 依頼1データ文字列
    ''' </summary>
    ''' <value></value>
    ''' <returns>依頼1データ文字列</returns>
    ''' <remarks></remarks>
    Public Property Irai1DataStr() As String
        Get
            Return strIrai1DataStr
        End Get
        Set(ByVal value As String)
            strIrai1DataStr = value
        End Set
    End Property
#End Region

#Region "依頼2データ文字列"
    ''' <summary>
    ''' 依頼2データ文字列
    ''' </summary>
    ''' <remarks></remarks>
    Private strIrai2DataStr As String
    ''' <summary>
    ''' 依頼2データ文字列
    ''' </summary>
    ''' <value></value>
    ''' <returns>依頼2データ文字列</returns>
    ''' <remarks></remarks>
    Public Property Irai2DataStr() As String
        Get
            Return strIrai2DataStr
        End Get
        Set(ByVal value As String)
            strIrai2DataStr = value
        End Set
    End Property
#End Region

#Region "ドロップダウンリスト文字列"
    Private strDdlDataStr As String
    Public Property DdlDataStr() As String
        Get
            Return strDdlDataStr
        End Get
        Set(ByVal value As String)
            strDdlDataStr = value
        End Set
    End Property
#End Region

#Region "請求先仕入先リンク"
    ''' <summary>
    ''' 請求先仕入先リンク
    ''' </summary>
    ''' <remarks></remarks>
    Private SeikyuuSiireLinkCtrl As SeikyuuSiireLinkCtrl
    ''' <summary>
    ''' 請求先仕入先リンク
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先仕入先リンク</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSiireLink() As SeikyuuSiireLinkCtrl
        Get
            Return SeikyuuSiireLinkCtrl
        End Get
        Set(ByVal value As SeikyuuSiireLinkCtrl)
            SeikyuuSiireLinkCtrl = value
        End Set
    End Property
#End Region

#Region "共通情報コピー用区分"
    ''' <summary>
    ''' 共通情報コピー用区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strCopyKbn As String
    ''' <summary>
    ''' 共通情報コピー用区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>共通情報コピー用区分</returns>
    ''' <remarks></remarks>
    Public Property CopyKbn() As String
        Get
            Return strCopyKbn
        End Get
        Set(ByVal value As String)
            strCopyKbn = value
        End Set
    End Property
#End Region

#Region "特別対応価格反映処理フラグ"
    Private strHiddenTokutaiKkkHaneiFlg As String = String.Empty
    Public Property HiddenTokutaiKkkHaneiFlg() As String
        Get
            Return strHiddenTokutaiKkkHaneiFlg
        End Get
        Set(ByVal value As String)
            strHiddenTokutaiKkkHaneiFlg = value
        End Set
    End Property
#End Region

End Class
