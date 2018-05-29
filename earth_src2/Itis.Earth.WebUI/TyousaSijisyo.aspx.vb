Imports Itis.Earth.BizLogic
Imports Itis.Framework.Report

Partial Public Class TyousaSijisyo
    Inherits System.Web.UI.Page
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "TyousaSijisyo"
    Private Const APOST As Char = ","c

    Public url1 As String = ""
    Public url2 As String = ""



    ''' <summary>
    ''' PageLoad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim paramKbn As String = Request.QueryString("Kbn")

            Dim paramBukkenNo As String = request.QueryString("HosyouSyoNo")

            ViewState("kbn") = paramKbn
            ViewState("bukkenNo") = paramBukkenNo

            Dim TyousaSijisyoLogic As New BizLogic.TyousaSijisyoLogic

            Dim dt As DataTable = TyousaSijisyoLogic.GetTyousaSijisyo(paramKbn, paramBukkenNo)

            Dim printDat As New System.Text.StringBuilder

            Dim dat As New System.Text.StringBuilder

            If dt.Rows.Count > 0 Then

                fcw = New FcwUtility(Page, "99999999", kinouId, ".fcp")

                With dt.Rows(0)
                    'dat.AppendLine("[Control Section]")
                    'dat.AppendLine("VERSION=6.3")
                    'dat.AppendLine("SEPARATOR=|")
                    'dat.AppendLine("AUTOPAGEBREAK=ON")
                    'dat.AppendLine("OPTION=FIELDATTR")
                    'dat.AppendLine(";")

                    printDat.Append(fcw.CreateDatHeader(APOST.ToString))

                    printDat.Append(fcw.CreateFormSection(""))

                    'dat.AppendLine("[Form Section]")
                    'dat.AppendLine("[Fixed Data Section]")
                    dat.AppendLine("�������=" & .Item("������Ж�").ToString)
                    dat.AppendLine("�敪=" & .Item("�敪").ToString)
                    dat.AppendLine("�ۏ؏�No=" & .Item("�����ԍ�").ToString)

                    If .Item("JIO��").ToString = "1" Then
                        dat.AppendLine("JIO��=JIO��")

                    End If

                    dat.AppendLine("�����X��=" & .Item("�����X��").ToString)
                    dat.AppendLine("��������=" & .Item("��������").ToString)
                    dat.AppendLine("�����Z��=" & .Item("�����Z��").ToString)
                    dat.AppendLine("���i=" & .Item("���i").ToString)
                    dat.AppendLine("�������@=" & .Item("�������@").ToString)

                    '�d�l�ύX�@2017/03/06 �A���@�ύX��

                    Dim Str As String = .Item("�I�v�V�����������@").ToString()
                    Dim StrCount As Integer = 0
                    StrCount = System.Text.Encoding.Default.GetBytes(Str).Length

                    If StrCount > 136 Then
                        dat.AppendLine("�I�v�V�����������@_11=" & .Item("�I�v�V�����������@").ToString)
                    Else
                        dat.AppendLine("�I�v�V�����������@_14=" & .Item("�I�v�V�����������@").ToString)
                    End If

                    ' �d�l�ύX�@2017/03/06 �A���@�ύX��

                    dat.AppendLine("�˗�����=" & .Item("�˗�����").ToString & " ��")
                    'If .Item("�\���").ToString.Trim = "1" Then
                    '    dat.AppendLine("�\���=���������ς�")
                    'Else
                    '    dat.AppendLine("�\���=���������˗�")
                    'End If

                    dat.AppendLine("�\���=" & .Item("�\���").ToString.Trim)


                    '�d�l�ύX�@2017/03/06 �A���@�ύX��

                    'If .Item("����L��").ToString = "1" Then
                    '    dat.AppendLine("����L��=�L��")
                    'ElseIf .Item("����L��").ToString = "0" Then
                    '    dat.AppendLine("����L��=���ݒ�")
                    'Else
                    '    dat.AppendLine("����L��=����")
                    'End If

                    If .Item("����L��").ToString = "1" Then

                        If .Item("����҃R�[�h").ToString = "0" Then
                            dat.AppendLine("����L��=�L")
                        ElseIf .Item("����҃R�[�h").ToString = "1" Then
                            dat.AppendLine("����L��=�L�i�{��l�j")
                        ElseIf .Item("����҃R�[�h").ToString = "2" Then
                            dat.AppendLine("����L��=�L�i�S���ҁj")
                        ElseIf .Item("����҃R�[�h").ToString = "3" Then
                            dat.AppendLine("����L��=�L�i�{��l�A�S���ҁj")
                        ElseIf .Item("����҃R�[�h").ToString = "4" Then
                            If .Item("����L��_���̑����l") IsNot DBNull.Value AndAlso .Item("����L��_���̑����l").ToString.Trim <> "" Then
                                dat.AppendLine("����L��=�L�i���̑��y" & .Item("����L��_���̑����l").ToString & "�z�j")
                            Else
                                dat.AppendLine("����L��=�L�i���̑��j")
                            End If
                        ElseIf .Item("����҃R�[�h").ToString = "5" Then
                            If .Item("����L��_���̑����l") IsNot DBNull.Value AndAlso .Item("����L��_���̑����l").ToString.Trim <> "" Then
                                dat.AppendLine("����L��=�L�i�{��l�A���̑��y" & .Item("����L��_���̑����l").ToString & "�z�j")
                            Else
                                dat.AppendLine("����L��=�L�i�{��l�A���̑��j")
                            End If
                        ElseIf .Item("����҃R�[�h").ToString = "6" Then
                            If .Item("����L��_���̑����l") IsNot DBNull.Value AndAlso .Item("����L��_���̑����l").ToString.Trim <> "" Then
                                dat.AppendLine("����L��=�L�i�S���ҁA���̑��y" & .Item("����L��_���̑����l").ToString & "�z�j")
                            Else
                                dat.AppendLine("����L��=�L�i�S���ҁA���̑��j")
                            End If
                        ElseIf .Item("����҃R�[�h").ToString = "7" Then
                            If .Item("����L��_���̑����l") IsNot DBNull.Value AndAlso .Item("����L��_���̑����l").ToString.Trim <> "" Then
                                dat.AppendLine("����L��=�L�i�{��l�A�S���ҁA���̑��y" & .Item("����L��_���̑����l").ToString & "�z�j")
                            Else
                                dat.AppendLine("����L��=�L�i�{��l�A�S���ҁA���̑��j")
                            End If
                        End If
                    ElseIf .Item("����L��") Is DBNull.Value _
                            OrElse .Item("����L��").ToString.Trim = "" _
                            OrElse .Item("����L��").ToString = "0" Then

                        dat.AppendLine("����L��=��")
                        'Else
                        '    dat.AppendLine("����L��=����")
                    End If

                    '�d�l�ύX�@2017/03/06 �A���@�ύX��


                    '    dat.AppendLine("����L��=" & .Item("����L��").ToString)


                    dat.AppendLine("������=" & Left(.Item("������").ToString, 10))
                    'dat.AppendLine("������=" & .Item("������").ToString)

                    If .Item("��������").ToString.Length <= 10 Then
                        dat.AppendLine("�����J�n����_14=" & .Item("��������").ToString)
                    Else
                        dat.AppendLine("�����J�n����_11=" & .Item("��������").ToString)
                    End If


                    Dim txt As String = ""
                    If txt = "" Then txt = MakeNaiyouKoteiKoumoku(.Item("���L����_�ύX����").ToString, 11, 68, 14)
                    If txt = "" Then txt = MakeNaiyouKoteiKoumoku(.Item("���L����_�ύX����").ToString, 14, 87, 11)
                    If txt = "" Then txt = MakeNaiyouKoteiKoumoku(.Item("���L����_�ύX����").ToString, 17, 105, 9)


                    dat.AppendLine("���L����=" & .Item("���L����").ToString)
                    dat.AppendLine(txt) '*****
                    dat.AppendLine("��z�S����=" & .Item("�����w�����쐬��").ToString)

                    '�d�l�ύX�@2017/03/06 �A���@�ύX��

                    dat.AppendLine("JHS��z�S��_����=" & .Item("JHS��z�S��_����").ToString)

                    '�d�l�ύX�@2017/03/06 �A���@�ύX��

                    Dim �s���}�� As String = ""

                    If .Item("�ē��}").ToString = "1" Then
                        If �s���}�� <> "" Then �s���}�� &= "�A"
                        �s���}�� &= "�ē��}�i�抄�}�E���ʐ}�Ȃǁj"
                    End If

                    If .Item("�z�u�}").ToString = "1" Then
                        If �s���}�� <> "" Then �s���}�� &= "�A"
                        �s���}�� &= "�z�u�}"
                    End If

                    If .Item("�e�K���ʐ}").ToString = "1" Then
                        If �s���}�� <> "" Then �s���}�� &= "�A"
                        �s���}�� &= "�e�K���ʐ}"
                    End If

                    dat.AppendLine("�s���}��=" & �s���}��)

                    '[FixedDataSection] ���쐬
                    printDat.Append(fcw.CreateFixedDataSection(dat.ToString))

                End With


                'DAT�t�@�C���쐬
                Dim errMsg As String = fcw.GetErrMsg(fcw.WriteData(printDat.ToString))

                ' �����挳�����[�f�[�^PDF�@�o��
                If Not errMsg.Equals(String.Empty) Then
                    '�G���[������ꍇ
                    'Return errMsg

                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "alert('" & errMsg & "');window.close();", True)

                Else
                    url1 = fcw.GetUrlByFileName(paramKbn & paramBukkenNo & "-�����w����")
                    Me.hiddenIframe1.Attributes.Item("src") = url1


                    '�G���[���Ȃ��ꍇ�A���[��OPEN
                    'Dim url As String = fcw.GetUrl(paramKbn, _
                    '                               paramBukkenNo, _
                    '                               "1")
                    'Return String.Empty
                End If

            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "alert('" & Messages.Instance.MSG039E & "');window.close();", True)
            End If

        End If
        'System.IO.File.WriteAllText("\\10.160.192.25\��A���V�X�e�����ijhs���L�j\430054_Earth�����w����(�V���[)�쐬�@�\�ǉ�\04_��A�쐬\test.dat", dat.ToString, System.Text.Encoding.Default)



    End Sub

    ''' <summary>
    ''' ���e�Œ荀�ڍ쐬
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="maxGyouSuu"></param>
    ''' <param name="gyouMonjiSuu"></param>
    ''' <param name="fontSize"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function MakeNaiyouKoteiKoumoku(ByVal text As String _
                                    , ByVal maxGyouSuu As Integer _
                                    , ByVal gyouMonjiSuu As Integer _
                                    , ByVal fontSize As Integer) As String

        text = text.Replace(Chr(13) & Chr(10), Chr(10))
        text = text.Replace(Chr(10) & Chr(13), Chr(10))

        Dim listKoteiKoumoku As New Generic.List(Of String)
        Dim gyouText As String = String.Empty
        Dim oneGyouCount As Integer = 0

        For i As Integer = 0 To text.Length - 1

            Dim str As String = text.Substring(i, 1)

            oneGyouCount += System.Text.Encoding.Default.GetBytes(str).Length

            If oneGyouCount > gyouMonjiSuu _
                OrElse str = Chr(10) _
                OrElse str = "��" _
                Then

                'new row
                If gyouText <> "" Then
                    listKoteiKoumoku.Add(gyouText)
                End If

                oneGyouCount = 0

                If str = Chr(10) Then
                    gyouText = String.Empty
                    oneGyouCount = 0
                Else
                    gyouText = str
                    oneGyouCount = System.Text.Encoding.Default.GetBytes(str).Length
                End If

            Else
                gyouText &= str

            End If

        Next

        If gyouText <> String.Empty Then
            listKoteiKoumoku.Add(gyouText)
        End If

        Dim rtv As New System.Text.StringBuilder

        For i As Integer = 0 To listKoteiKoumoku.Count - 1

            If fontSize = 9 Then
                If i < maxGyouSuu + 1 Then
                    rtv.AppendLine("�w��_" & fontSize & "_" & i & "=" & listKoteiKoumoku.Item(i))
                End If
            Else
                rtv.AppendLine("�w��_" & fontSize & "_" & i & "=" & listKoteiKoumoku.Item(i))
            End If

        Next

        If listKoteiKoumoku.Count > maxGyouSuu AndAlso fontSize <> 9 Then
            Return ""
        Else
            Return rtv.ToString
        End If

    End Function

    ''' <summary>
    ''' �w�����쐬
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRun.Click

        Me.hiddenIframe1.Attributes.Item("src") = ""

        Dim TyouhyouPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouPath").ToString
        TyouhyouPath = TyouhyouPath & ViewState("kbn") & ViewState("bukkenNo") & "-�����w����" & ".pdf"

        '�i�[�T�[�o�[A�Œ��[�̕ۑ�PATH
        Dim TyousaSijisyoTyouhyouServerAPath As String = System.Configuration.ConfigurationManager.AppSettings("TyousaSijisyoTyouhyouServerAPath").ToString
        TyousaSijisyoTyouhyouServerAPath = TyousaSijisyoTyouhyouServerAPath & ViewState("kbn") & ViewState("bukkenNo") & "�����w����.pdf"

        Dim idx As Integer = 0
Modoru:
        Try
            idx = idx + 1
            '�i�[�T�[�o�[A��Copy
            System.IO.File.Copy(TyouhyouPath, TyousaSijisyoTyouhyouServerAPath, True)
        Catch ex As Exception
            System.Threading.Thread.Sleep(2000)
            If idx < 15 Then
                GoTo Modoru
            End If
        End Try


        '���[��Open����
        Call Me.GetFile(TyouhyouPath)

    End Sub

    ''' <summary>
    ''' �t�@�C���擾
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <remarks></remarks>
    Private Sub GetFile(ByVal strFileName As String)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function OpenFile(){")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').href = '" & "file:" & strFileName.Replace("\", "/") & "';")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').click();")
            .AppendLine("   setTimeout(function(){(window.open('','_self').opener=window).close();},500);")
            .AppendLine("}")
            .AppendLine("setTimeout('OpenFile()',1000);")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub
End Class