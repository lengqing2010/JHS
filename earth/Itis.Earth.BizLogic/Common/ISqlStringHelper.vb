Imports System.Reflection

Public Interface ISqlStringHelper
    ''' <summary>
    ''' レコードクラスのプロパティ情報よりSQL文を生成する
    ''' </summary>
    ''' <param name="recordType">設定対象のレコードタイプ</param>
    ''' <param name="row">更新データレコード</param>
    ''' <param name="paramList">パラメータレコードのリスト</param>
    ''' <param name="recordTypeRow">更新データレコードタイプ</param>
    ''' <returns>更新SQL文</returns>
    ''' <remarks></remarks>
    Function MakeUpdateInfo(ByVal recordType As Type, _
                            ByVal row As Object, _
                            ByRef paramList As List(Of ParamRecord), _
                            Optional ByVal recordTypeRow As Type = Nothing) As String
End Interface
