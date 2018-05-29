Imports Itis.Earth.BizLogic
Partial Public Class kyoutuu_jyouhou
    Inherits System.Web.UI.UserControl
    Public Enum kbn As Integer
        KihonJyouhou = 1 'LIS6
        YosinJyouhou = 2 'WANGY10
    End Enum
    Private intKbn As kbn
    Private strKameitenCd As String
    Private strLoginUserId As String
    Private _kenngenn As Boolean
    Private _HansokuSinaKenngenn As Boolean
    Public strCss As String
    Public strCss2 As String
    Private KihonJyouhouBL As New KakakuseikyuJyouhouLogic()
    Private earthAction As New EarthAction

    ''' <summary>�敪�������Z�b�g</summary>
    Public WriteOnly Property GetStyle() As kbn
        Set(ByVal value As kbn)
            intKbn = value

        End Set
    End Property
    Public Sub SetItemValue(ByVal strKameitenCd As String)

        Dim dtKyoutuuJyouhouTable As New Itis.Earth.DataAccess.KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
        Dim KyoutuuJyouhouLogic As New Itis.Earth.BizLogic.KyoutuuJyouhouLogic
        dtKyoutuuJyouhouTable = KyoutuuJyouhouLogic.GetKameitenInfo(strKameitenCd)
        Select Case intKbn
            Case kbn.KihonJyouhou

            Case kbn.YosinJyouhou
                If dtKyoutuuJyouhouTable.Rows.Count > 0 Then
                    With dtKyoutuuJyouhouTable.Rows(0)
                        lblKousinSya.Text = TrimNull(.Item("simei"))
                        If TrimNull(.Item("upd_datetime")) <> "" Then
                            lblKousinHi.Text = CDate(.Item("upd_datetime")).ToString("yyyy/MM/dd HH:mm:ss")
                        Else
                            lblKousinHi.Text = TrimNull(.Item("upd_datetime"))
                        End If
                        tbxKyoutuKubun.Text = TrimNull(.Item("kbn"))
                        lblKubunMei.Text = TrimNull(.Item("kbn_mei"))
                        tbxTorikesi.Text = TrimNull(.Item("torikesi"))
                        '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                        '�u����v���ڂ��Z�b�g����
                        Me.hidTorikesi.Value = TrimNull(.Item("torikesi"))
                        '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                        tbxKyoutuKameitenCd.Text = TrimNull(.Item("kameiten_cd"))
                        tbxKyoutuKameitenMei1.Text = TrimNull(.Item("kameiten_mei1"))
                        tbxKyoutukakeMei1.Text = TrimNull(.Item("tenmei_kana1"))
                        tbxKyoutuKameitenMei2.Text = TrimNull(.Item("kameiten_mei2"))
                        tbxKyoutukakeMei2.Text = TrimNull(.Item("tenmei_kana2"))
                        tbxBirudaNo.Text = TrimNull(.Item("builder_no"))
                        lblBirudaMei.Text = TrimNull(.Item("builder_mei"))
                        tbxKeiretuCd.Text = TrimNull(.Item("keiretu_cd"))
                        lblKeiretuMei.Text = TrimNull(.Item("keiretu_mei"))
                        tbxEigyousyoCd.Text = TrimNull(.Item("eigyousyo_cd"))
                        lblEigyousyoMei.Text = TrimNull(.Item("eigyousyo_mei"))
                        tbxThKasiCd.Text = TrimNull(.Item("th_kasi_cd"))
                        '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
                        '(FC)������Џ����Z�b�g����
                        Call Me.SetFcTyousaKaisya()
                        '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
                    End With
                Else
                    Context.Items("strFailureMsg") = Messages.Instance.MSG020E
                    Server.Transfer("CommonErr.aspx")
                End If
        End Select

    End Sub
    Public WriteOnly Property GetKameitenCd() As String
        Set(ByVal KameitenCd As String)
            strKameitenCd = KameitenCd
        End Set
    End Property
    Public WriteOnly Property GetLoginUserId() As String
        Set(ByVal LoginUserId As String)
            strLoginUserId = LoginUserId
        End Set
    End Property

    Public ReadOnly Property GetKunbunClientID() As String
        Get
            Return CType(Common_drop1.FindControl("ddlCommonDrop"), DropDownList).ClientID
        End Get
    End Property

    Public WriteOnly Property GetItemValue() As Itis.Earth.DataAccess.KameitenDataSet.m_kameitenTableDataTable
        Set(ByVal dtKyoutuuJyouhouTable As Itis.Earth.DataAccess.KameitenDataSet.m_kameitenTableDataTable)

            If dtKyoutuuJyouhouTable.Rows.Count > 0 Then
                With dtKyoutuuJyouhouTable.Rows(0)
                    lblKousinSya.Text = TrimNull(.Item("simei"))
                    If TrimNull(.Item("upd_datetime")) <> "" Then
                        lblKousinHi.Text = CDate(.Item("upd_datetime")).ToString("yyyy/MM/dd HH:mm:ss")
                    Else
                        lblKousinHi.Text = TrimNull(.Item("upd_datetime"))
                    End If
                    tbxKyoutuKubun.Text = TrimNull(.Item("kbn"))
                    lblKubunMei.Text = TrimNull(.Item("kbn_mei"))
                    tbxTorikesi.Text = TrimNull(.Item("torikesi"))
                    '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                    '�u����v���ڂ��Z�b�g����
                    Me.hidTorikesi.Value = TrimNull(.Item("torikesi"))
                    '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                    tbxKyoutuKameitenCd.Text = TrimNull(.Item("kameiten_cd"))
                    tbxKyoutuKameitenMei1.Text = TrimNull(.Item("kameiten_mei1"))
                    tbxKyoutukakeMei1.Text = TrimNull(.Item("tenmei_kana1"))
                    tbxKyoutuKameitenMei2.Text = TrimNull(.Item("kameiten_mei2"))
                    tbxKyoutukakeMei2.Text = TrimNull(.Item("tenmei_kana2"))
                    'If Not (tbxKyoutuKubun.Text = "J" Or tbxKyoutuKubun.Text = "T") Then
                    tbxBirudaNo.Text = TrimNull(.Item("builder_no"))
                    lblBirudaMei.Text = TrimNull(.Item("builder_mei"))
                    'End If

                    tbxKeiretuCd.Text = TrimNull(.Item("keiretu_cd"))
                    lblKeiretuMei.Text = TrimNull(.Item("keiretu_mei"))
                    tbxEigyousyoCd.Text = TrimNull(.Item("eigyousyo_cd"))
                    lblEigyousyoMei.Text = TrimNull(.Item("eigyousyo_mei"))
                    tbxThKasiCd.Text = TrimNull(.Item("th_kasi_cd"))
                    hidHaita.Value = lblKousinHi.Text
                    Common_drop2.SelectedValue = TrimNull(.Item("hattyuu_teisi_flg"))
                    '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
                    '(FC)������Џ����Z�b�g����
                    Call Me.SetFcTyousaKaisya()
                    '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
                End With

            End If
            btnHansokuSina.Attributes.Remove("onclick")
            If tbxKyoutuKubun.Text.ToUpper = "A" Then

                btnHansokuSina.Attributes.Add("onclick", "return funcMove('" & UrlConst.TENBETU_SYUUSEI & "','2','1','" & tbxKyoutuKameitenCd.Text & "');")

            Else
                btnHansokuSina.Attributes.Add("onclick", "return funcMove('" & UrlConst.TENBETU_SYUUSEI & "','2','0','" & tbxKyoutuKameitenCd.Text & "');")
            End If
        End Set
    End Property
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    Public Sub SetReadonly(ByVal blnReadonly As Boolean)
        If blnReadonly Then
            tbxTorikesi.Attributes.Add("readonly", "true")
            tbxKyoutuKubun.Attributes.Add("readonly", "true")
            tbxKyoutuKameitenCd.Attributes.Add("readonly", "true")
            tbxKyoutuKameitenMei1.Attributes.Add("readonly", "true")
            tbxKyoutuKameitenMei2.Attributes.Add("readonly", "true")
            tbxKyoutukakeMei1.Attributes.Add("readonly", "true")
            tbxKyoutukakeMei2.Attributes.Add("readonly", "true")
            tbxBirudaNo.Attributes.Add("readonly", "true")
            tbxKeiretuCd.Attributes.Add("readonly", "true")
            tbxEigyousyoCd.Attributes.Add("readonly", "true")
            tbxThKasiCd.Attributes.Add("readonly", "true")
            tbxKyoutuKubun.CssClass = "readOnly"
            tbxTorikesi.CssClass = "readOnly"
            tbxKyoutuKameitenCd.CssClass = "readOnly"
            tbxKyoutuKameitenMei1.CssClass = "readOnly"
            tbxKyoutuKameitenMei2.CssClass = "readOnly"
            tbxKyoutukakeMei1.CssClass = "readOnly"
            tbxKyoutukakeMei2.CssClass = "readOnly"
            tbxBirudaNo.CssClass = "readOnly"
            tbxKeiretuCd.CssClass = "readOnly"
            tbxEigyousyoCd.CssClass = "readOnly"
            tbxThKasiCd.CssClass = "readOnly"
            btnBirudaNo.Visible = False
            btnKeiretuCd.Visible = False
            btnEigyousyoCd.Visible = False
            btnTouroku.Visible = False
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            '(FC)������Џ����Z�b�g����
            Me.tbxFcTyousaKaisya.Visible = False
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
        Else
            tbxTorikesi.Attributes.Add("readonly", "false")
            tbxKyoutuKubun.Attributes.Add("readonly", "false")
            tbxKyoutuKameitenCd.Attributes.Add("readonly", "false")
            tbxKyoutuKameitenMei1.Attributes.Add("readonly", "false")
            tbxKyoutuKameitenMei2.Attributes.Add("readonly", "false")
            tbxKyoutukakeMei1.Attributes.Add("readonly", "false")
            tbxKyoutukakeMei2.Attributes.Add("readonly", "false")
            tbxThKasiCd.Attributes.Add("readonly", "false")
            tbxKyoutuKubun.CssClass = ""
            tbxTorikesi.CssClass = ""
            tbxKyoutuKameitenCd.CssClass = ""
            tbxKyoutuKameitenMei1.CssClass = ""
            tbxKyoutuKameitenMei2.CssClass = ""
            tbxKyoutukakeMei1.CssClass = ""
            tbxKyoutukakeMei2.CssClass = ""
            tbxThKasiCd.CssClass = ""
            btnBirudaNo.Enabled = True
            btnKeiretuCd.Enabled = True
            btnEigyousyoCd.Enabled = True
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            '(FC)������Џ����Z�b�g����
            Me.tbxFcTyousaKaisya.Visible = True
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
        End If
    End Sub

    Protected Sub btnTyuiJyouhouInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTyuiJyouhouInquiry.Click
        Server.Transfer("TyuiJyouhouInquiry.aspx")
    End Sub

    Protected Sub btnTyokusetuNyuuryoku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTyokusetuNyuuryoku.Click

        comdrp.Visible = True
        tbxKyoutuKubun.Visible = False
        lblKubunMei.Visible = False
        tbxKyoutuKameitenCd.ReadOnly = False
        tbxKyoutuKameitenCd.CssClass = ""
        tbxKyoutuKameitenCd.TabIndex = 0
        Common_drop1.SelectedValue = ""
        Common_drop1.Enabled = False
        Common_drop1.CssClass = "readOnly"
        tbxKameitenCd.Text = ""
        tbxKameitenCd.Enabled = False
        tbxKameitenCd.CssClass = "readOnly"
        btnTyokusetuNyuuryoku.Visible = False
        btnTyokusetuNyuuryokuTyuusi.Visible = True
    End Sub

    Private Sub btnTyokusetuNyuuryokuTyuusi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyokusetuNyuuryokuTyuusi.Click
        comdrp.SelectedValue = ""
        comdrp.Visible = False
        tbxKyoutuKubun.Visible = True
        lblKubunMei.Visible = True
        tbxKyoutuKameitenCd.Text = ""
        tbxKyoutuKameitenCd.ReadOnly = True
        tbxKyoutuKameitenCd.CssClass = "readOnly"
        tbxKyoutuKameitenCd.TabIndex = -1
        Common_drop1.Enabled = True
        Common_drop1.CssClass = ""
        tbxKameitenCd.Enabled = True
        tbxKameitenCd.CssClass = ""
        btnTyokusetuNyuuryoku.Visible = True
        btnTyokusetuNyuuryokuTyuusi.Visible = False
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Select Case intKbn
                Case kbn.KihonJyouhou
                    lblTitle.Text = "�����X��{���Ɖ�"
                    btnKihonJyouhouInquiry.Visible = False
                    '---------------------From 2013.03.07���F�C��------------
                    btnKakakuJyouhou.Visible = True
                    '---------------------To   2013.03.07���F�C��------------
                    tablekensaku.Visible = False
                    lblKousin1.Text = "�ŐV�X�V�ҁF"
                    lblKousin2.Text = "�ŐV�X�V�����F"
                    strCss = "hissu"
                    strCss2 = "hissu"
                    btnTouroku.Enabled = _kenngenn
                    btnHansokuSina.Visible = _HansokuSinaKenngenn
                    tbxKyoutukakeMei2.Attributes.Add("onblur", "fncTokomozi(this)")
                    tbxKyoutukakeMei1.Attributes.Add("onblur", "fncTokomozi(this)")
                    '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                    '�u����v���ڂ��Z�b�g����
                    Call Me.SetTorikesi("DDL")
                    '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                Case kbn.YosinJyouhou
                    lblTitle.Text = "�����X�^�M���Ɖ�"
                    btnYosinJyouhouDetails.Visible = False
                    btnHansokuSina.Visible = False
                    lblKousinSya.Visible = False
                    lblKousinHi.Visible = False
                    lblKousin1.Visible = False
                    lblKousin2.Visible = False
                    strCss = "koumokuMei"
                    strCss2 = ""
                    tablekensaku.Visible = False
                    tbxTorikesi.TabIndex = -1
                    tbxKyoutuKameitenMei1.TabIndex = -1
                    tbxKyoutukakeMei1.TabIndex = -1
                    tbxKyoutuKameitenMei2.TabIndex = -1
                    tbxKyoutukakeMei2.TabIndex = -1
                    tbxBirudaNo.TabIndex = -1
                    tbxKeiretuCd.TabIndex = -1
                    tbxEigyousyoCd.TabIndex = -1
                    tbxThKasiCd.TabIndex = -1
                    lblKousinSya.Text = "�u�ЊO��v"
                    lblKousinSya.ForeColor = Drawing.Color.Red
                    lblKousinSya.Visible = True
                    lblKousinSya.Font.Bold = True
                    lblKousinSya.Font.Size = 12
                    '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                    '�u����v���ڂ��Z�b�g����
                    Call Me.SetTorikesi("TEXT")
                    '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
            End Select
        End If
        Select Case intKbn
            Case kbn.KihonJyouhou
                strCss = "hissu"
                strCss2 = "hissu"
            Case kbn.YosinJyouhou
                strCss = "koumokuMei"
                strCss2 = ""
        End Select

        '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
        '�c�Ə���
        Me.lblEigyousyoMei.Attributes.Add("readonly", "true;")
        '(FC)������Џ����Z�b�g����
        Me.tbxFcTyousaKaisya.Attributes.Add("readonly", "true;")
        '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================

        '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
        '�F���Z�b�g����
        Call Me.SetColor()
        '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================

        'JavaScript���쐬����
        Call Me.MakeJavaScript()

        btnKakakuJyouhou.Attributes.Add("onclick", "fncGamenSenni('" & strKameitenCd & "');return false;")
        tbxKyoutuKubun.Attributes.Add("readonly", "true")
        btnEigyouJyouhouInquiry.Visible = False
        tbxBirudaNo.Attributes.Add("onblur", "fncToUpper(this);")
        tbxThKasiCd.Attributes.Add("onblur", "fncToUpper(this);")
    End Sub

    Public Function checkInput(ByRef strId As String) As String
        Dim csScript As New StringBuilder
        Dim strCaption As String = ""
        Dim commonCheck As New CommonCheck
        Dim commonSearch As New CommonSearchLogic
        Dim dtBirudaTable As New DataAccess.CommonSearchDataSet.BirudaTableDataTable
        Dim blnErr As Boolean = False
        checkInput = ""
        '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �폜��=========================
        '���
        'strCaption = commonCheck.CheckHissuNyuuryoku(tbxTorikesi.Text, "���")
        'If strCaption <> "" Then
        '    checkInput = checkInput & strCaption.ToString
        '    If strId = "" Then
        '        strId = tbxTorikesi.ClientID
        '    End If
        'Else
        '    strCaption = commonCheck.CheckByte(tbxTorikesi.Text, 1, "���")
        '    If strCaption <> "" Then
        '        checkInput = checkInput & strCaption.ToString
        '        If strId = "" Then
        '            strId = tbxTorikesi.ClientID
        '        End If
        '    Else
        '        strCaption = commonCheck.CheckHankaku(tbxTorikesi.Text, "���", "1")
        '        If strCaption <> "" Then
        '            checkInput = checkInput & strCaption.ToString
        '            If strId = "" Then
        '                strId = tbxTorikesi.ClientID
        '            End If
        '        End If
        '    End If
        'End If
        '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �폜��=========================


        '�����X���P
        strCaption = commonCheck.CheckHissuNyuuryoku(tbxKyoutuKameitenMei1.Text, "�����X���P")
        If strCaption <> "" Then
            checkInput = checkInput & strCaption.ToString
            If strId = "" Then
                strId = tbxKyoutuKameitenMei1.ClientID
            End If
        Else
            strCaption = commonCheck.CheckByte(tbxKyoutuKameitenMei1.Text, 40, "�����X���P", WebUI.kbn.ZENKAKU)
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxKyoutuKameitenMei1.ClientID
                End If
            Else
                strCaption = commonCheck.CheckKinsoku(tbxKyoutuKameitenMei1.Text, "�����X���P")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxKyoutuKameitenMei1.ClientID
                    End If
                End If
            End If
        End If


        '�X�J�i���P
        strCaption = commonCheck.CheckHissuNyuuryoku(tbxKyoutukakeMei1.Text, "�X�J�i���P")
        If strCaption <> "" Then
            checkInput = checkInput & strCaption.ToString
            If strId = "" Then
                strId = tbxKyoutukakeMei1.ClientID
            End If
        Else
            strCaption = commonCheck.CheckByte(tbxKyoutukakeMei1.Text, 20, "�X�J�i���P")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxKyoutukakeMei1.ClientID
                End If
            Else
                strCaption = commonCheck.CheckKatakana(tbxKyoutukakeMei1.Text, "�X�J�i���P")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxKyoutukakeMei1.ClientID
                    End If
                Else
                    strCaption = commonCheck.CheckKinsoku(tbxKyoutukakeMei1.Text, "�X�J�i���P")
                    If strCaption <> "" Then
                        checkInput = checkInput & strCaption.ToString
                        If strId = "" Then
                            strId = tbxKyoutukakeMei1.ClientID
                        End If
                    End If
                End If
            End If
        End If



        '�����X���Q
        If tbxKyoutuKameitenMei2.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxKyoutuKameitenMei2.Text, 40, "�����X���Q", WebUI.kbn.ZENKAKU)
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxKyoutuKameitenMei2.ClientID
                End If
            Else
                strCaption = commonCheck.CheckKinsoku(tbxKyoutuKameitenMei2.Text, "�����X���Q")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxKyoutuKameitenMei2.ClientID
                    End If
                End If
            End If

        End If

        '�X�J�i���Q
        If tbxKyoutukakeMei2.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxKyoutukakeMei2.Text, 20, "�X�J�i���Q")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxKyoutukakeMei2.ClientID
                End If
            Else
                strCaption = commonCheck.CheckKatakana(tbxKyoutukakeMei2.Text, "�X�J�i���Q")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxKyoutukakeMei2.ClientID
                    End If
                Else
                    strCaption = commonCheck.CheckKinsoku(tbxKyoutukakeMei2.Text, "�X�J�i���Q")
                    If strCaption <> "" Then
                        checkInput = checkInput & strCaption.ToString
                        If strId = "" Then
                            strId = tbxKyoutukakeMei2.ClientID
                        End If
                    End If
                End If
            End If

        End If
        '�r���_ށ|NO
        If tbxBirudaNo.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxBirudaNo.Text, 9, "�r���_�[NO")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxBirudaNo.ClientID
                End If
                blnErr = True
            Else
                strCaption = commonCheck.CheckKinsoku(tbxBirudaNo.Text, "�r���_�[NO")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxBirudaNo.ClientID
                    End If
                    blnErr = True
                End If
            End If
        End If

        '�n��R�[�h
        blnErr = False
        If tbxKeiretuCd.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxKeiretuCd.Text, 5, "�n����")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxKeiretuCd.ClientID

                End If
                blnErr = True
            Else
                strCaption = commonCheck.CheckKinsoku(tbxKeiretuCd.Text, "�n����")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxKeiretuCd.ClientID

                    End If
                    blnErr = True
                End If
            End If

            If Not blnErr And tbxKeiretuCd.Text <> "" Then
                dtBirudaTable = commonSearch.GetCommonInfo(tbxKeiretuCd.Text, "m_keiretu", tbxKyoutuKubun.Text)
                If dtBirudaTable.Rows.Count = 0 Then
                    checkInput = checkInput & String.Format(Messages.Instance.MSG2008E, "�n����").ToString
                    If strId = "" Then
                        strId = tbxKeiretuCd.ClientID
                    End If

                Else
                    tbxKeiretuCd.Text = dtBirudaTable.Rows(0).Item(0)
                    lblKeiretuMei.Text = dtBirudaTable.Rows(0).Item(1)
                End If

            End If
        End If
        '�c�Ə��R�[�h
        blnErr = False
        If tbxEigyousyoCd.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxEigyousyoCd.Text, 5, "�c�Ə�����")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxEigyousyoCd.ClientID
                End If
                blnErr = True
            Else
                strCaption = commonCheck.CheckKinsoku(tbxEigyousyoCd.Text, "�c�Ə�����")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxEigyousyoCd.ClientID
                    End If
                    blnErr = True
                End If
            End If

            If Not blnErr And tbxEigyousyoCd.Text <> "" Then
                dtBirudaTable = commonSearch.GetCommonInfo(tbxEigyousyoCd.Text, "m_eigyousyo")
                If dtBirudaTable.Rows.Count = 0 Then
                    checkInput = checkInput & String.Format(Messages.Instance.MSG2008E, "�c�Ə�����").ToString
                    If strId = "" Then
                        strId = tbxEigyousyoCd.ClientID
                    End If

                Else
                    tbxEigyousyoCd.Text = dtBirudaTable.Rows(0).Item(0)
                    lblEigyousyoMei.Text = dtBirudaTable.Rows(0).Item(1)
                End If

            End If
        End If
        '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
        '(FC)������Џ����Z�b�g����
        Call Me.SetFcTyousaKaisya()
        '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================

        'TH���r����
        If tbxThKasiCd.Text <> "" Then
            strCaption = commonCheck.CheckByte(tbxThKasiCd.Text, 7, "TH���r����")
            If strCaption <> "" Then
                checkInput = checkInput & strCaption.ToString
                If strId = "" Then
                    strId = tbxThKasiCd.ClientID
                End If
            Else
                strCaption = commonCheck.CheckKinsoku(tbxThKasiCd.Text, "TH���r����")
                If strCaption <> "" Then
                    checkInput = checkInput & strCaption.ToString
                    If strId = "" Then
                        strId = tbxThKasiCd.ClientID
                    End If
                End If
            End If

        End If
    End Function
    Public Sub ShowMsg(ByVal msg As String, _
                                ByVal strClientID As String)

        Dim csScript As New StringBuilder


        Dim pPage As Page = Me.Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("ShowMsg2")


        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {strClientID, msg})
        End If


    End Sub
    Public Function haitaCheck(ByVal strKameiten As String) As String
        Dim cHaitaCheck As New HaitaCheck
        If hidHaita.Value <> "" Then

            Return cHaitaCheck.CheckHaita(strKameiten, "m_kameiten", CDate(hidHaita.Value).ToString("yyyy/MM/dd HH:mm:ss"))

        End If
        Return ""
    End Function
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Dim comchk As New CommonCheck
        Dim dtKyoutuuJyouhouData As New DataAccess.KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
        Dim strObjId As String = ""
        tbxKyoutukakeMei1.Text = comchk.SetTokomozi(tbxKyoutukakeMei1.Text)
        tbxKyoutukakeMei2.Text = comchk.SetTokomozi(tbxKyoutukakeMei2.Text)
        Dim strErr As String = checkInput(strObjId)
        If strErr <> "" Then
            ShowMsg(strErr, strObjId)

        Else
            strErr = haitaCheck(strKameitenCd)
            If strErr <> "" Then
                ShowMsg(strErr, btnTouroku.ClientID)
                setKousinSya(strErr)

            Else

                Dim KyoutuuJyouhouLogic As New KyoutuuJyouhouLogic
                dtKyoutuuJyouhouData.Rows.Add(dtKyoutuuJyouhouData.NewKyoutuuJyouhouTableRow)
                With dtKyoutuuJyouhouData.Rows(0)
                    .Item("kameiten_mei1") = tbxKyoutuKameitenMei1.Text
                    .Item("tenmei_kana1") = tbxKyoutukakeMei1.Text
                    .Item("kameiten_mei2") = tbxKyoutuKameitenMei2.Text
                    .Item("tenmei_kana2") = tbxKyoutukakeMei2.Text
                    .Item("builder_no") = tbxBirudaNo.Text
                    .Item("keiretu_cd") = tbxKeiretuCd.Text
                    .Item("eigyousyo_cd") = tbxEigyousyoCd.Text
                    .Item("th_kasi_cd") = tbxThKasiCd.Text
                    .Item("kameiten_cd") = strKameitenCd
                    .Item("simei") = strLoginUserId
                    '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �C����=========================
                    '.Item("torikesi") = tbxTorikesi.Text
                    .Item("torikesi") = Me.ddlTorikesi.SelectedValue.Trim
                    '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �C����=========================
                    .Item("hattyuu_teisi_flg") = Common_drop2.SelectedValue

                End With
                Dim commonSearch As New CommonSearchLogic

                KyoutuuJyouhouLogic.SetUpdKyoutuuJyouhouInfo(dtKyoutuuJyouhouData)
                Dim dtBirudaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.BirudaTableDataTable
                dtBirudaTable = commonSearch.GetCommonInfo(tbxBirudaNo.Text, "Biruda")
                If dtBirudaTable.Rows.Count = 1 Then
                    lblBirudaMei.Text = TrimNull(dtBirudaTable.Rows(0).Item(1))
                Else
                    lblBirudaMei.Text = ""
                End If

                dtBirudaTable = commonSearch.GetCommonInfo(tbxKeiretuCd.Text, "m_keiretu", tbxKyoutuKubun.Text)

                If dtBirudaTable.Rows.Count = 1 Then
                    lblKeiretuMei.Text = TrimNull(dtBirudaTable.Rows(0).Item(1))
                Else
                    lblKeiretuMei.Text = ""
                End If

                dtBirudaTable = commonSearch.GetCommonInfo(tbxEigyousyoCd.Text, "m_eigyousyo")
                If dtBirudaTable.Rows.Count = 1 Then
                    lblEigyousyoMei.Text = TrimNull(dtBirudaTable.Rows(0).Item(1))
                Else
                    lblEigyousyoMei.Text = ""
                End If
                '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
                '(FC)������Џ����Z�b�g����
                Call Me.SetFcTyousaKaisya()
                '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
                setKousinHi(strKameitenCd)
                CType(Me.Parent.FindControl("hidKeiretuCd"), HiddenField).Value = tbxKeiretuCd.Text
                CType(Me.Parent.FindControl("updHiddenpanel"), UpdatePanel).Update()
                ShowMsg(Replace(Messages.Instance.MSG018S, "@PARAM1", "���ʏ��"), btnTouroku.ClientID)

            End If

        End If

    End Sub
    Public Sub setKousinHi(ByVal strKameiten As String)
        Dim dtKyoutuuJyouhouTable As Itis.Earth.DataAccess.KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
        Dim KyoutuuJyouhouLogic As New KyoutuuJyouhouLogic


        dtKyoutuuJyouhouTable = KyoutuuJyouhouLogic.GetKameitenInfo(strKameitenCd)
        If dtKyoutuuJyouhouTable.Rows.Count > 0 Then
            With dtKyoutuuJyouhouTable.Rows(0)
                lblKousinSya.Text = TrimNull(.Item("simei"))
                If TrimNull(.Item("upd_datetime")) <> "" Then
                    lblKousinHi.Text = CDate(.Item("upd_datetime")).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    lblKousinHi.Text = TrimNull(.Item("upd_datetime"))
                End If
            End With
        End If
        hidHaita.Value = lblKousinHi.Text
        UpdatePanel6.Update()
        UpdatePanel7.Update()
    End Sub
    Public Sub setKousinSya(ByVal strErr As String)
        Dim dtKyoutuuJyouhouTable As Itis.Earth.DataAccess.KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable
        Dim KyoutuuJyouhouLogic As New KyoutuuJyouhouLogic
        If strErr <> Messages.Instance.MSG2009E Then
            dtKyoutuuJyouhouTable = KyoutuuJyouhouLogic.GetKameitenInfo(strKameitenCd)
            If dtKyoutuuJyouhouTable.Rows.Count > 0 Then
                With dtKyoutuuJyouhouTable.Rows(0)
                    lblKousinSya.Text = TrimNull(.Item("simei"))
                    If TrimNull(.Item("upd_datetime")) <> "" Then
                        lblKousinHi.Text = CDate(.Item("upd_datetime")).ToString("yyyy/MM/dd HH:mm:ss")
                    Else
                        lblKousinHi.Text = TrimNull(.Item("upd_datetime"))
                    End If
                End With
            End If
        End If

        UpdatePanel6.Update()
        UpdatePanel7.Update()

    End Sub
    Protected Sub btnKihonJyouhouInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKihonJyouhouInquiry.Click
        Server.Transfer("KihonJyouhouInquiry.aspx?strKameitenCd=" & strKameitenCd)
    End Sub

    Protected Sub btnEigyouJyouhouInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEigyouJyouhouInquiry.Click
        Server.Transfer("EigyouJyouhouInquiry.aspx?strKameitenCd=" & strKameitenCd)
    End Sub

    Protected Sub btnBukkenJyouhouInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBukkenJyouhouInquiry.Click
        Server.Transfer("BukkenJyouhouInquiry.aspx?strKameitenCd=" & strKameitenCd)
    End Sub

    Protected Sub btnYosinJyouhouDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYosinJyouhouDetails.Click
        Server.Transfer("YosinJyouhouInquiry.aspx?strKameitenCd=" & strKameitenCd)
    End Sub
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get
        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set
    End Property
    Public Property HansokuSinaKenngenn() As Boolean
        Get
            Return _HansokuSinaKenngenn
        End Get
        Set(ByVal value As Boolean)
            _HansokuSinaKenngenn = value
        End Set
    End Property

    Private Sub btnHansokuSina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHansokuSina.Click
        Response.Redirect(UrlConst.TENBETU_SYUUSEI & "?tenmd=2&isfc=1&kameicd=" & tbxKyoutuKameitenCd.Text)

    End Sub

    Private Sub tbxBirudaNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxBirudaNo.TextChanged
        Dim commonSearch As New CommonSearchLogic
        Dim dtBirudaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.BirudaTableDataTable
        dtBirudaTable = commonSearch.GetCommonInfo(tbxBirudaNo.Text, "Biruda")
        If dtBirudaTable.Rows.Count = 1 Then
            lblBirudaMei.Text = TrimNull(dtBirudaTable.Rows(0).Item(1))
        Else
            lblBirudaMei.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' ���ځu����v���Z�b�g����
    ''' </summary>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Private Sub SetTorikesi(ByVal strKbn As String)

        Select Case strKbn
            Case "DDL"
                'ddl
                Me.ddlTorikesi.Visible = True
                'text
                Me.tbxTorikesi.Visible = False
                '�u����vddl���Z�b�g����
                Call Me.SetTorikesiDDL()

                Me.ddlTorikesi.Attributes.Add("onChange", "fncSetTorikesi();")

            Case Else
                'ddl
                Me.ddlTorikesi.Visible = False
                'text
                Me.tbxTorikesi.Visible = True
                '�u����vtext���Z�b�g����
                Call Me.SetTorikesiTEXT()

        End Select

    End Sub

    ''' <summary>
    ''' �u����vddl���Z�b�g����
    ''' </summary>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Private Sub SetTorikesiDDL()

        '�f�[�^���擾����
        Dim kyoutuuJyouhouBC As New Itis.Earth.BizLogic.KyoutuuJyouhouLogic
        Dim dtTorikesi As New Data.DataTable
        dtTorikesi = kyoutuuJyouhouBC.GetTorikesiList()

        With Me.ddlTorikesi
            'ddl��Bound����
            .DataValueField = "code"
            .DataTextField = "meisyou"
            .DataSource = dtTorikesi
            .DataBind()

            '�擪�s
            .Items.Insert(0, New ListItem(String.Empty, "0"))

            If Not .Items.FindByValue(Me.hidTorikesi.Value.Trim) Is Nothing Then
                .SelectedValue = Me.hidTorikesi.Value.Trim
            Else
                .SelectedIndex = 0
            End If

        End With

    End Sub

    ''' <summary>
    ''' �u����vtext���Z�b�g����
    ''' </summary>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Private Sub SetTorikesiTEXT()

        If (Me.hidTorikesi.Value.Trim.Equals("0")) OrElse (Me.hidTorikesi.Value.Trim.Equals(String.Empty)) Then
            '=============2012/04/20 �ԗ� 405721�̗v�]�Ή� �C����==================
            'Me.tbxTorikesi.Text = Me.hidTorikesi.Value.Trim
            Me.tbxTorikesi.Text = String.Empty
            '=============2012/04/20 �ԗ� 405721�̗v�]�Ή� �C����==================
        Else
            '�f�[�^���擾����
            Dim kyoutuuJyouhouBC As New Itis.Earth.BizLogic.KyoutuuJyouhouLogic
            Dim dtTorikesi As New Data.DataTable
            dtTorikesi = kyoutuuJyouhouBC.GetTorikesiList(Me.hidTorikesi.Value.Trim)

            If dtTorikesi.Rows.Count > 0 Then
                Me.tbxTorikesi.Text = dtTorikesi.Rows(0).Item("meisyou").ToString.Trim
            Else
                Me.tbxTorikesi.Text = Me.hidTorikesi.Value.Trim & ":"
            End If
        End If

    End Sub

    ''' <summary>
    ''' �F��ύX����
    ''' </summary>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Private Sub SetColor()

        Dim strTorikesi As String
        If Me.ddlTorikesi.Visible Then
            strTorikesi = Me.ddlTorikesi.SelectedValue.Trim
        Else
            strTorikesi = Me.tbxTorikesi.Text.Trim
        End If

        If strTorikesi.Equals("0") OrElse strTorikesi.Equals(String.Empty) Then
            Me.tbxKyoutuKubun.ForeColor = Drawing.Color.Black
            Me.lblKubunMei.ForeColor = Drawing.Color.Black
            Me.ddlTorikesi.ForeColor = Drawing.Color.Black
            Me.tbxTorikesi.ForeColor = Drawing.Color.Black
            Me.Common_drop2.TextColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenCd.ForeColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenMei1.ForeColor = Drawing.Color.Black
            Me.tbxKyoutukakeMei1.ForeColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenMei2.ForeColor = Drawing.Color.Black
            Me.tbxKyoutukakeMei2.ForeColor = Drawing.Color.Black
            Me.tbxBirudaNo.ForeColor = Drawing.Color.Black
            Me.lblBirudaMei.ForeColor = Drawing.Color.Black
            Me.tbxKeiretuCd.ForeColor = Drawing.Color.Black
            Me.lblKeiretuMei.ForeColor = Drawing.Color.Black
            Me.tbxEigyousyoCd.ForeColor = Drawing.Color.Black
            Me.lblEigyousyoMei.ForeColor = Drawing.Color.Black
            Me.tbxThKasiCd.ForeColor = Drawing.Color.Black
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            '(FC)������Џ����Z�b�g����
            Me.tbxFcTyousaKaisya.ForeColor = Drawing.Color.Black
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
        Else
            Me.tbxKyoutuKubun.ForeColor = Drawing.Color.Red
            Me.lblKubunMei.ForeColor = Drawing.Color.Red
            Me.ddlTorikesi.ForeColor = Drawing.Color.Red
            Me.tbxTorikesi.ForeColor = Drawing.Color.Red
            Me.Common_drop2.TextColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenCd.ForeColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenMei1.ForeColor = Drawing.Color.Red
            Me.tbxKyoutukakeMei1.ForeColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenMei2.ForeColor = Drawing.Color.Red
            Me.tbxKyoutukakeMei2.ForeColor = Drawing.Color.Red
            Me.tbxBirudaNo.ForeColor = Drawing.Color.Red
            Me.lblBirudaMei.ForeColor = Drawing.Color.Red
            Me.tbxKeiretuCd.ForeColor = Drawing.Color.Red
            Me.lblKeiretuMei.ForeColor = Drawing.Color.Red
            Me.tbxEigyousyoCd.ForeColor = Drawing.Color.Red
            Me.lblEigyousyoMei.ForeColor = Drawing.Color.Red
            Me.tbxThKasiCd.ForeColor = Drawing.Color.Red
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            '(FC)������Џ����Z�b�g����
            Me.tbxFcTyousaKaisya.ForeColor = Drawing.Color.Red
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
        End If

    End Sub

    ''' <summary>
    ''' JavaScript���쐬����
    ''' </summary>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Private Sub MakeJavaScript()

        Dim csName As String = "setScript_kyoutuujyouhou"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '�����ύX���鎞
            .AppendLine("function fncSetTorikesi() ")
            .AppendLine("{ ")
            .AppendLine("	var strTorikesi = document.getElementById('" & Me.ddlTorikesi.ClientID & "').value; ")
            .AppendLine("	if(strTorikesi =='0') ")
            .AppendLine("	{ ")
            .AppendLine("		fncSetColor(''); ")
            .AppendLine("	} ")
            .AppendLine("	else ")
            .AppendLine("	{ ")
            .AppendLine("		fncSetColor('red'); ")
            .AppendLine("	} ")
            .AppendLine("} ")
            '�F���Z�b�g����
            .AppendLine("function fncSetColor(strColor) ")
            .AppendLine("{ ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKubun.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.lblKubunMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	if( document.getElementById('" & Me.ddlTorikesi.ClientID & "') != null ) ")
            .AppendLine("	{ ")
            .AppendLine("	    document.getElementById('" & Me.ddlTorikesi.ClientID & "').style.color = strColor; ")
            .AppendLine("	} ")
            .AppendLine("	if( document.getElementById('" & Me.tbxTorikesi.ClientID & "') != null ) ")
            .AppendLine("	{ ")
            .AppendLine("	    document.getElementById('" & Me.tbxTorikesi.ClientID & "').style.color = strColor; ")
            .AppendLine("	} ")
            .AppendLine("	document.getElementById('" & Me.Common_drop2.DdlClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenMei1.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutukakeMei1.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenMei2.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutukakeMei2.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxBirudaNo.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.lblBirudaMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKeiretuCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.lblKeiretuMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxEigyousyoCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.lblEigyousyoMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxThKasiCd.ClientID & "').style.color = strColor; ")
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            '(FC)������Џ����Z�b�g����
            .AppendLine("	document.getElementById('" & Me.tbxFcTyousaKaisya.ClientID & "').style.color = strColor; ")
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            .AppendLine("} ")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType(), csName, csScript.ToString())

    End Sub

    ''' <summary>
    ''' (FC)������Џ����Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ�</history>
    Private Sub SetFcTyousaKaisya()

        '�c�Ə��R�[�h
        Dim strEigyousyoCd As String
        strEigyousyoCd = Me.tbxEigyousyoCd.Text.Trim()

        If Not strEigyousyoCd.Equals(String.Empty) Then
            '�c�Ə��R�[�h�����

            'FC������Џ����擾����
            Dim kyoutuuJyouhouBC As New Itis.Earth.BizLogic.KyoutuuJyouhouLogic
            Dim dtFcTyousaKaisya As New Data.DataTable
            dtFcTyousaKaisya = kyoutuuJyouhouBC.GetFcTyousaKaisya(strEigyousyoCd)

            If dtFcTyousaKaisya.Rows.Count > 0 Then
                'FC������Џ��
                Me.tbxFcTyousaKaisya.Text = dtFcTyousaKaisya.Rows(0).Item("fc_tys_kaisya").ToString.Trim
            Else
                'FC������Џ��
                Me.tbxFcTyousaKaisya.Text = String.Empty
            End If

        Else
            '�c�Ə��R�[�h����

            'FC������Џ��
            Me.tbxFcTyousaKaisya.Text = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' (FC)������Џ����Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ�</history>
    Private Sub btnFcTyousaKaisya_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFcTyousaKaisya.Click

        Call Me.SetFcTyousaKaisya()

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
        dtTyousaJyouhou = KihonJyouhouBL.GetKameiten(strKameitenCd)
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

        Dim strTys_seikyuu_saki_cd As String         '����������R�[�h koj_seikyuu_saki_cd
        Dim strTys_seikyuu_saki_brc As String        '����������}�� koj_seikyuu_saki_brc 
        Dim strTys_seikyuu_saki_kbn As String        '����������敪 koj_seikyuu_saki_kbn

        '�����X�}�X�^�Ńf�[�^���擾����
        Dim dtTyousaJyouhou As Data.DataTable
        dtTyousaJyouhou = KihonJyouhouBL.GetKameiten(strKameitenCd)
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
        strScript = "objSrchWin = window.open('TokubetuTaiouMasterSearchList.aspx?sendSearchTerms=1$$$'+'" & strKameitenCd & "');"
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

        Dim FilePath As String = earthAction.TorihukiJyoukenKakuninhyou(strKameitenCd, Me.tbxKyoutuKubun.Text)

        Me.hidFile.Value = FilePath
        '�t�@�C���p�X�̑��݂��Ƃ𔻒f����
        If IO.File.Exists(FilePath.Replace("file:", String.Empty).Replace("/", "\")) Then
            Call GetFile()
        Else
            ShowMsg(Messages.Instance.MSG2076E, btnTorihukiJyoukenKakuninhyou.ClientID)
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

        Dim FilePath As String = earthAction.TyousaCard(strKameitenCd, Me.tbxKyoutuKubun.Text)

        Me.hidFile.Value = FilePath
        '�t�@�C���p�X�̑��݂��Ƃ𔻒f����
        If IO.File.Exists(FilePath.Replace("file:", String.Empty).Replace("/", "\")) Then
            Call GetFile()
        Else
            ShowMsg(Messages.Instance.MSG2077E, btnTyousaCard.ClientID)
        End If

    End Sub

    ''' <summary>
    ''' �t�@�C���p�X���J����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.26</history>
    Private Sub GetFile()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScriptFile"
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
        Page.ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub
End Class