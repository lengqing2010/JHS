Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection

Partial Public Class EigyouJyouhouInquiry
    Inherits System.Web.UI.Page

    ''' <summary>�����X�c�Ə�����������</summary>
    ''' <remarks>�����X�c�Ə�񌟍��@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2009/07/16�@����N(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Dim user_info As New LoginUserInfo


    '�c�Ə�񌟍�BL
    Private EigyouJyouhouInquiryBL As New EigyouJyouhouInquiryLogic
    Private commonCheck As New CommonCheck
    Private KihonJyouhouBL As New KakakuseikyuJyouhouLogic()
    Protected scrollHeight As Integer = 0
    Private earthAction As New EarthAction

    ''' <summary> �y�[�W���b�h</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '���O�C�����[�U�[���擾����B
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X

        '���O�C�����[�U�[ID���擾����B
        ViewState("userId") = ninsyou.GetUserID()
        ' ���[�U�[��{�F��
        jBn.userAuth(user_info)

        If ViewState("userId") = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E '�i"�Y�����[�U�[������܂���B"�j
            Server.Transfer("CommonErr.aspx")
        End If

        'If user_info Is Nothing Then
        '    Context.Items("strFailureMsg") = Messages.Instance.MSG2020E '�i"����������܂���B"�j
        '    Server.Transfer("CommonErr.aspx")
        'End If

        'javascript �쐬
        MakeJavascript()

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, ViewState("userId"))
            ' �敪�R���{�Ƀf�[�^���o�C���h����
            'Me.divMeisai.Attributes.Add("style", "height: 177px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:958px;")

            '���O�C�����[�U�[�̉c�ƃ}���敪���擾����B
            getEigyouManKbn()

            busyo_mei1.Visible = False
            busyo_mei2.Visible = False
            DisplayName1.Visible = False
            DisplayName2.Visible = False
            hansokuhin_cd1.Visible = False
            hansokuhin_cd2.Visible = False
            kameiten_cd1.Visible = False
            kameiten_cd2.Visible = False
            '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            torikesi1.Visible = False
            torikesi2.Visible = False
            '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            kameiten_mei1.Visible = False
            kameiten_mei2.Visible = False
            kouji_cd1.Visible = False
            kouji_cd2.Visible = False
            todouhuken_mei1.Visible = False
            todouhuken_mei2.Visible = False
            tyousa_cd1.Visible = False
            tyousa_cd2.Visible = False
            '�g�D���x����ݒ肷��
            setSosikiLabel()
        Else
            '--------From 2013.03.12���F�C��--------
            CloseCover()
            '--------To   2013.03.12���F�C��--------
        End If

        Me.chkKubunAll.Attributes.Add("onClick", "fncSetKubunVal();")
        Me.btnKensaku.Attributes.Add("onClick", "if(fncNyuuryokuCheck()==true){fncShowModal();}else{return false}")

        ''�n��R�[�h����������B
        'Me.btnKeiretuSearch.Attributes.Add("onclick", "fncKeiretuSearch();return false;")
        ''�c�ƒS��ID����������B
        'Me.btnTantouKensaku.Attributes.Add("onclick", "fncUserSearch();return false;")
        Me.btnClearWin.Attributes.Add("onclick", "fncClearWin();")
        Me.tbxTantouEigyouSyaMei.Attributes.Add("readonly", "true")
        Me.tbxKeiretuMei.Attributes.Add("readonly", "true")

        Me.tbxKameitenCd.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxKameitenKana.Attributes.Add("onBlur", "fncTokomozi(this);")

    End Sub

    ''' <summary>�g�D���x����ݒ肷��</summary>
    Private Sub setSosikiLabel()

        Dim dtSosikiLevel As EigyouJyouhouDataSet.sosikiLabelDataTable
        If ViewState("busyo_cd") = "0000" Or ViewState("t_sansyou_busyo_cd") = "0000" Then
            If ViewState("eigyouManKbn") <> "1" Then
                dtSosikiLevel = EigyouJyouhouInquiryBL.GetSosikiLabelInfo()
            Else
                dtSosikiLevel = EigyouJyouhouInquiryBL.GetSosikiLabelInfo2(ViewState("sosikiLevel"), ViewState("sosikiLevel2"), ViewState("busyo_cd"), ViewState("t_sansyou_busyo_cd"), ViewState("eigyouManKbn"))

            End If


        Else
            dtSosikiLevel = EigyouJyouhouInquiryBL.GetSosikiLabelInfo2(ViewState("sosikiLevel"), ViewState("sosikiLevel2"), ViewState("busyo_cd"), ViewState("t_sansyou_busyo_cd"), ViewState("eigyouManKbn"))

        End If
        Me.ddlSosikiLevel.Items.Clear()
        Me.ddlSosikiLevel.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        For i As Integer = 0 To dtSosikiLevel.Rows.Count - 1
            Dim ddlist As New ListItem
            ddlist = New ListItem
            ddlist.Text = dtSosikiLevel.Rows(i).Item(0).ToString & "�F" & dtSosikiLevel.Rows(i).Item(1).ToString
            ddlist.Value = dtSosikiLevel.Rows(i).Item(0).ToString
            ddlSosikiLevel.Items.Add(ddlist)
        Next

    End Sub

    ''' <summary>�g�D���x���͕ύX���̏���</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ddlSosikiLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSosikiLevel.SelectedIndexChanged




        If Me.ddlSosikiLevel.SelectedIndex = 0 Then
            Me.ddlBusyoCd.Items.Clear()
        Else
            setBusyo(Me.ddlSosikiLevel.SelectedValue.ToString)
        End If
    End Sub

    ''' <summary>�����R�[�h��ݒ肷��B</summary>
    ''' <param name="strSosikiCd">�I�����ꂽ�g�D���x���R�[�h</param>
    Private Sub setBusyo(ByVal strSosikiCd As String)

        'EMAB��Q�Ή����̊i�[����
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        Dim dtBusyoCd As EigyouJyouhouDataSet.busyoCdDataTable
        If ViewState("eigyouManKbn") <> "0" Then

            If ViewState("sosikiLevel") = strSosikiCd Then
                dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo2(strSosikiCd, ViewState("busyo_cd"), "")
            Else
                dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo2(strSosikiCd, "", ViewState("t_sansyou_busyo_cd"))

            End If



        Else
            If ViewState("busyo_cd") = "0000" Or ViewState("t_sansyou_busyo_cd") = "0000" Then
                dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo(strSosikiCd)
            Else
                dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo2(strSosikiCd, ViewState("busyo_cd"), ViewState("t_sansyou_busyo_cd"))

            End If

        End If

        Me.ddlBusyoCd.Items.Clear()
        If dtBusyoCd.Rows.Count <> 1 Then
            Me.ddlBusyoCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        End If
        For i As Integer = 0 To dtBusyoCd.Rows.Count - 1
            Dim ddlist As New ListItem
            ddlist = New ListItem
            ddlist.Text = dtBusyoCd.Rows(i).Item(0).ToString & "�F" & dtBusyoCd.Rows(i).Item(1).ToString
            ddlist.Value = dtBusyoCd.Rows(i).Item(0).ToString
            ddlBusyoCd.Items.Add(ddlist)

        Next

    End Sub

    ''' <summary>
    ''' [�����X��2�A�Z���A�n��R�[�h�A�c�Ə��R�[�h�A�r���_�[NO�A��\�Җ��A�d�b�ԍ�]�����N�{�^���̃N�b���N����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2013.03.11���F�ǉ� ��A�J���V�X�e��</history>
    Private Sub common_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles busyo_mei1.Click, busyo_mei2.Click, _
                                                                                          DisplayName1.Click, DisplayName2.Click, _
                                                                                          hansokuhin_cd1.Click, hansokuhin_cd2.Click, _
                                                                                          kameiten_cd1.Click, kameiten_cd2.Click, _
                                                                                          torikesi1.Click, torikesi2.Click, _
                                                                                          kameiten_mei1.Click, kameiten_mei2.Click, _
                                                                                          kouji_cd1.Click, kouji_cd2.Click, _
                                                                                          todouhuken_mei1.Click, todouhuken_mei2.Click, _
                                                                                          tyousa_cd1.Click, tyousa_cd2.Click, _
                                                                                          kameiten_mei21.Click, kameiten_mei22.Click, _
                                                                                          jyuusyo11.Click, jyuusyo12.Click, _
                                                                                          keiretu_cd1.Click, keiretu_cd2.Click, _
                                                                                          eigyousyo_cd1.Click, eigyousyo_cd2.Click, _
                                                                                          builder_no1.Click, builder_no2.Click, _
                                                                                          daihyousya_mei1.Click, daihyousya_mei2.Click, _
                                                                                          tel_no1.Click, tel_no2.Click

        Dim strName As String = CType(sender, LinkButton).ID
        'strName = Replace(Replace(strName, "1", " ASC"), "2", " DESC")
        If Right(strName, 1).Equals("1") Then
            strName = Left(strName, strName.Length - 1) & " ASC"
        Else
            strName = Left(strName, strName.Length - 1) & " DESC"
        End If

        Dim dtKameitenInfo As Data.DataTable = ViewState("dtKameitenInfo")

        Dim dv As DataView = dtKameitenInfo.DefaultView
        dv.Sort = strName

        Me.grdItiran.DataSource = dv
        Me.grdItiran.DataBind()
        '-------------------------from 2013.03.11���F�ǉ�-------------------
        Me.grdItiranRight.DataSource = dv
        Me.grdItiranRight.DataBind()
        '-------------------------to   2013.03.11���F�ǉ�-------------------

        'If ViewState("strID") <> "" Then
        '    CType(Me.FindControl(ViewState("strID")), LinkButton).ForeColor = Drawing.Color.SkyBlue
        'End If
        'ViewState("strID") = CType(sender, LinkButton).ClientID
        busyo_mei1.ForeColor = Drawing.Color.SkyBlue
        busyo_mei2.ForeColor = Drawing.Color.SkyBlue
        DisplayName1.ForeColor = Drawing.Color.SkyBlue
        DisplayName2.ForeColor = Drawing.Color.SkyBlue
        hansokuhin_cd1.ForeColor = Drawing.Color.SkyBlue
        hansokuhin_cd2.ForeColor = Drawing.Color.SkyBlue
        kameiten_cd1.ForeColor = Drawing.Color.SkyBlue
        kameiten_cd2.ForeColor = Drawing.Color.SkyBlue
        '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
        torikesi1.ForeColor = Drawing.Color.SkyBlue
        torikesi2.ForeColor = Drawing.Color.SkyBlue
        '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
        kameiten_mei1.ForeColor = Drawing.Color.SkyBlue
        kameiten_mei2.ForeColor = Drawing.Color.SkyBlue
        kouji_cd1.ForeColor = Drawing.Color.SkyBlue
        kouji_cd2.ForeColor = Drawing.Color.SkyBlue
        todouhuken_mei1.ForeColor = Drawing.Color.SkyBlue
        todouhuken_mei2.ForeColor = Drawing.Color.SkyBlue
        tyousa_cd1.ForeColor = Drawing.Color.SkyBlue
        tyousa_cd2.ForeColor = Drawing.Color.SkyBlue

        '-------------------------from 2013.04.02���F�ǉ�-------------------
        kameiten_mei21.ForeColor = Drawing.Color.SkyBlue
        kameiten_mei22.ForeColor = Drawing.Color.SkyBlue
        jyuusyo11.ForeColor = Drawing.Color.SkyBlue
        jyuusyo12.ForeColor = Drawing.Color.SkyBlue
        keiretu_cd1.ForeColor = Drawing.Color.SkyBlue
        keiretu_cd2.ForeColor = Drawing.Color.SkyBlue
        eigyousyo_cd1.ForeColor = Drawing.Color.SkyBlue
        eigyousyo_cd2.ForeColor = Drawing.Color.SkyBlue
        builder_no1.ForeColor = Drawing.Color.SkyBlue
        builder_no2.ForeColor = Drawing.Color.SkyBlue
        daihyousya_mei1.ForeColor = Drawing.Color.SkyBlue
        daihyousya_mei2.ForeColor = Drawing.Color.SkyBlue
        tel_no1.ForeColor = Drawing.Color.SkyBlue
        tel_no2.ForeColor = Drawing.Color.SkyBlue
        '-------------------------to   2013.04.02���F�ǉ�-------------------

        CType(sender, LinkButton).ForeColor = Drawing.Color.IndianRed

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")

        '��ʉ��X�N���[���ʒu��ݒ肷��
        SetScroll()

    End Sub
    ''' <summary> �����{�^���������̏���</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        '���̓`�F�b�N
        Dim blnDrop As Boolean = False
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId, blnDrop)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId, blnDrop)
            Exit Sub
        End If

        '���������ɂ���āA�c�Ə�����������B
        Dim dtParam As Data.DataTable = setParam()
        Dim dtKameitenInfo As Data.DataTable

        dtKameitenInfo = EigyouJyouhouInquiryBL.GetEigyouJyouhouInfo(dtParam, Me.chkBusyoCd.Checked)
        Me.grdItiran.DataSource = dtKameitenInfo
        Me.grdItiran.DataBind()
        '---------------------------From 2013.03.11���F�ǉ�--------------------
        Me.grdItiranRight.DataSource = dtKameitenInfo
        Me.grdItiranRight.DataBind()
        '---------------------------To   2013.03.11���F�ǉ�--------------------
        If dtKameitenInfo.Rows.Count = 0 Then
            busyo_mei1.Visible = False
            busyo_mei2.Visible = False
            DisplayName1.Visible = False
            DisplayName2.Visible = False
            hansokuhin_cd1.Visible = False
            hansokuhin_cd2.Visible = False
            kameiten_cd1.Visible = False
            kameiten_cd2.Visible = False
            '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            torikesi1.Visible = False
            torikesi2.Visible = False
            '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            kameiten_mei1.Visible = False
            kameiten_mei2.Visible = False
            kouji_cd1.Visible = False
            kouji_cd2.Visible = False
            todouhuken_mei1.Visible = False
            todouhuken_mei2.Visible = False
            tyousa_cd1.Visible = False
            tyousa_cd2.Visible = False

            '---------------------------From 2013.03.11���F�ǉ�--------------------
            '�����X��2
            kameiten_mei21.Visible = False
            kameiten_mei22.Visible = False
            '�Z��
            jyuusyo11.Visible = False
            jyuusyo12.Visible = False
            '�n��R�[�h
            keiretu_cd1.Visible = False
            keiretu_cd2.Visible = False
            '�c�Ə��R�[�h
            eigyousyo_cd1.Visible = False
            eigyousyo_cd2.Visible = False
            '�r���_�[NO
            builder_no1.Visible = False
            builder_no2.Visible = False
            '��\�Җ�
            daihyousya_mei1.Visible = False
            daihyousya_mei2.Visible = False
            '�d�b�ԍ�
            tel_no1.Visible = False
            tel_no2.Visible = False
            '---------------------------To   2013.03.11���F�ǉ�--------------------
        Else
            ViewState("dtParam") = dtParam
            busyo_mei1.ForeColor = Drawing.Color.SkyBlue
            busyo_mei2.ForeColor = Drawing.Color.SkyBlue
            DisplayName1.ForeColor = Drawing.Color.SkyBlue
            DisplayName2.ForeColor = Drawing.Color.SkyBlue
            hansokuhin_cd1.ForeColor = Drawing.Color.SkyBlue
            hansokuhin_cd2.ForeColor = Drawing.Color.SkyBlue
            kameiten_cd1.ForeColor = Drawing.Color.IndianRed
            kameiten_cd2.ForeColor = Drawing.Color.SkyBlue
            '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            torikesi1.ForeColor = Drawing.Color.SkyBlue
            torikesi2.ForeColor = Drawing.Color.SkyBlue
            '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            kameiten_mei1.ForeColor = Drawing.Color.SkyBlue
            kameiten_mei2.ForeColor = Drawing.Color.SkyBlue
            kouji_cd1.ForeColor = Drawing.Color.SkyBlue
            kouji_cd2.ForeColor = Drawing.Color.SkyBlue
            todouhuken_mei1.ForeColor = Drawing.Color.SkyBlue
            todouhuken_mei2.ForeColor = Drawing.Color.SkyBlue
            tyousa_cd1.ForeColor = Drawing.Color.SkyBlue
            tyousa_cd2.ForeColor = Drawing.Color.SkyBlue
            '---------------------------From 2013.03.11���F�ǉ�--------------------
            '�����X��2
            kameiten_mei21.ForeColor = Drawing.Color.SkyBlue
            kameiten_mei22.ForeColor = Drawing.Color.SkyBlue
            '�Z��
            jyuusyo11.ForeColor = Drawing.Color.SkyBlue
            jyuusyo12.ForeColor = Drawing.Color.SkyBlue
            '�n��R�[�h
            keiretu_cd1.ForeColor = Drawing.Color.SkyBlue
            keiretu_cd2.ForeColor = Drawing.Color.SkyBlue
            '�c�Ə��R�[�h
            eigyousyo_cd1.ForeColor = Drawing.Color.SkyBlue
            eigyousyo_cd2.ForeColor = Drawing.Color.SkyBlue
            '�r���_�[NO
            builder_no1.ForeColor = Drawing.Color.SkyBlue
            builder_no2.ForeColor = Drawing.Color.SkyBlue
            '��\�Җ�
            daihyousya_mei1.ForeColor = Drawing.Color.SkyBlue
            daihyousya_mei2.ForeColor = Drawing.Color.SkyBlue
            '�d�b�ԍ�
            tel_no1.ForeColor = Drawing.Color.SkyBlue
            tel_no2.ForeColor = Drawing.Color.SkyBlue
            '---------------------------To   2013.03.11���F�ǉ�--------------------
            busyo_mei1.Visible = True
            busyo_mei2.Visible = True
            DisplayName1.Visible = True
            DisplayName2.Visible = True
            hansokuhin_cd1.Visible = True
            hansokuhin_cd2.Visible = True
            kameiten_cd1.Visible = True
            kameiten_cd2.Visible = True
            '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            torikesi1.Visible = True
            torikesi2.Visible = True
            '=============2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            kameiten_mei1.Visible = True
            kameiten_mei2.Visible = True
            kouji_cd1.Visible = True
            kouji_cd2.Visible = True
            todouhuken_mei1.Visible = True
            todouhuken_mei2.Visible = True
            tyousa_cd1.Visible = True
            tyousa_cd2.Visible = True
            '---------------------------From 2013.03.11���F�ǉ�--------------------
            '�����X��2
            kameiten_mei21.Visible = True
            kameiten_mei22.Visible = True
            '�Z��
            jyuusyo11.Visible = True
            jyuusyo12.Visible = True
            '�n��R�[�h
            keiretu_cd1.Visible = True
            keiretu_cd2.Visible = True
            '�c�Ə��R�[�h
            eigyousyo_cd1.Visible = True
            eigyousyo_cd2.Visible = True
            '�r���_�[NO
            builder_no1.Visible = True
            builder_no2.Visible = True
            '��\�Җ�
            daihyousya_mei1.Visible = True
            daihyousya_mei2.Visible = True
            '�d�b�ԍ�
            tel_no1.Visible = True
            tel_no2.Visible = True
            '---------------------------To   2013.03.11���F�ǉ�--------------------

        End If

        '��ʉ��X�N���[���ʒu��ݒ肷��
        'SetScroll()

        '�������ʌ�����ݒ肷��B
        Dim intCount As Integer
        'If Me.ddlSosikiLevel.SelectedItem.Text = "0�FALL" OrElse Me.ddlSosikiLevel.SelectedItem.Text = String.Empty Then
        '    intCount = EigyouJyouhouInquiryBL.GetEigyouJyouhouCountInfo(dtParam, False)
        'Else

        'End If
        intCount = EigyouJyouhouInquiryBL.GetEigyouJyouhouCountInfo(dtParam, Me.chkBusyoCd.Checked)
        '  intCount = EigyouJyouhouInquiryBL.GetEigyouJyouhouCountInfo(dtParam, Me.chkBusyoCd.Checked)
        If Me.ddlSearchCount.SelectedValue = "max" Then
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
            '----------------From 2013.03.11���F�ǉ�-----------------------
            scrollHeight = intCount * 22 + 1
            '----------------To   2013.03.11���F�ǉ�-----------------------
        Else
            If intCount > Me.ddlSearchCount.SelectedValue Then
                Me.lblCount.Text = Me.ddlSearchCount.SelectedValue & " / " & intCount
                Me.lblCount.ForeColor = Drawing.Color.Red
                '----------------From 2013.03.11���F�ǉ�-----------------------
                scrollHeight = 100 * 22 + 1
                '----------------To   2013.03.11���F�ǉ�-----------------------
            Else
                Me.lblCount.Text = dtKameitenInfo.Rows.Count
                Me.lblCount.ForeColor = Drawing.Color.Black
                '----------------From 2013.03.11���F�ǉ�-----------------------
                scrollHeight = dtKameitenInfo.Rows.Count * 22 + 1
                '----------------To   2013.03.11���F�ǉ�-----------------------
            End If
        End If

        ViewState("dtKameitenInfo") = dtKameitenInfo
        ViewState("scrollHeight") = scrollHeight


        '�n�񖼂�ݒu�B
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
        If Me.tbxKeiretuCd.Text <> String.Empty Then
            Dim dtKeiretuTable As New CommonSearchDataSet.KeiretuTableDataTable
            dtKeiretuTable = CommonSearchLogic.GetKeiretuKensakuInfo(1, _
                                                                    ViewState("strKensakuKubun"), _
                                                                    ViewState("KeiretuCd"), _
                                                                    "", _
                                                                    ViewState("blnDelete"))
            If dtKeiretuTable.Rows.Count > 0 Then
                Me.tbxKeiretuMei.Text = dtKeiretuTable.Rows(0).Item("keiretu_mei").ToString
            Else
                Me.tbxKeiretuMei.Text = String.Empty
            End If
        Else
            Me.tbxKeiretuMei.Text = String.Empty
        End If

        '�S���c�Ɩ���ݒu�B
        If Me.tbxTantouEigyouID.Text <> String.Empty Then
            Dim dtUserInfo As New CommonSearchDataSet.BirudaTableDataTable
            dtUserInfo = CommonSearchLogic.GetUserInfo(1, _
                                                        Me.tbxTantouEigyouID.Text.ToString, _
                                                        "", _
                                                        ViewState("blnDelete"))
            If dtUserInfo.Rows.Count > 0 Then
                Me.tbxTantouEigyouSyaMei.Text = dtUserInfo.Rows(0).Item("mei").ToString
            Else
                Me.tbxTantouEigyouSyaMei.Text = String.Empty
            End If
        Else
            Me.tbxTantouEigyouSyaMei.Text = String.Empty
        End If

        '���ׂ̃��C�A�E�g��ݒ肷��B
        If intCount > 8 Then
            'Me.divMeisai.Attributes.Add("style", "height: 177px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:975px;")
        Else
            'Me.divMeisai.Attributes.Add("style", "height: 177px;border: 1px inset black;top: 0px;position: relative; left: 0px;overflow: auto;width:958px;")
        End If

        '�擾�����f�[�^���Ȃ��ꍇ
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty, False)
        End If

    End Sub

    ''' <summary>���O�C�����[�U�[�̉c�ƃ}���敪�Ƒg�D���x�����擾����B</summary>
    Private Sub getEigyouManKbn()

        Dim dtEigyouManKbn As EigyouJyouhouDataSet.eigyouManKbnDataTable
        dtEigyouManKbn = EigyouJyouhouInquiryBL.GetEigyouManKbnInfo(ViewState("userId"))

        If dtEigyouManKbn.Rows.Count > 0 Then
            '0:�ʏ�A1:�V�l
            ViewState("eigyouManKbn") = TrimNull(dtEigyouManKbn.Rows(0).Item("eigyou_man_kbn"))

            ViewState("sosikiLevel") = TrimNull(dtEigyouManKbn.Rows(0).Item("sosiki_level"))

            ViewState("busyo_cd") = TrimNull(dtEigyouManKbn.Rows(0).Item("busyo_cd"))

            ViewState("t_sansyou_busyo_cd") = TrimNull(dtEigyouManKbn.Rows(0).Item("t_sansyou_busyo_cd"))

            ViewState("sosikiLevel2") = TrimNull(Replace(dtEigyouManKbn.Rows(0).Item("sosiki_level2"), "-1", ""))

            If ViewState("eigyouManKbn") = "" Then
                ViewState("busyo_cd") = "0000"
                ViewState("t_sansyou_busyo_cd") = "0000"
                ViewState("eigyouManKbn") = "0"
                ViewState("sosikiLevel") = "0"
                ViewState("sosikiLevel2") = "-1"
            End If
        Else
            ViewState("busyo_cd") = "0000"
            ViewState("t_sansyou_busyo_cd") = "0000"
            ViewState("eigyouManKbn") = "0"
            ViewState("sosikiLevel") = "0"
            ViewState("sosikiLevel2") = "-1"
        End If

        If ViewState("eigyouManKbn") = "1" Then
            chkBusyoCd.Checked = True
            chkBusyoCd.Enabled = False
        End If

    End Sub
    ''' <summary>�󔒕����̍폜����</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    ''' <summary>����������ݒ肷��B</summary>
    ''' <returns>�p�����[�^�̃e�[�u��</returns>
    Private Function setParam() As EigyouJyouhouDataSet.paramEigyouJyouhouDataTable

        Dim dtParam As New EigyouJyouhouDataSet.paramEigyouJyouhouDataTable
        Dim row As EigyouJyouhouDataSet.paramEigyouJyouhouRow = dtParam.NewparamEigyouJyouhouRow

        '�敪
        If Me.chkKubunAll.Checked Then
            row.kbn = String.Empty
            ViewState("strKensakuKubun") = String.Empty
        Else
            Dim strKbns As String = String.Empty
            If Me.Common_drop1.SelectedValue <> String.Empty Then
                strKbns = strKbns & Me.Common_drop1.SelectedValue & ","
            End If
            If Me.Common_drop2.SelectedValue <> String.Empty Then
                strKbns = strKbns & Me.Common_drop2.SelectedValue & ","
            End If
            If Me.Common_drop3.SelectedValue <> String.Empty Then
                strKbns = strKbns & Me.Common_drop3.SelectedValue & ","
            End If
            strKbns = strKbns.Substring(0, strKbns.Length - 1)
            row.kbn = strKbns
            ViewState("strKensakuKubun") = strKbns
        End If

        '�މ�������X��\������
        row.torikesi = IIf(Me.chkTaikai.Checked, "1", String.Empty)
        ViewState("blnDelete") = IIf(Me.chkTaikai.Checked, True, False)
        '�����X�R�[�h
        row.kameitenCd = Me.tbxKameitenCd.Text
        '�����X�J�i
        row.kameitenKana = Me.tbxKameitenKana.Text
        '�n��
        row.keiretuCd = Me.tbxKeiretuCd.Text
        ViewState("KeiretuCd") = Me.tbxKeiretuCd.Text.ToString
        '�d�b�ԍ�
        row.tel = Me.tbxTel.Text.Replace("-", String.Empty)
        If row.tel.Replace("%", String.Empty) = String.Empty Then
            row.tel = String.Empty
        End If
        '�s���{��
        row.todouhukenCd = Me.Common_drop.SelectedValue
        '�g�D���x��
        If Me.ddlSosikiLevel.SelectedItem.Text = "0�FALL" Then
            row.sosikiLevel = String.Empty
            row.BusyoCd = "0000"
        Else
            row.sosikiLevel = Me.ddlSosikiLevel.SelectedValue
            '�����R�[�h     
            row.BusyoCd = Me.ddlBusyoCd.SelectedValue
        End If

        '�S���c��ID
        row.tantouEigyouId = Me.tbxTantouEigyouID.Text
        '���O�C�����[�U�[�̉c�ƃ}���敪
        row.eigyouManKbn = ViewState("eigyouManKbn")
        '���O�C�����[�U�[�̑g�D���x��
        row.userSosikiLevel = ViewState("sosikiLevel")
        '���������
        row.kensakuCount = Me.ddlSearchCount.SelectedValue
        '���O�C�����[�U�[�h�c
        row.loginUserId = ViewState("userId")
        row.busyo_cd = ViewState("busyo_cd")
        row.t_sansyou_busyo_cd = ViewState("t_sansyou_busyo_cd")

        dtParam.AddparamEigyouJyouhouRow(row)

        Return dtParam

    End Function
    ''' <summary> JavaScript</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("   var objKbn1 = document.getElementById('" & Me.Common_drop1.DdlClientID & "')")
            .AppendLine("   var objKbn2 = document.getElementById('" & Me.Common_drop2.DdlClientID & "')")
            .AppendLine("   var objKbn3 = document.getElementById('" & Me.Common_drop3.DdlClientID & "')")
            .AppendLine("   var objKbnAll = document.getElementById('" & Me.chkKubunAll.ClientID & "')")
            .AppendLine("   var objSearchCount = document.getElementById('" & Me.ddlSearchCount.ClientID & "')")
            .AppendLine("   var objSearch = document.getElementById('" & Me.btnKensaku.ClientID & "')")
            .AppendLine("   function fncSetKubunVal(){")
            .AppendLine("       if(objKbnAll.checked == true){")
            .AppendLine("           objKbn1.selectedIndex = 0;")
            .AppendLine("           objKbn2.selectedIndex = 0;")
            .AppendLine("           objKbn3.selectedIndex = 0;")
            .AppendLine("           objKbn1.disabled = true;")
            .AppendLine("           objKbn2.disabled = true;")
            .AppendLine("           objKbn3.disabled = true;")
            .AppendLine("       }else{")
            .AppendLine("           objKbn1.disabled = false;")
            .AppendLine("           objKbn2.disabled = false;")
            .AppendLine("           objKbn3.disabled = false;")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("   function funDisableButton(){")

            '--------------------From 2013.03.07 ���F�ǉ�--------------------
            .AppendLine("       var strKameitenCd = document.getElementById('hidKameitenCd').value;")
            .AppendLine("       document.getElementById('" & Me.hidSentakuKameitenCd.ClientID & "').value = strKameitenCd;")
            .AppendLine("       var strSentakuKbn = document.getElementById('hidKbnCd').value;")
            .AppendLine("       document.getElementById('" & Me.hidSentakuKbn.ClientID & "').value = strSentakuKbn;")
            '--------------------To   2013.03.06 ���F�ǉ�--------------------

            .AppendLine("       document.getElementById('" & Me.btnKihonJyouhou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnTyuiJikou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnBukkenJyouhou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnYosinJyouhou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnKakakuJyouhou.ClientID & "').disabled = false;")

            '--------------------From 2013.03.06 ���F�ǉ�--------------------
            .AppendLine("       document.getElementById('" & Me.btnSiharaiTyousa.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnSiharaiKouji.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnHoukakusyo.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnTorihukiJyoukenKakuninhyou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnTyousaCard.ClientID & "').disabled = false;")
            '--------------------To   2013.03.06 ���F�ǉ�--------------------

            .AppendLine("   }")
            .AppendLine("   function fncGamenSenni(strGamenId){")
            .AppendLine("       var strKameitenCd = document.getElementById('hidKameitenCd').value;")
            .AppendLine("       if(strGamenId == '0')  window.close();")
            .AppendLine("       if(strGamenId == '1')  window.open('KihonJyouhouInquiry.aspx?strKameitenCd='+strKameitenCd)")
            .AppendLine("       if(strGamenId == '2')  window.open('TyuiJyouhouInquiry.aspx?strKameitenCd='+strKameitenCd)")
            .AppendLine("       if(strGamenId == '3')  window.open('BukkenJyouhouInquiry.aspx?strKameitenCd='+strKameitenCd)")
            .AppendLine("       if(strGamenId == '4')  window.open('YosinJyouhouInquiry.aspx?strKameitenCd='+strKameitenCd)")
            .AppendLine("       if(strGamenId == '5')  window.open('HanbaiKakakuMasterSearchList.aspx?sendSearchTerms='+'1'+'$$$'+strKameitenCd)")
            .AppendLine("   }")

            .AppendLine("   function fncClearWin(){")
            .AppendLine("       for(var i = 0;i < document.forms.length;i++){")
            .AppendLine("          c_form = document.forms[i];")
            .AppendLine("          for (var j = 0;j < c_form.elements.length;j++){")
            .AppendLine("              if(c_form.elements[j].type == 'text'){")
            .AppendLine("                  c_form.elements[j].value = '';")
            .AppendLine("              }")
            .AppendLine("              if(c_form.elements[j].type == 'checkbox'){")
            .AppendLine("                  c_form.elements[j].checked = false;")
            .AppendLine("              }")
            .AppendLine("          }")
            .AppendLine("       }")

            .AppendLine("       objKbn1.selectedIndex = 0;")
            .AppendLine("       objKbn1.disabled = true;")
            .AppendLine("       objKbn2.selectedIndex = 0;")
            .AppendLine("       objKbn2.disabled = true;")
            .AppendLine("       objKbn3.selectedIndex = 0;")
            .AppendLine("       objKbn3.disabled = true;")
            .AppendLine("       objKbnAll.checked = true;")
            .AppendLine("       document.getElementById('" & CType(Me.Common_drop.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & Me.ddlSearchCount.ClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & Me.ddlSosikiLevel.ClientID & "').selectedIndex = 0;")

            .AppendLine("       for(var i = document.getElementById('" & Me.ddlBusyoCd.ClientID & "').options.length-1;i >=0;i--){")
            .AppendLine("           document.getElementById('" & Me.ddlBusyoCd.ClientID & "').options.remove(i);")
            .AppendLine("       }")
            '.AppendLine("       objKbn1.focus();")
            .AppendLine("   }")
            .AppendLine("   function fncSetKameitenCd(){")
            .AppendLine("       var strKameitenCd = event.srcElement.parentNode.parentNode.childNodes[3].innerText; ")
            .AppendLine("       document.getElementById('hidKameitenCd').value = strKameitenCd; ")
            '-------------------------------FROM ���F2013.03.26�ǉ�----------------------
            .AppendLine("       var strKbnCd = event.srcElement.parentNode.parentNode.childNodes[4].childNodes[1].value; ")
            .AppendLine("       document.getElementById('hidKbnCd').value = strKbnCd; ")
            '-------------------------------TO   ���F2013.03.26�ǉ�----------------------
            .AppendLine("   }")
            '�n��R�[�h����������B
            .AppendLine("   function fncKeiretuSearch(){")
            .AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       var strkbn='�n��'")
            .AppendLine("       var blnTaikai ")
            .AppendLine("       var arrKubun = ''")
            .AppendLine("       if(objKbn1.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn1.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn2.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn3.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn3.value + ',';")
            .AppendLine("       }")
            .AppendLine("       arrKubun = arrKubun.substring(0,arrKubun.length-1);")
            .AppendLine("       if(document.getElementById('" & Me.chkTaikai.ClientID & "').checked){")
            .AppendLine("           blnTaikai = 'False' ")
            .AppendLine("       }else{")
            .AppendLine("           blnTaikai = 'True' ")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxKeiretuCd.ClientID & "&objMei=" & Me.tbxKeiretuMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxKeiretuCd.ClientID & "').value)+")
            .AppendLine("       '&strMei='+escape(eval('document.all.'+'" & Me.tbxKeiretuMei.ClientID & "').value)+")
            .AppendLine("       '&KensakuKubun='+arrKubun+'&blnDelete='+blnTaikai, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '�c�ƒS��ID����������B
            .AppendLine("   function fncUserSearch(){")
            .AppendLine("       var strkbn='���[�U�[';")
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd=" & Me.tbxTantouEigyouID.ClientID & "&objMei=" & Me.tbxTantouEigyouSyaMei.ClientID & "&strCd='+escape(eval('document.all.'+'" & Me.tbxTantouEigyouID.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & Me.tbxTantouEigyouSyaMei.ClientID & "').value)+'&blnDelete=True', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("   }")

            .AppendLine("   function fncShowModal(){")
            .AppendLine("      var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("      var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("      if(buyDiv.style.display=='none')")
            .AppendLine("      {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("      }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("      }")
            .AppendLine("   }")
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")

            '--------------------From 2013.03.11 ���F�ǉ�-----------------------
            .AppendLine("   function fncScrollV(){")
            .AppendLine("       var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("       divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("       divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   }")
            .AppendLine("   function fncScrollH(){")
            .AppendLine("       var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("       divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("       divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function fncSetScroll(){")
            .AppendLine("       var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("       document.getElementById('" & Me.hidScroll.ClientID & "').value = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function fncSetLineColor(obj,index){")
            .AppendLine("       document.getElementById('" & Me.hidSelectRowIndex.ClientID & "').value = index;")
            .AppendLine("       var obj1 = objEBI('" + Me.grdItiranRight.ClientID + "').childNodes[0].childNodes[index] ")
            .AppendLine("       setSelectedLineColor(obj,obj1);")
            .AppendLine("   }")
            .AppendLine("   function wheel(event){")
            .AppendLine("       var delta = 0;")
            .AppendLine("       if(!event)")
            .AppendLine("           event = window.event;")
            .AppendLine("       if (event.wheelDelta){")
            .AppendLine("           delta = event.wheelDelta/120;")
            .AppendLine("           if (window.opera)")
            .AppendLine("               delta = -delta;")
            .AppendLine("       } else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("       if (delta)")
            .AppendLine("           handle(delta);")
            .AppendLine("   }")
            .AppendLine("   function handle(delta){")
            .AppendLine("      var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("      if (delta < 0){")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("      }else{")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("      }")
            .AppendLine("   }")
            '--------------------To   2013.03.11 ���F�ǉ�-----------------------

            '��ʓ��̓`�F�b�N
            .AppendLine("   function fncNyuuryokuCheck(){")
            .AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(objSearchCount.value == 'max'){")
            .AppendLine("          if(!confirm('" & Messages.Instance.MSG007C & "'))return false;")
            .AppendLine("       }")
            .AppendLine("       ")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("   function fncClickRow()")
            .AppendLine("   {")
            .AppendLine("       var intSelectRowIndex = document.getElementById('" & Me.hidSelectRowIndex.ClientID & "').value;")
            .AppendLine("       objEBI('" + Me.grdItiran.ClientID + "').childNodes[0].childNodes[intSelectRowIndex].childNodes[0].childNodes[0].click();")
            .AppendLine("   }")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub

    ''' <summary>
    ''' ��ʉ��X�N���[���ʒu��ݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013.03.11 ���F ��A�J���V�X�e��</history>
    Public Sub SetScroll()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("divheadright.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
            .AppendLine("divbodyright.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
            .AppendLine("divHscroll.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
        End With
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetScroll", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' �I�������s���N���b�N����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClickSelectRow()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("setTimeout('fncClickRow();',100);")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ClickRow", csScript.ToString, True)
    End Sub

    ''' <summary>���̓`�F�b�N</summary>
    ''' <param name="strObjId">�N���C�A���gID</param>
    ''' <returns>������</returns>
    Public Function CheckInput(ByRef strObjId As String, ByRef blnDrop As Boolean) As String
        Dim csScript As New StringBuilder
        With csScript
            '�����X���p�p���`�F�b�N
            If Me.tbxKameitenCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd.Text, "�����X�R�[�h"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCd.ClientID
                End If
            End If
            '�����X�J�i���p�J�i�`�F�b�N
            If Me.tbxKameitenKana.Text.Replace("%", String.Empty) <> String.Empty Then
                .Append(commonCheck.CheckKatakana(Me.tbxKameitenKana.Text.Replace("%", String.Empty), "�����X�J�i", True))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenKana.ClientID
                End If
            End If
            '�n��R�[�h���p�p���`�F�b�N
            If Me.tbxKeiretuCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text, "�n��R�[�h"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKeiretuCd.ClientID
                End If
            End If
            '�d�b�ԍ����p�����`�F�b�N
            If Me.tbxTel.Text.Replace("-", "").Replace("%", String.Empty) <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxTel.Text.Replace("-", "").Replace("%", String.Empty), "�d�b�ԍ�"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTel.ClientID
                End If
            End If
            If ddlSosikiLevel.SelectedValue <> "" Then
                .Append(commonCheck.CheckHissuNyuuryoku(ddlBusyoCd.SelectedValue, "�����R�[�h"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.ddlBusyoCd.ClientID
                    blnDrop = True
                End If
            End If
            '�S���c��ID���p�p���`�F�b�N
            If Me.tbxTantouEigyouID.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxTantouEigyouID.Text, "�S���c��ID"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTantouEigyouID.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function

    ''' <summary>�G���[���b�Z�[�W���|�b�v�A�b�v����B</summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
    ''' <param name="strObjId">�N���C�A���gID</param>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String, ByVal blnDrop As Boolean)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("   fncSetKubunVal();")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                If blnDrop Then
                    .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                Else
                    .AppendLine("   document.getElementById('" & strObjId & "').select();")
                End If


            End If
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub

    ''' <summary>DIV�\��</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' gridview�f�[�^��bound��
    ''' </summary>
    ''' <history>2012/04/20 �ԗ� 405721�̗v�]�̑Ή� �ǉ�</history>
    Private Sub grdItiran_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItiran.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            '���
            Dim torikesi As Label = CType(e.Row.FindControl("lblTorikesi"), Label)

            If torikesi.Text.Trim.Equals("0") OrElse torikesi.Text.Trim.Equals(String.Empty) Then
                '=============2012/04/20 �ԗ� 405721�̗v�]�Ή� �ǉ���==================
                torikesi.Text = String.Empty
                '=============2012/04/20 �ԗ� 405721�̗v�]�Ή� �ǉ���==================
            Else
                '���
                torikesi.ForeColor = Drawing.Color.Red
                '�����X�R�[�h
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
            End If

        End If

    End Sub

    ''' <summary>
    ''' gridview�f�[�^��bound��
    ''' </summary>
    Private Sub grdItiranRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItiranRight.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            '���
            Dim torikesi As Label = CType(Me.grdItiran.Rows(e.Row.RowIndex).FindControl("lblTorikesi"), Label)
            '�����X��
            Dim kameitenMei As Label = CType(e.Row.FindControl("lblKameitenMei"), Label)

            If Not torikesi.Text.Trim.Equals(String.Empty) Then
                '�����X��
                kameitenMei.ForeColor = Drawing.Color.Red
            End If

        End If
    End Sub

    ''' <summary>
    ''' �x�������i�����j�{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.06</history>
    Private Sub btnSiharaiTyousa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiharaiTyousa.Click

        Dim strTys_seikyuu_saki_cd As String         '����������R�[�h tys_seikyuu_saki_cd
        Dim strTys_seikyuu_saki_brc As String        '����������}�� tys_seikyuu_saki_brc 
        Dim strTys_seikyuu_saki_kbn As String        '����������敪 tys_seikyuu_saki_kbn

        '�����X�}�X�^�Ńf�[�^���擾����
        Dim dtTyousaJyouhou As Data.DataTable
        dtTyousaJyouhou = KihonJyouhouBL.GetKameiten(Me.hidSentakuKameitenCd.Value)
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

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")
        '��ʉ��X�N���[���ʒu��ݒ肷��
        SetScroll()

        '�I�������s���N���b�N����
        Call Me.ClickSelectRow()
    End Sub

    ''' <summary>
    ''' �x�������i�H���j�{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.06</history>
    Private Sub btnSiharaiKouji_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiharaiKouji.Click

        Dim strTys_seikyuu_saki_cd As String         '�H��������R�[�h koj_seikyuu_saki_cd
        Dim strTys_seikyuu_saki_brc As String        '�H��������}��   koj_seikyuu_saki_brc 
        Dim strTys_seikyuu_saki_kbn As String        '�H��������敪   koj_seikyuu_saki_kbn

        '�����X�}�X�^�Ńf�[�^���擾����
        Dim dtTyousaJyouhou As Data.DataTable
        dtTyousaJyouhou = KihonJyouhouBL.GetKameiten(Me.hidSentakuKameitenCd.Value)
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

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")
        '��ʉ��X�N���[���ʒu��ݒ肷��
        SetScroll()

        '�I�������s���N���b�N����
        Call Me.ClickSelectRow()
    End Sub

    ''' <summary>
    ''' �񍐏��E�I�v�V�����{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.06</history>
    Private Sub btnHoukakusyo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHoukakusyo.Click


        '�����X���i�������@���ʑΉ��}�X�^�Ɖ���
        Dim strScript As String
        strScript = "objSrchWin = window.open('TokubetuTaiouMasterSearchList.aspx?sendSearchTerms=1$$$'+'" & Me.hidSentakuKameitenCd.Value & "');"
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_seikyuInputPage", strScript, True)


        '�����敪�F�@1-�����X
        '�����R�[�h: �����X����

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")
        '��ʉ��X�N���[���ʒu��ݒ肷��
        SetScroll()

        '�I�������s���N���b�N����
        Call Me.ClickSelectRow()
    End Sub

    ''' <summary>
    ''' ��������m�F�\�{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.26</history>
    Private Sub btnTorihukiJyoukenKakuninhyou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorihukiJyoukenKakuninhyou.Click

        Dim FilePath As String = earthAction.TorihukiJyoukenKakuninhyou(Me.hidSentakuKameitenCd.Value, Me.hidSentakuKbn.Value)

        Me.hidFile.Value = FilePath
        '�t�@�C���p�X�̑��݂��Ƃ𔻒f����
        If IO.File.Exists(FilePath.Replace("file:", String.Empty).Replace("/", "\")) Then
            Call GetFile()
        Else
            ShowMessage(Messages.Instance.MSG2076E, String.Empty, False)
        End If

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")
        '��ʉ��X�N���[���ʒu��ݒ肷��
        SetScroll()

        '�I�������s���N���b�N����
        Call Me.ClickSelectRow()
    End Sub

    ''' <summary>
    ''' �����J�[�h�{�^�����������鎞
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.26</history>
    Private Sub btnTyousaCard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousaCard.Click

        Dim FilePath As String = earthAction.TyousaCard(Me.hidSentakuKameitenCd.Value, Me.hidSentakuKbn.Value)

        Me.hidFile.Value = FilePath
        '�t�@�C���p�X�̑��݂��Ƃ𔻒f����
        If IO.File.Exists(FilePath.Replace("file:", String.Empty).Replace("/", "\")) Then
            Call GetFile()
        Else
            ShowMessage(Messages.Instance.MSG2077E, String.Empty, False)
        End If

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")
        '��ʉ��X�N���[���ʒu��ݒ肷��
        SetScroll()

        '�I�������s���N���b�N����
        Call Me.ClickSelectRow()
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

End Class