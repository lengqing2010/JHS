Imports Itis.Earth.DataAccess
Public Class CommonSearchLogic
    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private CommonSearchDA As New CommonSearchDataAccess
    ''' <summary>郵便番号データを取得する</summary>
    Public Function SelYuubinInfo(ByVal intRows As String, _
                                        ByVal strYuubinNo As String, _
                                        ByVal strYuubinMei As String) As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelYuubinInfo(intRows, strYuubinNo, strYuubinMei)
    End Function
    '''<summary>郵便番号レコード行数を取得する</summary>
    Public Function SelYuubinCount(ByVal strYuubinNo As String, _
                                            ByVal strYuubinMei As String) As Integer
        Return CommonSearchDA.SelYuubinCount(strYuubinNo, strYuubinMei)
    End Function
    '''<summary>ユーザーデータを取得する</summary>
    Public Function GetUserInfo(ByVal intRows As String, _
                                            ByVal strUserId As String, _
                                            ByVal strUserMei As String, _
                                            ByVal blnDelete As Boolean) As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelUserInfo(intRows, strUserId, strUserMei, blnDelete)
    End Function
    '''<summary>営業データを取得する</summary>
    Public Function GetEigyouInfo(ByVal strUserId As String) As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelEigyouInfo(strUserId)
    End Function
    '''<summary>ユーザーレコード行数を取得する</summary>
    Public Function GetUserCount(ByVal strUserId As String, _
                                            ByVal strUserMei As String, _
                                            ByVal blnDelete As Boolean) As Integer
        Return CommonSearchDA.SelUserCount(strUserId, strUserMei, blnDelete)
    End Function

    '''<summary>系列データを取得する</summary>
    Public Function GetKojKaisyaKensakuInfo(ByVal intRows As String, _
                                    ByVal strCd As String, _
                                    ByVal strMei As String, _
                                    ByVal blnDelete As Boolean) As DataTable
        Return CommonSearchDA.SelKojKaisyaKensakuInfo(intRows, _
                                      strCd, _
                                      strMei, _
                                      blnDelete)

    End Function
    '''<summary>営業所レコード行数を取得する</summary>
    Public Function GetKojKaisyaKensakuCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal blnDelete As Boolean) As Long
        Return CommonSearchDA.SelKojKaisyaKensakuCount(strCd, strMei, blnDelete)
    End Function
    '''<summary>営業所データを取得する</summary>
    Public Function GetEigyousyoInfo(ByVal intRows As String, _
                                    ByVal strEigyousyoCd As String, _
                                    ByVal strEigyousyoMei As String, _
                                    ByVal blnDelete As Boolean) As CommonSearchDataSet.EigyousyoTableDataTable
        Return CommonSearchDA.SelEigyousyoInfo(intRows, strEigyousyoCd, strEigyousyoMei, blnDelete)
    End Function
    '''<summary>営業所レコード行数を取得する</summary>
    Public Function GetEigyousyoCount(ByVal strEigyousyoCd As String, _
                                        ByVal strEigyousyoMei As String, _
                                        ByVal blnDelete As Boolean) As Long
        Return CommonSearchDA.SelEigyousyoCount(strEigyousyoCd, strEigyousyoMei, blnDelete)
    End Function
    '''<summary>加盟店データを取得する</summary>
    Public Function GetKameitenKensakuInfo(ByVal intRows As String, _
                                        ByVal strKubun As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strKameitenKana As String, _
                                        ByVal blnDelete As Boolean) As CommonSearchDataSet.KameitenSearchTableDataTable
        Return CommonSearchDA.SelKameitenKensakuInfo(intRows, _
                                      strKubun, _
                                      strKameitenCd, _
                                      strKameitenKana, _
                                      blnDelete)

    End Function
    '''<summary>加盟店レコード行数を取得する</summary>
    Public Function GetKameitenKensakuCount(ByVal strKubun As String, _
                                      ByVal strKameitenCd As String, _
                                      ByVal strKameitenKana As String, _
                                      ByVal blnDelete As Boolean) As Integer
        Return CommonSearchDA.SelKameitenKensakuCount(strKubun, _
                                      strKameitenCd, _
                                      strKameitenKana, _
                                      blnDelete)
    End Function



    '''<summary>系列データを取得する</summary>
    Public Function GetKeiretuKensakuInfo(ByVal intRows As String, _
                                      ByVal strKubun As String, _
                                      ByVal strKeiretuCd As String, _
                                      ByVal strKeiretuMei As String, _
                                      ByVal blnDelete As Boolean) As CommonSearchDataSet.KeiretuTableDataTable
        Return CommonSearchDA.SelKeiretuKensakuInfo(intRows, _
                                      strKubun, _
                                      strKeiretuCd, _
                                      strKeiretuMei, _
                                      blnDelete)

    End Function
    '''<summary>系列レコード行数を取得する</summary>
    Public Function GetKeiretuKensakuCount(ByVal strKubun As String, _
                                      ByVal strKeiretuCd As String, _
                                      ByVal strKeiretuMei As String, _
                                      ByVal blnDelete As Boolean) As Integer
        Return CommonSearchDA.SelKeiretuKensakuCount(strKubun, _
                                      strKeiretuCd, _
                                      strKeiretuMei, _
                                      blnDelete)
    End Function
    '''<summary>商品データを取得する</summary>
    Public Function GetSyouhinInfo(ByVal intRows As String, _
                                    ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinMei As String, _
                                     ByVal soukoCd As String, _
                                    Optional ByVal blnDelete As String = "") As CommonSearchDataSet.SyouhinTableDataTable
        Return CommonSearchDA.SelSyouhinInfo(intRows, strSyouhinCd, strSyouhinMei, soukoCd, blnDelete)
    End Function
    '''<summary>商品レコード行数を取得する</summary>
    Public Function GetSyouhinCount(ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinMei As String, _
                                    ByVal soukoCd As String, _
                                    Optional ByVal blnDelete As String = "") As Long
        Return CommonSearchDA.SelSyouhinCount(strSyouhinCd, strSyouhinMei, soukoCd, blnDelete)
    End Function

    '==================2011/05/11 車龍 多棟割引情報表示変更 追加 開始↓==========================
    '''<summary>商品+標準化価格データを取得する</summary>
    Public Function GetSyouhinKakakuInfo(ByVal intRows As String, _
                                    ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinMei As String, _
                                     ByVal soukoCd As String, _
                                    Optional ByVal blnDelete As String = "") As Data.DataTable
        Return CommonSearchDA.SelSyouhinKakakuInfo(intRows, strSyouhinCd, strSyouhinMei, soukoCd, blnDelete)
    End Function
    '==================2011/05/11 車龍 多棟割引情報表示変更 追加 終了↑==========================

    '''<summary>ビルダーデータを取得する</summary>

    Public Function GetBirudaInfo(ByVal intRows As String, _
                                    ByVal strBirudaCd As String, _
                                    ByVal strBirudaMei As String, _
                                      ByVal blnDelete As Boolean) As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelBirudaInfo(intRows, strBirudaCd, strBirudaMei, blnDelete)
    End Function
    '''<summary>ビルダーレコード行数を取得する</summary>
    Public Function GetBirudaCount(ByVal strSyouhinCd As String, _
                                      ByVal strSyouhinMei As String, _
                                      ByVal blnDelete As Boolean) As Long
        Return CommonSearchDA.SelBirudaCount(strSyouhinCd, strSyouhinMei, blnDelete)
    End Function
    '''<summary>権限データを取得する</summary>
    Public Function GetKengen(ByVal strAccountNo As String) As CommonSearchDataSet.AccountTableDataTable
        Return CommonSearchDA.SelKengen(strAccountNo)
    End Function
    '''<summary>参照履歴データを新規する</summary>
    Public Function SetInsLog(ByVal strUrl As String, ByVal strUerId As String) As Boolean
        Return CommonSearchDA.InsUrlLog(strUrl, strUerId)
    End Function
    '''<summary>共通データを取得する</summary>
    Public Function GetCommonInfo(ByVal strCd As String, _
                                         ByVal strTableName As String, Optional ByVal strKubun As String = "") As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelCommonInfo(strCd, strTableName, strKubun)
    End Function
    '''<summary>加盟店種別を取得する</summary>
    Public Function Selkameitensyubetu(ByVal intRows As String, _
                    ByVal code As String, ByVal mei As String) As CommonSearchDataSet.meisyouTableDataTable
        Return CommonSearchDA.Selkameitensyubetu(intRows, code, mei)
    End Function

    '''<summary>加盟店種別レコード行数を取得する</summary>
    Public Function SelkameitensyubetuCount(ByVal code As String, ByVal mei As String) As Integer
        Return CommonSearchDA.SelkameitensyubetuCount(code, mei)
    End Function

    '''<summary>仕様を取得する</summary>
    Public Function SelSiyouInfo(ByVal intRows As String, _
                    ByVal code As String, ByVal mei As String) As CommonSearchDataSet.IntTableDataTable
        Return CommonSearchDA.SelSiyouInfo(intRows, code, mei)
    End Function
    '''<summary>仕様レコード行数を取得する</summary>
    Public Function SelSiyouCount(ByVal code As String, ByVal mei As String) As Integer
        Return CommonSearchDA.SelSiyouCount(code, mei)
    End Function
    Public Function GetSinkaikeiSiharaiSakiInfo(ByVal intRows As String, _
                                            ByVal strCd As String, _
                                            ByVal strCd2 As String, _
                                            ByVal strMei As String, _
                                            ByVal strMei2 As String) As DataTable

        Return CommonSearchDA.SelSinkaikeiSiharaiSakiInfo(intRows, strCd, strCd2, strMei, strMei2)
    End Function
    Public Function GetSinkaikeiSiharaiSakiCount(ByVal strCd As String, _
                                         ByVal strCd2 As String, _
                                         ByVal strMei As String, _
                                         ByVal strMei2 As String) As Integer

        Return CommonSearchDA.SelSinkaikeiSiharaiSakiCount(strCd, strCd2, strMei, strMei2)
    End Function

    ''' <summary>調査会社データを取得する</summary>
    Public Function GetTyousaInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, _
                                        Optional ByVal blnDelete As Boolean = True) As CommonSearchDataSet.tyousakaisyaTableDataTable
        Return CommonSearchDA.SelTyousaInfo(intRows, strCd, strMei, strMei2, blnDelete)
    End Function

    ''' <summary>調査会社データを取得する</summary>
    Public Function GetJigyousyoInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As DataTable
        Return CommonSearchDA.SelJigyousyoInfo(intRows, strCd, strMei, strMei2)
    End Function
    ''' <summary>調査会社データを取得する</summary>
    Public Function GetJigyousyoCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As Integer
        Return CommonSearchDA.SelJigyousyoCount(strCd, strMei, strMei2)
    End Function

    ''' <summary>調査会社レコード行数を取得する</summary>
    Public Function GetTyousaCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, _
                                        Optional ByVal blnDelete As Boolean = True) As Integer
        Return CommonSearchDA.SelTyousaCount(strCd, strMei, strMei2, blnDelete)
    End Function
    ''' <summary>請求先マスタを取得する</summary>
    Public Function GetSeikyuuSakiInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, ByVal strMei3 As String, ByVal blnDelete As Boolean, Optional ByVal blnKana As Boolean = False) As CommonSearchDataSet.SeikyuuSakiTableDataTable
        Return CommonSearchDA.SelSeikyuuSakiInfo(intRows, strCd, strMei, strMei2, strMei3, blnDelete, blnKana)
    End Function
    ''' <summary>請求先マスタレコード行数を取得する</summary>
    Public Function GetSeikyuuSakiCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, ByVal strMei3 As String, ByVal blnDelete As Boolean, Optional ByVal blnKana As Boolean = False) As Integer
        Return CommonSearchDA.SelSeikyuuSakiCount(strCd, strMei, strMei2, strMei3, blnDelete, blnKana)
    End Function

    ''' <summary>名寄先データを取得する</summary>
    Public Function GetNayoseSakiInfo(ByVal intRows As String, _
                                            ByVal strCd As String, _
                                            ByVal strMei As String, _
                                            ByVal strMei2 As String) As DataTable

        Return CommonSearchDA.SelNayoseSakiInfo(intRows, strCd, strMei, strMei2)
    End Function
    ''' <summary>名寄先行数を取得する</summary>
    Public Function GetNayoseSakiCount(ByVal strCd As String, _
                                         ByVal strMei As String, _
                                         ByVal strMei2 As String) As Integer

        Return CommonSearchDA.SelNayoseSakiCount(strCd, strMei, strMei2)
    End Function

    ''' <summary>特別対応データを取得する</summary>
    Public Function GetTokubetuKaiouInfo(ByVal intRows As String, ByVal strCd As String, ByVal strMei As String _
                                            , ByVal blnDelete As Boolean) As DataTable
        Return CommonSearchDA.SelTokubetuTaiouInfo(intRows, strCd, strMei, blnDelete)
    End Function

    ''' <summary>特別対応行数を取得</summary>
    Public Function GetTokubetuKaiouCount(ByVal strCd As String, ByVal strMei As String, ByVal blnDelete As Boolean) As Integer
        Return CommonSearchDA.SelTokubetuTaiouCount(strCd, strMei, blnDelete)
    End Function

    ''' <summary>コード取得元のマスタ取得する</summary>
    Public Function SelSAPSiireSaki(ByVal top As Integer, ByVal a1_ktokk As String, ByVal a1_lifnr As String, ByVal a1_a_zz_sort As String, ByVal sort As String) As DataSet
        Return CommonSearchDA.SelSAPSiireSaki(top, a1_ktokk, a1_lifnr, a1_a_zz_sort, sort)
    End Function


    ''' <summary>勘定ｸﾞﾙｰﾌﾟ取得する</summary>
    Public Function SelDis_a1_ktokk() As DataTable
        Return CommonSearchDA.SelDis_a1_ktokk
    End Function

End Class
