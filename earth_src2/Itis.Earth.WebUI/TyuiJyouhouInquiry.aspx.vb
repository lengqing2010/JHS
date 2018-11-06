Imports Itis.Earth.BizLogic

Partial Public Class TyuiJyouhouInquiry
    Inherits System.Web.UI.Page
    Dim dmp As New DataTable
    ''' <summary>�����X���̒��ӏ��Ɖ�</summary>
    ''' <remarks>�����X���ӏ��Ɖ��񋟂���</remarks>
    ''' <history>
    ''' <para>2009/07/15�@����(��A���V�X�e����)�@�o�^�쐬</para>
    ''' </history>
    Private blnBtn As Boolean
    Private KihonJyouhouBL As New KakakuseikyuJyouhouLogic()
    Private earthAction As New EarthAction




    '�ҏW���ڔ񊈐��A�����ݒ�Ή��@20180905�@�������@�Ή��@��
    'salesforce����_�ҏW�񊈐��t���O �擾
    Private Function Iskassei(ByVal KameitenCd As String, ByVal kbn As String) As Boolean

        If kbn.Trim <> "" Then
            If ViewState("Iskassei") Is Nothing Then
                If kbn = "" Then
                    ViewState("Iskassei") = ""
                Else
                    ViewState("Iskassei") = (New Salesforce).GetSalesforceHikasseiFlgByKbn(kbn)
                End If

            End If
        Else

            If ViewState("Iskassei") Is Nothing Then
                ViewState("Iskassei") = (New Salesforce).GetSalesforceHikasseiFlg(KameitenCd)
            End If

        End If
        Return ViewState("Iskassei").ToString <> "1"
    End Function

    '�ҏW���ڔ񊈐��A�����ݒ肷��
    Public Sub SetKassei()

        ViewState("Iskassei") = Nothing
        Dim kbn As String = ""
        Dim itKassei As Boolean = Iskassei("", lblKyoutuKubun.Text.Trim)


        '��{���i

        ddlKihonSyouhin.Enabled = itKassei
        ddlKihonSyouhin.CssClass = IIf(itKassei, "", "readOnly")
        tbxKihonSyouhinTyuuibun.ReadOnly = Not itKassei
        tbxKihonSyouhinTyuuibun.CssClass = IIf(itKassei, "", "readOnly")
        btnKihonSyouhin.Enabled = itKassei
        btnKihonSyouhin.CssClass = IIf(itKassei, "", "readOnly")

        '��{�������@
        ddlKihonTyousaHouhou.Enabled = itKassei
        ddlKihonTyousaHouhou.CssClass = IIf(itKassei, "", "readOnly")
        tbxKihonTyousaHouhouTyuuibun.ReadOnly = Not itKassei
        tbxKihonTyousaHouhouTyuuibun.CssClass = IIf(itKassei, "", "readOnly")
        btnKihonTyousaHouhou.Enabled = itKassei
        btnKihonTyousaHouhou.CssClass = IIf(itKassei, "", "readOnly")


        If Not itKassei Then
            For i As Integer = 0 To grdNaiyou11.Rows.Count - 1
                For Each c As Control In grdNaiyou11.Rows(i).Controls(1).Controls
                    Try
                        CType(c, TextBox).ReadOnly = Not itKassei
                        CType(c, TextBox).CssClass = IIf(itKassei, "", "readOnly")
                    Catch ex1 As Exception
                        Try
                            CType(c, Button).Enabled = itKassei
                            CType(c, Button).CssClass = IIf(itKassei, "", "readOnly")
                        Catch ex2 As Exception
                            Try
                                CType(c, DropDownList).Enabled = itKassei
                                CType(c, DropDownList).CssClass = IIf(itKassei, "", "readOnly")
                            Catch ex As Exception
                            End Try

                        End Try
                    End Try
                Next
            Next
            For i As Integer = 0 To grdNaiyou13.Rows.Count - 1
                For Each c As Control In grdNaiyou13.Rows(i).Controls(1).Controls
                    Try
                        CType(c, TextBox).ReadOnly = Not itKassei
                        CType(c, TextBox).CssClass = IIf(itKassei, "", "readOnly")
                    Catch ex1 As Exception
                        Try
                            CType(c, Button).Enabled = itKassei
                            CType(c, Button).CssClass = IIf(itKassei, "", "readOnly")
                        Catch ex2 As Exception
                            Try
                                CType(c, DropDownList).Enabled = itKassei
                                CType(c, DropDownList).CssClass = IIf(itKassei, "", "readOnly")
                            Catch ex As Exception
                            End Try

                        End Try
                    End Try
                Next
            Next
            For i As Integer = 0 To grdNaiyou19.Rows.Count - 1
                For Each c As Control In grdNaiyou19.Rows(i).Controls(1).Controls
                    Try
                        CType(c, TextBox).ReadOnly = Not itKassei
                        CType(c, TextBox).CssClass = IIf(itKassei, "", "readOnly")
                    Catch ex1 As Exception
                        Try
                            CType(c, Button).Enabled = itKassei
                            CType(c, Button).CssClass = IIf(itKassei, "", "readOnly")
                        Catch ex2 As Exception
                            Try
                                CType(c, DropDownList).Enabled = itKassei
                                CType(c, DropDownList).CssClass = IIf(itKassei, "", "readOnly")
                            Catch ex As Exception
                            End Try

                        End Try
                    End Try
                Next
            Next

            For i As Integer = 0 To grdNaiyou21.Rows.Count - 1
                For Each c As Control In grdNaiyou21.Rows(i).Controls(1).Controls
                    Try
                        CType(c, TextBox).ReadOnly = Not itKassei
                        CType(c, TextBox).CssClass = IIf(itKassei, "", "readOnly")
                    Catch ex1 As Exception
                        Try
                            CType(c, Button).Enabled = itKassei
                            CType(c, Button).CssClass = IIf(itKassei, "", "readOnly")
                        Catch ex2 As Exception
                            Try
                                CType(c, DropDownList).Enabled = itKassei
                                CType(c, DropDownList).CssClass = IIf(itKassei, "", "readOnly")
                            Catch ex As Exception
                            End Try

                        End Try
                    End Try
                Next
            Next

            For i As Integer = 0 To grdNaiyou23.Rows.Count - 1
                For Each c As Control In grdNaiyou23.Rows(i).Controls(1).Controls
                    Try
                        CType(c, TextBox).ReadOnly = Not itKassei
                        CType(c, TextBox).CssClass = IIf(itKassei, "", "readOnly")
                    Catch ex1 As Exception
                        Try
                            CType(c, Button).Enabled = itKassei
                            CType(c, Button).CssClass = IIf(itKassei, "", "readOnly")
                        Catch ex2 As Exception
                            Try
                                CType(c, DropDownList).Enabled = itKassei
                                CType(c, DropDownList).CssClass = IIf(itKassei, "", "readOnly")
                            Catch ex As Exception
                            End Try

                        End Try
                    End Try
                Next
            Next
            For i As Integer = 0 To grdNaiyou29.Rows.Count - 1
                For Each c As Control In grdNaiyou29.Rows(i).Controls(1).Controls
                    Try
                        CType(c, TextBox).ReadOnly = Not itKassei
                        CType(c, TextBox).CssClass = IIf(itKassei, "", "readOnly")
                    Catch ex1 As Exception
                        Try
                            CType(c, Button).Enabled = itKassei
                            CType(c, Button).CssClass = IIf(itKassei, "", "readOnly")
                        Catch ex2 As Exception
                            Try
                                CType(c, DropDownList).Enabled = itKassei
                                CType(c, DropDownList).CssClass = IIf(itKassei, "", "readOnly")
                            Catch ex As Exception
                            End Try

                        End Try
                    End Try
                Next
            Next
        End If


        ddl_koj_mitiraisyo_soufu_fuyou.Enabled = itKassei
        ddl_koj_mitiraisyo_soufu_fuyou.CssClass = IIf(itKassei, "", "readOnly")

        btn_siyou_kakuninhi_jigyousya.Enabled = itKassei
        btn_siyou_kakuninhi_jigyousya.CssClass = IIf(itKassei, "", "readOnly")


        tbx_siyou_kakuninhi_jigyousya.ReadOnly = Not itKassei
        tbx_siyou_kakuninhi_jigyousya.CssClass = IIf(itKassei, "", "readOnly")
        tbx_siyou_kakuninhi_kojkaisya.ReadOnly = Not itKassei
        tbx_siyou_kakuninhi_kojkaisya.CssClass = IIf(itKassei, "", "readOnly")

        btn_siyou_kakuninhi_kojkaisya.Enabled = itKassei
        btn_siyou_kakuninhi_kojkaisya.CssClass = IIf(itKassei, "", "readOnly")

    End Sub


    ''' <summary>�y�[�W���b�h</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        Dim user_info As New LoginUserInfo
        Dim commonChk As New CommonCheck
        jBn.userAuth(user_info)

        Dim Ninsyou As New BizLogic.Ninsyou
        ' ���[�U�[��{�F��
        Context.Items("strFailureMsg") = Messages.Instance.MSG2024E '�i"�Y�����[�U�[������܂���B"�j
        If Ninsyou.GetUserID() = "" Then
            Server.Transfer("CommonErr.aspx")
        End If
        Context.Items("strFailureMsg") = Messages.Instance.MSG2020E '�i"����������܂���B"�j
        If user_info Is Nothing Then
            ViewState("UserId") = Ninsyou.GetUserID()
        Else
            ViewState("UserId") = user_info.LoginUserId
        End If
        ViewState("KameitenCd") = Request.QueryString("strKameitenCd")
        '���ʏ���ݒ肷��
        Dim dtKyoutuuJyouhouTable As New Itis.Earth.DataAccess.KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
        Dim KyoutuuJyouhouLogic As New Itis.Earth.BizLogic.KyoutuuJyouhouLogic
        dtKyoutuuJyouhouTable = KyoutuuJyouhouLogic.GetKameitenInfo(ViewState("KameitenCd"))
        If dtKyoutuuJyouhouTable.Rows.Count > 0 Then
            With dtKyoutuuJyouhouTable.Rows(0)
                lblKyoutuKubun.Text = TrimNull(.Item("kbn"))
                lblKubunMei.Text = TrimNull(.Item("kbn_mei"))
                lblKyoutuKameitenCd.Text = TrimNull(.Item("kameiten_cd"))
                lblKyoutuKameitenMei1.Text = TrimNull(.Item("kameiten_mei1"))
                lblKyoutuKameitenMei2.Text = TrimNull(.Item("kameiten_mei2"))
                lblHi.Text = CDate(.Item("sansyou_date")).ToString("yyyy/MM/dd HH:mm:ss")
            End With

            '==================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���==============================
            '�u����v���Z�b�g����
            Call Me.SetTorikesi()
            '==================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���==============================

            '========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���======================
            '�u�H�������ʁv���Z�b�g����
            Call Me.SetKoujiUriageSyuubetu()
            '========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���======================

            commonChk.SetURL(Me, ViewState("UserId"))
        Else
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If
        'Javascript�쐬
        MakeScript()
        Dim dtAccountTable As DataAccess.CommonSearchDataSet.AccountTableDataTable

        If user_info Is Nothing Then
            blnBtn = False
        Else
            dtAccountTable = commonChk.CheckKengen(user_info.AccountNo)
            If dtAccountTable.Rows.Count = 0 Then
                blnBtn = False
            Else
                If dtAccountTable.Rows(0).Item("irai_gyoumu_kengen") = -1 _
                                                OrElse dtAccountTable.Rows(0).Item("kekka_gyoumu_kengen") = -1 _
                                                OrElse dtAccountTable.Rows(0).Item("hosyou_gyoumu_kengen") = -1 _
                                                OrElse dtAccountTable.Rows(0).Item("hkks_gyoumu_kengen") = -1 _
                                                OrElse dtAccountTable.Rows(0).Item("koj_gyoumu_kengen") = -1 = -1 Then
                    blnBtn = True
                Else
                    blnBtn = False
                End If
            End If


        End If

        If Not IsPostBack Then
            jikouLink.HRef = "javascript:changeDisplay('" & jikouTbody.ClientID & "');changeDisplay('" & jikouSpan.ClientID & "');"
            titleLink1.HRef = "javascript:changeDisplay('" & naiyouTbody1.ClientID & "');changeDisplay('" & titleSpan1.ClientID & "');"
            titleLink2.HRef = "javascript:changeDisplay('" & naiyouTbody2.ClientID & "');changeDisplay('" & titleSpan2.ClientID & "');"
            titleLink3.HRef = "javascript:changeDisplay('" & naiyouTbody3.ClientID & "');changeDisplay('" & titleSpan3.ClientID & "');"
            titleLink13.HRef = "javascript:changeDisplay('" & naiyouTbody13.ClientID & "');changeDisplay('" & titleSpan13.ClientID & "');"

            '========2015/02/03 ���� 407679�̑Ή� �ǉ���======================
            titleLink14.HRef = "javascript:changeDisplay('" & toraburuTbody.ClientID & "');changeDisplay('" & toraburuSpan.ClientID & "');"
            '========2015/02/03 ���� 407679�̑Ή� �ǉ���======================

            titleLinkKihonSyouhin.HRef = "javascript:changeDisplay('" & kihonSyouhinTbody.ClientID & "');changeDisplay('" & toraburuSpan.ClientID & "');"
            titleLinkKihonTyousaHouhou.HRef = "javascript:changeDisplay('" & kihonTyousaHouhouTbody.ClientID & "');changeDisplay('" & toraburuSpan.ClientID & "');"

            titleLink4.HRef = "javascript:changeDisplay('" & naiyouTbody4.ClientID & "');changeDisplay('" & titleSpan4.ClientID & "');"


            'A  =�D�撍�ӎ���
            'B  =�ʏ풍�ӎ���
            '11 =�w�� �������
            '19 =�֎~ �������
            '21 =�w�� �H�����
            '29 =�֎~ �H�����
            '1  =�w�� ����
            '9  =�֎~ ����
            'TORA =�g���u���E�N���[�����
            GrdStyle(ViewState("KameitenCd"), "11")
            GrdStyle(ViewState("KameitenCd"), "19")
            GrdStyle(ViewState("KameitenCd"), "1")

            GrdStyle(ViewState("KameitenCd"), "21")
            GrdStyle(ViewState("KameitenCd"), "29")


            GrdStyle(ViewState("KameitenCd"), "9")

            GrdStyle(ViewState("KameitenCd"), "13")
            GrdStyle(ViewState("KameitenCd"), "23")
            GrdStyle(ViewState("KameitenCd"), "3")
            GrdStyle(ViewState("KameitenCd"), "A", ViewState("UserId"))

            '========2015/02/03 ���� 407679�̑Ή� �ǉ���======================
            GrdStyle(ViewState("KameitenCd"), "TORA", ViewState("UserId"))
            '========2015/02/03 ���� 407679�̑Ή� �ǉ���======================


            Me.tbx_siyou_kakuninhi_jigyousya.Attributes.Add("onblur", "checkNumberAddFig(this);")
            Me.tbx_siyou_kakuninhi_jigyousya.Attributes.Add("onfocus", "removeFig(this);")


            Me.tbx_siyou_kakuninhi_kojkaisya.Attributes.Add("onblur", "checkNumberAddFig(this);")
            Me.tbx_siyou_kakuninhi_kojkaisya.Attributes.Add("onfocus", "removeFig(this);")

            Call Me.SetKihouSyouhinAndTyousaHouhou()
        Else
            closecover()
        End If

        btnEigyouJyouhouInquiry.Visible = False
        btnClose.Attributes.Add("onclick", "window.close();")
        SetKassei()
    End Sub
    ''' <summary>�e�[�u�����X�V</summary>
    ''' <returns>�X�V���ʂ̕�����</returns>
    Public Function TableUpdate(ByVal strKameitenCd As String, ByVal strBtn As String, ByVal strKbn As String, ByVal strUserId As String, ByVal strRowTime As String, ByVal intRow As Integer) As String
        Dim TyuiJyouhouInquiryLogic As New Itis.Earth.BizLogic.TyuiJyouhouInquiryLogic
        TableUpdate = ""
        Select Case strKbn
            'A  =�D�撍�ӎ���
            'B  =�ʏ풍�ӎ���
            Case "A", "B"
                Dim dtTyuuiJikouUPDTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable
                dtTyuuiJikouUPDTable.Rows.Add(dtTyuuiJikouUPDTable.NewRow)
                With dtTyuuiJikouUPDTable.Rows(0)
                    .Item("kameiten_cd") = strKameitenCd
                    .Item("tyuuijikou_syubetu") = Split(hidDdl.Value, ":")(0)
                    .Item("nyuuryoku_no") = hidNo.Value
                    .Item("nyuuryoku_date") = hidDate.Value
                    .Item("uketukesya_mei") = hidMei.Value
                    .Item("naiyou") = hidNaiyou.Value
                    .Item("kousinsya") = strUserId
                End With
                Select Case strBtn
                    Case "�o�^"
                        Return TyuiJyouhouInquiryLogic.SetInsTyuuiJikou(dtTyuuiJikouUPDTable)
                    Case "�C��"
                        Return TyuiJyouhouInquiryLogic.SetUpdTyuuiJikou(dtTyuuiJikouUPDTable, strRowTime)
                    Case "�폜"
                        Return TyuiJyouhouInquiryLogic.SetDelTyuuiJikou(strKameitenCd, hidNo.Value, strRowTime, strUserId)
                End Select

                '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
                'TORA =�g���u���E�N���[��
            Case "TORA"
                Dim dtTyuuiJikouUPDTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable
                dtTyuuiJikouUPDTable.Rows.Add(dtTyuuiJikouUPDTable.NewRow)
                With dtTyuuiJikouUPDTable.Rows(0)
                    .Item("kameiten_cd") = strKameitenCd
                    .Item("tyuuijikou_syubetu") = Split(hidDdl.Value, ":")(0)
                    .Item("nyuuryoku_no") = hidNo.Value
                    .Item("nyuuryoku_date") = hidDate.Value
                    .Item("uketukesya_mei") = hidMei.Value
                    .Item("naiyou") = hidNaiyou.Value
                    .Item("kousinsya") = strUserId
                End With
                Select Case strBtn
                    Case "�o�^"
                        Return TyuiJyouhouInquiryLogic.SetInsTyuuiJikou(dtTyuuiJikouUPDTable)
                    Case "�C��"
                        Return TyuiJyouhouInquiryLogic.SetUpdTyuuiJikou(dtTyuuiJikouUPDTable, strRowTime)
                    Case "�폜"
                        Return TyuiJyouhouInquiryLogic.SetDelTyuuiJikou(strKameitenCd, hidNo.Value, strRowTime, strUserId)
                End Select
                '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================

                '11 =�w�� �������
                '19 =�֎~ �������
                '21 =�w�� �H�����
                '29 =�֎~ �H�����
            Case "11", "19", "21", "29", "13", "23"
                Dim dtTyousaKaisyaUPDTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable
                dtTyousaKaisyaUPDTable.Rows.Add(dtTyousaKaisyaUPDTable.NewRow)
                With dtTyousaKaisyaUPDTable.Rows(0)
                    .Item("kameiten_cd") = strKameitenCd
                    .Item("kaisya_kbn") = Left(strKbn, 1)
                    .Item("kahi_kbn") = Right(strKbn, 1)
                    .Item("tys_kaisya_cd") = Left(Split(hidDdl.Value, ":")(0), 4) & ":" & Left(Split(hidDdl.Value, ":")(1), 4)
                    .Item("jigyousyo_cd") = Right(Split(hidDdl.Value, ":")(0), 2) & ":" & Right(Split(hidDdl.Value, ":")(1), 2)
                    .Item("kousinsya") = strUserId
                End With
                Select Case strBtn
                    Case "�o�^"
                        Return TyuiJyouhouInquiryLogic.SetInsTyousaKaisya(dtTyousaKaisyaUPDTable)
                    Case "�C��"
                        Return TyuiJyouhouInquiryLogic.SetUpdTyousaKaisya(dtTyousaKaisyaUPDTable, strRowTime)
                    Case "�폜"
                        Return TyuiJyouhouInquiryLogic.SetDelTyousaKaisya(dtTyousaKaisyaUPDTable, strRowTime, intRow)
                End Select
            Case Else
                '1  =�w�� ����
                '9  =�֎~ ����
                Select Case strBtn
                    Case "�o�^"
                        Return TyuiJyouhouInquiryLogic.SetInsKisoSiyouSettei(strKameitenCd, strKbn, Split(hidDdl.Value, ":")(0), strUserId)
                    Case "�C��"
                        Return TyuiJyouhouInquiryLogic.SetUpdKisoSiyouSettei(strKameitenCd, strKbn, hidDdl.Value, strUserId, strRowTime)
                    Case "�폜"
                        Return TyuiJyouhouInquiryLogic.SetDelKisoSiyouSettei(strKameitenCd, strKbn, Split(hidDdl.Value, ":")(1), strRowTime, strUserId, intRow)
                End Select
        End Select
    End Function
    ''' <summary>�����f�[�^��ݒ�</summary>
    ''' <returns>�p�����[�^�f�[�^�e�[�u��</returns>
    Public Function CreateHeadDataSource(ByVal strKameitenCd As String, ByVal strUerId As String, ByVal strKbn As String) As DataTable
        Dim TyuiJyouhouInquiryLogic As New Itis.Earth.BizLogic.TyuiJyouhouInquiryLogic
        Select Case strKbn
            Case "11", "19", "21", "29", "13", "23"
                Dim dtKaisyaTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.KaisyaTableDataTable

                dtKaisyaTable = TyuiJyouhouInquiryLogic.GetKaisyaJyouhouInfo(strKameitenCd, strKbn)
                dtKaisyaTable.AddKaisyaTableRow(dtKaisyaTable.NewRow)
                Return dtKaisyaTable
            Case "A"
                Dim dtTyuuiJikouTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.TyuuiJikouTableDataTable
                dtTyuuiJikouTable = TyuiJyouhouInquiryLogic.GetYuusenTyuuiJikouInfo(strKameitenCd, strUerId)
                dtTyuuiJikouTable.AddTyuuiJikouTableRow(dtTyuuiJikouTable.NewRow)
                Return dtTyuuiJikouTable
            Case "B"
                Dim dtTyuuiJikouTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.TyuuiJikouTableDataTable
                dtTyuuiJikouTable = TyuiJyouhouInquiryLogic.GetTuujyouTyuuiJikouInfo(strKameitenCd, strUerId)
                dtTyuuiJikouTable.AddTyuuiJikouTableRow(dtTyuuiJikouTable.NewRow)
                Return dtTyuuiJikouTable

                '========2015/02/03 ���� 407679�̑Ή� �ǉ���======================
            Case "TORA"
                Dim dtTyuuiJikouTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.TyuuiJikouTableDataTable
                dtTyuuiJikouTable = TyuiJyouhouInquiryLogic.GetTuujyouTyuuiJikouInfoTORA(strKameitenCd, strUerId)
                dtTyuuiJikouTable.AddTyuuiJikouTableRow(dtTyuuiJikouTable.NewRow)
                Return dtTyuuiJikouTable
                '========2015/02/03 ���� 407679�̑Ή� �ǉ���======================
            Case Else
                Dim dtKisoSiyouTableData As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.KisoSiyouTableDataTable
                dtKisoSiyouTableData = TyuiJyouhouInquiryLogic.GetKisoSiyouSetteiInfo(strKameitenCd, strKbn)
                dtKisoSiyouTableData.AddKisoSiyouTableRow(dtKisoSiyouTableData.NewRow)
                Return dtKisoSiyouTableData
        End Select

    End Function
    ''' <summary> GridView���e�A�t�H�[�}�b�g���Z�b�g</summary>
    Sub GrdViewStyle(ByVal intwidth() As String, ByVal grdBody As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0
        If IsArray(intwidth) AndAlso grdBody.Rows.Count > 0 Then
            For intRow = 0 To grdBody.Rows.Count - 1
                grdBody.Rows(intRow).Height = "22"
                For intCol = 0 To grdBody.Rows(intRow).Cells.Count - 1
                    If intwidth(intCol) <> "" Then
                        grdBody.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")

                    End If
                Next
            Next
        End If
    End Sub
    ''' <summary> GridView�f�[�^���Z�b�g</summary>
    Sub GrdViewControl(ByVal grdBody As GridView, ByVal strKbn As String, ByVal blnErr As Boolean)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0
        Dim intCount As Integer = 0
        Dim strSelect As String = ""
        Dim strMei As String = ""
        Dim TyuiJyouhouInquiryLogic As New Itis.Earth.BizLogic.TyuiJyouhouInquiryLogic
        For intRow = 0 To grdBody.Rows.Count - 1
            Dim arrRowId(4) As String
            For intCol = 0 To grdBody.Rows(intRow).Cells.Count - 1
                If strKbn = "A" Or strKbn = "B" Then
                    Select Case intCol
                        Case 0 '���
                            Dim comControl As New DropDownList
                            Dim dtMeisyouTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.MeisyouTableDataTable
                            comControl.Width = Unit.Pixel(152)
                            comControl.Items.Insert(0, New ListItem(String.Empty, ":"))
                            dtMeisyouTable = TyuiJyouhouInquiryLogic.GetSyubetuInfo(strKbn)
                            If dtMeisyouTable.Rows.Count > 0 Then
                                For intCount = 0 To dtMeisyouTable.Rows.Count - 1

                                    Dim ddlist As New ListItem
                                    ddlist.Text = dtMeisyouTable.Rows(intCount).Item("code") & ":" & dtMeisyouTable.Rows(intCount).Item("meisyou")
                                    ddlist.Value = dtMeisyouTable.Rows(intCount).Item("code")

                                    comControl.Items.Add(ddlist)
                                Next
                            End If
                            If blnErr Then
                                strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "ddl" & strKbn & intRow)

                            Else
                                If grdBody.Rows(intRow).Cells(intCol).Text <> "&nbsp;" Then
                                    strSelect = Split(grdBody.Rows(intRow).Cells(intCol).Text, ":")(0)
                                Else
                                    strSelect = ""
                                End If
                            End If

                            If comControl.Items.FindByValue(strSelect) IsNot Nothing Then
                                comControl.Items.FindByValue(strSelect).Selected = True
                            Else
                                comControl.SelectedIndex = 0
                            End If
                            comControl.ID = "ddl" & strKbn & intRow
                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)
                            arrRowId(0) = comControl.ClientID
                            If hidFirstNameKyoutu.Value = "" Then
                                hidFirstNameKyoutu.Value = comControl.ClientID.Substring(0, comControl.ClientID.LastIndexOf("ctl")).Replace("_", "$")
                            End If

                        Case 1 '���͓�
                            Dim comControl As New TextBox

                            comControl.Width = Unit.Pixel(64)
                            If blnErr Then
                                strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "date" & strKbn & intRow)

                            Else
                                strSelect = grdBody.Rows(intRow).Cells(intCol).Text

                            End If

                            If strSelect <> "" And strSelect <> "&nbsp;" Then
                                comControl.Text = CDate(strSelect).ToString("yyyy/MM/dd")
                            Else
                                comControl.Text = ""
                            End If
                            strSelect = ""
                            comControl.ID = "date" & strKbn & intRow
                            comControl.Attributes.Add("onblur", "checkDate(this);")
                            comControl.Attributes.Add("Style", "ime-mode:disabled;")
                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)
                            arrRowId(1) = comControl.ClientID
                        Case 2 '��t��
                            Dim comControl As New TextBox

                            comControl.Width = Unit.Pixel(133)
                            If blnErr Then
                                strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "mei" & strKbn & intRow)
                                comControl.Text = strSelect
                            Else
                                comControl.Text = dmp.Rows(intRow).Item(intCol).ToString
                            End If
                            strSelect = ""
                            comControl.ID = "mei" & strKbn & intRow
                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)
                            arrRowId(2) = comControl.ClientID
                        Case 3 '���e
                            Dim comControl As New TextBox
                            comControl.ID = "naiyou" & strKbn & intRow
                            comControl.Width = Unit.Pixel(499)
                            If intRow = grdBody.Rows.Count - 1 Then
                                If blnErr Then
                                    comControl.Text = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "naiyou" & strKbn & intRow)
                                Else
                                    strSelect = grdBody.Rows(intRow).Cells(intCol).Text

                                End If
                                strSelect = ""
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)

                                arrRowId(3) = comControl.ClientID
                                Dim BtnSinki As New Button
                                BtnSinki.Text = "�o�^"
                                BtnSinki.Width = 32
                                BtnSinki.Enabled = blnBtn
                                BtnAttributes(BtnSinki, "", arrRowId, strKbn, "", grdBody, intRow, "")
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(BtnSinki)
                            Else
                                If blnErr Then
                                    strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "naiyou" & strKbn & intRow)
                                    comControl.Text = Replace(strSelect, "&nbsp;", "")
                                Else

                                    comControl.Text = dmp.Rows(intRow).Item(intCol).ToString
                                End If

                                strSelect = ""
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)
                                arrRowId(3) = comControl.ClientID
                                Dim BtnSyuusei As New Button
                                BtnSyuusei.Text = "�C��"
                                BtnSyuusei.Width = 32
                                BtnSyuusei.Enabled = blnBtn
                                BtnAttributes(BtnSyuusei, Split(grdBody.Rows(intRow).Cells(0).Text, ":")(1), arrRowId, strKbn, "", grdBody, intRow, grdBody.Rows(intRow).Cells(intCol + 1).Text)
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(BtnSyuusei)
                                Dim btnSakujyo As New Button
                                btnSakujyo.Text = "�폜"
                                btnSakujyo.Width = 32
                                btnSakujyo.Enabled = blnBtn
                                BtnAttributes(btnSakujyo, Split(grdBody.Rows(intRow).Cells(0).Text, ":")(1), arrRowId, strKbn, "", grdBody, intRow, grdBody.Rows(intRow).Cells(intCol + 1).Text)

                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(btnSakujyo)

                            End If
                        Case 4 '�X�V��

                            grdBody.Rows(intRow).Cells(intCol).Visible = False
                    End Select

                    '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
                ElseIf strKbn = "TORA" Then
                    Select Case intCol
                        Case 0 '���
                            Dim comControl As New DropDownList
                            Dim dtMeisyouTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.MeisyouTableDataTable
                            comControl.Width = Unit.Pixel(152)
                            comControl.Items.Insert(0, New ListItem(String.Empty, ":"))
                            dtMeisyouTable = TyuiJyouhouInquiryLogic.GetSyubetuInfo(strKbn)
                            If dtMeisyouTable.Rows.Count > 0 Then
                                For intCount = 0 To dtMeisyouTable.Rows.Count - 1

                                    Dim ddlist As New ListItem
                                    ddlist.Text = dtMeisyouTable.Rows(intCount).Item("code") & ":" & dtMeisyouTable.Rows(intCount).Item("meisyou")
                                    ddlist.Value = dtMeisyouTable.Rows(intCount).Item("code")

                                    comControl.Items.Add(ddlist)
                                Next
                            End If
                            If blnErr Then
                                strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "ddl" & strKbn & intRow)

                            Else
                                If grdBody.Rows(intRow).Cells(intCol).Text <> "&nbsp;" Then
                                    strSelect = Split(grdBody.Rows(intRow).Cells(intCol).Text, ":")(0)
                                Else
                                    strSelect = ""
                                End If
                            End If

                            If comControl.Items.FindByValue(strSelect) IsNot Nothing Then
                                comControl.Items.FindByValue(strSelect).Selected = True
                            Else
                                comControl.SelectedIndex = 0
                            End If
                            comControl.ID = "ddl" & strKbn & intRow
                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)
                            arrRowId(0) = comControl.ClientID
                            If hidFirstNameKyoutu.Value = "" Then
                                hidFirstNameKyoutu.Value = comControl.ClientID.Substring(0, comControl.ClientID.LastIndexOf("ctl")).Replace("_", "$")
                            End If

                        Case 1 '���͓�
                            Dim comControl As New TextBox

                            comControl.Width = Unit.Pixel(64)
                            If blnErr Then
                                strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "date" & strKbn & intRow)

                            Else
                                strSelect = grdBody.Rows(intRow).Cells(intCol).Text

                            End If

                            If strSelect <> "" And strSelect <> "&nbsp;" Then
                                comControl.Text = CDate(strSelect).ToString("yyyy/MM/dd")
                            Else
                                comControl.Text = ""
                            End If
                            strSelect = ""
                            comControl.ID = "date" & strKbn & intRow
                            comControl.Attributes.Add("onblur", "checkDate(this);")
                            comControl.Attributes.Add("Style", "ime-mode:disabled;")
                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)
                            arrRowId(1) = comControl.ClientID
                        Case 2 '��t��
                            Dim comControl As New TextBox

                            comControl.Width = Unit.Pixel(133)
                            If blnErr Then
                                strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "mei" & strKbn & intRow)
                                comControl.Text = strSelect
                            Else
                                comControl.Text = dmp.Rows(intRow).Item(intCol).ToString
                            End If
                            strSelect = ""
                            comControl.ID = "mei" & strKbn & intRow
                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)
                            arrRowId(2) = comControl.ClientID
                        Case 3 '���e

                            If intRow = grdBody.Rows.Count - 1 Then
                                '�v���_�E���\��
                                Dim comConDrop As New DropDownList
                                comConDrop.Width = Unit.Pixel(220)
                                comConDrop.ID = "ddlNaiyou" & strKbn & intRow
                                comConDrop.Items.Insert(0, New ListItem(String.Empty, ":"))
                                Dim dtMeisyou33 As Data.DataTable = TyuiJyouhouInquiryLogic.GetSyubetuInfo33()

                                If dtMeisyou33.Rows.Count > 0 Then
                                    For intCount = 0 To dtMeisyou33.Rows.Count - 1

                                        Dim ddlist As New ListItem
                                        ddlist.Text = dtMeisyou33.Rows(intCount).Item("code") & ":" & dtMeisyou33.Rows(intCount).Item("meisyou")
                                        ddlist.Value = dtMeisyou33.Rows(intCount).Item("meisyou")

                                        comConDrop.Items.Add(ddlist)
                                    Next
                                End If
                                If blnErr Then
                                    strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "ddlNaiyou" & strKbn & intRow)

                                Else
                                    If grdBody.Rows(intRow).Cells(intCol).Text <> "&nbsp;" Then
                                        strSelect = Split(grdBody.Rows(intRow).Cells(intCol).Text, ":")(0)
                                    Else
                                        strSelect = ""
                                    End If
                                End If

                                If comConDrop.Items.FindByValue(strSelect) IsNot Nothing Then
                                    comConDrop.Items.FindByValue(strSelect).Selected = True
                                Else
                                    comConDrop.SelectedIndex = 0
                                End If

                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(comConDrop)
                                arrRowId(3) = comConDrop.ClientID
                                If hidFirstNameKyoutu.Value = "" Then
                                    hidFirstNameKyoutu.Value = comConDrop.ClientID.Substring(0, comConDrop.ClientID.LastIndexOf("ctl")).Replace("_", "$")
                                End If


                                '�e�L�X�g�{�b�N�X
                                Dim comControl As New TextBox
                                comControl.ID = "naiyou" & strKbn & intRow
                                comControl.Width = Unit.Pixel(278)


                                If blnErr Then
                                    comControl.Text = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "naiyou" & strKbn & intRow)
                                Else
                                    strSelect = grdBody.Rows(intRow).Cells(intCol).Text

                                End If
                                strSelect = ""
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)

                                arrRowId(4) = comControl.ClientID
                                Dim BtnSinki As New Button
                                BtnSinki.Text = "�o�^"
                                BtnSinki.Width = 32
                                BtnSinki.Enabled = blnBtn
                                BtnAttributes(BtnSinki, "", arrRowId, strKbn, "", grdBody, intRow, "")
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(BtnSinki)
                            Else

                                '�e�L�X�g�{�b�N�X
                                Dim comControl As New TextBox
                                comControl.ID = "naiyou" & strKbn & intRow
                                comControl.Width = Unit.Pixel(499)

                                If blnErr Then
                                    strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "naiyou" & strKbn & intRow)
                                    comControl.Text = Replace(strSelect, "&nbsp;", "")
                                Else

                                    comControl.Text = dmp.Rows(intRow).Item(intCol).ToString
                                End If

                                strSelect = ""
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(comControl)
                                arrRowId(3) = comControl.ClientID
                                Dim BtnSyuusei As New Button
                                BtnSyuusei.Text = "�C��"
                                BtnSyuusei.Width = 32
                                BtnSyuusei.Enabled = blnBtn
                                BtnAttributes(BtnSyuusei, Split(grdBody.Rows(intRow).Cells(0).Text, ":")(1), arrRowId, strKbn, "", grdBody, intRow, grdBody.Rows(intRow).Cells(intCol + 1).Text)
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(BtnSyuusei)
                                Dim btnSakujyo As New Button
                                btnSakujyo.Text = "�폜"
                                btnSakujyo.Width = 32
                                btnSakujyo.Enabled = blnBtn
                                BtnAttributes(btnSakujyo, Split(grdBody.Rows(intRow).Cells(0).Text, ":")(1), arrRowId, strKbn, "", grdBody, intRow, grdBody.Rows(intRow).Cells(intCol + 1).Text)

                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(btnSakujyo)

                            End If
                        Case 4 '�X�V��

                            grdBody.Rows(intRow).Cells(intCol).Visible = False
                    End Select
                    '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================

                Else
                    Select Case intCol
                        Case 0
                            grdBody.Rows(intRow).Cells(intCol).Attributes.Add("align", "right")
                        Case 1
                            Dim ddlControl As New TextBox


                            ddlControl.Width = Unit.Pixel(42)
                            If blnErr Then
                                strSelect = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "ddl" & strKbn & intRow)
                                strMei = Request.Form(hidFirstNameKyoutu.Value.Replace("grdBodyA", grdBody.ID) & "ctl" & Right("0" & intRow + 2, 2) & "$" & "txt" & strKbn & intRow)
                            Else
                                If grdBody.Rows(intRow).Cells(intCol).Text <> "&nbsp;" Then
                                    strSelect = Split(grdBody.Rows(intRow).Cells(intCol).Text, ":")(0)
                                    If strKbn = "1" Or strKbn = "9" Or strKbn = "3" Then
                                        Dim dtKisoSiyouTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.KisoSiyouTableDataTable
                                        dtKisoSiyouTable = TyuiJyouhouInquiryLogic.GetKisoSiyouInfo(strSelect)
                                        strMei = ""
                                        If dtKisoSiyouTable.Rows.Count > 0 Then
                                            strMei = dtKisoSiyouTable.Rows(intCount).Item("ks_siyou")
                                        End If

                                    Else
                                        Dim dtKaisyaTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.KaisyaTableDataTable
                                        dtKaisyaTable = TyuiJyouhouInquiryLogic.GetKaisyaInfo(strSelect)
                                        strMei = ""
                                        If dtKaisyaTable.Rows.Count > 0 Then
                                            strMei = dtKaisyaTable.Rows(intCount).Item("tys_mei")
                                        End If
                                    End If

                                Else
                                    strSelect = ""
                                    strMei = ""
                                End If
                            End If

                            ddlControl.Text = strSelect

                            ddlControl.Attributes.Add("Style", "ime-mode:disabled;")
                            ddlControl.ID = "ddl" & strKbn & intRow
                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(ddlControl)
                            arrRowId(0) = ddlControl.ClientID
                            Dim txtControl As New Label
                            txtControl.Width = Unit.Pixel(139)
                            txtControl.Text = strMei
                            txtControl.BackColor = Drawing.Color.Transparent
                            txtControl.BorderWidth = Unit.Pixel(0)
                            txtControl.Attributes.Add("readonly", "true")
                            txtControl.Attributes.Add("style", "vertical-align:middle;")
                            If strKbn = "11" Then
                                txtControl.Font.Bold = True
                            End If

                            txtControl.ID = "txt" & strKbn & intRow
                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(txtControl)
                            arrRowId(1) = txtControl.ClientID
                            Dim BtnKensaku As New Button
                            BtnKensaku.Text = "����"
                            BtnKensaku.Width = 32
                            BtnKensaku.Enabled = blnBtn
                            BtnAttributes(BtnKensaku, "", arrRowId, strKbn, "", grdBody, intRow, "")

                            grdBody.Rows(intRow).Cells(intCol).Controls.Add(BtnKensaku)

                            If intRow = grdBody.Rows.Count - 1 Then
                                strSelect = ""
                                strMei = ""
                                Dim BtnSinki As New Button
                                BtnSinki.Text = "�o�^"
                                BtnSinki.Width = 32
                                BtnSinki.Enabled = blnBtn
                                BtnAttributes(BtnSinki, "", arrRowId, strKbn, "", grdBody, intRow, "")

                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(BtnSinki)
                            Else

                                Dim BtnSyuusei As New Button
                                BtnSyuusei.Text = "�C��"
                                BtnSyuusei.Width = 32
                                BtnSyuusei.Enabled = blnBtn
                                BtnAttributes(BtnSyuusei, Split(grdBody.Rows(intRow).Cells(1).Text, ":")(1), arrRowId, strKbn, Split(grdBody.Rows(intRow).Cells(1).Text, ":")(0), grdBody, intRow, grdBody.Rows(intRow).Cells(intCol + 1).Text)
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(BtnSyuusei)
                                Dim btnSakujyo As New Button
                                btnSakujyo.Text = "�폜"
                                btnSakujyo.Width = 32
                                btnSakujyo.Enabled = blnBtn
                                BtnAttributes(btnSakujyo, Split(grdBody.Rows(intRow).Cells(1).Text, ":")(1), arrRowId, strKbn, Split(grdBody.Rows(intRow).Cells(1).Text, ":")(0), grdBody, intRow, grdBody.Rows(intRow).Cells(intCol + 1).Text)
                                grdBody.Rows(intRow).Cells(intCol).Controls.Add(btnSakujyo)
                            End If
                        Case 2
                            grdBody.Rows(intRow).Cells(intCol).Visible = False
                    End Select
                    'grdBody.Rows(intRow).Cells(intCol).Attributes.Add("style", "vertical-align: middle;")
                End If
            Next

        Next

    End Sub
    ''' <summary> �������Z�b�g</summary>
    Private Sub BtnAttributes(ByRef btn As Button, ByVal strCD As String, ByVal arrRowId() As String, ByVal strKbn As String, ByVal strMoto As String, ByVal grdBody As GridView, ByVal intRow As Integer, ByVal strRowTime As String)
        If btn.Text = "����" Then
            If strKbn = "1" Or strKbn = "9" Or strKbn = "3" Then
                btn.Attributes.Add("onclick", _
                                                                    "if (eval('document.all.'+'" & arrRowId(0) & "').value==''){fncOpen('����','" & arrRowId(0) & "','" & arrRowId(1) & "');return false;}ShowModal();return fncSetRowData('" & strCD & _
                                                                    "','" & arrRowId(0) & _
                                                                    "','" & arrRowId(1) & _
                                                                    "','" & arrRowId(2) & _
                                                                    "','" & arrRowId(3) & _
                                                                    "','" & strKbn & _
                                                                    "','" & btn.Text & _
                                                                    "','" & strMoto & _
                                                                    "','" & intRow & _
                                                                    "','" & strRowTime & "');")
            Else
                btn.Attributes.Add("onclick", _
                                                                    "if (eval('document.all.'+'" & arrRowId(0) & "').value==''){fncOpen('','" & arrRowId(0) & "','" & arrRowId(1) & "');return false;}ShowModal();return fncSetRowData('" & strCD & _
                                                                    "','" & arrRowId(0) & _
                                                                    "','" & arrRowId(1) & _
                                                                    "','" & arrRowId(2) & _
                                                                    "','" & arrRowId(3) & _
                                                                    "','" & strKbn & _
                                                                    "','" & btn.Text & _
                                                                    "','" & strMoto & _
                                                                    "','" & intRow & _
                                                                    "','" & strRowTime & "');")
            End If

        ElseIf btn.Text = "�폜" Then
            btn.Attributes.Add("onclick", _
                                                                "if (!confirm('�폜���܂��B')){return false;}ShowModal();return fncSetRowData('" & strCD & _
                                                                "','" & arrRowId(0) & _
                                                                "','" & arrRowId(1) & _
                                                                "','" & arrRowId(2) & _
                                                                "','" & arrRowId(3) & _
                                                                "','" & strKbn & _
                                                                "','" & btn.Text & _
                                                                "','" & strMoto & _
                                                                "','" & intRow & _
                                                                "','" & strRowTime & "');")
        Else
            '========2015/02/04 ���� 407679�̑Ή� �C����======================
            If strKbn = "TORA" AndAlso btn.Text = "�o�^" Then
                btn.Attributes.Add("onclick", _
                                                        "ShowModal();return fncSetRowDataTORA('" & strCD & _
                                                        "','" & arrRowId(0) & _
                                                        "','" & arrRowId(1) & _
                                                        "','" & arrRowId(2) & _
                                                        "','" & arrRowId(3) & _
                                                        "','" & arrRowId(4) & _
                                                        "','" & strKbn & _
                                                        "','" & btn.Text & _
                                                        "','" & strMoto & _
                                                        "','" & intRow & _
                                                        "','" & strRowTime & "');")
            Else
                btn.Attributes.Add("onclick", _
                                                        "ShowModal();return fncSetRowData('" & strCD & _
                                                        "','" & arrRowId(0) & _
                                                        "','" & arrRowId(1) & _
                                                        "','" & arrRowId(2) & _
                                                        "','" & arrRowId(3) & _
                                                        "','" & strKbn & _
                                                        "','" & btn.Text & _
                                                        "','" & strMoto & _
                                                        "','" & intRow & _
                                                        "','" & strRowTime & "');")
            End If
            'btn.Attributes.Add("onclick", _
            '                                        "ShowModal();return fncSetRowData('" & strCD & _
            '                                        "','" & arrRowId(0) & _
            '                                        "','" & arrRowId(1) & _
            '                                        "','" & arrRowId(2) & _
            '                                        "','" & arrRowId(3) & _
            '                                        "','" & strKbn & _
            '                                        "','" & btn.Text & _
            '                                        "','" & strMoto & _
            '                                        "','" & intRow & _
            '                                        "','" & strRowTime & "');")
            '========2015/02/04 ���� 407679�̑Ή� �C����======================
        End If

    End Sub
    ''' <summary> GridView�f�[�^�\�[�X���Z�b�g</summary>
    Private Sub GrdStyle(ByVal strKameitenCd As String, ByVal strKbn As String, Optional ByVal strUerId As String = "", Optional ByVal blnErr As Boolean = False)
        Dim intWidth(5) As String
        intWidth(0) = "19px"
        intWidth(1) = "287px"
        intWidth(2) = "0px"
        Select Case strKbn
            Case "11"

                grdNaiyou11.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou11.DataBind()
                GrdViewStyle(intWidth, grdNaiyou11)
                GrdViewControl(grdNaiyou11, strKbn, blnErr)

            Case "19"

                grdNaiyou19.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou19.DataBind()
                GrdViewStyle(intWidth, grdNaiyou19)
                GrdViewControl(grdNaiyou19, strKbn, blnErr)
            Case "21"
                intWidth(1) = "286px"
                grdNaiyou21.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou21.DataBind()
                GrdViewStyle(intWidth, grdNaiyou21)
                GrdViewControl(grdNaiyou21, strKbn, blnErr)
            Case "23"
                intWidth(1) = "286px"
                grdNaiyou23.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou23.DataBind()
                GrdViewStyle(intWidth, grdNaiyou23)
                GrdViewControl(grdNaiyou23, strKbn, blnErr)
            Case "29"
                intWidth(1) = "286px"
                grdNaiyou29.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou29.DataBind()
                GrdViewStyle(intWidth, grdNaiyou29)
                GrdViewControl(grdNaiyou29, strKbn, blnErr)
            Case "1"
                grdNaiyou1.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou1.DataBind()
                GrdViewStyle(intWidth, grdNaiyou1)
                GrdViewControl(grdNaiyou1, strKbn, blnErr)

            Case "9"

                grdNaiyou9.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou9.DataBind()
                GrdViewStyle(intWidth, grdNaiyou9)
                GrdViewControl(grdNaiyou9, strKbn, blnErr)
            Case "3"
                grdNaiyou3.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou3.DataBind()
                GrdViewStyle(intWidth, grdNaiyou3)
                GrdViewControl(grdNaiyou3, strKbn, blnErr)

            Case "13"

                grdNaiyou13.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, strKbn)
                grdNaiyou13.DataBind()
                GrdViewStyle(intWidth, grdNaiyou13)
                GrdViewControl(grdNaiyou13, strKbn, blnErr)

            Case "A", "B"
                intWidth(0) = "154px"
                intWidth(1) = "73px"
                intWidth(2) = "141px"
                intWidth(3) = ""
                intWidth(4) = ""
                If Not blnErr Then '
                    dmp = CreateHeadDataSource(strKameitenCd, strUerId, "A")
                    grdBodyA.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, "A")
                    grdBodyA.DataBind()
                    GrdViewStyle(intWidth, grdBodyA)
                End If
                strKbn = "A"
                GrdViewControl(grdBodyA, strKbn, blnErr)
                strKbn = "B"
                If Not blnErr Then '
                    dmp = CreateHeadDataSource(strKameitenCd, strUerId, "B")
                    grdBodyB.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, "B")
                    grdBodyB.DataBind()
                    GrdViewStyle(intWidth, grdBodyB)
                End If

                GrdViewControl(grdBodyB, strKbn, blnErr)

                '========2015/02/03 ���� 407679�̑Ή� �ǉ���======================
            Case "TORA"
                intWidth(0) = "154px"
                intWidth(1) = "73px"
                intWidth(2) = "141px"
                intWidth(3) = ""
                intWidth(4) = ""
                If Not blnErr Then
                    dmp = CreateHeadDataSource(strKameitenCd, strUerId, "TORA")
                    grdbodyTORA.DataSource = CreateHeadDataSource(strKameitenCd, strUerId, "TORA")
                    grdbodyTORA.DataBind()
                    GrdViewStyle(intWidth, grdbodyTORA)
                End If
                strKbn = "TORA"
                GrdViewControl(grdbodyTORA, strKbn, blnErr)
                '========2015/02/03 ���� 407679�̑Ή� �ǉ���======================
        End Select
    End Sub
    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("function fncOpen(strkbn,objCd,objMei,show)")
            .AppendLine("{")
            .AppendLine("   if(strkbn==''){")
            .AppendLine("objSrchWin = window.open('search_tyousa.aspx?show='+ show+'&Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd='+ objCd +'&objMei='+objMei+'&strCd='+escape(eval('document.all.'+objCd).value)+'&strMei='+escape(eval('document.all.'+objMei).value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("   }else{")
            .AppendLine("objSrchWin = window.open('search_common.aspx?show='+ show+'&Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd='+ objCd +'&objMei='+objMei+'&strCd='+escape(eval('document.all.'+objCd).value)+'&strMei='+escape(eval('document.all.'+objMei).value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}")
            .AppendLine("}")
            .AppendLine("function fncGamenSenni()")
            .AppendLine("{")
            .AppendLine("   var strKameitenCd = '" & Me.lblKyoutuKameitenCd.Text.Trim & "';")
            .AppendLine("   window.open('HanbaiKakakuMasterSearchList.aspx?sendSearchTerms='+'1'+'$$$'+strKameitenCd);")
            .AppendLine("}")
            .AppendLine("function ShowModal()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")

            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("   if(buyDiv.style.display=='none')")
            .AppendLine("   {")
            .AppendLine("    buyDiv.style.display='';")
            .AppendLine("    disable.style.display='';")
            .AppendLine("    disable.focus();")
            .AppendLine("   }else{")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("}")


            .AppendLine("function closecover()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")

            .AppendLine("}")

            .AppendLine("function  fncSetRowData(strCD,objDdlId,objDate,objMei,objNaiyou,strKbn,strBtnMei,strMoto,intRow,strRowTime)")
            .AppendLine("{")
            .AppendLine("document.getElementById ('" & hidRow.ClientID & "').value=intRow;")
            .AppendLine("document.getElementById ('" & hidNo.ClientID & "').value=strCD;")
            .AppendLine("if (strBtnMei=='�o�^'){")
            .AppendLine("document.getElementById ('" & hidDdl.ClientID & "').value=eval('document.all.'+objDdlId).value+':'+eval('document.all.'+objDdlId).value;")
            .AppendLine("}else{")
            .AppendLine("document.getElementById ('" & hidDdl.ClientID & "').value=eval('document.all.'+objDdlId).value+':'+strMoto;")
            .AppendLine("}")

            .AppendLine("if (objDate!='' && objMei!=''){")

            .AppendLine("document.getElementById ('" & hidDate.ClientID & "').value=eval('document.all.'+objDate).value;")
            .AppendLine("document.getElementById ('" & hidMei.ClientID & "').value=eval('document.all.'+objMei).value;")
            .AppendLine("document.getElementById ('" & hidNaiyou.ClientID & "').value=(eval('document.all.'+objNaiyou).value);")
            .AppendLine("}")

            .AppendLine("document.getElementById ('" & hidKbn.ClientID & "').value=strKbn;")
            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtnMei;")
            .AppendLine("document.getElementById ('" & hidRowTime.ClientID & "').value=strRowTime;")
            'A  =�D�撍�ӎ���
            'B  =�ʏ풍�ӎ���
            '11 =�w�� �������
            '19 =�֎~ �������
            '21 =�w�� �H�����
            '29 =�֎~ �H�����
            '1  =�w�� ����
            '9  =�֎~ ����
            .AppendLine("if (strKbn=='A' || strKbn=='B'){")
            .AppendLine("document.getElementById ('" & btnA.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='11'){")
            .AppendLine("document.getElementById ('" & btn11.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='19'){")
            .AppendLine("document.getElementById ('" & btn19.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='21'){")
            .AppendLine("document.getElementById ('" & btn21.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='29'){")
            .AppendLine("document.getElementById ('" & btn29.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='1'){")
            .AppendLine("document.getElementById ('" & btn1.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='9'){")
            .AppendLine("document.getElementById ('" & btn2.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='13'){")
            .AppendLine("document.getElementById ('" & btn13.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='23'){")
            .AppendLine("document.getElementById ('" & btn23.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("if (strKbn=='3'){")
            .AppendLine("document.getElementById ('" & btn3.ClientID & "').click();")
            .AppendLine("}")

            '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
            .AppendLine("if (strKbn=='TORA'){")
            .AppendLine("document.getElementById ('" & btnTORA.ClientID & "').click();")
            .AppendLine("}")
            '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================

            .AppendLine("return false;")
            .AppendLine("}")
            '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
            .AppendLine("function  fncSetRowDataTORA(strCD,objDdlId,objDate,objMei,objddlNaiyou,objNaiyou,strKbn,strBtnMei,strMoto,intRow,strRowTime)")
            .AppendLine("{")
            .AppendLine("document.getElementById ('" & hidRow.ClientID & "').value=intRow;")
            .AppendLine("document.getElementById ('" & hidNo.ClientID & "').value=strCD;")
            .AppendLine("if (strBtnMei=='�o�^'){")
            .AppendLine("document.getElementById ('" & hidDdl.ClientID & "').value=eval('document.all.'+objDdlId).value+':'+eval('document.all.'+objDdlId).value;")
            .AppendLine("}else{")
            .AppendLine("document.getElementById ('" & hidDdl.ClientID & "').value=eval('document.all.'+objDdlId).value+':'+strMoto;")
            .AppendLine("}")

            .AppendLine("if (objDate!='' && objMei!=''){")

            .AppendLine("document.getElementById ('" & hidDate.ClientID & "').value=eval('document.all.'+objDate).value;")
            .AppendLine("document.getElementById ('" & hidMei.ClientID & "').value=eval('document.all.'+objMei).value;")

            .AppendLine("if (eval('document.all.'+objNaiyou).value==''){")
            .AppendLine("document.getElementById ('" & hidNaiyou.ClientID & "').value=eval('document.all.'+objddlNaiyou).value;")
            .AppendLine("} else {")
            .AppendLine("document.getElementById ('" & hidNaiyou.ClientID & "').value=eval('document.all.'+objddlNaiyou).value + ' ' +(eval('document.all.'+objNaiyou).value);")
            .AppendLine("}")

            .AppendLine("}")
            .AppendLine("document.getElementById ('" & hidKbn.ClientID & "').value=strKbn;")
            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtnMei;")
            .AppendLine("document.getElementById ('" & hidRowTime.ClientID & "').value=strRowTime;")

            .AppendLine("if (strKbn=='TORA'){")
            .AppendLine("document.getElementById ('" & btnTORA.ClientID & "').click();")
            .AppendLine("}")

            .AppendLine("return false;")
            .AppendLine("}")
            '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
            .Append("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub
    ''' <summary>�{�^���̏���</summary>
    Protected Sub btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnA.Click, btn11.Click, btn19.Click, btn21.Click, btn29.Click, btn1.Click, btn2.Click, btn13.Click, btn23.Click, btn3.Click, btnTORA.Click
        Dim csScript As New StringBuilder
        Dim intIndex As Integer = -1
        Dim tmpScript As String = ""
        Dim strReturn As String = ""
        Dim strCaption As String = ""
        Dim strFocusID As String = ""
        If hidBtn.Value = "����" Then
            tmpScript = CheckInput(intIndex, True).ToString
        ElseIf hidBtn.Value <> "�폜" Then
            tmpScript = CheckInput(intIndex).ToString
        End If

        If tmpScript <> "" Then
            tmpScript = "alert('" & tmpScript & "');"
            csScript.Append(tmpScript)
            GrdStyle(ViewState("KameitenCd"), hidKbn.Value, ViewState("UserId"), True)


            strFocusID = GetFocusID(hidKbn.Value, hidRow.Value, intIndex)
            If (hidKbn.Value = "A" Or hidKbn.Value = "B") And (intIndex <> 0) Then
                csScript.Append("if(document.getElementById('" & strFocusID & "').type != ""submit"") document.getElementById('" & strFocusID & "').select();")
            Else
                csScript.Append("if(document.getElementById('" & strFocusID & "').type != ""submit"") document.getElementById('" & strFocusID & "').select();")
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.ToString, True)
            PanelUPD(hidKbn.Value)
            Return
        Else
            If hidBtn.Value = "����" Then
                Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
                Dim grdview As New GridView

                Select Case hidKbn.Value
                    Case "11"
                        grdview = grdNaiyou11
                    Case "19"
                        grdview = grdNaiyou19
                    Case "21"
                        grdview = grdNaiyou21
                    Case "29"
                        grdview = grdNaiyou29
                    Case "1"
                        grdview = grdNaiyou1
                    Case "9"
                        grdview = grdNaiyou9
                    Case "3"
                        grdview = grdNaiyou3
                    Case "13"
                        grdview = grdNaiyou13
                    Case "23"
                        grdview = grdNaiyou23
                End Select
                GrdStyle(ViewState("KameitenCd"), hidKbn.Value, ViewState("UserId"), False)
                If hidKbn.Value.Length = 2 Then
                    Dim dtKaisyaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.tyousakaisyaTableDataTable
                    dtKaisyaTable = CommonSearchLogic.GetTyousaInfo("2", Split(hidDdl.Value, ":")(0), "", "")
                    If dtKaisyaTable.Rows.Count = 1 Then

                        CType(grdview.Rows(hidRow.Value).Cells(1).Controls(0), TextBox).Text = dtKaisyaTable.Rows(0).Item(0).ToString + dtKaisyaTable.Rows(0).Item(1).ToString
                        CType(grdview.Rows(hidRow.Value).Cells(1).Controls(1), Label).Text = dtKaisyaTable.Rows(0).Item(2).ToString
                    Else
                        CType(grdview.Rows(hidRow.Value).Cells(1).Controls(0), TextBox).Text = Split(hidDdl.Value, ":")(0)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("fncOpen('','" & grdview.Rows(hidRow.Value).Cells(1).Controls(0).ClientID & "','" & grdview.Rows(hidRow.Value).Cells(1).Controls(1).ClientID & "','True');").ToString, True)
                    End If
                Else
                    Dim dtKisoSiyouTable As New Itis.Earth.DataAccess.CommonSearchDataSet.IntTableDataTable
                    dtKisoSiyouTable = CommonSearchLogic.SelSiyouInfo(2, Split(hidDdl.Value, ":")(0), "")
                    If dtKisoSiyouTable.Rows.Count = 1 Then
                        CType(grdview.Rows(hidRow.Value).Cells(1).Controls(0), TextBox).Text = dtKisoSiyouTable.Rows(0).Item(0).ToString
                        CType(grdview.Rows(hidRow.Value).Cells(1).Controls(1), Label).Text = dtKisoSiyouTable.Rows(0).Item(1).ToString
                    Else
                        CType(grdview.Rows(hidRow.Value).Cells(1).Controls(0), TextBox).Text = Split(hidDdl.Value, ":")(0)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("fncOpen('����','" & grdview.Rows(hidRow.Value).Cells(1).Controls(0).ClientID & "','" & grdview.Rows(hidRow.Value).Cells(1).Controls(1).ClientID & "','True');").ToString, True)
                    End If
                End If

                PanelUPD(hidKbn.Value)
                Return
            End If

            strReturn = TableUpdate(ViewState("KameitenCd"), hidBtn.Value, hidKbn.Value, ViewState("UserId"), hidRowTime.Value, CInt(hidRow.Value) + 1)

            If strReturn <> "" Then
                If hidBtn.Value = "�o�^" Or strReturn = "E" Then
                    Select Case hidKbn.Value
                        Case "A", "B"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "���ӎ���", "���ӎ���") & "')").ToString, True)
                        Case "1"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�w�蔻��", "�w�蔻��") & "')").ToString, True)
                        Case "9"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�֎~����", "�֎~����") & "')").ToString, True)
                        Case "3"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�D�攻��", "�D�攻��") & "')").ToString, True)

                        Case "11"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�w�蒲�����", "�w�蒲�����") & "')").ToString, True)
                        Case "19"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�����֎~(NG)�������", "�����֎~(NG)�������") & "')").ToString, True)
                        Case "13"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�D�撲�����", "�D�撲�����") & "')").ToString, True)
                        Case "21"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�w��H�����", "�w��H�����") & "')").ToString, True)
                        Case "29"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�����֎~(NG)�H�����", "�����֎~(NG)�H�����") & "')").ToString, True)
                        Case "23"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�D��H�����", "�D��H�����") & "')").ToString, True)
                            '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
                        Case "TORA"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG2029E, "�g���u���E�N���[�����", "�g���u���E�N���[�����") & "')").ToString, True)
                            '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
                    End Select
                    GrdStyle(ViewState("KameitenCd"), hidKbn.Value, ViewState("UserId"), True)

                ElseIf strReturn = "H" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & Messages.Instance.MSG2009E & "');").ToString, True)
                    GrdStyle(ViewState("KameitenCd"), hidKbn.Value, ViewState("UserId"), False)

                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG003E, Split(strReturn, ",")(0), Split(strReturn, ",")(1)) & "');").ToString, True)
                    GrdStyle(ViewState("KameitenCd"), hidKbn.Value, ViewState("UserId"), False)

                End If
                PanelUPD(hidKbn.Value)
                Return
            Else
                Select Case hidKbn.Value
                    Case "11"
                        strCaption = "�w�蒲�����"
                    Case "19"
                        strCaption = "�����֎~(NG)�������"
                    Case "13"
                        strCaption = "�D�撲�����"
                    Case "21"
                        strCaption = "�w��H�����"
                    Case "29"
                        strCaption = "�����֎~(NG)�H�����"
                    Case "23"
                        strCaption = "�D��H�����"
                    Case "1"
                        strCaption = "�w�蔻��"
                    Case "9"
                        strCaption = "�֎~����"
                    Case "3"
                        strCaption = "�D�攻��"
                    Case "A"
                        strCaption = "�D�撍�ӎ���"
                    Case "B"
                        strCaption = "�ʏ풍�ӎ���"
                        '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
                    Case "TORA"
                        strCaption = "�g���u���E�N���[�����"
                        '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
                End Select
                csScript.Append("alert('" & Messages.Instance.MSG018S.Replace("@PARAM1", strCaption) & "');")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.ToString, True)
                Dim dtKyoutuuJyouhouTable As New Itis.Earth.DataAccess.KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
                Dim KyoutuuJyouhouLogic As New Itis.Earth.BizLogic.KyoutuuJyouhouLogic
                dtKyoutuuJyouhouTable = KyoutuuJyouhouLogic.GetKameitenInfo(ViewState("KameitenCd"))
                If dtKyoutuuJyouhouTable.Rows.Count > 0 Then
                    lblHi.Text = CDate(dtKyoutuuJyouhouTable.Rows(0).Item("sansyou_date")).ToString("yyyy/MM/dd HH:mm:ss")
                    UpdatePanel2.Update()
                End If
            End If
            GrdStyle(ViewState("KameitenCd"), hidKbn.Value, ViewState("UserId"))
            PanelUPD(hidKbn.Value)
        End If
    End Sub
    Sub PanelUPD(ByVal strKBN As String)
        Select Case strKBN
            Case "11"

                GrdStyle(ViewState("KameitenCd"), "19", ViewState("UserId"))
                UpdatePanel19.Update()
                GrdStyle(ViewState("KameitenCd"), "21", ViewState("UserId"))
                UpdatePanel21.Update()
                GrdStyle(ViewState("KameitenCd"), "29", ViewState("UserId"))
                UpdatePanel29.Update()
                GrdStyle(ViewState("KameitenCd"), "13", ViewState("UserId"))
                UpdatePanel13.Update()
                GrdStyle(ViewState("KameitenCd"), "23", ViewState("UserId"))
                UpdatePanel23.Update()
            Case "19"
                GrdStyle(ViewState("KameitenCd"), "11", ViewState("UserId"))
                UpdatePanel11.Update()
                GrdStyle(ViewState("KameitenCd"), "21", ViewState("UserId"))
                UpdatePanel21.Update()
                GrdStyle(ViewState("KameitenCd"), "29", ViewState("UserId"))
                UpdatePanel29.Update()
                GrdStyle(ViewState("KameitenCd"), "13", ViewState("UserId"))
                UpdatePanel13.Update()
                GrdStyle(ViewState("KameitenCd"), "23", ViewState("UserId"))
                UpdatePanel23.Update()
            Case "21"
                GrdStyle(ViewState("KameitenCd"), "11", ViewState("UserId"))
                UpdatePanel11.Update()
                GrdStyle(ViewState("KameitenCd"), "19", ViewState("UserId"))
                UpdatePanel19.Update()
                GrdStyle(ViewState("KameitenCd"), "29", ViewState("UserId"))
                UpdatePanel29.Update()
                GrdStyle(ViewState("KameitenCd"), "13", ViewState("UserId"))
                UpdatePanel13.Update()
                GrdStyle(ViewState("KameitenCd"), "23", ViewState("UserId"))
                UpdatePanel23.Update()
            Case "29"
                GrdStyle(ViewState("KameitenCd"), "11", ViewState("UserId"))
                UpdatePanel11.Update()
                GrdStyle(ViewState("KameitenCd"), "19", ViewState("UserId"))
                UpdatePanel19.Update()
                GrdStyle(ViewState("KameitenCd"), "21", ViewState("UserId"))
                UpdatePanel21.Update()
                GrdStyle(ViewState("KameitenCd"), "13", ViewState("UserId"))
                UpdatePanel13.Update()
                GrdStyle(ViewState("KameitenCd"), "23", ViewState("UserId"))
                UpdatePanel23.Update()
            Case "13"
                GrdStyle(ViewState("KameitenCd"), "11", ViewState("UserId"))
                UpdatePanel11.Update()
                GrdStyle(ViewState("KameitenCd"), "19", ViewState("UserId"))
                UpdatePanel19.Update()
                GrdStyle(ViewState("KameitenCd"), "21", ViewState("UserId"))
                UpdatePanel21.Update()
                GrdStyle(ViewState("KameitenCd"), "29", ViewState("UserId"))
                UpdatePanel29.Update()
                GrdStyle(ViewState("KameitenCd"), "23", ViewState("UserId"))
                UpdatePanel23.Update()
            Case "23"
                GrdStyle(ViewState("KameitenCd"), "11", ViewState("UserId"))
                UpdatePanel11.Update()
                GrdStyle(ViewState("KameitenCd"), "19", ViewState("UserId"))
                UpdatePanel19.Update()
                GrdStyle(ViewState("KameitenCd"), "21", ViewState("UserId"))
                UpdatePanel21.Update()
                GrdStyle(ViewState("KameitenCd"), "29", ViewState("UserId"))
                UpdatePanel29.Update()
                GrdStyle(ViewState("KameitenCd"), "13", ViewState("UserId"))
                UpdatePanel13.Update()

            Case Else
                GrdStyle(ViewState("KameitenCd"), "11", ViewState("UserId"))
                UpdatePanel11.Update()
                GrdStyle(ViewState("KameitenCd"), "19", ViewState("UserId"))
                UpdatePanel19.Update()
                GrdStyle(ViewState("KameitenCd"), "21", ViewState("UserId"))
                UpdatePanel21.Update()
                GrdStyle(ViewState("KameitenCd"), "29", ViewState("UserId"))
                UpdatePanel29.Update()
                GrdStyle(ViewState("KameitenCd"), "13", ViewState("UserId"))
                UpdatePanel13.Update()
                GrdStyle(ViewState("KameitenCd"), "23", ViewState("UserId"))
                UpdatePanel23.Update()
        End Select
    End Sub
    ''' <summary>ClientID���擾</summary>
    ''' <returns>ClientID�̕�����</returns>
    Function GetFocusID(ByVal strKbn As String, ByVal strRow As Integer, ByVal strCol As Integer) As String
        Dim grdview As New GridView
        Dim strClientID As String
        Select Case strKbn
            Case "11"
                grdview = grdNaiyou11
            Case "19"
                grdview = grdNaiyou19
            Case "21"
                grdview = grdNaiyou21
            Case "29"
                grdview = grdNaiyou29
            Case "1"
                grdview = grdNaiyou1
            Case "9"
                grdview = grdNaiyou9
            Case "3"
                grdview = grdNaiyou3
            Case "13"
                grdview = grdNaiyou13
            Case "23"
                grdview = grdNaiyou23
            Case "A"
                grdview = grdBodyA
                '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
            Case "TORA"
                grdview = grdbodyTORA
                '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
            Case Else
                grdview = grdBodyB
        End Select
        If strRow > grdview.Rows.Count - 1 Then
            strRow = grdview.Rows.Count - 1
        End If
        If (strKbn = "A" Or strKbn = "B") And (strCol <> 0) Then
            strClientID = CType(grdview.Rows(strRow).Cells(strCol).Controls(0), TextBox).ClientID
            '========2015/02/04 ���� 407679�̑Ή� �C����======================
        ElseIf strKbn = "TORA" AndAlso strCol <> 0 Then
            If strRow <> grdview.Rows.Count - 1 Then
                strClientID = CType(grdview.Rows(strRow).Cells(strCol).Controls(0), TextBox).ClientID
            Else
                If strCol = 3 Then
                    strClientID = CType(grdview.Rows(strRow).Cells(strCol).Controls(1), TextBox).ClientID
                Else
                    strClientID = CType(grdview.Rows(strRow).Cells(strCol).Controls(0), TextBox).ClientID
                End If
            End If
            '========2015/02/04 ���� 407679�̑Ή� �C����======================
        Else
            strClientID = CType(grdview.Rows(strRow).Cells(strCol + 1).Controls(0), TextBox).ClientID
        End If
        Return strClientID
    End Function
    ''' <summary>���̓`�F�b�N</summary>
    ''' <returns>�G���[���b�Z�[�W</returns>
    Public Function CheckInput(ByRef strId As String, Optional ByVal bExist As Boolean = False) As StringBuilder
        Dim jBn As New Jiban
        Dim csScript As New StringBuilder
        Dim strCaption As String = ""
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""

        With csScript
            Select Case hidKbn.Value
                '========2015/02/04 ���� 407679�̑Ή� �C����======================
                'Case "A", "B"
                Case "A", "B", "TORA"
                    '========2015/02/04 ���� 407679�̑Ή� �C����======================

                    Select Case hidBtn.Value

                        Case "�o�^", "�C��"

                            '���
                            strErr = commoncheck.CheckHissuNyuuryoku(hidDdl.Value.Replace(":", ""), "���" & hidRow.Value + 1 & "�s��")
                            If strErr <> "" Then
                                csScript.Append(strErr)
                                If strId = -1 Then
                                    strId = "0"
                                End If
                            End If
                            '���͓�
                            If hidDate.Value <> "" Then
                                strErr = commoncheck.CheckYuukouHiduke(hidDate.Value, "���͓�" & hidRow.Value + 1 & "�s��")
                                If strErr <> "" Then
                                    csScript.Append(strErr)
                                    If strId = -1 Then
                                        strId = "1"
                                    End If
                                End If
                            Else
                                strErr = commoncheck.CheckHissuNyuuryoku(hidDate.Value, "���͓�" & hidRow.Value + 1 & "�s��")
                                If strErr <> "" Then
                                    csScript.Append(strErr)
                                    If strId = -1 Then
                                        strId = "1"
                                    End If
                                End If
                            End If

                            '��t��
                            strErr = commoncheck.CheckHissuNyuuryoku(hidMei.Value, "��t��" & hidRow.Value + 1 & "�s��")
                            If strErr <> "" Then
                                csScript.Append(strErr)
                                If strId = -1 Then
                                    strId = "2"
                                End If
                            End If
                            strErr = commoncheck.CheckByte(hidMei.Value, 20, "��t��" & hidRow.Value + 1 & "�s��", kbn.ZENKAKU)
                            If strErr <> "" Then
                                csScript.Append(strErr)
                                If strId = -1 Then
                                    strId = "2"
                                End If
                            End If
                            strErr = commoncheck.CheckKinsoku(hidMei.Value, "��t��" & hidRow.Value + 1 & "�s��")
                            If strErr <> "" Then
                                csScript.Append(strErr)
                                If strId = -1 Then
                                    strId = "2"
                                End If
                            End If

                            '���e
                            If hidNaiyou.Value <> "" Then

                                '========2015/02/10 ���� 407679�̑Ή� �ǉ���======================
                                '�V�K�̏ꍇ�A���e��v���_�E���K�{�`�F�b�N��ǉ�����
                                If hidKbn.Value = "TORA" Then
                                    If Left(hidNaiyou.Value, 1) = ":" Then
                                        strErr = commoncheck.CheckHissuNyuuryoku("", "���e�v���_�E��" & hidRow.Value + 1 & "�s��")
                                        If strErr <> "" Then
                                            csScript.Append(strErr)
                                            If strId = -1 Then
                                                strId = "3"
                                            End If
                                        End If
                                    End If
                                End If
                                '========2015/02/10 ���� 407679�̑Ή� �ǉ���======================

                                strErr = commoncheck.CheckByte(hidNaiyou.Value, 80, "���e" & hidRow.Value + 1 & "�s��", kbn.ZENKAKU)
                                If strErr <> "" Then
                                    csScript.Append(strErr)
                                    If strId = -1 Then
                                        strId = "3"
                                    End If
                                End If
                                strErr = commoncheck.CheckKinsoku(hidNaiyou.Value, "���e" & hidRow.Value + 1 & "�s��")
                                If strErr <> "" Then
                                    csScript.Append(strErr)
                                    If strId = -1 Then
                                        strId = "3"
                                    End If
                                End If

                            End If
                    End Select
                Case Else

                    Select Case hidKbn.Value
                        Case 11
                            strCaption = "�w�蒲����ЃR�[�h"
                        Case 19
                            strCaption = "�����֎~(NG)������Љ�ЃR�[�h"
                        Case 21
                            strCaption = "�w��H����ЃR�[�h"
                        Case 29
                            strCaption = "�����֎~(NG)�H����ЃR�[�h"
                        Case 1
                            strCaption = "�w���b�d�lNO"
                        Case 9
                            strCaption = "�֎~��b�d�lNO"
                        Case 3
                            strCaption = "�D���b�d�lNO"
                        Case 13
                            strCaption = "�D�撲����ЃR�[�h"
                        Case 23
                            strCaption = "�D��H����ЃR�[�h"
                    End Select
                    strErr = commoncheck.CheckHissuNyuuryoku(Split(hidDdl.Value, ":")(0), strCaption & hidRow.Value + 1 & "�s��")
                    If strErr <> "" Then
                        csScript.Append(strErr)
                        If strId = -1 Then
                            strId = "0"
                        End If
                    End If
                    Dim TyuiJyouhouInquiryLogic As New Itis.Earth.BizLogic.TyuiJyouhouInquiryLogic
                    If hidKbn.Value.Length = 2 Then
                        If strErr = "" Then
                            strErr = commoncheck.CheckByte(Split(hidDdl.Value, ":")(0), 7, strCaption & hidRow.Value + 1 & "�s��", kbn.HANKAKU)

                            csScript.Append(strErr)
                            If strId = -1 Then
                                strId = "0"
                            End If
                        End If
                        If strErr = "" And Not bExist Then
                            Dim dtKaisyaTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.KaisyaTableDataTable
                            dtKaisyaTable = TyuiJyouhouInquiryLogic.GetKaisyaInfo(Split(hidDdl.Value, ":")(0))

                            If dtKaisyaTable.Rows.Count = 0 Then
                                csScript.Append(String.Format(Messages.Instance.MSG2008E, strCaption))
                                If strId = -1 Then
                                    strId = "0"
                                End If
                            End If

                        End If
                    Else
                        If strErr = "" Then
                            strErr = commoncheck.CheckByte(Split(hidDdl.Value, ":")(0), 10, strCaption & hidRow.Value + 1 & "�s��", kbn.HANKAKU)
                            csScript.Append(strErr)
                            If strId = -1 Then
                                strId = "0"
                            End If

                            strErr = commoncheck.CheckHankaku(Split(hidDdl.Value, ":")(0), strCaption & hidRow.Value + 1 & "�s��", "1")
                            If strErr <> "" Then
                                csScript.Append(strErr)
                                If strId = -1 Then
                                    strId = "0"
                                End If
                            End If
                        End If
                        If strErr = "" And Not bExist Then
                            Dim dtKisoSiyouTable As New Itis.Earth.DataAccess.TyuiJyouhouDataSet.KisoSiyouTableDataTable
                            dtKisoSiyouTable = TyuiJyouhouInquiryLogic.GetKisoSiyouInfo(Split(hidDdl.Value, ":")(0))
                            If dtKisoSiyouTable.Rows.Count = 0 Then
                                csScript.Append(String.Format(Messages.Instance.MSG2008E, strCaption))
                                If strId = -1 Then
                                    strId = "0"
                                End If
                            End If

                        End If
                    End If

            End Select
        End With
        Return csScript
    End Function
    ''' <summary>�󔒕����̍폜����</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    ''' <summary>��{���{�^�����N���b�N��</summary>
    Protected Sub btnTyuiJyouhouInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTyuiJyouhouInquiry.Click
        Server.Transfer("KihonJyouhouInquiry.aspx?strKameitenCd=" & ViewState("KameitenCd"))
    End Sub

    ''' <summary>�������{�^�����N���b�N��</summary>
    Protected Sub btnBukkenJyouhouInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBukkenJyouhouInquiry.Click
        Server.Transfer("BukkenJyouhouInquiry.aspx?strKameitenCd=" & ViewState("KameitenCd"))
    End Sub
    ''' <summary>�^�M���{�^�����N���b�N��</summary>
    Protected Sub btnYosinJyouhouDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYosinJyouhouDetails.Click
        Server.Transfer("YosinJyouhouInquiry.aspx?strKameitenCd=" & ViewState("KameitenCd"))
    End Sub

    ''' <summary>
    ''' �x�������i�����j�{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.07</history>
    Private Sub btnSiharaiTyousa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiharaiTyousa.Click

        Dim strTys_seikyuu_saki_cd As String         '����������R�[�h tys_seikyuu_saki_cd
        Dim strTys_seikyuu_saki_brc As String        '����������}�� tys_seikyuu_saki_brc 
        Dim strTys_seikyuu_saki_kbn As String        '����������敪 tys_seikyuu_saki_kbn

        '�����X�}�X�^�Ńf�[�^���擾����
        Dim dtTyousaJyouhou As Data.DataTable
        dtTyousaJyouhou = KihonJyouhouBL.GetKameiten(ViewState("KameitenCd").ToString)
        If dtTyousaJyouhou.Rows.Count > 0 Then
            strTys_seikyuu_saki_cd = dtTyousaJyouhou.Rows(0).Item("tys_seikyuu_saki_cd").ToString
            strTys_seikyuu_saki_brc = dtTyousaJyouhou.Rows(0).Item("tys_seikyuu_saki_brc").ToString
            strTys_seikyuu_saki_kbn = dtTyousaJyouhou.Rows(0).Item("tys_seikyuu_saki_kbn").ToString
        Else
            strTys_seikyuu_saki_cd = String.Empty
            strTys_seikyuu_saki_brc = String.Empty
            strTys_seikyuu_saki_kbn = String.Empty
        End If

        '������A�f�[�^����������ARadioButton���N�b���N�O�A�f�[�^���폜���ꂽ
        If strTys_seikyuu_saki_kbn.Trim = "" OrElse strTys_seikyuu_saki_cd.Trim = "" OrElse strTys_seikyuu_saki_brc.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_Allinput", "alert('�������񂪐ݒ肳��Ă��܂���B\r\n���������͂��ĉ������B');", True)
            Exit Sub
        End If

        '������}�X�^����f�[�^���擾����
        Dim SKU_datatable As Data.DataTable
        SKU_datatable = KihonJyouhouBL.GetSeikyusaki(strTys_seikyuu_saki_kbn, strTys_seikyuu_saki_cd, strTys_seikyuu_saki_brc, 0, False)

        If SKU_datatable.Rows.Count = 1 Then
            '������}�X�^�����e�i���X��ʂ��J��
            Dim strScript As String
            strScript = "objSrchWin = window.open('SeikyuuSakiMaster.aspx?sendSearchTerms='+escape('" & strTys_seikyuu_saki_cd & "')+'$$$'+escape('" & strTys_seikyuu_saki_brc & "')+'$$$'+escape('" & strTys_seikyuu_saki_kbn & "')+'$$$'+escape('1'), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_seikyuInputPage", strScript, True)
        Else
            '��L�ȊO
            '���b�Z�[�W�u�����悪���݂��܂���v��\�����A�������f	
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_Syousai", "alert('�����悪���݂��܂���B');", True)
        End If

    End Sub

    ''' <summary>
    ''' �x�������i�H���j�{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.07</history>
    Private Sub btnSiharaiKouji_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiharaiKouji.Click

        Dim strTys_seikyuu_saki_cd As String         '�H��������R�[�h koj_seikyuu_saki_cd
        Dim strTys_seikyuu_saki_brc As String        '�H��������}��   koj_seikyuu_saki_brc 
        Dim strTys_seikyuu_saki_kbn As String        '�H��������敪   koj_seikyuu_saki_kbn

        '�����X�}�X�^�Ńf�[�^���擾����
        Dim dtTyousaJyouhou As Data.DataTable
        dtTyousaJyouhou = KihonJyouhouBL.GetKameiten(ViewState("KameitenCd").ToString)
        If dtTyousaJyouhou.Rows.Count > 0 Then
            strTys_seikyuu_saki_cd = dtTyousaJyouhou.Rows(0).Item("koj_seikyuu_saki_cd").ToString
            strTys_seikyuu_saki_brc = dtTyousaJyouhou.Rows(0).Item("koj_seikyuu_saki_brc").ToString
            strTys_seikyuu_saki_kbn = dtTyousaJyouhou.Rows(0).Item("koj_seikyuu_saki_kbn").ToString
        Else
            strTys_seikyuu_saki_cd = String.Empty
            strTys_seikyuu_saki_brc = String.Empty
            strTys_seikyuu_saki_kbn = String.Empty
        End If

        '������A�f�[�^����������ARadioButton���N�b���N�O�A�f�[�^���폜���ꂽ
        If strTys_seikyuu_saki_kbn.Trim = "" OrElse strTys_seikyuu_saki_cd.Trim = "" OrElse strTys_seikyuu_saki_brc.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_Allinput", "alert('�������񂪐ݒ肳��Ă��܂���B\r\n���������͂��ĉ������B');", True)
            Exit Sub
        End If

        '������}�X�^����f�[�^���擾����
        Dim SKU_datatable As Data.DataTable
        SKU_datatable = KihonJyouhouBL.GetSeikyusaki(strTys_seikyuu_saki_kbn, strTys_seikyuu_saki_cd, strTys_seikyuu_saki_brc, 0, False)

        If SKU_datatable.Rows.Count = 1 Then
            '������}�X�^�����e�i���X��ʂ��J��
            Dim strScript As String
            strScript = "objSrchWin = window.open('SeikyuuSakiMaster.aspx?sendSearchTerms='+escape('" & strTys_seikyuu_saki_cd & "')+'$$$'+escape('" & strTys_seikyuu_saki_brc & "')+'$$$'+escape('" & strTys_seikyuu_saki_kbn & "')+'$$$'+escape('2'), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_seikyuInputPage", strScript, True)
        Else
            '��L�ȊO
            '���b�Z�[�W�u�����悪���݂��܂���v��\�����A�������f	
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_Syousai", "alert('�����悪���݂��܂���B');", True)
        End If

    End Sub

    ''' <summary>
    ''' �񍐏��E�I�v�V�����{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.07</history>
    Private Sub btnHoukakusyo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHoukakusyo.Click


        '�����X���i�������@���ʑΉ��}�X�^�Ɖ���
        Dim strScript As String
        strScript = "objSrchWin = window.open('TokubetuTaiouMasterSearchList.aspx?sendSearchTerms=1$$$'+'" & ViewState("KameitenCd").ToString & "');"
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_seikyuInputPage", strScript, True)


        '�����敪�F�@1-�����X
        '�����R�[�h: �����X����

    End Sub

    ''' <summary>
    ''' ��������m�F�\�{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.26</history>
    Private Sub btnTorihukiJyoukenKakuninhyou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorihukiJyoukenKakuninhyou.Click

        Dim FilePath As String = earthAction.TorihukiJyoukenKakuninhyou(ViewState("KameitenCd").ToString, Me.lblKyoutuKubun.Text)

        Me.hidFile.Value = FilePath
        '�t�@�C���p�X�̑��݂��Ƃ𔻒f����
        If IO.File.Exists(FilePath.Replace("file:", String.Empty).Replace("/", "\")) Then
            Call GetFile()
        Else
            ShowMessage(Messages.Instance.MSG2076E)
        End If

    End Sub

    ''' <summary>
    ''' �����J�[�h�{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.26</history>
    Private Sub btnTyousaCard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousaCard.Click

        Dim FilePath As String = earthAction.TyousaCard(ViewState("KameitenCd").ToString, Me.lblKyoutuKubun.Text)

        Me.hidFile.Value = FilePath
        '�t�@�C���p�X�̑��݂��Ƃ𔻒f����
        If IO.File.Exists(FilePath.Replace("file:", String.Empty).Replace("/", "\")) Then
            Call GetFile()
        Else
            ShowMessage(Messages.Instance.MSG2077E)
        End If

    End Sub

    ''' <summary>
    ''' �t�@�C���p�X���J����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.26</history>
    Private Sub GetFile()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function OpenFile(){")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').href = document.getElementById('" & Me.hidFile.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("setTimeout('OpenFile()',1000);")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' CLOSE Div
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub closecover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "closecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "closecover1", _
                                        csScript.ToString, _
                                        True)
    End Sub

    ''' <summary>
    ''' �u����v���Z�b�g����
    ''' </summary>
    ''' <hidtory>2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ�</hidtory>
    Private Sub SetTorikesi()

        '�f�[�^���擾����
        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic
        Dim dtTorikesi As New Data.DataTable
        dtTorikesi = TyuiJyouhouInquiryLogic.GetTorikesi(ViewState("KameitenCd").ToString.Trim)

        If (dtTorikesi.Rows.Count > 0) AndAlso (Not dtTorikesi.Rows(0).Item("torikesi").ToString.Trim.Equals("0")) Then

            Me.lblTorikesi.Text = dtTorikesi.Rows(0).Item("torikesi").ToString.Trim & ":" & dtTorikesi.Rows(0).Item("meisyou").ToString.Trim

            '�F���Z�b�g����
            Call Me.SetColor(Drawing.Color.Red)
        Else
            Me.lblTorikesi.Text = String.Empty

            '�F���Z�b�g����
            Call Me.SetColor(Drawing.Color.Black)
        End If

    End Sub

    ''' <summary>
    ''' �F���Z�b�g����
    ''' </summary>
    ''' <hidtory>2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ�</hidtory>
    Private Sub SetColor(ByVal color As System.Drawing.Color)

        Me.lblKyoutuKubun.ForeColor = color
        Me.lblKubunMei.ForeColor = color
        Me.lblTorikesi.ForeColor = color
        Me.lblKyoutuKameitenCd.ForeColor = color
        Me.lblKyoutuKameitenMei1.ForeColor = color
        Me.lblKyoutuKameitenMei2.ForeColor = color
        '========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���======================
        Me.lblKoujiUriageSyuubetu.ForeColor = color
        '========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���======================

    End Sub

    ''' <summary>
    ''' �u�H�������ʁv���Z�b�g����
    ''' </summary>
    ''' <hidtory>2012/05/15 �ԗ� 407553�̑Ή� �ǉ�</hidtory>
    Private Sub SetKoujiUriageSyuubetu()

        '�f�[�^���擾����
        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic
        Dim dtKoujiUriageSyuubetu As New Data.DataTable
        dtKoujiUriageSyuubetu = TyuiJyouhouInquiryLogic.GetKoujiUriageSyuubetu(ViewState("KameitenCd").ToString.Trim)

        If (dtKoujiUriageSyuubetu.Rows.Count > 0) Then

            Me.lblKoujiUriageSyuubetu.Text = dtKoujiUriageSyuubetu.Rows(0).Item("meisyou").ToString.Trim
        Else
            Me.lblKoujiUriageSyuubetu.Text = String.Empty
        End If

    End Sub

    ''' <summary>�G���[���b�Z�[�W���|�b�v�A�b�v����B</summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
    Private Sub ShowMessage(ByVal strMessage As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            '.AppendLine("   fncSetKubunVal();")
            .AppendLine("	alert('" & strMessage & "');")
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub


    Private Sub SetKihouSyouhinAndTyousaHouhou()

        Call Me.SetDdlKihonSyouhin()
        Call Me.SetDdlKihonTyousaHouhou()

        Dim blnEabledFlg As Boolean

        '�����l���Z�b�g����
        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic
        Dim dtKihouSyouhinAndTyousaHouhou As Data.DataTable = TyuiJyouhouInquiryLogic.GetKihouSyouhinAndTyousaHouhou(ViewState("KameitenCd").ToString)

        If dtKihouSyouhinAndTyousaHouhou.Rows.Count > 0 Then
            With dtKihouSyouhinAndTyousaHouhou.Rows(0)
                Try
                    Me.ddlKihonSyouhin.SelectedValue = .Item("kihon_syouhin_cd").ToString
                Catch ex As Exception
                    Me.ddlKihonSyouhin.SelectedValue = String.Empty
                End Try

                Me.tbxKihonSyouhinTyuuibun.Text = .Item("kihon_syouhin_tyuuibun").ToString

                Try
                    Me.ddlKihonTyousaHouhou.SelectedValue = .Item("kihon_tyousahouhou_no").ToString
                Catch ex As Exception
                    Me.ddlKihonTyousaHouhou.SelectedValue = String.Empty
                End Try

                Me.tbxKihonTyousaHouhouTyuuibun.Text = .Item("kihon_tyousahouhou_tyuuibun").ToString

                '�H�����ψ˗������t�s�v
                If TrimNull(.Item("koj_mitiraisyo_soufu_fuyou")) = "" Then
                    ddl_koj_mitiraisyo_soufu_fuyou.SelectedIndex = 0
                Else
                    ddl_koj_mitiraisyo_soufu_fuyou.SelectedIndex = 1
                End If
                '�d�l�m�F��_���ƎҐ����z
                tbx_siyou_kakuninhi_jigyousya.Text = TrimNull(.Item("siyou_kakuninhi_jigyousya"))
                '�d�l�m�F��_�H����А����z
                tbx_siyou_kakuninhi_kojkaisya.Text = TrimNull(.Item("siyou_kakuninhi_kojkaisya"))


            End With
        Else
            Me.ddlKihonSyouhin.SelectedValue = String.Empty
            Me.tbxKihonSyouhinTyuuibun.Text = String.Empty

            Me.ddlKihonTyousaHouhou.SelectedValue = String.Empty
            Me.tbxKihonTyousaHouhouTyuuibun.Text = String.Empty

            '�H�����ψ˗������t�s�v
            ddl_koj_mitiraisyo_soufu_fuyou.SelectedIndex = 0
            '�d�l�m�F��_���ƎҐ����z
            tbx_siyou_kakuninhi_jigyousya.Text = ""
            '�d�l�m�F��_�H����А����z
            tbx_siyou_kakuninhi_kojkaisya.Text = ""

        End If

        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        blnEabledFlg = commonChk.CommonNinnsyou(strUserID, "tyousaka_kanrisya_kengen")

        Me.ddlKihonSyouhin.Enabled = blnEabledFlg
        Me.tbxKihonSyouhinTyuuibun.Enabled = blnEabledFlg
        Me.btnKihonSyouhin.Enabled = blnEabledFlg

        Me.ddlKihonTyousaHouhou.Enabled = blnEabledFlg
        Me.tbxKihonTyousaHouhouTyuuibun.Enabled = blnEabledFlg
        Me.btnKihonTyousaHouhou.Enabled = blnEabledFlg

    End Sub


    Private Sub SetDdlKihonSyouhin()

        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic

        Dim dtKihonSyouhin As Data.DataTable
        dtKihonSyouhin = TyuiJyouhouInquiryLogic.GetSyouhinCd()

        Me.ddlKihonSyouhin.DataValueField = "syouhin_cd"
        Me.ddlKihonSyouhin.DataTextField = "syouhin_mei"
        Me.ddlKihonSyouhin.DataSource = dtKihonSyouhin
        Me.ddlKihonSyouhin.DataBind()

        Me.ddlKihonSyouhin.Items.Insert(0, New ListItem(String.Empty, String.Empty))

    End Sub

    Private Sub SetDdlKihonTyousaHouhou()

        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic

        Dim dtKiKihonTyousaHouhou As Data.DataTable
        dtKiKihonTyousaHouhou = TyuiJyouhouInquiryLogic.GetTyousaHouhou()

        Me.ddlKihonTyousaHouhou.DataValueField = "tys_houhou_no"
        Me.ddlKihonTyousaHouhou.DataTextField = "tys_houhou_mei"
        Me.ddlKihonTyousaHouhou.DataSource = dtKiKihonTyousaHouhou
        Me.ddlKihonTyousaHouhou.DataBind()

        Me.ddlKihonTyousaHouhou.Items.Insert(0, New ListItem(String.Empty, String.Empty))

    End Sub


    Private Sub btnKihonSyouhin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKihonSyouhin.Click

        Dim commonChk As New CommonCheck
        Dim strErr As String = String.Empty

        strErr = commonChk.CheckByte(Me.tbxKihonSyouhinTyuuibun.Text, 80, "��{���i���ӕ�", kbn.ZENKAKU)
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KihonSyouhin", "alert('" & strErr & "'); document.getElementById('" & Me.tbxKihonSyouhinTyuuibun.ClientID & "').select();", True)
            Return
        End If
        strErr = commonChk.CheckKinsoku(Me.tbxKihonSyouhinTyuuibun.Text, "��{���i���ӕ�")
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KihonSyouhin", "alert('" & strErr & "'); document.getElementById('" & Me.tbxKihonSyouhinTyuuibun.ClientID & "').select();", True)
            Return
        End If

        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic

        If TyuiJyouhouInquiryLogic.SetKihonSyouhin(ViewState("KameitenCd").ToString, Me.ddlKihonSyouhin.SelectedValue, Me.tbxKihonSyouhinTyuuibun.Text) Then
            '�����̏ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KihonSyouhin", "alert('" & Messages.Instance.MSG018S.Replace("@PARAM1", "��{���i") & "');", True)
        Else
            '���s�̏ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KihonSyouhin", "alert('" & Messages.Instance.MSG019E.Replace("@PARAM1", "��{���i") & "');", True)
        End If

    End Sub


    Private Sub btnKihonTyousaHouhou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKihonTyousaHouhou.Click

        Dim commonChk As New CommonCheck
        Dim strErr As String = String.Empty

        strErr = commonChk.CheckByte(Me.tbxKihonTyousaHouhouTyuuibun.Text, 80, "��{�������@���ӕ�", kbn.ZENKAKU)
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KihonTyousaHouhou", "alert('" & strErr & "'); document.getElementById('" & Me.tbxKihonTyousaHouhouTyuuibun.ClientID & "').select();", True)
            Return
        End If
        strErr = commonChk.CheckKinsoku(Me.tbxKihonTyousaHouhouTyuuibun.Text, "��{�������@���ӕ�")
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KihonTyousaHouhou", "alert('" & strErr & "'); document.getElementById('" & Me.tbxKihonTyousaHouhouTyuuibun.ClientID & "').select();", True)
            Return
        End If

        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic

        If TyuiJyouhouInquiryLogic.SetKihonTyousaHouhou(ViewState("KameitenCd").ToString, Me.ddlKihonTyousaHouhou.SelectedValue, Me.tbxKihonTyousaHouhouTyuuibun.Text) Then
            '�����̏ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KihonTyousaHouhou", "alert('" & Messages.Instance.MSG018S.Replace("@PARAM1", "��{�������@") & "');", True)
        Else
            '���s�̏ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KihonTyousaHouhou", "alert('" & Messages.Instance.MSG019E.Replace("@PARAM1", "��{�������@") & "');", True)
        End If
    End Sub

    Protected Sub btn_siyou_kakuninhi_jigyousya_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_siyou_kakuninhi_jigyousya.Click

        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic

        Dim CommonCheck As New CommonCheck


        Dim strErr As String = ""

        If strErr = "" And tbx_siyou_kakuninhi_jigyousya.Text <> "" Then
            strErr = CommonCheck.CheckNum(tbx_siyou_kakuninhi_jigyousya.Text.Replace(",", ""), "�H���݌v�m�F��i���Ǝҁj")
        End If


        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "siyou_kakuninhi", "alert('" & strErr & "');document.getElementById('" & Me.tbx_siyou_kakuninhi_jigyousya.ClientID & "').select();", True)
            Exit Sub
        End If

        'strErr = CommonCheck.CheckHankaku(tbx_siyou_kakuninhi_kojkaisya.Text, "�H���݌v�m�F��i�H����Ёj", "1")
        'If strErr <> "" Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "siyou_kakuninhi", "alert('" & strErr & "');document.getElementById('" & Me.tbx_siyou_kakuninhi_kojkaisya.ClientID & "').select();", True)
        '    Exit Sub
        'End If


        If TyuiJyouhouInquiryLogic.UpdKojSiyouKakunin(ViewState("KameitenCd").ToString, Me.ddl_koj_mitiraisyo_soufu_fuyou.SelectedValue, Me.tbx_siyou_kakuninhi_jigyousya.Text.Replace(",", ""), Me.tbx_siyou_kakuninhi_kojkaisya.Text.Replace(",", ""), 1) Then
            '�����̏ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "siyou_kakuninhi", "alert('" & Messages.Instance.MSG018S.Replace("@PARAM1", "�H���E�d�l�m�F") & "');", True)
        Else
            '���s�̏ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "siyou_kakuninhi", "alert('" & Messages.Instance.MSG019E.Replace("@PARAM1", "�H���E�d�l�m�F") & "');", True)
        End If

    End Sub

    Protected Sub btn_siyou_kakuninhi_kojkaisya_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_siyou_kakuninhi_kojkaisya.Click

        Dim TyuiJyouhouInquiryLogic As New TyuiJyouhouInquiryLogic

        Dim CommonCheck As New CommonCheck


        Dim strErr As String = ""
        'strErr = CommonCheck.CheckHankaku(tbx_siyou_kakuninhi_jigyousya.Text, "�H���݌v�m�F��i���Ǝҁj", "1")
        'If strErr <> "" Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "siyou_kakuninhi", "alert('" & strErr & "');document.getElementById('" & Me.tbx_siyou_kakuninhi_jigyousya.ClientID & "').select();", True)
        '    Exit Sub
        'End If

        If strErr = "" And tbx_siyou_kakuninhi_kojkaisya.Text <> "" Then
            strErr = CommonCheck.CheckNum(tbx_siyou_kakuninhi_kojkaisya.Text.Replace(",", ""), "�H���݌v�m�F��i�H����Ёj")
        End If



        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "siyou_kakuninhi", "alert('" & strErr & "');document.getElementById('" & Me.tbx_siyou_kakuninhi_kojkaisya.ClientID & "').select();", True)
            Exit Sub
        End If


        If TyuiJyouhouInquiryLogic.UpdKojSiyouKakunin(ViewState("KameitenCd").ToString, Me.ddl_koj_mitiraisyo_soufu_fuyou.SelectedValue, Me.tbx_siyou_kakuninhi_jigyousya.Text.Replace(",", ""), Me.tbx_siyou_kakuninhi_kojkaisya.Text.Replace(",", ""), 2) Then
            '�����̏ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "siyou_kakuninhi", "alert('" & Messages.Instance.MSG018S.Replace("@PARAM1", "�H���E�d�l�m�F") & "');", True)
        Else
            '���s�̏ꍇ
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "siyou_kakuninhi", "alert('" & Messages.Instance.MSG019E.Replace("@PARAM1", "�H���E�d�l�m�F") & "');", True)
        End If

    End Sub
End Class