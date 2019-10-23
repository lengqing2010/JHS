Imports System.Data
Imports System.Data.SqlClient

Public Class MototyouLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
#Region "請求先元帳"

#Region "請求先元帳_伝票データ取得"
    ''' <summary>
    ''' 請求先元帳_伝票データ取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUrikakeDataNewest(ByVal seikyuuSakiCd As String, _
                                         ByVal seikyuuSakiBrc As String, _
                                         ByVal seikyuuSakiKbn As String, _
                                         Optional ByVal fromDate As Date = Nothing _
                                         ) As UrikakeDataRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiMototyouDenpyouData", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate _
                                                    )
        If fromDate = Nothing Then
            fromDate = EarthConst.Instance.MIN_DATE
        End If

        Dim dataAccess As New MototyouDataAccess
        Dim dtResult As DataTable
        Dim drList As List(Of UrikakeDataRecord)

        'データ取得
        dtResult = dataAccess.urikakeDataNewest(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate)

        'Listに格納
        drList = DataMappingHelper.Instance.getMapArray(Of UrikakeDataRecord)(GetType(UrikakeDataRecord), dtResult)

        If drList.Count <= 0 Then
            Return New UrikakeDataRecord
        End If

        '値戻し
        Return drList(0)

    End Function
#End Region

#Region "請求先元帳_繰越残高取得"
    ''' <summary>
    ''' 請求先元帳_繰越残高取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <returns>繰越残高(Long型)</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSakiMototyouKurikosiZan(ByVal seikyuuSakiCd As String, _
                                                      ByVal seikyuuSakiBrc As String, _
                                                      ByVal seikyuuSakiKbn As String, _
                                                      ByVal fromDate As Date _
                                                      ) As Long

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiMototyouKurikosiZan", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate _
                                                    )

        Dim dataAccess As New MototyouDataAccess
        Dim Data As Object
        Dim retData As Long

        'データ取得
        Data = dataAccess.seikyuuSakiMototyouKurikosiZan(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate)

        ' 取得出来ない場合、空白を返却
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'Longにキャスト
                retData = CType(Data, Long)
            Catch ex As Exception
                '失敗したらゼロ
                retData = 0
            End Try
        End If

        '値戻し
        Return retData

    End Function
#End Region

#Region "請求先元帳_伝票データ取得"
    ''' <summary>
    ''' 請求先元帳_伝票データ取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <param name="toDate">年月日TO</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSakiMototyouDenpyouData(ByVal seikyuuSakiCd As String, _
                                                      ByVal seikyuuSakiBrc As String, _
                                                      ByVal seikyuuSakiKbn As String, _
                                                      ByVal fromDate As Date, _
                                                      ByVal toDate As Date _
                                                      ) As List(Of SeikyuuSakiMototyouRecord)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiMototyouDenpyouData", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate, _
                                                    toDate _
                                                    )
        Dim dataAccess As New MototyouDataAccess
        Dim dtResult As DataTable
        Dim drList As List(Of SeikyuuSakiMototyouRecord)

        'データ取得
        dtResult = dataAccess.seikyuuSakiMototyouDenpyouData(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate, toDate)

        'Listに格納
        drList = DataMappingHelper.Instance.getMapArray(Of SeikyuuSakiMototyouRecord)(GetType(SeikyuuSakiMototyouRecord), dtResult)

        '値戻し
        Return drList

    End Function
#End Region

#End Region

#Region "支払先元帳"

#Region "支払先元帳_伝票データ取得"
    ''' <summary>
    ''' 支払先元帳_伝票データ取得
    ''' </summary>
    ''' <param name="tysKaisyaCd">調査会社コード</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetKaikakeDataNewest(ByVal tysKaisyaCd As String, _
                                         Optional ByVal fromDate As Date = Nothing _
                                         ) As KaikakeDataRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiSakiMototyouDenpyouData", _
                                                    tysKaisyaCd, _
                                                    fromDate _
                                                    )
        If fromDate = Nothing Then
            fromDate = EarthConst.Instance.MIN_DATE
        End If

        Dim dataAccess As New MototyouDataAccess
        Dim dtResult As DataTable
        Dim drList As List(Of KaikakeDataRecord)

        'データ取得
        dtResult = dataAccess.kaikakeDataNewest(tysKaisyaCd, fromDate)

        'Listに格納
        drList = DataMappingHelper.Instance.getMapArray(Of KaikakeDataRecord)(GetType(KaikakeDataRecord), dtResult)

        If drList.Count <= 0 Then
            Return New KaikakeDataRecord
        End If

        '値戻し
        Return drList(0)

    End Function
#End Region

#Region "支払先元帳_繰越残高取得"
    ''' <summary>
    ''' 支払先元帳_繰越残高取得
    ''' </summary>
    ''' <param name="tysKaisyaCd">調査会社コード</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <returns>繰越残高(Long型)</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiSakiMototyouKurikosiZan(ByVal tysKaisyaCd As String, _
                                                      ByVal fromDate As Date _
                                                      ) As Long

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiSakiMototyouKurikosiZan", _
                                                    tysKaisyaCd, _
                                                    fromDate _
                                                    )

        Dim dataAccess As New MototyouDataAccess
        Dim Data As Object
        Dim retData As Long

        'データ取得
        Data = dataAccess.siharaiSakiMototyouKurikosiZan(tysKaisyaCd, fromDate)

        ' 取得出来ない場合、空白を返却
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'Longにキャスト
                retData = CType(Data, Long)
            Catch ex As Exception
                '失敗したらゼロ
                retData = 0
            End Try
        End If

        '値戻し
        Return retData

    End Function
#End Region

#Region "支払先元帳_伝票データ取得"
    ''' <summary>
    ''' 支払先元帳_伝票データ取得
    ''' </summary>
    ''' <param name="tysKaisyaCd">調査会社コード</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <param name="toDate">年月日TO</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiSakiMototyouDenpyouData(ByVal tysKaisyaCd As String, _
                                                      ByVal fromDate As Date, _
                                                      ByVal toDate As Date _
                                                      ) As List(Of SiharaiSakiMototyouRecord)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiSakiMototyouDenpyouData", _
                                                    tysKaisyaCd, _
                                                    fromDate, _
                                                    toDate _
                                                    )
        Dim dataAccess As New MototyouDataAccess
        Dim dtResult As DataTable
        Dim drList As List(Of SiharaiSakiMototyouRecord)

        'データ取得
        dtResult = dataAccess.siharaiSakiMototyouDenpyouData(tysKaisyaCd, fromDate, toDate)

        'Listに格納
        drList = DataMappingHelper.Instance.getMapArray(Of SiharaiSakiMototyouRecord)(GetType(SiharaiSakiMototyouRecord), dtResult)

        '値戻し
        Return drList

    End Function
#End Region

#End Region

End Class
