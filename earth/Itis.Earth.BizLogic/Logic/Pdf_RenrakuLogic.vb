Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class Pdf_RenrakuLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 地盤予定連絡書用データを取得します
    ''' </summary>
    ''' <param name="kbn">PDFKeyレコード</param>
    ''' <param name="hosyouno">PDFKeyレコード</param>
    ''' <param name="accountno">PDFKeyレコード</param>
    ''' <returns>地盤予定連絡書用レコードのDataSet</returns>
    ''' <remarks></remarks>
    Public Function GetrenrakuSearchData(ByVal kbn As String, _
                                         ByVal hosyouno As String, _
                                         ByVal accountno As Integer) As DataSet

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetrenrakuSearchData", _
                                            kbn, _
                                            hosyouno, _
                                            accountno)

        ' 検索実行クラス
        Dim dataAccess As New PdfRenrakuDataAccess

        ' PDFデータの取得
        Dim ds As DataSet = dataAccess.GetPDFData(kbn, hosyouno, accountno)

        Return ds

    End Function

End Class
