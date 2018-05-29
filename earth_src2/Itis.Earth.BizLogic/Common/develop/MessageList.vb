Imports System.Text
Imports System.IO

Public Class MessageList

    ''' <summary>
    ''' メッセージ管理クラスに存在するメッセージの一覧をTSVファイルとして<br/>
    ''' 出力します<br/>
    ''' MSG001E\tメッセージ１です\r\n<br/>
    ''' MSG002E\tメッセージ２です\r\n<br/>
    ''' </summary>
    ''' <param name="output_path">出力先のパス</param>
    ''' <remarks></remarks>
    Public Sub getTsvFile(ByVal output_path As String)

        ' メッセージ管理クラスのフィールド情報
        Dim field_info As System.Reflection.FieldInfo

        Dim out_data As New StringBuilder

        For Each field_info In GetType(Messages).GetFields

            ' Stringのプロパティはメッセージ定数のみなのでStringで抽出
            If field_info.FieldType.FullName = GetType(String).ToString() Then

                ' メッセージ文言格納用
                Dim value As New Object
                ' メッセージを取得
                value = field_info.GetValue(Messages.Instance)

                out_data.Append(field_info.Name & vbTab & value & vbCrLf)

            End If

        Next

        ' データがあればファイル出力する
        If out_data.ToString().Trim() <> "" Then
            Try
                Using sw As StreamWriter = New StreamWriter(output_path)
                    sw.Write(out_data.ToString())
                    sw.Close()
                End Using
            Catch ex As Exception
                Debug.WriteLine("出力失敗")
            End Try
        End If

    End Sub

End Class
