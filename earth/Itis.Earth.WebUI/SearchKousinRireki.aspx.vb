Partial Public Class SearchKousinRireki
    Inherits System.Web.UI.Page

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Dim cookieKey As String = "earth_kensaku_checked"

    Dim cl As New CommonLogic
    Private DLogic As New DataLogic

    Dim MLogic As New MessageLogic
    Const pStrSpace As String = EarthConst.HANKAKU_SPACE

#Region "�s�R���g���[��ID�ړ���"
    Private Const CTRL_NAME_TR As String = "resultTr_"
    Private Const CTRL_NAME_HIDDEN_UNIQUE_NO As String = "HdnUniNo_"
#End Region


#Region "CSS�N���X��"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_DATE = "date"
#End Region

#Region "�p�����[�^/�e�Ɩ����"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _kbn As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrKbn() As String
        Get
            Return _kbn
        End Get
        Set(ByVal value As String)
            _kbn = value
        End Set
    End Property

    ''' <summary>
    ''' �ԍ�(�ۏ؏�No)
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _no As String
    ''' <summary>
    ''' �ԍ�(�ۏ؏�No)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrBangou() As String
        Get
            Return _no
        End Get
        Set(ByVal value As String)
            _no = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        Dim jSM As New JibanSessionManager

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' Key����ێ�
        Dim arrSearchTerm() As String

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then

            '���p�����[�^�̃`�F�b�N
            If Context.Items("sendSearchTerms") IsNot Nothing Then
                arrSearchTerm = Split(Context.Items("sendSearchTerms"), EarthConst.SEP_STRING)
            Else '�e�Ɩ���ʂ���̌ďo
                arrSearchTerm = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
            End If

            If arrSearchTerm.Length >= 2 Then
                pStrKbn = arrSearchTerm(0)     '�e��ʂ���POST���ꂽ���1 �F�敪
                pStrBangou = arrSearchTerm(1)     '�e��ʂ���POST���ꂽ���2 �F�ۏ؏�NO
            End If

            '���A�J�E���g�}�X�^�ւ̓o�^�L�����`�F�b�N(������΃��C���֖߂�)
            If userinfo.AccountNo = 0 _
                Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            ' �敪�R���{�Ƀf�[�^���o�C���h����
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic

            ' �敪�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKubun, DropDownHelper.DropDownType.Kubun)
            ' �X�V���ڋ敪�R���{�Ƀf�[�^���o�C���h����
            helper.SetKtMeisyouDropDownList(Me.SelectKousinKoumoku, EarthConst.emKtMeisyouType.KOUSIN_KOUMOKU, True, False)

            '****************************************************************************
            ' �n�Ճf�[�^�擾
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            '�n�Փǂݍ��݃f�[�^���Z�b�V�����ɑ��݂���ꍇ�A��ʂɕ\��������
            If logic.ExistsJibanData(pStrKbn, pStrBangou) And jibanRec IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jibanRec) '�n�Ճf�[�^���R���g���[���ɃZ�b�g
            End If

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()
            SelectKubun.Focus()

            If SelectKubun.SelectedValue <> String.Empty And TextHosyousyoNo.Value <> String.Empty Then
                search_ServerClick(sender, e)
            Else
                Me.ButtonClose.Visible = False
            End If

        End If

    End Sub


    ''' <summary>
    ''' �n�Ճ��R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s���i�Q�Ə����p�j
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        '��ʃR���g���[���ɐݒ�
        SelectKubun.SelectedValue = jr.Kbn

        Me.TextHosyousyoNo.Value = cl.GetDispStr(jr.HosyousyoNo) '�ԍ�

    End Sub


#Region "�{�^���C�x���g"

    ''' <summary>
    ''' ���������̎��s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick

        Dim MyLogic As New KousinRirekiLogic
        Dim listResult As List(Of KousinRirekiDataRecord)

        ' �����{�^���Ƀt�H�[�J�X
        btnSearch.Focus()

        'Key���R�[�h�N���X
        Dim recKey As New KousinRirekiDataKeyRecord

        '�{�^���������_�ł̃f�t�H���g�N����ʑI����Ԃ��N�b�L�[�ɕۑ�
        Dim Cookie As HttpCookie = New HttpCookie(cookieKey)
        Cookie.Values.Add(chkHyoujiGamen1.ID, chkHyoujiGamen1.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen2.ID, chkHyoujiGamen2.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen3.ID, chkHyoujiGamen3.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen4.ID, chkHyoujiGamen4.Checked.ToString())
        Cookie.Expires = DateTime.MaxValue ' �i���I�ۑ��N�b�L�[
        Response.AppendCookie(Cookie)

        '�����������̎擾
        Me.SetSearchKeyFromCtrl(recKey)

        '�\���ő匏��
        Dim end_count As Integer = IIf(maxSearchCount.Value <> "max", maxSearchCount.Value, EarthConst.MAX_RESULT_COUNT)
        Dim total_count As Integer = 0

        '��Key���ڂ����ɁA�Y���f�[�^��DB���璊�o
        listResult = MyLogic.getSearchKeyDataList(sender, recKey, 1, end_count, total_count)

        '����ʂɃZ�b�g
        If total_count = 0 Then
            ' �������ʃ[�����̏ꍇ�A���b�Z�[�W��\��
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
        ElseIf total_count = -1 Then
            ' �������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Exit Sub
        End If

        '�\��������������
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")
        If maxSearchCount.Value <> "max" Then
            If Val(maxSearchCount.Value) < total_count Then
                resultCount.Style("color") = "red"
                displayCount = maxSearchCount.Value & " / " & CommonLogic.Instance.GetDisplayString(total_count)
            End If
        End If

        '�������ʌ�����ݒ�
        resultCount.InnerHtml = displayCount

        '����ʂɃZ�b�g
        Me.SetCtrlFromDataRec(listResult)

    End Sub

    ''' <summary>
    ''' �����X�����{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonkameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kameitenSearch.ServerClick
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)

        Dim total_count As Integer

        Dim tmpScript As String = String.Empty

        'If SelectKubun.SelectedValue = String.Empty Then
        '    '�敪���I���̏ꍇ�A�G���[
        '    MLogic.AlertMessage(sender, Messages.MSG006E, 0, "error")
        '    masterAjaxSM.SetFocus(SelectKubun)
        '    Exit Sub
        'End If

        ' �擾�������i�荞�ޏꍇ�A������ǉ����Ă�������
        If TextKameitenCd.Value <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(SelectKubun.SelectedValue, _
                                                                    TextKameitenCd.Value, _
                                                                    False, _
                                                                    total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As KameitenSearchRecord = dataArray(0)
            TextKameitenCd.Value = recData.KameitenCd
            kameitenNm.Value = recData.KameitenMei1
            Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu)

            '�����X�R�[�h/����/������R�̕����F�X�^�C��
            cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.kameitenNm.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(recData.Torikesi))

            '�t�H�[�J�X�Z�b�g
            masterAjaxSM.SetFocus(kameitenSearch)
        Else
            '�t�H�[�J�X�Z�b�g
            Dim tmpFocusScript = "objEBI('" & kameitenSearch.ClientID & "').focus();"

            tmpScript = "callSearch('" & SelectKubun.ClientID & EarthConst.SEP_STRING & TextKameitenCd.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            TextKameitenCd.ClientID & EarthConst.SEP_STRING & kameitenNm.ClientID & "','" & kameitenSearch.ClientID & "', 'SearchKameitenWindow');"

            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �I���s�_�u���N���b�N���̎��s����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSend_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.ServerClick

    End Sub

#End Region


#Region "�v���C�x�[�g���\�b�h"

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �������s�{�^���֘A
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�������s�{�^���̃C�x���g�n���h����ݒ�
        btnSearch.Attributes("onclick") = "checkJikkou();"

        '�}�X�^�����n�R�[�h���͍��ڃC�x���g�n���h���ݒ�
        TextKameitenCd.Attributes("onblur") = "checkNumber(this);clrKameitenInfo(this);"

        '�N�b�L�[����f�t�H���g�N����ʑI����Ԃ��擾
        Dim Cookie As HttpCookie
        If Request.Cookies(cookieKey) IsNot Nothing Then
            Cookie = Request.Cookies(cookieKey)
            chkHyoujiGamen1.Checked = Cookie.Values(chkHyoujiGamen1.ID)
            chkHyoujiGamen2.Checked = Cookie.Values(chkHyoujiGamen2.ID)
            chkHyoujiGamen3.Checked = Cookie.Values(chkHyoujiGamen3.ID)
            chkHyoujiGamen4.Checked = Cookie.Values(chkHyoujiGamen4.ID)
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������R�e�L�X�g�{�b�N�X�̃X�^�C����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing)

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ��猟���L�[���R�[�h�ւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="recKey">���o���R�[�h�N���X�̃L�[</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As KousinRirekiDataKeyRecord)

        '�X�V�� FROM
        recKey.KousinbiFrom = IIf(Me.TextKousinbiFrom.Value <> String.Empty, Me.TextKousinbiFrom.Value, DateTime.MinValue)
        '�X�V�� TO
        recKey.KousinbiTo = IIf(Me.TextKousinbiTo.Value <> String.Empty, Me.TextKousinbiTo.Value, DateTime.MinValue)
        '�敪
        recKey.Kubun = IIf(Me.SelectKubun.SelectedValue <> String.Empty, Me.SelectKubun.SelectedValue, String.Empty)
        '�ۏ؏�NO
        recKey.HosyousyoNo = IIf(Me.TextHosyousyoNo.Value <> String.Empty, Me.TextHosyousyoNo.Value, String.Empty)
        '�X�V����
        recKey.KousinKoumoku = IIf(Me.SelectKousinKoumoku.SelectedItem.Text <> String.Empty, Me.SelectKousinKoumoku.SelectedItem.Text, String.Empty)
        '�X�V��
        recKey.Kousinsya = IIf(Me.TextKousinsya.Value <> String.Empty, Me.TextKousinsya.Value, String.Empty)

        '�ŐV�����X
        recKey.KameitenCd = IIf(Me.TextKameitenCd.Value <> String.Empty, Me.TextKameitenCd.Value, String.Empty)
        '�ŐV�����X�J�i
        recKey.KameitenKana = IIf(Me.TextKameitenKana.Value <> String.Empty, Me.TextKameitenKana.Value, String.Empty)
        '�X�V�O�l
        recKey.KousinBeforeValue = IIf(Me.TextKousinBeforeValue.Value <> String.Empty, Me.TextKousinBeforeValue.Value, String.Empty)
        '�X�V��l
        recKey.KousinAfterValue = IIf(Me.TextKousinAfterValue.Value <> String.Empty, Me.TextKousinAfterValue.Value, String.Empty)

    End Sub

    ''' <summary>
    ''' ��ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="listResult">KousinRirekiDataRecord</param>
    ''' <remarks></remarks>
    Private Sub SetCtrlFromDataRec(ByVal listResult As List(Of KousinRirekiDataRecord))
        '��������1���݂̂̏ꍇ�̗�ID�i�[�phidden���N���A
        firstSend.Value = ""
        Dim lineCounter As Integer = 0

        '�e�Z���̕��ݒ�p�̃��X�g�쐬�i�^�C�g���s�̕����x�[�X�ɂ���j
        Dim widthList1 As New List(Of String)
        Dim tableWidth1 As Integer = 0

        '************************
        '* ��ʃe�[�u���֏o��
        '************************
        '�s�J�E���^
        Dim rowCnt As Integer = 0
        Dim strSpace As String = EarthConst.HANKAKU_SPACE
        Dim objTr1 As New HtmlTableRow


        '�擾�����f�[�^����ʂɕ\��
        For Each data As KousinRirekiDataRecord In listResult

            rowCnt += 1
            lineCounter += 1

            'Tr
            objTr1 = New HtmlTableRow

            Dim objTdKousinNitiji As New HtmlTableCell      '�X�V����
            Dim objTdKubun As New HtmlTableCell             '�敪
            Dim objTdHosyousyoNo As New HtmlTableCell       '�ۏ؏�NO
            Dim objTdKousinKoumoku As New HtmlTableCell     '�X�V����
            Dim objTdKousinPreVal As New HtmlTableCell      '�X�V�O�l
            Dim objTdKousinPostVal As New HtmlTableCell     '�X�V��l
            Dim objTdKousinsya As New HtmlTableCell         '�X�V��

            Dim objIptRtnHiddn1 As New HtmlInputHidden

            Dim objTdOtherWin As New HtmlTableCell
            Dim objAncOtherWin1 As New HtmlAnchor
            Dim objAncOtherWin2 As New HtmlAnchor
            Dim objAncOtherWin3 As New HtmlAnchor
            Dim objAncOtherWin4 As New HtmlAnchor
            Dim objSpace1 As New HtmlGenericControl
            Dim objSpace2 As New HtmlGenericControl
            Dim objSpace3 As New HtmlGenericControl

            Dim strKtInfoColor As String = EarthConst.STYLE_COLOR_BLACK

            '�l�̐ݒ�
            objTdKousinNitiji.InnerHtml = DLogic.dtTime2Str(data.KousinNitiji, EarthConst.FORMAT_DATE_TIME_7)
            objTdKubun.InnerHtml = data.Kubun
            objTdHosyousyoNo.InnerHtml = data.HosyousyoNo
            objTdKousinKoumoku.InnerHtml = data.KousinKoumoku
            objTdKousinPreVal.InnerHtml = cl.GetDisplayString(data.KousinPreValue, EarthConst.HANKAKU_SPACE)
            objTdKousinPostVal.InnerHtml = cl.GetDisplayString(data.KousinPostValue, EarthConst.HANKAKU_SPACE)
            objTdKousinsya.InnerHtml = cl.GetDisplayString(data.Kousinsya, EarthConst.HANKAKU_SPACE)

            objIptRtnHiddn1.ID = "returnHidden" & lineCounter
            objIptRtnHiddn1.Value = data.Kubun & EarthConst.SEP_STRING & data.HosyousyoNo

            objSpace1.InnerHtml = pStrSpace & pStrSpace
            objSpace2.InnerHtml = pStrSpace & pStrSpace
            objSpace3.InnerHtml = pStrSpace & pStrSpace

            objAncOtherWin1.InnerText = "��"
            objAncOtherWin1.HRef = "javascript:void(0)"
            objAncOtherWin1.Attributes("onclick") = "returnSelectValueOtherWin(this,1);"
            objAncOtherWin1.Attributes("tabindex") = "-1"
            objAncOtherWin2.InnerText = "��"
            objAncOtherWin2.HRef = "javascript:void(0)"
            objAncOtherWin2.Attributes("onclick") = "returnSelectValueOtherWin(this,2);"
            objAncOtherWin2.Attributes("tabindex") = "-1"
            objAncOtherWin3.InnerText = "�H"
            objAncOtherWin3.HRef = "javascript:void(0)"
            objAncOtherWin3.Attributes("onclick") = "returnSelectValueOtherWin(this,3);"
            objAncOtherWin3.Attributes("tabindex") = "-1"
            objAncOtherWin4.InnerText = "��"
            objAncOtherWin4.HRef = "javascript:void(0)"
            objAncOtherWin4.Attributes("onclick") = "returnSelectValueOtherWin(this,4);"
            objAncOtherWin4.Attributes("tabindex") = "-1"

            objTdOtherWin.Style("text-align") = "center"

            With objTdOtherWin.Controls
                .Add(objIptRtnHiddn1)  '�߂�v�f(hidden)�́A�K���擪��̐擪��Input�^�O�ɃZ�b�g���邱��
                .Add(objAncOtherWin1)
                .Add(objSpace1)
                .Add(objAncOtherWin2)
                .Add(objSpace2)
                .Add(objAncOtherWin3)
                .Add(objSpace3)
                .Add(objAncOtherWin4)
            End With

            '�X�^�C���A�N���X�ݒ�
            objTdKousinNitiji.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdKubun.Attributes("class") = CSS_TEXT_CENTER
            objTdHosyousyoNo.Attributes("class") = CSS_TEXT_CENTER

            '�sID��JS�C�x���g�̕t�^
            objTr1.ID = CTRL_NAME_TR & rowCnt
            If rowCnt = 1 Then
                objTr1.Attributes("tabindex") = 0
                objTr1.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr1.Attributes("tabindex") = -1
            End If

            '�s�ɃZ�����Z�b�g1
            With objTr1.Controls
                .Add(objTdOtherWin)
                .Add(objTdKousinNitiji)
                .Add(objTdKubun)
                .Add(objTdHosyousyoNo)
                .Add(objTdKousinKoumoku)
                .Add(objTdKousinPreVal)
                .Add(objTdKousinPostVal)
                .Add(objTdKousinsya)
            End With

            '�e�[�u���ɍs���Z�b�g
            Me.searchGrid.Controls.Add(objTr1)

            If listResult.Count = 1 Then
                '��������1���݂̂̏ꍇ�̗�ID�i�[�phidden�ɒl���Z�b�g
                firstSend.Value = objTr1.ClientID
            End If

        Next

    End Sub
#End Region

End Class