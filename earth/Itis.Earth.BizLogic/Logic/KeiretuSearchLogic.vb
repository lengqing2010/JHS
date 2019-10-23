
''' <summary>
''' 加盟店検索用ロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class KeiretuSearchLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 加盟店マスタを検索します
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKeiretuCd">系列ｺｰﾄﾞ</param>
    ''' <param name="strKeiretuNm">系列名</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKeiretuSearchResult(ByVal strKubun As String, _
                                      ByVal strKeiretuCd As String, _
                                      ByVal strKeiretuNm As String, _
                                      ByVal blnDelete As Boolean) As List(Of KeiretuSearchRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKeiretuSearchResult", _
                                            strKubun, _
                                            strKeiretuCd, _
                                            strKeiretuNm, _
                                            blnDelete)

        Dim dataAccess As KeiretuSearchDataAccess = New KeiretuSearchDataAccess

        Dim arrRtnData As List(Of KeiretuSearchRecord) = _
        DataMappingHelper.Instance.getMapArray(Of KeiretuSearchRecord)(GetType(KeiretuSearchRecord), _
        dataAccess.getKeiretuKensakuData(strKubun, strKeiretuCd, strKeiretuNm, blnDelete))

        Return arrRtnData
    End Function

End Class
