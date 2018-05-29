Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class kakakuseikyuJyouhou
    Inherits System.Web.UI.UserControl

    Private KihonJyouhouBL As New KakakuseikyuJyouhouLogic()
    Private CommonMsgAndFocusBL As New kihonjyouhou.MessageAndFocus()
    Private CommonCheckFuc As New CommonCheck()
    Private CommonLG As New CommonLogic()
    Private _kameiten_cd As String
    Private _seisiki_nm As String
    Private _kameiten_nm1 As String

    Private _kyoutu_kbn As String
    Private _eigyousyo_cd As String

    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property kyoutu_kbn() As String

        Get
            Return _kyoutu_kbn
        End Get
        Set(ByVal value As String)
            _kyoutu_kbn = value
        End Set

    End Property

    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property eigyousyo_cd() As String
        Get
            Return _eigyousyo_cd
        End Get
        Set(ByVal value As String)
            _eigyousyo_cd = value
        End Set
    End Property

    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property seisiki_nm() As String
        Get
            Return _seisiki_nm
        End Get
        Set(ByVal value As String)
            _seisiki_nm = value
        End Set
    End Property

    ''' <summary>
    ''' �����X���P
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property kameiten_nm1() As String
        Get
            Return _kameiten_nm1
        End Get
        Set(ByVal value As String)
            _kameiten_nm1 = value
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

    Private _upd_login_user_id As String
    ''' <summary>
    ''' �X�V��ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Upd_login_user_id() As String
        Get
            Return _upd_login_user_id
        End Get

        Set(ByVal value As String)
            _upd_login_user_id = value
        End Set

    End Property

    Private _kenngenn As Boolean
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get

        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set

    End Property

    ''' <summary>
    ''' ��ʃ��[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            '�����`�F�b�N
            Call kengenCheck()
            '�^�C�g���@�����N�ݒ�
            Call titleTextRefSet()
            '�e��ʂ���l���擾
            Call GetValueByMainPage()
            '��ʍ��ڕ\���ݒ�
            Call gameihHyouji("Page_Load")

            Call tbxEventSettings()

        End If

        ''confrim�I�����ʂ��̔��f
        'Call reload_confrim()

        '��ʕ\�����ڂ̃v���v�e�ݒ�
        Call PageItemAddAttributes()

        'javaScript�ݒ�
        Call MakeJavaScript()

    End Sub

    ''' <summary>
    ''' �^�C�g���@�����N�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub titleTextRefSet()

        Me.titleText_kakaku.HRef = "javascript:changeDisplay('" & Me.meisaiTbody_kakaku.ClientID & "');changeDisplay('" & Me.titleInfobarKakaku.ClientID & "');"

    End Sub

    ''' <summary>
    ''' �e��ʂ���l���擾
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub GetValueByMainPage()

        ViewState.Item("kameiten_cd_touroku") = _kameiten_cd
        ViewState.Item("kameiten_cd") = _kameiten_cd
        ViewState.Item("user_id") = _upd_login_user_id

    End Sub

    ''' <summary>
    ''' �����`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub kengenCheck()

        Me.btnTouroku.Enabled = _kenngenn

    End Sub

    ''' <summary>
    ''' ��ʍ��ڃv���v�e�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub PageItemAddAttributes()
        '==================2011/05/11 �ԗ� �����������\���ύX �C�� �J�n��==========================
        'Me.btnKensaku1.Attributes.Add("onclick", "fncOpenwindowkakaku('" & tbxSyouhinCd1.ClientID & "','" & tbxSyouhinNm1.ClientID & "');return false;")
        'Me.btnKensaku2.Attributes.Add("onclick", "fncOpenwindowkakaku('" & tbxSyouhinCd2.ClientID & "','" & tbxSyouhinNm2.ClientID & "');return false;")
        'Me.btnKensaku3.Attributes.Add("onclick", "fncOpenwindowkakaku('" & tbxSyouhinCd3.ClientID & "','" & tbxSyouhinNm3.ClientID & "');return false;")
        Me.btnKensaku1.Attributes.Add("onclick", "fncOpenwindowkakaku('" & tbxSyouhinCd1.ClientID & "','" & tbxSyouhinNm1.ClientID & "','" & Me.tbxMoney1.ClientID & "');return false;")
        Me.btnKensaku2.Attributes.Add("onclick", "fncOpenwindowkakaku('" & tbxSyouhinCd2.ClientID & "','" & tbxSyouhinNm2.ClientID & "','" & Me.tbxMoney2.ClientID & "');return false;")
        Me.btnKensaku3.Attributes.Add("onclick", "fncOpenwindowkakaku('" & tbxSyouhinCd3.ClientID & "','" & tbxSyouhinNm3.ClientID & "','" & Me.tbxMoney3.ClientID & "');return false;")
        '==================2011/05/11 �ԗ� �����������\���ύX �C�� �I����==========================

        Me.tbxSyouhinNm1.Attributes.Add("readonly", "true")
        Me.tbxSyouhinNm2.Attributes.Add("readonly", "true")
        Me.tbxSyouhinNm3.Attributes.Add("readonly", "true")

        '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
        Me.tbxMoney1.Attributes.Add("readonly", "true")
        Me.tbxMoney2.Attributes.Add("readonly", "true")
        Me.tbxMoney3.Attributes.Add("readonly", "true")
        '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================


        ':) 100606 ������@�ǉ��@��

        Me.lblSeikyusaki_name_Hansoku.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_name_Kouj.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_name_Tys.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_name_Tatemono.Attributes.Add("readonly", "true")

        Me.lblSeikyusaki_simebi_Hansoku.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_simebi_Kouj.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_simebi_Tatemono.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_simebi_Tys.Attributes.Add("readonly", "true")

        ' ������@�ǉ��@������������

        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        Me.lblSeikyusaki_name_Sk5.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_name_Sk6.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_name_Sk7.Attributes.Add("readonly", "true")

        Me.lblSeikyusaki_simebi_Sk5.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_simebi_Sk6.Attributes.Add("readonly", "true")
        Me.lblSeikyusaki_simebi_Sk7.Attributes.Add("readonly", "true")
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================


        '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��
        'Me.tbxSSkk.Attributes.Add("onblur", "checkNumberAddFig(this);")
        'Me.tbxSSGRkk.Attributes.Add("onblur", "checkNumberAddFig(this);")
        'Me.tbxKaiseki.Attributes.Add("onblur", "checkNumberAddFig(this);")
        Me.tbxKaiyaku.Attributes.Add("onblur", "checkNumberAddFig(this);")
        '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��
        Me.tbxKaisekiHosyouKkk.Attributes.Add("onblur", "checkNumberAddFig(this);")
        Me.tbxSsgrKkk.Attributes.Add("onblur", "checkNumberAddFig(this);")
        '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��

        'Me.tbxSaitys.Attributes.Add("onblur", "checkNumberAddFig(this);")
        'Me.tbxHousyomu.Attributes.Add("onblur", "checkNumberAddFig(this);")
        'Me.tbxJizentys.Attributes.Add("onblur", "checkNumberAddFig(this);")

        'Me.tbxSSkk.Attributes.Add("onfocus", "removeFig(this);")
        'Me.tbxSSGRkk.Attributes.Add("onfocus", "removeFig(this);")
        'Me.tbxKaiseki.Attributes.Add("onfocus", "removeFig(this);")
        Me.tbxKaiyaku.Attributes.Add("onfocus", "removeFig(this);")
        '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��
        Me.tbxKaisekiHosyouKkk.Attributes.Add("onfocus", "removeFig(this);")
        Me.tbxSsgrKkk.Attributes.Add("onfocus", "removeFig(this);")
        '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��
        'Me.tbxSaitys.Attributes.Add("onfocus", "removeFig(this);")
        'Me.tbxHousyomu.Attributes.Add("onfocus", "removeFig(this);")
        'Me.tbxJizentys.Attributes.Add("onfocus", "removeFig(this);")
        '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��

        Me.tbxKameitenCd.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxSyouhinCd1.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxSyouhinCd2.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxSyouhinCd3.Attributes.Add("onBlur", "fncToUpper(this);")

    End Sub

    ''' <summary>
    ''' javaScript Registe
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub MakeJavaScript()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            '.Append("function fncOpenwindowkakaku(cd,nm)" & vbCrLf)
            .Append("function fncOpenwindowkakaku(cd,nm,objHyoujunkaKakaku)" & vbCrLf)
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
            .Append("{" & vbCrLf)
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            '.Append("var strkbn='���i';" & vbCrLf)
            .Append("var strkbn='���i���i';" & vbCrLf)
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
            '���i
            .Append("var strCd=objEBI(cd).value;" & vbCrLf)
            .Append("if (strCd == '00000'){strCd = ''} ;" & vbCrLf)
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            '.Append("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Parent.Page.Form.Name & "&objCd=' + cd + '&objMei=' + nm + '&strCd='+escape(strCd)+'&strMei='+escape(objEBI(nm).value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .Append("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Parent.Page.Form.Name & "&objCd=' + cd + '&objMei=' + nm + '&strCd='+escape(strCd)+'&strMei='+escape(objEBI(nm).value)+'&objHyoujunkaKakaku='+objHyoujunkaKakaku, 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
            .Append("}" & vbCrLf)
            ':) 100606 ------������@�@�ǉ� ��������������������������������������������������������������
            .Append("function SKU_CHANGE_HAN(){" & vbCrLf)
            .Append(" var totalHanNew;" & vbCrLf)
            .Append(" var totalHanOld;" & vbCrLf)
            .Append(" totalHanNew = objEBI('" & Me.ddlSeikyusakiKbn_Hansoku.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiCode_Hansoku.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiEdaban_Hansoku.ClientID & "').value;" & vbCrLf)
            .Append(" totalHanOld = objEBI('" & Me.hid_kbn_HAN.ClientID & "').value+'|'+objEBI('" & Me.hid_cd_HAN.ClientID & "').value+'|'+objEBI('" & Me.hid_brc_HAN.ClientID & "').value;" & vbCrLf)
            '.Append("alert(totalHanNew +'����'+ totalHanOld);" & vbCrLf)
            .Append("if(totalHanNew == totalHanOld){objEBI('" & Me.hid_kakunin_HAN.ClientID & "').value = '';}else{objEBI('" & Me.hid_kakunin_HAN.ClientID & "').value = 'changed';} " & vbCrLf)
            '.Append("alert(objEBI('" & Me.hid_kakunin_HAN.ClientID & "').value);" & vbCrLf)
            .Append("}" & vbCrLf)

            .Append("function SKU_CHANGE_KOU(){" & vbCrLf)
            .Append(" var totalHanNew;" & vbCrLf)
            .Append(" var totalHanOld;" & vbCrLf)
            .Append(" totalHanNew = objEBI('" & Me.ddlSeikyusakiKbn_Kouj.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiCode_Kouj.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiEdaban_Kouj.ClientID & "').value;" & vbCrLf)
            .Append(" totalHanOld = objEBI('" & Me.hid_kbn_KOU.ClientID & "').value+'|'+objEBI('" & Me.hid_cd_KOU.ClientID & "').value+'|'+objEBI('" & Me.hid_brc_KOU.ClientID & "').value;" & vbCrLf)
            '.Append("alert(totalHanNew +'����'+ totalHanOld);" & vbCrLf)
            .Append("if(totalHanNew == totalHanOld){objEBI('" & Me.hid_kakunin_KOU.ClientID & "').value = '';}else{objEBI('" & Me.hid_kakunin_KOU.ClientID & "').value = 'changed';} " & vbCrLf)
            '.Append("alert(objEBI('" & Me.hid_kakunin_KOU.ClientID & "').value);" & vbCrLf)
            .Append("}" & vbCrLf)


            .Append("function SKU_CHANGE_TAT(){" & vbCrLf)
            .Append(" var totalHanNew;" & vbCrLf)
            .Append(" var totalHanOld;" & vbCrLf)
            .Append(" totalHanNew = objEBI('" & Me.ddlSeikyusakiKbn_Tatemono.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiCode_Tatemono.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiEdaban_Tatemono.ClientID & "').value;" & vbCrLf)
            .Append(" totalHanOld = objEBI('" & Me.hid_kbn_TAT.ClientID & "').value+'|'+objEBI('" & Me.hid_cd_TAT.ClientID & "').value+'|'+objEBI('" & Me.hid_brc_TAT.ClientID & "').value;" & vbCrLf)
            '.Append("alert(totalHanNew +'����'+ totalHanOld);" & vbCrLf)
            .Append("if(totalHanNew == totalHanOld){objEBI('" & Me.hid_kakunin_TAT.ClientID & "').value = '';}else{objEBI('" & Me.hid_kakunin_TAT.ClientID & "').value = 'changed';} " & vbCrLf)
            '.Append("alert(objEBI('" & Me.hid_kakunin_TAT.ClientID & "').value);" & vbCrLf)
            .Append("}" & vbCrLf)


            .Append("function SKU_CHANGE_TYS(){" & vbCrLf)
            .Append(" var totalHanNew;" & vbCrLf)
            .Append(" var totalHanOld;" & vbCrLf)
            .Append(" totalHanNew = objEBI('" & Me.ddlSeikyusakiKbn_Tys.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiCode_Tys.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiEdaban_Tys.ClientID & "').value;" & vbCrLf)
            .Append(" totalHanOld = objEBI('" & Me.hid_kbn_TYS.ClientID & "').value+'|'+objEBI('" & Me.hid_cd_TYS.ClientID & "').value+'|'+objEBI('" & Me.hid_brc_TYS.ClientID & "').value;" & vbCrLf)
            '.Append("alert(totalHanNew +'����'+ totalHanOld);" & vbCrLf)
            .Append("if(totalHanNew == totalHanOld){objEBI('" & Me.hid_kakunin_TYS.ClientID & "').value = '';}else{objEBI('" & Me.hid_kakunin_TYS.ClientID & "').value = 'changed';} " & vbCrLf)
            '.Append("alert(objEBI('" & Me.hid_kakunin_TYS.ClientID & "').value);" & vbCrLf)
            .Append("}" & vbCrLf)

            '------������@�@�ǉ��@��������������������������������������������������������������������������

            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            .Append("function SKU_CHANGE_SK5(){" & vbCrLf)
            .Append(" var totalHanNew;" & vbCrLf)
            .Append(" var totalHanOld;" & vbCrLf)
            .Append(" totalHanNew = objEBI('" & Me.ddlSeikyusakiKbn_Sk5.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiCode_Sk5.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiEdaban_Sk5.ClientID & "').value;" & vbCrLf)
            .Append(" totalHanOld = objEBI('" & Me.hid_kbn_SK5.ClientID & "').value+'|'+objEBI('" & Me.hid_cd_SK5.ClientID & "').value+'|'+objEBI('" & Me.hid_brc_SK5.ClientID & "').value;" & vbCrLf)
            '.Append("alert(totalHanNew +'����'+ totalHanOld);" & vbCrLf)
            .Append("if(totalHanNew == totalHanOld){objEBI('" & Me.hid_kakunin_SK5.ClientID & "').value = '';}else{objEBI('" & Me.hid_kakunin_SK5.ClientID & "').value = 'changed';} " & vbCrLf)
            '.Append("alert(objEBI('" & Me.hid_kakunin_TYS.ClientID & "').value);" & vbCrLf)
            .Append("}" & vbCrLf)

            .Append("function SKU_CHANGE_SK6(){" & vbCrLf)
            .Append(" var totalHanNew;" & vbCrLf)
            .Append(" var totalHanOld;" & vbCrLf)
            .Append(" totalHanNew = objEBI('" & Me.ddlSeikyusakiKbn_Sk6.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiCode_Sk6.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiEdaban_Sk6.ClientID & "').value;" & vbCrLf)
            .Append(" totalHanOld = objEBI('" & Me.hid_kbn_SK6.ClientID & "').value+'|'+objEBI('" & Me.hid_cd_SK6.ClientID & "').value+'|'+objEBI('" & Me.hid_brc_SK6.ClientID & "').value;" & vbCrLf)
            '.Append("alert(totalHanNew +'����'+ totalHanOld);" & vbCrLf)
            .Append("if(totalHanNew == totalHanOld){objEBI('" & Me.hid_kakunin_SK6.ClientID & "').value = '';}else{objEBI('" & Me.hid_kakunin_SK6.ClientID & "').value = 'changed';} " & vbCrLf)
            '.Append("alert(objEBI('" & Me.hid_kakunin_TYS.ClientID & "').value);" & vbCrLf)
            .Append("}" & vbCrLf)

            .Append("function SKU_CHANGE_SK7(){" & vbCrLf)
            .Append(" var totalHanNew;" & vbCrLf)
            .Append(" var totalHanOld;" & vbCrLf)
            .Append(" totalHanNew = objEBI('" & Me.ddlSeikyusakiKbn_Sk7.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiCode_Sk7.ClientID & "').value+'|'+objEBI('" & Me.tbxSeikyusakiEdaban_Sk7.ClientID & "').value;" & vbCrLf)
            .Append(" totalHanOld = objEBI('" & Me.hid_kbn_SK7.ClientID & "').value+'|'+objEBI('" & Me.hid_cd_SK7.ClientID & "').value+'|'+objEBI('" & Me.hid_brc_SK7.ClientID & "').value;" & vbCrLf)
            '.Append("alert(totalHanNew +'����'+ totalHanOld);" & vbCrLf)
            .Append("if(totalHanNew == totalHanOld){objEBI('" & Me.hid_kakunin_SK7.ClientID & "').value = '';}else{objEBI('" & Me.hid_kakunin_SK7.ClientID & "').value = 'changed';} " & vbCrLf)
            '.Append("alert(objEBI('" & Me.hid_kakunin_TYS.ClientID & "').value);" & vbCrLf)
            .Append("}" & vbCrLf)

            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��
            '�̔����i�}�X�^�Ɖ��ʂ��J��
            .Append("function fncOpenHanbai(){" & vbCrLf)
            .Append("   objSrchWin = window.open('HanbaiKakakuMasterSearchList.aspx?sendSearchTerms=1$$$'+'" & _kameiten_cd & "');")
            .Append("}")
            '�����X���i�������@���ʑΉ��}�X�^�Ɖ��ʂ��J��
            .Append("function fncOpenTokubetu(){" & vbCrLf)
            '=================2012/03/08 �ԗ� �G���[�̑Ή� �C����===========================
            '.Append("   objSrchWin = window.open('TokubetuTaiouMasterSearchList.aspx?sendSearchTerms='+'" & _kameiten_cd & "');")
            .Append("   objSrchWin = window.open('TokubetuTaiouMasterSearchList.aspx?sendSearchTerms=1$$$'+'" & _kameiten_cd & "');")
            '=================2012/03/08 �ԗ� �G���[�̑Ή� �C����===========================
            .Append("}")
            '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��

        End With
        ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType, csName, csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' �����敔���̃e�L�X�g�{�b�N�X�ɓ��������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub tbxEventSettings()

        Me.ddlSeikyusakiKbn_Hansoku.Attributes.Add("onchange", "SKU_CHANGE_HAN();")
        Me.tbxSeikyusakiCode_Hansoku.Attributes.Add("onchange", "SKU_CHANGE_HAN();")
        Me.tbxSeikyusakiEdaban_Hansoku.Attributes.Add("onchange", "SKU_CHANGE_HAN();")

        Me.ddlSeikyusakiKbn_Kouj.Attributes.Add("onchange", "SKU_CHANGE_KOU();")
        Me.tbxSeikyusakiCode_Kouj.Attributes.Add("onchange", "SKU_CHANGE_KOU();")
        Me.tbxSeikyusakiEdaban_Kouj.Attributes.Add("onchange", "SKU_CHANGE_KOU();")

        Me.ddlSeikyusakiKbn_Tatemono.Attributes.Add("onchange", "SKU_CHANGE_TAT();")
        Me.tbxSeikyusakiCode_Tatemono.Attributes.Add("onchange", "SKU_CHANGE_TAT();")
        Me.tbxSeikyusakiEdaban_Tatemono.Attributes.Add("onchange", "SKU_CHANGE_TAT();")

        Me.ddlSeikyusakiKbn_Tys.Attributes.Add("onchange", "SKU_CHANGE_TYS();")
        Me.tbxSeikyusakiCode_Tys.Attributes.Add("onchange", "SKU_CHANGE_TYS();")
        Me.tbxSeikyusakiEdaban_Tys.Attributes.Add("onchange", "SKU_CHANGE_TYS();")

        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        Me.ddlSeikyusakiKbn_Sk5.Attributes.Add("onchange", "SKU_CHANGE_SK5();")
        Me.tbxSeikyusakiCode_Sk5.Attributes.Add("onchange", "SKU_CHANGE_SK5();")
        Me.tbxSeikyusakiEdaban_Sk5.Attributes.Add("onchange", "SKU_CHANGE_SK5();")

        Me.ddlSeikyusakiKbn_Sk6.Attributes.Add("onchange", "SKU_CHANGE_SK6();")
        Me.tbxSeikyusakiCode_Sk6.Attributes.Add("onchange", "SKU_CHANGE_SK6();")
        Me.tbxSeikyusakiEdaban_Sk6.Attributes.Add("onchange", "SKU_CHANGE_SK6();")

        Me.ddlSeikyusakiKbn_Sk7.Attributes.Add("onchange", "SKU_CHANGE_SK7();")
        Me.tbxSeikyusakiCode_Sk7.Attributes.Add("onchange", "SKU_CHANGE_SK7();")
        Me.tbxSeikyusakiEdaban_Sk7.Attributes.Add("onchange", "SKU_CHANGE_SK7();")
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================

    End Sub

    ''' <summary>
    ''' ��ʍ��ڕ\���ݒ�
    ''' </summary>
    ''' <param name="sender">����{�^���敪</param>
    ''' <remarks></remarks>
    Protected Sub gameihHyouji(ByVal sender As String)

        Dim kameiten As New DataTable
        Dim tatou As New DataTable
        Dim kinoMsg As String
        Dim kaimeitenCd As String

        If sender = "Page_Load" Then
            kaimeitenCd = ViewState.Item("kameiten_cd_touroku")
        Else
            kaimeitenCd = ViewState.Item("kameiten_cd")
        End If

        kameiten = KihonJyouhouBL.GetKameiten(kaimeitenCd)
        '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
        'tatou = KihonJyouhouBL.GetTatou(kaimeitenCd)
        tatou = KihonJyouhouBL.GetTatouKakaku(kaimeitenCd)
        '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

        If kameiten.Rows.Count = 0 Then

            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_gameihHyouji", "alert('" & Messages.Instance.MSG020E & "');", True)

        Else

            If sender = "Page_Load" Then
                ViewState.Item("tatou") = tatou
            End If

            '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��
            'Me.tbxSSkk.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("ss_kkk")))
            'Me.tbxSSGRkk.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("ssgr_kkk")))
            'Me.tbxKaiseki.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("kaiseki_hosyou_kkk")))
            Me.tbxKaiyaku.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("kaiyaku_haraimodosi_kkk")))
            '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��
            Me.tbxKaisekiHosyouKkk.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("kaiseki_hosyou_kkk")))
            Me.tbxSsgrKkk.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("ssgr_kkk")))
            '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��

            'Me.tbxSaitys.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("sai_tys_kkk")))
            'Me.tbxHousyomu.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("hosyounasi_umu")))
            'Me.tbxJizentys.Text = AddComa(CommonLG.getDisplayString(kameiten.Rows(0).Item("jizen_tys_kkk")))

            If sender = "Page_Load" Then

                'Me.tbxTysSekyuSaki.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("tys_seikyuu_saki"))
                'Me.tbxKoujSekyuSaki.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("koj_seikyuusaki"))
                'Me.tbxHansokuSekyuSaki.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("hansokuhin_seikyuusaki"))

            ElseIf sender = "btnCopy_Click" Then

                'Me.tbxTysSekyuSaki.Text = IIf(CommonLG.getDisplayString(kameiten.Rows(0).Item("tys_seikyuu_saki")) = "", kaimeitenCd, kameiten.Rows(0).Item("tys_seikyuu_saki").ToString)
                'Me.tbxKoujSekyuSaki.Text = IIf(CommonLG.getDisplayString(kameiten.Rows(0).Item("koj_seikyuusaki")) = "", kaimeitenCd, kameiten.Rows(0).Item("koj_seikyuusaki").ToString)
                'Me.tbxHansokuSekyuSaki.Text = IIf(CommonLG.getDisplayString(kameiten.Rows(0).Item("hansokuhin_seikyuusaki")) = "", kaimeitenCd, kameiten.Rows(0).Item("hansokuhin_seikyuusaki").ToString)

            End If

            'Me.tbxTysSekyuDate.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("tys_seikyuu_sime_date"))
            'Me.tbxKoujSekyuDate.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("koj_seikyuu_sime_date"))
            'Me.tbxHansokuSekyuDate.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("hansokuhin_seikyuu_sime_date"))
            '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��

            ' :-) 100606 �� ������ �ǉ� ��������������������������������������������������������

            '������敪---DDL---�l---�ݒ�
            If sender = "Page_Load" Then
                Call SeikyusakiKBNStart()

                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                '�����捀�ږ���ݒ肷��
                Call Me.SetSeikyuusakiKoumokuMei()
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================
            End If

            '������敪---DDL---�l---�ݒ�

            '������敪�@��
            Me.ddlSeikyusakiKbn_Tys.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("tys_seikyuu_saki_kbn"))
            Me.ddlSeikyusakiKbn_Kouj.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("koj_seikyuu_saki_kbn"))
            Me.ddlSeikyusakiKbn_Hansoku.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("hansokuhin_seikyuu_saki_kbn"))
            Me.ddlSeikyusakiKbn_Tatemono.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("tatemono_seikyuu_saki_kbn"))
            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            Me.ddlSeikyusakiKbn_Sk5.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_kbn5"))
            Me.ddlSeikyusakiKbn_Sk6.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_kbn6"))
            Me.ddlSeikyusakiKbn_Sk7.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_kbn7"))
            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            '������R�[�h�@��
            Me.tbxSeikyusakiCode_Tys.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("tys_seikyuu_saki_cd"))
            Me.tbxSeikyusakiCode_Kouj.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("koj_seikyuu_saki_cd"))
            Me.tbxSeikyusakiCode_Hansoku.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("hansokuhin_seikyuu_saki_cd"))
            Me.tbxSeikyusakiCode_Tatemono.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("tatemono_seikyuu_saki_cd"))
            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            Me.tbxSeikyusakiCode_Sk5.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_cd5"))
            Me.tbxSeikyusakiCode_Sk6.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_cd6"))
            Me.tbxSeikyusakiCode_Sk7.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_cd7"))
            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            '������}�ԁ@��
            Me.tbxSeikyusakiEdaban_Tys.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("tys_seikyuu_saki_brc"))
            Me.tbxSeikyusakiEdaban_Kouj.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("koj_seikyuu_saki_brc"))
            Me.tbxSeikyusakiEdaban_Hansoku.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("hansokuhin_seikyuu_saki_brc"))
            Me.tbxSeikyusakiEdaban_Tatemono.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("tatemono_seikyuu_saki_brc"))
            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            Me.tbxSeikyusakiEdaban_Sk5.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_brc5"))
            Me.tbxSeikyusakiEdaban_Sk6.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_brc6"))
            Me.tbxSeikyusakiEdaban_Sk7.Text = CommonLG.getDisplayString(kameiten.Rows(0).Item("seikyuu_saki_brc7"))
            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            '��{�Z�b�g�@�g�p�@���f
            Call KihonSetABO()

            ' ������ �ǉ� ����������������������������������������������������������������������

            Call SyouhinStart()

            Dim i As Int16
            Dim strHyoujunKkk As String
            For i = 0 To tatou.Rows.Count - 1
                If tatou.Rows(i).Item("toukubun") = 1 And tatou.Rows(i).Item("syouhin_cd").ToString <> "" Then
                    Me.tbxSyouhinCd1.Text = CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd"))
                    Me.tbxSyouhinNm1.Text = IIf(CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")) = "00000", "�����ݒ�Ȃ�", tatou.Rows(i).Item("syouhin_mei"))
                    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
                    If CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")).Trim.Equals("00000") Then
                        Me.tbxMoney1.Text = String.Empty
                    Else
                        strHyoujunKkk = tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim
                        If strHyoujunKkk.Equals(String.Empty) Then
                            Me.tbxMoney1.Text = String.Empty
                        Else
                            Me.tbxMoney1.Text = FormatNumber(strHyoujunKkk, 0) & "�~"
                        End If
                    End If
                    'Me.tbxMoney1.Text = IIf((CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")) = "00000") OrElse (tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim.Equals(String.Empty)), String.Empty, FormatNumber(tatou.Rows(i).Item("hyoujun_kkk"), 0) & "�~")
                    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
                End If

                If tatou.Rows(i).Item("toukubun") = 2 And tatou.Rows(i).Item("syouhin_cd").ToString <> "" Then
                    Me.tbxSyouhinCd2.Text = CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd"))
                    Me.tbxSyouhinNm2.Text = IIf(CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")) = "00000", "�����ݒ�Ȃ�", tatou.Rows(i).Item("syouhin_mei"))
                    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
                    If CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")).Trim.Equals("00000") Then
                        Me.tbxMoney2.Text = String.Empty
                    Else
                        strHyoujunKkk = tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim
                        If strHyoujunKkk.Equals(String.Empty) Then
                            Me.tbxMoney2.Text = String.Empty
                        Else
                            Me.tbxMoney2.Text = FormatNumber(strHyoujunKkk, 0) & "�~"
                        End If
                    End If
                    'Me.tbxMoney2.Text = IIf((CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")) = "00000") OrElse (tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim.Equals(String.Empty)), String.Empty, FormatNumber(tatou.Rows(i).Item("hyoujun_kkk"), 0) & "�~")
                    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
                End If

                If tatou.Rows(i).Item("toukubun") = 3 And tatou.Rows(i).Item("syouhin_cd").ToString <> "" Then
                    Me.tbxSyouhinCd3.Text = CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd"))
                    Me.tbxSyouhinNm3.Text = IIf(CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")) = "00000", "�����ݒ�Ȃ�", tatou.Rows(i).Item("syouhin_mei"))
                    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
                    If CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")).Trim.Equals("00000") Then
                        Me.tbxMoney3.Text = String.Empty
                    Else
                        strHyoujunKkk = tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim
                        If strHyoujunKkk.Equals(String.Empty) Then
                            Me.tbxMoney3.Text = String.Empty
                        Else
                            Me.tbxMoney3.Text = FormatNumber(strHyoujunKkk, 0) & "�~"
                        End If
                    End If
                    'Me.tbxMoney3.Text = IIf(CommonLG.getDisplayString(tatou.Rows(i).Item("syouhin_cd")) = "00000", String.Empty, FormatNumber(tatou.Rows(i).Item("hyoujun_kkk"), 0) & "�~")
                    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
                End If
            Next

            kinoMsg = "���R�s�["
            kinoMsg = KihonJyouhouBL.MakeMessage(Messages.Instance.MSG018S, kinoMsg)

            If sender = "btnCopy_Click" Then
                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_btnCopy_Click", "alert('" & kinoMsg & "');", True)
            End If

            Call setSeikyusakiNmAndSimebi()

        End If

        ':) 100607 --------������������������ ������@�ǉ�
        If sender = "Page_Load" Then

            'CONFRIM HID VALUE GOTO START
            Me.hidConfrim.Value = "0"

            ' MAKE HIDVALUE GOTO START VALUE
            Call hidvalueToStart()



        End If


        ' --------����������������������������

    End Sub

    '�����於�ƒ����̐ݒ�
    Private Sub setSeikyusakiNmAndSimebi()

        Call Me.KensakuEvent(Me.btnSeikyusaki_Sel_Tys.ClientID.ToString, "Page_Load")
        Call Me.KensakuEvent(Me.btnSeikyusaki_Sel_Kouj.ClientID.ToString, "Page_Load")
        Call Me.KensakuEvent(Me.btnSeikyusaki_Sel_Hansoku.ClientID.ToString, "Page_Load")
        Call Me.KensakuEvent(Me.btnSeikyusaki_Sel_Tatemono.ClientID.ToString, "Page_Load")
        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        Call Me.KensakuEvent(Me.btnSeikyusaki_Sel_Sk5.ClientID.ToString, "Page_Load")
        Call Me.KensakuEvent(Me.btnSeikyusaki_Sel_Sk6.ClientID.ToString, "Page_Load")
        Call Me.KensakuEvent(Me.btnSeikyusaki_Sel_Sk7.ClientID.ToString, "Page_Load")
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================


    End Sub


    ''' <summary>
    ''' MAKE HIDVALUE GOTO START VALUE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub hidvalueToStart()

        hid_kbn_HAN.Value = Me.ddlSeikyusakiKbn_Hansoku.Text
        hid_cd_HAN.Value = Me.tbxSeikyusakiCode_Hansoku.Text
        hid_brc_HAN.Value = Me.tbxSeikyusakiEdaban_Hansoku.Text
        hid_kakunin_HAN.Value = ""

        hid_kbn_KOU.Value = Me.ddlSeikyusakiKbn_Kouj.Text
        hid_cd_KOU.Value = Me.tbxSeikyusakiCode_Kouj.Text
        hid_brc_KOU.Value = Me.tbxSeikyusakiEdaban_Kouj.Text
        hid_kakunin_KOU.Value = ""

        hid_kbn_TAT.Value = Me.ddlSeikyusakiKbn_Tatemono.Text
        hid_cd_TAT.Value = Me.tbxSeikyusakiCode_Tatemono.Text
        hid_brc_TAT.Value = Me.tbxSeikyusakiEdaban_Tatemono.Text
        hid_kakunin_TAT.Value = ""

        hid_kbn_TYS.Value = Me.ddlSeikyusakiKbn_Tys.Text
        hid_cd_TYS.Value = Me.tbxSeikyusakiCode_Tys.Text
        hid_brc_TYS.Value = Me.tbxSeikyusakiEdaban_Tys.Text
        hid_kakunin_TYS.Value = ""

        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        hid_kbn_SK5.Value = Me.ddlSeikyusakiKbn_Sk5.Text
        hid_cd_SK5.Value = Me.tbxSeikyusakiCode_Sk5.Text
        hid_brc_SK5.Value = Me.tbxSeikyusakiEdaban_Sk5.Text
        hid_kakunin_SK5.Value = ""

        hid_kbn_SK6.Value = Me.ddlSeikyusakiKbn_Sk6.Text
        hid_cd_SK6.Value = Me.tbxSeikyusakiCode_Sk6.Text
        hid_brc_SK6.Value = Me.tbxSeikyusakiEdaban_Sk6.Text
        hid_kakunin_SK6.Value = ""

        hid_kbn_SK7.Value = Me.ddlSeikyusakiKbn_Sk7.Text
        hid_cd_SK7.Value = Me.tbxSeikyusakiCode_Sk7.Text
        hid_brc_SK7.Value = Me.tbxSeikyusakiEdaban_Sk7.Text
        hid_kakunin_SK7.Value = ""
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================

    End Sub

    ''' <summary>
    ''' confrim�ōēx���[�h�̎���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub reload_confrim()

        If ",1,2,".IndexOf(Me.hidConfrim.Value.Substring(0, 1)) > 0 Then

            Dim sender As String = Me.hidConfrim.Value.Substring(2, 3)

            Dim ddl_kbn_obj As New DropDownList
            Dim tbx_cd_obj As New TextBox
            Dim tbx_brc_obj As New TextBox

            Dim lbl_nm_obj As New TextBox
            Dim lbl_sime_obj As New TextBox

            Dim HID_SKU_KBN_OBJ As HiddenField
            Dim HID_SKU_CD_OBJ As HiddenField
            Dim HID_SKU_BRC_OBJ As HiddenField
            Dim HID_SKU_KAKUN_OBJ As HiddenField

            If sender = "HAN" Then

                ddl_kbn_obj = Me.ddlSeikyusakiKbn_Hansoku
                tbx_cd_obj = Me.tbxSeikyusakiCode_Hansoku
                tbx_brc_obj = Me.tbxSeikyusakiEdaban_Hansoku

                lbl_nm_obj = Me.lblSeikyusaki_name_Hansoku
                lbl_sime_obj = Me.lblSeikyusaki_simebi_Hansoku

                HID_SKU_KBN_OBJ = Me.hid_kbn_HAN
                HID_SKU_CD_OBJ = Me.hid_cd_HAN
                HID_SKU_BRC_OBJ = Me.hid_brc_HAN
                HID_SKU_KAKUN_OBJ = Me.hid_kakunin_HAN

            End If

            If sender = "KOU" Then
                ddl_kbn_obj = Me.ddlSeikyusakiKbn_Kouj
                tbx_cd_obj = Me.tbxSeikyusakiCode_Kouj
                tbx_brc_obj = Me.tbxSeikyusakiEdaban_Kouj

                lbl_nm_obj = Me.lblSeikyusaki_name_Kouj
                lbl_sime_obj = Me.lblSeikyusaki_simebi_Kouj

                HID_SKU_KBN_OBJ = Me.hid_kbn_KOU
                HID_SKU_CD_OBJ = Me.hid_cd_KOU
                HID_SKU_BRC_OBJ = Me.hid_brc_KOU
                HID_SKU_KAKUN_OBJ = Me.hid_kakunin_KOU

            End If

            If sender = "TAT" Then
                ddl_kbn_obj = Me.ddlSeikyusakiKbn_Tatemono
                tbx_cd_obj = Me.tbxSeikyusakiCode_Tatemono
                tbx_brc_obj = Me.tbxSeikyusakiEdaban_Tatemono

                lbl_nm_obj = Me.lblSeikyusaki_name_Tatemono
                lbl_sime_obj = Me.lblSeikyusaki_simebi_Tatemono

                HID_SKU_KBN_OBJ = Me.hid_kbn_TAT
                HID_SKU_CD_OBJ = Me.hid_cd_TAT
                HID_SKU_BRC_OBJ = Me.hid_brc_TAT
                HID_SKU_KAKUN_OBJ = Me.hid_kakunin_TAT

            End If

            If sender = "TYS" Then
                ddl_kbn_obj = Me.ddlSeikyusakiKbn_Tys
                tbx_cd_obj = Me.tbxSeikyusakiCode_Tys
                tbx_brc_obj = Me.tbxSeikyusakiEdaban_Tys

                lbl_nm_obj = Me.lblSeikyusaki_name_Tys
                lbl_sime_obj = Me.lblSeikyusaki_simebi_Tys

                HID_SKU_KBN_OBJ = Me.hid_kbn_TYS
                HID_SKU_CD_OBJ = Me.hid_cd_TYS
                HID_SKU_BRC_OBJ = Me.hid_brc_TYS
                HID_SKU_KAKUN_OBJ = Me.hid_kakunin_TYS

            End If

            '========================2011/06/28 �ԗ� �ǉ� �J�n��=======================================
            If sender = "SK5" Then
                ddl_kbn_obj = Me.ddlSeikyusakiKbn_Sk5
                tbx_cd_obj = Me.tbxSeikyusakiCode_Sk5
                tbx_brc_obj = Me.tbxSeikyusakiEdaban_Sk5

                lbl_nm_obj = Me.lblSeikyusaki_name_Sk5
                lbl_sime_obj = Me.lblSeikyusaki_simebi_Sk5

                HID_SKU_KBN_OBJ = Me.hid_kbn_SK5
                HID_SKU_CD_OBJ = Me.hid_cd_SK5
                HID_SKU_BRC_OBJ = Me.hid_brc_SK5
                HID_SKU_KAKUN_OBJ = Me.hid_kakunin_SK5

            End If

            If sender = "SK6" Then
                ddl_kbn_obj = Me.ddlSeikyusakiKbn_Sk6
                tbx_cd_obj = Me.tbxSeikyusakiCode_Sk6
                tbx_brc_obj = Me.tbxSeikyusakiEdaban_Sk6

                lbl_nm_obj = Me.lblSeikyusaki_name_Sk6
                lbl_sime_obj = Me.lblSeikyusaki_simebi_Sk6

                HID_SKU_KBN_OBJ = Me.hid_kbn_SK6
                HID_SKU_CD_OBJ = Me.hid_cd_SK6
                HID_SKU_BRC_OBJ = Me.hid_brc_SK6
                HID_SKU_KAKUN_OBJ = Me.hid_kakunin_SK6

            End If

            If sender = "SK7" Then
                ddl_kbn_obj = Me.ddlSeikyusakiKbn_Sk7
                tbx_cd_obj = Me.tbxSeikyusakiCode_Sk7
                tbx_brc_obj = Me.tbxSeikyusakiEdaban_Sk7

                lbl_nm_obj = Me.lblSeikyusaki_name_Sk7
                lbl_sime_obj = Me.lblSeikyusaki_simebi_Sk7

                HID_SKU_KBN_OBJ = Me.hid_kbn_SK7
                HID_SKU_CD_OBJ = Me.hid_cd_SK7
                HID_SKU_BRC_OBJ = Me.hid_brc_SK7
                HID_SKU_KAKUN_OBJ = Me.hid_kakunin_SK7

            End If
            '========================2011/06/28 �ԗ� �ǉ� �I����=======================================


            If Me.hidConfrim.Value.Substring(0, 1) = "1" Then
                '�EOK��I��
                '     �E������o�^���`�}�X�^����������i
                '     KEY:������o�^���`�}�X�^�D������}�� = �B��ʁD������}��
                '     AND ������o�^���`�}�X�^�D��� = 0
                '           �j
                Dim SKU_BRC As String
                SKU_BRC = Me.hidConfrim.Value.Substring(5)

                Dim seikyusakiHinaDT As Data.DataTable

                seikyusakiHinaDT = KihonJyouhouBL.GetSeikyusakiHina(SKU_BRC, 0)

                If seikyusakiHinaDT.Rows.Count > 0 Then
                    '           �E�Y������̂Ƃ�
                    '                 �C���.�����於 = "�y������V�K�o�^�z" + ������o�^���`�}�X�^�D�\�����e
                    lbl_nm_obj.Text = "�y������V�K�o�^�z" & seikyusakiHinaDT.Rows(0).Item("hyouji_naiyou").ToString

                    '                 �D���.�������ߓ� = ������o�^���`�}�X�^�D�������ߓ�
                    If seikyusakiHinaDT.Rows(0).Item("seikyuu_sime_date").ToString.Trim <> "" Then
                        lbl_sime_obj.Text = "���ߓ��F" & seikyusakiHinaDT.Rows(0).Item("seikyuu_sime_date").ToString & "��"
                    Else
                        lbl_sime_obj.Text = ""
                    End If

                Else
                    '           �E��L�ȊO
                    '                 �C���.�����於 = "�y������V�K�o�^�z"
                    lbl_nm_obj.Text = "�y������V�K�o�^�z"
                    '                 �D���.�������ߓ� = ��
                    lbl_sime_obj.Text = ""
                End If

                '++__++__
                'HID confrim check
                HID_SKU_KBN_OBJ.Value = ddl_kbn_obj.Text
                HID_SKU_CD_OBJ.Value = tbx_cd_obj.Text
                HID_SKU_BRC_OBJ.Value = tbx_brc_obj.Text
                HID_SKU_KAKUN_OBJ.Value = ""

                '     ���o�^�{�^���������ɐ�����}�X�^�ɐV�K�o�^����
            Else
                ' �E��L�ȊO
                '     �����挟����ʂ�ʃE�B���h�E�ŋN��(POPUP)

                '     �E�����挟����ʂ̌�������.������敪�ɂ͉��.������敪�̒l���Z�b�g
                '     �E�����挟����ʂ̌�������.������R�[�h�ɂ͉��.������R�[�h�̒l���Z�b�g
                '     �E�����挟����ʂ̌�������.������}�Ԃɂ͉��.������}�Ԃ̒l���Z�b�g

                '     �E�����挟����ʂŌ����A�I���������ʂ���ʂɃZ�b�g
                '           �@���.������敪�@=�@�����挟����ʑI������.������敪
                '           �A���.������R�[�h�@=�@�����挟����ʑI������.������R�[�h
                '           �B���.������}�ԁ@=�@�����挟����ʑI������.������}��

                '           �C���.�����於�@=�@�����於 (    ��������敪�ɉ����������於���擾
                '                 (1) ������}�X�^.������敪 = 0 : �����X
                '                   �A���.������R�[�h = �����X�}�X�^.�����X�R�[�h�ŉ����X�}�X�^���Q�Ƃ�
                '                       �����X�}�X�^.�������̂��擾
                '                 (2) ������}�X�^.������敪 = 1 : �������
                '                       �A���.������R�[�h = ������Ѓ}�X�^.������ЃR�[�h
                '               AND �B��ʂ̐�����}�� = ������Ѓ}�X�^.���Ə��R�[�h�Œ�����Ѓ}�X�^���Q�Ƃ�
                '                       ������Ѓ}�X�^.������x���於���擾
                '                 (3) ������}�X�^.������敪 = 2 : �c�Ə�
                '                   �A���.������R�[�h = �c�Ə��}�X�^.�c�Ə��R�[�h�ŉc�Ə��}�X�^���Q�Ƃ�
                '                       �c�Ə��}�X�^.�����於���擾
                '    )

                'Dim ddl_kbn_obj As New DropDownList
                'Dim tbx_cd_obj As New TextBox
                'Dim tbx_brc_obj As New TextBox

                'Dim lbl_nm_obj As New TextBox
                'Dim lbl_sime_obj As New TextBox

                'Dim strScript As String
                'strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&Kbn='+escape('������')+'&FormName=" & _
                '                 Me.Page.Form.Name & "&objKbn=" & _
                '                 ddl_kbn_obj.ClientID & _
                '                 "&objCd=" & tbx_cd_obj.ClientID & _
                '                 "&objBrc=" & tbx_brc_obj.ClientID & _
                '                 "&objMei=" & lbl_nm_obj.ClientID & _
                '                 "&strKbn='+escape(eval('document.all.'+'" & _
                '                 ddl_kbn_obj.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                '                 tbx_cd_obj.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                '                 tbx_brc_obj.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

                'ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "popup_fire", strScript, True)

                'Dim ddl_kbn_obj As New DropDownList
                'Dim tbx_cd_obj As New TextBox
                'Dim tbx_brc_obj As New TextBox

                'Dim lbl_nm_obj As New TextBox
                'Dim lbl_sime_obj As New TextBox
                Dim strScript As String
                strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&Kbn='+escape('������')+'&FormName=" & _
                             Me.Page.Form.Name & "&objKbn=" & _
                             ddl_kbn_obj.ClientID & _
                             "&objHidKbn=" & HID_SKU_KBN_OBJ.ClientID & _
                             "&objCd=" & tbx_cd_obj.ClientID & _
                             "&objHidCd=" & HID_SKU_CD_OBJ.ClientID & _
                             "&hidConfirm2=" & HID_SKU_KAKUN_OBJ.ClientID & _
                             "&objBrc=" & tbx_brc_obj.ClientID & _
                             "&objHidBrc=" & HID_SKU_BRC_OBJ.ClientID & _
                             "&objMei=" & lbl_nm_obj.ClientID & _
                             "&objDate=" & lbl_sime_obj.ClientID & _
                             "&strKbn='+escape(eval('document.all.'+'" & _
                             ddl_kbn_obj.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                             tbx_cd_obj.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                             tbx_brc_obj.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "popup_fire", strScript, True)


            End If

            Me.hidConfrim.Value = "0"

        End If

    End Sub

    ''' <summary>
    ''' �ڍׁ@�{�^���@����@�ꍇ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SyousaiEvent(ByVal sender As String)

        '�E������敪�A�����ύX��A�}�Ԃ�KEY�ɐ�����}�X�^����������i������}�X�^�D��� = 0�j
        Dim SKU_datatable As Data.DataTable
        SKU_datatable = New Data.DataTable

        Dim SKU_KBN As String = ""
        Dim SKU_CD As String = ""
        Dim SKU_BRC As String = ""

        Dim SKU_KBN_OBJ As New DropDownList
        Dim SKU_CD_OBJ As New TextBox
        Dim SKU_BRC_OBJ As New TextBox
        Dim SKU_NM_OBJ As New TextBox
        Dim SKU_SIMEBI_OBJ As New TextBox

        'Me.btnSeikyusaki_Sel_Hansoku
        'Me.btnSeikyusaki_Sel_Kouj
        'Me.btnSeikyusaki_Sel_Tatemono
        'Me.btnSeikyusaki_Sel_Tys

        If sender.IndexOf("Hansoku") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Hansoku.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Hansoku.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Hansoku
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Hansoku
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Hansoku
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Hansoku
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Hansoku

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        If sender.IndexOf("Kouj") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Kouj.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Kouj.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Kouj.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Kouj
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Kouj
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Kouj
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Kouj
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Kouj

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        If sender.IndexOf("Tatemono") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Tatemono.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Tatemono.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Tatemono.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Tatemono
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Tatemono
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Tatemono
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Tatemono
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Tatemono

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        If sender.IndexOf("Tys") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Tys.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Tys.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Tys.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Tys
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Tys
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Tys
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Tys
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Tys

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        '========================2011/06/28 �ԗ� �ǉ� �J�n��=======================================
        If sender.IndexOf("Sk5") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Sk5.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Sk5.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Sk5.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Sk5
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Sk5
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Sk5
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Sk5
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Sk5

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        If sender.IndexOf("Sk6") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Sk6.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Sk6.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Sk6.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Sk6
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Sk6
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Sk6
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Sk6
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Sk6

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        If sender.IndexOf("Sk7") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Sk7.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Sk7.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Sk7.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Sk7
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Sk7
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Sk7
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Sk7
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Sk7

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If
        '========================2011/06/28 �ԗ� �ǉ� �I����=======================================

        If SKU_KBN.Trim = "" OrElse SKU_CD.Trim = "" OrElse SKU_BRC.Trim = "" Then

            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_Allinput", "alert('�������񂪐ݒ肳��Ă��܂���B\r\n���������͂��ĉ������B');", True)

            Exit Sub

        End If

        '�E�@������敪�A�A�����ύX��A�B������}�Ԃ�KEY�ɐ�����}�X�^����������			
        '      �E�������ʂ�1���������ꍇ
        If SKU_datatable.Rows.Count = 1 Then
            '      �@�@�@������}�X�^�����e�i���X��ʂ��J��
            Dim strScript As String
            strScript = "objSrchWin = window.open('SeikyuuSakiMaster.aspx?sendSearchTerms='+escape(eval('document.all.'+'" & SKU_CD_OBJ.ClientID & "').value)+'$$$'+escape(eval('document.all.'+'" & SKU_BRC_OBJ.ClientID & "').value)+'$$$'+escape(eval('document.all.'+'" & SKU_KBN_OBJ.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_seikyuInputPage", strScript, True)
        Else
            '      ���L�ȊO
            '�@�@�@�@�@�@���b�Z�[�W�u�����悪���݂��܂���v��\�����A�������f	
            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_Syousai", "alert('�����悪���݂��܂���B');", True)
        End If


    End Sub

    ''' <summary>
    ''' �����@�{�^���@����@�ꍇ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KensakuEvent(ByVal sender As String, Optional ByVal FromWhere As String = "����")

        '�E������敪�A�����ύX��A�}�Ԃ�KEY�ɐ�����}�X�^����������i������}�X�^�D��� = 0�j
        Dim SKU_datatable As Data.DataTable
        SKU_datatable = New Data.DataTable

        Dim SKU_KBN As String = ""
        Dim SKU_CD As String = ""
        Dim SKU_BRC As String = ""

        Dim SKU_KBN_OBJ As New DropDownList
        Dim SKU_CD_OBJ As New TextBox
        Dim SKU_BRC_OBJ As New TextBox
        Dim SKU_NM_OBJ As New TextBox
        Dim SKU_SIMEBI_OBJ As New TextBox

        Dim HID_SKU_KBN_OBJ As New HiddenField
        Dim HID_SKU_CD_OBJ As New HiddenField
        Dim HID_SKU_BRC_OBJ As New HiddenField
        Dim HID_SKU_KAKUN_OBJ As New HiddenField

        If sender.IndexOf("Hansoku") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Hansoku.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Hansoku.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Hansoku
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Hansoku
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Hansoku
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Hansoku
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Hansoku

            HID_SKU_KBN_OBJ = Me.hid_kbn_HAN
            HID_SKU_CD_OBJ = Me.hid_cd_HAN
            HID_SKU_BRC_OBJ = Me.hid_brc_HAN
            HID_SKU_KAKUN_OBJ = Me.hid_kakunin_HAN

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        If sender.IndexOf("Kouj") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Kouj.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Kouj.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Kouj.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Kouj
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Kouj
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Kouj
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Kouj
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Kouj

            HID_SKU_KBN_OBJ = Me.hid_kbn_KOU
            HID_SKU_CD_OBJ = Me.hid_cd_KOU
            HID_SKU_BRC_OBJ = Me.hid_brc_KOU
            HID_SKU_KAKUN_OBJ = Me.hid_kakunin_KOU

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        If sender.IndexOf("Tatemono") > 0 Then

            SKU_KBN = Me.ddlSeikyusakiKbn_Tatemono.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Tatemono.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Tatemono.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Tatemono
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Tatemono
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Tatemono
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Tatemono
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Tatemono

            HID_SKU_KBN_OBJ = Me.hid_kbn_TAT
            HID_SKU_CD_OBJ = Me.hid_cd_TAT
            HID_SKU_BRC_OBJ = Me.hid_brc_TAT
            HID_SKU_KAKUN_OBJ = Me.hid_kakunin_TAT

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        If sender.IndexOf("Tys") > 0 Then

            
            SKU_KBN = Me.ddlSeikyusakiKbn_Tys.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Tys.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Tys.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Tys
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Tys
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Tys
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Tys
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Tys

            HID_SKU_KBN_OBJ = Me.hid_kbn_TYS
            HID_SKU_CD_OBJ = Me.hid_cd_TYS
            HID_SKU_BRC_OBJ = Me.hid_brc_TYS
            HID_SKU_KAKUN_OBJ = Me.hid_kakunin_TYS

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)

        End If

        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        If sender.IndexOf("Sk5") > 0 Then
            SKU_KBN = Me.ddlSeikyusakiKbn_Sk5.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Sk5.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Sk5.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Sk5
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Sk5
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Sk5
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Sk5
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Sk5

            HID_SKU_KBN_OBJ = Me.hid_kbn_SK5
            HID_SKU_CD_OBJ = Me.hid_cd_SK5
            HID_SKU_BRC_OBJ = Me.hid_brc_SK5
            HID_SKU_KAKUN_OBJ = Me.hid_kakunin_SK5

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)
        End If

        If sender.IndexOf("Sk6") > 0 Then
            SKU_KBN = Me.ddlSeikyusakiKbn_Sk6.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Sk6.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Sk6.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Sk6
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Sk6
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Sk6
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Sk6
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Sk6

            HID_SKU_KBN_OBJ = Me.hid_kbn_SK6
            HID_SKU_CD_OBJ = Me.hid_cd_SK6
            HID_SKU_BRC_OBJ = Me.hid_brc_SK6
            HID_SKU_KAKUN_OBJ = Me.hid_kakunin_SK6

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)
        End If

        If sender.IndexOf("Sk7") > 0 Then
            SKU_KBN = Me.ddlSeikyusakiKbn_Sk7.SelectedValue
            SKU_CD = Me.tbxSeikyusakiCode_Sk7.Text
            SKU_BRC = Me.tbxSeikyusakiEdaban_Sk7.Text

            SKU_KBN_OBJ = Me.ddlSeikyusakiKbn_Sk7
            SKU_CD_OBJ = Me.tbxSeikyusakiCode_Sk7
            SKU_BRC_OBJ = Me.tbxSeikyusakiEdaban_Sk7
            SKU_NM_OBJ = Me.lblSeikyusaki_name_Sk7
            SKU_SIMEBI_OBJ = Me.lblSeikyusaki_simebi_Sk7

            HID_SKU_KBN_OBJ = Me.hid_kbn_SK7
            HID_SKU_CD_OBJ = Me.hid_cd_SK7
            HID_SKU_BRC_OBJ = Me.hid_brc_SK7
            HID_SKU_KAKUN_OBJ = Me.hid_kakunin_SK7

            SKU_datatable = KihonJyouhouBL.GetSeikyusaki(SKU_KBN, SKU_CD, SKU_BRC, 0, False)
        End If
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================

        If FromWhere = "Page_Load" Then

            If SKU_datatable.Rows.Count = 0 Then

                SKU_NM_OBJ.Text = ""
                SKU_SIMEBI_OBJ.Text = ""

                Exit Sub

            Else

                If SKU_KBN.Trim = "" AndAlso SKU_CD.Trim = "" AndAlso SKU_BRC.Trim = "" Then
                    SKU_NM_OBJ.Text = ""
                    SKU_SIMEBI_OBJ.Text = ""
                    Exit Sub
                End If

            End If

        Else

            SKU_NM_OBJ.Text = ""
            SKU_SIMEBI_OBJ.Text = ""

        End If

        '      �E�������ʂ�1���������ꍇ
        If SKU_datatable.Rows.Count = 1 Then

            If SKU_datatable.Rows(0).Item(3).ToString = "1" And FromWhere <> "Page_Load" Then

                '                  �E���b�Z�[�W�u�w�肵��������͎������Ă��܂��B�v�\��
                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "alert_torikesiSita", "alert('�w�肵��������͎������Ă��܂��B');", True)

                Exit Sub

            End If

            '            �@���.������敪�@=�@������}�X�^.������敪
            '            �A���.������R�[�h�@=�@������}�X�^.������R�[�h
            '            �B���.������}�ԁ@=�@������}�X�^.������}��
            '�Ӗ������A�����@���������`�`�F�j

            '            �C���.�����於�@=�@�����於 (                  ��������敪�ɉ����������於���擾                   => 0:�����XM�A1:������Ѓ}�X�^�A2:�c�Ə��}�X�^
            '                  (1) ������}�X�^.������敪 = 0 : �����X
            If sender.IndexOf("Hansoku") > 0 Then
                Me.ddlSeikyusakiKbn_Hansoku.SelectedValue = SKU_datatable.Rows(0).Item(2).ToString
                Me.tbxSeikyusakiCode_Hansoku.Text = SKU_datatable.Rows(0).Item(0).ToString
                Me.tbxSeikyusakiEdaban_Hansoku.Text = SKU_datatable.Rows(0).Item(1).ToString

                SKU_KBN = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
                SKU_CD = Me.tbxSeikyusakiCode_Hansoku.Text
                SKU_BRC = Me.tbxSeikyusakiEdaban_Hansoku.Text
            End If
            If sender.IndexOf("Kouj") > 0 Then
                Me.ddlSeikyusakiKbn_Kouj.SelectedValue = SKU_datatable.Rows(0).Item(2).ToString
                Me.tbxSeikyusakiCode_Kouj.Text = SKU_datatable.Rows(0).Item(0).ToString
                Me.tbxSeikyusakiEdaban_Kouj.Text = SKU_datatable.Rows(0).Item(1).ToString

                SKU_KBN = Me.ddlSeikyusakiKbn_Kouj.SelectedValue
                SKU_CD = Me.tbxSeikyusakiCode_Kouj.Text
                SKU_BRC = Me.tbxSeikyusakiEdaban_Kouj.Text
            End If
            If sender.IndexOf("Tatemono") > 0 Then
                Me.ddlSeikyusakiKbn_Tatemono.SelectedValue = SKU_datatable.Rows(0).Item(2).ToString
                Me.tbxSeikyusakiCode_Tatemono.Text = SKU_datatable.Rows(0).Item(0).ToString
                Me.tbxSeikyusakiEdaban_Tatemono.Text = SKU_datatable.Rows(0).Item(1).ToString

                SKU_KBN = Me.ddlSeikyusakiKbn_Tatemono.SelectedValue
                SKU_CD = Me.tbxSeikyusakiCode_Tatemono.Text
                SKU_BRC = Me.tbxSeikyusakiEdaban_Tatemono.Text
            End If
            If sender.IndexOf("Tys") > 0 Then
                Me.ddlSeikyusakiKbn_Tys.SelectedValue = SKU_datatable.Rows(0).Item(2).ToString
                Me.tbxSeikyusakiCode_Tys.Text = SKU_datatable.Rows(0).Item(0).ToString
                Me.tbxSeikyusakiEdaban_Tys.Text = SKU_datatable.Rows(0).Item(1).ToString

                SKU_KBN = Me.ddlSeikyusakiKbn_Tys.SelectedValue
                SKU_CD = Me.tbxSeikyusakiCode_Tys.Text
                SKU_BRC = Me.tbxSeikyusakiEdaban_Tys.Text
            End If

            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            If sender.IndexOf("Sk5") > 0 Then
                Me.ddlSeikyusakiKbn_Sk5.SelectedValue = SKU_datatable.Rows(0).Item(2).ToString
                Me.tbxSeikyusakiCode_Sk5.Text = SKU_datatable.Rows(0).Item(0).ToString
                Me.tbxSeikyusakiEdaban_Sk5.Text = SKU_datatable.Rows(0).Item(1).ToString

                SKU_KBN = Me.ddlSeikyusakiKbn_Sk5.SelectedValue
                SKU_CD = Me.tbxSeikyusakiCode_Sk5.Text
                SKU_BRC = Me.tbxSeikyusakiEdaban_Sk5.Text
            End If

            If sender.IndexOf("Sk6") > 0 Then
                Me.ddlSeikyusakiKbn_Sk6.SelectedValue = SKU_datatable.Rows(0).Item(2).ToString
                Me.tbxSeikyusakiCode_Sk6.Text = SKU_datatable.Rows(0).Item(0).ToString
                Me.tbxSeikyusakiEdaban_Sk6.Text = SKU_datatable.Rows(0).Item(1).ToString

                SKU_KBN = Me.ddlSeikyusakiKbn_Sk6.SelectedValue
                SKU_CD = Me.tbxSeikyusakiCode_Sk6.Text
                SKU_BRC = Me.tbxSeikyusakiEdaban_Sk6.Text
            End If

            If sender.IndexOf("Sk7") > 0 Then
                Me.ddlSeikyusakiKbn_Sk7.SelectedValue = SKU_datatable.Rows(0).Item(2).ToString
                Me.tbxSeikyusakiCode_Sk7.Text = SKU_datatable.Rows(0).Item(0).ToString
                Me.tbxSeikyusakiEdaban_Sk7.Text = SKU_datatable.Rows(0).Item(1).ToString

                SKU_KBN = Me.ddlSeikyusakiKbn_Sk7.SelectedValue
                SKU_CD = Me.tbxSeikyusakiCode_Sk7.Text
                SKU_BRC = Me.tbxSeikyusakiEdaban_Sk7.Text
            End If
            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            If SKU_KBN = "0" Then
                Dim kameitenTBL As New KameitenDataSet.m_kameitenTableDataTable
                '                        �A���.������R�[�h = �����X�}�X�^.�����X�R�[�h�ŉ����X�}�X�^���Q�Ƃ�
                kameitenTBL = KihonJyouhouBL.GetKameiten(SKU_CD)
                '                        �����X�}�X�^.�������̂��擾
                If kameitenTBL.Rows.Count > 0 Then
                    SKU_NM_OBJ.Text = kameitenTBL.Rows(0).Item("kameiten_seisiki_mei").ToString
                Else
                    SKU_NM_OBJ.Text = ""
                End If
            End If

            '                  (2) ������}�X�^.������敪 = 1 : �������
            If SKU_KBN = "1" Then
                Dim tyousakaisyaTBL As New Data.DataTable
                '                        �A���.������R�[�h = ������Ѓ}�X�^.������ЃR�[�h
                '                       AND �B��ʂ̐�����}�� = ������Ѓ}�X�^.���Ə��R�[�h�Œ�����Ѓ}�X�^���Q�Ƃ�
                tyousakaisyaTBL = KihonJyouhouBL.GetTyousakaisya(SKU_CD, SKU_BRC, -1)
                '                        ������Ѓ}�X�^.������x���於���擾
                If tyousakaisyaTBL.Rows.Count > 0 Then
                    SKU_NM_OBJ.Text = tyousakaisyaTBL.Rows(0).Item("seikyuu_saki_shri_saki_mei").ToString
                Else
                    SKU_NM_OBJ.Text = ""
                End If
            End If

            '                  (3) ������}�X�^.������敪 = 2 : �c�Ə�
            If SKU_KBN = "2" Then
                Dim eigyousyoTBL As New Data.DataTable
                '                        �A���.������R�[�h = �c�Ə��}�X�^.�c�Ə��R�[�h�ŉc�Ə��}�X�^���Q�Ƃ�
                eigyousyoTBL = KihonJyouhouBL.GetEigyousyo(SKU_CD, -1)
                '                        �c�Ə��}�X�^.�����於���擾
                If eigyousyoTBL.Rows.Count > 0 Then
                    SKU_NM_OBJ.Text = eigyousyoTBL.Rows(0).Item("seikyuu_saki_mei").ToString
                Else
                    SKU_NM_OBJ.Text = ""
                End If
            End If

            '                              )
            '            �D���.���ߓ��@=�@������}�X�^.�������ߓ� + "��"
            If SKU_datatable.Rows(0).Item("seikyuu_sime_date").ToString.Trim <> String.Empty Then
                SKU_SIMEBI_OBJ.Text = "���ߓ��F" & SKU_datatable.Rows(0).Item("seikyuu_sime_date").ToString & "��"
            Else
                SKU_SIMEBI_OBJ.Text = ""
            End If

            '++__++__
            HID_SKU_KBN_OBJ.Value = SKU_KBN_OBJ.Text
            HID_SKU_CD_OBJ.Value = SKU_CD_OBJ.Text
            HID_SKU_BRC_OBJ.Value = SKU_BRC_OBJ.Text
            HID_SKU_KAKUN_OBJ.Value = ""

        Else

            '      �E��L�ȊO

            '            �E   �@���.������敪 = 0 �F�����X
            '            ���� �A���.������R�[�h = ���.�����X�R�[�h
            '            ���� �B���.������}�� <> �󔒂������ꍇ
            If SKU_KBN = "0" AndAlso SKU_CD = Me.kameiten_cd AndAlso SKU_BRC.Trim <> "" Then

                Dim SKU_SYURUI As String = ""

                If sender.IndexOf("Hansoku") > 0 Then
                    SKU_SYURUI = "HAN"
                End If

                If sender.IndexOf("Kouj") > 0 Then
                    SKU_SYURUI = "KOU"
                End If

                If sender.IndexOf("Tatemono") > 0 Then
                    SKU_SYURUI = "TAT"
                End If

                If sender.IndexOf("Tys") > 0 Then
                    SKU_SYURUI = "TYS"
                End If

                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                If sender.IndexOf("Sk5") > 0 Then
                    SKU_SYURUI = "SK5"
                End If
                If sender.IndexOf("Sk6") > 0 Then
                    SKU_SYURUI = "SK6"
                End If
                If sender.IndexOf("Sk7") > 0 Then
                    SKU_SYURUI = "SK7"
                End If
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                '                  �E�u���̓��e�̓o�^���ɐ�����}�X�^�ɓo�^���܂����H�v�\��
                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "confrim_kakaku", "if(confirm('���̓��e�̓o�^���ɐ�����}�X�^�ɓo�^���܂����H')){objEBI('" & Me.hidConfrim.ClientID & "').value = '1_" & SKU_SYURUI & SKU_BRC & "';}else{objEBI('" & Me.hidConfrim.ClientID & "').value = '2_" & SKU_SYURUI & SKU_BRC & "';}; window.setTimeout('objEBI(\'" & Me.btnConfirm.ClientID & "\').click()',1000); ", True)

                Exit Sub

                ''''
                '�y�[�W���ēx���[�h������Apageload�ɒǉ����܂�
                ''''

            Else

                '            �E��L�ȊO
                '                  �����挟����ʂ�ʃE�B���h�E�ŋN��
                '                  �E�����挟����ʂ̌�������.������敪�ɂ͉��.������敪�̒l���Z�b�g
                '                  �E�����挟����ʂ̌�������.������R�[�h�ɂ͉��.������R�[�h�̒l���Z�b�g
                '                  �E�����挟����ʂ̌�������.������}�Ԃɂ͉��.������}�Ԃ̒l���Z�b�g
                '                  �E�����挟����ʂŌ����A�I���������ʂ���ʂɃZ�b�g
                '                        �@���.������敪�@=�@�����挟����ʑI������.������敪
                '                        �A���.������R�[�h�@=�@�����挟����ʑI������.������R�[�h
                '                        �B���.������}�ԁ@=�@�����挟����ʑI������.������}��
                '                        �C���.�����於�@=�@�����於 (                  ��������敪�ɉ����������於���擾
                '                              (1) ������}�X�^.������敪 = 0 : �����X
                '                                    �A���.������R�[�h = �����X�}�X�^.�����X�R�[�h�ŉ����X�}�X�^���Q�Ƃ�
                '                                    �����X�}�X�^.�������̂��擾
                '                              (2) ������}�X�^.������敪 = 1 : �������
                '                                    �A���.������R�[�h = ������Ѓ}�X�^.������ЃR�[�h AND �B��ʂ̐�����}�� = ������Ѓ}�X�^.���Ə��R�[�h�Œ�����Ѓ}�X�^���Q�Ƃ�
                '                                    ������Ѓ}�X�^.������x���於���擾
                '                              (3) ������}�X�^.������敪 = 2 : �c�Ə�
                '                                    �A���.������R�[�h = �c�Ə��}�X�^.�c�Ə��R�[�h�ŉc�Ə��}�X�^���Q�Ƃ�
                '                                    �c�Ə��}�X�^.�����於���擾
                '                                          )
                'Dim SKU_KBN As String = ""
                'Dim SKU_CD As String = ""
                'Dim SKU_BRC As String = ""

                'Dim SKU_KBN_OBJ As New DropDownList
                'Dim SKU_CD_OBJ As New TextBox
                'Dim SKU_BRC_OBJ As New TextBox
                'Dim SKU_NM_OBJ As New TextBox
                'Dim SKU_SIMEBI_OBJ As New TextBox

                'HID_SKU_KBN_OBJ = Me.hid_kbn_TYS
                'HID_SKU_CD_OBJ = Me.hid_cd_TYS
                'HID_SKU_BRC_OBJ = Me.hid_brc_TYS
                'HID_SKU_KAKUN_OBJ = Me.hid_kakunin_TYS


                Dim strScript As String
                'strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&Kbn='+escape('������')+'&FormName=" & _
                '                 Me.Page.Form.Name & "&objKbn=" & _
                '                 SKU_KBN_OBJ.ClientID & _
                '                 "&objCd=" & SKU_CD_OBJ.ClientID & _
                '                 "&objBrc=" & SKU_BRC_OBJ.ClientID & _
                '                 "&objMei=" & SKU_NM_OBJ.ClientID & _
                '                 "&strKbn='+escape(eval('document.all.'+'" & _
                '                 SKU_KBN_OBJ.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                '                 SKU_CD_OBJ.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                '                 SKU_BRC_OBJ.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

                'ScriptManager.RegisterStartupScript(Me.Parent.Page , Me.Parent.Page.GetType(), "popup_fire", strScript, True)


                strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&Kbn='+escape('������')+'&FormName=" & _
                             Me.Page.Form.Name & "&objKbn=" & _
                             SKU_KBN_OBJ.ClientID & _
                             "&objHidKbn=" & HID_SKU_KBN_OBJ.ClientID & _
                             "&objCd=" & SKU_CD_OBJ.ClientID & _
                             "&objHidCd=" & HID_SKU_CD_OBJ.ClientID & _
                             "&hidConfirm2=" & HID_SKU_KAKUN_OBJ.ClientID & _
                             "&objBrc=" & SKU_BRC_OBJ.ClientID & _
                             "&objHidBrc=" & HID_SKU_BRC_OBJ.ClientID & _
                             "&objMei=" & SKU_NM_OBJ.ClientID & _
                             "&objDate=" & SKU_SIMEBI_OBJ.ClientID & _
                             "&strKbn='+escape(eval('document.all.'+'" & _
                             SKU_KBN_OBJ.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                             SKU_CD_OBJ.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                             SKU_BRC_OBJ.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "popup_fire", strScript, True)

            End If


        End If


    End Sub

    ''' <summary>
    ''' ��{�Z�b�g�@�{�^���@����@�ꍇ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KihonSetEvent()

        Me.kyoutu_kbn = Me.hidKyoutuKubun.Value
        Me.eigyousyo_cd = Me.hidKyoutuEigyousyoCode.Value
        '==================2015/09/27 �C�� �J�n��==========================
        ''==================2015/03/02 ���h�m �C�� �J�n��==========================
        'Dim fixEigyousyoCd As String
        'If eigyousyo_cd.Length >= 2 Then
        '    fixEigyousyoCd = Me.eigyousyo_cd.Substring(0, 2)
        'Else
        '    fixEigyousyoCd = String.Empty
        'End If
        ''If Me.kyoutu_kbn.Trim.ToUpper = "A" OrElse Me.kyoutu_kbn.Trim.ToUpper = "C" Then
        ''(��ʁD�敪="A" or "C") ���� ��ʁD�c�Ə��R�[�h���hBF�h�Ŏn�܂�l�ł͂Ȃ���
        'If (Me.kyoutu_kbn.Trim.ToUpper = "A" OrElse Me.kyoutu_kbn.Trim.ToUpper = "C") _
        '   AndAlso fixEigyousyoCd <> "BF" Then
        '    '==================2015/03/02 ���h�m �C�� �I����==========================
        If Me.kyoutu_kbn.Trim.ToUpper = "A" OrElse Me.kyoutu_kbn.Trim.ToUpper = "C" Then
            '==================2015/09/27 �C�� �I����==========================
            KihonSetEvent_A()

        Else

            KihonSetEvent_B()

        End If

    End Sub

    ''' <summary>
    ''' ��{�Z�b�g�@�{�^���@����@�ꍇ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KihonSetEvent_A()

        If Me.eigyousyo_cd.Trim = "" Then
            'SHOW MSG RETURN
            '�E���.�c�Ə��R�[�h = �󔒂̂Ƃ��@�ˁ@�u�c�Ə��R�[�h���Z�b�g����Ă��܂���v�\�����A�������f

            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_kihonSet_A", "alert('�c�Ə��R�[�h���Z�b�g����Ă��܂���B');", True)

        Else

            '�c�Ə��}�X�^�A������}�X�^����������	

            Dim EigyouDatatable As Data.DataTable
            EigyouDatatable = KihonJyouhouBL.GetEigyousyoForSeikyusaki(Me.eigyousyo_cd)

            If EigyouDatatable.Rows.Count = 0 Then
                '		�E�Y�������̂Ƃ�	
                '			�u���͂���Ă���c�Ə��R�[�h�͑��݂��܂���v�\�����A�������f
                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_kihonSet_A0", "alert('���͂���Ă���c�Ə��R�[�h�͑��݂��܂���B');", True)

            Else
                '		���L�ȊO	
                '			�@���.������敪 = �c�D������敪
                '			�A���.������R�[�h = �c�D������R�[�h
                '			�B���.������}�� = �c�D������}��
                '			�C���.�����於 = �c�D�����於
                '			�D���.�������ߓ� = ���D�������ߓ��i������}�X�^�ɑ��݂��Ȃ��ꍇ�͋󔒂ɂȂ�j

                Me.ddlSeikyusakiKbn_Hansoku.SelectedValue = EigyouDatatable.Rows(0).Item("seikyuu_saki_kbn").ToString
                Me.ddlSeikyusakiKbn_Kouj.SelectedValue = EigyouDatatable.Rows(0).Item("seikyuu_saki_kbn").ToString
                Me.ddlSeikyusakiKbn_Tatemono.SelectedValue = EigyouDatatable.Rows(0).Item("seikyuu_saki_kbn").ToString
                Me.ddlSeikyusakiKbn_Tys.SelectedValue = EigyouDatatable.Rows(0).Item("seikyuu_saki_kbn").ToString
                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                Me.ddlSeikyusakiKbn_Sk5.SelectedValue = EigyouDatatable.Rows(0).Item("seikyuu_saki_kbn").ToString
                Me.ddlSeikyusakiKbn_Sk6.SelectedValue = EigyouDatatable.Rows(0).Item("seikyuu_saki_kbn").ToString
                Me.ddlSeikyusakiKbn_Sk7.SelectedValue = EigyouDatatable.Rows(0).Item("seikyuu_saki_kbn").ToString
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                Me.tbxSeikyusakiCode_Hansoku.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_cd").ToString
                Me.tbxSeikyusakiCode_Kouj.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_cd").ToString
                Me.tbxSeikyusakiCode_Tatemono.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_cd").ToString
                Me.tbxSeikyusakiCode_Tys.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_cd").ToString
                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                Me.tbxSeikyusakiCode_Sk5.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_cd").ToString
                Me.tbxSeikyusakiCode_Sk6.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_cd").ToString
                Me.tbxSeikyusakiCode_Sk7.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_cd").ToString
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                Me.tbxSeikyusakiEdaban_Hansoku.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_brc").ToString
                Me.tbxSeikyusakiEdaban_Kouj.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_brc").ToString
                Me.tbxSeikyusakiEdaban_Tatemono.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_brc").ToString
                Me.tbxSeikyusakiEdaban_Tys.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_brc").ToString
                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                Me.tbxSeikyusakiEdaban_Sk5.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_brc").ToString
                '====================��2016/01/29 chel1 �C����====================
                'Me.tbxSeikyusakiEdaban_Sk6.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_brc").ToString
                If Not Me.ddlSeikyusakiKbn_Sk6.SelectedValue.Equals(String.Empty) Then
                    Me.tbxSeikyusakiEdaban_Sk6.Text = "30" '�ԍ��Œ�
                Else
                    Me.tbxSeikyusakiEdaban_Sk6.Text = String.Empty
                End If
                '====================��2016/01/29 chel1 �C����====================
                Me.tbxSeikyusakiEdaban_Sk7.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_brc").ToString
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                Me.lblSeikyusaki_name_Hansoku.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_mei").ToString
                Me.lblSeikyusaki_name_Kouj.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_mei").ToString
                Me.lblSeikyusaki_name_Tatemono.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_mei").ToString
                Me.lblSeikyusaki_name_Tys.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_mei").ToString
                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                Me.lblSeikyusaki_name_Sk5.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_mei").ToString
                Me.lblSeikyusaki_name_Sk6.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_mei").ToString
                Me.lblSeikyusaki_name_Sk7.Text = EigyouDatatable.Rows(0).Item("seikyuu_saki_mei").ToString
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                Dim simibi_A As String = EigyouDatatable.Rows(0).Item("seikyuu_sime_date").ToString

                If simibi_A.Trim = "" Then
                Else
                    simibi_A = "���ߓ��F" & simibi_A & "��"
                End If

                Me.lblSeikyusaki_simebi_Hansoku.Text = simibi_A
                Me.lblSeikyusaki_simebi_Kouj.Text = simibi_A
                Me.lblSeikyusaki_simebi_Tatemono.Text = simibi_A
                Me.lblSeikyusaki_simebi_Tys.Text = simibi_A
                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                Me.lblSeikyusaki_simebi_Sk5.Text = simibi_A
                '====================��2016/01/29 chel1 �폜��====================
                'Me.lblSeikyusaki_simebi_Sk6.Text = simibi_A
                '(�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���������)
                '====================��2016/01/29 chel1 �폜��====================
                Me.lblSeikyusaki_simebi_Sk7.Text = simibi_A
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                '====================��2016/01/29 chel1 �ǉ���====================
                '�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���
                Dim simibi_brc30 As String = EigyouDatatable.Rows(0).Item("seikyuu_sime_date_brc30").ToString
                If simibi_brc30.Trim = "" Then
                Else
                    simibi_brc30 = "���ߓ��F" & simibi_brc30 & "��"
                End If
                Me.lblSeikyusaki_simebi_Sk6.Text = simibi_brc30
                '====================��2016/01/29 chel1 �ǉ���====================

                'MAKE HID VALUE GOTO START
                Call hidvalueToStart()

            End If

        End If

    End Sub

    ''' <summary>
    ''' ��{�Z�b�g�@�{�^���@����@�ꍇ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KihonSetEvent_B()

        Me.seisiki_nm = Me.hidSeisikiMei.Value
        Me.kameiten_nm1 = Me.hidKyoutuKameitenMei1.Value

        Dim KihonDatatable As Data.DataTable
        KihonDatatable = KihonJyouhouBL.GetKihonsetData(_kameiten_cd)

        '====================��2016/01/29 chel1 �ǉ���====================
        Dim KihonDatatableBrc30 As Data.DataTable
        KihonDatatableBrc30 = KihonJyouhouBL.GetKihonsetDataBrc30(_kameiten_cd)
        '====================��2016/01/29 chel1 �ǉ���====================

        '====================��2016/01/29 chel1 �C����====================
        'If KihonDatatable.Rows.Count > 0 Then
        If (KihonDatatable.Rows.Count > 0) AndAlso (KihonDatatableBrc30.Rows.Count > 0) Then
            '================��2016/01/29 chel1 �C����====================
            '�E�Y������̂Ƃ����L���e�ŏ㏑������()

            '�@���.������敪 = 0
            Me.ddlSeikyusakiKbn_Hansoku.SelectedValue = "0"
            Me.ddlSeikyusakiKbn_Kouj.SelectedValue = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
            Me.ddlSeikyusakiKbn_Tatemono.SelectedValue = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
            Me.ddlSeikyusakiKbn_Tys.SelectedValue = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            Me.ddlSeikyusakiKbn_Sk5.SelectedValue = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
            Me.ddlSeikyusakiKbn_Sk6.SelectedValue = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
            Me.ddlSeikyusakiKbn_Sk7.SelectedValue = Me.ddlSeikyusakiKbn_Hansoku.SelectedValue
            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            '�A���.������R�[�h = ���.�����X�R�[�h
            Me.tbxSeikyusakiCode_Hansoku.Text = Me.kameiten_cd
            Me.tbxSeikyusakiCode_Kouj.Text = Me.tbxSeikyusakiCode_Hansoku.Text
            Me.tbxSeikyusakiCode_Tatemono.Text = Me.tbxSeikyusakiCode_Hansoku.Text
            Me.tbxSeikyusakiCode_Tys.Text = Me.tbxSeikyusakiCode_Hansoku.Text
            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            Me.tbxSeikyusakiCode_Sk5.Text = Me.tbxSeikyusakiCode_Hansoku.Text
            Me.tbxSeikyusakiCode_Sk6.Text = Me.tbxSeikyusakiCode_Hansoku.Text
            Me.tbxSeikyusakiCode_Sk7.Text = Me.tbxSeikyusakiCode_Hansoku.Text
            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            '�B���.������}�� = ������o�^���`�}�X�^�D������}��
            Me.tbxSeikyusakiEdaban_Hansoku.Text = KihonDatatable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyusakiEdaban_Kouj.Text = Me.tbxSeikyusakiEdaban_Hansoku.Text
            Me.tbxSeikyusakiEdaban_Tatemono.Text = Me.tbxSeikyusakiEdaban_Hansoku.Text
            Me.tbxSeikyusakiEdaban_Tys.Text = Me.tbxSeikyusakiEdaban_Hansoku.Text
            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            Me.tbxSeikyusakiEdaban_Sk5.Text = Me.tbxSeikyusakiEdaban_Hansoku.Text
            '====================��2016/01/29 chel1 �폜��====================
            'Me.tbxSeikyusakiEdaban_Sk6.Text = Me.tbxSeikyusakiEdaban_Hansoku.Text
            '(�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���������)
            '====================��2016/01/29 chel1 �폜��====================
            Me.tbxSeikyusakiEdaban_Sk7.Text = Me.tbxSeikyusakiEdaban_Hansoku.Text
            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            '�E��.������敪 = Null�̂Ƃ�
            If KihonDatatable.Rows(0).Item("seikyuu_saki_kbn").Equals(DBNull.Value) Then
                '	�C���.�����於 = "�y������V�K�o�^�z" +���D�\�����e
                Me.lblSeikyusaki_name_Hansoku.Text = "�y������V�K�o�^�z" & KihonDatatable.Rows(0).Item("hyouji_naiyou").ToString
                Me.lblSeikyusaki_name_Kouj.Text = Me.lblSeikyusaki_name_Hansoku.Text
                Me.lblSeikyusaki_name_Tatemono.Text = Me.lblSeikyusaki_name_Hansoku.Text
                Me.lblSeikyusaki_name_Tys.Text = Me.lblSeikyusaki_name_Hansoku.Text
                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                Me.lblSeikyusaki_name_Sk5.Text = Me.lblSeikyusaki_name_Hansoku.Text
                '====================��2016/01/29 chel1 �폜��====================
                'Me.lblSeikyusaki_name_Sk6.Text = Me.lblSeikyusaki_name_Hansoku.Text
                '(�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���������)
                '====================��2016/01/29 chel1 �폜��====================
                Me.lblSeikyusaki_name_Sk7.Text = Me.lblSeikyusaki_name_Hansoku.Text
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                '	�D���.�������ߓ� = ���D�������ߓ�
                Me.lblSeikyusaki_simebi_Hansoku.Text = "���ߓ��F" & KihonDatatable.Rows(0).Item("HINA_sime_date").ToString & "��"
                Me.lblSeikyusaki_simebi_Kouj.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                Me.lblSeikyusaki_simebi_Tatemono.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                Me.lblSeikyusaki_simebi_Tys.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                Me.lblSeikyusaki_simebi_Sk5.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                '====================��2016/01/29 chel1 �폜��====================
                'Me.lblSeikyusaki_simebi_Sk6.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                '(�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���������)
                '====================��2016/01/29 chel1 �폜��====================
                Me.lblSeikyusaki_simebi_Sk7.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

            Else
                '�E��L�ȊO()
                '	�D���.�������ߓ� = ���D�������ߓ�
                Me.lblSeikyusaki_simebi_Hansoku.Text = "���ߓ��F" & KihonDatatable.Rows(0).Item("SKU_sime_date").ToString & "��"
                Me.lblSeikyusaki_simebi_Kouj.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                Me.lblSeikyusaki_simebi_Tatemono.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                Me.lblSeikyusaki_simebi_Tys.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                Me.lblSeikyusaki_simebi_Sk5.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                '====================��2016/01/29 chel1 �폜��====================
                'Me.lblSeikyusaki_simebi_Sk6.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                '(�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���������)
                '====================��2016/01/29 chel1 �폜��====================
                Me.lblSeikyusaki_simebi_Sk7.Text = Me.lblSeikyusaki_simebi_Hansoku.Text
                '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                '	�E��ʁD�������� <> �󔒂�
                If Me.seisiki_nm.ToString.Trim <> "" Then
                    '		�C���.�����於 = ���D��������
                    Me.lblSeikyusaki_name_Hansoku.Text = Me.seisiki_nm
                    Me.lblSeikyusaki_name_Kouj.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    Me.lblSeikyusaki_name_Tatemono.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    Me.lblSeikyusaki_name_Tys.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                    Me.lblSeikyusaki_name_Sk5.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    '====================��2016/01/29 chel1 �폜��====================
                    'Me.lblSeikyusaki_name_Sk6.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    '(�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���������)
                    '====================��2016/01/29 chel1 �폜��====================
                    Me.lblSeikyusaki_name_Sk7.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                Else
                    '   �E��L�ȊO()
                    '		�C���.�����於 = ���D�����X���P
                    Me.lblSeikyusaki_name_Hansoku.Text = Me.kameiten_nm1
                    Me.lblSeikyusaki_name_Kouj.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    Me.lblSeikyusaki_name_Tatemono.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    Me.lblSeikyusaki_name_Tys.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
                    Me.lblSeikyusaki_name_Sk5.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    '====================��2016/01/29 chel1 �폜��====================
                    'Me.lblSeikyusaki_name_Sk6.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    '(�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���������)
                    '====================��2016/01/29 chel1 �폜��====================
                    Me.lblSeikyusaki_name_Sk7.Text = Me.lblSeikyusaki_name_Hansoku.Text
                    '==================2011/06/28 �ԗ� �ǉ� �I����==========================

                End If

            End If

            '====================��2016/01/29 chel1 �ǉ���====================
            '�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���
            Me.tbxSeikyusakiEdaban_Sk6.Text = "30"

            If KihonDatatableBrc30.Rows.Count > 0 Then
                '�E��.������敪 = Null�̂Ƃ�
                If KihonDatatableBrc30.Rows(0).Item("seikyuu_saki_kbn").Equals(DBNull.Value) Then
                    Me.lblSeikyusaki_name_Sk6.Text = "�y������V�K�o�^�z" & KihonDatatableBrc30.Rows(0).Item("hyouji_naiyou").ToString
                    Me.lblSeikyusaki_simebi_Sk6.Text = "���ߓ��F" & KihonDatatableBrc30.Rows(0).Item("HINA_sime_date").ToString & "��"
                Else
                    '�E��L�ȊO()
                    Me.lblSeikyusaki_simebi_Sk6.Text = "���ߓ��F" & KihonDatatableBrc30.Rows(0).Item("SKU_sime_date").ToString & "��"

                    '	�E��ʁD�������� <> �󔒂�
                    If Me.seisiki_nm.ToString.Trim <> "" Then
                        Me.lblSeikyusaki_name_Sk6.Text = Me.seisiki_nm
                    Else
                        '   �E��L�ȊO()
                        Me.lblSeikyusaki_name_Sk6.Text = Me.kameiten_nm1
                    End If
                End If

            Else
                '�}�ԁu30�v�̃f�[�^���Ȃ��ꍇ
                Me.lblSeikyusaki_simebi_Sk6.Text = ""
                Me.lblSeikyusaki_name_Sk6.Text = ""
            End If
            '====================��2016/01/29 chel1 �ǉ���====================



            'MAKE HID VALUE GOTO START
            Call hidvalueToStart()

        Else

            ' �E��L�ȊO()
            '�u������o�^���`�}�X�^�Ɋ�{�t���O�������Ă���f�[�^������܂���v�\�����A�������f						
            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_kihonSet", "alert('������o�^���`�}�X�^�Ɋ�{�t���O�������Ă���f�[�^������܂���');", True)

        End If

    End Sub

    ''' <summary>
    ''' '��{�Z�b�g�@�g�p�@���f
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KihonSetABO()

        Dim ABO As Boolean = True

        '������敪�@��
        If Me.ddlSeikyusakiKbn_Tys.Text <> "" Then
            ABO = False
        End If
        If Me.ddlSeikyusakiKbn_Kouj.Text <> "" Then
            ABO = False
        End If
        If Me.ddlSeikyusakiKbn_Hansoku.Text <> "" Then
            ABO = False
        End If
        If Me.ddlSeikyusakiKbn_Tatemono.Text <> "" Then
            ABO = False
        End If
        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        If Me.ddlSeikyusakiKbn_Sk5.Text <> "" Then
            ABO = False
        End If
        If Me.ddlSeikyusakiKbn_Sk6.Text <> "" Then
            ABO = False
        End If
        If Me.ddlSeikyusakiKbn_Sk7.Text <> "" Then
            ABO = False
        End If
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================

        '������R�[�h�@��
        If Me.tbxSeikyusakiCode_Tys.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiCode_Kouj.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiEdaban_Hansoku.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiEdaban_Tatemono.Text <> "" Then
            ABO = False
        End If
        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        If Me.tbxSeikyusakiCode_Sk5.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiCode_Sk6.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiCode_Sk7.Text <> "" Then
            ABO = False
        End If
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================

        '������}�ԁ@��
        If Me.tbxSeikyusakiEdaban_Tys.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiEdaban_Kouj.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiEdaban_Hansoku.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiEdaban_Tatemono.Text <> "" Then
            ABO = False
        End If
        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        If Me.tbxSeikyusakiEdaban_Sk5.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiEdaban_Sk6.Text <> "" Then
            ABO = False
        End If
        If Me.tbxSeikyusakiEdaban_Sk7.Text <> "" Then
            ABO = False
        End If
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================

        Me.btnKihonSet.Enabled = ABO

    End Sub

    ''' <summary>
    ''' ���ڃf�[�^�ɃR�}��ǉ�
    ''' </summary>
    ''' <param name="kekka">���z</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function AddComa(ByVal kekka As String) As String
        Dim i As Int16

        For i = 1 To kekka.Length - 1
            If (kekka.Length - i) Mod 3 = 0 Then
                kekka = kekka.Substring(0, i) & "," & kekka.Substring(i, kekka.Length - i)
                i = i + 1
            End If
        Next

        Return kekka
    End Function

    ''' <summary>
    ''' ������敪�����ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SeikyusakiKBNStart()

        Dim Syubetu As String = "1"
        Dim tblSeikyusakiKBN As Data.DataTable
        tblSeikyusakiKBN = KihonJyouhouBL.GetSeikyusakuKBN(Syubetu)

        If tblSeikyusakiKBN.Rows.Count > 0 Then

            Me.ddlSeikyusakiKbn_Hansoku.DataSource = tblSeikyusakiKBN
            Me.ddlSeikyusakiKbn_Hansoku.DataTextField = "meisyou"
            Me.ddlSeikyusakiKbn_Hansoku.DataValueField = "code"
            Me.ddlSeikyusakiKbn_Hansoku.DataBind()
            Me.ddlSeikyusakiKbn_Hansoku.Items.Insert(0, New ListItem("", ""))

            Me.ddlSeikyusakiKbn_Kouj.DataSource = tblSeikyusakiKBN
            Me.ddlSeikyusakiKbn_Kouj.DataTextField = "meisyou"
            Me.ddlSeikyusakiKbn_Kouj.DataValueField = "code"
            Me.ddlSeikyusakiKbn_Kouj.DataBind()
            Me.ddlSeikyusakiKbn_Kouj.Items.Insert(0, New ListItem("", ""))

            Me.ddlSeikyusakiKbn_Tatemono.DataSource = tblSeikyusakiKBN
            Me.ddlSeikyusakiKbn_Tatemono.DataTextField = "meisyou"
            Me.ddlSeikyusakiKbn_Tatemono.DataValueField = "code"
            Me.ddlSeikyusakiKbn_Tatemono.DataBind()
            Me.ddlSeikyusakiKbn_Tatemono.Items.Insert(0, New ListItem("", ""))

            Me.ddlSeikyusakiKbn_Tys.DataSource = tblSeikyusakiKBN
            Me.ddlSeikyusakiKbn_Tys.DataTextField = "meisyou"
            Me.ddlSeikyusakiKbn_Tys.DataValueField = "code"
            Me.ddlSeikyusakiKbn_Tys.DataBind()
            Me.ddlSeikyusakiKbn_Tys.Items.Insert(0, New ListItem("", ""))

            '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
            Me.ddlSeikyusakiKbn_Sk5.DataSource = tblSeikyusakiKBN
            Me.ddlSeikyusakiKbn_Sk5.DataTextField = "meisyou"
            Me.ddlSeikyusakiKbn_Sk5.DataValueField = "code"
            Me.ddlSeikyusakiKbn_Sk5.DataBind()
            Me.ddlSeikyusakiKbn_Sk5.Items.Insert(0, New ListItem("", ""))

            Me.ddlSeikyusakiKbn_Sk6.DataSource = tblSeikyusakiKBN
            Me.ddlSeikyusakiKbn_Sk6.DataTextField = "meisyou"
            Me.ddlSeikyusakiKbn_Sk6.DataValueField = "code"
            Me.ddlSeikyusakiKbn_Sk6.DataBind()
            Me.ddlSeikyusakiKbn_Sk6.Items.Insert(0, New ListItem("", ""))

            Me.ddlSeikyusakiKbn_Sk7.DataSource = tblSeikyusakiKBN
            Me.ddlSeikyusakiKbn_Sk7.DataTextField = "meisyou"
            Me.ddlSeikyusakiKbn_Sk7.DataValueField = "code"
            Me.ddlSeikyusakiKbn_Sk7.DataBind()
            Me.ddlSeikyusakiKbn_Sk7.Items.Insert(0, New ListItem("", ""))
            '==================2011/06/28 �ԗ� �ǉ� �I����==========================

        Else
            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_seikyusakiKBN", "alert('������敪�Z�b�g�A�b�v���鎞��" & Messages.Instance.MSG020E & "');", True)
        End If

    End Sub

    '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
    Public Sub SetSeikyuusakiKoumokuMei()

        '�����捀�ږ����擾����
        Dim dtKoumokuMei As New Data.DataTable
        dtKoumokuMei = KihonJyouhouBL.GetSeikyuusakiKoumokuMei()

        Me.tbxSeikyuusakiNaiyou1.Text = dtKoumokuMei.Rows(0).Item("meisyou").ToString.Trim
        Me.tbxSeikyuusakiNaiyou2.Text = dtKoumokuMei.Rows(1).Item("meisyou").ToString.Trim
        Me.tbxSeikyuusakiNaiyou3.Text = dtKoumokuMei.Rows(2).Item("meisyou").ToString.Trim
        Me.tbxSeikyuusakiNaiyou4.Text = dtKoumokuMei.Rows(3).Item("meisyou").ToString.Trim
        Me.tbxSeikyuusakiNaiyou5.Text = dtKoumokuMei.Rows(4).Item("meisyou").ToString.Trim
        Me.tbxSeikyuusakiNaiyou6.Text = dtKoumokuMei.Rows(5).Item("meisyou").ToString.Trim
        Me.tbxSeikyuusakiNaiyou7.Text = dtKoumokuMei.Rows(6).Item("meisyou").ToString.Trim

    End Sub
    '==================2011/06/28 �ԗ� �ǉ� �I����==========================

    ''' <summary>
    ''' ���i�R�[�h�����ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SyouhinStart()
        '==================2011/05/11 �ԗ� �����������\���ύX �C�� �J�n��==========================
        'Dim syouhinCd = ""
        ''   Dim syouhinNm = "�����ݒ�Ȃ�"
        'Me.tbxSyouhinCd1.Text = syouhinCd
        'Me.tbxSyouhinNm1.Text = "�ʏ튄���@-3,000�~/��(�ŕ�)"
        'Me.tbxSyouhinCd2.Text = syouhinCd
        'Me.tbxSyouhinNm2.Text = "�ʏ튄���@-4,000�~/��(�ŕ�)"
        'Me.tbxSyouhinCd3.Text = syouhinCd
        'Me.tbxSyouhinNm3.Text = "�ʏ튄���@-5,000�~/��(�ŕ�)"

        Dim syouhinCd = String.Empty
        '   Dim syouhinNm = "�����ݒ�Ȃ�"
        Me.tbxSyouhinCd1.Text = syouhinCd
        Me.tbxSyouhinNm1.Text = "�����ݒ�Ȃ�"
        Me.tbxMoney1.Text = String.Empty
        Me.tbxSyouhinCd2.Text = syouhinCd
        Me.tbxSyouhinNm2.Text = "�����ݒ�Ȃ�"
        Me.tbxMoney2.Text = String.Empty
        Me.tbxSyouhinCd3.Text = syouhinCd
        Me.tbxSyouhinNm3.Text = "�����ݒ�Ȃ�"
        Me.tbxMoney3.Text = String.Empty

        Dim kaimeitenCd As String
        Dim tatou As New DataTable

        kaimeitenCd = "AAAAA"
        tatou = KihonJyouhouBL.GetTatouKakaku(kaimeitenCd)

        Dim strHyoujunKkk As String
        For i As Integer = 0 To tatou.Rows.Count - 1
            If tatou.Rows(i).Item("toukubun") = 1 Then
                Me.tbxSyouhinNm1.Text = tatou.Rows(i).Item("syouhin_mei")

                strHyoujunKkk = tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim
                If strHyoujunKkk.Equals(String.Empty) Then
                    Me.tbxMoney1.Text = String.Empty
                Else
                    Me.tbxMoney1.Text = FormatNumber(strHyoujunKkk, 0) & "�~"
                End If
                'Me.tbxMoney1.Text = IIf(tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim.Equals(String.Empty), String.Empty, FormatNumber(tatou.Rows(i).Item("hyoujun_kkk"), 0) & "�~")
            End If

            If tatou.Rows(i).Item("toukubun") = 2 Then
                Me.tbxSyouhinNm2.Text = tatou.Rows(i).Item("syouhin_mei")

                strHyoujunKkk = tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim
                If strHyoujunKkk.Equals(String.Empty) Then
                    Me.tbxMoney2.Text = String.Empty
                Else
                    Me.tbxMoney2.Text = FormatNumber(strHyoujunKkk, 0) & "�~"
                End If
                'Me.tbxMoney2.Text = IIf(tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim.Equals(String.Empty), String.Empty, FormatNumber(tatou.Rows(i).Item("hyoujun_kkk"), 0) & "�~")
            End If

            If tatou.Rows(i).Item("toukubun") = 3 Then
                Me.tbxSyouhinNm3.Text = tatou.Rows(i).Item("syouhin_mei")

                strHyoujunKkk = tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim
                If strHyoujunKkk.Equals(String.Empty) Then
                    Me.tbxMoney3.Text = String.Empty
                Else
                    Me.tbxMoney3.Text = FormatNumber(strHyoujunKkk, 0) & "�~"
                End If
                'Me.tbxMoney3.Text = IIf(tatou.Rows(i).Item("hyoujun_kkk").ToString.Trim.Equals(String.Empty), String.Empty, FormatNumber(tatou.Rows(i).Item("hyoujun_kkk"), 0) & "�~")
            End If
        Next
        '==================2011/05/11 �ԗ� �����������\���ύX �C�� �I����==========================
    End Sub

    ''' <summary>
    ''' �o�^�{�^�����N���b�N���鎞
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '��ʓ��́@�`�F�b�N
        If PageItemInputCheck("�o�^") Then
        Else
            Exit Sub
        End If

        '������@�������@�`�F�b�N
        If SeikyusakiChangedCheck() Then
        Else
            Exit Sub
        End If

        Dim csScript As New StringBuilder
        Dim tourokuKekka As String
        Dim dtTatouHaita As KameitenDataSet.m_tatouwaribiki_setteiTableDataTable
        Dim errorSyouhinNo As String
        Dim CtlId As String

        '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
        'dtTatouHaita = ViewState.Item("tatou")
        dtTatouHaita = New KameitenDataSet.m_tatouwaribiki_setteiTableDataTable
        Dim dtTemp As New Data.DataTable
        dtTemp = CType(ViewState.Item("tatou"), Data.DataTable)
        Dim dr As Data.DataRow
        For i As Integer = 0 To dtTemp.Rows.Count - 1
            dr = dtTatouHaita.NewRow
            dr.Item("toukubun") = dtTemp.Rows(i).Item("toukubun")
            dr.Item("syouhin_cd") = dtTemp.Rows(i).Item("syouhin_cd")
            dr.Item("add_login_user_id") = dtTemp.Rows(i).Item("add_login_user_id")
            dr.Item("add_datetime") = dtTemp.Rows(i).Item("add_datetime")
            dr.Item("upd_login_user_id") = dtTemp.Rows(i).Item("upd_login_user_id")
            dr.Item("upd_datetime") = dtTemp.Rows(i).Item("upd_datetime")
            dr.Item("syouhin_mei") = dtTemp.Rows(i).Item("syouhin_mei")
            dtTatouHaita.Rows.Add(dr)
        Next
        '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================



        errorSyouhinNo = ""
        CtlId = ""

        Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction

        If Not otherPageFunction.DoFunction(Parent.Page, "Haitakameiten") Then
            Exit Sub
        End If

        '�o�^���� 
        tourokuKekka = KihonJyouhouBL.TourokuSyori(errorSyouhinNo, syouhinCdToArr, dtTatouHaita, makeAtatouTable, makeAkameitenTable, makeAseikyuTable)

        '�ŐV�X�V����---�X�V
        If InStr(tourokuKekka, Messages.Instance.MSG018S.ToString.Substring(7)) Then
            otherPageFunction.DoFunction(Parent.Page, "SetKyoutuuKousin")
        End If

        If errorSyouhinNo <> "" Then
            Select Case errorSyouhinNo
                Case "1"
                    CtlId = tbxSyouhinCd1.ClientID.ToString
                Case "2"
                    CtlId = tbxSyouhinCd2.ClientID.ToString
                Case "3"
                    CtlId = tbxSyouhinCd3.ClientID.ToString
            End Select

            If CtlId <> "" Then
                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_touroku1", csScript.Append("alert('" & tourokuKekka & "');objEBI('" & CtlId & "').select();").ToString, True)
            End If

        Else
            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_touroku12", csScript.Append("alert('" & tourokuKekka & "');").ToString, True)
        End If

        '��ʃf�[�^�Ď擾
        If InStr(tourokuKekka, "����") > 0 Then
            Call gameihHyouji("Page_Load")
        End If


    End Sub

    '������@�ύX�@�`�F�b�N
    Private Function SeikyusakiChangedCheck() As Boolean

        Dim csScript As New StringBuilder

        Dim MsgTotal_Tp As String = ""

        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        '��������e��
        Dim strSeikyuusakiNaiyouMei(6) As String
        strSeikyuusakiNaiyouMei(0) = IIf(Me.tbxSeikyuusakiNaiyou1.Text.Trim.Equals(String.Empty), "�y��������e1�z", "�y" & Me.tbxSeikyuusakiNaiyou1.Text.Trim & "�z")
        strSeikyuusakiNaiyouMei(1) = IIf(Me.tbxSeikyuusakiNaiyou2.Text.Trim.Equals(String.Empty), "�y��������e2�z", "�y" & Me.tbxSeikyuusakiNaiyou2.Text.Trim & "�z")
        strSeikyuusakiNaiyouMei(2) = IIf(Me.tbxSeikyuusakiNaiyou3.Text.Trim.Equals(String.Empty), "�y��������e3�z", "�y" & Me.tbxSeikyuusakiNaiyou3.Text.Trim & "�z")
        strSeikyuusakiNaiyouMei(3) = IIf(Me.tbxSeikyuusakiNaiyou4.Text.Trim.Equals(String.Empty), "�y��������e4�z", "�y" & Me.tbxSeikyuusakiNaiyou4.Text.Trim & "�z")
        strSeikyuusakiNaiyouMei(4) = IIf(Me.tbxSeikyuusakiNaiyou5.Text.Trim.Equals(String.Empty), "�y��������e5�z", "�y" & Me.tbxSeikyuusakiNaiyou5.Text.Trim & "�z")
        strSeikyuusakiNaiyouMei(5) = IIf(Me.tbxSeikyuusakiNaiyou6.Text.Trim.Equals(String.Empty), "�y��������e6�z", "�y" & Me.tbxSeikyuusakiNaiyou6.Text.Trim & "�z")
        strSeikyuusakiNaiyouMei(6) = IIf(Me.tbxSeikyuusakiNaiyou7.Text.Trim.Equals(String.Empty), "�y��������e7�z", "�y" & Me.tbxSeikyuusakiNaiyou7.Text.Trim & "�z")
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================

        If Me.hid_kakunin_TYS.Value <> "" Then

            If Me.ddlSeikyusakiKbn_Tys.Text = "" And _
                 Me.tbxSeikyusakiCode_Tys.Text = "" And _
                 Me.tbxSeikyusakiEdaban_Tys.Text = "" Then
            Else
                '==================2011/06/28 �ԗ� �C�� �J�n��==========================
                'MsgTotal_Tp = MsgTotal_Tp & "�y�����z�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"
                MsgTotal_Tp = MsgTotal_Tp & strSeikyuusakiNaiyouMei(0) & "�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"
                '==================2011/06/28 �ԗ� �C�� �I����==========================
            End If
        End If

        If Me.hid_kakunin_KOU.Value <> "" Then
            If Me.ddlSeikyusakiKbn_Kouj.Text = "" And _
                 Me.tbxSeikyusakiCode_Kouj.Text = "" And _
                 Me.tbxSeikyusakiEdaban_Kouj.Text = "" Then
            Else
                '==================2011/06/28 �ԗ� �C�� �J�n��==========================
                'MsgTotal_Tp = MsgTotal_Tp & "�y�H���z�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"
                MsgTotal_Tp = MsgTotal_Tp & strSeikyuusakiNaiyouMei(1) & "�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"
                '==================2011/06/28 �ԗ� �C�� �I����==========================
            End If

        End If

        If Me.hid_kakunin_HAN.Value <> "" Then

            If Me.ddlSeikyusakiKbn_Hansoku.Text = "" And _
                 Me.tbxSeikyusakiCode_Hansoku.Text = "" And _
                 Me.tbxSeikyusakiEdaban_Hansoku.Text = "" Then
            Else
                '==================2011/06/28 �ԗ� �C�� �J�n��==========================
                'MsgTotal_Tp = MsgTotal_Tp & "�y�̑��i�z�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"
                MsgTotal_Tp = MsgTotal_Tp & strSeikyuusakiNaiyouMei(2) & "�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"
                '==================2011/06/28 �ԗ� �C�� �I����==========================
            End If

        End If

        If Me.hid_kakunin_TAT.Value <> "" Then

            If Me.ddlSeikyusakiKbn_Tatemono.Text = "" And _
                 Me.tbxSeikyusakiCode_Tatemono.Text = "" And _
                 Me.tbxSeikyusakiEdaban_Tatemono.Text = "" Then
            Else
                '==================2011/06/28 �ԗ� �C�� �J�n��==========================
                'MsgTotal_Tp = MsgTotal_Tp & "�y�݌v�m�F�z�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"
                MsgTotal_Tp = MsgTotal_Tp & strSeikyuusakiNaiyouMei(3) & "�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"
                '==================2011/06/28 �ԗ� �C�� �I����==========================
            End If

        End If

        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        If Me.hid_kakunin_SK5.Value <> "" Then

            If Me.ddlSeikyusakiKbn_Sk5.Text = "" And _
                 Me.tbxSeikyusakiCode_Sk5.Text = "" And _
                 Me.tbxSeikyusakiEdaban_Sk5.Text = "" Then
            Else
                MsgTotal_Tp = MsgTotal_Tp & strSeikyuusakiNaiyouMei(4) & "�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"

            End If

        End If

        If Me.hid_kakunin_SK6.Value <> "" Then

            If Me.ddlSeikyusakiKbn_Sk6.Text = "" And _
                 Me.tbxSeikyusakiCode_Sk6.Text = "" And _
                 Me.tbxSeikyusakiEdaban_Sk6.Text = "" Then
            Else
                MsgTotal_Tp = MsgTotal_Tp & strSeikyuusakiNaiyouMei(5) & "�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"

            End If

        End If

        If Me.hid_kakunin_SK7.Value <> "" Then

            If Me.ddlSeikyusakiKbn_Sk7.Text = "" And _
                 Me.tbxSeikyusakiCode_Sk7.Text = "" And _
                 Me.tbxSeikyusakiEdaban_Sk7.Text = "" Then
            Else
                MsgTotal_Tp = MsgTotal_Tp & strSeikyuusakiNaiyouMei(6) & "�̐����悪�ύX����Ă��܂��B�����{�^���������Ă�������\n"

            End If

        End If
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================

        If MsgTotal_Tp <> "" Then

            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_eikyusakiChangedCheck", csScript.Append("alert('" & MsgTotal_Tp & "');").ToString, True)

            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' ��ʓ��̓`�F�b�N
    ''' </summary>
    ''' <param name="btnKbn">����{�^���敪</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function PageItemInputCheck(ByVal btnKbn As String) As Boolean

        Dim csScript As New StringBuilder
        Dim MsgTotal As String
        Dim CltID As String

        If btnKbn = "�o�^" Then

            '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��
            'TbxItemInputCheck(Me.tbxSSkk, "SS���i", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxSSkk, "SS���i", "����", 8)

            'TbxItemInputCheck(Me.tbxSaitys, "�Ē������i", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxSaitys, "�Ē������i", "����", 8)

            'TbxItemInputCheck(Me.tbxSSGRkk, "SSGR���i", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxSSGRkk, "SSGR���i", "����", 8)

            'TbxItemInputCheck(Me.tbxHousyomu, "�ۏؖ����i", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxHousyomu, "�ۏؖ����i", "����", 8)

            'TbxItemInputCheck(Me.tbxKaiseki, "��͕ۏ؉��i", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxKaiseki, "��͕ۏ؉��i", "����", 8)

            'TbxItemInputCheck(Me.tbxJizentys, "���O�������i", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxJizentys, "���O�������i", "����", 8)

            TbxItemInputCheck(Me.tbxKaiyaku, "��񕥖߉��i", "���p����", , 1)
            TbxItemInputCheck(Me.tbxKaiyaku, "��񕥖߉��i", "����", 8)

            '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��
            TbxItemInputCheck(Me.tbxKaisekiHosyouKkk, "SDS�ȊO ��t��͕i���ۏؗ�", "���p����", , 1)
            TbxItemInputCheck(Me.tbxKaisekiHosyouKkk, "SDS�ȊO ��t��͕i���ۏؗ�", "����", 8)
            TbxItemInputCheck(Me.tbxSsgrKkk, "SDS ��t��͕i���ۏؗ�", "���p����", , 1)
            TbxItemInputCheck(Me.tbxSsgrKkk, "SDS ��t��͕i���ۏؗ�", "����", 8)
            '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��

            'TbxItemInputCheck(Me.tbxTysSekyuSaki, "����������", "���p�p����")
            'TbxItemInputCheck(Me.tbxTysSekyuSaki, "����������", "����", 5)
            'TbxItemInputCheck(Me.tbxKoujSekyuSaki, "�H��������", "���p�p����")
            'TbxItemInputCheck(Me.tbxKoujSekyuSaki, "�H��������", "����", 5)
            'TbxItemInputCheck(Me.tbxHansokuSekyuSaki, "�̑��i������", "���p�p����")
            'TbxItemInputCheck(Me.tbxHansokuSekyuSaki, "�̑��i������", "����", 5)

            'TbxItemInputCheck(Me.tbxTysSekyuDate, "�����������ߓ�", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxTysSekyuDate, "�����������ߓ�", "����", 2)

            If InStr(CommonMsgAndFocusBL.Message, "�����������ߓ�") > 0 Then
            Else

                'TbxItemInputCheck(Me.tbxTysSekyuDate, "�����������ߓ�", "�͈�")
            End If

            'TbxItemInputCheck(Me.tbxKoujSekyuDate, "�H���������ߓ�", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxKoujSekyuDate, "�H���������ߓ�", "����", 2)

            If InStr(CommonMsgAndFocusBL.Message, "�H���������ߓ�") > 0 Then

            Else
                'TbxItemInputCheck(Me.tbxKoujSekyuDate, "�H���������ߓ�", "�͈�")
            End If

            'TbxItemInputCheck(Me.tbxHansokuSekyuDate, "�̑��i�������ߓ�", "���p����", , 1)
            'TbxItemInputCheck(Me.tbxHansokuSekyuDate, "�̑��i�������ߓ�", "����", 2)
            If InStr(CommonMsgAndFocusBL.Message, "�̑��i�������ߓ�") > 0 Then
            Else
                'TbxItemInputCheck(Me.tbxHansokuSekyuDate, "�̑��i�������ߓ�", "�͈�")
            End If
            '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��

            '�F�j�Q100606�@�^�@������@�@�ǉ��@�F�j�Q100606�@�^�@������@�@�ǉ��F�j�Q100606�@�^�@������@�@�ǉ�

            '==================2011/06/28 �ԗ� �C�� �J�n��==========================
            'TbxItemInputCheck(Me.tbxSeikyusakiCode_Tys, "����������R�[�h", "���p�p����")
            'TbxItemInputCheck(Me.tbxSeikyusakiCode_Tys, "����������R�[�h", "����", 5)

            'TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Tys, "����������}��", "���p�p����")
            'TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Tys, "����������}��", "����", 2)


            'TbxItemInputCheck(Me.tbxSeikyusakiCode_Kouj, "�H��������R�[�h", "���p�p����")
            'TbxItemInputCheck(Me.tbxSeikyusakiCode_Kouj, "�H��������R�[�h", "����", 5)

            'TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Kouj, "�H��������}��", "���p�p����")
            'TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Kouj, "�H��������}��", "����", 2)


            'TbxItemInputCheck(Me.tbxSeikyusakiCode_Hansoku, "�̑��i������R�[�h", "���p�p����")
            'TbxItemInputCheck(Me.tbxSeikyusakiCode_Hansoku, "�̑��i������R�[�h", "����", 5)

            'TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Hansoku, "�̑��i������}��", "���p�p����")
            'TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Hansoku, "�̑��i������}��", "����", 2)


            'TbxItemInputCheck(Me.tbxSeikyusakiCode_Tatemono, "�݌v�m�F������R�[�h", "���p�p����")
            'TbxItemInputCheck(Me.tbxSeikyusakiCode_Tatemono, "�݌v�m�F������R�[�h", "����", 5)

            'TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Tatemono, "�݌v�m�F������}��", "���p�p����")
            'TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Tatemono, "�݌v�m�F������}��", "����", 2)

            '��������e��
            Dim strSeikyuusakiNaiyouMei(6) As String
            strSeikyuusakiNaiyouMei(0) = IIf(Me.tbxSeikyuusakiNaiyou1.Text.Trim.Equals(String.Empty), "��������e1", Me.tbxSeikyuusakiNaiyou1.Text.Trim)
            strSeikyuusakiNaiyouMei(1) = IIf(Me.tbxSeikyuusakiNaiyou2.Text.Trim.Equals(String.Empty), "��������e2", Me.tbxSeikyuusakiNaiyou2.Text.Trim)
            strSeikyuusakiNaiyouMei(2) = IIf(Me.tbxSeikyuusakiNaiyou3.Text.Trim.Equals(String.Empty), "��������e3", Me.tbxSeikyuusakiNaiyou3.Text.Trim)
            strSeikyuusakiNaiyouMei(3) = IIf(Me.tbxSeikyuusakiNaiyou4.Text.Trim.Equals(String.Empty), "��������e4", Me.tbxSeikyuusakiNaiyou4.Text.Trim)
            strSeikyuusakiNaiyouMei(4) = IIf(Me.tbxSeikyuusakiNaiyou5.Text.Trim.Equals(String.Empty), "��������e5", Me.tbxSeikyuusakiNaiyou5.Text.Trim)
            strSeikyuusakiNaiyouMei(5) = IIf(Me.tbxSeikyuusakiNaiyou6.Text.Trim.Equals(String.Empty), "��������e6", Me.tbxSeikyuusakiNaiyou6.Text.Trim)
            strSeikyuusakiNaiyouMei(6) = IIf(Me.tbxSeikyuusakiNaiyou7.Text.Trim.Equals(String.Empty), "��������e7", Me.tbxSeikyuusakiNaiyou7.Text.Trim)

            TbxItemInputCheck(Me.tbxSeikyusakiCode_Tys, strSeikyuusakiNaiyouMei(0) & "������R�[�h", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiCode_Tys, strSeikyuusakiNaiyouMei(0) & "������R�[�h", "����", 5)

            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Tys, strSeikyuusakiNaiyouMei(0) & "������}��", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Tys, strSeikyuusakiNaiyouMei(0) & "������}��", "����", 2)


            TbxItemInputCheck(Me.tbxSeikyusakiCode_Kouj, strSeikyuusakiNaiyouMei(1) & "������R�[�h", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiCode_Kouj, strSeikyuusakiNaiyouMei(1) & "������R�[�h", "����", 5)

            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Kouj, strSeikyuusakiNaiyouMei(1) & "������}��", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Kouj, strSeikyuusakiNaiyouMei(1) & "������}��", "����", 2)


            TbxItemInputCheck(Me.tbxSeikyusakiCode_Hansoku, strSeikyuusakiNaiyouMei(2) & "������R�[�h", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiCode_Hansoku, strSeikyuusakiNaiyouMei(2) & "������R�[�h", "����", 5)

            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Hansoku, strSeikyuusakiNaiyouMei(2) & "������}��", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Hansoku, strSeikyuusakiNaiyouMei(2) & "������}��", "����", 2)


            TbxItemInputCheck(Me.tbxSeikyusakiCode_Tatemono, strSeikyuusakiNaiyouMei(3) & "������R�[�h", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiCode_Tatemono, strSeikyuusakiNaiyouMei(3) & "������R�[�h", "����", 5)

            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Tatemono, strSeikyuusakiNaiyouMei(3) & "������}��", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Tatemono, strSeikyuusakiNaiyouMei(3) & "������}��", "����", 2)


            TbxItemInputCheck(Me.tbxSeikyusakiCode_Sk5, strSeikyuusakiNaiyouMei(4) & "������R�[�h", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiCode_Sk5, strSeikyuusakiNaiyouMei(4) & "������R�[�h", "����", 5)

            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Sk5, strSeikyuusakiNaiyouMei(4) & "������}��", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Sk5, strSeikyuusakiNaiyouMei(4) & "������}��", "����", 2)


            TbxItemInputCheck(Me.tbxSeikyusakiCode_Sk6, strSeikyuusakiNaiyouMei(5) & "������R�[�h", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiCode_Sk6, strSeikyuusakiNaiyouMei(5) & "������R�[�h", "����", 5)

            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Sk6, strSeikyuusakiNaiyouMei(5) & "������}��", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Sk6, strSeikyuusakiNaiyouMei(5) & "������}��", "����", 2)


            TbxItemInputCheck(Me.tbxSeikyusakiCode_Sk7, strSeikyuusakiNaiyouMei(6) & "������R�[�h", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiCode_Sk7, strSeikyuusakiNaiyouMei(6) & "������R�[�h", "����", 5)

            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Sk7, strSeikyuusakiNaiyouMei(6) & "������}��", "���p�p����")
            TbxItemInputCheck(Me.tbxSeikyusakiEdaban_Sk7, strSeikyuusakiNaiyouMei(6) & "������}��", "����", 2)
            '==================2011/06/28 �ԗ� �C�� �I����==========================

            '�F�j�Q100606�@�^�@������@�@�ǉ��@�F�j�Q100606�@�^�@������@�@�ǉ��F�j�Q100606�@�^�@������@�@�ǉ�

            TbxItemInputCheck(Me.tbxSyouhinCd1, "���i�R�[�h�P", "���p�p����")
            TbxItemInputCheck(Me.tbxSyouhinCd1, "���i�R�[�h�P", "����", 8)
            TbxItemInputCheck(Me.tbxSyouhinCd2, "���i�R�[�h�Q", "���p�p����")
            TbxItemInputCheck(Me.tbxSyouhinCd2, "���i�R�[�h�Q", "����", 8)
            TbxItemInputCheck(Me.tbxSyouhinCd3, "���i�R�[�h�R", "���p�p����")
            TbxItemInputCheck(Me.tbxSyouhinCd3, "���i�R�[�h�R", "����", 8)


        ElseIf btnKbn = "�R�s�[" Then

            TbxItemInputCheck(Me.tbxKameitenCd, "�����X�R�[�h", "���͕K�{")

            If CommonMsgAndFocusBL.Message = "" Then
                TbxItemInputCheck(Me.tbxKameitenCd, "�����X�R�[�h", "���p�p����")
                TbxItemInputCheck(Me.tbxKameitenCd, "�����X�R�[�h", "����", 5)
            End If

        End If

        MsgTotal = CommonMsgAndFocusBL.Message

        If MsgTotal <> "" Then
            CltID = CommonMsgAndFocusBL.focusCtrl.ClientID
            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err_inputCheck", csScript.Append("alert('" & MsgTotal & "');objEBI('" & CltID & "').select();").ToString, True)
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' ���ڂ̓��̓`�F�b�N
    ''' </summary>
    ''' <param name="control">�`�F�b�N�����ʍ���</param>
    ''' <param name="MsgParam">���b�Z�[�W����</param>
    ''' <param name="kbn">�`�F�b�N�ތ^</param>
    ''' <param name="len">�����`�F�b�N���F����</param>
    ''' <param name="Flg">���p�����`�F�b�N���F�����t���O</param>
    ''' <remarks></remarks>
    Protected Sub TbxItemInputCheck(ByVal control As System.Web.UI.Control, _
                                    ByVal MsgParam As String, _
                                    ByVal kbn As String, _
                                    Optional ByVal len As Int64 = 0, _
                                    Optional ByVal Flg As Int16 = 0, _
                                    Optional ByVal kakaFlg As WebUI.kbn = kbn.HANKAKU)

        Dim checkKekka As String = ""
        If CType(control, TextBox).Text.ToString.Trim <> "" Then
            Select Case kbn
                Case "���p����"

                    If Flg = 1 Then
                        checkKekka = CommonCheckFuc.CheckHankaku(KingakuFormat(CType(control, TextBox).Text.ToString), MsgParam, "1")
                    Else
                        checkKekka = CommonCheckFuc.CheckHankaku(KingakuFormat(CType(control, TextBox).Text.ToString), MsgParam)
                    End If

                Case "���p�p����"
                    checkKekka = CommonCheckFuc.ChkHankakuEisuuji(KingakuFormat(CType(control, TextBox).Text.ToString), MsgParam)

                Case "����"
                    checkKekka = CommonCheckFuc.CheckByte(KingakuFormat(CType(control, TextBox).Text.ToString), len, MsgParam, kakaFlg)

                Case "�͈�"
                    checkKekka = HaniCheck(CType(control, TextBox).Text.ToString, MsgParam, 31)
            End Select
        Else
            If kbn = "���͕K�{" Then
                checkKekka = CommonCheckFuc.CheckHissuNyuuryoku(KingakuFormat(CType(control, TextBox).Text.ToString), MsgParam)
            End If
        End If
        If checkKekka <> "" Then
            CommonMsgAndFocusBL.Append(checkKekka)
            CommonMsgAndFocusBL.AppendFocusCtrl(control)
        End If

    End Sub

    ''' <summary>
    ''' �͈̓`�F�b�N
    ''' </summary>
    ''' <param name="target">���ڃR���g���[��</param>
    ''' <param name="MsgParam">���ږ�</param>
    ''' <param name="maxValue">�ő�l</param>
    ''' <returns>�G���[���b�Z�[�W</returns>
    ''' <remarks></remarks>
    Protected Function HaniCheck(ByVal target As String, ByVal MsgParam As String, ByVal maxValue As Int16) As String

        If CType(target, Int16) > maxValue Then
            Return String.Format(Messages.Instance.MSG2015E, MsgParam)
        End If

        Return ""

    End Function

    ''' <summary>
    ''' ���z�f�[�^����R���}������
    ''' </summary>
    ''' <param name="target">���z</param>
    ''' <returns>���z</returns>
    ''' <remarks></remarks>
    Protected Function KingakuFormat(ByVal target As String) As String
        target = target.Trim
        If InStr(target, ",") > 0 Then
            target = target.Replace(",", "")
        End If
        Return target
    End Function

    ''' <summary>
    ''' ������f�[�^�͉��e�[�u���ɍ쐬����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function makeAseikyuTable() As Data.DataTable

        Dim tempTable As New Data.DataTable

        tempTable.Columns.Add("seikyuu_saki_cd")
        tempTable.Columns.Add("seikyuu_saki_brc")
        tempTable.Columns.Add("seikyuu_saki_kbn")
        tempTable.Columns.Add("torikesi")
        tempTable.Columns.Add("add_login_user_id")
        tempTable.Columns.Add("add_datetime")

        Dim i As Integer

        '==========================2011/06/28 �ԗ� �C�� �J�n��================================
        'For i = 0 To 3
        For i = 0 To 6
            '==========================2011/06/28 �ԗ� �C�� �I����================================

            tempTable.Rows.Add()


            If i = 0 Then
                '����

                tempTable.Rows(i).Item("seikyuu_saki_cd") = ""
                tempTable.Rows(i).Item("seikyuu_saki_brc") = ""
                tempTable.Rows(i).Item("seikyuu_saki_kbn") = ""

                If Me.lblSeikyusaki_name_Tys.Text.Length >= 9 Then
                    If Me.lblSeikyusaki_name_Tys.Text.Substring(0, 9) = "�y������V�K�o�^�z" Then
                        tempTable.Rows(i).Item("seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Tys.Text
                        tempTable.Rows(i).Item("seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Tys.Text
                        tempTable.Rows(i).Item("seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Tys.Text
                    End If
                End If



            End If
            If i = 1 Then

                tempTable.Rows(i).Item("seikyuu_saki_cd") = ""
                tempTable.Rows(i).Item("seikyuu_saki_brc") = ""
                tempTable.Rows(i).Item("seikyuu_saki_kbn") = ""

                '�H��
                If Me.lblSeikyusaki_name_Kouj.Text.Length >= 9 Then

                    If Me.lblSeikyusaki_name_Kouj.Text.Substring(0, 9) = "�y������V�K�o�^�z" Then
                        tempTable.Rows(i).Item("seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Kouj.Text
                        tempTable.Rows(i).Item("seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Kouj.Text
                        tempTable.Rows(i).Item("seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Kouj.Text
                    End If
                End If

            End If
            If i = 2 Then

                tempTable.Rows(i).Item("seikyuu_saki_cd") = ""
                tempTable.Rows(i).Item("seikyuu_saki_brc") = ""
                tempTable.Rows(i).Item("seikyuu_saki_kbn") = ""

                If Me.lblSeikyusaki_name_Hansoku.Text.Length >= 9 Then
                    '�̑��i
                    If Me.lblSeikyusaki_name_Hansoku.Text.Substring(0, 9) = "�y������V�K�o�^�z" Then
                        tempTable.Rows(i).Item("seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Hansoku.Text
                        tempTable.Rows(i).Item("seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Hansoku.Text
                        tempTable.Rows(i).Item("seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Hansoku.Text
                    End If
                End If

            End If

            If i = 3 Then

                tempTable.Rows(i).Item("seikyuu_saki_cd") = ""
                tempTable.Rows(i).Item("seikyuu_saki_brc") = ""
                tempTable.Rows(i).Item("seikyuu_saki_kbn") = ""

                If Me.lblSeikyusaki_name_Tatemono.Text.Length >= 9 Then
                    '��������
                    If Me.lblSeikyusaki_name_Tatemono.Text.Substring(0, 9) = "�y������V�K�o�^�z" Then
                        tempTable.Rows(i).Item("seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Tatemono.Text
                        tempTable.Rows(i).Item("seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Tatemono.Text
                        tempTable.Rows(i).Item("seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Tatemono.Text
                    End If
                End If

            End If

            '==========================2011/06/28 �ԗ� �ǉ� �J�n��================================
            If i = 4 Then

                tempTable.Rows(i).Item("seikyuu_saki_cd") = ""
                tempTable.Rows(i).Item("seikyuu_saki_brc") = ""
                tempTable.Rows(i).Item("seikyuu_saki_kbn") = ""

                If Me.lblSeikyusaki_name_Sk5.Text.Length >= 9 Then
                    '��������e5
                    If Me.lblSeikyusaki_name_Sk5.Text.Substring(0, 9) = "�y������V�K�o�^�z" Then
                        tempTable.Rows(i).Item("seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Sk5.Text
                        tempTable.Rows(i).Item("seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Sk5.Text
                        tempTable.Rows(i).Item("seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Sk5.Text
                    End If
                End If

            End If

            If i = 5 Then

                tempTable.Rows(i).Item("seikyuu_saki_cd") = ""
                tempTable.Rows(i).Item("seikyuu_saki_brc") = ""
                tempTable.Rows(i).Item("seikyuu_saki_kbn") = ""

                If Me.lblSeikyusaki_name_Sk6.Text.Length >= 9 Then
                    '��������e6
                    If Me.lblSeikyusaki_name_Sk6.Text.Substring(0, 9) = "�y������V�K�o�^�z" Then
                        tempTable.Rows(i).Item("seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Sk6.Text
                        tempTable.Rows(i).Item("seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Sk6.Text
                        tempTable.Rows(i).Item("seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Sk6.Text
                    End If
                End If

            End If

            If i = 6 Then

                tempTable.Rows(i).Item("seikyuu_saki_cd") = ""
                tempTable.Rows(i).Item("seikyuu_saki_brc") = ""
                tempTable.Rows(i).Item("seikyuu_saki_kbn") = ""

                If Me.lblSeikyusaki_name_Sk7.Text.Length >= 9 Then
                    '��������e7
                    If Me.lblSeikyusaki_name_Sk7.Text.Substring(0, 9) = "�y������V�K�o�^�z" Then
                        tempTable.Rows(i).Item("seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Sk7.Text
                        tempTable.Rows(i).Item("seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Sk7.Text
                        tempTable.Rows(i).Item("seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Sk7.Text
                    End If
                End If

            End If
            '==========================2011/06/28 �ԗ� �ǉ� �I����================================

            tempTable.Rows(i).Item("torikesi") = 0

            tempTable.Rows(i).Item("add_login_user_id") = ViewState.Item("user_id")

        Next

        Return tempTable

    End Function

    ''' <summary>
    ''' ��ʑ��������f�[�^�͉��e�[�u���ɍ쐬����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function makeAtatouTable() As TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable

        Dim tempTable As New TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable

        Dim kameitenCd = ViewState.Item("kameiten_cd_touroku")

        Dim i As Integer

        For i = 0 To 2

            tempTable.Rows.Add(tempTable.NewRow)
            tempTable.Rows(i).Item("toukubun") = i + 1

            If i = 0 Then
                tempTable.Rows(i).Item("syouhin_cd") = tbxSyouhinCd1.Text.ToString.Trim
            End If
            If i = 1 Then
                tempTable.Rows(i).Item("syouhin_cd") = tbxSyouhinCd2.Text.ToString.Trim
            End If
            If i = 2 Then
                tempTable.Rows(i).Item("syouhin_cd") = tbxSyouhinCd3.Text.ToString.Trim
            End If

            tempTable.Rows(i).Item("kameiten_cd") = kameitenCd

            tempTable.Rows(i).Item("upd_login_user_id") = ViewState.Item("user_id")

            tempTable.Rows(i).Item("add_login_user_id") = ViewState.Item("user_id")

        Next

        Return tempTable

    End Function

    ''' <summary>
    ''' ��ʉ����X�f�[�^�͉��e�[�u���ɍ쐬����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function makeAkameitenTable() As KameitenDataSet.m_kameitenTableDataTable

        Dim tempTable As New KameitenDataSet.m_kameitenTableDataTable
        Dim i As Integer
        i = 0

        tempTable.Rows.Add(tempTable.NewRow)
        tempTable.Rows(i).Item("kameiten_cd") = ViewState.Item("kameiten_cd_touroku")

        '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��
        'If Me.tbxSSkk.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("ss_kkk") = Me.tbxSSkk.Text.Replace(",", "")
        'End If

        'If Me.tbxSSGRkk.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("ssgr_kkk") = Me.tbxSSGRkk.Text.Replace(",", "")
        'End If

        'If Me.tbxKaiseki.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("kaiseki_hosyou_kkk") = Me.tbxKaiseki.Text.Replace(",", "")
        'End If

        If Me.tbxKaiyaku.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("kaiyaku_haraimodosi_kkk") = Me.tbxKaiyaku.Text.Replace(",", "")
        End If

        '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��
        If Me.tbxKaisekiHosyouKkk.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("kaiseki_hosyou_kkk") = Me.tbxKaisekiHosyouKkk.Text.Replace(",", "")
        End If
        If Me.tbxSsgrKkk.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("ssgr_kkk") = Me.tbxSsgrKkk.Text.Replace(",", "")
        End If
        '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ�ǉ�����@-------------��

        'If Me.tbxSaitys.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("sai_tys_kkk") = Me.tbxSaitys.Text.Replace(",", "")
        'End If

        'If Me.tbxHousyomu.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("hosyounasi_umu") = Me.tbxHousyomu.Text.Replace(",", "")
        'End If

        'If Me.tbxJizentys.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("jizen_tys_kkk") = Me.tbxJizentys.Text.Replace(",", "")
        'End If

        'If Me.tbxTysSekyuSaki.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("tys_seikyuu_saki") = Me.tbxTysSekyuSaki.Text
        'End If

        'If Me.tbxTysSekyuDate.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("tys_seikyuu_sime_date") = Me.tbxTysSekyuDate.Text
        'End If

        'If Me.tbxKoujSekyuSaki.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("koj_seikyuusaki") = Me.tbxKoujSekyuSaki.Text
        'End If

        'If Me.tbxKoujSekyuDate.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("koj_seikyuu_sime_date") = Me.tbxKoujSekyuDate.Text
        'End If

        'If Me.tbxHansokuSekyuSaki.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("hansokuhin_seikyuusaki") = Me.tbxHansokuSekyuSaki.Text
        'End If

        'If Me.tbxHansokuSekyuDate.Text.ToString.Trim <> "" Then
        '    tempTable.Rows(i).Item("hansokuhin_seikyuu_sime_date") = Me.tbxHansokuSekyuDate.Text
        'End If
        '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��


        ':) 100607 �^�@������@�@�ǉ��@����������������������������������

        If Me.ddlSeikyusakiKbn_Hansoku.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("hansokuhin_seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Hansoku.Text
        End If
        If Me.ddlSeikyusakiKbn_Kouj.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("koj_seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Kouj.Text
        End If
        If Me.ddlSeikyusakiKbn_Tatemono.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("tatemono_seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Tatemono.Text
        End If
        If Me.ddlSeikyusakiKbn_Tys.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("tys_seikyuu_saki_kbn") = Me.ddlSeikyusakiKbn_Tys.Text
        End If
        '==========================2011/06/28 �ԗ� �ǉ� �J�n��===================================
        If Me.ddlSeikyusakiKbn_Sk5.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_kbn5") = Me.ddlSeikyusakiKbn_Sk5.Text
        End If
        If Me.ddlSeikyusakiKbn_Sk6.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_kbn6") = Me.ddlSeikyusakiKbn_Sk6.Text
        End If
        If Me.ddlSeikyusakiKbn_Sk7.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_kbn7") = Me.ddlSeikyusakiKbn_Sk7.Text
        End If
        '==========================2011/06/28 �ԗ� �ǉ� �I����===================================

        If Me.tbxSeikyusakiCode_Hansoku.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("hansokuhin_seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Hansoku.Text
        End If
        If Me.tbxSeikyusakiCode_Kouj.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("koj_seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Kouj.Text
        End If
        If Me.tbxSeikyusakiCode_Tatemono.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("tatemono_seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Tatemono.Text
        End If
        If Me.tbxSeikyusakiCode_Tys.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("tys_seikyuu_saki_cd") = Me.tbxSeikyusakiCode_Tys.Text
        End If
        '==========================2011/06/28 �ԗ� �ǉ� �J�n��===================================
        If Me.tbxSeikyusakiCode_Sk5.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_cd5") = Me.tbxSeikyusakiCode_Sk5.Text
        End If
        If Me.tbxSeikyusakiCode_Sk6.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_cd6") = Me.tbxSeikyusakiCode_Sk6.Text
        End If
        If Me.tbxSeikyusakiCode_Sk7.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_cd7") = Me.tbxSeikyusakiCode_Sk7.Text
        End If
        '==========================2011/06/28 �ԗ� �ǉ� �I����===================================

        If Me.tbxSeikyusakiEdaban_Hansoku.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("hansokuhin_seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Hansoku.Text
        End If
        If Me.tbxSeikyusakiEdaban_Kouj.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("koj_seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Kouj.Text
        End If
        If Me.tbxSeikyusakiEdaban_Tatemono.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("tatemono_seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Tatemono.Text
        End If
        If Me.tbxSeikyusakiEdaban_Tys.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("tys_seikyuu_saki_brc") = Me.tbxSeikyusakiEdaban_Tys.Text
        End If
        '==========================2011/06/28 �ԗ� �ǉ� �J�n��===================================
        If Me.tbxSeikyusakiEdaban_Sk5.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_brc5") = Me.tbxSeikyusakiEdaban_Sk5.Text
        End If
        If Me.tbxSeikyusakiEdaban_Sk6.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_brc6") = Me.tbxSeikyusakiEdaban_Sk6.Text
        End If
        If Me.tbxSeikyusakiEdaban_Sk7.Text.ToString.Trim <> "" Then
            tempTable.Rows(i).Item("seikyuu_saki_brc7") = Me.tbxSeikyusakiEdaban_Sk7.Text
        End If
        '==========================2011/06/28 �ԗ� �ǉ� �I����===================================

        '(: �@������@�@�ǉ��@��������������������������������������������


        tempTable.Rows(i).Item("upd_login_user_id") = ViewState.Item("user_id")

        Return tempTable

    End Function

    ''' <summary>
    ''' ���i�R�[�h�͔z��ɍ쐬����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function syouhinCdToArr() As String()

        Dim syouhinCd(2) As String

        syouhinCd(0) = Me.tbxSyouhinCd1.Text
        syouhinCd(1) = Me.tbxSyouhinCd2.Text
        syouhinCd(2) = Me.tbxSyouhinCd3.Text

        Return syouhinCd

    End Function

    ''' <summary>
    ''' ���R�s�[�{�^���N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click

        If PageItemInputCheck("�R�s�[") Then

            ViewState.Item("kameiten_cd") = Me.tbxKameitenCd.Text.ToString.Trim()
            Call gameihHyouji("btnCopy_Click")

        End If

    End Sub

    Protected Sub btnKihonSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKihonSet.Click

        Call KihonSetEvent()

    End Sub


    Protected Sub btnSeikyusaki_Sel_Tys_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeikyusaki_Sel_Tys.Click _
                                                                        , btnSeikyusaki_Sel_Kouj.Click _
                                                                        , btnSeikyusaki_Sel_Hansoku.Click _
                                                                        , btnSeikyusaki_Sel_Tatemono.Click _
                                                                        , btnSeikyusaki_Sel_Sk5.Click _
                                                                        , btnSeikyusaki_Sel_Sk6.Click _
                                                                        , btnSeikyusaki_Sel_Sk7.Click

        Call KensakuEvent(CType(sender, Button).ID)

    End Sub


    Protected Sub btnSeikyusaki_Syousai_Tys_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeikyusaki_Syousai_Tys.Click _
                                                                        , btnSeikyusaki_Syousai_Kouj.Click _
                                                                        , btnSeikyusaki_Syousai_Hansoku.Click _
                                                                        , btnSeikyusaki_Syousai_Tatemono.Click _
                                                                        , btnSeikyusaki_Syousai_Sk5.Click _
                                                                        , btnSeikyusaki_Syousai_Sk6.Click _
                                                                        , btnSeikyusaki_Syousai_Sk7.Click

        Call SyousaiEvent(CType(sender, Button).ID)

    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click

        'confrim�I�����ʂ��̔��f
        Call reload_confrim()

    End Sub

End Class