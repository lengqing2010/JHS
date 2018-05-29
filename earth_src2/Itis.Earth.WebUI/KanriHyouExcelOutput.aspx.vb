Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data

Partial Public Class KanriHyouExcelOutput
    Inherits System.Web.UI.Page

#Region "定数"
    Private Const SEP_STRING As String = "$$$"
#End Region

    Private kensaHoukokusyoOutputLogic As New KensaHoukokusyoOutputLogic
    'ログインユーザーを取得する。
    Private ninsyou As New Ninsyou()
    Private loginID As String

    Private Const FILE_NAME As String = "Kanrihyou"
    Protected Const XltFolder As String = "C:\JHS\earth\KensaHoukokusyo\"
    Protected Const CsvFolder As String = "C:\JHS\earth\KensaHoukokusyo\download\"

    Protected XltFile As String = FILE_NAME & ".xltm"
    Protected CsvDataFile As String = FILE_NAME & ".csv"

    Protected csvData As String = ""
    Protected strErr As String = ""

    Private sb_T As New StringBuilder

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '基本認証
        If ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        Else
            loginID = ninsyou.GetUserID()
        End If

        Dim strNoAddId As String = Request.QueryString("strNoAddId").ToString

        Dim strNo As String = strNoAddId.Split(",")(0)
        Dim ID1 As String = strNoAddId.Split(",")(1)
        Dim ID2 As String = strNoAddId.Split(",")(2)

        Me.MakeJavaScript()

        If Not IsPostBack Then
            GetScriptValue(ID1, ID2)
        Else
            If (Me.CreateExcelData(strNo) = True) Then
                Dim arr() As String = sb_T.ToString.Split(vbCrLf)
                'csvData = String.Join("@@@", arr)
                For i As Integer = 0 To arr.Length - 1
                    If arr(i) <> Chr(10) Then
                        csvData = csvData & arr(i).Replace(Chr(10), "") & "@@@"
                    End If
                Next
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "CLOSE", "funBtnEnable();", True)
            End If
        End If

    End Sub


    ''' <summary>
    ''' Excel出力
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CreateExcelData(ByVal strNo As String) As Boolean

        strNo = "'" & strNo.Replace(SEP_STRING, "','") & "'"

        'データを取得する
        Dim kanrihyouDt As Data.DataTable
        kanrihyouDt = kensaHoukokusyoOutputLogic.GetKanrihyouExcelInfo(strNo)

        If kanrihyouDt.Rows.Count > 0 Then
            '最大の発送日
            Dim maxHassouDate As String = kanrihyouDt.Compute("MAX(hassou_date)", "").ToString.Replace("/", String.Empty)
            'ファイルの格納パス
            Dim strKanrihyouExcelPath As String = System.Configuration.ConfigurationManager.AppSettings("KanrihyouExcelPath").ToString
            strKanrihyouExcelPath = strKanrihyouExcelPath & IIf(Right(strKanrihyouExcelPath, 1).Equals("\"), String.Empty, "\").ToString
            Dim filePath As String
            filePath = strKanrihyouExcelPath & "山梨_建物検査報告書_" & maxHassouDate & Now.ToString("HHmm") & "_管理表.xlsx"

            'ファイルのパス
            sb_T.AppendLine(filePath)

            'CSVデータを作成
            Dim tempList As New System.Collections.Generic.List(Of String)
            For i As Integer = 0 To kanrihyouDt.Rows.Count - 1
                tempList.Clear()

                tempList.Add((i + 1).ToString)
                For j As Integer = 0 To kanrihyouDt.Columns.Count - 1
                    With kanrihyouDt.Rows(i)
                        tempList.Add(.Item(j).ToString)
                    End With
                Next

                sb_T.AppendLine(String.Join(",", tempList.ToArray()))
            Next

        End If

        Return True

    End Function



    ''' <summary>
    '''　前画面データ格納
    ''' </summary>
    ''' <param name="strId1"></param>
    ''' <param name="strId2"></param>
    ''' <remarks></remarks>
    Protected Sub GetScriptValue(ByVal strId1 As String, ByVal strId2 As String)

        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("   document.getElementById('" & Me.hidSelName.ClientID & "').value='" & strId1 & "';" & vbCrLf)
            .Append("   document.getElementById('" & Me.hidDisableDiv.ClientID & "').value='" & strId2 & "';" & vbCrLf)
            .Append("   form1.submit();" & vbCrLf)
            .Append("</script>" & vbCrLf)
        End With
        ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)

    End Sub

    Protected Sub MakeJavaScript()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("function funOutError(strERR)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if (strERR==''){" & vbCrLf)
            .Append("   alert('指定した条件でデータはありません。');}else{" & vbCrLf)
            .Append("   alert(strERR);" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

End Class

