Partial Public Class PopupBukkenSintyokuJyky
    Inherits System.Web.UI.Page

    '�s�R���g���[��ID�ړ���
    Private Const CTRL_NAME_TR As String = "resultTr_"
    Private Const SELECT_SYUBETU_CTRL_NAME As String = "SelectSyubetu_"

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic
    Private dtRec As New HosyousyoKanriRecord

    Const CSS_TEXT_CENTER = "textCenter"
    Const CSS_DATE = "date"

#Region "�v���p�e�B"

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
    ''' �ԍ�
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _no As String
    ''' <summary>
    ''' �ԍ�
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

    ''' <summary>
    ''' ��ʃ��[�h
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _GamenMode As String = String.Empty
    ''' <summary>
    ''' ��ʃ��[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrGamenMode() As String
        Get
            Return _GamenMode
        End Get
        Set(ByVal value As String)
            _GamenMode = value
        End Set
    End Property

#End Region

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

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ
            cl.CloseWindow(Me)
            Exit Sub
        End If

        If IsPostBack = False Then '�����N����

            '���p�����[�^�̃`�F�b�N
            Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
            ' Key����ێ�
            pStrKbn = arrSearchTerm(0)     '�e��ʂ���POST���ꂽ���1 �F�敪
            pStrBangou = arrSearchTerm(1)     '�e��ʂ���POST���ꂽ���2 �F�ۏ؏�NO

            ' �p�����[�^�s�����͉�ʂ�\�����Ȃ�
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                cl.CloseWindow(Me)
                Exit Sub
            End If

            '���A�J�E���g�}�X�^�ւ̓o�^�L�����`�F�b�N(������΃��C���֖߂�)
            If userinfo.AccountNo = 0 _
                Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' �n�Ճf�[�^�擾
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            '�n�Փǂݍ��݃f�[�^���Z�b�V�����ɑ��݂���ꍇ�A��ʂɕ\��������
            If jibanRec IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jibanRec) '�n�Ճf�[�^���R���g���[���ɃZ�b�g
            Else
                cl.CloseWindow(Me)
                Exit Sub
            End If

            '****************************************************************************
            ' �ۏ؏��Ǘ����R�[�h�擾
            '****************************************************************************
            Dim rec As New HosyousyoKanriRecord '�ۏ؏��Ǘ����R�[�h
            Me.SetCtrlFromDataRec(sender, e, rec) '�ۏ؏��Ǘ����R�[�h�����ʕ\�����ڂւ̒l�Z�b�g

            '****************************************************************************
            ' �i���f�[�^�擾
            '****************************************************************************
            If rec.HosyousyoNo <> String.Empty Then
                Dim reportRec As New ReportIfGetRecord '�i���f�[�^���R�[�h
                logic.GetReportIfData(jibanRec, reportRec) '�i���f�[�^�擾
                Me.SetCtrlFromReportIFDataRec(sender, e, jibanRec, rec, reportRec) '�f�[�^���R���g���[���ɃZ�b�g
            End If

            Me.ButtonClose.Focus() '�t�H�[�J�X

        End If

    End Sub

    ''' <summary>
    ''' �n�Ճ��R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s���i�Q�Ə����p�j
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '��ʃR���g���[���ɐݒ�
        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kubun, False, True)
        objDrpTmp.SelectedValue = jr.Kbn
        Me.SpanKbn.InnerHtml = objDrpTmp.SelectedItem.Text '�敪
        Me.SpanBangou.InnerHtml = cl.GetDispStr(jr.HosyousyoNo) '�ԍ�

        '�t�ۏؖ���FLG
        If jr.FuhoSyoumeisyoFlg = "1" Then
            Me.SpanFuhoSyomeisyoFlg.InnerHtml = "�L��"
        ElseIf jr.FuhoSyoumeisyoFlg = "0" Then
            Me.SpanFuhoSyomeisyoFlg.InnerHtml = "����"
        Else
            Me.SpanFuhoSyomeisyoFlg.InnerHtml = ""
        End If

        '�t�ۏؖ���������
        Me.SpanFuhoSyomeisyoHassoDate.InnerHtml = cl.GetDispStr(jr.FuhoSyoumeisyoHassouDate)


    End Sub

    ''' <summary>
    ''' �ۏ؏��Ǘ����R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="sender">�n�Ճ��R�[�h</param>
    ''' <param name="e">�n�Ճ��R�[�h</param>
    ''' <param name="rec">�ۏ؏��Ǘ����R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs, ByRef rec As HosyousyoKanriRecord)

        Dim logic As New BukkenSintyokuJykyLogic

        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '���Y���f�[�^��DB���璊�o
        rec = logic.getSearchKeyDataRec(sender, pStrKbn, pStrBangou)

        '�ۏ؏��Ǘ��ǂݍ��݃f�[�^���Z�b�V�����ɑ��݂��Ȃ��ꍇ
        If rec.Kbn Is Nothing Or rec.HosyousyoNo Is Nothing Then
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            cl.CloseWindow(Me)
            Exit Sub
        End If

        '���_�~�[�R���{�ɃZ�b�g(��͊���)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.KAISEKI_KANRY, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.KaisekiKanry)
        Me.HiddenKaisekiKanry.Value = objDrpTmp.SelectedValue
        Me.SpanKaisekiKanry.InnerHtml = objDrpTmp.SelectedItem.Text

        '���_�~�[�R���{�ɃZ�b�g(�H���L��)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.KOJ_UMU, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.KojUmu)
        Me.HiddenKojUmu.Value = objDrpTmp.SelectedValue
        Me.SpanKojUmu.InnerHtml = objDrpTmp.SelectedItem.Text

        '���_�~�[�R���{�ɃZ�b�g(�H������)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.KOJ_KANRY, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.KojKanry)
        Me.HiddenKojKanry.Value = objDrpTmp.SelectedValue
        Me.SpanKojKanry.InnerHtml = objDrpTmp.SelectedItem.Text

        '���_�~�[�R���{�ɃZ�b�g(�����m�F����)
        objDrpTmp = New DropDownList
        helper.SetMeisyouDropDownList(objDrpTmp, EarthConst.emMeisyouType.NYUUKIN_KAKUNIN, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.NyuukinKakuninJyouken)
        Me.SpanNyuukinKakuninJyouken.InnerHtml = objDrpTmp.SelectedItem.Text

        '���_�~�[�R���{�ɃZ�b�g(������)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.NYUUKIN_JYKY, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.NyuukinJyky)
        Me.HiddenNyuukinJyky.Value = objDrpTmp.SelectedValue
        Me.SpanNyuukinJyky.InnerHtml = objDrpTmp.SelectedItem.Text

        '�@������=7�̏ꍇ�͓��t��\������
        If rec.NyuukinJyky = 7 Then
            '�����\������擾
            Dim strNyuukinYoteiDate As String
            strNyuukinYoteiDate = logic.setNyuukinYoteiDate(Me, rec.Kbn, rec.HosyousyoNo, rec.UpdDateTime)
            '�擾���������\�����\��
            Me.SpanNyuukinJyky.InnerHtml = Me.SpanNyuukinJyky.InnerHtml.Replace("MM/DD", strNyuukinYoteiDate)
        End If

        '���_�~�[�R���{�ɃZ�b�g(���r)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.KASI, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.Kasi)
        Me.HiddenKasi.Value = objDrpTmp.SelectedValue
        Me.SpanKasi.InnerHtml = objDrpTmp.SelectedItem.Text

        '�\���X�^�C���ݒ�
        Me.SetStringStyle()

        '��ʃ��[�h�ʐݒ�
        Me.SetGamenMode(rec)

    End Sub

    ''' <summary>
    ''' �i���f�[�^�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <param name="rec">�ۏ؏��Ǘ����R�[�h</param>
    ''' <param name="reportRec">�i���f�[�^���R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromReportIFDataRec(ByVal sender As Object, ByVal e As System.EventArgs, _
                                          ByVal jibanRec As JibanRecordBase, ByVal rec As HosyousyoKanriRecord, ByVal reportRec As ReportIfGetRecord)

        '���
        If reportRec.KaisekiKanryNaiyou <> String.Empty Then
            Me.SpanKaiseki.InnerHtml = reportRec.KaisekiKanryNaiyou
        End If

        '���ǍH��
        If reportRec.KojKanryHandan <> String.Empty Then
            Me.SpanKairyoKoji.InnerHtml = reportRec.KojKanryHandan
        End If

        '������
        If reportRec.NyuukinJykyHandan <> String.Empty Then
            Me.SpanNyuukin.InnerHtml = reportRec.NyuukinJykyHandan
        End If

        '���s�˗�
        If rec.BukkenJyky = 3 Then
            '�ۏ؏�T.������=3�̏ꍇ�A���s��
            Me.SpanHakkouIrai.InnerHtml = EarthConst.BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_ZUMI
        ElseIf cl.GetDisplayString(jibanRec.HosyousyoHakkouDate) <> Nothing Then
            '�n��T.�ۏ؏�������<>�󔒂̏ꍇ�A��t����
            Me.SpanHakkouIrai.InnerHtml = EarthConst.BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_KANRY
        ElseIf cl.GetDisplayString(IIf(jibanRec.HosyousyoHakIraisyoUmu = 1, "1", "")) = "1" OrElse _
               cl.GetDisplayString(reportRec.HakIraiTime) <> Nothing Then
            '�n��T.�ۏ؏����s�˗����L��=1 orelse �i��T.���s�˗�����<>�󔒂̏ꍇ�A���˗���
            Me.SpanHakkouIrai.InnerHtml = EarthConst.BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_IRAIZUMI
        Else
            '��
            Me.SpanHakkouIrai.InnerHtml = EarthConst.BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_MI
        End If

        '�H�����e
        If reportRec.KojKanryNaiyou <> String.Empty Then
            Me.SpanKojKanryNaiyou.InnerHtml = reportRec.KojKanryNaiyou
        End If

        '������
        If reportRec.NyuukinJykyNaiyou <> String.Empty Then
            Me.SpanNyuukinJykyNaiyou.InnerHtml = reportRec.NyuukinJykyNaiyou
        End If

    End Sub

#Region "�v���C�x�[�g���\�b�h"

    ''' <summary>
    ''' �ۏ؏��Ǘ��e�[�u������擾�����������ƂɁA
    ''' �\���X�^�C���𔒔����̐Ԏ������ɐݒ肷��
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetStringStyle()
        '��͊���
        If Me.HiddenKaisekiKanry.Value = "0" OrElse Me.HiddenKaisekiKanry.Value = "2" Then
            Me.SpanKaisekiKanry.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanKaisekiKanry.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            Me.CellKaisekiKanry.BgColor = EarthConst.STYLE_COLOR_WHITE
        End If
        '�H������
        If (Me.HiddenKojUmu.Value = "1" AndAlso Me.HiddenKojKanry.Value = "1") _
            OrElse (Me.HiddenKojUmu.Value = "0" AndAlso (Me.HiddenKojKanry.Value = "0" OrElse Me.HiddenKojKanry.Value = "1")) Then
        Else
            Me.SpanKojKanry.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanKojKanry.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            Me.CellKojKanry.BgColor = EarthConst.STYLE_COLOR_WHITE
        End If
        '������
        If Me.HiddenNyuukinJyky.Value = "2" OrElse Me.HiddenNyuukinJyky.Value = "4" _
            OrElse Me.HiddenNyuukinJyky.Value = "5" OrElse Me.HiddenNyuukinJyky.Value = "6" Then
            Me.SpanNyuukinJyky.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanNyuukinJyky.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            Me.CellNyuukinJyky.BgColor = EarthConst.STYLE_COLOR_WHITE
        End If
        '���r[��������]
        If Me.HiddenKasi.Value = "2" OrElse Me.HiddenKasi.Value = "3" OrElse Me.HiddenKasi.Value = "4" Then
            Me.SpanKasi.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanKasi.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            Me.CellKasi.BgColor = EarthConst.STYLE_COLOR_WHITE
        End If
    End Sub


    ''' <summary>
    ''' ��ʃ��[�h��ݒ肷��
    ''' </summary>
    ''' <param name="dataRec">�ۏ؏��Ǘ����R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetGamenMode(ByVal dataRec As HosyousyoKanriRecord)

        If cl.GetDisplayString(dataRec.SyoriFlg) = "0" Or cl.GetDisplayString(dataRec.SyoriFlg) = "1" Then
            pStrGamenMode = CStr(EarthEnum.emBukkenSintyokuJykyGamenMode.Nitiji)
        ElseIf cl.GetDisplayString(dataRec.SyoriFlg) = "2" Then
            pStrGamenMode = CStr(EarthEnum.emBukkenSintyokuJykyGamenMode.Getuji)
        End If

        'Hidden�ɑޔ�
        Me.HiddenGamenMode.Value = pStrGamenMode

        '��ʐݒ�
        Me.SetDispControl(dataRec)

    End Sub


    ''' <summary>
    ''' ��ʃ��[�h�ʂ̉�ʐݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispControl(ByVal dataRec As HosyousyoKanriRecord)
        'Hidden���Đݒ�
        pStrGamenMode = Me.HiddenGamenMode.Value

        If pStrGamenMode = CStr(EarthEnum.emBukkenSintyokuJykyGamenMode.Nitiji) Then '����
            '�ŏI��������(����)
            Me.SpanSyoriDateType.InnerHtml = "�ŏI��������(����) : "
        ElseIf pStrGamenMode = CStr(EarthEnum.emBukkenSintyokuJykyGamenMode.Getuji) Then '����
            '�ŏI��������(����)
            Me.SpanSyoriDateType.InnerHtml = "�ŏI��������(����) : "
        End If

        '�����������w��
        If cl.GetDisplayString(dataRec.SyoriDateTime) <> "" Then
            Me.TextSyoriDate.Text = cl.GetDisplayString(Format(dataRec.SyoriDateTime, "yyyy/MM/dd HH:mm"))
        Else
            Me.TextSyoriDate.Text = ""
        End If

    End Sub

#End Region

End Class