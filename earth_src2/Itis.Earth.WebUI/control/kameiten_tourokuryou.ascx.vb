Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Imports System.Web.UI.Page

Partial Public Class kameiten_tourokuryou
    Inherits System.Web.UI.UserControl

#Region "���ʕϐ�"
    Private KihonJyouhouInquiryBc As New Itis.Earth.BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
    Public msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus
    Private Const SEIKYUUSIMEDATE As Integer = 0
    Private Const HANSOHUHINSEIKYUUSIMEDATE As Integer = 1

    Private Const HANKAKU As Integer = 1
    Private Const ZENKAKU As Integer = 2
    Private Const YM As String = "yyyy/MM"
    Private Const YMD As String = "yyyy/MM/dd"
    Private Const SIGNDAY As String = "d"
    Private Const SIGNMONTH As String = "m"

#End Region

#Region "�v���p�e�B"
    Public _kameiten_cd As String
    Public _keiretuCd As String
    Public _Kbn As String
    Public _MiseCode As String
    Private _upd_login_user_id As String
    Public Property Upd_login_user_id() As String
        Get
            Return _upd_login_user_id
        End Get

        Set(ByVal value As String)
            _upd_login_user_id = value
        End Set

    End Property
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MiseCode() As String
        Get
            Return _MiseCode
        End Get
        Set(ByVal value As String)
            _MiseCode = value
        End Set
    End Property
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return _Kbn
        End Get
        Set(ByVal value As String)
            _Kbn = value
        End Set
    End Property

    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property kameiten_cd() As String
        Get
            Return _kameiten_cd
        End Get
        Set(ByVal value As String)
            _kameiten_cd = value
        End Set
    End Property
    ''' <summary>
    ''' �n��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property keiretuCd() As String
        Get
            Return _keiretuCd
        End Get
        Set(ByVal value As String)
            _keiretuCd = value
        End Set
    End Property

    '����
    Private _kenngenn As Boolean
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get
        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set
    End Property
#End Region

#Region "���"


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
        Dim itKassei As Boolean = Iskassei(_kameiten_cd, "")

        tbxAddDate.ReadOnly = Not itKassei
        tbxAddDate.CssClass = GetCss(itKassei, tbxAddDate.CssClass)


        If Not itKassei Then
            CommonKassei.SetDropdownListReadonly(ddlSeikyuuUmu)
        End If

        tbxSyouhinCd.ReadOnly = Not itKassei
        tbxSyouhinCd.CssClass = GetCss(itKassei, tbxSyouhinCd.CssClass)

        btnKansaku.Enabled = itKassei
        btnKansaku.CssClass = GetCss(itKassei, btnKansaku.CssClass)

        tbxZeinuki.ReadOnly = Not itKassei
        tbxZeinuki.CssClass = GetCss(itKassei, tbxZeinuki.CssClass)

        tbxSeikyuDate.ReadOnly = Not itKassei
        tbxSeikyuDate.CssClass = GetCss(itKassei, tbxSeikyuDate.CssClass)

        tbxUriDate.ReadOnly = Not itKassei
        tbxUriDate.CssClass = GetCss(itKassei, tbxUriDate.CssClass)

        tbxBikou.ReadOnly = Not itKassei
        tbxBikou.CssClass = GetCss(itKassei, tbxBikou.CssClass)
    End Sub

    Public Function GetCss(ByVal itKassei As Boolean, ByVal css As String)
        If itKassei Then
            Return Microsoft.VisualBasic.Strings.Replace(css, "readOnly", "", 1, -1, CompareMethod.Text)
        Else
            Return css & " readOnly"
        End If
    End Function

    ''' <summary>
    ''' ��ʏ�����
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            PageInit()
        Else
            If hidLastFocus.Value <> String.Empty Then
                If hidLastFocus.Value = "1" Then
                    tbxAddDate1_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "2" Then
                    tbxSyouhinCd_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "3" Then
                    tbxZeinuki_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "4" Then
                    tbxKoumutenSeikyuuGaku_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "5" Then
                    tbxSeikyuDate_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "6" Then
                    tbxAddDate1_TextChanged1(sender, e)
                ElseIf hidLastFocus.Value = "7" Then
                    tbxSyouhinCd1_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "8" Then
                    tbxZeinuki1_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "9" Then
                    tbxSeikyuDate1_TextChanged(sender, e)
                Else
                    hidLastFocus.Value = String.Empty
                End If


            End If

            'Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
            'otherPageFunction.DoFunction(Parent.Page, "closecover")

        End If
        SetKassei()
    End Sub

    ''' <summary>
    ''' �o�^���ύX��
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub tbxAddDate1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "1" Then
                Exit Sub
            End If
        End If

        '���������s���A����N�����@�@�@�@ �����͂̏ꍇ�A�����ݒ�@�@�@�@�@
        Dim dateValue As String
        dateValue = chkDate(Me.tbxAddDate.Text)
        If dateValue <> "false" Then
            Me.tbxAddDate.Text = dateValue
        Else
            ShowMsg("���t�ȊO�����͂���Ă��܂��B", Me.tbxAddDate)
            Me.hidLastFocus.Value = "1"
            Exit Sub
        End If
        ''���i���ނ̓��̓`�F�b�N
        If Me.tbxSyouhinCd.Text <> String.Empty Then

            '���������s���A����N������ݒ肷��
            SetDate(False)

            '������L�ɂ���
            Me.ddlSeikyuuUmu.SelectedIndex = 1

            '�Ŕ����z����͉\�ɂ���
            UnLockItemTextbox(tbxZeinuki)
            '���������s������͉\�ɂ���
            UnLockItemTextbox(tbxSeikyuDate)
        End If

        Me.hidLastFocus.Value = String.Empty

        msgAndFocus.setFocus(Me.Page, Me.ddlSeikyuuUmu)

    End Sub

    ''' <summary>
    ''' �̑��i�����c�[����
    ''' �z����
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub tbxAddDate1_TextChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "6" Then
                Exit Sub
            End If
        End If
        '���������s���A����N�����@�@�@�@ �����͂̏ꍇ�A�����ݒ�@�@�@�@�@

        Dim dateValue As String
        dateValue = chkDate(Me.tbxAddDate1.Text)
        If dateValue <> "false" Then
            Me.tbxAddDate1.Text = dateValue
        Else
            Me.hidLastFocus.Value = "6"
            ShowMsg("���t�ȊO�����͂���Ă��܂��B", Me.tbxAddDate1)
            Exit Sub
        End If


        ''���i���ނ̓��̓`�F�b�N
        If Me.tbxSyouhinCd1.Text <> String.Empty Then

            '���������s���A����N������ݒ肷��
            SetDate1(False)

            '������L�ɂ���
            Me.ddlSeikyuuUmu1.SelectedIndex = 1

            '�Ŕ����z����͉\�ɂ���
            UnLockItemTextbox(tbxZeinuki1)
            '���������s������͉\�ɂ���
            UnLockItemTextbox(tbxSeikyuDate1)

        End If


        Me.hidLastFocus.Value = String.Empty

        msgAndFocus.setFocus(Me.Page, Me.ddlSeikyuuUmu1)
    End Sub

    ''' <summary>
    ''' �����L��
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub ddlSeikyuuUmu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSeikyuuUmu.SelectedIndexChanged
        '�������͍��ڂ��Đݒ肷��
        OperateSeikyuData()

        msgAndFocus.setFocus(Me.Page, Me.tbxSyouhinCd)
    End Sub

    ''' <summary>
    '''  �̑��i�����c�[����
    ''' �����L��
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub ddlSeikyuuUmu1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSeikyuuUmu1.SelectedIndexChanged
        '�������͍��ڂ��Đݒ肷��
        OperateSeikyuData1()
        msgAndFocus.setFocus(Me.Page, Me.tbxSyouhinCd1)

    End Sub

    ''' <summary>
    ''' �o�^��
    ''' ���i�R�[�h�ύX
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub tbxSyouhinCd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxSyouhinCd.TextChanged
        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "2" Then

                Exit Sub
            End If
        End If

        '���i�R�[�h�̕ύX�ɂ��e���ڂ��Đݒ肷��
        If Not UpdShouhinCd() Then
            Me.hidLastFocus.Value = "2"
            Exit Sub
        End If




        If Me.hidLastFocus.Value <> "2" Then
            msgAndFocus.setFocus(Me.Page, Me.tbxBikou)
        End If
        Me.hidLastFocus.Value = String.Empty

    End Sub

    ''' <summary>
    '''  �̑��i�����c�[����
    ''' ���i�R�[�h��ύX
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxSyouhinCd1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxSyouhinCd1.TextChanged
        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "7" Then
                Exit Sub
            End If
        End If
        '���i�R�[�h�̕ύX�ɂ��e���ڂ��Đݒ肷��
        If Not UpdShouhinCd1() Then
            Me.hidLastFocus.Value = "7"
            Exit Sub
        End If

        If Me.hidLastFocus.Value <> "7" Then
            msgAndFocus.setFocus(Me.Page, Me.tbxBikou1)
        End If
        Me.hidLastFocus.Value = String.Empty
    End Sub

    ''' <summary>
    ''' �o�^��
    ''' �������ύX��
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub tbxZeinuki_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "3" Then
                Exit Sub
            End If
        End If

        If tbxZeinuki.Text <> String.Empty Then
            If Not Microsoft.VisualBasic.IsNumeric(tbxZeinuki.Text) Then
                ShowMsg(Messages.Instance.MSG2006E, Me.tbxZeinuki, "�������Ŕ����i")
                hidLastFocus.Value = "3"
                Exit Sub
            End If
        End If

        '�Ŕ���
        If Not Zeinuki() Then
        End If

        tbxZeinuki.Text = SetKingaku(tbxZeinuki.Text, False)

        Me.hidLastFocus.Value = String.Empty

        msgAndFocus.setFocus(Me.Page, Me.tbxBikou)
    End Sub

    ''' <summary>
    '''  �̑��i�����c�[����
    ''' �������ύX��
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub tbxZeinuki1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxZeinuki1.TextChanged

        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "8" Then
                Exit Sub
            End If
        End If

        If tbxZeinuki1.Text <> String.Empty Then
            If Not Microsoft.VisualBasic.IsNumeric(tbxZeinuki1.Text) Then
                Me.hidLastFocus.Value = "8"
                ShowMsg(Messages.Instance.MSG2006E, Me.tbxZeinuki1, "�������Ŕ����i")
                Exit Sub
            End If
        End If


        '�Ŕ���
        If Not Zeinuki1() Then

        End If
        tbxZeinuki1.Text = SetKingaku(tbxZeinuki1.Text, False)



        Me.hidLastFocus.Value = String.Empty
        msgAndFocus.setFocus(Me.Page, Me.tbxBikou1)
    End Sub

    ''' <summary>
    ''' �H���X�����ݒ�
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' 
    Protected Sub tbxKoumutenSeikyuuGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxKoumutenSeikyuuGaku.TextChanged

        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "4" Then
                Exit Sub
            End If
        End If

        If tbxKoumutenSeikyuuGaku.Text <> String.Empty Then
            If Not Microsoft.VisualBasic.IsNumeric(tbxKoumutenSeikyuuGaku.Text) Then
                Me.hidLastFocus.Value = "4"
                ShowMsg(Messages.Instance.MSG2006E, Me.tbxKoumutenSeikyuuGaku, "�H���X�����Ŕ����z")
                Exit Sub
            End If
        End If


        '�H���X����
        If Not SetKoumutenSeikyuu() Then
            'tbxKoumutenSeikyuuGaku.Text = SetKingaku(tbxKoumutenSeikyuuGaku.Text)

            'Exit Sub
        End If

        tbxKoumutenSeikyuuGaku.Text = SetKingaku(tbxKoumutenSeikyuuGaku.Text, False)


        Me.hidLastFocus.Value = String.Empty


        msgAndFocus.setFocus(Me.Page, Me.tbxBikou)
    End Sub

    ''' <summary>
    ''' �o�^�{�^��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '�`�F�b�N
        If Not chkInputValue() Then
            Exit Sub
        End If

        '�o�^�`�F�b�N
        Dim msg As String
        msg = KihonJyouhouInquiryBc.ChkTourokuryouTouroku(_kameiten_cd, "200", Me.hidUpdTime.Value)
        If msg <> String.Empty Then
            ShowMsg(msg, btnTouroku)
            Exit Sub
        End If

        Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
        If Not otherPageFunction.DoFunction(Parent.Page, "Haitakameiten") Then
            Exit Sub
        End If

        If _Kbn = "A" Then
            '�o�^�`�F�b�N
            msg = KihonJyouhouInquiryBc.ChkTourokuryouTouroku(_kameiten_cd, "210", Me.hidUpdTime1.Value)
            If msg <> String.Empty Then
                ShowMsg(msg, btnTouroku)
                Exit Sub
            End If

        End If

        '�X�V�̃f�[�^���쐬
        Dim insdata As New KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable
        Dim dr As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuRow

        '�X�V�̃f�[�^���쐬
        Dim insdata2 As New KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable
        Dim dr2 As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuRow

        dr = insdata.NewRow
        dr.mise_cd = _kameiten_cd
        dr.add_date = Me.tbxAddDate.Text
        dr.bunrui_cd = "200"
        dr.seikyuusyo_hak_date = Me.tbxSeikyuDate.Text
        dr.uri_date = Me.tbxUriDate.Text
        dr.seikyuu_umu = Me.ddlSeikyuuUmu.SelectedValue
        dr.uri_keijyou_date = Me.tbxUriDate.Text
        dr.syouhin_cd = Me.tbxSyouhinCd.Text
        dr.uri_gaku = Me.tbxZeinuki.Text.Replace(",", "")
        dr.zei_kbn = Me.hidZeikbnTxt.Value
        dr.bikou = Me.tbxBikou.Text
        dr.koumuten_seikyuu_gaku = Me.tbxKoumutenSeikyuuGaku.Text.Replace(",", "")
        dr.add_login_user_id = _upd_login_user_id
        dr.upd_login_user_id = _upd_login_user_id
        dr.syouhizei_gaku = Me.lblSyouhizei.Text.Replace(",", "")
        insdata.Rows.Add(dr)

        '�敪��"A"(FC)�̏ꍇ�A�����̑��i�̓��̓`�F�b�N���s�Ȃ�
        If _Kbn = "A" Then
            dr2 = insdata2.NewRow
            dr2.mise_cd = _kameiten_cd
            dr2.add_date = Me.tbxAddDate1.Text
            dr2.bunrui_cd = "210"
            dr2.seikyuusyo_hak_date = Me.tbxSeikyuDate1.Text
            dr2.uri_date = Me.tbxUriDate1.Text
            dr2.seikyuu_umu = Me.ddlSeikyuuUmu1.SelectedValue
            dr2.uri_keijyou_date = Me.tbxUriDate1.Text
            dr2.syouhin_cd = Me.tbxSyouhinCd1.Text
            dr2.uri_gaku = Me.tbxZeinuki1.Text.Replace(",", "")
            dr2.zei_kbn = Me.hidZeikbnTxt1.Value
            dr2.bikou = Me.tbxBikou1.Text
            dr2.koumuten_seikyuu_gaku = 0
            dr2.add_login_user_id = _upd_login_user_id
            dr2.upd_login_user_id = _upd_login_user_id
            dr2.syouhizei_gaku = Me.lblSyouhizei1.Text.Replace(",", "")
            insdata2.Rows.Add(dr2)
        End If

        Dim updBln As Boolean
        '�o�^
        If _Kbn = "A" Then
            updBln = KihonJyouhouInquiryBc.SetTenbetuSyokiSeikyuu(_kameiten_cd, insdata, insdata2)
        Else
            updBln = KihonJyouhouInquiryBc.SetTenbetuSyokiSeikyuu(_kameiten_cd, insdata, "200")
        End If


        If Not updBln Then
            Dim mei As String = "�o�^��"
            If _Kbn = "A" Then
                mei = mei & "����є̑��i�����c�[����"
            End If
            ShowMsg(Messages.Instance.MSG019E, Me.btnTouroku, mei)
            Exit Sub
        End If

        If _Kbn = "A" Then
            '�o�^��/�����̑��i�̓o�^
            ShowMsg(Messages.Instance.MSG2018E, Me.btnTouroku, "�o�^������є̑��i�����c�[����")

        Else
            '�o�^���̓o�^
            ShowMsg(Messages.Instance.MSG2018E, Me.btnTouroku, "�o�^��")
        End If

        PageInit()

    End Sub

    ''' <summary>
    ''' ���������s��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxSeikyuDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dateValue As String
        dateValue = chkDate(Me.tbxSeikyuDate.Text)
        If dateValue <> "false" Then
            Me.tbxSeikyuDate.Text = dateValue
            msgAndFocus.setFocus(Me.Page, Me.tbxBikou)
        Else
            Me.hidLastFocus.Value = "5"
            ShowMsg("���t�ȊO�����͂���Ă��܂��B", Me.tbxSeikyuDate)
        End If
        Me.hidLastFocus.Value = String.Empty

    End Sub

    ''' <summary>
    ''' �������Ŕ����i
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxUriDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dateValue As String
        dateValue = chkDate(Me.tbxUriDate.Text)
        If dateValue <> "false" Then
            Me.tbxUriDate.Text = dateValue
            msgAndFocus.setFocus(Me.Page, Me.tbxBikou1)
        Else
            ShowMsg("���t�ȊO�����͂���Ă��܂��B", Me.tbxUriDate)
        End If
        Me.hidLastFocus.Value = String.Empty
    End Sub

    ''' <summary>
    ''' �̑��i�����c�[����
    ''' �������Ŕ����i
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxUriDate1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dateValue As String
        dateValue = chkDate(Me.tbxUriDate1.Text)
        If dateValue <> "false" Then
            Me.tbxUriDate1.Text = dateValue
        Else
            ShowMsg("���t�ȊO�����͂���Ă��܂��B", Me.tbxUriDate1)
        End If
    End Sub

    ''' <summary>
    ''' �̑��i�����c�[����
    ''' ���������s��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxSeikyuDate1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dateValue As String
        dateValue = chkDate(Me.tbxSeikyuDate1.Text)
        If dateValue <> "false" Then
            Me.tbxSeikyuDate1.Text = dateValue
        Else
            Me.hidLastFocus.Value = "9"
            ShowMsg("���t�ȊO�����͂���Ă��܂��B", Me.tbxSeikyuDate1)
        End If
        Me.hidLastFocus.Value = String.Empty
    End Sub

    ''' <summary>
    ''' LBTN�o�^��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbtnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnTouroku.Click
        '���ׂ�\��
        If meisaiTbody.Style.Item("display") = "none" Then
            meisaiTbody.Style.Item("display") = "inline"
            btnTouroku.Style.Item("display") = "inline"
        Else
            meisaiTbody.Style.Item("display") = "none"
            btnTouroku.Style.Item("display") = "none"
        End If

    End Sub
#End Region

#Region "����"

    ''' <summary>
    ''' ��ʏ�����
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PageInit()

        Setkenngen()

        BindJavaScriptEvent()

        SetGamenValue(True)

    End Sub

    ''' <summary>
    ''' Javascript Event Bind
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindJavaScriptEvent()

        '�N��
        Me.tbxAddDate.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbxAddDate1.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbxSeikyuDate.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbxSeikyuDate1.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")


        'IE FIREROX �o�^���ύX����Ȃ�
        Me.tbxAddDate.Attributes.Add("onblur", "LostFocusPostBack(this,1)")
        Me.tbxAddDate1.Attributes.Add("onblur", "LostFocusPostBack(this,6)")
        Me.tbxSeikyuDate.Attributes.Add("onblur", "LostFocusPostBack(this,5)")
        Me.tbxSeikyuDate1.Attributes.Add("onblur", "LostFocusPostBack(this,9)")


        Me.tbxSyouhinCd.Attributes.Add("onblur", "fncToUpper(this);LostFocusPostBack(this,2)")

        Me.tbxZeinuki.Attributes.Add("onblur", "LostFocusPostBack(this,3)")
        Me.tbxKoumutenSeikyuuGaku.Attributes.Add("onblur", "LostFocusPostBack(this,4)")
        Me.tbxZeinuki1.Attributes.Add("onblur", "LostFocusPostBack(this,8)")

        Me.tbxSyouhinCd1.Attributes.Add("onblur", "fncToUpper(this);LostFocusPostBack(this,7)")

        '���z����
        Me.tbxKoumutenSeikyuuGaku.Attributes.Add("onfocus", "return SetKingaku(this)")
        Me.tbxZeinuki1.Attributes.Add("onfocus", "return SetKingaku(this)")
        Me.tbxZeinuki.Attributes.Add("onfocus", "return SetKingaku(this)")

        Me.tbxBikou.Attributes.Add("onfocus", "return GetFocusOperate(this)")
        Me.tbxBikou1.Attributes.Add("onfocus", "return GetFocusOperate(this)")

        Me.tbxSyouhinCd.Attributes.Add("onfocus", "return GetFocusOperate(this)")
        Me.tbxSyouhinCd1.Attributes.Add("onfocus", "return GetFocusOperate(this)")

        'popup
        Me.btnKansaku.Attributes.Add("onclick", "syainCdHenkouKbn=true;fncOpenwindowSyouhin(200);return false;")
        Me.btnKansaku1.Attributes.Add("onclick", "syainCdHenkouKbn=true;fncOpenwindowSyouhin(210);return false;")

        Me.ddlSeikyuuUmu.Attributes.Add("onchange", "ShowModal();")
        Me.ddlSeikyuuUmu1.Attributes.Add("onchange", "ShowModal();")


    End Sub

    ''' <summary>
    ''' ��ʂ̒l��ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGamenValue(Optional ByVal blnFirst As Boolean = False)

        Dim kameitenTableDataTable As KameitenDataSet.m_kameitenTableDataTable
        Dim tenbetuSyokiseikyu As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable

        '�����X���擾
        kameitenTableDataTable = KihonJyouhouInquiryBc.GetkameitenInfo(_kameiten_cd)

        '�o�^���̃f�[�^���擾
        tenbetuSyokiseikyu = KihonJyouhouInquiryBc.GetTenbetuSyokiSeikyu(_kameiten_cd, "200")

        Me.ddlSeikyuuUmu.SelectedIndex = 1
        Me.ddlSeikyuuUmu1.SelectedIndex = 1

        If Not kameitenTableDataTable(0).Istys_seikyuu_sakiNull Then
            '�n���R  2010/08/17�@����������˒���������R�[�h��ύX���� ��
            'If kameitenTableDataTable(0).tys_seikyuu_saki = _MiseCode Then
            If kameitenTableDataTable(0).tys_seikyuu_saki_cd = _MiseCode Then

                lblSeikyusaki.Text = "���ڐ���"
                hidSeikyusaki.Value = "��"
            Else
                lblSeikyusaki.Text = "������"
                hidSeikyusaki.Value = "��"
            End If
        Else
            lblSeikyusaki.Text = "������"
            hidSeikyusaki.Value = "��"
        End If

        If _Kbn = "A" Then
            lblSeikyusaki.Text = "�e�b����"
        End If



        '�f�[�^���鎞
        If tenbetuSyokiseikyu.Rows.Count > 0 Then

            '�o�^��
            If Not tenbetuSyokiseikyu(0).Isadd_dateNull Then
                Me.tbxAddDate.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).add_date).ToString(YMD)
            End If

            '�����L��
            If Not tenbetuSyokiseikyu(0).Isseikyuu_umuNull Then
                If tenbetuSyokiseikyu(0).seikyuu_umu = 1 Then
                    Me.ddlSeikyuuUmu.SelectedIndex = 1
                ElseIf tenbetuSyokiseikyu(0).seikyuu_umu = 0 Then
                    Me.ddlSeikyuuUmu.SelectedIndex = 0
                Else
                    Me.ddlSeikyuuUmu.SelectedIndex = 1
                End If
            Else
                Me.ddlSeikyuuUmu.SelectedIndex = 1
            End If

            '���iCD�@��
            If Not tenbetuSyokiseikyu(0).Issyouhin_cdNull Then
                Me.tbxSyouhinCd.Text = tenbetuSyokiseikyu(0).syouhin_cd
                Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable
                syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(tenbetuSyokiseikyu(0).syouhin_cd)
                If syouhinDataTable.Rows.Count > 0 Then
                    If Not syouhinDataTable(0).Issyouhin_meiNull Then
                        Me.lblSyouhinMei.Text = syouhinDataTable(0).syouhin_mei
                        Me.tbxSyouhinCd.Text = syouhinDataTable(0).syouhin_cd

                    End If

                    If Not syouhinDataTable(0).Ishyoujun_kkkNull Then
                        Me.hidHyoujunkakaku.Value = syouhinDataTable(0).hyoujun_kkk
                    Else
                        Me.hidHyoujunkakaku.Value = "0"
                    End If

                    If Not syouhinDataTable(0).Iszei_kbnNull Then
                        Me.hidZeikbn.Value = syouhinDataTable(0).zei_kbn
                    Else
                        Me.hidZeikbn.Value = ""
                    End If


                End If

            End If

            '�������Ŕ����z
            If Not tenbetuSyokiseikyu(0).Isuri_gakuNull Then
                Me.tbxZeinuki.Text = SetKingaku(tenbetuSyokiseikyu(0).uri_gaku, False)
            End If

            '�����
            If blnFirst Then
                If Not tenbetuSyokiseikyu(0).Iszei_kbnNull Then
                    Me.hidZeikbnTxt.Value = tenbetuSyokiseikyu(0).zei_kbn

                End If
                If tenbetuSyokiseikyu(0).syouhizei_gaku = "0" Then
                    '����łɂ��Z�b�g����

                    Me.lblSyouhizei.Text = 0
                Else
                    '����ŋ敪����ŗ������߂�
                    Me.lblSyouhizei.Text = SetKingaku(tenbetuSyokiseikyu(0).syouhizei_gaku)

                    '����ŋ敪����ŗ������߂�
                End If
                Me.lblZeikomi.Text = SetKingaku((SetVal(SetDouble(tbxZeinuki.Text.Replace(",", ""))) + SetVal(lblSyouhizei.Text)))

            Else

                If Not tenbetuSyokiseikyu(0).Iszei_kbnNull Then
                    Me.hidZeikbnTxt.Value = tenbetuSyokiseikyu(0).zei_kbn
                    Dim syouhizei As String
                    syouhizei = (KihonJyouhouInquiryBc.GetKakaku(Me.hidZeikbnTxt.Value))
                    If syouhizei = "0" Then
                        '����łɂ��Z�b�g����
                        Me.lblSyouhizei.Text = SetKingaku(syouhizei)
                    Else
                        '����ŋ敪����ŗ������߂�
                        Me.lblSyouhizei.Text = SetKingaku((Fix(SetVal(SetDouble(Me.lblSyouhizei.Text.Replace(",", "")) * SetVal(syouhizei * 100) / 100))))
                        '����ŋ敪����ŗ������߂�
                    End If
                End If
                '�ō����z���Z�o����
                Me.lblZeikomi.Text = SetKingaku((SetVal(SetDouble(tbxZeinuki.Text.Replace(",", ""))) + SetVal(lblSyouhizei.Text)))
            End If


            '�H���X����
            If Not tenbetuSyokiseikyu(0).Iskoumuten_seikyuu_gakuNull Then
                Me.tbxKoumutenSeikyuuGaku.Text = SetKingaku((tenbetuSyokiseikyu(0).koumuten_seikyuu_gaku), False)
            End If

            '���������s��
            If Not tenbetuSyokiseikyu(0).Isseikyuusyo_hak_dateNull Then
                Me.tbxSeikyuDate.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).seikyuusyo_hak_date).ToString(YMD)
            End If

            '����N����
            If Not tenbetuSyokiseikyu(0).Isuri_dateNull Then
                Me.tbxUriDate.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).uri_date).ToString(YMD)
            End If

            If Not tenbetuSyokiseikyu(0).IsbikouNull Then
                Me.tbxBikou.Text = tenbetuSyokiseikyu(0).bikou
            End If

            'If Not kameitenTableDataTable(0).Ishansokuhin_seikyuusakiNull Then
            '    If kameitenTableDataTable(0).hansokuhin_seikyuusaki = _MiseCode Then
            '        lblSeikyusaki.Text = "���ڐ���"
            '    Else
            '        lblSeikyusaki.Text = "������"
            '    End If
            'Else

            'End If

            If Not kameitenTableDataTable(0).Isupd_datetimeNull Then
                Me.hidUpdTime.Value = Convert.ToDateTime(tenbetuSyokiseikyu(0).upd_datetime).ToString("yyyy/MM/dd HH:mm:ss")
            End If
            If Not blnFirst Then
                SetKingaku()
            End If
            If tenbetuSyokiseikyu(0).Isuri_keijyou_flgNull OrElse tenbetuSyokiseikyu(0).uri_keijyou_flg <> "1" Then
                Me.lblUriageKeijou.Text = ""

                If tenbetuSyokiseikyu(0).Isseikyuu_umuNull OrElse tenbetuSyokiseikyu(0).seikyuu_umu <> 1 Then

                    LockItemTextbox(Me.tbxZeinuki)
                    LockItemTextbox(Me.tbxSeikyuDate)
                    LockItemTextbox(Me.tbxKoumutenSeikyuuGaku)


                Else
                    '�H���X�������z�̏��
                    If (hidSeikyusaki.Value = "��" Or _keiretuCd <> "THTH" Or _
                                _keiretuCd <> "0001" Or _keiretuCd <> "NF03") Then

                        UnLockItemTextbox(tbxKoumutenSeikyuuGaku)
                    Else
                        LockItemTextbox(tbxKoumutenSeikyuuGaku)
                    End If

                End If

            Else

                Me.lblUriageKeijou.Text = "���㏈����"
                LockItemTextbox(Me.tbxAddDate)
                LockItemdrp(Me.ddlSeikyuuUmu)
                LockItemTextbox(Me.tbxSyouhinCd)
                LockItemTextbox(Me.tbxZeinuki)
                LockItemTextbox(Me.tbxKoumutenSeikyuuGaku)
                LockItemTextbox(Me.tbxSeikyuDate)

                Me.btnKansaku.Enabled = False

            End If

        Else

            If (hidSeikyusaki.Value = "��" AndAlso _keiretuCd <> "THTH" AndAlso _
            _keiretuCd <> "0001" AndAlso _keiretuCd <> "NF03") Then
                LockItemTextbox(tbxKoumutenSeikyuuGaku)
            Else
                UnLockItemTextbox(tbxKoumutenSeikyuuGaku)
            End If

            ClearArea()

        End If




        If _Kbn = "A" Then



            '�̑��i�����c�[����
            tenbetuSyokiseikyu = KihonJyouhouInquiryBc.GetTenbetuSyokiSeikyu(_kameiten_cd, "210")
            If tenbetuSyokiseikyu.Rows.Count > 0 Then


                If Not tenbetuSyokiseikyu(0).Isadd_dateNull Then
                    Me.tbxAddDate1.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).add_date).ToString(YMD)
                End If

                '�����L��
                If Not tenbetuSyokiseikyu(0).Isseikyuu_umuNull Then
                    If tenbetuSyokiseikyu(0).seikyuu_umu = 1 Then
                        Me.ddlSeikyuuUmu1.SelectedIndex = 1
                    ElseIf tenbetuSyokiseikyu(0).seikyuu_umu = 0 Then
                        Me.ddlSeikyuuUmu1.SelectedIndex = 0
                    Else
                        Me.ddlSeikyuuUmu1.SelectedIndex = 1
                    End If
                Else
                    Me.ddlSeikyuuUmu1.SelectedIndex = 1
                End If

                '���iCD�@��
                If Not tenbetuSyokiseikyu(0).Issyouhin_cdNull Then

                    Me.tbxSyouhinCd1.Text = tenbetuSyokiseikyu(0).syouhin_cd
                    Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable

                    syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(tenbetuSyokiseikyu(0).syouhin_cd)
                    If syouhinDataTable.Rows.Count > 0 Then
                        If Not syouhinDataTable(0).Issyouhin_meiNull Then
                            Me.lblSyouhinMei1.Text = syouhinDataTable(0).syouhin_mei
                            Me.tbxSyouhinCd1.Text = syouhinDataTable(0).syouhin_cd
                        End If

                        If Not syouhinDataTable(0).Ishyoujun_kkkNull Then
                            Me.hidHyoujunkakaku1.Value = syouhinDataTable(0).hyoujun_kkk
                        Else
                            Me.hidHyoujunkakaku1.Value = "0"
                        End If

                        If Not syouhinDataTable(0).Iszei_kbnNull Then
                            Me.hidZeikbn1.Value = syouhinDataTable(0).zei_kbn
                        Else
                            Me.hidZeikbn1.Value = ""
                        End If
                    End If
                End If

                '�������Ŕ����z
                If Not tenbetuSyokiseikyu(0).Isuri_gakuNull Then
                    Me.tbxZeinuki1.Text = SetKingaku(tenbetuSyokiseikyu(0).uri_gaku, False)
                End If

                If blnFirst Then
                    If Not tenbetuSyokiseikyu(0).Iszei_kbnNull Then
                        Me.hidZeikbnTxt1.Value = tenbetuSyokiseikyu(0).zei_kbn

                    End If
                    If tenbetuSyokiseikyu(0).syouhizei_gaku = "0" Then
                        '����łɂ��Z�b�g����

                        Me.lblSyouhizei1.Text = 0
                    Else
                        '����ŋ敪����ŗ������߂�
                        Me.lblSyouhizei1.Text = SetKingaku(tenbetuSyokiseikyu(0).syouhizei_gaku)

                        '����ŋ敪����ŗ������߂�
                    End If
                    Me.lblZeikomi1.Text = SetKingaku((SetVal(SetDouble(tbxZeinuki1.Text.Replace(",", ""))) + SetVal(lblSyouhizei1.Text)))

                Else
                    '�����
                    If Not tenbetuSyokiseikyu(0).Iszei_kbnNull Then

                        Me.hidZeikbnTxt1.Value = tenbetuSyokiseikyu(0).zei_kbn
                        Dim syouhizei As String
                        syouhizei = (KihonJyouhouInquiryBc.GetKakaku(Me.hidZeikbnTxt1.Value))
                        If syouhizei = "0" Then
                            '����łɂ��Z�b�g����
                            Me.lblSyouhizei1.Text = SetKingaku(syouhizei)

                        Else
                            '����ŋ敪����ŗ������߂�
                            Me.lblSyouhizei1.Text = SetKingaku((Fix(SetVal(SetDouble(Me.lblSyouhizei1.Text.Replace(",", ""))) * SetVal(syouhizei * 100) / 100)))
                            '����ŋ敪����ŗ������߂�
                        End If
                    End If

                    '�ō����z���Z�o����
                    Me.lblZeikomi1.Text = SetKingaku((SetVal(SetDouble(tbxZeinuki1.Text.Replace(",", ""))) + SetVal(lblSyouhizei1.Text)))
                End If


                '���������s��
                If Not tenbetuSyokiseikyu(0).Isseikyuusyo_hak_dateNull Then
                    Me.tbxSeikyuDate1.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).seikyuusyo_hak_date).ToString(YMD)
                End If

                '����N����
                If Not tenbetuSyokiseikyu(0).Isuri_dateNull Then
                    Me.tbxUriDate1.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).uri_date).ToString(YMD)
                End If

                If Not tenbetuSyokiseikyu(0).IsbikouNull Then
                    Me.tbxBikou1.Text = tenbetuSyokiseikyu(0).bikou
                End If

                If Not kameitenTableDataTable(0).Isupd_datetimeNull Then
                    Me.hidUpdTime1.Value = Convert.ToDateTime(tenbetuSyokiseikyu(0).upd_datetime).ToString("yyyy/MM/dd HH:mm:ss")
                End If

                If Not blnFirst Then
                    SetKingaku1()
                End If
                If tenbetuSyokiseikyu(0).Isuri_keijyou_flgNull OrElse tenbetuSyokiseikyu(0).uri_keijyou_flg <> "1" Then

                    Me.lblUriageKeijou1.Text = ""
                    If tenbetuSyokiseikyu(0).Isseikyuu_umuNull OrElse tenbetuSyokiseikyu(0).seikyuu_umu <> 1 Then
                        LockItemTextbox(Me.tbxZeinuki1)
                        LockItemTextbox(Me.tbxSeikyuDate1)

                    Else

                    End If

                Else
                    Me.lblUriageKeijou1.Text = "���㏈����"
                    LockItemTextbox(Me.tbxAddDate1)
                    LockItemdrp(Me.ddlSeikyuuUmu1)
                    LockItemTextbox(Me.tbxSyouhinCd1)
                    LockItemTextbox(Me.tbxZeinuki1)
                    LockItemTextbox(Me.tbxSeikyuDate1)
                    Me.btnKansaku1.Enabled = False
                End If

            Else
                ClearArea1()
            End If

        Else
            '���ڂ���͕s�ɂ���
            LockItemTextbox(Me.tbxAddDate1)
            LockItemdrp(Me.ddlSeikyuuUmu1)
            LockItemTextbox(Me.tbxSyouhinCd1)
            LockItemTextbox(Me.tbxZeinuki1)
            LockItemTextbox(Me.tbxSeikyuDate1)
            LockItemTextbox(Me.tbxBikou1)
            Me.btnKansaku1.Enabled = False
        End If

        hidAutoKoumuFlg.Value = "False"
        hidAutoJituFlg.Value = "False"
    End Sub

    ''' <summary>
    ''' �o�^�����̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setkenngen()

        '�o�^����
        If _kenngenn = True Then
            Me.btnTouroku.Enabled = True
        Else
            Me.btnTouroku.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chkInputValue() As Boolean

        '�o�^���̓��̓`�F�b�N
        '�K�{���̓`�F�b�N
        '�o�^��
        Dim commonCheck As New CommonCheck

        If commonCheck.CheckYuukouHiduke(Me.tbxAddDate.Text, "�o�^��") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate, commonCheck.CheckYuukouHiduke(Me.tbxAddDate.Text, "�o�^��"))

        End If

        If Me.tbxAddDate.Text = String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate, Messages.Instance.MSG013E, "�o�^��")
        End If


        If Me.tbxSyouhinCd.Text = String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxSyouhinCd, Messages.Instance.MSG013E, "���i�R�[�h")
        End If


        '���t�`�F�b�N
        '�o�^��
        If Me.tbxAddDate.Text <> String.Empty Then
            If IsDate(Me.tbxAddDate.Text) = False Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate, "�o�^���͓��t�ȊO�����͂���Ă��܂��B\r\n")
            End If
        End If

        '���������s��
        If Me.tbxSeikyuDate.Text <> String.Empty Then
            If IsDate(Me.tbxSeikyuDate.Text) = False Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxSeikyuDate, "���������s���͓��t�ȊO�����͂���Ă��܂��B\r\n")
            End If
        End If



        '�����`�F�b�N
        '�Ŕ����z
        If Me.tbxZeinuki.Text <> String.Empty Then
            If Me.tbxZeinuki.Text.Replace(",", "").Length > 8 Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxZeinuki, Messages.Instance.MSG2021E, "�������Ŕ����z")
            End If
        End If

        If Me.tbxKoumutenSeikyuuGaku.Text <> String.Empty Then
            If Me.tbxKoumutenSeikyuuGaku.Text.Replace(",", "").Length > 8 Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxKoumutenSeikyuuGaku, Messages.Instance.MSG2021E, "�H���X�����Ŕ����z")
            End If
        End If


        '���l
        If Not chkKetaSuu(Me.tbxBikou, Me.tbxBikou.Text, "���l", 30, 2) Then
            '�I������
            'Return False
        End If

        Dim chkobj As New CommonCheck


        If chkobj.CheckKinsoku(Me.tbxBikou.Text, "���l") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou, chkobj.CheckKinsoku(Me.tbxBikou.Text, "���l"))
            '�I������
        End If


        If _Kbn = "A" Then
            '�o�^���̓��̓`�F�b�N
            '�K�{���̓`�F�b�N
            '�o�^��
            If commonCheck.CheckYuukouHiduke(Me.tbxAddDate1.Text, "�z����") <> String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate1, commonCheck.CheckYuukouHiduke(Me.tbxAddDate1.Text, "�z����"))

            End If

            If Me.tbxAddDate1.Text = String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate1, Messages.Instance.MSG013E, "�z����")
            End If

            If Me.tbxSyouhinCd1.Text = String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxSyouhinCd1, Messages.Instance.MSG013E, "���i�R�[�h")
            End If

            '���t�`�F�b�N
            '�z����
            If Me.tbxAddDate1.Text <> String.Empty Then
                If IsDate(Me.tbxAddDate1.Text) = False Then
                    msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate1, "�z�����͓��t�ȊO�����͂���Ă��܂��B\r\n")
                End If
            End If

            '���������s��
            If Me.tbxSeikyuDate1.Text <> String.Empty Then
                If IsDate(Me.tbxSeikyuDate1.Text) = False Then
                    msgAndFocus.AppendMsgAndCtrl(Me.tbxSeikyuDate1, "���������s���͓��t�ȊO�����͂���Ă��܂��B\r\n")
                End If
            End If

            '�����`�F�b�N
            '�Ŕ����z
            If Me.tbxZeinuki1.Text <> String.Empty Then
                If Me.tbxZeinuki1.Text.Replace(",", "").Length > 8 Then
                    msgAndFocus.AppendMsgAndCtrl(Me.tbxZeinuki1, Messages.Instance.MSG2021E, "�������Ŕ����z")
                End If
            End If

            '���l
            If Not chkKetaSuu(Me.tbxBikou1, Me.tbxBikou1.Text, "���l", 30, 2) Then

            End If

            If chkobj.CheckKinsoku(Me.tbxBikou1.Text, "���l") <> String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou1, chkobj.CheckKinsoku(Me.tbxBikou1.Text, "���l"))
                '�I������
            End If

        End If
        ''���b�Z�[�W�\��
        If msgAndFocus.Message <> String.Empty Then
            ShowMsg(msgAndFocus.Message, msgAndFocus.focusCtrl)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' �������͍��ڂ��Đݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OperateSeikyuData()

        hidAutoKoumuFlg.Value = "False"
        hidAutoJituFlg.Value = "False"

        If Me.ddlSeikyuuUmu.SelectedValue = "1" Then

            '���i�R�[�h�̖����̓`�F�b�N
            If Me.tbxSyouhinCd.Text.Trim = String.Empty Then
                Exit Sub
            End If

            If KihonJyouhouInquiryBc.GetkeiretuCd(_kameiten_cd) <> _keiretuCd Then
                Me.btnTouroku.Enabled = False
                ShowMsg("���̃��[�U�[�ɍX�V����Ă��܂��A��ʂ�������x��荞��ł��������B", Me.btnTouroku)
                Exit Sub
            End If

            Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable
            syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd.Text.Trim)

            If Not syouhinDataTable(0).Iszei_kbnNull Then
                Me.hidZeikbn.Value = syouhinDataTable(0).zei_kbn
            Else
                Me.hidZeikbn.Value = ""
            End If


            ' �Ŕ����z����͉\�ɂ���
            UnLockItemTextbox(tbxZeinuki)

            '�o�^���ŁA���ڐ����ڐ����܂��n��̏ꍇ�H���X�������z����͉\�ɂ���
            If (hidSeikyusaki.Value = "��" Or _keiretuCd = "THTH" Or _
                        _keiretuCd = "0001" Or _keiretuCd = "NF03") Then

                UnLockItemTextbox(tbxKoumutenSeikyuuGaku)
            End If

            '���������s������͉\�ɂ���
            UnLockItemTextbox(tbxSeikyuDate)


            koumuten = SetDouble(hidHyoujunkakaku.Value)
            jitu = SetDouble(hidHyoujunkakaku.Value)

            If GetMiseSeikyuu("A", Me.hidSeikyusaki.Value, _keiretuCd, _
                                            Me.tbxSyouhinCd.Text) = "True" Then

                Me.tbxZeinuki.Text = SetKingaku(jitu, False)
                Me.tbxKoumutenSeikyuuGaku.Text = SetKingaku(koumuten, False)
            End If

            ' �ŋ敪���擾����
            Me.hidZeikbnTxt.Value = Me.hidZeikbn.Value

            '���z���Čv�Z����
            SetKingaku()


            '�o�^���̓��̓`�F�b�N

            If Me.tbxAddDate.Text <> String.Empty Then

                '���������s���A����N������ݒ肷��
                SetDate(True)

            End If

            Me.tbxSyouhinCd.Focus()

        Else
            '�o�^��
            Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable

            If _Kbn = "A" Then

                'FC�X
                '���i�R�[�h="J0099"���Z�b�g
                Me.tbxSyouhinCd.Text = "J0099"
            Else
                'FC�X�ȊO
                '���i�R�[�h="C0099"���Z�b�g
                Me.tbxSyouhinCd.Text = "C0099"

            End If

            syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd.Text)
            If syouhinDataTable.Rows.Count > 0 Then

                If Not syouhinDataTable(0).Issyouhin_meiNull Then
                    Me.lblSyouhinMei.Text = syouhinDataTable(0).syouhin_mei
                    Me.tbxSyouhinCd.Text = syouhinDataTable(0).syouhin_cd
                End If

                '�W�����i
                If Not syouhinDataTable(0).Ishyoujun_kkkNull Then
                    tbxZeinuki.Text = SetKingaku(syouhinDataTable(0).hyoujun_kkk, False)
                Else
                    tbxZeinuki.Text = String.Empty
                End If

                '�ŋ敪
                If Not syouhinDataTable(0).Iszei_kbnNull Then
                    Me.hidZeikbnTxt.Value = syouhinDataTable(0).zei_kbn
                Else
                    hidZeikbnTxt.Value = String.Empty
                End If

            Else
                tbxZeinuki.Text = String.Empty
                Me.hidZeikbn.Value = String.Empty
            End If

            '���z���Z�o����
            SetKingaku()

            '�Ŕ����z����͕s�ɂ���
            LockItemTextbox(tbxZeinuki)

            '�H���X�������z��ݒ肷��
            tbxKoumutenSeikyuuGaku.Text = tbxZeinuki.Text

            '�H���X�������z����͕s�ɂ���
            LockItemTextbox(tbxKoumutenSeikyuuGaku)

            '���������s��������������
            tbxSeikyuDate.Text = String.Empty

            '����N�����̍X�V(�V�X�e�����t)
            Me.tbxUriDate.Text = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate).ToString(YMD)

            '���������s������͕s�ɂ���
            LockItemTextbox(tbxSeikyuDate)

            '���l�Ƀt�H�[�J�X���Z�b�g����
            msgAndFocus.setFocus(Me.Page, tbxBikou)

        End If

    End Sub

    ''' <summary>
    ''' �������͍��ڂ��Đݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OperateSeikyuData1()

        If Me.ddlSeikyuuUmu1.SelectedValue = "1" Then

            '���i�R�[�h�̖����̓`�F�b�N
            If Me.tbxSyouhinCd1.Text.Trim = String.Empty Then
                Exit Sub
            End If
            Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable
            syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd1.Text.Trim)

            If Not syouhinDataTable(0).Iszei_kbnNull Then
                Me.hidZeikbn1.Value = syouhinDataTable(0).zei_kbn
            Else
                Me.hidZeikbn1.Value = ""
            End If

            ' �Ŕ����z����͉\�ɂ���
            UnLockItemTextbox(tbxZeinuki1)
            '���������s������͉\�ɂ���
            UnLockItemTextbox(tbxSeikyuDate1)
            '�����̑��̏ꍇ�A���������z�ɕW�����i��ݒ�
            tbxZeinuki1.Text = SetKingaku(hidHyoujunkakaku1.Value, False)

            ' �ŋ敪���擾����
            Me.hidZeikbnTxt1.Value = Me.hidZeikbn1.Value

            '���z���Čv�Z����
            SetKingaku1()


            '�o�^���̓��̓`�F�b�N

            If Me.tbxAddDate1.Text <> String.Empty Then

                '���������s���A����N������ݒ肷��
                SetDate1(True)

            End If

            Me.tbxSyouhinCd1.Focus()

        Else
            '�o�^��
            Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable

            Me.tbxSyouhinCd1.Text = "K0099"
            syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin("K0099")
            If Not syouhinDataTable(0).Issyouhin_meiNull Then
                Me.lblSyouhinMei1.Text = syouhinDataTable(0).syouhin_mei
                Me.tbxSyouhinCd1.Text = syouhinDataTable(0).syouhin_cd
            End If


            '�W�����i
            If Not syouhinDataTable(0).Ishyoujun_kkkNull Then
                tbxZeinuki1.Text = SetKingaku(syouhinDataTable(0).hyoujun_kkk, False)
            Else
                tbxZeinuki1.Text = String.Empty
            End If

            '�ŋ敪
            If Not syouhinDataTable(0).Iszei_kbnNull Then
                Me.hidZeikbnTxt1.Value = syouhinDataTable(0).zei_kbn
            Else
                hidZeikbnTxt1.Value = String.Empty
            End If

            '���z���Z�o����
            SetKingaku1()

            '�Ŕ����z����͕s�ɂ���
            LockItemTextbox(tbxZeinuki1)


            '���������s��������������
            tbxSeikyuDate1.Text = String.Empty

            '����N�����̍X�V(�V�X�e�����t)
            Me.tbxUriDate1.Text = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate).ToString(YMD)

            '���������s������͕s�ɂ���
            LockItemTextbox(tbxSeikyuDate1)

            '���l�Ƀt�H�[�J�X���Z�b�g����
            Me.tbxBikou1.Focus()


        End If

    End Sub

    ''' <summary>
    '''  ���i�R�[�h�̕ύX�ɂ��e���ڂ��Đݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Private Function UpdShouhinCd() As Boolean

        hidAutoKoumuFlg.Value = "False"
        hidAutoJituFlg.Value = "False"

        '�����̓`�F�b�N
        If Me.tbxSyouhinCd.Text = String.Empty Then
            Me.lblSyouhinMei.Text = String.Empty
            Return True
        End If

        If KihonJyouhouInquiryBc.GetkeiretuCd(_kameiten_cd) <> _keiretuCd Then
            Me.btnTouroku.Enabled = False
            ShowMsg("���̃��[�U�[�ɍX�V����Ă��܂��A��ʂ�������x��荞��ł��������B", Me.btnTouroku)
            Return False
        End If

        Dim syouhin As KameitenjyushoDataSet.m_syouhinDataTable
        syouhin = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd.Text, "200")

        If syouhin.Rows.Count > 0 Then
            '�ŋ敪���擾����
            If Not syouhin(0).Iszei_kbnNull Then
                Me.hidZeikbn.Value = syouhin(0).zei_kbn
            End If
            '���i��
            Me.lblSyouhinMei.Text = syouhin(0).syouhin_mei
            Me.tbxSyouhinCd.Text = syouhin(0).syouhin_cd
            If Not syouhin(0).Ishyoujun_kkkNull Then
                Me.hidHyoujunkakaku.Value = syouhin(0).hyoujun_kkk
            Else
                Me.hidHyoujunkakaku.Value = "0"
            End If

            If Not syouhin(0).Iszei_kbnNull Then
                Me.hidZeikbnTxt.Value = syouhin(0).zei_kbn
            Else
                Me.hidZeikbnTxt.Value = ""
            End If

        Else
            Me.hidLastFocus.Value = "2"
            ShowMsg(Messages.Instance.MSG2008E, Me.tbxSyouhinCd, "���i�R�[�h")
            Me.tbxSyouhinCd.Text = String.Empty
            Me.lblSyouhinMei.Text = String.Empty
            Return False
        End If
        Me.hidLastFocus.Value = String.Empty

        '�����L���̔���
        If Me.ddlSeikyuuUmu.SelectedValue = "1" Then
            '�����L��
            '�o�^���̏ꍇ�A�����ݒ�
            koumuten = hidHyoujunkakaku.Value
            jitu = hidHyoujunkakaku.Value

            If GetMiseSeikyuu("A", Me.hidSeikyusaki.Value, _keiretuCd, _
                                            Me.tbxSyouhinCd.Text) = "True" Then
                Me.tbxZeinuki.Text = SetKingaku(jitu, False)
                Me.tbxKoumutenSeikyuuGaku.Text = SetKingaku(koumuten, False)
            End If

            '���z���Čv�Z����
            Call SetKingaku()

            '�o�^���̓��̓`�F�b�N
            If Me.tbxAddDate.Text <> String.Empty Then
                '���������s���A����N������ݒ肷��
                SetDate(False)
            End If
        End If
        Return True
    End Function

    ''' <summary>
    '''  ���i�R�[�h�̕ύX�ɂ��e���ڂ��Đݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Private Function UpdShouhinCd1() As Boolean
        '�����̓`�F�b�N
        If Me.tbxSyouhinCd1.Text = String.Empty Then
            Me.lblSyouhinMei1.Text = String.Empty
            Return True
        End If

        Dim syouhin As KameitenjyushoDataSet.m_syouhinDataTable
        syouhin = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd1.Text, "210")

        If syouhin.Rows.Count > 0 Then
            '�ŋ敪���擾����
            If Not syouhin(0).Iszei_kbnNull Then
                Me.hidZeikbn1.Value = syouhin(0).zei_kbn
            End If
            '���i��
            Me.lblSyouhinMei1.Text = syouhin(0).syouhin_mei
            Me.tbxSyouhinCd1.Text = syouhin(0).syouhin_cd
            If Not syouhin(0).Ishyoujun_kkkNull Then
                Me.hidHyoujunkakaku1.Value = syouhin(0).hyoujun_kkk
            Else
                Me.hidHyoujunkakaku1.Value = "0"
            End If

            If Not syouhin(0).Iszei_kbnNull Then
                Me.hidZeikbnTxt1.Value = syouhin(0).zei_kbn
            Else
                Me.hidZeikbnTxt1.Value = ""
            End If
        Else
            Me.hidLastFocus.Value = "7"
            ShowMsg(Messages.Instance.MSG2008E, Me.tbxSyouhinCd1, "���i�R�[�h")
            Me.tbxSyouhinCd1.Text = String.Empty
            Me.lblSyouhinMei1.Text = String.Empty
            Return False
        End If
        Me.hidLastFocus.Value = String.Empty

        '�����L���̔���
        If Me.ddlSeikyuuUmu1.SelectedValue = "1" Then

            '�����̑��̏ꍇ�A���������z�ɕW�����i��ݒ�
            tbxZeinuki1.Text = SetKingaku(hidHyoujunkakaku1.Value, False)

            '���z���Čv�Z����
            Call SetKingaku1()

            '�o�^���̓��̓`�F�b�N
            If Me.tbxAddDate1.Text <> String.Empty Then

                '���������s���A����N������ݒ肷��
                SetDate1(False)

            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' �o�^��
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Zeinuki() As Boolean

        koumuten = 0
        jitu = 0
        '���i���ނ������͂̏ꍇ�͏����Ȃ�

        If Me.tbxSyouhinCd.Text.Trim = String.Empty Then
            Return False
        End If

        If KihonJyouhouInquiryBc.GetkeiretuCd(_kameiten_cd) <> _keiretuCd Then
            Me.btnTouroku.Enabled = False
            ShowMsg("���̃��[�U�[�ɍX�V����Ă��܂��A��ʂ�������x��荞��ł��������B", Me.btnTouroku)
            Return False
        End If

        '���������z��long�^�̐������z���Ă����ꍇ��
        '�H���X���������ݒ�͍s�Ȃ�Ȃ��i����ŁE�ō����z�v�Z�̂݁j
        If SetDouble(tbxZeinuki.Text.Trim) > SetDouble("2147483647") Then
            '���z���Čv�Z����
            SetKingaku()
            Return False
        End If

        If hidSeikyusaki.Value = "��" Or hidAutoJituFlg.Value = "False" Then

            '���ڐ����ڐ����������͎����������ݒ薢���{�̏ꍇ�A�����ݒ�
            jitu = IIf(Me.tbxZeinuki.Text = String.Empty, 0, SetDouble(SetKingaku(Me.tbxZeinuki.Text, False)))

            If GetMiseSeikyuu("B", Me.hidSeikyusaki.Value, _keiretuCd, _
                                           Me.tbxSyouhinCd.Text) = "True" Then

                '�擾�������z���H���X�������z�ɐݒ�
                Me.tbxKoumutenSeikyuuGaku.Text = SetKingaku(koumuten, False)
                '�H���X�������z�����ݒ�t���O�n�m
                hidAutoKoumuFlg.Value = "True"
            End If
        End If

        '���z���Čv�Z����
        Call SetKingaku()
        Return True
    End Function

    ''' <summary>
    ''' �̑��i�����c�[����
    ''' ������
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Zeinuki1() As Boolean

        koumuten = 0
        jitu = 0

        '���z���Čv�Z����
        Call SetKingaku1()
        Return True
    End Function

    ''' <summary>
    ''' �H���X�����ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKoumutenSeikyuu() As Boolean

        koumuten = 0
        jitu = 0

        '���i���ނ������͂̏ꍇ�͏����Ȃ�
        If Me.tbxSyouhinCd.Text.Trim = String.Empty Then
            Return False
        End If

        If KihonJyouhouInquiryBc.GetkeiretuCd(_kameiten_cd) <> _keiretuCd Then
            Me.btnTouroku.Enabled = False
            ShowMsg("���̃��[�U�[�ɍX�V����Ă��܂��A��ʂ�������x��荞��ł��������B", Me.btnTouroku)
            Return False
        End If


        '�H���X�������z��long�^�̐������z���Ă����ꍇ�����𒆒f�i�G���[����j
        If SetDouble(Me.tbxKoumutenSeikyuuGaku.Text.Trim) > SetDouble("2147483647") Then
            Return False
        End If

        If hidSeikyusaki.Value = "��" Or hidAutoJituFlg.Value = "False" Then
            '���ڐ����ڐ����������͍H���X���������ݒ薢���{�̏ꍇ�A�����ݒ�

            koumuten = IIf(Me.tbxKoumutenSeikyuuGaku.Text = String.Empty, 0, SetDouble(SetKingaku(Me.tbxKoumutenSeikyuuGaku.Text, False)))
            If GetMiseSeikyuu("C", Me.hidSeikyusaki.Value, _keiretuCd, _
                                                   Me.tbxSyouhinCd.Text) = "True" Then

                '�擾�������z�����������z�ɐݒ�
                tbxZeinuki.Text = SetKingaku(jitu, False)
                '���������z�����ݒ�t���O�n�m
                hidAutoJituFlg.Value = "True"
            End If
        End If

        '���z���Čv�Z����
        Call SetKingaku()
        Return True
    End Function

    ''' <summary>
    ''' ���������s���A����N������ݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDate(ByVal bln As Boolean)

        Dim simeDate As String

        '���������s���̐ݒ�(�����͂̏ꍇ)
        If tbxSeikyuDate.Text.Trim = String.Empty Then

            simeDate = KihonJyouhouInquiryBc.GetSimeDate(_kameiten_cd, SEIKYUUSIMEDATE)
            Dim datenow As Date
            datenow = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate)

            If simeDate = String.Empty Then
                simeDate = Format(DateSerial(datenow.Year.ToString, datenow.Month + 1, 0), "dd")
            End If

            '���������s�������߂�
            simeDate = GetHkkouYMD(simeDate)

            ''���������s����\������
            tbxSeikyuDate.Text = simeDate
        End If

        '����N���������͂̏ꍇ����N�����̐ݒ�
        If bln = True OrElse tbxUriDate.Text.Trim = String.Empty Then
            tbxUriDate.Text = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate).ToString(YMD)
        End If

    End Sub

    ''' <summary>
    ''' �̑��i�����c�[����
    ''' ���������s���A����N������ݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDate1(ByVal bln As Boolean)

        Dim simeDate As String

        '���������s���̐ݒ�(�����͂̏ꍇ)
        If tbxSeikyuDate1.Text.Trim = String.Empty Then

            simeDate = KihonJyouhouInquiryBc.GetSimeDate(_kameiten_cd, HANSOHUHINSEIKYUUSIMEDATE)

            Dim datenow As Date
            datenow = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate)

            If simeDate = String.Empty Then
                simeDate = Format(DateSerial(datenow.Year.ToString, datenow.Month + 1, 0), "dd")
            End If

            '���������s�������߂�
            simeDate = GetHkkouYMD(simeDate)

            ''���������s����\������
            tbxSeikyuDate1.Text = simeDate
        End If

        '����N���������͂̏ꍇ����N�����̐ݒ�
        If bln = True OrElse tbxUriDate1.Text.Trim = String.Empty Then
            tbxUriDate1.Text = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate).ToString(YMD)
        End If

    End Sub

    ''' <summary>
    '''���������s�������߂�
    ''' </summary>
    ''' <param name="dayString">������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHkkouYMD(ByVal dayString As String) As String
        Dim strEditYMD As String
        Dim strDD As String
        Dim sysDateYMD As Date

        sysDateYMD = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate)

        strDD = Right$("00" & dayString, 2)

        strEditYMD = sysDateYMD.ToString(YM) & "/" & strDD

        If IsDate(strEditYMD) = False Then

            strEditYMD = Format$( _
                             DateAdd(SIGNDAY, _
                                    -1, _
                                    DateAdd(SIGNMONTH, 1, CDate(Format$(sysDateYMD, YM) & "/01"))), YMD)
        End If

        '���߂����������s�����V�X�e�����t�̏ꍇ�A�����̒������Đݒ肷��
        If YMDCheck(CDate(strEditYMD), CDate(KihonJyouhouInquiryBc.GetSysDate)) = -1 Then

            strEditYMD = Format$(DateAdd(SIGNMONTH, 1, CDate(strEditYMD)), YM) & "/" & strDD

            If IsDate(strEditYMD) = False Then
                strEditYMD = Format$( _
                                   DateAdd(SIGNDAY, _
                                          -1, _
                                          DateAdd(SIGNMONTH _
                                                 , 2 _
                                                 , CDate(Format$(sysDateYMD, YM) & "/01"))), YMD)
            End If
        End If
        Return strEditYMD

    End Function




    Private koumuten As Long
    Private jitu As Long
    ''' <summary>
    ''' LOCK
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub LockItemTextbox(ByVal control As TextBox)
        control.Attributes.Add("readonly", "true")
        'control.Enabled = False
        control.Style.Add("background-color", "silver")
    End Sub

    ''' <summary>
    ''' UNLOCK
    ''' 
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub UnLockItemTextbox(ByVal control As TextBox)
        '   control.Enabled = True
        control.Attributes.Remove("readonly")
        control.Style.Item("background-color") = ("white")
    End Sub

    ''' <summary>
    ''' LOCKDRP
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub LockItemdrp(ByVal control As DropDownList)
        ' control.Enabled = False
        'control.Attributes.Add("disabled", "true")
        'control.Style.Add("background-color", "silver")




        'control.Attributes.Item("onfocus") = "this.defaultIndex=this.selectedIndex;"
        'control.Attributes.Item("onchange") = "this.selectedIndex=this.defaultIndex;"

        'control.CssClass = "readOnly"
        'control.Style.Item("background-color") = "#D0D0D0"
        CommonKassei.SetDropdownListReadonly(control)

    End Sub

    ''' <summary>
    ''' UNLOCKDRP
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub UnLockItemdrp(ByVal control As DropDownList)
        '   control.Enabled = True
        'control.Attributes.Remove("disabled")
        'control.Style.Item("background-color") = ("white")

        CommonKassei.SetDropdownListNotReadonly(control)

    End Sub

    ''' <summary>
    ''' ���z���Z�o����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku()

        Dim blnComErrFlg As Boolean

        '������
        blnComErrFlg = True

        '�����̓`�F�b�N
        If Me.tbxZeinuki.Text = String.Empty Then
            Me.lblSyouhizei.Text = String.Empty
            Me.lblZeikomi.Text = String.Empty
            Exit Sub
        End If

        Dim syouhizei As String
        syouhizei = (KihonJyouhouInquiryBc.GetKakaku(Me.hidZeikbnTxt.Value))
        If syouhizei = "0" Then
            '����łɂ��Z�b�g����
            Me.lblSyouhizei.Text = syouhizei

        Else
            '����ŋ敪����ŗ������߂�
            Me.lblSyouhizei.Text = SetKingaku(Fix(SetVal(SetDouble(Me.tbxZeinuki.Text.Replace(",", ""))) * SetVal(syouhizei * 100) / 100))
            '����ŋ敪����ŗ������߂�

        End If

        '�ō����z���Z�o����
        Me.lblZeikomi.Text = SetKingaku(SetVal(SetDouble(tbxZeinuki.Text.Replace(",", "") + SetVal(lblSyouhizei.Text))))


    End Sub

    ''' <summary>
    ''' ���z���Z�o����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku1()

        Dim blnComErrFlg As Boolean

        '������
        blnComErrFlg = True

        '�����̓`�F�b�N
        If Me.tbxZeinuki1.Text = String.Empty Then
            Me.lblSyouhizei1.Text = String.Empty
            Me.lblZeikomi1.Text = String.Empty
            Exit Sub
        End If

        Dim syouhizei As String
        syouhizei = (KihonJyouhouInquiryBc.GetKakaku(Me.hidZeikbnTxt1.Value))
        If syouhizei = "0" Then
            '����łɂ��Z�b�g����
            Me.lblSyouhizei1.Text = SetKingaku(syouhizei)

        Else
            '����ŋ敪����ŗ������߂�
            Me.lblSyouhizei1.Text = SetKingaku(Fix(SetVal(SetDouble(Me.tbxZeinuki1.Text.Replace(",", ""))) * SetVal(syouhizei * 100) / 100))
            '����ŋ敪����ŗ������߂�

        End If

        '�ō����z���Z�o����
        Me.lblZeikomi1.Text = SetKingaku(SetVal(SetDouble(tbxZeinuki1.Text.Replace(",", ""))) + SetVal(lblSyouhizei1.Text))


    End Sub

    ''' <summary>
    ''' �����������擾
    ''' </summary>
    ''' <param name="ptn"></param>
    ''' <param name="seikyuuSaki"></param>
    ''' <param name="keiretu"></param>
    ''' <param name="shouhin"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMiseSeikyuu(ByVal ptn As Object, ByVal seikyuuSaki As String, _
                                       ByVal keiretu As Object, ByVal shouhin As Object) As String

        Dim table As String           '�擾�Ώۃe�[�u����
        Dim key As String           '�擾�p�L�[
        Dim item As String           '�擾���ږ�
        GetMiseSeikyuu = "True"
        '������

        '������= ���ڐ����ڐ����̏ꍇ�A���݂ɓ������z��ݒ�ɃZ�b�g
        If seikyuuSaki = "��" Then
            Select Case ptn
                Case "A"
                    '���i�R�[�h�E�����L���ύX���͍H���X�����z�Ǝ������z�ɓ��z��ݒ�i���̂܂܁j
                Case "B"
                    '���������z�ύX���͍H���X�������z�Ɏ��������z��ݒ�
                    koumuten = jitu
                Case Else
                    '�H���X�������z�ύX���͎��������z�ɍH���X�������z��ݒ�
                    jitu = koumuten
            End Select

            Exit Function

        End If

        '/////���ȉ���������������/////

        '�n���ނɂ�蕪��
        Select Case keiretu
            '�A�C�t���z�[��
            Case "0001"
                table = "m_honbu_seikyuu"
                item = "honbumuke_kkk"
                key = "jhs_syouhin_cd"
                '�s�g�F�̉��
            Case "THTH"
                table = "m_th_seikyuuyou_kakaku"
                item = "th_muke_kkk"
                key = "syouhin_cd"
                '�����_�[�z�[��
            Case "NF03"
                table = "m_wh_seikyuuyou_kakaku"
                item = "honbumuke_kkk"
                key = "syouhin_cd"
                '��L�ȊO
            Case Else
                Select Case ptn
                    Case "A"
                        '�������E�R�n��ȊO�̐����L���E���i�R�[�h�ύX��
                        koumuten = 0
                    Case "B"
                        '�������E�R�n��ȊO�̎��������z�ύX��(�ݒ�Ȃ��j
                        GetMiseSeikyuu = "False"
                    Case Else
                        '�������E�R�n��ȊO�̍H���X�������z�ύX��
                        '�i�������E�R�n��͍H���X�������z���͕s�Ȃ̂Œʏ�L�蓾�Ȃ��j
                        koumuten = 0
                        GetMiseSeikyuu = "False"
                End Select
                Exit Function
        End Select

        '/////���ȉ��������łR�n�񎞂̏�����/////

        '�����L���E���i�R�[�h�ύX���A�H���X�������z=0�̏ꍇ�͎������ɂ�ݒ�
        If (ptn = "A") And koumuten = 0 Then
            jitu = 0
            Exit Function
        End If

        Dim value As String
        value = KihonJyouhouInquiryBc.GelKakaku(item, table, key, shouhin, ptn, jitu, koumuten)

        If value.ToString = String.Empty Then
            Me.btnTouroku.Enabled = False
            ShowMsg2(Messages.Instance.MSG2013E, Me.btnTouroku)
            GetMiseSeikyuu = "False"
        Else
            Select Case ptn
                Case "B"
                    '�������E�R�n��ȊO�̐����L���E���i�R�[�h�ύX��
                    koumuten = value
                Case "A"
                    '�������E�R�n��ȊO�̎��������z�ύX��(�ݒ�Ȃ��j
                    jitu = value
                Case Else
                    '�������E�R�n��ȊO�̍H���X�������z�ύX��
                    '�i�������E�R�n��͍H���X�������z���͕s�Ȃ̂Œʏ�L�蓾�Ȃ��j
                    jitu = value
            End Select
        End If
    End Function

    ''' <summary>
    ''' ���z�\��
    ''' </summary>
    ''' <param name="uservalue">value</param>
    ''' <param name="flg">flg</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetKingaku(ByVal uservalue As String, Optional ByVal flg As Boolean = True)

        Try
            Dim value As String
            value = uservalue
            value = value.Replace(",", "")

            value = Int(value)

            If flg = False Then
                If value.Length > 8 Then
                    value = value.Substring(0, 8)
                End If
            End If


            If value = String.Empty Then
                Return "0"
            End If

            Dim invalue As Integer
            invalue = Convert.ToInt32(value)

            Return invalue.ToString("###,###,##0")

        Catch ex As Exception
            Return uservalue
        End Try


    End Function

    ''' <summary>
    ''' MsgBox�\��
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="param1"></param>
    ''' <param name="param2"></param>
    ''' <param name="param3"></param>
    ''' <param name="param4"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg(ByVal msg As String, _
                                ByVal control As System.Web.UI.Control, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "")

        Dim csScript As New StringBuilder


        Dim pPage As Page = Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("ShowMsg")

        msg = msg.Replace("@PARAM1", param1) _
                          .Replace("@PARAM2", param2) _
                          .Replace("@PARAM3", param3) _
                          .Replace("@PARAM4", param4)

        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {control.ClientID, csScript.AppendFormat( _
                                                                "" & msg & "", _
                                                                param1, param2, param3, param4).ToString _
                                                                })
        End If


    End Sub

    ''' <summary>
    ''' MsgBox�\��
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="param1"></param>
    ''' <param name="param2"></param>
    ''' <param name="param3"></param>
    ''' <param name="param4"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg2(ByVal msg As String, _
                                ByVal control As System.Web.UI.Control, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "")

        Dim csScript As New StringBuilder


        Dim pPage As Page = Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("ShowMsg2")

        msg = msg.Replace("@PARAM1", param1) _
                          .Replace("@PARAM2", param2) _
                          .Replace("@PARAM3", param3) _
                          .Replace("@PARAM4", param4)

        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {control.ClientID, csScript.AppendFormat( _
                                                                "" & msg & "", _
                                                                param1, param2, param3, param4).ToString _
                                                                })
        End If


    End Sub

    ''' <summary>
    ''' ���ڌ����`�F�b�N����
    ''' </summary>
    ''' <param name="rvntData"></param>
    ''' <param name="rstrItemName"></param>
    ''' <param name="rlngMax"></param>
    ''' <param name="rlngType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function chkKetaSuu(ByVal control As System.Web.UI.Control, _
                                            ByVal rvntData As String, _
                                            ByVal rstrItemName As String, _
                                            ByVal rlngMax As Long, _
                                            ByVal rlngType As Long) As Boolean

        '�l��Check���s�Ȃ�
        If rvntData = String.Empty Then
            Return True
        End If

        '�������`�F�b�N
        If System.Text.Encoding.Default.GetBytes(rvntData).Length() > rlngMax Then
            Dim csScript As New StringBuilder

            'MsgBox �\��
            If rlngType = HANKAKU Then
                '���p�@{0}�ɓo�^�ł��镶�����́A���p{1}�����ȓ��ł��B
                msgAndFocus.AppendMsgAndCtrl(control, Messages.Instance.MSG2003E, rstrItemName, rlngMax)
            Else

                '�S�p�@{0}�ɓo�^�ł��镶�����́A�S�p{1}�����ȓ��ł��B
                msgAndFocus.AppendMsgAndCtrl(control, Messages.Instance.MSG2002E, rstrItemName, Int(rlngMax / 2).ToString)
            End If
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' �����`�F�b�N
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function chkDate(ByVal value As String) As String

        Dim strBuf As String = String.Empty        '���BUF

        chkDate = "false"

        '���̓`�F�b�N
        If value = String.Empty Then
            Return String.Empty
        End If

        'If value = "00000000" Then
        '    '���ʂ̏ꍇ�ł��B
        '    Return "00000000"
        'End If

        'If Len(Trim(value)) = "0" Then
        '    '���ʂ̏ꍇ�ł��B
        '    Return "0"

        'End If

        '�����������݂̂��m�F
        If IsNumeric(value) Then
            '�����J�E���g
            Select Case Len(value)
                Case 4   'MMDD
                    strBuf = Mid(Format(Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate), "yyyy"), 1, 4)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 1, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 3, 2)
                Case 6   'YYMMDD

                    If Mid(value, 1, 2) > "70" Then
                        strBuf = "19"
                    Else
                        strBuf = "20"
                    End If

                    strBuf = strBuf & Mid(value, 1, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 3, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 5, 2)

                Case 8   'YYYYMMDD

                    strBuf = strBuf & Mid(value, 1, 4)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 5, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 7, 2)

                Case Else
                    '�G���[(���͂��ꂽ���t�Ɍ�肪����B)

                    Exit Function
            End Select

            '���t���ɕϊ��ł���m�F
            If Not IsDate(strBuf) Then
                '�G���[(���͂��ꂽ���t�Ɍ�肪����B)
                Return "false"

            End If


            '"/"���܂ޓ���
        ElseIf IsDate(value) Then
            strBuf = Format(CDate(value), YMD)
        Else
            'Date�ϊ��s�\(���͂��ꂽ���t�Ɍ�肪����B)
            Return "false"
        End If



        Return strBuf

    End Function

    ''' <summary>
    ''' �N���A
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearArea()
        Me.lblUriageKeijou.Text = String.Empty
        Me.tbxAddDate.Text = String.Empty
        Me.tbxSyouhinCd.Text = String.Empty
        Me.tbxZeinuki.Text = String.Empty
        Me.lblSyouhizei.Text = String.Empty
        Me.lblZeikomi.Text = String.Empty
        Me.tbxKoumutenSeikyuuGaku.Text = String.Empty
        Me.tbxSeikyuDate.Text = String.Empty
        Me.tbxUriDate.Text = String.Empty
        Me.tbxBikou.Text = String.Empty
    End Sub
    ''' <summary>
    ''' �N���A�P
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearArea1()
        Me.lblUriageKeijou1.Text = String.Empty
        Me.tbxAddDate1.Text = String.Empty
        Me.tbxSyouhinCd1.Text = String.Empty
        Me.tbxZeinuki1.Text = String.Empty
        Me.lblSyouhizei1.Text = String.Empty
        Me.lblZeikomi1.Text = String.Empty
        Me.tbxSeikyuDate1.Text = String.Empty
        Me.tbxUriDate1.Text = String.Empty
        Me.tbxBikou1.Text = String.Empty
    End Sub
    ''' <summary>
    ''' double��ݒ�
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDouble(ByVal value As String) As Double
        If value.Trim = String.Empty Then
            Return 0
        End If
        Return Convert.ToDouble(value)
    End Function

    ''' <summary>
    ''' VB��val����
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetVal(ByVal value As String) As Double
        If value.Trim = String.Empty Then
            Return 0
        End If
        Return Convert.ToDouble(value.Replace(",", ""))
    End Function


    ''' <summary>
    ''' ��check
    '''
    ''' </summary>
    ''' <param name="d1">date1</param>
    ''' <param name="d2">date2</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function YMDCheck(ByVal d1 As Date, ByVal d2 As Date) As Integer

        If d1.ToString(YMD) > d2.ToString(YMD) Then
            Return 1
        ElseIf d1.ToString(YMD) = d2.ToString(YMD) Then
            Return 0
        Else
            Return -1
        End If

    End Function


#End Region


    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



End Class

