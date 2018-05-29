Imports Itis.Earth.BizLogic
Imports System.Transactions
Imports System.Data

Partial Public Class TyousaMitumorisyoSakuseiInquiry
    Inherits System.Web.UI.Page

    '�C���X�^���X����
    Private tyousaMitumorisyoSakuseiInquiryBC As New TyousaMitumorisyoSakuseiInquiryLogic
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "TyousaMitsumorisyo"
    Private Const APOST As Char = ","c
    Private headFlg As Boolean = False
    Private Const SEP_STRING As String = "$$$"

    Enum PDFStatus As Integer

        OK = 0                              '����
        IOException = 1                     '�G���[(���̃��[�U���t�@�C�����J���Ă���)
        UnauthorizedAccessException = 2     '�G���[(�t�@�C�����쐬����p�X���s��)
        NoData = 3                          '�Ώۂ̃f�[�^���擾�ł��܂���B

    End Enum

    ''' <summary>
    ''' �����\��
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 ���F(��A���V�X�e����) �V�K�쐬</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'MakeJavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), SEP_STRING)
                '�敪
                Me.lblKbn.Text = arrSearchTerm(0)
                '�����ԍ�
                Me.lblBukkenNo.Text = arrSearchTerm(1)
            End If

            '������ʕ\��
            Call SetGamenHyouji()

        End If

        '�u���Ϗ��쐬�v
        Me.btnMitumorisyoSakusei.Attributes.Add("onclick", "if(!fncHiltusuNyuryokuCheck()){return false;};")
        '�u�߂�v�{�^��
        Me.btnCloseWin.Attributes.Add("onclick", "window.close();")
        '�u�N���A�v�{�^��
        Me.clearWin.Attributes.Add("onclick", "fncClear();return false;")
    End Sub

    ''' <summary>
    ''' �u�����v�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToday.Click

        Me.tbxSakuseiDate.Text = TyousaMitumorisyoSakuseiInquiryBC.GetSysTime().ToString("yyyy/MM/dd")

    End Sub

    ''' <summary>
    ''' �u���Ϗ��I���v�{�^������������
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub btnMitumorisyoSakusei_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMitumorisyoSakusei.Click

        Dim user_info As New BizLogic.LoginUserInfo
        Dim ninsyou As New BizLogic.Ninsyou()
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        jBn.userAuth(user_info) '���[�U�[��{�F��

        If user_info Is Nothing Then
            Call ShowMessage(Messages.Instance.MSG2024E)
            Exit Sub
        Else
            ViewState("UserId") = user_info.LoginUserId '�S����ID
            ViewState("UserName") = Split(user_info.Name(), "(")(0) '�S���Җ�
        End If

        '�o�^��ID���S���ҕR�t�����F��Ǘ��}�X�^�̒S����ID�ɑ��݂��Ȃ��ꍇ
        Dim dtTantousya As Data.DataTable
        dtTantousya = TyousaMitumorisyoSakuseiInquiryBC.GetSonzaiHandan(ViewState("UserId").ToString)
        If dtTantousya.Rows(0).Item(0).ToString = "0" Then
            Call ShowMessage("�S���҈󂪑��݂��Ȃ����ߌ��ύ쐬�ł��܂���\r\n�Ǘ��҂ւ��A��������")
            Exit Sub
        End If

        '�������Ϗ��쐬�Ǘ��e�[�u���ɓo�^����
        Dim strKbn As String '�敪
        strKbn = Me.lblKbn.Text

        Dim strHosyousyoNo As String '�ۏ؏�NO
        strHosyousyoNo = Me.lblBukkenNo.Text

        Dim inSyouhizei As Integer '����őI��
        If rdoZeikomi.Checked = True Then
            inSyouhizei = 1 '�ō���
        Else
            inSyouhizei = 0 '�Ŕ���
        End If

        Dim inMooru As Integer '���[���W�J
        If rdoSuru.Checked = True Then
            inMooru = 1 '����
        Else
            inMooru = 0 '���Ȃ�
        End If

        Dim inHyoujiJyuusyo As Integer '�\���Z��_�Ǘ�No
        inHyoujiJyuusyo = Me.ddlHyoujiJyuusyo.SelectedValue

        Dim strSyouninSyaId As String '���F��ID
        strSyouninSyaId = Me.ddlSyouniSya.SelectedItem.Value

        Dim strSyouninSyaMei As String '���F�Җ�
        strSyouninSyaMei = Me.ddlSyouniSya.SelectedItem.Text

        Dim strSakuseiDate As String '�������Ϗ��쐬��
        strSakuseiDate = Convert.ToDateTime(Me.tbxSakuseiDate.Text.Trim).ToString("yyyy/MM/dd")
        Dim strIraiTantousyaMei As String '�������Ϗ�_�˗��S����
        strIraiTantousyaMei = Me.tbxIraiTantousya.Text.Trim

        'Dim dtOne As Data.DataTable
        'dtOne = TyousaMitumorisyoSakuseiInquiryBC.GetKihonInfoOne(Me.lblKbn.Text, _
        '                                                          Me.lblBukkenNo.Text)
        'Dim dtTwo As Data.DataTable
        'dtTwo = TyousaMitumorisyoSakuseiInquiryBC.GetKihonInfoTwo(Me.lblKbn.Text, _
        '                                                          Me.lblBukkenNo.Text)
        ''�Y���f�[�^���Ȃ��ꍇ
        'If (dtOne.Rows.Count = 0) OrElse (dtTwo.Rows.Count = 0) Then
        '    Call Me.ShowMessage(Messages.Instance.MSG020E)
        '    Return
        'End If

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            '���Ϗ��̑��݂𔻝Ђ���
            Dim dtMitumoriCnt As Data.DataTable
            dtMitumoriCnt = TyousaMitumorisyoSakuseiInquiryBC.GetMitumoriCount(Me.lblKbn.Text, _
                                                                               Me.lblBukkenNo.Text)
            '���ύ쐬��
            Dim inMitumori As Integer
            inMitumori = Convert.ToInt32(Me.lblMitumorisyoSakuseiKaisuu.Text) + 1
            If dtMitumoriCnt.Rows(0).Item(0).ToString = "1" Then
                '���݂���UPDATE_�񐔁A
                TyousaMitumorisyoSakuseiInquiryBC.GetUpdMitumoriKaisu(Me.lblKbn.Text, _
                                                                      Me.lblBukkenNo.Text, _
                                                                      inMitumori, _
                                                                      inSyouhizei, _
                                                                      inMooru, _
                                                                      inHyoujiJyuusyo, _
                                                                      ViewState("UserId").ToString, _
                                                                      ViewState("UserName").ToString, _
                                                                      strSyouninSyaId, _
                                                                      strSyouninSyaMei, _
                                                                      strSakuseiDate, _
                                                                      strIraiTantousyaMei)
            Else
                '���݂��Ȃ�INSERT
                TyousaMitumorisyoSakuseiInquiryBC.GetInsMitumoriKaisu(Me.lblKbn.Text, _
                                                                      Me.lblBukkenNo.Text, _
                                                                      inMitumori, _
                                                                      inSyouhizei, _
                                                                      inMooru, _
                                                                      inHyoujiJyuusyo, _
                                                                      ViewState("UserId").ToString, _
                                                                      ViewState("UserName").ToString, _
                                                                      strSyouninSyaId, _
                                                                      strSyouninSyaMei, _
                                                                      strSakuseiDate, _
                                                                      strIraiTantousyaMei)
            End If

            '���[�̃f�[�^���쐬����
            Dim strMessage As String
            strMessage = Me.CreateFcwTyouhyouData()
            If strMessage.Equals(String.Empty) Then

                '���Ϗ��쐬��
                Call SetMitumorisakuseiKisu()
                '�u���[���W�J�v�̃Z�b�g�𔻝Ђ���
                Call SetMoruHandan()

                scope.Complete()

            Else
                scope.Dispose()

                '���b�Z�[�W
                Context.Items("strFailureMsg") = strMessage
                Server.Transfer("CommonErr.aspx")
            End If
        End Using

    End Sub

    ''' <summary>
    ''' ������ʕ\�����Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub SetGamenHyouji()

        '���Ϗ��쐬��
        Call SetMitumorisakuseiKisu()
        '�\���Z�� �I��
        Call SetHyoujiJyuusyo()
        '���F�� �I��
        Call SetSyounisya()
        '�u���[���W�J�v�̃Z�b�g�𔻝Ђ���
        Call SetMoruHandan()
    End Sub

    ''' <summary>
    ''' ���Ϗ��쐬��
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub SetMitumorisakuseiKisu()

        '���Ϗ��̑��݂𔻝Ђ���
        Dim dtMitumoriCnt As Data.DataTable
        dtMitumoriCnt = TyousaMitumorisyoSakuseiInquiryBC.GetMitumoriCount(Me.lblKbn.Text, _
                                                                           Me.lblBukkenNo.Text)

        '���Ϗ��쐬��
        Dim dtMitumorisyoKaisuu As Data.DataTable
        dtMitumorisyoKaisuu = TyousaMitumorisyoSakuseiInquiryBC.GetSakuseiKaisuu(Me.lblKbn.Text, _
                                                                                 Me.lblBukkenNo.Text)
        '���Ϗ����쐬�������
        If dtMitumoriCnt.Rows(0).Item(0).ToString = "1" Then
            If dtMitumorisyoKaisuu.Rows.Count > 0 Then
                Me.lblMitumorisyoSakuseiKaisuu.Text = dtMitumorisyoKaisuu.Rows(0).Item(0).ToString
            End If
        Else
            Me.lblMitumorisyoSakuseiKaisuu.Text = "0"
        End If
    End Sub

    ''' <summary>
    ''' �u�\���Z�� �I���v���Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub SetHyoujiJyuusyo()

        '�u�\���Z�� �I���v���擾����
        Dim dtJyuusyo As Data.DataTable
        dtJyuusyo = TyousaMitumorisyoSakuseiInquiryBC.GetJyuusyoInfo()

        If dtJyuusyo.Rows.Count > 0 Then
            Me.ddlHyoujiJyuusyo.DataValueField = "kanri_no"
            Me.ddlHyoujiJyuusyo.DataTextField = "shiten_mei"
            Me.ddlHyoujiJyuusyo.DataSource = dtJyuusyo
            Me.ddlHyoujiJyuusyo.DataBind()
        End If

        '�擪�s�͋󗓂ɃZ�b�g����
        Me.ddlHyoujiJyuusyo.Items.Insert(0, New ListItem(String.Empty, "0"))
    End Sub

    ''' <summary>
    ''' �u���F�� �I���v���Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub SetSyounisya()

        '�u���F�ҁv���擾����
        Dim dtSyouninsya As Data.DataTable
        dtSyouninsya = TyousaMitumorisyoSakuseiInquiryBC.GetSyouninSyaInfo()

        If dtSyouninsya.Rows.Count > 0 Then
            Me.ddlSyouniSya.DataValueField = "syouninsya_id"
            Me.ddlSyouniSya.DataTextField = "syouninsya_mei"
            Me.ddlSyouniSya.DataSource = dtSyouninsya
            Me.ddlSyouniSya.DataBind()
        End If

        '�擪�s�͋󗓂ɃZ�b�g����
        Me.ddlSyouniSya.Items.Insert(0, New ListItem(String.Empty, "0"))
    End Sub

    ''' <summary>
    ''' �u���[���W�J�v�̃Z�b�g�𔻝Ђ���
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/12/02 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub SetMoruHandan()

        Me.hidMoru.Value = Me.rdoSuru.Enabled

        '�u���[���W�J�v�̃Z�b�g�𔻝Ђ���
        '�쐬��>"1"�@AND�@"����"��I�������
        Dim dtMoruHandan As Data.DataTable
        dtMoruHandan = TyousaMitumorisyoSakuseiInquiryBC.GetMoruHandan(Me.lblKbn.Text, Me.lblBukkenNo.Text)
        If dtMoruHandan.Rows.Count > 0 Then
            'dtMoruHandan.Rows(0).Item("mit_sakusei_kaisuu").ToString >= "1" AndAlso _
            If dtMoruHandan.Rows(0).Item("mooru_tenkai_flg").ToString = "1" Then
                Me.rdoSuru.Enabled = False
                Me.rdoSinai.Enabled = False

                Me.rdoSuru.Checked = True
                Me.rdoSinai.Checked = False
            Else
                Me.rdoSuru.Enabled = True
                Me.rdoSinai.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' ���[�̃f�[�^���쐬����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/18 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Function CreateFcwTyouhyouData() As String

        '�C���X�^���X�̐���
        Dim sb As New StringBuilder
        Dim editDt_One As New DataTable
        Dim editDt_Two As New DataTable
        Dim editDt_Three As New DataTable
        Dim editDR_One As Data.DataRow = editDt_One.NewRow
        Dim editDR_Two As Data.DataRow = editDt_Two.NewRow
        Dim editDR_Three As Data.DataRow = editDt_Three.NewRow
        Dim sb_T As New StringBuilder

        Dim errMsg As String = String.Empty
        Dim seikyusyoNo As String       '������NO

        seikyusyoNo = Request("seino")
        fcw = New FcwUtility(Page, ViewState("UserId"), kinouId, ".fcx")

        'add feild
        Call Me.AddFeild_One(editDt_One)
        'add feild
        Call Me.AddFeild_Two(editDt_Two)
        'add feild
        Call Me.AddFeild_Three(editDt_Three)

        If Me.headFlg Then
        Else
            '[Head] ���쐬
            sb.Append(fcw.CreateDatHeader(APOST.ToString))
            Me.headFlg = True
        End If

        '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/17��
        Dim strKbnZeinu As String = "1" '�Ŕ�
        Dim strZeirituZeinu As String = String.Empty  '�Ŕ�

        Dim strKbnZeikomi As String = "2" '�ō�
        Dim strZeirituZeikomi As String = String.Empty '�ō�

        Dim dtZeiritu As Data.DataTable
        If rdoZeikomi.Checked = True Then
            '�ō�
            dtZeiritu = TyousaMitumorisyoSakuseiInquiryBC.GetZeiritu(strKbnZeikomi)
            If dtZeiritu.Rows.Count > 0 Then
                strZeirituZeikomi = dtZeiritu.Rows(0).Item(0).ToString
            End If
        Else
            '�Ŕ�
            dtZeiritu = TyousaMitumorisyoSakuseiInquiryBC.GetZeiritu(strKbnZeinu)
            If dtZeiritu.Rows.Count > 0 Then
                strZeirituZeinu = dtZeiritu.Rows(0).Item(0).ToString
            End If
        End If
        '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/17��

        '"�ō�"
        Dim hyoudai As String = String.Empty
        Dim syoukei As String = String.Empty
        Dim syouhizei As String = String.Empty
        If rdoZeikomi.Checked = True Then
            hyoudai = "�䌩�ύ��v���z(�ō�)"
            '---------------(407662_����ő��őΉ�_Earth) ���F�C�� 2014/02/18��
            'syoukei = "��" & Space(7) & "�v"
            syoukei = "���v" & Space(1) & "(" & strZeirituZeikomi & ")"
            '---------------(407662_����ő��őΉ�_Earth) ���F�C�� 2014/02/18��
            syouhizei = Space(1)
            '"�Ŕ�"
        ElseIf rdoZeinu.Checked = True Then
            '2014.01.10 ���F�C��(��蔭���ꗗ�\(407646).xls��No.12)
            'hyoudai = "�䌩�ύ��v���z(�Ŕ�)"
            hyoudai = "�䌩�ύ��v���z(�ō�)"
            '2014.01.10 ���F�C��(��蔭���ꗗ�\(407646).xls��No.12)
            syoukei = Space(1) & "���v" & Space(1) & "(�Ŕ�)"
            '---------------(407662_����ő��őΉ�_Earth) ���F�C�� 2014/02/18��
            'syouhizei = Space(1) & "��" & Space(1) & "��" & Space(1) & "��"
            syouhizei = "�����" & Space(1) & "(" & strZeirituZeinu & ")"
            '---------------(407662_����ő��őΉ�_Earth) ���F�C�� 2014/02/18��
        End If

        '�S���ҕR�t�����F��Ǘ��}�X�^����y���F��z���擾����
        Dim strTantouIn As String
        Dim dtTantouIn As Data.DataTable
        dtTantouIn = TyousaMitumorisyoSakuseiInquiryBC.GetTantouIn(Me.lblKbn.Text, _
                                                                   Me.lblBukkenNo.Text)
        If dtTantouIn.Rows.Count > 0 Then
            strTantouIn = dtTantouIn.Rows(0).Item(0).ToString
        Else
            strTantouIn = String.Empty
        End If

        '���F�ҕR�t�����F��Ǘ��}�X�^����y���F��z���擾����
        Dim strSyouninIn As String
        Dim dtSyouninIn As Data.DataTable
        dtSyouninIn = TyousaMitumorisyoSakuseiInquiryBC.GetSyouninIn(Me.lblKbn.Text, _
                                                                     Me.lblBukkenNo.Text)
        If dtSyouninIn.Rows.Count > 0 Then
            strSyouninIn = dtSyouninIn.Rows(0).Item(0).ToString
        Else
            strSyouninIn = String.Empty
        End If

        '[Fixed Data Section]ONE
        Dim dtOne As Data.DataTable
        dtOne = TyousaMitumorisyoSakuseiInquiryBC.GetKihonInfoOne(Me.lblKbn.Text, _
                                                                  Me.lblBukkenNo.Text)

        '---------------(407662_����ő��őΉ�_Earth) ���F�폜 2014/03/25��
        'If dtOne.Rows.Count = 0 Then
        '    '�f�[�^���Ȃ��ꍇ�A�G���[
        '    errMsg = fcw.GetErrMsg(PDFStatus.NoData)
        '    If Not errMsg.Equals(String.Empty) Then
        '        Return errMsg
        '    End If
        'End If
        '---------------(407662_����ő��őΉ�_Earth) ���F�폜 2014/03/25��

        '[Fixed Data Section]Two
        Dim dtTwo As Data.DataTable
        dtTwo = TyousaMitumorisyoSakuseiInquiryBC.GetKihonInfoTwo(Me.lblKbn.Text, _
                                                                  Me.lblBukkenNo.Text)

        '---------------(407662_����ő��őΉ�_Earth) ���F�폜 2014/03/25��
        'If dtTwo.Rows.Count = 0 Then
        '    '�f�[�^���Ȃ��ꍇ�A�G���[
        '    errMsg = fcw.GetErrMsg(PDFStatus.NoData)
        '    If Not errMsg.Equals(String.Empty) Then
        '        Return errMsg
        '    End If
        'End If
        '---------------(407662_����ő��őΉ�_Earth) ���F�폜 2014/03/25��


        '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/18��
        'webConfig�ŁA�������擾����
        Dim strBungen1 As String = System.Configuration.ConfigurationManager.AppSettings("BunGen1").ToString
        Dim strBungen2 As String = System.Configuration.ConfigurationManager.AppSettings("BunGen2").ToString
        Dim strBungen3 As String = System.Configuration.ConfigurationManager.AppSettings("BunGen3").ToString
        '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/18��

        '---------------(407662_����ő��őΉ�_Earth) ���F�C�� 2014/03/25��
        '[Fixed Data Section]_editDt_One�ɒǉ�����
        editDR_One = editDt_One.NewRow
        editDR_Two = editDt_Two.NewRow

        If dtOne.Rows.Count > 0 Then
            Dim strHituke As String = String.Empty
            strHituke = Left(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 4) & "�N" & _
                        Mid(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 5, 2) & "��" & _
                        Right(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 2) & "��"  '�������Ϗ��쐬��

            editDR_One.Item("hituke") = strHituke '�������Ϗ��쐬��
            editDR_One.Item("yuubin_no") = dtOne.Rows(0).Item("yuubin_no").ToString '�X�֔ԍ�
            editDR_One.Item("jyuusyo_1") = dtOne.Rows(0).Item("jyuusyo1").ToString '�Z��1
            editDR_One.Item("jyuusyo_2") = dtOne.Rows(0).Item("jyuusyo2").ToString '�Z��2
            editDR_One.Item("tel_fax") = dtOne.Rows(0).Item("tel_fax").ToString 'Fax
            editDR_One.Item("sousinsya") = dtOne.Rows(0).Item("sousinsya").ToString '�������Ϗ��쐬��

            editDR_Two.Item("sakuseihi") = strHituke '�������Ϗ��쐬��
            editDR_Two.Item("yuubin_no") = dtOne.Rows(0).Item("yuubin_no").ToString '�X�֔ԍ�
            editDR_Two.Item("jyuusyo_1") = dtOne.Rows(0).Item("jyuusyo1").ToString '�Z��1
            editDR_Two.Item("jyuusyo_2") = dtOne.Rows(0).Item("jyuusyo2").ToString '�Z��2
            editDR_Two.Item("tel_fax") = dtOne.Rows(0).Item("tel_fax").ToString 'Fax

            '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
            Dim strHosoku As String = dtOne.Rows(0).Item("hosoku").ToString
            editDR_One.Item("hosoku") = strHosoku '�⑫
            editDR_Two.Item("hosoku") = strHosoku '�⑫
            '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
        Else
            editDR_One.Item("hituke") = "" '�������Ϗ��쐬��
            editDR_One.Item("yuubin_no") = "" '�X�֔ԍ�
            editDR_One.Item("jyuusyo_1") = "" '�Z��1
            editDR_One.Item("jyuusyo_2") = "" '�Z��2
            editDR_One.Item("tel_fax") = "" 'Fax
            editDR_One.Item("sousinsya") = "" '�������Ϗ��쐬��

            editDR_Two.Item("sakuseihi") = ""  '�������Ϗ��쐬��
            editDR_Two.Item("yuubin_no") = "" '�X�֔ԍ�
            editDR_Two.Item("jyuusyo_1") = "" '�Z��1
            editDR_Two.Item("jyuusyo_2") = "" '�Z��2
            editDR_Two.Item("tel_fax") = "" 'Fax

            '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
            editDR_One.Item("hosoku") = "" '�⑫
            editDR_Two.Item("hosoku") = "" '�⑫
            '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
        End If

        If dtTwo.Rows.Count > 0 Then

            editDR_One.Item("made") = dtTwo.Rows(0).Item("made").ToString '�����X��
            'editDR_One.Item("attn") = dtTwo.Rows(0).Item("attn").ToString '�˗��S����

            editDR_Two.Item("kameiten_mei") = dtTwo.Rows(0).Item("made").ToString & Space(2) & "�䒆" '�����X��
            editDR_Two.Item("bukken_no") = dtTwo.Rows(0).Item("bukken_no").ToString '�敪�{����No(=�ۏ؏�No)
            editDR_Two.Item("bukken_mei") = dtTwo.Rows(0).Item("bukken_mei").ToString & Space(2) & "�l�@" '�{�喼
            editDR_Two.Item("bukken_jyuusyo") = dtTwo.Rows(0).Item("bukken_jyuusyo").ToString '�����Z��1+�����Z��2+�����Z��3
            editDR_Two.Item("syouninsya") = strSyouninIn '���F�҈���擾����
            editDR_Two.Item("tantousya") = strTantouIn '�S���҈���擾����
            editDR_Two.Item("hyoudai") = hyoudai
            editDR_Two.Item("syoukeiMei") = syoukei
            editDR_Two.Item("syouhizeiMei") = syouhizei
            editDR_Two.Item("bungen1") = strBungen1
            editDR_Two.Item("bungen2") = strBungen2
            editDR_Two.Item("bungen3") = strBungen3

        Else
            editDR_One.Item("made") = "" '�����X��
            'editDR_One.Item("attn") = "" '�˗��S����

            editDR_Two.Item("kameiten_mei") = "" & "�䒆" '�����X��
            editDR_Two.Item("bukken_no") = "" '�敪�{����No(=�ۏ؏�No)
            editDR_Two.Item("bukken_mei") = "" '�{�喼
            editDR_Two.Item("bukken_jyuusyo") = "" '�����Z��1+�����Z��2+�����Z��3
            editDR_Two.Item("syouninsya") = strSyouninIn '���F�҈���擾����
            editDR_Two.Item("tantousya") = strTantouIn '�S���҈���擾����
            editDR_Two.Item("hyoudai") = hyoudai
            editDR_Two.Item("syoukeiMei") = syoukei
            editDR_Two.Item("syouhizeiMei") = syouhizei
            editDR_Two.Item("bungen1") = strBungen1
            editDR_Two.Item("bungen2") = strBungen2
            editDR_Two.Item("bungen3") = strBungen3

        End If

        '�˗��S����
        Dim dtIraiTantousya As Data.DataTable
        dtIraiTantousya = tyousaMitumorisyoSakuseiInquiryBC.GetIraiTantousya(Me.lblKbn.Text, Me.lblBukkenNo.Text)

        If (dtIraiTantousya.Rows.Count > 0) AndAlso (Not dtIraiTantousya.Rows(0).Item("tys_mit_irai_tantousya_mei").ToString.Trim.Equals(String.Empty)) Then
            editDR_One.Item("attn") = dtIraiTantousya.Rows(0).Item("tys_mit_irai_tantousya_mei").ToString.Trim
        Else
            If (dtTwo.Rows.Count > 0) AndAlso (Not dtTwo.Rows(0).Item("attn").ToString.Trim.Equals(String.Empty)) Then
                editDR_One.Item("attn") = dtTwo.Rows(0).Item("attn").ToString.Trim
            Else
                editDR_One.Item("attn") = "��S����"
            End If
        End If

        '�s��ǉ�����
        editDt_One.Rows.Add(editDR_One)
        editDt_Two.Rows.Add(editDR_Two)


        ''[Fixed Data Section]_editDt_One�ɒǉ�����
        'If dtOne.Rows.Count > 0 AndAlso dtTwo.Rows.Count > 0 Then
        '    editDR_One = editDt_One.NewRow
        '    editDR_One.Item("hituke") = dtOne.Rows(0).Item("hituke") '�������Ϗ��쐬��
        '    editDR_One.Item("made") = dtTwo.Rows(0).Item("made").ToString '�����X��
        '    editDR_One.Item("attn") = dtTwo.Rows(0).Item("attn").ToString '�˗��S����
        '    editDR_One.Item("yuubin_no") = dtOne.Rows(0).Item("yuubin_no").ToString '�X�֔ԍ�
        '    editDR_One.Item("jyuusyo_1") = dtOne.Rows(0).Item("jyuusyo1").ToString '�Z��1
        '    editDR_One.Item("jyuusyo_2") = dtOne.Rows(0).Item("jyuusyo2").ToString '�Z��2
        '    editDR_One.Item("tel_fax") = dtOne.Rows(0).Item("tel_fax").ToString 'Fax
        '    editDR_One.Item("sousinsya") = dtOne.Rows(0).Item("sousinsya").ToString '�������Ϗ��쐬��

        '    editDt_One.Rows.Add(editDR_One)
        'End If

        ''[Fixed Data Section]_editDt_Two�ɒǉ�����
        'If dtOne.Rows.Count > 0 AndAlso dtTwo.Rows.Count > 0 Then
        '    editDR_Two = editDt_Two.NewRow
        '    editDR_Two.Item("sakuseihi") = Left(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 4) & "�N" & _
        '                                   Mid(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 5, 2) & "��" & _
        '                                   Right(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 2) & "��"  '�������Ϗ��쐬��
        '    editDR_Two.Item("kameiten_mei") = dtTwo.Rows(0).Item("made").ToString & Space(2) & "�䒆" '�����X��
        '    editDR_Two.Item("bukken_no") = dtTwo.Rows(0).Item("bukken_no").ToString '�敪�{����No(=�ۏ؏�No)
        '    editDR_Two.Item("bukken_mei") = dtTwo.Rows(0).Item("bukken_mei").ToString '�{�喼
        '    editDR_Two.Item("bukken_jyuusyo") = dtTwo.Rows(0).Item("bukken_jyuusyo").ToString '�����Z��1+�����Z��2+�����Z��3
        '    editDR_Two.Item("yuubin_no") = dtOne.Rows(0).Item("yuubin_no").ToString '�X�֔ԍ�
        '    editDR_Two.Item("jyuusyo_1") = dtOne.Rows(0).Item("jyuusyo1").ToString '�Z��1
        '    editDR_Two.Item("jyuusyo_2") = dtOne.Rows(0).Item("jyuusyo2").ToString '�Z��2
        '    editDR_Two.Item("tel_fax") = dtOne.Rows(0).Item("tel_fax").ToString 'Fax
        '    editDR_Two.Item("syouninsya") = strSyouninIn '���F�҈���擾����
        '    editDR_Two.Item("tantousya") = strTantouIn '�S���҈���擾����
        '    editDR_Two.Item("hyoudai") = hyoudai
        '    editDR_Two.Item("syoukeiMei") = syoukei
        '    editDR_Two.Item("syouhizeiMei") = syouhizei

        '    '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/18��
        '    editDR_Two.Item("bungen1") = strBungen1
        '    editDR_Two.Item("bungen2") = strBungen2
        '    editDR_Two.Item("bungen3") = strBungen3
        '    '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/18��

        '    editDt_Two.Rows.Add(editDR_Two)
        'End If
        '---------------(407662_����ő��őΉ�_Earth) ���F�C�� 2014/03/25��

        '�䌩�Ϗ��̃f�[�^
        Dim dtTyouhyouDate As Data.DataTable
        '"�ō�"
        If rdoZeikomi.Checked = True Then
            dtTyouhyouDate = TyousaMitumorisyoSakuseiInquiryBC.GetTyouhyouDate(Me.lblKbn.Text, _
                                                                               Me.lblBukkenNo.Text, _
                                                                               "�ō�")
            '"�Ŕ�"
        Else
            dtTyouhyouDate = TyousaMitumorisyoSakuseiInquiryBC.GetTyouhyouDate(Me.lblKbn.Text, _
                                                                               Me.lblBukkenNo.Text, _
                                                                               "�Ŕ�")
        End If

        '---------------(407662_����ő��őΉ�_Earth) ���F�폜 2014/03/25��
        'If dtTyouhyouDate.Rows.Count = 0 Then
        '    '�f�[�^���Ȃ��ꍇ�A�G���[
        '    errMsg = fcw.GetErrMsg(PDFStatus.NoData)
        '    If Not errMsg.Equals(String.Empty) Then
        '        Return errMsg
        '    End If
        'End If
        '---------------(407662_����ő��őΉ�_Earth) ���F�폜 2014/03/25��

        If dtTyouhyouDate.Rows.Count > 0 Then
            For i As Integer = 0 To dtTyouhyouDate.Rows.Count - 1
                editDR_Three = editDt_Three.NewRow
                editDR_Three.Item("syouhin_mei") = dtTyouhyouDate.Rows(i).Item("syouhin_mei") '���i��
                editDR_Three.Item("suuryou") = dtTyouhyouDate.Rows(i).Item("suuryou").ToString '����
                editDR_Three.Item("tanka") = dtTyouhyouDate.Rows(i).Item("tanka").ToString '�P��
                editDR_Three.Item("kingaku") = dtTyouhyouDate.Rows(i).Item("kingaku").ToString '���z
                editDR_Three.Item("bikou") = dtTyouhyouDate.Rows(i).Item("bikou").ToString '���l
                editDR_Three.Item("syouhizei") = dtTyouhyouDate.Rows(i).Item("syouhizei").ToString '�����
                editDt_Three.Rows.Add(editDR_Three)
            Next

        Else
            '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/03/25��
            editDR_Three = editDt_Three.NewRow
            editDR_Three.Item("syouhin_mei") = "" '���i��
            editDR_Three.Item("suuryou") = "" '����
            editDR_Three.Item("tanka") = "" '�P��
            editDR_Three.Item("kingaku") = "" '���z
            editDR_Three.Item("bikou") = "" '���l
            editDR_Three.Item("syouhizei") = "" '�����
            editDt_Three.Rows.Add(editDR_Three)
            '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/03/25��
        End If

        '��T�y�[�W
        sb.Append(vbCrLf)
        '[Form] ���쐬
        sb.Append(fcw.CreateFormSection("PAGE=PageOne"))
        '[FixedDataSection] ���쐬
        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_One(editDt_One)))

        '��U�y�[�W
        sb.Append(vbCrLf)
        '[Form] ���쐬
        sb.Append(fcw.CreateFormSection("PAGE=PageTwo"))
        '[FixedDataSection] ���쐬
        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_Two(editDt_Two)))
        '[TableDataSection](���쐬)
        sb.Append(fcw.CreateTableDataSection(GetTableDataSection(editDt_Three)))

        'DAT�t�@�C���쐬
        errMsg = fcw.GetErrMsg(fcw.WriteData(sb.ToString))

        ' �����挳�����[�f�[�^PDF�@�o��
        If Not errMsg.Equals(String.Empty) Then
            '�G���[������ꍇ
            Return errMsg
        Else
            '�G���[���Ȃ��ꍇ�A���[��OPEN
            Call Me.PopupFcw(fcw.GetUrl(Me.lblKbn.Text, _
                                        Me.lblBukkenNo.Text, _
                                        (Convert.ToInt32(Me.lblMitumorisyoSakuseiKaisuu.Text) + 1).ToString))

            Return String.Empty
        End If

    End Function

    ''' <summary>
    ''' ��T�y�[�WFixedDataSection
    ''' </summary>
    ''' <param name="editDt_One"></param>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub AddFeild_One(ByRef editDt_One As Data.DataTable)

        '[FIXED FEILD]
        editDt_One.Columns.Add("hituke", GetType(String)) '�������Ϗ��쐬��
        editDt_One.Columns.Add("made", GetType(String)) '�����X��
        editDt_One.Columns.Add("attn", GetType(String)) '�˗��S����
        editDt_One.Columns.Add("yuubin_no", GetType(String)) '�X�֔ԍ�
        editDt_One.Columns.Add("jyuusyo_1", GetType(String)) '�Z��1
        editDt_One.Columns.Add("jyuusyo_2", GetType(String)) '�Z��2
        editDt_One.Columns.Add("tel_fax", GetType(String)) 'Fax
        editDt_One.Columns.Add("sousinsya", GetType(String)) '�������Ϗ��쐬��

        '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
        editDt_One.Columns.Add("hosoku", GetType(String)) '�⑫
        '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================

    End Sub

    ''' <summary>
    ''' ��T�y�[�WGetFixedDataSection�̃f�[�^���擾
    ''' </summary>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/18 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Function GetFixedDataSection_One(ByVal data As DataTable) As String

        '�f�[�^���擾
        Return fcw.GetFixedDataSection( _
                                        "hituke" & _
                                        ",made" & _
                                        ",attn" & _
                                        ",yuubin_no" & _
                                        ",jyuusyo_1" & _
                                        ",jyuusyo_2" & _
                                        ",tel_fax" & _
                                        ",sousinsya" & _
                                        ",hosoku", data)

    End Function

    ''' <summary>
    ''' ��U�y�[�WFixedDataSection
    ''' </summary>
    ''' <param name="editDt_Two"></param>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub AddFeild_Two(ByRef editDt_Two As Data.DataTable)

        '[FIXED FEILD]
        editDt_Two.Columns.Add("sakuseihi", GetType(String)) '�������Ϗ��쐬��
        editDt_Two.Columns.Add("kameiten_mei", GetType(String)) '�����X��
        editDt_Two.Columns.Add("bukken_no", GetType(String)) '�敪�{����No(=�ۏ؏�No)
        editDt_Two.Columns.Add("bukken_mei", GetType(String)) '�{�喼
        editDt_Two.Columns.Add("bukken_jyuusyo", GetType(String)) '�����Z��1+�����Z��2+�����Z��3
        editDt_Two.Columns.Add("yuubin_no", GetType(String)) '�X�֔ԍ�
        editDt_Two.Columns.Add("jyuusyo_1", GetType(String)) '�Z��1
        editDt_Two.Columns.Add("jyuusyo_2", GetType(String)) '�Z��2
        editDt_Two.Columns.Add("tel_fax", GetType(String)) 'FAX
        editDt_Two.Columns.Add("syouninsya", GetType(String)) '���F�҈�
        editDt_Two.Columns.Add("tantousya", GetType(String)) '�S���҈�
        editDt_Two.Columns.Add("hyoudai", GetType(String)) '�\��
        editDt_Two.Columns.Add("syoukeiMei", GetType(String)) '���v
        editDt_Two.Columns.Add("syouhizeiMei", GetType(String)) '�����
        '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/18��
        editDt_Two.Columns.Add("bungen1", GetType(String)) '����
        editDt_Two.Columns.Add("bungen2", GetType(String)) '����
        editDt_Two.Columns.Add("bungen3", GetType(String)) '����
        '---------------(407662_����ő��őΉ�_Earth) ���F�ǉ� 2014/02/18��

        '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================
        editDt_Two.Columns.Add("hosoku", GetType(String)) '�⑫
        '==================2015/09/17 �Č��u430011�v�̑Ή� �ǉ���====================================

    End Sub

    ''' <summary>
    ''' ��U�y�[�WGetFixedDataSection�̃f�[�^���擾
    ''' </summary>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/18 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Function GetFixedDataSection_Two(ByVal data As DataTable) As String

        '�f�[�^���擾
        Return fcw.GetFixedDataSection( _
                                        "sakuseihi" & _
                                        ",kameiten_mei" & _
                                        ",bukken_no" & _
                                        ",bukken_mei" & _
                                        ",bukken_jyuusyo" & _
                                        ",yuubin_no" & _
                                        ",jyuusyo_1" & _
                                        ",jyuusyo_2" & _
                                        ",tel_fax" & _
                                        ",syouninsya" & _
                                        ",tantousya" & _
                                        ",hyoudai" & _
                                        ",syoukeiMei" & _
                                        ",syouhizeiMei" & _
                                        ",bungen1" & _
                                        ",bungen2" & _
                                        ",bungen3" & _
                                        ",hosoku", data)

    End Function

    ''' <summary>
    ''' ��U�y�[�W[TableDataSection]
    ''' </summary>
    ''' <param name="editDt_Three"></param>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub AddFeild_Three(ByRef editDt_Three As Data.DataTable)

        '[FIXED FEILD]
        editDt_Three.Columns.Add("syouhin_mei", GetType(String)) '���i��
        editDt_Three.Columns.Add("suuryou", GetType(String)) '����
        editDt_Three.Columns.Add("tanka", GetType(String)) '�P��
        editDt_Three.Columns.Add("kingaku", GetType(String)) '���z
        editDt_Three.Columns.Add("bikou", GetType(String)) '���l
        editDt_Three.Columns.Add("syouhizei", GetType(String)) '�����

    End Sub

    ''' <summary>
    ''' ��U�y�[�WGetTableDataSection�̃f�[�^���擾
    ''' </summary>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/18 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Function GetTableDataSection(ByVal data As DataTable) As String

        '����CLASS
        Dim earthAction As New EarthAction

        '�f�[�^���擾
        Return earthAction.JoinDataTable(data, _
                                         APOST, _
                                         "syouhin_mei" & _
                                         ",suuryou" & _
                                         ",tanka" & _
                                         ",kingaku" & _
                                         ",bikou" & _
                                         ",syouhizei")

    End Function

    ''' <summary>
    ''' MakeJavaScript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub MakeJavaScript()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function fncHiltusuNyuryokuCheck()")
            .AppendLine("{")
            '�u���Ϗ��쐬���v
            .AppendLine("   var tbxDate = document.getElementById('" & Me.tbxSakuseiDate.ClientID & "');")
            '   �K�{�`�F�b�N
            .AppendLine("   if(tbxDate.value.Trim() == '')")
            .AppendLine("       {")
            .AppendLine("           alert('�u���Ϗ��쐬���v�͕K�{�ł��B\r\n');")
            .AppendLine("           tbxDate.focus();")
            .AppendLine("           tbxDate.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '   ���t�`�F�b�N
            .AppendLine("   if(!chkDate(tbxDate.value.Trim()))")
            .AppendLine("       {")
            .AppendLine("           alert('" & Messages.Instance.MSG2017E & "');")
            .AppendLine("           tbxDate.focus();")
            .AppendLine("           tbxDate.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�u�˗��S���� ���́v
            .AppendLine("   var tbxIraiTantousya = document.getElementById('" & Me.tbxIraiTantousya.ClientID & "');")
            '   �����`�F�b�N
            .AppendLine("   if(!chkSiteinaiByte(tbxIraiTantousya.value.Trim(),20))")
            .AppendLine("       {")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2002E, "�u�˗��S���ҁv", 10) & "');")
            .AppendLine("           tbxIraiTantousya.focus();")
            .AppendLine("           tbxIraiTantousya.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")


            .AppendLine("   var ddlHyoujiJyuusyo = document.getElementById('" & Me.ddlHyoujiJyuusyo.ClientID & "'); ")
            .AppendLine("   var ddlSyouniSya = document.getElementById('" & Me.ddlSyouniSya.ClientID & "'); ")
            '�u�\���Z�� �I���v_�I��K�{
            .AppendLine("   if(ddlHyoujiJyuusyo.value == '0')")
            .AppendLine("       {")
            .AppendLine("           alert('�u�\���Z�� �I���v�͕K�{�ł�\r\n�\���Z����I�����Ă�������');")
            .AppendLine("           ddlHyoujiJyuusyo.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�u���F�� �I���v_�I��K�{
            .AppendLine("   if(ddlSyouniSya.value == '0')")
            .AppendLine("       {")
            .AppendLine("           alert('�u���F�� �I���v�͕K�{�ł�\r\n���F�҂�I�����Ă�������');")
            .AppendLine("           ddlSyouniSya.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("   return true;")
            .AppendLine("}")

            '�u�N���A�v�{�^��
            .AppendLine("function fncClear()")
            .AppendLine("{")
            '�\���Z��
            .AppendLine("   var ddlHyoujiJyuusyo = document.getElementById('" & Me.ddlHyoujiJyuusyo.ClientID & "'); ")
            '���F��
            .AppendLine("   var ddlSyouniSya = document.getElementById('" & Me.ddlSyouniSya.ClientID & "'); ")
            '���[���W�J �I��
            .AppendLine("   var rdoSuru = document.getElementById('" & Me.rdoSuru.ClientID & "'); ")
            '����ŕ\�� �I��
            .AppendLine("   var rdoZeinu = document.getElementById('" & Me.rdoZeinu.ClientID & "'); ")
            '���Ϗ��쐬��
            .AppendLine("   var tbxSakuseiDate = document.getElementById('" & Me.tbxSakuseiDate.ClientID & "'); ")
            '�˗��S����
            .AppendLine("   var tbxIraiTantousya = document.getElementById('" & Me.tbxIraiTantousya.ClientID & "'); ")

            .AppendLine("   ddlHyoujiJyuusyo.value = '0';")
            .AppendLine("   ddlSyouniSya.value = '0';")
            .AppendLine("   rdoSuru.checked = true;")
            .AppendLine("   rdoZeinu.checked = true;")
            .AppendLine("   tbxSakuseiDate.value = '';")
            .AppendLine("   tbxIraiTantousya.value = '';")
            .AppendLine("}")
            .AppendLine("</script>")

        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' ���[��Open
    ''' </summary>
    ''' <param name="strUrl">���[��URL</param>
    ''' <remarks></remarks>
    ''' <history>2013/11/28 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub PopupFcw(ByVal strUrl As String)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "tyouhyouOpenScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function window.onload(){")
            '���[���W�J
            .AppendLine("   var moru = document.getElementById('" & Me.rdoSuru.ClientID & "').checked;")
            .AppendLine("   var moruEnable = document.getElementById('" & Me.hidMoru.ClientID & "').value;")
            '�敪
            .AppendLine("   var kbn = document.getElementById('" & Me.lblKbn.ClientID & "').innerText;")
            '�ۏ؏�NO
            .AppendLine("   var bukkenNo = document.getElementById('" & Me.lblBukkenNo.ClientID & "').innerText;")
            '�쐬��
            .AppendLine("   var mitumorisyoSakuseiKaisuu = document.getElementById('" & Me.lblMitumorisyoSakuseiKaisuu.ClientID & "').innerText;")
            .AppendLine("   window.open('tyouhyouOpen.aspx?fcwUrl=' + escape('" & strUrl & "') + '$$$' + escape(moru) + '$$$' + escape(moruEnable) + '$$$' + escape(kbn) + '$$$' + escape(bukkenNo) + '$$$' + escape(mitumorisyoSakuseiKaisuu),'PopupFcw');")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' ShowMessage
    ''' </summary>
    ''' <param name="strMessage">���b�Z�[�W���e</param>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 ���F(��A���V�X�e����) �V�K�쐬</history>
    Private Sub ShowMessage(ByVal strMessage As String)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "ShowMessage"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function window.onload(){")
            .AppendLine("   alert('" & strMessage & "');")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub
End Class