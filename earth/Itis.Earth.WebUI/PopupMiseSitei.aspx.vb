
Partial Public Class PopupMiseSitei
    Inherits System.Web.UI.Page

    Dim masterAjaxSM As New ScriptManager

    Dim JibanLogic As New JibanLogic
    Dim KidouType As String = String.Empty
    Dim Kbn As String = String.Empty
    Dim IsFc As String = String.Empty
    Dim TenMd As String = String.Empty

    Dim cl As New CommonLogic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        masterAjaxSM = AjaxScriptManager1

        '���N�G�X�g�l�i�[
        Kbn = Request("kbn")
        IsFc = Request("isfc")
        TenMd = Request("tenmd")
        HiddenKidouType.Value = Request("type")
        KidouType = HiddenKidouType.Value

        If IsPostBack = False Then

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            ' �敪�R���{�Ƀf�[�^���o�C���h����
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            helper.SetDropDownList(Me.SelectKubun, DropDownHelper.DropDownType.Kubun)

            '��ʕ\���ݒ�
            SetDisplay(sender, e)

        Else
            KidouType = HiddenKidouType.Value

        End If
    End Sub

    ''' <summary>
    ''' ��ʕ\���ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetDisplay(ByVal sender As Object, ByVal e As System.EventArgs)

        '�R���g���[�����ʐݒ�
        TextKameitenCd.Attributes("onblur") = "checkNumber(this);"
        TextKameitenCd.Attributes("onchange") = "clrKameitenInfo(this,objEBI('" & ButtonKameitenKennsaku.ClientID & "'),objEBI('" & HiddenClearFlg.ClientID & "'));"
        TextEigyousyoCd.Attributes("onchange") = "clrName(this,objEBI('" & ButtonEigyousyoSearch.ClientID & "'),objEBI('" & HiddenClearFlg.ClientID & "'));"
        RadioKameitenSitei.Attributes("onclick") = "objEBI('" & ButtonChangeSitei.ClientID & "').click();"
        RadioEigyousyoSitei.Attributes("onclick") = "objEBI('" & ButtonChangeSitei.ClientID & "').click();"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������R�e�L�X�g�{�b�N�X�̃X�^�C����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

        '���N�G�X�g������擾
        If IsFc <> String.Empty Then
            If IsFc = "0" Then
                RadioKameitenSitei.Checked = True
                RadioEigyousyoSitei.Checked = False
                SelectKubun.SelectedValue = Request("kbn")
                SetFocus(TextKameitenCd)
            Else
                RadioKameitenSitei.Checked = False
                RadioEigyousyoSitei.Checked = True
                SetFocus(TextEigyousyoCd)
            End If
        End If

        '��ʃR�����g�\���ؑ�
        SpanPopupMess1.InnerText = String.Empty
        SpanPopupMess2.InnerText = "�����X�܂��͉c�Ə����w�肵�Ă��������B"

        Select Case KidouType
            Case "tenbetu"
                RadioKameitenSitei.Checked = True
                SelectKubun.SelectedValue = "A"
                SetFocus(TextKameitenCd)
            Case "tenbetuA"
                SelectKubun.SelectedValue = Kbn
                SpanPopupMess1.InnerText = "�o�^/�C���������������܂����B"
            Case "hansoku"
                RadioKameitenSitei.Checked = True
                SelectKubun.SelectedValue = "S"
                SetFocus(TextKameitenCd)
            Case "hansokuA"
                SelectKubun.SelectedValue = Kbn
                SpanPopupMess1.InnerText = "�o�^/�C���������������܂����B"
        End Select

        ButtonChangeSitei_ServerClick(sender, e)

    End Sub

    ''' <summary>
    ''' �����X�����{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKameitenKennsaku_ServerClick1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKameitenKennsaku.ServerClick
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim total_count As Integer = 5
        Dim tmpScript As String = String.Empty

        '�����X�R�[�h=�����͂ł��N���A�t���O�������Ă���ꍇ
        If Trim(TextKameitenCd.Text) = String.Empty And HiddenClearFlg.Value = "1" Then
            '�����X��񏉊���
            Me.clrKameitenInfo()

            setFocusAJ(ButtonKameitenKennsaku)
            Exit Sub
        End If

        '�敪���I���̏ꍇ�A�G���[
        If SelectKubun.SelectedValue = String.Empty Then
            tmpScript = "alert('" & Messages.MSG006E & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "kbnerr", tmpScript, True)
            setFocusAJ(SelectKubun)
            Exit Sub
        End If

        ' �擾�������i�荞�ޏꍇ�A������ǉ����Ă�������
        If TextKameitenCd.Text <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(SelectKubun.SelectedValue, _
                                                                    TextKameitenCd.Text, _
                                                                    True, _
                                                                    total_count)
        End If

        If dataArray.Count = 1 Then
            '���i������ʂɃZ�b�g
            Dim recData As KameitenSearchRecord = dataArray(0)
            '�����X�R�[�h����꒼��
            Me.TextKameitenCd.Text = dataArray(0).KameitenCd

            '�����X���ݒ菈��
            kameitenSearchAfter(sender, e)

        Else
            TextKameitenMei.Text = String.Empty
            TextKeiretu.Text = String.Empty
            TextKameiEigyousyoMei.Text = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & SelectKubun.ClientID & EarthConst.SEP_STRING & TextKameitenCd.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            TextKameitenCd.ClientID & EarthConst.SEP_STRING & TextKameitenMei.ClientID & "','" & ButtonKameitenKennsaku.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            Exit Sub
        End If

        '�t�H�[�J�X�̐ݒ�
        Me.setFocusAJ(Me.ButtonKameitenKennsaku)

    End Sub

    ''' <summary>
    ''' �����X�������s�㏈��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchAfter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '�����X�������s�㏈��(�����X�ڍ׏��A�r���_�[���擾)
        Dim logic As New KameitenSearchLogic

        Dim record As KameitenSearchRecord = logic.GetKameitenSearchResult(SelectKubun.SelectedValue, TextKameitenCd.Text, "", True)

        If Not record Is Nothing Then
            Me.TextKameitenCd.Text = cl.GetDisplayString(record.KameitenCd)
            Me.TextKameitenMei.Text = cl.GetDisplayString(record.KameitenMei1)
            Me.TextTorikesiRiyuu.Text = cl.GetDisplayString(cl.getTorikesiRiyuu(record.Torikesi, record.KtTorikesiRiyuu)) '�����X������R
            Me.TextKeiretu.Text = cl.GetDisplayString(record.KeiretuMei)
            Me.TextKameiEigyousyoMei.Text = cl.GetDisplayString(record.EigyousyoMei)

            '�����X�R�[�h��ޔ�
            Me.HiddenKameitenCdOld.Value = Me.TextKameitenCd.Text

            '�����X�R�[�h/����/������R�̕����F�X�^�C��
            cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(record.Torikesi))
            cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(record.Torikesi))
            cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(record.Torikesi))
            cl.setStyleFontColor(Me.TextKeiretu.Style, cl.getKameitenFontColor(record.Torikesi))
            cl.setStyleFontColor(Me.TextKameiEigyousyoMei.Style, cl.getKameitenFontColor(record.Torikesi))
        Else
            '�����X��񏉊���
            Me.clrKameitenInfo()

        End If

    End Sub

    ''' <summary>
    ''' �����X�֘A�����N���A���A�����X�^�C����W��(��)�ɖ߂�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clrKameitenInfo(Optional ByVal blnKameitenClear As Boolean = True)
        '�����X�����N���A����ꍇ
        If blnKameitenClear Then
            '�����X�R�[�h
            Me.TextKameitenCd.Text = String.Empty
            '�����X��
            Me.TextKameitenMei.Text = String.Empty
            '�����X������R
            Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(0, String.Empty)
            '�n��
            TextKeiretu.Text = String.Empty
            '�c�Ə���
            TextKameiEigyousyoMei.Text = String.Empty

            HiddenKameitenCdOld.Value = String.Empty
        End If

        HiddenClearFlg.Value = String.Empty

        '�����X�R�[�h/����/������R�̕����F�X�^�C��
        cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKeiretu.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKameiEigyousyoMei.Style, cl.getKameitenFontColor(0))

    End Sub

    ''' <summary>
    ''' �c�Ə������{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonEigyousyoSearch_ServerClick1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEigyousyoSearch.ServerClick
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim blnResult As Boolean

        If Trim(TextEigyousyoCd.Text) = String.Empty And HiddenClearFlg.Value = "1" Then
            TextEigyousyoMei.Text = String.Empty
            HiddenClearFlg.Value = String.Empty
            HiddenEigyousyoCdOld.Value = String.Empty
            setFocusAJ(ButtonEigyousyoSearch)
            Exit Sub
        End If

        ' �c�Ə��}�X�^������
        blnResult = cl.CallEigyousyoSearchWindow(sender _
                                                 , e _
                                                 , Me _
                                                 , Me.TextEigyousyoCd _
                                                 , Me.TextEigyousyoMei _
                                                 , Me.ButtonEigyousyoSearch _
                                                 , True _
                                                 )

        If blnResult Then
            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.ButtonEigyousyoSearch)
            'Hidden�ɑޔ�
            HiddenEigyousyoCdOld.Value = TextEigyousyoCd.Text
        End If

    End Sub

    ''' <summary>
    ''' Ajax���쎞�̃t�H�[�J�X�Z�b�g
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)

        masterAjaxSM.SetFocus(ctrl)

    End Sub

    ''' <summary>
    ''' �w�胉�W�I�I��ύX
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonChangeSitei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '�����X�w�胉�W�I�{�^��
        Dim booleanSitei As Boolean = RadioKameitenSitei.Checked

        SelectKubun.Enabled = booleanSitei
        TextKameitenCd.Enabled = booleanSitei
        ButtonKameitenKennsaku.Disabled = (booleanSitei = False)
        TextEigyousyoCd.Enabled = (booleanSitei = False)
        ButtonEigyousyoSearch.Disabled = booleanSitei

        '�`�F�b�N���
        If booleanSitei Then
            '�c�Ə������N���A
            TextEigyousyoCd.Text = String.Empty
            HiddenEigyousyoCdOld.Value = String.Empty
            TextEigyousyoMei.Text = String.Empty

            Me.clrKameitenInfo(False)

            setFocusAJ(TextKameitenCd)
        Else
            '�����X�����N���A
            Me.clrKameitenInfo()

            setFocusAJ(TextEigyousyoCd)
        End If

    End Sub
End Class