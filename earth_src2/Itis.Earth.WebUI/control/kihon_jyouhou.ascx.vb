Imports Itis.Earth.BizLogic
Partial Public Class kihon_jyouhou
    Inherits System.Web.UI.UserControl
    Public msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus
    Private KihonJyouhouLogic As New KihonJyouhouLogic
    Private strKameitenCd As String
    Private strLoginUserId As String
    Private _kenngenn As Boolean




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
        Dim itKassei As Boolean = Iskassei(strKameitenCd, "")

        '�N�ԓ���
        tbxlblNenkanTousuu.ReadOnly = Not itKassei
        tbxlblNenkanTousuu.CssClass = IIf(itKassei, "", "readOnly")

        '�H��������
        'Common_drop2.Enabled = itKassei

        If Not itKassei Then
            Common_drop2.Obj.Attributes.Item("onfocus") = "this.defaultIndex=this.selectedIndex;"
            Common_drop2.Obj.Attributes.Item("onchange") = "this.selectedIndex=this.defaultIndex;"
        End If

        Common_drop2.CssClass = IIf(itKassei, "", "readOnly")


    End Sub

    '�ҏW���ڔ񊈐��A�����ݒ�Ή��@20180905�@�������@�Ή��@��

    ''' <summary>�y�[�W���b�h</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btnTouroku.Enabled = _kenngenn
            PageINIT()
        End If
        tbxHKDate.Attributes.Add("onblur", "checkDate(this);")
        tbxJisinHosyou.Attributes.Add("onblur", "checkDate(this);")
        Me.tbxAddNengetu.Attributes.Add("onblur", "chkNengetu(this)")
        ddlSyoumeisyo.Attributes.Add("onchange", "ShowModal();")
        '  tbxSeisikiKana.Attributes.Add("onblur", "fncTokomozi(this)")
        SetKassei()
    End Sub
    ''' <summary>�y�[�W��INIT</summary>
    Public Sub PageINIT()
        Dim dtKihonJyouhouTable As New DataAccess.KihonJyouhouDataSet.KihonJyouhouTableDataTable
        dtKihonJyouhouTable = KihonJyouhouLogic.GetKihonJyouhouInfo(strKameitenCd)
        If dtKihonJyouhouTable.Rows.Count > 0 Then
            With dtKihonJyouhouTable.Rows(0)
                Common_drop1.SelectedValue = TrimNull(.Item("todouhuken_cd"))
                tbxlblNenkanTousuu.Text = TrimNull(.Item("nenkan_tousuu"))
                tbxEigyouCd.Text = TrimNull(.Item("eigyou_tantousya_cd"))
                tbxEigyouMei.Text = TrimNull(.Item("eigyou_tantousya_mei"))
                If TrimNull(.Item("hikitugi_kanryou_date")) <> "" Then
                    tbxHKDate.Text = CDate(TrimNull(.Item("hikitugi_kanryou_date"))).ToString("yyyy/MM/dd")
                Else
                    tbxHKDate.Text = TrimNull(.Item("hikitugi_kanryou_date"))
                End If

                tbxKyuuEigyouCd.Text = TrimNull(.Item("kyuu_eigyou_tantousya_cd"))
                tbxKyuuEigyouMei.Text = TrimNull(.Item("kyuu_eigyou_tantousya_mei"))

                If TrimNull(.Item("fuho_syoumeisyo_flg")) = "1" Then
                    ddlSyoumeisyo.SelectedIndex = 0
                    btnFuho.Enabled = True
                Else
                    ddlSyoumeisyo.SelectedIndex = 1
                    btnFuho.Enabled = False
                End If
                If TrimNull(.Item("fuho_syoumeisyo_kaisi_nengetu")) <> "" Then
                    tbxAddNengetu.Text = CDate(TrimNull(.Item("fuho_syoumeisyo_kaisi_nengetu"))).ToString("yyyy/MM")
                Else
                    tbxAddNengetu.Text = TrimNull(.Item("fuho_syoumeisyo_kaisi_nengetu"))
                End If

                If TrimNull(.Item("jisin_hosyou_flg")) = "1" Then
                    ddlJisinHosyou.SelectedIndex = 0
                Else
                    ddlJisinHosyou.SelectedIndex = 1
                End If

                If TrimNull(.Item("jisin_hosyou_add_date")) <> "" Then
                    tbxJisinHosyou.Text = CDate(TrimNull(.Item("jisin_hosyou_add_date"))).ToString("yyyy/MM/dd")
                Else
                    tbxJisinHosyou.Text = TrimNull(.Item("jisin_hosyou_add_date"))
                End If
                Common_drop2.SelectedValue = TrimNull(.Item("koj_uri_syubetsu"))
                If TrimNull(.Item("koj_support_system")) = "1" Then
                    ddlSystem.SelectedIndex = 0
                Else
                    ddlSystem.SelectedIndex = 1
                End If
                If TrimNull(.Item("jiosaki_flg")) = "1" Then
                    ddlJio.SelectedIndex = 0
                Else
                    ddlJio.SelectedIndex = 1
                End If
                hidHaita.Value = TrimNull(.Item("upd_datetime"))
                tbxSeisikiMei.Text = TrimNull(.Item("kameiten_seisiki_mei"))
                tbxSeisikiKana.Text = TrimNull(.Item("kameiten_seisiki_mei_kana"))

                '2013/11/01 ���F�ǉ� ��
                'SDS�����ݒ���
                If TrimNull(.Item("sds_jidoou_set_info")).Equals(String.Empty) Then
                    Me.ddlSds.SelectedIndex = 0
                Else
                    Me.ddlSds.SelectedIndex = TrimNull(.Item("sds_jidoou_set_info"))
                End If
                '2013/11/01 ���F�ǉ� ��

                '==================2017/01/01 ������ �ǉ� �V�z�Z����n���i�̔��j���� �s���Y�������� ���t�H�[���O�N�x�������z��==========================
                tbxSintikuKensuu.Text = TrimNull(.Item("shinchiku_jyuutaku_hikiwatashi_kensuu"))
                tbxFudouKensuu.Text = TrimNull(.Item("fudousan_baibai_kensuu"))
                tbxZenNenUkeoiKin.Text = TrimNull(.Item("reform_zennendo_ukeoi_kingaku"))
                '==================2017/01/01 ������ �ǉ� �V�z�Z����n���i�̔��j���� �s���Y�������� ���t�H�[���O�N�x�������z��==========================



            End With
        End If
    End Sub
    ''' <summary>�����X�R�[�h�������Z�b�g</summary>
    Public WriteOnly Property GetKameitenCd() As String
        Set(ByVal KameitenCd As String)
            strKameitenCd = KameitenCd
        End Set
    End Property
    ''' <summary>���O�C�����[�U�[�������Z�b�g</summary>
    Public WriteOnly Property GetLoginUserId() As String
        Set(ByVal LoginUserId As String)
            strLoginUserId = LoginUserId
        End Set
    End Property
    ''' <summary>�����N���N���b�N</summary>
    Private Sub lnkTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTitle.Click
        If meisaiTbody1.Style.Item("display") = "none" Then
            meisaiTbody1.Style.Item("display") = ""
            btnTouroku.Style.Item("display") = ""
        Else
            meisaiTbody1.Style.Item("display") = "none"
            btnTouroku.Style.Item("display") = "none"
        End If

    End Sub
    ''' <summary>�󔒂��폜</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    ''' <summary>���̓`�F�b�N</summary>
    Public Function checkInput(ByRef strId As String) As String
        Dim csScript As New StringBuilder
        Dim strCaption As String = ""
        Dim commonCheck As New CommonCheck
        Dim commonSearch As New CommonSearchLogic
        Dim dtBirudaTable As New DataAccess.CommonSearchDataSet.BirudaTableDataTable
        Dim blnErr As Boolean = False
        checkInput = ""
        '�����X���P
        strCaption = commonCheck.CheckHissuNyuuryoku(tbxSeisikiMei.Text, "��������")
        If strCaption <> "" Then
            checkInput = checkInput & strCaption.ToString
            If strId = "" Then
                strId = tbxSeisikiMei.ClientID
            End If
        Else
            strCaption = commonCheck.CheckByte(tbxSeisikiMei.Text, 80, "��������", WebUI.kbn.ZENKAKU)
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxSeisikiMei.ClientID
                End If
            Else
                strCaption = commonCheck.CheckKinsoku(tbxSeisikiMei.Text, "��������")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxSeisikiMei.ClientID
                    End If
                End If
            End If
        End If


        '�X�J�i���Q
        If tbxSeisikiKana.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxSeisikiKana.Text, 40, "�������̂Q", WebUI.kbn.ZENKAKU)
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxSeisikiKana.ClientID
                End If
            Else
                strCaption = commonCheck.CheckKinsoku(tbxSeisikiKana.Text, "�������̂Q")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxSeisikiKana.ClientID
                    End If
                End If
            End If
        End If
        '    strCaption = commonCheck.CheckHissuNyuuryoku(tbxSeisikiKana.Text, "�������̂Q")
        '    If strCaption <> "" Then
        '        checkInput = checkInput & strCaption.ToString
        '        If strId = "" Then
        '            strId = tbxSeisikiKana.ClientID
        '        End If
        '    Else


        '        'strCaption = commonCheck.CheckKatakana(tbxSeisikiKana.Text, "�������̃J�i")
        '        'If strCaption <> "" Then
        '        '    checkInput = checkInput & strCaption.ToString
        '        '    If strId = "" Then
        '        '        strId = tbxSeisikiKana.ClientID
        '        '    End If
        '        'Else

        '        'End If
        '    End If
        'End If

        strCaption = commonCheck.CheckHissuNyuuryoku(Common_drop1.SelectedText, "�s���{���R�[�h")
        If strCaption <> "" Then
            checkInput = checkInput & strCaption.ToString
            If strId = "" Then
                strId = Common_drop1.DdlClientID
            End If
        End If
        '�N�ԓ���
        If tbxlblNenkanTousuu.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxlblNenkanTousuu.Text, 5, "�N�ԓ���")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxlblNenkanTousuu.ClientID
                End If
            Else
                strCaption = commonCheck.ChkHankakuEisuuji(tbxlblNenkanTousuu.Text, "�N�ԓ���")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxlblNenkanTousuu.ClientID
                    End If
                End If
            End If
        End If
        If ddlSyoumeisyo.SelectedValue = "1" Then
            strCaption = commonCheck.CheckHissuNyuuryoku(tbxAddNengetu.Text, "�t�ۏؖ����J�n�N��")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxAddNengetu.ClientID
                End If
            End If
        End If
        '�t�ۏؖ����J�n�N��
        If tbxAddNengetu.Text <> "" Then
            strCaption = commonCheck.CheckYuukouHiduke(tbxAddNengetu.Text & "/01", "�t�ۏؖ����J�n�N��")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxAddNengetu.ClientID
                End If
            End If
        End If

        '==================2017/01/01 ������ �ǉ� �V�z�Z����n���i�̔��j���� �s���Y�������� ���t�H�[���O�N�x�������z��==========================
        '�V�z�Z����n���i�̔��j����
        If tbxSintikuKensuu.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxSintikuKensuu.Text, 10, "�V�z�Z����n���i�̔��j����")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxSintikuKensuu.ClientID
                End If
            Else
                strCaption = commonCheck.ChkHankakuEisuuji(tbxSintikuKensuu.Text, "�V�z�Z����n���i�̔��j����")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxSintikuKensuu.ClientID
                    End If
                End If
            End If
        End If


        '�s���Y��������
        If tbxFudouKensuu.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxFudouKensuu.Text, 8, "�s���Y��������")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxFudouKensuu.ClientID
                End If
            Else
                strCaption = commonCheck.ChkHankakuEisuuji(tbxFudouKensuu.Text, "�s���Y��������")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxFudouKensuu.ClientID
                    End If
                End If
            End If
        End If


        '���t�H�[���O�N�x�������z
        If tbxZenNenUkeoiKin.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxZenNenUkeoiKin.Text, 20, "���t�H�[���O�N�x�������z")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxZenNenUkeoiKin.ClientID
                End If
            Else
                strCaption = commonCheck.ChkHankakuEisuuji(tbxZenNenUkeoiKin.Text, "���t�H�[���O�N�x�������z")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxZenNenUkeoiKin.ClientID
                    End If
                End If
            End If
        End If
        '==================2017/01/01 ������ �ǉ� �V�z�Z����n���i�̔��j���� �s���Y�������� ���t�H�[���O�N�x�������z��==========================



        '�c�ƒS����

        blnErr = False
        If tbxEigyouCd.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxEigyouCd.Text, 64, "�c�ƒS����")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxEigyouCd.ClientID
                End If
                blnErr = True
            Else
                strCaption = commonCheck.ChkHankakuEisuuji(tbxEigyouCd.Text, "�c�ƒS����")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxEigyouCd.ClientID
                    End If
                    blnErr = True
                End If
            End If
            If Not blnErr Then
                dtBirudaTable = commonSearch.GetEigyouInfo(tbxEigyouCd.Text)
                If dtBirudaTable.Rows.Count = 0 Then
                    checkInput = checkInput & String.Format(Messages.Instance.MSG2008E, "�c�ƒS����").ToString
                    If strId = "" Then
                        strId = tbxEigyouCd.ClientID
                    End If

                Else
                    tbxEigyouCd.Text = dtBirudaTable.Rows(0).Item(0)
                    tbxEigyouMei.Text = dtBirudaTable.Rows(0).Item(1)
                End If
            End If
        End If

        '���p������
        If tbxHKDate.Text <> "" Then
            strCaption = commonCheck.CheckYuukouHiduke(tbxHKDate.Text, "���p������")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxHKDate.ClientID
                End If
            End If
        End If

        '���c�ƒS����
        blnErr = False
        If tbxKyuuEigyouCd.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxKyuuEigyouCd.Text, 64, "���c�ƒS����")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxKyuuEigyouCd.ClientID
                End If
                blnErr = True
            Else
                strCaption = commonCheck.ChkHankakuEisuuji(tbxKyuuEigyouCd.Text, "���c�ƒS����")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxKyuuEigyouCd.ClientID
                    End If
                    blnErr = True
                End If
            End If
            If Not blnErr Then
                dtBirudaTable = commonSearch.GetEigyouInfo(tbxKyuuEigyouCd.Text)
                If dtBirudaTable.Rows.Count = 0 Then
                    '===========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���========================
                    'checkInput = checkInput & String.Format(Messages.Instance.MSG2008E, "���c�ƒS����").ToString
                    'If strId = "" Then
                    '    strId = tbxKyuuEigyouCd.ClientID
                    'End If
                    '===========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���========================
                Else
                    tbxKyuuEigyouCd.Text = dtBirudaTable.Rows(0).Item(0)
                    tbxKyuuEigyouMei.Text = dtBirudaTable.Rows(0).Item(1)
                End If
            End If
        End If
        '�n�k�⏞�o�^��
        If tbxJisinHosyou.Text <> "" Then
            strCaption = commonCheck.CheckYuukouHiduke(tbxJisinHosyou.Text, "�n�k�⏞�o�^��")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxJisinHosyou.ClientID
                End If
            End If
        End If

    End Function
    ''' <summary>�G���[��\��</summary>
    Public Sub ShowMsg(ByVal msg As String, _
                               ByVal strClientID As String)

        Dim csScript As New StringBuilder


        Dim pPage As Page = Me.Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo
        If strClientID = Common_drop1.DdlClientID Then
            methodInfo = pType.GetMethod("ShowMsgFocus")
        Else
            methodInfo = pType.GetMethod("ShowMsg2")
        End If



        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {strClientID, msg})
        End If


    End Sub
    ''' <summary>�X�V�҂�ݒ�</summary>
    Public Sub setKousinSya(ByVal strErr As String)

        Dim csScript As New StringBuilder


        Dim pPage As Page = Me.Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("setKousinSya")


        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {strErr})
        End If


    End Sub
    ''' <summary>�X�V����ݒ�</summary>
    Public Sub setKousinHi(ByVal strErr As String)

        Dim csScript As New StringBuilder


        Dim pPage As Page = Me.Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("setKousinHi")


        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {strErr})
        End If


    End Sub
    ''' <summary>�r���`�F�b�N</summary>
    Public Function haitaCheck(ByVal strKameiten As String) As String

        Dim csScript As New StringBuilder


        Dim pPage As Page = Me.Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("haitaCheck")


        If Not methodInfo Is Nothing Then
            Return methodInfo.Invoke(pPage, New Object() {strKameiten})
        Else
            Return ""
        End If


    End Function
    ''' <summary>�o�^�{�^�����N�b���N</summary>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        Dim strObjId As String = ""
        Dim strErr As String = checkInput(strObjId)

        If strErr <> "" Then
            ShowMsg(strErr, strObjId)

        Else
            strErr = haitaCheck(strKameitenCd)
            If strErr <> "" Then
                ShowMsg(strErr, btnTouroku.ClientID)
                setKousinSya(strErr)
            Else
                Dim dtKihonJyouhouData As New DataAccess.KihonJyouhouDataSet.KihonJyouhouTableDataTable
                dtKihonJyouhouData.Rows.Add(dtKihonJyouhouData.NewKihonJyouhouTableRow)
                With dtKihonJyouhouData.Rows(0)
                    .Item("todouhuken_cd") = Common_drop1.SelectedValue
                    .Item("nenkan_tousuu") = tbxlblNenkanTousuu.Text
                    .Item("eigyou_tantousya_mei") = tbxEigyouCd.Text
                    .Item("hikitugi_kanryou_date") = tbxHKDate.Text
                    .Item("kyuu_eigyou_tantousya_mei") = tbxKyuuEigyouCd.Text
                    .Item("jisin_hosyou_flg") = ddlJisinHosyou.SelectedValue
                    .Item("jisin_hosyou_add_date") = tbxJisinHosyou.Text
                    .Item("fuho_syoumeisyo_flg") = ddlSyoumeisyo.SelectedValue
                    .Item("fuho_syoumeisyo_kaisi_nengetu") = tbxAddNengetu.Text
                    .Item("kameiten_cd") = strKameitenCd
                    .Item("upd_login_user_id") = strLoginUserId
                    .Item("koj_uri_syubetsu") = Common_drop2.SelectedValue
                    .Item("koj_support_system") = ddlSystem.SelectedValue
                    .Item("jiosaki_flg") = ddlJio.SelectedValue
                    .Item("kameiten_seisiki_mei") = tbxSeisikiMei.Text
                    .Item("kameiten_seisiki_mei_kana") = tbxSeisikiKana.Text
                    '2013/11/01 ���F�ǉ� ��
                    'SDS�����ݒ���
                    .Item("sds_jidoou_set_info") = Me.ddlSds.SelectedValue
                    '2013/11/01 ���F�ǉ� ��

                    '==================2017/01/01 ������ �ǉ� �V�z�Z����n���i�̔��j���� �s���Y�������� ���t�H�[���O�N�x�������z��==========================
                    .Item("shinchiku_jyuutaku_hikiwatashi_kensuu") = tbxSintikuKensuu.Text
                    .Item("fudousan_baibai_kensuu") = tbxFudouKensuu.Text
                    .Item("reform_zennendo_ukeoi_kingaku") = tbxZenNenUkeoiKin.Text
                    '==================2017/01/01 ������ �ǉ� �V�z�Z����n���i�̔��j���� �s���Y�������� ���t�H�[���O�N�x�������z��==========================


                End With
                KihonJyouhouLogic.SetUpdKihonJyouhouInfo(dtKihonJyouhouData)
                setKousinHi(strKameitenCd)
                Dim dtKyoutuuJyouhouTable As Itis.Earth.DataAccess.KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
                Dim KyoutuuJyouhouLogic As New KyoutuuJyouhouLogic
                dtKyoutuuJyouhouTable = KyoutuuJyouhouLogic.GetKameitenInfo(strKameitenCd)
                If dtKyoutuuJyouhouTable.Rows.Count > 0 Then
                    With dtKyoutuuJyouhouTable.Rows(0)
                        If TrimNull(.Item("upd_datetime")) <> "" Then
                            hidHaita.Value = CDate(.Item("upd_datetime")).ToString("yyyy/MM/dd HH:mm:ss")
                        Else
                            hidHaita.Value = TrimNull(.Item("upd_datetime"))
                        End If
                    End With
                End If
                ShowMsg(Replace(Messages.Instance.MSG018S, "@PARAM1", "��{���"), btnTouroku.ClientID)
                PageINIT()
            End If
        End If
      
    End Sub
    ''' <summary>�o�^�{�^��������ݒ�</summary>
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get
        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set
    End Property

    Private Sub ddlSyoumeisyo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSyoumeisyo.TextChanged
        If ddlSyoumeisyo.SelectedIndex = 0 Then
            tbxAddNengetu.Text = Now.ToString("yyyy/MM")
        End If
        msgAndFocus.setFocus(Me.Page, tbxAddNengetu)

    End Sub

    Protected Sub btnFuho_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strNengetu As String = ""
        Dim strNengetu2 As String = ""
        Dim strKbn As String = ""
        Dim KyoutuuJyouhouLogic As New KyoutuuJyouhouLogic
        Dim dtKyoutuuJyouhouTable As Itis.Earth.DataAccess.KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
        Dim intCount As Integer = 0
        dtKyoutuuJyouhouTable = KyoutuuJyouhouLogic.GetKameitenInfo(strKameitenCd)
        strKbn = dtKyoutuuJyouhouTable.Rows(0).Item("kbn")
        strNengetu = dtKyoutuuJyouhouTable.Rows(0).Item("sansyou_date")
        strNengetu = CDate(strNengetu).ToString("yyyy/MM")

        intCount = KyoutuuJyouhouLogic.SelKensuu(strKameitenCd, strKbn, strNengetu).Rows.Count
        If intCount = 0 Then
            strNengetu2 = strNengetu & "/01"
            strNengetu2 = DateAdd(DateInterval.Month, -1, CDate(strNengetu2)).ToString("yyyy/MM")
            intCount = KyoutuuJyouhouLogic.SelKensuu(strKameitenCd, strKbn, strNengetu2).Rows.Count
            If intCount = 0 Then
                ShowMsg("�����܂��͑O���ɕۏ؏��𔭍s���Ă��镨���͂���܂���B", btnFuho.ClientID)
            Else
                Dim strScript As String
                strScript = "objSrchWin = window.open('HosyousyoBukkenItiran.aspx?Kbn='+escape('" & strKbn & "')+'&Kameiten='+escape('" & strKameitenCd & "')+'&Getu='+escape('" & strNengetu & "')+'&Flg=1'" & _
                              ", 'searchWindow', 'height=480,width=850,menubar=no,toolbar=no,location=no,status=no,resizable=no,scrollbars=yes');"

                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "popup_fire", strScript, True)

            End If
        Else
            Dim strScript As String
            strScript = "objSrchWin = window.open('HosyousyoBukkenItiran.aspx?Kbn='+escape('" & strKbn & "')+'&Kameiten='+escape('" & strKameitenCd & "')+'&Getu='+escape('" & strNengetu & "')+'&Flg=0'" & _
                          ", 'searchWindow', 'height=480,width=850,menubar=no,toolbar=no,location=no,status=no,resizable=no,scrollbars=yes');"

            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "popup_fire", strScript, True)

        End If
    End Sub
    Private Function NullToEmpty(ByVal v As Object) As Object

        If v Is DBNull.Value Then
            Return String.Empty
        Else
            Return v.ToString()
        End If
    End Function
   
End Class